using Scada.Lang;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    internal class DriverClient
    {
        private readonly string pathProject;                                // path project
        private readonly Project project;                                   // configuration
        private List<DriverTag> driverTags;                                 // driver tags all
        private List<DriverTag> listTagsTask = new List<DriverTag>();       // driver tags task    

        public DriverClient(string path, Project project)
        {
            this.pathProject = path;
            this.project = new Project();
            this.project = project;
        }

        #region Process
        /// <summary>
        /// Sequential execution of tasks
        /// <para>Последовательное выполнение задач</para>
        /// </summary>
        public void Process()
        {
            try
            {
                Debuger.Log(Locale.IsRussian ? "В работе. " : "Working. ");

                Debuger.Log(Locale.IsRussian ?
                       @$"Количество задач: {project.ListTask.Count}." :
                       @$"Number of tasks: {project.ListTask.Count}.");

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

        public void Step(Task task)
        {
            try
            {
                Debuger.Log("Count = " + driverTags.Count.ToString());

                Debuger.Log("+0+");
            listTagsTask = new List<DriverTag>();

            string TagCodeDriverName =                  @$"DriverName_{task.Name}";
            string TagCodeDriverType =                  @$"DriverType_{task.Name}";
            string TagCodeDriverVolumeLabel =           @$"DriverVolumeLabel_{task.Name}";
            string TagCodeDriverTotalSize =             @$"DriverTotalSize_{task.Name}";
            string TagCodeDriverTotalSizeString =       @$"DriverTotalSizeString_{task.Name}";
            string TagCodeDriverCurrentSize =           @$"DriverCurrentSize_{task.Name}";
            string TagCodeDriverCurrentSizeString =     @$"DriverCurrentSizeString_{task.Name}";
            string TagCodeProcentFreeSpaceSetPoint =    @$"ProcentFreeSpaceSetPoint_{task.Name}";
            string TagCodeProcentFreeSpaceCurrent =     @$"ProcentFreeSpaceCurrent_{task.Name}";
            string TagCodeStatusAlarm =                 @$"StatusAlarm_{task.Name}";
            string TagCodeActionTask =                  @$"ActionTask_{task.Name}";
            string TagCodeActionDate =                  @$"ActionDate_{task.Name}";

            Debuger.Log("+0.0+");

                // Задаем необходимые параметры
                string drive = task.DiskName;                                   // Укажите диск для проверки
                decimal requiredFreeSpacePercentage = task.ProceentFreeSpace;   // Процент свободного пространства
                string directoryWatcher = task.Path;                         // Укажите каталог для сжатия

                DriverTag driverTagDriverName = FindTag(TagCodeDriverName);
                driverTagDriverName.TagDataValue = drive;
                listTagsTask.Add(driverTagDriverName);

            Debuger.Log("+0.1+");
            // Получаем информацию о диске
            DriveInfo driveInfo = new DriveInfo(drive);
            Debuger.Log("+0.2+");
            DriverTag driverTagDriverType = FindTag(TagCodeDriverType);
            driverTagDriverType.TagDataValue = driveInfo.DriveType.ToString();
            listTagsTask.Add(driverTagDriverType);
            Debuger.Log("+0.3+");
            DriverTag driverTagDriverVolumeLabel = FindTag(TagCodeDriverVolumeLabel);
            driverTagDriverVolumeLabel.TagDataValue = driveInfo.VolumeLabel;
            listTagsTask.Add(driverTagDriverVolumeLabel);
            Debuger.Log("+0.4+");
            DriverTag driverTagDriverTotalSize = FindTag(TagCodeDriverTotalSize);
            driverTagDriverTotalSize.TagDataValue = Convert.ToDouble(driveInfo.TotalSize);
            listTagsTask.Add(driverTagDriverTotalSize);
            Debuger.Log("+0.5+");
            DriverTag driverTagDriverTotalSizeString = FindTag(TagCodeDriverTotalSizeString);
            driverTagDriverTotalSizeString.TagDataValue = DiskSize(driveInfo.TotalSize);
            listTagsTask.Add(driverTagDriverTotalSizeString);
            Debuger.Log("+0.6+");
            DriverTag driverTagDriverCurrentSize = FindTag(TagCodeDriverCurrentSize);
            driverTagDriverCurrentSize.TagDataValue = Convert.ToDouble(driveInfo.TotalSize - driveInfo.TotalFreeSpace);
            listTagsTask.Add(driverTagDriverCurrentSize);
            Debuger.Log("+0.7+");
            DriverTag driverTagDriverCurrentSizeString = FindTag(TagCodeDriverCurrentSizeString);
            driverTagDriverCurrentSizeString.TagDataValue = DiskSize(driveInfo.TotalSize - driveInfo.TotalFreeSpace);
            listTagsTask.Add(driverTagDriverCurrentSizeString);
            Debuger.Log("+0.8+");
            // percent
            decimal freeSpacePercentage = (decimal)driveInfo.AvailableFreeSpace / driveInfo.TotalSize * 100;
            Debuger.Log("+0.9+");
            DriverTag driverTagProcentFreeSpaceSetPoint = FindTag(TagCodeProcentFreeSpaceSetPoint);
            driverTagProcentFreeSpaceSetPoint.TagDataValue = task.ProceentFreeSpace;
            listTagsTask.Add(driverTagProcentFreeSpaceSetPoint);
            Debuger.Log("+0.10+");
            DriverTag driverTagProcentFreeSpaceCurrent = FindTag(TagCodeProcentFreeSpaceCurrent);
            driverTagProcentFreeSpaceCurrent.TagDataValue = Math.Round(freeSpacePercentage, 0);
            listTagsTask.Add(driverTagProcentFreeSpaceCurrent);
            Debuger.Log("+0.11+");
            // action
            DriverTag driverTagActionTask = FindTag(TagCodeActionTask);
            driverTagActionTask.TagDataValue = Enum.GetName(typeof(ActionTask), task.Action);
            listTagsTask.Add(driverTagActionTask);
            Debuger.Log("+1+");
            // we check whether the free space exceeds the specified percentage.
            if (freeSpacePercentage > requiredFreeSpacePercentage)
            {
                Debuger.Log("+2+");
                // alarm off
                DriverTag driverTagStatusAlarm = FindTag(TagCodeStatusAlarm);
                driverTagStatusAlarm.TagDataValue = 0;
                listTagsTask.Add(driverTagStatusAlarm);

            }
            else
            {
                Debuger.Log("+3+");
                // alarm on
                DriverTag driverTagStatusAlarm = FindTag(TagCodeStatusAlarm);
                driverTagStatusAlarm.TagDataValue = 1;
                listTagsTask.Add(driverTagStatusAlarm);

                List<FilesDatabase> folders = new List<FilesDatabase>();
                List<FilesDatabase> foldersClear = new List<FilesDatabase>();
                List<FilesDatabase> foldersSort = new List<FilesDatabase>();
                List<DateTime> dateList = new List<DateTime>();
                DateTime dateMin = DateTime.MinValue;
                DateTime dateMax = DateTime.MinValue;


                // if a folder is specified
                if (Directory.Exists(directoryWatcher))
                {
                    Debuger.Log("+4+");
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

                                    // Определяем формат строки: "yyyyMMdd"
                                    string format = "yyyyMMdd";

                                    // Преобразуем строку в DateTime
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

                    Debuger.Log("+5+");
                }
                else
                {
                    Debuger.Log(String.Format(DriverDictonary.DirectoryDoesNotExist, directoryWatcher));
                    Debuger.Log("+6+");
                }


                switch (task.Action)
                {
                    case ActionTask.None:
                        Debuger.Log("+7+");
                        break;
                    case ActionTask.Delete:
                        Debuger.Log("+8+");
                        for (int f = 0; f < foldersSort.Count; f++)
                        {
                            FilesDatabase folder = foldersSort[f];

                            try
                            {
                                // Получаем информацию о диске
                                driveInfo = new DriveInfo(drive);
                                // percent
                                freeSpacePercentage = (decimal)driveInfo.AvailableFreeSpace / driveInfo.TotalSize * 100;

                                if (freeSpacePercentage > requiredFreeSpacePercentage)
                                {
                                    continue;
                                }
                                else
                                {
                                    DriverTag driverTagActionDate = FindTag(TagCodeActionDate);
                                    driverTagActionDate.TagDataValue = 1;
                                    listTagsTask.Add(driverTagActionDate);

                                    Directory.Delete(folder.PathFile, true);
                                    Debuger.Log(String.Format(DriverDictonary.DirectoryDelete,  folder.PathFile));
                                }
                            }
                            catch { }
                        }
                        break;
                    case ActionTask.CompressMove:
                        Debuger.Log("+9+");
                        for (int f = 0; f < foldersSort.Count; f++)
                        {
                            FilesDatabase folder = foldersSort[f];

                            try
                            {
                                // Получаем информацию о диске
                                driveInfo = new DriveInfo(drive);
                                // percent
                                freeSpacePercentage = (decimal)driveInfo.AvailableFreeSpace / driveInfo.TotalSize * 100;
                                if (freeSpacePercentage > requiredFreeSpacePercentage)
                                {
                                    continue;
                                }
                                else
                                {
                                    DriverTag driverTagActionDate = FindTag(TagCodeActionDate);
                                    driverTagActionDate.TagDataValue = 1;
                                    listTagsTask.Add(driverTagActionDate);

                                    string zipFilePath = Path.Combine(Path.GetDirectoryName(folder.PathFile), $"{Path.GetFileName(folder.PathFile)}.zip");
                                    string zipFileName = Path.GetFileName(zipFilePath);

                                    // Создаем ZIP архив
                                    ZipFile.CreateFromDirectory(folder.PathFile, zipFilePath);
                                    Debuger.Log(String.Format(DriverDictonary.DirectoryZip, folder.PathFile, zipFileName));

                                    File.Move(zipFilePath, Path.Combine(task.PathTo, zipFileName), true);
                                    Debuger.Log(String.Format(DriverDictonary.MoveZip, zipFileName, task.PathTo));

                                    Directory.Delete(folder.PathFile, true);
                                    Debuger.Log(String.Format(DriverDictonary.DirectoryDelete, folder.PathFile));
                                }
                            }
                            catch { }
                        }
                        break;
                }
            }
            Debuger.Log("+10+");
            listTagsTask = listTagsTask.Distinct().ToList();
            DriverTagReturn driverTagReturn = new DriverTagReturn();
            driverTagReturn.Return(listTagsTask);
            Debuger.Log("+11+");

            }
            catch (Exception ex)
            {
                Debuger.Log("Exp" + ex.Message.ToString());
            }
        }

        public static List<string> GetPhysicalDrivesNames()
        {
            List<string> disks = new List<string>();

            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady) // Проверяем, готов ли диск
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

        public static List<string> GetPhysicalDrives()
        {
            List<string> disks = new List<string>();

            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady) // Проверяем, готов ли диск
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

        public DriverTag FindTag(string code)
        {
            Debuger.Log(@$"{driverTags.Count.ToString()} === {listTagsTask.Count.ToString()}");

            DriverTag driverTag = new DriverTag();
            return driverTag = (DriverTag)driverTags.Find(x => x.TagCode == code);
        }

        public static string DiskSize(long totalBytes)
        {
            string result = string.Empty;
            // Определяем подходящую единицу измерения
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

        public void Validate(Task task)
        {

            List<string> diskInfo = DriverClient.GetPhysicalDrives();
            foreach (string info in diskInfo)
            {
                Debuger.Log(info);
            }
            Step(task);
        }

        #endregion Validate

        #region Log
        /// <summary>
        /// Getting logs
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(string msg);
        // transfer to the form and to the file in the Log folder
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

        ~DriverClient()
        {
            Dispose(false);
        }

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose

    }
}
