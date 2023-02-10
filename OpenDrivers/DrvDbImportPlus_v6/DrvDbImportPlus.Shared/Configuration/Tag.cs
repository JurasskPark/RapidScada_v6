using Scada.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Xml.Linq;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{

    public class GroupTag
    {
        public GroupTag()
        {

        }

        #region Группа тегов

        //ID группы тегов
        private Guid groupTagID;
        public Guid GroupTagID
        {
            get { return groupTagID; }
            set { groupTagID = value; }
        }

        //Название группы тегов
        private string groupTagName;
        public string GroupTagName
        {
            get { return groupTagName; }
            set { groupTagName = value; }
        }

        #endregion Группа тегов

        #region Список тегов

        private List<Tag> listTag;
        public List<Tag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }

        #endregion Список тегов

    }

    public class Tag
    {
        public Tag()
        {

        }

        public Tag(string tagCode, string tagName, object tagType, object tagFormat, bool tagEnabled)
        {
            this.TagCode = tagCode;
            this.TagName = tagName;
            this.TagEnabled = tagEnabled;
        }

        public Tag(string tagCode, string tagName, object tagType, object tagFormat, bool tagEnabled, object tagVal = null, int tagStat = 0)
        {
            this.TagCode = tagCode;
            this.TagName = tagName;
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

        /// <summary>
        /// 
        /// </summary>
        public enum TypeTag
        {
            Current = 0,
            Historical = 1,
        }

        public enum FormatTag
        {
            Unknown = 0,
            AsIs = 1,
            Minute = 2,
            Hourly = 3,
            Dayly = 4,
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
            xmlElem.AppendElem("Enable", TagEnabled);
        }

    }

}
