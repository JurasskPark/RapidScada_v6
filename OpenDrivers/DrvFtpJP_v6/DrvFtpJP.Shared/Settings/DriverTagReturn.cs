namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Sends driver tags to subscribers.
    /// <para>Передает теги драйвера подписчикам.</para>
    /// </summary>
    internal class DriverTagReturn
    {
        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverTagReturn()
        {
        }

        /// <summary>
        /// Sends driver tags to subscribers.
        /// <para>Передает теги драйвера подписчикам.</para>
        /// </summary>
        /// <param name="tags">Driver tag list.</param>
        internal void TagReturn(List<DriverTag> tags)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(tags);
        }

        /// <summary>
        /// Returns driver tags through the callback.
        /// <para>Возвращает теги драйвера через callback.</para>
        /// </summary>
        /// <param name="tags">Driver tag list.</param>
        public void Return(List<DriverTag> tags)
        {
            TagReturn(tags);
        }
        #endregion Basic

        #region Support class
        /// <summary>
        /// Occurs when driver tags are available.
        /// <para>Возникает при появлении тегов драйвера.</para>
        /// </summary>
        public static DebugData OnDebug;

        /// <summary>
        /// Represents a driver tag callback.
        /// <para>Представляет callback тегов драйвера.</para>
        /// </summary>
        /// <param name="tags">Driver tag list.</param>
        public delegate void DebugData(List<DriverTag> tags);
        #endregion Support class
    }
}