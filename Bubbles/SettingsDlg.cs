using System;
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
            startPriPro.Checked = Utils.getRegistry("StartPriPro", "0") == "1";
            startBookmarks.Checked = Utils.getRegistry("StartBookmarks", "0") == "1";
            startMySources.Checked = Utils.getRegistry("StartMySources", "0") == "1";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool start = Utils.getRegistry("StartIcons", "0") == "1";
            if (StartIcons.Checked ^ start)
                Utils.setRegistry("StartIcons", StartIcons.Checked ? "1" : "0");

            start = Utils.getRegistry("StartPaste", "0") == "1";
            if (StartPaste.Checked ^ start)
                Utils.setRegistry("StartPaste", StartPaste.Checked ? "1" : "0");

            start = Utils.getRegistry("StartBookmarks", "0") == "1";
            if (startBookmarks.Checked ^ start)
                Utils.setRegistry("StartBookmarks", startBookmarks.Checked ? "1" : "0");

            start = Utils.getRegistry("StartPriPro", "0") == "1";
            if (startPriPro.Checked ^ start)
                Utils.setRegistry("StartPriPro", startPriPro.Checked ? "1" : "0");

            start = Utils.getRegistry("StartMySources", "0") == "1";
            if (startMySources.Checked ^ start)
                Utils.setRegistry("StartMySources", startMySources.Checked ? "1" : "0");
        }
    }
}
