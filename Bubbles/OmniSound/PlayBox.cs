using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class PlayBox : Form
    {
        public PlayBox(Rectangle omniButton, string trackName, string topicGuid)
        {
            InitializeComponent();

            if (lblTrack.Text.Length > 30)
                toolTip1.SetToolTip(lblTrack, trackName);
            else
                toolTip1.SetToolTip(lblTrack, Utils.getString("PlayBox.lblTrack"));

            thisSize = this.Size;
            OmniButton = omniButton;
            lblTrack.Text = trackName;
            lblTrack.Tag = topicGuid;

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            try
            {
                // Works only on Windows 11!
                DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            }
            catch { }
        }
        Size thisSize; Rectangle OmniButton;

        private void PlayBox_Load(object sender, EventArgs e)
        {
            this.Size = thisSize;

            int locx = (OmniButton.Width - this.Width) / 2;
            this.Location = new Point(
                OmniButton.X + locx,
                OmniButton.Y + OmniButton.Height);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            StixMain.m_OmniSound.btnPause_Click(null, null);
        }

        private void btnStopPlay_Click(object sender, EventArgs e)
        {
            StixMain.m_OmniSound.btnPlay_Click(null, null);
            this.Close();
        }

        /// <summary>Audio clock.</summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            int sec = Convert.ToInt32(lblsecond.Text);

            if (sec >= 60)
            {
                sec = -1;
                int min = Convert.ToInt32(lblmin.Text);

                if (++min > 9)
                    lblmin.Text = min.ToString();
                else
                    lblmin.Text = "0" + min.ToString();
            }

            if (++sec > 9)
                lblsecond.Text = sec.ToString();
            else
                lblsecond.Text = "0" + sec.ToString();
        }

        private void lblTrack_Click(object sender, EventArgs e)
        {
            if (lblTrack.Tag == null) return;

            string tGuid = lblTrack.Tag.ToString();
            if (tGuid != "")
            {
                Topic t = MMUtils.ActiveDocument.FindByGuid(tGuid) as Topic;

                if (t != null)
                {
                    t.SelectOnly(); t.SnapIntoView();
                }
            }
        }

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
    }
}
