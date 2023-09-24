
using FastColoredTextBoxNS;
using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System.Data;
using System.Data.Common;
using System.Reflection;
using static Scada.Comm.Drivers.DrvDbImportPlus.Tag;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    /// <summary>
    /// Device configuration form.
    /// <para>Форма настройки конфигурации КП.</para>
    /// </summary>
    public partial class FrmConfig : Form
    {

        #region Variables
        private readonly AppDirs appDirs;               // the application directories
        private readonly string driverCode;             // the driver code
        private readonly int deviceNum;                 // the device number
        private readonly DrvDbImportPlusConfig config;  // the device configuration
        private string configFileName;                  // the configuration file name
        private bool modified;                          // the configuration was modified
        private bool connChanging;                      // connection settings are changing
        private bool cmdSelecting;                      // a command is selecting

        private DataSource dataSource;                  // the data source
        private List<Tag> deviceTags;                   // tags
        public List<Tag> DeviceTags
        {
            get { return deviceTags; }
            set { deviceTags = value; }
        }

        private ListViewItem selected;           // selected record tag
        private int indexSelectTag = 0;                 // index number tag
        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        private FrmConfig()
        {
            InitializeComponent();
        }

        #region Basic
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
            deviceTags = new List<Tag>();
        }

        /// <summary>
        /// Loading the form
        /// </summary>
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

            // translate the listview
            FormTranslator.Translate(lstTags, GetType().FullName);

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
        /// Gets or sets a value indicating whether the configuration was modified.
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
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

            ckbWriteDriverLog.Checked = config.WriteLogDriver;

            if (config.DeviceTagsBasedRequestedTableColumns)
            {
                rdbKPTagsBasedRequestedTableColumns.Checked = true;
            }
            else
            {
                rdbKPTagsBasedRequestedTableRows.Checked = true;
            }

            SetListViewColumnNames();

            // tags
            DeviceTags = config.DeviceTags;

            if (DeviceTags != null)
            {
                // update without flicker
                Type type = lstTags.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(lstTags, true, null);

                this.lstTags.BeginUpdate();
                this.lstTags.Items.Clear();

                #region Data display

                foreach (var tmpTag in DeviceTags)
                {
                    // inserted information
                    this.lstTags.Items.Add(new ListViewItem()
                    {
                        // tag name
                        Text = tmpTag.TagName,
                        SubItems =
                            {
                                // adding tag parameters
                                DriverUtils.NullToString(tmpTag.TagCode),
                                DriverUtils.NullToString(ListViewAsDisplayStringFormatTag((FormatTag)tmpTag.TagFormat)),
                                DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tmpTag.TagEnabled))
                            }
                    }).Tag = tmpTag.TagID; // in tag we pass the tag id... so that we can find
                }
                #endregion Data display

                this.lstTags.EndUpdate();
            }

            try
            {
                if (indexSelectTag != 0 && indexSelectTag < lstTags.Items.Count)
                {
                    // scroll through
                    lstTags.EnsureVisible(indexSelectTag);
                    lstTags.TopItem = lstTags.Items[indexSelectTag];
                    // Making the area active
                    lstTags.Focus();
                    // making the desired element selected
                    lstTags.Items[indexSelectTag].Selected = true;
                    lstTags.Select();
                }
            }
            catch { }

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
            Modified = false;
        }

        /// <summary>
        /// Sets the column names as needed for translation.
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmTagname.Name = nameof(clmTagname);
            clmTagCode.Name = nameof(clmTagCode);
            clmTagFormat.Name = nameof(clmTagFormat);
            clmTagEnabled.Name = nameof(clmTagEnabled);
        }

        /// <summary>
        /// Translating values to listview.
        /// </summary>
        public string ListViewAsDisplayStringFormatTag(FormatTag formatTag)
        {
            string result = string.Empty;
            if ((int)formatTag == 0)
            {
                result = Locale.IsRussian ?
                        "Float" :
                        "Float";
                return result;
            }

            if ((int)formatTag == 1)
            {
                result = Locale.IsRussian ?
                       "Дата и время" :
                       "DateTime";
                return result;
            }

            if ((int)formatTag == 2)
            {
                result = Locale.IsRussian ?
                       "Строка" :
                       "String";
                return result;
            }

            return result;
        }

        /// <summary>
        /// Translating values to listview.
        /// </summary>
        public string ListViewAsDisplayStringBoolean(bool boolTag)
        {
            string result = string.Empty;
            if (boolTag == false)
            {
                result = Locale.IsRussian ?
                        "Отключен" :
                        "Disabled";
                return result;
            }

            if (boolTag == true)
            {
                result = Locale.IsRussian ?
                       "Включен" :
                       "Enabled";
                return result;
            }

            return result;
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

            config.WriteLogDriver = ckbWriteDriverLog.Checked;

            if (rdbKPTagsBasedRequestedTableColumns.Checked)
            {
                config.DeviceTagsBasedRequestedTableColumns = true;
            }
            else if (rdbKPTagsBasedRequestedTableRows.Checked)
            {
                config.DeviceTagsBasedRequestedTableColumns = false;
            }

            config.DeviceTags = DeviceTags;
        }

        /// <summary>
        /// Saving settings
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            // set the control values
            ConfigToControls();
        }

        /// <summary>
        /// Saving the settings by first getting the parameters from the controls, and then displaying
        /// </summary>
        private void Save()
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

        #endregion Basic

        #region Tab Database
        /// <summary>
        /// Choosing the type of DBMS connection method.
        /// </summary>
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
        /// Changing the textbox changes the connection string.
        /// </summary>
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
        /// Сhanges to the connection string will be displayed using the attribute.
        /// </summary>
        private void txtConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                EnableConnString();
                Modified = true;
            }
        }

        /// <summary>
        /// Сhecking the connection to the database
        /// </summary>
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
        #endregion Tab Database

        #region Tab Data Retrieval
        /// <summary>
        /// When changing the request, we indicate that a change has occurred.
        /// </summary>
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

        #endregion Tab Data Retrieval

        #region Tab Data
        /// <summary>
        /// Execution of a request.
        /// </summary>
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

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuSelectQueryCut_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuSelectQueryCopy_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuSelectQueryPaste_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuSelectQuerySelectAll_Click(object sender, EventArgs e)
        {
            txtSelectQuery.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuSelectQueryUndo_Click(object sender, EventArgs e)
        {
            if (txtSelectQuery.UndoEnabled)
            {
                txtSelectQuery.Undo();
            }
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuSelectQueryRedo_Click(object sender, EventArgs e)
        {
            if (txtSelectQuery.RedoEnabled)
            {
                txtSelectQuery.Redo();
            }
        }

        /// <summary>
        /// Error handling. Disabling error messages.
        /// </summary>
        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
        #endregion Tab Data

        #region Tab Commands
        /// <summary>
        /// Creating a command.
        /// </summary>
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

        /// <summary>
        /// Deleting a command.
        /// </summary>
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

        /// <summary>
        /// Specifying/changing the command number.
        /// </summary>
        private void numCmdNum_ValueChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.CmdNum = Convert.ToInt32(numCmdNum.Value);
                UpdateCommands();
                Modified = true;
            }
        }

        /// <summary>
        /// Specifying/changing the command code.
        /// </summary>
        private void txtCmdCode_TextChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.CmdCode = txtCmdCode.Text;
                UpdateCommandItem();
                Modified = true;
            }
        }

        /// <summary>
        /// Specifying/changing the command name.
        /// </summary>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.Name = txtName.Text;
                UpdateCommandItem();
                Modified = true;
            }
        }

        /// <summary>
        /// Specifying/changing the command query.
        /// </summary>
        private void txtCmdQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!cmdSelecting && cbCommand.SelectedItem is ExportCmd exportCmd)
            {
                exportCmd.Query = txtCmdQuery.Text;
                Modified = true;
            }
        }

        /// <summary>
        /// After selecting a command, you need to show its properties.
        /// </summary>
        private void cbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmdSelecting)
            {
                cmdSelecting = true;
                ShowCommandParams(cbCommand.SelectedItem as ExportCmd);
                cmdSelecting = false;
            }
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

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuCmdQueryCut_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuCmdQueryCopy_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuCmdQueryPaste_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuCmdQuerySelectAll_Click(object sender, EventArgs e)
        {
            txtCmdQuery.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuCmdQueryUndo_Click(object sender, EventArgs e)
        {
            if (txtCmdQuery.UndoEnabled)
            {
                txtCmdQuery.Undo();
            }
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuCmdQueryRedo_Click(object sender, EventArgs e)
        {
            if (txtCmdQuery.RedoEnabled)
            {
                txtCmdQuery.Redo();
            }
        }
        #endregion Tab Commands

        #region Tab Settings

        /// <summary>
        /// Write Driver Log
        /// </summary>
        private void ckbWriteDriverLog_CheckedChanged(object sender, EventArgs e)
        {
             Modified = true;
        }

        /// <summary>
        /// The first way to get data is when each tag is a separate column.
        /// </summary>
        private void rdbKPTagsBasedRequestedTableColumns_CheckedChanged(object sender, EventArgs e)
        {
             Modified = true;
        }

        /// <summary>
        /// The second way to get data is when each tag is a separate row.
        /// </summary>
        private void rdbKPTagsBasedRequestedTableRows_CheckedChanged(object sender, EventArgs e)
        {
             Modified = true;
        }

        #region Tag Refresh
        /// <summary>
        /// Tag Refresh
        /// </summary>
        private void cmnuTagRefresh_Click(object sender, EventArgs e)
        {
            ConfigToControls();
        }
        #endregion Tag Refresh

        #region Tag selection
        /// <summary>
        /// Tag selection
        /// </summary>
        private void lstTags_MouseClick(object sender, MouseEventArgs e)
        {
            TagSelect();
        }

        /// <summary>
        /// Tag selection
        /// </summary>
        private void TagSelect()
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (deviceTags != null)
            {
                ListViewItem selected = tmplstTags.SelectedItems[0];
                indexSelectTag = tmplstTags.SelectedIndices[0];
                Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                Tag tmpTag = deviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
            }
        }

        #endregion Tag selection

        #region Tag add
        /// <summary>
        /// Tag add
        /// </summary>
        private void cmnuTagAdd_Click(object sender, EventArgs e)
        {
            TagAdd();
        }

        /// <summary>
        /// Tag add
        /// </summary>
        private void TagAdd()
        {
            try
            {
                Tag newTag = new Tag();
                newTag.TagID = Guid.NewGuid();
                newTag.TagFormat = FormatTag.Float;
                newTag.TagEnabled = true;

                if (DialogResult.OK == new FrmTag(1, ref newTag).ShowDialog())
                {
                    DeviceTags.Add(newTag);
                    ConfigToControls();
                }

                Modified = true;
            }
            catch { }
        }

        #endregion Tag add

        #region Tag list add
        /// <summary>
        /// Tag list add
        /// </summary>
        private void cmnuListTagAdd_Click(object sender, EventArgs e)
        {
            ListTagAdd();
        }

        /// <summary>
        /// Tag list add
        /// </summary>
        private void ListTagAdd()
        {
            try
            {
                FrmInputBox InputBox = new FrmInputBox();
                InputBox.Values = string.Empty;
                InputBox.ShowDialog();

                if (InputBox.DialogResult == DialogResult.OK)
                {
                    String[] tagsName = InputBox.Values.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (String tagName in tagsName)
                    {
                        Tag newTag = new Tag();
                        newTag.TagID = Guid.NewGuid();
                        newTag.TagName = tagName;
                        newTag.TagCode = tagName;
                        newTag.TagFormat = FormatTag.Float;
                        newTag.TagEnabled = true;

                        if (!DeviceTags.Contains(newTag))
                        {
                            DeviceTags.Add(newTag);
                            ConfigToControls();
                        }
                    }

                    Modified = true;
                }
            }
            catch { }
        }

        #endregion Tag list add

        #region Tag change
        /// <summary>
        /// Tag change
        /// </summary>
        private void lstTags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TagChange();
        }

        /// <summary>
        /// Tag change
        /// </summary>
        private void cmnuTagChange_Click(object sender, EventArgs e)
        {
            TagChange();
        }

        /// <summary>
        /// Tag change
        /// </summary>
        private void TagChange()
        {
            try
            {
                System.Windows.Forms.ListView tmplstTags = this.lstTags;
                if (tmplstTags.SelectedItems.Count <= 0)
                {
                    return;
                }

                if (DeviceTags != null)
                {
                    ListViewItem selected = tmplstTags.SelectedItems[0];
                    indexSelectTag = tmplstTags.SelectedIndices[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = DeviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));

                    FrmTag InputBox = new FrmTag(2, ref tmpTag);
                    InputBox.ShowDialog();

                    if (InputBox.DialogResult == DialogResult.OK)
                    {
                        // refresh
                        ConfigToControls();

                        Modified = true;
                        // scroll through
                        tmplstTags.EnsureVisible(indexSelectTag);
                        tmplstTags.TopItem = tmplstTags.Items[indexSelectTag];
                        // making the area active
                        tmplstTags.Focus();
                        // making the desired element selected
                        tmplstTags.Items[indexSelectTag].Selected = true;
                        tmplstTags.Select();
                    }
                }
            }
            catch { }
        }

        #endregion Tag change

        #region Tag delete

        /// <summary>
        /// Tag delete
        /// </summary>
        private void cmnuTagDelete_Click(object sender, EventArgs e)
        {
            TagDelete();
        }

        /// <summary>
        /// Tag delete
        /// </summary>
        private void lstTags_KeyDown(object sender, KeyEventArgs e)
        {
            TagDelete();
        }

        /// <summary>
        /// Tag delete
        /// </summary>
        private void TagDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstTags = this.lstTags;
                if (tmplstTags.SelectedItems.Count <= 0)
                {
                    return;
                }

                if (DeviceTags != null)
                {
                    ListViewItem selected = tmplstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = DeviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
                    indexSelectTag = DeviceTags.IndexOf(deviceTags.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    try
                    {
                        if (tmplstTags.Items.Count > 0)
                        {
                            DeviceTags.Remove(tmpTag);
                            tmplstTags.Items.Remove(this.selected);
                        }

                        // refresh
                        ConfigToControls();

                        if (indexSelectTag >= 1)
                        {
                            tmplstTags.EnsureVisible(indexSelectTag - 1);
                            tmplstTags.TopItem = tmplstTags.Items[indexSelectTag - 1];

                            tmplstTags.Focus();

                            tmplstTags.Items[indexSelectTag - 1].Selected = true;
                            tmplstTags.Select();
                        }
                    }
                    catch { }
                }

                Modified = true;
            }
            catch { }
        }

        #endregion Tag delete

        #region Tag list delete
        /// <summary>
        /// Tag list delete
        /// </summary>
        private void cmnuTagAllDelete_Click(object sender, EventArgs e)
        {
            ListTagDelete();
        }

        /// <summary>
        /// Tag list delete
        /// </summary>
        private void ListTagDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstTags = this.lstTags;

                if (DeviceTags.Count > 0)
                {
                    DeviceTags.Clear();
                    tmplstTags.Items.Clear();

                    Modified = true;
                }
            }
            catch { }
        }

        #endregion Tag list delete

        #region Tag Up
        /// <summary>
        /// Tag Up
        /// </summary>
        private void cmnuUp_Click(object sender, EventArgs e)
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstTags.SelectedItems.Count > 0)
            {
                if (DeviceTags != null)
                {
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Up);

                    selected = tmplstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = DeviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
                    indexSelectTag = DeviceTags.IndexOf(deviceTags.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    if (indexSelectTag == 0)
                    {
                        return;
                    }
                    else
                    {
                        DeviceTags.Reverse(indexSelectTag - 1, 2);
                    }
                }

                Modified = true;
            }
        }
        #endregion Tag Up

        #region Tag Down
        /// <summary>
        ///  Tag Down
        /// </summary>
        private void cmnuDown_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            // an item must be selected
            if (lstTags.SelectedItems.Count > 0)
            {
                if (deviceTags != null)
                {
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Down);

                    selected = tmplstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = DeviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
                    indexSelectTag = DeviceTags.IndexOf(deviceTags.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    if (indexSelectTag == deviceTags.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        DeviceTags.Reverse(indexSelectTag, 2);
                    }
                }

                Modified = true;
            }
        }

        #endregion Tag Down

        #endregion Tab Settings

    }
}
