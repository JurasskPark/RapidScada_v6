using Scada.Comm.Channels;
using Scada.Comm.Config;
using Scada.Comm.Lang;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProjectDriver
{
    [XmlRoot("DrvDebugConfig")]
    /// <summary>
    /// Represents the driver project configuration.
    /// <para>Представляет конфигурацию проекта драйвера.</para>
    /// </summary>
    public class Project
    {
        public Project()
        {
            Commands = new List<ProjectCommand>();
            Tags = new List<ProjectTag>();
            WriteLogDriver = true;
            MessageTypeLogDriver = Scada.Log.LogMessageType.Action;
            TypeChannel = ChannelBehavior.Mixed;
            StopConditionCheckFormat = TypeCode.Byte;
            StopConditionCheckValueText = "0";
        }

        public ChannelBehavior TypeChannel { get; set; }

        public Scada.Log.LogMessageType MessageTypeLogDriver { get; set; }

        public bool WriteLogDriver { get; set; }

        public int StopConditionCheckAddress { get; set; }

        public int StopConditionCheckLength { get; set; }

        public TypeCode StopConditionCheckFormat { get; set; }

        [XmlIgnore]
        public object StopConditionCheckValue
        {
            get => ParseStopConditionValue(StopConditionCheckValueText, StopConditionCheckFormat);
            set => StopConditionCheckValueText = value == null ? "0" : Convert.ToString(value, CultureInfo.InvariantCulture) ?? "0";
        }

        public string StopConditionCheckValueText { get; set; }

        public bool StopConditionLengthMode { get; set; }

        public bool StopConditionLengthIncludesItself { get; set; }

        public bool LanguageIsRussian { get; set; }

        [XmlArray("Commands")]
        [XmlArrayItem("Command")]
        public List<ProjectCommand> Commands { get; set; }

        [XmlArray("Tags")]
        [XmlArrayItem("Tag")]
        public List<ProjectTag> Tags { get; set; }

        /// <summary>
        /// Loads data from the specified source.
        /// </summary>
        public bool Load(string fileName, out string errMsg)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    Save(fileName, out errMsg);
                    errMsg = string.Empty;
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
                errMsg = CommPhrases.LoadDriverConfigError + ": " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Saves data to the specified destination.
        /// </summary>
        public bool Save(string fileName, out string errMsg)
        {
            try
            {
                Normalize();

                string? directory = Path.GetDirectoryName(fileName);
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
                errMsg = CommPhrases.SaveDriverConfigError + ": " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Gets the file name for the specified device.
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return Scada.Comm.Drivers.DrvDebug.DriverUtils.GetFileName(deviceNum);
        }

        /// <summary>
        /// Copies values from the specified source.
        /// </summary>
        private void CopyFrom(Project source)
        {
            TypeChannel = source.TypeChannel;
            MessageTypeLogDriver = source.MessageTypeLogDriver;
            WriteLogDriver = source.WriteLogDriver;
            StopConditionCheckAddress = source.StopConditionCheckAddress;
            StopConditionCheckLength = source.StopConditionCheckLength;
            StopConditionCheckFormat = source.StopConditionCheckFormat;
            StopConditionCheckValueText = string.IsNullOrWhiteSpace(source.StopConditionCheckValueText)
                ? "0"
                : source.StopConditionCheckValueText;
            StopConditionLengthMode = source.StopConditionLengthMode;
            StopConditionLengthIncludesItself = source.StopConditionLengthIncludesItself;
            LanguageIsRussian = source.LanguageIsRussian;
            Commands = source.Commands ?? new List<ProjectCommand>();
            Tags = source.Tags ?? new List<ProjectTag>();
        }

        /// <summary>
        /// Normalizes the current instance state.
        /// </summary>
        private void Normalize()
        {
            Commands ??= new List<ProjectCommand>();
            Tags ??= new List<ProjectTag>();
            StopConditionCheckValueText = string.IsNullOrWhiteSpace(StopConditionCheckValueText)
                ? "0"
                : StopConditionCheckValueText;

            int commandOrder = 0;
            foreach (ProjectCommand command in Commands)
            {
                if (command == null)
                {
                    continue;
                }

                command.Normalize(commandOrder++);
            }

            int tagOrder = 0;
            foreach (ProjectTag tag in Tags)
            {
                if (tag == null)
                {
                    continue;
                }

                tag.Normalize(tagOrder++);
            }
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        private static object ParseStopConditionValue(string valueText, TypeCode typeCode)
        {
            string text = string.IsNullOrWhiteSpace(valueText) ? "0" : valueText.Trim();

            try
            {
                bool isHex = text.StartsWith("0x", StringComparison.OrdinalIgnoreCase);
                return typeCode switch
                {
                    TypeCode.Byte => isHex ? Convert.ToByte(text[2..], 16) : byte.Parse(text, CultureInfo.InvariantCulture),
                    TypeCode.UInt16 => isHex ? Convert.ToUInt16(text[2..], 16) : ushort.Parse(text, CultureInfo.InvariantCulture),
                    TypeCode.UInt32 => isHex ? Convert.ToUInt32(text[2..], 16) : uint.Parse(text, CultureInfo.InvariantCulture),
                    TypeCode.UInt64 => isHex ? Convert.ToUInt64(text[2..], 16) : ulong.Parse(text, CultureInfo.InvariantCulture),
                    _ => 0
                };
            }
            catch
            {
                return 0;
            }
        }
    }

    public enum CommandDataKind
    {
        Hex = 0,
        Ascii = 1,
        Unicode = 2,
        Template = 3
    }

    public enum CommandRunMode
    {
        Sequence = 0,
        Single = 1,
        Cyclic = 2
    }

    /// <summary>
    /// Represents a configured command.
    /// <para>Представляет настроенную команду.</para>
    /// </summary>
    public class ProjectCommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Order { get; set; }

        public bool Enabled { get; set; } = true;

        public string Name { get; set; } = "Command";

        public CommandDataKind DataKind { get; set; } = CommandDataKind.Hex;

        public string Payload { get; set; } = string.Empty;

        public int DelayMs { get; set; }

        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Normalizes the current instance state.
        /// </summary>
        public void Normalize(int order)
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }

            Order = order;
            Name ??= "Command";
            Payload ??= string.Empty;
            Note ??= string.Empty;
            DelayMs = Math.Max(0, DelayMs);
        }
    }

    public enum TagMode
    {
        Decode = 0,
        Simulate = 1,
        DecodeAndSimulate = 2
    }

    public enum TagDataFormat
    {
        Bool = 0,
        Int16 = 1,
        UInt16 = 2,
        Int32 = 3,
        UInt32 = 4,
        Int64 = 5,
        UInt64 = 6,
        Float = 7,
        Double = 8,
        Ascii = 9,
        Unicode = 10,
        HexString = 11
    }

    public enum ByteOrder
    {
        LittleEndian = 0,
        BigEndian = 1,
        Mixed1032 = 2,
        Mixed2301 = 3
    }

    public enum SimulationKind
    {
        None = 0,
        Ramp = 1,
        Sawtooth = 2,
        Sine = 3,
        Square = 4,
        StringList = 5,
        StringGenerate = 6
    }

    public enum StringSimulationMode
    {
        Enumerate = 0,
        Template = 1
    }

    /// <summary>
    /// Represents a configured project tag.
    /// <para>Представляет настроенный тег проекта.</para>
    /// </summary>
    public class ProjectTag
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Order { get; set; }

        public bool Enabled { get; set; } = true;

        public string Name { get; set; } = "Tag";

        public int Channel { get; set; }

        public string Description { get; set; } = string.Empty;

        public TagMode Mode { get; set; } = TagMode.Decode;

        public int ArrayIndex { get; set; }

        public int DataLength { get; set; } = 2;

        public TagDataFormat DataFormat { get; set; } = TagDataFormat.UInt16;

        public ByteOrder ByteOrder { get; set; } = ByteOrder.LittleEndian;

        public double Coefficient { get; set; } = 1.0;

        public double Offset { get; set; }

        public int Precision { get; set; } = 2;

        public string Units { get; set; } = string.Empty;

        public string TestBytesHex { get; set; } = string.Empty;

        public SimulationKind SimulationKind { get; set; }

        public bool SimulateOnDecodeError { get; set; } = true;

        public ProjectTagSimulationOptions Simulation { get; set; } = new ProjectTagSimulationOptions();

        /// <summary>
        /// Normalizes the current instance state.
        /// </summary>
        public void Normalize(int order)
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }

            Order = order;
            Name ??= "Tag";
            Description ??= string.Empty;
            Units ??= string.Empty;
            TestBytesHex ??= string.Empty;
            DataLength = Math.Max(1, DataLength);
            Precision = Math.Max(0, Precision);
            ArrayIndex = Math.Max(0, ArrayIndex);
            Simulation ??= new ProjectTagSimulationOptions();
            Simulation.Normalize();
        }

        /// <summary>
        /// Gets the configured test bytes.
        /// </summary>
        public byte[] GetTestBytes()
        {
            return HexTools.String.ToByteArray(TestBytesHex);
        }

        /// <summary>
        /// Decodes the specified bytes to text.
        /// </summary>
        public string DecodeValueText(byte[] bytes)
        {
            return ProjectTagCodec.DecodeToText(this, bytes);
        }

        /// <summary>
        /// Decodes the configured test bytes to text.
        /// </summary>
        public string DecodeTestValueText()
        {
            return DecodeValueText(GetTestBytes());
        }
    }

    /// <summary>
    /// Represents simulation options for a project tag.
    /// <para>Представляет параметры симуляции для тега проекта.</para>
    /// </summary>
    public class ProjectTagSimulationOptions
    {
        public int UpdateIntervalMs { get; set; } = 1000;

        public double Min { get; set; }

        public double Max { get; set; } = 100;

        public double StartValue { get; set; }

        public double Step { get; set; } = 1;

        public bool Cycle { get; set; } = true;

        public double ResetValue { get; set; }

        public double Amplitude { get; set; } = 1;

        public double Bias { get; set; }

        public double PeriodSeconds { get; set; } = 60;

        public double PhaseDegrees { get; set; }

        public double LowValue { get; set; }

        public double HighValue { get; set; } = 1;

        public double DutyCyclePercent { get; set; } = 50;

        public StringSimulationMode StringMode { get; set; }

        public string StringValues { get; set; } = string.Empty;

        public string StringDelimiter { get; set; } = ";";

        public string StringTemplate { get; set; } = "TAG_{N}";

        /// <summary>
        /// Normalizes the current instance state.
        /// </summary>
        public void Normalize()
        {
            UpdateIntervalMs = Math.Max(1, UpdateIntervalMs);
            StringValues ??= string.Empty;
            StringDelimiter = string.IsNullOrEmpty(StringDelimiter) ? ";" : StringDelimiter;
            StringTemplate ??= "TAG_{N}";
        }
    }

    /// <summary>
    /// Provides tag encoding and decoding helpers.
    /// <para>Предоставляет вспомогательные методы кодирования и декодирования тегов.</para>
    /// </summary>
    public static class ProjectTagCodec
    {
        /// <summary>
        /// Decodes bytes to text for the specified tag.
        /// </summary>
        public static string DecodeToText(ProjectTag tag, byte[] bytes)
        {
            if (tag == null)
            {
                return string.Empty;
            }

            if (bytes == null || bytes.Length <= tag.ArrayIndex)
            {
                return string.Empty;
            }

            int count = Math.Min(tag.DataLength, bytes.Length - tag.ArrayIndex);
            byte[] segment = new byte[count];
            Array.Copy(bytes, tag.ArrayIndex, segment, 0, count);

            try
            {
                return tag.DataFormat switch
                {
                    TagDataFormat.Bool => (segment[0] != 0).ToString(),
                    TagDataFormat.Int16 => ApplyScale(HexTools.Int16.FromByteArray(ApplyOrder16(segment, tag.ByteOrder)), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.UInt16 => ApplyScale(HexTools.UInt16.FromByteArray(ApplyOrder16(segment, tag.ByteOrder)), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.Int32 => ApplyScale(HexTools.Int32.FromByteArray(ApplyOrder32(segment, tag.ByteOrder), ConvertOrder32(tag.ByteOrder)), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.UInt32 => ApplyScale(HexTools.UInt32.FromByteArray(ApplyOrder32(segment, tag.ByteOrder), ConvertOrder32(tag.ByteOrder)), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.Int64 => ApplyScale(HexTools.Int64.FromByteArray(ApplyOrder64(segment, tag.ByteOrder), ConvertOrder64(tag.ByteOrder)), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.UInt64 => ApplyScale((double)HexTools.UInt64.FromByteArray(ApplyOrder64(segment, tag.ByteOrder), ConvertOrder64(tag.ByteOrder)), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.Float => ApplyScale(HexTools.Single.FromByteArray(ApplyOrder32(segment, tag.ByteOrder), ConvertOrderFloat(tag.ByteOrder)), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.Double => ApplyScale(BitConverter.ToDouble(ApplyOrder64ForBitConverter(segment, tag.ByteOrder), 0), tag).ToString($"F{tag.Precision}", CultureInfo.InvariantCulture),
                    TagDataFormat.Ascii => Encoding.ASCII.GetString(segment).TrimEnd('\0'),
                    TagDataFormat.Unicode => Encoding.Unicode.GetString(AdjustUnicodeBytes(segment, tag.ByteOrder)).TrimEnd('\0'),
                    TagDataFormat.HexString => HexTools.String.FromByteArray(segment, " "),
                    _ => string.Empty
                };
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets a simulation preview for the specified tag.
        /// </summary>
        public static string GetSimulationPreview(ProjectTag tag)
        {
            if (tag == null)
            {
                return string.Empty;
            }

            ProjectTagSimulationOptions options = tag.Simulation ?? new ProjectTagSimulationOptions();
            return tag.SimulationKind switch
            {
                SimulationKind.Ramp => string.Join(" -> ", Enumerable.Range(0, 5)
                    .Select(i => (options.StartValue + options.Step * i).ToString(CultureInfo.InvariantCulture))),
                SimulationKind.Sawtooth => $"{options.Min} -> {options.Min + options.Step} -> {options.Max} -> {options.ResetValue}",
                SimulationKind.Sine => "sin(t)",
                SimulationKind.Square => $"{options.LowValue} -> {options.HighValue} -> {options.LowValue}",
                SimulationKind.StringList => string.Join(" -> ", SplitStringValues(options).Take(4)),
                SimulationKind.StringGenerate => options.StringTemplate.Replace("{N}", "1"),
                _ => string.Empty
            };
        }

        /// <summary>
        /// Splits configured string values.
        /// </summary>
        private static IEnumerable<string> SplitStringValues(ProjectTagSimulationOptions options)
        {
            string delimiter = string.IsNullOrEmpty(options.StringDelimiter) ? ";" : options.StringDelimiter;
            return (options.StringValues ?? string.Empty)
                .Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim());
        }

        /// <summary>
        /// Adjusts unicode bytes according to the configured byte order.
        /// </summary>
        private static byte[] AdjustUnicodeBytes(byte[] bytes, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] clone = (byte[])bytes.Clone();
                for (int i = 0; i + 1 < clone.Length; i += 2)
                {
                    (clone[i], clone[i + 1]) = (clone[i + 1], clone[i]);
                }

                return clone;
            }

            return bytes;
        }

        /// <summary>
        /// Applies scaling to the specified value.
        /// </summary>
        private static double ApplyScale(double value, ProjectTag tag)
        {
            return value * tag.Coefficient + tag.Offset;
        }

        /// <summary>
        /// Applies the configured byte order to the specified bytes.
        /// </summary>
        private static byte[] ApplyOrder16(byte[] bytes, ByteOrder order)
        {
            if (order != ByteOrder.BigEndian)
            {
                return bytes;
            }

            byte[] clone = (byte[])bytes.Clone();
            Array.Reverse(clone);
            return clone;
        }

        /// <summary>
        /// Applies the configured byte order to the specified bytes.
        /// </summary>
        private static byte[] ApplyOrder32(byte[] bytes, ByteOrder order)
        {
            return order switch
            {
                ByteOrder.BigEndian => (byte[])bytes.Clone(),
                ByteOrder.Mixed1032 => Reorder(bytes, 2, 3, 0, 1),
                ByteOrder.Mixed2301 => Reorder(bytes, 1, 0, 3, 2),
                _ => (byte[])bytes.Clone()
            };
        }

        /// <summary>
        /// Applies the configured byte order to the specified bytes.
        /// </summary>
        private static byte[] ApplyOrder64(byte[] bytes, ByteOrder order)
        {
            return order == ByteOrder.BigEndian ? (byte[])bytes.Clone() : ReverseCopy(bytes);
        }

        /// <summary>
        /// Applies the configured byte order to the specified bytes.
        /// </summary>
        private static byte[] ApplyOrder64ForBitConverter(byte[] bytes, ByteOrder order)
        {
            byte[] normalized = order == ByteOrder.BigEndian ? ReverseCopy(bytes) : (byte[])bytes.Clone();
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(normalized);
            }

            return normalized;
        }

        private static HexTools.Endian.Endianness ConvertOrder32(ByteOrder order)
        {
            return order switch
            {
                ByteOrder.BigEndian => HexTools.Endian.Endianness.BigEndian,
                _ => HexTools.Endian.Endianness.LittleEndian
            };
        }

        private static HexTools.Endian.Endianness ConvertOrder64(ByteOrder order)
        {
            return order switch
            {
                ByteOrder.BigEndian => HexTools.Endian.Endianness.BigEndian,
                _ => HexTools.Endian.Endianness.LittleEndian
            };
        }

        private static HexTools.Single.Endianness ConvertOrderFloat(ByteOrder order)
        {
            return order switch
            {
                ByteOrder.BigEndian => HexTools.Single.Endianness.Mixed0123,
                ByteOrder.Mixed1032 => HexTools.Single.Endianness.Mixed1032,
                ByteOrder.Mixed2301 => HexTools.Single.Endianness.Mixed2301,
                _ => HexTools.Single.Endianness.Mixed3210
            };
        }

        /// <summary>
        /// Creates a reversed copy of the specified byte array.
        /// </summary>
        private static byte[] ReverseCopy(byte[] bytes)
        {
            byte[] clone = (byte[])bytes.Clone();
            Array.Reverse(clone);
            return clone;
        }

        /// <summary>
        /// Reorders bytes according to the specified indexes.
        /// </summary>
        private static byte[] Reorder(byte[] bytes, params int[] indexes)
        {
            byte[] result = new byte[indexes.Length];
            for (int i = 0; i < indexes.Length; i++)
            {
                result[i] = bytes[indexes[i]];
            }

            return result;
        }
    }
}
