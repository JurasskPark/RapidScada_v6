using System.Net;
using System.Net.Sockets;

namespace Scada.Comm.Drivers.DrvPingJP
{
    public class DnsResolver
    {
        public static string ResolveHostName(string hostNameOrAddress)
        {
            try
            {
                string localIP = "0.0.0.0";
                IPHostEntry IPHostNameEntry = Dns.GetHostEntry(hostNameOrAddress);
                foreach (IPAddress ip in IPHostNameEntry.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                    }
                }
                return localIP;
            }
            catch
            {
                return "N/A";
            }
        }
    }
}
