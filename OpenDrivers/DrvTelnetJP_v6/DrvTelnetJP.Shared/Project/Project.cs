using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Lang;
using System.Xml;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    /// <summary>
    /// Represents a device configuration.
    /// <para>Представляет конфигурацию КП.</para>
    /// </summary>
    internal class Project : DeviceConfigBase
    {
        #region Property

        /// <summary>
        /// Gets or sets a value indicating whether to write the driver log.
        /// <para>Возвращает или задает признак записи журнала драйвера.</para>
        /// </summary>
        public bool Log { get; set; }

        /// <summary>
        /// Gets or sets the polling mode.
        /// <para>Возвращает или задает режим опроса.</para>
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the device tags.
        /// <para>Возвращает или задает теги КП.</para>
        /// </summary>
        public List<Tag> DeviceTags { get; set; }

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
        /// Sets the default values.
        /// <para>Устанавливает значения по умолчанию.</para>
        /// </summary>
        private new void SetToDefault()
        {
            Log = false;
            Mode = 0;
            DeviceTags = new List<Tag>();
        }

        /// <summary>
        /// Loads the configuration from the specified file.
        /// <para>Загружает конфигурацию из указанного файла.</para>
        /// </summary>
        public new bool Load(string fileName, out string errMsg)
        {
            SetToDefault();

            try
            {
                if (!File.Exists(fileName))
                {
                    return Save(fileName, out errMsg);
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlElement rootElem = xmlDoc.DocumentElement;

                if (rootElem == null)
                {
                    throw new ScadaException(CommonPhrases.NamedFileNotFound, fileName);
                }

                Log = rootElem.GetChildAsBool("Log");
                Mode = rootElem.GetChildAsInt("Mode");
                LoadDeviceTags(rootElem);

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
        public new bool Save(string fileName, out string errMsg)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDecl);

                XmlElement rootElem = xmlDoc.CreateElement("Project");
                xmlDoc.AppendChild(rootElem);

                rootElem.AppendElem("Log", Log);
                rootElem.AppendElem("Mode", Mode);

                XmlElement deviceTagsElem = rootElem.AppendElem("DeviceTags");
                foreach (Tag deviceTag in DeviceTags)
                {
                    deviceTag.SaveToXml(deviceTagsElem.AppendElem("Tag"));
                }

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
        /// <para>Возвращает короткое имя файла конфигурации КП.</para>
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return DeviceConfigBase.GetFileName(DriverUtils.DriverCode, deviceNum);
        }

        /// <summary>
        /// Loads device tags from XML.
        /// <para>Загружает теги КП из XML.</para>
        /// </summary>
        private void LoadDeviceTags(XmlElement rootElem)
        {
            if (rootElem.SelectSingleNode("DeviceTags") is not XmlNode deviceTagsNode)
            {
                return;
            }

            foreach (XmlNode deviceTagNode in deviceTagsNode.SelectNodes("Tag"))
            {
                Tag deviceTag = new Tag();
                deviceTag.LoadFromXml(deviceTagNode);
                DeviceTags.Add(deviceTag);
            }

            DeviceTags.Sort();
        }

        #endregion Basic
    }
}
