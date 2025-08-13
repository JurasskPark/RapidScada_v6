using DebugerLog;
using Scada.Comm.Drivers.DrvFtpJP;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrvFtpJP.Shared.FilesDirectorys
{
    public class FilesDirectoriesInformation
    {
        public FilesDirectoriesInformation()
        {
            this.Name = string.Empty;
            this.FullName = string.Empty;
            this.Type = FilesDirectoriesType.None;
            this.Size = 0;
            this.Date = DateTime.MinValue;
            this.Format = string.Empty;
        }

        public FilesDirectoriesInformation(string name, string fullname, FilesDirectoriesType type, long size, DateTime date, string format)
        {
            this.Name = name;
            this.FullName = fullname;
            this.Type = type;
            this.Size = size;
            this.Date = date;
            this.Format = format;
        }

        public enum FilesDirectoriesType : int
        {
            None,
            File,
            Directory,
            Link,
        }

        public string Name { get; set; }

        public string FullName { get; set; }

        public FilesDirectoriesType Type { get; set; }


        private long size;
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
                this.size = value;
                this.SizeString = DriverUtils.DiskSize(this.size);
            }
        }
        public DateTime Date { get; set; }

        public string SizeString { get; set; }

        public string Format { get; set; }

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

        public static List<FilesDirectoriesInformation> GetDirectoriesAndFiles(string path)
        {
            List<FilesDirectoriesInformation> list = new List<FilesDirectoriesInformation>();

            try
            {
                var directories = from dir in Directory.GetDirectories(path) select (new DirectoryInfo(dir));
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
                    catch { }
                }

                var files = from file in Directory.GetFiles(path) select (new FileInfo(file));
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
                    catch { }
                }

                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }
    }
}
