using Bubbles.AppManager;
using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Clipboard = System.Windows.Forms.Clipboard;
using Image = System.Drawing.Image;

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
            btnClose.Text = Utils.getString("button.close");
            txtAddTopic.Text = Utils.getString("SendToMapDlg.txtAddTopic");
            lblAddTopic.Text = Utils.getString("SendToMapDlg.lblAddTopic");
            lblSuccessMessage.Text = Utils.getString("SendToMapDlg.lblSuccessMessage");

            toolTip1.SetToolTip(subtopic, Utils.getString("TextOpsStix.pastesubtopic"));
            toolTip1.SetToolTip(PasteLink, Utils.getString("TextOpsStix.PasteLink.tooltip2"));
            toolTip1.SetToolTip(PasteNotes, Utils.getString("TextOpsStix.AddNotes.tooltip"));

            toolTip1.SetToolTip(MM, Utils.getString("SendToMapDlg.MM.tooltip"));
            toolTip1.SetToolTip(Manage, Utils.getString("ManageIcon.tooltip"));

            toolTip1.SetToolTip(OptionTextFormat, Utils.getString("TextOpsStix.workwith.unformatted"));
            toolTip1.SetToolTip(OptionReplaceInsert, Utils.getString("textops.contextmenu.insert2"));
            toolTip1.SetToolTip(OptionMultipleTopics, Utils.getString("textops.contextmenu.multipletopics2"));
            toolTip1.SetToolTip(OptionSourceLink, Utils.getString("TextOpsStix.sourcelink_no"));
            toolTip1.SetToolTip(OptionInternalLinks, Utils.getString("TextOpsStix.internallinks_no"));

            cm_Remove.Text = Utils.getString("button.delete");
            cm_openMap.Text = Utils.getString("SendToMapDlg.cm_openMap");
            cm_rename.Text = Utils.getString("SendToMapDlg.cm_rename");
            cm_showSubtopics.Text = Utils.getString("SendToMapDlg.cm_showSubtopics");
            cm_goToTopic.Text = Utils.getString("SendToMapDlg.cm_goToTopic");

            m_saveOptions.Text = Utils.getString("textops.manage.saveoptions");
            m_addReceivingTopic.Text = Utils.getString("SendToMapDlg.addReceivingTopic");
            m_saveOptions.ToolTipText = Utils.getString("textops.manage.saveoptions.tooltip");
            m_addReceivingTopic.ToolTipText = Utils.getString("SendToMapDlg.addReceivingTopic.tooltip");

            //this.MinimumSize = new Size(this.Width, this.Height);
            treeView1.VisibleChanged += TreeView1_VisibleChanged;

            using (StixDB db = new StixDB("SendToMap"))
            {
                XMLTopicCompanion tc;
                DataTable dt = db.ExecuteQuery("select * from SENDTOMAP order by orderID");

                foreach (DataRow dr in dt.Rows)
                {
                    string mappath = dr["mappath"].ToString();
                    if (SendToMaps.ContainsKey(mappath))
                        tc = SendToMaps[mappath][dr["topicguid"].ToString()];
                    else
                    {
                        if (XMLMapCompanion.Get(mappath) == null) continue;
                        SendToMaps.Add(mappath, XMLMapCompanion.m_topics);
                        tc = XMLMapCompanion.m_topics[dr["topicguid"].ToString()];
                    }
                    if (tc == null) continue;

                    TreeNode map = null;
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node.Tag.ToString() == mappath)
                            map = node;
                    }

                    if (map == null)
                    {
                        // Add map node
                        map = treeView1.Nodes.Add(dr["mapname"].ToString());
                        map.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
                        map.ForeColor = SystemColors.HotTrack;
                        map.Tag = dr["mappath"].ToString();
                        map.Text = map.Text;
                    }

                    // Get topics from XML

                    // Add main In-Tray topic
                    TreeNode _node = map.Nodes.Add(tc.TopicText);
                    _node.Tag = dr["topicguid"].ToString();
                    _node.ForeColor = SystemColors.HotTrack;

                    string topicName, topicGuid;

                    // Add subtopics of the main In-Tray topic
                    XmlNode subtopics = tc.m_root.SelectSingleNode("ap:SubTopics", tc.NSManager);
                    if (subtopics == null) continue; // no subtopics

                    foreach (XmlNode node in subtopics.ChildNodes)
                    {
                        XmlNode text = node.SelectSingleNode("ap:Text", tc.NSManager);
                        topicName = text.Attributes.GetNamedItem("PlainText").Value;
                        topicGuid = node.Attributes.GetNamedItem("OId").Value;

                        TreeNode subnode = _node.Nodes.Add(topicName);
                        subnode.Tag = topicGuid;
                    }
                }
            }

            SelectedTopicShow();

            this.HelpButtonClicked += this_HelpButtonClicked;
            treeView1.AfterLabelEdit += TreeView1_AfterLabelEdit;
            this.ResizeEnd += SendToMapDlg_ResizeEnd;

            this.MinimumSize = panelMinimized.Size;

            this.MouseEnter += (sender, e) => {  if (activate) this.Activate(); };
            treeView1.MouseEnter += (sender, e) => { if (activate) this.Activate(); };
            panelManage.MouseEnter += (sender, e) => { if (activate) this.Activate(); };

            if (StixMain.m_stixText == null)
                StixMain.m_stixText = new StixTextOps(0, "H");
        }
        public static bool activate = true;

        public void InitOptions()
        {
            string[] options = Utils.getRegistry("PasteOptionsSendToMap", "unformatted,textreplace,single,sourceno,linksno").Split(',');

            OptionTextFormat.Tag = options.Contains(tag_unformatted) ? tag_formatted :
                options.Contains(tag_formatted) ? tag_unformatted_links : tag_unformatted;
            OptionButton_MouseClick(OptionTextFormat, null);

            OptionReplaceInsert.Tag = options.Contains(tag_textreplace) ? tag_textadd : tag_textreplace;
            OptionButton_MouseClick(OptionReplaceInsert, null);

            OptionMultipleTopics.Tag = options.Contains(tag_single) ? tag_multiple : tag_single;
            OptionButton_MouseClick(OptionMultipleTopics, null);

            OptionSourceLink.Tag = options.Contains(tag_sourceno) ? tag_source_first :
                options.Contains(tag_sourceyes) ? tag_sourceno : tag_sourceyes;
            OptionButton_MouseClick(OptionSourceLink, null);

            OptionInternalLinks.Tag = options.Contains(tag_linksyes) ? tag_linksno : tag_linksyes;
            OptionButton_MouseClick(OptionInternalLinks, null);

        }

        // Workaround for bold font in the treeview nodes
        private void TreeView1_VisibleChanged(object sender, EventArgs e)
        {
            if (((TreeView)sender).Visible == true)
                treeView1.BackColor = System.Drawing.Color.Empty;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "SendToMap.htm");
        }

        public void SelectedTopicShow()
        {
            if (MMUtils.ActiveDocument == null || 
                MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
            {
                if (m_selected != null)
                    m_selected.Remove();
                m_selected = null;
            }
            else
            {
                if (m_selected == null)
                {
                    m_selected = treeView1.Nodes.Add(Utils.getString("SendToMapDlg.selectedtopic"));
                    m_selected.ForeColor = SystemColors.HotTrack;
                    m_selected.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
                    m_selected.Text = m_selected.Text;
                    m_selected.Tag = "selected";
                }
            }
        }
        TreeNode m_selected = null;

        private void m_addReceivingTopic_Click(object sender, EventArgs e)
        {
            if (m_selected != null) m_selected.Remove();

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                string mapPath = t.Document.FullName.ToLower();

                using (StixDB db = new StixDB("SendToMap"))
                {
                    DataTable dt = db.ExecuteQuery("select * from SENDTOMAP where topicguid=`" + t.Guid + "`");

                    if (dt.Rows.Count > 0) continue; // topic exists already in the list

                    TreeNode map = null; int order = 0;
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node.Tag.ToString() == mapPath)
                        {
                            map = node;
                            order = node.Index + 1;
                        }
                    }

                    if (map == null)
                    {
                        // Add map node
                        map = treeView1.Nodes.Add(t.Document.CentralTopic.Text);
                        map.Tag = mapPath;
                        map.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
                        map.ForeColor = SystemColors.HotTrack;
                        order = treeView1.Nodes.Count;
                    }

                    // Add topic node to map node
                    TreeNode _node = map.Nodes.Add(t.Text);
                    _node.Tag = t.Guid;
                    _node.ForeColor = SystemColors.HotTrack;

                    db.SendToMapAdd(mapPath, t.Document.CentralTopic.Text, t.Guid, order);
                }
            }
        }

        private void m_saveOptions_Click(object sender, EventArgs e)
        {
            string options = OptionTextFormat.Tag.ToString() + "," +
                    OptionReplaceInsert.Tag.ToString() + "," +
                    OptionMultipleTopics.Tag.ToString() + "," +
                    OptionSourceLink.Tag.ToString() + "," +
                    OptionInternalLinks.Tag.ToString();

            Utils.setRegistry("PasteOptionsSendToMap", options);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var hit = treeView1.HitTest(e.Location);
            if (hit.Location == TreeViewHitTestLocations.PlusMinus) return;

            if (e.Button == MouseButtons.Left)
            {
                treeView1.SelectedNode = e.Node;
                if (e.Node.Parent == null) return; // map selected

                //Topic t = GetTopic();
                //if (t != null)
                //    t.SelectOnly(); 
                //t = null;
            }
            else if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;

                foreach (ToolStripItem item in NodeMenu.Items)
                    item.Visible = true;

                if (e.Node.Parent == null) // map node or Selected Topic node
                {
                    if (e.Node.Tag == null || e.Node.Tag.ToString() == "selected")
                        return;

                    cm_goToTopic.Visible = false;
                    cm_showSubtopics.Visible = false;
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
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null) 
                return;

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

            RestoreMM();
        }

        private void cm_goToTopic_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
                return;

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
                string topictext = treeView1.SelectedNode.Text;
                if (topictext.Length > 62) topictext = topictext.Substring(0, 60) + "...";
                MessageBox.Show(String.Format(Utils.getString("SendToMapDlg.topicnotfound"), topictext));
                treeView1.SelectedNode.Remove();
                return;
            }

            RestoreMM();
            t.SelectOnly(); t.SnapIntoView();
        }

        /// <summary>
        /// Get topic from the selected node
        /// </summary>
        /// <returns>Topic</returns>
        Topic GetTopic()
        {
            TreeNode node = treeView1.SelectedNode;

            if (node == null) return null;
            if (node.Tag.ToString() == "selected")
            {
                if (MMUtils.ActiveDocument == null) return null;
                return MMUtils.ActiveDocument.Selection.PrimaryTopic;
            }
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
                string topictext = node.Text;
                if (topictext.Length > 62) topictext = topictext.Substring(0, 60) + "...";
                MessageBox.Show(String.Format(Utils.getString("SendToMapDlg.topicnotfound"), topictext));
                node.Remove();
                return null;
            }

            doc = null;
            return t;
        }

        private void cm_Remove_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null) return;

            using (StixDB db = new StixDB("SendToMap"))
            {
                if (node.Parent == null) // map node
                {
                    if (MessageBox.Show(Utils.getString("SendToMapDlg.delete"), "",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        return;
                    db.ExecuteNonQuery("delete from SENDTOMAP where mappath=`" + node.Tag.ToString() + "`");
                }
                else
                    db.ExecuteNonQuery("delete from SENDTOMAP where topicguid=`" + node.Tag.ToString() + "`");
            }

            node.Remove();
        }

        private void cm_rename_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null) return;

            treeView1.LabelEdit = true;
            node.BeginEdit();
        }

        private void cm_showSubtopics_Click(object sender, EventArgs e)
        {
            Topic t = GetTopic();

            if (t == null) return;

            TreeNode node = treeView1.SelectedNode;
            node.Nodes.Clear();

            foreach (Topic _t in t.SubTopics)
                node.Nodes.Add(_t.Text).Tag = _t.Guid;

            node.Expand();
        }

        private void TreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true; return;
            }

            TreeNode node = treeView1.SelectedNode;
            if (node.Parent == null) // map selected
            {
                using (StixDB db = new StixDB("SendToMap"))
                {
                    db.ExecuteNonQuery("update SENDTOMAP set mapname=`" + e.Label +
                        "` where mappath=`" + node.Tag.ToString() + "`");
                }
            }
            else
            {
                Topic t = GetTopic();
                if (t != null)
                    t.Text = e.Label;
                t = null;
            }
            treeView1.LabelEdit = false;
        }
        #endregion

        #region Manage Panel

        private void subtopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            GetOptions();
            Topic t = GetTopic(); if (t == null) return;
            t.SelectOnly();

            StixTextOps.SendToMap.Clear();

            StixTextOps.SelectedTopics.Clear(); StixTextOps.TopicsToAdd.Clear();
            StixTextOps.SelectedTopics.AddRange(new List<Topic> { t });

            StixTextOps.paste_success = false;
            StixMain.m_stixText.PasteTopic("subtopic", true, t.Document);

            if (endSubtopic_Click)
                EndSubtopic_Click();
        }
        public static bool endSubtopic_Click = true;

        public void EndSubtopic_Click()
        {
            // Show added topics
            TreeNode node = treeView1.SelectedNode;

            foreach (Topic _t in StixTextOps.SendToMap)
            {
                if (_t.IsValid)
                    node.Nodes.Add(_t.Text).Tag = _t.Guid;
            }
            StixTextOps.SendToMap.Clear();

            if (!node.IsExpanded && node.Nodes.Count > 0) node.Expand();

            endSubtopic_Click = true;
            ShowSuccess(StixTextOps.paste_success);
        }

        private void PasteLink_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Topic t = GetTopic();

                if (t != null && Clipboard.ContainsText())
                {
                    try
                    {
                        t.Hyperlinks.AddHyperlink(Clipboard.GetText());
                        ShowSuccess(true);
                    }
                    catch { ShowSuccess(false); }
                }
            }
            else if (e.Button == MouseButtons.Right) // populate OmniLinks groups menu
            {
                cmsOmniLinks.Items.Clear();
                ToolStripItem tsi = new ToolStripLabel(Utils.getString("SendToMapDlg.sendtoomilinks"));
                tsi.Font = new Font(cmsOmniLinks.Font, FontStyle.Bold);
                cmsOmniLinks.Items.Add(tsi);

                using (StixDB db = new StixDB("Links"))
                {
                    DataTable dt = db.ExecuteQuery("select * from LINKGROUPS where parentID = 0 order by _order");

                    foreach (DataRow dr in dt.Rows)
                    {
                        tsi = cmsOmniLinks.Items.Add(dr["name"].ToString());
                        tsi.Tag = Convert.ToInt32(dr["id"]);
                        tsi.Click += LinkGroup_Click;
                        PopRec(tsi, db);
                    }
                }
                cmsOmniLinks.Show(MousePosition);
            }
        }

        private void LinkGroup_Click(object sender, EventArgs e)
        {
            if (Utils.FreeVersionLimitExceeded("links")) return;
            int groupID = (int)(sender as ToolStripItem).Tag;

            if (StixMain.m_NewLink == null || StixMain.m_NewLink.IsDisposed)
            {
                StixMain.m_NewLink = new NewLinkDlg();

                StixMain.m_NewLink.Location = new Point(this.Right, this.Top);
                Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;
                if (StixMain.m_NewLink.Right > area.Right) // close to the right
                    StixMain.m_NewLink.Location = new Point(this.Left - StixMain.m_NewLink.Width, this.Top);
                if (StixMain.m_NewLink.Left < area.Left) // close to the left and right, center it
                    StixMain.m_NewLink.StartPosition = FormStartPosition.CenterScreen;

                StixMain.m_NewLink.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }

            StixMain.m_NewLink.from = "InfoPicker";
            StixMain.m_NewLink.newLink = true;
            StixMain.m_NewLink.txtLink.Text = Clipboard.GetText(TextDataFormat.UnicodeText);
            StixMain.m_NewLink.txtTitle.Text = "";
            StixMain.m_NewLink.SelectGroup(groupID);
            StixMain.m_NewLink.txtLink_KeyUp(null, null);
        }

        void PopRec(ToolStripItem tsi, StixDB db)
        {
            int parent = Convert.ToInt32(tsi.Tag); // Tag = link id

            DataTable dt = db.ExecuteQuery("select * from LINKGROUPS where parentID=" + parent +
                " order by _order");

            foreach (DataRow dr in dt.Rows)
            {
                ToolStripDropDown tsdd = (tsi as ToolStripMenuItem).DropDown;
                ToolStripItem _tsi = tsdd.Items.Add(dr["name"].ToString());
                _tsi.Tag = Convert.ToInt32(dr["id"]);
                _tsi.Click += LinkGroup_Click;
                PopRec(_tsi, db);
            }
        }

        private void PasteNotes_MouseClick(object sender, MouseEventArgs e)
        {
            Topic t = GetTopic();
            if (t == null) return;

            StixTextOps.SelectedTopics.Clear();
            StixTextOps.SelectedTopics.AddRange(new List<Topic> { t });

            GetOptions();
            StixMain.m_stixText.ProcessTopicNotes(true, t.Document);
            ShowSuccess(StixTextOps.pastetext);
        }

        public void ShowSuccess(bool success)
        {
            panelSuccess.Visible = true;
            if (success)
            {
                pSuccess.Image = Image.FromFile(Utils.ImagesPath + "like.png");
                lblSuccessMessage.Text = Utils.getString("SendToMapDlg.lblSuccessMessage");
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                pSuccess.Image = Image.FromFile(Utils.ImagesPath + "fail.png");
                lblSuccessMessage.Text = Utils.getString("SendToMapDlg.lblSuccessMessage_fail");
            }

            panelSuccess.Refresh();
            StixTextOps.paste_success = false;
            if (success)
                timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            panelSuccess.Visible = false;
        }

        void GetOptions()
        {
            StixTextOps.op_formatted = OptionTextFormat.Tag.ToString() == tag_formatted;
            StixTextOps.op_replace = OptionReplaceInsert.Tag.ToString() == "textadd";
            StixTextOps.op_single = OptionMultipleTopics.Tag.ToString() == tag_single;
            StixTextOps.op_source = OptionSourceLink.Tag.ToString() != tag_sourceno;
            StixTextOps.op_links = OptionInternalLinks.Tag.ToString() == tag_linksyes;

            StixTextOps.op_highlightlinks = OptionTextFormat.Tag.ToString() == tag_unformatted_links;
            StixTextOps.op_sourceonfirst = OptionSourceLink.Tag.ToString() == tag_source_first;
        }

        private void OptionButton_MouseClick(object sender, MouseEventArgs e)
        {
            StixMain.m_stixText.OptionButton_MouseClick(sender, e);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            cmsManage.Show(MousePosition);
        }

        #endregion

        #region Hide MindManager
        private void MM_Click(object sender, EventArgs e)
        {
            if (MMHidden) // Restore MM
            {
                toolTip1.SetToolTip(MM, Utils.getString("SendToMapDlg.MM.tooltip"));
                MMHidden = false; MMHidding = false;
                MM.Image = Image.FromFile(Utils.m_imagesPath + "MindManager.png");

                if (MMMaximized)
                    MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateMaximize;
                else
                {
                    MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateNormal;
                    MMUtils.MindManager.Top = MMTop; MMUtils.MindManager.Left = MMLeft;
                    MMUtils.MindManager.Width = MMWIdth; MMUtils.MindManager.Height = MMHeight;
                }
            }
            else // Hide MM behind this dialog
            {
                toolTip1.SetToolTip(MM, Utils.getString("SendToMapDlg.MM.restore"));
                MM.Image = Image.FromFile(Utils.m_imagesPath + "MindManager_hide.png");

                if (MMUtils.MindManager.WindowState == MmWindowState.mmWindowStateMaximize)
                    MMMaximized = true;
                else
                    MMMaximized = false; // normal

                MMHidden = true; MMHidding = true;

                // Save MM bounds
                MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateNormal;
                if (MMUtils.MindManager.Top != this.Top ||
                    MMUtils.MindManager.Width != this.Width - MM.Width)
                {
                    MMWIdth = MMUtils.MindManager.Width; MMHeight = MMUtils.MindManager.Height;
                    MMTop = MMUtils.MindManager.Top; MMLeft = MMUtils.MindManager.Left;

                    MMUtils.MindManager.Top = this.Top;
                    MMUtils.MindManager.Left = this.Left + (MM.Width / 2);
                    MMUtils.MindManager.Width = this.Width - MM.Width;
                    MMUtils.MindManager.Height = this.Height - MM.Height;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SendToMapDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            RestoreMM();

            foreach (Document doc in MMUtils.MindManager.AllDocuments)
            {
                doc.Save();

                if (!doc.Window.IsVisible) {
                    Thread.Sleep(100); doc.Close(); }
            }
        }

        void RestoreMM()
        {
            if (MMHidden) // Restore MM
            {
                MMHidden = false; MMHidding = false;

                if (MMMaximized)
                    MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateMaximize;
                else
                {
                    MMUtils.MindManager.WindowState = MmWindowState.mmWindowStateNormal;
                    MMUtils.MindManager.Top = MMTop; MMUtils.MindManager.Left = MMLeft;
                    MMUtils.MindManager.Width = MMWIdth; MMUtils.MindManager.Height = MMHeight;
                }
            }
        }
        #endregion

        public static bool MMHidden = false, MMHidding = false;
        bool MMMaximized = false;
        public static int MMWIdth, MMHeight, MMTop, MMLeft;

        private void txtAddTopic_Leave(object sender, EventArgs e)
        {
            if (txtAddTopic.Text.Trim() == "")
            {
                txtAddTopic.ForeColor = SystemColors.GrayText;
                txtAddTopic.Text = Utils.getString("SendToMapDlg.txtAddTopic");
            }
        }

        private void txtAddTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtAddTopic.ForeColor == SystemColors.GrayText)
            {
                txtAddTopic.ForeColor = SystemColors.WindowText;
                txtAddTopic.Text = "";
            }
        }

        private void SendToMapDlg_Load(object sender, EventArgs e)
        {
            WindowExpanded = this.Bounds;
            WindowCollapsed = new Rectangle(this.Location, panelMinimized.Size);
        }
        Rectangle WindowExpanded;
        Rectangle WindowCollapsed;

        private void SendToMapDlg_ResizeEnd(object sender, EventArgs e)
        {
            if (MMHidden)
            {
                MMUtils.MindManager.Top = this.Top;
                MMUtils.MindManager.Left = this.Left + (MM.Width / 2);
            }

            if (this.Height > panelMinimized.Height)
                WindowExpanded = this.Bounds;
            else
                WindowCollapsed = this.Bounds;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            const int WM_NCLBUTTONDBLCLK = 0x00A3;    // this constant int is different

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                if (this.Height <= panelMinimized.Height + 10)
                    this.Bounds = WindowExpanded;
                else
                {
                    this.Bounds = WindowCollapsed;
                    SendToMapDlg_FormClosing(null, null); // restore MM if hidden
                }

                this.OnResizeEnd(EventArgs.Empty);
            }
        }

        private void txtAddTopic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAddTopic.Text.Trim() == "") return;

                TreeNode node = treeView1.SelectedNode;

                if (node == null || (node.Parent == null && node.Tag.ToString() != "selected"))
                {
                    return;  // map selected or nothing selected
                }
                Topic t = GetTopic();

                if (t == null) return;

                t = t.AddSubTopic(txtAddTopic.Text);
                t.SelectOnly();
                TreeNode _node = node.Nodes.Add(t.Text);
                _node.Tag = t.Guid;
                treeView1.SelectedNode = _node;
                treeView1.Select();

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        public static List<Topic> SelectedTopics = new List<Topic>();

        public const string
            tag_formatted = "formatted",
            tag_unformatted = "unformatted",
            tag_unformatted_links = "unformatted_links",
            tag_textreplace = "textreplace",
            tag_textadd = "textadd",
            tag_single = "single",
            tag_multiple = "multiple",
            tag_sourceyes = "sourceyes",
            tag_sourceno = "sourceno",
            tag_source_first = "source_first",
            tag_linksyes = "linksyes",
            tag_linksno = "linksno";

        Dictionary<string, Dictionary<string, XMLTopicCompanion>> SendToMaps = new Dictionary<string, Dictionary<string, XMLTopicCompanion>>();
    }
}