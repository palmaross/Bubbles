namespace Bubbles
{
    partial class AddIconGroupDlg
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
            this.lblGroupName = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.cbMutEx = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(9, 10);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(68, 13);
            this.lblGroupName.TabIndex = 0;
            this.lblGroupName.Text = "Group name:";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(12, 26);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(174, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // cbMutEx
            // 
            this.cbMutEx.AutoSize = true;
            this.cbMutEx.Location = new System.Drawing.Point(12, 53);
            this.cbMutEx.Name = "cbMutEx";
            this.cbMutEx.Size = new System.Drawing.Size(113, 17);
            this.cbMutEx.TabIndex = 2;
            this.cbMutEx.Text = "Mutually Exclusive";
            this.cbMutEx.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(111, 78);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 78);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // AddIconGroupDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(198, 112);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbMutEx);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.lblGroupName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddIconGroupDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AddIconGroupDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox txtGroupName;
        public System.Windows.Forms.CheckBox cbMutEx;
    }
}