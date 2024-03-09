using Mindjet.MindManager.Interop;
using PRAManager;
using PRMapCompanion;
using Sticks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Clipboard = System.Windows.Forms.Clipboard;
using Timer = System.Windows.Forms.Timer;

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
            toolTip1.SetToolTip(pTopicWidth, Utils.getString("BubblesPaste.pTopicWidth.tooltip"));

            toolTip1.SetToolTip(OptionTextFormat, Utils.getString("BubblesPaste.workwith.unformatted"));
            toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("paste.contextmenu.insert2"));
            toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("paste.contextmenu.multipletopics2"));
            toolTip1.SetToolTip(OptionSourceLink, Utils.getString("BubblesPaste.sourcelink_no"));
            toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("BubblesPaste.internallinks_no"));

            toolTip1.SetToolTip(pictureHandle, stickname);

            cmsOptions.ItemClicked += ContextMenu_ItemClicked;
            cmsTopicWidths.ItemClicked += ContextMenu_ItemClicked;
            cmsCommon.ItemClicked += ContextMenu_ItemClicked;

            OP_myrisk.Text = Utils.getString("pastenotes.contextmenu.quickinsert");
            OP_myrisk.ToolTipText = Utils.getString("pastenotes.contextmenu.quickinsert.tooltip");
            OP_myrisk.Tag = "myrisk";

            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typepaste);
            //cmsOptions.Closing += CmsOptions_Closing;

            PopulateTopicWidth();

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
            else if (e.ClickedItem.Name == "ManageTopicWidths")
            {
                using (TopicWidthDlg dlg = new TopicWidthDlg())
                {
                    if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;

                    PopulateTopicWidth();
                }
            }
            else if (e.ClickedItem.Name == "ManualWidth")
            {
                if (MMUtils.ActiveDocument == null) return;

                int width = Convert.ToInt32(e.ClickedItem.Tag);
                if (width > 10)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.Shape.TextWidth = width;
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
                collapseOrientation = orientation;
                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;
                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                {
                    this.Location = collapseState; // restore collapsed location
                    if (orientation != collapseOrientation) Rotate();
                }
            }
        }
        Point collapseState = new Point(0, 0);
        string collapseOrientation = "N";

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
                    item.Visible = true;

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
        static bool replace = false;
        /// <summary>
        /// Paste text from clipboard to the selected topics
        /// </summary>
        public void pPasteToTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StickUtils.TopicWidthList.Clear();

                // Get links from copied text
                StickUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes", 
                    OptionInternalLinks.Tag.ToString() == "yes");

                if (MMUtils.ActiveDocument == null || !Clipboard.ContainsText() ||
                    MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
                    return;

                replace = OptionReplaceInsert.Tag.ToString() == "replace";

                SelectedTopics.Clear();
                SelectedTopics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>());

                bool singleforced = !Clipboard.ContainsData(System.Windows.DataFormats.Html) &&
                    !Clipboard.ContainsData(System.Windows.DataFormats.Rtf);

                if (OptionTextFormat.Tag.ToString() == "formatted" && !singleforced)
                {
                    transFormatted = true;
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
                    transFormatted = false;

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
                if (replace || pastetopic) // replace topic text with formatted text from Clipboard
                    t.Title.TextRTF = rtf;
                else
                    t.Title.InsertTextRTF(t.Text.Length + 1, rtf);

                // Add links
                if (StickUtils.SourceURL != "")
                    t.Hyperlinks.AddHyperlink(StickUtils.SourceURL);
                foreach (string link in StickUtils.Links)
                    t.Hyperlinks.AddHyperlink(link);

                StickUtils.TopicWidthList.Add(t);
            }
            pastetopic = false;
            StickUtils.SetTopicWidth();
        }
        bool pastetopic = false;

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

                StickUtils.TopicWidthList.Add(t);
            }
            StickUtils.SetTopicWidth();
        }

        

        public void PasteOperations_Tick(object sender, EventArgs e)
        {
            PasteOperations.Stop();
            pastetext = false;

            if (pasteOperation == "pastetotopic")
                PasteToTopic(MMUtils.ActiveDocument);
            else if (pasteOperation == "pasteastopic")
                PasteAsTopic();

            //Transaction _tr = MMUtils.ActiveDocument.NewTransaction("Paste to Topic");
            //_tr.IsUndoable = true;
            //_tr.Execute += new ITransactionEvents_ExecuteEventHandler(PasteOperationsTick);
            //_tr.Start();
        }
        public static List<Topic> PastedTopics = new List<Topic>();
        public static List<Topic> SelectedTopics = new List<Topic>();
        List<string> TopicLinks = new List<string>();
        static string pasteOperation = "";

        public void PasteToTopic(Document pDocument)
        {
            rtb.Clear();
            StickUtils.TopicWidthList.Clear();

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

            // Paste resulting (above) text to the selected topics
            foreach (Topic t in SelectedTopics)
            {
                if (pasteOperation == "pastetotopic") // Paste To Topic
                {
                    if (replace)
                        t.Title.TextRTF = rtb.Rtf;
                    else // add text to the end of topic text
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

                    StickUtils.TopicWidthList.Add(t);
                }
                else // Paste to Notes
                {
                    
                }
            }
            StickUtils.SetTopicWidth();

            PastedTopics.Clear(); SelectedTopics.Clear();
            DocumentStorage.Sync(MMUtils.ActiveDocument, false);
        }

        public void PasteAsTopic()
        {
            // or <Paste as Callout> or <Paste as Parent>
            bool onetopic = OptionMultipleTopics.Tag.ToString() == "single" || 
                transTopicType == "Callout" || transTopicType == "ParentTopic";

            rtb.Clear();
            StickUtils.TopicWidthList.Clear();

            if (onetopic) // Merge text from pasted topics
            {
                foreach (Topic t in BubblePaste.PastedTopics)
                {
                    //move cursor to the end
                    rtb.Select(rtb.TextLength, 0);
                    //append the topic rtf
                    rtb.SelectedRtf = t.Title.TextRTF;
                }
            }

            if (onetopic) // Delete pasted topics
                foreach (Topic t in BubblePaste.PastedTopics.Reverse<Topic>())
                    t.Delete();

            // Paste resulting (above) text to the selected topics
            int p = 0, i = 0; // selected topics count
            foreach (Topic t in BubblePaste.SelectedTopics)
            {
                p++; i++;
                Topic frameTopic = null;
                {
                    if (onetopic) // Also, Callout and Parent topic
                    {
                        frameTopic = StickUtils.AddTopic(t, transTopicType, rtb.Rtf, true, true);
                        StickUtils.TopicWidthList.Add(frameTopic);
                    }
                    else // Multiple topics to paste. Subtopic, Next Topic or Topic before
                    {
                        foreach (Topic _t in BubblePaste.PastedTopics)
                        {
                            StickUtils.TopicWidthList.Add(_t);

                            // Add the Source URL to the FIRST topic
                            // Other topics have links already
                            if (i++ == 1 && StickUtils.SourceURL != "")
                            {
                                _t.Hyperlinks.AddHyperlink(StickUtils.SourceURL);
                                if (_t.Hyperlinks.Count > 1)
                                    _t.Hyperlinks.MoveToTop(_t.Hyperlinks.Count);
                            }

                            // FIRST selected topic. <subtopic> is already in the place.
                            // Move <next topic> or <topic before> to the appropiate place. 
                            if (transTopicType != "subtopic" && p == 1)
                            {
                                // Get given topic index in the branch
                                int k = 1;
                                foreach (Topic __t in t.ParentTopic.AllSubTopics)
                                {
                                    if (__t == t) break; k++;
                                }
                                if (transTopicType == "nexttopic") k++; // Otherwise, topic is added before

                                t.ParentTopic.AllSubTopics.Insert(_t, k);
                            }

                            if (p > 1) // Other selected topics
                            {
                                StickUtils.Links.Clear();
                                foreach (Hyperlink link in _t.Hyperlinks)
                                    StickUtils.Links.Add(link.Address);

                                frameTopic = StickUtils.AddTopic(t, transTopicType, _t.Title.TextRTF, true);
                                StickUtils.TopicWidthList.Add(frameTopic);
                            }
                        }
                    }
                }
            }
            StickUtils.SetTopicWidth();

            BubblePaste.PastedTopics.Clear(); BubblePaste.SelectedTopics.Clear();
            DocumentStorage.Sync(MMUtils.ActiveDocument, false);
        }

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

        public void PasteTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null || 
                MMUtils.ActiveDocument.Selection.PrimaryTopic == null) return;

            if (e.Button == MouseButtons.Left)
            {
                PasteTopic("subtopic");
            }
            else if (e.Button == MouseButtons.Right)
            {
                StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "paste");
            }
        }

        public void PasteTopic(string topicType)
        {
            transTopicType = topicType;
            SelectedTopics.Clear(); TopicsToAdd.Clear();
            SelectedTopics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>());

            bool formatted = OptionTextFormat.Tag.ToString() == "formatted";
            bool single = OptionMultipleTopics.Tag.ToString() == "single";
            bool singleforced = !Clipboard.ContainsData(System.Windows.DataFormats.Html) &&
                !Clipboard.ContainsData(System.Windows.DataFormats.Rtf);

            StickUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes",
                OptionInternalLinks.Tag.ToString() == "yes");

            if (formatted && !singleforced)
            {
                transFormatted = true;
                if (single && Clipboard.ContainsData(System.Windows.DataFormats.Rtf)) // we have the rtf text already
                {
                    TopicsToAdd.Add(Clipboard.GetText(TextDataFormat.Rtf));
                    TrsPasteTopic(Utils.getString("addtopics.transactionname.insert"));
                }
                else // paste to format text
                {
                    DocumentStorage.Sync(MMUtils.ActiveDocument); // subscribe document to onObjectAdded event
                    pastetext = true; // for onObjectAdded event
                    pasteOperation = "pasteastopic";
                    PastedTopics.Clear();

                    StickUtils.ActivateMindManager();
                    SelectedTopics[0].SelectOnly(); // select the first of selected topics
                    PasteOperations.Start(); // start timer to process pasted topics
                                             // paste text from clipboard (in MM23 Selection.Paste() doesn't work!)
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                }
            }
            else // unformatted text
            {
                transFormatted = false;

                if (single || topicType == "callout" || topicType == "parenttopic") // paste as single topic with unformatted text from clipboard
                {
                    TopicsToAdd.Add(Clipboard.GetText(TextDataFormat.UnicodeText));
                    TrsPasteTopic(Utils.getString("addtopics.transactionname.insert"));
                }
                else // paste as multiple topic with unformatted text from clipboard
                {
                    string text = Clipboard.GetText(TextDataFormat.UnicodeText);
                    TopicsToAdd = text.Split(new[] { "\r\n", "\r", "\n" },
                        StringSplitOptions.RemoveEmptyEntries).ToList();
                    TrsPasteTopic(Utils.getString("addtopics.transactionname.insert"));
                }
            }
        }

        /// <summary>
        /// Topics we have to add  
        /// </summary>
        public List<string> TopicsToAdd = new List<string>();

        void TrsPasteTopic(string trname)
        {
            Transaction _tr = MMUtils.ActiveDocument.NewTransaction(trname);
            _tr.IsUndoable = true;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(PasteTopics);
            _tr.Start();
        }

        public void PasteTopics(Document pDocument)
        {
            if (TopicsToAdd.Count > 1 && transTopicType == "nexttopic")
                TopicsToAdd = TopicsToAdd.Reverse<string>().ToList();

            bool rtf = transFormatted;

            if (transTopicType == "parenttopic") // selected topics will be subtopics of the future parent topic
            {
                StickUtils.AddTopic(MMUtils.ActiveDocument.Selection.PrimaryTopic, transTopicType, TopicsToAdd[0], rtf, true);
            }
            else
            {
                StickUtils.TopicWidthList.Clear();
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    bool firsttopic = true; // Source Link is added to the first topic only!
                    foreach (var name in TopicsToAdd)
                    {
                        StickUtils.AddTopic(t, transTopicType, name, rtf, firsttopic);
                        if (firsttopic) firsttopic = false;
                    }
                }
            }
            StickUtils.SetTopicWidth();
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

        private void PopulateTopicWidth()
        {
            cmsTopicWidths.Items.Clear();
            string widths = Utils.getRegistry("TopicWidths", "63:100:130:160;300-200:200-160:150-120;1");
            string[] Widths = widths.Split(';');

            ToolStripItem tsi = null;
            tsi = cmsTopicWidths.Items.Add(Utils.getString("BubblesPaste.TopicWidth.Manage"));
            tsi.Name = "ManageTopicWidths";

            tsi = new ToolStripLabel(Utils.getString("BubblesPaste.TopicWidth.Label"));
            tsi.Font = new Font(tsi.Font, FontStyle.Bold);
            cmsTopicWidths.Items.Add(tsi);

            string[] ManualWidths = Widths[0].Split(':');
            pTopicWidth.Tag = ManualWidths[0];

            tsi = cmsTopicWidths.Items.Add(ManualWidths[1]);
            tsi.Name = "ManualWidth"; tsi.Tag = ManualWidths[1];

            tsi = cmsTopicWidths.Items.Add(ManualWidths[2]);
            tsi.Name = "ManualWidth"; tsi.Tag = ManualWidths[2];

            tsi = cmsTopicWidths.Items.Add(ManualWidths[3]);
            tsi.Name = "ManualWidth"; tsi.Tag = ManualWidths[3];

            string[] automatic = Widths[1].Split(':');
            string[] auto1 = automatic[0].Split('-');
            TextChars1 = Convert.ToInt32(auto1[0]);
            AutoTopicWidth1 = Convert.ToInt32(auto1[1]);
            string[] auto2 = automatic[1].Split('-');
            TextChars2 = Convert.ToInt32(auto2[0]);
            AutoTopicWidth2 = Convert.ToInt32(auto2[1]);
            string[] auto3 = automatic[2].Split('-');
            TextChars3 = Convert.ToInt32(auto3[0]);
            AutoTopicWidth3 = Convert.ToInt32(auto3[1]);

            MMPasteWidth = Widths[2] == "1";
        }

        private void pTopicWidth_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Shape.TextWidth = Convert.ToInt32(pTopicWidth.Tag);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTopicWidths.Items)
                    item.Visible = true;

                cmsTopicWidths.Show(Cursor.Position);
                return;
            }
        }

        //int TopicWidthMain, TopicWidth1, TopicWidth2, TopicWidth3;
        public static int TextChars1, TextChars2, TextChars3, AutoTopicWidth1, AutoTopicWidth2, AutoTopicWidth3;
        public static bool MMPasteWidth;

        string transTopicType; bool transFormatted;

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
