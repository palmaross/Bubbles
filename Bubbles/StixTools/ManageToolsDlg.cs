using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
            lblAddTool.Text = "  " + Utils.getString("WindowsToolsDlg.groupAddTool") + "  ";
            lblSpecifyPath.Text = Utils.getString("WindowsToolsDlg.lblSpecifyPath");
            lblTitle.Text = Utils.getString("WindowsToolsDlg.lblTitle");
            chAddToStix.Text = Utils.getString("WindowsToolsDlg.chAddToStix");
            btnAddToStix.Text = Utils.getString("WindowsToolsDlg.chAddToStix");
            btnAddTool.Text = Utils.getString("button.add");
            btnClose.Text = Utils.getString("button.close");

            t_rename.Text = Utils.getString("button.rename");
            t_remove.Text = Utils.getString("button.remove");
            t_run.Text = Utils.getString("WindowsToolsDlg.btnRun");
            cmsTool.ItemClicked += CmsTool_ItemClicked;

            int loc = (this.Width - lblAddTool.Width) / 2;
            lblAddTool.Location = new Point(loc, lblAddTool.Location.Y);

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
            else if (e.ClickedItem == t_rename)
            {
                selectedList.SelectedItems[0].BeginEdit();
                // Editing results in the listOmniTools_AfterLabelEdit
            }
            else if (e.ClickedItem == t_run)
            {
                ThisToolItem item = selectedList.SelectedItems[0].Tag as ThisToolItem;
                string path = item.Path;
                if (path.StartsWith("WT_"))
                {
                    path = path.Substring(3);
                    Process.Start("explorer.exe", @" shell:appsFolder\" + path);
                }
                else
                    Process.Start(path);
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

            string app_icons = Utils.m_dataPath + "AppIconDB\\";
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

                    if (File.Exists(app_icons + dr["type"].ToString()))
                        imageList1.Images.Add(Image.FromFile(app_icons + dr["type"].ToString()));
                    else
                        imageList1.Images.Add(StixUtils.GetToolImage(item.AppIcon, item.Path));
                    
                    lv.ImageIndex = i++;
                }
            }

            // GUID taken from https://learn.microsoft.com/en-us/windows/win32/shell/knownfolderid
            var FODLERID_AppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
            ShellObject appsFolder = (ShellObject)KnownFolderHelper.FromKnownFolderId(FODLERID_AppsFolder);

            foreach (var app in (IKnownFolder)appsFolder)
            {
                // The friendly app name
                string name = app.Name;
                string name2 = app.GetDisplayName(DisplayNameType.RelativeToParent);
                // The ParsingName property is the AppUserModelID
                string appUserModelID = app.ParsingName;
                string applicationPath = app.Properties.System.Link.TargetParsingPath.Value;
                // App icon
                System.Windows.Media.ImageSource icon = app.Thumbnail.SmallBitmapSource;

                if (AppToIgnore.Contains(appUserModelID) || AppToIgnore.Contains("WT_" + appUserModelID))
                    continue;

                ThisToolItem item = new ThisToolItem(name, "", "WT_" + appUserModelID, "");
                ListViewItem lv = listWindowsApps.Items.Add(name);

                Image img = ImageWpfToGDI(icon);
                imageList1.Images.Add(img);
                lv.ImageIndex = i++;
                item.App_Icon = img;
                lv.Tag = item;
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

                if (lv == listWindowsApps) t_rename.Visible = false;

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
                txtPath.Text = openFileDialog1.FileName;
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

            if (Stix == null || toolPath == "" || title == "") return;

            string imageType = Utils.GetFileType(toolPath);
            Image img = StixUtils.GetToolImage(imageType, toolPath);

            ThisToolItem item = new ThisToolItem(title, tooltip, toolPath, imageType, img);
            imageList1.Images.Add(img);
            var tool = listOmniTools.Items.Add(title, imageList1.Images.Count - 1);
            tool.Tag = item;

            if (chAddToStix.Checked)
                (Stix as StixTools).NewIcon(toolPath, title, "end", "", tooltip);

            using (StixDB db = new StixDB())
                db.AddTool(title, tooltip, toolPath, imageType, 0, 0);
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
                (Stix as StixTools).NewIcon(_item.Path, _item.Title, "end", _item.AppIcon, _item.Tooltip);
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "" || txtPath.Text.Trim() == "") btnAddTool.Enabled = false;
            else btnAddTool.Enabled = true;
        }

        List<string> AppToIgnore = new List<string>();
        StixDB db;
        ListView selectedList;
        Form Stix = null;
    }

    public class ThisToolItem
    {
        public ThisToolItem(string title, string tooltip, string path, string icon, Image app_icon = null)
        {
            Path = path;
            Title = title;
            AppIcon = icon;
            App_Icon = app_icon;
            Tooltip = tooltip;
        }

        public string Title = "";
        public string Tooltip = "";
        public string Path = "";
        public string AppIcon = "";
        public Image App_Icon;
    }
}
