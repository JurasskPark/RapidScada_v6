using System;
using System.Net;

namespace HexTools
{
    /// <summary>
    /// Provides methods for endianness conversion.
    /// <para>Предоставляет методы для преобразования порядка байтов.</para>
    /// </summary>
    public static class Endian
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the system is little endian
        /// </summary>
        public static bool IsLittleEndian { get; } = BitConverter.IsLittleEndian;

        /// <summary>
        /// Gets a value indicating whether the system is big endian
        /// </summary>
        public static bool IsBigEndian { get; } = !BitConverter.IsLittleEndian;

        #endregion

        #region Swap Methods (Manual)

        /// <summary>
        /// Swaps byte order in Int16 (short) value
        /// </summary>
        public static short SwapInt16(short value)
        {
            return (short)(((value >> 8) & 0xFF) | ((value & 0xFF) << 8));
        }

        /// <summary>
        /// Swaps byte order in UInt16 (ushort) value
        /// </summary>
        public static ushort SwapUInt16(ushort value)
        {
            return (ushort)(((value >> 8) & 0xFF) | ((value & 0xFF) << 8));
        }

        /// <summary>
        /// Swaps byte order in Int32 (int) value
        /// </summary>
        public static int SwapInt32(int value)
        {
            value = (int)(((value >> 16) & 0xFFFF) | ((value & 0xFFFF) << 16));
            return (int)(((value >> 8) & 0xFF00FF) | ((value & 0xFF00FF) << 8));
        }

        /// <summary>
        /// Swaps byte order in UInt32 (uint) value
        /// </summary>
        public static uint SwapUInt32(uint value)
        {
            value = ((value >> 16) & 0xFFFF) | ((value & 0xFFFF) << 16);
            return ((value >> 8) & 0xFF00FF) | ((value & 0xFF00FF) << 8);
        }

        /// <summary>
        /// Swaps byte order in Int64 (long) value
        /// </summary>
        public static long SwapInt64(long value)
        {
            value = ((value >> 32) & 0xFFFFFFFF) | ((value & 0xFFFFFFFF) << 32);
            value = ((value >> 16) & 0xFFFF0000FFFF) | ((value & 0xFFFF0000FFFF) << 16);
            return ((value >> 8) & 0xFF00FF00FF00FF) | ((value & 0xFF00FF00FF00FF) << 8);
        }

        /// <summary>
        /// Swaps byte order in UInt64 (ulong) value
        /// </summary>
        public static ulong SwapUInt64(ulong value)
        {
            value = ((value >> 32) & 0xFFFFFFFF) | ((value & 0xFFFFFFFF) << 32);
            value = ((value >> 16) & 0xFFFF0000FFFF) | ((value & 0xFFFF0000FFFF) << 16);
            return ((value >> 8) & 0xFF00FF00FF00FF) | ((value & 0xFF00FF00FF00FF) << 8);
        }

        #endregion

        #region .NET Built-in Methods

        /// <summary>
        /// Converts short from host byte order to network byte order (big endian)
        /// </summary>
        public static short HostToNetworkOrder(short value)
        {
            return IPAddress.HostToNetworkOrder(value);
        }

        /// <summary>
        /// Converts int from host byte order to network byte order (big endian)
        /// </summary>
        public static int HostToNetworkOrder(int value)
        {
            return IPAddress.HostToNetworkOrder(value);
        }

        /// <summary>
        /// Converts long from host byte order to network byte order (big endian)
        /// </summary>
        public static long HostToNetworkOrder(long value)
        {
            return IPAddress.HostToNetworkOrder(value);
        }

        /// <summary>
        /// Converts short from network byte order (big endian) to host byte order
        /// </summary>
        public static short NetworkToHostOrder(short value)
        {
            return IPAddress.NetworkToHostOrder(value);
        }

        /// <summary>
        /// Converts int from network byte order (big endian) to host byte order
        /// </summary>
        public static int NetworkToHostOrder(int value)
        {
            return IPAddress.NetworkToHostOrder(value);
        }

        /// <summary>
        /// Converts long from network byte order (big endian) to host byte order
        /// </summary>
        public static long NetworkToHostOrder(long value)
        {
            return IPAddress.NetworkToHostOrder(value);
        }

