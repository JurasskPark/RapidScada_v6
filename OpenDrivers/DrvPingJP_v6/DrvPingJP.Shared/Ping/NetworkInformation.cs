//using Scada.Lang;
//using System.Net.NetworkInformation;
//using System.Runtime.InteropServices;
//using System.Threading;
//using System.Timers;
//using Timer = System.Threading.Timer;

//namespace Scada.Comm.Drivers.DrvPingJP
//{
//    internal class NetworkInformation
//    {
//        public NetworkInformation() 
//        {
//            mode = 0;
//            listTag = new List<Tag>();

//            timerIsRunning = false;
//            interval = 1000;
//            timerCallback = new TimerCallback(TimerCallbackMethod);
//            timer = new Timer(timerCallback, null, 0, interval);
//        }

//        public NetworkInformation(int modePing, List<Tag> tags)
//        {
//            mode = modePing;
//            listTag = tags;

//            timerIsRunning = false;
//            interval = 1000;
//            timerCallback = new TimerCallback(TimerCallbackMethod);
//            timer = new Timer(timerCallback, null, 0, interval);
//        }

//        #region Dispose
//        private IntPtr _bufferPtr;
//        public int BUFFER_SIZE = 1024 * 1024 * 50; // 50 MB
//        private bool _disposed = false;

//        ~NetworkInformation()
//        {
//            Dispose(false);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (_disposed)
//            {
//                return;
//            }

//            if (disposing)
//            {
//                // free any other managed objects here.
//            }

//            // free any unmanaged objects here.
//            Marshal.FreeHGlobal(_bufferPtr);
//            _disposed = true;
//        }

//        #endregion Dispose

//        #region Variables
//        private Timer timer;
//        private readonly TimerCallback timerCallback;
//        private bool timerIsRunning;
//        private int interval = 1000;
//        private static readonly byte[] buffer = new byte[16];
//        private static readonly PingOptions options = new PingOptions(64, false);
//        private static readonly TimeSpan pingTimeout = TimeSpan.FromMilliseconds(1000);
//        private static readonly TimeSpan dnsTimeout = TimeSpan.FromMilliseconds(1000);
//        private static List<string> StatusPing = new List<string>();
//        private static int mode = 0;
//        private static List<Tag> listTag = new List<Tag>();
//        private static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
//        private static object lockObj = new object();

//        private static string bufferLength = Locale.IsRussian ? "число байт" : "bytes";
//        private static string rundtripTime = Locale.IsRussian ? "время" : "time";
//        private static string rundtripTimeFormat = Locale.IsRussian ? "мс" : "ms";
//        private static string noResponse = Locale.IsRussian ? "Заданный узел недоступен." : "The specified node is unavailable.";
//        private static string bad = Locale.IsRussian ? "Плохой!" : "Bad!";
//        #endregion Variables

//        #region DebugerLog
//        /// <summary>
//        /// Getting logs
//        /// <para>Получение лога<para>
//        /// </summary>
//        public DebugData OnDebug;
//        public delegate void DebugData(string msg);
//        internal void DebugerLog(string text)
//        {
//            if (OnDebug == null)
//            {
//                return;
//            }

//            OnDebug(text);
//        }
//        #endregion DebugerLog

//        #region DebugerTag
//        /// <summary>
//        /// Getting tag
//        /// <para>Получение тего<para>
//        /// </summary>
//        public DebugTag OnDebugTag;
//        public delegate void DebugTag(Tag tags);
//        internal void DebugerTag(Tag tag)
//        {
//            if (OnDebugTag == null)
//            {
//                return;
//            }

//            OnDebugTag(tag);
//        }
//        #endregion DebugerTag

//        #region DebugerTags
//        /// <summary>
//        /// Getting tags
//        /// <para>Получение тегов<para>
//        /// </summary>
//        public DebugTags OnDebugTags;
//        public delegate void DebugTags(List<Tag> tags);
//        internal void DebugerTags(List<Tag> tags)
//        {
//            if (OnDebugTags == null)
//            {
//                return;
//            }

//            OnDebugTags(tags);
//        }

//        #endregion DebugerTags

