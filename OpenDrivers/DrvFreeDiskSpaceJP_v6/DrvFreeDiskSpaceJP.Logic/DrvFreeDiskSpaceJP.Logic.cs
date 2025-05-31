// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvFreeDiskSpaceJPLogic.Logic;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.Logic
{

    /// <summary>
    /// Implements the driver logic.
    /// <para>Реализует логику драйвера.</para>
    /// </summary>
    public class DrvFreeDiskSpaceJPLogic : DriverLogic
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DrvFreeDiskSpaceJPLogic(ICommContext commContext)
            : base(commContext)
        {
        }

        /// <summary>
        /// Gets the driver code.
        /// <para>Получает код драйвера.</para>
        /// </summary>
        public override string Code
        {
            get
            {
                return DriverUtils.DriverCode;
            }
        }

        /// <summary>
        /// Creates a new device.
        /// <para>Создает новое устройство.</para>
        /// </summary>
        public override DeviceLogic CreateDevice(ILineContext lineContext, DeviceConfig deviceConfig)
        {
            return new DevFreeDiskSpaceJPLogic(CommContext, lineContext, deviceConfig);
        }
    }
}