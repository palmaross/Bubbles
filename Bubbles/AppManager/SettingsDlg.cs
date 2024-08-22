using PRAManager;
using System;
using System.Data;
using System.IO.Compression;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Win32;

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

            FaviconsToolStix.Text = Utils.getString("SettingsDlg.FaviconsToolStix");
            toolTip1.SetToolTip(FaviconsToolStix, Utils.getString("SettingsDlg.favicon"));
            FaviconsLinksWindow.Text = Utils.getString("SettingsDlg.FaviconsLinksWindow");
            toolTip1.SetToolTip(FaviconsLinksWindow, Utils.getString("SettingsDlg.favicon"));

            chOpenInOmniBrowser.Text = Utils.getString("SettingsDlg.chOpenInOmniBrowser");
            chTopicAutoWidth.Text = Utils.getString("SettingsDlg.chTopicAutoWidth");
            toolTip1.SetToolTip(chTopicAutoWidth, Utils.getString("TextOpsStix.MMAutoWidth.tooltip"));
            btnManageAutoWidth.Text = Utils.getString("SettingsDlg.btnManageAutoWidth");
            chSaveMaps.Text = Utils.getString("SettingsDlg.chSaveMaps");
            lblMin.Text = Utils.getString("SettingsDlg.lblMin");
            numSaveMaps.Location = new Point(chSaveMaps.Location.X + chSaveMaps.Width + p1.Width, numSaveMaps.Location.Y);
            lblMin.Location = new Point(numSaveMaps.Location.X + numSaveMaps.Width + p1.Width, lblMin.Location.Y);

            btnShare.Text = Utils.getString("SettingsDlg.btnShare");
            lblSharedPath.Text = Utils.getString("SettingsDlg.lblSharedPath");
            linkSystemPath.Text = Utils.getString("SettingsDlg.linkSystemPath");
            linkExport.Text = Utils.getString("SettingsDlg.linkExport");
            linkImport.Text = Utils.getString("SettingsDlg.linkImport");
            toolTip1.SetToolTip(btnBrowse, Utils.getString("SettingsDlg.btnBrowse"));

            btnSave.Text = Utils.getString("button.save");
            btnClose.Text = Utils.getString("button.close");

            // Fill stix list
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STIX order by type");
                bool allselected = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["type"].ToString() == "StixLego")
                        continue;
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
            
            FaviconsToolStix.Checked = Utils.getRegistry("FaviconsToolStix", "1") == "1";
            FaviconsLinksWindow.Checked = Utils.getRegistry("FaviconsLinksWindow", "1") == "1";

            chTopicAutoWidth.Checked = Utils.getRegistry("TopicAutoWidth", "0") == "1";

            if (Utils.FreeVersionLimitExceeded(StixUtils.typetextops, false))
                chTopicAutoWidth.Enabled = false;

            this.HelpButtonClicked += this_HelpButtonClicked;

            thisHeight = this.Height;
            this.Height -= panelShare.Height;

            string datapath = Utils.getRegistry("DataPath", "");
            if (datapath == "") datapath = Utils.m_defaultDataPath;
            txtDataPath.Text = datapath;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "Settings.htm");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (StixDB db = new StixDB())
            {
                // Save to database starting stix
                foreach (ListViewItem item in listRunAtStart.Items)
                {
                    int id = Convert.ToInt32(item.Tag);
                    int start = Convert.ToInt32(item.Checked);
                    db.ExecuteNonQuery("update STIX set start=" + start + " where id=" + id + "");
                }
            }

            // Apply and save Scale Factor
            btnTestScale_Click(null, null);
            Utils.setRegistry("ScaleFactor_Stix", stixScaleFactor.ToString());
            Utils.setRegistry("ScaleFactor_StixBase", stixbaseScaleFactor.ToString());

            if (StixMain.m_TaskInfo.Visible)
                StixMain.m_TaskInfo.SetQuickTaskDefault();

            Utils.setRegistry("FaviconsToolStix", FaviconsToolStix.Checked ? "1" : "0");
            Utils.setRegistry("FaviconsLinksWindow", FaviconsLinksWindow.Checked ? "1" : "0");
            Utils.setRegistry("OpenLinksInOmniBrowser", chOpenInOmniBrowser.Checked ? "1" : "0");

            Utils.setRegistry("TopicAutoWidth", chTopicAutoWidth.Checked ? "1" : "0");
            Utils.setRegistry("SaveMapsEnabled", chSaveMaps.Checked ? "1" : "0");
            StixMain.saveMapsTimer.Interval = (int)numSaveMaps.Value;
            if (!chSaveMaps.Checked) StixMain.saveMapsTimer.Stop();
            if (chSaveMaps.Checked && !StixMain.saveMapsTimer.Enabled) StixMain.saveMapsTimer.Start();

            string _dataPath = txtDataPath.Text.Trim();
            if (_dataPath == "") return;

            if (_dataPath.ToLower() != Utils.m_dataPath.ToLower())
            {
                // Need to copy files
                DialogResult _rc = MessageBox.Show(
                    Utils.getString("commonoptions.copyfile.settings.caption"),
                    Utils.getString("commonoptions.copyfile.title"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                bool pathChanged = true;
                if (_rc == DialogResult.Yes)
                {
                    if (!MMUtils.XCopy(Utils.m_dataPath, _dataPath))
                    {
                        // copy failed
                        txtDataPath.Text = Utils.getRegistry("DataPath", Utils.m_defaultDataPath);
                        pathChanged = false;
                        MessageBox.Show(Utils.getString("commonoptions.copyfile.error.caption"),
                            Utils.getString("commonoptions.copyfile.error.title"));
                    }
                }
                if (pathChanged)
                {
                    Utils.m_dataPath = _dataPath + "\\";

                    // Check if DataPath is default
                    Utils.setRegistry("DataPath", _dataPath != Utils.m_defaultDataPath ? _dataPath + "\\" : "");
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

        private void btnManageAutoWidth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (StixMain.m_topicAutoWidth != null && StixMain.m_topicAutoWidth.Visible) return;

            StixMain.m_topicAutoWidth = new AutoWidthsDlg();
            StixMain.m_topicAutoWidth.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            this.Close();
        }

        private void btnTestScale_Click(object sender, EventArgs e)
        {
            float SF_Stix, SF_StixBase;

            try { SF_Stix = Convert.ToInt32(numStix.Text.Trim('%').Trim());
            } catch { SF_Stix = 100; }

            try { SF_StixBase = Convert.ToInt32(numStixBase.Text.Trim('%').Trim());
            } catch { SF_StixBase = 100; }

            if (StixMain.m_StixBase.scaleFactor != SF_StixBase)
            {
                StixMain.m_StixBase.ScaleStick(StixMain.m_StixBase.scaleFactor, SF_StixBase);
            }

            foreach (var pair in StixMain.STICKS)
            {
                Form stick = pair.Value;
                if (stick == null) continue;

                switch (stick.Name)
                {
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

        private void btnShare_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (panelShare.Visible)
            {
                panelShare.Visible = false;
                this.Height -= panelShare.Height;
            }
            else
            {
                panelShare.Visible = true;
                this.Height = thisHeight;
            }
        }

        float stixScaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
        float stixbaseScaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_StixBase", "100"));
        float boxesScaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Boxes", "100"));
        int thisHeight;

        private void linkExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Utils.IsFree())
            {
                MessageBox.Show(Utils.getString("license.limitations.freelight.common"),
                    Utils.getString("FreeVersionLimitation"),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string _tempPath = Path.GetTempPath() + "PalmaRoss\\ExportImport";
            MMUtils.DeleteDirectory(_tempPath);
            Directory.CreateDirectory(_tempPath);

            // Ask user for path to save zip file
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyDocuments;
            folderBrowserDialog1.Description = Utils.getString("commonoptions.export.selectfolder");

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;

            string zipFile = folderBrowserDialog1.SelectedPath + "\\OmniStix Settigs.zip";
            if (File.Exists(zipFile)) File.Delete(zipFile);

            YCopy(Utils.m_dataPath.TrimEnd('\\'), _tempPath);

            // Create registry file
            ExportRegKey("HKEY_CURRENT_USER\\Software\\PalmaRoss\\OmniStix", _tempPath + "\\OmniStix.reg");

            try
            {
                ZipFile.CreateFromDirectory(_tempPath, zipFile);
            }
            catch (Exception _e)
            {
                MessageBox.Show(Utils.getString("commonoptions.export.ziperror.text") + _e.Message,
                                Utils.getString("commonoptions.export.ziperror.caption"));
                return;
            }

            MessageBox.Show(String.Format(Utils.getString("commonoptions.export.success.text"), zipFile),
                                          Utils.getString("commonoptions.export.success.caption"));
            MMUtils.DeleteDirectory(_tempPath);
        }

        private void linkImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Utils.IsFree())
            {
                MessageBox.Show(Utils.getString("license.limitations.freelight.common"),
                    Utils.getString("license.limitations.freelight.title"),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string _tempPath = Path.GetTempPath() + "PalmaRoss\\ExportImport";
            MMUtils.DeleteDirectory(_tempPath);
            Directory.CreateDirectory(_tempPath);

            // Ask user for path to zip file
            openFileDialog1.Filter = "Zip files (*.zip)|*.zip|All files (*.*)|*.*";
            openFileDialog1.DefaultExt = "zip";
            openFileDialog1.Title = Utils.getString("commonoptions.import.selectfolder");

            if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
                return;
            string zipFile = openFileDialog1.FileName;

            try
            {
                ZipFile.ExtractToDirectory(zipFile, _tempPath);
            }
            catch (Exception _e)
            {
                MessageBox.Show(Utils.getString("commonoptions.import.ziperror.text") + _e.Message,
                                Utils.getString("commonoptions.export.ziperror.caption"));
                return;
            }

            YCopy(_tempPath, Utils.m_dataPath);

            ImportRegKey(Utils.m_dataPath + "\\OmniStix.reg");
            // TODO какие-то значения подправить?
            Utils.setRegistry("DataPath", Utils.m_dataPath);

            MMUtils.DeleteDirectory(_tempPath);
            File.Delete(Utils.m_dataPath + "\\OmniStix.reg");

            MessageBox.Show(Utils.getString("commonoptions.import.success.text"),
                Utils.getString("commonoptions.export.success.caption"));
        }

        private void YCopy(string aSrcFolder, string aDstFolder)
        {
            if (!Directory.Exists(aSrcFolder))
                return;

            if (!Directory.Exists(aDstFolder))
                return;

            // Create subdirectory structure in destination    
            foreach (string dir in Directory.GetDirectories(aSrcFolder, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(Path.Combine(aDstFolder, dir.Substring(aSrcFolder.Length + 1)));
            }
            foreach (string file_name in Directory.GetFiles(aSrcFolder, "*", SearchOption.AllDirectories))
            {
                File.Copy(file_name, Path.Combine(aDstFolder, file_name.Substring(aSrcFolder.Length + 1)), true);
            }
        }

        private void ExportRegKey(string RegKey, string SavePath)
        {
            string path = "\"" + SavePath + "\"";
            string key = "\"" + RegKey + "\"";

            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "regedit.exe";
                proc.StartInfo.UseShellExecute = false;
                proc = Process.Start("regedit.exe", "/e " + path + " " + key + "");

                if (proc != null) proc.WaitForExit();
            }
            finally
            {
                if (proc != null) proc.Dispose();
            }
        }

        private void ImportRegKey(string aPath)
        {
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "reg.exe";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;

                proc.StartInfo.Arguments = "import " + aPath;
                proc.Start();

                if (proc != null) proc.WaitForExit();
            }
            finally
            {
                if (proc != null) proc.Dispose();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (Utils.IsFree())
            {
                MessageBox.Show(Utils.getString("license.limitations.freelight.common"),
                    Utils.getString("license.limitations.freelight.title"),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.Description = Utils.getString("commonoptions.path.settings.browse.dialog_desc");

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                if (!path.EndsWith("OmniStix")) path += "\\OmniStix";
                txtDataPath.Text = path;
            }
        }

        private void linkSystemPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtDataPath.Text = Utils.m_defaultDataPath;
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
