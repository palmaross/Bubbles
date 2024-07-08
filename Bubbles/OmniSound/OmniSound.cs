using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PRAManager;
using System.IO;
using Color = System.Drawing.Color;
using Mindjet.MindManager.Interop;
using AppManager;
using NAudio.Wave;
using System.Data;
using System.Drawing;

namespace Bubbles
{
    public partial class OmniSound : Form
    {
        public enum OmniRecorder
        {
            Stopped,
            Capturing,
            Paused
        }

        public OmniRecorder omniRecorder = OmniRecorder.Stopped;

        public OmniSound()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "OmniSound.htm");

            btnAddToTopic.Text = Utils.getString("OmniSound.btnAddToTopic");
            lblRecordName.Text = Utils.getString("OmniSound.lblRecordName");
            btnSaveRecord.Text = Utils.getString("button.save");
            btnCancelRecord.Text = Utils.getString("button.cancel");
            chAttachment.Text = Utils.getString("OmniSound.chAttachment");
            chAddToTopic.Text = Utils.getString("OmniSound.btnAddToTopic");
            chAttachmentSave.Text = Utils.getString("OmniSound.chAttachment");

            lblGroupName.Text = Utils.getString("ResourcesDlg.lblGroupName");
            btnAddGroup.Text = Utils.getString("button.add");
            btnCancelGroup.Text = Utils.getString("button.cancel");

            toolTip1.SetToolTip(btnRecord, Utils.getString("OmniSound.btnRecord.tooltip"));
            toolTip1.SetToolTip(pRecordSystem, Utils.getString("OmniSound.btnRecord.tooltip"));
            toolTip1.SetToolTip(btnStop, Utils.getString("OmniSound.btnStop.tooltip"));
            toolTip1.SetToolTip(btnPause, Utils.getString("OmniSound.btnPause.tooltip"));
            toolTip1.SetToolTip(btnPlay, Utils.getString("OmniSound.btnPlay.tooltip"));
            toolTip1.SetToolTip(chAttachment, Utils.getString("OmniSound.chAttachment.tooltip"));

            btnClose.Text = Utils.getString("button.close");
            o_help.Text = Utils.getString("button.help");
            o_recordtype.Text = Utils.getString("OmniSound.o_recordsystem");
            o_newgroup.Text = Utils.getString("OmniSound.newgroup");

            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            this.Paint += This_Paint; // paint the border
            pEmpty.Location = btnRecord.Location;

            InitAudioFiles();

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            try
            {
                // Works only on Windows 11!
                DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            }
            catch { }

