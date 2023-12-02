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
            this.pAddTopic = new System.Windows.Forms.PictureBox();
            this.pCut = new System.Windows.Forms.PictureBox();
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
            this.pPasteTopic = new System.Windows.Forms.PictureBox();
            this.pPaste = new System.Windows.Forms.PictureBox();
            this.pAddCallout = new System.Windows.Forms.PictureBox();
            this.pAddMultiple = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddTopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnformatText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            this.cmsPasteText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPasteTopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPaste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddCallout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddMultiple)).BeginInit();
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
            this.PasteLink.Location = new System.Drawing.Point(143, 5);
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
            this.PasteNotes.Location = new System.Drawing.Point(168, 5);
            this.PasteNotes.Name = "PasteNotes";
            this.PasteNotes.Size = new System.Drawing.Size(20, 20);
            this.PasteNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PasteNotes.TabIndex = 76;
            this.PasteNotes.TabStop = false;
            this.PasteNotes.Tag = "1";
            this.PasteNotes.Click += new System.EventHandler(this.PasteNotes_Click);
            // 
            // pAddTopic
            // 
            this.pAddTopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pAddTopic.Image = ((System.Drawing.Image)(resources.GetObject("pAddTopic.Image")));
            this.pAddTopic.Location = new System.Drawing.Point(198, 5);
            this.pAddTopic.Name = "pAddTopic";
            this.pAddTopic.Size = new System.Drawing.Size(20, 20);
            this.pAddTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAddTopic.TabIndex = 78;
            this.pAddTopic.TabStop = false;
            this.pAddTopic.Tag = "1";
            this.pAddTopic.MouseHover += new System.EventHandler(this.AddPasteTopic_MouseHover);
            // 
            // pCut
            // 
            this.pCut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pCut.Image = ((System.Drawing.Image)(resources.GetObject("pCut.Image")));
            this.pCut.Location = new System.Drawing.Point(54, 3);
            this.pCut.Name = "pCut";
            this.pCut.Size = new System.Drawing.Size(24, 24);
            this.pCut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCut.TabIndex = 79;
            this.pCut.TabStop = false;
            this.pCut.Tag = "1";
            this.pCut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pCut_MouseClick);
            // 
            // pCopy
            // 
            this.pCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pCopy.Image = ((System.Drawing.Image)(resources.GetObject("pCopy.Image")));
            this.pCopy.Location = new System.Drawing.Point(24, 3);
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
            this.UnformatText.Location = new System.Drawing.Point(277, 5);
            this.UnformatText.Name = "UnformatText";
            this.UnformatText.Size = new System.Drawing.Size(20, 20);
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
            this.Manage.Location = new System.Drawing.Point(304, 5);
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
            // pPasteTopic
            // 
            this.pPasteTopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pPasteTopic.Image = ((System.Drawing.Image)(resources.GetObject("pPasteTopic.Image")));
            this.pPasteTopic.Location = new System.Drawing.Point(118, 5);
            this.pPasteTopic.Name = "pPasteTopic";
            this.pPasteTopic.Size = new System.Drawing.Size(20, 20);
            this.pPasteTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPasteTopic.TabIndex = 91;
            this.pPasteTopic.TabStop = false;
            this.pPasteTopic.Tag = "1";
            this.pPasteTopic.MouseHover += new System.EventHandler(this.AddPasteTopic_MouseHover);
            // 
            // pPaste
            // 
            this.pPaste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pPaste.Image = ((System.Drawing.Image)(resources.GetObject("pPaste.Image")));
            this.pPaste.Location = new System.Drawing.Point(84, 3);
            this.pPaste.Name = "pPaste";
            this.pPaste.Size = new System.Drawing.Size(24, 24);
            this.pPaste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPaste.TabIndex = 92;
            this.pPaste.TabStop = false;
            this.pPaste.Tag = "1";
            this.pPaste.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pPaste_MouseClick);
            // 
            // pAddCallout
            // 
            this.pAddCallout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pAddCallout.Image = ((System.Drawing.Image)(resources.GetObject("pAddCallout.Image")));
            this.pAddCallout.Location = new System.Drawing.Point(223, 5);
            this.pAddCallout.Name = "pAddCallout";
            this.pAddCallout.Size = new System.Drawing.Size(20, 20);
            this.pAddCallout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAddCallout.TabIndex = 93;
            this.pAddCallout.TabStop = false;
            this.pAddCallout.Tag = "1";
            // 
            // pAddMultiple
            // 
            this.pAddMultiple.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pAddMultiple.Image = ((System.Drawing.Image)(resources.GetObject("pAddMultiple.Image")));
            this.pAddMultiple.Location = new System.Drawing.Point(248, 5);
            this.pAddMultiple.Name = "pAddMultiple";
            this.pAddMultiple.Size = new System.Drawing.Size(20, 20);
            this.pAddMultiple.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAddMultiple.TabIndex = 94;
            this.pAddMultiple.TabStop = false;
            this.pAddMultiple.Tag = "1";
            this.pAddMultiple.Click += new System.EventHandler(this.pAddMultiple_Click);
            // 
            // BubblePaste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(326, 30);
            this.ControlBox = false;
            this.Controls.Add(this.pAddMultiple);
            this.Controls.Add(this.pAddCallout);
            this.Controls.Add(this.pPaste);
            this.Controls.Add(this.pPasteTopic);
            this.Controls.Add(this.cmiSize);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.UnformatText);
            this.Controls.Add(this.pCopy);
            this.Controls.Add(this.pCut);
            this.Controls.Add(this.pAddTopic);
            this.Controls.Add(this.PasteNotes);
            this.Controls.Add(this.PasteLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubblePaste";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddTopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnformatText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            this.cmsPasteText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPasteTopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPaste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddCallout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddMultiple)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox PasteLink;
        private System.Windows.Forms.PictureBox PasteNotes;
        private System.Windows.Forms.PictureBox pCut;
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
        public System.Windows.Forms.PictureBox pAddTopic;
        public System.Windows.Forms.PictureBox pPasteTopic;
        private System.Windows.Forms.ToolStripMenuItem CP_copy_unformatted;
        private System.Windows.Forms.PictureBox pPaste;
        public System.Windows.Forms.PictureBox pAddCallout;
        public System.Windows.Forms.PictureBox pAddMultiple;
    }
}