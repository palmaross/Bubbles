using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bubbles
{
    internal partial class BubbleMySources : Form
    {
        public BubbleMySources(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation;

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "MySourcesStick.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(SourceList, Utils.getString("mysources.sourceview.list"));
            toolTip1.SetToolTip(pictureHandle, stickname);
            if (Utils.getRegistry("MySourcesView", "icons") == "list")
                toolTip1.SetToolTip(SourceList, Utils.getString("mysources.sourceview.icons"));

            orientation = Utils.getRegistry("OrientationSources", "H");

            MinLength = this.Width;
            RealLength = this.Width;
            Thickness = this.Height;
            panel1MinLength = panel1.Width;

            if (orientation == "V")
                orientation = StickUtils.RotateStick(this, p1, panel1, Manage, "H", true);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["BI_new"].Text = MMUtils.getString("mysources.contextmenu.new");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_new"], p2, "newsticker.png");

            contextMenuStrip1.Items["BI_delete"].Text = MMUtils.getString("float_icons.contextmenu.delete");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_delete"], p2, "deleteall.png");

            contextMenuStrip1.Items["BI_deleteall"].Text = MMUtils.getString("float_icons.contextmenu.deleteall");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_deleteall"], p2, "deleteall.png");
            
            contextMenuStrip1.Items["BI_rename"].Text = MMUtils.getString("float_icons.contextmenu.edit");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_rename"], p2, "edit.png");
            
            StickUtils.SetCommonContextMenu(contextMenuStrip1, p2);

            panel1.MouseDown += Panel1_MouseDown;
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            txtName.KeyUp += TxtName_KeyUp;
            this.Deactivate += BubblesIcons_Deactivate;
            Manage.Click += Manage_Click;

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

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from SOURCES order by _order");
                int k = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string title = row["title"].ToString();
                    string path = row["path"].ToString();
                    string type = row["type"].ToString();
                    int order = Convert.ToInt32(row["_order"].ToString());

                    MySourcesItem item = new MySourcesItem(title, path, type, order);
                    Sources.Add(item);
                    PictureBox pBox = StickUtils.AddSource(this, p1, item, path,
                        panel1, orientation, icondist.Width, k++);
                    pBox.MouseClick += Icon_Click;
                    pBox.MouseMove += PBox_MouseMove;
                    pBox.DragEnter += PBox_DragEnter;
                    pBox.DragDrop += PBox_DragDrop;
                }
            }
            RealLength = this.Width;

            panel1.DragEnter += panel1_DragEnter;
            panel1.DragDrop += panel1_DragDrop;
            Manage.AllowDrop = true;
            Manage.DragEnter += panel1_DragEnter;
            Manage.DragDrop += panel1_DragDrop;
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Items["BI_delete"].Visible = false;
            contextMenuStrip1.Items["BI_rename"].Visible = false;

            manage = true;
            contextMenuStrip1.Show(Cursor.Position);
        }
        bool manage = false;

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            txtName.Visible = false;
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sourcePath, sourceTitle, position;
            if (e.ClickedItem.Name == "BI_new")
            {
                using (AddSourceDlg _dlg = new AddSourceDlg())
                {
                    if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;
                    sourcePath = _dlg.txtPath.Text;
                    sourceTitle = _dlg.txtTitle.Text;
                    position = _dlg.rbtnEnd.Checked ? "end" :
                        _dlg.rbtnBegin.Checked ? "begin" :
                        _dlg.rbtnLeft.Checked ? "left" : "right";
                }
                NewIcon(sourcePath, sourceTitle, position);
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
                if (collapsed)
                {
                    collapsed = false;
                    this.BackColor = System.Drawing.Color.Lavender;
                }
            }
            else if (e.ClickedItem.Name == "BI_rename")
            {
                txtName.Visible = true;
                txtName.BringToFront();
                txtName.Location = p1.Location;
                txtName.Focus();
                txtName.Text = ((MySourcesItem)selectedIcon.Tag).Title;
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                orientation = StickUtils.RotateStick(this, p1, panel1, Manage, orientation);
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "MySourcesStick.htm.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StickUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_newstick")
            {
                string name = StickUtils.RenameStick(this, orientation, "");
                if (name != "")
                {
                    BubbleIcons form = new BubbleIcons(0, orientation, name);
                    StickUtils.CreateStick(form, name);
                }
            }
            else if (e.ClickedItem.Name == "BI_renamestick")
            {
                string newName = StickUtils.RenameStick(this, orientation, toolTip1.GetToolTip(pictureHandle));
                if (newName != "") toolTip1.SetToolTip(pictureHandle, newName);
            }
            else if (e.ClickedItem.Name == "BI_expand")
            {
                if (this.Width < RealLength)
                    this.Width = RealLength;
                this.BackColor = Color.Lavender;
                collapsed = false;
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                if (this.Width > MinLength)
                {
                    this.Width = MinLength;
                    this.BackColor = Color.Gainsboro;
                    collapsed = true;
                }
            }
            else if (e.ClickedItem.Name == "BI_delete_stick")
            {
                if (StickUtils.DeleteStick((int)this.Tag, StickUtils.typeicons))
                    this.Close();
            }
        }

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
            using (BubblesDB db = new BubblesDB())
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
            using (MySourcesListDlg list = new MySourcesListDlg())
            {
                int X, Y;
                if (orientation == "H")
                {
                    X = this.Location.X;
                    Y = this.Location.Y + this.Height;
                }
                else
                {
                    X = this.Location.X + this.Width;
                    Y = this.Location.Y;
                }

                list.Location = new Point(X, Y);
                var pos = new Point(X, Y);

                // If the form is close to the right or bottom screen side..
                Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;

                if (orientation == "H")
                {
                    if (list.Location.X + list.Width > area.Right)
                        pos.X = area.Right - list.Width;

                    if (pos.Y + list.Height > area.Bottom)
                        pos.Y = pos.Y - p1.Height - list.Height;
                }
                else
                {
                    if (list.Location.X + list.Width > area.Right)
                        pos.X = pos.X - p1.Height - list.Width;

                    if (pos.Y + list.Height > area.Bottom)
                        pos.Y = area.Bottom - list.Height;
                }

                list.Location = new Point(pos.X, pos.Y);

                foreach (var item in Sources)
                {
                    if (item.Type == "exe")
                    {
                        try
                        {
                            Icon appIcon = Icon.ExtractAssociatedIcon(item.Path);
                            list.imageList1.Images.Add(item.Title, appIcon.ToBitmap());
                            list.listView1.Items.Add(item.Title, item.Title).Tag = item.Path;
                        }
                        catch { list.listView1.Items.Add(item.Title, item.Type).Tag = item.Path; }
                    }
                    else
                        list.listView1.Items.Add(item.Title, item.Type).Tag = item.Path;
                }

                if (Sources.Count < 8) // If not a big amount, change heigt to adjust items count 
                    list.Height = Sources.Count * (int)(p2.Width * 1.3);

                list.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        void RefreshStick(bool deleteall = false)
        {
            StickUtils.Sources.Clear(); StickUtils.Sources.AddRange(Sources);
            List<PictureBox> pBoxs = StickUtils.RefreshStick(this, p1, panel1, orientation, Thickness,
                MinLength, panel1MinLength, icondist.Width, ref RealLength, StickUtils.typesources, deleteall);

            foreach (PictureBox pBox in pBoxs)
            {
                pBox.MouseClick += Icon_Click;
                pBox.MouseMove += PBox_MouseMove;
                pBox.DragEnter += PBox_DragEnter;
                pBox.DragDrop += PBox_DragDrop;
            }

            if (collapsed)
                this.Width = MinLength;
        }

        private void PBox_DragDrop(object sender, DragEventArgs e)
        {
            var target = (PictureBox)sender;
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                var source = (PictureBox)e.Data.GetData(typeof(PictureBox));
                if (source != target)
                {
                    try
                    {
                        int sourceIndex = (source.Tag as MySourcesItem).Order;
                        int targetIndex = (target.Tag as MySourcesItem).Order;
                        Sources.RemoveAt(sourceIndex - 1);
                        if (sourceIndex < targetIndex) { targetIndex--; }
                        Sources.Insert(targetIndex, source.Tag as MySourcesItem);
                        for (int i = 0; i < Sources.Count; i++)
                            Sources[i].Order = i + 1;

                        RefreshStick();
                    }
                    catch { }
                }
            }
        }

        private void PBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void PBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var pb = (PictureBox)sender;
                pb.DoDragDrop(pb, DragDropEffects.Move);
            }
        }

        #region resize dialog
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
                contextMenuStrip1.Items["BI_delete"].Visible = true;
                manage = false;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;

                contextMenuStrip1.Items["BI_delete"].Visible = false;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void TxtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dragging)
                {
                    txtName.Visible = false;
                    AfterDragging();
                    return;
                }

                MySourcesItem item = (MySourcesItem)selectedIcon.Tag;
                if (item == null)
                    return;
                ((IconItem)selectedIcon.Tag).IconName = txtName.Text.Trim();
                Sources.Find(p => p.Path == item.Path).Title = txtName.Text.Trim();
                txtName.Visible = false;
                toolTip1.SetToolTip(selectedIcon, txtName.Text.Trim());

                // Change name in the database
                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("update ICONS set name=`" + txtName.Text.Trim() + "` where filename=`" + item.Path + "`");
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtName.Visible = false;
                if (dragging)
                    AfterDragging();
            }
        }

        private void BubblesIcons_Deactivate(object sender, EventArgs e)
        {
            txtName.Visible = false;
            if (dragging)
                AfterDragging();
        }

        #region DragDrop
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string pic in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                string filename = Path.GetFileNameWithoutExtension(pic);

                foreach (var item in Sources) // проверим, есть ли в стике этот значок
                {
                    if (item.Path == filename + ".ico" || // custom icon
                        item.Path == "stock" + filename) // stock icon
                    {
                        MessageBox.Show(Utils.getString("float_icons.iconexists"));
                        return;
                    }
                }

                dragging = true;
                txtName.Visible = true;
                txtName.Text = filename;
                txtName.BringToFront();
                txtName.Location = p1.Location;
                txtName.Focus();

                new_icon = pic;
            }
        }

        /// <summary>
        /// Add new icon after dragging (wait for user entering icon name in the textbox)
        /// </summary>
        private void AfterDragging()
        {
            //NewIcon(new_icon, txtName.Text.Trim(), "end");
            txtName.Text = "";
            dragging = false;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        string new_icon;
        #endregion

        public static List<MySourcesItem> Sources = new List<MySourcesItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";

        int MinLength, RealLength, Thickness, panel1MinLength;
        bool dragging = false, collapsed = false;

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

    internal class MySourcesItem
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
