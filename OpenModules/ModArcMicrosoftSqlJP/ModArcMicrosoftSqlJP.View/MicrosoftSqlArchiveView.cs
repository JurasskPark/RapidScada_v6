// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Server.Archives;
using Scada.Server.Config;
using Scada.Server.Modules.ModArcMicrosoftSqlJP.View.Forms;

namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.View
{
    /// <summary>
    /// Implements the archive user interface.
    /// <para>Реализует пользовательский интерфейс архива.</para>
    /// </summary>
    internal class MicrosoftSqlArchiveView : ArchiveView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MicrosoftSqlArchiveView(ModuleView parentView, ArchiveConfig archiveConfig)
            : base(parentView, archiveConfig)
        {
            CanShowProperties = true;
        }

        /// <summary>
        /// Shows a modal dialog box for editing archive properties.
        /// </summary>
        public override bool ShowProperties()
        {
            Form form = ArchiveConfig.Kind switch
            {
                ArchiveKind.Current => new FrmMicrosoftSqlCAO(AppDirs, ArchiveConfig),
                ArchiveKind.Historical => new FrmMicrosoftSqlHAO(AppDirs, ArchiveConfig),
                ArchiveKind.Events => new FrmMicrosoftSqlEAO(AppDirs, ArchiveConfig),
                _ => null
            };

            return form != null && form.ShowDialog() == DialogResult.OK;
        }
    }
}
