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
            this.ListResources = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pRemoveFilter = new System.Windows.Forms.PictureBox();
            this.pMore = new System.Windows.Forms.PictureBox();
            this.pHelp = new System.Windows.Forms.PictureBox();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.txtResources = new System.Windows.Forms.TextBox();
            this.ResourceEnter = new System.Windows.Forms.PictureBox();
            this.pHandle = new System.Windows.Forms.PictureBox();
            this.panelIcons = new System.Windows.Forms.Panel();
            this.p7 = new System.Windows.Forms.PictureBox();
            this.p6 = new System.Windows.Forms.PictureBox();
            this.p5 = new System.Windows.Forms.PictureBox();
            this.p4 = new System.Windows.Forms.PictureBox();
            this.p3 = new System.Windows.Forms.PictureBox();
            this.p2 = new System.Windows.Forms.PictureBox();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddToTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToMap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.r_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.r_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.r_filter = new System.Windows.Forms.ToolStripMenuItem();
            this.filter_selected = new System.Windows.Forms.ToolStripMenuItem();
            this.filter_icon = new System.Windows.Forms.ToolStripMenuItem();
            this.filter_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.r_newresource = new System.Windows.Forms.ToolStripMenuItem();
            this.ManageIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.r_sort = new System.Windows.Forms.ToolStripMenuItem();
            this.sortAZ = new System.Windows.Forms.ToolStripMenuItem();
            this.sortZA = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.pCorrect = new System.Windows.Forms.PictureBox();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.pResourceMode = new System.Windows.Forms.PictureBox();
            this.pNewResource = new System.Windows.Forms.PictureBox();
            this.pAssignResource = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pRemoveFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceEnter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).BeginInit();
            this.panelIcons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pCorrect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pResourceMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewResource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAssignResource)).BeginInit();
            this.SuspendLayout();
            // 
            // ListResources
            // 
            this.ListResources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListResources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListResources.HideSelection = false;
            this.ListResources.Location = new System.Drawing.Point(2, 36);
            this.ListResources.Name = "ListResources";
            this.ListResources.ShowItemToolTips = true;
            this.ListResources.Size = new System.Drawing.Size(140, 125);
            this.ListResources.SmallImageList = this.imageList1;
            this.ListResources.TabIndex = 0;
            this.ListResources.UseCompatibleStateImageBehavior = false;
            this.ListResources.View = System.Windows.Forms.View.SmallIcon;
            this.ListResources.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listResources_MouseClick);
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
            this.panel1.Controls.Add(this.pRemoveFilter);
            this.panel1.Controls.Add(this.pMore);
            this.panel1.Controls.Add(this.pHelp);
            this.panel1.Controls.Add(this.pClose);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(142, 18);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pRemoveFilter
            // 
            this.pRemoveFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pRemoveFilter.Image = ((System.Drawing.Image)(resources.GetObject("pRemoveFilter.Image")));
            this.pRemoveFilter.Location = new System.Drawing.Point(55, 1);
            this.pRemoveFilter.Name = "pRemoveFilter";
            this.pRemoveFilter.Size = new System.Drawing.Size(16, 16);
            this.pRemoveFilter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRemoveFilter.TabIndex = 46;
            this.pRemoveFilter.TabStop = false;
            this.pRemoveFilter.Visible = false;
            this.pRemoveFilter.Click += new System.EventHandler(this.pRemoveFilter_Click);
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
            this.pMore.Click += new System.EventHandler(this.pManageIcons_Click);
            // 
            // pHelp
            // 
            this.pHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pHelp.Image = ((System.Drawing.Image)(resources.GetObject("pHelp.Image")));
            this.pHelp.Location = new System.Drawing.Point(102, 1);
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
            this.pClose.Location = new System.Drawing.Point(122, 1);
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
            // txtResources
            // 
            this.txtResources.BackColor = System.Drawing.Color.AliceBlue;
            this.txtResources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResources.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtResources.Location = new System.Drawing.Point(16, 19);
            this.txtResources.Name = "txtResources";
            this.txtResources.Size = new System.Drawing.Size(112, 20);
            this.txtResources.TabIndex = 4;
            this.txtResources.Text = "Resources";
            this.txtResources.Enter += new System.EventHandler(this.txtResources_Enter);
            this.txtResources.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResources_KeyDown);
            this.txtResources.Leave += new System.EventHandler(this.txtResources_Leave);
            // 
            // ResourceEnter
            // 
            this.ResourceEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourceEnter.Image = ((System.Drawing.Image)(resources.GetObject("ResourceEnter.Image")));
            this.ResourceEnter.Location = new System.Drawing.Point(127, 19);
            this.ResourceEnter.Name = "ResourceEnter";
            this.ResourceEnter.Size = new System.Drawing.Size(16, 16);
            this.ResourceEnter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ResourceEnter.TabIndex = 6;
            this.ResourceEnter.TabStop = false;
            this.ResourceEnter.Click += new System.EventHandler(this.ResourceEnter_Click);
            // 
            // pHandle
            // 
            this.pHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pHandle.Image = ((System.Drawing.Image)(resources.GetObject("pHandle.Image")));
            this.pHandle.Location = new System.Drawing.Point(61, 165);
            this.pHandle.Name = "pHandle";
            this.pHandle.Size = new System.Drawing.Size(24, 6);
            this.pHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHandle.TabIndex = 6;
            this.pHandle.TabStop = false;
            // 
            // panelIcons
            // 
            this.panelIcons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelIcons.Controls.Add(this.p7);
            this.panelIcons.Controls.Add(this.p6);
            this.panelIcons.Controls.Add(this.p5);
            this.panelIcons.Controls.Add(this.p4);
            this.panelIcons.Controls.Add(this.p3);
            this.panelIcons.Controls.Add(this.p2);
            this.panelIcons.Controls.Add(this.p1);
            this.panelIcons.Location = new System.Drawing.Point(18, 101);
            this.panelIcons.Name = "panelIcons";
            this.panelIcons.Size = new System.Drawing.Size(125, 20);
            this.panelIcons.TabIndex = 40;
            this.panelIcons.Visible = false;
            this.panelIcons.Leave += new System.EventHandler(this.panelIcons_Leave);
            // 
            // p7
            // 
            this.p7.Image = ((System.Drawing.Image)(resources.GetObject("p7.Image")));
            this.p7.Location = new System.Drawing.Point(108, 1);
            this.p7.Name = "p7";
            this.p7.Size = new System.Drawing.Size(16, 16);
            this.p7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p7.TabIndex = 13;
            this.p7.TabStop = false;
            this.p7.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p6
            // 
            this.p6.BackColor = System.Drawing.SystemColors.Control;
            this.p6.Image = ((System.Drawing.Image)(resources.GetObject("p6.Image")));
            this.p6.Location = new System.Drawing.Point(90, 1);
            this.p6.Name = "p6";
            this.p6.Size = new System.Drawing.Size(16, 16);
            this.p6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p6.TabIndex = 12;
            this.p6.TabStop = false;
            this.p6.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p5
            // 
            this.p5.Image = ((System.Drawing.Image)(resources.GetObject("p5.Image")));
            this.p5.Location = new System.Drawing.Point(72, 1);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(16, 16);
            this.p5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p5.TabIndex = 11;
            this.p5.TabStop = false;
            this.p5.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p4
            // 
            this.p4.Image = ((System.Drawing.Image)(resources.GetObject("p4.Image")));
            this.p4.Location = new System.Drawing.Point(54, 1);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(16, 16);
            this.p4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p4.TabIndex = 9;
            this.p4.TabStop = false;
            this.p4.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p3
            // 
            this.p3.Image = ((System.Drawing.Image)(resources.GetObject("p3.Image")));
            this.p3.Location = new System.Drawing.Point(36, 1);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(16, 16);
            this.p3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p3.TabIndex = 8;
            this.p3.TabStop = false;
            this.p3.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p2
            // 
            this.p2.Image = ((System.Drawing.Image)(resources.GetObject("p2.Image")));
            this.p2.Location = new System.Drawing.Point(18, 1);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(16, 16);
            this.p2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p2.TabIndex = 7;
            this.p2.TabStop = false;
            this.p2.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p1
            // 
            this.p1.Image = ((System.Drawing.Image)(resources.GetObject("p1.Image")));
            this.p1.Location = new System.Drawing.Point(0, 1);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 6;
            this.p1.TabStop = false;
            this.p1.Click += new System.EventHandler(this.Icon_Click);
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToTopic,
            this.AddToMap,
            this.toolStripSeparator1,
            this.r_rename,
            this.ChangeIcon,
            this.RemoveIcon,
            this.r_delete,
            this.toolStripSeparator2,
            this.r_filter,
            this.filter_selected,
            this.filter_icon,
            this.filter_remove,
            this.r_newresource,
            this.ManageIcons,
            this.toolStripSeparator3,
            this.r_sort,
            this.sortAZ,
            this.sortZA,
            this.sortByIcon,
            this.toolStripSeparator4});
            this.cmsMenu.Name = "cmsResource";
            this.cmsMenu.Size = new System.Drawing.Size(158, 380);
            // 
            // AddToTopic
            // 
            this.AddToTopic.Name = "AddToTopic";
            this.AddToTopic.Size = new System.Drawing.Size(157, 22);
            this.AddToTopic.Text = "Add To Topic";
            // 
            // AddToMap
            // 
            this.AddToMap.Name = "AddToMap";
            this.AddToMap.Size = new System.Drawing.Size(157, 22);
            this.AddToMap.Text = "Add To Map";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // r_rename
            // 
            this.r_rename.Name = "r_rename";
            this.r_rename.Size = new System.Drawing.Size(157, 22);
            this.r_rename.Text = "Rename";
            // 
            // ChangeIcon
            // 
            this.ChangeIcon.Name = "ChangeIcon";
            this.ChangeIcon.Size = new System.Drawing.Size(157, 22);
            this.ChangeIcon.Text = "Change Icon";
            // 
            // RemoveIcon
            // 
            this.RemoveIcon.Name = "RemoveIcon";
            this.RemoveIcon.Size = new System.Drawing.Size(157, 22);
            this.RemoveIcon.Text = "Remove Icon";
            // 
            // r_delete
            // 
            this.r_delete.Name = "r_delete";
            this.r_delete.Size = new System.Drawing.Size(157, 22);
            this.r_delete.Text = "Delete";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // r_filter
            // 
            this.r_filter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.r_filter.Name = "r_filter";
            this.r_filter.Size = new System.Drawing.Size(157, 22);
            this.r_filter.Text = "Filter";
            // 
            // filter_selected
            // 
            this.filter_selected.Name = "filter_selected";
            this.filter_selected.Size = new System.Drawing.Size(157, 22);
            this.filter_selected.Text = "Show Selected";
            // 
            // filter_icon
            // 
            this.filter_icon.Name = "filter_icon";
            this.filter_icon.Size = new System.Drawing.Size(157, 22);
            this.filter_icon.Text = "Show With Icon";
            // 
            // filter_remove
            // 
            this.filter_remove.Name = "filter_remove";
            this.filter_remove.Size = new System.Drawing.Size(157, 22);
            this.filter_remove.Text = "Remove Filter";
            // 
            // r_newresource
            // 
            this.r_newresource.Name = "r_newresource";
            this.r_newresource.Size = new System.Drawing.Size(157, 22);
            this.r_newresource.Text = "New Resource";
            // 
            // ManageIcons
            // 
            this.ManageIcons.Name = "ManageIcons";
            this.ManageIcons.Size = new System.Drawing.Size(157, 22);
            this.ManageIcons.Text = "Manage Icons";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // r_sort
            // 
            this.r_sort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.r_sort.Name = "r_sort";
            this.r_sort.Size = new System.Drawing.Size(157, 22);
            this.r_sort.Text = "Sort";
            // 
            // sortAZ
            // 
            this.sortAZ.Name = "sortAZ";
            this.sortAZ.Size = new System.Drawing.Size(157, 22);
            this.sortAZ.Text = "A - Z";
            // 
            // sortZA
            // 
            this.sortZA.Name = "sortZA";
            this.sortZA.Size = new System.Drawing.Size(157, 22);
            this.sortZA.Text = "Z - A";
            // 
            // sortByIcon
            // 
            this.sortByIcon.Name = "sortByIcon";
            this.sortByIcon.Size = new System.Drawing.Size(157, 22);
            this.sortByIcon.Text = "By Icon";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(154, 6);
            // 
            // pCorrect
            // 
            this.pCorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pCorrect.Image = ((System.Drawing.Image)(resources.GetObject("pCorrect.Image")));
            this.pCorrect.Location = new System.Drawing.Point(84, 9);
            this.pCorrect.Name = "pCorrect";
            this.pCorrect.Size = new System.Drawing.Size(16, 2);
            this.pCorrect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCorrect.TabIndex = 14;
            this.pCorrect.TabStop = false;
            this.pCorrect.Visible = false;
            // 
            // txtRename
            // 
            this.txtRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtRename.Location = new System.Drawing.Point(19, 126);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(125, 20);
            this.txtRename.TabIndex = 41;
            this.txtRename.Visible = false;
            this.txtRename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRename_KeyDown);
            this.txtRename.Leave += new System.EventHandler(this.txtRename_Leave);
            // 
            // pResourceMode
            // 
            this.pResourceMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pResourceMode.Image = ((System.Drawing.Image)(resources.GetObject("pResourceMode.Image")));
            this.pResourceMode.Location = new System.Drawing.Point(1, 19);
            this.pResourceMode.Name = "pResourceMode";
            this.pResourceMode.Size = new System.Drawing.Size(16, 16);
            this.pResourceMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pResourceMode.TabIndex = 42;
            this.pResourceMode.TabStop = false;
            this.pResourceMode.Click += new System.EventHandler(this.pNewResource_Click);
            // 
            // pNewResource
            // 
            this.pNewResource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pNewResource.Image = ((System.Drawing.Image)(resources.GetObject("pNewResource.Image")));
            this.pNewResource.Location = new System.Drawing.Point(2, 41);
            this.pNewResource.Name = "pNewResource";
            this.pNewResource.Size = new System.Drawing.Size(16, 16);
            this.pNewResource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pNewResource.TabIndex = 44;
            this.pNewResource.TabStop = false;
            this.pNewResource.Visible = false;
            // 
            // pAssignResource
            // 
            this.pAssignResource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pAssignResource.Image = ((System.Drawing.Image)(resources.GetObject("pAssignResource.Image")));
            this.pAssignResource.Location = new System.Drawing.Point(21, 41);
            this.pAssignResource.Name = "pAssignResource";
            this.pAssignResource.Size = new System.Drawing.Size(16, 16);
            this.pAssignResource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAssignResource.TabIndex = 45;
            this.pAssignResource.TabStop = false;
            this.pAssignResource.Visible = false;
            // 
            // ResourcesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 174);
            this.Controls.Add(this.pAssignResource);
            this.Controls.Add(this.pNewResource);
            this.Controls.Add(this.ResourceEnter);
            this.Controls.Add(this.pResourceMode);
            this.Controls.Add(this.txtRename);
            this.Controls.Add(this.pCorrect);
            this.Controls.Add(this.pHandle);
            this.Controls.Add(this.txtResources);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelIcons);
            this.Controls.Add(this.ListResources);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ResourcesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResourcesDlg";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pRemoveFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceEnter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).EndInit();
            this.panelIcons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pCorrect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pResourceMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewResource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAssignResource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListResources;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pClose;
        private System.Windows.Forms.PictureBox pHelp;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TextBox txtResources;
        private System.Windows.Forms.PictureBox ResourceEnter;
        private System.Windows.Forms.PictureBox pHandle;
        private System.Windows.Forms.Panel panelIcons;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.PictureBox p7;
        private System.Windows.Forms.PictureBox p6;
        private System.Windows.Forms.PictureBox p5;
        private System.Windows.Forms.PictureBox p4;
        private System.Windows.Forms.PictureBox p3;
        private System.Windows.Forms.PictureBox p2;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem AddToTopic;
        private System.Windows.Forms.ToolStripMenuItem AddToMap;
        private System.Windows.Forms.ToolStripMenuItem ChangeIcon;
        private System.Windows.Forms.ToolStripMenuItem r_delete;
        private System.Windows.Forms.ToolStripMenuItem r_rename;
        private System.Windows.Forms.PictureBox pCorrect;
        private System.Windows.Forms.PictureBox pMore;
        private System.Windows.Forms.ToolStripMenuItem r_newresource;
        private System.Windows.Forms.ToolStripMenuItem r_sort;
        private System.Windows.Forms.ToolStripMenuItem sortAZ;
        private System.Windows.Forms.ToolStripMenuItem sortZA;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem sortByIcon;
        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ManageIcons;
        private System.Windows.Forms.ToolStripMenuItem filter_selected;
        private System.Windows.Forms.ToolStripMenuItem filter_remove;
        private System.Windows.Forms.ToolStripMenuItem r_filter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem filter_icon;
        private System.Windows.Forms.PictureBox pResourceMode;
        private System.Windows.Forms.PictureBox pNewResource;
        private System.Windows.Forms.ToolStripMenuItem RemoveIcon;
        private System.Windows.Forms.PictureBox pAssignResource;
        private System.Windows.Forms.PictureBox pRemoveFilter;
    }
}