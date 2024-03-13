namespace Bubbles
{
    partial class MMOutToogle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MMOutToogle));
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pToggle = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CM_Close = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pToggle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            this.cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // pToggle
            // 
            this.pToggle.Image = ((System.Drawing.Image)(resources.GetObject("pToggle.Image")));
            this.pToggle.Location = new System.Drawing.Point(23, 3);
            this.pToggle.Name = "pToggle";
            this.pToggle.Size = new System.Drawing.Size(32, 14);
            this.pToggle.TabIndex = 0;
            this.pToggle.TabStop = false;
            this.pToggle.Click += new System.EventHandler(this.pToggle_Click);
            // 
            // pictureHandle
            // 
            this.pictureHandle.Image = ((System.Drawing.Image)(resources.GetObject("pictureHandle.Image")));
            this.pictureHandle.Location = new System.Drawing.Point(2, 1);
            this.pictureHandle.Name = "pictureHandle";
            this.pictureHandle.Size = new System.Drawing.Size(18, 18);
            this.pictureHandle.TabIndex = 1;
            this.pictureHandle.TabStop = false;
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CM_Close});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(104, 26);
            // 
            // CM_Close
            // 
            this.CM_Close.Name = "CM_Close";
            this.CM_Close.Size = new System.Drawing.Size(103, 22);
            this.CM_Close.Text = "Close";
            // 
            // MMOutToogle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(56, 20);
            this.ControlBox = false;
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.pToggle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MMOutToogle";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MMOutToogle";
            ((System.ComponentModel.ISupportInitialize)(this.pToggle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pToggle;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem CM_Close;
    }
}