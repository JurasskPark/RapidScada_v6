// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Lang;
using System.Xml;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Represents a device configuration.
    /// <para>Представляет конфигурацию драйвера.</para>
    /// </summary>
    public class DrvDbImportPlusProject : DeviceConfigBase
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvDbImportPlusProject()
        {
            SetToDefault();
        }

        /// <summary>
        /// Gets the DB connection settings.
        /// </summary>
        public DbConnSettings DbConnSettings { get; private set; }

        /// <summary>
        /// Gets the import commands.
        /// </summary>
        public List<ImportCmd> ImportCmds { get; set; }

        /// <summary>
        /// Gets the export commands.
        /// </summary>
        public List<ExportCmd> ExportCmds { get; set; }

        /// <summary>
        /// Gets the debuger settings.
        /// </summary>
        public DebugerSettings DebugerSettings { get; set; }


        /// <summary>
        /// Sets the default values.
        /// </summary>
        private void SetToDefault()
        {
            DbConnSettings = new DbConnSettings();     
            ImportCmds = new List<ImportCmd>();
            ExportCmds = new List<ExportCmd>();
            DebugerSettings = new DebugerSettings();
        }

        /// <summary>
        /// Loads the configuration from the specified file.
        /// </summary>
        public bool Load(string fileName, out string errMsg)
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
      
                try 
                { 
                    DbConnSettings.LoadFromXml(rootElem.SelectSingleNode("DbConnSettings")); 
                } 
                catch 
                { 
                    DbConnSettings = new DbConnSettings(); 
                }

                try
                {
                    if (rootElem.SelectSingleNode("ImportCmds") is XmlNode importCmdsNode)
                    {
                        foreach (XmlNode importCmdNode in importCmdsNode.SelectNodes("ImportCmd"))
                        {
                            ImportCmd importCmd = new ImportCmd();
                            importCmd.LoadFromXml(importCmdNode);
                            ImportCmds.Add(importCmd);
                        }
                    }
                }
                catch (Exception ex)
                { 
                    errMsg = ex.Message;
                    ImportCmds = new List<ImportCmd>(); 
                }

                try
                {
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
                }
                catch 
                { 
                    ExportCmds = new List<ExportCmd>(); 
                }

                try 
                { 
                    DebugerSettings.LoadFromXml(rootElem.SelectSingleNode("DebugerSettings")); 
                } 
                catch 
                { 
                    DebugerSettings = new DebugerSettings(); 
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
        public bool Save(string fileName, out string errMsg)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDecl);

                XmlElement rootElem = xmlDoc.CreateElement("DrvDbImportPlusProject");
                xmlDoc.AppendChild(rootElem);

                try 
                { 
                    DbConnSettings.SaveToXml(rootElem.AppendElem("DbConnSettings")); 
                } 
                catch { }

                try
                {
                    XmlElement importCmdsElem = rootElem.AppendElem("ImportCmds");
                    foreach (ImportCmd importCmd in ImportCmds)
                    {
                        importCmd.SaveToXml(importCmdsElem.AppendElem("ImportCmd"));
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
                } 
                catch { }

                try 
                { 
                    DebugerSettings.SaveToXml(rootElem.AppendElem("DebugerSettings")); 
                } 
                catch { }

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


    }
}
