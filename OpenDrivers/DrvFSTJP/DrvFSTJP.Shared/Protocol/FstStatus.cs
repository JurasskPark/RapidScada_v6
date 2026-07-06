using System;

namespace Scada.Comm.Drivers.DrvFSTJP.Protocol
{
    /// <summary>
    /// Represents the decoded 25-byte FST-03x status block.
    /// <para>Представляет декодированный 25-байтовый блок состояния ФСТ-03х.</para>
    /// </summary>
    public class FstStatus
    {
        public byte GlobalErrors { get; set; }

        public FstChannelStatus[] Channels { get; set; } = Array.Empty<FstChannelStatus>();

        public static bool TryDecode(byte[] data, out FstStatus status, out string errMsg)
        {
            status = null;
            errMsg = string.Empty;

            if (data == null || data.Length < 25)
            {
                errMsg = "FST status response must contain 25 data bytes.";
                return false;
            }

            FstChannelStatus[] channels = new FstChannelStatus[8];
            for (int channelNo = 1; channelNo <= 8; channelNo++)
            {
                int offset = 1 + (channelNo - 1) * 3;
                channels[channelNo - 1] = FstChannelStatus.Decode(channelNo, data[offset], data[offset + 1], data[offset + 2]);
            }

            status = new FstStatus
            {
                GlobalErrors = data[0],
                Channels = channels
            };

            return true;
        }
    }

    /// <summary>
    /// Represents a decoded three-byte channel status word.
    /// <para>Представляет декодированное трехбайтовое слово состояния канала.</para>
    /// </summary>
    public class FstChannelStatus
    {
        public int ChannelNo { get; set; }

        public int SensorType { get; set; }

        public bool CalibrationRequired { get; set; }

        public bool Threshold1 { get; set; }

        public bool Threshold2 { get; set; }

        public bool Disabled { get; set; }

        public int MessageCode { get; set; }

        public int ConcentrationRaw { get; set; }

        public int AlarmCode { get; set; }

        public bool HasConcentration => MessageCode == 1;

        public bool HasAlarm => MessageCode == 2;

        public static FstChannelStatus Decode(int channelNo, byte up, byte hi, byte lo)
        {
            int messageCode = (hi >> 6) & 0x03;
            int dataValue = ((hi & 0x0F) << 8) | lo;

            return new FstChannelStatus
            {
                ChannelNo = channelNo,
                SensorType = (up >> 4) & 0x0F,
                CalibrationRequired = (up & 0x08) != 0,
                Threshold1 = (up & 0x04) != 0,
                Threshold2 = (up & 0x02) != 0,
                Disabled = (up & 0x01) != 0,
                MessageCode = messageCode,
                ConcentrationRaw = dataValue,
                AlarmCode = messageCode == 2 ? lo : 0
            };
        }
    }
}
