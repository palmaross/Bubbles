using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
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

            lblIcons.Text = Utils.getString("BubblesMenuDlg.lblIcons.text");
            lblPriPro.Text = Utils.getString("BubblesMenuDlg.lblPriPro.text");
            lblNotepad.Text = Utils.getString("BubblesMenuDlg.lblNotepad.text");
            lblBookmarks.Text = Utils.getString("BubblesMenuDlg.lblBookmarks.text");
            lblMySources.Text = Utils.getString("BubblesMenuDlg.lblMySources.text");
            lblCopyPaste.Text = Utils.getString("BubblesMenuDlg.lblCopyPaste.text");
            lblFormat.Text = Utils.getString("BubblesMenuDlg.lblFormat.text");

            Location = new Point(Cursor.Position.X, MMUtils.MindManager.Top + MMUtils.MindManager.Height - this.Height - Settings.Height);
            this.Deactivate += This_Deactivate;

            mwIcons = new Bitmap(Utils.ImagesPath + "mwIcons.png");
            mwIconsActive = new Bitmap(Utils.ImagesPath + "mwIconsActive.png");

            mwPriPro = new Bitmap(Utils.ImagesPath + "mwPriPro.png");
            mwPriProActive = new Bitmap(Utils.ImagesPath + "mwPriProActive.png");

            mwBookmarks = new Bitmap(Utils.ImagesPath + "mwBookmarks.png");
            mwBookmarksActive = new Bitmap(Utils.ImagesPath + "mwBookmarksActive.png");

            mwCopyPaste = new Bitmap(Utils.ImagesPath + "mwCopyPaste.png");
            mwCopyPasteActive = new Bitmap(Utils.ImagesPath + "mwCopyPasteActive.png");

            mwSources = new Bitmap(Utils.ImagesPath + "mwSources.png");
            mwSourcesActive = new Bitmap(Utils.ImagesPath + "mwSourcesActive.png");

            mwFormat = new Bitmap(Utils.ImagesPath + "mwFormat.png");
            mwFormatActive = new Bitmap(Utils.ImagesPath + "mwFormatActive.png");

            mwNotepad = new Bitmap(Utils.ImagesPath + "mwNotepad.png");
            mwNotepadActive = new Bitmap(Utils.ImagesPath + "mwNotepadActive.png");
        }

        private void This_Deactivate(object sender, EventArgs e)
        {
            if (!keepmenu)
                this.Hide();
        }

        /// <summary>
        /// Get stick from database by its ID
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <returns>0 - Very new stick, -1 - User don't want select a stick, N - stick ID
        /// </returns>
        private int GetStick(string type, ref string position, ref string name)
        {
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKS where type=`" + type + "`");
                if (dt.Rows.Count == 0) // there are not sticks of this type
                    return 0;
                else if (dt.Rows.Count == 1) // only one stick, do not show SelectStickDlg
                {
                    position = dt.Rows[0]["position"].ToString();
                    name = dt.Rows[0]["name"].ToString();
                    return Convert.ToInt32(dt.Rows[0]["id"]);
                }
                else // more than one stick, show SelectStickDlg
                {
                    keepmenu = true;

                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    foreach (DataRow row in dt.Rows)
                        dict.Add(Convert.ToInt32(row["id"]), row["name"].ToString());

                    using (SelectStickDlg dlg = new SelectStickDlg(dict))
                    {
                        dlg.Location = new Point(this.Bounds.X + lblSelectStickLocation.Location.X, this.Bounds.Y);

                        if (dlg.ShowDialog() == DialogResult.Cancel)
                            return -1;

                        dt = db.ExecuteQuery("select * from STICKS where id=" + dlg.StickID + "");
                        if (dt.Rows.Count > 0)
                        {
                            position = dt.Rows[0]["position"].ToString();
                            name = dt.Rows[0]["name"].ToString();
                            return dlg.StickID;
                        }
                        return -1;
                    }
                }
            }
        }
        bool keepmenu = false;

        public void Icons_Click(object sender, EventArgs e)
        {
            string orientation = "H", location = "", name = ""; int id = 0;

            if (StickClicked(StickUtils.typeicons, ref orientation, ref location, ref name, ref id) == false)
                return; // user don't want select a stick, or stick already running, or stick troubles

            if (name == "")
                name = Utils.getString("icons.bubble.tooltip");
            BubbleIcons form = new BubbleIcons(id, orientation, name);
            form.Location = GetStickLocation(location);
            BubblesButton.STICKS.Add(id, form);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        public void PriPro_Click(object sender, EventArgs e)
        {
            string orientation = "H", location = "", name = ""; int id = 0;

            if (StickClicked(StickUtils.typepripro, ref orientation, ref location, ref name, ref id) == false)
                return; // user don't want select a stick, or stick already running, or stick troubles

            if (name == "")
                name = Utils.getString("pripro.bubble.tooltip");
            BubblePriPro form = new BubblePriPro(id, orientation, name);
            form.Location = GetStickLocation(location);
            BubblesButton.STICKS.Add(id, form);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        public void MySources_Click(object sender, EventArgs e)
        {
            string orientation = "H", location = "", name = ""; int id = 0;

            if (StickClicked(StickUtils.typesources, ref orientation, ref location, ref name, ref id) == false)
                return; // user don't want select a stick, or stick already running, or stick troubles

            if (name == "")
                name = Utils.getString("mysources.bubble.tooltip");
            BubbleMySources form = new BubbleMySources(id, orientation, name);
            form.Location = GetStickLocation(location);
            BubblesButton.STICKS.Add(id, form);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        bool StickClicked(string type, ref string orientation, ref string location, ref string name, ref int id)
        {
            string position = "";
            id = GetStick(type, ref position, ref name);
            keepmenu = false;

            if (id == -1) // Cancel button
                return false;
            if (id == 0) // The very first stick
            {
                using (BubblesDB db = new BubblesDB())
                {
                    // create "My Icons" stick
                    name = Utils.getString(type + ".bubble.tooltip");
                    db.AddStick(name, type, 0, "");

                    // Get auto-created ID
                    DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0)
                        id = Convert.ToInt32(dt.Rows[0][0]);
                    else
                        return false; // todo
                }
            }

            if (BubblesButton.STICKS.Keys.Contains(id))
            {
                MessageBox.Show(Utils.getString("sticks.stickalreadyrunning"));
                return false;
            }

            orientation = "H"; location = "";
            if (position != "")
            {
                string[] parts = position.Split('#');
                orientation = parts[0];
                location = parts[1];
            }
            return true;
        }

        public void Bookmarks_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleBookmarks.Visible)
            {
                BubblesButton.m_bubbleBookmarks.Hide();
                Bookmarks.Image = mwBookmarks;
            }
            else
            {
                if (firstBookmarks)
                {
                    firstBookmarks = false;
                    BubblesButton.m_bubbleBookmarks.Location = GetStickLocation("PositionBookmarks");
                }
                BubblesButton.m_bubbleBookmarks.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                Bookmarks.Image = mwBookmarksActive;
            }
        }

        public void PasteBubble_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubblePaste.Visible)
            {
                BubblesButton.m_bubblePaste.Hide();
                Paste.Image = mwCopyPaste;
            }
            else
            {
                if (firstPaste)
                {
                    firstPaste = false;
                    BubblesButton.m_bubblePaste.Location = GetStickLocation("PositionPaste");
                }
                BubblesButton.m_bubblePaste.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                Paste.Image = mwCopyPasteActive;
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            using (SettingsDlg dlg = new SettingsDlg())
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        public void Notepad_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleNotepad.Visible)
            {
                BubblesButton.m_bubbleNotepad.Hide();
                Notepad.Image = mwNotepad;
            }
            else
            {
                if (firstNotepad)
                {
                    firstNotepad = false;
                    BubblesButton.m_bubbleNotepad.Location = GetStickLocation("PositionNotepad");
                }
                BubblesButton.m_bubbleNotepad.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                Notepad.Image = mwNotepadActive;
            }
        }

        public void Formatting_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleFormat.Visible)
            {
                BubblesButton.m_bubbleFormat.Hide();
                Format.Image = mwFormat;
            }
            else
            {
                if (firstFormat)
                {
                    firstFormat = false;
                    BubblesButton.m_bubbleFormat.Location = GetStickLocation("PositionMySources");
                }
                BubblesButton.m_bubbleFormat.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                Format.Image = mwFormatActive;
            }
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

        public Point GetStickLocation(string location, bool start = false)
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

                if (!Utils.StickIsOnScreen(thisLocation, this.Size))
                    thisLocation = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            }
            return thisLocation;
        }

        public Image mwIcons, mwIconsActive, mwPriPro, mwPriProActive, mwBookmarks, mwBookmarksActive, mwCopyPaste,
            mwCopyPasteActive, mwSources, mwSourcesActive, mwFormat, mwFormatActive, mwNotepad, mwNotepadActive;

        private bool firstIcons = true, firstBookmarks = true, firstPaste = true, 
            firstPriPro = true, firstMySources = true, firstFormat = true, firstNotepad = true;
    }
}
