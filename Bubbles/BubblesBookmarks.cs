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
        public BubbleBookmarks()
        {
            InitializeComponent();

            //helpProvider1.HelpNamespace = MMMapNavigator.dllPath + "MapNavigator.chm";
            //helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            //helpProvider1.SetHelpKeyword(this, "bookmarks_express.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(AddBookmark, Utils.getString("bookmarks.addbookmark.tooltip"));
            toolTip1.SetToolTip(pictureHandle, Utils.getString("bookmarks.bubble.tooltip"));

            string location = Utils.getRegistry("PositionBookmarks", "");
            orientation = Utils.getRegistry("OrientationBookmarks", "H");

            MinLength = this.Width;
            MaxLength = this.Width * 5;
            Thickness = this.Height;
            panel1MinLength = panel1.Width;

            this.MinimumSize = this.Size;
            this.MaximumSize = new Size(MaxLength, Thickness);

            if (orientation == "V")
            {
                orientation = "H";
                RotateBubble();
                pCentral.Location = new Point(pCentral.Location.Y, pCentral.Location.X);
            }

            if (String.IsNullOrEmpty(location))
                Location = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            else
            {
                string[] xy = location.Split(',');
                Location = new Point(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));

                if (!Utils.IsOnScreen(Location, this.Size))
                    Location = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            contextMenuStrip1.Items["BI_delete"].Text = MMUtils.getString("bookmarks.contextmenu.delete");
            contextMenuStrip1.Items["BI_main"].Text = MMUtils.getString("bookmarks.contextmenu.maintopics");
            contextMenuStrip1.Items["BI_deletemain"].Text = MMUtils.getString("bookmarks.contextmenu.deletemaintopics");

            contextMenuStrip1.Items["BI_rotate"].Text = MMUtils.getString("float_icons.contextmenu.rotate");
            contextMenuStrip1.Items["BI_close"].Text = MMUtils.getString("float_icons.contextmenu.close");
            contextMenuStrip1.Items["BI_help"].Text = MMUtils.getString("float_icons.contextmenu.help");
            contextMenuStrip1.Items["BI_store"].Text = MMUtils.getString("float_icons.contextmenu.settings");

            panel1.MouseDown += Panel1_MouseDown;
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            Manage.Click += Manage_Click;

            if (orientation == "H")
                this.Size = new Size(MinLength, Thickness);
            else
                this.Size = new Size(Thickness, MinLength);
            Init();
        }

        public void Init()
        {
            List<BookmarkItem> BookmarkList = new List<BookmarkItem>();
            BookmarkList.AddRange(Bookmarks);
            Bookmarks.Clear();

            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>().Reverse())
            {
                if (p.Name != "p1")
                    p.Dispose();
            }

            if (orientation == "H")
            {
                this.Size = new Size(MinLength, Thickness);
                panel1.Width = panel1MinLength;
            }
            else
            {
                this.Size = new Size(Thickness, MinLength);
                panel1.Height = panel1MinLength;
            }

            if (MMUtils.ActiveDocument == null)
                return;

            Topic cTopic = MMUtils.ActiveDocument.CentralTopic;
            pCentral.Tag = new BookmarkItem(cTopic.Text, cTopic.Guid, false);
            toolTip1.SetToolTip(pCentral, cTopic.Text);
            pCentral.MouseClick += Icon_Click;
            cTopic = null;

            if (BookmarkedDocuments.Keys.Contains(MMUtils.ActiveDocument))
            {
                foreach (BookmarkItem item in BookmarkList)
                    AddIcon(item.TopicName, item.TopicGuid, item.MainTopic, item.FloatTopic);
            }
            else
            {
                // Add bookmarks exept floating topics
                LoadFromMapRecursive(MMUtils.ActiveDocument.CentralTopic);

                // Add bookmarks from the floating topic branches
                foreach (Topic _t in MMUtils.ActiveDocument.AllFloatingTopics)
                    LoadFromMapRecursive(_t);

                BookmarkedDocuments.Add(MMUtils.ActiveDocument, Bookmarks);
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
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_delete")
            {
                BookmarkItem _item = (BookmarkItem)selectedIcon.Tag;
                if (_item != null)
                {
                    Topic t = MMUtils.ActiveDocument.FindByGuid(_item.TopicGuid) as Topic;
                    if (t != null)
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAll();

                    Bookmarks.Remove(_item);
                    selectedIcon.Dispose();
                    prev = null;
                    Init();
                }
            }
            else if (e.ClickedItem.Name == "BI_main")
            {
                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");

                if (BookmarkedDocuments.ContainsKey(MMUtils.ActiveDocument))
                    BookmarkedDocuments.Remove(MMUtils.ActiveDocument);

                prev = null;
                Init();
            }
            else if (e.ClickedItem.Name == "BI_deletemain")
            {
                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                    t.GetAttributes(ATTR_NAMESPACE).DeleteAll();
                BookmarkedDocuments.Remove(MMUtils.ActiveDocument);
                prev = null;
                Init();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                this.Hide();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                RotateBubble();

                foreach (PictureBox p in panel1.Controls.OfType<PictureBox>())
                {
                    p.Location = new Point(p.Location.Y, p.Location.X);
                }
            }
            else if (e.ClickedItem.Name == "BI_help")
            {

            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                int x = this.Location.X;
                int y = this.Location.Y;

                Utils.setRegistry("OrientationBookmarks", orientation);
                Utils.setRegistry("PositionBookmarks", x.ToString() + "," + y.ToString());
            }
        }

        void RotateBubble()
        {
            if (orientation == "H")
            {
                orientation = "V";
                panel1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                Manage.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            }
            else
            {
                orientation = "H";
                panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                Manage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            int thisWidth = this.Width;
            int thisHeight = this.Height;

            Size panel1Size = new Size(panel1.Height, panel1.Width);
            Point panel1Location = new Point(panel1.Location.Y, panel1.Location.X);
            Point ManageLocation = new Point(Manage.Location.Y, Manage.Location.X);
            Point pCentralLocation = new Point(pCentral.Location.Y, pCentral.Location.X);
            Point addBookmarkLocation = new Point(AddBookmark.Location.Y, AddBookmark.Location.X);

            if (orientation == "H")
            {
                this.MinimumSize = new Size(MinLength, Thickness);
                this.MaximumSize = new Size(MaxLength, Thickness);
            }
            else
            {
                this.MinimumSize = new Size(Thickness, MinLength);
                this.MaximumSize = new Size(Thickness, MaxLength);
            }

            this.Size = new Size(thisHeight, thisWidth);

            pCentral.Location = pCentralLocation;
            panel1.Size = panel1Size;
            panel1.Location = panel1Location;
            AddBookmark.Location = addBookmarkLocation;
            Manage.Location = ManageLocation;
        }

        PictureBox FindByTag(BookmarkItem item)
        {
            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>())
            {
                if (p.Tag != null && (p.Tag as BookmarkItem) == item)
                    return p;
            }
            return null;
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
                pBox.Size = p2.Size;
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
                    pBox.Location = new Point(locX, p2.Location.Y);

                if (Bookmarks.Count > 2)
                    this.Width += pBox.Width + p3.Height;
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
                    pBox.Location = new Point(p2.Location.Y, locY);

                if (Bookmarks.Count > 2)
                    this.Height += pBox.Width + p3.Height;
            }
            prev = pBox;
        }
        PictureBox prev = null;

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

        private void Icon_Click(object sender, MouseEventArgs e)
        {
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

                if (selectedIcon == pCentral)
                    return;

                contextMenuStrip1.Items["BI_delete"].Visible = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void AddBookmark_Click(object sender, EventArgs e)
        {
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

        int MinLength, MaxLength, Thickness, panel1MinLength;

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
