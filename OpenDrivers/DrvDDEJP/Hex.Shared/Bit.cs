using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with bits and bit strings.
    /// <para>Предоставляет методы для работы с битами и битовыми строками.</para>
    /// </summary>
    public static class Bit
    {
        #region Byte to Bit String

        /// <summary>
        /// Converts byte to binary string (8 bits)
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="msbFirst">If true, most significant bit first (left to right)</param>
        /// <returns>Binary string (e.g. "10101010")</returns>
        public static string ToBinaryString(byte value, bool msbFirst = true)
        {
            char[] bits = new char[8];

            for (int i = 0; i < 8; i++)
            {
                if (msbFirst)
                {
                    // MSB first: bit 7 is leftmost.
                    bits[i] = ((value >> (7 - i)) & 1) == 1 ? '1' : '0';
                }
                else
                {
                    // LSB first: bit 0 is leftmost.
                    bits[i] = ((value >> i) & 1) == 1 ? '1' : '0';
                }
            }

            return new string(bits);
        }

        /// <summary>
        /// Converts byte array to binary string (each byte as 8 bits)
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="separator">Separator between bytes (e.g. " ")</param>
        /// <param name="msbFirst">If true, most significant bit first</param>
        /// <returns>Binary string</returns>
        public static string ToBinaryString(byte[]? bytes, string separator = " ", bool msbFirst = true)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            try
            {
                var result = new StringBuilder(bytes.Length * 9); // 8 bits + separator

                for (int i = 0; i < bytes.Length; i++)
                {
                    if (i > 0 && !string.IsNullOrEmpty(separator))
                    {
                        result.Append(separator);
                    }

                    result.Append(ToBinaryString(bytes[i], msbFirst));
                }

                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Bit String to Byte

        /// <summary>
        /// Converts binary string to byte
        /// </summary>
        /// <param name="binaryString">Binary string (e.g. "10101010")</param>
        /// <returns>Byte value or null on error</returns>
        public static byte? FromBinaryString(string binaryString)
        {
            if (string.IsNullOrWhiteSpace(binaryString))
            {
                return null;
            }

            try
            {
            // Remove separators.
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
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts binary string to byte array
        /// </summary>
        /// <param name="binaryString">Binary string with optional separators</param>
        /// <returns>Byte array or empty array on error</returns>
        public static byte[] FromBinaryStringToArray(string binaryString)
        {
            if (string.IsNullOrWhiteSpace(binaryString))
            {
                return Array.Empty<byte>();
            }

            try
            {
            // Remove separators.
                string clean = binaryString.Replace(" ", "").Replace("-", "").Replace("_", "");

            // Ensure the length is a multiple of 8.
                int padding = (8 - (clean.Length % 8)) % 8;
                if (padding > 0)
                {
                    clean = new string('0', padding) + clean;
                }

                int byteCount = clean.Length / 8;
                byte[] result = new byte[byteCount];

                for (int i = 0; i < byteCount; i++)
                {
                    string byteString = clean.Substring(i * 8, 8);
                    result[i] = FromBinaryString(byteString) ?? 0;
                }

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Integer to Bit String

        /// <summary>
        /// Converts integer to binary string
        /// </summary>
        /// <param name="value">Integer value</param>
        /// <param name="bitCount">Number of bits to show (default 32 for int)</param>
        /// <returns>Binary string</returns>
        public static string ToBinaryString(int value, int bitCount = 32)
        {
            if (bitCount <= 0 || bitCount > 64)
            {
                bitCount = 32;
            }

            char[] bits = new char[bitCount];

            for (int i = 0; i < bitCount; i++)
            {
                bits[i] = ((value >> (bitCount - 1 - i)) & 1) == 1 ? '1' : '0';
            }

            return new string(bits);
        }

        /// <summary>
        /// Converts integer to binary string with grouping
        /// </summary>
        /// <param name="value">Integer value</param>
        /// <param name="groupSize">Group size (e.g. 4 for nibbles)</param>
        /// <param name="groupSeparator">Group separator (e.g. " ")</param>
        /// <returns>Formatted binary string</returns>
        public static string ToBinaryStringFormatted(int value, int groupSize = 4, string groupSeparator = " ")
        {
            string binary = ToBinaryString(value);

            if (groupSize <= 0 || string.IsNullOrEmpty(groupSeparator))
            {
                return binary;
            }

            var result = new StringBuilder();
            for (int i = 0; i < binary.Length; i++)
            {
                if (i > 0 && i % groupSize == 0)
                {
                    result.Append(groupSeparator);
                }
                result.Append(binary[i]);
            }

            return result.ToString();
        }

        #endregion

        #region Hex String to Bit String

        /// <summary>
        /// Converts hex string to binary string
        /// </summary>
        /// <param name="hexString">Hex string (e.g. "FF" or "FF 00")</param>
        /// <returns>Binary string or empty string on error</returns>
        public static string FromHexString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }

            try
            {
                byte[] bytes = String.ToByteArray(hexString);
                return ToBinaryString(bytes, "");
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts hex string to binary string with custom byte order
        /// </summary>
        /// <param name="hexString">Hex string</param>
        /// <param name="byteOrder">Byte order pattern (e.g. "0123", "3210")</param>
        /// <returns>Binary string</returns>
        public static string FromHexStringWithOrder(string hexString, string byteOrder)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }

            try
            {
                byte[] bytes = String.ToByteArray(hexString);

            // Reorder bytes if a pattern is provided.
                if (!string.IsNullOrEmpty(byteOrder) && byteOrder.Length == bytes.Length)
                {
                    byte[] reordered = new byte[bytes.Length];
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (int.TryParse(byteOrder[i].ToString(), out int index) && index < bytes.Length)
                        {
                            reordered[i] = bytes[index];
                        }
                    }
                    bytes = reordered;
                }

                return ToBinaryString(bytes, "");
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Bit Operations

        /// <summary>
        /// Gets specific bit from byte
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="bitNumber">Bit index (0-7, 0 = LSB)</param>
        /// <returns>True if bit is set</returns>
        public static bool GetBit(byte value, int bitNumber)
        {
            if (bitNumber < 0 || bitNumber > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitNumber), "Bit number must be between 0 and 7");
            }

            return (value & (1 << bitNumber)) != 0;
        }

        /// <summary>
        /// Gets specific bit from byte (MSB order)
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="bitNumber">Bit index (0-7, 0 = MSB)</param>
        /// <returns>True if bit is set</returns>
        public static bool GetBitMsb(byte value, int bitNumber)
        {
            if (bitNumber < 0 || bitNumber > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitNumber), "Bit number must be between 0 and 7");
            }

            return (value & (1 << (7 - bitNumber))) != 0;
        }

        /// <summary>
        /// Sets specific bit in byte
        /// </summary>
        public static byte SetBit(byte value, int bitNumber)
        {
            if (bitNumber < 0 || bitNumber > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitNumber), "Bit number must be between 0 and 7");
            }

            return (byte)(value | (1 << bitNumber));
        }

        /// <summary>
        /// Clears specific bit in byte
        /// </summary>
        public static byte ClearBit(byte value, int bitNumber)
        {
            if (bitNumber < 0 || bitNumber > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitNumber), "Bit number must be between 0 and 7");
            }

            return (byte)(value & ~(1 << bitNumber));
        }

        /// <summary>
        /// Toggles specific bit in byte
        /// </summary>
        public static byte ToggleBit(byte value, int bitNumber)
        {
            if (bitNumber < 0 || bitNumber > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitNumber), "Bit number must be between 0 and 7");
            }

            return (byte)(value ^ (1 << bitNumber));
        }

        #endregion

        #region Bit Array Operations

        /// <summary>
        /// Extracts a range of bits from byte array
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="startBit">Start bit index (0-based)</param>
        /// <param name="bitCount">Number of bits to extract</param>
        /// <returns>Boolean array of extracted bits</returns>
        /// <summary>
        /// Extracts bits from the specified byte array.
        /// </summary>
        public static bool[] ExtractBits(byte[]? bytes, int startBit, int bitCount)
        {
            if (bytes == null || bytes.Length == 0 || bitCount <= 0)
            {
                return Array.Empty<bool>();
            }

            try
            {
                BitArray bits = new BitArray(bytes);

                if (startBit < 0 || startBit >= bits.Count)
                {
                    return Array.Empty<bool>();
                }

                int count = Math.Min(bitCount, bits.Count - startBit);
                bool[] result = new bool[count];

                for (int i = 0; i < count; i++)
                {
                    result[i] = bits[startBit + i];
                }

                return result;
            }
            catch
            {
                return Array.Empty<bool>();
            }
        }

        /// <summary>
        /// Sets bits in byte array from boolean array
        /// </summary>
        /// <param name="bytes">Source byte array (will be modified)</param>
        /// <param name="startBit">Start bit index</param>
        /// <param name="bits">Boolean values to set</param>
        /// <returns>Modified byte array</returns>
        /// <summary>
        /// Sets bits in the specified byte array.
        /// </summary>
        public static byte[] SetBits(byte[] bytes, int startBit, bool[] bits)
        {
            if (bytes == null || bits == null || bits.Length == 0)
            {
                return bytes ?? Array.Empty<byte>();
            }

            try
            {
                BitArray bitArray = new BitArray(bytes);

                for (int i = 0; i < bits.Length; i++)
                {
                    int position = startBit + i;
                    if (position < bitArray.Count)
                    {
                        bitArray[position] = bits[i];
                    }
                }

                byte[] result = new byte[bytes.Length];
                bitArray.CopyTo(result, 0);
                return result;
            }
            catch
            {
                return bytes;
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Counts number of set bits (1's) in byte
        /// </summary>
        public static int CountSetBits(byte value)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((value & (1 << i)) != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Checks if bit pattern matches
        /// </summary>
        public static bool MatchesPattern(byte value, byte mask, byte pattern)
        {
            return (value & mask) == pattern;
        }

        /// <summary>
        /// Reverses bits in byte
        /// </summary>
        public static byte ReverseBits(byte value)
        {
            byte result = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((value & (1 << i)) != 0)
                {
                    result |= (byte)(1 << (7 - i));
                }
            }
            return result;
        }

        #endregion
    }
}
