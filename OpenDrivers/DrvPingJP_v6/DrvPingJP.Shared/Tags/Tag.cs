using System.Xml;

namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// Implements a group of tags.
    /// <para>Реализует группу тегов.</para>
    /// </summary>
    public class GroupTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public GroupTag()
        {
            this.ListTag = new List<DriverTag>();
        }

        #region Tag Group
        /// <summary>
        /// Id of the tag group.
        /// </summary>
        private Guid groupTagID;
        public Guid GroupTagID
        {
            get { return groupTagID; }
            set { groupTagID = value; }
        }

        /// <summary>
        /// Tag group name.
        /// </summary>
        private string groupTagName;
        public string GroupTagName
        {
            get { return groupTagName; }
            set { groupTagName = value; }
        }

        #endregion Tag Group

        #region List of tags
        /// <summary>
        /// List of tags.
        /// </summary>
        private List<DriverTag> listTag;
        public List<DriverTag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }
        #endregion List of tags

    }

    /// <summary>
    /// Implements the tag.
    /// <para>Реализует тег.</para>
    /// </summary>
    public class DriverTag
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverTag()
        {

        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverTag(string code, string name, string ipAddress, int timeout, bool enabled)
        {
            this.Code = code;
            this.Name = name;
            this.IpAddress = ipAddress;
            this.Timeout = timeout;
            this.Enabled = enabled;          
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DriverTag(string code, string name, string ipAddress, int timeout, bool enabled, int val = 0, int stat = 0)
        {
            this.Code = code;
            this.Name = name;
            this.IpAddress = ipAddress;
            this.Timeout = timeout;
            this.Enabled = enabled;
            this.Val = val;
            this.Stat = stat;
        }

        #region DriverTag
        /// <summary>
        /// The tag ID.
        /// </summary>
        private Guid id;
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// The tag code.
        /// </summary>
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// The tag name.
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The tag IP address.
        /// </summary>
        private string ipAddress;
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        /// <summary>
        /// The tag timeout.
        /// </summary>
        private int timeout;
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// The tag enabled.
        /// </summary>
        private bool enabled;
        public bool Enabled
        {
            set { enabled = value; }
            get { return enabled; }
        }

        /// <summary>
        /// The tag value.
        /// </summary>
        private int val;
        public int Val
        {
            set { val = value; }
            get { return val; }
        }

        /// <summary>
        /// The tag state.
        /// </summary>
        private int stat;
        public int Stat
        {
            set { stat = value; }
            get { return stat; }
        }

        #endregion DriverTag

        /// <summary>
        /// Loads the command from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            ID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("ID"));
            Name = xmlNode.GetChildAsString("Name");
            Code = xmlNode.GetChildAsString("Code");
            IpAddress = xmlNode.GetChildAsString("IPAddress");
            Timeout = xmlNode.GetChildAsInt("Timeout");
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

            xmlElem.AppendElem("ID", ID);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Code", Code);
            xmlElem.AppendElem("IPAddress", IpAddress);
            xmlElem.AppendElem("Timeout", Timeout);
            xmlElem.AppendElem("Enable", Enabled);
        }

    }

}
