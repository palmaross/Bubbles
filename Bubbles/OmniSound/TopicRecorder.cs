using Mindjet.MindManager.Interop;
using NAudio.Wave;
using PRAManager;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class TopicRecorder : Form
    {
        public TopicRecorder(Rectangle omniButton, string topicGuid)
        {
            InitializeComponent();

            toolTip1.SetToolTip(pictureHandle, Utils.getString("TopicRecorder.Title") + Utils.getString("HeadIcon.tooltip"));
            toolTip1.SetToolTip(btnRecord, Utils.getString("TopicRecorder.btnRecord"));
            toolTip1.SetToolTip(btnPause, Utils.getString("TopicRecorder.btnPause"));
            toolTip1.SetToolTip(btnRecordEmpty, Utils.getString("TopicRecorder.btnRecord"));
            toolTip1.SetToolTip(btnStop, Utils.getString("OmniSound.btnStop.tooltip"));

            OmniButton = omniButton;
            ThisSize = this.Size;

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            try
            {
                // Works only on Windows 11!
                DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            }
            catch { }

            pictureHandle.MouseDown += (sender, e) => // move the stick
            {
                if (e.Clicks == 1 && e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };

            pictureHandle.MouseDoubleClick += (sender, e) => // move the stick
            {
                if (StixMain.m_OmniSound.writer != null)
                {
                    StixMain.m_OmniSound.waveIn.StopRecording();
                    StixMain.m_OmniSound.omniRecorder = OmniSound.OmniRecorder.Stopped;
                    StixMain.m_OmniSound.writer?.Dispose();
                    StixMain.m_OmniSound.writer = null;
                }
                this.Close();
            };

            btnRecordEmpty.Location = btnRecord.Location;
        }

        private void TopicRecorder_Load(object sender, EventArgs e)
        {
            this.Size = ThisSize;
            int locx = (OmniButton.Width - this.Width) / 2;

            this.Location = new Point(
                OmniButton.X + locx,
                OmniButton.Y + OmniButton.Height);
        }
        Rectangle OmniButton;
        Size ThisSize;

        private void btnRecord_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (StixMain.m_OmniSound.writer == null)
                {
                    Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
                    if (t == null) return;

                    if (t.ContainsControlStripType(StixMain.SOUNDSTRIP_URI))
                    {
                        MessageBox.Show(Utils.getString("OmniSound.topichasaudio"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        t = null; return;
                    }
                    t = null;

                    var outputFolder = Utils.m_dataPath + "SoundDB";
                    var outputFilePath = Path.Combine(outputFolder, "record.wav");

                    StixMain.m_OmniSound.writer = new WaveFileWriter(outputFilePath, StixMain.m_OmniSound.waveIn.WaveFormat);
                    StixMain.m_OmniSound.waveIn.StartRecording();
                    StixMain.m_OmniSound.omniRecorder = OmniSound.OmniRecorder.Capturing;
                    StixMain.m_OmniSound.waveIn.DataAvailable += StixMain.m_OmniSound.WaveIn_DataAvailable;

                    if (StixMain.m_OmniSound.Visible)
                    {
                        StixMain.m_OmniSound.Opacity = 0.2;
                        StixMain.m_OmniSound.btnStop.Enabled = false;
                        StixMain.m_OmniSound.btnPause.Enabled = false;
                    }

                    timer1.Start();
                }
                else
                {
                    MessageBox.Show(Utils.getString("OmniSound.busy.recording"));
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnRecord.Visible = true;
            btnRecordEmpty.Visible = false;

            StixMain.m_OmniSound.waveIn.StopRecording();
            StixMain.m_OmniSound.omniRecorder = OmniSound.OmniRecorder.Stopped;
            StixMain.m_OmniSound.waveIn.DataAvailable -= StixMain.m_OmniSound.WaveIn_DataAvailable;
            StixMain.m_OmniSound.writer?.Dispose();
            StixMain.m_OmniSound.writer = null;

            StixMain.m_OmniSound.Opacity = 1;
            StixMain.m_OmniSound.btnStop.Enabled = true;
            StixMain.m_OmniSound.btnPause.Enabled = true;

            using (SaveRecordDlg dlg = new SaveRecordDlg(false, true))
            {
                int locx = (dlg.Width - this.Width) / 2;
                dlg.Location = new Point(this.Left - locx, this.Bottom);
                dlg.txtName.Text = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }

            MMUtils.ActiveDocument.Save();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (StixMain.m_OmniSound.writer == null) return;

            if (StixMain.m_OmniSound.omniRecorder == OmniSound.OmniRecorder.Capturing)
            {
                StixMain.m_OmniSound.waveIn.StopRecording();
                StixMain.m_OmniSound.omniRecorder = OmniSound.OmniRecorder.Paused;
                timer1.Stop();
                btnRecord.Visible = true;
                btnRecordEmpty.Visible = false;
            }
            else
            {
                StixMain.m_OmniSound.omniRecorder = OmniSound.OmniRecorder.Capturing;
                //btnPause.Tag = "record";
                StixMain.m_OmniSound.waveIn.StartRecording();
                StixMain.m_OmniSound.omniRecorder = OmniSound.OmniRecorder.Capturing;
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (btnRecordEmpty.Visible)
            {
                btnRecordEmpty.Visible = false;
                btnRecord.Visible = true;
            }
            else
            {
                btnRecordEmpty.Visible = true;
                btnRecord.Visible = false;
            }
        }

        #region Rounded corners, Drag form
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

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        // For release capture
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion
    }
}
