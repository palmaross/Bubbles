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
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.PictureBox();
            this.btnDeleteAll = new System.Windows.Forms.PictureBox();
            this.pAddMain = new System.Windows.Forms.PictureBox();
            this.pDeleteMain = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDeleteMain)).BeginInit();
            this.SuspendLayout();
            // 
            // listBookmarks
            // 
            this.listBookmarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBookmarks.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBookmarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBookmarks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBookmarks.FormattingEnabled = true;
            this.listBookmarks.IntegralHeight = false;
            this.listBookmarks.Location = new System.Drawing.Point(3, 20);
            this.listBookmarks.Name = "listBookmarks";
            this.listBookmarks.Size = new System.Drawing.Size(234, 84);
            this.listBookmarks.TabIndex = 0;
            this.listBookmarks.SelectedIndexChanged += new System.EventHandler(this.listBookmarks_SelectedIndexChanged);
            this.listBookmarks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBookmarks_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(224, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 4;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "высота22";
            this.label1.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(6, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(16, 16);
            this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAdd.TabIndex = 74;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(27, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(16, 16);
            this.btnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDelete.TabIndex = 75;
            this.btnDelete.TabStop = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.Image")));
            this.btnDeleteAll.Location = new System.Drawing.Point(112, 2);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(16, 16);
            this.btnDeleteAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDeleteAll.TabIndex = 76;
            this.btnDeleteAll.TabStop = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // pAddMain
            // 
            this.pAddMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pAddMain.Image = ((System.Drawing.Image)(resources.GetObject("pAddMain.Image")));
            this.pAddMain.Location = new System.Drawing.Point(55, 2);
            this.pAddMain.Name = "pAddMain";
            this.pAddMain.Size = new System.Drawing.Size(16, 16);
            this.pAddMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAddMain.TabIndex = 77;
            this.pAddMain.TabStop = false;
            this.pAddMain.Click += new System.EventHandler(this.pAddMain_Click);
            // 
            // pDeleteMain
            // 
            this.pDeleteMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pDeleteMain.Image = ((System.Drawing.Image)(resources.GetObject("pDeleteMain.Image")));
            this.pDeleteMain.Location = new System.Drawing.Point(78, 2);
            this.pDeleteMain.Name = "pDeleteMain";
            this.pDeleteMain.Size = new System.Drawing.Size(16, 16);
            this.pDeleteMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pDeleteMain.TabIndex = 78;
            this.pDeleteMain.TabStop = false;
            this.pDeleteMain.Click += new System.EventHandler(this.pDeleteMain_Click);
            // 
            // BookmarkListDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(240, 107);
            this.Controls.Add(this.pDeleteMain);
            this.Controls.Add(this.pAddMain);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.listBookmarks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookmarkListDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDeleteMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox listBookmarks;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnAdd;
        private System.Windows.Forms.PictureBox btnDelete;
        private System.Windows.Forms.PictureBox btnDeleteAll;
        private System.Windows.Forms.PictureBox pAddMain;
        private System.Windows.Forms.PictureBox pDeleteMain;
    }
}