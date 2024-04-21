using PRAManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

            // New Link panel
            lblTitle.Text = Utils.getString("LinksDlg.LinkTitle");
            lblLink.Text = Utils.getString("LinksDlg.lblLink");
            btnCancel.Text = Utils.getString("button.cancel");

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

            this.MinimumSize = new Size(panelModify.Width, panelModify.Height);

            //panelModify.Location = new Point(panelModify.Location.X, panelModify.Location.Y);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            //this.ResizeEnd += AllSourcesDlg_ResizeEnd;

            Init();
        }

        private void AllSourcesDlg_ResizeEnd(object sender, EventArgs e)
        {
            LinkImage.Width = (int)(pSize.Width * 1.5); // map type icon
            LinkTitle.Width = (int)(dataGridView1.Width / 1.85);
        }

        void Init()
        {
            PopulateGroups();
        }

        void PopulateGroups()
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(Utils.getString("LinksDlg.AllGroups")); root.Tag = 0;
            treeView1.Nodes.Add(root);
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

        private void AddToTable(string title, string path, string groupName, int groupID)
        {
            string imageType = BubbleTools.GetFileType(path);
            Image img;

            if (imageType == "exe")
            {
                try
                {
                    Icon appIcon = Icon.ExtractAssociatedIcon(path);
                    img = appIcon.ToBitmap();
                }
                catch { img = GetImage(imageType); }
            }
            else
                img = GetImage(imageType);

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

        public static Image GetImage(string type)
        {
            switch (type)
            {
                case "audio": return Image.FromFile(Utils.ImagesPath + "ms_audio.png");
                case "excel": return Image.FromFile(Utils.ImagesPath + "ms_excel.png");
                case "exe": return Image.FromFile(Utils.ImagesPath + "ms_exe.png");
                case "image": return Image.FromFile(Utils.ImagesPath + "ms_img.png");
                case "macros": return Image.FromFile(Utils.ImagesPath + "ms_macros.png");
                case "map": return Image.FromFile(Utils.ImagesPath + "ms_map.png");
                case "pdf": return Image.FromFile(Utils.ImagesPath + "ms_pdf.png");
                case "txt": return Image.FromFile(Utils.ImagesPath + "ms_txt.png");
                case "video": return Image.FromFile(Utils.ImagesPath + "ms_video.png");
                case "http": return Image.FromFile(Utils.ImagesPath + "ms_web.png");
                case "word": return Image.FromFile(Utils.ImagesPath + "ms_word.png");
                case "youtube": return Image.FromFile(Utils.ImagesPath + "ms_youtube.png");
                case "chm": return Image.FromFile(Utils.ImagesPath + "chm.png");
            }
            return BubbleTools.file;
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

                txtEditNode.Location = new Point(m_editNode.Bounds.X, m_editNode.Bounds.Y);
                txtEditNode.Size = new Size(treeView1.Width - m_editNode.Bounds.X - pSize.Width, txtEditNode.Height);
                txtEditNode.Visible = true; txtEditNode.Focus();
                txtEditNode.Text = Utils.getString("LinksDlg.btnNewGroup"); 
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
                }
            }
            else if (e.ClickedItem.Name == "g_AddLink")
            {
                panelModify.Location = new Point(
                    (this.Width - panelModify.Width) / 2,
                    (this.Height - panelModify.Height) / 2);

                panelModify.Visible = true;
                panelModify.Tag = "new";
            }
            else if (e.ClickedItem.Name == "m_NewLink")
            {
                panelModify.Location = new Point(
                    (this.Width - panelModify.Width) / 2,
                    (this.Height - panelModify.Height) / 2);

                panelModify.Visible = true;
                panelModify.Tag = "new";
            }
            else if(e.ClickedItem.Name == "m_OpenLink")
            {
                OpenLink(false);
            }
            else if (e.ClickedItem.Name == "m_OpenInOmniBrowser")
            {
                OpenLink(true);
            }
            else if (e.ClickedItem.Name == "m_Modify")
            {
                selectedRow = dataGridView1.SelectedRows[0];

                panelModify.Visible = true;
                panelModify.Tag = "edit";
            }
            else if (e.ClickedItem.Name == "m_Delete")
            {
                selectedRows = dataGridView1.SelectedRows;

                if (MessageBox.Show(Utils.getString("LinksDlg.deletesources"), "",
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

                panelModify.Visible = false;
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
                    try {
                        System.Diagnostics.Process.Start(row.Cells["LinkPath"].Value.ToString());
                    } catch { }
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
                    try {
                        System.Diagnostics.Process.Start(path);
                    } catch { }

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

        /// <summary>Modify source.</summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            string link = txtLink2.Text.Trim();
            string title = txtTitle.Text.Trim();

            if (link == "" || title == "") return; // to do message to user

            int groupID = (int)selectedNode.Tag;
            string type = BubbleTools.GetFileType(link);

            using (StixDB _db = new StixDB())
            {
                DataTable dt = _db.ExecuteQuery("select * from LINKS " +
                    "where groupID=" + groupID + " and path=`" + link + "`");

                if (dt.Rows.Count > 0) return; // to do message to user

                if (panelModify.Tag.ToString() == "new")
                {
                    if (groupID == 0) return;

                    AddToTable(title, link, selectedNode.Text, groupID);
                    _db.AddLink(title, link, type, groupID);
                }
                else if (panelModify.Tag.ToString() == "edit")
                {
                    if (groupID == 0) // "All Links" group selected. Get link group id from link itself
                        groupID = (int)dataGridView1.SelectedRows[0].Cells["GroupID"].Value;

                    // Modify in the table
                    dataGridView1.SelectedRows[0].Cells["LinkTitle"].Value = title;
                    dataGridView1.SelectedRows[0].Cells["LinkPath"].Value = link;
                    txtLink.Text = link;

                    // Update in the database
                    _db.ExecuteNonQuery("update LINKS set" +
                        "title=`" + title + "`, path=`" + link + "`");
                }
            }
        }

        private void btnGetTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTitle.Text = "";
            string link = txtLink2.Text.Trim();
            if (link == "") return;
            string title = "";

            if (link.StartsWith("http"))
            {
                title = GetWebPageTitle(link);
            }

            if (!String.IsNullOrEmpty(title))
                txtTitle.Text = title;
        }

        private void txtLink2_KeyUp(object sender, KeyEventArgs e)
        {
            btnGetTitle_LinkClicked(sender, null);
            e.Handled = true; // to avoid the "ding" sound
            e.SuppressKeyPress = true;
        }

        private void txtLink2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtLink2.SelectAll();
        }

        string GetWebPageTitle(string url)
        {
            string title = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest.Create(url) as HttpWebRequest);
                HttpWebResponse response = (request.GetResponse() as HttpWebResponse);

                using (Stream stream = response.GetResponseStream())
                {
                    // compiled regex to check for <title></title> block
                    Regex titleCheck = new Regex(@"<title>\s*(.+?)\s*</title>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    int bytesToRead = 8092;
                    byte[] buffer = new byte[bytesToRead];
                    string contents = "";
                    int length = 0;
                    while ((length = stream.Read(buffer, 0, bytesToRead)) > 0)
                    {
                        // convert the byte-array to a string and add it to the rest of the
                        // contents that have been downloaded so far
                        contents += Encoding.UTF8.GetString(buffer, 0, length);

                        Match m = titleCheck.Match(contents);
                        if (m.Success)
                        {
                            // we found a <title></title> match =]
                            title = m.Groups[1].Value.ToString();
                            break;
                        }
                        else if (contents.Contains("</head>"))
                        {
                            // reached end of head-block; no title found =[
                            break;
                        }
                    }
                }
            }
            catch (Exception _e)
            {
                Console.WriteLine(_e);
            }

            return title;
        }

        /// <summary>Cancel modify source</summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelModify.Visible = false;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var item = dataGridView1.SelectedRows[0];

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
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                selectedNode = e.Node;

                foreach (ToolStripItem item in cmsGroup.Items)
                    item.Visible = true;

                if (selectedNode.Index == 0)
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
        }

        Dictionary<int, string> LinkGroups = new Dictionary<int, string>();

        public static string sortby;
        public static string thenby;

        DataGridViewRow selectedRow;
        DataGridViewSelectedRowCollection selectedRows;

        BrowserDlg OmniBrowser = null;

        bool m_editMode = false;
        TreeNode m_editNode = null;

        List<GroupItem> Groups = new List<GroupItem>();

        private void GroupsTree_DragOver(object sender, DragEventArgs e)
        {          
            treeView1.Select();
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = treeView1.PointToClient(new Point(e.X, e.Y));

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

        private void GroupsTree_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            var hittest = treeView1.HitTest(clientPoint.X, clientPoint.Y);
            TreeNode node = hittest.Node;

            if (node == null) return;
            if (selectedRows.Count == 0) return;
            if (e.Effect != DragDropEffects.Copy && e.Effect != DragDropEffects.Move) return;

            int groupID = (int)node.Tag;
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
                            (string)row.Cells["LinkType"].Value, groupID);
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

            SelectedNodeChanged();
        }
        int selectedLinksGroup = 0;

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedRows == null || selectedRows.Count == 0) return;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Rows are deselected with mousedown. But we need selected rows remain selected.
                try {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        if (!row.Selected) 
                            row.Selected = true;
                    }
                } catch { }

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

            ////// Get the index of the item the mouse is below.
            ////int rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            //////if shift key is not pressed
            ////if (Control.ModifierKeys != Keys.Shift && Control.ModifierKeys != Keys.Control)
            ////{
            ////    //if row under the mouse is not selected
            ////    if (rowIndexFromMouseDown > 0)
            ////    {
            ////        //if there only one row selected
            ////        if (dataGridView1.SelectedRows.Count == 1)
            ////        {
            ////            //select the row below the mouse
            ////            dataGridView1.ClearSelection();
            ////            dataGridView1.Rows[rowIndexFromMouseDown].Selected = true;
            ////        }
            ////    }
            ////}
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
}
