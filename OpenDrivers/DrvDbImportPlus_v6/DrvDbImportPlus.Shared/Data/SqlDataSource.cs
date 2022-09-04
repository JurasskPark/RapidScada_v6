// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Implements a data source for Microsoft SQL Server.
    /// <para>Реализует источник данных для Microsoft SQL Server.</para>
    /// </summary>
    internal class SqlDataSource : DataSource
    {
        /// <summary>
        /// The default port of the database server.
        /// </summary>
        private const int DefaultPort = 1433;

        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected override DbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        /// <summary>
        /// Creates a command.
        /// </summary>
        protected override DbCommand CreateCommand()
        {
            return new SqlCommand();
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
                
            if (!(cmd is SqlCommand))
            {
                throw new ArgumentException("SqlCommand is required.", "cmd");
            }

            SqlCommand sqlCmd = (SqlCommand)cmd;
            sqlCmd.Parameters.AddWithValue(paramName, value);
            //IDbDataParameter dp;
            //dp = cmd.CreateParameter();
            //dp.ParameterName = paramName;
            //dp.Value = value;
            //cmd.Parameters.Add(dp);

            //SqlCommand sqlCmd = (SqlCommand)cmd;
        }

        /// <summary>
        /// Clears the connection pool.
        /// </summary>
        protected override void ClearPool()
        {
            if (Connection != null)
                SqlConnection.ClearPool((SqlConnection)Connection);
        }


        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public override string BuildConnectionString(DbConnSettings connSettings)
        {
            return BuildSqlConnectionString(connSettings);
        }

        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public static string BuildSqlConnectionString(DbConnSettings connSettings)
        {
            if (connSettings == null)
                throw new ArgumentNullException("connSettings");

            if (connSettings.Port == string.Empty || connSettings.Port == null)
                connSettings.Port = DefaultPort.ToString();
          
            return string.Format("Server={0},{1};Database={2};User ID={3};Password={4};{5}", 
                connSettings.Server, connSettings.Port, connSettings.Database, connSettings.User, connSettings.Password, connSettings.OptionalOptions);
        }
    }
}
