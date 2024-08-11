namespace Bubbles
{
    partial class TopicRecorder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopicRecorder));
            this.btnRecord = new System.Windows.Forms.PictureBox();
            this.btnPause = new System.Windows.Forms.PictureBox();
            this.btnRecordEmpty = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnStop = new System.Windows.Forms.PictureBox();
            this.myToolTip1 = new Bubbles.MyToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecordEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.Location = new System.Drawing.Point(27, 5);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(28, 28);
            this.btnRecord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRecord.TabIndex = 21;
            this.btnRecord.TabStop = false;
            this.btnRecord.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnRecord_MouseClick);
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(97, 6);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(26, 26);
            this.btnPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPause.TabIndex = 22;
            this.btnPause.TabStop = false;
            this.btnPause.Tag = "record";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRecordEmpty
            // 
            this.btnRecordEmpty.Image = ((System.Drawing.Image)(resources.GetObject("btnRecordEmpty.Image")));
            this.btnRecordEmpty.Location = new System.Drawing.Point(12, 12);
            this.btnRecordEmpty.Name = "btnRecordEmpty";
            this.btnRecordEmpty.Size = new System.Drawing.Size(28, 28);
            this.btnRecordEmpty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRecordEmpty.TabIndex = 23;
            this.btnRecordEmpty.TabStop = false;
            this.btnRecordEmpty.Visible = false;
            // 
            // pictureHandle
            // 
            this.pictureHandle.BackColor = System.Drawing.Color.Transparent;
            this.pictureHandle.Image = ((System.Drawing.Image)(resources.GetObject("pictureHandle.Image")));
            this.pictureHandle.Location = new System.Drawing.Point(0, 0);
            this.pictureHandle.Name = "pictureHandle";
            this.pictureHandle.Size = new System.Drawing.Size(24, 24);
            this.pictureHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureHandle.TabIndex = 80;
            this.pictureHandle.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(62, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(28, 28);
            this.btnStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnStop.TabIndex = 81;
            this.btnStop.TabStop = false;
            this.btnStop.Tag = "record";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // myToolTip1
            // 
            this.myToolTip1.OwnerDraw = true;
            this.myToolTip1.ShowAlways = true;
            // 
            // TopicRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(130, 38);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.btnRecordEmpty);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TopicRecorder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TopicRecorder";
            this.Load += new System.EventHandler(this.TopicRecorder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecordEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox btnRecord;
        public System.Windows.Forms.PictureBox btnPause;
        public System.Windows.Forms.PictureBox btnRecordEmpty;
        public System.Windows.Forms.PictureBox pictureHandle;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        public System.Windows.Forms.PictureBox btnStop;
        private MyToolTip myToolTip1;
    }
}