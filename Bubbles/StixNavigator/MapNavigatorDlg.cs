using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace Bubbles
{
    public partial class MapNavigatorDlg : Form
    {
        public MapNavigatorDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "NavigatorWindow.htm");

            lblTitle.Text = Utils.getString("MapNavigatorDlg.Title");
            panelControls.BackColor = Utils.header;
            tabMain.Text = Utils.getString("MapNavigatorDlg.tabMain");
            tabBookmarks.Text = Utils.getString("MapNavigatorDlg.tabBookmarks");
            tabNavigation.Text = Utils.getString("MapNavigatorDlg.tabNavigation");
            linkAddBookmark.Text = Utils.getString("bookmarks.contextmenu.addbookmark");
            linkDeleteAllBookmarks.Text = Utils.getString("bookmarks.contextmenu.delete.allbookmarks");
            linkDeleteAllPositions.Text = Utils.getString("bookmarks.contextmenu.delete.allpositions");

            linkDeleteAllBookmarks.Location = new Point(linkAddBookmark.Width + p1.Width, linkAddBookmark.Location.Y);

            toolTip1.SetToolTip(btnClose, Utils.getString("button.close"));
            toolTip1.SetToolTip(btnHelp, Utils.getString("button.help"));
            toolTip1.SetToolTip(pRefresh, Utils.getString("MapNavigatorDlg.refresh"));

            // ListBox item context menu
            contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem DeleteBookmark = 
                new ToolStripMenuItem { Text = Utils.getString("bookmarks.contextmenu.delete.bookmark") };
            DeleteBookmark.Name = "DeleteBookmark";
            DeleteBookmark.Click += DeleteBookmark_Click;

            ToolStripMenuItem DeletePosition =
                new ToolStripMenuItem { Text = Utils.getString("bookmarks.contextmenu.delete.position") };
            DeletePosition.Name = "DeletePosition";
            DeletePosition.Click += DeletePosition_Click;

            contextMenuStrip.Items.Add(DeleteBookmark);
            contextMenuStrip.Items.Add(DeletePosition);

            this.MinimumSize = new Size((int)(this.Width / 1.2), this.Height / 2);
            this.MaximumSize = new Size(this.Width * 2, Screen.AllScreens.Max(s => s.Bounds.Height));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            //listBookmarks.MouseMove += listBox_MouseMove;
            lblTitle.MouseDown += PanelControls_MouseDown;
            panelControls.MouseDown += PanelControls_MouseDown;
            listBookmarks.DrawItem += listBookmarks_DrawItem;
            listPositions.DrawItem += listBookmarks_DrawItem;

            if (Utils.scalingFactor == 1)
                tabControl1.Width -= 1; tabControl1.Height -= 1;

            this.Paint += BookmarkListDlg_Paint; // paint the border

            defaultHeight = this.Height;
            defaultItemHeight = listPositions.ItemHeight;
            int fontsize = (int)listMainTopics.Font.Size;
            if (Utils.WindowFontSize != fontsize)
            {
                listMainTopics.Font = new Font(listMainTopics.Font.FontFamily, Utils.WindowFontSize * 0.75F);
                listBookmarks.Font = new Font(listBookmarks.Font.FontFamily, Utils.WindowFontSize * 0.75F);
                listPositions.Font = new Font(listPositions.Font.FontFamily, Utils.WindowFontSize * 0.75F);
            }

            Init();

            // Get listbox item height and adjust form height depending on listPositions height
            int itemHeight = (int)this.CreateGraphics().MeasureString("0", listPositions.Font, TextRenderer.MeasureText("0", new Font(listPositions.Font.FontFamily, Utils.WindowFontSize * 0.75F))).Height;
            int diff = (itemHeight - (int)(defaultItemHeight * Utils.scalingFactor)) * listPositions.Items.Count;
            this.Height += diff + 1;
        }
        public int defaultHeight;
        public int defaultItemHeight;

        private void PanelControls_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void BookmarkListDlg_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Init bookmarks listbox
        /// </summary>
        /// <param name="fromStix"></param>
        /// <param name="deleteall"></param>
        public void Init(bool fromStix = false, bool deleteall = false)
        {
            InitMainTopics();
            InitBookmarks();
            InitPositions();
        }

        public void InitMainTopics(bool fromStix = false)
        {
            listMainTopics.Items.Clear();
            if (MMUtils.ActiveDocument == null) return;

            Topic cTopic = MMUtils.ActiveDocument.CentralTopic;
            lblCentralTopic.Text = cTopic.Text.Replace("\n", " ").Trim();

            foreach (Topic t in cTopic.AllSubTopics)
                listMainTopics.Items.Add(new BookmarkItem(t.Text.Replace("\n", " ").Trim(), t.Guid));

            cTopic = null;

            if (!fromStix && StixMain.m_MapNavigator != null && !StixMain.m_MapNavigator.IsDisposed)
                StixMain.m_MapNavigator.InitMainTopicsContextMenu(true);
        }

        public void InitBookmarks(bool fromStix = false, bool deleteall = false)
        {
            listBookmarks.Items.Clear();

            if (deleteall) // delete all bookmarks clicked
            {
                if (MMUtils.ActiveDocument == null)
                    StixMapNavigator.DocumentBookmarks[MMUtils.ActiveDocument.Guid].Clear();
            }
            else // Fill bokkmarks list
            {
                if (MMUtils.ActiveDocument == null) return;

                if (StixMapNavigator.DocumentBookmarks.Keys.Contains(MMUtils.ActiveDocument.Guid))
                {
                    foreach (BookmarkItem item in StixMapNavigator.DocumentBookmarks[MMUtils.ActiveDocument.Guid])
                        listBookmarks.Items.Add(item);
                }
                else // First time seeing the map. Load bookmarks from map.
                {
                    StixMapNavigator.Bookmarks.Clear();

                    // Add bookmarks exсept floating topics
                    LoadFromMapRecursive(MMUtils.ActiveDocument.CentralTopic);

                    // Add bookmarks from the floating topic branches
                    foreach (Topic _t in MMUtils.ActiveDocument.AllFloatingTopics)
                        LoadFromMapRecursive(_t);

                    if (StixMapNavigator.Bookmarks.Count > 0)
                    {
                        List<BookmarkItem> list = new List<BookmarkItem>();
                        list.AddRange(StixMapNavigator.Bookmarks);
                        StixMapNavigator.DocumentBookmarks.Add(MMUtils.ActiveDocument.Guid, list);
                    }
                }
            }
            // Refresh Stix
            if (!fromStix && StixMain.m_MapNavigator != null && !StixMain.m_MapNavigator.IsDisposed)
                StixMain.m_MapNavigator.InitBookmarksContextMenu(true, deleteall);
        }

        public void InitPositions(bool fromStix = false, bool deleteall = false)
        {
            // Clear positions
            listPositions.Items.Clear();

            // Add empty positions
            for (int i = 1; i < 6; i++)
                listPositions.Items.Add(new PositionItem(Utils.getString("MapNavigatorDlg.addposition.tooltip"), "", i));

            if (deleteall && !fromStix) // delete all positions clicked
            {
                if (MMUtils.ActiveDocument != null)
                    StixMapNavigator.DocumentPositions[MMUtils.ActiveDocument.Guid].Clear();
            }
            else // Init/Update positions list
            {
                if (MMUtils.ActiveDocument == null) return;

                // Fill positions
                if (StixMapNavigator.DocumentPositions.Keys.Contains(MMUtils.ActiveDocument.Guid))
                {
                    if (StixMapNavigator.DocumentPositions.Values.Count > 0)
                    {
                        foreach (PositionItem item in StixMapNavigator.DocumentPositions[MMUtils.ActiveDocument.Guid])
                            if (item.Number != 0)
                                listPositions.Items[item.Number - 1] = item;
                    }
                }
                else
                    StixMapNavigator.DocumentPositions.Add(MMUtils.ActiveDocument.Guid, new List<PositionItem>());
            }
            if (!fromStix && StixMain.m_MapNavigator != null && !StixMain.m_MapNavigator.IsDisposed)
                StixMain.m_MapNavigator.InitPositions(true, deleteall);
        }

        void LoadFromMapRecursive(Topic _t)
        {
            if (_t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                string topictype =
                    _t.IsCentralTopic ? Central :
                    _t.IsMainTopic ? Main :
                    _t.IsFloatingTopic ? Floating : Normal;

                BookmarkItem item = new BookmarkItem(_t.Text.Replace("\n", " ").Trim(), _t.Guid, topictype);
                StixMapNavigator.Bookmarks.Add(item);
                listBookmarks.Items.Add(item);
            }
            foreach (Topic t in _t.AllSubTopics)
                LoadFromMapRecursive(t);
        }

        public void TopicAffected(string docGuid, Topic t, string reason)
        {
            bool isBookmark = t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED);
            string topicGuid = t.Guid;

            string topicText = t.Text;
            string text = topicText;
            if (text.Length > 100) text = text.Substring(0, 100);

            var position = StixMapNavigator.DocumentPositions[docGuid].Find(x => x.TopicGuid == topicGuid);
            var bookmark = StixMapNavigator.DocumentBookmarks[docGuid].Find(x => x.TopicGuid == topicGuid);

            string topicType = t.IsCentralTopic ? Central : t.IsMainTopic ? Main : t.IsFloatingTopic ? Floating : Normal;

            if (topicType == Central)
            {
                lblCentralTopic.Text = topicText;

                if (StixMain.m_MapNavigator != null && StixMain.m_MapNavigator.Visible)
                    StixMain.m_MapNavigator.toolTip1.SetToolTip(StixMain.m_MapNavigator.pCentral, text);
            }
            else if (topicType == Main)
                InitMainTopics();
            else if (topicType == Floating)
                topicType = Floating;

            if (position != null) // Position affected
            {
                if (reason == "text")
                    position.TopicName = topicText;
                else if (reason == "deleted")
                    StixMapNavigator.DocumentPositions[docGuid].Remove(position);

                InitPositions();
            }

            if (isBookmark)
            {
                if (bookmark != null) // Bookmark affected
                {
                    if (reason == "text")
                        bookmark.TopicName = topicText;
                    else if (reason == "deleted")
                        StixMapNavigator.DocumentBookmarks[docGuid].Remove(bookmark);

                    InitBookmarks();
                }

                if (reason == "added")
                {
                    BookmarkItem item = new BookmarkItem(topicText, topicGuid, topicType);
                    StixMapNavigator.DocumentBookmarks[docGuid].Add(item);
                    InitBookmarks();
                }
            }
        }

        private void lblCentralTopic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;
            MMUtils.ActiveDocument.CentralTopic.CenterInView();
        }

        private void listMainTopics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull(false)) return;

            if (listMainTopics.SelectedItem == null) return;

            BookmarkItem item = (BookmarkItem)listMainTopics.SelectedItem;
            if (item == null) return;

            Topic t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
            if (t == null) // topic was deleted
            {
                listBookmarks.Items.Remove(listBookmarks.SelectedItem);
            }
            else // select topic
            {
                if (!t.IsSelected)
                    t.SelectOnly();
                t.SnapIntoView();
                StixUtils.ActivateMindManager();
            }
        }

        private void listPositions_MouseUp(object sender, MouseEventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull(false)) return;

            int index = listPositions.IndexFromPoint(e.Location); // index of list item
            if (index < 0) return; // empty place clicked

            // Add position or go to topic with position
            if (e.Button == MouseButtons.Left)
            {
                PositionItem item = (PositionItem)listPositions.SelectedItem;
                if (item == null) return;

                if (item.TopicGuid == "") // add position
                {
                    // add to list
                    Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
                    if (t == null) return;

                    var _item = StixMapNavigator.DocumentPositions[MMUtils.ActiveDocument.Guid].Find(y => y.TopicGuid == t.Guid);
                    if (_item != null && _item.Number != 0 && _item.Number != item.Number)
                    {
                        // Topic has position and position number is different. Change position number?

                        if (MessageBox.Show(String.Format(Utils.getString("bookmarks.addposition.moveposition"), _item.Number), "",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return; // user wants to not change

                        // Clear old position
                        listPositions.Items[_item.Number - 1] = 
                            new PositionItem(Utils.getString("MapNavigatorDlg.addposition.tooltip"), "", _item.Number);

                        StixMapNavigator.DocumentPositions[MMUtils.ActiveDocument.Guid].Remove(_item);
                    }

                    // add position to list
                    item.TopicName = t.Text;
                    item.TopicGuid = t.Guid;
                    listPositions.SelectedItem = item;

                    // add to dictionary
                    StixMapNavigator.DocumentPositions[MMUtils.ActiveDocument.Guid].Add(item);

                    // update in the stick
                    if (StixMain.m_MapNavigator != null && !StixMain.m_MapNavigator.IsDisposed)
                        StixMain.m_MapNavigator.InitPositions(true);
                }
                else // Go to the topic with position
                {
                    Topic t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
                    if (t == null) // Topic was deleted. Remove position
                    {
                        MessageBox.Show(Utils.getString("bookmarks.position.notexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        DeletePosition_Click(null, null);
                    }
                    else
                    {
                        t.SelectOnly();
                        t.SnapIntoView();
                        StixUtils.ActivateMindManager();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right) // show "Delete Position" context menu
            {
                listPositions.SelectedIndex = index;
                PositionItem item = (PositionItem)listPositions.SelectedItem;
                if (item.TopicGuid != "")
                {
                    contextMenuStrip.Items["DeletePosition"].Visible = true;
                    contextMenuStrip.Items["DeleteBookmark"].Visible = false;
                    contextMenuStrip.Show(MousePosition);
                }
            }
        }

        private void DeletePosition_Click(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull(false)) return;

            PositionItem item = (PositionItem)listPositions.SelectedItem;
            StixMapNavigator.DocumentPositions[MMUtils.ActiveDocument.Guid].Remove(item);
            InitPositions();
        }

        private void linkDeleteAllPositions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InitPositions(false, true);
        }

        /// <summary>
        /// Adds bookmark to selected topic
        /// </summary>
        private void linkAddBookmark_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t == null) return;

            if (t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                MessageBox.Show(Utils.getString("bookmarks.addbookmark.exists"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");
            StixMapNavigator.DocumentBookmarks.Remove(MMUtils.ActiveDocument.Guid);
            InitBookmarks();
        }

        private void linkDeleteAllBookmarks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull(false)) return;

            if (MessageBox.Show(Utils.getString("bookmarks.confirm.deletebookmarks"), "",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
                {
                    if (t.ContainsAttributesNamespace(ATTR_NAMESPACE))
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAttribute(ATTR_BOOKMARKED);
                }
                InitBookmarks(false, true);
            }
        }

        // Delete bookmark
        private void DeleteBookmark_Click(object sender, EventArgs e)
        {
            if (listBookmarks.SelectedItem == null) return;
            if (Utils.ActiveDocumentOrSelectionNull(false)) return;

            BookmarkItem _item = listBookmarks.SelectedItem as BookmarkItem;
            if (_item != null)
            {
                Topic t = MMUtils.ActiveDocument.FindByGuid(_item.TopicGuid) as Topic;
                if (t != null)
                    t.GetAttributes(ATTR_NAMESPACE).DeleteAttribute(ATTR_BOOKMARKED);

                var item = StixMapNavigator.DocumentBookmarks[MMUtils.ActiveDocument.Guid].Find(x => x.TopicGuid == _item.TopicGuid);
                if (item != null)
                {
                    StixMapNavigator.DocumentBookmarks[MMUtils.ActiveDocument.Guid].Remove(item);
                    Init();
                }
            }
        }

        // Right click on bookmark's list (item context menu)
        private void listBookmarks_MouseUp(object sender, MouseEventArgs e)
        {
            int index = listBookmarks.IndexFromPoint(e.Location); // index of list item
            if (index < 0) return;

            if (e.Button == MouseButtons.Left)
            {
                BookmarkItem item = (BookmarkItem)listBookmarks.SelectedItem;
                if (item == null) return;

                if (MMUtils.ActiveDocument == null) return;
                
                Topic t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
                if (t == null)
                {
                    MessageBox.Show(Utils.getString("bookmarks.topic.noexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    StixMapNavigator.DocumentBookmarks[MMUtils.ActiveDocument.Guid].Remove(item);
                    Init();
                }
                else
                {
                    if (!t.IsSelected)
                        t.SelectOnly();
                    t.SnapIntoView();
                    StixUtils.ActivateMindManager();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                listBookmarks.SelectedItem = listBookmarks.Items[index];

                foreach (ToolStripItem item in contextMenuStrip.Items)
                    item.Visible = true;

                contextMenuStrip.Items["DeletePosition"].Visible = false;
                contextMenuStrip.Items["DeleteBookmark"].Visible = true;
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void pRefresh_Click(object sender, EventArgs e)
        {
            InitMainTopics();
            InitBookmarks();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            StixMain.m_MapNavigatorDlg = null;
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "NavigatorWindow.htm");
        }

        private void listBookmarks_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = sender as ListBox;

            if (lb.Items.Count < 1)
                return;
             
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index,
                                e.State ^ DrawItemState.Selected, e.ForeColor,
                                Color.WhiteSmoke); // selected item color

            e.DrawBackground();
            lb.ItemHeight = lb.Font.Height;            

            if (e.Index < 0) return;

            if (lb == listPositions) // Navigation listbox
            {
                PositionItem p_data = lb.Items[e.Index] as PositionItem;
                Font addposition = new Font(lb.Font, FontStyle.Underline);

                if (p_data.TopicGuid == "")
                    e.Graphics.DrawString(p_data.TopicName, addposition, Brushes.Gray, e.Bounds);
                else
                    e.Graphics.DrawString(p_data.TopicName, lb.Font, Brushes.Black, e.Bounds);
            }
            else // Bookmarks listbox
            {
                Font central = new Font(lb.Font, FontStyle.Bold);
                BookmarkItem b_data = lb.Items[e.Index] as BookmarkItem;

                //if (b_data.TopicType == Central)
                //    e.Graphics.DrawString(b_data.TopicName, central, Brushes.Black, e.Bounds);
                //if (b_data.TopicType == Main)
                //    e.Graphics.DrawString(b_data.TopicName, lb.Font, Brushes.Blue, e.Bounds);
                //else if (b_data.TopicType == Floating)
                //    e.Graphics.DrawString(b_data.TopicName, lb.Font, Brushes.Magenta, e.Bounds);
                //else if (b_data.TopicType == Normal)
                    e.Graphics.DrawString(b_data.TopicName, lb.Font, Brushes.Black, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            int w = pDraw.Width, h = pDraw.Height;
            try
            {
                // Active tab
                if (e.Index == this.tabControl1.SelectedIndex) // Active tab.
                {
                    Brush _BackBrush = new SolidBrush(Color.White);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, (rect.X) + w, rect.Y, (rect.Width) - w, rect.Height);

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                               e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                               e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + h);
                }
                else // All the rest tabs
                {
                    Brush _BackBrush = new SolidBrush(SystemColors.ButtonFace);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, rect.X, (rect.Y) - 0, rect.Width, (rect.Height) + (int)(w * 1.5));

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                    e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                              e.Bounds.Top + w);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        const string Central = "central", Floating = "floating", Main = "main", Normal = "normal", AddPosition = "addposition";
    }
}
