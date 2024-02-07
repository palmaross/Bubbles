using System;
using System.Windows.Forms;
using System.Drawing;
using PRAManager;
using WindowsInput.Native;
using WindowsInput;
using System.Linq;
using Mindjet.MindManager.Interop;
using Control = System.Windows.Forms.Control;
using System.Collections.Generic;
using PRMapCompanion;

namespace Bubbles
{
    public partial class StickPopup : UserControl
    {
        public StickPopup()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "IconStick.htm");

            pCollapse.Tag = Utils.getString("float_icons.contextmenu.collapse_expand");
            pRotate.Tag = Utils.getString("float_icons.contextmenu.rotate");
            pRemember.Tag = Utils.getString("float_icons.contextmenu.settings");
            pClose.Tag = Utils.getString("float_icons.contextmenu.close");

            foreach (PictureBox pb in panelH.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }

            pNewIcon.Tag = Utils.getString("float_icons.contextmenu.new");
            pDeleteAllIcons.Tag = Utils.getString("float_icons.contextmenu.deletealltopic");
            pNewBookmark.Tag = Utils.getString("bookmarks.contextmenu.add.tooltip");
            pBookmarkList.Tag = Utils.getString("mysources.sourceview.list");
            pFontItalic.Tag = "Italic";
            pCleanFormat.Tag = Utils.getString("bubbleformat.clearformat");

            foreach (PictureBox pb in panelOther.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }

            //Subtopic.Tag = Utils.getString("BubblesPaste.addsubtopic");
            NextTopic.Tag = Utils.getString("BubblesPaste.addtopic");
            TopicBefore.Tag = Utils.getString("BubblesPaste.addbefore");
            ParentTopic.Tag = Utils.getString("BubblesPaste.addparent");
            Callout.Tag = Utils.getString("BubblesPaste.addcallout");
            //ToggleTextFormat.Tag = Utils.getString("BubblesPaste.workwith.unformatted");
            //ToggleTextFormat.AccessibleName = "unformatted";

