using System.Xml;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
{
    /// <summary>
    /// Parser settings.
    /// <para>Настройки парсера.</para>
    /// </summary>
    public class TaskSettings
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public TaskSettings()
        {
            ID = Guid.NewGuid();
            Enabled = false;
            Name = "";
            Description = "";
            UseSubDir = false;
            AddFiles = true;
            DeleteFiles = false;
            UseReadFromLastLine = false;
            UseReadJustOneLastLine = false;
            Path = "";
            Filter = "";
            TemplateFileName = "";
            ScriptSelect = "";
            ScriptInsert = "";
            ScriptUpdate = "";
            ScriptDelete = "";
            ScriptRename = "";
            ScriptSynchronization = "";
            ScriptParserSelect = "";
            ScriptParserInsert = "";
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets are subfolders used.
        /// </summary>
        public bool UseSubDir { get; set; }

        /// <summary>
        /// Gets or sets are add files used.
        /// </summary>
        public bool AddFiles{ get; set; }

        /// <summary>
        /// Gets or sets are add files used.
        /// </summary>
        public bool DeleteFiles { get; set; }

        /// <summary>
        /// Gets or sets read from the last line.
        /// </summary>
        public bool UseReadFromLastLine { get; set; }

        /// <summary>
        /// Gets or sets read just one last line.
        /// </summary>
        public bool UseReadJustOneLastLine { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the template name.
        /// </summary>
        public string TemplateFileName { get; set; }

        /// <summary>
        /// Gets or sets the script select.
        /// </summary>
        public string ScriptSelect { get; set; }

        /// <summary>
        /// Gets or sets the script insert.
        /// </summary>
        public string ScriptInsert { get; set; }

        /// <summary>
        /// Gets or sets the script update.
        /// </summary>
        public string ScriptUpdate { get; set; }

        /// <summary>
        /// Gets or sets the script rename.
        /// </summary>
        public string ScriptRename { get; set; }

        /// <summary>
        /// Gets or sets the script synchronization.
        /// </summary>
        public string ScriptSynchronization { get; set; }

        /// <summary>
        /// Gets or sets the script delete.
        /// </summary>
        public string ScriptDelete { get; set; }

        /// <summary>
        /// Gets or sets the script parser select.
        /// </summary>
        public string ScriptParserSelect { get; set; }

        /// <summary>
        /// Gets or sets the script parser select.
        /// </summary>
        public string ScriptParserInsert { get; set; }

        /// <summary>
        /// Loads the settings from the XML node.
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
            Path = xmlNode.GetChildAsString("Path");

            AddFiles = xmlNode.GetChildAsBool("AddFiles");
            DeleteFiles = xmlNode.GetChildAsBool("DeleteFiles");

            UseSubDir = xmlNode.GetChildAsBool("UseSubDir");
            UseReadFromLastLine = xmlNode.GetChildAsBool("UseReadFromLastLine");
            UseReadJustOneLastLine = xmlNode.GetChildAsBool("UseReadJustOneLastLine");
            
            Filter = xmlNode.GetChildAsString("Filter");
            TemplateFileName = xmlNode.GetChildAsString("TemplateFileName");

            ScriptSelect = xmlNode.GetChildAsString("ScriptSelect");
            ScriptInsert = xmlNode.GetChildAsString("ScriptInsert");
            ScriptUpdate = xmlNode.GetChildAsString("ScriptUpdate");
            ScriptRename = xmlNode.GetChildAsString("ScriptRename");
            ScriptSynchronization = xmlNode.GetChildAsString("ScriptSynchronization");
            ScriptDelete = xmlNode.GetChildAsString("ScriptDelete");
            ScriptParserSelect = xmlNode.GetChildAsString("ScriptParserSelect");
            ScriptParserInsert = xmlNode.GetChildAsString("ScriptParserInsert");
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

            xmlElem.AppendElem("ID", ID);
            xmlElem.AppendElem("Enabled", Enabled);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Description", Description);
            xmlElem.AppendElem("Path", Path);

            xmlElem.AppendElem("AddFiles", AddFiles);
            xmlElem.AppendElem("DeleteFiles", DeleteFiles);

            xmlElem.AppendElem("UseSubDir", UseSubDir);
            xmlElem.AppendElem("UseReadFromLastLine", UseReadFromLastLine);
            xmlElem.AppendElem("UseReadJustOneLastLine", UseReadJustOneLastLine);
            
            xmlElem.AppendElem("Filter", Filter);
            xmlElem.AppendElem("TemplateFileName", TemplateFileName);

            xmlElem.AppendElem("ScriptSelect", ScriptSelect);
            xmlElem.AppendElem("ScriptInsert", ScriptInsert);
            xmlElem.AppendElem("ScriptUpdate", ScriptUpdate);
            xmlElem.AppendElem("ScriptRename", ScriptRename);
            xmlElem.AppendElem("ScriptSynchronization", ScriptSynchronization);
            xmlElem.AppendElem("ScriptDelete", ScriptDelete);
            xmlElem.AppendElem("ScriptParserSelect", ScriptParserSelect);
            xmlElem.AppendElem("ScriptParserInsert", ScriptParserInsert);
        }
    }
}
