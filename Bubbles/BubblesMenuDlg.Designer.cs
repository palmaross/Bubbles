namespace Bubbles
{
    partial class BubblesMenuDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubblesMenuDlg));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNotepad = new System.Windows.Forms.Label();
            this.Notepad = new System.Windows.Forms.PictureBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.Format = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.PictureBox();
            this.lblIcons = new System.Windows.Forms.Label();
            this.lblBookmarks = new System.Windows.Forms.Label();
            this.lblPriPro = new System.Windows.Forms.Label();
            this.lblCopyPaste = new System.Windows.Forms.Label();
            this.lblMySources = new System.Windows.Forms.Label();
            this.MySources = new System.Windows.Forms.PictureBox();
            this.Bookmarks = new System.Windows.Forms.PictureBox();
            this.Paste = new System.Windows.Forms.PictureBox();
            this.Icons = new System.Windows.Forms.PictureBox();
            this.PriPro = new System.Windows.Forms.PictureBox();
            this.Settings = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pSticker = new System.Windows.Forms.PictureBox();
            this.lblSticker = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Notepad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Format)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Help)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MySources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bookmarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Paste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriPro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSticker)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.lblSticker);
            this.panel1.Controls.Add(this.pSticker);
            this.panel1.Controls.Add(this.lblNotepad);
            this.panel1.Controls.Add(this.Notepad);
            this.panel1.Controls.Add(this.lblFormat);
            this.panel1.Controls.Add(this.Format);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Help);
            this.panel1.Controls.Add(this.lblIcons);
            this.panel1.Controls.Add(this.lblBookmarks);
            this.panel1.Controls.Add(this.lblPriPro);
            this.panel1.Controls.Add(this.lblCopyPaste);
            this.panel1.Controls.Add(this.lblMySources);
            this.panel1.Controls.Add(this.MySources);
            this.panel1.Controls.Add(this.Bookmarks);
            this.panel1.Controls.Add(this.Paste);
            this.panel1.Controls.Add(this.Icons);
            this.panel1.Controls.Add(this.PriPro);
            this.panel1.Controls.Add(this.Settings);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 143);
            this.panel1.TabIndex = 21;
            // 
            // lblNotepad
            // 
            this.lblNotepad.Location = new System.Drawing.Point(111, 55);
            this.lblNotepad.Name = "lblNotepad";
            this.lblNotepad.Size = new System.Drawing.Size(70, 13);
            this.lblNotepad.TabIndex = 33;
            this.lblNotepad.Text = "Блокнот";
            this.lblNotepad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Notepad
            // 
            this.Notepad.Image = ((System.Drawing.Image)(resources.GetObject("Notepad.Image")));
            this.Notepad.Location = new System.Drawing.Point(130, 21);
            this.Notepad.Name = "Notepad";
            this.Notepad.Size = new System.Drawing.Size(32, 32);
            this.Notepad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Notepad.TabIndex = 32;
            this.Notepad.TabStop = false;
            this.Notepad.Click += new System.EventHandler(this.Notepad_Click);
            // 
            // lblFormat
            // 
            this.lblFormat.Location = new System.Drawing.Point(370, 129);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(70, 13);
            this.lblFormat.TabIndex = 31;
            this.lblFormat.Text = "Формат";
            this.lblFormat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Format
            // 
            this.Format.Image = ((System.Drawing.Image)(resources.GetObject("Format.Image")));
            this.Format.Location = new System.Drawing.Point(389, 95);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(32, 32);
            this.Format.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Format.TabIndex = 30;
            this.Format.TabStop = false;
            this.Format.Click += new System.EventHandler(this.Formatting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "высота22";
            this.label1.Visible = false;
            // 
            // Help
            // 
            this.Help.Image = ((System.Drawing.Image)(resources.GetObject("Help.Image")));
            this.Help.Location = new System.Drawing.Point(279, 103);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(24, 24);
            this.Help.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Help.TabIndex = 28;
            this.Help.TabStop = false;
            // 
            // lblIcons
            // 
            this.lblIcons.Location = new System.Drawing.Point(-4, 127);
            this.lblIcons.Name = "lblIcons";
            this.lblIcons.Size = new System.Drawing.Size(70, 13);
            this.lblIcons.TabIndex = 26;
            this.lblIcons.Text = "Значки";
            this.lblIcons.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBookmarks
            // 
            this.lblBookmarks.Location = new System.Drawing.Point(184, 41);
            this.lblBookmarks.Name = "lblBookmarks";
            this.lblBookmarks.Size = new System.Drawing.Size(69, 13);
            this.lblBookmarks.TabIndex = 24;
            this.lblBookmarks.Text = "Закладки";
            this.lblBookmarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPriPro
            // 
            this.lblPriPro.Location = new System.Drawing.Point(43, 80);
            this.lblPriPro.Name = "lblPriPro";
            this.lblPriPro.Size = new System.Drawing.Size(70, 13);
            this.lblPriPro.TabIndex = 23;
            this.lblPriPro.Text = "ПриПро";
            this.lblPriPro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCopyPaste
            // 
            this.lblCopyPaste.Location = new System.Drawing.Point(322, 80);
            this.lblCopyPaste.Name = "lblCopyPaste";
            this.lblCopyPaste.Size = new System.Drawing.Size(70, 13);
            this.lblCopyPaste.TabIndex = 22;
            this.lblCopyPaste.Text = "Вставка";
            this.lblCopyPaste.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMySources
            // 
            this.lblMySources.Location = new System.Drawing.Point(252, 55);
            this.lblMySources.Name = "lblMySources";
            this.lblMySources.Size = new System.Drawing.Size(75, 13);
            this.lblMySources.TabIndex = 21;
            this.lblMySources.Text = "Источники";
            this.lblMySources.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MySources
            // 
            this.MySources.Image = ((System.Drawing.Image)(resources.GetObject("MySources.Image")));
            this.MySources.Location = new System.Drawing.Point(274, 21);
            this.MySources.Name = "MySources";
            this.MySources.Size = new System.Drawing.Size(32, 32);
            this.MySources.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MySources.TabIndex = 20;
            this.MySources.TabStop = false;
            this.MySources.Click += new System.EventHandler(this.MySources_Click);
            // 
            // Bookmarks
            // 
            this.Bookmarks.Image = ((System.Drawing.Image)(resources.GetObject("Bookmarks.Image")));
            this.Bookmarks.Location = new System.Drawing.Point(202, 7);
            this.Bookmarks.Name = "Bookmarks";
            this.Bookmarks.Size = new System.Drawing.Size(32, 32);
            this.Bookmarks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Bookmarks.TabIndex = 11;
            this.Bookmarks.TabStop = false;
            this.Bookmarks.Click += new System.EventHandler(this.Bookmarks_Click);
            // 
            // Paste
            // 
            this.Paste.Image = ((System.Drawing.Image)(resources.GetObject("Paste.Image")));
            this.Paste.Location = new System.Drawing.Point(341, 47);
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(32, 32);
            this.Paste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Paste.TabIndex = 10;
            this.Paste.TabStop = false;
            this.Paste.Click += new System.EventHandler(this.PasteBubble_Click);
            // 
            // Icons
            // 
            this.Icons.Image = ((System.Drawing.Image)(resources.GetObject("Icons.Image")));
            this.Icons.Location = new System.Drawing.Point(15, 95);
            this.Icons.Name = "Icons";
            this.Icons.Size = new System.Drawing.Size(32, 32);
            this.Icons.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Icons.TabIndex = 12;
            this.Icons.TabStop = false;
            this.Icons.Click += new System.EventHandler(this.Icons_Click);
            // 
            // PriPro
            // 
            this.PriPro.Image = ((System.Drawing.Image)(resources.GetObject("PriPro.Image")));
            this.PriPro.Location = new System.Drawing.Point(63, 47);
            this.PriPro.Name = "PriPro";
            this.PriPro.Size = new System.Drawing.Size(32, 32);
            this.PriPro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PriPro.TabIndex = 13;
            this.PriPro.TabStop = false;
            this.PriPro.Click += new System.EventHandler(this.PriPro_Click);
            // 
            // Settings
            // 
            this.Settings.Image = ((System.Drawing.Image)(resources.GetObject("Settings.Image")));
            this.Settings.Location = new System.Drawing.Point(132, 98);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(32, 32);
            this.Settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Settings.TabIndex = 14;
            this.Settings.TabStop = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // pSticker
            // 
            this.pSticker.Image = ((System.Drawing.Image)(resources.GetObject("pSticker.Image")));
            this.pSticker.Location = new System.Drawing.Point(202, 69);
            this.pSticker.Name = "pSticker";
            this.pSticker.Size = new System.Drawing.Size(32, 32);
            this.pSticker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pSticker.TabIndex = 34;
            this.pSticker.TabStop = false;
            this.pSticker.Click += new System.EventHandler(this.pSticker_Click);
            // 
            // lblSticker
            // 
            this.lblSticker.Location = new System.Drawing.Point(188, 104);
            this.lblSticker.Name = "lblSticker";
            this.lblSticker.Size = new System.Drawing.Size(59, 13);
            this.lblSticker.TabIndex = 35;
            this.lblSticker.Text = "Sticker";
            this.lblSticker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BubblesMenuDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(438, 145);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BubblesMenuDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dublicate";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Notepad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Format)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Help)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MySources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bookmarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Paste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriPro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSticker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBookmarks;
        private System.Windows.Forms.Label lblPriPro;
        private System.Windows.Forms.Label lblCopyPaste;
        private System.Windows.Forms.Label lblMySources;
        private System.Windows.Forms.Label lblIcons;
        private System.Windows.Forms.PictureBox Help;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.PictureBox Settings;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label lblNotepad;
        public System.Windows.Forms.PictureBox Bookmarks;
        public System.Windows.Forms.PictureBox Paste;
        public System.Windows.Forms.PictureBox Icons;
        public System.Windows.Forms.PictureBox PriPro;
        public System.Windows.Forms.PictureBox MySources;
        public System.Windows.Forms.PictureBox Format;
        public System.Windows.Forms.PictureBox Notepad;
        public System.Windows.Forms.PictureBox pSticker;
        private System.Windows.Forms.Label lblSticker;
    }
}