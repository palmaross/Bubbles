using PRAManager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class FormerBubblesMenuDlg : Form
    {
        public FormerBubblesMenuDlg()
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
            if (BubblesButton.m_bubbleSnippets.Visible)
                BubblesButton.m_bubbleSnippets.Hide();
            else
                BubblesButton.m_bubbleSnippets.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

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

        private void linkBookmarks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (BubblesButton.m_bubbleBookmarks.Visible)
                BubblesButton.m_bubbleBookmarks.Hide();
            else
                BubblesButton.m_bubbleBookmarks.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            this.Close();
        }

        private void linkCreateConfiguration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (CreateConfigurationDlg dlg = new CreateConfigurationDlg())
            {
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void linkRunConfiguration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void BubblesMenuDlg_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }
    }
}
