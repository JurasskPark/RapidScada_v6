using System;
using System.Collections.Generic;
using System.Net;

namespace Scada.Comm.Drivers.DrvPingJP
{

    public class IPRangeGenerator
    {
        public static List<IPAddress> GenerateIPRange(string startIP, string endIP)
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

        private static long BytesToLong(byte[] bytes)
        {
            return (long)(bytes[0] << 24) | (long)(bytes[1] << 16) | (long)(bytes[2] << 8) | (long)bytes[3];
        }

        private static IPAddress LongToIPAddress(long ip)
        {
            return new IPAddress(new byte[] {
            (byte)(ip >> 24),
            (byte)(ip >> 16),
            (byte)(ip >> 8),
            (byte)ip
        });
        }
    }
}
