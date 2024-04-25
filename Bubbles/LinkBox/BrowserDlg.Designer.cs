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
            this.panelModify = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLink2 = new System.Windows.Forms.TextBox();
            this.lblLink = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grBoxDownload = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.cbDownload = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pDraw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemove)).BeginInit();
            this.cmsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pGoBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGoForward)).BeginInit();
            this.panelModify.SuspendLayout();
            this.grBoxDownload.SuspendLayout();
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
            this.txtAddressBar.Size = new System.Drawing.Size(761, 21);
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
            this.tabPage1.Controls.Add(this.panelModify);
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
            this.btnAddSubtopic.Location = new System.Drawing.Point(12, 418);
            this.btnAddSubtopic.Name = "btnAddSubtopic";
            this.btnAddSubtopic.Size = new System.Drawing.Size(140, 23);
            this.btnAddSubtopic.TabIndex = 9;
            this.btnAddSubtopic.Text = "Add as Subtopic";
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
            this.btnAddNotes.Location = new System.Drawing.Point(158, 418);
            this.btnAddNotes.Name = "btnAddNotes";
            this.btnAddNotes.Size = new System.Drawing.Size(140, 23);
            this.btnAddNotes.TabIndex = 11;
            this.btnAddNotes.Text = "Добавить в примечания";
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
            this.btnSaveLink.Location = new System.Drawing.Point(358, 418);
            this.btnSaveLink.Name = "btnSaveLink";
            this.btnSaveLink.Size = new System.Drawing.Size(140, 23);
            this.btnSaveLink.TabIndex = 13;
            this.btnSaveLink.Text = "Save Link";
            this.btnSaveLink.UseVisualStyleBackColor = true;
            // 
            // panelModify
            // 
            this.panelModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelModify.BackColor = System.Drawing.Color.SeaShell;
            this.panelModify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModify.Controls.Add(this.lblResult);
            this.panelModify.Controls.Add(this.lblWait);
            this.panelModify.Controls.Add(this.btnOK);
            this.panelModify.Controls.Add(this.button1);
            this.panelModify.Controls.Add(this.txtLink2);
            this.panelModify.Controls.Add(this.lblLink);
            this.panelModify.Controls.Add(this.txtTitle);
            this.panelModify.Controls.Add(this.lblTitle);
            this.panelModify.Controls.Add(this.grBoxDownload);
            this.panelModify.Location = new System.Drawing.Point(177, 74);
            this.panelModify.Name = "panelModify";
            this.panelModify.Size = new System.Drawing.Size(450, 202);
            this.panelModify.TabIndex = 23;
            this.panelModify.Visible = false;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblResult.Location = new System.Drawing.Point(91, 168);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(265, 15);
            this.lblResult.TabIndex = 23;
            this.lblResult.Text = "Link added successfully";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult.Visible = false;
            // 
            // lblWait
            // 
            this.lblWait.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWait.Location = new System.Drawing.Point(146, 41);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(201, 13);
            this.lblWait.TabIndex = 18;
            this.lblWait.Text = "Wait...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(10, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(362, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtLink2
            // 
            this.txtLink2.Location = new System.Drawing.Point(72, 15);
            this.txtLink2.Name = "txtLink2";
            this.txtLink2.Size = new System.Drawing.Size(365, 20);
            this.txtLink2.TabIndex = 13;
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(9, 18);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(30, 13);
            this.lblLink.TabIndex = 12;
            this.lblLink.Text = "Link:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(72, 61);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(365, 20);
            this.txtTitle.TabIndex = 11;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(9, 64);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Title:";
            // 
            // grBoxDownload
            // 
            this.grBoxDownload.Controls.Add(this.btnPreview);
            this.grBoxDownload.Controls.Add(this.cbDownload);
            this.grBoxDownload.Location = new System.Drawing.Point(72, 99);
            this.grBoxDownload.Name = "grBoxDownload";
            this.grBoxDownload.Size = new System.Drawing.Size(365, 52);
            this.grBoxDownload.TabIndex = 24;
            this.grBoxDownload.TabStop = false;
            this.grBoxDownload.Text = "You can download page and add link to the downloaded file";
            this.grBoxDownload.Visible = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(206, 20);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(108, 23);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // cbDownload
            // 
            this.cbDownload.AutoSize = true;
            this.cbDownload.Location = new System.Drawing.Point(52, 23);
            this.cbDownload.Name = "cbDownload";
            this.cbDownload.Size = new System.Drawing.Size(116, 17);
            this.cbDownload.TabIndex = 20;
            this.cbDownload.Text = "Скачать страницу";
            this.cbDownload.UseVisualStyleBackColor = true;
            // 
            // BrowserDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(813, 450);
            this.Controls.Add(this.btnSaveLink);
            this.Controls.Add(this.pGoForward);
            this.Controls.Add(this.pGoBack);
            this.Controls.Add(this.btnAddNotes);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddSubtopic);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtAddressBar);
            this.Name = "BrowserDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OmniStix Browser";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pDraw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemove)).EndInit();
            this.cmsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pGoBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGoForward)).EndInit();
            this.panelModify.ResumeLayout(false);
            this.panelModify.PerformLayout();
            this.grBoxDownload.ResumeLayout(false);
            this.grBoxDownload.PerformLayout();
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
        private System.Windows.Forms.Panel panelModify;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLink2;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grBoxDownload;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.CheckBox cbDownload;
    }
}