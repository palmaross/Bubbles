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

            Location = new Point(Cursor.Position.X, MMUtils.MindManager.Top + MMUtils.MindManager.Height - this.Height - Settings.Height);
            this.Deactivate += Dublicate_Deactivate;

            Screen.Image = Image.FromFile(Utils.ImagesPath + "screen" + screen + ".png");
        }

        private void Dublicate_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Icons_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleIcons.Visible)
                BubblesButton.m_bubbleIcons.Hide();
            else
                BubblesButton.m_bubbleIcons.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void Bookmarks_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleBookmarks.Visible)
                BubblesButton.m_bubbleBookmarks.Hide();
            else
                BubblesButton.m_bubbleBookmarks.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void ClipBoard_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubblePriPro.Visible)
                BubblesButton.m_bubblePriPro.Hide();
            else
                BubblesButton.m_bubblePriPro.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void PasteBubble_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubblePaste.Visible)
                BubblesButton.m_bubblePaste.Hide();
            else
                BubblesButton.m_bubblePaste.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            using (SettingsDlg dlg = new SettingsDlg())
            {
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            this.Close();
        }

        private void MySources_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_bubbleMySources.Visible)
                BubblesButton.m_bubbleMySources.Hide();
            else
                BubblesButton.m_bubbleMySources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void Screen_Click(object sender, EventArgs e)
        {
            screen++;
            if (screen > screenCount)
                screen = 1;

            Screen.Image = Image.FromFile(Utils.ImagesPath + "screen" + screen + ".png");
        }

        public static int screen;
        public static int screenCount;
    }
}
