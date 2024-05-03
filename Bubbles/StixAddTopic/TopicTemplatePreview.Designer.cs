namespace Bubbles
{
    partial class TopicTemplatePreview
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panelIncrement = new System.Windows.Forms.Panel();
            this.linkMore = new System.Windows.Forms.LinkLabel();
            this.numFinish = new System.Windows.Forms.NumericUpDown();
            this.lblFinish = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.panelMore = new System.Windows.Forms.Panel();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.lblStep = new System.Windows.Forms.Label();
            this.chBoxNumPosition = new System.Windows.Forms.CheckBox();
            this.p1 = new System.Windows.Forms.PictureBox();
            this.panelIncrement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFinish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            this.panelMore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(300, 147);
            this.listBox1.TabIndex = 0;
            // 
            // panelIncrement
            // 
            this.panelIncrement.BackColor = System.Drawing.SystemColors.Control;
            this.panelIncrement.Controls.Add(this.linkMore);
            this.panelIncrement.Controls.Add(this.numFinish);
            this.panelIncrement.Controls.Add(this.lblFinish);
            this.panelIncrement.Controls.Add(this.lblStart);
            this.panelIncrement.Controls.Add(this.numStart);
            this.panelIncrement.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelIncrement.Location = new System.Drawing.Point(251, 1);
            this.panelIncrement.Name = "panelIncrement";
            this.panelIncrement.Size = new System.Drawing.Size(46, 113);
            this.panelIncrement.TabIndex = 1;
            this.panelIncrement.Visible = false;
            // 
            // linkMore
            // 
            this.linkMore.AutoSize = true;
            this.linkMore.Location = new System.Drawing.Point(3, 91);
            this.linkMore.Name = "linkMore";
            this.linkMore.Size = new System.Drawing.Size(40, 13);
            this.linkMore.TabIndex = 28;
            this.linkMore.TabStop = true;
            this.linkMore.Text = "More...";
            this.linkMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMore_LinkClicked);
            // 
            // numFinish
            // 
            this.numFinish.Location = new System.Drawing.Point(6, 65);
            this.numFinish.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numFinish.Name = "numFinish";
            this.numFinish.Size = new System.Drawing.Size(35, 20);
            this.numFinish.TabIndex = 26;
            this.numFinish.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numFinish.ValueChanged += new System.EventHandler(this.numEnd_ValueChanged);
            // 
            // lblFinish
            // 
            this.lblFinish.AutoSize = true;
            this.lblFinish.Location = new System.Drawing.Point(3, 49);
            this.lblFinish.Name = "lblFinish";
            this.lblFinish.Size = new System.Drawing.Size(37, 13);
            this.lblFinish.TabIndex = 27;
            this.lblFinish.Text = "Finish:";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(3, 6);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(32, 13);
            this.lblStart.TabIndex = 24;
            this.lblStart.Text = "Start:";
            // 
            // numStart
            // 
            this.numStart.Location = new System.Drawing.Point(6, 22);
            this.numStart.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(35, 20);
            this.numStart.TabIndex = 25;
            this.numStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStart.ValueChanged += new System.EventHandler(this.numStart_ValueChanged);
            // 
            // panelMore
            // 
            this.panelMore.Controls.Add(this.numStep);
            this.panelMore.Controls.Add(this.lblStep);
            this.panelMore.Controls.Add(this.chBoxNumPosition);
            this.panelMore.Location = new System.Drawing.Point(1, 113);
            this.panelMore.Name = "panelMore";
            this.panelMore.Size = new System.Drawing.Size(296, 31);
            this.panelMore.TabIndex = 2;
            this.panelMore.Visible = false;
            // 
            // numStep
            // 
            this.numStep.Location = new System.Drawing.Point(99, 8);
            this.numStep.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numStep.Name = "numStep";
            this.numStep.Size = new System.Drawing.Size(35, 20);
            this.numStep.TabIndex = 16;
            this.numStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStep.ValueChanged += new System.EventHandler(this.numStep_ValueChanged);
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(3, 11);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(94, 13);
            this.lblStep.TabIndex = 15;
            this.lblStep.Text = "Шаг инкремента:";
            // 
            // chBoxNumPosition
            // 
            this.chBoxNumPosition.AutoSize = true;
            this.chBoxNumPosition.Location = new System.Drawing.Point(144, 10);
            this.chBoxNumPosition.Name = "chBoxNumPosition";
            this.chBoxNumPosition.Size = new System.Drawing.Size(138, 17);
            this.chBoxNumPosition.TabIndex = 17;
            this.chBoxNumPosition.Text = "Номер перед текстом";
            this.chBoxNumPosition.UseVisualStyleBackColor = true;
            this.chBoxNumPosition.CheckedChanged += new System.EventHandler(this.chBoxNumPosition_CheckedChanged);
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(251, 1);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(16, 16);
            this.p1.TabIndex = 37;
            this.p1.TabStop = false;
            this.p1.Visible = false;
            // 
            // TopicTemplatePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.p1);
            this.Controls.Add(this.panelMore);
            this.Controls.Add(this.panelIncrement);
            this.Controls.Add(this.listBox1);
            this.Name = "TopicTemplatePreview";
            this.Size = new System.Drawing.Size(300, 147);
            this.panelIncrement.ResumeLayout(false);
            this.panelIncrement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFinish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            this.panelMore.ResumeLayout(false);
            this.panelMore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.LinkLabel linkMore;
        private System.Windows.Forms.Label lblFinish;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblStep;
        public System.Windows.Forms.NumericUpDown numFinish;
        public System.Windows.Forms.NumericUpDown numStart;
        public System.Windows.Forms.CheckBox chBoxNumPosition;
        public System.Windows.Forms.NumericUpDown numStep;
        public System.Windows.Forms.Panel panelIncrement;
        public System.Windows.Forms.Panel panelMore;
        public System.Windows.Forms.PictureBox p1;
    }
}
