namespace Bubbles
{
    partial class NewLinkDlg
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
            this.lblResult = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.TextBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.lblLink = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grBoxDownload = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.chDownload = new System.Windows.Forms.CheckBox();
            this.lblLinkGroup = new System.Windows.Forms.Label();
            this.cbLinkGroup = new System.Windows.Forms.ComboBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtComment = new System.Windows.Forms.TextBox();
            this.TextBoxMenu.SuspendLayout();
            this.grBoxDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblResult.Location = new System.Drawing.Point(92, 256);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(232, 15);
            this.lblResult.TabIndex = 38;
            this.lblResult.Text = "Link added successfully";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult.Visible = false;
            // 
            // lblWait
            // 
            this.lblWait.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWait.Location = new System.Drawing.Point(105, 51);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(201, 13);
            this.lblWait.TabIndex = 31;
            this.lblWait.Text = "Wait...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(14, 252);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 30;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(329, 252);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtLink
            // 
            this.txtLink.ContextMenuStrip = this.TextBoxMenu;
            this.txtLink.Location = new System.Drawing.Point(16, 25);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(356, 20);
            this.txtLink.TabIndex = 29;
            this.txtLink.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtLink_KeyUp);
            this.txtLink.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtLink_MouseDoubleClick);
            // 
            // TextBoxMenu
            // 
            this.TextBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteTxt});
            this.TextBoxMenu.Name = "TextBoxMenu";
            this.TextBoxMenu.Size = new System.Drawing.Size(103, 26);
            // 
            // pasteTxt
            // 
            this.pasteTxt.Name = "pasteTxt";
            this.pasteTxt.Size = new System.Drawing.Size(102, 22);
            this.pasteTxt.Text = "Paste";
            this.pasteTxt.Click += new System.EventHandler(this.paste_Click);
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(13, 9);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(30, 13);
            this.lblLink.TabIndex = 28;
            this.lblLink.Text = "Link:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(16, 71);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(388, 20);
            this.txtTitle.TabIndex = 27;
            this.txtTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtTitle_MouseDoubleClick);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(13, 55);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 26;
            this.lblTitle.Text = "Title:";
            // 
            // grBoxDownload
            // 
            this.grBoxDownload.Controls.Add(this.btnPreview);
            this.grBoxDownload.Controls.Add(this.chDownload);
            this.grBoxDownload.Location = new System.Drawing.Point(16, 189);
            this.grBoxDownload.Name = "grBoxDownload";
            this.grBoxDownload.Size = new System.Drawing.Size(387, 52);
            this.grBoxDownload.TabIndex = 33;
            this.grBoxDownload.TabStop = false;
            this.grBoxDownload.Text = "You can download page and add link to the downloaded file";
            this.grBoxDownload.Visible = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(215, 20);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(108, 23);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // chDownload
            // 
            this.chDownload.AutoSize = true;
            this.chDownload.Location = new System.Drawing.Point(61, 23);
            this.chDownload.Name = "chDownload";
            this.chDownload.Size = new System.Drawing.Size(116, 17);
            this.chDownload.TabIndex = 20;
            this.chDownload.Text = "Скачать страницу";
            this.chDownload.UseVisualStyleBackColor = true;
            // 
            // lblLinkGroup
            // 
            this.lblLinkGroup.AutoSize = true;
            this.lblLinkGroup.Location = new System.Drawing.Point(13, 107);
            this.lblLinkGroup.Name = "lblLinkGroup";
            this.lblLinkGroup.Size = new System.Drawing.Size(62, 13);
            this.lblLinkGroup.TabIndex = 34;
            this.lblLinkGroup.Text = "Link Group:";
            // 
            // cbLinkGroup
            // 
            this.cbLinkGroup.FormattingEnabled = true;
            this.cbLinkGroup.Location = new System.Drawing.Point(93, 103);
            this.cbLinkGroup.Name = "cbLinkGroup";
            this.cbLinkGroup.Size = new System.Drawing.Size(310, 21);
            this.cbLinkGroup.TabIndex = 35;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBrowse.Location = new System.Drawing.Point(373, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(30, 22);
            this.btnBrowse.TabIndex = 36;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtComment
            // 
            this.txtComment.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtComment.Location = new System.Drawing.Point(16, 136);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(387, 44);
            this.txtComment.TabIndex = 37;
            this.txtComment.Enter += new System.EventHandler(this.txtComment_Enter);
            this.txtComment.Leave += new System.EventHandler(this.txtComment_Leave);
            // 
            // NewLinkDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(420, 288);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.cbLinkGroup);
            this.Controls.Add(this.lblLinkGroup);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.lblLink);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grBoxDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLinkDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NewLinkDlg";
            this.TextBoxMenu.ResumeLayout(false);
            this.grBoxDownload.ResumeLayout(false);
            this.grBoxDownload.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label lblLinkGroup;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public System.Windows.Forms.TextBox txtLink;
        public System.Windows.Forms.TextBox txtTitle;
        public System.Windows.Forms.ComboBox cbLinkGroup;
        public System.Windows.Forms.GroupBox grBoxDownload;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ContextMenuStrip TextBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem pasteTxt;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.CheckBox chDownload;
    }
}