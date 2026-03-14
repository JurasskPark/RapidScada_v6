using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Timer = System.Threading.Timer;

namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements network information processing.
    /// <para>Реализует сетевую обработку информации.</para>
    /// </summary>
    internal class NetworkInformation : IDisposable
    {
        #region Variables
        Debuger driverLog = new Debuger();
        DriverTagReturn driverTagReturn = new DriverTagReturn();
        private Timer timer;
        private TimerCallback timerCallback;
        private bool timerIsRunning;
        private int interval = 1000;
        private byte[] buffer = new byte[16];
        private PingOptions options = new PingOptions(64, false);
        private TimeSpan pingTimeout = TimeSpan.FromMilliseconds(1000);
        private TimeSpan dnsTimeout = TimeSpan.FromMilliseconds(1000);
        private List<string> statusPing = new List<string>();
        private int mode = 0;
        private List<DriverTag> listTag = new List<DriverTag>();
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private object lockObj = new object();

        public event Action<string> OnDebug;
        public event Action<DriverTag> OnDebugTag;
        public event Action<List<DriverTag>> OnDebugTags;
        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public NetworkInformation()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public NetworkInformation(int modePing, List<DriverTag> tags)
        {
            mode = modePing;
            listTag = tags;
            Initialize();
        }

        /// <summary>
        /// Initializes internal components.
        /// </summary>
        private void Initialize()
        {
            driverLog = new Debuger();
            driverTagReturn = new DriverTagReturn();

            timerIsRunning = false;
            interval = 1000;
            timerCallback = new TimerCallback(TimerCallbackMethod);
            timer = new Timer(timerCallback, null, 0, interval);
        }

        /// <summary>
        /// Handles timer events.
        /// </summary>
        private void TimerCallbackMethod(object state)
        {
            if (timerIsRunning)
            {
                try
                {
                    if (mode == 0)
                    {
                        PingSynchronous();
                    }
                    else if (mode == 1)
                    {
                        PingAsynchronous();
                    }
                }
                catch (Exception ex)
                {
                    Debuger.Log($"Ошибка в таймере: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Performs synchronous ping.
        /// </summary>
        public void PingSynchronous()
        {
            var tasks = new List<Task>();

            foreach (var tag in listTag)
            {
                if (tag == null || !tag.Enabled)
                {
                    continue;
                }

                try
                {
                    var token = cancelTokenSource.Token;
                    var task = Task.Run(() => PingSynchronous(tag, token));
                    tasks.Add(task);
                }
                catch (Exception ex)
                {
                    Debuger.Log($"Ошибка при создании задачи: {ex.Message}");
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Performs synchronous ping for a tag.
        /// </summary>
        private void PingSynchronous(DriverTag tag, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            string result = string.Empty;

            try
            {
                // improved IP validation.
                if (!DriverUtils.IsIpAddress(tag.IpAddress))
                {
                    tag.IpAddress = DnsResolver.ResolveHostName(tag.IpAddress);
                    if (!DriverUtils.IsIpAddress(tag.IpAddress))
                    {
                        LogError(tag, "Неверный IP");
                        return;
                    }
                }

                using (var pingSender = new Ping())
                {
                    var reply = pingSender.Send(tag.IpAddress, (int)pingTimeout.TotalMilliseconds, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        LogSuccess(tag, reply);
                    }
                    else
                    {

                        // continue processing the synchronous ping result.

                        LogError(tag, "Заданный узел недоступен");
                    }
                }
            }
            catch (PingException ex)
            {
                Debuger.Log($"Ошибка пинга: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debuger.Log($"Общая ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Writes successful ping information.
        /// </summary>
        private void LogSuccess(DriverTag tag, PingReply reply)
        {
            string result = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fffff} " +
                           $"{reply.Address}: " +
                           $"bytes={reply.Buffer.Length} " +
                           $"time={reply.RoundtripTime} мс " +
                           $"TTL={reply.Options.Ttl}";

            tag.Val = 1;
            tag.Stat = 1;

            Debuger.Log(result);
            driverTagReturn.Return(tag);
        }

        /// <summary>
        /// Writes ping error information.
        /// </summary>
        private void LogError(DriverTag tag, string message)
        {
            string result = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fffff} " +
                           $"{tag.IpAddress}: " +
                           $"{message}";

            tag.Val = 0;
            tag.Stat = 0;

            Debuger.Log(result);
            driverTagReturn.Return(tag);
        }

        /// <summary>
        /// Performs asynchronous ping.
        /// </summary>
        public async Task PingAsynchronous()
        {
            var tasks = new List<Task>();

            foreach (var tag in listTag)
            {
                if (tag == null || !tag.Enabled)
                {
                    continue;
                }

                tasks.Add(PingAndUpdateAsync(tag));
            }

            await Task.WhenAll(tasks);
            driverTagReturn.Return(listTag);
        }

        /// <summary>
        /// Performs asynchronous ping for a tag.
        /// </summary>
        private async Task PingAndUpdateAsync(DriverTag tag)
        {
            try
            {
                if (!DriverUtils.IsIpAddress(tag.IpAddress))
                {
                    tag.IpAddress = DnsResolver.ResolveHostName(tag.IpAddress);
                    if (!DriverUtils.IsIpAddress(tag.IpAddress))
                    {
                        LogError(tag, "Неверный IP");
                        return;
                    }
                }

                using (var ping = new Ping())
                {
                    PingReply reply;
                    try
                    {
                        reply = await ping.SendPingAsync(
                            tag.IpAddress,
                            (int)pingTimeout.TotalMilliseconds,
                            buffer,
                            options);
                    }
                    catch (PingException ex)
                    {
                        Debuger.Log($"Ошибка пинга: {ex.Message}");
                        return;
                    }

                    if (reply.Status == IPStatus.Success)
                    {
                        LogSuccess(tag, reply);
                    }
                    else
                    {
                        LogError(tag, "Заданный узел недоступен");
                    }
                }
            }
            catch (Exception ex)
            {
                Debuger.Log($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Starts ping processing.
        /// </summary>
        public void PingStart()
        {
            if (!timerIsRunning)
            {
                timerIsRunning = true;
                timer = new Timer(timerCallback, null, 0, interval);
            }
        }

        /// <summary>
        /// Stops ping processing.
        /// </summary>
        public void PingStop()
        {
            if (timerIsRunning)
            {
                timerIsRunning = false;
                timer?.Dispose();
                timer = null;
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
            }
        }

        #region Dispose
        private IntPtr bufferPtr;
        private bool disposed = false;

        /// <summary>
        /// Releases resources used by the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases managed and unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                PingStop();
                Marshal.FreeHGlobal(bufferPtr);
            }

            disposed = true;
        }

        /// <summary>
        /// Dispose NetworkInformation.
        /// </summary>
        ~NetworkInformation()
        {
            Dispose(false);
        }
        #endregion Dispose
    }
}
