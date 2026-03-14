namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements the logic of subscribing to receive tags.
    /// <para>Реализует логику подписки на получение тегов.</para>
    /// </summary>
    internal class DriverTagReturn
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverTagReturn()
        {

        }

        /// <summary>
        /// Getting the tags
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(List<DriverTag> tags);

        /// <summary>
        /// Getting the tags
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
