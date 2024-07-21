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
            this.panelEditButtons = new System.Windows.Forms.Panel();
            this.pStrikeout = new System.Windows.Forms.PictureBox();
            this.pUnderline = new System.Windows.Forms.PictureBox();
            this.pItalic = new System.Windows.Forms.PictureBox();
            this.pBold = new System.Windows.Forms.PictureBox();
            this.fontDown = new System.Windows.Forms.PictureBox();
            this.fontUp = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MI_gototopic = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider2 = new System.Windows.Forms.HelpProvider();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnGetNotes = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaveAllNo = new System.Windows.Forms.PictureBox();
            this.btnSaveOneNo = new System.Windows.Forms.PictureBox();
            this.btnSaveAll = new System.Windows.Forms.PictureBox();
            this.btnSaveOne = new System.Windows.Forms.PictureBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelMinimized = new System.Windows.Forms.Panel();
            this.listTopics = new System.Windows.Forms.TreeView();
            this.btnNewTab = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PreviewPage = new System.Windows.Forms.TabPage();
            this.rtbPreview = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelFindNotes = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelFind = new System.Windows.Forms.Panel();
            this.linkSearchOptions = new System.Windows.Forms.LinkLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbSearchedText = new System.Windows.Forms.ComboBox();
            this.pBrowse = new System.Windows.Forms.PictureBox();
            this.cbFindIn = new System.Windows.Forms.ComboBox();
            this.lblLookIn = new System.Windows.Forms.Label();
            this.panelEditButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pStrikeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pUnderline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pItalic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontUp)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAllNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOneNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOne)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PreviewPage.SuspendLayout();
            this.panelFindNotes.SuspendLayout();
            this.panelFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBrowse)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEditButtons
            // 
            this.panelEditButtons.Controls.Add(this.pStrikeout);
            this.panelEditButtons.Controls.Add(this.pUnderline);
            this.panelEditButtons.Controls.Add(this.pItalic);
            this.panelEditButtons.Controls.Add(this.pBold);
            this.panelEditButtons.Controls.Add(this.fontDown);
            this.panelEditButtons.Controls.Add(this.fontUp);
            this.panelEditButtons.Location = new System.Drawing.Point(64, 3);
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
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnGetNotes);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 294);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(620, 37);
            this.panelBottom.TabIndex = 12;
            // 
            // btnGetNotes
            // 
            this.btnGetNotes.Location = new System.Drawing.Point(9, 6);
            this.btnGetNotes.Name = "btnGetNotes";
            this.btnGetNotes.Size = new System.Drawing.Size(110, 23);
            this.btnGetNotes.TabIndex = 25;
            this.btnGetNotes.Text = "Get Topic Notes";
            this.btnGetNotes.UseVisualStyleBackColor = true;
            this.btnGetNotes.Click += new System.EventHandler(this.btnGetNotes_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(535, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveAllNo
            // 
            this.btnSaveAllNo.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAllNo.Image")));
            this.btnSaveAllNo.Location = new System.Drawing.Point(27, 4);
            this.btnSaveAllNo.Margin = new System.Windows.Forms.Padding(6);
            this.btnSaveAllNo.Name = "btnSaveAllNo";
            this.btnSaveAllNo.Size = new System.Drawing.Size(16, 16);
            this.btnSaveAllNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSaveAllNo.TabIndex = 30;
            this.btnSaveAllNo.TabStop = false;
            // 
            // btnSaveOneNo
            // 
            this.btnSaveOneNo.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveOneNo.Image")));
            this.btnSaveOneNo.Location = new System.Drawing.Point(5, 4);
            this.btnSaveOneNo.Margin = new System.Windows.Forms.Padding(6);
            this.btnSaveOneNo.Name = "btnSaveOneNo";
            this.btnSaveOneNo.Size = new System.Drawing.Size(16, 16);
            this.btnSaveOneNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSaveOneNo.TabIndex = 29;
            this.btnSaveOneNo.TabStop = false;
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAll.Image")));
            this.btnSaveAll.Location = new System.Drawing.Point(318, 5);
            this.btnSaveAll.Margin = new System.Windows.Forms.Padding(6);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(16, 16);
            this.btnSaveAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSaveAll.TabIndex = 28;
            this.btnSaveAll.TabStop = false;
            this.btnSaveAll.Visible = false;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnSaveOne
            // 
            this.btnSaveOne.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveOne.Image")));
            this.btnSaveOne.Location = new System.Drawing.Point(291, 5);
            this.btnSaveOne.Margin = new System.Windows.Forms.Padding(6);
            this.btnSaveOne.Name = "btnSaveOne";
            this.btnSaveOne.Size = new System.Drawing.Size(16, 16);
            this.btnSaveOne.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSaveOne.TabIndex = 27;
            this.btnSaveOne.TabStop = false;
            this.btnSaveOne.Visible = false;
            this.btnSaveOne.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnSaveAll);
            this.panelTop.Controls.Add(this.btnSaveOne);
            this.panelTop.Controls.Add(this.btnSaveAllNo);
            this.panelTop.Controls.Add(this.panelEditButtons);
            this.panelTop.Controls.Add(this.btnSaveOneNo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(453, 25);
            this.panelTop.TabIndex = 13;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 23);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelMinimized);
            this.splitContainer1.Panel1.Controls.Add(this.listTopics);
            this.splitContainer1.Panel1.Controls.Add(this.btnNewTab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.panelTop);
            this.splitContainer1.Size = new System.Drawing.Size(620, 271);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 14;
            // 
            // panelMinimized
            // 
            this.panelMinimized.Location = new System.Drawing.Point(88, 55);
            this.panelMinimized.Name = "panelMinimized";
            this.panelMinimized.Size = new System.Drawing.Size(260, 37);
            this.panelMinimized.TabIndex = 26;
            this.panelMinimized.Visible = false;
            // 
            // listTopics
            // 
            this.listTopics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTopics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listTopics.Location = new System.Drawing.Point(0, 23);
            this.listTopics.Name = "listTopics";
            this.listTopics.Size = new System.Drawing.Size(163, 248);
            this.listTopics.TabIndex = 11;
            this.listTopics.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.listTopics_AfterSelect);
            this.listTopics.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.listTopics_NodeMouseClick);
            this.listTopics.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.listTopics_NodeMouseDoubleClick);
            // 
            // btnNewTab
            // 
            this.btnNewTab.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNewTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewTab.Location = new System.Drawing.Point(0, 0);
            this.btnNewTab.Name = "btnNewTab";
            this.btnNewTab.Size = new System.Drawing.Size(163, 23);
            this.btnNewTab.TabIndex = 27;
            this.btnNewTab.Text = "Open in a New Tab";
            this.btnNewTab.UseVisualStyleBackColor = true;
            this.btnNewTab.Click += new System.EventHandler(this.btnNewTab_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PreviewPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 22);
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(453, 246);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 14;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlNotes_DrawItem);
            this.tabControl1.MouseLeave += new System.EventHandler(this.tabControl1_MouseLeave);
            this.tabControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseMove);
            this.tabControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseUp);
            // 
            // PreviewPage
            // 
            this.PreviewPage.Controls.Add(this.rtbPreview);
            this.PreviewPage.Location = new System.Drawing.Point(4, 26);
            this.PreviewPage.Name = "PreviewPage";
            this.PreviewPage.Padding = new System.Windows.Forms.Padding(3);
            this.PreviewPage.Size = new System.Drawing.Size(445, 216);
            this.PreviewPage.TabIndex = 0;
            this.PreviewPage.Text = "tabPage1";
            this.PreviewPage.UseVisualStyleBackColor = true;
            // 
            // rtbPreview
            // 
            this.rtbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbPreview.Location = new System.Drawing.Point(3, 3);
            this.rtbPreview.Name = "rtbPreview";
            this.rtbPreview.Size = new System.Drawing.Size(439, 210);
            this.rtbPreview.TabIndex = 0;
            this.rtbPreview.Text = "";
            this.rtbPreview.TextChanged += new System.EventHandler(this.rtb_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(445, 216);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelFindNotes
            // 
            this.panelFindNotes.Controls.Add(this.radioButton3);
            this.panelFindNotes.Controls.Add(this.radioButton2);
            this.panelFindNotes.Controls.Add(this.radioButton1);
            this.panelFindNotes.Controls.Add(this.textBox1);
            this.panelFindNotes.Location = new System.Drawing.Point(200, 193);
            this.panelFindNotes.Name = "panelFindNotes";
            this.panelFindNotes.Size = new System.Drawing.Size(287, 100);
            this.panelFindNotes.TabIndex = 27;
            this.panelFindNotes.Visible = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(12, 78);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(94, 17);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "All Open Maps";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 53);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(94, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "All Open Maps";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Current Map";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(88, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(196, 21);
            this.textBox1.TabIndex = 6;
            // 
            // panelFind
            // 
            this.panelFind.Controls.Add(this.linkSearchOptions);
            this.panelFind.Controls.Add(this.btnSearch);
            this.panelFind.Controls.Add(this.cbSearchedText);
            this.panelFind.Controls.Add(this.pBrowse);
            this.panelFind.Controls.Add(this.cbFindIn);
            this.panelFind.Controls.Add(this.lblLookIn);
            this.panelFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFind.Location = new System.Drawing.Point(0, 0);
            this.panelFind.Name = "panelFind";
            this.panelFind.Size = new System.Drawing.Size(620, 23);
            this.panelFind.TabIndex = 28;
            // 
            // linkSearchOptions
            // 
            this.linkSearchOptions.AutoSize = true;
            this.linkSearchOptions.Location = new System.Drawing.Point(430, 5);
            this.linkSearchOptions.Name = "linkSearchOptions";
            this.linkSearchOptions.Size = new System.Drawing.Size(117, 13);
            this.linkSearchOptions.TabIndex = 29;
            this.linkSearchOptions.TabStop = true;
            this.linkSearchOptions.Text = "Opciones de búsqueda";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(554, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 23);
            this.btnSearch.TabIndex = 28;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbSearchedText
            // 
            this.cbSearchedText.FormattingEnabled = true;
            this.cbSearchedText.Location = new System.Drawing.Point(263, 1);
            this.cbSearchedText.Name = "cbSearchedText";
            this.cbSearchedText.Size = new System.Drawing.Size(160, 21);
            this.cbSearchedText.TabIndex = 12;
            // 
            // pBrowse
            // 
            this.pBrowse.Image = ((System.Drawing.Image)(resources.GetObject("pBrowse.Image")));
            this.pBrowse.Location = new System.Drawing.Point(239, 3);
            this.pBrowse.Name = "pBrowse";
            this.pBrowse.Size = new System.Drawing.Size(16, 16);
            this.pBrowse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBrowse.TabIndex = 9;
            this.pBrowse.TabStop = false;
            // 
            // cbFindIn
            // 
            this.cbFindIn.FormattingEnabled = true;
            this.cbFindIn.Location = new System.Drawing.Point(64, 1);
            this.cbFindIn.Name = "cbFindIn";
            this.cbFindIn.Size = new System.Drawing.Size(171, 21);
            this.cbFindIn.TabIndex = 7;
            // 
            // lblLookIn
            // 
            this.lblLookIn.AutoSize = true;
            this.lblLookIn.Location = new System.Drawing.Point(3, 4);
            this.lblLookIn.Name = "lblLookIn";
            this.lblLookIn.Size = new System.Drawing.Size(45, 13);
            this.lblLookIn.TabIndex = 6;
            this.lblLookIn.Text = "Look in:";
            // 
            // TopicNotesDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 331);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelFind);
            this.Controls.Add(this.panelFindNotes);
            this.Controls.Add(this.panelBottom);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TopicNotesDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TopicNotesDlg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TopicNotesDlg_FormClosing);
            this.Load += new System.EventHandler(this.TopicNotesDlg_Load);
            this.panelEditButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pStrikeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pUnderline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pItalic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontUp)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAllNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOneNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOne)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.PreviewPage.ResumeLayout(false);
            this.panelFindNotes.ResumeLayout(false);
            this.panelFindNotes.PerformLayout();
            this.panelFind.ResumeLayout(false);
            this.panelFind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBrowse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelEditButtons;
        private System.Windows.Forms.PictureBox fontDown;
        private System.Windows.Forms.PictureBox fontUp;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MI_gototopic;
        private System.Windows.Forms.ToolStripMenuItem MI_remove;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pUnderline;
        private System.Windows.Forms.PictureBox pItalic;
        private System.Windows.Forms.PictureBox pBold;
        private System.Windows.Forms.PictureBox pStrikeout;
        private System.Windows.Forms.HelpProvider helpProvider2;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.Panel panelMinimized;
        private System.Windows.Forms.Button btnGetNotes;
        private System.Windows.Forms.Panel panelFindNotes;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panelFind;
        private System.Windows.Forms.ComboBox cbFindIn;
        private System.Windows.Forms.Label lblLookIn;
        private System.Windows.Forms.PictureBox pBrowse;
        private System.Windows.Forms.ComboBox cbSearchedText;
        private System.Windows.Forms.LinkLabel linkSearchOptions;
        private System.Windows.Forms.Button btnSearch;
        public System.Windows.Forms.TreeView listTopics;
        private System.Windows.Forms.PictureBox btnSaveAllNo;
        private System.Windows.Forms.PictureBox btnSaveOneNo;
        private System.Windows.Forms.PictureBox btnSaveAll;
        private System.Windows.Forms.PictureBox btnSaveOne;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabPage PreviewPage;
        private System.Windows.Forms.Button btnNewTab;
        private System.Windows.Forms.RichTextBox rtbPreview;
        public System.Windows.Forms.TabControl tabControl1;
    }
}