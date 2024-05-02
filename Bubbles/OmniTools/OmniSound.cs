using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using PRAManager;
using System.IO;
using System.Media;
using Color = System.Drawing.Color;
using Mindjet.MindManager.Interop;
using AppManager;
using System.Linq;

namespace Bubbles
{
    public partial class OmniSound : Form
    {
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
            pClose.Text = Utils.getString("button.close");
            pHelp.Text = Utils.getString("button.help");

            toolTip1.SetToolTip(btnRecord, Utils.getString("OmniSound.btnRecord.tooltip"));
            toolTip1.SetToolTip(btnPause, Utils.getString("OmniSound.btnPause.tooltip"));
            toolTip1.SetToolTip(btnPlay, Utils.getString("OmniSound.btnPlay.tooltip"));

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
                btnSave_Click(null, null);
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
            mciSendString("open new Type waveaudio Alias omnisound", null, 0, IntPtr.Zero);
            mciSendString("set omnisound time format ms bitspersample 16 samplespersec 8000 channels 1", null, 0, IntPtr.Zero);
            mciSendString("record omnisound", null, 0, IntPtr.Zero);

            btnRecord.Image = StopRecord;
            toolTip1.SetToolTip(btnRecord, Utils.getString("OmniSound.btnRecord.stop.tooltip"));
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (pause)
            {
                pause = false;
                mciSendString("resume omnisound", null, 0, IntPtr.Zero);
                timer1.Start();
            }
            else
            {
                pause = true;
                mciSendString("pause omnisound", null, 0, IntPtr.Zero);
                timer1.Stop();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
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
            mciSendString("save omnisound " + filename, null, 0, IntPtr.Zero);
            mciSendString("close omnisound", null, 0, IntPtr.Zero);

            int i = cbRecords.Items.Add(recordName);
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
                lblDuration.Visible = false;
                // Remove playing topic indices.
                FilePath = "";
                TopicGuid = "";
                return;
            }

            // Start playing.

            play = true; btnPlay.Image = StopPlay;
            toolTip1.SetToolTip(btnPlay, Utils.getString("OmniSound.btnPlay.stop.tooltip"));

            string filename = FilePath;
            if (FilePath == "") // not from topic
                filename = (cbRecords.SelectedItem as AudioItem).Path;

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

                string duration = "(";
                if (min < 10) duration += "0" + min; else duration += min;
                duration += ":";
                if (sec < 10) duration += "0" + sec; else duration += sec;
                duration += ")";

                lblDuration.Text = duration;
            }

            timer1.Start();
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

            // Add strip icon.
            TransactionWrapper _w = new TransactionWrapper(_t, 
                TransactionWrapper.TransactionType.ADD_STRIP_ICON,
                (cbRecords.SelectedItem as AudioItem).Path);
            _w.controlStripURI = StixMain.STRIP_URI;
            _w.Execute();
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OmniRecorder.htm");
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
        private static extern long mciSendString(string lpstrCommand, StringBuilder returnValue, int uReturnLength, IntPtr winHandle);
        public const int MM_MCINOTIFY = 0x3B9;

        ////[STAThread]

        /// <summary>
        /// Catch end of playing audio track.
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MM_MCINOTIFY)
            {
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

        private void OmniSound_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
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
