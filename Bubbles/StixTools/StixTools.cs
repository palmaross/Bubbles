using PRAManager;
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
            toolTip1.SetToolTip(pictureHandle, stickname);

            orientation = _orientation; // "H" or "V"

            MinLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            StixUtils.icondist = pIconDist.Width;

            //// Context menu ////

            TM_rename.Text = Utils.getString("button.rename");
            StixUtils.SetContextMenuImage(TM_rename, "edit.png");

            TM_changeicon.Text = Utils.getString("tools.contextmenu.changeicon");
            StixUtils.SetContextMenuImage(TM_changeicon, "mm_project.ico");

            TM_delete.Text = Utils.getString("button.delete");
            StixUtils.SetContextMenuImage(TM_delete, "deleteall.png");

            PopulateManageCMS();
            StixUtils.SetCommonContextMenu(cmsManage, StixUtils.typetools);

            cmsTool.ItemClicked += ContextMenuTool_ItemClicked;
            cmsManage.ItemClicked += ContextMenuManage_ItemClicked;
            ////////////////// end Context menu

            Utils.InitIcons();

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from TOOLS where stickID=" + ID + " order by _order");
                foreach (DataRow row in dt.Rows)
                {
                    string title = row["title"].ToString();
                    string path = row["path"].ToString();
                    string type = row["type"].ToString();
                    int order = Convert.ToInt32(row["_order"].ToString());

                    Tools.Add(new ToolItem(title, path, type, order));
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

            manage = true;
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

        void PopulateManageCMS()
        {
            ToolStripItem tsi = new ToolStripLabel(Utils.getString("tools.addtool.label"));
            tsi.Font = new Font(tsi.Font, FontStyle.Bold); cmsManage.Items.Add(tsi);

            tsi = cmsManage.Items.Add(Utils.getString("tools.addtool.file"));
            tsi.Name = "MM_file";

            tsi = cmsManage.Items.Add(Utils.getString("tools.addtool.windows"));
            tsi.Name = "MM_windows";

            ToolStripItem tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.notepad"));
            tsm.Name = "notepad_tool";
            StixUtils.SetContextMenuImage(tsm, "tool-notepad.png");
            tsm.Click += SubmenuItem_Click;

            tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.snippng"));
            tsm.Name = "snipping_tool";
            StixUtils.SetContextMenuImage(tsm, "tool-snipping.png");
            tsm.Click += SubmenuItem_Click;

            tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.calculator"));
            tsm.Name = "calc_tool";
            StixUtils.SetContextMenuImage(tsm, "tool-calculator.png");
            tsm.Click += SubmenuItem_Click;

            tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.clock"));
            tsm.Name = "clock_tool";
            StixUtils.SetContextMenuImage(tsm, "tool-clock.png");
            tsm.Click += SubmenuItem_Click;

            tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.stickynotes"));
            tsm.Name = "snotes_tool";
            StixUtils.SetContextMenuImage(tsm, "tool-snotes.png");
            tsm.Click += SubmenuItem_Click;

            tsi = cmsManage.Items.Add(Utils.getString("tools.addtool.omni"));
            tsi.Name = "MM_omni";

            tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.contextmenu.currentmap"));
            tsm.Name = "currentmap_tool";
            StixUtils.SetContextMenuImage(tsm, "ql_map.png");
            tsm.Click += SubmenuItem_Click;

            tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.saveall"));
            tsm.ToolTipText = Utils.getString("tools.saveall.tooltip");
            tsm.Name = "saveall_tool";
            StixUtils.SetContextMenuImage(tsm, "tool-saveall.png");
            tsm.Click += SubmenuItem_Click;

            tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("tools.fastnavigation"));
            tsm.ToolTipText = Utils.getString("tools.fastnavigation.tooltip");
            tsm.Name = "fastnavig_tool";
            StixUtils.SetContextMenuImage(tsm, "tool-navigation.png");
            tsm.Click += SubmenuItem_Click;
        }

        private void SubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = sender as ToolStripItem; // submenu item

            switch (tsi.Name)
            {
                case "notepad_tool":
                    string toolPath = "c:\\Windows\\System32\\notepad.exe";
                    string toolTitle = Utils.getString("tools.notepad");
                    NewIcon(toolPath, toolTitle, "end", "tool-notepad.png");
                    break;
                case "snipping_tool":
                    NewIcon("SnippingTool.exe", Utils.getString("tools.snippng"), "end", "tool-snipping.png");
                    break;
                case "calc_tool":
                    string path = "C:\\Program Files\\WindowsApps\\Microsoft.WindowsCalculator_11.2401.0.0_x64__8wekyb3d8bbwe\\CalculatorApp.exe";
                    NewIcon(path, Utils.getString("tools.calculator"), "end", "tool-calculator.png");
                    break;
                case "clock_tool":
                    NewIcon("OT_clock", Utils.getString("tools.clock"), "end", "tool-clock.png");
                    break;
                case "snotes_tool":
                    path = "C:\\Program Files\\WindowsApps\\Microsoft.MicrosoftStickyNotes_6.0.2.0_x64__8wekyb3d8bbwe\\Microsoft.Notes.exe";
                    NewIcon(path, Utils.getString("tools.stickynotes"), "end", "tool-snotes.png");
                    break;
                case "currentmap_tool":
                    toolPath = MMUtils.ActiveDocument.FullName;
                    toolTitle = MMUtils.ActiveDocument.CentralTopic.Text.Trim();
                    NewIcon(toolPath, toolTitle, "end", "");
                    break;
                case "saveall_tool":
                    NewIcon("OT_saveall", Utils.getString("tools.saveall"), "end", "tool-saveall.png");
                    break;
                case "fastnavig_tool":
                    NewIcon("OT_navigation", Utils.getString("tools.fastnavigation"), "end", "tool-navigation.png");
                    break;
            }
        }

        private void ContextMenuTool_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "TM_changeicon")
            {

            }
            else if (e.ClickedItem.Name == "TM_rename")
            {
                ToolItem item = (ToolItem)selectedIcon.Tag;
                if (item == null) return;

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
                            item.Path + "` and stickID=" + (int)this.Tag + "");
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
            string toolPath, toolTitle, position;
            if (e.ClickedItem.Name == "MM_file")
            {
                using (NewToolDlg _dlg = new NewToolDlg(Tools, manage))
                {
                    if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;
                    toolPath = _dlg.txtPath.Text.Trim();
                    toolTitle = _dlg.txtTitle.Text.Trim();
                }
                NewIcon(toolPath, toolTitle, "end");
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

        private void NewIcon(string toolPath, string toolTitle, string position, string type = "")
        {
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

            ToolItem item = new ToolItem(toolTitle, toolPath, type, order);
            using (StixDB db = new StixDB())
                db.AddTool(toolTitle, toolPath, type, order, (int)this.Tag);

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

            if (e.Button == MouseButtons.Left)
            {
                ToolItem item = selectedIcon.Tag as ToolItem;
                RunTool(item);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTool.Items)
                    item.Visible = true;

                manage = false;
                cmsTool.Show(Cursor.Position);
            }
        }

        /// <summary>Run tool</summary>
        public void RunTool(ToolItem item)
        {
            if (item.Path == "OT_navigation")
            {
                if (MMUtils.ActiveDocument == null) return;

                if (aNavigationDlg == null || aNavigationDlg.IsDisposed || !aNavigationDlg.Visible)
                {
                    aNavigationDlg = null;
                    aNavigationDlg = new NavigationDlg();
                }
                else return;

                int topicCount = MMUtils.ActiveDocument.CentralTopic.SubTopics.Count + 1;
                int itemheight = aNavigationDlg.listView1.GetItemRect(0).Height;

                if (topicCount <= 10) // If not a big amount, change height to adjust items count 
                {
                    aNavigationDlg.thisHeight = topicCount * itemheight + aNavigationDlg.itemHeight.Width;
                    aNavigationDlg.listView1.Scrollable = false;
                }

                // Get tools list location
                Rectangle child = aNavigationDlg.RectangleToScreen(aNavigationDlg.ClientRectangle);
                aNavigationDlg.Location = StixUtils.GetChildLocation(this, child, orientation, "tools");

                aNavigationDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            if (item.Path.StartsWith("OT"))
                OmniTools.RunTool(item.Path);
            else
            {
                try { Process.Start(item.Path); }
                catch { } // todo message to user
            }    
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
        bool manage = false;

        ToolListDlg aToolList = null;
        NavigationDlg aNavigationDlg = null;

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
        public ToolItem(string title, string path, string type, int order)
        {
            Order = order;
            Path = path;
            Title = title;
            Type = type;
        }

        public string Title = "";
        public int Order = 0;
        public string Path = "";
        public string Type = "";
    }
}
