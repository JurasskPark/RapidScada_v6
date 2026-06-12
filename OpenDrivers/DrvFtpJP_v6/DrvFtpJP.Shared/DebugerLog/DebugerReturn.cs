namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Sends debug log messages to subscribers.
    /// <para>Передает подписчикам сообщения отладочного журнала.</para>
    /// </summary>
    internal class DebugerReturn
    {
        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DebugerReturn()
        {
        }

        /// <summary>
        /// Sends a debug log message to subscribers.
        /// <para>Передает подписчикам сообщение отладочного журнала.</para>
        /// </summary>
        /// <param name="text">Log message.</param>
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        /// <summary>
        /// Writes a debug log message.
        /// <para>Записывает сообщение отладочного журнала.</para>
        /// </summary>
        /// <param name="text">Log message.</param>
        public void Log(string text)
        {
            DebugerLog(text);
        }
        #endregion Basic

        #region Support class
        /// <summary>
        /// Occurs when a debug log message is available.
        /// <para>Возникает при появлении сообщения отладочного журнала.</para>
        /// </summary>
        public static DebugData OnDebug;

        /// <summary>
        /// Represents a debug log callback.
        /// <para>Представляет callback отладочного журнала.</para>
        /// </summary>
        /// <param name="msg">Log message.</param>
        public delegate void DebugData(string msg);
        #endregion Support class
    }
}