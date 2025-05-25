using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    /// <summary>
    /// Logging settings.
    /// <para>Настройки логирования.</para>
    /// </summary>
    public class DebugerSettings
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DebugerSettings()
        {
            LogWrite = true;
            LogDays = 7;
        }

        /// <summary>
        /// Gets or sets a value log.
        /// </summary>
        public bool LogWrite { get; set; }

        /// <summary>
        /// Gets or sets how many days to write a log
        /// </summary>
        public int LogDays { get; set; }


        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
                throw new ArgumentNullException("xmlNode");

            LogWrite = xmlNode.GetChildAsBool("LogWrite");
            LogDays = xmlNode.GetChildAsInt("LogDays");
        }

        /// <summary>
        /// Saves the settings into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
                throw new ArgumentNullException("xmlElem");

            xmlElem.AppendElem("LogWrite", LogWrite);
            xmlElem.AppendElem("LogDays", LogDays);
        }
    }
}
