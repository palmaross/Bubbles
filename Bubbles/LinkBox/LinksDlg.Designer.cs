namespace Bubbles
{
    partial class LinksDlg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtEditNode = new System.Windows.Forms.TextBox();
            this.pSize = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LinkImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.Learned = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LinkTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortByImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnOmniBrowser = new System.Windows.Forms.RadioButton();
            this.rbtnExternalApp = new System.Windows.Forms.RadioButton();
            this.lblOpenIn = new System.Windows.Forms.Label();
            this.panelModify = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtLink2 = new System.Windows.Forms.TextBox();
            this.lblLink = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grBoxDownload = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.cbDownload = new System.Windows.Forms.CheckBox();
            this.cmsLink = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_OpenLink = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OpenInOmniBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_NewLink = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Modify = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.cmsGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.g_AddLink = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.g_AddGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.g_RenameGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.g_DeleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListDrag = new System.Windows.Forms.ImageList(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelModify.SuspendLayout();
            this.grBoxDownload.SuspendLayout();
            this.cmsLink.SuspendLayout();
            this.cmsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtEditNode);
            this.splitContainer1.Panel1.Controls.Add(this.pSize);
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.txtLink);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(645, 321);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtEditNode
            // 
            this.txtEditNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEditNode.Location = new System.Drawing.Point(12, 12);
            this.txtEditNode.Name = "txtEditNode";
            this.txtEditNode.Size = new System.Drawing.Size(121, 20);
            this.txtEditNode.TabIndex = 17;
            this.txtEditNode.Visible = false;
            this.txtEditNode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEditNode_KeyUp);
            // 
            // pSize
            // 
            this.pSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pSize.Location = new System.Drawing.Point(65, 188);
            this.pSize.Name = "pSize";
            this.pSize.Size = new System.Drawing.Size(16, 16);
            this.pSize.TabIndex = 5;
            this.pSize.TabStop = false;
            this.pSize.Visible = false;
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(159, 321);
            this.treeView1.TabIndex = 0;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.GroupsTree_NodeMouseClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.GroupsTree_DragOver);
            this.treeView1.DragLeave += new System.EventHandler(this.treeView1_DragLeave);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LinkImage,
            this.Learned,
            this.LinkTitle,
            this.LinkGroup,
            this.LinkPath,
            this.LinkType,
            this.GroupID,
            this.SortByImage});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(482, 234);
            this.dataGridView1.TabIndex = 21;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            this.dataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseMove);
            // 
            // LinkImage
            // 
            this.LinkImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LinkImage.FillWeight = 10.61452F;
            this.LinkImage.HeaderText = "";
            this.LinkImage.Name = "LinkImage";
            this.LinkImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Learned
            // 
            this.Learned.HeaderText = "";
            this.Learned.Name = "Learned";
            // 
            // LinkTitle
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LinkTitle.DefaultCellStyle = dataGridViewCellStyle5;
            this.LinkTitle.FillWeight = 101.7739F;
            this.LinkTitle.HeaderText = "Title";
            this.LinkTitle.Name = "LinkTitle";
            // 
            // LinkGroup
            // 
            this.LinkGroup.HeaderText = "Group";
            this.LinkGroup.Name = "LinkGroup";
            // 
            // LinkPath
            // 
            this.LinkPath.HeaderText = "Path";
            this.LinkPath.Name = "LinkPath";
            this.LinkPath.Visible = false;
            // 
            // LinkType
            // 
            this.LinkType.HeaderText = "Type";
            this.LinkType.Name = "LinkType";
            this.LinkType.Visible = false;
            // 
            // GroupID
            // 
            this.GroupID.HeaderText = "GroupID";
            this.GroupID.Name = "GroupID";
            this.GroupID.Visible = false;
            // 
            // SortByImage
            // 
            this.SortByImage.HeaderText = "SortByImage";
            this.SortByImage.Name = "SortByImage";
            this.SortByImage.Visible = false;
            // 
            // txtLink
            // 
            this.txtLink.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLink.Location = new System.Drawing.Point(0, 257);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(482, 20);
            this.txtLink.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.SeaShell;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 277);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(482, 44);
            this.textBox1.TabIndex = 22;
            this.textBox1.Text = "1\r\n2\r\n3";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.rbtnOmniBrowser);
            this.panel1.Controls.Add(this.rbtnExternalApp);
            this.panel1.Controls.Add(this.lblOpenIn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 23);
            this.panel1.TabIndex = 5;
            // 
            // rbtnOmniBrowser
            // 
            this.rbtnOmniBrowser.AutoSize = true;
            this.rbtnOmniBrowser.Location = new System.Drawing.Point(231, 3);
            this.rbtnOmniBrowser.Name = "rbtnOmniBrowser";
            this.rbtnOmniBrowser.Size = new System.Drawing.Size(107, 17);
            this.rbtnOmniBrowser.TabIndex = 20;
            this.rbtnOmniBrowser.Text = "OmniStix Browser";
            this.rbtnOmniBrowser.UseVisualStyleBackColor = true;
            // 
            // rbtnExternalApp
            // 
            this.rbtnExternalApp.AutoSize = true;
            this.rbtnExternalApp.Checked = true;
            this.rbtnExternalApp.Location = new System.Drawing.Point(97, 3);
            this.rbtnExternalApp.Name = "rbtnExternalApp";
            this.rbtnExternalApp.Size = new System.Drawing.Size(85, 17);
            this.rbtnExternalApp.TabIndex = 19;
            this.rbtnExternalApp.TabStop = true;
            this.rbtnExternalApp.Text = "External App";
            this.rbtnExternalApp.UseVisualStyleBackColor = true;
            // 
            // lblOpenIn
            // 
            this.lblOpenIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOpenIn.AutoSize = true;
            this.lblOpenIn.Location = new System.Drawing.Point(3, 5);
            this.lblOpenIn.Name = "lblOpenIn";
            this.lblOpenIn.Size = new System.Drawing.Size(77, 13);
            this.lblOpenIn.TabIndex = 18;
            this.lblOpenIn.Text = "Открывать in:";
            // 
            // panelModify
            // 
            this.panelModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelModify.BackColor = System.Drawing.Color.SeaShell;
            this.panelModify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModify.Controls.Add(this.lblResult);
            this.panelModify.Controls.Add(this.lblWait);
            this.panelModify.Controls.Add(this.btnOK);
            this.panelModify.Controls.Add(this.btnClose);
            this.panelModify.Controls.Add(this.txtLink2);
            this.panelModify.Controls.Add(this.lblLink);
            this.panelModify.Controls.Add(this.txtTitle);
            this.panelModify.Controls.Add(this.lblTitle);
            this.panelModify.Controls.Add(this.grBoxDownload);
            this.panelModify.Location = new System.Drawing.Point(99, 72);
            this.panelModify.Name = "panelModify";
            this.panelModify.Size = new System.Drawing.Size(450, 202);
            this.panelModify.TabIndex = 22;
            this.panelModify.Visible = false;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblResult.Location = new System.Drawing.Point(91, 168);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(265, 15);
            this.lblResult.TabIndex = 23;
            this.lblResult.Text = "Link added successfully";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult.Visible = false;
            // 
            // lblWait
            // 
            this.lblWait.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWait.Location = new System.Drawing.Point(146, 41);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(201, 13);
            this.lblWait.TabIndex = 18;
            this.lblWait.Text = "Wait...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(10, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(362, 164);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtLink2
            // 
            this.txtLink2.Location = new System.Drawing.Point(72, 15);
            this.txtLink2.Name = "txtLink2";
            this.txtLink2.Size = new System.Drawing.Size(365, 20);
            this.txtLink2.TabIndex = 13;
            this.txtLink2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtLink2_KeyUp);
            this.txtLink2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtLink2_MouseDoubleClick);
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(9, 18);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(30, 13);
            this.lblLink.TabIndex = 12;
            this.lblLink.Text = "Link:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(72, 61);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(365, 20);
            this.txtTitle.TabIndex = 11;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(9, 64);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Title:";
            // 
            // grBoxDownload
            // 
            this.grBoxDownload.Controls.Add(this.btnPreview);
            this.grBoxDownload.Controls.Add(this.cbDownload);
            this.grBoxDownload.Location = new System.Drawing.Point(72, 99);
            this.grBoxDownload.Name = "grBoxDownload";
            this.grBoxDownload.Size = new System.Drawing.Size(365, 52);
            this.grBoxDownload.TabIndex = 24;
            this.grBoxDownload.TabStop = false;
            this.grBoxDownload.Text = "You can download page and add link to the downloaded file";
            this.grBoxDownload.Visible = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(206, 20);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(108, 23);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // cbDownload
            // 
            this.cbDownload.AutoSize = true;
            this.cbDownload.Location = new System.Drawing.Point(52, 23);
            this.cbDownload.Name = "cbDownload";
            this.cbDownload.Size = new System.Drawing.Size(116, 17);
            this.cbDownload.TabIndex = 20;
            this.cbDownload.Text = "Скачать страницу";
            this.cbDownload.UseVisualStyleBackColor = true;
            // 
            // cmsLink
            // 
            this.cmsLink.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_OpenLink,
            this.m_OpenInOmniBrowser,
            this.toolStripSeparator1,
            this.m_NewLink,
            this.m_Modify,
            this.m_Delete});
            this.cmsLink.Name = "contextMenu";
            this.cmsLink.Size = new System.Drawing.Size(214, 120);
            // 
            // m_OpenLink
            // 
            this.m_OpenLink.Name = "m_OpenLink";
            this.m_OpenLink.Size = new System.Drawing.Size(213, 22);
            this.m_OpenLink.Text = "Open in External App";
            // 
            // m_OpenInOmniBrowser
            // 
            this.m_OpenInOmniBrowser.Name = "m_OpenInOmniBrowser";
            this.m_OpenInOmniBrowser.Size = new System.Drawing.Size(213, 22);
            this.m_OpenInOmniBrowser.Text = "Open in OmniStix Browser";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // m_NewLink
            // 
            this.m_NewLink.Name = "m_NewLink";
            this.m_NewLink.Size = new System.Drawing.Size(213, 22);
            this.m_NewLink.Text = "New Link";
            // 
            // m_Modify
            // 
            this.m_Modify.Name = "m_Modify";
            this.m_Modify.Size = new System.Drawing.Size(213, 22);
            this.m_Modify.Text = "Modify";
            // 
            // m_Delete
            // 
            this.m_Delete.Name = "m_Delete";
            this.m_Delete.Size = new System.Drawing.Size(213, 22);
            this.m_Delete.Text = "Delete";
            // 
            // cmsGroup
            // 
            this.cmsGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.g_AddLink,
            this.toolStripSeparator3,
            this.g_AddGroup,
            this.g_RenameGroup,
            this.g_DeleteGroup});
            this.cmsGroup.Name = "cmsGroup";
            this.cmsGroup.Size = new System.Drawing.Size(154, 98);
            // 
            // g_AddLink
            // 
            this.g_AddLink.Name = "g_AddLink";
            this.g_AddLink.Size = new System.Drawing.Size(153, 22);
            this.g_AddLink.Text = "Add Link";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(150, 6);
            // 
            // g_AddGroup
            // 
            this.g_AddGroup.Name = "g_AddGroup";
            this.g_AddGroup.Size = new System.Drawing.Size(153, 22);
            this.g_AddGroup.Text = "New Group";
            // 
            // g_RenameGroup
            // 
            this.g_RenameGroup.Name = "g_RenameGroup";
            this.g_RenameGroup.Size = new System.Drawing.Size(153, 22);
            this.g_RenameGroup.Text = "Rename Group";
            // 
            // g_DeleteGroup
            // 
            this.g_DeleteGroup.Name = "g_DeleteGroup";
            this.g_DeleteGroup.Size = new System.Drawing.Size(153, 22);
            this.g_DeleteGroup.Text = "Delete Group";
            // 
            // imageListDrag
            // 
            this.imageListDrag.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListDrag.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListDrag.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LinksDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 321);
            this.Controls.Add(this.panelModify);
            this.Controls.Add(this.splitContainer1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinksDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Links2Dlg";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelModify.ResumeLayout(false);
            this.panelModify.PerformLayout();
            this.grBoxDownload.ResumeLayout(false);
            this.grBoxDownload.PerformLayout();
            this.cmsLink.ResumeLayout(false);
            this.cmsGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblOpenIn;
        private System.Windows.Forms.RadioButton rbtnExternalApp;
        private System.Windows.Forms.RadioButton rbtnOmniBrowser;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelModify;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtLink2;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ContextMenuStrip cmsLink;
        private System.Windows.Forms.ToolStripMenuItem m_OpenLink;
        private System.Windows.Forms.ToolStripMenuItem m_OpenInOmniBrowser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_Modify;
        private System.Windows.Forms.ToolStripMenuItem m_Delete;
        private System.Windows.Forms.PictureBox pSize;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ContextMenuStrip cmsGroup;
        private System.Windows.Forms.ToolStripMenuItem g_RenameGroup;
        private System.Windows.Forms.ToolStripMenuItem g_DeleteGroup;
        private System.Windows.Forms.ToolStripMenuItem g_AddLink;
        private System.Windows.Forms.ToolStripMenuItem g_AddGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtEditNode;
        private System.Windows.Forms.ToolStripMenuItem m_NewLink;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.ImageList imageListDrag;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox cbDownload;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewImageColumn LinkImage;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Learned;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkType;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortByImage;
        private System.Windows.Forms.GroupBox grBoxDownload;
    }
}