using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
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

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "IconStick.htm");

            Text = Utils.getString("TopicNotesDlg.title");
            lblTopics.Text = Utils.getString("TopicNotesDlg.lblTopics");
            lblMap.Text = Utils.getString("TopicNotesDlg.lblMap");
            btnSave.Text = Utils.getString("TopicNotesDlg.btnSave");

            toolTip1.SetToolTip(fontUp, Utils.getString("stickers.pIncreaseFont.tooltip"));
            toolTip1.SetToolTip(fontDown, Utils.getString("stickers.pDecreaseFont.tooltip"));
            toolTip1.SetToolTip(pSearchNotes, Utils.getString("TopicNotesDlg.pSearchNotes.tooltip"));
            toolTip1.SetToolTip(pHelp, Utils.getString("button.help"));

            rtb.Font = new Font("Microsoft Sans Serif", font);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            AddContextMenu(); // richTextBox context menu

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["MI_gototopic"].Text = Utils.getString("TopicNotesDlg.contextmenu.gototopic");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["MI_gototopic"], "expand.png");

            contextMenuStrip1.Items["MI_remove"].Text = Utils.getString("TopicNotesDlg.contextmenu.remove");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["MI_remove"], "deleteall.png");

            fBold = Image.FromFile(Utils.ImagesPath + "f_bold.png");
            fItalic = Image.FromFile(Utils.ImagesPath + "f_italic.png");
            fUnderline = Image.FromFile(Utils.ImagesPath + "f_under.png");
            fStrikeout = Image.FromFile(Utils.ImagesPath + "f_strike.png");
            fBoldActive = Image.FromFile(Utils.ImagesPath + "f_boldActive.png");
            fItalicActive = Image.FromFile(Utils.ImagesPath + "f_italicActive.png");
            fUnderlineActive = Image.FromFile(Utils.ImagesPath + "f_underActive.png");
            fStrikeoutActive = Image.FromFile(Utils.ImagesPath + "f_strikeActive.png");   
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "MI_gototopic")
            {
                if (listTopics.SelectedItem != null)
                    listTopics_MouseDoubleClick(null, null);
            }
            else if (e.ClickedItem.Name == "MI_remove")
            {
                listTopics_KeyDown(null, null);
            }
        }

        private void listTopics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedItemIndex >= 0)
            {
                if (textchanged) // save notes text
                {
                    TopicNotesItem _item = listTopics.Items[selectedItemIndex] as TopicNotesItem;
                    _item.RtfNotes = rtb.Rtf; _item.PlainNotes = rtb.Text;
                    listTopics.Items[selectedItemIndex] = _item;
                }
            }

            TopicNotesItem item = listTopics.SelectedItem as TopicNotesItem;
            if (item != null)
            {
                if (item.RtfNotes != "")
                    rtb.Rtf = item.RtfNotes;
                else
                    rtb.Text = item.PlainNotes;

                txtMapName.Text = item.MapName;
            }

            selectedItemIndex = listTopics.SelectedIndex;
            pSearchNotes_Click(null, null);
        }

        private void listTopics_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;

                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Select topic in the map and bring into view
        /// </summary>
        private void listTopics_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TopicNotesItem item = listTopics.SelectedItem as TopicNotesItem;
            Document doc = null; Topic t = null;

            if (item != null)
            {
                foreach (Document _doc in MMUtils.MindManager.AllDocuments)
                {
                    if (_doc.FullName == item.MapPath)
                    {
                        doc = _doc; doc.Activate();
                        t = item.topic;
                        break;
                    }
                }

                if (doc == null) // map was closed
                {
                    doc = MMUtils.MindManager.AllDocuments.Open(item.MapPath);
                    if (doc == null) return;

                    t = doc.FindByGuid(item.TopicGuid) as Topic;
                    if (t == null) { doc = null; return; }

                    item.topic = t;
                    listTopics.SelectedItem = item;
                }
                else if (!t.IsValid) // map was closed, but then opened again. Topic lost.
                {
                    t = doc.FindByGuid(item.TopicGuid) as Topic;
                    if (t == null) { doc = null; return; }

                    item.topic = t;
                    listTopics.SelectedItem = item;
                }

                t.SelectOnly(); t.SnapIntoView();
                t = null; doc = null; // important!
            }
        }

        private void pSearchNotes_Click(object sender, EventArgs e)
        {
            string searchedText = txtSearchNotes.Text.Trim().ToLower();
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
        }

        private void pHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "GetTopicNotes.htm");
        }

        private void fontUp_Click(object sender, EventArgs e)
        {
            if (font < 16.25F)
                font += 1;
            Change_RichTextBox_Size(font);
            //rtb.Font = new Font("Microsoft Sans Serif", font);
            //rtb.Rtf = rtb.Rtf;
        }

        private void fontDown_Click(object sender, EventArgs e)
        {
            if (font > 8.25F)
                font -= 1;
            Change_RichTextBox_Size(font);
            //rtb.Font = new Font("Microsoft Sans Serif", font);
            //rtb.Rtf = rtb.Rtf;
        }

        public void AddContextMenu()
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
            if (rtb.SelectionLength > 0) // change only selected text size
            {
                rtb.SelectionFont = new Font(rtb.SelectionFont.Name, size, rtb.SelectionFont.Style);
                return;
            }

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

        private void txtSearchNotes_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchNotes.Text.Trim() == "")
                pSearchNotes_Click(null, null);
        }

        private void txtSearchNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pSearchNotes_Click(null, null);

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void pBold_Click(object sender, EventArgs e)
        {
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
            if (rtb.SelectionFont.Bold) pBold.Image = fBoldActive;
            else pBold.Image = fBold;

            if (rtb.SelectionFont.Italic) pItalic.Image = fItalicActive;
            else pItalic.Image = fItalic;

            if (rtb.SelectionFont.Underline) pUnderline.Image = fUnderlineActive;
            else pUnderline.Image = fUnderline;

            if (rtb.SelectionFont.Strikeout) pStrikeout.Image = fStrikeoutActive;
            else pStrikeout.Image = fStrikeout;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get topic with this notes
            listTopics_MouseDoubleClick(null, null); // now topic is selected in the map
            // Replace topic notes
            MMUtils.ActiveDocument.Selection.PrimaryTopic.Notes.TextRTF = rtb.Rtf;
            MMUtils.ActiveDocument.Selection.PrimaryTopic.Notes.Commit();
            // Confirm the action
            pictOK.Visible = true;
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            pictOK.Visible = false;
        }

        float font = 8.25F;
        bool textchanged = false;
        int selectedItemIndex = -1;

        Image fBold, fItalic, fUnderline, fStrikeout,
            fBoldActive, fItalicActive, fUnderlineActive, fStrikeoutActive;

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

        private void listTopics_KeyDown(object sender, KeyEventArgs e)
        {
            if (e == null || e.KeyCode == Keys.Delete)
            {
                if (listTopics.SelectedItem != null)
                {
                    listTopics.Items.Remove(listTopics.SelectedItem);

                    if (listTopics.Items.Count > 0)
                        listTopics.SelectedIndex = 0;
                    else
                        rtb.Clear();
                }
            }
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            textchanged = true;
        }
    }

    public class TopicNotesItem
    {
        public TopicNotesItem(Topic t, string plainNotes, string rtfNotes, string topicName, string topicGuid, string mapPath, string mapName)
        {
            topic = t;
            PlainNotes = plainNotes;
            RtfNotes = rtfNotes;
            TopicName = topicName;
            TopicGuid = topicGuid;
            MapPath = mapPath;
            MapName = mapName;
        }

        public Topic topic = null;
        public string PlainNotes;
        public string RtfNotes;
        public string TopicName;
        public string TopicGuid;
        public string MapPath;
        public string MapName;

        public override string ToString()
        {
            return TopicName;
        }
    }
}
