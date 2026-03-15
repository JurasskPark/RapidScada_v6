using System;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides visualization utilities for binary data.
    /// <para>Предоставляет средства визуализации двоичных данных.</para>
    /// Shows bit positions with brackets: [01][23][45][67].
    /// </summary>
    public static class BinaryVisualizer
    {
        /// <summary>
        /// Converts byte to binary string with group visualization
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="groupSize">Number of bits per group (default 2)</param>
        /// <returns>Formatted string like "[01][23][45][67]"</returns>
        public static string VisualizeByte(byte value, int groupSize = 2)
        {
            if (groupSize <= 0 || groupSize > 8)
            {
                groupSize = 2;
            }

            string binary = Convert.ToString(value, 2).PadLeft(8, '0');
            var result = new StringBuilder();

            for (int i = 0; i < 8; i += groupSize)
            {
                if (i > 0)
                {
                    result.Append(' ');
                }
                result.Append('[');
                result.Append(binary.Substring(i, Math.Min(groupSize, 8 - i)));
                result.Append(']');
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts ushort to binary string with group visualization
        /// </summary>
        /// <param name="value">16-bit value</param>
        /// <param name="groupSize">Number of bits per group (default 4)</param>
        /// <returns>Formatted string like "[0000][1111][2222][3333]"</returns>
        public static string VisualizeUInt16(ushort value, int groupSize = 4)
        {
            string binary = Convert.ToString(value, 2).PadLeft(16, '0');
            var result = new StringBuilder();

            for (int i = 0; i < 16; i += groupSize)
            {
                if (i > 0)
                {
                    result.Append(' ');
                }
                result.Append('[');
                result.Append(binary.Substring(i, Math.Min(groupSize, 16 - i)));
                result.Append(']');
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts uint to binary string with group visualization
        /// </summary>
        public static string VisualizeUInt32(uint value, int groupSize = 8)
        {
            string binary = Convert.ToString(value, 2).PadLeft(32, '0');
            var result = new StringBuilder();

            for (int i = 0; i < 32; i += groupSize)
            {
                if (i > 0)
                {
                    result.Append(' ');
                }
                result.Append('[');
                result.Append(binary.Substring(i, Math.Min(groupSize, 32 - i)));
                result.Append(']');
            }

            return result.ToString();
        }

        /// <summary>
        /// Highlights specific bit position
        /// </summary>
        /// <param name="value">Byte value</param>
        /// <param name="bitPosition">Bit position to highlight (0-7)</param>
        /// <returns>String with highlighted bit: "[01][2[3]][45][67]"</returns>
        public static string HighlightBit(byte value, int bitPosition)
        {
            if (bitPosition < 0 || bitPosition > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitPosition), "Bit position must be 0-7");
            }

            string binary = Convert.ToString(value, 2).PadLeft(8, '0');
            int groupSize = 2;
            var result = new StringBuilder();

            for (int i = 0; i < 8; i += groupSize)
            {
                if (i > 0)
                {
                    result.Append(' ');
                }

                // Check whether bitPosition is in this group.
                if (bitPosition >= i && bitPosition < i + groupSize)
                {
                    int localPos = bitPosition - i;
                    result.Append('[');
                    string group = binary.Substring(i, groupSize);
                    result.Append(group.Substring(0, localPos));
                    result.Append('[');
                    result.Append(group[localPos]);
                    result.Append(']');
                    result.Append(group.Substring(localPos + 1));
                    result.Append(']');
                }
                else
                {
                    result.Append('[');
                    result.Append(binary.Substring(i, groupSize));
                    result.Append(']');
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts hex string to visualized binary
        /// </summary>
        public static string VisualizeHexString(string hexString, int bitsPerGroup = 4)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }

            try
            {
                byte[] bytes = String.ToByteArray(hexString);

                if (bytes.Length == 1)
                {
                    return VisualizeByte(bytes[0], bitsPerGroup / 2);
                }
                else if (bytes.Length == 2)
                {
                    ushort value = (ushort)((bytes[0] << 8) | bytes[1]);
                    return VisualizeUInt16(value, bitsPerGroup);
                }
                else if (bytes.Length == 4)
                {
                    uint value = (uint)((bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3]);
                    return VisualizeUInt32(value, bitsPerGroup);
                }

            // For longer arrays, show each byte.
                var result = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (i > 0)
                    {
                        result.Append(' ');
                    }
                    result.Append(VisualizeByte(bytes[i], bitsPerGroup / 2));
                }
                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
