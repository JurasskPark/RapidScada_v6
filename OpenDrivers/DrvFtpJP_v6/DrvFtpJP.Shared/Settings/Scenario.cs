using Scada.Comm.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    public class Scenario
    {
        public Scenario()
        {
            ID = Guid.NewGuid();
            Enabled = true;
            Name = string.Empty;
            Description = string.Empty;
            Actions = new List<OperationAction>();
        }

        public Guid ID { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<OperationAction> Actions { get; set; }

        #region Xml

        #region Load
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
            catch { }

        }
        #endregion Load

        #region Save
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
            catch { }
        }
        #endregion Save

        #endregion Xml
    }
}
