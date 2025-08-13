namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmName
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
            lblName = new Label();
            txtName = new TextBox();
            btnCancel = new Button();
            btnOk = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblName.Location = new Point(13, 14);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(71, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Input Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(119, 10);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(306, 23);
            txtName.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(338, 53);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(243, 53);
            btnOk.Margin = new Padding(4, 3, 4, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(88, 27);
            btnOk.TabIndex = 3;
            btnOk.Text = "Ок";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // FrmName
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(448, 97);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(txtName);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmName";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Name";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}