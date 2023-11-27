﻿namespace Bubbles
{
    partial class BubbleBookmarks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubbleBookmarks));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BI_addbookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.BI_main = new System.Windows.Forms.ToolStripMenuItem();
            this.BI_deletemain = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pCentral = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.p2 = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.BookmarkList = new System.Windows.Forms.PictureBox();
            this.p3 = new System.Windows.Forms.Panel();
            this.cmsDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BI_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.BI_bookmarklist = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pCentral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookmarkList)).BeginInit();
            this.cmsDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BI_addbookmark,
            this.BI_main,
            this.BI_deletemain,
            this.BI_bookmarklist});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(200, 114);
            // 
            // BI_addbookmark
            // 
            this.BI_addbookmark.Name = "BI_addbookmark";
            this.BI_addbookmark.Size = new System.Drawing.Size(199, 22);
            this.BI_addbookmark.Text = "Добавить закладку";
            // 
            // BI_main
            // 
            this.BI_main.Name = "BI_main";
            this.BI_main.Size = new System.Drawing.Size(199, 22);
            this.BI_main.Text = "Главные темы";
            // 
            // BI_deletemain
            // 
            this.BI_deletemain.Name = "BI_deletemain";
            this.BI_deletemain.Size = new System.Drawing.Size(199, 22);
            this.BI_deletemain.Text = "Удалить главные темы";
            // 
            // pCentral
            // 
            this.pCentral.Image = ((System.Drawing.Image)(resources.GetObject("pCentral.Image")));
            this.pCentral.Location = new System.Drawing.Point(23, 3);
            this.pCentral.Name = "pCentral";
            this.pCentral.Size = new System.Drawing.Size(24, 24);
            this.pCentral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCentral.TabIndex = 75;
            this.pCentral.TabStop = false;
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
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(50, 5);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(20, 20);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 81;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // p2
            // 
            this.p2.Location = new System.Drawing.Point(52, 7);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(16, 16);
            this.p2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p2.TabIndex = 82;
            this.p2.TabStop = false;
            this.p2.Visible = false;
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
            // BookmarkList
            // 
            this.BookmarkList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookmarkList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BookmarkList.Image = ((System.Drawing.Image)(resources.GetObject("BookmarkList.Image")));
            this.BookmarkList.Location = new System.Drawing.Point(93, 5);
            this.BookmarkList.Name = "BookmarkList";
            this.BookmarkList.Size = new System.Drawing.Size(20, 20);
            this.BookmarkList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BookmarkList.TabIndex = 80;
            this.BookmarkList.TabStop = false;
            this.BookmarkList.Click += new System.EventHandler(this.BookmarkList_Click);
            // 
            // p3
            // 
            this.p3.Location = new System.Drawing.Point(4, 26);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(16, 4);
            this.p3.TabIndex = 84;
            this.p3.Visible = false;
            // 
            // cmsDelete
            // 
            this.cmsDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BI_delete});
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.ShowImageMargin = false;
            this.cmsDelete.Size = new System.Drawing.Size(145, 26);
            // 
            // BI_delete
            // 
            this.BI_delete.Name = "BI_delete";
            this.BI_delete.Size = new System.Drawing.Size(144, 22);
            this.BI_delete.Text = "Удалить закладку";
            // 
            // BI_bookmarklist
            // 
            this.BI_bookmarklist.Name = "BI_bookmarklist";
            this.BI_bookmarklist.Size = new System.Drawing.Size(199, 22);
            this.BI_bookmarklist.Text = "Закладки списком";
            // 
            // BubbleBookmarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(142, 30);
            this.ControlBox = false;
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.pCentral);
            this.Controls.Add(this.BookmarkList);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.p1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubbleBookmarks";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pCentral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookmarkList)).EndInit();
            this.cmsDelete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox pCentral;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.ToolStripMenuItem BI_main;
        private System.Windows.Forms.ToolStripMenuItem BI_deletemain;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.PictureBox BookmarkList;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.PictureBox p2;
        private System.Windows.Forms.ToolStripMenuItem BI_addbookmark;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.ContextMenuStrip cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem BI_delete;
        private System.Windows.Forms.ToolStripMenuItem BI_bookmarklist;
    }
}