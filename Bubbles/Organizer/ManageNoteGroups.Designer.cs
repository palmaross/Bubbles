namespace Organizer
{
    partial class ManageNoteGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageNoteGroups));
            this.groupBoxAddNewGroup = new System.Windows.Forms.GroupBox();
            this.txtNewGroup = new System.Windows.Forms.TextBox();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnDeleteGroup = new System.Windows.Forms.PictureBox();
            this.groupBoxRenameGroup = new System.Windows.Forms.GroupBox();
            this.txtRenameGroup = new System.Windows.Forms.TextBox();
            this.btnRenameGroup = new System.Windows.Forms.Button();
            this.cbGroups = new System.Windows.Forms.ComboBox();
            this.panelDeleteGroup = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteGroupOK = new System.Windows.Forms.Button();
            this.rbtnDeleteGroupCancel = new System.Windows.Forms.RadioButton();
            this.rbtnDeleteGroupNoNotes = new System.Windows.Forms.RadioButton();
            this.rbtnDeleteGroupAndNotes = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxAddNewGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGroup)).BeginInit();
            this.groupBoxRenameGroup.SuspendLayout();
            this.panelDeleteGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAddNewGroup
            // 
            this.groupBoxAddNewGroup.Controls.Add(this.txtNewGroup);
            this.groupBoxAddNewGroup.Controls.Add(this.btnAddGroup);
            this.groupBoxAddNewGroup.Location = new System.Drawing.Point(30, 43);
            this.groupBoxAddNewGroup.Name = "groupBoxAddNewGroup";
            this.groupBoxAddNewGroup.Size = new System.Drawing.Size(245, 74);
            this.groupBoxAddNewGroup.TabIndex = 103;
            this.groupBoxAddNewGroup.TabStop = false;
            this.groupBoxAddNewGroup.Text = "Добавить новую группу";
            // 
            // txtNewGroup
            // 
            this.txtNewGroup.Location = new System.Drawing.Point(9, 17);
            this.txtNewGroup.Name = "txtNewGroup";
            this.txtNewGroup.Size = new System.Drawing.Size(227, 20);
            this.txtNewGroup.TabIndex = 5;
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(126, 43);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(110, 23);
            this.btnAddGroup.TabIndex = 5;
            this.btnAddGroup.Text = "Добавить";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.Image")));
            this.btnDeleteGroup.Location = new System.Drawing.Point(257, 14);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(16, 16);
            this.btnDeleteGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnDeleteGroup.TabIndex = 104;
            this.btnDeleteGroup.TabStop = false;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // groupBoxRenameGroup
            // 
            this.groupBoxRenameGroup.Controls.Add(this.txtRenameGroup);
            this.groupBoxRenameGroup.Controls.Add(this.btnRenameGroup);
            this.groupBoxRenameGroup.Location = new System.Drawing.Point(30, 126);
            this.groupBoxRenameGroup.Name = "groupBoxRenameGroup";
            this.groupBoxRenameGroup.Size = new System.Drawing.Size(245, 74);
            this.groupBoxRenameGroup.TabIndex = 102;
            this.groupBoxRenameGroup.TabStop = false;
            this.groupBoxRenameGroup.Text = "Переименовать выбранную группу";
            // 
            // txtRenameGroup
            // 
            this.txtRenameGroup.Location = new System.Drawing.Point(9, 19);
            this.txtRenameGroup.Name = "txtRenameGroup";
            this.txtRenameGroup.Size = new System.Drawing.Size(227, 20);
            this.txtRenameGroup.TabIndex = 5;
            // 
            // btnRenameGroup
            // 
            this.btnRenameGroup.Location = new System.Drawing.Point(126, 45);
            this.btnRenameGroup.Name = "btnRenameGroup";
            this.btnRenameGroup.Size = new System.Drawing.Size(110, 23);
            this.btnRenameGroup.TabIndex = 2;
            this.btnRenameGroup.Text = "Переименовать";
            this.btnRenameGroup.UseVisualStyleBackColor = true;
            this.btnRenameGroup.Click += new System.EventHandler(this.btnRenameGroup_Click);
            // 
            // cbGroups
            // 
            this.cbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroups.FormattingEnabled = true;
            this.cbGroups.Location = new System.Drawing.Point(31, 12);
            this.cbGroups.Name = "cbGroups";
            this.cbGroups.Size = new System.Drawing.Size(220, 21);
            this.cbGroups.Sorted = true;
            this.cbGroups.TabIndex = 101;
            this.cbGroups.SelectedIndexChanged += new System.EventHandler(this.cbGroups_SelectedIndexChanged);
            // 
            // panelDeleteGroup
            // 
            this.panelDeleteGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDeleteGroup.Controls.Add(this.label1);
            this.panelDeleteGroup.Controls.Add(this.btnDeleteGroupOK);
            this.panelDeleteGroup.Controls.Add(this.rbtnDeleteGroupCancel);
            this.panelDeleteGroup.Controls.Add(this.rbtnDeleteGroupNoNotes);
            this.panelDeleteGroup.Controls.Add(this.rbtnDeleteGroupAndNotes);
            this.panelDeleteGroup.Location = new System.Drawing.Point(13, 115);
            this.panelDeleteGroup.Name = "panelDeleteGroup";
            this.panelDeleteGroup.Size = new System.Drawing.Size(282, 186);
            this.panelDeleteGroup.TabIndex = 101;
            this.panelDeleteGroup.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Location 13x12";
            this.label1.Visible = false;
            // 
            // btnDeleteGroupOK
            // 
            this.btnDeleteGroupOK.Location = new System.Drawing.Point(116, 136);
            this.btnDeleteGroupOK.Name = "btnDeleteGroupOK";
            this.btnDeleteGroupOK.Size = new System.Drawing.Size(50, 23);
            this.btnDeleteGroupOK.TabIndex = 3;
            this.btnDeleteGroupOK.Text = "OK";
            this.btnDeleteGroupOK.UseVisualStyleBackColor = true;
            this.btnDeleteGroupOK.Click += new System.EventHandler(this.btnDeleteGroupOK_Click);
            // 
            // rbtnDeleteGroupCancel
            // 
            this.rbtnDeleteGroupCancel.AutoSize = true;
            this.rbtnDeleteGroupCancel.Checked = true;
            this.rbtnDeleteGroupCancel.Location = new System.Drawing.Point(11, 102);
            this.rbtnDeleteGroupCancel.Name = "rbtnDeleteGroupCancel";
            this.rbtnDeleteGroupCancel.Size = new System.Drawing.Size(128, 17);
            this.rbtnDeleteGroupCancel.TabIndex = 2;
            this.rbtnDeleteGroupCancel.TabStop = true;
            this.rbtnDeleteGroupCancel.Text = "Отменить операцию";
            this.rbtnDeleteGroupCancel.UseVisualStyleBackColor = true;
            // 
            // rbtnDeleteGroupNoNotes
            // 
            this.rbtnDeleteGroupNoNotes.Location = new System.Drawing.Point(11, 54);
            this.rbtnDeleteGroupNoNotes.Name = "rbtnDeleteGroupNoNotes";
            this.rbtnDeleteGroupNoNotes.Size = new System.Drawing.Size(251, 35);
            this.rbtnDeleteGroupNoNotes.TabIndex = 1;
            this.rbtnDeleteGroupNoNotes.Text = "Удалить группу, заметки группы перевести в категорию \"Без группы\")";
            this.rbtnDeleteGroupNoNotes.UseVisualStyleBackColor = true;
            // 
            // rbtnDeleteGroupAndNotes
            // 
            this.rbtnDeleteGroupAndNotes.AutoSize = true;
            this.rbtnDeleteGroupAndNotes.Location = new System.Drawing.Point(11, 26);
            this.rbtnDeleteGroupAndNotes.Name = "rbtnDeleteGroupAndNotes";
            this.rbtnDeleteGroupAndNotes.Size = new System.Drawing.Size(219, 17);
            this.rbtnDeleteGroupAndNotes.TabIndex = 0;
            this.rbtnDeleteGroupAndNotes.Text = "Удалить группу и все заметки группы";
            this.rbtnDeleteGroupAndNotes.UseVisualStyleBackColor = true;
            // 
            // ManageNoteGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelDeleteGroup);
            this.Controls.Add(this.groupBoxAddNewGroup);
            this.Controls.Add(this.btnDeleteGroup);
            this.Controls.Add(this.groupBoxRenameGroup);
            this.Controls.Add(this.cbGroups);
            this.Name = "ManageNoteGroups";
            this.Size = new System.Drawing.Size(308, 210);
            this.groupBoxAddNewGroup.ResumeLayout(false);
            this.groupBoxAddNewGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGroup)).EndInit();
            this.groupBoxRenameGroup.ResumeLayout(false);
            this.groupBoxRenameGroup.PerformLayout();
            this.panelDeleteGroup.ResumeLayout(false);
            this.panelDeleteGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAddNewGroup;
        private System.Windows.Forms.TextBox txtNewGroup;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.GroupBox groupBoxRenameGroup;
        private System.Windows.Forms.TextBox txtRenameGroup;
        private System.Windows.Forms.Button btnRenameGroup;
        private System.Windows.Forms.Panel panelDeleteGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteGroupOK;
        private System.Windows.Forms.RadioButton rbtnDeleteGroupCancel;
        private System.Windows.Forms.RadioButton rbtnDeleteGroupNoNotes;
        private System.Windows.Forms.RadioButton rbtnDeleteGroupAndNotes;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ComboBox cbGroups;
        public System.Windows.Forms.PictureBox btnDeleteGroup;
    }
}
