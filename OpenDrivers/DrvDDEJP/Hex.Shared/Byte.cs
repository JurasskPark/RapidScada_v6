using System;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with Byte values in hex format.
    /// <para>Предоставляет методы для работы со значениями Byte в шестнадцатеричном формате.</para>
    /// </summary>
    public static class Byte
    {
        #region To ByteArray

        /// <summary>
        /// Converts a byte value to a byte array.
        /// </summary>
        public static byte[] ToByteArray(byte value) => new[] { value };

        /// <summary>
        /// Returns the specified byte array or an empty array.
        /// </summary>
        public static byte[] ToByteArray(byte[]? values) => values ?? Array.Empty<byte>();

        #endregion

        #region From ByteArray

        /// <summary>
        /// Converts a byte array to a byte value.
        /// </summary>
        public static byte FromByteArray(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return 0;
            }
            return bytes[0];
        }

        /// <summary>
        /// Returns the specified byte array or an empty array.
        /// </summary>
        public static byte[] ToArray(byte[]? bytes) => bytes ?? Array.Empty<byte>();

        #endregion

        #region Hex String Conversion

        /// <summary>
        /// Converts a hex string to a byte value.
        /// </summary>
        public static byte FromHexString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return 0;
            }

            try
            {
                byte[] bytes = String.ToByteArray(hexString);

                if (bytes.Length == 0)
                {
                    return 0;
                }

                return bytes[0];
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts a byte value to a hex string.
        /// </summary>
        public static string ToHexString(byte value, bool withPrefix = false)
        {
            return withPrefix ? $"0x{value:X2}" : $"{value:X2}";
        }

        #endregion

        #region Conversion Methods

        /// <summary>
        /// Converts a byte value to a signed byte value.
        /// </summary>
        public static sbyte ToSByte(byte value) => unchecked((sbyte)value);

        /// <summary>
        /// Converts a signed byte value to a byte value.
        /// </summary>
        public static byte FromSByte(sbyte value) => unchecked((byte)value);

        #endregion
    }
}
