using Mindjet.MindManager.Interop;
using PopupControl;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Icon = System.Drawing.Icon;
using Control = System.Windows.Forms.Control;
using WindowsInput.Native;
using WindowsInput;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Clipboard = System.Windows.Forms.Clipboard;

namespace Bubbles
{
    internal class StixUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="orientation"></param>
        /// <param name="type"></param>
        /// <param name="oldname"></param>
        /// <param name="sticktype"></param>
        /// <returns></returns>
        public static string GetName(Form form, string orientation, string type, string oldname, bool stick = false)
        {
            using (GetNameDlg dlg = new GetNameDlg(form, orientation, oldname, type, stick))
            {
                dlg.stickType = type; dlg.stickID = (int)form.Tag; dlg.stick = stick;

                if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                    return "";

                string name = dlg.textBox1.Text.Trim();

                if (oldname != "") // rename stick or icon or tool
                {
                    using (StixDB db = new StixDB())
                    {
                        int stickID = (int)form.Tag;
                        if (stick) // rename stick
                        {
                            db.ExecuteNonQuery("update STICKS set name=`" + name + "` where id=" + stickID + "");
                            StixMain.m_StixBase.RenameContextMenuItem(type, stickID.ToString(), name);
                        }
                        else if (type == typeicons)
                            db.ExecuteNonQuery("update ICONS set name=`" + name +
                                "` where stickID=" + stickID + " and name =`" + oldname + "`");
                        else if (type == typetools)
                            db.ExecuteNonQuery("update TOOLS set title=`" + name +
                                "` where stickID=" + stickID + " and title =`" + oldname + "`");
                    }
                }
                return name;
            }
        }

