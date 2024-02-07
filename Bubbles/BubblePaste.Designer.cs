namespace Bubbles
{
    partial class BubblePaste
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubblePaste));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.PasteLink = new System.Windows.Forms.PictureBox();
            this.PasteNotes = new System.Windows.Forms.PictureBox();
            this.pCopy = new System.Windows.Forms.PictureBox();
            this.UnformatText = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.cmsPasteText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CP_copy_unformatted = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_copy_formatted = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_paste_unformatted = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_paste_formatted = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCommon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiSize = new System.Windows.Forms.PictureBox();
            this.subtopic = new System.Windows.Forms.PictureBox();
            this.pPaste = new System.Windows.Forms.PictureBox();
            this.ToggleTextFormat = new System.Windows.Forms.PictureBox();
            this.pReplace = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnformatText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            this.cmsPasteText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPaste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToggleTextFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pReplace)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // PasteLink
            // 
            this.PasteLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasteLink.Image = ((System.Drawing.Image)(resources.GetObject("PasteLink.Image")));
            this.PasteLink.Location = new System.Drawing.Point(52, 5);
            this.PasteLink.Name = "PasteLink";
            this.PasteLink.Size = new System.Drawing.Size(20, 20);
            this.PasteLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PasteLink.TabIndex = 75;
            this.PasteLink.TabStop = false;
            this.PasteLink.Tag = "1";
            this.PasteLink.Click += new System.EventHandler(this.PasteLink_Click);
            // 
            // PasteNotes
            // 
            this.PasteNotes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasteNotes.Image = ((System.Drawing.Image)(resources.GetObject("PasteNotes.Image")));
            this.PasteNotes.Location = new System.Drawing.Point(77, 5);
            this.PasteNotes.Name = "PasteNotes";
            this.PasteNotes.Size = new System.Drawing.Size(20, 20);
            this.PasteNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PasteNotes.TabIndex = 76;
            this.PasteNotes.TabStop = false;
            this.PasteNotes.Tag = "1";
            this.PasteNotes.Click += new System.EventHandler(this.PasteNotes_Click);
            // 
            // pCopy
            // 
            this.pCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pCopy.Image = ((System.Drawing.Image)(resources.GetObject("pCopy.Image")));
            this.pCopy.Location = new System.Drawing.Point(107, 3);
            this.pCopy.Name = "pCopy";
            this.pCopy.Size = new System.Drawing.Size(24, 24);
            this.pCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCopy.TabIndex = 81;
            this.pCopy.TabStop = false;
            this.pCopy.Tag = "1";
            this.pCopy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pCopy_MouseClick);
            // 
            // UnformatText
            // 
            this.UnformatText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UnformatText.Image = ((System.Drawing.Image)(resources.GetObject("UnformatText.Image")));
            this.UnformatText.Location = new System.Drawing.Point(200, 7);
            this.UnformatText.Name = "UnformatText";
            this.UnformatText.Size = new System.Drawing.Size(16, 16);
            this.UnformatText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UnformatText.TabIndex = 85;
            this.UnformatText.TabStop = false;
            this.UnformatText.Tag = "1";
            this.UnformatText.Click += new System.EventHandler(this.UnformatText_Click);
            // 
            // pictureHandle
            // 
            this.pictureHandle.BackColor = System.Drawing.Color.Transparent;
            this.pictureHandle.Image = ((System.Drawing.Image)(resources.GetObject("pictureHandle.Image")));
            this.pictureHandle.Location = new System.Drawing.Point(0, 0);
            this.pictureHandle.Name = "pictureHandle";
            this.pictureHandle.Size = new System.Drawing.Size(24, 24);
            this.pictureHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureHandle.TabIndex = 87;
            this.pictureHandle.TabStop = false;
            // 
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(247, 5);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(20, 20);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 88;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // cmsPasteText
            // 
            this.cmsPasteText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CP_copy_unformatted,
            this.CP_copy_formatted,
            this.CP_paste_unformatted,
            this.CP_paste_formatted});
            this.cmsPasteText.Name = "cmsPasteText";
            this.cmsPasteText.ShowImageMargin = false;
            this.cmsPasteText.Size = new System.Drawing.Size(173, 92);
            // 
            // CP_copy_unformatted
            // 
            this.CP_copy_unformatted.Name = "CP_copy_unformatted";
            this.CP_copy_unformatted.Size = new System.Drawing.Size(172, 22);
            this.CP_copy_unformatted.Text = "Copy Unformatted Text";
            // 
            // CP_copy_formatted
            // 
            this.CP_copy_formatted.Name = "CP_copy_formatted";
            this.CP_copy_formatted.Size = new System.Drawing.Size(172, 22);
            this.CP_copy_formatted.Text = "Copy Formatted Text";
            // 
            // CP_paste_unformatted
            // 
            this.CP_paste_unformatted.Name = "CP_paste_unformatted";
            this.CP_paste_unformatted.Size = new System.Drawing.Size(172, 22);
            this.CP_paste_unformatted.Text = "Paste Unformatted Text";
            // 
            // CP_paste_formatted
            // 
            this.CP_paste_formatted.Name = "CP_paste_formatted";
            this.CP_paste_formatted.Size = new System.Drawing.Size(172, 22);
            this.CP_paste_formatted.Text = "Paste Formatted Text";
            // 
            // cmsCommon
            // 
            this.cmsCommon.Name = "cmsCommon";
            this.cmsCommon.Size = new System.Drawing.Size(61, 4);
            // 
            // cmiSize
            // 
            this.cmiSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmiSize.Location = new System.Drawing.Point(0, 14);
            this.cmiSize.Name = "cmiSize";
            this.cmiSize.Size = new System.Drawing.Size(16, 16);
            this.cmiSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cmiSize.TabIndex = 89;
            this.cmiSize.TabStop = false;
            this.cmiSize.Visible = false;
            // 
            // subtopic
            // 
            this.subtopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.subtopic.Image = ((System.Drawing.Image)(resources.GetObject("subtopic.Image")));
            this.subtopic.Location = new System.Drawing.Point(27, 5);
            this.subtopic.Name = "subtopic";
            this.subtopic.Size = new System.Drawing.Size(20, 20);
            this.subtopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.subtopic.TabIndex = 91;
            this.subtopic.TabStop = false;
            this.subtopic.Tag = "1";
            this.subtopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PasteTopic_MouseClick);
            this.subtopic.MouseHover += new System.EventHandler(this.AddPasteTopic_MouseHover);
            // 
            // pPaste
            // 
            this.pPaste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pPaste.Image = ((System.Drawing.Image)(resources.GetObject("pPaste.Image")));
            this.pPaste.Location = new System.Drawing.Point(137, 3);
            this.pPaste.Name = "pPaste";
            this.pPaste.Size = new System.Drawing.Size(24, 24);
            this.pPaste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPaste.TabIndex = 92;
            this.pPaste.TabStop = false;
            this.pPaste.Tag = "1";
            this.pPaste.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pPaste_MouseClick);
            // 
            // ToggleTextFormat
            // 
            this.ToggleTextFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ToggleTextFormat.Image = ((System.Drawing.Image)(resources.GetObject("ToggleTextFormat.Image")));
            this.ToggleTextFormat.Location = new System.Drawing.Point(171, 5);
            this.ToggleTextFormat.Name = "ToggleTextFormat";
            this.ToggleTextFormat.Size = new System.Drawing.Size(20, 20);
            this.ToggleTextFormat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ToggleTextFormat.TabIndex = 93;
            this.ToggleTextFormat.TabStop = false;
            this.ToggleTextFormat.Tag = "1";
            this.ToggleTextFormat.Click += new System.EventHandler(this.ToggleTextFormat_Click);
            // 
            // pReplace
            // 
            this.pReplace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pReplace.Image = ((System.Drawing.Image)(resources.GetObject("pReplace.Image")));
            this.pReplace.Location = new System.Drawing.Point(221, 7);
            this.pReplace.Name = "pReplace";
            this.pReplace.Size = new System.Drawing.Size(16, 16);
            this.pReplace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pReplace.TabIndex = 94;
            this.pReplace.TabStop = false;
            this.pReplace.Tag = "1";
            this.pReplace.Click += new System.EventHandler(this.pReplace_Click);
            // 
            // BubblePaste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(269, 30);
            this.ControlBox = false;
            this.Controls.Add(this.pReplace);
            this.Controls.Add(this.ToggleTextFormat);
            this.Controls.Add(this.pPaste);
            this.Controls.Add(this.subtopic);
            this.Controls.Add(this.cmiSize);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.UnformatText);
            this.Controls.Add(this.pCopy);
            this.Controls.Add(this.PasteNotes);
            this.Controls.Add(this.PasteLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubblePaste";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnformatText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            this.cmsPasteText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subtopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPaste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToggleTextFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pReplace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox PasteNotes;
        private System.Windows.Forms.PictureBox pCopy;
        private System.Windows.Forms.PictureBox UnformatText;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.ContextMenuStrip cmsPasteText;
        private System.Windows.Forms.ToolStripMenuItem CP_paste_unformatted;
        private System.Windows.Forms.ToolStripMenuItem CP_paste_formatted;
        private System.Windows.Forms.ContextMenuStrip cmsCommon;
        private System.Windows.Forms.PictureBox cmiSize;
        private System.Windows.Forms.ToolStripMenuItem CP_copy_formatted;
        public System.Windows.Forms.PictureBox subtopic;
        private System.Windows.Forms.ToolStripMenuItem CP_copy_unformatted;
        private System.Windows.Forms.PictureBox pPaste;
        public System.Windows.Forms.PictureBox ToggleTextFormat;
        public System.Windows.Forms.PictureBox PasteLink;
        public System.Windows.Forms.PictureBox pReplace;
    }
}