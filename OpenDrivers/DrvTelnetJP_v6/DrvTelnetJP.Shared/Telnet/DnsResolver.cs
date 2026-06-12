using System.Net;
using System.Net.Sockets;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    /// <summary>
    /// Resolves host names to IPv4 addresses.
    /// <para>Преобразует имена узлов в IPv4-адреса.</para>
    /// </summary>
    public static class DnsResolver
    {
        #region Basic

        /// <summary>
        /// Resolves a host name or returns an empty string if resolving fails.
        /// <para>Преобразует имя узла или возвращает пустую строку при ошибке.</para>
        /// </summary>
        public static string ResolveHostName(string hostNameOrAddress)
        {
            if (string.IsNullOrWhiteSpace(hostNameOrAddress))
            {
                return string.Empty;
            }

            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(hostNameOrAddress);
                foreach (IPAddress ipAddress in hostEntry.AddressList)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipAddress.ToString();
                    }
                }
            }
            catch
            {
            }

            return string.Empty;
        }

        #endregion Basic
    }
}
