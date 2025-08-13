// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using FluentFTP;
using ManagerAssistant;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Lang;
using Scada.Log;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Scada.Comm.Drivers.DrvFtpJP.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevTextParserInDatabaseJPLogic : DeviceLogic
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevTextParserInDatabaseJPLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = false;
            ConnectionRequired = false;

            this.isDll = true;

            this.appDirs = commContext.AppDirs;

            this.deviceNum = deviceConfig.DeviceNum;
            this.driverCode = DriverUtils.DriverCode;

            string shortFileName = Project.GetFileName(deviceNum);
            this.pathProject = CommContext.AppDirs.ConfigDir;
            this.configFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);
            this.writeLogDriver = true;

            // load configuration
            project = new Project();
            if (!project.Load(configFileName, out string errMsg))
            {
                LogDriver(errMsg);
            }

            // manager
            Manager.IsDll = this.isDll;
            Manager.DeviceNum = this.deviceNum;
            Manager.Project = project;
            Manager.ProjectPath = configFileName;
            Manager.LogPath = this.pathLog = this.appDirs.LogDir;


            this.writeLogDriver = project.DebugerSettings.LogWrite;

            driverClient = new DriverClient(project);
        }

        #region Variables
        public readonly bool isDll;                             // application or dll
        private readonly string pathLog;                        // the path log
        private readonly AppDirs appDirs;                       // the application directories
        private readonly string driverCode;                     // the driver code
        private readonly int deviceNum;                         // the device number
        private readonly Project project;                       // the device configuration
        private readonly string pathProject;                    // the path device configuration
        private string configFileName;                          // the configuration file name
        private DriverClient driverClient;                      // client
        private bool validateLicense;                           // the validate license
        private int countFilesConfig;                           // count config files

        private List<DriverTag> deviceTags;                     // tags

        private bool writeLogDriver;                            // write driver log

        private static DateTime defaultDate = new DateTime(2000, 1, 1, 0, 0, 0);

        private Dictionary<int, string> ListRemoteFilesDownload = new Dictionary<int, string>();
        private object logLock = new object();
        #endregion Variables


        /// <summary>
        /// Performs actions after adding the device to a communication line.
        /// </summary>
        public override void OnCommLineStart()
        {
            base.OnCommLineStart();

            LogDriver(Locale.IsRussian ?
                       "Запуск драйвера" :
                       "Running the driver");

            LogDriver("[Driver " + driverCode + "]");
            LogDriver("[Version " + DriverUtils.Version + "]");
            LogDriver("[" + DriverDictonary.StartDriver + "]");
            LogDriver("[" + DriverDictonary.Delay + "][" + DriverUtils.NullToString(PollingOptions.Delay) + "]");
            LogDriver("[" + DriverDictonary.Timeout + "][" + DriverUtils.NullToString(PollingOptions.Timeout) + "]");
            LogDriver("[" + DriverDictonary.Period + "][" + DriverUtils.NullToString(PollingOptions.Period) + "]");
        }

        /// <summary>
        /// Performs actions after stopping the device on the communication line.
        /// </summary>
        public override void OnCommLineTerminate()
        {
            base.OnCommLineTerminate();

            driverClient.Dispose();

            LogDriver(Locale.IsRussian ?
                       "Останов драйвера" :
                       "Stopping the driver");
        }

        /// <summary>
        /// Initializes the device tags.
        /// </summary>
        public override void InitDeviceTags()
        {
            
        }

        /// <summary>
        /// Performs a communication session with the device.
        /// </summary>
        public override void Session()
        {
            base.Session();

            LastRequestOK = false;

            if (true)
            {
                // request data
                int tryNum = 0;

                while (RequestNeeded(ref tryNum))
                {
                    try
                    {

                        DebugerReturn.OnDebug = new DebugerReturn.DebugData(LogDriver);
                        DebugerFilesReturn.OnDebug = new DebugerFilesReturn.DebugData(LogDriverFiles);

                        if (driverClient.Connect())
                        {
                            driverClient.Process();
                            driverClient.Disconnect();
                            driverClient.Dispose();
                        }

                        LastRequestOK = true;

                        FinishRequest();
                        tryNum++;
                    }
                    catch
                    {
                        LastRequestOK = false;
                    }
                }


                if (!LastRequestOK && !IsTerminated)
                {
                    InvalidateData();
                }

                SleepPollingDelay();
            }

            // calculate session stats
            FinishRequest();
            FinishSession();
        }

        /// <summary>
        /// Log Write Driver
        /// </summary>
        /// <param name="text">Message</param>
        private void LogDriver(string text)
        {
            if (text == string.Empty || text == "" || text == null)
            {
                return;
            }
            if (writeLogDriver)
            {
                Log.WriteAction(text);
                Console.WriteLine(text);
            }
        }


        /// <summary>
        /// Log Write Driver
        /// </summary>
        /// <param name="text">Message</param>
        public void LogDriverFiles(FtpProgress progress, string direction)
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

        /// <summary>
        /// Update Log
        /// </summary>
        /// <param name="text">Message</param>
        private void UpdateLog(int fileIndex, string text, string findText)
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
                // Логирование ошибки
                Console.WriteLine($"Ошибка при обновлении лога: {ex.Message}");
            }

        }

    }
}
