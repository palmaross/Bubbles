namespace Bubbles
{
    partial class BrowserDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserDlg));
            this.txtAddressBar = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelMinimized = new System.Windows.Forms.Panel();
            this.pDraw = new System.Windows.Forms.PictureBox();
            this.pRemove = new System.Windows.Forms.PictureBox();
            this.btnAddSubtopic = new System.Windows.Forms.Button();
            this.cmsTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNotes = new System.Windows.Forms.Button();
            this.pGoBack = new System.Windows.Forms.PictureBox();
            this.pGoForward = new System.Windows.Forms.PictureBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveLink = new System.Windows.Forms.Button();
            this.btnAddLinkToTopic = new System.Windows.Forms.Button();
            this.pSearch = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pDraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemove)).BeginInit();
            this.cmsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pGoBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGoForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAddressBar
            // 
            this.txtAddressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddressBar.BackColor = System.Drawing.SystemColors.Window;
            this.txtAddressBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtAddressBar.Location = new System.Drawing.Point(48, 8);
            this.txtAddressBar.Name = "txtAddressBar";
            this.txtAddressBar.Size = new System.Drawing.Size(738, 21);
            this.txtAddressBar.TabIndex = 6;
            this.txtAddressBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAddressBar_KeyUp);
            this.txtAddressBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtAddressBar_MouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(813, 378);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panelMinimized);
            this.tabPage1.Controls.Add(this.pDraw);
            this.tabPage1.Controls.Add(this.pRemove);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(805, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New";
            // 
            // panelMinimized
            // 
            this.panelMinimized.Location = new System.Drawing.Point(44, 15);
            this.panelMinimized.Name = "panelMinimized";
            this.panelMinimized.Size = new System.Drawing.Size(260, 37);
            this.panelMinimized.TabIndex = 25;
            this.panelMinimized.Visible = false;
            // 
            // pDraw
            // 
            this.pDraw.BackColor = System.Drawing.Color.Red;
            this.pDraw.Image = ((System.Drawing.Image)(resources.GetObject("pDraw.Image")));
            this.pDraw.Location = new System.Drawing.Point(479, 51);
            this.pDraw.Name = "pDraw";
            this.pDraw.Size = new System.Drawing.Size(4, 1);
            this.pDraw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pDraw.TabIndex = 1;
            this.pDraw.TabStop = false;
            this.pDraw.Visible = false;
            // 
            // pRemove
            // 
            this.pRemove.Image = ((System.Drawing.Image)(resources.GetObject("pRemove.Image")));
            this.pRemove.Location = new System.Drawing.Point(540, 51);
            this.pRemove.Name = "pRemove";
            this.pRemove.Size = new System.Drawing.Size(16, 16);
            this.pRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRemove.TabIndex = 0;
            this.pRemove.TabStop = false;
            this.pRemove.Visible = false;
            // 
            // btnAddSubtopic
            // 
            this.btnAddSubtopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddSubtopic.Location = new System.Drawing.Point(10, 418);
            this.btnAddSubtopic.Name = "btnAddSubtopic";
            this.btnAddSubtopic.Size = new System.Drawing.Size(140, 23);
            this.btnAddSubtopic.TabIndex = 9;
            this.btnAddSubtopic.Text = "Add Text as Subtopic";
            this.btnAddSubtopic.UseVisualStyleBackColor = true;
            this.btnAddSubtopic.Click += new System.EventHandler(this.btnAddAsSubtopic_Click);
            // 
            // cmsTab
            // 
            this.cmsTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabRemove});
            this.cmsTab.Name = "cmsTab";
            this.cmsTab.Size = new System.Drawing.Size(118, 26);
            // 
            // tabRemove
            // 
            this.tabRemove.Name = "tabRemove";
            this.tabRemove.Size = new System.Drawing.Size(117, 22);
            this.tabRemove.Text = "Remove";
            this.tabRemove.Click += new System.EventHandler(this.tabRemove_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(724, 418);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddNotes
            // 
            this.btnAddNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNotes.Location = new System.Drawing.Point(156, 418);
            this.btnAddNotes.Name = "btnAddNotes";
            this.btnAddNotes.Size = new System.Drawing.Size(140, 23);
            this.btnAddNotes.TabIndex = 11;
            this.btnAddNotes.Text = "Add Text to Topic Notes";
            this.btnAddNotes.UseVisualStyleBackColor = true;
            this.btnAddNotes.Click += new System.EventHandler(this.btnAddToTopicNotes_Click);
            // 
            // pGoBack
            // 
            this.pGoBack.Image = ((System.Drawing.Image)(resources.GetObject("pGoBack.Image")));
            this.pGoBack.Location = new System.Drawing.Point(5, 10);
            this.pGoBack.Name = "pGoBack";
            this.pGoBack.Size = new System.Drawing.Size(16, 16);
            this.pGoBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pGoBack.TabIndex = 1;
            this.pGoBack.TabStop = false;
            this.pGoBack.Click += new System.EventHandler(this.pGoBack_Click);
            // 
            // pGoForward
            // 
            this.pGoForward.Image = ((System.Drawing.Image)(resources.GetObject("pGoForward.Image")));
            this.pGoForward.Location = new System.Drawing.Point(25, 10);
            this.pGoForward.Name = "pGoForward";
            this.pGoForward.Size = new System.Drawing.Size(16, 16);
            this.pGoForward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pGoForward.TabIndex = 12;
            this.pGoForward.TabStop = false;
            this.pGoForward.Click += new System.EventHandler(this.pGoForward_Click);
            // 
            // btnSaveLink
            // 
            this.btnSaveLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveLink.Location = new System.Drawing.Point(507, 418);
            this.btnSaveLink.Name = "btnSaveLink";
            this.btnSaveLink.Size = new System.Drawing.Size(148, 23);
            this.btnSaveLink.TabIndex = 13;
            this.btnSaveLink.Text = "Save Link to OmniLinks";
            this.btnSaveLink.UseVisualStyleBackColor = true;
            this.btnSaveLink.Click += new System.EventHandler(this.btnSaveLink_Click);
            // 
            // btnAddLinkToTopic
            // 
            this.btnAddLinkToTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddLinkToTopic.Location = new System.Drawing.Point(353, 418);
            this.btnAddLinkToTopic.Name = "btnAddLinkToTopic";
            this.btnAddLinkToTopic.Size = new System.Drawing.Size(148, 23);
            this.btnAddLinkToTopic.TabIndex = 15;
            this.btnAddLinkToTopic.Text = "Добавить ссылку на тему";
            this.btnAddLinkToTopic.UseVisualStyleBackColor = true;
            this.btnAddLinkToTopic.Click += new System.EventHandler(this.btnAddLinkToTopic_Click);
            // 
            // pSearch
            // 
            this.pSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pSearch.Image = ((System.Drawing.Image)(resources.GetObject("pSearch.Image")));
            this.pSearch.Location = new System.Drawing.Point(792, 10);
            this.pSearch.Name = "pSearch";
            this.pSearch.Size = new System.Drawing.Size(16, 16);
            this.pSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pSearch.TabIndex = 16;
            this.pSearch.TabStop = false;
            this.pSearch.Click += new System.EventHandler(this.pSearch_Click);
            // 
            // BrowserDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(813, 450);
            this.Controls.Add(this.pSearch);
            this.Controls.Add(this.btnAddLinkToTopic);
            this.Controls.Add(this.btnSaveLink);
            this.Controls.Add(this.pGoForward);
            this.Controls.Add(this.pGoBack);
            this.Controls.Add(this.btnAddNotes);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddSubtopic);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtAddressBar);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BrowserDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OmniStix Browser";
            this.Load += new System.EventHandler(this.BrowserDlg_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pDraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemove)).EndInit();
            this.cmsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pGoBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGoForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnAddSubtopic;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TextBox txtAddressBar;
        private System.Windows.Forms.ContextMenuStrip cmsTab;
        private System.Windows.Forms.ToolStripMenuItem tabRemove;
        private System.Windows.Forms.PictureBox pRemove;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddNotes;
        private System.Windows.Forms.PictureBox pGoBack;
        private System.Windows.Forms.PictureBox pGoForward;
        private System.Windows.Forms.PictureBox pDraw;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSaveLink;
        private System.Windows.Forms.Button btnAddLinkToTopic;
        private System.Windows.Forms.PictureBox pSearch;
        private System.Windows.Forms.Panel panelMinimized;
    }
}