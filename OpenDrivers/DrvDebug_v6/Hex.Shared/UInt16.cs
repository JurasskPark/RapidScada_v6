using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with UInt16 values in hex format.
    /// <para>Предоставляет методы для работы со значениями UInt16 в шестнадцатеричном формате.</para>
    /// </summary>
    public static class UInt16
    {
        #region To ByteArray

        /// <summary>
        /// Converts UInt16 value to byte array (big endian)
        /// </summary>
        /// <param name="value">UInt16 value to convert</param>
        /// <returns>Byte array (2 bytes) in big endian order</returns>
        public static byte[] ToByteArray(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            // Ensure big-endian byte order.
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// Converts array of UInt16 values to byte array
        /// </summary>
        /// <param name="values">Array of UInt16 values</param>
        /// <returns>Byte array containing all values in big endian order</returns>
        public static byte[] ToByteArray(ushort[] values)
        {
            if (values == null || values.Length == 0)
            {
                return Array.Empty<byte>();
            }

            var bytes = new List<byte>(values.Length * 2);
            foreach (ushort value in values)
            {
                bytes.AddRange(ToByteArray(value));
            }

            return bytes.ToArray();
        }

        #endregion To ByteArray

        #region From ByteArray

        /// <summary>
        /// Converts byte array to UInt16 value (big endian)
        /// </summary>
        /// <param name="bytes">Byte array (must contain at least 2 bytes)</param>
        /// <returns>UInt16 value or 0 on error</returns>
        public static ushort FromByteArray(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 2)
            {
                return 0;
            }

            // Big-endian order: bytes[0] is the high byte, bytes[1] is the low byte.
            return FromBytes(bytes[1], bytes[0]);
        }

        /// <summary>
        /// Creates UInt16 from low and high bytes
        /// </summary>
        /// <param name="lowByte">Low byte (least significant)</param>
        /// <param name="highByte">High byte (most significant)</param>
        /// <returns>UInt16 value</returns>
        public static ushort FromBytes(byte lowByte, byte highByte)
        {
            return (ushort)((highByte << 8) | lowByte);
        }

        /// <summary>
        /// Converts byte array to array of UInt16 values
        /// </summary>
        /// <param name="bytes">Byte array (length must be multiple of 2)</param>
        /// <returns>Array of UInt16 values</returns>
        public static ushort[] ToArray(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 2)
            {
                return Array.Empty<ushort>();
            }

            int wordCount = bytes.Length / 2;
            ushort[] values = new ushort[wordCount];

            for (int i = 0; i < wordCount; i++)
            {
                byte[] wordBytes = new byte[] { bytes[i * 2], bytes[i * 2 + 1] };
                values[i] = FromByteArray(wordBytes);
            }

            return values;
        }

        #endregion From ByteArray

        #region Hex String Conversion

        /// <summary>
        /// Converts hex string to UInt16 value
        /// </summary>
        /// <param name="hexString">Hex string (e.g. "FF", "00FF", "FF00")</param>
        /// <returns>UInt16 value</returns>
        public static ushort FromHexString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return 0;
            }

            // Use String.ToByteArray for parsing.
            byte[] bytes = String.ToByteArray(hexString);

            // Use the existing FromByteArray method.
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Converts UInt16 value to hex string
        /// </summary>
        /// <param name="value">UInt16 value</param>
        /// <param name="withPrefix">Add "0x" prefix</param>
        /// <returns>Hex string (e.g. "FF00")</returns>
        public static string ToHexString(ushort value, bool withPrefix = false)
        {
            // Use String.FromByteArray for conversion.
            byte[] bytes = ToByteArray(value);
            string hex = String.FromByteArray(bytes);

            return withPrefix ? "0x" + hex : hex;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Checks if value is within UInt16 range (0-65535)
        /// </summary>
        public static bool IsInRange(int value)
        {
            return value >= ushort.MinValue && value <= ushort.MaxValue;
        }

        /// <summary>
        /// Safely converts int to UInt16, returns 0 if out of range
        /// </summary>
        public static ushort SafeConvert(int value)
        {
            if (IsInRange(value))
            {
                return (ushort)value;
            }
            return 0;
        }

        #endregion  Utility Methods
    }
}
