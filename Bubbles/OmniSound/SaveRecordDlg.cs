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

            this.Paint += this_Paint; // paint form border

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

        private void this_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
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

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            panelNewGroup.Visible = true;
            panelNewGroup.BringToFront();
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
                cbGroups.Items.Add(item); cbGroups.SelectedIndex = i;

                panelNewGroup.Visible = false;
            }
        }

        private void btnCancelGroup_Click(object sender, EventArgs e)
        {
            panelNewGroup.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (chCloseRecorder.Checked) StixMain.m_TopicRecorder.Close();
            this.Close();
        }

        static int cbGroupsIndex = 0;
    }
}
