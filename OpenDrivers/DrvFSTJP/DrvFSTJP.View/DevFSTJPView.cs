using ProjectDriver;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvFSTJP.Protocol;
using Scada.Comm.Drivers.DrvFSTJP.View.Forms;
using Scada.Data.Const;
using Scada.Data.Entities;
using Scada.Forms;
using System;
using System.Collections.Generic;
using System.IO;

namespace Scada.Comm.Drivers.DrvFSTJP.View
{
    /// <summary>
    /// Implements the device view for ScadaAdmin.
    /// <para>Реализует представление КП для ScadaAdmin.</para>
    /// </summary>
    internal class DevFSTJPView : DeviceView
    {
        public DevFSTJPView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
            : base(parentView, lineConfig, deviceConfig)
        {
            CanShowProperties = true;
        }

        public override bool ShowProperties()
        {
            if (new FrmProject(AppDirs, DeviceNum).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DeviceConfigModified = true;
                return true;
            }

            return false;
        }

        public override PollingOptions GetPollingOptions()
        {
            PollingOptions pollingOptions = PollingOptions.CreateDefault();
            pollingOptions.Timeout = 1000;
            pollingOptions.Period = TimeSpan.FromMilliseconds(1000);
            return pollingOptions;
        }

        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            List<CnlPrototype> cnlPrototypes = new List<CnlPrototype>();
            Project project = new Project();
            string configFileName = Path.Combine(AppDirs.ConfigDir, Project.GetFileName(DeviceNum));

            if (!project.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
                return cnlPrototypes;
            }

            int tagNum = 1;
            foreach (FstTagDefinition tagDefinition in FstTagCatalog.Build(project))
            {
                cnlPrototypes.Add(new CnlPrototype
                {
                    Active = true,
                    Name = tagDefinition.Name,
                    Code = tagDefinition.Code,
                    TagCode = tagDefinition.Code,
                    TagNum = tagNum++,
                    CnlTypeID = CnlTypeID.InputOutput,
                    DataLen = 1,
                    DataTypeID = (int)TagDataType.Double,
                    DeviceNum = DeviceNum
                });
            }

            return cnlPrototypes;
        }
    }
}
