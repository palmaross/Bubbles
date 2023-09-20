using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bubbles
{
    internal partial class BubblesBookmarks : Form
    {
        public BubblesBookmarks()
        {
            InitializeComponent();

            //helpProvider1.HelpNamespace = MMMapNavigator.dllPath + "MapNavigator.chm";
            //helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            //helpProvider1.SetHelpKeyword(this, "bookmarks_express.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(Manage, Utils.getString("bookmarks.addbookmark.tooltip"));
            toolTip1.SetToolTip(Manage, Utils.getString("bookmarks.forward.tooltip"));
            toolTip1.SetToolTip(Manage, Utils.getString("bookmarks.back.tooltip"));

            string location = Utils.getRegistry("PositionBookmarks", "");
            orientation = Utils.getRegistry("OrientationBookmarks", "H");

            MinLength = this.Width;
            MaxLength = this.Width * 3;
            Thickness = this.Height;

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
            }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            contextMenuStrip1.Items["BI_new"].Text = MMUtils.getString("bookmarks.contextmenu.delete");
            contextMenuStrip1.Items["BI_delete"].Text = MMUtils.getString("bookmarks.contextmenu.maintopics");
            contextMenuStrip1.Items["BI_rename"].Text = MMUtils.getString("bookmarks.contextmenu.deletemaintopics");

            contextMenuStrip1.Items["BI_rotate"].Text = MMUtils.getString("float_icons.contextmenu.rotate");
            contextMenuStrip1.Items["BI_close"].Text = MMUtils.getString("float_icons.contextmenu.close");
            contextMenuStrip1.Items["BI_help"].Text = MMUtils.getString("float_icons.contextmenu.help");
            contextMenuStrip1.Items["BI_store"].Text = MMUtils.getString("float_icons.contextmenu.settings");

            panel1.MouseClick += Panel1_MouseClick;
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
            Bookmarks.Clear();
            foreach (PictureBox p in panel1.Controls)
                if (p.Name != "p1")
                    p.Dispose();

            if (MMUtils.ActiveDocument == null)
                return;

            pCentral.Tag = new BookmarkItem(MMUtils.ActiveDocument.CentralTopic.Text, "", false);

            if (BookmarkedDocuments.Keys.Contains(MMUtils.ActiveDocument))
            {
                foreach (BookmarkItem item in Bookmarks)
                    AddIcon(item.TopicName, item.TopicGuid, item.MainTopic);
            }
            else
            {
                // Add Main Topics
                foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
                {
                    Bookmarks.Add(new BookmarkItem(t.Text.Trim(), t.Guid, false));
                    AddIcon(t.Text, t.Guid, true);
                }

                LoadFromMapRecursive(MMUtils.ActiveDocument.CentralTopic);

                foreach (Topic _t in MMUtils.ActiveDocument.AllFloatingTopics)
                    LoadFromMapRecursive(_t);
            }
        }

        void LoadFromMapRecursive(Topic _t)
        {
            if (_t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                if (!_t.IsMainTopic) // main topics are already handled
                {
                    Bookmarks.Add(new BookmarkItem(_t.Text.Trim(), _t.Guid, false));
                    AddIcon(_t.Text.Trim(), _t.Guid, false);
                }
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

            }
            else if (e.ClickedItem.Name == "BI_maintopics")
            {

            }
            else if (e.ClickedItem.Name == "BI_deletemaintopics")
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

                Utils.setRegistry("OrientationIcons", orientation);
                Utils.setRegistry("PositionIcons", x.ToString() + "," + y.ToString());
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

            panel1.Size = panel1Size;
            panel1.Location = panel1Location;
            Manage.Location = ManageLocation;
        }

        PictureBox FindByTag(IconItem item)
        {
            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>())
            {
                if (p.Tag != null && (p.Tag as IconItem) == item)
                    return p;
            }
            return null;
        }

        void AddIcon(string tooltip, string guid, bool maintopic = false)
        {
            int iconOrder = icondist.Width * Bookmarks.Count;
            PictureBox pBox = new PictureBox();
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.MouseClick += Icon_Click;

            if (maintopic)
                pBox.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "bookmarkMain.png");
            else
                pBox.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "bookmarkRest.png");

            panel1.Controls.Add(pBox);
            pBox.Visible = true;
            toolTip1.SetToolTip(pBox, tooltip);
            pBox.BringToFront();

            pBox.Tag = new BookmarkItem(tooltip, guid);
            Bookmarks.Add((BookmarkItem)pBox.Tag);

            if (orientation == "H")
            {
                pBox.Location = new Point(p1.Location.X + iconOrder, p1.Location.Y);
                if (Bookmarks.Count > 2)
                    this.Width += icondist.Width;
            }
            else
            {
                pBox.Location = new Point(p1.Location.X, p1.Location.Y + iconOrder);
                if (Bookmarks.Count > 2)
                    this.Height += icondist.Width;
            }
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

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                IconItem item = selectedIcon.Tag as IconItem;
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                contextMenuStrip1.Items["BI_new"].Visible = true;
                contextMenuStrip1.Items["BI_delete"].Visible = true;
                contextMenuStrip1.Items["BI_rename"].Visible = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;

                contextMenuStrip1.Items["BI_delete"].Visible = false;
                contextMenuStrip1.Items["BI_rename"].Visible = false;

                contextMenuStrip1.Show(Cursor.Position);
            }
        }
        
        private void AddBookmark_Click(object sender, EventArgs e)
        {
            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                MessageBox.Show("", "", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");

                // add bookmark to bubble
            }
        }

        private void PreviousBookmark_Click(object sender, EventArgs e)
        {

        }

        private void NextBookmark_Click(object sender, EventArgs e)
        {

        }

        Dictionary<Document, List<BookmarkItem>> BookmarkedDocuments = new Dictionary<Document, List<BookmarkItem>>();

        public static List<BookmarkItem> Bookmarks = new List<BookmarkItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";

        int MinLength, MaxLength, Thickness;

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
        public BookmarkItem(string topicName, string topicGuid, bool maintopic)
        {
            TopicName = topicName;
            TopicGuid = topicGuid;
            MainTopic = maintopic;
        }
        public string TopicName = "";
        public string TopicGuid = "";
        public bool MainTopic = false;
    }
}
