// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.SqlClient;
using Scada.Admin.Deployment;
using Scada.Admin.Lang;
using Scada.Admin.Project;
using Scada.Data.Tables;
using System;
using System.IO;
using System.Text;
using static Scada.Storages.MicrosoftSqlStorage.MicrosoftSqlStorageShared;

namespace Scada.Admin.Extensions.ExtDepMicrosoftSqlJP
{
    /// <summary>
    /// Downloads configuration.
    /// </summary>
    internal class Downloader
    {
        /// <summary>
        /// The number of download tasks.
        /// </summary>
        private const int TaskCount = 5;

        private readonly ScadaProject project;
        private readonly ProjectInstance instance;
        private readonly DeploymentProfile profile;
        private readonly ITransferControl transferControl;
        private readonly DownloadOptions downloadOptions;
        private readonly ProgressTracker progressTracker;
        private SqlConnection conn;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Downloader(ScadaProject project, ProjectInstance instance, DeploymentProfile profile,
            ITransferControl transferControl)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
            this.instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this.profile = profile ?? throw new ArgumentNullException(nameof(profile));
            this.transferControl = transferControl ?? throw new ArgumentNullException(nameof(transferControl));
            downloadOptions = profile.DownloadOptions;
            progressTracker = new ProgressTracker(transferControl) { TaskCount = TaskCount };
            conn = null;
        }


        /// <summary>
        /// Downloads the configuration database.
        /// </summary>
        private void DownloadBase()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(AdminPhrases.DownloadBase);
            progressTracker.SubtaskCount = project.ConfigDatabase.AllTables.Length;

            foreach (IBaseTable baseTable in project.ConfigDatabase.AllTables)
            {
                transferControl.ThrowIfCancellationRequested();
                transferControl.WriteMessage(string.Format(ExtensionPhrases.DownloadTable, baseTable.Name));
                ReadBaseTable(baseTable, conn);
                progressTracker.SubtaskIndex++;
            }

            progressTracker.TaskIndex++;
        }

        /// <summary>
        /// Downloads view files.
        /// </summary>
        private void DownloadViews()
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(AdminPhrases.DownloadViews);

            string sql = $"SELECT path, contents FROM {GetTableName("view_file")}";
            SqlCommand cmd = new(sql, conn);
            int fileCount = 0;
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read() && !reader.IsDBNull(1))
            {
                transferControl.ThrowIfCancellationRequested();
                string path = reader.GetString(0);
                transferControl.WriteMessage(string.Format(ExtensionPhrases.DownloadView, path));

                string absolutePath = Path.Combine(project.Views.ViewDir, path);
                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
                File.WriteAllBytes(absolutePath, (byte[])reader[1]);
                fileCount++;
            }

            transferControl.WriteMessage(string.Format(AdminPhrases.FileCount, fileCount));
            progressTracker.TaskIndex++;
        }

        /// <summary>
        /// Downloads the configuration of the specified application.
        /// </summary>
        private void DownloadAppConfig(ProjectApp app)
        {
            transferControl.ThrowIfCancellationRequested();
            transferControl.WriteLine();
            transferControl.WriteMessage(string.Format(AdminPhrases.DownloadAppConfig, app.AppName));

            string sql = $"SELECT path, contents FROM {GetTableName("app_config")} WHERE app_id = @appID" +
                (downloadOptions.IgnoreRegKeys ? " AND path NOT LIKE @regPattern" : "");
            SqlCommand cmd = new(sql, conn);
            cmd.Parameters.Add("@appID", System.Data.SqlDbType.Int).Value = (int)app.ServiceApp;

            if (downloadOptions.IgnoreRegKeys)
                cmd.Parameters.Add("@regPattern", System.Data.SqlDbType.NVarChar, 400).Value = "%" + ScadaUtils.RegFileSuffix;

            int fileCount = 0;
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read() && !reader.IsDBNull(1))
            {
                transferControl.ThrowIfCancellationRequested();
                string path = reader.GetString(0);
                transferControl.WriteMessage(string.Format(ExtensionPhrases.DownloadConfigFile, path));

                string absolutePath = Path.Combine(app.ConfigDir, path);
                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
                File.WriteAllText(absolutePath, reader.GetString(1), Encoding.UTF8);
                fileCount++;
            }

            transferControl.WriteMessage(string.Format(AdminPhrases.FileCount, fileCount));
            progressTracker.TaskIndex++;
        }


        /// <summary>
        /// Downloads the configuration.
        /// </summary>
        public void Download()
        {
            if (!profile.DbEnabled)
                throw new ScadaException(AdminPhrases.DbNotEnabled);

            transferControl.SetCancelEnabled(true);
            transferControl.WriteMessage(AdminPhrases.DownloadConfig);

            try
            {
                conn = CreateDbConnection(profile.DbConnectionOptions);
                conn.Open();

                if (downloadOptions.IncludeBase)
                    DownloadBase();
                else
                    progressTracker.SkipTask();

                if (downloadOptions.IncludeView)
                    DownloadViews();
                else
                    progressTracker.SkipTask();

                if (downloadOptions.IncludeServer && instance.ServerApp.Enabled)
                    DownloadAppConfig(instance.ServerApp);
                else
                    progressTracker.SkipTask();

                if (downloadOptions.IncludeComm && instance.CommApp.Enabled)
                    DownloadAppConfig(instance.CommApp);
                else
                    progressTracker.SkipTask();

                if (downloadOptions.IncludeWeb && instance.WebApp.Enabled)
                    DownloadAppConfig(instance.WebApp);
                else
                    progressTracker.SkipTask();
            }
            finally
            {
                conn?.Close();
                conn = null;
            }

            progressTracker.SetCompleted();
            transferControl.WriteLine();
            transferControl.WriteMessage(AdminPhrases.DownloadConfigCompleted);
        }
    }
}