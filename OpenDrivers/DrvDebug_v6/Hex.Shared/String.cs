using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with hex strings.
    /// <para>Предоставляет методы для работы с шестнадцатеричными строками.</para>
    /// </summary>
    public static class String
    {
        #region To ByteArray

        /// <summary>
        /// Converts hex string to byte array
        /// </summary>
        /// <param name="hexString">String with hex values (supports separators: space, dot, comma, colon, semicolon, tab)</param>
        /// <returns>Byte array or empty array on error</returns>
        public static byte[] ToByteArray(string hexString)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return Array.Empty<byte>();
            }
            #endregion Validation

            try
            {
            // Remove the 0x prefix if it exists.
                if (hexString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    hexString = hexString[2..];
                }

            // Split the string by separators.
                string[] hexValues = hexString.Split(
                    new[] { ' ', '.', ',', ':', ';', '\t', '-' },
                    StringSplitOptions.RemoveEmptyEntries);

            // If there are no separators, split by 2 characters.
                if (hexValues.Length == 1 && hexValues[0].Length > 2)
                {
                    return SplitHexString(hexValues[0]);
                }

            // Convert each item.
                return hexValues
                    .Select(item => Convert.ToByte(item, 16))
                    .ToArray();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Splits continuous hex string into bytes
        /// </summary>
        private static byte[] SplitHexString(string hexString)
        {
            // Ensure the length is even.
            if (hexString.Length % 2 != 0)
            {
                hexString = "0" + hexString;
            }

            var bytes = new List<byte>();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string byteString = hexString.Substring(i, 2);
                bytes.Add(Convert.ToByte(byteString, 16));
            }
            return bytes.ToArray();
        }

        #endregion To ByteArray

        #region To String

        /// <summary>
        /// Converts byte array to hex string
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <param name="separator">Separator between bytes (default no separator)</param>
        /// <param name="prefix">Prefix for each byte (e.g. "0x")</param>
        /// <param name="maxLength">Maximum number of bytes (0 = all)</param>
        /// <returns>Hex string</returns>
        /// <summary>
        /// Converts the specified byte array to a value.
        /// </summary>
        public static string FromByteArray(byte[]? bytes, string separator = "", string prefix = "", int maxLength = 0)
        {
            #region Validation
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }
            #endregion Validation

            try
            {
                var result = new StringBuilder(bytes.Length * (2 + prefix.Length + separator.Length));
                int count = maxLength > 0 ? Math.Min(maxLength, bytes.Length) : bytes.Length;

                for (int i = 0; i < count; i++)
                {
                    if (i > 0 && !string.IsNullOrEmpty(separator))
                    {
                        result.Append(separator);
                    }

                    result.Append(prefix);
                    result.Append(bytes[i].ToString("X2"));
                }

                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts byte array to hex string with dots (format: "FF.2A.00")
        /// </summary>
        public static string ToStringWithDots(byte[] bytes, int maxLength = 0)
        {
            return FromByteArray(bytes, ".", "", maxLength);
        }

        /// <summary>
        /// Converts byte array to hex string with 0x prefix (format: "0xFF, 0x2A, 0x00")
        /// </summary>
        public static string ToStringWithPrefix(byte[] bytes, int maxLength = 0)
        {
            return FromByteArray(bytes, ", ", "0x", maxLength);
        }

        /// <summary>
        /// Converts byte to hex string
        /// </summary>
        public static string FromByte(byte b)
        {
            return b.ToString("X2");
        }

        #endregion To String

        #region Format Conversion

        /// <summary>
        /// Converts hex string to another format (e.g. "FF2A00" -> "0xFF, 0x2A, 0x00")
        /// </summary>
        public static string ConvertFormat(string hexString, string targetSeparator = ", ", string targetPrefix = "0x")
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }
            #endregion Validation

            byte[] bytes = ToByteArray(hexString);
            return FromByteArray(bytes, targetSeparator, targetPrefix);
        }

        /// <summary>
        /// Converts hex string to SQL format (0x...)
        /// </summary>
        public static string ToSqlFormat(string hexString)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }
            #endregion Validation

            // Clean the string from separators and prefix.
            string clean = hexString.Replace(".", "")
                                    .Replace(" ", "")
                                    .Replace("0x", "")
                                    .Replace("0X", "");

            return "0x" + clean;
        }

        /// <summary>
        /// Converts SQL format to regular hex string with dots
        /// </summary>
        public static string FromSqlFormat(string sqlHex)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(sqlHex))
            {
                return string.Empty;
            }
            #endregion Validation

            // Remove the 0x prefix if it exists.
            if (sqlHex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                sqlHex = sqlHex[2..];
            }

            return ToStringWithDots(ToByteArray(sqlHex));
        }

        #endregion Format Conversion

        #region To Other Types

        /// <summary>
        /// Converts hex string to float
        /// </summary>
        public static float ToFloat(string hexString)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return 0f;
            }
            #endregion Validation

            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
            return BitConverter.ToSingle(BitConverter.GetBytes(num), 0);
        }

        /// <summary>
        /// Converts hex string to double
        /// </summary>
        public static double ToDouble(string hexString)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return 0d;
            }
            #endregion Validation

            ulong num = ulong.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
            return BitConverter.ToDouble(BitConverter.GetBytes(num), 0);
        }

        /// <summary>
        /// Converts hex string to ASCII string
        /// </summary>
        public static string ToAscii(string hexString)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }
            #endregion Validation

            byte[] bytes = ToByteArray(hexString);
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Converts ASCII string to hex string
        /// </summary>
        public static string FromAscii(string asciiString)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(asciiString))
            {
                return string.Empty;
            }
            #endregion Validation

            byte[] bytes = Encoding.ASCII.GetBytes(asciiString);
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Converts an ASCII string, up to 8 characters long, to a floating point number.
        /// </summary>
        public static double EncodeAscii(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0d;
            }

            byte[] buffer = new byte[8];
            int length = Math.Min(8, text.Length);
            Encoding.ASCII.GetBytes(text, 0, length, buffer, 0);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Converts a Unicode string, up to 4 characters long, to a floating point number.
        /// </summary>
        public static double EncodeUnicode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0d;
            }

            byte[] buffer = new byte[8];
            int length = Math.Min(4, text.Length);
            Encoding.Unicode.GetBytes(text, 0, length, buffer, 0);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Converts the specified value to an ASCII string up to 8 characters long.
        /// </summary>
        public static string DecodeAscii(double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return Encoding.ASCII.GetString(buffer).TrimEnd((char)0);
        }

        /// <summary>
        /// Converts the specified value to a Unicode string up to 4 characters long.
        /// </summary>
        public static string DecodeUnicode(double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return Encoding.Unicode.GetString(buffer).TrimEnd((char)0);
        }

        #endregion To Other Types

        #region Array Operations

        /// <summary>
        /// Splits hex string into array of strings by element size
        /// </summary>
        public static string[] Split(string hexString, int elementSize)
        {
            #region Validation
            if (string.IsNullOrEmpty(hexString) || elementSize <= 0)
            {
                return Array.Empty<string>();
            }
            #endregion Validation

            // Remove possible separators.
            string clean = hexString.Replace(" ", "")
                                    .Replace(".", "")
                                    .Replace(",", "")
                                    .Replace(":", "")
                                    .Replace(";", "")
                                    .Replace("-", "");

            // Ensure the length is a multiple of elementSize.
            int length = clean.Length - (clean.Length % elementSize);

            return Enumerable.Range(0, length / elementSize)
                .Select(i => clean.Substring(i * elementSize, elementSize))
                .ToArray();
        }

        /// <summary>
        /// Extracts a substring with bounds checking.
        /// </summary>
        public static string SafeSubstring(string text, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(text) || startIndex >= text.Length)
            {
                return string.Empty;
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (startIndex + length > text.Length)
            {
                length = text.Length - startIndex;
            }

            return text.Substring(startIndex, length);
        }

        #endregion Array Operations
    }
}
