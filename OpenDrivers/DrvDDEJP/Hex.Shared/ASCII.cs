#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides methods for ASCII and text encoding conversions.
    /// <para>Предоставляет методы для ASCII и текстовых преобразований кодировки.</para>
    /// </summary>
    public static class Ascii
    {
        #region Constants

        private static readonly Encoding AsciiEncoding = Encoding.ASCII;

        // ASCII control character names.
        private static readonly Dictionary<byte, string> ControlCharNames = new Dictionary<byte, string>
        {
            { 0x00, "[NUL]" }, { 0x01, "[SOH]" }, { 0x02, "[STX]" }, { 0x03, "[ETX]" },
            { 0x04, "[EOT]" }, { 0x05, "[ENQ]" }, { 0x06, "[ACK]" }, { 0x07, "[BEL]" },
            { 0x08, "[BS]" },  { 0x09, "[TAB]" }, { 0x0A, "[LF]" },  { 0x0B, "[VT]" },
            { 0x0C, "[FF]" },  { 0x0D, "[CR]" },  { 0x0E, "[SO]" },  { 0x0F, "[SI]" },
            { 0x10, "[DLE]" }, { 0x11, "[DC1]" }, { 0x12, "[DC2]" }, { 0x13, "[DC3]" },
            { 0x14, "[DC4]" }, { 0x15, "[NAK]" }, { 0x16, "[SYN]" }, { 0x17, "[ETB]" },
            { 0x18, "[CAN]" }, { 0x19, "[EM]" },  { 0x1A, "[SUB]" }, { 0x1B, "[ESC]" },
            { 0x1C, "[FS]" },  { 0x1D, "[GS]" },  { 0x1E, "[RS]" },  { 0x1F, "[US]" },
            { 0x7F, "[DEL]" }
        };

        #endregion

        #region To ByteArray

        /// <summary>
        /// Converts string to ASCII byte array
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>ASCII byte array or empty array on error</returns>
        public static byte[] ToByteArray(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Array.Empty<byte>();
            }

            try
            {
                return AsciiEncoding.GetBytes(text);
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts hex string to ASCII byte array
        /// </summary>
        /// <param name="hexString">Hex string (e.g. "48656C6C6F" for "Hello")</param>
        /// <returns>ASCII byte array</returns>
        public static byte[] FromHexString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return Array.Empty<byte>();
            }

            try
            {
                // Use String.ToByteArray to parse hex.
                return String.ToByteArray(hexString);
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region From ByteArray

        /// <summary>
        /// Converts ASCII byte array to string
        /// </summary>
        /// <param name="bytes">ASCII byte array</param>
        /// <returns>Decoded string or empty string on error</returns>
        public static string FromByteArray(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            try
            {
                return AsciiEncoding.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts ASCII byte array to string with control character handling
        /// </summary>
        /// <param name="bytes">ASCII byte array</param>
        /// <param name="showControlChars">If true, show control chars as [NAME], else as spaces or nothing</param>
        /// <returns>Formatted string</returns>
        public static string FromByteArrayFormatted(byte[]? bytes, bool showControlChars = false)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            try
            {
                var result = new StringBuilder(bytes.Length * 2); // assume average 2 chars per byte

                foreach (byte b in bytes)
                {
                // Printable ASCII characters (32-126).
                    if (b >= 0x20 && b <= 0x7E)
                    {
                        result.Append((char)b);
                    }
                // Control characters.
                    else if (showControlChars && ControlCharNames.TryGetValue(b, out string? name))
                    {
                        result.Append(name);
                    }
                // Add a space for some control characters if they are hidden.
                    else if (!showControlChars && (b == 0x09 || b == 0x20)) // TAB or SPACE
                    {
                        result.Append(' ');
                    }
                // Ignore other control characters.
                }

            // Clean up multiple spaces.
                if (!showControlChars)
                {
                    string temp = result.ToString();
                    while (temp.Contains("  "))
                    {
                        temp = temp.Replace("  ", " ");
                    }
                    return temp;
                }

                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Hex String Conversion

        /// <summary>
        /// Converts ASCII string to hex string
        /// </summary>
        /// <param name="text">ASCII text</param>
        /// <param name="withPrefix">Add "0x" prefix</param>
        /// <returns>Hex string</returns>
        public static string ToHexString(string? text, bool withPrefix = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            try
            {
                byte[] bytes = ToByteArray(text);
                return String.FromByteArray(bytes, "", withPrefix ? "0x" : "");
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts ASCII byte array to hex string
        /// </summary>
        public static string ToHexString(byte[]? bytes, bool withPrefix = false)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            return String.FromByteArray(bytes, "", withPrefix ? "0x" : "");
        }

        /// <summary>
        /// Converts hex string to ASCII string
        /// </summary>
        /// <param name="hexString">Hex string (e.g. "48656C6C6F")</param>
        /// <returns>ASCII string</returns>
        public static string FromHexStringToString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }

            try
            {
                byte[] bytes = String.ToByteArray(hexString);
                return FromByteArray(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Special Format Conversions

        /// <summary>
        /// Converts byte array to ASCII string with protocol format (start: 3A, end: 0D0A)
        /// This is a specific format used in some industrial protocols
        /// </summary>
        public static byte[] ToProtocolFormat(byte[]? data)
        {
            if (data == null || data.Length == 0)
            {
                return Array.Empty<byte>();
            }

            try
            {
            // Format: 0x3A + data + 0x0D + 0x0A.
                byte[] result = new byte[data.Length + 3];
                result[0] = 0x3A; // ':'
                Array.Copy(data, 0, result, 1, data.Length);
                result[result.Length - 2] = 0x0D; // CR
                result[result.Length - 1] = 0x0A; // LF

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Extracts data from protocol format (removes 3A prefix and 0D0A suffix)
        /// </summary>
        public static byte[] FromProtocolFormat(byte[]? protocolData)
        {
            if (protocolData == null || protocolData.Length < 3)
            {
                return Array.Empty<byte>();
            }

            try
            {
            // Check the format.
                if (protocolData[0] != 0x3A ||
                    protocolData[protocolData.Length - 2] != 0x0D ||
                    protocolData[protocolData.Length - 1] != 0x0A)
                {
                    return Array.Empty<byte>();
                }

            // Extract data by skipping the first byte and the last 2 bytes.
                byte[] result = new byte[protocolData.Length - 3];
                Array.Copy(protocolData, 1, result, 0, result.Length);

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Checks if byte array contains only valid ASCII characters (0-127)
        /// </summary>
        public static bool IsValidAscii(byte[]? bytes)
        {
            if (bytes == null)
            {
                return false;
            }

            foreach (byte b in bytes)
            {
                if (b > 127)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if string contains only ASCII characters (0-127)
        /// </summary>
        public static bool IsValidAscii(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            foreach (char c in text)
            {
                if (c > 127)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Gets ASCII control character name
        /// </summary>
        public static string GetControlCharName(byte value)
        {
            return ControlCharNames.TryGetValue(value, out string? name) ? name : $"0x{value:X2}";
        }

        /// <summary>
        /// Converts byte array to readable ASCII representation with control chars visible
        /// </summary>
        public static string ToDebugString(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            var result = new StringBuilder();

            foreach (byte b in bytes)
            {
                if (b >= 0x20 && b <= 0x7E)
                {
                    result.Append((char)b);
                }
                else if (ControlCharNames.TryGetValue(b, out string? name))
                {
                    result.Append(name);
                }
                else
                {
                    result.Append($"\\x{b:X2}");
                }
            }

            return result.ToString();
        }

        #endregion
    }
}
