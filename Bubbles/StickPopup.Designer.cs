namespace Bubbles
{
    partial class StickPopup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StickPopup));
            this.panelH = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.PictureBox();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.pRemember = new System.Windows.Forms.PictureBox();
            this.pRotate = new System.Windows.Forms.PictureBox();
            this.pCollapse = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pPasteIcon = new System.Windows.Forms.PictureBox();
            this.panelMin = new System.Windows.Forms.Panel();
            this.pCleanFormat = new System.Windows.Forms.PictureBox();
            this.pFontItalic = new System.Windows.Forms.PictureBox();
            this.pBookmarkList = new System.Windows.Forms.PictureBox();
            this.pNewBookmark = new System.Windows.Forms.PictureBox();
            this.pNewIcon = new System.Windows.Forms.PictureBox();
            this.panelOther = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Subtopic = new System.Windows.Forms.PictureBox();
            this.NextTopic = new System.Windows.Forms.PictureBox();
            this.TopicBefore = new System.Windows.Forms.PictureBox();
            this.ParentTopic = new System.Windows.Forms.PictureBox();
            this.Callout = new System.Windows.Forms.PictureBox();
            this.panelPasteTopic = new System.Windows.Forms.Panel();
            this.ToggleTextFormat = new System.Windows.Forms.PictureBox();
            this.p11 = new System.Windows.Forms.PictureBox();
            this.panelAddTopic = new System.Windows.Forms.Panel();
            this.panelH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCollapse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPasteIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCleanFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pFontItalic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBookmarkList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewBookmark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewIcon)).BeginInit();
            this.panelOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Subtopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextTopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopicBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentTopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Callout)).BeginInit();
            this.panelPasteTopic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToggleTextFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p11)).BeginInit();
            this.SuspendLayout();
            // 
            // panelH
            // 
            this.panelH.BackColor = System.Drawing.SystemColors.Control;
            this.panelH.Controls.Add(this.p2);
            this.panelH.Controls.Add(this.p1);
            this.panelH.Controls.Add(this.pClose);
            this.panelH.Controls.Add(this.pRemember);
            this.panelH.Controls.Add(this.pRotate);
            this.panelH.Controls.Add(this.pCollapse);
            this.panelH.Location = new System.Drawing.Point(3, 3);
            this.panelH.Name = "panelH";
            this.panelH.Size = new System.Drawing.Size(128, 20);
            this.panelH.TabIndex = 0;
            // 
            // p2
            // 
            this.p2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.p2.Location = new System.Drawing.Point(84, 2);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(16, 16);
            this.p2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p2.TabIndex = 7;
            this.p2.TabStop = false;
            this.p2.Visible = false;
            // 
            // p1
            // 
            this.p1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.p1.Location = new System.Drawing.Point(64, 2);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p1.TabIndex = 6;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // pClose
            // 
            this.pClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pClose.Image = ((System.Drawing.Image)(resources.GetObject("pClose.Image")));
            this.pClose.Location = new System.Drawing.Point(110, 2);
            this.pClose.Name = "pClose";
            this.pClose.Size = new System.Drawing.Size(16, 16);
            this.pClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClose.TabIndex = 5;
            this.pClose.TabStop = false;
            this.pClose.Click += new System.EventHandler(this.pClose_Click);
            // 
            // pRemember
            // 
            this.pRemember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pRemember.Image = ((System.Drawing.Image)(resources.GetObject("pRemember.Image")));
            this.pRemember.Location = new System.Drawing.Point(40, 3);
            this.pRemember.Name = "pRemember";
            this.pRemember.Size = new System.Drawing.Size(14, 14);
            this.pRemember.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRemember.TabIndex = 4;
            this.pRemember.TabStop = false;
            this.pRemember.Click += new System.EventHandler(this.pRemember_Click);
            // 
            // pRotate
            // 
            this.pRotate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pRotate.Image = ((System.Drawing.Image)(resources.GetObject("pRotate.Image")));
            this.pRotate.Location = new System.Drawing.Point(22, 3);
            this.pRotate.Name = "pRotate";
            this.pRotate.Size = new System.Drawing.Size(14, 14);
            this.pRotate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRotate.TabIndex = 3;
            this.pRotate.TabStop = false;
            this.pRotate.Click += new System.EventHandler(this.pRotate_Click);
            // 
            // pCollapse
            // 
            this.pCollapse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pCollapse.Image = ((System.Drawing.Image)(resources.GetObject("pCollapse.Image")));
            this.pCollapse.Location = new System.Drawing.Point(2, 2);
            this.pCollapse.Name = "pCollapse";
            this.pCollapse.Size = new System.Drawing.Size(16, 16);
            this.pCollapse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCollapse.TabIndex = 1;
            this.pCollapse.TabStop = false;
            this.pCollapse.Click += new System.EventHandler(this.pCollapse_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // pPasteIcon
            // 
            this.pPasteIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pPasteIcon.Image = ((System.Drawing.Image)(resources.GetObject("pPasteIcon.Image")));
            this.pPasteIcon.Location = new System.Drawing.Point(20, 2);
            this.pPasteIcon.Name = "pPasteIcon";
            this.pPasteIcon.Size = new System.Drawing.Size(16, 16);
            this.pPasteIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPasteIcon.TabIndex = 9;
            this.pPasteIcon.TabStop = false;
            this.toolTip1.SetToolTip(this.pPasteIcon, "Развернуть");
            this.pPasteIcon.Click += new System.EventHandler(this.pPasteIcon_Click);
            // 
            // panelMin
            // 
            this.panelMin.Location = new System.Drawing.Point(3, 29);
            this.panelMin.Name = "panelMin";
            this.panelMin.Size = new System.Drawing.Size(88, 20);
            this.panelMin.TabIndex = 1;
            // 
            // pCleanFormat
            // 
            this.pCleanFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pCleanFormat.Image = ((System.Drawing.Image)(resources.GetObject("pCleanFormat.Image")));
            this.pCleanFormat.Location = new System.Drawing.Point(91, 2);
            this.pCleanFormat.Name = "pCleanFormat";
            this.pCleanFormat.Size = new System.Drawing.Size(16, 16);
            this.pCleanFormat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCleanFormat.TabIndex = 13;
            this.pCleanFormat.TabStop = false;
            this.pCleanFormat.Click += new System.EventHandler(this.pCleanFormat_Click);
            // 
            // pFontItalic
            // 
            this.pFontItalic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pFontItalic.Image = ((System.Drawing.Image)(resources.GetObject("pFontItalic.Image")));
            this.pFontItalic.Location = new System.Drawing.Point(73, 2);
            this.pFontItalic.Name = "pFontItalic";
            this.pFontItalic.Size = new System.Drawing.Size(16, 16);
            this.pFontItalic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pFontItalic.TabIndex = 12;
            this.pFontItalic.TabStop = false;
            this.pFontItalic.Click += new System.EventHandler(this.pFontItalic_Click);
            // 
            // pBookmarkList
            // 
            this.pBookmarkList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pBookmarkList.Image = ((System.Drawing.Image)(resources.GetObject("pBookmarkList.Image")));
            this.pBookmarkList.Location = new System.Drawing.Point(55, 2);
            this.pBookmarkList.Name = "pBookmarkList";
            this.pBookmarkList.Size = new System.Drawing.Size(16, 16);
            this.pBookmarkList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBookmarkList.TabIndex = 11;
            this.pBookmarkList.TabStop = false;
            this.pBookmarkList.Click += new System.EventHandler(this.pBookmarkList_Click);
            // 
            // pNewBookmark
            // 
            this.pNewBookmark.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pNewBookmark.Image = ((System.Drawing.Image)(resources.GetObject("pNewBookmark.Image")));
            this.pNewBookmark.Location = new System.Drawing.Point(38, 3);
            this.pNewBookmark.Name = "pNewBookmark";
            this.pNewBookmark.Size = new System.Drawing.Size(15, 15);
            this.pNewBookmark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pNewBookmark.TabIndex = 10;
            this.pNewBookmark.TabStop = false;
            this.pNewBookmark.Click += new System.EventHandler(this.pNewBookmark_Click);
            // 
            // pNewIcon
            // 
            this.pNewIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pNewIcon.Image = ((System.Drawing.Image)(resources.GetObject("pNewIcon.Image")));
            this.pNewIcon.Location = new System.Drawing.Point(2, 2);
            this.pNewIcon.Name = "pNewIcon";
            this.pNewIcon.Size = new System.Drawing.Size(16, 16);
            this.pNewIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pNewIcon.TabIndex = 8;
            this.pNewIcon.TabStop = false;
            this.pNewIcon.Click += new System.EventHandler(this.pNewIcon_Click);
            // 
            // panelOther
            // 
            this.panelOther.Controls.Add(this.pNewIcon);
            this.panelOther.Controls.Add(this.pPasteIcon);
            this.panelOther.Controls.Add(this.pCleanFormat);
            this.panelOther.Controls.Add(this.pNewBookmark);
            this.panelOther.Controls.Add(this.pFontItalic);
            this.panelOther.Controls.Add(this.pBookmarkList);
            this.panelOther.Location = new System.Drawing.Point(138, 3);
            this.panelOther.Name = "panelOther";
            this.panelOther.Size = new System.Drawing.Size(110, 20);
            this.panelOther.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Subtopic
            // 
            this.Subtopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Subtopic.Image = ((System.Drawing.Image)(resources.GetObject("Subtopic.Image")));
            this.Subtopic.Location = new System.Drawing.Point(2, 2);
            this.Subtopic.Name = "Subtopic";
            this.Subtopic.Size = new System.Drawing.Size(20, 20);
            this.Subtopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Subtopic.TabIndex = 8;
            this.Subtopic.TabStop = false;
            this.Subtopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pAddTopic_MouseClick);
            // 
            // NextTopic
            // 
            this.NextTopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NextTopic.Image = ((System.Drawing.Image)(resources.GetObject("NextTopic.Image")));
            this.NextTopic.Location = new System.Drawing.Point(28, 2);
            this.NextTopic.Name = "NextTopic";
            this.NextTopic.Size = new System.Drawing.Size(20, 20);
            this.NextTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.NextTopic.TabIndex = 9;
            this.NextTopic.TabStop = false;
            this.NextTopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pAddTopic_MouseClick);
            // 
            // TopicBefore
            // 
            this.TopicBefore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TopicBefore.Image = ((System.Drawing.Image)(resources.GetObject("TopicBefore.Image")));
            this.TopicBefore.Location = new System.Drawing.Point(54, 2);
            this.TopicBefore.Name = "TopicBefore";
            this.TopicBefore.Size = new System.Drawing.Size(20, 20);
            this.TopicBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TopicBefore.TabIndex = 10;
            this.TopicBefore.TabStop = false;
            this.TopicBefore.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pAddTopic_MouseClick);
            // 
            // ParentTopic
            // 
            this.ParentTopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ParentTopic.Image = ((System.Drawing.Image)(resources.GetObject("ParentTopic.Image")));
            this.ParentTopic.Location = new System.Drawing.Point(80, 2);
            this.ParentTopic.Name = "ParentTopic";
            this.ParentTopic.Size = new System.Drawing.Size(20, 20);
            this.ParentTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ParentTopic.TabIndex = 11;
            this.ParentTopic.TabStop = false;
            this.ParentTopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pAddTopic_MouseClick);
            // 
            // Callout
            // 
            this.Callout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Callout.Image = ((System.Drawing.Image)(resources.GetObject("Callout.Image")));
            this.Callout.Location = new System.Drawing.Point(106, 2);
            this.Callout.Name = "Callout";
            this.Callout.Size = new System.Drawing.Size(20, 20);
            this.Callout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Callout.TabIndex = 13;
            this.Callout.TabStop = false;
            this.Callout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pAddTopic_MouseClick);
            // 
            // panelPasteTopic
            // 
            this.panelPasteTopic.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panelPasteTopic.Controls.Add(this.ToggleTextFormat);
            this.panelPasteTopic.Controls.Add(this.Callout);
            this.panelPasteTopic.Controls.Add(this.ParentTopic);
            this.panelPasteTopic.Controls.Add(this.TopicBefore);
            this.panelPasteTopic.Controls.Add(this.NextTopic);
            this.panelPasteTopic.Controls.Add(this.Subtopic);
            this.panelPasteTopic.Location = new System.Drawing.Point(97, 29);
            this.panelPasteTopic.Name = "panelPasteTopic";
            this.panelPasteTopic.Size = new System.Drawing.Size(154, 24);
            this.panelPasteTopic.TabIndex = 2;
            // 
            // ToggleTextFormat
            // 
            this.ToggleTextFormat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ToggleTextFormat.Image = ((System.Drawing.Image)(resources.GetObject("ToggleTextFormat.Image")));
            this.ToggleTextFormat.Location = new System.Drawing.Point(132, 2);
            this.ToggleTextFormat.Name = "ToggleTextFormat";
            this.ToggleTextFormat.Size = new System.Drawing.Size(20, 20);
            this.ToggleTextFormat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ToggleTextFormat.TabIndex = 14;
            this.ToggleTextFormat.TabStop = false;
            this.ToggleTextFormat.Click += new System.EventHandler(this.ToggleTextFormat_Click);
            // 
            // p11
            // 
            this.p11.Location = new System.Drawing.Point(231, 29);
            this.p11.Name = "p11";
            this.p11.Size = new System.Drawing.Size(16, 3);
            this.p11.TabIndex = 3;
            this.p11.TabStop = false;
            // 
            // panelAddTopic
            // 
            this.panelAddTopic.Location = new System.Drawing.Point(97, 59);
            this.panelAddTopic.Name = "panelAddTopic";
            this.panelAddTopic.Size = new System.Drawing.Size(128, 20);
            this.panelAddTopic.TabIndex = 2;
            // 
            // StickPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAddTopic);
            this.Controls.Add(this.p11);
            this.Controls.Add(this.panelOther);
            this.Controls.Add(this.panelPasteTopic);
            this.Controls.Add(this.panelH);
            this.Controls.Add(this.panelMin);
            this.Name = "StickPopup";
            this.Size = new System.Drawing.Size(285, 86);
            this.panelH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCollapse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPasteIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCleanFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pFontItalic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBookmarkList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewBookmark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewIcon)).EndInit();
            this.panelOther.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Subtopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextTopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopicBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentTopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Callout)).EndInit();
            this.panelPasteTopic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ToggleTextFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelH;
        private System.Windows.Forms.PictureBox pCollapse;
        private System.Windows.Forms.PictureBox pRemember;
        private System.Windows.Forms.PictureBox pRotate;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Panel panelMin;
        public System.Windows.Forms.PictureBox pCleanFormat;
        public System.Windows.Forms.PictureBox pFontItalic;
        public System.Windows.Forms.PictureBox pBookmarkList;
        public System.Windows.Forms.PictureBox pNewBookmark;
        public System.Windows.Forms.PictureBox pPasteIcon;
        public System.Windows.Forms.PictureBox pNewIcon;
        public System.Windows.Forms.PictureBox p2;
        public System.Windows.Forms.PictureBox p1;
        public System.Windows.Forms.PictureBox pClose;
        public System.Windows.Forms.Panel panelOther;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.PictureBox Subtopic;
        public System.Windows.Forms.PictureBox NextTopic;
        public System.Windows.Forms.PictureBox TopicBefore;
        public System.Windows.Forms.PictureBox ParentTopic;
        public System.Windows.Forms.PictureBox Callout;
        public System.Windows.Forms.Panel panelPasteTopic;
        private System.Windows.Forms.PictureBox p11;
        public System.Windows.Forms.PictureBox ToggleTextFormat;
        public System.Windows.Forms.Panel panelAddTopic;
    }
}
