using System;
using System.Runtime.InteropServices;
using Mindjet.MindManager.Interop;
using PRAManager;
using System.IO;
using PRMapCompanion;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using PopupControl;
using System.Linq;

namespace Bubbles
{
    class BubblesButton : MMBase
    {
        public void Create()
        {
            if (m_bCreated)
                return;

            m_cmdBubbles = MMUtils.MindManager.Commands.Add(Utils.Registered_AddinName, "ribbon.palmaross.bubbles");
            m_cmdBubbles.Caption = "";
            m_cmdBubbles.ToolTip = Utils.getString("main.tooltip") + "\n" + Utils.getString("main.name");
            m_cmdBubbles.UpdateState += new ICommandEvents_UpdateStateEventHandler(m_cmdBubbles_UpdateState);
            m_cmdBubbles.LargeImagePath = Utils.ImagesPath + "STICKS.png";
            m_cmdBubbles.ImagePath = Utils.ImagesPath + "STICKS.png";
            m_cmdBubbles.Click += new ICommandEvents_ClickEventHandler(m_cmdBubbles_Click);
            m_ctrlBubbles = MMUtils.MindManager.StatusBarControls.AddButton(m_cmdBubbles);

            m_cmdDetachNotes = MMUtils.MindManager.Commands.Add(Utils.Registered_AddinName, "bubbles.detach_notes");
            m_cmdDetachNotes.Caption = Utils.getString("bubbles.notes.detach");
            m_cmdDetachNotes.UpdateState += new ICommandEvents_UpdateStateEventHandler(m_cmdDetachNotes_UpdateState);
            m_cmdDetachNotes.ImagePath = Utils.ImagesPath + "detach.png";
            m_cmdDetachNotes.Click += new ICommandEvents_ClickEventHandler(m_cmdDetachNotes_Click);
            m_cmdDetachNotes.SetDynamicMenu(MmDynamicMenu.mmDynamicMenuContextTopic);

            m_bubbleSnippets = new BubbleSnippets();
            m_bubblesMenu = new MainMenuDlg();
            commandPopup.Tag = 0; // Tag is a stick ID

            DocumentStorage.Subscribe(this);

            m_bCreated = true;

            using (StickerDummy dlg = new StickerDummy(null, new Point(0, 0)))
            {
                StickerDummy.DummyStickerWidth = dlg.Width;
                StickerDummy.DummyStickerHeight = dlg.Height;
                StickerDummy.DummyStickerImageX = dlg.pStickerImage.Location.X;
                StickerDummy.DummyStickerImageY = dlg.pStickerImage.Location.Y;
            }

            DataTable dt;
            using (BubblesDB db = new BubblesDB())
                dt = db.ExecuteQuery("select * from STICKS order by type");

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["start"]) == 0)
                    continue;

                m_bubblesMenu.startId = Convert.ToInt32(dr["id"]);

