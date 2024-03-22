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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pMore = new System.Windows.Forms.PictureBox();
            this.pHelp = new System.Windows.Forms.PictureBox();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pHandle = new System.Windows.Forms.PictureBox();
            this.ListMapResources = new System.Windows.Forms.ListView();
            this.txtCurrentMap = new System.Windows.Forms.TextBox();
            this.cbAddToDB = new System.Windows.Forms.CheckBox();
            this.lblCurrentMap = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.p11 = new System.Windows.Forms.PictureBox();
            this.cbDataBaseResources = new System.Windows.Forms.ComboBox();
            this.txtAllResources = new System.Windows.Forms.TextBox();
            this.cmsResource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_addtotopic = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_addtomap = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_color = new System.Windows.Forms.ToolStripMenuItem();
            this.cbReplaceResources = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pMore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p11)).BeginInit();
            this.cmsResource.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListDBResources
            // 
            this.ListDBResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListDBResources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListDBResources.HideSelection = false;
            this.ListDBResources.LabelEdit = true;
            this.ListDBResources.Location = new System.Drawing.Point(1, 33);
            this.ListDBResources.Name = "ListDBResources";
            this.ListDBResources.ShowItemToolTips = true;
            this.ListDBResources.Size = new System.Drawing.Size(158, 79);
            this.ListDBResources.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListDBResources.TabIndex = 0;
            this.ListDBResources.UseCompatibleStateImageBehavior = false;
            this.ListDBResources.View = System.Windows.Forms.View.SmallIcon;
            this.ListDBResources.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_AfterLabelEdit);
            this.ListDBResources.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_BeforeLabelEdit);
            this.ListDBResources.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ResourceList_KeyUp);
            this.ListDBResources.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listResources_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Banana.png");
            this.imageList1.Images.SetKeyName(1, "Basil.png");
            this.imageList1.Images.SetKeyName(2, "Blueberry.png");
            this.imageList1.Images.SetKeyName(3, "Flamingo.png");
            this.imageList1.Images.SetKeyName(4, "Grape.png");
            this.imageList1.Images.SetKeyName(5, "Graphite.png");
            this.imageList1.Images.SetKeyName(6, "Lavender.png");
            this.imageList1.Images.SetKeyName(7, "Peacock.png");
            this.imageList1.Images.SetKeyName(8, "Sage.png");
            this.imageList1.Images.SetKeyName(9, "Tangerine.png");
            this.imageList1.Images.SetKeyName(10, "Tomato.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pMore);
            this.panel1.Controls.Add(this.pHelp);
            this.panel1.Controls.Add(this.pClose);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 18);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pMore
            // 
            this.pMore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pMore.Image = ((System.Drawing.Image)(resources.GetObject("pMore.Image")));
            this.pMore.Location = new System.Drawing.Point(3, 1);
            this.pMore.Name = "pMore";
            this.pMore.Size = new System.Drawing.Size(16, 16);
            this.pMore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMore.TabIndex = 6;
            this.pMore.TabStop = false;
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
            this.pHandle.Location = new System.Drawing.Point(70, 268);
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
            this.ListMapResources.HideSelection = false;
            this.ListMapResources.Location = new System.Drawing.Point(1, 46);
            this.ListMapResources.Name = "ListMapResources";
            this.ListMapResources.ShowItemToolTips = true;
            this.ListMapResources.Size = new System.Drawing.Size(158, 57);
            this.ListMapResources.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListMapResources.TabIndex = 7;
            this.ListMapResources.UseCompatibleStateImageBehavior = false;
            this.ListMapResources.View = System.Windows.Forms.View.SmallIcon;
            this.ListMapResources.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_AfterLabelEdit);
            this.ListMapResources.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListResources_BeforeLabelEdit);
            this.ListMapResources.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ResourceList_KeyUp);
            this.ListMapResources.Leave += new System.EventHandler(this.ListMapResources_Leave);
            this.ListMapResources.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listResources_MouseClick);
            // 
            // txtCurrentMap
            // 
            this.txtCurrentMap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentMap.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtCurrentMap.Location = new System.Drawing.Point(1, 15);
            this.txtCurrentMap.Name = "txtCurrentMap";
            this.txtCurrentMap.Size = new System.Drawing.Size(159, 13);
            this.txtCurrentMap.TabIndex = 8;
            this.txtCurrentMap.Text = "New";
            this.txtCurrentMap.Enter += new System.EventHandler(this.txtAllResources_Enter);
            this.txtCurrentMap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResources_KeyDown);
            this.txtCurrentMap.Leave += new System.EventHandler(this.txtNewResource_Leave);
            // 
            // cbAddToDB
            // 
            this.cbAddToDB.BackColor = System.Drawing.Color.MintCream;
            this.cbAddToDB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAddToDB.Checked = true;
            this.cbAddToDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAddToDB.Location = new System.Drawing.Point(1, 28);
            this.cbAddToDB.Name = "cbAddToDB";
            this.cbAddToDB.Size = new System.Drawing.Size(159, 17);
            this.cbAddToDB.TabIndex = 9;
            this.cbAddToDB.Text = "Add to DataBase";
            this.cbAddToDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAddToDB.UseVisualStyleBackColor = false;
            // 
            // lblCurrentMap
            // 
            this.lblCurrentMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentMap.Location = new System.Drawing.Point(33, 0);
            this.lblCurrentMap.Name = "lblCurrentMap";
            this.lblCurrentMap.Size = new System.Drawing.Size(93, 13);
            this.lblCurrentMap.TabIndex = 10;
            this.lblCurrentMap.Text = "Current Map";
            this.lblCurrentMap.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Location = new System.Drawing.Point(2, 36);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer.Panel1.Controls.Add(this.p11);
            this.splitContainer.Panel1.Controls.Add(this.ListMapResources);
            this.splitContainer.Panel1.Controls.Add(this.txtCurrentMap);
            this.splitContainer.Panel1.Controls.Add(this.lblCurrentMap);
            this.splitContainer.Panel1.Controls.Add(this.cbAddToDB);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer.Panel2.Controls.Add(this.cbDataBaseResources);
            this.splitContainer.Panel2.Controls.Add(this.txtAllResources);
            this.splitContainer.Panel2.Controls.Add(this.ListDBResources);
            this.splitContainer.Size = new System.Drawing.Size(163, 228);
            this.splitContainer.SplitterDistance = 107;
            this.splitContainer.TabIndex = 12;
            // 
            // p11
            // 
            this.p11.Location = new System.Drawing.Point(111, 55);
            this.p11.Name = "p11";
            this.p11.Size = new System.Drawing.Size(35, 24);
            this.p11.TabIndex = 11;
            this.p11.TabStop = false;
            this.p11.Visible = false;
            // 
            // cbDataBaseResources
            // 
            this.cbDataBaseResources.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cbDataBaseResources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBaseResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDataBaseResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbDataBaseResources.FormattingEnabled = true;
            this.cbDataBaseResources.Location = new System.Drawing.Point(0, 0);
            this.cbDataBaseResources.Name = "cbDataBaseResources";
            this.cbDataBaseResources.Size = new System.Drawing.Size(165, 21);
            this.cbDataBaseResources.TabIndex = 12;
            // 
            // txtAllResources
            // 
            this.txtAllResources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAllResources.Location = new System.Drawing.Point(1, 18);
            this.txtAllResources.Name = "txtAllResources";
            this.txtAllResources.Size = new System.Drawing.Size(159, 13);
            this.txtAllResources.TabIndex = 11;
            this.txtAllResources.Enter += new System.EventHandler(this.txtAllResources_Enter);
            this.txtAllResources.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResources_KeyDown);
            this.txtAllResources.Leave += new System.EventHandler(this.txtNewResource_Leave);
            // 
            // cmsResource
            // 
            this.cmsResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_addtotopic,
            this.mi_remove,
            this.mi_addtomap,
            this.mi_rename,
            this.mi_delete,
            this.mi_color});
            this.cmsResource.Name = "cmsResource";
            this.cmsResource.Size = new System.Drawing.Size(190, 136);
            // 
            // mi_addtotopic
            // 
            this.mi_addtotopic.Name = "mi_addtotopic";
            this.mi_addtotopic.Size = new System.Drawing.Size(189, 22);
            this.mi_addtotopic.Text = "Add to topic(s)";
            // 
            // mi_remove
            // 
            this.mi_remove.Name = "mi_remove";
            this.mi_remove.Size = new System.Drawing.Size(189, 22);
            this.mi_remove.Text = "Remove from topic(s)";
            // 
            // mi_addtomap
            // 
            this.mi_addtomap.Name = "mi_addtomap";
            this.mi_addtomap.Size = new System.Drawing.Size(189, 22);
            this.mi_addtomap.Text = "Add to map";
            // 
            // mi_rename
            // 
            this.mi_rename.Name = "mi_rename";
            this.mi_rename.Size = new System.Drawing.Size(189, 22);
            this.mi_rename.Text = "Rename";
            // 
            // mi_delete
            // 
            this.mi_delete.Name = "mi_delete";
            this.mi_delete.Size = new System.Drawing.Size(189, 22);
            this.mi_delete.Text = "Delete";
            // 
            // mi_color
            // 
            this.mi_color.Name = "mi_color";
            this.mi_color.Size = new System.Drawing.Size(189, 22);
            this.mi_color.Text = "Color...";
            // 
            // cbReplaceResources
            // 
            this.cbReplaceResources.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbReplaceResources.Location = new System.Drawing.Point(2, 18);
            this.cbReplaceResources.Name = "cbReplaceResources";
            this.cbReplaceResources.Size = new System.Drawing.Size(163, 17);
            this.cbReplaceResources.TabIndex = 11;
            this.cbReplaceResources.Text = "Replace Resource(s)";
            this.cbReplaceResources.UseVisualStyleBackColor = false;
            // 
            // ResourcesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(167, 279);
            this.Controls.Add(this.cbReplaceResources);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pHandle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ResourcesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResourcesDlg";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pMore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p11)).EndInit();
            this.cmsResource.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListDBResources;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pClose;
        private System.Windows.Forms.PictureBox pHelp;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox pHandle;
        private System.Windows.Forms.PictureBox pMore;
        private System.Windows.Forms.ListView ListMapResources;
        private System.Windows.Forms.TextBox txtCurrentMap;
        private System.Windows.Forms.CheckBox cbAddToDB;
        private System.Windows.Forms.Label lblCurrentMap;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox txtAllResources;
        private System.Windows.Forms.ContextMenuStrip cmsResource;
        private System.Windows.Forms.CheckBox cbReplaceResources;
        private System.Windows.Forms.ToolStripMenuItem mi_delete;
        private System.Windows.Forms.ToolStripMenuItem mi_rename;
        private System.Windows.Forms.ToolStripMenuItem mi_color;
        private System.Windows.Forms.ToolStripMenuItem mi_addtotopic;
        private System.Windows.Forms.ToolStripMenuItem mi_addtomap;
        private System.Windows.Forms.PictureBox p11;
        private System.Windows.Forms.ToolStripMenuItem mi_remove;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cbDataBaseResources;
    }
}