//        #region Timer
//        private void TimerCallbackMethod(object state)
//        {
//            if (timerIsRunning)
//            {
//                if (mode == 0)
//                {
//                    PingSynchronous();
//                }
//                else if (mode == 1)
//                {
//                    PingAsynchronous();
//                }
//            }
//        }

//        #endregion Timer

//        #region PingSynchronous
//        public void PingSynchronousStart()
//        {
//            if (!timerIsRunning)
//            {
//                timerIsRunning = true;
//                timer = new Timer(timerCallback, null, 0, interval);
//            }
//        }

//        public void PingSynchronousStop()
//        {
//            try
//            {
//                if (timerIsRunning)
//                {
//                    timerIsRunning = false;
//                    timer?.Dispose();
//                    timer = null;
//                }

//                Thread.Sleep(100);
//                // after a time delay, we cancel the task
//                cancelTokenSource.Cancel();

//                // we are waiting for the completion of the task
//                Thread.Sleep(100);
//            }
//            finally
//            {
//                cancelTokenSource.Dispose();
//            }
//        }

//        public void PingSynchronous()
//        {
//            try
//            {
//                var tasks = new List<Task>();

//                for (int i = 0; i < listTag.Count; i++)
//                {
//                    Tag tmpTag = listTag[i];
//                    if (tmpTag == null || tmpTag.Enabled == false)
//                    {
//                        continue;
//                    }

//                    #region Ping
//                    if (tmpTag.Enabled == true) // enabled
//                    {
//                        try
//                        {
//                            CancellationToken token = cancelTokenSource.Token;

//                            Task task = new Task(() =>
//                            {
//                                PingSynchronous(tmpTag);

//                                if (token.IsCancellationRequested)
//                                {
//                                    token.ThrowIfCancellationRequested(); // генерируем исключение
//                                }

//                            }, token);

//                            try
//                            {
//                                task.Start();
//                            }
//                            catch { }

//                        }
//                        catch { }

//                    }
//                    #endregion Ping                
//                }
//            }
//            catch { }
//        }

//        private void PingSynchronous(Tag tag)
//        {
//            string result = string.Empty;

//            try
//            {
//                // if the IP has passed for validity, then we ping
//                if (DriverUtils.IsIpAddress(tag.IpAddress) == false)
//                { 
//                    tag.IpAddress = DnsResolver.ResolveHostName(tag.IpAddress);

//                    if (DriverUtils.IsIpAddress(tag.IpAddress) == false)
//                    {
//                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
//                        result += $@" " + tag.IpAddress.ToString() + ":";
//                        result += $@" {bad}";

//                        tag.Val = 0;
//                        tag.Stat = 0;

//                        DebugerLog(result);
//                        DebugerTag(tag);
//                    }
//                }

//                Ping pingSender = new Ping();
//                PingReply reply;

//                try
//                {
//                    reply = pingSender.Send(tag.IpAddress, tag.Timeout, buffer, options);

//                    if (reply.Status == IPStatus.Success)
//                    {
//                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
//                        result += $@" " + reply.Address.ToString();
//                        result += $@" {bufferLength}=" + reply.Buffer.Length.ToString();
//                        result += $@" {rundtripTime}=" + reply.RoundtripTime.ToString() + $@"{rundtripTimeFormat}";
//                        result += $@" TTL=" + reply.Options.Ttl.ToString();

//                        tag.Val = 1;
//                        tag.Stat = 1;

//                        DebugerLog(result);
//                        DebugerTag(tag);
//                    }
//                    else
//                    {
//                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
//                        result += $@" " + tag.IpAddress + ":";
//                        result += $@" {noResponse}";

//                        tag.Val = 0;
//                        tag.Stat = 1;

//                        DebugerLog(result);
//                        DebugerTag(tag);
//                    }
//                }
//                catch { }
//            }
//            catch { }
//        }

//        #endregion PingSynchronous

//        #region PingAsynchronous
//        public void PingAsynchronousStart()
//        {
//            if (!timerIsRunning)
//            {
//                timerIsRunning = true;
//                timer = new Timer(timerCallback, null, 0, interval);
//            }
//        }

//        public void PingAsynchronousStop()
//        {
//            try
//            {
//                if (timerIsRunning)
//                {
//                    timerIsRunning = false;
//                    timer?.Dispose();
//                    timer = null;
//                }
//            }
//            catch { }
//        }

