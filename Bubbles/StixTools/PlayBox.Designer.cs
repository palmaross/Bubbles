namespace Bubbles
{
    partial class PlayBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayBox));
            this.btnStopPlay = new System.Windows.Forms.PictureBox();
            this.btnPause = new System.Windows.Forms.PictureBox();
            this.lblTrack = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblsecond = new System.Windows.Forms.Label();
            this.lblmin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnStopPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStopPlay
            // 
            this.btnStopPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnStopPlay.Image")));
            this.btnStopPlay.Location = new System.Drawing.Point(142, 22);
            this.btnStopPlay.Name = "btnStopPlay";
            this.btnStopPlay.Size = new System.Drawing.Size(24, 24);
            this.btnStopPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnStopPlay.TabIndex = 21;
            this.btnStopPlay.TabStop = false;
            this.btnStopPlay.Click += new System.EventHandler(this.btnStopPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(5, 22);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(24, 24);
            this.btnPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnPause.TabIndex = 20;
            this.btnPause.TabStop = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lblTrack
            // 
            this.lblTrack.AutoEllipsis = true;
            this.lblTrack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrack.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTrack.Location = new System.Drawing.Point(0, 2);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(170, 15);
            this.lblTrack.TabIndex = 23;
            this.lblTrack.Text = "Название трека сколько будет";
            this.lblTrack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTrack.Click += new System.EventHandler(this.lblTrack_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDuration);
            this.panel1.Controls.Add(this.lblsecond);
            this.panel1.Controls.Add(this.lblmin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(33, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 19);
            this.panel1.TabIndex = 24;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.ForeColor = System.Drawing.Color.Indigo;
            this.lblDuration.Location = new System.Drawing.Point(52, 1);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(48, 16);
            this.lblDuration.TabIndex = 7;
            this.lblDuration.Text = "/  00:00";
            // 
            // lblsecond
            // 
            this.lblsecond.AutoSize = true;
            this.lblsecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsecond.ForeColor = System.Drawing.Color.Indigo;
            this.lblsecond.Location = new System.Drawing.Point(33, 1);
            this.lblsecond.Name = "lblsecond";
            this.lblsecond.Size = new System.Drawing.Size(21, 16);
            this.lblsecond.TabIndex = 5;
            this.lblsecond.Text = "00";
            // 
            // lblmin
            // 
            this.lblmin.AutoSize = true;
            this.lblmin.BackColor = System.Drawing.Color.Transparent;
            this.lblmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmin.ForeColor = System.Drawing.Color.Indigo;
            this.lblmin.Location = new System.Drawing.Point(5, 1);
            this.lblmin.Name = "lblmin";
            this.lblmin.Size = new System.Drawing.Size(21, 16);
            this.lblmin.TabIndex = 4;
            this.lblmin.Text = "00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Indigo;
            this.label1.Location = new System.Drawing.Point(25, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = ":";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PlayBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(170, 50);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStopPlay);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblTrack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PlayBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PlayBox";
            this.Load += new System.EventHandler(this.PlayBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnStopPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPause)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btnStopPlay;
        private System.Windows.Forms.PictureBox btnPause;
        public System.Windows.Forms.Label lblTrack;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblsecond;
        private System.Windows.Forms.Label lblmin;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label lblDuration;
    }
}