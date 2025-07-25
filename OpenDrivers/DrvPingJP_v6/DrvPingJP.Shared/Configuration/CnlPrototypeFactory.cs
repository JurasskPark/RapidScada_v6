﻿using Scada.Comm.Devices;
using Scada.Data.Const;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvPingJP
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
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(List<DriverTag> deviceTags)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();
            string nameTagGroup = Locale.IsRussian ? "Теги" : "Tags";
            CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);

            for (int i = 0; i < deviceTags.Count; i++)
            {
                group.AddCnlPrototype(deviceTags[i].Code, deviceTags[i].Name).SetFormat(FormatCode.OffOn);
            }
        
            groups.Add(group);
            return groups;      
        }
    }
}


