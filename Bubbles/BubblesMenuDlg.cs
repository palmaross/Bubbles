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

            Location = new Point(Cursor.Position.X, MMUtils.MindManager.Top + MMUtils.MindManager.Height - this.Height - (PasteBubble.Height * 2));

            this.Deactivate += BubblesMenuDlg_Deactivate;
        }

        private void BubblesMenuDlg_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasteBubble_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (BubblesButton.m_bubblePaste.Visible)
                BubblesButton.m_bubblePaste.Hide();
            else
                BubblesButton.m_bubblePaste.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void ClipBoard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (BubblesButton.m_Snippets.Visible)
                BubblesButton.m_Snippets.Hide();
            else
                BubblesButton.m_Snippets.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void linkSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (SettingsDlg dlg = new SettingsDlg()) 
            {
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            this.Close();
        }

        private void linkIcons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (BubblesButton.m_bubbleIcons.Visible)
                BubblesButton.m_bubbleIcons.Hide();
            else
                BubblesButton.m_bubbleIcons.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }
    }
}