        public static void CreateStick(Form newForm, string stickname, string sticktype)
        {
            int id = 0; bool contextmenu = false;
            using (StixDB db = new StixDB())
            {
                id = Utils.StickID();
                db.AddStick(id, stickname, sticktype, 0, "H", "");

                newForm.Location = StixMain.m_StixBase.GetStickLocation("", newForm.Size);
                newForm.Tag = id;
                StixMain.STICKS.Add(id, newForm);
                newForm.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

                // Create context menu for stick button in the main menu if there are more than one sticks of this type
                DataTable dt = db.ExecuteQuery("select * from STICKS where type=`" + sticktype + "`");
                if (dt.Rows.Count > 1) contextmenu = true;
            }
            if (contextmenu == true)
                StixMain.m_StixBase.AddSelectMenu(sticktype);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="p1">First dynamic icon</param>
        /// <param name="Manage">Manage icon</param>
        /// <param name="orientation"></param>
        /// <param name="pb">Add Bookmark or Tool List icon</param>
        /// <returns>orientation</returns>
        public static string RotateStick(Form form, PictureBox Manage, string orientation, PictureBox pb = null)
        {
            if (orientation == "H")
            {
                orientation = "V";
                Manage.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                if (pb != null) pb.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            }
            else
            {
                orientation = "H";
                Manage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                if (pb != null) pb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            int thisWidth = form.Width;
            int thisHeight = form.Height;

            // Get these buttons location before the stick size changing
            Point ManageLocation = new Point(Manage.Location.Y, Manage.Location.X);
            Point pbLoc = new Point();
            if (pb != null) pbLoc = new Point(pb.Location.Y, pb.Location.X);

            form.Size = new Size(thisHeight, thisWidth);

            // Now we can change these buttons location
            Manage.Location = ManageLocation;
            if (pb != null) pb.Location = pbLoc;

            foreach (PictureBox p in form.Controls.OfType<PictureBox>())
            {
                if (p.Name.StartsWith("fontcolor") || p.Name.StartsWith("fillcolor"))
                {
                    if (orientation == "H")
                        p.Location = new Point(p.Location.Y, p.Location.X + p.Width / 2);
                    else
                        p.Location = new Point(p.Location.Y - p.Width / 2, p.Location.X);
                }
                else if (p.Tag != null || p.Name == "pCentral" || p.Name == "p2" || p.Name == "p1")
                    p.Location = new Point(p.Location.Y, p.Location.X);
            }

            return orientation;
        }

        public static void SaveStick(Rectangle rec, int id, string orientation)
        {
            if (!Utils.IsOnMMWindow(rec))
            {
                if (MessageBox.Show(Utils.getString("sticks.stickisoutMMwindow.text"),
                    Utils.getString("sticks.stickisoutMMwindow.title"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }

            Point screenXY = Utils.MMScreen(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);

            using (StixDB db = new StixDB())
            {
                string location = "";
                DataTable dt = db.ExecuteQuery("select * from STICKS where id=" + id + "");

                if (dt.Rows.Count > 0)
                    location = dt.Rows[0]["location"].ToString();

                bool found = false;
                if (!String.IsNullOrEmpty(location)) // Search for screen
                {
                    string[] xy = location.Split(';');
                    foreach (string part in xy)
                    {
                        if (part.StartsWith(screenXY.X + "," + screenXY.Y))
                        {
                            location = location.Replace(part, screenXY.X + "," + screenXY.Y +
                                ":" + rec.X + "," + rec.Y);
                            found = true;
                            break;
                        }
                    }
                }

                if (!found) // screen not found, so new screen
                    location += ";" + screenXY.X + "," + screenXY.Y + ":" + rec.X + "," + rec.Y;
                location = location.TrimStart(';');

                db.ExecuteNonQuery("update STICKS set " +
                    "location=`" + location + "`, " +
                    "orientation=`" + orientation +
                    "` where id=" + id + "");
            }
        }

        public static List<PictureBox> RefreshStick(Form form, PictureBox p1, string orientation, 
            int MinLength, string sticktype, bool deleteall = false)
        {
            List <PictureBox> lpb = new List<PictureBox>();

            // Remove all icons
            foreach (PictureBox p in form.Controls.OfType<PictureBox>().Reverse())
            {
                if (p.Tag == null || p.Name == "SourceList") // not dynamic icons
                    continue;
                p.Dispose();
            }

            // Reset bubble size to minimum
            if (orientation == "H")
                stickLength = MinLength;
            else
                stickLength = MinLength;

            if (sticktype == typetools) stickLength += icondist;

            using (StixDB db = new StixDB())
            {
                if (deleteall) // Clean bubble and database
                {
                    if (sticktype == typeicons)
                        db.ExecuteNonQuery("delete from ICONS where stickID =" + (int)form.Tag + "");
                    else if (sticktype == typetools)
                        db.ExecuteNonQuery("delete from TOOLS where stickID =" + (int)form.Tag + "");
                }
                else // Add icons to stick
                {
                    int k = 0;
                    if (sticktype == typeicons)
                    {
                        foreach (var item in Icons)
                        {
                            PictureBox pb = AddIcon(p1, item, item.Path, orientation, k++);
                            lpb.Add(pb); form.Controls.Add(pb);
                            db.ExecuteNonQuery("update ICONS set _order=" + item.Order + " where stickID=" + (int)form.Tag + " and filename =`" + item.FileName + "`");
                        }
                    }
                    else if (sticktype == typetools)
                    {
                        foreach (var item in Tools)
                        {
                            PictureBox pb = AddTool(p1, item, item.Path, orientation, k++);
                            lpb.Add(pb); form.Controls.Add(pb);
                            db.ExecuteNonQuery("update TOOLS set _order=" + item.Order + " where stickID=" + (int)form.Tag + " and path =`" + item.Path + "`");
                        }
                    }
                }
            }

            if (orientation == "H")
                form.Width = stickLength;
            else
                form.Height = stickLength;

            return lpb;
        }

        public static PictureBox AddIcon(PictureBox p1, IconItem item, string path, 
            string orientation, int k)
        {
            PictureBox pBox = AddPitureBox(p1, orientation, path, k, typeicons);
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(pBox, item.IconName);
            pBox.Tag = item;
            return pBox;
        }

        public static PictureBox AddTool(PictureBox p1, ToolItem item, string path, 
            string orientation, int k)
        {
            PictureBox pBox = AddPitureBox(p1, orientation, path, k, typetools, item.Type);
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(pBox, item.Title);
            pBox.Tag = item;
            return pBox;
        }

        static PictureBox AddPitureBox(PictureBox p1, string orientation, string path, int k, 
            string stickType, string imageType = "")
        {
            PictureBox pBox = new PictureBox();
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.AllowDrop = true;
            if (stickType == typeicons)
                pBox.Image = Image.FromFile(path);
            else if (stickType == typetools)
            {
                if (imageType == "exe")
                {
                    try
                    {
                        Icon appIcon = Icon.ExtractAssociatedIcon(path);
                        pBox.Image = appIcon.ToBitmap();
                    }
                    catch { pBox.Image = Utils.GetImage(imageType); }
                }
                else if (imageType.Contains("."))
                    pBox.Image = Image.FromFile(Utils.m_dataPath + "IconDB\\" + imageType);
                else if (imageType == "http")
                {
                    if (Utils.getRegistry("FaviconsToolStix", "1") == "1")
                        pBox.Image = Utils.GetFavicon(path);
                    else
                        pBox.Image = Utils.GetImage(imageType);
                }
                else
                    pBox.Image = Utils.GetImage(imageType);
            }
            pBox.Visible = true;
            pBox.BringToFront();

            if (orientation == "H")
            {
                pBox.Location = new Point(p1.Location.X + (icondist * k++), p1.Location.Y);
                if (k > 4)
                    stickLength += icondist;
            }
            else
            {
                pBox.Location = new Point(p1.Location.X, p1.Location.Y + (icondist * k++));
                if (k > 4)
                    stickLength += icondist;
            }
            return pBox;
        }

        public static void DeleteIcon(PictureBox selectedIcon, int id, string type)
        {
            if (type == typeicons)
            {
                IconItem _item = (IconItem)selectedIcon.Tag;
                string filename = _item.FileName;

                if (_item == null) return;

                _item = Icons.Find(x => x.FileName == filename);
                Icons.Remove(_item);

                for (int i = 0; i < Icons.Count; i++)
                    Icons[i].Order = i + 1;

                using (StixDB db = new StixDB())
                    db.ExecuteNonQuery("delete from ICONS where stickID=" + id + " and filename=`" + filename + "`");
            }
            else if (type == typetools)
            {
                ToolItem _item = (ToolItem)selectedIcon.Tag;
                string filename = _item.Path;

                if (_item == null) return;

                _item = Tools.Find(x => x.Path == filename);
                Tools.Remove(_item);

                for (int i = 0; i < Tools.Count; i++)
                    Tools[i].Order = i + 1;

                using (StixDB db = new StixDB())
                    db.ExecuteNonQuery("delete from TOOLS where path=`" + filename + "`");
            }
        }

        public static bool DeleteStick(int id, string type)
        {
            if (MessageBox.Show(Utils.getString("stix.deletestick.warning"), 
                Utils.getString("stix.contextmenu.deletestick"), 
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return false;

            bool contextmenu = false;
            using (StixDB db = new StixDB())
            {
                // Delete icons that belong to this stick and clear context menu of this button
                if (type == typeicons)
                {
                    db.ExecuteNonQuery("delete from ICONS where stickID=" + id + "");
                    if (StixMain.m_StixBase.cmsIcons.Items.Count > 0)
                        StixMain.m_StixBase.cmsIcons.Items.Clear();
                }
                else if (type == typetools)
                {
                    db.ExecuteNonQuery("delete from TOOLS where stickID=" + id + "");
                    if (StixMain.m_StixBase.cmsTools.Items.Count > 0)
                        StixMain.m_StixBase.cmsTools.Items.Clear();
                }

                // Delete the stick
                db.ExecuteNonQuery("delete from STICKS where id=" + id + "");
                StixMain.STICKS.Remove(id);

                DataTable dt = db.ExecuteQuery("select * from STICKS where type=`" + type + "`");
                if (dt.Rows.Count > 1) contextmenu = true;
            }

            // Create context menu for the main menu button if there are more than one stick of this type
            if (contextmenu == true)
                StixMain.m_StixBase.AddSelectMenu(type);

            return true;
        }

        public static bool manage_clicked = false;
        public static void ShowCommandPopup(Form form, string orientation, string type, string popup = "", float scaleFactor = 100)
        {
            if (manage_clicked) // Manage icon was clicked, don't show command popup
            {
                manage_clicked = false; return;
            }

            if (StixMain.commandPopup.Tag != form.Tag || StixMain.commandPopup.Name != popup || !StixMain.commandPopup.Visible)
            {
                // Hide previous popup
                ActivateMindManager(); // In order to hide previous Popup

                var sp = new StixPopup();
                Control ff = null;

                // Control ff is a panel with icons from the StickPopup() User Control
                //if (popup == "") ff = sp.panelH;
                if (type == typetextops) ff = sp.panelPasteTopic;
                else if (popup == "progress") ff = sp.panelProgress;
                else if (popup == "priority") ff = sp.panelPriority;

                if (popup != "" && scaleFactor != 100)
                    ff.Scale(new SizeF(scaleFactor / 100, scaleFactor / 100)); // scale popup

                if (type != typeicons && type != typebookmarks && type != typeformat && popup == "")
                {
                    ff.Size = new Size(sp.panelCommonMin.Width, ff.Height);
                }
                else // add specific icons for specific popup
                {
                    if (type == typeicons)
                    {
                        sp.pNewIcon.Location = sp.p1.Location;
                        sp.pDeleteAllIcons.Location = sp.p2.Location;
                        ff.Controls.Add(sp.pNewIcon); ff.Controls.Add(sp.pDeleteAllIcons);
                    }
                    else if (type == typebookmarks)
                    {
                        sp.pNewBookmark.Location = sp.p1.Location;
                        sp.pBookmarkList.Location = sp.p2.Location;
                        ff.Controls.Add(sp.pNewBookmark); ff.Controls.Add(sp.pBookmarkList);
                    }
                }

                if (orientation == "V" && !popup.StartsWith("calendar")) // Rotate control and its elements
                {
                    int ffLength = ff.Width;
                    Point close = sp.pClose.Location;
                    ff.Width = ff.Height;
                    ff.Height = ffLength;
                    foreach (PictureBox pb in ff.Controls.OfType<PictureBox>())
                        pb.Location = new Point(pb.Location.Y, pb.Location.X);
                    sp.pClose.Location = new Point(close.Y, close.X);
                }

                ff.Tag = form; ff.AccessibleName = popup;
                StixMain.commandPopup = new Popup(ff);
                StixMain.commandPopup.Tag = form.Tag; // stick id
                StixMain.commandPopup.Name = popup;
                StixMain.commandPopup.ShowingAnimation = PopupAnimations.Center;
                StixMain.commandPopup.AnimationDuration = 300;

                Rectangle child = ff.RectangleToScreen(ff.ClientRectangle);
                Point loc = GetChildLocation(form, child, orientation, popup);
                StixMain.commandPopup.Show(loc);
            }
        }

        /// <summary>
        /// Add or paste topic to a certain position (next topic, topic before, etc.) relative to given topic
        /// </summary>
        /// <param name="t">Given (selected) topic</param>
        /// <param name="topicType">"subtopic", "next", "before", "parent", "callout"</param>
        /// <param name="text">New topic text. #default#" is a new topic default text</param>
        /// <returns>Added topic</returns>
        public static Topic AddTopic(Topic t, string topicType, string text = "#default#", 
            bool rtf = false, bool sourceURL = false)
        {
            Topic newTopic;

            if (topicType == "subtopic")
                newTopic = t.AllSubTopics.Add();
            else if (topicType == "callout")
                newTopic = t.AllCalloutTopics.Add();
            else // next topic, topic before or parent topic
                newTopic = t.ParentTopic.AllSubTopics.Add();

            if (rtf)
                newTopic.Title.TextRTF = text;
            else if (text != "#default#")
                newTopic.Text = text;

            if (sourceURL && SourceURL != "")
                newTopic.Hyperlinks.AddHyperlink(SourceURL);
            foreach (string link in Links)
                newTopic.Hyperlinks.AddHyperlink(link);

            TopicWidthList.Add(newTopic);

            if (topicType != "subtopic" && topicType != "callout")
            {
                // Get given topic index in the branch
                int i = 1;
                foreach (Topic _t in t.ParentTopic.AllSubTopics)
                {
                    if (_t == t) break; i++;
                }
                if (topicType == "nexttopic") i++; // Otherwise, topic is added before

                t.ParentTopic.AllSubTopics.Insert(newTopic, i);

                if (topicType == "parenttopic") // future parent just inserted (lines above)
                {
                    MMUtils.ActiveDocument.Selection.Cut();
                    newTopic.SelectOnly();

#if MINDJET23
                    // Paste copied topics. In MM23 Selection.Paste() doesn't work!
                    ActivateMindManager();
                    InputSimulator sim = new InputSimulator();
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                    // Text will be pasted after this (and previous) method is finished!!
#else
                    {
                        MMUtils.ActiveDocument.Selection.Paste();
                    }
#endif
                }
            }
            return newTopic;
        }

        public static List<Topic> TopicWidthList = new List<Topic>();

        public static void SetTopicWidth()
        {
            foreach (Topic t in TopicWidthList)
            {
                int textlength = t.Text.Length;
                string rtf = t.Title.TextRTF;

                string number = ""; int fs = 0; ;
                do
                {
                    fs = rtf.IndexOf("\\fs", fs + 1);
                    if (fs > 0)
                    {
                        number = new String(rtf.Substring(fs, 5).Where(Char.IsDigit).ToArray());
                        if (number != "")
                        {
                            try
                            {
                                fs = Convert.ToInt32(rtf.Substring(fs + 3, 2)) / 2;
                                break;
                            }
                            catch { }
                        }
                    }
                }
                while (fs != -1);

                if (fs == -1) fs = 12;

                textlength = GetTextLength(textlength, fs);

                if (textlength <= MinAutoTopicWidth) return;

                foreach (var pair in AutoTopicWidths)
                {
                    if (textlength > pair.Key) 
                    { 
                        t.Shape.TextWidth = pair.Value; break; 
                    }
                }
            }
            TopicWidthList.Clear();
        }

        /// <summary>
        /// Get text length considering font size (default is for the size 12)
        /// </summary>
        /// <param name="textlength">Given plain text length</param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static int GetTextLength(int textlength, float fontSize)
        {
            int fontsize = (int)fontSize;
            if (textlength == 0 || fontSize == 12) return textlength;

            if (fontSize == 8) textlength -= 33;
            else if (fontSize == 9) textlength -= 22;
            else if (fontSize == 10) textlength -= 15;
            else if (fontSize == 11) textlength -= 8;
            else if (fontSize == 13) textlength += 7;
            else if (fontSize == 14) textlength += 14;
            else if (fontSize == 15) textlength += 22;
            else if (fontSize == 16) textlength += 30;
            else if (fontSize > 16) textlength += 40;

            return textlength;
        }

        /// <summary>
        /// Get source url and links containing in the copied text
        /// </summary>
        public static void GetLinks(bool source_link, bool internal_links)
        {
            bool aWord = false;
            Links.Clear(); SourceURL = "";

            if (!source_link && !internal_links) return;

            string text = Clipboard.GetText(TextDataFormat.UnicodeText);
            string html = Clipboard.GetText(TextDataFormat.Html);
            string rtf = Clipboard.GetText(TextDataFormat.Rtf);

            if (!String.IsNullOrEmpty(html)) // from html page or from WORD document
            {
                // Get source url and detect if it's a word document

                //Word.Application WordObj;
                //WordObj = (Word.Application)Marshal.GetActiveObject("Word.Application");
                //List<string> doc_list = new List<string>();
                //for (int q = 0; q < WordObj.Windows.Count; q++)
                //{
                //    object idx = q + 1;
                //    Word.Window WinObj = WordObj.Windows.get_Item(ref idx);
                //    doc_list.Add(WinObj.Document.FullName);
                //}

                //string docPath = WordObj.ActiveDocument.FullName;

                int i = html.IndexOf("SourceURL:");
                if (i > 0) // yes, there is a source url
                {
                    i += 10; // skip the "SourceURL:"
                    int k = html.IndexOf("\r\n", i);
                    if (k > 0) SourceURL = html.Substring(i, k - i);

                    if (!source_link || SourceURL.ToLower().EndsWith(".doc") || SourceURL.ToLower().EndsWith(".docx"))
                    {
                        SourceURL = ""; aWord = true;
                    }
                }

                // Get links from code. <a href="">
                if (internal_links)
                {
                    var r = new Regex("<a.*?href=\"(.*?)\".*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var output = r.Matches(html).OfType<Match>().Select(x => x.Groups[1].Value);
                    foreach (var item in output)
                        if (!Links.Contains(item)) Links.Add(item);
                }
            }

            // Get links from rtf (but not from MSWord!) or plain text
            if (internal_links)
            {
                if (!String.IsNullOrEmpty(rtf) && !aWord)
                    text = rtf;

                var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (Match m in linkParser.Matches(text))
                {
                    string link = m.Value.TrimEnd('\\', 'p', 'a', 'r'); // correction for the links from pdf document
                    if (link.StartsWith("www.")) link = "http://" + link;

                    if (!Links.Contains(link))
                        Links.Add(link);
                }
            }
        }
        public static List<string> Links = new List<string>();
        public static string SourceURL = "";



        /// <summary>
        /// Add or paste topic to a certain position (next topic, topic before, etc.) relative to given topic
        /// </summary>
        /// <param name="t">Given (selected) topic</param>
        /// <param name="topicType">"subtopic", "next", "before", "parent", "callout"</param>
        /// <param name="text">Topic text</param>
        /// <param name="links">Topic links</param>
        /// <param name="rtf">If the topic text is formatted</param>
        public static Topic PasteTopic(Topic t, string topicType, string text, List<string> links, bool rtf = false)
        {
            Topic newTopic;

            if (topicType == "subtopic")
                newTopic = t.AllSubTopics.Add();
            else if (topicType == "Callout")
                newTopic = t.AllCalloutTopics.Add();
            else // next topic, topic before or parent topic
                newTopic = t.ParentTopic.AllSubTopics.Add();

            if (rtf)
                newTopic.Title.TextRTF = text;
            else
                newTopic.Text = text;

            if (topicType != "subtopic" && topicType != "Callout")
            {
                // Get given topic index in the branch
                int i = 1;
                foreach (Topic _t in t.ParentTopic.AllSubTopics)
                {
                    if (_t == t) break; i++;
                }
                if (topicType == "nexttopic") i++; // Otherwise, add topic before

                t.ParentTopic.AllSubTopics.Insert(newTopic, i);

                if (topicType == "ParentTopic")
                {
                    MMUtils.ActiveDocument.Selection.Cut();
                    newTopic.SelectOnly();

#if MINDJET23
                    // Paste copied topics. In MM23 Selection.Paste() doesn't work!
                    ActivateMindManager();
                    InputSimulator sim = new InputSimulator();
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                    // Text will be pasted after this (and previous) method is finished!!
#else
                    {
                        MMUtils.ActiveDocument.Selection.Paste();
                    }
#endif
                }
            }
            return newTopic;
        }

        public static bool ActivateMindManager()
        {
            Process p = Process.GetProcessesByName("MindManager").FirstOrDefault();
            if (p == null)
                return false;
            else
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                return true;
            }
        }

        public static string Handle_DragDrop(ref string path, string[] draggedFiles, 
            List<IconItem> aIcons, List<ToolItem> aTools)
        {
            string title = "";
            if (!String.IsNullOrEmpty(path)) // possible url
            {
                if (path.Contains("http"))
                {
                    Uri myUri = new Uri(path);
                    if (myUri != null)
                    {
                        if (aTools != null)
                        {
                            foreach (var item in aTools) // проверим, есть ли в стике значок с этим путем
                            if (item.Path == path) // yes, exists
                            { MessageBox.Show(Utils.getString("stix.iconexists")); return ""; }
                        }
                        else if (aIcons != null)
                        {
                            foreach (var item in aIcons) // проверим, есть ли в стике значок с этим путем
                                if (item.Path == path) // yes, exists
                                { MessageBox.Show(Utils.getString("stix.iconexists")); return ""; }
                        }
                        title = myUri.Host;
                    }
                }
            }
            else if (draggedFiles != null)
            {
                if (aTools != null)
                {
                    foreach (var item in aTools) // проверим, есть ли в стике значок с этим путем
                    if (item.Path == draggedFiles[0]) // yes, exists
                    { MessageBox.Show(Utils.getString("stix.iconexists")); return ""; }
                }
                if (aIcons != null)
                {
                    foreach (var item in aIcons) // проверим, есть ли в стике значок с этим путем
                        if (item.Path == draggedFiles[0]) // yes, exists
                        { MessageBox.Show(Utils.getString("stix.iconexists")); return ""; }
                }
                path = draggedFiles[0];
                title = Path.GetFileNameWithoutExtension(path);
            }
            return title;
        }

        public static void SetCommonContextMenu(ContextMenuStrip cms, string stickType = "")
        {
            ToolStripItem tsi = null;

            if (stickType == typeicons || stickType == typetools || stickType == typebookmarks)
            {
                tsi = new ToolStripLabel(Utils.getString("contextmenu.stickoperations"));
                tsi.Font = new Font(tsi.Font, FontStyle.Bold); cms.Items.Add(tsi);

                tsi = cms.Items.Add(Utils.getString("contextmenu.clearstick"));
                tsi.Name = "BI_deleteall";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "deleteall.png"), cmiSize);

                string deleteall = Utils.getString("icons.contextmenu.clearstick.tooltip");
                if (stickType == typetools) deleteall = Utils.getString("tools.contextmenu.deleteall");
                if (stickType == typebookmarks) deleteall = Utils.getString("bookmarks.contextmenu.deleteall");

                tsi.ToolTipText = deleteall;
            }

            tsi = cms.Items.Add(Utils.getString("stix.contextmenu.rotate"));
            tsi.Name = "BI_rotate";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "rotate.png"), cmiSize);

            tsi = cms.Items.Add(Utils.getString("stix.contextmenu.remember"));
            tsi.Name = "BI_store";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "remember.png"), cmiSize);

            if (stickType == typeicons || stickType == typetools)
            {
                tsi = cms.Items.Add(Utils.getString("button.rename"));
                tsi.Name = "BI_renamestick";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "edit.png"), cmiSize);

                tsi = cms.Items.Add(Utils.getString("stix.contextmenu.deletestick"));
                tsi.Name = "BI_delete_stick";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "deleteStick.png"), cmiSize);

                tsi = cms.Items.Add(Utils.getString("stix.contextmenu.newstick"));
                tsi.Name = "BI_newstick";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "newStick.png"), cmiSize);
            }

            tsi = cms.Items.Add(Utils.getString("stix.contextmenu.scale"));
            tsi.Name = "BI_scale";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "remember.png"), cmiSize);

            cms.Items.Add(new ToolStripSeparator());

            tsi = cms.Items.Add(Utils.getString("button.help"));
            tsi.Name = "BI_help";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "help.png"), cmiSize);

            tsi = cms.Items.Add(Utils.getString("button.close"));
            tsi.Name = "BI_close";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "close_sticker.png"), cmiSize);
        }

        public static void SetContextMenuImage(ToolStripItem tsi, string imgName)
        {
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            if (imgName != "")
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + imgName), cmiSize);
        }

        /// <summary>
        /// Get location for the command popup and other child forms
        /// </summary>
        /// <param name="parent">Parent form rectangle</param>
        /// <param name="child">Child form rectangle</param>
        /// <param name="orientation">Parent form orientation</param>  
        /// <param name="popup"></param>
        public static Point GetChildLocation(Form parent, Rectangle child, string orientation, string popup = "")
        {
            int X, Y;
            if (orientation == "H")
            {
                // common comands popup
                X = parent.Right - child.Width; // child right = parent right
                if (popup == "getname" || popup == "resources" || popup == "bookmarks" || 
                    popup == "tools" || popup == "icons")
                    X = parent.Left; // child right = parent left
                Y = parent.Bottom; // child top = parent bottom

                //if (popup == "add")
                //{
                //    X = parent.Left + (parent as BubblePaste).pAddTopic.Left;
                //    Y = parent.Top + ((parent as BubblePaste).pAddTopic.Top / 2);
                //}
                if (popup == "paste")
                {
                    X = parent.Left + (parent as StixTextOps).PasteLink.Left;
                    Y = parent.Top + ((parent as StixTextOps).subtopic.Top / 2);
                }
                else if (popup == "progress")
                {
                    X = parent.Left + (parent as StixTaskInfo).pPriority.Left;
                    Y = parent.Top + ((parent as StixTaskInfo).pPriority.Width / 4);
                }
                else if (popup == "priority")
                {
                    X = parent.Left + (parent as StixTaskInfo).pResources.Left;
                    Y = parent.Top + ((parent as StixTaskInfo).pPriority.Width / 4);
                }
                else if (popup == "calendar_startdate")
                {
                    X = parent.Left + (parent as StixTaskInfo).panelStartDate.Left;
                    Y = parent.Top;
                }
                else if (popup == "calendar_duedate")
                {
                    X = parent.Left + (parent as StixTaskInfo).panelDueDate.Left;
                    Y = parent.Top;
                }
            }
            else // vertical
            {
                // common comands popup
                X = parent.Right; // child left = parent right
                Y = parent.Bottom - child.Height; // child bottom = parent bottom
                if (popup == "getname" || popup == "resources" || popup == "bookmarks" || 
                    popup == "tools" || popup == "icons")
                    Y = parent.Top; // child top = parent top

                //if (popup == "add")
                //{
                //    X = parent.Left + ((parent as BubblePaste).pAddTopic.Left / 2);
                //    Y = parent.Top + (parent as BubblePaste).pAddTopic.Top;
                //}
                if (popup == "paste")
                {
                    X = parent.Left + ((parent as StixTextOps).subtopic.Left / 2);
                    Y = parent.Top + (parent as StixTextOps).PasteLink.Top;
                }
                else if (popup == "progress")
                {
                    X = parent.Left + (parent as StixTaskInfo).pPriority.Width / 4;
                    Y = parent.Top + (parent as StixTaskInfo).pPriority.Top;
                }
                else if (popup == "priority")
                {
                    X = parent.Left + (parent as StixTaskInfo).pPriority.Width / 4;
                    Y = parent.Top + (parent as StixTaskInfo).panelStartDate.Top;
                }
                else if (popup == "calendar_startdate")
                {
                    X = parent.Left;
                    Y = parent.Top + (parent as StixTaskInfo).panelStartDate.Top;
                }
                else if (popup == "calendar_duedate")
                {
                    X = parent.Left;
                    Y = parent.Top + (parent as StixTaskInfo).panelDueDate.Top;
                }
            }

            Point pos = new Point(X, Y); // Standard child location
            child.Location = pos;
            Point _pos = pos;

            // If the child is close to the right or bottom screen side...
            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;

            if (orientation == "H")  // horizontal stick orientation
            {
                if (child.Right > area.Right) // close to the right
                    pos.X = area.Right - child.Width; // set child right to the parent right

                if (child.Bottom > area.Bottom) // close to the bottom
                    pos.Y = parent.Top - child.Height; // set child bottom to the parent top
            }
            else // vertical stick orientation
            {
                if (_pos.X + child.Width > area.Right) // close to the right
                {
                    if (popup.StartsWith("calendar"))
                        pos.X = parent.Right - child.Width;
                    else
                        pos.X = parent.Left - child.Width; // set child right to the parent left
                }

                if (pos.Y + child.Height > area.Bottom) // close to the bottom
                    pos.Y = area.Bottom - child.Height; // set child bottom to the area bottom
            }

            return pos;
        }

        public static List<IconItem> Icons = new List<IconItem>();
        public static List<BookmarkItem> Bookmarks = new List<BookmarkItem>();
        public static List<ToolItem> Tools = new List<ToolItem>();

        // types must match Stix names!
        public const string typestick = "stick", typebase = "StixBase",
            typeicons = "StixIcons", typetaskinfo = "StixTaskInfo", 
            typeformat = "StixFormat", typetools = "StixTools", typebookmarks = "StixBookmarks",
            typeaddtopic = "StixAddTopic", typetextops = "StixTextOps", typeorganizer = "StixOrganizer";

        public static int stickLength;
        public static int icondist;
        public static Size cmiSize;

        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public static int MainTopicWidth = 63;
        public static List<int> ManualTopicWidths = new List<int>();
        public static Dictionary<int, int> AutoTopicWidths = new Dictionary<int, int>();
        public static int MinAutoTopicWidth;
        public static bool TopicAutoWidth = false;
        public static float ScalingFactor = 100;
    }

    class ResizeStick : Form
    {
        public ResizeStick(Form form) { aForm = form; }
        public Form aForm;

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
                        Point clientPoint = aForm.PointToClient(screenPoint);
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
    }
}
