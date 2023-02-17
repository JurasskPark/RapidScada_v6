using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;

namespace Scada.Comm.Drivers.DrvPing
{
    public class NetworkInformationExtensions
    {
        private static readonly byte[] buffer = new byte[16];
        private static readonly PingOptions options = new PingOptions(64, false);
        private static readonly TimeSpan pingTimeout = TimeSpan.FromMilliseconds(1000);
        private static readonly TimeSpan dnsTimeout = TimeSpan.FromMilliseconds(1000);

        public static bool Pinger(string hostAddress)
        {
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
                            return true;
                        }
                        else
                        {
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
                                return true;
                            }
                            else
                            {
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
