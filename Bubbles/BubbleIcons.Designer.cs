﻿namespace Bubbles
{
    partial class BubbleIcons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubbleIcons));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BI_new = new System.Windows.Forms.ToolStripMenuItem();
            this.BI_removeallfromtopic = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.cmsIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BI_newicon = new System.Windows.Forms.ToolStripMenuItem();
            this.BI_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.BI_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            this.cmsIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BI_new,
            this.BI_removeallfromtopic});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(226, 48);
            // 
            // BI_new
            // 
            this.BI_new.Name = "BI_new";
            this.BI_new.Size = new System.Drawing.Size(225, 22);
            this.BI_new.Text = "Новый значок";
            // 
            // BI_removeallfromtopic
            // 
            this.BI_removeallfromtopic.Name = "BI_removeallfromtopic";
            this.BI_removeallfromtopic.Size = new System.Drawing.Size(225, 22);
            this.BI_removeallfromtopic.Text = "Удалить все значки на теме";
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
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(113, 5);
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
            // cmsIcon
            // 
            this.cmsIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BI_newicon,
            this.BI_rename,
            this.BI_delete});
            this.cmsIcon.Name = "cmsIcon";
            this.cmsIcon.Size = new System.Drawing.Size(125, 70);
            // 
            // BI_newicon
            // 
            this.BI_newicon.Name = "BI_newicon";
            this.BI_newicon.Size = new System.Drawing.Size(124, 22);
            this.BI_newicon.Text = "New Icon";
            // 
            // BI_rename
            // 
            this.BI_rename.Name = "BI_rename";
            this.BI_rename.Size = new System.Drawing.Size(124, 22);
            this.BI_rename.Text = "Rename";
            // 
            // BI_delete
            // 
            this.BI_delete.Name = "BI_delete";
            this.BI_delete.Size = new System.Drawing.Size(124, 22);
            this.BI_delete.Text = "Delete";
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(26, 7);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 75;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // BubbleIcons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(138, 30);
            this.ControlBox = false;
            this.Controls.Add(this.p1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.Manage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubbleIcons";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            this.cmsIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem BI_new;
        private System.Windows.Forms.ToolStripMenuItem BI_removeallfromtopic;
        private System.Windows.Forms.ContextMenuStrip cmsIcon;
        private System.Windows.Forms.ToolStripMenuItem BI_newicon;
        private System.Windows.Forms.ToolStripMenuItem BI_rename;
        private System.Windows.Forms.ToolStripMenuItem BI_delete;
        public System.Windows.Forms.PictureBox pictureHandle;
        public System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.PictureBox p1;
    }
}