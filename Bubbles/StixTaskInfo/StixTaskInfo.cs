using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Cursor = System.Windows.Forms.Cursor;
using System.Data;
using Color = System.Drawing.Color;
using System.Collections.Generic;

namespace Bubbles
{
    internal partial class StixTaskInfo : Form
    {
        public StixTaskInfo(int ID, string _orientation, string stickname = "")
        {
            InitializeComponent();

            StixMain.m_TaskInfo = this;

            this.Tag = ID;
            orientation = _orientation;

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TaskInfoStix.htm");

            myToolTip1.SetToolTip(pictureHandle, stickname +
                Utils.getString("StixTaskInfo.description") +
                Utils.getString("HeadIcon.tooltip.tips"));

            toolTip1.SetToolTip(p100, Utils.getString("taskinfo.p100.tooltip"));
            toolTip1.SetToolTip(Manage, Utils.getString("ManageIcon.tooltip"));
            toolTip1.SetToolTip(pProgress, Utils.getString("taskinfo.pProgress.tooltip"));
            toolTip1.SetToolTip(pPriority, Utils.getString("taskinfo.pPriority.tooltip"));
            toolTip1.SetToolTip(pResources, Utils.getString("taskinfo.pResources.tooltip"));
            toolTip1.SetToolTip(numDuration, Utils.getString("taskinfo.numDuration.tooltip"));
            toolTip1.SetToolTip(linkDurationUnit, Utils.getString("taskinfo.lblDurUnit.tooltip"));
            toolTip1.SetToolTip(numEffort, Utils.getString("taskinfo.numEffort.tooltip"));
            toolTip1.SetToolTip(linkEffortUnit, Utils.getString("taskinfo.lblEffortUnit.tooltip"));
            toolTip1.SetToolTip(btnSetDuration, Utils.getString("taskinfo.btnSetDuration.tooltip"));
            toolTip1.SetToolTip(btnSetEffort, Utils.getString("taskinfo.btnSetEffort.tooltip"));
            myToolTip1.SetToolTip(pQuickTask, Utils.getString("taskinfo.pQuickTask.tooltip") +
                Utils.getString("taskinfo.pQuickTask.description") + Utils.getString("taskinfo.pQuickTask.tips"));
            myToolTip1.SetToolTip(pRemoveTaskInfo, Utils.getString("taskinfo.pRemoveTaskInfo.tooltip") +
                Utils.getString("taskinfo.pRemoveTaskInfo.description") + Utils.getString("taskinfo.pRemoveTaskInfo.tips"));
            toolTip1.SetToolTip(pStartDate, Utils.getString("taskinfo.pStartDate.tooltip"));
            toolTip1.SetToolTip(pDueDate, Utils.getString("taskinfo.pDueDate.tooltip"));

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

            TaskInfoDates = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Dates"));
            (TaskInfoDates as ToolStripMenuItem).CheckOnClick = true;

            TaskInfoProgress = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Progress"));
            (TaskInfoProgress as ToolStripMenuItem).CheckOnClick = true;

            TaskInfoPriority = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Priority"));
            (TaskInfoPriority as ToolStripMenuItem).CheckOnClick = true;

            TaskInfoResources = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.Resources"));
            (TaskInfoResources as ToolStripMenuItem).CheckOnClick = true;

            TaskInfoEffort = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.numEffort.tooltip"));
            (TaskInfoEffort as ToolStripMenuItem).CheckOnClick = true;

            cmsRemoveTaskInfo.Items.Add(new ToolStripSeparator());

            ToolStripItem RemoveDefaults = cmsRemoveTaskInfo.Items.Add(Utils.getString("taskinfo.removedefaults"));
            RemoveDefaults.ToolTipText = Utils.getString("taskinfo.removedefaults.tooltip");
            RemoveDefaults.Click += RemoveDefaults_Click;

            SetQuickTaskDefault();

            cmsRemoveTaskInfo.ItemClicked += ContextMenu_ItemClicked;
            cmsRemoveTaskInfo.Closing += CmsRemoveTaskInfo_Closing;

            cmsCommon.ItemClicked += cmsCommon_ItemClicked;
            StixUtils.SetCommonContextMenu(cmsCommon, StixUtils.typetaskinfo);

            StixStarting = true;
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.minute"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.hour"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.day"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.week"));
            ST_DurationUnits.Items.Add(Utils.getString("task.durationunits.month"));
            ST_DurationUnits.SelectedIndex = 2;
            linkDurationUnit.Text = Utils.getString("task.durationunit.day");

            ST_EffortUnits.Items.Add(Utils.getString("task.durationunits.minute"));
            ST_EffortUnits.Items.Add(Utils.getString("task.durationunits.hour"));
            ST_EffortUnits.Items.Add(Utils.getString("task.durationunits.day"));
            ST_EffortUnits.Items.Add(Utils.getString("task.durationunits.week"));
            ST_EffortUnits.Items.Add(Utils.getString("task.durationunits.month"));
            ST_EffortUnits.SelectedIndex = 1;
            linkEffortUnit.Text = Utils.getString("task.durationunit.hour");
            StixStarting = false;

            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();
            pictureHandle.MouseDown += Move_Stick;
            this.MouseDown += Move_Stick;
            Manage.Click += Manage_Click;

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

