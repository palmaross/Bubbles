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
            this.gbRunAtStart = new System.Windows.Forms.GroupBox();
            this.cbConfiguration = new System.Windows.Forms.ComboBox();
            this.rbtnConfiguration = new System.Windows.Forms.RadioButton();
            this.rbtnSticks = new System.Windows.Forms.RadioButton();
            this.gbRunAtStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(236, 212);
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
            this.btnSave.Location = new System.Drawing.Point(155, 212);
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
            this.listRunAtStart.Location = new System.Drawing.Point(12, 60);
            this.listRunAtStart.Name = "listRunAtStart";
            this.listRunAtStart.Size = new System.Drawing.Size(276, 70);
            this.listRunAtStart.TabIndex = 14;
            this.listRunAtStart.UseCompatibleStateImageBehavior = false;
            this.listRunAtStart.View = System.Windows.Forms.View.SmallIcon;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(13, 40);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(139, 17);
            this.cbSelectAll.TabIndex = 15;
            this.cbSelectAll.Text = "Выбрать/снять выбор";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // gbRunAtStart
            // 
            this.gbRunAtStart.Controls.Add(this.cbConfiguration);
            this.gbRunAtStart.Controls.Add(this.rbtnConfiguration);
            this.gbRunAtStart.Controls.Add(this.rbtnSticks);
            this.gbRunAtStart.Controls.Add(this.cbSelectAll);
            this.gbRunAtStart.Controls.Add(this.listRunAtStart);
            this.gbRunAtStart.Location = new System.Drawing.Point(12, 10);
            this.gbRunAtStart.Name = "gbRunAtStart";
            this.gbRunAtStart.Size = new System.Drawing.Size(299, 194);
            this.gbRunAtStart.TabIndex = 16;
            this.gbRunAtStart.TabStop = false;
            this.gbRunAtStart.Text = "Запускать при старте MindManager:";
            // 
            // cbConfiguration
            // 
            this.cbConfiguration.FormattingEnabled = true;
            this.cbConfiguration.Location = new System.Drawing.Point(13, 160);
            this.cbConfiguration.Name = "cbConfiguration";
            this.cbConfiguration.Size = new System.Drawing.Size(275, 21);
            this.cbConfiguration.TabIndex = 18;
            // 
            // rbtnConfiguration
            // 
            this.rbtnConfiguration.AutoSize = true;
            this.rbtnConfiguration.Location = new System.Drawing.Point(85, 137);
            this.rbtnConfiguration.Name = "rbtnConfiguration";
            this.rbtnConfiguration.Size = new System.Drawing.Size(103, 17);
            this.rbtnConfiguration.TabIndex = 17;
            this.rbtnConfiguration.Text = "Конфигурацию:";
            this.rbtnConfiguration.UseVisualStyleBackColor = true;
            // 
            // rbtnSticks
            // 
            this.rbtnSticks.AutoSize = true;
            this.rbtnSticks.Checked = true;
            this.rbtnSticks.Location = new System.Drawing.Point(85, 19);
            this.rbtnSticks.Name = "rbtnSticks";
            this.rbtnSticks.Size = new System.Drawing.Size(119, 17);
            this.rbtnSticks.TabIndex = 16;
            this.rbtnSticks.TabStop = true;
            this.rbtnSticks.Text = "Выбранные стики:";
            this.rbtnSticks.UseVisualStyleBackColor = true;
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(322, 244);
            this.Controls.Add(this.gbRunAtStart);
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
            this.gbRunAtStart.ResumeLayout(false);
            this.gbRunAtStart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ListView listRunAtStart;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.GroupBox gbRunAtStart;
        private System.Windows.Forms.ComboBox cbConfiguration;
        private System.Windows.Forms.RadioButton rbtnConfiguration;
        private System.Windows.Forms.RadioButton rbtnSticks;
    }
}