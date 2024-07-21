using Mindjet.MindManager.Interop;
using Organizer;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Bubbles
{
    public partial class TopicNotesDlg : Form
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

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            AddContextMenu(); // richTextBox context menu

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["MI_gototopic"].Text = Utils.getString("TopicNotesDlg.contextmenu.gototopic");
            StixUtils.SetContextMenuImage(contextMenuStrip1.Items["MI_gototopic"], "expand.png");

            contextMenuStrip1.Items["MI_remove"].Text = Utils.getString("TopicNotesDlg.contextmenu.remove");
            StixUtils.SetContextMenuImage(contextMenuStrip1.Items["MI_remove"], "deleteall.png");

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
        Rectangle WindowExpanded;
        Rectangle WindowCollapsed;

        private void This_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Height > panelMinimized.Height)
                WindowExpanded = this.Bounds;
            else
                WindowCollapsed = this.Bounds;
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "MI_gototopic")
            {
                if (listTopics.SelectedNode != null)
                    GetTopic(listTopics.SelectedNode, true);
            }
            else if (e.ClickedItem.Name == "MI_remove")
            {
                listTopics_KeyDown(null, null);
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
            if (selectednode == null || selectednode.Parent == null) return;

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
            }
            else // Open in the Preview page
            {
                if (PreviewPage.AccessibleName == "edited")
                {
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
                if (item.RtfNotes != "")
                    rtb.Rtf = item.RtfNotes;
                else
                    rtb.Text = item.PlainNotes;
            }

            btnSearch_Click(null, null);
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

        private void listTopics_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // SelectedNode here is a previous selected node!
                if (listTopics.SelectedNode != null && listTopics.SelectedNode.Parent != null)
                {
                    if (true) // Save notes text.
                    {
                        TopicNotesItem _item = listTopics.SelectedNode.Tag as TopicNotesItem;
                        _item.RtfNotes = rtbPreview.Rtf; _item.PlainNotes = rtbPreview.Text;
                        listTopics.SelectedNode.Tag = _item;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                listTopics.SelectedNode = e.Node;

                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;

                contextMenuStrip1.Show(Cursor.Position);
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

            //t = null; doc = null; // important!
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            falsealarm = true;

            string searchedText = cbSearchedText.Text.Trim().ToLower();
            // deselect all text
            rtbPreview.SelectAll();
            rtbPreview.SelectionBackColor = rtbPreview.BackColor;
            if (searchedText == "") return; // nothing to search for

            string tt = rtbPreview.Text.ToLower();
            Regex regex = new Regex(searchedText, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(tt);

            foreach (Match match in matches)
            {
                rtbPreview.Select(match.Index, match.Length);
                rtbPreview.SelectionBackColor = System.Drawing.Color.Yellow;
            }
            falsealarm = false;
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

        public void AddContextMenu()
        {
            if (rtbPreview.ContextMenuStrip == null)
            {
                ContextMenuStrip cms = new ContextMenuStrip()
                {
                    ShowImageMargin = false
                };

                ToolStripMenuItem tsmiCut = new ToolStripMenuItem(Utils.getString("button.cut"));
                tsmiCut.Click += (sender, e) => rtbPreview.Cut();
                cms.Items.Add(tsmiCut);

                ToolStripMenuItem tsmiCopy = new ToolStripMenuItem(Utils.getString("button.copy"));
                tsmiCopy.Click += (sender, e) => rtbPreview.Copy();
                cms.Items.Add(tsmiCopy);

                ToolStripMenuItem tsmiPaste = new ToolStripMenuItem(Utils.getString("button.paste"));
                tsmiPaste.Click += (sender, e) => rtbPreview.Paste();
                cms.Items.Add(tsmiPaste);

                cms.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem tsmiSelectAll = new ToolStripMenuItem(Utils.getString("button.selectall"));
                tsmiSelectAll.Click += (sender, e) => rtbPreview.SelectAll();
                cms.Items.Add(tsmiSelectAll);

                cms.Opening += (sender, e) =>
                {
                    tsmiCut.Enabled = !rtbPreview.ReadOnly && rtbPreview.SelectionLength > 0;
                    tsmiCopy.Enabled = rtbPreview.SelectionLength > 0;
                    tsmiPaste.Enabled = !rtbPreview.ReadOnly && System.Windows.Clipboard.ContainsText();
                    tsmiSelectAll.Enabled = rtbPreview.TextLength > 0 && rtbPreview.SelectionLength < rtbPreview.TextLength;
                };

                rtbPreview.ContextMenuStrip = cms;
            }
        }

        private void Change_RichTextBox_Size(float size)
        {
            if (rtbPreview.SelectionLength > 0) // change only selected text size
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont.Name, size, rtbPreview.SelectionFont.Style);
                return;
            }

            // Change all text size
            if (rtbPreview.TextLength == 0) return;
            panelEditButtons.Select();
            int currentsel = rtbPreview.SelectionStart; // remember position

            rtbPreview.Select(0, 1);
            var lastFontStyle = rtbPreview.SelectionFont.Style;
            var lastFontName = rtbPreview.SelectionFont.Name;
            var lastSelectionStart = 0;
            for (int i = 1; i < rtbPreview.TextLength; i++)
            {
                rtbPreview.Select(i, 1);

                var selStyle = rtbPreview.SelectionFont.Style;
                var selName = rtbPreview.SelectionFont.Name;

                if (selStyle != lastFontStyle || selName != lastFontName || i == rtbPreview.TextLength - 1)
                {
                    rtbPreview.Select(lastSelectionStart, i - lastSelectionStart);
                    rtbPreview.SelectionFont =
                        new Font(lastFontName, size, lastFontStyle);

                    lastFontStyle = selStyle;
                    lastFontName = selName;
                    lastSelectionStart = i;
                }
            }
            rtbPreview.Select(currentsel, 0); // restore position
        }

        private void txtSearchNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void pBold_Click(object sender, EventArgs e)
        {
            if (rtbPreview.SelectionFont.Bold)
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, ~FontStyle.Bold & rtbPreview.SelectionFont.Style);
                pBold.Image = fBold;
            }
            else
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, FontStyle.Bold | rtbPreview.SelectionFont.Style);
                pBold.Image = fBoldActive;
            }
        }

        private void pItalic_Click(object sender, EventArgs e)
        {
            if (rtbPreview.SelectionFont.Italic)
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, ~FontStyle.Italic & rtbPreview.SelectionFont.Style);
                pItalic.Image = fItalic;
            }
            else
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, FontStyle.Italic | rtbPreview.SelectionFont.Style);
                pItalic.Image = fItalicActive;
            }
        }

        private void pStrikeout_Click(object sender, EventArgs e)
        {
            if (rtbPreview.SelectionFont.Strikeout)
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, ~FontStyle.Strikeout & rtbPreview.SelectionFont.Style);
                pStrikeout.Image = fStrikeout;
            }
            else
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, FontStyle.Strikeout | rtbPreview.SelectionFont.Style);
                pStrikeout.Image = fStrikeoutActive;
            }
        }

        private void pUnderline_Click(object sender, EventArgs e)
        {
            if (rtbPreview.SelectionFont.Underline)
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, ~FontStyle.Underline & rtbPreview.SelectionFont.Style);
                pUnderline.Image = fUnderline;
            }
            else
            {
                rtbPreview.SelectionFont = new Font(rtbPreview.SelectionFont, FontStyle.Underline | rtbPreview.SelectionFont.Style);
                pUnderline.Image = fUnderlineActive;
            }
        }

        private void rtb_SelectionChanged(object sender, EventArgs e)
        {
            if (rtbPreview.SelectionFont.Bold) pBold.Image = fBoldActive;
            else pBold.Image = fBold;

            if (rtbPreview.SelectionFont.Italic) pItalic.Image = fItalicActive;
            else pItalic.Image = fItalic;

            if (rtbPreview.SelectionFont.Underline) pUnderline.Image = fUnderlineActive;
            else pUnderline.Image = fUnderline;

            if (rtbPreview.SelectionFont.Strikeout) pStrikeout.Image = fStrikeoutActive;
            else pStrikeout.Image = fStrikeout;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            fromTNDlg = true;
            SaveNotes(tabControl1.SelectedTab);
            fromTNDlg = false;
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            fromTNDlg = true;
            bool allsaved = true;
            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (tp.AccessibleName == "edited")
                    if (!SaveNotes(tp)) allsaved = false;
            }
            fromTNDlg = false;

            // Turn off the Save All button
            if (allsaved) {
                btnSaveAll.Visible = false; btnSaveAllNo.Visible = true; }

            if (tabControl1.SelectedTab.AccessibleName == "edit") {
                btnSaveOne.Visible = true; btnSaveOneNo.Visible = false; }
            else {
                btnSaveOne.Visible = false; btnSaveOneNo.Visible = true; }
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
            TreeNode map = null, node = null;
            foreach (TreeNode _node in listTopics.Nodes)
            {
                if (_node.Name == MMUtils.ActiveDocument.FullName)
                {
                    map = _node; break;
                }
            }
            if (map == null)
                map =listTopics.Nodes.Add(MMUtils.ActiveDocument.FullName, MMUtils.ActiveDocument.CentralTopic.Text, 0);

            map.NodeFont = new Font(map.NodeFont, FontStyle.Bold);

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (String.IsNullOrEmpty(t.Notes.Text))
                    continue;

                string notes = t.Notes.Text;
                string text = t.Text.Trim();
                if (String.IsNullOrEmpty(text)) text = Utils.getString("TopicNotesDlg.noname");

                string rtf = t.Notes.TextRTF;
                TopicNotesItem item = new TopicNotesItem(t, notes, rtf, t.Text, t.Guid);

                node = map.Nodes.Add(text);
                node.Tag = item;
            }
            // If we have added one topic only, select this topic
            if (node != null)
                listTopics.SelectedNode = node;
            listTopics.Select();
        }

        private void btnFindNotes_Click(object sender, EventArgs e)
        {

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
                TreeNode node = ((rtbItem)tp.Controls.OfType<RichTextBox>().First().Tag).Node;
                listTopics.SelectedNode = node;
                listTopics.Select();
            }
        }

        private void TopicNotesDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(Utils.getString("TopicNotesDlg.closewindow"), "", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

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
                            // Check if tab with this node is opened
                            foreach (TabPage tp in tabControl1.TabPages)
                            {
                                rtbItem rtbitem = tp.Controls.OfType<RichTextBox>().First().Tag as rtbItem;
                                if (rtbitem.Node == listTopics.SelectedNode) // Yes. it is opened
                                {
                                    if (tp.AccessibleName == "edited") // and it is modified!
                                    {
                                        // Save changes? 
                                        DialogResult dr = MessageBox.Show(Utils.getString("TopicNotesDlg.modifiednotes"), "",
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
                // Check Save buttons
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
        public TopicNotesItem(Topic t, string plainNotes, string rtfNotes, string topicName, string topicGuid)
        {
            topic = t;
            PlainNotes = plainNotes;
            RtfNotes = rtfNotes;
            TopicName = topicName;
            TopicGuid = topicGuid;
        }

        public Topic topic = null;
        public string PlainNotes;
        public string RtfNotes;
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
