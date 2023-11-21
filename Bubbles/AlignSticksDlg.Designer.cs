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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
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
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.btnConfigOK = new System.Windows.Forms.Button();
            this.cbConfigurations = new System.Windows.Forms.ComboBox();
            this.rbtnSaveToConfig = new System.Windows.Forms.RadioButton();
            this.rbtnNewConfig = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.panelConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(283, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(108, 275);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(142, 100);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(12, 91);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(33, 284);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(123, 91);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(104, 142);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
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
            this.btnClose.Location = new System.Drawing.Point(443, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlign.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAlign.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAlign.Image = ((System.Drawing.Image)(resources.GetObject("btnAlign.Image")));
            this.btnAlign.Location = new System.Drawing.Point(219, 384);
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
            this.btnDown.Location = new System.Drawing.Point(491, 217);
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
            this.btnUp.Location = new System.Drawing.Point(491, 185);
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
            this.btnRemember.Location = new System.Drawing.Point(12, 386);
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
            this.btnCreateConfig.Location = new System.Drawing.Point(108, 386);
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
            this.panelConfig.Controls.Add(this.rbtnNewConfig);
            this.panelConfig.Controls.Add(this.rbtnSaveToConfig);
            this.panelConfig.Controls.Add(this.cbConfigurations);
            this.panelConfig.Controls.Add(this.btnConfigOK);
            this.panelConfig.Controls.Add(this.txtConfigName);
            this.panelConfig.Location = new System.Drawing.Point(93, 239);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(259, 141);
            this.panelConfig.TabIndex = 42;
            this.panelConfig.Visible = false;
            // 
            // txtConfigName
            // 
            this.txtConfigName.Location = new System.Drawing.Point(17, 33);
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(226, 20);
            this.txtConfigName.TabIndex = 0;
            // 
            // btnConfigOK
            // 
            this.btnConfigOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfigOK.Location = new System.Drawing.Point(110, 113);
            this.btnConfigOK.Name = "btnConfigOK";
            this.btnConfigOK.Size = new System.Drawing.Size(38, 23);
            this.btnConfigOK.TabIndex = 43;
            this.btnConfigOK.Text = "ОК";
            this.btnConfigOK.UseVisualStyleBackColor = true;
            // 
            // cbConfigurations
            // 
            this.cbConfigurations.FormattingEnabled = true;
            this.cbConfigurations.Location = new System.Drawing.Point(15, 83);
            this.cbConfigurations.Name = "cbConfigurations";
            this.cbConfigurations.Size = new System.Drawing.Size(227, 21);
            this.cbConfigurations.TabIndex = 45;
            // 
            // rbtnSaveToConfig
            // 
            this.rbtnSaveToConfig.AutoSize = true;
            this.rbtnSaveToConfig.Location = new System.Drawing.Point(15, 64);
            this.rbtnSaveToConfig.Name = "rbtnSaveToConfig";
            this.rbtnSaveToConfig.Size = new System.Drawing.Size(167, 17);
            this.rbtnSaveToConfig.TabIndex = 46;
            this.rbtnSaveToConfig.Text = "Сохранить в конфигурацию:";
            this.rbtnSaveToConfig.UseVisualStyleBackColor = true;
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
            // AlignSticksDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 420);
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
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.listSticks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlignSticksDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlignSticksDlg";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
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
    }
}