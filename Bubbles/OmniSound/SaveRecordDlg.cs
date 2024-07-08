using AppManager;
using Mindjet.MindManager.Interop;
using NAudio.Wave;
using PRAManager;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class SaveRecordDlg : Form
    {
        public SaveRecordDlg(bool count, bool save)
        {
            InitializeComponent();

            lblGroup.Text = Utils.getString("SaveRecordDlg.lblGroup");
            lblRecordName.Text = Utils.getString("SaveRecordDlg.lblRecordName");
            chAttachment.Text = Utils.getString("SaveRecordDlg.chAttachment");
            chCloseRecorder.Text = Utils.getString("SaveRecordDlg.chCloseRecorder");
            btnSave.Text = Utils.getString("button.save");
            btnCancel.Text = Utils.getString("button.cancel");

            if (count)
            {
                panelSave.Visible = false;
                lblCount.Visible = true;
                lblCount.BringToFront();
                timer1.Start();
            }
            else if (save)
            {
                panelSave.Visible = true;
                lblCount.Visible = false;

                using (StixDB db = new StixDB())
                {
                    DataTable dt = db.ExecuteQuery("select * from AUDIOGROUPS order by name");
                    foreach (DataRow row in dt.Rows)
                        cbGroups.Items.Add(new AudioGroup(row["name"].ToString(), Convert.ToInt32(row["id"])));
                }

                if (cbGroups.Items.Count > 0)
                    cbGroups.SelectedIndex = 0;
            }
        }

        int i = 2;
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

            if (!(MMUtils.SelectedTopic() is Topic _t)) return;
            if (filename == "") return;

            if (_t.ContainsControlStripType(StixMain.SOUNDSTRIP_URI)) return;

            int groupID = (cbGroups.SelectedItem as AudioGroup).ID;
            StixMain.m_OmniSound.SaveRecord(filename, groupID, true, chAttachment.Checked);

            if (chCloseRecorder.Checked) StixMain.m_TopicRecorder.Close();
            cbGroupsIndex = cbGroups.SelectedIndex;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (chCloseRecorder.Checked) StixMain.m_TopicRecorder.Close();
            this.Close();
        }

        static int cbGroupsIndex = 0;
    }
}
