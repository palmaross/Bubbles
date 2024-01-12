using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class NewSourceDlg : Form
    {
        public NewSourceDlg(List<MySourcesItem> sources, bool manage)
        {
            InitializeComponent();

            Sources = sources;

            Text = Utils.getString("mysources.contextmenu.new");
            lblSpecifyPath.Text = Utils.getString("NewSourceDlg.lblSpecifyPath");
            toolTip1.SetToolTip(btnBrowse, Utils.getString("button.browse"));
            lblTitle.Text = Utils.getString("NewSourceDlg.lblTitle");
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

        private List<MySourcesItem> Sources = new List<MySourcesItem>();
    }
}
