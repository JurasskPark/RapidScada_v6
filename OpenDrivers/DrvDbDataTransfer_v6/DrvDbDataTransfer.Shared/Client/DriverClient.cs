using DataTablePrettyPrinter;
using Engine;
using Google.Protobuf.WellKnownTypes;
using Scada.Comm.Drivers.DrvTextParserInDatabaseJP;
using Scada.Lang;
using System.Data;


namespace Scada.Comm.Drivers.DrvDbDataTransfer
{
    /// <summary>
    /// Executes data transfer commands and returns tag values to Rapid SCADA.
    /// <para>Выполняет команды переноса данных и возвращает значения тегов в Rapid SCADA.</para>
    /// </summary>
    internal class DriverClient : IDisposable
    {
        #region Variable

        private readonly string pathProject;                                // path project
        private readonly DrvDbDataTransferProject project;                  // configuration
        private readonly List<ImportCmd> lstImportCmds;                     // import cmds
        private readonly List<ExportCmd> lstExportCmds;                     // export cmds
        private DatabaseCommand databaseCommand;                            // database command

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new empty instance of the class.
        /// <para>Инициализирует новый пустой экземпляр класса.</para>
        /// </summary>
        public DriverClient()
        {
            this.pathProject = string.Empty;
            this.project = new DrvDbDataTransferProject();
            this.lstImportCmds = new List<ImportCmd>();
            this.lstExportCmds = new List<ExportCmd>();
            this.databaseCommand = new DatabaseCommand();
        }

        /// <summary>
        /// Initializes a new instance of the class with project settings.
        /// <para>Инициализирует новый экземпляр класса с настройками проекта.</para>
        /// </summary>
        /// <param name="path">Project file path.</param>
        /// <param name="project">Driver project configuration.</param>
        /// <param name="deviceNum">Device number.</param>
        /// <param name="pathLog">Log directory path.</param>
        /// <param name="isDll">Indicates that the driver runs as a DLL.</param>
        public DriverClient(string path, DrvDbDataTransferProject project, int deviceNum, string pathLog, bool isDll)
        {
            this.pathProject = path;
            this.project = project ?? new DrvDbDataTransferProject();
            this.lstImportCmds = this.project.ImportCmds ?? new List<ImportCmd>();
            this.lstExportCmds = this.project.ExportCmds ?? new List<ExportCmd>();
            this.databaseCommand = new DatabaseCommand();
  
            Manager.PathLog = pathLog;
            Manager.PathProject = path;
            Manager.Project = this.project;
            Manager.DeviceNum = deviceNum;
            Manager.IsDll = isDll;
        }

        #endregion Basic

        #region Dispose

        /// <summary>
        /// Releases resources used by the client.
        /// <para>Освобождает ресурсы клиента.</para>
        /// </summary>
        public void Dispose()
        {
            databaseCommand?.Dispose();
            databaseCommand = null;
        }

        #endregion Dispose

        #region Process

