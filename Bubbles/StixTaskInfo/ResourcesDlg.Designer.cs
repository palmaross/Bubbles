namespace Bubbles
{
    partial class ResourcesDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourcesDlg));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pHelp = new System.Windows.Forms.PictureBox();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pHandle = new System.Windows.Forms.PictureBox();
            this.txtNewResource = new System.Windows.Forms.TextBox();
            this.cmsResource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.r_addtotopic = new System.Windows.Forms.ToolStripMenuItem();
            this.r_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.r_addtomap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.r_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.r_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.r_color = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.r_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.r_copyall = new System.Windows.Forms.ToolStripMenuItem();
            this.r_cut = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cmsGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.g_newresource = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.g_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.g_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.g_addtomap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.rg_copyall = new System.Windows.Forms.ToolStripMenuItem();
            this.g_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.g_pastefromclipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeViewCM = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeViewDB = new System.Windows.Forms.TreeView();
            this.txtNewGroup = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).BeginInit();
            this.cmsResource.SuspendLayout();
            this.cmsGroup.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pHelp);
            this.panel1.Controls.Add(this.pClose);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 18);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(24, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(75, 15);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Resources";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pHelp
            // 
            this.pHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pHelp.Image = ((System.Drawing.Image)(resources.GetObject("pHelp.Image")));
            this.pHelp.Location = new System.Drawing.Point(139, 1);
            this.pHelp.Name = "pHelp";
            this.pHelp.Size = new System.Drawing.Size(16, 16);
            this.pHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHelp.TabIndex = 4;
            this.pHelp.TabStop = false;
            this.pHelp.Click += new System.EventHandler(this.pHelp_Click);
            // 
            // pClose
            // 
            this.pClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pClose.Image = ((System.Drawing.Image)(resources.GetObject("pClose.Image")));
            this.pClose.Location = new System.Drawing.Point(159, 1);
            this.pClose.Name = "pClose";
            this.pClose.Size = new System.Drawing.Size(16, 16);
            this.pClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClose.TabIndex = 3;
            this.pClose.TabStop = false;
            this.pClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // pHandle
            // 
            this.pHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pHandle.Image = ((System.Drawing.Image)(resources.GetObject("pHandle.Image")));
            this.pHandle.Location = new System.Drawing.Point(73, 251);
            this.pHandle.Name = "pHandle";
            this.pHandle.Size = new System.Drawing.Size(24, 6);
            this.pHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHandle.TabIndex = 6;
            this.pHandle.TabStop = false;
            // 
            // txtNewResource
            // 
            this.txtNewResource.BackColor = System.Drawing.SystemColors.Window;
            this.txtNewResource.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNewResource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtNewResource.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtNewResource.Location = new System.Drawing.Point(3, 3);
            this.txtNewResource.Name = "txtNewResource";
            this.txtNewResource.Size = new System.Drawing.Size(164, 21);
            this.txtNewResource.TabIndex = 8;
            this.txtNewResource.Tag = "resource";
            this.txtNewResource.Text = "New";
            this.txtNewResource.Enter += new System.EventHandler(this.txtAddNew_Enter);
            this.txtNewResource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewResource_KeyDown);
            this.txtNewResource.Leave += new System.EventHandler(this.txtAddNew_Leave);
            // 
            // cmsResource
            // 
            this.cmsResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.r_addtotopic,
            this.r_remove,
            this.r_addtomap,
            this.toolStripSeparator3,
            this.r_rename,
            this.r_delete,
            this.r_color,
            this.toolStripSeparator1,
            this.r_copy,
            this.r_copyall,
            this.r_cut});
            this.cmsResource.Name = "cmsResource";
            this.cmsResource.ShowImageMargin = false;
            this.cmsResource.Size = new System.Drawing.Size(165, 214);
            // 
            // r_addtotopic
            // 
            this.r_addtotopic.Name = "r_addtotopic";
            this.r_addtotopic.Size = new System.Drawing.Size(164, 22);
            this.r_addtotopic.Text = "Add to topic(s)";
            // 
            // r_remove
            // 
            this.r_remove.Name = "r_remove";
            this.r_remove.Size = new System.Drawing.Size(164, 22);
            this.r_remove.Text = "Remove from topic(s)";
            // 
            // r_addtomap
            // 
            this.r_addtomap.Name = "r_addtomap";
            this.r_addtomap.Size = new System.Drawing.Size(164, 22);
            this.r_addtomap.Text = "Add to Map";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // r_rename
            // 
            this.r_rename.Name = "r_rename";
            this.r_rename.Size = new System.Drawing.Size(164, 22);
            this.r_rename.Text = "Rename";
            // 
            // r_delete
            // 
            this.r_delete.Name = "r_delete";
            this.r_delete.Size = new System.Drawing.Size(164, 22);
            this.r_delete.Text = "Delete";
            // 
            // r_color
            // 
            this.r_color.Name = "r_color";
            this.r_color.Size = new System.Drawing.Size(164, 22);
            this.r_color.Text = "Color...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // r_copy
            // 
            this.r_copy.Name = "r_copy";
            this.r_copy.Size = new System.Drawing.Size(164, 22);
            this.r_copy.Text = "Copy (Ctrl+C)";
            // 
            // r_copyall
            // 
            this.r_copyall.Name = "r_copyall";
            this.r_copyall.Size = new System.Drawing.Size(164, 22);
            this.r_copyall.Text = "Copy All";
            // 
            // r_cut
            // 
            this.r_cut.Name = "r_cut";
            this.r_cut.Size = new System.Drawing.Size(164, 22);
            this.r_cut.Text = "Cut (Ctrl+X)";
            // 
            // cmsGroup
            // 
            this.cmsGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.g_newresource,
            this.toolStripSeparator5,
            this.g_rename,
            this.g_delete,
            this.toolStripSeparator4,
            this.g_addtomap,
            this.toolStripSeparator2,
            this.rg_copyall,
            this.g_paste,
            this.g_pastefromclipboard});
            this.cmsGroup.Name = "cmsMore";
            this.cmsGroup.ShowImageMargin = false;
            this.cmsGroup.Size = new System.Drawing.Size(164, 176);
            // 
            // g_newresource
            // 
            this.g_newresource.Name = "g_newresource";
            this.g_newresource.Size = new System.Drawing.Size(163, 22);
            this.g_newresource.Text = "New Resource";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(160, 6);
            // 
            // g_rename
            // 
            this.g_rename.Name = "g_rename";
            this.g_rename.Size = new System.Drawing.Size(163, 22);
            this.g_rename.Text = "Rename Group";
            // 
            // g_delete
            // 
            this.g_delete.Name = "g_delete";
            this.g_delete.Size = new System.Drawing.Size(163, 22);
            this.g_delete.Text = "Delete Group";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(160, 6);
            // 
            // g_addtomap
            // 
            this.g_addtomap.Name = "g_addtomap";
            this.g_addtomap.Size = new System.Drawing.Size(163, 22);
            this.g_addtomap.Text = "Add to Map";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
            // 
            // rg_copyall
            // 
            this.rg_copyall.Name = "rg_copyall";
            this.rg_copyall.Size = new System.Drawing.Size(163, 22);
            this.rg_copyall.Text = "Copy All";
            // 
            // g_paste
            // 
            this.g_paste.Name = "g_paste";
            this.g_paste.Size = new System.Drawing.Size(163, 22);
            this.g_paste.Text = "Paste Copied (Ctrl+V)";
            // 
            // g_pastefromclipboard
            // 
            this.g_pastefromclipboard.Name = "g_pastefromclipboard";
            this.g_pastefromclipboard.Size = new System.Drawing.Size(163, 22);
            this.g_pastefromclipboard.Text = "Paste form Clipboard";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(1, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(178, 232);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeViewCM);
            this.tabPage1.Controls.Add(this.txtNewResource);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(170, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Текущая карта";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeViewCM
            // 
            this.treeViewCM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCM.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewCM.Location = new System.Drawing.Point(3, 24);
            this.treeViewCM.Name = "treeViewCM";
            this.treeViewCM.Size = new System.Drawing.Size(164, 177);
            this.treeViewCM.TabIndex = 9;
            this.treeViewCM.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeViewCM.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_DrawNode);
            this.treeViewCM.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeViewCM.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeViewCM.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyUp);
            this.treeViewCM.Leave += new System.EventHandler(this.treeView_Leave);
            this.treeViewCM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.treeViewDB);
            this.tabPage2.Controls.Add(this.txtNewGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(170, 204);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Все ресурсы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeViewDB
            // 
            this.treeViewDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDB.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewDB.Location = new System.Drawing.Point(3, 24);
            this.treeViewDB.Name = "treeViewDB";
            this.treeViewDB.Size = new System.Drawing.Size(164, 177);
            this.treeViewDB.TabIndex = 0;
            this.treeViewDB.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeViewDB.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_DrawNode);
            this.treeViewDB.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeViewDB.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeViewDB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyUp);
            this.treeViewDB.Leave += new System.EventHandler(this.treeView_Leave);
            this.treeViewDB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // txtNewGroup
            // 
            this.txtNewGroup.BackColor = System.Drawing.SystemColors.Window;
            this.txtNewGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNewGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtNewGroup.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNewGroup.Location = new System.Drawing.Point(3, 3);
            this.txtNewGroup.Name = "txtNewGroup";
            this.txtNewGroup.Size = new System.Drawing.Size(164, 21);
            this.txtNewGroup.TabIndex = 12;
            this.txtNewGroup.Tag = "resource";
            this.txtNewGroup.Text = "Add a New Group";
            this.txtNewGroup.Enter += new System.EventHandler(this.txtAddNew_Enter);
            this.txtNewGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewGroup_KeyDown);
            this.txtNewGroup.Leave += new System.EventHandler(this.txtAddNew_Leave);
            // 
            // ResourcesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 262);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pHandle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResourcesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResourcesDlg";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).EndInit();
            this.cmsResource.ResumeLayout(false);
            this.cmsGroup.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pClose;
        private System.Windows.Forms.PictureBox pHelp;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox pHandle;
        private System.Windows.Forms.TextBox txtNewResource;
        private System.Windows.Forms.ContextMenuStrip cmsResource;
        private System.Windows.Forms.ToolStripMenuItem r_delete;
        private System.Windows.Forms.ToolStripMenuItem r_rename;
        private System.Windows.Forms.ToolStripMenuItem r_color;
        private System.Windows.Forms.ToolStripMenuItem r_addtotopic;
        private System.Windows.Forms.ToolStripMenuItem r_addtomap;
        private System.Windows.Forms.ToolStripMenuItem r_remove;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem r_copy;
        private System.Windows.Forms.ToolStripMenuItem r_cut;
        private System.Windows.Forms.ContextMenuStrip cmsGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem g_pastefromclipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem g_rename;
        private System.Windows.Forms.ToolStripMenuItem g_delete;
        private System.Windows.Forms.ToolStripMenuItem g_paste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem g_addtomap;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtNewGroup;
        private System.Windows.Forms.ToolStripMenuItem r_copyall;
        private System.Windows.Forms.ToolStripMenuItem rg_copyall;
        private System.Windows.Forms.ToolStripMenuItem g_newresource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.TreeView treeViewDB;
        public System.Windows.Forms.TreeView treeViewCM;
    }
}