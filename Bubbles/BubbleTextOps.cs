using Mindjet.MindManager.Interop;
using PRAManager;
using PRMapCompanion;
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
using Color = System.Drawing.Color;
using Timer = System.Windows.Forms.Timer;

namespace Bubbles
{
    internal partial class BubbleTextOps : Form
    {
        public BubbleTextOps(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
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

            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typetextops);

            PopulateTopicWidth();

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typetextops);

            if (collapsed) {
                collapsed = false; Collapse(); }

            PasteOperations = new Timer() { Interval = 50 };
            PasteOperations.Tick += PasteOperations_Tick;

            // Apply scale factor
            this.Paint += this_Paint; // paint the border depending on scale factor
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            if (scale != 1)
                this.Scale(new SizeF(scale, scale)); // reset to 100%

            this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale
            scaleFactor = toScale;
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
                if (BubblesButton.topicWidthDlg.Visible)
                {
                    BubblesButton.topicWidthDlg.Hide();
                }
                else
                {
                    BubblesButton.topicWidthDlg.form = this;
                    BubblesButton.topicWidthDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
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
            else if (e.ClickedItem.Name == "MMAutoWidth")
            {
                if (StickUtils.TopicAutoWidth)
                    StickUtils.TopicAutoWidth = false;
                else
                    StickUtils.TopicAutoWidth = true;

                Utils.setRegistry("MMAutoWidth", StickUtils.TopicAutoWidth ? "1" : "0");
            }
            else if (e.ClickedItem.Name == "MMAutoWidthOut")
            {
                
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

            StickUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes",
                OptionInternalLinks.Tag.ToString() == "yes");

            if (OptionTextFormat.Tag.ToString() == "formatted" && String.IsNullOrEmpty(rtf))
            {
                // we have to make the rtf through the copying Clipboard to the topic
                pastetext = true;
                pasteOperation = "topicnotes";
                PastedTopics.Clear();

                StickUtils.ActivateMindManager();
                SelectedTopics[0].SelectOnly(); // get first of the selected topics
                PasteOperations.Start(); // start timer to process formatted text
                                         // paste from clipboard (in MM23 Selection.Paste() doesn't work!)
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                return;
            }

            ProcessTopicNotes(rtf);
        }

        void ProcessTopicNotes(string rtf)
        {
            Topic topictoselect = null;

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

                    AddLinksToTopicNotes(t);
                    t.Notes.Commit();
                    topictoselect = t;
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

                    AddLinksToTopicNotes(t);
                    t.Notes.Commit();
                    topictoselect = t;
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

            if (topictoselect != null)
            {
                topictoselect.SelectOnly();
                topictoselect.SnapIntoView();
                topictoselect = null;
            }
        }

        void AddLinksToTopicNotes(Topic t)
        {
            t.Notes.CursorPosition = -1;

            if (StickUtils.SourceURL != "")
                t.Notes.InsertTextHyperlink(StickUtils.SourceURL,  Utils.getString("BubblesPaste.AddNotes.Source"));

            if (StickUtils.Links.Count > 0)
            {
                int i = 1;
                foreach (string link in StickUtils.Links)
                    t.Notes.InsertTextHyperlink(link, Utils.getString("BubblesPaste.AddNotes.Link") + " " + i++);
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
            if (StickUtils.TopicAutoWidth)
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

            if (StickUtils.TopicAutoWidth)
                StickUtils.SetTopicWidth();
        }

        

        public void PasteOperations_Tick(object sender, EventArgs e)
        {
            PasteOperations.Stop();

            if (pasteOperation == "pastetotopic" || pasteOperation == "topicnotes")
                PasteToTopic(MMUtils.ActiveDocument);
            else if (pasteOperation == "pasteastopic")
                PasteAsTopic();

            pastetext = false;

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

            if (pasteOperation == "topicnotes") // Paste to Notes
            {
                ProcessTopicNotes(rtb.Rtf); return;
            }

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
            }

            if (StickUtils.TopicAutoWidth)
                StickUtils.SetTopicWidth();

            PastedTopics.Clear(); SelectedTopics.Clear();
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
                foreach (Topic t in BubbleTextOps.PastedTopics)
                {
                    //move cursor to the end
                    rtb.Select(rtb.TextLength, 0);
                    //append the topic rtf
                    rtb.SelectedRtf = t.Title.TextRTF;
                }
            }

            if (onetopic) // Delete pasted topics
                foreach (Topic t in BubbleTextOps.PastedTopics.Reverse<Topic>())
                    t.Delete();

