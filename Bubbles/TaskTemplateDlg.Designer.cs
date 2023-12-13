namespace Bubbles
{
    partial class TaskTemplateDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskTemplateDlg));
            this.chProgress = new System.Windows.Forms.CheckBox();
            this.cbTaskTemplates = new System.Windows.Forms.ComboBox();
            this.pProgress = new System.Windows.Forms.PictureBox();
            this.lblChangeValue = new System.Windows.Forms.Label();
            this.chPriority = new System.Windows.Forms.CheckBox();
            this.pPriority = new System.Windows.Forms.PictureBox();
            this.chStartDate = new System.Windows.Forms.CheckBox();
            this.chDueDate = new System.Windows.Forms.CheckBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.chPeriod = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbStartDatePeriod = new System.Windows.Forms.ComboBox();
            this.cbDueDatePeriod = new System.Windows.Forms.ComboBox();
            this.pIcon = new System.Windows.Forms.PictureBox();
            this.chIcon = new System.Windows.Forms.CheckBox();
            this.lblChangeIcon = new System.Windows.Forms.Label();
            this.chResources = new System.Windows.Forms.CheckBox();
            this.txtResources = new System.Windows.Forms.TextBox();
            this.chTags = new System.Windows.Forms.CheckBox();
            this.txtTagGroup = new System.Windows.Forms.TextBox();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.pNewTag = new System.Windows.Forms.PictureBox();
            this.pNewProperty = new System.Windows.Forms.PictureBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtProperty = new System.Windows.Forms.TextBox();
            this.chProperties = new System.Windows.Forms.CheckBox();
            this.lblTagGroup = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.lblProperty = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbResources = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewTag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewProperty)).BeginInit();
            this.SuspendLayout();
            // 
            // chProgress
            // 
            this.chProgress.AutoSize = true;
            this.chProgress.Location = new System.Drawing.Point(12, 48);
            this.chProgress.Name = "chProgress";
            this.chProgress.Size = new System.Drawing.Size(70, 17);
            this.chProgress.TabIndex = 1;
            this.chProgress.Text = "Progress:";
            this.chProgress.UseVisualStyleBackColor = true;
            // 
            // cbTaskTemplates
            // 
            this.cbTaskTemplates.FormattingEnabled = true;
            this.cbTaskTemplates.Location = new System.Drawing.Point(49, 12);
            this.cbTaskTemplates.Name = "cbTaskTemplates";
            this.cbTaskTemplates.Size = new System.Drawing.Size(252, 21);
            this.cbTaskTemplates.TabIndex = 2;
            // 
            // pProgress
            // 
            this.pProgress.Image = ((System.Drawing.Image)(resources.GetObject("pProgress.Image")));
            this.pProgress.Location = new System.Drawing.Point(97, 48);
            this.pProgress.Name = "pProgress";
            this.pProgress.Size = new System.Drawing.Size(16, 16);
            this.pProgress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pProgress.TabIndex = 3;
            this.pProgress.TabStop = false;
            // 
            // lblChangeValue
            // 
            this.lblChangeValue.AutoSize = true;
            this.lblChangeValue.Location = new System.Drawing.Point(125, 62);
            this.lblChangeValue.Name = "lblChangeValue";
            this.lblChangeValue.Size = new System.Drawing.Size(148, 13);
            this.lblChangeValue.TabIndex = 4;
            this.lblChangeValue.Text = "Click on icon to change value";
            // 
            // chPriority
            // 
            this.chPriority.AutoSize = true;
            this.chPriority.Location = new System.Drawing.Point(12, 73);
            this.chPriority.Name = "chPriority";
            this.chPriority.Size = new System.Drawing.Size(60, 17);
            this.chPriority.TabIndex = 5;
            this.chPriority.Text = "Priority:";
            this.chPriority.UseVisualStyleBackColor = true;
            // 
            // pPriority
            // 
            this.pPriority.Image = ((System.Drawing.Image)(resources.GetObject("pPriority.Image")));
            this.pPriority.Location = new System.Drawing.Point(97, 73);
            this.pPriority.Name = "pPriority";
            this.pPriority.Size = new System.Drawing.Size(16, 16);
            this.pPriority.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pPriority.TabIndex = 6;
            this.pPriority.TabStop = false;
            // 
            // chStartDate
            // 
            this.chStartDate.AutoSize = true;
            this.chStartDate.Location = new System.Drawing.Point(11, 111);
            this.chStartDate.Name = "chStartDate";
            this.chStartDate.Size = new System.Drawing.Size(74, 17);
            this.chStartDate.TabIndex = 9;
            this.chStartDate.Text = "StartDate:";
            this.chStartDate.UseVisualStyleBackColor = true;
            // 
            // chDueDate
            // 
            this.chDueDate.AutoSize = true;
            this.chDueDate.Location = new System.Drawing.Point(11, 135);
            this.chDueDate.Name = "chDueDate";
            this.chDueDate.Size = new System.Drawing.Size(72, 17);
            this.chDueDate.TabIndex = 10;
            this.chDueDate.Text = "DueDate:";
            this.chDueDate.UseVisualStyleBackColor = true;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(97, 108);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(81, 20);
            this.dtpStartDate.TabIndex = 9;
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDate.Location = new System.Drawing.Point(97, 132);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(81, 20);
            this.dtpDueDate.TabIndex = 11;
            // 
            // chPeriod
            // 
            this.chPeriod.AutoSize = true;
            this.chPeriod.Location = new System.Drawing.Point(11, 160);
            this.chPeriod.Name = "chPeriod";
            this.chPeriod.Size = new System.Drawing.Size(59, 17);
            this.chPeriod.TabIndex = 12;
            this.chPeriod.Text = "Period:";
            this.chPeriod.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(97, 158);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(241, 21);
            this.comboBox1.TabIndex = 9;
            // 
            // cbStartDatePeriod
            // 
            this.cbStartDatePeriod.FormattingEnabled = true;
            this.cbStartDatePeriod.Location = new System.Drawing.Point(184, 108);
            this.cbStartDatePeriod.Name = "cbStartDatePeriod";
            this.cbStartDatePeriod.Size = new System.Drawing.Size(154, 21);
            this.cbStartDatePeriod.TabIndex = 13;
            // 
            // cbDueDatePeriod
            // 
            this.cbDueDatePeriod.FormattingEnabled = true;
            this.cbDueDatePeriod.Location = new System.Drawing.Point(184, 132);
            this.cbDueDatePeriod.Name = "cbDueDatePeriod";
            this.cbDueDatePeriod.Size = new System.Drawing.Size(154, 21);
            this.cbDueDatePeriod.TabIndex = 14;
            // 
            // pIcon
            // 
            this.pIcon.Image = ((System.Drawing.Image)(resources.GetObject("pIcon.Image")));
            this.pIcon.Location = new System.Drawing.Point(98, 237);
            this.pIcon.Name = "pIcon";
            this.pIcon.Size = new System.Drawing.Size(16, 16);
            this.pIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pIcon.TabIndex = 10;
            this.pIcon.TabStop = false;
            // 
            // chIcon
            // 
            this.chIcon.AutoSize = true;
            this.chIcon.Location = new System.Drawing.Point(11, 237);
            this.chIcon.Name = "chIcon";
            this.chIcon.Size = new System.Drawing.Size(50, 17);
            this.chIcon.TabIndex = 9;
            this.chIcon.Text = "Icon:";
            this.chIcon.UseVisualStyleBackColor = true;
            // 
            // lblChangeIcon
            // 
            this.lblChangeIcon.AutoSize = true;
            this.lblChangeIcon.Location = new System.Drawing.Point(126, 239);
            this.lblChangeIcon.Name = "lblChangeIcon";
            this.lblChangeIcon.Size = new System.Drawing.Size(104, 13);
            this.lblChangeIcon.TabIndex = 11;
            this.lblChangeIcon.Text = "Click to change icon";
            // 
            // chResources
            // 
            this.chResources.AutoSize = true;
            this.chResources.Location = new System.Drawing.Point(11, 199);
            this.chResources.Name = "chResources";
            this.chResources.Size = new System.Drawing.Size(80, 17);
            this.chResources.TabIndex = 12;
            this.chResources.Text = "Resources:";
            this.chResources.UseVisualStyleBackColor = true;
            // 
            // txtResources
            // 
            this.txtResources.Location = new System.Drawing.Point(98, 196);
            this.txtResources.Name = "txtResources";
            this.txtResources.Size = new System.Drawing.Size(104, 20);
            this.txtResources.TabIndex = 13;
            // 
            // chTags
            // 
            this.chTags.AutoSize = true;
            this.chTags.Location = new System.Drawing.Point(12, 287);
            this.chTags.Name = "chTags";
            this.chTags.Size = new System.Drawing.Size(53, 17);
            this.chTags.TabIndex = 14;
            this.chTags.Text = "Tags:";
            this.chTags.UseVisualStyleBackColor = true;
            // 
            // txtTagGroup
            // 
            this.txtTagGroup.Location = new System.Drawing.Point(98, 285);
            this.txtTagGroup.Name = "txtTagGroup";
            this.txtTagGroup.Size = new System.Drawing.Size(105, 20);
            this.txtTagGroup.TabIndex = 15;
            // 
            // txtTag
            // 
            this.txtTag.Location = new System.Drawing.Point(209, 285);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(105, 20);
            this.txtTag.TabIndex = 16;
            // 
            // pNewTag
            // 
            this.pNewTag.Image = ((System.Drawing.Image)(resources.GetObject("pNewTag.Image")));
            this.pNewTag.Location = new System.Drawing.Point(319, 285);
            this.pNewTag.Name = "pNewTag";
            this.pNewTag.Size = new System.Drawing.Size(20, 20);
            this.pNewTag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pNewTag.TabIndex = 17;
            this.pNewTag.TabStop = false;
            // 
            // pNewProperty
            // 
            this.pNewProperty.Image = ((System.Drawing.Image)(resources.GetObject("pNewProperty.Image")));
            this.pNewProperty.Location = new System.Drawing.Point(318, 335);
            this.pNewProperty.Name = "pNewProperty";
            this.pNewProperty.Size = new System.Drawing.Size(20, 20);
            this.pNewProperty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pNewProperty.TabIndex = 21;
            this.pNewProperty.TabStop = false;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(208, 335);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(104, 20);
            this.txtValue.TabIndex = 20;
            // 
            // txtProperty
            // 
            this.txtProperty.Location = new System.Drawing.Point(97, 335);
            this.txtProperty.Name = "txtProperty";
            this.txtProperty.Size = new System.Drawing.Size(105, 20);
            this.txtProperty.TabIndex = 19;
            // 
            // chProperties
            // 
            this.chProperties.AutoSize = true;
            this.chProperties.Location = new System.Drawing.Point(11, 337);
            this.chProperties.Name = "chProperties";
            this.chProperties.Size = new System.Drawing.Size(76, 17);
            this.chProperties.TabIndex = 18;
            this.chProperties.Text = "Properties:";
            this.chProperties.UseVisualStyleBackColor = true;
            // 
            // lblTagGroup
            // 
            this.lblTagGroup.AutoSize = true;
            this.lblTagGroup.Location = new System.Drawing.Point(98, 269);
            this.lblTagGroup.Name = "lblTagGroup";
            this.lblTagGroup.Size = new System.Drawing.Size(45, 13);
            this.lblTagGroup.TabIndex = 22;
            this.lblTagGroup.Text = "Группа:";
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Location = new System.Drawing.Point(208, 269);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(28, 13);
            this.lblTag.TabIndex = 23;
            this.lblTag.Text = "Тег:";
            // 
            // lblProperty
            // 
            this.lblProperty.AutoSize = true;
            this.lblProperty.Location = new System.Drawing.Point(98, 319);
            this.lblProperty.Name = "lblProperty";
            this.lblProperty.Size = new System.Drawing.Size(58, 13);
            this.lblProperty.TabIndex = 24;
            this.lblProperty.Text = "Свойство:";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(206, 319);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(58, 13);
            this.lblValue.TabIndex = 25;
            this.lblValue.Text = "Значение:";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApply.Location = new System.Drawing.Point(11, 393);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 23);
            this.btnApply.TabIndex = 26;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(264, 393);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 23);
            this.btnClose.TabIndex = 27;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // cbResources
            // 
            this.cbResources.FormattingEnabled = true;
            this.cbResources.Location = new System.Drawing.Point(210, 196);
            this.cbResources.Name = "cbResources";
            this.cbResources.Size = new System.Drawing.Size(128, 21);
            this.cbResources.TabIndex = 28;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(208, 357);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(104, 21);
            this.comboBox2.TabIndex = 29;
            // 
            // TaskTemplateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 428);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.cbResources);
            this.Controls.Add(this.cbDueDatePeriod);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbStartDatePeriod);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.chPeriod);
            this.Controls.Add(this.lblProperty);
            this.Controls.Add(this.dtpDueDate);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.chDueDate);
            this.Controls.Add(this.lblTagGroup);
            this.Controls.Add(this.chStartDate);
            this.Controls.Add(this.pNewProperty);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtProperty);
            this.Controls.Add(this.chProperties);
            this.Controls.Add(this.pNewTag);
            this.Controls.Add(this.txtTag);
            this.Controls.Add(this.txtTagGroup);
            this.Controls.Add(this.chTags);
            this.Controls.Add(this.txtResources);
            this.Controls.Add(this.chResources);
            this.Controls.Add(this.lblChangeIcon);
            this.Controls.Add(this.pIcon);
            this.Controls.Add(this.chIcon);
            this.Controls.Add(this.pPriority);
            this.Controls.Add(this.chPriority);
            this.Controls.Add(this.lblChangeValue);
            this.Controls.Add(this.pProgress);
            this.Controls.Add(this.cbTaskTemplates);
            this.Controls.Add(this.chProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskTemplateDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Template";
            ((System.ComponentModel.ISupportInitialize)(this.pProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewTag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pNewProperty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chProgress;
        private System.Windows.Forms.ComboBox cbTaskTemplates;
        private System.Windows.Forms.PictureBox pProgress;
        private System.Windows.Forms.Label lblChangeValue;
        private System.Windows.Forms.CheckBox chPriority;
        private System.Windows.Forms.PictureBox pPriority;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chPeriod;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.CheckBox chDueDate;
        private System.Windows.Forms.CheckBox chStartDate;
        private System.Windows.Forms.ComboBox cbDueDatePeriod;
        private System.Windows.Forms.ComboBox cbStartDatePeriod;
        private System.Windows.Forms.PictureBox pIcon;
        private System.Windows.Forms.CheckBox chIcon;
        private System.Windows.Forms.Label lblChangeIcon;
        private System.Windows.Forms.CheckBox chResources;
        private System.Windows.Forms.TextBox txtResources;
        private System.Windows.Forms.CheckBox chTags;
        private System.Windows.Forms.TextBox txtTagGroup;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.PictureBox pNewTag;
        private System.Windows.Forms.PictureBox pNewProperty;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtProperty;
        private System.Windows.Forms.CheckBox chProperties;
        private System.Windows.Forms.Label lblTagGroup;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblProperty;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbResources;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}