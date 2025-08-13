using FluentFTP;
using Scada;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    public class OperationAction
    {
        public OperationAction()
        {
            ID = Guid.NewGuid();        
            Operation = OperationsActions.None;
            IsFile = false;
            Enabled = true;
            LocalPath = string.Empty;
            RemotePath = string.Empty;
            Mode = FtpFolderSyncMode.Update;
            RemoteExistsMode = FtpRemoteExists.Skip;
            LocalExistsMode = FtpLocalExists.Skip;
            FtpOptions = FtpVerify.None;
            Formats = new List<string>();
            MaxSizeFile = 0;
        }

        public enum OperationsActions : int
        {
            None = 0,
            LocalCreateDirectory = 1,
            RemoteCreateDirectory = 2,
            LocalRename = 3,
            RemoteRename = 4,
            LocalDeleteFile = 5,
            LocalDeleteDirectory = 6,
            RemoteDeleteFile = 7,
            RemoteDeleteDirectory = 8,
            LocalUploadFile = 9,
            LocalUploadDirectory = 10,
            RemoteDownloadFile = 11,
            RemoteDownloadDirectory = 12,
        }

        public Guid ID { get; set; }
        public bool Enabled { get; set; }
        public OperationsActions Operation { get; set; }
        public bool IsFile { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }
        public FtpFolderSyncMode Mode { get; set; }
        public FtpRemoteExists RemoteExistsMode { get; set; }
        public FtpLocalExists LocalExistsMode { get; set; }
        public FtpVerify FtpOptions { get; set; }
        public List<string> Formats { get; set; }
        public long MaxSizeFile { get; set; }

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
            Operation = (OperationsActions)Enum.Parse(typeof(OperationsActions), xmlNode.GetChildAsString("Operation"));
            LocalPath = xmlNode.GetChildAsString("LocalPath");
            RemotePath = xmlNode.GetChildAsString("RemotePath");
            Mode = (FtpFolderSyncMode)Enum.Parse(typeof(FtpFolderSyncMode), xmlNode.GetChildAsString("Mode"));
            RemoteExistsMode = (FtpRemoteExists)Enum.Parse(typeof(FtpRemoteExists), xmlNode.GetChildAsString("RemoteExistsMode"));
            LocalExistsMode = (FtpLocalExists)Enum.Parse(typeof(FtpLocalExists), xmlNode.GetChildAsString("LocalExistsMode"));
            FtpOptions = (FtpVerify)Enum.Parse(typeof(FtpVerify), xmlNode.GetChildAsString("FtpOptions"));
            Formats = xmlNode.GetChildAsString("Formats").Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            MaxSizeFile = Convert.ToInt64(xmlNode.GetChildAsString("MaxSizeFile"));
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
            xmlElem.AppendElem("Operation", Enum.GetName(typeof(OperationsActions), Operation));
            xmlElem.AppendElem("LocalPath", LocalPath);
            xmlElem.AppendElem("RemotePath", RemotePath);
            xmlElem.AppendElem("Mode", Enum.GetName(typeof(FtpFolderSyncMode), Mode));
            xmlElem.AppendElem("RemoteExistsMode", Enum.GetName(typeof(FtpRemoteExists), RemoteExistsMode));
            xmlElem.AppendElem("LocalExistsMode", Enum.GetName(typeof(FtpLocalExists), LocalExistsMode));
            xmlElem.AppendElem("FtpOptions", Enum.GetName(typeof(FtpVerify), FtpOptions));
            xmlElem.AppendElem("Formats", String.Join(", ", Formats));
            xmlElem.AppendElem("MaxSizeFile", MaxSizeFile);
        }
        #endregion Save
        #endregion Xml

    }
}
