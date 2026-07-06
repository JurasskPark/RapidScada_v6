using ProjectDriver;
using Scada.Comm.Channels;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvFSTJP.Protocol;
using Scada.Data.Const;
using Scada.Data.Entities;
using Scada.Data.Models;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Scada.Comm.Drivers.DrvFSTJP.Logic
{
    /// <summary>
    /// Implements communication with one FST-03x device.
    /// <para>Реализует обмен с одним прибором ФСТ-03х.</para>
    /// </summary>
    internal class DevFSTJPLogic : DeviceLogic
    {
        private readonly Project project;
        private readonly byte[] buffer;
        private readonly IReadOnlyList<FstTagDefinition> tagDefinitions;

        public DevFSTJPLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = true;
            ConnectionRequired = true;

            string projectFileName = Path.Combine(commContext.AppDirs.ConfigDir, Project.GetFileName(deviceConfig.DeviceNum));
            project = new Project();
            if (!project.Load(projectFileName, out string errMsg))
            {
                Log.WriteError(errMsg);
            }

            tagDefinitions = FstTagCatalog.Build(project);
            buffer = new byte[4096];
        }

        public override bool CheckBehaviorSupport(ChannelBehavior behavior)
        {
            return behavior == ChannelBehavior.Master;
        }

        public override void InitDeviceTags()
        {
            DeviceTags.FlattenGroups = true;
            TagGroup tagGroup = new TagGroup("FST-03x");

            int index = 0;
            foreach (FstTagDefinition tagDefinition in tagDefinitions)
            {
                DeviceTag deviceTag = tagGroup.AddTag(tagDefinition.Code, tagDefinition.Name);
                deviceTag.Index = index++;
                deviceTag.DataType = TagDataType.Double;
                deviceTag.DataLen = 1;
                deviceTag.Format = new TagFormat(TagFormatType.Number);
            }

            DeviceTags.AddGroup(tagGroup);
            DeviceData.Init();
        }

        public override void Session()
        {
            base.Session();

            try
            {
                bool success = true;

                if (project.PollLinkCheck)
                {
                    success &= PollLinkCheck(project.DeviceAddress);
                    DelayBetweenRequests();
                }

                if (project.PollStatus)
                {
                    success &= PollDeviceStatus(project.DeviceAddress);
                    DelayBetweenRequests();
                }

                foreach (FstRelayDeviceConfig relayDevice in project.RelayDevices
                    .Where(relayDevice => relayDevice.Enabled)
                    .OrderBy(relayDevice => relayDevice.Order))
                {
                    success &= PollRelayStatus(relayDevice);
                    DelayBetweenRequests();
                }

                LastRequestOK = success;
            }
            catch (Exception ex)
            {
                LastRequestOK = false;
                Log.WriteError(ex.Message);
            }

            FinishRequest();
            SleepPollingDelay();
            FinishSession();
        }

        public override void SendCommand(TeleCommand cmd)
        {
            base.SendCommand(cmd);

            try
            {
                byte[] request = BuildTelecommandPacket(cmd);
                if (request.Length == 0)
                {
                    LastRequestOK = false;
                    Log.WriteLine(Locale.IsRussian ? "Неизвестная команда драйвера." : "Unknown driver command.");
                    FinishCommand();
                    return;
                }

                FstPacket response = ExecuteRequest(request, out string errMsg);
                LastRequestOK = response != null;
                if (!LastRequestOK && !string.IsNullOrWhiteSpace(errMsg))
                {
                    Log.WriteLine(errMsg);
                }
            }
            catch (Exception ex)
            {
                LastRequestOK = false;
                Log.WriteError(ex.Message);
            }

            FinishCommand();
        }

        private bool PollLinkCheck(int deviceAddress)
        {
            byte[] request = FstProtocol.BuildLinkCheck(project.MasterAddress, deviceAddress);
            FstPacket response = ExecuteRequest(request, out string errMsg);

            if (response == null)
            {
                LogProtocolError("Link check failed", errMsg);
                return false;
            }

            if (!ValidateResponseAddress(response, deviceAddress) ||
                response.Command != FstProtocol.CmdLinkCheck ||
                response.Data.Length != 1)
            {
                LogProtocolError("Invalid link check response", FstPacket.ToHex(response.Data));
                return false;
            }

            DeviceData.Set("DeviceType", response.Data[0], CnlStatusID.Defined);
            return true;
        }

        private bool PollDeviceStatus(int deviceAddress)
        {
            byte[] request = FstProtocol.BuildStatusRequest(project.MasterAddress, deviceAddress);
            FstPacket response = ExecuteRequest(request, out string errMsg);

            if (response == null)
            {
                LogProtocolError("Status request failed", errMsg);
                return false;
            }

            if (!ValidateResponseAddress(response, deviceAddress))
            {
                LogProtocolError("Invalid status response address", string.Empty);
                return false;
            }

            if (response.Command != FstProtocol.DeviceTypeFst03V &&
                response.Command != FstProtocol.DeviceTypeFst03M)
            {
                LogProtocolError("Invalid status response code", response.Command.ToString("X2"));
                return false;
            }

            if (!FstStatus.TryDecode(response.Data, out FstStatus status, out errMsg))
            {
                LogProtocolError("Unable to decode status response", errMsg);
                return false;
            }

            DeviceData.Set("DeviceType", response.Command, CnlStatusID.Defined);
            DeviceData.Set("GlobalErrors", status.GlobalErrors, CnlStatusID.Defined);

            foreach (FstChannelConfig channelConfig in project.Channels.Where(channel => channel.Enabled))
            {
                FstChannelStatus channelStatus = status.Channels[channelConfig.ChannelNo - 1];
                ApplyChannelStatus(channelConfig, channelStatus);
            }

            return true;
        }

        private bool PollRelayStatus(FstRelayDeviceConfig relayDevice)
        {
            byte[] request = FstProtocol.BuildStatusRequest(project.MasterAddress, relayDevice.Address);
            FstPacket response = ExecuteRequest(request, out string errMsg);

            if (response == null)
            {
                LogProtocolError($"Relay status request failed: {relayDevice.Name}", errMsg);
                return false;
            }

            if (!ValidateResponseAddress(response, relayDevice.Address) ||
                response.Command != FstProtocol.DeviceTypeRelayBlock ||
                response.Data.Length < 2)
            {
                LogProtocolError($"Invalid relay status response: {relayDevice.Name}", FstPacket.ToHex(response.Data));
                return false;
            }

            DeviceData.Set($"{relayDevice.CodePrefix}_StateLo", response.Data[1], CnlStatusID.Defined);
            DeviceData.Set($"{relayDevice.CodePrefix}_StateHi", response.Data[0] & 0x03, CnlStatusID.Defined);
            DeviceData.Set($"{relayDevice.CodePrefix}_Errors", (response.Data[0] >> 2) & 0x3F, CnlStatusID.Defined);
            return true;
        }

        private void ApplyChannelStatus(FstChannelConfig channelConfig, FstChannelStatus channelStatus)
        {
            string prefix = channelConfig.CodePrefix;
            DeviceData.Set($"{prefix}_MessageCode", channelStatus.MessageCode, CnlStatusID.Defined);
            DeviceData.Set($"{prefix}_AlarmCode", channelStatus.HasAlarm ? channelStatus.AlarmCode : 0, CnlStatusID.Defined);
            DeviceData.Set($"{prefix}_SensorType", channelStatus.SensorType, CnlStatusID.Defined);
            DeviceData.Set($"{prefix}_CalibrationRequired", channelStatus.CalibrationRequired ? 1 : 0, CnlStatusID.Defined);
            DeviceData.Set($"{prefix}_Threshold1", channelStatus.Threshold1 ? 1 : 0, CnlStatusID.Defined);
            DeviceData.Set($"{prefix}_Threshold2", channelStatus.Threshold2 ? 1 : 0, CnlStatusID.Defined);
            DeviceData.Set($"{prefix}_Disabled", channelStatus.Disabled ? 1 : 0, CnlStatusID.Defined);

            if (channelStatus.HasConcentration)
            {
                double value = channelStatus.ConcentrationRaw * channelConfig.Coefficient + channelConfig.Offset;
                DeviceData.Set($"{prefix}_Concentration", Math.Round(value, channelConfig.Precision), CnlStatusID.Defined);
            }
        }

        private FstPacket ExecuteRequest(byte[] request, out string errMsg)
        {
            errMsg = string.Empty;
            string logText;

            LogPacket("TX", request);
            Connection.Write(request, 0, request.Length, ProtocolFormat.Hex, out logText);
            LogTransport(logText);

            byte[] responseBytes = ReadPacketBytes(out logText);
            LogTransport(logText);

            if (responseBytes.Length == 0)
            {
                errMsg = "No response received.";
                return null;
            }

            LogPacket("RX", responseBytes);

            if (!FstPacket.TryParse(responseBytes, out FstPacket response, out errMsg))
            {
                return null;
            }

            return response;
        }

        private byte[] ReadPacketBytes(out string logText)
        {
            logText = string.Empty;
            List<byte> receivedBytes = new List<byte>();
            Stopwatch stopwatch = Stopwatch.StartNew();
            long targetTicks = Math.Max(1, PollingOptions.Timeout) * Stopwatch.Frequency / 1000;

            while (stopwatch.ElapsedTicks < targetTicks)
            {
                int readCount = Connection.Read(buffer, 0, buffer.Length, PollingOptions.Timeout, ProtocolFormat.Hex, out logText);
                if (readCount <= 0)
                {
                    continue;
                }

                for (int i = 0; i < readCount; i++)
                {
                    receivedBytes.Add(buffer[i]);
                }

                int expectedLength = FstPacket.GetExpectedLength(receivedBytes);
                if (expectedLength > 0 && receivedBytes.Count >= expectedLength)
                {
                    return receivedBytes.Take(expectedLength).ToArray();
                }
            }

            return receivedBytes.ToArray();
        }

        private bool ValidateResponseAddress(FstPacket response, int expectedSourceAddress)
        {
            return response.SourceAddress == DriverUtils.NormalizeAddress(expectedSourceAddress) &&
                response.DestinationAddress == DriverUtils.NormalizeAddress(project.MasterAddress);
        }

        private byte[] BuildTelecommandPacket(TeleCommand cmd)
        {
            string commandCode = cmd.CmdCode ?? string.Empty;

            if (commandCode.Equals("ResetDevice", StringComparison.OrdinalIgnoreCase))
            {
                return FstProtocol.BuildResetRequest(project.MasterAddress, project.DeviceAddress, 0);
            }

            if (commandCode.Equals("ResetChannel", StringComparison.OrdinalIgnoreCase))
            {
                return FstProtocol.BuildResetRequest(project.MasterAddress, project.DeviceAddress, (int)Math.Round(cmd.CmdVal));
            }

            FstRelayDeviceConfig relayDevice = project.RelayDevices.FirstOrDefault(relay => relay.Enabled);
            if (relayDevice != null)
            {
                if (commandCode.Equals("RelayOn", StringComparison.OrdinalIgnoreCase))
                {
                    return FstProtocol.BuildRelayRequest(project.MasterAddress, relayDevice.Address, FstProtocol.CmdRelayOn, (int)Math.Round(cmd.CmdVal));
                }

                if (commandCode.Equals("RelayOff", StringComparison.OrdinalIgnoreCase))
                {
                    return FstProtocol.BuildRelayRequest(project.MasterAddress, relayDevice.Address, FstProtocol.CmdRelayOff, (int)Math.Round(cmd.CmdVal));
                }

                if (commandCode.Equals("RelaySetMask", StringComparison.OrdinalIgnoreCase))
                {
                    return FstProtocol.BuildRelayStateRequest(project.MasterAddress, relayDevice.Address, (ushort)Math.Max(0, Math.Min(0x03FF, (int)Math.Round(cmd.CmdVal))));
                }
            }

            if (commandCode.Equals("SendFstPacket", StringComparison.OrdinalIgnoreCase))
            {
                return cmd.CmdData ?? Array.Empty<byte>();
            }

            return Array.Empty<byte>();
        }

        private void DelayBetweenRequests()
        {
            if (project.RequestDelayMs > 0)
            {
                Thread.Sleep(project.RequestDelayMs);
            }
        }

        private void LogPacket(string prefix, byte[] packet)
        {
            if (project.DetailedPacketLog)
            {
                Log.WriteLine($"{prefix}: {FstPacket.ToHex(packet)}");
            }
        }

        private void LogTransport(string transportLog)
        {
            if (project.DetailedPacketLog && !string.IsNullOrWhiteSpace(transportLog))
            {
                Log.WriteLine(transportLog);
            }
        }

        private void LogProtocolError(string title, string detail)
        {
            string message = string.IsNullOrWhiteSpace(detail)
                ? title
                : $"{title}: {detail}";
            Log.WriteLine(message);
        }
    }
}
