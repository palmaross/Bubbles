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
    internal partial class BubbleSources : Form
    {
        public BubbleSources(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "MySourcesStick.htm");

            //toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(SourceList, Utils.getString("mysources.sourceview.list"));
            toolTip1.SetToolTip(pictureHandle, stickname);

            orientation = _orientation; // "H" or "V"

            MinLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            StixUtils.icondist = pIconDist.Width;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["BI_new"].Text =Utils.getString("mysources.contextmenu.new");
            StixUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_new"], "newsticker.png");

            contextMenuStrip1.Items["BI_rename"].Text = Utils.getString("button.rename");
            StixUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_rename"], "edit.png");

            contextMenuStrip1.Items["BI_paste"].Text = Utils.getString("float_icons.contextmenu.paste");
            StixUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_paste"], "paste.png");
            contextMenuStrip1.Items["BI_paste"].ToolTipText = Utils.getString("contextmenu.paste.source.tooltip");

            contextMenuStrip1.Items["BI_delete"].Text = Utils.getString("float_icons.contextmenu.delete");
            StixUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_delete"], "deleteall.png");

            StixUtils.SetCommonContextMenu(contextMenuStrip1, StixUtils.typesources);

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

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from SOURCES where stickID=" + ID + " order by _order");
                foreach (DataRow row in dt.Rows)
                {
                    string title = row["title"].ToString();
                    string path = row["path"].ToString();
                    string type = row["type"].ToString();
                    int order = Convert.ToInt32(row["_order"].ToString());

                    Sources.Add(new SourceItem(title, path, type, order));
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
            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();

            // Apply scale factor
            this.Paint += this_Paint; // paint the border depending on scale factor
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            scaleFactor = toScale;

            if (scale != 1)
            {
                this.Scale(new SizeF(scale, scale)); // reset to 100%
                StixUtils.icondist = pIconDist.Width;
                MinLength = (int)(MinLength * scale);
            }

            if (toScale != 100)
            {
                this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale
                StixUtils.icondist = pIconDist.Width;
                MinLength = (int)(MinLength * (toScale / 100));
            }
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            if (scaleFactor < 125) return;
            int width = 1;
            //if (scaleFactor > 200) width = 2;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid);
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
                StixUtils.Sources.Clear(); StixUtils.Sources.AddRange(Sources);
                StixUtils.DeleteIcon(selectedIcon, (int)this.Tag, StixUtils.typesources);
                Sources.Clear(); Sources.AddRange(StixUtils.Sources);
                RefreshStick();
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                Sources.Clear();
                RefreshStick(true);
            }
            else if (e.ClickedItem.Name == "BI_rename")
            {
                SourceItem item = (SourceItem)selectedIcon.Tag;
                if (item == null) return;

                // Get new source's name
                string name = StixUtils.GetName(this, orientation, StixUtils.typesources, item.Title);
                if (name != "")
                {
                    // Change title in the picture box tag
                    ((SourceItem)selectedIcon.Tag).Title = name;
                    // Change title in the Source list item
                    Sources.Find(p => p.Path == item.Path).Title = name;
                    toolTip1.SetToolTip(selectedIcon, name);

                    // Change title in the database
                    using (StixDB db = new StixDB())
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

                string title = StixUtils.Handle_DragDrop(ref path, copiedFiles, null, Sources);
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
                string name = StixUtils.GetName(this, orientation, StixUtils.typesources, title);
                if (name != "")
                    NewIcon(path, name, position);
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixButton.STICKS.Remove((int)this.Tag);
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
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_newstick")
            {
                string name = StixUtils.GetName(this, orientation, StixUtils.typestick, "");
                if (name != "")
                {
                    BubbleSources form = new BubbleSources(0, orientation, name);
                    StixUtils.CreateStick(form, name, StixUtils.typesources);
                }
            }
            else if (e.ClickedItem.Name == "BI_renamestick")
            {
                string newName = StixUtils.GetName(this, orientation, StixUtils.typesources, 
                    toolTip1.GetToolTip(pictureHandle), true);
                if (newName != "") toolTip1.SetToolTip(pictureHandle, newName);
            }
            else if (e.ClickedItem.Name == "BI_delete_stick")
            {
                if (StixUtils.DeleteStick((int)this.Tag, StixUtils.typesources))
                    this.Close();
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typesources, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation, SourceList);
        }

        private void NewIcon(string sourcePath, string sourceTitle, string position)
        {
            // Add icon to Sources list
            int order = Sources.Count + 1; // at the end
            if (position == "begin")
                order = 1;
            if (position == "left" || position == "right")
            {
                SourceItem _item = (SourceItem)selectedIcon.Tag;
                if (position == "left")
                    order = _item.Order == 1 ? 1 : _item.Order;
                else
                    order = _item.Order == Sources.Count ? Sources.Count + 1 : _item.Order + 1;
            }

            string type = GetFileType(sourcePath);

            SourceItem item = new SourceItem(sourceTitle, sourcePath, type, order);
            using (StixDB db = new StixDB())
                db.AddSource(sourceTitle, sourcePath, type, order, (int)this.Tag, 0);

            Sources.Insert(order - 1, item);
            for (int i = 0; i < Sources.Count; i++)
                Sources[i].Order = i + 1;

            RefreshStick();
        }

        public static string GetFileType(string path)
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

        SourceListDlg aSourceList = null;
        private void SourceList_Click(object sender, EventArgs e)
        {
            if (Sources.Count == 0) return;

            if (aSourceList == null || !aSourceList.Visible)
            {
                aSourceList = null;
                aSourceList = new SourceListDlg();
            }
            else return;

            foreach (var item in Sources)
            {
                if (item.Type == "exe")
                {
                    try
                    {
                        Icon appIcon = Icon.ExtractAssociatedIcon(item.Path);
                        aSourceList.imageList1.Images.Add(item.Title, appIcon.ToBitmap());
                        aSourceList.listView1.Items.Add(" " + item.Title, item.Title).Tag = item.Path;
                    }
                    catch { aSourceList.listView1.Items.Add(" " + item.Title, item.Type).Tag = item.Path; }
                }
                else
                    aSourceList.listView1.Items.Add(" " + item.Title, item.Type).Tag = item.Path;
            }

            int itemheight = aSourceList.listView1.GetItemRect(0).Height;
            if (Sources.Count <= 10) // If not a big amount, change height to adjust items count 
            {
                aSourceList.thisHeight = Sources.Count * itemheight + aSourceList.itemHeight.Width;
                aSourceList.listView1.Scrollable = false;
            }

            // Get source list location
            Rectangle child = aSourceList.RectangleToScreen(aSourceList.ClientRectangle);
            aSourceList.Location = StixUtils.GetChildLocation(this, child, orientation, "sources");

            aSourceList.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        void RefreshStick(bool deleteall = false)
        {
            StixUtils.Sources.Clear(); StixUtils.Sources.AddRange(Sources);
            List<PictureBox> pBoxs = StixUtils.RefreshStick(this, p1, orientation, MinLength, 
                StixUtils.typesources, deleteall);

            int i = 0;
            foreach (PictureBox pBox in pBoxs)
            {
                pBox.MouseClick += Icon_Click;
                pBox.MouseMove += PBox_MouseMove;
                pBox.DragEnter += Handle_DragEnter;
                pBox.DragDrop += Handle_DragDrop;
                pBox.MouseDown += PBox_MouseDown;
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
                SourceItem item = selectedIcon.Tag as SourceItem;
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
                int sourceIndex = (source.Tag as SourceItem).Order; // moving PB order
                int targetIndex = 0;

                if (sender is PictureBox) // also, can be this form
                {
                    var target = (PictureBox)sender;

                    if (target.Name == "pictureHandle")
                        targetIndex = 0; // move PB to the begin
                    else
                    {
                        // or after the target PB
                        try{ targetIndex = (target.Tag as SourceItem).Order; }
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
                    Sources.Insert(targetIndex, source.Tag as SourceItem);
                    for (int i = 0; i < Sources.Count; i++)
                        Sources[i].Order = i + 1;

                    RefreshStick();
                }
            }
            else // Drop *dragged* data
            {
                string path = (string)e.Data.GetData(DataFormats.UnicodeText, false);
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string title = StixUtils.Handle_DragDrop(ref path, draggedFiles, null, Sources);
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
                    string name = StixUtils.GetName(this, orientation, StixUtils.typesources, title);
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

        public List<SourceItem> Sources = new List<SourceItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";
        bool manage = false;

        int MinLength;
        public float scaleFactor = 100;

        string position; 

        public static Image audio, excel, exe, file, image, macros, map, pdf, txt, video, http, word, youtube, chm;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static readonly List<string> Images = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png", ".ico" };
        public static readonly List<string> Audio = new List<string> { ".aiff", ".au", ".midi", ".mp3", ".m4a", ".wav", ".wma" };
        public static readonly List<string> Video = new List<string> { ".asf", ".avi", ".mp4", ".mov", ".m4v", ".mpg", ".mpeg", ".wmv" };
        public static readonly List<string> Word = new List<string> { ".doc", ".docm", ".docx", ".rtf" };
        public static readonly List<string> Excel = new List<string> { ".xls", ".xlsx", ".xlsm" };
    }

    public class SourceItem
    {
        public SourceItem(string title, string path, string type, int order)
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
