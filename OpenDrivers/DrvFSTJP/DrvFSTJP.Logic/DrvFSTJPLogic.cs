using Scada.Comm.Config;
using Scada.Comm.Devices;

namespace Scada.Comm.Drivers.DrvFSTJP.Logic
{
    /// <summary>
    /// Implements the FST-03x driver logic.
    /// <para>Реализует логику драйвера ФСТ-03х.</para>
    /// </summary>
    internal class DrvFSTJPLogic : DriverLogic
    {
        public DrvFSTJPLogic(ICommContext commContext)
            : base(commContext)
        {
        }

        public override string Code => DriverUtils.DriverCode;

        public override DeviceLogic CreateDevice(ILineContext lineContext, DeviceConfig deviceConfig)
        {
            return new DevFSTJPLogic(CommContext, lineContext, deviceConfig);
        }
    }
}
