namespace Bubbles
{
    partial class StixMapNavigator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StixMapNavigator));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmsCommon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pCentral = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.cmsPositions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.b_DeletePosition = new System.Windows.Forms.ToolStripMenuItem();
            this.b_DeleteAllPositions = new System.Windows.Forms.ToolStripMenuItem();
            this.pMain = new System.Windows.Forms.PictureBox();
            this.cmsMainTopics = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.p1 = new System.Windows.Forms.PictureBox();
            this.B1 = new System.Windows.Forms.PictureBox();
            this.B2 = new System.Windows.Forms.PictureBox();
            this.B3 = new System.Windows.Forms.PictureBox();
            this.B4 = new System.Windows.Forms.PictureBox();
            this.B5 = new System.Windows.Forms.PictureBox();
            this.pBookmarkList = new System.Windows.Forms.PictureBox();
            this.pSearch = new System.Windows.Forms.PictureBox();
            this.cmsBookmarks = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pCentral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            this.cmsPositions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.B1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.B2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.B3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.B4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.B5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBookmarkList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // cmsCommon
            // 
            this.cmsCommon.Name = "contextMenuStrip1";
            this.cmsCommon.Size = new System.Drawing.Size(61, 4);
            // 
            // pCentral
            // 
            this.pCentral.Image = ((System.Drawing.Image)(resources.GetObject("pCentral.Image")));
            this.pCentral.Location = new System.Drawing.Point(26, 3);
            this.pCentral.Name = "pCentral";
            this.pCentral.Size = new System.Drawing.Size(24, 24);
            this.pCentral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCentral.TabIndex = 75;
            this.pCentral.TabStop = false;
            this.pCentral.Tag = "Central";
            this.pCentral.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pCentral_MouseClick);
            // 
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(266, 5);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(20, 20);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 77;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // pictureHandle
            // 
            this.pictureHandle.BackColor = System.Drawing.Color.Transparent;
            this.pictureHandle.Image = ((System.Drawing.Image)(resources.GetObject("pictureHandle.Image")));
            this.pictureHandle.Location = new System.Drawing.Point(0, 0);
            this.pictureHandle.Name = "pictureHandle";
            this.pictureHandle.Size = new System.Drawing.Size(24, 24);
            this.pictureHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureHandle.TabIndex = 77;
            this.pictureHandle.TabStop = false;
            // 
            // cmsPositions
            // 
            this.cmsPositions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b_DeletePosition,
            this.b_DeleteAllPositions});
            this.cmsPositions.Name = "cmsDelete";
            this.cmsPositions.ShowImageMargin = false;
            this.cmsPositions.Size = new System.Drawing.Size(151, 48);
            // 
            // b_DeletePosition
            // 
            this.b_DeletePosition.Name = "b_DeletePosition";
            this.b_DeletePosition.Size = new System.Drawing.Size(150, 22);
            this.b_DeletePosition.Text = "Delete Position";
            // 
            // b_DeleteAllPositions
            // 
            this.b_DeleteAllPositions.Name = "b_DeleteAllPositions";
            this.b_DeleteAllPositions.Size = new System.Drawing.Size(150, 22);
            this.b_DeleteAllPositions.Text = "Delete All Positions";
            // 
            // pMain
            // 
            this.pMain.Image = ((System.Drawing.Image)(resources.GetObject("pMain.Image")));
            this.pMain.Location = new System.Drawing.Point(55, 3);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(24, 24);
            this.pMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMain.TabIndex = 85;
            this.pMain.TabStop = false;
            this.pMain.Tag = "Main";
            this.pMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pMain_MouseClick);
            this.pMain.MouseHover += new System.EventHandler(this.pMain_MouseHover);
            // 
            // cmsMainTopics
            // 
            this.cmsMainTopics.Name = "cmsMainTopics";
            this.cmsMainTopics.Size = new System.Drawing.Size(61, 4);
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(43, -2);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(6, 20);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 81;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // B1
            // 
            this.B1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B1.Image = ((System.Drawing.Image)(resources.GetObject("B1.Image")));
            this.B1.Location = new System.Drawing.Point(113, 5);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(20, 20);
            this.B1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.B1.TabIndex = 90;
            this.B1.TabStop = false;
            this.B1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Position_MouseClick);
            // 
            // B2
            // 
            this.B2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B2.Image = ((System.Drawing.Image)(resources.GetObject("B2.Image")));
            this.B2.Location = new System.Drawing.Point(137, 5);
            this.B2.Name = "B2";
            this.B2.Size = new System.Drawing.Size(20, 20);
            this.B2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.B2.TabIndex = 91;
            this.B2.TabStop = false;
            this.B2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Position_MouseClick);
            // 
            // B3
            // 
            this.B3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B3.Image = ((System.Drawing.Image)(resources.GetObject("B3.Image")));
            this.B3.Location = new System.Drawing.Point(161, 5);
            this.B3.Name = "B3";
            this.B3.Size = new System.Drawing.Size(20, 20);
            this.B3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.B3.TabIndex = 92;
            this.B3.TabStop = false;
            this.B3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Position_MouseClick);
            // 
            // B4
            // 
            this.B4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B4.Image = ((System.Drawing.Image)(resources.GetObject("B4.Image")));
            this.B4.Location = new System.Drawing.Point(185, 5);
            this.B4.Name = "B4";
            this.B4.Size = new System.Drawing.Size(20, 20);
            this.B4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.B4.TabIndex = 93;
            this.B4.TabStop = false;
            this.B4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Position_MouseClick);
            // 
            // B5
            // 
            this.B5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B5.Image = ((System.Drawing.Image)(resources.GetObject("B5.Image")));
            this.B5.Location = new System.Drawing.Point(209, 5);
            this.B5.Name = "B5";
            this.B5.Size = new System.Drawing.Size(20, 20);
            this.B5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.B5.TabIndex = 94;
            this.B5.TabStop = false;
            this.B5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Position_MouseClick);
            // 
            // pBookmarkList
            // 
            this.pBookmarkList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pBookmarkList.Image = ((System.Drawing.Image)(resources.GetObject("pBookmarkList.Image")));
            this.pBookmarkList.Location = new System.Drawing.Point(84, 5);
            this.pBookmarkList.Name = "pBookmarkList";
            this.pBookmarkList.Size = new System.Drawing.Size(20, 20);
            this.pBookmarkList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBookmarkList.TabIndex = 95;
            this.pBookmarkList.TabStop = false;
            this.pBookmarkList.Tag = "List";
            this.pBookmarkList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BookmarkList_MouseClick);
            this.pBookmarkList.MouseHover += new System.EventHandler(this.pBookmarkList_MouseHover);
            // 
            // pSearch
            // 
            this.pSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pSearch.Image = ((System.Drawing.Image)(resources.GetObject("pSearch.Image")));
            this.pSearch.Location = new System.Drawing.Point(238, 5);
            this.pSearch.Name = "pSearch";
            this.pSearch.Size = new System.Drawing.Size(20, 20);
            this.pSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pSearch.TabIndex = 96;
            this.pSearch.TabStop = false;
            this.pSearch.Tag = "Search";
            this.pSearch.Click += new System.EventHandler(this.pSearch_Click);
            // 
            // cmsBookmarks
            // 
            this.cmsBookmarks.Name = "contextMenuStrip1";
            this.cmsBookmarks.Size = new System.Drawing.Size(61, 4);
            // 
            // StixMapNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(291, 30);
            this.ControlBox = false;
            this.Controls.Add(this.pSearch);
            this.Controls.Add(this.pBookmarkList);
            this.Controls.Add(this.B5);
            this.Controls.Add(this.B4);
            this.Controls.Add(this.B3);
            this.Controls.Add(this.B2);
            this.Controls.Add(this.B1);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pCentral);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.Manage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StixMapNavigator";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.pCentral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            this.cmsPositions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.B1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.B2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.B3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.B4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.B5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBookmarkList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsCommon;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.ContextMenuStrip cmsPositions;
        private System.Windows.Forms.ToolStripMenuItem b_DeletePosition;
        private System.Windows.Forms.PictureBox pMain;
        private System.Windows.Forms.ContextMenuStrip cmsMainTopics;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.PictureBox B1;
        private System.Windows.Forms.PictureBox B2;
        private System.Windows.Forms.PictureBox B3;
        private System.Windows.Forms.PictureBox B4;
        private System.Windows.Forms.PictureBox B5;
        private System.Windows.Forms.PictureBox pBookmarkList;
        private System.Windows.Forms.ToolStripMenuItem b_DeleteAllPositions;
        private System.Windows.Forms.PictureBox pSearch;
        private System.Windows.Forms.ContextMenuStrip cmsBookmarks;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.PictureBox pCentral;
    }
}