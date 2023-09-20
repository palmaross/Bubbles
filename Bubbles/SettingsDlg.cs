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
    public partial class SettingsDlg : Form
    {
        public SettingsDlg()
        {
            InitializeComponent();

            StartIcons.Checked = Utils.getRegistry("StartIcons", "0") == "1";
            StartPaste.Checked = Utils.getRegistry("StartPaste", "0") == "1";
            startSnippets.Checked = Utils.getRegistry("StartSnippets", "0") == "1";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool start = Utils.getRegistry("StartIcons", "0") == "1";
            if (StartIcons.Checked ^ start)
                Utils.setRegistry("StartIcons", StartIcons.Checked ? "1" : "0");

            start = Utils.getRegistry("StartPaste", "0") == "1";
            if (StartPaste.Checked ^ start)
                Utils.setRegistry("StartPaste", StartPaste.Checked ? "1" : "0");

            start = Utils.getRegistry("StartSnippets", "0") == "1";
            if (startSnippets.Checked ^ start)
                Utils.setRegistry("StartSnippets", startSnippets.Checked ? "1" : "0");
        }
    }
}
