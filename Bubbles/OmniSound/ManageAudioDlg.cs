using Mindjet.MindManager.Interop;
using NAudio.Wave;
using PRAManager;
using PRMapCompanion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;

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

            Text = Utils.getString("ManageAudioDlg.title");
            btnManageGroups.Text = Utils.getString("ManageAudioDlg.btnManageGroups");
            btnAddAudio.Text = Utils.getString("ManageAudioDlg.btnAddAudio");
            btnPlay.Text = "  " + Utils.getString("ManageAudioDlg.btnPlay");
            btnClose.Text = Utils.getString("button.close");

            btnNewGroup.Text = Utils.getString("ManageAudioDlg.btnNewGroup");
            btnRenameGroup.Text = Utils.getString("buttton.rename");
            btnDeleteGroup.Text = Utils.getString("buttton.delete");
            lblGroupName.Text = Utils.getString("ManageAudioDlg.lblGroupName");
            btnSave.Text = Utils.getString("button.save");
            btnCloseManage.Text = Utils.getString("button.close");

            lblAddToGroup.Text = Utils.getString("ManageAudioDlg.lblAddToGroup");
            lblPathToAudio.Text = Utils.getString("ManageAudioDlg.lblPathToAudio");
            lblAudioTitle.Text = Utils.getString("ManageAudioDlg.AudioTitle2");
            btnAddFile.Text = Utils.getString("button.add");
            btnCancel.Text = Utils.getString("button.cancel");

            Image img = Image.FromFile(Utils.m_imagesPath + "audio.ico");
            img = new Bitmap(img, p1.Size);
            btnPlay.Image = img;

            Group.HeaderText = Utils.getString("ManageAudioDlg.GroupHeader");
            AudioTitle.HeaderText = Utils.getString("ManageAudioDlg.AudioTitle");
            MapTitle.HeaderText = Utils.getString("ManageAudioDlg.MapTitle");
            aLength.HeaderText = Utils.getString("ManageAudioDlg.aLength");
            aSize.HeaderText = Utils.getString("ManageAudioDlg.aSize");

            m_OpenInFolder.Text = Utils.getString("ManageAudioDlg.btnOpenFile");
            m_OpenInMap.Text = Utils.getString("ManageAudioDlg.btnOpenMap");
            m_Delete.Text = Utils.getString("button.delete");
            m_Play.Text = Utils.getString("ManageAudioDlg.m_Play");
            m_Rename.Text = Utils.getString("ManageAudioDlg.m_Rename");

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            this.HelpButtonClicked += this_HelpButtonClicked;
            dgv.CellBeginEdit += Dgv_CellBeginEdit;
            dgv.CellEndEdit += Dgv_CellEndEdit;

            Group.Width = (int)(dgv.Width * 0.22);
            AudioTitle.Width = (int)(dgv.Width * 0.28);
            MapTitle.Width = (int)(dgv.Width * 0.28);
            aLength.Width = (int)(dgv.Width * 0.08);
            aSize.Width = (int)(dgv.Width * 0.11);

            this.MinimumSize = new Size((int)(this.Width / 2), this.Height / 2);
            this.MaximumSize = new Size(this.Width, Screen.AllScreens.Max(s => s.Bounds.Height));

            Init();
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == m_Rename)
            {
                dgv.CurrentCell = clickedCell;
                dgv.BeginEdit(true);
            }
            if (e.ClickedItem == m_Delete)
            {
                DeleteAudio();
            }
            if (e.ClickedItem == m_Play)
            {
                btnPlay_Click(null, null);
            }
            if (e.ClickedItem == m_OpenInFolder)
            {
                string path = dgv.SelectedRows[0].Cells["AudioPath"].Value.ToString();

                if (File.Exists(path))
                {
                    string argument = "/select, \"" + path + "\"";
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                }
                else
                {
                    MessageBox.Show(String.Format(Utils.getString("ManageAudioDlg.filenotexists"), path));
                }
            }
            if (e.ClickedItem == m_OpenInMap)
            {
                string path = dgv.SelectedRows[0].Cells["MapPath"].Value.ToString();
                string topicGuid = dgv.SelectedRows[0].Cells["TopicGuid"].Value.ToString();

                Document doc = DocumentStorage.GetOrOpenDocument(path);
                if (doc != null)
                {
                    Topic t = doc.FindByGuid(topicGuid) as Topic;
                    if (t != null)
                    {
                        doc.Activate();
                        t.SelectOnly();
                        t.SnapIntoView();
                        StixUtils.ActivateMindManager();
                    }
                    t = null; doc = null;
                }
            }
        }

        private void this_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "ManageAudio.htm");
        }

        void Init()
        {
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from AUDIOGROUPS");
                foreach (DataRow row in dt.Rows)
                    Groups.Add(Convert.ToInt32(row["id"]), row["name"].ToString());

                foreach (var item in Groups)
                {
                    AudioGroup ag = new AudioGroup(item.Value, item.Key);
                    cbGroups.Items.Add(ag); cbGroupsManage.Items.Add(ag);
                    if (cbGroups.Items.Count > 0) cbGroups.SelectedIndex = 0;
                    if (cbGroupsManage.Items.Count > 0) cbGroupsManage.SelectedIndex = 0;
                }

                dt = db.ExecuteQuery("select * from AUDIOS order by title");

                foreach (DataRow row in dt.Rows)
                {
                    string path = row["path"].ToString();
                    if (File.Exists(path))
                    {
                        AddToTable(Convert.ToInt32(row["id"]), Convert.ToInt32(row["groupID"]), row["title"].ToString(),
                            path, row["maptitle"].ToString(), row["mappath"].ToString(),
                            row["topicguid"].ToString(), row["timepoints"].ToString());
                    }
                    else
                        db.ExecuteNonQuery("delete from AUDIOS where path=`" + path + "`");
                }
            }
        }

        public void AddToTable(int id, int group, string title, string path, string map, string mappath, 
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

            row.Cells["Group"].Value = Groups[group];
            row.Cells["AudioTitle"].Value = title;
            row.Cells["MapTitle"].Value = map;
            row.Cells["aLength"].Value = length;
            row.Cells["aSize"].Value = size;
            row.Cells["ID"].Value = id;
            row.Cells["GroupID"].Value = group;
            row.Cells["AudioPath"].Value = path;
            row.Cells["MapPath"].Value = mappath;
            row.Cells["TopicGuid"].Value = topicGuid;
            row.Cells["TimePoints"].Value = timepoints;

            if (mappath == "")
                row.Cells["MapTitle"].ReadOnly = true;
        }


        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;

                var htinfo = dgv.HitTest(e.X, e.Y);
                if (htinfo.Type == DataGridViewHitTestType.Cell)
                    clickedCell = dgv.Rows[htinfo.RowIndex].Cells[htinfo.ColumnIndex];

                if (htinfo.RowIndex < 0) return;

                if (dgv.Rows[htinfo.RowIndex].Selected == false)
                {
                    dgv.ClearSelection();
                    dgv.Rows[htinfo.RowIndex].Selected = true;
                }

                if (dgv.SelectedRows.Count > 1)
                {
                    foreach (ToolStripItem item in contextMenuStrip1.Items)
                        item.Visible = false;
                    m_Delete.Visible = true;
                }
                else // one row selected
                {
                    // If cell is renameable...
                    if (htinfo.ColumnIndex == 0 || htinfo.ColumnIndex > 2 || // Length and Size cann not be modified
                        (htinfo.ColumnIndex == 2 &&
                        dgv.Rows[htinfo.RowIndex].Cells["MapPath"].Value.ToString() == "")) // audio is not in a map
                        m_Rename.Visible = false;
                    if (dgv.Rows[htinfo.RowIndex].Cells["MapPath"].Value.ToString() == "")
                        m_OpenInMap.Visible = false;
                }
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cellOldValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }
        string cellOldValue = "";

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string cellNewValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string column = dgv.Columns[e.ColumnIndex].Name;

            if (cellNewValue != cellOldValue)
            {
                int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ID"].Value);

                using (StixDB db = new StixDB())
                {
                    if (column == "AudioTitle")
                        db.ExecuteNonQuery("update AUDIOS " +
                            "set title=`" + cellNewValue + "` where id=" + id);
                    else if (column == "MapTitle")
                        db.ExecuteNonQuery("update AUDIOS " +
                            "set maptitle=`" + cellNewValue + "` where id=" + id);
                }
            }
            cellOldValue = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            lblGroupName.Visible = true;
            txtGroupName.Text = "";
            txtGroupName.Visible = true;
            btnSave.Tag = "NewGroup";
        }

        private void btnRenameGroup_Click(object sender, EventArgs e)
        {
            if (cbGroupsManage.SelectedIndex < 0) return;

            lblGroupName.Visible = true;
            txtGroupName.Text = cbGroupsManage.Text + " (1)";
            txtGroupName.Visible = true;
            btnSave.Tag = "RenameGroup";
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            lblGroupName.Visible = false;
            txtGroupName.Visible = false;

            AudioGroup ag = cbGroupsManage.SelectedItem as AudioGroup;
            if (ag.ID == 1)
            {
                MessageBox.Show(Utils.getString("ManageAudioDlg.delete.defaultgroup"));
                return;
            }

            if (MessageBox.Show(Utils.getString("ManageAudioDlg.deletegroup"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            
            int groupID = ag.ID;

            using (StixDB db = new StixDB())
            {
                db.ExecuteNonQuery("delete from AUDIOGROUPS where id=" + groupID + "");
                db.ExecuteNonQuery("delete from AUDIOS where groupID=" + groupID + "");
            }

            cbGroups.Items.Remove(ag); cbGroupsManage.Items.Remove(ag);
            StixMain.m_OmniSound.InitAudioFiles();
            if (cbGroupsManage.Items.Count > 0)
                cbGroupsManage.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtGroupName.Text.Trim();
            if (name == "") return;

            if (cbGroupsManage.SelectedItem == null) return;

            AudioGroup ag = cbGroupsManage.SelectedItem as AudioGroup;

            string _name = Utils.VS(name);
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from AUDIOGROUPS where name=`" + name + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.groupexists"));
                    return;
                }

                if (btnSave.Tag.ToString() == "NewGroup")
                {
                    db.AddAudioGroup(Utils.VS(name)); int id = 0;
                    dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                    ag = new AudioGroup(name, id);
                    int i = cbGroups.Items.Add(ag); cbGroupsManage.Items.Add(ag);
                    cbGroupsManage.SelectedIndex = i;
                    StixMain.m_OmniSound.InitAudioFiles();
                }
                else if (btnSave.Tag.ToString() == "RenameGroup")
                {
                    db.ExecuteNonQuery("update AUDIOGROUPS set name=`" + Utils.VS(name) + "` where id=" + ag.ID + "");
                
                    cbGroupsManage.Items.Remove(ag);
                    cbGroups.Items.Remove(ag);
                    string oldname = ag.Name; ag.Name = name; 
                    int i = cbGroupsManage.Items.Add(ag);
                    cbGroups.Items.Add(ag);
                    cbGroupsManage.SelectedIndex = i;
                    StixMain.m_OmniSound.InitAudioFiles();
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells["GroupID"].Value.ToString() == ag.ID.ToString())
                            row.Cells["Group"].Value = name;
                    }
                }
                lblGroupName.Visible = false;
                txtGroupName.Visible = false;
            }
        }

        private void btnManageGroups_Click(object sender, EventArgs e)
        {
            panelManageGroups.Visible = true;
            panelAddAudio.Visible = false;
            panelManageGroups.BringToFront();
        }

        private void btnCancelManage_Click(object sender, EventArgs e)
        {
            panelManageGroups.Visible = false;
        }

        private void btnAddAudio_Click(object sender, EventArgs e)
        {
            panelAddAudio.Visible = true; panelAddAudio.BringToFront();
            panelAddAudio.Location = new Point(panelManageGroups.Location.X, panelAddAudio.Location.Y);
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.Trim();
            if (path == "") return;

            int groupID = (cbGroups.SelectedItem as AudioGroup).ID;
            string title = Path.GetFileNameWithoutExtension(path);

            using (StixDB db = new StixDB())
            {
                db.AddAudio(title, path, "", "", "", groupID);

                int id = 0;
                DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                AddToTable(id, groupID, title, path, "", "", "", "");
            }
            panelAddAudio.Visible = false;
            txtPath.Text = ""; txtAudioTitle.Text = "";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Audio Files (*.mp3, *.wav, *.mp4, *wma, *.aac, *m4a)|*.mp3;*.wav;*.mp4;*wma;*.aac;*m4a;|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel) return;
            txtPath.Text = openFileDialog1.FileName;

            txtAudioTitle.Text = Path.GetFileNameWithoutExtension(txtPath.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelAddAudio.Visible = false;
            txtPath.Text = ""; txtAudioTitle.Text = "";
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteAudio();
        }

        void DeleteAudio()
        {
            if (MessageBox.Show(Utils.getString("ManageAudioDlg.deleteaudio"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (StixDB db = new StixDB())
            {
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    string path = row.Cells["AudioPath"].Value.ToString();

                    db.ExecuteNonQuery("delete from AUDIOS where id=" + id + "");
                    dgv.Rows.Remove(row);

                    // Delete file
                    if (File.Exists(path)) File.Delete(path);
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!StixMain.m_OmniSound.Visible)
            {
                int locx = (StixMain.m_OmniSound.Width - StixMain.OmniStixButton.Width) / 2;
                StixMain.m_OmniSound.Location = new Point(StixMain.OmniStixButton.Left - locx, StixMain.OmniStixButton.Bottom);
                StixMain.m_OmniSound.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }

            string path = dgv.SelectedRows[0].Cells["AudioPath"].Value.ToString();
            StixMain.m_OmniSound.FilePath = path;
            StixMain.m_OmniSound.btnPlay_Click(null, null);
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            btnPlay_Click(null, null);
        }

        Dictionary<int, string> Groups = new Dictionary<int, string>();
        DataGridViewCell clickedCell;
    }
}
