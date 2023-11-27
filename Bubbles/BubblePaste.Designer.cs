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
            this.cmsAddTopic = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CP_pasteas = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_addsubtopic = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_addnext = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_addbefore = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_addparent = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_addfloating = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_addcallout = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.PasteLink = new System.Windows.Forms.PictureBox();
            this.PasteNotes = new System.Windows.Forms.PictureBox();
            this.pPasteAsSubtopic = new System.Windows.Forms.PictureBox();
            this.pPaste = new System.Windows.Forms.PictureBox();
            this.pCopy = new System.Windows.Forms.PictureBox();
            this.UnformatText = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.cmsPasteText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CP_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_unformatted = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_inside = new System.Windows.Forms.ToolStripMenuItem();
            this.CP_atend = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUnformattedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCommon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiSize = new System.Windows.Forms.PictureBox();
            this.ToggleTextFormat = new System.Windows.Forms.PictureBox();
            this.cmsAddTopic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPasteAsSubtopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPaste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnformatText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            this.cmsPasteText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToggleTextFormat)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // cmsAddTopic
            // 
            this.cmsAddTopic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CP_pasteas,
            this.CP_addsubtopic,
            this.CP_addnext,
            this.CP_addbefore,
            this.CP_addparent,
            this.CP_addfloating,
            this.CP_addcallout});
            this.cmsAddTopic.Name = "contextMenuStrip1";
            this.cmsAddTopic.Size = new System.Drawing.Size(149, 158);
            // 
            // CP_pasteas
            // 
            this.CP_pasteas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.CP_pasteas.Name = "CP_pasteas";
            this.CP_pasteas.Size = new System.Drawing.Size(148, 22);
            this.CP_pasteas.Text = "Paste As:";
            // 
            // CP_addsubtopic
            // 
            this.CP_addsubtopic.Name = "CP_addsubtopic";
            this.CP_addsubtopic.Size = new System.Drawing.Size(148, 22);
            this.CP_addsubtopic.Text = "Subtopic";
            // 
            // CP_addnext
            // 
            this.CP_addnext.Name = "CP_addnext";
            this.CP_addnext.Size = new System.Drawing.Size(148, 22);
            this.CP_addnext.Text = "Next Topic";
            // 
            // CP_addbefore
            // 
            this.CP_addbefore.Name = "CP_addbefore";
            this.CP_addbefore.Size = new System.Drawing.Size(148, 22);
            this.CP_addbefore.Text = "Topic Before";
            // 
            // CP_addparent
            // 
            this.CP_addparent.Name = "CP_addparent";
            this.CP_addparent.Size = new System.Drawing.Size(148, 22);
            this.CP_addparent.Text = "Parent Topic";
            // 
            // CP_addfloating
            // 
            this.CP_addfloating.Name = "CP_addfloating";
            this.CP_addfloating.Size = new System.Drawing.Size(148, 22);
            this.CP_addfloating.Text = "Floating Topic";
            // 
            // CP_addcallout
            // 
            this.CP_addcallout.Name = "CP_addcallout";
            this.CP_addcallout.Size = new System.Drawing.Size(148, 22);
            this.CP_addcallout.Text = "Callout";
            // 
            // PasteLink
            // 
            this.PasteLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasteLink.Image = ((System.Drawing.Image)(resources.GetObject("PasteLink.Image")));
            this.PasteLink.Location = new System.Drawing.Point(113, 5);
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
            this.PasteNotes.Location = new System.Drawing.Point(139, 5);
            this.PasteNotes.Name = "PasteNotes";
            this.PasteNotes.Size = new System.Drawing.Size(20, 20);
            this.PasteNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PasteNotes.TabIndex = 76;
            this.PasteNotes.TabStop = false;
            this.PasteNotes.Tag = "1";
            this.PasteNotes.Click += new System.EventHandler(this.PasteNotes_Click);
            // 
            // pPasteAsSubtopic
            // 
            this.pPasteAsSubtopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pPasteAsSubtopic.Image = ((System.Drawing.Image)(resources.GetObject("pPasteAsSubtopic.Image")));
            this.pPasteAsSubtopic.Location = new System.Drawing.Point(87, 5);
            this.pPasteAsSubtopic.Name = "pPasteAsSubtopic";
            this.pPasteAsSubtopic.Size = new System.Drawing.Size(20, 20);
            this.pPasteAsSubtopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPasteAsSubtopic.TabIndex = 78;
            this.pPasteAsSubtopic.TabStop = false;
            this.pPasteAsSubtopic.Tag = "1";
            this.pPasteAsSubtopic.Click += new System.EventHandler(this.addsubtopic_Click);
            this.pPasteAsSubtopic.MouseHover += new System.EventHandler(this.addsubtopic_MouseHover);
            // 
            // pPaste
            // 
            this.pPaste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pPaste.Image = ((System.Drawing.Image)(resources.GetObject("pPaste.Image")));
            this.pPaste.Location = new System.Drawing.Point(24, 3);
            this.pPaste.Name = "pPaste";
            this.pPaste.Size = new System.Drawing.Size(24, 24);
            this.pPaste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPaste.TabIndex = 79;
            this.pPaste.TabStop = false;
            this.pPaste.Tag = "1";
            this.pPaste.Click += new System.EventHandler(this.pPaste_Click);
            // 
            // pCopy
            // 
            this.pCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pCopy.Image = ((System.Drawing.Image)(resources.GetObject("pCopy.Image")));
            this.pCopy.Location = new System.Drawing.Point(54, 3);
            this.pCopy.Name = "pCopy";
            this.pCopy.Size = new System.Drawing.Size(24, 24);
            this.pCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCopy.TabIndex = 81;
            this.pCopy.TabStop = false;
            this.pCopy.Tag = "1";
            this.pCopy.Click += new System.EventHandler(this.copyTopicText_Click);
            // 
            // UnformatText
            // 
            this.UnformatText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UnformatText.Image = ((System.Drawing.Image)(resources.GetObject("UnformatText.Image")));
            this.UnformatText.Location = new System.Drawing.Point(195, 5);
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
            this.Manage.Location = new System.Drawing.Point(221, 5);
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
            this.CP_paste,
            this.CP_unformatted,
            this.CP_inside,
            this.CP_atend,
            this.copyUnformattedTextToolStripMenuItem});
            this.cmsPasteText.Name = "cmsPasteText";
            this.cmsPasteText.Size = new System.Drawing.Size(158, 114);
            // 
            // CP_paste
            // 
            this.CP_paste.Name = "CP_paste";
            this.CP_paste.Size = new System.Drawing.Size(157, 22);
            this.CP_paste.Text = "Paste (Ctrl+V)";
            // 
            // CP_unformatted
            // 
            this.CP_unformatted.Name = "CP_unformatted";
            this.CP_unformatted.Size = new System.Drawing.Size(157, 22);
            this.CP_unformatted.Text = "Paste To Topic";
            // 
            // CP_inside
            // 
            this.CP_inside.Name = "CP_inside";
            this.CP_inside.Size = new System.Drawing.Size(157, 22);
            this.CP_inside.Text = "Paste Inside";
            // 
            // CP_atend
            // 
            this.CP_atend.Name = "CP_atend";
            this.CP_atend.Size = new System.Drawing.Size(157, 22);
            this.CP_atend.Text = "Add at End";
            // 
            // copyUnformattedTextToolStripMenuItem
            // 
            this.copyUnformattedTextToolStripMenuItem.Name = "copyUnformattedTextToolStripMenuItem";
            this.copyUnformattedTextToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyUnformattedTextToolStripMenuItem.Text = "Copy Topic Text";
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
            // ToggleTextFormat
            // 
            this.ToggleTextFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ToggleTextFormat.Image = ((System.Drawing.Image)(resources.GetObject("ToggleTextFormat.Image")));
            this.ToggleTextFormat.Location = new System.Drawing.Point(169, 5);
            this.ToggleTextFormat.Name = "ToggleTextFormat";
            this.ToggleTextFormat.Size = new System.Drawing.Size(20, 20);
            this.ToggleTextFormat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ToggleTextFormat.TabIndex = 90;
            this.ToggleTextFormat.TabStop = false;
            this.ToggleTextFormat.Tag = "unformatted";
            this.ToggleTextFormat.Click += new System.EventHandler(this.ToggleTextFormat_Click);
            // 
            // BubblePaste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(245, 30);
            this.ControlBox = false;
            this.Controls.Add(this.ToggleTextFormat);
            this.Controls.Add(this.cmiSize);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.UnformatText);
            this.Controls.Add(this.pCopy);
            this.Controls.Add(this.pPaste);
            this.Controls.Add(this.pPasteAsSubtopic);
            this.Controls.Add(this.PasteNotes);
            this.Controls.Add(this.PasteLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubblePaste";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.cmsAddTopic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PasteLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPasteAsSubtopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPaste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnformatText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            this.cmsPasteText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToggleTextFormat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmsAddTopic;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox PasteLink;
        private System.Windows.Forms.PictureBox PasteNotes;
        private System.Windows.Forms.PictureBox pPasteAsSubtopic;
        private System.Windows.Forms.PictureBox pPaste;
        private System.Windows.Forms.PictureBox pCopy;
        private System.Windows.Forms.PictureBox UnformatText;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.ToolStripMenuItem CP_addsubtopic;
        private System.Windows.Forms.ToolStripMenuItem CP_addnext;
        private System.Windows.Forms.ToolStripMenuItem CP_addbefore;
        private System.Windows.Forms.ToolStripMenuItem CP_addparent;
        private System.Windows.Forms.ToolStripMenuItem CP_addfloating;
        private System.Windows.Forms.ToolStripMenuItem CP_addcallout;
        private System.Windows.Forms.ContextMenuStrip cmsPasteText;
        private System.Windows.Forms.ToolStripMenuItem CP_unformatted;
        private System.Windows.Forms.ToolStripMenuItem CP_inside;
        private System.Windows.Forms.ToolStripMenuItem CP_atend;
        private System.Windows.Forms.ToolStripMenuItem CP_paste;
        private System.Windows.Forms.ContextMenuStrip cmsCommon;
        private System.Windows.Forms.ToolStripMenuItem CP_pasteas;
        private System.Windows.Forms.PictureBox cmiSize;
        private System.Windows.Forms.ToolStripMenuItem copyUnformattedTextToolStripMenuItem;
        public System.Windows.Forms.PictureBox ToggleTextFormat;
    }
}