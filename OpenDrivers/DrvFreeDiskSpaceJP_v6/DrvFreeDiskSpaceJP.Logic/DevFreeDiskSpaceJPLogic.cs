// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvFreeDiskSpaceJP;
using Scada.Data.Const;
using Scada.Lang;
using System.Data;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJPLogic.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevFreeDiskSpaceJPLogic : DeviceLogic
    {
        public readonly bool isDll;                             // application or dll
        private readonly string pathLog;                        // the path log
        private readonly string languageDir;                    // the language directory

        private readonly AppDirs appDirs;                       // the application directories
        private readonly string driverCode;                     // the driver code
        private readonly int deviceNum;                         // the device number

        private readonly Project project;                       // the driver configuration
        private readonly string pathProject;                    // the path driver configuration
        private string projectFileName;                         // the configuration file name
        private DriverClient driverClient;                      // client
        private List<DriverTag> driverTags;                     // tags       
        private bool writeLogDriver;                            // write driver log


        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DevFreeDiskSpaceJPLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = true;
            ConnectionRequired = false;

            this.isDll = true;
            this.writeLogDriver = true;

            this.appDirs = commContext.AppDirs;
            this.deviceNum = deviceConfig.DeviceNum;
            this.driverCode = DriverUtils.DriverCode;

            this.languageDir = commContext.AppDirs.LangDir;
            this.pathLog = commContext.AppDirs.LogDir;

            // load configuration
            string shortFileName = Project.GetFileName(deviceNum);
            this.pathProject = CommContext.AppDirs.ConfigDir;
            this.projectFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);
            this.project = new Project();
            if (!this.project.Load(pathProject, out string errMsg))
            {
                LogDriver(errMsg);
            }

            // manager
            Manager.IsDll = this.isDll;
            Manager.DeviceNum = this.deviceNum;
            Manager.PathProject = this.pathProject;
            Manager.Project = this.project;
            Manager.PathLog = this.pathLog;

            this.writeLogDriver = project.DebugerSettings.LogWrite;

            driverClient = new DriverClient(pathProject, project);
        }


        /// <summary>
        /// Performs actions after adding the device to a communication line.
        /// <para>Выполняет действия после добавления устройства к линии связи.</para>
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

            List<string> diskInfo = DriverClient.GetPhysicalDrives();
            foreach(string info in diskInfo)
            {
                LogDriver(info);
            }         
        }

        /// <summary>
        /// Performs actions after stopping the device on the communication line.
        /// <para>Выполняет действия после отключения устройства от линии связи.</para>
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
        /// <para>Инициализирует теги устройства.</para>
        /// </summary>
        public override void InitDeviceTags()
        {
            if (project == null)
            {
                LogDriver(Locale.IsRussian ?
                       "Количество тегов не было получено т.к. конфигурационный файл не был загружен" :
                       "The number of tags was not received because the configuration file was not loaded");
                return;
            }

            if (project.Load(projectFileName, out string errMsg))
            {
                driverTags = CnlPrototypeFactory.GetDriverTags(project);

                foreach (CnlPrototypeGroup group in CnlPrototypeFactory.GetCnlPrototypeGroups(project))
                {
                    DeviceTags.AddGroup(group.ToTagGroup());
                }
            }
            else
            {
                LogDriver(errMsg);
            }
        }

        /// <summary>
        /// Performs a communication session with the device.
        /// <para>Выполняет сеанс связи с устройством.</para>
        /// </summary>
        public override void Session()
        {
            base.Session();

            LastRequestOK = false;

            // request data
            int tryNum = 0;

            while (RequestNeeded(ref tryNum))
            {
                try
                {
                    DebugerReturn.OnDebug = new DebugerReturn.DebugData(LogDriver);
                    DriverTagReturn.OnDebug = new DriverTagReturn.DebugData(PollTagGet);

                    driverClient.Process();

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

            // calculate session stats
            FinishRequest();
            FinishSession();
        }

        /// <summary>
        /// Writing driver tags.
        /// <para>Запись тегов драйвера</para>
        /// </summary>
        /// <param name="text">Message</param>
        private void PollTagGet(List<DriverTag> tags)
        {
            LogDriver(tags.Count().ToString());
            for (int t = 0; t < tags.Count; t++)
            {

                if (tags[t] == null || tags[t].TagEnabled == false)
                {
                    continue;
                }

                DeviceTag findTag = DeviceTags.Where(r => r.Code == tags[t].TagCode).FirstOrDefault();
                SetTagData(findTag, tags[t].TagDataValue, tags[t].TagNumberDecimalPlaces);

            }
        }

        /// <summary>
        /// Recording of driver messages.
        /// <para>Запись сообщений драйвера.</para>
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
            }
        }

        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// <para>Устанавливает значение, статус и формат указанного тега.</para>
        /// </summary>
        private void SetTagData(DeviceTag deviceTag, object val, int numberDecimalPlaces = 3)
        {
            try
            {
                /*
                LogDriver("##########");
                LogDriver(string.Format(Locale.IsRussian ? "Тип данных: {0}" : "Data type: {0}", deviceTag.DataType.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Значение: {0}" : "Value: {0}", val.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Количество знаков после запятой или количество букв в слове: {0}" : "The number of decimal places or the number of letters in a word: {0}", numberDecimalPlaces.ToString()));     
                LogDriver(string.Format(Locale.IsRussian ? "Номер индекса: {0}" : "Index number: {0}", deviceTag.Index.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Код тега: {0}" : "Tag code: {0}", deviceTag.Code));
                LogDriver(string.Format(Locale.IsRussian ? "Длина данных: {0}" : "Data length: {0}", deviceTag.DataLength.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Количество элементов данных, хранящихся в значении тега: {0}" : "Data elements stored in the tag value: {0}", deviceTag.DataLen.ToString()));
                LogDriver("##########");
                */

                if (val == DBNull.Value || val == null)
                {
                    DeviceData.Invalidate(deviceTag.Index);
                }
                else
                {
                    if (val is string strVal && deviceTag.DataType == TagDataType.Unicode)
                    {
                        try
                        {
                            deviceTag.DataType = TagDataType.Unicode;
                            deviceTag.Format = new TagFormat(TagFormatType.String, "String");

                            DeviceData.SetUnicode(deviceTag.Code, Convert.ToString(val), 1);
                        }
                        catch (Exception ex) { Log.WriteInfo(ex.Message.ToString()); }

                    }
                    else if (val is string strVal2 && deviceTag.DataType == TagDataType.Double)
                    {
                        deviceTag.Format = new TagFormat(TagFormatType.Number, "N" + numberDecimalPlaces.ToString());
                        try 
                        { 
                            base.DeviceData.Set(deviceTag.Index, Math.Round(DriverUtils.StringToDouble(val.ToString()), numberDecimalPlaces), CnlStatusID.Defined); 
                        }
                        catch (Exception ex) { Log.WriteInfo(ex.Message.ToString()); }
                    }
                    else if (val is DateTime dtVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.DateTime;
                        try { base.DeviceData.SetDateTime(deviceTag.Index, dtVal, CnlStatusID.Defined); } catch (Exception ex) { Log.WriteInfo(ex.Message.ToString()); }
                    }
                    else if (deviceTag.Format == TagFormat.OffOn && Convert.ToBoolean(val) is System.Boolean bolVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.OffOn;
                        try { base.DeviceData.Set(deviceTag.Index, Convert.ToDouble(val), CnlStatusID.Defined); } catch (Exception ex) { Log.WriteInfo(ex.Message.ToString()); }
                    }
                    else if (val is Int32 intVal)
                    {
                        deviceTag.DataType = TagDataType.Int64;
                        deviceTag.Format = TagFormat.IntNumber;
                        try { base.DeviceData.Set(deviceTag, Convert.ToInt32(val), CnlStatusID.Defined); } catch (Exception ex) { Log.WriteInfo(ex.Message.ToString()); }
                    }
                    else if (val is Int64 int64Val)
                    {
                        deviceTag.DataType = TagDataType.Int64;
                        deviceTag.Format = TagFormat.IntNumber;
                        try { base.DeviceData.SetInt64(deviceTag.Index, Convert.ToInt64(val), CnlStatusID.Defined); } catch (Exception ex) { Log.WriteInfo(ex.Message.ToString()); }
                    }
                    else
                    {
                        deviceTag.Format = new TagFormat(TagFormatType.Number, "N" + numberDecimalPlaces.ToString()); 
                        try { base.DeviceData.Set(deviceTag.Index, Math.Round(Convert.ToDouble(val), numberDecimalPlaces), CnlStatusID.Defined); } catch (Exception ex) { Log.WriteInfo(ex.Message.ToString());  }
                    }
                }                 
            }
            catch (Exception ex)
            {
                Log.WriteInfo(ex.BuildErrorMessage(Locale.IsRussian ?
                    "Ошибка при установке данных тега \"{0}\"" :
                    "Error setting \"{0}\" tag data", deviceTag.Code));
            }
        }


        /// <summary>
        /// Converts data to scadа formats from driver format.
        /// <para>Преобразует данные из формата драйвера в форматы scada.</para>
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static TagFormat ConvertFormat(DriverTag.FormatData format)
        {
            TagFormat tagFormat = TagFormat.FloatNumber;
            switch (format)
            {
                case DriverTag.FormatData.Float:
                    return tagFormat = TagFormat.FloatNumber;
                case DriverTag.FormatData.Integer:
                    return tagFormat = TagFormat.IntNumber;
                case DriverTag.FormatData.DateTime:
                    return tagFormat = TagFormat.DateTime;
                case DriverTag.FormatData.String:
                    return tagFormat = TagFormat.String;
                case DriverTag.FormatData.Boolean:
                    return tagFormat = TagFormat.OffOn;
            }
            return tagFormat;
        }

    }
}
