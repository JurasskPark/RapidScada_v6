using Scada.Comm.Drivers.DrvFtpJP;
using System.Runtime.InteropServices;

namespace ManagerAssistant
{
    /// <summary>
    /// Represents a manager.
    /// <para>Представляет менеджер.</para>
    /// </summary>
    public static class Manager
    {
        #region Variables
        /// <summary>
        /// The application or dll
        /// </summary>
        public static bool IsDll;

        /// <summary>
        /// the write log
        /// </summary>
        public static bool LogWrite;

        /// <summary>
        /// The path log
        /// </summary>
        public static string LogPath;

        /// <summary>
        /// The log days
        /// </summary>
        public static int LogDays;

        /// <summary>
        /// The path log
        /// </summary>
        public static string ProjectPath;

        /// <summary>
        /// The project
        /// </summary>
        public static Project Project;

        /// <summary>
        /// The device number
        /// </summary>
        public static int DeviceNum;

        /// <summary>
        /// The database 
        /// </summary>
        //public static DbConnSettings Database;
        #endregion Variables
    }
}
