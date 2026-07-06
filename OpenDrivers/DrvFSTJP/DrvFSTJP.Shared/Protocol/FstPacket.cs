using System;
using System.Collections.Generic;
using System.Linq;

namespace Scada.Comm.Drivers.DrvFSTJP.Protocol
{
    /// <summary>
    /// Represents a single FST protocol packet.
    /// <para>Представляет один пакет протокола ФСТ.</para>
    /// </summary>
    public class FstPacket
    {
        public const byte StartByte1 = 0x0D;
        public const byte StartByte2 = 0x0A;
        public const int HeaderLength = 6;

        public int DestinationAddress { get; set; }

        public int SourceAddress { get; set; }

        public byte Command { get; set; }

        public byte[] Data { get; set; } = Array.Empty<byte>();

        public static byte[] Build(int sourceAddress, int destinationAddress, byte command, params byte[] data)
        {
            byte[] payload = data ?? Array.Empty<byte>();
            if (payload.Length > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(data), "FST packet data block cannot exceed 255 bytes.");
            }

            byte[] packet = new byte[HeaderLength + payload.Length + 1];
            packet[0] = StartByte1;
            packet[1] = StartByte2;
            packet[2] = (byte)((DriverUtils.NormalizeAddress(sourceAddress) << 4) | DriverUtils.NormalizeAddress(destinationAddress));
            packet[3] = command;
            packet[4] = (byte)payload.Length;
            packet[5] = CalculateSum(packet, 0, 5);
            Array.Copy(payload, 0, packet, HeaderLength, payload.Length);
            packet[packet.Length - 1] = CalculateSum(payload, 0, payload.Length);
            return packet;
        }

        public static bool TryParse(IReadOnlyList<byte> bytes, out FstPacket packet, out string errMsg)
        {
            packet = null;
            errMsg = string.Empty;

            if (bytes == null || bytes.Count < HeaderLength + 1)
            {
                errMsg = "Packet is too short.";
                return false;
            }

            int startIndex = FindStart(bytes);
            if (startIndex < 0)
            {
                errMsg = "Packet start marker 0D 0A was not found.";
                return false;
            }

            if (bytes.Count - startIndex < HeaderLength + 1)
            {
                errMsg = "Packet header is incomplete.";
                return false;
            }

            int dataLength = bytes[startIndex + 4];
            int packetLength = HeaderLength + dataLength + 1;
            if (bytes.Count - startIndex < packetLength)
            {
                errMsg = $"Packet is incomplete. Expected {packetLength} bytes.";
                return false;
            }

            byte expectedHeaderSum = CalculateSum(bytes, startIndex, 5);
            byte actualHeaderSum = bytes[startIndex + 5];
            if (expectedHeaderSum != actualHeaderSum)
            {
                errMsg = $"Invalid header checksum. Expected {expectedHeaderSum:X2}, received {actualHeaderSum:X2}.";
                return false;
            }

            byte expectedDataSum = CalculateSum(bytes, startIndex + HeaderLength, dataLength);
            byte actualDataSum = bytes[startIndex + HeaderLength + dataLength];
            if (expectedDataSum != actualDataSum)
            {
                errMsg = $"Invalid data checksum. Expected {expectedDataSum:X2}, received {actualDataSum:X2}.";
                return false;
            }

            byte addressByte = bytes[startIndex + 2];
            byte[] data = new byte[dataLength];
            for (int i = 0; i < dataLength; i++)
            {
                data[i] = bytes[startIndex + HeaderLength + i];
            }

            packet = new FstPacket
            {
                DestinationAddress = addressByte & 0x0F,
                SourceAddress = (addressByte >> 4) & 0x0F,
                Command = bytes[startIndex + 3],
                Data = data
            };

            return true;
        }

        public static int GetExpectedLength(IReadOnlyList<byte> bytes)
        {
            int startIndex = FindStart(bytes);
            if (startIndex < 0 || bytes.Count - startIndex < HeaderLength)
            {
                return 0;
            }

            return startIndex + HeaderLength + bytes[startIndex + 4] + 1;
        }

        public static string ToHex(byte[] bytes)
        {
            return bytes == null ? string.Empty : string.Join(" ", bytes.Select(value => value.ToString("X2")));
        }

        private static int FindStart(IReadOnlyList<byte> bytes)
        {
            if (bytes == null)
            {
                return -1;
            }

            for (int i = 0; i + 1 < bytes.Count; i++)
            {
                if (bytes[i] == StartByte1 && bytes[i + 1] == StartByte2)
                {
                    return i;
                }
            }

            return -1;
        }

        private static byte CalculateSum(IReadOnlyList<byte> bytes, int startIndex, int count)
        {
            int sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += bytes[startIndex + i];
            }

            return unchecked((byte)sum);
        }
    }
}
