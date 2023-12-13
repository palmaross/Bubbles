using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Cursor = System.Windows.Forms.Cursor;
using PRMapCompanion;

namespace Bubbles
{
    internal partial class BubbleTaskInfo : Form
    {
        public BubbleTaskInfo(int ID, string _orientation, string stickname = "")
        {
            InitializeComponent();

            BubblesButton.m_TaskInfo = this;
            DocumentStorage.Sync(MMUtils.ActiveDocument);

            this.Tag = ID; // correct
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TaskInfoStick.htm");

            toolTip1.SetToolTip(pictureHandle, stickname);
            toolTip1.SetToolTip(pProgress, Utils.getString("taskinfo.pProgress.tooltip"));
            toolTip1.SetToolTip(pPriority, Utils.getString("taskinfo.pPriority.tooltip"));
            toolTip1.SetToolTip(numDuration, Utils.getString("taskinfo.numDuration.tooltip"));
            toolTip1.SetToolTip(linkDurationUnit, Utils.getString("taskinfo.lblDurUnit.tooltip"));
            toolTip1.SetToolTip(pQuickTask, Utils.getString("taskinfo.pQuickTask.tooltip"));
            toolTip1.SetToolTip(pRemoveTaskInfo, Utils.getString("taskinfo.pRemoveTaskInfo.tooltip"));

            RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menus
            cmsCommon.ItemClicked += cmsCommon_ItemClicked;

            cmsCommon.Items["ST_deletetaskinfo"].Text = Utils.getString("taskinfo.contextmenu.deletetaskinfo");
            cmsCommon.Items["ST_deletetaskinfo"].ToolTipText = Utils.getString("taskinfo.contextmenu.deletetaskinfo.tooltip");
            StickUtils.SetContextMenuImage(cmsCommon.Items["ST_deletetaskinfo"], "removetaskinfo.png");

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

            pDueDate.Text = DateTime.Now.Date.ToString("dd, MM").Replace(", ", "/");
            pDueDate.Tag = DateTime.Now.Date.AddHours(8);

            BubblesButton.SetDates();
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
            if (e.ClickedItem.Name == "ST_deletetaskinfo") // delete all Task Info from topic
            {
                
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                BubblesButton.m_TaskInfo = null;
                DocumentStorage.Sync(MMUtils.ActiveDocument, false);
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

                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;
            }
        }


        private void p100_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                t.Task.Complete = 100;
        }

        private void pProgress_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                    return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Task.Complete = 0;
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

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Task.Priority = MmTaskPriority.mmTaskPriority1;
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
            else if (e.Button == MouseButtons.Right)
            {

            }
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

            //DateTime? dt = GetDate(fromstick);

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
                else
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

            cmsDuration.Hide();
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
                // onObjectChanged events processed
                stickDuration = false; // continue process set numDuration value
            }
        }
        /// <summary>True - Duration was set by stick </summary>
        public bool stickDuration = false;
        /// <summary>True - Duration was set by MM </summary>
        public bool MMDuration = false;
        /// <summary>True - Start Date was set by stick </summary>
        public bool stickStartDate = false;
        /// <summary>True - Start Date was set by MM </summary>
        public bool MMStartDate = false;
        /// <summary>True - Due Date was set by stick </summary>
        public bool stickDueDate = false;
        /// <summary>True - Due Date was set by MM </summary>
        public bool MMDueDate = false;

        private void pRemoveTaskInfo_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null && MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
            {
                DateTime dt = DateTime.Now.Date;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Task.StartDate = MMUtils.NULLDATE; t.Task.DueDate = MMUtils.NULLDATE;
                    t.Task.Complete = -1;
                }
            }
        }

        private void pQuickTask_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument != null && MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
                {
                    DateTime dt = DateTime.Now.Date.AddHours(8);

                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        t.Task.StartDate = dt; t.Task.DueDate = dt;
                        t.Task.Complete = 0;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {

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

        private MmTaskPriority GetPriority(int value)
        {
            switch (value)
            {
                case 1: return MmTaskPriority.mmTaskPriority1;
                case 2: return MmTaskPriority.mmTaskPriority2;
                case 3: return MmTaskPriority.mmTaskPriority3;
                case 4: return MmTaskPriority.mmTaskPriority4;
                case 5: return MmTaskPriority.mmTaskPriority5;
            }
            return 0;
        }

        private void pResources_MouseClick(object sender, MouseEventArgs e)
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

        string orientation = "H";
        bool collapsed = false;

        int RealLength;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
