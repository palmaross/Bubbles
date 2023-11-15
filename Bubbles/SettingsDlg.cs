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
            groupBoxRunAtStart.Text = Utils.getString("SettingsDlg.lblRunAtStart");
            cbSelectAll.Text = Utils.getString("SettingsDlg.cbSelectAll");

            btnSave.Text = Utils.getString("button.save");
            btnClose.Text = Utils.getString("button.close");

            // Fill RunAtStart list
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKS order by id");
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
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (BubblesDB db = new BubblesDB())
            {
                if (startchanged)
                {
                    foreach (ListViewItem item in listRunAtStart.Items)
                    {
                        int id = Convert.ToInt32(item.Tag);
                        int start = Convert.ToInt32(item.Checked);
                        db.ExecuteNonQuery("update STICKS set start=" + start + " where id=" + id + "");
                    }
                }
            }
        }

        private void listRunAtStart_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            startchanged = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        bool startchanged = false;
    }
}
