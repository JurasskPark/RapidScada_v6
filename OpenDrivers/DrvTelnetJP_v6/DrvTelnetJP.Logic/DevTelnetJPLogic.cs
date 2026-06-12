using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Data.Models;

namespace Scada.Comm.Drivers.DrvTelnetJP.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevTelnetJPLogic : DeviceLogic
    {
        #region Variable

        private readonly string driverCode;                         // driver code
        private readonly string configFileName;                     // configuration file name
        private readonly DrvTelnetJPConfig config;                  // device configuration
        private readonly NetworkInformation networkInformation;     // network information
        private bool writeLog;                                      // write driver log
        private List<Tag> deviceTags;                               // device tags
        private ushort countError;                                  // error count

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DevTelnetJPLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = false;
            ConnectionRequired = false;

            driverCode = DriverUtils.DriverCode;
            writeLog = false;
            deviceTags = new List<Tag>();

            networkInformation = new NetworkInformation
            {
                OnDebug = DebugerLog,
                OnDebugTag = DebugerTag,
                OnDebugTags = DebugerTags
            };

            string shortFileName = DrvTelnetJPConfig.GetFileName(deviceConfig.DeviceNum);
            configFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);

            config = new DrvTelnetJPConfig();
            if (config.Load(configFileName, out string errMsg))
            {
                writeLog = config.Log;
                deviceTags = config.DeviceTags ?? new List<Tag>();
            }
            else
            {
                DebugerLog(errMsg);
            }
        }

        /// <summary>
        /// Performs actions after adding the device to a communication line.
        /// <para>Выполняет действия после добавления КП на линию связи.</para>
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
        /// <para>Выполняет действия при завершении линии связи.</para>
        /// </summary>
        public override void OnCommLineTerminate()
        {
            networkInformation.StopTelnet();
        }

        /// <summary>
        /// Initializes the device tags.
        /// <para>Инициализирует теги КП.</para>
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
        /// <para>Выполняет сеанс связи с КП.</para>
        /// </summary>
        public override void Session()
        {
            base.Session();
            LastRequestOK = false;

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
                networkInformation.StopTelnet();
                InvalidateData();
            }

            SleepPollingDelay();
            FinishRequest();
            FinishSession();
        }

        #endregion Basic

        #region Request

        /// <summary>
        /// Requests data from the configured TCP endpoints.
        /// <para>Запрашивает данные с настроенных TCP-узлов.</para>
        /// </summary>
        private bool Request()
        {
            try
            {
                networkInformation.RunTelnet(deviceTags);
                return true;
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format(DriverDictonary.ErrorMessage, ex.Message));
                return false;
            }
        }

        #endregion Request

        #region Reinitialization

        /// <summary>
        /// Restarts the communication line.
        /// <para>Перезапускает линию связи.</para>
        /// </summary>
        private void ReinitializingDriver()
        {
            try
            {
                TeleCommand cmd = new TeleCommand
                {
                    CreationTime = DateTime.Now,
                    CommandID = ScadaUtils.GenerateUniqueID(DateTime.Now),
                    CmdVal = LineContext.CommLineNum,
                    CmdCode = CommCmdCode.RestartLine
                };

                CommContext.SendCommand(cmd, DriverDictonary.RestartLine);
            }
            catch (Exception ex)
            {
                DebugerLog(ex.Message);
            }
        }

        #endregion Reinitialization

        #region Debug Log

        /// <summary>
        /// Writes a message to the driver log.
        /// <para>Записывает сообщение в журнал драйвера.</para>
        /// </summary>
        public void DebugerLog(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
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
                Log.WriteInfo(ex.BuildErrorMessage(DriverDictonary.ErrorMessage));
                ReinitializingDriver();
            }
        }

        #endregion Debug Log

        #region Debug Tag

        /// <summary>
        /// Processes a returned tag.
        /// <para>Обрабатывает возвращенный тег.</para>
        /// </summary>
        public void DebugerTag(Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    ProcessEmptyTagResult();
                    return;
                }

                int indexTag = deviceTags.IndexOf(tag);
                if (!tag.TagEnabled || indexTag < 0)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(tag.TagCode))
                {
                    SetTagData(tag.TagCode, tag.TagVal, tag.TagStat);
                }
                else
                {
                    SetTagData(indexTag, tag.TagVal, tag.TagStat);
                }
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format(DriverDictonary.ErrorMessage, ex.Message));
                ReinitializingDriver();
            }
        }

        /// <summary>
        /// Processes returned tags.
        /// <para>Обрабатывает возвращенные теги.</para>
        /// </summary>
        public void DebugerTags(List<Tag> tags)
        {
            try
            {
                if (tags == null || tags.Count == 0)
                {
                    ProcessEmptyTagResult();
                    return;
                }

                for (int index = 0; index < tags.Count; index++)
                {
                    DebugerTag(tags[index]);
                }
            }
            catch (Exception ex)
            {
                DebugerLog(string.Format(DriverDictonary.ErrorMessage, ex.Message));
                ReinitializingDriver();
            }
        }

        /// <summary>
        /// Processes an empty tag result.
        /// <para>Обрабатывает пустой результат тегов.</para>
        /// </summary>
        private void ProcessEmptyTagResult()
        {
            countError++;
            DebugerLog(DriverDictonary.ErrorCount + " " + DriverUtils.NullToString(countError));

            if (countError >= 10)
            {
                ReinitializingDriver();
            }
        }

        #endregion Debug Tag

        #region Set Tag Data

        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// <para>Устанавливает значение, статус и формат указанного тега.</para>
        /// </summary>
        private void SetTagData(int tagIndex, object tagVal, int tagStat)
        {
            try
            {
                if (tagIndex < 0 || tagIndex >= DeviceTags.Count())
                {
                    return;
                }

                DeviceTag deviceTag = DeviceTags[tagIndex];
                SetDeviceTagFormat(deviceTag, tagVal);
                SetDeviceData(tagIndex, tagVal, tagStat);
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.BuildErrorMessage(DriverDictonary.ErrorSetData));
            }
        }

        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// <para>Устанавливает значение, статус и формат указанного тега.</para>
        /// </summary>
        private void SetTagData(string tagCode, object tagVal, int tagStat)
        {
            try
            {
                if (string.IsNullOrEmpty(tagCode) || DeviceTags.Count() == 0)
                {
                    return;
                }

                DeviceTag deviceTag = DeviceTags[tagCode];
                SetDeviceTagFormat(deviceTag, tagVal);
                SetDeviceData(tagCode, tagVal, tagStat);
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.BuildErrorMessage(DriverDictonary.ErrorSetData));
            }
        }

        /// <summary>
        /// Sets the device tag format according to the value type.
        /// <para>Устанавливает формат тега КП по типу значения.</para>
        /// </summary>
        private static void SetDeviceTagFormat(DeviceTag deviceTag, object tagVal)
        {
            if (tagVal is string)
            {
                deviceTag.DataType = TagDataType.Unicode;
                deviceTag.Format = TagFormat.String;
            }
            else if (tagVal is DateTime)
            {
                deviceTag.DataType = TagDataType.Double;
                deviceTag.Format = TagFormat.DateTime;
            }
            else
            {
                deviceTag.DataType = TagDataType.Double;
                deviceTag.Format = TagFormat.OffOn;
            }
        }

        /// <summary>
        /// Writes device data by tag index.
        /// <para>Записывает данные КП по индексу тега.</para>
        /// </summary>
        private void SetDeviceData(int tagIndex, object tagVal, int tagStat)
        {
            if (tagVal is string strVal)
            {
                DeviceData.SetUnicode(tagIndex, strVal, tagStat);
            }
            else if (tagVal is DateTime dtVal)
            {
                DeviceData.SetDateTime(tagIndex, dtVal, tagStat);
            }
            else
            {
                DeviceData.Set(tagIndex, Convert.ToDouble(tagVal), tagStat);
            }
        }

        /// <summary>
        /// Writes device data by tag code.
        /// <para>Записывает данные КП по коду тега.</para>
        /// </summary>
        private void SetDeviceData(string tagCode, object tagVal, int tagStat)
        {
            if (tagVal is string strVal)
            {
                DeviceData.SetUnicode(tagCode, strVal, tagStat);
            }
            else if (tagVal is DateTime dtVal)
            {
                DeviceData.SetDateTime(tagCode, dtVal, tagStat);
            }
            else
            {
                DeviceData.Set(tagCode, Convert.ToDouble(tagVal), tagStat);
            }
        }

        #endregion Set Tag Data
    }
}
