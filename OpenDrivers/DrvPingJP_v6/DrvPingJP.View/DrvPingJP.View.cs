using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvPingJP.View
{
    /// <summary>
    /// Implements the driver user interface.
    /// <para>Представление пользовательского интерфейса драйвера.</para>
    /// </summary>
    public class DrvPingJPView : DriverView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvPingJPView()
        {
            CanCreateDevice = true;
        }


        /// <summary>
        /// Gets the driver name.
        /// </summary>
        public override string Name => DriverUtils.Name(Locale.IsRussian);

        /// <summary>
        /// Gets the driver description.
        /// </summary>
        public override string Descr => DriverUtils.Description(Locale.IsRussian);


        /// <summary>
        /// Loads language dictionaries.
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AppDirs.LangDir, DriverUtils.DriverCode, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            Locale.GetDictionary("Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmConfig");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmInputBox");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmTag");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvPingJP.View.Forms.FrmHostSearch");
            Locale.GetDictionary("Scada.Comm.Drivers");

            CommonPhrases.Init();
            DriverPhrases.Init();
        }

        /// <summary>
        /// Creates a new device user interface.
        /// </summary>
        public override DeviceView CreateDeviceView(LineConfig lineConfig, DeviceConfig deviceConfig)
        {
            return new DevPingJPView(this, lineConfig, deviceConfig);
        }
    }
}
