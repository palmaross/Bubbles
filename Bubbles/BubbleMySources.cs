using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class BubbleMySources : Form
    {
        public BubbleMySources(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "MySourcesStick.htm");

            //toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(SourceList, Utils.getString("mysources.sourceview.list"));
            toolTip1.SetToolTip(pictureHandle, stickname);

            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            MinLength = this.Width; RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["BI_new"].Text =Utils.getString("mysources.contextmenu.new");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_new"], "newsticker.png");

            contextMenuStrip1.Items["BI_rename"].Text = Utils.getString("button.rename");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_rename"], "edit.png");

            contextMenuStrip1.Items["BI_paste"].Text = Utils.getString("float_icons.contextmenu.paste");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_paste"], "paste.png");
            contextMenuStrip1.Items["BI_paste"].ToolTipText = Utils.getString("contextmenu.paste.source.tooltip");

            contextMenuStrip1.Items["BI_delete"].Text = Utils.getString("float_icons.contextmenu.delete");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_delete"], "deleteall.png");

            StickUtils.SetCommonContextMenu(contextMenuStrip1, StickUtils.typesources);

            audio = Image.FromFile(Utils.ImagesPath + "ms_audio.png");
            excel = Image.FromFile(Utils.ImagesPath + "ms_excel.png");
            exe = Image.FromFile(Utils.ImagesPath + "ms_exe.png");
            file = Image.FromFile(Utils.ImagesPath + "ms_file.png");
            image = Image.FromFile(Utils.ImagesPath + "ms_img.png");
            macros = Image.FromFile(Utils.ImagesPath + "ms_macros.png");
            map = Image.FromFile(Utils.ImagesPath + "ms_map.png");
            pdf = Image.FromFile(Utils.ImagesPath + "ms_pdf.png");
            txt = Image.FromFile(Utils.ImagesPath + "ms_txt.png");
            video = Image.FromFile(Utils.ImagesPath + "ms_video.png");
            http = Image.FromFile(Utils.ImagesPath + "ms_web.png");
            word = Image.FromFile(Utils.ImagesPath + "ms_word.png");
            youtube = Image.FromFile(Utils.ImagesPath + "ms_youtube.png");
            chm = Image.FromFile(Utils.ImagesPath + "chm.png");

            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from SOURCES where stickID=" + ID + " order by _order");
                foreach (DataRow row in dt.Rows)
                {
                    string title = row["title"].ToString();
                    string path = row["path"].ToString();
                    string type = row["type"].ToString();
                    int order = Convert.ToInt32(row["_order"].ToString());

                    Sources.Add(new MySourcesItem(title, path, type, order));
                }
            }

            RefreshStick();

            this.MouseDown += Move_Stick;
            pictureHandle.MouseDown += Move_Stick;
            Manage.Click += Manage_Click;
            pictureHandle.Click += PictureHandle_Click;

            // Handle drag drop to place icon to the begin
            pictureHandle.AllowDrop = true;
            pictureHandle.DragEnter += Handle_DragEnter;
            pictureHandle.DragDrop += Handle_DragDrop;
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse(); // collapse/expand stick

            if (collapsed) {
                collapsed = false; Collapse(); }

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Items["BI_delete"].Visible = false;
            contextMenuStrip1.Items["BI_rename"].Visible = false;
            toolStripSeparator1.Visible = false;

            manage = true;
            contextMenuStrip1.Show(Cursor.Position);
        }

        private void PictureHandle_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = false;

            contextMenuStrip1.Items["BI_paste"].Visible = true;

            selectedIcon = pictureHandle;
            manage = false;
            contextMenuStrip1.Show(Cursor.Position);
        }

        private void Move_Stick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sourcePath, sourceTitle, position;
            if (e.ClickedItem.Name == "BI_new")
            {
                using (NewSourceDlg _dlg = new NewSourceDlg(Sources, manage))
                {
                    if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;
                    sourcePath = _dlg.txtPath.Text;
                    sourceTitle = _dlg.txtTitle.Text;
                }
                NewIcon(sourcePath, sourceTitle, "end");
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                StickUtils.Sources.Clear(); StickUtils.Sources.AddRange(Sources);
                StickUtils.DeleteIcon(selectedIcon, (int)this.Tag, StickUtils.typesources);
                Sources.Clear(); Sources.AddRange(StickUtils.Sources);
                RefreshStick();
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                Sources.Clear();
                RefreshStick(true);
            }
            else if (e.ClickedItem.Name == "BI_rename")
            {
                MySourcesItem item = (MySourcesItem)selectedIcon.Tag;
                if (item == null) return;

                // Get new source's name
                string name = StickUtils.GetName(this, orientation, StickUtils.typesources, item.Title);
                if (name != "")
                {
                    // Change title in the picture box tag
                    ((MySourcesItem)selectedIcon.Tag).Title = name;
                    // Change title in the Source list item
                    Sources.Find(p => p.Path == item.Path).Title = name;
                    toolTip1.SetToolTip(selectedIcon, name);

                    // Change title in the database
                    using (SticksDB db = new SticksDB())
                        db.ExecuteNonQuery("update SOURCES set title=`" + name + "` where path=`" + 
                            item.Path + "` and stickID=" + (int)this.Tag + "");
                }
            }
            else if (e.ClickedItem.Name == "BI_paste")
            {
                string path = (string)Clipboard.GetData(DataFormats.UnicodeText);
                string[] copiedFiles = (string[])Clipboard.GetData(DataFormats.FileDrop);
                if (path == null && copiedFiles == null)
                    return;

                string title = StickUtils.Handle_DragDrop(ref path, copiedFiles, null, Sources);
                if (title == "") return;

                if (path == "" || title == "")
                    return;

                position = "end";
                if (!manage) // if manage - paste at the end
                {
                    if (selectedIcon.Name == "pictureHandle")
                        position = "begin";
                    else
                        position = "right";
                }

                // Get source name
                string name = StickUtils.GetName(this, orientation, StickUtils.typesources, title);
                if (name != "")
                    NewIcon(path, name, position);
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
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "MySourcesStick.htm");
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
                    BubbleMySources form = new BubbleMySources(0, orientation + _collapsed, name);
                    StickUtils.CreateStick(form, name, StickUtils.typesources);
                }
            }
            else if (e.ClickedItem.Name == "BI_renamestick")
            {
                string newName = StickUtils.GetName(this, orientation, StickUtils.typesources, 
                    toolTip1.GetToolTip(pictureHandle), true);
                if (newName != "") toolTip1.SetToolTip(pictureHandle, newName);
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                Collapse();
            }
            else if (e.ClickedItem.Name == "BI_delete_stick")
            {
                if (StickUtils.DeleteStick((int)this.Tag, StickUtils.typesources))
                    this.Close();
            }
        }

        public void Rotate()
        {
            orientation = StickUtils.RotateStick(this, Manage, orientation, SourceList);
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
                StickUtils.Expand(this, RealLength, orientation, contextMenuStrip1);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;


                StickUtils.Collapse(this, orientation, contextMenuStrip1);
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

        private void NewIcon(string sourcePath, string sourceTitle, string position)
        {
            // Add icon to Sources list
            int order = Sources.Count + 1; // at the end
            if (position == "begin")
                order = 1;
            if (position == "left" || position == "right")
            {
                MySourcesItem _item = (MySourcesItem)selectedIcon.Tag;
                if (position == "left")
                    order = _item.Order == 1 ? 1 : _item.Order;
                else
                    order = _item.Order == Sources.Count ? Sources.Count + 1 : _item.Order + 1;
            }

            string type = GetFileType(sourcePath);

            MySourcesItem item = new MySourcesItem(sourceTitle, sourcePath, type, order);
            using (SticksDB db = new SticksDB())
                db.AddSource(sourceTitle, sourcePath, type, order, (int)this.Tag);

            Sources.Insert(order - 1, item);
            for (int i = 0; i < Sources.Count; i++)
                Sources[i].Order = i + 1;

            RefreshStick();
        }

        public string GetFileType(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            if (path.ToLower().StartsWith("http"))
            {
                if (path.ToLower().Contains("youtube.com"))
                    return "youtube";
                return "http";
            }
            else if (Audio.Contains(ext))
                return "audio";
            else if (Video.Contains(ext))
                return "video";
            else if (Word.Contains(ext))
                return "word";
            else if (Excel.Contains(ext))
                return "excel";
            else if (Images.Contains(ext))
                return "image";
            else if (ext == ".exe")
                return "exe";
            else if (ext == ".mmbas")
                return "macros";
            else if (ext == ".mmap" || ext == ".mmat")
                return "map";
            else if (ext == ".pdf")
                return "pdf";
            else if (ext == ".txt")
                return "txt";
            else if (ext == ".chm")
                return "chm";
            else
                return "file";
        }

        private void SourceList_Click(object sender, EventArgs e)
        {
            if (Sources.Count == 0) return;

            using (MySourcesListDlg dlg = new MySourcesListDlg())
            {
                foreach (var item in Sources)
                {
                    if (item.Type == "exe")
                    {
                        try
                        {
                            Icon appIcon = Icon.ExtractAssociatedIcon(item.Path);
                            dlg.imageList1.Images.Add(item.Title, appIcon.ToBitmap());
                            dlg.listView1.Items.Add(item.Title, item.Title).Tag = item.Path;
                        }
                        catch { dlg.listView1.Items.Add(item.Title, item.Type).Tag = item.Path; }
                    }
                    else
                        dlg.listView1.Items.Add(item.Title, item.Type).Tag = item.Path;
                }

                if (Sources.Count < 8) // If not a big amount, change height to adjust items count 
                    dlg.Height = Sources.Count * (int)(dlg.itemHeight.Height * 1.3);

                // Get source list location
                Rectangle child = dlg.RectangleToScreen(dlg.ClientRectangle);
                dlg.Location = StickUtils.GetChildLocation(this, child, orientation, "sources");
                
                dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        void RefreshStick(bool deleteall = false)
        {
            StickUtils.Sources.Clear(); StickUtils.Sources.AddRange(Sources);
            List<PictureBox> pBoxs = StickUtils.RefreshStick(this, p1, orientation, MinLength, 
                collapsed, StickUtils.typesources, deleteall);

            RealLength = StickUtils.stickLength;

            int i = 0;
            foreach (PictureBox pBox in pBoxs)
            {
                pBox.MouseClick += Icon_Click;
                pBox.MouseMove += PBox_MouseMove;
                pBox.DragEnter += Handle_DragEnter;
                pBox.DragDrop += Handle_DragDrop;
                pBox.MouseDown += PBox_MouseDown;

                if (collapsed && i++ > 0) // if collapsed hide all icons except the first
                    pBox.Visible = false;
            }
        }

        /// <summary>
        /// Open source
        /// </summary>
        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                MySourcesItem item = selectedIcon.Tag as MySourcesItem;
                Process.Start(item.Path);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                contextMenuStrip1.Items["BI_new"].Visible = true;
                contextMenuStrip1.Items["BI_rename"].Visible = true;
                toolStripSeparator1.Visible = true;
                contextMenuStrip1.Items["BI_paste"].Visible = true;
                contextMenuStrip1.Items["BI_delete"].Visible = true;

                manage = false;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        #region DragDrop
        private void Handle_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox))) // Move the picture box
            {
                var source = (PictureBox)e.Data.GetData(typeof(PictureBox)); // moving PB
                int sourceIndex = (source.Tag as MySourcesItem).Order; // moving PB order
                int targetIndex = 0;

                if (sender is PictureBox) // also, can be this form
                {
                    var target = (PictureBox)sender;

                    if (target.Name == "pictureHandle")
                        targetIndex = 0; // move PB to the begin
                    else
                    {
                        // or after the target PB
                        try{ targetIndex = (target.Tag as MySourcesItem).Order; }
                        catch { }
                    }
                }
                else // sender is this form or something else (moving PB to the end)
                    targetIndex = this.Controls.OfType<PictureBox>().Count() - 2; // minus pictureHandle and p1

                if (sourceIndex != targetIndex)
                {
                    // Reorder Sources list
                    Sources.RemoveAt(sourceIndex - 1);
                    if (sourceIndex < targetIndex) { targetIndex--; }
                    Sources.Insert(targetIndex, source.Tag as MySourcesItem);
                    for (int i = 0; i < Sources.Count; i++)
                        Sources[i].Order = i + 1;

                    RefreshStick();
                }
            }
            else // Drop *dragged* data
            {
                string path = (string)e.Data.GetData(DataFormats.UnicodeText, false);
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string title = StickUtils.Handle_DragDrop(ref path, draggedFiles, null, Sources);
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

                    // Get source name
                    string name = StickUtils.GetName(this, orientation, StickUtils.typesources, title);
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

        private void PBox_MouseDown(object sender, MouseEventArgs e)
        {
            cursor = Cursor.Position;
        }
        Point cursor;

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

        public List<MySourcesItem> Sources = new List<MySourcesItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";
        bool manage = false;

        int MinLength, RealLength;
        bool collapsed = false;

        string new_icon, position; 

        public static Image audio, excel, exe, file, image, macros, map, pdf, txt, video, http, word, youtube, chm;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public readonly List<string> Images = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png", ".ico" };
        public readonly List<string> Audio = new List<string> { ".aiff", ".au", ".midi", ".mp3", ".m4a", ".wav", ".wma" };
        public readonly List<string> Video = new List<string> { ".asf", ".avi", ".mp4", ".mov", ".m4v", ".mpg", ".mpeg", ".wmv" };
        public readonly List<string> Word = new List<string> { ".doc", ".docm", ".docx", ".rtf" };
        public readonly List<string> Excel = new List<string> { ".xls", ".xlsx", ".xlsm" };
    }

    public class MySourcesItem
    {
        public MySourcesItem(string title, string path, string type, int order)
        {
            Order = order;
            Path = path;
            Title = title;
            Type = type;
        }

        public string Title = "";
        public int Order = 0;
        public string Path = "";
        public string Type = "";
    }
}
