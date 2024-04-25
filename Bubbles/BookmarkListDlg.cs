using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class BookmarkListDlg : Form
    {
        public BookmarkListDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "BookmarksStick.htm");

            toolTip1.SetToolTip(btnAdd, Utils.getString("bookmarks.contextmenu.add.tooltip"));
            toolTip1.SetToolTip(btnClose, Utils.getString("button.close"));
            toolTip1.SetToolTip(btnHelp, Utils.getString("button.help"));

            b_deleteall.Text = Utils.getString("bookmarks.contextmenu.deleteall");
            b_deleteall.ToolTipText = Utils.getString("bookmarks.contextmenu.deleteall.tooltip");

            b_delete.Text = Utils.getString("bookmarks.contextmenu.delete");

            b_main.Text = Utils.getString("bookmarks.contextmenu.maintopics");
            b_main.ToolTipText = Utils.getString("bookmarks.contextmenu.maintopics.tooltip");

            b_removemain.Text = Utils.getString("bookmarks.contextmenu.deletemaintopics");
            b_removemain.ToolTipText = Utils.getString("bookmarks.contextmenu.deletemaintopics.tooltip");

            cmsMore.ItemClicked += CmsMore_ItemClicked;

            // ListBox item context menu
            ToolStripMenuItem contextMenuItemDelete = new ToolStripMenuItem { Text = Utils.getString("button.delete") };
            contextMenuItemDelete.Click += toolStripMenuItemDelete_Click;
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(contextMenuItemDelete);

            this.MinimumSize = new Size((int)(this.Width / 1.5), this.Height / 2);
            this.MaximumSize = new Size(this.Width * 2, Screen.AllScreens.Max(s => s.Bounds.Height));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            listBookmarks.MouseMove += listBox_MouseMove;
            listBookmarks.DrawItem += listBookmarks_DrawItem;
            listBookmarks.MouseClick += ListBookmarks_MouseClick;

            this.Paint += BookmarkListDlg_Paint; // paint the border

            listBookmarks.Resize += ListBookmarks_Resize;
        }

        private void ListBookmarks_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void CmsMore_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == b_main)
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");

                if (StixBookmarks.BookmarkedDocuments.ContainsKey(MMUtils.ActiveDocument))
                    StixBookmarks.BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

                Init(false);
            }
            else if (e.ClickedItem == b_removemain)
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                StixBookmarks.BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

                Init(false);
            }
            else if (e.ClickedItem == b_delete)
            {
                btnDelete_Click();
            }
            else if (e.ClickedItem == b_deleteall)
            {
                if (MMUtils.ActiveDocument == null) return;

                if (StixBookmarks.BookmarkedDocuments.Keys.Contains(MMUtils.ActiveDocument))
                {
                    Topic t;
                    foreach (BookmarkItem item in StixBookmarks.BookmarkedDocuments[MMUtils.ActiveDocument])
                    {
                        t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
                        if (t != null)
                            t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                    }
                    t = null;
                    StixBookmarks.BookmarkedDocuments.Remove(MMUtils.ActiveDocument);
                }
                else
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
                    {
                        if (t.ContainsAttributesNamespace(ATTR_NAMESPACE))
                            t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                    }
                }
                Init(false, true);
            }
        }

        private void BookmarkListDlg_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
        }

        public void Init(bool fromStick = false, bool deleteall = false)
        {
            listBookmarks.Items.Clear();
            StixBookmarks.Bookmarks.Clear();

            if (MMUtils.ActiveDocument == null) return;

            Topic cTopic = MMUtils.ActiveDocument.CentralTopic;
            listBookmarks.Items.Add(new BookmarkItem(cTopic.Text.Trim(), cTopic.Guid, ""));
            cTopic = null;

            if (deleteall)
            {
                if (StixButton.m_Bookmarks != null && !fromStick)
                    StixButton.m_Bookmarks.Init(true, true);
                return;
            }

            if (StixBookmarks.BookmarkedDocuments.Keys.Contains(MMUtils.ActiveDocument))
            {
                foreach (BookmarkItem item in StixBookmarks.BookmarkedDocuments[MMUtils.ActiveDocument])
                {
                    listBookmarks.Items.Add(item);
                    StixBookmarks.Bookmarks.Add(item);
                }
            }
            else
            {
                // Add bookmarks exсept floating topics
                LoadFromMapRecursive(MMUtils.ActiveDocument.CentralTopic);

                // Add bookmarks from the floating topic branches
                foreach (Topic _t in MMUtils.ActiveDocument.AllFloatingTopics)
                    LoadFromMapRecursive(_t);

                if (StixBookmarks.Bookmarks.Count > 0)
                {
                    List<BookmarkItem> list = new List<BookmarkItem>();
                    list.AddRange(StixBookmarks.Bookmarks);
                    StixBookmarks.BookmarkedDocuments.Add(MMUtils.ActiveDocument, list);
                }
            }
            // Refresh BubbleBookmarks stick
            if (StixButton.m_Bookmarks != null && !fromStick)
                StixButton.m_Bookmarks.Init(true);
        }

        void LoadFromMapRecursive(Topic _t)
        {
            if (_t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                string topictype =
                    _t.IsCentralTopic ? Central :
                    _t.IsMainTopic ? Main :
                    _t.IsFloatingTopic ? Float : Normal;

                BookmarkItem item = new BookmarkItem(_t.Text.Trim(), _t.Guid, topictype);
                StixBookmarks.Bookmarks.Add(item);
                listBookmarks.Items.Add(item);
            }
            foreach (Topic t in _t.AllSubTopics)
                LoadFromMapRecursive(t);
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsMore.Items)
                item.Visible = true;

            cmsMore.Show(Cursor.Position);
        }

        /// <summary>
        /// Check if listbox clicked on an item or empty place
        /// </summary>
        private void ListBookmarks_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var item = listBookmarks.IndexFromPoint(e.Location);
                if (item < 0)
                    listboxEmptyClick = true;
            }
        }

        /// <summary>
        /// True if listbox was clicked in empty place (not an item)
        /// </summary>
        bool listboxEmptyClick = false;

        private void listBookmarks_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Mouse right click (listbox item context menu)
            if ((MouseButtons & MouseButtons.Right) != 0)
                return;

            if (listboxEmptyClick)
            {
                listboxEmptyClick = false;
                return; // empty place clicked, not an item
            }

            BookmarkItem item = (BookmarkItem)listBookmarks.SelectedItem;
            if (item == null) return;

            Topic t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
            if (t == null)
            {
                MessageBox.Show(Utils.getString("bookmarks.topic.noexists"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                listBookmarks.Items.Remove(listBookmarks.SelectedItem);
            }
            else
            {
                if (!t.IsSelected)
                {
                    t.SelectOnly();
                    t.SnapIntoView();
                }
            }
        }

        /// <summary>
        /// Adds bookmark to selected topic
        /// </summary>
        /// <param name="aTopic">Topic to set bookmark to</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t == null) return;

            if (t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                MessageBox.Show(Utils.getString("bookmarks.addbookmark.error"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");

            if (StixBookmarks.BookmarkedDocuments.ContainsKey(MMUtils.ActiveDocument))
                StixBookmarks.BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

            Init();
        }

        // Delete selected in this dialog bookmark
        private void btnDelete_Click()
        {
            if (listBookmarks.SelectedItem == null) return;

            BookmarkItem _item = listBookmarks.SelectedItem as BookmarkItem;
            if (_item != null)
            {
                Topic t = MMUtils.ActiveDocument.FindByGuid(_item.TopicGuid) as Topic;
                if (t != null)
                    t.GetAttributes(ATTR_NAMESPACE).DeleteAll();

                var item = StixBookmarks.Bookmarks.Find(x => x.TopicGuid == _item.TopicGuid);
                if (item != null)
                {
                    StixBookmarks.Bookmarks.Remove(item);
                    StixBookmarks.BookmarkedDocuments[MMUtils.ActiveDocument].Clear();
                    StixBookmarks.BookmarkedDocuments[MMUtils.ActiveDocument].AddRange(StixBookmarks.Bookmarks);
                    Init(false);
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            if (StixBookmarks.BookmarkedDocuments.Keys.Contains(MMUtils.ActiveDocument))
            {
                Topic t;
                foreach (BookmarkItem item in StixBookmarks.BookmarkedDocuments[MMUtils.ActiveDocument])
                {
                    t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
                    if (t != null)
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                }
                t = null;
                StixBookmarks.BookmarkedDocuments.Remove(MMUtils.ActiveDocument);
            }
            else
            {
                foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
                {
                    if (t.ContainsAttributesNamespace(ATTR_NAMESPACE))
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                }
            }
            Init(false, true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            StixButton.m_BookmarkList = null;
            this.Close();
        }

        // Right click on bookmark's list (calls item context menu)
        private void listBookmarks_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var item = listBookmarks.IndexFromPoint(e.Location);
                if (item < 0)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
                return;
            }

            if (e.Button != MouseButtons.Right) return;

            var index = listBookmarks.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                _selectedBookmark = listBookmarks.Items[index];
                listBookmarks.SelectedItem = _selectedBookmark;
                contextMenuStrip.Show(Cursor.Position);
                contextMenuStrip.Visible = true;
            }
            else
            {
                contextMenuStrip.Visible = false;
            }
        }

        // Click on the "Delete" command in the item context menu = remove this bookmark from map topic
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            btnDelete_Click();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "BookmarksStick.htm");
        }

        private void listBox_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.Location);

            if (index >= 0 && index < lb.Items.Count)
            {
                string toolTipString = (lb.Items[index] as BookmarkItem).TopicName;

                // check if tooltip text coincides with the current one,
                // if so, do nothing
                if (toolTip1.GetToolTip(lb) != toolTipString)
                    toolTip1.SetToolTip(lb, toolTipString);
            }
            else
                toolTip1.Hide(lb);
        }

        private void listBookmarks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (listBookmarks.Items.Count < 1)
                return;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index,
                                e.State ^ DrawItemState.Selected, e.ForeColor,
                                System.Drawing.Color.Lavender); // selected item color

            e.DrawBackground();
            listBookmarks.ItemHeight = listBookmarks.Font.Height;
            Font central = new Font(listBookmarks.Font, FontStyle.Bold);

            if (e.Index < 0) return;
            BookmarkItem _data = listBookmarks.Items[e.Index] as BookmarkItem;

            if (_data.TopicType == "")
                e.Graphics.DrawString(_data.TopicName, central, Brushes.Black, e.Bounds);
            if (_data.TopicType == Main)
                e.Graphics.DrawString(_data.TopicName, listBookmarks.Font, Brushes.Blue, e.Bounds);
            else if (_data.TopicType == Float)
                e.Graphics.DrawString(_data.TopicName, listBookmarks.Font, Brushes.Magenta, e.Bounds);
            else if (_data.TopicType == Normal)
                e.Graphics.DrawString(_data.TopicName, listBookmarks.Font, Brushes.Black, e.Bounds); 
            
            e.DrawFocusRectangle();
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

        private object _selectedBookmark;
        private readonly ContextMenuStrip contextMenuStrip;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static string ATTR_NAMESPACE = "PALMAROSS_EXPRESSBOOKMARKS";
        public static string ATTR_BOOKMARKED = "BOOKMARKED";

        const string Central = "central", Float = "float", Main = "main", Normal = "normal";
    }
}
