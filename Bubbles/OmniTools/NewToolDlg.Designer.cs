namespace Bubbles
{
    partial class NewToolDlg
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblSpecifyPath = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 30);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(309, 20);
            this.txtPath.TabIndex = 0;
            this.txtPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyUp);
            this.txtPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtPath_MouseDoubleClick);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(327, 29);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(283, 115);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(199, 115);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblSpecifyPath
            // 
            this.lblSpecifyPath.AutoSize = true;
            this.lblSpecifyPath.Location = new System.Drawing.Point(12, 11);
            this.lblSpecifyPath.Name = "lblSpecifyPath";
            this.lblSpecifyPath.Size = new System.Drawing.Size(254, 13);
            this.lblSpecifyPath.TabIndex = 4;
            this.lblSpecifyPath.Text = "Укажите путь к файлу или вставьте веб-ссылку:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(10, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 13);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Имя инструмента:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(120, 76);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(238, 20);
            this.txtTitle.TabIndex = 14;
            // 
            // lblWait
            // 
            this.lblWait.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWait.Location = new System.Drawing.Point(85, 56);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(201, 13);
            this.lblWait.TabIndex = 19;
            this.lblWait.Text = "Wait...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Имя инструмента:";
            this.label1.Visible = false;
            // 
            // NewToolDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 148);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSpecifyPath);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewToolDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddSourceDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSpecifyPath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.TextBox txtPath;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label label1;
    }
}