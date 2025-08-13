using DrvFtpJP.Shared.FilesDirectorys;
using FluentFTP;
using Scada.Comm.Drivers.DrvFtpJP;
using Scada.Comm.Drivers.DrvFtpJP.View.Forms;
using Scada.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;
using ToolTip = System.Windows.Forms.ToolTip;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmFilesManager : Form
    {
        public FrmConfig formParent;                     // parent form
        public FtpClientSettings client = null;
        private DriverClient driverClient = null;
        private bool Closing = false;

        private string currentLocalPath = string.Empty;
        private string currentRemotePath = string.Empty;

        private Dictionary<int, string> ListRemoteFilesDownload = new Dictionary<int, string>();
        private object logLock = new object();

        public FrmFilesManager(FtpClientSettings client)
        {
            InitializeComponent();
            Initialize();
            this.client = client;
            this.driverClient = new DriverClient(client);
        }

        private void Initialize()
        {
            cmbLocalDrivers.Items.AddRange(FilesDirectoriesInformation.GetPhysicalDrivesNames().ToArray());
            cmbLocalDrivers.SelectedIndex = 0;

            this.lstLocalFiles.GotFocus += lvFiles_GotFocus;
            this.lstLocalFiles.LostFocus += lvFiles_LostFocus;
            this.lstRemoteFiles.GotFocus += lvFiles_GotFocus;
            this.lstRemoteFiles.LostFocus += lvFiles_LostFocus;

            DebugerReturn.OnDebug = new DebugerReturn.DebugData(Log);
            DebugerFilesReturn.OnDebug = new DebugerFilesReturn.DebugData(LogFiles);

            Dock = DockStyle.Fill;
            TopLevel = false;

            Translate();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            GetLocalDrives();
            Connect();
            GetRemoteDirectoriesAndFiles("/");
        }

        public void Connect()
        {        
           if(driverClient.Connect())
           {
                stsMainStatus.Text = DriverDictonary.MenuStatus(client.Name, client.Host, client.Username);
           }
        }

        public void Disconnect()
        {
            driverClient.Disconnect();
        }

        #region Translate
        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);

            // tranlaste the menu
            FormTranslator.Translate(cmnuLocalOperation, GetType().FullName);
            FormTranslator.Translate(cmnuRemoteOperation, GetType().FullName);
        }
        #endregion Translate

        #region Local

        private void GetLocalDrives()
        {
            DriveInfo[] localDrives;
            try
            {
                localDrives = DriveInfo.GetDrives();
                if (localDrives.Length == 0)
                {
                    MessageBox.Show(DriverDictonary.DiskErrorMessage, DriverDictonary.Error, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, DriverDictonary.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (UnauthorizedAccessException uae)
            {
                MessageBox.Show(uae.Message, DriverDictonary.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, DriverDictonary.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.lstLocalFiles.Items.Clear();
            this.SetColumnsForDrives();

            ListViewItem lvi;
            foreach (DriveInfo drive in localDrives)
            {
                try
                {
                    int indexImage = GetImageIndexByName("hdd");

                    lvi = new ListViewItem(drive.Name, indexImage);
                    lvi.SubItems.Add(drive.DriveType.ToString());
                    lvi.SubItems.Add(DriverUtils.DiskSize(drive.TotalSize));
                    lvi.SubItems.Add(DriverUtils.DiskSize(drive.TotalFreeSpace));
                    lvi.SubItems.Add(DriverUtils.DiskSize(drive.TotalSize - drive.TotalFreeSpace));
                    lvi.Tag = drive;
                    this.lstLocalFiles.Items.Add(lvi);
                }
                catch { }
            }

            foreach (ColumnHeader col in this.lstLocalFiles.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            this.currentLocalPath = DriverDictonary.Computer;
            this.txtLocalCurrentPath.Text = this.currentLocalPath;
        }

        private void SetColumnsForDrives()
        {
            this.lstLocalFiles.Columns.Clear();

            ColumnHeader ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnName;
            this.lstLocalFiles.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnType;
            this.lstLocalFiles.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnTotalSize;
            this.lstLocalFiles.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnFreeSpace;
            this.lstLocalFiles.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnOccupiedSpace;
            this.lstLocalFiles.Columns.Add(ch);

        }

        private void cmbLocalDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLocalDirectoriesAndFiles(cmbLocalDrivers.Text);
        }

        private void GetLocalDirectoriesAndFiles(string path)
        {
            try
            {
                List<FilesDirectoriesInformation> list = new List<FilesDirectoriesInformation>();
                list = FilesDirectoriesInformation.GetDirectoriesAndFiles(path);

                this.lstLocalFiles.Items.Clear();
                this.lstLocalFiles.SmallImageList = imgList16;
                this.lstLocalFiles.LargeImageList = imgList16;

                this.SetColumns(this.lstLocalFiles);

                ListViewItem lvi;

                // up 
                FilesDirectoriesInformation up = new FilesDirectoriesInformation();
                up.Name = "[..]";
                up.Type = FilesDirectoriesInformation.FilesDirectoriesType.None;

                lvi = new ListViewItem(up.Name, GetImageIndexByName("arrowup"));
                lvi.SubItems.Add(DriverDictonary.FilesDirectoriesTypeString(up.Type));
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Tag = up;
                this.lstLocalFiles.Items.Add(lvi);

                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        int indexImage = 0;
                        switch (list[i].Type)
                        {
                            case FilesDirectoriesInformation.FilesDirectoriesType.Directory:
                                indexImage = GetImageIndexByName("folder");
                                break;
                            case FilesDirectoriesInformation.FilesDirectoriesType.Link:
                                indexImage = GetImageIndexByName("folderlink");
                                break;
                            case FilesDirectoriesInformation.FilesDirectoriesType.File:
                                indexImage = GetImageIndexByName(list[i].Format);
                                if (list[i].Format == string.Empty)
                                {
                                    indexImage = GetImageIndexByName("file");
                                }
                                if (indexImage == -1)
                                {
                                    indexImage = GetImageIndexByName("file");
                                }
                                break;
                        }

                        lvi = new ListViewItem(list[i].Name, indexImage);
                        lvi.SubItems.Add(DriverDictonary.FilesDirectoriesTypeString(list[i].Type));
                        lvi.SubItems.Add(list[i].SizeString);
                        lvi.SubItems.Add(list[i].Date.ToString("yyyy-MM-dd HH:mm:ss"));
                        lvi.Tag = list[i];
                        this.lstLocalFiles.Items.Add(lvi);
                    }
                    catch { }
                }

                foreach (ColumnHeader col in this.lstLocalFiles.Columns)
                {
                    col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    int colWidthHeaderSize = col.Width;
                    col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    int colWidthContentSize = col.Width;
                    col.Width = Math.Max(colWidthHeaderSize, colWidthContentSize);
                }

                foreach (ColumnHeader col in this.lstLocalFiles.Columns)
                {
                    if (col.Text == DriverDictonary.ColumnSize)
                    {
                        if (col.Width < 74)
                        {
                            col.Width = 74;
                        }
                    }
                }

                this.currentLocalPath = path;
                this.txtLocalCurrentPath.Text = this.currentLocalPath;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void lstLocalFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                DriveInfo drive = currentItem.Tag as DriveInfo;
                this.GetLocalDirectoriesAndFiles(drive.RootDirectory.FullName);
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    DirectoryInfo currentDirectory = new DirectoryInfo(txtLocalCurrentPath.Text);
                    DirectoryInfo parrentDirectory = currentDirectory.Parent;

                    if (parrentDirectory == null)
                    {
                        this.GetLocalDrives();
                    }
                    else
                    {
                        this.GetLocalDirectoriesAndFiles(parrentDirectory.FullName);
                    }
                }
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory)
                {
                    this.GetLocalDirectoriesAndFiles(info.FullName);
                }
                else if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    this.txtLocalCurrentPath.Text = info.FullName;
                }
            }
        }

        #region Menu
        private void cmnuLocalOperationUpload_Click(object sender, EventArgs e)
        {
            LocalUpload();
        }

        private void LocalUpload()
        {
            ListView lv = lstLocalFiles as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                return;
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    return;
                }
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory)
                {
                    rtbFilesOperation.Text = string.Empty;
                    ListRemoteFilesDownload = new Dictionary<int, string>();

                    driverClient.LocalUploadDirectory(info.FullName, txtRemoteCurrentPath.Text.Trim(), FtpFolderSyncMode.Update, FtpRemoteExists.Overwrite);
                    GetLocalDirectoriesAndFiles(txtLocalCurrentPath.Text.Trim());
                    GetRemoteDirectoriesAndFiles(txtRemoteCurrentPath.Text.Trim());
                }
                else if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    rtbFilesOperation.Text = string.Empty;
                    ListRemoteFilesDownload = new Dictionary<int, string>();

                    driverClient.LocalUploadFile(info.FullName, txtRemoteCurrentPath.Text.Trim(), FtpRemoteExists.Overwrite);
                    GetLocalDirectoriesAndFiles(txtLocalCurrentPath.Text.Trim());
                    GetRemoteDirectoriesAndFiles(txtRemoteCurrentPath.Text.Trim());
                }
            }
        }

        private void cmnuLocalOperationRefresh_Click(object sender, EventArgs e)
        {
            LocalRefresh();
        }

        private void LocalRefresh()
        {
            this.GetLocalDirectoriesAndFiles(txtLocalCurrentPath.Text.Trim());
        }

        private void cmnuLocalOperationCreateDirectory_Click(object sender, EventArgs e)
        {
            LocaleCreateDirectory();
        }

        private void LocaleCreateDirectory()
        {
            string path = txtLocalCurrentPath.Text;
            FrmName frmName = new FrmName();
            if (frmName.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                driverClient.LocalCreateDirectory(path + @$"/" + frmName.FileName);
                this.GetLocalDirectoriesAndFiles(path);
            }
        }

        private void cmnuLocalOperationRename_Click(object sender, EventArgs e)
        {
            LocaleRename();
        }

        private void LocaleRename()
        {
            string currentPath = txtLocalCurrentPath.Text;
            string currentName = string.Empty;
            string renameName = string.Empty;

            ListView lv = lstLocalFiles as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                return;
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    return;
                }

                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory || info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    currentName = info.Name;

                    FrmName frmName = new FrmName();
                    frmName.isEditName = true;
                    frmName.FileName = currentName;
                    if (frmName.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string pathOld = currentPath + @"\" + currentName;
                        string pathNew = currentPath + @"\" + frmName.FileName.Trim();
                        if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory)
                        {
                            driverClient.LocalRename(pathOld, pathNew, false);
                        }
                        else if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                        {
                            driverClient.LocalRename(pathOld, pathNew, true);
                        }
                        this.GetLocalDirectoriesAndFiles(currentPath);
                    }
                }
            }
        }

        private void cmnuLocalOperationDelete_Click(object sender, EventArgs e)
        {
            LocaleDelete();
        }

        private void LocaleDelete()
        {
            ListView lv = lstLocalFiles as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                return;
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    return;
                }

                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory)
                {
                    DialogResult result = ShowConfirmationDialog(info.Name);

                    if (result == DialogResult.OK)
                    {
                        driverClient.LocalDeleteDirectory(info.FullName);
                        this.GetLocalDirectoriesAndFiles(txtLocalCurrentPath.Text.Trim());
                    }
                }
                else if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory || info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    DialogResult result = ShowConfirmationDialog(info.Name);

                    if (result == DialogResult.OK)
                    {
                        driverClient.LocalDeleteFile(info.FullName);
                        this.GetLocalDirectoriesAndFiles(txtLocalCurrentPath.Text.Trim());
                    }
                }
            }
        }
        #endregion Menu

        #region Buttom

        private void btnLocalRootDirectory_Click(object sender, EventArgs e)
        {
            GetLocalDirectoriesAndFiles(cmbLocalDrivers.Text);
        }

        private void btnLocalUpOneDirectory_Click(object sender, EventArgs e)
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(txtLocalCurrentPath.Text);
            DirectoryInfo parrentDirectory = currentDirectory.Parent;

            if (parrentDirectory == null)
            {
                this.GetLocalDrives();
            }
            else
            {
                this.GetLocalDirectoriesAndFiles(parrentDirectory.FullName);
            }
        }

        #endregion Button

        #endregion Local

        #region Remote
        private void GetRemoteDirectoriesAndFiles(string path)
        {
            try
            {
                List<FilesDirectoriesInformation> list = driverClient.GetRemoteListing(path);

                this.lstRemoteFiles.Items.Clear();
                this.lstRemoteFiles.SmallImageList = imgList16;
                this.lstRemoteFiles.LargeImageList = imgList16;

                this.SetColumns(this.lstRemoteFiles);

                ListViewItem lvi;

                // up 
                FilesDirectoriesInformation up = new FilesDirectoriesInformation();
                up.Name = "[..]";
                up.Type = FilesDirectoriesInformation.FilesDirectoriesType.None;

                lvi = new ListViewItem(up.Name, GetImageIndexByName("arrowup"));
                lvi.SubItems.Add(DriverDictonary.FilesDirectoriesTypeString(up.Type));
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Tag = up;
                this.lstRemoteFiles.Items.Add(lvi);

                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        int indexImage = 0;
                        switch (list[i].Type)
                        {
                            case FilesDirectoriesInformation.FilesDirectoriesType.Directory:
                                indexImage = GetImageIndexByName("folder");
                                break;
                            case FilesDirectoriesInformation.FilesDirectoriesType.Link:
                                indexImage = GetImageIndexByName("folderlink");
                                break;
                            case FilesDirectoriesInformation.FilesDirectoriesType.File:
                                indexImage = GetImageIndexByName(list[i].Format);
                                if (list[i].Format == string.Empty)
                                {
                                    indexImage = GetImageIndexByName("file");
                                }
                                if (indexImage == -1)
                                {
                                    indexImage = GetImageIndexByName("file");
                                }
                                break;
                        }

                        if (indexImage == -1)
                        {

                        }

                        lvi = new ListViewItem(list[i].Name, indexImage);
                        lvi.SubItems.Add(DriverDictonary.FilesDirectoriesTypeString(list[i].Type));
                        lvi.SubItems.Add(list[i].SizeString);
                        lvi.SubItems.Add(list[i].Date.ToString("yyyy-MM-dd HH:mm:ss"));
                        lvi.Tag = list[i];
                        this.lstRemoteFiles.Items.Add(lvi);
                    }
                    catch { }
                }

                foreach (ColumnHeader col in this.lstRemoteFiles.Columns)
                {
                    col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    int colWidthHeaderSize = col.Width;
                    col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    int colWidthContentSize = col.Width;
                    col.Width = Math.Max(colWidthHeaderSize, colWidthContentSize);
                }

                foreach (ColumnHeader col in this.lstRemoteFiles.Columns)
                {
                    if (col.Text == DriverDictonary.ColumnSize)
                    {
                        if (col.Width < 74)
                        {
                            col.Width = 74;
                        }
                    }
                }

                this.currentRemotePath = path;
                this.txtRemoteCurrentPath.Text = this.currentRemotePath;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void lstRemoteFiles_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                DriveInfo drive = currentItem.Tag as DriveInfo;
                this.GetRemoteDirectoriesAndFiles(drive.RootDirectory.FullName);
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    string path = DriverClient.GetUnixParentPath(txtRemoteCurrentPath.Text);
                    this.GetRemoteDirectoriesAndFiles(path);
                }

                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory)
                {
                    this.GetRemoteDirectoriesAndFiles(info.FullName);
                }
                else if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    this.txtRemoteCurrentPath.Text = info.FullName;
                }
            }
        }

        #region Menu
        private void cmnuRemoteOperationDownload_Click(object sender, EventArgs e)
        {
            RemoteDownload();
        }

        private void RemoteDownload()
        {
            ListView lv = lstRemoteFiles as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                return;
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    return;
                }
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory)
                {
                    rtbFilesOperation.Text = string.Empty;
                    ListRemoteFilesDownload = new Dictionary<int, string>();

                    driverClient.RemoteDownloadDirectory(txtLocalCurrentPath.Text.Trim(), info.FullName, FtpFolderSyncMode.Update, FtpLocalExists.Overwrite);
                    GetLocalDirectoriesAndFiles(txtLocalCurrentPath.Text.Trim());
                    GetRemoteDirectoriesAndFiles(txtRemoteCurrentPath.Text.Trim());
                }
                else if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    rtbFilesOperation.Text = string.Empty;
                    ListRemoteFilesDownload = new Dictionary<int, string>();

                    driverClient.RemoteDownloadFile(txtLocalCurrentPath.Text.Trim(), info.FullName, FtpLocalExists.Overwrite);
                    GetLocalDirectoriesAndFiles(txtLocalCurrentPath.Text.Trim());
                    GetRemoteDirectoriesAndFiles(txtRemoteCurrentPath.Text.Trim());
                }
            }
        }

        private void cmnuRemoteOperationRefresh_Click(object sender, EventArgs e)
        {
            RemoteRefresh();
        }

        private void RemoteRefresh()
        {
            this.GetRemoteDirectoriesAndFiles(txtRemoteCurrentPath.Text.Trim());
        }

        private void cmnuRemoteOperationCreateDirectory_Click(object sender, EventArgs e)
        {
            RemoteCreateDirectory();
        }

        private void RemoteCreateDirectory()
        {
            string path = txtRemoteCurrentPath.Text;
            FrmName frmName = new FrmName();
            if (frmName.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                driverClient.RemoteCreateDirectory(path + @$"/" + frmName.FileName, true);
                this.GetRemoteDirectoriesAndFiles(path);
            }
        }


        private void cmnuRemoteOperationRename_Click(object sender, EventArgs e)
        {
            RemoteRename();
        }

        private void RemoteRename()
        {
            string currentPath = txtRemoteCurrentPath.Text;
            string currentName = string.Empty;
            string renameName = string.Empty;

            ListView lv = lstRemoteFiles as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                return;
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    return;
                }

                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory || info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    currentName = info.Name;

                    FrmName frmName = new FrmName();
                    frmName.isEditName = true;
                    frmName.FileName = currentName;
                    if (frmName.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string pathOld = currentPath + @"/" + currentName;
                        string pathNew = currentPath + @"/" + frmName.FileName.Trim();
                        driverClient.RemoteRename(pathOld, pathNew);
                        this.GetRemoteDirectoriesAndFiles(currentPath);
                    }
                }
            }
        }

        private void cmnuRemoteOperationDelete_Click(object sender, EventArgs e)
        {
            RemoteDelete();
        }

        private void RemoteDelete()
        {
            ListView lv = lstRemoteFiles as ListView;
            if (lv == null)
            {
                return;
            }

            if (lv.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                return;
            }
            else if (currentItem.Tag is FilesDirectoriesInformation)
            {
                FilesDirectoriesInformation info = currentItem.Tag as FilesDirectoriesInformation;
                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.None)
                {
                    return;
                }

                if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory)
                {
                    DialogResult result = ShowConfirmationDialog(info.Name);

                    if (result == DialogResult.OK)
                    {
                        driverClient.RemoteDeleteDirectory(info.FullName);
                        this.GetRemoteDirectoriesAndFiles(txtRemoteCurrentPath.Text.Trim());
                    }
                }
                else if (info.Type == FilesDirectoriesInformation.FilesDirectoriesType.Directory || info.Type == FilesDirectoriesInformation.FilesDirectoriesType.File)
                {
                    DialogResult result = ShowConfirmationDialog(info.Name);

                    if (result == DialogResult.OK)
                    {
                        driverClient.RemoteDeleteFile(info.FullName);
                        this.GetRemoteDirectoriesAndFiles(txtRemoteCurrentPath.Text.Trim());
                    }
                }
            }
        }



        #endregion Menu

        #region Button

        private void btnRemoteRootDirectory_Click(object sender, EventArgs e)
        {
            GetLocalDirectoriesAndFiles("/");
        }

        private void btnRemoteUpOneDirectory_Click(object sender, EventArgs e)
        {
            string path = DriverClient.GetUnixParentPath(txtRemoteCurrentPath.Text);
            this.GetRemoteDirectoriesAndFiles(path);
        }

        #endregion Button

        #endregion Remote

        #region Common
        private void SetColumns(ListView lv)
        {
            lv.Columns.Clear();

            ColumnHeader ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnName;
            lv.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnType;
            lv.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnSize;
            ch.Width = 50;
            lv.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = DriverDictonary.ColumnChange;
            lv.Columns.Add(ch);
        }

        public int GetImageIndexByName(string imageName)
        {
            // Перебираем все изображения в ImageList
            for (int i = 0; i < imgList16.Images.Count; i++)
            {
                // Проверяем, совпадает ли имя текущего изображения с искомым
                if (imgList16.Images.Keys[i].StartsWith(imageName))
                {
                    return i; // Возвращаем найденный индекс
                }
            }
            return -1; // Возвращаем -1, если изображение не найдено
        }

        private void lvFiles_GotFocus(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null) { return; }

            if (lv.Name == this.lstLocalFiles.Name)
            {
                this.tlpPanelLeft.BackColor = Color.LightGray;
            }
            else if (lv.Name == this.lstRemoteFiles.Name)
            {
                this.tlpPanelRight.BackColor = Color.LightGray;
            }
        }

        private void lvFiles_LostFocus(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null) { return; }

            if (lv.Name == this.lstLocalFiles.Name)
            {
                this.tlpPanelLeft.BackColor = SystemColors.Control;
            }
            else
            {
                this.tlpPanelRight.BackColor = SystemColors.Control;
            }
        }

        private static DialogResult ShowConfirmationDialog(string name)
        {
            return MessageBox.Show(
                String.Format(DriverDictonary.QuestDelete, name),
                DriverDictonary.ConfirmDelete,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );
        }
        #endregion Common

        #region Log

        public void Log(string text)
        {
            Log(text, Color.Black);
        }

        public void Log(string text, Color textColor)
        {
            if (Closing)
            {
                return;
            }

            if (text.StartsWith("OK   :"))
            {
                textColor = Color.Green;
            }

            if (text.StartsWith("ERROR:"))
            {
                textColor = Color.Red;
            }

            Invoke((Action)(() =>
            {
                lock (rtbStatus)
                {
                    try
                    {
                        string str = text.Replace("\r\n", "\n") + "\n";
                        rtbStatus.AppendText(str);
                        rtbStatus.Select(rtbStatus.Text.Length - str.Length, str.Length);
                        rtbStatus.SelectionColor = textColor;

                        if (rtbStatus.Text.Length > 2048)
                        {
                            rtbStatus.Text = rtbStatus.Text.Remove(0, rtbStatus.Text.Length - 2048);
                        }

                        rtbStatus.Select(rtbStatus.Text.Length, 0);
                        rtbStatus.ScrollToCaret();
                    }
                    catch { }
                }
            }));
        }


        #endregion Log

        #region LogFiles

        public void LogFiles(FtpProgress progress, string direction)
        {
            LogFiles(progress, direction, Color.Black);
        }

        public void LogFiles(FtpProgress progress, string direction, Color textColor)
        {
            if (Closing)
                return;

            string text = string.Empty;
            string findText = $"[{progress.LocalPath}]";
            if (direction == "<-")
            {
                text = $"[{progress.LocalPath}] {direction} [{progress.RemotePath}] " +
                             $"[{DriverUtils.SpeedSize((long)progress.TransferSpeed)}] " +
                             $"[{DriverUtils.DiskSize((long)progress.TransferredBytes)}] ";
            }
            else if (direction == "->")
            {
                text = $"[{progress.LocalPath}] {direction} [{progress.RemotePath}] " +
                             $"[{DriverUtils.SpeedSize((long)progress.TransferSpeed)}] " +
                             $"[{DriverUtils.DiskSize((long)progress.TransferredBytes)}] " +
                             $"[{progress.Progress:F2} %] ";

            }

            Invoke(() => UpdateLog(progress.FileIndex, text, findText, textColor));
        }

        private void UpdateLog(int fileIndex, string text, string findText, Color textColor)
        {
            lock (logLock) // Используйте отдельный объект для блокировки
            {
                try
                {
                    if (ListRemoteFilesDownload.TryGetValue(fileIndex, out _))
                    {
                        // Обновление существующей записи
                        UpdateExistingLine(fileIndex, text, findText, textColor);
                    }
                    else
                    {
                        // Добавление новой записи
                        AddNewLine(text, textColor);
                        ListRemoteFilesDownload[fileIndex] = text;
                    }
                }
                catch (Exception ex)
                {
                    // Логирование ошибки
                    Console.WriteLine($"Ошибка при обновлении лога: {ex.Message}");
                }
            }
        }

        private void AddNewLine(string text, Color textColor)
        {
            rtbFilesOperation.AppendText(text + Environment.NewLine);
            ApplyColorToLastLine(text.Length, textColor);
            rtbFilesOperation.ScrollToCaret();
        }

        private void UpdateExistingLine(int fileIndex, string text, string findText, Color textColor)
        {
            // Находим индекс строки
            int lineIndex = FindLineIndexByFileIndex(findText);

            if (lineIndex != -1)
            {
                // Получаем текущую позицию
                int startIndex = rtbFilesOperation.GetFirstCharIndexFromLine(lineIndex);
                int endIndex = GetEndOfLineIndex(lineIndex);

                // Обновляем текст
                rtbFilesOperation.Select(startIndex, endIndex - startIndex);
                rtbFilesOperation.SelectedText = text;
                ApplyColorToLastLine(text.Length, textColor);
                rtbFilesOperation.ScrollToCaret();
            }
        }

        private int FindLineIndexByFileIndex(string findText)
        {
            for (int i = 0; i < rtbFilesOperation.Lines.Length; i++)
            {
                if (rtbFilesOperation.Lines[i].StartsWith(findText))
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetEndOfLineIndex(int lineIndex)
        {
            return lineIndex < rtbFilesOperation.Lines.Length - 1
                ? rtbFilesOperation.GetFirstCharIndexFromLine(lineIndex + 1) - 1
                : rtbFilesOperation.Text.Length;
        }

        private void ApplyColorToLastLine(int length, Color color)
        {
            rtbFilesOperation.Select(rtbFilesOperation.Text.Length - length, length);
            rtbFilesOperation.SelectionColor = color;
            rtbFilesOperation.Select(rtbFilesOperation.Text.Length, 0);
        }

        #endregion LogFiles




    }
}
