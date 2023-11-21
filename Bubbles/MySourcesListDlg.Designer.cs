namespace Bubbles
{
    partial class MySourcesListDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MySourcesListDlg));
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.p2 = new System.Windows.Forms.PictureBox();
            this.itemHeight = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(1, 1);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 105);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(224, 0);
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
            this.imageList1.ImageSize = new System.Drawing.Size(12, 12);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // p2
            // 
            this.p2.Location = new System.Drawing.Point(169, 37);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(12, 12);
            this.p2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p2.TabIndex = 9;
            this.p2.TabStop = false;
            this.p2.Visible = false;
            // 
            // itemHeight
            // 
            this.itemHeight.Location = new System.Drawing.Point(114, 47);
            this.itemHeight.Name = "itemHeight";
            this.itemHeight.Size = new System.Drawing.Size(14, 14);
            this.itemHeight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.itemHeight.TabIndex = 10;
            this.itemHeight.TabStop = false;
            this.itemHeight.Visible = false;
            // 
            // MySourcesListDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(240, 107);
            this.Controls.Add(this.itemHeight);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MySourcesListDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FloatManual1";
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox btnClose;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.PictureBox itemHeight;
        private System.Windows.Forms.PictureBox p2;
    }
}