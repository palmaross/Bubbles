namespace Bubbles
{
    partial class SourceListDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceListDlg));
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageSize = new System.Windows.Forms.PictureBox();
            this.itemHeight = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MS_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.MS_rename = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemHeight)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(2, 2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(212, 104);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(198, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 7;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageSize
            // 
            this.imageSize.Location = new System.Drawing.Point(169, 37);
            this.imageSize.Name = "imageSize";
            this.imageSize.Size = new System.Drawing.Size(16, 16);
            this.imageSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageSize.TabIndex = 9;
            this.imageSize.TabStop = false;
            this.imageSize.Visible = false;
            // 
            // itemHeight
            // 
            this.itemHeight.Location = new System.Drawing.Point(114, 47);
            this.itemHeight.Name = "itemHeight";
            this.itemHeight.Size = new System.Drawing.Size(5, 14);
            this.itemHeight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.itemHeight.TabIndex = 10;
            this.itemHeight.TabStop = false;
            this.itemHeight.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MS_delete,
            this.MS_rename});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
            // 
            // MS_delete
            // 
            this.MS_delete.Name = "MS_delete";
            this.MS_delete.Size = new System.Drawing.Size(117, 22);
            this.MS_delete.Text = "Delete";
            // 
            // MS_rename
            // 
            this.MS_rename.Name = "MS_rename";
            this.MS_rename.Size = new System.Drawing.Size(117, 22);
            this.MS_rename.Text = "Rename";
            // 
            // SourceListDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(216, 108);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.itemHeight);
            this.Controls.Add(this.imageSize);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SourceListDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FloatManual1";
            this.Load += new System.EventHandler(this.SourceListDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemHeight)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox btnClose;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.PictureBox itemHeight;
        private System.Windows.Forms.PictureBox imageSize;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MS_delete;
        private System.Windows.Forms.ToolStripMenuItem MS_rename;
    }
}