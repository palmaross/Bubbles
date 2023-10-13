using PRAManager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class BubblesMenuDlg : Form
    {
        public BubblesMenuDlg()
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
            this.Hide();
        }

        public void Icons_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleIcons.Visible)
            {
                BubblesButton.m_bubbleIcons.Hide();
                Icons.Image = mwIcons;
            }
            else
            {
                if (firstIcons)
                {
                    firstIcons = false;
                    BubblesButton.m_bubbleIcons.Location = GetStickLocation("PositionIcons");
                }
                BubblesButton.m_bubbleIcons.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                Icons.Image = mwIconsActive;
            }
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

        public void PriPro_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubblePriPro.Visible)
            {
                BubblesButton.m_bubblePriPro.Hide();
                PriPro.Image = mwPriPro;
            }
            else
            {
                if (firstPriPro)
                {
                    firstPriPro = false;
                    BubblesButton.m_bubblePriPro.Location = GetStickLocation("PositionPriPro");
                }
                BubblesButton.m_bubblePriPro.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                PriPro.Image = mwPriProActive;
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

        public void MySources_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleMySources.Visible)
            {
                BubblesButton.m_bubbleMySources.Hide();
                MySources.Image = mwSources;
            }
            else
            {
                if (firstMySources)
                {
                    firstMySources = false;
                    BubblesButton.m_bubbleMySources.Location = GetStickLocation("PositionMySources");
                }
                BubblesButton.m_bubbleMySources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                MySources.Image = mwSourcesActive;
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
                new StickerItem("\nHello!", "#515151", "#B9B9F9", "Verdana", 9, 0, "0",
                "hello1.png:" + 
                StickerDummy.DummyStickerImageX + ":" + StickerDummy.DummyStickerImageY + ":" +
                pSticker.Width + ":" + pSticker.Height, 
                "center", "sticker"), new Point(0, 0));
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        public Point GetStickLocation(string stick)
        {
            Point thisLocation = new Point();
            string location = Utils.getRegistry(stick, "");
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

                if (!Utils.WandIsOnScreen(thisLocation, this.Size))
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
