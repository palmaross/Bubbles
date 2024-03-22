using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ResourcesDlg : Form
    {
        public ResourcesDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TaskInfoResources.htm");

            txtCurrentMap.Text = Utils.getString("ResourcesDlg.dummytext");
            txtCurrentMap.ForeColor = SystemColors.GrayText;
            txtAllResources.Text = Utils.getString("ResourcesDlg.dummytext");
            txtAllResources.ForeColor = SystemColors.GrayText;

            toolTip1.SetToolTip(pMore, Utils.getString("ResourcesDlg.pMore"));
            toolTip1.SetToolTip(pHelp, Utils.getString("button.help"));
            toolTip1.SetToolTip(pClose, Utils.getString("button.close"));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            thisHeight = this.Height;

            // Context menu
            cmsResource.ItemClicked += ContextMenu_ItemClicked;

            mi_addtotopic.Text = Utils.getString("ResourcesDlg.menuAddToTopic");
            mi_addtomap.Text = Utils.getString("ResourcesDlg.menuAddToMap");
            mi_addtomap.ToolTipText = Utils.getString("ResourcesDlg.menuAddToMap.tooltip");
            mi_rename.Text = Utils.getString("button.rename");
            mi_delete.Text = Utils.getString("button.delete");

            this.Paint += this_Paint; // paint the border
            this.MinimumSize = new Size(this.Width, this.Height / 2);
            this.MaximumSize = new Size(this.Width, Screen.AllScreens.Max(s => s.Bounds.Height));

            //add single column; -2 => autosize
            ListMapResources.Columns.Add("MyColumn", -2, HorizontalAlignment.Left);
            ListMapResources.GridLines = true;

            //add single column; -2 => autosize
            ListDBResources.Columns.Add("MyColumn", -2, HorizontalAlignment.Left);
            ListDBResources.GridLines = true;

            InitCurrentMapResources();
            InitDataBaseResources();
        }

        public void InitCurrentMapResources()
        {
            ListMapResources.Clear();
            MapResources.Clear();

            MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
            foreach (MapMarker mm in mg)
            {
                string color = "#" + mm.Color.Value.ToString("X");
                if (color == "#0") color = "";

                ResourceItem item = new ResourceItem(mm.Label, color, 0);
                MapResources.Add(item);
            }

            MapResources = MapResources.OrderBy(m => m.Name).ToList();
            foreach (var item in MapResources)
            {
                var res = ListMapResources.Items.Add(item.Name); res.Tag = item;
                if (item.aColor != "")
                    res.BackColor = ColorTranslator.FromHtml(item.aColor);
            }
        }

        public void InitDataBaseResources()
        {
            ListDBResources.Clear();

            using (SticksDB db = new SticksDB())
            {
                // Fill Resource Groups
                cbDataBaseResources.Items.Add("All Resources");
                DataTable dt = db.ExecuteQuery("select * from RESOURCEGROUPS order by name");
                foreach (DataRow dr in dt.Rows)
                    cbDataBaseResources.Items.Add(dr["name"].ToString());
                cbDataBaseResources.SelectedIndex = 0;

                // Fill Resource List
                dt = db.ExecuteQuery("select * from RESOURCES order by name");
                foreach (DataRow dr in dt.Rows)
                {
                    ResourceItem item = new ResourceItem(
                        dr["name"].ToString(), dr["color"].ToString(), Convert.ToInt32(dr["groupID"]));

                    ListViewItem lvi = ListDBResources.Items.Add(item.Name); lvi.Tag = item;
                    if (item.aColor != "")
                        lvi.BackColor = ColorTranslator.FromHtml(item.aColor);
                }
            }
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ListView lv = selectedItem.ListView;
            if (lv == null) return;

            string[] listResources = new string[lv.SelectedItems.Count];
            for (int i = 0; i < lv.SelectedItems.Count; i++)
                listResources[i] = lv.SelectedItems[i].Text;

            // Rename selected resource
            if (e.ClickedItem.Name == "mi_rename")
            {
                selectedItem.ListView.LabelEdit = true;
                selectedItem.BeginEdit();            
            }
            // Add to topic selected resources
            else if (e.ClickedItem.Name == "mi_addtotopic")
            {
                SetResources(listResources, true);
            }
            // Remove topic selected resources
            else if (e.ClickedItem.Name == "mi_remove")
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (String.IsNullOrEmpty(t.Task.Resources))
                        continue;

                    List<string> taskResources = t.Task.Resources.Split(',').Select(x => x.Trim()).ToList();

                    foreach (string resource in listResources)
                        taskResources.Remove(resource);

                    if (taskResources.Count == 0)
                        t.Task.Resources = "";
                    else
                        t.Task.Resources = string.Join(",", taskResources);
                }
            }
            // Delete selected resources from map or database
            if (e.ClickedItem.Name == "mi_delete")
            {
                string message = Utils.getString("ResourcesDlg.delete.map");
                if (lv == ListDBResources) message = Utils.getString("ResourcesDlg.delete.database");

                if (MessageBox.Show(message, "", MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Warning) == DialogResult.Cancel) return;

                // Delete resources from Map Index
                if (lv == ListMapResources)
                {
                    MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                    foreach (MapMarker mm in mg)
                    {
                        if (listResources.Contains(mm.Label))
                            mm.Delete();
                    }
                }
                // Delete resources from database
                else if (lv == ListDBResources)
                {
                    int groupID = 0;

                    using (SticksDB db = new SticksDB())
                    {
                        foreach (string res in listResources)
                        {
                            db.ExecuteNonQuery("delete from RESOURCES " +
                                "where name=`" + res + "` and groupID=" + groupID + "");
                        }
                    }
                }

                // Remove from list
                foreach (ListViewItem item in lv.SelectedItems)
                    lv.Items.Remove(item);
            }
            // Set/change resource color
            if (e.ClickedItem.Name == "mi_color")
            {
                colorDialog1.FullOpen = true;
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                System.Drawing.Color c = colorDialog1.Color;
                string colorHEX = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
                if (colorHEX == "#ffffffff") colorHEX = ""; // white, no color

                using (SticksDB db = new SticksDB())
                {
                    foreach (ListViewItem item in lv.SelectedItems)
                    {
                        ResourceItem ri = item.Tag as ResourceItem;

                        if (item.BackColor == c) continue;

                        if (colorHEX == "") item.BackColor = SystemColors.Window;
                        else item.BackColor = ColorTranslator.FromHtml(colorHEX);

                        // Change resource color in the Map Index
                        if (lv == ListMapResources)
                        {
                            MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                            foreach (MapMarker mm in mg)
                            {
                                if (ri.Name == mm.Label)
                                {
                                    if (colorHEX == "")
                                    {
                                        if (mm.Color.Value == 0) continue;
                                        mm.Color.SetValue(0); continue;
                                    }

                                    int i = int.Parse(colorHEX.Substring(1), System.Globalization.NumberStyles.HexNumber);
                                    if (mm.Color.Value != i) mm.Color.SetValue(i);
                                }
                            }
                        }
                        // Change resource color in the Database
                        else if (lv == ListDBResources)
                        {
                            db.ExecuteNonQuery("update RESOURCES set color=`" + colorHEX +
                                "` where name=`" + ri.Name + "` and groupID=" + ri.GroupID + "");
                        }
                    }
                }
            }
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
        }

        private void NewResource(string name)
        {
            ListViewItem lvi; ResourceItem item;

            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                    "where name=`" + name + "`");

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                db.AddResource(name, "", 0);

                item = new ResourceItem(name, "", 0);

                lvi = ListDBResources.Items.Add(item.Name);

                lvi.Tag = item;
            }

            SortResources();
            lvi = FindResource(name);
            ListDBResources.Select();
            if (lvi != null) {
                lvi.Selected = true; selectedItem = lvi; }
        }

        private void EditResource()
        {
            string name = txtCurrentMap.Text.Trim();
            if (String.IsNullOrEmpty(name)) return;

            if (ListDBResources.SelectedItems.Count > 1)
                return;

            ListViewItem lvi = selectedItem; 
            ResourceItem item = lvi.Tag as ResourceItem;
            string oldName = item.Name;

            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                    "where name=`" + name + "`");

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update resource in the database
                db.ExecuteNonQuery("update RESOURCES set name=`" + name +
                    "` where name=`" + oldName + "`");
            }

            foreach (ListViewItem _item in ListDBResources.Items)
            {
                ResourceItem __item = _item.Tag as ResourceItem;
                if (__item.Name == oldName)
                {
                    __item.Name = name; _item.Tag = __item; _item.Text = name;
                }
            }

            //item.Name = name;
            //lvi.Tag = item;
            //lvi.Text = name;
            txtCurrentMap.Visible = false;
            SortResources();

            lvi = FindResource(name);
            ListDBResources.Select();
            if (lvi != null) {
                lvi.Selected = true; lvi.EnsureVisible(); selectedItem = lvi; }
        }

        ListViewItem FindResource(string name)
        {
            foreach (ListViewItem resource in ListDBResources.Items)
            {
                ResourceItem item = resource.Tag as ResourceItem;
                if (item.Name == name)
                    return resource;
            }
            return null;
        }

        private void SortResources()
        {
            List<ResourceItem> items = new List<ResourceItem>();
            foreach (ListViewItem item in ListDBResources.Items)
                items.Add(item.Tag as ResourceItem);

            items = items.OrderBy(x => x.Name).ToList();

            ListDBResources.Items.Clear();
            foreach (var item in items)
            {
                ListDBResources.Items.Add(item.Name).Tag = item;
            }
        }

        private void pHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "TaskInfoResources.htm");
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listResources_MouseClick(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;
            lv.LabelEdit = false;

            if (lv.SelectedItems.Count == 0) return;

            if (e == null || // from context menu
                e.Button == MouseButtons.Left)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control ||
                    (ModifierKeys & Keys.Shift) == Keys.Shift ||
                    lv.SelectedItems[0] == null)
                    return;

                string[] listResources = new string[] { lv.SelectedItems[0].Text };
                SetResources(listResources);
            }
            else if (e.Button == MouseButtons.Right) // ContextMenu
            {
                // Get selected item
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    var rectangle = lv.GetItemRect(i);
                    if (rectangle.Contains(e.Location))
                    {
                        selectedItem = lv.Items[i];
                        break;
                    }
                }

                // Show context menu
                foreach (ToolStripItem item in cmsResource.Items)
                    item.Visible = true;

                if (lv == ListMapResources)
                    mi_addtomap.Visible = false;
                if (lv.SelectedItems.Count > 1)
                    mi_rename.Visible = false;

                cmsResource.Show(Cursor.Position);
            }
        }

        private void ResourceList_KeyUp(object sender, KeyEventArgs e)
        {
            ListView lv = sender as ListView;

            // Delete selected resources.
            if (e.KeyCode == Keys.Delete)
            {
                int k = lv.SelectedItems.Count;

                string message = Utils.getString("");
                if (lv.Name == "ListDBResources")
                    message = Utils.getString("");

                if (MessageBox.Show(message, Utils.getString(""),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lv.SelectedItems)
                    {
                        ResourceItem res = item.Tag as ResourceItem;

                        if (lv.Name == "ListDBResources") // delete from DB
                        {

                        }
                        else // delete from Map Index (= from map)
                        {

                        }

                        item.Remove(); // delete resource from list
                    }
                }
            }
            // Set selected resources. Edit mode?
            else if (e.KeyCode == Keys.Enter)
            {
                foreach (ListViewItem item in lv.SelectedItems)
                {

                }
            }
            // Select all resources
            else if (e.KeyCode == Keys.A)
            {
                if (ModifierKeys == Keys.Control)
                {
                    foreach (ListViewItem item in lv.Items)
                        item.Selected = true;
                }
            }
        }

        private void SetResources(string[] listResources, bool addforced = false)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            string resources = "";
            foreach (string res in listResources)
                resources += res + ",";
            resources = resources.TrimEnd(',');

            if (cbReplaceResources.Checked)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Task.Resources = resources;
                return;
            }

            string listResource = listResources[0];

            bool alltopicshaveresource = false;

            // If more than one items selected, do not calculate resource presence on topics
            if (listResources.Length == 1)
            {
                alltopicshaveresource = true;
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (String.IsNullOrEmpty(t.Task.Resources))
                        alltopicshaveresource = false;

                    string[] taskResources = t.Task.Resources.Split(',').Select(x => x.Trim()).ToArray();
                    if (!taskResources.Contains(listResource))
                        alltopicshaveresource = false;
                }
            }

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                string[] taskResources = t.Task.Resources.Split(',').Select(x => x.Trim()).ToArray();

                if (alltopicshaveresource) // remove resource from topics
                {
                    if (addforced) continue;
                    if (taskResources.Contains(listResource))
                        t.Task.Resources = t.Task.Resources.Replace(listResource, "").TrimEnd(new Char[] { ',', ' ' });
                }
                else // assign resource to topics
                {
                    var newResources = taskResources.Union(listResources).ToArray();
                    string newresources = "";

                    foreach (string resource in newResources)
                        newresources += "," + resource;

                    t.Task.Resources = newresources;
                }
            }
        }

        /// <summary>
        /// Enter key assigns resource(s) to selected topic(s)
        /// </summary>
        private void txtResources_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResourceEnter_Click(sender, null);

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Add to 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceEnter_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            string _resources = tb.Text.Trim();
            if (_resources == "") return;

            string[] resources = _resources.Split(',').Select(x => x.Trim()).ToArray();

            if ((string)txtCurrentMap.Tag == "newresource") // add new resource(s) to the database
            {
                if (resources.Length > 1)
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.onlyoneresource"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                NewResource(_resources);
            }
            else if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
            {
                // Assign resource(s) to topic(s)
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    string[] topicResources = t.Task.Resources.Split(',').Select(x => x.Trim()).ToArray();
                    string[] newResources = topicResources.Union(resources).ToArray();

                    string result = "";
                    foreach (string res in newResources)
                        result += res + ",";
                    result = result.TrimEnd(',');

                    t.Task.Resources = result;
                }
            }
        }

        private void txtRename_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditResource();

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        List<ResourceItem> MapResources = new List<ResourceItem>();

        ListViewItem selectedItem = null;
        public int thisHeight;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        #region Resize window
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
        #endregion

        private void txtNewResource_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Trim() == "")
            {
                tb.Text = Utils.getString("ResourcesDlg.dummytext");
                tb.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtAllResources_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.ForeColor == SystemColors.GrayText)
            {
                tb.Text = "";
                tb.ForeColor = SystemColors.WindowText;
            }
        }

        private void ListMapResources_Leave(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            lv.LabelEdit = false;
        }

        private void ListResources_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;

            ListView lv = sender as ListView;
            string oldName = lv.SelectedItems[0].Text.Trim();
            string newName = e.Label.Trim();

            // Rename resource in the map
            if (lv == ListMapResources)
            {
                MapMarker mmm = null;

                MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                foreach (MapMarker mm in mg)
                {
                    if (mm.Label == newName)
                    {
                        MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"));
                        e.CancelEdit = true;
                        return; // Resource already exists
                    }
                    else if (mm.Label == oldName)
                        mmm = mm; // We have found our resource
                }

                // Change resource name in the Map Index
                if (mmm != null) mmm.Label = newName;
            }
            // Rename resource in the database 
            else if (lv == ListDBResources)
            {
                ResourceItem res = lv.SelectedItems[0].Tag as ResourceItem;

                using (SticksDB db = new SticksDB())
                {
                    DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                        "where name=`" + newName + "` and groupID=" + res.GroupID);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"));
                        e.CancelEdit = true;
                        return; // Resource already exists
                    }

                    res.Name = newName; lv.SelectedItems[0].Tag = res;

                    db.ExecuteNonQuery("update RESOURCES set name=`" + newName +
                        "` where name=`" + oldName + "` and groupID=" + res.GroupID);
                }
            }
        }

        /// <summary>
        /// Show the tip to cancel editing
        /// </summary>
        private void ListResources_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListView lv = sender as ListView;

            Point p = new Point(pClose.Height, this.PointToClient(Cursor.Position).Y - p11.Height);
            if (lv == ListDBResources)
                p = new Point(pClose.Height, this.PointToClient(Cursor.Position).Y - p11.Width);
            
            ToolTip tip = new ToolTip();
            tip.Show("ESC to cancel edit", this, p, 2000);
        }
    }

    public class ResourceItem
    {
        public ResourceItem(string name, string color, int groupID)
        {
            Name = name;
            aColor = color;
            GroupID = groupID;
        }
        public string Name = "";
        public string aColor = "";
        public int GroupID = 0;

        public override string ToString() 
        {
            return Name;
        }
    }
}
