namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements the logic of subscribing to receive tags.
    /// <para>Реализует логику подписки на получение тегов.</para>
    /// </summary>
    internal class DriverTagReturn
    {
        private readonly DebugData onDebug;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverTagReturn(DebugData onDebug = null)
        {
            this.onDebug = onDebug;
        }

        /// <summary>
        /// Getting the tags
        /// </summary>
        public delegate void DebugData(List<DriverTag> tags);

        /// <summary>
        /// Getting the tags
        /// </summary>
        internal void TagReturn(List<DriverTag> tags)
        {
            if (onDebug == null)
            {
                return;
            }

            onDebug(tags);
        }

        /// <summary>
        /// Getting the tags
        /// </summary>
        public void Return(DriverTag tag)
        {
            List<DriverTag> tags = new List<DriverTag>(); 
            tags.Add(tag);
            TagReturn(tags);
        }

        /// <summary>
        /// Getting the tags
        /// </summary>
        public void Return(List<DriverTag> tags)
        {
            TagReturn(tags);
        }
    }
}
