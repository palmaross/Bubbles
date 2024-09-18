using PRAManager;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class TaskTemplateDlg : Form
    {
        public TaskTemplateDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "Manage_Quick_Topics.htm");

            Text = Utils.getString("TaskTemplateDlg.title");
            lblGroup.Text = Utils.getString("TaskTemplateDlg.lblGroup");
            lblQTopic.Text = Utils.getString("TaskTemplateDlg.lblQTopic");
            lblTopicText.Text = Utils.getString("TopicTemplateDlg.lblTopicText");
            lblProgress.Text = Utils.getString("TaskTemplateDlg.chProgress");
            lblPriority.Text = Utils.getString("TaskTemplateDlg.chPriority");
            lblStartDate.Text = Utils.getString("TaskTemplateDlg.chStartDate");
            lblDueDate.Text = Utils.getString("TaskTemplateDlg.chDueDate");
            lblDuration.Text = Utils.getString("TaskTemplateDlg.lblDuration");
            lblEffort.Text = Utils.getString("TaskTemplateDlg.lblEffort");
            lblResources.Text = Utils.getString("TaskTemplateDlg.chResources");
            lblIcon.Text = Utils.getString("TaskTemplateDlg.chIcon");
            lblTags.Text = Utils.getString("TaskTemplateDlg.chTags");

            lblChangeValue.Text = Utils.getString("TaskTemplateDlg.lblChangeValue");
            lblChangeIcon.Text = Utils.getString("TaskTemplateDlg.lblChangeIcon");
            lblTagGroup.Text = Utils.getString("TaskTemplateDlg.lblTagGroup");
            lblTag.Text = Utils.getString("TaskTemplateDlg.lblTag");
            btnSave.Text = Utils.getString("button.save");
            btnClose.Text = Utils.getString("button.close");

            btnCancel.Text = Utils.getString("button.cancel");
            lblItemName.Text = Utils.getString("TaskTemplateDlg.lblItemName");

            toolTip1.SetToolTip(numStartDate, Utils.getString("TaskTemplateDlg.numDate.tooltip"));
            toolTip1.SetToolTip(numDueDate, Utils.getString("TaskTemplateDlg.numDate.tooltip"));
            toolTip1.SetToolTip(GNew, Utils.getString("ResourcesDlg.NewGroup"));
            toolTip1.SetToolTip(GEdit, Utils.getString("ResourcesDlg.RenameGroup"));
            toolTip1.SetToolTip(GDelete, Utils.getString("ResourcesDlg.DeleteGroup"));
            toolTip1.SetToolTip(New, Utils.getString("TaskTemplateDlg.New.tooltip"));
            toolTip1.SetToolTip(Edit, Utils.getString("TaskTemplateDlg.Edit.tooltip"));
            toolTip1.SetToolTip(Delete, Utils.getString("TaskTemplateDlg.Delete.tooltip"));
            toolTip1.SetToolTip(pStartPlace, Utils.getString("TaskTemplateDlg.calendar.tooltip"));
            toolTip1.SetToolTip(pDuePlace, Utils.getString("TaskTemplateDlg.calendar.tooltip"));

            string text = Utils.getString("quicktasktemplate.today");
            cbStartDatePeriod.Items.Add(new DateItem(text, "today"));
            cbDueDatePeriod.Items.Add(new DateItem(text, "today"));
            text = Utils.getString("quicktasktemplate.tomorrow");
            cbStartDatePeriod.Items.Add(new DateItem(text, "tomorrow"));
            cbDueDatePeriod.Items.Add(new DateItem(text, "tomorrow"));
            text = Utils.getString("quicktasktemplate.thisweek");
            cbStartDatePeriod.Items.Add(new DateItem(text, "thisweek"));
            cbDueDatePeriod.Items.Add(new DateItem(text, "thisweek"));
            text = Utils.getString("quicktasktemplate.nextweek");
            cbStartDatePeriod.Items.Add(new DateItem(text, "nextweek"));
            cbDueDatePeriod.Items.Add(new DateItem(text, "nextweek"));
            text = Utils.getString("quicktasktemplate.thismonth");
            cbStartDatePeriod.Items.Add(new DateItem(text, "thismonth"));
            cbDueDatePeriod.Items.Add(new DateItem(text, "thismonth"));
            text = Utils.getString("quicktasktemplate.nextmonth");
            cbStartDatePeriod.Items.Add(new DateItem(text, "nextmonth"));
            cbDueDatePeriod.Items.Add(new DateItem(text, "nextmonth"));

            cbStartDatePeriod.SelectedIndex = 0; cbDueDatePeriod.SelectedIndex = 0;

            cbDurationUnits.Items.Add(Utils.getString("task.durationunits.minute"));
            cbDurationUnits.Items.Add(Utils.getString("task.durationunits.hour"));
            cbDurationUnits.Items.Add(Utils.getString("task.durationunits.day"));
            cbDurationUnits.Items.Add(Utils.getString("task.durationunits.week"));
            cbDurationUnits.Items.Add(Utils.getString("task.durationunits.month"));
            cbDurationUnits.SelectedIndex = 2;

            cbEffortUnits.Items.Add(Utils.getString("task.durationunits.minute"));
            cbEffortUnits.Items.Add(Utils.getString("task.durationunits.hour"));
            cbEffortUnits.Items.Add(Utils.getString("task.durationunits.day"));
            cbEffortUnits.Items.Add(Utils.getString("task.durationunits.week"));
            cbEffortUnits.Items.Add(Utils.getString("task.durationunits.month"));
            cbEffortUnits.SelectedIndex = 1;

            db = new StixDB("QuickTopics");

            foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
            {
                if (pb.Name.StartsWith("mut") || pb.Name.StartsWith("ch4"))
                    toolTip1.SetToolTip(pb, Utils.getString("quicktask.unchecked"));
            }

            DataTable dt = db.ExecuteQuery("select * from QUICKTOPICGROUPS order by _order");
            foreach (DataRow row in dt.Rows)
            {
                QuickTopicGroup item = new QuickTopicGroup(Convert.ToInt32(row["id"]), row["name"].ToString(), Convert.ToInt32(row["_order"]));
                cbGroups.Items.Add(item);
            }

            if (cbGroups.Items.Count > 0)
                cbGroups.SelectedIndex = 0;

            using (StixDB _db = new StixDB("Resources"))
            {
                dt = _db.ExecuteQuery("select * from RESOURCES order by name");
                foreach (DataRow row in dt.Rows)
                    cbResources.Items.Add(row["name"]);
            }

            if (cbResources.Items.Count > 0)
                cbResources.SelectedIndex = 0;

            dtpStartDate.Location = cbStartDatePeriod.Location;
            dtpDueDate.Location = cbDueDatePeriod.Location;

            this.HelpButtonClicked += this_HelpButtonClicked;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "Manage_Quick_Topics.htm");
        }

        // New Quick Topic
        private void New_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = true;
            panelTemplateName.BringToFront();
            panelTemplateName.Tag = "newtopic";
            txtTemplateName.Text = "";
            lblItemName.Text = Utils.getString("TaskTemplateDlg.lblItemName");
        }

        private void GNew_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = true;
            panelTemplateName.BringToFront();
            panelTemplateName.Tag = "newgroup";
            txtTemplateName.Text = "";
            lblItemName.Text = Utils.getString("TaskTemplateDlg.lblItemName.group");

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = true;
            panelTemplateName.BringToFront();
            panelTemplateName.Tag = "edittopic";
            txtTemplateName.Text = cbQuickTopics.Text;
            lblItemName.Text = Utils.getString("TaskTemplateDlg.lblItemName");
        }

        private void GEdit_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = true;
            panelTemplateName.BringToFront();
            panelTemplateName.Tag = "editgroup";
            txtTemplateName.Text = cbGroups.Text;
            lblItemName.Text = Utils.getString("TaskTemplateDlg.lblItemName.group");
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Utils.getString("TaskTemplateDlg.delete.question"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            QuickTopicItem qtopic = cbQuickTopics.SelectedItem as QuickTopicItem;

            cbQuickTopics.Items.Remove(cbQuickTopics.SelectedItem);

            // Delete template from database
            db.ExecuteNonQuery("delete from QUICKTOPICTEMPLATES where id=" + qtopic.ID + "");

            if (cbQuickTopics.Items.Count > 0)
            {
                cbQuickTopics.SelectedIndex = 0;

                int i = 1;
                foreach (var item in cbQuickTopics.Items)
                {
                    qtopic = item as QuickTopicItem;
                    db.ExecuteNonQuery("update QUICKTOPICTEMPLATES set _order=" + i++ +
                        " where id=" + qtopic.ID + "");
                }
            }
        }

        private void GDelete_Click(object sender, EventArgs e)
        {
            QuickTopicGroup group = cbGroups.SelectedItem as QuickTopicGroup;

            if (group.ID == 1)
            {
                MessageBox.Show(Utils.getString("TaskTemplateDlg.deletegroup.error"), "",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(Utils.getString("TaskTemplateDlg.deletegroup.question"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            cbGroups.Items.Remove(cbQuickTopics.SelectedItem);

            // Delete template from database
            db.ExecuteNonQuery("delete from QUICKTOPICGROUPS where id=" + group.ID + "");

            if (cbGroups.Items.Count > 0)
            {
                cbGroups.SelectedIndex = 0;

                int i = 1;
                foreach (var item in cbGroups.Items)
                {
                    group = item as QuickTopicGroup;
                    db.ExecuteNonQuery("update QUICKTOPICGROUPS set _order=" + i++ + 
                        " where id=" + group.ID + "");
                }
            }            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string newName = txtTemplateName.Text.Trim();
            if (String.IsNullOrEmpty(newName)) return;


            if (panelTemplateName.Tag.ToString().EndsWith("topic"))
            {
                string oldName = cbQuickTopics.Text;
                int groupID = (cbGroups.SelectedItem as QuickTopicGroup).ID;

                DataTable dt = db.ExecuteQuery("select * from QUICKTOPICTEMPLATES " +
                    "where name=`" + newName + "` and groupID=" + groupID + "");

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("TaskTemplateDlg.templateexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((string)panelTemplateName.Tag == "newtopic") // add new quick topic
                {
                    db.AddQuickTopicTemplate(newName, groupID, cbQuickTopics.Items.Count + 1, 
                        "", "", "", "rel:today:1;rel:today:1", "", "", "", "", "");

                    int id = 0;
                    dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                    QuickTopicItem item = new QuickTopicItem(newName, id, groupID, cbQuickTopics.Items.Count + 1, 
                        "", 0, 0, "rel:today:1;rel:today:1", "", "", "", "", "");
                    int i = cbQuickTopics.Items.Add(item);
                    cbQuickTopics.SelectedIndex = i;
                }
                else // rename selected template
                {
                    QuickTopicItem item = cbQuickTopics.SelectedItem as QuickTopicItem;
                    item.Name = newName;

                    // Update template in the database
                    db.ExecuteNonQuery("update QUICKTOPICTEMPLATES set name=`" + newName +
                        "` where id=" + item.ID + "");

                    cbQuickTopics.Items.Remove(cbQuickTopics.SelectedItem);
                    int i = cbQuickTopics.Items.Add(item);
                    cbQuickTopics.SelectedIndex = i;
                }
            }
            else // group
            {
                QuickTopicGroup item = cbGroups.SelectedItem as QuickTopicGroup;

                string oldName = item.Name;
                int groupID = item.ID;

                DataTable dt = db.ExecuteQuery("select * from QUICKTOPICGROUPS " +
                    "where name=`" + newName + "`");

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("TaskTemplateDlg.groupexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((string)panelTemplateName.Tag == "newgroup") // add new quick topic group
                {
                    db.AddQuickTopicGroup(newName, cbGroups.Items.Count + 1);

                    int id = 0;
                    dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                    item = new QuickTopicGroup(id, newName, cbGroups.Items.Count + 1);
                    int i = cbGroups.Items.Add(item);
                    cbGroups.SelectedIndex = i;
                }
                else // rename selected group
                {
                    item.Name = newName;

                    // Update template in the database
                    db.ExecuteNonQuery("update QUICKTOPICGROUPS set name=`" + newName +
                        "` where id=" + item.ID + "");

                    cbQuickTopics.Items.Remove(cbGroups.SelectedItem);
                    int i = cbGroups.Items.Add(item);
                    cbGroups.SelectedIndex = i;
                }
            }

            panelTemplateName.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = false;
        }

        private void cbTaskTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuickTopicItem item = cbQuickTopics.SelectedItem as QuickTopicItem;
            selectedItem = item;

            if (item != null)
            {
                string dates = item.Dates, startdate = "", duedate = "";
                if (dates != "")
                {
                    string[] parts = item.Dates.Split(';');
                    startdate = parts[0]; duedate = parts[1];
                }

                mutTopicText.Image =
                    item.TopicTextState == "" ? pUnchecked.Image :
                    item.TopicTextState == "checked" ? pChecked.Image :
                    item.TopicTextState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                mutTopicText.Tag = item.TopicTextState;

                string tooltip = item.TopicTextState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(mutTopicText, Utils.getString(tooltip));

                mutProgress.Image =
                    item.ProgressState == "" ? pUnchecked.Image :
                    item.ProgressState == "checked" ? pChecked.Image :
                    item.ProgressState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                mutProgress.Tag = item.ProgressState;

                tooltip = item.ProgressState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(mutProgress, Utils.getString(tooltip));

                mutPriority.Image =
                    item.PriorityState == "" ? pUnchecked.Image :
                    item.PriorityState == "checked" ? pChecked.Image :
                    item.PriorityState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                mutPriority.Tag = item.PriorityState;

                tooltip = item.PriorityState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(mutPriority, Utils.getString(tooltip));

                mutStartDate.Image =
                    item.StartDateState == "" ? pUnchecked.Image :
                    item.StartDateState == "checked" ? pChecked.Image :
                    item.StartDateState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                mutStartDate.Tag = item.StartDateState;

                tooltip = item.StartDateState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(mutStartDate, Utils.getString(tooltip));

                mutDueDate.Image =
                    item.DueDateState == "" ? pUnchecked.Image :
                    item.DueDateState == "checked" ? pChecked.Image :
                    item.DueDateState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                mutDueDate.Tag = item.DueDateState;

                tooltip = item.DueDateState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(mutDueDate, Utils.getString(tooltip));

                mutDuration.Image = item.DurationState == "" ? pUnchecked.Image : pChecked.Image;
                mutDuration.Tag = item.DurationState;
                tooltip = item.DurationState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(mutDuration, Utils.getString(tooltip));

                mutEffort.Image = item.EffortState == "" ? pUnchecked.Image : pChecked.Image;
                mutEffort.Tag = item.EffortState;
                tooltip = item.EffortState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(mutEffort, Utils.getString(tooltip));

                ch4Resources.Image =
                    item.ResourcesState == "" ? pUnchecked.Image :
                    item.ResourcesState == "checked" ? pChecked.Image :
                    item.ResourcesState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                ch4Resources.Tag = item.ResourcesState;

                tooltip = item.ResourcesState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(ch4Resources, Utils.getString(tooltip));

                ch4Icon.Image =
                    item.IconState == "" ? pUnchecked.Image :
                    item.IconState == "checked" ? pChecked.Image :
                    item.IconState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                ch4Icon.Tag = item.IconState;

                tooltip = item.IconState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(ch4Icon, Utils.getString(tooltip));

                ch4Tags.Image =
                    item.TagsState == "" ? pUnchecked.Image :
                    item.TagsState == "checked" ? pChecked.Image :
                    item.TagsState == "uncheckedred" ? pUncheckedRed.Image : pCheckedRed.Image;
                ch4Tags.Tag = item.TagsState;

                tooltip = item.TagsState;
                if (tooltip == "") tooltip = "unchecked";
                tooltip = "quicktask." + tooltip;
                toolTip1.SetToolTip(ch4Tags, Utils.getString(tooltip));

                txtTopicText.Text = item.TopicText;

                switch (item.Progress)
                {
                    case 0: pProgress.Image = p0.Image; pProgress.Tag = 0; break;
                    case 25: pProgress.Image = p25.Image; pProgress.Tag = 25; break;
                    case 50: pProgress.Image = p50.Image; pProgress.Tag = 50; break;
                    case 75: pProgress.Image = p75.Image; pProgress.Tag = 75; break;
                    case 100: pProgress.Image = p100.Image; pProgress.Tag = 100; break;
                }

                switch (item.Priority)
                {
                    case 1: pPriority.Image = pPR1.Image; pPriority.Tag = 1; break;
                    case 2: pPriority.Image = pPR2.Image; pPriority.Tag = 2; break;
                    case 3: pPriority.Image = pPR3.Image; pPriority.Tag = 3; break;
                    case 4: pPriority.Image = pPR4.Image; pPriority.Tag = 4; break;
                    case 5: pPriority.Image = pPR5.Image; pPriority.Tag = 5; break;
                }

                if (startdate != "")
                {
                    string[] parts = startdate.Split(':');
                    if (parts[0] == "abs") // absolute date
                    {
                        dtpStartDate.Visible = true; dtpStartDate.BringToFront();
                        DateTime? dt = Utils.GetDate(parts[1]);
                        if (dt != null) dtpStartDate.Value = (DateTime)dt;
                        pStartPlace.Image = pPeriod.Image; pStartPlace.Tag = "period";
                    }
                    else // relative date
                    {
                        dtpStartDate.Visible = false;
                        numStartDate.Enabled = true;
                        SetPeriod(parts[1], cbStartDatePeriod);
                        numStartDate.Value = int.Parse(parts[2]);
                        pStartPlace.Image = pCalendar.Image; pStartPlace.Tag = "calendar";
                    }
                }
                else
                {
                    dtpStartDate.Visible = false;
                    numStartDate.Enabled = true;
                    SetPeriod("today", cbStartDatePeriod);
                    numStartDate.Value = 1;
                    pStartPlace.Image = pCalendar.Image; pStartPlace.Tag = "calendar";
                    toolTip1.SetToolTip(pDuePlace, Utils.getString("TaskTemplateDlg.calendar.tooltip"));
                }
                if (duedate != "")
                {
                    string[] parts = duedate.Split(':');
                    if (parts[0] == "abs") // absolute date
                    {
                        dtpDueDate.Visible = true; dtpDueDate.BringToFront();
                        DateTime? dt = Utils.GetDate(parts[1]);
                        if (dt != null) dtpDueDate.Value = (DateTime)dt;
                        pDuePlace.Image = pPeriod.Image; pDuePlace.Tag = "period";
                    }
                    else // relative date
                    {
                        dtpDueDate.Visible = false;
                        numDueDate.Enabled = true;
                        SetPeriod(parts[1], cbDueDatePeriod);
                        numDueDate.Value = int.Parse(parts[2]);
                        pDuePlace.Image = pCalendar.Image; pDuePlace.Tag = "calendar";
                    }
                }
                else
                {
                    dtpDueDate.Visible = false;
                    numDueDate.Enabled = true;
                    SetPeriod("today", cbDueDatePeriod);
                    numDueDate.Value = 1;
                    pDuePlace.Image = pCalendar.Image; pDuePlace.Tag = "calendar";
                    toolTip1.SetToolTip(pDuePlace, Utils.getString("TaskTemplateDlg.calendar.tooltip"));
                }

                // Duration
                int dValue = 1, dUnits = 2;
                if (item.Duration != "")
                {
                    string[] parts = item.Duration.Split(':');
                    if (parts.Length == 2)
                    {
                        dValue = Convert.ToInt32(parts[0]);
                        dUnits = Convert.ToInt32(parts[1]);
                    }
                }

                numDuration.Value = dValue;
                cbDurationUnits.SelectedIndex = dUnits;

                // Effort
                dValue = 1; dUnits = 2;
                if (item.Effort != "")
                {
                    string[] parts = item.Effort.Split(':');
                    if (parts.Length == 2)
                    {
                        dValue = Convert.ToInt32(parts[0]);
                        dUnits = Convert.ToInt32(parts[1]);
                    }
                }

                numEffort.Value = dValue;
                cbEffortUnits.SelectedIndex = dUnits;

                txtResources.Text = item.Resources;

                if (item.aIcon != "")
                {
                    string[] icons = item.aIcon.Split(';');

                    for (int i = 0; i < icons.Length; i++)
                    {
                        string filename = icons[i];
                        string path = "";

                        if (filename.StartsWith("stock"))
                        {
                            path = MMUtils.MindManager.GetPath(Mindjet.MindManager.Interop.MmDirectory.mmDirectoryIcons);
                            path += filename.Substring(5) + ".ico"; // stockemail -> email.ico
                        }
                        else
                        {
                            if (Utils.CustomIcons.ContainsKey(filename))
                                path = Utils.CustomIcons[filename];
                        }

                        if (File.Exists(path))
                        {
                            if (i == 0)
                            {
                                pIcon.Image = System.Drawing.Image.FromFile(path);
                                pIcon.Tag = filename;
                            }
                            else if (i == 1)
                            {
                                pIcon2.Image = System.Drawing.Image.FromFile(path);
                                pIcon2.Tag = filename;
                            }
                            else if (i == 2)
                            {
                                pIcon3.Image = System.Drawing.Image.FromFile(path);
                                pIcon3.Tag = filename;
                            }
                        }
                    }
                }
                else
                {
                    pIcon.Image = pIconDefault.Image; pIcon.Tag = "stockquestion-mark";
                    pIcon2.Image = pIconDefault.Image; pIcon.Tag = "";
                    pIcon3.Image = pIconDefault.Image; pIcon.Tag = "";
                }
                if (item.Tags != "")
                {
                    string[] tags = item.Tags.Split(';'); // tag1;tag2
                    string[] tag1 = tags[0].Split(':'); // group:tag or tag

                    if (tag1.Length == 1)
                        txtTag1.Text = tag1[0];
                    else {
                        txtTagGroup1.Text = tag1[0]; txtTag1.Text = tag1[1]; }

                    if (tags.Length == 2 && tags[1] != "")
                    {
                        string[] tag2 = tags[1].Split(':');

                        if (tag2.Length == 1)
                            txtTag2.Text = tag2[0];
                        else
                        {
                            txtTagGroup2.Text = tag2[0]; txtTag2.Text = tag2[1];
                        }
                    }
                }
                else
                {
                    txtTagGroup1.Text = ""; txtTag1.Text = "";
                    txtTagGroup2.Text = ""; txtTag2.Text = "";
                }
            }
        }

        void SetPeriod(string period, ComboBox cb)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                DateItem item = cb.Items[i] as DateItem;
                if (item.Period == period)
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            QuickTopicItem item = cbQuickTopics.SelectedItem as QuickTopicItem;
            if (item == null) return;

            item.TopicText = txtTopicText.Text.Trim();
            item.Progress = Convert.ToInt32(pProgress.Tag);
            item.Priority = Convert.ToInt32(pPriority.Tag);
            item.Resources = txtResources.Text.Trim();
            string icons = (string)pIcon.Tag + ";" + (string)pIcon2.Tag + ";" + (string)pIcon3.Tag;
            item.aIcon = icons.Trim(';').Replace(";;", ";");

            item.Duration = numDuration.Value + ":" + cbDurationUnits.SelectedIndex;
            string duration = mutDuration.Tag.ToString() == "" ? "" : "checked:";
            duration += item.Duration;

            item.Effort = numEffort.Value + ":" + cbEffortUnits.SelectedIndex;
            string effort = mutEffort.Tag.ToString() == "" ? "" : "checked:";
            effort += item.Effort;

            string startdate, duedate;

            if ((string)pStartPlace.Tag == "period") // calendar is shown
            {
                startdate = "abs:" + dtpStartDate.Value.ToShortDateString();
            }
            else // period is shown
            {
                DateItem _item = cbStartDatePeriod.SelectedItem as DateItem;
                startdate = "rel:" + _item.Period + ":" + numStartDate.Value.ToString();
            }

            if ((string)pDuePlace.Tag == "period") // calendar is shown
            {
                duedate = "abs:" + dtpDueDate.Value.ToShortDateString();
            }
            else // period is shown
            {
                DateItem _item = cbDueDatePeriod.SelectedItem as DateItem;
                duedate = "rel:" + _item.Period + ":" + numDueDate.Value.ToString();
            }

            item.Dates = startdate + ";" + duedate;

            string tag1 = "", tag2 = "", tags = "";
            if (txtTag1.Text.Trim() != "")
            {
                if (txtTagGroup1.Text.Trim() != "") tag1 = txtTagGroup1.Text.Trim() + ":";
                tag1 += txtTag1.Text.Trim();
            }
            else
            {
                txtTagGroup1.Text = ""; txtTag1.Text = "";
            }

            if (txtTag2.Text.Trim() != "")
            {
                if (txtTagGroup2.Text.Trim() != "") tag2 = txtTagGroup2.Text.Trim() + ":";
                tag2 += txtTag2.Text.Trim();
            }
            else
            {
                txtTagGroup2.Text = ""; txtTag2.Text = "";
            }

            if (tag1 != "") tags = tag1;
            if (tag2 != "")
            {
                if (tags != "") tags += ";" + tag2;
                else tags = tag2;
            }

            item.Tags = tags;

            item.TopicTextState = (string)mutTopicText.Tag;
            item.ProgressState = (string)mutProgress.Tag;
            item.PriorityState = (string)mutPriority.Tag;
            item.StartDateState = (string)mutStartDate.Tag;
            item.DueDateState = (string)mutDueDate.Tag;
            item.DurationState = (string)mutDuration.Tag;
            item.EffortState = (string)mutEffort.Tag;
            item.ResourcesState = (string)ch4Resources.Tag;
            item.IconState = (string)ch4Icon.Tag;
            item.TagsState = (string)ch4Tags.Tag;

            string topictext = item.TopicText; if (item.TopicTextState != "")
                topictext = item.TopicTextState + "$$$" + topictext;
            string progress = item.Progress.ToString(); if (item.ProgressState != "")
                progress = item.ProgressState + ":" + progress;
            string priority = item.Priority.ToString(); if (item.PriorityState != "")
                priority = item.PriorityState + ":" + priority;

            string state = item.StartDateState + ":" + item.DueDateState;
            if (state == ":") state = "";
            string dates = item.Dates.ToString();
            if (state != "") dates = state + "$$$" + dates;

            string resources = item.Resources.ToString(); if (item.ResourcesState != "")
                resources = item.ResourcesState + ":" + resources;
            string _tags = item.Tags.ToString(); if (item.TagsState != "")
                _tags = item.TagsState + ";" + _tags;
            string _icons = item.aIcon.ToString(); if (item.IconState != "")
                _icons = item.IconState + ":" + _icons;

            db.ExecuteNonQuery("update QUICKTOPICTEMPLATES set " +
                "topictext=`" + topictext + "`, " +
                "progress=`" + progress + "`, " +
                "priority=`" + priority + "`, " +
                "dates=`" + dates + "`, " +
                "duration=`" + duration + "`, " +
                "effort=`" + effort + "`, " +
                "resources=`" + resources + "`, " +
                "icons=`" + _icons + "`, " +
                "tags=`" + _tags + "` " +
                "where name=`" + item.Name + "`");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            db.Dispose(); db = null;
            this.Close();
        }

        private void TaskTemplateDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            StixMain.m_TaskInfo.PopulateQuickTopics();
        }

        private void pProgress_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(pProgress.Tag))
            {
                case 0: pProgress.Image = p25.Image; pProgress.Tag = 25; break;
                case 25: pProgress.Image = p50.Image; pProgress.Tag = 50; break;
                case 50: pProgress.Image = p75.Image; pProgress.Tag = 75; break;
                case 75: pProgress.Image = p100.Image; pProgress.Tag = 100; break;
                case 100: pProgress.Image = p0.Image; pProgress.Tag = 0; break;
            }
        }

        private void pPriority_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(pPriority.Tag))
            {
                case 1: pPriority.Image = pPR2.Image; pPriority.Tag = 2; break;
                case 2: pPriority.Image = pPR3.Image; pPriority.Tag = 3; break;
                case 3: pPriority.Image = pPR4.Image; pPriority.Tag = 4; break;
                case 4: pPriority.Image = pPR5.Image; pPriority.Tag = 5; break;
                case 5: pPriority.Image = pPR1.Image; pPriority.Tag = 1; break;
            }
        }

        private void QuickTaskIcon_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                using (SelectIconDlg _dlg = new SelectIconDlg("TaskTemplate"))
                {
                    if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;

                    string iconPath = _dlg.iconPath;
                    string fileName = "stock" + Path.GetFileNameWithoutExtension(iconPath);

                    if (StixIcons.StockIconFromString(fileName) == 0) // custom icon
                    {
                        fileName = MMUtils.MindManager.Utilities.GetCustomIconSignature(iconPath);

                        if (!Utils.CustomIcons.ContainsKey(fileName))
                        {
                            Utils.CustomIcons.Add(fileName, iconPath);
                            File.Copy(iconPath, Utils.m_iconDB + Path.GetFileName(iconPath));
                        }
                    }

                    pb.Image = System.Drawing.Image.FromFile(iconPath);
                    pb.Tag = fileName;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                pb.Image = System.Drawing.Image.FromFile(Utils.m_imagesPath + "empty_icon.png");
                pb.Tag = "";
            }
        }

        private void pStartPlace_Click(object sender, EventArgs e)
        {
            if ((string)pStartPlace.Tag == "calendar")
            {
                pStartPlace.Image = pPeriod.Image; pStartPlace.Tag = "period";
                dtpStartDate.Visible = true; dtpStartDate.BringToFront();

                string tooltip = Utils.getString("TaskTemplateDlg.relativedate.tooltip");
                toolTip1.SetToolTip(pStartPlace, tooltip);
                toolTip1.Show(tooltip, pStartPlace);
            }
            else
            {
                pStartPlace.Image = pCalendar.Image; pStartPlace.Tag = "calendar";
                dtpStartDate.Visible = false;

                string tooltip = Utils.getString("TaskTemplateDlg.calendar.tooltip");
                toolTip1.SetToolTip(pStartPlace, tooltip);
                toolTip1.Show(tooltip, pStartPlace);
            }
        }

        private void pDuePlace_Click(object sender, EventArgs e)
        {
            if ((string)pDuePlace.Tag == "calendar")
            {
                pDuePlace.Image = pPeriod.Image; pDuePlace.Tag = "period";
                dtpDueDate.Visible = true; dtpDueDate.BringToFront();

                string tooltip = Utils.getString("TaskTemplateDlg.relativedate.tooltip");
                toolTip1.SetToolTip(pDuePlace, tooltip);
                toolTip1.Show(tooltip, pDuePlace);
            }
            else
            {
                pDuePlace.Image = pCalendar.Image; pDuePlace.Tag = "calendar";
                dtpDueDate.Visible = false;

                string tooltip = Utils.getString("TaskTemplateDlg.calendar.tooltip");
                toolTip1.SetToolTip(pDuePlace, tooltip);
                toolTip1.Show(tooltip, pDuePlace);
            }
        }

        private void cbResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start) { start = false; return; }

            string res = txtResources.Text.Trim().TrimEnd(',');
            string[] resources = res.Split(',').Select(s => s.Trim()).ToArray();
            string resource = cbResources.SelectedItem.ToString();

            if (!resources.Contains(resource))
            {
                if (res == "") res = resource;
                else res += ", " + resource;
                txtResources.Text = res;
            }
        }
        bool start = true;

        StixDB db = null;
        QuickTopicItem selectedItem = null;

        private void FourStateCB_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = null;

            if (sender is Label lbl)
            {
                switch (lbl.Name)
                {
                    case "lblTopicText": pb = mutTopicText; break;
                    case "lblProgress": pb = mutProgress; break;
                    case "lblPriority": pb = mutPriority; break;
                    case "lblStartDate": pb = mutStartDate; break;
                    case "lblDueDate": pb = mutDueDate; break;
                    case "lblResources": pb = ch4Resources; break;
                    case "lblIcon": pb = ch4Icon; break;
                    case "lblTags": pb = ch4Tags; break;
                }
            }
            else
                pb = sender as PictureBox;

            if (pb == null) return;

            string state = pb.Tag.ToString();

            if (state == "")
            {
                pb.Tag = "checked"; pb.Image = pChecked.Image;
                toolTip1.SetToolTip(pb, Utils.getString("quicktask.checked"));
            }
            else if (state == "checked")
            {
                pb.Tag = "uncheckedred"; pb.Image = pUncheckedRed.Image;
                toolTip1.SetToolTip(pb, Utils.getString("quicktask.uncheckedred"));
            }
            else if (state == "uncheckedred")
            {
                if (pb.Name.StartsWith("mut"))
                {
                    pb.Tag = ""; pb.Image = pUnchecked.Image;
                    toolTip1.SetToolTip(pb, Utils.getString("quicktask.unchecked"));
                }
                else
                {
                    pb.Tag = "checkedred"; pb.Image = pCheckedRed.Image;
                    toolTip1.SetToolTip(pb, Utils.getString("quicktask.checkedred"));
                }
            }
            else if (state == "checkedred")
            {
                pb.Tag = ""; pb.Image = pUnchecked.Image;
                toolTip1.SetToolTip(pb, Utils.getString("quicktask.unchecked"));
            }
        }

        private void mutDuration_Click(object sender, EventArgs e)
        {
            if (mutDuration.Tag.ToString() == "")
            {
                mutDuration.Image = pChecked.Image;
                mutDuration.Tag = "checked";
            }
            else
            {
                mutDuration.Image = pUnchecked.Image;
                mutDuration.Tag = "";
            }
        }

        private void cbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbQuickTopics.Items.Clear(); cbQuickTopics.Text = "";

            int groupID = (cbGroups.SelectedItem as QuickTopicGroup).ID;
            DataTable dt = db.ExecuteQuery("select * from QUICKTOPICTEMPLATES " +
                "where groupID=" + groupID + " order by name");

            foreach (DataRow row in dt.Rows)
            {
                string topictextState = "", progressState = "", priorityState = "", startdateState = "",
                    duedateState = "", durationState = "", effortState = "", resourcesState = "", iconState = "", tagsState = "";

                string topictext = row["topictext"].ToString();
                string[] parts = topictext.Split(new string[] { "$$$" }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    topictextState = parts[0]; topictext = parts[1]; mutTopicText.Tag = topictextState;
                }

                string progress = row["progress"].ToString(); int _progress = -1;
                parts = progress.Split(':');
                if (parts.Length > 1)
                {
                    progressState = parts[0]; _progress = Convert.ToInt32(parts[1]); mutProgress.Tag = progressState;
                }

                string priority = row["priority"].ToString(); int _priority = 0;
                parts = priority.Split(':');
                if (parts.Length > 1)
                {
                    priorityState = parts[0]; _priority = Convert.ToInt32(parts[1]); mutPriority.Tag = priorityState;
                }

                string dates = row["dates"].ToString();
                parts = dates.Split(new string[] { "$$$" }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    string[] states = parts[0].Split(':');
                    startdateState = states[0]; duedateState = states[1]; dates = parts[1];
                    mutStartDate.Tag = startdateState; mutDueDate.Tag = duedateState;
                }

                string duration = row["duration"].ToString();
                if (duration != "")
                {
                    parts = duration.Split(':');
                    if (parts.Length == 3)
                    {
                        durationState = parts[0]; duration = parts[1] + ":" + parts[2];
                    }
                }

                string effort = row["effort"].ToString();
                if (effort != "")
                {
                    parts = effort.Split(':');
                    if (parts.Length == 3)
                    {
                        effortState = parts[0]; effort = parts[1] + ":" + parts[2];
                    }
                }

                string icons = row["icons"].ToString(); parts = icons.Split(':');
                if (parts.Length > 1) { iconState = parts[0]; icons = parts[1]; ch4Icon.Tag = iconState; }

                string resources = row["resources"].ToString(); parts = resources.Split(':');
                if (parts.Length > 1) { resourcesState = parts[0]; resources = parts[1]; ch4Resources.Tag = resourcesState; }

                string tags = row["tags"].ToString(); parts = tags.Split(new[] { ';' }, 2 );
                if (parts.Length > 1) { tagsState = parts[0]; tags = parts[1]; ch4Tags.Tag = tagsState; }

                QuickTopicItem item = new QuickTopicItem(row["name"].ToString(), Convert.ToInt32(row["id"]), Convert.ToInt32(row["groupID"]),
                        Convert.ToInt32(row["_order"]), topictext, _progress, _priority, dates, duration, effort, icons,
                        resources, tags, topictextState, progressState, priorityState, startdateState,
                        duedateState, durationState, effortState, iconState, resourcesState, tagsState);

                cbQuickTopics.Items.Add(item);
            }

            if (cbQuickTopics.Items.Count > 0)
                cbQuickTopics.SelectedIndex = 0;
        }
    }

    public class DateItem
    {
        public DateItem(string name, string period)
        {
            Name = name;
            Period = period;
        }

        public string Name = "";
        public string Period = "";

        public override string ToString() => Name;
    }
}
