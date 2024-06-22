namespace Bubbles
{
    partial class TopicPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopicPlayer));
            this.btnPause = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbTrack = new System.Windows.Forms.TrackBar();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.pVolume = new System.Windows.Forms.PictureBox();
            this.pPosition = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.lblClock = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.cmsManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pTitle = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.PictureBox();
            this.cmsPlayPauseButtons = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReplay = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTitle = new Bubbles.TickerLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).BeginInit();
            this.cmsPlayPauseButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(25, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(24, 24);
            this.btnPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPause.TabIndex = 20;
            this.btnPause.TabStop = false;
            this.btnPause.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPlayPause_MouseUp);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // tbTrack
            // 
            this.tbTrack.AutoSize = false;
            this.tbTrack.BackColor = System.Drawing.Color.Plum;
            this.tbTrack.Location = new System.Drawing.Point(130, 8);
            this.tbTrack.Maximum = 10000;
            this.tbTrack.Name = "tbTrack";
            this.tbTrack.Size = new System.Drawing.Size(88, 18);
            this.tbTrack.TabIndex = 29;
            this.tbTrack.Visible = false;
            this.tbTrack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbTrack_MouseDown);
            // 
            // tbVolume
            // 
            this.tbVolume.AutoSize = false;
            this.tbVolume.BackColor = System.Drawing.Color.Plum;
            this.tbVolume.Location = new System.Drawing.Point(117, 26);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(88, 18);
            this.tbVolume.TabIndex = 28;
            this.tbVolume.TickFrequency = 10;
            this.tbVolume.Value = 50;
            this.tbVolume.Visible = false;
            this.tbVolume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbVolume_MouseDown);
            // 
            // pVolume
            // 
            this.pVolume.Image = ((System.Drawing.Image)(resources.GetObject("pVolume.Image")));
            this.pVolume.Location = new System.Drawing.Point(222, 6);
            this.pVolume.Name = "pVolume";
            this.pVolume.Size = new System.Drawing.Size(20, 20);
            this.pVolume.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pVolume.TabIndex = 30;
            this.pVolume.TabStop = false;
            this.pVolume.Tag = "Title";
            this.pVolume.Click += new System.EventHandler(this.pVolume_Click);
            // 
            // pPosition
            // 
            this.pPosition.Image = ((System.Drawing.Image)(resources.GetObject("pPosition.Image")));
            this.pPosition.Location = new System.Drawing.Point(211, 18);
            this.pPosition.Name = "pPosition";
            this.pPosition.Size = new System.Drawing.Size(20, 20);
            this.pPosition.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPosition.TabIndex = 31;
            this.pPosition.TabStop = false;
            this.pPosition.Visible = false;
            this.pPosition.Click += new System.EventHandler(this.pPosition_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(88, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(245, 6);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(20, 20);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 78;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // pictureHandle
            // 
            this.pictureHandle.BackColor = System.Drawing.Color.Transparent;
            this.pictureHandle.Image = ((System.Drawing.Image)(resources.GetObject("pictureHandle.Image")));
            this.pictureHandle.Location = new System.Drawing.Point(0, 3);
            this.pictureHandle.Name = "pictureHandle";
            this.pictureHandle.Size = new System.Drawing.Size(24, 24);
            this.pictureHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureHandle.TabIndex = 79;
            this.pictureHandle.TabStop = false;
            // 
            // lblClock
            // 
            this.lblClock.BackColor = System.Drawing.Color.Cornsilk;
            this.lblClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClock.Location = new System.Drawing.Point(53, 8);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(76, 18);
            this.lblClock.TabIndex = 80;
            this.lblClock.Text = "                      ";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmsManage
            // 
            this.cmsManage.Name = "contextMenuStrip1";
            this.cmsManage.Size = new System.Drawing.Size(61, 4);
            // 
            // pTitle
            // 
            this.pTitle.Image = ((System.Drawing.Image)(resources.GetObject("pTitle.Image")));
            this.pTitle.Location = new System.Drawing.Point(237, 18);
            this.pTitle.Name = "pTitle";
            this.pTitle.Size = new System.Drawing.Size(20, 20);
            this.pTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pTitle.TabIndex = 83;
            this.pTitle.TabStop = false;
            this.pTitle.Visible = false;
            this.pTitle.Click += new System.EventHandler(this.pTitle_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(55, 8);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(24, 24);
            this.btnPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPlay.TabIndex = 84;
            this.btnPlay.TabStop = false;
            this.btnPlay.Visible = false;
            this.btnPlay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPlayPause_MouseUp);
            // 
            // cmsPlayPauseButtons
            // 
            this.cmsPlayPauseButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStop,
            this.menuReplay});
            this.cmsPlayPauseButtons.Name = "cmsPlayPauseButtons";
            this.cmsPlayPauseButtons.ShowImageMargin = false;
            this.cmsPlayPauseButtons.Size = new System.Drawing.Size(85, 48);
            // 
            // menuStop
            // 
            this.menuStop.Name = "menuStop";
            this.menuStop.Size = new System.Drawing.Size(84, 22);
            this.menuStop.Text = "Stop 2";
            // 
            // menuReplay
            // 
            this.menuReplay.Name = "menuReplay";
            this.menuReplay.Size = new System.Drawing.Size(84, 22);
            this.menuReplay.Text = "Replay";
            // 
            // timer1
            // 
            this.timer1.Interval = 400;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Plum;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitle.Location = new System.Drawing.Point(65, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(88, 18);
            this.lblTitle.TabIndex = 82;
            this.lblTitle.Text = "tickerLabel1";
            // 
            // TopicPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(269, 32);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.pTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblClock);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pPosition);
            this.Controls.Add(this.pVolume);
            this.Controls.Add(this.tbTrack);
            this.Controls.Add(this.tbVolume);
            this.Controls.Add(this.btnPause);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TopicPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PlayBox";
            this.Load += new System.EventHandler(this.PlayBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).EndInit();
            this.cmsPlayPauseButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pVolume;
        public System.Windows.Forms.TrackBar tbTrack;
        public System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox Manage;
        public System.Windows.Forms.PictureBox pictureHandle;
        public System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ContextMenuStrip cmsManage;
        private System.Windows.Forms.PictureBox pTitle;
        public System.Windows.Forms.PictureBox pPosition;
        public System.Windows.Forms.PictureBox btnPause;
        public System.Windows.Forms.PictureBox btnPlay;
        private System.Windows.Forms.ContextMenuStrip cmsPlayPauseButtons;
        private System.Windows.Forms.ToolStripMenuItem menuStop;
        private System.Windows.Forms.ToolStripMenuItem menuReplay;
        private System.Windows.Forms.Timer timer1;
        public TickerLabel lblTitle;
    }
}