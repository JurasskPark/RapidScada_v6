﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDbImportPlusLogic.Logic;

namespace Scada.Comm.Drivers.DrvDbImportPlus.Logic
{

    /// <summary>
    /// Implements the driver logic.
    /// <para>Реализует логику драйвера.</para>
    /// </summary>
    public class DrvDbImportPlusLogic : DriverLogic
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvDbImportPlusLogic(ICommContext commContext)
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
                return "DrvDbImportPlus";
            }
        }

        /// <summary>
        /// Creates a new device.
        /// </summary>
        public override DeviceLogic CreateDevice(ILineContext lineContext, DeviceConfig deviceConfig)
        {
            return new DevDbImportPlusLogic(CommContext, lineContext, deviceConfig);
        }
    }
}