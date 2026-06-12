using DebugerLog;
using Scada.Lang;
using System.Runtime.InteropServices;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Provides file search and file watcher helper methods.
    /// <para>Предоставляет методы поиска файлов и вспомогательные методы наблюдателя файлов.</para>
    /// </summary>
    public class FileWithChanges : IDisposable
    {
        #region Variable
        private IntPtr bufferPtr;                                           // buffer pointer
        private bool disposed = false;                                      // disposed flag
        private static List<FilesDatabase> lstFiles = new List<FilesDatabase>(); // file list
        private static List<string> lstFolders = new List<string>();        // folder list
        #endregion Variable

        #region Property
        /// <summary>
        /// Gets or sets the transfer buffer size.
        /// <para>Возвращает или задает размер буфера передачи.</para>
        /// </summary>
        public int BufferSize = 1024 * 1024 * 50;                           // buffer size
        #endregion Property

        #region Dispose
        /// <summary>
        /// Finalizes an instance of the class.
        /// <para>Финализирует экземпляр класса.</para>
        /// </summary>
        ~FileWithChanges()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases managed and unmanaged resources.
        /// <para>Освобождает управляемые и неуправляемые ресурсы.</para>
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be released.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // free any other managed objects here.
            }

            // free any unmanaged objects here.
            Marshal.FreeHGlobal(bufferPtr);
            disposed = true;
        }

        /// <summary>
        /// Releases resources used by the instance.
        /// <para>Освобождает ресурсы, используемые экземпляром.</para>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose

        #region Basic
        /// <summary>
        /// Gets a list of files from a directory.
        /// <para>Получает список файлов из каталога.</para>
        /// </summary>
        /// <param name="path">Directory path.</param>
        /// <param name="useSubDir">Indicates whether subdirectories should be searched.</param>
        /// <returns>File database row list.</returns>
        public static List<FilesDatabase> SearchFiles(string path, bool useSubDir = false)
        {
            try
            {
                lstFiles = new List<FilesDatabase>();
                if (useSubDir == false)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    FileInfo[] files = dirInfo.GetFiles();

                    foreach (FileInfo fileInfo in files)
                    {
                        FilesDatabase file = new FilesDatabase();
                        file.PathFile = fileInfo.FullName;
                        file.SizeFile = fileInfo.Length;
                        file.LastTimeChanged = fileInfo.LastWriteTime;
                        lstFiles.Add(file);
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
                LogError(ex);
                return new List<FilesDatabase>();
            }
        }

        /// <summary>
        /// Escapes single quotes in SQL values.
        /// <para>Экранирует одинарные кавычки в SQL-значениях.</para>
        /// </summary>
        /// <param name="text">Source text.</param>
        /// <returns>Escaped text.</returns>
        public static string SQLValidationValue(string text)
        {
            return text.Replace("''", "'").Replace("'", "''");
        }

        /// <summary>
        /// Searches files in a directory tree.
        /// <para>Ищет файлы в дереве каталогов.</para>
        /// </summary>
        /// <param name="dir">Directory path.</param>
        /// <returns>File database row list.</returns>
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
                LogError(ex);
                return lstFiles = new List<FilesDatabase>();
            }
        }

        /// <summary>
        /// Gets folders from a directory tree.
        /// <para>Получает каталоги из дерева каталогов.</para>
        /// </summary>
        /// <param name="dir">Directory path.</param>
        /// <returns>Folder path list.</returns>
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
                LogError(ex);
                return lstFolders = new List<string>();
            }
        }

        /// <summary>
        /// Gets files from the specified folder list.
        /// <para>Получает файлы из указанного списка каталогов.</para>
        /// </summary>
        /// <param name="lstFolders">Folder path list.</param>
        /// <returns>File database row list.</returns>
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
                        foreach (FileInfo fileInfo in files)
                        {
                            FilesDatabase file = new FilesDatabase();
                            file.PathFile = fileInfo.FullName;
                            file.SizeFile = fileInfo.Length;
                            file.LastTimeChanged = fileInfo.LastWriteTime;
                            lstFiles.Add(file);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }
                }

                return lstFiles;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return lstFiles;
            }
        }

        /// <summary>
        /// Writes an exception message to the driver log.
        /// <para>Записывает сообщение исключения в журнал драйвера.</para>
        /// </summary>
        /// <param name="ex">Exception.</param>
        private static void LogError(Exception ex)
        {
            string errMsg = @$"{ex.Message}";
            Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");
        }
        #endregion Basic
    }
}