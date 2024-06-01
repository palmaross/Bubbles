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
    public partial class EditToolDlg : Form
    {
        public EditToolDlg()
        {
            InitializeComponent();
        }

        private void pIcon_Click(object sender, EventArgs e)
        {
            using (SelectIconDlg dlg = new SelectIconDlg("EditTool"))
            {
                if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                    return;
            }
        }
    }
}
