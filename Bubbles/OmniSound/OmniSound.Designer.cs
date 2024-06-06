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
            this.lblmin = new System.Windows.Forms.Label();
            this.lblsecond = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.btnAddToTopic = new System.Windows.Forms.Button();
            this.cbRecords = new System.Windows.Forms.ComboBox();
            this.btnRecord = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.PictureBox();
            this.btnPause = new System.Windows.Forms.PictureBox();
            this.btnStopRecord = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDuration = new System.Windows.Forms.Label();
            this.btnStopPlay = new System.Windows.Forms.PictureBox();
            this.pRecord = new System.Windows.Forms.PictureBox();
            this.panelRecordName = new System.Windows.Forms.Panel();
            this.btnCancelRecord = new System.Windows.Forms.Button();
            this.btnSaveRecord = new System.Windows.Forms.Button();
            this.lblRecordName = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.chAttachment = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.o_close = new System.Windows.Forms.ToolStripMenuItem();
            this.o_advanced = new System.Windows.Forms.ToolStripMenuItem();
            this.o_help = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStopRecord)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnStopPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRecord)).BeginInit();
            this.panelRecordName.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblmin
            // 
            this.lblmin.AutoSize = true;
            this.lblmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmin.Location = new System.Drawing.Point(5, 1);
            this.lblmin.Name = "lblmin";
            this.lblmin.Size = new System.Drawing.Size(24, 17);
            this.lblmin.TabIndex = 4;
            this.lblmin.Text = "00";
            // 
            // lblsecond
            // 
            this.lblsecond.AutoSize = true;
            this.lblsecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsecond.Location = new System.Drawing.Point(33, 1);
            this.lblsecond.Name = "lblsecond";
            this.lblsecond.Size = new System.Drawing.Size(24, 17);
            this.lblsecond.TabIndex = 5;
            this.lblsecond.Text = "00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = ":";
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
            this.btnRecord.Size = new System.Drawing.Size(27, 27);
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
            this.btnPlay.Size = new System.Drawing.Size(27, 27);
            this.btnPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPlay.TabIndex = 15;
            this.btnPlay.TabStop = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(88, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(27, 27);
            this.btnPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPause.TabIndex = 16;
            this.btnPause.TabStop = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnStopRecord.Image")));
            this.btnStopRecord.Location = new System.Drawing.Point(55, 12);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(27, 27);
            this.btnStopRecord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnStopRecord.TabIndex = 17;
            this.btnStopRecord.TabStop = false;
            this.btnStopRecord.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDuration);
            this.panel1.Controls.Add(this.lblsecond);
            this.panel1.Controls.Add(this.lblmin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(71, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 19);
            this.panel1.TabIndex = 18;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(53, 1);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(52, 17);
            this.lblDuration.TabIndex = 7;
            this.lblDuration.Text = "/ 00:00";
            this.lblDuration.Visible = false;
            // 
            // btnStopPlay
            // 
            this.btnStopPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnStopPlay.Image")));
            this.btnStopPlay.Location = new System.Drawing.Point(127, 12);
            this.btnStopPlay.Name = "btnStopPlay";
            this.btnStopPlay.Size = new System.Drawing.Size(27, 27);
            this.btnStopPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnStopPlay.TabIndex = 19;
            this.btnStopPlay.TabStop = false;
            this.btnStopPlay.Visible = false;
            // 
            // pRecord
            // 
            this.pRecord.Image = ((System.Drawing.Image)(resources.GetObject("pRecord.Image")));
            this.pRecord.Location = new System.Drawing.Point(52, 48);
            this.pRecord.Name = "pRecord";
            this.pRecord.Size = new System.Drawing.Size(13, 13);
            this.pRecord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRecord.TabIndex = 20;
            this.pRecord.TabStop = false;
            this.pRecord.Visible = false;
            // 
            // panelRecordName
            // 
            this.panelRecordName.BackColor = System.Drawing.Color.PapayaWhip;
            this.panelRecordName.Controls.Add(this.btnCancelRecord);
            this.panelRecordName.Controls.Add(this.btnSaveRecord);
            this.panelRecordName.Controls.Add(this.lblRecordName);
            this.panelRecordName.Controls.Add(this.txtRecordName);
            this.panelRecordName.Location = new System.Drawing.Point(8, 11);
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
            this.o_close,
            this.o_advanced,
            this.o_help});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 70);
            // 
            // o_close
            // 
            this.o_close.Name = "o_close";
            this.o_close.Size = new System.Drawing.Size(155, 22);
            this.o_close.Text = "Close";
            // 
            // o_advanced
            // 
            this.o_advanced.Name = "o_advanced";
            this.o_advanced.Size = new System.Drawing.Size(155, 22);
            this.o_advanced.Text = "Advanced";
            // 
            // o_help
            // 
            this.o_help.Name = "o_help";
            this.o_help.Size = new System.Drawing.Size(155, 22);
            this.o_help.Text = "Help";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // OmniSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(202, 138);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.chAttachment);
            this.Controls.Add(this.pRecord);
            this.Controls.Add(this.btnStopPlay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStopRecord);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnAddToTopic);
            this.Controls.Add(this.cbRecords);
            this.Controls.Add(this.panelRecordName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OmniSound";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OmniSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OmniSound_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OmniSound_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStopRecord)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnStopPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRecord)).EndInit();
            this.panelRecordName.ResumeLayout(false);
            this.panelRecordName.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblmin;
        private System.Windows.Forms.Label lblsecond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Button btnAddToTopic;
        private System.Windows.Forms.PictureBox btnRecord;
        private System.Windows.Forms.PictureBox btnPlay;
        private System.Windows.Forms.PictureBox btnPause;
        private System.Windows.Forms.PictureBox btnStopRecord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnStopPlay;
        private System.Windows.Forms.PictureBox pRecord;
        private System.Windows.Forms.Panel panelRecordName;
        private System.Windows.Forms.Label lblRecordName;
        private System.Windows.Forms.Button btnSaveRecord;
        private System.Windows.Forms.Button btnCancelRecord;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ComboBox cbRecords;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.CheckBox chAttachment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem o_close;
        private System.Windows.Forms.ToolStripMenuItem o_advanced;
        private System.Windows.Forms.ToolStripMenuItem o_help;
        private System.Windows.Forms.Timer timer2;
    }
}