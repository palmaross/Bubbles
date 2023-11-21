using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bubbles
{
    public partial class StickPopup : UserControl
    {
        public StickPopup()
        {
            InitializeComponent();

            toolTip1.SetToolTip(pCollapse, Utils.getString("float_icons.contextmenu.collapse"));
            toolTip1.SetToolTip(pExpand, Utils.getString("float_icons.contextmenu.expand"));
            toolTip1.SetToolTip(pRotate, Utils.getString("float_icons.contextmenu.rotate"));
            toolTip1.SetToolTip(pRemember, Utils.getString("float_icons.contextmenu.settings"));
            toolTip1.SetToolTip(pClose, Utils.getString("float_icons.contextmenu.close"));
        }

        private void pCollapse_Click(object sender, EventArgs e)
        {
            var item = panelH.Tag as PopupItem;
            var stick = item.aForm;

            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Collapse(true);
                    break;
                case StickUtils.typepripro:
                    (stick as BubblePriPro).Collapse(true);
                    break;
                case StickUtils.typeformat:
                    (stick as BubbleFormat).Collapse(true);
                    break;
                case StickUtils.typesources:
                    (stick as BubbleMySources).Collapse(true);
                    break;
                case StickUtils.typebookmarks:
                    (stick as BubbleBookmarks).Collapse(true);
                    break;
                case StickUtils.typepaste:
                    (stick as BubblePaste).Collapse(true);
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Collapse(true);
                    break;
            }
            Destroy();
        }

        private void pExpand_Click(object sender, EventArgs e)
        {
            var item = panelH.Tag as PopupItem;
            var stick = item.aForm;

            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Collapse(false, true);
                    break;
                case StickUtils.typepripro:
                    (stick as BubblePriPro).Collapse(false, true);
                    break;
                case StickUtils.typeformat:
                    (stick as BubbleFormat).Collapse(false, true);
                    break;
                case StickUtils.typesources:
                    (stick as BubbleMySources).Collapse(false, true);
                    break;
                case StickUtils.typebookmarks:
                    (stick as BubbleBookmarks).Collapse(false, true);
                    break;
                case StickUtils.typepaste:
                    (stick as BubblePaste).Collapse(false, true);
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Collapse(false, true);
                    break;
            }
            Destroy();
        }

        private void pRotate_Click(object sender, EventArgs e)
        {
            var item = panelH.Tag as PopupItem;
            var stick = item.aForm;

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
            BubblesButton.popup.Dispose();
            this.Dispose();
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            var item = panelH.Tag as PopupItem;
            var stick = item.aForm;
            BubblesButton.STICKS.Remove((int)item.aForm.Tag);
            stick.Close();
            Destroy();
        }

        private void pRemember_Click(object sender, EventArgs e)
        {

        }
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
