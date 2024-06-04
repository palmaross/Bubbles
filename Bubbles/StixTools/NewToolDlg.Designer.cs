namespace Bubbles
{
    partial class NewToolDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewToolDlg));
            this.btnAddTool = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblToolName = new System.Windows.Forms.Label();
            this.lblTooltip = new System.Windows.Forms.Label();
            this.txtTooltip = new System.Windows.Forms.TextBox();
            this.pIcon = new System.Windows.Forms.PictureBox();
            this.lblToolIcon = new System.Windows.Forms.Label();
            this.lblChangeIcon = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblWait = new System.Windows.Forms.Label();
            this.lblSpecifyPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmsTextBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtboxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.txtboxClear = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTip = new System.Windows.Forms.Label();
            this.chAddToDatabase = new System.Windows.Forms.CheckBox();
            this.btnPages = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).BeginInit();
            this.cmsTextBoxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddTool
            // 
            this.btnAddTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddTool.Location = new System.Drawing.Point(11, 239);
            this.btnAddTool.Name = "btnAddTool";
            this.btnAddTool.Size = new System.Drawing.Size(75, 23);
            this.btnAddTool.TabIndex = 7;
            this.btnAddTool.Text = "Add";
            this.btnAddTool.UseVisualStyleBackColor = true;
            this.btnAddTool.Click += new System.EventHandler(this.btnAddTool_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(240, 239);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(91, 106);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(224, 20);
            this.txtTitle.TabIndex = 5;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // lblToolName
            // 
            this.lblToolName.AutoSize = true;
            this.lblToolName.Location = new System.Drawing.Point(7, 109);
            this.lblToolName.Name = "lblToolName";
            this.lblToolName.Size = new System.Drawing.Size(62, 13);
            this.lblToolName.TabIndex = 4;
            this.lblToolName.Text = "Tool Name:";
            // 
            // lblTooltip
            // 
            this.lblTooltip.AutoSize = true;
            this.lblTooltip.Location = new System.Drawing.Point(8, 171);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(209, 13);
            this.lblTooltip.TabIndex = 33;
            this.lblTooltip.Text = "Tooltip when  the mouse hover over a tool:";
            // 
            // txtTooltip
            // 
            this.txtTooltip.Location = new System.Drawing.Point(10, 187);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(305, 20);
            this.txtTooltip.TabIndex = 32;
            // 
            // pIcon
            // 
            this.pIcon.Image = ((System.Drawing.Image)(resources.GetObject("pIcon.Image")));
            this.pIcon.Location = new System.Drawing.Point(91, 140);
            this.pIcon.Name = "pIcon";
            this.pIcon.Size = new System.Drawing.Size(20, 20);
            this.pIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIcon.TabIndex = 37;
            this.pIcon.TabStop = false;
            this.pIcon.Click += new System.EventHandler(this.pIcon_Click);
            // 
            // lblToolIcon
            // 
            this.lblToolIcon.AutoSize = true;
            this.lblToolIcon.Location = new System.Drawing.Point(7, 144);
            this.lblToolIcon.Name = "lblToolIcon";
            this.lblToolIcon.Size = new System.Drawing.Size(81, 13);
            this.lblToolIcon.TabIndex = 36;
            this.lblToolIcon.Text = "Значок инстр.:";
            // 
            // lblChangeIcon
            // 
            this.lblChangeIcon.AutoSize = true;
            this.lblChangeIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeIcon.Location = new System.Drawing.Point(119, 144);
            this.lblChangeIcon.Name = "lblChangeIcon";
            this.lblChangeIcon.Size = new System.Drawing.Size(169, 13);
            this.lblChangeIcon.TabIndex = 40;
            this.lblChangeIcon.Text = "Click icon if you want to change it.";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(284, 61);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 22);
            this.btnBrowse.TabIndex = 42;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblWait
            // 
            this.lblWait.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWait.Location = new System.Drawing.Point(55, 87);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(201, 13);
            this.lblWait.TabIndex = 44;
            this.lblWait.Text = "Wait...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // lblSpecifyPath
            // 
            this.lblSpecifyPath.AutoSize = true;
            this.lblSpecifyPath.Location = new System.Drawing.Point(7, 47);
            this.lblSpecifyPath.Name = "lblSpecifyPath";
            this.lblSpecifyPath.Size = new System.Drawing.Size(254, 13);
            this.lblSpecifyPath.TabIndex = 43;
            this.lblSpecifyPath.Text = "Укажите путь к файлу или вставьте веб-ссылку:";
            // 
            // txtPath
            // 
            this.txtPath.ContextMenuStrip = this.cmsTextBoxMenu;
            this.txtPath.Location = new System.Drawing.Point(10, 63);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(270, 20);
            this.txtPath.TabIndex = 41;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            this.txtPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyUp);
            this.txtPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtPath_MouseDoubleClick);
            // 
            // cmsTextBoxMenu
            // 
            this.cmsTextBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtboxPaste,
            this.txtboxClear});
            this.cmsTextBoxMenu.Name = "cmsTextBoxMenu";
            this.cmsTextBoxMenu.Size = new System.Drawing.Size(103, 48);
            // 
            // txtboxPaste
            // 
            this.txtboxPaste.Name = "txtboxPaste";
            this.txtboxPaste.Size = new System.Drawing.Size(102, 22);
            this.txtboxPaste.Text = "Paste";
            this.txtboxPaste.Click += new System.EventHandler(this.txtboxPaste_Click);
            // 
            // txtboxClear
            // 
            this.txtboxClear.Name = "txtboxClear";
            this.txtboxClear.Size = new System.Drawing.Size(102, 22);
            this.txtboxClear.Text = "Clear";
            this.txtboxClear.Click += new System.EventHandler(this.txtboxClear_Click);
            // 
            // lblTip
            // 
            this.lblTip.BackColor = System.Drawing.SystemColors.Info;
            this.lblTip.Location = new System.Drawing.Point(10, 11);
            this.lblTip.Name = "lblTip";
            this.lblTip.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lblTip.Size = new System.Drawing.Size(306, 27);
            this.lblTip.TabIndex = 47;
            this.lblTip.Text = "The tool name and icon are generated automatically after you specify the link to " +
    "a file or webpage, or press the Enter key.";
            // 
            // chAddToDatabase
            // 
            this.chAddToDatabase.AutoSize = true;
            this.chAddToDatabase.Location = new System.Drawing.Point(12, 216);
            this.chAddToDatabase.Name = "chAddToDatabase";
            this.chAddToDatabase.Size = new System.Drawing.Size(151, 17);
            this.chAddToDatabase.TabIndex = 49;
            this.chAddToDatabase.Text = "Добавить в базу данных";
            this.chAddToDatabase.UseVisualStyleBackColor = true;
            // 
            // btnPages
            // 
            this.btnPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPages.BackColor = System.Drawing.SystemColors.Info;
            this.btnPages.Location = new System.Drawing.Point(126, 239);
            this.btnPages.Name = "btnPages";
            this.btnPages.Size = new System.Drawing.Size(75, 23);
            this.btnPages.TabIndex = 51;
            this.btnPages.Text = "Pages";
            this.btnPages.UseVisualStyleBackColor = false;
            this.btnPages.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // NewToolDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(326, 271);
            this.Controls.Add(this.btnPages);
            this.Controls.Add(this.chAddToDatabase);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.lblSpecifyPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblChangeIcon);
            this.Controls.Add(this.pIcon);
            this.Controls.Add(this.lblToolIcon);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.btnAddTool);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblToolName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewToolDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "New Tool";
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).EndInit();
            this.cmsTextBoxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddTool;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblToolName;
        private System.Windows.Forms.Label lblTooltip;
        public System.Windows.Forms.TextBox txtTooltip;
        private System.Windows.Forms.Label lblToolIcon;
        public System.Windows.Forms.PictureBox pIcon;
        private System.Windows.Forms.Label lblChangeIcon;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label lblSpecifyPath;
        public System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.CheckBox chAddToDatabase;
        private System.Windows.Forms.Button btnPages;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ContextMenuStrip cmsTextBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem txtboxPaste;
        private System.Windows.Forms.ToolStripMenuItem txtboxClear;
    }
}