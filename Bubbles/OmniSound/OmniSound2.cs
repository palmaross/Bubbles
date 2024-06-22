using NAudio.Wave;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class OmniSound2 : Form
    {
        public OmniSound2()
        {
            InitializeComponent();

            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 30)
                {
                    waveIn.StopRecording();
                }
            };
        }

        private void ButtonRecord_Click(object sender, EventArgs e)
        {
            var outputFolder = Utils.m_dataPath + "SoundDB";
            var outputFilePath = Path.Combine(outputFolder, "record_test.wav");

            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            waveIn.StartRecording();
            buttonRecord.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Tag.ToString() == "record")
            {
                btnPause.Tag = "paused";
                waveIn.StopRecording();
            }
            else
            {
                btnPause.Tag = "record";
                waveIn.StartRecording();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            waveIn.StopRecording();
            writer?.Dispose();
            writer = null;
            waveIn.Dispose();
            buttonRecord.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void OmniSound2_FormClosing(object sender, FormClosingEventArgs e)
        {
            waveIn.StopRecording();
            writer?.Dispose();
            writer = null;
            waveIn.Dispose();
        }

        WaveFileWriter writer = null;
        WaveInEvent waveIn = new WaveInEvent();
    }
}
