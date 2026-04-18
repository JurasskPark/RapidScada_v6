using Scada.Comm.Config;
using Scada.Comm.Devices;

namespace Scada.Comm.Drivers.DrvDDEJP.Logic
{
    /// <summary>
    /// Provides the driver logic entry point for DrvDDEJP.
    /// <para>Реализует точку входа логики драйвера DrvDDEJP.</para>
    /// </summary>
    internal class DrvDDEJPLogic : DriverLogic
    {
        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        /// <param name="commContext">The communication context.</param>
        public DrvDDEJPLogic(ICommContext commContext)
            : base(commContext)
        {
        }

        /// <summary>
        /// Gets the driver code.
        /// <para>Возвращает код драйвера.</para>
        /// </summary>
        public override string Code => DriverUtils.DriverCode;

        /// <summary>
        /// Creates a new device logic instance.
        /// <para>Создаёт новый экземпляр логики устройства.</para>
        /// </summary>
        public override DeviceLogic CreateDevice(ILineContext lineContext, DeviceConfig deviceConfig)
        {
            return new DevDDEJPLogic(CommContext, lineContext, deviceConfig);
        }

        #endregion Basic
    }
}
