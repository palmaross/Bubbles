namespace Bubbles
{
    partial class ManageAudioDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageAudioDlg));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AudioTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AudioPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopicGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimePoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnAddAudio = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_Play = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_OpenInFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OpenInMap = new System.Windows.Forms.ToolStripMenuItem();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.panelManageGroups = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.btnRenameGroup = new System.Windows.Forms.PictureBox();
            this.btnDeleteGroup = new System.Windows.Forms.PictureBox();
            this.btnNewGroup = new System.Windows.Forms.PictureBox();
            this.cbGroupsManage = new System.Windows.Forms.ComboBox();
            this.btnCloseManage = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.panelAddAudio = new System.Windows.Forms.Panel();
            this.lblAudioTitle = new System.Windows.Forms.Label();
            this.txtAudioTitle = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblPathToAudio = new System.Windows.Forms.Label();
            this.cbGroups = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblAddToGroup = new System.Windows.Forms.Label();
            this.btnManageGroups = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.panelManageGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRenameGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNewGroup)).BeginInit();
            this.panelAddAudio.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Group,
            this.AudioTitle,
            this.MapTitle,
            this.aLength,
            this.aSize,
            this.ID,
            this.AudioPath,
            this.GroupID,
            this.MapPath,
            this.TopicGuid,
            this.TimePoints});
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(740, 242);
            this.dgv.TabIndex = 0;
            this.dgv.DoubleClick += new System.EventHandler(this.dgv_DoubleClick);
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_KeyDown);
            this.dgv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDown);
            // 
            // Group
            // 
            this.Group.HeaderText = "Group";
            this.Group.Name = "Group";
            this.Group.ReadOnly = true;
            this.Group.Width = 200;
            // 
            // AudioTitle
            // 
            this.AudioTitle.HeaderText = "Audo Title";
            this.AudioTitle.Name = "AudioTitle";
            this.AudioTitle.Width = 200;
            // 
            // MapTitle
            // 
            this.MapTitle.HeaderText = "Map";
            this.MapTitle.Name = "MapTitle";
            this.MapTitle.Width = 200;
            // 
            // aLength
            // 
            this.aLength.HeaderText = "Length";
            this.aLength.Name = "aLength";
            this.aLength.ReadOnly = true;
            this.aLength.Width = 60;
            // 
            // aSize
            // 
            this.aSize.HeaderText = "Size (KB)";
            this.aSize.Name = "aSize";
            this.aSize.ReadOnly = true;
            this.aSize.Width = 60;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // AudioPath
            // 
            this.AudioPath.HeaderText = "AudioPath";
            this.AudioPath.Name = "AudioPath";
            this.AudioPath.Visible = false;
            // 
            // GroupID
            // 
            this.GroupID.HeaderText = "GroupID";
            this.GroupID.Name = "GroupID";
            this.GroupID.Visible = false;
            // 
            // MapPath
            // 
            this.MapPath.HeaderText = "MapPath";
            this.MapPath.Name = "MapPath";
            this.MapPath.Visible = false;
            // 
            // TopicGuid
            // 
            this.TopicGuid.HeaderText = "TopicGuid";
            this.TopicGuid.Name = "TopicGuid";
            this.TopicGuid.Visible = false;
            // 
            // TimePoints
            // 
            this.TimePoints.HeaderText = "TimePoints";
            this.TimePoints.Name = "TimePoints";
            this.TimePoints.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(677, 260);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlay.AutoSize = true;
            this.btnPlay.Location = new System.Drawing.Point(317, 260);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(78, 23);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnAddAudio
            // 
            this.btnAddAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddAudio.Location = new System.Drawing.Point(147, 260);
            this.btnAddAudio.Name = "btnAddAudio";
            this.btnAddAudio.Size = new System.Drawing.Size(129, 23);
            this.btnAddAudio.TabIndex = 5;
            this.btnAddAudio.Text = "Добавить аудио";
            this.btnAddAudio.UseVisualStyleBackColor = true;
            this.btnAddAudio.Click += new System.EventHandler(this.btnAddAudio_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_Play,
            this.m_Rename,
            this.m_Delete,
            this.toolStripSeparator1,
            this.m_OpenInFolder,
            this.m_OpenInMap});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(222, 120);
            // 
            // m_Play
            // 
            this.m_Play.Name = "m_Play";
            this.m_Play.Size = new System.Drawing.Size(221, 22);
            this.m_Play.Text = "Play (double-click)";
            // 
            // m_Rename
            // 
            this.m_Rename.Name = "m_Rename";
            this.m_Rename.Size = new System.Drawing.Size(221, 22);
            this.m_Rename.Text = "Rename (slow double-click)";
            // 
            // m_Delete
            // 
            this.m_Delete.Name = "m_Delete";
            this.m_Delete.Size = new System.Drawing.Size(221, 22);
            this.m_Delete.Text = "Delete (Delete key)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(218, 6);
            // 
            // m_OpenInFolder
            // 
            this.m_OpenInFolder.Name = "m_OpenInFolder";
            this.m_OpenInFolder.Size = new System.Drawing.Size(221, 22);
            this.m_OpenInFolder.Text = "Open in Folder";
            // 
            // m_OpenInMap
            // 
            this.m_OpenInMap.Name = "m_OpenInMap";
            this.m_OpenInMap.Size = new System.Drawing.Size(221, 22);
            this.m_OpenInMap.Text = "Open in Map";
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(413, 262);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.TabIndex = 6;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // panelManageGroups
            // 
            this.panelManageGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelManageGroups.BackColor = System.Drawing.Color.PapayaWhip;
            this.panelManageGroups.Controls.Add(this.btnSave);
            this.panelManageGroups.Controls.Add(this.lblGroupName);
            this.panelManageGroups.Controls.Add(this.btnRenameGroup);
            this.panelManageGroups.Controls.Add(this.btnDeleteGroup);
            this.panelManageGroups.Controls.Add(this.btnNewGroup);
            this.panelManageGroups.Controls.Add(this.cbGroupsManage);
            this.panelManageGroups.Controls.Add(this.btnCloseManage);
            this.panelManageGroups.Controls.Add(this.txtGroupName);
            this.panelManageGroups.Location = new System.Drawing.Point(14, 143);
            this.panelManageGroups.Name = "panelManageGroups";
            this.panelManageGroups.Size = new System.Drawing.Size(299, 109);
            this.panelManageGroups.TabIndex = 96;
            this.panelManageGroups.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 77);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 60;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(7, 49);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(70, 13);
            this.lblGroupName.TabIndex = 7;
            this.lblGroupName.Text = "Group Name:";
            this.lblGroupName.Visible = false;
            // 
            // btnRenameGroup
            // 
            this.btnRenameGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRenameGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnRenameGroup.Image")));
            this.btnRenameGroup.Location = new System.Drawing.Point(245, 14);
            this.btnRenameGroup.Name = "btnRenameGroup";
            this.btnRenameGroup.Size = new System.Drawing.Size(20, 20);
            this.btnRenameGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRenameGroup.TabIndex = 59;
            this.btnRenameGroup.TabStop = false;
            this.btnRenameGroup.Click += new System.EventHandler(this.btnRenameGroup_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.Image")));
            this.btnDeleteGroup.Location = new System.Drawing.Point(270, 14);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(20, 20);
            this.btnDeleteGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDeleteGroup.TabIndex = 58;
            this.btnDeleteGroup.TabStop = false;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGroup.Image")));
            this.btnNewGroup.Location = new System.Drawing.Point(220, 14);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(20, 20);
            this.btnNewGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnNewGroup.TabIndex = 57;
            this.btnNewGroup.TabStop = false;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // cbGroupsManage
            // 
            this.cbGroupsManage.FormattingEnabled = true;
            this.cbGroupsManage.Location = new System.Drawing.Point(8, 14);
            this.cbGroupsManage.Name = "cbGroupsManage";
            this.cbGroupsManage.Size = new System.Drawing.Size(206, 21);
            this.cbGroupsManage.Sorted = true;
            this.cbGroupsManage.TabIndex = 5;
            // 
            // btnCloseManage
            // 
            this.btnCloseManage.Location = new System.Drawing.Point(215, 77);
            this.btnCloseManage.Name = "btnCloseManage";
            this.btnCloseManage.Size = new System.Drawing.Size(75, 23);
            this.btnCloseManage.TabIndex = 3;
            this.btnCloseManage.Text = "Close";
            this.btnCloseManage.UseVisualStyleBackColor = true;
            this.btnCloseManage.Click += new System.EventHandler(this.btnCancelManage_Click);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(86, 46);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(204, 20);
            this.txtGroupName.TabIndex = 1;
            this.txtGroupName.Visible = false;
            // 
            // panelAddAudio
            // 
            this.panelAddAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelAddAudio.BackColor = System.Drawing.Color.PapayaWhip;
            this.panelAddAudio.Controls.Add(this.lblAudioTitle);
            this.panelAddAudio.Controls.Add(this.txtAudioTitle);
            this.panelAddAudio.Controls.Add(this.btnBrowse);
            this.panelAddAudio.Controls.Add(this.lblPathToAudio);
            this.panelAddAudio.Controls.Add(this.cbGroups);
            this.panelAddAudio.Controls.Add(this.btnCancel);
            this.panelAddAudio.Controls.Add(this.btnAddFile);
            this.panelAddAudio.Controls.Add(this.txtPath);
            this.panelAddAudio.Controls.Add(this.lblAddToGroup);
            this.panelAddAudio.Location = new System.Drawing.Point(334, 118);
            this.panelAddAudio.Name = "panelAddAudio";
            this.panelAddAudio.Size = new System.Drawing.Size(341, 134);
            this.panelAddAudio.TabIndex = 97;
            this.panelAddAudio.Visible = false;
            // 
            // lblAudioTitle
            // 
            this.lblAudioTitle.AutoSize = true;
            this.lblAudioTitle.Location = new System.Drawing.Point(10, 77);
            this.lblAudioTitle.Name = "lblAudioTitle";
            this.lblAudioTitle.Size = new System.Drawing.Size(60, 13);
            this.lblAudioTitle.TabIndex = 8;
            this.lblAudioTitle.Text = "Audio Title:";
            // 
            // txtAudioTitle
            // 
            this.txtAudioTitle.Location = new System.Drawing.Point(90, 74);
            this.txtAudioTitle.Name = "txtAudioTitle";
            this.txtAudioTitle.Size = new System.Drawing.Size(206, 20);
            this.txtAudioTitle.TabIndex = 7;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(300, 40);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblPathToAudio
            // 
            this.lblPathToAudio.AutoSize = true;
            this.lblPathToAudio.Location = new System.Drawing.Point(10, 45);
            this.lblPathToAudio.Name = "lblPathToAudio";
            this.lblPathToAudio.Size = new System.Drawing.Size(73, 13);
            this.lblPathToAudio.TabIndex = 5;
            this.lblPathToAudio.Text = "Path to audio:";
            // 
            // cbGroups
            // 
            this.cbGroups.FormattingEnabled = true;
            this.cbGroups.Location = new System.Drawing.Point(90, 11);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Size = new System.Drawing.Size(206, 21);
            this.cbGroups.Sorted = true;
            this.cbGroups.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(256, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(13, 102);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 2;
            this.btnAddFile.Text = "Add";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(90, 42);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(206, 20);
            this.txtPath.TabIndex = 1;
            // 
            // lblAddToGroup
            // 
            this.lblAddToGroup.AutoSize = true;
            this.lblAddToGroup.Location = new System.Drawing.Point(10, 15);
            this.lblAddToGroup.Name = "lblAddToGroup";
            this.lblAddToGroup.Size = new System.Drawing.Size(73, 13);
            this.lblAddToGroup.TabIndex = 0;
            this.lblAddToGroup.Text = "Add to Group:";
            // 
            // btnManageGroups
            // 
            this.btnManageGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnManageGroups.Location = new System.Drawing.Point(12, 260);
            this.btnManageGroups.Name = "btnManageGroups";
            this.btnManageGroups.Size = new System.Drawing.Size(129, 23);
            this.btnManageGroups.TabIndex = 2;
            this.btnManageGroups.Text = "Управление группами";
            this.btnManageGroups.UseVisualStyleBackColor = true;
            this.btnManageGroups.Click += new System.EventHandler(this.btnManageGroups_Click);
            // 
            // ManageAudioDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(764, 290);
            this.Controls.Add(this.panelAddAudio);
            this.Controls.Add(this.panelManageGroups);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.btnAddAudio);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnManageGroups);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgv);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageAudioDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Audio";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.panelManageGroups.ResumeLayout(false);
            this.panelManageGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRenameGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNewGroup)).EndInit();
            this.panelAddAudio.ResumeLayout(false);
            this.panelAddAudio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnAddAudio;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem m_Rename;
        private System.Windows.Forms.ToolStripMenuItem m_Delete;
        private System.Windows.Forms.ToolStripMenuItem m_OpenInFolder;
        private System.Windows.Forms.ToolStripMenuItem m_OpenInMap;
        private System.Windows.Forms.ToolStripMenuItem m_Play;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.Panel panelManageGroups;
        private System.Windows.Forms.Button btnCloseManage;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Panel panelAddAudio;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblPathToAudio;
        private System.Windows.Forms.ComboBox cbGroups;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblAddToGroup;
        private System.Windows.Forms.ComboBox cbGroupsManage;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.PictureBox btnRenameGroup;
        private System.Windows.Forms.PictureBox btnDeleteGroup;
        private System.Windows.Forms.PictureBox btnNewGroup;
        private System.Windows.Forms.Button btnManageGroups;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn AudioTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn MapTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn aLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn aSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AudioPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MapPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn TopicGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimePoints;
        private System.Windows.Forms.Label lblAudioTitle;
        private System.Windows.Forms.TextBox txtAudioTitle;
        public System.Windows.Forms.DataGridView dgv;
    }
}