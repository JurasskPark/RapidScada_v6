// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data.Common;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// The base class of the data source.
    /// <para>Базовый класс источника данных.</para>
    /// </summary>
    internal abstract class DataSource
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected DataSource()
        {
            Connection = null;
            SelectCommand = null;
            ExportCommandsNum = new SortedList<int, DbCommand>();
            ExportCommandsCode = new SortedList<string, DbCommand>();
        }


        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        protected DbConnection Connection { get; set; }

        /// <summary>
        /// Gets the select command.
        /// </summary>
        public DbCommand SelectCommand { get; protected set; }

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
        public void Init(string connectionString, DrvDbImportPlusConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
                
            Connection = CreateConnection();
            Connection.ConnectionString = connectionString;

            SelectCommand = CreateCommand();
            SelectCommand.CommandText = config.SelectQuery;
            SelectCommand.Connection = Connection;

            foreach (ExportCmd exportCmd in config.ExportCmds)
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

            if (cmd.Parameters.Contains(paramName))
            {
                cmd.Parameters[paramName].Value = value;
            }
            else
            {
                AddCmdParamWithValue(cmd, paramName, value);
            }               
        }

        /// <summary>
        /// Return the data source.
        /// </summary>
        public static DataSource GetDataSourceType(DrvDbImportPlusConfig config)
        {
            DataSource dataSource;

            switch (config.DataSourceType)
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
                default:
                    return dataSource = null;
            }
        }
    }
}
