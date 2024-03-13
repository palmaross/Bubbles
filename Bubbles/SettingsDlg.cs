using System;
using System.Data;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class SettingsDlg : Form
    {
        public SettingsDlg()
        {
            InitializeComponent();

            Text = Utils.getString("SettingsDlg.Title");
            gbRunAtStart.Text = Utils.getString("SettingsDlg.gbRunAtStart");
            rbtnSticks.Text = Utils.getString("SettingsDlg.rbtnSticks");
            cbSelectAll.Text = Utils.getString("SettingsDlg.cbSelectAll");
            rbtnConfiguration.Text = Utils.getString("SettingsDlg.rbtnConfiguration");

            btnSave.Text = Utils.getString("button.save");
            btnClose.Text = Utils.getString("button.close");

            // Fill sticks list
            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKS order by type");
                bool allselected = true;
                foreach (DataRow dr in dt.Rows)
                {
                    var item = listRunAtStart.Items.Add(dr["name"].ToString());
                    item.Tag = dr["id"].ToString();
                    item.Checked = dr["start"].ToString() == "1";
                    if (!item.Checked) allselected = false;
                }
                if (allselected)
                    cbSelectAll.Checked = true;


                // Fill configurations
                dt = db.ExecuteQuery("select * from CONFIGS order by name");
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new ConfigItem(dr["name"].ToString(), Convert.ToInt32(dr["id"]));
                    cbConfiguration.Items.Add(item);
                }
                if (cbConfiguration.Items.Count > 0)
                    cbConfiguration.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SticksDB db = new SticksDB())
            {
                if (rbtnSticks.Checked)
                {
                    // Save to database starting sticks
                    foreach (ListViewItem item in listRunAtStart.Items)
                    {
                        int id = Convert.ToInt32(item.Tag);
                        int start = Convert.ToInt32(item.Checked);
                        db.ExecuteNonQuery("update STICKS set start=" + start + " where id=" + id + "");
                    }
                    // Disable starting configuration
                    db.ExecuteNonQuery("update CONFIGS set start=" + 0 + " where start=" + 1 + "");
                }
                else if (rbtnConfiguration.Checked)
                {
                    if (cbConfiguration.Items.Count > 0)
                    {
                        db.ExecuteNonQuery("update CONFIGS set start=" + 0 + " where start=" + 1 + "");
                        var item = cbConfiguration.SelectedItem as ConfigItem;
                        // Save to database starting configuration
                        db.ExecuteNonQuery("update CONFIGS set start=" + 1 + " where id=" + item.ID + "");
                        // Disable starting sticks
                        db.ExecuteNonQuery("update STICKS set start=" + 0 + " where start=" + 1 + "");
                    }
                }
            }
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll.Checked) {
                foreach (ListViewItem item in listRunAtStart.Items)
                    item.Checked = true;
            }
            else {
                foreach (ListViewItem item in listRunAtStart.Items)
                    item.Checked = false;
            }
        }
    }

    public class ConfigItem
    {
        public ConfigItem(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name = "";
        public int ID = 0;
    }
}
