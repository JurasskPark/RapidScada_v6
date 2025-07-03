using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Scada.Comm.Drivers.DrvPingJP
{
    public class IPSubnetGenerator
    {
        public static List<IPAddress> GenerateSubnet(string startIP, string subnet)
        {
            List<IPAddress> ipList = new List<IPAddress>();


        
            //int lim = startIP.AddressFamily == AddressFamily.InterNetwork ? 32 : 64;
            //if (subnet < 1 || subnet > lim - 1)
            //    throw new ArgumentOutOfRangeException("subnet");

            //ulong end = Extract(start) | ((1UL << (lim - subnet)) - 1);
            //SetRange(start, Pack(start.AddressFamily, end));
        

            return ipList;
        }
    }
}
