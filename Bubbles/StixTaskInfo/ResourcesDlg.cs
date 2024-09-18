using Mindjet.MindManager.Interop;
using NAudio.Wave;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Clipboard = System.Windows.Forms.Clipboard;
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

            lblTitle.Text = Utils.getString("taskinfo.Resources");
            txtNewResource.Text = Utils.getString("ResourcesDlg.dummytext");
            toolTip1.SetToolTip(txtNewResource, Utils.getString("ResourcesDlg.addnewresource.curmap.tooltip"));
            txtNewResource.ForeColor = SystemColors.GrayText;
            txtNewGroup.Text = Utils.getString("ResourcesDlg.newgroup");
            toolTip1.SetToolTip(txtNewGroup, Utils.getString("ResourcesDlg.addnewresource.db.tooltip"));
            txtNewGroup.ForeColor = SystemColors.GrayText;

            toolTip1.SetToolTip(pHelp, Utils.getString("button.help"));
            toolTip1.SetToolTip(pClose, Utils.getString("button.close"));

            tabPage1.Text = Utils.getString("ResourcesDlg.lblCurrentMap");
            tabPage2.Text = Utils.getString("ResourcesDlg.database");

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            thisHeight = this.Height;
            panel1.BackColor = Utils.header;

            // Context menu
            cmsResource.ItemClicked += ContextMenu_ItemClicked;
            cmsGroup.ItemClicked += ContextMenu_ItemClicked;

            r_addtotopic.Text = Utils.getString("ResourcesDlg.menuAddToTopic");
            r_remove.Text = Utils.getString("ResourcesDlg.menuRemoveFromTopic");
            r_addtomap.Text = Utils.getString("ResourcesDlg.menuAddToMap");
            r_addtomap.ToolTipText = Utils.getString("ResourcesDlg.menuAddToMap.tooltip");
            r_rename.Text = Utils.getString("button.rename");
            r_delete.Text = Utils.getString("button.delete");
            r_color.Text = Utils.getString("stixformat.contextmenu.color");
            r_copy.Text = Utils.getString("button.copy");
            r_copyall.Text = Utils.getString("ResourcesDlg.CopyAll");
            rg_copyall.Text = Utils.getString("ResourcesDlg.CopyAll");
            r_cut.Text = Utils.getString("button.cut");

            g_newresource.Text = Utils.getString("ResourcesDlg.NewResource");
            g_rename.Text = Utils.getString("ResourcesDlg.RenameGroup");
            g_delete.Text = Utils.getString("ResourcesDlg.DeleteGroup");
            g_addtomap.Text = Utils.getString("ResourcesDlg.menuAddToMap");
            g_addtomap.ToolTipText = Utils.getString("ResourcesDlg.menuAddToMap.group.tooltip");
            g_paste.Text = Utils.getString("ResourcesDlg.Paste");
            g_pastefromclipboard.Text = Utils.getString("ResourcesDlg.PasteClipboard");

            this.Paint += this_Paint; // paint form border
            this.MinimumSize = new Size(this.Width, this.Height / 2);
            this.MaximumSize = new Size(this.Width, Screen.AllScreens.Max(s => s.Bounds.Height));

            treeViewCM.Sorted = true;
            treeViewDB.Sorted = true;
            InitCurrentMapResources();
            InitDataBaseResources();

            StixUtils.ActivateMindManager();

            this.ResizeEnd += ResourcesDlg_ResizeEnd;
        }

        private void ResourcesDlg_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh(); // to reset splitter
        }

        public void InitCurrentMapResources()
        {
            treeViewCM.Nodes.Clear();
            MapResources.Clear();

            if (MMUtils.ActiveDocument == null) return;

            MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
            foreach (MapMarker mm in mg)
            {
                string color = "#" + mm.Color.Value.ToString("X");
                if (color == "#0") color = "";

                ResourceItem item = new ResourceItem(mm.Label, color, 0);
                var res = treeViewCM.Nodes.Add(mm.Label); res.Tag = item;

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
            treeViewDB.Nodes.Clear();

            using (StixDB db = new StixDB("Resources"))
            {
                treeViewDB.Nodes.Clear();
                TreeNode root = new TreeNode(Utils.getString("ResourcesDlg.allresources"));
                root.Tag = 0; treeViewDB.Nodes.Add(root);
                root.NodeFont = new Font(treeViewDB.Font, FontStyle.Bold);

                List<string> resources = new List<string>();
                DataTable dt = db.ExecuteQuery("select * from RESOURCES order by name");
                foreach (DataRow res in dt.Rows)
                {
                    string resource = res["name"].ToString();
                    if (resources.Contains(resource)) continue;

                    string color = res["color"].ToString();
                    ResourceItem item = new ResourceItem(resource, color, Convert.ToInt32(res["groupID"]));
                    TreeNode _node = root.Nodes.Add(resource);
                    resources.Add(resource);
                    _node.Tag = item;
                }

                dt = db.ExecuteQuery("select * from RESOURCEGROUPS order by name");
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode node = treeViewDB.Nodes.Add(dr["name"].ToString());
                    node.Tag = Convert.ToInt32(dr["id"]);
                    node.NodeFont = new Font(treeViewDB.Font, FontStyle.Bold);

                    dt = db.ExecuteQuery("select * from RESOURCES where groupID=" + (int)node.Tag + " order by name");
                    foreach (DataRow res in dt.Rows)
                    {
                        string color = res["color"].ToString();
                        ResourceItem item = new ResourceItem(res["name"].ToString(), color, Convert.ToInt32(res["groupID"]));
                        TreeNode _node = node.Nodes.Add(res["name"].ToString());
                        _node.Tag = item;

                        if (color != "")
                        {
                            _node.BackColor = ColorTranslator.FromHtml(color);
                            Color c = _node.BackColor;
                            int cc = (int)Math.Sqrt(c.R * c.R * .299 + c.G * c.G * .587 + c.B * c.B * .114);
                            if (cc > 130) _node.ForeColor = SystemColors.WindowText;
                            else _node.ForeColor = SystemColors.Window;
                        }
                    }
                }

                treeViewDB.SelectedNode = root;
            }
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            TreeView tv = treeViewDB;
            if (currentMap) tv = treeViewCM;
            GetSelectedNodes(tv);

            TreeNode group = null; int groupID = 0;
            if (!currentMap)
            {
                group = treeViewDB.SelectedNode;
                if (group == null) group = SelectedNodes[0];

                if (group != null)
                {
                    if (group.Parent != null) group = group.Parent; // resource selected
                    groupID = Convert.ToInt32(group.Tag);
                }
            }

            // Add new resource to a group.
            if (e.ClickedItem == g_newresource)
            {
                if (group == null) return;
                ResourceItem item = new ResourceItem(Utils.getString("ResourcesDlg.NewResource"), "", groupID);
                TreeNode node = group.Nodes.Add(item.Name);
                treeViewDB.SelectedNode.Expand();
                treeViewDB.SelectedNode.Checked = false;
                treeViewDB.SelectedNode = node;
                tv.LabelEdit = true;
                node.BeginEdit();
            }    
            // Rename group.
            else if (e.ClickedItem == g_rename)
            {
                tv.LabelEdit = true;
                tv.SelectedNode.BeginEdit();
            }
            // Delete group.
            else if (e.ClickedItem == g_delete)
            {
                if (MessageBox.Show(String.Format(Utils.getString("ResourcesDlg.delete.group"), group.Text),
                    "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    using (StixDB db = new StixDB("Resources"))
                    {
                        db.ExecuteNonQuery("delete from RESOURCES where groupID=" + groupID + "");
                        db.ExecuteNonQuery("delete from RESOURCEGROUPS where id=" + groupID + "");
                    }

                    treeViewDB.SelectedNode.Remove();
                    treeViewDB.SelectedNode = treeViewDB.Nodes[0];
                }
            }
            // Add all group's resources to Map Index
            else if (e.ClickedItem == g_addtomap)
            {
                Dictionary<string, string> _resources = new Dictionary<string, string>();
                foreach (TreeNode node in group.Nodes)
                {
                    ResourceItem res = node.Tag as ResourceItem;
                    _resources.Add(res.Name, res.aColor);
                }
                AddResourcesToMap(_resources);
            }
            // Paste copied resources to the map or to the selected group.
            else if (e.ClickedItem == g_paste)
            {
                PasteResources(tv);
            }
            // Paste from clipboard to the map or to the selected group.
            else if (e.ClickedItem == g_pastefromclipboard)
            {
                PasteResources(tv, true); return;
            }
            else // Resource context menu item clicked
            {
                Dictionary<string, string> resources = new Dictionary<string, string>();
                string[] listResources = new string[SelectedNodes.Count];

                for (int i = 0; i < SelectedNodes.Count; i++)
                    listResources[i] = SelectedNodes[i].Text;

                foreach (var item in SelectedNodes)
                {
                    if (item.Tag != null)
                    {
                        ResourceItem res = item.Tag as ResourceItem;
                        resources.Add(res.Name, res.aColor);
                    }
                }

                // Add selected resources to topic
                if (e.ClickedItem == r_addtotopic)
                {
                    AddResourcesToMap(resources);
                    SetResources(listResources, true);
                }
                // Remove selected resources from topic
                else if (e.ClickedItem == r_remove)
                {
                    if (Utils.ActiveDocumentOrSelectionNull()) return;

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
                else if (e.ClickedItem == r_addtomap)
                {
                    AddResourcesToMap(resources);
                }
                // Rename selected resource
                else if (e.ClickedItem == r_rename)
                {
                    if (tv.SelectedNode == null) return;
                    tv.LabelEdit = true;
                    tv.SelectedNode.BeginEdit();
                }
                // Delete selected resources from map or database
                if (e.ClickedItem == r_delete)
                {
                    DeleteResources(listResources, currentMap, groupID);
                }
                // Set/change resource color
                if (e.ClickedItem == r_color)
                {
                    colorDialog1.FullOpen = true;
                    if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                        return;

                    Color c = colorDialog1.Color;
                    string colorHEX = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
                    if (colorHEX == "#ffffffff") colorHEX = ""; // white, no color

                    using (StixDB db = new StixDB("Resources"))
                    {
                        foreach (TreeNode item in SelectedNodes)
                        {
                            ResourceItem ri = item.Tag as ResourceItem;

                            if (item.BackColor == c) continue;

                            if (colorHEX == "")
                            {
                                item.BackColor = SystemColors.Window;
                                item.ForeColor = SystemColors.WindowText;
                            }
                            else
                            {
                                item.BackColor = ColorTranslator.FromHtml(colorHEX);
                                int cc = (int)Math.Sqrt(c.R * c.R * .299 + c.G * c.G * .587 + c.B * c.B * .114);
                                if (cc > 130) item.ForeColor = SystemColors.WindowText;
                                else item.ForeColor = SystemColors.Window;
                            }

                            ri.aColor = colorHEX; item.Tag = ri;

                            // Change resource color in the Map Index
                            if (currentMap && MMUtils.ActiveDocument != null)
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
                            else if (!currentMap)
                            {
                                db.ExecuteNonQuery("update RESOURCES set color=`" + colorHEX +
                                    "` where name=`" + ri.Name + "` and groupID=" + ri.GroupID + "");
                            }
                        }
                    }
                }
                // Copy/Cut selected resource
                else if (e.ClickedItem == r_copy || e.ClickedItem == r_cut)
                {
                    CopyCutResources(tv, e.ClickedItem == r_cut);
                }
                else if (e.ClickedItem == r_copyall || e.ClickedItem == rg_copyall)
                {
                    CopiedResources.Clear();
                    string _resources = "";

                    if (currentMap)
                    {
                        foreach (TreeNode node in treeViewCM.Nodes)
                        {
                            ResourceItem res = node.Tag as ResourceItem;
                            CopiedResources.Add(res);
                            _resources += res.Name + "\r\n";
                        }
                    }
                    else
                    {
                        foreach (TreeNode node in treeViewDB.SelectedNode.Nodes)
                        {
                            ResourceItem res = node.Tag as ResourceItem;
                            CopiedResources.Add(res);
                            _resources += res.Name + "\r\n";
                        }
                    }

                    if (_resources == "") return;
                    _resources = _resources.TrimEnd('\r', '\n');
                    Clipboard.SetText(_resources);

                    string message = Utils.getString("ResourcesDlg.copy.success");
                    MMUtils.MindManager.NotificationsDialog.ShowNotification("", message, MmNotificationsDialogOptions.mmNotificationsDialogOptionsAutoClose);
                }
            }
        }

        void AddResourcesToMap(Dictionary<string, string> resources)
        {
            if (MMUtils.ActiveDocument == null) return;
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

        void CopyCutResources(TreeView tv, bool cut)
        {
            GetSelectedNodes(tv);
            if (SelectedNodes.Count == 0) return;
            CopiedResources.Clear();
            string resources = "";

            foreach (TreeNode node in SelectedNodes)
            {
                ResourceItem res = node.Tag as ResourceItem;
                CopiedResources.Add(res);
                resources += res.Name + "\r\n";

                if (cut)
                {
                    node.Remove();
                    // todo delete from database!
                }
            }

            resources = resources.TrimEnd('\r', '\n');
            Clipboard.SetText(resources);

            string message = Utils.getString("ResourcesDlg.copy.success");
            MMUtils.MindManager.NotificationsDialog.ShowNotification("", message, MmNotificationsDialogOptions.mmNotificationsDialogOptionsAutoClose);
        }

        void DeleteResources(string[] listResources, bool currentMap, int groupID = 0)
        {
            TreeView tv = currentMap ? treeViewCM : treeViewDB;
            GetSelectedNodes(tv);

            // Delete resources from Map Index
            if (currentMap && MMUtils.ActiveDocument != null)
            {
                if (MessageBox.Show(Utils.getString("ResourcesDlg.delete.map"), "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;

                MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                foreach (MapMarker mm in mg)
                {
                    if (listResources.Contains(mm.Label)) mm.Delete();
                }
                // Remove resources from list.
                foreach (TreeNode node in SelectedNodes)
                    node.Remove();
                SelectedNodes.Clear();
            }
            // Delete resources from database
            else if (!currentMap)
            {
                DialogResult rc = MessageBox.Show(Utils.getString("ResourcesDlg.delete.database"), "",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (rc == DialogResult.Cancel) return;

                bool fromall = false;
                using (StixDB db = new StixDB("Resources"))
                {
                    foreach (TreeNode res in SelectedNodes)
                    {
                        if (rc == DialogResult.Yes) // delete from all groups
                        {
                            db.ExecuteNonQuery("delete from RESOURCES " +
                                "where name=`" + res.Text + "`");
                            fromall = true;
                        }
                        else // delete from selected group
                        {
                            if (res.Parent != null)
                            {
                                try
                                {
                                    groupID = Convert.ToInt32(res.Parent.Tag);
                                    db.ExecuteNonQuery("delete from RESOURCES " +
                                        "where name=`" + res.Text + "` and groupID=" + groupID + "");
                                } catch { }
                            }
                        }
                    }
                }

                if (fromall) 
                    InitDataBaseResources();
                else
                    foreach (TreeNode node in SelectedNodes) node.Remove();

                SelectedNodes.Clear();
            }
        }

        void PasteResources(TreeView tv, bool clipboard = false)
        {
            string show = "", group = ""; int groupID = 0;
            List<ResourceItem> resources = new List<ResourceItem>();

            if (clipboard)
            {
                string text = Clipboard.GetText().Trim();
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
            if (tv == treeViewDB)
            {
                group = treeViewDB.SelectedNode.Text;
                groupID = Convert.ToInt32(treeViewDB.SelectedNode.Tag);

                if (groupID == 0)  {
                    MessageBox.Show(Utils.getString("pasteresources.group.error")); return;
                }

                where = String.Format(Utils.getString("pasteresources.preview.group"), group);
            }

            if (MessageBox.Show(String.Format(Utils.getString("pasteresources.preview"),where, show),
                Utils.getString("pasteresources.clipboard.title"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (StixDB db = new StixDB("Resources"))
                {
                    foreach (var res in resources)
                    {
                        if (tv == treeViewCM)
                        {
                            if (MMUtils.ActiveDocument == null) return;

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
                            foreach (TreeNode item in treeViewDB.SelectedNode.Nodes)
                                if (item.Text == res.Name) found = true;
                            if (found) continue;

                            TreeNode lvi = treeViewDB.SelectedNode.Nodes.Add(res.Name);
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

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (unselect)
            {
                (sender as TreeView).SelectedNode = null;
                unselect = false;
            }
            else
                e.Node.Checked = true;
        }

        bool unselect = false;
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView tv = sender as TreeView;
            currentMap = tv == treeViewCM;

            tv.LabelEdit = false;

            if (e.Button == MouseButtons.Left)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control) // multiple nodes selection
                {
                    // Unselect group nodes if there are selected. Multiple selection is for the resources only, not for groups.
                    if (!currentMap)
                        foreach (TreeNode node in tv.Nodes)
                            node.Checked = false;

                    if (e.Node.Parent == null && !currentMap) {
                        unselect = true; return; } // group clicked

                    if (e.Node.Checked)
                        e.Node.Checked = false;
                    else
                        e.Node.Checked = true;

                    unselect = true;
                    return;
                }
                else // A normal click. Unselected all nodes and select clicked one.
                {
                    UnselectNodes(tv);
                    e.Node.Checked = true;

                    if (e.Node.Parent == null && !currentMap) return; // group clicked
                }

                if (currentMap && Utils.ActiveDocumentOrSelectionNull()) return;

                GetSelectedNodes(tv);
                Dictionary<string, string> resources = new Dictionary<string, string>();

                foreach (TreeNode item in SelectedNodes)
                {
                    ResourceItem res = item.Tag as ResourceItem;
                    resources.Add(res.Name, res.aColor);
                }
                AddResourcesToMap(resources);

                string[] listResources = new string[] { e.Node.Text };
                SetResources(listResources);
            }
            else if (e.Button == MouseButtons.Right) // ContextMenu
            {
                GetSelectedNodes(tv);

                if (!SelectedNodes.Contains(e.Node)) // Clicking on the non-selected node.
                {
                    UnselectNodes(tv);
                    tv.SelectedNode = e.Node;
                    e.Node.Checked = true;
                    SelectedNodes.Add(e.Node);
                }

                if (!currentMap && e.Node.Parent == null) // Group selected. Show group context menu.
                {
                    foreach (ToolStripItem item in cmsGroup.Items)
                        item.Visible = true;

                    bool commongroup = Convert.ToInt32(e.Node.Tag) == 0;

                    if (CopiedResources.Count == 0 || commongroup) g_paste.Visible = false;
                    if (!Clipboard.ContainsText() || commongroup) g_pastefromclipboard.Visible = false;
                    if (commongroup)
                    {
                        g_delete.Visible = false; g_newresource.Visible = false; g_rename.Visible = false;
                    }

                    g_pastefromclipboard.ToolTipText = Utils.getString("ResourcesDlg.PasteToGroup.tooltip");
                    cmsGroup.Show(Cursor.Position);
                }
                else // Resource selected. Show resource context menu.
                {
                    if (MMUtils.ActiveDocument == null) return;

                    foreach (ToolStripItem item in cmsResource.Items)
                        item.Visible = true;

                    if (SelectedNodes.Count > 1) r_rename.Visible = false;

                    if (MMUtils.ActiveDocument.Selection.PrimaryTopic == null) // There are not selected topics.
                    {
                        r_addtotopic.Visible = false;
                        r_remove.Visible = false;
                    }

                    if (currentMap)
                    {
                        r_addtomap.Visible = false;
                        r_cut.Visible = false;
                    }
                    else // resources in the groups
                    {
                        r_copyall.Visible = false;

                        if (Convert.ToInt32(e.Node.Parent.Tag) == 0) // All Resources group
                        {
                            r_delete.Visible = false;
                            r_color.Visible = false;
                            r_cut.Visible = false;
                        }
                    }

                    cmsResource.Show(Cursor.Position);
                }
            }
        }

        void UnselectNodes(TreeView tv)
        {
            foreach (TreeNode node in tv.Nodes)
            {
                node.Checked = false;

                foreach (TreeNode _node in node.Nodes)
                    _node.Checked = false;
            }
            SelectedNodes.Clear();
            tv.SelectedNode = null;
        }

        /// <summary>
        /// Detect click on the treeview blank area. 
        /// Show the paste items from the context menu for a current map.
        /// </summary>
        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeView tv = sender as TreeView;
            currentMap = tv == treeViewCM;

            if (tv.HitTest(e.Location).Node == null) // click on the empty space
            {
                UnselectNodes(tv);

                if (e.Button == MouseButtons.Right && currentMap)
                {
                    foreach (ToolStripItem item in cmsGroup.Items)
                        item.Visible = false;

                    rg_copyall.Visible = true;
                    g_paste.Visible = true;
                    g_pastefromclipboard.Visible = true;

                    cmsGroup.Show(Cursor.Position);
                }
                unselect = true;
            }
        }

        private void treeView_KeyUp(object sender, KeyEventArgs e)
        {
            TreeView tv = sender as TreeView;
            currentMap = tv == treeViewCM;
            GetSelectedNodes(tv);
            string[] listResources = new string[SelectedNodes.Count];

            for (int i = 0; i < SelectedNodes.Count; i++)
                listResources[i] = SelectedNodes[i].Text;

            // Delete selected resources.
            if (e.KeyCode == Keys.Delete)
            {
                DeleteResources(listResources, currentMap);
            }
            // Copy/Cut selected resources.
            else if (e.KeyCode == Keys.C || e.KeyCode == Keys.X)
            {
                if (ModifierKeys == Keys.Control)
                    CopyCutResources(tv, e.KeyCode == Keys.X);
            }
            // Set selected resources. Edit mode?
            else if (e.KeyCode == Keys.Enter)
            {
                foreach (TreeNode item in SelectedNodes)
                {

                }
            }
            // Select all resources
            else if (e.KeyCode == Keys.A)
            {
                if (ModifierKeys == Keys.Control && tv == treeViewCM)
                {
                    foreach (TreeNode node in tv.Nodes)
                        node.Checked = true;
                }
            }

            e.Handled = true; // to avoid the "ding" sound
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Add selected resources to topic and to map
        /// </summary>
        /// <param name="listResources">Resources to add. Name and color</param>
        /// <param name="addforced">if command is from context menu</param>
        private void SetResources(string[] listResources, bool addforced = false)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

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
        private void txtNewResource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Add new Resource(s) to the topics and map.
                AddResourceToMap();

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void txtNewGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = txtNewGroup.Text.Trim();
                if (newName == "") return;

                using (StixDB db = new StixDB("Resources"))
                {
                    DataTable dt = db.ExecuteQuery("select from RESOURCEGROUPS where name=`" + newName + "`");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("ResourcesDlg.groupexists")); return;
                    }

                    db.AddResourceGroup(newName);
                    // Get created group id
                    int groupID = 1;
                    dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);
                    TreeNode node = treeViewDB.Nodes.Add(newName);
                    node.Tag = groupID;
                    node.NodeFont = new Font(treeViewDB.Font, FontStyle.Bold);
                }
                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Add resource to topic
        /// </summary>
        private void AddResourceToMap()
        {
            string _resources = txtNewResource.Text.Trim();
            if (_resources == "") return;

            string[] resources = _resources.Split(',').Select(x => x.Trim()).ToArray();

            if (MMUtils.ActiveDocument == null) return;

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

            txtNewResource.Text = "";

            // Add resource to the list
            foreach (string res in resources)
            {
                bool found = false;
                foreach (TreeNode resource in treeViewCM.Nodes)
                {
                    if (resource.Text == res)
                    {
                        found = true; break;
                    }
                }
                if (!found)
                {
                    ResourceItem _res = new ResourceItem(res, "", 0);
                    treeViewCM.Nodes.Add(res).Tag = _res;
                }
            }
        }
        #region Manage Groups

        #endregion

        void GetSelectedNodes(TreeView tv)
        {
            SelectedNodes.Clear();

            foreach (TreeNode node in tv.Nodes)
            {
                if (tv == treeViewCM && (node.IsSelected || node.Checked))
                    SelectedNodes.Add(node);

                if (tv == treeViewDB)
                {
                    foreach (TreeNode _node in node.Nodes)
                        if (_node.IsSelected || _node.Checked)
                            SelectedNodes.Add(_node);
                }
            }
        }

        public Dictionary<string, string> MapResources = new Dictionary<string, string>();
        List<ResourceItem> CopiedResources = new List<ResourceItem>();
        List<TreeNode> SelectedNodes = new List<TreeNode>();

        TreeNode selectedItem = null;
        bool currentMap = true;
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

        private void txtAddNew_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.Trim() == "")
                tb.ForeColor = SystemColors.GrayText;

            if (tb == txtNewResource)
                tb.Text = Utils.getString("ResourcesDlg.dummytext");
            else if (tb == txtNewGroup)
                tb.Text = Utils.getString("ResourcesDlg.newgroup");
        }

        private void txtAddNew_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.ForeColor == SystemColors.GrayText)
            {
                tb.Text = "";
                tb.ForeColor = SystemColors.WindowText;
            }
        }

        private void treeView_Leave(object sender, EventArgs e)
        {
            TreeView tv = sender as TreeView;
            tv.LabelEdit = false;
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null) 
            {
                e.CancelEdit = true; 
                if (e.Node.Tag == null) e.Node.Remove(); 
                return; 
            }

            TreeView tv = sender as TreeView;
            currentMap = tv == treeViewCM;

            if (MMUtils.ActiveDocument == null && currentMap) return;

            string oldName = e.Node.Text.Trim();
            string newName = e.Label.Trim();
            bool rename = false;

            if (newName == "") {
                e.CancelEdit = true; e.Node.Remove(); return; }

            // Rename resource in the map
            if (currentMap)
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
            // Rename resource or group in the database 
            else if (!currentMap)
            {
                using (StixDB db = new StixDB("Resources"))
                {
                    if (e.Node.Parent == null) // rename group
                    {
                        DataTable dt = db.ExecuteQuery("select * from RESOURCEGROUPS " +
                            "where name=`" + newName + "`");

                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show(Utils.getString("ResourcesDlg.groupexists"));
                            e.CancelEdit = true;
                            return; // Group already exists
                        }

                        db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + newName +
                            "` where name=`" + oldName + "`");
                    }
                    else // add new or rename resource
                    {
                        ResourceItem item;
                        if (e.Node.Tag == null) // add new resource
                        {
                            int groupID = Convert.ToInt32(e.Node.Parent.Tag);
                            DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                            "where name=`" + newName + "` and groupID=" + groupID + "");

                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"));
                                e.CancelEdit = true;
                                return; // Resource in this group already exists
                            }

                            // Add resource to All Resources node
                            dt = db.ExecuteQuery("select * from RESOURCES " + "where name=`" + newName + "`");
                            item = new ResourceItem(newName, "", groupID);
                            if (dt.Rows.Count == 0)
                                treeViewDB.Nodes[0].Nodes.Add(newName).Tag = item;

                            db.AddResource(newName, "", groupID);
                            e.Node.Tag = item;
                        }
                        else // rename resource
                        {
                            rename = true;
                            item = e.Node.Tag as ResourceItem;

                            DataTable dt = db.ExecuteQuery("select * from RESOURCES " +
                                "where name=`" + newName + "`");

                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show(Utils.getString("ResourcesDlg.resourceexists"));
                                e.CancelEdit = true;
                                return; // Resource already exists
                            }

                            item.Name = newName; e.Node.Tag = item;
                            db.ExecuteNonQuery("update RESOURCES set name=`" + newName +
                                "` where name=`" + oldName + "`");
                        }
                        e.Node.Text = newName;
                    }
                }
            }

            if (rename)
                InitDataBaseResources();
            else
                tv.Sort();

            treeViewDB.SelectedNode = e.Node;
            tv.LabelEdit = false;
        }

        void SortNode(TreeNode node)
        {
            List<String> sorted = new List<string>();
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (TreeNode child in node.Nodes)
            {
                sorted.Add(child.Text);
                nodes.Add(child);
            }

            sorted.Sort();

            int i = 0;
            foreach (string s in sorted)
            {
                foreach (TreeNode _node in nodes)
                {
                    if (_node.Text == s)
                    {
                        _node.Remove();
                        node.Nodes.Insert(i++, _node);
                    }
                }
            }
        }

        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (!e.Node.IsVisible) return;

            Font nodeFont = e.Node.NodeFont;
            if (nodeFont == null) nodeFont = e.Node.TreeView.Font;
            
            Color backColor = e.Node.BackColor;
            if (backColor == null || backColor == SystemColors.HotTrack) backColor = e.Node.TreeView.BackColor;
            
            Color foreColor = e.Node.ForeColor;
            if (foreColor == null) foreColor = e.Node.TreeView.ForeColor;

            if (e.Node.Checked)
            {
                using (SolidBrush br = new SolidBrush(SystemColors.HotTrack))
                    e.Graphics.FillRectangle(br, e.Node.Bounds);

                //using (SolidBrush br = new SolidBrush(SystemColors.Window))
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, nodeFont, e.Bounds, SystemColors.Window);
                    // e.Graphics.DrawString(e.Node.Text, e.Node.TreeView.Font, br, e.Bounds);
            }
            else
            {
                using (SolidBrush br = new SolidBrush(backColor))
                    e.Graphics.FillRectangle(br, e.Node.Bounds);

                //using (SolidBrush br = new SolidBrush(SystemColors.WindowText))
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, nodeFont, e.Bounds, foreColor);
                    //e.Graphics.DrawString(e.Node.Text, e.Node.TreeView.Font, br, e.Bounds);
            }
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