//        public async void PingAsynchronous()
//        {
//            var tasks = new List<Task>();
//            for (int i = 0; i < listTag.Count; i++)
//            {
//                Ping pingSender = new Ping();
//                var task = PingAndUpdateAsync(pingSender, listTag[i]);
//                tasks.Add(task);
//            }

//            await Task.WhenAll(tasks).ContinueWith(t =>
//            {
//                DebugerTags(listTag);
//            });
//        }

//        private async Task PingAndUpdateAsync(System.Net.NetworkInformation.Ping ping, Tag tag)
//        {
//            string result = string.Empty;

//            try
//            {
//                // if the IP has passed for validity, then we ping
//                if (DriverUtils.IsIpAddress(tag.IpAddress) == false)
//                { 
//                    tag.IpAddress = DnsResolver.ResolveHostName(tag.IpAddress);

//                    if (DriverUtils.IsIpAddress(tag.IpAddress) == false)
//                    {
//                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
//                        result += $@" " + tag.IpAddress.ToString() + ":";
//                        result += $@" {bad}";

//                        tag.Val = 0;
//                        tag.Stat = 0;

//                        DebugerLog(result);
//                    }
//                }

//                Ping pingSender = new Ping();
//                PingReply reply;

//                try
//                {
//                    reply = await ping.SendPingAsync(tag.IpAddress, tag.Timeout, buffer, options);
//                    if (reply.Status == IPStatus.Success)
//                    {
//                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
//                        result += $@" " + reply.Address.ToString();
//                        result += $@" {bufferLength}=" + reply.Buffer.Length.ToString();
//                        result += $@" {rundtripTime}=" + reply.RoundtripTime.ToString() + $@"{rundtripTimeFormat}";
//                        result += " TTL=" + reply.Options.Ttl.ToString();

//                        tag.Val = 1;
//                        tag.Stat = 1;

//                        DebugerLog(result);
//                    }
//                    else
//                    {
//                        result += $@"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff");
//                        result += $@" " + tag.IpAddress.ToString() + ":";
//                        result += $@" {noResponse}";

//                        tag.Val = 0;
//                        tag.Stat = 1;

//                        DebugerLog(result);
//                    }


//                     listTag.Add(tag);

//                }
//                catch { }

//            }
//            catch { }
//        }

//        #endregion PingAsynchronous
//    }
//}

using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Timer = System.Threading.Timer;

namespace Scada.Comm.Drivers.DrvPingJP
{
    internal class NetworkInformation : IDisposable
    {
        // Исправлено: убраны лишние статические модификаторы
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

        // Исправлено: добавлены конструкторы с инициализацией
        public NetworkInformation()
        {
            Initialize();
        }

        public NetworkInformation(int modePing, List<DriverTag> tags)
        {
            mode = modePing;
            listTag = tags;
            Initialize();
        }

        private void Initialize()
        {
            driverLog = new Debuger();
            driverTagReturn = new DriverTagReturn();

            timerIsRunning = false;
            interval = 1000;
            timerCallback = new TimerCallback(TimerCallbackMethod);
            timer = new Timer(timerCallback, null, 0, interval);
        }

        // Исправлено: добавлены типы для делегатов
        public event Action<string> OnDebug;
        public event Action<DriverTag> OnDebugTag;
        public event Action<List<DriverTag>> OnDebugTags;

        // Исправлено: улучшена обработка исключений
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

        // Исправлено: улучшена обработка задач
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

        private void PingSynchronous(DriverTag tag, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            string result = string.Empty;

            try
            {
                // Исправлено: улучшена валидация IP
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

                        // Продолжение метода PingSynchronous

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

        // Асинхронный пинг
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

        // Методы управления таймером
        public void PingStart()
        {
            if (!timerIsRunning)
            {
                timerIsRunning = true;
                timer = new Timer(timerCallback, null, 0, interval);
            }
        }

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
        public int BUFFER_SIZE = 1024 * 1024 * 50;
        private bool disposed = false;

        // Реализация IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        ~NetworkInformation()
        {
            Dispose(false);
        }
        #endregion Dispose
    }
}