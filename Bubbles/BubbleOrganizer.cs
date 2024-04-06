using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class BubbleOrganizer : Form
    {
        public BubbleOrganizer(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation; // "H" or "V"

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "OrganizerStick.htm");

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            toolTip1.SetToolTip(pClipboard, Utils.getString("BubbleOrganizer.clipboard.tooltip"));
            toolTip1.SetToolTip(pIdeas, Utils.getString("BubbleOrganizer.ideas.tooltip"));
            toolTip1.SetToolTip(pLinks, Utils.getString("BubbleOrganizer.links.tooltip"));
            toolTip1.SetToolTip(pNotes, Utils.getString("BubbleOrganizer.notes.tooltip"));
            toolTip1.SetToolTip(pTodos, Utils.getString("BubbleOrganizer.todos.tooltip"));

            //toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, stickname);

            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            StixUtils.SetCommonContextMenu(contextMenuStrip1, StixUtils.typeorganizer);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();
        }

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
            base.OnMouseDown(e);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OrganizerStick.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                //ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typeorganizer, scaleFactor);
                //dlg.Location =
                //    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                //dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation);
        }

        private void PasteLink_Click(object sender, EventArgs e)
        {
            
        }

        private void PasteNotes_Click(object sender, EventArgs e)
        {
            
        }

        private void Notes_Click(object sender, EventArgs e)
        {
            if (StixButton.m_Notes == null)
            {
                StixButton.m_Notes = new Organizer.NotesDlg();
                StixButton.m_Notes.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            else
            {
                if (StixButton.m_Notes.WindowState == FormWindowState.Minimized)
                    StixButton.m_Notes.WindowState = FormWindowState.Normal;
            }
        }

        private void addsubtopic_Click(object sender, EventArgs e)
        {
            
        }

        private void callout_Click(object sender, EventArgs e)
        {
            
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Show(Cursor.Position);
        }

        string orientation = "H";

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
