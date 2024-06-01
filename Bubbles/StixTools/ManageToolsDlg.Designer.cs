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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageToolsDlg));
            this.btnClose = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listWindowsApps = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.p1 = new System.Windows.Forms.PictureBox();
            this.cmsTool = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.t_edittool = new System.Windows.Forms.ToolStripMenuItem();
            this.t_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.t_run = new System.Windows.Forms.ToolStripMenuItem();
            this.splitPanel = new System.Windows.Forms.SplitContainer();
            this.listOmniTools = new System.Windows.Forms.ListView();
            this.btnAddToStix = new System.Windows.Forms.Button();
            this.groupAddTool = new System.Windows.Forms.GroupBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.lblChangeIcon = new System.Windows.Forms.Label();
            this.btnCloseAddTool = new System.Windows.Forms.Button();
            this.pIcon = new System.Windows.Forms.PictureBox();
            this.lblToolIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panelNewTool = new System.Windows.Forms.Panel();
            this.btnNewTool = new System.Windows.Forms.Button();
            this.paneEditTool = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pIcon2 = new System.Windows.Forms.PictureBox();
            this.lblToolIcon2 = new System.Windows.Forms.Label();
            this.lblTooltip2 = new System.Windows.Forms.Label();
            this.txtTooltip2 = new System.Windows.Forms.TextBox();
            this.txtTitle2 = new System.Windows.Forms.TextBox();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblChangeIcon2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.cmsTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).BeginInit();
            this.splitPanel.Panel1.SuspendLayout();
            this.splitPanel.Panel2.SuspendLayout();
            this.splitPanel.SuspendLayout();
            this.groupAddTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).BeginInit();
            this.cmsTextBoxMenu.SuspendLayout();
            this.panelNewTool.SuspendLayout();
            this.paneEditTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(257, 436);
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
            this.listWindowsApps.Size = new System.Drawing.Size(314, 312);
            this.listWindowsApps.SmallImageList = this.imageList1;
            this.listWindowsApps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listWindowsApps.TabIndex = 10;
            this.listWindowsApps.UseCompatibleStateImageBehavior = false;
            this.listWindowsApps.View = System.Windows.Forms.View.SmallIcon;
            this.listWindowsApps.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listWindowsApps_MouseDoubleClick);
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
            this.t_edittool,
            this.t_remove,
            this.t_run});
            this.cmsTool.Name = "cmsTool";
            this.cmsTool.Size = new System.Drawing.Size(118, 70);
            // 
            // t_edittool
            // 
            this.t_edittool.Name = "t_edittool";
            this.t_edittool.Size = new System.Drawing.Size(117, 22);
            this.t_edittool.Text = "Edit";
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
            // splitPanel
            // 
            this.splitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitPanel.Location = new System.Drawing.Point(12, 12);
            this.splitPanel.Name = "splitPanel";
            this.splitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.Controls.Add(this.listWindowsApps);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.Controls.Add(this.listOmniTools);
            this.splitPanel.Size = new System.Drawing.Size(314, 416);
            this.splitPanel.SplitterDistance = 312;
            this.splitPanel.TabIndex = 20;
            // 
            // listOmniTools
            // 
            this.listOmniTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listOmniTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listOmniTools.HideSelection = false;
            this.listOmniTools.LabelEdit = true;
            this.listOmniTools.Location = new System.Drawing.Point(0, 0);
            this.listOmniTools.Name = "listOmniTools";
            this.listOmniTools.ShowItemToolTips = true;
            this.listOmniTools.Size = new System.Drawing.Size(314, 100);
            this.listOmniTools.SmallImageList = this.imageList1;
            this.listOmniTools.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listOmniTools.TabIndex = 0;
            this.listOmniTools.UseCompatibleStateImageBehavior = false;
            this.listOmniTools.View = System.Windows.Forms.View.Details;
            this.listOmniTools.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listOmniTools_AfterLabelEdit);
            this.listOmniTools.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listOmniTools_BeforeLabelEdit);
            this.listOmniTools.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listOmniTools_MouseDoubleClick);
            this.listOmniTools.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDown);
            // 
            // btnAddToStix
            // 
            this.btnAddToStix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddToStix.Location = new System.Drawing.Point(131, 436);
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
            this.groupAddTool.BackColor = System.Drawing.Color.Transparent;
            this.groupAddTool.Controls.Add(this.lblTip);
            this.groupAddTool.Controls.Add(this.lblChangeIcon);
            this.groupAddTool.Controls.Add(this.btnCloseAddTool);
            this.groupAddTool.Controls.Add(this.pIcon);
            this.groupAddTool.Controls.Add(this.lblToolIcon);
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
            this.groupAddTool.Location = new System.Drawing.Point(-2, 0);
            this.groupAddTool.Name = "groupAddTool";
            this.groupAddTool.Size = new System.Drawing.Size(323, 268);
            this.groupAddTool.TabIndex = 22;
            this.groupAddTool.TabStop = false;
            // 
            // lblTip
            // 
            this.lblTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTip.Location = new System.Drawing.Point(9, 199);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(306, 29);
            this.lblTip.TabIndex = 40;
            this.lblTip.Text = "The tool name and icon are generated automatically after you paste the link to a " +
    "file or webpage, or press the Enter key.";
            // 
            // lblChangeIcon
            // 
            this.lblChangeIcon.AutoSize = true;
            this.lblChangeIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeIcon.Location = new System.Drawing.Point(120, 123);
            this.lblChangeIcon.Name = "lblChangeIcon";
            this.lblChangeIcon.Size = new System.Drawing.Size(169, 13);
            this.lblChangeIcon.TabIndex = 39;
            this.lblChangeIcon.Text = "Click icon if you want to change it.";
            // 
            // btnCloseAddTool
            // 
            this.btnCloseAddTool.Location = new System.Drawing.Point(247, 238);
            this.btnCloseAddTool.Name = "btnCloseAddTool";
            this.btnCloseAddTool.Size = new System.Drawing.Size(68, 23);
            this.btnCloseAddTool.TabIndex = 38;
            this.btnCloseAddTool.Text = "Close";
            this.btnCloseAddTool.UseVisualStyleBackColor = true;
            this.btnCloseAddTool.Click += new System.EventHandler(this.btnCloseAddTool_Click);
            // 
            // pIcon
            // 
            this.pIcon.Image = ((System.Drawing.Image)(resources.GetObject("pIcon.Image")));
            this.pIcon.Location = new System.Drawing.Point(88, 119);
            this.pIcon.Name = "pIcon";
            this.pIcon.Size = new System.Drawing.Size(20, 20);
            this.pIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIcon.TabIndex = 35;
            this.pIcon.TabStop = false;
            this.pIcon.Click += new System.EventHandler(this.pIcon_Click);
            // 
            // lblToolIcon
            // 
            this.lblToolIcon.AutoSize = true;
            this.lblToolIcon.Location = new System.Drawing.Point(7, 123);
            this.lblToolIcon.Name = "lblToolIcon";
            this.lblToolIcon.Size = new System.Drawing.Size(81, 13);
            this.lblToolIcon.TabIndex = 34;
            this.lblToolIcon.Text = "Значок инстр.:";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "*";
            // 
            // lblTooltip
            // 
            this.lblTooltip.AutoSize = true;
            this.lblTooltip.Location = new System.Drawing.Point(7, 151);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(209, 13);
            this.lblTooltip.TabIndex = 31;
            this.lblTooltip.Text = "Tooltip when  the mouse hover over a tool:";
            // 
            // txtTooltip
            // 
            this.txtTooltip.Location = new System.Drawing.Point(9, 167);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(306, 20);
            this.txtTooltip.TabIndex = 30;
            // 
            // chAddToStix
            // 
            this.chAddToStix.AutoSize = true;
            this.chAddToStix.Location = new System.Drawing.Point(84, 242);
            this.chAddToStix.Name = "chAddToStix";
            this.chAddToStix.Size = new System.Drawing.Size(77, 17);
            this.chAddToStix.TabIndex = 29;
            this.chAddToStix.Text = "Add to Stix";
            this.chAddToStix.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(285, 33);
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
            this.lblWait.Location = new System.Drawing.Point(54, 64);
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
            this.lblSpecifyPath.Location = new System.Drawing.Point(18, 17);
            this.lblSpecifyPath.Name = "lblSpecifyPath";
            this.lblSpecifyPath.Size = new System.Drawing.Size(254, 13);
            this.lblSpecifyPath.TabIndex = 25;
            this.lblSpecifyPath.Text = "Укажите путь к файлу или вставьте веб-ссылку:";
            // 
            // btnAddTool
            // 
            this.btnAddTool.Enabled = false;
            this.btnAddTool.Location = new System.Drawing.Point(10, 237);
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
            this.txtPath.Location = new System.Drawing.Point(9, 36);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panelNewTool
            // 
            this.panelNewTool.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelNewTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNewTool.Controls.Add(this.groupAddTool);
            this.panelNewTool.Location = new System.Drawing.Point(8, 12);
            this.panelNewTool.Name = "panelNewTool";
            this.panelNewTool.Size = new System.Drawing.Size(323, 274);
            this.panelNewTool.TabIndex = 11;
            this.panelNewTool.Visible = false;
            // 
            // btnNewTool
            // 
            this.btnNewTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewTool.Location = new System.Drawing.Point(12, 436);
            this.btnNewTool.Name = "btnNewTool";
            this.btnNewTool.Size = new System.Drawing.Size(112, 23);
            this.btnNewTool.TabIndex = 31;
            this.btnNewTool.Tag = "";
            this.btnNewTool.Text = "New Tool";
            this.btnNewTool.UseVisualStyleBackColor = true;
            this.btnNewTool.Click += new System.EventHandler(this.btnNewTool_Click);
            // 
            // paneEditTool
            // 
            this.paneEditTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paneEditTool.Controls.Add(this.lblChangeIcon2);
            this.paneEditTool.Controls.Add(this.btnCancel);
            this.paneEditTool.Controls.Add(this.pIcon2);
            this.paneEditTool.Controls.Add(this.lblToolIcon2);
            this.paneEditTool.Controls.Add(this.lblTooltip2);
            this.paneEditTool.Controls.Add(this.txtTooltip2);
            this.paneEditTool.Controls.Add(this.txtTitle2);
            this.paneEditTool.Controls.Add(this.lblTitle2);
            this.paneEditTool.Controls.Add(this.btnOK);
            this.paneEditTool.Location = new System.Drawing.Point(7, 291);
            this.paneEditTool.Name = "paneEditTool";
            this.paneEditTool.Size = new System.Drawing.Size(324, 156);
            this.paneEditTool.TabIndex = 32;
            this.paneEditTool.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(246, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 23);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pIcon2
            // 
            this.pIcon2.Image = ((System.Drawing.Image)(resources.GetObject("pIcon2.Image")));
            this.pIcon2.Location = new System.Drawing.Point(89, 45);
            this.pIcon2.Name = "pIcon2";
            this.pIcon2.Size = new System.Drawing.Size(20, 20);
            this.pIcon2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIcon2.TabIndex = 42;
            this.pIcon2.TabStop = false;
            this.pIcon2.Click += new System.EventHandler(this.pIcon_Click);
            // 
            // lblToolIcon2
            // 
            this.lblToolIcon2.AutoSize = true;
            this.lblToolIcon2.Location = new System.Drawing.Point(8, 49);
            this.lblToolIcon2.Name = "lblToolIcon2";
            this.lblToolIcon2.Size = new System.Drawing.Size(81, 13);
            this.lblToolIcon2.TabIndex = 41;
            this.lblToolIcon2.Text = "Значок инстр.:";
            // 
            // lblTooltip2
            // 
            this.lblTooltip2.AutoSize = true;
            this.lblTooltip2.Location = new System.Drawing.Point(8, 77);
            this.lblTooltip2.Name = "lblTooltip2";
            this.lblTooltip2.Size = new System.Drawing.Size(209, 13);
            this.lblTooltip2.TabIndex = 40;
            this.lblTooltip2.Text = "Tooltip when  the mouse hover over a tool:";
            // 
            // txtTooltip2
            // 
            this.txtTooltip2.Location = new System.Drawing.Point(11, 93);
            this.txtTooltip2.Name = "txtTooltip2";
            this.txtTooltip2.Size = new System.Drawing.Size(304, 20);
            this.txtTooltip2.TabIndex = 39;
            // 
            // txtTitle2
            // 
            this.txtTitle2.Location = new System.Drawing.Point(80, 12);
            this.txtTitle2.Name = "txtTitle2";
            this.txtTitle2.Size = new System.Drawing.Size(235, 20);
            this.txtTitle2.TabIndex = 38;
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Location = new System.Drawing.Point(8, 15);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(67, 13);
            this.lblTitle2.TabIndex = 37;
            this.lblTitle2.Text = "Имя инстр.:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(11, 124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 23);
            this.btnOK.TabIndex = 36;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblChangeIcon2
            // 
            this.lblChangeIcon2.AutoSize = true;
            this.lblChangeIcon2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeIcon2.Location = new System.Drawing.Point(119, 48);
            this.lblChangeIcon2.Name = "lblChangeIcon2";
            this.lblChangeIcon2.Size = new System.Drawing.Size(169, 13);
            this.lblChangeIcon2.TabIndex = 44;
            this.lblChangeIcon2.Text = "Click icon if you want to change it.";
            // 
            // ManageToolsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(339, 469);
            this.Controls.Add(this.paneEditTool);
            this.Controls.Add(this.btnNewTool);
            this.Controls.Add(this.panelNewTool);
            this.Controls.Add(this.btnAddToStix);
            this.Controls.Add(this.splitPanel);
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
            this.splitPanel.Panel1.ResumeLayout(false);
            this.splitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).EndInit();
            this.splitPanel.ResumeLayout(false);
            this.groupAddTool.ResumeLayout(false);
            this.groupAddTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).EndInit();
            this.cmsTextBoxMenu.ResumeLayout(false);
            this.panelNewTool.ResumeLayout(false);
            this.paneEditTool.ResumeLayout(false);
            this.paneEditTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListView listWindowsApps;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.ContextMenuStrip cmsTool;
        private System.Windows.Forms.ToolStripMenuItem t_edittool;
        private System.Windows.Forms.ToolStripMenuItem t_remove;
        private System.Windows.Forms.ToolStripMenuItem t_run;
        private System.Windows.Forms.SplitContainer splitPanel;
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
        private System.Windows.Forms.Label lblTooltip;
        public System.Windows.Forms.TextBox txtTooltip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pIcon;
        private System.Windows.Forms.Label lblToolIcon;
        private System.Windows.Forms.Panel panelNewTool;
        private System.Windows.Forms.Button btnNewTool;
        private System.Windows.Forms.Button btnCloseAddTool;
        private System.Windows.Forms.Label lblChangeIcon;
        private System.Windows.Forms.Panel paneEditTool;
        private System.Windows.Forms.PictureBox pIcon2;
        private System.Windows.Forms.Label lblToolIcon2;
        private System.Windows.Forms.Label lblTooltip2;
        public System.Windows.Forms.TextBox txtTooltip2;
        public System.Windows.Forms.TextBox txtTitle2;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblChangeIcon2;
    }
}