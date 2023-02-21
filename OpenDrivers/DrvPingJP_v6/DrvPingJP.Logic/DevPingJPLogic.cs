// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvPingJP;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Lang;

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
        private readonly DrvPingJPConfig config;                  // the device configuration
        private string configFileName;                          // the configuration file name
        private bool wrtiteLog;                                 // write log
        private List<Tag> deviceTags;                           // tags


        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevPingJPLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = true;
            ConnectionRequired = false;

            this.deviceNum = deviceConfig.DeviceNum;
            this.driverCode = DriverUtils.DriverCode;
            string shortFileName = DrvPingJPConfig.GetFileName(deviceNum);
            configFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);
            wrtiteLog = false;
            config = new DrvPingJPConfig();
        }


        /// <summary>
        /// Performs actions after adding the device to a communication line.
        /// </summary>
        public override void OnCommLineStart()
        {
            // load configuration
            DrvPingJPConfig config = new DrvPingJPConfig();

            if (config.Load(configFileName, out string errMsg))
            {
                wrtiteLog = config.Log;
            }
            else
            {
                Log.WriteLine(errMsg);
            }
        }

        public override void OnCommLineTerminate()
        {
            try
            {
                Thread.Sleep(100);
                // after a time delay, we cancel the task
                cancelTokenSource.Cancel();

                // we are waiting for the completion of the task
                Thread.Sleep(100);
            }
            finally
            {
                cancelTokenSource.Dispose();
            }

            base.OnCommLineTerminate();
        }

        /// <summary>
        /// Initializes the device tags.
        /// </summary>
        public override void InitDeviceTags()
        {
            if (config == null)
            {
                Log.WriteLine(Locale.IsRussian ?
                       "Количество тегов не было получено т.к. конфигурационный файл не был загружен" :
                       "The number of tags was not received because the configuration file was not loaded");
                return;
            }

            if (config.Load(configFileName, out string errMsg))
            {
                deviceTags = config.DeviceTags;

                foreach (CnlPrototypeGroup group in CnlPrototypeFactory.GetCnlPrototypeGroups(deviceTags))
                {
                    DeviceTags.AddGroup(group.ToTagGroup());
                }
            }
            else
            {
                Log.WriteLine(errMsg);
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

        /// <summary>
        /// Requests data from the database.
        /// </summary>
        private bool Request()
        {
            try
            {
                for (int index = 0; index < deviceTags.Count; ++index)
                {
                    Tag tmpTag = deviceTags[index];
                    int indexTag = deviceTags.IndexOf(deviceTags[index]);

                    if (tmpTag == null || tmpTag.TagEnabled == false)
                    {
                        continue;
                    }

                    #region Ping
                    if (tmpTag.TagEnabled == true)
                    {
                        try
                        {
                            CancellationToken token = cancelTokenSource.Token;

                            Task task = new Task(() =>
                            {
                                bool statusIP = NetworkInformationExtensions.Pinger(tmpTag.TagIPAddress, out string result);

                                if (wrtiteLog && result != string.Empty)
                                {
                                    Log.WriteLine(result);
                                }

                                if (statusIP)
                                {
                                    if (tmpTag.TagCode != string.Empty)
                                    {
                                        SetTagData(tmpTag.TagCode, 1, 1);
                                    }
                                    else
                                    {
                                        SetTagData(indexTag, 1, 1);
                                    }
                                }
                                else
                                {
                                    if (tmpTag.TagCode != string.Empty)
                                    {
                                        SetTagData(tmpTag.TagCode, 0, 1);
                                    }
                                    else
                                    {
                                        SetTagData(indexTag, 0, 1);
                                    }
                                }

                                if (token.IsCancellationRequested)
                                {
                                    token.ThrowIfCancellationRequested(); // generating an exception
                                }

                            }, token);

                            try
                            {
                                task.Start();
                            }
                            catch { }

                        }
                        catch { }
                    }
                    #endregion Ping

                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Ошибка при выполнении: {0}" :
                    "Error executing: {0}", ex.Message));
                return false;
            }
        }

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
                Log.WriteInfo(ex.BuildErrorMessage(Locale.IsRussian ?
                    "Ошибка при установке данных тега" :
                    "Error setting tag data"));
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
                Log.WriteInfo(ex.BuildErrorMessage(Locale.IsRussian ?
                    "Ошибка при установке данных тега" :
                    "Error setting tag data"));
            }
        }
    }
}
