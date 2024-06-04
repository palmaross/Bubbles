using PRAManager;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class EditToolDlg : Form
    {
        public EditToolDlg()
        {
            InitializeComponent();

            lblToolName.Text = Utils.getString("NewToolDlg.lblTitle");
            lblTooltip.Text = Utils.getString("NewToolDlg.lblTooltip");
            lblToolIcon.Text = Utils.getString("NewToolDlg.lblToolIcon");
            lblChangeIcon.Text = Utils.getString("NewToolDlg.lblChangeIcon");
            chChangeInDataBase.Text = Utils.getString("EditToolDlg.chChangeInDataBase");
            btnCancel.Text = Utils.getString("button.cancel");

            this.Paint += This_Paint; // paint the border
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void pIcon_Click(object sender, EventArgs e)
        {
            using (SelectIconDlg dlg = new SelectIconDlg("EditTool"))
            {
                if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                    return;
                else
                {
                    pIcon.Tag = dlg.iconPath;
                    pIcon.Image = Image.FromFile(dlg.iconPath);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "") return;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
