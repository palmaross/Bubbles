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
            this.components = new System.ComponentModel.Container();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.listRunAtStart = new System.Windows.Forms.ListView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.groupBoxRunAtStart = new System.Windows.Forms.GroupBox();
            this.groupBoxRunAtStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(236, 164);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(155, 164);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // listRunAtStart
            // 
            this.listRunAtStart.CheckBoxes = true;
            this.listRunAtStart.HideSelection = false;
            this.listRunAtStart.Location = new System.Drawing.Point(12, 39);
            this.listRunAtStart.Name = "listRunAtStart";
            this.listRunAtStart.Size = new System.Drawing.Size(276, 70);
            this.listRunAtStart.TabIndex = 14;
            this.listRunAtStart.UseCompatibleStateImageBehavior = false;
            this.listRunAtStart.View = System.Windows.Forms.View.SmallIcon;
            this.listRunAtStart.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listRunAtStart_ItemChecked);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(13, 19);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(139, 17);
            this.cbSelectAll.TabIndex = 15;
            this.cbSelectAll.Text = "Выбрать/снять выбор";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBoxRunAtStart
            // 
            this.groupBoxRunAtStart.Controls.Add(this.cbSelectAll);
            this.groupBoxRunAtStart.Controls.Add(this.listRunAtStart);
            this.groupBoxRunAtStart.Location = new System.Drawing.Point(12, 10);
            this.groupBoxRunAtStart.Name = "groupBoxRunAtStart";
            this.groupBoxRunAtStart.Size = new System.Drawing.Size(299, 120);
            this.groupBoxRunAtStart.TabIndex = 16;
            this.groupBoxRunAtStart.TabStop = false;
            this.groupBoxRunAtStart.Text = "Запускать при старте MindManager:";
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(322, 196);
            this.Controls.Add(this.groupBoxRunAtStart);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBoxRunAtStart.ResumeLayout(false);
            this.groupBoxRunAtStart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ListView listRunAtStart;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.GroupBox groupBoxRunAtStart;
    }
}