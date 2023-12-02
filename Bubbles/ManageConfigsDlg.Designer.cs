namespace Bubbles
{
    partial class ManageConfigsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageConfigsDlg));
            this.cbConfigs = new System.Windows.Forms.ComboBox();
            this.configEdit = new System.Windows.Forms.PictureBox();
            this.configNew = new System.Windows.Forms.PictureBox();
            this.configDelete = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.gbNewConfig = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chboxRunAtStart = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chBoxVisibleSticks = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.configEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configDelete)).BeginInit();
            this.gbNewConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbConfigs
            // 
            this.cbConfigs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConfigs.FormattingEnabled = true;
            this.cbConfigs.Location = new System.Drawing.Point(12, 12);
            this.cbConfigs.Name = "cbConfigs";
            this.cbConfigs.Size = new System.Drawing.Size(205, 21);
            this.cbConfigs.Sorted = true;
            this.cbConfigs.TabIndex = 0;
            // 
            // configEdit
            // 
            this.configEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.configEdit.Image = ((System.Drawing.Image)(resources.GetObject("configEdit.Image")));
            this.configEdit.Location = new System.Drawing.Point(277, 12);
            this.configEdit.Margin = new System.Windows.Forms.Padding(6);
            this.configEdit.Name = "configEdit";
            this.configEdit.Size = new System.Drawing.Size(20, 20);
            this.configEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.configEdit.TabIndex = 36;
            this.configEdit.TabStop = false;
            this.configEdit.Click += new System.EventHandler(this.configEdit_Click);
            // 
            // configNew
            // 
            this.configNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.configNew.Image = ((System.Drawing.Image)(resources.GetObject("configNew.Image")));
            this.configNew.Location = new System.Drawing.Point(251, 12);
            this.configNew.Margin = new System.Windows.Forms.Padding(6);
            this.configNew.Name = "configNew";
            this.configNew.Size = new System.Drawing.Size(20, 20);
            this.configNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.configNew.TabIndex = 34;
            this.configNew.TabStop = false;
            this.configNew.Click += new System.EventHandler(this.configNew_Click);
            // 
            // configDelete
            // 
            this.configDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.configDelete.Image = ((System.Drawing.Image)(resources.GetObject("configDelete.Image")));
            this.configDelete.Location = new System.Drawing.Point(224, 12);
            this.configDelete.Margin = new System.Windows.Forms.Padding(6);
            this.configDelete.Name = "configDelete";
            this.configDelete.Size = new System.Drawing.Size(20, 20);
            this.configDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.configDelete.TabIndex = 35;
            this.configDelete.TabStop = false;
            this.configDelete.Click += new System.EventHandler(this.configDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(222, 162);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 162);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtConfigName
            // 
            this.txtConfigName.Location = new System.Drawing.Point(10, 25);
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(264, 20);
            this.txtConfigName.TabIndex = 39;
            // 
            // gbNewConfig
            // 
            this.gbNewConfig.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gbNewConfig.Controls.Add(this.btnCancel);
            this.gbNewConfig.Controls.Add(this.btnOK);
            this.gbNewConfig.Controls.Add(this.txtConfigName);
            this.gbNewConfig.Location = new System.Drawing.Point(12, 44);
            this.gbNewConfig.Name = "gbNewConfig";
            this.gbNewConfig.Size = new System.Drawing.Size(284, 87);
            this.gbNewConfig.TabIndex = 40;
            this.gbNewConfig.TabStop = false;
            this.gbNewConfig.Text = "Имя новой конфигурации";
            this.gbNewConfig.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(199, 54);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(111, 54);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 41;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chboxRunAtStart
            // 
            this.chboxRunAtStart.AutoSize = true;
            this.chboxRunAtStart.Location = new System.Drawing.Point(12, 46);
            this.chboxRunAtStart.Name = "chboxRunAtStart";
            this.chboxRunAtStart.Size = new System.Drawing.Size(205, 17);
            this.chboxRunAtStart.TabIndex = 42;
            this.chboxRunAtStart.Text = "Запускать при старте MindManager";
            this.chboxRunAtStart.UseVisualStyleBackColor = true;
            // 
            // chBoxVisibleSticks
            // 
            this.chBoxVisibleSticks.Location = new System.Drawing.Point(12, 64);
            this.chBoxVisibleSticks.Name = "chBoxVisibleSticks";
            this.chBoxVisibleSticks.Size = new System.Drawing.Size(285, 36);
            this.chBoxVisibleSticks.TabIndex = 43;
            this.chBoxVisibleSticks.Text = "Стики в конфигурации заменить видимыми на экране стиками.";
            this.chBoxVisibleSticks.UseVisualStyleBackColor = true;
            // 
            // ManageConfigsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(309, 195);
            this.Controls.Add(this.gbNewConfig);
            this.Controls.Add(this.chboxRunAtStart);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chBoxVisibleSticks);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.configEdit);
            this.Controls.Add(this.configNew);
            this.Controls.Add(this.configDelete);
            this.Controls.Add(this.cbConfigs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageConfigsDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ManageConfigsDlg";
            ((System.ComponentModel.ISupportInitialize)(this.configEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configDelete)).EndInit();
            this.gbNewConfig.ResumeLayout(false);
            this.gbNewConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbConfigs;
        private System.Windows.Forms.PictureBox configEdit;
        private System.Windows.Forms.PictureBox configNew;
        private System.Windows.Forms.PictureBox configDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtConfigName;
        private System.Windows.Forms.GroupBox gbNewConfig;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chboxRunAtStart;
        private System.Windows.Forms.CheckBox chBoxVisibleSticks;
    }
}