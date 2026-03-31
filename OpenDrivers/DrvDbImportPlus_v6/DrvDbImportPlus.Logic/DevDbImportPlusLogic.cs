// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDbImportPlus;
using Scada.Comm.Lang;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Lang;
using System.Data.Common;
using System.Text;

namespace Scada.Comm.Drivers.DrvDbImportPlusLogic.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevDbImportPlusLogic : DeviceLogic
    {
        private readonly AppDirs appDirs;                      // the application directories
        private readonly string driverCode;                    // the driver code
        private readonly int deviceNum;                        // the device number
        private readonly int commLineNum;                      // the communication line number
        private readonly DrvDbImportPlusProject project;       // the device configuration
        private readonly string pathProject;                   // the configuration path
        private readonly string projectFileName;               // the configuration file name
        private readonly string pathLog;                       // the log path
        private readonly string logFileName;                   // the log file name

        private readonly DriverClient driverClient;            // the driver client
        private readonly List<DriverTag> deviceTags;           // aggregated import tags
        public DataSource dataSource;                          // the data source
        private readonly bool writeLogLine;                    // write communication line log
        private bool writeLogDriver;                           // write driver log

        public const string defaultTimestampFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";   // the timestamp format

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevDbImportPlusLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceProject)
            : base(commContext, lineContext, deviceProject)
        {
            CanSendCommands = true;
            ConnectionRequired = false;

            this.appDirs = commContext.AppDirs;
            this.deviceNum = deviceProject.DeviceNum;
            this.commLineNum = lineContext.CommLineNum;
            this.driverCode = DriverUtils.DriverCode;

            string shortFileName = DriverUtils.GetFileName(deviceNum);
            this.pathProject = CommContext.AppDirs.ConfigDir;
            this.projectFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);

            string shortFileLogName = DriverUtils.GetFileLogName(commLineNum);
            this.pathLog = commContext.AppDirs.LogDir;
            this.logFileName = Path.Combine(commContext.AppDirs.LogDir, shortFileLogName);

            this.writeLogLine = lineContext.LineConfig.LineOptions.DetailedLog;

            dataSource = null;

            deviceTags = new List<DriverTag>();
            writeLogDriver = true;

            // load configuration
            project = new DrvDbImportPlusProject();
            if (!project.Load(projectFileName, out string errMsg))
            {
                dataSource = null;
                LogDriver(errMsg);
            }
            else
            {
                writeLogDriver = project.DebugerSettings?.LogWrite ?? true;
            }

            driverClient = new DriverClient(projectFileName, project, deviceNum, pathLog, true);
        }


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

            if (project != null)
            {
                CanSendCommands = project.ExportCmds.Count > 0 || project.ImportCmds.Count > 0;
            }
        }

        /// <summary>
        /// Performs actions after stopping the device on the communication line.
        /// </summary>
        public override void OnCommLineTerminate()
        {
            base.OnCommLineTerminate();

            LogDriver(Locale.IsRussian ?
                       "Останов драйвера" :
                       "Stopping the driver");
        }

        /// <summary>
        /// Initializes the device tags.
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
                foreach (CnlPrototypeGroup group in CnlPrototypeFactory.GetCnlPrototypeGroups(project.ImportCmds, project.ExportCmds))
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
        /// </summary>
        public override void Session()
        {
            base.Session();

            LastRequestOK = false;

            try
            {
                DebugerReturn.OnDebug = new DebugerReturn.DebugData(LogDriver);
                DebugerTagReturn.OnDebug = new DebugerTagReturn.DebugData(PollTagGet);
                DriverClient driverClient = new DriverClient(projectFileName, project, deviceNum, pathLog, true);
                driverClient.Process();
                driverClient.Dispose();

                LastRequestOK = true;
            }     
            catch
            {
                LastRequestOK = false;
            }

             SleepPollingDelay();
            
            // calculate session stats
            FinishRequest();
            FinishSession();
        }

        /// <summary>
        /// Receives parsed tags from DriverClient.
        /// </summary>
        private void PollTagGet(List<DriverTag> tags)
        {
            if (tags == null || tags.Count == 0)
                return;

            List<DeviceTag> deviceTagList = DeviceTags.ToList();

            foreach (DriverTag srcTag in tags)
            {
                if (srcTag == null || !srcTag.Enabled)
                    continue;

                DeviceTag deviceTag = deviceTagList.Find(t => t.Code == srcTag.Code);
                if (deviceTag != null)
                {
                    SetTagData(deviceTag, srcTag.Val, srcTag.NumberDecimalPlaces);
                }
            }
        }

        /// <summary>
        /// Initializes the data source.
        /// </summary>
        public void InitDataSource(DrvDbImportPlusProject project)
        {
            dataSource = DataSource.GetDataSourceType(project);

            if (dataSource != null)
            {
                string connStr = string.IsNullOrEmpty(project.DbConnSettings.ConnectionString) ?
                    dataSource.BuildConnectionString(project.DbConnSettings) :
                    project.DbConnSettings.ConnectionString;

                if (string.IsNullOrEmpty(connStr))
                {
                    dataSource = null;
                    LogDriver(Locale.IsRussian ?
                        "Соединение не определено" :
                        "Connection is undefined");
                }
                else
                {
                    dataSource.Init(project);
                }
            }
            else
            {
                LogDriver(Locale.IsRussian ?
                      "Data source type is not set or not supported" :
                      "Тип источника данных не задан или не поддерживается");
            }
        }

        /// <summary>
        /// Validates the data source.
        /// </summary>
        private bool ValidateDataSource()
        {
            if (dataSource == null)
            {
                LogDriver(Locale.IsRussian ?
                    "Нормальное взаимодействие с КП невозможно, т.к. источник данных не определён" :
                    "Normal device communication is impossible because data source is undefined");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validates the database command.
        /// </summary>
        private bool ValidateCommand(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                LogDriver(Locale.IsRussian ?
                    "Нормальное взаимодействие с КП невозможно, т.к. SQL-команда не определена" :
                    "Normal device communication is impossible because SQL command is undefined");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Connects to the database.
        /// </summary>
        private bool Connect()
        {
            try
            {
                dataSource.Connect();
                return true;
            }
            catch (Exception ex)
            {
                LogDriver(string.Format(Locale.IsRussian ?
                    "Ошибка при соединении с БД: {0}" :
                    "Error connecting to DB: {0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// Disconnects from the database.
        /// </summary>
        private void Disconnect()
        {
            try
            {
                dataSource.Disconnect();
            }
            catch (Exception ex)
            {
                LogDriver(string.Format(Locale.IsRussian ?
                    "Ошибка при разъединении с БД: {0}" :
                    "Error disconnecting from DB: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// </summary>
        private void SetTagData(DeviceTag deviceTag, object val, int numberDecimalPlaces = 3)
        {
            try
            {
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
                        catch (Exception e) 
                        {
                            LogDriver(e.Message.ToString());
                        }

                    }
                    else if (val is string strVal2 && deviceTag.DataType == TagDataType.Double)
                    {
                        deviceTag.Format = new TagFormat(TagFormatType.Number, "N" + numberDecimalPlaces.ToString());
                        try { base.DeviceData.Set(deviceTag.Index, Math.Round(DriverUtils.StringToDouble(val.ToString()), numberDecimalPlaces), CnlStatusID.Defined); }
                        catch (Exception e)
                        {
                            LogDriver(e.Message.ToString());
                        }
                    }
                    else if (val is DateTime dtVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.DateTime;
                        try { base.DeviceData.SetDateTime(deviceTag.Index, dtVal, CnlStatusID.Defined); } catch { }
                    }
                    else if (deviceTag.Format == TagFormat.OffOn && Convert.ToBoolean(val) is System.Boolean bolVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.OffOn;
                        try { base.DeviceData.Set(deviceTag.Index, Convert.ToDouble(val), CnlStatusID.Defined); } catch { }
                    }
                    else if (val is Int32 intVal)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.IntNumber;
                        try { base.DeviceData.Set(deviceTag, Convert.ToInt32(val), CnlStatusID.Defined); } catch { }
                    }
                    else if (val is Int64 int64Val)
                    {
                        deviceTag.DataType = TagDataType.Double;
                        deviceTag.Format = TagFormat.IntNumber;
                        try { base.DeviceData.SetInt64(deviceTag.Index, Convert.ToInt64(val), CnlStatusID.Defined); } catch { }
                    }
                    else
                    {
                        deviceTag.Format = new TagFormat(TagFormatType.Number, "N" + numberDecimalPlaces.ToString()); 
                        try { base.DeviceData.Set(deviceTag.Index, Math.Round(Convert.ToDouble(val), numberDecimalPlaces), CnlStatusID.Defined); } catch { }
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
        /// Converts data to scadа formats from tag format.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static TagFormat ConvertFormat(DriverTag.FormatTag format)
        {
            TagFormat tagFormat = TagFormat.FloatNumber;
            switch (format)
            {
                case DriverTag.FormatTag.Float:
                    return tagFormat = TagFormat.FloatNumber;
                case DriverTag.FormatTag.Integer:
                    return tagFormat = TagFormat.IntNumber;
                case DriverTag.FormatTag.DateTime:
                    return tagFormat = TagFormat.DateTime;
                case DriverTag.FormatTag.String:
                    return tagFormat = TagFormat.String;
                case DriverTag.FormatTag.Boolean:
                    return tagFormat = TagFormat.OffOn;
            }
            return tagFormat;
        }

        /// <summary>
        /// Sends a telecontrol command to the database.
        /// Supports command matching by both export and import command definitions.
        /// </summary>
        public override void SendCommand(TeleCommand cmd)
        {
            base.SendCommand(cmd);

            if (CanSendCommands)
            {
                LastRequestOK = false;

                // Log regardless of whether logging is enabled or not
                Log.WriteAction(string.Format(Locale.IsRussian ?
                    "Получена команда. " :
                    "Command received. "));
                Log.WriteAction(string.Format(Locale.IsRussian ?
                    "Дата: " + cmd.CreationTime.ToString() :
                    "Date: " + cmd.CreationTime.ToString()));
                Log.WriteAction(string.Format(Locale.IsRussian ?
                   "Пользователь ID: " + cmd.UserID.ToString() :
                   "User ID: " + cmd.UserID.ToString()));
                Log.WriteAction(string.Format(Locale.IsRussian ?
                   "Номер устройства: " + cmd.DeviceNum.ToString() :
                   "Device number: " + cmd.DeviceNum.ToString()));
                Log.WriteAction(string.Format(Locale.IsRussian ?
                    "Номер команды (@cmdNum): " + cmd.CmdNum :
                    "Command number (@cmdNum): " + cmd.CmdNum));
                Log.WriteAction(string.Format(Locale.IsRussian ?
                    "Код команды (@cmdCode): " + cmd.CmdCode :
                    "Command code (@cmdCode): " + cmd.CmdCode));
                Log.WriteAction(string.Format(Locale.IsRussian ?
                    "Значение команды (@cmdVal): " + cmd.CmdVal :
                    "Command value (@cmdVal): " + cmd.CmdVal));
                Log.WriteAction(string.Format(Locale.IsRussian ?
                    "Значение команды (@cmdData): " + TeleCommand.CmdDataToString(cmd.CmdData) :
                    "Command value (@cmdData): " + TeleCommand.CmdDataToString(cmd.CmdData)));

                // Data polling is handled by DriverClient/DatabaseCommand.
                // Initialize data source here only when a telecontrol command is actually sent.
                if (dataSource == null)
                {
                    InitDataSource(project);
                }

                if (ValidateDataSource() && FindDbCommand(cmd, out DbCommand dbCommand))
                {
                    dataSource.SetCmdParam(dbCommand, "cmdVal", cmd.CmdDataIsEmpty
                        ? cmd.CmdVal
                        : TeleCommand.CmdDataToString(cmd.CmdData));

                    if (ValidateCommand(dbCommand))
                    {
                        if (Connect() && SendDbCommand(dbCommand))
                        {
                            DeviceTag findTag = DeviceTags.ToList().Find(t => t.Code == cmd.CmdCode);
                            if (findTag != null)
                            {
                                if (cmd.CmdDataIsEmpty)
                                {
                                    DeviceData.Set(findTag.Code, cmd.CmdVal, 1);
                                }
                                else
                                {
                                    findTag.DataType = TagDataType.Unicode;
                                    DeviceData.SetUnicode(findTag.Code, TeleCommand.CmdDataToString(cmd.CmdData), 1);
                                }
                            }

                            LastRequestOK = true;
                        }

                        Disconnect();
                    }                                 
                }
                else
                {
                    LastRequestOK = false;
                    LogDriver(CommPhrases.InvalidCommand);
                }
            }

            FinishRequest();
            FinishCommand();
        }

        /// <summary>
        /// Finds a database command by command code or number.
        /// Lookup is performed against both export and import command definitions.
        /// </summary>
        private bool FindDbCommand(TeleCommand cmd, out DbCommand dbCommand)
        {
            if (TryGetProjectCommandKey(cmd, out int projectCmdNum, out string projectCmdCode))
            {
                if (!string.IsNullOrWhiteSpace(projectCmdCode) &&
                    dataSource.ExportCommandsCode != null &&
                    dataSource.ExportCommandsCode.TryGetValue(projectCmdCode, out dbCommand))
                {
                    return true;
                }

                if (projectCmdNum > 0 &&
                    dataSource.ExportCommandsNum != null &&
                    dataSource.ExportCommandsNum.TryGetValue(projectCmdNum, out dbCommand))
                {
                    return true;
                }
            }

            // Fallback: direct lookup by incoming command parameters.
            if (dataSource.ExportCommandsCode != null &&
                !string.IsNullOrWhiteSpace(cmd.CmdCode) &&
                dataSource.ExportCommandsCode.TryGetValue(cmd.CmdCode, out dbCommand))
            {
                return true;
            }

            if (dataSource.ExportCommandsNum != null &&
                cmd.CmdNum > 0 &&
                dataSource.ExportCommandsNum.TryGetValue(cmd.CmdNum, out dbCommand))
            {
                return true;
            }

            dbCommand = null;
            return false;
        }

        /// <summary>
        /// Tries to find a command in project export/import definitions and returns its key values.
        /// </summary>
        private bool TryGetProjectCommandKey(TeleCommand cmd, out int cmdNum, out string cmdCode)
        {
            cmdNum = 0;
            cmdCode = string.Empty;

            ExportCmd exportCmd = project.ExportCmds?.Find(c =>
                c.Enabled &&
                ((!string.IsNullOrWhiteSpace(cmd.CmdCode) && string.Equals(c.CmdCode, cmd.CmdCode, StringComparison.Ordinal)) ||
                 (cmd.CmdNum > 0 && c.CmdNum == cmd.CmdNum)));

            if (exportCmd != null)
            {
                cmdNum = exportCmd.CmdNum;
                cmdCode = exportCmd.CmdCode;
                return true;
            }

            ImportCmd importCmd = project.ImportCmds?.Find(c =>
                c.Enabled &&
                ((!string.IsNullOrWhiteSpace(cmd.CmdCode) && string.Equals(c.CmdCode, cmd.CmdCode, StringComparison.Ordinal)) ||
                 (cmd.CmdNum > 0 && c.CmdNum == cmd.CmdNum)));

            if (importCmd != null)
            {
                cmdNum = importCmd.CmdNum;
                cmdCode = importCmd.CmdCode;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sends the command to the database.
        /// </summary>
        private bool SendDbCommand(DbCommand dbCommand)
        {
            try
            {
                LogDriver(Locale.IsRussian ?
                    "Запрос на изменение данных" :
                    "Data modification request");

                try
                {
                    int countChange = dbCommand.ExecuteNonQuery();

                    LogDriver(dbCommand.CommandText);

                    LogDriver(Locale.IsRussian ?
                        @$"Изменено записей {countChange}" :
                        @$"Changed rows {countChange}");
                    return true;

                }
                catch (Exception e)
                {
                    LogDriver(e.Message);
                    return false;
                }   
            }
            catch (Exception ex)
            {
                LogDriver(string.Format(Locale.IsRussian ?
                    "Ошибка при отправке команды БД: {0}" :
                    "Error sending command to the database: {0}", ex.ToString()));
                return false;
            }
        }

        /// <summary>
        /// Log Write Driver
        /// </summary>
        /// <param name="text">Message</param>
        private void LogDriver(string text, bool writeDateTime = true)
        {
            if (text == string.Empty || text == "" || text == null)
            {
                return;
            }

            if (writeLogDriver && writeLogLine)
            {
                if (!writeDateTime)
                {
                    // for raw transport lines (Receive/Send) keep legacy format without timestamp.
                    using StreamWriter streamWriter = File.AppendText(logFileName);
                    streamWriter.WriteLine(text);
                    return;
                }

                 Log.WriteMessage(text, Scada.Log.LogMessageType.Info);   
            }
            else if (writeLogDriver && !writeLogLine)
            {
                StringBuilder sb = new StringBuilder();
                StreamWriter streamWriter = File.AppendText(logFileName);
                if (writeDateTime)
                {
                    sb.Append(DateTime.Now.ToString(defaultTimestampFormat));
                }
                sb.Append(" ").Append(text);
                streamWriter.WriteLine(sb.ToString().Trim());
                streamWriter.Close();
            }
        }

    }
}
