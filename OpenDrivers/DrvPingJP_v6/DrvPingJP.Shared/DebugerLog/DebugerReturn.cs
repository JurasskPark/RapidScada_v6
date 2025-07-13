namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements the logic of subscribing to receive logs.
    /// <para>Реализует логику подписку на получение логов.</para>
    /// </summary>
    internal class DebugerReturn
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DebugerReturn()
        {

        }

        /// <summary>
        /// Getting the log
        /// <para>Получение лога</para>
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(string msg);

        /// <summary>
        /// Getting the log
        /// <para>Получение лога</para>
        /// </summary>
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        /// <summary>
        /// Getting the log
        /// <para>Получение лога</para>
        /// </summary>
        public void Log(string text)
        {
            DebugerLog(text);
        }

    }
}
