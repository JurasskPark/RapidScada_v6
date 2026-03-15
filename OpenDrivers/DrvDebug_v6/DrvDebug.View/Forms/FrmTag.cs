using ProjectDriver;
using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvDebug.View.Forms.Tag
{
    /// <summary>
    /// Displays the tag editor form.
    /// <para>Отображает форму редактирования тега.</para>
    /// </summary>
    public partial class FrmTag : Form
    {
        private const string DictionaryKey = "Scada.Comm.Drivers.DrvDebug.View.Forms.Tag.FrmTag";
        private readonly ProjectTag sourceTag;
        private readonly ProjectTag editTag;
        private readonly string languageDir;
        private bool loadingControls;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmTag(ProjectTag tag)
        {
            sourceTag = tag ?? throw new ArgumentNullException(nameof(tag));
            editTag = CloneTag(tag);
            languageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lang");
            InitializeComponent();
        }

        /// <summary>
        /// Handles form loading.
        /// </summary>
        private void FrmTag_Load(object sender, EventArgs e)
        {
            loadingControls = true;
            LoadLanguage(languageDir, Locale.IsRussian);
            Translate();
            cmbMode.DisplayMember = nameof(SelectionItem<TagMode>.Text);
            cmbMode.ValueMember = nameof(SelectionItem<TagMode>.Value);
            cmbMode.DataSource = CreateTagModeItems();
            cmbFormat.DataSource = Enum.GetValues(typeof(TagDataFormat));
            cmbByteOrder.DisplayMember = nameof(SelectionItem<ByteOrder>.Text);
            cmbByteOrder.ValueMember = nameof(SelectionItem<ByteOrder>.Value);
            cmbByteOrder.DataSource = CreateByteOrderItems();
            cmbSimulationKind.DisplayMember = nameof(SelectionItem<SimulationKind>.Text);
            cmbSimulationKind.ValueMember = nameof(SelectionItem<SimulationKind>.Value);
            cmbSimulationKind.DataSource = CreateSimulationKindItems();
            cmbStringMode.DisplayMember = nameof(SelectionItem<StringSimulationMode>.Text);
            cmbStringMode.ValueMember = nameof(SelectionItem<StringSimulationMode>.Value);
            cmbStringMode.DataSource = CreateStringModeItems();
            loadingControls = false;
            ControlsFromTag();
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

            Locale.GetDictionary("Scada.Comm.Drivers.DrvDebug.View.Forms.Tag.FrmTag");
        }

        /// <summary>
        /// Translates the form.
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
            TranslateCaptions();
            TranslateTabs();
            TranslateButtons();
        }

        /// <summary>
        /// Translates labels and check boxes explicitly.
        /// </summary>
        private void TranslateCaptions()
        {
            lblName.Text = GetPhrase("lblName.Text", lblName.Text);
            chkEnabled.Text = GetPhrase("chkEnabled.Text", chkEnabled.Text);
            lblChannel.Text = GetPhrase("lblChannel.Text", lblChannel.Text);
            lblDescription.Text = GetPhrase("lblDescription.Text", lblDescription.Text);
            lblMode.Text = GetPhrase("lblMode.Text", lblMode.Text);
            lblIndex.Text = GetPhrase("lblIndex.Text", lblIndex.Text);
            lblLength.Text = GetPhrase("lblLength.Text", lblLength.Text);
            lblFormat.Text = GetPhrase("lblFormat.Text", lblFormat.Text);
            lblByteOrder.Text = GetPhrase("lblByteOrder.Text", lblByteOrder.Text);
            lblCoefficient.Text = GetPhrase("lblCoefficient.Text", lblCoefficient.Text);
            lblOffset.Text = GetPhrase("lblOffset.Text", lblOffset.Text);
            lblPrecision.Text = GetPhrase("lblPrecision.Text", lblPrecision.Text);
            lblUnits.Text = GetPhrase("lblUnits.Text", lblUnits.Text);
            lblTestBytes.Text = GetPhrase("lblTestBytes.Text", lblTestBytes.Text);
            chkSimulateOnDecodeError.Text = GetPhrase("chkSimulateOnDecodeError.Text", chkSimulateOnDecodeError.Text);
            lblDecodePreviewCaption.Text = GetPhrase("lblDecodePreviewCaption.Text", lblDecodePreviewCaption.Text);
            lblSimulationPreviewCaption.Text = GetPhrase("lblSimulationPreviewCaption.Text", lblSimulationPreviewCaption.Text);
            grpSimulation.Text = GetPhrase("grpSimulation.Text", grpSimulation.Text);
            lblSimulationKind.Text = GetPhrase("lblSimulationKind.Text", lblSimulationKind.Text);
            lblSimInterval.Text = GetPhrase("lblSimInterval.Text", lblSimInterval.Text);
            lblMin.Text = GetPhrase("lblMin.Text", lblMin.Text);
            lblMax.Text = GetPhrase("lblMax.Text", lblMax.Text);
            lblStartValue.Text = GetPhrase("lblStartValue.Text", lblStartValue.Text);
            lblStep.Text = GetPhrase("lblStep.Text", lblStep.Text);
            lblResetValue.Text = GetPhrase("lblResetValue.Text", lblResetValue.Text);
            lblAmplitude.Text = GetPhrase("lblAmplitude.Text", lblAmplitude.Text);
            lblBias.Text = GetPhrase("lblBias.Text", lblBias.Text);
            lblPeriod.Text = GetPhrase("lblPeriod.Text", lblPeriod.Text);
            lblPhase.Text = GetPhrase("lblPhase.Text", lblPhase.Text);
            lblLowValue.Text = GetPhrase("lblLowValue.Text", lblLowValue.Text);
            lblHighValue.Text = GetPhrase("lblHighValue.Text", lblHighValue.Text);
            lblDutyCycle.Text = GetPhrase("lblDutyCycle.Text", lblDutyCycle.Text);
            chkCycle.Text = GetPhrase("chkCycle.Text", chkCycle.Text);
            lblStringMode.Text = GetPhrase("lblStringMode.Text", lblStringMode.Text);
            lblStringValues.Text = GetPhrase("lblStringValues.Text", lblStringValues.Text);
            lblStringDelimiter.Text = GetPhrase("lblStringDelimiter.Text", lblStringDelimiter.Text);
            lblStringTemplate.Text = GetPhrase("lblStringTemplate.Text", lblStringTemplate.Text);
        }

        /// <summary>
        /// Translates tab captions explicitly.
        /// </summary>
        private void TranslateTabs()
        {
            tabGeneral.Text = GetPhrase("tabGeneral.Text", tabGeneral.Text);
            tabSimulation.Text = GetPhrase("tabSimulation.Text", tabSimulation.Text);
        }

        /// <summary>
        /// Translates button captions explicitly.
        /// </summary>
        private void TranslateButtons()
        {
            btnOk.Text = GetPhrase("btnOk.Text", btnOk.Text);
            btnCancel.Text = GetPhrase("btnCancel.Text", btnCancel.Text);
        }

        /// <summary>
        /// Creates tag mode items.
        /// </summary>
        private SelectionItem<TagMode>[] CreateTagModeItems()
        {
            return new[]
            {
                new SelectionItem<TagMode> { Value = TagMode.Decode, Text = GetPhrase("cmbMode.Decode", "Decode") },
                new SelectionItem<TagMode> { Value = TagMode.Simulate, Text = GetPhrase("cmbMode.Simulate", "Simulate") },
                new SelectionItem<TagMode> { Value = TagMode.DecodeAndSimulate, Text = GetPhrase("cmbMode.DecodeAndSimulate", "Decode and simulate") }
            };
        }

        /// <summary>
        /// Creates byte order items.
        /// </summary>
        private SelectionItem<ByteOrder>[] CreateByteOrderItems()
        {
            return new[]
            {
                new SelectionItem<ByteOrder> { Value = ByteOrder.BigEndian, Text = GetPhrase("cmbByteOrder.BigEndian", "0123 - Big endian") },
                new SelectionItem<ByteOrder> { Value = ByteOrder.LittleEndian, Text = GetPhrase("cmbByteOrder.LittleEndian", "3210 - Little endian") },
                new SelectionItem<ByteOrder> { Value = ByteOrder.Mixed1032, Text = GetPhrase("cmbByteOrder.Mixed1032", "1032 - Mixed endian") },
                new SelectionItem<ByteOrder> { Value = ByteOrder.Mixed2301, Text = GetPhrase("cmbByteOrder.Mixed2301", "2301 - Mixed endian") }
            };
        }

        /// <summary>
        /// Creates simulation kind items.
        /// </summary>
        private SelectionItem<SimulationKind>[] CreateSimulationKindItems()
        {
            return new[]
            {
                new SelectionItem<SimulationKind> { Value = SimulationKind.None, Text = GetPhrase("cmbSimulationKind.None", "None") },
                new SelectionItem<SimulationKind> { Value = SimulationKind.Ramp, Text = GetPhrase("cmbSimulationKind.Ramp", "Ramp") },
                new SelectionItem<SimulationKind> { Value = SimulationKind.Sawtooth, Text = GetPhrase("cmbSimulationKind.Sawtooth", "Sawtooth") },
                new SelectionItem<SimulationKind> { Value = SimulationKind.Sine, Text = GetPhrase("cmbSimulationKind.Sine", "Sine") },
                new SelectionItem<SimulationKind> { Value = SimulationKind.Square, Text = GetPhrase("cmbSimulationKind.Square", "Square") },
                new SelectionItem<SimulationKind> { Value = SimulationKind.StringList, Text = GetPhrase("cmbSimulationKind.StringList", "String list") },
                new SelectionItem<SimulationKind> { Value = SimulationKind.StringGenerate, Text = GetPhrase("cmbSimulationKind.StringGenerate", "String generate") }
            };
        }

        /// <summary>
        /// Creates string simulation mode items.
        /// </summary>
        private SelectionItem<StringSimulationMode>[] CreateStringModeItems()
        {
            return new[]
            {
                new SelectionItem<StringSimulationMode> { Value = StringSimulationMode.Enumerate, Text = GetPhrase("cmbStringMode.Enumerate", "Enumerate") },
                new SelectionItem<StringSimulationMode> { Value = StringSimulationMode.Template, Text = GetPhrase("cmbStringMode.Template", "Template") }
            };
        }

        /// <summary>
        /// Copies tag values to controls.
        /// </summary>
        private void ControlsFromTag()
        {
            loadingControls = true;
            txtName.Text = editTag.Name;
            chkEnabled.Checked = editTag.Enabled;
            nudChannel.Value = Math.Max(nudChannel.Minimum, Math.Min(nudChannel.Maximum, editTag.Channel));
            txtDescription.Text = editTag.Description;
            cmbMode.SelectedValue = editTag.Mode;
            nudIndex.Value = Math.Max(nudIndex.Minimum, Math.Min(nudIndex.Maximum, editTag.ArrayIndex));
            nudLength.Value = Math.Max(nudLength.Minimum, Math.Min(nudLength.Maximum, editTag.DataLength));
            cmbFormat.SelectedItem = editTag.DataFormat;
            cmbByteOrder.SelectedValue = editTag.ByteOrder;
            txtCoefficient.Text = editTag.Coefficient.ToString(CultureInfo.InvariantCulture);
            txtOffset.Text = editTag.Offset.ToString(CultureInfo.InvariantCulture);
            nudPrecision.Value = Math.Max(nudPrecision.Minimum, Math.Min(nudPrecision.Maximum, editTag.Precision));
            txtUnits.Text = editTag.Units;
            txtTestBytes.Text = editTag.TestBytesHex;
            chkSimulateOnDecodeError.Checked = editTag.SimulateOnDecodeError;
            cmbSimulationKind.SelectedValue = editTag.SimulationKind;
            nudSimInterval.Value = Math.Max(nudSimInterval.Minimum, Math.Min(nudSimInterval.Maximum, editTag.Simulation.UpdateIntervalMs));
            txtMin.Text = editTag.Simulation.Min.ToString(CultureInfo.InvariantCulture);
            txtMax.Text = editTag.Simulation.Max.ToString(CultureInfo.InvariantCulture);
            txtStartValue.Text = editTag.Simulation.StartValue.ToString(CultureInfo.InvariantCulture);
            txtStep.Text = editTag.Simulation.Step.ToString(CultureInfo.InvariantCulture);
            chkCycle.Checked = editTag.Simulation.Cycle;
            txtResetValue.Text = editTag.Simulation.ResetValue.ToString(CultureInfo.InvariantCulture);
            txtAmplitude.Text = editTag.Simulation.Amplitude.ToString(CultureInfo.InvariantCulture);
            txtBias.Text = editTag.Simulation.Bias.ToString(CultureInfo.InvariantCulture);
            txtPeriod.Text = editTag.Simulation.PeriodSeconds.ToString(CultureInfo.InvariantCulture);
            txtPhase.Text = editTag.Simulation.PhaseDegrees.ToString(CultureInfo.InvariantCulture);
            txtLowValue.Text = editTag.Simulation.LowValue.ToString(CultureInfo.InvariantCulture);
            txtHighValue.Text = editTag.Simulation.HighValue.ToString(CultureInfo.InvariantCulture);
            txtDutyCycle.Text = editTag.Simulation.DutyCyclePercent.ToString(CultureInfo.InvariantCulture);
            cmbStringMode.SelectedValue = editTag.Simulation.StringMode;
            txtStringValues.Text = editTag.Simulation.StringValues;
            txtStringDelimiter.Text = editTag.Simulation.StringDelimiter;
            txtStringTemplate.Text = editTag.Simulation.StringTemplate;

            RefreshPreview();
            UpdateSimulationVisibility();
            loadingControls = false;
        }

        /// <summary>
        /// Copies control values to the edited tag.
        /// </summary>
        private void ControlsToTag()
        {
            editTag.Name = txtName.Text.Trim();
            editTag.Enabled = chkEnabled.Checked;
            editTag.Channel = Decimal.ToInt32(nudChannel.Value);
            editTag.Description = txtDescription.Text.Trim();
            editTag.Mode = cmbMode.SelectedValue is TagMode mode ? mode : editTag.Mode;
            editTag.ArrayIndex = Decimal.ToInt32(nudIndex.Value);
            editTag.DataLength = Decimal.ToInt32(nudLength.Value);
            editTag.DataFormat = cmbFormat.SelectedItem is TagDataFormat format ? format : editTag.DataFormat;
            editTag.ByteOrder = cmbByteOrder.SelectedValue is ByteOrder byteOrder ? byteOrder : editTag.ByteOrder;
            editTag.Coefficient = ParseDouble(txtCoefficient.Text, 1.0);
            editTag.Offset = ParseDouble(txtOffset.Text, 0.0);
            editTag.Precision = Decimal.ToInt32(nudPrecision.Value);
            editTag.Units = txtUnits.Text.Trim();
            editTag.TestBytesHex = txtTestBytes.Text.Trim();
            editTag.SimulateOnDecodeError = chkSimulateOnDecodeError.Checked;
            editTag.SimulationKind = cmbSimulationKind.SelectedValue is SimulationKind simulationKind
                ? simulationKind
                : editTag.SimulationKind;
            editTag.Simulation.UpdateIntervalMs = Decimal.ToInt32(nudSimInterval.Value);
            editTag.Simulation.Min = ParseDouble(txtMin.Text, 0.0);
            editTag.Simulation.Max = ParseDouble(txtMax.Text, 100.0);
            editTag.Simulation.StartValue = ParseDouble(txtStartValue.Text, 0.0);
            editTag.Simulation.Step = ParseDouble(txtStep.Text, 1.0);
            editTag.Simulation.Cycle = chkCycle.Checked;
            editTag.Simulation.ResetValue = ParseDouble(txtResetValue.Text, 0.0);
            editTag.Simulation.Amplitude = ParseDouble(txtAmplitude.Text, 1.0);
            editTag.Simulation.Bias = ParseDouble(txtBias.Text, 0.0);
            editTag.Simulation.PeriodSeconds = ParseDouble(txtPeriod.Text, 60.0);
            editTag.Simulation.PhaseDegrees = ParseDouble(txtPhase.Text, 0.0);
            editTag.Simulation.LowValue = ParseDouble(txtLowValue.Text, 0.0);
            editTag.Simulation.HighValue = ParseDouble(txtHighValue.Text, 1.0);
            editTag.Simulation.DutyCyclePercent = ParseDouble(txtDutyCycle.Text, 50.0);
            editTag.Simulation.StringMode = cmbStringMode.SelectedValue is StringSimulationMode stringMode
                ? stringMode
                : editTag.Simulation.StringMode;
            editTag.Simulation.StringValues = txtStringValues.Text.Trim();
            editTag.Simulation.StringDelimiter = txtStringDelimiter.Text.Trim();
            editTag.Simulation.StringTemplate = txtStringTemplate.Text.Trim();
            editTag.Normalize(editTag.Order);
        }

        /// <summary>
        /// Confirms tag changes.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            ControlsToTag();
            if (string.IsNullOrWhiteSpace(editTag.Name))
            {
                MessageBox.Show(this, GetPhrase("Warning.TagNameRequired", "Tag name is required."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CopyTag(editTag, sourceTag);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancels tag changes.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Updates preview values when editor controls change.
        /// </summary>
        private void preview_Changed(object sender, EventArgs e)
        {
            if (loadingControls)
            {
                return;
            }

            ControlsToTag();
            RefreshPreview();
            UpdateSimulationVisibility();
        }

        /// <summary>
        /// Refreshes decode and simulation previews.
        /// </summary>
        private void RefreshPreview()
        {
            lblDecodePreview.Text = editTag.DecodeTestValueText();
            lblSimulationPreview.Text = ProjectTagCodec.GetSimulationPreview(editTag);
        }

        /// <summary>
        /// Updates simulation control visibility.
        /// </summary>
        private void UpdateSimulationVisibility()
        {
            bool simulationEnabled = editTag.Mode != TagMode.Decode;
            grpSimulation.Enabled = simulationEnabled;
            pnlStringSimulation.Enabled = simulationEnabled &&
                (editTag.SimulationKind == SimulationKind.StringList || editTag.SimulationKind == SimulationKind.StringGenerate);
        }

        /// <summary>
        /// Creates a copy of the specified tag.
        /// </summary>
        private static ProjectTag CloneTag(ProjectTag source)
        {
            return new ProjectTag
            {
                Id = source.Id,
                Order = source.Order,
                Enabled = source.Enabled,
                Name = source.Name,
                Channel = source.Channel,
                Description = source.Description,
                Mode = source.Mode,
                ArrayIndex = source.ArrayIndex,
                DataLength = source.DataLength,
                DataFormat = source.DataFormat,
                ByteOrder = source.ByteOrder,
                Coefficient = source.Coefficient,
                Offset = source.Offset,
                Precision = source.Precision,
                Units = source.Units,
                TestBytesHex = source.TestBytesHex,
                SimulateOnDecodeError = source.SimulateOnDecodeError,
                SimulationKind = source.SimulationKind,
                Simulation = new ProjectTagSimulationOptions
                {
                    UpdateIntervalMs = source.Simulation?.UpdateIntervalMs ?? 1000,
                    Min = source.Simulation?.Min ?? 0,
                    Max = source.Simulation?.Max ?? 100,
                    StartValue = source.Simulation?.StartValue ?? 0,
                    Step = source.Simulation?.Step ?? 1,
                    Cycle = source.Simulation?.Cycle ?? true,
                    ResetValue = source.Simulation?.ResetValue ?? 0,
                    Amplitude = source.Simulation?.Amplitude ?? 1,
                    Bias = source.Simulation?.Bias ?? 0,
                    PeriodSeconds = source.Simulation?.PeriodSeconds ?? 60,
                    PhaseDegrees = source.Simulation?.PhaseDegrees ?? 0,
                    LowValue = source.Simulation?.LowValue ?? 0,
                    HighValue = source.Simulation?.HighValue ?? 1,
                    DutyCyclePercent = source.Simulation?.DutyCyclePercent ?? 50,
                    StringMode = source.Simulation?.StringMode ?? StringSimulationMode.Enumerate,
                    StringValues = source.Simulation?.StringValues ?? string.Empty,
                    StringDelimiter = source.Simulation?.StringDelimiter ?? ";",
                    StringTemplate = source.Simulation?.StringTemplate ?? "TAG_{N}"
                }
            };
        }

        /// <summary>
        /// Copies tag values from the source to the target.
        /// </summary>
        private static void CopyTag(ProjectTag source, ProjectTag target)
        {
            target.Id = source.Id;
            target.Order = source.Order;
            target.Enabled = source.Enabled;
            target.Name = source.Name;
            target.Channel = source.Channel;
            target.Description = source.Description;
            target.Mode = source.Mode;
            target.ArrayIndex = source.ArrayIndex;
            target.DataLength = source.DataLength;
            target.DataFormat = source.DataFormat;
            target.ByteOrder = source.ByteOrder;
            target.Coefficient = source.Coefficient;
            target.Offset = source.Offset;
            target.Precision = source.Precision;
            target.Units = source.Units;
            target.TestBytesHex = source.TestBytesHex;
            target.SimulateOnDecodeError = source.SimulateOnDecodeError;
            target.SimulationKind = source.SimulationKind;
            target.Simulation = source.Simulation;
        }

        /// <summary>
        /// Parses a floating-point value using invariant culture.
        /// </summary>
        private static double ParseDouble(string text, double defaultValue)
        {
            return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                ? value
                : defaultValue;
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
