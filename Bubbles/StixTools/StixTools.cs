using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bubbles
{
    internal partial class StixTools : Form
    {
        public StixTools(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "ToolStix.htm");

            toolTip1.SetToolTip(ToolList, Utils.getString("tools.toolview.list"));
            toolTip1.SetToolTip(pictureHandle, stickname + Utils.getString("HeadIcon.tooltip"));
            toolTip1.SetToolTip(Manage, Utils.getString("ManageIcon.tooltip"));

            orientation = _orientation; // "H" or "V"

            MinLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            StixUtils.icondist = pIconDist.Width;

            //// Context menu ////

            TM_edit.Text = Utils.getString("button.rename");
            StixUtils.SetContextMenuImage(TM_edit, "edit.png");

            TM_delete.Text = Utils.getString("button.remove");
            StixUtils.SetContextMenuImage(TM_delete, "deleteall.png");

            TM_closeoptions.Text = Utils.getString("tools.contextmenu.closeoptions");
            StixUtils.SetContextMenuImage(TM_closeoptions, "manage.png");

            c_activemap.Text = Utils.getString("tools.closeoptions.activemap");
            c_alwayssave.Text = Utils.getString("tools.closeoptions.saveconfirm");

            ToolStripItem tsi = cmsManage.Items.Add(Utils.getString("tools.managetools"));
            tsi.Name = "NewTool";
            StixUtils.SetContextMenuImage(tsi, "tool.png");
            StixUtils.SetCommonContextMenu(cmsManage, StixUtils.typetools);

            cmsTool.ItemClicked += ContextMenuTool_ItemClicked;
            cmsManage.ItemClicked += ContextMenuManage_ItemClicked;
            ////////////////// end Context menu

            Utils.InitIcons();

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from TOOLS where stixID=" + ID + " order by _order");
                foreach (DataRow row in dt.Rows)
                {
                    string title = row["title"].ToString();
                    string path = row["path"].ToString();
                    string type = row["type"].ToString();
                    string tooltip = row["tooltip"].ToString();
                    int order = Convert.ToInt32(row["_order"].ToString());

                    Tools.Add(new ToolItem(title, path, type, order, tooltip));
                }
            }

            RefreshStick();

            this.MouseDown += Move_Stick;
            pictureHandle.MouseDown += Move_Stick;
            Manage.Click += Manage_Click;

            // Handle drag drop to place icon to the begin
            pictureHandle.AllowDrop = true;
            pictureHandle.DragEnter += Handle_DragEnter;
            pictureHandle.DragDrop += Handle_DragDrop;
            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();

            // Apply scale factor
            this.Paint += this_Paint; // paint the border depending on scale factor
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);

            TM_closeoptions.DropDown.Closing += DropDown_Closing;
        }

        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
            else
                cmsTool.Close();
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            scaleFactor = toScale;

            if (scale != 1)
            {
                this.Scale(new SizeF(scale, scale)); // reset to 100%
                StixUtils.icondist = pIconDist.Width;
                MinLength = (int)(MinLength * scale);
            }

            if (toScale != 100)
            {
                this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale
                StixUtils.icondist = pIconDist.Width;
                MinLength = (int)(MinLength * (toScale / 100));
            }
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            if (scaleFactor < 125) return;
            int width = 1;
            //if (scaleFactor > 200) width = 2;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsManage.Items)
                item.Visible = true;

            cmsManage.Show(Cursor.Position);
        }

        private void Move_Stick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ContextMenuTool_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "TM_edit")
            {
                ToolItem item = (ToolItem)selectedIcon.Tag;
                if (item == null) return;

                using (EditToolDlg dlg = new EditToolDlg())
                {
                    if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;
                }

                // Get new tool's name
                string name = StixUtils.GetName(this, orientation, StixUtils.typetools, item.Title);
                if (name != "")
                {
                    // Change title in the picture box tag
                    ((ToolItem)selectedIcon.Tag).Title = name;
                    // Change title in the Tool list item
                    Tools.Find(p => p.Path == item.Path).Title = name;
                    toolTip1.SetToolTip(selectedIcon, name);

                    // Change title in the database
                    using (StixDB db = new StixDB())
                        db.ExecuteNonQuery("update TOOLS set title=`" + name + "` where path=`" +
                            item.Path + "` and stixID=" + (int)this.Tag + "");
                }
            }
            else if (e.ClickedItem.Name == "TM_delete")
            {
                StixUtils.Tools.Clear(); StixUtils.Tools.AddRange(Tools);
                StixUtils.DeleteIcon(selectedIcon, (int)this.Tag, StixUtils.typetools);
                Tools.Clear(); Tools.AddRange(StixUtils.Tools);
                RefreshStick();
            }
        }

        private void ContextMenuManage_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "NewTool")
            {
                using (ManageToolsDlg dlg = new ManageToolsDlg(this))
                    dlg.ShowDialog();
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                Tools.Clear();
                RefreshStick(true);
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixMain.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "ToolStix.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_newstick")
            {
                string name = StixUtils.GetName(this, orientation, StixUtils.typestick, "");
                if (name != "")
                {
                    StixTools form = new StixTools(0, orientation, name);
                    StixUtils.CreateStick(form, name, StixUtils.typetools);
                }
            }
            else if (e.ClickedItem.Name == "BI_renamestick")
            {
                string newName = StixUtils.GetName(this, orientation, StixUtils.typetools, 
                    toolTip1.GetToolTip(pictureHandle), true);
                if (newName != "") toolTip1.SetToolTip(pictureHandle, newName);
            }
            else if (e.ClickedItem.Name == "BI_delete_stick")
            {
                if (StixUtils.DeleteStick((int)this.Tag, StixUtils.typetools))
                    this.Close();
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typetools, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation, ToolList);
        }

        public void NewIcon(string toolPath, string toolTitle, string position, string type = "", string tooltip = "")
        {
            if (Tools.Find(t => t.Path == toolPath) != null)
            {
                MessageBox.Show(Utils.getString("tools.toolexists"), "");
                return;
            };

            // Add icon to Tools list
            int order = Tools.Count + 1; // at the end
            if (position == "begin")
                order = 1;
            if (position == "left" || position == "right")
            {
                ToolItem _item = (ToolItem)selectedIcon.Tag;
                if (position == "left")
                    order = _item.Order == 1 ? 1 : _item.Order;
                else
                    order = _item.Order == Tools.Count ? Tools.Count + 1 : _item.Order + 1;
            }

            if (type == "")
                type = Utils.GetFileType(toolPath);

            ToolItem item = new ToolItem(toolTitle, toolPath, type, order, tooltip);
            using (StixDB db = new StixDB())
                db.AddTool(toolTitle, tooltip, toolPath, type, order, (int)this.Tag);

            Tools.Insert(order - 1, item);
            for (int i = 0; i < Tools.Count; i++)
                Tools[i].Order = i + 1;

            RefreshStick();
        }

        private void ToolList_Click(object sender, EventArgs e)
        {
            if (Tools.Count == 0) return;

            if (aToolList == null || aToolList.IsDisposed || !aToolList.Visible)
            {
                aToolList = null;
                aToolList = new ToolListDlg(Tools);
                aToolList.ToolStix = this;
            }
            else return;

            int itemheight = aToolList.listView1.GetItemRect(0).Height;
            if (Tools.Count <= 12) // If not a big amount, change ListView height to adjust items count 
            {
                aToolList.thisHeight = Tools.Count * itemheight + aToolList.itemHeight.Width;
                aToolList.listView1.Scrollable = false;
            }

            // Get tools list location
            Rectangle child = aToolList.RectangleToScreen(aToolList.ClientRectangle);
            aToolList.Location = StixUtils.GetChildLocation(this, child, orientation, "tools");

            aToolList.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        void RefreshStick(bool deleteall = false)
        {
            StixUtils.Tools.Clear(); StixUtils.Tools.AddRange(Tools);
            List<PictureBox> pBoxs = StixUtils.RefreshStick(this, p1, orientation, MinLength, 
                StixUtils.typetools, deleteall);

            int i = 0;
            foreach (PictureBox pBox in pBoxs)
            {
                pBox.MouseClick += Icon_Click;
                pBox.MouseMove += PBox_MouseMove;
                pBox.DragEnter += Handle_DragEnter;
                pBox.DragDrop += Handle_DragDrop;
                pBox.MouseDown += PBox_MouseDown;
            }
        }

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;
            ToolItem icon = selectedIcon.Tag as ToolItem;

            if (e.Button == MouseButtons.Left)
            {
                RunTool(icon);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTool.Items)
                    item.Visible = true;

                if (icon.Path != "OT_CloseAll")
                {
                    TM_closeoptions.Visible = false;
                    toolStripSeparator1.Visible = false;
                }

                cmsTool.Show(Cursor.Position);
            }
        }

        private void c_activemap_Click(object sender, EventArgs e)
        {
            if (c_activemap.Checked)
                OmniTools.donotcloseactivemap = true;
            else
                OmniTools.donotcloseactivemap = false;
        }

        private void c_alwayssave_Click(object sender, EventArgs e)
        {
            if (c_alwayssave.Checked)
                OmniTools.savemapsaskme = false;
            else
                OmniTools.savemapsaskme = true;
        }

        /// <summary>Run tool</summary>
        public void RunTool(ToolItem item)
        {
            string path = item.Path;
            try
            {
                if (path.StartsWith("OT_")) // Omni function
                    OmniTools.RunTool(path, this, orientation);
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
                    MessageBox.Show(Utils.getString("tools.run.filenotfound.1") +
                        Utils.getString("tools.run.filenotfound.2"));
                }
                else // unknown reason
                    MessageBox.Show(Utils.getString("tools.run.error"));
            }
        }

        public void RunWindowsTool(string path, string title)
        {
            try
            {
                path = path.Substring(3);
                Process.Start("explorer.exe", @" shell:appsFolder\" + path);
            }
            catch
            {
                MessageBox.Show(String.Format(Utils.getString("tools.run.error"), title),  "", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public ProcessStartInfo GetStartInfo(string app)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C explorer.exe shell:Appsfolder\\" + app + "_8wekyb3d8bbwe!App";
           return startInfo;
        }

        #region DragDrop
        private void Handle_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox))) // Move the picture box
            {
                var tool = (PictureBox)e.Data.GetData(typeof(PictureBox)); // moving PB
                int toolIndex = (tool.Tag as ToolItem).Order; // moving PB order
                int targetIndex = 0;

                if (sender is PictureBox) // also, can be this form
                {
                    var target = (PictureBox)sender;

                    if (target.Name == "pictureHandle")
                        targetIndex = 0; // move PB to the begin
                    else
                    {
                        // or after the target PB
                        try{ targetIndex = (target.Tag as ToolItem).Order; }
                        catch { }
                    }
                }
                else // sender is this form or something else (moving PB to the end)
                    targetIndex = this.Controls.OfType<PictureBox>().Count() - 2; // minus pictureHandle and p1

                if (toolIndex != targetIndex)
                {
                    // Reorder Tools list
                    Tools.RemoveAt(toolIndex - 1);
                    if (toolIndex < targetIndex) { targetIndex--; }
                    Tools.Insert(targetIndex, tool.Tag as ToolItem);
                    for (int i = 0; i < Tools.Count; i++)
                        Tools[i].Order = i + 1;

                    RefreshStick();
                }
            }
            else // Drop *dragged* data
            {
                string path = (string)e.Data.GetData(DataFormats.UnicodeText, false);
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string title = StixUtils.Handle_DragDrop(ref path, draggedFiles, null, Tools);
                if (title == "") return;

                if (path != "")
                {
                    position = "end";
                    if (sender is PictureBox)
                    {
                        var target = (PictureBox)sender;
                        if (target.Name == "pictureHandle")
                            position = "begin";
                        else
                        {
                            // or after the target PB
                            selectedIcon = (PictureBox)sender;
                            position = "right";
                        }
                    }

                    // Get tool name
                    string name = StixUtils.GetName(this, orientation, StixUtils.typetools, title);
                    if (name != "")
                        NewIcon(path, name, position);
                }
            }
        }

        private void Handle_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox))) // Moving picture box
                e.Effect = DragDropEffects.Move;
            else // Dragging data
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                    e.Data.GetDataPresent(DataFormats.UnicodeText))
                    e.Effect = DragDropEffects.Copy; // Okay
                else
                    e.Effect = DragDropEffects.None; // Unknown data, ignore it
            }
        }

        private void PBox_MouseDown(object sender, MouseEventArgs e)
        {
            cursor = Cursor.Position;
        }
        Point cursor;

        private void PBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var pb = (PictureBox)sender;
                if (Math.Abs(Cursor.Position.X - cursor.X) > Manage.Width / 2 ||
                    Math.Abs(Cursor.Position.Y - cursor.Y) > Manage.Width / 2)
                    pb.DoDragDrop(pb, DragDropEffects.Move);
            }
        }
        #endregion

        public List<ToolItem> Tools = new List<ToolItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";

        ToolListDlg aToolList = null;
        MapContentDlg aNavigationDlg = null;

        int MinLength;
        public float scaleFactor = 100;

        string position;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public class ToolItem
    {
        public ToolItem(string title, string path, string type, int order, string tooltip)
        {
            Order = order;
            Path = path;
            Title = title;
            Type = type;
            Tooltip = tooltip;
        }

        public string Title = "";
        public int Order = 0;
        public string Path = "";
        public string Type = "";
        public string Tooltip = "";
    }
}
