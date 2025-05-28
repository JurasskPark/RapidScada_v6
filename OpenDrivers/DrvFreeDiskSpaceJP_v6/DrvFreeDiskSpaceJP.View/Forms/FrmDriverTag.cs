using Scada.Forms;
using Scada.Lang;
using static Scada.Comm.Drivers.DrvFreeDiskSpaceJP.DriverTag;


namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    public partial class FrmDriverTag : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmDriverTag(ref DriverTag tag, int mode)
        {
            //this.Tag = tag;
            //this.Mode = mode;

            InitializeComponent();

            if (Tag == null)
            {
                return;
            }
        }

        #region Variables
        private int Mode;
        //private DriverTag Tag;
        #endregion Variables

        #region Form
        private void FrmTag_Load(object sender, EventArgs e)
        {
            ConfigToControls();
            Translate();
        }


        private void ConfigToControls()
        {
            //if (Tag != null)
            //{
            //    txtTagID.Text = Tag.TagID.ToString();
            //    ckbTagEnabled.Checked = Tag.TagEnabled;
            //    txtTagName.Text = Tag.TagName;
            //    txtTagCode.Text = Tag.TagCode;
            //    txtTagDescription.Text = Tag.TagDescription;
            //    txtTagAddressBlock.Text = Tag.TagAddressNumberBlock.ToString();
            //    txtTagAddressLine.Text = Tag.TagAddressNumberLine.ToString();
            //    txtTagAddressParameter.Text = Tag.TagAddressNumberParameter.ToString();

            //    try
            //    {
            //        cmbTagDataType.Items.AddRange(Enum.GetNames(typeof(DriverTag.FormatData)));
            //        if (Tag.TagFormatData != null)
            //        {
            //            cmbTagDataType.SelectedIndex = cmbTagDataType.FindString(Tag.TagFormatData.ToString());
            //        }
            //    }
            //    catch { }

            //    nudNumberOfDecimalPlaces.Value = Convert.ToDecimal(Tag.TagNumberDecimalPlaces);

            //    if (Tag.DeviceTagsBasedRequestedTableColumns)
            //    {
            //        rdbTagsBasedRequestedTableColumns.Checked = true;
            //    }
            //    else
            //    {
            //        rdbTagsBasedRequestedTableRows.Checked = true;
            //    }

            //    txtColumnNames.Text = Tag.ColumnNames;
            //    txtColumnNamesTag.Text = Tag.ColumnNameTag;
            //    txtColumnNamestValue.Text = Tag.ColumnNameValue;

            //    try
            //    {
            //        cmbValueFormat.Items.AddRange(Enum.GetNames(typeof(Tag.FormatTag)));
            //        if (Tag.ColumnNameValueFormat != null)
            //        {
            //            cmbValueFormat.SelectedIndex = cmbValueFormat.FindString(Tag.ColumnNameValueFormat.ToString());
            //        }
            //    }
            //    catch { }

            //    nudValueNumberOfDecimalPlaces.Value = Convert.ToDecimal(Tag.ColumnNameValueNumberDecimalPlaces);
            //    txtColumnNamesDateTime.Text = Tag.ColumnNameDatetime;
            //}
        }
        #endregion Form

        #region Lang
        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }
        #endregion Lang

        #region Button Save
        private void btnTagSave_Click(object sender, EventArgs e)
        {
            ControlsToConfig();

            DialogResult = DialogResult.OK;

            Close();
        }

        private void ControlsToConfig()
        {
            //if (Mode == 1)
            //{
            //    Tag.TagID = Guid.NewGuid();
            //}
            //else if (Mode == 2)
            //{
            //    Tag.TagID = DriverUtils.StringToGuid(txtTagID.Text);
            //}

            //Tag.TagEnabled = ckbTagEnabled.Checked;

            //Tag.TagAddressNumberBlock = txtTagAddressBlock.Text.Trim();
            //Tag.TagAddressNumberLine = txtTagAddressLine.Text.Trim();
            //Tag.TagAddressNumberParameter = txtTagAddressParameter.Text.Trim();
            //Tag.TagName = txtTagName.Text;
            //Tag.TagCode = txtTagCode.Text;
            //Tag.TagDescription = txtTagDescription.Text;
            //Tag.TagFormatData = (DriverTag.FormatData)Enum.Parse(typeof(DriverTag.FormatData), cmbTagDataType.Text);
            //Tag.TagNumberDecimalPlaces = Convert.ToInt32(nudNumberOfDecimalPlaces.Value);

            //if (rdbTagsBasedRequestedTableColumns.Checked)
            //{
            //    Tag.DeviceTagsBasedRequestedTableColumns = true;
            //}
            //else if (rdbTagsBasedRequestedTableRows.Checked)
            //{
            //    Tag.DeviceTagsBasedRequestedTableColumns = false;
            //}

            //Tag.ColumnNames = txtColumnNames.Text;
            //Tag.ColumnNameTag = txtColumnNamesTag.Text;
            //Tag.ColumnNameValue = txtColumnNamestValue.Text;

            //Tag.ColumnNameValueFormat = (Tag.FormatTag)Enum.Parse(typeof(Tag.FormatTag), cmbValueFormat.Text);

            //Tag.ColumnNameValueNumberDecimalPlaces = Convert.ToInt32(nudValueNumberOfDecimalPlaces.Value);
            //Tag.ColumnNameDatetime = txtColumnNamesDateTime.Text;
        }
        #endregion Button Save

        #region Button Cancel
        private void btn_TagCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
        #endregion Button Cancel

        #region Combobox DataType
        private void cmbTagDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FormatData value = (FormatData)cmbTagDataType.SelectedIndex;
            //lblNumberOfDecimalPlaces.Visible = true;
            //lblMaxNumberCharactersInWord.Visible = false;
            //nudNumberOfDecimalPlaces.Maximum = 16;
            ////this.Width = 479;
            ////tabControl.Width = 431;

            //switch (value)
            //{
            //    case FormatData.Float:
            //        nudNumberOfDecimalPlaces.Enabled = true;
            //        nudNumberOfDecimalPlaces.Visible = true;
            //        nudNumberOfDecimalPlaces.Value = 3;
            //        gbTableProperty.Visible = false;
            //        break;
            //    case FormatData.DateTime:
            //        nudNumberOfDecimalPlaces.Enabled = false;
            //        nudNumberOfDecimalPlaces.Visible = true;
            //        nudNumberOfDecimalPlaces.Value = 0;
            //        gbTableProperty.Visible = false;
            //        break;
            //    case FormatData.Integer:
            //        nudNumberOfDecimalPlaces.Enabled = false;
            //        nudNumberOfDecimalPlaces.Visible = true;
            //        nudNumberOfDecimalPlaces.Value = 0;
            //        gbTableProperty.Visible = false;
            //        break;
            //    case FormatData.Boolean:
            //        nudNumberOfDecimalPlaces.Enabled = false;
            //        nudNumberOfDecimalPlaces.Visible = true;
            //        nudNumberOfDecimalPlaces.Value = 0;
            //        gbTableProperty.Visible = false;
            //        break;
            //    case FormatData.String:
            //        lblNumberOfDecimalPlaces.Visible = false;
            //        nudNumberOfDecimalPlaces.Visible = true;
            //        lblMaxNumberCharactersInWord.Visible = true;
            //        nudNumberOfDecimalPlaces.Enabled = true;
            //        nudNumberOfDecimalPlaces.Value = 4;
            //        nudNumberOfDecimalPlaces.Maximum = 300;
            //        gbTableProperty.Visible = false;
            //        break;
            //    case FormatData.Table:
            //        //this.Width = 787;
            //        //tabControl.Width = 739;
            //        nudNumberOfDecimalPlaces.Enabled = false;
            //        nudNumberOfDecimalPlaces.Visible = false;
            //        lblNumberOfDecimalPlaces.Visible = false;
            //        lblNumberOfDecimalPlaces.Visible = false;
            //        gbTableProperty.Visible = true;
            //        break;
            //}
        }
        #endregion Combobox DataType

        #region Combobox ValueFormat
        private void cmbValueFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormatData value = (FormatData)cmbValueFormat.SelectedIndex;
            lblValueNumberOfDecimalPlaces.Visible = true;
            lblValueMaxNumberCharactersInWord.Visible = false;
            nudValueNumberOfDecimalPlaces.Maximum = 16;
            switch (value)
            {
                case FormatData.Float:
                    nudValueNumberOfDecimalPlaces.Enabled = true;
                    nudValueNumberOfDecimalPlaces.Value = 3;
                    break;
                case FormatData.DateTime:
                    nudValueNumberOfDecimalPlaces.Enabled = false;
                    nudValueNumberOfDecimalPlaces.Value = 0;
                    break;
                case FormatData.Integer:
                    nudValueNumberOfDecimalPlaces.Enabled = false;
                    nudValueNumberOfDecimalPlaces.Value = 0;
                    break;
                case FormatData.Boolean:
                    nudValueNumberOfDecimalPlaces.Enabled = false;
                    nudValueNumberOfDecimalPlaces.Value = 0;
                    break;
                case FormatData.String:
                    lblValueNumberOfDecimalPlaces.Visible = false;
                    lblValueMaxNumberCharactersInWord.Visible = true;
                    nudValueNumberOfDecimalPlaces.Enabled = true;
                    nudValueNumberOfDecimalPlaces.Value = 4;
                    nudValueNumberOfDecimalPlaces.Maximum = 300;
                    break;
            }
        }
        #endregion Combobox ValueFormat
    }
}
