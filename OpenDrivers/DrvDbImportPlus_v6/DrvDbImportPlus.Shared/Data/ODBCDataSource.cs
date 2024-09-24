// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

//using System.Data.Common;
//using System.Data.Odbc;

//namespace Scada.Comm.Drivers.DrvDbImportPlus
//{
//    /// <summary>
//    /// Implements a data source for ODBC.
//    /// <para>Реализует источник данных для ODBC.</para>
//    /// </summary>
//    internal class  OdbcDataSource : DataSource
//    {
//        /// <summary>
//        /// Creates a database connection.
//        /// </summary>
//        protected override DbConnection CreateConnection()
//        {
//            return new OdbcConnection();
//        }

//        /// <summary>
//        /// Creates a command.
//        /// </summary>
//        protected override DbCommand CreateCommand()
//        {
//            return new OdbcCommand();
//        }

//        /// <summary>
//        /// Adds the command parameter containing the value.
//        /// </summary>
//        protected override void AddCmdParamWithValue(DbCommand cmd, string paramName, object value)
//        {
//            if (cmd == null)
//            {
//                throw new ArgumentNullException("cmd");
//            }
                
//            if (!(cmd is OdbcCommand))
//            {
//                throw new ArgumentException("ODBCCommand is required.", "cmd");
//            }
               
//            OdbcCommand oleDbCmd = (OdbcCommand)cmd;
//            oleDbCmd.Parameters.AddWithValue(paramName, value);
//        }

//        /// <summary>
//        /// Clears the connection pool.
//        /// </summary>
//        protected override void ClearPool()
//        {
//            // do nothing
//        }


//        /// <summary>
//        /// Builds a connection string based on the specified connection settings.
//        /// </summary>
//        public override string BuildConnectionString(DbConnSettings connSettings)
//        {
//            return BuildOdbcConnectionString(connSettings);
//        }

//        /// <summary>
//        /// Builds a connection string based on the specified connection settings.
//        /// </summary>
//        public static string BuildOdbcConnectionString(DbConnSettings connSettings)
//        {
//            return "";
//        }
//    }
//}
