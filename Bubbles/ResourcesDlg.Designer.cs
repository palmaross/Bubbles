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
            this.pManageIcons = new System.Windows.Forms.PictureBox();
            this.Sort = new System.Windows.Forms.PictureBox();
            this.Help = new System.Windows.Forms.PictureBox();
            this.Close = new System.Windows.Forms.PictureBox();
            this.Edit = new System.Windows.Forms.PictureBox();
            this.Delete = new System.Windows.Forms.PictureBox();
            this.New = new System.Windows.Forms.PictureBox();
            this.btnAddToTopic = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.panelResourceName = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtRenameResource = new System.Windows.Forms.TextBox();
            this.lblResourceName = new System.Windows.Forms.Label();
            this.btnAddToMap = new System.Windows.Forms.Button();
            this.txtResources = new System.Windows.Forms.TextBox();
            this.pAddToMap = new System.Windows.Forms.PictureBox();
            this.pHandle = new System.Windows.Forms.PictureBox();
            this.cmsSort = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sortAZ = new System.Windows.Forms.ToolStripMenuItem();
            this.sortZA = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.panelIcons = new System.Windows.Forms.Panel();
            this.p7 = new System.Windows.Forms.PictureBox();
            this.p6 = new System.Windows.Forms.PictureBox();
            this.p5 = new System.Windows.Forms.PictureBox();
            this.p4 = new System.Windows.Forms.PictureBox();
            this.p3 = new System.Windows.Forms.PictureBox();
            this.p2 = new System.Windows.Forms.PictureBox();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.p0 = new System.Windows.Forms.PictureBox();
            this.cmsResource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddToTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToMap = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.r_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.r_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.pCorrect = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pManageIcons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Help)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.New)).BeginInit();
            this.panelResourceName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pAddToMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).BeginInit();
            this.cmsSort.SuspendLayout();
            this.panelIcons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p0)).BeginInit();
            this.cmsResource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pCorrect)).BeginInit();
            this.SuspendLayout();
            // 
            // ListResources
            // 
            this.ListResources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListResources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListResources.HideSelection = false;
            this.ListResources.Location = new System.Drawing.Point(1, 40);
            this.ListResources.Name = "ListResources";
            this.ListResources.ShowItemToolTips = true;
            this.ListResources.Size = new System.Drawing.Size(182, 155);
            this.ListResources.SmallImageList = this.imageList1;
            this.ListResources.TabIndex = 0;
            this.ListResources.UseCompatibleStateImageBehavior = false;
            this.ListResources.View = System.Windows.Forms.View.List;
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
            this.panel1.Controls.Add(this.pManageIcons);
            this.panel1.Controls.Add(this.Sort);
            this.panel1.Controls.Add(this.Help);
            this.panel1.Controls.Add(this.Close);
            this.panel1.Controls.Add(this.Edit);
            this.panel1.Controls.Add(this.Delete);
            this.panel1.Controls.Add(this.New);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 22);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pManageIcons
            // 
            this.pManageIcons.Image = ((System.Drawing.Image)(resources.GetObject("pManageIcons.Image")));
            this.pManageIcons.Location = new System.Drawing.Point(75, 1);
            this.pManageIcons.Name = "pManageIcons";
            this.pManageIcons.Size = new System.Drawing.Size(20, 20);
            this.pManageIcons.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pManageIcons.TabIndex = 6;
            this.pManageIcons.TabStop = false;
            this.pManageIcons.Click += new System.EventHandler(this.pManageIcons_Click);
            // 
            // Sort
            // 
            this.Sort.Image = ((System.Drawing.Image)(resources.GetObject("Sort.Image")));
            this.Sort.Location = new System.Drawing.Point(112, 3);
            this.Sort.Name = "Sort";
            this.Sort.Size = new System.Drawing.Size(16, 16);
            this.Sort.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Sort.TabIndex = 5;
            this.Sort.TabStop = false;
            this.Sort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Sort_MouseClick);
            // 
            // Help
            // 
            this.Help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Help.Image = ((System.Drawing.Image)(resources.GetObject("Help.Image")));
            this.Help.Location = new System.Drawing.Point(142, 3);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(16, 16);
            this.Help.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Help.TabIndex = 4;
            this.Help.TabStop = false;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.Image = ((System.Drawing.Image)(resources.GetObject("Close.Image")));
            this.Close.Location = new System.Drawing.Point(162, 3);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(16, 16);
            this.Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Close.TabIndex = 3;
            this.Close.TabStop = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Edit
            // 
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Location = new System.Drawing.Point(25, 1);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(20, 20);
            this.Edit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Edit.TabIndex = 2;
            this.Edit.TabStop = false;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Delete
            // 
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Location = new System.Drawing.Point(50, 1);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(20, 20);
            this.Delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Delete.TabIndex = 1;
            this.Delete.TabStop = false;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // New
            // 
            this.New.Image = ((System.Drawing.Image)(resources.GetObject("New.Image")));
            this.New.Location = new System.Drawing.Point(1, 1);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(20, 20);
            this.New.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.New.TabIndex = 0;
            this.New.TabStop = false;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // btnAddToTopic
            // 
            this.btnAddToTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddToTopic.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddToTopic.Location = new System.Drawing.Point(0, 196);
            this.btnAddToTopic.Name = "btnAddToTopic";
            this.btnAddToTopic.Size = new System.Drawing.Size(93, 23);
            this.btnAddToTopic.TabIndex = 2;
            this.btnAddToTopic.Text = "Add To Topic";
            this.btnAddToTopic.UseVisualStyleBackColor = true;
            this.btnAddToTopic.Click += new System.EventHandler(this.btnAddToTopic_Click);
            // 
            // panelResourceName
            // 
            this.panelResourceName.Controls.Add(this.btnCancel);
            this.panelResourceName.Controls.Add(this.btnOk);
            this.panelResourceName.Controls.Add(this.txtRenameResource);
            this.panelResourceName.Controls.Add(this.lblResourceName);
            this.panelResourceName.Location = new System.Drawing.Point(0, 68);
            this.panelResourceName.Name = "panelResourceName";
            this.panelResourceName.Size = new System.Drawing.Size(184, 80);
            this.panelResourceName.TabIndex = 3;
            this.panelResourceName.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(106, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(6, 50);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // txtRenameResource
            // 
            this.txtRenameResource.Location = new System.Drawing.Point(6, 24);
            this.txtRenameResource.Name = "txtRenameResource";
            this.txtRenameResource.Size = new System.Drawing.Size(172, 20);
            this.txtRenameResource.TabIndex = 1;
            // 
            // lblResourceName
            // 
            this.lblResourceName.AutoSize = true;
            this.lblResourceName.Location = new System.Drawing.Point(4, 7);
            this.lblResourceName.Name = "lblResourceName";
            this.lblResourceName.Size = new System.Drawing.Size(76, 13);
            this.lblResourceName.TabIndex = 0;
            this.lblResourceName.Text = "Имя ресурса:";
            // 
            // btnAddToMap
            // 
            this.btnAddToMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddToMap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddToMap.Location = new System.Drawing.Point(92, 196);
            this.btnAddToMap.Name = "btnAddToMap";
            this.btnAddToMap.Size = new System.Drawing.Size(91, 23);
            this.btnAddToMap.TabIndex = 39;
            this.btnAddToMap.Text = "Add To Map";
            this.btnAddToMap.UseVisualStyleBackColor = true;
            this.btnAddToMap.Click += new System.EventHandler(this.btnAddToMap_Click);
            // 
            // txtResources
            // 
            this.txtResources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResources.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtResources.Location = new System.Drawing.Point(0, 23);
            this.txtResources.Name = "txtResources";
            this.txtResources.Size = new System.Drawing.Size(183, 20);
            this.txtResources.TabIndex = 4;
            this.txtResources.Text = "Resources (separated by commas)";
            this.txtResources.Enter += new System.EventHandler(this.txtResources_Enter);
            this.txtResources.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResources_KeyDown);
            this.txtResources.Leave += new System.EventHandler(this.txtResources_Leave);
            // 
            // pAddToMap
            // 
            this.pAddToMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pAddToMap.Image = ((System.Drawing.Image)(resources.GetObject("pAddToMap.Image")));
            this.pAddToMap.Location = new System.Drawing.Point(167, 23);
            this.pAddToMap.Name = "pAddToMap";
            this.pAddToMap.Size = new System.Drawing.Size(16, 16);
            this.pAddToMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAddToMap.TabIndex = 6;
            this.pAddToMap.TabStop = false;
            this.pAddToMap.Click += new System.EventHandler(this.pAddToMap_Click);
            // 
            // pHandle
            // 
            this.pHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pHandle.Image = ((System.Drawing.Image)(resources.GetObject("pHandle.Image")));
            this.pHandle.Location = new System.Drawing.Point(80, 221);
            this.pHandle.Name = "pHandle";
            this.pHandle.Size = new System.Drawing.Size(24, 6);
            this.pHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHandle.TabIndex = 6;
            this.pHandle.TabStop = false;
            // 
            // cmsSort
            // 
            this.cmsSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortAZ,
            this.sortZA,
            this.sortByIcon});
            this.cmsSort.Name = "cmsSort";
            this.cmsSort.Size = new System.Drawing.Size(198, 70);
            // 
            // sortAZ
            // 
            this.sortAZ.Name = "sortAZ";
            this.sortAZ.Size = new System.Drawing.Size(197, 22);
            this.sortAZ.Text = "Сортировать A-Z";
            // 
            // sortZA
            // 
            this.sortZA.Name = "sortZA";
            this.sortZA.Size = new System.Drawing.Size(197, 22);
            this.sortZA.Text = "Сортировать Z-A";
            // 
            // sortByIcon
            // 
            this.sortByIcon.Name = "sortByIcon";
            this.sortByIcon.Size = new System.Drawing.Size(197, 22);
            this.sortByIcon.Text = "Сортировать по метке";
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
            this.panelIcons.Controls.Add(this.p0);
            this.panelIcons.Location = new System.Drawing.Point(17, 154);
            this.panelIcons.Name = "panelIcons";
            this.panelIcons.Size = new System.Drawing.Size(162, 20);
            this.panelIcons.TabIndex = 40;
            this.panelIcons.Visible = false;
            this.panelIcons.Leave += new System.EventHandler(this.panelIcons_Leave);
            // 
            // p7
            // 
            this.p7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p7.Image = ((System.Drawing.Image)(resources.GetObject("p7.Image")));
            this.p7.Location = new System.Drawing.Point(142, 1);
            this.p7.Name = "p7";
            this.p7.Size = new System.Drawing.Size(16, 16);
            this.p7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p7.TabIndex = 13;
            this.p7.TabStop = false;
            this.p7.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p6
            // 
            this.p6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p6.Image = ((System.Drawing.Image)(resources.GetObject("p6.Image")));
            this.p6.Location = new System.Drawing.Point(122, 1);
            this.p6.Name = "p6";
            this.p6.Size = new System.Drawing.Size(16, 16);
            this.p6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p6.TabIndex = 12;
            this.p6.TabStop = false;
            this.p6.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p5
            // 
            this.p5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p5.Image = ((System.Drawing.Image)(resources.GetObject("p5.Image")));
            this.p5.Location = new System.Drawing.Point(102, 1);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(16, 16);
            this.p5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p5.TabIndex = 11;
            this.p5.TabStop = false;
            this.p5.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p4
            // 
            this.p4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p4.Image = ((System.Drawing.Image)(resources.GetObject("p4.Image")));
            this.p4.Location = new System.Drawing.Point(82, 1);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(16, 16);
            this.p4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p4.TabIndex = 10;
            this.p4.TabStop = false;
            this.p4.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p3
            // 
            this.p3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p3.Image = ((System.Drawing.Image)(resources.GetObject("p3.Image")));
            this.p3.Location = new System.Drawing.Point(62, 1);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(16, 16);
            this.p3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p3.TabIndex = 9;
            this.p3.TabStop = false;
            this.p3.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p2
            // 
            this.p2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p2.Image = ((System.Drawing.Image)(resources.GetObject("p2.Image")));
            this.p2.Location = new System.Drawing.Point(42, 1);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(16, 16);
            this.p2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p2.TabIndex = 8;
            this.p2.TabStop = false;
            this.p2.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p1
            // 
            this.p1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p1.Image = ((System.Drawing.Image)(resources.GetObject("p1.Image")));
            this.p1.Location = new System.Drawing.Point(22, 1);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 7;
            this.p1.TabStop = false;
            this.p1.Click += new System.EventHandler(this.Icon_Click);
            // 
            // p0
            // 
            this.p0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.p0.Image = ((System.Drawing.Image)(resources.GetObject("p0.Image")));
            this.p0.Location = new System.Drawing.Point(2, 1);
            this.p0.Name = "p0";
            this.p0.Size = new System.Drawing.Size(16, 16);
            this.p0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p0.TabIndex = 6;
            this.p0.TabStop = false;
            this.p0.Click += new System.EventHandler(this.Icon_Click);
            // 
            // cmsResource
            // 
            this.cmsResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToTopic,
            this.AddToMap,
            this.ChangeIcon,
            this.r_rename,
            this.r_delete});
            this.cmsResource.Name = "cmsResource";
            this.cmsResource.Size = new System.Drawing.Size(143, 114);
            // 
            // AddToTopic
            // 
            this.AddToTopic.Name = "AddToTopic";
            this.AddToTopic.Size = new System.Drawing.Size(142, 22);
            this.AddToTopic.Text = "Add To Topic";
            // 
            // AddToMap
            // 
            this.AddToMap.Name = "AddToMap";
            this.AddToMap.Size = new System.Drawing.Size(142, 22);
            this.AddToMap.Text = "Add To Map";
            // 
            // ChangeIcon
            // 
            this.ChangeIcon.Name = "ChangeIcon";
            this.ChangeIcon.Size = new System.Drawing.Size(142, 22);
            this.ChangeIcon.Text = "Change Icon";
            // 
            // r_rename
            // 
            this.r_rename.Name = "r_rename";
            this.r_rename.Size = new System.Drawing.Size(142, 22);
            this.r_rename.Text = "Rename";
            // 
            // r_delete
            // 
            this.r_delete.Name = "r_delete";
            this.r_delete.Size = new System.Drawing.Size(142, 22);
            this.r_delete.Text = "Delete";
            // 
            // pCorrect
            // 
            this.pCorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pCorrect.Image = ((System.Drawing.Image)(resources.GetObject("pCorrect.Image")));
            this.pCorrect.Location = new System.Drawing.Point(156, 46);
            this.pCorrect.Name = "pCorrect";
            this.pCorrect.Size = new System.Drawing.Size(16, 2);
            this.pCorrect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCorrect.TabIndex = 14;
            this.pCorrect.TabStop = false;
            this.pCorrect.Visible = false;
            // 
            // ResourcesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 230);
            this.Controls.Add(this.pCorrect);
            this.Controls.Add(this.pHandle);
            this.Controls.Add(this.pAddToMap);
            this.Controls.Add(this.txtResources);
            this.Controls.Add(this.btnAddToMap);
            this.Controls.Add(this.panelResourceName);
            this.Controls.Add(this.btnAddToTopic);
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
            ((System.ComponentModel.ISupportInitialize)(this.pManageIcons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Help)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.New)).EndInit();
            this.panelResourceName.ResumeLayout(false);
            this.panelResourceName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pAddToMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pHandle)).EndInit();
            this.cmsSort.ResumeLayout(false);
            this.panelIcons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p0)).EndInit();
            this.cmsResource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pCorrect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListResources;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Edit;
        private System.Windows.Forms.PictureBox Delete;
        private System.Windows.Forms.PictureBox New;
        private System.Windows.Forms.PictureBox Close;
        private System.Windows.Forms.Button btnAddToTopic;
        private System.Windows.Forms.PictureBox Help;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Panel panelResourceName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtRenameResource;
        private System.Windows.Forms.Label lblResourceName;
        private System.Windows.Forms.Button btnAddToMap;
        private System.Windows.Forms.PictureBox Sort;
        private System.Windows.Forms.TextBox txtResources;
        private System.Windows.Forms.PictureBox pAddToMap;
        private System.Windows.Forms.PictureBox pHandle;
        private System.Windows.Forms.ContextMenuStrip cmsSort;
        private System.Windows.Forms.ToolStripMenuItem sortAZ;
        private System.Windows.Forms.ToolStripMenuItem sortZA;
        private System.Windows.Forms.ToolStripMenuItem sortByIcon;
        private System.Windows.Forms.Panel panelIcons;
        private System.Windows.Forms.PictureBox p0;
        private System.Windows.Forms.PictureBox p7;
        private System.Windows.Forms.PictureBox p6;
        private System.Windows.Forms.PictureBox p5;
        private System.Windows.Forms.PictureBox p4;
        private System.Windows.Forms.PictureBox p3;
        private System.Windows.Forms.PictureBox p2;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.ContextMenuStrip cmsResource;
        private System.Windows.Forms.ToolStripMenuItem AddToTopic;
        private System.Windows.Forms.ToolStripMenuItem AddToMap;
        private System.Windows.Forms.ToolStripMenuItem ChangeIcon;
        private System.Windows.Forms.ToolStripMenuItem r_delete;
        private System.Windows.Forms.ToolStripMenuItem r_rename;
        private System.Windows.Forms.PictureBox pCorrect;
        private System.Windows.Forms.PictureBox pManageIcons;
    }
}