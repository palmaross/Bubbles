namespace Bubbles
{
    partial class MapNavigatorDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapNavigatorDlg));
            this.listBookmarks = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnHelp = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.lblCentralTopic = new System.Windows.Forms.Label();
            this.listMainTopics = new System.Windows.Forms.ListBox();
            this.tabBookmarks = new System.Windows.Forms.TabPage();
            this.linkDeleteAllBookmarks = new System.Windows.Forms.LinkLabel();
            this.linkAddBookmark = new System.Windows.Forms.LinkLabel();
            this.tabNavigation = new System.Windows.Forms.TabPage();
            this.linkDeleteAllPositions = new System.Windows.Forms.LinkLabel();
            this.listPositions = new System.Windows.Forms.ListBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.pRefresh = new System.Windows.Forms.PictureBox();
            this.pDraw = new System.Windows.Forms.PictureBox();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabBookmarks.SuspendLayout();
            this.tabNavigation.SuspendLayout();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBookmarks
            // 
            this.listBookmarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBookmarks.BackColor = System.Drawing.SystemColors.Window;
            this.listBookmarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBookmarks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBookmarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBookmarks.FormattingEnabled = true;
            this.listBookmarks.IntegralHeight = false;
            this.listBookmarks.Location = new System.Drawing.Point(1, 2);
            this.listBookmarks.Name = "listBookmarks";
            this.listBookmarks.Size = new System.Drawing.Size(212, 70);
            this.listBookmarks.TabIndex = 0;
            this.listBookmarks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBookmarks_MouseUp);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Magenta;
            this.label1.Location = new System.Drawing.Point(174, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 73;
            this.label1.Text = "высота22";
            this.label1.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(211, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 79;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(191, 1);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(16, 16);
            this.btnHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHelp.TabIndex = 80;
            this.btnHelp.TabStop = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabBookmarks);
            this.tabControl1.Controls.Add(this.tabNavigation);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(1, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(230, 117);
            this.tabControl1.TabIndex = 82;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabMain
            // 
            this.tabMain.BackColor = System.Drawing.SystemColors.Window;
            this.tabMain.Controls.Add(this.lblCentralTopic);
            this.tabMain.Controls.Add(this.label1);
            this.tabMain.Controls.Add(this.listMainTopics);
            this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.Location = new System.Drawing.Point(4, 24);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(222, 89);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main Topics";
            // 
            // lblCentralTopic
            // 
            this.lblCentralTopic.AutoSize = true;
            this.lblCentralTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCentralTopic.Location = new System.Drawing.Point(1, 0);
            this.lblCentralTopic.Name = "lblCentralTopic";
            this.lblCentralTopic.Size = new System.Drawing.Size(92, 15);
            this.lblCentralTopic.TabIndex = 2;
            this.lblCentralTopic.Text = "Central Topic";
            this.lblCentralTopic.Click += new System.EventHandler(this.lblCentralTopic_Click);
            // 
            // listMainTopics
            // 
            this.listMainTopics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMainTopics.BackColor = System.Drawing.SystemColors.Window;
            this.listMainTopics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listMainTopics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listMainTopics.FormattingEnabled = true;
            this.listMainTopics.IntegralHeight = false;
            this.listMainTopics.ItemHeight = 15;
            this.listMainTopics.Location = new System.Drawing.Point(1, 16);
            this.listMainTopics.Name = "listMainTopics";
            this.listMainTopics.Size = new System.Drawing.Size(222, 73);
            this.listMainTopics.TabIndex = 1;
            this.listMainTopics.SelectedIndexChanged += new System.EventHandler(this.listMainTopics_SelectedIndexChanged);
            // 
            // tabBookmarks
            // 
            this.tabBookmarks.Controls.Add(this.linkDeleteAllBookmarks);
            this.tabBookmarks.Controls.Add(this.linkAddBookmark);
            this.tabBookmarks.Controls.Add(this.listBookmarks);
            this.tabBookmarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabBookmarks.Location = new System.Drawing.Point(4, 24);
            this.tabBookmarks.Name = "tabBookmarks";
            this.tabBookmarks.Padding = new System.Windows.Forms.Padding(3);
            this.tabBookmarks.Size = new System.Drawing.Size(222, 89);
            this.tabBookmarks.TabIndex = 1;
            this.tabBookmarks.Text = "Bookmarks";
            this.tabBookmarks.UseVisualStyleBackColor = true;
            // 
            // linkDeleteAllBookmarks
            // 
            this.linkDeleteAllBookmarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkDeleteAllBookmarks.AutoSize = true;
            this.linkDeleteAllBookmarks.LinkColor = System.Drawing.Color.Purple;
            this.linkDeleteAllBookmarks.Location = new System.Drawing.Point(125, 74);
            this.linkDeleteAllBookmarks.Name = "linkDeleteAllBookmarks";
            this.linkDeleteAllBookmarks.Size = new System.Drawing.Size(80, 15);
            this.linkDeleteAllBookmarks.TabIndex = 2;
            this.linkDeleteAllBookmarks.TabStop = true;
            this.linkDeleteAllBookmarks.Text = "Удалить все";
            this.linkDeleteAllBookmarks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDeleteAllBookmarks_LinkClicked);
            // 
            // linkAddBookmark
            // 
            this.linkAddBookmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkAddBookmark.AutoSize = true;
            this.linkAddBookmark.LinkColor = System.Drawing.Color.Purple;
            this.linkAddBookmark.Location = new System.Drawing.Point(1, 74);
            this.linkAddBookmark.Name = "linkAddBookmark";
            this.linkAddBookmark.Size = new System.Drawing.Size(119, 15);
            this.linkAddBookmark.TabIndex = 1;
            this.linkAddBookmark.TabStop = true;
            this.linkAddBookmark.Text = "Добавить закладку";
            this.linkAddBookmark.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAddBookmark_LinkClicked);
            // 
            // tabNavigation
            // 
            this.tabNavigation.Controls.Add(this.linkDeleteAllPositions);
            this.tabNavigation.Controls.Add(this.listPositions);
            this.tabNavigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabNavigation.Location = new System.Drawing.Point(4, 24);
            this.tabNavigation.Name = "tabNavigation";
            this.tabNavigation.Padding = new System.Windows.Forms.Padding(3);
            this.tabNavigation.Size = new System.Drawing.Size(222, 89);
            this.tabNavigation.TabIndex = 2;
            this.tabNavigation.Text = "Navigation";
            this.tabNavigation.UseVisualStyleBackColor = true;
            // 
            // linkDeleteAllPositions
            // 
            this.linkDeleteAllPositions.AutoSize = true;
            this.linkDeleteAllPositions.LinkColor = System.Drawing.Color.Purple;
            this.linkDeleteAllPositions.Location = new System.Drawing.Point(0, 0);
            this.linkDeleteAllPositions.Name = "linkDeleteAllPositions";
            this.linkDeleteAllPositions.Size = new System.Drawing.Size(131, 15);
            this.linkDeleteAllPositions.TabIndex = 3;
            this.linkDeleteAllPositions.TabStop = true;
            this.linkDeleteAllPositions.Text = "Удалить все позиции";
            this.linkDeleteAllPositions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDeleteAllPositions_LinkClicked);
            // 
            // listPositions
            // 
            this.listPositions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listPositions.BackColor = System.Drawing.SystemColors.Window;
            this.listPositions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listPositions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listPositions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listPositions.FormattingEnabled = true;
            this.listPositions.IntegralHeight = false;
            this.listPositions.ItemHeight = 15;
            this.listPositions.Location = new System.Drawing.Point(3, 18);
            this.listPositions.Name = "listPositions";
            this.listPositions.Size = new System.Drawing.Size(206, 71);
            this.listPositions.TabIndex = 1;
            this.listPositions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listPositions_MouseUp);
            // 
            // panelControls
            // 
            this.panelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControls.BackColor = System.Drawing.Color.Lavender;
            this.panelControls.Controls.Add(this.pictureBox1);
            this.panelControls.Controls.Add(this.pRefresh);
            this.panelControls.Controls.Add(this.pDraw);
            this.panelControls.Controls.Add(this.p1);
            this.panelControls.Controls.Add(this.lblTitle);
            this.panelControls.Controls.Add(this.btnHelp);
            this.panelControls.Controls.Add(this.btnClose);
            this.panelControls.Location = new System.Drawing.Point(1, 1);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(230, 18);
            this.panelControls.TabIndex = 83;
            // 
            // pRefresh
            // 
            this.pRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pRefresh.Image = ((System.Drawing.Image)(resources.GetObject("pRefresh.Image")));
            this.pRefresh.Location = new System.Drawing.Point(173, 2);
            this.pRefresh.Name = "pRefresh";
            this.pRefresh.Size = new System.Drawing.Size(14, 14);
            this.pRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRefresh.TabIndex = 86;
            this.pRefresh.TabStop = false;
            this.pRefresh.Click += new System.EventHandler(this.pRefresh_Click);
            // 
            // pDraw
            // 
            this.pDraw.BackColor = System.Drawing.Color.Red;
            this.pDraw.Image = ((System.Drawing.Image)(resources.GetObject("pDraw.Image")));
            this.pDraw.Location = new System.Drawing.Point(124, 16);
            this.pDraw.Name = "pDraw";
            this.pDraw.Size = new System.Drawing.Size(4, 1);
            this.pDraw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pDraw.TabIndex = 85;
            this.pDraw.TabStop = false;
            this.pDraw.Visible = false;
            // 
            // p1
            // 
            this.p1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.p1.Location = new System.Drawing.Point(147, 10);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(3, 16);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 84;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTitle.Location = new System.Drawing.Point(25, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(107, 15);
            this.lblTitle.TabIndex = 83;
            this.lblTitle.Text = "Map Navigation";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // MapNavigatorDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(232, 138);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MapNavigatorDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabBookmarks.ResumeLayout(false);
            this.tabBookmarks.PerformLayout();
            this.tabNavigation.ResumeLayout(false);
            this.tabNavigation.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listBookmarks;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox btnHelp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabBookmarks;
        private System.Windows.Forms.TabPage tabNavigation;
        private System.Windows.Forms.Panel panelControls;
        public System.Windows.Forms.ListBox listMainTopics;
        public System.Windows.Forms.ListBox listPositions;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel linkAddBookmark;
        private System.Windows.Forms.LinkLabel linkDeleteAllBookmarks;
        private System.Windows.Forms.LinkLabel linkDeleteAllPositions;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.PictureBox pDraw;
        public System.Windows.Forms.Label lblCentralTopic;
        private System.Windows.Forms.PictureBox pRefresh;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}