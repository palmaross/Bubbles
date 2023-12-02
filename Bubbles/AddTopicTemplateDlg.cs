using Bubbles23;
using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bubbles
{
    public partial class AddTopicTemplateDlg : Form
    {
        public AddTopicTemplateDlg()
        {
            InitializeComponent();

            Text = Utils.getString("TopicTemplateDlg.Title");
            lblTemplates.Text = Utils.getString("TopicTemplateDlg.lblTemplates");
            linkManageTemplates.Text = Utils.getString("TopicTemplateDlg.linkManageTemplates");
            lblTopicText.Text = Utils.getString("TopicTemplateDlg.lblTopicText");
            grTemplate.Text = Utils.getString("TopicTemplateDlg.grTemplate");
            lblTopics.Text = Utils.getString("TopicTemplateDlg.lblTopics");
            rbtnCustom.Text = Utils.getString("TopicTemplateDlg.rbtnFreeTemplate");
            rbtnUseIncrement.Text = Utils.getString("TopicTemplateDlg.rbtnUseIncrement");
            grIncrement.Text = Utils.getString("TopicTemplateDlg.grIncrement");
            lblStart.Text = Utils.getString("TopicTemplateDlg.lblStart");
            lblStep.Text = Utils.getString("TopicTemplateDlg.lblStep");
            rbtnFinish.Text = Utils.getString("TopicTemplateDlg.rbtnFinish");
            rbtnSteps.Text = Utils.getString("TopicTemplateDlg.rbtnSteps");
            grPosition.Text = Utils.getString("TopicTemplateDlg.grPosition");
            rbtnBegin.Text = Utils.getString("TopicTemplateDlg.rbtnBegin");
            rbtnEnd.Text = Utils.getString("TopicTemplateDlg.rbtnEnd");
            linkSaveTemplate.Text = Utils.getString("button.save");
            linkNewTemplate.Text = Utils.getString("TopicTemplateDlg.linkNewTemplate");
            linkPreview.Text = Utils.getString("TopicTemplateDlg.btnPreview");
            grAdd.Text = Utils.getString("button.add");
            Subtopic.Text = Utils.getString("TopicTemplateDlg.rbtnSubtopic");
            NextTopic.Text = Utils.getString("TopicTemplateDlg.rbtnNextTopic");
            TopicBefore.Text = Utils.getString("TopicTemplateDlg.rbtnTopicBefore");
            btnAddTopics.Text = Utils.getString("TopicTemplateDlg.btnAdd");
            btnCancel.Text = Utils.getString("button.cancel");

            lblRename.Text = Utils.getString("TopicTemplateDlg.lblRename");
            btnDeleteTemplate.Text = Utils.getString("TopicTemplateDlg.btnDeleteTemplate");
            btnClose.Text = Utils.getString("button.close");
            toolTip1.SetToolTip(btnRename, Utils.getString("notes.group.rename"));
            lblTemplateName.Text = Utils.getString("TopicTemplateDlg.lblTemplateName");
            btnCreate.Text = Utils.getString("TopicTemplateDlg.btnCreate");
            btnCancelCreate.Text = Utils.getString("button.cancel");

            ttp.Location = grIncrement.Location; ttpc.Location = grIncrement.Location;
            this.Controls.Add(ttp); this.Controls.Add(ttpc);
            ttp.Visible = false; ttpc.Visible = false;

            imageList1.ImageSize = new Size(p1.Width, p1.Height);
            imageList1.Images.Add(System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddSubtopic.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddTopic.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddBefore20.png"));

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from MT_TEMPLATES order by templateName");

                foreach (DataRow row in dt.Rows)
                {
                    TemplateItem item = new TemplateItem(Convert.ToInt32(row["id"]), 
                        row["templateName"].ToString(), row["topicName"].ToString(), row["pattern"].ToString());

                    cbTemplates.Items.Add(item);
                }
            }

            if (cbTemplates.Items.Count > 0)
                cbTemplates.SelectedIndex = 0;
        }

        private void linkPreview_Click(object sender, EventArgs e)
        {
            if (GetTopicList() == null)
                return;

            ttp.listBox1.Items.Clear();
            ttp.listBox1.Items.AddRange(TopicList.ToArray());
            ttp.btnClose.Visible = true;
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
            int finish = (int)numEnd.Value;
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

        private void Template_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTopics.Checked)
            {
                linkPreview.Enabled = false;
                ttpc.Visible = false;
                ttp.listBox1.Items.Clear();
                ttp.btnClose.Visible = false;
                ttp.Visible = true;
                ttp.BringToFront();

                if (txtTopicText.Text.Trim() == "")
                    txtTopicText.Text = Utils.getString("Template.Preview.Topic");

                for (int i = 0; i < numTopics.Value; i++)
                    ttp.listBox1.Items.Add(txtTopicText.Text);
            }
            else if (rbtnCustom.Checked)
            {
                linkPreview.Enabled = false;
                ttp.Visible = false;
                ttpc.Visible = true;
                ttpc.BringToFront();
                if (ttpc.textBox1.Text.Trim() == "")
                    ttpc.textBox1.Text = Utils.getString("Template.Preview.Custom");
            }
            else if (rbtnUseIncrement.Checked)
            {
                linkPreview.Enabled = true;
                ttp.Visible = false;
                ttpc.Visible = false;

                if (txtTopicText.Text.Trim() == "")
                    txtTopicText.Text = Utils.getString("Template.Preview.Topic");
            }
        }

        private void btnAddTopics_Click(object sender, EventArgs e)
        {
            TopicList.Clear();

            if (rbtnTopics.Checked)
            {
                string name = txtTopicText.Text.Trim();
                if (name == "") return;

                for (int i = 0; i < numTopics.Value; i++) TopicList.Add(name);
            }
            else if (rbtnCustom.Checked)
            {
                string[] lines = ttpc.textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines) TopicList.Add(line);
            }
            else if (rbtnUseIncrement.Checked)
            {
                GetTopicList();
            }

            string topictype = "Subtopic";
            if (NextTopic.Checked) topictype = "NextTopic";
            if (TopicBefore.Checked) topictype = "TopicBefore";

            if (TopicList != null && TopicList.Count > 0)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    transTopic = t; transTopicType = topictype;
                    AddTopicTransaction(Utils.getString("addtopics.transactionname.insert"));
                }
            }

            DialogResult = DialogResult.OK;
        }
        Topic transTopic;
        string transTopicType;

        void AddTopicTransaction(string trname)
        {
            Transaction _tr = MMUtils.ActiveDocument.NewTransaction(trname);
            _tr.IsUndoable = true;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(AddTopics);
            _tr.Start();
        }

        public void AddTopics(Document pDocument)
        {
            if (TopicList.Count > 1 && transTopicType == "NextTopic")
                TopicList = TopicList.Reverse<string>().ToList();

            foreach (var name in TopicList)
                StickUtils.AddTopic(transTopic, transTopicType, name);
        }

        private void linkManageTemplates_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cbTemplates.Items.Count == 0) return;

            panelManageTemplates.Visible = true;
            panelManageTemplates.BringToFront();

            var item = cbTemplates.SelectedItem as TemplateItem;
            txtRename.Text = item.TemplateName;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            string newName = txtRename.Text.Trim();
            var item = cbTemplates.SelectedItem as TemplateItem;

            if (newName == "" || newName == item.TemplateName) return;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from MT_TEMPLATES where templateName=`" + newName + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("TopicTemplateDlg.nameexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                db.ExecuteNonQuery("update MT_TEMPLATES set templateName=`" + newName + "` where id=" + item.ID + "");

                item.TemplateName = newName;
                cbTemplates.Items.Remove(cbTemplates.SelectedItem);
                cbTemplates.Items.Add(item);
                cbTemplates.SelectedItem = item;
            }

            panelManageTemplates.Visible = false;
        }

        private void btnDeleteTemplate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Utils.getString("TopicTemplateDlg.deletetemplate"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            var item = cbTemplates.SelectedItem as TemplateItem;

            using (BubblesDB db = new BubblesDB())
                db.ExecuteNonQuery("delete from MT_TEMPLATES where id=" + item.ID + "");

            cbTemplates.Items.Remove(item);
            if (cbTemplates.Items.Count > 0) cbTemplates.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panelManageTemplates.Visible = false;
        }

        private void linkNewTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelNewTemplate.Visible = true;
            panelNewTemplate.BringToFront();
            linkNewTemplate.SendToBack();
        }

        private void linkSaveTemplate_Click(object sender, EventArgs e)
        {
            btnCreate_Click(linkSaveTemplate, null);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bool save = sender == linkSaveTemplate;

            string newName = txtTemplateName.Text.Trim();
            if (save) newName = cbTemplates.Text;

            string topicName = txtTopicText.Text.Trim();
            if (newName == "") return;

            string pattern_data = "";

            if (rbtnTopics.Checked)
            {
                if (topicName == "") return;
                pattern_data = "topics###" + numTopics.Value.ToString();
            }
            else if (rbtnCustom.Checked)
            {
                pattern_data = "custom###";

                string[] lines = ttpc.textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (string line in lines)
                    pattern_data += line + "###";

                pattern_data = pattern_data.TrimEnd('#');
            }
            else if (rbtnUseIncrement.Checked)
            {
                string position = rbtnBegin.Checked ? "begin" : "end";

                pattern_data = "increment###" + numStart.Value + "," + numStep.Value +"," + 
                    numEnd.Value + "," + numTopics.Value + "," + position;
            }

            using (BubblesDB db = new BubblesDB())
            {
                if (save)
                {
                    var template = cbTemplates.SelectedItem as TemplateItem;

                    db.ExecuteNonQuery("update MT_TEMPLATES set " +
                        "topicName=`" + topicName + "`, " +
                        "pattern =`" + pattern_data + "`, " +
                        "where id=" + template.ID + "");

                    template.TopicName = topicName;
                    template.Pattern = pattern_data;
                    cbTemplates.SelectedItem = template;
                }
                else
                {
                    DataTable dt = db.ExecuteQuery("select * from MT_TEMPLATES where templateName=`" + newName + "`");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("TopicTemplateDlg.nameexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    db.AddPattern(newName, topicName, pattern_data);

                    // Get auto-created ID
                    int id = 0;
                    dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                    var item = new TemplateItem(id, newName, topicName, pattern_data);

                    cbTemplates.Items.Add(item);
                    cbTemplates.SelectedItem = item;
                }
            }
            panelNewTemplate.Visible = false;
        }

        private void btnCancelCreate_Click(object sender, EventArgs e)
        {
            linkNewTemplate.BringToFront();
            panelNewTemplate.Visible = false;
        }

        private void cbTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateItem item = cbTemplates.SelectedItem as TemplateItem;

            txtTopicText.Text = item.TopicName;

            List<string> pattern = item.Pattern.Split(new string[] { "###" }, StringSplitOptions.None).ToList();

            switch (pattern[0])
            {
                case "topics":
                    linkPreview.Enabled = false;
                    ttpc.Visible = false;
                    ttp.listBox1.Items.Clear();
                    ttp.btnClose.Visible = false;
                    ttp.Visible = true;
                    ttp.BringToFront();

                    rbtnTopics.Checked = true;
                    numTopics.Value = Convert.ToInt32(pattern[1]);

                    for (int i = 0; i < numTopics.Value; i++)
                        ttp.listBox1.Items.Add(item.TopicName);

                    break;
                case "custom":
                    linkPreview.Enabled = false;
                    ttp.Visible = false;
                    ttpc.Visible = true;
                    ttpc.BringToFront();

                    rbtnCustom.Checked = true;
                    pattern.RemoveAt(0);

                    string lines = "";
                    foreach (string line in pattern)
                        lines += line + "\r\n";

                    ttpc.textBox1.Text = lines.TrimEnd('\n', '\r');
                    break;
                case "increment":
                    linkPreview.Enabled = true;
                    ttp.Visible = false;
                    ttpc.Visible = false;

                    rbtnUseIncrement.Checked = true;
                    string[] incs = pattern[1].Split(',');
                    numStart.Value = Convert.ToInt32(incs[0]);
                    numStep.Value = Convert.ToInt32(incs[1]);
                    numEnd.Value = Convert.ToInt32(incs[2]);
                    numTopics.Value = Convert.ToInt32(incs[3]);
                    rbtnBegin.Checked = incs[4] == "begin";
                    break;
            }

            txtRename.Text = item.TemplateName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ttp != null) ttp.Dispose();
            TopicList.Clear();
            this.Close();
        }

        public List<string> TopicList = new List<string>();
        TopicTemplatePreview ttp = new TopicTemplatePreview();
        TopicTemplatePreviewCustom ttpc = new TopicTemplatePreviewCustom();

        private void lblTopics_Click(object sender, EventArgs e)
        {
            if (!rbtnTopics.Checked) rbtnTopics.Checked = true;
        }
    }

    public class TemplateItem
    {
        public TemplateItem(int id, string templateName, string topicName, string pattern)
        {
            ID = id;
            TemplateName = templateName;
            TopicName = topicName;
            Pattern = pattern;
        }
        public int ID = 0;
        public string TemplateName = "";
        public string TopicName = "";
        public string Pattern = "";

        public override string ToString()
        {
            return TemplateName;
        }
    }
}
