// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.SqlClient;
using Scada.Admin.Deployment;
using Scada.Admin.Extensions.ExtDepMicrosoftSqlJP.Config;
using Scada.Admin.Lang;
using Scada.Admin.Project;
using Scada.Agent.Client;
using Scada.Data.Entities;
using Scada.Data.Tables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using static Scada.Storages.MicrosoftSqlStorage.MicrosoftSqlStorageShared;

namespace Scada.Admin.Extensions.ExtDepMicrosoftSqlJP
{
    /// <summary>
    /// Uploads configuration.
    /// </summary>
    internal class Uploader
    {
        /// <summary>
        /// The number of upload tasks.
        /// </summary>
        private const int TaskCount = 15;

        private readonly ScadaProject project;
        private readonly ProjectInstance instance;
        private readonly DeploymentProfile profile;
        private readonly ITransferControl transferControl;
        private readonly ExtensionConfig extensionConfig;
        private readonly UploadOptions uploadOptions;
        private readonly ProgressTracker progressTracker;
        private SqlConnection conn;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Uploader(ScadaProject project, ProjectInstance instance, DeploymentProfile profile,
            ITransferControl transferControl, ExtensionConfig extensionConfig)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
            this.instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this.profile = profile ?? throw new ArgumentNullException(nameof(profile));
            this.transferControl = transferControl ?? throw new ArgumentNullException(nameof(transferControl));
            this.extensionConfig = extensionConfig ?? throw new ArgumentNullException(nameof(extensionConfig));
            uploadOptions = profile.UploadOptions;
            progressTracker = new ProgressTracker(transferControl) { TaskCount = TaskCount };
            conn = null;
        }


        /// <summary>
        /// Creates a database schema.
        /// </summary>
        private void CreateSchema()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteMessage(ExtensionPhrases.CreateSchema);

