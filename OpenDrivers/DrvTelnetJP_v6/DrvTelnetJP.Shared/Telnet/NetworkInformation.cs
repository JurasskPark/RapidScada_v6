using Scada.Lang;
using System.Net;
using System.Net.Sockets;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    /// <summary>
    /// Checks TCP port availability.
    /// <para>Проверяет доступность TCP-портов.</para>
    /// </summary>
    internal class NetworkInformation
    {
        #region Variable

        private readonly object syncRoot = new object();            // synchronization object
        private CancellationTokenSource cancelTokenSource;          // cancellation token source

        private static readonly string bad = Locale.IsRussian ? "Плохой!" : "Bad!";
        private static readonly string open = Locale.IsRussian ? "Открыт!" : "Open!";
        private static readonly string close = Locale.IsRussian ? "Закрыт!" : "Close!";

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets the log callback.
        /// <para>Возвращает или задает обратный вызов журнала.</para>
        /// </summary>
        public DebugData OnDebug { get; set; }

        /// <summary>
        /// Gets or sets the tag callback.
        /// <para>Возвращает или задает обратный вызов тега.</para>
        /// </summary>
        public DebugTag OnDebugTag { get; set; }

        /// <summary>
        /// Gets or sets the tags callback.
        /// <para>Возвращает или задает обратный вызов тегов.</para>
        /// </summary>
        public DebugTags OnDebugTags { get; set; }

        #endregion Property

        #region Delegate

        /// <summary>
        /// Sends log messages.
        /// <para>Передает сообщения журнала.</para>
        /// </summary>
        public delegate void DebugData(string msg);

        /// <summary>
        /// Sends one tag.
        /// <para>Передает один тег.</para>
        /// </summary>
        public delegate void DebugTag(Tag tag);

        /// <summary>
        /// Sends multiple tags.
        /// <para>Передает несколько тегов.</para>
        /// </summary>
        public delegate void DebugTags(List<Tag> tags);

        #endregion Delegate

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public NetworkInformation()
        {
            cancelTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Runs TCP checks for the specified tags.
        /// <para>Выполняет TCP-проверки для указанных тегов.</para>
        /// </summary>
        public void RunTelnet(List<Tag> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                DebugerTags(new List<Tag>());
                return;
            }

            CancellationToken token = GetCancellationToken();
            List<Task> tasks = new List<Task>();

            foreach (Tag tag in tags)
            {
                if (tag == null || !tag.TagEnabled)
                {
                    continue;
                }

                tasks.Add(Task.Run(() => CheckTcpPort(tag, token), token));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ex)
            {
                ex.Handle(inner => inner is OperationCanceledException);
            }
        }

        /// <summary>
        /// Stops active TCP checks.
        /// <para>Останавливает активные TCP-проверки.</para>
        /// </summary>
        public void StopTelnet()
        {
            lock (syncRoot)
            {
                cancelTokenSource.Cancel();
                cancelTokenSource.Dispose();
                cancelTokenSource = new CancellationTokenSource();
            }
        }

        #endregion Basic

        #region Debug

        /// <summary>
        /// Sends tags to the callback.
        /// <para>Передает теги в обратный вызов.</para>
        /// </summary>
        internal void DebugerTags(List<Tag> tags)
        {
            OnDebugTags?.Invoke(tags);
        }

        /// <summary>
        /// Sends a tag to the callback.
        /// <para>Передает тег в обратный вызов.</para>
        /// </summary>
        internal void DebugerTag(Tag tag)
        {
            OnDebugTag?.Invoke(tag);
        }

        /// <summary>
        /// Sends a log message to the callback.
        /// <para>Передает сообщение журнала в обратный вызов.</para>
        /// </summary>
        internal void DebugerLog(string text)
        {
            OnDebug?.Invoke(text);
        }

        #endregion Debug

        #region Telnet

        /// <summary>
        /// Checks one TCP endpoint.
        /// <para>Проверяет один TCP-узел.</para>
        /// </summary>
        private void CheckTcpPort(Tag tag, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            try
            {
                string ipAddress = ResolveIpAddress(tag);
                if (!DriverUtils.IsIpAddress(ipAddress))
                {
                    SetTagResult(tag, 0, 0, bad);
                    return;
                }

                tag.TagIPAddress = ipAddress;

                if (IsTcpPortOpen(ipAddress, tag.TagPort, tag.TagTimeout))
                {
                    SetTagResult(tag, 1, 1, open);
                }
                else
                {
                    SetTagResult(tag, 0, 1, close);
                }
            }
            catch (Exception ex)
            {
                DebugerLog(ex.Message);
                SetTagResult(tag, 0, 0, bad);
            }
        }

        /// <summary>
        /// Resolves an IP address from a tag address.
        /// <para>Получает IP-адрес из адреса тега.</para>
        /// </summary>
        private static string ResolveIpAddress(Tag tag)
        {
            if (DriverUtils.IsIpAddress(tag.TagIPAddress))
            {
                return tag.TagIPAddress;
            }

            return DnsResolver.ResolveHostName(tag.TagIPAddress);
        }

        /// <summary>
        /// Checks whether the TCP port is open.
        /// <para>Проверяет, открыт ли TCP-порт.</para>
        /// </summary>
        private static bool IsTcpPortOpen(string ipAddress, int port, int timeout)
        {
            if (port <= 0 || port > 65535)
            {
                return false;
            }

            int actualTimeout = Math.Max(timeout, 1);
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            IAsyncResult asyncResult = socket.BeginConnect(remoteEndPoint, null, null);
            bool connected = asyncResult.AsyncWaitHandle.WaitOne(actualTimeout, false);

            if (!connected)
            {
                socket.Close();
                return false;
            }

            socket.EndConnect(asyncResult);
            return true;
        }

        /// <summary>
        /// Sets a tag result and sends it to subscribers.
        /// <para>Устанавливает результат тега и передает его подписчикам.</para>
        /// </summary>
        private void SetTagResult(Tag tag, int value, int status, string message)
        {
            tag.TagVal = value;
            tag.TagStat = status;

            DebugerLog($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fffff} {tag.TagIPAddress}:{tag.TagPort} {message}");
            DebugerTag(tag);
        }

        /// <summary>
        /// Gets the current cancellation token.
        /// <para>Возвращает текущий токен отмены.</para>
        /// </summary>
        private CancellationToken GetCancellationToken()
        {
            lock (syncRoot)
            {
                return cancelTokenSource.Token;
            }
        }

        #endregion Telnet
    }
}
