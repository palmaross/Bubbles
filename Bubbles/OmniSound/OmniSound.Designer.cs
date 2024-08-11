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
            this.cbRecordings = new System.Windows.Forms.ComboBox();
            this.btnRecord = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.PictureBox();
            this.btnPause = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.PictureBox();
            this.panelRecordName = new System.Windows.Forms.Panel();
            this.btnNewGroup = new System.Windows.Forms.PictureBox();
            this.cbGroupsSave = new System.Windows.Forms.ComboBox();
            this.chAddToTopic = new System.Windows.Forms.CheckBox();
            this.chAttachmentSave = new System.Windows.Forms.CheckBox();
            this.btnCancelRecord = new System.Windows.Forms.Button();
            this.btnSaveRecord = new System.Windows.Forms.Button();
            this.lblRecordName = new System.Windows.Forms.Label();
            this.Volume = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.chAttachment = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.o_manage = new System.Windows.Forms.ToolStripMenuItem();
            this.o_recordtype = new System.Windows.Forms.ToolStripMenuItem();
            this.o_newgroup = new System.Windows.Forms.ToolStripMenuItem();
            this.o_help = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.aTrack = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pRecordSystem = new System.Windows.Forms.PictureBox();
            this.pEmpty = new System.Windows.Forms.PictureBox();
            this.pBlink = new System.Windows.Forms.PictureBox();
            this.lblClock = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timerStopPlay = new System.Windows.Forms.Timer(this.components);
            this.cbGroups = new System.Windows.Forms.ComboBox();
            this.btnMore = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.panelNewGroup = new System.Windows.Forms.Panel();
            this.btnCancelGroup = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.lblGroupName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).BeginInit();
            this.panelRecordName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNewGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Volume)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRecordSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBlink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panelNewGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtRecordName
            // 
            this.txtRecordName.Location = new System.Drawing.Point(6, 66);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.Size = new System.Drawing.Size(178, 20);
            this.txtRecordName.TabIndex = 8;
            // 
            // btnAddToTopic
            // 
            this.btnAddToTopic.Location = new System.Drawing.Point(14, 123);
            this.btnAddToTopic.Name = "btnAddToTopic";
            this.btnAddToTopic.Size = new System.Drawing.Size(84, 23);
            this.btnAddToTopic.TabIndex = 9;
            this.btnAddToTopic.Text = "Add to Topic";
            this.btnAddToTopic.UseVisualStyleBackColor = true;
            this.btnAddToTopic.Click += new System.EventHandler(this.btnAddToTopic_Click);
            // 
            // cbRecordings
            // 
            this.cbRecordings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecordings.FormattingEnabled = true;
            this.cbRecordings.Location = new System.Drawing.Point(14, 92);
            this.cbRecordings.Name = "cbRecordings";
            this.cbRecordings.Size = new System.Drawing.Size(178, 21);
            this.cbRecordings.Sorted = true;
            this.cbRecordings.TabIndex = 11;
            // 
            // btnRecord
            // 
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.Location = new System.Drawing.Point(14, 18);
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
            this.btnPlay.Location = new System.Drawing.Point(159, 18);
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
            this.panelRecordName.Controls.Add(this.btnNewGroup);
            this.panelRecordName.Controls.Add(this.cbGroupsSave);
            this.panelRecordName.Controls.Add(this.chAddToTopic);
            this.panelRecordName.Controls.Add(this.chAttachmentSave);
            this.panelRecordName.Controls.Add(this.btnCancelRecord);
            this.panelRecordName.Controls.Add(this.btnSaveRecord);
            this.panelRecordName.Controls.Add(this.lblRecordName);
            this.panelRecordName.Controls.Add(this.txtRecordName);
            this.panelRecordName.Location = new System.Drawing.Point(6, 9);
            this.panelRecordName.Name = "panelRecordName";
            this.panelRecordName.Size = new System.Drawing.Size(190, 185);
            this.panelRecordName.TabIndex = 21;
            this.panelRecordName.Visible = false;
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGroup.Image")));
            this.btnNewGroup.Location = new System.Drawing.Point(164, 20);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(20, 20);
            this.btnNewGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnNewGroup.TabIndex = 123;
            this.btnNewGroup.TabStop = false;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // cbGroupsSave
            // 
            this.cbGroupsSave.FormattingEnabled = true;
            this.cbGroupsSave.Location = new System.Drawing.Point(6, 20);
            this.cbGroupsSave.Name = "cbGroupsSave";
            this.cbGroupsSave.Size = new System.Drawing.Size(155, 21);
            this.cbGroupsSave.Sorted = true;
            this.cbGroupsSave.TabIndex = 83;
            // 
            // chAddToTopic
            // 
            this.chAddToTopic.AutoSize = true;
            this.chAddToTopic.Location = new System.Drawing.Point(8, 102);
            this.chAddToTopic.Name = "chAddToTopic";
            this.chAddToTopic.Size = new System.Drawing.Size(87, 17);
            this.chAddToTopic.TabIndex = 27;
            this.chAddToTopic.Text = "Add to Topic";
            this.chAddToTopic.UseVisualStyleBackColor = true;
            // 
            // chAttachmentSave
            // 
            this.chAttachmentSave.AutoSize = true;
            this.chAttachmentSave.Location = new System.Drawing.Point(98, 102);
            this.chAttachmentSave.Name = "chAttachmentSave";
            this.chAttachmentSave.Size = new System.Drawing.Size(89, 17);
            this.chAttachmentSave.TabIndex = 26;
            this.chAttachmentSave.Text = "+ Attachment";
            this.chAttachmentSave.UseVisualStyleBackColor = true;
            // 
            // btnCancelRecord
            // 
            this.btnCancelRecord.Location = new System.Drawing.Point(109, 146);
            this.btnCancelRecord.Name = "btnCancelRecord";
            this.btnCancelRecord.Size = new System.Drawing.Size(75, 23);
            this.btnCancelRecord.TabIndex = 23;
            this.btnCancelRecord.Text = "Cancel";
            this.btnCancelRecord.UseVisualStyleBackColor = true;
            this.btnCancelRecord.Click += new System.EventHandler(this.btnCancelRecord_Click);
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.Location = new System.Drawing.Point(6, 146);
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRecord.TabIndex = 22;
            this.btnSaveRecord.Text = "Save";
            this.btnSaveRecord.UseVisualStyleBackColor = true;
            this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
            // 
            // lblRecordName
            // 
            this.lblRecordName.AutoSize = true;
            this.lblRecordName.Location = new System.Drawing.Point(3, 48);
            this.lblRecordName.Name = "lblRecordName";
            this.lblRecordName.Size = new System.Drawing.Size(76, 13);
            this.lblRecordName.TabIndex = 9;
            this.lblRecordName.Text = "Record Name:";
            // 
            // Volume
            // 
            this.Volume.AutoSize = false;
            this.Volume.BackColor = System.Drawing.Color.Lavender;
            this.Volume.Location = new System.Drawing.Point(28, 154);
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
            this.chAttachment.Location = new System.Drawing.Point(103, 127);
            this.chAttachment.Name = "chAttachment";
            this.chAttachment.Size = new System.Drawing.Size(89, 17);
            this.chAttachment.TabIndex = 24;
            this.chAttachment.Text = "+ Attachment";
            this.chAttachment.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.o_manage,
            this.o_recordtype,
            this.o_newgroup,
            this.o_help});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 92);
            // 
            // o_manage
            // 
            this.o_manage.Name = "o_manage";
            this.o_manage.Size = new System.Drawing.Size(134, 22);
            this.o_manage.Text = "Manage Audios";
            // 
            // o_recordtype
            // 
            this.o_recordtype.CheckOnClick = true;
            this.o_recordtype.Name = "o_recordtype";
            this.o_recordtype.Size = new System.Drawing.Size(134, 22);
            this.o_recordtype.Tag = "false";
            this.o_recordtype.Text = "System Sound";
            // 
            // o_newgroup
            // 
            this.o_newgroup.Name = "o_newgroup";
            this.o_newgroup.Size = new System.Drawing.Size(134, 22);
            this.o_newgroup.Text = "Add New Group";
            // 
            // o_help
            // 
            this.o_help.Name = "o_help";
            this.o_help.Size = new System.Drawing.Size(134, 22);
            this.o_help.Text = "Help";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 155);
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
            this.aTrack.Location = new System.Drawing.Point(28, 176);
            this.aTrack.Maximum = 10000;
            this.aTrack.Name = "aTrack";
            this.aTrack.Size = new System.Drawing.Size(164, 20);
            this.aTrack.TabIndex = 27;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 177);
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
            this.lblClock.Location = new System.Drawing.Point(63, 47);
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
            // cbGroups
            // 
            this.cbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroups.FormattingEnabled = true;
            this.cbGroups.Location = new System.Drawing.Point(14, 70);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Size = new System.Drawing.Size(178, 21);
            this.cbGroups.Sorted = true;
            this.cbGroups.TabIndex = 82;
            this.cbGroups.SelectedIndexChanged += new System.EventHandler(this.cbGroups_SelectedIndexChanged);
            // 
            // btnMore
            // 
            this.btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMore.Image = ((System.Drawing.Image)(resources.GetObject("btnMore.Image")));
            this.btnMore.Location = new System.Drawing.Point(5, 1);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(16, 16);
            this.btnMore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMore.TabIndex = 94;
            this.btnMore.TabStop = false;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(185, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 93;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelNewGroup
            // 
            this.panelNewGroup.BackColor = System.Drawing.Color.PapayaWhip;
            this.panelNewGroup.Controls.Add(this.btnCancelGroup);
            this.panelNewGroup.Controls.Add(this.btnAddGroup);
            this.panelNewGroup.Controls.Add(this.txtGroupName);
            this.panelNewGroup.Controls.Add(this.lblGroupName);
            this.panelNewGroup.Location = new System.Drawing.Point(6, 67);
            this.panelNewGroup.Name = "panelNewGroup";
            this.panelNewGroup.Size = new System.Drawing.Size(190, 81);
            this.panelNewGroup.TabIndex = 95;
            this.panelNewGroup.Visible = false;
            // 
            // btnCancelGroup
            // 
            this.btnCancelGroup.Location = new System.Drawing.Point(109, 52);
            this.btnCancelGroup.Name = "btnCancelGroup";
            this.btnCancelGroup.Size = new System.Drawing.Size(75, 23);
            this.btnCancelGroup.TabIndex = 3;
            this.btnCancelGroup.Text = "Cancel";
            this.btnCancelGroup.UseVisualStyleBackColor = true;
            this.btnCancelGroup.Click += new System.EventHandler(this.btnCancelGroup_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(6, 52);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(75, 23);
            this.btnAddGroup.TabIndex = 2;
            this.btnAddGroup.Text = "Add";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(6, 24);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(178, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(6, 6);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(70, 13);
            this.lblGroupName.TabIndex = 0;
            this.lblGroupName.Text = "Group Name:";
            // 
            // OmniSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(202, 202);
            this.Controls.Add(this.panelNewGroup);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbGroups);
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
            this.Controls.Add(this.cbRecordings);
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
            ((System.ComponentModel.ISupportInitialize)(this.btnNewGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Volume)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRecordSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBlink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panelNewGroup.ResumeLayout(false);
            this.panelNewGroup.PerformLayout();
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
        public System.Windows.Forms.ComboBox cbRecordings;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.CheckBox chAttachment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem o_recordtype;
        private System.Windows.Forms.ToolStripMenuItem o_help;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar aTrack;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pRecordSystem;
        public System.Windows.Forms.PictureBox pEmpty;
        public System.Windows.Forms.PictureBox pBlink;
        public System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timerStopPlay;
        public System.Windows.Forms.TrackBar Volume;
        public System.Windows.Forms.PictureBox btnPause;
        public System.Windows.Forms.PictureBox btnStop;
        public System.Windows.Forms.ComboBox cbGroups;
        private System.Windows.Forms.PictureBox btnMore;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.ToolStripMenuItem o_manage;
        private System.Windows.Forms.CheckBox chAddToTopic;
        private System.Windows.Forms.CheckBox chAttachmentSave;
        public System.Windows.Forms.ComboBox cbGroupsSave;
        private System.Windows.Forms.ToolStripMenuItem o_newgroup;
        private System.Windows.Forms.Panel panelNewGroup;
        private System.Windows.Forms.Button btnCancelGroup;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.PictureBox btnNewGroup;
    }
}