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
            this.numBoxes = new System.Windows.Forms.MaskedTextBox();
            this.cbBoxes = new System.Windows.Forms.ComboBox();
            this.lblBoxes = new System.Windows.Forms.Label();
            this.gbScaleFactor = new System.Windows.Forms.GroupBox();
            this.btnTestScale = new System.Windows.Forms.Button();
            this.numStixBase = new System.Windows.Forms.MaskedTextBox();
            this.numStix = new System.Windows.Forms.MaskedTextBox();
            this.lblStixBase = new System.Windows.Forms.Label();
            this.lblStix = new System.Windows.Forms.Label();
            this.cbStix = new System.Windows.Forms.ComboBox();
            this.cbStixBase = new System.Windows.Forms.ComboBox();
            this.FaviconsToolStix = new System.Windows.Forms.CheckBox();
            this.FaviconsLinksWindow = new System.Windows.Forms.CheckBox();
            this.chOpenInOmniBrowser = new System.Windows.Forms.CheckBox();
            this.chTopicAutoWidth = new System.Windows.Forms.CheckBox();
            this.btnManageAutoWidth = new System.Windows.Forms.LinkLabel();
            this.chSaveMaps = new System.Windows.Forms.CheckBox();
            this.numSaveMaps = new System.Windows.Forms.NumericUpDown();
            this.lblMin = new System.Windows.Forms.Label();
            this.linkExport = new System.Windows.Forms.LinkLabel();
            this.linkSystemPath = new System.Windows.Forms.LinkLabel();
            this.txtDataPath = new System.Windows.Forms.TextBox();
            this.lblSharedPath = new System.Windows.Forms.Label();
            this.linkImport = new System.Windows.Forms.LinkLabel();
            this.btnShare = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelShare = new System.Windows.Forms.Panel();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.gbRunAtStart.SuspendLayout();
            this.gbScaleFactor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSaveMaps)).BeginInit();
            this.panelShare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(274, 488);
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
            this.btnSave.Location = new System.Drawing.Point(10, 488);
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
            this.listRunAtStart.Location = new System.Drawing.Point(12, 42);
            this.listRunAtStart.Name = "listRunAtStart";
            this.listRunAtStart.Size = new System.Drawing.Size(312, 84);
            this.listRunAtStart.TabIndex = 14;
            this.listRunAtStart.UseCompatibleStateImageBehavior = false;
            this.listRunAtStart.View = System.Windows.Forms.View.SmallIcon;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(13, 22);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(139, 17);
            this.cbSelectAll.TabIndex = 15;
            this.cbSelectAll.Text = "Выбрать/снять выбор";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // gbRunAtStart
            // 
            this.gbRunAtStart.Controls.Add(this.cbSelectAll);
            this.gbRunAtStart.Controls.Add(this.numBoxes);
            this.gbRunAtStart.Controls.Add(this.listRunAtStart);
            this.gbRunAtStart.Controls.Add(this.cbBoxes);
            this.gbRunAtStart.Controls.Add(this.lblBoxes);
            this.gbRunAtStart.Location = new System.Drawing.Point(12, 10);
            this.gbRunAtStart.Name = "gbRunAtStart";
            this.gbRunAtStart.Size = new System.Drawing.Size(335, 134);
            this.gbRunAtStart.TabIndex = 16;
            this.gbRunAtStart.TabStop = false;
            this.gbRunAtStart.Text = "Запускать при старте MindManager:";
            // 
            // numBoxes
            // 
            this.numBoxes.Location = new System.Drawing.Point(238, 16);
            this.numBoxes.Mask = "000%";
            this.numBoxes.Name = "numBoxes";
            this.numBoxes.Size = new System.Drawing.Size(39, 20);
            this.numBoxes.TabIndex = 5;
            this.numBoxes.Text = "100";
            this.numBoxes.ValidatingType = typeof(int);
            this.numBoxes.Visible = false;
            this.numBoxes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numStix_KeyDown);
            this.numBoxes.Leave += new System.EventHandler(this.numStix_Leave);
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
            this.cbBoxes.Location = new System.Drawing.Point(237, 15);
            this.cbBoxes.Name = "cbBoxes";
            this.cbBoxes.Size = new System.Drawing.Size(58, 21);
            this.cbBoxes.TabIndex = 12;
            this.cbBoxes.Visible = false;
            this.cbBoxes.SelectedIndexChanged += new System.EventHandler(this.cbStix_SelectedIndexChanged);
            // 
            // lblBoxes
            // 
            this.lblBoxes.AutoSize = true;
            this.lblBoxes.Location = new System.Drawing.Point(178, 20);
            this.lblBoxes.Name = "lblBoxes";
            this.lblBoxes.Size = new System.Drawing.Size(39, 13);
            this.lblBoxes.TabIndex = 2;
            this.lblBoxes.Text = "Boxes:";
            this.lblBoxes.Visible = false;
            // 
            // gbScaleFactor
            // 
            this.gbScaleFactor.Controls.Add(this.btnTestScale);
            this.gbScaleFactor.Controls.Add(this.numStixBase);
            this.gbScaleFactor.Controls.Add(this.numStix);
            this.gbScaleFactor.Controls.Add(this.lblStixBase);
            this.gbScaleFactor.Controls.Add(this.lblStix);
            this.gbScaleFactor.Controls.Add(this.cbStix);
            this.gbScaleFactor.Controls.Add(this.cbStixBase);
            this.gbScaleFactor.Location = new System.Drawing.Point(12, 158);
            this.gbScaleFactor.Name = "gbScaleFactor";
            this.gbScaleFactor.Size = new System.Drawing.Size(335, 78);
            this.gbScaleFactor.TabIndex = 17;
            this.gbScaleFactor.TabStop = false;
            this.gbScaleFactor.Text = "Scale Factor (allowed values: 100 to 300%)";
            // 
            // btnTestScale
            // 
            this.btnTestScale.Location = new System.Drawing.Point(189, 31);
            this.btnTestScale.Name = "btnTestScale";
            this.btnTestScale.Size = new System.Drawing.Size(70, 27);
            this.btnTestScale.TabIndex = 19;
            this.btnTestScale.Text = "Test";
            this.btnTestScale.UseVisualStyleBackColor = true;
            this.btnTestScale.Click += new System.EventHandler(this.btnTestScale_Click);
            // 
            // numStixBase
            // 
            this.numStixBase.Location = new System.Drawing.Point(94, 47);
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
            // lblStixBase
            // 
            this.lblStixBase.AutoSize = true;
            this.lblStixBase.Location = new System.Drawing.Point(10, 51);
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
            this.cbStixBase.Location = new System.Drawing.Point(93, 46);
            this.cbStixBase.Name = "cbStixBase";
            this.cbStixBase.Size = new System.Drawing.Size(58, 21);
            this.cbStixBase.TabIndex = 11;
            this.cbStixBase.SelectedIndexChanged += new System.EventHandler(this.cbStix_SelectedIndexChanged);
            // 
            // FaviconsToolStix
            // 
            this.FaviconsToolStix.Checked = true;
            this.FaviconsToolStix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FaviconsToolStix.Location = new System.Drawing.Point(10, 244);
            this.FaviconsToolStix.Name = "FaviconsToolStix";
            this.FaviconsToolStix.Size = new System.Drawing.Size(297, 30);
            this.FaviconsToolStix.TabIndex = 22;
            this.FaviconsToolStix.Text = "Show favicons for Web links on the ToolStix";
            this.FaviconsToolStix.UseVisualStyleBackColor = true;
            // 
            // FaviconsLinksWindow
            // 
            this.FaviconsLinksWindow.AutoSize = true;
            this.FaviconsLinksWindow.Checked = true;
            this.FaviconsLinksWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FaviconsLinksWindow.Location = new System.Drawing.Point(10, 273);
            this.FaviconsLinksWindow.Name = "FaviconsLinksWindow";
            this.FaviconsLinksWindow.Size = new System.Drawing.Size(242, 17);
            this.FaviconsLinksWindow.TabIndex = 23;
            this.FaviconsLinksWindow.Text = "Show favicons for Web links in Links Window";
            this.FaviconsLinksWindow.UseVisualStyleBackColor = true;
            // 
            // chOpenInOmniBrowser
            // 
            this.chOpenInOmniBrowser.Location = new System.Drawing.Point(10, 295);
            this.chOpenInOmniBrowser.Name = "chOpenInOmniBrowser";
            this.chOpenInOmniBrowser.Size = new System.Drawing.Size(297, 30);
            this.chOpenInOmniBrowser.TabIndex = 24;
            this.chOpenInOmniBrowser.Text = "ToolStix: open web-links in the OmniBrowser by default";
            this.chOpenInOmniBrowser.UseVisualStyleBackColor = true;
            // 
            // chTopicAutoWidth
            // 
            this.chTopicAutoWidth.AutoSize = true;
            this.chTopicAutoWidth.Location = new System.Drawing.Point(10, 330);
            this.chTopicAutoWidth.Name = "chTopicAutoWidth";
            this.chTopicAutoWidth.Size = new System.Drawing.Size(181, 17);
            this.chTopicAutoWidth.TabIndex = 25;
            this.chTopicAutoWidth.Text = "Автоматическая ширина темы";
            this.chTopicAutoWidth.UseVisualStyleBackColor = true;
            // 
            // btnManageAutoWidth
            // 
            this.btnManageAutoWidth.Location = new System.Drawing.Point(201, 331);
            this.btnManageAutoWidth.Name = "btnManageAutoWidth";
            this.btnManageAutoWidth.Size = new System.Drawing.Size(145, 13);
            this.btnManageAutoWidth.TabIndex = 26;
            this.btnManageAutoWidth.TabStop = true;
            this.btnManageAutoWidth.Text = "Manage Auto-Widths";
            this.btnManageAutoWidth.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnManageAutoWidth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnManageAutoWidth_LinkClicked);
            // 
            // chSaveMaps
            // 
            this.chSaveMaps.AutoSize = true;
            this.chSaveMaps.Location = new System.Drawing.Point(10, 359);
            this.chSaveMaps.Name = "chSaveMaps";
            this.chSaveMaps.Size = new System.Drawing.Size(135, 17);
            this.chSaveMaps.TabIndex = 27;
            this.chSaveMaps.Text = "Save open maps every";
            this.chSaveMaps.UseVisualStyleBackColor = true;
            // 
            // numSaveMaps
            // 
            this.numSaveMaps.Location = new System.Drawing.Point(151, 357);
            this.numSaveMaps.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numSaveMaps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSaveMaps.Name = "numSaveMaps";
            this.numSaveMaps.Size = new System.Drawing.Size(24, 20);
            this.numSaveMaps.TabIndex = 28;
            this.numSaveMaps.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(190, 360);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(26, 13);
            this.lblMin.TabIndex = 20;
            this.lblMin.Text = "min.";
            // 
            // linkExport
            // 
            this.linkExport.AutoSize = true;
            this.linkExport.Location = new System.Drawing.Point(0, 58);
            this.linkExport.Name = "linkExport";
            this.linkExport.Size = new System.Drawing.Size(134, 13);
            this.linkExport.TabIndex = 29;
            this.linkExport.TabStop = true;
            this.linkExport.Text = "Экспортировать OmniStix";
            this.linkExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkExport_LinkClicked);
            // 
            // linkSystemPath
            // 
            this.linkSystemPath.Location = new System.Drawing.Point(190, 10);
            this.linkSystemPath.Name = "linkSystemPath";
            this.linkSystemPath.Size = new System.Drawing.Size(114, 13);
            this.linkSystemPath.TabIndex = 6;
            this.linkSystemPath.TabStop = true;
            this.linkSystemPath.Text = "System Path";
            this.linkSystemPath.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkSystemPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSystemPath_LinkClicked);
            // 
            // txtDataPath
            // 
            this.txtDataPath.Location = new System.Drawing.Point(2, 28);
            this.txtDataPath.Name = "txtDataPath";
            this.txtDataPath.ReadOnly = true;
            this.txtDataPath.Size = new System.Drawing.Size(300, 20);
            this.txtDataPath.TabIndex = 1;
            // 
            // lblSharedPath
            // 
            this.lblSharedPath.AutoSize = true;
            this.lblSharedPath.Location = new System.Drawing.Point(1, 10);
            this.lblSharedPath.Name = "lblSharedPath";
            this.lblSharedPath.Size = new System.Drawing.Size(72, 13);
            this.lblSharedPath.TabIndex = 0;
            this.lblSharedPath.Text = " Shared Path:";
            // 
            // linkImport
            // 
            this.linkImport.Location = new System.Drawing.Point(183, 58);
            this.linkImport.Name = "linkImport";
            this.linkImport.Size = new System.Drawing.Size(156, 13);
            this.linkImport.TabIndex = 31;
            this.linkImport.TabStop = true;
            this.linkImport.Text = "Импортировать OmniStix";
            this.linkImport.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkImport_LinkClicked);
            // 
            // btnShare
            // 
            this.btnShare.AutoSize = true;
            this.btnShare.Location = new System.Drawing.Point(10, 386);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(79, 13);
            this.btnShare.TabIndex = 7;
            this.btnShare.TabStop = true;
            this.btnShare.Text = "Share OmniStix";
            this.btnShare.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnShare_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBrowse.Location = new System.Drawing.Point(306, 26);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 23);
            this.btnBrowse.TabIndex = 33;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelShare
            // 
            this.panelShare.Controls.Add(this.txtDataPath);
            this.panelShare.Controls.Add(this.btnBrowse);
            this.panelShare.Controls.Add(this.lblSharedPath);
            this.panelShare.Controls.Add(this.linkImport);
            this.panelShare.Controls.Add(this.linkSystemPath);
            this.panelShare.Controls.Add(this.linkExport);
            this.panelShare.Location = new System.Drawing.Point(10, 402);
            this.panelShare.Name = "panelShare";
            this.panelShare.Size = new System.Drawing.Size(345, 77);
            this.panelShare.TabIndex = 34;
            this.panelShare.Visible = false;
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(242, 360);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(6, 10);
            this.p1.TabIndex = 35;
            this.p1.TabStop = false;
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(360, 521);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.panelShare);
            this.Controls.Add(this.btnShare);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.numSaveMaps);
            this.Controls.Add(this.chSaveMaps);
            this.Controls.Add(this.btnManageAutoWidth);
            this.Controls.Add(this.chTopicAutoWidth);
            this.Controls.Add(this.chOpenInOmniBrowser);
            this.Controls.Add(this.FaviconsLinksWindow);
            this.Controls.Add(this.FaviconsToolStix);
            this.Controls.Add(this.gbScaleFactor);
            this.Controls.Add(this.gbRunAtStart);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.numSaveMaps)).EndInit();
            this.panelShare.ResumeLayout(false);
            this.panelShare.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ListView listRunAtStart;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.GroupBox gbRunAtStart;
        private System.Windows.Forms.GroupBox gbScaleFactor;
        private System.Windows.Forms.MaskedTextBox numStix;
        private System.Windows.Forms.Label lblBoxes;
        private System.Windows.Forms.Label lblStixBase;
        private System.Windows.Forms.Label lblStix;
        private System.Windows.Forms.MaskedTextBox numBoxes;
        private System.Windows.Forms.MaskedTextBox numStixBase;
        private System.Windows.Forms.ComboBox cbStix;
        private System.Windows.Forms.ComboBox cbStixBase;
        private System.Windows.Forms.ComboBox cbBoxes;
        private System.Windows.Forms.Button btnTestScale;
        private System.Windows.Forms.CheckBox FaviconsToolStix;
        private System.Windows.Forms.CheckBox FaviconsLinksWindow;
        private System.Windows.Forms.CheckBox chOpenInOmniBrowser;
        private System.Windows.Forms.CheckBox chTopicAutoWidth;
        private System.Windows.Forms.LinkLabel btnManageAutoWidth;
        private System.Windows.Forms.CheckBox chSaveMaps;
        private System.Windows.Forms.NumericUpDown numSaveMaps;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.LinkLabel linkExport;
        private System.Windows.Forms.LinkLabel linkSystemPath;
        private System.Windows.Forms.TextBox txtDataPath;
        private System.Windows.Forms.Label lblSharedPath;
        private System.Windows.Forms.LinkLabel linkImport;
        private System.Windows.Forms.LinkLabel btnShare;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel panelShare;
        private System.Windows.Forms.PictureBox p1;
    }
}