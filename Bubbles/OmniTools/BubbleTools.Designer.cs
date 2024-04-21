namespace Bubbles
{
    partial class BubbleTools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubbleTools));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmsManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.ToolList = new System.Windows.Forms.PictureBox();
            this.pIconDist = new System.Windows.Forms.PictureBox();
            this.cmsTool = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TM_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_changeicon = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_delete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIconDist)).BeginInit();
            this.cmsTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // cmsManage
            // 
            this.cmsManage.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsManage.Name = "contextMenuStrip1";
            this.cmsManage.Size = new System.Drawing.Size(181, 26);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 74;
            this.label1.Text = "высота22";
            this.label1.Visible = false;
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(27, 7);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 75;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(117, 5);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(20, 20);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 77;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // pictureHandle
            // 
            this.pictureHandle.BackColor = System.Drawing.Color.Transparent;
            this.pictureHandle.Image = ((System.Drawing.Image)(resources.GetObject("pictureHandle.Image")));
            this.pictureHandle.Location = new System.Drawing.Point(0, 0);
            this.pictureHandle.Name = "pictureHandle";
            this.pictureHandle.Size = new System.Drawing.Size(24, 24);
            this.pictureHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureHandle.TabIndex = 77;
            this.pictureHandle.TabStop = false;
            // 
            // ToolList
            // 
            this.ToolList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ToolList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ToolList.Image = ((System.Drawing.Image)(resources.GetObject("ToolList.Image")));
            this.ToolList.Location = new System.Drawing.Point(93, 5);
            this.ToolList.Name = "ToolList";
            this.ToolList.Size = new System.Drawing.Size(20, 20);
            this.ToolList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ToolList.TabIndex = 79;
            this.ToolList.TabStop = false;
            this.ToolList.MouseHover += new System.EventHandler(this.ToolList_Click);
            // 
            // pIconDist
            // 
            this.pIconDist.Location = new System.Drawing.Point(60, 7);
            this.pIconDist.Name = "pIconDist";
            this.pIconDist.Size = new System.Drawing.Size(22, 16);
            this.pIconDist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIconDist.TabIndex = 100;
            this.pIconDist.TabStop = false;
            this.pIconDist.Visible = false;
            // 
            // cmsTool
            // 
            this.cmsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TM_rename,
            this.TM_changeicon,
            this.TM_delete});
            this.cmsTool.Name = "cmsManage";
            this.cmsTool.Size = new System.Drawing.Size(162, 70);
            // 
            // TM_rename
            // 
            this.TM_rename.Name = "TM_rename";
            this.TM_rename.Size = new System.Drawing.Size(161, 22);
            this.TM_rename.Text = "Переименовать";
            // 
            // TM_changeicon
            // 
            this.TM_changeicon.Name = "TM_changeicon";
            this.TM_changeicon.Size = new System.Drawing.Size(161, 22);
            this.TM_changeicon.Text = "Change Icon";
            // 
            // TM_delete
            // 
            this.TM_delete.Name = "TM_delete";
            this.TM_delete.Size = new System.Drawing.Size(161, 22);
            this.TM_delete.Text = "Удалить";
            // 
            // BubbleTools
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(142, 30);
            this.ControlBox = false;
            this.Controls.Add(this.pIconDist);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.ToolList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureHandle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubbleTools";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIconDist)).EndInit();
            this.cmsTool.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmsManage;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.PictureBox ToolList;
        private System.Windows.Forms.PictureBox pIconDist;
        private System.Windows.Forms.ContextMenuStrip cmsTool;
        private System.Windows.Forms.ToolStripMenuItem TM_rename;
        private System.Windows.Forms.ToolStripMenuItem TM_changeicon;
        private System.Windows.Forms.ToolStripMenuItem TM_delete;
    }
}