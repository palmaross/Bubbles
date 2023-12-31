﻿using Mindjet.MindManager.Interop;
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
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "OrganizerStick.htm");

            MinLength = this.Width;
            RealLength = this.Width;

            if (orientation == "V")
            {
                orientation = "H";
                RotateBubble();

                foreach (PictureBox p in this.Controls.OfType<PictureBox>())
                {
                    p.Location = new Point(pNotes.Location.Y, p.Location.X);
                }
            }

            toolTip1.SetToolTip(pClipboard, Utils.getString("BubbleOrganizer.clipboard.tooltip"));
            toolTip1.SetToolTip(pIdeas, Utils.getString("BubbleOrganizer.ideas.tooltip"));
            toolTip1.SetToolTip(pLinks, Utils.getString("BubbleOrganizer.links.tooltip"));
            toolTip1.SetToolTip(pNotes, Utils.getString("BubbleOrganizer.notes.tooltip"));
            toolTip1.SetToolTip(pTodos, Utils.getString("BubbleOrganizer.todos.tooltip"));

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, stickname);

            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            StickUtils.SetCommonContextMenu(contextMenuStrip1, p2, StickUtils.typeorganizer);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += PictureHandle_MouseDoubleClick;

            if (collapsed) {
                collapsed = false; Collapse(); }
        }

        private void PictureHandle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Collapse();
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
                RotateBubble();

                foreach (PictureBox p in this.Controls.OfType<PictureBox>())
                {
                    if (p.Name == "Manage") continue;
                    p.Location = new Point(p.Location.Y, p.Location.X);
                }
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OrganizerStick.htm");
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

                StickUtils.Expand(this, RealLength, orientation);
                contextMenuStrip1.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.collapse");
                pIdeas.Visible = true;
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation);
                pIdeas.Visible = false;
                collapsed = true;
                contextMenuStrip1.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.expand");
            }
        }

        public void Rotate()
        {
            //orientation = StickUtils.RotateStick(this, p1, panel1, Manage, orientation);
        }

        void RotateBubble()
        {
            if (orientation == "H")
            {
                orientation = "V";
                Manage.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            }
            else
            {
                orientation = "H";
                Manage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            int thisWidth = this.Width;
            int thisHeight = this.Height;

            Point ManageLocation = new Point(Manage.Location.Y, Manage.Location.X);
            this.Size = new Size(thisHeight, thisWidth);
            Manage.Location = ManageLocation;
        }

        #region resize dialog
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
        #endregion

        private void PasteLink_Click(object sender, EventArgs e)
        {
            
        }

        private void PasteNotes_Click(object sender, EventArgs e)
        {
            
        }

        private void Notes_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_Notes == null)
            {
                BubblesButton.m_Notes = new Organizer.NotesDlg();
                BubblesButton.m_Notes.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            else
            {
                if (BubblesButton.m_Notes.WindowState == FormWindowState.Minimized)
                    BubblesButton.m_Notes.WindowState = FormWindowState.Normal;
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
        bool collapsed = false;
        int MinLength, RealLength;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
