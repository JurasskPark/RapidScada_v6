using System.Xml;

namespace Scada.Comm.Drivers.DrvPingJP
{

    public class GroupTag
    {
        public GroupTag()
        {
            this.ListTag = new List<DriverTag>();
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

        public DriverTag(string code, string name, string ipAddress, int timeout, bool enabled)
        {
            this.Code = code;
            this.Name = name;
            this.IpAddress = ipAddress;
            this.Timeout = timeout;
            this.Enabled = enabled;          
        }

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

        private Guid id;
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string ipAddress;
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        private int timeout;
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        private bool enabled;
        public bool Enabled
        {
            set { enabled = value; }
            get { return enabled; }
        }

        private int val;
        public int Val
        {
            set { val = value; }
            get { return val; }
        }

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
