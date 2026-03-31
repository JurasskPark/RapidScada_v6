using System.Xml;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// A group of tags.
    /// <para>Группа тегов.</para>
    /// </summary>
    public class GroupDriverTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public GroupDriverTag()
        {
            Id = Guid.NewGuid();
            Name = "";
            ListTag = new List<DriverTag>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list tag.
        /// </summary>
        public List<DriverTag> ListTag { get; set; }

    }

    /// <summary>
    /// Tag.
    /// <para>Тег.</para>
    /// </summary>
    public class DriverTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverTag()
        {
            Id = Guid.NewGuid();
            Name = "";
            Code = "";
            Format = FormatTag.Float;
            Enabled = true;
            Val = new object();
            Stat = 0;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the enum format tag.
        /// </summary>
        public enum FormatTag
        {
            Float = 0,
            DateTime = 1,
            String = 2,
            Integer = 3,
            Boolean = 4
        }

        /// <summary>
        /// Gets or sets the format tag.
        /// </summary>
        public FormatTag Format { get; set; }

        /// <summary>
        /// Gets or sets the enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the val.
        /// </summary>
        public object Val { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Stat { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the number decimal places.
        /// </summary>
        public int NumberDecimalPlaces { get; set; }
        
        /// <summary>
        /// Loads the command from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            Id = DriverUtils.StringToGuid(xmlNode.GetChildAsString("ID"));
            Name = xmlNode.GetChildAsString("Name");
            Code = xmlNode.GetChildAsString("Code");
            Format = (FormatTag)xmlNode.GetChildAsInt("Format");
            NumberDecimalPlaces = xmlNode.GetChildAsInt("NumberDecimalPlaces");
            Enabled = xmlNode.GetChildAsBool("Enable");
        }

        /// <summary>
        /// Saves the configuration into the XML node.
        /// </summary>
        public virtual void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException(nameof(xmlElem));
            }

            xmlElem.AppendElem("ID", Id);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Code", Code);
            xmlElem.AppendElem("Format", (int)Format);
            xmlElem.AppendElem("NumberDecimalPlaces", (int)NumberDecimalPlaces);
            xmlElem.AppendElem("Enable", Enabled);
        }

    }
}
