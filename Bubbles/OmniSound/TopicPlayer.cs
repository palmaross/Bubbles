using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NAudio.Wave;
using System.Data;

namespace Bubbles
{
    public partial class TopicPlayer : Form
    {
        public TopicPlayer(Rectangle omniButton)
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TopicPlayer.htm");

            myToolTip1.SetToolTip(pictureHandle, Utils.getString("TopicPlayer.Title") +
                Utils.getString("TopicPlayer.description") + Utils.getString("HeadIcon.tooltip.tips"));
            toolTip1.SetToolTip(btnPause, Utils.getString("TopicPlayer.btnPause") + Utils.getString("TopicPlayer.btnPausePlay"));
            toolTip1.SetToolTip(btnPlay, Utils.getString("TopicPlayer.btnPlay") + Utils.getString("TopicPlayer.btnPausePlay"));
            toolTip1.SetToolTip(pVolume, Utils.getString("TopicPlayer.pVolume"));
            toolTip1.SetToolTip(pPosition, Utils.getString("TopicPlayer.pPosition"));
            toolTip1.SetToolTip(pTitle, Utils.getString("TopicPlayer.pTitle"));

            menuStop.Text = Utils.getString("TopicPlayer.menuStop");
            menuReplay.Text = Utils.getString("TopicPlayer.menuReplay");

            ToolStripItem tsi = cmsManage.Items.Add(Utils.getString("TopicPlayer.GoToTopic"));
            tsi.Name = "GoToTopic";
            StixUtils.SetContextMenuImage(tsi, "soundTopic.png");

            tsi = cmsManage.Items.Add(Utils.getString("TopicPlayer.SoundTopics"));
            tsi.Name = "SoundTopics";
            StixUtils.SetContextMenuImage(tsi, "soundTopics.png");

            SoundTopicsList = (tsi as ToolStripMenuItem).DropDown;
            InitSoundTopicsList();

            StixUtils.SetCommonContextMenu(cmsManage, StixUtils.typeTopicPlayer);
            cmsManage.ItemClicked += ContextMenuStrip1_ItemClicked;
            SoundTopicsList.ItemClicked += ContextMenuStrip1_ItemClicked;
            cmsPlayPauseButtons.ItemClicked += ContextMenuStrip1_ItemClicked;

            toolTip1.SetToolTip(lblTitle, Utils.getString("TopicPlayer.lblTrack"));
            lblTitle.Location = tbTrack.Location;

            OmniButton = omniButton;
            lblClock.Text = lblClock.Text = "00:00 / 00:00";

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            try
            {
                // Works only on Windows 11!
                DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            }
            catch { }

            tbVolume.Scroll += (s, a) =>
            {
                StixMain.m_OmniSound.outputDevice.Volume = tbVolume.Value / 100f;
                lblClock.Text = "Volume  " + tbVolume.Value;
                if (StixMain.m_OmniSound.Visible)
                    StixMain.m_OmniSound.Volume.Value = tbVolume.Value;
            };

            tbTrack.Scroll += (s, a) =>
            {
                if (StixMain.m_OmniSound.audioFile != null)
                    StixMain.m_OmniSound.audioFile.CurrentTime = TimeSpan.FromSeconds(tbTrack.Value);
            };

            pictureHandle.MouseDown += (sender, e) => // move the stick
            {
                if (e.Clicks == 1 && e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };
            pictureHandle.MouseDoubleClick += (sender, e) =>
            {
                Stop();
                this.Hide();
            };

            MinLength = this.Width;
            fsize = lblClock.Font.Size;
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);
        }
        Rectangle OmniButton;

        private void PlayBox_Load(object sender, EventArgs e)
        {
            int locx = (OmniButton.Width - this.Width) / 2;
            this.Location = new Point(
                OmniButton.X + locx,
                OmniButton.Y + OmniButton.Height);
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            scaleFactor = toScale;
            float _fsize = fsize * (toScale / 100);

            if (scale != 1)
            {
                this.Scale(new SizeF(scale, scale)); // reset to 100%
                //StixUtils.icondist = pIconDist.Width;
                MinLength = (int)(MinLength * scale);
            }

            if (toScale != 100)
            {
                this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale
                //StixUtils.icondist = pIconDist.Width;
                MinLength = (int)(MinLength * (toScale / 100));
            }

            lblClock.Font = new Font(lblClock.Font.FontFamily, _fsize);
            lblTitle.Font = new Font(lblClock.Font.FontFamily, _fsize);
        }
        float fsize;

        public void InitSoundTopicsList()
        {
            SoundTopicsList.Items.Clear();

            foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
            {
                if (t.ContainsControlStripType(StixMain.SOUNDSTRIP_URI))
                {
                    string tText = t.Text.Trim();
                    if (tText.Length == 0) tText = "No name";
                    if (tText.Length > 50) tText = tText.Substring(0, 50);

                    ToolStripItem tsi = SoundTopicsList.Items.Add(tText);
                    tsi.Name = "AudioTopic";
                    tsi.Tag = t.Guid;
                }
            }
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "GoToTopic")
            {
                ShowPlayingTopic();
            }
            if (e.ClickedItem.Name == "AudioTopic") // Audio topics in the map
            {
                Topic t = MMUtils.ActiveDocument.FindByGuid(e.ClickedItem.Tag.ToString()) as Topic;
                if (t != null)
                {
                    t.SelectOnly(); t.SnapIntoView();
                    StixUtils.ActivateMindManager();
                    t = null;
                    btnPlay_Click();
                }
            }
            else if (e.ClickedItem == menuStop)
            {
                Stop();
            }
            else if (e.ClickedItem == menuReplay)
            {
                if (StixMain.m_OmniSound.audioFile != null)
                {
                    tbTrack.Value = 0;
                    StixMain.m_OmniSound.audioFile.CurrentTime = TimeSpan.FromSeconds(0);
                }
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixMain.m_OmniSound.outputDevice?.Stop();
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "TopicPlayer.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, "H");
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typeTopicPlayer, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, "H", "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            StixUtils.manage_clicked = true;

            foreach (ToolStripItem item in cmsManage.Items)
                item.Visible = true;

            cmsManage.Show(Cursor.Position);
        }

