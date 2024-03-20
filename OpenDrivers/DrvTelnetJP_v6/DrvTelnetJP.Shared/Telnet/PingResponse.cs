using System.Net;
using System.Net.NetworkInformation;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    public class PingResponse
    {
        public string HostNameOrAddress { get; set; }
        public IPAddress IPAddress { get; set; }
        public IPStatus Status { get; set; }
        public bool IsSuccess { get; set; }
        public long RoundTripTime { get; set; }
        public bool IsResolved { get; set; }
    }
}
