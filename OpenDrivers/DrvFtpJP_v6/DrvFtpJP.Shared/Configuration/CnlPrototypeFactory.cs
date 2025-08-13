// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Lang;
using System.Xml;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Creates channel prototypes for a device.
    /// <para>Создает прототипы каналов устройства.</para>
    /// </summary>
    internal static class CnlPrototypeFactory
    {

        /// <summary>
        /// Gets the grouped channel prototypes.
        /// </summary>
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(Project project)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();
            return groups;      
        }

        /// <summary>
        /// Gets the grouped channel prototypes.
        /// </summary>
        public static List<DriverTag> GetDeviceTags(Project project)
        {
            List<DriverTag> deviceTags = new List<DriverTag>();
            return deviceTags;
        }
    }
}


