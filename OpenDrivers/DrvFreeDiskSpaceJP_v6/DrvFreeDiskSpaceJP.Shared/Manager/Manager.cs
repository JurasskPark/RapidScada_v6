using Scada.Comm.Drivers.DrvFreeDiskSpaceJP;
using System.Runtime.InteropServices;

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
        /// The application or dll
        /// </summary>
        public static bool IsDll;

        /// <summary>
        /// The path log
        /// </summary>
        public static string PathLog;

        /// <summary>
        /// The path log
        /// </summary>
        public static string PathProject;

        /// <summary>
        /// The project
        /// </summary>
        public static Project Project;

        /// <summary>
        /// The device number
        /// </summary>
        public static int DeviceNum;
        #endregion Variables
    }
}
