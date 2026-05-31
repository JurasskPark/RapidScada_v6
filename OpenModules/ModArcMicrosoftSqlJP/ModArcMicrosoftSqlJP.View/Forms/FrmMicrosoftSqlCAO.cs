// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Forms;
using Scada.Server.Config;
using Scada.Server.Modules.ModArcMicrosoftSqlJP.Config;
using Scada.Server.Modules.ModArcMicrosoftSqlJP.View.Controls;

namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.View.Forms
{
    /// <summary>
    /// Represents a form for editing current archive options.
    /// <para>Представляет форму для редактирования параметров архива текущих данных.</para>
    /// </summary>
    public partial class FrmMicrosoftSqlCAO : Form
    {
        private readonly AppDirs appDirs;             // the application directories
        private readonly ArchiveConfig archiveConfig; // the archive configuration
        private readonly MicrosoftSqlCAO options;          // the archive options


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmMicrosoftSqlCAO()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmMicrosoftSqlCAO(AppDirs appDirs, ArchiveConfig archiveConfig)
            : this()
        {
            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.archiveConfig = archiveConfig ?? throw new ArgumentNullException(nameof(archiveConfig));
            options = new MicrosoftSqlCAO(archiveConfig.CustomOptions);
        }


        /// <summary>
        /// Sets the controls according to the options.
        /// </summary>
        private void OptionsToControls()
        {
            ctrlCurrentArchiveOptions.ArchiveOptions = options;
            ctrlDatabaseOptions.DatabaseOptions = options;
        }

        /// <summary>
        /// Sets the options according to the controls.
        /// </summary>
        private void ControlsToOptions()
        {
            ctrlCurrentArchiveOptions.ControlsToOptions();
            ctrlDatabaseOptions.ControlsToOptions();
            options.AddToOptionList(archiveConfig.CustomOptions);
        }


        private void FrmMicrosoftSqlHAO_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);
            FormTranslator.Translate(ctrlCurrentArchiveOptions, ctrlCurrentArchiveOptions.GetType().FullName);
            FormTranslator.Translate(ctrlDatabaseOptions, ctrlDatabaseOptions.GetType().FullName);

            OptionsToControls();
            UiUtils.FillConnections(ctrlDatabaseOptions.ConnectionComboBox, appDirs.ConfigDir);
        }

        private void btnManageConn_Click(object sender, EventArgs e)
        {
            UiUtils.EditConnections(ctrlDatabaseOptions.ConnectionComboBox, appDirs.ConfigDir);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ControlsToOptions();
            DialogResult = DialogResult.OK;
        }
    }
}
