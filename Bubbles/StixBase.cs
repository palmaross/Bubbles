using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;

namespace Bubbles
{
    internal partial class StixBase : Form
    {
        public StixBase(int ID, string _orientation)
        {
            InitializeComponent();

            thisHeight = this.Height;
            thisWidth = this.Width;

            this.Tag = ID;
            orientation = _orientation; // "H" or "V"

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "stixbase.htm");

            if (orientation == "V")
            {
                orientation = "H"; Rotate();
            }

            toolTip1.SetToolTip(stxIcons, Utils.getString("BubbleIcons.bubble.tooltip"));
            toolTip1.SetToolTip(stxTaskInfo, Utils.getString("BubbleTaskInfo.bubble.tooltip"));
            toolTip1.SetToolTip(stxBookmarks, Utils.getString("BubbleBookmarks.bubble.tooltip"));
            toolTip1.SetToolTip(stxSources, Utils.getString("BubbleMySources.bubble.tooltip"));
            toolTip1.SetToolTip(stxAddTopic, Utils.getString("BubbleAddTopic.bubble.tooltip"));
            toolTip1.SetToolTip(stxTextOps, Utils.getString("BubbleTextOps.bubble.tooltip"));
            toolTip1.SetToolTip(stxFormat, Utils.getString("BubbleFormat.bubble.tooltip"));
            toolTip1.SetToolTip(boxResources, Utils.getString("Box.Resources"));
            toolTip1.SetToolTip(boxBookmarks, Utils.getString("Box.Bookmarks"));
            toolTip1.SetToolTip(boxSources, Utils.getString("Box.Sources"));
            toolTip1.SetToolTip(Stickers, Utils.getString("stickers.contextmenu.mystickers"));

