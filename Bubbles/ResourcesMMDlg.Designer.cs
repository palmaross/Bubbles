namespace Bubbles
{
    partial class ResourcesMMDlg
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Ресурсы   ", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Теги   ", 1, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourcesMMDlg));
            this.AddNewResourcesGroup = new System.Windows.Forms.LinkLabel();
            this.AddNewTagGroup = new System.Windows.Forms.LinkLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuNewResourceGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewTagGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewResource = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewTag = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddToMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMutuallyExclusive = new System.Windows.Forms.ToolStripMenuItem();
            this.menuResourceCard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuColor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddToTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddAllToMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.imageListDrag = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.imageSize = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.checkedListBoxItemHeight = new System.Windows.Forms.PictureBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.CheckMap = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxItemHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // AddNewResourcesGroup
            // 
            this.AddNewResourcesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.AddNewResourcesGroup.AutoSize = true;
            this.AddNewResourcesGroup.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.AddNewResourcesGroup.LinkColor = System.Drawing.Color.MediumBlue;
            this.AddNewResourcesGroup.Location = new System.Drawing.Point(12, 6);
            this.AddNewResourcesGroup.Name = "AddNewResourcesGroup";
            this.AddNewResourcesGroup.Size = new System.Drawing.Size(206, 13);
            this.AddNewResourcesGroup.TabIndex = 0;
            this.AddNewResourcesGroup.TabStop = true;
            this.AddNewResourcesGroup.Text = "Добавить новую группу Ресурсов";
            this.AddNewResourcesGroup.VisitedLinkColor = System.Drawing.Color.MediumBlue;
            this.AddNewResourcesGroup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddNewResourcesGroup_LinkClicked);
            // 
            // AddNewTagGroup
            // 
            this.AddNewTagGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.AddNewTagGroup.AutoSize = true;
            this.AddNewTagGroup.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.AddNewTagGroup.LinkColor = System.Drawing.Color.MediumBlue;
            this.AddNewTagGroup.Location = new System.Drawing.Point(12, 23);
            this.AddNewTagGroup.Name = "AddNewTagGroup";
            this.AddNewTagGroup.Size = new System.Drawing.Size(185, 13);
            this.AddNewTagGroup.TabIndex = 1;
            this.AddNewTagGroup.TabStop = true;
            this.AddNewTagGroup.Text = "Добавить новую группу Тегов";
            this.AddNewTagGroup.VisitedLinkColor = System.Drawing.Color.Blue;
            this.AddNewTagGroup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddNewTagGroup_LinkClicked);
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.SystemColors.Window;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeView1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.Indent = 14;
            this.treeView1.ItemHeight = 21;
            this.treeView1.Location = new System.Drawing.Point(1, 100);
            this.treeView1.Name = "treeView1";
            treeNode1.BackColor = System.Drawing.SystemColors.ControlLight;
            treeNode1.ForeColor = System.Drawing.Color.RoyalBlue;
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "NodeResources";
            treeNode1.NodeFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "Ресурсы   ";
            treeNode2.BackColor = System.Drawing.SystemColors.ControlLight;
            treeNode2.ForeColor = System.Drawing.Color.RoyalBlue;
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "NodeTags";
            treeNode2.NodeFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Tag = "";
            treeNode2.Text = "Теги   ";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeView1.ShowLines = false;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(327, 261);
            this.treeView1.TabIndex = 4;
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView1_DrawNode);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_DragOver);
            this.treeView1.DragLeave += new System.EventHandler(this.treeView1_DragLeave);
            this.treeView1.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.treeView1_GiveFeedback);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewResourceGroup,
            this.menuNewTagGroup,
            this.menuShowAll,
            this.menuCollapseAll,
            this.menuNewResource,
            this.menuNewTag,
            this.menuRename,
            this.menuDeleteGroup,
            this.menuDelete,
            this.menuAddToMap,
            this.menuMutuallyExclusive,
            this.menuResourceCard,
            this.menuColor,
            this.menuAddToTopic,
            this.menuImport,
            this.menuAddAllToMap,
            this.menuSeparator,
            this.menuCopy,
            this.menuCut,
            this.menuPaste});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 444);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // menuNewResourceGroup
            // 
            this.menuNewResourceGroup.Name = "menuNewResourceGroup";
            this.menuNewResourceGroup.Size = new System.Drawing.Size(203, 22);
            this.menuNewResourceGroup.Text = "Новая группа ресурсов";
            // 
            // menuNewTagGroup
            // 
            this.menuNewTagGroup.Name = "menuNewTagGroup";
            this.menuNewTagGroup.Size = new System.Drawing.Size(203, 22);
            this.menuNewTagGroup.Text = "Новая группа тегов";
            // 
            // menuShowAll
            // 
            this.menuShowAll.Name = "menuShowAll";
            this.menuShowAll.Size = new System.Drawing.Size(203, 22);
            this.menuShowAll.Text = "Развернуть все";
            // 
            // menuCollapseAll
            // 
            this.menuCollapseAll.Name = "menuCollapseAll";
            this.menuCollapseAll.Size = new System.Drawing.Size(203, 22);
            this.menuCollapseAll.Text = "Свернуть все";
            // 
            // menuNewResource
            // 
            this.menuNewResource.Name = "menuNewResource";
            this.menuNewResource.Size = new System.Drawing.Size(203, 22);
            this.menuNewResource.Text = "Новый ресурс";
            // 
            // menuNewTag
            // 
            this.menuNewTag.Name = "menuNewTag";
            this.menuNewTag.Size = new System.Drawing.Size(203, 22);
            this.menuNewTag.Text = "Новый тег";
            // 
            // menuRename
            // 
            this.menuRename.Name = "menuRename";
            this.menuRename.Size = new System.Drawing.Size(203, 22);
            this.menuRename.Text = "Переименовать";
            // 
            // menuDeleteGroup
            // 
            this.menuDeleteGroup.Name = "menuDeleteGroup";
            this.menuDeleteGroup.Size = new System.Drawing.Size(203, 22);
            this.menuDeleteGroup.Text = "Удалить группу";
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(203, 22);
            this.menuDelete.Text = "Удалить";
            // 
            // menuAddToMap
            // 
            this.menuAddToMap.Name = "menuAddToMap";
            this.menuAddToMap.Size = new System.Drawing.Size(203, 22);
            this.menuAddToMap.Text = "Добавить в карту";
            // 
            // menuMutuallyExclusive
            // 
            this.menuMutuallyExclusive.Name = "menuMutuallyExclusive";
            this.menuMutuallyExclusive.Size = new System.Drawing.Size(203, 22);
            this.menuMutuallyExclusive.Text = "Взаимоисключающие";
            // 
            // menuResourceCard
            // 
            this.menuResourceCard.Name = "menuResourceCard";
            this.menuResourceCard.Size = new System.Drawing.Size(203, 22);
            this.menuResourceCard.Text = "Карточка ресурса";
            // 
            // menuColor
            // 
            this.menuColor.Name = "menuColor";
            this.menuColor.Size = new System.Drawing.Size(203, 22);
            this.menuColor.Text = "Цвет...";
            // 
            // menuAddToTopic
            // 
            this.menuAddToTopic.Name = "menuAddToTopic";
            this.menuAddToTopic.Size = new System.Drawing.Size(203, 22);
            this.menuAddToTopic.Text = "Добавить в тему";
            // 
            // menuImport
            // 
            this.menuImport.Name = "menuImport";
            this.menuImport.Size = new System.Drawing.Size(203, 22);
            this.menuImport.Text = "Мастер импорта...";
            // 
            // menuAddAllToMap
            // 
            this.menuAddAllToMap.Name = "menuAddAllToMap";
            this.menuAddAllToMap.Size = new System.Drawing.Size(203, 22);
            this.menuAddAllToMap.Text = "Добавить все в карту";
            // 
            // menuSeparator
            // 
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(203, 22);
            this.menuSeparator.Text = "-----------------";
            // 
            // menuCopy
            // 
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(203, 22);
            this.menuCopy.Text = "Копировать";
            // 
            // menuCut
            // 
            this.menuCut.Name = "menuCut";
            this.menuCut.Size = new System.Drawing.Size(203, 22);
            this.menuCut.Text = "Вырезать";
            // 
            // menuPaste
            // 
            this.menuPaste.Name = "menuPaste";
            this.menuPaste.Size = new System.Drawing.Size(203, 22);
            this.menuPaste.Text = "Вставить";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // imageListDrag
            // 
            this.imageListDrag.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListDrag.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListDrag.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(226, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(49, 21);
            this.textBox1.TabIndex = 6;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // imageSize
            // 
            this.imageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageSize.Image = ((System.Drawing.Image)(resources.GetObject("imageSize.Image")));
            this.imageSize.Location = new System.Drawing.Point(290, 7);
            this.imageSize.Name = "imageSize";
            this.imageSize.Size = new System.Drawing.Size(16, 16);
            this.imageSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageSize.TabIndex = 7;
            this.imageSize.TabStop = false;
            this.imageSize.Click += new System.EventHandler(this.imageSize_Click);
            // 
            // checkedListBoxItemHeight
            // 
            this.checkedListBoxItemHeight.Location = new System.Drawing.Point(156, 171);
            this.checkedListBoxItemHeight.Name = "checkedListBoxItemHeight";
            this.checkedListBoxItemHeight.Size = new System.Drawing.Size(14, 14);
            this.checkedListBoxItemHeight.TabIndex = 9;
            this.checkedListBoxItemHeight.TabStop = false;
            this.checkedListBoxItemHeight.Visible = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(3, 72);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(46, 13);
            this.lblSearch.TabIndex = 10;
            this.lblSearch.Text = "Найти:";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(54, 69);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(143, 21);
            this.txtSearch.TabIndex = 11;
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrev.Location = new System.Drawing.Point(203, 68);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(25, 20);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNext.Location = new System.Drawing.Point(234, 68);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(25, 20);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // CheckMap
            // 
            this.CheckMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckMap.AutoSize = true;
            this.CheckMap.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.CheckMap.LinkColor = System.Drawing.Color.MediumBlue;
            this.CheckMap.Location = new System.Drawing.Point(12, 58);
            this.CheckMap.Name = "CheckMap";
            this.CheckMap.Size = new System.Drawing.Size(93, 13);
            this.CheckMap.TabIndex = 14;
            this.CheckMap.TabStop = true;
            this.CheckMap.Text = "Сверить карту";
            this.CheckMap.Visible = false;
            this.CheckMap.VisitedLinkColor = System.Drawing.Color.Blue;
            this.CheckMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CheckMap_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ResourcesMMDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(329, 358);
            this.ControlBox = false;
            this.Controls.Add(this.CheckMap);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.checkedListBoxItemHeight);
            this.Controls.Add(this.imageSize);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.AddNewTagGroup);
            this.Controls.Add(this.AddNewResourcesGroup);
            this.Controls.Add(this.treeView1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResourcesMMDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ResourcesTags1";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxItemHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuNewResourceGroup;
        private System.Windows.Forms.ToolStripMenuItem menuRename;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem menuImport;
        private System.Windows.Forms.ToolStripMenuItem menuAddToMap;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ImageList imageListDrag;

        private System.Windows.Forms.TreeNode m_dragNode;
        private System.Windows.Forms.TreeNode m_tempDropNode;
        private System.Windows.Forms.TreeNode m_editNode;
        private LabelEditType m_editType = LabelEditType.EDIT_DISABLED;
        private System.Windows.Forms.ToolStripMenuItem menuNewTagGroup;
        private System.Windows.Forms.ToolStripMenuItem menuNewResource;
        private System.Windows.Forms.ToolStripMenuItem menuNewTag;
        private System.Windows.Forms.ToolStripMenuItem menuMutuallyExclusive;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private bool m_bGotKeydown = false;
        private bool m_bStateChanged = false;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem menuColor;
        private System.Windows.Forms.ToolStripMenuItem menuAddToTopic;
        private System.Windows.Forms.ToolStripMenuItem menuShowAll;
        private System.Windows.Forms.ToolStripMenuItem menuCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem menuResourceCard;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.PictureBox imageSize;
        public System.Windows.Forms.PictureBox checkedListBoxItemHeight;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteGroup;
        public System.Windows.Forms.LinkLabel AddNewTagGroup;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.LinkLabel AddNewResourcesGroup;
        private System.Windows.Forms.ToolStripMenuItem menuAddAllToMap;
        public System.Windows.Forms.LinkLabel CheckMap;
        private System.Windows.Forms.ToolStripMenuItem menuSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuCut;
        private System.Windows.Forms.ToolStripMenuItem menuPaste;
        private System.Windows.Forms.Timer timer1;
    }
}