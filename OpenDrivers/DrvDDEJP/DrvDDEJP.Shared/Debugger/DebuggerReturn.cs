using System;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Provides a callback-based debug message dispatcher.
    /// <para>Предоставляет диспетчер отладочных сообщений на основе обратного вызова.</para>
    /// </summary>
    internal class DebuggerReturn
    {
        #region Variable

        /// <summary>
        /// Represents a delegate that handles a debug message.
        /// <para>Делегат для обработки отладочных сообщений.</para>
        /// </summary>
        public delegate void DebugData(string msg);

        /// <summary>
        /// Occurs when a debug message is available.
        /// <para>Срабатывает при наличии отладочного сообщения.</para>
        /// </summary>
        public static DebugData OnDebug = null;                     // static debug callback

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DebuggerReturn()
        {
        }

        /// <summary>
        /// Sends a debug message to subscribed handlers.
        /// <para>Отправляет отладочное сообщение подписчикам.</para>
        /// </summary>
        /// <param name="text">The debug text to send.</param>
        public void Log(string text)
        {
            OnDebug?.Invoke(text);
        }

        #endregion Basic
    }
}
