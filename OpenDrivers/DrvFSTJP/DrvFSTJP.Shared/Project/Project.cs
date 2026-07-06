using Scada.Comm.Drivers.DrvFSTJP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ProjectDriver
{
    /// <summary>
    /// Represents the XML configuration of an FST-03x device.
    /// <para>Представляет XML-конфигурацию прибора ФСТ-03х.</para>
    /// </summary>
    [XmlRoot("DrvFSTJPConfig")]
    public class Project
    {
        public Project()
        {
            MasterAddress = 0;
            DeviceAddress = 1;
            PollLinkCheck = true;
            PollStatus = true;
            DetailedPacketLog = true;
            RequestDelayMs = 50;
            Channels = new List<FstChannelConfig>();
            RelayDevices = new List<FstRelayDeviceConfig>();
        }

        public int MasterAddress { get; set; }

        public int DeviceAddress { get; set; }

        public bool PollLinkCheck { get; set; }

        public bool PollStatus { get; set; }

        public bool DetailedPacketLog { get; set; }

        public int RequestDelayMs { get; set; }

        [XmlArray("Channels")]
        [XmlArrayItem("Channel")]
        public List<FstChannelConfig> Channels { get; set; }

        [XmlArray("RelayDevices")]
        [XmlArrayItem("RelayDevice")]
        public List<FstRelayDeviceConfig> RelayDevices { get; set; }

        public bool Load(string fileName, out string errMsg)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    SetDefaultChannels();
                    Save(fileName, out errMsg);
                    return true;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using FileStream stream = File.OpenRead(fileName);

                if (serializer.Deserialize(stream) is Project loadedProject)
                {
                    CopyFrom(loadedProject);
                }

                Normalize();
                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = "Unable to load FST-03x driver configuration: " + ex.Message;
                return false;
            }
        }

        public bool Save(string fileName, out string errMsg)
        {
            try
            {
                Normalize();

                string directory = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using FileStream stream = File.Create(fileName);
                serializer.Serialize(stream, this);

                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = "Unable to save FST-03x driver configuration: " + ex.Message;
                return false;
            }
        }

        public static string GetFileName(int deviceNum)
        {
            return DriverUtils.GetFileName(deviceNum);
        }

        public void Normalize()
        {
            MasterAddress = DriverUtils.NormalizeAddress(MasterAddress);
            DeviceAddress = DriverUtils.NormalizeAddress(DeviceAddress);
            RequestDelayMs = Math.Max(0, RequestDelayMs);
            Channels ??= new List<FstChannelConfig>();
            RelayDevices ??= new List<FstRelayDeviceConfig>();

            if (Channels.Count == 0)
            {
                SetDefaultChannels();
            }

            Channels = Channels
                .Where(channel => channel != null)
                .GroupBy(channel => DriverUtils.NormalizeChannelNo(channel.ChannelNo))
                .Select(group => group.OrderBy(channel => channel.Order).First())
                .OrderBy(channel => DriverUtils.NormalizeChannelNo(channel.ChannelNo))
                .ToList();

            for (int order = 0; order < Channels.Count; order++)
            {
                Channels[order].Normalize(order);
            }

            RelayDevices = RelayDevices
                .Where(relayDevice => relayDevice != null)
                .OrderBy(relayDevice => relayDevice.Order)
                .ToList();

            for (int order = 0; order < RelayDevices.Count; order++)
            {
                RelayDevices[order].Normalize(order);
            }
        }

        private void SetDefaultChannels()
        {
            Channels = Enumerable.Range(1, 8)
                .Select(channelNo => new FstChannelConfig
                {
                    Order = channelNo - 1,
                    ChannelNo = channelNo,
                    Name = $"Channel {channelNo}",
                    CodePrefix = $"CH{channelNo}",
                    Enabled = true
                })
                .ToList();
        }

        private void CopyFrom(Project source)
        {
            MasterAddress = source.MasterAddress;
            DeviceAddress = source.DeviceAddress;
            PollLinkCheck = source.PollLinkCheck;
            PollStatus = source.PollStatus;
            DetailedPacketLog = source.DetailedPacketLog;
            RequestDelayMs = source.RequestDelayMs;
            Channels = source.Channels ?? new List<FstChannelConfig>();
            RelayDevices = source.RelayDevices ?? new List<FstRelayDeviceConfig>();
        }
    }

    /// <summary>
    /// Represents one FST-03x measurement channel.
    /// <para>Представляет один измерительный канал ФСТ-03х.</para>
    /// </summary>
    public class FstChannelConfig
    {
        public int Order { get; set; }

        public bool Enabled { get; set; } = true;

        public int ChannelNo { get; set; } = 1;

        public string Name { get; set; } = "Channel";

        public string CodePrefix { get; set; } = "CH1";

        public double Coefficient { get; set; } = 1.0;

        public double Offset { get; set; }

        public int Precision { get; set; } = 2;

        public string Units { get; set; } = string.Empty;

        public void Normalize(int order)
        {
            Order = order;
            ChannelNo = DriverUtils.NormalizeChannelNo(ChannelNo);
            Name = string.IsNullOrWhiteSpace(Name) ? $"Channel {ChannelNo}" : Name.Trim();
            CodePrefix = NormalizeCodePrefix(CodePrefix, ChannelNo);
            Precision = Math.Max(0, Precision);
            Units ??= string.Empty;
        }

        private static string NormalizeCodePrefix(string codePrefix, int channelNo)
        {
            string text = string.IsNullOrWhiteSpace(codePrefix)
                ? $"CH{channelNo}"
                : codePrefix.Trim();

            foreach (char character in Path.GetInvalidFileNameChars())
            {
                text = text.Replace(character.ToString(CultureInfo.InvariantCulture), "_");
            }

            return text.Replace(" ", "_");
        }
    }

    /// <summary>
    /// Represents an optional relay expansion block configured in XML.
    /// <para>Представляет дополнительный блок релейного расширения, настроенный в XML.</para>
    /// </summary>
    public class FstRelayDeviceConfig
    {
        public int Order { get; set; }

        public bool Enabled { get; set; } = true;

        public int Address { get; set; } = 2;

        public string Name { get; set; } = "Relay block";

        public string CodePrefix { get; set; } = "BRR";

        public void Normalize(int order)
        {
            Order = order;
            Address = DriverUtils.NormalizeAddress(Address);
            Name = string.IsNullOrWhiteSpace(Name) ? $"Relay block {Address}" : Name.Trim();
            CodePrefix = string.IsNullOrWhiteSpace(CodePrefix) ? $"BRR{Address}" : CodePrefix.Trim().Replace(" ", "_");
        }
    }
}
