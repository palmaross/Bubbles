namespace DragDropTest
{
    partial class DDResult
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grbInput = new System.Windows.Forms.GroupBox();
            this.panImage = new System.Windows.Forms.Panel();
            this.panPreview = new System.Windows.Forms.Panel();
            this.grbOutput = new System.Windows.Forms.GroupBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbResults = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCopyHtml = new System.Windows.Forms.Button();
            this.chkClearHtml = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkDownloadImages = new System.Windows.Forms.CheckBox();
            this.grbInput.SuspendLayout();
            this.grbOutput.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbInput
            // 
            this.grbInput.BackColor = System.Drawing.Color.Transparent;
            this.grbInput.Controls.Add(this.panImage);
            this.grbInput.Controls.Add(this.panPreview);
            this.grbInput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInput.ForeColor = System.Drawing.Color.White;
            this.grbInput.Location = new System.Drawing.Point(12, 12);
            this.grbInput.Name = "grbInput";
            this.grbInput.Size = new System.Drawing.Size(270, 280);
            this.grbInput.TabIndex = 0;
            this.grbInput.TabStop = false;
            this.grbInput.Text = "Image Drag Drop";
            // 
            // panImage
            // 
            this.panImage.AllowDrop = true;
            this.panImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panImage.Location = new System.Drawing.Point(10, 20);
            this.panImage.Name = "panImage";
            this.panImage.Size = new System.Drawing.Size(250, 250);
            this.panImage.TabIndex = 0;
            this.panImage.DragDrop += new System.Windows.Forms.DragEventHandler(this.panImage_DragDrop);
            this.panImage.DragEnter += new System.Windows.Forms.DragEventHandler(this.panImage_DragEnter);
            // 
            // panPreview
            // 
            this.panPreview.AllowDrop = true;
            this.panPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panPreview.Location = new System.Drawing.Point(220, 20);
            this.panPreview.Name = "panPreview";
            this.panPreview.Size = new System.Drawing.Size(40, 250);
            this.panPreview.TabIndex = 2;
            // 
            // grbOutput
            // 
            this.grbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbOutput.BackColor = System.Drawing.Color.Transparent;
            this.grbOutput.Controls.Add(this.webBrowser1);
            this.grbOutput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbOutput.ForeColor = System.Drawing.Color.White;
            this.grbOutput.Location = new System.Drawing.Point(582, 12);
            this.grbOutput.Name = "grbOutput";
            this.grbOutput.Size = new System.Drawing.Size(420, 437);
            this.grbOutput.TabIndex = 1;
            this.grbOutput.TabStop = false;
            this.grbOutput.Text = "HTML Source (Read Only)";
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.CausesValidation = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(10, 20);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(401, 407);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 210);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drag Dop Results List";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(10, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(248, 180);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rtbResults);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(297, 298);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 151);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drag Dop Results Details";
            // 
            // rtbResults
            // 
            this.rtbResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.rtbResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbResults.ForeColor = System.Drawing.Color.White;
            this.rtbResults.Location = new System.Drawing.Point(6, 22);
            this.rtbResults.Name = "rtbResults";
            this.rtbResults.Size = new System.Drawing.Size(258, 123);
            this.rtbResults.TabIndex = 0;
            this.rtbResults.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(297, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 280);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Text Source Drag Drop";
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Location = new System.Drawing.Point(10, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 250);
            this.textBox1.TabIndex = 0;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox1_DragEnter);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnCopyHtml);
            this.groupBox4.Controls.Add(this.chkClearHtml);
            this.groupBox4.Location = new System.Drawing.Point(582, 455);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(420, 53);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // btnCopyHtml
            // 
            this.btnCopyHtml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyHtml.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCopyHtml.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCopyHtml.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnCopyHtml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyHtml.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyHtml.ForeColor = System.Drawing.Color.Black;
            this.btnCopyHtml.Location = new System.Drawing.Point(277, 17);
            this.btnCopyHtml.Name = "btnCopyHtml";
            this.btnCopyHtml.Size = new System.Drawing.Size(134, 25);
            this.btnCopyHtml.TabIndex = 1;
            this.btnCopyHtml.Text = "Copy Html Content";
            this.btnCopyHtml.UseVisualStyleBackColor = false;
            this.btnCopyHtml.Click += new System.EventHandler(this.btnCopyHtml_Click);
            // 
            // chkClearHtml
            // 
            this.chkClearHtml.AutoSize = true;
            this.chkClearHtml.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClearHtml.Location = new System.Drawing.Point(18, 21);
            this.chkClearHtml.Name = "chkClearHtml";
            this.chkClearHtml.Size = new System.Drawing.Size(158, 19);
            this.chkClearHtml.TabIndex = 0;
            this.chkClearHtml.Text = "Clear Html Page on Drop";
            this.chkClearHtml.UseVisualStyleBackColor = true;
            this.chkClearHtml.CheckedChanged += new System.EventHandler(this.chkClearHtml_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.chkDownloadImages);
            this.groupBox5.Location = new System.Drawing.Point(297, 455);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(270, 53);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            // 
            // chkDownloadImages
            // 
            this.chkDownloadImages.AutoSize = true;
            this.chkDownloadImages.Checked = true;
            this.chkDownloadImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDownloadImages.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDownloadImages.Location = new System.Drawing.Point(18, 21);
            this.chkDownloadImages.Name = "chkDownloadImages";
            this.chkDownloadImages.Size = new System.Drawing.Size(159, 19);
            this.chkDownloadImages.TabIndex = 0;
            this.chkDownloadImages.Text = "Download Linked Images";
            this.chkDownloadImages.UseVisualStyleBackColor = true;
            this.chkDownloadImages.CheckedChanged += new System.EventHandler(this.chkDownloadImages_CheckedChanged);
            // 
            // DDResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1014, 522);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbOutput);
            this.Controls.Add(this.grbInput);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.Name = "DDResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drag Drop Results";
            this.Load += new System.EventHandler(this.DDResult_Load);
            this.grbInput.ResumeLayout(false);
            this.grbOutput.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox grbInput;
        private System.Windows.Forms.Panel panImage;
        private System.Windows.Forms.GroupBox grbOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox rtbResults;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkClearHtml;
        private System.Windows.Forms.Button btnCopyHtml;
        private System.Windows.Forms.Panel panPreview;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkDownloadImages;
        public System.Windows.Forms.TextBox textBox1;
    }
}

