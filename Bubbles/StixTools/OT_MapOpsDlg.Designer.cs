namespace Bubbles
{
    partial class OT_MapOpsDlg
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
            this.chSave = new System.Windows.Forms.CheckBox();
            this.chClose = new System.Windows.Forms.CheckBox();
            this.chAddToStix = new System.Windows.Forms.CheckBox();
            this.MapList = new System.Windows.Forms.CheckedListBox();
            this.checkAll = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chSave
            // 
            this.chSave.AutoSize = true;
            this.chSave.Checked = true;
            this.chSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chSave.Location = new System.Drawing.Point(8, 12);
            this.chSave.Name = "chSave";
            this.chSave.Size = new System.Drawing.Size(51, 17);
            this.chSave.TabIndex = 0;
            this.chSave.Text = "Save";
            this.chSave.UseVisualStyleBackColor = true;
            // 
            // chClose
            // 
            this.chClose.AutoSize = true;
            this.chClose.Checked = true;
            this.chClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chClose.Location = new System.Drawing.Point(93, 12);
            this.chClose.Name = "chClose";
            this.chClose.Size = new System.Drawing.Size(52, 17);
            this.chClose.TabIndex = 1;
            this.chClose.Text = "Close";
            this.chClose.UseVisualStyleBackColor = true;
            // 
            // chAddToStix
            // 
            this.chAddToStix.AutoSize = true;
            this.chAddToStix.Location = new System.Drawing.Point(169, 12);
            this.chAddToStix.Name = "chAddToStix";
            this.chAddToStix.Size = new System.Drawing.Size(108, 17);
            this.chAddToStix.TabIndex = 2;
            this.chAddToStix.Text = "Добавить to Stix";
            this.chAddToStix.UseVisualStyleBackColor = true;
            // 
            // MapList
            // 
            this.MapList.CheckOnClick = true;
            this.MapList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapList.FormattingEnabled = true;
            this.MapList.Location = new System.Drawing.Point(8, 54);
            this.MapList.Name = "MapList";
            this.MapList.Size = new System.Drawing.Size(262, 100);
            this.MapList.TabIndex = 3;
            // 
            // checkAll
            // 
            this.checkAll.AutoSize = true;
            this.checkAll.Checked = true;
            this.checkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAll.Location = new System.Drawing.Point(21, 35);
            this.checkAll.Name = "checkAll";
            this.checkAll.Size = new System.Drawing.Size(120, 17);
            this.checkAll.TabIndex = 4;
            this.checkAll.Text = "Check/Uncheck All";
            this.checkAll.UseVisualStyleBackColor = true;
            this.checkAll.CheckedChanged += new System.EventHandler(this.checkAll_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(195, 162);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(8, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // OT_MapOpsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(276, 192);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.checkAll);
            this.Controls.Add(this.MapList);
            this.Controls.Add(this.chAddToStix);
            this.Controls.Add(this.chClose);
            this.Controls.Add(this.chSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OT_MapOpsDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OT_MapOps";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OT_MapOpsDlg_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chSave;
        private System.Windows.Forms.CheckBox chClose;
        private System.Windows.Forms.CheckBox chAddToStix;
        private System.Windows.Forms.CheckedListBox MapList;
        private System.Windows.Forms.CheckBox checkAll;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
    }
}