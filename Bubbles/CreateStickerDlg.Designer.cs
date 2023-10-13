namespace Bubbles
{
    partial class CreateStickerDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateStickerDlg));
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbFonts = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblStickName = new System.Windows.Forms.Label();
            this.pBold = new System.Windows.Forms.PictureBox();
            this.lblBody = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.fillcolor = new System.Windows.Forms.PictureBox();
            this.fontcolor = new System.Windows.Forms.PictureBox();
            this.pDecreaseFont = new System.Windows.Forms.PictureBox();
            this.pIncreaseFont = new System.Windows.Forms.PictureBox();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.Editor = new System.Windows.Forms.RichTextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fillcolor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontcolor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDecreaseFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIncreaseFont)).BeginInit();
            this.panelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.SystemColors.Menu;
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.txtTitle);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.cmbFonts);
            this.panelTop.Controls.Add(this.btnApply);
            this.panelTop.Controls.Add(this.btnSave);
            this.panelTop.Controls.Add(this.lblStickName);
            this.panelTop.Controls.Add(this.pBold);
            this.panelTop.Controls.Add(this.lblBody);
            this.panelTop.Controls.Add(this.lblText);
            this.panelTop.Controls.Add(this.fillcolor);
            this.panelTop.Controls.Add(this.fontcolor);
            this.panelTop.Controls.Add(this.pDecreaseFont);
            this.panelTop.Controls.Add(this.pIncreaseFont);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(298, 76);
            this.panelTop.TabIndex = 0;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(50, 26);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(241, 20);
            this.txtTitle.TabIndex = 97;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(168, 49);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 98;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // cmbFonts
            // 
            this.cmbFonts.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFonts.FormattingEnabled = true;
            this.cmbFonts.Location = new System.Drawing.Point(186, 3);
            this.cmbFonts.Name = "cmbFonts";
            this.cmbFonts.Size = new System.Drawing.Size(105, 21);
            this.cmbFonts.TabIndex = 2;
            this.cmbFonts.Text = "Comic Sans MS";
            this.cmbFonts.SelectedIndexChanged += new System.EventHandler(this.cmbFonts_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Location = new System.Drawing.Point(87, 49);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 49);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblStickName
            // 
            this.lblStickName.AutoSize = true;
            this.lblStickName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStickName.Location = new System.Drawing.Point(3, 29);
            this.lblStickName.Name = "lblStickName";
            this.lblStickName.Size = new System.Drawing.Size(38, 13);
            this.lblStickName.TabIndex = 96;
            this.lblStickName.Text = "Name:";
            // 
            // pBold
            // 
            this.pBold.Image = ((System.Drawing.Image)(resources.GetObject("pBold.Image")));
            this.pBold.Location = new System.Drawing.Point(161, 4);
            this.pBold.Name = "pBold";
            this.pBold.Size = new System.Drawing.Size(16, 16);
            this.pBold.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBold.TabIndex = 95;
            this.pBold.TabStop = false;
            this.pBold.Tag = "";
            this.pBold.Click += new System.EventHandler(this.pBold_Click);
            // 
            // lblBody
            // 
            this.lblBody.AutoSize = true;
            this.lblBody.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBody.Location = new System.Drawing.Point(58, 6);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(34, 13);
            this.lblBody.TabIndex = 94;
            this.lblBody.Text = "Body:";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblText.Location = new System.Drawing.Point(3, 6);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(31, 13);
            this.lblText.TabIndex = 93;
            this.lblText.Text = "Text:";
            // 
            // fillcolor
            // 
            this.fillcolor.Location = new System.Drawing.Point(96, 4);
            this.fillcolor.Name = "fillcolor";
            this.fillcolor.Size = new System.Drawing.Size(16, 16);
            this.fillcolor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fillcolor.TabIndex = 92;
            this.fillcolor.TabStop = false;
            this.fillcolor.Click += new System.EventHandler(this.fillcolor_Click);
            // 
            // fontcolor
            // 
            this.fontcolor.Location = new System.Drawing.Point(37, 4);
            this.fontcolor.Name = "fontcolor";
            this.fontcolor.Size = new System.Drawing.Size(16, 16);
            this.fontcolor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fontcolor.TabIndex = 90;
            this.fontcolor.TabStop = false;
            this.fontcolor.Click += new System.EventHandler(this.fontcolor_Click);
            // 
            // pDecreaseFont
            // 
            this.pDecreaseFont.Image = ((System.Drawing.Image)(resources.GetObject("pDecreaseFont.Image")));
            this.pDecreaseFont.Location = new System.Drawing.Point(141, 4);
            this.pDecreaseFont.Name = "pDecreaseFont";
            this.pDecreaseFont.Size = new System.Drawing.Size(16, 16);
            this.pDecreaseFont.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pDecreaseFont.TabIndex = 3;
            this.pDecreaseFont.TabStop = false;
            this.pDecreaseFont.Click += new System.EventHandler(this.pDecreaseFont_Click);
            // 
            // pIncreaseFont
            // 
            this.pIncreaseFont.Image = ((System.Drawing.Image)(resources.GetObject("pIncreaseFont.Image")));
            this.pIncreaseFont.Location = new System.Drawing.Point(121, 4);
            this.pIncreaseFont.Name = "pIncreaseFont";
            this.pIncreaseFont.Size = new System.Drawing.Size(16, 16);
            this.pIncreaseFont.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIncreaseFont.TabIndex = 1;
            this.pIncreaseFont.TabStop = false;
            this.pIncreaseFont.Click += new System.EventHandler(this.pIncreaseFont_Click);
            // 
            // panelEditor
            // 
            this.panelEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEditor.Controls.Add(this.Editor);
            this.panelEditor.Location = new System.Drawing.Point(1, 77);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(296, 122);
            this.panelEditor.TabIndex = 1;
            // 
            // Editor
            // 
            this.Editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Editor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Editor.Location = new System.Drawing.Point(1, 0);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(294, 122);
            this.Editor.TabIndex = 0;
            this.Editor.Text = "";
            // 
            // CreateStickerDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 200);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateStickerDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StickerTemplate";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fillcolor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontcolor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDecreaseFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIncreaseFont)).EndInit();
            this.panelEditor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.RichTextBox Editor;
        private System.Windows.Forms.PictureBox pIncreaseFont;
        private System.Windows.Forms.PictureBox pDecreaseFont;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox fillcolor;
        private System.Windows.Forms.PictureBox fontcolor;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.ComboBox cmbFonts;
        private System.Windows.Forms.PictureBox pBold;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblStickName;
        private System.Windows.Forms.Button btnClose;
    }
}