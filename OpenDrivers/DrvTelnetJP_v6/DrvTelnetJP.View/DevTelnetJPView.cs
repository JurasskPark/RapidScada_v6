using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvTelnetJP.View.Forms;
using Scada.Data.Const;
using Scada.Data.Models;
using Scada.Forms;

namespace Scada.Comm.Drivers.DrvTelnetJP.View
{
    /// <summary>
    /// Implements the data source user interface.
    /// <para>Реализует пользовательский интерфейс источника данных.</para>
    /// </summary>
    internal class DevPingJPView : DeviceView
    {

        private DrvTelnetJPConfig config = new DrvTelnetJPConfig();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevPingJPView(DriverView parentView, LineConfig lineConfig, DeviceConfig deviceConfig)
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
            return new PollingOptions(0, 0) { Period = new TimeSpan(0, 0, 0, 1) };
        }

        /// <summary>
        /// Gets the channel prototypes for the device.
        /// </summary>
        public override ICollection<CnlPrototype> GetCnlPrototypes()
        {
            string configFileName = Path.Combine(AppDirs.ConfigDir, DrvTelnetJPConfig.GetFileName(DeviceNum));

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

                cnlPrototypes.Add(new CnlPrototype
                {
                    Active = tmpTag.TagEnabled,
                    Name = tmpTag.TagName,
                    CnlTypeID = CnlTypeID.Input,
                    TagNum = indexTag + 1,
                    TagCode = tmpTag.TagCode,
                    FormatCode = FormatCode.OffOn,
                    EventMask = eventMask
                });
            }
            
            return cnlPrototypes;
        }

    }
}
