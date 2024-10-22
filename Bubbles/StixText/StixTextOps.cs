using HtmlAgilityPack;
using Mindjet.MindManager.Interop;
using mshtml;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Clipboard = System.Windows.Forms.Clipboard;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Timer = System.Windows.Forms.Timer;

namespace Bubbles
{
    internal partial class StixTextOps : Form
    {
        public StixTextOps(int ID, string _orientation, string stickname = "")
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation; // "H" or "V"

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TextOpsStix.htm");

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            toolTip1.SetToolTip(subtopic, Utils.getString("TextOpsStix.pastesubtopic"));
            toolTip1.SetToolTip(pPasteToTopic, Utils.getString("TextOpsStix.pPaste.tooltip"));
            toolTip1.SetToolTip(pCopyTopicText, Utils.getString("TextOpsStix.pCopy.tooltip"));
            toolTip1.SetToolTip(PasteLink, Utils.getString("TextOpsStix.PasteLink.tooltip"));
            toolTip1.SetToolTip(PasteNotes, Utils.getString("TextOpsStix.PasteNotes.tooltip"));
            toolTip1.SetToolTip(UnformatText, Utils.getString("TextOpsStix.unformate.tooltip"));
            toolTip1.SetToolTip(pReplace, Utils.getString("TextOpsStix.pReplace.tooltip"));
            toolTip1.SetToolTip(pTopicWidth, 
                String.Format(Utils.getString("TextOpsStix.pTopicWidth.tooltip"), TopicWidthsDlg.mainwidth));

