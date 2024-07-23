namespace Bubbles
{
	partial class ProgressDlg
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
            this.Message = new System.Windows.Forms.Label();
            this.pbTopics = new System.Windows.Forms.ProgressBar();
            this.btnAbort = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Location = new System.Drawing.Point(11, 10);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(50, 13);
            this.Message.TabIndex = 0;
            this.Message.Text = "Message";
            // 
            // pbTopics
            // 
            this.pbTopics.Location = new System.Drawing.Point(0, 0);
            this.pbTopics.Name = "pbTopics";
            this.pbTopics.Size = new System.Drawing.Size(232, 20);
            this.pbTopics.Step = 1;
            this.pbTopics.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbTopics.TabIndex = 1;
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(255, 0);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(87, 21);
            this.btnAbort.TabIndex = 2;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnAbort);
            this.panel1.Controls.Add(this.pbTopics);
            this.panel1.Location = new System.Drawing.Point(12, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 25);
            this.panel1.TabIndex = 3;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(87, 10);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 13);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "_";
            // 
            // ProgressDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(366, 81);
            this.ControlBox = false;
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Message);
            this.Location = new System.Drawing.Point(300, 300);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Dashboard";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion

		public System.Windows.Forms.Label Message;
		public System.Windows.Forms.ProgressBar pbTopics;
		public int m_links = 0;
		public int m_brokenlinks = 0;
		public System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblCount;
    }
}