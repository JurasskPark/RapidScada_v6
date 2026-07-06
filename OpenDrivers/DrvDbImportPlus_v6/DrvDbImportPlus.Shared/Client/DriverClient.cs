using DataTablePrettyPrinter;
using Engine;
using Google.Protobuf.WellKnownTypes;
using Scada.Comm.Drivers.DrvTextParserInDatabaseJP;
using Scada.Lang;
using System.Data;
using System.Runtime.InteropServices;


namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    internal class DriverClient
    {
        private readonly string pathProject;                                // path project
        private readonly DrvDbImportPlusProject project;                    // configuration
        private readonly List<ImportCmd> lstImportCmds;                     // import cmds
        private readonly List<ExportCmd> lstExportCmds;                     // export cmds
        private DatabaseCommand databaseCommand;                            // database command
        private const string PgTimestampFormat = "yyyy-MM-dd HH:mm:ss.ffffff";

        public DriverClient()
        {
            this.pathProject = string.Empty;
            this.project = new DrvDbImportPlusProject();
        }

        public DriverClient(string path, DrvDbImportPlusProject project, int deviceNum, string pathLog, bool isDll)
        {
            this.pathProject = path;
            this.project = new DrvDbImportPlusProject();
            this.project = project;
            this.lstImportCmds = project?.ImportCmds ?? new List<ImportCmd>();
            this.lstExportCmds = project?.ExportCmds ?? new List<ExportCmd>();
            this.databaseCommand = new DatabaseCommand();
  
            Manager.PathLog = pathLog;
            Manager.PathProject = path;
            Manager.Project = project;
            Manager.DeviceNum = deviceNum;
            Manager.IsDll = isDll;
        }

        #region Log
        /// <summary>
        /// Getting logs
        /// </summary>
        public static DebugData OnDebug = null;
        public delegate void DebugData(string msg);
        // transfer to the form and to the file in the Log folder
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        #endregion Log

        #region Dispose
        private IntPtr _bufferPtr = IntPtr.Zero;
        public int BUFFER_SIZE = 1024 * 1024 * 50; // 50 MB
        private bool _disposed = false;

        ~DriverClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // free any other managed objects here.
            }

            // free any unmanaged objects here.
            Marshal.FreeHGlobal(_bufferPtr);
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(ex.Message.ToString());
            }
        }

        public DataTable Process(ImportCmd cmd)
        {
            DataTable dtData = new DataTable();

            try
            {
                databaseCommand = new DatabaseCommand();

                if (cmd.HistoryEnabled || HistoryQueryHelper.HasPeriodComments(cmd.Query))
                {
                    ProcessHistoryTagImport(cmd);
                    return dtData;
                }

                dtData = databaseCommand.Reguest(cmd.Query, out int rowCount, out string errMsg);

                Debuger.Log(Environment.NewLine + dtData.ToPrettyPrintedString(), false);

                List<DriverTag> tags = GetTagValues(dtData, cmd.DeviceTags, cmd.IsColumnBased);

                Debuger.Log(Environment.NewLine + tags.ConvertTagsToTable("Tags"), false);

                DebugerTagReturn tagReturn = new DebugerTagReturn();
                tagReturn.Return(tags);

                return dtData;
            }
            catch (Exception ex)
            {
                Debuger.Log(ex.Message.ToString());
                return dtData;
            }
        }

        #endregion Process

        private void ProcessHistoryTagImport(ImportCmd cmd)
        {
            if (!HistoryQueryHelper.TryParsePeriod(cmd.Query, out DateTime startTime, out DateTime endTime, out string errMsg))
            {
                Debuger.Log(errMsg);
                return;
            }

            List<HistoryQueryWindow> windows = HistoryQueryHelper.BuildWindows(
                startTime,
                endTime,
                cmd.HistoryWindowMinutes);

            LogHistoryStart(cmd, startTime, endTime, windows.Count);

            for (int i = 0; i < windows.Count; i++)
            {
                HistoryQueryWindow window = windows[i];
                string query = HistoryQueryHelper.RenderWindowQuery(cmd.Query, window);
                LogHistoryWindow(i + 1, windows.Count, window, query);

                DataTable windowData = databaseCommand.Reguest(query, out int rowCount, out string requestErrMsg);
                if (!string.IsNullOrEmpty(requestErrMsg))
                {
                    Debuger.Log(string.Format(Locale.IsRussian ?
                        "Ошибка исторического импорта: {0}" :
                        "Historical import error: {0}", requestErrMsg));

                    if (cmd.HistoryStopOnError)
                    {
                        break;
                    }
                }

                List<DriverTag> tags = GetTagValues(windowData, cmd.DeviceTags, cmd.IsColumnBased);
                int historyCount = tags.Count(tag => tag.Date != DateTime.MinValue);

                Debuger.Log(string.Format(Locale.IsRussian ?
                    "Исторический импорт: прочитано строк {0}, подготовлено исторических значений {1}." :
                    "Historical import: read rows {0}, prepared historical values {1}.",
                    rowCount,
                    historyCount));

                ReturnTags(cmd, tags);
            }
        }


        private static void ReturnTags(ImportCmd cmd, List<DriverTag> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return;
            }

            int batchSize = cmd.HistoryBatchSize;
            if (batchSize <= 0 || batchSize >= tags.Count)
            {
                DebugerTagReturn tagReturn = new DebugerTagReturn();
                tagReturn.Return(tags);
                return;
            }

            for (int index = 0; index < tags.Count; index += batchSize)
            {
                DebugerTagReturn tagReturn = new DebugerTagReturn();
                tagReturn.Return(tags.Skip(index).Take(batchSize).ToList());
            }
        }
        private static void LogHistoryStart(ImportCmd cmd, DateTime startTime, DateTime endTime, int windowCount)
        {
            string cmdName = string.IsNullOrWhiteSpace(cmd.Name) ? cmd.CmdCode : cmd.Name;

            Debuger.Log(string.Format(Locale.IsRussian ?
                "Исторический импорт: {0}. Период: {1} - {2}. Окон: {3}." :
                "Historical import: {0}. Period: {1} - {2}. Windows: {3}.",
                cmdName,
                startTime.ToString(PgTimestampFormat),
                endTime.ToString(PgTimestampFormat),
                windowCount));
        }

        private static void LogHistoryWindow(
            int windowNumber,
            int windowCount,
            HistoryQueryWindow window,
            string query)
        {
            Debuger.Log(string.Format(Locale.IsRussian ?
                "Окно {0}/{1}: {2} - {3}." :
                "Window {0}/{1}: {2} - {3}.",
                windowNumber,
                windowCount,
                window.StartTime.ToString(PgTimestampFormat),
                window.EndTime.ToString(PgTimestampFormat)));

            Debuger.Log(query);
        }
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
                if (dtData == null || dtData.Columns.Count == 0)
                {
                    LogValidationError(isColumnBased);
                    return resultTags;
                }

                if (dtData.Rows.Count == 0)
                {
                    return resultTags;
                }

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
        /// Validates the DataTable structure based on processing mode.
        /// </summary>
        private bool ValidateDataTableStructure(DataTable dtData, bool isColumnBased)
        {
            if (dtData == null || dtData.Columns.Count == 0)
            {
                return false;
            }

            if (isColumnBased)
            {
                // column-based mode requires at least one column.
                return dtData.Columns.Count >= 1;
            }
            else
            {
                // row-based mode requires specific columns for name, value, and time.
                return dtData.Columns.Count >= 2 &&
                       ContainsColumn(dtData, "TAGNAME") &&
                       ContainsColumn(dtData, "TAGVALUE");
            }
        }

        private static bool ContainsColumn(DataTable dtData, string columnName)
        {
            foreach (DataColumn column in dtData.Columns)
            {
                if (column.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
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

            if (tagDateTimeColumnIndex >= 0)
            {
                ProcessColumnBasedRows(dtData, tagLookup, resultTags, tagDateTimeColumnIndex);
                return;
            }

            DataRow firstRow = dtData.Rows[0];

            if (tagNameColumnIndex >= 0 && tagValueColumnIndex >= 0)
            {
                object tagNameObj = firstRow[tagNameColumnIndex];
                if (tagNameObj != null && tagNameObj != DBNull.Value)
                {
                    string tagName = tagNameObj.ToString();
                    if (!string.IsNullOrEmpty(tagName) && tagLookup.TryGetValue(tagName, out DriverTag tag))
                    {
                        object tagValue = firstRow[tagValueColumnIndex];
                        resultTags.Add(CreateTagDataWithDate(tag, tagValue, DateTime.MinValue));
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
                        resultTags.Add(CreateTagDataWithDate(tag, value, DateTime.MinValue));
                    }
                }
            }
        }

        private void ProcessColumnBasedRows(
            DataTable dtData,
            Dictionary<string, DriverTag> tagLookup,
            List<DriverTag> resultTags,
            int tagDateTimeColumnIndex)
        {
            foreach (DataRow row in dtData.Rows)
            {
                DateTime timestamp = ConvertToDateTime(row[tagDateTimeColumnIndex]);

                foreach (DataColumn column in dtData.Columns)
                {
                    string columnName = column.ColumnName;

                    if (IsTimeColumn(columnName))
                    {
                        continue;
                    }

                    if (tagLookup.TryGetValue(columnName, out DriverTag tag))
                    {
                        object value = row[column];
                        if (value != null && value != DBNull.Value)
                        {
                            resultTags.Add(CreateTagDataWithDate(tag, value, timestamp));
                        }
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


    }
}