            // Paste resulting (above) text to the selected topics
            int p = 0, i = 0; // selected topics count
            foreach (Topic t in BubbleTextOps.SelectedTopics)
            {
                p++; i++;
                Topic frameTopic = null;
                {
                    if (onetopic) // Also, Callout and Parent topic
                    {
                        frameTopic = StickUtils.AddTopic(t, transTopicType, rtb.Rtf, true, true);
                        if (!StickUtils.TopicWidthList.Contains(frameTopic))
                            StickUtils.TopicWidthList.Add(frameTopic);
                    }
                    else // Multiple topics to paste. Subtopic, Next Topic or Topic before
                    {
                        foreach (Topic _t in BubbleTextOps.PastedTopics)
                        {
                            if (!StickUtils.TopicWidthList.Contains(_t))
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
                            if (transTopicType == "subtopic" && p == 1)
                            {
                                if (OptionTextFormat.Tag.ToString() == "unformatted")
                                    _t.Font.SetAutomatic(63);
                                if (!StickUtils.TopicWidthList.Contains(_t))
                                    StickUtils.TopicWidthList.Add(_t);
                            }
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

                                if (OptionTextFormat.Tag.ToString() == "formatted")
                                    frameTopic = StickUtils.AddTopic(t, transTopicType, _t.Title.TextRTF, true);
                                else
                                    frameTopic = StickUtils.AddTopic(t, transTopicType, _t.Text);

                                if (!StickUtils.TopicWidthList.Contains(frameTopic))
                                    StickUtils.TopicWidthList.Add(frameTopic);
                            }
                        }
                    }
                }
            }

            if (StickUtils.TopicAutoWidth)
                StickUtils.SetTopicWidth();

            PastedTopics.Clear(); SelectedTopics.Clear();
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
                StickUtils.ShowCommandPopup(this, orientation, StickUtils.typetextops, "paste");
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

                    // We have formatted multiline text with links. MindManager, help!
                    if (TopicsToAdd.Count > 1 && StickUtils.Links.Count > 1 && !singleforced)
                    {
                        pastetext = true; // for onObjectAdded event
                        pasteOperation = "pasteastopic";
                        PastedTopics.Clear();

                        StickUtils.ActivateMindManager();
                        SelectedTopics[0].SelectOnly(); // select the first of selected topics
                        PasteOperations.Start(); // start timer to process pasted topics
                                                 // paste text from clipboard (in MM23 Selection.Paste() doesn't work!)
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                    }
                    else
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
            if (StickUtils.TopicAutoWidth)
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

        public void PopulateTopicWidth()
        {
            cmsTopicWidths.Items.Clear();

            ToolStripItem tsi = null;
            tsi = cmsTopicWidths.Items.Add(Utils.getString("BubblesPaste.TopicWidth.Manage"));
            tsi.Name = "ManageTopicWidths";

            tsi = new ToolStripLabel(Utils.getString("BubblesPaste.TopicWidth.Label"));
            tsi.Font = new Font(tsi.Font, FontStyle.Bold);
            cmsTopicWidths.Items.Add(tsi);

            foreach (int width in StickUtils.ManualTopicWidths)
            {
                tsi = cmsTopicWidths.Items.Add(width.ToString());
                tsi.Name = "ManualWidth"; tsi.Tag = width.ToString();
            }

            tsi = new ToolStripLabel(Utils.getString("BubblesPaste.TopicWidth.Label2"));
            tsi.ToolTipText = Utils.getString("BubblesPaste.TopicWidth.tooltip");
            cmsTopicWidths.Items.Add(tsi);

            ToolStripTextBox mtb = new ToolStripTextBox();
            mtb.Width = Manage.Width * 2;
            mtb.BorderStyle = BorderStyle.FixedSingle;
            mtb.ToolTipText = Utils.getString("BubblesPaste.TopicWidth.tooltip");
            mtb.KeyDown += Mtb_KeyDown;
            cmsTopicWidths.Items.Add(mtb);

            cmsTopicWidths.Items.Add(new ToolStripSeparator());

            tsi = cmsTopicWidths.Items.Add(Utils.getString("BubblesPaste.TopicWidth.MMAutoWidth"));
            tsi.Name = "MMAutoWidth";
            tsi.ToolTipText = Utils.getString("BubblesPaste.MMAutoWidth.tooltip");
            (tsi as ToolStripMenuItem).CheckOnClick = true;
            (tsi as ToolStripMenuItem).Checked = StickUtils.TopicAutoWidth;
            cmsTopicWidths.Items.Add(tsi);

            //var dd = (tsi as ToolStripMenuItem).DropDown;
            //dd.ItemClicked += ContextMenu_ItemClicked;
            //tsi = dd.Items.Add(Utils.getString("BubblesPaste.MMAutoWidth.Out"));
            //tsi.Name = "MMAutoWidthOut";
        }

        private void Mtb_KeyDown(object sender, KeyEventArgs e)
        {
            ToolStripTextBox tb = sender as ToolStripTextBox;
            if (e.KeyCode == Keys.Enter)
            {
                int width = 0;
                try {
                    width = Convert.ToInt32(tb.Text.Trim());
                } catch { return; }

                if (width > 10)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.Shape.TextWidth = width;
                }

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void pTopicWidth_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null) return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Shape.TextWidth = StickUtils.MainTopicWidth;
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTopicWidths.Items)
                    item.Visible = true;

                cmsTopicWidths.Show(Cursor.Position);
                return;
            }
        }

        string transTopicType; bool transFormatted;

        string orientation = "H";
        int RealLength;
        bool collapsed = false;
        public float scaleFactor = 100;

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
