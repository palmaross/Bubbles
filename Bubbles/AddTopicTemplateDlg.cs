using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
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
            lblTemplates.Text = Utils.getString("TopicTemplateDlg.lblTemplates");
            linkManageTemplates.Text = Utils.getString("TopicTemplateDlg.linkManageTemplates");
            lblTopicText.Text = Utils.getString("TopicTemplateDlg.lblTopicText");
            grTemplate.Text = Utils.getString("TopicTemplateDlg.grTemplate");
            lblTopics.Text = Utils.getString("TopicTemplateDlg.lblTopics");
            rbtnCustom.Text = Utils.getString("TopicTemplateDlg.rbtnFreeTemplate");
            rbtnUseIncrement.Text = Utils.getString("TopicTemplateDlg.rbtnUseIncrement");
            linkSaveTemplate.Text = Utils.getString("button.save");
            linkNewTemplate.Text = Utils.getString("TopicTemplateDlg.linkNewTemplate");
            grAdd.Text = Utils.getString("button.add");
            Subtopic.Text = Utils.getString("TopicTemplateDlg.rbtnSubtopic");
            NextTopic.Text = Utils.getString("TopicTemplateDlg.rbtnNextTopic");
            TopicBefore.Text = Utils.getString("TopicTemplateDlg.rbtnTopicBefore");
            btnAddTopics.Text = Utils.getString("TopicTemplateDlg.btnAdd");
            btnCancel.Text = Utils.getString("button.cancel");

            lblRename.Text = Utils.getString("TopicTemplateDlg.lblRename");
            btnDeleteTemplate.Text = Utils.getString("TopicTemplateDlg.btnDeleteTemplate");
            btnClose.Text = Utils.getString("button.close");
            toolTip1.SetToolTip(btnRename, Utils.getString("button.rename"));
            lblTemplateName.Text = Utils.getString("TopicTemplateDlg.lblTemplateName");
            btnCreate.Text = Utils.getString("TopicTemplateDlg.btnCreate");
            btnCancelCreate.Text = Utils.getString("button.cancel");

            ttp.Location = txtCustom.Location;
            this.Controls.Add(ttp);
            ttp.Visible = false;
            ttp.aParentForm = this;
            ttp_panelMore_Width = ttp.panelMore.Width;

            imageList1.ImageSize = new Size(p1.Width, p1.Height);
            imageList1.Images.Add(System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddSubtopic.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddTopic.png"));
            imageList1.Images.Add(System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddBefore.png"));

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

        List<string> GetTopicList()
        {
            TopicList.Clear();

            if (rbtnTopics.Checked) return null;

            string name = txtTopicText.Text.Trim();
            if (name == "") return null;

            int start = (int)ttp.numStart.Value;
            int finish = (int)ttp.numFinish.Value;
            int step = (int)ttp.numStep.Value;

            bool begin = ttp.chBoxNumPosition.Checked;

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

            return TopicList;
        }

        string GetPreview(string topictext, int number, bool begin)
        {
            if (begin) topictext = number + " " + topictext;
            else topictext += " " + number;

            return topictext;
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
                string[] lines = txtCustom.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
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

                string[] lines = txtCustom.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (string line in lines)
                    pattern_data += line + "###";

                pattern_data = pattern_data.TrimEnd('#');
            }
            else if (rbtnUseIncrement.Checked)
            {
                string position = ttp.chBoxNumPosition.Checked ? "begin" : "end";

                pattern_data = "increment###" + ttp.numStart.Value + "," + ttp.numFinish.Value + "," + 
                    ttp.numStep.Value +"," + position;
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
                    txtCustom.Visible = false;
                    ttp.listBox1.Items.Clear();
                    ttp.Visible = true;
                    ttp.BringToFront();
                    ttp.panelIncrement.Visible = false;
                    ttp.panelMore.Visible = false;

                    rbtnTopics.Checked = true;
                    numTopics.Value = Convert.ToInt32(pattern[1]);

                    for (int i = 0; i < numTopics.Value; i++)
                        ttp.listBox1.Items.Add(item.TopicName);

                    break;
                case "custom":
                    ttp.Visible = false;
                    txtCustom.Visible = true;
                    txtCustom.BringToFront();

                    rbtnCustom.Checked = true;
                    pattern.RemoveAt(0);

                    string lines = "";
                    foreach (string line in pattern)
                        lines += line + "\r\n";

                    txtCustom.Text = lines.TrimEnd('\n', '\r');
                    break;
                case "increment":
                    ttp.Visible = true;
                    txtCustom.Visible = false;
                    ttp.panelIncrement.Visible = true;
                    ttp.panelIncrement.Location = ttp.p1.Location;
                    ttp.panelMore.Width = ttp_panelMore_Width;

                    rbtnUseIncrement.Checked = true;
                    string[] incs = pattern[1].Split(',');
                    ttp.numStart.Value = Convert.ToInt32(incs[0]);
                    ttp.numFinish.Value = Convert.ToInt32(incs[1]);
                    ttp.numStep.Value = Convert.ToInt32(incs[2]);
                    ttp.chBoxNumPosition.Checked = incs[3] == "begin";

                    if (GetTopicList() == null)
                        return;

                    if (TopicList.Count > 11)
                    {
                        ttp.panelIncrement.Location = new Point(ttp.p1.Location.X - p1.Width, ttp.p1.Location.Y);
                        ttp.panelMore.Width -= p1.Width;
                    }

                    ttp.listBox1.Items.Clear();
                    ttp.listBox1.Items.AddRange(TopicList.ToArray());
                    ttp.Visible = true;
                    ttp.BringToFront();
                    break;
            }

            txtRename.Text = item.TemplateName;
        }

        public void TemplateType_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTopics.Checked)
            {
                txtCustom.Visible = false;
                ttp.listBox1.Items.Clear();
                ttp.Visible = true;
                ttp.BringToFront();
                ttp.panelIncrement.Visible = false;
                ttp.panelMore.Visible = false;

                if (txtTopicText.Text.Trim() == "")
                    txtTopicText.Text = Utils.getString("Template.Preview.Topic");

                for (int i = 0; i < numTopics.Value; i++)
                    ttp.listBox1.Items.Add(txtTopicText.Text);
            }
            else if (rbtnCustom.Checked)
            {
                ttp.Visible = false;
                txtCustom.Visible = true;
                txtCustom.BringToFront();

                if (txtCustom.Text.Trim() == "")
                    txtCustom.Text = Utils.getString("Template.Preview.Custom");
            }
            else if (rbtnUseIncrement.Checked)
            {
                ttp.Visible = true;
                txtCustom.Visible = false;
                ttp.panelIncrement.Visible = true;
                ttp.panelIncrement.Location = ttp.p1.Location;

                if (txtTopicText.Text.Trim() == "")
                    txtTopicText.Text = Utils.getString("Template.Preview.Topic");

                if (GetTopicList() == null)
                    return;

                if (TopicList.Count > 11)
                    ttp.panelIncrement.Location = new Point(ttp.p1.Location.X - p1.Width, ttp.p1.Location.Y);

                ttp.listBox1.Items.Clear();
                ttp.listBox1.Items.AddRange(TopicList.ToArray());
                ttp.Visible = true;
                ttp.BringToFront();
            }
        }


        private void lblTopics_Click(object sender, EventArgs e)
        {
            if (!rbtnTopics.Checked) rbtnTopics.Checked = true;
        }

        private void txtTopicText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                TemplateType_CheckedChanged(null, null);
        }

        private void txtTopicText_Leave(object sender, EventArgs e)
        {
            if (txtTopicTextTextChanged)
            {
                txtTopicTextTextChanged = false;
                TemplateType_CheckedChanged(null, null);
            }
        }

        private void txtTopicText_TextChanged(object sender, EventArgs e)
        {
            txtTopicTextTextChanged = true;
        }

        private void numTopics_ValueChanged(object sender, EventArgs e)
        {
            TemplateType_CheckedChanged(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ttp != null) ttp.Dispose();
            TopicList.Clear();
            this.Close();
        }

        public List<string> TopicList = new List<string>();
        TopicTemplatePreview ttp = new TopicTemplatePreview();
        bool txtTopicTextTextChanged = false;

        int ttp_panelMore_Width;
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
