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
            this.lblFontSize = new System.Windows.Forms.Label();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblPx = new System.Windows.Forms.Label();
            this.gbRunAtStart.SuspendLayout();
            this.gbScaleFactor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSaveMaps)).BeginInit();
            this.panelShare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(548, 996);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 44);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(20, 996);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 44);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // listRunAtStart
            // 
            this.listRunAtStart.CheckBoxes = true;
            this.listRunAtStart.HideSelection = false;
            this.listRunAtStart.Location = new System.Drawing.Point(24, 81);
            this.listRunAtStart.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listRunAtStart.Name = "listRunAtStart";
            this.listRunAtStart.Size = new System.Drawing.Size(620, 158);
            this.listRunAtStart.TabIndex = 14;
            this.listRunAtStart.UseCompatibleStateImageBehavior = false;
            this.listRunAtStart.View = System.Windows.Forms.View.SmallIcon;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(26, 42);
            this.cbSelectAll.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(259, 29);
            this.cbSelectAll.TabIndex = 15;
            this.cbSelectAll.Text = "Выбрать/снять выбор";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // gbRunAtStart
            // 
            this.gbRunAtStart.Controls.Add(this.cbSelectAll);
            this.gbRunAtStart.Controls.Add(this.listRunAtStart);
            this.gbRunAtStart.Location = new System.Drawing.Point(24, 19);
            this.gbRunAtStart.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbRunAtStart.Name = "gbRunAtStart";
            this.gbRunAtStart.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbRunAtStart.Size = new System.Drawing.Size(670, 258);
            this.gbRunAtStart.TabIndex = 16;
            this.gbRunAtStart.TabStop = false;
            this.gbRunAtStart.Text = "Запускать при старте MindManager:";
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
            this.gbScaleFactor.Location = new System.Drawing.Point(24, 304);
            this.gbScaleFactor.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbScaleFactor.Name = "gbScaleFactor";
            this.gbScaleFactor.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbScaleFactor.Size = new System.Drawing.Size(670, 150);
            this.gbScaleFactor.TabIndex = 17;
            this.gbScaleFactor.TabStop = false;
            this.gbScaleFactor.Text = "Scale Factor (allowed values: 100 to 300%)";
            // 
            // btnTestScale
            // 
            this.btnTestScale.Location = new System.Drawing.Point(378, 60);
            this.btnTestScale.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnTestScale.Name = "btnTestScale";
            this.btnTestScale.Size = new System.Drawing.Size(140, 52);
            this.btnTestScale.TabIndex = 19;
            this.btnTestScale.Text = "Test";
            this.btnTestScale.UseVisualStyleBackColor = true;
            this.btnTestScale.Click += new System.EventHandler(this.btnTestScale_Click);
            // 
            // numStixBase
            // 
            this.numStixBase.Location = new System.Drawing.Point(188, 90);
            this.numStixBase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numStixBase.Mask = "000%";
            this.numStixBase.Name = "numStixBase";
            this.numStixBase.Size = new System.Drawing.Size(74, 31);
            this.numStixBase.TabIndex = 4;
            this.numStixBase.Text = "100";
            this.numStixBase.ValidatingType = typeof(int);
            this.numStixBase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numStix_KeyDown);
            this.numStixBase.Leave += new System.EventHandler(this.numStix_Leave);
            // 
            // numStix
            // 
            this.numStix.Location = new System.Drawing.Point(188, 42);
            this.numStix.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numStix.Mask = "009%";
            this.numStix.Name = "numStix";
            this.numStix.Size = new System.Drawing.Size(74, 31);
            this.numStix.TabIndex = 3;
            this.numStix.Text = "100";
            this.numStix.ValidatingType = typeof(int);
            this.numStix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numStix_KeyDown);
            this.numStix.Leave += new System.EventHandler(this.numStix_Leave);
            // 
            // lblStixBase
            // 
            this.lblStixBase.AutoSize = true;
            this.lblStixBase.Location = new System.Drawing.Point(20, 98);
            this.lblStixBase.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStixBase.Name = "lblStixBase";
            this.lblStixBase.Size = new System.Drawing.Size(109, 25);
            this.lblStixBase.TabIndex = 1;
            this.lblStixBase.Text = "Stix Base:";
            // 
            // lblStix
            // 
            this.lblStix.AutoSize = true;
            this.lblStix.Location = new System.Drawing.Point(20, 54);
            this.lblStix.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStix.Name = "lblStix";
            this.lblStix.Size = new System.Drawing.Size(54, 25);
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
            this.cbStix.Location = new System.Drawing.Point(186, 40);
            this.cbStix.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbStix.Name = "cbStix";
            this.cbStix.Size = new System.Drawing.Size(112, 33);
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
            this.cbStixBase.Location = new System.Drawing.Point(186, 88);
            this.cbStixBase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbStixBase.Name = "cbStixBase";
            this.cbStixBase.Size = new System.Drawing.Size(112, 33);
            this.cbStixBase.TabIndex = 11;
            this.cbStixBase.SelectedIndexChanged += new System.EventHandler(this.cbStix_SelectedIndexChanged);
            // 
            // FaviconsToolStix
            // 
            this.FaviconsToolStix.Checked = true;
            this.FaviconsToolStix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FaviconsToolStix.Location = new System.Drawing.Point(20, 469);
            this.FaviconsToolStix.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FaviconsToolStix.Name = "FaviconsToolStix";
            this.FaviconsToolStix.Size = new System.Drawing.Size(594, 58);
            this.FaviconsToolStix.TabIndex = 22;
            this.FaviconsToolStix.Text = "Show favicons for Web links on the ToolStix";
            this.FaviconsToolStix.UseVisualStyleBackColor = true;
            // 
            // FaviconsLinksWindow
            // 
            this.FaviconsLinksWindow.AutoSize = true;
            this.FaviconsLinksWindow.Checked = true;
            this.FaviconsLinksWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FaviconsLinksWindow.Location = new System.Drawing.Point(20, 525);
            this.FaviconsLinksWindow.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.FaviconsLinksWindow.Name = "FaviconsLinksWindow";
            this.FaviconsLinksWindow.Size = new System.Drawing.Size(476, 29);
            this.FaviconsLinksWindow.TabIndex = 23;
            this.FaviconsLinksWindow.Text = "Show favicons for Web links in Links Window";
            this.FaviconsLinksWindow.UseVisualStyleBackColor = true;
            // 
            // chOpenInOmniBrowser
            // 
            this.chOpenInOmniBrowser.Location = new System.Drawing.Point(20, 567);
            this.chOpenInOmniBrowser.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chOpenInOmniBrowser.Name = "chOpenInOmniBrowser";
            this.chOpenInOmniBrowser.Size = new System.Drawing.Size(594, 58);
            this.chOpenInOmniBrowser.TabIndex = 24;
            this.chOpenInOmniBrowser.Text = "ToolStix: open web-links in the OmniBrowser by default";
            this.chOpenInOmniBrowser.UseVisualStyleBackColor = true;
            // 
            // chTopicAutoWidth
            // 
            this.chTopicAutoWidth.AutoSize = true;
            this.chTopicAutoWidth.Location = new System.Drawing.Point(20, 635);
            this.chTopicAutoWidth.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chTopicAutoWidth.Name = "chTopicAutoWidth";
            this.chTopicAutoWidth.Size = new System.Drawing.Size(349, 29);
            this.chTopicAutoWidth.TabIndex = 25;
            this.chTopicAutoWidth.Text = "Автоматическая ширина темы";
            this.chTopicAutoWidth.UseVisualStyleBackColor = true;
            // 
            // btnManageAutoWidth
            // 
            this.btnManageAutoWidth.AutoSize = true;
            this.btnManageAutoWidth.Location = new System.Drawing.Point(446, 637);
            this.btnManageAutoWidth.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnManageAutoWidth.Name = "btnManageAutoWidth";
            this.btnManageAutoWidth.Size = new System.Drawing.Size(213, 25);
            this.btnManageAutoWidth.TabIndex = 26;
            this.btnManageAutoWidth.TabStop = true;
            this.btnManageAutoWidth.Text = "Manage Auto-Widths";
            this.btnManageAutoWidth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnManageAutoWidth_LinkClicked);
            // 
            // chSaveMaps
            // 
            this.chSaveMaps.AutoSize = true;
            this.chSaveMaps.Location = new System.Drawing.Point(20, 690);
            this.chSaveMaps.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chSaveMaps.Name = "chSaveMaps";
            this.chSaveMaps.Size = new System.Drawing.Size(402, 29);
            this.chSaveMaps.TabIndex = 27;
            this.chSaveMaps.Text = "Сохранять открытые карты каждые";
            this.chSaveMaps.UseVisualStyleBackColor = true;
            // 
            // numSaveMaps
            // 
            this.numSaveMaps.Location = new System.Drawing.Point(452, 687);
            this.numSaveMaps.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
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
            this.numSaveMaps.Size = new System.Drawing.Size(64, 31);
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
            this.lblMin.Location = new System.Drawing.Point(528, 692);
            this.lblMin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(52, 25);
            this.lblMin.TabIndex = 20;
            this.lblMin.Text = "min.";
            // 
            // linkExport
            // 
            this.linkExport.AutoSize = true;
            this.linkExport.Location = new System.Drawing.Point(0, 112);
            this.linkExport.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.linkExport.Name = "linkExport";
            this.linkExport.Size = new System.Drawing.Size(267, 25);
            this.linkExport.TabIndex = 29;
            this.linkExport.TabStop = true;
            this.linkExport.Text = "Экспортировать OmniStix";
            this.linkExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkExport_LinkClicked);
            // 
            // linkSystemPath
            // 
            this.linkSystemPath.Location = new System.Drawing.Point(380, 19);
            this.linkSystemPath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.linkSystemPath.Name = "linkSystemPath";
            this.linkSystemPath.Size = new System.Drawing.Size(228, 25);
            this.linkSystemPath.TabIndex = 6;
            this.linkSystemPath.TabStop = true;
            this.linkSystemPath.Text = "System Path";
            this.linkSystemPath.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkSystemPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSystemPath_LinkClicked);
            // 
            // txtDataPath
            // 
            this.txtDataPath.Location = new System.Drawing.Point(4, 54);
            this.txtDataPath.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDataPath.Name = "txtDataPath";
            this.txtDataPath.ReadOnly = true;
            this.txtDataPath.Size = new System.Drawing.Size(596, 31);
            this.txtDataPath.TabIndex = 1;
            // 
            // lblSharedPath
            // 
            this.lblSharedPath.AutoSize = true;
            this.lblSharedPath.Location = new System.Drawing.Point(2, 19);
            this.lblSharedPath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSharedPath.Name = "lblSharedPath";
            this.lblSharedPath.Size = new System.Drawing.Size(143, 25);
            this.lblSharedPath.TabIndex = 0;
            this.lblSharedPath.Text = " Shared Path:";
            // 
            // linkImport
            // 
            this.linkImport.Location = new System.Drawing.Point(366, 112);
            this.linkImport.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.linkImport.Name = "linkImport";
            this.linkImport.Size = new System.Drawing.Size(312, 25);
            this.linkImport.TabIndex = 31;
            this.linkImport.TabStop = true;
            this.linkImport.Text = "Импортировать OmniStix";
            this.linkImport.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkImport_LinkClicked);
            // 
            // btnShare
            // 
            this.btnShare.AutoSize = true;
            this.btnShare.Location = new System.Drawing.Point(20, 800);
            this.btnShare.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(161, 25);
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
            this.btnBrowse.Location = new System.Drawing.Point(612, 50);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(64, 44);
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
            this.panelShare.Location = new System.Drawing.Point(20, 831);
            this.panelShare.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelShare.Name = "panelShare";
            this.panelShare.Size = new System.Drawing.Size(690, 148);
            this.panelShare.TabIndex = 34;
            this.panelShare.Visible = false;
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(18, 748);
            this.lblFontSize.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(278, 25);
            this.lblFontSize.TabIndex = 20;
            this.lblFontSize.Text = "Font Size in dialog windows";
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(452, 744);
            this.numFontSize.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numFontSize.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numFontSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(64, 31);
            this.numFontSize.TabIndex = 36;
            this.numFontSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // lblPx
            // 
            this.lblPx.AutoSize = true;
            this.lblPx.Location = new System.Drawing.Point(528, 748);
            this.lblPx.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPx.Name = "lblPx";
            this.lblPx.Size = new System.Drawing.Size(41, 25);
            this.lblPx.TabIndex = 37;
            this.lblPx.Text = "px.";
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(720, 1060);
            this.Controls.Add(this.lblPx);
            this.Controls.Add(this.numFontSize);
            this.Controls.Add(this.lblFontSize);
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
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
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
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
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
        private System.Windows.Forms.Label lblStixBase;
        private System.Windows.Forms.Label lblStix;
        private System.Windows.Forms.MaskedTextBox numStixBase;
        private System.Windows.Forms.ComboBox cbStix;
        private System.Windows.Forms.ComboBox cbStixBase;
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
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Label lblPx;
    }
}