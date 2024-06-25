using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Bubbles
{
    public partial class BookmarksDlg : Form
    {
        public BookmarksDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "GlobalBookmarks.htm");

            Text = Utils.getString("BookmarksDlg.Title");
            btnNewGroup.Text = Utils.getString("BookmarksDlg.btnNewGroup");
            btnClose.Text = Utils.getString("button.close");
            btnCancel.Text = Utils.getString("button.cancel");

            treeView1.Sorted = true;

            imageList1.ImageSize = p1.Size;
            Image img1 = Image.FromFile(Utils.m_imagesPath + "folder.png");
            Image img2 = Image.FromFile(Utils.m_imagesPath + "folder_opened.png");
            Image img3 = Image.FromFile(Utils.m_imagesPath + "bookmark_s.png");
            img1 = new Bitmap(img1, p1.Size);
            img2 = new Bitmap(img2, p1.Size);
            img3 = new Bitmap(img3, p1.Size);
            imageList1.Images.Add(img1);
            imageList1.Images.Add(img2);
            imageList1.Images.Add(img3);

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from BOOKMARKGROUPS order by name");

                foreach (DataRow dr in dt.Rows)
                {
                    // Add group.
                    TreeNode node = treeView1.Nodes.Add("group", dr["name"].ToString());
                    node.Tag = new GroupNode(Convert.ToInt32(dr["id"]), dr["name"].ToString());
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = node.IsExpanded ? 1 : 0;

                    DataTable _dt = db.ExecuteQuery("select * from BOOKMARKS where groupID=" +
                        Convert.ToInt32(dr["id"]) + "");

                    // Add bookmarks to group.
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        TreeNode _node = node.Nodes.Add("bookmark", _dr["name"].ToString());
                        _node.Tag = new BookmarkNode(_dr["name"].ToString(), _dr["mappath"].ToString(), 
                            _dr["topicguid"].ToString(), Convert.ToInt32(_dr["groupID"]));
                        _node.ImageIndex = 2;
                        _node.SelectedImageIndex = 2;
                    }
                }
            }
            treeView1.Select();

            this.HelpButtonClicked += this_HelpButtonClicked;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "GlobalBookmarks.htm");
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            lblGroupBookmark.Text = Utils.getString("BookmarksDlg.groupname");
            btnAction.Text = Utils.getString("BookmarksDlg.add_group");
            txtGroupBookmark.Text = "";
            btnAction.Tag = "addgroup";

            panelActions.Visible = true;
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            string name = txtGroupBookmark.Text.Trim();
            if (name.Length < 1) return;

            string action = btnAction.Tag.ToString();

            using (StixDB db = new StixDB())
            {

                if (action == "addgroup" || action == "renamegroup")
                {
                    DataTable dt = db.ExecuteQuery("select * from BOOKMARKGROUPS where name=`" + name + "`");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("BookmarksDlg.groupexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                    }

                    if (action == "addgroup")
                    {
                        db.AddBookmarkGroup(name);
                        // Get created group id
                        int groupID = 1;
                        dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                        if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);

                        TreeNode node = treeView1.Nodes.Add("group", name);
                        node.Tag = new GroupNode(groupID, name);
                    }
                    else // rename group
                    {
                        db.ExecuteNonQuery("update BOOKMARKGROUPS set name=`" + name +
                            "` where name=`" + treeView1.SelectedNode.Text + "`");

                        treeView1.SelectedNode.Text = name;
                        GroupNode gn = treeView1.SelectedNode.Tag as GroupNode;
                        gn.GroupName = name;
                        treeView1.SelectedNode.Tag = gn;
                    }
                }
                else if (action == "addbookmark")
                {
                    GroupNode group = treeView1.SelectedNode.Tag as GroupNode;
                    DataTable dt = db.ExecuteQuery("select * from BOOKMARKS where name=`" + name +
                        "` and groupID=" + group.ID + "");

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("BookmarksDlg.bookmarkexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                    }

                    Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
                    if (t != null)
                    {
                        TreeNode node = treeView1.SelectedNode.Nodes.Add("bookmark", name);
                        node.Tag = new BookmarkNode(name, MMUtils.ActiveDocument.FullName, t.Guid, group.ID);
                        node.ImageIndex = 2; node.SelectedImageIndex = 2;
                        db.AddBookmark(name, MMUtils.ActiveDocument.FullName, t.Guid, group.ID);
                        panelActions.Visible = false;
                    }
                }
                else if (action == "renamebookmark")
                {
                    BookmarkNode bn = treeView1.SelectedNode.Tag as BookmarkNode;

                    DataTable dt = db.ExecuteQuery("select * from BOOKMARKS where name=`" + name +
                        "` and groupID=" + bn.GroupID + "");

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("BookmarksDlg.bookmarkexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                    }

                    db.ExecuteNonQuery("update BOOKMARKS set name=`" + name +
                        "` where name=`" + treeView1.SelectedNode.Text + "` and groupID=" + bn.GroupID + "");

                    treeView1.SelectedNode.Text = name;
                    bn.BookmarkName = name;
                    treeView1.SelectedNode.Tag = bn;
                }
            }
            panelActions.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelActions.Visible = false;
        }

        private void txtNewGroup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAction_Click(null, null);
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (treeView1.SelectedNode.Name == "group")
            {
                if (treeView1.SelectedNode.IsExpanded)
                    treeView1.SelectedNode.Collapse();
                else
                    treeView1.SelectedNode.Expand();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node.Name == "bookmark")
                {
                    BookmarkNode bn = e.Node.Tag as BookmarkNode;
                    string mapPath = bn.MapPath;
                    string topicGuid = bn.TopicGuid;

                    // Get or activate map.
                    Document doc = null;
                    foreach (Document _doc in MMUtils.MindManager.AllDocuments)
                    {
                        if (_doc.FullName == mapPath)
                        {
                            doc = _doc; doc.Activate(); break;
                        }
                    }

                    if (doc == null)
                        doc = MMUtils.MindManager.AllDocuments.Open(mapPath);

                    if (doc == null)
                    {
                        MessageBox.Show(String.Format(Utils.getString("BookmarksDlg.mapnotfound"), mapPath), "", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Select bookmarked topic.
                    Topic t = doc.FindByGuid(topicGuid) as Topic;
                    doc = null;
                    if (t == null)
                    {
                        MessageBox.Show(String.Format(Utils.getString("BookmarksDlg.topicnotfound"), mapPath), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        t.SelectOnly();
                        t.SnapIntoView();
                        StixUtils.ActivateMindManager();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;

                if (e.Node.Name == "group")
                    cmsGroup.Show(MousePosition);
                else if (e.Node.Name == "bookmark")
                    cmsBookmark.Show(MousePosition);
            }
        }

        private void b_addBookmark_Click(object sender, EventArgs e)
        {
            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t == null) return;

            lblGroupBookmark.Text = Utils.getString("BookmarksDlg.bookmarkname");
            btnAction.Text = Utils.getString("BookmarksDlg.addbookmark");
            string text = t.Text;
            if (text.Length > 35) text = text.Substring(0, 35);
            txtGroupBookmark.Text = text;
            btnAction.Tag = "addbookmark";

            panelActions.Visible = true;
        }

        private void b_renameGroup_Click(object sender, EventArgs e)
        {
            lblGroupBookmark.Text = Utils.getString("BookmarksDlg.groupname");
            btnAction.Text = Utils.getString("BookmarksDlg.renamegroup");
            txtGroupBookmark.Text = treeView1.SelectedNode.Text + " (2)";
            btnAction.Tag = "renamegroup";

            panelActions.Visible = true;
        }

        private void b_deleteGroup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Utils.getString("BookmarksDlg.deletegroup.confirm"), "", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                GroupNode gn = (GroupNode)treeView1.SelectedNode.Tag;
                if (gn != null)
                {
                    using (StixDB db = new StixDB())
                    {
                        db.ExecuteNonQuery("delete from BOOKMARKS where groupID=" + gn.ID + "");
                        db.ExecuteNonQuery("delete from BOOKMARKGROUPS where id=" + gn.ID + "");
                    }

                    treeView1.SelectedNode.Remove();
                }
            }
        }

        private void b_renameBookmark_Click(object sender, EventArgs e)
        {
            lblGroupBookmark.Text = Utils.getString("BookmarksDlg.bookmarkname");
            txtGroupBookmark.Text = treeView1.SelectedNode.Text + " (2)";
            btnAction.Text = Utils.getString("BookmarksDlg.renamebookmark");
            btnAction.Tag = "renamebookmark";

            panelActions.Visible = true;
        }

        private void b_deleteBookmark_Click(object sender, EventArgs e)
        {
            BookmarkNode bn = (BookmarkNode)treeView1.SelectedNode.Tag;
            if (bn != null)
            {
                treeView1.SelectedNode.Remove();

                using (StixDB db = new StixDB())
                {
                    db.ExecuteNonQuery("delete from BOOKMARKS where groupID=" + bn.GroupID + 
                        " and name=`" + bn.BookmarkName + "`");
                }
            }
        }
    }

    internal class GroupNode
    {
        public GroupNode(int id, string groupName)
        {
            GroupName = groupName;
            ID = id;
        }
        public string GroupName = "";
        public int ID;
    }

    internal class BookmarkNode
    {
        public BookmarkNode(string bookmarkName, string mapPath, string topicGuid, int groupID)
        {
            BookmarkName = bookmarkName;
            MapPath = mapPath;
            TopicGuid = topicGuid;
            GroupID = groupID;
        }
        public string BookmarkName = "";
        public string MapPath = "";
        public string TopicGuid;
        public int GroupID;
    }
}
