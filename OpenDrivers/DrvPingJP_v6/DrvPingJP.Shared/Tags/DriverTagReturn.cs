namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements the logic of subscribing to receive tags.
    /// <para>Реализует логику подписку на получение тегов.</para>
    /// </summary>
    internal class DriverTagReturn
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverTagReturn()
        {

        }

        /// <summary>
        /// Getting the tags
        /// <para>Получение тегов</para>
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(List<DriverTag> tags);

        /// <summary>
        /// Getting the tags
        /// <para>Получение тегов</para>
        /// </summary>
        internal void TagReturn(List<DriverTag> tags)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(tags);
        }

        /// <summary>
        /// Getting the tags
        /// <para>Получение тегов</para>
        /// </summary>
        public void Return(DriverTag tag)
        {
            List<DriverTag> tags = new List<DriverTag>(); 
            tags.Add(tag);
            TagReturn(tags);
        }

        /// <summary>
        /// Getting the tags
        /// <para>Получение тегов</para>
        /// </summary>
        public void Return(List<DriverTag> tags)
        {
            TagReturn(tags);
        }
    }
}
