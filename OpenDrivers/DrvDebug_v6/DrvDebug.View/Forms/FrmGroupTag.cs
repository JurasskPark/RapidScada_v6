using ProjectDriver;
using Scada.Comm.Drivers.DrvDebug.View.Forms.Tag;
using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvDebug.View.Forms.GroupTag
{
    /// <summary>
    /// Displays the tag group editor form.
    /// <para>Отображает форму редактирования группы тегов.</para>
    /// </summary>
    public partial class FrmGroupTag : Form
    {
        private readonly BindingList<ProjectTag> tags;
        private readonly IList<ProjectTag> sourceTags;
        private readonly string languageDir;
        private const string DictionaryKey = "Scada.Comm.Drivers.DrvDebug.View.Forms.GroupTag.FrmGroupTag";

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmGroupTag(IList<ProjectTag> sourceTags)
        {
            this.sourceTags = sourceTags ?? new List<ProjectTag>();
            tags = new BindingList<ProjectTag>(this.sourceTags.Select(CloneTag).ToList());
            languageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lang");
            InitializeComponent();
        }

        /// <summary>
        /// Gets the edited tags.
        /// </summary>
        public IList<ProjectTag> Tags => tags;

        /// <summary>
        /// Handles form loading.
        /// </summary>
        private void FrmGroupTag_Load(object sender, EventArgs e)
        {
            LoadLanguage(languageDir, Locale.IsRussian);
            Translate();
            dgvTags.AutoGenerateColumns = false;
            dgvTags.DataSource = tags;
            dgvTags.CellFormatting += dgvTags_CellFormatting;
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

            Locale.GetDictionary("Scada.Comm.Drivers.DrvDebug.View.Forms.GroupTag.FrmGroupTag");
        }

        /// <summary>
        /// Translates the form.
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
            TranslateButtons();
            TranslateGridColumns();
        }

        /// <summary>
        /// Translates button captions explicitly.
        /// </summary>
        private void TranslateButtons()
        {
            btnAdd.Text = GetPhrase("btnAdd.Text", btnAdd.Text);
            btnEdit.Text = GetPhrase("btnEdit.Text", btnEdit.Text);
            btnDelete.Text = GetPhrase("btnDelete.Text", btnDelete.Text);
            btnUp.Text = GetPhrase("btnUp.Text", btnUp.Text);
            btnDown.Text = GetPhrase("btnDown.Text", btnDown.Text);
            btnOk.Text = GetPhrase("btnOk.Text", btnOk.Text);
            btnCancel.Text = GetPhrase("btnCancel.Text", btnCancel.Text);
        }

        /// <summary>
        /// Adds a new tag.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProjectTag tag = new ProjectTag { Order = tags.Count };
            using FrmTag dialog = new FrmTag(tag);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                tags.Add(tag);
                NormalizeOrders();
            }
        }

        /// <summary>
        /// Edits the selected tag.
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTags.CurrentRow?.DataBoundItem is not ProjectTag tag)
            {
                return;
            }

            using FrmTag dialog = new FrmTag(tag);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                dgvTags.Refresh();
            }
        }

        /// <summary>
        /// Deletes the selected tag.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTags.CurrentRow?.DataBoundItem is not ProjectTag tag)
            {
                return;
            }

            tags.Remove(tag);
            NormalizeOrders();
        }

        /// <summary>
        /// Moves the selected tag up.
        /// </summary>
        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveSelected(-1);
        }

        /// <summary>
        /// Moves the selected tag down.
        /// </summary>
        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveSelected(1);
        }

        /// <summary>
        /// Confirms tag changes.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            NormalizeOrders();
            sourceTags.Clear();
            foreach (ProjectTag tag in tags)
            {
                sourceTags.Add(CloneTag(tag));
            }

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
        /// Moves the selected tag by the specified shift.
        /// </summary>
        private void MoveSelected(int shift)
        {
            if (dgvTags.CurrentRow?.DataBoundItem is not ProjectTag tag)
            {
                return;
            }

            int index = tags.IndexOf(tag);
            int nextIndex = index + shift;
            if (index < 0 || nextIndex < 0 || nextIndex >= tags.Count)
            {
                return;
            }

            tags.RemoveAt(index);
            tags.Insert(nextIndex, tag);
            NormalizeOrders();
            dgvTags.ClearSelection();
            dgvTags.Rows[nextIndex].Selected = true;
            if (dgvTags.Rows[nextIndex].Cells.Count > 0)
            {
                dgvTags.CurrentCell = dgvTags.Rows[nextIndex].Cells[0];
            }
        }

        /// <summary>
        /// Normalizes tag order values.
        /// </summary>
        private void NormalizeOrders()
        {
            for (int i = 0; i < tags.Count; i++)
            {
                tags[i].Order = i;
                tags[i].Normalize(i);
            }

            dgvTags.Refresh();
        }

        /// <summary>
        /// Formats tag grid cells.
        /// </summary>
        private void dgvTags_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvTags.Rows[e.RowIndex].DataBoundItem is not ProjectTag tag)
            {
                return;
            }

            string columnName = dgvTags.Columns[e.ColumnIndex].Name;

            if (columnName == "colMode")
            {
                e.Value = GetTagModeText(tag.Mode);
                e.FormattingApplied = true;
            }
            else if (columnName == "colSimulation")
            {
                e.Value = GetSimulationKindText(tag.SimulationKind);
                e.FormattingApplied = true;
            }
            else if (columnName == "colPreview")
            {
                e.Value = string.IsNullOrWhiteSpace(tag.TestBytesHex)
                    ? ProjectTagCodec.GetSimulationPreview(tag)
                    : tag.DecodeTestValueText();
                e.FormattingApplied = true;
            }
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
        /// Translates DataGridView column headers explicitly.
        /// </summary>
        private void TranslateGridColumns()
        {
            if (colName != null)
                colName.HeaderText = GetPhrase("colName.HeaderText", colName.HeaderText);
            if (colChannel != null)
                colChannel.HeaderText = GetPhrase("colChannel.HeaderText", colChannel.HeaderText);
            if (colMode != null)
                colMode.HeaderText = GetPhrase("colMode.HeaderText", colMode.HeaderText);
            if (colIndex != null)
                colIndex.HeaderText = GetPhrase("colIndex.HeaderText", colIndex.HeaderText);
            if (colLength != null)
                colLength.HeaderText = GetPhrase("colLength.HeaderText", colLength.HeaderText);
            if (colFormat != null)
                colFormat.HeaderText = GetPhrase("colFormat.HeaderText", colFormat.HeaderText);
            if (colSimulation != null)
                colSimulation.HeaderText = GetPhrase("colSimulation.HeaderText", colSimulation.HeaderText);
            if (colPreview != null)
                colPreview.HeaderText = GetPhrase("colPreview.HeaderText", colPreview.HeaderText);
        }

        /// <summary>
        /// Gets localized tag mode text.
        /// </summary>
        private static string GetTagModeText(TagMode mode)
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
        private static string GetSimulationKindText(SimulationKind simulationKind)
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
