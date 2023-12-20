namespace Bubbles
{
    partial class TopicNotesDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopicNotesDlg));
            this.listTopics = new System.Windows.Forms.ListBox();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.txtSearchNotes = new System.Windows.Forms.TextBox();
            this.panelEditButtons = new System.Windows.Forms.Panel();
            this.pStrikeout = new System.Windows.Forms.PictureBox();
            this.pUnderline = new System.Windows.Forms.PictureBox();
            this.pItalic = new System.Windows.Forms.PictureBox();
            this.pBold = new System.Windows.Forms.PictureBox();
            this.fontDown = new System.Windows.Forms.PictureBox();
            this.fontUp = new System.Windows.Forms.PictureBox();
            this.pSearchNotes = new System.Windows.Forms.PictureBox();
            this.lblTopics = new System.Windows.Forms.Label();
            this.lblMap = new System.Windows.Forms.Label();
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.panelRtb = new System.Windows.Forms.Panel();
            this.pictOK = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MI_gototopic = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.pHelp = new System.Windows.Forms.PictureBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelEditButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pStrikeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pUnderline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pItalic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSearchNotes)).BeginInit();
            this.panelRtb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictOK)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // listTopics
            // 
            this.listTopics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listTopics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listTopics.FormattingEnabled = true;
            this.listTopics.IntegralHeight = false;
            this.listTopics.Location = new System.Drawing.Point(0, 19);
            this.listTopics.Name = "listTopics";
            this.listTopics.Size = new System.Drawing.Size(162, 273);
            this.listTopics.TabIndex = 0;
            this.listTopics.SelectedIndexChanged += new System.EventHandler(this.listTopics_SelectedIndexChanged);
            this.listTopics.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listTopics_KeyDown);
            this.listTopics.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listTopics_MouseDoubleClick);
            this.listTopics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listTopics_MouseDown);
            // 
            // rtb
            // 
            this.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtb.Location = new System.Drawing.Point(5, 0);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(417, 310);
            this.rtb.TabIndex = 1;
            this.rtb.Text = "";
            this.rtb.SelectionChanged += new System.EventHandler(this.rtb_SelectionChanged);
            this.rtb.TextChanged += new System.EventHandler(this.rtb_TextChanged);
            this.rtb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb_KeyDown);
            // 
            // txtSearchNotes
            // 
            this.txtSearchNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchNotes.Location = new System.Drawing.Point(356, 1);
            this.txtSearchNotes.Name = "txtSearchNotes";
            this.txtSearchNotes.Size = new System.Drawing.Size(196, 20);
            this.txtSearchNotes.TabIndex = 2;
            this.txtSearchNotes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTopics_MouseDoubleClick);
            this.txtSearchNotes.TextChanged += new System.EventHandler(this.txtSearchNotes_TextChanged);
            this.txtSearchNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchNotes_KeyDown);
            // 
            // panelEditButtons
            // 
            this.panelEditButtons.Controls.Add(this.pStrikeout);
            this.panelEditButtons.Controls.Add(this.pUnderline);
            this.panelEditButtons.Controls.Add(this.pItalic);
            this.panelEditButtons.Controls.Add(this.pBold);
            this.panelEditButtons.Controls.Add(this.fontDown);
            this.panelEditButtons.Controls.Add(this.fontUp);
            this.panelEditButtons.Location = new System.Drawing.Point(170, 1);
            this.panelEditButtons.Name = "panelEditButtons";
            this.panelEditButtons.Size = new System.Drawing.Size(185, 18);
            this.panelEditButtons.TabIndex = 3;
            // 
            // pStrikeout
            // 
            this.pStrikeout.Image = ((System.Drawing.Image)(resources.GetObject("pStrikeout.Image")));
            this.pStrikeout.Location = new System.Drawing.Point(123, 1);
            this.pStrikeout.Name = "pStrikeout";
            this.pStrikeout.Size = new System.Drawing.Size(16, 16);
            this.pStrikeout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pStrikeout.TabIndex = 8;
            this.pStrikeout.TabStop = false;
            this.pStrikeout.Click += new System.EventHandler(this.pStrikeout_Click);
            // 
            // pUnderline
            // 
            this.pUnderline.Image = ((System.Drawing.Image)(resources.GetObject("pUnderline.Image")));
            this.pUnderline.Location = new System.Drawing.Point(101, 1);
            this.pUnderline.Name = "pUnderline";
            this.pUnderline.Size = new System.Drawing.Size(16, 16);
            this.pUnderline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pUnderline.TabIndex = 7;
            this.pUnderline.TabStop = false;
            this.pUnderline.Click += new System.EventHandler(this.pUnderline_Click);
            // 
            // pItalic
            // 
            this.pItalic.Image = ((System.Drawing.Image)(resources.GetObject("pItalic.Image")));
            this.pItalic.Location = new System.Drawing.Point(79, 1);
            this.pItalic.Name = "pItalic";
            this.pItalic.Size = new System.Drawing.Size(16, 16);
            this.pItalic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pItalic.TabIndex = 6;
            this.pItalic.TabStop = false;
            this.pItalic.Click += new System.EventHandler(this.pItalic_Click);
            // 
            // pBold
            // 
            this.pBold.Image = ((System.Drawing.Image)(resources.GetObject("pBold.Image")));
            this.pBold.Location = new System.Drawing.Point(57, 1);
            this.pBold.Name = "pBold";
            this.pBold.Size = new System.Drawing.Size(16, 16);
            this.pBold.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBold.TabIndex = 5;
            this.pBold.TabStop = false;
            this.pBold.Click += new System.EventHandler(this.pBold_Click);
            // 
            // fontDown
            // 
            this.fontDown.Image = ((System.Drawing.Image)(resources.GetObject("fontDown.Image")));
            this.fontDown.Location = new System.Drawing.Point(24, 1);
            this.fontDown.Name = "fontDown";
            this.fontDown.Size = new System.Drawing.Size(16, 16);
            this.fontDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fontDown.TabIndex = 4;
            this.fontDown.TabStop = false;
            this.fontDown.Click += new System.EventHandler(this.fontDown_Click);
            // 
            // fontUp
            // 
            this.fontUp.Image = ((System.Drawing.Image)(resources.GetObject("fontUp.Image")));
            this.fontUp.Location = new System.Drawing.Point(2, 1);
            this.fontUp.Name = "fontUp";
            this.fontUp.Size = new System.Drawing.Size(16, 16);
            this.fontUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fontUp.TabIndex = 3;
            this.fontUp.TabStop = false;
            this.fontUp.Click += new System.EventHandler(this.fontUp_Click);
            // 
            // pSearchNotes
            // 
            this.pSearchNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pSearchNotes.BackColor = System.Drawing.Color.Transparent;
            this.pSearchNotes.Image = ((System.Drawing.Image)(resources.GetObject("pSearchNotes.Image")));
            this.pSearchNotes.Location = new System.Drawing.Point(556, 0);
            this.pSearchNotes.Name = "pSearchNotes";
            this.pSearchNotes.Size = new System.Drawing.Size(16, 16);
            this.pSearchNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pSearchNotes.TabIndex = 5;
            this.pSearchNotes.TabStop = false;
            this.pSearchNotes.Click += new System.EventHandler(this.pSearchNotes_Click);
            // 
            // lblTopics
            // 
            this.lblTopics.Location = new System.Drawing.Point(49, 3);
            this.lblTopics.Name = "lblTopics";
            this.lblTopics.Size = new System.Drawing.Size(57, 14);
            this.lblTopics.TabIndex = 8;
            this.lblTopics.Text = "Topics";
            this.lblTopics.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblMap
            // 
            this.lblMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMap.Location = new System.Drawing.Point(47, 295);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(64, 13);
            this.lblMap.TabIndex = 9;
            this.lblMap.Text = "Map:";
            this.lblMap.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtMapName
            // 
            this.txtMapName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMapName.Location = new System.Drawing.Point(0, 311);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(164, 20);
            this.txtMapName.TabIndex = 10;
            // 
            // panelRtb
            // 
            this.panelRtb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRtb.BackColor = System.Drawing.SystemColors.Window;
            this.panelRtb.Controls.Add(this.pictOK);
            this.panelRtb.Controls.Add(this.btnSave);
            this.panelRtb.Controls.Add(this.rtb);
            this.panelRtb.Location = new System.Drawing.Point(166, 19);
            this.panelRtb.Name = "panelRtb";
            this.panelRtb.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.panelRtb.Size = new System.Drawing.Size(427, 310);
            this.panelRtb.TabIndex = 11;
            // 
            // pictOK
            // 
            this.pictOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictOK.Image = ((System.Drawing.Image)(resources.GetObject("pictOK.Image")));
            this.pictOK.Location = new System.Drawing.Point(293, 289);
            this.pictOK.Margin = new System.Windows.Forms.Padding(6);
            this.pictOK.Name = "pictOK";
            this.pictOK.Size = new System.Drawing.Size(16, 16);
            this.pictOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictOK.TabIndex = 23;
            this.pictOK.TabStop = false;
            this.pictOK.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSave.ForeColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(312, 285);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save To Topic";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_gototopic,
            this.MI_remove});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(235, 48);
            // 
            // MI_gototopic
            // 
            this.MI_gototopic.Name = "MI_gototopic";
            this.MI_gototopic.Size = new System.Drawing.Size(234, 22);
            this.MI_gototopic.Text = "Go To Topic (Double Click)";
            // 
            // MI_remove
            // 
            this.MI_remove.Name = "MI_remove";
            this.MI_remove.Size = new System.Drawing.Size(234, 22);
            this.MI_remove.Text = "Remove From List (Delete key)";
            // 
            // pHelp
            // 
            this.pHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pHelp.BackColor = System.Drawing.Color.Transparent;
            this.pHelp.Image = ((System.Drawing.Image)(resources.GetObject("pHelp.Image")));
            this.pHelp.Location = new System.Drawing.Point(577, 0);
            this.pHelp.Name = "pHelp";
            this.pHelp.Size = new System.Drawing.Size(16, 16);
            this.pHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHelp.TabIndex = 13;
            this.pHelp.TabStop = false;
            this.pHelp.Click += new System.EventHandler(this.pHelp_Click);
            // 
            // TopicNotesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 331);
            this.Controls.Add(this.pHelp);
            this.Controls.Add(this.txtMapName);
            this.Controls.Add(this.lblMap);
            this.Controls.Add(this.lblTopics);
            this.Controls.Add(this.pSearchNotes);
            this.Controls.Add(this.panelEditButtons);
            this.Controls.Add(this.txtSearchNotes);
            this.Controls.Add(this.listTopics);
            this.Controls.Add(this.panelRtb);
            this.Name = "TopicNotesDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TopicNotesDlg";
            this.panelEditButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pStrikeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pUnderline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pItalic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSearchNotes)).EndInit();
            this.panelRtb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictOK)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.TextBox txtSearchNotes;
        private System.Windows.Forms.Panel panelEditButtons;
        private System.Windows.Forms.PictureBox pSearchNotes;
        private System.Windows.Forms.Label lblTopics;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.TextBox txtMapName;
        public System.Windows.Forms.ListBox listTopics;
        private System.Windows.Forms.Panel panelRtb;
        private System.Windows.Forms.PictureBox fontDown;
        private System.Windows.Forms.PictureBox fontUp;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MI_gototopic;
        private System.Windows.Forms.ToolStripMenuItem MI_remove;
        private System.Windows.Forms.PictureBox pHelp;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pUnderline;
        private System.Windows.Forms.PictureBox pItalic;
        private System.Windows.Forms.PictureBox pBold;
        private System.Windows.Forms.PictureBox pStrikeout;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictOK;
    }
}