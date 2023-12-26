using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvPingJPLogic.Logic;

namespace Scada.Comm.Drivers.DrvPingJP.Logic
{

    /// <summary>
    /// Implements the driver logic.
    /// <para>Реализует логику драйвера.</para>
    /// </summary>
    public class DrvPingJPLogic : DriverLogic
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvPingJPLogic(ICommContext commContext)
            : base(commContext)
        {
        }

        /// <summary>
        /// Gets the driver code.
        /// </summary>
        public override string Code
        {
            get
            {
                return "DrvPingJP";
            }
        }

        /// <summary>
        /// Creates a new device.
        /// </summary>
        public override DeviceLogic CreateDevice(ILineContext lineContext, DeviceConfig deviceConfig)
        {
            return new DevPingJPLogic(CommContext, lineContext, deviceConfig);
        }
    }
}