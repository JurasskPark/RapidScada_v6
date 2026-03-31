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
        public static DebugData OnDebug;
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
        private IntPtr _bufferPtr;
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
            if (tags == null || tags.Count == 0)
            {
                return tags;
            }

            try
            {
                // validate data table structure
                if (!ValidateDataTableStructure(dtData, isColumnBased))
                {
                    LogValidationError(isColumnBased);
                    return tags;
                }

                // create lookup dictionary for faster tag access
                var tagLookup = tags
                    .Where(tag => tag != null && tag.Enabled)
                    .ToDictionary(tag => tag.Name, tag => tag, StringComparer.OrdinalIgnoreCase);

                if (isColumnBased)
                {
                    ProcessColumnBasedData(dtData, tagLookup);
                }
                else
                {
                    ProcessRowBasedData(dtData, tagLookup);
                }
            }
            catch (Exception ex)
            {
                Debuger.Log($"Error processing tag values: {ex.Message}");
            }

            return tags;
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
                       dtData.Columns.Contains("TAGVALUE") &&
                     (!dtData.Columns.Contains("TAGDATETIME") || dtData.Columns.Contains("TAGDATETIME"));
            }
        }

        /// <summary>
        /// Processes column-based data where tag names are column headers.
        /// </summary>
        private void ProcessColumnBasedData(DataTable dtData, Dictionary<string, DriverTag> tagLookup)
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
                else if (columnName.Equals("TAGDATETIME", StringComparison.OrdinalIgnoreCase))
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
                        DateTime timestamp = commonTimestamp != DateTime.MinValue ? commonTimestamp : DateTime.MinValue;
                        SetTagDataWithDate(tag, tagValue, timestamp);
                    }
                }
            }
            else
            {
                foreach (DataColumn column in dtData.Columns)
                {
                    string columnName = column.ColumnName;

                    if (columnName.Equals("TAGDATETIME", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (tagLookup.TryGetValue(columnName, out DriverTag tag))
                    {
                        object value = firstRow[column];
                        DateTime timestamp = commonTimestamp != DateTime.MinValue ? commonTimestamp : DateTime.MinValue;
                        SetTagDataWithDate(tag, value, timestamp);
                    }
                }
            }
        }

        /// <summary>
        /// Processes row-based data where each row represents a tag.
        /// </summary>
        private void ProcessRowBasedData(DataTable dtData, Dictionary<string, DriverTag> tagLookup)
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
                else if (columnName.Equals("TAGDATETIME", StringComparison.OrdinalIgnoreCase))
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

                    SetTagDataWithDate(tag, value, timestamp);
                }
            }
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
                    return DateTime.UtcNow;
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
                    return parsedDate;
                }

                return DateTime.UtcNow;
            }
            catch
            {
                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Updates tag with value and date.
        /// </summary>
        private DriverTag SetTagDataWithDate(DriverTag tag, object value, DateTime date)
        {
            // update tag value
            tag.Val = value;
            tag.Date = date;
            tag.Stat = 1;

            return tag;
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
                    ? "Таблица должна содержать колонки TAGNAME, TAGVALUE или TAGDATETIME для строчного режима."
                    : "Table must contain TAGNAME, TAGVALUE or TAGDATETIME columns for row-based mode.");

            Debuger.Log(errorMessage);
        }

        #endregion Get tag values from table


    }
}
