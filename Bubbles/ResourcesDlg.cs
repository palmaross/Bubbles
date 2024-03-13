using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;

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

            txtResources.Text = Utils.getString("ResourcesDlg.dummytext");
            txtResources.Tag = "";

            toolTip1.SetToolTip(pMore, Utils.getString("ResourcesDlg.pMore"));
            toolTip1.SetToolTip(pRemoveFilter, Utils.getString("ResourcesDlg.contextmenu.r_removefilter"));
            toolTip1.SetToolTip(pHelp, Utils.getString("button.help"));
            toolTip1.SetToolTip(pClose, Utils.getString("button.close"));
            toolTip1.SetToolTip(pResourceMode, Utils.getString("ResourcesDlg.resourcemode.assign.tooltip"));
            toolTip1.SetToolTip(ResourceEnter, Utils.getString("ResourcesDlg.addresourcetotopic.tooltip"));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            thisHeight = this.Height;
            sortype = "sortAZ";

            // Context menu
            cmsMenu.ItemClicked += ContextMenuStrip_ItemClicked;

            r_newresource.Text = Utils.getString("ResourcesDlg.NewResorce");
            StickUtils.SetContextMenuImage(r_newresource, "addicon.png");
            AddToTopic.Text = Utils.getString("ResourcesDlg.btnAddToTopic");
            AddToMap.Text = Utils.getString("ResourcesDlg.btnAddToMap");
            AddToMap.ToolTipText = Utils.getString("ResourcesDlg.btnAddToMap.tooltip");
            ChangeIcon.Text = Utils.getString("ResourcesDlg.ChangeIcon");
            StickUtils.SetContextMenuImage(ChangeIcon, "1.png");
            RemoveIcon.Text = Utils.getString("ResourcesDlg.RemoveIcon");
            StickUtils.SetContextMenuImage(RemoveIcon, "removeicon.png");
            r_rename.Text = Utils.getString("button.rename");
            StickUtils.SetContextMenuImage(r_rename, "edit.png");
            r_delete.Text = Utils.getString("button.delete");
            StickUtils.SetContextMenuImage(r_delete, "deleteall.png");

            r_filter.Text = Utils.getString("ResourcesDlg.contextmenu.r_filter");
            filter_selected.Text = Utils.getString("ResourcesDlg.contextmenu.r_filtershow");
            StickUtils.SetContextMenuImage(filter_selected, "filter.png");
            filter_icon.Text = Utils.getString("ResourcesDlg.contextmenu.r_filtericon");
            StickUtils.SetContextMenuImage(filter_icon, "filterbyicon.png");
            filter_remove.Text = Utils.getString("ResourcesDlg.contextmenu.r_removefilter");
            StickUtils.SetContextMenuImage(filter_remove, "filterremove.png");

            r_sort.Text = Utils.getString("notes.contextmenu.sort");
            sortAZ.Text = Utils.getString("notes.contextmenu.sortAZ");
            StickUtils.SetContextMenuImage(sortAZ, "sortAZ.png");
            sortZA.Text = Utils.getString("notes.contextmenu.sortZA");
            StickUtils.SetContextMenuImage(sortZA, "sortZA.png");
            sortByIcon.Text = Utils.getString("ResourcesDlg.sortByIcon");
            StickUtils.SetContextMenuImage(sortByIcon, "1.png");

            cmsMenu.Items["ManageIcons"].Text = Utils.getString("ResourcesDlg.pManageIcons");
            StickUtils.SetContextMenuImage(cmsMenu.Items["ManageIcons"], "5.png");

            this.Paint += this_Paint; // paint the border
            this.MinimumSize = new Size(this.Width, this.Height / 2);
            this.MaximumSize = new Size(this.Width, Screen.AllScreens.Max(s => s.Bounds.Height));
            this.Deactivate += ResourcesDlg_Deactivate;

            //add single column; -2 => autosize
            ListResources.Columns.Add("MyColumn", -2, HorizontalAlignment.Left);
            ListResources.GridLines = true;

            // Set icons
            imageList1.ImageSize = pHelp.Size;
            for (int i = 1; i < 8; i++)
            imageList1.Images.Add(i.ToString(), Image.FromFile(Utils.ImagesPath + i.ToString() + ".png"));

            // Manage resource icons User Control
            mri.Location = panel1.Location;
            this.Controls.Add(mri);
            mri.Visible = false;
            mri.aParentForm = this;

            pResourceMode.BringToFront(); ResourceEnter.BringToFront();
            txtResources.AutoSize = false;
            txtResources.Size = new Size(txtResources.Width, ResourceEnter.Width);

            Init();
        }

        private void ResourcesDlg_Deactivate(object sender, EventArgs e)
        {
            panelIcons.Visible = false;
        }

        public void Init()
        {
            ListResources.Clear();

            using (SticksDB db = new SticksDB())
            {
                // Add icon names to dictionary
                DataTable dt = db.ExecuteQuery("select * from RESOURCEGROUPS");
                foreach (DataRow dr in dt.Rows)
                {
                    int icon = Convert.ToInt32(dr["icon"]);
                    if (icon > 0 && !ResourceIcons.ContainsKey(icon))
                        ResourceIcons.Add(icon, dr["name"].ToString());
                }
                // Set tooltips to icons
                foreach (PictureBox pb in panelIcons.Controls.OfType<PictureBox>())
                {
                    int i = Convert.ToInt32(pb.Name.Substring(1));
                    toolTip1.SetToolTip(pb, ResourceIcons[i]);
                }

                // Fill Resource List
                dt = db.ExecuteQuery("select * from RESOURCES order by name");
                foreach (DataRow dr in dt.Rows)
                {
                    int icon = Convert.ToInt32(dr["icon"]);
                    ResourceItem item = new ResourceItem(
                        dr["name"].ToString(), icon, dr["color"].ToString(), Convert.ToInt32(dr["groupID"]));

                    ListViewItem lvi = ListResources.Items.Add(item.Name);
                    lvi.Tag = item;
                    if (icon > 0) { 
                        lvi.ImageIndex = icon - 1; lvi.ToolTipText = ResourceIcons[item.aIcon]; }
                }
            }
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "AddToTopic")
            {
                listResources_MouseClick(null, null);
            }
            else if (e.ClickedItem.Name == "AddToMap")
            {
                MapMarkerGroup mmg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);

                foreach (ListViewItem item in ListResources.SelectedItems)
                    mmg.AddResourceMarker(item.Text);
            }
            else if (e.ClickedItem.Name == "ChangeIcon")
            {
                if (selectedItem != null)
                {
                    panelIcons.Location = new Point(panelIcons.Location.X, ListResources.Location.Y + selectedItem.Bounds.Y - pCorrect.Height);
                    panelIcons.Visible = true;
                    ClearIcons();
                    panelIcons.Focus();
                }
            }
            else if (e.ClickedItem.Name == "RemoveIcon")
            {
                ChahgeIcon(0);
            }
            else if (e.ClickedItem.Name == "r_rename")
            {
                if (selectedItem != null)
                {
                    txtRename.Location = new Point(txtRename.Location.X, ListResources.Location.Y + selectedItem.Bounds.Y - pCorrect.Height);
                    txtRename.Visible = true;
                    txtRename.Text = selectedItem.Text;
                    txtRename.Focus();
                }
            }
            else if (e.ClickedItem.Name == "r_delete")
            {
                Delete_Click(null, null);
            }
            else if (e.ClickedItem.Name == "r_newresource")
            {
                txtResources.Tag = "";
                pNewResource_Click(null, null);
            }
            else if (e.ClickedItem.Name == "ManageIcons")
            {
                thisHeight = this.Height;
                if (this.Height < mri.Height + pCorrect.Height)
                    this.Height = mri.Height + pCorrect.Height;

                if (!mri.Visible)
                {
                    mri.Show();
                    mri.BringToFront();
                }
            }
            else if (e.ClickedItem.Name.StartsWith("sort"))
            {
                sortype = e.ClickedItem.Name;
                SortResources();
            }
            else if (e.ClickedItem.Name.StartsWith("filter"))
            {
                FilterResources(e.ClickedItem.Name);
            }
        }

        void ChahgeIcon(int icon = 0)
        {
            if (ListResources.SelectedItems.Count > 1)
            {
                if (MessageBox.Show(Utils.getString("ResourcesDlg.removeicon.question"), "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }

            if (selectedItem == null) return;

            string selecteditemname = selectedItem.Text;

            bool warning = false;
            using (SticksDB db = new SticksDB())
            {
                foreach (ListViewItem lvi in ListResources.SelectedItems)
                {
                    ResourceItem item = lvi.Tag as ResourceItem;

                    if (item.aIcon == icon) continue;

                    DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                        "where name=`" + item.Name + "` and icon=" + icon + "");

                    if (dt.Rows.Count > 0) { warning = true; continue; }

                    // Update resource in the database
                    db.ExecuteNonQuery("update RESOURCES set icon=" + icon + " where name=`" +
                        item.Name + "` and icon=" + item.aIcon + "");

                    item.aIcon = icon;
                    lvi.Remove();
                    ListViewItem newlvi = ListResources.Items.Add(item.Name);
                    newlvi.ToolTipText = "";
                    if (icon > 0) 
                    { 
                        newlvi.ImageIndex = icon - 1;
                        newlvi.ToolTipText = ResourceIcons[icon];
                    }
                    newlvi.Tag = item;
                }
            }

            SortResources();
            ListViewItem _lvi = FindResource(selecteditemname, icon);
            ListResources.Select();
            if (_lvi != null) { _lvi.Selected = true; selectedItem = _lvi; }

            if (warning)
                MessageBox.Show(Utils.getString("ResourcesDlg.removeicon.warning"), "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void panelIcons_Leave(object sender, EventArgs e)
        {
            panelIcons.Visible = false;
        }

        /// <summary>Change resource icon</summary>
        private void Icon_Click(object sender, EventArgs e)
        {
            ClearIcons();
            PictureBox pb = sender as PictureBox;
            // Get clicked icon index
            int i = Convert.ToInt32(pb.Name.Substring(1));

            // "New Resource" mode. User has been selected icon
            if (panelIcons.Location.Y == ListResources.Location.Y)
            {
                selectedIcon = i;
                pb.BackColor = System.Drawing.Color.Cyan;
                return;
            }

            // Change resource icon
            ChahgeIcon(i);
            
            // Hide icons panel
            panelIcons.Visible = false;
        }

        void ClearIcons()
        {
            foreach (PictureBox pb in panelIcons.Controls)
                pb.BackColor = SystemColors.Control;
        }

        private void pManageIcons_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsMenu.Items)
                item.Visible = false;

            r_newresource.Visible = true;
            ManageIcons.Visible = true;
            r_sort.Visible = true;
            sortAZ.Visible = true;
            sortZA.Visible = true;
            sortByIcon.Visible = true;
            toolStripSeparator3.Visible = true;

            cmsMenu.Show(Cursor.Position);
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
                    "where name=`" + name + "` and icon=" + selectedIcon + "");

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panelIcons.Visible = true;
                    return;
                }

                db.AddResource(name, selectedIcon, "", 0);

                item = new ResourceItem(name, selectedIcon, "", 0);

                if (selectedIcon == 0)
                    lvi = ListResources.Items.Add(item.Name);
                else
                    lvi = ListResources.Items.Add(item.Name, selectedIcon);

                lvi.Tag = item;
            }

            SortResources();
            lvi = FindResource(name, item.aIcon);
            ListResources.Select();
            if (lvi != null) {
                lvi.Selected = true; selectedItem = lvi; }
        }

        private void EditResource()
        {
            string name = txtRename.Text.Trim();
            if (String.IsNullOrEmpty(name)) return;

            if (ListResources.SelectedItems.Count > 1)
                return;

            ListViewItem lvi = selectedItem; 
            ResourceItem item = lvi.Tag as ResourceItem;
            string oldName = item.Name;

            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                    "where name=`" + name + "` and icon=" + item.aIcon + "");

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

            foreach (ListViewItem _item in ListResources.Items)
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
            txtRename.Visible = false;
            SortResources();

            lvi = FindResource(name, item.aIcon);
            ListResources.Select();
            if (lvi != null) {
                lvi.Selected = true; lvi.EnsureVisible(); selectedItem = lvi; }
        }

        ListViewItem FindResource(string name, int icon)
        {
            foreach (ListViewItem resource in ListResources.Items)
            {
                ResourceItem item = resource.Tag as ResourceItem;
                if (item.Name == name && item.aIcon == icon)
                    return resource;
            }
            return null;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Utils.getString("ResourcesDlg.deleteicon.question"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (SticksDB db = new SticksDB())
            {
                foreach (ListViewItem item in ListResources.SelectedItems)
                {
                    ResourceItem ritem = item.Tag as ResourceItem;
                    ListResources.Items.Remove(item);

                    // Delete resource from the database
                    db.ExecuteNonQuery("delete from RESOURCES where name=`" + ritem.Name + "` and groupID=" + ritem.GroupID + "");
                }
            }
        }

        private void SortResources()
        {
            List<ResourceItem> items = new List<ResourceItem>();
            foreach (ListViewItem item in ListResources.Items)
                items.Add(item.Tag as ResourceItem);

            if (sortype == "sortAZ" || sortype == "")
                items = items.OrderBy(x => x.Name).ToList();
            else if (sortype == "sortZA")
                items = items.OrderByDescending(x => x.Name).ToList();
            else if (sortype == "sortByIcon")
                items = items.OrderBy(x => x.aIcon).ThenBy(x => x.Name).ToList();

            ListResources.Items.Clear();
            foreach (var item in items)
            {
                if (item.aIcon == 0)
                    ListResources.Items.Add(item.Name).Tag = item;
                else
                    ListResources.Items.Add(item.Name, item.aIcon - 1).Tag = item;
            }
        }

        void FilterResources(string filtertype)
        {
            if (filtertype == "filter_selected")
            {
                for (int i = ListResources.Items.Count - 1; i >= 0; i--)
                {
                    if (!ListResources.Items[i].Selected)
                        ListResources.Items[i].Remove();
                }
                foreach (ListViewItem item in ListResources.Items)
                    item.Selected = false;

                pRemoveFilter.Visible = true;
            }
            else if (filtertype == "filter_icon")
            {
                if (ListResources.SelectedItems.Count == 1)
                {
                    ResourceItem item = ListResources.SelectedItems[0].Tag as ResourceItem;
                    int icon = item.aIcon;
                    for (int i = ListResources.Items.Count - 1; i >= 0; i--)
                    {
                        item = ListResources.Items[i].Tag as ResourceItem;
                        if (item.aIcon != icon)
                            ListResources.Items[i].Remove();
                    }
                    pRemoveFilter.Visible = true;
                }
                else
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.filtericon.tip"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (filtertype == "filter_remove")
            {
                pRemoveFilter.Visible = false;
                Init();
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
            if (ListResources.SelectedItems.Count == 0) return;

            if (e == null || // from context menu
                e.Button == MouseButtons.Left)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control ||
                    (ModifierKeys & Keys.Shift) == Keys.Shift)
                    return;

                string[] listResources = new string[ListResources.SelectedItems.Count];
                for (int i = 0; i < ListResources.SelectedItems.Count; i++)
                    listResources[i] = ListResources.SelectedItems[i].Text;

                SetResources(listResources);
            }
            else if (e.Button == MouseButtons.Right) // ContextMenu
            {
                // Get selected item
                for (int i = 0; i < ListResources.Items.Count; i++)
                {
                    var rectangle = ListResources.GetItemRect(i);
                    if (rectangle.Contains(e.Location))
                    {
                        selectedItem = ListResources.Items[i];
                        break;
                    }
                }

                // Show context menu
                foreach (ToolStripItem item in cmsMenu.Items)
                    item.Visible = true;

                r_newresource.Visible = false;
                ManageIcons.Visible = false;
                r_sort.Visible = false;
                sortAZ.Visible = false;
                sortZA.Visible = false;
                sortByIcon.Visible = false;
                toolStripSeparator3.Visible = false;

                cmsMenu.Show(Cursor.Position);
            }
        }

        public static void SetResources(string[] listResources)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            string listResource = listResources[0];

            bool alltopicshaveresource = false;

            // If more than one items selected, do not calculate resoource presence on topics
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
                    if (taskResources.Contains(listResource))
                        t.Task.Resources = t.Task.Resources.Replace(listResource, "").TrimEnd(new Char[] { ',', ' ' });
                }
                else // assign resource to topics
                {
                    var newResources = taskResources.Union(listResources).ToArray();
                    string newresources = "";

                    foreach (string resource in newResources)
                        newresources += ", " + resource;

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
                ResourceEnter_Click(null, null);

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
            string _resources = txtResources.Text.Trim();
            if (_resources == "") return;

            string[] resources = _resources.Split(',').Select(x => x.Trim()).ToArray();

            if ((string)txtResources.Tag == "newresource") // add new resource(s) to the database
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

        private void txtResources_Enter(object sender, EventArgs e)
        {
            if (txtResources.Text.Trim() == Utils.getString("ResourcesDlg.dummytext") ||
                txtResources.Text.Trim() == Utils.getString("ResourcesDlg.lblResourceName"))
            {
                txtResources.Text = string.Empty;
                txtResources.ForeColor = SystemColors.WindowText;
            }

            if ((string)txtResources.Tag == "newresource")
            {
                panelIcons.Visible = true;
                panelIcons.Location = new Point(panelIcons.Location.X, ListResources.Location.Y);
                ClearIcons();
                selectedIcon = 0;
            }
        }

        private void txtResources_Leave(object sender, EventArgs e)
        {
            if (txtResources.Text.Trim() == "")
            {
                if ((string)txtResources.Tag == "newresource")
                    txtResources.Text = Utils.getString("ResourcesDlg.lblResourceName");
                else
                    txtResources.Text = Utils.getString("ResourcesDlg.dummytext");

                txtResources.ForeColor = SystemColors.GrayText;
            }

            if (!panelIcons.Focused)
                panelIcons.Visible = false;
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

        private void txtRename_Leave(object sender, EventArgs e)
        {
            txtRename.Visible = false;
        }

        private void pNewResource_Click(object sender, EventArgs e)
        {
            if ((string)txtResources.Tag == "newresource")
            {
                txtResources.Tag = "";
                txtResources.BackColor = System.Drawing.Color.AliceBlue;
                pResourceMode.Image = pAssignResource.Image;
                toolTip1.SetToolTip(pResourceMode, Utils.getString("ResourcesDlg.resourcemode.assign.tooltip"));
                toolTip1.SetToolTip(ResourceEnter, Utils.getString("ResourcesDlg.addresourcetotopic.tooltip"));

                if (txtResources.Text == "" || txtResources.Text == Utils.getString("ResourcesDlg.lblResourceName"))
                {
                    txtResources.ForeColor = SystemColors.GrayText;
                    txtResources.Text = Utils.getString("ResourcesDlg.dummytext");
                }
                else
                    txtResources.ForeColor = SystemColors.WindowText;
            }
            else
            {
                txtResources.Tag = "newresource";
                txtResources.BackColor = System.Drawing.Color.Moccasin;
                pResourceMode.Image = pNewResource.Image;
                toolTip1.SetToolTip(pResourceMode, Utils.getString("ResourcesDlg.resourcemode.new.tooltip"));
                toolTip1.SetToolTip(ResourceEnter, Utils.getString("ResourcesDlg.addnewresource.tooltip"));

                if (txtResources.Text == "" || txtResources.Text == Utils.getString("ResourcesDlg.dummytext"))
                {
                    txtResources.ForeColor = SystemColors.GrayText;
                    txtResources.Text = Utils.getString("ResourcesDlg.lblResourceName");
                }
                else
                    txtResources.ForeColor = SystemColors.WindowText;
            }
            ListResources.Focus();
        }

        ListViewItem selectedItem = null;
        int selectedIcon = 0;
        string sortype = "";
        public int thisHeight;
        public Dictionary<int, string> ResourceIcons = new Dictionary<int, string>();

        ManageResourceIcons mri = new ManageResourceIcons();

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

        private void pRemoveFilter_Click(object sender, EventArgs e)
        {
            pRemoveFilter.Visible = false;
            Init();
        }
    }

    public class ResourceItem
    {
        public ResourceItem(string name, int icon, string color, int groupID)
        {
            Name = name;
            aIcon = icon;
            aColor = color;
            GroupID = groupID;
        }
        public string Name = "";
        public string aColor = "";
        public int aIcon = 0;
        public int GroupID = 0;

        public override string ToString() 
        {
            return Name;
        }
    }
}
