﻿using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace Bubbles
{
    public partial class ResourcesDlg : Form
    {
        public ResourcesDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "Resources.htm");

            btnAddToTopic.Text = Utils.getString("ResourcesDlg.btnAddToTopic");
            txtResources.Text = Utils.getString("ResourcesDlg.dummytext");
            btnAddToMap.Text = Utils.getString("ResourcesDlg.btnAddToMap");
            lblResourceName.Text = Utils.getString("ResourcesDlg.lblResourceName");
            btnCancel.Text = Utils.getString("button.cancel");

            toolTip1.SetToolTip(New, Utils.getString("ResourcesDlg.NewResorce"));
            toolTip1.SetToolTip(Edit, Utils.getString("ResourcesDlg.EditResorce"));
            toolTip1.SetToolTip(Delete, Utils.getString("ResourcesDlg.DeleteResorce"));
            toolTip1.SetToolTip(pManageIcons, Utils.getString("ResourcesDlg.pManageIcons"));
            toolTip1.SetToolTip(Sort, Utils.getString("notes.contextmenu.sort"));
            toolTip1.SetToolTip(btnAddToMap, Utils.getString("ResourcesDlg.btnAddToMap.tooltip"));
            toolTip1.SetToolTip(Help, Utils.getString("button.help"));
            toolTip1.SetToolTip(Close, Utils.getString("button.close"));
            toolTip1.SetToolTip(pAddToMap, Utils.getString("ResourcesDlg.pAddToMap.tooltip"));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            cmsSort.ItemClicked += ContextMenuStrip_ItemClicked;
            cmsResource.ItemClicked += ContextMenuStrip_ItemClicked;

            cmsSort.Items["sortAZ"].Text = Utils.getString("notes.contextmenu.sortAZ");
            StickUtils.SetContextMenuImage(cmsSort.Items["sortAZ"], "sortAZ.png");
            cmsSort.Items["sortZA"].Text = Utils.getString("notes.contextmenu.sortZA");
            StickUtils.SetContextMenuImage(cmsSort.Items["sortZA"], "sortZA.png");
            cmsSort.Items["sortByIcon"].Text = Utils.getString("ResourcesDlg.sortByIcon");
            StickUtils.SetContextMenuImage(cmsSort.Items["sortByIcon"], "0.png");

            cmsResource.Items["AddToTopic"].Text = Utils.getString("ResourcesDlg.btnAddToTopic");
            //StickUtils.SetContextMenuImage(cmsResource.Items["AddToTopic"], "newsticker.png");
            cmsResource.Items["AddToMap"].Text = Utils.getString("ResourcesDlg.btnAddToMap");
            //StickUtils.SetContextMenuImage(cmsResource.Items["AddToMap"], "newsticker.png");
            cmsResource.Items["ChangeIcon"].Text = Utils.getString("ResourcesDlg.ChangeIcon");
            StickUtils.SetContextMenuImage(cmsResource.Items["ChangeIcon"], "0.png");
            cmsResource.Items["r_rename"].Text = Utils.getString("button.rename");
            StickUtils.SetContextMenuImage(cmsResource.Items["r_rename"], "edit.png");
            cmsResource.Items["r_delete"].Text = Utils.getString("button.delete");
            StickUtils.SetContextMenuImage(cmsResource.Items["r_delete"], "deleteall.png");

            this.Paint += this_Paint; // paint the border
            this.MinimumSize = new Size(this.Width, this.Height / 2);
            this.MaximumSize = new Size(this.Width, Screen.AllScreens.Max(s => s.Bounds.Height));

            //add single column; -2 => autosize
            ListResources.Columns.Add("MyColumn", -2, HorizontalAlignment.Left);
            ListResources.GridLines = true;

            // Set icons
            imageList1.ImageSize = Help.Size;
            string path = Utils.ImagesPath + "\\ResourceLabels\\";
            for (int i = 0; i < 8; i++)
            imageList1.Images.Add(i.ToString(), Image.FromFile(path + i.ToString() + ".png"));

            // Manage resource icons User Control
            mri.Location = panel1.Location;
            this.Controls.Add(mri);
            mri.Visible = false;
            mri.ParentForm = this;

            Init();
        }

        public void Init()
        {
            ListResources.Clear();

            using (BubblesDB db = new BubblesDB())
            {
                // Add icon names to dictionary
                DataTable dt = db.ExecuteQuery("select * from RESOURCEGROUPS");
                foreach (DataRow dr in dt.Rows)
                {
                    int icon = Convert.ToInt32(dr["icon"]);
                    if (icon >= 0)
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

                    ListViewItem lvi = ListResources.Items.Add(item.Name, icon);
                    lvi.Tag = item; lvi.ToolTipText = ResourceIcons[item.aIcon];
                }
            }
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.StartsWith("sort"))
            {
                sortype = e.ClickedItem.Name;
                Sort_MouseClick(null, null);
            }
            else if (e.ClickedItem.Name == "AddToTopic")
            {

            }
            else if (e.ClickedItem.Name == "AddToMap")
            {

            }
            else if (e.ClickedItem.Name == "ChangeIcon")
            {
                if (selectedItem != null)
                {
                    panelIcons.Location = new Point(panelIcons.Location.X, ListResources.Location.Y + selectedItem.Position.Y - pCorrect.Height);
                    panelIcons.Visible = true;
                }
            }
            else if (e.ClickedItem.Name == "r_rename")
            {

            }
            else if (e.ClickedItem.Name == "r_delete")
            {
                Delete_Click(null, null);
            }
        }

        private void panelIcons_Leave(object sender, EventArgs e)
        {
            panelIcons.Visible = false;
        }

        /// <summary>Change resource icon</summary>
        private void Icon_Click(object sender, EventArgs e)
        {
            // Get clicked icon index
            PictureBox pb = sender as PictureBox;
            int i = Convert.ToInt32(pb.Name.Substring(1));
            // Replace selected resource icon with clicked icon
            selectedItem.ImageIndex = i;
            // Update selected item Tag
            ResourceItem item = selectedItem.Tag as ResourceItem;
            item.aIcon = i; selectedItem.Tag = item;
            // Hide icons panel
            panelIcons.Visible = false;

            // Update resource in the database
            using (BubblesDB db = new BubblesDB())
                db.ExecuteNonQuery("update RESOURCES set icon=" + i + 
                    " where name=`" + item.Name + "` and groupID=" + item.GroupID + "");

            selectedItem.ToolTipText = ResourceIcons[item.aIcon];
        }

        private void pManageIcons_Click(object sender, EventArgs e)
        {
            if (!mri.Visible)
            {
                mri.Show();
                mri.BringToFront();
            }
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
        }

        private void New_Click(object sender, EventArgs e)
        {

        }

        private void Edit_Click(object sender, EventArgs e)
        {

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Utils.getString("ResourcesDlg.deleteicon.question"), "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            using (BubblesDB db = new BubblesDB())
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

        private void btnAddToTopic_Click(object sender, EventArgs e)
        {

        }

        private void Sort_MouseClick(object sender, MouseEventArgs e)
        {
            if (e == null || e.Button == MouseButtons.Left)
            {
                List<ResourceItem> items = new List<ResourceItem>();
                foreach (ListViewItem item in ListResources.Items)
                    items.Add(item.Tag as ResourceItem);

                if (sortype == "sortAZ" || sortype == "")
                    items = items.OrderBy(x => x.Name).ToList();
                else if (sortype == "sortZA")
                    items = items.OrderByDescending(x => x.Name).ToList();
                else if (sortype == "sortByIcon")
                    items = items.OrderBy(x => x.aIcon).ToList();

                ListResources.Items.Clear();
                foreach (var item in items)
                    ListResources.Items.Add(item.Name, item.aIcon).Tag = item;
            }
            else if (e.Button == MouseButtons.Right) // Context Menu
            {
                foreach (ToolStripItem item in cmsSort.Items)
                    item.Visible = true;

                cmsSort.Show(Cursor.Position);
            }
        }

        private void Help_Click(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listResources_MouseClick(object sender, MouseEventArgs e)
        {
            if (ListResources.SelectedItems.Count == 0) return;

            if (e.Button == MouseButtons.Left)
            {
                if ((ModifierKeys & Keys.Control) == Keys.Control ||
                    (ModifierKeys & Keys.Shift) == Keys.Shift)
                    return;

                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                    return;

                string[] listResources = new string[ListResources.SelectedItems.Count];
                for (int i = 0; i < ListResources.SelectedItems.Count; i++)
                    listResources[i] = ListResources.SelectedItems[i].Text;

                string listResource = ListResources.SelectedItems[0].Text;

                bool alltopicshaveresource = false;

                // If more than one items selected, do not calculate resoource presence on topics
                if (ListResources.SelectedItems.Count == 1)
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
            if (e.Button == MouseButtons.Right) // ContextMenu
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
                foreach (ToolStripItem item in cmsResource.Items)
                    item.Visible = true;

                cmsResource.Show(Cursor.Position);
            }
        }

        private void btnAddToMap_Click(object sender, EventArgs e)
        {

        }

        private void pAddToMap_Click(object sender, EventArgs e)
        {

        }

        private void txtResources_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtResources_Enter(object sender, EventArgs e)
        {

        }

        private void txtResources_Leave(object sender, EventArgs e)
        {

        }

        ListViewItem selectedItem = null;
        string sortype = "";
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
