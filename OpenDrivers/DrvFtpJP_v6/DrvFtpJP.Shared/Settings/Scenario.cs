using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Represents an FTP operation scenario.
    /// <para>Представляет сценарий FTP-операций.</para>
    /// </summary>
    public class Scenario
    {
        #region Property

        /// <summary>
        /// Gets or sets the scenario identifier.
        /// <para>Возвращает или задает идентификатор сценария.</para>
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the scenario is enabled.
        /// <para>Возвращает или задает признак включения сценария.</para>
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the scenario name.
        /// <para>Возвращает или задает имя сценария.</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the scenario description.
        /// <para>Возвращает или задает описание сценария.</para>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the scenario action list.
        /// <para>Возвращает или задает список действий сценария.</para>
        /// </summary>
        public List<OperationAction> Actions { get; set; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public Scenario()
        {
            ID = Guid.NewGuid();
            Enabled = true;
            Name = string.Empty;
            Description = string.Empty;
            Actions = new List<OperationAction>();
        }

        /// <summary>
        /// Loads the scenario from the XML node.
        /// <para>Загружает сценарий из XML-узла.</para>
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            ID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("ID"));
            Enabled = xmlNode.GetChildAsBool("Enabled");
            Name = xmlNode.GetChildAsString("Name");
            Description = xmlNode.GetChildAsString("Description");

            try
            {
                if (xmlNode.SelectSingleNode("Actions") is XmlNode exportActionsNode)
                {
                    foreach (XmlNode exportActionNode in exportActionsNode.SelectNodes("Action"))
                    {
                        OperationAction action = new OperationAction();
                        action.LoadFromXml(exportActionNode);
                        Actions.Add(action);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Saves the scenario into the XML node.
        /// <para>Сохраняет сценарий в XML-узел.</para>
        /// </summary>
        public virtual void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException(nameof(xmlElem));
            }

            xmlElem.AppendElem("ID", ID);
            xmlElem.AppendElem("Enabled", Enabled);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Description", Description);

            try
            {
                XmlElement exportActionsElem = xmlElem.AppendElem("Actions");
                foreach (OperationAction action in Actions)
                {
                    action.SaveToXml(exportActionsElem.AppendElem("Action"));
                }
            }
            catch
            {
            }
        }

        #endregion Basic
    }
}
