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
            this.lblStart = new System.Windows.Forms.Label();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.numEnd = new System.Windows.Forms.NumericUpDown();
            this.rbtnBegin = new System.Windows.Forms.RadioButton();
            this.rbtnEnd = new System.Windows.Forms.RadioButton();
            this.lblStep = new System.Windows.Forms.Label();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddTopics = new System.Windows.Forms.Button();
            this.grPosition = new System.Windows.Forms.GroupBox();
            this.grIncrement = new System.Windows.Forms.GroupBox();
            this.numSteps = new System.Windows.Forms.NumericUpDown();
            this.rbtnSteps = new System.Windows.Forms.RadioButton();
            this.rbtnFinish = new System.Windows.Forms.RadioButton();
            this.linkSaveTemplate = new System.Windows.Forms.LinkLabel();
            this.linkPreview = new System.Windows.Forms.LinkLabel();
            this.linkNewTemplate = new System.Windows.Forms.LinkLabel();
            this.panelNewTemplate = new System.Windows.Forms.Panel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancelCreate = new System.Windows.Forms.Button();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.rbtnTopics = new System.Windows.Forms.RadioButton();
            this.rbtnUseIncrement = new System.Windows.Forms.RadioButton();
            this.numTopics = new System.Windows.Forms.NumericUpDown();
            this.lblTemplates = new System.Windows.Forms.Label();
            this.cbTemplates = new System.Windows.Forms.ComboBox();
            this.linkManageTemplates = new System.Windows.Forms.LinkLabel();
            this.panelManageTemplates = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDeleteTemplate = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.lblRename = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            this.grPosition.SuspendLayout();
            this.grIncrement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSteps)).BeginInit();
            this.panelNewTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopics)).BeginInit();
            this.panelManageTemplates.SuspendLayout();
            this.grAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.grTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTopicText
            // 
            this.lblTopicText.AutoSize = true;
            this.lblTopicText.Location = new System.Drawing.Point(12, 60);
            this.lblTopicText.Name = "lblTopicText";
            this.lblTopicText.Size = new System.Drawing.Size(61, 13);
            this.lblTopicText.TabIndex = 0;
            this.lblTopicText.Text = "Topic Text:";
            // 
            // txtTopicText
            // 
            this.txtTopicText.Location = new System.Drawing.Point(79, 58);
            this.txtTopicText.Name = "txtTopicText";
            this.txtTopicText.Size = new System.Drawing.Size(237, 20);
            this.txtTopicText.TabIndex = 1;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(55, 28);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(32, 13);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "Start:";
            // 
            // numStart
            // 
            this.numStart.Location = new System.Drawing.Point(96, 26);
            this.numStart.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(38, 20);
            this.numStart.TabIndex = 3;
            this.numStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numEnd
            // 
            this.numEnd.Location = new System.Drawing.Point(97, 57);
            this.numEnd.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numEnd.Name = "numEnd";
            this.numEnd.Size = new System.Drawing.Size(38, 20);
            this.numEnd.TabIndex = 5;
            this.numEnd.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // rbtnBegin
            // 
            this.rbtnBegin.AutoSize = true;
            this.rbtnBegin.Location = new System.Drawing.Point(10, 19);
            this.rbtnBegin.Name = "rbtnBegin";
            this.rbtnBegin.Size = new System.Drawing.Size(132, 17);
            this.rbtnBegin.TabIndex = 9;
            this.rbtnBegin.Text = "Перед текстом темы";
            this.rbtnBegin.UseVisualStyleBackColor = true;
            // 
            // rbtnEnd
            // 
            this.rbtnEnd.AutoSize = true;
            this.rbtnEnd.Checked = true;
            this.rbtnEnd.Location = new System.Drawing.Point(148, 19);
            this.rbtnEnd.Name = "rbtnEnd";
            this.rbtnEnd.Size = new System.Drawing.Size(124, 17);
            this.rbtnEnd.TabIndex = 10;
            this.rbtnEnd.TabStop = true;
            this.rbtnEnd.Text = "После текста темы";
            this.rbtnEnd.UseVisualStyleBackColor = true;
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(183, 28);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(32, 13);
            this.lblStep.TabIndex = 13;
            this.lblStep.Text = "Step:";
            // 
            // numStep
            // 
            this.numStep.Location = new System.Drawing.Point(227, 25);
            this.numStep.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numStep.Name = "numStep";
            this.numStep.Size = new System.Drawing.Size(38, 20);
            this.numStep.TabIndex = 14;
            this.numStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(241, 394);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = " Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddTopics
            // 
            this.btnAddTopics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddTopics.Location = new System.Drawing.Point(11, 394);
            this.btnAddTopics.Name = "btnAddTopics";
            this.btnAddTopics.Size = new System.Drawing.Size(100, 23);
            this.btnAddTopics.TabIndex = 21;
            this.btnAddTopics.Text = "Добавить темы";
            this.btnAddTopics.UseVisualStyleBackColor = true;
            this.btnAddTopics.Click += new System.EventHandler(this.btnAddTopics_Click);
            // 
            // grPosition
            // 
            this.grPosition.Controls.Add(this.rbtnBegin);
            this.grPosition.Controls.Add(this.rbtnEnd);
            this.grPosition.Location = new System.Drawing.Point(15, 89);
            this.grPosition.Name = "grPosition";
            this.grPosition.Size = new System.Drawing.Size(276, 46);
            this.grPosition.TabIndex = 22;
            this.grPosition.TabStop = false;
            this.grPosition.Text = "Number Position:";
            // 
            // grIncrement
            // 
            this.grIncrement.Controls.Add(this.numSteps);
            this.grIncrement.Controls.Add(this.rbtnSteps);
            this.grIncrement.Controls.Add(this.grPosition);
            this.grIncrement.Controls.Add(this.rbtnFinish);
            this.grIncrement.Controls.Add(this.lblStep);
            this.grIncrement.Controls.Add(this.numStart);
            this.grIncrement.Controls.Add(this.lblStart);
            this.grIncrement.Controls.Add(this.numEnd);
            this.grIncrement.Controls.Add(this.numStep);
            this.grIncrement.Location = new System.Drawing.Point(12, 148);
            this.grIncrement.Name = "grIncrement";
            this.grIncrement.Size = new System.Drawing.Size(304, 147);
            this.grIncrement.TabIndex = 25;
            this.grIncrement.TabStop = false;
            this.grIncrement.Text = "Increment";
            // 
            // numSteps
            // 
            this.numSteps.Location = new System.Drawing.Point(227, 57);
            this.numSteps.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSteps.Name = "numSteps";
            this.numSteps.Size = new System.Drawing.Size(38, 20);
            this.numSteps.TabIndex = 16;
            this.numSteps.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // rbtnSteps
            // 
            this.rbtnSteps.AutoSize = true;
            this.rbtnSteps.Location = new System.Drawing.Point(166, 58);
            this.rbtnSteps.Name = "rbtnSteps";
            this.rbtnSteps.Size = new System.Drawing.Size(60, 17);
            this.rbtnSteps.TabIndex = 15;
            this.rbtnSteps.Text = "Topics:";
            this.rbtnSteps.UseVisualStyleBackColor = true;
            // 
            // rbtnFinish
            // 
            this.rbtnFinish.AutoSize = true;
            this.rbtnFinish.Checked = true;
            this.rbtnFinish.Location = new System.Drawing.Point(39, 58);
            this.rbtnFinish.Name = "rbtnFinish";
            this.rbtnFinish.Size = new System.Drawing.Size(47, 17);
            this.rbtnFinish.TabIndex = 11;
            this.rbtnFinish.TabStop = true;
            this.rbtnFinish.Text = "End:";
            this.rbtnFinish.UseVisualStyleBackColor = true;
            // 
            // linkSaveTemplate
            // 
            this.linkSaveTemplate.AutoSize = true;
            this.linkSaveTemplate.Location = new System.Drawing.Point(14, 298);
            this.linkSaveTemplate.Name = "linkSaveTemplate";
            this.linkSaveTemplate.Size = new System.Drawing.Size(32, 13);
            this.linkSaveTemplate.TabIndex = 33;
            this.linkSaveTemplate.TabStop = true;
            this.linkSaveTemplate.Text = "Save";
            this.linkSaveTemplate.Click += new System.EventHandler(this.linkSaveTemplate_Click);
            // 
            // linkPreview
            // 
            this.linkPreview.Location = new System.Drawing.Point(211, 298);
            this.linkPreview.Name = "linkPreview";
            this.linkPreview.Size = new System.Drawing.Size(102, 13);
            this.linkPreview.TabIndex = 32;
            this.linkPreview.TabStop = true;
            this.linkPreview.Text = "Предпросмотр";
            this.linkPreview.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkPreview.Click += new System.EventHandler(this.linkPreview_Click);
            // 
            // linkNewTemplate
            // 
            this.linkNewTemplate.AutoSize = true;
            this.linkNewTemplate.Location = new System.Drawing.Point(88, 298);
            this.linkNewTemplate.Name = "linkNewTemplate";
            this.linkNewTemplate.Size = new System.Drawing.Size(110, 13);
            this.linkNewTemplate.TabIndex = 29;
            this.linkNewTemplate.TabStop = true;
            this.linkNewTemplate.Text = "Create New Template";
            this.linkNewTemplate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNewTemplate_LinkClicked);
            // 
            // panelNewTemplate
            // 
            this.panelNewTemplate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelNewTemplate.Controls.Add(this.btnCreate);
            this.panelNewTemplate.Controls.Add(this.btnCancelCreate);
            this.panelNewTemplate.Controls.Add(this.txtTemplateName);
            this.panelNewTemplate.Controls.Add(this.lblTemplateName);
            this.panelNewTemplate.Location = new System.Drawing.Point(12, 229);
            this.panelNewTemplate.Name = "panelNewTemplate";
            this.panelNewTemplate.Size = new System.Drawing.Size(304, 96);
            this.panelNewTemplate.TabIndex = 31;
            this.panelNewTemplate.Visible = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(26, 63);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(72, 23);
            this.btnCreate.TabIndex = 34;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancelCreate
            // 
            this.btnCancelCreate.Location = new System.Drawing.Point(206, 63);
            this.btnCancelCreate.Name = "btnCancelCreate";
            this.btnCancelCreate.Size = new System.Drawing.Size(72, 23);
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
            this.rbtnTopics.CheckedChanged += new System.EventHandler(this.Template_CheckedChanged);
            // 
            // rbtnUseIncrement
            // 
            this.rbtnUseIncrement.AutoSize = true;
            this.rbtnUseIncrement.Checked = true;
            this.rbtnUseIncrement.Location = new System.Drawing.Point(201, 22);
            this.rbtnUseIncrement.Name = "rbtnUseIncrement";
            this.rbtnUseIncrement.Size = new System.Drawing.Size(97, 17);
            this.rbtnUseIncrement.TabIndex = 26;
            this.rbtnUseIncrement.TabStop = true;
            this.rbtnUseIncrement.Text = "With Increment";
            this.rbtnUseIncrement.UseVisualStyleBackColor = true;
            this.rbtnUseIncrement.CheckedChanged += new System.EventHandler(this.Template_CheckedChanged);
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
            this.numTopics.Click += new System.EventHandler(this.lblTopics_Click);
            // 
            // lblTemplates
            // 
            this.lblTemplates.AutoSize = true;
            this.lblTemplates.Location = new System.Drawing.Point(14, 14);
            this.lblTemplates.Name = "lblTemplates";
            this.lblTemplates.Size = new System.Drawing.Size(59, 13);
            this.lblTemplates.TabIndex = 27;
            this.lblTemplates.Text = "Templates:";
            // 
            // cbTemplates
            // 
            this.cbTemplates.FormattingEnabled = true;
            this.cbTemplates.Location = new System.Drawing.Point(77, 11);
            this.cbTemplates.Name = "cbTemplates";
            this.cbTemplates.Size = new System.Drawing.Size(239, 21);
            this.cbTemplates.Sorted = true;
            this.cbTemplates.TabIndex = 28;
            this.cbTemplates.SelectedIndexChanged += new System.EventHandler(this.cbTemplates_SelectedIndexChanged);
            // 
            // linkManageTemplates
            // 
            this.linkManageTemplates.Location = new System.Drawing.Point(172, 35);
            this.linkManageTemplates.Name = "linkManageTemplates";
            this.linkManageTemplates.Size = new System.Drawing.Size(140, 13);
            this.linkManageTemplates.TabIndex = 30;
            this.linkManageTemplates.TabStop = true;
            this.linkManageTemplates.Text = "Manage Templates";
            this.linkManageTemplates.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkManageTemplates.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkManageTemplates_LinkClicked);
            // 
            // panelManageTemplates
            // 
            this.panelManageTemplates.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelManageTemplates.Controls.Add(this.btnClose);
            this.panelManageTemplates.Controls.Add(this.btnDeleteTemplate);
            this.panelManageTemplates.Controls.Add(this.btnRename);
            this.panelManageTemplates.Controls.Add(this.txtRename);
            this.panelManageTemplates.Controls.Add(this.lblRename);
            this.panelManageTemplates.Location = new System.Drawing.Point(11, 91);
            this.panelManageTemplates.Name = "panelManageTemplates";
            this.panelManageTemplates.Size = new System.Drawing.Size(304, 108);
            this.panelManageTemplates.TabIndex = 31;
            this.panelManageTemplates.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(220, 63);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 23);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDeleteTemplate
            // 
            this.btnDeleteTemplate.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteTemplate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteTemplate.Location = new System.Drawing.Point(12, 62);
            this.btnDeleteTemplate.Name = "btnDeleteTemplate";
            this.btnDeleteTemplate.Size = new System.Drawing.Size(178, 24);
            this.btnDeleteTemplate.TabIndex = 10;
            this.btnDeleteTemplate.Text = "Delete selected template";
            this.btnDeleteTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDeleteTemplate.UseVisualStyleBackColor = false;
            this.btnDeleteTemplate.Click += new System.EventHandler(this.btnDeleteTemplate_Click);
            // 
            // btnRename
            // 
            this.btnRename.BackColor = System.Drawing.SystemColors.Control;
            this.btnRename.Image = ((System.Drawing.Image)(resources.GetObject("btnRename.Image")));
            this.btnRename.Location = new System.Drawing.Point(265, 27);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(27, 22);
            this.btnRename.TabIndex = 9;
            this.btnRename.UseVisualStyleBackColor = false;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(14, 28);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(246, 20);
            this.txtRename.TabIndex = 8;
            // 
            // lblRename
            // 
            this.lblRename.AutoSize = true;
            this.lblRename.Location = new System.Drawing.Point(11, 12);
            this.lblRename.Name = "lblRename";
            this.lblRename.Size = new System.Drawing.Size(136, 13);
            this.lblRename.TabIndex = 7;
            this.lblRename.Text = "Rename selected template:";
            // 
            // grAdd
            // 
            this.grAdd.Controls.Add(this.NextTopic);
            this.grAdd.Controls.Add(this.TopicBefore);
            this.grAdd.Controls.Add(this.Subtopic);
            this.grAdd.Location = new System.Drawing.Point(11, 330);
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
            this.imageList1.Images.SetKeyName(2, "cpAddBefore20.png");
            // 
            // TopicBefore
            // 
            this.TopicBefore.ImageKey = "cpAddBefore20.png";
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
            this.p1.Location = new System.Drawing.Point(17, 30);
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
            this.rbtnCustom.CheckedChanged += new System.EventHandler(this.Template_CheckedChanged);
            // 
            // grTemplate
            // 
            this.grTemplate.Controls.Add(this.rbtnCustom);
            this.grTemplate.Controls.Add(this.lblTopics);
            this.grTemplate.Controls.Add(this.rbtnTopics);
            this.grTemplate.Controls.Add(this.numTopics);
            this.grTemplate.Controls.Add(this.rbtnUseIncrement);
            this.grTemplate.Location = new System.Drawing.Point(11, 89);
            this.grTemplate.Name = "grTemplate";
            this.grTemplate.Size = new System.Drawing.Size(305, 54);
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
            // AddTopicTemplateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(328, 427);
            this.Controls.Add(this.linkSaveTemplate);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.linkManageTemplates);
            this.Controls.Add(this.linkPreview);
            this.Controls.Add(this.cbTemplates);
            this.Controls.Add(this.linkNewTemplate);
            this.Controls.Add(this.lblTemplates);
            this.Controls.Add(this.btnAddTopics);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtTopicText);
            this.Controls.Add(this.lblTopicText);
            this.Controls.Add(this.grIncrement);
            this.Controls.Add(this.grAdd);
            this.Controls.Add(this.grTemplate);
            this.Controls.Add(this.panelManageTemplates);
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
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            this.grPosition.ResumeLayout(false);
            this.grPosition.PerformLayout();
            this.grIncrement.ResumeLayout(false);
            this.grIncrement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSteps)).EndInit();
            this.panelNewTemplate.ResumeLayout(false);
            this.panelNewTemplate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopics)).EndInit();
            this.panelManageTemplates.ResumeLayout(false);
            this.panelManageTemplates.PerformLayout();
            this.grAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.grTemplate.ResumeLayout(false);
            this.grTemplate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTopicText;
        private System.Windows.Forms.TextBox txtTopicText;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.NumericUpDown numEnd;
        private System.Windows.Forms.RadioButton rbtnBegin;
        private System.Windows.Forms.RadioButton rbtnEnd;
        private System.Windows.Forms.NumericUpDown numStep;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddTopics;
        private System.Windows.Forms.GroupBox grPosition;
        private System.Windows.Forms.GroupBox grIncrement;
        private System.Windows.Forms.NumericUpDown numSteps;
        private System.Windows.Forms.RadioButton rbtnSteps;
        private System.Windows.Forms.RadioButton rbtnFinish;
        private System.Windows.Forms.RadioButton rbtnTopics;
        private System.Windows.Forms.RadioButton rbtnUseIncrement;
        private System.Windows.Forms.NumericUpDown numTopics;
        private System.Windows.Forms.Label lblTemplates;
        private System.Windows.Forms.ComboBox cbTemplates;
        private System.Windows.Forms.LinkLabel linkNewTemplate;
        private System.Windows.Forms.LinkLabel linkManageTemplates;
        private System.Windows.Forms.Panel panelNewTemplate;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Panel panelManageTemplates;
        private System.Windows.Forms.Button btnDeleteTemplate;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.Label lblRename;
        private System.Windows.Forms.Button btnClose;
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
        private System.Windows.Forms.LinkLabel linkPreview;
        private System.Windows.Forms.LinkLabel linkSaveTemplate;
    }
}