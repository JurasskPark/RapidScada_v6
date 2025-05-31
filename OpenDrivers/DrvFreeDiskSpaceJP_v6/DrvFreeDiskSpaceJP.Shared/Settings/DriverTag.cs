using System.Text;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    #region Class DriverGroupTag
    /// <summary>
    /// A group of driver tags.
    /// <para>Группа тегов драйвера.</para>
    /// </summary>
    public class DriverGroupTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverGroupTag()
        {
            Group = new List<DriverTag>();
        }

        #region Tag group
        public List<DriverTag> Group { get; set; }
        #endregion Tag group
    }
    #endregion Class DriverGroupTag

    #region Class DriverTag
    public class DriverTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverTag()
        {
            TagID = Guid.NewGuid();
            TagName = string.Empty;
            TagCode = string.Empty;
            TagAddress = string.Empty;
            TagDescription = string.Empty;
            TagEnabled = true;
            TagDataValue = new object();
            TagValueFormat = DriverTag.FormatData.Float;
            TagDateTime = DateTime.MinValue;
            TagNumberDecimalPlaces = 3;
        }

        #region Variables
        /// <summary>
        /// The tag identifier.
        /// <para>Идентификатор тега.</para>
        /// </summary>
        private Guid tagID;
        public Guid TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        /// <summary>
        /// The address of the tag.
        /// <para>Адрес тега.</para>
        /// </summary>
        private string tagAddress;
        public string TagAddress
        {
            get { return tagAddress; }
            set { tagAddress = value; }
        }

        /// <summary>
        /// The name of the tag.
        /// <para>Название тега.</para>
        /// </summary>
        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        /// <summary>
        /// The tag code.
        /// <para>Код тега.</para>
        /// </summary>
        private string tagCode;
        public string TagCode
        {
            get { return tagCode; }
            set { tagCode = value; }
        }

        /// <summary>
        /// Description of the tag.
        /// <para>Описание тега.</para>
        /// </summary>
        private string tagDescription;
        public string TagDescription
        {
            set { tagDescription = value; }
            get { return tagDescription; }
        }

        /// <summary>
        /// The tag is enabled.
        /// <para>Включен тег.</para>
        /// </summary>
        private bool tagEnabled;
        public bool TagEnabled
        {
            set { tagEnabled = value; }
            get { return tagEnabled; }
        }

        /// <summary>
        /// The tag value.
        /// <para>Значение тега.</para>
        /// </summary>
        private object tagDataValue;
        public object TagDataValue
        {
            set { tagDataValue = value; }
            get { return tagDataValue; }
        }

        /// <summary>
        /// The time when the data was received.
        /// <para>Время получения данных.</para>
        /// </summary>
        private DateTime tagDateTime;
        public DateTime TagDateTime
        {
            set { tagDateTime = value; }
            get { return tagDateTime; }
        }

        /// <summary>
        /// The data format.
        /// <para>Формат данных.</para>
        /// </summary>
        public enum FormatData
        {
            Float = 0,
            DateTime = 1,
            String = 2,
            Integer = 3,
            Boolean = 4,
        }

        /// <summary>
        /// The format of the tag data.
        /// <para>Формат данных тега.</para>
        /// </summary>
        private FormatData tagValueFormat;
        public FormatData TagValueFormat
        {
            set { tagValueFormat = value; }
            get { return tagValueFormat; }
        }

        /// <summary>
        /// The number of decimal places.
        /// <para>Количество знаков после запятой.</para>
        /// </summary>
        private int tagNumberDecimalPlaces;
        public int TagNumberDecimalPlaces
        {
            set { tagNumberDecimalPlaces = value; }
            get { return tagNumberDecimalPlaces; }
        }

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

            TagID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("TagID"));
            TagName = xmlNode.GetChildAsString("TagName");
            TagCode = xmlNode.GetChildAsString("TagCode");
            
            try
            {
                TagValueFormat = (FormatData)Enum.Parse(typeof(FormatData), xmlNode.GetChildAsString("TagValueFormat"));
            }
            catch { TagValueFormat = FormatData.Float; }

            TagNumberDecimalPlaces = xmlNode.GetChildAsInt("TagNumberDecimalPlaces");
            TagAddress = xmlNode.GetChildAsString("TagAddress");
            TagDescription = xmlNode.GetChildAsString("TagDescription");
            TagEnabled = xmlNode.GetChildAsBool("TagEnabled");
            TagDataValue = new object();
            TagDateTime = DateTime.MinValue;
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

            xmlElem.AppendElem("TagID", TagID.ToString());
            xmlElem.AppendElem("TagName", TagName);
            xmlElem.AppendElem("TagCode", TagCode);
            xmlElem.AppendElem("TagValueFormat", Enum.GetName(typeof(FormatData), TagValueFormat));
            xmlElem.AppendElem("TagNumberDecimalPlaces", TagNumberDecimalPlaces.ToString());
            xmlElem.AppendElem("TagAddress", TagAddress);
            xmlElem.AppendElem("TagDescription", TagDescription);
            xmlElem.AppendElem("TagEnabled", TagEnabled.ToString());
           
        }
        #endregion Xml

        #region Convert to string

        #region IsNullString
        /// <summary>
        /// Сonverting an object to a string, if the object is empty, it returns an empty string.
        /// <para>Преобразование объекта в строку, если объект пуст, возвращает пустую строку.</para>
        /// </summary>
        /// <param name="Value">object Value</param>
        /// <returns>string Value</returns>
        public static string TagToString(object Value, FormatData formatData = FormatData.String)
        {
            try
            {
                // check for null
                if (Value == null) return "";

                var type = Value.GetType();

                // check for invalid values in date times.
                if (type == typeof(DateTime))
                {
                    if (((DateTime)Value) == DateTime.MinValue)
                    {
                        return string.Empty;
                    }

                    var date = (DateTime)Value;

                    if (date.Millisecond > 0)
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                    }
                    else
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                // use only the local name for qualified names.
                if (type == typeof(XmlQualifiedName))
                {
                    return ((XmlQualifiedName)Value).Name;
                }

                // use only the name for system types.
                if (type.FullName == "System.RuntimeType")
                {
                    return ((Type)Value).FullName;
                }

                // treat byte arrays as a special case.
                if (type == typeof(byte[]))
                {
                    var bytes = (byte[])Value;

                    var buffer = new StringBuilder(bytes.Length * 3);

                    foreach (var character in bytes)
                    {
                        buffer.Append(character.ToString("X2"));
                        buffer.Append(".");
                    }

                    return buffer.ToString();
                }

                // show the element type and length for arrays.
                if (type.IsArray)
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)Value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"{type.GetElementType()?.Name}[{((Array)Value).Length}]{result}";
                }

                // instances of array are always treated as arrays of objects.
                if (type == typeof(Array))
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)Value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"Object[{((Array)Value).Length}]{result}";
                }

                // default behavior.
                return Value.ToString().Replace("\"", "");
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        #endregion IsNullString

        #endregion Convert to string

    }
    #endregion Class DriverTag
}
