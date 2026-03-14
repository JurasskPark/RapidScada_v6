namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Represents a manager.
    /// <para>Представление менеджера.</para>
    /// </summary>
    public static class Manager
    {
        #region Variables
        /// <summary>
        /// The application or dll.
        /// </summary>
        public static bool IsDll;

        /// <summary>
        /// The path log.
        /// </summary>
        public static string PathLog;

        /// <summary>
        /// The path project.
        /// </summary>
        public static string PathProject;

        /// <summary>
        /// The project.
        /// </summary>
        public static Project Project;

        /// <summary>
        /// The device number.
        /// </summary>
        public static int DeviceNum;
        #endregion Variables
    }
}
