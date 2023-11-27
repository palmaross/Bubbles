using Bubbles23;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class AddTopicTemplateDlg : Form
    {
        public AddTopicTemplateDlg()
        {
            InitializeComponent();

            Text = Utils.getString("TopicTemplateDlg.Title");
            grFromTo.Text = Utils.getString("TopicTemplateDlg.grFromTo");
            lblTopicText.Text = Utils.getString("TopicTemplateDlg.lblTopicText");
            txtTopicText.Text = Utils.getString("TopicTemplateDlg.txtTopicText");
            rbtnTopics.Text = Utils.getString("TopicTemplateDlg.rbtnTopics");
            rbtnUseIncrement.Text = Utils.getString("TopicTemplateDlg.rbtnUseIncrement");
            grIncrement.Text = Utils.getString("TopicTemplateDlg.grIncrement");
            lblStart.Text = Utils.getString("TopicTemplateDlg.lblStart");
            lblStep.Text = Utils.getString("TopicTemplateDlg.lblStep");
            rbtnFinish.Text = Utils.getString("TopicTemplateDlg.rbtnFinish");
            rbtnSteps.Text = Utils.getString("TopicTemplateDlg.rbtnSteps");
            grPosition.Text = Utils.getString("TopicTemplateDlg.grPosition");
            rbtnBegin.Text = Utils.getString("TopicTemplateDlg.rbtnBegin");
            rbtnEnd.Text = Utils.getString("TopicTemplateDlg.rbtnEnd");
            btnPreview.Text = Utils.getString("TopicTemplateDlg.btnPreview");
            btnCancel.Text = Utils.getString("button.cancel");

            ttp.Location = grIncrement.Location;
            this.Controls.Add(ttp);
            ttp.Visible = false;

            rbtnUseIncrement.Location = 
                new Point(txtTopicText.Right - rbtnUseIncrement.Width, rbtnUseIncrement.Location.Y);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (GetTopicList() == null)
                return;

            ttp.listBox1.Items.Clear();
            ttp.listBox1.Items.AddRange(TopicList.ToArray());
            ttp.Visible = true;
            ttp.BringToFront();
        }

        List<string> GetTopicList()
        {
            TopicList.Clear();

            if (rbtnTopics.Checked) return null;

            string name = txtTopicText.Text.Trim();
            if (name == "") return null;

            int start = (int)numStart.Value;
            int finish = (int)numFinish.Value;
            int step = (int)numStep.Value;
            int steps = (int)numSteps.Value;

            bool begin = rbtnBegin.Checked;

            if (rbtnFinish.Checked)
            {
                if (step > 0)
                {
                    for (int i = start; i <= finish; i += step)
                        TopicList.Add(GetPreview(name, i, begin));
                }
                else
                {
                    for (int i = start; i >= finish; i += step)
                        TopicList.Add(GetPreview(name, i, begin));
                }
            }
            else
            {
                if (step > 0)
                {
                    for (int i = start; i <= steps; i += step)
                        TopicList.Add(GetPreview(name, i, begin));
                }
                else
                {
                    for (int i = start; i >= steps; i -= step)
                        TopicList.Add(GetPreview(name, i, begin));
                }
            }

            return TopicList;
        }

        string GetPreview(string topictext, int number, bool begin)
        {
            if (begin) topictext = number + " " + topictext;
            else topictext += " " + number;

            return topictext;
        }

        private void rbtnTopics_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTopics.Checked) { btnPreview.Enabled = false; }
            else { btnPreview.Enabled = true; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rbtnTopics.Checked)
            {
                string name = txtTopicText.Text.Trim();
                if (name == "") return;

                for (int i = 0; i <= numTopics.Value; i++)
                    TopicList.Add(name);
            }
            else
            {
                // Populate TopicList
                if (GetTopicList() != null)
                    DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ttp != null) ttp.Dispose();
            TopicList.Clear();
            this.Close();
        }

        public List<string> TopicList = new List<string>();
        TopicTemplatePreview ttp = new TopicTemplatePreview();
    }
}
