﻿using Mindjet.MindManager.Interop;
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
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;
using WindowsInput.Native;
using WindowsInput;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bubbles
{
    internal class StickUtils
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
            using (GetNameDlg dlg = new GetNameDlg(form, orientation, oldname))
            {
                dlg.stickType = type; dlg.stickID = (int)form.Tag; dlg.stick = stick;

                if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                    return "";

                string name = dlg.textBox1.Text.Trim();

                if (oldname != "") // rename stick or icon or source
                {
                    using (BubblesDB db = new BubblesDB())
                    {
                        int stickID = (int)form.Tag;
                        if (stick) // rename stick
                        {
                            db.ExecuteNonQuery("update STICKS set name=`" + name + "` where id=" + stickID + "");
                            BubblesButton.m_bubblesMenu.RenameContextMenuItem(type, stickID.ToString(), name);
                        }
                        else if (type == typeicons)
                            db.ExecuteNonQuery("update ICONS set name=`" + name +
                                "` where stickID=" + stickID + " and name =`" + oldname + "`");
                        else if (type == typesources)
                            db.ExecuteNonQuery("update SOURCES set title=`" + name +
                                "` where stickID=" + stickID + " and title =`" + oldname + "`");
                    }
                }
                return name;
            }
        }

        public static void CreateStick(Form newForm, string stickname, string sticktype)
        {
            int id = 0; bool contextmenu = false;
            using (BubblesDB db = new BubblesDB())
            {
                id = Utils.StickID();
                db.AddStick(id, stickname, sticktype, 0, "", 0);

                newForm.Location = BubblesButton.m_bubblesMenu.GetStickLocation("", newForm.Size);
                newForm.Tag = id;
                BubblesButton.STICKS.Add(id, newForm);
                newForm.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

                // Create context menu for stick button in the main menu if there are more than one sticks of this type
                DataTable dt = db.ExecuteQuery("select * from STICKS where type=`" + sticktype + "`");
                if (dt.Rows.Count > 1) contextmenu = true;
            }
            if (contextmenu == true)
                BubblesButton.m_bubblesMenu.AddSelectMenu(sticktype);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="p1">First dynamic icon</param>
        /// <param name="Manage">Manage icon</param>
        /// <param name="orientation"></param>
        /// <param name="pb">Add Bookmark or Source List icon</param>
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

        public static string SaveStick(Rectangle rec, int id, string orientation, bool collapsed, int configID = 0)
        {
            string position = "";

            if (!Utils.IsOnMMWindow(rec))
            {
                if (MessageBox.Show(Utils.getString("sticks.stickisoutMMwindow.text"),
                    Utils.getString("sticks.stickisoutMMwindow.title"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return "";
            }

            Point screenXY = Utils.MMScreen(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);

            using (BubblesDB db = new BubblesDB())
            {
                string location = "";
                DataTable dt = db.ExecuteQuery("select * from STICKS where id=" + id + "");

                if (dt.Rows.Count > 0)
                    position = dt.Rows[0]["position"].ToString();

                if (position != "")
                {
                    string[] parts = position.Split('#');
                    location = parts[1];
                }

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

                string _collapsed = collapsed ? "1" : "0";
                position = orientation + _collapsed + "#" + location.TrimStart(';');

                if (configID != 0)
                    db.ExecuteNonQuery("update STICKS set position=`" + position + 
                        "` where id=" + id + " and configID=" + configID + "");
            }
            return position; // for CreateConfiguration() method
        }

        public static List<PictureBox> RefreshStick(Form form, PictureBox p1, string orientation, 
            int MinLength, bool collapsed, string sticktype, bool deleteall = false)
        {
            List <PictureBox> lpb = new List<PictureBox>();

            // Remove all icons
            foreach (PictureBox p in form.Controls.OfType<PictureBox>().Reverse())
            {
                if (p.Tag == null) // not dynamic icons
                    continue;
                p.Dispose();
            }

            // Reset bubble size to minimum
            if (orientation == "H")
                stickLength = MinLength;
            else
                stickLength = MinLength;

            using (BubblesDB db = new BubblesDB())
            {
                if (deleteall) // Clean bubble and database
                {
                    if (sticktype == typeicons)
                        db.ExecuteNonQuery("delete from ICONS where stickID =" + (int)form.Tag + "");
                    else if (sticktype == typepripro)
                        db.ExecuteNonQuery("delete from PRIPRO where stickID =" + (int)form.Tag + "");
                    else if (sticktype == typesources)
                        db.ExecuteNonQuery("delete from SOURCES where stickID =" + (int)form.Tag + "");
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
                    else if (sticktype == typepripro)
                    {
                        foreach (var item in PriPros)
                        {
                            PictureBox pb = AddPriPro(p1, item, orientation, k++);
                            lpb.Add(pb); form.Controls.Add(pb);
                        }
                    }
                    else if (sticktype == typesources)
                    {
                        foreach (var item in Sources)
                        {
                            PictureBox pb = AddSource(p1, item, item.Path, orientation, k++);
                            lpb.Add(pb); form.Controls.Add(pb);
                            db.ExecuteNonQuery("update SOURCES set _order=" + item.Order + " where stickID=" + (int)form.Tag + " and path =`" + item.Path + "`");
                        }
                    }
                }
            }

            if (orientation == "H")
                if (!collapsed) form.Width = stickLength;
            else
                if (!collapsed) form.Height = stickLength;

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

        public static PictureBox AddPriPro(PictureBox p1, PriProItem item,
            string orientation, int k)
        {
            PictureBox pBox = AddPitureBox(p1, orientation, "", k, typepripro,
                item.Type, item.Value);
            if (item.Type == "pro")
            {
                ToolTip tt = new ToolTip();
                tt.ShowAlways = true;
                tt.SetToolTip(pBox, item.Value.ToString() + "%");
            }
            pBox.Tag = item;
            return pBox;
        }

        public static PictureBox AddSource(PictureBox p1, MySourcesItem item, string path, 
            string orientation, int k)
        {
            PictureBox pBox = AddPitureBox(p1, orientation, path, k, typesources, item.Type);
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(pBox, item.Title);
            pBox.Tag = item;
            return pBox;
        }

        static PictureBox AddPitureBox(PictureBox p1, string orientation, string path, int k, 
            string stickType, string imageType = "", int prirproValue = 0)
        {
            PictureBox pBox = new PictureBox();
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.AllowDrop = true;
            if (stickType == typeicons)
                pBox.Image = Image.FromFile(path);
            else if (stickType == typepripro)
                pBox.Image = GetImage(imageType, prirproValue);
            else if (stickType == typesources)
            {
                if (imageType == "exe")
                {
                    try
                    {
                        Icon appIcon = Icon.ExtractAssociatedIcon(path);
                        pBox.Image = appIcon.ToBitmap();
                    }
                    catch { pBox.Image = GetImage(imageType); }
                }
                else
                    pBox.Image = GetImage(imageType);
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
                pBox.Location = new Point(p1.Location.X, icondist * k++);
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

                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("delete from ICONS where stickID=" + id + " and filename=`" + filename + "`");
            }
            else if (type == typepripro)
            {
                PriProItem _item = (PriProItem)selectedIcon.Tag;

                PriProItem __item = PriPros.Find(x => x.Type == _item.Type && x.Value == _item.Value);
                if (__item != null)
                    PriPros.Remove(__item);

                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("delete from PRIPRO where stickID=" + id + 
                        " and type=`" + _item.Type + "` and value=" + _item.Value + "");
            }
            else if (type == typesources)
            {
                MySourcesItem _item = (MySourcesItem)selectedIcon.Tag;
                string filename = _item.Path;

                if (_item == null) return;

                _item = Sources.Find(x => x.Path == filename);
                Sources.Remove(_item);

                for (int i = 0; i < Sources.Count; i++)
                    Sources[i].Order = i + 1;

                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("delete from SOURCES where path=`" + filename + "`");
            }
        }

        public static bool DeleteStick(int id, string type)
        {
            if (MessageBox.Show(Utils.getString("sticks.deletestick.warning"), 
                Utils.getString("float_icons.contextmenu.deletestick"), 
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return false;

            bool contextmenu = false;
            using (BubblesDB db = new BubblesDB())
            {
                // Delete icons that belong to this stick and clear context menu of this button
                if (type == typeicons)
                {
                    db.ExecuteNonQuery("delete from ICONS where stickID=" + id + "");
                    if (BubblesButton.m_bubblesMenu.cmsIcons.Items.Count > 0)
                        BubblesButton.m_bubblesMenu.cmsIcons.Items.Clear();
                }
                else if (type == typepripro)
                {
                    db.ExecuteNonQuery("delete from PRIPRO where stickID=" + id + "");
                    if (BubblesButton.m_bubblesMenu.cmsPriPro.Items.Count > 0)
                        BubblesButton.m_bubblesMenu.cmsPriPro.Items.Clear();
                }
                else if (type == typesources)
                {
                    db.ExecuteNonQuery("delete from SOURCES where stickID=" + id + "");
                    if (BubblesButton.m_bubblesMenu.cmsMySources.Items.Count > 0)
                        BubblesButton.m_bubblesMenu.cmsMySources.Items.Clear();
                }

                // Delete the stick
                db.ExecuteNonQuery("delete from STICKS where id=" + id + "");
                BubblesButton.STICKS.Remove(id);

                DataTable dt = db.ExecuteQuery("select * from STICKS where type=`" + type + "`");
                if (dt.Rows.Count > 1) contextmenu = true;
            }

            // Create context menu for the main menu button if there are more than one stick of this type
            if (contextmenu == true)
                BubblesButton.m_bubblesMenu.AddSelectMenu(type);

            return true;
        }

        public static void Collapse(Form form, string orientation, ContextMenuStrip cms)
        {
            if (form.Width > minSize || form.Height > minSize)
            {
                if (orientation == "H")
                    form.Width = minSize;
                else
                    form.Height = minSize;

                form.BackColor = Color.Gainsboro;

                int i = 0;
                foreach (PictureBox pb in form.Controls.OfType<PictureBox>())
                {
                    // hide all icons except the first
                    if (pb.Name != "pBold" && pb.Name != "pPaste" && pb.Tag != null && i++ > 0) // only dynamic icons
                        pb.Visible = false;
                }

                cms.Items["BI_collapse"].Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "expand.png"), cmiSize);
                cms.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.expand");
            }
        }

        public static void Expand(Form form, int RealLength, string orientation, ContextMenuStrip cms)
        {
            if (orientation == "H" && form.Width < RealLength)
                form.Width = RealLength;
            else if (orientation == "V" && form.Height < RealLength)
                form.Height = RealLength;

            form.BackColor = Color.Lavender;

            // show all icons
            foreach (PictureBox pb in form.Controls.OfType<PictureBox>())
                if (pb.Tag != null) pb.Visible = true;

            cms.Items["BI_collapse"].Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "collapse.png"), cmiSize);
            cms.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.collapse");
        }

        public static void ShowCommandPopup(Form form, string orientation, string type, string popup = "")
        {
            if (BubblesButton.commandPopup.Tag != form.Tag || BubblesButton.commandPopup.Name != popup || !BubblesButton.commandPopup.Visible)
            {
                // Kill previous popup
                BubblesButton.commandPopup.Hide();

                var sp = new StickPopup();
                Control ff = null;

                if (popup == "") ff = sp.panelH;
                else if (type == typepaste) ff = sp.panelPasteTopic;

                if (type == typepaste && popup == "add") ff.Width = sp.panelAddTopic.Width;

                if (type != typeicons && type != typebookmarks && type != typeformat && popup == "")
                {
                    ff.Size = new Size(sp.panelMin.Width, ff.Height);
                }
                else
                {
                    if (type == typeicons)
                    {
                        sp.pNewIcon.Location = sp.p1.Location;
                        sp.pPasteIcon.Location = sp.p2.Location;
                        ff.Controls.Add(sp.pNewIcon); ff.Controls.Add(sp.pPasteIcon);
                    }
                    else if (type == typebookmarks)
                    {
                        sp.pNewBookmark.Location = sp.p1.Location;
                        sp.pBookmarkList.Location = sp.p2.Location;
                        ff.Controls.Add(sp.pNewBookmark); ff.Controls.Add(sp.pBookmarkList);
                    }
                    else if (type == typeformat)
                    {
                        sp.pFontItalic.Location = sp.p1.Location;
                        sp.pCleanFormat.Location = sp.p2.Location;
                        ff.Controls.Add(sp.pFontItalic); ff.Controls.Add(sp.pCleanFormat);
                    }
                }

                if (orientation == "V") // Rotate control and its elements
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
                BubblesButton.commandPopup = new Popup(ff);
                BubblesButton.commandPopup.Tag = form.Tag; // stick id
                BubblesButton.commandPopup.Name = popup;
                BubblesButton.commandPopup.ShowingAnimation = PopupAnimations.Center;
                BubblesButton.commandPopup.AnimationDuration = 300;

                Rectangle child = ff.RectangleToScreen(ff.ClientRectangle);
                Point loc = GetChildLocation(form, child, orientation, popup);
                BubblesButton.commandPopup.Show(loc);
            }
        }

        public static void HideCommandPopup(Form form, string orientation)
        {
            // If cursor is outside the stick and the command popup panel, hide the popup panel 
            if (BubblesButton.commandPopup.Visible)
            {
                bool hide = false;

                if (orientation == "H")
                {
                    if (Cursor.Position.X < form.Left || Cursor.Position.X > form.Right ||
                        Cursor.Position.Y < form.Top || Cursor.Position.Y > form.Bottom + form.Height)
                        hide = true;
                }
                else
                {
                    if (Cursor.Position.X < form.Left || Cursor.Position.X > form.Right + form.Width ||
                        Cursor.Position.Y < form.Top || Cursor.Position.Y > form.Bottom)
                        hide = true;
                }

                if (hide)
                {
                    BubblesButton.commandPopup.Hide();
                    BubblesButton.commandPopup.Tag = 0;
                }
            }
        }

        /// <summary>
        /// Add new topic to a certain position (next topic, topic before, etc.) relative to given topic
        /// </summary>
        /// <param name="t">Given topic</param>
        /// <param name="topicType">"subtopic", "next", "before", "parent", "callout"</param>
        /// <param name="text">New topic text. #default#" is a new topic default text</param>
        /// <returns></returns>
        public static Topic AddTopic(Topic t, string topicType, string text = "#default#", bool addparent = true)
        {
            Topic newTopic;

            if (topicType == "Subtopic")
            {
                if (text == "#default#")
                    newTopic = t.AllSubTopics.Add();
                else
                    newTopic = t.AddSubTopic(text);
            }
            else if (topicType == "Callout")
            {
                newTopic = t.AllCalloutTopics.Add();
                if (text != "#default#")
                    newTopic.Text = text;
            }
            else
            {
                if (text == "#default#")
                    newTopic = t.ParentTopic.AllSubTopics.Add();
                else
                    newTopic = t.ParentTopic.AddSubTopic(text);

                // Get given topic index in the branch
                int i = 1;
                foreach (Topic _t in t.ParentTopic.AllSubTopics)
                {
                    if (_t == t) break; i++;
                }
                if (topicType == "NextTopic") i++; // Otherwise, add topic before

                t.ParentTopic.AllSubTopics.Insert(newTopic, i);

                if (topicType == "ParentTopic")
                {
                    ActivateMindManager();

                    //t.SelectOnly();
                    MMUtils.ActiveDocument.Selection.Cut();
                    newTopic.SelectOnly();

                    // Paste copied topics. In MM23 Selection.Paste() doesn't work!
                    InputSimulator sim = new InputSimulator();
                    sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);

                    // Text will be pasted after this (and previous) method is finished!!
                }
            }

            return newTopic;
        }

        static bool ActivateMindManager()
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

        public static void CreateConfiguration(Dictionary<int, Form> sticks, int configID, int start)
        {
            string name, type, position, orientation = "H"; bool collapsed;
            List<int> ids = new List<int>();

            using (BubblesDB db = new BubblesDB())
            {
                foreach (var stick in sticks)
                {
                    // Update _start_ configuration
                    // First, set all configurations to unstart

                    DataTable dt = db.ExecuteQuery("select * from CONFIGS");
                    foreach (DataRow dr in dt.Rows)
                        db.ExecuteNonQuery("update CONFIGS set start=" + 0 + " where id=" + Convert.ToInt32(dr["id"]) + "");

                    // And, if the configuration has to be started, set start state to it
                    if (start == 1)
                        db.ExecuteNonQuery("update CONFIGS set start=1 where id=" + configID + "");

                    int stickid = stick.Key;
                    ids.Add(stickid);

                    dt = db.ExecuteQuery("select * from STICKS where id=" + stickid + "");
                    if (dt.Rows.Count == 0) continue; // impossible, but...

                    name = dt.Rows[0]["name"].ToString();
                    type = stick.Value.Name;

                    if (stick.Value.Width < stick.Value.Height) orientation = "V";
                    if (orientation == "H")
                        collapsed = stick.Value.Width == minSize;
                    else
                        collapsed = stick.Value.Height == minSize;

                    dt = db.ExecuteQuery("select * from STICKS where id=" + stickid + " and configID=" + configID + "");
                    // If stick is not in the configuration already, add it there
                    if (dt.Rows.Count == 0) db.AddStick(stickid, name, type, 0, "", configID);

                    // Update stick position
                    position = SaveStick(stick.Value.Bounds, stickid, orientation, collapsed, configID);
                    db.ExecuteNonQuery("update STICKS set position=`" + position + "` where id=" + stickid + " and configID=" + configID + "");
                }
            }
        }

        public static List<Form> ToConfig = new List<Form>();

        public static string Handle_DragDrop(ref string path, string[] draggedFiles, 
            List<IconItem> aIcons, List<MySourcesItem> aSources)
        {
            string title = "";
            if (!String.IsNullOrEmpty(path)) // possible url
            {
                if (path.Contains("http"))
                {
                    Uri myUri = new Uri(path);
                    if (myUri != null)
                    {
                        if (aSources != null)
                        {
                            foreach (var item in aSources) // проверим, есть ли в стике значок с этим путем
                            if (item.Path == path) // yes, exists
                            { MessageBox.Show(Utils.getString("float_icons.iconexists")); return ""; }
                        }
                        else if (aIcons != null)
                        {
                            foreach (var item in aIcons) // проверим, есть ли в стике значок с этим путем
                                if (item.Path == path) // yes, exists
                                { MessageBox.Show(Utils.getString("float_icons.iconexists")); return ""; }
                        }
                        title = myUri.Host;
                    }
                }
            }
            else if (draggedFiles != null)
            {
                if (aSources != null)
                {
                    foreach (var item in aSources) // проверим, есть ли в стике значок с этим путем
                    if (item.Path == draggedFiles[0]) // yes, exists
                    { MessageBox.Show(Utils.getString("float_icons.iconexists")); return ""; }
                }
                if (aIcons != null)
                {
                    foreach (var item in aIcons) // проверим, есть ли в стике значок с этим путем
                        if (item.Path == draggedFiles[0]) // yes, exists
                        { MessageBox.Show(Utils.getString("float_icons.iconexists")); return ""; }
                }
                path = draggedFiles[0];
                title = Path.GetFileName(path);
            }
            return title;
        }

        public static Image GetImage(string type, int value)
        {
            if (type == "pri")
            {
                switch (value)
                {
                    case 1: return BubblePriPro.PR1;
                    case 2: return BubblePriPro.PR2;
                    case 3: return BubblePriPro.PR3;
                    case 4: return BubblePriPro.PR4;
                    case 5: return BubblePriPro.PR5;
                }
            }
            else
            {
                switch (value)
                {
                    case 0: return BubblePriPro.PRG0;
                    case 10: return BubblePriPro.PRG10;
                    case 25: return BubblePriPro.PRG25;
                    case 35: return BubblePriPro.PRG35;
                    case 50: return BubblePriPro.PRG50;
                    case 65: return BubblePriPro.PRG65;
                    case 75: return BubblePriPro.PRG75;
                    case 90: return BubblePriPro.PRG90;
                    case 100: return BubblePriPro.PRG100;
                }
            }
            return null;
        }

        public static Image GetImage(string type)
        {
            switch (type)
            {
                case "audio": return BubbleMySources.audio;
                case "excel": return BubbleMySources.excel;
                case "exe": return BubbleMySources.exe;
                case "image": return BubbleMySources.image;
                case "macros": return BubbleMySources.macros;
                case "map": return BubbleMySources.map;
                case "pdf": return BubbleMySources.pdf;
                case "txt": return BubbleMySources.txt;
                case "video": return BubbleMySources.video;
                case "http": return BubbleMySources.http;
                case "word": return BubbleMySources.word;
                case "youtube": return BubbleMySources.youtube;
                case "chm": return BubbleMySources.chm;
            }
            return BubbleMySources.file;
        }

        public static void SetCommonContextMenu(ContextMenuStrip cms, string stickType = "")
        {
            string deleteall = Utils.getString("float_icons.contextmenu.deleteall");
            if (stickType == typesources) deleteall = Utils.getString("mysources.contextmenu.deleteall");
            if (stickType == typebookmarks) deleteall = Utils.getString("bookmarks.contextmenu.deleteall");

            ToolStripItem tsi = null;

            if (stickType != typeformat && stickType != typepaste && stickType != typeorganizer)
            {
                tsi = cms.Items.Add(deleteall);
                tsi.Name = "BI_deleteall";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "deleteall.png"), cmiSize);
            }

            tsi = cms.Items.Add(Utils.getString("contextmenu.stickoperations"));
            tsi.Font = new Font(tsi.Font, FontStyle.Bold);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.collapse"));
            tsi.Name = "BI_collapse";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "collapse.png"), cmiSize);
            tsi.ToolTipText = Utils.getString("float_icons.contextmenu.collapse.tooltip");

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.rotate"));
            tsi.Name = "BI_rotate";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "rotate.png"), cmiSize);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.settings"));
            tsi.Name = "BI_store";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "remember.png"), cmiSize);

            if (stickType != typebookmarks && stickType != typeformat && stickType != typepaste && stickType != typeorganizer)
            {
                tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.renamestick"));
                tsi.Name = "BI_renamestick";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "edit.png"), cmiSize);

                tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.deletestick"));
                tsi.Name = "BI_delete_stick";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "deleteStick.png"), cmiSize);

                tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.newstick"));
                tsi.Name = "BI_newstick";
                tsi.ImageScaling = ToolStripItemImageScaling.None;
                tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "newStick.png"), cmiSize);
            }

            cms.Items.Add(new ToolStripSeparator());

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.help"));
            tsi.Name = "BI_help";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "help.png"), cmiSize);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.close"));
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
        public static Point GetChildLocation(Form parent, Rectangle child, string orientation, string popup = "")
        {
            int X, Y;
            if (orientation == "H")
            {
                // common comands popup
                X = parent.Right - child.Width; // child right = parent right
                Y = parent.Bottom; // child top = parent bottom

                if (popup == "add")
                {
                    X = parent.Left + (parent as BubblePaste).pAddTopic.Left;
                    Y = parent.Top + ((parent as BubblePaste).pAddTopic.Top / 2);
                }
                else if (popup == "paste")
                {
                    X = parent.Left + (parent as BubblePaste).pPasteTopic.Left;
                    Y = parent.Top + ((parent as BubblePaste).pPasteTopic.Top / 2);
                }
            }
            else // top right corner
            {
                // common comands popup
                X = parent.Right; // child left = parent right
                Y = parent.Bottom - child.Height; // child bottom = parent bottom

                if (popup == "add")
                {
                    X = parent.Left + ((parent as BubblePaste).pAddTopic.Left / 2);
                    Y = parent.Top + (parent as BubblePaste).pAddTopic.Top;
                }
                else if (popup == "paste")
                {
                    X = parent.Left + ((parent as BubblePaste).pPasteTopic.Left / 2);
                    Y = parent.Top + (parent as BubblePaste).pPasteTopic.Top;
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
                    pos.X = parent.Right - child.Width; // set child right to the parent right

                if (child.Bottom > area.Bottom) // close to the bottom
                    pos.Y = parent.Bottom - child.Height; // set child bottom to the parent top
            }
            else // vertical stick orientation
            {
                if (_pos.X + child.Width > area.Right) // close to the right
                    pos.X = parent.Left - child.Width; // set child right to the parent left

                if (pos.Y + child.Height > area.Bottom) // close to the bottom
                    pos.Y = area.Bottom - child.Height; // set child bottom to the area bottom
            }

            return pos;
        }

        public static List<IconItem> Icons = new List<IconItem>();
        public static List<PriProItem> PriPros = new List<PriProItem>();
        public static List<BookmarkItem> Bookmarks = new List<BookmarkItem>();
        public static List<MySourcesItem> Sources = new List<MySourcesItem>();

        public const string typestick = "stick", typeicons = "BubbleIcons", typepripro = "BubblePriPro", typeformat = "BubbleFormat",
            typesources = "BubbleMySources", typebookmarks = "BubbleBookmarks", typepaste = "BubblePaste", typeorganizer = "BubbleOrganizer";

        public static int minSize;
        public static int stickThickness;
        public static int stickLength;
        public static int icondist;
        public static Size cmiSize;

        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
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
