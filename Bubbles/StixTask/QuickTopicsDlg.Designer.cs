namespace Bubbles
{
    partial class QuickTopicsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickTopicsDlg));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pQuickTask = new System.Windows.Forms.PictureBox();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.pHandle = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.btnManage = new System.Windows.Forms.PictureBox();
            this.p11 = new System.Windows.Forms.PictureBox();
            this.pHelp = new System.Windows.Forms.PictureBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.panelHead = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pQuickTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).BeginInit();
            this.panelHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.LabelEdit = true;
            this.treeView1.Location = new System.Drawing.Point(2, 21);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(182, 195);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_BeforeLabelEdit);
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_DragOver);
            // 
            // pQuickTask
            // 
            this.pQuickTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pQuickTask.Image = ((System.Drawing.Image)(resources.GetObject("pQuickTask.Image")));
            this.pQuickTask.Location = new System.Drawing.Point(3, 1);
            this.pQuickTask.Name = "pQuickTask";
            this.pQuickTask.Size = new System.Drawing.Size(16, 16);
            this.pQuickTask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pQuickTask.TabIndex = 86;
            this.pQuickTask.TabStop = false;
            this.pQuickTask.Tag = "1";
            // 
            // pClose
            // 
            this.pClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pClose.Image = ((System.Drawing.Image)(resources.GetObject("pClose.Image")));
            this.pClose.Location = new System.Drawing.Point(165, 1);
            this.pClose.Name = "pClose";
            this.pClose.Size = new System.Drawing.Size(16, 16);
            this.pClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClose.TabIndex = 88;
            this.pClose.TabStop = false;
            this.pClose.Click += new System.EventHandler(this.pClose_Click);
            // 
            // pHandle
            // 
            this.pHandle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pHandle.Image = ((System.Drawing.Image)(resources.GetObject("pHandle.Image")));
            this.pHandle.Location = new System.Drawing.Point(81, 218);
            this.pHandle.Name = "pHandle";
            this.pHandle.Size = new System.Drawing.Size(24, 6);
            this.pHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHandle.TabIndex = 89;
            this.pHandle.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTitle.Location = new System.Drawing.Point(22, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(89, 15);
            this.lblTitle.TabIndex = 90;
            this.lblTitle.Text = "Quick Topics";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_Delete,
            this.m_Rename});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 48);
            // 
            // m_Delete
            // 
            this.m_Delete.Name = "m_Delete";
            this.m_Delete.Size = new System.Drawing.Size(92, 22);
            this.m_Delete.Text = "Delete";
            // 
            // m_Rename
            // 
            this.m_Rename.Name = "m_Rename";
            this.m_Rename.Size = new System.Drawing.Size(92, 22);
            this.m_Rename.Text = "Rename";
            // 
            // btnManage
            // 
            this.btnManage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManage.Image = ((System.Drawing.Image)(resources.GetObject("btnManage.Image")));
            this.btnManage.Location = new System.Drawing.Point(124, 1);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(17, 17);
            this.btnManage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnManage.TabIndex = 92;
            this.btnManage.TabStop = false;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // p11
            // 
            this.p11.Location = new System.Drawing.Point(75, 102);
            this.p11.Name = "p11";
            this.p11.Size = new System.Drawing.Size(35, 24);
            this.p11.TabIndex = 93;
            this.p11.TabStop = false;
            this.p11.Visible = false;
            // 
            // pHelp
            // 
            this.pHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pHelp.Image = ((System.Drawing.Image)(resources.GetObject("pHelp.Image")));
            this.pHelp.Location = new System.Drawing.Point(145, 1);
            this.pHelp.Name = "pHelp";
            this.pHelp.Size = new System.Drawing.Size(16, 16);
            this.pHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHelp.TabIndex = 94;
            this.pHelp.TabStop = false;
            this.pHelp.Click += new System.EventHandler(this.pHelp_Click);
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.Moccasin;
            this.panelHead.Controls.Add(this.pQuickTask);
            this.panelHead.Controls.Add(this.lblTitle);
            this.panelHead.Controls.Add(this.pHelp);
            this.panelHead.Controls.Add(this.btnManage);
            this.panelHead.Controls.Add(this.pClose);
            this.panelHead.Location = new System.Drawing.Point(1, 1);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(182, 18);
            this.panelHead.TabIndex = 95;
            // 
            // QuickTopicsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 228);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.p11);
            this.Controls.Add(this.pHandle);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuickTopicsDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "QuickTopicsDlg";
            ((System.ComponentModel.ISupportInitialize)(this.pQuickTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.panelHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pQuickTask;
        private System.Windows.Forms.PictureBox pClose;
        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pHandle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem m_Delete;
        private System.Windows.Forms.ToolStripMenuItem m_Rename;
        private System.Windows.Forms.PictureBox btnManage;
        private System.Windows.Forms.PictureBox p11;
        private System.Windows.Forms.PictureBox pHelp;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Panel panelHead;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}