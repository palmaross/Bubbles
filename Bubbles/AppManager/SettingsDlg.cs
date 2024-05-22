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

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "Settings.htm");

            Text = Utils.getString("SettingsDlg.Title");
            gbRunAtStart.Text = Utils.getString("SettingsDlg.gbRunAtStart");
            cbSelectAll.Text = Utils.getString("SettingsDlg.cbSelectAll");

            gbScaleFactor.Text = Utils.getString("SettingsDlg.gbScaleFactor");
            lblStix.Text = Utils.getString("SettingsDlg.lblStix");
            lblStixBase.Text = Utils.getString("SettingsDlg.lblStixBase");
            lblBoxes.Text = Utils.getString("SettingsDlg.lblBoxes");
            btnTestScale.Text = Utils.getString("SettingsDlg.btnTestScale");

            QTR_Dates.Text = Utils.getString("taskinfo.Dates");
            QTR_Progress.Text = Utils.getString("SettingsDlg.QTR_Progress");
            QTR_Priority.Text = Utils.getString("SettingsDlg.QTR_Priority");
            QTR_Resources.Text = Utils.getString("taskinfo.Resources");
            QTR_Effort.Text = Utils.getString("taskinfo.numEffort.tooltip");

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

            // Fill Quick Task Remove Defaults
            string qtr_defaults = Utils.getRegistry("QuickTaskRemoveDefaults", "");
            if (qtr_defaults != "")
            {
                bool taskinfostix = StixMain.m_TaskInfo != null;
                string[] parts = qtr_defaults.Split(';');
                foreach (string part in parts)
                {
                    string[] parts2 = part.Split(':');
                    switch (parts2[0])
                    {
                        case "dates": QTR_Dates.Checked = parts2[1] == "1"; break;
                        case "progress": QTR_Progress.Checked = parts2[1] == "1"; break;
                        case "priority": QTR_Priority.Checked = parts2[1] == "1"; break;
                        case "resources": QTR_Resources.Checked = parts2[1] == "1"; break;
                        case "effort": QTR_Effort.Checked = parts2[1] == "1"; break;
                    }
                }
            }

            // Fill Scale Factor
            numStix.Text = stixScaleFactor.ToString() + "%";
            numStixBase.Text = stixbaseScaleFactor.ToString() + "%";
            
            FaviconsToolStix.Checked = Utils.getRegistry("FaviconsToolStix", "1") == "1";
            FaviconsLinksWindow.Checked = Utils.getRegistry("FaviconsLinksWindow", "1") == "1";

            this.HelpButtonClicked += this_HelpButtonClicked;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "Settings.htm");
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

            // Save Quick Task Remove Defaults
            string defaults = "dates:" + (QTR_Dates.Checked ? "1" : "0") + ";";
            defaults += "priority:" + (QTR_Priority.Checked ? "1" : "0") + ";";
            defaults += "progress:" + (QTR_Progress.Checked ? "1" : "0") + ";";
            defaults += "resources:" + (QTR_Resources.Checked ? "1" : "0") + ";";
            defaults += "effort:" + (QTR_Effort.Checked ? "1" : "0");
            Utils.setRegistry("QuickTaskRemoveDefaults", defaults);

            if (StixMain.m_TaskInfo != null)
                StixMain.m_TaskInfo.SetQuickTaskDefault();

            Utils.setRegistry("FaviconsToolStix", FaviconsToolStix.Checked ? "1" : "0");
            Utils.setRegistry("FaviconsLinksWindow", FaviconsLinksWindow.Checked ? "1" : "0");
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
            float SF_Stix, SF_StixBase;

            try { SF_Stix = Convert.ToInt32(numStix.Text.Trim('%').Trim());
            } catch { SF_Stix = 100; }

            try { SF_StixBase = Convert.ToInt32(numStixBase.Text.Trim('%').Trim());
            } catch { SF_StixBase = 100; }

            foreach (var pair in StixMain.STICKS)
            {
                Form stick = pair.Value;
                if (stick == null) continue;

                switch (stick.Name)
                {
                    case StixUtils.typebase:
                        (stick as StartMenu).ScaleStick((stick as StartMenu).scaleFactor, SF_StixBase);
                        break;
                    case StixUtils.typeicons:
                        (stick as StixIcons).ScaleStick((stick as StixIcons).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typetaskinfo:
                        (stick as StixTaskInfo).ScaleStick((stick as StixTaskInfo).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typeaddtopic:
                        (stick as StixAddTopic).ScaleStick((stick as StixAddTopic).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typeformat:
                        (stick as StixFormat).ScaleStick((stick as StixFormat).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typetools:
                        (stick as StixTools).ScaleStick((stick as StixTools).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typemapnavigator:
                        (stick as StixMapNavigator).ScaleStick((stick as StixMapNavigator).scaleFactor, SF_Stix);
                        break;
                    case StixUtils.typetextops:
                        (stick as StixTextOps).ScaleStick((stick as StixTextOps).scaleFactor, SF_Stix);
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
