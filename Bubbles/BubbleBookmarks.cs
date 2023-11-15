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

            this.Tag = ID;
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "BookmarksStick.htm.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(AddBookmark, Utils.getString("bookmarks.addbookmark.tooltip"));
            toolTip1.SetToolTip(pictureHandle, Utils.getString("bookmarks.bubble.tooltip"));

            MinLength = this.Width; RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }


            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["BI_addbookmark"].Text = Utils.getString("bookmarks.contextmenu.add");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_addbookmark"], p2, "book_add.png");

            contextMenuStrip1.Items["BI_delete"].Text = Utils.getString("bookmarks.contextmenu.delete");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_delete"], p2, "deleteall.png");

            contextMenuStrip1.Items["BI_main"].Text = Utils.getString("bookmarks.contextmenu.maintopics");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_main"], p2, "bookmarkMain.png");

            contextMenuStrip1.Items["BI_deletemain"].Text = Utils.getString("bookmarks.contextmenu.deletemaintopics");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_deletemain"], p2, "deletemain.png");

            StickUtils.SetCommonContextMenu(contextMenuStrip1, p2, StickUtils.typebookmarks);

            panel1.MouseDown += Panel1_MouseDown;
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            Manage.Click += Manage_Click;
            pictureHandle.MouseDoubleClick += PictureHandle_MouseDoubleClick;

            Init();

            if (collapsed) {
                collapsed = false; Collapse();  }
        }

        public void Init(bool deleteall = false)
        {
            prev = null; Bookmarks.Clear(); RealLength = MinLength;

            if (!collapsed)
            {
                if (orientation == "H")
                    this.Width = MinLength;
                else
                    this.Height = MinLength;
            }

            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>().Reverse())
            {
                if (p.Name != "p1")
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
                    return;
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

                List<BookmarkItem> list = new List<BookmarkItem>();
                list.AddRange(Bookmarks);
                BookmarkedDocuments.Add(MMUtils.ActiveDocument, list);
            }

            // If stick is collapsed, do not set the resulting width
            if (!collapsed)
            {
                if (orientation == "H")
                    this.Width = RealLength;
                else
                    this.Height = RealLength;
            }
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

            contextMenuStrip1.Items["BI_delete"].Visible = false;
            contextMenuStrip1.Show(Cursor.Position);
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

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_addbookmark")
            {
                if (MMUtils.ActiveDocument == null) return;
                AddBookmark_Click(null, null);
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                BookmarkItem _item = (BookmarkItem)selectedIcon.Tag;
                if (_item != null)
                {
                    Topic t = MMUtils.ActiveDocument.FindByGuid(_item.TopicGuid) as Topic;
                    if (t != null)
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAll();

                    Bookmarks.Remove(_item);
                    selectedIcon.Dispose();
                    Init();
                }
            }
            else if (e.ClickedItem.Name == "BI_main")
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");

                if (BookmarkedDocuments.ContainsKey(MMUtils.ActiveDocument))
                    BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

                Init();
            }
            else if (e.ClickedItem.Name == "BI_deletemain")
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

                Init();
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

                Init(true);
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
            orientation = StickUtils.RotateStick(this, p1, panel1, Manage, orientation, false, AddBookmark);
            pCentral.Location = new Point(pCentral.Location.Y, pCentral.Location.X);
        }

        private void PictureHandle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Collapse();
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
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation);
                collapsed = true;
                contextMenuStrip1.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.expand");
            }
        }

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
                pBox.Size = p4.Size;
                pBox.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "bookmarkRest.png");
            }

            panel1.Controls.Add(pBox);
            pBox.Visible = true;
            toolTip1.SetToolTip(pBox, tooltip);
            pBox.BringToFront();

            pBox.Tag = new BookmarkItem(tooltip, guid, maintopic, floattopic);
            Bookmarks.Add((BookmarkItem)pBox.Tag);

            int locX;
            if (prev == null)
                locX = 0;
            else
                locX = prev.Location.X + prev.Width + p3.Height;

            if (orientation == "H")
            {
                if (maintopic || floattopic)
                    pBox.Location = new Point(locX, p1.Location.Y);
                else
                    pBox.Location = new Point(locX, p4.Location.Y);

                if (Bookmarks.Count > 2)
                    RealLength += pBox.Width + p3.Height;
            }
            else
            {
                int locY;
                if (prev == null)
                    locY = 0;
                else
                    locY = prev.Location.Y + prev.Width + p3.Height;

                if (maintopic || floattopic)
                    pBox.Location = new Point(p1.Location.Y, locY);
                else
                    pBox.Location = new Point(p4.Location.Y, locY);

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
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                if (selectedIcon == pCentral) return;

                contextMenuStrip1.Items["BI_delete"].Visible = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void AddBookmark_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t == null)
                return;

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

        public Dictionary<Document, List<BookmarkItem>> BookmarkedDocuments = new Dictionary<Document, List<BookmarkItem>>();

        public static List<BookmarkItem> Bookmarks = new List<BookmarkItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";
        bool manage = false;
        bool collapsed = false;

        int MinLength, RealLength;

        // For this_MouseDown
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
