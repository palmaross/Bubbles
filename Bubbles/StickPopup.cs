using System;
using System.Windows.Forms;
using System.Drawing;
using PRAManager;
using WindowsInput.Native;
using WindowsInput;
using System.Linq;
using Mindjet.MindManager.Interop;
using Control = System.Windows.Forms.Control;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Image = System.Drawing.Image;

namespace Bubbles
{
    public partial class StickPopup : UserControl
    {
        public StickPopup()
        {
            InitializeComponent();

            TogglePasteAdd.Text = Utils.getString("StickPopup.TogglePasteAdd.paste");

            pCollapse.Tag = Utils.getString("float_icons.contextmenu.collapse_expand");
            pRotate.Tag = Utils.getString("float_icons.contextmenu.rotate");
            pRemember.Tag = Utils.getString("float_icons.contextmenu.settings");
            pClose.Tag = Utils.getString("float_icons.contextmenu.close");

            foreach (PictureBox pb in panelH.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }

            pNewIcon.Tag = Utils.getString("float_icons.contextmenu.new");
            pPasteIcon.Tag = Utils.getString("contextmenu.paste.icon.tooltip");
            pNewBookmark.Tag = Utils.getString("bookmarks.contextmenu.add.tooltip");
            pBookmarkList.Tag = Utils.getString("mysources.sourceview.list");
            pFontItalic.Tag = "Italic";
            pCleanFormat.Tag = Utils.getString("bubbleformat.clearformat");

            foreach (PictureBox pb in panelOther.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }

            pAddsubtopic.Tag = Utils.getString("BubblesPaste.addsubtopic");
            pAddNext.Tag = Utils.getString("BubblesPaste.addtopic");
            pAddBefore.Tag = Utils.getString("BubblesPaste.addbefore");
            pAddParent.Tag = Utils.getString("BubblesPaste.addparent");
            pAddCallout.Tag = Utils.getString("BubblesPaste.addcallout");
            TogglePasteAdd.Tag = Utils.getString("StickPopup.TogglePasteAdd.tooltip");
            TogglePasteAdd.AccessibleName = "paste";

            foreach (Control pb in panelPasteTopic.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }
        }

        private void pb_MouseLeave(object sender, EventArgs e)
        {
            Control pb = sender as Control;
            toolTip1.Hide(pb);
        }

        private void pb_MouseHover(object sender, EventArgs e)
        {
            Control pb = sender as Control;
            int offset = pb.Height / 2;

            Point pnt = pb.PointToClient(Cursor.Position);
            pnt = new Point(pnt.X += offset, pnt.Y += offset);  // Give a little offset
            toolTip1.Show((string)pb.Tag, pb, pnt);
        }

