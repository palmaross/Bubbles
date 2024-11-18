using Bubbles.AppManager;
using Microsoft.Win32;
using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Bubbles
{
    public partial class TopicNotesDlg : Form
    {
        private DOMListener Listener;
        public HtmlEditor editor;

        public TopicNotesDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "OmniNotes.htm");

            Text = Utils.getString("TopicNotesDlg.title");
            lblLookIn.Text = Utils.getString("TopicNotesDlg.lblLookIn");
            toolTip1.SetToolTip(pBrowse, Utils.getString("TopicNotesDlg.Browse"));
            linkSearchOptions.Text = Utils.getString("TopicNotesDlg.linkSearchOptions");
            btnSearch.Text = Utils.getString("button.search");
            cbSearchedText.Text = Utils.getString("TopicNotesDlg.cbSearchedText");
            cbSearchedText.ForeColor = SystemColors.ControlDark;
            toolTip1.SetToolTip(cbSearchedText, Utils.getString("TopicNotesDlg.cbSearchedText.tooltip"));

            btnNewTab.Text = Utils.getString("TopicNotesDlg.btnNewTab");
            btnGetTopicNotes.Text = Utils.getString("topiccontextmenu.notes.detach");
            toolTip1.SetToolTip(btnSaveOne, Utils.getString("TopicNotesDlg.btnSaveOne"));
            toolTip1.SetToolTip(btnSaveAll, Utils.getString("TopicNotesDlg.btnSaveAll"));
            btnClose.Text = Utils.getString("button.close");

            toolTip1.SetToolTip(fontUp, Utils.getString("stickers.pIncreaseFont.tooltip"));
            toolTip1.SetToolTip(fontDown, Utils.getString("stickers.pDecreaseFont.tooltip"));

            tabControl1.TabPages.Remove(tabPage2);
           
            PreviewPage.AccessibleName = "";
            PreviewPage.Text = Utils.getString("TopicNotesDlg.PreviewPage");

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            cmsTopics.ItemClicked += CmsTopics_ItemClicked;
            cmsSearchOptions.ItemClicked += CmsSearchOptions_ItemClicked;
            cmsWebBrowser.ItemClicked += CmsWebBrowser_ItemClicked;
            cmsSearchOptions.Closing += CmsSearchOptions_Closing;

            WB_Copy.Text = Utils.getString("button.copyctrl");
            WB_CopyAll.Text = Utils.getString("TopicNotesDlg.copyall");
            WB_Cut.Text = Utils.getString("button.cutctrl");
            WB_Paste.Text = Utils.getString("button.pastectrl");
            WB_SelectAll.Text = Utils.getString("button.selectallctrl");
            WB_SelectAll.Text = Utils.getString("button.selectall");
            WB_PasteToTopic.Text = Utils.getString("TopicNotesDlg.addtonotes");
            WB_PasteToTopic.ToolTipText = Utils.getString("TopicNotesDlg.addtonotes.tooltip");

            cmsWebBrowser.Opening += (sender, e) =>
            {
                WB_Cut.Enabled = editor.Selection != null && editor.Selection.text != null;
                WB_Copy.Enabled = editor.Selection != null && editor.Selection.text != null;
                WB_Paste.Enabled = System.Windows.Clipboard.ContainsText();
                //WB_SelectAll.Enabled = wb.Document.Body.InnerText.Length > 0;
                WB_PasteToTopic.Enabled = MMUtils.ActiveDocument != null &&
                    MMUtils.ActiveDocument.Selection.PrimaryTopic != null;
            };

            SO_AddToResults.Text = Utils.getString("SO_AddToResults");
            SO_ReplaceResults.Text = Utils.getString("SO_ReplaceResults");
            SO_ReplaceResults.Checked = true;

            MI_gototopic.Text = Utils.getString("TopicNotesDlg.contextmenu.gototopic");
            StixUtils.SetContextMenuImage(MI_gototopic, "expand.png");
            MI_remove.Text = Utils.getString("TopicNotesDlg.contextmenu.remove");
            StixUtils.SetContextMenuImage(MI_remove, "deleteall.png");

            LookInCurrenMap.Text = Utils.getString("LookIn.currentMap");
            LookInAllOpenMaps.Text = Utils.getString("LookIn.openMaps");
            LookInCollections.Text = Utils.getString("LookIn.mapCollections");
            LookInFolders.Text = Utils.getString("LookIn.mapFolders");

            cmsLookIn.ItemClicked += CmsLookIn_ItemClicked;
            Collections = LookInCollections.DropDown;
            Collections.Name = "Collections";
            (Collections as ToolStripDropDownMenu).ShowImageMargin = false;
            Collections.ItemClicked += CmsLookIn_ItemClicked;
            Folders = LookInFolders.DropDown;
            Folders.Name = "Folders";
            (Folders as ToolStripDropDownMenu).ShowImageMargin = false;
            Folders.ItemClicked += CmsLookIn_ItemClicked;
            InitCollectionsAndFolders();
            cbFindIn.Text = Utils.getString("LookIn.currentMap");
            cbFindIn.Tag = "currentMap";

            fBold = Image.FromFile(Utils.ImagesPath + "f_bold.png");
            fItalic = Image.FromFile(Utils.ImagesPath + "f_italic.png");
            fUnderline = Image.FromFile(Utils.ImagesPath + "f_under.png");
            fStrikethrough = Image.FromFile(Utils.ImagesPath + "f_strike.png");
            fBoldActive = Image.FromFile(Utils.ImagesPath + "f_boldActive.png");
            fItalicActive = Image.FromFile(Utils.ImagesPath + "f_italicActive.png");
            fUnderlineActive = Image.FromFile(Utils.ImagesPath + "f_underActive.png");
            fStrikethroughActive = Image.FromFile(Utils.ImagesPath + "f_strikeActive.png");

            this.HelpButtonClicked += This_HelpButtonClicked;
            this.ResizeEnd += This_ResizeEnd;

            btnSaveOne.Location = btnSaveOneNo.Location;
            btnSaveAll.Location = btnSaveAllNo.Location;

            List<string> keywords = Utils.getRegistry("SearchHistory", "").Split(';').ToList();
            if (keywords.Count > 0)
                foreach (string k in keywords)
                    cbSearchedText.Items.Add(k);

            m_progressDlg.Create();
            m_progressDlg.dlgParams.title = Utils.getString("TopicNotesDlg.ProgressDlg.Title");
            m_progressDlg.dlgParams.abortTitle = Utils.getString("TopicNotesDlg.ProgressDlg.Abort");
            m_progressDlg.dlgParams.message = Utils.getString("TopicNotesDlg.ProgressDlg.message");

            this.Activated += This_Activated;
            this.Deactivate += This_Deactivated;

            cbSearchedText.GotFocus += CbSearchedText_GotFocus;
            cbSearchedText.LostFocus += CbSearchedText_LostFocus;

            cbFontFamily.SelectedIndex = 0;

            int fontsize = (int)listTopics.Font.Size;
            if (Utils.WindowFontSize != fontsize)
            {
                listTopics.Font = new Font(listTopics.Font.FontFamily, Utils.WindowFontSize * 0.75F);
            }
        }

        private void CbSearchedText_GotFocus(object sender, EventArgs e)
        {
            if (cbSearchedText.ForeColor == SystemColors.ControlDark)
            {
                cbSearchedText.ForeColor = SystemColors.WindowText;
                cbSearchedText.Text = "";
            }
        }

        private void CbSearchedText_LostFocus(object sender, EventArgs e)
        {
            if (cbSearchedText.Text == "")
            {
                cbSearchedText.ForeColor = SystemColors.ControlDark;
                cbSearchedText.Text = Utils.getString("TopicNotesDlg.cbSearchedText");
            }
        }

        ToolStripDropDown Collections;
        ToolStripDropDown CollectionMaps;
        ToolStripDropDown Folders;
        ToolStripDropDown FolderMaps;

        #region Dialog
        private void This_Activated(object sender, EventArgs e)
        {
            InterceptKeys.SetHook();
        }

        private void This_Deactivated(object sender, EventArgs e)
        {
            InterceptKeys.ReleaseHook();
        }

        /// <summary>Collapse/Expand Dialog</summary>
        private void This_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void This_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OmniNotes.htm");
        }

        private void TopicNotesDlg_Load(object sender, EventArgs e)
        {
            WindowExpanded = this.Bounds;
            WindowCollapsed = new Rectangle(this.Location, panelMinimized.Size);
        }
        public Rectangle WindowExpanded;
        Rectangle WindowCollapsed;

        private void TopicNotesDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MMClose && listTopics.Nodes.Count > 0) // If MM closed we wan't show ask user about closing this window
            {
                if (MessageBox.Show(Utils.getString("TopicNotesDlg.closewindow"), "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            m_progressDlg.Destroy();
            StixMain.OmniTopics.Clear();

            InterceptKeys.ReleaseHook(); // Important! Release keys hook!
        }
        public bool MMClose = false;

        #endregion

        #region TabControl

        /// <summary>Get webbrowser editor for the browser in this page</summary>
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Controls.OfType<WebBrowser>().Count() > 0)
                editor = (e.TabPage.Controls.OfType<WebBrowser>().First().Tag as wbItem).Editor;
        }

        private void tabControl1_MouseLeave(object sender, EventArgs e)
        {
            if (HoverIndex != -1)
            {
                HoverIndex = -1;
                tabControl1.Invalidate();
            }
        }

        /// <summary>
        /// Select the topic node of this tab or Remove tab if red cross clicked.
        /// </summary>
        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedTab == null) return;
            TabPage tp = tabControl1.SelectedTab;

            // Get a delete button bounds
            var rx = (Rectangle)tp.Tag;

            if (rx.Contains(e.Location)) // Сlick on a delete button!
            {
                if (tp.AccessibleName == "edited") // and it is modified!
                {
                    // Save changes? 
                    DialogResult dr = MessageBox.Show(Utils.getString("TopicNotesDlg.notsavedpage"), "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (dr == DialogResult.Cancel)
                        return;
                    else if (dr == DialogResult.Yes) // Save changes and remove
                        SaveNotes(tp);
                }

                bool prevdeleted = true;
                if (preselectedTab != null)
                    prevdeleted = preselectedTab == tp;

                int i = tabControl1.SelectedIndex - 1;
                WebBrowser _wb = tp.Controls.OfType<WebBrowser>().First();
                tp.Controls.Remove(_wb); _wb.Dispose(); _wb = null; editor = null;
                tp.Dispose();

                // Get the previous node
                if (prevdeleted)
                {
                    TreeNode node = ((wbItem)tabControl1.TabPages[i].Controls.OfType<WebBrowser>().First().Tag).Node;
                    listTopics.SelectedNode = node;
                    listTopics.Select();
                }
                else
                {
                    listTopics.SelectedNode = ((wbItem)preselectedTab.Controls.OfType<WebBrowser>().First().Tag).Node;
                    listTopics.Select();
                }
            }
            else // Select appropiate node
            {
                if (tp.Controls.OfType<WebBrowser>().Count() > 0 && 
                    (wbItem)tp.Controls.OfType<WebBrowser>().First().Tag != null)
                {
                    TreeNode node = ((wbItem)tp.Controls.OfType<WebBrowser>().First().Tag).Node;
                    listTopics.SelectedNode = node;
                    listTopics.Select();
                }
            }

            UpdateSaveButtons();
        }

        TabPage preselectedTab = null;
        #endregion

        #region Topics

        /// <summary>TreeView node context menu item clicked.</summary>
        private void CmsTopics_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == MI_gototopic)
            {
                if (listTopics.SelectedNode != null)
                    GetTopic(listTopics.SelectedNode, true);
            }
            else if (e.ClickedItem == MI_remove)
            {
                listTopics_KeyDown(null, null);
            }
        }

        /// <summary>'Open in New Tab' button clicked.</summary>
        private void btnNewTab_Click(object sender, EventArgs e)
        {
            newtab = true;
            listTopics_AfterSelect(null, null);
        }
        bool newtab = false;

        /// <summary>
        /// TreeView node left-clicked. Find tab with topic notes or open notes in the Preview Page.
        /// </summary>
        private void listTopics_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectednode = listTopics.SelectedNode;

            pBold.Image = fBold;
            pItalic.Image = fItalic;
            pUnderline.Image = fUnderline;
            pStrikethrough.Image = fStrikethrough;

            // No topic selected
            if (selectednode == null || selectednode.Parent == null)
                return;

            // Check if notes are opened
            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (tp.Controls.OfType<WebBrowser>().Count() == 0) break; // the very first time
                wbItem wbitem = tp.Controls.OfType<WebBrowser>().First().Tag as wbItem;
                if (!newtab && wbitem.Node == selectednode) // Node is opened already. Show.
                {
                    editor = wbitem.Editor;

                    if (tp.AccessibleName == "edited")
                    {
                        btnSaveOne.Visible = true; btnSaveOneNo.Visible = false;
                    }
                    else
                    {
                        btnSaveOne.Visible = false; btnSaveOneNo.Visible = true;
                    }

                    tabControl1.SelectTab(tp); return;
                }
            }

            TopicNotesItem item = selectednode.Tag as TopicNotesItem;

            var html = item.TopicNotes;
            WebBrowser wb = CreateWB(html, selectednode);

            if (newtab) // Open in a new tab
            {
                newtab = false;
                TabPage tp = new TabPage(selectednode.Text);
                tabControl1.TabPages.Add(tp);
                tp.AccessibleName = "";
                tp.Controls.Add(wb);
                tp.ToolTipText = selectednode.Text;
                tabControl1.SelectTab(tp);
            }
            else // Open in the Preview page
            {
                if (PreviewPage.AccessibleName == "edited") // there are modified notes here
                {
                    // Save changes? 
                    DialogResult dr = MessageBox.Show(Utils.getString("TopicNotesDlg.notsavedpage"), "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    wbItem preview = PreviewPage.Controls.OfType<WebBrowser>().First().Tag as wbItem;

                    if (dr == DialogResult.Cancel)
                    {
                        listTopics.SelectedNode = preview.Node;
                        return;
                    }
                    else if (dr == DialogResult.Yes) // Save changes
                    {
                        SaveNotes(PreviewPage);
                    }

                    PreviewPage.AccessibleName = "";
                    UpdateSaveButtons();
                }

                if (PreviewPage.Controls.OfType<WebBrowser>().Count() > 0)
                {
                    WebBrowser _wb = PreviewPage.Controls.OfType<WebBrowser>().First();
                    PreviewPage.Controls.Remove(_wb); _wb.Dispose(); _wb = null;
                }

                PreviewPage.AccessibleName = "";
                PreviewPage.Controls.Add(wb);
                PreviewPage.Text = item.TopicName;
                PreviewPage.ToolTipText = item.TopicName;
                tabControl1.SelectTab(PreviewPage);
            }

            btnSaveOne.Visible = false; btnSaveOneNo.Visible = true;
            tabControl1.Invalidate();
            timerSearchInNotes.Start();
        }

        private void timerSearchInNotes_Tick(object sender, EventArgs e)
        {
            timerSearchInNotes.Stop();
            falsealarm = true;
            SearchInNotes();
        }

        public WebBrowser CreateWB(string html, TreeNode selectednode)
        {
            WebBrowser wb = new WebBrowser() { Dock = DockStyle.Fill };

            editor = new HtmlEditor(wb, html);
            wbItem wbTag = new wbItem(html, "", selectednode, editor);
            wb.Tag = wbTag;

            wb.IsWebBrowserContextMenuEnabled = false;
            wb.ContextMenuStrip = cmsWebBrowser;

            Listener = new DOMListener(wb);
            Listener.DOMChanged += this.OnDOMChanged;

            return wb;
        }

        /// <summary>Show node context menu.</summary>
        private void listTopics_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                listTopics.SelectedNode = e.Node;

                foreach (ToolStripItem item in cmsTopics.Items)
                    item.Visible = true;

                cmsTopics.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Select the node topic in the map and bring it into view.
        /// </summary>
        private void listTopics_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GetTopic(listTopics.SelectedNode, true);
        }

        /// <summary>Get node topic.</summary>
        /// <param name="node">Given node.</param>
        /// <param name="selecttopic">False: do not select topic, do not activate map.</param>
        private Topic GetTopic(TreeNode node, bool selecttopic)
        {
            string mappath;
            TopicNotesItem item = null;
            bool topic = false; // if map opens but not topic

            if (node.Parent == null) // map clicked
                mappath = node.Name;
            else // topic clicked
            {
                mappath = node.Parent.Name;
                item = node.Tag as TopicNotesItem;
                if (item == null) return null;
                topic = true;
            }

            Document doc = null; Topic t = null;

            foreach (Document _doc in MMUtils.MindManager.AllDocuments)
            {
                if (_doc.FullName == mappath)
                {
                    doc = _doc;
                    if (selecttopic) doc.Activate();
                    break;
                }
            }

            if (doc == null) // map is not opened
            {
                doc = MMUtils.MindManager.AllDocuments.Open(mappath, "", selecttopic);
                if (doc == null) return null; // we can't open the map

                t = doc.FindByGuid(item.TopicGuid) as Topic;
                if (t == null)
                {
                    MessageBox.Show(Utils.getString("TopicNotesDlg.topiclost"));
                    listTopics.SelectedNode.Remove();
                    doc = null; return null;
                }
            }
            else
            {
                t = doc.FindByGuid(item.TopicGuid) as Topic;
                if (t == null)
                {
                    MessageBox.Show(Utils.getString("TopicNotesDlg.topiclost"));
                    listTopics.SelectedNode.Remove();
                    doc = null; return null;
                }
            }

            doc = null; // important!

            if (selecttopic) // User wants to view a topic
            {
                if (topic)
                {
                    t.SelectOnly(); t.SnapIntoView();
                }
                t = null; return t;
            }
            else // Function is called from the SaveNotes button
                return t;
        }

        /// <summary>Add selected topics with notes to this window.</summary>
        private void btnGetTopicNotes_Click(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            if (listTopics.Nodes.Count > 0 && listTopics.Nodes[0].Nodes.Count > 1 && 
                Utils.FreeVersionLimitExceeded("topic2notes"))
                return;

            TreeNode map = null, node = null;
            string mappath = MMUtils.ActiveDocument.FullName.ToLower();
            foreach (TreeNode _node in listTopics.Nodes)
            {
                if (_node.Name == mappath)
                {
                    map = _node; break;
                }
            }
            if (map == null)
            {
                if (listTopics.Nodes.Count > 0 && Utils.FreeVersionLimitExceeded("topicnotes"))
                    return;

                map = listTopics.Nodes.Add(mappath, MMUtils.ActiveDocument.CentralTopic.Text, 0);
                map.NodeFont = new Font(listTopics.Font, FontStyle.Bold);
                map.Text = map.Text;
            }

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (String.IsNullOrEmpty(t.Notes.Text))
                    continue;

                // Check in topic already is 
                bool found = false;
                foreach (TreeNode _node in map.Nodes)
                    if ((_node.Tag as TopicNotesItem).TopicGuid == t.Guid)
                    {
                        listTopics.SelectedNode = _node;
                        listTopics.Select();
                        found = true;
                        break;
                    }
                if (found) continue;

                string topictext = t.Text.Trim();
                if (String.IsNullOrEmpty(topictext)) topictext = Utils.getString("TopicNotesDlg.noname");

                TopicNotesItem item = new TopicNotesItem(topictext, t.Guid, t.Notes.TextXHTML);

                node = map.Nodes.Add(topictext);
                node.Tag = item;

                if (StixMain.OmniTopics.Keys.Contains(mappath) && !StixMain.OmniTopics[mappath].Keys.Contains(t.Guid))
                    StixMain.OmniTopics[mappath].Add(t.Guid, node);
                else
                    StixMain.OmniTopics[mappath] = new Dictionary<string, TreeNode> { { t.Guid, node } };
            }

            // Select appropiate node
            if (node != null) listTopics.SelectedNode = node;
            listTopics.Select();
        }
        
        /// <summary>Find map node in the treeview. If absent, create node with map name.</summary>
        /// <param name="doc">Given document.</param>
        /// <returns>Map Node</returns>
        TreeNode GetMapNode(string mapPath, string mapName)
        {
            TreeNode map = null;
            foreach (TreeNode _node in listTopics.Nodes)
            {
                if (_node.Name == mapPath) {
                    map = _node; break; }
            }
            if (map == null)
                map = listTopics.Nodes.Add(mapPath, mapName, 0);

            map.NodeFont = new Font(listTopics.Font, FontStyle.Bold);
            map.Text = map.Text;
            return map;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">Topic with notes.</param>
        /// <param name="map">Map node in the tree.</param>
        /// <param name="replace"></param>
        /// <returns>Added topic node</returns>
        TreeNode AddNotesNode(string mapPath, string mapName, TopicNotesItem item, bool replace = false)
        {
            TreeNode node = null;

            TreeNode map = GetMapNode(mapPath, mapName);

            string topictext = item.TopicName.Trim();
            if (String.IsNullOrEmpty(topictext)) topictext = Utils.getString("TopicNotesDlg.noname");

            if (replace) node = map;
            else node = map.Nodes.Add(topictext);
            node.Tag = item;

            if (StixMain.OmniTopics.Keys.Contains(mapPath))
            {
                if (!StixMain.OmniTopics[mapPath].Keys.Contains(item.TopicGuid))
                    StixMain.OmniTopics[mapPath].Add(item.TopicGuid, node);
            }
            else
                StixMain.OmniTopics[mapPath] = new Dictionary<string, TreeNode> { { item.TopicGuid, node } };

            return node;
        }

        void UpdateNotes(TreeNode node, Document doc, bool mapcopy)
        {
            // Get topic 
            //TopicNotesItem item = node.Tag as TopicNotesItem;
            //Topic t = item.topic;
            //if (t == null || !t.IsValid)
            //    doc.FindByGuid(item.TopicGuid);
            //if (t == null) return;

            //string notes = t.Notes.Text;
            //string topictext = t.Text.Trim();
            //if (String.IsNullOrEmpty(topictext)) topictext = Utils.getString("TopicNotesDlg.noname");

            //item = new TopicNotesItem(t, topictext, t.Guid, t.Notes);
            //node.Tag = item;

            //if (mapcopy && item.topic != null && item.topic.IsValid)
            //    item.topic.Notes = t.Notes;

            //foreach (TabPage tp in tabControl1.TabPages)
            //{
            //    RichTextBox rtb = tp.Controls.OfType<RichTextBox>().First();
            //    rtbItem rtbitem = rtb.Tag as rtbItem;
            //    if (rtbitem.Node == node) // 
            //    {
            //        if (item.tNotes.IsPlainTextOnly)
            //        {
            //            rtb.Rtf = item.tNotes.TextRTF; rtbitem.RtfText = item.tNotes.TextRTF;
            //        }
            //        else rtb.Text = item.PlainNotes;
            //        rtbitem.PlainText = item.PlainNotes;

            //        tp.AccessibleName = ""; // remove status "modified"
            //    }
            //}
        }

        /// <summary>
        /// Delete selected node
        /// </summary>
        private void listTopics_KeyDown(object sender, KeyEventArgs e)
        {
            if (e == null || e.KeyCode == Keys.Delete)
            {
                if (listTopics.SelectedNode != null)
                {
                    if (listTopics.SelectedNode.Parent == null) // map node
                    {
                        if (listTopics.SelectedNode.Nodes.Count > 0) // has topic nodes
                        {
                            foreach (TreeNode node in listTopics.SelectedNode.Nodes)
                            {
                                // Check if tab with this node is opened
                                foreach (TabPage tp in tabControl1.TabPages)
                                {
                                    if (tp.Controls.OfType<WebBrowser>().Count() == 0) continue;

                                    WebBrowser wb = tp.Controls.OfType<WebBrowser>().First();
                                    wbItem rtbitem = wb.Tag as wbItem;

                                    if (rtbitem.Node == node) // Yes. it is opened
                                    {
                                        if (tp.AccessibleName == "edited") // and it is modified!
                                        {
                                            // Save changes? 
                                            DialogResult dr = MessageBox.Show(String.Format(Utils.getString("TopicNotesDlg.modifiednotes"), tp.Text), "",
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                            if (dr == DialogResult.Cancel)
                                            {
                                                tabControl1.SelectTab(tp);
                                                return;
                                            }
                                            else if (dr == DialogResult.Yes) // Save changes and remove
                                            {
                                                SaveNotes(tp);
                                            }
                                        }

                                        // Remove tab page.
                                        if (tp == PreviewPage)
                                        {
                                            tp.Text = Utils.getString("TopicNotesDlg.PreviewPage");
                                            tp.AccessibleName = "";
                                        }
                                        else
                                            tabControl1.TabPages.Remove(tp);

                                        wb.Dispose(); wb = null;
                                        editor = null;
                                    }
                                }
                            }
                        }
                        // Remove nodes from OmniTopics dict
                        string mappath = listTopics.SelectedNode.Name;
                        StixMain.OmniTopics.Remove(mappath);

                        // What node will be selected after the node removing?
                        TreeNode selectnode = listTopics.SelectedNode.PrevNode;
                        if (selectnode == null) selectnode = listTopics.SelectedNode.NextNode;

                        listTopics.SelectedNode.Remove();

                        if (selectnode != null)
                            listTopics.SelectedNode = selectnode;
                        else if (listTopics.Nodes.Count > 0)
                            listTopics.SelectedNode = listTopics.Nodes[0];
                    }
                    else // topic node
                    {
                        // Check if tab with this node is opened
                        foreach (TabPage tp in tabControl1.TabPages)
                        {
                            WebBrowser wb = tp.Controls.OfType<WebBrowser>().First();
                            wbItem wbitem = wb.Tag as wbItem;
                            if (wbitem.Node == listTopics.SelectedNode) // Yes. it is opened
                            {
                                if (tp.AccessibleName == "edited") // and it is modified!
                                {
                                    // Save changes? 
                                    DialogResult dr = MessageBox.Show(Utils.getString("TopicNotesDlg.notsavedpage"), "",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                    if (dr == DialogResult.Cancel)
                                    {
                                        tabControl1.SelectTab(tp);
                                        return;
                                    }
                                    else if (dr == DialogResult.Yes) // Save changes and remove
                                    {
                                        SaveNotes(tp);
                                    }
                                }

                                // Remove tab page.
                                if (tp == PreviewPage)
                                {
                                    tp.Text = Utils.getString("TopicNotesDlg.PreviewPage");
                                    tp.AccessibleName = "";
                                }
                                else
                                    tabControl1.TabPages.Remove(tp);

                                string mappath = listTopics.SelectedNode.Parent.Name;
                                TopicNotesItem item = listTopics.SelectedNode.Tag as TopicNotesItem;
                                StixMain.OmniTopics[mappath].Remove(item.TopicGuid);

                                wb.Dispose(); wb = null;
                                editor = null;
                            }
                        }

                        // What node will be selected after the node removing?
                        TreeNode selectnode = listTopics.SelectedNode.PrevNode;
                        if (selectnode == null) selectnode = listTopics.SelectedNode.NextNode;
                        if (selectnode == null) // Empty map node. Remove it.
                            listTopics.SelectedNode.Parent.Remove();
                        else
                        {
                            listTopics.SelectedNode.Remove();
                            if (selectnode != null)
                                listTopics.SelectedNode = selectnode;
                        }
                    }
                }

                if (listTopics.Nodes.Count == 0)
                    StixMain.OmniTopics.Clear();
                UpdateSaveButtons();
            }
        }

        #endregion

        #region WebBrowser
        public void SelectonChanged(WebBrowser wb)
        {
            wb.Document.AttachEventHandler("onselectionchange", SelectionChanged);
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            if (editor == null) return;

            if (editor.IsBold()) pBold.Image = fBoldActive; else pBold.Image = fBold;
            if (editor.IsItalic()) pItalic.Image = fItalicActive; else pItalic.Image = fItalic;
            if (editor.IsUnderline()) pUnderline.Image = fUnderlineActive; else pUnderline.Image = fUnderline;
            if (editor.IsStrikeThrough()) pStrikethrough.Image = fStrikethroughActive; else pStrikethrough.Image = fStrikethrough;

            try
            {
                string font = editor.Selection.queryCommandValue("fontname");
                cbFontFamily.Text = font;
            } catch { }
        }

        private void CmsWebBrowser_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == WB_Copy)
            {
                editor.Copy();
            }
            else if (e.ClickedItem == WB_CopyAll)
            {
                editor.SelectAll(); editor.Copy(); editor.UnselectAll();
            }
            else if (e.ClickedItem == WB_Cut)
            {
                editor.Cut();
            }
            else if (e.ClickedItem == WB_Paste)
            {
                editor.Paste();
            }
            else if (e.ClickedItem == WB_SelectAll)
            {
                editor.SelectAll();
            }
            else if (e.ClickedItem == WB_PasteToTopic)
            {
                AddToNotes();
            }
        }

        void OnDOMChanged()
        {
           if (falsealarm)
            {
                falsealarm = false;
                return;
            }
            else
            {
                btnSaveOne.Visible = true; btnSaveOneNo.Visible = false;
                btnSaveAll.Visible = true; btnSaveAllNo.Visible = false;

                // Set the tab text in red
                tabControl1.SelectedTab.AccessibleName = "edited";
                tabControl1.Invalidate();
            }
        }

        private bool SaveNotes(TabPage tp)
        {
            if (tabControl1.SelectedTab.Controls.OfType<WebBrowser>().Count() == 0) return false;
            WebBrowser wb = tabControl1.SelectedTab.Controls.OfType<WebBrowser>().First();

            wbItem _item = tabControl1.SelectedTab.Controls.OfType<WebBrowser>().First().Tag as wbItem;

            string html = wb.Document.Body.InnerHtml;
            html = html.Replace("<div contenteditable=\"true\">", "");
            int index = html.IndexOf("</div><script>");
            if (index != -1) html = html.Remove(index);

            string mm = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""          ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""><html  xmlns=""http://www.w3.org/1999/xhtml"">";

            string result = mm + html + "</html>";
            TreeNode node = ((wbItem)tp.Controls.OfType<WebBrowser>().First().Tag).Node;

            // Find topic with this notes
            TopicNotesItem item = node.Tag as TopicNotesItem;
            Topic t = GetTopic(node, false); // Get the needed topic

            if (t == null)
            {
                // Message to user
                return false;
            }

            // Replace topic notes

            item.TopicNotes = result; node.Tag = item;
            t.Notes.TextXHTML = result;
            t.Notes.Commit();
            t.Document.Save();

            if (!t.Document.Window.IsVisible) t.Document.Close();
            t = null;

            // Turn off the Save button
            btnSaveOne.Visible = false; btnSaveOneNo.Visible = true;
            // Turn off the red tab name
            tp.AccessibleName = "";
            tabControl1.Invalidate();
            return true;
        }
        public bool fromTNDlg = false;

        public void AddToNotes()
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;
            if (tabControl1.SelectedTab.Controls.OfType<WebBrowser>().Count() == 0) return;

            string html = editor.Selection.htmlText;
            string result = MMUtils.ActiveDocument.Selection.PrimaryTopic.Notes.TextXHTML;

            // Add to topic notes
            MMUtils.ActiveDocument.Selection.PrimaryTopic.Notes.TextXHTML = result + html;
            MMUtils.ActiveDocument.Selection.PrimaryTopic.Notes.Commit();
        }

        #endregion

        #region SearchPanel

        void InitCollectionsAndFolders(bool collections = true, bool folders = true)
        {
            ToolStripItem tsi;
            if (collections)
            {
                Collections.Items.Clear();

                foreach (MapShortcutCollection collection in MMUtils.MindManager.MapShortcutCollections)
                {
                    if (collection.Count == 0) continue;

                    tsi = Collections.Items.Add(collection.Name);
                    tsi.Name = "mapcollection";
                    tsi.Tag =
                    CollectionMaps = (tsi as ToolStripMenuItem).DropDown;
                    (CollectionMaps as ToolStripDropDownMenu).ShowImageMargin = false;
                    CollectionMaps.ItemClicked += CmsLookIn_ItemClicked;

                    foreach (MapShortcut item in collection)
                    {
                        tsi = CollectionMaps.Items.Add(item.Name);
                        tsi.Tag = item.Path;
                        tsi.Name = "mappath";
                    }
                }

                Collections.Items.Add(new ToolStripSeparator());
                tsi = Collections.Items.Add(Utils.getString("button.refresh"));
                tsi.Name = "RefreshCollections";
            }

            if (folders)
            {
                Folders.Items.Clear();
                Dictionary<string, string> _Folders = GetMyFolders();

                Folders.Visible = true;
                foreach (var folder in _Folders)
                {
                    tsi = Folders.Items.Add(folder.Key);
                    tsi.Name = "folder";
                    tsi.Tag = folder.Value; // Path to folder
                    FolderMaps = (tsi as ToolStripMenuItem).DropDown;
                    (FolderMaps as ToolStripDropDownMenu).ShowImageMargin = false;
                    FolderMaps.ItemClicked += CmsLookIn_ItemClicked;

                    DirectoryInfo di = new DirectoryInfo(folder.Value);
                    FileInfo[] ffi = di.GetFiles("*.mmap", SearchOption.TopDirectoryOnly);
                    foreach (var fi in ffi)
                    {
                        tsi = FolderMaps.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                        tsi.Tag = fi.FullName;
                        tsi.Name = "mappath";
                    }
                }

                Folders.Items.Add(new ToolStripSeparator());
                tsi = Folders.Items.Add(Utils.getString("button.refresh"));
                tsi.Name = "RefreshFolders";
            }
        }

        /// <summary>Open Folder/File dialog</summary>
        private void pBrowse_MouseClick(object sender, MouseEventArgs e)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.InitialDirectory = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryMyMaps);

            if (e.Button == MouseButtons.Left)
            {
                dlg.IsFolderPicker = true;
                dlg.Title = Utils.getString("TopicNotesDlg.BrowseDlgTitle.Folder");
            }
            else
            {
                dlg.IsFolderPicker = false;
                dlg.Title = Utils.getString("TopicNotesDlg.BrowseDlgTitle.File");
                dlg.Filters.Add(new CommonFileDialogFilter("MindManager Maps", "*.mmap"));
            }

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                cbFindIn.Text = dlg.FileName;
                string what = "mappath:";
                if (dlg.IsFolderPicker) what = "folder:";
                cbFindIn.Tag = what + dlg.FileName;
            }
        }

        /// <summary>Get MM MyMaps Folders from registry</summary>
        /// <returns>Dictionary Key = Folder Name, Value = Folder Path</returns>
        private Dictionary<string, string> GetMyFolders()
        {
            var valuesBynames = new Dictionary<string, string>();
            int mmVersion = Utils.Version;
            string REGISTRY_ROOT = @"Software\Mindjet\MindManager\" + mmVersion.ToString() + @"\MyMaps\Folders";

            using (RegistryKey rootKey = Registry.CurrentUser.OpenSubKey(REGISTRY_ROOT))
            {
                if (rootKey != null)
                {
                    string[] valueNames = rootKey.GetValueNames();
                    foreach (string currSubKey in valueNames)
                    {
                        string value = rootKey.GetValue(currSubKey) as string;
                        valuesBynames.Add(currSubKey, value);
                    }
                    rootKey.Close();
                }

            }
            return valuesBynames;
        }

        private void linkSearchOptions_Click(object sender, EventArgs e)
        {
            cmsSearchOptions.Show(MousePosition);
        }

        private void CmsSearchOptions_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
        }

        private void CmsLookIn_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == LookInCurrenMap)
            {
                cbFindIn.Text = LookInCurrenMap.Text;
                cbFindIn.Tag = "currentMap";
            }
            else if (e.ClickedItem == LookInAllOpenMaps)
            {
                cbFindIn.Text = LookInAllOpenMaps.Text;
                cbFindIn.Tag = "openMaps";
            }
            else if (e.ClickedItem == LookInCollections) // Look in the all collections
            {
                cbFindIn.Text = LookInCollections.Text;
                cbFindIn.Tag = "mapcollections";
            }
            else if (e.ClickedItem.Name == "mapcollection") // look in the given collection
            {
                cbFindIn.Text = e.ClickedItem.Text;
                cbFindIn.Tag = "mc:" + e.ClickedItem.Text;
            }
            else if (e.ClickedItem == LookInFolders) // Look in the all folders
            {
                cbFindIn.Text = LookInFolders.Text;
                cbFindIn.Tag = "myfolders";
            }
            else if (e.ClickedItem.Name == "folder") // look in the given folder
            {
                cbFindIn.Text = e.ClickedItem.Text;
                cbFindIn.Tag = "folder:" + e.ClickedItem.Tag;
            }
            else if (e.ClickedItem.Name == "mappath")
            {
                cbFindIn.Text = e.ClickedItem.Text;
                cbFindIn.Tag = "mappath:" + e.ClickedItem.Tag;
            }
            else if (e.ClickedItem.Name == "RefreshCollections")
            {
                InitCollectionsAndFolders(true, false);
            }
            else if (e.ClickedItem.Name == "RefreshFolders")
            {
                InitCollectionsAndFolders(false);
            }

            cmsLookIn.Close();
        }

        private void CmsSearchOptions_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == SO_AddToResults)
            {
                if (SO_AddToResults.Checked)
                    SO_ReplaceResults.Checked = true;
                else
                    SO_ReplaceResults.Checked = false;
            }
            else if (e.ClickedItem == SO_ReplaceResults)
            {
                if (SO_ReplaceResults.Checked)
                    SO_AddToResults.Checked = true;
                else
                    SO_AddToResults.Checked = false;
            }
        }

        void SearchInNotes()
        {
            if (tabControl1.SelectedTab.Controls.OfType<WebBrowser>().Count() == 0) return;

            string _searchedText = cbSearchedText.Text.Trim().ToLower();
            WebBrowser wb = tabControl1.SelectedTab.Controls.OfType<WebBrowser>().First();
            falsealarm = true;
            // deselect all text
            editor.ClearSearchedText();
            if (_searchedText == "" || _searchedText == "*") return; // nothing to search for

            List<string> searchedText = new List<string>();

            if (_searchedText.StartsWith("\""))
            {
                if (_searchedText.Length < 3 || !_searchedText.EndsWith("\"")) return;
                searchedText.Add(_searchedText.TrimStart('\"').TrimEnd('\"'));
            }
            else
                searchedText = _searchedText.Replace("  ", " ").Split(' ').ToList();

            editor.SearchText(searchedText);
        }

        /// <summary>Show cbFindIn combobox context menu</summary>
        private void cbFindIn_Click(object sender, EventArgs e)
        {
            Point loc = new Point(0, cbFindIn.Height);
            cmsLookIn.Show(cbFindIn, loc);
            cmsLookIn.Focus();
        }

        /// <summary>
        /// Search topics with notes containing the text or the text in notes
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string lookin = cbFindIn.Tag as string;
            string _searchedText = cbSearchedText.Text.Trim().ToLower();
            if (_searchedText == "") return;
            List<string> searchedText = new List<string>();

            if (_searchedText.StartsWith("\"") && _searchedText.EndsWith("\""))
                searchedText.Add(_searchedText.TrimStart('\"').TrimEnd('\"'));
            else
                searchedText = _searchedText.Replace("  ", " ").Split(' ').ToList();

            List<string> keywords = Utils.getRegistry("SearchHistory", "").Split(';').ToList();
            if (_searchedText != "*" && !keywords.Contains(_searchedText))
            {
                if (keywords.Count >= 15) _ = keywords.Take(14).ToList();
                keywords.Insert(0, _searchedText);
                string _keywords = string.Join(";", keywords).TrimEnd(';');
                Utils.setRegistry("SearchHistory", _keywords);
                cbSearchedText.Items.Insert(0, searchedText);
            }

            TreeNode map = null;
            MyNodes.Clear();
            List<string> mappaths = new List<string>();

            if (SO_ReplaceResults.Checked) listTopics.Nodes.Clear();
            else if (SO_AddToResults.Checked) // get nodes to avoid duplicates
            {
                foreach (TreeNode node in listTopics.Nodes)
                {
                    foreach (TreeNode _node in node.Nodes)
                        MyNodes[_node] = (_node.Tag as TopicNotesItem).TopicGuid;
                }
            }

            m_progressDlg.Show(); int maps = 0, i = 1;

            if (lookin == "currentMap") // Search for notes in the current map
            {
                if (MMUtils.ActiveDocument != null)
                    map = SearchNotesInMap(searchedText, MMUtils.ActiveDocument.FullName, "1 / 1");
            }
            else if (lookin == "openMaps") // Search for notes in the all open maps
            {
                maps = MMUtils.MindManager.VisibleDocuments.Count;
                foreach (Document doc in MMUtils.MindManager.VisibleDocuments)
                {
                    string mapcount = i++ + " / " + maps;
                    map = SearchNotesInMap(searchedText, doc.FullName, mapcount);
                }
            }
            else if (lookin == "mapcollections") // Search for notes in the all map collections
            {
                foreach (MapShortcutCollection collection in MMUtils.MindManager.MapShortcutCollections)
                    maps += collection.Count;

                foreach (MapShortcutCollection collection in MMUtils.MindManager.MapShortcutCollections)
                {
                    foreach (MapShortcut _map in collection)
                    {
                        string mapcount = i++ + " / " + maps;
                        if (mappaths.Contains(_map.Path)) continue;
                        map = SearchNotesInMap(searchedText, _map.Path, mapcount);
                        mappaths.Add(_map.Path);
                    }
                }
            }
            else if (lookin.StartsWith("mc:")) // Search for notes in the specified map collection
            {
                string cName = lookin.Substring(3);
                
                foreach (MapShortcutCollection collection in MMUtils.MindManager.MapShortcutCollections)
                {
                    maps = collection.Count; // number of maps in the collection
                    if (collection.Name == cName)
                    {
                        foreach (MapShortcut _map in collection)
                        {
                            string mapcount = i++ + " / " + maps;
                            if (mappaths.Contains(_map.Path)) continue;
                            map = SearchNotesInMap(searchedText, _map.Path, mapcount);
                            mappaths.Add(_map.Path);
                        }
                    }
                }
            }
            else if (lookin == "myfolders") // Search for notes in the all folders
            {
                foreach (ToolStripItem folder in Folders.Items)
                {
                    if (folder as ToolStripMenuItem is ToolStripDropDownItem _folder)
                        maps += _folder.DropDown.Items.Count;
                }

                foreach (ToolStripItem folder in Folders.Items)
                {
                    if (folder as ToolStripMenuItem is ToolStripDropDownItem _folder)
                    {
                        foreach (ToolStripItem _map in _folder.DropDown.Items)
                        {
                            string mapcount = i++ + " / " + maps;
                            if (mappaths.Contains(_map.Tag as string)) continue;
                            map = SearchNotesInMap(searchedText, _map.Tag as string, mapcount);
                            mappaths.Add(_map.Tag as string);
                        }
                    }
                }
            }
            else if (lookin.StartsWith("folder:")) // Search for notes in the specified folder
            {
                string folderPath = lookin.Substring(7);

                DirectoryInfo di = new DirectoryInfo(folderPath);
                FileInfo[] ffi = di.GetFiles("*.mmap", SearchOption.TopDirectoryOnly);
                maps += ffi.Count();

                foreach (var fi in ffi)
                {
                    string mapcount = i++ + " / " + maps;
                    if (mappaths.Contains(fi.FullName)) continue;
                    map = SearchNotesInMap(searchedText, fi.FullName, mapcount);
                    mappaths.Add(fi.FullName);
                }
            }
            else if (lookin.StartsWith("mappath:")) // Is a map path
            {
                string mapPath = lookin.Substring(8);
                map = SearchNotesInMap(searchedText, mapPath, "1 / 1");
            }
            m_progressDlg.Hide();

            listTopics.Select();
            if (map != null)
                listTopics.SelectedNode = map.Nodes[0];
            else if (listTopics.Nodes.Count > 0)
                listTopics.SelectedNode = listTopics.Nodes[0];

            int total = listTopics.GetNodeCount(true);

            if (total > 3 && Utils.IsFree())
            {
                int mapcount = listTopics.Nodes.Count;
                total -= mapcount;

                MessageBox.Show(String.Format(Utils.getString("limitation.topic2notes2"), total, mapcount), 
                    Utils.getString("FreeVersionLimitation"));

                TreeNode first = listTopics.Nodes[0];
                listTopics.Nodes.Clear();
                listTopics.Nodes.Add(first);

                for (int k = first.Nodes.Count - 1; k > 1; k--)
                {
                    listTopics.Nodes[0].Nodes[k].Remove();
                }
            }
        }
        Dictionary<TreeNode, string> MyNodes = new Dictionary<TreeNode, string>();

        TreeNode SearchNotesInMap(List<string> searchedText, string mappath, string mapcount)
        {
            mappath = mappath.ToLower();
            var m_xmlDocument = XMLMapCompanion.Get(mappath);
            if (m_xmlDocument == null) return null;

            string mapName = XMLMapCompanion.CentralTopicText;
            TreeNode map = GetMapNode(mappath, mapName);

            m_progressDlg.dlgParams.minimum = 0;
            m_progressDlg.dlgParams.value = 1;
            m_progressDlg.dlgParams.maximum = XMLMapCompanion.m_topics.Count;

            string topicName, topicGuid, topicNotes;
            foreach (var t in XMLMapCompanion.m_topics)
            {
                m_progressDlg.dlgParams.value++;
                m_progressDlg.dlgParams.count = mapcount;
                System.Windows.Forms.Application.DoEvents();

                topicNotes = Utils.ClearTopicNotes(t.Value.NotesHtml());
                if (topicNotes == "") continue;

                topicName = t.Value.TopicText;
                topicGuid = t.Key;
                TopicNotesItem item = new TopicNotesItem(topicName, topicGuid, topicNotes);

                if (m_progressDlg.AbortPressed)
                {
                    if (map.Nodes.Count == 0) { map.Remove(); map = null; }
                    return map;
                }

                foreach (string search in searchedText)
                {
                    if (search == "*" || topicNotes.ToLower().Contains(search))
                        AddNotesNode(mappath, mapName, item, MyNodes.Values.Contains(topicGuid));
                }

                if (map.Nodes.Count == 1) 
                    map.Expand(); // user can view how topics are added
            }

            if (map.Nodes.Count == 0) { map.Remove(); map = null; }
            return map;
        }

        private void cbSearchedText_TextChanged(object sender, EventArgs e)
        {
            if (cbSearchedText.Text == "" || cbSearchedText.Text.Length > 1)
                SearchInNotes();
        }

        private void pDeleteSearchedText_Click(object sender, EventArgs e)
        {
            cbSearchedText.Text = "";
        }

        private void txtSearchNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchInNotes();

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region Buttons

        private void fontSize_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Controls.OfType<WebBrowser>().Count() == 0) return;

            bool selectall = false;
            if (editor.Selection == null || editor.Selection.text == null)
            {
                tabControl1.SelectedTab.Controls.OfType<WebBrowser>().First().Focus();
                editor.SelectAll();
                selectall = true;
            }
            try
            {
                var size = (int)editor.Selection.queryCommandValue("FontSize");
                if ((sender as PictureBox) == fontUp) size += 1; else size -= 1;
                if (size > 7 || size < 1) size = 3;
                editor.FontSize(size);
                if (selectall) selectallSize = size;
            }
            catch {
                if (selectall)
                {
                    if ((sender as PictureBox) == fontUp) selectallSize += 1; else selectallSize -= 1;
                    if (selectallSize > 7 || selectallSize < 1) selectallSize = 3;
                    editor.FontSize(selectallSize);
                }
                else { editor.FontSize(3); }
            }
            finally { if (selectall) editor.UnselectAll(); }
        }
        int selectallSize = 3;

        private void numFontSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                editor.FontSize(numFontSize.Value.ToString());
            }
        }

        public void pBold_Click(object sender, EventArgs e)
        {
            if (editor != null) editor.Bold();
            SelectionChanged(null, null);
        }

        public void pItalic_Click(object sender, EventArgs e)
        {
            if (editor != null) editor.Italic();
            SelectionChanged(null, null);
        }

        public void pStrikeout_Click(object sender, EventArgs e)
        {
            if (editor != null) editor.Strikethrough();
            SelectionChanged(null, null);
        }

        public void pUnderline_Click(object sender, EventArgs e)
        {
            if (editor != null) editor.Underline();
            SelectionChanged(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            fromTNDlg = true;
            SaveNotes(tabControl1.SelectedTab);
            fromTNDlg = false;
            UpdateSaveButtons();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            fromTNDlg = true;

            foreach (TabPage tp in tabControl1.TabPages)
                if (tp.AccessibleName == "edited")
                    SaveNotes(tp);

            fromTNDlg = false;
            UpdateSaveButtons();
        }

        void UpdateSaveButtons()
        {
            btnSaveOne.Visible = false; btnSaveAll.Visible = false;
            btnSaveOneNo.Visible = true; btnSaveAllNo.Visible = true;

            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (tp.AccessibleName == "edited")
                {
                    btnSaveAll.Visible = true;

                    if (tabControl1.SelectedTab == tp)
                        btnSaveOne.Visible = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        float font = 8.25F;
        public bool falsealarm = false;

        Image fBold, fItalic, fUnderline, fStrikethrough,
            fBoldActive, fItalicActive, fUnderlineActive, fStrikethroughActive;

        private void cbFontFamily_TextChanged(object sender, EventArgs e)
        {
            if (editor == null) return;
            editor.Font(cbFontFamily.Text);
        }

        private void cbFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int HoverIndex = -1;
        Rectangle Delete;

        public ThreadedProgressDlg m_progressDlg = new ThreadedProgressDlg();

        #region Utilities
        private void tabControlNotes_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var tp = tabControl1.TabPages[e.Index];
            var rt = e.Bounds;
            var rx = new Rectangle(rt.Right - 22, (rt.Y + (rt.Height - 16)) / 2 + 1, 16, 16);

            if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
            {
                rx.Offset(0, 2);
            }

            if (tp != PreviewPage)
            {
                rt.Inflate(-rx.Width, 0);
                rt.Offset(-(rx.Width / 2), 0);
            }

            using (Font f = new Font("Marlett", 10f))
            using (StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.Character,
                FormatFlags = StringFormatFlags.NoWrap,
            })
            {
                if (e.Index == this.tabControl1.SelectedIndex) // Active tab.
                    e.Graphics.FillRectangle(Brushes.White, rt.X, rt.Y, rt.Width, rt.Height);
                //if (tp.AccessibleName.Contains("edited")) // Content changed
                //    e.Graphics.FillRectangle(Brushes.Yellow, rt.X, rt.Y, rt.Width, rt.Height);

                if (tp.AccessibleName == "edited")
                    g.DrawString(tp.Text, tp.Font ?? Font, Brushes.Red, rt, sf);
                else
                    g.DrawString(tp.Text, tp.Font ?? Font, Brushes.Black, rt, sf);

                if (tp != PreviewPage) // we wan't delete the preview page
                    g.DrawString("r", f, HoverIndex == e.Index ? Brushes.Red : Brushes.LightGray, rx, sf);
            }
            tp.Tag = rx;
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            preselectedTab = tabControl1.SelectedTab;

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                var rx = (Rectangle)tabControl1.TabPages[i].Tag;

                if (rx.Contains(e.Location))
                {
                    //To avoid the redundant calls. 
                    if (HoverIndex != i)
                    {
                        HoverIndex = i;
                        tabControl1.Invalidate();
                    }
                    return;
                }
            }

            //To avoid the redundant calls.
            if (HoverIndex != -1)
            {
                HoverIndex = -1;
                tabControl1.Invalidate();
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            const int WM_NCLBUTTONDBLCLK = 0x00A3;    // this constant int is different

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                if (this.Height <= panelMinimized.Height + 10)
                    this.Bounds = WindowExpanded;
                else
                    this.Bounds = WindowCollapsed;

                this.OnResizeEnd(EventArgs.Empty);
            }
        }
    }

    #endregion

    public class TopicNotesItem
    {
        public TopicNotesItem(string topicName, string topicGuid, string notes)
        {
            TopicName = topicName;
            TopicGuid = topicGuid;
            TopicNotes = notes;
        }

        public string TopicName;
        public string TopicGuid;
        public string TopicNotes;
    }

    public class wbItem
    {
        public wbItem(string notes, string searchedText, TreeNode node, HtmlEditor editor)
        {
            tNotes = notes;
            SearchedText = searchedText;
            Node = node;
            Editor = editor;
        }

        public string tNotes;
        public string SearchedText;
        public TreeNode Node;
        public HtmlEditor Editor;
    }
}
