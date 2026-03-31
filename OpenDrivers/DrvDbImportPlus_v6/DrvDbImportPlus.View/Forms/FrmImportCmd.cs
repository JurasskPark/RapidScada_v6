using FastColoredTextBoxNS;
using Scada.Forms;
using Scada.Lang;
using System;
using System.Reflection;
using static Scada.Comm.Drivers.DrvDbImportPlus.DriverTag;
using static System.Net.Mime.MediaTypeNames;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    /// <summary>
    /// Form for setting up commands for data import.
    /// <para>Форма настройки команд для импорта данных.</para>
    /// </summary>
    public partial class FrmImportCmd : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmImportCmd()
        {
            InitializeComponent();
        }

        #region Variables
        private bool modified;                                  // the configuration was modified
        public FrmProject formParent;                           // parent form
        public DrvDbImportPlusProject project;                  // the configuration
        public ImportCmd cmd = new ImportCmd();                 // the import commands
        public List<DriverTag> tags = new List<DriverTag>();    // the tags

        private ListViewItem selected;                          // selected row
        private int indexSelectRow = 0;                         // index select row
        public Guid idRow;                                      // id row
        public string nameRow;                                  // name row
        #endregion Variables

        #region Form
        /// <summary>
        /// Loading the form.
        /// </summary>
        private void FrmImportCmd_Load(object sender, EventArgs e)
        {
            Translate();
            ProjectToControls();
            SetupHotkeys();
        }

        /// <summary>
        /// Intercepting button presses on the form.
        /// </summary>
        private void FrmImportCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnExecuteSQLQuery.PerformClick();
            }
        }

        #endregion Form

        #region Modified
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
        #endregion Modified

        #region Project

        private void ProjectToControls()
        {
            if (cmd != null)
            {
                ckbEnabled.Checked = cmd.Enabled;
                nudCmdNum.Value = Convert.ToDecimal(cmd.CmdNum);
                txtCmdCode.Text = cmd.CmdCode;
                txtCmdName.Text = cmd.Name;
                txtCmdDescription.Text = cmd.Description;
                fctCmdQuery.Text = cmd.Query;

                if (cmd.IsColumnBased)
                {
                    rdbTagsBasedRequestedTableColumns.Checked = true;
                }
                else
                {
                    rdbTagsBasedRequestedTableRows.Checked = true;
                }



                tags = cmd.DeviceTags;
                ListTagsToListView();
            }
        }

        private void ListTagsToListView()
        {
            if (tags != null)
            {
                Type type = lstTags.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(lstTags, true, null);

                this.lstTags.BeginUpdate();
                this.lstTags.Items.Clear();

                foreach (var tmpTag in tags)
                {
                    this.lstTags.Items.Add(new ListViewItem()
                    {
                        Text = tmpTag.Name,
                        SubItems =
                            {
                                DriverUtils.NullToString(tmpTag.Code),
                                ListViewAsDisplayStringFormatTag(tmpTag.Format),
                                ListViewAsDisplayStringBoolean(tmpTag.Enabled),
                            }
                    }).Tag = tmpTag.Id;
                }

                this.lstTags.EndUpdate();
            }

            ListViewExtensions.ResizeColumn(lstTags, "clmName");
            ListViewExtensions.ResizeColumn(lstTags, "clmCode");
            ListViewExtensions.ResizeColumn(lstTags, "clmFormat");
            ListViewExtensions.ResizeColumnDefault(lstTags, "clmEnabled");
            
            try
            {
                if (indexSelectRow != null && indexSelectRow < lstTags.Items.Count)
                {
                    lstTags.EnsureVisible(indexSelectRow);
                    lstTags.TopItem = lstTags.Items[indexSelectRow];
                    lstTags.Focus();
                    lstTags.Items[indexSelectRow].Selected = true;
                    lstTags.Select();
                }
            }
            catch { }
        }

        private void ControlsToProject()
        {
            cmd.Enabled = ckbEnabled.Checked;
            cmd.CmdNum = Convert.ToInt32(nudCmdNum.Value);
            cmd.CmdCode = txtCmdCode.Text;
            cmd.Name = txtCmdName.Text;
            cmd.Description = txtCmdDescription.Text;
            cmd.Query = fctCmdQuery.Text;

            if (rdbTagsBasedRequestedTableColumns.Checked)
            {
                cmd.IsColumnBased = true;
            }
            else
            {
                cmd.IsColumnBased = false;
            }

                cmd.DeviceTags = tags;
        }

        #endregion Project

        #region Translate

        /// <summary>
        /// Sets the column names as needed for translation.
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmName.Name = nameof(clmName);
            clmCode.Name = nameof(clmCode);
            clmFormat.Name = nameof(clmFormat);
            clmEnabled.Name = nameof(clmEnabled);
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

            if ((int)formatTag == 3)
            {
                result = Locale.IsRussian ?
                       "Целое число" :
                       "Integer";
                return result;
            }

            if ((int)formatTag == 4)
            {
                result = Locale.IsRussian ?
                       "Логическое значение" :
                       "Boolean";
                return result;
            }

            return result;
        }

        /// <summary>
        /// Translating values to listview.
        /// </summary>
        public string ListViewAsDisplayStringBoolean(bool boolValue)
        {
            string result = string.Empty;
            if (boolValue == false)
            {
                result = Locale.IsRussian ?
                        "Отключен" :
                        "Disabled";
                return result;
            }

            if (boolValue == true)
            {
                result = Locale.IsRussian ?
                       "Включен" :
                       "Enabled";
                return result;
            }

            return result;
        }

        /// <summary>
        /// Doesn't show value if it's zero.
        /// </summary>
        public string ListViewNoDisplayStringZero(string zeroValue)
        {
            string result = string.Empty;
            if (zeroValue == "0")
            {
                return string.Empty;
            }
            else
            {
                return zeroValue;
            }
        }

        /// <summary>
        /// Translate form.
        /// </summary>
        private void Translate()
        {
            // column names
            SetListViewColumnNames();

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            // tranlaste the menu
            FormTranslator.Translate(cmnuMenuScriptQuery, GetType().FullName);
            FormTranslator.Translate(cmnuMenuListTags, GetType().FullName);
        }

        #endregion Translate

        #region Control

        #region DataGridView
        /// <summary>
        /// Hiding errors in data processing.
        /// </summary>
        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }

        #endregion DataGridView

        #region FastColorTextBox

        /// <summary>
        /// Hotkeys Mapping.
        /// </summary>
        private void SetupHotkeys()
        {
            fctCmdQuery.HotkeysMapping.Clear();

            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.C] = FCTBAction.Copy;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.X] = FCTBAction.Cut;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.V] = FCTBAction.Paste;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.A] = FCTBAction.SelectAll;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.Z] = FCTBAction.Undo;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.Y] = FCTBAction.Redo;

            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.Insert] = FCTBAction.Copy;
            fctCmdQuery.HotkeysMapping[Keys.Shift | Keys.Insert] = FCTBAction.Paste;
            fctCmdQuery.HotkeysMapping[Keys.Shift | Keys.Delete] = FCTBAction.Cut;

            fctCmdQuery.HotkeysMapping[Keys.Up] = FCTBAction.GoUp;
            fctCmdQuery.HotkeysMapping[Keys.Down] = FCTBAction.GoDown;
            fctCmdQuery.HotkeysMapping[Keys.Left] = FCTBAction.GoLeft;
            fctCmdQuery.HotkeysMapping[Keys.Right] = FCTBAction.GoRight;

            fctResult.HotkeysMapping.Clear();

            fctResult.HotkeysMapping[Keys.Control | Keys.C] = FCTBAction.Copy;
            fctResult.HotkeysMapping[Keys.Control | Keys.X] = FCTBAction.Cut;
            fctResult.HotkeysMapping[Keys.Control | Keys.V] = FCTBAction.Paste;
            fctResult.HotkeysMapping[Keys.Control | Keys.A] = FCTBAction.SelectAll;
            fctResult.HotkeysMapping[Keys.Control | Keys.Z] = FCTBAction.Undo;
            fctResult.HotkeysMapping[Keys.Control | Keys.Y] = FCTBAction.Redo;

            fctResult.HotkeysMapping[Keys.Control | Keys.Insert] = FCTBAction.Copy;
            fctResult.HotkeysMapping[Keys.Shift | Keys.Insert] = FCTBAction.Paste;
            fctResult.HotkeysMapping[Keys.Shift | Keys.Delete] = FCTBAction.Cut;

            fctResult.HotkeysMapping[Keys.Up] = FCTBAction.GoUp;
            fctResult.HotkeysMapping[Keys.Down] = FCTBAction.GoDown;
            fctResult.HotkeysMapping[Keys.Left] = FCTBAction.GoLeft;
            fctResult.HotkeysMapping[Keys.Right] = FCTBAction.GoRight;
        }

        #endregion FastColorTextBox

        #region Button

        #region ExecuteSQLQuery
        /// <summary>
        /// Executing an SQL script.
        /// </summary>
        private async void btnExecuteSQLQuery_Click(object sender, EventArgs e)
        {
            try
            {
                ControlsToProject();
                btnExecuteSQLQuery.Enabled = false;
                await Task.Run(() => ExecuteSQLQuery());
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
            finally
            {
                btnExecuteSQLQuery.Enabled = true;
            }
        }

        #endregion ExecuteSQLQuery

        #region Save
        /// <summary>
        /// Saving settings.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToProject();

            DialogResult = DialogResult.OK;

            Close();
        }

        #endregion Save

        #region Close
        /// <summary>
        /// Close form.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            Close();
        }

        #endregion Close

        #endregion Button

        #endregion Control

        #region Execute
        /// <summary>
        /// Executing an SQL script.
        /// </summary>
        private void ExecuteSQLQuery()
        {
            dgvData.Invoke(new Action(() =>
            {
                dgvData.DataSource = null;
            }));

            fctResult.Invoke(new Action(() => fctResult.Text = string.Empty));

            DebugerReturn.OnDebug = new DebugerReturn.DebugData(PollLogGet);

            DriverClient driverClient = new DriverClient(formParent.pathProject, formParent.project, formParent.deviceNum, formParent.pathProject, false);
            var result = driverClient.Process(cmd);

            dgvData.Invoke(new Action(() =>
            {
                dgvData.DataSource = result;
            }));

            driverClient.Dispose();
        }

        /// <summary>
        /// Poll Log Get.
        /// </summary>
        private void PollLogGet(string text, bool writeDateTime = true)
        {
            try
            {
                if (!IsHandleCreated)
                {
                    this.CreateControl();
                }

                if (IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        fctResult.AppendText(text + Environment.NewLine);
                    });
                }
                else
                {
                    fctResult.AppendText(text + Environment.NewLine);
                }
            }
            catch { }
        }

        #endregion Execute

        #region Tags

        #region ListView Tags

        #region Select
        /// <summary>
        /// Mouse click on ListView
        /// </summary>
        private void lstTags_MouseClick(object sender, MouseEventArgs e)
        {
            TagSelect();
        }


        /// <summary>
        /// Mouse double click on ListView
        /// </summary>
        private void lstTags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TagSelect();
            TagChange();
        }

        /// <summary>
        /// Mouse click on ListView
        /// </summary>
        private void TagSelect()
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (tags != null)
            {
                try
                {
                    selected = tmplstTags.SelectedItems[0];
                    indexSelectRow = tmplstTags.SelectedIndices[0];
                    idRow = DriverUtils.StringToGuid(tmplstTags.SelectedItems[0].Tag.ToString());
                    nameRow = Convert.ToString(tmplstTags.SelectedItems[0].SubItems[1].Text.Trim());
                }
                catch { }
            }
        }

        #endregion Select

        #region Add
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
                DriverTag newTag = new DriverTag();
                newTag.Id = Guid.NewGuid();
                newTag.Format = FormatTag.Float;
                newTag.NumberDecimalPlaces = 3;
                newTag.Enabled = true;

                if (DialogResult.OK == new FrmTag(1, ref newTag).ShowDialog())
                {
                    tags.Add(newTag);
                    ListTagsToListView();
                }

                Modified = true;
            }
            catch { }
        }

        #endregion Add

        #region List Add
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
                        DriverTag newTag = new DriverTag();
                        newTag.Id = Guid.NewGuid();
                        newTag.Name = tagName;
                        newTag.Code = tagName;
                        newTag.NumberDecimalPlaces = 3;
                        newTag.Format = FormatTag.Float;
                        newTag.Enabled = true;

                        if (!tags.Contains(newTag))
                        {
                            tags.Add(newTag);
                            ProjectToControls();
                        }
                    }

                    Modified = true;
                }
            }
            catch { }
        }

        #endregion List Add

        #region Change

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

                if (tags != null)
                {
                    ListViewItem selected = tmplstTags.SelectedItems[0];
                    indexSelectRow = tmplstTags.SelectedIndices[0];
                    Guid selectId = DriverUtils.StringToGuid(selected.Tag.ToString());

                    DriverTag tmpTag = tags.Find((Predicate<DriverTag>)(r => r.Id == selectId));

                    FrmTag InputBox = new FrmTag(2, ref tmpTag);
                    InputBox.ShowDialog();

                    if (InputBox.DialogResult == DialogResult.OK)
                    {
                        // refresh
                        ProjectToControls();

                        Modified = true;
                        // scroll through
                        tmplstTags.EnsureVisible(indexSelectRow);
                        tmplstTags.TopItem = tmplstTags.Items[indexSelectRow];
                        // making the area active
                        tmplstTags.Focus();
                        // making the desired element selected
                        tmplstTags.Items[indexSelectRow].Selected = true;
                        tmplstTags.Select();
                    }
                }
            }
            catch { }
        }

        #endregion Change

        #region Delete

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

                if (tags != null)
                {
                    ListViewItem selected = tmplstTags.SelectedItems[0];
                    Guid selectId = DriverUtils.StringToGuid(selected.Tag.ToString());

                    DriverTag tmpTag = tags.Find((Predicate<DriverTag>)(r => r.Id == selectId));
                    indexSelectRow = tags.IndexOf(tags.Where(n => n.Id == selectId).FirstOrDefault());

                    try
                    {
                        if (tmplstTags.Items.Count > 0)
                        {
                            tags.Remove(tmpTag);
                            tmplstTags.Items.Remove(this.selected);
                        }

                        // refresh
                        ProjectToControls();

                        Modified = true;

                        if (indexSelectRow >= 1)
                        {
                            // scroll through
                            tmplstTags.EnsureVisible(indexSelectRow - 1);
                            tmplstTags.TopItem = tmplstTags.Items[indexSelectRow - 1];
                            // making the area active
                            tmplstTags.Focus();
                            // making the desired element selected
                            tmplstTags.Items[indexSelectRow - 1].Selected = true;
                            tmplstTags.Select();
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        #endregion Delete

        #region List Delete
        /// <summary>
        /// Tag list delete
        /// </summary>
        private void cmnuTagListDelete_Click(object sender, EventArgs e)
        {
            TagListDelete();
        }

        /// <summary>
        /// Tag list delete
        /// </summary>
        private void TagListDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstTags = this.lstTags;

                if (tags.Count > 0)
                {
                    tags.Clear();
                    tmplstTags.Items.Clear();

                    Modified = true;
                }
            }
            catch { }
        }

        #endregion  List Delete

        #region Up
        /// <summary>
        /// Tag Up
        /// </summary>
        private void cmnuUp_Click(object sender, EventArgs e)
        {
            TagUp();
        }

        private void TagUp()
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstTags.SelectedItems.Count > 0)
            {
                if (tags != null)
                {
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Up);

                    selected = tmplstTags.SelectedItems[0];
                    Guid selectId = DriverUtils.StringToGuid(selected.Tag.ToString());

                    DriverTag tmpTag = tags.Find((Predicate<DriverTag>)(r => r.Id == selectId));
                    indexSelectRow = tags.IndexOf(tags.Where(n => n.Id == selectId).FirstOrDefault());

                    if (indexSelectRow == 0)
                    {
                        return;
                    }
                    else
                    {
                        tags.Reverse(indexSelectRow - 1, 2);
                    }
                }

                Modified = true;
            }
        }
        #endregion Tag Up

        #region Down
        /// <summary>
        ///  Tag Down
        /// </summary>
        private void cmnuDown_Click(object sender, EventArgs e)
        {
            TagDown();
        }

        private void TagDown()
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstTags.SelectedItems.Count > 0)
            {
                if (tags != null)
                {
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Down);

                    selected = tmplstTags.SelectedItems[0];
                    Guid selectId = DriverUtils.StringToGuid(selected.Tag.ToString());

                    DriverTag tmpTag = tags.Find((Predicate<DriverTag>)(r => r.Id == selectId));
                    indexSelectRow = tags.IndexOf(tags.Where(n => n.Id == selectId).FirstOrDefault());

                    if (indexSelectRow == tags.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        tags.Reverse(indexSelectRow, 2);
                    }
                }

                Modified = true;
            }
        }

        #endregion Down

        #endregion ListView Tags

        #endregion Tags

        #region Menu Select

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuScriptQueryCut_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuScriptQueryCopy_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuScriptQueryPaste_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuScriptQuerySelectAll_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuScriptQueryUndo_Click(object sender, EventArgs e)
        {
            if (fctCmdQuery.UndoEnabled)
            {
                fctCmdQuery.Undo();
            }
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuScriptQueryRedo_Click(object sender, EventArgs e)
        {
            if (fctCmdQuery.RedoEnabled)
            {
                fctCmdQuery.Redo();
            }
        }

        #endregion Menu Select

        #region Show Error
        /// <summary>
        /// Displaying an error message
        /// </summary>
        private void ShowExceptionMessage(Exception ex)
        {
            MessageBox.Show(this, ex.InnerException != null ? ex.InnerException.Message : ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion Show Error
    }
}
