using Scada.Comm.Config;
using Scada.Comm.Devices;

namespace Scada.Comm.Drivers.DrvTelnetJP.Logic
{
    /// <summary>
    /// Implements the driver logic.
    /// <para>Реализует логику драйвера.</para>
    /// </summary>
    public class DrvTelnetJPLogic : DriverLogic
    {
        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DrvTelnetJPLogic(ICommContext commContext)
            : base(commContext)
        {
        }

        /// <summary>
        /// Gets the driver code.
        /// <para>Возвращает код драйвера.</para>
        /// </summary>
        public override string Code => DriverUtils.DriverCode;

        /// <summary>
        /// Creates a new device.
        /// <para>Создает новый КП.</para>
        /// </summary>
        public override DeviceLogic CreateDevice(ILineContext lineContext, DeviceConfig deviceConfig)
        {
            return new DevTelnetJPLogic(CommContext, lineContext, deviceConfig);
        }

        #endregion Basic
    }
}
