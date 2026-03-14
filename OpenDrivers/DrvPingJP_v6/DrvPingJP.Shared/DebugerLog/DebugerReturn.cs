namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements the logic of subscribing to receive logs.
    /// <para>Представление логики подписки на получение логов.</para>
    /// </summary>
    internal class DebugerReturn
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DebugerReturn()
        {

        }

        /// <summary>
        /// Getting the log
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(string msg);

        /// <summary>
        /// Getting the log
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
        /// </summary>
        public void Log(string text)
        {
            DebugerLog(text);
        }

    }
}
