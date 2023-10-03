namespace Bubbles
{
    partial class SelectIconDlg
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pSpace = new System.Windows.Forms.PictureBox();
            this.pBox = new System.Windows.Forms.PictureBox();
            this.txtIconName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rbtnLeft = new System.Windows.Forms.RadioButton();
            this.rbtnRight = new System.Windows.Forms.RadioButton();
            this.rbtnEnd = new System.Windows.Forms.RadioButton();
            this.rbtnBegin = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 32);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(173, 310);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pSpace);
            this.panel1.Controls.Add(this.pBox);
            this.panel1.Location = new System.Drawing.Point(196, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 310);
            this.panel1.TabIndex = 1;
            // 
            // pSpace
            // 
            this.pSpace.Location = new System.Drawing.Point(7, 63);
            this.pSpace.Name = "pSpace";
            this.pSpace.Size = new System.Drawing.Size(10, 10);
            this.pSpace.TabIndex = 1;
            this.pSpace.TabStop = false;
            this.pSpace.Visible = false;
            // 
            // pBox
            // 
            this.pBox.Location = new System.Drawing.Point(7, 26);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(16, 16);
            this.pBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBox.TabIndex = 0;
            this.pBox.TabStop = false;
            this.pBox.Visible = false;
            // 
            // txtIconName
            // 
            this.txtIconName.Location = new System.Drawing.Point(12, 349);
            this.txtIconName.Name = "txtIconName";
            this.txtIconName.Size = new System.Drawing.Size(102, 20);
            this.txtIconName.TabIndex = 2;
            this.txtIconName.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(277, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(194, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rbtnLeft
            // 
            this.rbtnLeft.AutoSize = true;
            this.rbtnLeft.Location = new System.Drawing.Point(13, 8);
            this.rbtnLeft.Name = "rbtnLeft";
            this.rbtnLeft.Size = new System.Drawing.Size(56, 17);
            this.rbtnLeft.TabIndex = 4;
            this.rbtnLeft.Text = "Слева";
            this.rbtnLeft.UseVisualStyleBackColor = true;
            // 
            // rbtnRight
            // 
            this.rbtnRight.AutoSize = true;
            this.rbtnRight.Location = new System.Drawing.Point(98, 8);
            this.rbtnRight.Name = "rbtnRight";
            this.rbtnRight.Size = new System.Drawing.Size(62, 17);
            this.rbtnRight.TabIndex = 5;
            this.rbtnRight.Text = "Справа";
            this.rbtnRight.UseVisualStyleBackColor = true;
            // 
            // rbtnEnd
            // 
            this.rbtnEnd.AutoSize = true;
            this.rbtnEnd.Checked = true;
            this.rbtnEnd.Location = new System.Drawing.Point(282, 8);
            this.rbtnEnd.Name = "rbtnEnd";
            this.rbtnEnd.Size = new System.Drawing.Size(65, 17);
            this.rbtnEnd.TabIndex = 6;
            this.rbtnEnd.TabStop = true;
            this.rbtnEnd.Text = "В конец";
            this.rbtnEnd.UseVisualStyleBackColor = true;
            // 
            // rbtnBegin
            // 
            this.rbtnBegin.AutoSize = true;
            this.rbtnBegin.Location = new System.Drawing.Point(190, 8);
            this.rbtnBegin.Name = "rbtnBegin";
            this.rbtnBegin.Size = new System.Drawing.Size(70, 17);
            this.rbtnBegin.TabIndex = 7;
            this.rbtnBegin.Text = "В начало";
            this.rbtnBegin.UseVisualStyleBackColor = true;
            // 
            // SelectIconDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(359, 381);
            this.Controls.Add(this.rbtnBegin);
            this.Controls.Add(this.rbtnEnd);
            this.Controls.Add(this.rbtnRight);
            this.Controls.Add(this.rbtnLeft);
            this.Controls.Add(this.txtIconName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectIconDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Icon";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pSpace;
        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.TextBox txtIconName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.RadioButton rbtnLeft;
        public System.Windows.Forms.RadioButton rbtnRight;
        public System.Windows.Forms.RadioButton rbtnEnd;
        public System.Windows.Forms.RadioButton rbtnBegin;
    }
}