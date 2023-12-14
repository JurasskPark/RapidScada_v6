// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDbImportPlus.View.Forms;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Forms;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View
{
    /// <summary>
    /// Implements the data source user interface.
    /// <para>Реализует пользовательский интерфейс источника данных.</para>
    /// </summary>
    internal class DevDbImportPlusView : DeviceView
    {

        private DrvDbImportPlusConfig config = new DrvDbImportPlusConfig();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevDbImportPlusView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
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
            return new PollingOptions(0, 0) { Period = new TimeSpan(0, 0, 0, 5) };
        }

        /// <summary>
        /// Gets the channel prototypes for the device.
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            string configFileName = Path.Combine(AppDirs.ConfigDir, DrvDbImportPlusConfig.GetFileName(DeviceNum));

            // load a configuration
            if (File.Exists(configFileName) && !config.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            List<CnlPrototype> cnlPrototypes = new List<CnlPrototype>();

            for (int index = 0; index < config.DeviceTags.Count; ++index)
            {
                Tag tmpTag = config.DeviceTags[index];
                int indexTag = config.DeviceTags.IndexOf(config.DeviceTags[index]);

                // create channel for element
                bool isBool = tmpTag.TagEnabled;

                int eventMask = new EventMask
                {
                    Enabled = true,
                    DataChange = isBool,
                    StatusChange = !isBool,
                    Command = !isBool
                }.Value;

                string tmpTagformat = string.Empty;
                switch(tmpTag.TagFormat.ToString())
                {
                    case "Float":
                        tmpTagformat = "N" + tmpTag.NumberDecimalPlaces;
                        break;
                    case "DateTime":
                        tmpTagformat = FormatCode.DateTime;
                        break;
                    case "String":
                        tmpTagformat = FormatCode.String;
                        break;
                    case "Integer":
                        tmpTagformat = FormatCode.N0;
                        break;
                    case "Boolean":
                        tmpTagformat = FormatCode.OffOn;
                        break;
                }

                cnlPrototypes.Add(new CnlPrototype
                {
                    Active = tmpTag.TagEnabled,
                    Name = tmpTag.TagName,
                    CnlTypeID = CnlTypeID.Input,
                    TagNum = indexTag + 1,
                    TagCode = tmpTag.TagCode,
                    FormatCode = tmpTagformat,
                    EventMask = eventMask
                });
            }

            return cnlPrototypes;
        }

    }
}
