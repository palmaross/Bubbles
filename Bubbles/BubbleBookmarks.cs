using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class BubbleBookmarks : Form
    {
        public BubbleBookmarks(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            BubblesButton.m_Bookmarks = this;

            this.Tag = ID;
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "BookmarksStick.htm.htm");

            //toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(BookmarkList, Utils.getString("bookmarks.contextmenu.list"));
            toolTip1.SetToolTip(pictureHandle, stickname);

            MinLength = this.Width; RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            cmsDelete.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["BI_addbookmark"].Text = Utils.getString("bookmarks.contextmenu.add");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_addbookmark"], "book_add.png");

            cmsDelete.Items["BI_delete"].Text = Utils.getString("bookmarks.contextmenu.delete");

            contextMenuStrip1.Items["BI_main"].Text = Utils.getString("bookmarks.contextmenu.maintopics");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_main"], "bookmarkMain.png");

            contextMenuStrip1.Items["BI_deletemain"].Text = Utils.getString("bookmarks.contextmenu.deletemaintopics");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_deletemain"], "deletemain.png");

            contextMenuStrip1.Items["BI_bookmarklist"].Text = Utils.getString("bookmarks.contextmenu.list");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_bookmarklist"], "list.png");

            StickUtils.SetCommonContextMenu(contextMenuStrip1, StickUtils.typebookmarks);

            this.MouseDown += Move_Stick;
            pictureHandle.MouseDown += Move_Stick;
            Manage.Click += Manage_Click;
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();

            // Show command popup
            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typebookmarks);            

            Init();

            if (collapsed) {
                collapsed = false; Collapse();  }
        }

        public void Init(bool fromList = false, bool deleteall = false)
        {
            prev = null; RealLength = MinLength; Bookmarks.Clear();

            if (!collapsed)
            {
                if (orientation == "H")
                    this.Width = MinLength;
                else
                    this.Height = MinLength;
            }

            foreach (PictureBox p in this.Controls.OfType<PictureBox>().Reverse())
            {
                if (p.Tag != null && p.Name != "pCentral")
                    p.Dispose();
            }

            if (MMUtils.ActiveDocument == null)
            {
                toolTip1.SetToolTip(pCentral, "");
                return;
            }
            else
            {
                Topic cTopic = MMUtils.ActiveDocument.CentralTopic;
                pCentral.Tag = new BookmarkItem(cTopic.Text, cTopic.Guid, false);
                toolTip1.SetToolTip(pCentral, cTopic.Text);
                pCentral.MouseClick += Icon_Click;
                cTopic = null;

                if (deleteall)
                {
                    if (BubblesButton.m_BookmarkList != null && !fromList)
                        BubblesButton.m_BookmarkList.Init(true, true);
                    return;
                }
            }

            if (BookmarkedDocuments.Keys.Contains(MMUtils.ActiveDocument))
            {
                foreach (BookmarkItem item in BookmarkedDocuments[MMUtils.ActiveDocument])
                    AddIcon(item.TopicName, item.TopicGuid, item.MainTopic, item.FloatTopic);
            }
            else
            {
                // Add bookmarks exсept floating topics
                LoadFromMapRecursive(MMUtils.ActiveDocument.CentralTopic);

                // Add bookmarks from the floating topic branches
                foreach (Topic _t in MMUtils.ActiveDocument.AllFloatingTopics)
                    LoadFromMapRecursive(_t);

                if (Bookmarks.Count > 0)
                {
                    List<BookmarkItem> list = new List<BookmarkItem>();
                    list.AddRange(Bookmarks);
                    BookmarkedDocuments.Add(MMUtils.ActiveDocument, list);
                }
            }

            // If stick is collapsed, do not set the resulting width
            if (!collapsed)
            {
                if (orientation == "H")
                    this.Width = RealLength;
                else
                    this.Height = RealLength;
            }

            // Refresh Bookmarks list
            if (BubblesButton.m_BookmarkList != null && !fromList)
                BubblesButton.m_BookmarkList.Init(true);
        }

        void LoadFromMapRecursive(Topic _t)
        {
            if (_t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                AddIcon(_t.Text.Trim(), _t.Guid, _t.IsMainTopic, _t.IsFloatingTopic);
            }
            foreach (Topic t in _t.AllSubTopics)
                LoadFromMapRecursive(t);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;
            contextMenuStrip1.Show(Cursor.Position);
        }

        private void Move_Stick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_addbookmark")
            {
                if (MMUtils.ActiveDocument == null) return;
                AddBookmark();
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                BookmarkItem _item = (BookmarkItem)selectedIcon.Tag;
                if (_item != null)
                {
                    Topic t = MMUtils.ActiveDocument.FindByGuid(_item.TopicGuid) as Topic;
                    if (t != null)
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAll();

                    var item = Bookmarks.Find(x => x.TopicGuid == _item.TopicGuid);
                    if (item != null)
                    {
                        Bookmarks.Remove(item);
                        BookmarkedDocuments[MMUtils.ActiveDocument].Clear();
                        BookmarkedDocuments[MMUtils.ActiveDocument].AddRange(Bookmarks);
                        Init(false);
                    }
                }
            }
            else if (e.ClickedItem.Name == "BI_main")
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");

                if (BookmarkedDocuments.ContainsKey(MMUtils.ActiveDocument))
                    BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

                Init(false);
            }
            else if (e.ClickedItem.Name == "BI_deletemain")
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

                Init(false);
            }
            else if (e.ClickedItem.Name == "BI_bookmarklist")
            {
                BookmarkList_Click(null, null);
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                if (MMUtils.ActiveDocument == null) return;

                if (BookmarkedDocuments.Keys.Contains(MMUtils.ActiveDocument))
                {
                    Topic t;
                    foreach (BookmarkItem item in BookmarkedDocuments[MMUtils.ActiveDocument])
                    {
                        t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
                        if (t != null) 
                            t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                    }
                    t = null;
                    BookmarkedDocuments.Remove(MMUtils.ActiveDocument);
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
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                BubblesButton.m_Bookmarks = null;
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "BookmarksStick.htm");
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
            orientation = StickUtils.RotateStick(this, Manage, orientation, BookmarkList);
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

                collapseState = this.Location; // remember collapsed location
                collapseOrientation = orientation;
                StickUtils.Expand(this, RealLength, orientation, contextMenuStrip1);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, contextMenuStrip1);
                collapsed = true;
                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                {
                    this.Location = collapseState; // restore collapsed location
                    if (orientation != collapseOrientation) Rotate();
                }
            }
        }
        Point collapseState = new Point(0, 0);
        string collapseOrientation = "N";

        void AddIcon(string tooltip, string guid, bool maintopic = false, bool floattopic = false)
        {
            PictureBox pBox = new PictureBox();
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.MouseClick += Icon_Click;

            if (maintopic)
                pBox.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "bookmarkMain.png");
            else if (floattopic)
                pBox.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "bookmarkFloat.png");
            else
            {
                pBox.Size = p2.Size;
                pBox.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "bookmarkRest.png");
            }

            this.Controls.Add(pBox);
            pBox.Visible = true;
            toolTip1.SetToolTip(pBox, tooltip);
            pBox.BringToFront();

            pBox.Tag = new BookmarkItem(tooltip, guid, maintopic, floattopic);
            Bookmarks.Add((BookmarkItem)pBox.Tag);

            if (orientation == "H")
            {
                int locX;
                if (prev == null)
                    locX = p1.Location.X;
                else
                    locX = prev.Location.X + prev.Width + p3.Height;

                if (maintopic || floattopic)
                    pBox.Location = new Point(locX, p1.Location.Y);
                else
                    pBox.Location = new Point(locX, p2.Location.Y);

                if (Bookmarks.Count > 2)
                    RealLength += pBox.Width + p3.Height;
            }
            else
            {
                int locY;
                if (prev == null)
                    locY = p1.Location.Y;
                else
                    locY = prev.Location.Y + prev.Width + p3.Height;

                if (maintopic || floattopic)
                    pBox.Location = new Point(p1.Location.X, locY);
                else
                    pBox.Location = new Point(p2.Location.X, locY);

                if (Bookmarks.Count > 2)
                    RealLength += pBox.Width + p3.Height;
            }
            prev = pBox;
        }
        PictureBox prev = null;

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                BookmarkItem item = selectedIcon.Tag as BookmarkItem;
                Topic t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
                if (t != null)
                {
                    t.SelectOnly();
                    t.SnapIntoView();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (selectedIcon == pCentral) return;

                cmsDelete.Items["BI_delete"].Visible = true;
                cmsDelete.Show(Cursor.Position);
            }
        }

        public void AddBookmark()
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

            if (BookmarkedDocuments.ContainsKey(MMUtils.ActiveDocument))
                BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

            prev = null;
            Init();
        }

        public void BookmarkList_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_BookmarkList == null)
            {
                BubblesButton.m_BookmarkList = new BookmarkListDlg();

                // Get bookmark list location
                Rectangle child = BubblesButton.m_BookmarkList.RectangleToScreen(BubblesButton.m_BookmarkList.ClientRectangle);
                BubblesButton.m_BookmarkList.Location = StickUtils.GetChildLocation(this, child, orientation, "bookmarks");
                
                BubblesButton.m_BookmarkList.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            BubblesButton.m_BookmarkList.Init(true);
        }

        public static Dictionary<Document, List<BookmarkItem>> BookmarkedDocuments = new Dictionary<Document, List<BookmarkItem>>();
        public static List<BookmarkItem> Bookmarks = new List<BookmarkItem>();
        
        PictureBox selectedIcon = null;
        string orientation = "H";
        bool collapsed = false;

        int MinLength, RealLength;

        // For MouseDown event
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static string ATTR_NAMESPACE = "PALMAROSS_EXPRESSBOOKMARKS";
        public static string ATTR_BOOKMARKED = "BOOKMARKED";
    }

    internal class BookmarkItem
    {
        public BookmarkItem(string topicName, string topicGuid, bool maintopic = false, bool floattopic = false)
        {
            TopicName = topicName;
            TopicGuid = topicGuid;
            MainTopic = maintopic;
            FloatTopic = floattopic;
        }
        public string TopicName = "";
        public string TopicGuid = "";
        public bool MainTopic;
        public bool FloatTopic;
    }
}
