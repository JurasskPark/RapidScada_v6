﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using DrvDbImportPlus.Common.Configuration;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDbImportPlus;
using Scada.Comm.Lang;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Lang;
using System.Data;
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
        /// <summary>
        /// Supported tag types.
        /// </summary>
        private enum TagType { Number, String, DateTime };

        private readonly AppDirs appDirs;                       // the application directories
        private readonly string driverCode;                     // the driver code
        private readonly int deviceNum;                         // the device number
        private readonly DrvDbImportPlusConfig config;          // the device configuration
        private string configFileName;                          // the configuration file name

        private List<Tag> deviceTags;                           // tags
        private List<ExportCmd> deviceCommands;                 // commands

        public DataSource dataSource;                           // the data source

        private bool deviceTagsBasedRequestedTableColumns;      // indicating Device Tags Based on the List of Requested Table Columns
        private bool writeLogDriver;                            // write driver log

        private DataTable dtData = new DataTable("Data");
        private DataTable dtSchema = new DataTable("Schema");



        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevDbImportPlusLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = true;
            ConnectionRequired = false;

            this.deviceNum = deviceConfig.DeviceNum;
            this.driverCode = DriverUtils.DriverCode;
            string shortFileName = DrvDbImportPlusConfig.GetFileName(deviceNum);
            configFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);

            dataSource = null;

            deviceTagsBasedRequestedTableColumns = true;
            writeLogDriver = true;

            // load configuration
            config = new DrvDbImportPlusConfig();
            if (!config.Load(configFileName, out string errMsg))
            {
                dataSource = null;
                LogDriver(errMsg);
            }
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

            if (config != null)
            {
                InitDataSource(config);
                CanSendCommands = config.ExportCmds.Count > 0;
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
            if (config == null)
            {
                LogDriver(Locale.IsRussian ?
                       "Количество тегов не было получено т.к. конфигурационный файл не был загружен" :
                       "The number of tags was not received because the configuration file was not loaded");
                return;
            }

            if (config.Load(configFileName, out string errMsg))
            {
                deviceTags = config.DeviceTags;
                deviceCommands = config.ExportCmds;

                foreach (CnlPrototypeGroup group in CnlPrototypeFactory.GetCnlPrototypeGroups(deviceTags, deviceCommands))
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

            if (ValidateDataSource() && ValidateCommand(dataSource.SelectCommand))
            {
                // request data
                int tryNum = 0;

                while (RequestNeeded(ref tryNum))
                {
                    if (Connect() && Request())
                    {
                        LastRequestOK = true;
                    }

                    Disconnect();
                    FinishRequest();
                    tryNum++;
                }


                if (!LastRequestOK && !IsTerminated)
                {
                    InvalidateData();
                }

            }
            else
            {
                SleepPollingDelay();
            }

            // calculate session stats
            FinishRequest();
            FinishSession();
        }

        /// <summary>
        /// Initializes the data source.
        /// </summary>
        public void InitDataSource(DrvDbImportPlusConfig config)
        {
            deviceTagsBasedRequestedTableColumns = config.DeviceTagsBasedRequestedTableColumns;
            writeLogDriver = config.WriteLogDriver;

            dataSource = DataSource.GetDataSourceType(config);

            if (dataSource != null)
            {
                string connStr = string.IsNullOrEmpty(config.DbConnSettings.ConnectionString) ?
                    dataSource.BuildConnectionString(config.DbConnSettings) :
                    config.DbConnSettings.ConnectionString;

                if (string.IsNullOrEmpty(connStr))
                {
                    dataSource = null;
                    LogDriver(Locale.IsRussian ?
                        "Соединение не определено" :
                        "Connection is undefined");
                }
                else
                {
                    dataSource.Init(connStr, config);
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
        /// Requests data from the database.
        /// </summary>
        private bool Request()
        {
            try
            {
                LogDriver(Locale.IsRussian ?
                    "Запрос данных" :
                    "Data request");

                //Tag based Columns
                if (deviceTagsBasedRequestedTableColumns == true)
                {
                    #region Formation of the structure
                    dtData = new DataTable("Data");

                    using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows == true)
                        {
                            dtData.Load(reader);
                            ParseDataTable(dtData, deviceTagsBasedRequestedTableColumns);
                        }
                    }

                    if (dtData.Columns.Count == 0)
                    {
                        LogDriver(Locale.IsRussian ?
                            "Данные отсутствуют" :
                            "No data available");
                        InvalidateData();
                    }
                    #endregion Formation of the structure
                }
                else //Tag base Row
                {
                    #region Formation of the structure
                    dtData = new DataTable("Data");

                    using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        dtSchema = reader.GetSchemaTable();
                        DataColumnCollection columns = dtData.Columns;

                        for (int cntRow = 0; cntRow < dtSchema.Rows.Count; cntRow++)
                        {
                            DataRow schemarow = dtSchema.Rows[cntRow];

                            string columnName = string.Empty;
                            object columnSize = new object();
                            Type dataType;
                            string dataTypeName = string.Empty;

                            try
                            {
                                columnName = reader.GetName(cntRow);
                            }
                            catch
                            {
                                columnName = schemarow["ColumnName"].ToString();
                            }

                            try
                            {
                                columnSize = schemarow["ColumnSize"];
                            }
                            catch { }

                            try
                            {
                                dataType = reader.GetFieldType(cntRow);
                            }
                            catch
                            {
                                dataTypeName = schemarow["DataTypeName"].ToString();
                                dataType = ElementType.GetElementType(dataTypeName);
                            }

                            if (columnName == string.Empty || columnName == "" || dataType == null)
                            {
                                LogDriver(Locale.IsRussian ?
                                "Не удалось определить формат данных у столбца таблицы. Информация о столбце таблицы: " :
                                "The data format of the table column could not be determined. Information about the table column: ");
                                LogDriver("Column Number = " + cntRow + ", Column Name = " + columnName + ", Size = " + columnSize + ", Data Type = " + dataTypeName + ".");
                            }

                            if (!columns.Contains(columnName.ToString()))
                            {
                                dtData.Columns.Add(columnName.ToString(), (Type)dataType);
                            }
                        }

                        while (reader.Read())
                        {
                            object[] ColArray = new object[reader.FieldCount];
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader[i] != null)
                                {
                                    ColArray[i] = reader[i];
                                }
                            }
                            dtData.LoadDataRow(ColArray, true);
                        }

                        ParseDataTable(dtData, deviceTagsBasedRequestedTableColumns);

                    }

                    if (dtData.Rows.Count == 0)
                    {
                        LogDriver(Locale.IsRussian ?
                            "Данные отсутствуют" :
                            "No data available");
                        InvalidateData();
                    }
                    #endregion Formation of the structure
                }

                return true;
            }
            catch (Exception ex)
            {
                LogDriver(string.Format(Locale.IsRussian ?
                    "Ошибка при выполнении запроса: {0}" :
                    "Error executing query: {0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// Parsing a table to search for tags
        /// </summary>
        /// <param name="dtData">DataTable</param>
        /// <param name="DeviceTagsBasedRequestedTableColumns">Tag based Columns or Row</param>
        private void ParseDataTable(DataTable dtData, bool DeviceTagsBasedRequestedTableColumns)
        {
            try
            {
                LogDriver(PrintDataTable(dtData));

                string name = string.Empty;
                object value = new object();
                int findNumberDecimalPlaces = 3;

                Dictionary<object, object> tableResult = new Dictionary<object, object>();
                List<DeviceTag> findDeviceTags = DeviceTags.ToList();

                for (int index = 0; index < findDeviceTags.Count - 1; ++index)
                {     
                    DeviceTag findTag = findDeviceTags[index];
                    string findCode = findTag.Code;
                    string findTagname = findTag.Name;
                   
                    Tag tmpTag = deviceTags.Where(r => r.TagCode == findCode).FirstOrDefault();

                    if (tmpTag == null || tmpTag.TagEnabled == false)
                    {
                        continue;
                    }

                    findNumberDecimalPlaces = tmpTag.NumberDecimalPlaces;
                    Tag.FormatTag tmpFormat = (Tag.FormatTag)Enum.Parse(typeof(Tag.FormatTag), tmpTag.TagFormat.ToString());
                    findTag.SetFormat(ConvertFormat(tmpFormat));
                    
                    #region Type Data
                    if (DeviceTagsBasedRequestedTableColumns)
                    {
                        if (dtData.Columns.Count == 0 || dtData.Rows.Count == 0)
                        {
                            LogDriver(Locale.IsRussian ?
                                "Количество столбцов или количество записей недостаточно для обработки данных. Обработка данных прекращена." :
                                "The number of columns or the number of records is not enough to process the data. Data processing has been terminated.");
                            return;
                        }

                        using (DataTableReader reader = new DataTableReader(dtData))
                        {
                            DataTable dtSchema = reader.GetSchemaTable();
                            DataColumnCollection columns = dtData.Columns;

                            for (int cntColumns = 0; cntColumns < columns.Count; cntColumns++)
                            {
                                DataColumn column = columns[cntColumns];
                                DataRow schemarow = dtSchema.Rows[cntColumns];
                                name = string.Empty;

                                try
                                {
                                    name = columns[cntColumns].ColumnName;
                                }
                                catch { }

                                DataRow dataTablerow = dtData.Rows[0];

                                try
                                {
                                    value = dataTablerow.ItemArray[cntColumns];
                                }
                                catch { }

                                if (!tableResult.TryGetValue(name, out object existingKey))
                                {
                                    // Create if not exists in dictionary
                                    tableResult.Add(name, value);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dtData.Columns.Count < 2 || dtData.Rows.Count == 0)
                        {
                            LogDriver(Locale.IsRussian ?
                                "Количество столбцов или количество записей недостаточно для обработки данных. Обработка данных прекращена." :
                                "The number of columns or the number of records is not enough to process the data. Data processing has been terminated.");
                            return;
                        }

                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            try
                            {
                                name = dtData.Rows[i][0].ToString();
                            }
                            catch { }

                            try
                            {
                                value = dtData.Rows[i][1];
                            }
                            catch { }

                            if (!tableResult.TryGetValue(name, out object existingKey))
                            {
                                // Create if not exists in dictionary
                                tableResult.Add(name, value);
                            }
                        }
                    }

                    object findValue = tableResult.Where(x => x.Key.ToString() == findTagname).FirstOrDefault().Value;

                    SetTagData(findTag, findValue, findNumberDecimalPlaces);

                    #endregion Type Data
                }
            }
            catch { }
        }

        /// <summary>
        /// Generates a textual representation of the data of <paramref name="table"/>.
        /// </summary>
        /// <param name="table">The table to print.</param>
        /// <returns>A textual representation of the data of <paramref name="table"/>.</returns>
        public static System.String PrintDataTable(DataTable table)
        {
            System.String GetCellValueAsString(DataRow row, DataColumn column)
            {
                var cellValue = row[column];
                var cellValueAsString = cellValue is null or DBNull ? "{null}" : cellValue.ToString();

                return cellValueAsString;
            }

            var columnWidths = new Dictionary<DataColumn, Int32>();

            foreach (DataColumn column in table.Columns)
            {
                columnWidths.Add(column, column.ColumnName.Length);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    columnWidths[column] = Math.Max(columnWidths[column], GetCellValueAsString(row, column).Length);
                }
            }

            var resultBuilder = new StringBuilder();

            resultBuilder.Append("| ");

            foreach (DataColumn column in table.Columns)
            {
                resultBuilder.Append(column.ColumnName.PadRight(columnWidths[column]));
                resultBuilder.Append(" | ");
            }

            resultBuilder.AppendLine();

            foreach (DataRow row in table.Rows)
            {
                resultBuilder.Append("| ");

                foreach (DataColumn column in table.Columns)
                {
                    resultBuilder.Append(GetCellValueAsString(row, column).PadRight(columnWidths[column]));
                    resultBuilder.Append(" | ");
                }

                resultBuilder.AppendLine();
            }

            return resultBuilder.ToString();
        }
    
        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// </summary>
        private void SetTagData(DeviceTag deviceTag, object val, int numberDecimalPlaces = 3)
        {
            try
            {
                LogDriver("##########");
                LogDriver(string.Format(Locale.IsRussian ? "Тип данных: {0}" : "Data type: {0}", deviceTag.DataType.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Значение: {0}" : "Value: {0}", val.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Количество знаков после запятой или количество букв в слове: {0}" : "The number of decimal places or the number of letters in a word: {0}", numberDecimalPlaces.ToString()));     
                LogDriver(string.Format(Locale.IsRussian ? "Номер индекса: {0}" : "Index number: {0}", deviceTag.Index.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Код тега: {0}" : "Tag code: {0}", deviceTag.Code));
                LogDriver(string.Format(Locale.IsRussian ? "Длина данных: {0}" : "Data length: {0}", deviceTag.DataLength.ToString()));
                LogDriver(string.Format(Locale.IsRussian ? "Количество элементов данных, хранящихся в значении тега: {0}" : "Data elements stored in the tag value: {0}", deviceTag.DataLen.ToString()));
                LogDriver("##########");

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
        public static TagFormat ConvertFormat(Tag.FormatTag format)
        {
            TagFormat tagFormat = TagFormat.FloatNumber;
            switch (format)
            {
                case Tag.FormatTag.Float:
                    return tagFormat = TagFormat.FloatNumber;
                case Tag.FormatTag.Integer:
                    return tagFormat = TagFormat.IntNumber;
                case Tag.FormatTag.DateTime:
                    return tagFormat = TagFormat.DateTime;
                case Tag.FormatTag.String:
                    return tagFormat = TagFormat.String;
                case Tag.FormatTag.Boolean:
                    return tagFormat = TagFormat.OffOn;
            }
            return tagFormat;
        }

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

                InitDataSource(config);

                if(FindCommandConfig(cmd, out DbCommand dbCommand))
                {
                    if (cmd.CmdDataIsEmpty)
                    {
                        dataSource.SetCmdParam(dbCommand, "cmdVal", cmd.CmdVal);
                    }
                    else
                    {
                        dataSource.SetCmdParam(dbCommand, "cmdVal", TeleCommand.CmdDataToString(cmd.CmdData));
                    }

                    if (ValidateDataSource() && ValidateCommand(dbCommand))
                    {
                        if (Connect() && SendDbCommand(dbCommand))
                        {
                            List<DeviceTag> findDeviceTags = DeviceTags.ToList();
                            DeviceTag findTag = findDeviceTags.Find(t => t.Code == cmd.CmdCode);

                            if (cmd.CmdDataIsEmpty)
                            {
                                DeviceData.Set(findTag.Code, cmd.CmdVal, 1);
                            }
                            else
                            {
                                findTag.DataType = TagDataType.Unicode;
                                DeviceData.SetUnicode(findTag.Code, TeleCommand.CmdDataToString(cmd.CmdData), 1);
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
        /// Finds a command configuration by command code or number.
        /// </summary>
        private bool FindCommandConfig(TeleCommand cmd, out DbCommand dbCommand)
        {
            if (dataSource.ExportCommandsCode != null && 
                !string.IsNullOrEmpty(cmd.CmdCode) &&
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
        /// Sends the command to the database.
        /// </summary>
        private bool SendDbCommand(DbCommand dbCommand)
        {
            try
            {
                LogDriver(Locale.IsRussian ?
                    "Запрос на изменение данных" :
                    "Data modification request");
                LogDriver(dbCommand.CommandText);
                int countChange = dbCommand.ExecuteNonQuery();
                LogDriver(Locale.IsRussian ?
                    @$"Изменено записей {countChange}" :
                    @$"Changed rows {countChange}");
                return true;
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
        private void LogDriver(string text)
        {
            if(text == string.Empty || text == "" || text == null)
            {
                return;
            }
            if (writeLogDriver)
            {
                Log.WriteAction(text);
            }
        }

    }
}
