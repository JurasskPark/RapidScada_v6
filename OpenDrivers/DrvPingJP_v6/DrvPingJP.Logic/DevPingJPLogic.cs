using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvPingJP;
using Scada.Data.Models;
using System.Globalization;

namespace Scada.Comm.Drivers.DrvPingJPLogic.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevPingJPLogic : DeviceLogic
    {
        public readonly bool isDll;                             // application or dll
        private readonly string pathLog;                        // the path log
        private readonly AppDirs appDirs;                       // the application directories
        private readonly string driverCode;                     // the driver code
        private readonly int deviceNum;                         // the device number
        private readonly string pathProject;                    // the configuration file name
        private readonly Project project;                       // the device configuration  
        private DriverClient driverClient;                      // thie driver client
        private readonly bool writeLog;                         // write log
        private readonly int pingMode;                          // type ping
        private List<DriverTag> deviceTags;                     // tags
        private ushort countError;                              // count error


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevPingJPLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = true;
            ConnectionRequired = false;
            writeLog = false;

            this.isDll = true;
            this.pathLog = CommContext.AppDirs.LogDir;
            this.deviceNum = deviceConfig.DeviceNum;
            this.driverCode = DriverUtils.DriverCode;

            string shortFileName = Project.GetFileName(deviceNum);
            pathProject = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);
            
            // load configuration
            project = new Project();
            if (project.Load(pathProject, out string errMsg))
            {
                writeLog = project.DebugerSettings.LogWrite;
                pingMode = project.Mode;
                deviceTags = project.DeviceTags;
            }
            else
            {
                DebugerLog(errMsg);
            }

            DebugerReturn.OnDebug = new DebugerReturn.DebugData(DebugerLog);
            DriverTagReturn.OnDebug = new DriverTagReturn.DebugData(DebugerTags);

            this.driverClient = new DriverClient(project);
            this.deviceTags = new List<DriverTag>();

            // manager
            Manager.IsDll = this.isDll;
            Manager.DeviceNum = this.deviceNum;
            Manager.PathProject = this.pathProject;
            Manager.Project = this.project;
            Manager.PathLog = this.pathLog;
        }



        /// <summary>
        /// Performs actions after adding the device to a communication line.
        /// </summary>
        public override void OnCommLineStart()
        {
            DebugerLog("[Driver " + driverCode + "]");
            DebugerLog("[Version " + DriverUtils.Version + "]");
            DebugerLog("[" + DriverDictonary.StartDriver + "]");
            DebugerLog("[" + DriverDictonary.Delay + "][" + DriverUtils.NullToString(PollingOptions.Delay) + "]");
            DebugerLog("[" + DriverDictonary.Timeout + "][" + DriverUtils.NullToString(PollingOptions.Timeout) + "]");
            DebugerLog("[" + DriverDictonary.Period + "][" + DriverUtils.NullToString(PollingOptions.Period) + "]");
            DebugerLog("[" + DriverDictonary.CulturеName + "][" + DriverUtils.NullToString(CultureInfo.CurrentCulture.Name) + "]");
        }

        /// <summary>
        /// Performs actions when terminating a communication line.
        /// </summary>
        public override void OnCommLineTerminate()
        {
            driverClient.Stop();
            driverClient.Dispose();
        }

        /// <summary>
        /// Initializes the device tags.
        /// </summary>
        public override void InitDeviceTags()
        {
            if (project == null)
            {
                DebugerLog(DriverDictonary.ProjectNo);
                return;
            }

            foreach (CnlPrototypeGroup group in CnlPrototypeFactory.GetCnlPrototypeGroups(deviceTags))
            {
                DeviceTags.AddGroup(group.ToTagGroup());
            }
        }

        /// <summary>
        /// Performs a communication session with the device.
        /// </summary>
        public override void Session()
        {
            base.Session();

            LastRequestOK = false;

            // request data
            int tryNum = 0;

            while (RequestNeeded(ref tryNum))
            {
                if (Request())
                {
                    LastRequestOK = true;
                }
 
                FinishRequest();
                tryNum++;
            }

            if (!LastRequestOK && !IsTerminated)
            {
                InvalidateData();
            }

            SleepPollingDelay();
            // calculate session stats
            FinishRequest();
            FinishSession();
        }

        #region ReInitializingOPCserver

        private void ReinitializingDriver()
        {
            try
            {
                TeleCommand cmd = new TeleCommand();
                cmd.CreationTime = DateTime.Now;
                cmd.CommandID = ScadaUtils.GenerateUniqueID(DateTime.Now);
                cmd.CmdVal = LineContext.CommLineNum;
                cmd.CmdCode = CommCmdCode.RestartLine;
                CommContext.SendCommand(cmd, DriverDictonary.RestartLine);
            }
            catch (Exception ex)
            {
                DebugerLog(ex.Message.ToString());
            }
        }
        #endregion ReInitializingOPCserver

        #region  Request

        /// <summary>
        /// Requests data from the database.
        /// </summary>
        private bool Request()
        {
            try
            {
                driverClient.Ping();

                return true;
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format(DriverDictonary.ErrorMessage, ex.Message));
                return false;
            }
        }

        #endregion  Request

        #region Debug Log
        /// <summary>
        /// Getting logs
        /// </summary>
        public void DebugerLog(string text)
        {
            try
            {
                if (text == string.Empty)
                {
                    return;
                }

                if (writeLog)
                {
                    Log.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format(DriverDictonary.ErrorMessage, ex.Message));
                ReinitializingDriver();
            }
        }
        #endregion Debug Log

        #region Debug Tag
        public void DebugerTag(DriverTag tag)
        {
            try
            {
                if (tag == null)
                {
                    countError++;
                    DebugerLog(DriverDictonary.ErrorCount + " " + DriverUtils.NullToString(countError));
                    if (countError >= 10)
                    {
                        ReinitializingDriver();
                    }
                    return;
                }

                int indexTag = deviceTags.IndexOf(tag);

                if (tag.Enabled == true) // enabled
                {
                    if (tag.Code != string.Empty)
                    {
                        SetTagData(tag.Code, tag.Val, tag.Stat);
                    }
                    else
                    {
                        SetTagData(indexTag, tag.Val, tag.Stat);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format(DriverDictonary.ErrorMessage, ex.Message));
                ReinitializingDriver();
            }
        }
        #endregion Debug Tag

        #region Debug Tags
        public void DebugerTags(List<DriverTag> tags)
        {
            try
            {
                if (tags == null || tags.Count == 0)
                {
                    countError++;
                    DebugerLog(DriverDictonary.ErrorCount + " " + DriverUtils.NullToString(countError));
                    if (countError >= 10)
                    {
                        ReinitializingDriver();
                    }
                    return;
                }

                for (int index = 0; index < tags.Count; index++)
                {

                    DriverTag tmpTag = tags[index];
                    int indexTag = deviceTags.IndexOf(tags[index]);

                    if (tmpTag == null || tmpTag.Enabled == false || indexTag < 0)
                    {
                        continue;
                    }

                    if (tmpTag.Enabled == true) // enabled
                    {
                        if (tmpTag.Code != string.Empty)
                        {
                            SetTagData(tmpTag.Code, tmpTag.Val, tmpTag.Stat);
                        }
                        else
                        {
                            SetTagData(indexTag, tmpTag.Val, tmpTag.Stat);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format(DriverDictonary.ErrorMessage, ex.Message));
                ReinitializingDriver();
            }
        }
        #endregion Debug Tags

        #region Set Tag Data

        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// </summary>
        private void SetTagData(int tagIndex, object val, int stat)
        {
            try
            {
                if (DeviceTags.Count() > 0)
                {
                    DeviceTag deviceTag = DeviceTags[tagIndex];
                    

                    if (val is string strVal)
                    {
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.Format = TagFormat.String;
                        try { base.DeviceData.SetUnicode(tagIndex, strVal, stat); } catch { }
                    }
                    else if (val is DateTime dtVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.DateTime;
                        try { base.DeviceData.SetDateTime(tagIndex, dtVal, stat); } catch { }
                    }
                    else
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.OffOn;
                        try { base.DeviceData.Set(tagIndex, Convert.ToDouble(val), stat); } catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.BuildErrorMessage(DriverDictonary.ErrorSetData));
            }
        }

        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// </summary>
        private void SetTagData(string code, object val, int stat)
        {
            try
            {
                if (DeviceTags.Count() > 0)
                {
                    DeviceTag deviceTag = DeviceTags[code];

                    if (val is string strVal)
                    {
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.Format = TagFormat.String;
                        try { base.DeviceData.SetUnicode(code, strVal, stat); } catch { }
                    }
                    else if (val is DateTime dtVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.DateTime;
                        try { base.DeviceData.SetDateTime(code, dtVal, stat); } catch { }
                    }
                    else
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.OffOn;
                        try { base.DeviceData.Set(code, Convert.ToDouble(val), stat); } catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.BuildErrorMessage(DriverDictonary.ErrorSetData));
            }
        }

        #endregion Set Tag Data

    }
}

