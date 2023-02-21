using System.Xml;

namespace Scada.Comm.Drivers.DrvPingJP
{

    public class GroupTag
    {
        public GroupTag()
        {

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

        private List<Tag> listTag;
        public List<Tag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }

        #endregion List of tags

    }

    public class Tag
    {
        public Tag()
        {

        }

        public Tag(string tagCode, string tagName, string tagIPAddress,  bool tagEnabled)
        {
            this.TagCode = tagCode;
            this.TagName = tagName;
            this.TagIPAddress = tagIPAddress;
            this.TagEnabled = tagEnabled;          
        }

        public Tag(string tagCode, string tagName, string tagIPAddress, bool tagEnabled, object tagVal = null, int tagStat = 0)
        {
            this.TagCode = tagCode;
            this.TagName = tagName;
            this.TagIPAddress = tagIPAddress;
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

        private string tagCode;
        public string TagCode
        {
            get { return tagCode; }
            set { tagCode = value; }
        }

        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        private string tagIPAddress;
        public string TagIPAddress
        {
            get { return tagIPAddress; }
            set { tagIPAddress = value; }
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
            TagIPAddress = xmlNode.GetChildAsString("IPAddress");
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
            xmlElem.AppendElem("IPAddress", TagIPAddress);
            xmlElem.AppendElem("Enable", TagEnabled);
        }

    }

}
