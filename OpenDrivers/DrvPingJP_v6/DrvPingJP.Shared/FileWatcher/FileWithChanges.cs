using Scada.Lang;
using System.Runtime.InteropServices;


namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Performing operations with files and directories in the operating system.
    /// <para>Проведение операций с файлами и каталогами в операционной системе.</para>
    /// </summary>
    public class FileWithChanges : IDisposable
    {
        #region Variables
        object Obj = new object();
        #endregion Variables

        #region Dispose
        private IntPtr _bufferPtr;
        public int BUFFER_SIZE = 1024 * 1024 * 50; // 50 MB
        private bool _disposed = false;

        /// <summary>
        /// Dispose an instance of a class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        ~FileWithChanges()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose an instance of a class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
            }

            // Free any unmanaged objects here.
            Marshal.FreeHGlobal(_bufferPtr);
            _disposed = true;
        }

        /// <summary>
        /// Dispose an instance of a class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose

        #region SearchFiles
        private static List<FilesDatabase> lstFiles = new List<FilesDatabase>(); 
        private static List<string> lstFolders = new List<string>();
        private static string dir = string.Empty;
        private static string subdir = string.Empty;

        /// <summary>
        /// Getting a list of files from a directory.
        /// <para>Получение списка файлов из каталога.</para>
        /// </summary>
        public static List<FilesDatabase> SearchFiles(string path, bool useSubDir = false)
        {
            try
            {
                lstFiles = new List<FilesDatabase>();
                if (useSubDir == false)
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[] files = dir.GetFiles();          // get a list of directory files // получаем список файлов каталога

                    foreach (FileInfo i in files)
                    {
                        FilesDatabase file = new FilesDatabase();
                        file.PathFile = i.FullName;
                        file.SizeFile = i.Length;
                        file.LastTimeChanged = i.LastWriteTime;
                        lstFiles.Add(file);
                        //Debuger.Log("Имя файла {0}, Размер файла {1}, Дата создания {2}, Дата изменения {3}", i.Name, i.Length, i.CreationTime, i.LastWriteTime); 
                    }

                    return lstFiles;
                }
                else
                {
                    return IterateSortFoldersFiles(path);
                }
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return new List<FilesDatabase>();
            }
        }

        /// <summary>
        /// Getting a list of directories.
        /// <para>Получение списка каталогов.</para>
        /// </summary>
        public static List<FilesDatabase> SearchFolders(string path, bool useSubDir = false)
        {
            try
            {
                List<FilesDatabase> lstFoldersCurrent = new List<FilesDatabase>();
                if (useSubDir == false)
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    DirectoryInfo[] dires = dir.GetDirectories();          // get a list of directory files // получаем список файлов каталога

                    foreach (DirectoryInfo i in dires)
                    {
                        FilesDatabase file = new FilesDatabase();
                        file.PathFile = i.FullName;
                        file.LastTimeChanged = i.LastWriteTime;
                        lstFoldersCurrent.Add(file);
                        //Debuger.Log("Имя файла {0}, Размер файла {1}, Дата создания {2}, Дата изменения {3}", i.Name, i.Length, i.CreationTime, i.LastWriteTime); 
                    }

                    return lstFoldersCurrent;
                }
                else
                {
                    List<string> lstFolder = IterateSortFolders(path);
                    foreach (string folder in lstFolder)
                    {
                        DirectoryInfo dir = new DirectoryInfo(folder);
                        
                        FilesDatabase file = new FilesDatabase();
                        file.PathFile = dir.FullName;
                        file.LastTimeChanged = dir.LastWriteTime;
                        lstFoldersCurrent.Add(file);
                    }
                    return lstFoldersCurrent;
                }
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return new List<FilesDatabase>();
            }
        }

        /// <summary>
        /// Search for all objects through recursion.
        /// <para>Поиск всех объектов через рекурсию.</para>
        /// </summary>
        private static List<FilesDatabase> IterateSortFoldersFiles(string dir)
        {
            try
            {
                lstFiles = new List<FilesDatabase>();
                lstFolders = new List<string>();
                lstFolders.Add(dir);

                return IterateSortFiles(IterateSortFolders(dir));
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return lstFiles = new List<FilesDatabase>();
            }
        }

        /// <summary>
        /// Search for all objects through recursion.
        /// <para>Поиск всех объектов через рекурсию.</para>
        /// </summary>
        private static List<string> IterateSortFolders(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                foreach (DirectoryInfo tmpdir in dirInfo.GetDirectories())
                {
                    lstFolders.Add(tmpdir.FullName);
                    IterateSortFolders(tmpdir.FullName);
                }

                return lstFolders;
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return lstFolders = new List<string>();
            }
        }

        /// <summary>
        /// Search for all objects through recursion.
        /// <para>Поиск всех объектов через рекурсию.</para>
        /// </summary>
        private static List<FilesDatabase> IterateSortFiles(List<string> lstFolders)
        {
            try
            {
                foreach (string dir in lstFolders)
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dir);
                        FileInfo[] files = dirInfo.GetFiles();
                        foreach (FileInfo i in files)
                        {
                            FilesDatabase file = new FilesDatabase();
                            file.PathFile = i.FullName;
                            file.SizeFile = i.Length;
                            file.LastTimeChanged = i.LastWriteTime;
                            lstFiles.Add(file);
                            //Debuger.Log("Имя файла {0}, Размер файла {1}, Дата создания {2}, Дата изменения {3}", i.Name, i.Length, i.CreationTime, i.LastWriteTime); 
                        }
                    }
                    catch (Exception ex)
                    {
                        string errMsg = @$"{ex.Message}";
                        Debuger.Log(Locale.IsRussian ?
                        @$"[Ошибка] {errMsg}" :
                        @$"[Error] {errMsg}");
                    }
                }

                return lstFiles;
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return lstFiles;
            }
        }
        #endregion SearchFiles

    }
}
