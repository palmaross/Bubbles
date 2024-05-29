namespace Bubbles
{
    partial class CloseMapsDlg
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
            this.chActiveMap = new System.Windows.Forms.CheckBox();
            this.rbtnSaveMap = new System.Windows.Forms.RadioButton();
            this.rbtnAskMe = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chActiveMap
            // 
            this.chActiveMap.AutoSize = true;
            this.chActiveMap.Checked = true;
            this.chActiveMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chActiveMap.Location = new System.Drawing.Point(14, 76);
            this.chActiveMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chActiveMap.Name = "chActiveMap";
            this.chActiveMap.Size = new System.Drawing.Size(156, 19);
            this.chActiveMap.TabIndex = 0;
            this.chActiveMap.Text = "Do not close active map";
            this.chActiveMap.UseVisualStyleBackColor = true;
            // 
            // rbtnSaveMap
            // 
            this.rbtnSaveMap.AutoSize = true;
            this.rbtnSaveMap.Checked = true;
            this.rbtnSaveMap.Location = new System.Drawing.Point(14, 12);
            this.rbtnSaveMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbtnSaveMap.Name = "rbtnSaveMap";
            this.rbtnSaveMap.Size = new System.Drawing.Size(242, 19);
            this.rbtnSaveMap.TabIndex = 2;
            this.rbtnSaveMap.TabStop = true;
            this.rbtnSaveMap.Text = "If map is modified, save it before closing";
            this.rbtnSaveMap.UseVisualStyleBackColor = true;
            // 
            // rbtnAskMe
            // 
            this.rbtnAskMe.AutoSize = true;
            this.rbtnAskMe.Location = new System.Drawing.Point(14, 39);
            this.rbtnAskMe.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbtnAskMe.Name = "rbtnAskMe";
            this.rbtnAskMe.Size = new System.Drawing.Size(227, 19);
            this.rbtnAskMe.TabIndex = 3;
            this.rbtnAskMe.Text = "If map is modified, ask me what to do";
            this.rbtnAskMe.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(14, 112);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(221, 112);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // CloseMapsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(309, 146);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rbtnAskMe);
            this.Controls.Add(this.rbtnSaveMap);
            this.Controls.Add(this.chActiveMap);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloseMapsDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Close all Maps";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chActiveMap;
        private System.Windows.Forms.RadioButton rbtnSaveMap;
        private System.Windows.Forms.RadioButton rbtnAskMe;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}