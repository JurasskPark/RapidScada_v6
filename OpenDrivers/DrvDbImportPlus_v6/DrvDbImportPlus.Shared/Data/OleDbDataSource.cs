// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

//using System.Data.Common;
//using System.Data.OleDb;

//namespace Scada.Comm.Drivers.DrvDbImportPlus
//{
//    /// <summary>
//    /// Implements a data source for OLE DB.
//    /// <para>Реализует источник данных для OLE DB.</para>
//    /// </summary>
//    internal class OleDbDataSource : DataSource
//    {
//        /// <summary>
//        /// Creates a database connection.
//        /// </summary>
//        protected override DbConnection CreateConnection()
//        {
//            return new OleDbConnection();
//        }

//        /// <summary>
//        /// Creates a command.
//        /// </summary>
//        protected override DbCommand CreateCommand()
//        {
//            return new OleDbCommand();
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
                
//            if (!(cmd is OleDbCommand))
//            {
//                throw new ArgumentException("OleDbCommand is required.", "cmd");
//            }
                
//            OleDbCommand oleDbCmd = (OleDbCommand)cmd;
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
//            return BuildOleDbConnectionString(connSettings);
//        }

//        /// <summary>
//        /// Builds a connection string based on the specified connection settings.
//        /// </summary>
//        public static string BuildOleDbConnectionString(DbConnSettings connSettings)
//        {
//            return "";
//        }
//    }
//}
