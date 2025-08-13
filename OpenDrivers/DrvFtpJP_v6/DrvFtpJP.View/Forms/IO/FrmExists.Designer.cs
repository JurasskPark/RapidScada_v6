namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmExists
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
            overwriteFile = new Button();
            resumeFile = new Button();
            skipFile = new Button();
            overwriteAll = new Button();
            resumeAll = new Button();
            skipAll = new Button();
            Cancel = new Button();
            label1 = new Label();
            label2 = new Label();
            SourceIcon = new PictureBox();
            DestinationIcon = new PictureBox();
            SourceFileName = new Label();
            DestinationFileName = new Label();
            SourceFileDate = new Label();
            SourceFileSize = new Label();
            DestinationFileDate = new Label();
            DestinationFileSize = new Label();
            ((System.ComponentModel.ISupportInitialize)SourceIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DestinationIcon).BeginInit();
            SuspendLayout();
            // 
            // overwriteFile
            // 
            overwriteFile.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            overwriteFile.ForeColor = Color.Green;
            overwriteFile.Location = new Point(335, 9);
            overwriteFile.Margin = new Padding(4, 3, 4, 3);
            overwriteFile.Name = "overwriteFile";
            overwriteFile.Size = new Size(286, 27);
            overwriteFile.TabIndex = 0;
            overwriteFile.Text = "Overwrite this file";
            overwriteFile.UseVisualStyleBackColor = true;
            // 
            // resumeFile
            // 
            resumeFile.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            resumeFile.ForeColor = Color.Green;
            resumeFile.Location = new Point(335, 76);
            resumeFile.Margin = new Padding(4, 3, 4, 3);
            resumeFile.Name = "resumeFile";
            resumeFile.Size = new Size(286, 27);
            resumeFile.TabIndex = 1;
            resumeFile.Text = "Resume this file";
            resumeFile.UseVisualStyleBackColor = true;
            resumeFile.Visible = false;
            // 
            // skipFile
            // 
            skipFile.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            skipFile.ForeColor = Color.Green;
            skipFile.Location = new Point(335, 43);
            skipFile.Margin = new Padding(4, 3, 4, 3);
            skipFile.Name = "skipFile";
            skipFile.Size = new Size(286, 27);
            skipFile.TabIndex = 2;
            skipFile.Text = "Skip this file";
            skipFile.UseVisualStyleBackColor = true;
            // 
            // overwriteAll
            // 
            overwriteAll.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            overwriteAll.ForeColor = Color.Maroon;
            overwriteAll.Location = new Point(335, 131);
            overwriteAll.Margin = new Padding(4, 3, 4, 3);
            overwriteAll.Name = "overwriteAll";
            overwriteAll.Size = new Size(286, 27);
            overwriteAll.TabIndex = 3;
            overwriteAll.Text = "Overwrite all files";
            overwriteAll.UseVisualStyleBackColor = true;
            // 
            // resumeAll
            // 
            resumeAll.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            resumeAll.ForeColor = Color.Maroon;
            resumeAll.Location = new Point(335, 197);
            resumeAll.Margin = new Padding(4, 3, 4, 3);
            resumeAll.Name = "resumeAll";
            resumeAll.Size = new Size(286, 27);
            resumeAll.TabIndex = 4;
            resumeAll.Text = "Resume all files";
            resumeAll.UseVisualStyleBackColor = true;
            resumeAll.Visible = false;
            // 
            // skipAll
            // 
            skipAll.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            skipAll.ForeColor = Color.Maroon;
            skipAll.Location = new Point(335, 164);
            skipAll.Margin = new Padding(4, 3, 4, 3);
            skipAll.Name = "skipAll";
            skipAll.Size = new Size(286, 27);
            skipAll.TabIndex = 5;
            skipAll.Text = "Skip all files";
            skipAll.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new Point(335, 255);
            Cancel.Margin = new Padding(4, 3, 4, 3);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(286, 48);
            Cancel.TabIndex = 6;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(13, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(72, 13);
            label1.TabIndex = 7;
            label1.Text = "Source file:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(13, 164);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(96, 13);
            label2.TabIndex = 7;
            label2.Text = "Destination file:";
            // 
            // SourceIcon
            // 
            SourceIcon.Location = new Point(14, 43);
            SourceIcon.Margin = new Padding(4, 3, 4, 3);
            SourceIcon.Name = "SourceIcon";
            SourceIcon.Size = new Size(47, 43);
            SourceIcon.TabIndex = 8;
            SourceIcon.TabStop = false;
            // 
            // DestinationIcon
            // 
            DestinationIcon.Location = new Point(13, 197);
            DestinationIcon.Margin = new Padding(4, 3, 4, 3);
            DestinationIcon.Name = "DestinationIcon";
            DestinationIcon.Size = new Size(48, 43);
            DestinationIcon.TabIndex = 8;
            DestinationIcon.TabStop = false;
            // 
            // SourceFileName
            // 
            SourceFileName.Location = new Point(67, 43);
            SourceFileName.Margin = new Padding(4, 0, 4, 0);
            SourceFileName.Name = "SourceFileName";
            SourceFileName.Size = new Size(261, 60);
            SourceFileName.TabIndex = 9;
            SourceFileName.Text = "source file name";
            // 
            // DestinationFileName
            // 
            DestinationFileName.Location = new Point(65, 197);
            DestinationFileName.Margin = new Padding(4, 0, 4, 0);
            DestinationFileName.Name = "DestinationFileName";
            DestinationFileName.Size = new Size(262, 60);
            DestinationFileName.TabIndex = 9;
            DestinationFileName.Text = "destination file name";
            // 
            // SourceFileDate
            // 
            SourceFileDate.AutoSize = true;
            SourceFileDate.Location = new Point(65, 107);
            SourceFileDate.Margin = new Padding(4, 0, 4, 0);
            SourceFileDate.Name = "SourceFileDate";
            SourceFileDate.Size = new Size(87, 15);
            SourceFileDate.TabIndex = 10;
            SourceFileDate.Text = "source file date";
            // 
            // SourceFileSize
            // 
            SourceFileSize.AutoSize = true;
            SourceFileSize.Location = new Point(65, 129);
            SourceFileSize.Margin = new Padding(4, 0, 4, 0);
            SourceFileSize.Name = "SourceFileSize";
            SourceFileSize.Size = new Size(83, 15);
            SourceFileSize.TabIndex = 10;
            SourceFileSize.Text = "source file size";
            // 
            // DestinationFileDate
            // 
            DestinationFileDate.AutoSize = true;
            DestinationFileDate.Location = new Point(67, 264);
            DestinationFileDate.Margin = new Padding(4, 0, 4, 0);
            DestinationFileDate.Name = "DestinationFileDate";
            DestinationFileDate.Size = new Size(111, 15);
            DestinationFileDate.TabIndex = 10;
            DestinationFileDate.Text = "destination file date";
            // 
            // DestinationFileSize
            // 
            DestinationFileSize.AutoSize = true;
            DestinationFileSize.Location = new Point(67, 286);
            DestinationFileSize.Margin = new Padding(4, 0, 4, 0);
            DestinationFileSize.Name = "DestinationFileSize";
            DestinationFileSize.Size = new Size(107, 15);
            DestinationFileSize.TabIndex = 10;
            DestinationFileSize.Text = "destination file size";
            // 
            // FrmExists
            // 
            AcceptButton = overwriteFile;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = Cancel;
            ClientSize = new Size(636, 314);
            Controls.Add(DestinationFileSize);
            Controls.Add(SourceFileSize);
            Controls.Add(DestinationFileDate);
            Controls.Add(SourceFileDate);
            Controls.Add(DestinationFileName);
            Controls.Add(SourceFileName);
            Controls.Add(DestinationIcon);
            Controls.Add(SourceIcon);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Cancel);
            Controls.Add(skipAll);
            Controls.Add(resumeAll);
            Controls.Add(overwriteAll);
            Controls.Add(skipFile);
            Controls.Add(resumeFile);
            Controls.Add(overwriteFile);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmExists";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "File exists...";
            ((System.ComponentModel.ISupportInitialize)SourceIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)DestinationIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button overwriteFile;
        private System.Windows.Forms.Button resumeFile;
        private System.Windows.Forms.Button skipFile;
        private System.Windows.Forms.Button overwriteAll;
        private System.Windows.Forms.Button resumeAll;
        private System.Windows.Forms.Button skipAll;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.PictureBox SourceIcon;
        public System.Windows.Forms.PictureBox DestinationIcon;
        public System.Windows.Forms.Label SourceFileName;
        public System.Windows.Forms.Label SourceFileDate;
        public System.Windows.Forms.Label SourceFileSize;
        public System.Windows.Forms.Label DestinationFileName;
        public System.Windows.Forms.Label DestinationFileDate;
        public System.Windows.Forms.Label DestinationFileSize;

    }
}