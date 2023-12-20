using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Bubbles
{
    internal enum LabelEditType
    {
        EDIT_DISABLED,
        EDIT_NEW,
        EDIT_RENAME
    }

    internal partial class ResourcesMMDlg : Form
    {
        public ResourcesMMDlg()
        {
            InitializeComponent();

            // Assign default tags
            Font _nodeFont = treeView1.Font;
            int _fontHeight = Convert.ToInt32(Math.Floor(_nodeFont.GetHeight()));
            treeView1.ItemHeight = _fontHeight + 8;

            treeView1.Nodes[0].Tag = new CLNodeTag(CLNodeType.NODE_ROOT, CLNodeArea.NODE_RESOURCES);
            treeView1.Nodes[1].Tag = new CLNodeTag(CLNodeType.NODE_ROOT, CLNodeArea.NODE_TAGS);

            // Treeview
            treeView1.Nodes[0].Text = Utils.getString("commonlists.panel.resources.caption") + "  ";
            treeView1.Nodes[1].Text = Utils.getString("commonlists.panel.tags.caption") + "  ";

            // Panel
            AddNewTagGroup.Text = Utils.getString("commonlists.panel.addnewtaggroup.caption");
            toolTip1.SetToolTip(AddNewTagGroup, Utils.getString("commonlists.panel.addnewtaggroup.tooltip"));
            AddNewResourcesGroup.Text = Utils.getString("commonlists.panel.addnewresourcesgroup.caption");
            toolTip1.SetToolTip(AddNewResourcesGroup, Utils.getString("commonlists.panel.addnewresourcesgroup.tooltip"));
            ManageResources.Text = Utils.getString("commonlists.panel.ManageResources.caption");
            CheckMap.Text = Utils.getString("commonlists.panel.CheckMap.caption");
            toolTip1.SetToolTip(CheckMap, Utils.getString("commonlists.panel.CheckMap.tooltip"));
            lblSearch.Text = Utils.getString("commonlists.panel.lblSearch.caption");
            toolTip1.SetToolTip(btnNext, "Enter or Right arrow key");
            toolTip1.SetToolTip(btnPrev, "Shift+Enter or Left arrow key");

            // Context menu
            // ROOT
            contextMenuStrip1.Items[0].Text = Utils.getString("commonlists.panel.contextmenu.newresourcesgroup.caption");
            contextMenuStrip1.Items[1].Text = Utils.getString("commonlists.panel.contextmenu.newtaggroup.caption");
            contextMenuStrip1.Items[2].Text = Utils.getString("commonlists.panel.contextmenu.showall.caption");
            contextMenuStrip1.Items[3].Text = Utils.getString("commonlists.panel.contextmenu.collapseall.caption");
            contextMenuStrip1.Items[15].Text = Utils.getString("commonlists.panel.contextmenu.addalltomap.caption");
            // GROUP
            contextMenuStrip1.Items[4].Text = Utils.getString("commonlists.panel.contextmenu.newresource.caption");
            contextMenuStrip1.Items[5].Text = Utils.getString("commonlists.panel.contextmenu.newtag.caption");
            // GROUP or ENTRY
            contextMenuStrip1.Items[6].Text = Utils.getString("commonlists.panel.contextmenu.rename.caption");
            contextMenuStrip1.Items[7].Text = Utils.getString("commonlists.panel.contextmenu.menuDeleteGroup.caption");
            contextMenuStrip1.Items[8].Text = Utils.getString("commonlists.panel.contextmenu.menuDelete.caption");
            contextMenuStrip1.Items[9].Text = Utils.getString("commonlists.panel.contextmenu.addtomap.caption");
            contextMenuStrip1.Items[10].Text = Utils.getString("commonlists.panel.contextmenu.mutuallyexclusive.caption");
            // ENTRY
            contextMenuStrip1.Items[11].Text = Utils.getString("commonlists.panel.contextmenu.menuResourceCard.caption");
            contextMenuStrip1.Items[12].Text = Utils.getString("commonlists.panel.contextmenu.color.caption");
            contextMenuStrip1.Items[13].Text = Utils.getString("commonlists.panel.contextmenu.menuAddToTopic.caption");

            contextMenuStrip1.Items[14].Text = Utils.getString("commonlists.panel.contextmenu.import.caption");

            contextMenuStrip1.Items[17].Text = Utils.getString("button.copy");
            contextMenuStrip1.Items[18].Text = Utils.getString("button.cut");
            contextMenuStrip1.Items[19].Text = Utils.getString("button.paste");

            menuDeleteGroup.ToolTipText = Utils.getString("commonlists.panel.menuDeleteGroup.tooltip");
            menuAddToMap.ToolTipText = Utils.getString("commonlists.panel.menuAddToMap.tooltip");
            menuAddAllToMap.ToolTipText = Utils.getString("commonlists.panel.menuAddAllToMap.tooltip");

            helpProvider1.HelpNamespace = MMUtils.getDLLPath() + "multimaps.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "manage_lists.htm");

            imageList1.ImageSize = imageSize.Size;
            System.Drawing.Image img1 = System.Drawing.Image.FromFile(MMUtils.getDLLPath() + "images\\resource.png");
            System.Drawing.Image img2 = System.Drawing.Image.FromFile(MMUtils.getDLLPath() + "images\\tag.png");
            img1 = new Bitmap(img1, imageSize.Size);
            img2 = new Bitmap(img2, imageSize.Size);
            imageList1.Images.Add(img1);
            imageList1.Images.Add(img2);

            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = false;

            contextMenuStrip1.Closed += ContextMenuStrip1_Closed;

            txtSearch.KeyUp += TxtSearch_KeyUp;
            _originalClip = Cursor.Clip;
        }

        private void ContextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = false;
        }

        public void Init()
        {
            FoundNodes.Clear();
            searchedText = "";

            using (CLDatabaseResources db = new CLDatabaseResources())
            {
                // delete incorrect resource entries
                db.ExecuteNonQuery("delete from RES where groupID='0' and ID='copy'");

                // Fill Resources.
                treeView1.Nodes[0].Nodes.Clear();

                // Get groups
                DataTable _resFolders = db.ExecuteQuery("select * from RESGROUPS order by orderID");
                if (_resFolders != null && _resFolders.Rows.Count > 0)
                {
                    for (int i = 0; i < _resFolders.Rows.Count; i++)
                    {
                        DataRow _resFolder = _resFolders.Rows[i];

                        CLNodeTag _tag = new CLNodeTag(CLNodeType.NODE_GROUP, CLNodeArea.NODE_RESOURCES)
                        {
                            orderID = Convert.ToInt32(_resFolder["orderID"]),
                            ID = _resFolder["ID"].ToString()
                        };

                        TreeNode _node = new TreeNode(_resFolder["name"].ToString())
                        {
                            Tag = _tag,
                            ForeColor = System.Drawing.Color.RoyalBlue
                        };
                        _node.NodeFont = new Font(_node.NodeFont ?? treeView1.Font, FontStyle.Bold);
                        treeView1.Nodes[0].Nodes.Add(_node);

                        // Get items for this group (only those with ID = "copy"!)
                        DataTable _resItems = db.ExecuteQuery("select * from RES where groupID=`" + _resFolder["ID"].ToString() + "` order by orderID");
                        if (_resItems != null && _resItems.Rows.Count > 0)
                        {
                            for (int j = 0; j < _resItems.Rows.Count; j++)
                            {
                                DataRow _resItemRow = _resItems.Rows[j];

                                CLNodeTag _tag2 = new CLNodeTag(CLNodeType.NODE_ENTRY, CLNodeArea.NODE_RESOURCES)
                                {
                                    orderID = Convert.ToInt32(_resItemRow["orderID"]),
                                    groupID = _resItemRow["groupID"].ToString(),
                                };

                                // Get original resource entry
                                DataTable originalItem = db.ExecuteQuery("select * from RES where " +
                                    "resName=`" + _resItemRow["resName"].ToString() + "` and groupID='0'");

                                if (originalItem != null && originalItem.Rows.Count > 0)
                                {
                                    _tag2.tagColor = originalItem.Rows[0]["reserved1"].ToString();
                                    _tag2.ID = originalItem.Rows[0]["ID"].ToString();

                                    TreeNode _node2 = new TreeNode(_resItemRow["resName"].ToString())
                                    {
                                        Tag = _tag2,
                                    };
                                    try { _node2.BackColor = ColorTranslator.FromHtml(_tag2.tagColor); }
                                    catch { }
                                    _node.Nodes.Add(_node2);
                                }
                                else
                                {
                                    MMBase.TRACE("commonlists original Item not found"); // todo debug
                                }
                            }
                        }
                    }
                }
                else
                {
                    MMBase.TRACE("commonlists _resFolders = 0"); // todo debug
                }
            }
            using (CLDatabaseTags db = new CLDatabaseTags())
            {
                // Fill Tags.
                treeView1.Nodes[1].Nodes.Clear();

                // Get groups
                DataTable _tagFolders = db.ExecuteQuery("select * from TAGGROUPS order by orderID");
                if (_tagFolders != null && _tagFolders.Rows.Count > 0)
                {
                    for (int i = 0; i < _tagFolders.Rows.Count; i++)
                    {
                        DataRow _tagRow = _tagFolders.Rows[i];

                        CLNodeTag _tag = new CLNodeTag(CLNodeType.NODE_GROUP, CLNodeArea.NODE_TAGS)
                        {
                            orderID = Convert.ToInt32(_tagRow["orderID"]),
                            ID = _tagRow["ID"].ToString()
                        };
                        try { _tag.mutex = Convert.ToBoolean(_tagRow["mutex"]); } catch { _tag.mutex = false; }

                        TreeNode _node = new TreeNode(_tagRow["name"].ToString())
                        {
                            Tag = _tag,
                            ForeColor = System.Drawing.Color.RoyalBlue
                        };
                        _node.NodeFont = new Font(_node.NodeFont ?? treeView1.Font, FontStyle.Bold);

                        treeView1.Nodes[1].Nodes.Add(_node);

                        // Get items for this group
                        DataTable _tagItems = db.ExecuteQuery("select * from TAGS where groupID=`" + _tagRow["ID"].ToString() + "` order by orderID");
                        if (_tagFolders != null && _tagFolders.Rows.Count > 0)
                        {
                            for (int j = 0; j < _tagItems.Rows.Count; j++)
                            {
                                DataRow _tagItemRow = _tagItems.Rows[j];

                                CLNodeTag _tag2 = new CLNodeTag(CLNodeType.NODE_ENTRY, CLNodeArea.NODE_TAGS)
                                {
                                    orderID = Convert.ToInt32(_tagItemRow["orderID"]),
                                    groupID = _tagItemRow["groupID"].ToString(),
                                    tagColor = _tagItemRow["tagColor"].ToString(),
                                    ID = _tagItemRow["ID"].ToString()
                                };

                                TreeNode _node2 = new TreeNode(_tagItemRow["tagName"].ToString())
                                {
                                    Tag = _tag2,
                                };
                                try { _node2.BackColor = ColorTranslator.FromHtml(_tag2.tagColor); }
                                catch { }
                                _node.Nodes.Add(_node2);
                            }
                        }
                    }
                }
                else
                {
                    MMBase.TRACE("commonlists _tagFolders = 0"); // todo debug
                }
            }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_editType != LabelEditType.EDIT_DISABLED)
                AcceptAddEditNode();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selectedNode = e.Node;

            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;

                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                CLNodeTag _tag = (CLNodeTag)e.Node.Tag;

                if (_tag.type == CLNodeType.NODE_GROUP)
                {
                    // group node
                    contextMenuStrip1.Items["menuRename"].Visible = true;
                    contextMenuStrip1.Items["menuDeleteGroup"].Visible = true;
                    contextMenuStrip1.Items["menuImport"].Visible = _tag.area == CLNodeArea.NODE_RESOURCES;
                    contextMenuStrip1.Items["menuNewResource"].Visible = _tag.area == CLNodeArea.NODE_RESOURCES;
                    contextMenuStrip1.Items["menuAddToMap"].Visible = MMUtils.ActiveDocument != null;
                    contextMenuStrip1.Items["menuSeparator"].Visible = true;
                    contextMenuStrip1.Items["menuPaste"].Visible = true;
                    contextMenuStrip1.Items["menuPaste"].Enabled = CopiedItem != null;

                    if (_tag.area == CLNodeArea.NODE_TAGS)
                    {
                        contextMenuStrip1.Items["menuNewTag"].Visible = true;
                        contextMenuStrip1.Items["menuMutuallyExclusive"].Visible = true;
                        menuMutuallyExclusive.Checked = _tag.mutex;
                    }
                }
                else if (_tag.type == CLNodeType.NODE_ENTRY)
                {
                    if (MMUtils.ActiveDocument != null && MMUtils.ActiveDocument.Selection.HasTopic)
                    {
                        menuAddToTopic.Enabled = true;
                        bool _toggle = true;
                        if (_tag.area == CLNodeArea.NODE_RESOURCES)
                        {
                            foreach (Topic _topic in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                                _toggle &= MMUtils.TopicHasResource(_topic, e.Node.Text);

                            foreach (Topic _topic in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                            {
                                if (MMUtils.TopicHasResource(_topic, e.Node.Text) && _toggle)
                                    contextMenuStrip1.Items[13].Text =
                                        Utils.getString("commonlists.panel.contextmenu.menuRemoveFromTopic.caption");
                                else
                                    contextMenuStrip1.Items[13].Text =
                                        Utils.getString("commonlists.panel.contextmenu.menuAddToTopic.caption");
                            }
                        }
                        else // tag item
                        {
                            foreach (Topic _topic in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                                _toggle &= MapMarkers.TopicHasTag(_topic, e.Node.Parent.Text, _tag.groupID, e.Node.Text);

                            foreach (Topic _topic in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                            {
                                if (MapMarkers.TopicHasTag(_topic, e.Node.Parent.Text, _tag.groupID, e.Node.Text) && _toggle)
                                    contextMenuStrip1.Items[13].Text =
                                        Utils.getString("commonlists.panel.contextmenu.menuRemoveFromTopic.caption");
                                else
                                    contextMenuStrip1.Items[13].Text =
                                        Utils.getString("commonlists.panel.contextmenu.menuAddToTopic.caption");
                            }
                        }
                    }
                    else
                    {
                        menuAddToTopic.Enabled = false;
                        contextMenuStrip1.Items[13].Text =
                            Utils.getString("commonlists.panel.contextmenu.menuAddToTopic.caption");
                    }

                    contextMenuStrip1.Items["menuRename"].Visible = true;
                    contextMenuStrip1.Items["menuDelete"].Visible = true;
                    contextMenuStrip1.Items["menuAddToMap"].Visible = MMUtils.ActiveDocument != null;
                    contextMenuStrip1.Items["menuResourceCard"].Visible = _tag.area == CLNodeArea.NODE_RESOURCES;
                    contextMenuStrip1.Items["menuAddToTopic"].Visible = MMUtils.ActiveDocument != null;
#if !MINDJET20                    
                    contextMenuStrip1.Items["menuColor"].Visible = true;
#endif

                    contextMenuStrip1.Items["menuSeparator"].Visible = true;
                    contextMenuStrip1.Items["menuCopy"].Visible = true;
                    contextMenuStrip1.Items["menuCut"].Visible = true;
                    contextMenuStrip1.Items["menuPaste"].Visible = true;
                    contextMenuStrip1.Items["menuPaste"].Enabled = CopiedItem != null;
                }
                else if (_tag.type == CLNodeType.NODE_ROOT)
                {
                    // Root node
                    contextMenuStrip1.Items["menuNewResourceGroup"].Visible = _tag.area == CLNodeArea.NODE_RESOURCES;
                    contextMenuStrip1.Items["menuNewTagGroup"].Visible = _tag.area == CLNodeArea.NODE_TAGS;
                    contextMenuStrip1.Items["menuImport"].Visible = _tag.area == CLNodeArea.NODE_TAGS;
                    contextMenuStrip1.Items["menuShowAll"].Visible = true;
                    contextMenuStrip1.Items["menuCollapseAll"].Visible = true;
                    contextMenuStrip1.Items["menuAddAllToMap"].Visible = MMUtils.ActiveDocument != null;
                }
            } // right button
            else if (e.Button == MouseButtons.Left)
            {
                treeView1.SelectedNode = e.Node;
                CLNodeTag _tag = (CLNodeTag)e.Node.Tag;
                if (_tag.type == CLNodeType.NODE_ENTRY)
                {
                    treeView1_NodeMouseDoubleClickInternal(e.Node);
                }
                else
                {
                    m_bGotKeydown = !m_bStateChanged;
                }
            }
        }

        private void treeView1_NodeMouseDoubleClickInternal(TreeNode node)
        {
            TreeNode _node = node;
            CLNodeTag _tag = (CLNodeTag)_node.Tag;
            if (_tag == null || _tag.type == CLNodeType.NODE_ROOT || _tag.type == CLNodeType.NODE_GROUP)
                return; // Double click at area name or group name
            if (MMUtils.ActiveDocument == null || !MMUtils.ActiveDocument.Selection.HasTopic)
                return; // No active document or no topics selected
            if (_tag.area == CLNodeArea.NODE_RESOURCES)
            {
                bool _toggle = true;
                foreach (Topic _topic in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    _toggle &= MMUtils.TopicHasResource(_topic, _node.Text);

                using (CLDatabaseResources db = new CLDatabaseResources())
                {
                    foreach (Topic _topic in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        if (MMUtils.TopicHasResource(_topic, _node.Text) && _toggle)
                            MMUtils.RemoveResourceFromTopic(_topic, _node.Text);
                        else
                        {
                            MapMarkerGroup _mmg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                            if (_mmg == null || !_mmg.IsValid)
                                continue;

                            // if group doesn't have this tag, add it to group
                            if (!MapMarkers.AddTextLabelMarkerToGroup(_mmg, _node.Text, _tag.ID, _tag.tagColor, true, true))
                                continue;

                            MMUtils.AddResourceToTopic(_topic, _node.Text);

                            MMUtils.ActiveDocument.Calendar.GetResourceRate(_node.Text, out double rate, out MmRateUnit rateUnit);

                            DataTable dt = db.ExecuteQuery("select * from RES where resName ='" + _node.Text +
                                "' and ID <> 'copy'");

                            if (dt.Rows.Count <= 0) continue;

                            try
                            {
                                if (Convert.ToInt32(dt.Rows[0]["rate"]) != 0 && rate == 0)
                                {
                                    switch (Convert.ToInt32(dt.Rows[0]["rateUnit"]))
                                    {
                                        case 0:
                                            rateUnit = MmRateUnit.mmRateUnitHour;
                                            break;
                                        case 1:
                                            rateUnit = MmRateUnit.mmRateUnitDay;
                                            break;
                                        case 2:
                                            rateUnit = MmRateUnit.mmRateUnitWeek;
                                            break;
                                        case 3:
                                            rateUnit = MmRateUnit.mmRateUnitUsage;
                                            break;
                                    }
                                    MMUtils.ActiveDocument.Calendar.SetResourceRate(_node.Text,
                                        Convert.ToInt32(dt.Rows[0]["rate"]), rateUnit);
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            else if (_tag.area == CLNodeArea.NODE_TAGS)
            {
                string tagGroupName = _node.Parent.Text;
                string tagName = _node.Text;
                CLNodeTag nodeGroup = (CLNodeTag)_node.Parent.Tag;
                CLNodeTag nodeTag = (CLNodeTag)_node.Tag;

                bool toggle = true;
                foreach (Topic _topic in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    toggle &= MapMarkers.TopicHasTag(_topic, tagGroupName, nodeTag.groupID, tagName);

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    try
                    {
                        if (MapMarkers.TopicHasTag(t, tagGroupName, nodeTag.groupID, tagName) && toggle)
                            MapMarkers.RemoveTagFromTopic(t, tagGroupName, nodeTag.groupID, tagName);
                        else
                            MapMarkers.AddTagToTopic(t, tagName, nodeTag.ID, tagGroupName, nodeTag.groupID, _tag.tagColor, nodeGroup.mutex);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Add resources from database
        /// </summary>
        /// <param name="resources">selected in db resources</param>
        public static void AddResources(List<string> resources)
        {
            CLNodeTag _tag;
            try
            {
                _tag = (CLNodeTag)selectedNode.Tag; // resource group

                if (_tag.area != CLNodeArea.NODE_RESOURCES || _tag.type != CLNodeType.NODE_GROUP)
                {
                    MessageBox.Show(Utils.getString("commonlists.addresources.error"),
                        Utils.getString("commonlists.addresources.error.caption"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show(Utils.getString("commonlists.addresources.error"),
                        Utils.getString("commonlists.addresources.error.caption"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (CLDatabaseResources db = new CLDatabaseResources())
            {
                foreach (string res in resources)
                {
                    bool found = false;
                    foreach (TreeNode __node in selectedNode.Nodes)
                        if (__node.Text == res)
                        {
                            found = true;
                            break;
                        }
                    if (found) continue; // resource exists in this group

                    // Add node to this treeview
                    CLNodeTag tag = new CLNodeTag(CLNodeType.NODE_ENTRY, CLNodeArea.NODE_RESOURCES)
                    {
                        groupID = _tag.ID,
                        orderID = selectedNode.Nodes.Count + 1
                    };

                    TreeNode node = new TreeNode(res)
                    {
                        Tag = tag
                    };
                    selectedNode.Nodes.Add(node);

                    // Add copy to db
                    db.AddResourceCopy(tag.groupID, res, tag.orderID);
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            TreeNode _clickedNode = treeView1.SelectedNode;
            CLNodeTag _tag = (CLNodeTag)treeView1.SelectedNode.Tag;
            contextMenuStrip1.Close();

            if (e.ClickedItem.Name == "menuAddToTopic")
            {
                treeView1_NodeMouseDoubleClickInternal(_clickedNode);
            }
            else if (e.ClickedItem.Name == "menuResourceCard")
            {
                using (ResourceCardDlg dlg = new ResourceCardDlg(_clickedNode.Text))
                {
                    dlg.ShowDialog();
                    if (dlg.DialogResult == DialogResult.OK)
                    {
                        if (dlg.name != _clickedNode.Text)
                            UpdateResources(_clickedNode.Text, dlg.name);
                    }
                }
            }
            else if (e.ClickedItem.Name == "menuColor")
            {
                // Show tip if message not disabled 
                if (MMUtils.getRegistry("", "ColorTip", "0") == "0")
                {
                    using (TipForColorDlg dlg = new TipForColorDlg())
                        dlg.ShowDialog();
                }

                colorDialog1.FullOpen = true;
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                System.Drawing.Color c = colorDialog1.Color;
                _tag.tagColor = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();

                if (_tag.tagColor == "#ffffffff") // white color
                    _tag.tagColor = "";
                _clickedNode.BackColor = c;

                int density = (int)Math.Sqrt(
                    c.R * c.R * .299 +
                    c.G * c.G * .587 +
                    c.B * c.B * .114);

                if (density < 140)
                    _clickedNode.ForeColor = SystemColors.Control;
                else
                    _clickedNode.ForeColor = SystemColors.WindowText;

                if (_tag.area == CLNodeArea.NODE_TAGS)
                {
                    using (CLDatabaseTags db = new CLDatabaseTags())
                    {
                        db.ExecuteNonQuery("update TAGS set tagColor=`" + _tag.tagColor +
                            "` where groupID=`" + _tag.groupID + "` and tagName=`" + _clickedNode.Text + "`");
                    }
                }
                else if (_tag.area == CLNodeArea.NODE_RESOURCES)
                {
                    using (CLDatabaseResources db = new CLDatabaseResources())
                    {
                        db.ExecuteNonQuery("update RES set reserved1=`" + _tag.tagColor + "` where resName=`" + _clickedNode.Text + "` and ID <> 'copy'");
                    }

                    // Update resources color in the duplicate entries.
                    UpdateDupesColor(_clickedNode.Text, _tag.tagColor);
                }
            }
            else if (e.ClickedItem.Name == "menuAddFromDB")
            {
                if (!MMCommonLists.m_resourcesDlg.Visible)
                {
                    MMCommonLists.m_resourcesDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }
                else
                {
                    MMCommonLists.m_resourcesDlg.WindowState = FormWindowState.Normal;
                    MMCommonLists.m_resourcesDlg.Focus();
                }
            }
            else if (e.ClickedItem.Name == "menuAddToMap" || e.ClickedItem.Name == "menuAddAllToMap")
            {
                MapMarkerGroup _mmg;

                if (_tag.area == CLNodeArea.NODE_RESOURCES)
                {
                    _mmg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                    if (_mmg == null || !_mmg.IsValid)
                        return;

                    // Add selected resource to Map Resources group.
                    if (_tag.type == CLNodeType.NODE_ENTRY)
                    {
                        MapMarkers.AddTextLabelMarkerToGroup(_mmg, _clickedNode.Text, _tag.ID, _tag.tagColor, true, true);
                    }
                    // Add all resources from selected group to Map Resources group.
                    else if (_tag.type == CLNodeType.NODE_GROUP)
                    {
                        foreach (TreeNode _node in _clickedNode.Nodes)
                            _mmg.AddResourceMarker(_node.Text);
                    }
                    // Add all resources from all groups to Map Resources group.
                    else if (_tag.type == CLNodeType.NODE_ROOT)
                    {
                        foreach (TreeNode node in _clickedNode.Nodes)
                        {
                            foreach (TreeNode _node in node.Nodes)
                                _mmg.AddResourceMarker(_node.Text);
                        }
                    }
                }
                else if (_tag.area == CLNodeArea.NODE_TAGS)
                {
                    // Add only one entry to Map Marker Group
                    if (_tag.type == CLNodeType.NODE_ENTRY)
                    {
                        bool mutex = (treeView1.SelectedNode.Parent.Tag as CLNodeTag).mutex;
                        _mmg = MapMarkers.GetMapMarkerGroup(MMUtils.ActiveDocument, _clickedNode.Parent.Text, _tag.groupID, true, mutex);

                        if (!CheckMarkerGroup(_mmg))
                            return;

                        try
                        {
                            MapMarkers.AddTextLabelMarkerToGroup(_mmg, _clickedNode.Text, _tag.ID, _tag.tagColor, true);
                        }
                        catch { }
                    }
                    // Add entire tag group to Map Marker groups
                    else if (_tag.type == CLNodeType.NODE_GROUP)
                    {
                        _mmg = MapMarkers.GetMapMarkerGroup(MMUtils.ActiveDocument, _clickedNode.Text, _tag.ID, true, _tag.mutex);
                        if (!CheckMarkerGroup(_mmg))
                            return;

                        foreach (TreeNode _node in _clickedNode.Nodes)
                        {
                            CLNodeTag __tag = (CLNodeTag)_node.Tag;
                            try
                            {
                                if (!_mmg.IsValid)
                                    _mmg = MapMarkers.GetMapMarkerGroup(MMUtils.ActiveDocument, _clickedNode.Text, _tag.ID, true, _tag.mutex);
                                MapMarkers.AddTextLabelMarkerToGroup(_mmg, _node.Text, __tag.ID, __tag.tagColor, true);
                            }
                            catch (Exception _e) { MMUtils.ErrorToSupport("Error 03. Adding Marker Group." + "\r\n\r\n" + _e.Message); }
                        }
                    }
                    else if (_tag.type == CLNodeType.NODE_ROOT)
                    {
                        foreach (TreeNode node in _clickedNode.Nodes) // groups
                        {
                            CLNodeTag __tag = (CLNodeTag)node.Tag;
                            _mmg = MapMarkers.GetMapMarkerGroup(MMUtils.ActiveDocument, node.Text, __tag.ID, true, __tag.mutex);

                            if (!CheckMarkerGroup(_mmg))
                                return;

                            foreach (TreeNode _node in node.Nodes) // tags
                            {
                                __tag = (CLNodeTag)_node.Tag;
                                try
                                {
                                    if (!_mmg.IsValid)
                                        _mmg = MapMarkers.GetMapMarkerGroup(MMUtils.ActiveDocument, _clickedNode.Text, _tag.ID, true, _tag.mutex);
                                    MapMarkers.AddTextLabelMarkerToGroup(_mmg, _node.Text, __tag.ID, __tag.tagColor, true);
                                }
                                catch { }
                            }
                        }
                    }
                }
            } // end of "Add to map" menu item
            // Import markers
            else if (e.ClickedItem.Name == "menuImport")
            {
                // Make a list of resources already in group (resources) or tag groups 
                List<string> _existingItems = new List<string>();
                foreach (TreeNode _node in _clickedNode.Nodes)
                    _existingItems.Add(_node.Text);
                int orderID = _existingItems.Count + 1;

                // Import resourses to the selected group.
                if (_tag.area == CLNodeArea.NODE_RESOURCES && _tag.type == CLNodeType.NODE_GROUP)
                {
                    using (CLDatabaseResources db = new CLDatabaseResources())
                    {
                        List<string> resourcesInMap = CLImportWizard.ImportResources(_clickedNode.Text);
                        if (resourcesInMap == null || resourcesInMap.Count < 1)
                            return;

                        foreach (string _s in resourcesInMap)
                        {
                            bool copy = false;
                            if (_existingItems.Contains(_s))
                                continue;

                            CLNodeTag _nodeTag = new CLNodeTag(CLNodeType.NODE_ENTRY, _tag.area)
                            {
                                groupID = _tag.ID,
                                orderID = orderID
                            };

                            TreeNode _node = new TreeNode(_s)
                            {
                                Tag = _nodeTag
                            };

                            _clickedNode.Nodes.Add(_node);
                            db.AddResourceCopy(_tag.ID, _s, orderID++);

                            // try to find original item
                            DataTable dt = db.ExecuteQuery("select * from RES where resName=`" + _s + "and ID <> `" + "copy`");

                            if (dt.Rows.Count == 0) // original item not found, add it
                                db.AddResource(MMUtils.ID(), "0", _s, "", "", "", "", 0, 0, "", "", 0);
                        }
                    }
                }
                // Import tags from Tag Group
                else if (_tag.area == CLNodeArea.NODE_TAGS && _tag.type == CLNodeType.NODE_ROOT)
                {
                    using (CLDatabaseTags db = new CLDatabaseTags())
                    {
                        List<MapMarkerGroup> tagGroups = CLImportWizard.ImportTags();
                        if (tagGroups == null || tagGroups.Count < 1)
                            return;

                        TreeNode _node = null;
                        string groupID = "";
                        foreach (MapMarkerGroup mg in tagGroups)
                        {
                            if (_existingItems.Contains(mg.Name)) // tag group
                            {
                                foreach (TreeNode node in treeView1.Nodes[1].Nodes) // tag groups
                                    if (node.Text == mg.Name)
                                    {
                                        _node = node;
                                        CLNodeTag _nodeTag = (CLNodeTag)_node.Tag;
                                        groupID = _nodeTag.ID;
                                        break;
                                    }
                                if (_node == null)
                                    continue;
                            }
                            else // new Group
                            {
                                CLNodeTag _nodeTag = new CLNodeTag(CLNodeType.NODE_GROUP, CLNodeArea.NODE_TAGS)
                                {
                                    orderID = orderID,
                                    mutex = mg.MutuallyExclusive
                                };
                                string _groupid = mg.GetAttributes(MapMarkers.ATTR_MARKERS).GetAttributeValue(MapMarkers.URI_MARKERS);
                                if (String.IsNullOrEmpty(_groupid) || _groupid == "0")
                                {
                                    _nodeTag.ID = MMUtils.ID();
                                    if (!mg.ContainsAttributesNamespace(MapMarkers.outlook_attr))
                                        mg.GetAttributes(MapMarkers.ATTR_MARKERS).SetAttributeValue(MapMarkers.URI_MARKERS, _nodeTag.ID);
                                }
                                else
                                    _nodeTag.ID = _groupid;

                                db.AddGroup(_nodeTag.ID, mg.Name, (_nodeTag.mutex ? 1 : 0), orderID++);

                                _node = new TreeNode(mg.Name)
                                {
                                    Tag = _nodeTag,
                                    ForeColor = System.Drawing.Color.RoyalBlue,
                                    NodeFont = new Font(treeView1.Font, FontStyle.Bold)
                                };
                                groupID = _nodeTag.ID;
                                _clickedNode.Nodes.Add(_node);
                            }

                            List<string> existingTags = new List<string>();
                            foreach (TreeNode __node in _node.Nodes)
                                existingTags.Add(__node.Text);

                            orderID = existingTags.Count + 1;

                            // Add tags to a group.
                            foreach (MapMarker mm in mg)
                            {
                                if (existingTags.Contains(mm.Label))
                                    continue;

                                // Add tag to group
                                string tagColor = "#" + mm.Color.Value.ToString("X");
                                if (tagColor == "#0") tagColor = "";

                                CLNodeTag _nodeTag = new CLNodeTag(CLNodeType.NODE_ENTRY, CLNodeArea.NODE_TAGS)
                                {
                                    orderID = orderID,
                                    tagColor = tagColor,
                                    groupID = groupID
                                };

                                TreeNode _tagNode = new TreeNode(mm.Label)
                                {
                                    Tag = _nodeTag
                                };
                                _node.Nodes.Add(_tagNode);
                                if (tagColor != "")
                                {
                                    try { _tagNode.BackColor = ColorTranslator.FromHtml(tagColor); }
                                    catch { }
                                }

                                string _id = mm.GetAttributes(MapMarkers.ATTR_MARKERS).GetAttributeValue(MapMarkers.URI_MARKERS);
                                if (String.IsNullOrEmpty(_id))
                                {
                                    _nodeTag.ID = MMUtils.ID();
                                    mm.GetAttributes(MapMarkers.ATTR_MARKERS).SetAttributeValue(MapMarkers.URI_MARKERS, _nodeTag.ID);
                                }
                                else
                                    _nodeTag.ID = _id;

                                // Add tag to db
                                db.AddTag(_nodeTag.ID, groupID, mm.Label, tagColor, orderID++);
                            }
                        }
                    }
                }
            } // menu import end
            // Delete group
            else if (e.ClickedItem.Name == "menuDeleteGroup" && _tag.type == CLNodeType.NODE_GROUP)
            {
                // Delete whole group
                DialogResult _rc = MessageBox.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd),
                    Utils.getString("commonlists.confirm.deletegroup.text"),
                    Utils.getString("commonlists.confirm.deletegroup.title"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (_rc == DialogResult.Yes)
                {
                    if (_tag.area == CLNodeArea.NODE_RESOURCES)
                    {
                        using (CLDatabaseResources db = new CLDatabaseResources())
                        {
                            db.ExecuteNonQuery("delete from RESGROUPS where ID=`" + _tag.ID + "`");
                            // delete dublicated resources
                            db.ExecuteNonQuery("delete from RES where groupID=`" + _tag.ID + "` and ID='copy'");
                        }
                    }
                    else
                    {
                        using (CLDatabaseTags db = new CLDatabaseTags())
                        {
                            db.ExecuteNonQuery("delete from TAGGROUPS where ID=`" + _tag.ID + "`");
                            // delete all tags that belongs to this group
                            db.ExecuteNonQuery("delete from TAGS where groupID=`" + _tag.ID + "`");
                        }
                    }

                    TreeNode _parentNode = _clickedNode.Parent;
                    _clickedNode.Remove();
                    treeView1.SelectedNode = _parentNode;
                }
            }
            // Delete - resource or tag
            else if (e.ClickedItem.Name == "menuDelete" && _tag.type == CLNodeType.NODE_ENTRY)
            {
                if (_tag.area == CLNodeArea.NODE_RESOURCES)
                {
                    using (CLDatabaseResources db = new CLDatabaseResources())
                    {
                        // delete resource from db if it is a copy
                        db.ExecuteNonQuery("delete from RES where groupID=`" + _tag.groupID +
                        "` and resName=`" + _clickedNode.Text + "` and ID = 'copy'");
                    }
                }
                else
                {
                    using (CLDatabaseTags db = new CLDatabaseTags())
                    {
                        db.ExecuteNonQuery("delete from TAGS where groupID=`" + _tag.groupID +
                        "` and tagName=`" + _clickedNode.Text + "`");
                    }
                }
                TreeNode _parentNode = _clickedNode.Parent;
                _clickedNode.Remove();
                treeView1.SelectedNode = _parentNode;
            }
            // menu rename
            else if (e.ClickedItem.Name == "menuRename")
            {
                if (_tag.type == CLNodeType.NODE_ROOT)
                    return;

                m_editType = LabelEditType.EDIT_RENAME;
                m_editNode = _clickedNode;
                StartNodeEdit();
            }
            // Add new group or item
            else if (e.ClickedItem.Name.StartsWith("menuNew"))
            {
                AddNewGroupOrItem(_clickedNode);
            }
            // menu mutually exclusive
            else if (e.ClickedItem.Name == "menuMutuallyExclusive")
            {
                if (_tag.area == CLNodeArea.NODE_TAGS && _tag.type == CLNodeType.NODE_GROUP)
                {
                    // check the tag group in the map
                    MapMarkerGroup mg = MapMarkers.GetMapMarkerGroup(MMUtils.ActiveDocument, _clickedNode.Text, _tag.groupID, false);
                    if (mg != null)
                    {
                        if (mg.MutuallyExclusive == _tag.mutex)
                        {
                            try
                            {
                                mg.MutuallyExclusive = !_tag.mutex;
                            }
                            catch
                            { // if MutuallyExclusive is read-only (some topic already has two tags from this group) we can't change this
                                MessageBox.Show(Utils.getString("commonlists.error.mutuallyexclusive"),
                                Utils.getString("commonlists.error.caption"),
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }

                    // change the MutuallyExclusive checked status in the context menu
                    _tag.mutex = !_tag.mutex;
                    menuMutuallyExclusive.Checked = _tag.mutex;
                    int mutex = _tag.mutex == true ? 1 : 0;

                    // update database
                    using (CLDatabaseTags db = new CLDatabaseTags())
                    {
                        db.ExecuteNonQuery("update TAGGROUPS set mutex=" + mutex + " where ID=`" + _tag.ID + "`");
                    }
                }
            }
            else if (e.ClickedItem.Name == "menuCollapseAll")
            {
                _clickedNode.Collapse(false);
            }
            else if (e.ClickedItem.Name == "menuShowAll")
            {
                _clickedNode.ExpandAll();
            }
            else if (e.ClickedItem.Name == "menuCopy")
            {
                CopiedItem = _tag;
                CopiedItemName = _clickedNode.Text;
            }
            else if (e.ClickedItem.Name == "menuCut")
            {
                CopiedItem = _tag;
                CopiedItemName = _clickedNode.Text;

                if (_tag.area == CLNodeArea.NODE_RESOURCES)
                {
                    using (CLDatabaseResources db = new CLDatabaseResources())
                    {
                        // delete resource from db if it is a copy
                        db.ExecuteNonQuery("delete from RES where groupID=`" + _tag.groupID +
                        "` and resName=`" + _clickedNode.Text + "` and ID = 'copy'");
                    }
                }
                else
                {
                    using (CLDatabaseTags db = new CLDatabaseTags())
                    {
                        db.ExecuteNonQuery("delete from TAGS where groupID=`" + _tag.groupID +
                        "` and tagName=`" + _clickedNode.Text + "`");
                    }
                }
                TreeNode _parentNode = _clickedNode.Parent;
                _clickedNode.Remove();
                treeView1.SelectedNode = _parentNode;
            }
            else if (e.ClickedItem.Name == "menuPaste")
            {
                if (CopiedItem == null || CopiedItemName == "")
                    return;

                CLNodeTag aClikedNode = _clickedNode.Tag as CLNodeTag;
                if (CopiedItem.area != aClikedNode.area)
                    return; // resource is copied to tags or vice versa

                int index = _clickedNode.Nodes.Count; // if group clicked
                string groupID = _tag.ID;
                if (aClikedNode.type == CLNodeType.NODE_ENTRY) // item clicked
                {
                    index = _clickedNode.Index + 1;
                    _clickedNode = _clickedNode.Parent;
                    groupID = _tag.groupID;
                }

                foreach (TreeNode tnode in _clickedNode.Nodes)
                {
                    if (tnode.Text == CopiedItemName)
                        return; // item with this name exists
                }

                // Insert new node with copied item name
                TreeNode node = new TreeNode(CopiedItemName);
                _clickedNode.Nodes.Insert(index, node);

                // Create new Tag for node
                CLNodeTag __tag = new CLNodeTag(CopiedItem.type, CopiedItem.area)
                { ID = CopiedItem.ID, groupID = groupID, orderID = index, tagColor = CopiedItem.tagColor };
                node.Tag = __tag;
                // Set copied item color to node
                try { node.BackColor = ColorTranslator.FromHtml(CopiedItem.tagColor); }
                catch { }

                // Add pasted item to DB
                if (__tag.area == CLNodeArea.NODE_TAGS)
                {
                    using (CLDatabaseTags db = new CLDatabaseTags())
                        db.AddTag(__tag.ID, __tag.groupID, CopiedItemName, __tag.tagColor, index);
                }
                else if (__tag.area == CLNodeArea.NODE_RESOURCES)
                {
                    using (CLDatabaseResources db = new CLDatabaseResources())
                        db.AddResourceCopy(__tag.groupID, CopiedItemName, index);
                }
            }
        }
        CLNodeTag CopiedItem = null;
        string CopiedItemName = "";

        private bool CheckMarkerGroup(MapMarkerGroup _mmg)
        {
            if (_mmg == null || !_mmg.IsValid)
            {
                MessageBox.Show(this,
                    Utils.getString("commonlists.error.wronggrouptype"),
                    Utils.getString("commonlists.error.caption"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        protected void AddNewGroupOrItem(TreeNode aNode)
        {
            CLNodeTag _tag = (CLNodeTag)aNode.Tag;
            if (_tag.type == CLNodeType.NODE_ENTRY)
                return;

            m_editType = LabelEditType.EDIT_NEW;
            TreeNode _newNode = new TreeNode("");
            CLNodeTag _newTag = new CLNodeTag(_tag.type == CLNodeType.NODE_ROOT ? CLNodeType.NODE_GROUP : CLNodeType.NODE_ENTRY, _tag.area);
            _newNode.Tag = _newTag;

            aNode.Nodes.Add(_newNode);

            if (_newTag.type == CLNodeType.NODE_GROUP)
            {
                _newNode.ForeColor = System.Drawing.Color.RoyalBlue;
                _newNode.NodeFont = new Font(_newNode.NodeFont ?? treeView1.Font, FontStyle.Bold);
            }
            else // item
            {
                _newNode.ForeColor = System.Drawing.Color.Black;
            }

            m_editNode = _newNode;
            StartNodeEdit();
        }

        #region Collapse and expand
        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            m_bGotKeydown = false;
            m_bStateChanged = true;
            TreeNode _node = e.Node;
            CLNodeTag _tag = (CLNodeTag)_node.Tag;
            _node.Tag = _tag;
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            m_bGotKeydown = false;
            m_bStateChanged = true;
            TreeNode _node = e.Node;
            CLNodeTag _tag = (CLNodeTag)_node.Tag;
            _node.Tag = _tag;
        }
        #endregion

        protected bool GroupHasElement(TreeNodeCollection aNodes, string aElementName)
        {
            foreach (TreeNode _node in aNodes)
                if (_node.Text == aElementName)
                    return true;
            return false;
        }

        #region DragDrop
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Get drag node and select it
            m_dragNode = (TreeNode)e.Item;
            dragItemColor = m_dragNode.BackColor;
            CLNodeTag _tag = (CLNodeTag)m_dragNode.Tag;
            if (_tag.type == CLNodeType.NODE_ROOT)
                return;

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

        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            Cursor.Clip = treeView1.RectangleToScreen(treeView1.ClientRectangle);

            // Compute drag position and move image
            Point _formP = this.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragMove(_formP.X - this.treeView1.Left, _formP.Y - this.treeView1.Top);

            // Get actual drop node
            TreeNode _dropNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(new Point(e.X, e.Y)));
            if (_dropNode == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            // Make sure region is the same and drag node isn't root node for items.
            // When draggigg folders, allow only folder and root to be drop target
            CLNodeTag _dragTag = (CLNodeTag)m_dragNode.Tag;
            CLNodeTag _dropTag = (CLNodeTag)_dropNode.Tag;
            if (_dragTag.area != _dropTag.area
                || (_dragTag.type == CLNodeType.NODE_ENTRY && _dropTag.type == CLNodeType.NODE_ROOT)
                || (_dragTag.type == CLNodeType.NODE_GROUP && _dropTag.type == CLNodeType.NODE_ENTRY)
                )
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

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            DragHelper.ImageList_DragEnter(this.treeView1.Handle, e.X - this.treeView1.Left, e.Y - this.treeView1.Top);

            // Enable timer for scrolling dragged item
            this.timer.Enabled = true;
            this.timer1.Enabled = true;
        }

        private void treeView1_DragLeave(object sender, EventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.treeView1.Handle);

            // Disable timer for scrolling dragged item
            this.timer.Enabled = false;
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
            if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left)
            {
                timer1.Enabled = false;
                Cursor.Clip = _originalClip;
            }
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            // Unlock updates
            DragHelper.ImageList_DragLeave(this.treeView1.Handle);

            // Get drop node
            TreeNode _dropNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(new Point(e.X, e.Y)));
            if (m_dragNode == _dropNode)
                return; // Nothing changed

            CLNodeTag _dragTag = (CLNodeTag)m_dragNode.Tag;
            CLNodeTag _dropTag = (CLNodeTag)_dropNode.Tag;
            // Check if we in the same area
            if (_dragTag.area != _dropTag.area)
                return;

            TreeNodeCollection _dragParent = m_dragNode.Parent != null ? m_dragNode.Parent.Nodes : treeView1.Nodes;
            TreeNodeCollection _dropParent = _dropNode.Parent != null ? _dropNode.Parent.Nodes : treeView1.Nodes;

            CLDatabaseResources dbRes = new CLDatabaseResources();
            CLDatabaseTags dbTags = new CLDatabaseTags();

            if (_dragTag.type == CLNodeType.NODE_ENTRY)
            {
                // Dropping entry to root node is not allowed.
                if (_dropTag.type == CLNodeType.NODE_ROOT)
                    return;

                if (_dropTag.type == CLNodeType.NODE_GROUP)
                    _dropParent = _dropNode.Nodes;
                if (_dragTag.groupID != _dropTag.groupID && GroupHasElement(_dropParent, m_dragNode.Text))
                    return; // Same element in new group already exists. Probably we shall blame it.

                TreeNode _newNode = new TreeNode(m_dragNode.Text);

                string groupID = _dropTag.type == CLNodeType.NODE_ENTRY ? _dropTag.groupID : _dropTag.ID;

                _newNode.Tag = new CLNodeTag(_dragTag.type, _dragTag.area)
                { groupID = groupID, ID = "copy", tagColor = _dragTag.tagColor };

                // Add drag node to new position
                int _dropNodeIndex = _dropTag.type == CLNodeType.NODE_GROUP ? 0 : _dropParent.IndexOf(_dropNode) + 1;
                _dropParent.Insert(_dropNodeIndex, _newNode);

                _newNode.BackColor = dragItemColor;

                bool ctrlPressed = false;
                // Leave drag node if CTRL pressed
                if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl) ||
                    System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightCtrl))
                    ctrlPressed = true;
                else // otherwise, remove it
                    _dragParent.Remove(m_dragNode);

                if (ctrlPressed) // node is copied, add new entry in db
                {
                    if (_dropTag.area == CLNodeArea.NODE_RESOURCES)
                        dbRes.AddResourceCopy(groupID, m_dragNode.Text, 0);
                    else
                        dbTags.AddTag(MMUtils.ID(), groupID, m_dragNode.Text, "", 0);
                }
                else // node is moved, update drag node group ID
                {
                    if (_dragTag.area == CLNodeArea.NODE_RESOURCES)
                        dbRes.ExecuteNonQuery("update RES set groupID=`" + groupID +
                            "` where groupID=`" + _dragTag.groupID + "` and resName='" + m_dragNode.Text + "'");
                    else if (_dragTag.area == CLNodeArea.NODE_TAGS)
                        dbTags.ExecuteNonQuery("update TAGS set groupID=`" + groupID +
                            "` where groupID=`" + _dragTag.groupID + "` and tagName=`" + m_dragNode.Text + "`");
                }

                // update order ID in drop group
                int i = 1;
                foreach (TreeNode _node in _dropParent)
                {
                    CLNodeTag _nodeTag = (CLNodeTag)_node.Tag;
                    if (_nodeTag.area == CLNodeArea.NODE_RESOURCES)
                        dbRes.ExecuteNonQuery("update RES set orderID=" + i++ +
                            " where groupID=`" + _nodeTag.groupID + "` and resName=`" + _node.Text + "`");
                    else
                        dbTags.ExecuteNonQuery("update TAGS set orderID=" + i++ +
                            " where groupID=`" + _nodeTag.groupID + "` and tagName=`" + _node.Text + "`");
                }
                // update order ID in drag group if node was moved
                if (!ctrlPressed)
                {
                    i = 1;
                    foreach (TreeNode _node in _dragParent)
                    {
                        CLNodeTag _nodeTag = (CLNodeTag)_node.Tag;
                        if (_nodeTag.area == CLNodeArea.NODE_RESOURCES)
                            dbRes.ExecuteNonQuery("update RES set orderID=" + i++ +
                                " where groupID=`" + _nodeTag.groupID + "` and resName=`" + _node.Text + "`");
                        else
                            dbTags.ExecuteNonQuery("update TAGS set orderID=" + i++ +
                                " where groupID=`" + _nodeTag.groupID + "` and tagName=`" + _node.Text + "`");
                    }
                }
            } // dragging entry end
            else if (_dragTag.type == CLNodeType.NODE_GROUP)
            {
                // Dragging just one entry.
                // If drop node is root, move drag node _after_ drop node.
                // If drop node is group, add drag node to the beginnig of that group.
                if (_dropTag.type == CLNodeType.NODE_ENTRY)
                    return;
                if (_dropTag.type == CLNodeType.NODE_ROOT)
                    _dropParent = _dropNode.Nodes;
                _dragParent.Remove(m_dragNode);
                int _dropNodeIndex = _dropTag.type == CLNodeType.NODE_ROOT ? 0 : _dropParent.IndexOf(_dropNode) + 1;
                _dropParent.Insert(_dropNodeIndex, m_dragNode);

                // Update database
                int i = 1;

                foreach (TreeNode _node in _dropParent)
                {
                    CLNodeTag _nodeTag = (CLNodeTag)_node.Tag;
                    if (_nodeTag.area == CLNodeArea.NODE_RESOURCES)
                        dbRes.ExecuteNonQuery("update RESGROUPS set orderID=" + i++ + " where ID=`" + _nodeTag.groupID + "`");
                    else if (_nodeTag.area == CLNodeArea.NODE_TAGS)
                        dbTags.ExecuteNonQuery("update TAGGROUPS set orderID=" + i++ + " where ID=`" + _nodeTag.groupID + "`");
                }
            } // dragging group end
            dbRes.Dispose();
            dbTags.Dispose();

            Cursor.Clip = _originalClip;
        }
        #endregion

        #region Renaming related

        private void StartNodeEdit()
        {
            if (m_editType == LabelEditType.EDIT_DISABLED)
                return;
            AddNewResourcesGroup.Enabled = false;
            AddNewTagGroup.Enabled = false;
            m_editNode.EnsureVisible();
            Rectangle _p = m_editNode.Bounds;
            _p.Y += treeView1.Bounds.Y;
            _p.X += treeView1.Bounds.X;
            _p.Height = textBox1.Height;
            _p.Width = treeView1.Bounds.Right - _p.X - 4;
            textBox1.Bounds = _p;
            textBox1.Text = m_editNode.Text;
            textBox1.Visible = true;
            textBox1.Focus();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                AcceptAddEditNode();
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                CancelAddEditNode();
            }
            if (e.KeyCode == Keys.V && e.Control)
            {
                textBox1.Text = System.Windows.Clipboard.GetText();
            }
        }

        private void AcceptAddEditNode()
        {
            AddNewResourcesGroup.Enabled = true;
            AddNewTagGroup.Enabled = true;
            textBox1.Visible = false;
            treeView1.Focus();

            string ID = MMUtils.ID(); // unic ID for new groups, resources and tags

            string _label = textBox1.Text;
            if (_label == null || _label == "" || _label.Contains(",") || _label.Contains("'") || _label != _label.Trim())
            {
                // Disabled character in item name. Blame it?
                CancelAddEditNode();
                return;
            }
            TreeNode _node = m_editNode;
            CLNodeTag _tag = (CLNodeTag)_node.Tag;

            switch (m_editType)
            {
                case LabelEditType.EDIT_DISABLED:
                    return;
                case LabelEditType.EDIT_NEW:
                    {
                        // Create new item
                        TreeNodeCollection _parent = _node.Parent.Nodes;
                        if (GroupHasElement(_parent, _label))
                        {
                            // Such element already exists. Blame it?
                            _parent.Remove(_node);
                            break;
                        }

                        _node.Text = _label;
                        _tag.orderID = _parent.Count;
                        selectedNode = _node;

                        // Update database
                        if (_tag.type == CLNodeType.NODE_GROUP)
                        {
                            if (_tag.area == CLNodeArea.NODE_RESOURCES)
                            {
                                using (CLDatabaseResources db = new CLDatabaseResources())
                                {
                                    db.AddGroup(ID, _node.Text, _tag.orderID);
                                    _tag.ID = ID;
                                }
                            }
                            else if (_tag.area == CLNodeArea.NODE_TAGS)
                            {
                                using (CLDatabaseTags db = new CLDatabaseTags())
                                {
                                    db.AddGroup(ID, _node.Text, 0, _tag.orderID);
                                    _tag.ID = ID;
                                }
                            }
                        }
                        else if (_tag.type == CLNodeType.NODE_ENTRY)
                        {
                            _tag.ID = ID;

                            string groupID = (_node.Parent.Tag as CLNodeTag).ID;
                            _tag.groupID = groupID;

                            if (_tag.area == CLNodeArea.NODE_RESOURCES) // resource
                            {
                                using (CLDatabaseResources db = new CLDatabaseResources())
                                {
                                    DataTable dt = db.ExecuteQuery("select * from RES where resName ='" + _label + "'");
                                    // add copy
                                    db.AddResourceCopy(groupID, _label, _tag.orderID);
                                    // add original
                                    if (dt.Rows.Count < 1) // new resource, will be the original entry
                                        db.AddResource(ID, "0", _label, "", "", "", "", 0, 0, "", "", 0);
                                }
                            }
                            else if (_tag.area == CLNodeArea.NODE_TAGS) // tag
                            {
                                using (CLDatabaseTags db = new CLDatabaseTags())
                                    db.AddTag(ID, groupID, _label, "", _tag.orderID);

                            }
                        } // entry
                        treeView1.SelectedNode = m_editNode;
                        break;
                    }
                case LabelEditType.EDIT_RENAME:
                    {
                        // Rename item
                        if (GroupHasElement(_node.Parent.Nodes, _label))
                        {
                            // Such element already exists. Blame it?
                            break;
                        }
                        if (_node.Text == _label)
                            break;

                        // Update database
                        if (_tag.area == CLNodeArea.NODE_RESOURCES)
                        {
                            using (CLDatabaseResources db = new CLDatabaseResources())
                            {
                                if (_tag.type == CLNodeType.NODE_GROUP)
                                {
                                    db.ExecuteNonQuery("update RESGROUPS set name=`" + MMUtils.ValidateString(_label)
                                        + "` where ID=`" + _tag.ID + "`");
                                }
                                else if (_tag.type == CLNodeType.NODE_ENTRY)
                                {
                                    db.ExecuteNonQuery("update RES set resName=`" + MMUtils.ValidateString(_label)
                                        + "` where resName=`" + _node.Text + "`");
                                    UpdateResources(_node.Text, _label);
                                }
                            }
                        }
                        else if (_tag.area == CLNodeArea.NODE_TAGS)
                        {
                            using (CLDatabaseTags db = new CLDatabaseTags())
                            {
                                if (_tag.type == CLNodeType.NODE_GROUP)
                                {
                                    db.ExecuteNonQuery("update TAGGROUPS set name=`" + MMUtils.ValidateString(_label)
                                        + "` where ID=`" + _tag.ID + "`");
                                }
                                else if (_tag.type == CLNodeType.NODE_ENTRY)
                                {
                                    db.ExecuteNonQuery("update TAGS set tagName=`" + MMUtils.ValidateString(_label)
                                        + "` where tagName=`" + _node.Text + "` and groupID=`" + _tag.groupID + "`");
                                }
                            }
                        }
                        _node.Text = _label;
                        break;
                    }
            }
            m_editType = LabelEditType.EDIT_DISABLED;
            m_editNode = null;
        }

        public void UpdateResources(string oldName, string newName)
        {
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                foreach (TreeNode _node in node.Nodes)
                    if (_node.Text == oldName)
                        _node.Text = newName;
            }
        }

        private void UpdateDupesColor(string name, string color)
        {
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                foreach (TreeNode _node in node.Nodes)
                {
                    if (_node.Text == name)
                    {
                        try
                        {
                            _node.BackColor = ColorTranslator.FromHtml(color);
                        }
                        catch { }
                    }
                }
            }
        }

        public void RemoveResources(string name)
        {
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                    if (node.Nodes[i].Text == name)
                        node.Nodes[i].Remove();
            }
        }

        private void CancelAddEditNode()
        {
            AddNewResourcesGroup.Enabled = true;
            AddNewTagGroup.Enabled = true;
            textBox1.Visible = false;
            treeView1.Focus();

            if (m_editNode == null)
            {
                m_editType = LabelEditType.EDIT_DISABLED;
                return;
            }

            if (m_editType == LabelEditType.EDIT_NEW)
                m_editNode.Remove();

            m_editType = LabelEditType.EDIT_DISABLED;
            m_editNode = null;
        }
        #endregion

        private void AddNewResourcesGroup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNewGroupOrItem(treeView1.Nodes[0]);
        }

        private void AddNewTagGroup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNewGroupOrItem(treeView1.Nodes[1]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("'"))
                textBox1.Text = textBox1.Text.Replace("'", "");
            if (textBox1.Text.Contains(","))
                textBox1.Text = textBox1.Text.Replace(",", "");
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            m_bGotKeydown = false;
            m_bStateChanged = true;
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_bGotKeydown && treeView1.SelectedNode != null)
                    treeView1.SelectedNode.Toggle();
            }
            m_bGotKeydown = false;
            m_bStateChanged = false;

            Cursor.Clip = _originalClip;
        }

        private bool isColorEmpty(System.Drawing.Color aColor)
        {
            return (aColor.R == 0 && aColor.G == 0 && aColor.B == 0 && aColor.A == 0);
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            // Draw the background and node text for a selected node.

            // Draw the background of the selected node. The NodeBounds method makes 
            // the highlight rectangle large enough toinclude the text of a node tag, if one is present.
            Rectangle _nodeRect = e.Node.Bounds;
            if (_nodeRect.Width == 0 || _nodeRect.Height == 0)
            {
                e.DrawDefault = true;
                return;
            }
            int _indent = (e.Node.Level * treeView1.Indent) + treeView1.Margin.Left + 17;

            // Determine if node is hovered
            TreeNode _nodeAtCursor = treeView1.GetNodeAt(treeView1.PointToClient(System.Windows.Forms.Control.MousePosition));
            bool _isHover = (e.Node == _nodeAtCursor);

            Font _nodeFont = e.Node.NodeFont;
            if (_nodeFont == null)
                _nodeFont = ((TreeView)sender).Font;
            if (_isHover)
            {
                _nodeFont = new Font(_nodeFont, _nodeFont.Style | FontStyle.Underline);
            }

            int _fontHeight = Convert.ToInt32(Math.Floor(_nodeFont.GetHeight()));

            Rectangle _fillRectRoot = new Rectangle(0, _nodeRect.Y + 1, treeView1.ClientSize.Width, _nodeRect.Height - 2);
            Rectangle _fillRect = new Rectangle(_indent, _nodeRect.Y + 1, e.Node.Bounds.Width + 4, _nodeRect.Height - 2);
            Rectangle _textRect = new Rectangle(_indent, _nodeRect.Y + 3, treeView1.ClientSize.Width - _indent, _fontHeight);
            Rectangle _imageRect = new Rectangle(_fillRectRoot.Right - 22, _fillRectRoot.Y + 1, 16, 16);
            Rectangle _plusminusRect = new Rectangle(_indent - 16, _nodeRect.Y + 3, 16, 16);
            CLNodeTag _tag = (CLNodeTag)e.Node.Tag;

            System.Drawing.Color _nodeBackColor = e.Node.BackColor;
            if (isColorEmpty(_nodeBackColor))
                _nodeBackColor = SystemColors.Window;
            System.Drawing.Color _nodeForeColor = e.Node.ForeColor;
            if (isColorEmpty(_nodeForeColor))
                _nodeForeColor = treeView1.ForeColor;

            if (e.Node.IsSelected)
            {
                _nodeForeColor = SystemColors.Control;
                _nodeBackColor = SystemColors.MenuHighlight;
            }

            if (_tag.type == CLNodeType.NODE_ROOT)
                e.Graphics.FillRectangle(new SolidBrush(_nodeBackColor), _fillRectRoot);
            else
                e.Graphics.FillRectangle(new SolidBrush(_nodeBackColor), _fillRect);

            // Draw PlusMinus if needed
            if (treeView1.ShowPlusMinus && e.Node.Nodes.Count > 0)
            {
                if (System.Windows.Forms.Application.RenderWithVisualStyles)
                {
                    VisualStyleElement element = (e.Node.IsExpanded) ?
                        VisualStyleElement.TreeView.Glyph.Opened : VisualStyleElement.TreeView.Glyph.Closed;

                    VisualStyleRenderer renderer = new VisualStyleRenderer(element);
                    renderer.DrawBackground(e.Graphics, _plusminusRect);
                }
                else
                {
                    int h = 8;
                    int w = 8;
                    int x = _plusminusRect.X;
                    int y = _plusminusRect.Y + (_plusminusRect.Height / 2) - 4;

                    e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlDark), x, y, w, h);
                    e.Graphics.FillRectangle(new SolidBrush(_nodeBackColor), x + 1, y + 1, w - 1, h - 1);
                    e.Graphics.DrawLine(new Pen(new SolidBrush(System.Drawing.Color.Black)), x + 2, y + 4, x + w - 2, y + 4);

                    if (!e.Node.IsExpanded)
                        e.Graphics.DrawLine(new Pen(new SolidBrush(System.Drawing.Color.Black)), x + 4, y + 2, x + 4, y + h - 2);
                }
            }

            // Draw node text
            e.Graphics.DrawString(e.Node.Text, _nodeFont, new SolidBrush(_nodeForeColor), _textRect);
            // Draw image if any
            if (_tag.type == CLNodeType.NODE_ROOT)
                e.Graphics.DrawImageUnscaled(imageList1.Images[e.Node.ImageIndex], _imageRect.X - imageSize.Width / 2, _imageRect.Y);
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Right)
                btnNext_Click(null, null);
            if (e.KeyData == (Keys.Shift | Keys.Enter) || e.KeyData == Keys.Left)
                btnPrev_Click(null, null);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (FoundNodes.Count < 1)
                return;
            if (txtSearch.Text.Length < 2)
                return;

            if (FoundNodes.Count > 0 && foundNode < FoundNodes.Count && foundNode > 0)
            {
                treeView1.SelectedNode = FoundNodes[--foundNode];
                txtSearch.Focus();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            string text = txtSearch.Text.ToLower().Trim();

            if (text.Length < 2)
                return;

            if (text != searchedText)
            {
                foundNode = 0;
                FoundNodes.Clear(); // new search
                foreach (TreeNode node in treeView1.Nodes) // Resources & Tags
                    SearchRecursive(node, text);
            }

            searchedText = text;
            if (FoundNodes.Count > 0 && foundNode < FoundNodes.Count)
            {
                if (FoundNodes[foundNode].IsSelected && foundNode < FoundNodes.Count - 1)
                    foundNode++;

                treeView1.SelectedNode = FoundNodes[foundNode];
                txtSearch.Focus();
            }
        }

        private void SearchRecursive(TreeNode node, string text)
        {
            foreach (TreeNode subnode in node.Nodes)
            {
                CLNodeTag tag = (CLNodeTag)subnode.Tag;
                if (tag.type == CLNodeType.NODE_ENTRY)
                {
                    if (subnode.Text.ToLower().Trim().Contains(txtSearch.Text))
                        FoundNodes.Add(subnode);
                }
                SearchRecursive(subnode, text);
            }
        }

        private void CheckMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void imageSize_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, "manage_lists.htm");
        }

        public static TreeNode selectedNode = null;
        System.Drawing.Color dragItemColor = System.Drawing.Color.RoyalBlue;

        int foundNode = 0;
        string searchedText = "";
        List<TreeNode> FoundNodes = new List<TreeNode>();

        Rectangle _originalClip;
    }

    public enum CLNodeType
    {
        NODE_ROOT,
        NODE_GROUP,
        NODE_ENTRY
    }

    public enum CLNodeArea
    {
        NODE_RESOURCES,
        NODE_TAGS
    }

    internal class CLNodeTag : Object
    {
        // Root and group nodes
        public CLNodeTag(CLNodeType aNodeType, CLNodeArea aNodeArea)
        {
            type = aNodeType;
            area = aNodeArea;
        }

        public override string ToString()
        {
            return "";
        }

        public CLNodeType type = CLNodeType.NODE_ROOT;
        public CLNodeArea area = CLNodeArea.NODE_RESOURCES;
        public int orderID = 1;
        public string groupID = "0";
        public string tagColor = "";
        public bool mutex = false;
        public string ID = "0";
    }
}
