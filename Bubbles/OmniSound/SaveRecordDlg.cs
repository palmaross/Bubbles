using AppManager;
using Mindjet.MindManager.Interop;
using NAudio.Wave;
using PRAManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class SaveRecordDlg : Form
    {
        public SaveRecordDlg(bool count, bool save)
        {
            InitializeComponent();

            if (count)
            {
                panelSave.Visible = false;
                lblCount.Visible = true;
                timer1.Start();
            }
            else if (save)
            {
                panelSave.Visible = true;
                lblCount.Visible = false;
            }
        }

        int i = 4;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCount.Text = i.ToString();
            if (i-- < 1)
            {
                timer1.Stop();
                StixMain.m_TopicRecorder.timer1.Start(); // blinking icon

                if (StixMain.m_OmniSound.writer == null)
                {
                    var outputFolder = Utils.m_dataPath + "SoundDB";
                    var outputFilePath = Path.Combine(outputFolder, "record.wav");

                    StixMain.m_OmniSound.writer = new WaveFileWriter(outputFilePath, StixMain.m_OmniSound.waveIn.WaveFormat);
                    StixMain.m_OmniSound.waveIn.DataAvailable += StixMain.m_OmniSound.WaveIn_DataAvailable;
                    StixMain.m_OmniSound.waveIn.StartRecording();
                    StixMain.m_OmniSound.omniRecorder = OmniSound.OmniRecorder.Capturing;
                }

                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string filename = txtName.Text.Trim();
            string outputFolder = Utils.m_dataPath + "SoundDB\\";

            StixMain.m_OmniSound.SaveRecord(filename);

            if (!(MMUtils.SelectedTopic() is Topic _t))
                return;

            if (_t.ContainsControlStripType(StixMain.SOUNDSTRIP_URI))
                return;

            if (chAttachment.Checked) // Add as attachment
                _t.Attachments.Add(outputFolder + filename + ".mp3");

            TransactionWrapper _w = new TransactionWrapper(_t,
                TransactionWrapper.TransactionType.ADD_STRIP_ICON, outputFolder + filename + ".mp3");
            _w.controlStripURI = StixMain.SOUNDSTRIP_URI;
            _w.Execute();

            if (chCloseRecorder.Checked) StixMain.m_TopicRecorder.Close();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (chCloseRecorder.Checked) StixMain.m_TopicRecorder.Close();
            this.Close();
        }
    }
}
