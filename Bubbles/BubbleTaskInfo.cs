using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Cursor = System.Windows.Forms.Cursor;
using System.Data;

namespace Bubbles
{
    internal partial class BubbleTaskInfo : Form
    {
        public BubbleTaskInfo(int ID, string _orientation, string stickname = "")
        {
            InitializeComponent();

            BubblesButton.m_TaskInfo = this;

            this.Tag = ID; // correct
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TaskInfoStick.htm");

            toolTip1.SetToolTip(pictureHandle, stickname);
            toolTip1.SetToolTip(pProgress, Utils.getString("taskinfo.pProgress.tooltip"));
            toolTip1.SetToolTip(pPriority, Utils.getString("taskinfo.pPriority.tooltip"));
            toolTip1.SetToolTip(pResources, Utils.getString("taskinfo.pResources.tooltip"));
            toolTip1.SetToolTip(numDuration, Utils.getString("taskinfo.numDuration.tooltip"));
            toolTip1.SetToolTip(linkDurationUnit, Utils.getString("taskinfo.lblDurUnit.tooltip"));
            toolTip1.SetToolTip(pQuickTask, Utils.getString("taskinfo.pQuickTask.tooltip"));
            toolTip1.SetToolTip(pRemoveTaskInfo, Utils.getString("taskinfo.pRemoveTaskInfo.tooltip"));
            toolTip1.SetToolTip(pStartDate, Utils.getString("taskinfo.pStartDate.tooltip"));
            toolTip1.SetToolTip(pDueDate, Utils.getString("taskinfo.pDueDate.tooltip"));

            RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menus
            cmsDates.Items["Dates_today"].Text = Utils.getString("quicktasktemplate.today");
            cmsDates.Items["Dates_today_today"].Text = Utils.getString("cmsDates.today_today");
            cmsDates.Items["Dates_today_tomorrow"].Text = Utils.getString("cmsDates.today_tomorrow");
            cmsDates.Items["Dates_tomorrow"].Text = Utils.getString("quicktasktemplate.tomorrow");
            cmsDates.Items["Dates_tomorrow_tomorrow"].Text = Utils.getString("cmsDates.tomorrow_tomorrow");
            cmsDates.Items["Dates_thisweek"].Text = Utils.getString("quicktasktemplate.thisweek");
            cmsDates.Items["Dates_nextweek"].Text = Utils.getString("quicktasktemplate.nextweek");
            cmsDates.ItemClicked += ContextMenu_ItemClicked;

            ToolStripItem tsi = new ToolStripLabel(Utils.getString("taskinfo.cmsRemoveTaskInfo.Label"));
            tsi.Font = new Font(tsi.Font, FontStyle.Bold);
            cmsRemoveTaskInfo.Items.Add(tsi);

            tsi = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Dates"));
            tsi.Name = "TaskInfoDates"; cmsRemoveTaskInfo.Items.Add(tsi);
            (tsi as ToolStripMenuItem).CheckOnClick = true;
            (tsi as ToolStripMenuItem).Checked = true;
            tsi = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Progress"));
            tsi.Name = "TaskInfoProgress"; cmsRemoveTaskInfo.Items.Add(tsi);
            (tsi as ToolStripMenuItem).CheckOnClick = true;
            (tsi as ToolStripMenuItem).Checked = true;
            tsi = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Priority"));
            tsi.Name = "TaskInfoPriority"; cmsRemoveTaskInfo.Items.Add(tsi);
            (tsi as ToolStripMenuItem).CheckOnClick = true;
            tsi = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Resources"));
            tsi.Name = "TaskInfoResources"; cmsRemoveTaskInfo.Items.Add(tsi);
            (tsi as ToolStripMenuItem).CheckOnClick = true;

            cmsRemoveTaskInfo.ItemClicked += ContextMenu_ItemClicked;
            cmsRemoveTaskInfo.Closing += CmsRemoveTaskInfo_Closing;

            cmsCommon.ItemClicked += cmsCommon_ItemClicked;
            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typetaskinfo);

