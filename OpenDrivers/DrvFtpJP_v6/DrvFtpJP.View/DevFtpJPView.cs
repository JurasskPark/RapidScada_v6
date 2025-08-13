// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvFtpJP.View.Forms;
using Scada.Comm.Drivers.DrvFtpJP;
using Scada.Forms;
using ManagerAssistant;

namespace Scada.Comm.Drivers.DrvFtpJP.View
{
    /// <summary>
    /// Implements the data source user interface.
    /// <para>Реализует пользовательский интерфейс источника данных.</para>
    /// </summary>
    internal class DevFtpJPView : DeviceView
    {

        private Project project = new Project();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevFtpJPView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
            : base(parentView, lineConfig, deviceConfig)
        {
            CanShowProperties = true;
            Manager.DeviceNum = deviceConfig.DeviceNum;
        }

        /// <summary>
        /// Shows a modal dialog box for editing data source properties.
        /// </summary>
        public override bool ShowProperties()
        {
            if (new FrmConfig(AppDirs, LineConfig, DeviceConfig, DeviceNum).ShowDialog() == DialogResult.OK) 
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
            return new PollingOptions(0, 0) { Period = new TimeSpan(0, 0, 0, 5) };
        }

        /// <summary>
        /// Gets the channel prototypes for the device.
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            string configFileName = Path.Combine(AppDirs.ConfigDir, Project.GetFileName(DeviceNum));

            // load a configuration
            if (File.Exists(configFileName) && !project.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            List<CnlPrototype> cnlPrototypes = new List<CnlPrototype>();
            List<CnlPrototypeGroup> cnlPrototypeGroups = CnlPrototypeFactory.GetCnlPrototypeGroups(project);
            cnlPrototypes = cnlPrototypeGroups.GetCnlPrototypes();

            for (int i = 0; i < cnlPrototypes.Count; i++)
            {
                cnlPrototypes[i].TagNum = i + 1;
            }

            return cnlPrototypes;
        }

    }
}
