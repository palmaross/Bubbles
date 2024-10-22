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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnAddTopic = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelManage = new System.Windows.Forms.Panel();
            this.PasteLink = new System.Windows.Forms.PictureBox();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.OptionInternalLinks = new System.Windows.Forms.PictureBox();
            this.OptionReplaceInsert = new System.Windows.Forms.PictureBox();
            this.OptionSourceLink = new System.Windows.Forms.PictureBox();
            this.OptionTextFormat = new System.Windows.Forms.PictureBox();
            this.OptionMultipleTopics = new System.Windows.Forms.PictureBox();
            this.PasteNotes = new System.Windows.Forms.PictureBox();
            this.subtopic = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.NodeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cm_openMap = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_goToTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_addSubtopic = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.MM = new System.Windows.Forms.PictureBox();
            this.panelButtons.SuspendLayout();
            this.panelManage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).BeginInit();
            this.panelOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OptionInternalLinks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionReplaceInsert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionSourceLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionTextFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionMultipleTopics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtopic)).BeginInit();
            this.NodeMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MM)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.LabelEdit = true;
            this.treeView1.Location = new System.Drawing.Point(0, 32);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(295, 171);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnAddTopic);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 203);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(295, 43);
            this.panelButtons.TabIndex = 1;
            // 
            // btnAddTopic
            // 
            this.btnAddTopic.Location = new System.Drawing.Point(11, 10);
            this.btnAddTopic.Name = "btnAddTopic";
            this.btnAddTopic.Size = new System.Drawing.Size(168, 23);
            this.btnAddTopic.TabIndex = 1;
            this.btnAddTopic.Text = "Добавить тему в базу данных";
            this.btnAddTopic.UseVisualStyleBackColor = true;
            this.btnAddTopic.Click += new System.EventHandler(this.btnAddTopic_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(208, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelManage
            // 
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
            this.PasteLink.Click += new System.EventHandler(this.PasteLink_Click);
            // 
            // panelOptions
            // 
            this.panelOptions.BackColor = System.Drawing.Color.OldLace;
            this.panelOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOptions.Controls.Add(this.OptionInternalLinks);
            this.panelOptions.Controls.Add(this.OptionReplaceInsert);
            this.panelOptions.Controls.Add(this.OptionSourceLink);
            this.panelOptions.Controls.Add(this.OptionTextFormat);
            this.panelOptions.Controls.Add(this.OptionMultipleTopics);
            this.panelOptions.Location = new System.Drawing.Point(172, 5);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(118, 23);
            this.panelOptions.TabIndex = 110;
            // 
            // OptionInternalLinks
            // 
            this.OptionInternalLinks.BackColor = System.Drawing.Color.OldLace;
            this.OptionInternalLinks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionInternalLinks.Image = ((System.Drawing.Image)(resources.GetObject("OptionInternalLinks.Image")));
            this.OptionInternalLinks.Location = new System.Drawing.Point(95, 2);
            this.OptionInternalLinks.Name = "OptionInternalLinks";
            this.OptionInternalLinks.Size = new System.Drawing.Size(18, 18);
            this.OptionInternalLinks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionInternalLinks.TabIndex = 112;
            this.OptionInternalLinks.TabStop = false;
            this.OptionInternalLinks.Tag = "no";
            // 
            // OptionReplaceInsert
            // 
            this.OptionReplaceInsert.BackColor = System.Drawing.Color.OldLace;
            this.OptionReplaceInsert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionReplaceInsert.Image = ((System.Drawing.Image)(resources.GetObject("OptionReplaceInsert.Image")));
            this.OptionReplaceInsert.Location = new System.Drawing.Point(26, 2);
            this.OptionReplaceInsert.Name = "OptionReplaceInsert";
            this.OptionReplaceInsert.Size = new System.Drawing.Size(18, 18);
            this.OptionReplaceInsert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionReplaceInsert.TabIndex = 108;
            this.OptionReplaceInsert.TabStop = false;
            this.OptionReplaceInsert.Tag = "replace";
            // 
            // OptionSourceLink
            // 
            this.OptionSourceLink.BackColor = System.Drawing.Color.OldLace;
            this.OptionSourceLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionSourceLink.Image = ((System.Drawing.Image)(resources.GetObject("OptionSourceLink.Image")));
            this.OptionSourceLink.Location = new System.Drawing.Point(73, 2);
            this.OptionSourceLink.Name = "OptionSourceLink";
            this.OptionSourceLink.Size = new System.Drawing.Size(18, 18);
            this.OptionSourceLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionSourceLink.TabIndex = 111;
            this.OptionSourceLink.TabStop = false;
            this.OptionSourceLink.Tag = "no";
            // 
            // OptionTextFormat
            // 
            this.OptionTextFormat.BackColor = System.Drawing.Color.OldLace;
            this.OptionTextFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OptionTextFormat.Image = ((System.Drawing.Image)(resources.GetObject("OptionTextFormat.Image")));
            this.OptionTextFormat.Location = new System.Drawing.Point(4, 2);
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
            this.OptionMultipleTopics.Location = new System.Drawing.Point(48, 2);
            this.OptionMultipleTopics.Name = "OptionMultipleTopics";
            this.OptionMultipleTopics.Size = new System.Drawing.Size(18, 18);
            this.OptionMultipleTopics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OptionMultipleTopics.TabIndex = 109;
            this.OptionMultipleTopics.TabStop = false;
            this.OptionMultipleTopics.Tag = "single";
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
            this.PasteNotes.Click += new System.EventHandler(this.PasteNotes_Click);
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
            this.subtopic.Click += new System.EventHandler(this.subtopic_Click);
            // 
            // NodeMenu
            // 
            this.NodeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cm_openMap,
            this.cm_goToTopic,
            this.cm_addSubtopic,
            this.cm_Remove,
            this.cm_rename});
            this.NodeMenu.Name = "NodeMenu";
            this.NodeMenu.Size = new System.Drawing.Size(147, 114);
            // 
            // cm_openMap
            // 
            this.cm_openMap.Name = "cm_openMap";
            this.cm_openMap.Size = new System.Drawing.Size(146, 22);
            this.cm_openMap.Text = "Open Map";
            this.cm_openMap.Click += new System.EventHandler(this.cm_openMap_Click);
            // 
            // cm_goToTopic
            // 
            this.cm_goToTopic.Name = "cm_goToTopic";
            this.cm_goToTopic.Size = new System.Drawing.Size(146, 22);
            this.cm_goToTopic.Text = "Go to Topic";
            this.cm_goToTopic.Click += new System.EventHandler(this.cm_goToTopic_Click);
            // 
            // cm_addSubtopic
            // 
            this.cm_addSubtopic.Name = "cm_addSubtopic";
            this.cm_addSubtopic.Size = new System.Drawing.Size(146, 22);
            this.cm_addSubtopic.Text = "Add Subtopic";
            this.cm_addSubtopic.Click += new System.EventHandler(this.cm_addSubtopic_Click);
            // 
            // cm_Remove
            // 
            this.cm_Remove.Name = "cm_Remove";
            this.cm_Remove.Size = new System.Drawing.Size(146, 22);
            this.cm_Remove.Text = "Remove";
            this.cm_Remove.Click += new System.EventHandler(this.cm_Remove_Click);
            // 
            // cm_rename
            // 
            this.cm_rename.Name = "cm_rename";
            this.cm_rename.Size = new System.Drawing.Size(146, 22);
            this.cm_rename.Text = "Rename";
            this.cm_rename.Click += new System.EventHandler(this.cm_rename_Click);
            // 
            // MM
            // 
            this.MM.BackColor = System.Drawing.Color.OldLace;
            this.MM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MM.Image = ((System.Drawing.Image)(resources.GetObject("MM.Image")));
            this.MM.Location = new System.Drawing.Point(118, 8);
            this.MM.Name = "MM";
            this.MM.Size = new System.Drawing.Size(18, 18);
            this.MM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MM.TabIndex = 113;
            this.MM.TabStop = false;
            this.MM.Tag = "unformatted";
            this.MM.Click += new System.EventHandler(this.MM_Click);
            // 
            // SendToMapDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(295, 246);
            this.Controls.Add(this.treeView1);
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
            this.panelButtons.ResumeLayout(false);
            this.panelManage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).EndInit();
            this.panelOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OptionInternalLinks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionReplaceInsert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionSourceLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionTextFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptionMultipleTopics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtopic)).EndInit();
            this.NodeMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelManage;
        public System.Windows.Forms.PictureBox subtopic;
        private System.Windows.Forms.PictureBox PasteNotes;
        public System.Windows.Forms.PictureBox OptionInternalLinks;
        public System.Windows.Forms.PictureBox OptionSourceLink;
        public System.Windows.Forms.PictureBox OptionTextFormat;
        public System.Windows.Forms.PictureBox OptionReplaceInsert;
        public System.Windows.Forms.PictureBox OptionMultipleTopics;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddTopic;
        public System.Windows.Forms.PictureBox PasteLink;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ContextMenuStrip NodeMenu;
        private System.Windows.Forms.ToolStripMenuItem cm_Remove;
        private System.Windows.Forms.ToolStripMenuItem cm_addSubtopic;
        private System.Windows.Forms.ToolStripMenuItem cm_openMap;
        private System.Windows.Forms.ToolStripMenuItem cm_rename;
        private System.Windows.Forms.ToolStripMenuItem cm_goToTopic;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.PictureBox MM;
    }
}