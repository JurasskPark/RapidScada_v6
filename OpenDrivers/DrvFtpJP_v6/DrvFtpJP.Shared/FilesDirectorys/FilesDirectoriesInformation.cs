using DebugerLog;
using Scada.Comm.Drivers.DrvFtpJP;

namespace DrvFtpJP.Shared.FilesDirectorys
{
    /// <summary>
    /// Represents information about a file system object.
    /// <para>Представляет информацию об объекте файловой системы.</para>
    /// </summary>
    public class FilesDirectoriesInformation
    {
        #region Variable
        private long size;                       // object size
        #endregion Variable

        #region Property
        /// <summary>
        /// Gets or sets object name.
        /// <para>Возвращает или задает имя объекта.</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets full object path.
        /// <para>Возвращает или задает полный путь объекта.</para>
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets object type.
        /// <para>Возвращает или задает тип объекта.</para>
        /// </summary>
        public FilesDirectoriesType Type { get; set; }

        /// <summary>
        /// Gets or sets object size in bytes.
        /// <para>Возвращает или задает размер объекта в байтах.</para>
        /// </summary>
        public long Size
        {
            get
            {
                if (Type == FilesDirectoriesType.File)
                {
                    return size;
                }
                else if (Type == FilesDirectoriesType.Directory)
                {
                    return 0;
                }

                return 0;
            }
            set
            {
                size = value;
                SizeString = DriverUtils.DiskSize(size);
            }
        }

        /// <summary>
        /// Gets or sets last write date.
        /// <para>Возвращает или задает дату последнего изменения.</para>
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets formatted object size.
        /// <para>Возвращает или задает форматированный размер объекта.</para>
        /// </summary>
        public string SizeString { get; set; }

        /// <summary>
        /// Gets or sets file extension.
        /// <para>Возвращает или задает расширение файла.</para>
        /// </summary>
        public string Format { get; set; }
        #endregion Property

        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FilesDirectoriesInformation()
        {
            Name = string.Empty;
            FullName = string.Empty;
            Type = FilesDirectoriesType.None;
            Size = 0;
            Date = DateTime.MinValue;
            Format = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the class with specified values.
        /// <para>Инициализирует новый экземпляр класса с указанными значениями.</para>
        /// </summary>
        /// <param name="name">Object name.</param>
        /// <param name="fullname">Full object path.</param>
        /// <param name="type">Object type.</param>
        /// <param name="size">Object size.</param>
        /// <param name="date">Last write date.</param>
        /// <param name="format">File extension.</param>
        public FilesDirectoriesInformation(string name, string fullname, FilesDirectoriesType type, long size, DateTime date, string format)
        {
            Name = name;
            FullName = fullname;
            Type = type;
            Size = size;
            Date = date;
            Format = format;
        }

        /// <summary>
        /// Gets a list of available drive names.
        /// <para>Получает список имен доступных дисков.</para>
        /// </summary>
        /// <returns>Drive name list.</returns>
        public static List<string> GetPhysicalDrivesNames()
        {
            List<string> disks = new List<string>();

            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        disks.Add($"{drive.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(string.Format(DriverDictonary.DiskError, ex.Message));
            }

            return disks;
        }

        /// <summary>
        /// Gets directories and files from the specified path.
        /// <para>Получает каталоги и файлы из указанного пути.</para>
        /// </summary>
        /// <param name="path">Directory path.</param>
        /// <returns>Directory and file information list.</returns>
        public static List<FilesDirectoriesInformation> GetDirectoriesAndFiles(string path)
        {
            List<FilesDirectoriesInformation> list = new List<FilesDirectoriesInformation>();

            try
            {
                IEnumerable<DirectoryInfo> directories = from dir in Directory.GetDirectories(path) select new DirectoryInfo(dir);
                foreach (DirectoryInfo dir in directories)
                {
                    try
                    {
                        FilesDirectoriesInformation directoryInfo = new FilesDirectoriesInformation();
                        directoryInfo.Type = FilesDirectoriesType.Directory;
                        directoryInfo.Date = dir.LastWriteTime;
                        directoryInfo.Name = dir.Name;
                        directoryInfo.FullName = dir.FullName;
                        directoryInfo.Size = -1;
                        directoryInfo.SizeString = string.Empty;
                        directoryInfo.Format = string.Empty;
                        list.Add(directoryInfo);
                    }
                    catch
                    {
                    }
                }

                IEnumerable<FileInfo> files = from file in Directory.GetFiles(path) select new FileInfo(file);
                foreach (FileInfo file in files)
                {
                    try
                    {
                        FilesDirectoriesInformation fileInfo = new FilesDirectoriesInformation();
                        fileInfo.Type = FilesDirectoriesType.File;
                        fileInfo.Date = file.LastWriteTime;
                        fileInfo.Name = file.Name;
                        fileInfo.FullName = file.FullName;
                        fileInfo.Size = file.Length;
                        fileInfo.Format = Path.GetExtension(file.Name).TrimStart('.');
                        list.Add(fileInfo);
                    }
                    catch
                    {
                    }
                }

                return list;
            }
            catch
            {
                return list;
            }
        }
        #endregion Basic

        #region Support class
        /// <summary>
        /// Defines file system object types.
        /// <para>Определяет типы объектов файловой системы.</para>
        /// </summary>
        public enum FilesDirectoriesType : int
        {
            /// <summary>
            /// Object type is not defined.
            /// <para>Тип объекта не задан.</para>
            /// </summary>
            None,

            /// <summary>
            /// File object.
            /// <para>Файл.</para>
            /// </summary>
            File,

            /// <summary>
            /// Directory object.
            /// <para>Каталог.</para>
            /// </summary>
            Directory,

            /// <summary>
            /// Link object.
            /// <para>Ссылка.</para>
            /// </summary>
            Link,
        }
        #endregion Support class
    }
}