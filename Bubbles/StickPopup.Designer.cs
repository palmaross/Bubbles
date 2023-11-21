namespace Bubbles
{
    partial class StickPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StickPopup));
            this.panelH = new System.Windows.Forms.Panel();
            this.pClose = new System.Windows.Forms.PictureBox();
            this.pRemember = new System.Windows.Forms.PictureBox();
            this.pRotate = new System.Windows.Forms.PictureBox();
            this.pExpand = new System.Windows.Forms.PictureBox();
            this.pCollapse = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pExpand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCollapse)).BeginInit();
            this.SuspendLayout();
            // 
            // panelH
            // 
            this.panelH.BackColor = System.Drawing.SystemColors.Control;
            this.panelH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelH.Controls.Add(this.pClose);
            this.panelH.Controls.Add(this.pRemember);
            this.panelH.Controls.Add(this.pRotate);
            this.panelH.Controls.Add(this.pExpand);
            this.panelH.Controls.Add(this.pCollapse);
            this.panelH.Location = new System.Drawing.Point(17, 3);
            this.panelH.Name = "panelH";
            this.panelH.Size = new System.Drawing.Size(112, 20);
            this.panelH.TabIndex = 0;
            // 
            // pClose
            // 
            this.pClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pClose.Image = ((System.Drawing.Image)(resources.GetObject("pClose.Image")));
            this.pClose.Location = new System.Drawing.Point(92, 1);
            this.pClose.Name = "pClose";
            this.pClose.Size = new System.Drawing.Size(16, 16);
            this.pClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pClose.TabIndex = 5;
            this.pClose.TabStop = false;
            this.pClose.Click += new System.EventHandler(this.pClose_Click);
            // 
            // pRemember
            // 
            this.pRemember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pRemember.Image = ((System.Drawing.Image)(resources.GetObject("pRemember.Image")));
            this.pRemember.Location = new System.Drawing.Point(65, 1);
            this.pRemember.Name = "pRemember";
            this.pRemember.Size = new System.Drawing.Size(16, 16);
            this.pRemember.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRemember.TabIndex = 4;
            this.pRemember.TabStop = false;
            this.pRemember.Click += new System.EventHandler(this.pRemember_Click);
            // 
            // pRotate
            // 
            this.pRotate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pRotate.Image = ((System.Drawing.Image)(resources.GetObject("pRotate.Image")));
            this.pRotate.Location = new System.Drawing.Point(44, 1);
            this.pRotate.Name = "pRotate";
            this.pRotate.Size = new System.Drawing.Size(16, 16);
            this.pRotate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pRotate.TabIndex = 3;
            this.pRotate.TabStop = false;
            this.pRotate.Click += new System.EventHandler(this.pRotate_Click);
            // 
            // pExpand
            // 
            this.pExpand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pExpand.Image = ((System.Drawing.Image)(resources.GetObject("pExpand.Image")));
            this.pExpand.Location = new System.Drawing.Point(23, 1);
            this.pExpand.Name = "pExpand";
            this.pExpand.Size = new System.Drawing.Size(16, 16);
            this.pExpand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pExpand.TabIndex = 2;
            this.pExpand.TabStop = false;
            this.pExpand.Click += new System.EventHandler(this.pExpand_Click);
            // 
            // pCollapse
            // 
            this.pCollapse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pCollapse.Image = ((System.Drawing.Image)(resources.GetObject("pCollapse.Image")));
            this.pCollapse.Location = new System.Drawing.Point(2, 1);
            this.pCollapse.Name = "pCollapse";
            this.pCollapse.Size = new System.Drawing.Size(16, 16);
            this.pCollapse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCollapse.TabIndex = 1;
            this.pCollapse.TabStop = false;
            this.pCollapse.Click += new System.EventHandler(this.pCollapse_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // StickPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelH);
            this.Name = "StickPopup";
            this.Size = new System.Drawing.Size(197, 137);
            this.panelH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRemember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pExpand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCollapse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelH;
        private System.Windows.Forms.PictureBox pCollapse;
        private System.Windows.Forms.PictureBox pExpand;
        private System.Windows.Forms.PictureBox pRemember;
        private System.Windows.Forms.PictureBox pRotate;
        private System.Windows.Forms.PictureBox pClose;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
