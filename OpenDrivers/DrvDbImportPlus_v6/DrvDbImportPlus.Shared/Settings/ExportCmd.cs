// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Xml;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Projecturation of a data export command.
    /// <para>Конфигурация команды экспорта данных.</para>
    /// </summary>
    public class ExportCmd : IComparable<ExportCmd>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ExportCmd()
        {
            Id = Guid.NewGuid();
            Enabled = true;
            CmdNum = 0;
            CmdCode = "DBEXPORTCODE"; 
            Name = "";
            Description = "";
            Query = "";
            IsColumnBased = true;
            Length = 80;
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
        /// Gets or sets the command name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the SQL-query of the command.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the driver tags based on the list of requested table columns.
        /// </summary>
        public bool IsColumnBased { get; set; }

        /// <summary>
        /// Gets or sets the lenght command (string).
        /// </summary>
        public int Length { get; set; } = 80;

        /// <summary>
        /// Loads the command from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            string idText = xmlNode.GetChildAsString("Id");
            if (string.IsNullOrWhiteSpace(idText))
            {
                idText = xmlNode.GetChildAsString("ID");
            }
            Id = string.IsNullOrWhiteSpace(idText) ? Guid.NewGuid() : Guid.Parse(idText);

            Enabled = xmlNode.GetChildAsBool("Enabled");
            CmdNum = xmlNode.GetChildAsInt("CmdNum");
            CmdCode = xmlNode.GetChildAsString("CmdCode");
            Name = xmlNode.GetChildAsString("Name");
            Description = xmlNode.GetChildAsString("Description");
            Query = xmlNode.GetChildAsString("Query");

            Length = xmlNode.GetChildAsInt("Length");
            if (Length <= 0)
            {
                Length = xmlNode.GetChildAsInt("Lenght"); // backward compatibility
            }
            if (Length <= 0)
            {
                Length = 80;
            }
        }

        /// <summary>
        /// Saves the command into the XML node.
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
            xmlElem.AppendElem("Query", Query);
            xmlElem.AppendElem("Length", Length);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        public int CompareTo(ExportCmd other)
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
