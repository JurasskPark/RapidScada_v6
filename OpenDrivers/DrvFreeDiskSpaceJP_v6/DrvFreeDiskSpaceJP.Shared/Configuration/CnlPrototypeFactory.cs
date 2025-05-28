// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Data.Const;
using Scada.Lang;
using System.Xml;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
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

            for (int i = 0; i < project.ListTask.Count(); i++)
            {
                Task task = project.ListTask[i];

                if (task.Enabled)
                {
                    string nameTagGroup = Locale.IsRussian ? @$"{task.Name} Теги" : @$"{task.Name} Tags";
                    CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);

                    group.AddCnlPrototype(@$"DriverName_{task.Name}", "Driver name").Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 64);                   // (256 / 4)
                    group.AddCnlPrototype(@$"DriverType_{task.Name}", "Driver type").Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);                    // (16 / 4)
                    group.AddCnlPrototype(@$"DriverVolumeLabel_{task.Name}", "Driver volume label").Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);     // (16 / 4)
                    group.AddCnlPrototype(@$"DriverTotalSize_{task.Name}", "Driver total size").Configure(cnl => cnl.DataTypeID = 1).Configure(cnl => cnl.DataLen = 1).SetFormat(FormatCode.N0);
                    group.AddCnlPrototype(@$"DriverTotalSizeString_{task.Name}", "Driver total size (string)").Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);
                    group.AddCnlPrototype(@$"DriverCurrentSize_{task.Name}", "Driver current size").Configure(cnl => cnl.DataTypeID = 1).Configure(cnl => cnl.DataLen = 1).SetFormat(FormatCode.N0);
                    group.AddCnlPrototype(@$"DriverCurrentSizeString_{task.Name}", "Driver current size (string)").Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);
                    group.AddCnlPrototype(@$"ProcentFreeSpaceSetPoint_{task.Name}", "Procent set point").SetFormat(FormatCode.N0);
                    group.AddCnlPrototype(@$"ProcentFreeSpaceCurrent_{task.Name}", "Procent current free space").SetFormat(FormatCode.N0);
                    group.AddCnlPrototype(@$"StatusAlarm_{task.Name}", "Alarm").SetFormat(FormatCode.OffOn);
                    group.AddCnlPrototype(@$"ActionTask_{task.Name}", "Action").Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 3);
                    group.AddCnlPrototype(@$"ActionDate_{task.Name}", "Date").SetFormat(FormatCode.DateTime);

                    groups.Add(group);
                }
            }

            return groups;      
        }

        public static List<DriverTag> GetDriverTags(Project project)
        {
            List<DriverTag> tags = new List<DriverTag>();

            for (int i = 0; i < project.ListTask.Count(); i++)
            {
                Task task = project.ListTask[i];

                if (task.Enabled)
                {
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverName_{task.Name}",                TagName = "Driver name",                    TagEnabled = true, TagAddress = "01", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 256    });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverType_{task.Name}",                TagName = "Driver type",                    TagEnabled = true, TagAddress = "02", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverVolumeLabel_{task.Name}",         TagName = "Driver volume label",            TagEnabled = true, TagAddress = "03", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverTotalSize_{task.Name}",           TagName = "Driver total size",              TagEnabled = true, TagAddress = "04", TagValueFormat = DriverTag.FormatData.Integer,        TagNumberDecimalPlaces = 0      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverTotalSizeString_{task.Name}",     TagName = "Driver total size (string)",     TagEnabled = true, TagAddress = "05", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverCurrentSize_{task.Name}",         TagName = "Driver current size",            TagEnabled = true, TagAddress = "06", TagValueFormat = DriverTag.FormatData.Integer,        TagNumberDecimalPlaces = 0      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverCurrentSizeString_{task.Name}",   TagName = "Driver current size (string)",   TagEnabled = true, TagAddress = "07", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"ProcentFreeSpaceSetPoint_{task.Name}",  TagName = "Procent set point",              TagEnabled = true, TagAddress = "08", TagValueFormat = DriverTag.FormatData.Float,          TagNumberDecimalPlaces = 0      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"ProcentFreeSpaceCurrent_{task.Name}",   TagName = "Procent current free space",     TagEnabled = true, TagAddress = "09", TagValueFormat = DriverTag.FormatData.Float,          TagNumberDecimalPlaces = 0      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"StatusAlarm_{task.Name}",               TagName = "Alarm",                          TagEnabled = true, TagAddress = "10", TagValueFormat = DriverTag.FormatData.Boolean,        TagNumberDecimalPlaces = 4      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"ActionTask_{task.Name}",                TagName = "Action",                         TagEnabled = true, TagAddress = "11", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 12     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"ActionDate_{task.Name}",                TagName = "ActionDate",                     TagEnabled = true, TagAddress = "12", TagValueFormat = DriverTag.FormatData.DateTime,       TagNumberDecimalPlaces = 4      });

                }
            }

            return tags;
        }
    }
}


