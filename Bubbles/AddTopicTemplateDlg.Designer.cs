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
            this.lblTopicText = new System.Windows.Forms.Label();
            this.txtTopicText = new System.Windows.Forms.TextBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.numFinish = new System.Windows.Forms.NumericUpDown();
            this.rbtnBegin = new System.Windows.Forms.RadioButton();
            this.rbtnEnd = new System.Windows.Forms.RadioButton();
            this.lblStep = new System.Windows.Forms.Label();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grPosition = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.grIncrement = new System.Windows.Forms.GroupBox();
            this.grFromTo = new System.Windows.Forms.GroupBox();
            this.numSteps = new System.Windows.Forms.NumericUpDown();
            this.rbtnSteps = new System.Windows.Forms.RadioButton();
            this.rbtnFinish = new System.Windows.Forms.RadioButton();
            this.rbtnTopics = new System.Windows.Forms.RadioButton();
            this.rbtnUseIncrement = new System.Windows.Forms.RadioButton();
            this.numTopics = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFinish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            this.grPosition.SuspendLayout();
            this.grIncrement.SuspendLayout();
            this.grFromTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTopics)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTopicText
            // 
            this.lblTopicText.AutoSize = true;
            this.lblTopicText.Location = new System.Drawing.Point(12, 16);
            this.lblTopicText.Name = "lblTopicText";
            this.lblTopicText.Size = new System.Drawing.Size(61, 13);
            this.lblTopicText.TabIndex = 0;
            this.lblTopicText.Text = "Topic Text:";
            // 
            // txtTopicText
            // 
            this.txtTopicText.Location = new System.Drawing.Point(79, 14);
            this.txtTopicText.Name = "txtTopicText";
            this.txtTopicText.Size = new System.Drawing.Size(204, 20);
            this.txtTopicText.TabIndex = 1;
            this.txtTopicText.Text = "Day";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(27, 22);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(32, 13);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "Start:";
            // 
            // numStart
            // 
            this.numStart.Location = new System.Drawing.Point(68, 20);
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
            // numFinish
            // 
            this.numFinish.Location = new System.Drawing.Point(69, 51);
            this.numFinish.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numFinish.Name = "numFinish";
            this.numFinish.Size = new System.Drawing.Size(38, 20);
            this.numFinish.TabIndex = 5;
            this.numFinish.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // rbtnBegin
            // 
            this.rbtnBegin.AutoSize = true;
            this.rbtnBegin.Location = new System.Drawing.Point(45, 16);
            this.rbtnBegin.Name = "rbtnBegin";
            this.rbtnBegin.Size = new System.Drawing.Size(52, 17);
            this.rbtnBegin.TabIndex = 9;
            this.rbtnBegin.Text = "Begin";
            this.rbtnBegin.UseVisualStyleBackColor = true;
            // 
            // rbtnEnd
            // 
            this.rbtnEnd.AutoSize = true;
            this.rbtnEnd.Checked = true;
            this.rbtnEnd.Location = new System.Drawing.Point(163, 16);
            this.rbtnEnd.Name = "rbtnEnd";
            this.rbtnEnd.Size = new System.Drawing.Size(44, 17);
            this.rbtnEnd.TabIndex = 10;
            this.rbtnEnd.TabStop = true;
            this.rbtnEnd.Text = "End";
            this.rbtnEnd.UseVisualStyleBackColor = true;
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(150, 22);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(32, 13);
            this.lblStep.TabIndex = 13;
            this.lblStep.Text = "Step:";
            // 
            // numStep
            // 
            this.numStep.Location = new System.Drawing.Point(194, 19);
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
            this.btnCancel.Location = new System.Drawing.Point(210, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = " Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(129, 259);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grPosition
            // 
            this.grPosition.Controls.Add(this.rbtnBegin);
            this.grPosition.Controls.Add(this.rbtnEnd);
            this.grPosition.Location = new System.Drawing.Point(11, 117);
            this.grPosition.Name = "grPosition";
            this.grPosition.Size = new System.Drawing.Size(251, 40);
            this.grPosition.TabIndex = 22;
            this.grPosition.TabStop = false;
            this.grPosition.Text = "Position:";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreview.Location = new System.Drawing.Point(15, 259);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(92, 23);
            this.btnPreview.TabIndex = 24;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // grIncrement
            // 
            this.grIncrement.Controls.Add(this.grFromTo);
            this.grIncrement.Controls.Add(this.grPosition);
            this.grIncrement.Location = new System.Drawing.Point(12, 74);
            this.grIncrement.Name = "grIncrement";
            this.grIncrement.Size = new System.Drawing.Size(273, 169);
            this.grIncrement.TabIndex = 25;
            this.grIncrement.TabStop = false;
            this.grIncrement.Text = "Increment";
            // 
            // grFromTo
            // 
            this.grFromTo.Controls.Add(this.numSteps);
            this.grFromTo.Controls.Add(this.rbtnSteps);
            this.grFromTo.Controls.Add(this.rbtnFinish);
            this.grFromTo.Controls.Add(this.numStart);
            this.grFromTo.Controls.Add(this.numStep);
            this.grFromTo.Controls.Add(this.numFinish);
            this.grFromTo.Controls.Add(this.lblStep);
            this.grFromTo.Controls.Add(this.lblStart);
            this.grFromTo.Location = new System.Drawing.Point(11, 23);
            this.grFromTo.Name = "grFromTo";
            this.grFromTo.Size = new System.Drawing.Size(251, 80);
            this.grFromTo.TabIndex = 15;
            this.grFromTo.TabStop = false;
            this.grFromTo.Text = "From-To";
            // 
            // numSteps
            // 
            this.numSteps.Location = new System.Drawing.Point(194, 51);
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
            this.rbtnSteps.Location = new System.Drawing.Point(133, 52);
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
            this.rbtnFinish.Location = new System.Drawing.Point(11, 52);
            this.rbtnFinish.Name = "rbtnFinish";
            this.rbtnFinish.Size = new System.Drawing.Size(47, 17);
            this.rbtnFinish.TabIndex = 11;
            this.rbtnFinish.TabStop = true;
            this.rbtnFinish.Text = "End:";
            this.rbtnFinish.UseVisualStyleBackColor = true;
            // 
            // rbtnTopics
            // 
            this.rbtnTopics.AutoSize = true;
            this.rbtnTopics.Location = new System.Drawing.Point(15, 45);
            this.rbtnTopics.Name = "rbtnTopics";
            this.rbtnTopics.Size = new System.Drawing.Size(60, 17);
            this.rbtnTopics.TabIndex = 17;
            this.rbtnTopics.Text = "Topics:";
            this.rbtnTopics.UseVisualStyleBackColor = true;
            this.rbtnTopics.CheckedChanged += new System.EventHandler(this.rbtnTopics_CheckedChanged);
            // 
            // rbtnUseIncrement
            // 
            this.rbtnUseIncrement.AutoSize = true;
            this.rbtnUseIncrement.Checked = true;
            this.rbtnUseIncrement.Location = new System.Drawing.Point(186, 45);
            this.rbtnUseIncrement.Name = "rbtnUseIncrement";
            this.rbtnUseIncrement.Size = new System.Drawing.Size(94, 17);
            this.rbtnUseIncrement.TabIndex = 26;
            this.rbtnUseIncrement.TabStop = true;
            this.rbtnUseIncrement.Text = "Use Increment";
            this.rbtnUseIncrement.UseVisualStyleBackColor = true;
            // 
            // numTopics
            // 
            this.numTopics.Location = new System.Drawing.Point(77, 45);
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
            // 
            // AddTopicTemplateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(297, 292);
            this.Controls.Add(this.numTopics);
            this.Controls.Add(this.rbtnUseIncrement);
            this.Controls.Add(this.rbtnTopics);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtTopicText);
            this.Controls.Add(this.lblTopicText);
            this.Controls.Add(this.grIncrement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTopicTemplateDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Topic Template";
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFinish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            this.grPosition.ResumeLayout(false);
            this.grPosition.PerformLayout();
            this.grIncrement.ResumeLayout(false);
            this.grFromTo.ResumeLayout(false);
            this.grFromTo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTopics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTopicText;
        private System.Windows.Forms.TextBox txtTopicText;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.NumericUpDown numFinish;
        private System.Windows.Forms.RadioButton rbtnBegin;
        private System.Windows.Forms.RadioButton rbtnEnd;
        private System.Windows.Forms.NumericUpDown numStep;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grPosition;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.GroupBox grIncrement;
        private System.Windows.Forms.GroupBox grFromTo;
        private System.Windows.Forms.NumericUpDown numSteps;
        private System.Windows.Forms.RadioButton rbtnSteps;
        private System.Windows.Forms.RadioButton rbtnFinish;
        private System.Windows.Forms.RadioButton rbtnTopics;
        private System.Windows.Forms.RadioButton rbtnUseIncrement;
        private System.Windows.Forms.NumericUpDown numTopics;
    }
}