                switch (dr["type"].ToString()) 
                {
                    case StickUtils.typeicons:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.pIcons, null);
                        break;
                    case StickUtils.typetaskinfo:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.TaskInfo, null);
                        break;
                    case StickUtils.typeformat:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Format, null);
                        break;
                    case StickUtils.typesources:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.MySources, null);
                        break;
                    case StickUtils.typebookmarks:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Bookmarks, null);
                        break;
                    case StickUtils.typepaste:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Paste, null);
                        break;
                    case StickUtils.typeorganizer:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Organizer, null);
                        break;
                }
            }

            HidePopup = new Timer() { Interval = 2000 };
            HidePopup.Tick += HidePopup_Tick;
            HidePopup.Start();
        }

        /// <summary>
        /// Hide command popup if cursor position is out of stick or popup bounds
        /// </summary>
        private void HidePopup_Tick(object sender, EventArgs e)
        {
            if (commandPopup.Visible)
            {
                int stickid = Convert.ToInt32(commandPopup.Tag);
                Form form = STICKS[stickid];

                if (form.RectangleToScreen(form.ClientRectangle).Contains(Cursor.Position) ||
                    commandPopup.RectangleToScreen(commandPopup.ClientRectangle).Contains(Cursor.Position) ||
                    commandPopup.Name.StartsWith("calendar"))
                return;

                StickUtils.ActivateMindManager(); // = Hide popup
            }
        }

        private void m_cmdBubbles_Click()
        {
            if (m_bubblesMenu.Visible)
                m_bubblesMenu.Hide();
            else
            {
                m_bubblesMenu.Location = new Point(Cursor.Position.X - m_bubblesMenu.Width / 2, MMUtils.MindManager.Top + MMUtils.MindManager.Height - m_bubblesMenu.Height - m_bubblesMenu.Settings.Height);
                m_bubblesMenu.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                int locX = m_bubblesMenu.panel1.Location.X;
                int startLoc = m_bubblesMenu.panel1.Location.Y + m_bubblesMenu.panel1.Height;
                m_bubblesMenu.panel1.Location = new Point(locX, m_bubblesMenu.label1.Location.Y);
                m_bubblesMenu.Refresh();

                do
                {
                    startLoc = startLoc - 7;
                    m_bubblesMenu.panel1.Location = new Point(locX, startLoc);
                    m_bubblesMenu.panel1.Refresh();
                }
                while (m_bubblesMenu.panel1.Location.Y > 0);
            }
        }

        private void m_cmdBubbles_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
            pChecked = false;
        }

        private void m_cmdDetachNotes_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
            pChecked = false;
        }

        private void m_cmdDetachNotes_Click()
        {
            if (m_topicNotes == null || m_topicNotes.IsDisposed)
                m_topicNotes = new TopicNotesDlg();

            if (!m_topicNotes.Visible)
                m_topicNotes.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            int selectedtopic_index = -1, addedtopics = 0;
            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (String.IsNullOrEmpty(t.Notes.Text))
                    continue;

                addedtopics++;
                string notes = t.Notes.Text;

                string rtf = "";
                rtf = t.Notes.TextRTF;
                //if (!t.Notes.IsPlainTextOnly) { rtf = t.Notes.TextRTF; }
                TopicNotesItem item = new TopicNotesItem(t, notes, rtf, t.Text, t.Guid,
                    t.Document.FullName, t.Document.CentralTopic.Text);
                selectedtopic_index = m_topicNotes.listTopics.Items.Add(item);
            }
            // If we have added one topic only, select this topic
            if (addedtopics == 1)
                m_topicNotes.listTopics.SelectedIndex = selectedtopic_index;
        }

        public override void onDocumentActivated(MMEventArgs aArgs)
        {
            if (m_Bookmarks != null)
                m_Bookmarks.Init();

            if (m_TaskInfo != null && m_TaskInfo.Visible)
                DocumentStorage.Sync(MMUtils.ActiveDocument); // subscribe document to events
            else
                DocumentStorage.Sync(MMUtils.ActiveDocument, false); // unsubscribe document
        }

        public override void onDocumentDeactivated(MMEventArgs aArgs)
        {
            if (MMUtils.ActiveDocument == null && m_Bookmarks != null)
                m_Bookmarks.Init();
        }

        // For Task Info stick only.
        public override void onObjectChanged(MMEventArgs aArgs)
        {
            if (m_TaskInfo == null || !m_TaskInfo.Visible) return;

            if (aArgs.what.Contains("selection"))
            {
                // If map selection changed, change the dates in the TaskInfo stick with selected topic dates
                SetDates();
            }
            else if (aArgs.what.Contains("task")) // it's possible that user changed task dates
            {
                SetDates2();
            }
        }

        public static void SetDates()
        {
            // No topics selected. Disable Task Info stick controls
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
            {
                if (m_TaskInfo.pPriority.Enabled)  // if controls are enabled
                    EnableTaskInfoControls(false); // disable them
                return;
            }

            // There are topics seleted

            // Enable Task Info stick controls if they are disabled
            if (!m_TaskInfo.pPriority.Enabled) EnableTaskInfoControls();

            // Set dates in the TaskInfo stick (from topic)
            SetDates2();
        }

        public static void SetDates2()
        {
            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            DateTime startdate = t.Task.StartDate, duedate = t.Task.DueDate;
            bool startequal = true, dueequal = true;

            foreach (Topic _t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (!_t.Task.HasStartDate || _t.Task.StartDate != startdate)
                    startequal = false;
                if (!_t.Task.HasDueDate || _t.Task.DueDate != duedate)
                    dueequal = false; 
            }

            // Start Date
            DateTime dt = t.Task.StartDate;
            if (dt == MMUtils.NULLDATE || !startequal)
            {
                m_TaskInfo.pStartDate.BackColor = SystemColors.ControlLight;
                m_TaskInfo.pTopicStartDate.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                m_TaskInfo.pTopicStartDate.Tag = false;
                tt.SetToolTip(m_TaskInfo.pTopicStartDate, Utils.getString("taskinfo.pTopicStartDate.set.tooltip"));
            }
            else
            {
                m_TaskInfo.pStartDate.BackColor = SystemColors.Window;
                string topicdate = dt.ToString("dd, MM").Replace(", ", "/");

                if (m_TaskInfo.pStartDate.Text != topicdate)
                {
                    m_TaskInfo.pStartDate.Text = topicdate;
                    m_TaskInfo.pStartDate.Tag = dt.Date.AddHours(8);
                    tt.SetToolTip(m_TaskInfo.pStartDate, dt.ToLongDateString());
                }
                m_TaskInfo.pStartDate.Select(0, 0); // set carret to the beginning
                m_TaskInfo.pTopicStartDate.Tag = true;
                m_TaskInfo.pTopicStartDate.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");
                tt.SetToolTip(m_TaskInfo.pTopicStartDate, Utils.getString("taskinfo.pTopicStartDate.remove.tooltip"));
            }

            // Due Date
            dt = t.Task.DueDate;
            if (dt == MMUtils.NULLDATE || !dueequal)
            {
                m_TaskInfo.pDueDate.BackColor = SystemColors.ControlLight;
                m_TaskInfo.pTopicDueDate.Tag = false;
                m_TaskInfo.pTopicDueDate.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                tt.SetToolTip(m_TaskInfo.pTopicDueDate, Utils.getString("taskinfo.pTopicDueDate.set.tooltip"));
            }
            else
            {
                m_TaskInfo.pDueDate.BackColor = SystemColors.Window;
                string topicdate = dt.ToString("dd, MM").Replace(", ", "/");

                if (m_TaskInfo.pDueDate.Text != topicdate)
                {
                    m_TaskInfo.pDueDate.Text = topicdate;
                    m_TaskInfo.pDueDate.Tag = dt.Date.AddHours(8);
                    tt.SetToolTip(m_TaskInfo.pDueDate, dt.ToLongDateString());
                }
                m_TaskInfo.pDueDate.Select(0, 0);
                m_TaskInfo.pTopicDueDate.Tag = true;
                m_TaskInfo.pTopicDueDate.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");
                tt.SetToolTip(m_TaskInfo.pTopicDueDate, Utils.getString("taskinfo.pTopicDueDate.remove.tooltip"));
            }

            if (!m_TaskInfo.stickDuration)
            {
                SetTaskInfoDurationUnit(t);

                int duration = t.Task.GetDuration(t.Task.DurationUnit);

                if (m_TaskInfo.numDuration.Value != duration)
                {
                    m_TaskInfo.MMDuration = true;
                    m_TaskInfo.numDuration.Value = duration;
                }
            }
        }

        public static void SetTaskInfoDurationUnit(Topic t)
        {
            MmDurationUnit unit = t.Task.DurationUnit;

            switch (unit)
            {
                case MmDurationUnit.mmDurationUnitMinute:
                    m_TaskInfo.ST_DurationUnits.SelectedIndex = 0; break;
                case MmDurationUnit.mmDurationUnitHour:
                    m_TaskInfo.ST_DurationUnits.SelectedIndex = 1; break;
                case MmDurationUnit.mmDurationUnitDay:
                    m_TaskInfo.ST_DurationUnits.SelectedIndex = 2; break;
                case MmDurationUnit.mmDurationUnitWeek:
                    m_TaskInfo.ST_DurationUnits.SelectedIndex = 3; break;
                case MmDurationUnit.mmDurationUnitMonth:
                    m_TaskInfo.ST_DurationUnits.SelectedIndex = 4; break;
            }
        }

        static void EnableTaskInfoControls(bool enable = true)
        {
            foreach (System.Windows.Forms.Control c in m_TaskInfo.Controls)
            {
                if (c.Name != "pictureHandle" && c.Name != "Manage" && 
                    c.Name != "pResources" && c.Name != "pQuickTask")
                {
                    c.Enabled = enable;
                }
            }

            // Disable set date checkbox if controls are disabled
            if (!enable)
            {
                m_TaskInfo.pTopicStartDate.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                m_TaskInfo.pTopicDueDate.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                m_TaskInfo.pStartDate.BackColor = SystemColors.ControlLight;
                m_TaskInfo.pDueDate.BackColor = SystemColors.ControlLight;
            }
        }

        public void Destroy()
        {
            if (!m_bCreated)
                return;

            m_ctrlBubbles.Delete(); Marshal.ReleaseComObject(m_ctrlBubbles); m_ctrlBubbles = null;
            Marshal.ReleaseComObject(m_cmdBubbles); m_cmdBubbles = null;

            Marshal.ReleaseComObject(m_cmdDetachNotes); m_cmdDetachNotes = null;

            if (m_bubbleSnippets.Visible)
                m_bubbleSnippets.Hide();
            m_bubbleSnippets.Dispose();
            m_bubbleSnippets = null;

            if (BubbleBookmarks.BookmarkedDocuments != null && BubbleBookmarks.BookmarkedDocuments.Count > 0)
            {
                BubbleBookmarks.BookmarkedDocuments.Clear();
                BubbleBookmarks.BookmarkedDocuments = null;
            }

            if (m_BookmarkList != null)
            {
                m_BookmarkList.Hide();
                m_BookmarkList.Dispose();
                m_BookmarkList = null;
            }

            if (m_Resources != null)
            {
                m_Resources.Hide();
                m_Resources.Dispose();
                m_Resources = null;
            }
            
            if (m_topicNotes != null)
            {
                m_topicNotes.listTopics.Items.Clear();
                m_topicNotes.Close();
                m_topicNotes.Dispose();
                m_topicNotes = null;
            }

            if (m_bubblesMenu.Visible)
                m_bubblesMenu.Hide();
            m_bubblesMenu.Dispose();
            m_bubblesMenu = null;

            if (m_Notes != null)
            {
                m_Notes.Dispose();
                m_Notes = null;
            }

            foreach (var stick in STICKS)
            {
                if (stick.Value.Visible)
                    stick.Value.Hide();
                stick.Value.Dispose();
            }
            STICKS.Clear();

            foreach (var note in pNOTES)
            {
                if (note.Value.Visible)
                    note.Value.Hide();
                note.Value.Dispose();
            }
            pNOTES.Clear();

            if (commandPopup != null)
            {
                commandPopup.Dispose(); commandPopup = null;
            }

            HidePopup.Stop();
            HidePopup.Tick -= HidePopup_Tick;
            HidePopup.Dispose(); HidePopup = null;

            DocumentStorage.Unsubscribe(this);

            Utils.StockIcons.Clear();

            m_bCreated = false;
        }

        private bool m_bCreated;

        public static BubbleSnippets m_bubbleSnippets = null;

        public static BubbleBookmarks m_Bookmarks;
        public static BookmarkListDlg m_BookmarkList;

        public static ResourcesDlg m_Resources;

        public static BubbleTaskInfo m_TaskInfo;

        public static Organizer.NotesDlg m_Notes;

        public static MainMenuDlg m_bubblesMenu = null;

        private Command m_cmdBubbles;
        private Mindjet.MindManager.Interop.Control m_ctrlBubbles;

        private Command m_cmdDetachNotes;
        public static TopicNotesDlg m_topicNotes;

        public static Dictionary<int, Form> STICKS = new Dictionary<int, Form>();
        public static Dictionary<int, Form> pNOTES = new Dictionary<int, Form>();

        public static Popup commandPopup = new Popup(new StickPopup().panelH);

        Timer HidePopup = new Timer();
        static ToolTip tt = new ToolTip() { ShowAlways = true, AutoPopDelay = 3000 };
    }
}
