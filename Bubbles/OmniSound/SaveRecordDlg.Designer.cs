namespace Bubbles
{
    partial class SaveRecordDlg
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
            this.components = new System.ComponentModel.Container();
            this.panelSave = new System.Windows.Forms.Panel();
            this.chCloseRecorder = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chAttachment = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblRecordName = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbGroups = new System.Windows.Forms.ComboBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.panelSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSave
            // 
            this.panelSave.Controls.Add(this.lblGroup);
            this.panelSave.Controls.Add(this.cbGroups);
            this.panelSave.Controls.Add(this.chCloseRecorder);
            this.panelSave.Controls.Add(this.btnCancel);
            this.panelSave.Controls.Add(this.btnSave);
            this.panelSave.Controls.Add(this.chAttachment);
            this.panelSave.Controls.Add(this.txtName);
            this.panelSave.Controls.Add(this.lblRecordName);
            this.panelSave.Location = new System.Drawing.Point(1, 1);
            this.panelSave.Name = "panelSave";
            this.panelSave.Size = new System.Drawing.Size(200, 168);
            this.panelSave.TabIndex = 0;
            this.panelSave.Visible = false;
            // 
            // chCloseRecorder
            // 
            this.chCloseRecorder.AutoSize = true;
            this.chCloseRecorder.Location = new System.Drawing.Point(6, 117);
            this.chCloseRecorder.Name = "chCloseRecorder";
            this.chCloseRecorder.Size = new System.Drawing.Size(129, 17);
            this.chCloseRecorder.TabIndex = 5;
            this.chCloseRecorder.Text = "Close Topic Recorder";
            this.chCloseRecorder.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(117, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 140);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chAttachment
            // 
            this.chAttachment.AutoSize = true;
            this.chAttachment.Location = new System.Drawing.Point(6, 96);
            this.chAttachment.Name = "chAttachment";
            this.chAttachment.Size = new System.Drawing.Size(102, 17);
            this.chAttachment.TabIndex = 2;
            this.chAttachment.Text = "Add Attachment";
            this.chAttachment.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 67);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblRecordName
            // 
            this.lblRecordName.AutoSize = true;
            this.lblRecordName.Location = new System.Drawing.Point(3, 50);
            this.lblRecordName.Name = "lblRecordName";
            this.lblRecordName.Size = new System.Drawing.Size(74, 13);
            this.lblRecordName.TabIndex = 0;
            this.lblRecordName.Text = "Record name:";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Verdana", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCount.Location = new System.Drawing.Point(46, 16);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(123, 130);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "3";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbGroups
            // 
            this.cbGroups.FormattingEnabled = true;
            this.cbGroups.Location = new System.Drawing.Point(6, 23);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Size = new System.Drawing.Size(186, 21);
            this.cbGroups.TabIndex = 6;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(3, 6);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(39, 13);
            this.lblGroup.TabIndex = 7;
            this.lblGroup.Text = "Group:";
            // 
            // SaveRecordDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(202, 170);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.panelSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaveRecordDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SaveRecordDlg";
            this.panelSave.ResumeLayout(false);
            this.panelSave.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRecordName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chAttachment;
        private System.Windows.Forms.CheckBox chCloseRecorder;
        public System.Windows.Forms.Panel panelSave;
        public System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cbGroups;
    }
}