            string sql = $"IF SCHEMA_ID(N'{Schema}') IS NULL EXEC(N'CREATE SCHEMA {QuoteName(Schema)}')";
            new SqlCommand(sql, conn).ExecuteNonQuery();
            progressTracker.TaskIndex++;
        }

        /// <summary>
        /// Creates and fills the application dictionary.
        /// </summary>
        private void CreateAppTable()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteMessage(ExtensionPhrases.CreateAppDict);
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                new SqlCommand(GetAppTableDDL(), conn, trans).ExecuteNonQuery();

                string sql =
                    $"MERGE {GetTableName("app")} WITH (HOLDLOCK) AS target " +
                    "USING (SELECT @appID AS app_id, @name AS name) AS source " +
                    "ON target.app_id = source.app_id " +
                    "WHEN NOT MATCHED THEN INSERT (app_id, name) VALUES (source.app_id, source.name);";
                SqlCommand insertCmd = new(sql, conn, trans);
                SqlParameter appIdParam = insertCmd.Parameters.Add("@appID", SqlDbType.Int);
                SqlParameter nameParam = insertCmd.Parameters.Add("@name", SqlDbType.NVarChar, 100);

                foreach (ServiceApp app in Enum.GetValues(typeof(ServiceApp)))
                {
                    if (app != ServiceApp.Unknown)
                    {
                        appIdParam.Value = (int)app;
                        nameParam.Value = ScadaUtils.GetAppName(app);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Removes all tables from the configuration database.
        /// </summary>
        private void ClearBase()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(ExtensionPhrases.ClearBase);
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                progressTracker.SubtaskCount = project.ConfigDatabase.AllTables.Length;

                foreach (IBaseTable baseTable in project.ConfigDatabase.AllTables)
                {
                    transferControl.ThrowIfCancellationRequested();
                    transferControl.WriteMessage(string.Format(ExtensionPhrases.DeleteTable, baseTable.Name));

                    string sql = GetDropTableDDL(baseTable);
                    new SqlCommand(sql, conn, trans).ExecuteNonQuery();
                    progressTracker.SubtaskIndex++;
                }

                trans.Commit();
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Deletes all rows from the configuration database tables.
        /// </summary>
        private void TruncateBase()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(ExtensionPhrases.ClearBase);
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                progressTracker.SubtaskCount = project.ConfigDatabase.AllTables.Length;

                SetBaseConstraints(false, trans);

                foreach (IBaseTable baseTable in project.ConfigDatabase.AllTables)
                {
                    transferControl.ThrowIfCancellationRequested();
                    transferControl.WriteMessage(string.Format(ExtensionPhrases.TruncateTable, baseTable.Name));

                    string sql = $"DELETE FROM {GetBaseTableName(baseTable)}";
                    new SqlCommand(sql, conn, trans).ExecuteNonQuery();
                    progressTracker.SubtaskIndex++;
                }

                SetBaseConstraints(true, trans);
                trans.Commit();
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Creates and fills the configuration database tables.
        /// </summary>
        private void CreateBase()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(ExtensionPhrases.CreateBase);
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                progressTracker.SubtaskCount = project.ConfigDatabase.AllTables.Length;

                foreach (IBaseTable baseTable in project.ConfigDatabase.AllTables)
                {
                    transferControl.ThrowIfCancellationRequested();
                    transferControl.WriteMessage(string.Format(ExtensionPhrases.CreateTable, baseTable.Name));

                    string sql = GetBaseTableDDL(baseTable);
                    new SqlCommand(sql, conn, trans).ExecuteNonQuery();
                    InsertRows(baseTable, trans);
                    progressTracker.SubtaskIndex++;
                }

                trans.Commit();
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Fills the configuration database tables.
        /// </summary>
        private void FillBase()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(ExtensionPhrases.CreateBase);
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                SetBaseConstraints(false, trans);
                progressTracker.SubtaskCount = project.ConfigDatabase.AllTables.Length;

                foreach (IBaseTable baseTable in project.ConfigDatabase.AllTables)
                {
                    transferControl.ThrowIfCancellationRequested();
                    transferControl.WriteMessage(string.Format(ExtensionPhrases.FillTable, baseTable.Name));

                    InsertRows(baseTable, trans);
                    progressTracker.SubtaskIndex++;
                }

                SetBaseConstraints(true, trans);
                trans.Commit();
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Enables or disables foreign keys for the configuration database tables.
        /// </summary>
        private void SetBaseConstraints(bool enabled, SqlTransaction trans)
        {
            string checkMode = enabled ? "CHECK" : "NOCHECK";

            foreach (IBaseTable baseTable in project.ConfigDatabase.AllTables)
            {
                string sql = $"ALTER TABLE {GetBaseTableName(baseTable)} {checkMode} CONSTRAINT ALL";
                new SqlCommand(sql, conn, trans).ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts rows in the configuration database table.
        /// </summary>
        private void InsertRows(IBaseTable baseTable, SqlTransaction trans)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(baseTable.ItemType);
            int propCnt = props.Count;

            if (propCnt == 0 || baseTable.ItemCount == 0)
                return;

            StringBuilder sbSql1 = new();
            StringBuilder sbSql2 = new();
            sbSql1.Append("INSERT INTO ").Append(GetBaseTableName(baseTable)).Append(" (");
            sbSql2.Append("VALUES (");

            for (int i = 0; i < propCnt; i++)
            {
                if (i > 0)
                {
                    sbSql1.Append(", ");
                    sbSql2.Append(", ");
                }

                PropertyDescriptor prop = props[i];
                sbSql1.Append(GetBaseColumnName(prop));
                sbSql2.Append('@').Append(prop.Name);
            }

            sbSql1.Append(") ");
            sbSql2.Append(");");

            string sql = sbSql1.ToString() + sbSql2.ToString();
            SqlCommand cmd = new(sql, conn, trans);

            foreach (PropertyDescriptor prop in props)
            {
                AddParameter(cmd.Parameters, prop.Name, prop.PropertyType);
            }

            bool filterByObj = uploadOptions.ObjectFilter.Count > 0 &&
                (baseTable.ItemType == typeof(Cnl) || baseTable.ItemType == typeof(View));

            foreach (object item in filterByObj ?
                SelectItems(baseTable, uploadOptions.ObjectFilter) : baseTable.EnumerateItems())
            {
                for (int i = 0; i < propCnt; i++)
                {
                    cmd.Parameters[i].Value = props[i].GetValue(item) ?? DBNull.Value;
                }

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Creates foreign keys for the configuration database tables.
        /// </summary>
        private void CreateForeignKeys()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(ExtensionPhrases.CreateFKs);
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                progressTracker.SubtaskCount = project.ConfigDatabase.AllTables.Length;

                foreach (IBaseTable baseTable in project.ConfigDatabase.AllTables)
                {
                    transferControl.ThrowIfCancellationRequested();
                    transferControl.WriteMessage(string.Format(ExtensionPhrases.CreateTableFKs, baseTable.Name));

                    foreach (TableRelation relation in baseTable.DependsOn)
                    {
                        string sql = GetBaseForeignKeyDDL(relation);
                        new SqlCommand(sql, conn, trans).ExecuteNonQuery();
                    }

                    progressTracker.SubtaskIndex++;
                }

                trans.Commit();
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Removes the view table.
        /// </summary>
        private void ClearViews()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(ExtensionPhrases.ClearViews);

            string sql = GetDropTableDDL("view_file");
            new SqlCommand(sql, conn).ExecuteNonQuery();
            progressTracker.TaskIndex++;
        }

        /// <summary>
        /// Creates and fills the view table.
        /// </summary>
        private void CreateViews()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteMessage(ExtensionPhrases.CreateViews);
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                new SqlCommand(GetViewTableDDL(), conn, trans).ExecuteNonQuery();
                ICollection<FileInfo> viewFiles = GetViewFiles();

                if (viewFiles.Count > 0)
                {
                    string sql = $"INSERT INTO {GetTableName("view_file")} (path, contents, write_time) " +
                        "VALUES (@path, @contents, @writeTime)";
                    SqlCommand cmd = new(sql, conn, trans);
                    SqlParameter pathParam = cmd.Parameters.Add("@path", SqlDbType.NVarChar, 400);
                    SqlParameter contentsParam = cmd.Parameters.Add("@contents", SqlDbType.VarBinary, -1);
                    SqlParameter writeTimeParam = cmd.Parameters.Add("@writeTime", SqlDbType.DateTime2);

                    int viewDirLen = project.Views.ViewDir.Length;
                    progressTracker.SubtaskCount = viewFiles.Count;

                    foreach (FileInfo fileInfo in viewFiles)
                    {
                        transferControl.ThrowIfCancellationRequested();
                        transferControl.WriteMessage(string.Format(ExtensionPhrases.CreateView, fileInfo.Name));

                        pathParam.Value = fileInfo.FullName[viewDirLen..];
                        contentsParam.Value = File.ReadAllBytes(fileInfo.FullName);
                        writeTimeParam.Value = fileInfo.LastWriteTimeUtc;
                        cmd.ExecuteNonQuery();
                        progressTracker.SubtaskIndex++;
                    }
                }

                trans.Commit();
                transferControl.WriteMessage(string.Format(AdminPhrases.FileCount, viewFiles.Count));
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Gets a collection of view files.
        /// </summary>
        private ICollection<FileInfo> GetViewFiles()
        {
            if (uploadOptions.ObjectFilter.Count > 0)
            {
                List<FileInfo> fileInfoList = [];
                HashSet<string> pathSet = [];

                foreach (View view in SelectItems(project.ConfigDatabase.ViewTable, uploadOptions.ObjectFilter))
                {
                    if (pathSet.Add(view.Path))
                    {
                        string fileName = Path.Combine(project.Views.ViewDir, view.Path);
                        FileInfo fileInfo = new(fileName);

                        if (fileInfo.Exists)
                            fileInfoList.Add(fileInfo);
                    }
                }

                return fileInfoList;
            }
            else
            {
                DirectoryInfo viewDirInfo = new(project.Views.ViewDir);
                return viewDirInfo.Exists ? viewDirInfo.GetFiles("*", SearchOption.AllDirectories) : [];
            }
        }

        /// <summary>
        /// Clears the configuration of all applications.
        /// </summary>
        private void ClearAllAppConfig()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(ExtensionPhrases.ClearAllAppConfig);

            string sql = GetDropTableDDL("app_config");
            new SqlCommand(sql, conn).ExecuteNonQuery();
            progressTracker.TaskIndex++;
        }

        /// <summary>
        /// Clears the configuration of the specified application.
        /// </summary>
        private void ClearAppConfig(ProjectApp app)
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(string.Format(ExtensionPhrases.ClearAppConfig, app.AppName));

            string sql = $"DELETE FROM {GetTableName("app_config")} WHERE app_id = @appID" +
                (uploadOptions.IgnoreRegKeys ? " AND path NOT LIKE @regPattern" : "");
            SqlCommand cmd = new(sql, conn);
            cmd.Parameters.Add("@appID", SqlDbType.Int).Value = (int)app.ServiceApp;

            if (uploadOptions.IgnoreRegKeys)
                cmd.Parameters.Add("@regPattern", SqlDbType.NVarChar, 400).Value = "%" + ScadaUtils.RegFileSuffix;

            cmd.ExecuteNonQuery();
            progressTracker.TaskIndex++;
        }

        /// <summary>
        /// Creates and fills the configuration of the specified application.
        /// </summary>
        private void CreateAppConfig(ProjectApp app)
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteMessage(string.Format(ExtensionPhrases.CreateAppConfig, app.AppName));
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                new SqlCommand(GetAppConfigTableDDL(), conn, trans).ExecuteNonQuery();
                ICollection<FileInfo> configFiles = GetAppConfigFiles(app);

                if (configFiles.Count > 0)
                {
                    string sql = $"INSERT INTO {GetTableName("app_config")} (app_id, path, contents, write_time) " +
                        "VALUES (@appID, @path, @contents, @writeTime)";
                    SqlCommand cmd = new(sql, conn, trans);
                    cmd.Parameters.Add("@appID", SqlDbType.Int).Value = (int)app.ServiceApp;
                    SqlParameter pathParam = cmd.Parameters.Add("@path", SqlDbType.NVarChar, 400);
                    SqlParameter contentsParam = cmd.Parameters.Add("@contents", SqlDbType.NVarChar, -1);
                    SqlParameter writeTimeParam = cmd.Parameters.Add("@writeTime", SqlDbType.DateTime2);

                    int configDirLen = app.ConfigDir.Length;
                    progressTracker.SubtaskCount = configFiles.Count;

                    foreach (FileInfo fileInfo in configFiles)
                    {
                        transferControl.ThrowIfCancellationRequested();
                        transferControl.WriteMessage(string.Format(ExtensionPhrases.CreateConfigFile, fileInfo.Name));

                        pathParam.Value = fileInfo.FullName[configDirLen..];
                        contentsParam.Value = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
                        writeTimeParam.Value = fileInfo.LastWriteTimeUtc;
                        cmd.ExecuteNonQuery();
                        progressTracker.SubtaskIndex++;
                    }
                }

                trans.Commit();
                transferControl.WriteMessage(string.Format(AdminPhrases.FileCount, configFiles.Count));
                progressTracker.TaskIndex++;
            }
            catch
            {
                trans?.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Gets a collection of application configuration files.
        /// </summary>
        private ICollection<FileInfo> GetAppConfigFiles(ProjectApp app)
        {
            DirectoryInfo configDirInfo = new(app.ConfigDir);

            if (configDirInfo.Exists)
            {
                if (uploadOptions.IgnoreRegKeys)
                {
                    List<FileInfo> fileInfoList = [];

                    foreach (FileInfo fileInfo in configDirInfo.EnumerateFiles("*", SearchOption.AllDirectories))
                    {
                        if (!fileInfo.Name.EndsWith(ScadaUtils.RegFileSuffix, StringComparison.OrdinalIgnoreCase))
                            fileInfoList.Add(fileInfo);
                    }

                    return fileInfoList;
                }
                else
                {
                    return configDirInfo.GetFiles("*", SearchOption.AllDirectories);
                }
            }
            else
            {
                return [];
            }
        }

        /// <summary>
        /// Gets a SQL script to create the configuration database table.
        /// </summary>
        private static string GetBaseTableDDL(IBaseTable baseTable)
        {
            StringBuilder sbSql = new();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(baseTable.ItemType);

            if (props.Count > 0)
            {
                sbSql.AppendLine($"IF OBJECT_ID({GetBaseTableObjectNameLiteral(baseTable)}, N'U') IS NULL");
                sbSql.AppendLine("BEGIN");
                sbSql.AppendLine($"CREATE TABLE {GetBaseTableName(baseTable)} (");

                foreach (PropertyDescriptor prop in props)
                {
                    sbSql
                        .Append(GetBaseColumnName(prop)).Append(' ')
                        .Append(GetDbTypeName(prop.PropertyType))
                        .Append(prop.PropertyType.IsNullable() || prop.PropertyType.IsClass ? "" : " NOT NULL")
                        .AppendLine(",");
                }

                sbSql.AppendLine(
                    $"CONSTRAINT {QuoteName("pk_" + baseTable.Name.ToLowerInvariant())} " +
                    $"PRIMARY KEY ({GetBaseColumnName(baseTable.PrimaryKey)}))");
                sbSql.AppendLine("END");
            }

            return sbSql.ToString();
        }

        /// <summary>
        /// Gets a SQL script to create a foreign key of the configuration database table.
        /// </summary>
        private static string GetBaseForeignKeyDDL(TableRelation relation)
        {
            string fkName = "fk_" +
                relation.ChildTable.Name.ToLowerInvariant() + "_" +
                relation.ChildColumn.ToLowerInvariant();

            return
                $"IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'{EscapeString(fkName)}' " +
                $"AND parent_object_id = OBJECT_ID({GetBaseTableObjectNameLiteral(relation.ChildTable)})) " +
                $"ALTER TABLE {GetBaseTableName(relation.ChildTable)} " +
                $"ADD CONSTRAINT {QuoteName(fkName)} FOREIGN KEY ({GetBaseColumnName(relation.ChildColumn)}) " +
                $"REFERENCES {GetBaseTableName(relation.ParentTable)} ({GetBaseColumnName(relation.ParentTable.PrimaryKey)})";
        }

        /// <summary>
        /// Gets a SQL script to remove a table and its foreign keys.
        /// </summary>
        private static string GetDropTableDDL(IBaseTable baseTable)
        {
            return GetDropTableDDL(baseTable.Name.ToLowerInvariant());
        }

        /// <summary>
        /// Gets a SQL script to remove a table and its foreign keys.
        /// </summary>
        private static string GetDropTableDDL(string tableName)
        {
            string objectNameLiteral = GetObjectNameLiteral(tableName);

            return
                "DECLARE @sql nvarchar(max) = N'';" + Environment.NewLine +
                "SELECT @sql = @sql + N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + " +
                "N'.' + QUOTENAME(OBJECT_NAME(parent_object_id)) + N' DROP CONSTRAINT ' + QUOTENAME(name) + N';' " +
                "FROM sys.foreign_keys " +
                $"WHERE parent_object_id = OBJECT_ID({objectNameLiteral}) OR referenced_object_id = OBJECT_ID({objectNameLiteral});" +
                Environment.NewLine +
                "EXEC sp_executesql @sql;" + Environment.NewLine +
                $"DROP TABLE IF EXISTS {GetTableName(tableName)};";
        }

        /// <summary>
        /// Gets a SQL script to create the view table.
        /// </summary>
        private static string GetViewTableDDL()
        {
            string tableName = "view_file";

            return
                $"IF OBJECT_ID({GetObjectNameLiteral(tableName)}, N'U') IS NULL " +
                "BEGIN " +
                $"CREATE TABLE {GetTableName(tableName)}(" +
                "file_id int IDENTITY(1,1) NOT NULL, " +
                "path nvarchar(400) NOT NULL, " +
                "contents varbinary(max) NULL, " +
                "write_time datetime2(7) NULL, " +
                "CONSTRAINT " + QuoteName("pk_view_file") + " PRIMARY KEY (file_id), " +
                "CONSTRAINT " + QuoteName("un_view_file_path") + " UNIQUE (path)) " +
                "END";
        }

        /// <summary>
        /// Gets a SQL script to create the application table.
        /// </summary>
        private static string GetAppTableDDL()
        {
            string tableName = "app";

            return
                $"IF OBJECT_ID({GetObjectNameLiteral(tableName)}, N'U') IS NULL " +
                "BEGIN " +
                $"CREATE TABLE {GetTableName(tableName)}(" +
                "app_id int NOT NULL, " +
                "name nvarchar(100) NOT NULL, " +
                "CONSTRAINT " + QuoteName("pk_app") + " PRIMARY KEY (app_id)) " +
                "END";
        }

        /// <summary>
        /// Gets a SQL script to create the application configuration table.
        /// </summary>
        private static string GetAppConfigTableDDL()
        {
            string tableName = "app_config";

            return
                $"IF OBJECT_ID({GetObjectNameLiteral(tableName)}, N'U') IS NULL " +
                "BEGIN " +
                $"CREATE TABLE {GetTableName(tableName)}(" +
                "file_id int IDENTITY(1,1) NOT NULL, " +
                "app_id int NOT NULL, " +
                "path nvarchar(400) NOT NULL, " +
                "contents nvarchar(max) NULL, " +
                "write_time datetime2(7) NULL, " +
                "CONSTRAINT " + QuoteName("pk_app_config") + " PRIMARY KEY (file_id), " +
                "CONSTRAINT " + QuoteName("un_app_config_app_path") + " UNIQUE (app_id, path), " +
                "CONSTRAINT " + QuoteName("fk_app_config_app") + $" FOREIGN KEY (app_id) REFERENCES {GetTableName("app")} (app_id)) " +
                "END; " +
                "IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'fki_app_config_app_fkey' " +
                $"AND object_id = OBJECT_ID({GetObjectNameLiteral(tableName)})) " +
                $"CREATE INDEX {QuoteName("fki_app_config_app_fkey")} ON {GetTableName(tableName)} (app_id);";
        }

        /// <summary>
        /// Gets the database type name corresponding to the specified property type.
        /// </summary>
        private static string GetDbTypeName(Type propType)
        {
            Type type = propType.IsNullable() ? Nullable.GetUnderlyingType(propType) : propType;

            if (type == typeof(int))
                return "int";
            else if (type == typeof(double))
                return "float";
            else if (type == typeof(bool))
                return "bit";
            else if (type == typeof(DateTime))
                return "datetime2(7)";
            else if (type == typeof(string))
                return "nvarchar(max)";
            else
                throw new ScadaException("Data type {0} is not supported.", type.FullName);
        }

        /// <summary>
        /// Gets the database type corresponding to the specified property type.
        /// </summary>
        private static SqlDbType GetDbType(Type propType)
        {
            Type type = propType.IsNullable() ? Nullable.GetUnderlyingType(propType) : propType;

            if (type == typeof(int))
                return SqlDbType.Int;
            else if (type == typeof(double))
                return SqlDbType.Float;
            else if (type == typeof(bool))
                return SqlDbType.Bit;
            else if (type == typeof(DateTime))
                return SqlDbType.DateTime2;
            else if (type == typeof(string))
                return SqlDbType.NVarChar;
            else
                throw new ScadaException("Data type {0} is not supported.", type.FullName);
        }

        /// <summary>
        /// Adds a typed SQL parameter.
        /// </summary>
        private static SqlParameter AddParameter(SqlParameterCollection parameters, string parameterName, Type propType)
        {
            SqlDbType dbType = GetDbType(propType);
            SqlParameter parameter = parameters.Add("@" + parameterName, dbType);

            if (dbType == SqlDbType.NVarChar)
                parameter.Size = -1;

            return parameter;
        }

        /// <summary>
        /// Selects items from the specified table filtered by objects.
        /// </summary>
        private static IEnumerable SelectItems(IBaseTable baseTable, List<int> objNums)
        {
            foreach (int objNum in objNums)
            {
                foreach (object item in baseTable.SelectItems(new TableFilter("ObjNum", objNum), true))
                {
                    yield return item;
                }
            }
        }


        /// <summary>
        /// Uploads the configuration.
        /// </summary>
        public void Upload()
        {
            if (!profile.DbEnabled)
                throw new ScadaException(AdminPhrases.DbNotEnabled);

            transferControl.SetCancelEnabled(true);
            transferControl.WriteMessage(AdminPhrases.UploadConfig);

            try
            {
                conn = CreateDbConnection(profile.DbConnectionOptions);
                conn.Open();
                CreateSchema();
                CreateAppTable();

                if (uploadOptions.IncludeBase)
                {
                    if (extensionConfig.ClearBaseMethod == ClearBaseMethod.DropTables)
                    {
                        ClearBase();
                        CreateBase();
                        CreateForeignKeys();
                    }
                    else
                    {
                        TruncateBase();
                        FillBase();
                        progressTracker.SkipTask(1);
                    }
                }
                else
                {
                    progressTracker.SkipTask(3);
                }

                if (uploadOptions.IncludeView)
                {
                    ClearViews();
                    CreateViews();
                }
                else
                {
                    progressTracker.SkipTask(2);
                }

                bool clearAllAppConfig = !uploadOptions.IgnoreRegKeys &&
                    (uploadOptions.IncludeServer || !instance.ServerApp.Enabled) &&
                    (uploadOptions.IncludeComm || !instance.CommApp.Enabled) &&
                    (uploadOptions.IncludeWeb || !instance.WebApp.Enabled);

                if (clearAllAppConfig)
                    ClearAllAppConfig();
                else
                    progressTracker.SkipTask();

                void UploadAppConfig(bool includeApp, ProjectApp app)
                {
                    if (includeApp && app.Enabled)
                    {
                        if (clearAllAppConfig)
                        {
                            progressTracker.SkipTask();
                            transferControl.WriteLine();
                        }
                        else
                        {
                            ClearAppConfig(app);
                        }

                        CreateAppConfig(app);
                    }
                    else
                    {
                        progressTracker.SkipTask(2);
                    }
                }

                UploadAppConfig(uploadOptions.IncludeServer, instance.ServerApp);
                UploadAppConfig(uploadOptions.IncludeComm, instance.CommApp);
                UploadAppConfig(uploadOptions.IncludeWeb, instance.WebApp);

                if (!uploadOptions.RestartAnyService)
                {
                    progressTracker.SkipTask();
                }
                else if (profile.AgentEnabled)
                {
                    AgentClient agentClient = new(profile.AgentConnectionOptions);
                    new ServiceStarter(agentClient, instance, uploadOptions, transferControl, progressTracker)
                        .SetProcessTimeout(profile.AgentConnectionOptions.Timeout)
                        .RestartServices();
                    agentClient.TerminateSession();
                }
                else
                {
                    transferControl.WriteLine();
                    transferControl.WriteError(ExtensionPhrases.UnableRestartServices);
                    progressTracker.SkipTask();
                }
            }
            finally
            {
                conn?.Close();
                conn = null;
            }

            progressTracker.SetCompleted();
            transferControl.WriteLine();
            transferControl.WriteMessage(AdminPhrases.UploadConfigCompleted);
        }
    }
}