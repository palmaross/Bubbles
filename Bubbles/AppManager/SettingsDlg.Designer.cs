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
            this.gbRunAtStart.SuspendLayout();
            this.gbScaleFactor.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(236, 363);
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
            this.btnSave.Location = new System.Drawing.Point(155, 363);
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
            this.listRunAtStart.Size = new System.Drawing.Size(276, 84);
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
            this.gbRunAtStart.Size = new System.Drawing.Size(299, 134);
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
            this.gbScaleFactor.Size = new System.Drawing.Size(299, 78);
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
            this.btnManageAutoWidth.AutoSize = true;
            this.btnManageAutoWidth.Location = new System.Drawing.Point(194, 331);
            this.btnManageAutoWidth.Name = "btnManageAutoWidth";
            this.btnManageAutoWidth.Size = new System.Drawing.Size(107, 13);
            this.btnManageAutoWidth.TabIndex = 26;
            this.btnManageAutoWidth.TabStop = true;
            this.btnManageAutoWidth.Text = "Manage Auto-Widths";
            this.btnManageAutoWidth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnManageAutoWidth_LinkClicked);
            // 
            // SettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(322, 395);
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
    }
}