        private void pCollapse_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Collapse();
                    break;
                case StickUtils.typepripro:
                    (stick as BubblePriPro).Collapse();
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
                case StickUtils.typepaste:
                    (stick as BubblePaste).Collapse();
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Collapse();
                    break;
            }
            Destroy();
        }

        private void pRotate_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Rotate();
                    break;
                case StickUtils.typepripro:
                    (stick as BubblePriPro).Rotate();
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
                case StickUtils.typepaste:
                    (stick as BubblePaste).Rotate();
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Rotate();
                    break;
            }
            Destroy();
        }

        void Destroy()
        {
            BubblesButton.commandPopup.Hide();
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            BubblesButton.STICKS.Remove((int)stick.Tag);
            stick.Close();
            Destroy();
        }

        private void pRemember_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;

            string orientation = "H";
            if (stick.Width < stick.Height) orientation = "V";

            bool collapsed = stick.Width == StickUtils.minSize;

            StickUtils.SaveStick(stick.Bounds, (int)stick.Tag, orientation, collapsed);
        }

        private void pNewIcon_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as BubbleIcons).NewIcon();
        }

        private void pPasteIcon_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as BubbleIcons).PasteIcon();
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

        private void TogglePasteAdd_Click(object sender, EventArgs e)
        {
            if (TogglePasteAdd.AccessibleName.ToString() == "paste")
            {
                TogglePasteAdd.Text = Utils.getString("StickPopup.TogglePasteAdd.add");
                TogglePasteAdd.AccessibleName = "add";
                pTopicTemplate.Visible = true;
            }
            else if (TogglePasteAdd.AccessibleName.ToString() == "add")
            {
                TogglePasteAdd.Text = Utils.getString("StickPopup.TogglePasteAdd.paste");
                TogglePasteAdd.AccessibleName = "paste";
                pTopicTemplate.Visible = false;
            }
        }

        private void pAddsubtopic_Click(object sender, EventArgs e)
        {
            int count = MMUtils.ActiveDocument.Selection.OfType<Topic>().Count();
            if (MMUtils.ActiveDocument == null || count == 0) return;

            bool paste = TogglePasteAdd.AccessibleName == "paste";
            if (paste && !System.Windows.Forms.Clipboard.ContainsText()) return;

            // Get all selected topics
            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                Topic _t = t.AddSubTopic("");
                topicstoPaste.Add(_t);
            }

            if (paste) // add subtopic(s) and paste clipboard conent to them
            {
                string text = System.Windows.Forms.Clipboard.GetText(); // unformatted text

                ActivateMindManager(); // we need the selected topic to be active!

                if (!FormattedText())
                    System.Windows.Forms.Clipboard.SetText(text); // set unformatted text to clipboard

                // We can make the Ctrl-V only with one topic!
                topicstoPaste[0].SelectOnly();

                // Select topic text (in order to enter inside the topic!)
                sim.Keyboard.KeyDown(VirtualKeyCode.F2);

                // And replace it with the clipboard formatted text (pressing Ctrl-V).
                // Copied text may have several lines but we don't want several topics,
                // we want paste this text inside the topic
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);

                if (count == 1)
                    topicstoPaste.Clear(); // we have already pasted the only subtopic (topicstoPaste[0])
                else
                    timer1.Start(); // We can't make Ctrl-V with multiple topics.
                                    // So, we did this with one topic and we get its text in a timer time
                                    // to populate the rest topics with this text.
            }
            else // add subtopic(s)
            {
                foreach (Topic t in topicstoPaste)
                    t.AllSubTopics.Add();

                topicstoPaste.Clear(); // important!
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // to do надо бы в транзакцию оформить??
            timer1.Stop();

            topicstoPaste[1].SelectOnly(); // we have to release the topic with pasted text (topics[0]) to achieve its text

            // Get the needed text
            string text = topicstoPaste[0].Text;
            if (FormattedText())
                text = topicstoPaste[0].Title.TextRTF;

            topicstoPaste.RemoveAt(0); // topic is already handled 

            foreach (Topic t in topicstoPaste)
            {
                if (FormattedText())
                    t.Title.TextRTF = text;
                else
                    t.Text = text;
            }

            topicstoPaste.Clear();
        }
        List<Topic> topicstoPaste = new List<Topic>();

        private void pAddNext_Click(object sender, EventArgs e)
        {

        }

        private void pAddBefore_Click(object sender, EventArgs e)
        {

        }

        private void pAddParent_Click(object sender, EventArgs e)
        {

        }

        private void pAddCallout_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Add topic or paste to topic
        /// </summary>
        /// <param name="t">Topic to paste text to or topic to which add another topic</param>
        void PasteToTopic(string topic)
        {
            bool paste = TogglePasteAdd.AccessibleName == "paste";
            string text = "";

            if (paste && System.Windows.Forms.Clipboard.ContainsText())
                text = System.Windows.Forms.Clipboard.GetText();

            if (MMUtils.ActiveDocument != null && MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (paste && text != "")
                    {
                        if (FormattedText())
                        {
                            ActivateMindManager();
                            Topic _t = null;

                            switch (topic)
                            {
                                case "subtopic":
                                    _t = t.AddSubTopic("");

                                    break;
                            }

                            _t.SelectOnly();
                            // select topic text
                            sim.Keyboard.KeyDown(VirtualKeyCode.F2);
                            // and replace it with the clipboard formatted text
                            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                        }
                        else // unformatted text
                        {
                            t.AddSubTopic(text);
                        }
                    }
                    else
                        t.AllSubTopics.Add();
                }
            }
        }

        bool ActivateMindManager()
        {
            Process p = Process.GetProcessesByName("MindManager").FirstOrDefault();
            if (p == null)
                return false;
            else
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                return true;
            }
        }

        bool FormattedText()
        {
            var stick = panelPasteTopic.Tag as Form;
            return (stick as BubblePaste).ToggleTextFormat.Tag.ToString() == "formatted";
        }


        private void pTopicTemplate_Click(object sender, EventArgs e)
        {
            using (AddTopicTemplateDlg dlg = new AddTopicTemplateDlg())
            {
                dlg.ShowDialog();
                if (dlg.TopicList.Count > 0)
                {
                    pTopicTemplate.Image = Image.FromFile(Utils.ImagesPath + "cpTopicTemplateActive.png");
                    pTopicTemplate.Tag = 1;
                }
                else
                {
                    pTopicTemplate.Image = Image.FromFile(Utils.ImagesPath + "cpTopicTemplate.png");
                    pTopicTemplate.Tag = 0;
                }
            }
        }

        InputSimulator sim = new InputSimulator();

        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
    }

    public class PopupItem
    {
        public PopupItem(Form form, string orientation, bool collapsed)
        {
            aForm = form;
            aOrientation = orientation;
            Collapsed = collapsed;
        }

        public Form aForm;
        public string aOrientation;
        public bool Collapsed;
    }
}
