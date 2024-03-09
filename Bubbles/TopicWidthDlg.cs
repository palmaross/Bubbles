using Bubbles;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sticks
{
    public partial class TopicWidthDlg : Form
    {
        public TopicWidthDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "PasteStick.htm#TopicWidths");

            Text = Utils.getString("TopicWidthDlg.Title");
            lblManual.Text = Utils.getString("TopicWidthDlg.lblManual");
            lblMainWidth.Text = Utils.getString("TopicWidthDlg.lblMainWidth");
            lblOtherWidths.Text = Utils.getString("TopicWidthDlg.lblOtherWidths");
            lblAutomatic.Text = Utils.getString("TopicWidthDlg.lblAutomatic");
            cbTextMore1.Text = Utils.getString("TopicWidthDlg.lblTextMore");
            cbTextMore2.Text = Utils.getString("TopicWidthDlg.lblTextMore");
            cbTextMore3.Text = Utils.getString("TopicWidthDlg.lblTextMore");
            lblChars1.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblChars2.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblChars3.Text = Utils.getString("TopicWidthDlg.lblChars");
            chbMindManager.Text = Utils.getString("TopicWidthDlg.chbMindManager");
            btnCancel.Text = Utils.getString("button.cancel");
            btnOK.Text = Utils.getString("button.save");

            string widths = Utils.getRegistry("TopicWidths", "63:100:130:160;300-200:200-160:150-120;1");
            string[] Widths = widths.Split(';');

            string[] ManualWidths = Widths[0].Split(':');
            numMainWidth.Value = Convert.ToInt32(ManualWidths[0]);
            numWidth1.Value = Convert.ToInt32(ManualWidths[1]);
            numWidth2.Value = Convert.ToInt32(ManualWidths[2]);
            numWidth3.Value = Convert.ToInt32(ManualWidths[3]);

            string[] automatic = Widths[1].Split(':');
            string[] auto1 = automatic[0].Split('-');
            numChars1.Value = Convert.ToInt32(auto1[0]);
            numAuto1.Value = Convert.ToInt32(auto1[1]);
            string[] auto2 = automatic[1].Split('-');
            numChars2.Value = Convert.ToInt32(auto2[0]);
            numAuto2.Value = Convert.ToInt32(auto2[1]);
            string[] auto3 = automatic[2].Split('-');
            numChars3.Value = Convert.ToInt32(auto3[0]);
            numAuto3.Value = Convert.ToInt32(auto3[1]);

            chbMindManager.Checked = Widths[2] == "1";
            this.HelpButtonClicked += this_HelpButtonClicked;
        }

        private void this_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "PasteStick.htm#TopicWidths");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string reg = numMainWidth.Value.ToString() + ":";
            reg += numWidth1.Value.ToString() + ":";
            reg += numWidth2.Value.ToString() + ":";
            reg += numWidth3.Value.ToString() + ";";
            reg += numChars1.Value.ToString() + "-";
            reg += numAuto1.Value.ToString() + ":";
            reg += numChars2.Value.ToString() + "-";
            reg += numAuto2.Value.ToString() + ":";
            reg += numChars3.Value.ToString() + "-";
            reg += numAuto3.Value.ToString() + ";";
            reg += chbMindManager.Checked ? "1" : "0";

            Utils.setRegistry("TopicWidths", reg);
        }
    }
}