            foreach (Control pb in panelPasteTopic.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }
        }

        private void pb_MouseHover(object sender, EventArgs e)
        {
            Control pb = sender as Control;
            int offset = pb.Height / 2;
            bool add = panelPasteTopic.AccessibleName == "add";

            string tooltip = (string)pb.Tag;
            switch (pb.Name)
            {
                case "Subtopic":
                    if (add)
                        tooltip = Utils.getString("BubblesPaste.addsubtopic");
                    else
                        tooltip = Utils.getString("BubblesPaste.pastesubtopic");
                    break;
                case "NextTopic":
                    if (add)
                        tooltip = Utils.getString("BubblesPaste.addtopic");
                    else
                        tooltip = Utils.getString("BubblesPaste.pastetopic");
                    break;
                case "TopicBefore":
                    if (add)
                        tooltip = Utils.getString("BubblesPaste.addbefore");
                    else
                        tooltip = Utils.getString("BubblesPaste.pastebefore");
                    break;
                case "ParentTopic":
                    if (add)
                        tooltip = Utils.getString("BubblesPaste.addparent");
                    else
                        tooltip = Utils.getString("BubblesPaste.pasteparent");
                    break;
                case "Callout":
                    if (add)
                        tooltip = Utils.getString("BubblesPaste.addcallout");
                    else
                        tooltip = Utils.getString("BubblesPaste.pastecallout");
                    break;
            }

            Point pnt = pb.PointToClient(Cursor.Position);
            pnt = new Point(pnt.X += offset, pnt.Y += offset);  // Give a little offset
            toolTip1.Show(tooltip, pb, pnt);
        }
        private void pb_MouseLeave(object sender, EventArgs e)
        {
            Control pb = sender as Control;
            toolTip1.Hide(pb);
        }

        #region CommonComandsPopup
        private void pCollapse_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            StickUtils.ActivateMindManager(); // In order to hide Popup

            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Collapse();
                    break;
                case StickUtils.typetaskinfo:
                    (stick as BubbleTaskInfo).Collapse();
                    break;
                case StickUtils.typeformat:
                    (stick as BubbleFormat).Collapse();
                    break;
                case StickUtils.typesources:
                    (stick as BubbleMySources).Collapse();
                    break;
                case StickUtils.typebookmarks:
                    (stick as BubbleBookmarks).Collapse();
                    break;
                case StickUtils.typeaddtopic:
                    (stick as BubbleAddTopic).Collapse();
                    break;
                case StickUtils.typepaste:
                    (stick as BubblePaste).Collapse();
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Collapse();
                    break;
            }
        }

        private void pRotate_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            StickUtils.ActivateMindManager(); // In order to hide Popup

            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Rotate();
                    break;
                case StickUtils.typetaskinfo:
                    (stick as BubbleTaskInfo).Rotate();
                    break;
                case StickUtils.typeformat:
                    (stick as BubbleFormat).Rotate();
                    break;
                case StickUtils.typesources:
                    (stick as BubbleMySources).Rotate();
                    break;
                case StickUtils.typebookmarks:
                    (stick as BubbleBookmarks).Rotate();
                    break;
                case StickUtils.typeaddtopic:
                    (stick as BubbleAddTopic).Rotate();
                    break;
                case StickUtils.typepaste:
                    (stick as BubblePaste).Rotate();
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Rotate();
                    break;
            }
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            StickUtils.ActivateMindManager(); // In order to hide Popup

            BubblesButton.STICKS.Remove((int)stick.Tag);
            stick.Close();

            if (stick.Name == "BubbleTaskInfo")
            {
                BubblesButton.m_TaskInfo = null;
                DocumentStorage.Sync(MMUtils.ActiveDocument, false);
            }
        }

        private void pRemember_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            StickUtils.ActivateMindManager(); // In order to hide Popup

            string orientation = "H";
            if (stick.Width < stick.Height) orientation = "V";

            bool collapsed = stick.Width == StickUtils.minSize;

            StickUtils.SaveStick(stick.Bounds, (int)stick.Tag, orientation, collapsed);
        }
        #endregion

        #region DifferentStickCommands
        private void pNewIcon_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as BubbleIcons).NewIcon();
        }

        private void pDeleteAllIcons_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                if (t.UserIcons.Count > 0)
                    t.UserIcons.RemoveAll();
        }

        private void pBookmarkList_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as BubbleBookmarks).BookmarkList_Click(null, null);
        }

        private void pNewBookmark_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            var stick = panelH.Tag as Form;
            (stick as BubbleBookmarks).AddBookmark();
        }

        private void pFontItalic_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as BubbleFormat).pItalic_Click(null, null);
        }

        private void pCleanFormat_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as BubbleFormat).pClearFormat_Click(null, null);
        }
        #endregion

        #region BubblePaste

        private void pAddTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PasteAddTopic((sender as PictureBox).Name);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (topicsToPaste.Count == 1)
            {
                // Release the topic with pasted text
                topicsToPaste[0].ParentTopic.SelectOnly();
                topicsToPaste[0].SelectOnly();
                return;
            }

            topicsToPaste[1].SelectOnly(); // we have to release the topic with pasted text (topics[0]) to achieve its text

            PasteTopicTransaction(Utils.getString("addtopics.transactionname.paste"));
        }
        List<Topic> topicsToPaste = new List<Topic>();

        void PasteTopicTransaction(string trname)
        {
            Transaction _tr = MMUtils.ActiveDocument.NewTransaction(trname);
            _tr.IsUndoable = true;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(PasteTopics);
            _tr.Start();
        }

        public void PasteTopics(Document pDocument)
        {
            // Get the needed text
            string text = topicsToPaste[0].Text;
            if (FormattedText())
                text = topicsToPaste[0].Title.TextRTF;

            topicsToPaste.RemoveAt(0); // topic is already handled 

            foreach (Topic t in topicsToPaste)
            {
                if (FormattedText())
                    t.Title.TextRTF = text;
                else
                    t.Text = text;
            }

            topicsToPaste.Clear();
        }

        /// <summary>
        /// Add topic or paste to topic
        /// </summary>
        /// <param name="topictype">With which topic perform the operation</param>
        void PasteAddTopic(string topictype)
        {
            if (MMUtils.ActiveDocument == null) return;
            int count = MMUtils.ActiveDocument.Selection.OfType<Topic>().Count();
            if (count == 0) return;

            bool paste = panelPasteTopic.AccessibleName == "paste";
            if (paste && !System.Windows.Forms.Clipboard.ContainsText()) return;

            if (paste) // add subtopic(s) and paste clipboard conent to them
            {
                // Create subtopics for all selected topics
                Topic _t = null; bool addparent = true;

                string text = System.Windows.Forms.Clipboard.GetText(); // unformatted text

                if (!FormattedText())
                    System.Windows.Forms.Clipboard.SetText(text); // set unformatted text to clipboard

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (addparent || topictype != "ParentTopic")
                    {
                        if (topictype != "ParentTopic") text = "";

                        _t = StickUtils.AddTopic(t, topictype, text); // For "pAddParent" - adds empty parent topic
                        addparent = false;                      // There should be only one topic!
                        topicsToPaste.Add(_t);
                    }
                }

                if (topictype != "ParentTopic")
                {
                    StickUtils.ActivateMindManager(); // we need the selected topic to be active!

                    // We can make the Ctrl-V with one topic only!
                    topicsToPaste[0].SelectOnly();

                    // Select topic text (in order to enter inside the topic)
                    sim.Keyboard.KeyDown(VirtualKeyCode.F2);

                    // And replace it with the clipboard formatted text (pressing Ctrl-V).
                    // Copied text may have several lines but we don't want several topics,
                    // we want paste all text inside the topic
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);

                    // Text will be pasted after this method is finished!!
                }

                if (count == 1 || topictype == "ParentTopic")
                    topicsToPaste.Clear(); // we have already pasted the only subtopic (topicstoPaste[0])
                else
                    timer1.Start(); // We can't make Ctrl-V with multiple topics.
                                    // So, we did this with one topic and we get its text in a timer time
                                    // to populate the rest topics with this text.
            }
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
        #endregion

        private void ToggleTextFormat_Click(object sender, EventArgs e)
        {
            //if (ToggleTextFormat.AccessibleName.ToString() == "unformatted")
            //{
            //    ToggleTextFormat.Tag = Utils.getString("BubblesPaste.workwith.formatted");
            //    ToggleTextFormat.AccessibleName = "formatted";
            //    ToggleTextFormat.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "formattedText.png");
            //}
            //else if (ToggleTextFormat.AccessibleName == "formatted")
            //{
            //    ToggleTextFormat.Tag = Utils.getString("BubblesPaste.workwith.unformatted");
            //    ToggleTextFormat.AccessibleName = "unformatted";
            //    ToggleTextFormat.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "unformattedText.png");
            //}
        }

        bool FormattedText()
        {
            return true;// ToggleTextFormat.AccessibleName == "formatted";
        }

        private void pProgress_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            PictureBox pb = sender as PictureBox;
            int value = Convert.ToInt32(pb.Name.Substring(1));

            bool alltopicshaveicon = true;
            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (t.Task.Complete != value) { alltopicshaveicon = false; break; }
            }

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (alltopicshaveicon) t.Task.Complete = -1;
                else t.Task.Complete = value;
            }
        }

        private void pPriority_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            PictureBox pb = sender as PictureBox;
            int value = Convert.ToInt32(pb.Name.Substring(3));

            bool alltopicshaveicon = true;
            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (t.Task.Priority != TaskPriority(value)) { alltopicshaveicon = false; break; }
            }

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (alltopicshaveicon) t.Task.Priority = 0;
                else t.Task.Priority = TaskPriority(value);
            }
        }

        MmTaskPriority TaskPriority(int value)
        {
            switch (value)
            {
                case 2: return MmTaskPriority.mmTaskPriority2;
                case 3: return MmTaskPriority.mmTaskPriority3;
                case 4: return MmTaskPriority.mmTaskPriority4;
                case 5: return MmTaskPriority.mmTaskPriority5;
            }
            return 0;
        }

        InputSimulator sim = new InputSimulator();
        public List<string> TopicList = new List<string>();
    }
}
