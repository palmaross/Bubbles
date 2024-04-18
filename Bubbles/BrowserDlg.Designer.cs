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
            this.btnGo = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pRemove = new System.Windows.Forms.PictureBox();
            this.btnGetText = new System.Windows.Forms.Button();
            this.cmsTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pGoBack = new System.Windows.Forms.PictureBox();
            this.pGoForward = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pRemove)).BeginInit();
            this.cmsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pGoBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGoForward)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAddressBar
            // 
            this.txtAddressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddressBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtAddressBar.Location = new System.Drawing.Point(48, 8);
            this.txtAddressBar.Name = "txtAddressBar";
            this.txtAddressBar.Size = new System.Drawing.Size(525, 21);
            this.txtAddressBar.TabIndex = 6;
            this.txtAddressBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAddressBar_KeyUp);
            this.txtAddressBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtAddressBar_MouseDoubleClick);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(577, 7);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(35, 21);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(616, 375);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.pRemove);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(608, 347);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New";
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
            // btnGetText
            // 
            this.btnGetText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetText.Location = new System.Drawing.Point(12, 418);
            this.btnGetText.Name = "btnGetText";
            this.btnGetText.Size = new System.Drawing.Size(113, 23);
            this.btnGetText.TabIndex = 9;
            this.btnGetText.Text = "Add as Subtopic";
            this.btnGetText.UseVisualStyleBackColor = true;
            this.btnGetText.Click += new System.EventHandler(this.btnGetText_Click);
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
            this.btnClose.Location = new System.Drawing.Point(527, 418);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(134, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Add to Notes";
            this.button1.UseVisualStyleBackColor = true;
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
            // BrowserDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 450);
            this.Controls.Add(this.pGoForward);
            this.Controls.Add(this.pGoBack);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGetText);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtAddressBar);
            this.Name = "BrowserDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OmniStix Browser";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pRemove)).EndInit();
            this.cmsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pGoBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGoForward)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnGetText;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TextBox txtAddressBar;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ContextMenuStrip cmsTab;
        private System.Windows.Forms.ToolStripMenuItem tabRemove;
        private System.Windows.Forms.PictureBox pRemove;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pGoBack;
        private System.Windows.Forms.PictureBox pGoForward;
    }
}