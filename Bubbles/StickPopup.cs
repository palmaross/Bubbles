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

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
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
                case StickUtils.typetextops:
                    (stick as BubbleTextOps).Collapse();
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
                case StickUtils.typetextops:
                    (stick as BubbleTextOps).Rotate();
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

        private void pPasteTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (panelPasteTopic.Tag is Form ff)
                    (ff as BubbleTextOps).PasteTopic((sender as PictureBox).Name.ToLower());
            }
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
    }
}
