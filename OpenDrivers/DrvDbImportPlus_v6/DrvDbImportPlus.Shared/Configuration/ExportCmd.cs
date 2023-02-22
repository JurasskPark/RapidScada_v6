// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Xml;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Configuration of a data export command.
    /// <para>Конфигурация команды экспорта данных.</para>
    /// </summary>
    internal class ExportCmd : IComparable<ExportCmd>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ExportCmd()
        {
            CmdNum = 1;
            CmdCode = "DBTAG"; 
            Name = "";
            Query = "";
        }

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
        /// Gets or sets the SQL-query of the command.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Loads the command from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }
                
            CmdNum = xmlNode.GetChildAsInt("CmdNum");
            CmdCode = xmlNode.GetChildAsString("CmdCode");
            Name = xmlNode.GetChildAsString("Name");
            Query = xmlNode.GetChildAsString("Query");
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
                
            xmlElem.AppendElem("CmdNum", CmdNum);
            xmlElem.AppendElem("CmdCode", CmdCode);
            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Query", Query);
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
