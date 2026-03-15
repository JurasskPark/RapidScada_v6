namespace Scada.Comm.Drivers.DrvDebug.View.Forms.Tag
{
    partial class FrmTag
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabMain = new TabControl();
            tabGeneral = new TabPage();
            lblSimulationPreviewCaption = new Label();
            lblDecodePreviewCaption = new Label();
            lblSimulationPreview = new Label();
            lblDecodePreview = new Label();
            txtTestBytes = new TextBox();
            chkSimulateOnDecodeError = new CheckBox();
            lblTestBytes = new Label();
            txtUnits = new TextBox();
            nudPrecision = new NumericUpDown();
            txtOffset = new TextBox();
            txtCoefficient = new TextBox();
            cmbByteOrder = new ComboBox();
            cmbFormat = new ComboBox();
            nudLength = new NumericUpDown();
            nudIndex = new NumericUpDown();
            cmbMode = new ComboBox();
            txtDescription = new TextBox();
            nudChannel = new NumericUpDown();
            chkEnabled = new CheckBox();
            txtName = new TextBox();
            lblUnits = new Label();
            lblPrecision = new Label();
            lblOffset = new Label();
            lblCoefficient = new Label();
            lblByteOrder = new Label();
            lblFormat = new Label();
            lblLength = new Label();
            lblIndex = new Label();
            lblMode = new Label();
            lblDescription = new Label();
            lblChannel = new Label();
            lblName = new Label();
            tabSimulation = new TabPage();
            pnlStringSimulation = new Panel();
            txtStringTemplate = new TextBox();
            txtStringDelimiter = new TextBox();
            txtStringValues = new TextBox();
            cmbStringMode = new ComboBox();
            lblStringTemplate = new Label();
            lblStringDelimiter = new Label();
            lblStringValues = new Label();
            lblStringMode = new Label();
            grpSimulation = new GroupBox();
            chkCycle = new CheckBox();
            nudSimInterval = new NumericUpDown();
            txtDutyCycle = new TextBox();
            txtHighValue = new TextBox();
            txtLowValue = new TextBox();
            txtPhase = new TextBox();
            txtPeriod = new TextBox();
            txtBias = new TextBox();
            txtAmplitude = new TextBox();
            txtResetValue = new TextBox();
            txtStep = new TextBox();
            txtStartValue = new TextBox();
            txtMax = new TextBox();
            txtMin = new TextBox();
            cmbSimulationKind = new ComboBox();
            lblDutyCycle = new Label();
            lblHighValue = new Label();
            lblLowValue = new Label();
            lblPhase = new Label();
            lblPeriod = new Label();
            lblBias = new Label();
            lblAmplitude = new Label();
            lblResetValue = new Label();
            lblStep = new Label();
            lblStartValue = new Label();
            lblMax = new Label();
            lblMin = new Label();
            lblSimInterval = new Label();
            lblSimulationKind = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            tabMain.SuspendLayout();
            tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecision).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudChannel).BeginInit();
            tabSimulation.SuspendLayout();
            pnlStringSimulation.SuspendLayout();
            grpSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSimInterval).BeginInit();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabMain.Controls.Add(tabGeneral);
            tabMain.Controls.Add(tabSimulation);
            tabMain.Location = new Point(12, 12);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(760, 476);
            tabMain.TabIndex = 2;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(lblSimulationPreviewCaption);
            tabGeneral.Controls.Add(lblDecodePreviewCaption);
            tabGeneral.Controls.Add(lblSimulationPreview);
            tabGeneral.Controls.Add(lblDecodePreview);
            tabGeneral.Controls.Add(txtTestBytes);
            tabGeneral.Controls.Add(chkSimulateOnDecodeError);
            tabGeneral.Controls.Add(lblTestBytes);
            tabGeneral.Controls.Add(txtUnits);
            tabGeneral.Controls.Add(nudPrecision);
            tabGeneral.Controls.Add(txtOffset);
            tabGeneral.Controls.Add(txtCoefficient);
            tabGeneral.Controls.Add(cmbByteOrder);
            tabGeneral.Controls.Add(cmbFormat);
            tabGeneral.Controls.Add(nudLength);
            tabGeneral.Controls.Add(nudIndex);
            tabGeneral.Controls.Add(cmbMode);
            tabGeneral.Controls.Add(txtDescription);
            tabGeneral.Controls.Add(nudChannel);
            tabGeneral.Controls.Add(chkEnabled);
            tabGeneral.Controls.Add(txtName);
            tabGeneral.Controls.Add(lblUnits);
            tabGeneral.Controls.Add(lblPrecision);
            tabGeneral.Controls.Add(lblOffset);
            tabGeneral.Controls.Add(lblCoefficient);
            tabGeneral.Controls.Add(lblByteOrder);
            tabGeneral.Controls.Add(lblFormat);
            tabGeneral.Controls.Add(lblLength);
            tabGeneral.Controls.Add(lblIndex);
            tabGeneral.Controls.Add(lblMode);
            tabGeneral.Controls.Add(lblDescription);
            tabGeneral.Controls.Add(lblChannel);
            tabGeneral.Controls.Add(lblName);
            tabGeneral.Location = new Point(4, 24);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Size = new Size(752, 448);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            // 
            // lblSimulationPreviewCaption
            // 
            lblSimulationPreviewCaption.AutoSize = true;
            lblSimulationPreviewCaption.Location = new Point(16, 369);
            lblSimulationPreviewCaption.Name = "lblSimulationPreviewCaption";
            lblSimulationPreviewCaption.Size = new Size(108, 15);
            lblSimulationPreviewCaption.TabIndex = 0;
            lblSimulationPreviewCaption.Text = "Simulation preview";
            // 
            // lblDecodePreviewCaption
            // 
            lblDecodePreviewCaption.AutoSize = true;
            lblDecodePreviewCaption.Location = new Point(16, 333);
            lblDecodePreviewCaption.Name = "lblDecodePreviewCaption";
            lblDecodePreviewCaption.Size = new Size(91, 15);
            lblDecodePreviewCaption.TabIndex = 1;
            lblDecodePreviewCaption.Text = "Decode preview";
            // 
            // lblSimulationPreview
            // 
            lblSimulationPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSimulationPreview.BorderStyle = BorderStyle.FixedSingle;
            lblSimulationPreview.Location = new Point(230, 362);
            lblSimulationPreview.Name = "lblSimulationPreview";
            lblSimulationPreview.Size = new Size(507, 28);
            lblSimulationPreview.TabIndex = 2;
            lblSimulationPreview.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDecodePreview
            // 
            lblDecodePreview.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDecodePreview.BorderStyle = BorderStyle.FixedSingle;
            lblDecodePreview.Location = new Point(230, 326);
            lblDecodePreview.Name = "lblDecodePreview";
            lblDecodePreview.Size = new Size(507, 28);
            lblDecodePreview.TabIndex = 3;
            lblDecodePreview.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTestBytes
            // 
            txtTestBytes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTestBytes.Location = new Point(230, 271);
            txtTestBytes.Name = "txtTestBytes";
            txtTestBytes.Size = new Size(330, 23);
            txtTestBytes.TabIndex = 4;
            txtTestBytes.TextChanged += preview_Changed;
            // 
            // chkSimulateOnDecodeError
            // 
            chkSimulateOnDecodeError.AutoSize = true;
            chkSimulateOnDecodeError.Location = new Point(231, 300);
            chkSimulateOnDecodeError.Name = "chkSimulateOnDecodeError";
            chkSimulateOnDecodeError.Size = new Size(159, 19);
            chkSimulateOnDecodeError.TabIndex = 5;
            chkSimulateOnDecodeError.Text = "Simulate on decode error";
            chkSimulateOnDecodeError.CheckedChanged += preview_Changed;
            // 
            // lblTestBytes
            // 
            lblTestBytes.AutoSize = true;
            lblTestBytes.Location = new Point(16, 274);
            lblTestBytes.Name = "lblTestBytes";
            lblTestBytes.Size = new Size(58, 15);
            lblTestBytes.TabIndex = 6;
            lblTestBytes.Text = "Test bytes";
            // 
            // txtUnits
            // 
            txtUnits.Location = new Point(557, 239);
            txtUnits.Name = "txtUnits";
            txtUnits.Size = new Size(180, 23);
            txtUnits.TabIndex = 7;
            txtUnits.TextChanged += preview_Changed;
            // 
            // nudPrecision
            // 
            nudPrecision.Location = new Point(230, 242);
            nudPrecision.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudPrecision.Name = "nudPrecision";
            nudPrecision.Size = new Size(120, 23);
            nudPrecision.TabIndex = 8;
            nudPrecision.Value = new decimal(new int[] { 2, 0, 0, 0 });
            nudPrecision.ValueChanged += preview_Changed;
            // 
            // txtOffset
            // 
            txtOffset.Location = new Point(557, 207);
            txtOffset.Name = "txtOffset";
            txtOffset.Size = new Size(180, 23);
            txtOffset.TabIndex = 9;
            txtOffset.TextChanged += preview_Changed;
            // 
            // txtCoefficient
            // 
            txtCoefficient.Location = new Point(230, 210);
            txtCoefficient.Name = "txtCoefficient";
            txtCoefficient.Size = new Size(180, 23);
            txtCoefficient.TabIndex = 10;
            txtCoefficient.TextChanged += preview_Changed;
            // 
            // cmbByteOrder
            // 
            cmbByteOrder.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbByteOrder.Location = new Point(557, 175);
            cmbByteOrder.Name = "cmbByteOrder";
            cmbByteOrder.Size = new Size(180, 23);
            cmbByteOrder.TabIndex = 11;
            cmbByteOrder.SelectedIndexChanged += preview_Changed;
            // 
            // cmbFormat
            // 
            cmbFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFormat.Location = new Point(230, 178);
            cmbFormat.Name = "cmbFormat";
            cmbFormat.Size = new Size(180, 23);
            cmbFormat.TabIndex = 12;
            cmbFormat.SelectedIndexChanged += preview_Changed;
            // 
            // nudLength
            // 
            nudLength.Location = new Point(557, 143);
            nudLength.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            nudLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLength.Name = "nudLength";
            nudLength.Size = new Size(99, 23);
            nudLength.TabIndex = 13;
            nudLength.Value = new decimal(new int[] { 2, 0, 0, 0 });
            nudLength.ValueChanged += preview_Changed;
            // 
            // nudIndex
            // 
            nudIndex.Location = new Point(230, 146);
            nudIndex.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudIndex.Name = "nudIndex";
            nudIndex.Size = new Size(120, 23);
            nudIndex.TabIndex = 14;
            nudIndex.ValueChanged += preview_Changed;
            // 
            // cmbMode
            // 
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.Location = new Point(230, 114);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(180, 23);
            cmbMode.TabIndex = 15;
            cmbMode.SelectedIndexChanged += preview_Changed;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(230, 79);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(507, 23);
            txtDescription.TabIndex = 16;
            txtDescription.TextChanged += preview_Changed;
            // 
            // nudChannel
            // 
            nudChannel.Location = new Point(230, 47);
            nudChannel.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudChannel.Name = "nudChannel";
            nudChannel.Size = new Size(120, 23);
            nudChannel.TabIndex = 17;
            nudChannel.ValueChanged += preview_Changed;
            // 
            // chkEnabled
            // 
            chkEnabled.AutoSize = true;
            chkEnabled.Location = new Point(502, 17);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(68, 19);
            chkEnabled.TabIndex = 18;
            chkEnabled.Text = "Enabled";
            chkEnabled.CheckedChanged += preview_Changed;
            // 
            // txtName
            // 
            txtName.Location = new Point(230, 15);
            txtName.Name = "txtName";
            txtName.Size = new Size(250, 23);
            txtName.TabIndex = 19;
            txtName.TextChanged += preview_Changed;
            // 
            // lblUnits
            // 
            lblUnits.AutoSize = true;
            lblUnits.Location = new Point(439, 242);
            lblUnits.Name = "lblUnits";
            lblUnits.Size = new Size(34, 15);
            lblUnits.TabIndex = 20;
            lblUnits.Text = "Units";
            // 
            // lblPrecision
            // 
            lblPrecision.AutoSize = true;
            lblPrecision.Location = new Point(16, 242);
            lblPrecision.Name = "lblPrecision";
            lblPrecision.Size = new Size(55, 15);
            lblPrecision.TabIndex = 21;
            lblPrecision.Text = "Precision";
            // 
            // lblOffset
            // 
            lblOffset.AutoSize = true;
            lblOffset.Location = new Point(439, 210);
            lblOffset.Name = "lblOffset";
            lblOffset.Size = new Size(39, 15);
            lblOffset.TabIndex = 22;
            lblOffset.Text = "Offset";
            // 
            // lblCoefficient
            // 
            lblCoefficient.AutoSize = true;
            lblCoefficient.Location = new Point(16, 210);
            lblCoefficient.Name = "lblCoefficient";
            lblCoefficient.Size = new Size(65, 15);
            lblCoefficient.TabIndex = 23;
            lblCoefficient.Text = "Coefficient";
            // 
            // lblByteOrder
            // 
            lblByteOrder.AutoSize = true;
            lblByteOrder.Location = new Point(439, 178);
            lblByteOrder.Name = "lblByteOrder";
            lblByteOrder.Size = new Size(61, 15);
            lblByteOrder.TabIndex = 24;
            lblByteOrder.Text = "Byte order";
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(16, 178);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(45, 15);
            lblFormat.TabIndex = 25;
            lblFormat.Text = "Format";
            // 
            // lblLength
            // 
            lblLength.AutoSize = true;
            lblLength.Location = new Point(439, 146);
            lblLength.Name = "lblLength";
            lblLength.Size = new Size(68, 15);
            lblLength.TabIndex = 26;
            lblLength.Text = "Data length";
            // 
            // lblIndex
            // 
            lblIndex.AutoSize = true;
            lblIndex.Location = new Point(16, 146);
            lblIndex.Name = "lblIndex";
            lblIndex.Size = new Size(67, 15);
            lblIndex.TabIndex = 27;
            lblIndex.Text = "Array index";
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Location = new Point(16, 114);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(38, 15);
            lblMode.TabIndex = 28;
            lblMode.Text = "Mode";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(16, 82);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 29;
            lblDescription.Text = "Description";
            // 
            // lblChannel
            // 
            lblChannel.AutoSize = true;
            lblChannel.Location = new Point(16, 50);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(51, 15);
            lblChannel.TabIndex = 30;
            lblChannel.Text = "Channel";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(16, 18);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 31;
            lblName.Text = "Name";
            // 
            // tabSimulation
            // 
            tabSimulation.Controls.Add(pnlStringSimulation);
            tabSimulation.Controls.Add(grpSimulation);
            tabSimulation.Location = new Point(4, 24);
            tabSimulation.Name = "tabSimulation";
            tabSimulation.Size = new Size(752, 448);
            tabSimulation.TabIndex = 1;
            tabSimulation.Text = "Simulation";
            // 
            // pnlStringSimulation
            // 
            pnlStringSimulation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlStringSimulation.Controls.Add(txtStringTemplate);
            pnlStringSimulation.Controls.Add(txtStringDelimiter);
            pnlStringSimulation.Controls.Add(txtStringValues);
            pnlStringSimulation.Controls.Add(cmbStringMode);
            pnlStringSimulation.Controls.Add(lblStringTemplate);
            pnlStringSimulation.Controls.Add(lblStringDelimiter);
            pnlStringSimulation.Controls.Add(lblStringValues);
            pnlStringSimulation.Controls.Add(lblStringMode);
            pnlStringSimulation.Location = new Point(15, 296);
            pnlStringSimulation.Name = "pnlStringSimulation";
            pnlStringSimulation.Size = new Size(720, 139);
            pnlStringSimulation.TabIndex = 0;
            // 
            // txtStringTemplate
            // 
            txtStringTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtStringTemplate.Location = new Point(152, 107);
            txtStringTemplate.Name = "txtStringTemplate";
            txtStringTemplate.Size = new Size(548, 23);
            txtStringTemplate.TabIndex = 0;
            txtStringTemplate.TextChanged += preview_Changed;
            // 
            // txtStringDelimiter
            // 
            txtStringDelimiter.Location = new Point(152, 75);
            txtStringDelimiter.Name = "txtStringDelimiter";
            txtStringDelimiter.Size = new Size(180, 23);
            txtStringDelimiter.TabIndex = 1;
            txtStringDelimiter.TextChanged += preview_Changed;
            // 
            // txtStringValues
            // 
            txtStringValues.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtStringValues.Location = new Point(152, 43);
            txtStringValues.Name = "txtStringValues";
            txtStringValues.Size = new Size(548, 23);
            txtStringValues.TabIndex = 2;
            txtStringValues.TextChanged += preview_Changed;
            // 
            // cmbStringMode
            // 
            cmbStringMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStringMode.Location = new Point(152, 11);
            cmbStringMode.Name = "cmbStringMode";
            cmbStringMode.Size = new Size(180, 23);
            cmbStringMode.TabIndex = 3;
            cmbStringMode.SelectedIndexChanged += preview_Changed;
            // 
            // lblStringTemplate
            // 
            lblStringTemplate.AutoSize = true;
            lblStringTemplate.Location = new Point(10, 110);
            lblStringTemplate.Name = "lblStringTemplate";
            lblStringTemplate.Size = new Size(55, 15);
            lblStringTemplate.TabIndex = 4;
            lblStringTemplate.Text = "Template";
            // 
            // lblStringDelimiter
            // 
            lblStringDelimiter.AutoSize = true;
            lblStringDelimiter.Location = new Point(10, 78);
            lblStringDelimiter.Name = "lblStringDelimiter";
            lblStringDelimiter.Size = new Size(55, 15);
            lblStringDelimiter.TabIndex = 5;
            lblStringDelimiter.Text = "Delimiter";
            // 
            // lblStringValues
            // 
            lblStringValues.AutoSize = true;
            lblStringValues.Location = new Point(10, 46);
            lblStringValues.Name = "lblStringValues";
            lblStringValues.Size = new Size(40, 15);
            lblStringValues.TabIndex = 6;
            lblStringValues.Text = "Values";
            // 
            // lblStringMode
            // 
            lblStringMode.AutoSize = true;
            lblStringMode.Location = new Point(10, 14);
            lblStringMode.Name = "lblStringMode";
            lblStringMode.Size = new Size(72, 15);
            lblStringMode.TabIndex = 7;
            lblStringMode.Text = "String mode";
            // 
            // grpSimulation
            // 
            grpSimulation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSimulation.Controls.Add(chkCycle);
            grpSimulation.Controls.Add(nudSimInterval);
            grpSimulation.Controls.Add(txtDutyCycle);
            grpSimulation.Controls.Add(txtHighValue);
            grpSimulation.Controls.Add(txtLowValue);
            grpSimulation.Controls.Add(txtPhase);
            grpSimulation.Controls.Add(txtPeriod);
            grpSimulation.Controls.Add(txtBias);
            grpSimulation.Controls.Add(txtAmplitude);
            grpSimulation.Controls.Add(txtResetValue);
            grpSimulation.Controls.Add(txtStep);
            grpSimulation.Controls.Add(txtStartValue);
            grpSimulation.Controls.Add(txtMax);
            grpSimulation.Controls.Add(txtMin);
            grpSimulation.Controls.Add(cmbSimulationKind);
            grpSimulation.Controls.Add(lblDutyCycle);
            grpSimulation.Controls.Add(lblHighValue);
            grpSimulation.Controls.Add(lblLowValue);
            grpSimulation.Controls.Add(lblPhase);
            grpSimulation.Controls.Add(lblPeriod);
            grpSimulation.Controls.Add(lblBias);
            grpSimulation.Controls.Add(lblAmplitude);
            grpSimulation.Controls.Add(lblResetValue);
            grpSimulation.Controls.Add(lblStep);
            grpSimulation.Controls.Add(lblStartValue);
            grpSimulation.Controls.Add(lblMax);
            grpSimulation.Controls.Add(lblMin);
            grpSimulation.Controls.Add(lblSimInterval);
            grpSimulation.Controls.Add(lblSimulationKind);
            grpSimulation.Location = new Point(15, 14);
            grpSimulation.Name = "grpSimulation";
            grpSimulation.Size = new Size(720, 276);
            grpSimulation.TabIndex = 1;
            grpSimulation.TabStop = false;
            grpSimulation.Text = "Numeric simulation";
            // 
            // chkCycle
            // 
            chkCycle.AutoSize = true;
            chkCycle.Location = new Point(646, 29);
            chkCycle.Name = "chkCycle";
            chkCycle.Size = new Size(55, 19);
            chkCycle.TabIndex = 0;
            chkCycle.Text = "Cycle";
            chkCycle.CheckedChanged += preview_Changed;
            // 
            // nudSimInterval
            // 
            nudSimInterval.Location = new Point(520, 27);
            nudSimInterval.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudSimInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSimInterval.Name = "nudSimInterval";
            nudSimInterval.Size = new Size(120, 23);
            nudSimInterval.TabIndex = 1;
            nudSimInterval.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSimInterval.ValueChanged += preview_Changed;
            // 
            // txtDutyCycle
            // 
            txtDutyCycle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDutyCycle.Location = new Point(520, 245);
            txtDutyCycle.Name = "txtDutyCycle";
            txtDutyCycle.Size = new Size(180, 23);
            txtDutyCycle.TabIndex = 2;
            txtDutyCycle.TextChanged += preview_Changed;
            // 
            // txtHighValue
            // 
            txtHighValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtHighValue.Location = new Point(520, 216);
            txtHighValue.Name = "txtHighValue";
            txtHighValue.Size = new Size(180, 23);
            txtHighValue.TabIndex = 3;
            txtHighValue.TextChanged += preview_Changed;
            // 
            // txtLowValue
            // 
            txtLowValue.Location = new Point(520, 187);
            txtLowValue.Name = "txtLowValue";
            txtLowValue.Size = new Size(180, 23);
            txtLowValue.TabIndex = 4;
            txtLowValue.TextChanged += preview_Changed;
            // 
            // txtPhase
            // 
            txtPhase.Location = new Point(152, 187);
            txtPhase.Name = "txtPhase";
            txtPhase.Size = new Size(180, 23);
            txtPhase.TabIndex = 5;
            txtPhase.TextChanged += preview_Changed;
            // 
            // txtPeriod
            // 
            txtPeriod.Location = new Point(520, 155);
            txtPeriod.Name = "txtPeriod";
            txtPeriod.Size = new Size(180, 23);
            txtPeriod.TabIndex = 6;
            txtPeriod.TextChanged += preview_Changed;
            // 
            // txtBias
            // 
            txtBias.Location = new Point(152, 155);
            txtBias.Name = "txtBias";
            txtBias.Size = new Size(180, 23);
            txtBias.TabIndex = 7;
            txtBias.TextChanged += preview_Changed;
            // 
            // txtAmplitude
            // 
            txtAmplitude.Location = new Point(520, 123);
            txtAmplitude.Name = "txtAmplitude";
            txtAmplitude.Size = new Size(180, 23);
            txtAmplitude.TabIndex = 8;
            txtAmplitude.TextChanged += preview_Changed;
            // 
            // txtResetValue
            // 
            txtResetValue.Location = new Point(152, 123);
            txtResetValue.Name = "txtResetValue";
            txtResetValue.Size = new Size(180, 23);
            txtResetValue.TabIndex = 9;
            txtResetValue.TextChanged += preview_Changed;
            // 
            // txtStep
            // 
            txtStep.Location = new Point(520, 91);
            txtStep.Name = "txtStep";
            txtStep.Size = new Size(180, 23);
            txtStep.TabIndex = 10;
            txtStep.TextChanged += preview_Changed;
            // 
            // txtStartValue
            // 
            txtStartValue.Location = new Point(152, 91);
            txtStartValue.Name = "txtStartValue";
            txtStartValue.Size = new Size(180, 23);
            txtStartValue.TabIndex = 11;
            txtStartValue.TextChanged += preview_Changed;
            // 
            // txtMax
            // 
            txtMax.Location = new Point(520, 59);
            txtMax.Name = "txtMax";
            txtMax.Size = new Size(180, 23);
            txtMax.TabIndex = 12;
            txtMax.TextChanged += preview_Changed;
            // 
            // txtMin
            // 
            txtMin.Location = new Point(152, 59);
            txtMin.Name = "txtMin";
            txtMin.Size = new Size(180, 23);
            txtMin.TabIndex = 13;
            txtMin.TextChanged += preview_Changed;
            // 
            // cmbSimulationKind
            // 
            cmbSimulationKind.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSimulationKind.Location = new Point(152, 27);
            cmbSimulationKind.Name = "cmbSimulationKind";
            cmbSimulationKind.Size = new Size(180, 23);
            cmbSimulationKind.TabIndex = 14;
            cmbSimulationKind.SelectedIndexChanged += preview_Changed;
            // 
            // lblDutyCycle
            // 
            lblDutyCycle.AutoSize = true;
            lblDutyCycle.Location = new Point(340, 248);
            lblDutyCycle.Name = "lblDutyCycle";
            lblDutyCycle.Size = new Size(45, 15);
            lblDutyCycle.TabIndex = 15;
            lblDutyCycle.Text = "Duty %";
            // 
            // lblHighValue
            // 
            lblHighValue.AutoSize = true;
            lblHighValue.Location = new Point(340, 219);
            lblHighValue.Name = "lblHighValue";
            lblHighValue.Size = new Size(33, 15);
            lblHighValue.TabIndex = 16;
            lblHighValue.Text = "High";
            // 
            // lblLowValue
            // 
            lblLowValue.AutoSize = true;
            lblLowValue.Location = new Point(340, 190);
            lblLowValue.Name = "lblLowValue";
            lblLowValue.Size = new Size(60, 15);
            lblLowValue.TabIndex = 17;
            lblLowValue.Text = "Low value";
            // 
            // lblPhase
            // 
            lblPhase.AutoSize = true;
            lblPhase.Location = new Point(17, 190);
            lblPhase.Name = "lblPhase";
            lblPhase.Size = new Size(61, 15);
            lblPhase.TabIndex = 18;
            lblPhase.Text = "Phase deg";
            // 
            // lblPeriod
            // 
            lblPeriod.AutoSize = true;
            lblPeriod.Location = new Point(340, 158);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(61, 15);
            lblPeriod.TabIndex = 19;
            lblPeriod.Text = "Period sec";
            // 
            // lblBias
            // 
            lblBias.AutoSize = true;
            lblBias.Location = new Point(17, 158);
            lblBias.Name = "lblBias";
            lblBias.Size = new Size(28, 15);
            lblBias.TabIndex = 20;
            lblBias.Text = "Bias";
            // 
            // lblAmplitude
            // 
            lblAmplitude.AutoSize = true;
            lblAmplitude.Location = new Point(340, 126);
            lblAmplitude.Name = "lblAmplitude";
            lblAmplitude.Size = new Size(63, 15);
            lblAmplitude.TabIndex = 21;
            lblAmplitude.Text = "Amplitude";
            // 
            // lblResetValue
            // 
            lblResetValue.AutoSize = true;
            lblResetValue.Location = new Point(17, 126);
            lblResetValue.Name = "lblResetValue";
            lblResetValue.Size = new Size(66, 15);
            lblResetValue.TabIndex = 22;
            lblResetValue.Text = "Reset value";
            // 
            // lblStep
            // 
            lblStep.AutoSize = true;
            lblStep.Location = new Point(340, 94);
            lblStep.Name = "lblStep";
            lblStep.Size = new Size(30, 15);
            lblStep.TabIndex = 23;
            lblStep.Text = "Step";
            // 
            // lblStartValue
            // 
            lblStartValue.AutoSize = true;
            lblStartValue.Location = new Point(17, 94);
            lblStartValue.Name = "lblStartValue";
            lblStartValue.Size = new Size(62, 15);
            lblStartValue.TabIndex = 24;
            lblStartValue.Text = "Start value";
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.Location = new Point(340, 62);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(30, 15);
            lblMax.TabIndex = 25;
            lblMax.Text = "Max";
            // 
            // lblMin
            // 
            lblMin.AutoSize = true;
            lblMin.Location = new Point(17, 62);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(28, 15);
            lblMin.TabIndex = 26;
            lblMin.Text = "Min";
            // 
            // lblSimInterval
            // 
            lblSimInterval.AutoSize = true;
            lblSimInterval.Location = new Point(340, 30);
            lblSimInterval.Name = "lblSimInterval";
            lblSimInterval.Size = new Size(65, 15);
            lblSimInterval.TabIndex = 27;
            lblSimInterval.Text = "Interval ms";
            // 
            // lblSimulationKind
            // 
            lblSimulationKind.AutoSize = true;
            lblSimulationKind.Location = new Point(17, 30);
            lblSimulationKind.Name = "lblSimulationKind";
            lblSimulationKind.Size = new Size(31, 15);
            lblSimulationKind.TabIndex = 28;
            lblSimulationKind.Text = "Kind";
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(582, 496);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(90, 27);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(678, 496);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 27);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmTag
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 535);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(tabMain);
            MinimizeBox = false;
            MinimumSize = new Size(800, 574);
            Name = "FrmTag";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tag";
            Load += FrmTag_Load;
            tabMain.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecision).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudChannel).EndInit();
            tabSimulation.ResumeLayout(false);
            pnlStringSimulation.ResumeLayout(false);
            pnlStringSimulation.PerformLayout();
            grpSimulation.ResumeLayout(false);
            grpSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSimInterval).EndInit();
            ResumeLayout(false);
        }

        private TabControl tabMain;
        private TabPage tabGeneral;
        private TabPage tabSimulation;
        private TextBox txtName;
        private CheckBox chkEnabled;
        private NumericUpDown nudChannel;
        private TextBox txtDescription;
        private ComboBox cmbMode;
        private NumericUpDown nudIndex;
        private NumericUpDown nudLength;
        private ComboBox cmbFormat;
        private ComboBox cmbByteOrder;
        private TextBox txtCoefficient;
        private TextBox txtOffset;
        private NumericUpDown nudPrecision;
        private TextBox txtUnits;
        private TextBox txtTestBytes;
        private CheckBox chkSimulateOnDecodeError;
        private Label lblName;
        private Label lblChannel;
        private Label lblDescription;
        private Label lblMode;
        private Label lblIndex;
        private Label lblLength;
        private Label lblFormat;
        private Label lblByteOrder;
        private Label lblCoefficient;
        private Label lblOffset;
        private Label lblPrecision;
        private Label lblUnits;
        private Label lblTestBytes;
        private Label lblDecodePreviewCaption;
        private Label lblDecodePreview;
        private Label lblSimulationPreviewCaption;
        private Label lblSimulationPreview;
        private GroupBox grpSimulation;
        private ComboBox cmbSimulationKind;
        private NumericUpDown nudSimInterval;
        private TextBox txtMin;
        private TextBox txtMax;
        private TextBox txtStartValue;
        private TextBox txtStep;
        private TextBox txtResetValue;
        private TextBox txtAmplitude;
        private TextBox txtBias;
        private TextBox txtPeriod;
        private TextBox txtPhase;
        private TextBox txtLowValue;
        private TextBox txtHighValue;
        private TextBox txtDutyCycle;
        private CheckBox chkCycle;
        private Label lblSimulationKind;
        private Label lblSimInterval;
        private Label lblMin;
        private Label lblMax;
        private Label lblStartValue;
        private Label lblStep;
        private Label lblResetValue;
        private Label lblAmplitude;
        private Label lblBias;
        private Label lblPeriod;
        private Label lblPhase;
        private Label lblLowValue;
        private Label lblHighValue;
        private Label lblDutyCycle;
        private Panel pnlStringSimulation;
        private ComboBox cmbStringMode;
        private TextBox txtStringValues;
        private TextBox txtStringDelimiter;
        private TextBox txtStringTemplate;
        private Label lblStringMode;
        private Label lblStringValues;
        private Label lblStringDelimiter;
        private Label lblStringTemplate;
        private Button btnOk;
        private Button btnCancel;
    }
}