            // Quick Task button context menu
            cmsTaskTemplates.ItemClicked += ContextMenu_ItemClicked;
            PopulateQuickTopics();

            // Resources context menu
            cmsResources.ItemClicked += ContextMenu_ItemClicked;
            PopulateResources();

            // Check Quick Task Remove Defaults

            fsize = pStartDate.Font.Size; ffsize = linkDurationUnit.Font.Size;

            // Apply scale factor
            this.Paint += (o, e) => StixUtils.PaintStix(this, scaleFactor, e);
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);
        }

        /// <summary>
        /// Save Quick Task Remove Defaults
        /// </summary>
        private void RemoveDefaults_Click(object sender, EventArgs e)
        {
            string defaults = "dates:" + ((TaskInfoDates as ToolStripMenuItem).Checked ? "1" : "0") + ";";
            defaults += "priority:" + ((TaskInfoPriority as ToolStripMenuItem).Checked ? "1" : "0") + ";";
            defaults += "progress:" + ((TaskInfoProgress as ToolStripMenuItem).Checked ? "1" : "0") + ";";
            defaults += "resources:" + ((TaskInfoResources as ToolStripMenuItem).Checked ? "1" : "0") + ";";
            defaults += "effort:" + ((TaskInfoEffort as ToolStripMenuItem).Checked ? "1" : "0");
            Utils.setRegistry("QuickTaskRemoveDefaults", defaults);
        }

        ToolStripItem TaskInfoDates, TaskInfoProgress, TaskInfoPriority, TaskInfoResources, TaskInfoEffort;

        private void StixTaskInfo_Load(object sender, EventArgs e)
        {
            StixStarting = true;
            StixMain.SetDates();
            StixStarting = false;
        }

        public void SetQuickTaskDefault()
        {
            bool dates = true, progress = true, priority = false, resources = false, effort = false;
            string qtr_defaults = Utils.getRegistry("QuickTaskRemoveDefaults", "");
            if (qtr_defaults != "")
            {
                string[] parts = qtr_defaults.Split(';');
                foreach (string part in parts)
                {
                    string[] parts2 = part.Split(':');
                    switch (parts2[0])
                    {
                        case "dates": dates = parts2[1] == "1" ? true : false; break;
                        case "progress": progress = parts2[1] == "1" ? true : false; break;
                        case "priority": priority = parts2[1] == "1" ? true : false; break;
                        case "resources": resources = parts2[1] == "1" ? true : false; break;
                        case "effort": effort = parts2[1] == "1" ? true : false; break;
                    }
                }
            }

            (TaskInfoDates as ToolStripMenuItem).Checked = dates;
            (TaskInfoProgress as ToolStripMenuItem).Checked = progress;
            (TaskInfoPriority as ToolStripMenuItem).Checked = priority;
            (TaskInfoResources as ToolStripMenuItem).Checked = resources;
            (TaskInfoEffort as ToolStripMenuItem).Checked = effort;
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            scaleFactor = toScale;
            if (StixUtils.ScaleStick(this, fromScale, toScale)) return;

            float _fsize = fsize * (toScale / 100);
            float _ffsize = ffsize * (toScale / 100);

            pStartDate.Font = new Font(pStartDate.Font.FontFamily, _fsize);
            pDueDate.Font = new Font(pStartDate.Font.FontFamily, _fsize);
            numDuration.Font = new Font(numDuration.Font.FontFamily, _fsize);
            linkDurationUnit.Font = new Font(numDuration.Font.FontFamily, _ffsize);
            numEffort.Font = new Font(numEffort.Font.FontFamily, _fsize);
            linkEffortUnit.Font = new Font(numEffort.Font.FontFamily, _ffsize);
        }
        float fsize, ffsize;

        private void CmsRemoveTaskInfo_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
        }

        public void PopulateResources()
        {
            if (MMUtils.ActiveDocument == null) return;

            cmsResources.Items.Clear();
            ToolStripItem tsi;

            // Resources from current map
            MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
            foreach (MapMarker mm in mg)
            {
                string color = "#" + mm.Color.Value.ToString("X");
                if (color == "#0") color = "";

                tsi = cmsResources.Items.Add(mm.Label);
                tsi.Name = "cm_resource";

                if (color != "")
                {
                    Color c = ColorTranslator.FromHtml(color);
                    tsi.BackColor = c;
                    int cc = (int)Math.Sqrt(c.R * c.R * .299 + c.G * c.G * .587 + c.B * c.B * .114);
                    if (cc > 130) tsi.ForeColor = SystemColors.WindowText;
                    else tsi.ForeColor = SystemColors.Window;
                }
            }

            ToolStripTextBox mtb = new ToolStripTextBox();
            mtb.Size = new Size(panelDueDate.Width * 4, mtb.Height);
            mtb.BorderStyle = BorderStyle.FixedSingle;
            mtb.Text = Utils.getString("ResourcesDlg.dummytext");
            mtb.ForeColor = SystemColors.GrayText;
            //mtb.ToolTipText = Utils.getString("taskinfo.newresource.tooltip");
            mtb.KeyDown += Mtb_KeyDown;
            mtb.GotFocus += Mtb_GotFocus;
            mtb.LostFocus += Mtb_LostFocus;
            cmsResources.Items.Add(mtb);

            tsi = new ToolStripLabel("");
            tsi.Font = new Font("Arial", 1);
            cmsResources.Items.Add(tsi);
            cmsResources.Items.Add(new ToolStripSeparator());

            if (!Utils.IsFree())
            {
                // Resources from database
                using (StixDB db = new StixDB("Resources"))
                {

                    tsi = cmsResources.Items.Add(Utils.getString("ResourcesDlg.allresources"));
                    ToolStripDropDown dd = (tsi as ToolStripMenuItem).DropDown;
                    (dd as ToolStripDropDownMenu).ShowImageMargin = false;
                    dd.ItemClicked += ContextMenu_ItemClicked;

                    tsi.Font = new Font(tsi.Font, FontStyle.Italic);

                    DataTable dt = db.ExecuteQuery("select * from RESOURCES order by name");

                    List<string> resources = new List<string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (resources.Contains(dr["name"].ToString())) continue;

                        resources.Add(dr["name"].ToString());
                        var res = dd.Items.Add(dr["name"].ToString());
                        res.Font = new Font(tsi.Font, FontStyle.Regular);
                        res.Name = "cm_resource"; res.Tag = dr["color"].ToString();

                        if (res.Tag.ToString() != "")
                            res.BackColor = ColorTranslator.FromHtml(res.Tag.ToString());
                    }

                    // Fill Resource Groups
                    dt = db.ExecuteQuery("select * from RESOURCEGROUPS order by name");
                    foreach (DataRow dr in dt.Rows)
                    {
                        tsi = cmsResources.Items.Add(dr["name"].ToString());
                        dd = (tsi as ToolStripMenuItem).DropDown;
                        (dd as ToolStripDropDownMenu).ShowImageMargin = false;
                        tsi.Font = new Font(tsi.Font, FontStyle.Italic);
                        dd.ItemClicked += ContextMenu_ItemClicked;

                        DataTable _dt = db.ExecuteQuery("select * from RESOURCES " +
                        "where groupID=" + Convert.ToInt32(dr["id"]) + " order by name");

                        foreach (DataRow _dr in _dt.Rows)
                        {
                            var res = dd.Items.Add(_dr["name"].ToString());
                            res.Font = new Font(tsi.Font, FontStyle.Regular);
                            res.Name = "cm_resource"; res.Tag = _dr["color"].ToString();
                            if (res.Tag.ToString() != "")
                                res.BackColor = ColorTranslator.FromHtml(res.Tag.ToString());
                        }
                    }
                }
            }
            cmsResources.Items.Add(new ToolStripSeparator());

            tsi = cmsResources.Items.Add(Utils.getString("taskinfo.resources.delete"));
            tsi.ToolTipText = Utils.getString("taskinfo.resources.delete.tooltip");
            tsi.Name = "RemoveResources";
            StixUtils.SetContextMenuImage(tsi, "deleteall.png");

            tsi = cmsResources.Items.Add(Utils.getString("Box.Resources"));
            tsi.Name = "ResourceBox";
            StixUtils.SetContextMenuImage(tsi, "resources.png");

            //tsi = new ToolStripLabel(Utils.getString("taskinfo.MapResources"));
            //tsi.Font = new Font(tsi.Font, FontStyle.Bold);
            //cmsResources.Items.Add(tsi);
        }

        private void Mtb_LostFocus(object sender, EventArgs e)
        {
            ToolStripTextBox tb = sender as ToolStripTextBox;
            tb.Text = ""; tb.Text = Utils.getString("ResourcesDlg.dummytext");
            tb.ForeColor = SystemColors.GrayText;
        }

        private void Mtb_GotFocus(object sender, EventArgs e)
        {
            ToolStripTextBox tb = sender as ToolStripTextBox;

            if (tb.Text == Utils.getString("ResourcesDlg.dummytext"))
            {
                tb.Text = ""; tb.ForeColor = SystemColors.WindowText;
            }
        }

        private void Mtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MMUtils.ActiveDocument == null) return;

                ToolStripTextBox tb = sender as ToolStripTextBox;

                string _resources = tb.Text.Trim();
                if (_resources == "") return;

                string[] resources = _resources.Split(',').Select(x => x.Trim()).ToArray();

                // Add to topics
                if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
                {
                    // Assign resource(s) to topic(s)
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        string[] topicResources = t.Task.Resources.Split(',').Select(x => x.Trim()).ToArray();
                        string[] newResources = topicResources.Union(resources).ToArray();

                        string result = "";
                        foreach (string res in newResources)
                            result += res + ",";
                        result = result.TrimEnd(',');

                        t.Task.Resources = result;
                    }
                }

                // Add to Map Index
                MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                foreach (string res in resources)
                {
                    bool found = false;
                    foreach (MapMarker mm in mg)
                        if (mm.Label == res) found = true;

                    if (!found)
                        mg.AddResourceMarker(res);
                }

                tb.Text = ""; tb.Text = Utils.getString("ResourcesDlg.dummytext");
                tb.ForeColor = SystemColors.GrayText;
                PopulateResources();

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        public void PopulateQuickTopics(bool fromQuicTopics = false)
        {
            cmsTaskTemplates.Items.Clear();
            ToolStripItem tsi;

            using (StixDB db = new StixDB("QuickTopics"))
            {
                // Get groups
                DataTable dt = db.ExecuteQuery("select * from QUICKTOPICGROUPS order by _order");
                Dictionary<int, string> groups = new Dictionary<int, string>();
                foreach (DataRow row in dt.Rows)
                    groups.Add(Convert.ToInt32(row["id"]), row["name"].ToString());

                if (StixMain.m_QuickTopics != null && StixMain.m_QuickTopics.Visible && !fromQuicTopics)
                    StixMain.m_QuickTopics.treeView1.Nodes.Clear();

                int i = 1;
                foreach (var group in groups)
                {
                    ToolStripDropDown dd = null;
                    TreeNode groupNode = null;

                    if (StixMain.m_QuickTopics != null && StixMain.m_QuickTopics.Visible && !fromQuicTopics)
                    {
                        groupNode = StixMain.m_QuickTopics.treeView1.Nodes.Add(group.Value);
                        groupNode.Tag = group.Key; groupNode.Name = "QTGroup";
                    }

                    if (group.Key > 1) // Not Favorites
                    {
                        tsi = cmsTaskTemplates.Items.Add(group.Value);
                        tsi.Tag = group.Key;
                        dd = (tsi as ToolStripMenuItem).DropDown;
                        (dd as ToolStripDropDownMenu).ShowImageMargin = false;
                        dd.ItemClicked += ContextMenu_ItemClicked;
                    }

                    dt = db.ExecuteQuery("select * from QUICKTOPICTEMPLATES " +
                        "where groupID=" + group.Key + " order by _order");

                    foreach (DataRow row in dt.Rows)
                    {
                        string topictextState = "", progressState = "", priorityState = "", startdateState = "",
                        duedateState = "", durationState = "", effortState = "", resourcesState = "", iconState = "", tagsState = "";

                        string topictext = row["topictext"].ToString();
                        string[] parts = topictext.Split(new string[] { "$$$" }, StringSplitOptions.None);
                        if (parts.Length > 1)
                        {
                            topictextState = parts[0]; topictext = parts[1];
                        }

                        string progress = row["progress"].ToString(); int _progress = -1;
                        parts = progress.Split(':');
                        if (parts.Length > 1)
                        {
                            progressState = parts[0]; _progress = Convert.ToInt32(parts[1]);
                        }

                        string priority = row["priority"].ToString(); int _priority = 0;
                        parts = priority.Split(':');
                        if (parts.Length > 1)
                        {
                            priorityState = parts[0]; _priority = Convert.ToInt32(parts[1]);
                        }

                        string dates = row["dates"].ToString();
                        parts = dates.Split(new string[] { "$$$" }, StringSplitOptions.None);
                        if (parts.Length > 1)
                        {
                            string[] states = parts[0].Split(':');
                            startdateState = states[0]; duedateState = states[1]; dates = parts[1];
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
                        if (parts.Length > 1) { iconState = parts[0]; icons = parts[1]; }

                        string resources = row["resources"].ToString(); parts = resources.Split(':');
                        if (parts.Length > 1) { resourcesState = parts[0]; resources = parts[1]; }

                        string tags = row["tags"].ToString(); parts = tags.Split(new[] { ';' }, 2);
                        if (parts.Length > 1) { tagsState = parts[0]; tags = parts[1]; }

                        QuickTopicItem item = new QuickTopicItem(row["name"].ToString(), Convert.ToInt32(row["id"]), Convert.ToInt32(row["groupID"]),
                            Convert.ToInt32(row["_order"]), topictext, _progress, _priority, dates, duration, effort, icons,
                            resources, tags, topictextState, progressState, priorityState, startdateState,
                            duedateState, durationState, effortState, iconState, resourcesState, tagsState);

                        if (group.Key == 1) // Favorites
                        {
                            tsi = cmsTaskTemplates.Items.Add(item.Name);
                            tsi.Tag = item; tsi.Name = "TaskTemplate";

                            if (item.Order == 1) QuickTopic = item;
                        }
                        else // Other group
                        {
                            tsi = dd.Items.Add(item.Name);
                            tsi.Tag = item; tsi.Name = "TaskTemplate";
                        }

                        if (Utils.IsFree() && i++ > 1)
                        {
                            tsi.Enabled = false;
                            tsi.ForeColor = SystemColors.ControlDark;
                            tsi.ToolTipText = Utils.getString("FreeVersionLimitation") + Utils.getString("limitation.endrestriction");
                        }

                        if (StixMain.m_QuickTopics != null && StixMain.m_QuickTopics.Visible && !fromQuicTopics)
                            groupNode.Nodes.Add(item.Name).Tag = item;
                    }
                }
            }

            cmsTaskTemplates.Items.Add(new ToolStripSeparator());

            tsi = cmsTaskTemplates.Items.Add(Utils.getString("taskinfo.quicktask.manage"));
            tsi.Name = "ManageTaskTemplates";
            StixUtils.SetContextMenuImage(tsi, "manage.png");

            tsi = cmsTaskTemplates.Items.Add(Utils.getString("taskinfo.quicktask.qtaskslist"));
            tsi.Name = "QuickTaskList";
            StixUtils.SetContextMenuImage(tsi, "list.png");
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
            if (e.ClickedItem.Name == "ResourceBox")
            {
                if (Utils.FreeVersionLimitExceeded(StixUtils.typetaskinfo))
                    return;

                if (StixMain.m_Resources == null)
                    StixMain.m_Resources = new ResourcesDlg();

                if (StixMain.m_Resources.Visible)
                    StixMain.m_Resources.Hide();
                else
                {
                    StixMain.m_Resources.InitCurrentMapResources();

                    if (StixMain.m_Resources.Location.IsEmpty)
                    {
                        StixMain.m_Resources.Location =
                        StixUtils.GetChildLocation(this, StixMain.m_Resources.Bounds, orientation, "resources");
                    }
                    StixMain.m_Resources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
            }
            else if (e.ClickedItem.Name == "RemoveResources")
            {
                if (Utils.ActiveDocumentOrSelectionNull()) return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Task.Resources = "";
                }
            }
            else if (e.ClickedItem.Name == "cm_resource")
            {
                if (Utils.ActiveDocumentOrSelectionNull()) return;

                string res = e.ClickedItem.Text;

                // Check if resource exists in the map.
                MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                bool found = false;
                foreach (MapMarker _res in mg)
                    if (_res.Label == res) { found = true; break; }

                // Resource not found, add it to the map.
                if (!found)
                {
                    MapMarker mmm = mg.AddResourceMarker(res);
                    // Set color
                    if (e.ClickedItem.Tag != null && e.ClickedItem.Tag.ToString() != "")
                    {
                        try
                        {
                            int i = int.Parse(e.ClickedItem.Tag.ToString().Substring(1), System.Globalization.NumberStyles.HexNumber);
                            mmm.Color.SetValue(i);
                        }
                        catch { }
                    }
                }

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    string[] topicResources = t.Task.Resources.Split(',').Select(x => x.Trim()).ToArray();

                    if (topicResources.Contains(res)) continue;

                    if (t.Task.Resources == "")
                        t.Task.Resources = res;
                    else
                        t.Task.Resources += "," + res;
                }
            }
            else if (e.ClickedItem.Name == "TaskTemplate")
            {
                if (MMUtils.ActiveDocument == null) return;

                QuickTopic = e.ClickedItem.Tag as QuickTopicItem;

                Transaction _tr = MMUtils.ActiveDocument.NewTransaction(Utils.getString("QuickTask.transaction.name"));
                _tr.IsUndoable = true;
                _tr.Execute += new ITransactionEvents_ExecuteEventHandler(SetQuickTopic);
                _tr.Start();
            }
            else if (e.ClickedItem.Name == "ManageTaskTemplates")
            {
                using (TaskTemplateDlg dlg = new TaskTemplateDlg())
                    dlg.ShowDialog();

                PopulateQuickTopics();
            }
            else if (e.ClickedItem.Name == "QuickTaskList")
            {
                if (Utils.FreeVersionLimitExceeded(StixUtils.typetaskinfo))
                    return;

                if (StixMain.m_QuickTopics == null || StixMain.m_QuickTopics.IsDisposed)
                {
                    StixMain.m_QuickTopics = new QuickTopicsDlg();
                    StixMain.m_QuickTopics.Location = StixUtils.GetChildLocation(this, StixMain.m_QuickTopics.Bounds, orientation);
                    StixMain.m_QuickTopics.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
                PopulateQuickTopics();
                // Expand Favorites
                StixMain.m_QuickTopics.treeView1.Nodes[0].Expand();
            }
            else if (e.ClickedItem.Name.StartsWith("Dates_"))
            {
                if (Utils.ActiveDocumentOrSelectionNull()) return;

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
                StixMain.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "TaskInfoStix.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typetaskinfo, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation);

            panelStartDate.Location = new Point(panelStartDate.Location.Y, panelStartDate.Location.X);
            panelDueDate.Location = new Point(panelDueDate.Location.Y, panelDueDate.Location.X);
            panelDuration.Location = new Point(panelDuration.Location.Y, panelDuration.Location.X);
            panelEffort.Location = new Point(panelEffort.Location.Y, panelEffort.Location.X);
        }

        private void p100_Click(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

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
                if (Utils.ActiveDocumentOrSelectionNull()) return;

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
                StixUtils.ShowCommandPopup(this, orientation, StixUtils.typetaskinfo, "progress", scaleFactor);
            }
        }

        private void pPriority_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Utils.ActiveDocumentOrSelectionNull()) return;

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
                StixUtils.ShowCommandPopup(this, orientation, StixUtils.typetaskinfo, "priority", scaleFactor);
            }
        }

        private void DateBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string date = "calendar_startdate"; DateTime dt = (DateTime)pStartDate.Tag;
                if (sender as MaskedTextBox == pDueDate) 
                { date = "calendar_duedate"; dt = (DateTime)pDueDate.Tag; }

                MyDateTimePicker mdtp = new MyDateTimePicker(scaleFactor);
                mdtp.Location = StixUtils.GetChildLocation(this, mdtp.Bounds, orientation, date);
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

            if (Utils.ActiveDocumentOrSelectionNull()) return;

            string fromstick = pStartDate.Text;
            DateTime dt = (DateTime)pStartDate.Tag;
            if (pb.Name == "pDueDateToggle")
                dt = (DateTime)pDueDate.Tag;

            if (dt == null) return;

            bool setdate = !(bool)pb.Tag;

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (setdate) // set date to topic
                {
                    if (pb.Name == "pStartDateToggle")
                    {
                        t.Task.StartDate = dt;
                        pStartDateToggle.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");

                    }
                    else
                    {
                        t.Task.DueDate = dt;
                        pDueDateToggle.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");

                    }
                }
                else // remove date from topic
                {
                    if (pb.Name == "pStartDateToggle")
                    {
                        t.Task.StartDate = MMUtils.NULLDATE;
                        pStartDateToggle.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");

                    }
                    else
                    {
                        t.Task.DueDate = MMUtils.NULLDATE;
                        pDueDateToggle.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");

                    }
                }
            }
        }

        #region Duration&Effort

        private void ST_DurationUnits_MouseEnter(object sender, EventArgs e)
        {
            (sender as ToolStripComboBox).DroppedDown = true;
        }

        // User selecting duration/effort unit
        private void ST_DurationUnits_DropDown(object sender, EventArgs e)
        {
            ToolStripComboBox cb = sender as ToolStripComboBox;
            selectedUnit = cb.SelectedIndex;
        }

        // User selected duration/effort unit
        private void ST_DurationUnits_DropDownClosed(object sender, EventArgs e)
        {
            ToolStripComboBox cb = sender as ToolStripComboBox;
            if (cb.SelectedIndex != selectedUnit)
                stickDuration = true;
        }
        int selectedUnit;

        private void ST_DurationUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = (sender as ToolStripComboBox).SelectedIndex;
            LinkLabel ll;

            if ((sender as ToolStripComboBox) == ST_DurationUnits)
                ll = linkDurationUnit;
            else
                ll = linkEffortUnit;

            switch (i)
            {
                case 0:
                    ll.Text = Utils.getString("task.durationunit.minute"); break;
                case 1:
                    ll.Text = Utils.getString("task.durationunit.hour"); break;
                case 2:
                    ll.Text = Utils.getString("task.durationunit.day"); break;
                case 3:
                    ll.Text = Utils.getString("task.durationunit.week"); break;
                case 4:
                    ll.Text = Utils.getString("task.durationunit.month"); break;
            }

            StixUtils.ActivateMindManager(); // cmsDuration.Hide() locks MindManager

            if (StixStarting) return;

            if ((sender as ToolStripComboBox).DroppedDown)
            {
                stickDuration = true;

                if ((sender as ToolStripComboBox) == ST_DurationUnits)
                    numDuration_ValueChanged(numDuration, null);
                else if (numEffort.Value > 0)
                    numDuration_ValueChanged(numEffort, null);
            }
        }

        /// <summary>
        /// Show the Duration unit combobox
        /// </summary>
        private void linkDurationUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ST_DurationUnits.Visible = true;
            ST_EffortUnits.Visible = false;

            Rectangle rec = linkDurationUnit.RectangleToScreen(linkDurationUnit.ClientRectangle);
            cmsDuration.Show(rec.X, rec.Bottom);
        }

        private void linkEffortUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ST_EffortUnits.Visible = true;
            ST_DurationUnits.Visible = false;

            Rectangle rec = linkEffortUnit.RectangleToScreen(linkEffortUnit.ClientRectangle);
            cmsDuration.Show(rec.X, rec.Bottom);
        }

        private void numDuration_MouseDown(object sender, MouseEventArgs e)
        {
            durationValueBefore = numDuration.Value;
        }
        decimal durationValueBefore;
        private void numEffort_MouseDown(object sender, MouseEventArgs e)
        {
            effortValueBefore = numEffort.Value;
        }
        decimal effortValueBefore;

        private void numDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((sender as NumericUpDown) == numEffort)
                {
                    if (numEffort.Value == 0) numEffort.Text = "";
                    if (numEffort.Text == "") { numEffort.Value = 0; numEffort.Text = ""; }

                    if (numEffort.Value == effortValueBefore)
                        return;
                }
                else
                {
                    if (numDuration.Value == durationValueBefore)
                        return;
                }

                stickDuration = true;
                numDuration_ValueChanged(sender, e);

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
       }

        private void btnSetDuration_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            NumericUpDown num = numDuration;

            if (pb == btnSetEffort)
            {
                num = numEffort;

                if (numEffort.Value == 0) numEffort.Text = "";
                if (numEffort.Text == "") { numEffort.Value = 0; numEffort.Text = ""; }
            }

            stickDuration = true;
            numDuration_ValueChanged(num, e);
        }

        private void numDuration_ValueChanged(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            if ((sender as NumericUpDown) == numDuration)
            {
                if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 1 && !stickDuration)
                    return; // Duration for multiple topics = 0

                if (MMUtils.ActiveDocument.Selection.PrimaryTopic != null)
                {
                    stickDuration = true; // do not set numDuration value in Bubbles.onObjectChanged event

                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        int effort = 0;
                        if (t.Task.HasEffort)
                            effort = t.Task.GetEffort(t.Task.EffortUnit);

                        int duration = t.Task.GetDuration(t.Task.DurationUnit);

                        int i = ST_DurationUnits.SelectedIndex;
                        MmDurationUnit unit = GetDurationUnit(i);
                        if (t.Task.DurationUnit != unit || duration != (int)numDuration.Value)
                        {
                            t.Task.DurationUnit = unit;
                            t.Task.SetDuration(unit, (int)numDuration.Value); // raises onObjectChanged event
                        }

                        if (t.Task.HasEffort && duration == effort)
                        {
                            t.Task.EffortUnit = unit;
                            t.Task.SetEffort(unit, (int)numDuration.Value);

                            StixMain.SetTaskInfoEffortUnit(t);
                            numEffort.Value = (int)numDuration.Value;
                        }
                    }
                    // onObjectChanged event raised now
                    stickDuration = false; // to continue process numDuration value changing
                }
            }
            else // numEffort
            {
                if (!stickDuration) return;

                if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 1 && !stickDuration)
                    return; // Duration for multiple topics = 0

                if (MMUtils.ActiveDocument == null) return;

                List<Topic> topics = MMUtils.ActiveDocument.Selection.OfType<Topic>().ToList();

                if (topics.Count > 0)
                {
                    stickDuration = true; // do not set numDuration value in Bubbles.onObjectChanged event
                    foreach (Topic t in topics)
                    {
                        if ((numEffort.Value == 0 || numEffort.Text == "") && t.Task.HasEffort)
                        {
                            Utils.DeleteEffort(t);
                            MMUtils.ActiveDocument.Selection.Add(t);
                        }
                        else
                        {
                            int i = ST_EffortUnits.SelectedIndex;
                            MmDurationUnit unit = GetDurationUnit(i);
                            t.Task.EffortUnit = unit;
                            t.Task.SetEffort(unit, (int)numEffort.Value); // raises onObjectChanged event
                        }
                    }
                    // onObjectChanged event raised now
                    stickDuration = false; // to continue process numDuration value changing
                }
            }
        }

        /// <summary>True - Duration was set by stick </summary>
        public bool stickDuration = false;
        /// <summary>True - Duration is set when starting Stix</summary>
        public bool StixStarting = true;

        #endregion

        private void pRemoveTaskInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Utils.ActiveDocumentOrSelectionNull()) return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if ((TaskInfoDates as ToolStripMenuItem).Checked) {
                        t.Task.StartDate = MMUtils.NULLDATE; t.Task.DueDate = MMUtils.NULLDATE; }

                    if ((TaskInfoProgress as ToolStripMenuItem).Checked)
                        t.Task.Complete = -1;

                    if ((TaskInfoPriority as ToolStripMenuItem).Checked)
                        t.Task.Priority = 0;

                    if ((TaskInfoResources as ToolStripMenuItem).Checked)
                        t.Task.Resources = "";

                    if ((TaskInfoEffort as ToolStripMenuItem).Checked &&
                        t.Task.HasEffort) Utils.DeleteEffort(t);
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
            if (MMUtils.ActiveDocument == null) return;

            if (e.Button == MouseButtons.Left)
            {
                Transaction _tr = MMUtils.ActiveDocument.NewTransaction(Utils.getString("QuickTask.transaction.name"));
                _tr.IsUndoable = true;
                _tr.Execute += new ITransactionEvents_ExecuteEventHandler(SetQuickTopic);
                _tr.Start();
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTaskTemplates.Items)
                    item.Visible = true;

                cmsTaskTemplates.Show(Cursor.Position);
            }
        }

        public void SetQuickTopic(Document pDocument)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            string startdate = "", duedate = "";
            if (QuickTopic.Dates != "")
            {
                string[] dates = QuickTopic.Dates.Split(';');
                startdate = dates[0]; duedate = dates[1];
            }

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (QuickTopic.TopicTextState != "")
                    t.Text = QuickTopic.TopicText;

                if (QuickTopic.ProgressState != "")
                    t.Task.Complete = QuickTopic.Progress;

                if (QuickTopic.PriorityState != "")
                    t.Task.Priority = GetPriority(QuickTopic.Priority);

                DateTime? dtstart = Utils.GetDate(startdate);
                DateTime? dtdue = Utils.GetDate(duedate);
                if (QuickTopic.StartDateState != "")
                    t.Task.StartDate = (DateTime)dtstart;
                if (QuickTopic.DueDateState != "")
                    t.Task.DueDate = (DateTime)dtdue;

                if (QuickTopic.DurationState != "")
                {
                    string[] parts = QuickTopic.Duration.Split(':');
                    try
                    {
                        t.Task.DurationUnit = GetDurationUnit(Convert.ToInt32(parts[1]));
                        t.Task.SetDuration(t.Task.DurationUnit, Convert.ToInt32(parts[0]));
                    }
                    catch { }
                }

                if (QuickTopic.EffortState != "")
                {
                    if (QuickTopic.ResourcesState.Contains("red"))
                        Utils.DeleteEffort(t);
                    else
                    {
                        string[] parts = QuickTopic.Effort.Split(':');
                        try
                        {
                            t.Task.EffortUnit = GetDurationUnit(Convert.ToInt32(parts[1]));
                            t.Task.SetEffort(t.Task.EffortUnit, Convert.ToInt32(parts[0]));
                        }
                        catch { }
                    }
                }

                if (QuickTopic.ResourcesState != "")
                {
                    if (QuickTopic.ResourcesState.Contains("red"))
                        t.Task.Resources = QuickTopic.Resources;
                    else
                    {
                        string[] topicResources = t.Task.Resources.Split(',').Select(x => x.Trim()).ToArray();
                        string[] resources = QuickTopic.Resources.Split(',').Select(x => x.Trim()).ToArray();
                        string[] newResources = topicResources.Union(resources).ToArray();

                        string result = "";
                        foreach (string res in newResources) result += ", " + res;

                        t.Task.Resources = result.TrimStart(',');
                    }
                }

                if (QuickTopic.IconState != "")
                {
                    if (QuickTopic.IconState.Contains("red") && t.UserIcons.Count > 0)
                        t.UserIcons.RemoveAll();

                    if (QuickTopic.IconState.StartsWith("checked"))
                    {
                        string[] icons = QuickTopic.aIcon.Split(';');

                        foreach (string icon in icons) SetIcon(icon, t);
                    }
                }

                if (QuickTopic.TagsState != "")
                {
                    if (QuickTopic.TagsState.Contains("red") && t.TextLabels.Count > 0)
                        t.TextLabels.RemoveAll();

                    if (QuickTopic.TagsState.StartsWith("checked"))
                    {
                        string[] tags = QuickTopic.Tags.Split(';'); // tag1;tag2
                        string[] tag1 = tags[0].Split(':'); // group:tag or tag without group
                        string tagname = tag1.Length == 1 ? tag1[0] : tag1[1];
                        string groupname = tag1.Length == 1 ? "" : tag1[0];

                        if (tagname != "")
                            MapMarkers.AddTagToTopic(t, tagname, "", groupname, "");

                        if (tags.Length > 1)
                        {
                            string[] tag2 = tags[1].Split(':');
                            tagname = tag2.Length == 1 ? tag2[0] : tag2[1];
                            groupname = tag2.Length == 1 ? "" : tag2[0];

                            if (tagname != "")
                                MapMarkers.AddTagToTopic(t, tagname, "", groupname, "");
                        }
                    }
                }
            }
        }

        void SetIcon(string fileName, Topic t)
        {
            if (fileName.StartsWith("stock"))
            {
                MmStockIcon icon = StixIcons.StockIconFromString(fileName);
                if (icon != 0)
                {
                    MapMarkers.GetIcon(icon, "", "", "");

                    if (!t.AllIcons.ContainsStockIcon(icon))
                        t.AllIcons.AddStockIcon(icon);
                }
            }
            else
            {
                // filename is the icon signature
                string path = Utils.CustomIcons[fileName];
                if (System.IO.File.Exists(path))
                {
                    MapMarkers.GetIcon(0, fileName, "", path);

                    if (!t.AllIcons.ContainsCustomIcon(fileName))
                        t.AllIcons.AddCustomIconFromMap(fileName);
                }
            }
        }

        /// <summary>
        /// Get MmDurationUnit. From Stix or from Quick Task
        /// </summary>
        /// <param name="duration">Get duration unit. False - effort unit</param>
        /// <param name="i">From Quick Task</param>
        /// <returns></returns>
        public static MmDurationUnit GetDurationUnit(int i)
        {
            switch (i)
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
                PopulateResources();

                foreach (ToolStripItem item in cmsResources.Items)
                    item.Visible = true;

                cmsResources.Show(Cursor.Position);
            }
            else if (e.Button == MouseButtons.Right)
            {
                
            }
        }

        string orientation = "H";

        public QuickTopicItem QuickTopic = null;
        string daterightclick = "";

        public float scaleFactor = 100;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public class QuickTopicGroup
    {
        public QuickTopicGroup(int id, string name, int order) 
        {
            ID = id;
            Name = name;
            Order = order;
        }

        public int ID;
        public string Name = "";
        public int Order;

        public override string ToString()
        {
            return Name;
        }
    }

    public class QuickTopicItem
    {
        public QuickTopicItem(string name, int id, int groupID, int order, string topicText, int progress, int priority,
            string dates, string duration, string effort, string icon, string resources, string tags,
            string topicTextState = "", string progressState = "", string priorityState = "", 
            string startDateState = "", string dueDateState = "", string durationState = "", string effortState = "",
            string iconState = "", string resourcesState = "", string tagsState = "")
        {
            Name = name;
            ID = id;
            GroupID = groupID;
            Order = order;
            TopicText = topicText;
            Progress = progress;
            Priority = priority;
            Dates = dates;
            Duration = duration;
            Effort = effort;
            aIcon = icon;
            Resources = resources;
            Tags = tags;

            TopicTextState = topicTextState; ProgressState = progressState; PriorityState = priorityState;
            StartDateState = startDateState; DueDateState = dueDateState; DurationState = durationState; EffortState = effortState;
            IconState = iconState; ResourcesState = resourcesState; TagsState = tagsState;
        }

        public string TopicText = "";
        public int ID;
        public int GroupID = 1;
        public int Order = 0;
        public string Name = "";
        public int Progress = 0;
        public int Priority = 0;
        public string Dates = "";
        public string Duration = "";
        public string Effort = "";
        public string aIcon = "";
        public string Resources = "";
        public string Tags = "";

        public string TopicTextState = "";
        public string ProgressState = "";
        public string PriorityState = "";
        public string StartDateState = "";
        public string DueDateState = "";
        public string DurationState = "";
        public string EffortState = "";
        public string IconState = "";
        public string ResourcesState = "";
        public string TagsState = "";

        public override string ToString()
        {
            return Name;
        }
    }
}
