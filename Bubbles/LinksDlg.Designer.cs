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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtEditNode = new System.Windows.Forms.TextBox();
            this.pSize = new System.Windows.Forms.PictureBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SourceImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.SourceTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourcePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortByImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnOmniBrowser = new System.Windows.Forms.RadioButton();
            this.rbtnExternalApp = new System.Windows.Forms.RadioButton();
            this.lblOpenIn = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.panelModify = new System.Windows.Forms.Panel();
            this.btnGetTitle = new System.Windows.Forms.LinkLabel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLink2 = new System.Windows.Forms.TextBox();
            this.lblLink = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmsSource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_OpenLink = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OpenInOmniBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_AddToGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MoveToGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelModify.SuspendLayout();
            this.cmsSource.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.txtLink);
            this.splitContainer1.Size = new System.Drawing.Size(645, 325);
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
            this.pSize.Location = new System.Drawing.Point(65, 192);
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
            this.treeView1.Size = new System.Drawing.Size(159, 325);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.GroupsTree_NodeMouseClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.GroupsTree_DragDrop);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.GroupsTree_DragOver);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceImage,
            this.SourceTitle,
            this.LinkGroup,
            this.SourcePath,
            this.SourceType,
            this.GroupID,
            this.SortByImage});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(482, 282);
            this.dataGridView1.TabIndex = 21;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            this.dataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseMove);
            // 
            // SourceImage
            // 
            this.SourceImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SourceImage.FillWeight = 10.61452F;
            this.SourceImage.HeaderText = "";
            this.SourceImage.Name = "SourceImage";
            this.SourceImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // SourceTitle
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SourceTitle.DefaultCellStyle = dataGridViewCellStyle2;
            this.SourceTitle.FillWeight = 101.7739F;
            this.SourceTitle.HeaderText = "Title";
            this.SourceTitle.Name = "SourceTitle";
            // 
            // LinkGroup
            // 
            this.LinkGroup.HeaderText = "Group";
            this.LinkGroup.Name = "LinkGroup";
            // 
            // SourcePath
            // 
            this.SourcePath.HeaderText = "Path";
            this.SourcePath.Name = "SourcePath";
            this.SourcePath.Visible = false;
            // 
            // SourceType
            // 
            this.SourceType.HeaderText = "Type";
            this.SourceType.Name = "SourceType";
            this.SourceType.Visible = false;
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
            this.rbtnOmniBrowser.Location = new System.Drawing.Point(86, 3);
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
            this.rbtnExternalApp.Location = new System.Drawing.Point(211, 3);
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
            // txtLink
            // 
            this.txtLink.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLink.Location = new System.Drawing.Point(0, 305);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(482, 20);
            this.txtLink.TabIndex = 4;
            // 
            // panelModify
            // 
            this.panelModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelModify.BackColor = System.Drawing.Color.Linen;
            this.panelModify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModify.Controls.Add(this.btnGetTitle);
            this.panelModify.Controls.Add(this.btnOK);
            this.panelModify.Controls.Add(this.btnCancel);
            this.panelModify.Controls.Add(this.txtLink2);
            this.panelModify.Controls.Add(this.lblLink);
            this.panelModify.Controls.Add(this.txtTitle);
            this.panelModify.Controls.Add(this.lblTitle);
            this.panelModify.Location = new System.Drawing.Point(99, 82);
            this.panelModify.Name = "panelModify";
            this.panelModify.Size = new System.Drawing.Size(450, 136);
            this.panelModify.TabIndex = 22;
            this.panelModify.Visible = false;
            // 
            // btnGetTitle
            // 
            this.btnGetTitle.Location = new System.Drawing.Point(300, 38);
            this.btnGetTitle.Name = "btnGetTitle";
            this.btnGetTitle.Size = new System.Drawing.Size(137, 15);
            this.btnGetTitle.TabIndex = 17;
            this.btnGetTitle.TabStop = true;
            this.btnGetTitle.Text = "Получить название";
            this.btnGetTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnGetTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnGetTitle_LinkClicked);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(10, 98);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(359, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // cmsSource
            // 
            this.cmsSource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_OpenLink,
            this.m_OpenInOmniBrowser,
            this.toolStripSeparator1,
            this.m_AddToGroup,
            this.m_MoveToGroup,
            this.toolStripSeparator2,
            this.m_Modify,
            this.m_Delete});
            this.cmsSource.Name = "contextMenu";
            this.cmsSource.Size = new System.Drawing.Size(214, 148);
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
            // m_AddToGroup
            // 
            this.m_AddToGroup.Name = "m_AddToGroup";
            this.m_AddToGroup.Size = new System.Drawing.Size(213, 22);
            this.m_AddToGroup.Text = "Add to Group";
            // 
            // m_MoveToGroup
            // 
            this.m_MoveToGroup.Name = "m_MoveToGroup";
            this.m_MoveToGroup.Size = new System.Drawing.Size(213, 22);
            this.m_MoveToGroup.Text = "Move to Group";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(210, 6);
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
            // LinksDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 325);
            this.Controls.Add(this.panelModify);
            this.Controls.Add(this.splitContainer1);
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
            this.cmsSource.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtLink2;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ContextMenuStrip cmsSource;
        private System.Windows.Forms.ToolStripMenuItem m_OpenLink;
        private System.Windows.Forms.ToolStripMenuItem m_OpenInOmniBrowser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_AddToGroup;
        private System.Windows.Forms.ToolStripMenuItem m_MoveToGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
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
        private System.Windows.Forms.DataGridViewImageColumn SourceImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourcePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortByImage;
        private System.Windows.Forms.TextBox txtEditNode;
        private System.Windows.Forms.LinkLabel btnGetTitle;
    }
}