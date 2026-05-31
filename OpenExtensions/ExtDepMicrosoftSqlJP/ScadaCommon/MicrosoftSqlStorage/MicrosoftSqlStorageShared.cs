// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.SqlClient;
using Scada.Data.Tables;
using Scada.Dbms;
using Scada.Lang;
using System.ComponentModel;

namespace Scada.Storages.MicrosoftSqlStorage
{
    /// <summary>
    /// The class provides helper methods for Microsoft SQL Server storage shared with other assemblies.
    /// </summary>
    internal static class MicrosoftSqlStorageShared
    {
        /// <summary>
        /// The database schema.
        /// </summary>
        public const string Schema = "project";


        /// <summary>
        /// Creates a database connection.
        /// </summary>
        public static SqlConnection CreateDbConnection(DbConnectionOptions options)
        {
            string connectionString = options.BuildConnectionString(KnownDBMS.MSSQL);
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Escapes an SQL name and encloses it in square brackets.
        /// </summary>
        public static string QuoteName(string name)
        {
            return "[" + name.Replace("]", "]]") + "]";
        }

        /// <summary>
        /// Escapes a string value for use as an SQL literal.
        /// </summary>
        public static string EscapeString(string value)
        {
            return value.Replace("'", "''");
        }

        /// <summary>
        /// Gets the full name of a table.
        /// </summary>
        public static string GetTableName(string tableName)
        {
            return $"{QuoteName(Schema)}.{QuoteName(tableName)}";
        }

        /// <summary>
        /// Gets an object name literal for OBJECT_ID.
        /// </summary>
        public static string GetObjectNameLiteral(string tableName)
        {
            return "N'" + EscapeString(Schema + "." + tableName) + "'";
        }

        /// <summary>
        /// Gets the name of the configuration database table.
        /// </summary>
        public static string GetBaseTableName(IBaseTable baseTable)
        {
            return GetTableName(baseTable.Name.ToLowerInvariant());
        }

        /// <summary>
        /// Gets an object name literal of the configuration database table.
        /// </summary>
        public static string GetBaseTableObjectNameLiteral(IBaseTable baseTable)
        {
            return GetObjectNameLiteral(baseTable.Name.ToLowerInvariant());
        }

        /// <summary>
        /// Gets the column name of the configuration database table.
        /// </summary>
        public static string GetBaseColumnName(string propName, bool addQuotes = true)
        {
            string columnName = propName.ToLowerInvariant();
            return addQuotes ? QuoteName(columnName) : columnName;
        }

        /// <summary>
        /// Gets the column name of the configuration database table.
        /// </summary>
        public static string GetBaseColumnName(PropertyDescriptor prop, bool addQuotes = true)
        {
            return GetBaseColumnName(prop.Name, addQuotes);
        }

        /// <summary>
        /// Reads the configuration database table.
        /// </summary>
        public static void ReadBaseTable(IBaseTable baseTable, SqlConnection conn)
        {
            string sql = "SELECT * FROM " + GetBaseTableName(baseTable);
            SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                try
                {
                    reader.GetOrdinal(GetBaseColumnName(baseTable.PrimaryKey, false));
                }
                catch
                {
                    throw new ScadaException(Locale.IsRussian ?
                        "Первичный ключ \"{0}\" не найден" :
                        "Primary key \"{0}\" not found", baseTable.PrimaryKey);
                }

                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(baseTable.ItemType);
                int propCnt = props.Count;
                int[] colIdxs = new int[propCnt];

                for (int i = 0; i < propCnt; i++)
                {
                    try { colIdxs[i] = reader.GetOrdinal(GetBaseColumnName(props[i], false)); }
                    catch { colIdxs[i] = -1; }
                }

                baseTable.Modified = true;

                while (reader.Read())
                {
                    object item = baseTable.NewItem();

                    for (int i = 0; i < propCnt; i++)
                    {
                        int colIdx = colIdxs[i];

                        if (colIdx >= 0 && !reader.IsDBNull(colIdx))
                            props[i].SetValue(item, reader[colIdx]);
                    }

                    baseTable.AddObject(item);
                }
            }
        }
    }
}