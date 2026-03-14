using System.Net;
using System.Net.Sockets;

namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Resolves DNS names.
    /// <para>Представление разрешения DNS-имен.</para>
    /// </summary>
    public class DnsResolver
    {
        /// <summary>
        /// Gets the host address from the DNS server.
        /// </summary>
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
