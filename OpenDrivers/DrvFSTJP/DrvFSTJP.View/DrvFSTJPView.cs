using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvFSTJP.View
{
    /// <summary>
    /// Implements the driver view for ScadaAdmin.
    /// <para>Реализует представление драйвера для ScadaAdmin.</para>
    /// </summary>
    public class DrvFSTJPView : DriverView
    {
        public DrvFSTJPView()
        {
            CanCreateDevice = true;
        }

        public override string Name => DriverUtils.Name(Locale.IsRussian);

        public override string Descr => DriverUtils.Description(Locale.IsRussian);

        public override void LoadDictionaries()
        {
            CommonPhrases.Init();
        }

        public override DeviceView CreateDeviceView(LineConfig lineConfig, DeviceConfig deviceConfig)
        {
            return new DevFSTJPView(this, lineConfig, deviceConfig);
        }
    }
}
