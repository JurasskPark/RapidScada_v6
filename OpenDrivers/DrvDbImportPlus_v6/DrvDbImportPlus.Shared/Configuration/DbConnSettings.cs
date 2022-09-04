// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Xml;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// DB connection settings.
    /// <para>Настройки соединения с БД.</para>
    /// </summary>
    internal class DbConnSettings
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DbConnSettings()
        {
            Server = "";
            Database = "";
            User = "";
            Password = "";
            Port = "";
            ConnectionString = "";
        }


        /// <summary>
        /// Gets or sets the server host.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the database port.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the database username.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the database user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the database optional options.
        /// </summary>
        public string OptionalOptions { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }


        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
                throw new ArgumentNullException("xmlNode");

            Server = xmlNode.GetChildAsString("Server");
            Database = xmlNode.GetChildAsString("Database");
            Port = xmlNode.GetChildAsString("Port");
            User = xmlNode.GetChildAsString("User");
            Password = ScadaUtils.Decrypt(xmlNode.GetChildAsString("Password"));
            OptionalOptions = xmlNode.GetChildAsString("OptionalOptions");
            ConnectionString = ScadaUtils.Decrypt(xmlNode.GetChildAsString("ConnectionString"));
        }

        /// <summary>
        /// Saves the settings into the XML node.
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
                throw new ArgumentNullException("xmlElem");

            xmlElem.AppendElem("Server", Server);
            xmlElem.AppendElem("Database", Database);
            xmlElem.AppendElem("Port", Port);
            xmlElem.AppendElem("User", User);
            xmlElem.AppendElem("Password", ScadaUtils.Encrypt(Password));
            xmlElem.AppendElem("OptionalOptions", OptionalOptions);
            xmlElem.AppendElem("ConnectionString", ScadaUtils.Encrypt(ConnectionString));
        }
    }
}
