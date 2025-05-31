using Scada.Lang;
using System.Data;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    /// <summary>
    /// Implements the driver client.
    /// <para>Реализует клиент драйвера.</para>
    /// </summary>
    internal class DriverClient
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverClient(string path, Project project)
        {
            this.pathProject = path;
            this.project = new Project();
            this.project = project;
        }

        #region Variables
        private readonly string pathProject;                                // path project
        private readonly Project project;                                   // configuration
        private List<DriverTag> driverTags;                                 // driver tags all
        private List<DriverTag> listTagsTask = new List<DriverTag>();       // driver tags task   
        #endregion Variables

        #region Process
        /// <summary>
        /// Sequential execution of tasks.
        /// <para>Последовательное выполнение задач.</para>
        /// </summary>
        public void Process()
        {
            try
            {
                Debuger.Log(Locale.IsRussian ? "В работе." : "Working.");

                this.driverTags = CnlPrototypeFactory.GetDriverTags(project);

                foreach (Task task in project.ListTask)
                {
                    if (task.Enabled == true)
                    {
                        Step(task);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Task completion step.
        /// <para>Шаг выполнения задачи.</para>
        /// </summary>
        public void Step(Task task)
        {
            try
            {
                listTagsTask = new List<DriverTag>();

                string TagCodeDriverName = @$"DriverName_{task.Name}";
                string TagCodeDriverType = @$"DriverType_{task.Name}";
                string TagCodeDriverVolumeLabel = @$"DriverVolumeLabel_{task.Name}";
                string TagCodeDriverTotalSize = @$"DriverTotalSize_{task.Name}";
                string TagCodeDriverTotalSizeString = @$"DriverTotalSizeString_{task.Name}";
                string TagCodeDriverCurrentSize = @$"DriverCurrentSize_{task.Name}";
                string TagCodeDriverCurrentSizeString = @$"DriverCurrentSizeString_{task.Name}";
                string TagCodeProcentFreeSpaceSetPoint = @$"PercentFreeSpaceSetPoint_{task.Name}";
                string TagCodeProcentFreeSpaceCurrent = @$"PercentFreeSpaceCurrent_{task.Name}";
                string TagCodeStatusAlarm = @$"StatusAlarm_{task.Name}";
                string TagCodeActionTask = @$"ActionTask_{task.Name}";
                string TagCodeActionDate = @$"ActionDate_{task.Name}";

                // we set the necessary parameters
                string drive = task.DiskName;                                   // Укажите диск для проверки
                decimal requiredFreeSpacePercentage = task.ProceentFreeSpace;   // Процент свободного пространства
                string directoryWatcher = task.Path;                            // Укажите каталог для сжатия

                DriverTag driverTagDriverName = FindTag(TagCodeDriverName);
                driverTagDriverName.TagDataValue = drive;
                listTagsTask.Add(driverTagDriverName);

                // getting information about the disk
                DriveInfo driveInfo = new DriveInfo(drive);

                DriverTag driverTagDriverType = FindTag(TagCodeDriverType);
                driverTagDriverType.TagDataValue = driveInfo.DriveType.ToString();
                listTagsTask.Add(driverTagDriverType);

                DriverTag driverTagDriverVolumeLabel = FindTag(TagCodeDriverVolumeLabel);
                driverTagDriverVolumeLabel.TagDataValue = driveInfo.VolumeLabel;
                listTagsTask.Add(driverTagDriverVolumeLabel);

                DriverTag driverTagDriverTotalSize = FindTag(TagCodeDriverTotalSize);
                driverTagDriverTotalSize.TagDataValue = Convert.ToDouble(driveInfo.TotalSize);
                listTagsTask.Add(driverTagDriverTotalSize);

                DriverTag driverTagDriverTotalSizeString = FindTag(TagCodeDriverTotalSizeString);
                driverTagDriverTotalSizeString.TagDataValue = DiskSize(driveInfo.TotalSize);
                listTagsTask.Add(driverTagDriverTotalSizeString);
   
                DriverTag driverTagDriverCurrentSize = FindTag(TagCodeDriverCurrentSize);
                driverTagDriverCurrentSize.TagDataValue = Convert.ToDouble(driveInfo.TotalSize - driveInfo.TotalFreeSpace);
                listTagsTask.Add(driverTagDriverCurrentSize);

                DriverTag driverTagDriverCurrentSizeString = FindTag(TagCodeDriverCurrentSizeString);
                driverTagDriverCurrentSizeString.TagDataValue = DiskSize(driveInfo.TotalSize - driveInfo.TotalFreeSpace);
                listTagsTask.Add(driverTagDriverCurrentSizeString);

                // percent
                decimal freeSpacePercentage = (decimal)driveInfo.AvailableFreeSpace / driveInfo.TotalSize * 100;

                DriverTag driverTagProcentFreeSpaceSetPoint = FindTag(TagCodeProcentFreeSpaceSetPoint);
                driverTagProcentFreeSpaceSetPoint.TagDataValue = task.ProceentFreeSpace;
                listTagsTask.Add(driverTagProcentFreeSpaceSetPoint);

                DriverTag driverTagProcentFreeSpaceCurrent = FindTag(TagCodeProcentFreeSpaceCurrent);
                driverTagProcentFreeSpaceCurrent.TagDataValue = Math.Round(freeSpacePercentage, 5);
                listTagsTask.Add(driverTagProcentFreeSpaceCurrent);

                // action
                DriverTag driverTagActionTask = FindTag(TagCodeActionTask);
                driverTagActionTask.TagDataValue = DriverDictonary.ActionTaskString(task.Action);
                listTagsTask.Add(driverTagActionTask);
   
                // we check whether the free space exceeds the specified percentage.
                if (freeSpacePercentage > requiredFreeSpacePercentage)
                {
                    // alarm off
                    DriverTag driverTagStatusAlarm = FindTag(TagCodeStatusAlarm);
                    driverTagStatusAlarm.TagDataValue = 0;
                    listTagsTask.Add(driverTagStatusAlarm);
                    // datetime off
                    DriverTag driverTagActionDate = FindTag(TagCodeActionDate);
                    driverTagActionDate.TagDataValue = new object();
                    listTagsTask.Add(driverTagActionDate);
                }
                else
                {
                    // alarm on
                    DriverTag driverTagStatusAlarm = FindTag(TagCodeStatusAlarm);
                    driverTagStatusAlarm.TagDataValue = 1;
                    listTagsTask.Add(driverTagStatusAlarm);
                    // datetime on
                    DriverTag driverTagActionDate = FindTag(TagCodeActionDate);
                    driverTagActionDate.TagDataValue = DateTime.UtcNow;
                    listTagsTask.Add(driverTagActionDate);

                    List<FilesDatabase> folders = new List<FilesDatabase>();
                    List<FilesDatabase> foldersClear = new List<FilesDatabase>();
                    List<FilesDatabase> foldersSort = new List<FilesDatabase>();
                    List<DateTime> dateList = new List<DateTime>();
                    DateTime dateMin = DateTime.MinValue;
                    DateTime dateMax = DateTime.MinValue;


                    // if a folder is specified
                    if (Directory.Exists(directoryWatcher))
                    {
                        folders = FileWithChanges.SearchFolders(directoryWatcher, true);
                        foldersClear = new List<FilesDatabase>();
                        dateList = new List<DateTime>();
                        dateMin = DateTime.MinValue;
                        dateMax = DateTime.MinValue;

                        foreach (FilesDatabase folder in folders)
                        {
                            try
                            {
                                string folderName = Path.GetFileName(folder.PathFile).TrimEnd().ToUpper();
                                if (folderName != "CUR" && folderName != "MIN" && folderName != "HOUR" && folderName != "DAY")
                                {
                                    if (folderName.Contains("MIN") || folderName.Contains("HOUR") || folderName.Contains("DAY"))
                                    {
                                        string dateString = folderName.Replace("MIN", "").Replace("HOUR", "").Replace("DAY", "");

                                        // we define the format of the string: "yyyyMMdd"
                                        string format = "yyyyMMdd";

                                        // convert a string to DateTime
                                        if (DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out DateTime date))
                                        {
                                            folder.LastTimeChanged = date;
                                            foldersClear.Add(folder);
                                            dateList.Add(date);
                                        }
                                    }
                                }
                            }
                            catch { }
                        }

                        dateMin = dateList.Min();
                        dateMax = dateList.Max();

                        foldersSort = foldersClear.OrderBy(x => x.LastTimeChanged).ToList();
                    }
                    else
                    {
                        Debuger.Log(String.Format(DriverDictonary.DirectoryDoesNotExist, directoryWatcher));
                    }


                    switch (task.Action)
                    {
                        case ActionTask.None:

                            break;
                        case ActionTask.Delete:

                            for (int f = 0; f < foldersSort.Count; f++)
                            {
                                FilesDatabase folder = foldersSort[f];

                                try
                                {
                                    // getting information about the disk
                                    driveInfo = new DriveInfo(drive);
                                    // percent
                                    freeSpacePercentage = (decimal)driveInfo.AvailableFreeSpace / driveInfo.TotalSize * 100;

                                    if (freeSpacePercentage > requiredFreeSpacePercentage)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        driverTagActionDate = FindTag(TagCodeActionDate);
                                        driverTagActionDate.TagDataValue = DateTime.UtcNow;
                                        listTagsTask.Add(driverTagActionDate);

                                        Directory.Delete(folder.PathFile, true);
                                        Debuger.Log(String.Format(DriverDictonary.DirectoryDelete, folder.PathFile));
                                    }
                                }
                                catch { }
                            }
                            break;
                        case ActionTask.CompressMove:
   
                            for (int f = 0; f < foldersSort.Count; f++)
                            {
                                FilesDatabase folder = foldersSort[f];

                                try
                                {
                                    // getting information about the disk
                                    driveInfo = new DriveInfo(drive);
                                    // percent
                                    freeSpacePercentage = (decimal)driveInfo.AvailableFreeSpace / driveInfo.TotalSize * 100;
                                    if (freeSpacePercentage > requiredFreeSpacePercentage)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        driverTagActionDate = FindTag(TagCodeActionDate);
                                        driverTagActionDate.TagDataValue = DateTime.UtcNow;
                                        listTagsTask.Add(driverTagActionDate);

                                        string zipFilePath = Path.Combine(Path.GetDirectoryName(folder.PathFile), $"{Path.GetFileName(folder.PathFile)}.zip");
                                        string zipFileName = Path.GetFileName(zipFilePath);

                                        // create a ZIP archive
                                        ZipFile.CreateFromDirectory(folder.PathFile, zipFilePath);
                                        Debuger.Log(String.Format(DriverDictonary.DirectoryZip, folder.PathFile, zipFileName));
                                        // moving zip archive
                                        File.Move(zipFilePath, Path.Combine(task.PathTo, zipFileName), true);
                                        Debuger.Log(String.Format(DriverDictonary.MoveZip, zipFileName, task.PathTo));
                                        // delete the directory
                                        Directory.Delete(folder.PathFile, true);
                                        Debuger.Log(String.Format(DriverDictonary.DirectoryDelete, folder.PathFile));
                                    }
                                }
                                catch { }
                            }
                            break;
                    }
                }

                listTagsTask = listTagsTask.Distinct().ToList();
                DriverTagReturn driverTagReturn = new DriverTagReturn();
                driverTagReturn.Return(listTagsTask);
            }
            catch (Exception ex)
            {
                Debuger.Log("Exp" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Getting a list of media on the device (names).
        /// <para>Получения списка носителей на устройстве (названия).</para>
        /// </summary>
        public static List<string> GetPhysicalDrivesNames()
        {
            List<string> disks = new List<string>();

            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        disks.Add($"{drive.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(String.Format(DriverDictonary.DiskError, ex.Message));
            }

            return disks;
        }

        /// <summary>
        /// Getting a list of media on the device (information).
        /// <para>Получения списка носителей на устройстве (информация).</para>
        /// </summary>
        public static List<string> GetPhysicalDrives()
        {
            List<string> disks = new List<string>();

            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        string info = String.Format(DriverDictonary.DiskInfo, drive.Name, drive.DriveType, DiskSize(drive.TotalSize - drive.TotalFreeSpace), DiskSize(drive.TotalSize));
                        disks.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(String.Format(DriverDictonary.DiskError, ex.Message));
            }

            return disks;
        }

        /// <summary>
        /// Tag search by code from the tag list.
        /// <para>Поиск тега по коду из списка тегов.</para>
        /// </summary>
        public DriverTag FindTag(string code)
        {
            DriverTag driverTag = new DriverTag();
            return driverTag = (DriverTag)driverTags.Find(x => x.TagCode == code);
        }

        /// <summary>
        /// Size driver.
        /// <para>Размер носителя.</para>
        /// </summary>
        public static string DiskSize(long totalBytes)
        {
            string result = string.Empty;

            string sizeUnit;
            double sizeValue;

            const long BytesInKilobyte = 1024;
            const long BytesInMegabyte = BytesInKilobyte * 1024;
            const long BytesInGigabyte = BytesInMegabyte * 1024;
            const long BytesInTerabyte = BytesInGigabyte * 1024;

            if (totalBytes >= BytesInTerabyte)
            {
                sizeUnit = Locale.IsRussian ? "ТБ" : "TB";
                sizeValue = totalBytes / (double)BytesInTerabyte;
            }
            else if (totalBytes >= BytesInGigabyte)
            {
                sizeUnit = Locale.IsRussian ? "ГБ" : "GB"; ;
                sizeValue = totalBytes / (double)BytesInGigabyte;
            }
            else if (totalBytes >= BytesInMegabyte)
            {
                sizeUnit = Locale.IsRussian ? "МБ" : "MB";
                sizeValue = totalBytes / (double)BytesInMegabyte;
            }
            else if (totalBytes >= BytesInKilobyte)
            {
                sizeUnit = Locale.IsRussian ? "КБ" : "KB";
                sizeValue = totalBytes / (double)BytesInKilobyte;
            }
            else
            {
                sizeUnit = Locale.IsRussian ? "Байтов" : "Bytes";
                sizeValue = totalBytes;
            }

            return result = $"{sizeValue:F2} {sizeUnit}";
        }

        #endregion Process

        #region Validate
        /// <summary>
        /// Checking the task from the form.
        /// <para>Проверка задачи из формы.</para>
        /// </summary>
        public void Validate(Task task)
        {

            List<string> diskInfo = DriverClient.GetPhysicalDrives();
            foreach (string info in diskInfo)
            {
                Debuger.Log(info);
            }

            this.driverTags = CnlPrototypeFactory.GetDriverTags(project);
            Step(task);
        }

        #endregion Validate

        #region Log
        /// <summary>
        /// Getting logs.
        /// <para>Получение логов.</para>
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(string msg);

        /// <summary>
        /// Transfer to the form and to the file in the Log folder.
        /// <para>Перенести сообщение на форму или в лог файл.</para>
        /// </summary>
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        #endregion Log

        #region Dispose
        private IntPtr _bufferPtr;
        public int BUFFER_SIZE = 1024 * 1024 * 50; // 50 MB
        private bool _disposed = false;

        /// <summary>
        /// Destroying an instance of the class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        ~DriverClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Destroying an instance of the class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // free any other managed objects here.
            }

            // free any unmanaged objects here.
            Marshal.FreeHGlobal(_bufferPtr);
            _disposed = true;
        }

        /// <summary>
        /// Destroying an instance of the class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose

    }
}
