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
        /// <para>Возвращает сгруппированные прототипы каналов.</para>
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

                    string tagNameDriverName = Locale.IsRussian ? @$"Название носителя" : @$"Driver name";
                    string tagNameDriverType = Locale.IsRussian ? @$"Тип носителя" : @$"Driver type";
                    string tagNameDriverVolumeLabel = Locale.IsRussian ? @$"Метка тома носителя" : @$"Driver volume label";
                    string tagNameDriverTotalSize = Locale.IsRussian ? @$"Размер носителя" : @$"Driver total size";
                    string tagNameDriverTotalSizeString = Locale.IsRussian ? @$"Размер носителя (строка)" : @$"Driver total size (string)";
                    string tagNameDriverCurrentSize = Locale.IsRussian ? @$"Текущий размер носителя" : @$"Driver current size";
                    string tagNameDriverCurrentSizeString = Locale.IsRussian ? @$"Текущий размер носителя (строка)" : @$"Driver current size (string)";
                    string tagNamePercentFreeSpaceSetPoint = Locale.IsRussian ? @$"Заданное значение в процентах" : @$"Percent set point";
                    string tagNamePercentFreeSpaceCurrent = Locale.IsRussian ? @$"Текущее значение в процентах" : @$"Percent current free space";
                    string tagNameStatusAlarm = Locale.IsRussian ? @$"Авария (статус)" : @$"Alarm (status)";
                    string tagNameActionTask = Locale.IsRussian ? @$"Действие" : @$"Action";
                    string tagNameActionDate = Locale.IsRussian ? @$"Дата" : @$"Date";

                    group.AddCnlPrototype(@$"DriverName_{task.Name}", tagNameDriverName).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 64);                   // (256 / 4)
                    group.AddCnlPrototype(@$"DriverType_{task.Name}", tagNameDriverType).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);                    // (16 / 4)
                    group.AddCnlPrototype(@$"DriverVolumeLabel_{task.Name}", tagNameDriverVolumeLabel).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);     // (16 / 4)
                    group.AddCnlPrototype(@$"DriverTotalSize_{task.Name}", tagNameDriverTotalSize);
                    group.AddCnlPrototype(@$"DriverTotalSizeString_{task.Name}", tagNameDriverTotalSizeString).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);
                    group.AddCnlPrototype(@$"DriverCurrentSize_{task.Name}", tagNameDriverCurrentSize);
                    group.AddCnlPrototype(@$"DriverCurrentSizeString_{task.Name}", tagNameDriverCurrentSizeString).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 4);
                    group.AddCnlPrototype(@$"PercentFreeSpaceSetPoint_{task.Name}", tagNamePercentFreeSpaceSetPoint).SetFormat(FormatCode.N0);
                    group.AddCnlPrototype(@$"PercentFreeSpaceCurrent_{task.Name}", tagNamePercentFreeSpaceCurrent).SetFormat(FormatCode.N0);
                    group.AddCnlPrototype(@$"StatusAlarm_{task.Name}", tagNameStatusAlarm).SetFormat(FormatCode.OffOn);
                    group.AddCnlPrototype(@$"ActionTask_{task.Name}", tagNameActionTask).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = 5);
                    group.AddCnlPrototype(@$"ActionDate_{task.Name}", tagNameActionDate).SetFormat(FormatCode.DateTime);

                    groups.Add(group);
                }
            }

            return groups;      
        }

        /// <summary>
        /// Get a list of tags from the driver.
        /// <para>Получить список тегов из драйвера.</para>>
        /// </summary>
        public static List<DriverTag> GetDriverTags(Project project)
        {
            List<DriverTag> tags = new List<DriverTag>();

            for (int i = 0; i < project.ListTask.Count(); i++)
            {
                Task task = project.ListTask[i];

                if (task.Enabled)
                {
                    string tagNameDriverName = Locale.IsRussian ? @$"Название носителя" : @$"Driver name";
                    string tagNameDriverType = Locale.IsRussian ? @$"Тип носителя" : @$"Driver type";
                    string tagNameDriverVolumeLabel = Locale.IsRussian ? @$"Метка тома носителя" : @$"Driver volume label";
                    string tagNameDriverTotalSize = Locale.IsRussian ? @$"Размер носителя" : @$"Driver total size";
                    string tagNameDriverTotalSizeString = Locale.IsRussian ? @$"Размер носителя (строка)" : @$"Driver total size (string)";
                    string tagNameDriverCurrentSize = Locale.IsRussian ? @$"Текущий размер носителя" : @$"Driver current size";
                    string tagNameDriverCurrentSizeString = Locale.IsRussian ? @$"Текущий размер носителя (строка)" : @$"Driver current size (string)";
                    string tagNamePercentFreeSpaceSetPoint = Locale.IsRussian ? @$"Заданное значение в процентах" : @$"Percent set point";
                    string tagNamePercentFreeSpaceCurrent = Locale.IsRussian ? @$"Текущее значение в процентах" : @$"Percent current free space";
                    string tagNameStatusAlarm = Locale.IsRussian ? @$"Авария (статус)" : @$"Alarm (status)";
                    string tagNameActionTask = Locale.IsRussian ? @$"Действие" : @$"Action";
                    string tagNameActionDate = Locale.IsRussian ? @$"Дата" : @$"Date";

                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverName_{task.Name}",                TagName = tagNameDriverName,                    TagEnabled = true, TagAddress = "01", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 256    });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverType_{task.Name}",                TagName = tagNameDriverType,                    TagEnabled = true, TagAddress = "02", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverVolumeLabel_{task.Name}",         TagName = tagNameDriverVolumeLabel,             TagEnabled = true, TagAddress = "03", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverTotalSize_{task.Name}",           TagName = tagNameDriverTotalSize,               TagEnabled = true, TagAddress = "04", TagValueFormat = DriverTag.FormatData.Integer,        TagNumberDecimalPlaces = 0      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverTotalSizeString_{task.Name}",     TagName = tagNameDriverTotalSizeString,         TagEnabled = true, TagAddress = "05", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverCurrentSize_{task.Name}",         TagName = tagNameDriverCurrentSize,             TagEnabled = true, TagAddress = "06", TagValueFormat = DriverTag.FormatData.Integer,        TagNumberDecimalPlaces = 0      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"DriverCurrentSizeString_{task.Name}",   TagName = tagNameDriverCurrentSizeString,       TagEnabled = true, TagAddress = "07", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 16     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"PercentFreeSpaceSetPoint_{task.Name}",  TagName = tagNamePercentFreeSpaceSetPoint,      TagEnabled = true, TagAddress = "08", TagValueFormat = DriverTag.FormatData.Float,          TagNumberDecimalPlaces = 5      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"PercentFreeSpaceCurrent_{task.Name}",   TagName = tagNamePercentFreeSpaceCurrent,       TagEnabled = true, TagAddress = "09", TagValueFormat = DriverTag.FormatData.Float,          TagNumberDecimalPlaces = 5      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"StatusAlarm_{task.Name}",               TagName = tagNameStatusAlarm,                   TagEnabled = true, TagAddress = "10", TagValueFormat = DriverTag.FormatData.Boolean,        TagNumberDecimalPlaces = 4      });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"ActionTask_{task.Name}",                TagName = tagNameActionTask,                    TagEnabled = true, TagAddress = "11", TagValueFormat = DriverTag.FormatData.String,         TagNumberDecimalPlaces = 20     });
                    tags.Add(new DriverTag() { TagID = Guid.NewGuid(), TagCode = @$"ActionDate_{task.Name}",                TagName = tagNameActionDate,                    TagEnabled = true, TagAddress = "12", TagValueFormat = DriverTag.FormatData.DateTime,       TagNumberDecimalPlaces = 4      });

                }
            }

            return tags;
        }
    }
}


