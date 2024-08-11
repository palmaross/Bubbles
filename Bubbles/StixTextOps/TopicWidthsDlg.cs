using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class TopicWidthsDlg : Form
    {
        public TopicWidthsDlg(Form _form)
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TextOpsStix.htm#managewidths.htm");

            Text = Utils.getString("TopicWidthDlg.Title");
            lblMainWidth.Text = Utils.getString("TopicWidthDlg.lblMainWidth");
            lblOtherWidths.Text = Utils.getString("TopicWidthDlg.lblOtherWidths");

            btnClose.Text = Utils.getString("button.close");
            btnSave.Text = Utils.getString("button.save");

            this.HelpButtonClicked += this_HelpButtonClicked;
            form = _form;

            numMainWidth.Value = mainwidth; int i = 0;
            foreach (NumericUpDown num in this.Controls.OfType<NumericUpDown>())
            {
                if (num.Name == "numMainWidth") continue;
                num.Value = widths[i++];
            }
            i = 0;
            foreach (CheckBox ch in this.Controls.OfType<CheckBox>())
                ch.Checked = checkstate[i++];
        }

        private void this_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "TextOpsStix.htm#managewidths.htm");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stixwidths.Clear();
            using (StixDB db = new StixDB())
            {
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numMainWidth.Value +
                    " where name=`" + numMainWidth.Name + "`");
                mainwidth = (int)numMainWidth.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth1.Value + ", " +
                    "_checked=" + (cbm1.Checked ? 1 : 0) +
                    " where name=`" + numWidth1.Name + "`");
                widths[0] = (int)numWidth1.Value; checkstate[0] = cbm1.Checked;
                if (cbm1.Checked && !stixwidths.Contains((int)numWidth1.Value)) stixwidths.Add((int)numWidth1.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth2.Value + ", " +
                    "_checked=" + (cbm2.Checked ? 1 : 0) +
                    " where name=`" + numWidth2.Name + "`");
                widths[1] = (int)numWidth2.Value; checkstate[1] = cbm2.Checked;
                if (cbm2.Checked && !stixwidths.Contains((int)numWidth2.Value)) stixwidths.Add((int)numWidth2.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth3.Value + ", " +
                    "_checked=" + (cbm3.Checked ? 1 : 0) +
                    " where name=`" + numWidth3.Name + "`");
                widths[2] = (int)numWidth3.Value; checkstate[2] = cbm3.Checked;
                if (cbm3.Checked && !stixwidths.Contains((int)numWidth3.Value)) stixwidths.Add((int)numWidth3.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth4.Value + ", " +
                    "_checked=" + (cbm4.Checked ? 1 : 0) +
                    " where name=`" + numWidth4.Name + "`");
                widths[3] = (int)numWidth4.Value; checkstate[3] = cbm4.Checked;
                if (cbm4.Checked && !stixwidths.Contains((int)numWidth4.Value)) stixwidths.Add((int)numWidth4.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth5.Value + ", " +
                    "_checked=" + (cbm5.Checked ? 1 : 0) +
                    " where name=`" + numWidth5.Name + "`");
                widths[4] = (int)numWidth5.Value; checkstate[4] = cbm5.Checked;
                if (cbm5.Checked && !stixwidths.Contains((int)numWidth5.Value)) stixwidths.Add((int)numWidth5.Value);
            }

            stixwidths = stixwidths.OrderBy(i => i).ToList();
            (form as StixTextOps).toolTip1.SetToolTip((form as StixTextOps).pTopicWidth, String.Format(Utils.getString("TextOpsStix.pTopicWidth.tooltip"), mainwidth));
            (form as StixTextOps).PopulateTopicWidths();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Form form;

        public static int mainwidth = 64;
        public static List<int> widths = new List<int>() { 100, 150, 200, 250, 200};
        public static List<int> stixwidths = new List<int>();
        public static List<bool> checkstate = new List<bool>() { true, true, true, true, false };
    }
}
