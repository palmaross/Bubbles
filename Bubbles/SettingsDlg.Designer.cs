namespace Bubbles
{
    partial class SettingsDlg
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
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxAtStart = new System.Windows.Forms.GroupBox();
            this.startSnippets = new System.Windows.Forms.CheckBox();
            this.StartIcons = new System.Windows.Forms.CheckBox();
            this.StartPaste = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxAtStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(263, 139);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // groupBoxAtStart
            // 
            this.groupBoxAtStart.Controls.Add(this.startSnippets);
            this.groupBoxAtStart.Controls.Add(this.StartIcons);
            this.groupBoxAtStart.Controls.Add(this.StartPaste);
            this.groupBoxAtStart.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAtStart.Name = "groupBoxAtStart";
            this.groupBoxAtStart.Size = new System.Drawing.Size(284, 92);
            this.groupBoxAtStart.TabIndex = 2;
            this.groupBoxAtStart.TabStop = false;
            this.groupBoxAtStart.Text = "Запускать при старте MindManager";
            // 
            // startSnippets
            // 
            this.startSnippets.AutoSize = true;
            this.startSnippets.Location = new System.Drawing.Point(9, 65);
            this.startSnippets.Name = "startSnippets";
            this.startSnippets.Size = new System.Drawing.Size(99, 17);
            this.startSnippets.TabIndex = 2;
            this.startSnippets.Text = "Буфер обмена";
            this.startSnippets.UseVisualStyleBackColor = true;
            // 
            // StartIcons
            // 
            this.StartIcons.AutoSize = true;
            this.StartIcons.Location = new System.Drawing.Point(9, 42);
            this.StartIcons.Name = "StartIcons";
            this.StartIcons.Size = new System.Drawing.Size(62, 17);
            this.StartIcons.TabIndex = 1;
            this.StartIcons.Text = "Значки";
            this.StartIcons.UseVisualStyleBackColor = true;
            // 
            // StartPaste
            // 
            this.StartPaste.AutoSize = true;
            this.StartPaste.Location = new System.Drawing.Point(9, 19);
            this.StartPaste.Name = "StartPaste";
            this.StartPaste.Size = new System.Drawing.Size(68, 17);
            this.StartPaste.TabIndex = 0;
            this.StartPaste.Text = "Вставка";
            this.StartPaste.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(182, 139);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(348, 171);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxAtStart);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBoxAtStart.ResumeLayout(false);
            this.groupBoxAtStart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBoxAtStart;
        private System.Windows.Forms.CheckBox startSnippets;
        private System.Windows.Forms.CheckBox StartIcons;
        private System.Windows.Forms.CheckBox StartPaste;
        private System.Windows.Forms.Button btnSave;
    }
}