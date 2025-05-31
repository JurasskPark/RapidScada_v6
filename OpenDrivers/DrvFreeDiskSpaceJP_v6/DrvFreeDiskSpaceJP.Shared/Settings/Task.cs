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
    /// Task settings.
    /// <para>Настройки задачи.</para>
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
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

        #region Variables
        /// <summary>
        /// Gets or sets the task identifier.
        /// <para>Возвращает или задает идентификатор задачи.</para>
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the task is enabled.
        /// <para>Возвращает или задает, что задача включена.</para>
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// <para>Возвращает или задает название.</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// <para>Возвращает или задает описание.</para>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the disk name.
        /// <para>Возвращает или задает название диска.</para>
        /// </summary>
        public string DiskName { get; set; }

        /// <summary>
        /// Gets or sets the percentage of free space.
        /// <para>Возвращает или задает процент свободного места.</para>
        /// </summary>
        public decimal ProceentFreeSpace { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// <para>Возвращает или задает путь.</para>
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the path to.
        /// <para>Возвращает или задает путь куда.</para>
        /// </summary>
        public string PathTo { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// <para>Возвращает или задает действие.</para>
        /// </summary>
        public ActionTask Action { get; set; }
        #endregion Variables

        #region Xml
        /// <summary>
        /// Loads the settings from the XML node.
        /// <para>Загружает настройки из XML-узла.</para>
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
        /// <para>Сохраняет настройки в XML-узле.</para>
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
        #endregion Xml
    }
}
