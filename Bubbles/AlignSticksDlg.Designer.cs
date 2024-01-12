namespace Bubbles
{
    partial class AlignSticksDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlignSticksDlg));
            this.pictureBoxHH = new System.Windows.Forms.PictureBox();
            this.pictureBoxHV = new System.Windows.Forms.PictureBox();
            this.pictureBoxVV = new System.Windows.Forms.PictureBox();
            this.pictureBoxVH = new System.Windows.Forms.PictureBox();
            this.rbtnHH = new System.Windows.Forms.RadioButton();
            this.rbtnVV = new System.Windows.Forms.RadioButton();
            this.rbtnVH = new System.Windows.Forms.RadioButton();
            this.rbtnHV = new System.Windows.Forms.RadioButton();
            this.lblAlign = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAlign = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.PictureBox();
            this.btnUp = new System.Windows.Forms.PictureBox();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.listSticks = new System.Windows.Forms.ListView();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.btnRemember = new System.Windows.Forms.Button();
            this.btnCreateConfig = new System.Windows.Forms.Button();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.btnConfigCancel = new System.Windows.Forms.Button();
            this.chboxRunAtStart = new System.Windows.Forms.CheckBox();
            this.rbtnNewConfig = new System.Windows.Forms.RadioButton();
            this.rbtnSaveToConfig = new System.Windows.Forms.RadioButton();
            this.cbConfigurations = new System.Windows.Forms.ComboBox();
            this.btnConfigOK = new System.Windows.Forms.Button();
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.chBoxExpandSticks1 = new System.Windows.Forms.CheckBox();
            this.chBoxExpandSticks2 = new System.Windows.Forms.CheckBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.panelConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxHH
            // 
            this.pictureBoxHH.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHH.Image")));
            this.pictureBoxHH.Location = new System.Drawing.Point(12, 28);
            this.pictureBoxHH.Name = "pictureBoxHH";
            this.pictureBoxHH.Size = new System.Drawing.Size(283, 30);
            this.pictureBoxHH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxHH.TabIndex = 0;
            this.pictureBoxHH.TabStop = false;
            this.pictureBoxHH.Click += new System.EventHandler(this.pictureBoxHH_Click);
            // 
            // pictureBoxHV
            // 
            this.pictureBoxHV.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHV.Image")));
            this.pictureBoxHV.Location = new System.Drawing.Point(108, 275);
            this.pictureBoxHV.Name = "pictureBoxHV";
            this.pictureBoxHV.Size = new System.Drawing.Size(142, 100);
            this.pictureBoxHV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxHV.TabIndex = 1;
            this.pictureBoxHV.TabStop = false;
            this.pictureBoxHV.Click += new System.EventHandler(this.pictureBoxHV_Click);
            // 
            // pictureBoxVV
            // 
            this.pictureBoxVV.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxVV.Image")));
            this.pictureBoxVV.Location = new System.Drawing.Point(12, 91);
            this.pictureBoxVV.Name = "pictureBoxVV";
            this.pictureBoxVV.Size = new System.Drawing.Size(33, 284);
            this.pictureBoxVV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxVV.TabIndex = 2;
            this.pictureBoxVV.TabStop = false;
            this.pictureBoxVV.Click += new System.EventHandler(this.pictureBoxVV_Click);
            // 
            // pictureBoxVH
            // 
            this.pictureBoxVH.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxVH.Image")));
            this.pictureBoxVH.Location = new System.Drawing.Point(123, 91);
            this.pictureBoxVH.Name = "pictureBoxVH";
            this.pictureBoxVH.Size = new System.Drawing.Size(104, 142);
            this.pictureBoxVH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxVH.TabIndex = 3;
            this.pictureBoxVH.TabStop = false;
            this.pictureBoxVH.Click += new System.EventHandler(this.pictureBoxVH_Click);
            // 
            // rbtnHH
            // 
            this.rbtnHH.AutoSize = true;
            this.rbtnHH.Location = new System.Drawing.Point(12, 5);
            this.rbtnHH.Name = "rbtnHH";
            this.rbtnHH.Size = new System.Drawing.Size(63, 17);
            this.rbtnHH.TabIndex = 4;
            this.rbtnHH.Text = "Дорога";
            this.rbtnHH.UseVisualStyleBackColor = true;
            this.rbtnHH.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // rbtnVV
            // 
            this.rbtnVV.AutoSize = true;
            this.rbtnVV.Location = new System.Drawing.Point(12, 66);
            this.rbtnVV.Name = "rbtnVV";
            this.rbtnVV.Size = new System.Drawing.Size(55, 17);
            this.rbtnVV.TabIndex = 5;
            this.rbtnVV.Text = "Столб";
            this.rbtnVV.UseVisualStyleBackColor = true;
            this.rbtnVV.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // rbtnVH
            // 
            this.rbtnVH.AutoSize = true;
            this.rbtnVH.Location = new System.Drawing.Point(124, 66);
            this.rbtnVH.Name = "rbtnVH";
            this.rbtnVH.Size = new System.Drawing.Size(103, 17);
            this.rbtnVH.TabIndex = 6;
            this.rbtnVH.Text = "Книги на полке";
            this.rbtnVH.UseVisualStyleBackColor = true;
            this.rbtnVH.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // rbtnHV
            // 
            this.rbtnHV.AutoSize = true;
            this.rbtnHV.Checked = true;
            this.rbtnHV.Location = new System.Drawing.Point(125, 252);
            this.rbtnHV.Name = "rbtnHV";
            this.rbtnHV.Size = new System.Drawing.Size(99, 17);
            this.rbtnHV.TabIndex = 7;
            this.rbtnHV.TabStop = true;
            this.rbtnHV.Text = "Книги стопкой";
            this.rbtnHV.UseVisualStyleBackColor = true;
            this.rbtnHV.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // lblAlign
            // 
            this.lblAlign.AutoSize = true;
            this.lblAlign.Location = new System.Drawing.Point(310, 7);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(208, 13);
            this.lblAlign.TabIndex = 9;
            this.lblAlign.Text = "Равнение по первому (верхнему) стику:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(443, 419);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlign.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAlign.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAlign.Image = ((System.Drawing.Image)(resources.GetObject("btnAlign.Image")));
            this.btnAlign.Location = new System.Drawing.Point(219, 417);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(99, 27);
            this.btnAlign.TabIndex = 11;
            this.btnAlign.Text = "Отравнять";
            this.btnAlign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlign.UseVisualStyleBackColor = false;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(491, 203);
            this.btnDown.Margin = new System.Windows.Forms.Padding(6);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDown.TabIndex = 36;
            this.btnDown.TabStop = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(491, 171);
            this.btnUp.Margin = new System.Windows.Forms.Padding(6);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnUp.TabIndex = 35;
            this.btnUp.TabStop = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(316, 24);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(91, 17);
            this.cbSelectAll.TabIndex = 37;
            this.cbSelectAll.Text = "Выбрать все";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // listSticks
            // 
            this.listSticks.CheckBoxes = true;
            this.listSticks.HideSelection = false;
            this.listSticks.Location = new System.Drawing.Point(313, 47);
            this.listSticks.Name = "listSticks";
            this.listSticks.Size = new System.Drawing.Size(205, 328);
            this.listSticks.TabIndex = 38;
            this.listSticks.UseCompatibleStateImageBehavior = false;
            this.listSticks.View = System.Windows.Forms.View.List;
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(61, 242);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(50, 4);
            this.p1.TabIndex = 39;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // btnRemember
            // 
            this.btnRemember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemember.Location = new System.Drawing.Point(12, 419);
            this.btnRemember.Name = "btnRemember";
            this.btnRemember.Size = new System.Drawing.Size(90, 23);
            this.btnRemember.TabIndex = 40;
            this.btnRemember.Text = "Запомнить";
            this.btnRemember.UseVisualStyleBackColor = true;
            this.btnRemember.Click += new System.EventHandler(this.btnRemember_Click);
            // 
            // btnCreateConfig
            // 
            this.btnCreateConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateConfig.Location = new System.Drawing.Point(108, 419);
            this.btnCreateConfig.Name = "btnCreateConfig";
            this.btnCreateConfig.Size = new System.Drawing.Size(90, 23);
            this.btnCreateConfig.TabIndex = 41;
            this.btnCreateConfig.Text = "Конфигурация";
            this.btnCreateConfig.UseVisualStyleBackColor = true;
            this.btnCreateConfig.Click += new System.EventHandler(this.btnCreateConfig_Click);
            // 
            // panelConfig
            // 
            this.panelConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelConfig.Controls.Add(this.btnConfigCancel);
            this.panelConfig.Controls.Add(this.chboxRunAtStart);
            this.panelConfig.Controls.Add(this.rbtnNewConfig);
            this.panelConfig.Controls.Add(this.rbtnSaveToConfig);
            this.panelConfig.Controls.Add(this.cbConfigurations);
            this.panelConfig.Controls.Add(this.btnConfigOK);
            this.panelConfig.Controls.Add(this.txtConfigName);
            this.panelConfig.Location = new System.Drawing.Point(108, 242);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(259, 171);
            this.panelConfig.TabIndex = 42;
            this.panelConfig.Visible = false;
            // 
            // btnConfigCancel
            // 
            this.btnConfigCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfigCancel.Location = new System.Drawing.Point(223, 3);
            this.btnConfigCancel.Name = "btnConfigCancel";
            this.btnConfigCancel.Size = new System.Drawing.Size(30, 23);
            this.btnConfigCancel.TabIndex = 49;
            this.btnConfigCancel.Text = "Х";
            this.btnConfigCancel.UseVisualStyleBackColor = true;
            this.btnConfigCancel.Click += new System.EventHandler(this.btnConfigCancel_Click);
            // 
            // chboxRunAtStart
            // 
            this.chboxRunAtStart.AutoSize = true;
            this.chboxRunAtStart.Location = new System.Drawing.Point(15, 119);
            this.chboxRunAtStart.Name = "chboxRunAtStart";
            this.chboxRunAtStart.Size = new System.Drawing.Size(205, 17);
            this.chboxRunAtStart.TabIndex = 48;
            this.chboxRunAtStart.Text = "Запускать при старте MindManager";
            this.chboxRunAtStart.UseVisualStyleBackColor = true;
            // 
            // rbtnNewConfig
            // 
            this.rbtnNewConfig.AutoSize = true;
            this.rbtnNewConfig.Checked = true;
            this.rbtnNewConfig.Location = new System.Drawing.Point(17, 12);
            this.rbtnNewConfig.Name = "rbtnNewConfig";
            this.rbtnNewConfig.Size = new System.Drawing.Size(135, 17);
            this.rbtnNewConfig.TabIndex = 47;
            this.rbtnNewConfig.TabStop = true;
            this.rbtnNewConfig.Text = "Новая конфигурация:";
            this.rbtnNewConfig.UseVisualStyleBackColor = true;
            // 
            // rbtnSaveToConfig
            // 
            this.rbtnSaveToConfig.AutoSize = true;
            this.rbtnSaveToConfig.Location = new System.Drawing.Point(15, 64);
            this.rbtnSaveToConfig.Name = "rbtnSaveToConfig";
            this.rbtnSaveToConfig.Size = new System.Drawing.Size(154, 17);
            this.rbtnSaveToConfig.TabIndex = 46;
            this.rbtnSaveToConfig.Text = "Обновить конфигурацию:";
            this.rbtnSaveToConfig.UseVisualStyleBackColor = true;
            // 
            // cbConfigurations
            // 
            this.cbConfigurations.FormattingEnabled = true;
            this.cbConfigurations.Location = new System.Drawing.Point(15, 83);
            this.cbConfigurations.Name = "cbConfigurations";
            this.cbConfigurations.Size = new System.Drawing.Size(227, 21);
            this.cbConfigurations.TabIndex = 45;
            // 
            // btnConfigOK
            // 
            this.btnConfigOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfigOK.Location = new System.Drawing.Point(114, 143);
            this.btnConfigOK.Name = "btnConfigOK";
            this.btnConfigOK.Size = new System.Drawing.Size(30, 23);
            this.btnConfigOK.TabIndex = 43;
            this.btnConfigOK.Text = "ОК";
            this.btnConfigOK.UseVisualStyleBackColor = true;
            this.btnConfigOK.Click += new System.EventHandler(this.btnConfigOK_Click);
            // 
            // txtConfigName
            // 
            this.txtConfigName.Location = new System.Drawing.Point(17, 33);
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(226, 20);
            this.txtConfigName.TabIndex = 0;
            // 
            // chBoxExpandSticks1
            // 
            this.chBoxExpandSticks1.AutoSize = true;
            this.chBoxExpandSticks1.Checked = true;
            this.chBoxExpandSticks1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxExpandSticks1.Location = new System.Drawing.Point(100, 5);
            this.chBoxExpandSticks1.Name = "chBoxExpandSticks1";
            this.chBoxExpandSticks1.Size = new System.Drawing.Size(174, 17);
            this.chBoxExpandSticks1.TabIndex = 43;
            this.chBoxExpandSticks1.Text = "Развернуть свернутые стики";
            this.chBoxExpandSticks1.UseVisualStyleBackColor = true;
            this.chBoxExpandSticks1.Visible = false;
            // 
            // chBoxExpandSticks2
            // 
            this.chBoxExpandSticks2.AutoSize = true;
            this.chBoxExpandSticks2.Checked = true;
            this.chBoxExpandSticks2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxExpandSticks2.Location = new System.Drawing.Point(13, 91);
            this.chBoxExpandSticks2.Name = "chBoxExpandSticks2";
            this.chBoxExpandSticks2.Size = new System.Drawing.Size(174, 17);
            this.chBoxExpandSticks2.TabIndex = 44;
            this.chBoxExpandSticks2.Text = "Развернуть свернутые стики";
            this.chBoxExpandSticks2.UseVisualStyleBackColor = true;
            this.chBoxExpandSticks2.Visible = false;
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(11, 389);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(177, 13);
            this.lblDistance.TabIndex = 45;
            this.lblDistance.Text = "Расстояние между стиками (мм):";
            // 
            // numDistance
            // 
            this.numDistance.Location = new System.Drawing.Point(216, 387);
            this.numDistance.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(34, 20);
            this.numDistance.TabIndex = 46;
            this.numDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AlignSticksDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 453);
            this.Controls.Add(this.numDistance);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.chBoxExpandSticks2);
            this.Controls.Add(this.chBoxExpandSticks1);
            this.Controls.Add(this.panelConfig);
            this.Controls.Add(this.btnCreateConfig);
            this.Controls.Add(this.btnRemember);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnAlign);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAlign);
            this.Controls.Add(this.rbtnHV);
            this.Controls.Add(this.rbtnVH);
            this.Controls.Add(this.rbtnVV);
            this.Controls.Add(this.rbtnHH);
            this.Controls.Add(this.pictureBoxHH);
            this.Controls.Add(this.pictureBoxVH);
            this.Controls.Add(this.pictureBoxVV);
            this.Controls.Add(this.pictureBoxHV);
            this.Controls.Add(this.listSticks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlignSticksDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlignSticksDlg";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxHH;
        private System.Windows.Forms.PictureBox pictureBoxHV;
        private System.Windows.Forms.PictureBox pictureBoxVV;
        private System.Windows.Forms.PictureBox pictureBoxVH;
        private System.Windows.Forms.RadioButton rbtnHH;
        private System.Windows.Forms.RadioButton rbtnVV;
        private System.Windows.Forms.RadioButton rbtnVH;
        private System.Windows.Forms.RadioButton rbtnHV;
        private System.Windows.Forms.Label lblAlign;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAlign;
        private System.Windows.Forms.PictureBox btnDown;
        private System.Windows.Forms.PictureBox btnUp;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.ListView listSticks;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.Button btnRemember;
        private System.Windows.Forms.Button btnCreateConfig;
        private System.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.Button btnConfigOK;
        private System.Windows.Forms.TextBox txtConfigName;
        private System.Windows.Forms.ComboBox cbConfigurations;
        private System.Windows.Forms.RadioButton rbtnNewConfig;
        private System.Windows.Forms.RadioButton rbtnSaveToConfig;
        private System.Windows.Forms.CheckBox chboxRunAtStart;
        private System.Windows.Forms.Button btnConfigCancel;
        private System.Windows.Forms.CheckBox chBoxExpandSticks1;
        private System.Windows.Forms.CheckBox chBoxExpandSticks2;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.NumericUpDown numDistance;
    }
}