using System.Data;

using DataTablePrettyPrinter;
using Engine;
using Scada.Comm.Drivers.DrvTextParserInDatabaseJP;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// processes database import commands.
    /// <para>Обрабатывает команды импорта из базы данных.</para>
    /// </summary>
    internal class DriverClient : IDisposable
    {
        #region Variable

        private readonly DrvDbImportPlusProject project;                    // driver configuration
        private readonly List<ImportCmd> lstImportCmds;                     // import commands
        private DatabaseCommand databaseCommand;                            // database command executor
        public static DebugData OnDebug = null;                             // debug callback

        #endregion Variable

        #region Basic

        /// <summary>
        /// initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverClient()
        {
            this.project = new DrvDbImportPlusProject();
            this.lstImportCmds = new List<ImportCmd>();
            this.databaseCommand = new DatabaseCommand();
        }

        /// <summary>
        /// initializes a new instance of the class with driver settings.
        /// <para>Инициализирует новый экземпляр класса с настройками драйвера.</para>
        /// </summary>
        public DriverClient(string path, DrvDbImportPlusProject project, int deviceNum, string pathLog, bool isDll)
        {
            this.project = project ?? new DrvDbImportPlusProject();
            this.lstImportCmds = this.project.ImportCmds ?? new List<ImportCmd>();
            this.databaseCommand = new DatabaseCommand();

            Manager.PathLog = pathLog;
            Manager.PathProject = path;
            Manager.Project = this.project;
            Manager.DeviceNum = deviceNum;
            Manager.IsDll = isDll;
        }

        /// <summary>
        /// releases the resources used by the object.
        /// <para>Освобождает ресурсы, используемые объектом.</para>
        /// </summary>
        public void Dispose()
        {
            databaseCommand?.Dispose();
            databaseCommand = null;
        }

        /// <summary>
        /// sequentially executes import commands.
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
                        if (cmd.Enabled)
                        {
                            try
                            {
                                Process(cmd);
                            }
                            catch (Exception ex)
                            {
                                Debuger.Log(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(ex.Message);
            }
        }

        /// <summary>
        /// executes an import command.
        /// <para>Выполняет команду импорта.</para>
        /// </summary>
        public DataTable Process(ImportCmd cmd)
        {
            DataTable dtData = new DataTable();

            try
            {
                databaseCommand?.Dispose();
                databaseCommand = new DatabaseCommand();

                string query = DriverUtils.ResolveDateTimePatterns(cmd.Query);
                dtData = databaseCommand.Reguest(query, out int rowCount, out string errMsg);
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
                Debuger.Log(ex.Message);
                return dtData;
            }
        }

        /// <summary>
        /// converts a data table to Rapid SCADA tags and returns them to the driver runtime.
        /// <para>Преобразует таблицу данных в теги Rapid SCADA и возвращает их среде выполнения драйвера.</para>
        /// </summary>
        /// <param name="dtData">source data table.</param>
        /// <param name="cmd">import command settings.</param>
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

        #endregion Basic

        #region Log

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

        #region Get tag values from table

        /// <summary>
        /// parses a DataTable to extract tag values, including timestamp information.
        /// </summary>
        /// <param name="dtData">source DataTable with tag data.</param>
        /// <param name="tags">list of tags to search for.</param>
        /// <param name="isColumnBased">true if tags are organized by columns (tag name = column name),
        /// false if tags are organized by rows (tag name in first column).</param>
        /// <returns>list of DriverTag objects with updated values and timestamps.</returns>
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
        /// validates the DataTable structure based on processing mode.
        /// </summary>
        private bool ValidateDataTableStructure(DataTable dtData, bool isColumnBased)
        {
            if (dtData == null || dtData.Columns.Count == 0)
            {
                return false;
            }

            if (isColumnBased)
            {
                // column-based mode requires at least one column
                return dtData.Columns.Count >= 1;
            }
            else
            {
                // row-based mode requires specific columns for name, value, and time
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
        /// processes column-based data where tag names are column headers.
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
        /// processes row-based data where each row represents a tag.
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
        /// checks whether the column stores a tag timestamp.
        /// </summary>
        private static bool IsTimeColumn(string columnName)
        {
            return columnName.Equals("TAGDATETIME", StringComparison.OrdinalIgnoreCase) ||
                   columnName.Equals("TAGTIME", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// converts object to DateTime with error handling.
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
        /// creates a tag data object with value and date.
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
        /// logs validation error based on locale.
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
