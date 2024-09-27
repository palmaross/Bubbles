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
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblToolName = new System.Windows.Forms.Label();
            this.lblTooltip = new System.Windows.Forms.Label();
            this.txtTooltip = new System.Windows.Forms.TextBox();
            this.pIcon = new System.Windows.Forms.PictureBox();
            this.lblToolIcon = new System.Windows.Forms.Label();
            this.lblChangeIcon = new System.Windows.Forms.Label();
            this.chChangeInDataBase = new System.Windows.Forms.CheckBox();
            this.btnPages = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(11, 135);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(91, 6);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(220, 21);
            this.txtTitle.TabIndex = 5;
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
            this.lblTooltip.Location = new System.Drawing.Point(8, 65);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(209, 13);
            this.lblTooltip.TabIndex = 33;
            this.lblTooltip.Text = "Tooltip when  the mouse hover over a tool:";
            // 
            // txtTooltip
            // 
            this.txtTooltip.Location = new System.Drawing.Point(10, 82);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(302, 20);
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
            // lblChangeIcon
            // 
            this.lblChangeIcon.AutoSize = true;
            this.lblChangeIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeIcon.Location = new System.Drawing.Point(116, 41);
            this.lblChangeIcon.Name = "lblChangeIcon";
            this.lblChangeIcon.Size = new System.Drawing.Size(169, 13);
            this.lblChangeIcon.TabIndex = 40;
            this.lblChangeIcon.Text = "Click icon if you want to change it.";
            // 
            // chChangeInDataBase
            // 
            this.chChangeInDataBase.AutoSize = true;
            this.chChangeInDataBase.Checked = true;
            this.chChangeInDataBase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chChangeInDataBase.Location = new System.Drawing.Point(12, 110);
            this.chChangeInDataBase.Name = "chChangeInDataBase";
            this.chChangeInDataBase.Size = new System.Drawing.Size(153, 17);
            this.chChangeInDataBase.TabIndex = 51;
            this.chChangeInDataBase.Text = "Изменить в базе данных";
            this.chChangeInDataBase.UseVisualStyleBackColor = true;
            // 
            // btnPages
            // 
            this.btnPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPages.BackColor = System.Drawing.SystemColors.Info;
            this.btnPages.Location = new System.Drawing.Point(119, 135);
            this.btnPages.Name = "btnPages";
            this.btnPages.Size = new System.Drawing.Size(75, 23);
            this.btnPages.TabIndex = 52;
            this.btnPages.Text = "Pages";
            this.btnPages.UseVisualStyleBackColor = false;
            this.btnPages.Visible = false;
            // 
            // EditToolDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(326, 168);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPages);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chChangeInDataBase);
            this.Controls.Add(this.lblChangeIcon);
            this.Controls.Add(this.pIcon);
            this.Controls.Add(this.lblToolIcon);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.txtTitle);
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
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblToolName;
        private System.Windows.Forms.Label lblTooltip;
        public System.Windows.Forms.TextBox txtTooltip;
        private System.Windows.Forms.Label lblToolIcon;
        public System.Windows.Forms.PictureBox pIcon;
        private System.Windows.Forms.Label lblChangeIcon;
        private System.Windows.Forms.Button btnPages;
        public System.Windows.Forms.CheckBox chChangeInDataBase;
    }
}