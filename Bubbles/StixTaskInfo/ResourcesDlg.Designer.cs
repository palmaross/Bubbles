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
            this.ListDBResources = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pHelp = new System.Windows.Forms.PictureBox();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pHandle = new System.Windows.Forms.PictureBox();
            this.ListMapResources = new System.Windows.Forms.ListView();
            this.txtCurrentMap = new System.Windows.Forms.TextBox();
            this.lblCurrentMap = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitter = new System.Windows.Forms.PictureBox();
            this.p11 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.cbResourceGroup = new System.Windows.Forms.ComboBox();
            this.txtNewResourceDB = new System.Windows.Forms.TextBox();
            this.cmsResource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_addtotopic = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_addtomap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_color = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_cut = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cmsMore = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mm_new = new System.Windows.Forms.ToolStripMenuItem();
            this.mm_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.mm_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mm_addtomap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mm_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.mm_pastefromclipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemoveResources = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.cmsResource.SuspendLayout();
            this.cmsMore.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListDBResources
            // 
            this.ListDBResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListDBResources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListDBResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListDBResources.HideSelection = false;
            this.ListDBResources.Location = new System.Drawing.Point(1, 40);
            this.ListDBResources.Name = "ListDBResources";
            this.ListDBResources.ShowItemToolTips = true;
            this.ListDBResources.Size = new System.Drawing.Size(147, 88);
            this.ListDBResources.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListDBResources.TabIndex = 0;
            this.ListDBResources.UseCompatibleStateImageBehavior = false;
            this.ListDBResources.View = System.Windows.Forms.View.Details;
            this.ListDBResources.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_AfterLabelEdit);
            this.ListDBResources.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_BeforeLabelEdit);
            this.ListDBResources.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ResourceList_KeyUp);
            this.ListDBResources.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listResources_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pHelp);
            this.panel1.Controls.Add(this.pClose);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 18);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
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
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pHelp
            // 
            this.pHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pHelp.Image = ((System.Drawing.Image)(resources.GetObject("pHelp.Image")));
            this.pHelp.Location = new System.Drawing.Point(128, 1);
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
            this.pClose.Location = new System.Drawing.Point(148, 1);
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
            this.pHandle.Location = new System.Drawing.Point(70, 289);
            this.pHandle.Name = "pHandle";
            this.pHandle.Size = new System.Drawing.Size(24, 6);
            this.pHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHandle.TabIndex = 6;
            this.pHandle.TabStop = false;
            // 
            // ListMapResources
            // 
            this.ListMapResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListMapResources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListMapResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListMapResources.HideSelection = false;
            this.ListMapResources.Location = new System.Drawing.Point(1, 38);
            this.ListMapResources.Name = "ListMapResources";
            this.ListMapResources.ShowItemToolTips = true;
            this.ListMapResources.Size = new System.Drawing.Size(147, 70);
            this.ListMapResources.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListMapResources.TabIndex = 7;
            this.ListMapResources.UseCompatibleStateImageBehavior = false;
            this.ListMapResources.View = System.Windows.Forms.View.Details;
            this.ListMapResources.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_AfterLabelEdit);
            this.ListMapResources.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_BeforeLabelEdit);
            this.ListMapResources.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ResourceList_KeyUp);
            this.ListMapResources.Leave += new System.EventHandler(this.ListMapResources_Leave);
            this.ListMapResources.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listResources_MouseClick);
            // 
            // txtCurrentMap
            // 
            this.txtCurrentMap.BackColor = System.Drawing.SystemColors.Info;
            this.txtCurrentMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCurrentMap.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtCurrentMap.Location = new System.Drawing.Point(1, 17);
            this.txtCurrentMap.Name = "txtCurrentMap";
            this.txtCurrentMap.Size = new System.Drawing.Size(151, 21);
            this.txtCurrentMap.TabIndex = 8;
            this.txtCurrentMap.Tag = "resource";
            this.txtCurrentMap.Text = "New";
            this.txtCurrentMap.Enter += new System.EventHandler(this.txtResources_Enter);
            this.txtCurrentMap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResources_KeyDown);
            this.txtCurrentMap.Leave += new System.EventHandler(this.txtResources_Leave);
            // 
            // lblCurrentMap
            // 
            this.lblCurrentMap.AutoSize = true;
            this.lblCurrentMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentMap.Location = new System.Drawing.Point(1, 0);
            this.lblCurrentMap.Name = "lblCurrentMap";
            this.lblCurrentMap.Size = new System.Drawing.Size(86, 15);
            this.lblCurrentMap.TabIndex = 10;
            this.lblCurrentMap.Text = "Current Map";
            this.lblCurrentMap.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblCurrentMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblCurrentMap_MouseClick);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Location = new System.Drawing.Point(2, 19);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer.Panel1.Controls.Add(this.splitter);
            this.splitContainer.Panel1.Controls.Add(this.p11);
            this.splitContainer.Panel1.Controls.Add(this.ListMapResources);
            this.splitContainer.Panel1.Controls.Add(this.txtCurrentMap);
            this.splitContainer.Panel1.Controls.Add(this.lblCurrentMap);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer.Panel2.Controls.Add(this.btnClose);
            this.splitContainer.Panel2.Controls.Add(this.cbResourceGroup);
            this.splitContainer.Panel2.Controls.Add(this.txtNewResourceDB);
            this.splitContainer.Panel2.Controls.Add(this.ListDBResources);
            this.splitContainer.Size = new System.Drawing.Size(166, 246);
            this.splitContainer.SplitterDistance = 111;
            this.splitContainer.TabIndex = 12;
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(36, 97);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(10, 6);
            this.splitter.TabIndex = 12;
            this.splitter.TabStop = false;
            this.splitter.Visible = false;
            // 
            // p11
            // 
            this.p11.Location = new System.Drawing.Point(111, 55);
            this.p11.Name = "p11";
            this.p11.Size = new System.Drawing.Size(35, 24);
            this.p11.TabIndex = 11;
            this.p11.TabStop = false;
            this.p11.Visible = false;
            this.p11.Click += new System.EventHandler(this.p11_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(144, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 15);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 6;
            this.btnClose.TabStop = false;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbResourceGroup
            // 
            this.cbResourceGroup.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cbResourceGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResourceGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbResourceGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbResourceGroup.FormattingEnabled = true;
            this.cbResourceGroup.Location = new System.Drawing.Point(0, 0);
            this.cbResourceGroup.Name = "cbResourceGroup";
            this.cbResourceGroup.Size = new System.Drawing.Size(165, 23);
            this.cbResourceGroup.TabIndex = 12;
            this.cbResourceGroup.SelectedIndexChanged += new System.EventHandler(this.cbDataBaseResources_SelectedIndexChanged);
            this.cbResourceGroup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbResourceGroup_MouseDown);
            // 
            // txtNewResourceDB
            // 
            this.txtNewResourceDB.BackColor = System.Drawing.SystemColors.Info;
            this.txtNewResourceDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtNewResourceDB.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNewResourceDB.Location = new System.Drawing.Point(2, 19);
            this.txtNewResourceDB.Name = "txtNewResourceDB";
            this.txtNewResourceDB.Size = new System.Drawing.Size(151, 21);
            this.txtNewResourceDB.TabIndex = 11;
            this.txtNewResourceDB.Tag = "resource";
            this.txtNewResourceDB.Text = "111";
            this.txtNewResourceDB.Enter += new System.EventHandler(this.txtResources_Enter);
            this.txtNewResourceDB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResources_KeyDown);
            this.txtNewResourceDB.Leave += new System.EventHandler(this.txtResources_Leave);
            // 
            // cmsResource
            // 
            this.cmsResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_addtotopic,
            this.mi_remove,
            this.mi_addtomap,
            this.toolStripSeparator3,
            this.mi_rename,
            this.mi_delete,
            this.mi_color,
            this.toolStripSeparator1,
            this.mi_copy,
            this.mi_cut});
            this.cmsResource.Name = "cmsResource";
            this.cmsResource.ShowImageMargin = false;
            this.cmsResource.Size = new System.Drawing.Size(165, 192);
            // 
            // mi_addtotopic
            // 
            this.mi_addtotopic.Name = "mi_addtotopic";
            this.mi_addtotopic.Size = new System.Drawing.Size(164, 22);
            this.mi_addtotopic.Text = "Add to topic(s)";
            // 
            // mi_remove
            // 
            this.mi_remove.Name = "mi_remove";
            this.mi_remove.Size = new System.Drawing.Size(164, 22);
            this.mi_remove.Text = "Remove from topic(s)";
            // 
            // mi_addtomap
            // 
            this.mi_addtomap.Name = "mi_addtomap";
            this.mi_addtomap.Size = new System.Drawing.Size(164, 22);
            this.mi_addtomap.Text = "Add to Map";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // mi_rename
            // 
            this.mi_rename.Name = "mi_rename";
            this.mi_rename.Size = new System.Drawing.Size(164, 22);
            this.mi_rename.Text = "Rename";
            // 
            // mi_delete
            // 
            this.mi_delete.Name = "mi_delete";
            this.mi_delete.Size = new System.Drawing.Size(164, 22);
            this.mi_delete.Text = "Delete";
            // 
            // mi_color
            // 
            this.mi_color.Name = "mi_color";
            this.mi_color.Size = new System.Drawing.Size(164, 22);
            this.mi_color.Text = "Color...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // mi_copy
            // 
            this.mi_copy.Name = "mi_copy";
            this.mi_copy.Size = new System.Drawing.Size(164, 22);
            this.mi_copy.Text = "Copy (Ctrl+C)";
            // 
            // mi_cut
            // 
            this.mi_cut.Name = "mi_cut";
            this.mi_cut.Size = new System.Drawing.Size(164, 22);
            this.mi_cut.Text = "Cut (Ctrl+X)";
            // 
            // cmsMore
            // 
            this.cmsMore.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mm_new,
            this.mm_rename,
            this.mm_delete,
            this.toolStripSeparator4,
            this.mm_addtomap,
            this.toolStripSeparator2,
            this.mm_paste,
            this.mm_pastefromclipboard});
            this.cmsMore.Name = "cmsMore";
            this.cmsMore.ShowImageMargin = false;
            this.cmsMore.Size = new System.Drawing.Size(164, 148);
            // 
            // mm_new
            // 
            this.mm_new.Name = "mm_new";
            this.mm_new.Size = new System.Drawing.Size(163, 22);
            this.mm_new.Text = "New Group";
            // 
            // mm_rename
            // 
            this.mm_rename.Name = "mm_rename";
            this.mm_rename.Size = new System.Drawing.Size(163, 22);
            this.mm_rename.Text = "Rename Group";
            // 
            // mm_delete
            // 
            this.mm_delete.Name = "mm_delete";
            this.mm_delete.Size = new System.Drawing.Size(163, 22);
            this.mm_delete.Text = "Delete Group";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(160, 6);
            // 
            // mm_addtomap
            // 
            this.mm_addtomap.Name = "mm_addtomap";
            this.mm_addtomap.Size = new System.Drawing.Size(163, 22);
            this.mm_addtomap.Text = "Add to Map";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
            // 
            // mm_paste
            // 
            this.mm_paste.Name = "mm_paste";
            this.mm_paste.Size = new System.Drawing.Size(163, 22);
            this.mm_paste.Text = "Paste Copied (Ctrl+V)";
            // 
            // mm_pastefromclipboard
            // 
            this.mm_pastefromclipboard.Name = "mm_pastefromclipboard";
            this.mm_pastefromclipboard.Size = new System.Drawing.Size(163, 22);
            this.mm_pastefromclipboard.Text = "Paste form Clipboard";
            // 
            // btnRemoveResources
            // 
            this.btnRemoveResources.BackColor = System.Drawing.SystemColors.Info;
            this.btnRemoveResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRemoveResources.ForeColor = System.Drawing.Color.Red;
            this.btnRemoveResources.Location = new System.Drawing.Point(2, 265);
            this.btnRemoveResources.Name = "btnRemoveResources";
            this.btnRemoveResources.Size = new System.Drawing.Size(166, 21);
            this.btnRemoveResources.TabIndex = 13;
            this.btnRemoveResources.Text = "Remove Resources from topic";
            this.btnRemoveResources.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRemoveResources.UseVisualStyleBackColor = false;
            this.btnRemoveResources.Click += new System.EventHandler(this.btnRemoveResources_Click);
            // 
            // ResourcesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(170, 300);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.btnRemoveResources);
            this.Controls.Add(this.pHandle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResourcesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResourcesDlg";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.cmsResource.ResumeLayout(false);
            this.cmsMore.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListDBResources;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pClose;
        private System.Windows.Forms.PictureBox pHelp;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox pHandle;
        private System.Windows.Forms.TextBox txtCurrentMap;
        private System.Windows.Forms.Label lblCurrentMap;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox txtNewResourceDB;
        private System.Windows.Forms.ContextMenuStrip cmsResource;
        private System.Windows.Forms.ToolStripMenuItem mi_delete;
        private System.Windows.Forms.ToolStripMenuItem mi_rename;
        private System.Windows.Forms.ToolStripMenuItem mi_color;
        private System.Windows.Forms.ToolStripMenuItem mi_addtotopic;
        private System.Windows.Forms.ToolStripMenuItem mi_addtomap;
        private System.Windows.Forms.PictureBox p11;
        private System.Windows.Forms.ToolStripMenuItem mi_remove;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cbResourceGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mi_copy;
        private System.Windows.Forms.ToolStripMenuItem mi_cut;
        private System.Windows.Forms.ContextMenuStrip cmsMore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mm_pastefromclipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ListView ListMapResources;
        private System.Windows.Forms.Button btnRemoveResources;
        private System.Windows.Forms.ToolStripMenuItem mm_new;
        private System.Windows.Forms.ToolStripMenuItem mm_rename;
        private System.Windows.Forms.ToolStripMenuItem mm_delete;
        private System.Windows.Forms.ToolStripMenuItem mm_paste;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mm_addtomap;
        private System.Windows.Forms.PictureBox splitter;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}