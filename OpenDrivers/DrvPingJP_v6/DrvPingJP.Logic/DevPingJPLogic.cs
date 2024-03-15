using DrvOPCClassicJP.Shared;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvPingJP;
using Scada.Data.Models;
using Scada.Lang;
using System.Linq.Expressions;
using System.Reflection;

namespace Scada.Comm.Drivers.DrvPingJPLogic.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevPingJPLogic : DeviceLogic
    {
        /// <summary>
        /// Supported tag types.
        /// </summary>
        private enum TagType { Number, String, DateTime };

        private readonly AppDirs appDirs;                       // the application directories
        private readonly string driverCode;                     // the driver code
        private readonly int deviceNum;                         // the device number
        private readonly string configFileName;                 // the configuration file name
        private readonly DrvPingJPConfig config;                // the device configuration  
        private readonly NetworkInformation networkInformation; // network (ping)
        private readonly bool writeLog;                         // write log
        private readonly int pingMode;                          // type ping
        private List<Tag> deviceTags;                           // tags
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

            this.deviceNum = deviceConfig.DeviceNum;
            this.driverCode = DriverUtils.DriverCode;

            this.networkInformation = new NetworkInformation();
            this.networkInformation.OnDebug = new NetworkInformation.DebugData(DebugerLog);
            this.networkInformation.OnDebugTag = new NetworkInformation.DebugTag(DebugerTag);
            this.networkInformation.OnDebugTags = new NetworkInformation.DebugTags(DebugerTags);

            string shortFileName = DrvPingJPConfig.GetFileName(deviceNum);
            configFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);
            
            // load configuration
            config = new DrvPingJPConfig();
            if (config.Load(configFileName, out string errMsg))
            {
                writeLog = config.Log;
                pingMode = config.Mode;
                deviceTags = config.DeviceTags;
            }
            else
            {
                DebugerLog(errMsg);
            }
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
        }


        /// <summary>
        /// Performs actions when terminating a communication line.
        /// </summary>
        public override void OnCommLineTerminate()
        {
            if(config.Mode == 1)
            {
                networkInformation.StopPingSynchronous();
            }
        }

        /// <summary>
        /// Initializes the device tags.
        /// </summary>
        public override void InitDeviceTags()
        {
            if (config == null)
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
                networkInformation.StopPingSynchronous();
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
                #region Ping
                if (pingMode == 0)
                {
                    #region Synchronous
                    networkInformation.RunPingSynchronous(deviceTags);
                    return true;
                    #endregion Synchronous
                }
                else if (pingMode == 1)
                {
                    #region Asynchronous
                    networkInformation.RunPingAsynchronous(deviceTags);
                    return true;
                    #endregion Asynchronous
                }
                #endregion Ping 
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
        public void DebugerTag(Tag tag)
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

                if (tag.TagEnabled == true) // enabled
                {
                    if (tag.TagCode != string.Empty)
                    {
                        SetTagData(tag.TagCode, tag.TagVal, tag.TagStat);
                    }
                    else
                    {
                        SetTagData(indexTag, tag.TagVal, tag.TagStat);
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
        public void DebugerTags(List<Tag> tags)
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

                    Tag tmpTag = tags[index];
                    int indexTag = deviceTags.IndexOf(tags[index]);

                    if (tmpTag == null || tmpTag.TagEnabled == false || indexTag < 0)
                    {
                        continue;
                    }

                    if (tmpTag.TagEnabled == true) // enabled
                    {
                        if (tmpTag.TagCode != string.Empty)
                        {
                            SetTagData(tmpTag.TagCode, tmpTag.TagVal, tmpTag.TagStat);
                        }
                        else
                        {
                            SetTagData(indexTag, tmpTag.TagVal, tmpTag.TagStat);
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
        private void SetTagData(int tagIndex, object tagVal, int tagStat)
        {
            try
            {
                if (DeviceTags.Count() > 0)
                {
                    DeviceTag deviceTag = DeviceTags[tagIndex];
                    

                    if (tagVal is string strVal)
                    {
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.Format = TagFormat.String;
                        try { base.DeviceData.SetUnicode(tagIndex, strVal, tagStat); } catch { }
                    }
                    else if (tagVal is DateTime dtVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.DateTime;
                        try { base.DeviceData.SetDateTime(tagIndex, dtVal, tagStat); } catch { }
                    }
                    else
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.OffOn;
                        try { base.DeviceData.Set(tagIndex, Convert.ToDouble(tagVal), tagStat); } catch { }
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
        private void SetTagData(string tagCode, object tagVal, int tagStat)
        {
            try
            {
                if (DeviceTags.Count() > 0)
                {
                    DeviceTag deviceTag = DeviceTags[tagCode];

                    if (tagVal is string strVal)
                    {
                        deviceTag.DataType = TagDataType.Unicode;
                        deviceTag.Format = TagFormat.String;
                        try { base.DeviceData.SetUnicode(tagCode, strVal, tagStat); } catch { }
                    }
                    else if (tagVal is DateTime dtVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.DateTime;
                        try { base.DeviceData.SetDateTime(tagCode, dtVal, tagStat); } catch { }
                    }
                    else
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.OffOn;
                        try { base.DeviceData.Set(tagCode, Convert.ToDouble(tagVal), tagStat); } catch { }
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

