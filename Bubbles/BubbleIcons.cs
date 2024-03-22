using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Clipboard = System.Windows.Forms.Clipboard;

namespace Bubbles
{
    internal partial class BubbleIcons : Form
    {
        public BubbleIcons(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation.Substring(0,1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "IconStick.htm");

            toolTip1.SetToolTip(pictureHandle, stickname);
            StickName = stickname;

            MinLength = this.Width; RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            cmsManage.ItemClicked += ContextMenuStrip1_ItemClicked;
            cmsIcon.ItemClicked += ContextMenuStrip1_ItemClicked;

            cmsIcon.Items["BI_newicon"].Text = Utils.getString("float_icons.contextmenu.new");
            StickUtils.SetContextMenuImage(cmsIcon.Items["BI_newicon"], "addicon.png");

            cmsIcon.Items["BI_rename"].Text = Utils.getString("button.rename");
            StickUtils.SetContextMenuImage(cmsIcon.Items["BI_rename"], "edit.png");

            BI_delete.Text = Utils.getString("float_icons.contextmenu.delete");
            BI_delete.ToolTipText = Utils.getString("float_icons.contextmenu.delete.tooltip");
            StickUtils.SetContextMenuImage(cmsIcon.Items["BI_delete"], "deleteall.png");

            BI_new.Text = Utils.getString("float_icons.contextmenu.new");
            StickUtils.SetContextMenuImage(BI_new, "addicon.png");

            BI_removeallfromtopic.Text = Utils.getString("float_icons.contextmenu.deletealltopic");
            StickUtils.SetContextMenuImage(BI_removeallfromtopic, "removeallicons.png");

            BI_addtomap.Text = Utils.getString("icons.contextmenu.addtomap");
            StickUtils.SetContextMenuImage(BI_addtomap, "icongroup.png");
            BI_addtomap.ToolTipText = Utils.getString("icons.contextmenu.addtomap.tooltip");

            StickUtils.SetCommonContextMenu(cmsManage, StickUtils.typeicons);

            cmsManage.Closing += ContextMenuStrip1_Closing;

            if (ID != 0) // if id = 0 - new stick is creating, ignore this step
            {
                using (SticksDB db = new SticksDB())
                {
                    // Check if there is a group or no
                    DataTable dt = db.ExecuteQuery("select * from STICKS where id=" + ID + "");

                    dt = db.ExecuteQuery("select * from ICONS where stickID=" + ID + " order by _order");

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["name"].ToString();
                        string filename = row["filename"].ToString();
                        string _filename = filename;
                        string iconPath = "";
                        string rootPath = Utils.m_dataPath + "IconDB\\";

                        if (filename.StartsWith("stock"))
                        {
                            rootPath = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
                            _filename = filename.Substring(5) + ".ico"; // stockemail -> email.ico
                            iconPath = rootPath + _filename;
                        }
                        else if (filename.StartsWith("pripro"))
                        {
                            rootPath = Utils.dllPath + "Images\\";
                            iconPath = rootPath + filename.Substring(6) + ".png";
                        }
                        else // custom icon
                        {
                            if (Utils.CustomIcons.ContainsKey(filename))
                                iconPath = Utils.CustomIcons[filename];
                            else
                            {
                                string path = Utils.GetIconFile(filename);
                                if (path == "") continue;
                                iconPath = path;
                            }
                        }

                        if (File.Exists(iconPath))
                        {
                            IconItem item = new IconItem(name, filename, Convert.ToInt32(row["_order"]), iconPath);
                            Icons.Add(item);
                        }
                    }
                }

                RefreshStick();
            }

            // Handle drag drop to place icon to the end
            this.DragEnter += Handle_DragEnter;
            this.DragDrop += Handle_DragDrop;

            // Handle drag drop to place icon to the begin
            pictureHandle.AllowDrop = true;
            pictureHandle.DragEnter += Handle_DragEnter;
            pictureHandle.DragDrop += Handle_DragDrop;

            pictureHandle.Click += PictureHandle_Click; // right click to show context menu (Paste icon to the begin)
            pictureHandle.MouseDown += Move_Stick; // move the stick
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse(); // collapse/expand stick
            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typeicons);

            this.MouseDown += Move_Stick; // move the stick
            this.MouseClick += BubbleIcons_MouseClick;
            Manage.Click += Manage_Click; // "Manage" icon's context menu

