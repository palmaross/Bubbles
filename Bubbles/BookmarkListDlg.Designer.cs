namespace Bubbles
{
    partial class BookmarkListDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookmarkListDlg));
            this.listBookmarks = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmsMore = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.b_main = new System.Windows.Forms.ToolStripMenuItem();
            this.b_removemain = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.b_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.b_deleteall = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnHelp = new System.Windows.Forms.PictureBox();
            this.btnMore = new System.Windows.Forms.PictureBox();
            this.cmsMore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMore)).BeginInit();
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
            this.listBookmarks.Location = new System.Drawing.Point(3, 20);
            this.listBookmarks.Name = "listBookmarks";
            this.listBookmarks.Size = new System.Drawing.Size(178, 84);
            this.listBookmarks.TabIndex = 0;
            this.listBookmarks.SelectedIndexChanged += new System.EventHandler(this.listBookmarks_SelectedIndexChanged);
            this.listBookmarks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBookmarks_MouseDown);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // cmsMore
            // 
            this.cmsMore.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsMore.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b_main,
            this.b_removemain,
            this.toolStripSeparator1,
            this.b_delete,
            this.b_deleteall});
            this.cmsMore.Name = "contextMenuStrip1";
            this.cmsMore.ShowImageMargin = false;
            this.cmsMore.Size = new System.Drawing.Size(192, 98);
            // 
            // b_main
            // 
            this.b_main.Name = "b_main";
            this.b_main.Size = new System.Drawing.Size(191, 22);
            this.b_main.Text = "Bookmark Main Topics";
            // 
            // b_removemain
            // 
            this.b_removemain.Name = "b_removemain";
            this.b_removemain.Size = new System.Drawing.Size(191, 22);
            this.b_removemain.Text = "Remove from Main Topics";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // b_delete
            // 
            this.b_delete.Name = "b_delete";
            this.b_delete.Size = new System.Drawing.Size(191, 22);
            this.b_delete.Text = "Delete Selected Bookmarks";
            // 
            // b_deleteall
            // 
            this.b_deleteall.Name = "b_deleteall";
            this.b_deleteall.Size = new System.Drawing.Size(191, 22);
            this.b_deleteall.Text = "Delete All Bookmarks";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Magenta;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "высота22";
            this.label1.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(78, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(16, 16);
            this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAdd.TabIndex = 74;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(165, 2);
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
            this.btnHelp.Location = new System.Drawing.Point(145, 2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(16, 16);
            this.btnHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHelp.TabIndex = 80;
            this.btnHelp.TabStop = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnMore
            // 
            this.btnMore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMore.Image = ((System.Drawing.Image)(resources.GetObject("btnMore.Image")));
            this.btnMore.Location = new System.Drawing.Point(4, 2);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(16, 16);
            this.btnMore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMore.TabIndex = 81;
            this.btnMore.TabStop = false;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // BookmarkListDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(184, 107);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBookmarks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookmarkListDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.cmsMore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox listBookmarks;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmsMore;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnAdd;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox btnHelp;
        private System.Windows.Forms.PictureBox btnMore;
        private System.Windows.Forms.ToolStripMenuItem b_main;
        private System.Windows.Forms.ToolStripMenuItem b_removemain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem b_delete;
        private System.Windows.Forms.ToolStripMenuItem b_deleteall;
    }
}