namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    /// <summary>
    /// Represents a manager.
    /// <para>Представляет менеджер.</para>
    /// </summary>
    public static class Manager
    {
        #region Variables
        /// <summary>
        /// The application or dll.
        /// <para>Приложение или библиотека.</para>
        /// </summary>
        public static bool IsDll;

        /// <summary>
        /// The path log.
        /// <para>Путь до лога.</para>
        /// </summary>
        public static string PathLog;

        /// <summary>
        /// The path project.
        /// <para>Путь до проекта.</para>
        /// </summary>
        public static string PathProject;

        /// <summary>
        /// The project.
        /// <para>Проект.</para>
        /// </summary>
        public static Project Project;

        /// <summary>
        /// The device number.
        /// <para>Номер устройства.</para>
        /// </summary>
        public static int DeviceNum;
        #endregion Variables
    }
}