            Volume.Scroll += (s, a) =>
            {
                outputDevice.Volume = Volume.Value / 100f;
            };
            Volume.ValueChanged += (s, a) =>
            {
                if (StixMain.m_TopicPlayer != null && StixMain.m_TopicPlayer.Visible)
                {
                    StixMain.m_TopicPlayer.tbVolume.Value = Volume.Value;
                    if (StixMain.m_TopicPlayer.pPosition.Visible) // not busy with Volume text 
                        StixMain.m_TopicPlayer.lblClock.Text = "Volume  " + Volume.Value;
                }
            };
            aTrack.Scroll += (s, a) =>
            {
                if (audioFile != null)
                    audioFile.CurrentTime = TimeSpan.FromSeconds(aTrack.Value);
            };
            outputDevice.PlaybackStopped += OnPlaybackStopped;
        }

        public void InitAudioFiles()
        {
            cbGroups.Items.Clear();
            cbGroupsSave.Items.Clear();

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from AUDIOGROUPS order by name");
                foreach (DataRow row in dt.Rows)
                {
                    cbGroups.Items.Add(new AudioGroup(row["name"].ToString(), Convert.ToInt32(row["id"])));
                    cbGroupsSave.Items.Add(new AudioGroup(row["name"].ToString(), Convert.ToInt32(row["id"])));
                }
            }
            if (cbGroups.Items.Count > 0) cbGroups.SelectedIndex = 0;
            if (cbGroupsSave.Items.Count > 0) cbGroupsSave.SelectedIndex = 0;
        }

        private void cbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbRecords.Items.Clear();
            int groupID = (cbGroups.SelectedItem as AudioGroup).ID;

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from AUDIOS where groupID=" + groupID + " order by title");

                foreach (DataRow row in dt.Rows)
                    cbRecords.Items.Add(new AudioItem(Convert.ToInt32(row["id"]), 
                        row["title"].ToString(), row["path"].ToString()));
            }

            if (cbRecords.Items.Count > 0)
                cbRecords.SelectedIndex = 0;
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == o_help)
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OmniSound.htm");
            }
            else if (e.ClickedItem == o_manage)
            {
                if (StixMain.m_ManageAudio == null || StixMain.m_ManageAudio.IsDisposed)
                {
                    StixMain.m_ManageAudio = new ManageAudioDlg();
                    StixMain.m_ManageAudio.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
            }
            else if (e.ClickedItem == o_recordtype)
            {
                if (o_recordtype.Checked) // switch to microphone
                {
                    pRecordSystem.Visible = false;
                    btnRecord.Visible = true;
                    btnRecord.BringToFront();
                    SystemAudio = false;
                    o_recordtype.Text = Utils.getString("OmniSound.o_recordsystem");
                }
                else // switch to system sound
                {
                    pRecordSystem.Visible = true;
                    pRecordSystem.Location = btnRecord.Location;
                    pRecordSystem.BringToFront();
                    btnRecord.Visible = false;
                    SystemAudio = true;
                    o_recordtype.Text = Utils.getString("OmniSound.o_micro");
                }
            }
            else if (e.ClickedItem == o_newgroup)
            {
                panelNewGroup.Location = panelRecordName.Location;
                panelNewGroup.Visible = true; 
                panelNewGroup.BringToFront();
            }
        }

        private void btnCancelGroup_Click(object sender, EventArgs e)
        {
            panelNewGroup.Visible = false;
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string name = txtGroupName.Text.Trim();
            if (name == "") return;

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from AUDIOGROUPS where name=`" + name + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.groupexists"));
                    return;
                }

                db.AddAudioGroup(name); int id = 0;
                dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                AudioGroup item = new AudioGroup(name, id);
                int i = cbGroups.Items.Add(item); cbGroups.SelectedIndex = i;
                cbGroupsSave.Items.Add(item); cbGroupsSave.SelectedIndex = i;

                panelNewGroup.Visible = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnStop_Click(null, null);
            RecordLimit = 10;
            this.Opacity = 1;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
            this.Hide();
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument.Path == "")
            {
                if (MessageBox.Show(Utils.getString("OmniStix.SaveMap.warning"), "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }

            if (audioFile != null) // play is performed!
            {
                MessageBox.Show(Utils.getString("OmniSound.busy.playing"));
                return;
            }

            if (writer != null) // recording is performed!
            {
                MessageBox.Show(Utils.getString("OmniSound.busy.recording"));
                return;
            }

            if (StixMain.m_TopicRecorder != null && StixMain.m_TopicRecorder.Visible)
            {
                StixMain.m_TopicRecorder.Opacity = 0.3;
                StixMain.m_TopicRecorder.btnStop.Enabled = false;
                StixMain.m_TopicRecorder.btnPause.Enabled = false;
            }

            // Start record

            lblClock.ForeColor = Color.Red;
            toolTip1.SetToolTip(lblClock, Utils.getString("OmniSound.lblClock.tooltip"));

            // Blink the button and start clock
            pEmpty.BringToFront();

            AudioLength = " / " + RecordLimit + ":00";

            timer1.Enabled = true;
            timer1.Start();

            var outputFolder = Utils.m_dataPath + "SoundDB";
            var outputFilePath = Path.Combine(outputFolder, "record.wav");
            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);

            timer3.Start();

            if (SystemAudio)
            {
                writer = new WaveFileWriter(outputFilePath, capture.WaveFormat);
                capture.StartRecording();
                omniRecorder = OmniRecorder.Capturing;
                capture.DataAvailable += WaveIn_DataAvailable;
            }
            else // Voice record
            {
                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.StartRecording();
                omniRecorder = OmniRecorder.Capturing;
                waveIn.DataAvailable += WaveIn_DataAvailable;
            }
        }

        public void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
            // Stop after 10 min (~ 12 Mb)
            if (!SystemAudio && writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 60 * RecordLimit)
            {
                waveIn.StopRecording();
                go = true;
            }
            if (SystemAudio && writer.Position > capture.WaveFormat.AverageBytesPerSecond * 60 * RecordLimit)
            {
                capture.StopRecording();
                go = true;
            }
        }
        bool sampleIsEmpty = true;

        bool go = false;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!go) return;
            go = false;
            timer3.Stop();
            timer1.Stop();

            DialogResult dr;
            using (TimeLimitReachedDlg dlg = new TimeLimitReachedDlg())
                dr = dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            if (dr == DialogResult.Cancel)
            {
                StopRecording(true, true);
                if (File.Exists(Utils.m_dataPath + "SoundDB\\record.wav"))
                    File.Delete(Utils.m_dataPath + "SoundDB\\record.wav");

                omniRecorder = OmniRecorder.Stopped;
            }
            else if (dr == DialogResult.Yes) // Resume
            {
                pause = true;
                omniRecorder = OmniRecorder.Paused;
                RecordLimit += 5;
                btnPause_Click(null, null);
                timer3.Start();
            }
            else if (dr == DialogResult.No) // Stop
            {
                StopRecording(true);
                omniRecorder = OmniRecorder.Stopped;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (writer != null) // Stop recording
            {
                StopRecording();

                if (StixMain.m_TopicRecorder != null && StixMain.m_TopicRecorder.Visible)
                {
                    StixMain.m_TopicRecorder.Opacity = 1;
                    StixMain.m_TopicRecorder.btnStop.Enabled = true;
                    StixMain.m_TopicRecorder.btnPause.Enabled = true;
                }
            }
            if (outputDevice.PlaybackState != PlaybackState.Stopped) // Stop playing
            {
                outputDevice.Stop();
            }
        }

        private void StopRecording(bool stopped = false, bool cancel = false)
        {
            if (SystemAudio && !stopped)
            {
                capture.StopRecording();
                capture.DataAvailable -= WaveIn_DataAvailable;
            }
            else if (!stopped)
            {
                waveIn.StopRecording();
                waveIn.DataAvailable -= WaveIn_DataAvailable;
            }

            omniRecorder = OmniRecorder.Stopped;

            bool sampleEmpty = writer == null || writer.Length == 0;

            writer?.Dispose();
            writer = null;

            timer1.Stop();
            pEmpty.Visible = false;
            btnRecord.Visible = true;

            lblClock.Text = "00:00 / 00:00";
            lblClock.ForeColor = Color.Black;
            toolTip1.SetToolTip(lblClock, "");

            if (cancel) return;

            if (sampleEmpty)
            {
                MessageBox.Show(Utils.getString("OmniSound.emptyrecord"));
                return;
            }

            txtRecordName.Text = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");
            cbGroupsSave.SelectedIndex = cbGroups.SelectedIndex;
            panelRecordName.Visible = true;
            panelRecordName.BringToFront();
            txtRecordName.Focus();
        }

        public void btnPause_Click(object sender, EventArgs e)
        {
            if (writer == null && audioFile == null) return;

            if (pause) // Resume Play/Record
            {
                pause = false;

                if (writer != null)
                {
                    if (SystemAudio)
                        capture.StartRecording();
                    else
                        waveIn.StartRecording();

                    omniRecorder = OmniRecorder.Capturing;
                }
                else if (outputDevice.PlaybackState != PlaybackState.Stopped)
                {
                    outputDevice.Play();
                    if (StixMain.m_TopicPlayer != null && StixMain.m_TopicPlayer.Visible)
                    {
                        StixMain.m_TopicPlayer.btnPause.Visible = true;
                        StixMain.m_TopicPlayer.btnPlay.Visible = false;
                    }
                }
                else
                    return;

                timer1.Start();
            }
            else // Pause Play/Record
            {
                pause = true;

                if (writer != null)
                {
                    if (SystemAudio)
                        capture.StopRecording();
                    else
                        waveIn.StopRecording();

                    omniRecorder = OmniRecorder.Paused;
                }
                else if (outputDevice.PlaybackState != PlaybackState.Stopped)
                {
                    outputDevice.Pause();
                    if (StixMain.m_TopicPlayer != null && StixMain.m_TopicPlayer.Visible)
                    {
                        StixMain.m_TopicPlayer.btnPause.Visible = false;
                        StixMain.m_TopicPlayer.btnPlay.Visible = true;
                        StixMain.m_TopicPlayer.btnPlay.Location = StixMain.m_TopicPlayer.btnPause.Location;
                    }
                }

                timer1.Stop();
            }
        }

        public void btnPlay_Click(object sender, EventArgs e)
        {
            if (writer != null) // record is performed!
            {
                MessageBox.Show(Utils.getString("OmniSound.busy.recording"));
                return;
            }

            if (sender == btnPlay)
                FilePath = "";

            lblClock.ForeColor = Color.Black;

            // Start playing.

            if (outputDevice.PlaybackState != PlaybackState.Stopped)
            {
                outputDevice.Stop();
                timerStopPlay.Start(); // We have to exit first.
            }
            else
                Play();
        }

        void Play()
        {
            string filename = FilePath;
            if (FilePath == "") // FilePath - called from TopicPlayer
            {
                if (cbRecords.Items.Count > 0 && cbRecords.SelectedIndex >= 0)
                    filename = (cbRecords.SelectedItem as AudioItem).Path;
                else
                    return;
            }

            try
            {
                outputDevice.Volume = Volume.Value / 100f;
                audioFile = new AudioFileReader(filename);
            }
            catch { MessageBox.Show(Utils.getString("OmniSound.filecorrupted")); return; }

            outputDevice.Init(audioFile);

            // Get audio length.
            MediaFoundationReader mfr = new MediaFoundationReader(filename);
            var ts = mfr.TotalTime;
            AudioLength = " / 00:00";

            if (ts.TotalSeconds > 0)
            {
                string mins = ts.Minutes.ToString();
                if (ts.Minutes < 10) mins = "0" + mins;
                string secs = ts.Seconds.ToString();
                if (ts.Seconds < 10) secs = "0" + secs;

                AudioLength = " / " + mins + ":" + secs;
            }

            aTrack.Value = 0;
            aTrack.Maximum = (int)ts.TotalSeconds;

            if (StixMain.m_TopicPlayer != null && StixMain.m_TopicPlayer.Visible)
            {
                StixMain.m_TopicPlayer.tbTrack.Value = 0;
                StixMain.m_TopicPlayer.tbTrack.Maximum = (int)ts.TotalSeconds;

                StixMain.m_TopicPlayer.lblTitle.Text = Path.GetFileNameWithoutExtension(filename);
                StixMain.m_TopicPlayer.btnPause.Visible = true;
                StixMain.m_TopicPlayer.btnPlay.Visible = false;
            }

            outputDevice.Play();
            timer1.Start();
        }

        private void timerStopPlay_Tick(object sender, EventArgs e)
        {
            timerStopPlay.Stop();
            Play();
        }

        public void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }

            if (StixMain.m_TopicPlayer != null && StixMain.m_TopicPlayer.Visible)
                StixMain.m_TopicPlayer.Stop();

            timer1.Stop();
            lblClock.Text = "00:00 / 00:00";
            StixMain.playingtopicguid = "";
        }

        public void SaveRecord(string recordName, int groupID, bool addtotopic, bool attachment)
        {
            string outputFolder = Utils.m_dataPath + "SoundDB";
            DirectoryInfo di = new DirectoryInfo(outputFolder);

            foreach (FileInfo fi in di.GetFiles())
            {
                if (Path.GetFileNameWithoutExtension(fi.Name.ToLower()) == recordName.ToLower())
                {
                    if (MessageBox.Show(Utils.getString("OmniSound.recordexists"), "",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                }
            }

            string filename = Utils.m_dataPath + "SoundDB\\" + recordName + ".mp3";
            string wavFile = Utils.m_dataPath + "SoundDB\\" + recordName + ".wav";
            string recordedWavFile = Utils.m_dataPath + "SoundDB\\record.wav";

            bool fail = false;
            using (var reader = new WaveFileReader(recordedWavFile))
            {
                try
                {
                    MediaFoundationEncoder.EncodeToMp3(reader, filename);
                }
                catch (InvalidOperationException ex)
                {
                    fail = true;

                    File.Copy(Utils.m_dataPath + "SoundDB\\record.wav", wavFile);
                    filename = wavFile;

                    MMBase.TRACE("encoding audiofile to MP3...\r\n\r\n" + ex.Message + "\r\n\r\n" + ex.StackTrace);
                    MessageBox.Show("Error encoding audiofile to MP3...\r\n\r\nPlease find the 'OmniStix_logfile.txt' file in your Documents folder and send it to the support@palmaross.com\r\n\r\nThank you!",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }

            if (!fail)
            {
                try { File.Delete(recordedWavFile); } catch { }
            }

            string mappath = "", maptitle = "", topicguid = "";
            Topic t = MMUtils.SelectedTopic();

            if (addtotopic && t != null)
            {
                mappath = t.Document.FullName;
                maptitle = t.Document.CentralTopic.Text;
                topicguid = t.Guid;
            }

            int id = 0;
            using (StixDB db = new StixDB())
            {
                db.AddAudio(recordName, filename, mappath, maptitle, topicguid, groupID);
                DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);
            }

            string a_guid = "";
            if (attachment && t != null)
                a_guid = "###" + t.Attachments.Add(filename).Guid;

            if (addtotopic && t != null)
            {
                TransactionWrapper _w = new TransactionWrapper(t,
            TransactionWrapper.TransactionType.ADD_STRIP_ICON, id.ToString() + a_guid);
                _w.controlStripURI = StixMain.SOUNDSTRIP_URI;
                _w.Execute();
            }

            int i = cbRecords.Items.Add(new AudioItem(id, recordName, filename));
            cbRecords.SelectedIndex = i;
            panelRecordName.Visible = false;
            t = null;
        }

        int blink = 0;
        /// <summary>Audio ticker.</summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (writer != null) // Record ticker
            {
                var ts = writer.TotalTime;

                string mins = ts.Minutes.ToString();
                if (ts.Minutes < 10) mins = "0" + mins;
                string secs = ts.Seconds.ToString();
                if (ts.Seconds < 10) secs = "0" + secs;

                string playClock = mins + ":" + secs;
                lblClock.Text = playClock + AudioLength;

                // blinking icon
                if (blink++ % 2 == 0) pEmpty.Visible = true;
                else pEmpty.Visible = false;
            }
            else // Playback ticker
            {
                aTrack.Value = (int)audioFile.CurrentTime.TotalSeconds;

                var ts = audioFile.CurrentTime;
                string mins = ts.Minutes.ToString();
                if (ts.Minutes < 10) mins = "0" + mins;
                string secs = ts.Seconds.ToString();
                if (ts.Seconds < 10) secs = "0" + secs;

                string playClock = mins + ":" + secs;
                lblClock.Text = playClock + AudioLength;

                if (StixMain.m_TopicPlayer != null && StixMain.m_TopicPlayer.Visible)
                {
                    if (!StixMain.m_TopicPlayer.pPosition.Visible) // not busy with Volume text 
                        StixMain.m_TopicPlayer.lblClock.Text = playClock + AudioLength;

                    StixMain.m_TopicPlayer.tbTrack.Value = (int)audioFile.CurrentTime.TotalSeconds;
                }
            }
        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            int groupID = (cbGroupsSave.SelectedItem as AudioGroup).ID;

            if (txtRecordName.Text.Trim() == "") return;

            SaveRecord(txtRecordName.Text.Trim(), groupID, chAddToTopic.Checked, chAttachmentSave.Checked);
            panelRecordName.Visible = false;
        }

        private void btnCancelRecord_Click(object sender, EventArgs e)
        {
            panelRecordName.Visible = false;
            if (File.Exists(Utils.m_dataPath + "SoundDB\\record.wav"))
                File.Delete(Utils.m_dataPath + "SoundDB\\record.wav");
        }

        private void btnAddToTopic_Click(object sender, EventArgs e)
        {
            if (!(MMUtils.SelectedTopic() is Topic _t))
                return;

            if (cbRecords.Items.Count == 0) return;

            if (MMUtils.ActiveDocument.Path == "")
            {
                MessageBox.Show(Utils.getString("OmniStix.SaveMap"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            if (_t.ContainsControlStripType(StixMain.SOUNDSTRIP_URI))
            {
                MessageBox.Show(Utils.getString("OmniSound.topichasaudio"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id = (cbRecords.SelectedItem as AudioItem).ID;

            using (StixDB db = new StixDB())
                db.ExecuteNonQuery("update AUDIOS set " +
                    "mappath=`" + MMUtils.ActiveDocument.FullName + "`, " +
                    "maptitle=`" + MMUtils.ActiveDocument.CentralTopic.Text + "`, " +
                    "topicguid=`" + _t.Guid + "` where id=" + id + "");

            if (chAttachment.Checked) // Add attachment
                _t.Attachments.Add((cbRecords.SelectedItem as AudioItem).Path);

            // Add strip icon.
            TransactionWrapper _w = new TransactionWrapper(_t,
                TransactionWrapper.TransactionType.ADD_STRIP_ICON,
                (cbRecords.SelectedItem as AudioItem).Path);
            _w.controlStripURI = StixMain.SOUNDSTRIP_URI;
            _w.Execute();
        }

        private void OmniSound_MouseDown(object sender, MouseEventArgs e)
        {
            // move form
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void lblClock_Click(object sender, EventArgs e)
        {
            if (writer == null) return;

            RecordLimit += 5;
            AudioLength = " / " + RecordLimit + ":00";

            if (RecordLimit >= 30 && SystemAudio)
                MessageBox.Show(Utils.getString("OmniSound.RecordTimeWarning"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void Destroy()
        {
            writer?.Dispose();
            writer = null;
            waveIn?.Dispose();
            waveIn = null;
            capture?.Dispose();
            capture = null;

            audioFile?.Dispose();
            audioFile = null;
            outputDevice?.Dispose();
            outputDevice = null;

            this.Close();
        }

        public bool pause = false;
        string AudioLength = "";
        public bool SystemAudio = false;

        /// <summary> Path to audio file attached to topic.</summary>
        public string FilePath = "";

        public WaveInEvent waveIn = new WaveInEvent();
        public WaveFileWriter writer = null;

        public WaveOutEvent outputDevice = new WaveOutEvent();
        public AudioFileReader audioFile;

        WasapiLoopbackCapture capture = new WasapiLoopbackCapture();

        int RecordLimit = 10; // Min.



        #region Move the form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        #region Rounded corners
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
        #endregion

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            panelNewGroup.Location = new Point(panelRecordName.Left, panelRecordName.Bottom - panelNewGroup.Height);
            panelNewGroup.Visible = true;
            panelNewGroup.BringToFront();
        }
    }

    public class AudioItem
    {
        public AudioItem(int id, string name, string path)
        {
            Name = name;
            Path = path;
            ID = id;
        }

        public string Name;
        public string Path;
        public int ID;

        public override string ToString()
        {
            return Name;
        }
    }
    public class AudioGroup
    {
        public AudioGroup(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public string Name;
        public int ID;

        public override string ToString()
        {
            return Name;
        }
    }
}
