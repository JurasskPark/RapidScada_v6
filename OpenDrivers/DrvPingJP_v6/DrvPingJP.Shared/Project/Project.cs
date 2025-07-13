using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Lang;
using System.Xml;

namespace Scada.Comm.Drivers.DrvPingJP
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
        /// Mode (Synchronous / Asynchronous)
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets tag names as a list.
        /// </summary>
        public List<DriverTag> DeviceTags { get; set; }

        /// <summary>
        /// Gets or sets the debuger settings.
        /// <para>Возвращает или задает настройки отладчика.</para>
        /// </summary>
        public DebugerSettings DebugerSettings { get; set; }

        /// <summary>
        /// Sets the default values.
        /// </summary>
#pragma warning disable CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        private void SetToDefault()
        #pragma warning restore CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        {
            DeviceTags = new List<DriverTag>();
            DebugerSettings = new DebugerSettings();
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
                    try { Mode = rootElem.GetChildAsInt("Mode"); } catch { Mode = 0; }
                    if (rootElem.SelectSingleNode("DeviceTags") is XmlNode exportDeviceTagsNode)
                    {
                        foreach (XmlNode exportDeviceTagNode in exportDeviceTagsNode.SelectNodes("Tag"))
                        {
                            DriverTag exportDeviceTag = new DriverTag();
                            exportDeviceTag.LoadFromXml(exportDeviceTagNode);
                            DeviceTags.Add(exportDeviceTag);
                        }
                        DeviceTags.Sort();
                    }

                    try { DebugerSettings.LoadFromXml(rootElem.SelectSingleNode("DebugerSettings")); } catch { DebugerSettings = new DebugerSettings(); }

                }
                catch {  }
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.LoadDriverConfigError + ":" + ex.Message;
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

                XmlElement rootElem = xmlDoc.CreateElement("Project");
                xmlDoc.AppendChild(rootElem);

                try { rootElem.AppendElem("Mode", Mode); } catch { }
                try
                {
                    XmlElement exportDeviceTagsElem = rootElem.AppendElem("DeviceTags");
                    foreach (DriverTag exportDeviceTag in DeviceTags)
                    {
                        exportDeviceTag.SaveToXml(exportDeviceTagsElem.AppendElem("Tag"));
                    }
                }
                catch { }

                try { DebugerSettings.SaveToXml(rootElem.AppendElem("DebugerSettings")); } catch { }

                xmlDoc.Save(fileName);
                errMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.SaveDriverConfigError + ":" + ex.Message;
                return false;
            }
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
