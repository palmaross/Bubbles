namespace Bubbles
{
    partial class SnippetsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnippetsDlg));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtAddItem = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbtnPasteInside = new System.Windows.Forms.RadioButton();
            this.rbtnAddSubtopic = new System.Windows.Forms.RadioButton();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(367, 121);
            this.listBox1.TabIndex = 0;
            // 
            // txtAddItem
            // 
            this.txtAddItem.AllowDrop = true;
            this.txtAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddItem.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtAddItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddItem.Location = new System.Drawing.Point(24, 90);
            this.txtAddItem.Multiline = true;
            this.txtAddItem.Name = "txtAddItem";
            this.txtAddItem.Size = new System.Drawing.Size(343, 56);
            this.txtAddItem.TabIndex = 3;
            this.txtAddItem.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(236, 124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(47, 20);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(284, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 20);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbtnPasteInside
            // 
            this.rbtnPasteInside.AutoSize = true;
            this.rbtnPasteInside.Checked = true;
            this.rbtnPasteInside.Location = new System.Drawing.Point(12, 8);
            this.rbtnPasteInside.Name = "rbtnPasteInside";
            this.rbtnPasteInside.Size = new System.Drawing.Size(82, 17);
            this.rbtnPasteInside.TabIndex = 6;
            this.rbtnPasteInside.TabStop = true;
            this.rbtnPasteInside.Text = "Paste inside";
            this.rbtnPasteInside.UseVisualStyleBackColor = true;
            // 
            // rbtnAddSubtopic
            // 
            this.rbtnAddSubtopic.AutoSize = true;
            this.rbtnAddSubtopic.Location = new System.Drawing.Point(127, 8);
            this.rbtnAddSubtopic.Name = "rbtnAddSubtopic";
            this.rbtnAddSubtopic.Size = new System.Drawing.Size(117, 17);
            this.rbtnAddSubtopic.TabIndex = 7;
            this.rbtnAddSubtopic.Text = "Вставить подтему";
            this.rbtnAddSubtopic.UseVisualStyleBackColor = true;
            // 
            // pClose
            // 
            this.pClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pClose.Image = ((System.Drawing.Image)(resources.GetObject("pClose.Image")));
            this.pClose.Location = new System.Drawing.Point(359, 6);
            this.pClose.Name = "pClose";
            this.pClose.Size = new System.Drawing.Size(20, 20);
            this.pClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClose.TabIndex = 37;
            this.pClose.TabStop = false;
            this.pClose.Click += new System.EventHandler(this.pClose_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // SnippetsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(391, 163);
            this.Controls.Add(this.pClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtAddItem);
            this.Controls.Add(this.rbtnAddSubtopic);
            this.Controls.Add(this.rbtnPasteInside);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SnippetsDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SnippetsDlg";
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtAddItem;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbtnPasteInside;
        private System.Windows.Forms.RadioButton rbtnAddSubtopic;
        private System.Windows.Forms.PictureBox pClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}