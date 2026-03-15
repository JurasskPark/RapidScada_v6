using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with Int16 values in hex format.
    /// <para>Предоставляет методы для работы со значениями Int16 в шестнадцатеричном формате.</para>
    /// </summary>
    public static class Int16
    {
        #region To ByteArray

        /// <summary>
        /// Converts Int16 value to byte array (big endian)
        /// </summary>
        /// <param name="value">Int16 value to convert</param>
        /// <returns>Byte array (2 bytes) in big endian order</returns>
        public static byte[] ToByteArray(short value)
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
        /// Converts array of Int16 values to byte array
        /// </summary>
        /// <param name="values">Array of Int16 values</param>
        /// <returns>Byte array containing all values in big endian order</returns>
        public static byte[] ToByteArray(short[] values)
        {
            if (values == null || values.Length == 0)
            {
                return Array.Empty<byte>();
            }

            var bytes = new List<byte>(values.Length * 2);
            foreach (short value in values)
            {
                bytes.AddRange(ToByteArray(value));
            }

            return bytes.ToArray();
        }

        #endregion

        #region From ByteArray

        /// <summary>
        /// Converts byte array to Int16 value (big endian)
        /// </summary>
        /// <param name="bytes">Byte array (must contain at least 2 bytes)</param>
        /// <returns>Int16 value or 0 on error</returns>
        public static short FromByteArray(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 2)
            {
                return 0;
            }

            // Big-endian order: bytes[0] is the high byte, bytes[1] is the low byte.
            return FromBytes(bytes[1], bytes[0]);
        }

        /// <summary>
        /// Creates Int16 from low and high bytes
        /// </summary>
        /// <param name="lowByte">Low byte (least significant)</param>
        /// <param name="highByte">High byte (most significant)</param>
        /// <returns>Int16 value</returns>
        public static short FromBytes(byte lowByte, byte highByte)
        {
            return (short)((highByte << 8) | lowByte);
        }

        /// <summary>
        /// Converts byte array to array of Int16 values
        /// </summary>
        /// <param name="bytes">Byte array (length must be multiple of 2)</param>
        /// <returns>Array of Int16 values</returns>
        public static short[] ToArray(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 2)
            {
                return Array.Empty<short>();
            }

            int wordCount = bytes.Length / 2;
            short[] values = new short[wordCount];

            for (int i = 0; i < wordCount; i++)
            {
                byte[] wordBytes = new byte[] { bytes[i * 2], bytes[i * 2 + 1] };
                values[i] = FromByteArray(wordBytes);
            }

            return values;
        }

        #endregion

        #region Hex String Conversion

        /// <summary>
        /// Converts hex string to Int16 value
        /// </summary>
        /// <param name="hexString">Hex string (e.g. "FF", "00FF", "FF00")</param>
        /// <returns>Int16 value</returns>
        public static short FromHexString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return 0;
            }

            // Use String.ToByteArray to parse the hex string.
            byte[] bytes = String.ToByteArray(hexString);

            return FromByteArray(bytes);
        }

        /// <summary>
        /// Converts Int16 value to hex string
        /// </summary>
        /// <param name="value">Int16 value</param>
        /// <param name="withPrefix">Add "0x" prefix</param>
        /// <returns>Hex string (e.g. "FF00")</returns>
        public static string ToHexString(short value, bool withPrefix = false)
        {
            byte[] bytes = ToByteArray(value);
            string hex = String.FromByteArray(bytes);

            return withPrefix ? "0x" + hex : hex;
        }

        #endregion

        #region Conversion Methods

        /// <summary>
        /// Converts int to Int16 with overflow handling
        /// </summary>
        /// <param name="value">Integer value to convert</param>
        /// <returns>Int16 value, clamped to Int16 range on overflow</returns>
        public static short FromInt(int value)
        {
            if (value > short.MaxValue)
            {
                return short.MaxValue;
            }

            if (value < short.MinValue)
            {
                return short.MinValue;
            }

            return (short)value;
        }

        /// <summary>
        /// Converts UInt16 to Int16 (preserves bits, not value)
        /// </summary>
        /// <param name="value">UInt16 value</param>
        /// <returns>Int16 with same bit pattern</returns>
        public static short FromUInt16(ushort value)
        {
            return unchecked((short)value);
        }

        /// <summary>
        /// Converts Int16 to UInt16 (preserves bits, not value)
        /// </summary>
        /// <param name="value">Int16 value</param>
        /// <returns>UInt16 with same bit pattern</returns>
        public static ushort ToUInt16(short value)
        {
            return unchecked((ushort)value);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Checks if value is within Int16 range (-32768 to 32767)
        /// </summary>
        public static bool IsInRange(int value)
        {
            return value >= short.MinValue && value <= short.MaxValue;
        }

        /// <summary>
        /// Safely converts int to Int16, returns 0 if out of range
        /// </summary>
        public static short SafeConvert(int value)
        {
            if (IsInRange(value))
            {
                return (short)value;
            }
            return 0;
        }

        #endregion
    }
}
