using FluentFTP;
using ManagerAssistant;
using Scada.Comm.Config;
using Scada.Comm.Drivers.DrvFtpJP;

namespace DrvFtpJP.Console
{
    public class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static async Task Main(string[] args)
        {
            isDll = false;

            pathLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log");
            pathProject = AppDomain.CurrentDomain.BaseDirectory;

            LineConfig lineConfig = new LineConfig();
            DeviceConfig deviceConfig = new DeviceConfig();
            deviceConfig.DeviceNum = 0;
            CreateDevice(lineConfig, deviceConfig);
            Session();

        }

        private static bool isDll;                              // application or dll
        private static string pathLog;                          // path log
        private static int deviceNum;                           // the device number
        private static string driverCode;                       // the driver code
        private static Project project;                         // the device configuration
        private static string pathProject;                      // the path device configuration
        private static string configFileName;                   // the configuration file name
        private static DriverClient driverClient;               // client
        private static int countFilesConfig;                    // count config files

        private static Dictionary<int, string> ListRemoteFilesDownload = new Dictionary<int, string>();
        private static object logLock = new object();


        /// <summary>
        /// Creates a new device.
        /// </summary>
        public static void CreateDevice(LineConfig lineConfig, DeviceConfig deviceConfig)
        {          
            deviceNum = deviceConfig.DeviceNum;
            driverCode = DriverUtils.DriverCode;

            string shortFileName = Project.GetFileName(deviceNum);
            configFileName = Path.Combine(pathProject, shortFileName);

            // load configuration
            project = new Project();
            if (!project.Load(configFileName, out string errMsg))
            {
                LogDriver(errMsg);
            }

            //project.DeviceNum = deviceNum;

            // manager
            Manager.IsDll = isDll;
            Manager.DeviceNum = deviceNum;
            Manager.Project = project;
            Manager.LogPath = pathLog;
            Manager.ProjectPath = configFileName;

            countFilesConfig = 0;

            driverClient = new Scada.Comm.Drivers.DrvFtpJP.DriverClient(project);
        }

        /// <summary>
        /// Log Write Driver
        /// </summary>
        /// <param name="text">Message</param>
        public static void LogDriver(string text)
        {
            if (text == string.Empty || text == "" || text == null)
            {
                return;
            }

            System.Console.WriteLine(text + Environment.NewLine);
        }


        /// <summary>
        /// Tag Write Driver
        /// </summary>
        /// <param name="text">Message</param>
        private static void PollTagGet(List<DriverTag> tags)
        {
            for (int t = 0; t < tags.Count; t++)
            {
                //if (tags[t].TagFormatData != DriverTag.FormatTag.Table)
                //{
                //    if (tags[t] == null || tags[t].TagEnabled == false)
                //    {
                //        continue;
                //    }

                //    DriverTag.TagToString(tags[t]);

                    //DeviceTag findTag = DeviceTags.Where(r => r.Code == tags[t].TagCode).FirstOrDefault();
                    //SetTagData(findTag, tags[t].TagDataValue, tags[t].TagNumberDecimalPlaces);
                //}
                //else
                //{
                //    ParseDataTable(tags[t]);
                //}
            }
        }

        /// <summary>
        /// Performs a communication session with the device.
        /// </summary>
        public static void Session()
        {
            if (true)
            {
                // request data
                int tryNum = 0;

                while (tryNum == 0)
                {
                    try
                    {
                        DebugerReturn.OnDebug = new DebugerReturn.DebugData(LogDriver);
                        DebugerFilesReturn.OnDebug = new DebugerFilesReturn.DebugData(LogDriverFiles);
                        DriverTagReturn.OnDebug = new DriverTagReturn.DebugData(PollTagGet);

                        driverClient = new DriverClient(project);
                        if (driverClient.Connect())
                        {
                            driverClient.Process();
                            driverClient.Disconnect();
                            driverClient.Dispose();
                        }

                        tryNum++;
                    }
                    catch { }
                } 
            }
        }

        /// <summary>
        /// Log Write Driver
        /// </summary>
        /// <param name="text">Message</param>
        public static void LogDriverFiles(FtpProgress progress, string direction)
        {
            string text = string.Empty;
            string findText = $"[{progress.LocalPath}]";
            if (direction == "<-")
            {
                text = $"[{progress.LocalPath}] {direction} [{progress.RemotePath}] " +
                             $"[{DriverUtils.SpeedSize((long)progress.TransferSpeed)}] " +
                             $"[{DriverUtils.DiskSize((long)progress.TransferredBytes)}] ";
            }
            else if (direction == "->")
            {
                text = $"[{progress.LocalPath}] {direction} [{progress.RemotePath}] " +
                             $"[{DriverUtils.SpeedSize((long)progress.TransferSpeed)}] " +
                             $"[{DriverUtils.DiskSize((long)progress.TransferredBytes)}] " +
                             $"[{progress.Progress:F2} %] ";

            }

            UpdateLog(progress.FileIndex, text, findText);
        }

        private static void UpdateLog(int fileIndex, string text, string findText)
        {
            try
            {
                if (ListRemoteFilesDownload.TryGetValue(fileIndex, out _))
                {
                    // Обновление существующей записи
                    LogDriver(text + " " + findText);
                }
                else
                {
                    // Добавление новой записи
                    LogDriver(text);
                    ListRemoteFilesDownload[fileIndex] = text;
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}