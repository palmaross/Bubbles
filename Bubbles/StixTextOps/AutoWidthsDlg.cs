using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class AutoWidthsDlg : Form
    {
        public AutoWidthsDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "manage_topic_widths.htm");

            Text = Utils.getString("TopicWidthDlg.Title");
            lblTextMore.Text = Utils.getString("TopicWidthDlg.lblTextMore");
            lblTopicWidth.Text = Utils.getString("TopicWidthDlg.lblTopicWidth");

            foreach (Label lbl in this.Controls.OfType<Label>())
            {
                if (lbl.Name.StartsWith("lblChars"))
                    lbl.Text = Utils.getString("TopicWidthDlg.lblChars");
                else if (lbl.Name.StartsWith("mm"))
                    lbl.Text = Utils.getString("TopicWidthDlg.mm");
            }

            btnClose.Text = Utils.getString("button.close");
            btnSave.Text = Utils.getString("button.save");

            this.HelpButtonClicked += this_HelpButtonClicked;

            int i = 0;
            foreach (NumericUpDown num in this.Controls.OfType<NumericUpDown>())
            {
                if (num.Name.StartsWith("numChar"))
                    num.Value = chars[i];
                else
                    num.Value = lengths[i++];

                if (Utils.scalingFactor == 1) { num.Width += 4; }
            }
            i = 0;
            foreach (CheckBox ch in this.Controls.OfType<CheckBox>())
                ch.Checked = checkstate[i++];
        }

        private void this_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "manage_topic_widths.htm");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<int> mwidths = new List<int>();
            Dictionary<int, int> awidths = new Dictionary<int, int>();

            using (StixDB db = new StixDB())
            {
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars1.Value + ", " +
                    "_value=" + numAuto1.Value + ", " +
                    "_checked=" + (cbTextMore1.Checked ? 1 : 0) +
                    " where name=`" + numAuto1.Name + "`");
                chars[0] = (int)numChars1.Value; lengths[0] = (int)numAuto1.Value; checkstate[0] = cbTextMore1.Checked;
                if (cbTextMore1.Checked && !awidths.Keys.Contains((int)numChars1.Value)) awidths[(int)numChars1.Value] = (int)numAuto1.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars2.Value + ", " +
                    "_value=" + numAuto2.Value + ", " +
                    "_checked=" + (cbTextMore2.Checked ? 1 : 0) +
                    " where name=`" + numAuto2.Name + "`");
                chars[1] = (int)numChars2.Value; lengths[1] = (int)numAuto2.Value; checkstate[1] = cbTextMore1.Checked;
                if (cbTextMore2.Checked && !awidths.Keys.Contains((int)numChars2.Value)) awidths[(int)numChars2.Value] = (int)numAuto2.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars3.Value + ", " +
                    "_value=" + numAuto3.Value + ", " +
                    "_checked=" + (cbTextMore3.Checked ? 1 : 0) +
                    " where name=`" + numAuto3.Name + "`");
                chars[2] = (int)numChars3.Value; lengths[2] = (int)numAuto3.Value; checkstate[2] = cbTextMore1.Checked;
                if (cbTextMore3.Checked && !awidths.Keys.Contains((int)numChars3.Value)) awidths[(int)numChars3.Value] = (int)numAuto3.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars4.Value + ", " +
                    "_value=" + numAuto4.Value + ", " +
                    "_checked=" + (cbTextMore4.Checked ? 1 : 0) +
                    " where name=`" + numAuto4.Name + "`");
                chars[3] = (int)numChars4.Value; lengths[3] = (int)numAuto4.Value; checkstate[3] = cbTextMore1.Checked;
                if (cbTextMore4.Checked && !awidths.Keys.Contains((int)numChars4.Value)) awidths[(int)numChars4.Value] = (int)numAuto4.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars5.Value + ", " +
                    "_value=" + numAuto5.Value + ", " +
                    "_checked=" + (cbTextMore5.Checked ? 1 : 0) +
                    " where name=`" + numAuto5.Name + "`");
                chars[4] = (int)numChars5.Value; lengths[4] = (int)numAuto5.Value; checkstate[4] = cbTextMore1.Checked;
                if (cbTextMore5.Checked && !awidths.Keys.Contains((int)numChars5.Value)) awidths[(int)numChars5.Value] = (int)numAuto5.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars6.Value + ", " +
                    "_value=" + numAuto6.Value + ", " +
                    "_checked=" + (cbTextMore6.Checked ? 1 : 0) +
                    " where name=`" + numAuto6.Name + "`");
                chars[5] = (int)numChars6.Value; lengths[5] = (int)numAuto6.Value; checkstate[5] = cbTextMore1.Checked;
                if (cbTextMore6.Checked && !awidths.Keys.Contains((int)numChars6.Value)) awidths[(int)numChars6.Value] = (int)numAuto6.Value;
            }

            StixUtils.AutoTopicWidths = awidths.OrderByDescending(key => key.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            StixUtils.MinAutoTopicWidth = StixUtils.AutoTopicWidths.Keys.Last();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static List<int> chars = new List<int>() { 300, 200, 150, 300, 300, 300 };
        public static List<int> lengths = new List<int>() { 200, 160, 120, 200, 200, 200 };
        public static List<bool> checkstate = new List<bool>() { true, true, true, false, false, false };
    }
}