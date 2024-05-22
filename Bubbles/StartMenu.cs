using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "stixstartmenu.htm");

            toolTip1.SetToolTip(stxIcons, Utils.getString("StixIcons.tooltip"));
            toolTip1.SetToolTip(stxTaskInfo, Utils.getString("StixTaskInfo.tooltip"));
            toolTip1.SetToolTip(stxMapNavigator, Utils.getString("StixMapNavigator.tooltip") + 
                Utils.getString("StixMapNavigator.tooltip2"));
            toolTip1.SetToolTip(stxTools, Utils.getString("StixTools.tooltip"));
            toolTip1.SetToolTip(stxAddTopics, Utils.getString("StixAddTopic.tooltip"));
            toolTip1.SetToolTip(stxTextOps, Utils.getString("StixTextOps.tooltip"));
            toolTip1.SetToolTip(stxFormat, Utils.getString("StixFormat.tooltip"));
            toolTip1.SetToolTip(boxResources, Utils.getString("Box.Resources"));
            toolTip1.SetToolTip(boxBookmarks, Utils.getString("Box.Bookmarks"));
            toolTip1.SetToolTip(boxSources, Utils.getString("Box.Links"));
            toolTip1.SetToolTip(OmniRec, Utils.getString("Box.OmniSound"));
            toolTip1.SetToolTip(Stickers, Utils.getString("stickers.contextmenu.stickers"));

            cm_show.Text = Utils.getString("startmenu.contextmenu.show");
            cm_hide.Text = Utils.getString("startmenu.contextmenu.hide");
            cm_close.Text = Utils.getString("startmenu.contextmenu.close");
            cm_remember.Text = Utils.getString("startmenu.contextmenu.remember");

            cm_show.ToolTipText = Utils.getString("startmenu.contextmenu.show.tooltip");
            cm_hide.ToolTipText = Utils.getString("startmenu.contextmenu.hide.tooltip");
            cm_remember.ToolTipText = Utils.getString("startmenu.contextmenu.remember.tooltip");

            cm_settings.Text = Utils.getString("SettingsDlg.Title");
            cm_help.Text = Utils.getString("button.help");
            cm_about.Text = Utils.getString("startmenu.contextmenu.about");
            cm_autoclose.Text = Utils.getString("startmenu.contextmenu.autohide");
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
            StixUtils.SetContextMenuImage(cm_help, "ms_chm.png");
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
                cmsTools.Close();
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
                    Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "stixstartmenu.htm");
                    break;
                case "cm_settings":
                    using (SettingsDlg dlg = new SettingsDlg())
                        dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                    break;
                case "cm_about":
                    Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "About.htm");
                    break;

                //Bulk operations
                case "cm_show":
                    foreach (var stick in StixMain.STICKS.Values)
                    {
                        if (!stick.Visible) stick.Show();
                    }
                    break;
                case "cm_hide":
                    foreach (var stick in StixMain.STICKS.Values)
                        stick.Hide();
                    break;
                case "cm_close":
                    foreach (var stick in StixMain.STICKS.Values)
                    {
                        stick.Close(); stick.Dispose();
                    }
                    StixMain.STICKS.Clear();
                    break;
                case "cm_remember":
                    foreach (var stick in StixMain.STICKS)
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
            if (sender == null || e.Button == MouseButtons.Left)
            {
                BaseIcon_MouseClick(stxMapNavigator, null);
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (StixMain.m_MapNavigatorDlg == null)
                {
                    StixMain.m_MapNavigatorDlg = new MapNavigatorDlg();

                    // Get navigation window location
                    if (StixMain.m_MapNavigatorDlg.Location.IsEmpty)
                    {
                        StixMain.m_MapNavigatorDlg.Location =
                            new Point(StixMain.OmniSticksButton.Location.X, this.Bottom);
                    }

                    StixMain.m_MapNavigatorDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
                StixMain.m_MapNavigatorDlg.Init();
            }
        }

        private void StxTools_Click(object sender, EventArgs e)
        {
            BaseIcon_MouseClick(stxTools, null);
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
            if (StixMain.m_Bookmarks == null || StixMain.m_Bookmarks.IsDisposed)
                StixMain.m_Bookmarks = new BookmarksDlg();

            if (StixMain.m_Bookmarks.Visible)
                StixMain.m_Bookmarks.WindowState = FormWindowState.Normal;
            else
            {
                if (StixMain.m_Bookmarks.Location.IsEmpty)
                {
                    StixMain.m_Bookmarks.Location =
                        new Point(StixMain.OmniSticksButton.Location.X, this.Bottom);
                }
                StixMain.m_Bookmarks.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void BoxResources_Click(object sender, EventArgs e)
        {
            if (StixMain.m_Resources == null)
                StixMain.m_Resources = new ResourcesDlg();

            StixMain.m_Resources.InitCurrentMapResources();

            if (StixMain.m_Resources.Visible)
                StixMain.m_Resources.WindowState = FormWindowState.Normal;
            else
            {
                if (StixMain.m_Resources.Location.IsEmpty)
                {
                    StixMain.m_Resources.Location =
                        new Point(StixMain.OmniSticksButton.Location.X, this.Bottom);
                }
                StixMain.m_Resources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void OmniRec_Click(object sender, EventArgs e)
        {
            if (StixMain.m_OmniSound.Visible)
                StixMain.m_OmniSound.WindowState = FormWindowState.Normal;
            else
            {
                if (StixMain.m_OmniSound.Location.IsEmpty)
                {
                    StixMain.m_OmniSound.Location =
                        new Point(StixMain.OmniSticksButton.Location.X, this.Bottom);
                }
                StixMain.m_OmniSound.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void BoxSources_Click(object sender, EventArgs e)
        {
            StixMain.m_AllSources = null;
            StixMain.m_AllSources = new LinksDlg();
            StixMain.m_AllSources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
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
            string stickType; string defaultName;
            PictureBox pb = sender as PictureBox;

            if (pb == stxIcons)
            {
                stickType = StixUtils.typeicons;
                defaultName = Utils.getString("StixIcons.tooltip");
                if (cmsIcons.Items.Count > 0 && startId == 0)
                {
                    cmsIcons.Show(Cursor.Position); return;
                }
            }
            else if (pb == stxTaskInfo)
            {
                stickType = StixUtils.typetaskinfo;
                defaultName = Utils.getString("StixTaskInfo.tooltip");
            }
            else if (pb == stxTools)
            {
                stickType = StixUtils.typetools;
                defaultName = Utils.getString("StixTools.tooltip");
                if (cmsTools.Items.Count > 0 && startId == 0)
                {
                    cmsTools.Show(Cursor.Position); return;
                }
            }
            else if (pb == stxMapNavigator)
            {
                stickType = StixUtils.typemapnavigator;
                defaultName = Utils.getString("StixMapNavigator.tooltip");
            }
            else if (pb == stxFormat)
            {
                stickType = StixUtils.typeformat;
                defaultName = Utils.getString("StixFormat.tooltip");
            }
            else if (pb == stxAddTopics)
            {
                stickType = StixUtils.typeaddtopic;
                defaultName = Utils.getString("StixAddTopic.tooltip");
            }
            else if (pb == stxTextOps)
            {
                stickType = StixUtils.typetextops;
                defaultName = Utils.getString("StixTextOps.tooltip");
            }
            else if (pb.Name == "Organizer")
            {
                stickType = StixUtils.typeorganizer;
                defaultName = Utils.getString("StixOrganizer.tooltip");
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
                    form = new StixIcons(id, orientation, name); break;
                case StixUtils.typetaskinfo:
                    form = new StixTaskInfo(id, orientation, name); break;
                case StixUtils.typetools:
                    form = new StixTools(id, orientation, name); break;
                case StixUtils.typemapnavigator:
                    form = new StixMapNavigator(id, orientation, name); break;
                case StixUtils.typeformat:
                    form = new StixFormat(id, orientation, name); break;
                case StixUtils.typeaddtopic:
                    form = new StixAddTopic(id, orientation, name); break;
                case StixUtils.typetextops:
                    form = new StixTextOps(id, orientation, name); break;
                case StixUtils.typeorganizer:
                    form = new StixOrganizer(id, orientation, name); break;
            }

            form.Location = GetStickLocation(location, form.Size);
            StixMain.STICKS.Add(id, form);
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
                thisLocation = new Point(StixMain.OmniSticksButton.Location.X, this.Bottom);
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
            foreach (var stick in StixMain.STICKS)
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
                    name = Utils.getString(type + ".tooltip");
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
            if (type == "" || type == StixUtils.typetools)
            {
                if (cmsTools.Items.Count > 0) cmsTools.Items.Clear();
                cmsTools = GetSticks(StixUtils.typetools, cmsTools);
                if (cmsTools.Items.Count > 0)
                {
                    stxTools.ContextMenuStrip = cmsTools;
                    cmsTools.ItemClicked += cms_ItemClicked;
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
                case StixUtils.typetools:
                    StxIcon_MouseClick(stxTools, null); break;
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
        public ContextMenuStrip cmsTools = new ContextMenuStrip() { ShowImageMargin = false };

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
