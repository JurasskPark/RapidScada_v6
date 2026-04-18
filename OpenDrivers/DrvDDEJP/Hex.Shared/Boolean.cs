using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with Boolean values and bit operations.
    /// <para>Предоставляет методы для работы с логическими значениями и битовыми операциями.</para>
    /// </summary>
    public static class Boolean
    {
        #region Single Boolean

        /// <summary>
        /// Converts boolean value to byte array (1 byte: 1 for true, 0 for false)
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <returns>Byte array with 1 byte</returns>
        public static byte[] ToByteArray(bool value)
        {
            return new byte[] { (byte)(value ? 1 : 0) };
        }

        /// <summary>
        /// Converts byte to boolean (0 = false, anything else = true)
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <returns>Boolean value</returns>
        public static bool FromByte(byte value)
        {
            return value != 0;
        }

        /// <summary>
        /// Converts byte array to boolean (first byte: 0 = false, anything else = true)
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>Boolean value or false on error</returns>
        public static bool FromByteArray(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return false;
            }
            return bytes[0] != 0;
        }

        #endregion

        #region Boolean Array to Byte Array

        /// <summary>
        /// Converts boolean array to byte array (8 booleans per byte)
        /// </summary>
        /// <param name="bits">Array of boolean values</param>
        /// <returns>Byte array with bits packed</returns>
        public static byte[] ToByteArray(bool[]? bits)
        {
            if (bits == null || bits.Length == 0)
            {
                return Array.Empty<byte>();
            }

            try
            {
                int numBytes = (bits.Length + 7) / 8; // ceiling division
                byte[] bytes = new byte[numBytes];

                for (int i = 0; i < bits.Length; i++)
                {
                    if (bits[i])
                    {
                        int byteIndex = i / 8;
                        int bitIndex = i % 8;
                        bytes[byteIndex] |= (byte)(1 << (7 - bitIndex)); // MSB first
                    }
                }

                return bytes;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts boolean array to byte array (LSB first variant)
        /// </summary>
        /// <param name="bits">Array of boolean values</param>
        /// <param name="leastSignificantBitFirst">If true, bit 0 is LSB, else bit 0 is MSB</param>
        /// <returns>Byte array with bits packed</returns>
        public static byte[] ToByteArray(bool[]? bits, bool leastSignificantBitFirst)
        {
            if (bits == null || bits.Length == 0)
            {
                return Array.Empty<byte>();
            }

            try
            {
                int numBytes = (bits.Length + 7) / 8;
                byte[] bytes = new byte[numBytes];

                for (int i = 0; i < bits.Length; i++)
                {
                    if (bits[i])
                    {
                        int byteIndex = i / 8;
                        int bitIndex = i % 8;

                        if (leastSignificantBitFirst)
                        {
                            bytes[byteIndex] |= (byte)(1 << bitIndex); // LSB first
                        }
                        else
                        {
                            bytes[byteIndex] |= (byte)(1 << (7 - bitIndex)); // MSB first
                        }
                    }
                }

                return bytes;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Byte Array to Boolean Array

        /// <summary>
        /// Converts byte array to boolean array (each bit becomes a boolean)
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>Array of boolean values</returns>
        public static bool[] ToArray(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Array.Empty<bool>();
            }

            try
            {
                BitArray bits = new BitArray(bytes);
                bool[] result = new bool[bits.Count];

                for (int i = 0; i < bits.Count; i++)
                {
                    result[i] = bits[i];
                }

                return result;
            }
            catch
            {
                return Array.Empty<bool>();
            }
        }

        /// <summary>
        /// Converts byte array to boolean array with specified number of bits
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="bitCount">Number of bits to extract</param>
        /// <returns>Array of boolean values</returns>
        public static bool[] ToArray(byte[]? bytes, int bitCount)
        {
            if (bytes == null || bytes.Length == 0 || bitCount <= 0)
            {
                return Array.Empty<bool>();
            }

            try
            {
                BitArray bits = new BitArray(bytes);
                int count = Math.Min(bitCount, bits.Count);
                bool[] result = new bool[count];

                for (int i = 0; i < count; i++)
                {
                    result[i] = bits[i];
                }

                return result;
            }
            catch
            {
                return Array.Empty<bool>();
            }
        }

        /// <summary>
        /// Converts byte array to boolean array (MSB first)
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="mostSignificantBitFirst">If true, bit 0 is MSB, else bit 0 is LSB</param>
        /// <returns>Array of boolean values</returns>
        public static bool[] ToArray(byte[]? bytes, bool mostSignificantBitFirst)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Array.Empty<bool>();
            }

            try
            {
                bool[] result = new bool[bytes.Length * 8];

                for (int byteIndex = 0; byteIndex < bytes.Length; byteIndex++)
                {
                    for (int bitIndex = 0; bitIndex < 8; bitIndex++)
                    {
                        int resultIndex = byteIndex * 8 + bitIndex;

                        if (mostSignificantBitFirst)
                        {
                // MSB first: bit 0 is most significant.
                            result[resultIndex] = (bytes[byteIndex] & (1 << (7 - bitIndex))) != 0;
                        }
                        else
                        {
                // LSB first: bit 0 is least significant.
                            result[resultIndex] = (bytes[byteIndex] & (1 << bitIndex)) != 0;
                        }
                    }
                }

                return result;
            }
            catch
            {
                return Array.Empty<bool>();
            }
        }

        #endregion

        #region Bit Operations

        /// <summary>
        /// Gets specific bit value from byte
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="bitIndex">Bit index (0-7, 0 = LSB, 7 = MSB)</param>
        /// <returns>True if bit is set, false otherwise</returns>
        public static bool GetBit(byte value, int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "Bit index must be between 0 and 7");
            }

            return (value & (1 << bitIndex)) != 0;
        }

        /// <summary>
        /// Gets specific bit value from byte (MSB order)
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="bitIndex">Bit index (0-7, 0 = MSB, 7 = LSB)</param>
        /// <returns>True if bit is set, false otherwise</returns>
        public static bool GetBitMsb(byte value, int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "Bit index must be between 0 and 7");
            }

            return (value & (1 << (7 - bitIndex))) != 0;
        }

        /// <summary>
        /// Sets specific bit in byte to 1
        /// </summary>
        /// <param name="value">Original byte value</param>
        /// <param name="bitIndex">Bit index (0-7, 0 = LSB, 7 = MSB)</param>
        /// <returns>Byte with bit set</returns>
        public static byte SetBit(byte value, int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "Bit index must be between 0 and 7");
            }

            return (byte)(value | (1 << bitIndex));
        }

        /// <summary>
        /// Clears specific bit in byte to 0
        /// </summary>
        /// <param name="value">Original byte value</param>
        /// <param name="bitIndex">Bit index (0-7, 0 = LSB, 7 = MSB)</param>
        /// <returns>Byte with bit cleared</returns>
        public static byte ClearBit(byte value, int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "Bit index must be between 0 and 7");
            }

            return (byte)(value & ~(1 << bitIndex));
        }

        /// <summary>
        /// Toggles specific bit in byte
        /// </summary>
        /// <param name="value">Original byte value</param>
        /// <param name="bitIndex">Bit index (0-7, 0 = LSB, 7 = MSB)</param>
        /// <returns>Byte with bit toggled</returns>
        public static byte ToggleBit(byte value, int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "Bit index must be between 0 and 7");
            }

            return (byte)(value ^ (1 << bitIndex));
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Converts byte to binary string representation
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="separator">Separator between bits (optional)</param>
        /// <returns>Binary string (e.g. "10101010")</returns>
        public static string ToBinaryString(byte value, string separator = "")
        {
            char[] bits = new char[8];
            for (int i = 7; i >= 0; i--)
            {
                bits[7 - i] = ((value >> i) & 1) == 1 ? '1' : '0';
            }

            if (string.IsNullOrEmpty(separator))
            {
                return new string(bits);
            }

            return string.Join(separator, bits);
        }

        /// <summary>
        /// Creates byte from binary string
        /// </summary>
        /// <param name="binaryString">Binary string (e.g. "10101010")</param>
        /// <returns>Byte value</returns>
        public static byte FromBinaryString(string binaryString)
        {
            if (string.IsNullOrWhiteSpace(binaryString))
            {
                return 0;
            }

            string clean = binaryString.Replace(" ", "").Replace("-", "").Replace("_", "");

            if (clean.Length > 8)
            {
                clean = clean.Substring(0, 8);
            }

            byte result = 0;
            for (int i = 0; i < clean.Length; i++)
            {
                if (clean[clean.Length - 1 - i] == '1')
                {
                    result |= (byte)(1 << i);
                }
            }

            return result;
        }

        #endregion
    }
}
