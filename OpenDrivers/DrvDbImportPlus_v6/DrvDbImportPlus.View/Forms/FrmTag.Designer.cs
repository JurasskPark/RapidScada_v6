namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    partial class FrmTag
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTagname = new System.Windows.Forms.Label();
            this.txtTagname = new System.Windows.Forms.TextBox();
            this.ckbTagEnabled = new System.Windows.Forms.CheckBox();
            this.lblEnabled = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtTagCode = new System.Windows.Forms.TextBox();
            this.lblTagCode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTagname
            // 
            this.lblTagname.AutoSize = true;
            this.lblTagname.Location = new System.Drawing.Point(12, 15);
            this.lblTagname.Name = "lblTagname";
            this.lblTagname.Size = new System.Drawing.Size(55, 15);
            this.lblTagname.TabIndex = 0;
            this.lblTagname.Text = "Tagname";
            // 
            // txtTagname
            // 
            this.txtTagname.Location = new System.Drawing.Point(165, 12);
            this.txtTagname.Name = "txtTagname";
            this.txtTagname.Size = new System.Drawing.Size(225, 23);
            this.txtTagname.TabIndex = 1;
            // 
            // ckbTagEnabled
            // 
            this.ckbTagEnabled.AutoSize = true;
            this.ckbTagEnabled.Location = new System.Drawing.Point(165, 70);
            this.ckbTagEnabled.Name = "ckbTagEnabled";
            this.ckbTagEnabled.Size = new System.Drawing.Size(15, 14);
            this.ckbTagEnabled.TabIndex = 4;
            this.ckbTagEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEnabled
            // 
            this.lblEnabled.AutoSize = true;
            this.lblEnabled.Location = new System.Drawing.Point(12, 69);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(49, 15);
            this.lblEnabled.TabIndex = 7;
            this.lblEnabled.Text = "Enabled";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(302, 92);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 27);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(206, 92);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 27);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(206, 92);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 27);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTagCode
            // 
            this.txtTagCode.Location = new System.Drawing.Point(165, 41);
            this.txtTagCode.Name = "txtTagCode";
            this.txtTagCode.Size = new System.Drawing.Size(225, 23);
            this.txtTagCode.TabIndex = 13;
            // 
            // lblTagCode
            // 
            this.lblTagCode.AutoSize = true;
            this.lblTagCode.Location = new System.Drawing.Point(12, 44);
            this.lblTagCode.Name = "lblTagCode";
            this.lblTagCode.Size = new System.Drawing.Size(54, 15);
            this.lblTagCode.TabIndex = 12;
            this.lblTagCode.Text = "Tag code";
            // 
            // FrmTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 131);
            this.Controls.Add(this.txtTagCode);
            this.Controls.Add(this.lblTagCode);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblEnabled);
            this.Controls.Add(this.ckbTagEnabled);
            this.Controls.Add(this.txtTagname);
            this.Controls.Add(this.lblTagname);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTag";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmTag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblTagname;
        private TextBox txtTagname;
        private CheckBox ckbTagEnabled;
        private Label lblEnabled;
        private Button btnClose;
        private Button btnSave;
        private Button btnAdd;
        private TextBox txtTagCode;
        private Label lblTagCode;
    }
}