namespace Bubbles
{
    partial class SelectImageDlg
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pSpace = new System.Windows.Forms.PictureBox();
            this.pImage = new System.Windows.Forms.PictureBox();
            this.pIcon = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 13);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(173, 294);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pIcon);
            this.panel1.Controls.Add(this.pSpace);
            this.panel1.Controls.Add(this.pImage);
            this.panel1.Location = new System.Drawing.Point(196, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 294);
            this.panel1.TabIndex = 1;
            // 
            // pSpace
            // 
            this.pSpace.Location = new System.Drawing.Point(86, 88);
            this.pSpace.Name = "pSpace";
            this.pSpace.Size = new System.Drawing.Size(10, 10);
            this.pSpace.TabIndex = 1;
            this.pSpace.TabStop = false;
            this.pSpace.Visible = false;
            // 
            // pImage
            // 
            this.pImage.Location = new System.Drawing.Point(3, 117);
            this.pImage.Name = "pImage";
            this.pImage.Size = new System.Drawing.Size(32, 32);
            this.pImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pImage.TabIndex = 0;
            this.pImage.TabStop = false;
            this.pImage.Visible = false;
            // 
            // pIcon
            // 
            this.pIcon.Location = new System.Drawing.Point(41, 133);
            this.pIcon.Name = "pIcon";
            this.pIcon.Size = new System.Drawing.Size(16, 16);
            this.pIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIcon.TabIndex = 2;
            this.pIcon.TabStop = false;
            this.pIcon.Visible = false;
            // 
            // SelectImageDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(472, 319);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectImageDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Icon";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pSpace;
        private System.Windows.Forms.PictureBox pImage;
        private System.Windows.Forms.PictureBox pIcon;
    }
}