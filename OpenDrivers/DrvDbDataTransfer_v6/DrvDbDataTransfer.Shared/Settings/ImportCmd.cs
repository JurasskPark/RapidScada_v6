using InfluxDB.Client.Api.Domain;
using System.Xml;
using System.Xml.Linq;

namespace Scada.Comm.Drivers.DrvDbDataTransfer
{
    /// <summary>
    /// Projecturation of a data import command.
    /// <para>Конфигурация команды import данных.</para>
    /// </summary>
    public class ImportCmd : IComparable<ImportCmd>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ImportCmd()
        {
            Id = Guid.NewGuid();
            Enabled = true;
            CmdNum = 0;
            CmdCode = "DBIMPORTCODE";
            Name = "";
            Description = "";
            SelectQuery = "";
            InsertQuery = "";
            StopOnError = true;
            BatchSize = 0;
            HistoryEnabled = false;
            HistoryWindowMinutes = 60;
            HistoryBatchSize = 0;
            HistoryStopOnError = true;
            IsColumnBased = true;
            DeviceTags = new List<DriverTag>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the command number.
        /// </summary>
        public int CmdNum { get; set; }

        /// <summary>
        /// Gets or sets the command code.
        /// </summary>
        public string CmdCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the SQL SELECT query executed against the source database.
        /// </summary>
        public string SelectQuery { get; set; }

        /// <summary>
        /// Gets or sets the parameterized SQL command executed against the target database.
        /// </summary>
        public string InsertQuery { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether transfer stops on first target write error.
        /// </summary>
        public bool StopOnError { get; set; }

        /// <summary>
        /// Gets or sets the optional write batch size. Zero means row-by-row execution in one transaction.
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether historical SQL window import is enabled.
        /// </summary>
        public bool HistoryEnabled { get; set; }

        /// <summary>
        /// Gets or sets historical query window length in minutes.
        /// </summary>
        public int HistoryWindowMinutes { get; set; }

        /// <summary>
        /// Gets or sets write batch size used by historical import. Zero uses BatchSize.
        /// </summary>
        public int HistoryBatchSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether historical import stops on first error.
        /// </summary>
        public bool HistoryStopOnError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the driver tags based on the list of requested table columns.
        /// </summary>
        public bool IsColumnBased { get; set; }

        /// <summary>
        /// Gets or sets tag names as a list.
        /// </summary>
        public List<DriverTag> DeviceTags { get; set; }

        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            Id = Guid.Parse(xmlNode.GetChildAsString("Id"));
            Enabled = xmlNode.GetChildAsBool("Enabled");
            CmdNum = xmlNode.GetChildAsInt("CmdNum");
            CmdCode = xmlNode.GetChildAsString("CmdCode");
            Name = xmlNode.GetChildAsString("Name");
            Description = xmlNode.GetChildAsString("Description");
            SelectQuery = xmlNode.GetChildAsString("SelectQuery");
            if (string.IsNullOrEmpty(SelectQuery))
            {
                SelectQuery = xmlNode.GetChildAsString("Query");
            }
            InsertQuery = xmlNode.GetChildAsString("InsertQuery");
            StopOnError = xmlNode.SelectSingleNode("StopOnError") == null ||
                xmlNode.GetChildAsBool("StopOnError");
            BatchSize = xmlNode.GetChildAsInt("BatchSize");
            HistoryEnabled = xmlNode.GetChildAsBool("HistoryEnabled");
            HistoryWindowMinutes = xmlNode.SelectSingleNode("HistoryWindowMinutes") == null ?
                60 :
                xmlNode.GetChildAsInt("HistoryWindowMinutes");
            HistoryBatchSize = xmlNode.GetChildAsInt("HistoryBatchSize");
            HistoryStopOnError = xmlNode.SelectSingleNode("HistoryStopOnError") == null ||
                xmlNode.GetChildAsBool("HistoryStopOnError");

            IsColumnBased = xmlNode.GetChildAsBool("IsColumnBased");

            if (xmlNode.SelectSingleNode("DeviceTags") is XmlNode importDeviceTagsNode)
            {
                foreach (XmlNode importDeviceTagNode in importDeviceTagsNode.SelectNodes("Tag"))
                {
                    DriverTag importDeviceTag = new DriverTag();
                    importDeviceTag.LoadFromXml(importDeviceTagNode);
                    DeviceTags.Add(importDeviceTag);
                }
            }
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

            xmlElem.AppendElem("Id", Id);
            xmlElem.AppendElem("Enabled", Enabled);
            xmlElem.AppendElem("CmdNum", CmdNum);
            xmlElem.AppendElem("CmdCode", CmdCode);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Description", Description);
            xmlElem.AppendElem("SelectQuery", SelectQuery);
            xmlElem.AppendElem("InsertQuery", InsertQuery);
            xmlElem.AppendElem("StopOnError", StopOnError);
            xmlElem.AppendElem("BatchSize", BatchSize);
            xmlElem.AppendElem("HistoryEnabled", HistoryEnabled);
            xmlElem.AppendElem("HistoryWindowMinutes", HistoryWindowMinutes);
            xmlElem.AppendElem("HistoryBatchSize", HistoryBatchSize);
            xmlElem.AppendElem("HistoryStopOnError", HistoryStopOnError);

            xmlElem.AppendElem("IsColumnBased", IsColumnBased); 

            XmlElement importDeviceTagsElem = xmlElem.AppendElem("DeviceTags");
            foreach (DriverTag importDeviceTag in DeviceTags)
            {
                importDeviceTag.SaveToXml(importDeviceTagsElem.AppendElem("Tag"));
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        public int CompareTo(ImportCmd other)
        {
            return CmdNum.CompareTo(other.CmdNum);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("[{0}] {1}", CmdNum, Name);
        }
    }
}
