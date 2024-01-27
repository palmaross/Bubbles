namespace Bubbles
{
    partial class BubbleAddTopic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubbleAddTopic));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.cmsAddMultiple = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsCommon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiSize = new System.Windows.Forms.PictureBox();
            this.pAddMultiple = new System.Windows.Forms.PictureBox();
            this.Callout = new System.Windows.Forms.PictureBox();
            this.ParentTopic = new System.Windows.Forms.PictureBox();
            this.TopicBefore = new System.Windows.Forms.PictureBox();
            this.NextTopic = new System.Windows.Forms.PictureBox();
            this.Subtopic = new System.Windows.Forms.PictureBox();
            this.numUpDown = new System.Windows.Forms.NumericUpDown();
            this.TopicText = new System.Windows.Forms.TextBox();
            this.chIncrement = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddMultiple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Callout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentTopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopicBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextTopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Subtopic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // pictureHandle
            // 
            this.pictureHandle.BackColor = System.Drawing.Color.Transparent;
            this.pictureHandle.Image = ((System.Drawing.Image)(resources.GetObject("pictureHandle.Image")));
            this.pictureHandle.Location = new System.Drawing.Point(0, 0);
            this.pictureHandle.Name = "pictureHandle";
            this.pictureHandle.Size = new System.Drawing.Size(24, 24);
            this.pictureHandle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureHandle.TabIndex = 87;
            this.pictureHandle.TabStop = false;
            // 
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(302, 5);
            this.Manage.Name = "Manage";
            this.Manage.Size = new System.Drawing.Size(20, 20);
            this.Manage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Manage.TabIndex = 88;
            this.Manage.TabStop = false;
            this.Manage.Click += new System.EventHandler(this.Manage_Click);
            // 
            // cmsAddMultiple
            // 
            this.cmsAddMultiple.Name = "cmsPasteText";
            this.cmsAddMultiple.ShowImageMargin = false;
            this.cmsAddMultiple.Size = new System.Drawing.Size(36, 4);
            // 
            // cmsCommon
            // 
            this.cmsCommon.Name = "cmsCommon";
            this.cmsCommon.Size = new System.Drawing.Size(61, 4);
            // 
            // cmiSize
            // 
            this.cmiSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmiSize.Location = new System.Drawing.Point(0, 14);
            this.cmiSize.Name = "cmiSize";
            this.cmiSize.Size = new System.Drawing.Size(16, 16);
            this.cmiSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cmiSize.TabIndex = 89;
            this.cmiSize.TabStop = false;
            this.cmiSize.Visible = false;
            // 
            // pAddMultiple
            // 
            this.pAddMultiple.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pAddMultiple.Image = ((System.Drawing.Image)(resources.GetObject("pAddMultiple.Image")));
            this.pAddMultiple.Location = new System.Drawing.Point(276, 5);
            this.pAddMultiple.Name = "pAddMultiple";
            this.pAddMultiple.Size = new System.Drawing.Size(20, 20);
            this.pAddMultiple.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAddMultiple.TabIndex = 94;
            this.pAddMultiple.TabStop = false;
            this.pAddMultiple.Tag = "1";
            this.pAddMultiple.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pAddMultiple_MouseClick);
            // 
            // Callout
            // 
            this.Callout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Callout.Image = ((System.Drawing.Image)(resources.GetObject("Callout.Image")));
            this.Callout.Location = new System.Drawing.Point(132, 5);
            this.Callout.Name = "Callout";
            this.Callout.Size = new System.Drawing.Size(20, 20);
            this.Callout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Callout.TabIndex = 99;
            this.Callout.TabStop = false;
            this.Callout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddTopic_MouseClick);
            // 
            // ParentTopic
            // 
            this.ParentTopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ParentTopic.Image = ((System.Drawing.Image)(resources.GetObject("ParentTopic.Image")));
            this.ParentTopic.Location = new System.Drawing.Point(106, 5);
            this.ParentTopic.Name = "ParentTopic";
            this.ParentTopic.Size = new System.Drawing.Size(20, 20);
            this.ParentTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ParentTopic.TabIndex = 98;
            this.ParentTopic.TabStop = false;
            this.ParentTopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddTopic_MouseClick);
            // 
            // TopicBefore
            // 
            this.TopicBefore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TopicBefore.Image = ((System.Drawing.Image)(resources.GetObject("TopicBefore.Image")));
            this.TopicBefore.Location = new System.Drawing.Point(80, 5);
            this.TopicBefore.Name = "TopicBefore";
            this.TopicBefore.Size = new System.Drawing.Size(20, 20);
            this.TopicBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TopicBefore.TabIndex = 97;
            this.TopicBefore.TabStop = false;
            this.TopicBefore.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddTopic_MouseClick);
            // 
            // NextTopic
            // 
            this.NextTopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NextTopic.Image = ((System.Drawing.Image)(resources.GetObject("NextTopic.Image")));
            this.NextTopic.Location = new System.Drawing.Point(54, 5);
            this.NextTopic.Name = "NextTopic";
            this.NextTopic.Size = new System.Drawing.Size(20, 20);
            this.NextTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.NextTopic.TabIndex = 96;
            this.NextTopic.TabStop = false;
            this.NextTopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddTopic_MouseClick);
            // 
            // Subtopic
            // 
            this.Subtopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Subtopic.Image = ((System.Drawing.Image)(resources.GetObject("Subtopic.Image")));
            this.Subtopic.Location = new System.Drawing.Point(28, 5);
            this.Subtopic.Name = "Subtopic";
            this.Subtopic.Size = new System.Drawing.Size(20, 20);
            this.Subtopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Subtopic.TabIndex = 95;
            this.Subtopic.TabStop = false;
            this.Subtopic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddTopic_MouseClick);
            // 
            // numUpDown
            // 
            this.numUpDown.Location = new System.Drawing.Point(204, 7);
            this.numUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.Size = new System.Drawing.Size(27, 20);
            this.numUpDown.TabIndex = 101;
            this.numUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TopicText
            // 
            this.TopicText.Location = new System.Drawing.Point(158, 7);
            this.TopicText.Name = "TopicText";
            this.TopicText.Size = new System.Drawing.Size(44, 20);
            this.TopicText.TabIndex = 102;
            // 
            // chIncrement
            // 
            this.chIncrement.AutoSize = true;
            this.chIncrement.Location = new System.Drawing.Point(235, 9);
            this.chIncrement.Name = "chIncrement";
            this.chIncrement.Size = new System.Drawing.Size(41, 17);
            this.chIncrement.TabIndex = 104;
            this.chIncrement.Text = "Inc";
            this.chIncrement.UseVisualStyleBackColor = true;
            // 
            // BubbleAddTopic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(324, 30);
            this.ControlBox = false;
            this.Controls.Add(this.chIncrement);
            this.Controls.Add(this.TopicText);
            this.Controls.Add(this.numUpDown);
            this.Controls.Add(this.Callout);
            this.Controls.Add(this.ParentTopic);
            this.Controls.Add(this.TopicBefore);
            this.Controls.Add(this.NextTopic);
            this.Controls.Add(this.Subtopic);
            this.Controls.Add(this.pAddMultiple);
            this.Controls.Add(this.cmiSize);
            this.Controls.Add(this.Manage);
            this.Controls.Add(this.pictureHandle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubbleAddTopic";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmiSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pAddMultiple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Callout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentTopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopicBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextTopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Subtopic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.ContextMenuStrip cmsAddMultiple;
        private System.Windows.Forms.ContextMenuStrip cmsCommon;
        private System.Windows.Forms.PictureBox cmiSize;
        public System.Windows.Forms.PictureBox pAddMultiple;
        public System.Windows.Forms.PictureBox Callout;
        public System.Windows.Forms.PictureBox ParentTopic;
        public System.Windows.Forms.PictureBox TopicBefore;
        public System.Windows.Forms.PictureBox NextTopic;
        public System.Windows.Forms.PictureBox Subtopic;
        private System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.TextBox TopicText;
        private System.Windows.Forms.CheckBox chIncrement;
    }
}