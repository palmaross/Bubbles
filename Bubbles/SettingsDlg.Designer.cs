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
            this.gbScaleFactor = new System.Windows.Forms.GroupBox();
            this.btnTestScale3 = new System.Windows.Forms.Button();
            this.btnTestScale2 = new System.Windows.Forms.Button();
            this.btnTestScale1 = new System.Windows.Forms.Button();
            this.numBoxes = new System.Windows.Forms.MaskedTextBox();
            this.numStixBase = new System.Windows.Forms.MaskedTextBox();
            this.numStix = new System.Windows.Forms.MaskedTextBox();
            this.lblBoxes = new System.Windows.Forms.Label();
            this.lblStixBase = new System.Windows.Forms.Label();
            this.lblStix = new System.Windows.Forms.Label();
            this.cbStix = new System.Windows.Forms.ComboBox();
            this.cbStixBase = new System.Windows.Forms.ComboBox();
            this.cbBoxes = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.gbRunAtStart.SuspendLayout();
            this.gbScaleFactor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(236, 329);
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
            this.btnSave.Location = new System.Drawing.Point(155, 329);
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
            this.listRunAtStart.Size = new System.Drawing.Size(276, 80);
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
            this.gbRunAtStart.Size = new System.Drawing.Size(299, 204);
            this.gbRunAtStart.TabIndex = 16;
            this.gbRunAtStart.TabStop = false;
            this.gbRunAtStart.Text = "Запускать при старте MindManager:";
            // 
            // cbConfiguration
            // 
            this.cbConfiguration.FormattingEnabled = true;
            this.cbConfiguration.Location = new System.Drawing.Point(13, 171);
            this.cbConfiguration.Name = "cbConfiguration";
            this.cbConfiguration.Size = new System.Drawing.Size(275, 21);
            this.cbConfiguration.TabIndex = 18;
            // 
            // rbtnConfiguration
            // 
            this.rbtnConfiguration.AutoSize = true;
            this.rbtnConfiguration.Location = new System.Drawing.Point(85, 148);
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
            // gbScaleFactor
            // 
            this.gbScaleFactor.Controls.Add(this.btnTestScale3);
            this.gbScaleFactor.Controls.Add(this.btnTestScale2);
            this.gbScaleFactor.Controls.Add(this.btnTestScale1);
            this.gbScaleFactor.Controls.Add(this.numBoxes);
            this.gbScaleFactor.Controls.Add(this.numStixBase);
            this.gbScaleFactor.Controls.Add(this.numStix);
            this.gbScaleFactor.Controls.Add(this.lblBoxes);
            this.gbScaleFactor.Controls.Add(this.lblStixBase);
            this.gbScaleFactor.Controls.Add(this.lblStix);
            this.gbScaleFactor.Controls.Add(this.cbStix);
            this.gbScaleFactor.Controls.Add(this.cbStixBase);
            this.gbScaleFactor.Controls.Add(this.cbBoxes);
            this.gbScaleFactor.Location = new System.Drawing.Point(12, 222);
            this.gbScaleFactor.Name = "gbScaleFactor";
            this.gbScaleFactor.Size = new System.Drawing.Size(299, 98);
            this.gbScaleFactor.TabIndex = 17;
            this.gbScaleFactor.TabStop = false;
            this.gbScaleFactor.Text = "Scale Factor (allowed values: 100 to 267%)";
            // 
            // btnTestScale3
            // 
            this.btnTestScale3.Location = new System.Drawing.Point(164, 67);
            this.btnTestScale3.Name = "btnTestScale3";
            this.btnTestScale3.Size = new System.Drawing.Size(54, 23);
            this.btnTestScale3.TabIndex = 20;
            this.btnTestScale3.Text = "Test";
            this.btnTestScale3.UseVisualStyleBackColor = true;
            this.btnTestScale3.Click += new System.EventHandler(this.btnTestScale_Click);
            // 
            // btnTestScale2
            // 
            this.btnTestScale2.Location = new System.Drawing.Point(164, 43);
            this.btnTestScale2.Name = "btnTestScale2";
            this.btnTestScale2.Size = new System.Drawing.Size(54, 23);
            this.btnTestScale2.TabIndex = 19;
            this.btnTestScale2.Text = "Test";
            this.btnTestScale2.UseVisualStyleBackColor = true;
            this.btnTestScale2.Click += new System.EventHandler(this.btnTestScale_Click);
            // 
            // btnTestScale1
            // 
            this.btnTestScale1.Location = new System.Drawing.Point(164, 20);
            this.btnTestScale1.Name = "btnTestScale1";
            this.btnTestScale1.Size = new System.Drawing.Size(54, 23);
            this.btnTestScale1.TabIndex = 18;
            this.btnTestScale1.Text = "Test";
            this.btnTestScale1.UseVisualStyleBackColor = true;
            this.btnTestScale1.Click += new System.EventHandler(this.btnTestScale_Click);
            // 
            // numBoxes
            // 
            this.numBoxes.Location = new System.Drawing.Point(94, 68);
            this.numBoxes.Mask = "000%";
            this.numBoxes.Name = "numBoxes";
            this.numBoxes.Size = new System.Drawing.Size(39, 20);
            this.numBoxes.TabIndex = 5;
            this.numBoxes.Text = "100";
            this.numBoxes.ValidatingType = typeof(int);
            this.numBoxes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numStix_KeyDown);
            this.numBoxes.Leave += new System.EventHandler(this.numStix_Leave);
            // 
            // numStixBase
            // 
            this.numStixBase.Location = new System.Drawing.Point(94, 45);
            this.numStixBase.Mask = "000%";
            this.numStixBase.Name = "numStixBase";
            this.numStixBase.Size = new System.Drawing.Size(39, 20);
            this.numStixBase.TabIndex = 4;
            this.numStixBase.Text = "100";
            this.numStixBase.ValidatingType = typeof(int);
            this.numStixBase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numStix_KeyDown);
            this.numStixBase.Leave += new System.EventHandler(this.numStix_Leave);
            // 
            // numStix
            // 
            this.numStix.Location = new System.Drawing.Point(94, 22);
            this.numStix.Mask = "009%";
            this.numStix.Name = "numStix";
            this.numStix.Size = new System.Drawing.Size(39, 20);
            this.numStix.TabIndex = 3;
            this.numStix.Text = "100";
            this.numStix.ValidatingType = typeof(int);
            this.numStix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numStix_KeyDown);
            this.numStix.Leave += new System.EventHandler(this.numStix_Leave);
            // 
            // lblBoxes
            // 
            this.lblBoxes.AutoSize = true;
            this.lblBoxes.Location = new System.Drawing.Point(10, 72);
            this.lblBoxes.Name = "lblBoxes";
            this.lblBoxes.Size = new System.Drawing.Size(39, 13);
            this.lblBoxes.TabIndex = 2;
            this.lblBoxes.Text = "Boxes:";
            // 
            // lblStixBase
            // 
            this.lblStixBase.AutoSize = true;
            this.lblStixBase.Location = new System.Drawing.Point(10, 50);
            this.lblStixBase.Name = "lblStixBase";
            this.lblStixBase.Size = new System.Drawing.Size(54, 13);
            this.lblStixBase.TabIndex = 1;
            this.lblStixBase.Text = "Stix Base:";
            // 
            // lblStix
            // 
            this.lblStix.AutoSize = true;
            this.lblStix.Location = new System.Drawing.Point(10, 28);
            this.lblStix.Name = "lblStix";
            this.lblStix.Size = new System.Drawing.Size(27, 13);
            this.lblStix.TabIndex = 0;
            this.lblStix.Text = "Stix:";
            // 
            // cbStix
            // 
            this.cbStix.FormattingEnabled = true;
            this.cbStix.Items.AddRange(new object[] {
            "100%",
            "125%",
            "150%",
            "200%",
            "250%",
            "267%"});
            this.cbStix.Location = new System.Drawing.Point(93, 21);
            this.cbStix.Name = "cbStix";
            this.cbStix.Size = new System.Drawing.Size(58, 21);
            this.cbStix.TabIndex = 6;
            this.cbStix.SelectedIndexChanged += new System.EventHandler(this.cbStix_SelectedIndexChanged);
            // 
            // cbStixBase
            // 
            this.cbStixBase.FormattingEnabled = true;
            this.cbStixBase.Items.AddRange(new object[] {
            "100%",
            "125%",
            "150%",
            "200%",
            "250%",
            "267%"});
            this.cbStixBase.Location = new System.Drawing.Point(93, 44);
            this.cbStixBase.Name = "cbStixBase";
            this.cbStixBase.Size = new System.Drawing.Size(58, 21);
            this.cbStixBase.TabIndex = 11;
            this.cbStixBase.SelectedIndexChanged += new System.EventHandler(this.cbStix_SelectedIndexChanged);
            // 
            // cbBoxes
            // 
            this.cbBoxes.FormattingEnabled = true;
            this.cbBoxes.Items.AddRange(new object[] {
            "100%",
            "125%",
            "150%",
            "200%",
            "250%",
            "267%"});
            this.cbBoxes.Location = new System.Drawing.Point(93, 67);
            this.cbBoxes.Name = "cbBoxes";
            this.cbBoxes.Size = new System.Drawing.Size(58, 21);
            this.cbBoxes.TabIndex = 12;
            this.cbBoxes.SelectedIndexChanged += new System.EventHandler(this.cbStix_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(90, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "%";
            this.label4.Visible = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown1.Location = new System.Drawing.Point(70, 322);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(38, 21);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(70, 322);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(55, 21);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.Visible = false;
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(322, 361);
            this.Controls.Add(this.gbScaleFactor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.gbRunAtStart);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.comboBox2);
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
            this.gbScaleFactor.ResumeLayout(false);
            this.gbScaleFactor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.GroupBox gbScaleFactor;
        private System.Windows.Forms.MaskedTextBox numStix;
        private System.Windows.Forms.Label lblBoxes;
        private System.Windows.Forms.Label lblStixBase;
        private System.Windows.Forms.Label lblStix;
        private System.Windows.Forms.MaskedTextBox numBoxes;
        private System.Windows.Forms.MaskedTextBox numStixBase;
        private System.Windows.Forms.ComboBox cbStix;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStixBase;
        private System.Windows.Forms.ComboBox cbBoxes;
        private System.Windows.Forms.Button btnTestScale1;
        private System.Windows.Forms.Button btnTestScale3;
        private System.Windows.Forms.Button btnTestScale2;
    }
}