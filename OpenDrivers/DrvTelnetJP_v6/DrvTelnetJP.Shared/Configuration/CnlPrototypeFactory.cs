﻿using Scada.Comm.Devices;
using Scada.Data.Const;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvTelnetJP
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
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(List<Tag> deviceTags)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();
            string nameTagGroup = Locale.IsRussian ? "Теги" : "Tags";
            CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);

            for (int i = 0; i < deviceTags.Count; i++)
            {
                bool tmpTagEnable = deviceTags[i].TagEnabled;

                group.AddCnlPrototype(deviceTags[i].TagCode, deviceTags[i].TagName).SetFormat(FormatCode.OffOn).Configure(cnl => cnl.Active = tmpTagEnable);
            }
        
            groups.Add(group);
            return groups;      
        }
    }
}