            if (collapsed) {
                collapsed = false; Collapse(); }
        }

        private void ContextMenuStrip1_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (groupclicked)
            {
                groupclicked = false;
                e.Cancel = true;
            }
        }

        private void BubbleIcons_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsIcon.Items)
                    item.Visible = false;

                cmsIcon.Items["BI_newicon"].Visible = true;

                cmsIcon.Show(Cursor.Position);
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            StickUtils.manage_clicked = true;

            foreach (ToolStripItem item in cmsManage.Items)
                item.Visible = true;

            selectedIcon = pictureHandle;
            cmsManage.Show(Cursor.Position);
        }

        public void PictureHandle_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsManage.Items)
                item.Visible = false;

            cmsManage.Show(Cursor.Position);
        }

        private void Move_Stick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_new" || e.ClickedItem.Name == "BI_newicon")
            {
                NewIcon();
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                StickUtils.Icons.Clear(); StickUtils.Icons.AddRange(Icons);
                StickUtils.DeleteIcon(selectedIcon, (int)this.Tag, StickUtils.typeicons);
                Icons.Clear(); Icons.AddRange(StickUtils.Icons);
                RefreshStick();
            }
            else if (e.ClickedItem.Name == "BI_removeallfromtopic")
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                    return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    if (t.UserIcons.Count > 0)
                        t.UserIcons.RemoveAll();
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                Icons.Clear();
                RefreshStick(true);
            }
            else if (e.ClickedItem.Name == "BI_addtomap")
            {
                MmStockIcon stockicon = 0; string signature = "";

                using (AddIconGroupDlg dlg = new AddIconGroupDlg(this, orientation, StickName))
                {
                    if (dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.OK)
                    {
                        foreach (var item in Icons)
                        {
                            if (item.FileName.StartsWith("pripro"))
                                continue; // We can't add to group Priority and Progress icons

                            if (item.FileName.StartsWith("stock"))
                                stockicon = StockIconFromString(item.FileName);
                            else
                                signature = item.FileName;

                            MapMarkers.GetIcon(stockicon, signature, item.IconName, item.Path, 
                                dlg.txtGroupName.Text.Trim(), dlg.cbMutEx.Checked, true);
                        }
                    }
                }  
            }
            else if (e.ClickedItem.Name == "BI_rename") // rename icon
            {
                IconItem item = (IconItem)selectedIcon.Tag;
                if (item == null) return;

                // Get new source's name
                string name = StickUtils.GetName(this, orientation, StickUtils.typeicons, item.IconName);
                if (name != "")
                {
                    // Change title in the picture box tag
                    ((IconItem)selectedIcon.Tag).IconName = name;
                    // Change title in the Source list item
                    Icons.Find(p => p.Path == item.Path).IconName = name;
                    toolTip1.SetToolTip(selectedIcon, name);

                    // Change title in the database
                    using (SticksDB db = new SticksDB())
                        db.ExecuteNonQuery("update ICONS set name=`" + name + "` where filename=`" +
                            item.Path + "` and stickID=" + (int)this.Tag + "");
                }
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "IconStick.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StickUtils.SaveStick(this.Bounds, (int)this.Tag, orientation, collapsed);
            }
            else if (e.ClickedItem.Name == "BI_newstick")
            {
                string name = StickUtils.GetName(this, orientation, StickUtils.typestick, "");
                if (name != "")
                {
                    string _collapsed = collapsed ? "1" : "0";
                    BubbleIcons form = new BubbleIcons(0, orientation + _collapsed, name);
                    StickUtils.CreateStick(form, name, StickUtils.typeicons);
                }
            }
            else if (e.ClickedItem.Name == "BI_renamestick")
            {
                string oldName = toolTip1.GetToolTip(pictureHandle);
                string newName = StickUtils.GetName(this, orientation, StickUtils.typeicons, oldName, true);
                if (newName != "" && newName != oldName)
                {
                    toolTip1.SetToolTip(pictureHandle, newName);
                    StickName = newName;
                } 
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                Collapse();
            }
            else if (e.ClickedItem.Name == "BI_delete_stick")
            {
                if (StickUtils.DeleteStick((int)this.Tag, StickUtils.typeicons))
                    this.Close();
            }
        }

        public void NewIcon()
        {
            List<string> filenames = Icons.Select(x => x.FileName).ToList();

            using (SelectIconDlg _dlg = new SelectIconDlg(filenames))
            {
                if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                    return;

                foreach (var pair in _dlg.SelectedIcons)
                {
                    string name = Path.GetFileNameWithoutExtension(pair.Value);
                    if (name.StartsWith("pripro")) name = "";
                    NewIcon(pair.Value, name, "end");
                }
            }
        }

        public void PasteIcon()
        {
            string path = null;
            string[] copiedFiles = (string[])Clipboard.GetData(DataFormats.FileDrop);
            if (path == null && copiedFiles == null)
                return;

            string title = StickUtils.Handle_DragDrop(ref path, copiedFiles, Icons, null);
            if (title == "") return;

            if (path == "" || title == "")
                return;

            position = "end";
            if (selectedIcon != null)
            {
                if (selectedIcon.Name == "pictureHandle")
                    position = "begin";
            }

            // Get source name
            string name = StickUtils.GetName(this, orientation, StickUtils.typeicons, title);
            if (name != "") NewIcon(path, name, position);
        }

        public void Rotate()
        {
            orientation = StickUtils.RotateStick(this, Manage, orientation);
        }

        /// <summary>
        /// Collapse/Expand stick
        /// </summary>
        /// <param name="CollapseAll">"Collapse All" command from Main Menu</param>
        /// <param name="ExpandAll">"Expand All" command from Main Menu</param>
        public void Collapse(bool CollapseAll = false, bool ExpandAll = false)
        {
            if (collapsed) // Expand stick
            {
                if (CollapseAll) return;

                collapseState = this.Location; // remember collapsed location
                collapseOrientation = orientation;
                StickUtils.Expand(this, RealLength, orientation, cmsManage);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, cmsManage);
                collapsed = true;
                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                {
                    this.Location = collapseState; // restore collapsed location
                    if (orientation != collapseOrientation) Rotate();
                }
            }
        }
        Point collapseState = new Point(0, 0);
        string collapseOrientation = "N";

        /// <summary>
        /// Add new icon at stick
        /// </summary>
        /// <param name="iconPath"></param>
        /// <param name="iconName"></param>
        /// <param name="position">"begin" or "end"</param>
        private void NewIcon(string iconPath, string iconName, string position)
        {
            bool pripro = iconPath.StartsWith(Utils.dllPath);
            string MMpath = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
            string fileName = "stock" + Path.GetFileNameWithoutExtension(iconPath);
            if (pripro)
            {
                fileName = "pripro" + Path.GetFileNameWithoutExtension(iconPath);
                iconName = "";
            }

            if (!pripro && !Utils.StockIcons.ContainsKey(fileName)) // кастомная иконка, сохраним файл в нашей базе
            {
                fileName = MMUtils.MindManager.Utilities.GetCustomIconSignature(iconPath);
                if (!Utils.CustomIcons.ContainsKey(fileName))
                {
                    if (iconPath.ToLower().StartsWith(MMpath))
                    {
                        // user added icon to the MMLibrary after MindManager start
                        Utils.CustomIcons.Add(fileName, iconPath);
                    }
                    else // user added icon dragging it from windows explorer. Save icon to our path.
                    {
                        //fileName = Path.GetFileName(iconPath);
                        string newPath = Utils.m_dataPath + "IconDB\\" + fileName;
                        string extension = Path.GetExtension(iconPath);
                        newPath += extension;

                        if (Utils.GetIconFile(fileName) == "")
                            File.Copy(iconPath, newPath);

                        Utils.CustomIcons.Add(fileName, newPath);
                    }
                }
            }

            // Add icon to Icons list
            int order = Icons.Count + 1; // at the end
            if (position == "begin") 
                order = 1;
            else if (position == "right")
            {
                IconItem _item = (IconItem)selectedIcon.Tag;
                order = _item.Order == Icons.Count ? Icons.Count + 1 : _item.Order + 1;
            }

            IconItem item = new IconItem(iconName, fileName, order, iconPath);
            using (SticksDB db = new SticksDB())
                db.AddIcon(iconName, fileName, order, (int)this.Tag);

            Icons.Insert(order - 1, item);
            for (int i = 0; i < Icons.Count; i++)
                Icons[i].Order = i + 1;

            RefreshStick();
        }

        void RefreshStick(bool deleteall = false)
        {
            StickUtils.Icons.Clear(); StickUtils.Icons.AddRange(Icons);
            List<PictureBox> pBoxs = StickUtils.RefreshStick(this, p1, orientation, MinLength, 
                collapsed, StickUtils.typeicons, deleteall);

            RealLength = StickUtils.stickLength;

            int i = 0;
            foreach (PictureBox pBox in pBoxs)
            {
                pBox.MouseClick += Icon_Click;
                pBox.MouseMove += PBox_MouseMove;
                pBox.DragEnter += Handle_DragEnter;
                pBox.DragDrop += Handle_DragDrop;
                pBox.MouseDown += PBox_MouseDown;
                pBox.GiveFeedback += PBox_GiveFeedback;

                if (collapsed && i++ > 0) // if collapsed hide all icons except the first
                    pBox.Visible = false;
            }
        }

        private void PBox_MouseDown(object sender, MouseEventArgs e)
        {
            cursor = Cursor.Position;
        }
        Point cursor;

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0) 
                    return;

                IconItem item = selectedIcon.Tag as IconItem;
                string fileName = item.FileName;
                bool alltopicshaveicon = true;

                if (fileName.StartsWith("pripro"))
                {
                    int value; string iconName = fileName.Substring(6);
                    if (iconName.StartsWith("pro")) // Progress icon
                    {
                        value = Convert.ToInt32(iconName.Substring(3));

                        foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            if (t.Task.Complete != value)
                            { alltopicshaveicon = false; break; }
                        }

                        foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            if (alltopicshaveicon) // remove Progress from topic
                                t.Task.Complete = -1;
                            else  // set icon on topic
                            {
                                if (t.Task.Complete != value)
                                    t.Task.Complete = value;
                            }
                        }
                    }
                    else // Priority icon
                    {
                        value = Convert.ToInt32(iconName.Substring(2));

                        foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            if (t.Task.Priority != BubbleTaskInfo.GetPriority(value))
                            { alltopicshaveicon = false; break; }
                        }

                        foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            if (alltopicshaveicon) // remove Priority from topic
                                t.Task.Priority = MmTaskPriority.mmTaskPriorityNone;
                            else
                            {
                                if (t.Task.Priority != BubbleTaskInfo.GetPriority(value))
                                    t.Task.Priority = BubbleTaskInfo.GetPriority(value);
                            }
                        }
                    }
                }
                else if (fileName.StartsWith("stock"))
                {
                    MmStockIcon icon = Utils.StockIcons[fileName];

                    if (icon != 0)
                    {
                        foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            if (!t.AllIcons.ContainsStockIcon(icon))
                            { alltopicshaveicon = false; break; }
                        }
                        foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            if (alltopicshaveicon) // remove icon from topic
                            {
                                if (t.AllIcons.ContainsStockIcon(icon))
                                    t.AllIcons.RemoveStockIcon(icon);
                            }
                            else // set icon on topic
                            {
                                if (!t.AllIcons.ContainsStockIcon(icon))
                                {
                                    t.AllIcons.AddStockIcon(icon);
                                }
                            }
                        }
                    }
                }
                else // custom icon
                {
                    string signature = fileName;
                    string path = Utils.CustomIcons[signature];
                    if (path == null || path == "") return;

                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        if (!t.AllIcons.ContainsCustomIcon(signature))
                        { alltopicshaveicon = false; break; }
                    }
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {

                        if (alltopicshaveicon) // remove icon from topic
                        {
                            if (t.AllIcons.ContainsCustomIcon(signature))
                                t.AllIcons.RemoveCustomIcon(signature);
                        }
                        else // set icon on topic
                        {
                            if (!t.AllIcons.ContainsCustomIcon(signature))
                            {
                                t.AllIcons.AddCustomIconFromMap(signature);
                            }
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsIcon.Items)
                    item.Visible = true;

                cmsIcon.Items["BI_newicon"].Visible = false;

                cmsIcon.Show(Cursor.Position);
            }
        }

        #region DragDrop
        private void Handle_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox))) // Move the picture box
            {
                var source = (PictureBox)e.Data.GetData(typeof(PictureBox)); // moving PB
                int sourceIndex = (source.Tag as IconItem).Order; // moving PB order
                int targetIndex = 0;

                if (sender is PictureBox) // also, can be this form
                {
                    var target = (PictureBox)sender;

                    if (target.Name == "pictureHandle")
                        targetIndex = 0; // move PB to the begin
                    else
                    {
                        // or after the target PB
                        try { targetIndex = (target.Tag as IconItem).Order; }
                        catch { }
                    }
                }
                else // sender is this form or something else (moving PB to the end)
                    targetIndex = this.Controls.OfType<PictureBox>().Count() - 2; // minus pictureHandle and p1

                if (sourceIndex != targetIndex)
                {
                    // Reorder Sources list
                    Icons.RemoveAt(sourceIndex - 1);
                    if (sourceIndex < targetIndex) { targetIndex--; }
                    Icons.Insert(targetIndex, source.Tag as IconItem);
                    for (int i = 0; i < Icons.Count; i++)
                        Icons[i].Order = i + 1;

                    RefreshStick();
                }
            }
            else // Drop *dragged* data
            {
                string path = null;
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string title = StickUtils.Handle_DragDrop(ref path, draggedFiles, Icons, null);

                if (title == "") return;

                if (path != "")
                {
                    position = "end";
                    if (sender is PictureBox)
                    {
                        var target = (PictureBox)sender;
                        if (target.Name == "pictureHandle")
                            position = "begin";
                        else
                        {
                            // or after the target PB
                            selectedIcon = (PictureBox)sender;
                            position = "right";
                        }
                    }

                    // Get icon name
                    string name = StickUtils.GetName(this, orientation, StickUtils.typeicons, title);
                    if (name != "")
                        NewIcon(path, name, position);
                }
            }
        }

        private void Handle_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox))) // Moving picture box
                e.Effect = DragDropEffects.Move;
            else // Dragging data
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                    e.Data.GetDataPresent(DataFormats.UnicodeText))
                    e.Effect = DragDropEffects.Copy; // Okay
                else
                    e.Effect = DragDropEffects.None; // Unknown data, ignore it
            }
        }

        private void PBox_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                e.UseDefaultCursors = false;

                //Cursor.Current = new Cursor(Utils.ImagesPath + "mm_project.ico");
                Cursor.Current = new Cursor(Utils.ImagesPath + "Cursor1.cur");
                //Graphics graphics = this.CreateGraphics();
                //Rectangle rectangle = new Rectangle(
                //  new Point(0, 0), new Size(pictureHandle.Width,
                //  pictureHandle.Height)); // <<<<<<<<< you will need to define size based on SystemParameters.CursorHeight here
                //Cursor.Current.Draw(graphics, rectangle);
                //Cursor.Current.Dispose();

                //DrawCursorsOnForm(Cursor.Current);
            }
            else
                e.UseDefaultCursors = true;
        }

        private void DrawCursorsOnForm(Cursor cursor)
        {
            // If the form's cursor is not the Hand cursor and the 
            // Current cursor is the Default, Draw the specified 
            // cursor on the form in normal size and twice normal size.
            //if (this.Cursor != Cursors.Hand &
            //  Cursor.Current == Cursors.Default)
            {
                // Draw the cursor stretched.
                Graphics graphics = this.CreateGraphics();
                Rectangle rectangle = new Rectangle(
                  new Point(10, 10), new Size(pictureHandle.Width * 2,
                  pictureHandle.Height * 2));
                //cursor.DrawStretched(graphics, rectangle);

                // Draw the cursor in normal size.
                rectangle.Location = new Point(
                rectangle.Width + rectangle.Location.X,
                  rectangle.Height + rectangle.Location.Y);
                //rectangle.Size = cursor.Size;
                cursor.Draw(graphics, rectangle);

                // Dispose of the cursor.
                cursor.Dispose();
            }
        }

        private void PBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var pb = (PictureBox)sender;
                if (Math.Abs(Cursor.Position.X - cursor.X) > Manage.Width / 2 ||
                    Math.Abs(Cursor.Position.Y - cursor.Y) > Manage.Width / 2)
                    pb.DoDragDrop(pb, DragDropEffects.Move);
            }
        }

        #endregion

        #region stock icon enumaration

        /// <summary>
        /// Gets stock icon enumaration
        /// </summary>
        /// <param name="aStockIcon">string representation of a stock icon</param>
        /// <returns>MM Stock icon enumerator, or 0 in case of error</returns>
        public static MmStockIcon StockIconFromString(string aStockIcon)
        {
            aStockIcon = aStockIcon.ToLower();
            switch (aStockIcon)
            {
                case "stockunknown":
                    return MmStockIcon.mmStockIconUnknown;
                case "stocksmiley-happy":
                    return MmStockIcon.mmStockIconSmileyHappy;
                case "stocksmiley-neutral":
                    return MmStockIcon.mmStockIconSmileyNeutral;
                case "stocksmiley-sad":
                    return MmStockIcon.mmStockIconSmileySad;
                case "stocksmiley-angry":
                    return MmStockIcon.mmStockIconSmileyAngry;
                case "stocksmiley-screaming":
                    return MmStockIcon.mmStockIconSmileyScreaming;
                case "stockclock":
                    return MmStockIcon.mmStockIconClock;
                case "stocktime_15":
                    return MmStockIcon.mmStockIconClock; // double
                case "stockcalendar":
                    return MmStockIcon.mmStockIconCalendar;
                case "stocksingles_calendar":
                    return MmStockIcon.mmStockIconCalendar; // double
                case "stockletter":
                    return MmStockIcon.mmStockIconLetter;
                case "stockdocuments_7":
                    return MmStockIcon.mmStockIconLetter; // double
                case "stockemail":
                    return MmStockIcon.mmStockIconEmail;
                case "stocksymbol_04":
                    return MmStockIcon.mmStockIconEmail; // double
                case "stockmailbox":
                    return MmStockIcon.mmStockIconMailbox;
                case "stocksingles_mailbox":
                    return MmStockIcon.mmStockIconMailbox; // double
                case "stockmegaphone":
                    return MmStockIcon.mmStockIconMegaphone;
                case "stocksingles_megaphone":
                    return MmStockIcon.mmStockIconMegaphone; // double
                case "stockhouse":
                    return MmStockIcon.mmStockIconHouse;
                case "stocksingles_house":
                    return MmStockIcon.mmStockIconHouse; // double
                case "stockrolodex":
                    return MmStockIcon.mmStockIconRolodex;
                case "stocksingles_id_card":
                    return MmStockIcon.mmStockIconRolodex; // double
                case "stockdollar":
                    return MmStockIcon.mmStockIconDollar;
                case "stocksingles_dollar":
                    return MmStockIcon.mmStockIconDollar; // double
                case "stockeuro":
                    return MmStockIcon.mmStockIconEuro;
                case "stockcurrencycurrency_3":
                    return MmStockIcon.mmStockIconEuro; // double
                case "stockflag-red":
                    return MmStockIcon.mmStockIconFlagRed;
                case "stockflag-blue":
                    return MmStockIcon.mmStockIconFlagBlue;
                case "stockflag-green":
                    return MmStockIcon.mmStockIconFlagGreen;
                case "stockflag-black":
                    return MmStockIcon.mmStockIconFlagBlack;
                case "stockflag-orange":
                    return MmStockIcon.mmStockIconFlagOrange;
                case "stockflag-yellow":
                    return MmStockIcon.mmStockIconFlagYellow;
                case "stockflag-purple":
                    return MmStockIcon.mmStockIconFlagPurple;
                case "stocktraffic-lights-red":
                    return MmStockIcon.mmStockIconTrafficLightsRed;
                case "stocksigns_01":
                    return MmStockIcon.mmStockIconTrafficLightsRed; // double
                case "stockmarker1":
                    return MmStockIcon.mmStockIconMarker1;
                case "stockmarker2":
                    return MmStockIcon.mmStockIconMarker2;
                case "stockmarker3":
                    return MmStockIcon.mmStockIconMarker3;
                case "stockmarker4":
                    return MmStockIcon.mmStockIconMarker4;
                case "stockmarker5":
                    return MmStockIcon.mmStockIconMarker5;
                case "stockmarker6":
                    return MmStockIcon.mmStockIconMarker6;
                case "stockmarker7":
                    return MmStockIcon.mmStockIconMarker7;
                case "stockresource1":
                    return MmStockIcon.mmStockIconResource1;
                case "stockset_user_blue":
                    return MmStockIcon.mmStockIconResource1; // double
                case "stockresource2":
                    return MmStockIcon.mmStockIconResource2;
                case "stockset_user_red":
                    return MmStockIcon.mmStockIconResource2; // double
                case "stockpadlock-locked":
                    return MmStockIcon.mmStockIconPadlockLocked;
                case "stocksingles_lock":
                    return MmStockIcon.mmStockIconPadlockLocked; // double
                case "stockpadlock-unlocked":
                    return MmStockIcon.mmStockIconPadlockUnlocked;
                case "stocksingles_unlocked":
                    return MmStockIcon.mmStockIconPadlockUnlocked; // double
                case "stockarrow-up":
                    return MmStockIcon.mmStockIconArrowUp;
                case "stockarrowsarrow_19":
                    return MmStockIcon.mmStockIconArrowUp; // double
                case "stockarrow-right":
                    return MmStockIcon.mmStockIconArrowRight;
                case "stockarrowsarrow_25":
                    return MmStockIcon.mmStockIconArrowRight; // double
                case "stocktwo-end-arrow":
                    return MmStockIcon.mmStockIconTwoEndArrow;
                case "stockarrowsarrow_02":
                    return MmStockIcon.mmStockIconTwoEndArrow; // double
                case "stockphone":
                    return MmStockIcon.mmStockIconPhone;
                case "stockcellphone":
                    return MmStockIcon.mmStockIconCellphone;
                case "stocksingles_phone":
                    return MmStockIcon.mmStockIconCellphone; // double
                case "stockcamera":
                    return MmStockIcon.mmStockIconCamera;
                case "stocksingles_camera":
                    return MmStockIcon.mmStockIconCamera; // double
                case "stockfax":
                    return MmStockIcon.mmStockIconFax;
                case "stockstop":
                    return MmStockIcon.mmStockIconStop;
                case "stocksigns_11":
                    return MmStockIcon.mmStockIconStop; // double
                case "stockexclamation-mark":
                    return MmStockIcon.mmStockIconExclamationMark;
                case "stocksigns_08":
                    return MmStockIcon.mmStockIconExclamationMark; // double
                case "stockquestion-mark":
                    return MmStockIcon.mmStockIconQuestionMark;
                case "stockfeedback_q":
                    return MmStockIcon.mmStockIconQuestionMark; // double
                case "stockthumbs-up":
                    return MmStockIcon.mmStockIconThumbsUp;
                case "stockfeedback_10":
                    return MmStockIcon.mmStockIconThumbsUp; // double
                case "stockon-hold":
                    return MmStockIcon.mmStockIconOnHold;
                case "stockhourglass":
                    return MmStockIcon.mmStockIconHourglass;
                case "stocktime_03":
                    return MmStockIcon.mmStockIconHourglass; // double
                case "stockemergency":
                    return MmStockIcon.mmStockIconEmergency;
                case "stocksingles_alarm":
                    return MmStockIcon.mmStockIconEmergency; // double
                case "stockno-entry":
                    return MmStockIcon.mmStockIconNoEntry;
                case "stocksigns_09":
                    return MmStockIcon.mmStockIconNoEntry; // double
                case "stockbomb":
                    return MmStockIcon.mmStockIconBomb;
                case "stocksingles_bomb":
                    return MmStockIcon.mmStockIconBomb; // double
                case "stockkey":
                    return MmStockIcon.mmStockIconKey;
                case "stocksingles_key":
                    return MmStockIcon.mmStockIconKey; // double
                case "stockglasses":
                    return MmStockIcon.mmStockIconGlasses;
                case "stocksingles_glasses":
                    return MmStockIcon.mmStockIconGlasses; // double
                case "stockjudge-hammer":
                    return MmStockIcon.mmStockIconJudgeHammer;
                case "stocksingles_gavel":
                    return MmStockIcon.mmStockIconJudgeHammer; // double
                case "stockrocket":
                    return MmStockIcon.mmStockIconRocket;
                case "stocksingles_rocket":
                    return MmStockIcon.mmStockIconRocket; // double
                case "stockscales":
                    return MmStockIcon.mmStockIconScales;
                case "stocksingles_scale":
                    return MmStockIcon.mmStockIconScales; // double
                case "stockredo":
                    return MmStockIcon.mmStockIconRedo;
                case "stockarrowsarrow_07":
                    return MmStockIcon.mmStockIconRedo; // double
                case "stocklightbulb":
                    return MmStockIcon.mmStockIconLightbulb;
                case "stocksingles_light_bulb":
                    return MmStockIcon.mmStockIconLightbulb; // double
                case "stockcoffee-cup":
                    return MmStockIcon.mmStockIconCoffeeCup;
                case "stocksingles_coffee_2":
                    return MmStockIcon.mmStockIconCoffeeCup; // double
                case "stocktwo-feet":
                    return MmStockIcon.mmStockIconTwoFeet;
                case "stocksingles_foot_print":
                    return MmStockIcon.mmStockIconTwoFeet; // double
                case "stockmeeting":
                    return MmStockIcon.mmStockIconMeeting;
                case "stocksingles_handshake":
                    return MmStockIcon.mmStockIconMeeting; // double
                case "stockcheck":
                    return MmStockIcon.mmStockIconCheck;
                case "stocknote":
                    return MmStockIcon.mmStockIconNote;
                case "stocksingles_note":
                    return MmStockIcon.mmStockIconNote; // double
                case "stockthumbs-down":
                    return MmStockIcon.mmStockIconThumbsDown;
                case "stockfeedback_12":
                    return MmStockIcon.mmStockIconThumbsDown; // double
                case "stockarrow-left":
                    return MmStockIcon.mmStockIconArrowLeft;
                case "stockarrowsarrow_24":
                    return MmStockIcon.mmStockIconArrowLeft; // double
                case "stockarrow-down":
                    return MmStockIcon.mmStockIconArrowDown;
                case "stockarrowsarrow_22":
                    return MmStockIcon.mmStockIconArrowDown; // double
                case "stockbook":
                    return MmStockIcon.mmStockIconBook;
                case "stocksingles_book_2":
                    return MmStockIcon.mmStockIconBook; // double
                case "stockmagnifying-glass":
                    return MmStockIcon.mmStockIconMagnifyingGlass;
                case "stocksingles_magnifying_glass":
                    return MmStockIcon.mmStockIconMagnifyingGlass; // double
                case "stockbroken-connection":
                    return MmStockIcon.mmStockIconBrokenConnection;
                case "stockinformation":
                    return MmStockIcon.mmStockIconInformation;
                case "stockfolder":
                    return MmStockIcon.mmStockIconFolder;
                case "stockdocuments_6":
                    return MmStockIcon.mmStockIconFolder; // double
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets stock icon filename from enumeration
        /// </summary>
        /// <param name="stockEnum">stock icon enumeration</param>
        /// <returns>Stock icon filename</returns>
        public static string StockIconFileName(int stockEnum)
        {
            switch (stockEnum)
            {
                case 1: return "unknown";
                case 2: return "smiley-happy";
                case 3: return "smiley-neutral";
                case 4: return "smiley-sad";
                case 5: return "smiley-angry";
                case 6: return "smiley-screaming";
                case 7: return "clock";
                case 8: return "calendar";
                case 9: return "letter";
                case 10: return "email";
                case 11: return "mailbox";
                case 12: return "megaphone";
                case 13: return "house";
                case 14: return "rolodex";
                case 15: return "dollar";
                case 16: return "euro";
                case 17: return "flag-red";
                case 18: return "flag-blue";
                case 19: return "flag-green";
                case 20: return "flag-black";
                case 21: return "flag-orange";
                case 22: return "flag-yellow";
                case 23: return "flag-purple";
                case 24: return "traffic-lights-red";
                case 25: return "marker1";
                case 26: return "marker2";
                case 27: return "marker3";
                case 28: return "marker4";
                case 29: return "marker5";
                case 30: return "marker6";
                case 31: return "marker7";
                case 32: return "resource1";
                case 33: return "resource2";
                case 34: return "padlock-locked";
                case 35: return "padlock-unlocked";
                case 36: return "arrow-up";
                case 37: return "arrow-right";
                case 38: return "two-end-arrow";
                case 39: return "phone";
                case 40: return "cellphone";
                case 41: return "camera";
                case 42: return "fax";
                case 43: return "stop";
                case 44: return "exclamation-mark";
                case 45: return "question-mark";
                case 46: return "thumbs-up";
                case 47: return "on-hold";
                case 48: return "hourglass";
                case 49: return "emergency";
                case 50: return "no-entry";
                case 51: return "bomb";
                case 52: return "key";
                case 53: return "glasses";
                case 54: return "judge-hammer";
                case 55: return "rocket";
                case 56: return "scales";
                case 57: return "redo";
                case 58: return "lightbulb";
                case 59: return "coffee-cup";
                case 60: return "two-feet";
                case 61: return "meeting";
                case 62: return "check";
                case 63: return "note";
                case 64: return "thumbs-down";
                case 65: return "arrow-left";
                case 66: return "arrow-down";
                case 67: return "book";
                case 68: return "magnifying-glass";
                case 69: return "broken-connection";
                case 70: return "information";
                case 71: return "folder";
                default: return "";
            }
        }

        #endregion

        private List<IconItem> Icons = new List<IconItem>();
        PictureBox selectedIcon = null;
        public string orientation = "H";
        public bool collapsed = false;
        string position;

        int MinLength, RealLength;
        bool groupclicked = false;

        string StickName;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        // For release capture
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public class IconItem
    {
        public IconItem(string name, string filename, int order, string path) 
        {
            IconName = name;
            FileName = filename;
            Order = order;
            Path = path;
        }
        public string IconName = "";
        public string FileName = "";
        public int Order = 0;
        public string Path = "";
    }
}
