using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class CloseMapsDlg : Form
    {
        public CloseMapsDlg()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Document active = MMUtils.ActiveDocument;

            foreach (Document doc in MMUtils.MindManager.AllDocuments)
            {
                if (doc.IsModified)
                {
                    if (rbtnAskMe.Checked)
                    {
                        DialogResult dr = MessageBox.Show(
                            String.Format(Utils.getString("tools.closenotsaved"), doc.CentralTopic.Text), "MindManager",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (dr != DialogResult.Yes) continue;
                    }
                    doc.Save();
                }
            }

            foreach (Document doc in MMUtils.MindManager.AllDocuments)
            {
                if (doc == active && chActiveMap.Checked) continue;
                doc.Close();
            }
        }
    }
}
