// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using DrvDbImportPlus.Common.Configuration;
using NpgsqlTypes;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDbImportPlus;
using Scada.Comm.Lang;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Lang;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.Design.AxImporter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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

        private readonly AppDirs appDirs;               // the application directories
        private readonly string driverCode;             // the driver code
        private readonly int deviceNum;                 // the device number
        private readonly DrvDbImportPlusConfig config;  // the device configuration
        private string configFileName;                  // the configuration file name

        public DataSource dataSource;   // the data source

        private bool DeviceTagsBasedRequestedTableColumns; //indicating Device Tags Based on the List of Requested Table Columns
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
                foreach (CnlPrototypeGroup group in CnlPrototypeFactory.GetCnlPrototypeGroups(config))
                {
                    Log.WriteLine(group.CnlPrototypes.Count.ToString());
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

        /// 
        /// <summary>
        /// Sets value, status and format of the specified tag.
        /// </summary>
        private void SetTagData(string code, int tagIndex, object val, int stat)
        {
            try
            {
                //Log.WriteLine("code " + code.ToString() + " tagIndex " + tagIndex.ToString() + " val " + val.ToString() + " stat " + stat.ToString());

                DeviceTag deviceTag = DeviceTags[tagIndex];

                if (val is string strVal)
                {
                    deviceTag.DataType = TagDataType.Unicode;
                    deviceTag.Format = TagFormat.String;
                    try { base.DeviceData.SetUnicode(code, strVal, stat); } catch { }
                    try { base.DeviceData.SetUnicode(tagIndex, strVal, stat); } catch { } 
                }
                else if (val is DateTime dtVal)
                {
                    deviceTag.DataType = TagDataType.Double;
                    deviceTag.Format = TagFormat.DateTime;
                    try { base.DeviceData.SetDateTime(code, dtVal, stat); } catch { }
                    try { base.DeviceData.SetDateTime(tagIndex, dtVal, stat); } catch { }
                }
                else 
                {
                    deviceTag.DataType = TagDataType.Double;
                    deviceTag.Format = TagFormat.FloatNumber;
                    try { base.DeviceData.Set(code, Convert.ToDouble(val), stat); } catch { }
                    try { base.DeviceData.Set(tagIndex, Convert.ToDouble(val), stat); } catch { }
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
                    using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            Log.WriteLine(CommPhrases.ResponseOK);

                            int tagCnt = DeviceTags.Count;
                            int fieldCnt = reader.FieldCount;

                            for (int i = 0, cnt = Math.Min(tagCnt, fieldCnt); i < cnt; i++)
                            {
                                SetTagData("DBTAG" + (i + 1).ToString() + "", i, reader[i], 1);
                            }
                        }
                        else
                        {
                            Log.WriteLine(Locale.IsRussian ?
                                "Данные отсутствуют" :
                                "No data available");
                            InvalidateData();
                        }
                    }
                }
                else //Tag base Columns
                {
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
                    }

                    int tagCnt = DeviceTags.Count;
                    int rowCnt = dtData.Rows.Count;

                    try
                    {
                        Log.WriteLine(CommPhrases.ResponseOK);

                        for (int i = 0; i < Math.Min(tagCnt, rowCnt); i++)
                        {
                            if (dtData.Rows[i][0].ToString() == string.Empty || dtData.Rows[i][0] is null)
                            {
                                Log.WriteLine(Locale.IsRussian ?
                                    "Столбец номер 1 с названием тега пустой или равен null. Номер тега и номер строки таблицы - " + (i + 1) :
                                    "Column number 1 with the tag name is empty or null. Tag number and table row number -" + (i + 1));
                            }

                            if (dtData.Rows[i][1].ToString() == string.Empty || dtData.Rows[i][1] is null)
                            {
                                Log.WriteLine(Locale.IsRussian ?
                                    "Столбец номер 2 с значениям тега пустой или равен null. Номер тега и номер строки таблицы - " + (i + 1) :
                                    "Column number 2 with the tag value is empty or null. Tag number and table row number" + (i + 1));
                            }
                            SetTagData("DBTAG" + (i + 1).ToString() + "", i, dtData.Rows[i][1], 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLine(ex.ToString());
                        InvalidateData();
                    }

                    try
                    {
                        for (int i = 0; i < Math.Min(tagCnt, rowCnt); i++)
                        {
                            //create a historical data slice
                            if (dtData.Rows[i][1].ToString() == string.Empty || dtData.Rows[i][2] is null || dtData.Rows.Count < 3)
                            {
                                Log.WriteLine(Locale.IsRussian ?
                                  "Столбец номер 3 с датой и временем тега пустой или равен null. Номер тега и номер строки таблицы - " + (i + 1) :
                                  "Column number 3 with the date time of the tag is empty or null. Tag number and table row number - " + (i + 1));
                            }
                            else
                            {
                                DateTime dtSSlice = (DateTime)dtData.Rows[i][2];
                                DeviceSlice deviceSlice = new DeviceSlice(
                                    new DateTime(dtSSlice.Year, dtSSlice.Month, dtSSlice.Day, dtSSlice.Hour, dtSSlice.Minute, dtSSlice.Second, DateTimeKind.Utc),
                                    1, 1);
                                deviceSlice.DeviceTags[0] = DeviceTags["DBTAG" + (i + 1).ToString() + ""];
                                string Descr = Locale.IsRussian ? " Значение = " : " Value = ";
                                deviceSlice.Descr = DeviceTags[i].Name + Descr + dtData.Rows[i][1].ToString();
                                DeviceData.EnqueueSlice(deviceSlice);
                            }
                        }
                    }
                    catch                 
                    {
                        InvalidateData();
                    }

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
