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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddAudio = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnOpenMap = new System.Windows.Forms.Button();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AudioTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AudioPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MapPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopicGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimePoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Group,
            this.AudioTitle,
            this.MapTitle,
            this.aLength,
            this.aSize,
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
            // btnNewGroup
            // 
            this.btnNewGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewGroup.Location = new System.Drawing.Point(12, 260);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(85, 23);
            this.btnNewGroup.TabIndex = 2;
            this.btnNewGroup.Text = "New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlay.Location = new System.Drawing.Point(421, 260);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(85, 23);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(512, 260);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAddAudio
            // 
            this.btnAddAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddAudio.Location = new System.Drawing.Point(103, 260);
            this.btnAddAudio.Name = "btnAddAudio";
            this.btnAddAudio.Size = new System.Drawing.Size(85, 23);
            this.btnAddAudio.TabIndex = 5;
            this.btnAddAudio.Text = "Add from file";
            this.btnAddAudio.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.Location = new System.Drawing.Point(216, 260);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(85, 23);
            this.btnOpenFile.TabIndex = 6;
            this.btnOpenFile.Text = "Open in folder";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            // 
            // btnOpenMap
            // 
            this.btnOpenMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenMap.Location = new System.Drawing.Point(307, 260);
            this.btnOpenMap.Name = "btnOpenMap";
            this.btnOpenMap.Size = new System.Drawing.Size(85, 23);
            this.btnOpenMap.TabIndex = 7;
            this.btnOpenMap.Text = "Open in Map";
            this.btnOpenMap.UseVisualStyleBackColor = true;
            // 
            // Group
            // 
            this.Group.HeaderText = "Group";
            this.Group.Name = "Group";
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
            // ManageAudioDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(764, 290);
            this.Controls.Add(this.btnOpenMap);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnAddAudio);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnNewGroup);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddAudio;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnOpenMap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn AudioTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn MapTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn aLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn aSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn AudioPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MapPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn TopicGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimePoints;
    }
}