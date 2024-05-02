using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class NewToolDlg : Form
    {
        public NewToolDlg(List<ToolItem> tools, bool manage)
        {
            InitializeComponent();

            Tools = tools;

            Text = Utils.getString("NewToolDlg.contextmenu.new");
            lblSpecifyPath.Text = Utils.getString("NewToolDlg.lblSpecifyPath");
            toolTip1.SetToolTip(btnBrowse, Utils.getString("button.browse"));
            lblTitle.Text = Utils.getString("NewToolDlg.lblTitle");
            lblWait.Text = Utils.getString("LinksDlg.lblWait");
            btnCancel.Text = Utils.getString("button.cancel");

            int x = lblTitle.Location.X + lblTitle.Width + label1.Height;
            txtTitle.Location = new Point(x, txtTitle.Location.Y);
            txtTitle.Width = label1.Width - x;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                txtPath.Text = openFileDialog1.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPath.Text != "")
            {
                // проверить файл на существование?
                DialogResult = DialogResult.OK;
            }
        }

        private List<ToolItem> Tools = new List<ToolItem>();

        private void txtPath_KeyUp(object sender, KeyEventArgs e)
        {
            string path = txtPath.Text.Trim();
            if (path == "") return;
            string title = "";

            if (path.StartsWith("http"))
            {
                lblWait.Visible = true;
                title = Utils.GetWebPageTitle(path);
                lblWait.Visible = false;
            }
            else // file
            {
                try { title = Path.GetFileName(path); }
                catch { }
            }

            if (!String.IsNullOrEmpty(title))
                txtTitle.Text = title;

            this.Refresh();

            e.Handled = true; // to avoid the "ding" sound
            e.SuppressKeyPress = true;
        }

        private void txtPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtPath.SelectAll();
        }
    }
}
