using NAudio.Wave;
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
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace Bubbles
{
    public partial class ManageAudioDlg : Form
    {
        public ManageAudioDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "ManageAudio.htm");

            Group.Width = (int)(dgv.Width * 0.2);
            AudioTitle.Width = (int)(dgv.Width * 0.3);
            MapTitle.Width = (int)(dgv.Width * 0.3);
            aLength.Width = (int)(dgv.Width * 0.09);
            aSize.Width = (int)(dgv.Width * 0.09);

            Init();
        }

        void Init()
        {
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from AUDIOGROUPS");
                foreach (DataRow row in dt.Rows)
                    Groups.Add(Convert.ToInt32(row["id"]), row["name"].ToString());

                dt = db.ExecuteQuery("select * from AUDIOS order by title");

                foreach (DataRow row in dt.Rows)
                {
                    AddToTable(Convert.ToInt32(row["groupID"]), row["title"].ToString(),
                        row["path"].ToString(), row["maptitle"].ToString(), row["mappath"].ToString(),
                        row["topicguid"].ToString(), row["timepoints"].ToString());
                }
            }
        }

        public void AddToTable(int group, string title, string path, string map, string mappath, 
            string topicGuid, string timepoints)
        {
            int rowId = dgv.Rows.Add();
            DataGridViewRow row = dgv.Rows[rowId];

            // Get audio length.
            MediaFoundationReader mfr = new MediaFoundationReader(path);
            var ts = mfr.TotalTime;

            string mins = ts.Minutes.ToString();
            if (ts.Minutes < 10) mins = "0" + mins;
            string secs = ts.Seconds.ToString();
            if (ts.Seconds < 10) secs = "0" + secs;
            string length = mins + ":" + secs;

            // Get file size
            FileInfo fi = new FileInfo(path);
            float s = fi.Length / 1000F;
            string size = ((float)Math.Round(s, 2)).ToString();

            row.Cells["GroupID"].Value = group;
            row.Cells["Group"].Value = Groups[group];
            row.Cells["AudioTitle"].Value = title;
            row.Cells["AudioPath"].Value = title;
            row.Cells["MapTitle"].Value = map;
            row.Cells["MapPath"].Value = mappath;
            row.Cells["TopicGuid"].Value = topicGuid;
            row.Cells["TimePoints"].Value = timepoints;
            row.Cells["aLength"].Value = length;
            row.Cells["aSize"].Value = size;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Dictionary<int, string> Groups = new Dictionary<int, string>();
    }
}
