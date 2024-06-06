using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using PRAManager;
using System.IO;
using Color = System.Drawing.Color;
using Mindjet.MindManager.Interop;
using AppManager;
using System.Linq;
using System.Drawing;

namespace Bubbles
{
    public partial class OmniSound : Form
    {
        // Try the https://www.nuget.org/packages/naudio !!!
        public OmniSound()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "OmniRecorder.htm");

            btnAddToTopic.Text = Utils.getString("OmniSound.btnAddToTopic");
            lblRecordName.Text = Utils.getString("OmniSound.lblRecordName");
            btnSaveRecord.Text = Utils.getString("button.save");
            btnCancelRecord.Text = Utils.getString("button.cancel");
            chAttachment.Text = Utils.getString("OmniSound.chAttachment");

            toolTip1.SetToolTip(btnRecord, Utils.getString("OmniSound.btnRecord.tooltip"));
            toolTip1.SetToolTip(btnPause, Utils.getString("OmniSound.btnPause.tooltip"));
            toolTip1.SetToolTip(btnPlay, Utils.getString("OmniSound.btnPlay.tooltip"));
            toolTip1.SetToolTip(chAttachment, Utils.getString("OmniSound.chAttachment.tooltip"));

            o_close.Text = Utils.getString("button.close");
            o_help.Text = Utils.getString("button.help");

            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            this.Paint += This_Paint; // paint the border

            StartRecord = btnRecord.Image; StopRecord = btnStopRecord.Image;
            StartPlay = btnPlay.Image; StopPlay = btnStopPlay.Image;

            string path = Utils.m_dataPath + "SoundDB";
            DirectoryInfo di = new DirectoryInfo(path);

            var extensions = new[] { "*.wav", "*.mp3" };
            var files = extensions.SelectMany(ext => di.GetFiles(ext, SearchOption.AllDirectories));

            foreach (var fi in files)
                cbRecords.Items.Add(new AudioItem(Path.GetFileNameWithoutExtension(fi.Name), fi.FullName));

