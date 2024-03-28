namespace Bubbles
{
    partial class OmniButton
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
            this.OmniStix = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OmniStix
            // 
            this.OmniStix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OmniStix.AutoSize = true;
            this.OmniStix.BackColor = System.Drawing.Color.LightSeaGreen;
            this.OmniStix.Font = new System.Drawing.Font("Comic Sans MS", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OmniStix.ForeColor = System.Drawing.Color.Ivory;
            this.OmniStix.Location = new System.Drawing.Point(34, -2);
            this.OmniStix.Name = "OmniStix";
            this.OmniStix.Size = new System.Drawing.Size(67, 19);
            this.OmniStix.TabIndex = 0;
            this.OmniStix.Text = "OmniStix";
            // 
            // Rounded
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(117, 17);
            this.Controls.Add(this.OmniStix);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Rounded";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Rounded";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OmniStix;
    }
}