using System;
using System.Data;
using System.Drawing;
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

            gbScaleFactor.Text = Utils.getString("SettingsDlg.gbScaleFactor");
            lblStix.Text = Utils.getString("SettingsDlg.lblStix");
            lblStixBase.Text = Utils.getString("SettingsDlg.lblStixBase");
            lblBoxes.Text = Utils.getString("SettingsDlg.lblBoxes");
            btnTestScale1.Text = Utils.getString("SettingsDlg.btnTestScale");

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

            // Fill Scale Factor
            numStix.Text = stixScaleFactor.ToString() + "%";
            numStixBase.Text = stixbaseScaleFactor.ToString() + "%";
            numBoxes.Text = boxesScaleFactor.ToString() + "%";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (StixDB db = new StixDB())
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

            // Save Scale Factor
            Utils.setRegistry("ScaleFactor_Stix", numStix.Text.Trim('%'));
            Utils.setRegistry("ScaleFactor_StixBase", numStixBase.Text.Trim('%'));
            Utils.setRegistry("ScaleFactor_Boxes", numBoxes.Text.Trim('%'));
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
            float scaleFactor;

            if (sender == btnTestScale1) // All visible stix
            {
                try { scaleFactor = Convert.ToInt32(numStix.Text.Trim('%').Trim());
                } catch { return; }

                foreach (var pair in BubblesButton.STICKS)
                {
                    Form stick = pair.Value;
                    if (stick.Name == "StixBase" || !stick.Visible) continue;

                    switch (stick.Name)
                    {
                        case StickUtils.typeicons:
                            (stick as BubbleIcons).ScaleStick(stixbaseScaleFactor, scaleFactor);
                            break;
                        case StickUtils.typetaskinfo:
                            (stick as BubbleTaskInfo).ScaleStick(stixbaseScaleFactor, scaleFactor);
                            break;
                        case StickUtils.typeaddtopic:
                            (stick as BubbleAddTopic).ScaleStick(stixbaseScaleFactor, scaleFactor);
                            break;
                        case StickUtils.typeformat:
                            (stick as BubbleFormat).ScaleStick(stixbaseScaleFactor, scaleFactor);
                            break;
                        case StickUtils.typesources:
                            (stick as BubbleMySources).ScaleStick(stixbaseScaleFactor, scaleFactor);
                            break;
                        case StickUtils.typebookmarks:
                            (stick as BubbleBookmarks).ScaleStick(stixbaseScaleFactor, scaleFactor);
                            break;
                        case StickUtils.typetextops:
                            (stick as BubbleTextOps).ScaleStick(stixbaseScaleFactor, scaleFactor);
                            break;
                    }
                }
                stixScaleFactor = scaleFactor;
            }
            else if (sender == btnTestScale2) // Base stick
            {
                try { scaleFactor = Convert.ToInt32(numStixBase.Text.Trim('%').Trim());
                } catch { return; }

                BubblesButton.m_StixBase.ScaleStick(stixbaseScaleFactor, scaleFactor);
                stixbaseScaleFactor = scaleFactor;
            }
            else if(sender == btnTestScale3) // Boxes
            {
                
            }
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
                if (value > 267) { mtb.Text = "267%"; return; }

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