            if (cbRecords.Items.Count > 0)
                cbRecords.SelectedIndex = 0;

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

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == o_close)
            {
                this.Hide();
            }
            else if (e.ClickedItem == o_help)
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OmniRecorder.htm");
            }
            else if (e.ClickedItem == o_advanced)
            {

            }
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void OmniSound_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { new Microsoft.VisualBasic.Devices.Audio().Stop(); }
            catch { }

            timer1.Stop();
            timer1.Enabled = false;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (play) // play is performed!
            {
                MessageBox.Show("OmniSound.busy.playing");
                return;
            }

            lblDuration.Visible = false;

            if (record) // Stop and save record
            {
                toolTip1.SetToolTip(btnRecord, Utils.getString("OmniSound.btnRecord.tooltip"));
                record = false;
                btnRecord.Image = StartRecord;
                pRecord.Visible = false;
                Stop_Click();
                return;
            }

            // Start record

            record = true;
            lblmin.Text = "00"; lblmin.ForeColor = Color.Red;
            lblsecond.Text = "00"; lblsecond.ForeColor = Color.Red;
            label1.ForeColor = Color.Red;
            pRecord.Visible = true;

            timer1.Enabled = true;
            timer1.Start();
            mciSendString("open new Type waveaudio alias omnisound", null, 0, IntPtr.Zero);
            mciSendString("set omnisound time format ms bitspersample 16 samplespersec 8000 channels 1", null, 0, IntPtr.Zero);
            mciSendString("record omnisound", null, 0, IntPtr.Zero);

            btnRecord.Image = StopRecord;
            toolTip1.SetToolTip(btnRecord, Utils.getString("OmniSound.btnRecord.stop.tooltip"));
        }

        public void btnPause_Click(object sender, EventArgs e)
        {
            if (pause)
            {
                pause = false;
                mciSendString("resume omnisound", null, 0, IntPtr.Zero);
                timer1.Start();
                if (StixMain.m_playBox != null && StixMain.m_playBox.Visible)
                    StixMain.m_playBox.timer1.Start();
            }
            else
            {
                pause = true;
                mciSendString("pause omnisound", null, 0, IntPtr.Zero);
                timer1.Stop();
                if (StixMain.m_playBox != null && StixMain.m_playBox.Visible)
                    StixMain.m_playBox.timer1.Stop();
            }
        }

        private void Stop_Click()
        {
            mciSendString("stop omnisound", null, 0, IntPtr.Zero);

            timer1.Stop();
            timer1.Enabled = false;

            lblmin.Text = "00"; lblmin.ForeColor = Color.Black;
            lblsecond.Text = "00"; lblsecond.ForeColor = Color.Black;
            label1.ForeColor = Color.Black;

            txtRecordName.Text = "Record 1";
            panelRecordName.Visible = true;
            panelRecordName.BringToFront();
            txtRecordName.SelectAll();
            txtRecordName.Focus();
        }

        private void txtRecordName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mciSendString("close omnisound", null, 0, IntPtr.Zero);
                panelRecordName.Visible = false;
                return;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SaveRecord();
            }
        }

        void SaveRecord()
        {
            string recordName = txtRecordName.Text.Trim();
            string path = Utils.m_dataPath + "SoundDB";
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo fi in di.GetFiles())
            {
                if (Path.GetFileNameWithoutExtension(fi.Name) == recordName)
                {
                    if (MessageBox.Show(Utils.getString("OmniSound.recordexists"), "",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                }
            }

            string filename = Utils.m_dataPath + "SoundDB\\" + recordName + ".wav";
            mciSendString("save omnisound \"" + filename + "\"", null, 0, IntPtr.Zero);
            mciSendString("close omnisound", null, 0, IntPtr.Zero);

            int i = cbRecords.Items.Add(new AudioItem(recordName, filename));
            cbRecords.SelectedIndex = i;
            panelRecordName.Visible = false;
        }

        public void btnPlay_Click(object sender, EventArgs e)
        {
            if (record) // record is performed!
            {
                MessageBox.Show("OmniSound.busy.recording");
                return;
            }

            lblmin.Text = "00"; lblmin.ForeColor = Color.Black;
            lblsecond.Text = "00"; lblsecond.ForeColor = Color.Black;
            label1.ForeColor = Color.Black;

            if (play) // Stop playing.
            {
                toolTip1.SetToolTip(btnPlay, Utils.getString("OmniSound.btnPlay.tooltip"));
                play = false;
                btnPlay.Image = StartPlay;
                mciSendString("stop omnisound", null, 0, IntPtr.Zero);
                mciSendString("close omnisound", null, 0, IntPtr.Zero);
                timer1.Stop();
                if (StixMain.m_playBox != null && StixMain.m_playBox.Visible) 
                    StixMain.m_playBox.timer1.Stop();
                lblDuration.Visible = false;
                // Remove playing topic indices.
                FilePath = "";
                TopicGuid = "";
                return;
            }

            // Start playing.

            play = true; btnPlay.Image = StopPlay;
            toolTip1.SetToolTip(btnPlay, Utils.getString("OmniSound.btnPlay.stop.tooltip"));

            string filename = "";
            if (FilePath == "")
            {
                if (cbRecords.Items.Count > 0 && cbRecords.SelectedIndex >= 0) // not from topic
                    filename = (cbRecords.SelectedItem as AudioItem).Path;
                else
                    return;
            }
            else
                filename = FilePath;

            StringBuilder lengthBuf = new StringBuilder(32);

            mciSendString("open \"" + filename + "\" type mpegvideo alias omnisound", null, 0, IntPtr.Zero);
            mciSendString("status omnisound length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
            mciSendString("play omnisound from 0 notify", null, 0, this.Handle);

            // Get audio length.
            int length = 0;
            int.TryParse(lengthBuf.ToString(), out length);

            if (length > 0)
            {
                lblDuration.Visible = true;

                float length2 = length / 1000F;
                length = (int)Math.Round(length2);
                playSecond = length;
                int min = length / 60;
                int sec = length % 60;

                string duration = "/  ";
                if (min < 10) duration += "0" + min; else duration += min;
                duration += ":";
                if (sec < 10) duration += "0" + sec; else duration += sec;
                //duration += ")";

                lblDuration.Text = duration;
                if (StixMain.m_playBox != null && StixMain.m_playBox.Visible)
                    StixMain.m_playBox.lblDuration.Text = duration;
            }

            timer1.Start();
            if (StixMain.m_playBox != null && StixMain.m_playBox.Visible)
                StixMain.m_playBox.timer1.Start();
        }

        /// <summary>Audio clock.</summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (record) // blinking icon
            {
                if (pRecord.Visible) pRecord.Visible = false;
                else pRecord.Visible = true;
            }

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

            // Get end of audio. Stop timer.
            playSecond -= 1;
        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void btnCancelRecord_Click(object sender, EventArgs e)
        {
            mciSendString("close omnisound", null, 0, IntPtr.Zero);
            panelRecordName.Visible = false;
        }

        private void btnAddToTopic_Click(object sender, EventArgs e)
        {
            if (!(MMUtils.SelectedTopic() is Topic _t))
                return;

            if (_t.ContainsControlStripType(StixMain.STRIP_URI))
                return;

            if (chAttachment.Checked) // Add as attachment
            {
                _t.Attachments.Add((cbRecords.SelectedItem as AudioItem).Path);
            }
            else // Add strip icon.
            {
                TransactionWrapper _w = new TransactionWrapper(_t,
                    TransactionWrapper.TransactionType.ADD_STRIP_ICON,
                    (cbRecords.SelectedItem as AudioItem).Path);
                _w.controlStripURI = StixMain.STRIP_URI;
                _w.Execute();
            }
        }

        private void OmniSound_MouseDown(object sender, MouseEventArgs e)
        {
            cur = MousePosition;

            // move form
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            // check if it's just á mouse click
            timer2.Start();
        }
        Point cur;

        /// <summary>
        /// Check if MouseDown event is for move form or it is a click
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            if ((MouseButtons & MouseButtons.Left) != 0) // mouse button still is pressed
                return;

            timer2.Stop();
            if (MousePosition.X < cur.X + 5 && MousePosition.X > cur.X - 5 &&
                MousePosition.Y < cur.Y + 5 && MousePosition.Y > cur.Y - 5)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        System.Drawing.Image StartRecord, StopRecord, StartPlay, StopPlay;

        public bool play = false;
        bool record = false;
        bool pause = false;
        int playSecond = 0;

        /// <summary>
        /// Path to audio file attached to topic.
        /// </summary>
        public string FilePath = "";
        /// <summary>
        /// Guid of paying topic.
        /// </summary>
        public string TopicGuid = "";

        //[DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        //private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, StringBuilder returnValue, int uReturnLength, IntPtr winHandle);
        public const int MM_MCINOTIFY = 0x3B9;

        ////[STAThread]

        /// <summary>
        /// Catch end of playing audio track.
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MM_MCINOTIFY)
            {
                if (StixMain.m_playBox != null && StixMain.m_playBox.Visible)
                {
                    StixMain.m_playBox.timer1.Stop();
                    StixMain.m_playBox.Close();
                }
                timer1.Stop();
                lblDuration.Visible = false;
                play = false;
                btnPlay.Image = StartPlay;
                lblmin.Text = "00";
                lblsecond.Text = "00";

                mciSendString("close omnisound", null, 0, IntPtr.Zero);

                FilePath = "";
                TopicGuid = "";
            }
            base.WndProc(ref m);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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

    public class AudioItem
    {
        public AudioItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name;
        public string Path;

        public override string ToString()
        {
            return Name;
        }
    }
}
