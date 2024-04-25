namespace Bubbles
{
    partial class NewLinkDlg
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
            this.lblResult = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.lblLink = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grBoxDownload = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.cbDownload = new System.Windows.Forms.CheckBox();
            this.grBoxDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblResult.Location = new System.Drawing.Point(106, 171);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(265, 15);
            this.lblResult.TabIndex = 32;
            this.lblResult.Text = "Link added successfully";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult.Visible = false;
            // 
            // lblWait
            // 
            this.lblWait.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWait.Location = new System.Drawing.Point(161, 44);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(201, 13);
            this.lblWait.TabIndex = 31;
            this.lblWait.Text = "Wait...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(25, 167);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 30;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(377, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(87, 18);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(365, 20);
            this.txtLink.TabIndex = 29;
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(24, 21);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(30, 13);
            this.lblLink.TabIndex = 28;
            this.lblLink.Text = "Link:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(87, 64);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(365, 20);
            this.txtTitle.TabIndex = 27;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(24, 67);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 26;
            this.lblTitle.Text = "Title:";
            // 
            // grBoxDownload
            // 
            this.grBoxDownload.Controls.Add(this.btnPreview);
            this.grBoxDownload.Controls.Add(this.cbDownload);
            this.grBoxDownload.Location = new System.Drawing.Point(87, 102);
            this.grBoxDownload.Name = "grBoxDownload";
            this.grBoxDownload.Size = new System.Drawing.Size(365, 52);
            this.grBoxDownload.TabIndex = 33;
            this.grBoxDownload.TabStop = false;
            this.grBoxDownload.Text = "You can download page and add link to the downloaded file";
            this.grBoxDownload.Visible = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(206, 20);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(108, 23);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // cbDownload
            // 
            this.cbDownload.AutoSize = true;
            this.cbDownload.Location = new System.Drawing.Point(52, 23);
            this.cbDownload.Name = "cbDownload";
            this.cbDownload.Size = new System.Drawing.Size(116, 17);
            this.cbDownload.TabIndex = 20;
            this.cbDownload.Text = "Скачать страницу";
            this.cbDownload.UseVisualStyleBackColor = true;
            // 
            // NewLinkDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 208);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.lblLink);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grBoxDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLinkDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewLinkDlg";
            this.grBoxDownload.ResumeLayout(false);
            this.grBoxDownload.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grBoxDownload;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.CheckBox cbDownload;
    }
}