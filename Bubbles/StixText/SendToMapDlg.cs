using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using Clipboard = System.Windows.Forms.Clipboard;

namespace Bubbles
{
    public partial class SendToMapDlg : Form
    {
        public SendToMapDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "SendToMap.htm");

            Text = Utils.getString("SendToMapDlg.Title");
            btnAddTopic.Text = Utils.getString("SendToMapDlg.btnAddTopic");
            btnClose.Text = Utils.getString("button.close");

            toolTip1.SetToolTip(subtopic, Utils.getString("TextOpsStix.pastesubtopic"));
            toolTip1.SetToolTip(PasteLink, Utils.getString("TextOpsStix.PasteLink.tooltip"));
            toolTip1.SetToolTip(PasteNotes, Utils.getString("TextOpsStix.PasteNotes.tooltip"));

            toolTip1.SetToolTip(OptionTextFormat, Utils.getString("TextOpsStix.workwith.unformatted"));
            toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("textops.contextmenu.insert2"));
            toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("textops.contextmenu.multipletopics2"));
            toolTip1.SetToolTip(OptionSourceLink, Utils.getString("TextOpsStix.sourcelink_no"));
            toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("TextOpsStix.internallinks_no"));

            cm_Remove.Text = Utils.getString("button.remove");
            cm_addSubtopic.Text = Utils.getString("SendToMapDlg.cm_addSubtopic");
            cm_openMap.Text = Utils.getString("SendToMapDlg.cm_openMap");
            cm_rename.Text = Utils.getString("SendToMapDlg.cm_rename");

            treeView1.Sorted = true;

            using (StixDB db = new StixDB("SendToMap"))
            {
                DataTable dt = db.ExecuteQuery("select * from SENDTOMAP");

                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode map = null;
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node.Tag.ToString() == dr["mappath"].ToString())
                            map = node;
                    }

                    if (map == null)
                    {
                        // Add map node
                        map = treeView1.Nodes.Add(dr["mapname"].ToString());
                        map.ForeColor = SystemColors.HotTrack;
                        map.Tag = dr["mappath"].ToString();
                    }

                    // Add topic node
                    TreeNode _node = map.Nodes.Add(dr["topicname"].ToString());
                    _node.Tag = dr["topicguid"].ToString();
                    _node.ForeColor = SystemColors.HotTrack;
                }
            }

            this.HelpButtonClicked += this_HelpButtonClicked;
            treeView1.AfterLabelEdit += TreeView1_AfterLabelEdit;

            this.MouseEnter += (sender, e) => this.Activate();
            treeView1.MouseEnter += (sender, e) => this.Activate();
            panelManage.MouseEnter += (sender, e) => this.Activate();

            if (StixMain.m_stixText == null)
                StixMain.m_stixText = new StixTextOps(0, "H");
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "SendToMap.htm");
        }

        private void btnAddTopic_Click(object sender, EventArgs e)
        {
            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                string mapPath = t.Document.FullName.ToLower();

                using (StixDB db = new StixDB("SendToMap"))
                {
                    DataTable dt = db.ExecuteQuery("select * from SENDTOMAP where topicguid=`" + t.Guid + "`");

                    if (dt.Rows.Count > 0) continue; // topic exists already in the list

                    TreeNode map = null;
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node.Tag.ToString() == mapPath)
                            map = node;
                    }

                    if (map == null)
                    {
                        // Add map node
                        map = treeView1.Nodes.Add(t.Document.CentralTopic.Text);
                        map.Tag = mapPath;
                        map.ForeColor = SystemColors.HotTrack;
                    }

                    // Add topic node to map node
                    TreeNode _node = map.Nodes.Add(t.Text);
                    _node.Tag = t.Guid;
                    _node.ForeColor = SystemColors.HotTrack;

                    db.SendToMapAdd(mapPath, t.Document.CentralTopic.Text, t.Guid, t.Text);
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var hit = treeView1.HitTest(e.Location);
            if (hit.Location == TreeViewHitTestLocations.PlusMinus) return;

            if (e.Button == MouseButtons.Left)
            {
                treeView1.SelectedNode = e.Node;
                if (e.Node.Parent == null) return; // map selected

                Topic t = GetTopic();
                if (t != null)
                    t.SelectOnly(); 
                t = null;
            }
            else if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;

                foreach (ToolStripItem item in NodeMenu.Items) 
                    item.Visible = true;

                if (e.Node.Parent == null) // map node
                {
                    cm_addSubtopic.Visible = false;
                    cm_goToTopic.Visible = false;
                }
                else
                {
                    cm_openMap.Visible = false;
                }

                NodeMenu.Show(MousePosition);
            }
        }

        #region Context Menu

        private void cm_openMap_Click(object sender, EventArgs e)
        {
            string path = treeView1.SelectedNode.Tag.ToString();

            if (!File.Exists(path))
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.opendocfail"));
                return;
            }

            if (Utils.GetOrOpenDocument(path, true) == null)
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.opendocfail"));
                return;
            }
        }

        private void cm_goToTopic_Click(object sender, EventArgs e)
        {
            string topicGuid = treeView1.SelectedNode.Tag.ToString();

            TreeNode mapnode = treeView1.SelectedNode;
            while (mapnode.Parent != null)
                mapnode = mapnode.Parent;
            string path = mapnode.Tag.ToString();

            if (!File.Exists(path))
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.opendocfail"));
                return;
            }

            Document doc = Utils.GetOrOpenDocument(path, true);

            if (doc == null)
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.opendocfail"));
                return;
            }

            Topic t = doc.FindByGuid(topicGuid) as Topic; doc = null;
            if (t == null)
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.topicnotfound"));
                return;
            }

            t.SelectOnly(); t.SnapIntoView();
        }

        private void cm_addSubtopic_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Parent == null) return; // map selected
            Topic t = GetTopic();

            t = t.AddSubTopic(Utils.getString("SendToMapDlg.subtopic"));
            TreeNode _node = node.Nodes.Add(t.Text);
            _node.Tag = t.Guid;
            treeView1.SelectedNode = _node;
            t = null;
            _node.BeginEdit();
        }

        /// <summary>
        /// Get topic from the selected node
        /// </summary>
        /// <returns>Topic</returns>
        Topic GetTopic()
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Parent == null) return null; // map selected

            string topicGuid = node.Tag.ToString();
            string mapPath;

            TreeNode mapnode = node;
            while (mapnode.Parent != null)
                mapnode = mapnode.Parent;
            mapPath = mapnode.Tag.ToString();

            if (!File.Exists(mapPath))
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.opendocfail"));
                return null;
            }

            Document doc = Utils.GetOrOpenDocument(mapPath, false, false);
            if (doc == null)
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.opendocfail"));
                return null;
            }

            Topic t = doc.FindByGuid(topicGuid) as Topic;
            if (t == null)
            {
                MessageBox.Show(Utils.getString("SendToMapDlg.topicnotfound"));
                return null;
            }

            doc = null;
            return t;
        }

        private void cm_Remove_Click(object sender, EventArgs e)
        {

        }

        private void cm_rename_Click(object sender, EventArgs e)
        {

        }

        private void TreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true; return;
            }

            TreeNode node = treeView1.SelectedNode;
            if (node.Parent == null) return; // map selected

            Topic t = GetTopic();
            t.Text = e.Label;
            t = null;
        }
        #endregion

        #region Manage Panel

        private void subtopic_Click(object sender, EventArgs e)
        {

        }

        private void PasteLink_Click(object sender, EventArgs e)
        {
            Topic t = GetTopic();

            if (t != null && Clipboard.ContainsText())
            {
                t.Hyperlinks.AddHyperlink(Clipboard.GetText());
            }
        }

        private void PasteNotes_Click(object sender, EventArgs e)
        {

        }

        private void OptionButton_MouseClick(object sender, MouseEventArgs e)
        {
            StixMain.m_stixText.OptionButton_MouseClick(sender, e);
        }

        #endregion

        private void MM_Click(object sender, EventArgs e)
        {
            if (MMHidden) // Restore MM
            {
                MMHidden = false;

                if (MMMaximized)
                    MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateMaximize;
                else
                {
                    MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateNormal;
                    MMUtils.MindManager.Width = MMWIdth; MMUtils.MindManager.Height = MMHeight;
                    MMUtils.MindManager.Top = MMTop; MMUtils.MindManager.Left = MMLeft;
                }
            }
            else // Hide MM behind this dialog
            {
                if (MMUtils.MindManager.WindowState == MmWindowState.mmWindowStateMaximize)
                    MMMaximized = true;
                else
                    MMMaximized = false; // normal

                MMHidden = true; MMHidding = true;
                // Save MM bounds
                MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateNormal;
                MMWIdth = MMUtils.MindManager.Width; MMHeight = MMUtils.MindManager.Height;
                MMTop = MMUtils.MindManager.Top; MMLeft = MMUtils.MindManager.Left;

                MMUtils.MindManager.Width = this.Width - MM.Width;
                MMUtils.MindManager.Height = this.Height - MM.Height;
                MMUtils.MindManager.Top = this.Top;
                MMUtils.MindManager.Left = this.Left + (MM.Width / 2);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static bool MMHidden = false, MMHidding = false;
        bool MMMaximized = false;
        public static int MMWIdth, MMHeight, MMTop, MMLeft;
    }
}