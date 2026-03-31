// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data.Common;
using System.Diagnostics;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Implements a unified data source for InfluxDB 2.x/3.x using HTTP API.
    /// <para>Реализует унифицированный источник данных для InfluxDB 2.x/3.x с использованием HTTP API.</para>
    /// </summary>
    internal class InfluxDBHttpDataSource : DataSource
    {
        /// <summary>
        /// The default port of the database server.
        /// </summary>
        private const int DefaultPort = 8086;

        private readonly DataSourceType _dataSourceType;
        private InfluxDBHttpClient _influxClient;
        private string _connectionString = "";

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public InfluxDBHttpDataSource(DataSourceType dataSourceType)
        {
            _dataSourceType = dataSourceType;
        }

        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected override DbConnection CreateConnection()
        {
            // Если клиент еще не создан, но строка подключения есть - создаем его
            if (_influxClient == null && !string.IsNullOrEmpty(_connectionString))
            {
                InitializeClient();
            }

            return new InfluxDBHttpConnection(_influxClient);
        }

        /// <summary>
        /// Creates a database connection with connection string.
        /// </summary>
        protected override DbConnection CreateConnection(string connectionString)
        {
            // Сохраняем строку подключения
            if (!string.IsNullOrEmpty(connectionString))
            {
                _connectionString = connectionString;
            }

            // Если клиент еще не создан, но строка подключения есть - создаем его
            if (_influxClient == null && !string.IsNullOrEmpty(_connectionString))
            {
                InitializeClient();
            }

            return new InfluxDBHttpConnection(_influxClient);
        }

        /// <summary>
        /// Creates a command.
        /// </summary>
        protected override DbCommand CreateCommand()
        {
            // Если клиент еще не создан, но строка подключения есть - создаем его
            if (_influxClient == null && !string.IsNullOrEmpty(_connectionString))
            {
                InitializeClient();
            }

            return new InfluxDBHttpCommand(_influxClient);
        }

        /// <summary>
        /// Adds the command parameter containing the value.
        /// </summary>
        protected override void AddCmdParamWithValue(DbCommand cmd, string paramName, object value)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            if (!(cmd is InfluxDBHttpCommand influxCmd))
                throw new ArgumentException("InfluxDBHttpCommand is required.", nameof(cmd));

            influxCmd.Parameters[paramName] = value;
        }

        /// <summary>
        /// Clears the connection pool.
        /// </summary>
        protected override void ClearPool()
        {
            _influxClient?.Dispose();
            _influxClient = null;
        }

        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public override string BuildConnectionString(DbConnSettings connSettings)
        {
            return BuildInfluxConnectionString(connSettings);
        }

        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public static string BuildInfluxConnectionString(DbConnSettings connSettings)
        {
            if (connSettings == null)
                throw new ArgumentNullException(nameof(connSettings));

            // Если строка подключения уже готова, возвращаем ее
            if (!string.IsNullOrEmpty(connSettings.ConnectionString))
                return connSettings.ConnectionString;

            // Иначе строим на основе настроек
            if (string.IsNullOrEmpty(connSettings.Port))
                connSettings.Port = DefaultPort.ToString();

            string url = $"http://{connSettings.Server}:{connSettings.Port}";
            string token = connSettings.Password ?? string.Empty; // Password = Token
            string database = connSettings.Database ?? string.Empty;

            // Собираем строку подключения
            var builder = new System.Data.Common.DbConnectionStringBuilder();
            builder["Url"] = url;
            builder["Token"] = token;
            builder["DataSourceType"] = connSettings.DataSourceType.ToString();

            if (connSettings.DataSourceType == DataSourceType.InfluxDBv2)
            {
                builder["Bucket"] = database;
            }
            else // InfluxDBv3
            {
                builder["Database"] = database;
            }

            // Добавляем все дополнительные параметры из OptionalOptions
            if (!string.IsNullOrEmpty(connSettings.OptionalOptions))
            {
                builder["OptionalOptions"] = connSettings.OptionalOptions;
            }

            return builder.ConnectionString;
        }

        /// <summary>
        /// Initializes the InfluxDB HTTP client.
        /// </summary>
        private void InitializeClient()
        {
            if (_influxClient != null)
                return;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not set.");
            }

            var url = GetValueFromConnectionString("Url");
            var token = GetValueFromConnectionString("Token");

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException("Url is required in connection string");

            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("Token is required in connection string (use Password field)");

            string targetDatabase;

            if (_dataSourceType == DataSourceType.InfluxDBv2)
            {
                targetDatabase = GetValueFromConnectionString("Bucket");
                if (string.IsNullOrEmpty(targetDatabase))
                    throw new InvalidOperationException("Bucket is required for InfluxDB 2.x (use Database field)");
            }
            else // InfluxDBv3
            {
                targetDatabase = GetValueFromConnectionString("Database");
                if (string.IsNullOrEmpty(targetDatabase))
                    throw new InvalidOperationException("Database is required for InfluxDB 3.x");
            }

            _influxClient = new InfluxDBHttpClient(url, token, targetDatabase);

            // Обновляем все созданные команды
            UpdateCommandsWithClient();
        }

        /// <summary>
        /// Updates commands with the initialized client.
        /// </summary>
        private void UpdateCommandsWithClient()
        {
            if (_influxClient == null)
                return;

            // Обновляем SelectCommand
            if (SelectCommand != null && SelectCommand is InfluxDBHttpCommand influxSelectCmd)
            {
                influxSelectCmd.UpdateClient(_influxClient);
            }

            // Обновляем экспортные команды
            var numKeys = ExportCommandsNum.Keys.ToList();
            foreach (var key in numKeys)
            {
                var cmd = ExportCommandsNum[key];
                if (cmd != null && cmd is InfluxDBHttpCommand influxExportCmd)
                {
                    influxExportCmd.UpdateClient(_influxClient);
                }
            }

            var codeKeys = ExportCommandsCode.Keys.ToList();
            foreach (var key in codeKeys)
            {
                var cmd = ExportCommandsCode[key];
                if (cmd != null && cmd is InfluxDBHttpCommand influxExportCmd)
                {
                    influxExportCmd.UpdateClient(_influxClient);
                }
            }

            // Также обновляем Connection
            if (Connection != null && Connection is InfluxDBHttpConnection influxConnection)
            {
                influxConnection.UpdateClient(_influxClient);
            }
        }

        /// <summary>
        /// Extracts a value from the connection string.
        /// </summary>
        private string GetValueFromConnectionString(string key)
        {
            if (string.IsNullOrEmpty(_connectionString))
                return string.Empty;

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

            return string.Empty;
        }

        /// <summary>
        /// Gets the database/bucket name from the connection string.
        /// </summary>
        public string GetTargetDatabase()
        {
            if (_dataSourceType == DataSourceType.InfluxDBv2)
                return GetValueFromConnectionString("Bucket");
            else
                return GetValueFromConnectionString("Database");
        }

        /// <summary>
        /// Tests the connection with a simple query.
        /// </summary>
        public void TestConnection()
        {
            if (_influxClient == null)
                InitializeClient();

            var url = GetValueFromConnectionString("Url");
            var token = GetValueFromConnectionString("Token");
            var database = GetTargetDatabase();

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(database))
            {
                throw new InvalidOperationException("Connection parameters are not properly projectured");
            }

            using var testClient = new InfluxDBHttpClient(url, token, database);

            // Тестируем сначала GET
            bool getSuccess = testClient.TestConnectionSync(true);

            if (!getSuccess)
            {
                // Пробуем POST
                bool postSuccess = testClient.TestConnectionSync(false);

                if (!postSuccess)
                {
                    throw new InvalidOperationException("Both GET and POST methods failed");
                }
            }
        }

        /// <summary>
        /// Simple connection test.
        /// </summary>
        public bool TestConnectionSimple()
        {
            try
            {
                if (_influxClient == null)
                    InitializeClient();

                var result = _influxClient.ExecuteQueryAsync("SHOW MEASUREMENTS").Result;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}