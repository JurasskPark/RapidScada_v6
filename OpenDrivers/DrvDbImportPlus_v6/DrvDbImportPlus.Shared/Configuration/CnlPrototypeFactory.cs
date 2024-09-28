// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Data.Const;
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
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(List<Tag> deviceTags)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();
            string nameTagGroup = Locale.IsRussian ? "Теги" : "Tags";
            string nameTagGroupString = Locale.IsRussian ? "Строковые теги" : "String tags";
            CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);
            CnlPrototypeGroup groupString = new CnlPrototypeGroup(nameTagGroupString);

            for (int i = 0; i < deviceTags.Count; i++)
            {
                bool tmpTagEnable = deviceTags[i].TagEnabled;
                string tmpTagFormat = string.Empty;
                int tmpTagDataTypeId = 0;
                switch (deviceTags[i].TagFormat.ToString())
                {
                    case "Float":
                        tmpTagFormat = "N" + deviceTags[i].NumberDecimalPlaces;
                        tmpTagDataTypeId = 0;
                        break;
                    case "DateTime":
                        tmpTagFormat = FormatCode.DateTime;
                        tmpTagDataTypeId = 0;
                        break;
                    case "String":
                        tmpTagFormat = FormatCode.String;
                        tmpTagDataTypeId = 3;
                        break;
                    case "Integer":
                        tmpTagFormat = FormatCode.N0;
                        tmpTagDataTypeId = 0;
                        break;
                    case "Boolean":
                        tmpTagFormat = FormatCode.OffOn;
                        tmpTagDataTypeId = 0;
                        break;
                }

                if ((Tag.FormatTag)deviceTags[i].TagFormat == Tag.FormatTag.String)
                {
                    int maxlen = Convert.ToInt32(Math.Ceiling((decimal)deviceTags[i].NumberDecimalPlaces / (decimal)4));

                    for (int j = 0; j < maxlen; j++)
                    {
                        groupString.AddCnlPrototype(deviceTags[i].TagCode + @$"[{j}]", deviceTags[i].TagName + @$"[{j}]").Configure(cnl => cnl.DataTypeID = tmpTagDataTypeId).Configure(cnl => cnl.DataLen = 1).Configure(cnl => cnl.Active = tmpTagEnable);
                    }
                }
                else
                {
                    group.AddCnlPrototype(deviceTags[i].TagCode, deviceTags[i].TagName).Configure(cnl => cnl.DataTypeID = tmpTagDataTypeId).Configure(cnl => cnl.SetFormat(tmpTagFormat)).Configure(cnl => cnl.DataLen = 1).Configure(cnl => cnl.Active = tmpTagEnable);
                }           
            }
        
            groups.Add(group);
            groups.Add(groupString);
        
            return groups;      
        }

    }
}


