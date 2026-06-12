// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Lang;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Represents a device configuration.
    /// <para>Представляет конфигурацию КП.</para>
    /// </summary>
    public class Project
    {
        #region Property

        /// <summary>
        /// Gets or sets FTP client settings.
        /// <para>Возвращает или задает настройки FTP-клиента.</para>
        /// </summary>
        public FtpClientSettings FtpClientSettings { get; set; }

        /// <summary>
        /// Gets or sets scenarios as a list.
        /// <para>Возвращает или задает сценарии в виде списка.</para>
        /// </summary>
        public List<Scenario> Scenarios { get; set; }

        /// <summary>
        /// Gets or sets device tags as a list.
        /// <para>Возвращает или задает теги КП в виде списка.</para>
        /// </summary>
        public List<DriverTag> DeviceTags { get; set; }

        /// <summary>
        /// Gets or sets debugger settings.
        /// <para>Возвращает или задает настройки отладчика.</para>
        /// </summary>
        public DebugerSettings DebugerSettings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Russian language is used.
        /// <para>Возвращает или задает признак использования русского языка.</para>
        /// </summary>
        public bool LanguageIsRussian { get; set; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public Project()
        {
            SetToDefault();
        }

        /// <summary>
        /// Loads the configuration from the specified file.
        /// <para>Загружает конфигурацию из указанного файла.</para>
        /// </summary>
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

                LoadFtpClientSettings(rootElem);
                LoadScenarios(rootElem);
                LoadDeviceTags(rootElem);
                LoadDebugerSettings(rootElem);
                LoadLanguage(rootElem);

                errMsg = string.Empty;
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
        /// <para>Сохраняет конфигурацию в указанный файл.</para>
        /// </summary>
        public bool Save(string fileName, out string errMsg)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDecl);

                XmlElement rootElem = xmlDoc.CreateElement("Project");
                xmlDoc.AppendChild(rootElem);

                SaveFtpClientSettings(rootElem);
                SaveScenarios(rootElem);
                SaveDeviceTags(rootElem);
                SaveDebugerSettings(rootElem);
                SaveLanguage(rootElem);

                xmlDoc.Save(fileName);
                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.SaveDriverConfigError + ":" + Environment.NewLine + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// <para>Возвращает краткое имя файла конфигурации КП.</para>
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return DeviceConfigBase.GetFileName(DriverUtils.DriverCode, deviceNum);
        }

        /// <summary>
        /// Sets the default values.
        /// <para>Устанавливает значения по умолчанию.</para>
        /// </summary>
        private void SetToDefault()
        {
            FtpClientSettings = new FtpClientSettings();
            Scenarios = new List<Scenario>();
            DeviceTags = new List<DriverTag>();
            DebugerSettings = new DebugerSettings();
            LanguageIsRussian = false;
        }

        #endregion Basic

        #region Load

        /// <summary>
        /// Loads FTP client settings.
        /// <para>Загружает настройки FTP-клиента.</para>
        /// </summary>
        private void LoadFtpClientSettings(XmlElement rootElem)
        {
            try
            {
                FtpClientSettings.LoadFromXml(rootElem.SelectSingleNode("FtpClientSettings"));
            }
            catch
            {
                FtpClientSettings = new FtpClientSettings();
            }
        }

        /// <summary>
        /// Loads scenarios.
        /// <para>Загружает сценарии.</para>
        /// </summary>
        private void LoadScenarios(XmlElement rootElem)
        {
            try
            {
                if (rootElem.SelectSingleNode("Scenarios") is XmlNode scenariosNode)
                {
                    foreach (XmlNode scenarioNode in scenariosNode.SelectNodes("Scenario"))
                    {
                        Scenario scenario = new Scenario();
                        scenario.LoadFromXml(scenarioNode);
                        Scenarios.Add(scenario);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Loads device tags.
        /// <para>Загружает теги КП.</para>
        /// </summary>
        private void LoadDeviceTags(XmlElement rootElem)
        {
            try
            {
                if (rootElem.SelectSingleNode("DeviceTags") is XmlNode deviceTagsNode)
                {
                    foreach (XmlNode deviceTagNode in deviceTagsNode.SelectNodes("Tag"))
                    {
                        DriverTag deviceTag = new DriverTag();
                        deviceTag.LoadFromXml(deviceTagNode);
                        DeviceTags.Add(deviceTag);
                    }

                    DeviceTags.Sort();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Loads debugger settings.
        /// <para>Загружает настройки отладчика.</para>
        /// </summary>
        private void LoadDebugerSettings(XmlElement rootElem)
        {
            try
            {
                DebugerSettings.LoadFromXml(rootElem.SelectSingleNode("DebugerSettings"));
            }
            catch
            {
                DebugerSettings = new DebugerSettings();
            }
        }

        /// <summary>
        /// Loads language settings.
        /// <para>Загружает настройки языка.</para>
        /// </summary>
        private void LoadLanguage(XmlElement rootElem)
        {
            try
            {
                LanguageIsRussian = rootElem.GetChildAsBool("LanguageIsRussian");
            }
            catch
            {
                LanguageIsRussian = false;
            }
        }

        #endregion Load

        #region Save

        /// <summary>
        /// Saves FTP client settings.
        /// <para>Сохраняет настройки FTP-клиента.</para>
        /// </summary>
        private void SaveFtpClientSettings(XmlElement rootElem)
        {
            try
            {
                FtpClientSettings.SaveToXml(rootElem.AppendElem("FtpClientSettings"));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves scenarios.
        /// <para>Сохраняет сценарии.</para>
        /// </summary>
        private void SaveScenarios(XmlElement rootElem)
        {
            try
            {
                XmlElement scenariosElem = rootElem.AppendElem("Scenarios");
                foreach (Scenario scenario in Scenarios)
                {
                    scenario.SaveToXml(scenariosElem.AppendElem("Scenario"));
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves device tags.
        /// <para>Сохраняет теги КП.</para>
        /// </summary>
        private void SaveDeviceTags(XmlElement rootElem)
        {
            try
            {
                XmlElement deviceTagsElem = rootElem.AppendElem("DeviceTags");
                foreach (DriverTag deviceTag in DeviceTags)
                {
                    deviceTag.SaveToXml(deviceTagsElem.AppendElem("Tag"));
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves debugger settings.
        /// <para>Сохраняет настройки отладчика.</para>
        /// </summary>
        private void SaveDebugerSettings(XmlElement rootElem)
        {
            try
            {
                DebugerSettings.SaveToXml(rootElem.AppendElem("DebugerSettings"));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves language settings.
        /// <para>Сохраняет настройки языка.</para>
        /// </summary>
        private void SaveLanguage(XmlElement rootElem)
        {
            try
            {
                rootElem.AppendElem("LanguageIsRussian", LanguageIsRussian);
            }
            catch
            {
            }
        }

        #endregion Save
    }
}
