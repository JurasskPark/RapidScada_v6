using System;
using System.Collections.Generic;
using System.Net;

namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Generates IP address ranges.
    /// </summary>
    public class IPAddressGenerator
    {
        /// <summary>
        /// Generates a list of IP addresses.
        /// </summary>
        public static List<IPAddress> GenerateIPAddresses(string startIP, string endIP)
        {
            List<IPAddress> ipList = new List<IPAddress>();

            // convert strings to IPAddress.
            IPAddress start = IPAddress.Parse(startIP);
            IPAddress end = IPAddress.Parse(endIP);

            // get the IP address bytes.
            byte[] startBytes = start.GetAddressBytes();
            byte[] endBytes = end.GetAddressBytes();

            // convert bytes to long values.
            long startLong = BytesToLong(startBytes);
            long endLong = BytesToLong(endBytes);

            // generate IP addresses.
            for (long i = startLong; i <= endLong; i++)
            {
                ipList.Add(LongToIPAddress(i));
            }

            return ipList;
        }

        /// <summary>
        /// Converts a byte array to a long IP address value.
        /// </summary>
        private static long BytesToLong(byte[] bytes)
        {
            return (long)(bytes[0] << 24) | (long)(bytes[1] << 16) | (long)(bytes[2] << 8) | (long)bytes[3];
        }

        /// <summary>
        /// Converts a long value to an IP address.
        /// </summary>
        private static IPAddress LongToIPAddress(long ip)
        {
            return new IPAddress(new byte[] {
            (byte)(ip >> 24),
            (byte)(ip >> 16),
            (byte)(ip >> 8),
            (byte)ip
        });
        }

        /// <summary>
        /// Generates a list of IP addresses.
        /// </summary>
        public static List<IPAddress> GenerateIPAddresses(string startIP, int cidr)
        {
            if (cidr < 0 || cidr > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(cidr));
            }

            // parse the starting IP address.
            IPAddress startIp = IPAddress.Parse(startIP);

            // create the subnet mask based on CIDR.
            byte[] subnetBytes = new byte[4];
            for (int i = 0; i < cidr / 8; i++)
            {
                subnetBytes[i] = 255;
            }
            if (cidr % 8 != 0)
            {
                subnetBytes[cidr / 8] = (byte)(256 - Math.Pow(2, 8 - cidr % 8));
            }

            // get the IP address bytes.
            byte[] startIpBytes = startIp.GetAddressBytes();

            // calculate the network address.
            byte[] networkAddressBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                networkAddressBytes[i] = (byte)(startIpBytes[i] & subnetBytes[i]);
            }

            // calculate the broadcast address.
            byte[] broadcastAddressBytes = new byte[4];
            byte[] inverseSubnetBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                inverseSubnetBytes[i] = (byte)(~subnetBytes[i]);
                broadcastAddressBytes[i] = (byte)(networkAddressBytes[i] | inverseSubnetBytes[i]);
            }

            // convert byte arrays to IP addresses.
            IPAddress networkAddress = new IPAddress(networkAddressBytes);
            IPAddress broadcastAddress = new IPAddress(broadcastAddressBytes);

            // get all IP addresses in the range.
            List<IPAddress> ipList = new List<IPAddress>();

            // convert IP addresses to uint for iteration.
            uint network = BytesToUInt32(networkAddress.GetAddressBytes());
            uint broadcast = BytesToUInt32(broadcastAddress.GetAddressBytes());

            for (uint i = network; i <= broadcast; i++)
            {
                IPAddress currentIp = LongToIPAddress(i);
                ipList.Add(currentIp);
            }

            return ipList;
        }

        /// <summary>
        /// Converts a byte array to an unsigned integer.
        /// </summary>
        private static uint BytesToUInt32(byte[] bytes)
        {
            return ((uint)bytes[0] << 24) |
                   ((uint)bytes[1] << 16) |
                   ((uint)bytes[2] << 8) |
                   bytes[3];
        }

        /// <summary>
        /// Converts an unsigned integer to an IP address.
        /// </summary>
        private static IPAddress LongToIPAddress(uint ip)
        {
            return new IPAddress(new byte[]
            {
                (byte)(ip >> 24),
                (byte)(ip >> 16),
                (byte)(ip >> 8),
                (byte)ip
            });
        }

    }
}
