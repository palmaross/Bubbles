namespace Bubbles
{
    partial class AddSourceDlg
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
            this.rbtnBegin = new System.Windows.Forms.RadioButton();
            this.rbtnEnd = new System.Windows.Forms.RadioButton();
            this.rbtnRight = new System.Windows.Forms.RadioButton();
            this.rbtnLeft = new System.Windows.Forms.RadioButton();
            this.lblInsertSource = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblInsertSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 28);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(309, 20);
            this.txtPath.TabIndex = 0;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(327, 27);
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
            this.btnCancel.Location = new System.Drawing.Point(283, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(199, 144);
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
            this.lblSpecifyPath.Location = new System.Drawing.Point(12, 9);
            this.lblSpecifyPath.Name = "lblSpecifyPath";
            this.lblSpecifyPath.Size = new System.Drawing.Size(274, 13);
            this.lblSpecifyPath.TabIndex = 4;
            this.lblSpecifyPath.Text = "Укажите путь к источнику или вставьте веб-ссылку:";
            // 
            // rbtnBegin
            // 
            this.rbtnBegin.AutoSize = true;
            this.rbtnBegin.Location = new System.Drawing.Point(174, 18);
            this.rbtnBegin.Name = "rbtnBegin";
            this.rbtnBegin.Size = new System.Drawing.Size(70, 17);
            this.rbtnBegin.TabIndex = 11;
            this.rbtnBegin.Text = "В начало";
            this.rbtnBegin.UseVisualStyleBackColor = true;
            // 
            // rbtnEnd
            // 
            this.rbtnEnd.AutoSize = true;
            this.rbtnEnd.Checked = true;
            this.rbtnEnd.Location = new System.Drawing.Point(271, 18);
            this.rbtnEnd.Name = "rbtnEnd";
            this.rbtnEnd.Size = new System.Drawing.Size(65, 17);
            this.rbtnEnd.TabIndex = 10;
            this.rbtnEnd.TabStop = true;
            this.rbtnEnd.Text = "В конец";
            this.rbtnEnd.UseVisualStyleBackColor = true;
            // 
            // rbtnRight
            // 
            this.rbtnRight.AutoSize = true;
            this.rbtnRight.Location = new System.Drawing.Point(92, 18);
            this.rbtnRight.Name = "rbtnRight";
            this.rbtnRight.Size = new System.Drawing.Size(62, 17);
            this.rbtnRight.TabIndex = 9;
            this.rbtnRight.Text = "Справа";
            this.rbtnRight.UseVisualStyleBackColor = true;
            // 
            // rbtnLeft
            // 
            this.rbtnLeft.AutoSize = true;
            this.rbtnLeft.Location = new System.Drawing.Point(9, 18);
            this.rbtnLeft.Name = "rbtnLeft";
            this.rbtnLeft.Size = new System.Drawing.Size(56, 17);
            this.rbtnLeft.TabIndex = 8;
            this.rbtnLeft.Text = "Слева";
            this.rbtnLeft.UseVisualStyleBackColor = true;
            // 
            // lblInsertSource
            // 
            this.lblInsertSource.Controls.Add(this.rbtnEnd);
            this.lblInsertSource.Controls.Add(this.rbtnBegin);
            this.lblInsertSource.Controls.Add(this.rbtnLeft);
            this.lblInsertSource.Controls.Add(this.rbtnRight);
            this.lblInsertSource.Location = new System.Drawing.Point(12, 90);
            this.lblInsertSource.Name = "lblInsertSource";
            this.lblInsertSource.Size = new System.Drawing.Size(346, 42);
            this.lblInsertSource.TabIndex = 12;
            this.lblInsertSource.TabStop = false;
            this.lblInsertSource.Text = "Вставить источник";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(10, 61);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(115, 13);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Название источника:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(130, 59);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(228, 20);
            this.txtTitle.TabIndex = 14;
            // 
            // AddSourceDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 177);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSpecifyPath);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblInsertSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSourceDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddSourceDlg";
            this.lblInsertSource.ResumeLayout(false);
            this.lblInsertSource.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSpecifyPath;
        public System.Windows.Forms.RadioButton rbtnBegin;
        public System.Windows.Forms.RadioButton rbtnEnd;
        public System.Windows.Forms.RadioButton rbtnRight;
        public System.Windows.Forms.RadioButton rbtnLeft;
        private System.Windows.Forms.GroupBox lblInsertSource;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.TextBox txtPath;
        public System.Windows.Forms.TextBox txtTitle;
    }
}