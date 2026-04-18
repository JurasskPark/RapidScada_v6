using System;

namespace HexTools
{
    /// <summary>
    /// Provides helper methods for BCD conversion.
    /// <para>Предоставляет вспомогательные методы для преобразования BCD.</para>
    /// BCD uses 4 bits per decimal digit (0-9).
    /// </summary>
    public static class BcdConverter
    {
        #region Byte BCD (2 digits)

        /// <summary>
        /// Converts BCD byte to integer (e.g. 0x12 -> 12)
        /// </summary>
        public static int BcdToByte(byte bcdValue)
        {
            // 0x12 = (1 << 4) + 2 = 18 in binary, but it represents 12 in BCD.
            return (bcdValue >> 4) * 10 + (bcdValue & 0x0F);
        }

        /// <summary>
        /// Converts integer (0-99) to BCD byte (e.g. 12 -> 0x12)
        /// </summary>
        public static byte ByteToBcd(int value)
        {
            if (value < 0 || value > 99)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0-99 for BCD byte conversion");
            }
            return (byte)(((value / 10) << 4) | (value % 10));
        }

        #endregion

        #region UShort BCD (4 digits)

        /// <summary>
        /// Converts BCD ushort to integer (e.g. 0x1234 -> 1234)
        /// </summary>
        public static int BcdToUShort(ushort bcdValue)
        {
            int result = 0;
            int multiplier = 1;

            for (int i = 0; i < 4; i++)
            {
                int digit = (bcdValue >> (i * 4)) & 0x0F;
                if (digit > 9) throw new InvalidOperationException("Invalid BCD digit");
                result += digit * multiplier;
                multiplier *= 10;
            }

            return result;
        }

        /// <summary>
        /// Converts integer (0-9999) to BCD ushort (e.g. 1234 -> 0x1234)
        /// </summary>
        public static ushort UShortToBcd(int value)
        {
            if (value < 0 || value > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0-9999 for BCD ushort conversion");
            }

            ushort result = 0;
            for (int i = 0; i < 4; i++)
            {
                int digit = value % 10;
                result |= (ushort)(digit << (i * 4));
                value /= 10;
            }

            return result;
        }

        #endregion

        #region UInt BCD (8 digits)

        /// <summary>
        /// Converts BCD uint to integer (e.g. 0x12345678 -> 12345678)
        /// </summary>
        public static int BcdToUInt(uint bcdValue) // Returns Int32 because the supported range fits into Int32.
        {
            long result = 0;
            long multiplier = 1;

            for (int i = 0; i < 8; i++)
            {
                int digit = (int)((bcdValue >> (i * 4)) & 0x0F);
                if (digit > 9) throw new InvalidOperationException("Invalid BCD digit");
                result += digit * multiplier;
                multiplier *= 10;
            }

            return (int)result;
        }

        /// <summary>
        /// Converts integer (0-99999999) to BCD uint (e.g. 12345678 -> 0x12345678)
        /// </summary>
        public static uint UIntToBcd(int value)
        {
            if (value < 0 || value > 99999999)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0-99999999 for BCD uint conversion");
            }

            uint result = 0;
            int temp = value;
            for (int i = 0; i < 8; i++)
            {
                int digit = temp % 10;
                result |= (uint)(digit << (i * 4));
                temp /= 10;
            }

            return result;
        }

        #endregion

        #region Array Conversions

        /// <summary>
        /// Converts BCD byte array to integer array
        /// </summary>
        public static int[] BcdToBytes(byte[]? bcdBytes)
        {
            if (bcdBytes == null)
            {
                return Array.Empty<int>();
            }

            int[] result = new int[bcdBytes.Length];
            for (int i = 0; i < bcdBytes.Length; i++)
            {
                result[i] = BcdToByte(bcdBytes[i]);
            }
            return result;
        }

        /// <summary>
        /// Converts integer array to BCD byte array
        /// </summary>
        public static byte[] BytesToBcd(int[]? values)
        {
            if (values == null)
            {
                return Array.Empty<byte>();
            }

            byte[] result = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ByteToBcd(values[i]);
            }
            return result;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Checks if byte is valid BCD (both nibbles 0-9)
        /// </summary>
        public static bool IsValidBcdByte(byte value)
        {
            return ((value >> 4) <= 9) && ((value & 0x0F) <= 9);
        }

        /// <summary>
        /// Checks if ushort is valid BCD (all digits 0-9)
        /// </summary>
        public static bool IsValidBcdUShort(ushort value)
        {
            for (int i = 0; i < 4; i++)
            {
                if (((value >> (i * 4)) & 0x0F) > 9)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
