
using FastColoredTextBoxNS;
using MySqlX.XDevAPI.Relational;
using Scada.Comm.Config;
using Scada.Comm.Drivers.DrvDbImportPlus;
using Scada.Comm.Lang;
using Scada.Data.Models;
using Scada.Forms;
using Scada.Lang;
using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    /// <summary>
    /// Device configuration form.
    /// <para>Форма настройки конфигурации КП.</para>
    /// </summary>
    public partial class FrmConfig : Form
    {
        private readonly AppDirs appDirs;               // the application directories
        private readonly string driverCode;             // the driver code
        private readonly int deviceNum;                 // the device number
        private readonly DrvDbImportPlusConfig config;  // the device configuration
        private string configFileName;                  // the configuration file name
        private bool modified;                          // the configuration was modified
        private bool connChanging;                      // connection settings are changing
        private bool cmdSelecting;                      // a command is selecting

        private DataSource dataSource;                  // the data source

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        private FrmConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmConfig(AppDirs appDirs, int deviceNum)
            : this()
        {
            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.deviceNum = deviceNum;   
            this.driverCode = DriverUtils.DriverCode;
            config = new DrvDbImportPlusConfig();
            configFileName = Path.Combine(appDirs.ConfigDir, DrvDbImportPlusConfig.GetFileName(deviceNum));
            modified = false;
            connChanging = false;
            cmdSelecting = false;
            dataSource = null;
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            FormTranslator.Translate(txtHelp, GetType().FullName);
            FormTranslator.Translate(cmnuSelectQuery, GetType().FullName);
            FormTranslator.Translate(cmnuCmdQuery, GetType().FullName);
            FormTranslator.Translate(cmnuLstTags, GetType().FullName);

            Text = string.Format(Text, deviceNum);

            // load a configuration
            if (File.Exists(configFileName) && !config.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // language SQL
            txtSelectQuery.Language = Language.SQL;
            txtCmdQuery.Language = Language.SQL;

            // display the configuration
            ConfigToControls();
            Modified = false;
        }

        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified)
            {
                DialogResult result = MessageBox.Show(CommPhrases.SaveDeviceConfigConfirm,
                    CommonPhrases.QuestionCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        if (!config.Save(configFileName, out string errMsg))
                        {
                            ScadaUiUtils.ShowError(errMsg);
                            e.Cancel = true;
                        }
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// </summary>
        private bool Modified
        {
            get
            {
                return modified;
            }
            set
            {
                modified = value;
                btnSave.Enabled = modified;
            }
        }


        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            connChanging = true;
            cmdSelecting = true;

            // set the control values
            cbDataSourceType.SelectedIndex = (int)config.DataSourceType;
            txtServer.Text = config.DbConnSettings.Server;
            txtPort.Text = config.DbConnSettings.Port;
            txtDatabase.Text = config.DbConnSettings.Database;
            txtUser.Text = config.DbConnSettings.User;
            txtPassword.Text = config.DbConnSettings.Password;
            txtSelectQuery.Text = config.SelectQuery;
            txtOptionalOptions.Text = config.DbConnSettings.OptionalOptions;
            
            if (config.AutoTagCount)
            {
                numTagCount.Value = 0;
                numTagCount.Enabled = false;
                chkAutoTagCount.Checked = true;
            }
            else
            {
                numTagCount.SetValue(config.TagCount);
                numTagCount.Enabled = true;
                chkAutoTagCount.Checked = false;
            }

            foreach (string tag in config.DeviceTags)
            {
                if (!lstTags.Items.Contains(tag.Trim()))
                {
                    lstTags.Items.Add(tag.Trim());
                }
            }
 
            if (config.DeviceTagsBasedRequestedTableColumns)
            {
                rdbKPTagsBasedRequestedTableColumns.Checked = true;
            }
            else
            {
                rdbKPTagsBasedRequestedTableRows.Checked = true;
            }

            // tune the controls represent the connection properties
            if (config.DataSourceType == DataSourceType.Undefined)
            {
                gbConnection.Enabled = false;
                txtConnectionString.Text = "";
            }
            else
            {
                gbConnection.Enabled = true;
                string connStr = BuildConnectionsString();

                if (string.IsNullOrEmpty(connStr) || !string.IsNullOrEmpty(config.DbConnSettings.ConnectionString))
                {
                    txtConnectionString.Text = config.DbConnSettings.ConnectionString;
                    EnableConnString();
                }
                else
                {
                    EnableConnProps();
                }
            }

            // fill the command list
            cbCommand.Items.AddRange(config.ExportCmds.ToArray());

            if (cbCommand.Items.Count > 0)
            {
                cbCommand.SelectedIndex = 0;
            }
       
            ShowCommandParams(cbCommand.SelectedItem as ExportCmd);

            connChanging = false;
            cmdSelecting = false;
        }

        /// <summary>
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            config.DataSourceType = (DataSourceType)cbDataSourceType.SelectedIndex;
            config.DbConnSettings.Server = txtServer.Text;
            config.DbConnSettings.Port = txtPort.Text;
            config.DbConnSettings.Database = txtDatabase.Text;
            config.DbConnSettings.User = txtUser.Text;
            config.DbConnSettings.Password = txtPassword.Text;
            config.DbConnSettings.OptionalOptions = txtOptionalOptions.Text;
            config.DbConnSettings.ConnectionString = txtConnectionString.Text == BuildConnectionsString() ? "" : txtConnectionString.Text;
            config.SelectQuery = txtSelectQuery.Text;

            if (chkAutoTagCount.Checked)
            {
                config.AutoTagCount = true;
                config.TagCount = 0;
            }
            else
            {
                config.AutoTagCount = false;
                config.TagCount = Convert.ToInt32(numTagCount.Value);
            }

            config.DeviceTags = lstTags.Items.Cast<String>().ToList();

            if (rdbKPTagsBasedRequestedTableColumns.Checked)
            {
                config.DeviceTagsBasedRequestedTableColumns = true;
            }
            else if (rdbKPTagsBasedRequestedTableRows.Checked)
            {
                config.DeviceTagsBasedRequestedTableColumns = false;
            }
        }

        /// <summary>
        /// Builds a connection string based on the connection settings.
        /// </summary>
        private string BuildConnectionsString()
        {
            DataSourceType dataSourceType = (DataSourceType)cbDataSourceType.SelectedIndex;

            DbConnSettings connSettings = new DbConnSettings()
            {
                Server = txtServer.Text,
                Database = txtDatabase.Text,
                Port = txtPort.Text,
                User = txtUser.Text,
                Password = txtPassword.Text,
                OptionalOptions = txtOptionalOptions.Text
            };
            
            switch (dataSourceType)
            {
                case DataSourceType.MSSQL:
                    return SqlDataSource.BuildSqlConnectionString(connSettings);
                case DataSourceType.Oracle:
                    return OraDataSource.BuildOraConnectionString(connSettings);
                case DataSourceType.PostgreSQL:
                    return PgSqlDataSource.BuildPgSqlConnectionString(connSettings);
                case DataSourceType.MySQL:
                    return MySqlDataSource.BuildMySqlConnectionString(connSettings);
                case DataSourceType.OLEDB:
                    return OleDbDataSource.BuildOleDbConnectionString(connSettings);
                case DataSourceType.ODBC:
                    return OdbcDataSource.BuildOdbcConnectionString(connSettings);
                case DataSourceType.Firebird:
                    return FirebirdDataSource.BuildFbConnectionString(connSettings);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Display the connection controls like enabled.
        /// </summary>
        private void EnableConnProps()
        {
            txtServer.BackColor = txtDatabase.BackColor = txtUser.BackColor = txtPassword.BackColor = txtPort.BackColor = txtOptionalOptions.BackColor = Color.FromKnownColor(KnownColor.Window);
            txtServer.Enabled = txtDatabase.Enabled = txtUser.Enabled = txtPassword.Enabled = txtPort.Enabled = txtOptionalOptions.Enabled = true;
            txtConnectionString.BackColor = Color.FromKnownColor(KnownColor.Control);
            txtConnectionString.Enabled = false;
        }

        /// <summary>
        /// Display the connection string like enabled.
        /// </summary>
        private void EnableConnString()
        {
            txtServer.BackColor = txtDatabase.BackColor = txtUser.BackColor = txtPassword.BackColor = txtPort.BackColor = txtOptionalOptions.BackColor = Color.FromKnownColor(KnownColor.Control);
            txtServer.Enabled = txtDatabase.Enabled = txtUser.Enabled = txtPassword.Enabled = txtPort.Enabled = txtOptionalOptions.Enabled = false;
            txtConnectionString.BackColor = Color.FromKnownColor(KnownColor.Window);
            txtConnectionString.Enabled = true;
        }

        /// <summary>
        /// Shows the command parameters.
        /// </summary>
        private void ShowCommandParams(ExportCmd exportCmd)
        {
            if (exportCmd == null)
            {
                gbCommandParams.Enabled = false;
                numCmdNum.Value = numCmdNum.Minimum;
                txtCmdCode.Text = "DBTAG" + (numCmdNum.Value).ToString() + "";
                txtName.Text = "";
                txtCmdQuery.Text = "";
            }
            else
            {
                gbCommandParams.Enabled = true;
                numCmdNum.SetValue(exportCmd.CmdNum);
                txtCmdCode.Text = exportCmd.CmdCode;
                txtName.Text = exportCmd.Name;
                txtCmdQuery.Text = exportCmd.Query;
            }
        }

        /// <summary>
        /// Sort and update the command list.
        /// </summary>
        private void UpdateCommands()
        {
            try
            {
                cbCommand.BeginUpdate();
                config.ExportCmds.Sort();
                ExportCmd selectedCmd = cbCommand.SelectedItem as ExportCmd;

                cbCommand.Items.Clear();
                cbCommand.Items.AddRange(config.ExportCmds.ToArray());
                cbCommand.SelectedIndex = config.ExportCmds.IndexOf(selectedCmd);
            }
            finally
            {
                cbCommand.EndUpdate();
            }
        }

        /// <summary>
        /// Update text of the selected item.
        /// </summary>
        private void UpdateCommandItem()
        {
            if (cbCommand.SelectedIndex >= 0)
            {
                cbCommand.Items[cbCommand.SelectedIndex] = cbCommand.SelectedItem;
            }          
        }

        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbDataSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                connChanging = true;

                if (cbDataSourceType.SelectedIndex == 0)
                {
                    gbConnection.Enabled = false;
                    txtConnectionString.Text = "";
                }
                else
                {
                    gbConnection.Enabled = true;
                    string connStr = BuildConnectionsString();

                    if (string.IsNullOrEmpty(connStr))
                    {
                        EnableConnString();
                    }   
                    else
                    {
                        EnableConnProps();
                    }
                       
                }

                Modified = true;
                connChanging = false;
            }
        }

        private void txtConnProp_TextChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                string connStr = BuildConnectionsString();

                if (!string.IsNullOrEmpty(connStr))
                {
                    connChanging = true;
                    EnableConnProps();
                    connChanging = false;
                }

                Modified = true;
            }
        }

        private void txtSelectQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!connChanging)
            {
                string connStr = BuildConnectionsString();

                if (!string.IsNullOrEmpty(connStr))
                {
                    connChanging = true;
                    EnableConnProps();
                    connChanging = false;
                }

                Modified = true;
            }
        }

        private void txtConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                EnableConnString();
                Modified = true;
            }
        }

        private void rdbKPTagsBasedRequestedTableColumns_CheckedChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                Modified = true;
            }
        }

        private void rdbKPTagsBasedRequestedTableRows_CheckedChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                if (chkAutoTagCount.Checked == true)
                {
                    chkAutoTagCount.Checked = false;
                }
                Modified = true;
            }
        }

        private void chkAutoTagCount_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbKPTagsBasedRequestedTableRows.Checked == true)
            {
                numTagCount.Enabled = false;
                chkAutoTagCount.Checked = false;
            }
            numTagCount.Enabled = !chkAutoTagCount.Checked;
            Modified = true;
        }

        private void numTagCount_ValueChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting)
            {
                cmdSelecting = true;
                ShowCommandParams(cbCommand.SelectedItem as ExportCmd);
                cmdSelecting = false;
            }
        }

        private void btnCreateCommand_Click(object sender, EventArgs e)
        {
            // add a new command
            ExportCmd exportCmd = new ExportCmd();

            if (config.ExportCmds.Count > 0)
            {
                exportCmd.CmdNum = config.ExportCmds[config.ExportCmds.Count - 1].CmdNum + 1;
            }
                
            config.ExportCmds.Add(exportCmd);
            cbCommand.SelectedIndex = cbCommand.Items.Add(exportCmd);
            Modified = true;
        }

        private void btnDeleteCommand_Click(object sender, EventArgs e)
        {
            // delete the selected command
            int selectedIndex = cbCommand.SelectedIndex;

            if (selectedIndex >= 0)
            {
                config.ExportCmds.RemoveAt(selectedIndex);
                cbCommand.Items.RemoveAt(selectedIndex);

                if (cbCommand.Items.Count > 0)
                {
                    cbCommand.SelectedIndex = selectedIndex >= cbCommand.Items.Count ?
                        cbCommand.Items.Count - 1 : selectedIndex;
                }
                else
                {
                    ShowCommandParams(null);
                }

                Modified = true;
            }
        }

        private void numCmdNum_ValueChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.CmdNum = Convert.ToInt32(numCmdNum.Value);
                UpdateCommands();
                Modified = true;
            }
        }

        private void txtCmdCode_TextChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.CmdCode = txtCmdCode.Text;
                UpdateCommandItem();
                Modified = true;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.Name = txtName.Text;
                UpdateCommandItem();
                Modified = true;
            }
        }

        private void btnTagnameAdd_Click(object sender, EventArgs e)
        {
            if (!lstTags.Items.Contains(txtTagnameAdd.Text.Trim()))
            {
                lstTags.Items.Add(txtTagnameAdd.Text.Trim());
                numTagCount.Value = lstTags.Items.Count;
                Modified = true;
            }
        }

        private void btnTagnameAddList_Click(object sender, EventArgs e)
        {
            FrmInputBox InputBox = new FrmInputBox();
            InputBox.Values = string.Empty;
            InputBox.ShowDialog();

            if (InputBox.DialogResult == DialogResult.OK)
            {
                String[] tags = InputBox.Values.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (String tag in tags)
                {
                    if (!lstTags.Items.Contains(tag.Trim()))
                    {
                        lstTags.Items.Add(tag.Trim());
                        numTagCount.Value = lstTags.Items.Count;
                        Modified = true;
                    }
                }
            }
        }

        private void btnTagnameDelete_Click(object sender, EventArgs e)
        {
            if (lstTags.SelectedIndex != -1)
            {
                lstTags.Items.RemoveAt(lstTags.SelectedIndex);
                numTagCount.Value = lstTags.Items.Count;
                Modified = true;
            }
            else
            {
                MessageBox.Show(Locale.IsRussian ?
                    "Выберите тег для удаления!" :
                    "Select the tag to delete!");
            }
        }

        private void btnTagnameDeleteList_Click(object sender, EventArgs e)
        {
            lstTags.Items.Clear();
            numTagCount.Value = lstTags.Items.Count;
            Modified = true;
        }

        private void lstTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTags.SelectedIndex >= 0)
            {
                txtTagnameAdd.Text = lstTags.SelectedItem.ToString();          
            }
            Modified = true;
        }

        private void txtCmdQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.Query = txtCmdQuery.Text;
                Modified = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // retrieve the configuration
            ControlsToConfig();

            // save the configuration
            if (config.Save(configFileName, out string errMsg))
            {
                Modified = false;
            }          
            else
            {
                ScadaUiUtils.ShowError(errMsg);
            }
                
        }

        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            // retrieve the configuration
            ControlsToConfig();

            // save the configuration
            if (config.Save(configFileName, out string errMsg))
            {
                Modified = false;
            }
            else
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // load a configuration
            if (File.Exists(configFileName) && !config.Load(configFileName, out errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // set the control values
            ConfigToControls();

            switch (config.DataSourceType)
            {
                case DataSourceType.MSSQL:
                    dataSource = new SqlDataSource();
                    break;
                case DataSourceType.Oracle:
                    dataSource = new OraDataSource();
                    break;
                case DataSourceType.PostgreSQL:
                    dataSource = new PgSqlDataSource();
                    break;
                case DataSourceType.MySQL:
                    dataSource = new MySqlDataSource();
                    break;
                case DataSourceType.OLEDB:
                    dataSource = new OleDbDataSource();
                    break;
                case DataSourceType.ODBC:
                    dataSource = new OdbcDataSource();
                    break;
                case DataSourceType.Firebird:
                    dataSource = new FirebirdDataSource();
                    break;
                default:
                    dataSource = null;
                    break;
            }

            if (dataSource != null)
            {
                string connStr = string.IsNullOrEmpty(config.DbConnSettings.ConnectionString) ?
                    dataSource.BuildConnectionString(config.DbConnSettings) :
                    config.DbConnSettings.ConnectionString;

                if (string.IsNullOrEmpty(connStr))
                {
                    dataSource = null;
                    MessageBox.Show(Locale.IsRussian ?
                        "Соединение не определено" :
                        "Connection is undefined");
                }
                else
                {
                    dataSource.Init(connStr, config);

                    try
                    {
                        Application.DoEvents();
                        dataSource.Connect();
                        MessageBox.Show(Locale.IsRussian ?
                        "Соединение успешно прошло!" :
                        "The connection was successful!");
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format(Locale.IsRussian ?
                            "Ошибка при соединении с БД: {0}" :
                            "Error connecting to DB: {0}", ex.Message));
                    }
                }
            }
        }

        private void btnExecuteSQLQuery_Click(object sender, EventArgs e)
        {
            dataSource = DataSource.GetDataSourceType(config);
           
            if (dataSource != null)
            {
                string connStr = string.IsNullOrEmpty(config.DbConnSettings.ConnectionString) ?
                    dataSource.BuildConnectionString(config.DbConnSettings) :
                    config.DbConnSettings.ConnectionString;

                if (string.IsNullOrEmpty(connStr))
                {
                    dataSource = null;
                    MessageBox.Show(Locale.IsRussian ?
                        "Соединение не определено" :
                        "Connection is undefined");
                }
                else
                {
                    dataSource.Init(connStr, config);

                    try
                    {
                        dataSource.Connect();
                        DataTable dt = new DataTable();

                        //Tag based Columns
                        if (rdbKPTagsBasedRequestedTableColumns.Checked == true)
                        {
                            using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.SingleRow))
                            {             
                                if (reader.HasRows == true)
                                {        
                                    dt.Load(reader);
                                    dgvData.DataSource = dt;
                                }
                                else
                                {
                                    MessageBox.Show(Locale.IsRussian ?
                                        "Данные отсутствуют" :
                                        "No data available");
                                }                       
                            }
                        }
                        else //Tag base Columns
                        {
                            using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.Default))
                            {
                                if (reader.HasRows == true)
                                {
                                    dt.Load(reader);
                                    dgvData.DataSource = dt;
                                }
                                else
                                {
                                    MessageBox.Show(Locale.IsRussian ?
                                        "Данные отсутствуют" :
                                        "No data available");
                                }
                            }
                        }

                        dataSource.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format(Locale.IsRussian ?
                            "Ошибка при соединении с БД: {0}" :
                            "Error connecting to DB: {0}", ex.Message));
                    }
                }
            }
        }

        private void cmnuSelectQueryCut_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Cut();
        }

        private void cmnuSelectQueryCopy_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Copy();
        }

        private void cmnuSelectQueryPaste_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Paste();
        }

        private void cmnuSelectQuerySelectAll_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Selection.SelectAll();
        }

        private void cmnuSelectQueryUndo_Click(object sender, EventArgs e)
        {
            if (txtSelectQuery.UndoEnabled)
            {
                txtSelectQuery.Undo();
            }                
        }

        private void cmnuSelectQueryRedo_Click(object sender, EventArgs e)
        {
            if (txtSelectQuery.RedoEnabled)
            {
                txtSelectQuery.Redo();
            }               
        }

        private void cmnuCmdQueryCut_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Cut();
        }

        private void cmnuCmdQueryCopy_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Copy();
        }

        private void cmnuCmdQueryPaste_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Paste();
        }

        private void cmnuCmdQuerySelectAll_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Selection.SelectAll();
        }

        private void cmnuCmdQueryUndo_Click(object sender, EventArgs e)
        {
            if (txtCmdQuery.UndoEnabled)
            {
                txtCmdQuery.Undo();
            }     
        }

        private void cmnuCmdQueryRedo_Click(object sender, EventArgs e)
        {
            if (txtCmdQuery.RedoEnabled)
            {
                txtCmdQuery.Redo();
            }    
        }

        private void cmnuUp_Click(object sender, EventArgs e)
        {
            // an item must be selected
            if (lstTags.SelectedItems.Count > 0)
            {
                object selected = lstTags.SelectedItem;
                int index = lstTags.Items.IndexOf(selected);
                int total = lstTags.Items.Count;
                // if the item is right at the top, throw it right down to the bottom
                if (index == 0)
                {
                    lstTags.Items.Remove(selected);
                    lstTags.Items.Insert(total - 1, selected);
                    lstTags.SetSelected(total - 1, true);
                }
                else // to move the selected item upwards in the listbox
                {
                    lstTags.Items.Remove(selected);
                    lstTags.Items.Insert(index - 1, selected);
                    lstTags.SetSelected(index - 1, true);
                }
            }
        }

        private void cmnuDown_Click(object sender, EventArgs e)
        {
            // an item must be selected
            if (lstTags.SelectedItems.Count > 0)
            {
                object selected = lstTags.SelectedItem;
                int index = lstTags.Items.IndexOf(selected);
                int total = lstTags.Items.Count;
                // if the item is last in the listbox, move it all the way to the top
                if (index == total - 1)
                {
                    lstTags.Items.Remove(selected);
                    lstTags.Items.Insert(0, selected);
                    lstTags.SetSelected(0, true);
                }
                else // to move the selected item downwards in the listbox
                {
                    lstTags.Items.Remove(selected);
                    lstTags.Items.Insert(index + 1, selected);
                    lstTags.SetSelected(index + 1, true);
                }
            }
        }


    }
}
