namespace Bubbles
{
    partial class BubbleOrganizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubbleOrganizer));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pClipboard = new System.Windows.Forms.PictureBox();
            this.pIdeas = new System.Windows.Forms.PictureBox();
            this.pLinks = new System.Windows.Forms.PictureBox();
            this.pNotes = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.pTodos = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pClipboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIdeas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pLinks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTodos)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // pClipboard
            // 
            this.pClipboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pClipboard.Image = ((System.Drawing.Image)(resources.GetObject("pClipboard.Image")));
            this.pClipboard.Location = new System.Drawing.Point(123, 3);
            this.pClipboard.Name = "pClipboard";
            this.pClipboard.Size = new System.Drawing.Size(24, 24);
            this.pClipboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClipboard.TabIndex = 75;
            this.pClipboard.TabStop = false;
            this.pClipboard.Tag = "1";
            this.pClipboard.Click += new System.EventHandler(this.PasteLink_Click);
            // 
            // pIdeas
            // 
            this.pIdeas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pIdeas.Image = ((System.Drawing.Image)(resources.GetObject("pIdeas.Image")));
            this.pIdeas.Location = new System.Drawing.Point(57, 3);
            this.pIdeas.Name = "pIdeas";
            this.pIdeas.Size = new System.Drawing.Size(24, 24);
            this.pIdeas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIdeas.TabIndex = 76;
            this.pIdeas.TabStop = false;
            this.pIdeas.Tag = "1";
            this.pIdeas.Click += new System.EventHandler(this.PasteNotes_Click);
            // 
            // pLinks
            // 
            this.pLinks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pLinks.Image = ((System.Drawing.Image)(resources.GetObject("pLinks.Image")));
            this.pLinks.Location = new System.Drawing.Point(90, 3);
            this.pLinks.Name = "pLinks";
            this.pLinks.Size = new System.Drawing.Size(24, 24);
            this.pLinks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pLinks.TabIndex = 80;
            this.pLinks.TabStop = false;
            this.pLinks.Tag = "1";
            this.pLinks.Click += new System.EventHandler(this.callout_Click);
            // 
            // pNotes
            // 
            this.pNotes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pNotes.Image = ((System.Drawing.Image)(resources.GetObject("pNotes.Image")));
            this.pNotes.Location = new System.Drawing.Point(24, 3);
            this.pNotes.Name = "pNotes";
            this.pNotes.Size = new System.Drawing.Size(24, 24);
            this.pNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pNotes.TabIndex = 83;
            this.pNotes.TabStop = false;
            this.pNotes.Tag = "1";
            this.pNotes.Click += new System.EventHandler(this.Notes_Click);
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
            this.Manage.Location = new System.Drawing.Point(189, 5);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(20, 20);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 88;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // pTodos
            // 
            this.pTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pTodos.Image = ((System.Drawing.Image)(resources.GetObject("pTodos.Image")));
            this.pTodos.Location = new System.Drawing.Point(156, 3);
            this.pTodos.Name = "pTodos";
            this.pTodos.Size = new System.Drawing.Size(24, 24);
            this.pTodos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pTodos.TabIndex = 78;
            this.pTodos.TabStop = false;
            this.pTodos.Tag = "1";
            this.pTodos.Click += new System.EventHandler(this.addsubtopic_Click);
            // 
            // BubbleOrganizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(213, 30);
            this.ControlBox = false;
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.pNotes);
            this.Controls.Add(this.pLinks);
            this.Controls.Add(this.pTodos);
            this.Controls.Add(this.pIdeas);
            this.Controls.Add(this.pClipboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubbleOrganizer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.pClipboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIdeas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pLinks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTodos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox pClipboard;
        private System.Windows.Forms.PictureBox pIdeas;
        private System.Windows.Forms.PictureBox pLinks;
        private System.Windows.Forms.PictureBox pNotes;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.PictureBox pTodos;
    }
}