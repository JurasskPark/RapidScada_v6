// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

#pragma warning disable 618 // disable the warning about obsolete Oracle classes

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Implements a data source for Oracle.
    /// <para>Реализует источник данных для Oracle.</para>
    /// </summary>
    internal class OraDataSource : DataSource
    {
        /// <summary>
        /// Creates a database connection.
        /// </summary>
        protected override DbConnection CreateConnection()
        {
            return new OracleConnection();
        }

        /// <summary>
        /// Creates a command.
        /// </summary>
        protected override DbCommand CreateCommand()
        {
            return new OracleCommand();
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
               
            if (!(cmd is OracleCommand))
            {
                throw new ArgumentException("OracleCommand is required.", "cmd");
            }
                
            OracleCommand oraCmd = (OracleCommand)cmd;      
            oraCmd.Parameters.Add(new OracleParameter(paramName, value) );
        }

        /// <summary>
        /// Clears the connection pool.
        /// </summary>
        protected override void ClearPool()
        {
            if (Connection != null)
            {
                OracleConnection.ClearPool((OracleConnection)Connection);
            }           
        }


        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public override string BuildConnectionString(DbConnSettings connSettings)
        {
            return BuildOraConnectionString(connSettings);
        }

        /// <summary>
        /// Builds a connection string based on the specified connection settings.
        /// </summary>
        public static string BuildOraConnectionString(DbConnSettings connSettings)
        {
            if (connSettings == null)
            {
                throw new ArgumentNullException("connSettings");
            }
                
            return string.Format("Server={0}{1};User ID={2};Password={3};{4}", connSettings.Server, 
                string.IsNullOrEmpty(connSettings.Database) ? "" : "/" + connSettings.Database, 
                connSettings.User, connSettings.Password, connSettings.OptionalOptions);
        }
    }
}
