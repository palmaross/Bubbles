namespace Organizer
{
    partial class NoteDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteDlg));
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.txtNoteTitle = new System.Windows.Forms.TextBox();
            this.cbTags = new System.Windows.Forms.ComboBox();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.pLink = new System.Windows.Forms.PictureBox();
            this.pTag = new System.Windows.Forms.PictureBox();
            this.panelLink = new System.Windows.Forms.Panel();
            this.cbGroups = new System.Windows.Forms.ComboBox();
            this.panelTags = new System.Windows.Forms.Panel();
            this.Tags = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblGroup = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTag)).BeginInit();
            this.panelLink.SuspendLayout();
            this.panelTags.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(0, 20);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(466, 206);
            this.txtContent.TabIndex = 0;
            this.txtContent.Text = "";
            this.txtContent.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtContent_LinkClicked);
            this.txtContent.Enter += new System.EventHandler(this.txtNote_Enter);
            this.txtContent.Leave += new System.EventHandler(this.txtNote_Leave);
            // 
            // txtNoteTitle
            // 
            this.txtNoteTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoteTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNoteTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtNoteTitle.Location = new System.Drawing.Point(0, 0);
            this.txtNoteTitle.Name = "txtNoteTitle";
            this.txtNoteTitle.Size = new System.Drawing.Size(466, 20);
            this.txtNoteTitle.TabIndex = 2;
            this.txtNoteTitle.Enter += new System.EventHandler(this.txtNote_Enter);
            this.txtNoteTitle.Leave += new System.EventHandler(this.txtNote_Leave);
            // 
            // cbTags
            // 
            this.cbTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbTags.FormattingEnabled = true;
            this.cbTags.Location = new System.Drawing.Point(20, 0);
            this.cbTags.Name = "cbTags";
            this.cbTags.Size = new System.Drawing.Size(108, 21);
            this.cbTags.Sorted = true;
            this.cbTags.TabIndex = 4;
            this.cbTags.SelectedIndexChanged += new System.EventHandler(this.cbTags_SelectedIndexChanged);
            // 
            // txtLink
            // 
            this.txtLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLink.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtLink.Location = new System.Drawing.Point(20, 0);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(445, 20);
            this.txtLink.TabIndex = 9;
            this.txtLink.DoubleClick += new System.EventHandler(this.txtLink_DoubleClick);
            this.txtLink.Enter += new System.EventHandler(this.txtNote_Enter);
            this.txtLink.Leave += new System.EventHandler(this.txtNote_Leave);
            // 
            // pLink
            // 
            this.pLink.BackColor = System.Drawing.SystemColors.Window;
            this.pLink.Image = ((System.Drawing.Image)(resources.GetObject("pLink.Image")));
            this.pLink.Location = new System.Drawing.Point(3, 2);
            this.pLink.Name = "pLink";
            this.pLink.Size = new System.Drawing.Size(14, 14);
            this.pLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pLink.TabIndex = 11;
            this.pLink.TabStop = false;
            // 
            // pTag
            // 
            this.pTag.BackColor = System.Drawing.SystemColors.Window;
            this.pTag.Image = ((System.Drawing.Image)(resources.GetObject("pTag.Image")));
            this.pTag.Location = new System.Drawing.Point(3, 2);
            this.pTag.Name = "pTag";
            this.pTag.Size = new System.Drawing.Size(14, 14);
            this.pTag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pTag.TabIndex = 12;
            this.pTag.TabStop = false;
            // 
            // panelLink
            // 
            this.panelLink.BackColor = System.Drawing.SystemColors.Window;
            this.panelLink.Controls.Add(this.pLink);
            this.panelLink.Controls.Add(this.txtLink);
            this.panelLink.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLink.Location = new System.Drawing.Point(0, 244);
            this.panelLink.Name = "panelLink";
            this.panelLink.Size = new System.Drawing.Size(466, 18);
            this.panelLink.TabIndex = 14;
            // 
            // cbGroups
            // 
            this.cbGroups.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.cbGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGroups.ForeColor = System.Drawing.SystemColors.Window;
            this.cbGroups.FormattingEnabled = true;
            this.cbGroups.Location = new System.Drawing.Point(50, 7);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Size = new System.Drawing.Size(190, 21);
            this.cbGroups.TabIndex = 17;
            this.cbGroups.SelectedIndexChanged += new System.EventHandler(this.cbGroups_SelectedIndexChanged);
            // 
            // panelTags
            // 
            this.panelTags.BackColor = System.Drawing.SystemColors.Window;
            this.panelTags.Controls.Add(this.Tags);
            this.panelTags.Controls.Add(this.pTag);
            this.panelTags.Controls.Add(this.cbTags);
            this.panelTags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTags.Location = new System.Drawing.Point(0, 226);
            this.panelTags.Name = "panelTags";
            this.panelTags.Size = new System.Drawing.Size(466, 18);
            this.panelTags.TabIndex = 15;
            // 
            // Tags
            // 
            this.Tags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Tags.Location = new System.Drawing.Point(126, 0);
            this.Tags.Name = "Tags";
            this.Tags.Size = new System.Drawing.Size(339, 20);
            this.Tags.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.cbGroups);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.lblGroup);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 262);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 30);
            this.panel1.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Location = new System.Drawing.Point(385, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGroup.AutoSize = true;
            this.lblGroup.ForeColor = System.Drawing.SystemColors.Window;
            this.lblGroup.Location = new System.Drawing.Point(5, 8);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(39, 13);
            this.lblGroup.TabIndex = 2;
            this.lblGroup.Text = "Group:";
            this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(304, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 21);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // NoteDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(466, 292);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtNoteTitle);
            this.Controls.Add(this.panelTags);
            this.Controls.Add(this.panelLink);
            this.Controls.Add(this.panel1);
            this.Name = "NoteDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NoteDlg_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTag)).EndInit();
            this.panelLink.ResumeLayout(false);
            this.panelLink.PerformLayout();
            this.panelTags.ResumeLayout(false);
            this.panelTags.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbTags;
        private System.Windows.Forms.PictureBox pLink;
        private System.Windows.Forms.PictureBox pTag;
        private System.Windows.Forms.Panel panelLink;
        private System.Windows.Forms.Panel panelTags;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbGroups;
        public System.Windows.Forms.RichTextBox txtContent;
        public System.Windows.Forms.TextBox txtNoteTitle;
        public System.Windows.Forms.TextBox txtLink;
        public System.Windows.Forms.TextBox Tags;
        public System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}