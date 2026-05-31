// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.Logic
{
    /// <summary>
    /// Builds requests for creating and using the archive.
    /// <para>Формирует запросы для создания и использования архива.</para>
    /// </summary>
    internal class QueryBuilder
    {
        /// <summary>
        /// Defines the correspondence between the event object properties and the database column names.
        /// </summary>
        public static readonly Dictionary<string, string> EventColumnMap;


        /// <summary>
        /// Initializes the class.
        /// </summary>
        static QueryBuilder()
        {
            EventColumnMap = new Dictionary<string, string>()
            {
                { "EventID", "event_id" },
                { "Timestamp", "time_stamp" },
                { "Hidden", "hidden" },
                { "CnlNum", "cnl_num" },
                { "ObjNum", "obj_num" },
                { "DeviceNum", "device_num" },
                { "PrevCnlVal", "prev_cnl_val" },
                { "PrevCnlStat", "prev_cnl_stat" },
                { "CnlVal", "cnl_val" },
                { "CnlStat", "cnl_stat" },
                { "Severity", "severity" },
                { "AckRequired", "ack_required" },
                { "Ack", "ack" },
                { "AckTimestamp", "ack_timestamp" },
                { "AckUserID", "ack_user_id" },
                { "TextFormat", "text_format" },
                { "Text", "event_text" },
                { "Data", "event_data" }
            };
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public QueryBuilder(string archiveCode)
        {
            string tablePrefix = string.IsNullOrEmpty(archiveCode) ? "" : archiveCode.ToLowerInvariant() + "_";
            CurrentTableShort = tablePrefix + "current";
            HistoricalTableShort = tablePrefix + "historical";
            EventTableShort = tablePrefix + "event";

            CurrentTableObjectName = GetObjectName(CurrentTableShort);
            HistoricalTableObjectName = GetObjectName(HistoricalTableShort);
            EventTableObjectName = GetObjectName(EventTableShort);

            string schema = QuoteIdentifier(DbUtils.Schema) + ".";
            CurrentTable = schema + QuoteIdentifier(CurrentTableShort);
            HistoricalTable = schema + QuoteIdentifier(HistoricalTableShort);
            EventTable = schema + QuoteIdentifier(EventTableShort);
        }


        /// <summary>
        /// Gets the short name of the current data table.
        /// </summary>
        private string CurrentTableShort { get; }

        /// <summary>
        /// Gets the short name of the historical data table.
        /// </summary>
        private string HistoricalTableShort { get; }

        /// <summary>
        /// Gets the short name of the event table.
        /// </summary>
        private string EventTableShort { get; }

        /// <summary>
        /// Gets the current data table object name.
        /// </summary>
        private string CurrentTableObjectName { get; }

        /// <summary>
        /// Gets the historical data table object name.
        /// </summary>
        private string HistoricalTableObjectName { get; }

        /// <summary>
        /// Gets the event table object name.
        /// </summary>
        private string EventTableObjectName { get; }

        /// <summary>
        /// Gets the name of the current data table.
        /// </summary>
        public string CurrentTable { get; }

        /// <summary>
        /// Gets the name of the historical data table.
        /// </summary>
        public string HistoricalTable { get; }

        /// <summary>
        /// Gets the name of the event table.
        /// </summary>
        public string EventTable { get; }


        /// <summary>
        /// Gets an object name used by SQL Server metadata functions.
        /// </summary>
        private static string GetObjectName(string tableName)
        {
            return DbUtils.Schema + "." + tableName;
        }

        /// <summary>
        /// Escapes a string literal.
        /// </summary>
        private static string EscapeString(string s)
        {
            return s.Replace("'", "''");
        }

        /// <summary>
        /// Quotes a SQL Server identifier.
        /// </summary>
        private static string QuoteIdentifier(string identifier)
        {
            return "[" + identifier.Replace("]", "]]") + "]";
        }

        /// <summary>
        /// Gets a query that creates an index if it does not exist.
        /// </summary>
        private static string CreateIndexQuery(string tableObjectName, string tableName, string indexName, string columns)
        {
            return
                $"IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'{EscapeString(indexName)}' " +
                $"AND object_id = OBJECT_ID(N'{EscapeString(tableObjectName)}')) " +
                $"CREATE INDEX {QuoteIdentifier(indexName)} ON {tableName} ({columns});";
        }


        /// <summary>
        /// Gets an SQL query to create a schema for the archives.
        /// </summary>
        public string CreateSchemaQuery
        {
            get
            {
                return
                    $"IF SCHEMA_ID(N'{EscapeString(DbUtils.Schema)}') IS NULL " +
                    $"EXEC(N'CREATE SCHEMA {QuoteIdentifier(DbUtils.Schema)}')";
            }
        }

        /// <summary>
        /// Gets an SQL query to create the current data table.
        /// </summary>
        public string CreateCurrentTableQuery
        {
            get
            {
                return
                    $"IF OBJECT_ID(N'{EscapeString(CurrentTableObjectName)}', N'U') IS NULL BEGIN " +
                    $"CREATE TABLE {CurrentTable} (" +
                    "cnl_num int NOT NULL, " +
                    "time_stamp datetime2 NOT NULL, " +
                    "val float NOT NULL, " +
                    "stat int NOT NULL, " +
                    $"CONSTRAINT {QuoteIdentifier("pk_" + CurrentTableShort)} PRIMARY KEY (cnl_num)); " +
                    "END";
            }
        }

        /// <summary>
        /// Gets an SQL query to create the historical data table.
        /// </summary>
        public string CreateHistoricalTableQuery
        {
            get
            {
                return
                    $"IF OBJECT_ID(N'{EscapeString(HistoricalTableObjectName)}', N'U') IS NULL BEGIN " +
                    $"CREATE TABLE {HistoricalTable} (" +
                    "cnl_num int NOT NULL, " +
                    "time_stamp datetime2 NOT NULL, " +
                    "val float NOT NULL, " +
                    "stat int NOT NULL, " +
                    $"CONSTRAINT {QuoteIdentifier("pk_" + HistoricalTableShort)} PRIMARY KEY (cnl_num, time_stamp)); " +
                    "END; " +
                    CreateIndexQuery(HistoricalTableObjectName, HistoricalTable,
                        "idx_" + HistoricalTableShort + "_time_stamp", "time_stamp");
            }
        }

        /// <summary>
        /// Gets an SQL query to create the event table.
        /// </summary>
        public string CreateEventTableQuery
        {
            get
            {
                return
                    $"IF OBJECT_ID(N'{EscapeString(EventTableObjectName)}', N'U') IS NULL BEGIN " +
                    $"CREATE TABLE {EventTable} (" +
                    "event_id bigint NOT NULL, " +
                    "time_stamp datetime2 NOT NULL, " +
                    "hidden bit NOT NULL, " +
                    "cnl_num int NOT NULL, " +
                    "obj_num int NOT NULL, " +
                    "device_num int NOT NULL, " +
                    "prev_cnl_val float NOT NULL, " +
                    "prev_cnl_stat int NOT NULL, " +
                    "cnl_val float NOT NULL, " +
                    "cnl_stat int NOT NULL, " +
                    "severity int NOT NULL, " +
                    "ack_required bit NOT NULL, " +
                    "ack bit NOT NULL, " +
                    "ack_timestamp datetime2 NOT NULL, " +
                    "ack_user_id int NOT NULL, " +
                    "text_format int NOT NULL, " +
                    "event_text nvarchar(max), " +
                    "event_data varbinary(max), " +
                    $"CONSTRAINT {QuoteIdentifier("pk_" + EventTableShort)} PRIMARY KEY (event_id, time_stamp)); " +
                    "END; " +
                    CreateIndexQuery(EventTableObjectName, EventTable,
                        "idx_" + EventTableShort + "_time_stamp", "time_stamp") + " " +
                    CreateIndexQuery(EventTableObjectName, EventTable,
                        "idx_" + EventTableShort + "_cnl_num", "cnl_num") + " " +
                    CreateIndexQuery(EventTableObjectName, EventTable,
                        "idx_" + EventTableShort + "_obj_num", "obj_num") + " " +
                    CreateIndexQuery(EventTableObjectName, EventTable,
                        "idx_" + EventTableShort + "_device_num", "device_num");
            }
        }

        /// <summary>
        /// Gets an SQL query to insert or update current data.
        /// </summary>
        public string InsertCurrentDataQuery
        {
            get
            {
                return
                    $"MERGE {CurrentTable} WITH (HOLDLOCK) AS target " +
                    "USING (VALUES (@cnlNum, @timestamp, @val, @stat)) " +
                    "AS source (cnl_num, time_stamp, val, stat) " +
                    "ON target.cnl_num = source.cnl_num " +
                    "WHEN MATCHED THEN UPDATE SET " +
                    "time_stamp = source.time_stamp, val = source.val, stat = source.stat " +
                    "WHEN NOT MATCHED THEN INSERT (cnl_num, time_stamp, val, stat) " +
                    "VALUES (source.cnl_num, source.time_stamp, source.val, source.stat);";
            }
        }

        /// <summary>
        /// Gets an SQL query to insert or update historical data.
        /// </summary>
        public string InsertHistoricalDataQuery
        {
            get
            {
                return
                    $"MERGE {HistoricalTable} WITH (HOLDLOCK) AS target " +
                    "USING (VALUES (@cnlNum, @timestamp, @val, @stat)) " +
                    "AS source (cnl_num, time_stamp, val, stat) " +
                    "ON target.cnl_num = source.cnl_num AND target.time_stamp = source.time_stamp " +
                    "WHEN MATCHED THEN UPDATE SET val = source.val, stat = source.stat " +
                    "WHEN NOT MATCHED THEN INSERT (cnl_num, time_stamp, val, stat) " +
                    "VALUES (source.cnl_num, source.time_stamp, source.val, source.stat);";
            }
        }

        /// <summary>
        /// Gets an SQL query to insert or update an event.
        /// </summary>
        public string InsertEventQuery
        {
            get
            {
                return
                    $"MERGE {EventTable} WITH (HOLDLOCK) AS target " +
                    "USING (VALUES (@eventID, @timestamp, @hidden, @cnlNum, @objNum, @deviceNum, " +
                    "@prevCnlVal, @prevCnlStat, @cnlVal, @cnlStat, @severity, @ackRequired, @ack, " +
                    "@ackTimestamp, @ackUserID, @textFormat, @eventText, @eventData)) " +
                    "AS source (event_id, time_stamp, hidden, cnl_num, obj_num, device_num, " +
                    "prev_cnl_val, prev_cnl_stat, cnl_val, cnl_stat, severity, ack_required, ack, " +
                    "ack_timestamp, ack_user_id, text_format, event_text, event_data) " +
                    "ON target.event_id = source.event_id AND target.time_stamp = source.time_stamp " +
                    "WHEN MATCHED THEN UPDATE SET " +
                    "hidden = source.hidden, cnl_num = source.cnl_num, obj_num = source.obj_num, " +
                    "device_num = source.device_num, prev_cnl_val = source.prev_cnl_val, " +
                    "prev_cnl_stat = source.prev_cnl_stat, cnl_val = source.cnl_val, " +
                    "cnl_stat = source.cnl_stat, severity = source.severity, " +
                    "ack_required = source.ack_required, ack = source.ack, " +
                    "ack_timestamp = source.ack_timestamp, ack_user_id = source.ack_user_id, " +
                    "text_format = source.text_format, event_text = source.event_text, " +
                    "event_data = source.event_data " +
                    "WHEN NOT MATCHED THEN INSERT (event_id, time_stamp, hidden, cnl_num, obj_num, device_num, " +
                    "prev_cnl_val, prev_cnl_stat, cnl_val, cnl_stat, severity, ack_required, ack, " +
                    "ack_timestamp, ack_user_id, text_format, event_text, event_data) " +
                    "VALUES (source.event_id, source.time_stamp, source.hidden, source.cnl_num, " +
                    "source.obj_num, source.device_num, source.prev_cnl_val, source.prev_cnl_stat, " +
                    "source.cnl_val, source.cnl_stat, source.severity, source.ack_required, source.ack, " +
                    "source.ack_timestamp, source.ack_user_id, source.text_format, source.event_text, source.event_data);";
            }
        }

        /// <summary>
        /// Gets an SQL query to select events. The WHERE clause must be added to the query.
        /// </summary>
        public string SelectEventQuery
        {
            get
            {
                return
                    "SELECT event_id, time_stamp, hidden, cnl_num, obj_num, device_num, " +
                    "prev_cnl_val, prev_cnl_stat, cnl_val, cnl_stat, severity, " +
                    "ack_required, ack, ack_timestamp, ack_user_id, text_format, event_text, event_data " +
                    $"FROM {EventTable} ";
            }
        }
    }
}