        private void btnPlayPause_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickCount++;
                buttonClicked = (sender as PictureBox).Name;

                if (!timer1.Enabled)
                    timer1.Start();
            }
            else if (e.Button == MouseButtons.Right)
            {
                menuReplay.Enabled = StixMain.m_OmniSound.audioFile != null;
                menuStop.Enabled = StixMain.m_OmniSound.audioFile != null;

                cmsPlayPauseButtons.Show(MousePosition);
            }
        }
        int clickCount = 0;
        string buttonClicked;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (clickCount == 1)
            {
                if (buttonClicked == "btnPause")
                    btnPause_Click();
                else
                    btnPlay_Click();
            }
            else // Double click. Stop playing.
            {
                Stop();
            }
            clickCount = 0;
        }

        public void Stop()
        {
            if (StixMain.m_OmniSound.outputDevice.PlaybackState != PlaybackState.Stopped)
            {
                StixMain.m_OmniSound.outputDevice.Stop();
            }
            if (StixMain.m_OmniSound.audioFile != null)
            {
                StixMain.m_OmniSound.audioFile.Dispose();
                StixMain.m_OmniSound.audioFile = null;
            }
            btnPlay.Visible = true;
            btnPlay.Location = btnPause.Location;
            btnPause.Visible = false;
            lblClock.Text = "00:00 / 00:00";
            lblTitle.Visible = true; lblTitle.Text = ""; lblTitle.BringToFront();
            pVolume.Visible = true;
        }

        private void btnPause_Click()
        {
            if (StixMain.m_OmniSound.outputDevice.PlaybackState == PlaybackState.Stopped)
                return;

            StixMain.m_OmniSound.pause = false;
            StixMain.m_OmniSound.btnPause_Click(null, null);
        }

        private void btnPlay_Click()
        {
            if (StixMain.m_OmniSound.outputDevice.PlaybackState == PlaybackState.Stopped)
            {
                Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
                if (t != null)
                    StixMain.Play();
            }
            else
            {
                StixMain.m_OmniSound.pause = true;
                StixMain.m_OmniSound.btnPause_Click(null, null);
            }
        }

        private void lblTrack_Click(object sender, EventArgs e)
        {
            if (lblClock.Tag == null) return;

            string tGuid = lblClock.Tag.ToString();
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

        private void pPosition_Click(object sender, EventArgs e)
        {
            pTitle.Visible = true;
            pPosition.Visible = false;
            pTitle.Location = pVolume.Location;
            pVolume.Visible = false;
            tbVolume.Visible = false;
            tbTrack.Visible = true;
            tbTrack.BringToFront();
        }

        private void pTitle_Click(object sender, EventArgs e)
        {
            pVolume.Visible = true;
            pPosition.Visible = false;
            pTitle.Visible = false;
            lblTitle.Visible = true;
            lblTitle.BringToFront();
            tbTrack.Visible = false;
        }

        private void pVolume_Click(object sender, EventArgs e)
        {
                pPosition.Visible = true;
                pPosition.Location = pVolume.Location;

                tbVolume.Location = tbTrack.Location;
                tbVolume.BringToFront();
                tbVolume.Visible = true;

            lblClock.Text = "Volume  " + tbVolume.Value;
        }

        ToolStripDropDown SoundTopicsList;
        public float scaleFactor = 100;
        int MinLength;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        // For release capture
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void tbTrack_MouseDown(object sender, MouseEventArgs e)
        {
            if (StixMain.m_OmniSound.audioFile == null) return;

            double dblValue;
            // Jump to the clicked location
            dblValue = e.X / (double)tbTrack.Width * (tbTrack.Maximum - tbTrack.Minimum);
            tbTrack.Value = Convert.ToInt32(dblValue);

            StixMain.m_OmniSound.audioFile.CurrentTime = TimeSpan.FromSeconds(tbTrack.Value);
        }

        private void tbVolume_MouseDown(object sender, MouseEventArgs e)
        {
            double dblValue;
            // Jump to the clicked location
            dblValue = e.X / (double)tbVolume.Width * (tbVolume.Maximum - tbVolume.Minimum);
            tbVolume.Value = Convert.ToInt32(dblValue);

            StixMain.m_OmniSound.outputDevice.Volume = tbVolume.Value / 100f;
            lblClock.Text = "Volume  " + tbVolume.Value;
        }

        void ShowPlayingTopic()
        {
            var audiofile = StixMain.m_OmniSound.audioFile;
            if (audiofile != null)
            {
                using (StixDB db = new StixDB())
                {
                    DataTable dt = db.ExecuteQuery("select * from AUDIOS where path=`" + audiofile.FileName + "`");
                    if (dt.Rows.Count > 0 &&
                        !String.IsNullOrEmpty(dt.Rows[0]["mappath"].ToString())) // audio note is not attached to a topic
                    {
                        Document doc = Utils.GetOrOpenDocument(dt.Rows[0]["mappath"].ToString(), true);
                        if (doc != null)
                        {
                            Topic t = doc.FindByGuid(dt.Rows[0]["topicguid"].ToString()) as Topic;
                            if (t != null)
                            {
                                t.SelectOnly();
                                t.SnapIntoView();
                            }
                        }
                    }
                }
            }
        }
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ShowPlayingTopic();
        }
    }
}
