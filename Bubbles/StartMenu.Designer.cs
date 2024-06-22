namespace Bubbles
{
    partial class StartMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartMenu));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.stxAddTopics = new System.Windows.Forms.PictureBox();
            this.stxTextOps = new System.Windows.Forms.PictureBox();
            this.stxFormat = new System.Windows.Forms.PictureBox();
            this.stxTaskInfo = new System.Windows.Forms.PictureBox();
            this.stxIcons = new System.Windows.Forms.PictureBox();
            this.boxSources = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.cmsManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cm_hide = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_show = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_close = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_remember = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cm_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_help = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_about = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cm_autoclose = new System.Windows.Forms.ToolStripMenuItem();
            this.cm_closemenu = new System.Windows.Forms.ToolStripMenuItem();
            this.stxTools = new System.Windows.Forms.PictureBox();
            this.stxMapNavigator = new System.Windows.Forms.PictureBox();
            this.boxBookmarks = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBoxes = new System.Windows.Forms.Panel();
            this.Misc = new System.Windows.Forms.PictureBox();
            this.OmniSound = new System.Windows.Forms.PictureBox();
            this.Stickers = new System.Windows.Forms.PictureBox();
            this.p2 = new System.Windows.Forms.PictureBox();
            this.cmsOmniSound = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TopicPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TopicRecorder = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMisc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.o_QuickTopics = new System.Windows.Forms.ToolStripMenuItem();
            this.o_Resources = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.stxAddTopics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxTextOps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxTaskInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxIcons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxSources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            this.cmsManage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxMapNavigator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxBookmarks)).BeginInit();
            this.panelBoxes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Misc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OmniSound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stickers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            this.cmsOmniSound.SuspendLayout();
            this.cmsMisc.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // stxAddTopics
            // 
            this.stxAddTopics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stxAddTopics.Image = ((System.Drawing.Image)(resources.GetObject("stxAddTopics.Image")));
            this.stxAddTopics.Location = new System.Drawing.Point(143, 4);
            this.stxAddTopics.Name = "stxAddTopics";
            this.stxAddTopics.Size = new System.Drawing.Size(24, 24);
            this.stxAddTopics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stxAddTopics.TabIndex = 75;
            this.stxAddTopics.TabStop = false;
            this.stxAddTopics.Tag = "1";
            this.stxAddTopics.Click += new System.EventHandler(this.StxAddTopic_Click);
            // 
            // stxTextOps
            // 
            this.stxTextOps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stxTextOps.Image = ((System.Drawing.Image)(resources.GetObject("stxTextOps.Image")));
            this.stxTextOps.Location = new System.Drawing.Point(177, 4);
            this.stxTextOps.Name = "stxTextOps";
            this.stxTextOps.Size = new System.Drawing.Size(24, 24);
            this.stxTextOps.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stxTextOps.TabIndex = 76;
            this.stxTextOps.TabStop = false;
            this.stxTextOps.Tag = "1";
            this.stxTextOps.Click += new System.EventHandler(this.StxTextOps_Click);
            // 
            // stxFormat
            // 
            this.stxFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stxFormat.Image = ((System.Drawing.Image)(resources.GetObject("stxFormat.Image")));
            this.stxFormat.Location = new System.Drawing.Point(211, 4);
            this.stxFormat.Name = "stxFormat";
            this.stxFormat.Size = new System.Drawing.Size(24, 24);
            this.stxFormat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stxFormat.TabIndex = 78;
            this.stxFormat.TabStop = false;
            this.stxFormat.Tag = "1";
            this.stxFormat.Click += new System.EventHandler(this.StxFormat_Click);
            // 
            // stxTaskInfo
            // 
            this.stxTaskInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stxTaskInfo.Image = ((System.Drawing.Image)(resources.GetObject("stxTaskInfo.Image")));
            this.stxTaskInfo.Location = new System.Drawing.Point(41, 4);
            this.stxTaskInfo.Name = "stxTaskInfo";
            this.stxTaskInfo.Size = new System.Drawing.Size(24, 24);
            this.stxTaskInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stxTaskInfo.TabIndex = 79;
            this.stxTaskInfo.TabStop = false;
            this.stxTaskInfo.Tag = "1";
            this.stxTaskInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StxTaskInfo_MouseClick);
            // 
            // stxIcons
            // 
            this.stxIcons.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stxIcons.Image = ((System.Drawing.Image)(resources.GetObject("stxIcons.Image")));
            this.stxIcons.Location = new System.Drawing.Point(7, 4);
            this.stxIcons.Name = "stxIcons";
            this.stxIcons.Size = new System.Drawing.Size(24, 24);
            this.stxIcons.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stxIcons.TabIndex = 81;
            this.stxIcons.TabStop = false;
            this.stxIcons.Tag = "1";
            this.stxIcons.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StxIcon_MouseClick);
            // 
            // boxSources
            // 
            this.boxSources.BackColor = System.Drawing.Color.Moccasin;
            this.boxSources.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxSources.Image = ((System.Drawing.Image)(resources.GetObject("boxSources.Image")));
            this.boxSources.Location = new System.Drawing.Point(76, 6);
            this.boxSources.Name = "boxSources";
            this.boxSources.Size = new System.Drawing.Size(24, 24);
            this.boxSources.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boxSources.TabIndex = 85;
            this.boxSources.TabStop = false;
            this.boxSources.Tag = "1";
            this.boxSources.Click += new System.EventHandler(this.BoxSources_Click);
            // 
            // Manage
            // 
            this.Manage.BackColor = System.Drawing.Color.Moccasin;
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(212, 6);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(24, 24);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 88;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // cmsManage
            // 
            this.cmsManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cm_hide,
            this.cm_show,
            this.cm_close,
            this.cm_remember,
            this.toolStripSeparator1,
            this.cm_settings,
            this.cm_help,
            this.cm_about,
            this.toolStripSeparator2,
            this.cm_autoclose,
            this.cm_closemenu});
            this.cmsManage.Name = "cmsCommon";
            this.cmsManage.Size = new System.Drawing.Size(181, 214);
            // 
            // cm_hide
            // 
            this.cm_hide.Name = "cm_hide";
            this.cm_hide.Size = new System.Drawing.Size(180, 22);
            this.cm_hide.Text = "Hide All Stix";
            // 
            // cm_show
            // 
            this.cm_show.Name = "cm_show";
            this.cm_show.Size = new System.Drawing.Size(180, 22);
            this.cm_show.Text = "Show Hidden Stix";
            // 
            // cm_close
            // 
            this.cm_close.Name = "cm_close";
            this.cm_close.Size = new System.Drawing.Size(180, 22);
            this.cm_close.Text = "Close All Stix";
            // 
            // cm_remember
            // 
            this.cm_remember.Name = "cm_remember";
            this.cm_remember.Size = new System.Drawing.Size(180, 22);
            this.cm_remember.Text = "Remember All Stix";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // cm_settings
            // 
            this.cm_settings.Name = "cm_settings";
            this.cm_settings.Size = new System.Drawing.Size(180, 22);
            this.cm_settings.Text = "Settings";
            // 
            // cm_help
            // 
            this.cm_help.Name = "cm_help";
            this.cm_help.Size = new System.Drawing.Size(180, 22);
            this.cm_help.Text = "Help";
            // 
            // cm_about
            // 
            this.cm_about.Name = "cm_about";
            this.cm_about.Size = new System.Drawing.Size(180, 22);
            this.cm_about.Text = "About";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // cm_autoclose
            // 
            this.cm_autoclose.Name = "cm_autoclose";
            this.cm_autoclose.Size = new System.Drawing.Size(180, 22);
            this.cm_autoclose.Text = "Close Automatically";
            // 
            // cm_closemenu
            // 
            this.cm_closemenu.Name = "cm_closemenu";
            this.cm_closemenu.Size = new System.Drawing.Size(180, 22);
            this.cm_closemenu.Text = "Close";
            // 
            // stxTools
            // 
            this.stxTools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stxTools.Image = ((System.Drawing.Image)(resources.GetObject("stxTools.Image")));
            this.stxTools.Location = new System.Drawing.Point(109, 4);
            this.stxTools.Name = "stxTools";
            this.stxTools.Size = new System.Drawing.Size(24, 24);
            this.stxTools.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stxTools.TabIndex = 91;
            this.stxTools.TabStop = false;
            this.stxTools.Tag = "1";
            this.stxTools.MouseClick += new System.Windows.Forms.MouseEventHandler(this.stxTools_MouseClick);
            // 
            // stxMapNavigator
            // 
            this.stxMapNavigator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stxMapNavigator.Image = ((System.Drawing.Image)(resources.GetObject("stxMapNavigator.Image")));
            this.stxMapNavigator.Location = new System.Drawing.Point(75, 4);
            this.stxMapNavigator.Name = "stxMapNavigator";
            this.stxMapNavigator.Size = new System.Drawing.Size(24, 24);
            this.stxMapNavigator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stxMapNavigator.TabIndex = 92;
            this.stxMapNavigator.TabStop = false;
            this.stxMapNavigator.Tag = "1";
            this.stxMapNavigator.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StxBookmarks_MouseClick);
            // 
            // boxBookmarks
            // 
            this.boxBookmarks.BackColor = System.Drawing.Color.Moccasin;
            this.boxBookmarks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxBookmarks.Image = ((System.Drawing.Image)(resources.GetObject("boxBookmarks.Image")));
            this.boxBookmarks.Location = new System.Drawing.Point(42, 6);
            this.boxBookmarks.Name = "boxBookmarks";
            this.boxBookmarks.Size = new System.Drawing.Size(24, 24);
            this.boxBookmarks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.boxBookmarks.TabIndex = 94;
            this.boxBookmarks.TabStop = false;
            this.boxBookmarks.Tag = "1";
            this.boxBookmarks.Click += new System.EventHandler(this.BoxBookmarks_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "высота22";
            this.label1.Visible = false;
            // 
            // panelBoxes
            // 
            this.panelBoxes.BackColor = System.Drawing.Color.Moccasin;
            this.panelBoxes.Controls.Add(this.Misc);
            this.panelBoxes.Controls.Add(this.OmniSound);
            this.panelBoxes.Controls.Add(this.boxBookmarks);
            this.panelBoxes.Controls.Add(this.boxSources);
            this.panelBoxes.Controls.Add(this.Stickers);
            this.panelBoxes.Controls.Add(this.Manage);
            this.panelBoxes.Location = new System.Drawing.Point(-1, 33);
            this.panelBoxes.Name = "panelBoxes";
            this.panelBoxes.Size = new System.Drawing.Size(245, 37);
            this.panelBoxes.TabIndex = 96;
            // 
            // Misc
            // 
            this.Misc.BackColor = System.Drawing.Color.Moccasin;
            this.Misc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Misc.Image = ((System.Drawing.Image)(resources.GetObject("Misc.Image")));
            this.Misc.Location = new System.Drawing.Point(110, 6);
            this.Misc.Name = "Misc";
            this.Misc.Size = new System.Drawing.Size(24, 24);
            this.Misc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Misc.TabIndex = 99;
            this.Misc.TabStop = false;
            this.Misc.Tag = "1";
            this.Misc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Misc_MouseDown);
            // 
            // OmniSound
            // 
            this.OmniSound.BackColor = System.Drawing.Color.Moccasin;
            this.OmniSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OmniSound.Image = ((System.Drawing.Image)(resources.GetObject("OmniSound.Image")));
            this.OmniSound.Location = new System.Drawing.Point(8, 6);
            this.OmniSound.Name = "OmniSound";
            this.OmniSound.Size = new System.Drawing.Size(24, 24);
            this.OmniSound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OmniSound.TabIndex = 98;
            this.OmniSound.TabStop = false;
            this.OmniSound.Tag = "1";
            this.OmniSound.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OmniSound_MouseClick);
            // 
            // Stickers
            // 
            this.Stickers.BackColor = System.Drawing.Color.Moccasin;
            this.Stickers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Stickers.Image = ((System.Drawing.Image)(resources.GetObject("Stickers.Image")));
            this.Stickers.Location = new System.Drawing.Point(160, 6);
            this.Stickers.Name = "Stickers";
            this.Stickers.Size = new System.Drawing.Size(24, 24);
            this.Stickers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Stickers.TabIndex = 97;
            this.Stickers.TabStop = false;
            this.Stickers.Tag = "1";
            this.Stickers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Stickers_MouseClick);
            // 
            // p2
            // 
            this.p2.Location = new System.Drawing.Point(123, 34);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(12, 12);
            this.p2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p2.TabIndex = 98;
            this.p2.TabStop = false;
            this.p2.Visible = false;
            // 
            // cmsOmniSound
            // 
            this.cmsOmniSound.AllowDrop = true;
            this.cmsOmniSound.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TopicPlayer,
            this.TopicRecorder});
            this.cmsOmniSound.Name = "cmsOmniSound";
            this.cmsOmniSound.ShowImageMargin = false;
            this.cmsOmniSound.Size = new System.Drawing.Size(128, 48);
            this.cmsOmniSound.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsOmniSound_ItemClicked);
            // 
            // TopicPlayer
            // 
            this.TopicPlayer.Name = "TopicPlayer";
            this.TopicPlayer.Size = new System.Drawing.Size(127, 22);
            this.TopicPlayer.Text = "Topic Player";
            // 
            // TopicRecorder
            // 
            this.TopicRecorder.Name = "TopicRecorder";
            this.TopicRecorder.Size = new System.Drawing.Size(127, 22);
            this.TopicRecorder.Text = "Topic Recorder";
            // 
            // cmsMisc
            // 
            this.cmsMisc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.o_QuickTopics,
            this.o_Resources});
            this.cmsMisc.Name = "cmsMisc";
            this.cmsMisc.Size = new System.Drawing.Size(142, 48);
            // 
            // o_QuickTopics
            // 
            this.o_QuickTopics.Name = "o_QuickTopics";
            this.o_QuickTopics.Size = new System.Drawing.Size(141, 22);
            this.o_QuickTopics.Text = "Quick Topics";
            // 
            // o_Resources
            // 
            this.o_Resources.Name = "o_Resources";
            this.o_Resources.Size = new System.Drawing.Size(141, 22);
            this.o_Resources.Text = "Resources";
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(243, 68);
            this.ControlBox = false;
            this.Controls.Add(this.p2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stxMapNavigator);
            this.Controls.Add(this.stxTools);
            this.Controls.Add(this.stxIcons);
            this.Controls.Add(this.stxTaskInfo);
            this.Controls.Add(this.stxFormat);
            this.Controls.Add(this.stxTextOps);
            this.Controls.Add(this.stxAddTopics);
            this.Controls.Add(this.panelBoxes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StartMenu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.stxAddTopics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxTextOps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxTaskInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxIcons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxSources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            this.cmsManage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stxTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxMapNavigator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boxBookmarks)).EndInit();
            this.panelBoxes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Misc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OmniSound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stickers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            this.cmsOmniSound.ResumeLayout(false);
            this.cmsMisc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox boxSources;
        private System.Windows.Forms.ContextMenuStrip cmsManage;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBoxes;
        private System.Windows.Forms.PictureBox boxBookmarks;
        public System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.PictureBox Stickers;
        private System.Windows.Forms.ToolStripMenuItem cm_settings;
        private System.Windows.Forms.ToolStripMenuItem cm_help;
        private System.Windows.Forms.ToolStripMenuItem cm_about;
        private System.Windows.Forms.ToolStripMenuItem cm_close;
        private System.Windows.Forms.ToolStripMenuItem cm_hide;
        private System.Windows.Forms.ToolStripMenuItem cm_show;
        private System.Windows.Forms.ToolStripMenuItem cm_remember;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.PictureBox stxAddTopics;
        public System.Windows.Forms.PictureBox stxTextOps;
        public System.Windows.Forms.PictureBox stxTaskInfo;
        public System.Windows.Forms.PictureBox stxIcons;
        public System.Windows.Forms.PictureBox stxMapNavigator;
        public System.Windows.Forms.PictureBox stxFormat;
        public System.Windows.Forms.PictureBox stxTools;
        private System.Windows.Forms.PictureBox p2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cm_autoclose;
        private System.Windows.Forms.ToolStripMenuItem cm_closemenu;
        private System.Windows.Forms.PictureBox OmniSound;
        private System.Windows.Forms.ContextMenuStrip cmsOmniSound;
        private System.Windows.Forms.ToolStripMenuItem TopicPlayer;
        private System.Windows.Forms.ToolStripMenuItem TopicRecorder;
        private System.Windows.Forms.PictureBox Misc;
        private System.Windows.Forms.ContextMenuStrip cmsMisc;
        private System.Windows.Forms.ToolStripMenuItem o_QuickTopics;
        private System.Windows.Forms.ToolStripMenuItem o_Resources;
    }
}