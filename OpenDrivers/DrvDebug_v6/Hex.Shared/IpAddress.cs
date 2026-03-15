using System;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with IP addresses in hex format.
    /// <para>Предоставляет методы для работы с IP-адресами в шестнадцатеричном формате.</para>
    /// </summary>
    public static class IpAddress
    {
        #region Constants

        private const int Ipv4BytesLength = 4;
        private const int Ipv6BytesLength = 16;

        #endregion Constants

        #region To ByteArray

        /// <summary>
        /// Converts IP address string to byte array
        /// </summary>
        /// <param name="ipAddress">IP address string (IPv4 or IPv6)</param>
        /// <returns>Byte array or empty array on error</returns>
        public static byte[] ToByteArray(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return Array.Empty<byte>();
            }

            try
            {
            // Try to parse the IP address.
                if (IPAddress.TryParse(ipAddress, out IPAddress? address))
                {
            // GetAddressBytes returns bytes in network order.
                    return address.GetAddressBytes();
                }

                return Array.Empty<byte>();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion To ByteArray

        #region From ByteArray

        /// <summary>
        /// Converts byte array to IP address string
        /// </summary>
        /// <param name="bytes">Byte array (4 bytes for IPv4, 16 bytes for IPv6)</param>
        /// <returns>IP address string or empty string on error</returns>
        public static string FromByteArray(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            try
            {
            // Check whether the length is valid for an IP address.
                if (bytes.Length != Ipv4BytesLength && bytes.Length != Ipv6BytesLength)
                {
                    return string.Empty;
                }

                IPAddress address = new IPAddress(bytes);
                return address.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion From ByteArray

        #region Hex Conversion

        /// <summary>
        /// Converts hex string to IP address
        /// </summary>
        /// <param name="hexString">Hex string (e.g. "C0A80101" for 192.168.1.1)</param>
        /// <returns>IP address string or empty string on error</returns>
        public static string FromHexString(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return string.Empty;
            }

            // Use String.ToByteArray to parse hex.
            byte[] bytes = String.ToByteArray(hexString);

            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            return FromByteArray(bytes);
        }

        /// <summary>
        /// Converts IP address to hex string
        /// </summary>
        /// <param name="ipAddress">IP address string</param>
        /// <param name="withPrefix">Add "0x" prefix</param>
        /// <returns>Hex string or empty string on error</returns>
        public static string ToHexString(string ipAddress, bool withPrefix = false)
        {
            byte[] bytes = ToByteArray(ipAddress);

            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            return String.FromByteArray(bytes, "", withPrefix ? "0x" : "");
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates if string is a valid IP address (IPv4 or IPv6)
        /// </summary>
        /// <param name="ipAddress">IP address string to validate</param>
        /// <returns>True if valid IP address</returns>
        public static bool IsValid(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        /// <summary>
        /// Validates if string is a valid IPv4 address
        /// </summary>
        /// <param name="ipAddress">IP address string to validate</param>
        /// <returns>True if valid IPv4 address</returns>
        public static bool IsValidIpv4(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            string[] octets = ipAddress.Split('.');
            if (octets.Length != Ipv4BytesLength)
            {
                return false;
            }

            foreach (string octet in octets)
            {
                if (!byte.TryParse(octet, out _))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates if string is a valid IPv6 address
        /// </summary>
        /// <param name="ipAddress">IP address string to validate</param>
        /// <returns>True if valid IPv6 address</returns>
        public static bool IsValidIpv6(string ipAddress)
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress? address))
            {
                return false;
            }

            return address.AddressFamily == AddressFamily.InterNetworkV6;
        }

        #endregion Validation

        #region Format Conversion

        /// <summary>
        /// Output format for IP address
        /// </summary>
        public enum OutputFormat
        {
            /// <summary>Regular dotted decimal (192.168.1.1)</summary>
            Decimal,
            /// <summary>Hex format (0xC0 0xA8 0x01 0x01)</summary>
            Hex,
            /// <summary>Binary format (11000000 10101000 00000001 00000001)</summary>
            Binary
        }

        /// <summary>
        /// Converts IP address to different format
        /// </summary>
        /// <param name="ipAddress">IP address string</param>
        /// <param name="format">Output format (Hex, Decimal, Binary)</param>
        /// <returns>Formatted string</returns>
        public static string ConvertFormat(string ipAddress, OutputFormat format)
        {
            byte[] bytes = ToByteArray(ipAddress);

            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            return format switch
            {
                OutputFormat.Hex => String.FromByteArray(bytes, " ", "0x"),
                OutputFormat.Decimal => string.Join(".", bytes),
                OutputFormat.Binary => string.Join(" ", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))),
                _ => ipAddress
            };
        }

        #endregion Format Conversion

        #region Endian Conversion

        /// <summary>
        /// Converts IP address bytes to network order (big endian)
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns>Bytes in network order</returns>
        public static byte[] ToNetworkOrder(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Array.Empty<byte>();
            }

            if (BitConverter.IsLittleEndian && bytes.Length == Ipv4BytesLength)
            {
            // For IPv4, network order is big-endian.
                byte[] networkOrder = new byte[bytes.Length];
                Array.Copy(bytes, networkOrder, bytes.Length);
                Array.Reverse(networkOrder);
                return networkOrder;
            }

            return bytes ?? Array.Empty<byte>();
        }

        /// <summary>
        /// Converts IP address bytes from network order to host order
        /// </summary>
        /// <param name="bytes">Bytes in network order</param>
        /// <returns>Bytes in host order</returns>
        public static byte[] FromNetworkOrder(byte[]? bytes)
        {
            // This is the same as ToNetworkOrder because the operation is reversible.
            return ToNetworkOrder(bytes);
        }

        #endregion Endian Conversion

        #region Utility Methods

        /// <summary>
        /// Gets address family of IP address (IPv4 or IPv6)
        /// </summary>
        /// <param name="ipAddress">IP address string</param>
        /// <returns>AddressFamily or null if invalid</returns>
        public static AddressFamily? GetAddressFamily(string ipAddress)
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress? address))
            {
                return null;
            }

            return address.AddressFamily;
        }

        /// <summary>
        /// Checks if IP address is IPv4
        /// </summary>
        public static bool IsIpv4(string ipAddress)
        {
            return GetAddressFamily(ipAddress) == AddressFamily.InterNetwork;
        }

        /// <summary>
        /// Checks if IP address is IPv6
        /// </summary>
        public static bool IsIpv6(string ipAddress)
        {
            return GetAddressFamily(ipAddress) == AddressFamily.InterNetworkV6;
        }

        #endregion Utility Methods
    }
}
