using FluentFTP;
using Scada;
using Scada.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Scada.Comm.Drivers.DrvFtpJP
{
    public class FtpClientSettings
    {
        public FtpClientSettings()
        {
            Name = string.Empty;
            Host = "127.0.0.1";
            Username = "ftptest";
            Password = "ftppassword";
            Port = 21;
            FtpDataType = FtpDataConnectionType.AutoPassive;
            EncryptionMode = FtpEncryptionMode.None;
            IsFavorite = false;
            Encryption = false;
            SshKey = string.Empty;
            Config = new FtpConfig();
        }

        public string Name { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public FtpDataConnectionType FtpDataType { get; set; }
        public FtpEncryptionMode EncryptionMode { get; set; }
        public bool IsFavorite { get; set; }
        public bool Encryption { get; set; }
        public string SshKey { get; set; }
        public FtpConfig Config { get; set; }



        /// <summary>
        /// Loads the settings from the XML node.
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException("xmlNode");
            }

            Name = xmlNode.GetChildAsString("Name");
            Host = xmlNode.GetChildAsString("Host");
            Username = xmlNode.GetChildAsString("Username");
            Password = xmlNode.GetChildAsString("Password");
            Port = xmlNode.GetChildAsInt("Port");
            FtpDataType = (FtpDataConnectionType)Enum.Parse(typeof(FtpDataConnectionType), xmlNode.GetChildAsString("FtpDataType"));
            EncryptionMode = (FtpEncryptionMode)Enum.Parse(typeof(FtpEncryptionMode), xmlNode.GetChildAsString("EncryptionMode"));
            IsFavorite = xmlNode.GetChildAsBool("IsFavorite");
            Encryption = xmlNode.GetChildAsBool("Encryption");
            SshKey = xmlNode.GetChildAsString("SshKey");
            Config = new FtpConfig();
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

            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Host", Host);
            xmlElem.AppendElem("Username", Username);
            xmlElem.AppendElem("Password", Password);
            xmlElem.AppendElem("Port", Port);
            xmlElem.AppendElem("FtpDataType", Enum.GetName(typeof(FtpDataConnectionType), FtpDataType));
            xmlElem.AppendElem("EncryptionMode", Enum.GetName(typeof(FtpEncryptionMode), EncryptionMode));
            xmlElem.AppendElem("IsFavorite", IsFavorite);
            xmlElem.AppendElem("Encryption", Encryption);
            xmlElem.AppendElem("SshKey", SshKey);
        }
    }
}
