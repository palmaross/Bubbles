namespace Bubbles
{
    partial class AddTopicTemplateDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTopicTemplateDlg));
            this.lblTopicText = new System.Windows.Forms.Label();
            this.txtTopicText = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddTopics = new System.Windows.Forms.Button();
            this.panelNewTemplate = new System.Windows.Forms.Panel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancelCreate = new System.Windows.Forms.Button();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.rbtnTopics = new System.Windows.Forms.RadioButton();
            this.rbtnUseIncrement = new System.Windows.Forms.RadioButton();
            this.numTopics = new System.Windows.Forms.NumericUpDown();
            this.cbTemplates = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grAdd = new System.Windows.Forms.GroupBox();
            this.NextTopic = new System.Windows.Forms.RadioButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TopicBefore = new System.Windows.Forms.RadioButton();
            this.Subtopic = new System.Windows.Forms.RadioButton();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.rbtnCustom = new System.Windows.Forms.RadioButton();
            this.grTemplate = new System.Windows.Forms.GroupBox();
            this.lblTopics = new System.Windows.Forms.Label();
            this.txtCustom = new System.Windows.Forms.TextBox();
            this.Rename = new System.Windows.Forms.PictureBox();
            this.Delete = new System.Windows.Forms.PictureBox();
            this.New = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelNewTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopics)).BeginInit();
            this.grAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.grTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rename)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.New)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTopicText
            // 
            this.lblTopicText.AutoSize = true;
            this.lblTopicText.Location = new System.Drawing.Point(12, 50);
            this.lblTopicText.Name = "lblTopicText";
            this.lblTopicText.Size = new System.Drawing.Size(61, 13);
            this.lblTopicText.TabIndex = 0;
            this.lblTopicText.Text = "Topic Text:";
            // 
            // txtTopicText
            // 
            this.txtTopicText.Location = new System.Drawing.Point(79, 48);
            this.txtTopicText.Name = "txtTopicText";
            this.txtTopicText.Size = new System.Drawing.Size(237, 20);
            this.txtTopicText.TabIndex = 1;
            this.txtTopicText.TextChanged += new System.EventHandler(this.txtTopicText_TextChanged);
            this.txtTopicText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTopicText_KeyUp);
            this.txtTopicText.Leave += new System.EventHandler(this.txtTopicText_Leave);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(241, 368);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddTopics
            // 
            this.btnAddTopics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddTopics.Location = new System.Drawing.Point(11, 368);
            this.btnAddTopics.Name = "btnAddTopics";
            this.btnAddTopics.Size = new System.Drawing.Size(100, 23);
            this.btnAddTopics.TabIndex = 21;
            this.btnAddTopics.Text = "Добавить темы";
            this.btnAddTopics.UseVisualStyleBackColor = true;
            this.btnAddTopics.Click += new System.EventHandler(this.btnAddTopics_Click);
            // 
            // panelNewTemplate
            // 
            this.panelNewTemplate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelNewTemplate.Controls.Add(this.btnCreate);
            this.panelNewTemplate.Controls.Add(this.btnCancelCreate);
            this.panelNewTemplate.Controls.Add(this.txtTemplateName);
            this.panelNewTemplate.Controls.Add(this.lblTemplateName);
            this.panelNewTemplate.Location = new System.Drawing.Point(12, 49);
            this.panelNewTemplate.Name = "panelNewTemplate";
            this.panelNewTemplate.Size = new System.Drawing.Size(304, 96);
            this.panelNewTemplate.TabIndex = 31;
            this.panelNewTemplate.Visible = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(26, 63);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 23);
            this.btnCreate.TabIndex = 34;
            this.btnCreate.Text = "Переименовать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancelCreate
            // 
            this.btnCancelCreate.Location = new System.Drawing.Point(178, 63);
            this.btnCancelCreate.Name = "btnCancelCreate";
            this.btnCancelCreate.Size = new System.Drawing.Size(100, 23);
            this.btnCancelCreate.TabIndex = 33;
            this.btnCancelCreate.Text = "Cancel";
            this.btnCancelCreate.UseVisualStyleBackColor = true;
            this.btnCancelCreate.Click += new System.EventHandler(this.btnCancelCreate_Click);
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Location = new System.Drawing.Point(26, 28);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(252, 20);
            this.txtTemplateName.TabIndex = 31;
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.Location = new System.Drawing.Point(24, 11);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(79, 13);
            this.lblTemplateName.TabIndex = 0;
            this.lblTemplateName.Text = "Имя шаблона:";
            // 
            // rbtnTopics
            // 
            this.rbtnTopics.AutoSize = true;
            this.rbtnTopics.Location = new System.Drawing.Point(6, 24);
            this.rbtnTopics.Name = "rbtnTopics";
            this.rbtnTopics.Size = new System.Drawing.Size(14, 13);
            this.rbtnTopics.TabIndex = 17;
            this.rbtnTopics.UseVisualStyleBackColor = true;
            this.rbtnTopics.CheckedChanged += new System.EventHandler(this.TemplateType_CheckedChanged);
            // 
            // rbtnUseIncrement
            // 
            this.rbtnUseIncrement.AutoSize = true;
            this.rbtnUseIncrement.Checked = true;
            this.rbtnUseIncrement.Location = new System.Drawing.Point(218, 22);
            this.rbtnUseIncrement.Name = "rbtnUseIncrement";
            this.rbtnUseIncrement.Size = new System.Drawing.Size(72, 17);
            this.rbtnUseIncrement.TabIndex = 26;
            this.rbtnUseIncrement.TabStop = true;
            this.rbtnUseIncrement.Text = "Increment";
            this.rbtnUseIncrement.UseVisualStyleBackColor = true;
            this.rbtnUseIncrement.CheckedChanged += new System.EventHandler(this.TemplateType_CheckedChanged);
            // 
            // numTopics
            // 
            this.numTopics.Location = new System.Drawing.Point(24, 22);
            this.numTopics.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numTopics.Name = "numTopics";
            this.numTopics.Size = new System.Drawing.Size(38, 20);
            this.numTopics.TabIndex = 17;
            this.numTopics.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTopics.ValueChanged += new System.EventHandler(this.numTopics_ValueChanged);
            this.numTopics.Click += new System.EventHandler(this.lblTopics_Click);
            // 
            // cbTemplates
            // 
            this.cbTemplates.FormattingEnabled = true;
            this.cbTemplates.Location = new System.Drawing.Point(11, 11);
            this.cbTemplates.Name = "cbTemplates";
            this.cbTemplates.Size = new System.Drawing.Size(228, 21);
            this.cbTemplates.Sorted = true;
            this.cbTemplates.TabIndex = 28;
            this.cbTemplates.SelectedIndexChanged += new System.EventHandler(this.cbTemplates_SelectedIndexChanged);
            // 
            // grAdd
            // 
            this.grAdd.Controls.Add(this.NextTopic);
            this.grAdd.Controls.Add(this.TopicBefore);
            this.grAdd.Controls.Add(this.Subtopic);
            this.grAdd.Location = new System.Drawing.Point(11, 307);
            this.grAdd.Name = "grAdd";
            this.grAdd.Size = new System.Drawing.Size(304, 51);
            this.grAdd.TabIndex = 35;
            this.grAdd.TabStop = false;
            this.grAdd.Text = "Добавить";
            // 
            // NextTopic
            // 
            this.NextTopic.ImageKey = "cpAddTopic.png";
            this.NextTopic.ImageList = this.imageList1;
            this.NextTopic.Location = new System.Drawing.Point(93, 18);
            this.NextTopic.Name = "NextTopic";
            this.NextTopic.Size = new System.Drawing.Size(104, 24);
            this.NextTopic.TabIndex = 20;
            this.NextTopic.Text = "Темы после";
            this.NextTopic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NextTopic.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cpAddSubtopic.png");
            this.imageList1.Images.SetKeyName(1, "cpAddTopic.png");
            this.imageList1.Images.SetKeyName(2, "cpAddBefore.png");
            // 
            // TopicBefore
            // 
            this.TopicBefore.ImageKey = "cpAddBefore.png";
            this.TopicBefore.ImageList = this.imageList1;
            this.TopicBefore.Location = new System.Drawing.Point(200, 18);
            this.TopicBefore.Name = "TopicBefore";
            this.TopicBefore.Size = new System.Drawing.Size(103, 24);
            this.TopicBefore.TabIndex = 19;
            this.TopicBefore.Text = "Topic Before";
            this.TopicBefore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TopicBefore.UseVisualStyleBackColor = true;
            // 
            // Subtopic
            // 
            this.Subtopic.Checked = true;
            this.Subtopic.ImageKey = "cpAddSubtopic.png";
            this.Subtopic.ImageList = this.imageList1;
            this.Subtopic.Location = new System.Drawing.Point(6, 18);
            this.Subtopic.Name = "Subtopic";
            this.Subtopic.Size = new System.Drawing.Size(84, 24);
            this.Subtopic.TabIndex = 18;
            this.Subtopic.TabStop = true;
            this.Subtopic.Text = "Subtopic";
            this.Subtopic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Subtopic.UseVisualStyleBackColor = true;
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(11, 30);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.TabIndex = 36;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // rbtnCustom
            // 
            this.rbtnCustom.AutoSize = true;
            this.rbtnCustom.Location = new System.Drawing.Point(110, 22);
            this.rbtnCustom.Name = "rbtnCustom";
            this.rbtnCustom.Size = new System.Drawing.Size(60, 17);
            this.rbtnCustom.TabIndex = 37;
            this.rbtnCustom.Text = "Custom";
            this.rbtnCustom.UseVisualStyleBackColor = true;
            this.rbtnCustom.CheckedChanged += new System.EventHandler(this.TemplateType_CheckedChanged);
            // 
            // grTemplate
            // 
            this.grTemplate.Controls.Add(this.rbtnCustom);
            this.grTemplate.Controls.Add(this.lblTopics);
            this.grTemplate.Controls.Add(this.rbtnTopics);
            this.grTemplate.Controls.Add(this.numTopics);
            this.grTemplate.Controls.Add(this.rbtnUseIncrement);
            this.grTemplate.Location = new System.Drawing.Point(11, 79);
            this.grTemplate.Name = "grTemplate";
            this.grTemplate.Size = new System.Drawing.Size(304, 54);
            this.grTemplate.TabIndex = 38;
            this.grTemplate.TabStop = false;
            this.grTemplate.Text = "Шаблон:";
            // 
            // lblTopics
            // 
            this.lblTopics.AutoSize = true;
            this.lblTopics.Location = new System.Drawing.Point(65, 23);
            this.lblTopics.Name = "lblTopics";
            this.lblTopics.Size = new System.Drawing.Size(35, 13);
            this.lblTopics.TabIndex = 17;
            this.lblTopics.Text = "topics";
            this.lblTopics.Click += new System.EventHandler(this.lblTopics_Click);
            // 
            // txtCustom
            // 
            this.txtCustom.Location = new System.Drawing.Point(11, 139);
            this.txtCustom.Multiline = true;
            this.txtCustom.Name = "txtCustom";
            this.txtCustom.Size = new System.Drawing.Size(304, 156);
            this.txtCustom.TabIndex = 39;
            this.txtCustom.Visible = false;
            // 
            // Rename
            // 
            this.Rename.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Rename.Image = ((System.Drawing.Image)(resources.GetObject("Rename.Image")));
            this.Rename.Location = new System.Drawing.Point(270, 11);
            this.Rename.Name = "Rename";
            this.Rename.Size = new System.Drawing.Size(20, 20);
            this.Rename.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Rename.TabIndex = 56;
            this.Rename.TabStop = false;
            this.Rename.Click += new System.EventHandler(this.Rename_Click);
            // 
            // Delete
            // 
            this.Delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Location = new System.Drawing.Point(295, 11);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(20, 20);
            this.Delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Delete.TabIndex = 55;
            this.Delete.TabStop = false;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // New
            // 
            this.New.Cursor = System.Windows.Forms.Cursors.Hand;
            this.New.Image = ((System.Drawing.Image)(resources.GetObject("New.Image")));
            this.New.Location = new System.Drawing.Point(245, 11);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(20, 20);
            this.New.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.New.TabIndex = 54;
            this.New.TabStop = false;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(138, 368);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 57;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddTopicTemplateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(328, 401);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Rename);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.New);
            this.Controls.Add(this.txtCustom);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.cbTemplates);
            this.Controls.Add(this.btnAddTopics);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtTopicText);
            this.Controls.Add(this.lblTopicText);
            this.Controls.Add(this.grAdd);
            this.Controls.Add(this.grTemplate);
            this.Controls.Add(this.panelNewTemplate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTopicTemplateDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Topic Template";
            this.panelNewTemplate.ResumeLayout(false);
            this.panelNewTemplate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopics)).EndInit();
            this.grAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.grTemplate.ResumeLayout(false);
            this.grTemplate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rename)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.New)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTopicText;
        private System.Windows.Forms.TextBox txtTopicText;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddTopics;
        private System.Windows.Forms.RadioButton rbtnTopics;
        private System.Windows.Forms.RadioButton rbtnUseIncrement;
        private System.Windows.Forms.NumericUpDown numTopics;
        private System.Windows.Forms.ComboBox cbTemplates;
        private System.Windows.Forms.Panel panelNewTemplate;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Button btnCancelCreate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox grAdd;
        private System.Windows.Forms.RadioButton Subtopic;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.RadioButton NextTopic;
        private System.Windows.Forms.RadioButton TopicBefore;
        private System.Windows.Forms.PictureBox p1;
        private System.Windows.Forms.RadioButton rbtnCustom;
        private System.Windows.Forms.GroupBox grTemplate;
        private System.Windows.Forms.Label lblTopics;
        private System.Windows.Forms.TextBox txtCustom;
        private System.Windows.Forms.PictureBox Rename;
        private System.Windows.Forms.PictureBox Delete;
        private System.Windows.Forms.PictureBox New;
        private System.Windows.Forms.Button btnSave;
    }
}