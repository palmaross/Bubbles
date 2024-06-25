namespace Bubbles
{
    partial class BookmarksDlg
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.panelActions = new System.Windows.Forms.Panel();
            this.lblGroupBookmark = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAction = new System.Windows.Forms.Button();
            this.txtGroupBookmark = new System.Windows.Forms.TextBox();
            this.cmsBookmark = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.b_renameBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.b_deleteBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.b_addBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.b_renameGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.b_deleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.panelActions.SuspendLayout();
            this.cmsBookmark.SuspendLayout();
            this.cmsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(2, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(232, 150);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewGroup.Location = new System.Drawing.Point(2, 159);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(88, 23);
            this.btnNewGroup.TabIndex = 1;
            this.btnNewGroup.Text = "Новая группа";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(146, 159);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelActions.Controls.Add(this.lblGroupBookmark);
            this.panelActions.Controls.Add(this.btnCancel);
            this.panelActions.Controls.Add(this.btnAction);
            this.panelActions.Controls.Add(this.txtGroupBookmark);
            this.panelActions.Location = new System.Drawing.Point(2, 46);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(232, 82);
            this.panelActions.TabIndex = 3;
            this.panelActions.Visible = false;
            // 
            // lblGroupBookmark
            // 
            this.lblGroupBookmark.AutoSize = true;
            this.lblGroupBookmark.Location = new System.Drawing.Point(7, 7);
            this.lblGroupBookmark.Name = "lblGroupBookmark";
            this.lblGroupBookmark.Size = new System.Drawing.Size(71, 13);
            this.lblGroupBookmark.TabIndex = 6;
            this.lblGroupBookmark.Text = "Имя группы:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(160, 51);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(10, 51);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(146, 23);
            this.btnAction.TabIndex = 4;
            this.btnAction.Text = "Переименовать закладку";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // txtGroupBookmark
            // 
            this.txtGroupBookmark.Location = new System.Drawing.Point(10, 25);
            this.txtGroupBookmark.Name = "txtGroupBookmark";
            this.txtGroupBookmark.Size = new System.Drawing.Size(212, 20);
            this.txtGroupBookmark.TabIndex = 0;
            this.txtGroupBookmark.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNewGroup_KeyUp);
            // 
            // cmsBookmark
            // 
            this.cmsBookmark.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b_renameBookmark,
            this.b_deleteBookmark});
            this.cmsBookmark.Name = "cmsBookmark";
            this.cmsBookmark.Size = new System.Drawing.Size(175, 48);
            // 
            // b_renameBookmark
            // 
            this.b_renameBookmark.Name = "b_renameBookmark";
            this.b_renameBookmark.Size = new System.Drawing.Size(174, 22);
            this.b_renameBookmark.Text = "Rename Bookmark";
            this.b_renameBookmark.Click += new System.EventHandler(this.b_renameBookmark_Click);
            // 
            // b_deleteBookmark
            // 
            this.b_deleteBookmark.Name = "b_deleteBookmark";
            this.b_deleteBookmark.Size = new System.Drawing.Size(174, 22);
            this.b_deleteBookmark.Text = "Delete Bookmark";
            this.b_deleteBookmark.Click += new System.EventHandler(this.b_deleteBookmark_Click);
            // 
            // cmsGroup
            // 
            this.cmsGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b_addBookmark,
            this.b_renameGroup,
            this.b_deleteGroup});
            this.cmsGroup.Name = "cmsGroup";
            this.cmsGroup.Size = new System.Drawing.Size(154, 70);
            // 
            // b_addBookmark
            // 
            this.b_addBookmark.Name = "b_addBookmark";
            this.b_addBookmark.Size = new System.Drawing.Size(153, 22);
            this.b_addBookmark.Text = "Add Bookmark";
            this.b_addBookmark.Click += new System.EventHandler(this.b_addBookmark_Click);
            // 
            // b_renameGroup
            // 
            this.b_renameGroup.Name = "b_renameGroup";
            this.b_renameGroup.Size = new System.Drawing.Size(153, 22);
            this.b_renameGroup.Text = "Rename Group";
            this.b_renameGroup.Click += new System.EventHandler(this.b_renameGroup_Click);
            // 
            // b_deleteGroup
            // 
            this.b_deleteGroup.Name = "b_deleteGroup";
            this.b_deleteGroup.Size = new System.Drawing.Size(153, 22);
            this.b_deleteGroup.Text = "Delete Group";
            this.b_deleteGroup.Click += new System.EventHandler(this.b_deleteGroup_Click);
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(154, 36);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(14, 14);
            this.p1.TabIndex = 4;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // BookmarksDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(235, 191);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNewGroup);
            this.Controls.Add(this.treeView1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookmarksDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Global Bookmarks";
            this.panelActions.ResumeLayout(false);
            this.panelActions.PerformLayout();
            this.cmsBookmark.ResumeLayout(false);
            this.cmsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.TextBox txtGroupBookmark;
        private System.Windows.Forms.Label lblGroupBookmark;
        private System.Windows.Forms.ContextMenuStrip cmsBookmark;
        private System.Windows.Forms.ContextMenuStrip cmsGroup;
        private System.Windows.Forms.ToolStripMenuItem b_renameBookmark;
        private System.Windows.Forms.ToolStripMenuItem b_deleteBookmark;
        private System.Windows.Forms.ToolStripMenuItem b_addBookmark;
        private System.Windows.Forms.ToolStripMenuItem b_renameGroup;
        private System.Windows.Forms.ToolStripMenuItem b_deleteGroup;
        private System.Windows.Forms.PictureBox p1;
    }
}