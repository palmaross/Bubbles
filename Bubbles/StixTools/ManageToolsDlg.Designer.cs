namespace Bubbles
{
    partial class ManageToolsDlg
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
            this.btnClose = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listWindowsApps = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.p1 = new System.Windows.Forms.PictureBox();
            this.cmsTool = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.t_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.t_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.t_run = new System.Windows.Forms.ToolStripMenuItem();
            this.splitRight = new System.Windows.Forms.SplitContainer();
            this.listOmniTools = new System.Windows.Forms.ListView();
            this.btnAddToStix = new System.Windows.Forms.Button();
            this.groupAddTool = new System.Windows.Forms.GroupBox();
            this.lblTooltip = new System.Windows.Forms.Label();
            this.txtTooltip = new System.Windows.Forms.TextBox();
            this.chAddToStix = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblWait = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSpecifyPath = new System.Windows.Forms.Label();
            this.btnAddTool = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmsTextBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtboxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.txtboxClear = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAddTool = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.cmsTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
            this.splitRight.Panel1.SuspendLayout();
            this.splitRight.Panel2.SuspendLayout();
            this.splitRight.SuspendLayout();
            this.groupAddTool.SuspendLayout();
            this.cmsTextBoxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(257, 593);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // listWindowsApps
            // 
            this.listWindowsApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listWindowsApps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listWindowsApps.HideSelection = false;
            this.listWindowsApps.Location = new System.Drawing.Point(0, 0);
            this.listWindowsApps.Name = "listWindowsApps";
            this.listWindowsApps.Size = new System.Drawing.Size(314, 278);
            this.listWindowsApps.SmallImageList = this.imageList1;
            this.listWindowsApps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listWindowsApps.TabIndex = 10;
            this.listWindowsApps.UseCompatibleStateImageBehavior = false;
            this.listWindowsApps.View = System.Windows.Forms.View.SmallIcon;
            this.listWindowsApps.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(248, 53);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.TabIndex = 15;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // cmsTool
            // 
            this.cmsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.t_rename,
            this.t_remove,
            this.t_run});
            this.cmsTool.Name = "cmsTool";
            this.cmsTool.Size = new System.Drawing.Size(118, 70);
            // 
            // t_rename
            // 
            this.t_rename.Name = "t_rename";
            this.t_rename.Size = new System.Drawing.Size(117, 22);
            this.t_rename.Text = "Rename";
            // 
            // t_remove
            // 
            this.t_remove.Name = "t_remove";
            this.t_remove.Size = new System.Drawing.Size(117, 22);
            this.t_remove.Text = "Remove";
            // 
            // t_run
            // 
            this.t_run.Name = "t_run";
            this.t_run.Size = new System.Drawing.Size(117, 22);
            this.t_run.Text = "Run";
            // 
            // splitRight
            // 
            this.splitRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitRight.Location = new System.Drawing.Point(12, 12);
            this.splitRight.Name = "splitRight";
            this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRight.Panel1
            // 
            this.splitRight.Panel1.Controls.Add(this.listWindowsApps);
            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.listOmniTools);
            this.splitRight.Size = new System.Drawing.Size(314, 366);
            this.splitRight.SplitterDistance = 278;
            this.splitRight.TabIndex = 20;
            // 
            // listOmniTools
            // 
            this.listOmniTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listOmniTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listOmniTools.HideSelection = false;
            this.listOmniTools.LabelEdit = true;
            this.listOmniTools.Location = new System.Drawing.Point(0, 0);
            this.listOmniTools.Name = "listOmniTools";
            this.listOmniTools.Size = new System.Drawing.Size(314, 84);
            this.listOmniTools.SmallImageList = this.imageList1;
            this.listOmniTools.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listOmniTools.TabIndex = 0;
            this.listOmniTools.UseCompatibleStateImageBehavior = false;
            this.listOmniTools.View = System.Windows.Forms.View.Details;
            this.listOmniTools.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listOmniTools_AfterLabelEdit);
            this.listOmniTools.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listOmniTools_BeforeLabelEdit);
            this.listOmniTools.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDown);
            // 
            // btnAddToStix
            // 
            this.btnAddToStix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddToStix.Location = new System.Drawing.Point(12, 593);
            this.btnAddToStix.Name = "btnAddToStix";
            this.btnAddToStix.Size = new System.Drawing.Size(112, 23);
            this.btnAddToStix.TabIndex = 21;
            this.btnAddToStix.Tag = "";
            this.btnAddToStix.Text = "Добавить на стик";
            this.btnAddToStix.UseVisualStyleBackColor = true;
            this.btnAddToStix.Click += new System.EventHandler(this.btnAddToStix_Click);
            // 
            // groupAddTool
            // 
            this.groupAddTool.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupAddTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupAddTool.Controls.Add(this.label2);
            this.groupAddTool.Controls.Add(this.label1);
            this.groupAddTool.Controls.Add(this.lblTooltip);
            this.groupAddTool.Controls.Add(this.txtTooltip);
            this.groupAddTool.Controls.Add(this.chAddToStix);
            this.groupAddTool.Controls.Add(this.btnBrowse);
            this.groupAddTool.Controls.Add(this.lblWait);
            this.groupAddTool.Controls.Add(this.txtTitle);
            this.groupAddTool.Controls.Add(this.lblTitle);
            this.groupAddTool.Controls.Add(this.lblSpecifyPath);
            this.groupAddTool.Controls.Add(this.btnAddTool);
            this.groupAddTool.Controls.Add(this.txtPath);
            this.groupAddTool.Location = new System.Drawing.Point(8, 398);
            this.groupAddTool.Name = "groupAddTool";
            this.groupAddTool.Size = new System.Drawing.Size(323, 186);
            this.groupAddTool.TabIndex = 22;
            this.groupAddTool.TabStop = false;
            // 
            // lblTooltip
            // 
            this.lblTooltip.AutoSize = true;
            this.lblTooltip.Location = new System.Drawing.Point(7, 113);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(209, 13);
            this.lblTooltip.TabIndex = 31;
            this.lblTooltip.Text = "Tooltip when  the mouse hover over a tool:";
            // 
            // txtTooltip
            // 
            this.txtTooltip.Location = new System.Drawing.Point(9, 129);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(306, 20);
            this.txtTooltip.TabIndex = 30;
            // 
            // chAddToStix
            // 
            this.chAddToStix.AutoSize = true;
            this.chAddToStix.Checked = true;
            this.chAddToStix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chAddToStix.Location = new System.Drawing.Point(164, 161);
            this.chAddToStix.Name = "chAddToStix";
            this.chAddToStix.Size = new System.Drawing.Size(77, 17);
            this.chAddToStix.TabIndex = 29;
            this.chAddToStix.Text = "Add to Stix";
            this.chAddToStix.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(285, 37);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 22);
            this.btnBrowse.TabIndex = 22;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblWait
            // 
            this.lblWait.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblWait.Location = new System.Drawing.Point(54, 66);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(201, 13);
            this.lblWait.TabIndex = 28;
            this.lblWait.Text = "Wait...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(88, 84);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(227, 20);
            this.txtTitle.TabIndex = 27;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(18, 87);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(67, 13);
            this.lblTitle.TabIndex = 26;
            this.lblTitle.Text = "Имя инстр.:";
            // 
            // lblSpecifyPath
            // 
            this.lblSpecifyPath.AutoSize = true;
            this.lblSpecifyPath.Location = new System.Drawing.Point(18, 21);
            this.lblSpecifyPath.Name = "lblSpecifyPath";
            this.lblSpecifyPath.Size = new System.Drawing.Size(254, 13);
            this.lblSpecifyPath.TabIndex = 25;
            this.lblSpecifyPath.Text = "Укажите путь к файлу или вставьте веб-ссылку:";
            // 
            // btnAddTool
            // 
            this.btnAddTool.Enabled = false;
            this.btnAddTool.Location = new System.Drawing.Point(86, 156);
            this.btnAddTool.Name = "btnAddTool";
            this.btnAddTool.Size = new System.Drawing.Size(68, 23);
            this.btnAddTool.TabIndex = 24;
            this.btnAddTool.Text = "Add";
            this.btnAddTool.UseVisualStyleBackColor = true;
            this.btnAddTool.Click += new System.EventHandler(this.btnAddTool_Click);
            // 
            // txtPath
            // 
            this.txtPath.ContextMenuStrip = this.cmsTextBoxMenu;
            this.txtPath.Location = new System.Drawing.Point(9, 40);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(270, 20);
            this.txtPath.TabIndex = 21;
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
            // lblAddTool
            // 
            this.lblAddTool.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblAddTool.AutoSize = true;
            this.lblAddTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblAddTool.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddTool.Location = new System.Drawing.Point(128, 390);
            this.lblAddTool.Name = "lblAddTool";
            this.lblAddTool.Size = new System.Drawing.Size(83, 15);
            this.lblAddTool.TabIndex = 30;
            this.lblAddTool.Text = "Add New Tool";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(7, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "*";
            // 
            // ManageToolsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(339, 626);
            this.Controls.Add(this.lblAddTool);
            this.Controls.Add(this.groupAddTool);
            this.Controls.Add(this.btnAddToStix);
            this.Controls.Add(this.splitRight);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.btnClose);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageToolsDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WindowsToolsDlg";
            this.Resize += new System.EventHandler(this.WindowsToolsDlg_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.cmsTool.ResumeLayout(false);
            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
            this.splitRight.ResumeLayout(false);
            this.groupAddTool.ResumeLayout(false);
            this.groupAddTool.PerformLayout();
            this.cmsTextBoxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListView listWindowsApps;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.ContextMenuStrip cmsTool;
        private System.Windows.Forms.ToolStripMenuItem t_rename;
        private System.Windows.Forms.ToolStripMenuItem t_remove;
        private System.Windows.Forms.ToolStripMenuItem t_run;
        private System.Windows.Forms.SplitContainer splitRight;
        private System.Windows.Forms.ListView listOmniTools;
        private System.Windows.Forms.Button btnAddToStix;
        private System.Windows.Forms.GroupBox groupAddTool;
        private System.Windows.Forms.Label lblWait;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSpecifyPath;
        private System.Windows.Forms.Button btnAddTool;
        public System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chAddToStix;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip cmsTextBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem txtboxPaste;
        private System.Windows.Forms.ToolStripMenuItem txtboxClear;
        private System.Windows.Forms.Label lblAddTool;
        private System.Windows.Forms.Label lblTooltip;
        public System.Windows.Forms.TextBox txtTooltip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}