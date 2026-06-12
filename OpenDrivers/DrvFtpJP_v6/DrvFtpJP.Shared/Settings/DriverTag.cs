using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Represents a group of driver tags.
    /// <para>Представляет группу тегов драйвера.</para>
    /// </summary>
    public class GroupDriverTag
    {
        #region Variable
        private Guid groupTagID;                 // tag group ID
        private string groupTagName;             // tag group name
        private List<DriverTag> listTag;         // tag list
        #endregion Variable

        #region Property
        /// <summary>
        /// Gets or sets tag group ID.
        /// <para>Возвращает или задает ID группы тегов.</para>
        /// </summary>
        public Guid GroupTagID
        {
            get { return groupTagID; }
            set { groupTagID = value; }
        }

        /// <summary>
        /// Gets or sets tag group name.
        /// <para>Возвращает или задает имя группы тегов.</para>
        /// </summary>
        public string GroupTagName
        {
            get { return groupTagName; }
            set { groupTagName = value; }
        }

        /// <summary>
        /// Gets or sets tag list.
        /// <para>Возвращает или задает список тегов.</para>
        /// </summary>
        public List<DriverTag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }
        #endregion Property

        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public GroupDriverTag()
        {
            ListTag = new List<DriverTag>();
        }
        #endregion Basic
    }

    /// <summary>
    /// Represents a driver tag.
    /// <para>Представляет тег драйвера.</para>
    /// </summary>
    public class DriverTag
    {
        #region Variable
        private Guid tagID;                      // tag ID
        private string tagName;                  // tag name
        private string tagCode;                  // tag code
        private object tagFormat;                // tag format
        private bool tagEnabled;                 // tag enabled flag
        private object tagVal;                   // tag value
        private int tagStat;                     // tag status
        private DateTime tagDatetime;            // tag timestamp
        private int numberDecimalPlaces;         // decimal place count
        #endregion Variable

        #region Property
        /// <summary>
        /// Gets or sets tag ID.
        /// <para>Возвращает или задает ID тега.</para>
        /// </summary>
        public Guid TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        /// <summary>
        /// Gets or sets tag name.
        /// <para>Возвращает или задает имя тега.</para>
        /// </summary>
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        /// <summary>
        /// Gets or sets tag code.
        /// <para>Возвращает или задает код тега.</para>
        /// </summary>
        public string TagCode
        {
            get { return tagCode; }
            set { tagCode = value; }
        }

        /// <summary>
        /// Gets or sets tag format.
        /// <para>Возвращает или задает формат тега.</para>
        /// </summary>
        public object TagFormat
        {
            get { return tagFormat; }
            set { tagFormat = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tag is enabled.
        /// <para>Возвращает или задает признак включения тега.</para>
        /// </summary>
        public bool TagEnabled
        {
            get { return tagEnabled; }
            set { tagEnabled = value; }
        }

        /// <summary>
        /// Gets or sets tag value.
        /// <para>Возвращает или задает значение тега.</para>
        /// </summary>
        public object TagVal
        {
            get { return tagVal; }
            set { tagVal = value; }
        }

        /// <summary>
        /// Gets or sets tag status.
        /// <para>Возвращает или задает статус тега.</para>
        /// </summary>
        public int TagStat
        {
            get { return tagStat; }
            set { tagStat = value; }
        }

        /// <summary>
        /// Gets or sets tag timestamp.
        /// <para>Возвращает или задает время тега.</para>
        /// </summary>
        public DateTime TagDatetime
        {
            get { return tagDatetime; }
            set { tagDatetime = value; }
        }

        /// <summary>
        /// Gets or sets the number of decimal places.
        /// <para>Возвращает или задает количество знаков после запятой.</para>
        /// </summary>
        public int NumberDecimalPlaces
        {
            get { return numberDecimalPlaces; }
            set { numberDecimalPlaces = value; }
        }
        #endregion Property

        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverTag()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class with tag settings.
        /// <para>Инициализирует новый экземпляр класса с настройками тега.</para>
        /// </summary>
        /// <param name="tagName">Tag name.</param>
        /// <param name="tagCode">Tag code.</param>
        /// <param name="tagFormat">Tag format.</param>
        /// <param name="tagEnabled">Tag enabled flag.</param>
        public DriverTag(string tagName, string tagCode, object tagFormat, bool tagEnabled)
        {
            TagName = tagName;
            TagCode = tagCode;
            TagFormat = tagFormat;
            TagEnabled = tagEnabled;
        }

        /// <summary>
        /// Initializes a new instance of the class with tag settings and value.
        /// <para>Инициализирует новый экземпляр класса с настройками и значением тега.</para>
        /// </summary>
        /// <param name="tagName">Tag name.</param>
        /// <param name="tagCode">Tag code.</param>
        /// <param name="tagFormat">Tag format.</param>
        /// <param name="tagEnabled">Tag enabled flag.</param>
        /// <param name="tagVal">Tag value.</param>
        /// <param name="tagStat">Tag status.</param>
        public DriverTag(string tagName, string tagCode, object tagFormat, bool tagEnabled, object tagVal = null, int tagStat = 0)
        {
            TagName = tagName;
            TagCode = tagCode;
            TagFormat = tagFormat;
            TagEnabled = tagEnabled;
            TagVal = tagVal;
            TagStat = tagStat;
        }

        /// <summary>
        /// Loads the tag from the XML node.
        /// <para>Загружает тег из XML-узла.</para>
        /// </summary>
        /// <param name="xmlNode">XML node.</param>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            TagID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("ID"));
            TagName = xmlNode.GetChildAsString("Name");
            TagCode = xmlNode.GetChildAsString("Code");
            TagFormat = (FormatTag)xmlNode.GetChildAsInt("Format");
            NumberDecimalPlaces = xmlNode.GetChildAsInt("NumberDecimalPlaces");
            TagEnabled = xmlNode.GetChildAsBool("Enable");
        }

        /// <summary>
        /// Saves the tag into the XML node.
        /// <para>Сохраняет тег в XML-узел.</para>
        /// </summary>
        /// <param name="xmlElem">XML element.</param>
        public virtual void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException(nameof(xmlElem));
            }

            xmlElem.AppendElem("ID", TagID);
            xmlElem.AppendElem("Name", TagName);
            xmlElem.AppendElem("Code", TagCode);
            xmlElem.AppendElem("Format", (int)TagFormat);
            xmlElem.AppendElem("NumberDecimalPlaces", (int)NumberDecimalPlaces);
            xmlElem.AppendElem("Enable", TagEnabled);
        }
        #endregion Basic

        #region Support class
        /// <summary>
        /// Defines tag value formats.
        /// <para>Определяет форматы значений тегов.</para>
        /// </summary>
        public enum FormatTag : int
        {
            /// <summary>
            /// Floating-point number.
            /// <para>Число с плавающей точкой.</para>
            /// </summary>
            Float = 0,

            /// <summary>
            /// Date and time.
            /// <para>Дата и время.</para>
            /// </summary>
            DateTime = 1,

            /// <summary>
            /// String value.
            /// <para>Строковое значение.</para>
            /// </summary>
            String = 2,

            /// <summary>
            /// Integer value.
            /// <para>Целочисленное значение.</para>
            /// </summary>
            Integer = 3,

            /// <summary>
            /// Boolean value.
            /// <para>Логическое значение.</para>
            /// </summary>
            Boolean = 4
        }
        #endregion Support class
    }
}