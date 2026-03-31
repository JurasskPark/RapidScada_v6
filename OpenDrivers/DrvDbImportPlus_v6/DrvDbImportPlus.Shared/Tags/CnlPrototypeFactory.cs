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
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(List<ImportCmd> importCmds, List<ExportCmd> exportCmds)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();

            string nameTagGroup = Locale.IsRussian ? "Теги" : "Tags";          
            CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);

            string nameTagGroupString = Locale.IsRussian ? "Строковые теги" : "String tags";
            CnlPrototypeGroup groupString = new CnlPrototypeGroup(nameTagGroupString);

            string nameTagGroupCommand = Locale.IsRussian ? "Командные теги" : "Command tags";
            CnlPrototypeGroup groupCommand = new CnlPrototypeGroup(nameTagGroupCommand);

            HashSet<string> usedTagCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (importCmds != null)
            {
                foreach (ImportCmd importCmd in importCmds)
                {
                    if (importCmd?.DeviceTags == null || !importCmd.Enabled)
                        continue;

                    foreach (DriverTag tag in importCmd.DeviceTags)
                    {
                        if (tag == null || !tag.Enabled || string.IsNullOrWhiteSpace(tag.Code))
                            continue;

                        if (!usedTagCodes.Add(tag.Code))
                            continue;

                        if (tag.Format == DriverTag.FormatTag.String)
                        {
                            int maxlen = Convert.ToInt32(Math.Ceiling((decimal)Math.Max(tag.NumberDecimalPlaces, 1) / 4m));
                            groupString.AddCnlPrototype(tag.Code, tag.Name)
                                .Configure(cnl => cnl.DataTypeID = 3)
                                .Configure(cnl => cnl.DataLen = maxlen);
                        }
                        else
                        {
                            group.AddCnlPrototype(tag.Code, tag.Name);
                        }
                    }
                }
            }

            HashSet<string> usedCmdCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (exportCmds != null)
            {
                foreach (ExportCmd exportCmd in exportCmds)
                {
                    if (exportCmd == null || !exportCmd.Enabled || string.IsNullOrWhiteSpace(exportCmd.CmdCode))
                        continue;

                    if (!usedCmdCodes.Add(exportCmd.CmdCode))
                        continue;

                    int maxlen = Convert.ToInt32(Math.Ceiling((decimal)Math.Max(exportCmd.Length, 1) / 4m));
                    groupCommand.AddCnlPrototype(exportCmd.CmdCode, exportCmd.Name)
                        .Configure(cnl => cnl.DataTypeID = 3)
                        .Configure(cnl => cnl.DataLen = maxlen);
                }
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


