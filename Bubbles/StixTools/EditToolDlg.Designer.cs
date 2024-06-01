namespace Bubbles
{
    partial class EditToolDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditToolDlg));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtToolName = new System.Windows.Forms.TextBox();
            this.lblToolName = new System.Windows.Forms.Label();
            this.lblTooltip = new System.Windows.Forms.Label();
            this.txtTooltip = new System.Windows.Forms.TextBox();
            this.pIcon = new System.Windows.Forms.PictureBox();
            this.lblToolIcon = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(11, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(221, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtToolName
            // 
            this.txtToolName.Location = new System.Drawing.Point(91, 6);
            this.txtToolName.Name = "txtToolName";
            this.txtToolName.Size = new System.Drawing.Size(205, 20);
            this.txtToolName.TabIndex = 5;
            // 
            // lblToolName
            // 
            this.lblToolName.AutoSize = true;
            this.lblToolName.Location = new System.Drawing.Point(7, 9);
            this.lblToolName.Name = "lblToolName";
            this.lblToolName.Size = new System.Drawing.Size(62, 13);
            this.lblToolName.TabIndex = 4;
            this.lblToolName.Text = "Tool Name:";
            // 
            // lblTooltip
            // 
            this.lblTooltip.AutoSize = true;
            this.lblTooltip.Location = new System.Drawing.Point(8, 66);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(209, 13);
            this.lblTooltip.TabIndex = 33;
            this.lblTooltip.Text = "Tooltip when  the mouse hover over a tool:";
            // 
            // txtTooltip
            // 
            this.txtTooltip.Location = new System.Drawing.Point(10, 82);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(286, 20);
            this.txtTooltip.TabIndex = 32;
            // 
            // pIcon
            // 
            this.pIcon.Image = ((System.Drawing.Image)(resources.GetObject("pIcon.Image")));
            this.pIcon.Location = new System.Drawing.Point(91, 37);
            this.pIcon.Name = "pIcon";
            this.pIcon.Size = new System.Drawing.Size(20, 20);
            this.pIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIcon.TabIndex = 37;
            this.pIcon.TabStop = false;
            this.pIcon.Click += new System.EventHandler(this.pIcon_Click);
            // 
            // lblToolIcon
            // 
            this.lblToolIcon.AutoSize = true;
            this.lblToolIcon.Location = new System.Drawing.Point(7, 41);
            this.lblToolIcon.Name = "lblToolIcon";
            this.lblToolIcon.Size = new System.Drawing.Size(81, 13);
            this.lblToolIcon.TabIndex = 36;
            this.lblToolIcon.Text = "Значок инстр.:";
            // 
            // EditToolDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(306, 144);
            this.Controls.Add(this.pIcon);
            this.Controls.Add(this.lblToolIcon);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtToolName);
            this.Controls.Add(this.lblToolName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditToolDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EditToolDlg";
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox txtToolName;
        private System.Windows.Forms.Label lblToolName;
        private System.Windows.Forms.Label lblTooltip;
        public System.Windows.Forms.TextBox txtTooltip;
        private System.Windows.Forms.PictureBox pIcon;
        private System.Windows.Forms.Label lblToolIcon;
    }
}