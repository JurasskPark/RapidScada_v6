// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Lang;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    /// <summary>
    /// Represents a device configuration.
    /// <para>Представляет конфигурацию КП.</para>
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Project()
        {
            SetToDefault();
        }

        /// <summary>
        /// Gets or sets tag names as a list.
        /// </summary>
        public List<Tag> DeviceTags { get; set; }

        /// <summary>
        /// Gets the debuger settings.
        /// </summary>
        public DebugerSettings DebugerSettings { get; set; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        public bool LanguageIsRussian { get; set; }


        /// <summary>
        /// Sets the default values.
        /// </summary>
#pragma warning disable CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        private void SetToDefault()
    
        {
            DeviceTags = new List<Tag>();
            DebugerSettings = new DebugerSettings();
            LanguageIsRussian = false;
        }
#pragma warning restore CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения

        /// <summary>
        /// Loads the configuration from the specified file.
        /// </summary>
#pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        public bool Load(string fileName, out string errMsg)
      
        {
            SetToDefault();

            try
            {
                if (!File.Exists(fileName))
                {
                    try
                    {
                        Save(fileName, out errMsg);
                    }
                    catch
                    {
                        throw new FileNotFoundException(string.Format(CommonPhrases.NamedFileNotFound, fileName));
                    }
                }


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlElement rootElem = xmlDoc.DocumentElement;

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

                try { DebugerSettings.LoadFromXml(rootElem.SelectSingleNode("DebugerSettings")); } catch { DebugerSettings = new DebugerSettings(); }

                try { LanguageIsRussian = rootElem.GetChildAsBool("LanguageIsRussian"); } catch { LanguageIsRussian = false; }

                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.LoadDriverConfigError + ":" + Environment.NewLine + ex.Message;
                return false;
            }
        }
#pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        /// <summary>
        /// Saves the configuration to the specified file.
        /// </summary>
#pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        public bool Save(string fileName, out string errMsg)
       
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDecl);

                XmlElement rootElem = xmlDoc.CreateElement("Project");
                xmlDoc.AppendChild(rootElem);

                try
                {
                    XmlElement exportDeviceTagsElem = rootElem.AppendElem("DeviceTags");
                    foreach (Tag exportDeviceTag in DeviceTags)
                    {
                        exportDeviceTag.SaveToXml(exportDeviceTagsElem.AppendElem("Tag"));
                    }
                }
                catch { }

                try { DebugerSettings.SaveToXml(rootElem.AppendElem("DebugerSettings")); } catch { }

                try { rootElem.AppendElem("LanguageIsRussian", LanguageIsRussian); } catch { }

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
#pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        /// <summary>
        /// Gets the short name of the device configuration file.
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return DeviceConfigBase.GetFileName(DriverUtils.DriverCode, deviceNum);
        }
    }
}