            if (collapsed) {
                collapsed = false; Collapse(); }

            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.minute"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.hour"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.day"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.week"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.month"));
            ST_DurationUnits.SelectedIndex = 2;
            linkDurationUnit.Text = Utils.getString("task.durationunit.day");

            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();
            pictureHandle.MouseDown += Move_Stick;
            this.MouseDown += Move_Stick;
            Manage.Click += Manage_Click;
            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typetaskinfo);

            this.ActiveControl = null;

            // Set today's date
            pStartDate.Text = DateTime.Now.Date.ToString("dd, MM").Replace(", ", "/");
            pStartDate.Tag = DateTime.Now.Date.AddHours(8);
            pStartDate.AutoSize = false;
            pStartDate.Size = new Size(pStartDate.Width, p2.Width);

            pDueDate.Text = DateTime.Now.Date.ToString("dd, MM").Replace(", ", "/");
            pDueDate.Tag = DateTime.Now.Date.AddHours(8);
            pDueDate.AutoSize = false;
            pDueDate.Size = new Size(pDueDate.Width, p2.Width);

            numDuration.AutoSize = false;
            numDuration.Size = new Size(numDuration.Width, p2.Width);
            linkDurationUnit.BringToFront();

            BubblesButton.SetDates();

            // Quick Task button context menu
            cmsTaskTemplates.ItemClicked += ContextMenu_ItemClicked;
            PopulateQuickTasks();
        }

        private void CmsRemoveTaskInfo_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
        }

        void PopulateQuickTasks()
        {
            cmsTaskTemplates.Items.Clear();

            ToolStripItem tsi = cmsTaskTemplates.Items.Add(Utils.getString("taskinfo.quicktask.manage"));
            tsi.Name = "ManageTaskTemplates";
            StickUtils.SetContextMenuImage(cmsTaskTemplates.Items["ManageTaskTemplates"], "manage.png");
            cmsTaskTemplates.Items.Add(new ToolStripSeparator());

            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from TASKTEMPLATES order by prime DESC, name");

                foreach (DataRow row in dt.Rows)
                {
                    TaskTemplateItem item = new TaskTemplateItem(Convert.ToInt32(row["prime"]), row["name"].ToString(),
                        Convert.ToInt32(row["progress"]), Convert.ToInt32(row["priority"]),
                        row["dates"].ToString(), row["icon"].ToString(), row["resources"].ToString(), row["tags"].ToString());

                    tsi = cmsTaskTemplates.Items.Add(item.Name);
                    tsi.Tag = item; tsi.Name = "TaskTemplate";

                    if (item.Primary == 1) primaryQuickTask = item;
                }
            }
        }

        private void ResourceTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if ((sender as ToolStripTextBox).ForeColor == SystemColors.GrayText)
            {
                (sender as ToolStripTextBox).Text = "";
                (sender as ToolStripTextBox).ForeColor = SystemColors.WindowText;
            }
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "TaskTemplate")
            {
                QuickTask = e.ClickedItem.Tag as TaskTemplateItem;

                Transaction _tr = MMUtils.ActiveDocument.NewTransaction(Utils.getString("QuickTask.transaction.name"));
                _tr.IsUndoable = true;
                _tr.Execute += new ITransactionEvents_ExecuteEventHandler(SetQuickTask);
                _tr.Start();
            }
            else if (e.ClickedItem.Name == "ManageTaskTemplates")
            {
                using (TaskTemplateDlg dlg = new TaskTemplateDlg())
                    dlg.ShowDialog();

                PopulateQuickTasks();
            }
            else if (e.ClickedItem.Name.StartsWith("Dates_"))
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                    return;

                DateTime startdate = DateTime.Now.Date, duedate = DateTime.Now.Date;
                bool both = false;
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    switch (e.ClickedItem.Name.Substring(6))
                    {
                        case "today":
                            both = false; break;
                        case "today_today":
                            both = true; break;
                        case "today_tomorrow":
                            both = true; duedate = duedate.AddDays(1); break;
                        case "tomorrow":
                            both = false; 
                            startdate = startdate.AddDays(1);
                            duedate = duedate.AddDays(1);
                            break;
                        case "tomorrow_tomorrow":
                            both = true;
                            startdate = startdate.AddDays(1); duedate = duedate.AddDays(1);
                            break;
                        case "thisweek":
                            both = true;
                            startdate = Utils.getWeekBegin();
                            duedate = Utils.getWeekEnd();
                            break;
                        case "nextweek":
                            both = true;
                            startdate = Utils.getWeekBegin(1);
                            duedate = Utils.getWeekEnd(1);
                            break;
                    }
                    if (daterightclick == "startdate")
                    {
                        t.Task.StartDate = startdate.AddHours(8);
                        if (both) t.Task.DueDate = duedate.AddHours(8);
                    }
                    else if (daterightclick == "duedate")
                    {
                        if (!both)
                            t.Task.DueDate = duedate.AddHours(8);
                        else
                        {
                            t.Task.StartDate = startdate.AddHours(8);
                            t.Task.DueDate = duedate.AddHours(8);
                        }
                    }
                }
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsCommon.Items)
                item.Visible = true;

            cmsCommon.Show(Cursor.Position);
        }

        private void Move_Stick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void cmsCommon_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                BubblesButton.m_TaskInfo = null;
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "TaskInfoStick.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StickUtils.SaveStick(this.Bounds, (int)this.Tag, orientation, collapsed);
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                Collapse();
            }
        }

        public void Rotate()
        {
            orientation = StickUtils.RotateStick(this, Manage, orientation);

            panelStartDate.Location = new Point(panelStartDate.Location.Y, panelStartDate.Location.X);
            panelDueDate.Location = new Point(panelDueDate.Location.Y, panelDueDate.Location.X);
            panelDuration.Location = new Point(panelDuration.Location.Y, panelDuration.Location.X);
        }

        /// <summary>
        /// Collapse/Expand stick
        /// </summary>
        /// <param name="CollapseAll">"Collapse All" command from Main Menu</param>
        /// <param name="ExpandAll">"Expand All" command from Main Menu</param>
        public void Collapse(bool CollapseAll = false, bool ExpandAll = false)
        {
            if (collapsed) // Expand stick
            {
                if (CollapseAll) return;

                collapseState = this.Location; // remember collapsed location
                collapseOrientation = orientation;
                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;
                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                {
                    this.Location = collapseState; // restore collapsed location
                    if (orientation != collapseOrientation) Rotate();
                }
            }
        }
        Point collapseState = new Point(0, 0);
        string collapseOrientation = "N";

        private void p100_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            bool alltopicshaveicon = true;
            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (t.Task.Complete != 100) { alltopicshaveicon = false; break; }
            }

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (alltopicshaveicon) t.Task.Complete = -1;
                else t.Task.Complete = 100;
            }
        }

        private void pProgress_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                    return;

                bool alltopicshaveicon = true;
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (t.Task.Complete != 0) { alltopicshaveicon = false; break; }
                }

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (alltopicshaveicon) t.Task.Complete = -1;
                    else t.Task.Complete = 0;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                StickUtils.ShowCommandPopup(this, orientation, StickUtils.typetaskinfo, "progress");
            }
        }

        private void pPriority_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                    return;

                bool alltopicshaveicon = true;
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (t.Task.Priority != MmTaskPriority.mmTaskPriority1) 
                    { alltopicshaveicon = false; break; }
                }

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (alltopicshaveicon) t.Task.Priority = 0;
                    else t.Task.Priority = MmTaskPriority.mmTaskPriority1;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                StickUtils.ShowCommandPopup(this, orientation, StickUtils.typetaskinfo, "priority");
            }
        }

        private void DateBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string date = "calendar_startdate"; DateTime dt = (DateTime)pStartDate.Tag;
                if (sender as MaskedTextBox == pDueDate) 
                { date = "calendar_duedate"; dt = (DateTime)pDueDate.Tag; }

                MyDateTimePicker mdtp = new MyDateTimePicker();
                mdtp.Location = StickUtils.GetChildLocation(this, mdtp.Bounds, orientation, date);
                mdtp.dateTimePicker1.Value = dt;
                mdtp.AccessibleName = date;
                mdtp.Show(); // ShowDialog() produce a "ding" sound when closing form
            }
        }

        private void pStartDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                daterightclick = "startdate";
        }

        private void pDueDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                daterightclick = "duedate";
        }

        private void pTopicSetDate_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pStartDate.Select(0, 0); pDueDate.Select(0, 0);

            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            string fromstick = pStartDate.Text;
            DateTime dt = (DateTime)pStartDate.Tag;
            if (pb.Name == "pTopicDueDate")
                dt = (DateTime)pDueDate.Tag;

            if (dt == null) return;

            bool setdate = !(bool)pb.Tag;

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (setdate) // set date to topic
                {
                    if (pb.Name == "pTopicStartDate")
                    {
                        t.Task.StartDate = (DateTime)dt;
                        pTopicStartDate.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");

                    }
                    else
                    {
                        t.Task.DueDate = (DateTime)dt;
                        pTopicDueDate.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");

                    }
                }
                else // remove date from topic
                {
                    if (pb.Name == "pTopicStartDate")
                    {
                        t.Task.StartDate = MMUtils.NULLDATE;
                        pTopicStartDate.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");

                    }
                    else
                    {
                        t.Task.DueDate = MMUtils.NULLDATE;
                        pTopicDueDate.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");

                    }
                }
            }
        }

        private void ST_DurationUnits_MouseEnter(object sender, EventArgs e)
        {
            ST_DurationUnits.DroppedDown = true;
        }

        private void ST_DurationUnits_Enter(object sender, EventArgs e)
        {
            ST_DurationUnits.DroppedDown = true;
        }

        private void ST_DurationUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ST_DurationUnits.SelectedIndex)
            {
                case 0:
                    linkDurationUnit.Text = Utils.getString("task.durationunit.minute");
                    break;
                case 1:
                    linkDurationUnit.Text = Utils.getString("task.durationunit.hour");
                    break;
                case 2:
                    linkDurationUnit.Text = Utils.getString("task.durationunit.day");
                    break;
                case 3:
                    linkDurationUnit.Text = Utils.getString("task.durationunit.week");
                    break;
                case 4:
                    linkDurationUnit.Text = Utils.getString("task.durationunit.month");
                    break;
            }

            StickUtils.ActivateMindManager(); // cmsDuration.Hide() locks MindManager
        }

        /// <summary>
        /// Show the Duration unit combobox
        /// </summary>
        private void linkDurationUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmsDuration.Items["ST_DurationUnits"].Visible = true;

            Rectangle rec = linkDurationUnit.RectangleToScreen(linkDurationUnit.ClientRectangle);
            cmsDuration.Show(rec.X, rec.Bottom);
        }

        private void numDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                numDuration_ValueChanged(null, null);

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void numDuration_ValueChanged(object sender, EventArgs e)
        {
            if (MMDuration) { MMDuration = false; return; } // Duration was changed by MM. Do not process here!

            if (MMUtils.ActiveDocument != null && MMUtils.ActiveDocument.Selection.PrimaryTopic != null)
            {
                stickDuration = true; // do not set numDuration value in Bubbles.onObjectChanged event
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Task.DurationUnit = GetDurationUnit();
                    t.Task.SetDuration(GetDurationUnit(), (int)numDuration.Value); // raises onObjectChanged event
                }
                // onObjectChanged event raised
                stickDuration = false; // to continue process numDuration value changing
            }
        }
        /// <summary>True - Duration was set by stick </summary>
        public bool stickDuration = false;
        /// <summary>True - Duration was set by MM </summary>
        public bool MMDuration = false;

        private void pRemoveTaskInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument != null && MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        if ((cmsRemoveTaskInfo.Items["TaskInfoDates"] as ToolStripMenuItem).Checked) {
                            t.Task.StartDate = MMUtils.NULLDATE; t.Task.DueDate = MMUtils.NULLDATE; }

                        if ((cmsRemoveTaskInfo.Items["TaskInfoProgress"] as ToolStripMenuItem).Checked)
                            t.Task.Complete = -1;

                        if ((cmsRemoveTaskInfo.Items["TaskInfoPriority"] as ToolStripMenuItem).Checked)
                            t.Task.Priority = 0;

                        if ((cmsRemoveTaskInfo.Items["TaskInfoResources"] as ToolStripMenuItem).Checked)
                            t.Task.Resources = "";
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsRemoveTaskInfo.Items)
                    item.Visible = true;

                cmsRemoveTaskInfo.Show(Cursor.Position);
            }
        }

        private void pQuickTask_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && primaryQuickTask != null)
            {
                QuickTask = primaryQuickTask;
                Transaction _tr = MMUtils.ActiveDocument.NewTransaction(Utils.getString("QuickTask.transaction.name"));
                _tr.IsUndoable = true;
                _tr.Execute += new ITransactionEvents_ExecuteEventHandler(SetQuickTask);
                _tr.Start();
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTaskTemplates.Items)
                    item.Visible = true;

                cmsTaskTemplates.Show(Cursor.Position);
            }
        }

        void SetQuickTask(Document pDocument)
        {
            if (MMUtils.ActiveDocument == null ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0) return;

            string startdate = "", duedate = "";
            if (QuickTask.Dates != "")
            {
                string[] dates = QuickTask.Dates.Split(';');
                startdate = dates[0]; duedate = dates[1];
            }

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                CheckTopicTaskInfo(t);

                t.Task.Complete = QuickTask.Progress;
                t.Task.Priority = GetPriority(QuickTask.Priority);
                DateTime? dtstart = Utils.GetDate(startdate);
                DateTime? dtdue = Utils.GetDate(duedate);
                if (dtstart != null && !(dtstart == Utils.NULLDATE && t.Task.HasDueDate))
                    t.Task.StartDate = (DateTime)dtstart;
                if (dtdue != null && !(dtdue == Utils.NULLDATE && t.Task.HasStartDate)) 
                    t.Task.DueDate = (DateTime)dtdue;
                if (QuickTask.Resources != "")
                    t.Task.Resources = QuickTask.Resources;
                if (QuickTask.aIcon != "")
                    SetIcon(QuickTask.aIcon, t);
                if (QuickTask.Tags != "")
                {
                    string[] tags = QuickTask.Tags.Split(';');
                    string[] tag1 = tags[0].Split(':');
                    MapMarkers.AddTagToTopic(t, tag1[1], "", tag1[0], "");

                    if (tags.Length > 1)
                    {
                        string[] tag2 = tags[1].Split(':');
                        MapMarkers.AddTagToTopic(t, tag2[1], "", tag2[0], "");
                    }
                }
            }
        }

        void CheckTopicTaskInfo(Topic t)
        {
            if (t.Task.Priority != 0 && QuickTask.Priority == 0 ||
                (t.Task.Complete >= 0 && QuickTask.Progress == -1) ||
                ((t.Task.StartDate != Utils.NULLDATE || t.Task.DueDate != Utils.NULLDATE) && QuickTask.Dates == "") ||
                (t.Task.Resources != "" && QuickTask.Resources == ""))
            {
                string text = t.Text;
                if (text.Length > 40) text = text.Substring(0, 37) + "...";

                if (MessageBox.Show(String.Format(Utils.getString("quicktask.DeleteTaskInfo"), text),
                    Utils.getString("quicktask.DeleteTaskInfo.title"), 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (t.Task.Priority != 0) t.Task.Priority = 0;
                    if (t.Task.Complete >= 0) t.Task.Complete = -1;
                    if (t.Task.Resources != "") t.Task.Resources = "";
                    if (t.Task.StartDate != Utils.NULLDATE) t.Task.StartDate = Utils.NULLDATE;
                    if (t.Task.DueDate != Utils.NULLDATE) t.Task.DueDate = Utils.NULLDATE;
                }
            }
        }

        void SetIcon(string fileName, Topic t)
        {
            if (fileName.StartsWith("stock"))
            {
                MmStockIcon icon = BubbleIcons.StockIconFromString(fileName);
                if (icon != 0)
                {
                    MapMarkers.GetIcon(icon, "", "", "");

                    if (!t.AllIcons.ContainsStockIcon(icon))
                        t.AllIcons.AddStockIcon(icon);
                }
            }
            else
            {
                string path = Utils.m_dataPath + "IconDB\\" + fileName;
                if (System.IO.File.Exists(path))
                {
                    string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(path);
                    MapMarkers.GetIcon(0, signature, "", path);

                    if (!t.AllIcons.ContainsCustomIcon(signature))
                        t.AllIcons.AddCustomIconFromMap(signature);
                }
            }
        }

        private MmDurationUnit GetDurationUnit()
        {
            switch (ST_DurationUnits.SelectedIndex)
            {
                case 0:
                    return MmDurationUnit.mmDurationUnitMinute;
                case 1:
                    return MmDurationUnit.mmDurationUnitHour;
                case 2:
                    return MmDurationUnit.mmDurationUnitDay;
                case 3:
                    return MmDurationUnit.mmDurationUnitWeek;
                case 4:
                    return MmDurationUnit.mmDurationUnitMonth;
            }
            return MmDurationUnit.mmDurationUnitDay;
        }

        public static MmTaskPriority GetPriority(int value)
        {
            switch (value)
            {
                case 1: return MmTaskPriority.mmTaskPriority1;
                case 2: return MmTaskPriority.mmTaskPriority2;
                case 3: return MmTaskPriority.mmTaskPriority3;
                case 4: return MmTaskPriority.mmTaskPriority4;
                case 5: return MmTaskPriority.mmTaskPriority5;
                default: return MmTaskPriority.mmTaskPriorityNone;
            }
        }

        private void pResources_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (BubblesButton.m_Resources == null)
                    BubblesButton.m_Resources = new ResourcesDlg();

                if (BubblesButton.m_Resources.Visible)
                    BubblesButton.m_Resources.Hide();
                else
                {
                    BubblesButton.m_Resources.Location =
                        StickUtils.GetChildLocation(this, BubblesButton.m_Resources.Bounds, orientation, "resources");
                    BubblesButton.m_Resources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
            }
        }

        string orientation = "H";
        bool collapsed = false;

        int RealLength;

        TaskTemplateItem primaryQuickTask = null;
        TaskTemplateItem QuickTask = null;
        string daterightclick = "";

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public class TaskTemplateItem
    {
        public TaskTemplateItem(int primary, string name, int progress, int priority,
            string dates, string icon, string resources, string tags)
        {
            Primary = primary;
            Name = name;
            Progress = progress;
            Priority = priority;
            Dates = dates;
            aIcon = icon;
            Resources = resources;
            Tags = tags;
        }

        public int Primary = 0;
        public string Name = "";
        public int Progress = 0;
        public int Priority = 0;
        public string Dates = "";
        public string aIcon = "";
        public string Resources = "";
        public string Tags = "";

        public override string ToString()
        {
            return Name;
        }
    }
}
