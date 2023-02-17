using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Scada.Comm.Drivers.DrvPing
{
    public class DnsResponse
    {
        public string HostName { get; private set; }
        public IPAddress IPAddress { get; private set; }

        public DnsResponse(string hostName, IPAddress ip)
        {
            this.HostName = hostName;
            this.IPAddress = ip;
        }
    }
}
