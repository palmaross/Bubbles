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
        public ManageToolsDlg(Form form = null)
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "WindowsToolsDlg.htm");

            Stix = form;

            Text = Utils.getString("WindowsToolsDlg.Title");
            lblSpecifyPath.Text = Utils.getString("WindowsToolsDlg.lblSpecifyPath");
            lblTitle.Text = Utils.getString("WindowsToolsDlg.lblTitle");
            chAddToStix.Text = Utils.getString("WindowsToolsDlg.chAddToStix");
            btnAddToStix.Text = Utils.getString("WindowsToolsDlg.chAddToStix");
            btnAddTool.Text = Utils.getString("button.add");
            lblToolIcon.Text = Utils.getString("WindowsToolsDlg.lblToolIcon");
            lblChangeIcon.Text = Utils.getString("WindowsToolsDlg.lblChangeIcon");
            lblChangeIcon2.Text = Utils.getString("WindowsToolsDlg.lblChangeIcon");
            lblTooltip.Text = Utils.getString("WindowsToolsDlg.lblTooltip");
            lblTip.Text = Utils.getString("WindowsToolsDlg.lblTip");
            btnNewTool.Text = Utils.getString("WindowsToolsDlg.btnNewTool");
            btnClose.Text = Utils.getString("button.close");
            btnCloseAddTool.Text = Utils.getString("button.close");

            lblTitle2.Text = Utils.getString("WindowsToolsDlg.lblTitle");
            lblToolIcon2.Text = Utils.getString("WindowsToolsDlg.lblToolIcon");
            lblTooltip2.Text = Utils.getString("WindowsToolsDlg.lblTooltip");
            btnCancel.Text = Utils.getString("button.cancel");

            t_edittool.Text = Utils.getString("button.edit");
            t_remove.Text = Utils.getString("button.remove");
            t_run.Text = Utils.getString("tools.runtool.menu");
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
            imageList1.ImageSize = p1.Size;
            db = new StixDB();

            Init();
        }

        private void WindowsToolsDlg_Resize(object sender, EventArgs e)
        {
            var controls = GetControlsOfType<ListView>(this);
            foreach (ListView lv in controls)
            {
                if (lv.Columns.Count == 0) continue;
                lv.Columns[0].Width = lv.Width - 4 - SystemInformation.VerticalScrollBarWidth;
            }

            groupAddTool.Refresh();
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
                if (MessageBox.Show(Utils.getString("WindowsToolsDlg.confirm.remove"), "",
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
                            ThisToolItem _item = item.Tag as ThisToolItem;
                            sw.WriteLine(_item.Path);
                            item.Remove();
                        }
                        sw.Close();
                    }
                    else // remove from Omni list and database
                    {
                        using (StixDB db = new StixDB())
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
                panelNewTool.Visible = false;
                paneEditTool.Visible = true;
                paneEditTool.Location = panelNewTool.Location;

                ListViewItem lv = listOmniTools.SelectedItems[0];
                ThisToolItem item = lv.Tag as ThisToolItem;

                txtTitle2.Text = item.Title;
                txtTooltip2.Text = item.Tooltip;
                pIcon2.Image = imageList1.Images[listOmniTools.SelectedItems[0].ImageIndex];
                pIcon2.Tag = item.aType; // image file
            }
            else if (e.ClickedItem == t_run)
            {
                ThisToolItem item = selectedList.SelectedItems[0].Tag as ThisToolItem;
                RunTool(item.Path);
            }
        }

        private void listOmniTools_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            Control pb = sender as Control;
            Point pnt = pb.PointToClient(Cursor.Position);
            pnt = new Point(pnt.X += lblTitle.Height, pnt.Y);  // Give a little offset to right
            toolTip1.Show(Utils.getString("WindowsToolsDlg.edit.tooltip"), pb, pnt, 2500);
        }

        private void listOmniTools_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            toolTip1.Hide(listOmniTools);
            if (e.Label == null) return; // Esc key pressed

            string oldName = listOmniTools.SelectedItems[0].Text.Trim();
            string newName = e.Label.Trim();
            if (oldName == newName) return;

            using (StixDB db = new StixDB())
                db.ExecuteNonQuery("update TOOLS set title=`" + newName +
                    "` where title=`" + oldName + "`");
        }

        void Init()
        {
            StreamReader sr = new StreamReader(Utils.m_dataPath + "AppsToIgnore.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                AppToIgnore.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();

            // Init left part
            DataTable dt = db.ExecuteQuery("select * from TOOLS");

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["stixID"].ToString() == "0")
                {
                    ThisToolItem item = new ThisToolItem(dr["title"].ToString(), dr["tooltip"].ToString(), dr["path"].ToString(), dr["type"].ToString());
                    ListViewItem lv = listOmniTools.Items.Add(dr["title"].ToString());
                    lv.Tag = item;
                    if (dr["tooltip"].ToString() != "")
                        lv.ToolTipText = dr["tooltip"].ToString();

                    if (imageList1.Images.ContainsKey(item.aType))
                    {
                        lv.ImageIndex = imageList1.Images.IndexOfKey(item.aType);
                    }
                    else
                    {
                        if (File.Exists(app_icons + item.aType))
                            imageList1.Images.Add(item.aType, Image.FromFile(app_icons + item.aType));
                        else
                            imageList1.Images.Add(item.aType, StixUtils.GetToolImage(item.aType, item.Path));
                        
                        lv.ImageIndex = i++;
                    }
                }
            }

            // GUID taken from https://learn.microsoft.com/en-us/windows/win32/shell/knownfolderid
            var FODLERID_AppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
            ShellObject appsFolder = (ShellObject)KnownFolderHelper.FromKnownFolderId(FODLERID_AppsFolder);

            
            StreamWriter sw = new StreamWriter(Utils.m_dataPath + "AppsTest.txt", true);

            foreach (var app in (IKnownFolder)appsFolder)
            {
                // The friendly app name
                string name = app.Name;
                string appUserModelID = app.ParsingName;
                string appPath = app.Properties.System.Link.TargetParsingPath.Value;

                if (AppToIgnore.Contains(appUserModelID) || AppToIgnore.Contains("WT_" + appUserModelID))
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
                        appIcon = app.Thumbnail.Bitmap;
                }

                appIcon.MakeTransparent();

                ThisToolItem item = new ThisToolItem(name, "", toolPath, type);
                if (OmniTools.WindowsAppIcons.ContainsKey(item.Path))
                    item.aType = OmniTools.WindowsAppIcons[item.Path];
                ListViewItem lv = listWindowsApps.Items.Add(name);
                
                imageList1.Images.Add(appIcon);
                lv.ImageIndex = i++;
                item.App_Icon = appIcon;
                lv.Tag = item;
            }
            sw.Close();
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
                txtPath_KeyUp(null, null); // proceed with title and icon
            }
        }

        private void txtPath_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender == null || e.KeyCode == Keys.Enter || (e.KeyCode == Keys.V && e.Control))
            {
                string path = txtPath.Text.Trim();
                if (path == "") return;
                string title = "";

                if (path.StartsWith("http"))
                {
                    lblWait.Visible = true;
                    title = Utils.GetWebPageTitle(path);
                    lblWait.Visible = false;
                }
                else // file
                {
                    try { title = Path.GetFileName(path); }
                    catch { }
                }

                if (!String.IsNullOrEmpty(title))
                    txtTitle.Text = title;

                // Set app icon
                string imageType = Utils.GetFileType(path);
                Image img = StixUtils.GetToolImage(imageType, path);
                if (img != null) pIcon.Image = img;
                pIcon.Tag = imageType;

                this.Refresh();

                if (e != null)
                {
                    e.Handled = true; // to avoid the "ding" sound
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void btnAddTool_Click(object sender, EventArgs e)
        {
            string toolPath = txtPath.Text.Trim();
            string title = txtTitle.Text.Trim();
            string tooltip = txtTooltip.Text.Trim();

            if (toolPath == "" || title == "" || pIcon.Tag.ToString() == "") return;

            string imageType = pIcon.Tag.ToString();

            ThisToolItem item = new ThisToolItem(title, tooltip, toolPath, imageType, pIcon.Image);

            int imageIndex;
            if (imageList1.Images.ContainsKey(item.aType))
            {
                // if imageList contains image key, image file exists in the AppIconDB
                imageIndex = imageList1.Images.IndexOfKey(item.aType);
            }
            else
            {
                imageList1.Images.Add(pIcon.Image);
                imageIndex = imageList1.Images.Count - 1;

                // if image file not exists in the AppIconDB, add...
                if (item.aType.Contains(".") && // it's a file name (.png or .ico, etc...)
                    !File.Exists(app_icons + item.aType)) // and file is not saved yet
                    pIcon.Image.Save(app_icons + item.aType);
            }

            var tool = listOmniTools.Items.Add(title, imageIndex);
            tool.Tag = item;

            if (chAddToStix.Checked)
                (Stix as StixTools).NewIcon(toolPath, title, "end", imageType, tooltip);

            using (StixDB db = new StixDB())
                db.AddTool(title, tooltip, toolPath, imageType, 0, 0);
        }

        /// <summary>
        /// Process tool editing.
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            string title = txtTitle2.Text.Trim();
            string tooltip = txtTooltip2.Text.Trim();

            if (title == "") return;

            var tool = listOmniTools.SelectedItems[0];
            ThisToolItem item = tool.Tag as ThisToolItem;

            item.Tooltip = tooltip;
            item.Title = title;

            if (pIcon2.Tag.ToString() != item.aType)
            {
                item.aType = pIcon2.Tag.ToString();

                if (imageList1.Images.ContainsKey(item.aType))
                {
                    // if imageList contains image key, image file exists in the AppIconDB
                    tool.ImageIndex = imageList1.Images.IndexOfKey(item.aType);
                }
                else
                {
                    Image img = pIcon2.Image;
                    imageList1.Images.Add(img);
                    tool.ImageIndex = imageList1.Images.Count - 1;

                    // if image file not exists in the AppIconDB, add...
                    if (!File.Exists(app_icons + item.aType))
                        img.Save(app_icons + item.aType);
                }
            }

            tool.Text = title;
            tool.ToolTipText = tooltip;
            tool.Tag = item;

            using (StixDB db = new StixDB())
                db.ExecuteNonQuery("update TOOLS set " +
                    "title=`" + title + "`, " +
                    "tooltip=`" + tooltip + "`, " +
                    "type=`" + item.aType + "` " +
                    "where path=`" + item.Path + "`"
                    );

            paneEditTool.Visible = false;
        }

        /// <summary>
        /// Close Edit Tool panel.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            paneEditTool.Visible = false;
        }


        private void txtPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtPath.SelectAll();
        }

        private void txtboxPaste_Click(object sender, EventArgs e)
        {
            txtPath.Text = Clipboard.GetText();
            txtPath_KeyUp(null, null);
        }

        private void txtboxClear_Click(object sender, EventArgs e)
        {
            txtPath.Clear();
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim() == "" || txtTitle.Text.Trim() == "") btnAddTool.Enabled = false;
            else btnAddTool.Enabled = true;
        }

        private void btnAddToStix_Click(object sender, EventArgs e)
        {
            if (Stix == null) return;

            foreach (ListViewItem item in selectedList.SelectedItems)
            {
                ThisToolItem _item = item.Tag as ThisToolItem;

                if (selectedList == listWindowsApps) // We have to save the Windows App icon
                {
                    if (_item.aType != "exe" && !_item.aType.EndsWith(".png"))
                    {
                        string temp = Utils.m_localDataPath + "auxicon.png";
                        if (File.Exists(temp)) File.Delete(temp);
                        _item.App_Icon.Save(temp, ImageFormat.Png);

                        string newicon = "tool-" + Utils.GetRandom().ToString() + ".png";
                        string newiconPath = Utils.m_dataPath + "AppIconDB\\" + newicon;
                        if (!File.Exists(newiconPath))
                            File.Move(temp, newiconPath);

                        _item.aType = newicon;
                    }
                }

                (Stix as StixTools).NewIcon(_item.Path, _item.Title, "end", _item.aType, _item.Tooltip);
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "" || txtPath.Text.Trim() == "") btnAddTool.Enabled = false;
            else btnAddTool.Enabled = true;
        }

        private void btnNewTool_Click(object sender, EventArgs e)
        {
            paneEditTool.Visible = false;
            panelNewTool.Visible = true;

            txtPath.Text = ""; txtTitle.Text = ""; txtTooltip.Text = "";
            chAddToStix.Checked = false; pIcon.Tag = "";
            pIcon.Image = Image.FromFile(Utils.m_imagesPath + "empty_icon.png");
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

        private void btnCloseAddTool_Click(object sender, EventArgs e)
        {
            panelNewTool.Visible = false;
        }

        List<string> AppToIgnore = new List<string>();
        StixDB db;
        ListView selectedList;
        Form Stix = null;
        string app_icons = Utils.m_dataPath + "AppIconDB\\";

        private void listWindowsApps_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ThisToolItem item = listWindowsApps.SelectedItems[0].Tag as ThisToolItem;
            RunTool(item.Path);
        }

        private void listOmniTools_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ThisToolItem item = listOmniTools.SelectedItems[0].Tag as ThisToolItem;
            RunTool(item.Path);
        }

        public void RunTool(string path)
        {
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
                    MessageBox.Show(Utils.getString("tools.run.filenotfound.1"));
                }
                else // unknown reason
                    MessageBox.Show(Utils.getString("tools.run.error"));
            }
        }
    }

    public class ThisToolItem
    {
        public ThisToolItem(string title, string tooltip, string path, string type, Image app_icon = null)
        {
            Path = path;
            Title = title;
            aType = type;
            App_Icon = app_icon;
            Tooltip = tooltip;
        }

        public string Title = "";
        public string Tooltip = "";
        public string Path = "";
        public string aType = "";
        public Image App_Icon;
    }
}
