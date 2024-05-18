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
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            this.SuspendLayout();
            // 
            // rbtnContains
            // 
            this.rbtnContains.AutoSize = true;
            this.rbtnContains.Checked = true;
            this.rbtnContains.Location = new System.Drawing.Point(4, 9);
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
            this.rbtnStartsWith.Location = new System.Drawing.Point(74, 9);
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
            this.listTopics.Location = new System.Drawing.Point(2, 34);
            this.listTopics.Name = "listTopics";
            this.listTopics.Size = new System.Drawing.Size(278, 84);
            this.listTopics.TabIndex = 2;
            this.listTopics.SelectedIndexChanged += new System.EventHandler(this.listTopics_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(166, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(98, 21);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // pClose
            // 
            this.pClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pClose.Image = ((System.Drawing.Image)(resources.GetObject("pClose.Image")));
            this.pClose.Location = new System.Drawing.Point(263, 1);
            this.pClose.Name = "pClose";
            this.pClose.Size = new System.Drawing.Size(16, 16);
            this.pClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClose.TabIndex = 4;
            this.pClose.TabStop = false;
            this.pClose.Click += new System.EventHandler(this.pClose_Click);
            // 
            // SearchTextDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 122);
            this.Controls.Add(this.pClose);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnContains;
        private System.Windows.Forms.RadioButton rbtnStartsWith;
        private System.Windows.Forms.ListBox listTopics;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox pClose;
    }
}