        #endregion

        #region Array Conversion

        /// <summary>
        /// Converts byte array from one endianness to another
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <param name="fromEndian">Source endianness</param>
        /// <param name="toEndian">Target endianness</param>
        /// <returns>Converted byte array</returns>
        public static byte[] ConvertEndianness(byte[]? bytes, Endianness fromEndian, Endianness toEndian)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Array.Empty<byte>();
            }

            // If endianness is the same, return a copy.
            if (fromEndian == toEndian)
            {
                byte[] copy = new byte[bytes.Length];
                Array.Copy(bytes, copy, bytes.Length);
                return copy;
            }

            byte[] result = new byte[bytes.Length];

            // Reverse the entire array for a single value of any size.
            // Note: this works for any size, but assumes the array contains only one value.
            for (int i = 0; i < bytes.Length; i++)
            {
                result[i] = bytes[bytes.Length - 1 - i];
            }

            return result;
        }

        /// <summary>
        /// Converts byte array containing multiple values of specified size
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="valueSize">Size of each value in bytes (2, 4, 8)</param>
        /// <param name="fromEndian">Source endianness</param>
        /// <param name="toEndian">Target endianness</param>
        /// <returns>Converted byte array</returns>
        public static byte[] ConvertEndianness(byte[]? bytes, int valueSize, Endianness fromEndian, Endianness toEndian)
        {
            if (bytes == null || bytes.Length == 0 || valueSize <= 0)
            {
                return Array.Empty<byte>();
            }

            if (bytes.Length % valueSize != 0)
            {
                return Array.Empty<byte>();
            }

            // If endianness is the same, return a copy.
            if (fromEndian == toEndian)
            {
                byte[] copy = new byte[bytes.Length];
                Array.Copy(bytes, copy, bytes.Length);
                return copy;
            }

            byte[] result = new byte[bytes.Length];
            int valueCount = bytes.Length / valueSize;

            for (int i = 0; i < valueCount; i++)
            {
                int offset = i * valueSize;

            // Reverse bytes within each value.
                for (int j = 0; j < valueSize; j++)
                {
                    result[offset + j] = bytes[offset + valueSize - 1 - j];
                }
            }

            return result;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Endianness enumeration
        /// </summary>
        public enum Endianness
        {
            /// <summary>Little endian (least significant byte first)</summary>
            LittleEndian,
            /// <summary>Big endian (most significant byte first)</summary>
            BigEndian
        }

        /// <summary>
        /// Gets the system endianness
        /// </summary>
        public static Endianness SystemEndianness =>
            IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;

        /// <summary>
        /// Converts value to target endianness if needed
        /// </summary>
        public static short ToEndianness(short value, Endianness targetEndianness)
        {
            if (SystemEndianness == targetEndianness)
            {
                return value;
            }
            return SwapInt16(value);
        }

        /// <summary>
        /// Converts value to target endianness if needed
        /// </summary>
        public static int ToEndianness(int value, Endianness targetEndianness)
        {
            if (SystemEndianness == targetEndianness)
            {
                return value;
            }
            return SwapInt32(value);
        }

        /// <summary>
        /// Converts value to target endianness if needed
        /// </summary>
        public static long ToEndianness(long value, Endianness targetEndianness)
        {
            if (SystemEndianness == targetEndianness)
            {
                return value;
            }
            return SwapInt64(value);
        }

        /// <summary>
        /// Converts value to target endianness if needed
        /// </summary>
        public static ushort ToEndianness(ushort value, Endianness targetEndianness)
        {
            if (SystemEndianness == targetEndianness)
            {
                return value;
            }
            return SwapUInt16(value);
        }

        /// <summary>
        /// Converts value to target endianness if needed
        /// </summary>
        public static uint ToEndianness(uint value, Endianness targetEndianness)
        {
            if (SystemEndianness == targetEndianness)
            {
                return value;
            }
            return SwapUInt32(value);
        }

        /// <summary>
        /// Converts value to target endianness if needed
        /// </summary>
        public static ulong ToEndianness(ulong value, Endianness targetEndianness)
        {
            if (SystemEndianness == targetEndianness)
            {
                return value;
            }
            return SwapUInt64(value);
        }

        #endregion
    }
}