        /// <summary>
        /// Sequential execution of tasks
        /// <para>Последовательное выполнение задач</para>
        /// </summary>
        public void Process()
        {
            try
            {
                Debuger.Log(Locale.IsRussian ?
                       @$"Количество команд импорта данных: {lstImportCmds.Count}." :
                       @$"Count of data import commands: {lstImportCmds.Count}.");

                if (lstImportCmds.Count > 0)
                {
                    foreach (ImportCmd cmd in lstImportCmds)
                    {
                        if (cmd.Enabled == true)
                        {
                            try
                            {
                                Process(cmd);
                            }
                            catch (Exception ex)
                            {
                                Debuger.Log(ex.Message.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Executes a single import or transfer command.
        /// <para>Выполняет одну команду импорта или переноса данных.</para>
        /// </summary>
        /// <param name="cmd">Import command settings.</param>
        /// <returns>Data table read by the command.</returns>
        public DataTable Process(ImportCmd cmd)
        {
            DataTable dtData = new DataTable();

            try
            {
                databaseCommand?.Dispose();
                databaseCommand = new DatabaseCommand();

                if (!string.IsNullOrWhiteSpace(cmd.InsertQuery))
                {
                    DbTransferResult transferResult = databaseCommand.Transfer(project, cmd);
                    dtData = transferResult.SourceData ?? new DataTable();

                    if (!transferResult.Success || transferResult.ReadRows > 0)
                    {
                        LogTransferResult(cmd, transferResult);
                    }

                    if (transferResult.Success && transferResult.ReadRows > 0)
                    {
                        ReturnTagsFromDataTable(dtData, cmd);
                    }

                    return dtData;
                }

                string selectQueryForImport = DriverUtils.ResolveDateTimePatterns(cmd.SelectQuery);

                dtData = databaseCommand.Reguest(selectQueryForImport, out int rowCount, out string errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    Debuger.Log(errMsg);
                    return dtData;
                }

                if (rowCount == 0)
                {
                    return dtData;
                }

                ReturnTagsFromDataTable(dtData, cmd);

                return dtData;
            }
            catch (Exception ex)
            {
                Debuger.Log(ex.Message.ToString());
                return dtData;
            }
        }

        #endregion Process

        #region Get tag values from table

        /// <summary>
        /// Parses a DataTable to extract tag values, including timestamp information.
        /// </summary>
        /// <param name="dtData">Source DataTable with tag data.</param>
        /// <param name="tags">List of tags to search for.</param>
        /// <param name="isColumnBased">True if tags are organized by columns (tag name = column name),
        /// False if tags are organized by rows (tag name in first column).</param>
        /// <returns>List of DriverTag objects with updated values and timestamps.</returns>
        public List<DriverTag> GetTagValues(DataTable dtData, List<DriverTag> tags, bool isColumnBased)
        {
            List<DriverTag> resultTags = new List<DriverTag>();

            if (tags == null || tags.Count == 0)
            {
                return resultTags;
            }

            try
            {
                // validate data table structure
                if (!ValidateDataTableStructure(dtData, isColumnBased))
                {
                    LogValidationError(isColumnBased);
                    return resultTags;
                }

                // create lookup dictionary for faster tag access
                var tagLookup = tags
                    .Where(tag => tag != null && tag.Enabled)
                    .GroupBy(tag => tag.Name, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);

                if (isColumnBased)
                {
                    ProcessColumnBasedData(dtData, tagLookup, resultTags);
                }
                else
                {
                    ProcessRowBasedData(dtData, tagLookup, resultTags);
                }
            }
            catch (Exception ex)
            {
                Debuger.Log($"Error processing tag values: {ex.Message}");
            }

            return resultTags;
        }

        /// <summary>
        /// Converts a data table to Rapid SCADA tags and returns them to the driver runtime.
        /// <para>Преобразует таблицу данных в теги Rapid SCADA и возвращает их среде выполнения драйвера.</para>
        /// </summary>
        /// <param name="dtData">Source data table.</param>
        /// <param name="cmd">Import command settings.</param>
        private void ReturnTagsFromDataTable(DataTable dtData, ImportCmd cmd)
        {
            if (dtData == null || dtData.Rows.Count == 0 || cmd?.DeviceTags == null || cmd.DeviceTags.Count == 0)
            {
                return;
            }

            Debuger.Log(Environment.NewLine + dtData.ToPrettyPrintedString(), false);

            List<DriverTag> tags = GetTagValues(dtData, cmd.DeviceTags, cmd.IsColumnBased);
            if (tags.Count == 0)
            {
                return;
            }

            Debuger.Log(Environment.NewLine + tags.ConvertTagsToTable("Tags"), false);

            DebugerTagReturn tagReturn = new DebugerTagReturn();
            tagReturn.Return(tags);
        }

        /// <summary>
        /// Logs data transfer result when rows were read or an error occurred.
        /// <para>Записывает результат переноса данных, когда прочитаны строки или произошла ошибка.</para>
        /// </summary>
        /// <param name="cmd">Import command settings.</param>
        /// <param name="transferResult">Transfer execution result.</param>
        private void LogTransferResult(ImportCmd cmd, DbTransferResult transferResult)
        {
            string cmdName = string.IsNullOrWhiteSpace(cmd.Name) ? cmd.CmdCode : cmd.Name;

            Debuger.Log(string.Format(Locale.IsRussian ?
                "Перенос данных: {0}" :
                "Data transfer: {0}", cmdName));

            Debuger.Log(string.Format(Locale.IsRussian ?
                "Источник: {0}. Приемник: {1}." :
                "Source: {0}. Target: {1}.",
                project.SourceDbConnSettings.DataSourceType,
                project.TargetDbConnSettings.DataSourceType));

            Debuger.Log(string.Format(Locale.IsRussian ?
                "Прочитано строк: {0}. Записано строк: {1}." :
                "Read rows: {0}. Written rows: {1}.",
                transferResult.ReadRows,
                transferResult.WrittenRows));

            if (!transferResult.Success)
            {
                Debuger.Log(string.Format(Locale.IsRussian ?
                    "Ошибка переноса: {0}" :
                    "Transfer error: {0}", transferResult.ErrorMessage));
            }
        }

        /// <summary>
        /// Validates the DataTable structure based on processing mode.
        /// </summary>
        private bool ValidateDataTableStructure(DataTable dtData, bool isColumnBased)
        {
            if (dtData == null || dtData.Columns.Count == 0 || dtData.Rows.Count == 0)
            {
                return false;
            }

            if (isColumnBased)
            {
                // column-based mode requires at least one column and one row
                return dtData.Columns.Count >= 1 && dtData.Rows.Count >= 1;
            }
            else
            {
                // row-based mode requires specific columns for name, value, and time
                return dtData.Columns.Count >= 2 &&
                       dtData.Columns.Contains("TAGNAME") &&
                       dtData.Columns.Contains("TAGVALUE");
            }
        }

        /// <summary>
        /// Processes column-based data where tag names are column headers.
        /// </summary>
        private void ProcessColumnBasedData(DataTable dtData, Dictionary<string, DriverTag> tagLookup, List<DriverTag> resultTags)
        {
            if (dtData.Rows.Count == 0)
            {
                return;
            }

            DataRow firstRow = dtData.Rows[0];

            int tagNameColumnIndex = -1;
            int tagValueColumnIndex = -1;
            int tagDateTimeColumnIndex = -1;

            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                string columnName = dtData.Columns[i].ColumnName;

                if (columnName.Equals("TAGNAME", StringComparison.OrdinalIgnoreCase))
                {
                    tagNameColumnIndex = i;
                }
                else if (columnName.Equals("TAGVALUE", StringComparison.OrdinalIgnoreCase))
                {
                    tagValueColumnIndex = i;
                }
                else if (IsTimeColumn(columnName))
                {
                    tagDateTimeColumnIndex = i;
                }
            }

            DateTime commonTimestamp = DateTime.MinValue;
            if (tagDateTimeColumnIndex >= 0)
            {
                object dateTimeValue = firstRow[tagDateTimeColumnIndex];
                commonTimestamp = ConvertToDateTime(dateTimeValue);
            }

            if (tagNameColumnIndex >= 0 && tagValueColumnIndex >= 0)
            {
                object tagNameObj = firstRow[tagNameColumnIndex];
                if (tagNameObj != null && tagNameObj != DBNull.Value)
                {
                    string tagName = tagNameObj.ToString();
                    if (!string.IsNullOrEmpty(tagName) && tagLookup.TryGetValue(tagName, out DriverTag tag))
                    {
                        object tagValue = firstRow[tagValueColumnIndex];
                        resultTags.Add(CreateTagDataWithDate(tag, tagValue, commonTimestamp));
                    }
                }
            }
            else
            {
                foreach (DataColumn column in dtData.Columns)
                {
                    string columnName = column.ColumnName;

                    if (IsTimeColumn(columnName))
                    {
                        continue;
                    }

                    if (tagLookup.TryGetValue(columnName, out DriverTag tag))
                    {
                        object value = firstRow[column];
                        resultTags.Add(CreateTagDataWithDate(tag, value, commonTimestamp));
                    }
                }
            }
        }

        /// <summary>
        /// Processes row-based data where each row represents a tag.
        /// </summary>
        private void ProcessRowBasedData(DataTable dtData, Dictionary<string, DriverTag> tagLookup, List<DriverTag> resultTags)
        {
            int nameColumnIndex = -1;
            int valueColumnIndex = -1;
            int timeColumnIndex = -1;

            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                string columnName = dtData.Columns[i].ColumnName;

                if (columnName.Equals("TAGNAME", StringComparison.OrdinalIgnoreCase))
                {
                    nameColumnIndex = i;
                }
                else if (columnName.Equals("TAGVALUE", StringComparison.OrdinalIgnoreCase))
                {
                    valueColumnIndex = i;
                }
                else if (IsTimeColumn(columnName))
                {
                    timeColumnIndex = i;
                }
            }

            if (nameColumnIndex == -1 || valueColumnIndex == -1)
            {
                return;
            }

            foreach (DataRow row in dtData.Rows)
            {
                object tagNameObj = row[nameColumnIndex];
                if (tagNameObj == null || tagNameObj == DBNull.Value)
                {
                    continue;
                }

                string tagName = tagNameObj.ToString();
                if (string.IsNullOrEmpty(tagName))
                {
                    continue;
                }

                if (tagLookup.TryGetValue(tagName, out DriverTag tag))
                {
                    object value = row[valueColumnIndex];

                    DateTime timestamp = DateTime.MinValue;
                    if (timeColumnIndex >= 0)
                    {
                        object timeObj = row[timeColumnIndex];
                        if (timeObj != null && timeObj != DBNull.Value)
                        {
                            timestamp = ConvertToDateTime(timeObj);
                        }
                    }

                    resultTags.Add(CreateTagDataWithDate(tag, value, timestamp));
                }
            }
        }

        /// <summary>
        /// Checks whether the column stores a tag timestamp.
        /// </summary>
        private static bool IsTimeColumn(string columnName)
        {
            return columnName.Equals("TAGDATETIME", StringComparison.OrdinalIgnoreCase) ||
                columnName.Equals("TAGTIME", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Converts object to DateTime with error handling.
        /// </summary>
        private DateTime ConvertToDateTime(object value)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    return DateTime.MinValue;
                }

                if (value is DateTime dateTimeValue)
                {
                    return dateTimeValue.Kind == DateTimeKind.Utc
                        ? dateTimeValue
                        : dateTimeValue.ToUniversalTime();
                }

                if (value is DateTimeOffset dateTimeOffsetValue)
                {
                    return dateTimeOffsetValue.UtcDateTime;
                }

                if (DateTime.TryParse(value.ToString(), out DateTime parsedDate))
                {
                    return parsedDate.Kind == DateTimeKind.Utc
                        ? parsedDate
                        : parsedDate.ToUniversalTime();
                }

                return DateTime.MinValue;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Creates a tag data object with value and date.
        /// </summary>
        private DriverTag CreateTagDataWithDate(DriverTag tag, object value, DateTime date)
        {
            return new DriverTag
            {
                Id = tag.Id,
                Name = tag.Name,
                Code = tag.Code,
                Format = tag.Format,
                Enabled = tag.Enabled,
                Val = value,
                Stat = 1,
                Date = date,
                NumberDecimalPlaces = tag.NumberDecimalPlaces
            };
        }

        /// <summary>
        /// Logs validation error based on locale.
        /// </summary>
        private void LogValidationError(bool isColumnBased)
        {
            string errorMessage = isColumnBased
                ? (Locale.IsRussian
                    ? "Недостаточно столбцов или записей для обработки данных в колоночном режиме."
                    : "Insufficient columns or records to process data in column-based mode.")
                : (Locale.IsRussian
                    ? "Таблица должна содержать колонки TAGNAME, TAGVALUE и опционально TAGTIME или TAGDATETIME для строчного режима."
                    : "Table must contain TAGNAME, TAGVALUE and optionally TAGTIME or TAGDATETIME columns for row-based mode.");

            Debuger.Log(errorMessage);
        }

        #endregion Get tag values from table

        #region Log

        /// <summary>
        /// Occurs when the driver writes a debug message.
        /// <para>Возникает при записи отладочного сообщения драйвером.</para>
        /// </summary>
        public static DebugData OnDebug = null;

        /// <summary>
        /// Represents a method that receives debug text.
        /// <para>Представляет метод, принимающий отладочный текст.</para>
        /// </summary>
        /// <param name="msg">Debug message text.</param>
        public delegate void DebugData(string msg);

        /// <summary>
        /// Sends debug text to the subscribed form handler.
        /// <para>Передает отладочный текст подписчику формы.</para>
        /// </summary>
        /// <param name="text">Debug message text.</param>
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        #endregion Log

    }
}
