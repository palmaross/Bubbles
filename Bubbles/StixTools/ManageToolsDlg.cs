using Microsoft.WindowsAPICodePack.Shell;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ManageToolsDlg : Form
    {
        public ManageToolsDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "ManageTools.htm");

            Text = Utils.getString("ManageToolsDlg.Title");
            lblWTools.Text = Utils.getString("ManageToolsDlg.lblWTools");
            lblOTools.Text = Utils.getString("ManageToolsDlg.lblOTools");
            btnAddToStix.Text = Utils.getString("ManageToolsDlg.btnAddToStix");
            btnNewTool.Text = Utils.getString("ManageToolsDlg.btnNewTool");
            btnClose.Text = Utils.getString("button.close");

            t_edittool.Text = Utils.getString("button.edit");
            t_remove.Text = Utils.getString("button.remove");
            t_remove.ToolTipText = Utils.getString("tools.remove.menu.tooltip");
            t_run.Text = Utils.getString("tools.runtool.menu");
            t_copytoomni.Text = Utils.getString("tools.copytoomni.menu");
            t_copytoomni.ToolTipText = Utils.getString("tools.copytoomni.menu.tooltip");
            cmsTool.ItemClicked += CmsTool_ItemClicked;

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            var controls = GetControlsOfType<ListView>(this);

            foreach (ListView lv in controls)
            {
                lv.Columns.Add("", 0, HorizontalAlignment.Left);
                lv.HeaderStyle = ColumnHeaderStyle.None;
                lv.Columns[0].Width = lv.Width - 4 - SystemInformation.VerticalScrollBarWidth;
            }

            this.FormClosing += WindowsToolsDlg_FormClosing;
            this.HelpButtonClicked += this_HelpButtonClicked;
            imageList1.ImageSize = p1.Size;
            db = new StixDB("Tools");

            Utils.InitIcons();
            Init();
        }
        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "ManageTools.htm");
        }

        private void WindowsToolsDlg_Resize(object sender, EventArgs e)
        {
            var controls = GetControlsOfType<ListView>(this);
            foreach (ListView lv in controls)
            {
                if (lv.Columns.Count == 0) continue;
                lv.Columns[0].Width = lv.Width - 4 - SystemInformation.VerticalScrollBarWidth;
            }
        }

        public static IEnumerable<Control> GetControlsOfType<T>(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetControlsOfType<T>(ctrl)).Concat(controls).Where(c => c is T);
        }

        private void CmsTool_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == t_remove)
            {
                if (MessageBox.Show(Utils.getString("ManageToolsDlg.confirm.remove"), "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    // Get an array of all selected items
                    var selectedItems = selectedList.SelectedItems;

                    // Remove selected items
                    if (selectedList == listWindowsApps) // remove and add to Ignore list
                    {
                        StreamWriter sw = new StreamWriter(Utils.m_dataPath + "AppsToIgnore.txt", true);
                        foreach (ListViewItem item in selectedItems)
                        {
                            ToolItem _item = item.Tag as ToolItem;
                            sw.WriteLine(_item.Path);
                            item.Remove();
                        }
                        sw.Close();
                    }
                    else // remove from Omni list and database
                    {
                        using (StixDB db = new StixDB("Tools"))
                        {
                            foreach (ListViewItem item in selectedItems)
                            {
                                db.ExecuteNonQuery("delete from TOOLS where " +
                                    "title=`" + item.Text + "` and stixID=0");

                                item.Remove();
                            }
                        }
                    }
                }
            }
            else if (e.ClickedItem == t_edittool)
            {
                ListViewItem lv = listOmniTools.SelectedItems[0];
                ToolItem item = lv.Tag as ToolItem;

                using (EditToolDlg dlg = new EditToolDlg())
                {
                    dlg.chChangeInDataBase.Visible = false;
                    dlg.Height -= dlg.chChangeInDataBase.Height;
                    dlg.txtTitle.Text = item.Title;
                    dlg.txtTooltip.Text = item.Tooltip;
                    dlg.pIcon.Image = imageList1.Images[listOmniTools.SelectedItems[0].ImageIndex];
                    dlg.pIcon.Tag = item.Type; // image file

                    dlg.Location = new Point(this.Left + btnClose.Height, MousePosition.Y);
                    int x = this.Left + btnClose.Height;
                    int y = MousePosition.Y;
                    // Check if the dlg is close to the bottom screen side...
                    Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;
                    if (dlg.Bottom > area.Bottom) // is close to the bottom
                        y -= dlg.Height + btnClose.Height; // Move dlg up
                    dlg.Location = new Point(x, y);

                    if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;

                    string title = dlg.txtTitle.Text.Trim();
                    string tooltip = dlg.txtTooltip.Text.Trim();
                    string type = dlg.pIcon.Tag.ToString();

                    if (title == "") return;

                    var tool = listOmniTools.SelectedItems[0];
                    item = tool.Tag as ToolItem;

                    item.Tooltip = tooltip;
                    item.Title = title;

                    if (dlg.pIcon.Tag.ToString() != item.Type)
                    {
                        item.Type = dlg.pIcon.Tag.ToString();

                        if (imageList1.Images.ContainsKey(item.Type))
                        {
                            // if imageList contains image key, image file exists in the AppIconDB
                            tool.ImageIndex = imageList1.Images.IndexOfKey(item.Type);
                        }
                        else
                        {
                            Image img = dlg.pIcon.Image;
                            imageList1.Images.Add(img);
                            tool.ImageIndex = imageList1.Images.Count - 1;

                            // if image file not exists in the AppIconDB, add...
                            if (!File.Exists(app_icons + item.Type))
                                img.Save(app_icons + item.Type);
                        }
                    }

                    tool.Text = title;
                    tool.ToolTipText = tooltip;
                    tool.Tag = item;
                }

                string path = item.Path;
                if (path.StartsWith(Utils.m_dataPath + "ToolStixApps"))
                    path = Path.GetFileName(path);

                using (StixDB db = new StixDB("Tools"))
                {
                    db.ExecuteNonQuery("update TOOLS set " +
                        "title=`" + item.Title + "`, " +
                        "tooltip=`" + item.Tooltip + "`, " +
                        "type=`" + item.Type + "` " +
                        "where path=`" + path + "`"
                        );
                }

                // Process changes on the open Stix
                foreach (var pair in StixMain.STICKS)
                {
                    Form form = pair.Value;
                    if (form.Name == StixUtils.typetools)
                    {
                        var stix = form as StixTools;
                        for (int i = 0; i < stix.Tools.Count; i++)
                        {
                            if (stix.Tools[i].Path == item.Path)
                            {
                                stix.Tools[i] = item; break;
                            }
                        }
                        stix.RefreshStick();
                    }
                }
            }
            else if (e.ClickedItem == t_run)
            {
                ToolItem item = selectedList.SelectedItems[0].Tag as ToolItem;
                RunTool(item.Path, item.Title);
            }
            else if (e.ClickedItem == t_copytoomni)
            {
                // Copy tool to omni panel
                ToolItem item = selectedList.SelectedItems[0].Tag as ToolItem;
                ListViewItem lv = listOmniTools.Items.Add(item.Title);
                lv.Tag = item;
                lv.ImageIndex = selectedList.SelectedItems[0].ImageIndex;

                // Fix in database
                db.AddTool(item.Title, item.Tooltip, item.Path, item.Type, 0, 0);
            }
        }

        private void listOmniTools_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            Control pb = sender as Control;
            Point pnt = pb.PointToClient(Cursor.Position);
            pnt = new Point(pnt.X += btnClose.Height / 2, pnt.Y);  // Give a little offset to right
            toolTip1.Show(Utils.getString("ManageToolsDlg.edit.tooltip"), pb, pnt, 2500);
        }

        private void listOmniTools_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            toolTip1.Hide(listOmniTools);
            if (e.Label == null) return; // Esc key pressed

            ToolItem item = listOmniTools.SelectedItems[0].Tag as ToolItem;
            string oldName = item.Title;
            string newName = e.Label.Trim();

            if (oldName == newName) return;

            item.Title = newName;

            string path = item.Path;
            if (path.StartsWith(Utils.m_dataPath + "ToolStixApps"))
                path = Path.GetFileName(path);

            using (StixDB db = new StixDB("Tools"))
                db.ExecuteNonQuery("update TOOLS set title=`" + newName +
                    "` where path =`" + path + "`");

            // Process changes on the open Stix
            foreach (var pair in StixMain.STICKS)
            {
                Form form = pair.Value;
                if (form.Name == StixUtils.typetools)
                {
                    var stix = form as StixTools;
                    for (int i = 0; i < stix.Tools.Count; i++)
                    {
                        if (stix.Tools[i].Path == item.Path)
                        {
                            stix.Tools[i] = item; break;
                        }
                    }
                    stix.RefreshStick();
                }
            }
        }

        void Init()
        {
            // Get Windows Tools to ignore
            StreamReader sr = new StreamReader(Utils.m_dataPath + "AppsToIgnore.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                AppToIgnore.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();

            // Populate Windows Tools

            // GUID taken from https://learn.microsoft.com/en-us/windows/win32/shell/knownfolderid
            var FODLERID_AppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
            ShellObject appsFolder = (ShellObject)KnownFolderHelper.FromKnownFolderId(FODLERID_AppsFolder);

            //StreamWriter sw = new StreamWriter(Utils.m_dataPath + "AppsTest.txt", true);
            int i = 0;
            foreach (var app in (IKnownFolder)appsFolder)
            {
                // The friendly app name
                string name = app.Name;
                string appUserModelID = app.ParsingName;
                string appPath = app.Properties.System.Link.TargetParsingPath.Value;

                if (AppToIgnore.Contains(appUserModelID) || 
                    AppToIgnore.Contains("WT_" + appUserModelID) ||
                    AppToIgnore.Contains(appPath))
                    continue;

                // App icon
                Bitmap appIcon; string toolPath; string type = "";

                if (appUserModelID.Contains("WhatsAppDesktop"))
                {
                    appIcon = (Bitmap)Image.FromFile(app_icons + "tool-whatsapp.png");
                    toolPath = "WT_" + appUserModelID;
                    type = "tool-whatsapp.png";
                }
                else if (appPath != null && appPath.EndsWith(".exe") && File.Exists(appPath) &&
                    !appUserModelID.StartsWith("Chrome._crx") &&
                    !appUserModelID.StartsWith("Microsoft.Windows.AdministrativeTools") &&
                    !appUserModelID.StartsWith("Microsoft.AutoGenerated.{DAA168DE-4306-C8BC-8C11-B596240BDDED}"))
                {
                    appIcon = Icon.ExtractAssociatedIcon(appPath).ToBitmap();
                    toolPath = appPath;
                    type = "exe";
                    FileVersionInfo myFileVersionInfo =
                        FileVersionInfo.GetVersionInfo(appPath);
                }
                else
                {
                    toolPath = "WT_" + appUserModelID;

                    if (OmniTools.WindowsAppIcons.ContainsKey(toolPath))
                    {
                        appIcon = (Bitmap)Image.FromFile(app_icons + OmniTools.WindowsAppIcons[toolPath]);
                        type = OmniTools.WindowsAppIcons[toolPath];
                    }
                    else
                    {
                        appIcon = app.Thumbnail.Bitmap;
                        type = "file";
                    }
                }

                appIcon.MakeTransparent();

                ToolItem item = new ToolItem(name, toolPath, type, 0, "");
                if (OmniTools.WindowsAppIcons.ContainsKey(item.Path))
                    item.Type = OmniTools.WindowsAppIcons[item.Path];
                ListViewItem lv = listWindowsApps.Items.Add(name);
                
                imageList1.Images.Add(appIcon);
                lv.ImageIndex = i++;
                item.App_Icon = appIcon;
                lv.Tag = item;
            }
            //sw.Close();

            // Populate bottom panel
            InitOmniTools();
        }

        public void InitOmniTools()
        {
            listOmniTools.Items.Clear();

            DataTable dt = db.ExecuteQuery("select * from TOOLS");

            int i = imageList1.Images.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["stixID"].ToString() == "0")
                {
                    string path = dr["path"].ToString();
                    if (!path.StartsWith("OT_") && path == Path.GetFileName(path))
                        path = Utils.m_dataPath + "ToolStixApps\\" + path;

                    ToolItem item = new ToolItem(dr["title"].ToString(), path, dr["type"].ToString(), 0, dr["tooltip"].ToString());
                    ListViewItem lv = listOmniTools.Items.Add(dr["title"].ToString());
                    lv.Tag = item;
                    if (dr["tooltip"].ToString() != "")
                        lv.ToolTipText = dr["tooltip"].ToString();

                    if (item.Type == "http")
                    {
                        imageList1.Images.Add(item.Type, Utils.GetFavicon(item.Path));
                        lv.ImageIndex = i++;
                    }
                    else if (imageList1.Images.ContainsKey(item.Type))
                    {
                        lv.ImageIndex = imageList1.Images.IndexOfKey(item.Type);
                    }
                    else
                    {
                        if (File.Exists(app_icons + item.Type))
                            imageList1.Images.Add(item.Type, Image.FromFile(app_icons + item.Type));
                        else
                            imageList1.Images.Add(item.Type, StixUtils.GetToolImage(item.Type, item.Path));

                        lv.ImageIndex = i++;
                    }
                }
            }
        }

        private Image ImageWpfToGDI(System.Windows.Media.ImageSource image)
        {
            MemoryStream ms = new MemoryStream();
            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(image as System.Windows.Media.Imaging.BitmapSource));
            encoder.Save(ms);
            ms.Flush();
            return Image.FromStream(ms);
        }

        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;
            selectedList = lv;

            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTool.Items)
                    item.Visible = true;

                if (lv == listWindowsApps) t_edittool.Visible = false;
                else t_copytoomni.Visible = false;

                if (lv.SelectedItems.Count > 1)
                {
                    t_run.Visible = false;
                    t_edittool.Visible = false;
                }

                cmsTool.Show(MousePosition);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowsToolsDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Dispose(); db = null;
        }

        private void btnAddToStix_Click(object sender, EventArgs e)
        {
            cbAddToStix.Items.Clear();

            // Get ToolStix from database
            using (StixDB db = new StixDB("Stix"))
            {
                DataTable dt = db.ExecuteQuery("select * from STIX where type=`" + StixUtils.typetools + "`");

                foreach (DataRow dr in dt.Rows)
                    cbAddToStix.Items.Add(new cbToolItem(dr["name"].ToString(), Convert.ToInt32(dr["id"])));

                if (dt.Rows.Count == 0) // There are no ToolStix
                {
                    return;
                }
                else if (dt.Rows.Count > 1) // More than one - user have to select needed Stix
                {
                    cbAddToStix.Focus();
                    cbAddToStix.DroppedDown = true; // Open ComboBox
                    return;
                }
            }

            // There is one ToolStix only. No need to open ComboBox, add tool to stick.
            AddToolToStix((cbAddToStix.Items[0] as cbToolItem).StixID);
        }

        private void cbAddToStix_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToolToStix((cbAddToStix.SelectedItem as cbToolItem).StixID);
        }

        private void AddToolToStix(int stixID)
        {
            if (selectedList == null || selectedList.SelectedItems.Count == 0)
                return;

            foreach (ListViewItem item in selectedList.SelectedItems)
            {
                ToolItem _item = item.Tag as ToolItem;

                if (selectedList == listWindowsApps) // We have to save the Windows App icon
                {
                    if (_item.Type != "exe" && !_item.Type.EndsWith(".png"))
                    {
                        string temp = Utils.m_localDataPath + "auxicon.png";
                        if (File.Exists(temp)) File.Delete(temp);
                        _item.App_Icon.Save(temp, ImageFormat.Png);

                        string newicon = "tool-" + Utils.GetRandom().ToString() + ".png";
                        string newiconPath = Utils.m_dataPath + "AppIconDB\\" + newicon;
                        if (!File.Exists(newiconPath))
                            File.Move(temp, newiconPath);

                        _item.Type = newicon;
                    }
                }

                bool done = false;
                foreach (var pair in StixMain.STICKS)
                {
                    if (pair.Key == stixID) // Stix is opened. Add tool to the Stix.
                    {
                        (pair.Value as StixTools).NewIcon(_item.Path, _item.Title, "end", _item.Type, _item.Tooltip);
                        done = true; break;
                    }
                }

                if (!done) // Stix is not opened. Add icon to the stix in the database.
                {
                    DataTable dt = db.ExecuteQuery("select * from TOOLS where stixID=" + stixID + "");
                    int count = dt.Rows.Count + 1;
                    string path;

                    // Check if Stix has this tool already
                    if (count > 1) // 1 - Stix doesn't have tools yet
                    {
                        done = false;
                        foreach (DataRow dr in dt.Rows)
                        {
                            path = dr["path"].ToString();
                            if (!path.StartsWith("WT_") && !path.StartsWith("OT_") && 
                                path == Path.GetFileName(path)) // Relative path to the ToolStixApps folder
                                path = Utils.m_dataPath + "ToolStixApps\\" + path; // Make the absolute

                            if (path == _item.Path)
                                done = true; break;
                        }
                        if (done) continue; // Stix has tool. Do not add to database.
                    }

                    path = _item.Path;
                    if (path.StartsWith(Utils.m_dataPath + "ToolStixApps"))
                        path = Path.GetFileName(path); // Make path relative
                    db.AddTool(_item.Title, _item.Tooltip, path, _item.Type, count, stixID);
                }
            }
        }

        private void btnNewTool_Click(object sender, EventArgs e)
        {
            if (Utils.IsFree())
            {
                MessageBox.Show(Utils.getString("limitation.newtool"),
                    Utils.getString("FreeVersionLimitation"));
                return;
            }

            using (NewToolDlg dlg = new NewToolDlg(this, null))
            {
                dlg.Location = this.Location;
                dlg.ShowDialog();
            }
        }

        private void pIcon_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            string iconPath = "";

            using (SelectIconDlg dlg = new SelectIconDlg("ManageTools"))
            {
                if (dlg.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                   iconPath = dlg.iconPath;
            }

            pb.Image = Image.FromFile(iconPath);

            string filename = Path.GetFileName(iconPath);
            if (filename.StartsWith("tool-"))
                pb.Tag = Path.GetFileName(iconPath);
            else
                pb.Tag = "tool-" + Path.GetFileName(iconPath);

            string path = Utils.m_dataPath + "AppIconDB\\" + pb.Tag.ToString();
            if (!File.Exists(path))
                File.Copy(iconPath, path);
        }

        List<string> AppToIgnore = new List<string>();
        StixDB db;
        ListView selectedList;
        public Form Stix = null;
        string app_icons = Utils.m_dataPath + "AppIconDB\\";

        private void listWindowsApps_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToolItem item = listWindowsApps.SelectedItems[0].Tag as ToolItem;
            RunTool(item.Path, item.Title);
        }

        private void listOmniTools_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToolItem item = listOmniTools.SelectedItems[0].Tag as ToolItem;
            RunTool(item.Path, item.Title);
        }

        public void RunTool(string path, string tool)
        {
            if (Utils.FreeVersionLimitExceeded("runtool"))
                return;

            try
            {
                if (path.StartsWith("OT_")) // Omni function
                    OmniTools.RunTool(path, this, "V");
                else if (path.StartsWith("WT_")) // Windows tool
                    Process.Start("explorer.exe", @" shell:appsFolder\" + path.Substring(3));
                else // HTTP or file
                {
                    if (path.EndsWith(".mmbas"))
                        MMUtils.MindManager.RunMacro(path);
                    else
                        Process.Start(path);
                }
            }
            catch
            {
                if (!path.StartsWith("OT_") && !File.Exists(path)) // file not exists
                {
                    MessageBox.Show(String.Format(Utils.getString("tools.run.filenotfound.1"), path));
                }
                else // unknown reason
                    MessageBox.Show(String.Format(Utils.getString("tools.run.error"), tool));
            }
        }
    }

    public class cbToolItem
    {
        public cbToolItem(string title, int stixID)
        {
            StixID = stixID;
            Title = title;
        }

        public string Title = "";
        public int StixID;

        public override string ToString()
        {
            return Title;
        }
    }
}
