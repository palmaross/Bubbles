using PRAManager;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class MainMenuDlg : Form
    {
        public MainMenuDlg()
        {
            InitializeComponent();

            lblIcons.Text = Utils.getString("stickIcons.name");
            lblPriPro.Text = Utils.getString("stickPriPro.name");
            lblOrganizer.Text = Utils.getString("stickOrganizer.name");
            lblBookmarks.Text = Utils.getString("stickBookmarks.name");
            lblMySources.Text = Utils.getString("stickSources.name");
            lblCopyPaste.Text = Utils.getString("stickPaste.name");
            lblFormat.Text = Utils.getString("stickFormat.name");
            toolTip1.SetToolTip(pBulkOperations, Utils.getString("sticks.bulkoperations"));

            lblStickers.Text = Utils.getString("BubblesMenuDlg.lblStickers.text");

            Location = new Point(Cursor.Position.X, MMUtils.MindManager.Top + MMUtils.MindManager.Height - this.Height - Settings.Height);

            mwIcons = new Bitmap(Utils.ImagesPath + "mwIcons.png");
            mwPriPro = new Bitmap(Utils.ImagesPath + "mwPriPro.png");
            mwBookmarks = new Bitmap(Utils.ImagesPath + "mwBookmarks.png");
            mwCopyPaste = new Bitmap(Utils.ImagesPath + "mwCopyPaste.png");
            mwSources = new Bitmap(Utils.ImagesPath + "mwSources.png");
            mwFormat = new Bitmap(Utils.ImagesPath + "mwFormat.png");
            mwFormatActive = new Bitmap(Utils.ImagesPath + "mwFormatActive.png");
            mwOrganizer = new Bitmap(Utils.ImagesPath + "mwOrganizer.png");

            StickUtils.minSize = pMinSize.Width;
            StickUtils.stickThickness = pMinSize.Height;
            StickUtils.icondist = pIconDist.Width;
            StickUtils.cmiSize = p2.Size;

            // Bulk operations context menu
            BulkOperations.Items["BO_hide"].ToolTipText = Utils.getString("contextmenu.hide.tooltip");
            BulkOperations.Items["BO_show"].ToolTipText = Utils.getString("contextmenu.show.tooltip");
            BulkOperations.Items["BO_remember"].ToolTipText = Utils.getString("contextmenu.remember.tooltip");
            BulkOperations.Items["BO_align"].Text = Utils.getString("bulkoperations.contextmenu.align");
            BulkOperations.Items["BO_configuration"].Text = Utils.getString("bulkoperations.contextmenu.configuration");
            BulkOperations.Items["BO_help"].Text = Utils.getString("float_icons.contextmenu.help");

            BulkOperations.ItemClicked += BulkOperations_ItemClicked;
            foreach (ToolStripItem item in BulkOperations.Items.OfType<ToolStripMenuItem>())
                StickUtils.SetContextMenuImage(item, item.Name.Substring(3) + ".png");

            configuration = BulkOperations.Items["BO_configuration"] as ToolStripMenuItem;
            configuration.DropDown.Items.Add(Utils.getString("contextmenu.configuration.save")).Name = "Config_create";
            configuration.DropDown.Items.Add(Utils.getString("contextmenu.configuration.manage")).Name = "Config_manage";
            configuration.DropDown.ItemClicked += BulkOperations_ItemClicked;

            // Fill configuration-to-run list in the Configuration sub menu
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from CONFIGS order by name");
                if (dt.Rows.Count > 0)
                {
                    var label = configuration.DropDown.Items.Add(Utils.getString("contextmenu.configuration.run"));
                    label.Font = new Font(label.Font, FontStyle.Bold);

                    foreach (DataRow dr in dt.Rows)
                        configuration.DropDown.Items.Add(dr["name"].ToString()).Tag = Convert.ToInt32(dr["id"]);
                }
            }

            this.Deactivate += This_Deactivate;

            // Context menu for multiple sticks for button
            AddSelectMenu();
        }

        private void This_Deactivate(object sender, EventArgs e)
        {
            if (!keepmenu)
                this.Hide();
        }

        /// <summary>
        /// Create Context Menu for multiple sticks per button
        /// </summary>
        /// <param name="type">Icons, PriPro or MySources. If "", then all</param>
        public void AddSelectMenu(string type = "")
        {
            if (type == "" || type == StickUtils.typeicons)
            {
                if (cmsIcons.Items.Count > 0) cmsIcons.Items.Clear();
                cmsIcons = GetSticks(StickUtils.typeicons, cmsIcons);
                if (cmsIcons.Items.Count > 0)
                {
                    pIcons.ContextMenuStrip = cmsIcons;
                    cmsIcons.ItemClicked += cms_ItemClicked;
                }
            }
            if (type == "" || type == StickUtils.typepripro)
            {
                if (cmsPriPro.Items.Count > 0) cmsPriPro.Items.Clear();
                cmsPriPro = GetSticks(StickUtils.typepripro, cmsPriPro);
                if (cmsPriPro.Items.Count > 0)
                {
                    PriPro.ContextMenuStrip = cmsPriPro;
                    cmsPriPro.ItemClicked += cms_ItemClicked;
                }
            }
            if (type == "" || type == StickUtils.typesources)
            {
                if (cmsMySources.Items.Count > 0) cmsMySources.Items.Clear();
                cmsMySources = GetSticks(StickUtils.typesources, cmsMySources);
                if (cmsMySources.Items.Count > 0)
                {
                    MySources.ContextMenuStrip = cmsMySources;
                    cmsMySources.ItemClicked += cms_ItemClicked;
                }
            }
        }

        public void RenameContextMenuItem(string type, string id, string newname)
        {
            if (type == StickUtils.typeicons && cmsIcons.Items.Count > 1)
            {
                foreach (ToolStripItem item in cmsIcons.Items)
                {
                    string[] tag = item.Tag.ToString().Split(':');
                    if (tag[0] == id)
                    {
                        item.Text = newname;
                        item.Tag = id + ":" + newname;
                        return;
                    }
                }
            }
        }

        private void cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var tsm = e.ClickedItem; if (tsm == null) return;

            string[] parts = tsm.Tag.ToString().Split(':');
            string type = parts[1];
            startId = Convert.ToInt32(parts[0]);

            switch (type)
            {
                case StickUtils.typeicons:
                    MenuIcon_Click(pIcons, null); break;
                case StickUtils.typepripro:
                    MenuIcon_Click(PriPro, null); break;
                case StickUtils.typesources:
                    MenuIcon_Click(MySources, null); break;
            }
        }


        ContextMenuStrip GetSticks(string type, ContextMenuStrip cms)
        {
            using (BubblesDB db = new BubblesDB())
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

        /// <summary>
        /// Get stick from database by its ID
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <returns>0 - Very new stick, -1 - User don't want select a stick, N - stick ID
        /// </returns>
        private int GetStick(string type, int id, ref string position, ref string name)
        {
            using (BubblesDB db = new BubblesDB())
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
                    position = dt.Rows[0]["position"].ToString();
                    name = dt.Rows[0]["name"].ToString();
                    return Convert.ToInt32(dt.Rows[0]["id"]);
                }
                else // more than one stick, and?
                {
                    return -1;
                }
            }
        }
        bool keepmenu = false;

        public void MenuIcon_Click(object sender, EventArgs e)
        {
            string stickType = ""; string defaultName = "";
            PictureBox pb = sender as PictureBox;

            if (pb.Name == "pIcons")
            {
                stickType = StickUtils.typeicons;
                defaultName = Utils.getString("icons.bubble.tooltip");
                if (cmsIcons.Items.Count > 0 && startId == 0)
                {
                    cmsIcons.Show(Cursor.Position); return;
                }
            }
            else if (pb.Name == "PriPro")
            {
                stickType = StickUtils.typepripro;
                defaultName = Utils.getString("pripro.bubble.tooltip");
                if (cmsPriPro.Items.Count > 0 && startId == 0)
                {
                    cmsPriPro.Show(Cursor.Position); return;
                }
            }
            else if (pb.Name == "MySources")
            {
                stickType = StickUtils.typesources;
                defaultName = Utils.getString("mysources.bubble.tooltip");
                if (cmsMySources.Items.Count > 0 && startId == 0)
                {
                    cmsMySources.Show(Cursor.Position); return;
                }
            }
            else if (pb.Name == "Bookmarks")
            {
                stickType = StickUtils.typebookmarks;
                defaultName = Utils.getString("bookmarks.bubble.tooltip");
            }
            else if (pb.Name == "Format")
            {
                stickType = StickUtils.typeformat;
                defaultName = Utils.getString("format.bubble.tooltip");
            }
            else if (pb.Name == "Paste")
            {
                stickType = StickUtils.typepaste;
                defaultName = Utils.getString("copypaste.bubble.tooltip");
            }
            else if (pb.Name == "Organizer")
            {
                stickType = StickUtils.typeorganizer;
                defaultName = Utils.getString("organizer.bubble.tooltip");
            }

            string orientation = "H0", location = "", name = ""; int id = startId; // "H0" - Horizontal&Not collapsed

            if (StickClicked(stickType, ref orientation, ref location, ref name, ref id) == 2)
                return; // stick already running, or stick troubles

            if (name == "") name = defaultName;

            Form form = null;
            switch (stickType)
            {
                case StickUtils.typeicons:
                    form = new BubbleIcons(id, orientation, name); break;
                case StickUtils.typepripro:
                    form = new BubblePriPro(id, orientation, name); break;
                case StickUtils.typesources:
                    form = new BubbleMySources(id, orientation, name); break;
                case StickUtils.typebookmarks:
                    form = new BubbleBookmarks(id, orientation, name); break;
                case StickUtils.typeformat:
                    form = new BubbleFormat(id, orientation, name); break;
                case StickUtils.typepaste:
                    form = new BubblePaste(id, orientation, name); break;
                case StickUtils.typeorganizer:
                    form = new BubbleOrganizer(id, orientation, name); break;
            }

            form.Location = GetStickLocation(location);
            BubblesButton.STICKS.Add(id, form);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        /// <summary>
        /// Get stick parameters
        /// </summary>
        /// <returns>0 - stick troubles, don't run, 1 - stick ok, run it, 2 - stick already runned</returns>
        int StickClicked(string type, ref string orientation, ref string location, ref string name, ref int id)
        {
            string position = "";
            id = GetStick(type, id, ref position, ref name);

            // If stick is running, show it (if it is hidden) or tell user that it is already running
            foreach (var stick in BubblesButton.STICKS)
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

            keepmenu = false; startId = 0;

            if (id == 0) // The very first stick
            {
                using (BubblesDB db = new BubblesDB())
                {
                    // create "My Icons" stick
                    name = Utils.getString(type + ".bubble.tooltip");
                    id = Utils.StickID();
                    db.AddStick(id, name, type, 0, "", 0);
                }
            }

            orientation = "H0"; location = ""; // "H0" - Horizontal&Not collapsed
            if (position != "")
            {
                string[] parts = position.Split('#');
                orientation = parts[0];
                location = parts[1];
            }
            return 1;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            using (SettingsDlg dlg = new SettingsDlg())
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        private void Help_Click(object sender, EventArgs e)
        {

        }

        private void pSticker_Click(object sender, EventArgs e)
        {
            StickerDummy form = new StickerDummy(
                new StickerItem(0, "\nHello!", "#515151", "#B9B9F9", "Verdana", 9, 0, "0",
                "hello1.png:" + 
                StickerDummy.DummyStickerImageX + ":" + StickerDummy.DummyStickerImageY + ":" +
                pSticker.Width + ":" + pSticker.Height, "center", "sticker"), new Point(0, 0));

            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        public Point GetStickLocation(string location)
        {
            Point thisLocation = new Point();

            // Example: location = "5120,0:5126,363;0,0:2,358"
            // 5120,0 - screen1.Location, 5126,363 - this.Location on the screen1
            // 0,0 - screen2.Location, 2,358 - this.Location on the screen2

            if (String.IsNullOrEmpty(location))
                thisLocation = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
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
                    !Utils.StickIsOnScreen(thisLocation, this.Size))
                    thisLocation = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            }
            return thisLocation;
        }

        private void pBulkOperations_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in BulkOperations.Items)
            {
                item.Visible = true; item.Enabled = true;
            }
            foreach (ToolStripItem item in configuration.DropDown.Items)
            {
                item.Visible = true; item.Enabled = true;
            }

            int total = BubblesButton.STICKS.Count;
            if (total == 0)
            {
                foreach (ToolStripItem item in BulkOperations.Items)
                    item.Enabled = false;
                foreach (ToolStripItem item in configuration.DropDown.Items)
                    item.Enabled = true;

                BulkOperations.Items["BO_help"].Enabled = true;
                BulkOperations.Items["BO_configuration"].Enabled = true;
                configuration.DropDown.Items["Config_create"].Enabled = false;
                BulkOperations.Show(Cursor.Position);
                return;
            }

            int visibles = 0, invisibles = 0;
            foreach (var stick in BubblesButton.STICKS.Values)
            {
                if (stick.Visible) visibles++; else invisibles++;
            }

            string cVisibles = " (" + visibles + ")", 
                   cInvisibles = " (" + invisibles + ")",
                   cTotal = " (" + total + ")";

            BulkOperations.Items["BO_hide"].Text = Utils.getString("bulkoperations.contextmenu.hide");
            BulkOperations.Items["BO_show"].Text = Utils.getString("bulkoperations.contextmenu.show");
            BulkOperations.Items["BO_close"].Text = Utils.getString("bulkoperations.contextmenu.close");
            BulkOperations.Items["BO_collapse"].Text = Utils.getString("bulkoperations.contextmenu.collapse");
            BulkOperations.Items["BO_expand"].Text = Utils.getString("bulkoperations.contextmenu.expand");
            BulkOperations.Items["BO_remember"].Text = Utils.getString("bulkoperations.contextmenu.remember");

            BulkOperations.Items["BO_hide"].Enabled = visibles > 0;
            BulkOperations.Items["BO_hide"].Text += cVisibles;

            BulkOperations.Items["BO_show"].Enabled = invisibles > 0;
            BulkOperations.Items["BO_show"].Text += cInvisibles;

            BulkOperations.Items["BO_close"].Enabled = total > 0;
            BulkOperations.Items["BO_close"].Text += cTotal;

            BulkOperations.Items["BO_collapse"].Enabled = visibles > 0;
            BulkOperations.Items["BO_collapse"].Text += cVisibles;

            BulkOperations.Items["BO_expand"].Enabled = visibles > 0;
            BulkOperations.Items["BO_expand"].Text += cVisibles;

            BulkOperations.Items["BO_remember"].Enabled = visibles > 0;
            BulkOperations.Items["BO_remember"].Text += cVisibles;

            BulkOperations.Show(Cursor.Position);
        }

        private void BulkOperations_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "BO_show":
                    foreach (var stick in BubblesButton.STICKS.Values) {
                        if (!stick.Visible) stick.Show(); }
                    break;
                case "BO_hide":
                    foreach (var stick in BubblesButton.STICKS.Values)
                        stick.Hide();
                    break;
                case "BO_close":
                    foreach (var stick in BubblesButton.STICKS.Values) {
                        stick.Close(); stick.Dispose(); }
                    BubblesButton.STICKS.Clear();
                    break;
                case "BO_collapse":
                case "BO_expand":
                    bool collapse = true; bool expand = false;
                    if (e.ClickedItem.Name == "BO_expand") { collapse = false; expand = true; };

                    foreach (var stick in BubblesButton.STICKS.Values)
                    {
                        switch (stick.Name)
                        {
                            case StickUtils.typeicons:
                                (stick as BubbleIcons).Collapse(collapse, expand);
                                break;
                            case StickUtils.typepripro:
                                (stick as BubblePriPro).Collapse(collapse, expand);
                                break;
                            case StickUtils.typeformat:
                                (stick as BubbleFormat).Collapse(collapse, expand);
                                break;
                            case StickUtils.typesources:
                                (stick as BubbleMySources).Collapse(collapse, expand);
                                break;
                            case StickUtils.typebookmarks:
                                (stick as BubbleBookmarks).Collapse(collapse, expand);
                                break;
                            case StickUtils.typepaste:
                                (stick as BubblePaste).Collapse(collapse, expand);
                                break;
                            case StickUtils.typeorganizer:
                                (stick as BubbleOrganizer).Collapse(collapse, expand);
                                break;
                        }
                    }
                    break;
                case "BO_remember":
                    foreach (var stick in BubblesButton.STICKS)
                    {
                        string orientation = "H";
                        if (stick.Value.Width < stick.Value.Height) orientation = "V";

                        bool collapsed = stick.Value.Width == StickUtils.minSize;

                        StickUtils.SaveStick(stick.Value.Bounds, stick.Key, orientation, collapsed);
                    }
                    break;
                case "BO_align":
                    using (AlignSticksDlg dlg = new AlignSticksDlg())
                    {
                        dlg.ShowDialog();
                    }
                    break;
                case "Config_manage":
                    using (ManageConfigsDlg dlg = new ManageConfigsDlg())
                        dlg.ShowDialog();
;                    break;
                case "BO_help":
                    try { Process.Start(Utils.dllPath + "Sticks.chm"); } catch { }
                    break;
            }
        }

        public Image mwIcons, mwPriPro, mwBookmarks, mwCopyPaste, 
            mwSources, mwFormat, mwFormatActive, mwOrganizer;

        public int startId = 0;

        public ContextMenuStrip cmsIcons = new ContextMenuStrip() { ShowImageMargin = false };
        public ContextMenuStrip cmsPriPro = new ContextMenuStrip() { ShowImageMargin = false };
        public ContextMenuStrip cmsMySources = new ContextMenuStrip() { ShowImageMargin = false };
        public ToolStripMenuItem configuration;
    }
}
