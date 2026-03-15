using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HexTools
{
    /// <summary>
    /// Provides operations for byte arrays.
    /// <para>Предоставляет операции для массивов байт.</para>
    /// </summary>
    public static class Operation
    {
        #region Combine

        /// <summary>
        /// Combines two byte arrays into one
        /// </summary>
        /// <param name="first">First byte array</param>
        /// <param name="second">Second byte array</param>
        /// <returns>Combined byte array or empty array on error</returns>
        public static byte[] Combine(byte[]? first, byte[]? second)
        {
            // Handle null cases.
            if (first == null && second == null)
            {
                return Array.Empty<byte>();
            }

            if (first == null)
            {
                return second ?? Array.Empty<byte>();
            }

            if (second == null)
            {
                return first;
            }

            try
            {
                byte[] result = new byte[first.Length + second.Length];
                Buffer.BlockCopy(first, 0, result, 0, first.Length);
                Buffer.BlockCopy(second, 0, result, first.Length, second.Length);

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Combines multiple byte arrays into one
        /// </summary>
        /// <param name="arrays">Array of byte arrays</param>
        /// <returns>Combined byte array</returns>
        public static byte[] Combine(params byte[]?[]? arrays)
        {
            if (arrays == null || arrays.Length == 0)
            {
                return Array.Empty<byte>();
            }

            try
            {
                // Calculate the total length.
                int totalLength = 0;
                foreach (var array in arrays)
                {
                    if (array != null)
                    {
                        totalLength += array.Length;
                    }
                }

                byte[] result = new byte[totalLength];
                int offset = 0;

                foreach (var array in arrays)
                {
                    if (array != null && array.Length > 0)
                    {
                        Buffer.BlockCopy(array, 0, result, offset, array.Length);
                        offset += array.Length;
                    }
                }

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Split

        /// <summary>
        /// Splits byte array by separator bytes
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="separator">Separator byte array</param>
        /// <param name="removeEmptyEntries">Remove empty entries from result</param>
        /// <returns>List of byte arrays between separators</returns>
        /// <summary>
        /// Splits the specified byte array.
        /// </summary>
        public static List<byte[]> Split(byte[]? bytes, byte[]? separator, bool removeEmptyEntries = true)
        {
            var result = new List<byte[]>();

            if (bytes == null || bytes.Length == 0 || separator == null || separator.Length == 0)
            {
                return result;
            }

            try
            {
                int separatorLength = separator.Length;
                var currentSegment = new List<byte>();

                for (int i = 0; i < bytes.Length; i++)
                {
                    // Check whether a separator was found at the current position.
                    if (i <= bytes.Length - separatorLength && IsMatch(bytes, i, separator))
                    {
                        // Add the current segment if it is not empty or empty entries are allowed.
                        if (currentSegment.Count > 0 || !removeEmptyEntries)
                        {
                            result.Add(currentSegment.ToArray());
                            currentSegment.Clear();
                        }
                        else
                        {
                            // Add an empty array if empty entries are allowed.
                            if (!removeEmptyEntries)
                            {
                                result.Add(Array.Empty<byte>());
                            }
                        }

                        // Skip separator bytes.
                        i += separatorLength - 1;
                    }
                    else
                    {
                        currentSegment.Add(bytes[i]);
                    }
                }

                // Add the last segment.
                if (currentSegment.Count > 0 || !removeEmptyEntries)
                {
                    result.Add(currentSegment.ToArray());
                }

                return result;
            }
            catch
            {
                return new List<byte[]>();
            }
        }

        /// <summary>
        /// Splits byte array by single byte separator
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="separator">Separator byte</param>
        /// <param name="removeEmptyEntries">Remove empty entries from result</param>
        /// <returns>List of byte arrays between separators</returns>
        /// <summary>
        /// Splits the specified byte array.
        /// </summary>
        public static List<byte[]> Split(byte[]? bytes, byte separator, bool removeEmptyEntries = true)
        {
            return Split(bytes, new[] { separator }, removeEmptyEntries);
        }

        /// <summary>
        /// Checks if separator matches at given position
        /// </summary>
        private static bool IsMatch(byte[] bytes, int startIndex, byte[] separator)
        {
            for (int i = 0; i < separator.Length; i++)
            {
                if (bytes[startIndex + i] != separator[i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Extract (formerly Search)

        /// <summary>
        /// Extracts a portion of byte array
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="offset">Starting offset</param>
        /// <param name="length">Number of bytes to extract</param>
        /// <returns>Extracted byte array or empty array on error</returns>
        /// <summary>
        /// Extracts a segment from the specified byte array.
        /// </summary>
        public static byte[] Extract(byte[]? bytes, int offset, int length)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Array.Empty<byte>();
            }

            if (offset < 0 || length < 0 || offset + length > bytes.Length)
            {
                return Array.Empty<byte>();
            }

            try
            {
                byte[] result = new byte[length];
                Buffer.BlockCopy(bytes, offset, result, 0, length);
                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Resize

        /// <summary>
        /// Resizes byte array to specified size
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="newSize">New size</param>
        /// <param name="offset">Offset to start copying from</param>
        /// <returns>Resized byte array or empty array on error</returns>
        /// <summary>
        /// Resizes the specified byte array.
        /// </summary>
        public static byte[] Resize(byte[]? bytes, int newSize, int offset = 0)
        {
            if (bytes == null || bytes.Length == 0 || newSize <= 0)
            {
                return Array.Empty<byte>();
            }

            if (offset < 0 || offset >= bytes.Length)
            {
                return Array.Empty<byte>();
            }

            try
            {
                int bytesToCopy = Math.Min(newSize, bytes.Length - offset);
                byte[] result = new byte[newSize];

                Buffer.BlockCopy(bytes, offset, result, 0, bytesToCopy);

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Compare

        /// <summary>
        /// Compares two byte arrays for equality
        /// </summary>
        /// <param name="first">First byte array</param>
        /// <param name="second">Second byte array</param>
        /// <returns>True if arrays are equal</returns>
        public static bool Compare(byte[]? first, byte[]? second)
        {
                // Both arrays are null or reference the same instance.
            if (ReferenceEquals(first, second))
            {
                return true;
            }

                // One array is null.
            if (first == null || second == null)
            {
                return false;
            }

                // Arrays have different lengths.
            if (first.Length != second.Length)
            {
                return false;
            }

            try
            {
                // Use fast comparison with long pointers when possible.
                if (first.Length >= sizeof(long))
                {
                    return MemoryMarshal.Cast<byte, long>(first)
                        .SequenceEqual(MemoryMarshal.Cast<byte, long>(second));
                }

                // Fall back to element-by-element comparison.
                for (int i = 0; i < first.Length; i++)
                {
                    if (first[i] != second[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Finds first occurrence of pattern in byte array
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="pattern">Pattern to find</param>
        /// <returns>Index of first occurrence or -1 if not found</returns>
        public static int IndexOf(byte[]? bytes, byte[]? pattern)
        {
            if (bytes == null || pattern == null || bytes.Length == 0 || pattern.Length == 0)
            {
                return -1;
            }

            if (pattern.Length > bytes.Length)
            {
                return -1;
            }

            try
            {
                for (int i = 0; i <= bytes.Length - pattern.Length; i++)
                {
                    if (IsMatch(bytes, i, pattern))
                    {
                        return i;
                    }
                }

                return -1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Finds last occurrence of pattern in byte array
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="pattern">Pattern to find</param>
        /// <returns>Index of last occurrence or -1 if not found</returns>
        public static int LastIndexOf(byte[]? bytes, byte[]? pattern)
        {
            if (bytes == null || pattern == null || bytes.Length == 0 || pattern.Length == 0)
            {
                return -1;
            }

            if (pattern.Length > bytes.Length)
            {
                return -1;
            }

            try
            {
                for (int i = bytes.Length - pattern.Length; i >= 0; i--)
                {
                    if (IsMatch(bytes, i, pattern))
                    {
                        return i;
                    }
                }

                return -1;
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region Replace

        /// <summary>
        /// Replaces all occurrences of pattern with replacement
        /// </summary>
        /// <param name="bytes">Source byte array</param>
        /// <param name="pattern">Pattern to find</param>
        /// <param name="replacement">Replacement bytes</param>
        /// <returns>New byte array with replacements</returns>
        /// <summary>
        /// Replaces a pattern in the specified byte array.
        /// </summary>
        public static byte[] Replace(byte[]? bytes, byte[]? pattern, byte[]? replacement)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Array.Empty<byte>();
            }

            if (pattern == null || pattern.Length == 0)
            {
                return bytes ?? Array.Empty<byte>();
            }

            replacement ??= Array.Empty<byte>();

            try
            {
                var result = new List<byte>();

                for (int i = 0; i < bytes.Length; i++)
                {
                    if (i <= bytes.Length - pattern.Length && IsMatch(bytes, i, pattern))
                    {
                    // Add the replacement bytes.
                        result.AddRange(replacement);

                    // Skip the matched pattern.
                        i += pattern.Length - 1;
                    }
                    else
                    {
                        result.Add(bytes[i]);
                    }
                }

                return result.ToArray();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Remove (formerly Regex method)

        /// <summary>
        /// Removes all occurrences of a byte from array
        /// </summary>
        public static byte[] Remove(byte[]? bytes, byte valueToRemove)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Array.Empty<byte>();
            }

            try
            {
                return bytes.Where(b => b != valueToRemove).ToArray();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion
    }
}
