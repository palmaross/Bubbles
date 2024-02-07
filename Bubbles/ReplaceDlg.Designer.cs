namespace Bubbles
{
    partial class ReplaceDlg
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
            this.rbtnSelectedTopics = new System.Windows.Forms.RadioButton();
            this.rbtnWholeMap = new System.Windows.Forms.RadioButton();
            this.txtText1 = new System.Windows.Forms.TextBox();
            this.labelWith = new System.Windows.Forms.Label();
            this.txtText2 = new System.Windows.Forms.TextBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.checkTopicText = new System.Windows.Forms.CheckBox();
            this.checkTopicNotes = new System.Windows.Forms.CheckBox();
            this.btnSkip = new System.Windows.Forms.Button();
            this.chbReplaceAll = new System.Windows.Forms.CheckBox();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.linkGoToTopic = new System.Windows.Forms.LinkLabel();
            this.linkReplaceAll = new System.Windows.Forms.LinkLabel();
            this.linkMore = new System.Windows.Forms.LinkLabel();
            this.minSize = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rbtnSelectedTopics
            // 
            this.rbtnSelectedTopics.AutoSize = true;
            this.rbtnSelectedTopics.Location = new System.Drawing.Point(138, 12);
            this.rbtnSelectedTopics.Name = "rbtnSelectedTopics";
            this.rbtnSelectedTopics.Size = new System.Drawing.Size(102, 17);
            this.rbtnSelectedTopics.TabIndex = 0;
            this.rbtnSelectedTopics.Text = "Selected Topics";
            this.rbtnSelectedTopics.UseVisualStyleBackColor = true;
            // 
            // rbtnWholeMap
            // 
            this.rbtnWholeMap.AutoSize = true;
            this.rbtnWholeMap.Checked = true;
            this.rbtnWholeMap.Location = new System.Drawing.Point(15, 12);
            this.rbtnWholeMap.Name = "rbtnWholeMap";
            this.rbtnWholeMap.Size = new System.Drawing.Size(80, 17);
            this.rbtnWholeMap.TabIndex = 1;
            this.rbtnWholeMap.TabStop = true;
            this.rbtnWholeMap.Text = "Whole Map";
            this.rbtnWholeMap.UseVisualStyleBackColor = true;
            // 
            // txtText1
            // 
            this.txtText1.Location = new System.Drawing.Point(12, 40);
            this.txtText1.Name = "txtText1";
            this.txtText1.Size = new System.Drawing.Size(235, 20);
            this.txtText1.TabIndex = 2;
            // 
            // labelWith
            // 
            this.labelWith.AutoSize = true;
            this.labelWith.Location = new System.Drawing.Point(12, 63);
            this.labelWith.Name = "labelWith";
            this.labelWith.Size = new System.Drawing.Size(26, 13);
            this.labelWith.TabIndex = 3;
            this.labelWith.Text = "with";
            // 
            // txtText2
            // 
            this.txtText2.Location = new System.Drawing.Point(12, 81);
            this.txtText2.Name = "txtText2";
            this.txtText2.Size = new System.Drawing.Size(235, 20);
            this.txtText2.TabIndex = 4;
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReplace.Location = new System.Drawing.Point(11, 288);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 23);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(172, 288);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // checkTopicText
            // 
            this.checkTopicText.AutoSize = true;
            this.checkTopicText.Checked = true;
            this.checkTopicText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTopicText.Location = new System.Drawing.Point(12, 126);
            this.checkTopicText.Name = "checkTopicText";
            this.checkTopicText.Size = new System.Drawing.Size(77, 17);
            this.checkTopicText.TabIndex = 7;
            this.checkTopicText.Text = "Topic Text";
            this.checkTopicText.UseVisualStyleBackColor = true;
            // 
            // checkTopicNotes
            // 
            this.checkTopicNotes.AutoSize = true;
            this.checkTopicNotes.Location = new System.Drawing.Point(123, 126);
            this.checkTopicNotes.Name = "checkTopicNotes";
            this.checkTopicNotes.Size = new System.Drawing.Size(84, 17);
            this.checkTopicNotes.TabIndex = 8;
            this.checkTopicNotes.Text = "Topic Notes";
            this.checkTopicNotes.UseVisualStyleBackColor = true;
            // 
            // btnSkip
            // 
            this.btnSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSkip.Location = new System.Drawing.Point(91, 288);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(75, 23);
            this.btnSkip.TabIndex = 11;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            // 
            // chbReplaceAll
            // 
            this.chbReplaceAll.AutoSize = true;
            this.chbReplaceAll.Checked = true;
            this.chbReplaceAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbReplaceAll.Location = new System.Drawing.Point(12, 149);
            this.chbReplaceAll.Name = "chbReplaceAll";
            this.chbReplaceAll.Size = new System.Drawing.Size(80, 17);
            this.chbReplaceAll.TabIndex = 12;
            this.chbReplaceAll.Text = "Replace All";
            this.chbReplaceAll.UseVisualStyleBackColor = true;
            // 
            // rtb
            // 
            this.rtb.Location = new System.Drawing.Point(11, 173);
            this.rtb.Name = "rtb";
            this.rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb.Size = new System.Drawing.Size(235, 86);
            this.rtb.TabIndex = 13;
            this.rtb.Text = "";
            // 
            // linkGoToTopic
            // 
            this.linkGoToTopic.Location = new System.Drawing.Point(146, 261);
            this.linkGoToTopic.Name = "linkGoToTopic";
            this.linkGoToTopic.Size = new System.Drawing.Size(100, 13);
            this.linkGoToTopic.TabIndex = 14;
            this.linkGoToTopic.TabStop = true;
            this.linkGoToTopic.Text = "Go to Topic";
            this.linkGoToTopic.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // linkReplaceAll
            // 
            this.linkReplaceAll.AutoSize = true;
            this.linkReplaceAll.Location = new System.Drawing.Point(11, 261);
            this.linkReplaceAll.Name = "linkReplaceAll";
            this.linkReplaceAll.Size = new System.Drawing.Size(96, 13);
            this.linkReplaceAll.TabIndex = 15;
            this.linkReplaceAll.TabStop = true;
            this.linkReplaceAll.Text = "Replace All Entries";
            // 
            // linkMore
            // 
            this.linkMore.Location = new System.Drawing.Point(79, 104);
            this.linkMore.Name = "linkMore";
            this.linkMore.Size = new System.Drawing.Size(88, 13);
            this.linkMore.TabIndex = 16;
            this.linkMore.TabStop = true;
            this.linkMore.Tag = "Max";
            this.linkMore.Text = "More >>>>";
            this.linkMore.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMore_LinkClicked);
            // 
            // minSize
            // 
            this.minSize.Location = new System.Drawing.Point(77, 63);
            this.minSize.Name = "minSize";
            this.minSize.Size = new System.Drawing.Size(158, 13);
            this.minSize.TabIndex = 17;
            this.minSize.Text = "minSize";
            this.minSize.Visible = false;
            // 
            // ReplaceDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(259, 323);
            this.Controls.Add(this.minSize);
            this.Controls.Add(this.linkMore);
            this.Controls.Add(this.linkReplaceAll);
            this.Controls.Add(this.linkGoToTopic);
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.chbReplaceAll);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.checkTopicNotes);
            this.Controls.Add(this.checkTopicText);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.txtText2);
            this.Controls.Add(this.labelWith);
            this.Controls.Add(this.txtText1);
            this.Controls.Add(this.rbtnWholeMap);
            this.Controls.Add(this.rbtnSelectedTopics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReplaceDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtnSelectedTopics;
        private System.Windows.Forms.RadioButton rbtnWholeMap;
        private System.Windows.Forms.TextBox txtText1;
        private System.Windows.Forms.Label labelWith;
        private System.Windows.Forms.TextBox txtText2;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox checkTopicText;
        private System.Windows.Forms.CheckBox checkTopicNotes;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.CheckBox chbReplaceAll;
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.LinkLabel linkGoToTopic;
        private System.Windows.Forms.LinkLabel linkReplaceAll;
        private System.Windows.Forms.LinkLabel linkMore;
        private System.Windows.Forms.Label minSize;
    }
}