            toolTip1.SetToolTip(OptionTextFormat, Utils.getString("TextOpsStix.workwith.unformatted"));
            toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("textops.contextmenu.insert2"));
            toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("textops.contextmenu.multipletopics2"));
            toolTip1.SetToolTip(OptionSourceLink, Utils.getString("TextOpsStix.sourcelink_no"));
            toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("TextOpsStix.internallinks_no"));

            myToolTip1.SetToolTip(pictureHandle, stickname +
                Utils.getString("StixTextOps.description") + Utils.getString("HeadIcon.tooltip.tips"));
            toolTip1.SetToolTip(Manage, Utils.getString("ManageIcon.tooltip"));

            cmsTopicWidths.ItemClicked += ContextMenu_ItemClicked;
            cmsCommon.ItemClicked += ContextMenu_ItemClicked;

            StixUtils.SetCommonContextMenu(cmsCommon, StixUtils.typetextops);

            PopulateTopicWidths();

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();

            PasteOperations = new Timer() { Interval = 50 };
            PasteOperations.Tick += PasteOperations_Tick;

            // Apply scale factor
            this.Paint += (o, e) => StixUtils.PaintStix(this, scaleFactor, e);
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);

            editor = new HtmlEditor(WB, "<p>Some text</p>");
        }
        HtmlEditor editor;

        public void ScaleStick(float fromScale, float toScale)
        {
            scaleFactor = toScale;
            if (StixUtils.ScaleStick(this, fromScale, toScale)) return;
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
            if (e.ClickedItem.Name == "ManageTopicWidths")
            {
                using (TopicWidthsDlg dlg = new TopicWidthsDlg(this))
                    dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            else if (e.ClickedItem.Name == "ManualWidth")
            {
                if (Utils.ActiveDocumentOrSelectionNull()) return;

                int width = Convert.ToInt32(e.ClickedItem.Tag);
                if (width > 10)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.Shape.TextWidth = width;
                }
            }
            else if (e.ClickedItem.Name == "TopicAutoWidth")
            {
                if (StixUtils.TopicAutoWidth)
                    StixUtils.TopicAutoWidth = false;
                else
                    StixUtils.TopicAutoWidth = true;

                Utils.setRegistry("TopicAutoWidth", StixUtils.TopicAutoWidth ? "1" : "0");
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixMain.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "TextOpsStix.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typetextops, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation);

            panelOptions.Size = new Size(panelOptions.Height, panelOptions.Width);
            panelOptions.Location = new Point(panelOptions.Location.Y, panelOptions.Location.X);
        }

        private void PasteLink_Click(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            if (Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Hyperlinks.AddHyperlink(Clipboard.GetText());
            }
        }

        private void PasteNotes_MouseClick(object sender, MouseEventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            SelectedTopics.Clear();
            SelectedTopics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>());

            replace = OptionReplaceInsert.Tag.ToString() == "replace";

            // If there are affected by MM23 API bug topic notes, update them
            if (!replace)
                StixMain.UpdateTopicNotes(MMUtils.ActiveDocument);

            string rtf = Clipboard.GetText(TextDataFormat.Rtf);
            string html = Clipboard.GetText(TextDataFormat.Html).Replace("Â", "");
            string plain = Clipboard.GetText(TextDataFormat.UnicodeText);

            ProcessTopicNotes(plain, html, rtf);
        }

        public string AdjustPixels(string html)
        {
            //IEnumerable<int> result = AllIndexesOf(html, "px");
            int idx = 0;
            //foreach (int i in result)
            do
            {
                idx = html.IndexOf("px", idx);

                if (idx == -1) break;

                if (html.Substring(idx + 2, 1) != " " && html.Substring(idx + 2, 1) != ";")
                {
                    idx += 2;
                    continue; // it's not a pixels
                }

                int k = idx; bool found = false; float px;
                do
                {
                    string number = html.Substring(k-- - 1, 1);
                    if (number == " " || number == ":")
                    {
                        found = true; k++; break;
                    }
                }
                while (k > 0);

                if (found)
                {
                    if (float.TryParse(html.Substring(k, idx - k), out px) && px != 0) // it's a number
                    {
                        html = html.Substring(0, k) + (px * Utils.scalingFactor).ToString() + html.Substring(idx);
                    }
                }

                idx += 2;
            }
            while (idx < html.Length);

            return html;
        }

        public static IEnumerable<int> AllIndexesOf(string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }
        }

        void ProcessTopicNotes(string plain, string html, string rtf)
        {
            Topic topictoselect = null;
            bool affected = false;
            //bool sourceproccessed = true, linksproccessed = true;
            bool aPlain = false, aHtml = false, aWord = false, aPdf = false;
            bool isPlain = false, failed = false;
            falsealarm = true; 

            foreach (Topic t in SelectedTopics)
            {
                if (OptionTextFormat.Tag.ToString() == "formatted")
                {
                    if (html != "" && rtf == "") // from web
                    {
                        html = ParseClipboardHtml(false, ref isPlain);
                        aHtml = true;
                    }
                    else if (rtf != "") // from word or pdf
                    {
                        //t.Notes.TextRTF = rtf;

                        if (html != "") // from word
                            aWord = true;
                        else // from PDF
                        {
                            aPdf = true;
                            //sourceproccessed = false; linksproccessed = false;
                        }
                    }
                    else
                        aPlain = true;                    
                }
                else // replace topic notes with plain text
                {
                    // If preserve internal links
                    if (OptionInternalLinks.Tag.ToString() == "yes" || OptionSourceLink.Tag.ToString() == "yes")
                    {
                        if (html != "") // web or word
                        {
                            html = ParseClipboardHtml(true, ref isPlain);
                            if (rtf == "" || !isPlain) aHtml = true;
                            if (isPlain) aPlain = true;
                        }
                        else // plain or PDF
                        {
                            if (rtf == "") aPlain = true;
                            else aPdf = true;
                            //sourceproccessed = false; linksproccessed = false;
                        }
                    }
                    else // no links
                        aPlain = true;
                }

                if (replace || t.Notes.IsEmpty) // replace topic notes
                {
                    // From web. Source and links already proccessed
                    if (aHtml)
                    {
                        string mm = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""          ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""><html  xmlns=""http://www.w3.org/1999/xhtml"">";
                        string result = mm + html + "</html>";
                        try
                        {
                            t.Notes.TextXHTML = result;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(Utils.getString("TextOpsStix.pastenotes.failed"));
                            failed = true;
                        }
                    }
                    // From Word.
                    else if (aWord)
                    {
                        t.Notes.TextRTF = rtf;

                        // Proccess source link
                        if (OptionSourceLink.Tag.ToString() == "yes")
                            AppendToTopicNotes(t);
                    }
                    else if (aPdf)
                    {
                        t.Notes.TextRTF = rtf;
                    }
                    else if (aPlain)
                    {
                        t.Notes.Text = plain;
                    }
                }
                else // append to topic notes
                {
                    // From web. Source and links already proccessed
                    if (aHtml)
                    {
                        AppendToTopicNotes(t, html, false);
                    }
                    else if (aWord)
                    {
                        string _rtf = t.Notes.TextRTF;
#if !MINDJET20
                        falsealarm = false;
                        t.Notes.AppendRtf(rtf);
                        affected = true;
#endif
                        // Proccess source link
                        if (OptionSourceLink.Tag.ToString() == "yes")
                            AppendToTopicNotes(t);
                    }
                    else if (aPdf)
                    {
                        falsealarm = false;
                        t.Notes.AppendRtf(rtf);
                        affected = true;
                    }
                    else if (aPlain)
                    {
                        string notes = "";
                        if (t.Notes.IsPlainTextOnly)
                        {
                            notes = t.Notes.Text;
                            t.Notes.Text = notes + "\r\n" + plain;
                        }
                        else
                        {
                            notes = t.Notes.TextXHTML.Replace("</html>", "");
                            plain = "<p>" + plain.Replace("\r\n", "</p><p>") + "</p>";
                            t.Notes.TextXHTML = notes + plain + "</html>";
                        }
                    }
                }

                //if (!linksproccessed || !sourceproccessed) // rtf or plain
                //{
                //    StixUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes", OptionInternalLinks.Tag.ToString() == "yes");
                //    affected = AddLinksToTopicNotes(t, !linksproccessed);
                //}

                t.Notes.Commit();
                topictoselect = t;

                StixMain.MarkOrAddTopicToBugList(t, affected);
                if (failed) break;
            } // end foreach topic

            if (topictoselect != null)
            {
                topictoselect.SelectOnly();
                topictoselect.SnapIntoView();
                topictoselect = null;
            }
        }
        public static bool falsealarm = false;

        void AppendToTopicNotes(Topic t, string _html = "", bool source = true)
        {
            string html = t.Notes.TextXHTML.Replace("</html>", "");

            if (_html != "") html += _html;

            if (OptionSourceLink.Tag.ToString() == "yes" && source)
            {
                StixUtils.GetSourceURL(Clipboard.GetText(TextDataFormat.Html));
                if (StixUtils.SourceURL != "")
                {
                    html += "<p><a href=\"" + StixUtils.SourceURL + "\"><font color=\"red\">" +
                        Utils.getString("TextOpsStix.AddNotes.Source") + "</font></a></p>";
                }
            }
            try
            {
                t.Notes.TextXHTML = html + "</html>";
            }
            catch
            {
                MessageBox.Show(Utils.getString("TextOpsStix.pastenotes.failed"));
            }

            falsealarm = true;
        }

        /// <summary>
        /// Parse/clean clipboard html. Option - convert to plain and add links. 
        /// </summary>
        /// <param name="plainwithlinks"></param>
        /// <returns>Clean html or plain html with links</returns>
        string ParseClipboardHtml(bool plainwithlinks, ref bool isPlain)
        {
            // Clean Stix browser
            editor.SelectAll();
            editor.Delete();

            // Paste html from clipboard to Stix browser
            IHTMLTxtRange rng = editor.doc.selection.createRange();
            rng.execCommand("Paste", false, null);
            // Get clean html
            string html = WB.Document.Body.InnerHtml;

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            try
            {
                var form_nodes = htmlDoc.DocumentNode.SelectNodes("//*[name()='form']")?.ToList();
                if (form_nodes != null)
                {
                    foreach (var form_node in form_nodes)
                        form_node.Remove();
                }
            } 
            catch { }

            var img_nodes = htmlDoc.DocumentNode.SelectNodes("//*[name()='img']")?.ToList();
            if (img_nodes != null)
            {
                foreach (var img_node in img_nodes)
                {
                    foreach (var attr in img_node.Attributes.Reverse())
                    {
                        if (attr.Name == "srcset")
                            attr.Remove();
                    }
                }
            }

            var a_nodes = htmlDoc.DocumentNode.SelectNodes("//*[name()='a']")?.ToList();
            if (a_nodes != null)
            {
                foreach (var a_node in a_nodes)
                {
                    foreach (var attr in a_node.Attributes.Reverse())
                    {
                        if (attr.Name == "name")
                        {
                            a_node.Remove();
                            break;
                        }
                    }
                }
            }

            var _nodes = htmlDoc.DocumentNode.SelectNodes("//*[name()='div']")?.ToList();
            if (_nodes != null)
            {
                List<string> tags = new List<string>() { "div" };
                html = RemoveDivs(htmlDoc, tags);
                //foreach (var node in _nodes)
                //{
                //    try { htmlDoc.DocumentNode.RemoveChild(node, true); }
                //    catch { }
                //}
            }
            else
                html = htmlDoc.DocumentNode.OuterHtml;

            // Adjust font size to scaling factor
            html = AdjustPixels(html);

            if (html.EndsWith("<p><br></p>"))
                html = html.Replace("<p><br></p>", "");

            if (plainwithlinks)
            {
                //// Convert to plain text with hyperlinks ////

                List<string> links = new List<string>();

                if (OptionInternalLinks.Tag.ToString() == "yes")
                {
                    // getting the non-anchor nodes
                    var nodes = htmlDoc.DocumentNode.SelectNodes("//*[name()='a']")?.ToList();

                    if (nodes == null)
                        isPlain = true;
                    else
                    {
                        // replacing with the inner html
                        foreach (var node in nodes)
                        {
                            HtmlNode rep = node.Clone();
                            string url = "";
                            foreach (var atr in rep.Attributes.Reverse())
                            {
                                if (atr.Name == "href") url = atr.Value;
                                else atr.Remove();
                            }

                            if (rep.InnerHtml.StartsWith("<img")) // image with hyperlink
                                rep.InnerHtml = "***" + "[picture]" + "###";
                            else
                                rep.InnerHtml = "***" + rep.InnerText + "###";

                            links.Add(rep.InnerHtml + url);
                            node.ParentNode.ReplaceChild(rep, node);
                        }

                        // and getting the output
                        var output = htmlDoc.DocumentNode.OuterHtml;
                        WB.Document.Body.InnerHtml = output;
                    }
                }
                else
                    isPlain = true;

                // Get plain text
                editor.SelectAll();
                rng = editor.doc.selection.createRange();
                string plain = rng.text;//.Replace("\r\n\r\n", "\r\n");

                // And convert it to html
                html = "<p>" + plain.Replace("\r\n", "</p><p>") + "</p>";

                if (OptionInternalLinks.Tag.ToString() == "yes")
                {
                    // Add links
                    foreach (var link in links)
                    {
                        string[] pair = link.Split(new string[] { "###" }, StringSplitOptions.None);
                        string title = pair[0] + "###";
                        string url = "<a href=\"" + pair[1] + "\">" + pair[0].Substring(3) + "</a>";

                        var regex = new Regex(Regex.Escape(title));
                        html = regex.Replace(html, url, 1);
                    }
                }
                else
                    isPlain = true;
            }

            if (OptionSourceLink.Tag.ToString() == "yes")
            {
                StixUtils.GetSourceURL(Clipboard.GetText(TextDataFormat.Html));
                if (StixUtils.SourceURL != "")
                {
                    html += "<p><a href=\"" + StixUtils.SourceURL + "\"><font color=\"red\">" +
                        Utils.getString("TextOpsStix.AddNotes.Source") + "</font></a></p>";
                    isPlain = false;
                }
            }

            return html;
        }

        string RemoveDivs(HtmlDocument htmlDoc, List<string> tagNames)
        {
            var tags = (from tag in htmlDoc.DocumentNode.Descendants()
                        where tagNames.Contains(tag.Name)
                        select tag).Reverse();

            // find formatting tags
            foreach (var item in tags)
            {
                if (item.PreviousSibling == null)
                {
                    // Prepend children to parent node in reverse order
                    foreach (HtmlNode node in item.ChildNodes.Reverse())
                        item.ParentNode.PrependChild(node);
                }
                else
                {
                    // Insert children after previous sibling
                    foreach (HtmlNode node in item.ChildNodes)
                        item.ParentNode.InsertAfter(node, item.PreviousSibling);
                }

                // remove from tree
                item.Remove();
            }

            // return transformed html
            return htmlDoc.DocumentNode.WriteContentTo().Trim();
        }

        bool AddLinksToTopicNotes(Topic t, bool links = true)
        {
            t.Notes.CursorPosition = -1; bool affected = false;

            if (StixUtils.SourceURL != "")
            {
                t.Notes.InsertTextHyperlink(StixUtils.SourceURL, Utils.getString("TextOpsStix.AddNotes.Source"));
                affected = true;
            }
            if (StixUtils.Links.Count > 0 && links)
            {
                affected = true; int i = 1;
                foreach (string link in StixUtils.Links)
                    t.Notes.InsertTextHyperlink(link, Utils.getString("TextOpsStix.AddNotes.Link") + " " + i++);
            }
            return affected;
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
                if (Utils.ActiveDocumentOrSelectionNull()) return;

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
                StixUtils.TopicWidthList.Clear();

                // Get links from copied text
                StixUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes", 
                    OptionInternalLinks.Tag.ToString() == "yes");

                if (Utils.ActiveDocumentOrSelectionNull()) return;

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

                        StixUtils.ActivateMindManager();
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
                if (StixUtils.SourceURL != "")
                    t.Hyperlinks.AddHyperlink(StixUtils.SourceURL);
                foreach (string link in StixUtils.Links)
                    t.Hyperlinks.AddHyperlink(link);

                StixUtils.TopicWidthList.Add(t);
            }
            pastetopic = false;
            if (StixUtils.TopicAutoWidth)
                StixUtils.SetTopicWidth();
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
                if (StixUtils.SourceURL != "")
                    t.Hyperlinks.AddHyperlink(StixUtils.SourceURL);
                foreach (string link in StixUtils.Links)
                    t.Hyperlinks.AddHyperlink(link);

                StixUtils.TopicWidthList.Add(t);
            }

            if (StixUtils.TopicAutoWidth)
                StixUtils.SetTopicWidth();
        }

        

        public void PasteOperations_Tick(object sender, EventArgs e)
        {
            PasteOperations.Stop();

            if (Utils.ActiveDocumentOrSelectionNull()) return;

            if (pasteOperation == "pastetotopic")
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
        static string pasteOperation = "";

        public void PasteToTopic(Document pDocument)
        {
            rtb.Clear();
            StixUtils.TopicWidthList.Clear();

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
                    if (StixUtils.SourceURL != "")
                        t.Hyperlinks.AddHyperlink(StixUtils.SourceURL);

                    foreach (string link in StixUtils.Links)
                        t.Hyperlinks.AddHyperlink(link);

                    StixUtils.TopicWidthList.Add(t);
                }
            }

            if (StixUtils.TopicAutoWidth)
                StixUtils.SetTopicWidth();

            PastedTopics.Clear(); SelectedTopics.Clear();
        }

        public void PasteAsTopic()
        {
            // or <Paste as Callout> or <Paste as Parent>
            bool onetopic = OptionMultipleTopics.Tag.ToString() == "single" || 
                transTopicType == "Callout" || transTopicType == "ParentTopic";

            rtb.Clear();
            StixUtils.TopicWidthList.Clear();

            if (onetopic) // Merge text from pasted topics
            {
                foreach (Topic t in PastedTopics)
                {
                    //move cursor to the end
                    rtb.Select(rtb.TextLength, 0);
                    //append the topic rtf
                    rtb.SelectedRtf = t.Title.TextRTF;
                }
            }

            if (onetopic) // Delete pasted topics
                foreach (Topic t in PastedTopics.Reverse<Topic>())
                    t.Delete();

            // Paste resulting (above) text to the selected topics
            int p = 0, i = 0; // selected topics count
            foreach (Topic t in SelectedTopics)
            {
                p++; i++;
                Topic frameTopic = null;
                {
                    if (onetopic) // Also, Callout and Parent topic
                    {
                        frameTopic = StixUtils.AddTopic(t, transTopicType, rtb.Rtf, true, true);
                        if (!StixUtils.TopicWidthList.Contains(frameTopic))
                            StixUtils.TopicWidthList.Add(frameTopic);
                    }
                    else // Multiple topics to paste. Subtopic, Next Topic or Topic before
                    {
                        foreach (Topic _t in StixTextOps.PastedTopics)
                        {
                            if (!StixUtils.TopicWidthList.Contains(_t))
                                StixUtils.TopicWidthList.Add(_t);

                            // Add the Source URL to the FIRST topic
                            // Other topics have links already
                            if (i++ == 1 && StixUtils.SourceURL != "")
                            {
                                _t.Hyperlinks.AddHyperlink(StixUtils.SourceURL);
                                if (_t.Hyperlinks.Count > 1)
                                    _t.Hyperlinks.MoveToTop(_t.Hyperlinks.Count);
                            }

                            // FIRST selected topic. <subtopic> is already in the place.
                            // Move <next topic> or <topic before> to the appropiate place. 
                            if (transTopicType == "subtopic" && p == 1)
                            {
                                if (OptionTextFormat.Tag.ToString() == "unformatted")
                                    _t.Font.SetAutomatic(63);
                                if (!StixUtils.TopicWidthList.Contains(_t))
                                    StixUtils.TopicWidthList.Add(_t);
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
                                StixUtils.Links.Clear();
                                foreach (Hyperlink link in _t.Hyperlinks)
                                    StixUtils.Links.Add(link.Address);

                                if (OptionTextFormat.Tag.ToString() == "formatted")
                                    frameTopic = StixUtils.AddTopic(t, transTopicType, _t.Title.TextRTF, true);
                                else
                                    frameTopic = StixUtils.AddTopic(t, transTopicType, _t.Text);

                                if (!StixUtils.TopicWidthList.Contains(frameTopic))
                                    StixUtils.TopicWidthList.Add(frameTopic);
                            }
                        }
                    }
                }
            }

            if (StixUtils.TopicAutoWidth)
                StixUtils.SetTopicWidth();

            PastedTopics.Clear(); SelectedTopics.Clear();
        }

        private void UnformatText_Click(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                t.Font.SetAutomatic(63);
                t.TextColor.SetAutomatic();
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
            if (e.Button == MouseButtons.Left)
            {
                PasteTopic("subtopic");
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (Utils.FreeVersionLimitExceeded("pastetopics"))
                    return;

                StixUtils.ShowCommandPopup(this, orientation, StixUtils.typetextops, "paste", scaleFactor);
            }
        }

        public void PasteTopic(string topicType)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            transTopicType = topicType;
            SelectedTopics.Clear(); TopicsToAdd.Clear();
            SelectedTopics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>());

            bool formatted = OptionTextFormat.Tag.ToString() == "formatted";
            bool single = OptionMultipleTopics.Tag.ToString() == "single";
            bool singleforced = !Clipboard.ContainsData(System.Windows.DataFormats.Html) &&
                !Clipboard.ContainsData(System.Windows.DataFormats.Rtf);

            StixUtils.GetLinks(OptionSourceLink.Tag.ToString() == "yes",
                OptionInternalLinks.Tag.ToString() == "yes");

            if (formatted && !singleforced)
            {
                transFormatted = true;
                if (single && Clipboard.ContainsData(System.Windows.DataFormats.Rtf)) // we have the rtf text already
                {
                    TopicsToAdd.Add(Clipboard.GetText(TextDataFormat.Rtf));
                    TrsPasteTopic(Utils.getString("TextOpsStix.transactionname.insert"));
                }
                else // paste to format text
                {
                    pastetext = true; // for onObjectAdded event
                    pasteOperation = "pasteastopic";
                    PastedTopics.Clear();

                    StixUtils.ActivateMindManager();
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
                    TrsPasteTopic(Utils.getString("TextOpsStix.transactionname.insert"));
                }
                else // paste as multiple topic with unformatted text from clipboard
                {
                    string text = Clipboard.GetText(TextDataFormat.UnicodeText);
                    TopicsToAdd = text.Split(new[] { "\r\n", "\r", "\n" },
                        StringSplitOptions.RemoveEmptyEntries).ToList();

                    // We have formatted multiline text with links. MindManager, help!
                    if (TopicsToAdd.Count > 1 && StixUtils.Links.Count > 1 && !singleforced)
                    {
                        pastetext = true; // for onObjectAdded event
                        pasteOperation = "pasteastopic";
                        PastedTopics.Clear();

                        StixUtils.ActivateMindManager();
                        SelectedTopics[0].SelectOnly(); // select the first of selected topics
                        PasteOperations.Start(); // start timer to process pasted topics
                                                 // paste text from clipboard (in MM23 Selection.Paste() doesn't work!)
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                    }
                    else
                        TrsPasteTopic(Utils.getString("TextOpsStix.transactionname.insert"));
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
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            if (TopicsToAdd.Count > 1 && transTopicType == "nexttopic")
                TopicsToAdd = TopicsToAdd.Reverse<string>().ToList();

            bool rtf = transFormatted;

            if (transTopicType == "parenttopic") // selected topics will be subtopics of the future parent topic
            {
                StixUtils.AddTopic(MMUtils.ActiveDocument.Selection.PrimaryTopic, transTopicType, TopicsToAdd[0], rtf, true);
            }
            else
            {
                StixUtils.TopicWidthList.Clear();
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    bool firsttopic = true; // Source Link is added to the first topic only!
                    foreach (var name in TopicsToAdd)
                    {
                        StixUtils.AddTopic(t, transTopicType, name, rtf, firsttopic);
                        if (firsttopic) firsttopic = false;
                    }
                }
            }
            if (StixUtils.TopicAutoWidth)
                StixUtils.SetTopicWidth();
        }

        public void OptionButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (Utils.FreeVersionLimitExceeded(StixUtils.typetextops))
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (sender == OptionTextFormat || sender == StixMain.m_sendToMap.OptionTextFormat)
                {
                    PictureBox pb = sender == OptionTextFormat ? OptionTextFormat : StixMain.m_sendToMap.OptionTextFormat;
                    toolTip1 = sender == OptionTextFormat ? toolTip1 : StixMain.m_sendToMap.toolTip1;

                    if (pb.Tag.ToString() == "formatted")
                    {
                        pb.Tag = "unformatted";
                        pb.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "unformattedText.png");
                        toolTip1.SetToolTip(pb, Utils.getString("TextOpsStix.workwith.unformatted"));
                    }
                    else
                    {
                        pb.Tag = "formatted";
                        pb.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "formattedText.png");
                        toolTip1.SetToolTip(pb, Utils.getString("TextOpsStix.workwith.formatted"));
                    }
                }
                else if (sender == OptionReplaceInsert)
                {
                    if (OptionReplaceInsert.Tag.ToString() == "replace")
                    {
                        OptionReplaceInsert.Tag = "insert";
                        OptionReplaceInsert.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "inserttext.png");
                        toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("textops.contextmenu.insert1"));

                        PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotes.png");
                        toolTip1.SetToolTip(PasteNotes, Utils.getString("TextOpsStix.AddNotes.tooltip"));
                    }
                    else
                    {
                        OptionReplaceInsert.Tag = "replace";
                        OptionReplaceInsert.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "replacetext.png");
                        toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("textops.contextmenu.insert2"));
                        
                        PasteNotes.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "PasteNotes.png");
                        toolTip1.SetToolTip(PasteNotes, Utils.getString("TextOpsStix.PasteNotes.tooltip"));
                    }
                }
                else if (sender == OptionMultipleTopics)
                {
                    if (OptionMultipleTopics.Tag.ToString() == "single")
                    {
                        OptionMultipleTopics.Tag = "multiple";
                        OptionMultipleTopics.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "cpTopicTemplate.png");
                        toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("textops.contextmenu.multipletopics1"));
                    }
                    else
                    {
                        OptionMultipleTopics.Tag = "single";
                        OptionMultipleTopics.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "cpAddSingle.png");
                        toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("textops.contextmenu.multipletopics2"));
                    }
                }
                else if (sender == OptionSourceLink)
                {
                    if (OptionSourceLink.Tag.ToString() == "no")
                    {
                        OptionSourceLink.Tag = "yes";
                        OptionSourceLink.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "sourcelink_active.png");
                        toolTip1.SetToolTip(OptionSourceLink, Utils.getString("TextOpsStix.sourcelink_yes"));
                    }
                    else
                    {
                        OptionSourceLink.Tag = "no";
                        OptionSourceLink.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "sourcelink.png");
                        toolTip1.SetToolTip(OptionSourceLink, Utils.getString("TextOpsStix.sourcelink_no"));
                    }
                }
                else if (sender == OptionInternalLinks)
                {
                    if (OptionInternalLinks.Tag.ToString() == "no")
                    {
                        OptionInternalLinks.Tag = "yes";
                        OptionInternalLinks.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "internallinks_active.png");
                        toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("TextOpsStix.internallinks_yes"));
                    }
                    else
                    {
                        OptionInternalLinks.Tag = "no";
                        OptionInternalLinks.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "internallinks.png");
                        toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("TextOpsStix.internallinks_no"));
                    }
                }
            }
        }

        private void pReplace_Click(object sender, EventArgs e)
        {
            if (!StixMain.m_ReplaceDlg.Visible)
                StixMain.m_ReplaceDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        public void PopulateTopicWidths()
        {
            cmsTopicWidths.Items.Clear();

            ToolStripItem tsi = new ToolStripLabel(Utils.getString("TextOpsStix.TopicWidth.Label"));
            tsi.Font = new Font(tsi.Font, FontStyle.Bold);
            cmsTopicWidths.Items.Add(tsi);

            foreach (int width in TopicWidthsDlg.stixwidths)
            {
                tsi = cmsTopicWidths.Items.Add(width.ToString());
                tsi.Name = "ManualWidth"; tsi.Tag = width.ToString();
            }

            ToolStripTextBox mtb = new ToolStripTextBox();
            mtb.Width = pReplace.Width * 2;
            mtb.BorderStyle = BorderStyle.FixedSingle;
            mtb.ToolTipText = Utils.getString("TextOpsStix.TopicWidth.tooltip");
            mtb.KeyDown += Mtb_KeyDown;
            cmsTopicWidths.Items.Add(mtb);

            cmsTopicWidths.Items.Add(new ToolStripSeparator());

            tsi = cmsTopicWidths.Items.Add(Utils.getString("TextOpsStix.TopicWidth.Manage"));
            tsi.Name = "ManageTopicWidths";

            //tsi = cmsTopicWidths.Items.Add(Utils.getString("TextOpsStix.TopicWidth.MMAutoWidth"));
            //tsi.Name = "MMAutoWidth";
            //tsi.ToolTipText = Utils.getString("TextOpsStix.MMAutoWidth.tooltip");
            //(tsi as ToolStripMenuItem).CheckOnClick = true;
            //(tsi as ToolStripMenuItem).Checked = StixUtils.TopicAutoWidth;
            //cmsTopicWidths.Items.Add(tsi);
        }

        private void Mtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

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
                if (Utils.ActiveDocumentOrSelectionNull()) return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Shape.TextWidth = TopicWidthsDlg.mainwidth;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (Utils.FreeVersionLimitExceeded(StixUtils.typetextops))
                    return;

                foreach (ToolStripItem item in cmsTopicWidths.Items)
                    item.Visible = true;

                cmsTopicWidths.Show(Cursor.Position);
                return;
            }
        }

        string transTopicType; bool transFormatted;

        string orientation = "H";
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

        private void WB_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!(e.Url.ToString().Equals("about:blank", StringComparison.InvariantCultureIgnoreCase)))
            {
                System.Diagnostics.Process.Start(e.Url.ToString());
                e.Cancel = true;
            }
        }
    }
}
