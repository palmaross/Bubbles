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
            cbSelectAll.Text = Utils.getString("SettingsDlg.cbSelectAll");

            gbScaleFactor.Text = Utils.getString("SettingsDlg.gbScaleFactor");
            lblStix.Text = Utils.getString("SettingsDlg.lblStix");
            lblStixBase.Text = Utils.getString("SettingsDlg.lblStixBase");
            lblBoxes.Text = Utils.getString("SettingsDlg.lblBoxes");
            btnTestScale.Text = Utils.getString("SettingsDlg.btnTestScale");

            btnSave.Text = Utils.getString("button.save");
            btnClose.Text = Utils.getString("button.close");

            // Fill sticks list
            using (StixDB db = new StixDB())
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
            }

            // Fill Scale Factor
            numStix.Text = stixScaleFactor.ToString() + "%";
            numStixBase.Text = stixbaseScaleFactor.ToString() + "%";
            //numBoxes.Text = boxesScaleFactor.ToString() + "%";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (StixDB db = new StixDB())
            {
                // Save to database starting sticks
                foreach (ListViewItem item in listRunAtStart.Items)
                {
                    int id = Convert.ToInt32(item.Tag);
                    int start = Convert.ToInt32(item.Checked);
                    db.ExecuteNonQuery("update STICKS set start=" + start + " where id=" + id + "");
                }
            }

            // Apply and save Scale Factor
            btnTestScale_Click(null, null);
            Utils.setRegistry("ScaleFactor_Stix", stixScaleFactor.ToString());
            Utils.setRegistry("ScaleFactor_StixBase", stixbaseScaleFactor.ToString());
            Utils.setRegistry("ScaleFactor_Boxes", boxesScaleFactor.ToString());
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

        private void cbStix_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb == cbStix)
                numStix.Text = cbStix.Text;
            else if (cb == cbStixBase)
                numStixBase.Text = cbStixBase.Text;
            else if (cb == cbBoxes)
                numBoxes.Text = cbBoxes.Text;
        }

        private void btnTestScale_Click(object sender, EventArgs e)
        {
            float SF_Stix, SF_StixBase, SF_Boxes;

            try { SF_Stix = Convert.ToInt32(numStix.Text.Trim('%').Trim());
            } catch { SF_Stix = 100; }

            try { SF_StixBase = Convert.ToInt32(numStixBase.Text.Trim('%').Trim());
            } catch { SF_StixBase = 100; }

            try { SF_Boxes = Convert.ToInt32(numBoxes.Text.Trim('%').Trim());
            } catch { SF_Boxes = 100; }

            foreach (var pair in StixButton.STICKS)
            {
                Form stick = pair.Value;
                if (stick == null) continue;

                switch (stick.Name)
                {
                    case StixUtils.typebase:
                        (stick as StixBase).ScaleStick((stick as StixBase).scaleFactor, SF_StixBase);
                        break;
                    case StixUtils.typeicons:
                        (stick as BubbleIcons).ScaleStick((stick as BubbleIcons).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typetaskinfo:
                        (stick as BubbleTaskInfo).ScaleStick((stick as BubbleTaskInfo).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typeaddtopic:
                        (stick as BubbleAddTopic).ScaleStick((stick as BubbleAddTopic).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typeformat:
                        (stick as BubbleFormat).ScaleStick((stick as BubbleFormat).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typetools:
                        (stick as BubbleTools).ScaleStick((stick as BubbleTools).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typebookmarks:
                        (stick as BubbleBookmarks).ScaleStick((stick as BubbleBookmarks).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typetextops:
                        (stick as BubbleTextOps).ScaleStick((stick as BubbleTextOps).scaleFactor, SF_Stix);
                        break;
                }
            }
            stixScaleFactor = SF_Stix;
            stixbaseScaleFactor = SF_StixBase;
        }

        private void numStix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e == null || e.KeyCode == Keys.Enter)
            {
                MaskedTextBox mtb = sender as MaskedTextBox;
                int value;

                try { value = Convert.ToInt32(mtb.Text.Trim('%').Trim());
                } catch { mtb.Text = "100%"; return; }

                if (value < 100) { mtb.Text = "100%"; return; }
                if (value > 300) { mtb.Text = "300%"; return; }

                if (e != null)
                {
                    e.Handled = true; // to avoid the "ding" sound
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void numStix_Leave(object sender, EventArgs e)
        {
            numStix_KeyDown(sender, null);
        }

        float stixScaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
        float stixbaseScaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_StixBase", "100"));
        float boxesScaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Boxes", "100"));
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
