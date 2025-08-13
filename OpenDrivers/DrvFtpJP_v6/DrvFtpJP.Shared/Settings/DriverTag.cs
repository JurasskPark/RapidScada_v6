using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{

    public class GroupDriverTag
    {
        public GroupDriverTag()
        {
            ListTag = new List<DriverTag>();
        }

        #region Tag Group

        //ID of the tag group
        private Guid groupTagID;
        public Guid GroupTagID
        {
            get { return groupTagID; }
            set { groupTagID = value; }
        }

        //Tag group name
        private string groupTagName;
        public string GroupTagName
        {
            get { return groupTagName; }
            set { groupTagName = value; }
        }

        #endregion Tag Group

        #region List of tags

        private List<DriverTag> listTag;
        public List<DriverTag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }

        #endregion List of tags
    }

    public class DriverTag
    {
        public DriverTag()
        {

        }

        public DriverTag(string tagName, string tagCode, object tagFormat, bool tagEnabled)
        {
            this.TagName = tagName;
            this.TagCode = tagCode;       
            this.TagFormat = tagFormat;
            this.TagEnabled = tagEnabled;
        }

        public DriverTag(string tagName, string tagCode, object tagFormat, bool tagEnabled, object tagVal = null, int tagStat = 0)
        {
            this.TagName = tagName;
            this.TagCode = tagCode;
            this.TagFormat = tagFormat;
            this.TagEnabled = tagEnabled;
            this.TagVal = tagVal;
            this.TagStat = tagStat;
        }

        #region Tag

        private Guid tagID;
        public Guid TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }

        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        private string tagCode;
        public string TagCode
        {
            get { return tagCode; }
            set { tagCode = value; }
        }

        private object tagFormat;
        public object TagFormat
        {
            set { tagFormat = value; }
            get { return tagFormat; }
        }

        private bool tagEnabled;
        public bool TagEnabled
        {
            set { tagEnabled = value; }
            get { return tagEnabled; }
        }

        private object tagVal;
        public object TagVal
        {
            set { tagVal = value; }
            get { return tagVal; }
        }

        private int tagStat;
        public int TagStat
        {
            set { tagStat = value; }
            get { return tagStat; }
        }

        private DateTime tagDatetime;
        public DateTime TagDatetime
        {
            set { tagDatetime = value; }
            get { return tagDatetime; }
        }

        public enum FormatTag : int
        {
            Float = 0,
            DateTime = 1,
            String = 2,
            Integer = 3,
            Boolean = 4
        }

        private int numberDecimalPlaces;
        public int NumberDecimalPlaces
        {
            set { numberDecimalPlaces = value; }
            get { return numberDecimalPlaces; }
        }

        #endregion Tag

        /// <summary>
        /// Loads the command from the XML node.
        /// </summary>
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
        /// Saves the configuration into the XML node.
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
            xmlElem.AppendElem("Format", (int)TagFormat);
            xmlElem.AppendElem("NumberDecimalPlaces", (int)NumberDecimalPlaces);
            xmlElem.AppendElem("Enable", TagEnabled);
        }

    }
}
