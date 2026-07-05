// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data.Common;
using System.Text.RegularExpressions;

namespace Scada.Comm.Drivers.DrvDbDataTransfer
{
    /// <summary>
    /// The base class of the data source.
    /// <para>Базовый класс источника данных.</para>
    /// </summary>
    public abstract class DataSource
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected DataSource()
        {
            Connection = null;
            ConnectionString = string.Empty;
            SelectCommand = null;
            ExportCommandsNum = new SortedList<int, DbCommand>();
            ExportCommandsCode = new SortedList<string, DbCommand>();
        }


        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        protected DbConnection Connection { get; set; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        protected string ConnectionString { get; set; }

        /// <summary>
        /// Gets the select command.
        /// </summary>
        public DbCommand SelectCommand { get; set; }

        /// <summary>
        /// Gets the export commands.
        /// </summary>
        public SortedList<int, DbCommand> ExportCommandsNum { get; protected set; }

        public SortedList<string, DbCommand> ExportCommandsCode { get; protected set; }


        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected abstract DbConnection CreateConnection();

        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected abstract DbConnection CreateConnection(string connectionString);

        /// <summary>
        /// Creates a command.
        /// </summary>
        protected abstract DbCommand CreateCommand();

        /// <summary>
        /// Adds the command parameter containing the value.
        /// </summary>
        protected abstract void AddCmdParamWithValue(DbCommand cmd, string paramName, object value);

        /// <summary>
        /// Clears the connection pool.
        /// </summary>
        protected abstract void ClearPool();

        /// <summary>
        /// Extracts host name and port from the specified server name.
        /// </summary>
        protected static void ExtractHostAndPort(string server, int defaultPort, out string host, out int port)
        {
            int ind = server.IndexOf(':');

            if (ind >= 0)
            {
                host = server.Substring(0, ind);
                if (!int.TryParse(server.Substring(ind + 1), out port))
                {
                    port = defaultPort;
                }
            }
            else
            {
                host = server;
                port = defaultPort;
            }
        }


        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public abstract string BuildConnectionString(DbConnSettings connSettings);

        /// <summary>
        /// Creates a provider-specific command.
        /// </summary>
        public DbCommand CreateDbCommand()
        {
            DbCommand command = CreateCommand();
            command.Connection = Connection;
            return command;
        }

        /// <summary>
        /// Begins a database transaction on the active connection.
        /// </summary>
        public DbTransaction BeginTransaction()
        {
            if (Connection == null)
            {
                throw new InvalidOperationException("Connection is not initialized.");
            }

            return Connection.BeginTransaction();
        }

        /// <summary>
        /// Adds or updates a command parameter.
        /// </summary>
        public void SetCmdParamWithValue(DbCommand cmd, string paramName, object value)
        {
            SetCmdParam(cmd, paramName, value);
        }

        /// <summary>
        /// Converts query parameter markers from the UI style to the provider style.
        /// </summary>
        public virtual string NormalizeCommandText(string commandText)
        {
            return commandText;
        }

        /// <summary>
        /// Converts a parameter name from the UI style to the provider style.
        /// </summary>
        public virtual string NormalizeParameterName(string parameterName)
        {
            return parameterName;
        }

        /// <summary>
        /// Replaces SQL parameter prefixes without touching escaped @@ tokens.
        /// </summary>
        protected static string ReplaceParameterPrefix(string commandText, char prefix)
        {
            return Regex.Replace(
                commandText ?? "",
                @"(?<!@)([@:])([A-Za-z_][A-Za-z0-9_]*)",
                match => prefix + match.Groups[2].Value);
        }

        /// <summary>
        /// Initializes the data source from connection settings.
        /// </summary>
        public void Init(DbConnSettings connSettings)
        {
            if (connSettings == null)
            {
                throw new ArgumentNullException("connSettings");
            }

            ConnectionString = string.IsNullOrEmpty(connSettings.ConnectionString) ?
                BuildConnectionString(connSettings) :
                connSettings.ConnectionString;

            Connection = string.IsNullOrEmpty(ConnectionString) ?
                CreateConnection() :
                CreateConnection(ConnectionString);

            Connection.ConnectionString = ConnectionString;

            SelectCommand = CreateCommand();
            SelectCommand.Connection = Connection;
            SelectCommand.CommandText = "";
        }

        /// <summary>
        /// Connects to the database.
        /// </summary>
        public void Connect()
        {
            if (Connection == null)
            {
                throw new InvalidOperationException("Connection is not initialized.");
            }
               
            try
            {
                Connection.Open();
            }
            catch
            {
                Connection.Close();
                ClearPool();
                throw;
            }
        }

        /// <summary>
        /// Disconnects from the database.
        /// </summary>
        public void Disconnect()
        {
            Connection?.Close();
        }

        /// <summary>
        /// Initializes the data source.
        /// </summary>
        public void Init(DrvDbDataTransferProject project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            ConnectionString = string.IsNullOrEmpty(project.DbConnSettings.ConnectionString) ?
                BuildConnectionString(project.DbConnSettings) :
                project.DbConnSettings.ConnectionString;

            if (String.IsNullOrEmpty(ConnectionString))
            {
                Connection = CreateConnection();
            }
            else
            {
                Connection = CreateConnection(ConnectionString);
            }

            Connection.ConnectionString = ConnectionString;
            
            SelectCommand = CreateCommand();
            SelectCommand.Connection = Connection;
            SelectCommand.CommandText = "";

            foreach (ImportCmd importCmd in project.ImportCmds)
            {
                DbCommand importCommand = CreateCommand();
                importCommand.CommandText = importCmd.Query;
                importCommand.Connection = Connection;

                ExportCommandsNum[importCmd.CmdNum] = importCommand;
                ExportCommandsCode[importCmd.CmdCode] = importCommand;
            }
      
            foreach (ExportCmd exportCmd in project.ExportCmds)
            {
                DbCommand exportCommand = CreateCommand();
                exportCommand.CommandText = exportCmd.Query;
                exportCommand.Connection = Connection;

                ExportCommandsNum[exportCmd.CmdNum] = exportCommand;
                ExportCommandsCode[exportCmd.CmdCode] = exportCommand;
            }
        }

        /// <summary>
        /// Sets the command parameter.
        /// </summary>
        public void SetCmdParam(DbCommand cmd, string paramName, object value)
        {
            ArgumentNullException.ThrowIfNull(cmd, nameof(cmd));

            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (string.IsNullOrWhiteSpace(paramName))
            {
                throw new ArgumentException("Parameter name cannot be null or whitespace", nameof(paramName));
            }

            object paramValue = value ?? DBNull.Value;

            if (cmd.Parameters.Contains(paramName))
            {
                cmd.Parameters[paramName].Value = paramValue;
            }
            else
            {
                AddCmdParamWithValue(cmd, paramName, paramValue);
            }               
        }

        /// <summary>
        /// Return the data source.
        /// </summary>
        public static DataSource GetDataSourceType(DrvDbDataTransferProject project)
        {
            return project == null ? null : GetDataSourceType(project.DbConnSettings.DataSourceType);
        }

        /// <summary>
        /// Return the data source.
        /// </summary>
        public static DataSource GetDataSourceType(DbConnSettings connSettings)
        {
            return connSettings == null ? null : GetDataSourceType(connSettings.DataSourceType);
        }

        /// <summary>
        /// Return the data source.
        /// </summary>
        public static DataSource GetDataSourceType(DataSourceType dataSourceType)
        {
            DataSource dataSource;

            switch (dataSourceType)
            {
                case DataSourceType.MSSQL:
                    return dataSource = new SqlDataSource();
                case DataSourceType.Oracle:
                    return dataSource = new OraDataSource();
                case DataSourceType.PostgreSQL:
                    return dataSource = new PgSqlDataSource();
                case DataSourceType.MySQL:
                    return dataSource = new MySqlDataSource();
                //case DataSourceType.OLEDB:
                    //return dataSource = new OleDbDataSource();
                //case DataSourceType.ODBC:
                    //return dataSource = new OdbcDataSource();
                case DataSourceType.Firebird:
                    return dataSource = new FirebirdDataSource();
                case DataSourceType.InfluxDBv2:
                    return new InfluxDBHttpDataSource(DataSourceType.InfluxDBv2);
                case DataSourceType.InfluxDBv3:
                    return new InfluxDBHttpDataSource(DataSourceType.InfluxDBv3);
                default:
                    return dataSource = null;
            }
        }
    }
}
