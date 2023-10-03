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
            this.lblIcons = new System.Windows.Forms.Label();
            this.lblSettings = new System.Windows.Forms.Label();
            this.lblBookmarks = new System.Windows.Forms.Label();
            this.lblPriPro = new System.Windows.Forms.Label();
            this.lblCopyPaste = new System.Windows.Forms.Label();
            this.lblMySources = new System.Windows.Forms.Label();
            this.MySources = new System.Windows.Forms.PictureBox();
            this.Bookmarks = new System.Windows.Forms.PictureBox();
            this.PasteBubble = new System.Windows.Forms.PictureBox();
            this.Icons = new System.Windows.Forms.PictureBox();
            this.ClipBoard = new System.Windows.Forms.PictureBox();
            this.Settings = new System.Windows.Forms.PictureBox();
            this.Screen = new System.Windows.Forms.PictureBox();
            this.Help = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MySources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bookmarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteBubble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClipBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Help)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.Help);
            this.panel1.Controls.Add(this.Screen);
            this.panel1.Controls.Add(this.lblIcons);
            this.panel1.Controls.Add(this.lblSettings);
            this.panel1.Controls.Add(this.lblBookmarks);
            this.panel1.Controls.Add(this.lblPriPro);
            this.panel1.Controls.Add(this.lblCopyPaste);
            this.panel1.Controls.Add(this.lblMySources);
            this.panel1.Controls.Add(this.MySources);
            this.panel1.Controls.Add(this.Bookmarks);
            this.panel1.Controls.Add(this.PasteBubble);
            this.panel1.Controls.Add(this.Icons);
            this.panel1.Controls.Add(this.ClipBoard);
            this.panel1.Controls.Add(this.Settings);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 137);
            this.panel1.TabIndex = 21;
            // 
            // lblIcons
            // 
            this.lblIcons.AutoSize = true;
            this.lblIcons.Location = new System.Drawing.Point(257, 118);
            this.lblIcons.Name = "lblIcons";
            this.lblIcons.Size = new System.Drawing.Size(66, 13);
            this.lblIcons.TabIndex = 26;
            this.lblIcons.Text = "Мои значки";
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.Location = new System.Drawing.Point(59, 118);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(62, 13);
            this.lblSettings.TabIndex = 25;
            this.lblSettings.Text = "Настройки";
            this.lblSettings.Visible = false;
            // 
            // lblBookmarks
            // 
            this.lblBookmarks.AutoSize = true;
            this.lblBookmarks.Location = new System.Drawing.Point(3, 118);
            this.lblBookmarks.Name = "lblBookmarks";
            this.lblBookmarks.Size = new System.Drawing.Size(56, 13);
            this.lblBookmarks.TabIndex = 24;
            this.lblBookmarks.Text = "Закладки";
            // 
            // lblPriPro
            // 
            this.lblPriPro.AutoSize = true;
            this.lblPriPro.Location = new System.Drawing.Point(223, 62);
            this.lblPriPro.Name = "lblPriPro";
            this.lblPriPro.Size = new System.Drawing.Size(47, 13);
            this.lblPriPro.TabIndex = 23;
            this.lblPriPro.Text = "ПриПро";
            // 
            // lblCopyPaste
            // 
            this.lblCopyPaste.AutoSize = true;
            this.lblCopyPaste.Location = new System.Drawing.Point(33, 61);
            this.lblCopyPaste.Name = "lblCopyPaste";
            this.lblCopyPaste.Size = new System.Drawing.Size(94, 13);
            this.lblCopyPaste.TabIndex = 22;
            this.lblCopyPaste.Text = "Режимы вставки";
            // 
            // lblMySources
            // 
            this.lblMySources.AutoSize = true;
            this.lblMySources.Location = new System.Drawing.Point(119, 39);
            this.lblMySources.Name = "lblMySources";
            this.lblMySources.Size = new System.Drawing.Size(83, 13);
            this.lblMySources.TabIndex = 21;
            this.lblMySources.Text = "Мои источники";
            // 
            // MySources
            // 
            this.MySources.Image = ((System.Drawing.Image)(resources.GetObject("MySources.Image")));
            this.MySources.Location = new System.Drawing.Point(145, 5);
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
            this.Bookmarks.Location = new System.Drawing.Point(15, 85);
            this.Bookmarks.Name = "Bookmarks";
            this.Bookmarks.Size = new System.Drawing.Size(32, 32);
            this.Bookmarks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Bookmarks.TabIndex = 11;
            this.Bookmarks.TabStop = false;
            this.Bookmarks.Click += new System.EventHandler(this.Bookmarks_Click);
            // 
            // PasteBubble
            // 
            this.PasteBubble.Image = ((System.Drawing.Image)(resources.GetObject("PasteBubble.Image")));
            this.PasteBubble.Location = new System.Drawing.Point(63, 28);
            this.PasteBubble.Name = "PasteBubble";
            this.PasteBubble.Size = new System.Drawing.Size(32, 32);
            this.PasteBubble.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PasteBubble.TabIndex = 10;
            this.PasteBubble.TabStop = false;
            this.PasteBubble.Click += new System.EventHandler(this.PasteBubble_Click);
            // 
            // Icons
            // 
            this.Icons.Image = ((System.Drawing.Image)(resources.GetObject("Icons.Image")));
            this.Icons.Location = new System.Drawing.Point(275, 85);
            this.Icons.Name = "Icons";
            this.Icons.Size = new System.Drawing.Size(32, 32);
            this.Icons.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Icons.TabIndex = 12;
            this.Icons.TabStop = false;
            this.Icons.Click += new System.EventHandler(this.Icons_Click);
            // 
            // ClipBoard
            // 
            this.ClipBoard.Image = ((System.Drawing.Image)(resources.GetObject("ClipBoard.Image")));
            this.ClipBoard.Location = new System.Drawing.Point(230, 28);
            this.ClipBoard.Name = "ClipBoard";
            this.ClipBoard.Size = new System.Drawing.Size(32, 32);
            this.ClipBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ClipBoard.TabIndex = 13;
            this.ClipBoard.TabStop = false;
            this.ClipBoard.Click += new System.EventHandler(this.ClipBoard_Click);
            // 
            // Settings
            // 
            this.Settings.Image = ((System.Drawing.Image)(resources.GetObject("Settings.Image")));
            this.Settings.Location = new System.Drawing.Point(109, 87);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(32, 32);
            this.Settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Settings.TabIndex = 14;
            this.Settings.TabStop = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Screen
            // 
            this.Screen.Image = ((System.Drawing.Image)(resources.GetObject("Screen.Image")));
            this.Screen.Location = new System.Drawing.Point(149, 78);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(24, 24);
            this.Screen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Screen.TabIndex = 27;
            this.Screen.TabStop = false;
            this.Screen.Click += new System.EventHandler(this.Screen_Click);
            // 
            // Help
            // 
            this.Help.Image = ((System.Drawing.Image)(resources.GetObject("Help.Image")));
            this.Help.Location = new System.Drawing.Point(182, 93);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(24, 24);
            this.Help.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Help.TabIndex = 28;
            this.Help.TabStop = false;
            // 
            // BubblesMenuDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(324, 140);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BubblesMenuDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dublicate";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MySources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bookmarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasteBubble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClipBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Help)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Settings;
        private System.Windows.Forms.PictureBox Bookmarks;
        private System.Windows.Forms.PictureBox PasteBubble;
        private System.Windows.Forms.PictureBox Icons;
        private System.Windows.Forms.PictureBox ClipBoard;
        private System.Windows.Forms.PictureBox MySources;
        private System.Windows.Forms.Label lblBookmarks;
        private System.Windows.Forms.Label lblPriPro;
        private System.Windows.Forms.Label lblCopyPaste;
        private System.Windows.Forms.Label lblMySources;
        private System.Windows.Forms.Label lblIcons;
        public System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.PictureBox Help;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}