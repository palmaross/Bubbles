using Bubbles.Properties;
using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class StixBase : Form
    {
        public StixBase()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "stixbase.htm");

            toolTip1.SetToolTip(stxIcons, Utils.getString("BubbleIcons.bubble.tooltip"));
            toolTip1.SetToolTip(stxTaskInfo, Utils.getString("BubbleTaskInfo.bubble.tooltip"));
            toolTip1.SetToolTip(stxBookmarks, Utils.getString("BubbleBookmarks.bubble.tooltip"));
            toolTip1.SetToolTip(stxSources, Utils.getString("BubbleMySources.bubble.tooltip"));
            toolTip1.SetToolTip(stxAddTopics, Utils.getString("BubbleAddTopic.bubble.tooltip"));
            toolTip1.SetToolTip(stxTextOps, Utils.getString("BubbleTextOps.bubble.tooltip"));
            toolTip1.SetToolTip(stxFormat, Utils.getString("BubbleFormat.bubble.tooltip"));
            toolTip1.SetToolTip(boxResources, Utils.getString("Box.Resources"));
            toolTip1.SetToolTip(boxBookmarks, Utils.getString("Box.Bookmarks"));
            toolTip1.SetToolTip(boxSources, Utils.getString("Box.Sources"));
            toolTip1.SetToolTip(Stickers, Utils.getString("stickers.contextmenu.stickers"));

            cm_show.Text = Utils.getString("bulkoperations.contextmenu.show");
            cm_hide.Text = Utils.getString("bulkoperations.contextmenu.hide");
            cm_close.Text = Utils.getString("bulkoperations.contextmenu.close");
            cm_remember.Text = Utils.getString("bulkoperations.contextmenu.remember");

            cm_show.ToolTipText = Utils.getString("contextmenu.show.tooltip");
            cm_hide.ToolTipText = Utils.getString("contextmenu.hide.tooltip");
            cm_close.ToolTipText = Utils.getString("bulkoperations.contextmenu.close");
            cm_remember.ToolTipText = Utils.getString("contextmenu.remember.tooltip");

            cm_settings.Text = Utils.getString("SettingsDlg.Title");
            cm_help.Text = Utils.getString("button.help");
            cm_about.Text = Utils.getString("stixbase.contextmenu.about");
            cm_autoclose.Text = Utils.getString("stixbase.contextmenu.autohide");
            cm_closemenu.Text = Utils.getString("button.close");

            StixUtils.cmiSize = p2.Size;

            Color c = ColorTranslator.FromHtml("#e0e5ed");
            this.BackColor = c;
            c = ColorTranslator.FromHtml("#c9d7eb");
            panelBoxes.BackColor = c;
            foreach (PictureBox pb in panelBoxes.Controls.OfType<PictureBox>())
                pb.BackColor = c;

            cmsManage.ItemClicked += ContextMenu_ItemClicked;
            StixUtils.SetContextMenuImage(cm_show, "show.png");
            StixUtils.SetContextMenuImage(cm_hide, "hide.png");
            StixUtils.SetContextMenuImage(cm_close, "deleteall.png");
            StixUtils.SetContextMenuImage(cm_remember, "remember.png");
            StixUtils.SetContextMenuImage(cm_settings, "manage.png");
            StixUtils.SetContextMenuImage(cm_help, "chm.png");
            StixUtils.SetContextMenuImage(cm_about, "help.png");
            StixUtils.SetContextMenuImage(cm_autoclose, "check.png");
            StixUtils.SetContextMenuImage(cm_closemenu, "deleteStick.png");
            cm_autoclose.Tag = "auto";

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.Deactivate += This_Deactivate;

            // Apply scale factor
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_StixBase", "100"));
            ScaleStick(100F, scaleFactor);

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            try
            {
                // Works only on Windows 11!
                DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            }
            catch { }

            this.MouseDown += Move_Stick;
            panelBoxes.MouseDown += Move_Stick;

            // Context menu for multiple sticks for button
            AddSelectMenu();
        }

        private void Move_Stick(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void This_Deactivate(object sender, EventArgs e)
        {
            if (cm_autoclose.Tag.ToString() == "auto")
            {
                cmsManage.Close();
                cmsIcons.Close();
                cmsMySources.Close();
                this.Hide();
            }
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 300) return;

            float scale = 100F / fromScale;
            if (scale != 1)
                this.Scale(new SizeF(scale, scale)); // reset to 100%

            this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale
            scaleFactor = toScale;
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "cm_help":
                    Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "stixbase.htm");
                    break;
                case "cm_settings":
                    using (SettingsDlg dlg = new SettingsDlg())
                        dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                    break;
                case "cm_about":

                    break;

                //Bulk operations
                case "cm_show":
                    foreach (var stick in StixButton.STICKS.Values)
                    {
                        if (!stick.Visible) stick.Show();
                    }
                    break;
                case "cm_hide":
                    foreach (var stick in StixButton.STICKS.Values)
                        stick.Hide();
                    break;
                case "cm_close":
                    foreach (var stick in StixButton.STICKS.Values)
                    {
                        stick.Close(); stick.Dispose();
                    }
                    StixButton.STICKS.Clear();
                    break;
                case "cm_remember":
                    foreach (var stick in StixButton.STICKS)
                    {
                        string orientation = "H";

                        if (stick.Value.Width < stick.Value.Height) orientation = "V";

                        StixUtils.SaveStick(stick.Value.Bounds, stick.Key, orientation);
                    }
                    break;
                case "cm_autoclose":
                    if (cm_autoclose.Tag.ToString() == "auto")
                    {
                        cm_autoclose.Image = Image.FromFile(Utils.ImagesPath + "uncheck.png");
                        cm_autoclose.Tag = "manual";
                    }
                    else
                    {
                        cm_autoclose.Image = Image.FromFile(Utils.ImagesPath + "check.png");
                        cm_autoclose.Tag = "auto";
                    }
                    break;
                case "cm_closemenu":
                    cmsManage.Close();
                    this.Hide();
                    break;
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsManage.Items)
                item.Visible = true;

            cmsManage.Show(Cursor.Position);
        }

        private void StxIcon_MouseClick(object sender, MouseEventArgs e)
        {
            BaseIcon_MouseClick(stxIcons, null);
        }

        private void StxTaskInfo_MouseClick(object sender, MouseEventArgs e)
        {
            BaseIcon_MouseClick(stxTaskInfo, null);
        }

        private void StxBookmarks_MouseClick(object sender, MouseEventArgs e)
        {
            BaseIcon_MouseClick(stxBookmarks, null);
        }

        private void StxSources_Click(object sender, EventArgs e)
        {
            BaseIcon_MouseClick(stxSources, null);
        }

        private void StxAddTopic_Click(object sender, EventArgs e)
        {
            BaseIcon_MouseClick(stxAddTopics, null);
        }

        private void StxTextOps_Click(object sender, EventArgs e)
        {
            BaseIcon_MouseClick(stxTextOps, null);
        }

        private void StxFormat_Click(object sender, EventArgs e)
        {
            BaseIcon_MouseClick(stxFormat, null);
        }

        private void BoxBookmarks_Click(object sender, EventArgs e)
        {
            if (StixButton.m_BookmarkList == null)
                StixButton.m_BookmarkList = new BookmarkListDlg();

            StixButton.m_BookmarkList.Init();

            if (StixButton.m_BookmarkList.Visible)
                StixButton.m_BookmarkList.WindowState = FormWindowState.Normal;
            else
            {
                if (StixButton.m_BookmarkList.Location.IsEmpty)
                {
                    StixButton.m_BookmarkList.Location =
                        new Point(StixButton.OmniSticksButton.Location.X, this.Bottom);
                }
                StixButton.m_BookmarkList.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void BoxResources_Click(object sender, EventArgs e)
        {
            if (StixButton.m_Resources == null)
                StixButton.m_Resources = new ResourcesDlg();

            StixButton.m_Resources.InitCurrentMapResources();

            if (StixButton.m_Resources.Visible)
                StixButton.m_Resources.WindowState = FormWindowState.Normal;
            else
            {
                if (StixButton.m_Resources.Location.IsEmpty)
                {
                    StixButton.m_Resources.Location =
                        new Point(StixButton.OmniSticksButton.Location.X, this.Bottom);
                }
                StixButton.m_Resources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void BoxSources_Click(object sender, EventArgs e)
        {
            StixButton.m_AllSources = null;
            StixButton.m_AllSources = new AllSourcesDlg();
            StixButton.m_AllSources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        private void Stickers_MouseClick(object sender, MouseEventArgs e)
        {
            StickerDummy form = new StickerDummy(
                new StickerItem(0, "\nHello!", "#515151", "#B9B9F9", "Verdana", 9, 0, "0",
                "hello1.png:" +
                StickerDummy.DummyStickerImageX + ":" + StickerDummy.DummyStickerImageY + ":" +
                Stickers.Width + ":" + Stickers.Height, "center", "sticker"), new Point(0, 0));

            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        public void BaseIcon_MouseClick(object sender, MouseEventArgs e)
        {
            string stickType = ""; string defaultName = "";
            PictureBox pb = sender as PictureBox;

            if (pb.Name == "stxIcons")
            {
                stickType = StixUtils.typeicons;
                defaultName = Utils.getString("BubbleIcons.bubble.tooltip");
                if (cmsIcons.Items.Count > 0 && startId == 0)
                {
                    cmsIcons.Show(Cursor.Position); return;
                }
            }
            else if (pb.Name == "stxTaskInfo")
            {
                stickType = StixUtils.typetaskinfo;
                defaultName = Utils.getString("BubbleTaskInfo.bubble.tooltip");
            }
            else if (pb.Name == "stxSources")
            {
                stickType = StixUtils.typesources;
                defaultName = Utils.getString("BubbleMySources.bubble.tooltip");
                if (cmsMySources.Items.Count > 0 && startId == 0)
                {
                    cmsMySources.Show(Cursor.Position); return;
                }
            }
            else if (pb.Name == "stxBookmarks")
            {
                stickType = StixUtils.typebookmarks;
                defaultName = Utils.getString("BubbleBookmarks.bubble.tooltip");
            }
            else if (pb.Name == "stxFormat")
            {
                stickType = StixUtils.typeformat;
                defaultName = Utils.getString("BubbleFormat.bubble.tooltip");
            }
            else if (pb.Name == "stxAddTopics")
            {
                stickType = StixUtils.typeaddtopic;
                defaultName = Utils.getString("BubbleAddTopic.bubble.tooltip");
            }
            else if (pb.Name == "stxTextOps")
            {
                stickType = StixUtils.typetextops;
                defaultName = Utils.getString("BubbleTextOps.bubble.tooltip");
            }
            else if (pb.Name == "Organizer")
            {
                stickType = StixUtils.typeorganizer;
                defaultName = Utils.getString("BubbleOrganizer.bubble.tooltip");
            }
            else return;

            string orientation = "H", location = "", name = ""; int id = startId;

            if (StickClicked(stickType, ref orientation, ref location, ref name, ref id) == 2)
            {
                startId = 0; return; // stick already running, or stick troubles
            }

            if (name == "") name = defaultName;

            Form form = null;
            switch (stickType)
            {
                case StixUtils.typeicons:
                    form = new BubbleIcons(id, orientation, name); break;
                case StixUtils.typetaskinfo:
                    form = new BubbleTaskInfo(id, orientation, name); break;
                case StixUtils.typesources:
                    form = new BubbleSources(id, orientation, name); break;
                case StixUtils.typebookmarks:
                    form = new BubbleBookmarks(id, orientation, name); break;
                case StixUtils.typeformat:
                    form = new BubbleFormat(id, orientation, name); break;
                case StixUtils.typeaddtopic:
                    form = new BubbleAddTopic(id, orientation, name); break;
                case StixUtils.typetextops:
                    form = new BubbleTextOps(id, orientation, name); break;
                case StixUtils.typeorganizer:
                    form = new BubbleOrganizer(id, orientation, name); break;
            }

            form.Location = GetStickLocation(location, form.Size);
            StixButton.STICKS.Add(id, form);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            StixUtils.ActivateMindManager();
        }

        public Point GetStickLocation(string location, Size size)
        {
            Point thisLocation = new Point();

            // Example: location = "5120,0:5126,363;0,0:2,358"
            // 5120,0 - screen1.Location, 5126,363 - this.Location on the screen1
            // 0,0 - screen2.Location, 2,358 - this.Location on the screen2

            if (String.IsNullOrEmpty(location))
                thisLocation = new Point(StixButton.OmniSticksButton.Location.X, this.Bottom);
            else
            {
                // Location of the screen where center of MindManager is located
                Point rec = Utils.MMScreen(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                    MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);

                string[] xy = location.Split(';'); // get screens
                foreach (string part in xy)
                {
                    if (part.StartsWith(rec.X + "," + rec.Y)) // we found the screen
                    {
                        xy = part.Split(':')[1].Split(','); // get X & Y for this screen
                        int x = Convert.ToInt32(xy[0]);
                        int y = Convert.ToInt32(xy[1]);
                        thisLocation = new Point(x, y);
                    }
                }

                if (thisLocation.X + thisLocation.Y == 0 ||
                    !Utils.StickIsOnScreen(thisLocation, size))
                    thisLocation = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            }
            return thisLocation;
        }

        /// <summary>
        /// Get stick parameters
        /// </summary>
        /// <returns>0 - stick troubles, don't run, 1 - stick ok, run it, 2 - stick already runned</returns>
        int StickClicked(string type, ref string orientation, ref string location, ref string name, ref int id)
        {
            id = GetStick(type, id, ref location, ref orientation, ref name);

            // If stick is running, show it (if it is hidden) or tell user that it is already running
            foreach (var stick in StixButton.STICKS)
            {
                if (stick.Key == id)
                {
                    if (stick.Value.Visible)
                        MessageBox.Show(Utils.getString("sticks.stickalreadyrunning"));
                    else
                        stick.Value.Show();
                    return 2;
                }
            }

            startId = 0;

            if (id == 0) // The very first stick
            {
                using (StixDB db = new StixDB())
                {
                    // create "My Icons" stick
                    name = Utils.getString(type + ".bubble.tooltip");
                    id = Utils.StickID();
                    db.AddStick(id, name, type, 0, "H", "");
                }
            }
            return 1;
        }

        /// <summary>
        /// Get stick from database by its ID
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <returns>0 - Very new stick, -1 - User don't want select a stick, N - stick ID
        /// </returns>
        private int GetStick(string type, int id, ref string location, ref string orientation, ref string name)
        {
            using (StixDB db = new StixDB())
            {
                DataTable dt;
                if (id != 0)
                    dt = db.ExecuteQuery("select * from STICKS where id=" + id + "");
                else
                    dt = db.ExecuteQuery("select * from STICKS where type=`" + type + "`");

                if (dt.Rows.Count == 0) // there are not sticks with this type or id
                    return 0;
                else if (dt.Rows.Count == 1) // only one stick, do not show SelectStickDlg
                {
                    location = dt.Rows[0]["location"].ToString();
                    orientation = dt.Rows[0]["orientation"].ToString();
                    name = dt.Rows[0]["name"].ToString();
                    return Convert.ToInt32(dt.Rows[0]["id"]);
                }
                else // more than one stick, and?
                {
                    return -1;
                }
            }
        }

        public void RenameContextMenuItem(string type, string id, string newname)
        {
            if (type == StixUtils.typeicons && cmsIcons.Items.Count > 1)
            {
                foreach (ToolStripItem item in cmsIcons.Items)
                {
                    string[] tag = item.Tag.ToString().Split(':');
                    if (tag[0] == id)
                    {
                        item.Text = newname;
                        item.Tag = id + ":" + type;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Create Context Menu for multiple sticks per button
        /// </summary>
        /// <param name="type">Icons or MySources. If "", then all</param>
        public void AddSelectMenu(string type = "")
        {
            if (type == "" || type == StixUtils.typeicons)
            {
                if (cmsIcons.Items.Count > 0) cmsIcons.Items.Clear();
                cmsIcons = GetSticks(StixUtils.typeicons, cmsIcons);
                if (cmsIcons.Items.Count > 0)
                {
                    stxIcons.ContextMenuStrip = cmsIcons;
                    cmsIcons.ItemClicked += cms_ItemClicked;
                }
            }
            if (type == "" || type == StixUtils.typesources)
            {
                if (cmsMySources.Items.Count > 0) cmsMySources.Items.Clear();
                cmsMySources = GetSticks(StixUtils.typesources, cmsMySources);
                if (cmsMySources.Items.Count > 0)
                {
                    stxSources.ContextMenuStrip = cmsMySources;
                    cmsMySources.ItemClicked += cms_ItemClicked;
                }
            }
        }

        private void cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var tsi = e.ClickedItem; if (tsi == null) return;

            string[] parts = tsi.Tag.ToString().Split(':');
            string type = parts[1];
            startId = Convert.ToInt32(parts[0]);

            switch (type)
            {
                case StixUtils.typeicons:
                    StxIcon_MouseClick(stxIcons, null); break;
                case StixUtils.typesources:
                    StxIcon_MouseClick(stxSources, null); break;
            }
        }

        ContextMenuStrip GetSticks(string type, ContextMenuStrip cms)
        {
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKS where type=`" + type + "`");

                if (dt.Rows.Count > 1)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ToolStripMenuItem tsm = new ToolStripMenuItem(row["name"].ToString());
                        tsm.Tag = row["id"] + ":" + type;
                        cms.Items.Add(tsm);
                    }
                }
            }
            return cms;
        }

        public int startId = 0;

        public ContextMenuStrip cmsIcons = new ContextMenuStrip() { ShowImageMargin = false };
        public ContextMenuStrip cmsMySources = new ContextMenuStrip() { ShowImageMargin = false };

        public float scaleFactor = 100;

        // Rounded corners
        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        // Copied from dwmapi.h
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute,
            ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
