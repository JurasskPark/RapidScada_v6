using Scada.Lang;
using System.Net.NetworkInformation;

namespace Scada.Comm.Drivers.DrvPingJP
{
    internal class NetworkInformation
    {
        private static readonly byte[] buffer = new byte[16];
        private static readonly PingOptions options = new PingOptions(64, false);
        private static readonly TimeSpan pingTimeout = TimeSpan.FromMilliseconds(1000);
        private static readonly TimeSpan dnsTimeout = TimeSpan.FromMilliseconds(1000);
        private static List<string> StatusPing = new List<string>();
        private static List<Tag> listTag = new List<Tag>();
        private static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        static object lockObj = new object();

        static string bufferLength = Locale.IsRussian ? "число байт" : "bytes";
        static string rundtripTime = Locale.IsRussian ? "время" : "time";
        static string rundtripTimeFormat = Locale.IsRussian ? "мс" : "ms";
        static string noResponse = Locale.IsRussian ? "Заданный узел недоступен." : "The specified node is unavailable.";
        static string bad = Locale.IsRussian ? "Плохой!" : "Bad!";

        #region DebugerTags
        /// <summary>
        /// Getting tags
        /// <para>Получение тегов<para>
        /// </summary>
        public DebugTags OnDebugTags;
        public delegate void DebugTags(List<Tag> tags);
        internal void DebugerTags(List<Tag> tags)
        {
            if (OnDebugTags == null)
            {
                return;
            }

            OnDebugTags(tags);
        }

        public DebugTag OnDebugTag;
        public delegate void DebugTag(Tag tags);
        internal void DebugerTag(Tag tag)
        {
            if (OnDebugTag == null)
            {
                return;
            }

            OnDebugTag(tag);
        }
        #endregion DebugerTags

        #region DebugerLog
        /// <summary>
        /// Getting logs
        /// <para>Получение лога<para>
        /// </summary>
        public DebugData OnDebug;
        public delegate void DebugData(string msg);
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }
        #endregion DebugerLog

        public void RunPingSynchronous(List<Tag> tags)
        {
            try
            {
                var tasks = new List<Task>();

                for (int i = 0; i < tags.Count; i++)
                {
                    Tag tmpTag = tags[i];
                    if (tmpTag == null || tmpTag.TagEnabled == false)
                    {
                        continue;
                    }

                    #region Ping
                    if (tmpTag.TagEnabled == true) // enabled
                    {
                        try
                        {
                            CancellationToken token = cancelTokenSource.Token;

                            Task task = new Task(() =>
                            {
                                Pinger(ref tmpTag);
                                DebugerTag(tmpTag);

                                if (token.IsCancellationRequested)
                                {
                                    token.ThrowIfCancellationRequested(); // генерируем исключение
                                }

                            }, token);

                            try
                            {
                                task.Start();
                            }
                            catch { }

                        }
                        catch { }

                    }
                    #endregion Ping                
                }
            }
            catch
            { }
        }

        public void StopPingSynchronous()
        {
            try
            {
                Thread.Sleep(100);
                // after a time delay, we cancel the task
                cancelTokenSource.Cancel();

                // we are waiting for the completion of the task
                Thread.Sleep(100);
            }
            finally
            {
                cancelTokenSource.Dispose();
            }
        }

        private void Pinger(ref Tag tag)
        {
            string result = string.Empty;

            try
            {
                // if the IP has passed for validity, then we ping
                if (DriverUtils.IsIpAddress(tag.TagIPAddress) == true)
                {

                }
                else // if the IP did not pass for validity, then we will try to get an ip address from DNS
                {
                    tag.TagIPAddress = DnsResolver.ResolveHostName(tag.TagIPAddress);

                    if (DriverUtils.IsIpAddress(tag.TagIPAddress) == false)
                    {
                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                        result += $@" " + tag.TagIPAddress.ToString() + ":";
                        result += $@" {bad}";

                        tag.TagVal = 0;
                        tag.TagStat = 0;

                        DebugerLog(result);
                        DebugerTag(tag);
                    }
                }

                Ping pingSender = new Ping();
                PingReply reply;

                try
                {
                    reply = pingSender.Send(tag.TagIPAddress, tag.TagTimeout, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                        result += $@" " + reply.Address.ToString();
                        result += $@" {bufferLength}=" + reply.Buffer.Length.ToString();
                        result += $@" {rundtripTime}=" + reply.RoundtripTime.ToString() + $@"{rundtripTimeFormat}";
                        result += $@" TTL=" + reply.Options.Ttl.ToString();

                        tag.TagVal = 1;
                        tag.TagStat = 1;

                        DebugerLog(result);
                    }
                    else
                    {
                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                        result += $@" " + tag.TagIPAddress + ":";
                        result += $@" {noResponse}";

                        tag.TagVal = 0;
                        tag.TagStat = 1;

                        DebugerLog(result);
                    }
                }
                catch { }
            }
            catch { }
        }

        public void RunPingAsynchronous(List<Tag> tags)
        {
            PingerAsynchronous(tags);
        }

        public async void PingerAsynchronous(List<Tag> tags)
        {
            listTag.Clear();
            var tasks = new List<Task>();
            for (int i = 0; i < tags.Count; i++)
            {
                Ping pingSender = new Ping();
                var task = PingAndUpdateAsync(pingSender, tags[i]);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks).ContinueWith(t =>
            {
                DebugerTags(listTag);
            });
        }

        private async Task PingAndUpdateAsync(System.Net.NetworkInformation.Ping ping, Tag tag)
        {
            string result = string.Empty;

            try
            {
                // if the IP has passed for validity, then we ping
                if (DriverUtils.IsIpAddress(tag.TagIPAddress) == true)
                {

                }
                else
                {
                    tag.TagIPAddress = DnsResolver.ResolveHostName(tag.TagIPAddress);

                    if (DriverUtils.IsIpAddress(tag.TagIPAddress) == false)
                    {
                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                        result += $@" " + tag.TagIPAddress.ToString() + ":";
                        result += $@" {bad}";

                        tag.TagVal = 0;
                        tag.TagStat = 0;

                        DebugerLog(result);
                    }
                }

                Ping pingSender = new Ping();
                PingReply reply;

                try
                {
                    reply = await ping.SendPingAsync(tag.TagIPAddress, tag.TagTimeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                        result += $@" " + reply.Address.ToString();
                        result += $@" {bufferLength}=" + reply.Buffer.Length.ToString();
                        result += $@" {rundtripTime}=" + reply.RoundtripTime.ToString() + $@"{rundtripTimeFormat}";
                        result += " TTL=" + reply.Options.Ttl.ToString();

                        tag.TagVal = 1;
                        tag.TagStat = 1;

                        DebugerLog(result);
                    }
                    else
                    {
                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                        result += $@" " + tag.TagIPAddress.ToString() + ":";
                        result += $@" {noResponse}";

                        tag.TagVal = 0;
                        tag.TagStat = 1;

                        DebugerLog(result);
                    }

                    lock (lockObj)
                    {
                        listTag.Add(tag);
                    }
                }
                catch { }

            }
            catch { }
        }
    }
}
