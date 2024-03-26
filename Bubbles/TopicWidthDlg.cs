using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class TopicWidthDlg : Form
    {
        public TopicWidthDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
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
            cbTextMore4.Text = Utils.getString("TopicWidthDlg.lblTextMore");
            cbTextMore5.Text = Utils.getString("TopicWidthDlg.lblTextMore");
            cbTextMore6.Text = Utils.getString("TopicWidthDlg.lblTextMore");
            lblChars1.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblChars2.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblChars3.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblChars4.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblChars5.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblChars6.Text = Utils.getString("TopicWidthDlg.lblChars");
            lblMoreAuto.Text = Utils.getString("TopicWidthDlg.lblMoreAuto");
            btnCancel.Text = Utils.getString("button.close");
            btnOK.Text = Utils.getString("button.save");

            this.HelpButtonClicked += this_HelpButtonClicked;
        }

        private void this_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "PasteStick.htm#TopicWidths");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<int> mwidths = new List<int>();
            Dictionary<int, int> awidths = new Dictionary<int, int>();

            using (StixDB db = new StixDB())
            {
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numMainWidth.Value +
                    " where name=`" + numMainWidth.Name + "`");
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth1.Value + ", " +
                    "_checked=" + (cbm1.Checked ? 1 : 0) +
                    " where name=`" + numWidth1.Name + "`");
                if (cbm1.Checked && !mwidths.Contains((int)numWidth1.Value)) mwidths.Add((int)numWidth1.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth2.Value + ", " +
                    "_checked=" + (cbm2.Checked ? 1 : 0) +
                    " where name=`" + numWidth2.Name + "`");
                if (cbm2.Checked && !mwidths.Contains((int)numWidth2.Value)) mwidths.Add((int)numWidth2.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth3.Value + ", " +
                    "_checked=" + (cbm3.Checked ? 1 : 0) +
                    " where name=`" + numWidth3.Name + "`");
                if (cbm3.Checked && !mwidths.Contains((int)numWidth3.Value)) mwidths.Add((int)numWidth3.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth4.Value + ", " +
                    "_checked=" + (cbm4.Checked ? 1 : 0) +
                    " where name=`" + numWidth4.Name + "`");
                if (cbm4.Checked && !mwidths.Contains((int)numWidth4.Value)) mwidths.Add((int)numWidth4.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth5.Value + ", " +
                    "_checked=" + (cbm5.Checked ? 1 : 0) +
                    " where name=`" + numWidth5.Name + "`");
                if (cbm5.Checked && !mwidths.Contains((int)numWidth5.Value)) mwidths.Add((int)numWidth5.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "_value=" + numWidth6.Value + ", " +
                    "_checked=" + (cbm6.Checked ? 1 : 0) +
                    " where name=`" + numWidth6.Name + "`");
                if (cbm6.Checked && !mwidths.Contains((int)numWidth6.Value)) mwidths.Add((int)numWidth6.Value);
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars1.Value + ", " +
                    "_value=" + numAuto1.Value + ", " +
                    "_checked=" + (cbTextMore1.Checked ? 1 : 0) +
                    " where name=`" + numAuto1.Name + "`");
                if (cbTextMore1.Checked && !awidths.Keys.Contains((int)numChars1.Value)) awidths[(int)numChars1.Value] = (int)numAuto1.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars2.Value + ", " +
                    "_value=" + numAuto2.Value + ", " +
                    "_checked=" + (cbTextMore2.Checked ? 1 : 0) +
                    " where name=`" + numAuto2.Name + "`");
                if (cbTextMore2.Checked && !awidths.Keys.Contains((int)numChars2.Value)) awidths[(int)numChars2.Value] = (int)numAuto2.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars3.Value + ", " +
                    "_value=" + numAuto3.Value + ", " +
                    "_checked=" + (cbTextMore3.Checked ? 1 : 0) +
                    " where name=`" + numAuto3.Name + "`");
                if (cbTextMore3.Checked && !awidths.Keys.Contains((int)numChars3.Value)) awidths[(int)numChars3.Value] = (int)numAuto3.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars4.Value + ", " +
                    "_value=" + numAuto4.Value + ", " +
                    "_checked=" + (cbTextMore4.Checked ? 1 : 0) +
                    " where name=`" + numAuto4.Name + "`");
                if (cbTextMore4.Checked && !awidths.Keys.Contains((int)numChars4.Value)) awidths[(int)numChars4.Value] = (int)numAuto4.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars5.Value + ", " +
                    "_value=" + numAuto5.Value + ", " +
                    "_checked=" + (cbTextMore5.Checked ? 1 : 0) +
                    " where name=`" + numAuto5.Name + "`");
                if (cbTextMore5.Checked && !awidths.Keys.Contains((int)numChars5.Value)) awidths[(int)numChars5.Value] = (int)numAuto5.Value;
                db.ExecuteNonQuery("update TOPICWIDTHS set " +
                    "chars=" + numChars6.Value + ", " +
                    "_value=" + numAuto6.Value + ", " +
                    "_checked=" + (cbTextMore6.Checked ? 1 : 0) +
                    " where name=`" + numAuto6.Name + "`");
                if (cbTextMore6.Checked && !awidths.Keys.Contains((int)numChars6.Value)) awidths[(int)numChars6.Value] = (int)numAuto6.Value;
            }

            StickUtils.MainTopicWidth = (int)numMainWidth.Value;
            StickUtils.ManualTopicWidths = mwidths.OrderBy(i => i).ToList();
            StickUtils.AutoTopicWidths = awidths.OrderByDescending(key => key.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            StickUtils.MinAutoTopicWidth = StickUtils.AutoTopicWidths.Keys.Last();

            (form as BubbleTextOps).PopulateTopicWidth();
        }

        private void lblMoreAuto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MoreAutoWidths.Visible)
            {
                MoreAutoWidths.Visible = false;
                lblMoreAuto.Text = Utils.getString("TopicWidthDlg.lblMoreAuto");
            }
            else
            {
                MoreAutoWidths.Visible = true;
                lblMoreAuto.Text = Utils.getString("TopicWidthDlg.lblMoreAuto2");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void TopicWidthDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public Form form;
    }
}