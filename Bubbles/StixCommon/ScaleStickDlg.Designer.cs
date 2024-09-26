namespace Bubbles
{
    partial class ScaleStickDlg
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
            this.numSfactor = new System.Windows.Forms.MaskedTextBox();
            this.cbSfactor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // numSfactor
            // 
            this.numSfactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSfactor.Location = new System.Drawing.Point(7, 6);
            this.numSfactor.Mask = "000%";
            this.numSfactor.Name = "numSfactor";
            this.numSfactor.Size = new System.Drawing.Size(39, 21);
            this.numSfactor.TabIndex = 12;
            this.numSfactor.Text = "100";
            this.numSfactor.ValidatingType = typeof(int);
            this.numSfactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numSfactor_KeyDown);
            // 
            // cbSfactor
            // 
            this.cbSfactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSfactor.FormattingEnabled = true;
            this.cbSfactor.Items.AddRange(new object[] {
            "100%",
            "125%",
            "150%",
            "200%",
            "250%",
            "300%"});
            this.cbSfactor.Location = new System.Drawing.Point(6, 5);
            this.cbSfactor.Name = "cbSfactor";
            this.cbSfactor.Size = new System.Drawing.Size(56, 23);
            this.cbSfactor.TabIndex = 13;
            this.cbSfactor.SelectedIndexChanged += new System.EventHandler(this.cbStixBase_SelectedIndexChanged);
            // 
            // ScaleStickDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(67, 30);
            this.Controls.Add(this.numSfactor);
            this.Controls.Add(this.cbSfactor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScaleStickDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ScaleStickDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox numSfactor;
        private System.Windows.Forms.ComboBox cbSfactor;
    }
}