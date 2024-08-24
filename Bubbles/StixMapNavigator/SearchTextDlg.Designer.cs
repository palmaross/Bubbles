namespace Bubbles
{
    partial class SearchTextDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchTextDlg));
            this.rbtnContains = new System.Windows.Forms.RadioButton();
            this.rbtnStartsWith = new System.Windows.Forms.RadioButton();
            this.listTopics = new System.Windows.Forms.ListBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.panelHead = new System.Windows.Forms.Panel();
            this.pHelp = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            this.panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // rbtnContains
            // 
            this.rbtnContains.AutoSize = true;
            this.rbtnContains.Checked = true;
            this.rbtnContains.Location = new System.Drawing.Point(4, 24);
            this.rbtnContains.Name = "rbtnContains";
            this.rbtnContains.Size = new System.Drawing.Size(66, 17);
            this.rbtnContains.TabIndex = 0;
            this.rbtnContains.TabStop = true;
            this.rbtnContains.Text = "Contains";
            this.rbtnContains.UseVisualStyleBackColor = true;
            // 
            // rbtnStartsWith
            // 
            this.rbtnStartsWith.AutoSize = true;
            this.rbtnStartsWith.Location = new System.Drawing.Point(74, 24);
            this.rbtnStartsWith.Name = "rbtnStartsWith";
            this.rbtnStartsWith.Size = new System.Drawing.Size(94, 17);
            this.rbtnStartsWith.TabIndex = 1;
            this.rbtnStartsWith.Text = "Начинается с";
            this.rbtnStartsWith.UseVisualStyleBackColor = true;
            // 
            // listTopics
            // 
            this.listTopics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listTopics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listTopics.FormattingEnabled = true;
            this.listTopics.IntegralHeight = false;
            this.listTopics.ItemHeight = 15;
            this.listTopics.Location = new System.Drawing.Point(2, 44);
            this.listTopics.Name = "listTopics";
            this.listTopics.Size = new System.Drawing.Size(288, 96);
            this.listTopics.TabIndex = 2;
            this.listTopics.SelectedIndexChanged += new System.EventHandler(this.listTopics_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(167, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(123, 21);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // pClose
            // 
            this.pClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pClose.Image = ((System.Drawing.Image)(resources.GetObject("pClose.Image")));
            this.pClose.Location = new System.Drawing.Point(271, 1);
            this.pClose.Name = "pClose";
            this.pClose.Size = new System.Drawing.Size(16, 16);
            this.pClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClose.TabIndex = 4;
            this.pClose.TabStop = false;
            this.pClose.Click += new System.EventHandler(this.pClose_Click);
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.Moccasin;
            this.panelHead.Controls.Add(this.pictureBox1);
            this.panelHead.Controls.Add(this.pHelp);
            this.panelHead.Controls.Add(this.lblTitle);
            this.panelHead.Controls.Add(this.pClose);
            this.panelHead.Location = new System.Drawing.Point(1, 1);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(288, 18);
            this.panelHead.TabIndex = 5;
            // 
            // pHelp
            // 
            this.pHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pHelp.Image = ((System.Drawing.Image)(resources.GetObject("pHelp.Image")));
            this.pHelp.Location = new System.Drawing.Point(250, 1);
            this.pHelp.Name = "pHelp";
            this.pHelp.Size = new System.Drawing.Size(16, 16);
            this.pHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pHelp.TabIndex = 6;
            this.pHelp.TabStop = false;
            this.pHelp.Click += new System.EventHandler(this.pHelp_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTitle.Location = new System.Drawing.Point(20, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(89, 13);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Search Topics";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // SearchTextDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 140);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.listTopics);
            this.Controls.Add(this.rbtnStartsWith);
            this.Controls.Add(this.rbtnContains);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchTextDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SearchTextDlg";
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.panelHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnContains;
        private System.Windows.Forms.RadioButton rbtnStartsWith;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox pClose;
        private System.Windows.Forms.Panel panelHead;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pHelp;
        private System.Windows.Forms.HelpProvider helpProvider1;
        public System.Windows.Forms.ListBox listTopics;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}