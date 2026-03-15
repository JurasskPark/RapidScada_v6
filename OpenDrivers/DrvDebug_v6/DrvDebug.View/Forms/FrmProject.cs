using ProjectDriver;
using Scada.Comm.Channels;
using Scada.Comm.Config;
using Scada.Comm.Drivers.DrvDebug.View.Forms.GroupTag;
using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvDebug.View.Forms
{
    /// <summary>
    /// Displays the device configuration form.
    /// <para>Отображает форму настройки устройства.</para>
    /// </summary>
    public partial class FrmProject : Form
    {
        private const string DictionaryKey = "Scada.Comm.Drivers.DrvDebug.View.Forms.FrmProject";
        private readonly AppDirs? appDirs;
        private readonly DeviceConfig? deviceConfig;
        private readonly int deviceNum;
        private readonly string projectFileName;
        private readonly Project project;
        private readonly BindingList<ProjectCommand> commands;
        private readonly string languageDir;
        
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmProject()
        {
            InitializeComponent();
            project = new Project();
            languageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lang");
            projectFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DriverUtils.GetFileName(0));
            commands = new BindingList<ProjectCommand>();
        }

        /// <summary>
        /// Initializes a new instance of the class for the specified device.
        /// </summary>
        public FrmProject(AppDirs appDirs, LineConfig lineConfig, DeviceConfig deviceConfig, int deviceNum)
            : this()
        {
            this.appDirs = appDirs;
            this.deviceConfig = deviceConfig;
            this.deviceNum = deviceNum;
            languageDir = appDirs.LangDir;
            projectFileName = Path.Combine(appDirs.ConfigDir, DriverUtils.GetFileName(deviceNum));
        }

        /// <summary>
        /// Handles form loading.
        /// </summary>
        private void FrmProject_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadLanguage(languageDir, Locale.IsRussian);
            Translate();
            Text = string.Format(GetPhrase("Title.DeviceFormat", "DrvDebug [{0}]"), deviceNum);
            colCmdKind.DisplayMember = nameof(SelectionItem<CommandDataKind>.Text);
            colCmdKind.ValueMember = nameof(SelectionItem<CommandDataKind>.Value);
            colCmdKind.DataSource = CreateCommandKindItems();
            dgvCommands.AutoGenerateColumns = false;
            dgvCommands.DataSource = commands;
            cmbCheckFormat.DataSource = new object[]
            {
                TypeCode.Byte,
                TypeCode.UInt16,
                TypeCode.UInt32,
                TypeCode.UInt64
            };
            cmbLogType.DisplayMember = nameof(SelectionItem<Scada.Log.LogMessageType>.Text);
            cmbLogType.ValueMember = nameof(SelectionItem<Scada.Log.LogMessageType>.Value);
            cmbLogType.DataSource = CreateLogTypeItems();
            ControlsFromProject();
            btnSave.Enabled = false;
            RefreshTagsGrid();
        }

        /// <summary>
        /// Loads the project and initializes command and tag collections.
        /// </summary>
        private void LoadProject()
        {
            if (!project.Load(projectFileName, out string errMsg) && !string.IsNullOrWhiteSpace(errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            project.Commands ??= new System.Collections.Generic.List<ProjectCommand>();
            project.Tags ??= new System.Collections.Generic.List<ProjectTag>();

            commands.Clear();
            foreach (ProjectCommand command in project.Commands.OrderBy(c => c.Order))
            {
                commands.Add(command);
            }
        }

        /// <summary>
        /// Adds a new command.
        /// </summary>
        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            string commandPrefix = GetPhrase("Default.CommandNamePrefix", "Command");
            commands.Add(new ProjectCommand
            {
                Order = commands.Count,
                Name = $"{commandPrefix}_{commands.Count + 1}"
            });
            NormalizeCommands();
        }

        /// <summary>
        /// Deletes the selected command.
        /// </summary>
        private void btnDeleteCommand_Click(object sender, EventArgs e)
        {
            if (dgvCommands.CurrentRow?.DataBoundItem is ProjectCommand command)
            {
                commands.Remove(command);
                NormalizeCommands();
            }
        }

        /// <summary>
        /// Moves the selected command up.
        /// </summary>
        private void btnCommandUp_Click(object sender, EventArgs e)
        {
            MoveCommand(-1);
        }

        /// <summary>
        /// Moves the selected command down.
        /// </summary>
        private void btnCommandDown_Click(object sender, EventArgs e)
        {
            MoveCommand(1);
        }

        /// <summary>
        /// Opens the tag editor dialog.
        /// </summary>
        private void btnEditTags_Click(object sender, EventArgs e)
        {
            using FrmGroupTag dialog = new FrmGroupTag(project.Tags);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                project.Tags = dialog.Tags.OrderBy(t => t.Order).ToList();
                RefreshTagsGrid();
                SetModified();
            }
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToProject();
            if (!project.Save(projectFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
                return;
            }

            btnSave.Enabled = false;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Marks the form as modified when a control changes.
        /// </summary>
        private void controls_Changed(object sender, EventArgs e)
        {
            SetModified();
        }

        /// <summary>
        /// Handles stop condition mode changes.
        /// </summary>
        private void StopConditionMode_Changed(object sender, EventArgs e)
        {
            UpdateStopConditionControls();
            SetModified();
        }

        /// <summary>
        /// Handles stop condition format changes.
        /// </summary>
        private void cmbCheckFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetModified();
        }

        /// <summary>
        /// Handles command cell value changes.
        /// </summary>
        private void dgvCommands_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            NormalizeCommands();
        }

        /// <summary>
        /// Commits edits when the current command cell is dirty.
        /// </summary>
        private void dgvCommands_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvCommands.IsCurrentCellDirty)
            {
                dgvCommands.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// Copies control values to the project.
        /// </summary>
        private void ControlsToProject()
        {
            NormalizeCommands();
            project.Commands = commands.ToList();
            project.TypeChannel = rdbMaster.Checked
                ? ChannelBehavior.Master
                : rdbSlave.Checked
                    ? ChannelBehavior.Slave
                    : ChannelBehavior.Mixed;
            project.StopConditionCheckAddress = Decimal.ToInt32(nudCheckAddress.Value);
            project.StopConditionCheckLength = Decimal.ToInt32(nudCheckLength.Value);
            project.StopConditionCheckFormat = cmbCheckFormat.SelectedItem is TypeCode typeCode
                ? typeCode
                : TypeCode.Byte;
            project.StopConditionCheckValueText = txtCheckValue.Text.Trim();
            project.StopConditionLengthMode = rdbLengthMode.Checked;
            project.StopConditionLengthIncludesItself = chkLengthIncludesItself.Checked;
            project.WriteLogDriver = chkWriteLog.Checked;
            project.MessageTypeLogDriver = cmbLogType.SelectedValue is Scada.Log.LogMessageType messageType
                ? messageType
                : Scada.Log.LogMessageType.Action;
        }

        /// <summary>
        /// Copies project values to the controls.
        /// </summary>
        private void ControlsFromProject()
        {
            rdbMaster.Checked = project.TypeChannel == ChannelBehavior.Master;
            rdbSlave.Checked = project.TypeChannel == ChannelBehavior.Slave;
            rdbMixed.Checked = project.TypeChannel == ChannelBehavior.Mixed;
            if (!rdbMaster.Checked && !rdbSlave.Checked && !rdbMixed.Checked)
            {
                rdbMixed.Checked = true;
            }

            nudCheckAddress.Value = Math.Max(nudCheckAddress.Minimum,
                Math.Min(nudCheckAddress.Maximum, project.StopConditionCheckAddress));
            nudCheckLength.Value = Math.Max(nudCheckLength.Minimum,
                Math.Min(nudCheckLength.Maximum, Math.Max(1, project.StopConditionCheckLength)));
            cmbCheckFormat.SelectedItem = project.StopConditionCheckFormat;
            txtCheckValue.Text = project.StopConditionCheckValueText ?? "0";
            rdbLengthMode.Checked = project.StopConditionLengthMode;
            rdbMarkerMode.Checked = !project.StopConditionLengthMode;
            chkLengthIncludesItself.Checked = project.StopConditionLengthIncludesItself;
            chkWriteLog.Checked = project.WriteLogDriver;
            cmbLogType.SelectedValue = project.MessageTypeLogDriver;
            UpdateStopConditionControls();
        }

        /// <summary>
        /// Updates stop condition controls according to the selected mode.
        /// </summary>
        private void UpdateStopConditionControls()
        {
            bool isLengthMode = rdbLengthMode.Checked;
            txtCheckValue.Enabled = !isLengthMode;
            lblCheckValue.Enabled = !isLengthMode;
            chkLengthIncludesItself.Enabled = isLengthMode;
            lblMarkerHint.Visible = true;
            lblLengthHint.Visible = true;
            lblCheckAddress.Visible = !isLengthMode;
            lblCheckLength.Visible = !isLengthMode;
            lblCheckAddressMode.Visible = isLengthMode;
            lblCheckLengthMode.Visible = isLengthMode;
        }

        /// <summary>
        /// Refreshes the tag preview grid.
        /// </summary>
        private void RefreshTagsGrid()
        {
            dgvTags.Rows.Clear();
            project.Tags ??= new System.Collections.Generic.List<ProjectTag>();
            foreach (ProjectTag tag in project.Tags.OrderBy(t => t.Order))
            {
                string preview = string.IsNullOrWhiteSpace(tag.TestBytesHex)
                    ? ProjectTagCodec.GetSimulationPreview(tag)
                    : tag.DecodeTestValueText();
                dgvTags.Rows.Add(tag.Name, tag.Channel, GetTagModeText(tag.Mode), tag.ArrayIndex, tag.DataLength, tag.DataFormat, GetSimulationKindText(tag.SimulationKind), preview);
            }
        }

        /// <summary>
        /// Moves the selected command by the specified shift.
        /// </summary>
        private void MoveCommand(int shift)
        {
            if (dgvCommands.CurrentRow?.DataBoundItem is not ProjectCommand command)
            {
                return;
            }

            int index = commands.IndexOf(command);
            int nextIndex = index + shift;
            if (index < 0 || nextIndex < 0 || nextIndex >= commands.Count)
            {
                return;
            }

            commands.RemoveAt(index);
            commands.Insert(nextIndex, command);
            NormalizeCommands();
            dgvCommands.ClearSelection();
            dgvCommands.Rows[nextIndex].Selected = true;
            if (dgvCommands.Rows[nextIndex].Cells.Count > 0)
            {
                dgvCommands.CurrentCell = dgvCommands.Rows[nextIndex].Cells[0];
            }
        }

        /// <summary>
        /// Normalizes the command list.
        /// </summary>
        private void NormalizeCommands()
        {
            if (commands == null)
            {
                return;
            }

            for (int i = 0; i < commands.Count; i++)
            {
                commands[i] ??= new ProjectCommand();
                commands[i].Normalize(i);
            }

            dgvCommands?.Refresh();
            SetModified();
        }

        /// <summary>
        /// Marks the form as modified.
        /// </summary>
        private void SetModified()
        {
            btnSave.Enabled = true;
        }

        /// <summary>
        /// Loads the driver translation.
        /// </summary>
        private void LoadLanguage(string languageDir, bool isRussian = false)
        {
            string culture = isRussian ? "ru-RU" : "en-GB";
            string fileName = $"{DriverUtils.DriverCode}.{culture}.xml";
            string languageFile = Path.Combine(languageDir, fileName);

            if (!File.Exists(languageFile))
            {
                languageFile = Path.Combine(languageDir, "Lang", fileName);
            }

            if (!Locale.LoadDictionaries(languageFile, out string errMsg) && errMsg != string.Empty)
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            Locale.GetDictionary("Scada.Comm.Drivers.DrvDebug.View.Forms.FrmProject");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDebug.View.Forms.GroupTag.FrmGroupTag");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDebug.View.Forms.Tag.FrmTag");
        }

        /// <summary>
        /// Translates the form.
        /// </summary>
        private void Translate()
        {
            SetLDataGriedViewColumnNames();

            CommPhrases.Init();

            // Translate the form.
            FormTranslator.Translate(this, GetType().FullName);

            // Translate DataGridView controls if supported by the framework.
            FormTranslator.Translate(dgvCommands, GetType().FullName);
            FormTranslator.Translate(dgvTags, GetType().FullName);

            // Apply column captions explicitly because FormTranslator does not always
            // update DataGridView header text for runtime-bound grids.
            TranslateGridColumns();
        }

        /// <summary>
        /// Sets the column names as needed for translation.
        /// </summary>
        private void SetLDataGriedViewColumnNames()
        {
            colCmdEnabled.Name = nameof(colCmdEnabled);
            colCmdName.Name = nameof(colCmdName);
            colCmdKind.Name = nameof(colCmdKind);
            colCmdPayload.Name = nameof(colCmdPayload);
            colCmdDelay.Name = nameof(colCmdDelay);
            colCmdNote.Name = nameof(colCmdNote);

            colTagName.Name = nameof(colTagName);
            colTagChannel.Name = nameof(colTagChannel);
            colTagMode.Name = nameof(colTagMode);
            colTagIndex.Name = nameof(colTagIndex);
            colTagLength.Name = nameof(colTagLength);
            colTagFormat.Name = nameof(colTagFormat);
            colTagSimulation.Name = nameof(colTagSimulation);
            colTagPreview.Name = nameof(colTagPreview);
        }

        /// <summary>
        /// Translates DataGridView column headers explicitly.
        /// </summary>
        private void TranslateGridColumns()
        {
            colCmdEnabled.HeaderText = GetPhrase("colCmdEnabled.HeaderText", colCmdEnabled.HeaderText);
            colCmdName.HeaderText = GetPhrase("colCmdName.HeaderText", colCmdName.HeaderText);
            colCmdKind.HeaderText = GetPhrase("colCmdKind.HeaderText", colCmdKind.HeaderText);
            colCmdPayload.HeaderText = GetPhrase("colCmdPayload.HeaderText", colCmdPayload.HeaderText);
            colCmdDelay.HeaderText = GetPhrase("colCmdDelay.HeaderText", colCmdDelay.HeaderText);
            colCmdNote.HeaderText = GetPhrase("colCmdNote.HeaderText", colCmdNote.HeaderText);

            colTagName.HeaderText = GetPhrase("colTagName.HeaderText", colTagName.HeaderText);
            colTagChannel.HeaderText = GetPhrase("colTagChannel.HeaderText", colTagChannel.HeaderText);
            colTagMode.HeaderText = GetPhrase("colTagMode.HeaderText", colTagMode.HeaderText);
            colTagIndex.HeaderText = GetPhrase("colTagIndex.HeaderText", colTagIndex.HeaderText);
            colTagLength.HeaderText = GetPhrase("colTagLength.HeaderText", colTagLength.HeaderText);
            colTagFormat.HeaderText = GetPhrase("colTagFormat.HeaderText", colTagFormat.HeaderText);
            colTagSimulation.HeaderText = GetPhrase("colTagSimulation.HeaderText", colTagSimulation.HeaderText);
            colTagPreview.HeaderText = GetPhrase("colTagPreview.HeaderText", colTagPreview.HeaderText);
        }

        /// <summary>
        /// Creates command kind items.
        /// </summary>
        private SelectionItem<CommandDataKind>[] CreateCommandKindItems()
        {
            return new[]
            {
                new SelectionItem<CommandDataKind> { Value = CommandDataKind.Hex, Text = GetPhrase("colCmdKind.Hex", "HEX") },
                new SelectionItem<CommandDataKind> { Value = CommandDataKind.Ascii, Text = GetPhrase("colCmdKind.Ascii", "ASCII") },
                new SelectionItem<CommandDataKind> { Value = CommandDataKind.Unicode, Text = GetPhrase("colCmdKind.Unicode", "Unicode") },
                new SelectionItem<CommandDataKind> { Value = CommandDataKind.Template, Text = GetPhrase("colCmdKind.Template", "Template") }
            };
        }

        /// <summary>
        /// Creates log type items.
        /// </summary>
        private SelectionItem<Scada.Log.LogMessageType>[] CreateLogTypeItems()
        {
            return new[]
            {
                new SelectionItem<Scada.Log.LogMessageType> { Value = Scada.Log.LogMessageType.Action, Text = GetPhrase("cmbLogType.Action", "Action") },
                new SelectionItem<Scada.Log.LogMessageType> { Value = Scada.Log.LogMessageType.Info, Text = GetPhrase("cmbLogType.Info", "Info") },
                new SelectionItem<Scada.Log.LogMessageType> { Value = Scada.Log.LogMessageType.Warning, Text = GetPhrase("cmbLogType.Warning", "Warning") },
                new SelectionItem<Scada.Log.LogMessageType> { Value = Scada.Log.LogMessageType.Error, Text = GetPhrase("cmbLogType.Error", "Error") }
            };
        }

        /// <summary>
        /// Gets localized tag mode text.
        /// </summary>
        private string GetTagModeText(TagMode mode)
        {
            return mode switch
            {
                TagMode.Decode => GetPhrase("tagMode.Decode", "Decode"),
                TagMode.Simulate => GetPhrase("tagMode.Simulate", "Simulate"),
                TagMode.DecodeAndSimulate => GetPhrase("tagMode.DecodeAndSimulate", "Decode and simulate"),
                _ => mode.ToString()
            };
        }

        /// <summary>
        /// Gets localized simulation kind text.
        /// </summary>
        private string GetSimulationKindText(SimulationKind simulationKind)
        {
            return simulationKind switch
            {
                SimulationKind.None => GetPhrase("simulationKind.None", "None"),
                SimulationKind.Ramp => GetPhrase("simulationKind.Ramp", "Ramp"),
                SimulationKind.Sawtooth => GetPhrase("simulationKind.Sawtooth", "Sawtooth"),
                SimulationKind.Sine => GetPhrase("simulationKind.Sine", "Sine"),
                SimulationKind.Square => GetPhrase("simulationKind.Square", "Square"),
                SimulationKind.StringList => GetPhrase("simulationKind.StringList", "String list"),
                SimulationKind.StringGenerate => GetPhrase("simulationKind.StringGenerate", "String generate"),
                _ => simulationKind.ToString()
            };
        }

        /// <summary>
        /// Gets a translated phrase or a default value.
        /// </summary>
        private static string GetPhrase(string key, string defaultValue)
        {
            string phrase = Locale.GetDictionary(DictionaryKey).GetPhrase(key);
            return string.IsNullOrEmpty(phrase) || phrase == key ? defaultValue : phrase;
        }
    }
}
