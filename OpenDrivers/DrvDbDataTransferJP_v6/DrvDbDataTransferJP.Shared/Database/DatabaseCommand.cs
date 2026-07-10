using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using Engine;
using Scada.Comm.Drivers.DrvDbDataTransferJP;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvTextParserInDatabaseJP
{
    /// <summary>
    /// Класс для выполнения SQL-команд в базе данных.
    /// Предоставляет методы для выполнения запросов и получения результатов в виде таблиц или количества измененных строк.
    /// </summary>
    public class DatabaseCommand : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DatabaseCommand() 
        {
            
        }

        /// <summary>
        /// Class finalizer.
        /// </summary>
        ~DatabaseCommand()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the DatabaseCommand object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">True to release managed resources; False to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (dtData != null)
                {
                    dtData.Dispose();
                    dtData = null;
                }

                if (dtSchema != null)
                {
                    dtSchema.Dispose();
                    dtSchema = null;
                }

                if (dataSource != null)
                {
                    if (dataSource is IDisposable disposableDataSource)
                    {
                        disposableDataSource.Dispose();
                    }
                    dataSource = null;
                }
            }

            disposed = true;
        }

        /// <summary>
        /// Project configuration.
        /// </summary>
        public static DrvDbDataTransferJPProject project = new DrvDbDataTransferJPProject();

        /// <summary>
        /// Data source for database connection.
        /// </summary>
        private DataSource dataSource;

        /// <summary>
        /// Table with query result data.
        /// </summary>
        private DataTable dtData = new DataTable("Data");

        /// <summary>
        /// Table with query result data schema.
        /// </summary>
        private DataTable dtSchema = new DataTable("Schema");

        /// <summary>
        /// Resource release flag.
        /// </summary>
        private bool disposed = false;

        #region Main execution methods

        /// <summary>
        /// Tests database connection using the specified connection settings.
        /// </summary>
        /// <param name="connSettings">Connection settings to test.</param>
        /// <param name="errMsg">Error message if the connection fails.</param>
        /// <returns>True if the connection is successful; otherwise, False.</returns>
        public bool ConnectionTest(DbConnSettings connSettings, out string errMsg)
        {
            errMsg = string.Empty;

            // get data source type
            dataSource = DataSource.GetDataSourceType(connSettings);

            if (dataSource == null)
            {
                errMsg = Locale.IsRussian ?
                    "Тип источника данных не поддерживается" :
                    "Data source type is not supported";
                return false;
            }

            // build connection string
            string connStr = string.IsNullOrEmpty(connSettings.ConnectionString) ?
                dataSource.BuildConnectionString(connSettings) :
                connSettings.ConnectionString;

            if (string.IsNullOrEmpty(connStr))
            {
                errMsg = Locale.IsRussian ?
                    "Строка подключения не может быть пустой" :
                    "Connection string cannot be empty";
                return false;
            }

            try
            {
                // initialize and test connection
                dataSource.Init(connSettings);
                dataSource.Connect();
                dataSource.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Tests database connection using the specified configuration.
        /// </summary>
        /// <param name="project">Project configuration for connection.</param>
        /// <param name="errMsg">Error message if the connection fails.</param>
        /// <returns>True if the connection is successful; otherwise, False.</returns>
        public bool ConnectionTest(DrvDbDataTransferJPProject project, out string errMsg)
        {
            return ConnectionTest(project.SourceDbConnSettings, out errMsg);
        }

        /// <summary>
        /// Executes an SQL script and returns the result as a data table.
        /// </summary>
        /// <param name="script">SQL script to execute.</param>
        /// <returns>Data table with query results.</returns>
        public DataTable Reguest(string script)
        {
            DataTable dataTable = new DataTable();
            string errMsg = string.Empty;

            project = Manager.Project;

            // load configuration if the connection string is empty
            if (string.IsNullOrEmpty(Manager.Project.SourceDbConnSettings.ConnectionString))
            {
                string projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    DriverUtils.GetFileName(Manager.DeviceNum));
                project.Load(projectPath, out errMsg);
            }

            // initialize data source if it hasn't been initialized
            if (dataSource == null)
            {
                InitDataSource(project);
            }

            // query validation and execution
            if (ValidateDataSource())
            {
                if (Connect())
                {
                    dataTable = Request(script);
                    Disconnect();
                }
            }
            else
            {
                Debuger.Log(errMsg);
            }

            return dataTable;
        }

        /// <summary>
        /// Executes an SQL query and returns data in table format.
        /// </summary>
        /// <param name="request">SQL query to execute.</param>
        /// <param name="dtData">Table with query results.</param>
        /// <param name="rowCount">Number of returned rows.</param>
        /// <param name="errMsg">Error message if execution fails.</param>
        public void Reguest(string request, out DataTable dtData, out int rowCount, out string errMsg)
        {
            dtData = new DataTable();
            DataTable dtSchema = new DataTable();
            rowCount = 0;
            errMsg = string.Empty;

            project = Manager.Project;

            // load configuration if the connection string is empty
            if (project == null)
            {
                LoadProjectConfiguration(out errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    return;
                }
            }

            // get data source type
            dataSource = DataSource.GetDataSourceType(project);

            if (dataSource != null)
            {
                // build connection string
                string connStr = string.IsNullOrEmpty(project.SourceDbConnSettings.ConnectionString) ?
                    dataSource.BuildConnectionString(project.SourceDbConnSettings) :
                    project.SourceDbConnSettings.ConnectionString;

                if (string.IsNullOrEmpty(connStr))
                {
                    dataSource = null;
                    errMsg = Locale.IsRussian ?
                        "Строка подключения не может быть пустой" :
                        "Connection string cannot be empty";
                    return;
                }

                try
                {
                    // initialization and connection
                    dataSource.Init(project);

                    // set command text before execution
                    dataSource.SelectCommand.CommandText = request;

                    dataSource.Connect();

                    // execute SELECT query
                    if (IsSelectQuery(request))
                    {
                        dtData = new DataTable("Data");

                        using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            dtSchema = reader.GetSchemaTable();
                            DataColumnCollection columns = dtData.Columns;

                            // create table structure
                            for (int cntRow = 0; cntRow < dtSchema.Rows.Count; cntRow++)
                            {
                                DataRow schemarow = dtSchema.Rows[cntRow];

                                string columnName = string.Empty;
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
                                    dataType = reader.GetFieldType(cntRow);
                                }
                                catch
                                {
                                    dataTypeName = schemarow["DataTypeName"].ToString();
                                    dataType = ElementType.GetElementType(dataTypeName);
                                }

                                if (!columns.Contains(columnName))
                                {
                                    dtData.Columns.Add(columnName, dataType);
                                }
                            }

                            // read data
                            while (reader.Read())
                            {
                                object[] ColArray = new object[reader.FieldCount];
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    ColArray[i] = reader[i] ?? DBNull.Value;
                                }
                                dtData.LoadDataRow(ColArray, true);
                            }
                        }

                        rowCount = dtData.Rows.Count;
                        dataSource.Disconnect();
                    }
                    else
                    {
                        // for non-SELECT queries (INSERT, UPDATE, DELETE, etc.)
                        rowCount = dataSource.SelectCommand.ExecuteNonQuery();
                        dataSource.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    dtData = new DataTable();
                    errMsg = ex.Message;
                    if (dataSource != null)
                    {
                        try { dataSource.Disconnect(); } catch { }
                    }
                }
            }
            else
            {
                errMsg = Locale.IsRussian ?
                    "Источник данных не поддерживается" :
                    "Data source is not supported";
            }
        }

        /// <summary>
        /// Executes an SQL query and returns a data table.
        /// </summary>
        /// <param name="request">SQL query to execute.</param>
        /// <param name="rowCount">Number of returned or affected rows.</param>
        /// <param name="errMsg">Error message if execution fails.</param>
        /// <returns>Table with SELECT query results or empty table for other queries.</returns>
        public DataTable Reguest(string request, out int rowCount, out string errMsg)
        {
            dtData = new DataTable();
            rowCount = 0;
            errMsg = string.Empty;

            try
            {
                project = Manager.Project;

                // load configuration if not loaded
                if (project == null)
                {
                    LoadProjectConfiguration(out errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        return dtData;
                    }
                }

                // initialize data source
                if (!InitializeDataSource(out errMsg))
                {
                    return dtData;
                }

                // set command text before execution
                dataSource.SelectCommand.CommandText = request;

                // execute query depending on its type
                if (IsSelectQuery(request))
                {
                    ExecuteSelectQuery(request, out rowCount, out errMsg);
                }
                else
                {
                    ExecuteNonQuery(request, out rowCount, out errMsg);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return dtData;
        }

        /// <summary>
        /// Executes a scalar query or a set of queries.
        /// </summary>
        /// <param name="request">Main SQL query.</param>
        /// <param name="queryRequestList">List of additional queries to execute.</param>
        /// <param name="dtData">Data table (not actively used in this method).</param>
        /// <param name="rowCount">Total number of affected rows.</param>
        /// <param name="errMsg">Error message if execution fails.</param>
        public void Scalar(string request, List<string> queryRequestList, out DataTable dtData, out int rowCount, out string errMsg)
        {
            dtData = new DataTable();
            rowCount = 0;
            errMsg = string.Empty;

            project = Manager.Project;

            // load configuration if not loaded
            if (project == null)
            {
                LoadProjectConfiguration(out errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    return;
                }
            }

            // determine execution logic based on parameters
            if (!string.IsNullOrEmpty(request) && queryRequestList.Count > 0)
            {
                ExecuteScalar(request, out dtData, out rowCount, out errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    // retry execution of additional queries
                    foreach (string tmpQueryRequest in queryRequestList)
                    {
                        ExecuteScalar(tmpQueryRequest, out dtData, out int tmpRowCount, out errMsg);
                        if (string.IsNullOrEmpty(errMsg))
                        {
                            rowCount += tmpRowCount;
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(request) && queryRequestList.Count == 0)
            {
                ExecuteScalar(request, out dtData, out rowCount, out errMsg);
            }
            else if (string.IsNullOrEmpty(request) && queryRequestList.Count > 0)
            {
                foreach (string tmpQueryRequest in queryRequestList)
                {
                    ExecuteScalar(tmpQueryRequest, out dtData, out int tmpRowCount, out errMsg);
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        rowCount += tmpRowCount;
                    }
                }
            }
        }

        /// <summary>
        /// Executes a scalar SQL query (INSERT, UPDATE, DELETE, etc.).
        /// </summary>
        /// <param name="request">SQL query to execute.</param>
        /// <param name="dtData">Data table (not used for scalar queries).</param>
        /// <param name="rowCount">Number of affected rows.</param>
        /// <param name="errMsg">Error message if execution fails.</param>
        public void ExecuteScalar(string request, out DataTable dtData, out int rowCount, out string errMsg)
        {
            dtData = new DataTable();
            rowCount = 0;
            errMsg = string.Empty;

            try
            {
                project = Manager.Project;

                // load configuration if not loaded
                if (project == null)
                {
                    LoadProjectConfiguration(out errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        return;
                    }
                }

                // initialize data source
                if (!InitializeDataSource(out errMsg))
                {
                    return;
                }

                // execute query
                dataSource.SelectCommand.CommandText = request;
                dataSource.Connect();
                rowCount = dataSource.SelectCommand.ExecuteNonQuery();
                dataSource.Disconnect();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                if (dataSource != null)
                {
                    try { dataSource.Disconnect(); } catch { }
                }
            }
        }

        /// <summary>
        /// Executes a database-to-database transfer command.
        /// </summary>
        public DbTransferResult Transfer(DrvDbDataTransferJPProject project, ImportCmd cmd)
        {
            DbTransferResult result = new DbTransferResult();

            if (project == null)
            {
                result.ErrorMessage = Locale.IsRussian ?
                    "Конфигурация проекта не задана" :
                    "Project configuration is not specified";
                return result;
            }

            if (cmd == null)
            {
                result.ErrorMessage = Locale.IsRussian ?
                    "Команда переноса не задана" :
                    "Transfer command is not specified";
                return result;
            }

            string selectQuery = DriverUtils.ResolveDateTimePatterns(cmd.SelectQuery ?? "");
            if (string.IsNullOrWhiteSpace(selectQuery) || string.IsNullOrWhiteSpace(cmd.InsertQuery))
            {
                result.ErrorMessage = Locale.IsRussian ?
                    "Для переноса должны быть заданы SELECT и INSERT/UPSERT запросы" :
                    "Both SELECT and INSERT/UPSERT queries must be specified for transfer";
                return result;
            }

            try
            {
                DataTable sourceData = ExecuteSelect(project.SourceDbConnSettings, selectQuery);
                result.SourceData = sourceData;
                result.ReadRows = sourceData.Rows.Count;

                if (sourceData.Rows.Count == 0)
                {
                    return result;
                }

                int writtenRows;
                int errorRowCount;
                ExecuteTargetWrites(
                    project.TargetDbConnSettings,
                    cmd.InsertQuery,
                    sourceData,
                    cmd.StopOnError,
                    cmd.BatchSize,
                    out writtenRows,
                    out errorRowCount);

                result.WrittenRows = writtenRows;
                if (errorRowCount > 0)
                {
                    result.ErrorMessage = string.Format(Locale.IsRussian ?
                        "Ошибок записи: {0} из {1} строк" :
                        "Write errors: {0} out of {1} rows", errorRowCount, result.ReadRows);
                }

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Executes a SELECT query against the specified source connection settings.
        /// </summary>
        public DataTable ExecuteSelect(DbConnSettings connSettings, string selectQuery)
        {
            DataSource source = CreateInitializedDataSource(connSettings);
            DataTable dataTable = new DataTable("TransferData");

            try
            {
                source.SelectCommand.CommandText = selectQuery;
                source.Connect();

                using (DbDataReader reader = source.SelectCommand.ExecuteReader(CommandBehavior.Default))
                {
                    DataTable schemaTable = reader.GetSchemaTable();
                    CreateTransferTableStructure(reader, schemaTable, dataTable);

                    while (reader.Read())
                    {
                        object[] rowData = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            rowData[i] = reader[i] ?? DBNull.Value;
                        }

                        dataTable.LoadDataRow(rowData, true);
                    }
                }
            }
            finally
            {
                source.Disconnect();
            }

            return dataTable;
        }

        private void ExecuteTargetWrites(
            DbConnSettings connSettings,
            string insertQuery,
            DataTable sourceData,
            bool stopOnError,
            int batchSize,
            out int writtenRows,
            out int errorRowCount)
        {
            DataSource target = CreateInitializedDataSource(connSettings);
            List<string> parameterNames = ExtractParameterNames(insertQuery);
            writtenRows = 0;
            errorRowCount = 0;

            if (parameterNames.Count == 0)
            {
                throw new InvalidOperationException(Locale.IsRussian ?
                    "INSERT/UPSERT запрос должен содержать параметры, соответствующие колонкам SELECT" :
                    "INSERT/UPSERT query must contain parameters matching SELECT columns");
            }

            try
            {
                target.Connect();

                if (batchSize > 0)
                {
                    ExecuteTargetWritesInBatches(
                        target,
                        insertQuery,
                        sourceData,
                        stopOnError,
                        batchSize,
                        parameterNames,
                        out writtenRows,
                        out errorRowCount);
                    return;
                }

                using (DbTransaction transaction = target.BeginTransaction())
                using (DbCommand insertCommand = target.CreateDbCommand())
                {
                    insertCommand.CommandText = target.NormalizeCommandText(insertQuery);
                    insertCommand.Transaction = transaction;

                    // Log INSERT text with first-row parameter values
                    LogInsertPreview(insertCommand, insertQuery, sourceData, parameterNames);

                    try
                    {
                        int rowNumber = 0;
                        foreach (DataRow row in sourceData.Rows)
                        {
                            rowNumber++;

                            try
                            {
                                ApplyParameters(target, insertCommand, sourceData, row, parameterNames);
                                writtenRows += insertCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                errorRowCount++;

                                string errorMsg = string.Format(Locale.IsRussian ?
                                    "Ошибка записи строки {0}: {1}" :
                                    "Error writing row {0}: {1}", rowNumber, ex.Message);

                                Debuger.Log(errorMsg);

                                if (stopOnError)
                                {
                                    throw new InvalidOperationException(errorMsg, ex);
                                }
                            }
                        }

                        if (errorRowCount == 0 || !stopOnError)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                target.Disconnect();
            }
        }

        private void ExecuteTargetWritesInBatches(
            DataSource target,
            string insertQuery,
            DataTable sourceData,
            bool stopOnError,
            int batchSize,
            List<string> parameterNames,
            out int writtenRows,
            out int errorRowCount)
        {
            writtenRows = 0;
            errorRowCount = 0;
            int rowNumber = 0;

            while (rowNumber < sourceData.Rows.Count)
            {
                using (DbTransaction transaction = target.BeginTransaction())
                using (DbCommand insertCommand = target.CreateDbCommand())
                {
                    insertCommand.CommandText = target.NormalizeCommandText(insertQuery);
                    insertCommand.Transaction = transaction;

                    if (rowNumber == 0)
                    {
                        LogInsertPreview(insertCommand, insertQuery, sourceData, parameterNames);
                    }

                    try
                    {
                        int batchEnd = Math.Min(rowNumber + batchSize, sourceData.Rows.Count);

                        while (rowNumber < batchEnd)
                        {
                            DataRow row = sourceData.Rows[rowNumber];
                            rowNumber++;

                            try
                            {
                                ApplyParameters(target, insertCommand, sourceData, row, parameterNames);
                                writtenRows += insertCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                errorRowCount++;

                                string errorMsg = string.Format(Locale.IsRussian ?
                                    "Ошибка записи строки {0}: {1}" :
                                    "Error writing row {0}: {1}", rowNumber, ex.Message);

                                Debuger.Log(errorMsg);

                                if (stopOnError)
                                {
                                    throw new InvalidOperationException(errorMsg, ex);
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static DataSource CreateInitializedDataSource(DbConnSettings connSettings)
        {
            DataSource dataSource = DataSource.GetDataSourceType(connSettings);
            if (dataSource == null)
            {
                throw new InvalidOperationException(Locale.IsRussian ?
                    "Тип источника данных не задан или не поддерживается" :
                    "Data source type is not set or not supported");
            }

            dataSource.Init(connSettings);
            return dataSource;
        }

        private static void CreateTransferTableStructure(DbDataReader reader, DataTable schemaTable, DataTable dataTable)
        {
            for (int cntRow = 0; cntRow < schemaTable.Rows.Count; cntRow++)
            {
                DataRow schemaRow = schemaTable.Rows[cntRow];
                string columnName = reader.GetName(cntRow);
                Type dataType = reader.GetFieldType(cntRow);

                if (dataType == typeof(object) && schemaRow.Table.Columns.Contains("DataTypeName"))
                {
                    dataType = DbValueConverter.GetClrType(schemaRow["DataTypeName"].ToString());
                }

                if (!dataTable.Columns.Contains(columnName))
                {
                    dataTable.Columns.Add(columnName, dataType);
                }
            }
        }

        private static void ApplyParameters(
            DataSource target,
            DbCommand insertCommand,
            DataTable sourceData,
            DataRow row,
            List<string> parameterNames)
        {
            foreach (string parameterName in parameterNames)
            {
                string columnName = NormalizeParameterName(parameterName);
                DataColumn column = sourceData.Columns
                    .Cast<DataColumn>()
                    .FirstOrDefault(c => string.Equals(c.ColumnName, columnName, StringComparison.OrdinalIgnoreCase));

                if (column == null)
                {
                    throw new InvalidOperationException(string.Format(Locale.IsRussian ?
                        "Параметр {0} не соответствует колонке SELECT" :
                        "Parameter {0} does not match a SELECT column", parameterName));
                }

                try
                {
                    object value = DbValueConverter.ConvertForParameter(row[column], column);
                    target.SetCmdParamWithValue(insertCommand, target.NormalizeParameterName(parameterName), value);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format(Locale.IsRussian ?
                        "Ошибка конвертации параметра {0} из колонки {1}: {2}" :
                        "Error converting parameter {0} from column {1}: {2}",
                        parameterName,
                        column.ColumnName,
                        ex.Message), ex);
                }
            }
        }

        private static List<string> ExtractParameterNames(string query)
        {
            return Regex.Matches(query ?? "", @"(?<!@)([@:][A-Za-z_][A-Za-z0-9_]*)")
                .Cast<Match>()
                .Select(match => match.Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static string NormalizeParameterName(string parameterName)
        {
            return string.IsNullOrWhiteSpace(parameterName) ?
                string.Empty :
                parameterName.Trim().TrimStart('@', ':');
        }

        /// <summary>
        /// Logs INSERT query text and first-row parameter values for debugging.
        /// </summary>
        private static void LogInsertPreview(
            System.Data.Common.DbCommand insertCommand,
            string insertQuery,
            DataTable sourceData,
            List<string> parameterNames)
        {
            if (sourceData.Rows.Count == 0)
            {
                return;
            }

            Debuger.Log(insertQuery);

            DataRow firstRow = sourceData.Rows[0];
            var paramParts = new List<string>();

            foreach (string paramName in parameterNames)
            {
                string colName = NormalizeParameterName(paramName);
                DataColumn col = sourceData.Columns[colName];
                object val = col != null ? firstRow[col] : DBNull.Value;
                string display = val == DBNull.Value || val == null ? "NULL" : val.ToString();
                paramParts.Add($"{paramName}={display}");
            }

            Debuger.Log(string.Join("; ", paramParts));
        }

        #endregion Main execution methods

        #region Helper methods

        /// <summary>
        /// Checks if the data source is initialized and ready for use.
        /// </summary>
        /// <returns>True if the data source is valid; otherwise, False.</returns>
        private bool ValidateDataSource()
        {
            if (dataSource == null)
            {
                Debuger.Log(Locale.IsRussian ?
                    "Нормальное взаимодействие невозможно, т.к. источник данных не определён." :
                    "Normal interaction is not possible because the data source is not defined.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Initializes the data source based on project configuration.
        /// </summary>
        /// <param name="project">Project configuration for initialization.</param>
        public void InitDataSource(DrvDbDataTransferJPProject project)
        {
            dataSource = DataSource.GetDataSourceType(project);

            if (dataSource == null)
            {
                Debuger.Log(Locale.IsRussian ?
                    "Тип источника данных не задан или не поддерживается" :
                    "Data source type is not set or not supported");
                return;
            }

            // initialize data source
            dataSource.Init(project);
        }

        /// <summary>
        /// Connects to the database.
        /// </summary>
        /// <returns>True if the connection is successful; otherwise, False.</returns>
        private bool Connect()
        {
            try
            {
                dataSource.Connect();
                return true;
            }
            catch (Exception ex)
            {
                Debuger.Log(string.Format(Locale.IsRussian ?
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
                if (dataSource != null)
                {
                    dataSource.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(string.Format(Locale.IsRussian ?
                    "Ошибка при разъединении с БД: {0}" :
                    "Error disconnecting from DB: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Executes an SQL query and returns the result as a data table.
        /// </summary>
        /// <param name="script">SQL script to execute.</param>
        /// <returns>Data table with query results.</returns>
        private DataTable Request(string script)
        {
            try
            {
                dtData = new DataTable("Data");

                // set command text before execution
                dataSource.SelectCommand.CommandText = script;

                using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.Default))
                {
                    if (reader.FieldCount == 0)
                    {
                        return null;
                    }

                    // get data schema
                    dtSchema = reader.GetSchemaTable();

                    // create table structure based on schema
                    CreateTableStructure(reader, dtSchema);

                    // read data
                    ReadDataIntoTable(reader);
                }

                return dtData;
            }
            catch (Exception ex)
            {
                Debuger.Log(string.Format(Locale.IsRussian ?
                    "Ошибка при выполнении запроса: {0}" + Environment.NewLine + "{1}" :
                    "Error executing query: {0}" + Environment.NewLine + "{1}", ex.Message, script));
                return dtData;
            }
        }

        #endregion Helper methods

        #region Private helper methods

        /// <summary>
        /// Loads project configuration from file.
        /// </summary>
        /// <param name="errMsg">Error message if loading fails.</param>
        private void LoadProjectConfiguration(out string errMsg)
        {
            errMsg = string.Empty;
            string projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                DriverUtils.GetFileName(Manager.DeviceNum));
            project.Load(projectPath, out errMsg);
        }

        /// <summary>
        /// Initializes the data source for database operations.
        /// </summary>
        /// <param name="errMsg">Error message if initialization fails.</param>
        /// <returns>True if initialization is successful; otherwise, False.</returns>
        private bool InitializeDataSource(out string errMsg)
        {
            errMsg = string.Empty;
            dataSource = DataSource.GetDataSourceType(project);

            if (dataSource == null)
            {
                errMsg = Locale.IsRussian ?
                    "Тип источника данных не поддерживается" :
                    "Data source type is not supported";
                return false;
            }

            dataSource.Init(project);
            return true;
        }

        /// <summary>
        /// Checks if the query is a SELECT query.
        /// </summary>
        /// <param name="request">SQL query to check.</param>
        /// <returns>True if the query starts with SELECT; otherwise, False.</returns>
        private bool IsSelectQuery(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
            {
                return false;
            }

            string trimmedRequest = TrimLeadingSqlComments(request);
            return trimmedRequest.StartsWith("select", StringComparison.OrdinalIgnoreCase);
        }

        private static string TrimLeadingSqlComments(string request)
        {
            int position = 0;

            while (position < request.Length)
            {
                while (position < request.Length && char.IsWhiteSpace(request[position]))
                {
                    position++;
                }

                if (position + 1 < request.Length && request[position] == '-' && request[position + 1] == '-')
                {
                    position += 2;
                    while (position < request.Length && request[position] != '\r' && request[position] != '\n')
                    {
                        position++;
                    }

                    continue;
                }

                if (position + 1 < request.Length && request[position] == '/' && request[position + 1] == '*')
                {
                    int commentEnd = request.IndexOf("*/", position + 2, StringComparison.Ordinal);
                    if (commentEnd < 0)
                    {
                        return string.Empty;
                    }

                    position = commentEnd + 2;
                    continue;
                }

                break;
            }

            return request.Substring(position).TrimStart();
        }

        /// <summary>
        /// Executes a SELECT query and populates a table with data.
        /// </summary>
        /// <param name="request">SQL SELECT query.</param>
        /// <param name="rowCount">Number of returned rows.</param>
        /// <param name="errMsg">Error message if execution fails.</param>
        private void ExecuteSelectQuery(string request, out int rowCount, out string errMsg)
        {
            rowCount = 0;
            errMsg = string.Empty;

            try
            {
                dataSource.Connect();
                dataSource.SelectCommand.CommandText = request;

                dtData = new DataTable("Data");

                using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.Default))
                {
                    dtSchema = reader.GetSchemaTable();
                    CreateTableStructure(reader, dtSchema);
                    ReadDataIntoTable(reader);
                    rowCount = dtData.Rows.Count;
                }

                dataSource.Disconnect();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                if (dataSource != null)
                {
                    try { dataSource.Disconnect(); } catch { }
                }
            }
        }

        /// <summary>
        /// Executes a non-SELECT query (INSERT, UPDATE, DELETE, etc.).
        /// </summary>
        /// <param name="request">SQL query to execute.</param>
        /// <param name="rowCount">Number of affected rows.</param>
        /// <param name="errMsg">Error message if execution fails.</param>
        private void ExecuteNonQuery(string request, out int rowCount, out string errMsg)
        {
            rowCount = 0;
            errMsg = string.Empty;

            try
            {
                dataSource.Connect();
                dataSource.SelectCommand.CommandText = request;
                rowCount = dataSource.SelectCommand.ExecuteNonQuery();
                dataSource.Disconnect();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                if (dataSource != null)
                {
                    try { dataSource.Disconnect(); } catch { }
                }
            }
        }

        /// <summary>
        /// Creates table structure based on data schema from DataReader.
        /// </summary>
        /// <param name="reader">DataReader with data.</param>
        /// <param name="schemaTable">Table with data schema.</param>
        private void CreateTableStructure(DbDataReader reader, DataTable schemaTable)
        {
            DataColumnCollection columns = dtData.Columns;

            for (int cntRow = 0; cntRow < schemaTable.Rows.Count; cntRow++)
            {
                DataRow schemaRow = schemaTable.Rows[cntRow];

                // get column name
                string columnName;
                try
                {
                    columnName = reader.GetName(cntRow);
                }
                catch
                {
                    columnName = schemaRow["ColumnName"].ToString();
                }

                // get data type
                Type dataType;
                try
                {
                    dataType = reader.GetFieldType(cntRow);
                }
                catch
                {
                    string dataTypeName = schemaRow["DataTypeName"].ToString();
                    dataType = ElementType.GetElementType(dataTypeName);
                }

                // check column data correctness
                if (string.IsNullOrEmpty(columnName) || dataType == null)
                {
                    Debuger.Log(Locale.IsRussian ?
                        "Не удалось определить формат данных у столбца таблицы. Информация о столбце таблицы: " :
                        "The data format of the table column could not be determined. Information about the table column: ");
                    Debuger.Log($"Column Number = {cntRow}, Column Name = {columnName}, Data Type = {dataType?.Name ?? "unknown"}.");
                    continue;
                }

                // add column if it does not exist yet
                if (!columns.Contains(columnName))
                {
                    dtData.Columns.Add(columnName, dataType);
                }
            }
        }

        /// <summary>
        /// Reads data from DataReader and loads it into a table.
        /// </summary>
        /// <param name="reader">DataReader with data.</param>
        private void ReadDataIntoTable(DbDataReader reader)
        {
            while (reader.Read())
            {
                object[] rowData = new object[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    rowData[i] = reader[i] ?? DBNull.Value;
                }
                dtData.LoadDataRow(rowData, true);
            }
        }

        #endregion Private helper methods
    }
}
