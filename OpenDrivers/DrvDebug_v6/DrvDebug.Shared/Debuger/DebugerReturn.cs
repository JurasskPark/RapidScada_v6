namespace DebugerLog
{
    /// <summary>
    /// Provides a callback-based debug message dispatcher.
    /// <para>Предоставляет диспетчер отладочных сообщений на основе обратного вызова.</para>
    /// </summary>
    internal class DebugerReturn
    {
        /// <summary>
        /// Represents a delegate that handles a debug message.
        /// </summary>
        public delegate void DebugData(string msg);

        /// <summary>
        /// Occurs when a debug message is available.
        /// </summary>
        public static DebugData? OnDebug = null;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DebugerReturn()
        {
        }

        /// <summary>
        /// Sends a debug message to subscribed handlers.
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
        /// Writes a debug message.
        /// </summary>
        public void Log(string text)
        {
            DebugerLog(text);
        }
    }
}
