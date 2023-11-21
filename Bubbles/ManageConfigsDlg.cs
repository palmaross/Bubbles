using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ManageConfigsDlg : Form
    {
        public ManageConfigsDlg()
        {
            InitializeComponent();

            Text = Utils.getString("ManageConfigsDlg.Title");
            chboxRunAtStart.Text = Utils.getString("SettingsDlg.gbRunAtStart");
            gbNewConfig.Text = Utils.getString("ManageConfigsDlg.gbNewConfig");
            btnCancel.Text = Utils.getString("button.cancel");
            lblSticks.Text = Utils.getString("ManageConfigsDlg.lblSticks");
            btnSave.Text = Utils.getString("button.save");
            btnClose.Text = Utils.getString("button.close");

            toolTip1.SetToolTip(configDelete, Utils.getString("ManageConfigsDlg.configDelete"));
            toolTip1.SetToolTip(configNew, Utils.getString("ManageConfigsDlg.configNew"));
            toolTip1.SetToolTip(configEdit, Utils.getString("ManageConfigsDlg.configEdit"));

            using (BubblesDB db = new BubblesDB())
            {
                // Fill configuration combobox
                DataTable dt = db.ExecuteQuery("select * from CONFIGS order by name");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var item = new ConfigItem(dr["name"].ToString(), Convert.ToInt32(dr["id"]));
                        cbConfigs.Items.Add(item);
                    }
                }

                // Fill Stick list
                dt = db.ExecuteQuery("select * from STICKS order by type");
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new StickItem(Convert.ToInt32(dr["id"]), dr["name"].ToString());
                    listSticks.Items.Add(item);
                }

                if (cbConfigs.Items.Count > 0)
                    cbConfigs.SelectedIndex = 0;
                else
                {
                    configEdit.Enabled = false;
                    configDelete.Enabled = false;
                }
            }
        }

        private void configDelete_Click(object sender, EventArgs e)
        {
            var config = cbConfigs.SelectedItem as ConfigItem;

            if (MessageBox.Show(String.Format(Utils.getString("ManageConfigsDlg.deleteconfig"), config.Name), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (BubblesDB db = new BubblesDB())
                {
                    db.ExecuteNonQuery("delete from CONFIGS where id=" + config.ID + "");
                    db.ExecuteNonQuery("delete from STICKS where configID=" + config.ID + "");
                }

                cbConfigs.Items.Remove(config);
                if (cbConfigs.Items.Count <= 0)
                {
                    configDelete.Enabled = false;
                    configEdit.Enabled = false;
                }
            }
        }

        private void configNew_Click(object sender, EventArgs e)
        {
            gbNewConfig.Text = Utils.getString("ManageConfigsDlg.gbNewConfig.new");
            gbNewConfig.Visible = true;
            gbNewConfig.BringToFront();
            edit = false;
        }

        // Rename configuration
        private void configEdit_Click(object sender, EventArgs e)
        {
            gbNewConfig.Text = Utils.getString("ManageConfigsDlg.gbNewConfig.edit");
            gbNewConfig.Visible = true;
            gbNewConfig.BringToFront();
            edit = true;
        }

        // Configuration create or rename
        private void btnOK_Click(object sender, EventArgs e)
        {
            string newName = txtConfigName.Text.Trim();
            string oldName = "";
            if (String.IsNullOrEmpty(newName)) return;

            var item = cbConfigs.SelectedItem as ConfigItem;
            if (item != null) oldName = item.Name;
            int start = chboxRunAtStart.Checked ? 1 : 0;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from CONFIGS where name=`" + newName + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("ManageConfigsDlg.nameexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }

                if (edit) // update configuration name
                {
                    db.ExecuteNonQuery("update CONFIGS set " +
                        "name=`" + newName + "`, " +
                        "start=" + start +
                        " where id=" + item.ID + "");

                    cbConfigs.Items.Remove(cbConfigs.SelectedIndex);
                    item.Name = newName;
                }
                else // create new configuration
                {
                    if (newName == oldName) return;
                    
                    db.AddConfig(newName, start);
                    // Get config id
                    int id = 0;
                    dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);
                    item = new ConfigItem(newName, id);

                    while (listSticks.CheckedIndices.Count > 0)
                        listSticks.SetItemChecked(listSticks.CheckedIndices[0], false);

                    chboxRunAtStart.Checked = false;
                }

                cbConfigs.Items.Add(item);
                cbConfigs.SelectedItem = item;
                gbNewConfig.Visible = false;

                if (cbConfigs.Items.Count > 0)
                {
                    configEdit.Enabled = true;
                    configDelete.Enabled = true;
                }
            }
        }

        // Cancel configuration creating or renaming
        private void btnCancel_Click(object sender, EventArgs e)
        {
            gbNewConfig.Visible = false;
        }

        // Save changes
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbConfigs.Items.Count == 0) return;

            var config = cbConfigs.SelectedItem as ConfigItem;
            if (config == null) return;

            int start = chboxRunAtStart.Checked ? 1 : 0;

            using (BubblesDB db = new BubblesDB())
            {
                db.ExecuteNonQuery("update CONFIGS set " +
                    "start=" + start + " where id=" + config.ID + "");

                db.ExecuteNonQuery("delete from STICKS where configID=" + config.ID + "");

                foreach (StickItem _item in listSticks.SelectedItems)
                {
                    int id = _item.ID;

                }
            }
        }

        // Close dialog
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbConfigs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var config = (ConfigItem)cbConfigs.SelectedItem;

            // Get sticks for selected confuguration
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKS where configID=" + config.ID + "");
                if (dt.Rows.Count > 0)
                {
                    List<int> ids = new List<int>();
                    foreach (DataRow row in dt.Rows) ids.Add(Convert.ToInt32(row["id"]));

                    for (int i = 0; i < listSticks.Items.Count; i++)
                    {
                        var item = listSticks.Items[i] as StickItem;
                        if (ids.Contains(item.ID))
                            listSticks.SetItemChecked(i, true);
                        else
                            listSticks.SetItemChecked(i, false);
                    }
                }
            }
        }

        bool edit = false;
    }
}
