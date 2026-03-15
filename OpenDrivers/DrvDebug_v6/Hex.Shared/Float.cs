using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with Single values in hex format.
    /// <para>Предоставляет методы для работы со значениями Single в шестнадцатеричном формате.</para>
    /// Supports 4 byte order variants: 0123 (big endian), 3210 (little endian), 1032 and 2301 (mixed endian for PLC/SCADA systems).
    /// </summary>
    public static class Single
    {
        #region Constants

        private const int SingleSize = 4; // 4 bytes = 32 bits.

        #endregion

        #region Endianness

        /// <summary>
        /// Byte order for 32-bit values (float, UInt32, Int32)
        /// </summary>
        public enum Endianness
        {
            /// <summary>
            /// 0123 - Big endian (most significant byte first)
            /// Network order, Modbus, most PLCs
            /// </summary>
            Mixed0123 = 0x0123,

            /// <summary>
            /// 3210 - Little endian (least significant byte first)
            /// Intel x86/x64, Windows
            /// </summary>
            Mixed3210 = 0x3210,

            /// <summary>
            /// 1032 - Mixed endian (word order preserved, bytes within word reversed)
            /// Some DSP processors
            /// </summary>
            Mixed1032 = 0x1032,

            /// <summary>
            /// 2301 - Mixed endian (words reversed, bytes within word preserved)
            /// Some PLCs (Siemens, etc.)
            /// </summary>
            Mixed2301 = 0x2301
        }

        #endregion

        #region To ByteArray

        /// <summary>
        /// Converts float value to byte array
        /// </summary>
        /// <param name="value">Float value to convert</param>
        /// <param name="endianness">Byte order (big endian, little endian, mixed1032, mixed2301)</param>
        /// <returns>Byte array (4 bytes) or empty array on error</returns>
        public static byte[] ToByteArray(float value, Endianness endianness = Endianness.Mixed0123)
        {
            try
            {
                byte[] bytes = BitConverter.GetBytes(value); // BitConverter returns system byte order

            // BitConverter on a little-endian system returns 3210 order.
            // BitConverter on a big-endian system returns 0123 order.

                if (BitConverter.IsLittleEndian)
                {
                // The system is little-endian.
                    switch (endianness)
                    {
                        case Endianness.Mixed0123:      // want 0123
                            return new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] };

                        case Endianness.Mixed3210:    // want 3210
                            return new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] };

                        case Endianness.Mixed1032:        // want 1032
                            return new byte[] { bytes[2], bytes[3], bytes[0], bytes[1] };

                        case Endianness.Mixed2301:        // want 2301
                            return new byte[] { bytes[1], bytes[0], bytes[3], bytes[2] };
                    }
                }
                else
                {
                // The system is big-endian.
                    switch (endianness)
                    {
                        case Endianness.Mixed0123:      // want 0123
                            return new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] };

                        case Endianness.Mixed3210:    // want 3210
                            return new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] };

                        case Endianness.Mixed1032:        // want 1032
                            return new byte[] { bytes[1], bytes[0], bytes[3], bytes[2] };

                        case Endianness.Mixed2301:        // want 2301
                            return new byte[] { bytes[2], bytes[3], bytes[0], bytes[1] };
                    }
                }

                return Array.Empty<byte>();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts array of float values to byte array
        /// </summary>
        /// <param name="values">Array of float values</param>
        /// <param name="endianness">Byte order for each float</param>
        /// <returns>Byte array containing all values</returns>
        public static byte[] ToByteArray(float[]? values, Endianness endianness = Endianness.Mixed0123)
        {
            if (values == null || values.Length == 0)
            {
                return Array.Empty<byte>();
            }

            try
            {
                var bytes = new List<byte>(values.Length * SingleSize);
                foreach (float value in values)
                {
                    bytes.AddRange(ToByteArray(value, endianness));
                }

                return bytes.ToArray();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region From ByteArray

        /// <summary>
        /// Reorders bytes according to specified endianness without modifying original array
        /// </summary>
        private static byte[] ReorderBytes(byte[] bytes, Endianness endianness)
        {
            byte[] result = new byte[SingleSize];

            switch (endianness)
            {
                case Endianness.Mixed0123:      // 0123
                    result[0] = bytes[0];
                    result[1] = bytes[1];
                    result[2] = bytes[2];
                    result[3] = bytes[3];
                    break;

                case Endianness.Mixed3210:    // 3210
                    result[0] = bytes[3];
                    result[1] = bytes[2];
                    result[2] = bytes[1];
                    result[3] = bytes[0];
                    break;

                case Endianness.Mixed1032:        // 1032
                    result[0] = bytes[1];
                    result[1] = bytes[0];
                    result[2] = bytes[3];
                    result[3] = bytes[2];
                    break;

                case Endianness.Mixed2301:        // 2301
                    result[0] = bytes[2];
                    result[1] = bytes[3];
                    result[2] = bytes[0];
                    result[3] = bytes[1];
                    break;
            }

            return result;
        }

        /// <summary>
        /// Converts byte array to float value
        /// </summary>
        /// <param name="bytes">Byte array (must contain at least 4 bytes)</param>
        /// <param name="endianness">Byte order of the input array</param>
        /// <returns>Float value or 0 on error</returns>
        public static float FromByteArray(byte[]? bytes, Endianness endianness = Endianness.Mixed0123)
        {
            if (bytes == null || bytes.Length < SingleSize)
            {
                return 0f;
            }

            try
            {
            // Create a copy to avoid modifying the original array.
                byte[] temp = new byte[SingleSize];
                Array.Copy(bytes, 0, temp, 0, SingleSize);

            // Reorder bytes according to the specified endianness.
                byte[] reordered = ReorderBytes(temp, endianness);

            // BitConverter expects bytes in system order.
                if (BitConverter.IsLittleEndian)
                {
                // The system is little-endian, so reverse to the expected format.
                    Array.Reverse(reordered);
                }

                return BitConverter.ToSingle(reordered, 0);
            }
            catch
            {
                return 0f;
            }
        }

        /// <summary>
        /// Converts byte array to array of float values
        /// </summary>
        /// <param name="bytes">Byte array (length must be multiple of 4)</param>
        /// <param name="endianness">Byte order of the input array</param>
        /// <returns>Array of float values</returns>
        public static float[] ToArray(byte[]? bytes, Endianness endianness = Endianness.Mixed0123)
        {
            if (bytes == null || bytes.Length < SingleSize)
            {
                return Array.Empty<float>();
            }

            try
            {
                int floatCount = bytes.Length / SingleSize;
                float[] values = new float[floatCount];

                for (int i = 0; i < floatCount; i++)
                {
                    byte[] floatBytes = new byte[SingleSize];
                    Array.Copy(bytes, i * SingleSize, floatBytes, 0, SingleSize);
                    values[i] = FromByteArray(floatBytes, endianness);
                }

                return values;
            }
            catch
            {
                return Array.Empty<float>();
            }
        }

        #endregion

        #region Hex String Conversion

        /// <summary>
        /// Converts hex string to float
        /// </summary>
        public static float FromHexString(string hexString, Endianness endianness = Endianness.Mixed0123)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return 0f;
            }

            byte[] bytes = String.ToByteArray(hexString);
            return FromByteArray(bytes, endianness);
        }

        /// <summary>
        /// Converts float to hex string
        /// </summary>
        public static string ToHexString(float value, Endianness endianness = Endianness.Mixed0123, bool withPrefix = false)
        {
            byte[] bytes = ToByteArray(value, endianness);
            return String.FromByteArray(bytes, "", withPrefix ? "0x" : "");
        }

        #endregion

        #region Custom Order

        /// <summary>
        /// Reorders bytes according to custom pattern string
        /// </summary>
        /// <param name="bytes">Source byte array (must have at least 4 bytes)</param>
        /// <param name="order">4-character order pattern (e.g. "3210", "1032", "2301", "0123")</param>
        /// <returns>Reordered byte array or empty array on error</returns>
        public static byte[] ReorderBytesWithPattern(byte[]? bytes, string order)
        {
            if (bytes == null || bytes.Length < SingleSize)
            {
                return Array.Empty<byte>();
            }

            if (string.IsNullOrEmpty(order) || order.Length != SingleSize)
            {
                return bytes ?? Array.Empty<byte>();
            }

            try
            {
                byte[] result = new byte[SingleSize];

                for (int i = 0; i < SingleSize; i++)
                {
                    if (int.TryParse(order[i].ToString(), out int sourceIndex) &&
                        sourceIndex >= 0 && sourceIndex < SingleSize)
                    {
                        result[i] = bytes[sourceIndex];
                    }
                    else
                    {
                        return bytes ?? Array.Empty<byte>();
                    }
                }

                return result;
            }
            catch
            {
                return bytes ?? Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts byte array to float using custom byte order pattern
        /// </summary>
        /// <param name="bytes">Byte array (must contain at least 4 bytes)</param>
        /// <param name="order">4-character order pattern (e.g. "3210", "1032")</param>
        /// <returns>Float value or 0 on error</returns>
        public static float FromByteArrayWithPattern(byte[]? bytes, string order)
        {
            if (bytes == null || bytes.Length < SingleSize)
            {
                return 0f;
            }

            try
            {
                byte[] reordered = ReorderBytesWithPattern(bytes, order);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(reordered);
                }

                return BitConverter.ToSingle(reordered, 0);
            }
            catch
            {
                return 0f;
            }
        }

        /// <summary>
        /// Converts byte array to float array using custom byte order pattern
        /// </summary>
        /// <param name="bytes">Byte array (length must be multiple of 4)</param>
        /// <param name="order">4-character order pattern (e.g. "3210", "1032")</param>
        /// <returns>Array of float values</returns>
        public static float[] ToArrayWithPattern(byte[]? bytes, string order)
        {
            if (bytes == null || bytes.Length < SingleSize)
            {
                return Array.Empty<float>();
            }

            try
            {
                int floatCount = bytes.Length / SingleSize;
                float[] values = new float[floatCount];

                for (int i = 0; i < floatCount; i++)
                {
                    byte[] floatBytes = new byte[SingleSize];
                    Array.Copy(bytes, i * SingleSize, floatBytes, 0, SingleSize);
                    values[i] = FromByteArrayWithPattern(floatBytes, order);
                }

                return values;
            }
            catch
            {
                return Array.Empty<float>();
            }
        }

        #endregion Custom Order

        #region Utility Methods

        /// <summary>
        /// Checks if byte array can be converted to float (length >= 4)
        /// </summary>
        /// <param name="bytes">Byte array to check</param>
        /// <returns>True if array can be converted to at least one float</returns>
        public static bool CanConvertToFloat(byte[]? bytes)
        {
            return bytes != null && bytes.Length >= SingleSize;
        }

        /// <summary>
        /// Gets number of floats in byte array
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>Number of complete floats in array</returns>
        public static int GetFloatCount(byte[]? bytes)
        {
            if (bytes == null)
            {
                return 0;
            }
            return bytes.Length / SingleSize;
        }

        /// <summary>
        /// Gets string representation of endianness pattern
        /// </summary>
        /// <param name="endianness">Endianness enum value</param>
        /// <returns>4-character pattern (e.g. "0123", "3210")</returns>
        public static string GetEndiannessPattern(Endianness endianness)
        {
            return endianness switch
            {
                Endianness.Mixed0123 => "0123",
                Endianness.Mixed3210 => "3210",
                Endianness.Mixed1032 => "1032",
                Endianness.Mixed2301 => "2301",
                _ => "0123"
            };
        }

        #endregion Utility Methods
    }
}
