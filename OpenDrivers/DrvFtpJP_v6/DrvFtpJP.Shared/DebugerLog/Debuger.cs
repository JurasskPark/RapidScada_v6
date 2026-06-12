using ManagerAssistant;
using Scada.Comm.Drivers.DrvFtpJP;

namespace DebugerLog
{
    /// <summary>
    /// Writes driver log messages.
    /// <para>Записывает сообщения журнала драйвера.</para>
    /// </summary>
    public class Debuger
    {
        #region Variable
        public static bool isDll;                    // application or dll
        public static bool logWrite;                 // log write flag
        public static int logDays;                   // log retention period in days
        public static string logPath;                // log path
        #endregion Variable

        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public Debuger()
        {
            isDll = Manager.IsDll;
            logWrite = Manager.LogWrite;
            logPath = Manager.LogPath;
        }

        /// <summary>
        /// Writes a log message using manager settings.
        /// <para>Записывает сообщение журнала с использованием настроек менеджера.</para>
        /// </summary>
        /// <param name="text">Log message.</param>
        public static void Log(string text)
        {
            try
            {
                if (Manager.IsDll)
                {
                    if (Manager.LogWrite)
                    {
                        DebugerReturn debugerReturn = new DebugerReturn();
                        debugerReturn.Log(text);
                    }
                }
                else if (!Manager.IsDll)
                {
                    if (Manager.LogWrite)
                    {
                        Log(Manager.LogPath, text);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Writes a log message to the specified folder.
        /// <para>Записывает сообщение журнала в указанный каталог.</para>
        /// </summary>
        /// <param name="folder">Log folder.</param>
        /// <param name="text">Log message.</param>
        public static void Log(string folder, string text)
        {
            try
            {
                // driver
                DebugerReturn debugerReturn = new DebugerReturn();
                debugerReturn.Log(text);

                text = @$"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff")}] {text}";

                // file log
                Directory.CreateDirectory(folder);
                StreamWriter streamWriter = File.AppendText(Path.Combine(folder, DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
                streamWriter.WriteLine(text);
                streamWriter.Close();
            }
            catch
            {
            }

            try
            {
                Clear(folder, Manager.LogDays);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Deletes old log files.
        /// <para>Удаляет старые файлы журнала.</para>
        /// </summary>
        /// <param name="path">Log folder path.</param>
        /// <param name="days">Log retention period in days.</param>
        public static void Clear(string path, int days)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            try
            {
                days = days > 0 ? days * (-1) : days;
                DateTime findDateTime = DateTime.Now.AddDays(days);

                List<FilesDatabase> files = FileWithChanges.SearchFiles(path);

                for (int i = 0; i < files.Count; i++)
                {
                    FilesDatabase file = files[i];
                    if (file != null)
                    {
                        if (file.LastTimeChanged < findDateTime)
                        {
                            string fullPathToUpper = file.PathFile.ToUpper();
                            string templateFileName = ".log";

                            if (templateFileName != string.Empty && fullPathToUpper.Contains(templateFileName.ToUpper()))
                            {
                                try
                                {
                                    File.Delete(fullPathToUpper);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        #endregion Basic
    }
}