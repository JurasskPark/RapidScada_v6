// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Adapter for InfluxDB HTTP client to DbConnection interface.
    /// <para>Адаптер клиента HTTP InfluxDB к интерфейсу DbConnection.</para>
    /// </summary>
    internal class InfluxDBHttpConnection : DbConnection
    {
        private InfluxDBHttpClient _client;
        private ConnectionState _state = ConnectionState.Closed;
        private string _connectionString;

        public InfluxDBHttpConnection(InfluxDBHttpClient client)
        {
            _client = client;
        }

        public void UpdateClient(InfluxDBHttpClient client)
        {
            _client = client;
        }

        public override string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value ?? "";
        }

        public override string Database => GetValueFromConnectionString("Database") ??
                                          GetValueFromConnectionString("Bucket") ??
                                          string.Empty;

        public override string DataSource => GetValueFromConnectionString("Url") ?? string.Empty;
        public override string ServerVersion => "InfluxDB HTTP API";
        public override ConnectionState State => _state;

        public override void Open()
        {
            if (_client == null)
                throw new InvalidOperationException("InfluxDB client is not initialized");

            _state = ConnectionState.Open;
        }

        public override void Close()
        {
            _state = ConnectionState.Closed;
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotSupportedException("ChangeDatabase not supported by InfluxDB HTTP API");
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotSupportedException("Transactions not supported by InfluxDB HTTP API");
        }

        protected override DbCommand CreateDbCommand()
        {
            if (_client == null)
                throw new InvalidOperationException("InfluxDB client is not initialized");

            return new InfluxDBHttpCommand(_client)
            {
                Connection = this
            };
        }

        private string GetValueFromConnectionString(string key)
        {
            if (string.IsNullOrEmpty(_connectionString))
                return null;

            var pairs = _connectionString.Split(';');
            foreach (var pair in pairs)
            {
                var keyValue = pair.Split('=', 2);
                if (keyValue.Length == 2 &&
                    keyValue[0].Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return keyValue[1].Trim();
                }
            }

            return null;
        }
    }


    /// <summary>
    /// Adapter for InfluxDB queries to DbCommand interface.
    /// <para>Адаптер запросов InfluxDB к интерфейсу DbCommand.</para>
    /// </summary>
    internal class InfluxDBHttpCommand : DbCommand
    {
        private InfluxDBHttpClient _influxClient;
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();
        private readonly InfluxDBParameterCollection _parameterCollection = new InfluxDBParameterCollection();
        private InfluxDBHttpConnection _connection;
        private DbTransaction _transaction;
        private string _commandText = "";
        private int _commandTimeout = 30;
        private CommandType _commandType = CommandType.Text;
        private bool _designTimeVisible = false;
        private UpdateRowSource _updatedRowSource = UpdateRowSource.None;

        public InfluxDBHttpCommand(InfluxDBHttpClient influxClient)
        {
            _influxClient = influxClient;
        }

        public void UpdateClient(InfluxDBHttpClient client)
        {
            _influxClient = client;
        }

        public override string CommandText
        {
            get => _commandText;
            set => _commandText = value ?? "";
        }

        public override int CommandTimeout
        {
            get => _commandTimeout;
            set => _commandTimeout = value >= 0 ? value : 30;
        }

        public override CommandType CommandType
        {
            get => _commandType;
            set => _commandType = value;
        }

        public override bool DesignTimeVisible
        {
            get => _designTimeVisible;
            set => _designTimeVisible = value;
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get => _updatedRowSource;
            set => _updatedRowSource = value;
        }

        protected override DbConnection DbConnection
        {
            get => _connection;
            set => _connection = value as InfluxDBHttpConnection;
        }

        protected override DbParameterCollection DbParameterCollection => _parameterCollection;

        protected override DbTransaction DbTransaction
        {
            get => _transaction;
            set => _transaction = value;
        }

        public Dictionary<string, object> Parameters => _parameters;

        public override void Cancel()
        {
            // InfluxDB HTTP не поддерживает отмену через стандартный API
        }

        public override int ExecuteNonQuery()
        {
            // InfluxDB HTTP API для запросов только на чтение через /query endpoint
            return 0;
        }

        public override object ExecuteScalar()
        {
            if (_influxClient == null)
                throw new InvalidOperationException("InfluxDB client is not initialized. Command was created before Init completed.");

            if (string.IsNullOrEmpty(CommandText))
                throw new InvalidOperationException("CommandText is not set");

            var result = _influxClient.ExecuteQueryToDictionary(CommandText);

            if (result.Count > 0 && result[0].Count > 0)
            {
                return result[0].Values.First();
            }

            return null;
        }

        public override void Prepare()
        {
            // Не требуется для HTTP запросов
        }

        protected override DbParameter CreateDbParameter()
        {
            return new InfluxDBParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            if (_influxClient == null)
                throw new InvalidOperationException("InfluxDB client is not initialized.");

            if (string.IsNullOrEmpty(CommandText))
                throw new InvalidOperationException("CommandText is not set");

            try
            {
                // Синхронное получение данных
                var json = Task.Run(async () =>
                {
                    return await _influxClient.ExecuteQueryAsync(CommandText, true).ConfigureAwait(false);
                }).Result;

                Debug.WriteLine($"Raw JSON response: {json.Substring(0, Math.Min(500, json.Length))}...");

                // Парсим JSON в DataTable с учетом InfluxDB специфики
                DataTable parsedTable = ParseInfluxJsonToDataTable(json);

                if (parsedTable.Rows.Count == 0)
                {
                    Debug.WriteLine("No rows returned, but query executed successfully");
                }

                return new InfluxDBDataReader(parsedTable);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error executing query: {ex.Message}");
                throw new InvalidOperationException($"Failed to execute query: {ex.Message}", ex);
            }
        }

        private DataTable ParseInfluxJsonToDataTable(string json)
        {
            var dataTable = new DataTable();

            try
            {
                Debug.WriteLine($"Parsing InfluxDB JSON response...");

                // Десериализуем JSON
                var jsonObj = Newtonsoft.Json.Linq.JObject.Parse(json);

                // Проверяем наличие results
                var results = jsonObj["results"] as Newtonsoft.Json.Linq.JArray;
                if (results == null || results.Count == 0)
                {
                    Debug.WriteLine("No results in response");
                    dataTable.Columns.Add("Message", typeof(string));
                    dataTable.Rows.Add("No results returned");
                    return dataTable;
                }

                var firstResult = results[0];

                // Проверяем на ошибки
                var error = firstResult["error"];
                if (error != null)
                {
                    Debug.WriteLine($"InfluxDB error: {error}");
                    dataTable.Columns.Add("Error", typeof(string));
                    dataTable.Rows.Add(error.ToString());
                    return dataTable;
                }

                // Проверяем наличие series
                var series = firstResult["series"] as Newtonsoft.Json.Linq.JArray;
                if (series == null || series.Count == 0)
                {
                    Debug.WriteLine("No series in result (empty query result)");
                    // Создаем пустую таблицу с информационным сообщением
                    dataTable.Columns.Add("Result", typeof(string));
                    dataTable.Rows.Add("Query executed successfully but returned no data");
                    return dataTable;
                }

                // Обрабатываем первую series (обычно это то, что нам нужно)
                var firstSeries = series[0];
                var columns = firstSeries["columns"] as Newtonsoft.Json.Linq.JArray;
                var values = firstSeries["values"] as Newtonsoft.Json.Linq.JArray;

                if (columns == null)
                {
                    Debug.WriteLine("No columns in series");
                    return dataTable;
                }

                // Создаем колонки
                foreach (var column in columns)
                {
                    dataTable.Columns.Add(column.ToString(), typeof(string));
                }

                // Заполняем данные
                if (values != null)
                {
                    foreach (var rowValue in values)
                    {
                        if (rowValue is Newtonsoft.Json.Linq.JArray rowArray)
                        {
                            var row = dataTable.NewRow();
                            for (int i = 0; i < Math.Min(rowArray.Count, dataTable.Columns.Count); i++)
                            {
                                var value = rowArray[i];
                                if (value == null || value.Type == Newtonsoft.Json.Linq.JTokenType.Null)
                                {
                                    row[i] = DBNull.Value;
                                }
                                else
                                {
                                    row[i] = value.ToString();
                                }
                            }
                            dataTable.Rows.Add(row);
                        }
                    }
                }

                Debug.WriteLine($"Created DataTable with {dataTable.Rows.Count} rows and {dataTable.Columns.Count} columns");

                // Логируем структуру таблицы для отладки
                Debug.WriteLine("Table columns:");
                foreach (DataColumn column in dataTable.Columns)
                {
                    Debug.WriteLine($"  {column.ColumnName} ({column.DataType.Name})");
                }

                if (dataTable.Rows.Count > 0)
                {
                    Debug.WriteLine("First row sample:");
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        Debug.WriteLine($"  {dataTable.Columns[i].ColumnName}: {dataTable.Rows[0][i]}");
                    }
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing InfluxDB JSON: {ex.Message}\n{ex.StackTrace}");

                // Создаем таблицу с ошибкой
                dataTable.Columns.Clear();
                dataTable.Columns.Add("Error", typeof(string));
                dataTable.Rows.Add($"Error parsing response: {ex.Message}");
                return dataTable;
            }
        }

        //protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        //{
        //    if (_influxClient == null)
        //        throw new InvalidOperationException("InfluxDB client is not initialized.");

        //    if (string.IsNullOrEmpty(CommandText))
        //        throw new InvalidOperationException("CommandText is not set");

        //    // Синхронное получение DataTable
        //    var dataTable = Task.Run(async () =>
        //    {
        //        return await _influxClient.ExecuteQueryAsync(CommandText, true).ConfigureAwait(false);
        //    }).Result;

        //    // Парсим JSON в DataTable
        //    DataTable parsedTable = ParseJsonToDataTable(dataTable);

        //    return new InfluxDBDataReader(parsedTable);
        //}

        private DataTable ParseJsonToDataTable(string json)
        {
            var dataTable = new DataTable();

            try
            {
                Debug.WriteLine($"Parsing JSON: {json.Substring(0, Math.Min(200, json.Length))}...");

                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                if (jsonObj.results != null && jsonObj.results.Count > 0)
                {
                    var firstResult = jsonObj.results[0];

                    // Проверяем на ошибки
                    if (firstResult.error != null)
                    {
                        string error = firstResult.error;
                        Debug.WriteLine($"InfluxDB error: {error}");
                        throw new InvalidOperationException($"InfluxDB error: {error}");
                    }

                    // Обрабатываем разные типы ответов
                    if (firstResult.series != null && firstResult.series.Count > 0)
                    {
                        ProcessSeries(firstResult.series[0], dataTable);
                    }
                    else if (firstResult.messages != null)
                    {
                        ProcessMessages(firstResult.messages, dataTable);
                    }
                    else
                    {
                        // Пустой результат, но без ошибки
                        Debug.WriteLine("Empty result (no series or messages)");
                        dataTable.Columns.Add("Result", typeof(string));
                        dataTable.Rows.Add("No data returned");
                    }
                }
                else
                {
                    Debug.WriteLine("No results in response");
                    dataTable.Columns.Add("Result", typeof(string));
                    dataTable.Rows.Add("No results in response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Parse error: {ex.Message}");
                throw new InvalidOperationException($"Failed to parse JSON to DataTable: {ex.Message}", ex);
            }

            Debug.WriteLine($"DataTable created: {dataTable.Rows.Count} rows, {dataTable.Columns.Count} columns");
            return dataTable;
        }

        private void ProcessSeries(dynamic series, DataTable dataTable)
        {
            try
            {
                // Получаем колонки
                List<string> columns = new List<string>();
                if (series.columns != null)
                {
                    columns = ((IEnumerable<dynamic>)series.columns).Select(c => (string)c).ToList();
                }

                // Получаем значения
                List<dynamic> values = new List<dynamic>();
                if (series.values != null)
                {
                    values = ((IEnumerable<dynamic>)series.values).ToList();
                }

                Debug.WriteLine($"Processing series: columns={columns.Count}, values={values.Count}");

                // Если нет колонок, создаем стандартную
                if (columns.Count == 0)
                {
                    columns.Add("value");
                }

                // Создаем колонки в DataTable
                foreach (var column in columns)
                {
                    dataTable.Columns.Add(column, typeof(string));
                }

                // Заполняем данные
                foreach (var rowValues in values)
                {
                    if (rowValues is IEnumerable<dynamic> rowArray)
                    {
                        var row = dataTable.NewRow();
                        var valueArray = rowArray.ToArray();

                        for (int i = 0; i < Math.Min(columns.Count, valueArray.Length); i++)
                        {
                            var value = valueArray[i];
                            row[i] = value?.ToString() ?? DBNull.Value;
                        }

                        dataTable.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ProcessSeries: {ex.Message}");

                // Создаем простую таблицу с ошибкой
                dataTable.Columns.Clear();
                dataTable.Columns.Add("Error", typeof(string));
                dataTable.Rows.Add($"Error processing series: {ex.Message}");
            }
        }

        private void ProcessMessages(dynamic messages, DataTable dataTable)
        {
            dataTable.Columns.Add("Message", typeof(string));

            var messagesList = ((IEnumerable<dynamic>)messages).ToList();
            foreach (var message in messagesList)
            {
                if (message.text != null)
                {
                    var row = dataTable.NewRow();
                    row[0] = message.text.ToString();
                    dataTable.Rows.Add(row);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _parameters.Clear();
                _parameterCollection.Clear();
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Parameter collection for InfluxDB commands.
    /// <para>Коллекция параметров для команд InfluxDB.</para>
    /// </summary>
    internal class InfluxDBParameterCollection : DbParameterCollection
    {
        private readonly List<DbParameter> _parameters = new List<DbParameter>();

        public override int Count => _parameters.Count;
        public override object SyncRoot => ((ICollection)_parameters).SyncRoot;

        public override int Add(object value)
        {
            _parameters.Add((DbParameter)value);
            return _parameters.Count - 1;
        }

        public override void AddRange(Array values)
        {
            foreach (var value in values)
            {
                if (value is DbParameter param)
                {
                    _parameters.Add(param);
                }
            }
        }

        public override void Clear()
        {
            _parameters.Clear();
        }

        public override bool Contains(object value)
        {
            return value is DbParameter param && _parameters.Contains(param);
        }

        public override bool Contains(string value)
        {
            return _parameters.Any(p => p.ParameterName == value);
        }

        public override void CopyTo(Array array, int index)
        {
            Array.Copy(_parameters.ToArray(), 0, array, index, _parameters.Count);
        }

        public override IEnumerator GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        public override int IndexOf(object value)
        {
            return value is DbParameter param ? _parameters.IndexOf(param) : -1;
        }

        public override int IndexOf(string parameterName)
        {
            return _parameters.FindIndex(p => p.ParameterName == parameterName);
        }

        public override void Insert(int index, object value)
        {
            if (value is DbParameter param)
            {
                _parameters.Insert(index, param);
            }
        }

        public override void Remove(object value)
        {
            if (value is DbParameter param)
            {
                _parameters.Remove(param);
            }
        }

        public override void RemoveAt(int index)
        {
            if (index >= 0 && index < _parameters.Count)
            {
                _parameters.RemoveAt(index);
            }
        }

        public override void RemoveAt(string parameterName)
        {
            var index = IndexOf(parameterName);
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }

        protected override DbParameter GetParameter(int index)
        {
            if (index >= 0 && index < _parameters.Count)
            {
                return _parameters[index];
            }
            throw new IndexOutOfRangeException($"Index {index} is out of range. Collection has {_parameters.Count} items.");
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return _parameters.FirstOrDefault(p => p.ParameterName == parameterName);
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            if (index >= 0 && index < _parameters.Count)
            {
                _parameters[index] = value;
            }
            else
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range. Collection has {_parameters.Count} items.");
            }
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            var index = IndexOf(parameterName);
            if (index >= 0)
            {
                _parameters[index] = value;
            }
            else
            {
                Add(value);
            }
        }
    }

    /// <summary>
    /// Parameter for InfluxDB queries.
    /// <para>Параметр для запросов InfluxDB.</para>
    /// </summary>
    internal class InfluxDBParameter : DbParameter
    {
        public override DbType DbType { get; set; }
        public override ParameterDirection Direction { get; set; }
        public override bool IsNullable { get; set; }
        public override string ParameterName { get; set; }
        public override int Size { get; set; }
        public override string SourceColumn { get; set; }
        public override bool SourceColumnNullMapping { get; set; }
        public override object Value { get; set; }

        public override void ResetDbType()
        {
            DbType = DbType.Object;
        }
    }

    /// <summary>
    /// Data reader for InfluxDB query results.
    /// <para>Считыватель данных для результатов запросов InfluxDB.</para>
    /// </summary>
    internal class InfluxDBDataReader : DbDataReader
    {
        private readonly DataTable _dataTable;
        private int _currentRowIndex = -1;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public InfluxDBDataReader(DataTable dataTable)
        {
            _dataTable = dataTable ?? new DataTable();
        }

        public override DataTable GetSchemaTable()
        {
            var schemaTable = new DataTable("SchemaTable");

            schemaTable.Columns.Add("ColumnName", typeof(string));
            schemaTable.Columns.Add("ColumnOrdinal", typeof(int));
            schemaTable.Columns.Add("ColumnSize", typeof(int));
            schemaTable.Columns.Add("DataType", typeof(Type));
            schemaTable.Columns.Add("AllowDBNull", typeof(bool));
            schemaTable.Columns.Add("IsReadOnly", typeof(bool));

            for (int i = 0; i < _dataTable.Columns.Count; i++)
            {
                var row = schemaTable.NewRow();
                var column = _dataTable.Columns[i];

                row["ColumnName"] = column.ColumnName;
                row["ColumnOrdinal"] = i;
                row["ColumnSize"] = -1;
                row["DataType"] = column.DataType;
                row["AllowDBNull"] = true;
                row["IsReadOnly"] = true;

                schemaTable.Rows.Add(row);
            }

            return schemaTable;
        }


        public override object this[int ordinal] => _dataTable.Rows[_currentRowIndex][ordinal];
        public override object this[string name] => _dataTable.Rows[_currentRowIndex][name];
        public override int Depth => 0;
        public override int FieldCount => _dataTable.Columns.Count;
        public override bool HasRows => _dataTable.Rows.Count > 0;
        public override bool IsClosed => false;
        public override int RecordsAffected => _dataTable.Rows.Count;

        public override bool Read()
        {
            _currentRowIndex++;
            return _currentRowIndex < _dataTable.Rows.Count;
        }

        public override bool NextResult() => false;
        public override bool GetBoolean(int ordinal) => Convert.ToBoolean(this[ordinal]);
        public override byte GetByte(int ordinal) => Convert.ToByte(this[ordinal]);
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length) => 0;
        public override char GetChar(int ordinal) => Convert.ToChar(this[ordinal]);
        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length) => 0;
        public override string GetDataTypeName(int ordinal) => _dataTable.Columns[ordinal].DataType.Name;
        public override DateTime GetDateTime(int ordinal) => Convert.ToDateTime(this[ordinal]);
        public override decimal GetDecimal(int ordinal) => Convert.ToDecimal(this[ordinal]);
        public override double GetDouble(int ordinal) => Convert.ToDouble(this[ordinal]);
        public override Type GetFieldType(int ordinal) => _dataTable.Columns[ordinal].DataType;
        public override float GetFloat(int ordinal) => Convert.ToSingle(this[ordinal]);
        public override Guid GetGuid(int ordinal) => Guid.Parse(GetString(ordinal));
        public override short GetInt16(int ordinal) => Convert.ToInt16(this[ordinal]);
        public override int GetInt32(int ordinal) => Convert.ToInt32(this[ordinal]);
        public override long GetInt64(int ordinal) => Convert.ToInt64(this[ordinal]);
        public override string GetString(int ordinal) => Convert.ToString(this[ordinal]);
        public override string GetName(int ordinal) => _dataTable.Columns[ordinal].ColumnName;
        public override int GetOrdinal(string name) => _dataTable.Columns.IndexOf(name);
        public override object GetValue(int ordinal) => this[ordinal];
        public override int GetValues(object[] values)
        {
            var row = _dataTable.Rows[_currentRowIndex];
            int count = Math.Min(row.ItemArray.Length, values.Length);
            Array.Copy(row.ItemArray, values, count);
            return count;
        }
        public override bool IsDBNull(int ordinal) => this[ordinal] == DBNull.Value || this[ordinal] == null;
        public override void Close() => _currentRowIndex = -1;
        public override IEnumerator GetEnumerator() => _dataTable.Rows.GetEnumerator();
    }
}