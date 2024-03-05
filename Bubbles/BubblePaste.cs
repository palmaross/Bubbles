using System;
using System.Linq;
using System.Windows.Forms;
using PRAManager;
using Mindjet.MindManager.Interop;
using System.Runtime.InteropServices;
using WindowsInput.Native;
using WindowsInput;
using System.Drawing;
using System.Collections.Generic;
using Clipboard = System.Windows.Forms.Clipboard;
using Timer = System.Windows.Forms.Timer;
using PRMapCompanion;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Bubbles
{
    internal partial class BubblePaste : Form
    {
        public BubblePaste(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "PasteStick.htm");

            RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            toolTip1.SetToolTip(subtopic, Utils.getString("BubblesPaste.pastesubtopic"));
            toolTip1.SetToolTip(pPasteToTopic, Utils.getString("BubblesPaste.pPaste.tooltip"));
            toolTip1.SetToolTip(pCopyTopicText, Utils.getString("BubblesPaste.pCopy.tooltip"));
            toolTip1.SetToolTip(PasteLink, Utils.getString("BubblesPaste.PasteLink.tooltip"));
            toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.PasteNotes.tooltip"));
            toolTip1.SetToolTip(UnformatText, Utils.getString("BubblesPaste.unformate.tooltip"));
            toolTip1.SetToolTip(pReplace, Utils.getString("BubblesPaste.pReplace.tooltip"));

            toolTip1.SetToolTip(OptionTextFormat, Utils.getString("BubblesPaste.workwith.unformatted"));
            toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("paste.contextmenu.insert2"));
            toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("paste.contextmenu.multipletopics2"));
            toolTip1.SetToolTip(OptionSourceLink, Utils.getString("BubblesPaste.sourcelink_no"));
            toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("BubblesPaste.internallinks_no"));

            toolTip1.SetToolTip(pictureHandle, stickname);

            cmsOptions.ItemClicked += ContextMenu_ItemClicked;
            cmsCommon.ItemClicked += ContextMenu_ItemClicked;

            OP_myrisk.Text = Utils.getString("pastenotes.contextmenu.quickinsert");
            OP_myrisk.ToolTipText = Utils.getString("pastenotes.contextmenu.quickinsert.tooltip");
            OP_myrisk.Tag = "myrisk";

            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typepaste);
            //cmsOptions.Closing += CmsOptions_Closing;

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste);

            if (collapsed) {
                collapsed = false; Collapse(); }

            PasteOperations = new Timer() { Interval = 50 };
            PasteOperations.Tick += PasteOperations_Tick;
        }

        private void CmsOptions_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
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

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == OP_myrisk)
            {
                if (OP_myrisk.Checked) // user unchecks _Unsafe_ Insert Notes mode
                {
                    PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotes.png");

                    if (OptionReplaceInsert.Tag.ToString() == "replace")
                        toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.PasteNotes.tooltip"));
                    else
                        toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.AddNotes.tooltip"));
                }
                else // user checks _Unsafe_ Insert Notes mode
                {
                    if (OptionReplaceInsert.Tag.ToString() == "insert")
                    {
                        PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotesRisk.png");
                        toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.AddNotes.unsafe.tooltip"));
                    }
                    else
                    {
                        PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotes.png");
                        toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.PasteNotes.tooltip"));
                    }
                }
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "PasteStick.htm");
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
            orientation = StickUtils.RotateStick(this, Manage, orientation);

            panelOptions.Size = new Size(panelOptions.Height, panelOptions.Width);
            panelOptions.Location = new Point(panelOptions.Location.Y, panelOptions.Location.X);
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
                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;
                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                    this.Location = collapseState; // restore collapsed location
            }
        }
        Point collapseState = new Point(0, 0);

        private void PasteLink_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null &&
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0 &&
                Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Hyperlinks.AddHyperlink(System.Windows.Forms.Clipboard.GetText());
                }
            }
        }

        private void PasteNotes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsOptions.Items)
                    item.Visible = false;

                OP_myrisk.Visible = true;

                cmsOptions.Show(Cursor.Position);
                return;
            }

            if (MMUtils.ActiveDocument == null || !Clipboard.ContainsText() ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            replace = OptionReplaceInsert.Tag.ToString() == "replace";

            SelectedTopics.Clear();
            SelectedTopics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>());
            string rtf = Clipboard.GetText(TextDataFormat.Rtf);

            if (OptionTextFormat.Tag.ToString() == "formatted" && String.IsNullOrEmpty(rtf))
            {
                // we have to make the rtf through the copying Clipboard to the topic
                DocumentStorage.Sync(MMUtils.ActiveDocument); // subscribe document to onObjectAdded event
                pastetext = true;
                PastedTopics.Clear();

                StickUtils.ActivateMindManager();
                SelectedTopics[0].SelectOnly(); // get first of the selected topics
                PasteOperations.Start(); // start timer to process formatted text
                                         // paste from clipboard (in MM23 Selection.Paste() doesn't work!)
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                return;
            }

            if (replace) // replace topic notes text with text from Clipboard
            {
                foreach (Topic t in SelectedTopics)
                {
                    UserActionNotes = false; // do not add notes to the TopicsWithNotes

                    if (OptionTextFormat.Tag.ToString() == "formatted")
                        t.Notes.TextRTF = rtf;
                    else
                        t.Notes.Text = Clipboard.GetText(TextDataFormat.UnicodeText);

                    UserActionNotes = false; // do not add notes to the TopicsWithNotes
                    t.Notes.Commit();
                }
            }
            else // insert text at the end
            {
                Document doc = null; Topic tcopy;

                if (!OP_myrisk.Checked)
                { doc = SaveOpenMapCopy(); }

                foreach (Topic t in SelectedTopics)
                {
                    UserActionNotes = false; // do not add notes to the TopicsWithNotes

                    if (doc != null)
                    {
                        tcopy = doc.FindByGuid(t.Guid) as Topic;
                        if (tcopy == null) continue;
                        t.Notes.TextXHTML = tcopy.Notes.TextXHTML;
                    }

                    if (OptionTextFormat.Tag.ToString() == "formatted")
                    {
                        t.Notes.AppendRtf(rtf);
                    }
                    else // unformatted text
                    {
                        t.Notes.CursorPosition = -1;
                        t.Notes.Insert("\r\n" + Clipboard.GetText(TextDataFormat.UnicodeText));
                    }

                    UserActionNotes = false; // do not add notes to the TopicsWithNotes
                    t.Notes.Commit();
                }

                if (doc != null)
                {
                    // Close and delete temp doc
                    string path = doc.FullName;
                    doc.Close();
                    try { File.Delete(path); } catch { }
                    doc = null; tcopy = null;
                }
            }
        }

        public static List<string> TopicsWithNotes = new List<string>();
        public static bool UserActionNotes = true;

        Document SaveOpenMapCopy()
        {
            string aName = MMUtils.nowUnixTimestamp() + ".mmap"; // temp map
            string path = Utils.m_localDataPath + aName;
            MMUtils.ActiveDocument.SaveAs(path, true); // save it to temp directory

            // Wait document to be active, as opening is asyncronous thing.
            long _now = MMUtils.GetTimestamp();
            bool mapopening = false;

            while ((MMUtils.GetTimestamp() - _now) < 30)
            {
                int _ts = (int)(MMUtils.GetTimestamp() - _now);
                if (_ts > 30) _ts = 30;

                try { System.Windows.Forms.Application.DoEvents(); }
                catch { }

                if (File.Exists(path) && !mapopening) // map has been saved
                {
                    if (!mapopening) // open it (if not opened already)
                    {
                        MMUtils.MindManager.AllDocuments.Open(path, "", false);
                        mapopening = true;
                    }

                    // Try to locate document
                    foreach (Document _doc in MMUtils.MindManager.AllDocuments)
                    {
                        if (_doc.Name == aName) 
                            return _doc;
                    }
                }
            }
            // 30 sec. passed, document was not opened, so...
            return null;
        }

        private void AddPasteTopic_MouseHover(object sender, EventArgs e)
        {
            //PictureBox pb = sender as PictureBox;

            //if (pb.Name == "pPasteTopic")
            //    StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "paste");
            //else
            //    StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "add");
        }

        private void pCopy_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (OptionTextFormat.Tag.ToString() == "unformatted")
                {
                    string text = "";
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        text += t.Text + "\r\n";

                    text = text.TrimEnd('\n').TrimEnd('\r');

                    if (text != "")
                        Clipboard.SetText(text);
                }
                else
                {
                    rtb.Clear();
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        //move cursor to the end
                        rtb.Select(rtb.TextLength, 0);
                        //append the rtf
                        rtb.SelectedRtf = t.Title.TextRTF;
                    }

                    DataObject dto = new DataObject();
                    dto.SetText(rtb.Rtf, TextDataFormat.Rtf);
                    dto.SetText(rtb.Text, TextDataFormat.UnicodeText);
                    Clipboard.Clear(); Clipboard.SetDataObject(dto);
                }
            }
            //else if (e.Button == MouseButtons.Right)
            //{
            //    foreach (ToolStripItem item in cmsOptions.Items)
            //        item.Visible = false;

            //    cmsOptions.Show(Cursor.Position);
            //}
        }

        public static bool pastetext = false;
        bool replace = false;
        /// <summary>
        /// Paste text from clipboard to the selected topics
        /// </summary>
        public void pPasteToTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // If Link options are checked get links from copied text
                StickUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes", OptionInternalLinks.Tag.ToString() == "yes");

                if (MMUtils.ActiveDocument == null || !Clipboard.ContainsText() ||
                    MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
                    return;

                replace = OptionReplaceInsert.Tag.ToString() == "replace";

                SelectedTopics.Clear();
                SelectedTopics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>());

                if (OptionTextFormat.Tag.ToString() == "formatted")
                {                 
                    if (Clipboard.ContainsData(System.Windows.DataFormats.Rtf)) // we have the rtf text already
                    {
                        Transaction _tr = MMUtils.ActiveDocument.NewTransaction("Paste Text");
                        _tr.IsUndoable = true;
                        _tr.Execute += new ITransactionEvents_ExecuteEventHandler(TrsPasteToTopicFormatted);
                        _tr.Start();
                    }
                    else // we have to use the Ctrl-V method to convert clipboard content to topics
                    {
                        DocumentStorage.Sync(MMUtils.ActiveDocument); // subscribe document to onObjectAdded event
                        pastetext = true;
                        PastedTopics.Clear();

                        StickUtils.ActivateMindManager();
                        pasteOperation = "pastetotopic";
                        SelectedTopics[0].SelectOnly(); // select the first of selected topics
                        PasteOperations.Start(); // start timer to process pasted topics
                                                 // paste text from clipboard (in MM23 Selection.Paste() doesn't work!)
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                    }
                }
                else // paste unformatted text
                {
                    Transaction _tr = MMUtils.ActiveDocument.NewTransaction("Paste Text");
                    _tr.IsUndoable = true;
                    _tr.Execute += new ITransactionEvents_ExecuteEventHandler(TrsPasteToTopicUnFormatted);
                    _tr.Start();
                }
            }
        }

        /// <summary>
        /// Transaction. Paste formatted text (rtf) to the selected topics.
        /// </summary>
        void TrsPasteToTopicFormatted(Document pDocument)
        {
            string rtf = Clipboard.GetText(TextDataFormat.Rtf);

            foreach (Topic t in SelectedTopics)
            {
                if (replace) // replace topic text with unformatted text from Clipboard
                    t.Title.TextRTF = rtf;
                else
                    t.Title.InsertTextRTF(t.Text.Length + 1, rtf);

                // Add links
                if (StickUtils.SourceURL != "")
                    t.Hyperlinks.AddHyperlink(StickUtils.SourceURL);
                foreach (string link in StickUtils.Links)
                    t.Hyperlinks.AddHyperlink(link);
            }
        }

        /// <summary>
        /// Transaction. Paste UNformatted text to the selected topics.
        /// </summary>
        void TrsPasteToTopicUnFormatted(Document pDocument)
        {
            string text = Clipboard.GetText(TextDataFormat.UnicodeText);

            foreach (Topic t in SelectedTopics)
            {
                if (replace) // replace topic text with unformatted text from Clipboard
                    t.Text = text;
                else // append unformatted text from Clipboard to
                     // (possibly formatted, we can't check it!) topic text
                {
                    // Get topic font family and font size
                    string name = t.Font.Name;
                    float size = t.Font.Size;

                    // Set unformatted text from clipboard to rtb
                    rtb.Clear(); rtb.Text = " " + text;
                    // Apply topic font to this text
                    rtb.Font = new Font(name, size);
                    // Get rtf text (to append to the topic rtf text)
                    string ClipboardRtf = rtb.Rtf;

                    rtb.Clear();
                    rtb.Rtf = t.Title.TextRTF; // copy topic rtf to rtb
                    //move cursor to the end
                    rtb.Select(rtb.TextLength, 0);
                    //append the rtf from clipboard
                    rtb.SelectedRtf = ClipboardRtf;
                    t.Title.TextRTF = rtb.Rtf;
                }

                // Add links
                if (StickUtils.SourceURL != "")
                    t.Hyperlinks.AddHyperlink(StickUtils.SourceURL);
                foreach (string link in StickUtils.Links)
                    t.Hyperlinks.AddHyperlink(link);
            }
        }

        

        public void PasteOperations_Tick(object sender, EventArgs e)
        {
            PasteOperations.Stop();
            pastetext = false;

            // Merge text from pasted topics and save links (if any)
            if (pasteOperation == "pastetotopic" || // if <Paste Text To Topic> command 
                OptionMultipleTopics.Tag.ToString() == "single") // or <Paste as Topic> command with "paste to single topic" option
            {
                rtb.Clear();

                // Pass pasted topics' text to the richtextbox and save links
                foreach (Topic t in PastedTopics)
                {
                    //move cursor to the end
                    rtb.Select(rtb.TextLength, 0);
                    //append the topic rtf
                    rtb.SelectedRtf = t.Title.TextRTF;
                }

                // Delete pasted topics
                foreach (Topic t in PastedTopics.Reverse<Topic>())
                    t.Delete();
            }

            // Paste resulting (above) text to the selected topics
            if (pasteOperation == "pastetotopic" || // if <Paste Text To Topic> command 
                OptionMultipleTopics.Tag.ToString() == "single") // or <Paste as Topic> command with "paste to single topic" option
            {
                foreach (Topic t in SelectedTopics)
                {
                    if (pasteOperation == "pasteastopic")
                    {
                        if (OptionMultipleTopics.Tag.ToString() == "single")
                        {
                            
                        }
                    }
                    else if (replace)
                    {
                        t.Title.TextRTF = rtb.Rtf;
                    }
                    else // add text to end of topic text
                    {
                        string rtf = rtb.Rtf; rtb.Clear();

                        rtb.Rtf = t.Title.TextRTF; // copy topic rtf to rtb
                                                   //move cursor to the end
                        rtb.Select(rtb.TextLength, 0);
                        //append the rtf from aux topics
                        rtb.SelectedRtf = rtf;
                        t.Title.TextRTF = rtb.Rtf;
                    }

                    // Set links
                    if (StickUtils.SourceURL != "")
                        t.Hyperlinks.AddHyperlink(StickUtils.SourceURL);

                    foreach (string link in StickUtils.Links)
                        t.Hyperlinks.AddHyperlink(link);
                }
            }

            PastedTopics.Clear(); SelectedTopics.Clear();
            DocumentStorage.Sync(MMUtils.ActiveDocument, false);
        }
        public static List<Topic> PastedTopics = new List<Topic>();
        public static List<Topic> SelectedTopics = new List<Topic>();
        List<string> TopicLinks = new List<string>();
        string pasteOperation = "";

        private void UnformatText_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Font.SetAutomatic(63);
                    t.TextColor.SetAutomatic();
                }
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsCommon.Items)
                item.Visible = true;

            cmsCommon.Show(Cursor.Position);
        }

        private void PasteTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null || 
                MMUtils.ActiveDocument.Selection.PrimaryTopic == null) return;

            if (e.Button == MouseButtons.Left)
            {
                SelectedTopics.Clear(); TopicsToAdd.Clear();
                SelectedTopics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>());

                bool formatted = OptionTextFormat.Tag.ToString() == "formatted";
                bool single = OptionMultipleTopics.Tag.ToString() == "single";

                StickUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes", 
                    OptionInternalLinks.Tag.ToString() == "yes");

                transTopicType = "subtopic";

                if (single)
                {
                    if (formatted)
                    {

                    }
                    else // paste as single topic with unformatted text from clipboard
                    {
                        TopicsToAdd.Add(Clipboard.GetText(TextDataFormat.UnicodeText));
                        AddTopicTransaction(Utils.getString("addtopics.transactionname.insert"));
                    }
                }
                else
                {
                    if (formatted)
                    {

                    }
                    else // paste as single topic with unformatted text from clipboard
                    {
                        if (StickUtils.Links.Count == 0)
                        {
                            string text = Clipboard.GetText(TextDataFormat.UnicodeText);
                            TopicsToAdd = text.Split(new[] { "\r\n", "\r", "\n" }, 
                                StringSplitOptions.RemoveEmptyEntries).ToList();
                            AddTopicTransaction(Utils.getString("addtopics.transactionname.insert"));
                        }
                        else
                        {
                            TopicsToAdd.Add(Clipboard.GetText(TextDataFormat.UnicodeText));
                            AddTopicTransaction(Utils.getString("addtopics.transactionname.insert"));
                        }
                    }
                    return;
                    DocumentStorage.Sync(MMUtils.ActiveDocument); // subscribe document to onObjectAdded event
                    pastetext = true; // for onObjectAdded event
                    PastedTopics.Clear();

                    StickUtils.ActivateMindManager();
                    SelectedTopics[0].SelectOnly(); // select the first of selected topics
                    PasteOperations.Start(); // start timer to process pasted topics
                                             // paste text from clipboard (in MM23 Selection.Paste() doesn't work!)
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);

                    //PasteTopic((sender as PictureBox).Name);
                    AddTopicTransaction(Utils.getString("addtopics.transactionname.insert"));
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "paste");
            }
        }
        /// <summary>
        /// Topics we have to add  
        /// </summary>
        public List<string> TopicsToAdd = new List<string>();

        void AddTopicTransaction(string trname)
        {
            Transaction _tr = MMUtils.ActiveDocument.NewTransaction(trname);
            _tr.IsUndoable = true;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(PasteTopics);
            _tr.Start();
        }

        public void PasteTopics(Document pDocument)
        {
            if (TopicList.Count > 1 && transTopicType == "nexttopic")
                TopicList = TopicList.Reverse<string>().ToList();

            if (transTopicType == "ParentTopic") // selected topics will be subtopics of the future parent topic
            {
                StickUtils.AddTopic(MMUtils.ActiveDocument.Selection.PrimaryTopic, transTopicType, TopicsToAdd[0]);
            }
            else
            {
                
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    bool firsttopic = true; // Source Link is added to the first topic only!
                    foreach (var name in TopicsToAdd)
                    {
                        StickUtils.AddTopic(t, transTopicType, name, false, firsttopic);
                        if (firsttopic) firsttopic = false;
                    }
                }
            }
        }

        private void OptionButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender == OptionTextFormat)
                {
                    if (OptionTextFormat.Tag.ToString() == "formatted")
                    {
                        OptionTextFormat.Tag = "unformatted";
                        OptionTextFormat.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "unformattedText.png");
                        toolTip1.SetToolTip(OptionTextFormat, Utils.getString("BubblesPaste.workwith.unformatted"));
                    }
                    else
                    {
                        OptionTextFormat.Tag = "formatted";
                        OptionTextFormat.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "formattedText.png");
                        toolTip1.SetToolTip(OptionTextFormat, Utils.getString("BubblesPaste.workwith.formatted"));
                    }
                }
                else if (sender == OptionReplaceInsert)
                {
                    if (OptionReplaceInsert.Tag.ToString() == "replace")
                    {
                        OptionReplaceInsert.Tag = "insert";
                        OptionReplaceInsert.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "inserttext.png");
                        toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("paste.contextmenu.insert1"));

                        if (OP_myrisk.Checked)
                        {
                            PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotesRisk.png");
                            toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.AddNotes.unsafe.tooltip"));
                        }
                        else
                        {
                            PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotes.png");
                            toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.AddNotes.tooltip"));
                        }
                    }
                    else
                    {
                        OptionReplaceInsert.Tag = "replace";
                        OptionReplaceInsert.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "replacetext.png");
                        toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("paste.contextmenu.insert2"));
                        
                        PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotes.png");
                        toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.PasteNotes.tooltip"));
                    }
                }
                else if (sender == OptionMultipleTopics)
                {
                    if (OptionMultipleTopics.Tag.ToString() == "single")
                    {
                        OptionMultipleTopics.Tag = "multiple";
                        OptionMultipleTopics.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "cpTopicTemplate.png");
                        toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("paste.contextmenu.multipletopics1"));
                    }
                    else
                    {
                        OptionMultipleTopics.Tag = "single";
                        OptionMultipleTopics.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddSingle.png");
                        toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("paste.contextmenu.multipletopics2"));
                    }
                }
                else if (sender == OptionSourceLink)
                {
                    if (OptionSourceLink.Tag.ToString() == "no")
                    {
                        OptionSourceLink.Tag = "yes";
                        OptionSourceLink.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "sourcelink_active.png");
                        toolTip1.SetToolTip(OptionSourceLink, Utils.getString("BubblesPaste.sourcelink_yes"));
                    }
                    else
                    {
                        OptionSourceLink.Tag = "no";
                        OptionSourceLink.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "sourcelink.png");
                        toolTip1.SetToolTip(OptionSourceLink, Utils.getString("BubblesPaste.sourcelink_no"));
                    }
                }
                else if (sender == OptionInternalLinks)
                {
                    if (OptionInternalLinks.Tag.ToString() == "no")
                    {
                        OptionInternalLinks.Tag = "yes";
                        OptionInternalLinks.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "internallinks_active.png");
                        toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("BubblesPaste.internallinks_yes"));
                    }
                    else
                    {
                        OptionInternalLinks.Tag = "no";
                        OptionInternalLinks.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "internallinks.png");
                        toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("BubblesPaste.internallinks_no"));
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                //foreach (ToolStripItem item in cmsOptions.Items)
                //    item.Visible = true;

                //cmsOptions.Show(Cursor.Position);
            }
        }

        private void pReplace_Click(object sender, EventArgs e)
        {
            if (!BubblesButton.m_ReplaceDlg.Visible)
                BubblesButton.m_ReplaceDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        private void pTopicWidth_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Shape.TextWidth = Convert.ToSingle(pTopicWidth.Tag);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsOptions.Items)
                    item.Visible = true;

                OP_myrisk.Visible = false;

                cmsOptions.Show(Cursor.Position);
                return;
            }
        }

        string transTopicType;
        public List<string> TopicList = new List<string>();

        string orientation = "H";
        int RealLength;
        bool collapsed = false;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        InputSimulator sim = new InputSimulator();

        public static Timer PasteOperations = new Timer();
    }
}
