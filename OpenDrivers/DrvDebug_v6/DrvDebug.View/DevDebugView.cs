// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDebug.View.Forms;
using Scada.Data.Const;
using Scada.Forms;
using System.IO;
using Project = ProjectDriver.Project;

namespace Scada.Comm.Drivers.DrvDebug.View
{
    /// <summary>
    /// Implements the data source user interface.
    /// <para>Реализует пользовательский интерфейс источника данных.</para>
    /// </summary>
    internal class DevDebugView : DeviceView
    {
        private Project project = new Project();
        private LineConfig LineConfigDriver = new LineConfig();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevDebugView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
            : base(parentView, lineConfig, deviceConfig)
        {
            CanShowProperties = true;
            LineConfigDriver = lineConfig;
        }

        /// <summary>
        /// Shows a modal dialog box for editing data source properties.
        /// </summary>
        public override bool ShowProperties()
        {
            if (new FrmProject(AppDirs, LineConfig, DeviceConfig, DeviceNum).ShowDialog() == DialogResult.OK)
            {
                LineConfigModified = true;
                DeviceConfigModified = true;

                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Gets the default polling options for the device.
        /// </summary>
        public override PollingOptions GetPollingOptions()
        {
            PollingOptions pollingOptions = PollingOptions.CreateDefault();
            return pollingOptions;
        }


        /// <summary>
        /// Gets the channel prototypes for the device.
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            List<CnlPrototype> cnlPrototypes = new List<CnlPrototype>();
            string configFileName = Path.Combine(AppDirs.ConfigDir, DriverUtils.GetFileName(DeviceNum));

            if (File.Exists(configFileName) && !project.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
                return cnlPrototypes;
            }

            int tagNum = 1;
            foreach (ProjectDriver.ProjectTag tag in project.Tags.OrderBy(t => t.Order))
            {
                CnlPrototype prototype = new CnlPrototype
                {
                    Active = tag.Enabled,
                    Name = tag.Name,
                    Code = GetTagCode(tag),
                    TagCode = GetTagCode(tag),
                    TagNum = tagNum++,
                    CnlTypeID = CnlTypeID.InputOutput,
                    DataLen = tag.DataLength,
                    DeviceNum = DeviceNum
                };

                prototype.DataTypeID = tag.DataFormat switch
                {
                    ProjectDriver.TagDataFormat.Ascii => (int)TagDataType.ASCII,
                    ProjectDriver.TagDataFormat.Unicode => (int)TagDataType.Unicode,
                    ProjectDriver.TagDataFormat.Int64 => (int)TagDataType.Int64,
                    ProjectDriver.TagDataFormat.UInt64 => (int)TagDataType.Int64,
                    _ => (int)TagDataType.Double
                };

                cnlPrototypes.Add(prototype);
            }

            return cnlPrototypes;
        }

        /// <summary>
        /// Gets the tag code for the specified tag.
        /// </summary>
        private static string GetTagCode(ProjectDriver.ProjectTag tag)
        {
            return string.IsNullOrWhiteSpace(tag.Name)
                ? $"tag_{tag.Channel}_{tag.Id:N}"
                : tag.Name.Trim().Replace(" ", "_");
        }

    }
}
