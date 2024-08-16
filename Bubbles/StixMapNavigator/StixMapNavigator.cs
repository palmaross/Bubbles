using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace Bubbles
{
    internal partial class StixMapNavigator : Form
    {
        public StixMapNavigator(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            StixMain.m_MapNavigator = this;

            this.Tag = ID;
            orientation = _orientation; // "H" or "V"

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "NavigationStix.htm");

            myToolTip1.SetToolTip(pictureHandle, stickname +
                Utils.getString("StixMapNavigator.description") +
                Utils.getString("HeadIcon.tooltip.tips"));
            toolTip1.SetToolTip(pCentral, Utils.getString("bookmarks.centraltopic"));
            toolTip1.SetToolTip(Manage, Utils.getString("ManageIcon.tooltip"));
            toolTip1.SetToolTip(pSearch, Utils.getString("bookmarks.SearchTopics"));

            foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
                if (pb.Name.StartsWith("B"))
                    toolTip1.SetToolTip(pb, Utils.getString("bookmarks.addbookmark.tooltip"));

            b_DeletePosition.Text = Utils.getString("bookmarks.contextmenu.delete.position");
            b_DeleteAllPositions.Text = Utils.getString("bookmarks.contextmenu.delete.allpositions");

            MinLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            ToolStripItem tsi = cmsCommon.Items.Add(Utils.getString("bookmarks.contextmenu.navwindow"));
            tsi.Name = "MNWindow";
            StixUtils.SetContextMenuImage(tsi, "position.png");

            cmsCommon.ItemClicked += ContextMenuStrip1_ItemClicked;
            cmsPositions.ItemClicked += ContextMenuStrip1_ItemClicked;

            StixUtils.SetCommonContextMenu(cmsCommon, StixUtils.typemapnavigator);

            this.MouseDown += Move_Stick;
            pictureHandle.MouseDown += Move_Stick;
            Manage.Click += Manage_Click;
            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();

            // Apply scale factor
            this.Paint += this_Paint; // paint the border depending on scale factor
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);

            pPosition = Image.FromFile(b_path + "position.png");
            pPosition_empty = Image.FromFile(b_path + "position_empty.png");
            pBookmarks = Image.FromFile(b_path + "bookmarks.png");
            pBookmarksActive = Image.FromFile(b_path + "bookmarks_active.png");

            Init();
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            scaleFactor = toScale;

            if (scale != 1)
            {
                this.Scale(new SizeF(scale, scale)); // reset to 100%
                StixUtils.icondist = (int)(StixUtils.icondist * scale);
                MinLength = (int)(MinLength * scale);
            }

            if (toScale != 100)
            {
                this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale
                StixUtils.icondist = (int)(StixUtils.icondist * (toScale / 100));
                MinLength = (int)(MinLength * (toScale / 100));
            }
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            if (scaleFactor < 125) return;
            int width = 1;
            //if (scaleFactor > 200) width = 2;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Initialize bookmark stix.
        /// </summary>
        /// <param name="fromList">True - called from bookmark list dlg</param>
        /// <param name="deleteall">True - delete all bookmarks in the map and stix</param>
        public void Init()
        {
            pBookmarkList.Image = pBookmarks;
            cmsMainTopics.Items.Clear();
            cmsBookmarks.Items.Clear();

            if (MMUtils.ActiveDocument == null)
            {
                pCentral.Tag = "";
                //toolTip1.SetToolTip(pCentral, "");
            }
            else
            {
                pCentral.Tag = MMUtils.ActiveDocument.CentralTopic.Guid;
                //toolTip1.SetToolTip(pCentral, MMUtils.ActiveDocument.CentralTopic.Text);

                InitMainTopicsContextMenu();
                InitBookmarksContextMenu();
                InitPositions();
            }
        }

        public void InitMainTopicsContextMenu(bool fromList = false)
        {
            cmsMainTopics.Items.Clear();
            if (MMUtils.ActiveDocument == null) return;

            ToolStripItem tsi = new ToolStripLabel(Utils.getString("bookmarks.maintopics"));
            tsi.Font = new Font(cmsMainTopics.Font, FontStyle.Bold);
            cmsMainTopics.Items.Add(tsi);

            foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.AllSubTopics)
            {
                string topicText = t.Text;
                if (topicText.Length > 50) topicText = topicText.Substring(0, 50);

                tsi = cmsMainTopics.Items.Add(topicText);
                tsi.Tag = t.Guid;
                tsi.Click += MainTopicMenu_Click;
            }

            // Update main topics in the Map Navigator window
            if (!fromList && StixMain.m_MapNavigatorDlg != null && StixMain.m_MapNavigatorDlg.Visible)
                StixMain.m_MapNavigatorDlg.InitMainTopics(true);

            cmsMainTopics.Items.Add(new ToolStripSeparator());

            tsi = cmsMainTopics.Items.Add(Utils.getString("button.refresh"));
            tsi.ToolTipText = Utils.getString("bookmarks.refresh.maintopics");
            StixUtils.SetContextMenuImage(tsi, "refresh.png");
            tsi.Click += RefreshMainTopics;
        }

        private void RefreshMainTopics(object sender, EventArgs e)
        {
            InitMainTopicsContextMenu();
            cmsMainTopics.Show();
        }

        public void InitBookmarksContextMenu(bool fromList = false, bool deleteall = false)
        {
            cmsBookmarks.Items.Clear();

            if (MMUtils.ActiveDocument == null) return;

            ToolStripItem tsi = new ToolStripLabel(Utils.getString("bookmarks.ebookmarks"));
            tsi.Font = new Font(cmsMainTopics.Font, FontStyle.Bold);
            cmsBookmarks.Items.Add(tsi);

            if (deleteall) // delete all bookmarks in the current map and the stix
            {
                if (!fromList)
                    foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAttribute(ATTR_BOOKMARKED);
            }
            else // Fill context menu
            {
                // Fill bookmarks from dictionary
                bool bookmarks = false;
                if (DocumentBookmarks.Keys.Contains(MMUtils.ActiveDocument.Guid))
                {
                    foreach (BookmarkItem item in DocumentBookmarks[MMUtils.ActiveDocument.Guid])
                    {
                        bookmarks = true;
                        AddBookmarkToList(item.TopicName, item.TopicGuid);
                    }
                }
                else // First time seeing the map. Load bookmarks from map.
                {
                    Bookmarks.Clear();

                    // Add bookmarks exсept floating topics
                    LoadFromMapRecursive(MMUtils.ActiveDocument.CentralTopic);

                    // Add bookmarks from the floating topic branches
                    foreach (Topic _t in MMUtils.ActiveDocument.AllFloatingTopics)
                        LoadFromMapRecursive(_t);

                    List<BookmarkItem> list = new List<BookmarkItem>();

                    if (Bookmarks.Count > 0)
                    {
                        list.AddRange(Bookmarks);
                        bookmarks = true;

                        foreach (var bookmark in Bookmarks)
                            AddBookmarkToList(bookmark.TopicName, bookmark.TopicGuid);
                    }
                    DocumentBookmarks.Add(MMUtils.ActiveDocument.Guid, list);
                }

                if (bookmarks) // There are bookmarks in the map.
                    pBookmarkList.Image = pBookmarksActive;

                cmsBookmarks.Items.Add(new ToolStripSeparator());

                tsi = cmsBookmarks.Items.Add(Utils.getString("bookmarks.contextmenu.addbookmark"));
                StixUtils.SetContextMenuImage(tsi, "newsticker.png");
                tsi.Click += AddBookmark_Click;

                tsi = cmsBookmarks.Items.Add(Utils.getString("bookmarks.contextmenu.delete.allbookmarks"));
                StixUtils.SetContextMenuImage(tsi, "deleteall.png");
                tsi.Click += DeleteAllBookmarks_Click;

                tsi = cmsBookmarks.Items.Add(Utils.getString("button.refresh"));
                StixUtils.SetContextMenuImage(tsi, "refresh.png");
                tsi.Click += RefreshBookmarks;
            }

            // Update bookmarks in the Map Navigator window
            if (StixMain.m_MapNavigatorDlg != null && StixMain.m_MapNavigatorDlg.Visible && !fromList)
                StixMain.m_MapNavigatorDlg.InitBookmarks(true, deleteall);
        }

        private void RefreshBookmarks(object sender, EventArgs e)
        {
            InitBookmarksContextMenu();
            cmsBookmarks.Show();
        }

        public void InitPositions(bool fromList = false, bool deleteall = false)
        {
            // Clear positions
            foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
            {
                if (pb.Name.StartsWith("B"))
                {
                    pb.Image = pPosition_empty;
                    pb.Tag = "";
                    toolTip1.SetToolTip(pb, Utils.getString("bookmarks.addposition.tooltip"));
                }
            }

            if (deleteall) // delete all positions clicked
            {
                if (MMUtils.ActiveDocument == null) return;

                if (!fromList && DocumentPositions.ContainsKey(MMUtils.ActiveDocument.Guid))
                    DocumentPositions[MMUtils.ActiveDocument.Guid].Clear();
            }
            else
            {
                if (MMUtils.ActiveDocument == null) return;

                // Fill positions
                if (DocumentPositions.ContainsKey(MMUtils.ActiveDocument.Guid))
                {
                    foreach (PositionItem item in DocumentPositions[MMUtils.ActiveDocument.Guid])
                    {
                        if (item.Number > 5) continue; // no valid for five stix's positions
                        PictureBox pb = SetPosition(item.Number);
                        pb.Tag = item;
                        string text = item.TopicName;
                        if (text.Length > 80) text = text.Substring(0, 80);
                        toolTip1.SetToolTip(pb, text);
                    }
                }
                else
                    DocumentPositions.Add(MMUtils.ActiveDocument.Guid, new List<PositionItem>());

            }
            if (StixMain.m_MapNavigatorDlg != null && !fromList)
                StixMain.m_MapNavigatorDlg.Init(true, deleteall);
        }

        /// <summary>
        /// Fill Bookmarks icon context menu
        /// </summary>
        void AddBookmarkToList(string topicText, string topicGuid)
        {
            string text = topicText;
            if (text.Length > 50) text = text.Substring(0, 50);
            ToolStripItem tsi = cmsBookmarks.Items.Add(text);
            tsi.Name = "aBookmark"; tsi.Tag = topicGuid;
            tsi.MouseDown += Bookmark_MouseDown;
        }

        private void Bookmark_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null) return;

                string topicGuid = (sender as ToolStripMenuItem).Tag.ToString();

                Topic t = MMUtils.ActiveDocument.FindByGuid(topicGuid) as Topic;
                if (t != null)
                {
                    t.SelectOnly();
                    t.SnapIntoView();
                    StixUtils.ActivateMindManager();
                }

                cmsBookmarks.Close();
            }
            if (e.Button == MouseButtons.Right)
            {
                if (menuItem != null && menuItem.DropDown.Items.Count > 0)
                {
                    menuItem.DropDown.Items.Clear(); cmsBookmarks.Refresh();
                }
                menuItem = sender as ToolStripMenuItem;
                var menuDelete = menuItem.DropDown.Items.Add(Utils.getString("bookmarks.contextmenu.delete.bookmark"));
                menuDelete.Tag = menuItem.Tag;
                menuDelete.Click += DeleteBookmark_Click;
                (menuItem.DropDown as ToolStripDropDownMenu).ShowImageMargin = false;
                menuItem.DropDown.Show();
            }
        }
        ToolStripMenuItem menuItem;

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBookmark_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t != null)
            {
                if (t.GetAttributes(ATTR_NAMESPACE).GetAttributeValue(ATTR_BOOKMARKED) != "")
                {
                    MessageBox.Show(Utils.getString("bookmarks.addbookmark.exists"));
                    return;
                }
                // Add bookmark bookmark to topic
                t.GetAttributes(ATTR_NAMESPACE).SetAttributeValue(ATTR_BOOKMARKED, "1");

                // Reinit bookmark list (menu)
                if (DocumentBookmarks.ContainsKey(MMUtils.ActiveDocument.Guid))
                    DocumentBookmarks.Remove(MMUtils.ActiveDocument.Guid);
                InitBookmarksContextMenu();

                // Show menu
                cmsBookmarks.Show(this.Left + pBookmarkList.Location.X, this.Bottom);

                // Select added bookmark
                int i = 0; bool found = false;
                foreach (ToolStripItem item in cmsBookmarks.Items)
                {
                    if (item.Tag != null && item.Tag.ToString() == t.Guid)
                    {
                        found = true;
                        break;
                    }
                    i++;
                }
                if (found)
                    cmsBookmarks.Items[i].Select();
            }
        }

        private void DeleteBookmark_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            string topicGuid = (sender as ToolStripItem).Tag.ToString();
            if (!String.IsNullOrEmpty(topicGuid))
            {
                Topic t = MMUtils.ActiveDocument.FindByGuid(topicGuid) as Topic;
                if (t != null)
                {
                    t.GetAttributes(ATTR_NAMESPACE).DeleteAttribute(ATTR_BOOKMARKED);

                    if (!DocumentBookmarks.ContainsKey(MMUtils.ActiveDocument.Guid)) return;

                    var item = DocumentBookmarks[MMUtils.ActiveDocument.Guid].Find(x => x.TopicGuid == topicGuid);
                    if (item != null)
                    {
                        DocumentBookmarks[MMUtils.ActiveDocument.Guid].Remove(item);
                        InitBookmarksContextMenu();
                        cmsBookmarks.Show(this.Left + pBookmarkList.Location.X, this.Bottom);
                    }
                }
            }
        }

        private void DeleteAllBookmarks_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;

            if (MessageBox.Show(Utils.getString("bookmarks.confirm.deletebookmarks"), "",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (DocumentBookmarks.ContainsKey(MMUtils.ActiveDocument.Guid))
                    DocumentBookmarks.Remove(MMUtils.ActiveDocument.Guid);

                foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
                {
                    if (t.ContainsAttributesNamespace(ATTR_NAMESPACE))
                        t.GetAttributes(ATTR_NAMESPACE).DeleteAttribute(ATTR_BOOKMARKED);
                }
                // Clear stix bookmarks.
                InitBookmarksContextMenu(false, true);
            }
        }

        void LoadFromMapRecursive(Topic _t)
        {
            if (_t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED))
            {
                string ttext = _t.Text;
                if (ttext.Length > 100) ttext = _t.Text.Substring(0, 100);

                string topictype =
                    _t.IsCentralTopic ? Central :
                    _t.IsMainTopic ? Main :
                    _t.IsFloatingTopic ? Floating : Normal;

                Bookmarks.Add(new BookmarkItem(ttext, _t.Guid, topictype));
            }

            foreach (Topic t in _t.AllSubTopics)
                LoadFromMapRecursive(t);
        }

        PictureBox SetPosition(int number)
        {
            switch (number)
            {
                case 1: B1.Image = pPosition; return B1;
                case 2: B2.Image = pPosition; return B2;
                case 3: B3.Image = pPosition; return B3;
                case 4: B4.Image = pPosition; return B4;
                case 5: B5.Image = pPosition; return B5;
            }
            return null;
        }

        public void TopicAffected(string docGuid, Topic t, string reason)
        {
            bool isBookmark = t.GetAttributes(ATTR_NAMESPACE).HasAttribute(ATTR_BOOKMARKED);
            string topicGuid = t.Guid;

            string topicText = t.Text;
            string text = topicText;
            if (text.Length > 80) text = text.Substring(0, 80);

            var position = DocumentPositions[docGuid].Find(x => x.TopicGuid == topicGuid);
            var bookmark = DocumentBookmarks[docGuid].Find(x => x.TopicGuid == topicGuid);

            string topicType = t.IsCentralTopic ? Central : t.IsMainTopic ? Main : t.IsFloatingTopic ? Floating : Normal;

            if (topicType == Central)
            {
                toolTip1.SetToolTip(pCentral, text);

                if (StixMain.m_MapNavigatorDlg != null && StixMain.m_MapNavigatorDlg.Visible)
                    StixMain.m_MapNavigatorDlg.lblCentralTopic.Text = topicText;
            }
            else if (topicType == Main)
                InitMainTopicsContextMenu();
            else if (topicType == Floating)
                topicType = Floating;

            if (position != null) // Position affected
            {
                if (reason == "text")
                    position.TopicName = topicText;
                else if (reason == "deleted")
                    DocumentPositions[docGuid].Remove(position);

                InitPositions();
            }

            if (isBookmark)
            {
                if (bookmark != null) // Bookmark affected
                {
                    if (reason == "text")
                        bookmark.TopicName = topicText;
                    else if (reason == "deleted")
                        DocumentBookmarks[docGuid].Remove(bookmark);

                    InitBookmarksContextMenu();
                }

                if (reason == "added")
                {
                    BookmarkItem item = new BookmarkItem(topicText, topicGuid, topicType);
                    DocumentBookmarks[docGuid].Add(item);
                    InitBookmarksContextMenu();
                }
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsCommon.Items)
                item.Visible = true;
            cmsCommon.Show(Cursor.Position);
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
            // Delete position.
            if (e.ClickedItem.Name == "b_DeletePosition")
            {
                DeletePosition(selectedIcon);

                // Update positions in the Map Navigator window
                if (StixMain.m_MapNavigatorDlg != null && StixMain.m_MapNavigatorDlg.Visible)
                    StixMain.m_MapNavigatorDlg.InitPositions(true);
            }
            // Delete all positions.
            else if (e.ClickedItem.Name == "b_DeleteAllPositions")
            {
                if (MMUtils.ActiveDocument != null)
                {
                    if (DocumentPositions.Keys.Contains(MMUtils.ActiveDocument.Guid))
                        DocumentPositions[MMUtils.ActiveDocument.Guid].Clear();
                    InitPositions(false, true);
                }
            }
            // Open MapNavigator Window.
            else if (e.ClickedItem.Name == "MNWindow")
            {
                if (Utils.FreeVersionLimitExceeded("mapnavigator"))
                    return;

                if (StixMain.m_MapNavigatorDlg == null)
                {
                    StixMain.m_MapNavigatorDlg = new MapNavigatorDlg();

                    // Get navigation window location
                    if (StixMain.m_MapNavigatorDlg.Location.IsEmpty)
                    {
                        StixMain.m_MapNavigatorDlg.Location = 
                            StixUtils.GetChildLocation(this, StixMain.m_MapNavigatorDlg.Bounds, orientation, "bookmarks");
                    }
                    StixMain.m_MapNavigatorDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
                StixMain.m_MapNavigatorDlg.Init();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixMain.STICKS.Remove((int)this.Tag);
                StixMain.m_MapNavigator = null;
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "NavigationStix.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typemapnavigator, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        void DeletePosition(PictureBox pb)
        {
            int number = 0;
            int.TryParse(pb.Name.Substring(1, 1), out number);

            if (number != 0)
            {
                pb.Image = pPosition_empty; pb.Tag = "";
                toolTip1.SetToolTip(pb, Utils.getString("bookmarks.addposition.tooltip"));

                if (MMUtils.ActiveDocument == null) return;
                var item = DocumentPositions[MMUtils.ActiveDocument.Guid].Find(x => x.Number == number);
                if (item != null)
                    DocumentPositions[MMUtils.ActiveDocument.Guid].Remove(item);
            }
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation);
        }

        private void pCentral_MouseClick(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;
            if (!MMUtils.ActiveDocument.CentralTopic.IsVisible) return;

            MMUtils.ActiveDocument.CentralTopic.SelectOnly();
            MMUtils.ActiveDocument.CentralTopic.SnapIntoView();
        }

        private void pMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;
            if (MMUtils.ActiveDocument.CentralTopic.AllSubTopics.Count == 0) return;
            if (cmsMainTopics.Items.Count == 0) return;

            foreach (ToolStripItem item in cmsMainTopics.Items)
                item.Visible = true;

            cmsMainTopics.Show(Cursor.Position);
        }

        private void pMain_MouseHover(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;
            if (MMUtils.ActiveDocument.CentralTopic.AllSubTopics.Count == 0) return;
            if (cmsMainTopics.Items.Count == 0) return;

            foreach (ToolStripItem item in cmsMainTopics.Items)
                item.Visible = true;

            cmsMainTopics.Show(Cursor.Position);
        }

        private void MainTopicMenu_Click(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull(false)) return;

            string guid = (sender as ToolStripItem).Tag.ToString();

            if (!String.IsNullOrEmpty(guid))
            {
                Topic t = MMUtils.ActiveDocument.FindByGuid(guid) as Topic;
                if (t != null)
                {
                    t.SelectOnly(); t.SnapIntoView(); StixUtils.ActivateMindManager();
                }
            }
        }

        /// <summary>
        /// Position clicked. Add position or select positionbed topic. Or show the context menu.
        /// </summary>
        private void Position_MouseClick(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null) return;
            PictureBox pb = sender as PictureBox;
            selectedIcon = pb;

            if (e.Button == MouseButtons.Left)
            {
                PositionItem item = pb.Tag as PositionItem;
                if (item == null) // Empty position clicked. Add position
                {
                    if (Convert.ToInt32(pb.Name.Substring(1, 1)) > 1 &&
                        Utils.FreeVersionLimitExceeded(StixUtils.typemapnavigator))
                        return;

                    AddPosition(Convert.ToInt32(pb.Name.Substring(1,1))); // position number
                }
                else // Select positionmarked topic
                {
                    Topic t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
                    if (t == null) // Positionmarked topic not found. Was deleted?
                    {
                        MessageBox.Show(Utils.getString("bookmarks.position.notexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        var _item = DocumentPositions[MMUtils.ActiveDocument.Guid].Find(x => x.Number == item.Number);
                        if (_item != null)
                        {
                            DocumentPositions[MMUtils.ActiveDocument.Guid].Remove(_item);
                            pb.Image = pPosition_empty;
                        }
                    }
                    else
                    {
                        t.SelectOnly();
                        t.SnapIntoView();
                        StixUtils.ActivateMindManager();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (pb.Tag == null || pb.Tag.ToString() == "")
                    return;

                foreach (ToolStripItem item in cmsPositions.Items)
                    item.Visible = true;

                cmsPositions.Show(Cursor.Position);
            }
        }

        public void AddPosition(int number)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;

            string topictext = t.Text;
            if (topictext.Length > 80) topictext = topictext.Substring(0, 80);

            var item = DocumentPositions[MMUtils.ActiveDocument.Guid].Find(y => y.TopicGuid == t.Guid);

            if (item != null && item.Number != 0 && number != item.Number)
            {
                // Topic has position and it is different. Change position number?

                if (MessageBox.Show(String.Format(Utils.getString("bookmarks.addposition.moveposition"), item.Number), "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Clear previous position
                    PictureBox pb;
                    if (number < 6)
                    {
                        pb = SetPosition(item.Number); // find needed picture box 
                        pb.Image = pPosition_empty; pb.Tag = ""; // and clear its position
                    }

                    item.Number = number;

                    pb = SetPosition(number);
                    pb.Tag = item;
                    toolTip1.SetToolTip(pb, topictext);
                }
                else
                    return;
            }
            else // Set position
            {
                PictureBox pb = SetPosition(number);
                if (pb != null)
                {
                    item = new PositionItem(t.Text, t.Guid, number);
                    pb.Tag = item;
                    toolTip1.SetToolTip(pb, topictext);

                    DocumentPositions[MMUtils.ActiveDocument.Guid].Add(item);
                }
            }
            // Update positions in the Map Navigator window
            if (StixMain.m_MapNavigatorDlg != null && StixMain.m_MapNavigatorDlg.Visible)
                StixMain.m_MapNavigatorDlg.InitPositions(true);
        }

        private void pBookmarkList_MouseHover(object sender, EventArgs e)
        {
            if (Utils.FreeVersionLimitExceeded(StixUtils.typemapnavigator))
                return;

            foreach (ToolStripItem item in cmsBookmarks.Items)
                item.Visible = true;

            cmsBookmarks.Show(Cursor.Position);
        }

        public void BookmarkList_MouseClick(object sender, MouseEventArgs e)
        {
            if (Utils.FreeVersionLimitExceeded(StixUtils.typemapnavigator))
                return;

            foreach (ToolStripItem item in cmsBookmarks.Items)
                item.Visible = true;

            cmsBookmarks.Show(Cursor.Position);
        }

        private void pSearch_Click(object sender, EventArgs e)
        {
            if (StixMain.m_SearchText == null || StixMain.m_SearchText.IsDisposed)
            {
                StixMain.m_SearchText = new SearchTextDlg();

                // Get window location
                Rectangle child = StixMain.m_SearchText.RectangleToScreen(StixMain.m_SearchText.ClientRectangle);
                StixMain.m_SearchText.Location = StixUtils.GetChildLocation(this, child, orientation, "bookmarks");

                StixMain.m_SearchText.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        /// <summary>
        /// Key - Document, Value - Bookmarks
        /// </summary>
        public static Dictionary<string, List<BookmarkItem>> DocumentBookmarks = new Dictionary<string, List<BookmarkItem>>();
        public static Dictionary<string, List<PositionItem>> DocumentPositions = new Dictionary<string, List<PositionItem>>();

        public static List<BookmarkItem> Bookmarks = new List<BookmarkItem>();
        
        PictureBox selectedIcon = null;
        string orientation = "H";

        public float scaleFactor = 100;

        int MinLength;

        string b_path = Utils.m_imagesPath;

        const string Central = "central", Floating = "floating", Main = "main", Normal = "normal";
        Image pPosition, pPosition_empty, pBookmarks, pBookmarksActive;

        // For MouseDown event
        public const int WM_NCLBUTTONDOWN = 0xA1;

        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static string ATTR_NAMESPACE = "PALMAROSS_EXPRESSBOOKMARKS";
        public static string ATTR_BOOKMARKED = "BOOKMARKED";
        public static string ATTR_POSITION = "POSITION";
    }

    internal class BookmarkItem
    {
        public BookmarkItem(string topicName, string topicGuid, string topicType = "")
        {
            TopicName = topicName;
            TopicGuid = topicGuid;
            TopicType = topicType;
        }
        public string TopicName = "";
        public string TopicGuid = "";
        public string TopicType;

        public override string ToString()
        {
            return TopicName;
        }
    }

    internal class PositionItem
    {
        public PositionItem(string topicName, string topicGuid, int number)
        {
            TopicName = topicName;
            TopicGuid = topicGuid;
            Number = number;
        }
        public string TopicName = "";
        public string TopicGuid = "";
        public int Number;

        public override string ToString()
        {
            return TopicName;
        }
    }
}
