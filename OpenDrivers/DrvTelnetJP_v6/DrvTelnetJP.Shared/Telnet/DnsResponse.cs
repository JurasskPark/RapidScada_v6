using System.Net;

namespace Scada.Comm.Drivers.DrvTelnetJP
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
