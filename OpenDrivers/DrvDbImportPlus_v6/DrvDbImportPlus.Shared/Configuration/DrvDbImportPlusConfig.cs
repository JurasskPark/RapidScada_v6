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
using System.Xml.Linq;

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
        /// Gets or sets tag names as a list.
        /// </summary>
        public List<Tag> DeviceTags { get; set; }

        /// <summary>
        /// Gets the export commands.
        /// </summary>
        public List<ExportCmd> ExportCmds { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating KP Tags Based on the List of Requested Table Columns.
        /// </summary>
        public bool DeviceTagsBasedRequestedTableColumns { get; set; }

        /// <summary>
        /// Gets or sets a value Historical Data.
        /// </summary>
        public bool HistoricalData { get; set; }

        /// <summary>
        /// Gets or sets a value Discrepancy in Seconds.
        /// </summary>
        public int DiscrepancyInSeconds { get; set; }

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
            DeviceTags = new List<Tag>();
            DeviceTagsBasedRequestedTableColumns = true;
            DiscrepancyInSeconds = 0;
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

                try { DataSourceType = rootElem.GetChildAsEnum<DataSourceType>("DataSourceType"); } catch { DataSourceType = DataSourceType.Undefined; }
                try { DbConnSettings.LoadFromXml(rootElem.SelectSingleNode("DbConnSettings")); } catch { DbConnSettings = new DbConnSettings(); }
                try { SelectQuery = rootElem.GetChildAsString("SelectQuery"); } catch { SelectQuery = ""; }
                try { DeviceTagsBasedRequestedTableColumns = rootElem.GetChildAsBool("DeviceTagsBasedRequestedTableColumns"); } catch { DeviceTagsBasedRequestedTableColumns = true; }
                try
                {
                    if (rootElem.SelectSingleNode("DeviceTags") is XmlNode exportDeviceTagsNode)
                    {
                        foreach (XmlNode exportDeviceTagNode in exportDeviceTagsNode.SelectNodes("Tag"))
                        {
                            Tag exportDeviceTag = new Tag();
                            exportDeviceTag.LoadFromXml(exportDeviceTagNode);
                            DeviceTags.Add(exportDeviceTag);
                        }
                        DeviceTags.Sort();
                    }
                }
                catch {  }
                try {
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
                } catch { ExportCmds = new List<ExportCmd>(); }
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

                try { rootElem.AppendElem("DataSourceType", DataSourceType); } catch { }
                try { DbConnSettings.SaveToXml(rootElem.AppendElem("DbConnSettings")); } catch { }
                try { rootElem.AppendElem("SelectQuery", SelectQuery); } catch { }
                try { rootElem.AppendElem("DeviceTagsBasedRequestedTableColumns", DeviceTagsBasedRequestedTableColumns); } catch { }
                try
                {
                    XmlElement exportDeviceTagsElem = rootElem.AppendElem("DeviceTags");
                    foreach (Tag exportDeviceTag in DeviceTags)
                    {
                        exportDeviceTag.SaveToXml(exportDeviceTagsElem.AppendElem("Tag"));
                    }
                }
                catch { }
                try
                {
                    XmlElement exportCmdsElem = rootElem.AppendElem("ExportCmds");
                    foreach (ExportCmd exportCmd in ExportCmds)
                    {
                        exportCmd.SaveToXml(exportCmdsElem.AppendElem("ExportCmd"));
                    }
                } catch { }

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
