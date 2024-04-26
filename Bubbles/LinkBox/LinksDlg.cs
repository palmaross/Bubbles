using PRAManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class LinksDlg : Form
    {
        public LinksDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "LinksDlg.htm");

            Text = Utils.getString("LinksDlg.title");
            lblOpenIn.Text = Utils.getString("LinksDlg.lblOpenIn");
            rbtnOmniBrowser.Text = Utils.getString("LinksDlg.rbtnOmniBrowser");
            rbtnExternalApp.Text = Utils.getString("LinksDlg.rbtnExternalApp");

            m_OpenLink.Text = Utils.getString("LinksDlg.btnOpen");
            m_OpenInOmniBrowser.Text = Utils.getString("LinksDlg.omnibrowser");
            m_NewLink.Text = Utils.getString("LinksDlg.addlink");
            m_Modify.Text = Utils.getString("button.modify");
            m_Delete.Text = Utils.getString("button.delete");

            g_AddLink.Text = Utils.getString("LinksDlg.addlink");
            g_AddGroup.Text = Utils.getString("LinksDlg.NewGroup");
            g_RenameGroup.Text = Utils.getString("LinksDlg.RenameGroup");
            g_DeleteGroup.Text = Utils.getString("LinksDlg.DeleteGroup");

            LinkTitle.HeaderText = Utils.getString("LinksDlg.LinkTitle");
            LinkGroup.HeaderText = Utils.getString("LinksDlg.LinkGroup");

            cmsLink.ItemClicked += ContextMenu_ItemClicked;
            cmsGroup.ItemClicked += ContextMenu_ItemClicked;

            LinkImage.Width = (int)(pSize.Width * 1.5); // link type icon
            //Processed.Width = (int)(pSize.Width * 1.5); // processed checkbox

            this.MinimumSize = new Size(this.Width / 2, this.Height / 2);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            //dataGridView1.Columns[0].CellTemplate = new MyDataGridViewImageCell();

            Utils.InitIcons();
            Init();
        }

        void Init()
        {
            PopulateGroups();
        }

        void PopulateGroups()
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(Utils.getString("LinksDlg.AllGroups"));
            root.Tag = 0; treeView1.Nodes.Add(root);
            db = new StixDB();

            DataTable dt = db.ExecuteQuery("select * from LINKGROUPS where parentID = 0 order by _order");

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node = new TreeNode(dr["name"].ToString());
                node.Tag = Convert.ToInt32(dr["id"]);
                treeView1.Nodes.Add(node);
                PopRec(node);
            }

            db.Dispose(); db = null;

            treeView1.SelectedNode = root;
            SelectedNodeChanged();
        }
        StixDB db = null;

        void PopRec(TreeNode node)
        {
            int parent = Convert.ToInt32(node.Tag); // Tag = link id

            DataTable dt = db.ExecuteQuery("select * from LINKGROUPS where parentID=" + parent +
                " order by _order");

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode _node = new TreeNode(dr["name"].ToString());
                _node.Tag = Convert.ToInt32(dr["id"]); node.Nodes.Add(_node);
                PopRec(_node);
            }
        }

        public void AddToTable(string title, string path, string groupName, int groupID)
        {
            string imageType = Utils.GetFileType(path);
            Image img = null;

            if (imageType == "exe")
            {
                try
                {
                    Icon appIcon = Icon.ExtractAssociatedIcon(path);
                    img = appIcon.ToBitmap();
                }
                catch { img = Utils.GetImage(imageType); }
            }
            else if (imageType == "http")
            {
                if (Utils.getRegistry("FaviconsLinksWindow", "1") == "1")
                    img = Utils.GetFavicon(path);
                else
                    img = Utils.GetImage(imageType);
            }
            else
                img = Utils.GetImage(imageType);

            img = new Bitmap(img, new Size(pSize.Width, pSize.Height));

            int rowId = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[rowId];

            row.Cells["LinkImage"].Value = img;
            row.Cells["LinkTitle"].Value = title;
            row.Cells["LinkGroup"].Value = groupName;
            row.Cells["LinkPath"].Value = path;
            row.Cells["GroupID"].Value = groupID;
            row.Cells["SortByImage"].Value = imageType;
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "g_AddGroup")
            {
                selectedNode = treeView1.SelectedNode;

                if (selectedNode == treeView1.Nodes[0])
                    m_editNode = treeView1.Nodes.Add("");
                else
                {
                    m_editNode = selectedNode.Nodes.Add("");
                    selectedNode.Expand();
                }

                m_editNode.Tag = -1;
                treeView1.SelectedNode = m_editNode;
                selectedNode = m_editNode;
                SelectedNodeChanged();
                txtLink.Text = "";

                txtEditNode.Location = new Point(m_editNode.Bounds.X, m_editNode.Bounds.Y);
                txtEditNode.Size = new Size(treeView1.Width - m_editNode.Bounds.X - pSize.Width, txtEditNode.Height);
                txtEditNode.Visible = true; txtEditNode.Focus();
                txtEditNode.Text = Utils.getString("LinksDlg.NewGroup");
                txtEditNode.SelectAll();
                m_editMode = false; // "new group" mode
            }
            else if (e.ClickedItem.Name == "g_RenameGroup")
            {
                m_editNode = treeView1.SelectedNode;

                txtEditNode.Location = new Point(m_editNode.Bounds.X, m_editNode.Bounds.Y);
                txtEditNode.Size = new Size(treeView1.Width - m_editNode.Bounds.X - pSize.Width, txtEditNode.Height);
                txtEditNode.Visible = true; txtEditNode.Focus();
                txtEditNode.Text = m_editNode.Text + " (1)";
                m_editMode = true; // "edit group" mode
            }
            else if (e.ClickedItem.Name == "g_DeleteGroup")
            {
                if (MessageBox.Show(Utils.getString("LinksDlg.deletegroup"), "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    int groupID = (int)treeView1.SelectedNode.Tag;
                    List<int> list = new List<int>();
                    GetGroupIds(treeView1.SelectedNode, ref list);

                    using (StixDB _db = new StixDB())
                    {
                        foreach (int id in list)
                            _db.ExecuteNonQuery("delete from LINKS where groupID=" + id + "");

                        foreach (int id in list)
                            _db.ExecuteNonQuery("delete from LINKGROUPS where id=" + id + "");
                    }

                    treeView1.SelectedNode.Remove();

                    if (StixButton.m_NewLink != null && !StixButton.m_NewLink.IsDisposed)
                        StixButton.m_NewLink.FillGroups();
                }
            }
            else if (e.ClickedItem.Name == "g_AddLink" || e.ClickedItem.Name == "m_NewLink" || e.ClickedItem.Name == "m_Modify")
            {
                if (StixButton.m_NewLink == null || StixButton.m_NewLink.IsDisposed)
                {
                    StixButton.m_NewLink = new NewLinkDlg();
                    StixButton.m_NewLink.LinksDialog = this;
                    StixButton.m_NewLink.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }

                StixButton.m_NewLink.from = "LinksDlg";
                StixButton.m_NewLink.newLink = true;

                int groupID = 1;
                if (e.ClickedItem.Name == "g_AddLink")
                    groupID = (int)selectedNode.Tag;
                else if (dataGridView1.Rows.Count > 0)
                    groupID = (int)dataGridView1.Rows[0].Cells["GroupID"].Value;

                StixButton.m_NewLink.SelectGroup(groupID);

                if (e.ClickedItem.Name != "g_AddLink")
                {
                    selectedRow = dataGridView1.SelectedRows[0];

                    if (e.ClickedItem.Name == "m_Modify")
                    {
                        StixButton.m_NewLink.newLink = false;
                        StixButton.m_NewLink.txtLink.Text = (string)selectedRow.Cells["LinkPath"].Value;
                        StixButton.m_NewLink.txtTitle.Text = (string)selectedRow.Cells["LinkTitle"].Value;
                    }
                }
            }
            else if (e.ClickedItem.Name == "m_OpenLink")
            {
                OpenLink(false);
            }
            else if (e.ClickedItem.Name == "m_OpenInOmniBrowser")
            {
                OpenLink(true);
            }
            else if (e.ClickedItem.Name == "m_Delete")
            {
                selectedRows = dataGridView1.SelectedRows;

                if (MessageBox.Show(Utils.getString("LinksDlg.deletelinks"), "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    using (StixDB _db = new StixDB())
                    {
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            int groupID = (int)row.Cells["GroupID"].Value;
                            string path = (string)row.Cells["LinkPath"].Value;

                            _db.ExecuteNonQuery("delete from LINKS " +
                                "where path=`" + path + "` and groupID=" + groupID + "");

                            dataGridView1.Rows.Remove(row);
                        }
                    }
                }
            }
        }

        void OpenLink(bool omniBrowser)
        {
            if (dataGridView1.Rows.Count <= 0 || dataGridView1.SelectedRows.Count <= 0)
                return;

            if (!omniBrowser)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(row.Cells["LinkPath"].Value.ToString());
                    }
                    catch { }
                }
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string path = row.Cells["LinkPath"].Value.ToString();
                string lpath = path.ToLower();

                if (!lpath.StartsWith("http") && !lpath.StartsWith("www") && !lpath.EndsWith(".pdf") &&
                    !lpath.EndsWith(".htm") && !lpath.EndsWith(".html"))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                    catch { }

                    continue;
                }

                // Open links in Omni Browser

                if (OmniBrowser == null || OmniBrowser.IsDisposed)
                {
                    OmniBrowser = new BrowserDlg(path);
                    OmniBrowser.txtAddressBar.Text = path;
                    OmniBrowser.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
                else
                {
                    OmniBrowser.txtAddressBar.Text = path;
                    OmniBrowser.Navigate(true);
                }
            }
        }

        List<string> LinksForOB = new List<string>();

        void GetGroupIds(TreeNode node, ref List<int> list)
        {
            list.Add((int)node.Tag);

            foreach (TreeNode _node in node.Nodes)
            {
                GetGroupIds(_node, ref list);
            }
        }

        private void txtEditNode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEditNode.Text.Trim() == "") return;

            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                AcceptAddEditNode();

                if (StixButton.m_NewLink != null && !StixButton.m_NewLink.IsDisposed)
                    StixButton.m_NewLink.FillGroups();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                txtEditNode.Visible = false;
                treeView1.Focus();

                if (!m_editMode)
                    m_editNode.Remove();
            }
        }

        private void AcceptAddEditNode()
        {
            string name = txtEditNode.Text.Trim();

            using (StixDB _db = new StixDB())
            {
                DataTable dt = _db.ExecuteQuery("select * from LINKGROUPS where name=`" + name + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("ResourcesDlg.groupexists"));
                    return;
                }
                else if (m_editMode) // edit group
                {
                    _db.ExecuteNonQuery("update LINKGROUPS set name=`" + name +
                        "` where name = `" + m_editNode.Text + "`");

                    txtEditNode.Visible = false;
                    m_editNode.Text = name;
                }
                else // add new group
                {
                    int parent = 0, order = 1;
                    if (m_editNode.Parent != null)
                    {
                        parent = (int)m_editNode.Parent.Tag;
                        order = m_editNode.Parent.Nodes.Count;
                    }

                    _db.AddLinkGroup(name, parent, order);

                    // Get created group id
                    int groupID = 1;
                    dt = _db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);
                    m_editNode.Tag = groupID;

                    txtEditNode.Visible = false;
                    m_editNode.Text = name;
                }
            }
        }

        /// <summary>
        /// Sort by the first column (map type)
        /// </summary>
        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 && e.ColumnIndex == 0)
            {
                sortby = "SortByImage";
                thenby = "LinkTitle";
                SortByType();
            }
        }

        private void SortByType()
        {
            if (Direction == ListSortDirection.Ascending)
            {
                Direction = ListSortDirection.Descending;
                dataGridView1.Sort(new MyComparer(SortOrder.Descending));
            }
            else
            {
                Direction = ListSortDirection.Ascending;
                dataGridView1.Sort(new MyComparer(SortOrder.Ascending));
            }
        }
        private ListSortDirection Direction = ListSortDirection.Descending;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                var item = dataGridView1.SelectedRows[0];

                if (item.Cells["LinkPath"].Value != null)
                    txtLink.Text = item.Cells["LinkPath"].Value.ToString();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (rbtnExternalApp.Checked)
                OpenLink(false);
            else if (rbtnOmniBrowser.Checked)
                OpenLink(true);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            // Get clicked row index
            int i = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (i == -1) return; // Headers row clicked

            if (e.Button == MouseButtons.Left)
            {
                //if (selectedState != null)
                //{
                //    dataGridView1.Rows[i].Selected = false;
                //    foreach (DataGridViewRow row in selectedState)
                //        row.Selected = true;

                //    selectedState = null;
                //}
            }
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView1.SelectedRows.Count >= 0)
                {
                    bool clickonselected = false;
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (row.Index == i)
                        {
                            clickonselected = true; break;
                        }
                    }

                    if (!clickonselected)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[i].Selected = true;
                    }
                }

                foreach (ToolStripItem item in cmsLink.Items)
                    item.Visible = true;

                if (dataGridView1.SelectedRows.Count > 1)
                    cmsLink.Items["m_Modify"].Visible = false; // we can modify one row only
                if ((int)treeView1.SelectedNode.Tag == 0)
                    cmsLink.Items["m_NewLink"].Visible = false; // we can't add link to "All Groups"

                cmsLink.Show(Cursor.Position);
            }
        }

        private void GroupsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (txtEditNode.Visible)
            {
                txtEditNode.Visible = false;
                if (!m_editMode) m_editNode.Remove();
            }

            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                selectedNode = e.Node;
                SelectedNodeChanged();

                foreach (ToolStripItem item in cmsGroup.Items)
                    item.Visible = true;

                if ((int)selectedNode.Tag == 0)
                {
                    g_AddLink.Visible = false;
                    g_DeleteGroup.Visible = false;
                    g_RenameGroup.Visible = false;
                }

                cmsGroup.Show(Cursor.Position);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (selectedNode == e.Node) return; // click on already selected node
                treeView1.SelectedNode = e.Node;
                selectedNode = e.Node;
                SelectedNodeChanged();
            }
        }
        public TreeNode selectedNode = null;

        void SelectedNodeChanged()
        {
            dataGridView1.Rows.Clear();
            selectedNode = treeView1.SelectedNode;
            int groupID = (int)selectedNode.Tag;

            if (groupID == 0)
            {
                LinkGroup.Visible = true;
                LinkTitle.Width = (int)(dataGridView1.Width * 0.6);
                LinkGroup.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                LinkGroup.Visible = false;
                LinkTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            using (StixDB _db = new StixDB())
            {
                Dictionary<int, string> groups = new Dictionary<int, string>();
                DataTable dt = _db.ExecuteQuery("select * from LINKGROUPS");
                foreach (DataRow dr in dt.Rows)
                    groups.Add(Convert.ToInt32(dr["id"]), dr["name"].ToString());

                if (groupID == 0)
                    dt = _db.ExecuteQuery("select * from LINKS order by title");
                else
                    dt = _db.ExecuteQuery("select * from LINKS where groupID=" + groupID + " order by title");

                foreach (DataRow dr in dt.Rows)
                {
                    int linkGroup = Convert.ToInt32(dr["groupID"]);

                    AddToTable(dr["title"].ToString(), dr["path"].ToString(), groups[linkGroup],
                        linkGroup);
                }
            }
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1_SelectionChanged(null, null);
            }
        }

        Dictionary<int, string> LinkGroups = new Dictionary<int, string>();

        public static string sortby;
        public static string thenby;

        DataGridViewRow selectedRow;
        DataGridViewSelectedRowCollection selectedRows;

        BrowserDlg OmniBrowser = null;

        bool m_editMode = false;
        TreeNode m_editNode = null;

        HtmlAgilityPack.HtmlDocument htmlDoc = null;

        #region DragDrop
        TreeNode m_dragNode, m_tempDropNode, m_timerNode;
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Get drag node and select it
            m_dragNode = (TreeNode)e.Item;

            imageListDrag.Images.Clear();
            int _width = m_dragNode.Bounds.Size.Width + treeView1.Indent <= 256 ? m_dragNode.Bounds.Size.Width + treeView1.Indent : 256;
            int _height = m_dragNode.Bounds.Height;
            imageListDrag.ImageSize = new Size(_width, _height);

            // Create new bitmap
            // This bitmap will contain the tree node image to be dragged
            Bitmap _bmp = new Bitmap(_width, _height);

            // Get graphics from bitmap
            Graphics _gfx = Graphics.FromImage(_bmp);

            // Draw node icon into the bitmap
            // Draw node label into bitmap
            _gfx.DrawString(m_dragNode.Text,
                treeView1.Font,
                new SolidBrush(treeView1.ForeColor),
                (float)this.treeView1.Indent, 1.0f);

            // Add bitmap to imagelist
            imageListDrag.Images.Add(_bmp);

            // Get mouse position in client coordinates
            Point _p = treeView1.PointToClient(MousePosition);

            // Compute delta between mouse position and node bounds
            int _dx = _p.X + treeView1.Indent - m_dragNode.Bounds.Left;
            int _dy = _p.Y - m_dragNode.Bounds.Top;

            // Begin dragging image
            if (DragHelper.ImageList_BeginDrag(imageListDrag.Handle, 0, _dx, _dy))
            {
                // Begin dragging
                try
                {
                    treeView1.DoDragDrop(_bmp, DragDropEffects.Move);
                    // End dragging image
                    DragHelper.ImageList_EndDrag();
                }
                catch { }
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Bitmap)))
            {
                DragHelper.ImageList_DragEnter(this.treeView1.Handle, e.X - this.treeView1.Left, e.Y - this.treeView1.Top);

                // Enable timer for scrolling dragged item
                this.timer.Enabled = true;
                this.timer1.Enabled = true;
            }
        }

        private void treeView1_DragLeave(object sender, EventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.treeView1.Handle);

            // Disable timer for scrolling dragged item
            this.timer.Enabled = false;
            this.timer1.Enabled = false;
        }

        private void GroupsTree_DragOver(object sender, DragEventArgs e)
        {
            //Cursor.Clip = treeView1.RectangleToScreen(treeView1.ClientRectangle);

            // Compute drag position and move image
            Point clientPoint = this.PointToClient(new Point(e.X, e.Y));

            if (e.Data.GetDataPresent(typeof(Bitmap)))
            {
                DragHelper.ImageList_DragMove(clientPoint.X - this.treeView1.Left, clientPoint.Y - this.treeView1.Top);

                // Get actual drop node
                TreeNode _dropNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(new Point(e.X, e.Y)));
                if (_dropNode == null)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                e.Effect = DragDropEffects.Move;

                // if mouse is on a new node select it
                if (m_tempDropNode != _dropNode)
                {
                    DragHelper.ImageList_DragShowNolock(false);
                    this.treeView1.SelectedNode = _dropNode;
                    if (_dropNode.Nodes.Count > 0)
                    {
                        timer1.Start();
                        m_timerNode = _dropNode;
                    }
                    DragHelper.ImageList_DragShowNolock(true);
                    m_tempDropNode = _dropNode;
                }

                // Avoid that drop node is child of drag node 
                TreeNode _tmpNode = _dropNode;
                while (_tmpNode.Parent != null)
                {
                    if (_tmpNode.Parent == m_dragNode) e.Effect = DragDropEffects.None;
                    _tmpNode = _tmpNode.Parent;
                }
            }
            else
            {
                treeView1.Select();

                var hittest = treeView1.HitTest(clientPoint.X, clientPoint.Y);
                TreeNode node = hittest.Node;

                if (node != null && (int)node.Tag != 0)
                {
                    treeView1.SelectedNode = node; // Indicate node to drop on.

                    if (ModifierKeys == Keys.Control)
                        e.Effect = DragDropEffects.Copy;
                    else if ((int)node.Tag == 0 || node == null)
                        e.Effect = DragDropEffects.None;
                    else
                        e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void treeView1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                // Show pointer cursor while dragging
                e.UseDefaultCursors = false;
                this.treeView1.Cursor = Cursors.Default;
            }
            else e.UseDefaultCursors = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // get node at mouse position
            Point _pt = treeView1.PointToClient(MousePosition);
            TreeNode _node = this.treeView1.GetNodeAt(_pt);

            if (_node == null) return;

            // if mouse is near to the top, scroll up
            if (_pt.Y < 30)
            {
                // set actual node to the upper one
                if (_node.PrevVisibleNode != null)
                {
                    _node = _node.PrevVisibleNode;

                    // hide drag image
                    DragHelper.ImageList_DragShowNolock(false);
                    // scroll and refresh
                    _node.EnsureVisible();
                    this.treeView1.Refresh();
                    // show drag image
                    DragHelper.ImageList_DragShowNolock(true);

                }
            }
            // if mouse is near to the bottom, scroll down
            else if (_pt.Y > this.treeView1.Size.Height - 30)
            {
                if (_node.NextVisibleNode != null)
                {
                    _node = _node.NextVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    _node.EnsureVisible();
                    this.treeView1.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (m_timerNode == this.treeView1.SelectedNode)
                m_timerNode.Expand();
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            bool addAsChildNode = false; // if right mouse button is pressed, drop the child node

            if (ModifierKeys == Keys.Control)
                addAsChildNode = true;

            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            var hittest = treeView1.HitTest(clientPoint.X, clientPoint.Y);
            TreeNode targetNode = hittest.Node;

            // We can't add subgroup to the "All Links" node
            if (addAsChildNode && (int)targetNode.Tag == 0)
                return;

            if (e.Data.GetDataPresent(typeof(Bitmap))) // TreeView Node is dropped
            {
                // Unlock updates
                DragHelper.ImageList_DragLeave(this.treeView1.Handle);

                // Get drop node
                TreeNode _dropNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(new Point(e.X, e.Y)));
                if (m_dragNode == _dropNode)
                    return; // Nothing changed

                // Confirm that the node at the drop location is not the dragged node
                // or a descendant of the dragged node.  
                if (!m_dragNode.Equals(targetNode) && !ContainsNode(m_dragNode, targetNode))
                {
                    TreeNode exParent = null; // dragged node ex-parent
                    int parentID = 0;

                    if (m_dragNode.Parent != null)
                        exParent = m_dragNode.Parent;

                    // Remove the node from its current location and add it to the node at the drop location.  
                    m_dragNode.Remove();
                    int i = targetNode.Index + 1;

                    if (addAsChildNode)
                    {
                        targetNode.Nodes.Add(m_dragNode);
                        parentID = (int)targetNode.Tag;
                    }
                    else
                    {
                        if (targetNode.Parent == null) // root node
                            treeView1.Nodes.Insert(i, m_dragNode);
                        else
                        {
                            targetNode.Parent.Nodes.Insert(i, m_dragNode);
                            parentID = (int)targetNode.Parent.Tag;
                        }
                    }

                    treeView1.SelectedNode = m_dragNode;

                    using (StixDB _db = new StixDB())
                    {
                        // Update parentID of dragged node.
                        _db.ExecuteNonQuery("update LINKGROUPS set parentID=" + parentID + " where id=" + (int)m_dragNode.Tag + "");

                        TreeNodeCollection tnc = treeView1.Nodes;

                        if (exParent != null)
                        {
                            parentID = (int)exParent.Tag;
                            tnc = exParent.Nodes;
                        }

                        // Update order of siblings where dragged node lived.
                        int o = 1; if (parentID == 0) o = 0;
                        foreach (TreeNode node in tnc)
                            _db.ExecuteNonQuery("update LINKGROUPS set _order=" + o++ + " where id=" + (int)node.Tag + "");

                        parentID = 0;
                        tnc = treeView1.Nodes;

                        if (m_dragNode.Parent != null)
                        {
                            parentID = (int)m_dragNode.Parent.Tag;
                            tnc = m_dragNode.Parent.Nodes;
                        }

                        // Update order of siblings where dragged node live now.
                        o = 1; if (parentID == 0) o = 0;
                        foreach (TreeNode node in tnc)
                            _db.ExecuteNonQuery("update LINKGROUPS set _order=" + o++ + " where id=" + (int)node.Tag + "");
                    }
                }
            }
            else // Link from table is dropped
            {
                if (targetNode == null) return;
                if (selectedRows.Count == 0) return;
                if (e.Effect != DragDropEffects.Copy && e.Effect != DragDropEffects.Move) return;

                int groupID = (int)targetNode.Tag;
                if (groupID == 0) return; // "All Links" group. to do

                using (StixDB _db = new StixDB())
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        string path = (string)row.Cells["LinkPath"].Value;

                        DataTable dt = _db.ExecuteQuery("select * from LINKS where " +
                            "path=`" + path + "` and groupID=" + groupID + "");

                        if (dt.Rows.Count > 0) continue; // link exists already

                        int copiedLinksGroup = (int)row.Cells["GroupID"].Value;
                        if (copiedLinksGroup == groupID) continue; // row is copied to its group

                        // Copy 
                        if (e.Effect == DragDropEffects.Copy)
                        {
                            _db.AddLink((string)row.Cells["LinkTitle"].Value, path,
                                (string)row.Cells["LinkType"].Value, "", "", groupID);
                        }
                        // MOVE selected node to selected group.
                        if (e.Effect == DragDropEffects.Move)
                        {
                            _db.ExecuteNonQuery("update LINKS set groupID=" + groupID +
                                " where path =`" + path + "` and groupID =" + copiedLinksGroup + "");

                            if (selectedLinksGroup != 0)
                                dataGridView1.Rows.Remove(row);
                        }
                    }
                }
            }
            SelectedNodeChanged();
        }
        int selectedLinksGroup = 0;
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.  
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node,   
            // call the ContainsNode method recursively using the parent of   
            // the second node.  
            return ContainsNode(node1, node2.Parent);
        }
        #endregion

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    selectedState = dataGridView1.SelectedRows;
            //}
        }
        DataGridViewSelectedRowCollection selectedState;

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedRows == null || selectedRows.Count == 0) return;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Rows are deselected with mousedown. But we need selected rows remain selected.
                try
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        if (!row.Selected)
                            row.Selected = true;
                    }
                }
                catch { }

                // Proceed with the drag and drop, passing in the list item.
                if (ModifierKeys == Keys.Control)
                {
                    dataGridView1.DoDragDrop(dataGridView1.SelectedRows, DragDropEffects.Copy);
                }
                else
                {
                    dataGridView1.DoDragDrop(dataGridView1.SelectedRows, DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            selectedRows = dataGridView1.SelectedRows;
            selectedLinksGroup = (int)treeView1.SelectedNode.Tag;
        }
    }

    internal class GroupItem
    {
        public GroupItem(int id, string name, int parentID, int order)
        {
            ID = id;
            Name = name;
            ParentID = parentID;
            Order = order;
        }

        public int ID;
        public string Name;
        public int ParentID;
        public int Order;
    }

    internal class MyComparer : System.Collections.IComparer
    {
        private static int sortOrderModifier = 1;

        public MyComparer(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

            string by = LinksDlg.sortby;
            string thenby = LinksDlg.thenby;
            string value1 = DataGridViewRow1.Cells[by].Value.ToString();
            string value2 = DataGridViewRow2.Cells[by].Value.ToString();
            string thenvalue1 = DataGridViewRow1.Cells[thenby].Value.ToString();
            string thenvalue2 = DataGridViewRow2.Cells[thenby].Value.ToString();

            // Try to sort based on the sortby column
            int CompareResult = String.Compare(value1, value2);

            // If the results are equal, sort based on the thenby column (file name).
            if (CompareResult == 0)
            {
                CompareResult = String.Compare(thenvalue1, thenvalue2);
                CompareResult *= sortOrderModifier; // to have always ascending sort order in this column
            }
            return CompareResult * sortOrderModifier;
        }
    }

    class MyDataGridViewImageCell : DataGridViewImageCell
    {
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates cellState, object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            cellStyle.BackColor = Color.White;

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value,
                formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }
}
