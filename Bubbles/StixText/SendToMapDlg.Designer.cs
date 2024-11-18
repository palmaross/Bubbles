namespace Bubbles
{
    partial class SendToMapDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendToMapDlg));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelSuccess = new System.Windows.Forms.Panel();
            this.lblSuccessMessage = new System.Windows.Forms.Label();
            this.pSuccess = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelManage = new System.Windows.Forms.Panel();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.MM = new System.Windows.Forms.PictureBox();
            this.PasteLink = new System.Windows.Forms.PictureBox();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.OptionInternalLinks = new System.Windows.Forms.PictureBox();
            this.OptionSourceLink = new System.Windows.Forms.PictureBox();
            this.OptionTextFormat = new System.Windows.Forms.PictureBox();
            this.OptionMultipleTopics = new System.Windows.Forms.PictureBox();
            this.PasteNotes = new System.Windows.Forms.PictureBox();
            this.subtopic = new System.Windows.Forms.PictureBox();
            this.OptionReplaceInsert = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.NodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cm_openMap = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_goToTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_showSubtopics = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_addReceivingTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.m_saveOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAddTopic = new System.Windows.Forms.Label();
            this.txtAddTopic = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmsOmniLinks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelMinimized = new System.Windows.Forms.Panel();
            this.treeView1 = new Bubbles.TreeViewNonHSB();
            this.panelButtons.SuspendLayout();
            this.panelSuccess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pSuccess)).BeginInit();
            this.panelManage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).BeginInit();
            this.panelOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionInternalLinks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionSourceLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionTextFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionMultipleTopics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionReplaceInsert)).BeginInit();
            this.NodeMenu.SuspendLayout();
            this.cmsManage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.panelSuccess);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 230);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(295, 36);
            this.panelButtons.TabIndex = 1;
            // 
            // panelSuccess
            // 
            this.panelSuccess.Controls.Add(this.lblSuccessMessage);
            this.panelSuccess.Controls.Add(this.pSuccess);
            this.panelSuccess.Location = new System.Drawing.Point(10, 7);
            this.panelSuccess.Name = "panelSuccess";
            this.panelSuccess.Size = new System.Drawing.Size(192, 22);
            this.panelSuccess.TabIndex = 1;
            this.panelSuccess.Visible = false;
            // 
            // lblSuccessMessage
            // 
            this.lblSuccessMessage.AutoSize = true;
            this.lblSuccessMessage.Location = new System.Drawing.Point(32, 5);
            this.lblSuccessMessage.Name = "lblSuccessMessage";
            this.lblSuccessMessage.Size = new System.Drawing.Size(100, 13);
            this.lblSuccessMessage.TabIndex = 2;
            this.lblSuccessMessage.Text = "Pasted successfully";
            // 
            // pSuccess
            // 
            this.pSuccess.BackColor = System.Drawing.Color.Transparent;
            this.pSuccess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pSuccess.Image = ((System.Drawing.Image)(resources.GetObject("pSuccess.Image")));
            this.pSuccess.Location = new System.Drawing.Point(3, 1);
            this.pSuccess.Name = "pSuccess";
            this.pSuccess.Size = new System.Drawing.Size(20, 20);
            this.pSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pSuccess.TabIndex = 115;
            this.pSuccess.TabStop = false;
            this.pSuccess.Tag = "unformatted";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(208, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelManage
            // 
            this.panelManage.Controls.Add(this.Manage);
            this.panelManage.Controls.Add(this.MM);
            this.panelManage.Controls.Add(this.PasteLink);
            this.panelManage.Controls.Add(this.panelOptions);
            this.panelManage.Controls.Add(this.PasteNotes);
            this.panelManage.Controls.Add(this.subtopic);
            this.panelManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelManage.Location = new System.Drawing.Point(0, 0);
            this.panelManage.Name = "panelManage";
            this.panelManage.Size = new System.Drawing.Size(295, 32);
            this.panelManage.TabIndex = 2;
            // 
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(269, 6);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(20, 20);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 114;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // MM
            // 
            this.MM.BackColor = System.Drawing.Color.OldLace;
            this.MM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MM.Image = ((System.Drawing.Image)(resources.GetObject("MM.Image")));
            this.MM.Location = new System.Drawing.Point(99, 6);
            this.MM.Name = "MM";
            this.MM.Size = new System.Drawing.Size(20, 20);
            this.MM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MM.TabIndex = 113;
            this.MM.TabStop = false;
            this.MM.Tag = "unformatted";
            this.MM.Click += new System.EventHandler(this.MM_Click);
            // 
            // PasteLink
            // 
            this.PasteLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasteLink.Image = ((System.Drawing.Image)(resources.GetObject("PasteLink.Image")));
            this.PasteLink.Location = new System.Drawing.Point(32, 6);
            this.PasteLink.Name = "PasteLink";
            this.PasteLink.Size = new System.Drawing.Size(20, 20);
            this.PasteLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PasteLink.TabIndex = 111;
            this.PasteLink.TabStop = false;
            this.PasteLink.Tag = "1";
            this.PasteLink.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PasteLink_MouseClick);
            // 
            // panelOptions
            // 
            this.panelOptions.BackColor = System.Drawing.Color.OldLace;
            this.panelOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOptions.Controls.Add(this.OptionInternalLinks);
            this.panelOptions.Controls.Add(this.OptionSourceLink);
            this.panelOptions.Controls.Add(this.OptionTextFormat);
            this.panelOptions.Controls.Add(this.OptionMultipleTopics);
            this.panelOptions.Location = new System.Drawing.Point(149, 5);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(99, 24);
            this.panelOptions.TabIndex = 110;
            // 
            // OptionInternalLinks
            // 
            this.OptionInternalLinks.BackColor = System.Drawing.Color.OldLace;
            this.OptionInternalLinks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionInternalLinks.Image = ((System.Drawing.Image)(resources.GetObject("OptionInternalLinks.Image")));
            this.OptionInternalLinks.Location = new System.Drawing.Point(75, 2);
            this.OptionInternalLinks.Name = "OptionInternalLinks";
            this.OptionInternalLinks.Size = new System.Drawing.Size(18, 18);
            this.OptionInternalLinks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionInternalLinks.TabIndex = 112;
            this.OptionInternalLinks.TabStop = false;
            this.OptionInternalLinks.Tag = "no";
            this.OptionInternalLinks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OptionButton_MouseClick);
            // 
            // OptionSourceLink
            // 
            this.OptionSourceLink.BackColor = System.Drawing.Color.OldLace;
            this.OptionSourceLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionSourceLink.Image = ((System.Drawing.Image)(resources.GetObject("OptionSourceLink.Image")));
            this.OptionSourceLink.Location = new System.Drawing.Point(51, 2);
            this.OptionSourceLink.Name = "OptionSourceLink";
            this.OptionSourceLink.Size = new System.Drawing.Size(18, 18);
            this.OptionSourceLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionSourceLink.TabIndex = 111;
            this.OptionSourceLink.TabStop = false;
            this.OptionSourceLink.Tag = "no";
            this.OptionSourceLink.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OptionButton_MouseClick);
            // 
            // OptionTextFormat
            // 
            this.OptionTextFormat.BackColor = System.Drawing.Color.OldLace;
            this.OptionTextFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionTextFormat.Image = ((System.Drawing.Image)(resources.GetObject("OptionTextFormat.Image")));
            this.OptionTextFormat.Location = new System.Drawing.Point(3, 2);
            this.OptionTextFormat.Name = "OptionTextFormat";
            this.OptionTextFormat.Size = new System.Drawing.Size(18, 18);
            this.OptionTextFormat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionTextFormat.TabIndex = 107;
            this.OptionTextFormat.TabStop = false;
            this.OptionTextFormat.Tag = "unformatted";
            this.OptionTextFormat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OptionButton_MouseClick);
            // 
            // OptionMultipleTopics
            // 
            this.OptionMultipleTopics.BackColor = System.Drawing.Color.OldLace;
            this.OptionMultipleTopics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionMultipleTopics.Image = ((System.Drawing.Image)(resources.GetObject("OptionMultipleTopics.Image")));
            this.OptionMultipleTopics.Location = new System.Drawing.Point(27, 2);
            this.OptionMultipleTopics.Name = "OptionMultipleTopics";
            this.OptionMultipleTopics.Size = new System.Drawing.Size(18, 18);
            this.OptionMultipleTopics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionMultipleTopics.TabIndex = 109;
            this.OptionMultipleTopics.TabStop = false;
            this.OptionMultipleTopics.Tag = "single";
            this.OptionMultipleTopics.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OptionButton_MouseClick);
            // 
            // PasteNotes
            // 
            this.PasteNotes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasteNotes.Image = ((System.Drawing.Image)(resources.GetObject("PasteNotes.Image")));
            this.PasteNotes.Location = new System.Drawing.Point(57, 6);
            this.PasteNotes.Name = "PasteNotes";
            this.PasteNotes.Size = new System.Drawing.Size(20, 20);
            this.PasteNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PasteNotes.TabIndex = 106;
            this.PasteNotes.TabStop = false;
            this.PasteNotes.Tag = "1";
            this.PasteNotes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PasteNotes_MouseClick);
            // 
            // subtopic
            // 
            this.subtopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.subtopic.Image = ((System.Drawing.Image)(resources.GetObject("subtopic.Image")));
            this.subtopic.Location = new System.Drawing.Point(7, 6);
            this.subtopic.Name = "subtopic";
            this.subtopic.Size = new System.Drawing.Size(20, 20);
            this.subtopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.subtopic.TabIndex = 100;
            this.subtopic.TabStop = false;
            this.subtopic.Tag = "1";
            this.subtopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.subtopic_MouseClick);
            // 
            // OptionReplaceInsert
            // 
            this.OptionReplaceInsert.BackColor = System.Drawing.Color.OldLace;
            this.OptionReplaceInsert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionReplaceInsert.Image = ((System.Drawing.Image)(resources.GetObject("OptionReplaceInsert.Image")));
            this.OptionReplaceInsert.Location = new System.Drawing.Point(271, 60);
            this.OptionReplaceInsert.Name = "OptionReplaceInsert";
            this.OptionReplaceInsert.Size = new System.Drawing.Size(18, 18);
            this.OptionReplaceInsert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionReplaceInsert.TabIndex = 108;
            this.OptionReplaceInsert.TabStop = false;
            this.OptionReplaceInsert.Tag = "replace";
            this.OptionReplaceInsert.Visible = false;
            this.OptionReplaceInsert.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OptionButton_MouseClick);
            // 
            // NodeMenu
            // 
            this.NodeMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.NodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cm_openMap,
            this.cm_goToTopic,
            this.cm_Remove,
            this.cm_rename,
            this.cm_showSubtopics});
            this.NodeMenu.Name = "NodeMenu";
            this.NodeMenu.ShowImageMargin = false;
            this.NodeMenu.Size = new System.Drawing.Size(156, 136);
            // 
            // cm_openMap
            // 
            this.cm_openMap.Name = "cm_openMap";
            this.cm_openMap.Size = new System.Drawing.Size(155, 22);
            this.cm_openMap.Text = "Open Map";
            this.cm_openMap.Click += new System.EventHandler(this.cm_openMap_Click);
            // 
            // cm_goToTopic
            // 
            this.cm_goToTopic.Name = "cm_goToTopic";
            this.cm_goToTopic.Size = new System.Drawing.Size(155, 22);
            this.cm_goToTopic.Text = "Go to Topic";
            this.cm_goToTopic.Click += new System.EventHandler(this.cm_goToTopic_Click);
            // 
            // cm_Remove
            // 
            this.cm_Remove.Name = "cm_Remove";
            this.cm_Remove.Size = new System.Drawing.Size(155, 22);
            this.cm_Remove.Text = "Remove";
            this.cm_Remove.Click += new System.EventHandler(this.cm_Remove_Click);
            // 
            // cm_rename
            // 
            this.cm_rename.Name = "cm_rename";
            this.cm_rename.Size = new System.Drawing.Size(155, 22);
            this.cm_rename.Text = "Rename";
            this.cm_rename.Click += new System.EventHandler(this.cm_rename_Click);
            // 
            // cm_showSubtopics
            // 
            this.cm_showSubtopics.Name = "cm_showSubtopics";
            this.cm_showSubtopics.Size = new System.Drawing.Size(155, 22);
            this.cm_showSubtopics.Text = "Show Subtopics";
            this.cm_showSubtopics.Click += new System.EventHandler(this.cm_showSubtopics_Click);
            // 
            // cmsManage
            // 
            this.cmsManage.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_addReceivingTopic,
            this.m_saveOptions});
            this.cmsManage.Name = "cmsManage";
            this.cmsManage.ShowImageMargin = false;
            this.cmsManage.Size = new System.Drawing.Size(157, 48);
            // 
            // m_addReceivingTopic
            // 
            this.m_addReceivingTopic.Name = "m_addReceivingTopic";
            this.m_addReceivingTopic.Size = new System.Drawing.Size(156, 22);
            this.m_addReceivingTopic.Text = "Add Receiving Topic";
            this.m_addReceivingTopic.Click += new System.EventHandler(this.m_addReceivingTopic_Click);
            // 
            // m_saveOptions
            // 
            this.m_saveOptions.Name = "m_saveOptions";
            this.m_saveOptions.Size = new System.Drawing.Size(156, 22);
            this.m_saveOptions.Text = "Save Options";
            this.m_saveOptions.Click += new System.EventHandler(this.m_saveOptions_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAddTopic);
            this.panel1.Controls.Add(this.txtAddTopic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 22);
            this.panel1.TabIndex = 5;
            // 
            // lblAddTopic
            // 
            this.lblAddTopic.AutoSize = true;
            this.lblAddTopic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAddTopic.Location = new System.Drawing.Point(6, 3);
            this.lblAddTopic.Name = "lblAddTopic";
            this.lblAddTopic.Size = new System.Drawing.Size(60, 13);
            this.lblAddTopic.TabIndex = 6;
            this.lblAddTopic.Text = "Добавить:";
            // 
            // txtAddTopic
            // 
            this.txtAddTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddTopic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddTopic.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtAddTopic.Location = new System.Drawing.Point(67, 1);
            this.txtAddTopic.Name = "txtAddTopic";
            this.txtAddTopic.Size = new System.Drawing.Size(222, 21);
            this.txtAddTopic.TabIndex = 0;
            this.txtAddTopic.Text = "Add topic text and press Enter";
            this.txtAddTopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtAddTopic_MouseClick);
            this.txtAddTopic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddTopic_KeyDown);
            this.txtAddTopic.Leave += new System.EventHandler(this.txtAddTopic_Leave);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmsOmniLinks
            // 
            this.cmsOmniLinks.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsOmniLinks.Name = "cmsOmniLinks";
            this.cmsOmniLinks.ShowImageMargin = false;
            this.cmsOmniLinks.Size = new System.Drawing.Size(36, 4);
            // 
            // panelMinimized
            // 
            this.panelMinimized.Location = new System.Drawing.Point(18, 114);
            this.panelMinimized.Name = "panelMinimized";
            this.panelMinimized.Size = new System.Drawing.Size(260, 37);
            this.panelMinimized.TabIndex = 109;
            this.panelMinimized.Visible = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(0, 54);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(295, 176);
            this.treeView1.TabIndex = 4;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // SendToMapDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(295, 266);
            this.Controls.Add(this.panelMinimized);
            this.Controls.Add(this.OptionReplaceInsert);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelManage);
            this.Controls.Add(this.panelButtons);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendToMapDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManageSendInfoDlg";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendToMapDlg_FormClosing);
            this.Load += new System.EventHandler(this.SendToMapDlg_Load);
            this.panelButtons.ResumeLayout(false);
            this.panelSuccess.ResumeLayout(false);
            this.panelSuccess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pSuccess)).EndInit();
            this.panelManage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).EndInit();
            this.panelOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionInternalLinks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionSourceLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionTextFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionMultipleTopics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionReplaceInsert)).EndInit();
            this.NodeMenu.ResumeLayout(false);
            this.cmsManage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelManage;
        public System.Windows.Forms.PictureBox subtopic;
        public System.Windows.Forms.PictureBox OptionInternalLinks;
        public System.Windows.Forms.PictureBox OptionSourceLink;
        public System.Windows.Forms.PictureBox OptionTextFormat;
        public System.Windows.Forms.PictureBox OptionReplaceInsert;
        public System.Windows.Forms.PictureBox OptionMultipleTopics;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.PictureBox PasteLink;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ContextMenuStrip NodeMenu;
        private System.Windows.Forms.ToolStripMenuItem cm_Remove;
        private System.Windows.Forms.ToolStripMenuItem cm_openMap;
        private System.Windows.Forms.ToolStripMenuItem cm_rename;
        private System.Windows.Forms.ToolStripMenuItem cm_goToTopic;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.PictureBox MM;
        private System.Windows.Forms.ToolStripMenuItem cm_showSubtopics;
        public System.Windows.Forms.PictureBox PasteNotes;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.ContextMenuStrip cmsManage;
        private System.Windows.Forms.ToolStripMenuItem m_saveOptions;
        private System.Windows.Forms.ToolStripMenuItem m_addReceivingTopic;
        private TreeViewNonHSB treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAddTopic;
        private System.Windows.Forms.Label lblAddTopic;
        private System.Windows.Forms.Panel panelSuccess;
        public System.Windows.Forms.PictureBox pSuccess;
        private System.Windows.Forms.Label lblSuccessMessage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip cmsOmniLinks;
        private System.Windows.Forms.Panel panelMinimized;
    }
}