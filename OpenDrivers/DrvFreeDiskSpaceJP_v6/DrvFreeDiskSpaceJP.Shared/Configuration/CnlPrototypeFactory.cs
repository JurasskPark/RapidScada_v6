// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
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


            //foreach (ParserSettings settings in project.ListParsersSettings)
            //{
            //    string nameTagGroup = Locale.IsRussian ? @$"{settings.Name} Теги" : @$"{settings.Name} Tags";
            //    CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);

            //    string nameTagGroupString = Locale.IsRussian ? @$"{settings.Name} Строковые теги" : @$"{settings.Name} String tags";
            //    CnlPrototypeGroup groupString = new CnlPrototypeGroup(nameTagGroupString);


            //    if (settings.Enabled == true)
            //    {
            //        ParserTextGroupTag groupParserText = settings.Settings.GroupTag;

            //        for (int i = 0; i < groupParserText.Group.Count; i++)
            //        {
            //            if ((DriverTag.FormatData)groupParserText.Group[i].TagFormatData == DriverTag.FormatData.String)
            //            {
            //                int maxlen = Convert.ToInt32(Math.Ceiling((decimal)groupParserText.Group[i].TagNumberDecimalPlaces / (decimal)4));
            //                groupString.AddCnlPrototype(groupParserText.Group[i].TagCode, groupParserText.Group[i].TagName).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = maxlen);
            //            }
            //            else if((DriverTag.FormatData)groupParserText.Group[i].TagFormatData == DriverTag.FormatData.Table)
            //            {
            //                if((Tag.FormatTag)groupParserText.Group[i].ColumnNameValueFormat == Tag.FormatTag.String)
            //                {
            //                    int maxlen = Convert.ToInt32(Math.Ceiling((decimal)groupParserText.Group[i].ColumnNameValueNumberDecimalPlaces / (decimal)4));
            //                    groupString.AddCnlPrototype(groupParserText.Group[i].TagCode, groupParserText.Group[i].TagName).Configure(cnl => cnl.DataTypeID = 3).Configure(cnl => cnl.DataLen = maxlen);
            //                }
            //                else
            //                {
            //                    group.AddCnlPrototype(groupParserText.Group[i].TagCode, groupParserText.Group[i].TagName);
            //                }
            //            }
            //            else
            //            {
            //                group.AddCnlPrototype(groupParserText.Group[i].TagCode, groupParserText.Group[i].TagName);
            //            }
            //        }

            //        groups.Add(group);

            //        if (groupString != null && groupString.CnlPrototypes.Count > 0)
            //        {
            //            groups.Add(groupString);
            //        }

            //    }
            //}

            return groups;      
        }

        /// <summary>
        /// Gets the grouped channel prototypes.
        /// </summary>
        //public static List<DriverTag> GetDeviceTags(Project project)
        //{
        //    List<DriverTag> deviceTags = new List<DriverTag>();

        //    foreach (ParserSettings settings in project.ListParsersSettings)
        //    {
        //        if (settings.Enabled == true)
        //        {
        //            ParserTextGroupTag groupParserText = settings.Settings.GroupTag;

        //            for (int i = 0; i < groupParserText.Group.Count; i++)
        //            {
        //                if (groupParserText.Group[i].TagEnabled == true)
        //                {
        //                    if ((DriverTag.FormatData)groupParserText.Group[i].TagFormatData == DriverTag.FormatData.Table)
        //                    {
        //                        DriverTag parserTextTag = new DriverTag();
        //                        parserTextTag.TagName = groupParserText.Group[i].ColumnNameTag;
        //                        parserTextTag.TagFormatData = (DriverTag.FormatData)Enum.Parse(typeof(DriverTag.FormatData), groupParserText.Group[i].ColumnNameValueFormat.ToString());
        //                        parserTextTag.TagNumberDecimalPlaces = groupParserText.Group[i].ColumnNameValueNumberDecimalPlaces;
        //                    }
        //                    else
        //                    {
        //                        deviceTags.Add(groupParserText.Group[i]);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return deviceTags;
        //}
    }
}


