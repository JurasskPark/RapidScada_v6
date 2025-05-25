using Scada.Forms;
using Scada.Lang;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static Scada.Comm.Drivers.DrvFreeDiskSpaceJP.Tag;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    public partial class FrmTask : Form
    {
        public FrmTask()
        {
            InitializeComponent();
        }

        #region Variables
        public FrmConfig formParent;                            // parent form
        public Project project;                                 // the project configuration

        private bool modified;                                  // the configuration was modified

        public TaskSettings settings = new TaskSettings();

        List<DriverTag> listTag = new List<DriverTag>();
        public List<DriverTag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }

        private ListViewItem selectedItem;              // selected item
        public int indexSelect = 0;                     // num index
        public int sortingMethod = 0;                   // sorting method

        public int deviceNum;
        public int DeviceNum                            // device num
        {
            get { return deviceNum; }
            set { deviceNum = value; }
        }
        #endregion Variables

        #region Form
        private void FrmParser_Load(object sender, EventArgs e)
        {
            LoadData();
            Translate();
        }

        private void LoadData()
        {
            ConfigToControls();
        }

        private void ConfigToControls()
        {
            //txtID.Enabled = false;

            //if (settings != null)
            //{
            //    txtID.Text = settings.ID.ToString();
            //    ckbEnabled.Checked = settings.Enabled;
            //    txtName.Text = settings.Name.ToString();
            //    txtDescription.Text = settings.Description.ToString();
            //    txtPath.Text = settings.Path.ToString();

            //    ckbFilesAdd.Checked = settings.AddFiles;
            //    ckbFilesDelete.Checked = settings.DeleteFiles;

            //    ckbUseSubDir.Checked = settings.UseSubDir;
            //    ckbUseReadFromLastLine.Checked = settings.UseReadFromLastLine;
            //    ckbReadJustOneLastLine.Checked = settings.UseReadJustOneLastLine;

            //    txtFilter.Text = settings.Filter.ToString();
            //    txtTemplateFileName.Text = settings.TemplateFileName.ToString();

                //ListTag = settings.Settings.GroupTag.Group;

            //    ListToListView();

            //    DictonaryToListView();
            //    //DictonatyToolTipHelp();
            //}
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            DialogResult = DialogResult.OK;

            Close();
        }

        private void Save()
        {
            ControlsToConfig();
        }

        private void ControlsToConfig()
        {
            //settings.ID = DriverUtils.StringToGuid(txtID.Text);
            //settings.Enabled = ckbEnabled.Checked;
            //settings.Name = txtName.Text.Trim();
            //settings.Description = txtDescription.Text.Trim();
            //settings.Path = txtPath.Text.Trim();

            //settings.AddFiles = ckbFilesAdd.Checked;
            //settings.DeleteFiles = ckbFilesDelete.Checked;

            //settings.UseSubDir = ckbUseSubDir.Checked;
            //settings.UseReadFromLastLine = ckbUseReadFromLastLine.Checked;
            //settings.UseReadJustOneLastLine = ckbReadJustOneLastLine.Checked;

            //settings.Filter = txtFilter.Text.Trim();
            //settings.TemplateFileName = txtTemplateFileName.Text.Trim();

            //settings.Settings.GroupTag.Group = ListTag;
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            Close();
        }
        #endregion Form

        #region Lang

        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            SetListViewColumnNames();

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            // tranlaste the menu
            //FormTranslator.Translate(cmnuTagEdit, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuContent, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuResult, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuDictonary, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptSelect, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptDelete, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptInsert, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptRename, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptSynchronization, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptUpdate, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptParserSelect, GetType().FullName);
            //FormTranslator.Translate(cmnuMenuScriptParserInsert, GetType().FullName);
            //// translate the listview
            //FormTranslator.Translate(lstTags, GetType().FullName);
            //FormTranslator.Translate(lstDictonary, GetType().FullName);
        }

        /// <summary>
        /// Sets the column names as needed for translation.
        /// </summary>
        private void SetListViewColumnNames()
        {
            //clmAddressBlock.Name = nameof(clmAddressBlock);
            //clmAddressLine.Name = nameof(clmAddressLine);
            //clmAddressParameter.Name = nameof(clmAddressParameter);
            //clmFormat.Name = nameof(clmFormat);
            //clmTagDataValue.Name = nameof(clmTagDataValue);
            //clmTagDescription.Name = nameof(clmTagDescription);
            //clmTagName.Name = nameof(clmTagName);
            //clmVariable.Name = nameof(clmVariable);
            //clmName.Name = nameof(clmName);
            //clmValue.Name = nameof(clmValue);
            //clmDescription.Name = nameof(clmDescription);
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

        #endregion Lang

        #region Control

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

        #endregion Control

        #region ListView

        private void ListToListView()
        {
            // tags
            if (ListTag != null)
            {
                //Обновление без мерцания
                Type type = lstTags.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(lstTags, true, null);

                this.lstTags.BeginUpdate();
                this.lstTags.Items.Clear();

                #region Отображение данные 
                foreach (var tmpTag in ListTag)
                {
                    //Вставили инфорамцию
                    this.lstTags.Items.Add(new ListViewItem()
                    {
                        //Название тега
                        Text = tmpTag.TagName,
                        SubItems =
                            {
                                //Добавляем параметры тега
                                DriverTag.TagToString(tmpTag.TagFormatData),
                                DriverTag.TagToString(tmpTag.TagAddressNumberBlock),
                                DriverTag.TagToString(tmpTag.TagAddressNumberLine),
                                DriverTag.TagToString(tmpTag.TagAddressNumberParameter),
                                DriverTag.TagToString(tmpTag.TagDescription),
                                DriverTag.TagToString(tmpTag.TagDataValue),
                            }
                    }).Tag = tmpTag.TagID; //В Tag передаём ID тега..., чтобы можно было найти
                }
                #endregion Отображение данные 

                this.lstTags.EndUpdate();
            }

            try
            {
                if (indexSelect != null && indexSelect < lstTags.Items.Count)
                {
                    //Прокручиваем
                    lstTags.EnsureVisible(indexSelect);
                    lstTags.TopItem = lstTags.Items[indexSelect];
                    //Делаем область активной
                    lstTags.Focus();
                    //Делаю нужный элемент выбранным
                    lstTags.Items[indexSelect].Selected = true;
                    lstTags.Select();
                }
            }
            catch { }
        }


        private void DictonaryToListView()
        {
            // tags
            //if (ListDictonary != null)
            //{
            //    //Обновление без мерцания
            //    Type type = lstDictonary.GetType();
            //    PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            //    propertyInfo.SetValue(lstDictonary, true, null);

            //    this.lstDictonary.BeginUpdate();
            //    this.lstDictonary.Items.Clear();

            //    #region Отображение данные 
            //    foreach (var tmpVariable in ListDictonary)
            //    {
            //        //Вставили инфорамцию
            //        this.lstDictonary.Items.Add(new ListViewItem()
            //        {
            //            //Название тега
            //            Text = tmpVariable.Variable,
            //            SubItems =
            //                {
            //                    //Добавляем параметры тега
            //                    DriverUtils.NullToString(tmpVariable.VariableName),
            //                    DriverUtils.NullToString(tmpVariable.VariableValue),
            //                    DriverUtils.NullToString(tmpVariable.VariableDescription),
            //                }
            //        }).Tag = tmpVariable.Variable; //В Tag передаём ID тега..., чтобы можно было найти
            //    }
            //    #endregion Отображение данные 

            //    this.lstDictonary.EndUpdate();
            //}
        }

        #endregion ListView

        #region Menu Select

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuScriptSelectCut_Click(object sender, EventArgs e)
        {
            //fctScriptSelect.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuScriptSelectCopy_Click(object sender, EventArgs e)
        {
            //fctScriptSelect.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuScriptSelectPaste_Click(object sender, EventArgs e)
        {
            //fctScriptSelect.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuScriptSelectSelectAll_Click(object sender, EventArgs e)
        {
            //fctScriptSelect.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuScriptSelectUndo_Click(object sender, EventArgs e)
        {
            //if (fctScriptSelect.UndoEnabled)
            //{
            //    fctScriptSelect.Undo();
            //}
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuScriptSelectRedo_Click(object sender, EventArgs e)
        {
            //if (fctScriptSelect.RedoEnabled)
            //{
            //    fctScriptSelect.Redo();
            //}
        }

        #endregion Menu Select

        #region Menu Insert

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuScriptInsertCut_Click(object sender, EventArgs e)
        {
            //fctScriptInsert.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuScriptInsertCopy_Click(object sender, EventArgs e)
        {
            //fctScriptInsert.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuScriptInsertPaste_Click(object sender, EventArgs e)
        {
            //fctScriptInsert.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuScriptInsertSelectAll_Click(object sender, EventArgs e)
        {
            //fctScriptInsert.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuScriptInsertUndo_Click(object sender, EventArgs e)
        {
            //if (fctScriptInsert.UndoEnabled)
            //{
            //    fctScriptInsert.Undo();
            //}
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuScriptInsertRedo_Click(object sender, EventArgs e)
        {
            //if (fctScriptInsert.RedoEnabled)
            //{
            //    fctScriptInsert.Redo();
            //}
        }

        #endregion Menu Insert

        #region Menu Update

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuScriptUpdateCut_Click(object sender, EventArgs e)
        {
            //fctScriptUpdate.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuScriptUpdateCopy_Click(object sender, EventArgs e)
        {
            //fctScriptUpdate.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuScriptUpdatePaste_Click(object sender, EventArgs e)
        {
            //fctScriptUpdate.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuScriptUpdateSelectAll_Click(object sender, EventArgs e)
        {
            //fctScriptUpdate.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuScriptUpdateUndo_Click(object sender, EventArgs e)
        {
            //if (fctScriptUpdate.UndoEnabled)
            //{
            //    fctScriptUpdate.Undo();
            //}
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuScriptUpdateRedo_Click(object sender, EventArgs e)
        {
            //if (fctScriptUpdate.RedoEnabled)
            //{
            //    fctScriptUpdate.Redo();
            //}
        }

        #endregion Menu Update

        #region Menu Delete

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuScriptDeleteCut_Click(object sender, EventArgs e)
        {
            //fctScriptDelete.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuScriptDeleteCopy_Click(object sender, EventArgs e)
        {
            //fctScriptDelete.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuScriptDeletePaste_Click(object sender, EventArgs e)
        {
            //fctScriptDelete.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuScriptDeleteSelectAll_Click(object sender, EventArgs e)
        {
            //fctScriptDelete.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuScriptDeleteUndo_Click(object sender, EventArgs e)
        {
            //if (fctScriptDelete.UndoEnabled)
            //{
            //    fctScriptDelete.Undo();
            //}
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuScriptDeleteRedo_Click(object sender, EventArgs e)
        {
            //if (fctScriptDelete.RedoEnabled)
            //{
            //    fctScriptDelete.Redo();
            //}
        }

        #endregion Menu Delete

        #region Menu Renamed

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuScriptRenameCut_Click(object sender, EventArgs e)
        {
            //fctScriptRename.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuScriptRenameCopy_Click(object sender, EventArgs e)
        {
            //fctScriptRename.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuScriptRenamePaste_Click(object sender, EventArgs e)
        {
            //fctScriptRename.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuScriptRenameSelectAll_Click(object sender, EventArgs e)
        {
            //fctScriptRename.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuScriptRenameUndo_Click(object sender, EventArgs e)
        {
            //if (fctScriptRename.UndoEnabled)
            //{
            //    fctScriptRename.Undo();
            //}
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuScriptRenameRedo_Click(object sender, EventArgs e)
        {
            //if (fctScriptRename.RedoEnabled)
            //{
            //    fctScriptRename.Redo();
            //}
        }


        #endregion Menu Renamed

        #region Tags

        #region Обновление тегов

        private void tol_TagRefresh_Click(object sender, EventArgs e)
        {
            ConfigToControls();
        }

        #endregion Обновление тегов

        #region Добавление тега

        private void tolTagAdd_Click(object sender, EventArgs e)
        {
            TagAdd();
        }

        private void TagAdd()
        {
            try
            {
                DriverTag newTag = new DriverTag();
                newTag.TagID = Guid.NewGuid();
                newTag.TagDescription = "";
                newTag.TagEnabled = true;
                newTag.TagDataValue = 0;
                newTag.TagAddressNumberBlock = string.Empty;
                newTag.TagAddressNumberLine = string.Empty;
                newTag.TagAddressNumberParameter = string.Empty;

                if (DialogResult.OK == new FrmDriverTag(ref newTag, 1).ShowDialog())
                {
                    ListTag.Add(newTag);
                }

                //Обновляем информацию
                ListToListView();
            }
            catch { }
        }

        #endregion Добавление тега

        #region Изменение тега

        private void tolTagChange_Click(object sender, EventArgs e)
        {
            TagChange();
        }

        private void lst_Tags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TagChange();
        }

        private void TagChange()
        {
            try
            {
                ListView lstTags = this.lstTags;
                if (lstTags.SelectedItems.Count <= 0)
                {
                    return;
                }

                if (ListTag != null)
                {
                    ListViewItem selectedIndex = lstTags.SelectedItems[0];
                    indexSelect = lstTags.SelectedIndices[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selectedIndex.Tag.ToString());

                    DriverTag tmpTag = ListTag.Find((Predicate<DriverTag>)(r => r.TagID == SelectTagID));
                    indexSelect = ListTag.IndexOf(tmpTag);

                    FrmDriverTag InputBox = new FrmDriverTag(ref tmpTag, 2);
                    InputBox.ShowDialog();

                    if (InputBox.DialogResult == DialogResult.OK)
                    {
                        ListTag[indexSelect] = tmpTag;
                        //Обновляем информацию
                        ListToListView();

                        //Прокручиваем
                        this.lstTags.EnsureVisible(indexSelect);
                        this.lstTags.TopItem = this.lstTags.Items[indexSelect];
                        //Делаем область активной
                        this.lstTags.Focus();
                        //Делаю нужный элемент выбранным
                        this.lstTags.Items[indexSelect].Selected = true;
                        this.lstTags.Select();
                    }
                }
            }
            catch { }
        }

        #endregion Изменение тега

        #region Удаление тега

        private void tolTagDelete_Click(object sender, EventArgs e)
        {
            TagDelete();
        }

        private void lstTags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                TagDelete();
            }
            else if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
            {
                TagUp();
            }
            else if (e.KeyCode == Keys.Down && e.Modifiers == Keys.Control)
            {
                TagDown();
            }

        }

        private void TagDelete()
        {
            try
            {
                ListView lstTags = this.lstTags;
                if (lstTags.SelectedItems.Count <= 0)
                {
                    return;
                }

                if (ListTag != null)
                {
                    ListViewItem selectedIndex = lstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selectedIndex.Tag.ToString());

                    DriverTag tmpTag = ListTag.Find((Predicate<DriverTag>)(r => r.TagID == SelectTagID));
                    indexSelect = ListTag.IndexOf(ListTag.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    try
                    {
                        if (lstTags.Items.Count > 0)
                        {
                            ListTag.Remove(tmpTag);
                            lstTags.Items.Remove(this.selectedItem);
                        }

                        //Обновляем информацию
                        ListToListView();

                        //Прокручиваем
                        this.lstTags.EnsureVisible(indexSelect - 1);
                        this.lstTags.TopItem = this.lstTags.Items[indexSelect - 1];
                        //Делаем область активной
                        this.lstTags.Focus();
                        //Делаю нужный элемент выбранным
                        this.lstTags.Items[indexSelect - 1].Selected = true;
                        this.lstTags.Select();
                    }
                    catch { }
                }
            }
            catch { }
        }

        #endregion Удаление тега

        #region Удаление всех тегов
        private void tolTagDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                ListTag.Clear();
                //Обновляем информацию
                ListToListView();
            }
            catch { }
        }
        #endregion Удаление всех тегов

        #region Выбор тега
        private void lstTags_MouseClick(object sender, MouseEventArgs e)
        {
            TagSelect();
        }

        private void TagSelect()
        {
            ListView lstTags = this.lstTags;
            if (lstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (ListTag != null)
            {
                ListViewItem selectedIndex = lstTags.SelectedItems[0];
                indexSelect = lstTags.SelectedIndices[0];
                Guid SelectTagID = DriverUtils.StringToGuid(selectedIndex.Tag.ToString());

                DriverTag tmpTag = ListTag.Find((Predicate<DriverTag>)(r => r.TagID == SelectTagID));
            }
        }
        #endregion Выбор тега

        #region Tag Up
        private void tolTagUp_Click(object sender, EventArgs e)
        {
            TagUp();
        }

        /// <summary>
        /// Tag Up
        /// </summary>
        private void TagUp()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstTags.SelectedItems.Count > 0)
            {
                if (ListTag != null)
                {
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Up);

                    ListViewItem selectedIndex = tmplstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selectedIndex.Tag.ToString());

                    DriverTag tmpTag = ListTag.Find((Predicate<DriverTag>)(r => r.TagID == SelectTagID));
                    indexSelect = ListTag.IndexOf(ListTag.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    if (indexSelect == 0)
                    {
                        return;
                    }
                    else
                    {
                        ListTag.Reverse(indexSelect - 1, 2);
                    }
                }

                Modified = true;
            }
        }
        #endregion Tag Up

        #region Tag Down
        private void tolTagDown_Click(object sender, EventArgs e)
        {
            TagDown();
        }

        /// <summary>
        ///  Tag Down
        /// </summary>
        private void TagDown()
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            // an item must be selected
            if (lstTags.SelectedItems.Count > 0)
            {
                if (ListTag != null)
                {
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Down);

                    ListViewItem selected = tmplstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    DriverTag tmpTag = ListTag.Find((Predicate<DriverTag>)(r => r.TagID == SelectTagID));
                    indexSelect = ListTag.IndexOf(ListTag.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    if (indexSelect == ListTag.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        ListTag.Reverse(indexSelect, 2);
                    }
                }

                Modified = true;
            }
        }

        #endregion Tag Down

        #endregion Tags

        #region Validate
        private void tolValidate_Click(object sender, EventArgs e)
        {
            //fctResult.Text = string.Empty;

            //GetTagValue();
            //Validate();

            //tabControlSettings.SelectedTab = tabControlSettings.TabPages["tabValidate"];
        }

        private void GetTagValue()
        {
            //string content = fctContent.Text;
            //string code = fctScriptParserInsert.Text;

            //ParserText parserText = new ParserText();
            //ParserListBlocks blocks = parserText.Parsing(content, settings.Settings);

            //for (int t = 0; t < ListTag.Count; t++)
            //{
            //    DriverTag parserTag = ListTag[t];
            //    DriverTag.GetValue(blocks, ref parserTag);

            //    string parserTagValue = DriverTag.TagToString(parserTag.TagDataValue, parserTag.TagFormatData);
            //    fctResult.Text += "/*" + parserTag.TagName + "=" + parserTagValue + "*/" + Environment.NewLine;
            //}

            //ListToListView();
            //lstTags.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void Validate()
        {

            //string content = fctContent.Text;
            //string code = fctScriptParserInsert.Text;

            //ParserText parserText = new ParserText();
            //ParserListBlocks blocks = parserText.Parsing(content, settings.Settings);

            //for (int t = 0; t < ListTag.Count; t++)
            //{
            //    DriverTag parserTag = ListTag[t];
            //    DriverTag.GetValue(blocks, ref parserTag);

            //    string parserTagValue = DriverTag.TagToString(parserTag.TagDataValue, parserTag.TagFormatData);
            //    ParserTextVariable parserTextVariable = ListDictonary.Find(t => t.VariableName == parserTag.TagName);
            //    if (parserTextVariable == null)
            //    {
            //        continue;
            //    }

            //    code = code.Replace(DriverTag.TagToString(parserTextVariable.Variable), parserTagValue);
            //}

            //for (int d = 0; d < ListDictonary.Count; d++)
            //{
            //    ParserTextVariable parserTextVariable = ListDictonary[d];
            //    if (parserTextVariable.VariableValue == string.Empty)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        code = code.Replace(parserTextVariable.Variable, parserTextVariable.VariableValue);
            //    }
            //}

            //fctResult.Text += code;
        }
        #endregion Validate

        #region Run
        private void cmnuRun_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void tolRun_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void Run()
        {
            //fctContent.Text = string.Empty;
            //fctResult.Text = string.Empty;

            //DebugerReturn.OnDebug = new DebugerReturn.DebugData(PollLogGet);
            //DriverTagReturn.OnDebug = new DriverTagReturn.DebugData(PollTagGet);

            //FileWatcherLite watcher = new FileWatcherLite(settings.Name, settings.Path, settings.AddFiles, settings.DeleteFiles, settings.UseSubDir, settings.UseReadFromLastLine, settings.UseReadJustOneLastLine, settings.Filter, settings.TemplateFileName, settings.ScriptSelect, settings.ScriptInsert, settings.ScriptUpdate, settings.ScriptDelete, settings.ScriptRename, settings.ScriptSynchronization, settings.ScriptParserSelect, settings.ScriptParserInsert, settings.Settings, settings.DictonaryVariable);
            //watcher.Process();
            //watcher.Dispose();
        }


        //private void PollTagGet(List<DriverTag> tags)
        //{
        //    for (int t = 0; t < tags.Count; t++)
        //    {
        //        string parserTagValue = DriverTag.TagToString(tags[t].TagDataValue, tags[t].TagFormatData);
        //        if (tags[t].TagFormatData != DriverTag.FormatData.Table)
        //        {
        //            fctResult.Text += tags[t].TagName + "=" + parserTagValue + Environment.NewLine;
        //        }
        //        else
        //        {
        //            fctResult.Text += parserTagValue + Environment.NewLine;
        //        }
        //    }
        //}

        //private void PollLogGet(string text)
        //{
        //    try
        //    {
        //        if (!IsHandleCreated)
        //        {
        //            this.CreateControl();
        //        }

        //        if (IsHandleCreated)
        //        {
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                fctResult.AppendText(text + Environment.NewLine);
        //            });
        //        }
        //        else
        //        {
        //            fctResult.AppendText(text + Environment.NewLine);
        //        }
        //    }
        //    catch { }
        //}

        #endregion Run

        #region CSV
        public static DataTable Default()
        {
            DataTable dtData = new DataTable();
            dtData.Clear();
            dtData.TableName = "dtData";

            for (int h = 0; h < mibHeader.Length; h++)
            {
                dtData.Columns.Add(mibHeader[h], typeof(string));
            }

            return dtData;
        }

        public static string[] mibHeader = new string[]
        {
            "TagID",
            "TagName",
            "TagCode",
            "TagAddressNumberBlock",
            "TagAddressNumberLine",
            "TagAddressNumberParameter",
            "TagDescription",
            "TagEnabled",
            "TagNumberDecimalPlaces",

            "DeviceTagsBasedRequestedTableColumns",
            "ColumnNames",
            "ColumnNameTag",
            "ColumnNameValue",
            "ColumnNameValueFormat",
            "ColumnNameValueNumberDecimalPlaces",
            "ColumnNameDatetime",
        };

        private void tolDataLoadFromCSV_Click(object sender, EventArgs e)
        {
            DataLoadCSV();
        }

        private void DataLoadCSV()
        {
            bool header = true;
            bool quotes = true;
            char separater = char.Parse(";");
            string encoding = "UTF8";

            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = DriverPhrases.TitleLoadCSV;
            OFD.Filter = DriverPhrases.FilterCSV;
            OFD.InitialDirectory = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath);

            if (OFD.ShowDialog() == DialogResult.OK && OFD.FileName != "")
            {
                string FileName = OFD.FileName;

                try
                {
                    var listMIB = FastCSV.ReadFile<DriverTag>
                        (
                            FileName,           // filename
                            header,             // has header
                            separater,          // delimiter
                            encoding,
                            quotes,
                            (o, c) =>           // to object function o : cars object, c : columns array read
                            {
                                o.TagID = DriverUtils.StringToGuid(c[0]);
                                o.TagName = c[1];
                                o.TagCode = c[2];
                                o.TagAddressNumberBlock = c[3];
                                o.TagAddressNumberLine = c[4];
                                o.TagAddressNumberParameter = c[5];
                                o.TagDescription = c[6];
                                o.TagEnabled = Convert.ToBoolean(c[7]);
                                o.TagNumberDecimalPlaces = Convert.ToInt32(c[8]);

                                o.DeviceTagsBasedRequestedTableColumns = Convert.ToBoolean(c[9]);
                                o.ColumnNames = c[10];
                                o.ColumnNameTag = c[11];
                                o.ColumnNameValue = c[12];
                                o.ColumnNameValueFormat = (FormatTag)Enum.Parse(typeof(FormatTag), c[13]); ;
                                o.ColumnNameValueNumberDecimalPlaces = Convert.ToInt32(c[14]);
                                o.ColumnNameDatetime = c[15];

                                // add to list
                                ListTag.Add(o);
                                return true;
                            }
                        );

                    LoadData();
                    Modified = true;
                }
                catch { }
            }
        }



        private void tolDataSaveInCSV_Click(object sender, EventArgs e)
        {
            DataSaveCSV();
        }
        private void DataSaveCSV()
        {
            bool header = true;
            bool quotes = true;
            char separater = char.Parse(";");
            string encoding = "UTF8";

            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = DriverPhrases.TitleSaveCSV;
            SFD.Filter = DriverPhrases.FilterCSV;
            SFD.InitialDirectory = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath);

            if (SFD.ShowDialog() == DialogResult.OK && SFD.FileName != "")
            {
                string FileName = SFD.FileName;

                FastCSV.WriteFile<DriverTag>(
                    FileName,
                    mibHeader,          // has header
                    separater,          // delimiter
                    encoding,
                    quotes,
                    ListTag,
                    (o, c) =>
                    {
                        c.Add(o.TagID);
                        c.Add(o.TagName);
                        c.Add(o.TagCode);
                        c.Add(o.TagAddressNumberBlock);
                        c.Add(o.TagAddressNumberLine);
                        c.Add(o.TagAddressNumberParameter);
                        c.Add(o.TagDescription);
                        c.Add(o.TagEnabled);
                        c.Add(o.TagNumberDecimalPlaces);
                        c.Add(o.DeviceTagsBasedRequestedTableColumns);
                        c.Add(o.ColumnNames);
                        c.Add(o.ColumnNameTag);
                        c.Add(o.ColumnNameValue);
                        c.Add(o.ColumnNameValueFormat);
                        c.Add(o.ColumnNameValueNumberDecimalPlaces);
                        c.Add(o.ColumnNameDatetime);
                    });
            }
        }

        #endregion CSV
    }
}
