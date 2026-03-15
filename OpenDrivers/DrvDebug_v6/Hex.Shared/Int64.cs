using System;
using System.Collections.Generic;
using System.Linq;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with Int64 values in hex format.
    /// <para>Предоставляет методы для работы со значениями Int64 в шестнадцатеричном формате.</para>
    /// </summary>
    public static class Int64
    {
        private const int ValueSize = 8; // 8 bytes = 64 bits.

        #region To ByteArray

        /// <summary>
        /// Converts Int64 value to byte array
        /// </summary>
        /// <param name="value">Int64 value to convert</param>
        /// <param name="endianness">Byte order (using Endian.Endianness)</param>
        /// <returns>Byte array (8 bytes)</returns>
        public static byte[] ToByteArray(long value, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (Endian.SystemEndianness != endianness)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// Converts array of Int64 values to byte array
        /// </summary>
        public static byte[] ToByteArray(long[]? values, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (values == null || values.Length == 0)
            {
                return Array.Empty<byte>();
            }

            var bytes = new List<byte>(values.Length * ValueSize);
            foreach (long val in values)
            {
                bytes.AddRange(ToByteArray(val, endianness));
            }

            return bytes.ToArray();
        }

        #endregion

        #region From ByteArray

        /// <summary>
        /// Converts byte array to Int64 value
        /// </summary>
        public static long FromByteArray(byte[]? bytes, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (bytes == null || bytes.Length < ValueSize)
            {
                return 0;
            }

            try
            {
                byte[] temp = new byte[ValueSize];
                Array.Copy(bytes, 0, temp, 0, ValueSize);

                if (Endian.SystemEndianness != endianness)
                {
                    Array.Reverse(temp);
                }

                return BitConverter.ToInt64(temp, 0);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts byte array to array of Int64 values
        /// </summary>
        public static long[] ToArray(byte[]? bytes, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (bytes == null || bytes.Length < ValueSize)
            {
                return Array.Empty<long>();
            }

            try
            {
                int valueCount = bytes.Length / ValueSize;
                long[] values = new long[valueCount];

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
                return Array.Empty<long>();
            }
        }

        #endregion

        #region Hex String Conversion

        /// <summary>
        /// Converts hex string to Int64 value
        /// </summary>
        public static long FromHexString(string hexString, Endian.Endianness endianness = Endian.Endianness.BigEndian)
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
        /// Converts Int64 value to hex string
        /// </summary>
        public static string ToHexString(long value, Endian.Endianness endianness = Endian.Endianness.BigEndian, bool withPrefix = false)
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
        /// Converts Int64 to UInt64 (preserves bits)
        /// </summary>
        public static ulong ToUInt64(long value) => unchecked((ulong)value);

        /// <summary>
        /// Converts UInt64 to Int64 (preserves bits)
        /// </summary>
        public static long FromUInt64(ulong value) => unchecked((long)value);

        /// <summary>
        /// Converts Int64 to double (interpret bits as double)
        /// </summary>
        public static double ToDouble(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// Converts double to Int64 (preserve bit pattern)
        /// </summary>
        public static long FromDouble(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt64(bytes, 0);
        }

        #endregion
    }
}
