namespace Bubbles
{
    partial class OmniSound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OmniSound));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.btnAddToTopic = new System.Windows.Forms.Button();
            this.cbRecords = new System.Windows.Forms.ComboBox();
            this.btnRecord = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.PictureBox();
            this.btnPause = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.PictureBox();
            this.panelRecordName = new System.Windows.Forms.Panel();
            this.btnCancelRecord = new System.Windows.Forms.Button();
            this.btnSaveRecord = new System.Windows.Forms.Button();
            this.lblRecordName = new System.Windows.Forms.Label();
            this.Volume = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.chAttachment = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.o_recordsystem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.o_close = new System.Windows.Forms.ToolStripMenuItem();
            this.o_help = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.aTrack = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pRecordSystem = new System.Windows.Forms.PictureBox();
            this.pEmpty = new System.Windows.Forms.PictureBox();
            this.pBlink = new System.Windows.Forms.PictureBox();
            this.lblClock = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timerStopPlay = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).BeginInit();
            this.panelRecordName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Volume)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRecordSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBlink)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtRecordName
            // 
            this.txtRecordName.Location = new System.Drawing.Point(10, 50);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.Size = new System.Drawing.Size(165, 20);
            this.txtRecordName.TabIndex = 8;
            this.txtRecordName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRecordName_KeyUp);
            // 
            // btnAddToTopic
            // 
            this.btnAddToTopic.Location = new System.Drawing.Point(14, 103);
            this.btnAddToTopic.Name = "btnAddToTopic";
            this.btnAddToTopic.Size = new System.Drawing.Size(84, 23);
            this.btnAddToTopic.TabIndex = 9;
            this.btnAddToTopic.Text = "Add to Topic";
            this.btnAddToTopic.UseVisualStyleBackColor = true;
            this.btnAddToTopic.Click += new System.EventHandler(this.btnAddToTopic_Click);
            // 
            // cbRecords
            // 
            this.cbRecords.FormattingEnabled = true;
            this.cbRecords.Location = new System.Drawing.Point(14, 72);
            this.cbRecords.Name = "cbRecords";
            this.cbRecords.Size = new System.Drawing.Size(178, 21);
            this.cbRecords.Sorted = true;
            this.cbRecords.TabIndex = 11;
            // 
            // btnRecord
            // 
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.Location = new System.Drawing.Point(12, 12);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(28, 28);
            this.btnRecord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRecord.TabIndex = 14;
            this.btnRecord.TabStop = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(161, 12);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(28, 28);
            this.btnPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPlay.TabIndex = 15;
            this.btnPlay.TabStop = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(104, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(28, 28);
            this.btnPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPause.TabIndex = 16;
            this.btnPause.TabStop = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(71, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(28, 28);
            this.btnStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnStop.TabIndex = 17;
            this.btnStop.TabStop = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // panelRecordName
            // 
            this.panelRecordName.BackColor = System.Drawing.Color.PapayaWhip;
            this.panelRecordName.Controls.Add(this.btnCancelRecord);
            this.panelRecordName.Controls.Add(this.btnSaveRecord);
            this.panelRecordName.Controls.Add(this.lblRecordName);
            this.panelRecordName.Controls.Add(this.txtRecordName);
            this.panelRecordName.Location = new System.Drawing.Point(8, 12);
            this.panelRecordName.Name = "panelRecordName";
            this.panelRecordName.Size = new System.Drawing.Size(186, 117);
            this.panelRecordName.TabIndex = 21;
            this.panelRecordName.Visible = false;
            // 
            // btnCancelRecord
            // 
            this.btnCancelRecord.Location = new System.Drawing.Point(100, 85);
            this.btnCancelRecord.Name = "btnCancelRecord";
            this.btnCancelRecord.Size = new System.Drawing.Size(75, 23);
            this.btnCancelRecord.TabIndex = 23;
            this.btnCancelRecord.Text = "Cancel";
            this.btnCancelRecord.UseVisualStyleBackColor = true;
            this.btnCancelRecord.Click += new System.EventHandler(this.btnCancelRecord_Click);
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.Location = new System.Drawing.Point(10, 85);
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRecord.TabIndex = 22;
            this.btnSaveRecord.Text = "Save";
            this.btnSaveRecord.UseVisualStyleBackColor = true;
            this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
            // 
            // lblRecordName
            // 
            this.lblRecordName.Location = new System.Drawing.Point(5, 8);
            this.lblRecordName.Name = "lblRecordName";
            this.lblRecordName.Size = new System.Drawing.Size(176, 30);
            this.lblRecordName.TabIndex = 9;
            this.lblRecordName.Text = "Введите имя записи и нажмите Enter. Для отмены - Escape.";
            // 
            // Volume
            // 
            this.Volume.AutoSize = false;
            this.Volume.BackColor = System.Drawing.Color.Lavender;
            this.Volume.Location = new System.Drawing.Point(28, 134);
            this.Volume.Maximum = 100;
            this.Volume.Name = "Volume";
            this.Volume.Size = new System.Drawing.Size(164, 20);
            this.Volume.TabIndex = 25;
            this.Volume.TickFrequency = 10;
            this.Volume.Value = 50;
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // chAttachment
            // 
            this.chAttachment.AutoSize = true;
            this.chAttachment.Location = new System.Drawing.Point(103, 107);
            this.chAttachment.Name = "chAttachment";
            this.chAttachment.Size = new System.Drawing.Size(95, 17);
            this.chAttachment.TabIndex = 24;
            this.chAttachment.Text = "As Attachment";
            this.chAttachment.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.o_recordsystem,
            this.toolStripSeparator1,
            this.o_close,
            this.o_help});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowCheckMargin = true;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 76);
            // 
            // o_recordsystem
            // 
            this.o_recordsystem.CheckOnClick = true;
            this.o_recordsystem.Name = "o_recordsystem";
            this.o_recordsystem.Size = new System.Drawing.Size(187, 22);
            this.o_recordsystem.Text = "Record System Audio";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // o_close
            // 
            this.o_close.Name = "o_close";
            this.o_close.Size = new System.Drawing.Size(187, 22);
            this.o_close.Text = "Close";
            // 
            // o_help
            // 
            this.o_help.Name = "o_help";
            this.o_help.Size = new System.Drawing.Size(187, 22);
            this.o_help.Text = "Help";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 135);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // aTrack
            // 
            this.aTrack.AutoSize = false;
            this.aTrack.BackColor = System.Drawing.Color.Lavender;
            this.aTrack.Location = new System.Drawing.Point(28, 156);
            this.aTrack.Maximum = 10000;
            this.aTrack.Name = "aTrack";
            this.aTrack.Size = new System.Drawing.Size(164, 20);
            this.aTrack.TabIndex = 27;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 157);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // pRecordSystem
            // 
            this.pRecordSystem.Image = ((System.Drawing.Image)(resources.GetObject("pRecordSystem.Image")));
            this.pRecordSystem.Location = new System.Drawing.Point(88, 80);
            this.pRecordSystem.Name = "pRecordSystem";
            this.pRecordSystem.Size = new System.Drawing.Size(28, 28);
            this.pRecordSystem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRecordSystem.TabIndex = 29;
            this.pRecordSystem.TabStop = false;
            this.pRecordSystem.Visible = false;
            this.pRecordSystem.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // pEmpty
            // 
            this.pEmpty.Image = ((System.Drawing.Image)(resources.GetObject("pEmpty.Image")));
            this.pEmpty.Location = new System.Drawing.Point(136, 80);
            this.pEmpty.Name = "pEmpty";
            this.pEmpty.Size = new System.Drawing.Size(28, 28);
            this.pEmpty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pEmpty.TabIndex = 30;
            this.pEmpty.TabStop = false;
            this.pEmpty.Visible = false;
            // 
            // pBlink
            // 
            this.pBlink.Location = new System.Drawing.Point(182, 125);
            this.pBlink.Name = "pBlink";
            this.pBlink.Size = new System.Drawing.Size(16, 16);
            this.pBlink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBlink.TabIndex = 31;
            this.pBlink.TabStop = false;
            this.pBlink.Visible = false;
            // 
            // lblClock
            // 
            this.lblClock.BackColor = System.Drawing.Color.Transparent;
            this.lblClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClock.Location = new System.Drawing.Point(62, 47);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(78, 18);
            this.lblClock.TabIndex = 81;
            this.lblClock.Text = "00:00 / 00:00         ";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClock.Click += new System.EventHandler(this.lblClock_Click);
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timerStopPlay
            // 
            this.timerStopPlay.Interval = 500;
            this.timerStopPlay.Tick += new System.EventHandler(this.timerStopPlay_Tick);
            // 
            // OmniSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(202, 186);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.lblClock);
            this.Controls.Add(this.pBlink);
            this.Controls.Add(this.pEmpty);
            this.Controls.Add(this.pRecordSystem);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.aTrack);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Volume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnAddToTopic);
            this.Controls.Add(this.cbRecords);
            this.Controls.Add(this.chAttachment);
            this.Controls.Add(this.panelRecordName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OmniSound";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OmniSound";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OmniSound_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).EndInit();
            this.panelRecordName.ResumeLayout(false);
            this.panelRecordName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Volume)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRecordSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBlink)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Button btnAddToTopic;
        private System.Windows.Forms.PictureBox btnRecord;
        private System.Windows.Forms.PictureBox btnPlay;
        private System.Windows.Forms.Panel panelRecordName;
        private System.Windows.Forms.Label lblRecordName;
        private System.Windows.Forms.Button btnSaveRecord;
        private System.Windows.Forms.Button btnCancelRecord;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ComboBox cbRecords;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.CheckBox chAttachment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem o_close;
        private System.Windows.Forms.ToolStripMenuItem o_recordsystem;
        private System.Windows.Forms.ToolStripMenuItem o_help;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar aTrack;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pRecordSystem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.PictureBox pEmpty;
        public System.Windows.Forms.PictureBox pBlink;
        public System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timerStopPlay;
        public System.Windows.Forms.TrackBar Volume;
        public System.Windows.Forms.PictureBox btnPause;
        public System.Windows.Forms.PictureBox btnStop;
    }
}