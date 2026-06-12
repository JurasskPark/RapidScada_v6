using System.Xml;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    /// <summary>
    /// Represents a group of driver tags.
    /// <para>Представляет группу тегов драйвера.</para>
    /// </summary>
    public class GroupTag
    {
        #region Variable

        private Guid groupTagID;                                  // tag group ID
        private string groupTagName = string.Empty;               // tag group name
        private List<Tag> listTag = new List<Tag>();              // tag list

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets the tag group ID.
        /// <para>Возвращает или задает идентификатор группы тегов.</para>
        /// </summary>
        public Guid GroupTagID
        {
            get => groupTagID;
            set => groupTagID = value;
        }

        /// <summary>
        /// Gets or sets the tag group name.
        /// <para>Возвращает или задает имя группы тегов.</para>
        /// </summary>
        public string GroupTagName
        {
            get => groupTagName;
            set => groupTagName = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the tag list.
        /// <para>Возвращает или задает список тегов.</para>
        /// </summary>
        public List<Tag> ListTag
        {
            get => listTag;
            set => listTag = value ?? new List<Tag>();
        }

        #endregion Property
    }

    /// <summary>
    /// Represents a TCP check tag.
    /// <para>Представляет тег проверки TCP-порта.</para>
    /// </summary>
    public class Tag : IComparable<Tag>
    {
        #region Variable

        private Guid tagID;                                       // tag ID
        private string tagCode = string.Empty;                    // tag code
        private string tagName = string.Empty;                    // tag name
        private string tagIPAddress = string.Empty;               // IP address or host name
        private int tagPort;                                      // TCP port
        private int tagTimeout;                                   // timeout
        private bool tagEnabled;                                  // enabled flag
        private int tagVal;                                       // tag value
        private int tagStat;                                      // tag status
        private DateTime tagDatetime;                             // tag date and time

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets the tag ID.
        /// <para>Возвращает или задает идентификатор тега.</para>
        /// </summary>
        public Guid TagID
        {
            get => tagID;
            set => tagID = value;
        }

        /// <summary>
        /// Gets or sets the tag code.
        /// <para>Возвращает или задает код тега.</para>
        /// </summary>
        public string TagCode
        {
            get => tagCode;
            set => tagCode = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the tag name.
        /// <para>Возвращает или задает имя тега.</para>
        /// </summary>
        public string TagName
        {
            get => tagName;
            set => tagName = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the IP address or host name.
        /// <para>Возвращает или задает IP-адрес или имя узла.</para>
        /// </summary>
        public string TagIPAddress
        {
            get => tagIPAddress;
            set => tagIPAddress = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the TCP port.
        /// <para>Возвращает или задает TCP-порт.</para>
        /// </summary>
        public int TagPort
        {
            get => tagPort;
            set => tagPort = value;
        }

        /// <summary>
        /// Gets or sets the timeout.
        /// <para>Возвращает или задает таймаут.</para>
        /// </summary>
        public int TagTimeout
        {
            get => tagTimeout;
            set => tagTimeout = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tag is enabled.
        /// <para>Возвращает или задает признак включения тега.</para>
        /// </summary>
        public bool TagEnabled
        {
            get => tagEnabled;
            set => tagEnabled = value;
        }

        /// <summary>
        /// Gets or sets the tag value.
        /// <para>Возвращает или задает значение тега.</para>
        /// </summary>
        public int TagVal
        {
            get => tagVal;
            set => tagVal = value;
        }

        /// <summary>
        /// Gets or sets the tag status.
        /// <para>Возвращает или задает статус тега.</para>
        /// </summary>
        public int TagStat
        {
            get => tagStat;
            set => tagStat = value;
        }

        /// <summary>
        /// Gets or sets the tag date and time.
        /// <para>Возвращает или задает дату и время тега.</para>
        /// </summary>
        public DateTime TagDatetime
        {
            get => tagDatetime;
            set => tagDatetime = value;
        }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public Tag()
        {
            TagID = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public Tag(string tagCode, string tagName, string tagIPAddress, int tagPort, int tagTimeout, bool tagEnabled)
            : this(tagCode, tagName, tagIPAddress, tagPort, tagTimeout, tagEnabled, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public Tag(string tagCode, string tagName, string tagIPAddress, int tagPort, int tagTimeout, bool tagEnabled, int tagVal = 0, int tagStat = 0)
            : this()
        {
            TagCode = tagCode;
            TagName = tagName;
            TagIPAddress = tagIPAddress;
            TagPort = tagPort;
            TagTimeout = tagTimeout;
            TagEnabled = tagEnabled;
            TagVal = tagVal;
            TagStat = tagStat;
        }

        /// <summary>
        /// Compares tags by name and code.
        /// <para>Сравнивает теги по имени и коду.</para>
        /// </summary>
        public int CompareTo(Tag other)
        {
            if (other == null)
            {
                return 1;
            }

            int nameCompare = string.Compare(TagName, other.TagName, StringComparison.OrdinalIgnoreCase);
            return nameCompare != 0
                ? nameCompare
                : string.Compare(TagCode, other.TagCode, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Loads the tag from the XML node.
        /// <para>Загружает тег из XML-узла.</para>
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            TagID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("ID"));
            if (TagID == Guid.Empty)
            {
                TagID = Guid.NewGuid();
            }

            TagName = xmlNode.GetChildAsString("Name");
            TagCode = xmlNode.GetChildAsString("Code");
            TagIPAddress = xmlNode.GetChildAsString("IPAddress");
            TagPort = xmlNode.GetChildAsInt("Port");
            TagTimeout = xmlNode.GetChildAsInt("Timeout");
            TagEnabled = xmlNode.GetChildAsBool("Enable");
        }

        /// <summary>
        /// Saves the tag into the XML node.
        /// <para>Сохраняет тег в XML-узел.</para>
        /// </summary>
        public virtual void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException(nameof(xmlElem));
            }

            xmlElem.AppendElem("ID", TagID);
            xmlElem.AppendElem("Name", TagName);
            xmlElem.AppendElem("Code", TagCode);
            xmlElem.AppendElem("IPAddress", TagIPAddress);
            xmlElem.AppendElem("Port", TagPort);
            xmlElem.AppendElem("Timeout", TagTimeout);
            xmlElem.AppendElem("Enable", TagEnabled);
        }

        #endregion Basic
    }
}
