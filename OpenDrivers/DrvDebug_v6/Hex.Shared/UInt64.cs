using System;
using System.Collections.Generic;
using System.Linq;
using static HexTools.Endian;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with UInt64 values in hex format.
    /// <para>Предоставляет методы для работы со значениями UInt64 в шестнадцатеричном формате.</para>
    /// </summary>
    public static class UInt64
    {
        private const int ValueSize = 8; // 8 bytes = 64 bits.

        #region To ByteArray

        /// <summary>
        /// Converts a UInt64 value to a byte array.
        /// </summary>
        public static byte[] ToByteArray(ulong value, Endianness endianness = Endianness.BigEndian)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if (Endian.SystemEndianness != endianness)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// Converts an array of UInt64 values to a byte array.
        /// </summary>
        public static byte[] ToByteArray(ulong[]? values, Endianness endianness = Endianness.BigEndian)
        {
            if (values == null || values.Length == 0)
            {
                return Array.Empty<byte>();
            }

            var bytes = new List<byte>(values.Length * ValueSize);
            foreach (ulong val in values)
            {
                bytes.AddRange(ToByteArray(val, endianness));
            }

            return bytes.ToArray();
        }

        #endregion

        #region From ByteArray

        /// <summary>
        /// Converts a byte array to a UInt64 value.
        /// </summary>
        public static ulong FromByteArray(byte[]? bytes, Endianness endianness = Endianness.BigEndian)
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

                return BitConverter.ToUInt64(temp, 0);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts a byte array to an array of UInt64 values.
        /// </summary>
        public static ulong[] ToArray(byte[]? bytes, Endianness endianness = Endianness.BigEndian)
        {
            if (bytes == null || bytes.Length < ValueSize)
            {
                return Array.Empty<ulong>();
            }

            try
            {
                int valueCount = bytes.Length / ValueSize;
                ulong[] values = new ulong[valueCount];

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
                return Array.Empty<ulong>();
            }
        }

        #endregion

        #region Hex String Conversion

        /// <summary>
        /// Converts a hex string to a UInt64 value.
        /// </summary>
        public static ulong FromHexString(string hexString, Endianness endianness = Endianness.BigEndian)
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
        /// Converts a UInt64 value to a hex string.
        /// </summary>
        public static string ToHexString(ulong value, Endianness endianness = Endianness.BigEndian, bool withPrefix = false)
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
        /// Converts a UInt64 value to an Int64 value.
        /// </summary>
        public static long ToInt64(ulong value) => unchecked((long)value);

        /// <summary>
        /// Converts an Int64 value to a UInt64 value.
        /// </summary>
        public static ulong FromInt64(long value) => unchecked((ulong)value);

        #endregion
    }
}
