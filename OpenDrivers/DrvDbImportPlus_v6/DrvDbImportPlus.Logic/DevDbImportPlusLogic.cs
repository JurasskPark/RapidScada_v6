// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using DrvDbImportPlus.Common.Configuration;
using MySqlX.XDevAPI.Relational;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDbImportPlus;
using Scada.Comm.Lang;
using Scada.Data.Models;
using Scada.Lang;
using System.Data;
using System.Data.Common;
using static Scada.Comm.Drivers.DrvDbImportPlus.Tag;


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

        public DataSource dataSource;                           // the data source

        private bool DeviceTagsBasedRequestedTableColumns;      // indicating Device Tags Based on the List of Requested Table Columns
        private bool HistoricalData;                            // condition for transmitting historical data
        private int DiscrepancyInSeconds;                       // the dead zone for the passage of which the value is considered to be poor quality
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

            config = new DrvDbImportPlusConfig();

            dataSource = null;

            DeviceTagsBasedRequestedTableColumns = true;
        }


        /// <summary>
        /// Performs actions after adding the device to a communication line.
        /// </summary>
        public override void OnCommLineStart()
        {
            // load configuration
            DrvDbImportPlusConfig config = new DrvDbImportPlusConfig();

            if (config.Load(configFileName, out string errMsg))
            {
                InitDataSource(config);
                CanSendCommands = config.ExportCmds.Count > 0;
            }
            else
            {
                dataSource = null;
                Log.WriteLine(errMsg);
            }
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
            DeviceTagsBasedRequestedTableColumns = config.DeviceTagsBasedRequestedTableColumns;
            HistoricalData = config.HistoricalData;
            DiscrepancyInSeconds = config.DiscrepancyInSeconds;

            dataSource = DataSource.GetDataSourceType(config);

            if (dataSource != null)
            {
                string connStr = string.IsNullOrEmpty(config.DbConnSettings.ConnectionString) ?
                    dataSource.BuildConnectionString(config.DbConnSettings) :
                    config.DbConnSettings.ConnectionString;

                if (string.IsNullOrEmpty(connStr))
                {
                    dataSource = null;
                    Log.WriteLine(Locale.IsRussian ?
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
                Log.WriteLine(Locale.IsRussian ?
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
                Log.WriteLine(Locale.IsRussian ?
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
                Log.WriteLine(Locale.IsRussian ?
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
                Log.WriteLine(string.Format(Locale.IsRussian ?
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
                Log.WriteLine(string.Format(Locale.IsRussian ?
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
                Log.WriteLine(Locale.IsRussian ?
                    "Запрос данных" :
                    "Data request");

                //Tag based Columns
                if (DeviceTagsBasedRequestedTableColumns == true)
                {
                    #region Formation of the structure
                    dtData = new DataTable("Data");

                    using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows == true)
                        {
                            dtData.Load(reader);
                            ParseDataTableData(dtData, DeviceTagsBasedRequestedTableColumns);
                        }
                    }
                    #endregion Formation of the structure

                    if (dtData.Columns.Count == 0)
                    {
                        Log.WriteLine(Locale.IsRussian ?
                            "Данные отсутствуют" :
                            "No data available");
                        InvalidateData();
                    }
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
                                Log.WriteLine(Locale.IsRussian ?
                                "Не удалось определить формат данных у столбца таблицы. Информация о столбце таблицы: " :
                                "The data format of the table column could not be determined. Information about the table column: ");
                                Log.WriteLine("Column Number = " + cntRow + ", Column Name = " + columnName + ", Size = " + columnSize + ", Data Type = " + dataTypeName + ".");
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

                        ParseDataTableData(dtData, DeviceTagsBasedRequestedTableColumns);
                    }
                    #endregion Formation of the structure

                    if (dtData.Rows.Count == 0)
                    {
                        Log.WriteLine(Locale.IsRussian ?
                            "Данные отсутствуют" :
                            "No data available");
                        InvalidateData();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Ошибка при выполнении запроса: {0}" :
                    "Error executing query: {0}", ex.Message));
                return false;
            }
        }

        private void ParseDataTableData(DataTable dtData, bool DeviceTagsBasedRequestedTableColumns)
        {
            string name = string.Empty;
            object value = new object();

            // we reset the previous usage
            for (int c = 0; c < deviceTags.Count; c++)
            {
                deviceTags[c].TagVal = null;
                deviceTags[c].TagStat = 0;
            }

            if (DeviceTagsBasedRequestedTableColumns)
            {
                if (dtData.Columns.Count == 0 || dtData.Rows.Count == 0)
                {
                    Log.WriteLine(Locale.IsRussian ?
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

                        #region Find Tag
                        Tag tag = (Tag)deviceTags.Where(x => x.TagName == name).FirstOrDefault();
                        if (tag == null)
                        {
                            tag.TagVal = null;
                            tag.TagStat = 0;
                        }
                        else
                        {
                            tag.TagVal = value;
                            tag.TagStat = 1;
                        }
                        #endregion  Find Tag

                    }
                }
            }
            else
            {
                if (dtData.Columns.Count < 2 || dtData.Rows.Count == 0)
                {
                    Log.WriteLine(Locale.IsRussian ?
                        "Количество столбцов или количество записей недостаточно для обработки данных. Обработка данных прекращена." :
                        "The number of columns or the number of records is not enough to process the data. Data processing has been terminated.");
                    return;
                }

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (dtData.Rows[i][0].ToString() == string.Empty || dtData.Rows[i][0] is null)
                    {
                        Log.WriteLine(Locale.IsRussian ?
                            "Столбец номер 1 с названием тега пустой или равен null. Номер тега и номер строки таблицы - " + (i + 1) :
                            "Column number 1 with the tag name is empty or null. Tag number and table row number -" + (i + 1));
                    }

                    try
                    {
                        name = dtData.Rows[i][0].ToString();
                    }
                    catch { }


                    if (dtData.Rows[i][1].ToString() == string.Empty || dtData.Rows[i][1] is null)
                    {
                        Log.WriteLine(Locale.IsRussian ?
                            "Столбец номер 2 с значениям тега пустой или равен null. Номер тега и номер строки таблицы - " + (i + 1) :
                            "Column number 2 with the tag value is empty or null. Tag number and table row number" + (i + 1));
                    }

                    try
                    {
                        value = dtData.Rows[i][1];
                    }
                    catch { }

                    #region  Find Tag
                    Tag tag = (Tag)deviceTags.Where(x => x.TagName == name).FirstOrDefault();
                    if (tag == null)
                    {
                        tag.TagVal = null;
                        tag.TagStat = 0;
                    }
                    else
                    {
                        tag.TagVal = value;
                        tag.TagStat = 1;
                    }
                    #endregion  Find Tag

                }
            }

            #region Recording current data

            for (int t = 0; t < deviceTags.Count; t++)
            {
                if(deviceTags[t].TagEnabled == false)
                {
                    continue;
                }

                if (deviceTags[t].TagCode != string.Empty)
                {
                    SetTagData(deviceTags[t].TagCode, deviceTags[t].TagVal, deviceTags[t].TagStat);
                }
                else
                {
                    SetTagData(deviceTags.IndexOf(deviceTags[t]), deviceTags[t].TagVal, deviceTags[t].TagStat);
                }
            }

            #endregion  Recording current data
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
                        deviceTag.Format = TagFormat.FloatNumber;
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
                        deviceTag.Format = TagFormat.FloatNumber;
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

        /// <summary>
        /// Converts the device slice to a general purpose slice.
        /// </summary>
        private bool ConvertSlice(DeviceSlice srcSlice, out Slice destSlice)
        {
            try
            {
                int srcDataLength = srcSlice.CnlData.Length;
                int destDataLength = 0;
                List<int> cnlNums = new List<int>(srcDataLength);

                foreach (DeviceTag deviceTag in srcSlice.DeviceTags)
                {
                    if (deviceTag == null)
                    {
                        throw new ScadaException(Locale.IsRussian ?
                            "Неопределенные теги в срезе не допускаются." :
                            "Undefined tags are not allowed in a slice.");
                    }

                    if (deviceTag.Cnl != null)
                    {
                        int tagDataLength = deviceTag.DataLength;

                        for (int i = 0; i < tagDataLength; i++)
                        {
                            cnlNums.Add(deviceTag.Cnl.CnlNum + i);
                        }

                        destDataLength += tagDataLength;
                    }
                }

                if (destDataLength == 0)
                {
                    destSlice = null;
                    return false;
                }
                else if (destDataLength == srcDataLength)
                {
                    destSlice = new Slice(srcSlice.Timestamp, cnlNums.ToArray(), srcSlice.CnlData);
                    return true;
                }
                else
                {
                    CnlData[] destCnlData = new CnlData[destDataLength];
                    int srcDataIndex = 0;
                    int destDataIndex = 0;

                    foreach (DeviceTag deviceTag in srcSlice.DeviceTags)
                    {
                        int tagDataLength = deviceTag.DataLength;

                        if (deviceTag.Cnl != null)
                        {
                            Array.Copy(srcSlice.CnlData, srcDataIndex, destCnlData, destDataIndex, tagDataLength);
                            destDataIndex += tagDataLength;
                        }

                        srcDataIndex += tagDataLength;
                    }

                    destSlice = new Slice(srcSlice.Timestamp, cnlNums.ToArray(), destCnlData);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex, CommPhrases.DataSourceMessage, string.Format(Locale.IsRussian ?
                    "Ошибка при конвертировании среза от устройства {0}" :
                    "Error converting slice from the device {0}", srcSlice.DeviceNum));
                destSlice = null;
                return false;
            }
        }

        /// <summary>
        /// Sends the telecontrol command.
        /// </summary>
        public override void SendCommand(TeleCommand cmd)
        {
            base.SendCommand(cmd);

            if (CanSendCommands)
            {
                LastRequestOK = false;

                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Получена команда. " :
                    "Command received. "));
                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Номер команды (@cmdNum): " + cmd.CmdNum :
                    "Command number (@cmdNum): " + cmd.CmdNum));
                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Код команды (@cmdCode): " + cmd.CmdCode :
                    "Command code (@cmdCode): " + cmd.CmdCode));
                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Значение команды (@cmdVal): " + cmd.CmdVal :
                    "Command value (@cmdVal): " + cmd.CmdVal));

                DbCommand dbCommand;

                if (dataSource.ExportCommandsNum.TryGetValue(cmd.CmdNum, out dbCommand) ||
                    dataSource.ExportCommandsCode.TryGetValue(cmd.CmdCode, out dbCommand) ||
                    dataSource.ExportCommandsNum.TryGetValue(0, out dbCommand))
                {
                    if (ValidateDataSource() && ValidateCommand(dbCommand))
                    {
                        dataSource.SetCmdParam(dbCommand, "cmdVal", (object)cmd.CmdVal);
                        dataSource.SetCmdParam(dbCommand, "cmdCode", (object)cmd.CmdCode);
                        dataSource.SetCmdParam(dbCommand, "cmdNum", (object)cmd.CmdNum);
                        int tryNum = 0;

                        while (RequestNeeded(ref tryNum))
                        {
                            if (Connect() && SendDbCommand(dbCommand))
                            {
                                LastRequestOK = true;
                            }
                                
                            Disconnect();
                            FinishRequest();
                            tryNum++;
                        }
                    }
                }
                else
                {
                    LastRequestOK = false;
                    Log.WriteLine(CommPhrases.InvalidCommand);
                }
            }

            FinishRequest();
            FinishCommand();
        }

        /// <summary>
        /// Sends the command to the database.
        /// </summary>
        private bool SendDbCommand(DbCommand dbCommand)
        {
            try
            {
                Log.WriteLine(Locale.IsRussian ?
                    "Запрос на изменение данных" :
                    "Data modification request");
                Log.WriteLine(dbCommand.CommandText);
                dbCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLine(string.Format(Locale.IsRussian ?
                    "Ошибка при отправке команды БД: {0}" :
                    "Error sending command to the database: {0}", ex.Message));
                return false;
            }
        }

    }
}
