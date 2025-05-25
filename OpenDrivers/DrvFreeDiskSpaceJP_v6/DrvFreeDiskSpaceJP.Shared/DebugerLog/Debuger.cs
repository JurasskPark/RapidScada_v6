namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    /// <summary>
    /// Recording logs.
    /// <para>Запись логов.</para>
    /// </summary>
    public class Debuger
    {
        #region Variables
        public static Project project = new Project();                         // the project configuration
        public static string pathLog;
        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Debuger()
        {
            string errMsg = string.Empty;
            project = Manager.Project;
            pathLog = Manager.PathLog;
        }

        /// <summary>
        /// Recording log
        /// </summary>
        public static void Log(string text)
        {
            try
            {
                

                string errMsg = string.Empty;

                project = Manager.Project;
                pathLog = Manager.PathLog;

                if (Manager.IsDll)
                {
                    if (project.DebugerSettings.LogWrite)
                    {
                        DebugerReturn debugerReturn = new DebugerReturn();
                        debugerReturn.Log(text);
                    }
                }
                else if (!Manager.IsDll)
                {
                    if (project.DebugerSettings.LogWrite)
                    {
                        Log(Manager.PathLog, text);
                    }
                }
            }
            catch { }

        }

        /// <summary>
        /// Recording log
        /// </summary>
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
            catch { }

            try
            {
                Clear(folder, project.DebugerSettings.LogDays);
            }
            catch { }
        }

        /// <summary>
        /// Clear log
        /// </summary>
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
                                catch { }
                            }
                        }
                    }
                }
            }
            catch { }
        }


    }
}