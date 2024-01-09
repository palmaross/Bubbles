namespace Bubbles
{
    partial class BubbleTaskInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BubbleTaskInfo));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmsDuration = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ST_DurationUnits = new System.Windows.Forms.ToolStripComboBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.Manage = new System.Windows.Forms.PictureBox();
            this.pictureHandle = new System.Windows.Forms.PictureBox();
            this.cmsCommon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pPriority = new System.Windows.Forms.PictureBox();
            this.pProgress = new System.Windows.Forms.PictureBox();
            this.pResources = new System.Windows.Forms.PictureBox();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.p100 = new System.Windows.Forms.PictureBox();
            this.pQuickTask = new System.Windows.Forms.PictureBox();
            this.pRemoveTaskInfo = new System.Windows.Forms.PictureBox();
            this.p2 = new System.Windows.Forms.PictureBox();
            this.linkDurationUnit = new System.Windows.Forms.LinkLabel();
            this.pTopicStartDate = new System.Windows.Forms.PictureBox();
            this.pTopicDueDate = new System.Windows.Forms.PictureBox();
            this.panelStartDate = new System.Windows.Forms.Panel();
            this.pStartDate = new System.Windows.Forms.MaskedTextBox();
            this.cmsDates = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Dates_today = new System.Windows.Forms.ToolStripMenuItem();
            this.Dates_today_today = new System.Windows.Forms.ToolStripMenuItem();
            this.Dates_today_tomorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.Dates_tomorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.Dates_tomorrow_tomorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.Dates_thisweek = new System.Windows.Forms.ToolStripMenuItem();
            this.Dates_nextweek = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDueDate = new System.Windows.Forms.Panel();
            this.pDueDate = new System.Windows.Forms.MaskedTextBox();
            this.panelDuration = new System.Windows.Forms.Panel();
            this.cmsTaskTemplates = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsResources = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pResources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p100)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pQuickTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemoveTaskInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTopicStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTopicDueDate)).BeginInit();
            this.panelStartDate.SuspendLayout();
            this.cmsDates.SuspendLayout();
            this.panelDueDate.SuspendLayout();
            this.panelDuration.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // cmsDuration
            // 
            this.cmsDuration.AutoSize = false;
            this.cmsDuration.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ST_DurationUnits});
            this.cmsDuration.Name = "contextMenuStrip1";
            this.cmsDuration.ShowImageMargin = false;
            this.cmsDuration.Size = new System.Drawing.Size(135, 48);
            // 
            // ST_DurationUnits
            // 
            this.ST_DurationUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ST_DurationUnits.Name = "ST_DurationUnits";
            this.ST_DurationUnits.Size = new System.Drawing.Size(125, 23);
            this.ST_DurationUnits.SelectedIndexChanged += new System.EventHandler(this.ST_DurationUnits_SelectedIndexChanged);
            this.ST_DurationUnits.Enter += new System.EventHandler(this.ST_DurationUnits_Enter);
            this.ST_DurationUnits.MouseEnter += new System.EventHandler(this.ST_DurationUnits_MouseEnter);
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
            // Manage
            // 
            this.Manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Manage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Manage.Image = ((System.Drawing.Image)(resources.GetObject("Manage.Image")));
            this.Manage.Location = new System.Drawing.Point(269, 5);
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
            // cmsCommon
            // 
            this.cmsCommon.Name = "cmsIcon";
            this.cmsCommon.Size = new System.Drawing.Size(61, 4);
            // 
            // pPriority
            // 
            this.pPriority.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pPriority.Image = ((System.Drawing.Image)(resources.GetObject("pPriority.Image")));
            this.pPriority.Location = new System.Drawing.Point(68, 7);
            this.pPriority.Name = "pPriority";
            this.pPriority.Size = new System.Drawing.Size(16, 16);
            this.pPriority.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPriority.TabIndex = 78;
            this.pPriority.TabStop = false;
            this.pPriority.Tag = "1";
            this.pPriority.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pPriority_MouseClick);
            // 
            // pProgress
            // 
            this.pProgress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pProgress.Image = ((System.Drawing.Image)(resources.GetObject("pProgress.Image")));
            this.pProgress.Location = new System.Drawing.Point(47, 7);
            this.pProgress.Name = "pProgress";
            this.pProgress.Size = new System.Drawing.Size(16, 16);
            this.pProgress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pProgress.TabIndex = 79;
            this.pProgress.TabStop = false;
            this.pProgress.Tag = "1";
            this.pProgress.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pProgress_MouseClick);
            // 
            // pResources
            // 
            this.pResources.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pResources.Image = ((System.Drawing.Image)(resources.GetObject("pResources.Image")));
            this.pResources.Location = new System.Drawing.Point(89, 7);
            this.pResources.Name = "pResources";
            this.pResources.Size = new System.Drawing.Size(16, 16);
            this.pResources.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pResources.TabIndex = 82;
            this.pResources.TabStop = false;
            this.pResources.Tag = "1";
            this.pResources.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pResources_MouseClick);
            // 
            // numDuration
            // 
            this.numDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numDuration.Location = new System.Drawing.Point(0, 0);
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(30, 19);
            this.numDuration.TabIndex = 83;
            this.numDuration.Tag = "1";
            this.numDuration.ValueChanged += new System.EventHandler(this.numDuration_ValueChanged);
            this.numDuration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numDuration_KeyDown);
            // 
            // p100
            // 
            this.p100.Cursor = System.Windows.Forms.Cursors.Hand;
            this.p100.Image = ((System.Drawing.Image)(resources.GetObject("p100.Image")));
            this.p100.Location = new System.Drawing.Point(26, 7);
            this.p100.Name = "p100";
            this.p100.Size = new System.Drawing.Size(16, 16);
            this.p100.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p100.TabIndex = 84;
            this.p100.TabStop = false;
            this.p100.Tag = "1";
            this.p100.Click += new System.EventHandler(this.p100_Click);
            // 
            // pQuickTask
            // 
            this.pQuickTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pQuickTask.Image = ((System.Drawing.Image)(resources.GetObject("pQuickTask.Image")));
            this.pQuickTask.Location = new System.Drawing.Point(226, 7);
            this.pQuickTask.Name = "pQuickTask";
            this.pQuickTask.Size = new System.Drawing.Size(16, 16);
            this.pQuickTask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pQuickTask.TabIndex = 85;
            this.pQuickTask.TabStop = false;
            this.pQuickTask.Tag = "1";
            this.pQuickTask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pQuickTask_MouseClick);
            // 
            // pRemoveTaskInfo
            // 
            this.pRemoveTaskInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pRemoveTaskInfo.Image = ((System.Drawing.Image)(resources.GetObject("pRemoveTaskInfo.Image")));
            this.pRemoveTaskInfo.Location = new System.Drawing.Point(246, 7);
            this.pRemoveTaskInfo.Name = "pRemoveTaskInfo";
            this.pRemoveTaskInfo.Size = new System.Drawing.Size(16, 16);
            this.pRemoveTaskInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRemoveTaskInfo.TabIndex = 87;
            this.pRemoveTaskInfo.TabStop = false;
            this.pRemoveTaskInfo.Tag = "1";
            this.pRemoveTaskInfo.Click += new System.EventHandler(this.pRemoveTaskInfo_Click);
            // 
            // p2
            // 
            this.p2.Location = new System.Drawing.Point(22, 25);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(19, 3);
            this.p2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p2.TabIndex = 88;
            this.p2.TabStop = false;
            this.p2.Tag = "1";
            // 
            // linkDurationUnit
            // 
            this.linkDurationUnit.BackColor = System.Drawing.Color.Moccasin;
            this.linkDurationUnit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkDurationUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkDurationUnit.Location = new System.Drawing.Point(-4, 16);
            this.linkDurationUnit.Name = "linkDurationUnit";
            this.linkDurationUnit.Size = new System.Drawing.Size(36, 13);
            this.linkDurationUnit.TabIndex = 90;
            this.linkDurationUnit.TabStop = true;
            this.linkDurationUnit.Text = "month";
            this.linkDurationUnit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkDurationUnit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDurationUnit_LinkClicked);
            // 
            // pTopicStartDate
            // 
            this.pTopicStartDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pTopicStartDate.Image = ((System.Drawing.Image)(resources.GetObject("pTopicStartDate.Image")));
            this.pTopicStartDate.Location = new System.Drawing.Point(-1, 16);
            this.pTopicStartDate.Name = "pTopicStartDate";
            this.pTopicStartDate.Size = new System.Drawing.Size(32, 14);
            this.pTopicStartDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pTopicStartDate.TabIndex = 94;
            this.pTopicStartDate.TabStop = false;
            this.pTopicStartDate.Click += new System.EventHandler(this.pTopicSetDate_Click);
            // 
            // pTopicDueDate
            // 
            this.pTopicDueDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pTopicDueDate.Image = ((System.Drawing.Image)(resources.GetObject("pTopicDueDate.Image")));
            this.pTopicDueDate.Location = new System.Drawing.Point(-1, 16);
            this.pTopicDueDate.Name = "pTopicDueDate";
            this.pTopicDueDate.Size = new System.Drawing.Size(32, 14);
            this.pTopicDueDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pTopicDueDate.TabIndex = 96;
            this.pTopicDueDate.TabStop = false;
            this.pTopicDueDate.Click += new System.EventHandler(this.pTopicSetDate_Click);
            // 
            // panelStartDate
            // 
            this.panelStartDate.Controls.Add(this.pStartDate);
            this.panelStartDate.Controls.Add(this.pTopicStartDate);
            this.panelStartDate.Location = new System.Drawing.Point(113, 0);
            this.panelStartDate.Name = "panelStartDate";
            this.panelStartDate.Size = new System.Drawing.Size(32, 32);
            this.panelStartDate.TabIndex = 98;
            // 
            // pStartDate
            // 
            this.pStartDate.BeepOnError = true;
            this.pStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pStartDate.ContextMenuStrip = this.cmsDates;
            this.pStartDate.Culture = new System.Globalization.CultureInfo("");
            this.pStartDate.Location = new System.Drawing.Point(0, 0);
            this.pStartDate.Mask = "00/00";
            this.pStartDate.Name = "pStartDate";
            this.pStartDate.Size = new System.Drawing.Size(31, 20);
            this.pStartDate.TabIndex = 0;
            this.pStartDate.TabStop = false;
            this.pStartDate.ValidatingType = typeof(System.DateTime);
            this.pStartDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateBox_MouseClick);
            this.pStartDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pStartDate_MouseDown);
            // 
            // cmsDates
            // 
            this.cmsDates.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Dates_today,
            this.Dates_today_today,
            this.Dates_today_tomorrow,
            this.Dates_tomorrow,
            this.Dates_tomorrow_tomorrow,
            this.Dates_thisweek,
            this.Dates_nextweek});
            this.cmsDates.Name = "cmsDates";
            this.cmsDates.ShowImageMargin = false;
            this.cmsDates.Size = new System.Drawing.Size(169, 158);
            // 
            // Dates_today
            // 
            this.Dates_today.Name = "Dates_today";
            this.Dates_today.Size = new System.Drawing.Size(168, 22);
            this.Dates_today.Text = "Today";
            // 
            // Dates_today_today
            // 
            this.Dates_today_today.Name = "Dates_today_today";
            this.Dates_today_today.Size = new System.Drawing.Size(168, 22);
            this.Dates_today_today.Text = "Today - Today";
            // 
            // Dates_today_tomorrow
            // 
            this.Dates_today_tomorrow.Name = "Dates_today_tomorrow";
            this.Dates_today_tomorrow.Size = new System.Drawing.Size(168, 22);
            this.Dates_today_tomorrow.Text = "Today - Tomorrow";
            // 
            // Dates_tomorrow
            // 
            this.Dates_tomorrow.Name = "Dates_tomorrow";
            this.Dates_tomorrow.Size = new System.Drawing.Size(168, 22);
            this.Dates_tomorrow.Text = "Tomorrow";
            // 
            // Dates_tomorrow_tomorrow
            // 
            this.Dates_tomorrow_tomorrow.Name = "Dates_tomorrow_tomorrow";
            this.Dates_tomorrow_tomorrow.Size = new System.Drawing.Size(168, 22);
            this.Dates_tomorrow_tomorrow.Text = "Tomorrow - Tomorrow";
            // 
            // Dates_thisweek
            // 
            this.Dates_thisweek.Name = "Dates_thisweek";
            this.Dates_thisweek.Size = new System.Drawing.Size(168, 22);
            this.Dates_thisweek.Text = "This Week";
            // 
            // Dates_nextweek
            // 
            this.Dates_nextweek.Name = "Dates_nextweek";
            this.Dates_nextweek.Size = new System.Drawing.Size(168, 22);
            this.Dates_nextweek.Text = "Next Week";
            // 
            // panelDueDate
            // 
            this.panelDueDate.Controls.Add(this.pDueDate);
            this.panelDueDate.Controls.Add(this.pTopicDueDate);
            this.panelDueDate.Location = new System.Drawing.Point(187, 0);
            this.panelDueDate.Name = "panelDueDate";
            this.panelDueDate.Size = new System.Drawing.Size(32, 32);
            this.panelDueDate.TabIndex = 99;
            // 
            // pDueDate
            // 
            this.pDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDueDate.ContextMenuStrip = this.cmsDates;
            this.pDueDate.Location = new System.Drawing.Point(0, 0);
            this.pDueDate.Mask = "00/00";
            this.pDueDate.Name = "pDueDate";
            this.pDueDate.Size = new System.Drawing.Size(31, 20);
            this.pDueDate.TabIndex = 0;
            this.pDueDate.TabStop = false;
            this.pDueDate.ValidatingType = typeof(System.DateTime);
            this.pDueDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateBox_MouseClick);
            this.pDueDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pDueDate_MouseDown);
            // 
            // panelDuration
            // 
            this.panelDuration.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panelDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDuration.Controls.Add(this.numDuration);
            this.panelDuration.Controls.Add(this.linkDurationUnit);
            this.panelDuration.Location = new System.Drawing.Point(150, 0);
            this.panelDuration.Name = "panelDuration";
            this.panelDuration.Size = new System.Drawing.Size(32, 32);
            this.panelDuration.TabIndex = 100;
            // 
            // cmsTaskTemplates
            // 
            this.cmsTaskTemplates.Name = "cmsTaskTemplates";
            this.cmsTaskTemplates.ShowImageMargin = false;
            this.cmsTaskTemplates.Size = new System.Drawing.Size(36, 4);
            // 
            // cmsResources
            // 
            this.cmsResources.Name = "cmsResources";
            this.cmsResources.Size = new System.Drawing.Size(61, 4);
            // 
            // BubbleTaskInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(293, 30);
            this.ControlBox = false;
            this.Controls.Add(this.panelDuration);
            this.Controls.Add(this.panelDueDate);
            this.Controls.Add(this.panelStartDate);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.pRemoveTaskInfo);
            this.Controls.Add(this.pQuickTask);
            this.Controls.Add(this.p100);
            this.Controls.Add(this.pResources);
            this.Controls.Add(this.pProgress);
            this.Controls.Add(this.pPriority);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.pictureHandle);
            this.Controls.Add(this.Manage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BubbleTaskInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.cmsDuration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Manage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pResources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p100)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pQuickTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemoveTaskInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTopicStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pTopicDueDate)).EndInit();
            this.panelStartDate.ResumeLayout(false);
            this.panelStartDate.PerformLayout();
            this.cmsDates.ResumeLayout(false);
            this.panelDueDate.ResumeLayout(false);
            this.panelDueDate.PerformLayout();
            this.panelDuration.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmsDuration;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.PictureBox Manage;
        private System.Windows.Forms.PictureBox pictureHandle;
        private System.Windows.Forms.ContextMenuStrip cmsCommon;
        private System.Windows.Forms.PictureBox p100;
        private System.Windows.Forms.PictureBox pQuickTask;
        private System.Windows.Forms.PictureBox pRemoveTaskInfo;
        private System.Windows.Forms.PictureBox p2;
        public System.Windows.Forms.PictureBox pPriority;
        private System.Windows.Forms.PictureBox pProgress;
        private System.Windows.Forms.Panel panelDuration;
        public System.Windows.Forms.Panel panelStartDate;
        public System.Windows.Forms.NumericUpDown numDuration;
        public System.Windows.Forms.LinkLabel linkDurationUnit;
        public System.Windows.Forms.ToolStripComboBox ST_DurationUnits;
        public System.Windows.Forms.PictureBox pTopicStartDate;
        public System.Windows.Forms.PictureBox pTopicDueDate;
        public System.Windows.Forms.PictureBox pResources;
        public System.Windows.Forms.MaskedTextBox pStartDate;
        public System.Windows.Forms.MaskedTextBox pDueDate;
        public System.Windows.Forms.Panel panelDueDate;
        private System.Windows.Forms.ContextMenuStrip cmsTaskTemplates;
        private System.Windows.Forms.ContextMenuStrip cmsResources;
        private System.Windows.Forms.ContextMenuStrip cmsDates;
        private System.Windows.Forms.ToolStripMenuItem Dates_today;
        private System.Windows.Forms.ToolStripMenuItem Dates_tomorrow;
        private System.Windows.Forms.ToolStripMenuItem Dates_nextweek;
        private System.Windows.Forms.ToolStripMenuItem Dates_today_today;
        private System.Windows.Forms.ToolStripMenuItem Dates_today_tomorrow;
        private System.Windows.Forms.ToolStripMenuItem Dates_tomorrow_tomorrow;
        private System.Windows.Forms.ToolStripMenuItem Dates_thisweek;
    }
}