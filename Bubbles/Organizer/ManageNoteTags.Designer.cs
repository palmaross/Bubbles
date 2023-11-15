namespace Organizer
{
    partial class ManageNoteTags
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageNoteTags));
            this.groupBoxAddNewTag = new System.Windows.Forms.GroupBox();
            this.txtNewTag = new System.Windows.Forms.TextBox();
            this.btnAddTag = new System.Windows.Forms.Button();
            this.btnDeleteTag = new System.Windows.Forms.PictureBox();
            this.groupBoxRenameTag = new System.Windows.Forms.GroupBox();
            this.txtRenameTag = new System.Windows.Forms.TextBox();
            this.btnRenameTag = new System.Windows.Forms.Button();
            this.cbTags = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxAddNewTag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteTag)).BeginInit();
            this.groupBoxRenameTag.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAddNewTag
            // 
            this.groupBoxAddNewTag.Controls.Add(this.txtNewTag);
            this.groupBoxAddNewTag.Controls.Add(this.btnAddTag);
            this.groupBoxAddNewTag.Location = new System.Drawing.Point(32, 42);
            this.groupBoxAddNewTag.Name = "groupBoxAddNewTag";
            this.groupBoxAddNewTag.Size = new System.Drawing.Size(245, 74);
            this.groupBoxAddNewTag.TabIndex = 107;
            this.groupBoxAddNewTag.TabStop = false;
            this.groupBoxAddNewTag.Text = "Добавить новый таг";
            // 
            // txtNewTag
            // 
            this.txtNewTag.Location = new System.Drawing.Point(9, 17);
            this.txtNewTag.Name = "txtNewTag";
            this.txtNewTag.Size = new System.Drawing.Size(227, 20);
            this.txtNewTag.TabIndex = 5;
            // 
            // btnAddTag
            // 
            this.btnAddTag.Location = new System.Drawing.Point(126, 43);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(110, 23);
            this.btnAddTag.TabIndex = 5;
            this.btnAddTag.Text = "Добавить";
            this.btnAddTag.UseVisualStyleBackColor = true;
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // btnDeleteTag
            // 
            this.btnDeleteTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteTag.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteTag.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteTag.Image")));
            this.btnDeleteTag.Location = new System.Drawing.Point(259, 13);
            this.btnDeleteTag.Name = "btnDeleteTag";
            this.btnDeleteTag.Size = new System.Drawing.Size(16, 16);
            this.btnDeleteTag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDeleteTag.TabIndex = 108;
            this.btnDeleteTag.TabStop = false;
            this.btnDeleteTag.Click += new System.EventHandler(this.btnDeleteTag_Click);
            // 
            // groupBoxRenameTag
            // 
            this.groupBoxRenameTag.Controls.Add(this.txtRenameTag);
            this.groupBoxRenameTag.Controls.Add(this.btnRenameTag);
            this.groupBoxRenameTag.Location = new System.Drawing.Point(32, 125);
            this.groupBoxRenameTag.Name = "groupBoxRenameTag";
            this.groupBoxRenameTag.Size = new System.Drawing.Size(245, 74);
            this.groupBoxRenameTag.TabIndex = 106;
            this.groupBoxRenameTag.TabStop = false;
            this.groupBoxRenameTag.Text = "Переименовать выбранный таг";
            // 
            // txtRenameTag
            // 
            this.txtRenameTag.Location = new System.Drawing.Point(9, 19);
            this.txtRenameTag.Name = "txtRenameTag";
            this.txtRenameTag.Size = new System.Drawing.Size(227, 20);
            this.txtRenameTag.TabIndex = 5;
            // 
            // btnRenameTag
            // 
            this.btnRenameTag.Location = new System.Drawing.Point(126, 45);
            this.btnRenameTag.Name = "btnRenameTag";
            this.btnRenameTag.Size = new System.Drawing.Size(110, 23);
            this.btnRenameTag.TabIndex = 2;
            this.btnRenameTag.Text = "Переименовать";
            this.btnRenameTag.UseVisualStyleBackColor = true;
            this.btnRenameTag.Click += new System.EventHandler(this.btnRenameTag_Click);
            // 
            // cbTags
            // 
            this.cbTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTags.FormattingEnabled = true;
            this.cbTags.Location = new System.Drawing.Point(33, 11);
            this.cbTags.Name = "cbTags";
            this.cbTags.Size = new System.Drawing.Size(220, 21);
            this.cbTags.Sorted = true;
            this.cbTags.TabIndex = 105;
            this.cbTags.SelectedIndexChanged += new System.EventHandler(this.cbTags_SelectedIndexChanged);
            // 
            // ManageNoteTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBoxAddNewTag);
            this.Controls.Add(this.btnDeleteTag);
            this.Controls.Add(this.groupBoxRenameTag);
            this.Controls.Add(this.cbTags);
            this.Name = "ManageNoteTags";
            this.Size = new System.Drawing.Size(308, 210);
            this.groupBoxAddNewTag.ResumeLayout(false);
            this.groupBoxAddNewTag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteTag)).EndInit();
            this.groupBoxRenameTag.ResumeLayout(false);
            this.groupBoxRenameTag.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAddNewTag;
        private System.Windows.Forms.TextBox txtNewTag;
        private System.Windows.Forms.Button btnAddTag;
        public System.Windows.Forms.PictureBox btnDeleteTag;
        private System.Windows.Forms.GroupBox groupBoxRenameTag;
        private System.Windows.Forms.TextBox txtRenameTag;
        private System.Windows.Forms.Button btnRenameTag;
        public System.Windows.Forms.ComboBox cbTags;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
