using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace Bubbles
{
    public partial class QuickTopicsDlg : Form
    {
        public QuickTopicsDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "quick_topics.htm");

            lblTitle.Text = Utils.getString("QuickTopicsDlg.Title");

            m_Delete.Text = Utils.getString("button.delete");
            m_Rename.Text = Utils.getString("button.rename");

            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.Paint += this_Paint; // paint the border
            this.MouseDown += QuickTopicsDlg_MouseDown;
        }

        private void pHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "quick_topics.htm");
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == m_Delete)
            {
                if (treeView1.SelectedNode.Parent == null) // Group
                {
                    if ((int)treeView1.SelectedNode.Tag == 1)
                    {
                        MessageBox.Show(Utils.getString("TaskTemplateDlg.deletegroup.error"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (MessageBox.Show(Utils.getString("TaskTemplateDlg.deletegroup.question"), "",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (StixDB db = new StixDB())
                        {
                            db.ExecuteNonQuery("delete from QUICKTOPICTEMPLATES " +
                                "where groupID=" + (int)treeView1.SelectedNode.Tag + "");

                            db.ExecuteNonQuery("delete from QUICKTOPICGROUPS " +
                                "where id=" + (int)treeView1.SelectedNode.Tag + "");
                        }
                        treeView1.SelectedNode.Remove();
                    }
                }
                else // Template
                {
                    if (MessageBox.Show(Utils.getString("TaskTemplateDlg.delete.question"), "",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (StixDB db = new StixDB())
                        {
                            db.ExecuteNonQuery("delete from QUICKTOPICTEMPLATES " +
                                "where id=" + ((QuickTopicItem)treeView1.SelectedNode.Tag).ID + "");
                        }
                        treeView1.SelectedNode.Remove();
                    }
                }
            }
            else if (e.ClickedItem == m_Rename)
            {
                treeView1.SelectedNode.BeginEdit();
            }
        }

        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Point p = new Point(pClose.Height, this.PointToClient(Cursor.Position).Y - p11.Height);

            ToolTip tip = new ToolTip();
            tip.Show("ESC to cancel edit", this, p, 2000);
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null) return;

            string oldName = treeView1.SelectedNode.Text.Trim();
            string newName = e.Label.Trim();
            if (oldName == newName) return;

            TreeNode selected = treeView1.SelectedNode;
            bool group = selected.Parent == null;

            using (StixDB db = new StixDB())
            {
                if (group)
                {
                    DataTable dt = db.ExecuteQuery("select * from QUICKTOPICGROUPS " +
                        "where name=`" + newName + "`");

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("TaskTemplateDlg.groupexists"), "");
                        return;
                    }

                    db.ExecuteNonQuery("update QUICKTOPICGROUPS set " +
                        "name=`" + newName + "` where name=`" + oldName + "`");
                }
                else // rename template
                {
                    DataTable dt = db.ExecuteQuery("select * from QUICKTOPICTEMPLATES " +
                        "where name=`" + newName + "` and groupID=" + (int)selected.Parent.Tag + "");

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("TaskTemplateDlg.templateexists"), "");
                        return;
                    }

                    db.ExecuteNonQuery("update QUICKTOPICTEMPLATES set " +
                        "name=`" + newName + "` where name=`" + oldName + 
                        "` and groupID=" + (int)selected.Parent.Tag + "");
                }
            }

            if (!group) selected.Parent.Expand();

            if (StixMain.m_TaskInfo.Visible)
                StixMain.m_TaskInfo.PopulateQuickTopics(true);
        }

        private void QuickTopicsDlg_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0) return;

                QuickTopicItem QuickTask = e.Node.Tag as QuickTopicItem;
                if (QuickTask == null) return; // group clicked

                StixMain.m_TaskInfo.QuickTopic = QuickTask;

                Transaction _tr = MMUtils.ActiveDocument.NewTransaction(Utils.getString("QuickTask.transaction.name"));
                _tr.IsUndoable = true;
                _tr.Execute += new ITransactionEvents_ExecuteEventHandler(StixMain.m_TaskInfo.SetQuickTopic);
                _tr.Start();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if ((int)e.Node.Tag == 1)
                    m_Delete.Visible = false;
                else
                    m_Delete.Visible = true;

                contextMenuStrip1.Show(MousePosition);
                treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
            }
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            using (TaskTemplateDlg dlg = new TaskTemplateDlg())
            {
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        #region DragDrop
        /// <summary>
        /// Handle user dragging nodes in treeview
        /// </summary>
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// Handle user dragging node into another node
        /// </summary>
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = treeView1.GetNodeAt(targetPoint);
            treeView1.SelectedNode = targetNode;
        }

        /// <summary>
        /// Handle user dropping a dragged node onto another node
        /// </summary>
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Sanity check
            if (draggedNode == null) return;

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeView1.GetNodeAt(targetPoint);

            bool GroupIsDragged = draggedNode.Parent == null;

            TreeNode draggedGroupNode = null, droppedGroupNode = null;
            if (!GroupIsDragged) draggedGroupNode = draggedNode.Parent;

            if (GroupIsDragged && draggedNode.Tag.ToString() == "1")
                return; // Favorites group can not be moved

            bool copy = e.KeyState == 8; // Ctrl key holding
            TreeNode clonedNode = (TreeNode)draggedNode.Clone();

            // Group 
            if (targetNode == null) // Drop on the blank space. Add group to the end.
            {
                if (!GroupIsDragged) return; // Groups only!

                if (!copy) draggedNode.Remove();
                treeView1.Nodes.Add(clonedNode);
            }
            else
            {
                bool DropToGroup = targetNode.Parent == null;

                if (GroupIsDragged && !DropToGroup)
                    return; // group to topic - no

                TreeNode parentNode = targetNode;

                // Confirm that the node at the drop location is not 
                // the dragged node and that target node isn't null
                // (for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && targetNode != null)
                {
                    bool canDrop = true;
                    while (canDrop && (parentNode != null))
                    {
                        canDrop = !Object.ReferenceEquals(draggedNode, parentNode);
                        parentNode = parentNode.Parent;
                    }

                    if (canDrop)
                    {
                        if (!copy) draggedNode.Remove();

                        using (StixDB db = new StixDB())
                        {
                            if (GroupIsDragged)
                            {
                                treeView1.Nodes.Insert(targetNode.Index + 1, clonedNode);

                                if (copy)
                                {
                                    db.AddQuickTopicGroup(clonedNode.Text, 0);

                                    int id = 0;
                                    DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                                    clonedNode.Tag = id;
                                }
                            }
                            else // Topic is moved
                            {
                                if (DropToGroup) // Drop to group. Insert to the first place.
                                {
                                    targetNode.Nodes.Insert(0, clonedNode);
                                    droppedGroupNode = targetNode;
                                }
                                else // Insert under the dropped node.
                                {
                                    targetNode.Parent.Nodes.Insert(targetNode.Index + 1, clonedNode);
                                    droppedGroupNode = targetNode.Parent;
                                }

                                QuickTopicItem item = clonedNode.Tag as QuickTopicItem;

                                if (item.GroupID != (int)droppedGroupNode.Tag && !copy) // Topic was moved to other group!
                                {
                                    db.ExecuteNonQuery("update QUICKTOPICTEMPLATES set " +
                                    "groupID=" + (int)droppedGroupNode.Tag + " where id=" + item.ID + "");
                                    item.GroupID = (int)droppedGroupNode.Tag;
                                }

                                if (copy)
                                {
                                    DataTable dt = db.ExecuteQuery("select * from QUICKTOPICTEMPLATES " +
                                        "where id=" + item.ID + "");

                                    if (dt.Rows.Count > 0) 
                                    {
                                        DataRow row = dt.Rows[0];
                                        db.AddQuickTopicTemplate(item.Name, (int)droppedGroupNode.Tag, item.Order, row["topictext"].ToString(), 
                                            row["progress"].ToString(), row["priority"].ToString(), row["dates"].ToString(), 
                                            row["duration"].ToString(), row["effort"].ToString(), row["icons"].ToString(),
                                            row["resources"].ToString(), row["tags"].ToString());

                                        int id = 0;
                                        dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                                        if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                                        item.ID = id;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            using (StixDB db = new StixDB())
            {
                int i = 1;

                // Order groups
                if (GroupIsDragged)
                {
                    i++; // start with the second group (first is Favorites)
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node != null)
                        {
                            if ((int)node.Tag == 1) continue; // Favorites group
                            db.ExecuteNonQuery("update QUICKTOPICGROUPS set " +
                                "_order=" + i++ + " where id=" + (int)node.Tag);
                        }
                    }
                }
                else
                {
                    // Order group from which node was dragged
                    if (!copy) // If node was copied, no need to order 
                    {
                        foreach (TreeNode node in draggedGroupNode.Nodes)
                        {
                            if (node != null)
                            {
                                int id = ((QuickTopicItem)node.Tag).ID;
                                db.ExecuteNonQuery("update QUICKTOPICTEMPLATES set " +
                                    "_order=" + i++ + " where id=" + id + "");
                            }
                        }
                    }
                    i = 1;
                    // Order group to which node was dropped
                    foreach (TreeNode node in droppedGroupNode.Nodes)
                    {
                        if (node != null)
                        {
                            int id = ((QuickTopicItem)node.Tag).ID;
                            db.ExecuteNonQuery("update QUICKTOPICTEMPLATES set " +
                                "_order=" + i++ + " where id=" + id + "");
                        }
                    }
                }
            }
        }
        #endregion

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

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
