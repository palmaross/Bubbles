using System;
using System.IO;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class AddSourceDlg : Form
    {
        public AddSourceDlg()
        {
            InitializeComponent();

            Text = Utils.getString("mysources.contextmenu.new");
            lblSpecifyPath.Text = Utils.getString("SelectIconDlg.lblSpecifyPath");
            toolTip1.SetToolTip(btnBrowse, Utils.getString("button.browse"));
            lblTitle.Text = Utils.getString("SelectIconDlg.lblTitle");
            lblInsertSource.Text = Utils.getString("SelectIconDlg.lblInsertSource");
            rbtnLeft.Text = Utils.getString("SelectIconDlg.rbtnLeft");
            rbtnRight.Text = Utils.getString("SelectIconDlg.rbtnRight");
            rbtnEnd.Text = Utils.getString("SelectIconDlg.rbtnEnd");
            rbtnBegin.Text = Utils.getString("SelectIconDlg.rbtnBegin");
            btnCancel.Text = Utils.getString("button.cancel");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Title = Utils.getString("commonoptions.path.settings.browse.dialog_desc");
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

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPath.Text.ToLower().StartsWith("http"))
                {
                    Uri myUri = new Uri(txtPath.Text);
                    txtTitle.Text = myUri.Host;
                }
                else
                    txtTitle.Text = Path.GetFileName(txtPath.Text);
            } 
            catch { }
        }
    }
}
