using System;
using System.Collections.Generic;
using System.Net;

namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    public class IPAddressGenerator
    {
        /// <summary>
        /// Generating a list of IP addresses
        /// </summary>
        public static List<IPAddress> GenerateIPAddresses(string startIP, string endIP)
        {
            List<IPAddress> ipList = new List<IPAddress>();

            // Преобразуем строки в IPAddress
            IPAddress start = IPAddress.Parse(startIP);
            IPAddress end = IPAddress.Parse(endIP);

            // Получаем байты IP-адресов
            byte[] startBytes = start.GetAddressBytes();
            byte[] endBytes = end.GetAddressBytes();

            // Преобразуем байты в long
            long startLong = BytesToLong(startBytes);
            long endLong = BytesToLong(endBytes);

            // Генерируем IP-адреса
            for (long i = startLong; i <= endLong; i++)
            {
                ipList.Add(LongToIPAddress(i));
            }

            return ipList;
        }

        /// <summary>
        /// Converting a byte array to a long (IP address)
        /// </summary>
        private static long BytesToLong(byte[] bytes)
        {
            return (long)(bytes[0] << 24) | (long)(bytes[1] << 16) | (long)(bytes[2] << 8) | (long)bytes[3];
        }

        /// <summary>
        /// Converting a long to an IP address
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
        /// Generating a list of IP addresses
        /// </summary>
        public static List<IPAddress> GenerateIPAddresses(string startIP, int cidr)
        {
            // Парсим начальный IP-адрес
            IPAddress startIp = IPAddress.Parse(startIP);

            // Создаем маску подсети на основе CIDR
            byte[] subnetBytes = new byte[4];
            for (int i = 0; i < cidr / 8; i++)
            {
                subnetBytes[i] = 255;
            }
            if (cidr % 8 != 0)
            {
                subnetBytes[cidr / 8] = (byte)(256 - Math.Pow(2, 8 - cidr % 8));
            }

            // Получаем байты IP-адреса
            byte[] startIpBytes = startIp.GetAddressBytes();

            // Вычисляем сетевой адрес
            byte[] networkAddressBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                networkAddressBytes[i] = (byte)(startIpBytes[i] & subnetBytes[i]);
            }

            // Вычисляем широковещательный адрес
            byte[] broadcastAddressBytes = new byte[4];
            byte[] inverseSubnetBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                inverseSubnetBytes[i] = (byte)(~subnetBytes[i]);
                broadcastAddressBytes[i] = (byte)(networkAddressBytes[i] | inverseSubnetBytes[i]);
            }

            // Преобразуем байты в IP-адреса
            IPAddress networkAddress = new IPAddress(networkAddressBytes);
            IPAddress broadcastAddress = new IPAddress(broadcastAddressBytes);

            // Получаем все IP-адреса в диапазоне
            List<IPAddress> ipList = new List<IPAddress>();

            // Преобразуем IP-адреса в uint для удобства перебора
            uint network = BitConverter.ToUInt32(networkAddress.GetAddressBytes(), 0);
            uint broadcast = BitConverter.ToUInt32(broadcastAddress.GetAddressBytes(), 0);

            for (uint i = network; i <= broadcast; i++)
            {
                byte[] currentIpBytes = BitConverter.GetBytes(i);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(currentIpBytes);
                }

                IPAddress currentIp = new IPAddress(currentIpBytes);
                ipList.Add(currentIp);
            }

            return ipList;
        }

       

    }
}
