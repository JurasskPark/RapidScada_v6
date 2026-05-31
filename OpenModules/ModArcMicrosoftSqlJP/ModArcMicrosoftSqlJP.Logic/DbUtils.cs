// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.SqlClient;
using Scada.Dbms;
using Scada.Server.Modules.ModArcMicrosoftSqlJP.Config;
using System.Data;

namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.Logic
{
    /// <summary>
    /// The class provides helper methods for accessing a database.
    /// <para>Класс, предоставляющий вспомогательные методы для доступа к базе данных.</para>
    /// </summary>
    internal static class DbUtils
    {
        /// <summary>
        /// The date format used for naming partitions.
        /// </summary>
        private const string PartitionDateFormat = "yyyyMMdd";
        /// <summary>
        /// The database schema.
        /// </summary>
        public const string Schema = "mod_arc_microsoft_sql";


        /// <summary>
        /// Gets the start and end dates of the partition period.
        /// </summary>
        private static void GetPartitionDates(DateTime today, PartitionSize partitionSize,
            out DateTime startDate, out DateTime endDate)
        {
            switch (partitionSize)
            {
                case PartitionSize.OneDay:
                    startDate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0, DateTimeKind.Utc);
                    endDate = startDate.AddDays(1);
                    break;
                case PartitionSize.OneMonth:
                    startDate = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                    endDate = startDate.AddMonths(1);
                    break;
                default: // PartitionSize.OneYear
                    startDate = new DateTime(today.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    endDate = startDate.AddYears(1);
                    break;
            }
        }

        /// <summary>
        /// Creates a database connection.
        /// </summary>
        public static SqlConnection CreateDbConnection(DbConnectionOptions options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            return new SqlConnection(options.BuildConnectionString(KnownDBMS.MSSQL));
        }

        /// <summary>
        /// Checks a logical partition period.
        /// </summary>
        public static void CreatePartition(SqlConnection conn, string tableName,
            DateTime today, PartitionSize partitionSize, out string partitionName)
        {
            ArgumentNullException.ThrowIfNull(conn, nameof(conn));
            ArgumentNullException.ThrowIfNull(tableName, nameof(tableName));

            GetPartitionDates(today, partitionSize, out DateTime startDate, out DateTime endDate);
            partitionName = tableName +
                "_" + startDate.ToString(PartitionDateFormat) +
                "_" + endDate.ToString(PartitionDateFormat);
        }

        /// <summary>
        /// Deletes the outdated rows.
        /// </summary>
        public static int DeleteOutdatedData(SqlConnection conn, string tableName, DateTime minDT)
        {
            ArgumentNullException.ThrowIfNull(conn, nameof(conn));
            ArgumentNullException.ThrowIfNull(tableName, nameof(tableName));

            SqlCommand cmd = new($"DELETE FROM {tableName} WHERE time_stamp < @minTime", conn);
            cmd.Parameters.Add("minTime", SqlDbType.DateTime2).Value = minDT;
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the time (UTC) when the archive was last written to.
        /// </summary>
        public static DateTime GetLastWriteTime(SqlConnection conn, string tableName)
        {
            ArgumentNullException.ThrowIfNull(conn, nameof(conn));
            ArgumentNullException.ThrowIfNull(tableName, nameof(tableName));

            string sql = "SELECT MAX(time_stamp) FROM " + tableName;
            SqlCommand cmd = new(sql, conn);
            object timestampObj = cmd.ExecuteScalar();
            return timestampObj is DateTime dateTime
                ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
                : DateTime.MinValue;
        }

        /// <summary>
        /// Gets the value of the specified column as a universal time.
        /// </summary>
        public static DateTime GetDateTimeUtc(this SqlDataReader reader, int columnIndex)
        {
            return DateTime.SpecifyKind(reader.GetDateTime(columnIndex), DateTimeKind.Utc);
        }
    }
}
