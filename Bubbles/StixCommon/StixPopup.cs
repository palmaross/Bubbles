using System;
using System.Windows.Forms;
using System.Drawing;
using PRAManager;
using WindowsInput;
using System.Linq;
using Mindjet.MindManager.Interop;
using Control = System.Windows.Forms.Control;

namespace Bubbles
{
    public partial class StixPopup : UserControl
    {
        public StixPopup()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "IconStick.htm");

            pRotate.Tag = Utils.getString("stix.contextmenu.rotate");
            pRemember.Tag = Utils.getString("stix.contextmenu.remember");
            pClose.Tag = Utils.getString("button.close");

            foreach (PictureBox pb in panelH.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }

            pNewIcon.Tag = Utils.getString("icons.contextmenu.new");
            pDeleteAllIcons.Tag = Utils.getString("icons.contextmenu.deletealltopic");
            pNewBookmark.Tag = Utils.getString("bookmarks.contextmenu.add.tooltip");
            pBookmarkList.Tag = Utils.getString("tools.toolview.list");
            pFontItalic.Tag = "Italic";

            foreach (PictureBox pb in panelOther.Controls) {
                pb.MouseHover += pb_MouseHover; pb.MouseLeave += pb_MouseLeave; }

            //Subtopic.Tag = Utils.getString("AddTopicStix.addsubtopic");
            NextTopic.Tag = Utils.getString("AddTopicStix.addtopic");
            TopicBefore.Tag = Utils.getString("AddTopicStix.addbefore");
            ParentTopic.Tag = Utils.getString("AddTopicStix.addparent");
            Callout.Tag = Utils.getString("AddTopicStix.addcallout");
            //ToggleTextFormat.Tag = Utils.getString("TextOpsStix.workwith.unformatted");
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
                        tooltip = Utils.getString("AddTopicStix.addsubtopic");
                    else
                        tooltip = Utils.getString("TextOpsStix.pastesubtopic");
                    break;
                case "NextTopic":
                    if (add)
                        tooltip = Utils.getString("AddTopicStix.addtopic");
                    else
                        tooltip = Utils.getString("TextOpsStix.pastetopic");
                    break;
                case "TopicBefore":
                    if (add)
                        tooltip = Utils.getString("AddTopicStix.addbefore");
                    else
                        tooltip = Utils.getString("TextOpsStix.pastebefore");
                    break;
                case "ParentTopic":
                    if (add)
                        tooltip = Utils.getString("AddTopicStix.addparent");
                    else
                        tooltip = Utils.getString("TextOpsStix.pasteparent");
                    break;
                case "Callout":
                    if (add)
                        tooltip = Utils.getString("AddTopicStix.addcallout");
                    else
                        tooltip = Utils.getString("TextOpsStix.pastecallout");
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

        private void pRotate_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            StixUtils.ActivateMindManager(); // In order to hide Popup

            switch (stick.Name)
            {
                case StixUtils.typeicons:
                    (stick as StixIcons).Rotate();
                    break;
                case StixUtils.typetaskinfo:
                    (stick as StixTaskInfo).Rotate();
                    break;
                case StixUtils.typeformat:
                    (stick as StixFormat).Rotate();
                    break;
                case StixUtils.typetools:
                    (stick as StixTools).Rotate();
                    break;
                case StixUtils.typemapnavigator:
                    (stick as StixMapNavigator).Rotate();
                    break;
                case StixUtils.typeaddtopic:
                    (stick as StixAddTopic).Rotate();
                    break;
                case StixUtils.typetextops:
                    (stick as StixTextOps).Rotate();
                    break;
                case StixUtils.typeorganizer:
                    (stick as StixOrganizer).Rotate();
                    break;
            }
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            StixUtils.ActivateMindManager(); // In order to hide Popup

            StixMain.STICKS.Remove((int)stick.Tag);
            stick.Close();

            if (stick.Name == "BubbleTaskInfo")
            {
                StixMain.m_TaskInfo = null;
            }
        }

        private void pRemember_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            StixUtils.ActivateMindManager(); // In order to hide Popup

            string orientation = "H";
            if (stick.Width < stick.Height) orientation = "V";

            StixUtils.SaveStick(stick.Bounds, (int)stick.Tag, orientation);
        }
        #endregion

        #region DifferentStickCommands
        private void pNewIcon_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as StixIcons).NewIcon();
        }

        private void pDeleteAllIcons_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                if (t.UserIcons.Count > 0)
                    t.UserIcons.RemoveAll();
        }

        private void pFontItalic_Click(object sender, EventArgs e)
        {
            var stick = panelH.Tag as Form;
            (stick as StixFormat).pItalic_Click(null, null);
        }

        #endregion

        private void pPasteTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (panelPasteTopic.Tag is Form ff)
                    (ff as StixTextOps).PasteTopic((sender as PictureBox).Name.ToLower());
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
