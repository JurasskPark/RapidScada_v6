// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Lang;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Scada.Comm.Drivers.DrvDbImportPlus
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
            string nameTagGroupString = Locale.IsRussian ? "Строковые теги" : "String tags";
            CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);
            CnlPrototypeGroup groupString = new CnlPrototypeGroup(nameTagGroupString);

            for (int i = 0; i < deviceTags.Count; i++)
            {
                if ((Tag.FormatTag)deviceTags[i].TagFormat == Tag.FormatTag.String)
                {
                    int maxlen = Convert.ToInt32(Math.Ceiling((decimal)deviceTags[i].NumberDecimalPlaces / (decimal)4));

                    //groupString.AddCnlPrototype(deviceTags[i].TagCode, deviceTags[i].TagName);//.Configure(cnl => cnl.DataLen = maxlen);

                    for (int j = 0; j < maxlen; j++)
                    {
                        groupString.AddCnlPrototype(deviceTags[i].TagCode + @$"[{j}]", deviceTags[i].TagName + @$"[{j}]");
                    }
                }
                else
                {
                    group.AddCnlPrototype(deviceTags[i].TagCode, deviceTags[i].TagName);
                }           
            }
        
            groups.Add(group);
            groups.Add(groupString);
        
            return groups;      
        }

    }
}


