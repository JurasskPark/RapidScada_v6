// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Lang;

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
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(List<Tag> deviceTags, List<ExportCmd> deviceCommands)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();

            string nameTagGroup = Locale.IsRussian ? "Теги" : "Tags";          
            CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);

            string nameTagGroupString = Locale.IsRussian ? "Строковые теги" : "String tags";
            CnlPrototypeGroup groupString = new CnlPrototypeGroup(nameTagGroupString);

            string nameTagGroupCommand = Locale.IsRussian ? "Командные теги" : "Command tags";
            CnlPrototypeGroup groupCommand = new CnlPrototypeGroup(nameTagGroupCommand);

            for (int i = 0; i < deviceTags.Count; i++)
            {
                if ((Tag.FormatTag)deviceTags[i].TagFormat == Tag.FormatTag.String)
                {
                    int maxlen = Convert.ToInt32(Math.Ceiling((decimal)deviceTags[i].NumberDecimalPlaces / (decimal)4));

                    groupString.AddCnlPrototype(deviceTags[i].TagCode, deviceTags[i].TagName).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = maxlen);
                }
                else
                {
                    group.AddCnlPrototype(deviceTags[i].TagCode, deviceTags[i].TagName);
                }           
            }

            for (int i = 0; i < deviceCommands.Count; i++)
            {
                int maxlen = Convert.ToInt32(Math.Ceiling((decimal)deviceCommands[i].Lenght / (decimal)4));

                groupCommand.AddCnlPrototype(deviceCommands[i].CmdCode, deviceCommands[i].Name).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = maxlen);
            }
            
            groups.Add(group);

            if(groupString != null && groupString.CnlPrototypes.Count > 0)
            {
                groups.Add(groupString);
            }

            if (groupCommand != null && groupCommand.CnlPrototypes.Count > 0)
            {
                groups.Add(groupCommand);
            }

            return groups;      
        }

    }
}


