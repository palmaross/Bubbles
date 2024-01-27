using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TaskInfoQuickTask.htm");

            Text = Utils.getString("TaskTemplateDlg.title");
            chPrimary.Text = Utils.getString("TaskTemplateDlg.chPrimary");
            chProgress.Text = Utils.getString("TaskTemplateDlg.chProgress");
            chPriority.Text = Utils.getString("TaskTemplateDlg.chPriority");
            chStartDate.Text = Utils.getString("TaskTemplateDlg.chStartDate");
            chDueDate.Text = Utils.getString("TaskTemplateDlg.chDueDate");
            chResources.Text = Utils.getString("TaskTemplateDlg.chResources");
            chIcon.Text = Utils.getString("TaskTemplateDlg.chIcon");
            chTags.Text = Utils.getString("TaskTemplateDlg.chTags");

            lblChangeValue.Text = Utils.getString("TaskTemplateDlg.lblChangeValue");
            lblChangeIcon.Text = Utils.getString("TaskTemplateDlg.lblChangeIcon");
            lblTagGroup.Text = Utils.getString("TaskTemplateDlg.lblTagGroup");
            lblTag.Text = Utils.getString("TaskTemplateDlg.lblTag");
            btnApply.Text = Utils.getString("button.apply");
            btnClose.Text = Utils.getString("button.close");

            btnCancel.Text = Utils.getString("button.cancel");
            lblResourceName.Text = Utils.getString("TaskTemplateDlg.lblResourceName");

            toolTip1.SetToolTip(chPrimary, Utils.getString("TaskTemplateDlg.chPrimary.tooltip"));
            toolTip1.SetToolTip(numStartDate, Utils.getString("TaskTemplateDlg.numDate.tooltip"));
            toolTip1.SetToolTip(numDueDate, Utils.getString("TaskTemplateDlg.numDate.tooltip"));
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

            db = new BubblesDB();

            DataTable dt = db.ExecuteQuery("select * from TASKTEMPLATES order by name");
            foreach (DataRow row in dt.Rows)
            {
                TaskTemplateItem item = new TaskTemplateItem(Convert.ToInt32(row["prime"]), row["name"].ToString(),
                    Convert.ToInt32(row["progress"]), Convert.ToInt32(row["priority"]), row["dates"].ToString(), 
                    row["icon"].ToString(), row["resources"].ToString(), row["tags"].ToString());

                cbTaskTemplates.Items.Add(item);
            }

            pIcon.Tag = "stockquestion-mark";
            pPriority.Tag = 1; pProgress.Tag = 0;

            dt = db.ExecuteQuery("select * from RESOURCES order by name");
            foreach (DataRow row in dt.Rows)
                cbResources.Items.Add(row["name"]);

            if (cbTaskTemplates.Items.Count > 0)
                cbTaskTemplates.SelectedIndex = 0;
            if (cbResources.Items.Count > 0)
                cbResources.SelectedIndex = 0;

            dtpStartDate.Location = cbStartDatePeriod.Location;
            dtpDueDate.Location = cbDueDatePeriod.Location;

            this.HelpButtonClicked += this_HelpButtonClicked;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "TaskInfoQuickTask.htm");
        }

        private void New_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = true;
            panelTemplateName.BringToFront();
            panelTemplateName.Tag = "new";
            txtTemplateName.Text = "";
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = true;
            panelTemplateName.BringToFront();
            panelTemplateName.Tag = "edit";
            txtTemplateName.Text = cbTaskTemplates.Text;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (chPrimary.Checked)
            {
                MessageBox.Show(Utils.getString("TaskTemplateDlg.primary.warning"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(Utils.getString("TaskTemplateDlg.delete.question"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string name = cbTaskTemplates.Text;

            cbTaskTemplates.Items.Remove(cbTaskTemplates.SelectedItem);

            // Delete template from database
            db.ExecuteNonQuery("delete from TASKTEMPLATES where name=`" + name + "`");

            if (cbTaskTemplates.Items.Count > 0)
                cbTaskTemplates.SelectedIndex = 0;
        }

        private void cbTaskTemplates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RenameTemplate();
        }

        void RenameTemplate()
        {
            if (selectedItem == null) return;

            var item = selectedItem;
            string name = cbTaskTemplates.Text.Trim();

            if (item.Name != name) // User changed template name
            {
                if (MessageBox.Show(Utils.getString("taskinfo.quicktask.rename"), "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    db.ExecuteNonQuery("update TASKTEMPLATES set name=`" + name +
                        "` where name=`" + item.Name + "`");

                    DataTable dt = db.ExecuteQuery("select * from TASKTEMPLATES " +
                        "where name=`" + name + "`");

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("TaskTemplateDlg.templateexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RemoveItem(item.Name);
                    item.Name = name;
                    int i = cbTaskTemplates.Items.Add(item);
                    cbTaskTemplates.SelectedIndex = i;
                }
            }
            else
            {
                foreach (var _item in cbTaskTemplates.Items)
                {
                    TaskTemplateItem __item = _item as TaskTemplateItem;
                    if (__item.Name == name)
                    {
                        cbTaskTemplates.SelectedItem = __item;
                        break;
                    }
                }
            }
        }

        void RemoveItem(string name)
        {
            foreach (var _item in cbTaskTemplates.Items)
            {
                TaskTemplateItem item = _item as TaskTemplateItem;
                if (item.Name == name)
                {
                    cbTaskTemplates.Items.Remove(_item);
                    return;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string oldName = cbTaskTemplates.Text;
            string newName = txtTemplateName.Text.Trim();
            if (String.IsNullOrEmpty(newName)) return;

            DataTable dt = db.ExecuteQuery("select * from TASKTEMPLATES " +
                "where name=`" + newName + "`");

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show(Utils.getString("TaskTemplateDlg.templateexists"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((string)panelTemplateName.Tag == "new") // add new template
            {
                db.AddTaskTemplate(0, newName, 0, 0, "rel:today:1;rel:today:1", "", "", "");
                TaskTemplateItem item = new TaskTemplateItem(0, newName, 0, 0, "rel:today:1;rel:today:1", "", "", "");
                int i = cbTaskTemplates.Items.Add(item);
                cbTaskTemplates.SelectedIndex = i;
            }
            else // rename selected template
            {
                TaskTemplateItem item = cbTaskTemplates.SelectedItem as TaskTemplateItem;
                item.Name = newName;

                // Update template in the database
                db.ExecuteNonQuery("update TASKTEMPLATES set name=`" + newName +
                    "` where name=`" + oldName + "`");

                cbTaskTemplates.Items.Remove(cbTaskTemplates.SelectedItem);
                int i = cbTaskTemplates.Items.Add(item);
                cbTaskTemplates.SelectedIndex = i;
            }

            panelTemplateName.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelTemplateName.Visible = false;
        }

        private void cbTaskTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskTemplateItem item = cbTaskTemplates.SelectedItem as TaskTemplateItem;
            selectedItem = item;

            if (item != null)
            {
                string dates = item.Dates, startdate = "", duedate = "";
                if (dates != "")
                {
                    string[] parts = item.Dates.Split(';');
                    startdate = parts[0]; duedate = parts[1];
                }

                chPrimary.Checked = item.Primary == 1;
                if (chPrimary.Checked) chPrimary.Enabled = false; else chPrimary.Enabled = true;
                chProgress.Checked = item.Progress >= 0;
                chPriority.Checked = item.Priority > 0;
                chStartDate.Checked = startdate != "";
                chDueDate.Checked = duedate != "";
                chResources.Checked = item.Resources != "";
                chIcon.Checked = item.aIcon != "";
                chTags.Checked = item.Tags != "";

                if (chProgress.Checked)
                {
                    switch (item.Progress)
                    {
                        case 0: pProgress.Image = p0.Image; pProgress.Tag = 0; break;
                        case 25: pProgress.Image = p25.Image; pProgress.Tag = 25; break;
                        case 50: pProgress.Image = p50.Image; pProgress.Tag = 50; break;
                        case 75: pProgress.Image = p75.Image; pProgress.Tag = 75; break;
                        case 100: pProgress.Image = p100.Image; pProgress.Tag = 100; break;
                    }
                }
                else 
                {
                    pProgress.Image = p0.Image; pProgress.Tag = 0;
                }
                if (chPriority.Checked)
                {
                    switch (item.Priority)
                    {
                        case 1: pPriority.Image = pPR1.Image; pPriority.Tag = 1; break;
                        case 2: pPriority.Image = pPR2.Image; pPriority.Tag = 2; break;
                        case 3: pPriority.Image = pPR3.Image; pPriority.Tag = 3; break;
                        case 4: pPriority.Image = pPR4.Image; pPriority.Tag = 4; break;
                        case 5: pPriority.Image = pPR5.Image; pPriority.Tag = 5; break;
                    }
                }
                else
                {
                    pPriority.Image = pPR1.Image; pPriority.Tag = 1;
                }
                if (chStartDate.Checked)
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
                        SetPeriod(parts[1], cbStartDatePeriod);
                        numStartDate.Value = int.Parse(parts[2]);
                        pStartPlace.Image = pCalendar.Image; pStartPlace.Tag = "calendar";
                    }
                }
                else
                {
                    dtpStartDate.Visible = true;
                    cbStartDatePeriod.SelectedIndex = 0;
                    numStartDate.Enabled = false;
                    pStartPlace.Image = pCalendar.Image; pStartPlace.Tag = "calendar";
                }
                if (chDueDate.Checked)
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
                        SetPeriod(parts[1], cbDueDatePeriod);
                        numDueDate.Value = int.Parse(parts[2]);
                        pDuePlace.Image = pCalendar.Image; pDuePlace.Tag = "calendar";
                    }
                }
                else
                {
                    dtpDueDate.Visible = true;
                    cbDueDatePeriod.SelectedIndex = 0;
                    numDueDate.Enabled = false;
                    pDuePlace.Image = pCalendar.Image; pDuePlace.Tag = "calendar";
                }
                if (chResources.Checked)
                {
                    txtResources.Text = item.Resources;
                }
                else
                    txtResources.Text = "";

                if (chIcon.Checked)
                {
                    string filename = item.aIcon;
                    string _filename = filename;
                    string rootPath = Utils.m_dataPath + "IconDB\\";

                    if (filename.StartsWith("stock"))
                    {
                        rootPath = MMUtils.MindManager.GetPath(Mindjet.MindManager.Interop.MmDirectory.mmDirectoryIcons);
                        _filename = filename.Substring(5) + ".ico"; // stockemail -> email.ico
                    }

                    if (File.Exists(rootPath + _filename))
                    {
                        pIcon.Image = Image.FromFile(rootPath + _filename);
                        pIcon.Tag = filename;
                    }
                }
                else
                {
                    pIcon.Image = pIconDefault.Image; pIcon.Tag = "stockquestion-mark";
                }
                if (chTags.Checked)
                {
                    string[] tags = item.Tags.Split(';');
                    string[] parts1 = tags[0].Split(':');
                    txtTagGroup1.Text = parts1[0]; txtTag1.Text = parts1[1];

                    if (tags.Length == 2)
                    {
                        string[] parts2 = tags[1].Split(':');
                        txtTagGroup2.Text = parts2[0]; txtTag2.Text = parts2[1];
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            // First, check if template was renamed!
            RenameTemplate();

            TaskTemplateItem item = cbTaskTemplates.SelectedItem as TaskTemplateItem;

            if (chPrimary.Checked && chPrimary.Enabled) // primary changed!
            {
                db.ExecuteNonQuery("update TASKTEMPLATES set prime=0 where prime=1");
                db.ExecuteNonQuery("update TASKTEMPLATES set prime=1 where name=`" + item.Name + "`");
                chPrimary.Enabled = false;
                for (int i = 0; i < cbTaskTemplates.Items.Count; i++)
                {
                    TaskTemplateItem _item = cbTaskTemplates.Items[i] as TaskTemplateItem;
                    if (_item.Primary == 1)
                    {
                        _item.Primary = 0; cbTaskTemplates.Items[i] = _item; break;
                    }
                }
                item.Primary = 1;
            }

            if (chProgress.Checked) { item.Progress = (int)pProgress.Tag; }
            else { item.Progress = -1; pProgress.Image = p0.Image; pProgress.Tag = 0; }
            if (chPriority.Checked) { item.Priority = (int)pPriority.Tag; }
            else { item.Priority = 0; pPriority.Image = pPR1.Image; pPriority.Tag = 1; }
            if (chResources.Checked) { item.Resources = txtResources.Text.Trim(); }
            else { item.Resources = ""; txtResources.Text = ""; }
            if (chIcon.Checked) { item.aIcon = (string)pIcon.Tag; }
            else { item.aIcon = ""; pIcon.Image = pIconDefault.Image; pIcon.Tag = "stockquestion-mark"; }

            if (chStartDate.Checked || chDueDate.Checked)
            {
                string startdate = "", duedate = "";
                if (chStartDate.Checked)
                {
                    if ((string)pStartPlace.Tag == "period") // calendar is shown
                    {
                        startdate = "abs:" + dtpStartDate.Value.ToShortDateString();
                    }
                    else // period is shown
                    {
                        DateItem _item = cbStartDatePeriod.SelectedItem as DateItem;
                        startdate = "rel:" + _item.Period + ":" + numStartDate.Value.ToString();
                    }
                }
                else
                {
                    dtpStartDate.Visible = false; cbStartDatePeriod.SelectedIndex = 0; numStartDate.Value = 1;
                }
                if (chDueDate.Checked)
                {
                    if ((string)pDuePlace.Tag == "period") // calendar is shown
                    {
                        duedate = "abs:" + dtpDueDate.Value.ToShortDateString();
                    }
                    else // period is shown
                    {
                        DateItem _item = cbDueDatePeriod.SelectedItem as DateItem;
                        duedate = "rel:" + _item.Period + ":" + numDueDate.Value.ToString();
                    }
                }
                else
                {
                    dtpDueDate.Visible = false; cbDueDatePeriod.SelectedIndex = 0; numDueDate.Value = 1;
                }

                item.Dates = startdate + ";" + duedate;
            }
            else
            {
                dtpStartDate.Visible = false; cbStartDatePeriod.SelectedIndex = 0; numStartDate.Value = 1;
                dtpDueDate.Visible = false; cbDueDatePeriod.SelectedIndex = 0; numDueDate.Value = 1;
                item.Dates = "";
            }
            if (chTags.Checked)
            {
                string tag1 = "", tag2 = "", tags = "";
                if (txtTagGroup1.Text != "" && txtTag1.Text != "")
                    tag1 = txtTagGroup1.Text.Trim() + ":" + txtTag1.Text.Trim();
                else { 
                    txtTagGroup1.Text = ""; txtTag1.Text = ""; }

                if (txtTagGroup2.Text != "" && txtTag2.Text != "")
                    tag2 = txtTagGroup2.Text.Trim() + ":" + txtTag2.Text.Trim();
                else {
                    txtTagGroup2.Text = ""; txtTag2.Text = ""; }

                if (tag1 != "") tags = tag1;
                if (tag2 != "")
                {
                    if (tags != "") tags += ";" + tag2;
                    else tags = tag2;
                }

                item.Tags = tags;
            }
            else
            {
                txtTagGroup1.Text = ""; txtTagGroup2.Text = "";
                txtTag1.Text = ""; txtTag2.Text = ""; item.Tags = "";
            }

            cbTaskTemplates.SelectedItem = item;

            db.ExecuteNonQuery("update TASKTEMPLATES set " +
                "progress=" + item.Progress + ", " +
                "priority=" + item.Priority + ", " +
                "dates=`" + item.Dates + "`, " +
                "resources=`" + item.Resources + "`, " +
                "icon=`" + item.aIcon + "`, " +
                "tags=`" + item.Tags + "` " +
                "where name=`" + item.Name + "`");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            db.Dispose(); db = null;
            this.Close();
        }

        private void pProgress_Click(object sender, EventArgs e)
        {
            switch (pProgress.Tag)
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
            switch (pPriority.Tag)
            {
                case 1: pPriority.Image = pPR2.Image; pPriority.Tag = 2; break;
                case 2: pPriority.Image = pPR3.Image; pPriority.Tag = 3; break;
                case 3: pPriority.Image = pPR4.Image; pPriority.Tag = 4; break;
                case 4: pPriority.Image = pPR5.Image; pPriority.Tag = 5; break;
                case 5: pPriority.Image = pPR1.Image; pPriority.Tag = 1; break;
            }
        }

        private void pIcon_Click(object sender, EventArgs e)
        {
            using (SelectIconDlg _dlg = new SelectIconDlg(new List<string>()))
            {
                _dlg.aIconName = false;

                if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                    return;

                string iconPath = _dlg.iconPath;
                string fileName = "stock" + Path.GetFileNameWithoutExtension(iconPath);

                if (BubbleIcons.StockIconFromString(fileName) == 0) // custom icon
                    fileName = Path.GetFileName(iconPath);

                pIcon.Image = Image.FromFile(iconPath);
                pIcon.Tag = fileName;
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

        BubblesDB db = null;
        TaskTemplateItem selectedItem = null;
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
