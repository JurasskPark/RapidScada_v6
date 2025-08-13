using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    public class ApplicationPath
    {
        /// <summary>
        /// Get directory of executable file.
        /// <para>Получить директорию исполняемого файла.</para>
        /// </summary>
        public static string StartPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Get directory of language translations.
        /// <para>Получить директорию языковых переводов.</para>
        /// </summary>
        public static string LangDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Lang");

        /// <summary>
        /// Get log directory.
        /// <para>Получить директорию логов.</para>
        /// </summary>
        public static string LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log");

    }
}
