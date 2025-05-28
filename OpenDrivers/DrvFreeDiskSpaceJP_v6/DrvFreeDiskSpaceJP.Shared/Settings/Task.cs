using System.Xml;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    /// <summary>
    /// Task actions
    /// <para>Действия задачи</para>
    /// </summary>
    public enum ActionTask : int
    {
        None = 0,
        Delete = 1,
        CompressMove = 2,
    }

    /// <summary>
    /// Parser settings.
    /// <para>Настройки парсера.</para>
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Task()
        {
            ID = Guid.NewGuid();
            Enabled = false;
            Name = "";
            Description = "";
            DiskName = "";
            ProceentFreeSpace = 20;
            Path = "";
            PathTo = "";

        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the disk name.
        /// </summary>
        public string DiskName { get; set; }

        /// <summary>
        /// Gets or sets the percentage of free space.
        /// </summary>
        public decimal ProceentFreeSpace { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the path to.
        /// </summary>
        public string PathTo { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public ActionTask Action { get; set; } 

        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            ID = Guid.Parse(xmlNode.GetChildAsString("ID"));
            Enabled = xmlNode.GetChildAsBool("Enabled");
            Name = xmlNode.GetChildAsString("Name");
            Description = xmlNode.GetChildAsString("Description");
            DiskName = xmlNode.GetChildAsString("DiskName");
            ProceentFreeSpace = Convert.ToDecimal(DriverUtils.StringToDouble(xmlNode.GetChildAsString("ProceentFreeSpace")));
            Path = xmlNode.GetChildAsString("Path");
            Action = (ActionTask)Enum.Parse(typeof(ActionTask), xmlNode.GetChildAsString("Action"));
            PathTo = xmlNode.GetChildAsString("PathTo");
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

            xmlElem.AppendElem("ID", ID);
            xmlElem.AppendElem("Enabled", Enabled);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Description", Description);
            xmlElem.AppendElem("DiskName", DiskName);
            xmlElem.AppendElem("ProceentFreeSpace", DriverUtils.NullToString(ProceentFreeSpace));
            xmlElem.AppendElem("Path", Path);
            xmlElem.AppendElem("Action", Enum.GetName(typeof(ActionTask), Action));
            xmlElem.AppendElem("PathTo", PathTo);
        }
    }
}
