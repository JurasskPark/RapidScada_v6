// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Config;
using Scada.Data.Const;
using Scada.Lang;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Security.Principal;
using Scada.Data.Models;
using Scada.Comm.Drivers.DrvPing;
using Scada.Data.Entities;

namespace Scada.Comm.Drivers.DrvPing
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
                group.AddCnlPrototype(deviceTags[i].TagCode, deviceTags[i].TagName);
            }
        
            groups.Add(group);
            return groups;      
        }
    }
}


