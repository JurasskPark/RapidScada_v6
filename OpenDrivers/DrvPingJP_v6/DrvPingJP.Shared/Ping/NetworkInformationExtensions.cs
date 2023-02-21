using Scada.Lang;
using System.Net.NetworkInformation;

namespace Scada.Comm.Drivers.DrvPingJP
{
    public class NetworkInformationExtensions
    {
        private static readonly byte[] buffer = new byte[16];
        private static readonly PingOptions options = new PingOptions(64, false);
        private static readonly TimeSpan pingTimeout = TimeSpan.FromMilliseconds(1000);
        private static readonly TimeSpan dnsTimeout = TimeSpan.FromMilliseconds(1000);

        public static bool Pinger(string hostAddress, out string result)
        {
            result = string.Empty;
            string bufferLength = Locale.IsRussian ? "число байт" : "bytes";
            string rundtripTime = Locale.IsRussian ? "время" : "time";
            string rundtripTimeFormat = Locale.IsRussian ? "мс" : "ms";
            string noResponse = Locale.IsRussian ? "Заданный узел недоступен." : "The specified node is unavailable.";
            try
            {
                // if the IP has passed for validity, then we ping
                if (DriverUtils.IsIpAddress(hostAddress) == true)
                {
                    Ping pingSender = new Ping();
                    PingReply reply;

                    try
                    {
                        reply = pingSender.Send(hostAddress, (int)pingTimeout.Seconds, buffer, options);
                        if (reply.Status == IPStatus.Success)
                        {
                            result += "" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                            result += " " + reply.Address.ToString();
                            result += $@" {bufferLength}=" + reply.Buffer.Length.ToString();
                            result += $@" {rundtripTime}=" + reply.RoundtripTime.ToString() + $@"{rundtripTimeFormat}";
                            result += " TTL=" + reply.Options.Ttl.ToString();
                            return true;
                        }
                        else
                        {
                            result += "" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                            result += " " + hostAddress.ToString() + ":";
                            result += $@" {noResponse}";
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else // if the IP did not pass for validity, then we will try to get an ip address from DNS
                {
                    hostAddress = DnsResolver.ResolveHostName(hostAddress);
                    // if the IP is caught, we will ping
                    if (DriverUtils.IsIpAddress(hostAddress) == true)
                    {
                        Ping pingSender = new Ping();
                        PingReply reply;
                        try
                        {
                            reply = pingSender.Send(hostAddress, (int)pingTimeout.Seconds, buffer, options);
                            if (reply.Status == IPStatus.Success)
                            {
                                result += "" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                                result += " " + reply.Address.ToString();
                                result += $@" {bufferLength}=" + reply.Buffer.Length.ToString();
                                result += $@" {rundtripTime}=" + reply.RoundtripTime.ToString() + $@"{rundtripTimeFormat}";
                                result += " TTL=" + reply.Options.Ttl.ToString();
                                return true;
                            }
                            else
                            {
                                result += "" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                                result += " " + hostAddress.ToString() + ":";
                                result += $@" {noResponse}";
                                return false;
                            }
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
