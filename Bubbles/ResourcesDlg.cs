using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace Bubbles
{
    public partial class ResourcesDlg : Form
    {
        public ResourcesDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "TaskInfoResources.htm");

            txtCurrentMap.Text = Utils.getString("ResourcesDlg.dummytext");
            toolTip1.SetToolTip(txtCurrentMap, Utils.getString("ResourcesDlg.addnewresource.curmap.tooltip"));
            txtCurrentMap.ForeColor = SystemColors.GrayText;
            txtNewResourceDB.Text = Utils.getString("ResourcesDlg.dummytext");
            toolTip1.SetToolTip(txtNewResourceDB, Utils.getString("ResourcesDlg.addnewresource.db.tooltip"));
            txtNewResourceDB.ForeColor = SystemColors.GrayText;

            toolTip1.SetToolTip(pHelp, Utils.getString("button.help"));
            toolTip1.SetToolTip(pClose, Utils.getString("button.close"));

            btnRemoveResources.Text = Utils.getString("taskinfo.resources.delete");
            toolTip1.SetToolTip(btnRemoveResources, Utils.getString("taskinfo.resources.delete.tooltip"));
            lblCurrentMap.Text = Utils.getString("ResourcesDlg.lblCurrentMap");

            //lblGroupName.Text = Utils.getString("ResourcesDlg.lblGroupName");

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            thisHeight = this.Height;

            ListDBResources.Columns.Add("", 0, HorizontalAlignment.Left);
            ListDBResources.HeaderStyle = ColumnHeaderStyle.None;
            ListDBResources.Columns[0].Width = ListDBResources.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            ListMapResources.Columns.Add("", 0, HorizontalAlignment.Left);
            ListMapResources.HeaderStyle = ColumnHeaderStyle.None;
            ListMapResources.Columns[0].Width = ListMapResources.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            // Context menu
            cmsResource.ItemClicked += ContextMenu_ItemClicked;
            cmsMore.ItemClicked += ContextMenu_ItemClicked;

            mi_addtotopic.Text = Utils.getString("ResourcesDlg.menuAddToTopic");
            mi_remove.Text = Utils.getString("ResourcesDlg.menuRemoveFromTopic");
            mi_addtomap.Text = Utils.getString("ResourcesDlg.menuAddToMap");
            mi_addtomap.ToolTipText = Utils.getString("ResourcesDlg.menuAddToMap.tooltip");
            mi_rename.Text = Utils.getString("button.rename");
            mi_delete.Text = Utils.getString("button.delete");
            mi_color.Text = Utils.getString("bubbleformat.contextmenu.color");
            mi_copy.Text = Utils.getString("ResourcesDlg.Copy");
            mi_cut.Text = Utils.getString("ResourcesDlg.Cut");

            mm_new.Text = Utils.getString("ResourcesDlg.NewGroup");
            mm_rename.Text = Utils.getString("ResourcesDlg.RenameGroup");
            mm_delete.Text = Utils.getString("ResourcesDlg.DeleteGroup");
            mm_addtomap.Text = Utils.getString("ResourcesDlg.menuAddToMap");
            mm_addtomap.ToolTipText = Utils.getString("ResourcesDlg.menuAddToMap.group.tooltip");
            mm_paste.Text = Utils.getString("ResourcesDlg.Paste");
            mm_pastefromclipboard.Text = Utils.getString("ResourcesDlg.PasteClipboard");

            this.Paint += this_Paint; // paint the border
            this.MinimumSize = new Size(this.Width, this.Height / 2);
            this.MaximumSize = new Size(this.Width, Screen.AllScreens.Max(s => s.Bounds.Height));

            InitCurrentMapResources();
            InitDataBaseResources();

            StixUtils.ActivateMindManager();

            splitContainer.Paint += SplitContainer_Paint;
            this.ResizeEnd += ResourcesDlg_ResizeEnd;
        }

        private void ResourcesDlg_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh(); // to reset splitter
        }

        private void SplitContainer_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            s.SplitterWidth = splitter.Height;

            if (s != null)
            {
                int gripLineWidth = splitter.Width;
                // Fill Splitter rectangle
                e.Graphics.FillRectangle(SystemBrushes.Control,
                    s.SplitterRectangle.X, s.SplitterDistance, s.SplitterRectangle.Width, s.SplitterWidth);
                // Draw gripper dots in center
                Pen _dashedPen = new Pen(Color.Black, splitter.Height / 3);
                _dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawLine(_dashedPen,
                    (s.SplitterRectangle.Width / 2) - (gripLineWidth / 2),
                    s.SplitterDistance + s.SplitterWidth / 2,
                    (s.SplitterRectangle.Width / 2) + (gripLineWidth / 2),
                    s.SplitterDistance + s.SplitterWidth / 2);
            }
        }

        public void InitCurrentMapResources()
        {
            ListMapResources.Items.Clear();
            MapResources.Clear();

            MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
            foreach (MapMarker mm in mg)
            {
                string color = "#" + mm.Color.Value.ToString("X");
                if (color == "#0") color = "";

                ResourceItem item = new ResourceItem(mm.Label, color, 0);

                var res = ListMapResources.Items.Add(mm.Label); res.Tag = item;
                if (color != "")
                {
                    Color c = ColorTranslator.FromHtml(color);
                    res.BackColor = c;
                    int cc = (int)Math.Sqrt(c.R * c.R * .299 + c.G * c.G * .587 + c.B * c.B * .114);
                    if (cc > 130) res.ForeColor = SystemColors.WindowText;
                    else res.ForeColor = SystemColors.Window;
                }

                MapResources.Add(mm.Label, color);
            }
        }

        public void InitDataBaseResources()
        {
            ListDBResources.Items.Clear();
            cbResourceGroup.Items.Clear();

            using (StixDB db = new StixDB())
            {
                // Fill Resource Groups
                ResourceGroup gitem = new ResourceGroup(0, Utils.getString("ResourcesDlg.allresources"));
                cbResourceGroup.Items.Add(gitem);

                DataTable dt = db.ExecuteQuery("select * from RESOURCEGROUPS order by name");

                foreach (DataRow dr in dt.Rows)
                {
                    gitem = new ResourceGroup(Convert.ToInt32(dr["id"]), dr["name"].ToString());
                    cbResourceGroup.Items.Add(gitem);
                }

                if (cbResourceGroup.Items.Count > 0)
                    cbResourceGroup.SelectedIndex = 0;
            }
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ListView lv = selectedList;

            // New group.
            if (e.ClickedItem.Name == "mm_new")
            {
                txtNewResourceDB.BackColor = SystemColors.GradientInactiveCaption;
                txtNewResourceDB.Text = Utils.getString("ResourcesDlg.group.dummytext");
                txtNewResourceDB.Font = new Font(txtNewResourceDB.Font, FontStyle.Bold);
                txtNewResourceDB.ForeColor = SystemColors.GrayText;
                txtNewResourceDB.Tag = "new";
                txtNewResourceDB.Enabled = true;
                btnClose.Visible = true;
            }
            // Rename group.
            else if (e.ClickedItem.Name == "mm_rename")
            {
                ResourceGroup g = cbResourceGroup.SelectedItem as ResourceGroup;
                txtNewResourceDB.BackColor = SystemColors.GradientInactiveCaption;
                txtNewResourceDB.Text = g.Name + " (2)";
                txtNewResourceDB.Font = new Font(txtNewResourceDB.Font, FontStyle.Bold);
                txtNewResourceDB.ForeColor = SystemColors.WindowText;
                txtNewResourceDB.Tag = "rename";
                btnClose.Visible = true;
            }
            // Delete group.
            else if (e.ClickedItem.Name == "mm_delete")
            {
                ResourceGroup g = cbResourceGroup.SelectedItem as ResourceGroup;

                if (lv.Items.Count == 0 ||
                    MessageBox.Show(String.Format(Utils.getString("ResourcesDlg.delete.group"), g.Name),
                    "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    using (StixDB db = new StixDB())
                    {
                        db.ExecuteNonQuery("delete from RESOURCES where groupID=" + g.GroupID + "");
                        db.ExecuteNonQuery("delete from RESOURCEGROUPS where id=" + g.GroupID + "");
                    }

                    cbResourceGroup.Items.Remove(g);
                    if (cbResourceGroup.Items.Count > 0) 
                        cbResourceGroup.SelectedIndex = 0;
                }
            }
            // Add all group's resources to Map Index
            else if (e.ClickedItem.Name == "mm_addtomap")
            {
                Dictionary<string, string> _resources = new Dictionary<string, string>();
                foreach (ListViewItem item in lv.Items)
                {
                    ResourceItem res = item.Tag as ResourceItem;
                    _resources.Add(res.Name, res.aColor);
                }
                AddResourcesToMap(_resources);
            }
            // Paste copied resources to map or to group.
            else if (e.ClickedItem.Name == "mm_paste")
            {
                PasteResources(lv);
            }
            // Paste from clipboard to map or to group.
            else if (e.ClickedItem.Name == "mm_pastefromclipboard")
            {
                PasteResources(lv, true); return;
            }

            if (lv == null) return;

            string[] listResources = new string[lv.SelectedItems.Count];
            for (int i = 0; i < lv.SelectedItems.Count; i++)
                listResources[i] = lv.SelectedItems[i].Text;

            Dictionary<string, string> resources = new Dictionary<string, string>();
            foreach (ListViewItem item in lv.SelectedItems)
            {
                ResourceItem res = item.Tag as ResourceItem;
                resources.Add(res.Name, res.aColor);
            }

            // Add selected resources to topic
            if (e.ClickedItem.Name == "mi_addtotopic")
            {
                AddResourcesToMap(resources);
                SetResources(listResources, true);
            }
            // Remove selected resources from topic
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
            // Add selected resource to Map Index
            else if (e.ClickedItem.Name == "mi_addtomap")
            {
                AddResourcesToMap(resources);
            }
            // Rename selected resource
            else if (e.ClickedItem.Name == "mi_rename")
            {
                selectedItem.ListView.LabelEdit = true;
                selectedItem.BeginEdit();
            }
            // Delete selected resources from map or database
            if (e.ClickedItem.Name == "mi_delete")
            {
                // Delete resources from Map Index
                if (lv == ListMapResources)
                {
                    if (MessageBox.Show(Utils.getString("ResourcesDlg.delete.map"), "",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        return;

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
                    DialogResult rc = MessageBox.Show(Utils.getString("ResourcesDlg.delete.map"), "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (rc == DialogResult.Cancel) return;

                    ResourceGroup g = cbResourceGroup.SelectedItem as ResourceGroup;
                    int groupID = g.GroupID;

                    using (StixDB db = new StixDB())
                    {
                        foreach (string res in listResources)
                        {
                            if (rc == DialogResult.Yes) // delete from database
                                db.ExecuteNonQuery("delete from RESOURCES " + 
                                    "where name=`" + res + "`");
                            else // delete from selected group
                                db.ExecuteNonQuery("delete from RESOURCES " +
                                    "where name=`" + res + "` and groupID=" + groupID + "");
                        }
                    }
                }

                // Remove items from list.
                foreach (ListViewItem item in lv.SelectedItems)
                    lv.Items.Remove(item);
            }
            // Set/change resource color
            if (e.ClickedItem.Name == "mi_color")
            {
                colorDialog1.FullOpen = true;
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                Color c = colorDialog1.Color;
                string colorHEX = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
                if (colorHEX == "#ffffffff") colorHEX = ""; // white, no color

                using (StixDB db = new StixDB())
                {
                    foreach (ListViewItem item in lv.SelectedItems)
                    {
                        ResourceItem ri = item.Tag as ResourceItem;

                        if (item.BackColor == c) continue;

                        if (colorHEX == "") { 
                            item.BackColor = SystemColors.Window;
                            item.ForeColor = SystemColors.WindowText;
                        }
                        else {
                            item.BackColor = ColorTranslator.FromHtml(colorHEX);
                            int cc = (int)Math.Sqrt(c.R * c.R * .299 + c.G * c.G * .587 + c.B * c.B * .114);
                            if (cc > 130) item.ForeColor = SystemColors.WindowText;
                            else item.ForeColor = SystemColors.Window;
                        }

                        ri.aColor = colorHEX; item.Tag = ri;

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
            // Copy/Cut selected resource
            else if (e.ClickedItem.Name == "mi_copy" || e.ClickedItem.Name == "mi_cut")
            {
                CopyCutResources(lv, e.ClickedItem.Name == "mi_cut");
            }
        }

        void AddResourcesToMap(Dictionary<string, string> resources)
        {
            MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);

            foreach (var res in resources)
            {
                bool found = false;
                // Check if Map Resources contain selected resource
                foreach (MapMarker mm in mg)
                {
                    if (mm.Label == res.Key)
                    {
                        found = true; break; // yes, contain
                    }
                }
                if (found) continue;  // yes, contain

                // Add resource to Map Resources
                MapMarker mmm = mg.AddResourceMarker(res.Key);
                // Set color
                if (res.Value != "")
                {
                    int i = int.Parse(res.Value.Substring(1), System.Globalization.NumberStyles.HexNumber);
                    mmm.Color.SetValue(i);
                }
            }
        }

        void CopyCutResources(ListView lv, bool cut)
        {
            if (lv.SelectedItems.Count == 0) return;
            CopiedResources.Clear();
            string resources = "";

            foreach (ListViewItem item in lv.SelectedItems)
            {
                ResourceItem res = item.Tag as ResourceItem;
                CopiedResources.Add(res);
                resources += res.Name + "\r\n";

                if (cut) item.Remove();
            }

            resources = resources.TrimEnd('\r', '\n');
            System.Windows.Forms.Clipboard.SetText(resources);

            string message = Utils.getString("ResourcesDlg.copy.success");
            MMUtils.MindManager.NotificationsDialog.ShowNotification("", message, MmNotificationsDialogOptions.mmNotificationsDialogOptionsAutoClose);
        }

        void PasteResources(ListView lv, bool clipboard = false)
        {
            string show = "", group = ""; int groupID = 0;
            List<ResourceItem> resources = new List<ResourceItem>();

            if (clipboard)
            {
                string text = System.Windows.Forms.Clipboard.GetText().Trim();
                if (text == "") {
                    MessageBox.Show(Utils.getString("pasteresources.clipboard.empty")); return; }

                string[] parts = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] pparts = part.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                    foreach (var res in pparts)
                    {
                        if (res.Length > 40) // It's too long for resource. Clipboard no valid
                        {
                            MessageBox.Show(Utils.getString("pasteresources.clipboard.notvalid")); return;
                        }

                        show += "\r\n" + res;
                        resources.Add(new ResourceItem(res, "", 0));
                    }
                }
            }
            else // paste copied resources
            {
                if (CopiedResources.Count == 0) return;

                foreach (var res in CopiedResources) show += "\r\n" + res.Name;
                resources.AddRange(CopiedResources);
            }

            string where = Utils.getString("pasteresources.preview.curmap");
            if (lv == ListDBResources)
            {
                ResourceGroup item = cbResourceGroup.SelectedItem as ResourceGroup;
                if (item.GroupID == 0)  {
                    MessageBox.Show(Utils.getString("pasteresources.group.error")); return;
                }

                groupID = item.GroupID; group = item.Name;
                where = String.Format(Utils.getString("pasteresources.preview.group"), group);
            }

            if (MessageBox.Show(String.Format(Utils.getString("pasteresources.preview"),where, show),
                Utils.getString("pasteresources.clipboard.title"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (StixDB db = new StixDB())
                {
                    foreach (var res in resources)
                    {
                        if (lv == ListMapResources)
                        {
                            MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);

                            bool found = false;
                            // Check if Map Resources contain selected resource
                            foreach (MapMarker mm in mg)
                            {
                                if (mm.Label == res.Name)
                                {
                                    found = true; break; // yes, contain
                                }
                            }
                            if (found) continue;  // yes, contain

                            // Add resource to Map Resources
                            MapMarker mmm = mg.AddResourceMarker(res.Name);
                            // Set color
                            if (res.aColor != "")
                            {
                                int i = int.Parse(res.aColor.Substring(1), System.Globalization.NumberStyles.HexNumber);
                                mmm.Color.SetValue(i);
                            }
                        }
                        else // Paste to selected group
                        {
                            // Add to database
                            DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                                "where name=`" + res.Name + "` and groupID=" + groupID + "");

                            if (dt.Rows.Count == 0)
                                db.AddResource(res.Name, res.aColor, groupID);

                            // Add to list
                            bool found = false;
                            foreach (ListViewItem item in ListDBResources.Items)
                                if (item.Text == res.Name) found = true;
                            if (found) continue;

                            ListViewItem lvi = ListDBResources.Items.Add(res.Name);
                            res.GroupID = groupID; lvi.Tag = res;

                            if (res.aColor != "")
                            {
                                Color c = ColorTranslator.FromHtml(res.aColor);
                                lvi.BackColor = c;

                                int cc = (int)Math.Sqrt(c.R * c.R * .299 + c.G * c.G * .587 + c.B * c.B * .114);
                                if (cc > 130) lvi.ForeColor = SystemColors.WindowText;
                                else lvi.ForeColor = SystemColors.Window;
                            }
                        }
                    }
                }
            }
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
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
            selectedList = lv;
            lv.LabelEdit = false;

            if (e.Button == MouseButtons.Left && lv.SelectedItems.Count == 1)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control ||
                    (ModifierKeys & Keys.Shift) == Keys.Shift ||
                    lv.SelectedItems[0] == null)
                    return;

                if (MMUtils.ActiveDocument.Selection.PrimaryTopic != null)
                {
                    Dictionary<string, string> resources = new Dictionary<string, string>();
                    foreach (ListViewItem item in lv.SelectedItems)
                    {
                        ResourceItem res = item.Tag as ResourceItem;
                        resources.Add(res.Name, res.aColor);
                    }
                    AddResourcesToMap(resources);

                    string[] listResources = new string[] { lv.SelectedItems[0].Text };
                    SetResources(listResources);
                }
            }
            else if (e.Button == MouseButtons.Right) // ContextMenu
            {
                if (lv.SelectedItems.Count == 0)
                {
                    foreach (ToolStripItem item in cmsResource.Items)
                        item.Visible = false;

                    mm_paste.Visible = true;
                    cmsResource.Show(Cursor.Position);
                    return;
                }

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
                {
                    mi_addtomap.Visible = false;
                    mi_cut.Visible = false;
                }
                else if (lv == ListDBResources)
                {
                    ResourceGroup rg = cbResourceGroup.SelectedItem as ResourceGroup;
                    if (rg.GroupID == 0)
                    {
                        mi_delete.Visible = false;
                        mi_cut.Visible = false;
                        mi_color.Visible = false;
                    }
                }

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

                string message = Utils.getString("ResourcesDlg.delete.map");
                if (lv.Name == "ListDBResources")
                    message = Utils.getString("ResourcesDlg.delete.database");

                if (MessageBox.Show(message, Utils.getString("ResourcesDlg.delete.title"),
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
            // Copy/Cut selected resources.
            else if (e.KeyCode == Keys.C || e.KeyCode == Keys.X)
            {
                if (ModifierKeys == Keys.Control)
                    CopyCutResources(lv, e.KeyCode == Keys.X);
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

        /// <summary>
        /// Add selected resources to topic and to map
        /// </summary>
        /// <param name="listResources">Resources to add. Name and color</param>
        /// <param name="addforced">if command is from context menu</param>
        private void SetResources(string[] listResources, bool addforced = false)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            // Get string for replace
            string resources = "";
            foreach (string res in listResources)
                resources += res + ",";
            resources = resources.TrimEnd(',');

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
            TextBox tb = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                // New Resource add to topics and map.
                if (tb.Tag.ToString() == "resource")
                {
                    ResourceEnter_Click(sender, null);
                }
                // New Group add. Or rename group.
                else
                {
                    if (tb.Tag.ToString() == "new")
                    {
                        ResourceGroup item;

                        string newName = txtNewResourceDB.Text.Trim();
                        if (newName == "") return;

                        if (cbResourceGroup.Items.Count > 0)
                        {
                            if (cbResourceGroup.SelectedItem != null)
                            {
                                item = cbResourceGroup.SelectedItem as ResourceGroup;
                                if (newName == item.Name) return;
                            }
                        }

                        using (StixDB db = new StixDB())
                        {
                            DataTable dt = db.ExecuteQuery("select from RESOURCEGROUPS where name=`" + newName + "`");
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show(Utils.getString("ResourcesDlg.groupexists")); return;
                            }
                            db.AddResourceGroup(newName);
                        }

                        // Close group mode of the txtNewResourceDB.
                        btnClose_Click(null, null);

                        // Reinit combobox and select new group.
                        InitDataBaseResources();
                        cbResourceGroup.SelectedIndex = cbResourceGroup.FindStringExact(newName);
                    }
                    else if (tb.Tag.ToString() == "rename")
                    {
                        ResourceGroup g = cbResourceGroup.SelectedItem as ResourceGroup;

                        string newName = txtNewResourceDB.Text.Trim();
                        if (newName == "" || newName == g.Name) return;

                        using (StixDB db = new StixDB())
                        {
                            DataTable dt = db.ExecuteQuery("select * from RESOURCEGROUPS where name=`" + newName + "`");
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show(Utils.getString("ResourcesDlg.groupexists"));
                                txtNewResourceDB.Text = g.Name + " (2)";
                                return;
                            }

                            db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + newName + "` where id=" + g.GroupID + "");
                        }

                        // Close group mode of the txtNewResourceDB.
                        btnClose_Click(null, null);

                        // Reinit combobox and select renamed group.
                        InitDataBaseResources();
                        cbResourceGroup.SelectedIndex = cbResourceGroup.FindStringExact(newName);
                    }
                }

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                
            }
        }

        /// <summary>
        /// Add to 
        /// </summary>
        private void ResourceEnter_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            string _resources = tb.Text.Trim();
            if (_resources == "") return;

            string[] resources = _resources.Split(',').Select(x => x.Trim()).ToArray();

            if (tb == txtNewResourceDB) // add new resource(s) to the database
            {
                ResourceGroup group = cbResourceGroup.SelectedItem as ResourceGroup;

                foreach (string res in resources)
                    NewResource(res, group.GroupID);

                tb.Text = "";
            }
            else // Add to the topic and to the Map Index
            {
                // Add to topics
                if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
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

                // Add to Map Index
                MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                foreach (string res in resources)
                {
                    bool found = false;
                    foreach (MapMarker mm in mg)
                        if (mm.Label == res) found = true;

                    if (!found)
                        mg.AddResourceMarker(res);
                }

                tb.Text = "";

                // Add resource to the list
                foreach (string res in resources)
                {
                    bool found = false;
                    foreach (ListViewItem resource in ListMapResources.Items)
                    {
                        if (resource.Text == res)
                        {
                            found = true; break;
                        }
                    }
                    if (!found)
                        ListMapResources.Items.Add(res);
                }
            }
        }

        private void NewResource(string name, int groupID)
        {
            ListViewItem lvi; ResourceItem item;

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                    "where name=`" + name + "` and groupID=" + groupID + "");

                if (dt.Rows.Count > 0) return;

                db.AddResource(name, "", groupID);

                item = new ResourceItem(name, "", groupID);

                lvi = ListDBResources.Items.Add(item.Name);
                lvi.Tag = item;
            }
        }

        #region Manage Groups

        /// <summary>
        /// Toggle txtNewResourceDB to resource mode.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            txtNewResourceDB.BackColor = Color.White;
            txtNewResourceDB.Text = Utils.getString("ResourcesDlg.dummytext");
            toolTip1.SetToolTip(txtNewResourceDB, Utils.getString("ResourcesDlg.addnewresource.db.tooltip"));
            txtNewResourceDB.ForeColor = SystemColors.GrayText;
            txtNewResourceDB.Font = new Font(txtNewResourceDB.Font, FontStyle.Regular);
            btnClose.Visible = false;
            txtNewResourceDB.Tag = "resource";
        }

        #endregion

        public Dictionary<string, string> MapResources = new Dictionary<string, string>();
        List<ResourceItem> CopiedResources = new List<ResourceItem>();

        ListViewItem selectedItem = null;
        ListView selectedList = null;
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

        private void txtResources_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Tag.ToString() == "resource")
            {
                if (tb.Text.Trim() == "")
                {
                    tb.Text = Utils.getString("ResourcesDlg.dummytext");
                    tb.ForeColor = SystemColors.GrayText;
                }
            }
            else
            {
                btnClose_Click(null, null);
            }
        }

        private void txtResources_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Tag.ToString() != "rename" && tb.ForeColor == SystemColors.GrayText)
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

                using (StixDB db = new StixDB())
                {
                    DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                        "where name=`" + newName + "`");

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"));
                        e.CancelEdit = true;
                        return; // Resource already exists
                    }

                    res.Name = newName; lv.SelectedItems[0].Tag = res;

                    db.ExecuteNonQuery("update RESOURCES set name=`" + newName +
                        "` where name=`" + oldName + "`");
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

        private void cbDataBaseResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListDBResources.Items.Clear();

            ResourceGroup group = cbResourceGroup.SelectedItem as ResourceGroup;

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                    "where groupID=" + group.GroupID + " order by name");

                if (group.GroupID == 0)
                    dt = db.ExecuteQuery("select * from RESOURCES order by name");

                foreach (DataRow dr in dt.Rows)
                {
                    ResourceItem res = new ResourceItem(
                        dr["name"].ToString(), dr["color"].ToString(), Convert.ToInt32(dr["groupID"]));

                    if (ResourceExists(res)) continue;

                    var lvi = ListDBResources.Items.Add(res.Name); lvi.Tag = res;
                    if (res.aColor != "")
                    {
                        Color c = ColorTranslator.FromHtml(res.aColor);
                        lvi.BackColor = c;
                        int cc = (int)Math.Sqrt(c.R * c.R * .299 + c.G * c.G * .587 + c.B * c.B * .114);
                        if (cc > 130) lvi.ForeColor = SystemColors.WindowText;
                        else lvi.ForeColor = SystemColors.Window;
                    }
                }
            }

            if (group.GroupID == 0 && txtNewResourceDB.Tag.ToString() == "resource")
                txtNewResourceDB.Enabled = false;
            else
                txtNewResourceDB.Enabled = true;
        }

        bool ResourceExists(ResourceItem res)
        {
            foreach (ListViewItem item in ListDBResources.Items)
            {
                ResourceItem ri = item.Tag as ResourceItem;
                if (ri.Name == res.Name && ri.aColor == res.aColor)
                    return true;
            }
            return false;
        }

        private void btnRemoveResources_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null ||
                    MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
                return;

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                t.Task.Resources = "";
            }
        }

        private void cbResourceGroup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedList = ListDBResources;
                ResourceGroup g = cbResourceGroup.SelectedItem as ResourceGroup;

                foreach (ToolStripItem item in cmsMore.Items)
                    item.Visible = false;

                if (g.GroupID == 0)
                {
                    mm_new.Visible = true;
                }
                else
                {
                    foreach (ToolStripItem item in cmsMore.Items)
                        item.Visible = true;
                    mm_pastefromclipboard.ToolTipText = Utils.getString("ResourcesDlg.PasteToGroup.tooltip");
                }

                cmsMore.Show(Cursor.Position);
                return;
            }
        }

        private void lblCurrentMap_MouseClick(object sender, MouseEventArgs e)
        {
            selectedList = ListMapResources;

            foreach (ToolStripItem item in cmsMore.Items)
                item.Visible = false;

            mm_paste.Visible = true;
            mm_pastefromclipboard.Visible = true;
            mm_pastefromclipboard.ToolTipText = Utils.getString("ResourcesDlg.PasteToMap.tooltip");

            cmsMore.Show(Cursor.Position);
            return;
        }

        private void p11_Click(object sender, EventArgs e)
        {

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

    public class ResourceGroup
    {
        public ResourceGroup(int id, string name)
        {
            Name = name;
            GroupID = id;
        }
        public string Name = "";
        public int GroupID = 0;

        public override string ToString()
        {
            return Name;
        }
    }
}
