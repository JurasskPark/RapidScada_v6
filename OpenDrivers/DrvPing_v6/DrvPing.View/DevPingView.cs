// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvPing.View.Forms;

namespace Scada.Comm.Drivers.DrvPing.View
{
    /// <summary>
    /// Implements the data source user interface.
    /// <para>Реализует пользовательский интерфейс источника данных.</para>
    /// </summary>
    internal class DevPingView : DeviceView
    {

        private DrvPingConfig config = new DrvPingConfig();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevPingView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
            : base(parentView, lineConfig, deviceConfig)
        {
            CanShowProperties = true;
        }

        /// <summary>
        /// Shows a modal dialog box for editing data source properties.
        /// </summary>
        public override bool ShowProperties()
        {
            if (new FrmConfig(AppDirs, DeviceNum).ShowDialog() == DialogResult.OK) 
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
            return new PollingOptions(10000, 200);
        }

        /// <summary>
        /// Gets the channel prototypes for the device.
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            return CnlPrototypeFactory.GetCnlPrototypeGroups(config.DeviceTags).GetCnlPrototypes();
        }

    }
}
