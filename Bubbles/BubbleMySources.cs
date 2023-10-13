using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Bubbles
{
    internal partial class BubbleMySources : Form
    {
        public BubbleMySources()
        {
            InitializeComponent();

            //helpProvider1.HelpNamespace = MMMapNavigator.dllPath + "MapNavigator.chm";
            //helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            //helpProvider1.SetHelpKeyword(this, "bookmarks_express.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(SourceList, Utils.getString("mysources.contextmenu.list"));
            toolTip1.SetToolTip(pictureHandle, Utils.getString("mysources.bubble.tooltip"));
            if (Utils.getRegistry("ListIconView", "icons") == "list")
                toolTip1.SetToolTip(SourceList, Utils.getString("mysources.contextmenu.icons"));

            orientation = Utils.getRegistry("OrientationSources", "H");

            MinLength = this.Width;
            MaxLength = this.Width * 4;
            Thickness = this.Height;
            panel1MinLength = panel1.Width;

            this.MinimumSize = this.Size;
            this.MaximumSize = new Size(MaxLength, Thickness);

            if (orientation == "V")
            {
                orientation = "H";
                RotateBubble();
                p1.Location = new Point(p1.Location.Y, p1.Location.X);
            }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            contextMenuStrip1.Items["BI_new"].Text = MMUtils.getString("mysources.contextmenu.new");
            contextMenuStrip1.Items["BI_delete"].Text = MMUtils.getString("float_icons.contextmenu.delete");
            contextMenuStrip1.Items["BI_deleteall"].Text = MMUtils.getString("float_icons.contextmenu.deleteall");

            contextMenuStrip1.Items["BI_rotate"].Text = MMUtils.getString("float_icons.contextmenu.rotate");
            contextMenuStrip1.Items["BI_close"].Text = MMUtils.getString("float_icons.contextmenu.close");
            contextMenuStrip1.Items["BI_help"].Text = MMUtils.getString("float_icons.contextmenu.help");
            contextMenuStrip1.Items["BI_store"].Text = MMUtils.getString("float_icons.contextmenu.settings");

            panel1.MouseClick += Panel1_MouseClick;
            panel1.MouseDown += Panel1_MouseDown;
            pictureHandle.MouseDown += PictureHandle_MouseDown;
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

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from SOURCES order by _order");

                foreach (DataRow row in dt.Rows)
                {
                    string title = row["title"].ToString();
                    string path = row["path"].ToString();
                    string type = row["type"].ToString();
                    int order = Convert.ToInt32(row["_order"].ToString());

                    Sources.Add(new MySourcesItem(title, path, type, order));
                    AddIcon(new MySourcesItem(title, path, type, order));
                }
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Items["BI_delete"].Visible = false;

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

                // Add icon to bubble and to Icons list
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
                    db.AddSource(sourceTitle, sourcePath, type, order);

                Sources.Insert(order - 1, item);
                for (int i = 0; i < Sources.Count; i++)
                    Sources[i].Order = i + 1;

                RefreshBubble();
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                MySourcesItem _item = (MySourcesItem)selectedIcon.Tag;
                string filename = _item.Path;

                if (_item == null) return;

                _item = Sources.Find(x => x.Path == filename);
                Sources.Remove(_item);

                for (int i = 0; i < Sources.Count; i++)
                    Sources[i].Order = i + 1;

                using (BubblesDB db = new BubblesDB())
                {
                    // delete icon from db
                    db.ExecuteNonQuery("delete from SOURCES where path=`" + filename + "`");
                }

                RefreshBubble();
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                RefreshBubble(true);
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                this.Hide();
                BubblesButton.m_bubblesMenu.MySources.Image = BubblesButton.m_bubblesMenu.mwSources;
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                RotateBubble();

                foreach (PictureBox p in panel1.Controls.OfType<PictureBox>())
                {
                    p.Location = new Point(p.Location.Y, p.Location.X);
                }
            }
            else if (e.ClickedItem.Name == "BI_help")
            {

            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                int x = this.Location.X;
                int y = this.Location.Y;

                Utils.setRegistry("OrientationSources", orientation);
                Utils.setRegistry("PositionSources", x.ToString() + "," + y.ToString());
            }
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
            else
                return "file";
        }

        private void SourceList_Click(object sender, EventArgs e)
        {
            if (BubblesButton.m_MySourcesList.Visible)
            {
                BubblesButton.m_MySourcesList.Hide();
                return;
            }

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

            BubblesButton.m_MySourcesList.Location = new Point(X, Y);
            var pos = new Point(X, Y);

            // If the form is close to the right or bottom screen side..
            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;

            if (orientation == "H")
            {
                if (BubblesButton.m_MySourcesList.Location.X + BubblesButton.m_MySourcesList.Width > area.Right)
                    pos.X = area.Right - BubblesButton.m_MySourcesList.Width;

                if (pos.Y + BubblesButton.m_MySourcesList.Height > area.Bottom)
                    pos.Y = pos.Y - p1.Height - BubblesButton.m_MySourcesList.Height;
            }
            else
            {
                if (BubblesButton.m_MySourcesList.Location.X + BubblesButton.m_MySourcesList.Width > area.Right)
                    pos.X = pos.X - p1.Height - BubblesButton.m_MySourcesList.Width;

                if (pos.Y + BubblesButton.m_MySourcesList.Height > area.Bottom)
                    pos.Y = area.Bottom - BubblesButton.m_MySourcesList.Height;
            }

            BubblesButton.m_MySourcesList.Location = new Point(pos.X, pos.Y);

            foreach (var item in Sources)
                BubblesButton.m_MySourcesList.listView1.Items.Add(item.Title, item.Type).Tag = item.Path;

            BubblesButton.m_MySourcesList.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            //using (MySourcesListDlg dlg = new MySourcesListDlg(X, Y, orientation))
            //{
            //    foreach(var item in Sources)
            //    {
            //        dlg.listView1.Items.Add(item.Title, item.Type).Tag = item.Path;
            //    }

            //    dlg.ShowDialog();
            //}
        }

        void RefreshBubble(bool deleteall = false)
        {
            // Remove all icons and comboitems
            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>().Reverse())
            {
                if (p.Name != "p1")
                    p.Dispose();
            }

            // Reset bubble size to minimum
            if (orientation == "H")
            {
                this.Size = new Size(MinLength, Thickness);
                panel1.Width = panel1MinLength;
            }
            else
            {
                this.Size = new Size(Thickness, MinLength);
                panel1.Width = panel1MinLength;
            }

            using (BubblesDB db = new BubblesDB())
            {
                if (deleteall) // Clean bubble and database
                {
                    Sources.Clear();
                    db.ExecuteNonQuery("delete from SOURCES");
                }
                else // Add icons to bubble
                {
                    k = 0;
                    foreach (var item in Sources)
                    {
                        AddIcon(item);
                        db.ExecuteNonQuery("update SOURCES set _order=" + item.Order + " where path=`" + item.Path + "`");
                    }
                }
            }
        }

        void RotateBubble()
        {
            if (orientation == "H")
            {
                orientation = "V";
                panel1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                Manage.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                SourceList.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            }
            else
            {
                orientation = "H";
                panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                Manage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                SourceList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            int thisWidth = this.Width;
            int thisHeight = this.Height;

            Size panel1Size = new Size(panel1.Height, panel1.Width);
            Point panel1Location = new Point(panel1.Location.Y, panel1.Location.X);
            Point ManageLocation = new Point(Manage.Location.Y, Manage.Location.X);
            Point SourceListLocation = new Point(SourceList.Location.Y, SourceList.Location.X);

            if (orientation == "H")
            {
                this.MinimumSize = new Size(MinLength, Thickness);
                this.MaximumSize = new Size(MaxLength, Thickness);
            }
            else
            {
                this.MinimumSize = new Size(Thickness, MinLength);
                this.MaximumSize = new Size(Thickness, MaxLength);
            }

            this.Size = new Size(thisHeight, thisWidth);

            panel1.Size = panel1Size;
            panel1.Location = panel1Location;
            Manage.Location = ManageLocation;
            SourceList.Location = SourceListLocation;
        }

        PictureBox FindByTag(MySourcesItem item)
        {
            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>())
            {
                if (p.Tag != null && (p.Tag as MySourcesItem) == item)
                    return p;
            }
            return null;
        }

        void AddIcon(MySourcesItem item)
        {
            PictureBox pBox = new PictureBox();
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.MouseClick += Icon_Click;
            pBox.AllowDrop = true;
            pBox.MouseMove += PBox_MouseMove;
            pBox.DragEnter += PBox_DragEnter;
            pBox.DragDrop += PBox_DragDrop;
            if (item.Type == "exe")
            {
                try
                {
                    System.Drawing.Icon appIcon = System.Drawing.Icon.ExtractAssociatedIcon(item.Path);
                    pBox.Image = appIcon.ToBitmap();
                }
                catch { pBox.Image = GetImageByType(item.Type); }
            }
            else
                pBox.Image = GetImageByType(item.Type);
            panel1.Controls.Add(pBox);
            pBox.Visible = true;
            toolTip1.SetToolTip(pBox, item.Title);
            pBox.BringToFront();
            pBox.Tag = new MySourcesItem(item.Title, item.Path, item.Type, item.Order);

            if (orientation == "H")
            {
                pBox.Location = new Point(icondist.Width * k++, p1.Location.Y);
                if (k > 4)
                    this.Width += icondist.Width;
            }
            else
            {
                pBox.Location = new Point(p1.Location.X, icondist.Width * k++);
                if (k > 4)
                    this.Height += icondist.Width;
            }
        }
        int k = 0;

        private Image GetImageByType(string type)
        {
            switch (type)
            {
                case "audio": return audio;
                case "excel": return excel;
                case "exe": return exe;
                case "image": return image;
                case "macros": return macros;
                case "map": return map;
                case "pdf": return pdf;
                case "txt": return txt;
                case "video": return video;
                case "http": return http;
                case "word": return word;
                case "youtube": return youtube;
            }
            return file;
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

                        RefreshBubble();
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

        public static List<MySourcesItem> Sources = new List<MySourcesItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";

        int MinLength, MaxLength, Thickness, panel1MinLength;

        Image audio, excel, exe, file, image, macros, map, pdf, txt, video, http, word, youtube;

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

        public override string ToString()
        {
            return Title;
        }

        public string Title = "";
        public int Order = 0;
        public string Path = "";
        public string Type = "";
    }
}
