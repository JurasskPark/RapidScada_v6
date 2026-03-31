using InfluxDB.Client.Api.Domain;
using System.Xml;
using System.Xml.Linq;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Projecturation of a data import command.
    /// <para>Конфигурация команды import данных.</para>
    /// </summary>
    public class ImportCmd : IComparable<ImportCmd>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ImportCmd()
        {
            Id = Guid.NewGuid();
            Enabled = true;
            CmdNum = 0;
            CmdCode = "DBIMPORTCODE";
            Name = "";
            Description = "";
            Query = "";
            IsColumnBased = true;
            DeviceTags = new List<DriverTag>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the command number.
        /// </summary>
        public int CmdNum { get; set; }

        /// <summary>
        /// Gets or sets the command code.
        /// </summary>
        public string CmdCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the SQL-query of the command.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the driver tags based on the list of requested table columns.
        /// </summary>
        public bool IsColumnBased { get; set; }

        /// <summary>
        /// Gets or sets tag names as a list.
        /// </summary>
        public List<DriverTag> DeviceTags { get; set; }

        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            Id = Guid.Parse(xmlNode.GetChildAsString("Id"));
            Enabled = xmlNode.GetChildAsBool("Enabled");
            CmdNum = xmlNode.GetChildAsInt("CmdNum");
            CmdCode = xmlNode.GetChildAsString("CmdCode");
            Name = xmlNode.GetChildAsString("Name");
            Description = xmlNode.GetChildAsString("Description");
            Query = xmlNode.GetChildAsString("Query");

            IsColumnBased = xmlNode.GetChildAsBool("IsColumnBased");

            if (xmlNode.SelectSingleNode("DeviceTags") is XmlNode importDeviceTagsNode)
            {
                foreach (XmlNode importDeviceTagNode in importDeviceTagsNode.SelectNodes("Tag"))
                {
                    DriverTag importDeviceTag = new DriverTag();
                    importDeviceTag.LoadFromXml(importDeviceTagNode);
                    DeviceTags.Add(importDeviceTag);
                }
            }
        }

        /// <summary>
        /// Saves the settings into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException("xmlElem");
            }

            xmlElem.AppendElem("Id", Id);
            xmlElem.AppendElem("Enabled", Enabled);
            xmlElem.AppendElem("CmdNum", CmdNum);
            xmlElem.AppendElem("CmdCode", CmdCode);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Description", Description);
            xmlElem.AppendElem("Query", Query);

            xmlElem.AppendElem("IsColumnBased", IsColumnBased); 

            XmlElement importDeviceTagsElem = xmlElem.AppendElem("DeviceTags");
            foreach (DriverTag importDeviceTag in DeviceTags)
            {
                importDeviceTag.SaveToXml(importDeviceTagsElem.AppendElem("Tag"));
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        public int CompareTo(ImportCmd other)
        {
            return CmdNum.CompareTo(other.CmdNum);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("[{0}] {1}", CmdNum, Name);
        }
    }
}
