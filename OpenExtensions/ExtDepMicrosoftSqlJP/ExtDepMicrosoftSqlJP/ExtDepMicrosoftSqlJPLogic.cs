// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.SqlClient;
using Scada.Admin.Config;
using Scada.Admin.Deployment;
using Scada.Admin.Extensions.ExtDepMicrosoftSqlJP.Config;
using Scada.Admin.Lang;
using Scada.Admin.Project;
using Scada.ComponentModel;
using Scada.Dbms;
using Scada.Forms;
using Scada.Forms.Forms;
using Scada.Lang;
using System;
using System.IO;
using System.Windows.Forms;
using static Scada.Storages.MicrosoftSqlStorage.MicrosoftSqlStorageShared;

namespace Scada.Admin.Extensions.ExtDepMicrosoftSqlJP
{
    /// <summary>
    /// Represents an extension logic.
    /// </summary>
    public class ExtDepMicrosoftSqlJPLogic : ExtensionLogic
    {
        private readonly ExtensionConfig extensionConfig;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ExtDepMicrosoftSqlJPLogic(IAdminContext adminContext)
            : base(adminContext)
        {
            extensionConfig = new ExtensionConfig();

            CanShowProperties = true;
            CanDeploy = true;
        }


        /// <summary>
        /// Gets the full name of the extension configuration file.
        /// </summary>
        private string ConfigFileName =>
            Path.Combine(AdminContext.AppDirs.ConfigDir, ExtensionConfig.DefaultFileName);

        /// <summary>
        /// Gets the extension code.
        /// </summary>
        public override string Code
        {
            get
            {
                return "ExtDepMicrosoftSqlJP";
            }
        }

        /// <summary>
        /// Gets the extension name.
        /// </summary>
        public override string Name
        {
            get
            {
                return Locale.IsRussian ? "Развёртывание в Microsoft SQL Server" : "Deployment to Microsoft SQL Server";
            }
        }

        /// <summary>
        /// Gets the extension description.
        /// </summary>
        public override string Descr
        {
            get
            {
                return Locale.IsRussian ?
                    "Расширение обеспечивает передачу проекта в базу данных Microsoft SQL Server." :
                    "The extension provides project transfer to a Microsoft SQL Server database.";
            }
        }


        /// <summary>
        /// Loads language dictionaries.
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AdminContext.AppDirs.LangDir, Code, out string errMsg))
                AdminContext.ErrLog.WriteError(AdminPhrases.ExtensionMessage, Code, errMsg);

            ExtensionPhrases.Init();
            AttrTranslator.Translate(typeof(ExtensionConfig));
        }

        /// <summary>
        /// Loads the extension configuration.
        /// </summary>
        public override void LoadConfig()
        {
            string fileName = ConfigFileName;

            if (File.Exists(fileName) &&
                !extensionConfig.Load(fileName, out string errMsg))
            {
                AdminContext.ErrLog.WriteError(AdminPhrases.ExtensionMessage, Code, errMsg);
            }
        }

        /// <summary>
        /// Shows a modal dialog box for editing extension properties.
        /// </summary>
        public override void ShowProperties(AdminConfig adminConfig)
        {
            FrmOptions frmOptions = new() { Options = extensionConfig };

            if (frmOptions.ShowDialog() == DialogResult.OK &&
                !extensionConfig.Save(ConfigFileName, out string errMsg))
            {
                AdminContext.ErrLog.HandleError(errMsg);
            }
        }

        /// <summary>
        /// Tests a database connection.
        /// </summary>
        public override bool TestDbConnection(DbConnectionOptions connectionOptions, out string errMsg)
        {
            if (connectionOptions.KnownDBMS == KnownDBMS.MSSQL)
            {
                SqlConnection conn = null;

                try
                {
                    conn = CreateDbConnection(connectionOptions);
                    conn.Open();
                    errMsg = "";
                    return true;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    return false;
                }
                finally
                {
                    conn?.Close();
                }
            }
            else
            {
                errMsg = CommonPhrases.DatabaseNotSupported;
                return false;
            }
        }

        /// <summary>
        /// Downloads the project configuration.
        /// </summary>
        public override void DownloadConfig(ScadaProject project, ProjectInstance instance, DeploymentProfile profile,
            ITransferControl transferControl)
        {
            new Downloader(project, instance, profile, transferControl).Download();
        }

        /// <summary>
        /// Uploads the project configuration.
        /// </summary>
        public override void UploadConfig(ScadaProject project, ProjectInstance instance, DeploymentProfile profile,
            ITransferControl transferControl)
        {
            new Uploader(project, instance, profile, transferControl, extensionConfig).Upload();
        }
    }
}