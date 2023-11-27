using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ManageConfigsDlg : Form
    {
        /// <summary>
        /// Manage stick configurations
        /// </summary>
        /// <param name="Sticks">Sticks to create/update configuration with</param>
        public ManageConfigsDlg()
        {
            InitializeComponent();

            Text = Utils.getString("ManageConfigsDlg.Title");
            chboxRunAtStart.Text = Utils.getString("SettingsDlg.gbRunAtStart");
            chBoxVisibleSticks.Text = Utils.getString("ManageConfigsDlg.chBoxVisibleSticks");
            gbNewConfig.Text = Utils.getString("ManageConfigsDlg.gbNewConfig");
            btnCancel.Text = Utils.getString("button.cancel");
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
                        " where id=" + item.ID + "");

                    cbConfigs.Items.Remove(cbConfigs.SelectedIndex);
                    item.Name = newName;
                }
                else // create new configuration
                {
                    if (newName == oldName) return;
                    
                    db.AddConfig(newName, 0);
                    // Get config id
                    int id = 0;
                    dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);
                    item = new ConfigItem(newName, id);

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

            // Update _start_ configuration
            // First, set all configurations to unstart
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from CONFIGS");
                foreach (DataRow dr in dt.Rows)
                    db.ExecuteNonQuery("update CONFIGS set start=" + 0 + " where id=" + Convert.ToInt32(dr["id"]) + "");

                // And, if the configuration has to be started, set start state to it
                if (start == 1)
                    db.ExecuteNonQuery("update CONFIGS set start=1 where id=" + config.ID + "");
            }

            if (chBoxVisibleSticks.Checked)
            {
                Dictionary<int, Form> sticks = new Dictionary<int, Form>();

                foreach (var stick in BubblesButton.STICKS)
                    if (stick.Value.Visible)
                        sticks.Add(stick.Key, stick.Value);

                if (sticks.Count > 0)
                {
                    // Delete configuration's sticks in the database
                    using (BubblesDB db = new BubblesDB())
                        db.ExecuteNonQuery("delete from STICKS where configID=" + config.ID + "");
                    
                    // Add visible sticks to the configuration
                    StickUtils.CreateConfiguration(sticks, config.ID, start);
                }
            }
        }

        // Close dialog
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool edit = false;
    }
}
