using Scada.Client;
using Scada.Data.Models;
using Scada.Data.Tables;
using Scada.Lang;
using Scada.Log;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;


namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    internal class DriverClient
    {
        private readonly ILog log;                                          // the application log

        private readonly string pathProject;                                // path project
        private readonly Project project;                                   // configuration
     
        public DriverClient(string path, Project project)
        {
            this.pathProject = path;
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
                List<string> disks = GetPhysicalDrives();
                foreach (var disk in disks)
                {
                    Console.WriteLine(disk);
                }


                // Задаем необходимые параметры
                string drive = "C:\\"; // Укажите диск для проверки
                decimal requiredFreeSpacePercentage = 10; // Процент свободного пространства
                string directoryToCompress = @"C:\example"; // Укажите каталог для сжатия

                // Получаем информацию о диске
                DriveInfo driveInfo = new DriveInfo(drive);
                decimal freeSpacePercentage = (decimal)driveInfo.AvailableFreeSpace / driveInfo.TotalSize * 100;

                // Проверяем, превышает ли свободное пространство заданный процент
                if (freeSpacePercentage > requiredFreeSpacePercentage)
                {
                    Console.WriteLine($"Свободное пространство на диске {drive} составляет {freeSpacePercentage:F2}%.");

                    // Если указан каталог, его нужно сжать в архив
                    if (Directory.Exists(directoryToCompress))
                    {
                        string zipFilePath = Path.Combine(Path.GetDirectoryName(directoryToCompress),
                            $"{Path.GetFileName(directoryToCompress)}.zip");

                        // Создаем ZIP архив
                        ZipFile.CreateFromDirectory(directoryToCompress, zipFilePath);
                        Console.WriteLine($"Каталог '{directoryToCompress}' был успешно сжат в архив '{zipFilePath}'.");
                    }
                    else
                    {
                        Console.WriteLine($"Указанный каталог '{directoryToCompress}' не существует.");
                    }
                }
                else
                {
                    Console.WriteLine($"Свободное пространство на диске {drive} составляет {freeSpacePercentage:F2}%. Необходимо больше пространства.");
                }

            }
            catch { }
        }

        static List<string> GetPhysicalDrives()
        {
            List<string> disks = new List<string>();

            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady) // Проверяем, готов ли диск
                    {
                        disks.Add($"{drive.Name} - {drive.DriveType} - {drive.VolumeLabel} - {drive.TotalSize / (1024 * 1024 * 1024)} ГБ");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении информации о дисках: {ex.Message}");
            }

            return disks;
        }
        #endregion Process

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
