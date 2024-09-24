// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Npgsql;
using System.Data.Common;


namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Implements a data source for PostgreSQL.
    /// <para>Реализует источник данных для PostgreSQL.</para>
    /// </summary>
    internal class PgSqlDataSource : DataSource
    {
        /// <summary>
        /// The default port of the database server.
        /// </summary>
        private const int DefaultPort = 5432;


        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected override DbConnection CreateConnection()
        {
            return new NpgsqlConnection();
        }

        /// <summary>
        /// Creates a command.
        /// </summary>
        protected override DbCommand CreateCommand()
        {
            return new NpgsqlCommand();
        }

        /// <summary>
        /// Adds the command parameter containing the value.
        /// </summary>
        protected override void AddCmdParamWithValue(DbCommand cmd, string paramName, object value)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }
                
            if (!(cmd is NpgsqlCommand))
            {
                throw new ArgumentException("NpgsqlCommand is required.", "cmd");
            }
               
            NpgsqlCommand pgSqlCmd = (NpgsqlCommand)cmd;
            pgSqlCmd.Parameters.AddWithValue(paramName, value);
        }

        /// <summary>
        /// Clears the connection pool.
        /// </summary>
        protected override void ClearPool()
        {
            NpgsqlConnection.ClearAllPools();
        }


        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public override string BuildConnectionString(DbConnSettings connSettings)
        {
            return BuildPgSqlConnectionString(connSettings);
        }

        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public static string BuildPgSqlConnectionString(DbConnSettings connSettings)
        {
            if (connSettings == null)
            {
                throw new ArgumentNullException("connSettings");
            }
                

            if (connSettings.Port == string.Empty || connSettings.Port == null)
            {
                connSettings.Port = DefaultPort.ToString();
            }
                
            ExtractHostAndPort(connSettings.Server, Convert.ToInt32(connSettings.Port), out string host, out int port);
            return string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};{5};",
                host, port, connSettings.Database, connSettings.User, connSettings.Password, connSettings.OptionalOptions);
        }
    }
}
