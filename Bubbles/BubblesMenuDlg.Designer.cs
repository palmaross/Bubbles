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
            this.PasteBubble = new System.Windows.Forms.LinkLabel();
            this.linkIcons = new System.Windows.Forms.LinkLabel();
            this.ClipBoard = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkSettings = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // PasteBubble
            // 
            this.PasteBubble.AutoSize = true;
            this.PasteBubble.Location = new System.Drawing.Point(12, 9);
            this.PasteBubble.Name = "PasteBubble";
            this.PasteBubble.Size = new System.Drawing.Size(94, 13);
            this.PasteBubble.TabIndex = 0;
            this.PasteBubble.TabStop = true;
            this.PasteBubble.Text = "Режимы вставки";
            this.PasteBubble.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PasteBubble_LinkClicked);
            // 
            // linkIcons
            // 
            this.linkIcons.AutoSize = true;
            this.linkIcons.Location = new System.Drawing.Point(12, 41);
            this.linkIcons.Name = "linkIcons";
            this.linkIcons.Size = new System.Drawing.Size(43, 13);
            this.linkIcons.TabIndex = 1;
            this.linkIcons.TabStop = true;
            this.linkIcons.Text = "Значки";
            this.linkIcons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkIcons_LinkClicked);
            // 
            // ClipBoard
            // 
            this.ClipBoard.AutoSize = true;
            this.ClipBoard.Location = new System.Drawing.Point(12, 25);
            this.ClipBoard.Name = "ClipBoard";
            this.ClipBoard.Size = new System.Drawing.Size(80, 13);
            this.ClipBoard.TabIndex = 2;
            this.ClipBoard.TabStop = true;
            this.ClipBoard.Text = "Буфер обмена";
            this.ClipBoard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ClipBoard_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 41);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 13);
            this.linkLabel1.TabIndex = 3;
            // 
            // linkSettings
            // 
            this.linkSettings.AutoSize = true;
            this.linkSettings.Location = new System.Drawing.Point(12, 56);
            this.linkSettings.Name = "linkSettings";
            this.linkSettings.Size = new System.Drawing.Size(62, 13);
            this.linkSettings.TabIndex = 4;
            this.linkSettings.TabStop = true;
            this.linkSettings.Text = "Настройки";
            this.linkSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSettings_LinkClicked);
            // 
            // BubblesMenuDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 88);
            this.ControlBox = false;
            this.Controls.Add(this.linkSettings);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ClipBoard);
            this.Controls.Add(this.linkIcons);
            this.Controls.Add(this.PasteBubble);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BubblesMenuDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "BubblesMenuDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel PasteBubble;
        private System.Windows.Forms.LinkLabel linkIcons;
        private System.Windows.Forms.LinkLabel ClipBoard;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkSettings;
    }
}