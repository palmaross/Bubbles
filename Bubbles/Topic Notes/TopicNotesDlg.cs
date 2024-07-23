using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Bubbles
{
    internal partial class TopicNotesDlg : Form
    {
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

            btnGetNotes.Text = Utils.getString("topiccontextmenu.notes.detach");
            toolTip1.SetToolTip(btnSaveOne, Utils.getString("TopicNotesDlg.btnSaveOne"));
            toolTip1.SetToolTip(btnSaveAll, Utils.getString("TopicNotesDlg.btnSaveAll"));
            btnClose.Text = Utils.getString("button.close");

            toolTip1.SetToolTip(fontUp, Utils.getString("stickers.pIncreaseFont.tooltip"));
            toolTip1.SetToolTip(fontDown, Utils.getString("stickers.pDecreaseFont.tooltip"));

            tabControl1.TabPages.Remove(tabPage2);
            rtbPreview.Font = new Font("Microsoft Sans Serif", font);
            rtbPreview.SelectionChanged += rtb_SelectionChanged;
            rtbPreview.KeyDown += rtb_KeyDown;
            PreviewPage.AccessibleName = "";
            PreviewPage.Text = Utils.getString("TopicNotesDlg.PreviewPage");
            AddContextMenu(rtbPreview); // richTextBox context menu

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            cmsTopics.ItemClicked += ContextMenuStrip1_ItemClicked;
            cmsSearchOptions.ItemClicked += CmsSearchOptions_ItemClicked;
            cmsSearchOptions.Closing += CmsSearchOptions_Closing;

            SO_AddToResults.Text = Utils.getString("SO_AddToResults");
            SO_ReplaceResults.Text = Utils.getString("SO_ReplaceResults");
            SO_AddToResults.Checked = true;

            MI_gototopic.Text = Utils.getString("TopicNotesDlg.contextmenu.gototopic");
            StixUtils.SetContextMenuImage(MI_gototopic, "expand.png");

            MI_remove.Text = Utils.getString("TopicNotesDlg.contextmenu.remove");
            StixUtils.SetContextMenuImage(MI_remove, "deleteall.png");

            MI_UpdateTopicNotes.Text = Utils.getString("TopicNotesDlg.contextmenu.updatenotes");
            StixUtils.SetContextMenuImage(MI_UpdateTopicNotes, "refresh.png");

            fBold = Image.FromFile(Utils.ImagesPath + "f_bold.png");
            fItalic = Image.FromFile(Utils.ImagesPath + "f_italic.png");
            fUnderline = Image.FromFile(Utils.ImagesPath + "f_under.png");
            fStrikeout = Image.FromFile(Utils.ImagesPath + "f_strike.png");
            fBoldActive = Image.FromFile(Utils.ImagesPath + "f_boldActive.png");
            fItalicActive = Image.FromFile(Utils.ImagesPath + "f_italicActive.png");
            fUnderlineActive = Image.FromFile(Utils.ImagesPath + "f_underActive.png");
            fStrikeoutActive = Image.FromFile(Utils.ImagesPath + "f_strikeActive.png");

            this.HelpButtonClicked += This_HelpButtonClicked;
            this.ResizeEnd += This_ResizeEnd;

            btnSaveOne.Location = btnSaveOneNo.Location;
            btnSaveAll.Location = btnSaveAllNo.Location;

            cbFindIn.DisplayMember = "Text"; cbFindIn.ValueMember = "Value";

            //cbFindIn.Items.Add(new { Text = Utils.getString("LookIn.thisRtb"), Value = "thisRtb" });
            cbFindIn.Items.Add(new { Text = Utils.getString("LookIn.currentMap"), Value = "currentMap" });
            cbFindIn.Items.Add(new { Text = Utils.getString("LookIn.openMaps"), Value = "openMaps" });
            cbFindIn.SelectedIndex = 0;

            m_progressDlg.Create();
            m_progressDlg.dlgParams.title = Utils.getString("TopicNotesDlg.ProgressDlg.Title");
            m_progressDlg.dlgParams.abortTitle = Utils.getString("TopicNotesDlg.ProgressDlg.Abort");
            m_progressDlg.dlgParams.message = Utils.getString("TopicNotesDlg.ProgressDlg.message");
        }
        private void This_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Height > panelMinimized.Height)
                WindowExpanded = this.Bounds;
            else
                WindowCollapsed = this.Bounds;
        }

        private void CmsSearchOptions_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
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

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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
            else if (e.ClickedItem == MI_UpdateTopicNotes)
            {
                Document doc = null;

                string mappath = "";
                TreeNode node = listTopics.SelectedNode;
                if (node.Parent == null) // map
                    mappath = node.Name;
                else // topic
                    mappath = node.Parent.Name;

                foreach (Document _doc in MMUtils.MindManager.VisibleDocuments)
                {
                    if (_doc.FullName == mappath) doc = _doc; break;
                }

                bool mapcopy = false, mapopened = false;
                if (doc == null) { // we have to open document
                    doc = MMUtils.MindManager.AllDocuments.Open(mappath, "", false); mapopened = true; }
                else
                { // map is here. Get the copy.
                    doc = StixUtils.GetMapCopy(); mapcopy = true;
                    if (doc != null) mappath = doc.FullName;
                }

                if (doc == null) return;

                falsealarm = true;
                if (node.Parent == null) // map
                {
                    foreach (TreeNode _node in node.Nodes)
                        UpdateNotes(_node, doc, mapcopy);
                }
                else // topic
                {
                    UpdateNotes(node, doc, mapcopy);
                }
                falsealarm = false;

                if (mapopened || mapcopy) {
                    doc.Save(); doc.Close(); }
                if (mapcopy && File.Exists(mappath))
                    File.Delete(mappath);

                UpdateSaveButtons();
            }
        }

        void UpdateNotes(TreeNode node, Document doc, bool mapcopy)
        {
            // Get topic 
            TopicNotesItem item = node.Tag as TopicNotesItem;
            Topic t = item.topic;
            if (t == null || !t.IsValid)
                doc.FindByGuid(item.TopicGuid);
            if (t == null) return;

            string notes = t.Notes.Text;
            string topictext = t.Text.Trim();
            if (String.IsNullOrEmpty(topictext)) topictext = Utils.getString("TopicNotesDlg.noname");

            string rtf = "", html = "";
            if (!t.Notes.IsPlainTextOnly) { rtf = t.Notes.TextRTF; html = t.Notes.TextXHTML; }

            item = new TopicNotesItem(t, topictext, t.Guid, notes, rtf, html);
            node.Tag = item;

            if (mapcopy && item.topic != null && item.topic.IsValid)
                item.topic.Notes = t.Notes;

            foreach (TabPage tp in tabControl1.TabPages)
            {
                RichTextBox rtb = tp.Controls.OfType<RichTextBox>().First();
                rtbItem rtbitem = rtb.Tag as rtbItem;
                if (rtbitem.Node == node) // 
                {
                    if (item.RtfNotes != "")
                    {
                        rtb.Rtf = item.RtfNotes; rtbitem.RtfText = item.RtfNotes;
                    }
                    else rtb.Text = item.PlainNotes;
                    rtbitem.PlainText = item.PlainNotes;

                    tp.AccessibleName = ""; // remove status "modified"
                }
            }
        }

        private void btnNewTab_Click(object sender, EventArgs e)
        {
            newtab = true;
            listTopics_AfterSelect(null, null);
        }

        bool newtab = false;
        private void listTopics_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectednode = listTopics.SelectedNode;

            // No topic selected
            if (selectednode == null || selectednode.Parent == null) // switch to Preview Page
                return;

            RichTextBox rtb = null;
            // Check if notes are opened
            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (tp.Controls.OfType<RichTextBox>().First().Tag == null) break; // the very first time
                rtbItem rtbitem = tp.Controls.OfType<RichTextBox>().First().Tag as rtbItem;
                if (!newtab && rtbitem.Node == selectednode) // Node is opened already. Show.
                {
                    if (tp.AccessibleName == "edited") {
                        btnSaveOne.Visible = true; btnSaveOneNo.Visible = false; }
                    else {
                        btnSaveOne.Visible = false; btnSaveOneNo.Visible = true; }

                    tabControl1.SelectTab(tp); return; 
                }
            }

            TopicNotesItem item = selectednode.Tag as TopicNotesItem;
            rtbItem rtbTag = new rtbItem(item.PlainNotes, item.RtfNotes, "", selectednode);

            if (newtab) // Open in a new tab
            {
                newtab = false;
                rtb = new RichTextBox() { Dock = DockStyle.Fill, Tag = rtbTag };
                TabPage tp = new TabPage(selectednode.Text);
                tabControl1.TabPages.Add(tp);
                tp.Controls.Add(rtb);
                tabControl1.SelectTab(tp);
                rtb.TextChanged += rtb_TextChanged;
                rtb.SelectionChanged += rtb_SelectionChanged;
                rtb.KeyDown += rtb_KeyDown;
                AddContextMenu(rtb);
            }
            else // Open in the Preview page
            {
                if (PreviewPage.AccessibleName == "edited") // there are modified notes here
                {
                    // Save changes? 
                    DialogResult dr = MessageBox.Show(Utils.getString("TopicNotesDlg.notsavedpage"), "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    rtbItem preview = PreviewPage.Controls.OfType<RichTextBox>().First().Tag as rtbItem;

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
                }

                PreviewPage.Text = item.TopicName;
                tabControl1.SelectTab(PreviewPage);
                rtbPreview.Tag = rtbTag;
                rtb = rtbPreview;
            }

            falsealarm = true;
            btnSaveOne.Visible = false; btnSaveOneNo.Visible = true;

            // Show topic notes
            if (item != null)
            {
                if (item.RtfNotes == "")
                    rtb.Text = item.PlainNotes;
                else
                    rtb.Rtf = item.RtfNotes;
            }

            SearchInNotes();
            tabControl1.Invalidate();
            falsealarm = false;
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            if (falsealarm)
                return;
            else
            {
                btnSaveOne.Visible = true; btnSaveOneNo.Visible = false;
                btnSaveAll.Visible = true; btnSaveAllNo.Visible = false;

                // Set the tab text in red
                tabControl1.SelectedTab.AccessibleName = "edited";
                tabControl1.Invalidate();
            }
        }

        rtbItem preview_modified = null;
        private void listTopics_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Will be processed in the AfterSelect event.
            }
            else if (e.Button == MouseButtons.Right)
            {
                listTopics.SelectedNode = e.Node;

                foreach (ToolStripItem item in cmsTopics.Items)
                    item.Visible = true;

                cmsTopics.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Select topic in the map and bring into view
        /// </summary>
        private void listTopics_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GetTopic(listTopics.SelectedNode, true);
        }

        /// <summary>
        /// Get topic with notes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="selecttopic">If false, do not select topic, do not activate map</param>
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
                    if (topic) t = item.topic;
                    break;
                }
            }

            if (doc == null) // map is not opened
            {
                doc = MMUtils.MindManager.AllDocuments.Open(mappath, "", selecttopic);
                if (doc == null) return null; // we can't open the map

                if (topic) // 
                {
                    t = doc.FindByGuid(item.TopicGuid) as Topic;
                    if (t == null)
                    {
                        MessageBox.Show(Utils.getString("TopicNotesDlg.topiclost"));
                        listTopics.SelectedNode.Remove();
                        doc = null; return null;
                    }
                    item.topic = t;
                }
            }
            else if (t != null && !t.IsValid) // map was closed, but then opened again. Topic lost.
            {
                t = doc.FindByGuid(item.TopicGuid) as Topic;
                if (t == null) 
                {
                    MessageBox.Show(Utils.getString("TopicNotesDlg.topiclost"));
                    listTopics.SelectedNode.Remove();
                    doc = null; return null;
                }
                item.topic = t;
            }

            doc = null; // important!

            if (selecttopic) // User wants to view a topic
            {
                if (topic) {
                    t.SelectOnly(); t.SnapIntoView(); 
                }
                t = null; return t;
            }
            else // Function is called from the SaveNotes button
                return t;
        }

        void SearchInNotes()
        {
            string searchedText = cbSearchedText.Text.Trim().ToLower();

            falsealarm = true;
            RichTextBox rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().First();

            // deselect all text
            rtb.SelectAll();
            rtb.SelectionBackColor = rtb.BackColor;
            if (searchedText == "") return; // nothing to search for

            string tt = rtb.Text.ToLower();
            Regex regex = new Regex(searchedText, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(tt);

            foreach (Match match in matches)
            {
                rtb.Select(match.Index, match.Length);
                rtb.SelectionBackColor = System.Drawing.Color.Yellow;
            }
            falsealarm = false;
        }

        /// <summary>
        /// Search topics with notes conteining the text or the text in notes
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string lookin = (cbFindIn.SelectedItem as dynamic).Value;
            string searchedText = cbSearchedText.Text.Trim().ToLower();
            if (searchedText == "") return;

            TreeNode map = null;
            MyNodes.Clear();

            if (SO_ReplaceResults.Checked) listTopics.Nodes.Clear();
            else if (SO_AddToResults.Checked) // get nodes to avoid duplicates
            {
                foreach (TreeNode node in listTopics.Nodes)
                {
                    foreach (TreeNode _node in node.Nodes)
                        MyNodes[_node] = (_node.Tag as TopicNotesItem).TopicGuid;
                }
            }

            m_progressDlg.Show();

            if (lookin == "currentMap")
            {
                map = SearchNotesInMap(searchedText, MMUtils.ActiveDocument, "1 / 1");
            }
            else if (lookin == "openMaps")
            {
                int i = 1, maps = MMUtils.MindManager.VisibleDocuments.Count;
                foreach (Document doc in MMUtils.MindManager.VisibleDocuments)
                {
                    string mapcount = i++ + " / " + maps;
                    map = SearchNotesInMap(searchedText, doc, mapcount);
                }
            }
            m_progressDlg.Hide();

            listTopics.Select();
            if (map != null)
                listTopics.SelectedNode = map.Nodes[0];
            else if (listTopics.Nodes.Count > 0)
                listTopics.SelectedNode = listTopics.Nodes[0];
        }
        Dictionary<TreeNode, string> MyNodes = new Dictionary<TreeNode, string>();

        TreeNode SearchNotesInMap(string searchedText, Document doc, string mapcount)
        {
            TreeNode map = GetMapNode(doc);
            int max = doc.Range(MmRange.mmRangeAllTopics).Count;

            m_progressDlg.dlgParams.minimum = 0;
            m_progressDlg.dlgParams.value = 1;
            m_progressDlg.dlgParams.maximum = doc.Range(MmRange.mmRangeAllTopics).Count;

            foreach (Topic t in doc.Range(MmRange.mmRangeAllTopics))
            {
                if (m_progressDlg.AbortPressed)
                {
                    if (map.Nodes.Count == 0) { map.Remove(); map = null; }
                    return map;
                }

                m_progressDlg.dlgParams.value++;
                m_progressDlg.dlgParams.count = mapcount;
                System.Windows.Forms.Application.DoEvents();

                if (!t.Notes.IsTextEmpty && t.Notes.Text.Contains(searchedText))
                {
                    AddNotesNode(t, map, MyNodes.Values.Contains(t.Guid));
                }

                if (map.Nodes.Count == 1) map.Expand(); // user can view how topics are added
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

        private void fontUp_Click(object sender, EventArgs e)
        {
            if (font < 16.25F)
                font += 1;
            Change_RichTextBox_Size(font);
        }

        private void fontDown_Click(object sender, EventArgs e)
        {
            if (font > 8.25F)
                font -= 1;
            Change_RichTextBox_Size(font);
        }

        public void AddContextMenu(RichTextBox rtb)
        {
            if (rtb.ContextMenuStrip == null)
            {
                ContextMenuStrip cms = new ContextMenuStrip()
                {
                    ShowImageMargin = false
                };

                ToolStripMenuItem tsmiCut = new ToolStripMenuItem(Utils.getString("button.cut"));
                tsmiCut.Click += (sender, e) => rtb.Cut();
                cms.Items.Add(tsmiCut);

                ToolStripMenuItem tsmiCopy = new ToolStripMenuItem(Utils.getString("button.copy"));
                tsmiCopy.Click += (sender, e) => rtb.Copy();
                cms.Items.Add(tsmiCopy);

                ToolStripMenuItem tsmiPaste = new ToolStripMenuItem(Utils.getString("button.paste"));
                tsmiPaste.Click += (sender, e) => rtb.Paste();
                cms.Items.Add(tsmiPaste);

                cms.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem tsmiSelectAll = new ToolStripMenuItem(Utils.getString("button.selectall"));
                tsmiSelectAll.Click += (sender, e) => rtb.SelectAll();
                cms.Items.Add(tsmiSelectAll);

                cms.Opening += (sender, e) =>
                {
                    tsmiCut.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                    tsmiCopy.Enabled = rtb.SelectionLength > 0;
                    tsmiPaste.Enabled = !rtb.ReadOnly && System.Windows.Clipboard.ContainsText();
                    tsmiSelectAll.Enabled = rtb.TextLength > 0 && rtb.SelectionLength < rtb.TextLength;
                };

                rtb.ContextMenuStrip = cms;
            }
        }

        private void Change_RichTextBox_Size(float size)
        {
            RichTextBox rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().First();

            // Change all text size
            if (rtb.TextLength == 0) return;
            panelEditButtons.Select();
            int currentsel = rtb.SelectionStart; // remember position

            rtb.Select(0, 1);
            var lastFontStyle = rtb.SelectionFont.Style;
            var lastFontName = rtb.SelectionFont.Name;
            var lastSelectionStart = 0;
            for (int i = 1; i < rtb.TextLength; i++)
            {
                rtb.Select(i, 1);

                var selStyle = rtb.SelectionFont.Style;
                var selName = rtb.SelectionFont.Name;

                if (selStyle != lastFontStyle || selName != lastFontName || i == rtb.TextLength - 1)
                {
                    rtb.Select(lastSelectionStart, i - lastSelectionStart);
                    rtb.SelectionFont =
                        new Font(lastFontName, size, lastFontStyle);

                    lastFontStyle = selStyle;
                    lastFontName = selName;
                    lastSelectionStart = i;
                }
            }
            rtb.Select(currentsel, 0); // restore position
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

        private void pBold_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().First();

            if (rtb.SelectionFont.Bold)
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, ~FontStyle.Bold & rtb.SelectionFont.Style);
                pBold.Image = fBold;
            }
            else
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Bold | rtb.SelectionFont.Style);
                pBold.Image = fBoldActive;
            }
        }

        private void pItalic_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().First();

            if (rtb.SelectionFont.Italic)
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, ~FontStyle.Italic & rtb.SelectionFont.Style);
                pItalic.Image = fItalic;
            }
            else
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Italic | rtb.SelectionFont.Style);
                pItalic.Image = fItalicActive;
            }
        }

        private void pStrikeout_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().First();

            if (rtb.SelectionFont.Strikeout)
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, ~FontStyle.Strikeout & rtb.SelectionFont.Style);
                pStrikeout.Image = fStrikeout;
            }
            else
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Strikeout | rtb.SelectionFont.Style);
                pStrikeout.Image = fStrikeoutActive;
            }
        }

        private void pUnderline_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().First();

            if (rtb.SelectionFont.Underline)
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, ~FontStyle.Underline & rtb.SelectionFont.Style);
                pUnderline.Image = fUnderline;
            }
            else
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Underline | rtb.SelectionFont.Style);
                pUnderline.Image = fUnderlineActive;
            }
        }

        private void rtb_SelectionChanged(object sender, EventArgs e)
        {

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

        private bool SaveNotes(TabPage tp)
        {
            string text = tp.Controls.OfType<RichTextBox>().First().Text;
            string rtf = tp.Controls.OfType<RichTextBox>().First().Rtf;
            TreeNode node = ((rtbItem)tp.Controls.OfType<RichTextBox>().First().Tag).Node;

            // Find topic with this notes
            TopicNotesItem item = node.Tag as TopicNotesItem;
            Topic t = item.topic;
            if (t == null || !t.IsValid)
                t = GetTopic(node, false); // Get the needed topic

            if (t == null)
            {
                // Message to user
                return false;
            }

            // Replace topic notes

            item.PlainNotes = text; item.RtfNotes = rtf; node.Tag = item;
            t.Notes.TextRTF = rtf;
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

        private void btnGetNotes_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0) return;

            TreeNode map = GetMapNode(MMUtils.ActiveDocument);

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (String.IsNullOrEmpty(t.Notes.Text))
                    continue;
                AddNotesNode(t, map);
            }

            if (map.Nodes.Count == 0) map.Remove();

            listTopics.SelectedNode = map;
            listTopics.Select();
        }

        TreeNode GetMapNode(Document doc)
        {
            TreeNode map = null;
            foreach (TreeNode _node in listTopics.Nodes)
            {
                if (_node.Name == doc.FullName)
                {
                    map = _node; break;
                }
            }
            if (map == null)
                map = listTopics.Nodes.Add(doc.FullName, doc.CentralTopic.Text, 0);

            map.NodeFont = new Font(listTopics.Font, FontStyle.Bold);
            map.Text = map.Text;
            return map;
        }

        void AddNotesNode(Topic t, TreeNode map, bool replace = false)
        {
            TreeNode node = null;
            string notes = t.Notes.Text;
            string topictext = t.Text.Trim();
            if (String.IsNullOrEmpty(topictext)) topictext = Utils.getString("TopicNotesDlg.noname");

            string rtf = "", html = "";
            if (!t.Notes.IsPlainTextOnly) { rtf = t.Notes.TextRTF; html = t.Notes.TextXHTML; }

            TopicNotesItem item = new TopicNotesItem(t, topictext, t.Guid, notes, rtf, html);

            if (replace) node = map;
            else node = map.Nodes.Add(topictext);
            node.Tag = item;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        float font = 8.25F;
        public bool falsealarm = false;

        Image fBold, fItalic, fUnderline, fStrikeout,
            fBoldActive, fItalicActive, fUnderlineActive, fStrikeoutActive;

        private int HoverIndex = -1;
        Rectangle Delete;

        public ThreadedProgressDlg m_progressDlg = new ThreadedProgressDlg();

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

        private void linkSearchOptions_Click(object sender, EventArgs e)
        {
            cmsSearchOptions.Show(MousePosition);
        }

        TabPage preselectedTab = null;

        private void tabControl1_MouseLeave(object sender, EventArgs e)
        {
            if (HoverIndex != -1)
            {
                HoverIndex = -1;
                tabControl1.Invalidate();
            }
        }

        /// <summary>
        /// Remove tab.
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
                tp.Dispose();

                // Get the previous node
                if (prevdeleted)
                {
                    TreeNode node = ((rtbItem)tabControl1.TabPages[i].Controls.OfType<RichTextBox>().First().Tag).Node;
                    listTopics.SelectedNode = node;
                    listTopics.Select();
                }
                else
                {
                    listTopics.SelectedNode = ((rtbItem)preselectedTab.Controls.OfType<RichTextBox>().First().Tag).Node;
                    listTopics.Select();
                }
            }
            else // Select appropiate node
            {
                if ((rtbItem)tp.Controls.OfType<RichTextBox>().First().Tag != null)
                {
                    TreeNode node = ((rtbItem)tp.Controls.OfType<RichTextBox>().First().Tag).Node;
                    listTopics.SelectedNode = node;
                    listTopics.Select();
                }
            }

            UpdateSaveButtons();
        }

        private void TopicNotesDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MMClose)
            {
                if (MessageBox.Show(Utils.getString("TopicNotesDlg.closewindow"), "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            m_progressDlg.Destroy();
        }
        public bool MMClose = false;

        private void rtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.B)
            {
                pBold_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                pItalic_Click(null, null);
                e.SuppressKeyPress = true; // important!
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {
                pUnderline_Click(null, null);
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                pStrikeout_Click(null, null);
            }
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
                                    rtbItem rtbitem = tp.Controls.OfType<RichTextBox>().First().Tag as rtbItem;
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
                                        if (tp != PreviewPage)
                                            tabControl1.TabPages.Remove(tp);
                                    }
                                }
                            }
                        }

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
                            rtbItem rtbitem = tp.Controls.OfType<RichTextBox>().First().Tag as rtbItem;
                            if (rtbitem.Node == listTopics.SelectedNode) // Yes. it is opened
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
                                if (tp != PreviewPage)
                                    tabControl1.TabPages.Remove(tp);
                            }
                        }

                        // What node will be selected after the node removing?
                        TreeNode selectnode = listTopics.SelectedNode.PrevNode;
                        if (selectnode == null) selectnode = listTopics.SelectedNode.NextNode;
                        if (selectnode == null) selectnode = listTopics.SelectedNode.Parent;

                        listTopics.SelectedNode.Remove();
                        if (selectnode != null)
                            listTopics.SelectedNode = selectnode;
                    }
                }

                if (listTopics.Nodes.Count == 0)
                {
                    PreviewPage.Controls.OfType<RichTextBox>().First().Text = "";
                    PreviewPage.AccessibleName = "";
                    PreviewPage.Text = Utils.getString("TopicNotesDlg.PreviewPage");
                }
                UpdateSaveButtons();
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

    public class TopicNotesItem
    {
        public TopicNotesItem(Topic t, string topicName, string topicGuid, string text, string rtf, string html)
        {
            topic = t;
            TopicName = topicName;
            TopicGuid = topicGuid;
            PlainNotes = text;
            RtfNotes = rtf;
            HtmlNotes = html;
        }

        public Topic topic = null;
        public string PlainNotes;
        public string RtfNotes;
        public string HtmlNotes;
        public string TopicName;
        public string TopicGuid;

        public override string ToString()
        {
            return TopicName;
        }
    }

    public class rtbItem
    {
        public rtbItem(string plainText, string rtfText, string searchedText, TreeNode node)
        {
            PlainText = plainText;
            RtfText = rtfText;
            SearchedText = searchedText;
            Node = node;
        }

        public string PlainText;
        public string RtfText;
        public string SearchedText;
        public TreeNode Node;
    }
}
