// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Implements a data source for MySQL.
    /// <para>Реализует источник данных для MySQL.</para>
    /// </summary>
    internal class MySqlDataSource : DataSource
    {
        /// <summary>
        /// The default port of the database server.
        /// </summary>
        private const int DefaultPort = 3306;


        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected override DbConnection CreateConnection()
        {
            return new MySqlConnection();
        }

        /// <summary>
        /// Creates a command.
        /// </summary>
        protected override DbCommand CreateCommand()
        {
            return new MySqlCommand();
        }

        /// <summary>
        /// Adds the command parameter containing the value.
        /// </summary>
        protected override void AddCmdParamWithValue(DbCommand cmd, string paramName, object value)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");
            if (!(cmd is MySqlCommand))
                throw new ArgumentException("MySqlCommand is required.", "cmd");

            MySqlCommand mySqlCmd = (MySqlCommand)cmd;
            mySqlCmd.Parameters.AddWithValue(paramName, value);
        }

        /// <summary>
        /// Clears the connection pool.
        /// </summary>
        protected override void ClearPool()
        {
            if (Connection != null)
                MySqlConnection.ClearPool((MySqlConnection)Connection);
        }


        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public override string BuildConnectionString(DbConnSettings connSettings)
        {
            return BuildMySqlConnectionString(connSettings);
        }

        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public static string BuildMySqlConnectionString(DbConnSettings connSettings)
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

            return new MySqlConnectionStringBuilder()
            {
                Server = host,
                Port = Convert.ToUInt32(connSettings.Port),
                Database = connSettings.Database,
                UserID = connSettings.User,
                Password = connSettings.Password
            }
            .ToString();
        }
    }
}
