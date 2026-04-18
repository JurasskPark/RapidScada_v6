using System;
using System.Collections.Generic;
using System.Linq;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with Int32 values in hex format.
    /// <para>Предоставляет методы для работы со значениями Int32 в шестнадцатеричном формате.</para>
    /// </summary>
    public static class Int32
    {
        #region Constants

        private const int ValueSize = 4; // 4 bytes = 32 bits.

        #endregion

        #region To ByteArray

        /// <summary>
        /// Converts Int32 value to byte array
        /// </summary>
        /// <param name="value">Int32 value to convert</param>
        /// <param name="endianness">Byte order (big endian or little endian)</param>
        /// <returns>Byte array (4 bytes)</returns>
        public static byte[] ToByteArray(int value, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            // BitConverter returns bytes in system endianness.
            // Convert to the target endianness if needed.
            if (Endian.SystemEndianness != endianness)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// Converts array of Int32 values to byte array
        /// </summary>
        /// <param name="values">Array of Int32 values</param>
        /// <param name="endianness">Byte order for each value</param>
        /// <returns>Byte array containing all values</returns>
        public static byte[] ToByteArray(int[]? values, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (values == null || values.Length == 0)
            {
                return Array.Empty<byte>();
            }

            var bytes = new List<byte>(values.Length * ValueSize);
            foreach (int value in values)
            {
                bytes.AddRange(ToByteArray(value, endianness));
            }

            return bytes.ToArray();
        }

        #endregion

        #region From ByteArray

        /// <summary>
        /// Converts byte array to Int32 value
        /// </summary>
        /// <param name="bytes">Byte array (must contain at least 4 bytes)</param>
        /// <param name="endianness">Byte order of the input array</param>
        /// <returns>Int32 value or 0 on error</returns>
        public static int FromByteArray(byte[]? bytes, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (bytes == null || bytes.Length < ValueSize)
            {
                return 0;
            }

            try
            {
            // Create a copy to avoid modifying the original array.
                byte[] temp = new byte[ValueSize];
                Array.Copy(bytes, 0, temp, 0, ValueSize);

            // Convert to system endianness if needed.
                if (Endian.SystemEndianness != endianness)
                {
                    Array.Reverse(temp);
                }

                return BitConverter.ToInt32(temp, 0);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Creates Int32 from four bytes
        /// </summary>
        /// <param name="byte1">First byte (most significant or least depending on endianness)</param>
        /// <param name="byte2">Second byte</param>
        /// <param name="byte3">Third byte</param>
        /// <param name="byte4">Fourth byte (least significant or most depending on endianness)</param>
        /// <param name="endianness">Byte order of the input bytes</param>
        /// <returns>Int32 value</returns>
        public static int FromBytes(byte byte1, byte byte2, byte byte3, byte byte4, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (endianness == Endian.Endianness.BigEndian)
            {
            // Big-endian order: byte1 is MSB, byte4 is LSB.
                return (byte1 << 24) | (byte2 << 16) | (byte3 << 8) | byte4;
            }
            else
            {
            // Little-endian order: byte1 is LSB, byte4 is MSB.
                return (byte4 << 24) | (byte3 << 16) | (byte2 << 8) | byte1;
            }
        }

        /// <summary>
        /// Converts byte array to array of Int32 values
        /// </summary>
        /// <param name="bytes">Byte array (length must be multiple of 4)</param>
        /// <param name="endianness">Byte order of the input array</param>
        /// <returns>Array of Int32 values</returns>
        public static int[] ToArray(byte[]? bytes, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (bytes == null || bytes.Length < ValueSize)
            {
                return Array.Empty<int>();
            }

            try
            {
                int valueCount = bytes.Length / ValueSize;
                int[] values = new int[valueCount];

                for (int i = 0; i < valueCount; i++)
                {
                    byte[] valueBytes = new byte[ValueSize];
                    Array.Copy(bytes, i * ValueSize, valueBytes, 0, ValueSize);
                    values[i] = FromByteArray(valueBytes, endianness);
                }

                return values;
            }
            catch
            {
                return Array.Empty<int>();
            }
        }

        #endregion

        #region Hex String Conversion

        /// <summary>
        /// Converts hex string to Int32 value
        /// </summary>
        /// <param name="hexString">Hex string (e.g. "FFFFFFFF", "7FFFFFFF")</param>
        /// <param name="endianness">Byte order of the hex string</param>
        /// <returns>Int32 value or 0 on error</returns>
        public static int FromHexString(string hexString, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return 0;
            }

            try
            {
                byte[] bytes = String.ToByteArray(hexString);
                return FromByteArray(bytes, endianness);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts Int32 value to hex string
        /// </summary>
        /// <param name="value">Int32 value</param>
        /// <param name="endianness">Byte order for output</param>
        /// <param name="withPrefix">Add "0x" prefix</param>
        /// <returns>Hex string (e.g. "FFFFFFFF")</returns>
        public static string ToHexString(int value, Endian.Endianness endianness = Endian.Endianness.BigEndian, bool withPrefix = false)
        {
            try
            {
                byte[] bytes = ToByteArray(value, endianness);
                return String.FromByteArray(bytes, "", withPrefix ? "0x" : "");
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Conversion Methods

        /// <summary>
        /// Converts Int32 to UInt32 (preserves bits, not value)
        /// </summary>
        /// <param name="value">Int32 value</param>
        /// <returns>UInt32 with same bit pattern</returns>
        public static uint ToUInt32(int value)
        {
            return unchecked((uint)value);
        }

        /// <summary>
        /// Converts UInt32 to Int32 (preserves bits, not value)
        /// </summary>
        /// <param name="value">UInt32 value</param>
        /// <returns>Int32 with same bit pattern</returns>
        public static int FromUInt32(uint value)
        {
            return unchecked((int)value);
        }

        /// <summary>
        /// Converts Int32 to float (interpret bits as float)
        /// </summary>
        /// <param name="value">Int32 value</param>
        /// <returns>Float with same bit pattern</returns>
        public static float ToFloat(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// Converts float to Int32 (preserve bit pattern)
        /// </summary>
        /// <param name="value">Float value</param>
        /// <returns>Int32 with same bit pattern</returns>
        public static int FromFloat(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Converts Int64 (long) to Int32 with overflow handling
        /// </summary>
        /// <param name="value">Int64 value to convert</param>
        /// <returns>Int32 value, clamped to Int32 range on overflow</returns>
        public static int FromInt64(long value)
        {
            if (value > int.MaxValue)
            {
                return int.MaxValue;
            }

            if (value < int.MinValue)
            {
                return int.MinValue;
            }

            return (int)value;
        }

        /// <summary>
        /// Converts Int32 to Int64
        /// </summary>
        public static long ToInt64(int value)
        {
            return value;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Checks if byte array can be converted to Int32 (length >= 4)
        /// </summary>
        /// <param name="bytes">Byte array to check</param>
        /// <returns>True if array can be converted to at least one Int32</returns>
        public static bool CanConvert(byte[]? bytes)
        {
            return bytes != null && bytes.Length >= ValueSize;
        }

        /// <summary>
        /// Gets number of Int32 values in byte array
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>Number of complete Int32 values in array</returns>
        public static int GetValueCount(byte[]? bytes)
        {
            if (bytes == null)
            {
                return 0;
            }
            return bytes.Length / ValueSize;
        }

        /// <summary>
        /// Creates Int32 from high and low words (Int16)
        /// </summary>
        /// <param name="highWord">High word (most significant 16 bits)</param>
        /// <param name="lowWord">Low word (least significant 16 bits)</param>
        /// <returns>Int32 value</returns>
        public static int FromWords(short highWord, short lowWord)
        {
            return (highWord << 16) | (lowWord & 0xFFFF);
        }

        /// <summary>
        /// Gets high word (most significant 16 bits) from Int32
        /// </summary>
        public static short GetHighWord(int value)
        {
            return (short)((value >> 16) & 0xFFFF);
        }

        /// <summary>
        /// Gets low word (least significant 16 bits) from Int32
        /// </summary>
        public static short GetLowWord(int value)
        {
            return (short)(value & 0xFFFF);
        }

        #endregion
    }
}
