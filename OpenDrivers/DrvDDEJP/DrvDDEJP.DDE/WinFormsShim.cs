using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides a minimal WinForms compatibility shim for message types and filters.
    /// <para>Минимальная совместимость с WinForms: типы сообщений и фильтры.</para>
    /// </summary>
    public struct Message
    {
        /// <summary>
        /// Gets or sets the window handle.
        /// <para>Дескриптор окна.</para>
        /// </summary>
        public IntPtr HWnd { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// <para>Идентификатор сообщения.</para>
        /// </summary>
        public int Msg { get; set; }

        /// <summary>
        /// Gets or sets the WParam value.
        /// <para>Параметр WParam.</para>
        /// </summary>
        public IntPtr WParam { get; set; }

        /// <summary>
        /// Gets or sets the LParam value.
        /// <para>Параметр LParam.</para>
        /// </summary>
        public IntPtr LParam { get; set; }

        /// <summary>
        /// Gets or sets the result value.
        /// <para>Результат обработки сообщения.</para>
        /// </summary>
        public IntPtr Result { get; set; }
    }

    /// <summary>
    /// Defines a minimal message filter interface.
    /// <para>Интерфейс минимального фильтра сообщений.</para>
    /// </summary>
    public interface IMessageFilter
    {
        /// <summary>
        /// Pre-filters a message before dispatching.
        /// <para>Предварительная обработка сообщения перед отправкой.</para>
        /// </summary>
        /// <param name="m">The message to filter.</param>
        /// <returns>True to filter out the message; otherwise false.</returns>
        bool PreFilterMessage(ref Message m);
    }

    /// <summary>
    /// Provides a minimal Application shim that supports message filters.
    /// <para>Минимальная реализация Application с поддержкой фильтров сообщений.</para>
    /// </summary>
    public static class Application
    {
        #region Variable
        [ThreadStatic]
        private static List<IMessageFilter>? messageFilters;        // thread-local list of registered message filters
        #endregion Variable

        /// <summary>
        /// Adds a message filter if not already registered.
        /// <para>Добавляет фильтр сообщений, если он ещё не зарегистрирован.</para>
        /// </summary>
        /// <param name="value">The message filter to add.</param>
        public static void AddMessageFilter(IMessageFilter value)
        {
            if (value == null)
            {
                return;
            }

            messageFilters ??= new List<IMessageFilter>();
            if (!messageFilters.Contains(value))
            {
                messageFilters.Add(value);
            }
        }

        /// <summary>
        /// Removes a previously registered message filter.
        /// <para>Удаляет ранее зарегистрированный фильтр сообщений.</para>
        /// </summary>
        /// <param name="value">The message filter to remove.</param>
        public static void RemoveMessageFilter(IMessageFilter value)
        {
            messageFilters?.Remove(value);
        }
    }
}
