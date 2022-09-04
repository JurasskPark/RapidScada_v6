// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Scada.Comm.Devices;
using System.IO;
using System.Xml;
using Scada.Lang;
using Scada.Comm.Lang;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Represents a device configuration.
    /// <para>Представляет конфигурацию КП.</para>
    /// </summary>
    internal class DrvDbImportPlusConfig : DeviceConfigBase
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvDbImportPlusConfig()
        {
            SetToDefault();
        }

        /// <summary>
        /// Gets or sets the data source type.
        /// </summary>
        public DataSourceType DataSourceType { get; set; }

        /// <summary>
        /// Gets the DB connection settings.
        /// </summary>
        public DbConnSettings DbConnSettings { get; private set; }

        /// <summary>
        /// Gets or sets the SQL-query to retrieve data.
        /// </summary>
        public string SelectQuery { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to calculate tag count automatically by parsing the query.
        /// </summary>
        public bool AutoTagCount { get; set; }

        /// <summary>
        /// Gets or sets the exact number of tags.
        /// </summary>
        public int TagCount { get; set; }

        /// <summary>
        /// Gets the export commands.
        /// </summary>
        public List<ExportCmd> ExportCmds { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating KP Tags Based on the List of Requested Table Columns.
        /// </summary>
        public bool DeviceTagsBasedRequestedTableColumns { get; set; }


        /// <summary>
        /// Sets the default values.
        /// </summary>
        #pragma warning disable CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        private void SetToDefault()
        #pragma warning restore CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        {
            DataSourceType = DataSourceType.Undefined;
            DbConnSettings = new DbConnSettings();
            SelectQuery = "";
            AutoTagCount = true;
            TagCount = 0;
            DeviceTagsBasedRequestedTableColumns = true;
            ExportCmds = new List<ExportCmd>();
        }


        /// <summary>
        /// Loads the configuration from the specified file.
        /// </summary>
        #pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        public bool Load(string fileName, out string errMsg)
        #pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        {
            SetToDefault();

            try
            {
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException(string.Format(CommonPhrases.NamedFileNotFound, fileName));
                }
                    

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlElement rootElem = xmlDoc.DocumentElement;

                DataSourceType = rootElem.GetChildAsEnum<DataSourceType>("DataSourceType");
                DbConnSettings.LoadFromXml(rootElem.SelectSingleNode("DbConnSettings"));
                SelectQuery = rootElem.GetChildAsString("SelectQuery");
                AutoTagCount = rootElem.GetChildAsBool("AutoTagCount");
                TagCount = rootElem.GetChildAsInt("TagCount");
                DeviceTagsBasedRequestedTableColumns = rootElem.GetChildAsBool("DeviceTagsBasedRequestedTableColumns");

                if (rootElem.SelectSingleNode("ExportCmds") is XmlNode exportCmdsNode)
                {
                    foreach (XmlNode exportCmdNode in exportCmdsNode.SelectNodes("ExportCmd"))
                    {
                        ExportCmd exportCmd = new ExportCmd();
                        exportCmd.LoadFromXml(exportCmdNode);
                        ExportCmds.Add(exportCmd);
                    }

                    ExportCmds.Sort();
                }

                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.LoadDriverConfigError + ":" + Environment.NewLine + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Saves the configuration to the specified file.
        /// </summary>
        #pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        public bool Save(string fileName, out string errMsg)
        #pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDecl);

                XmlElement rootElem = xmlDoc.CreateElement("DrvDbImportPlusConfig");
                xmlDoc.AppendChild(rootElem);

                rootElem.AppendElem("DataSourceType", DataSourceType);
                DbConnSettings.SaveToXml(rootElem.AppendElem("DbConnSettings"));
                rootElem.AppendElem("SelectQuery", SelectQuery);
                rootElem.AppendElem("AutoTagCount", AutoTagCount);
                rootElem.AppendElem("TagCount", TagCount);

                rootElem.AppendElem("DeviceTagsBasedRequestedTableColumns", DeviceTagsBasedRequestedTableColumns);

                XmlElement exportCmdsElem = rootElem.AppendElem("ExportCmds");
                foreach (ExportCmd exportCmd in ExportCmds)
                {
                    exportCmd.SaveToXml(exportCmdsElem.AppendElem("ExportCmd"));
                }

                xmlDoc.Save(fileName);
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.SaveDriverConfigError + ":" + Environment.NewLine + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Calculates tag count based on the SQL-query.
        /// </summary>
        public int CalcTagCount()
        {
            int tagCount = 0;

            if (!string.IsNullOrEmpty(SelectQuery))
            {
                if (DeviceTagsBasedRequestedTableColumns)
                {
                    // count the number of words between select and from separated by commas
                    int selectInd = SelectQuery.IndexOf("select", StringComparison.OrdinalIgnoreCase);
                    int fromInd = SelectQuery.IndexOf("from", StringComparison.OrdinalIgnoreCase);

                    if (selectInd >= 0)
                    {
                        if (fromInd < 0)
                            fromInd = SelectQuery.Length - 1;

                        for (int i = selectInd + "select".Length; i < fromInd; i++)
                        {
                            if (SelectQuery[i] == ',')
                                tagCount++;
                        }

                        tagCount++;
                    }
                }
            }

            return tagCount;
        }

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return DeviceConfigBase.GetFileName(DriverUtils.DriverCode, deviceNum);
        }
    }
}
