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
            this.t_edittool = new System.Windows.Forms.ToolStripMenuItem();
            this.t_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.t_run = new System.Windows.Forms.ToolStripMenuItem();
            this.t_copytoomni = new System.Windows.Forms.ToolStripMenuItem();
            this.splitPanel = new System.Windows.Forms.SplitContainer();
            this.lblOTools = new System.Windows.Forms.Label();
            this.listOmniTools = new System.Windows.Forms.ListView();
            this.btnAddToStix = new System.Windows.Forms.Button();
            this.btnNewTool = new System.Windows.Forms.Button();
            this.cbAddToStix = new System.Windows.Forms.ComboBox();
            this.lblWTools = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.cmsTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).BeginInit();
            this.splitPanel.Panel1.SuspendLayout();
            this.splitPanel.Panel2.SuspendLayout();
            this.splitPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(257, 471);
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
            this.listWindowsApps.Size = new System.Drawing.Size(314, 329);
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
            this.t_run,
            this.t_copytoomni});
            this.cmsTool.Name = "cmsTool";
            this.cmsTool.Size = new System.Drawing.Size(177, 92);
            // 
            // t_edittool
            // 
            this.t_edittool.Name = "t_edittool";
            this.t_edittool.Size = new System.Drawing.Size(176, 22);
            this.t_edittool.Text = "Edit";
            // 
            // t_remove
            // 
            this.t_remove.Name = "t_remove";
            this.t_remove.Size = new System.Drawing.Size(176, 22);
            this.t_remove.Text = "Remove";
            // 
            // t_run
            // 
            this.t_run.Name = "t_run";
            this.t_run.Size = new System.Drawing.Size(176, 22);
            this.t_run.Text = "Run";
            // 
            // t_copytoomni
            // 
            this.t_copytoomni.Name = "t_copytoomni";
            this.t_copytoomni.Size = new System.Drawing.Size(176, 22);
            this.t_copytoomni.Text = "Copy to OmniTools";
            // 
            // splitPanel
            // 
            this.splitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitPanel.Location = new System.Drawing.Point(12, 16);
            this.splitPanel.Name = "splitPanel";
            this.splitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.Controls.Add(this.listWindowsApps);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.Controls.Add(this.lblOTools);
            this.splitPanel.Panel2.Controls.Add(this.listOmniTools);
            this.splitPanel.Size = new System.Drawing.Size(314, 440);
            this.splitPanel.SplitterDistance = 329;
            this.splitPanel.TabIndex = 20;
            // 
            // lblOTools
            // 
            this.lblOTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOTools.Location = new System.Drawing.Point(52, 0);
            this.lblOTools.Name = "lblOTools";
            this.lblOTools.Size = new System.Drawing.Size(210, 13);
            this.lblOTools.TabIndex = 35;
            this.lblOTools.Text = "Omni Tools";
            this.lblOTools.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listOmniTools
            // 
            this.listOmniTools.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listOmniTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listOmniTools.HideSelection = false;
            this.listOmniTools.LabelEdit = true;
            this.listOmniTools.Location = new System.Drawing.Point(0, 16);
            this.listOmniTools.Name = "listOmniTools";
            this.listOmniTools.ShowItemToolTips = true;
            this.listOmniTools.Size = new System.Drawing.Size(314, 101);
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
            this.btnAddToStix.Location = new System.Drawing.Point(131, 471);
            this.btnAddToStix.Name = "btnAddToStix";
            this.btnAddToStix.Size = new System.Drawing.Size(112, 23);
            this.btnAddToStix.TabIndex = 21;
            this.btnAddToStix.Tag = "";
            this.btnAddToStix.Text = "Добавить на стик";
            this.btnAddToStix.UseVisualStyleBackColor = true;
            this.btnAddToStix.Click += new System.EventHandler(this.btnAddToStix_Click);
            // 
            // btnNewTool
            // 
            this.btnNewTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewTool.Location = new System.Drawing.Point(12, 471);
            this.btnNewTool.Name = "btnNewTool";
            this.btnNewTool.Size = new System.Drawing.Size(112, 23);
            this.btnNewTool.TabIndex = 31;
            this.btnNewTool.Tag = "";
            this.btnNewTool.Text = "New Tool";
            this.btnNewTool.UseVisualStyleBackColor = true;
            this.btnNewTool.Click += new System.EventHandler(this.btnNewTool_Click);
            // 
            // cbAddToStix
            // 
            this.cbAddToStix.FormattingEnabled = true;
            this.cbAddToStix.Location = new System.Drawing.Point(132, 471);
            this.cbAddToStix.Name = "cbAddToStix";
            this.cbAddToStix.Size = new System.Drawing.Size(110, 21);
            this.cbAddToStix.TabIndex = 33;
            this.cbAddToStix.SelectedIndexChanged += new System.EventHandler(this.cbAddToStix_SelectedIndexChanged);
            // 
            // lblWTools
            // 
            this.lblWTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWTools.Location = new System.Drawing.Point(62, 0);
            this.lblWTools.Name = "lblWTools";
            this.lblWTools.Size = new System.Drawing.Size(210, 13);
            this.lblWTools.TabIndex = 34;
            this.lblWTools.Text = "Windows Tools";
            this.lblWTools.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ManageToolsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(339, 504);
            this.Controls.Add(this.lblWTools);
            this.Controls.Add(this.btnNewTool);
            this.Controls.Add(this.btnAddToStix);
            this.Controls.Add(this.splitPanel);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbAddToStix);
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
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListView listWindowsApps;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.ContextMenuStrip cmsTool;
        private System.Windows.Forms.ToolStripMenuItem t_edittool;
        private System.Windows.Forms.ToolStripMenuItem t_remove;
        private System.Windows.Forms.ToolStripMenuItem t_run;
        private System.Windows.Forms.SplitContainer splitPanel;
        private System.Windows.Forms.ListView listOmniTools;
        private System.Windows.Forms.Button btnAddToStix;
        private System.Windows.Forms.Button btnNewTool;
        public System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cbAddToStix;
        public System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem t_copytoomni;
        private System.Windows.Forms.Label lblWTools;
        private System.Windows.Forms.Label lblOTools;
    }
}