            toolTip1.SetToolTip(pictureHandle, Utils.getString("StixBase.Name"));

            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typebase);
            cmsCommon.ItemClicked += ContextMenu_ItemClicked;

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // To move stix
            this.MouseDown += Move_Stick;
            pictureHandle.MouseDown += Move_Stick;
            pictureHandle.MouseDoubleClick += (sender, e) => { this.Hide(); };

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typetextops);
            this.Paint += this_Paint; // paint the border
            panelBoxes.Paint += PanelBoxes_Paint;
            //panelOther.Paint += PanelOther_Paint;

            // Apply scale factor
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_StixBase", "100"));
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

        private void PanelOther_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, 1, ButtonBorderStyle.Solid,
                Color.Black, 0, ButtonBorderStyle.None,
                Color.Black, 1, ButtonBorderStyle.Solid,
                Color.Black, 0, ButtonBorderStyle.None);
        }

        private void PanelBoxes_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, 1, ButtonBorderStyle.Solid,
                Color.Black, 0, ButtonBorderStyle.None,
                Color.Black, 0, ButtonBorderStyle.Solid,
                Color.Black, 0, ButtonBorderStyle.None);
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            if (scaleFactor < 125) return;
            int width = 1;
            //if (scaleFactor > 200) width = 2;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid);
        }

        private void Move_Stick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "77")
            {
                
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                this.Hide();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "stixbase.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StickUtils.SaveStick(this.Bounds, (int)this.Tag, orientation, false); // todo
            }
            else if (e.ClickedItem.Name == "BS_settings")
            {
                using (SettingsDlg dlg = new SettingsDlg())
                    dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public void Rotate()
        {
            orientation = StickUtils.RotateStick(this, Manage, orientation);

            panelBoxes.Location = new Point(panelBoxes.Location.Y, panelBoxes.Location.X);
            panelBoxes.Size = new Size(panelBoxes.Height, panelBoxes.Width);

            panelOther.Location = new Point(panelOther.Location.Y, panelOther.Location.X);
            panelOther.Size = new Size(panelOther.Height, panelOther.Width);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsCommon.Items)
                item.Visible = true;

            cmsCommon.Show(Cursor.Position);
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
            BaseIcon_MouseClick(stxAddTopic, null);
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

        }

        private void BoxResources_Click(object sender, EventArgs e)
        {

        }

        private void BoxSources_Click(object sender, EventArgs e)
        {

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
                stickType = StickUtils.typeicons;
                defaultName = Utils.getString("BubbleIcons.bubble.tooltip");
                if (cmsIcons.Items.Count > 0 && startId == 0)
                {
                    cmsIcons.Show(Cursor.Position); return;
                }
            }
            else if (pb.Name == "stxTaskInfo")
            {
                stickType = StickUtils.typetaskinfo;
                defaultName = Utils.getString("BubbleTaskInfo.bubble.tooltip");
            }
            else if (pb.Name == "stxSources")
            {
                stickType = StickUtils.typesources;
                defaultName = Utils.getString("BubbleMySources.bubble.tooltip");
                if (cmsMySources.Items.Count > 0 && startId == 0)
                {
                    cmsMySources.Show(Cursor.Position); return;
                }
            }
            else if (pb.Name == "stxBookmarks")
            {
                stickType = StickUtils.typebookmarks;
                defaultName = Utils.getString("BubbleBookmarks.bubble.tooltip");
            }
            else if (pb.Name == "stxFormat")
            {
                stickType = StickUtils.typeformat;
                defaultName = Utils.getString("BubbleFormat.bubble.tooltip");
            }
            else if (pb.Name == "stxAddTopic")
            {
                stickType = StickUtils.typeaddtopic;
                defaultName = Utils.getString("BubbleAddTopic.bubble.tooltip");
            }
            else if (pb.Name == "stxTextOps")
            {
                stickType = StickUtils.typetextops;
                defaultName = Utils.getString("BubbleTextOps.bubble.tooltip");
            }
            else if (pb.Name == "Organizer")
            {
                stickType = StickUtils.typeorganizer;
                defaultName = Utils.getString("BubbleOrganizer.bubble.tooltip");
            }

            string orientation = "H0", location = "", name = ""; int id = startId; // "H0" - Horizontal&Not collapsed

            if (StickClicked(stickType, ref orientation, ref location, ref name, ref id) == 2)
            {
                startId = 0; return; // stick already running, or stick troubles
            }

            if (name == "") name = defaultName;

            Form form = null;
            switch (stickType)
            {
                case StickUtils.typeicons:
                    form = new BubbleIcons(id, orientation, name); break;
                case StickUtils.typetaskinfo:
                    form = new BubbleTaskInfo(id, orientation, name); break;
                case StickUtils.typesources:
                    form = new BubbleMySources(id, orientation, name); break;
                case StickUtils.typebookmarks:
                    form = new BubbleBookmarks(id, orientation, name); break;
                case StickUtils.typeformat:
                    form = new BubbleFormat(id, orientation, name); break;
                case StickUtils.typeaddtopic:
                    form = new BubbleAddTopic(id, orientation, name); break;
                case StickUtils.typetextops:
                    form = new BubbleTextOps(id, orientation, name); break;
                case StickUtils.typeorganizer:
                    form = new BubbleOrganizer(id, orientation, name); break;
            }

            form.Location = GetStickLocation(location, form.Size);
            BubblesButton.STICKS.Add(id, form);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            StickUtils.ActivateMindManager();
        }
        public Point GetStickLocation(string location, Size size)
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

            startId = 0;

            if (id == 0) // The very first stick
            {
                using (StixDB db = new StixDB())
                {
                    // create "My Icons" stick
                    name = Utils.getString(type + ".bubble.tooltip");
                    id = Utils.StickID();
                    db.AddStick(id, name, type, 0, "", 0, 0);
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

        string orientation = "H";

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public int startId = 0;

        public ContextMenuStrip cmsIcons = new ContextMenuStrip() { ShowImageMargin = false };
        public ContextMenuStrip cmsMySources = new ContextMenuStrip() { ShowImageMargin = false };
        public ToolStripMenuItem configuration;

        public float scaleFactor = 100;
        int thisWidth; int thisHeight;
        List<StartControl> thisControls = new List<StartControl>();
    }

    public class StartControl
    {
        public StartControl(Control c, int _width, int _height, int x, int y)
        {
            control = c;
            width = _width;
            height = _height;
            X = x; Y = y;
        }

        public Control control;
        public int width, height, X, Y;
    }
}
