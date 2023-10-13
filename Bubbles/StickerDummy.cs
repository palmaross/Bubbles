using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class StickerDummy : Form
    {
        public StickerDummy(StickerItem item, Point location)
        {
            InitializeComponent();

            if (item == null)
                return;

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Allow move the form with Editor control and Control panels (top and bottom)
            Editor.MouseDown += Editor_MouseDown;
            panelControlTop.MouseDown += PanelControl_MouseDown;
            panelControlBottom.MouseDown += PanelControl_MouseDown;

            // 
            Editor.SelectAll();
            Editor.SelectionAlignment = HorizontalAlignment.Center;
            this.Resize += StickerDummy_Resize;

            // Font Family context menu
            contextMenuFontFamily.ItemClicked += ContextMenuFontFamily_ItemClicked;

            // Font Size context menu
            F_Size.SelectedIndexChanged += F_Size_SelectedIndexChanged;

            // Editor context menu
            contextMenuEditor.ItemClicked += ContextMenuEditor_ItemClicked;
            contextMenuEditor.Items["CM_pasteinside"].Text = MMUtils.getString("DummySticker.contextmenu.pasteinside");

            // Buttton New context menus
            contextMenuMain.ItemClicked += ContextMenuMain_ItemClicked;

            ToolStripMenuItem Templates = contextMenuMain.Items["CM_templates"] as ToolStripMenuItem;
            contextMenuMain.Items["CM_mystickers"].Text = MMUtils.getString("stickers.contextmenu.mystickers");
            contextMenuMain.Items["CM_templates"].Text = MMUtils.getString("stickers.contextmenu.templates");
            AddStickers();

            // Other context menus
            contextMenuOther.ItemClicked += ContextMenuOther_ItemClicked;

            // To hide caret and selection in Move Mode
            Editor.GotFocus += Editor_GotFocus;
            Editor.MouseEnter += Editor_GotFocus;
            Editor.MouseClick += Editor_MouseClick;
            Editor.SelectionChanged += Editor_SelectionChanged;
            Editor.LostFocus += Editor_LostFocus;
            Editor.MouseLeave += Editor_MouseLeave;
            panelControlTop.MouseLeave += Editor_LostFocus;
            panelControlBottom.MouseLeave += Editor_LostFocus;

            this.Deactivate += StickerDummy_Deactivate;

            Editor.Select(0, (Environment.NewLine + Environment.NewLine).Length);
            Editor.SelectionProtected = true;

            Editor.Text = item.Content;
            Editor.ForeColor = ColorTranslator.FromHtml(item.TextColor);
            Editor.BackColor = ColorTranslator.FromHtml(item.FillColor);
            this.BackColor = Editor.BackColor;
            Editor.Font = new Font(item.FontFamily, item.TextSize, item.TextBold == 1 ? FontStyle.Bold : FontStyle.Regular);

            Editor.SelectAll();
            Editor.SelectionAlignment = item.Alignment == "left" ? HorizontalAlignment.Left :
                item.Alignment == "center" ? HorizontalAlignment.Center : HorizontalAlignment.Right;

            if (location.X + location.Y == 0)
            {
                Location = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                    MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);
                this.Tag = "0";
            }
            else
                Location = location;

            if (item.aImage != "")
            {
                string image = item.aImage;
                string[] parts = item.aImage.Split(':');
                if (File.Exists(Utils.m_dataPath + "IconDB\\" + parts[0]))
                {
                    pStickerImage.ImageLocation = Utils.m_dataPath + "IconDB\\" + parts[0];
                    pStickerImage.Location = new Point(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                    pStickerImage.Size = new Size(Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4]));
                    pStickerImage.Visible = true;
                }
            }

            if (item.StickSize != "0")
            {
                string[] size = item.StickSize.Split(':');
                this.Size = new Size(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
            }

            foreach(Control ctrl in this.Controls)
            if (ctrl.GetType() == typeof(VScrollBar))
                ctrl.Width = 20;

            fontcolor.Tag = item.TextColor;
            fontcolor.Paint += pVisualStatus_Paint;
            fillcolor.Tag = item.FillColor;
            fillcolor.Paint += pVisualStatus_Paint;

            pPaste.MouseEnter += PPaste_MouseEnter;
            this.KeyUp += StickerDummy_KeyUp;
            this.KeyDown += StickerDummy_KeyDown;

            // Register mouse events
            pStickerImage.MouseUp += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (null == c) return;
                _dragging = false;
                // Return the picture if it goes beyond the sticker
                if (c.Top < 0) c.Top = 0;
                if (c.Left < 0) c.Left = 0;
                if (c.Bottom > this.Height) c.Top = this.Height - c.Height;
                if (c.Right > this.Width) c.Left = this.Width - c.Width;
            };

            pStickerImage.MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    _dragging = true;
                    _xPos = args.X;
                    _yPos = args.Y;
                }
                else if (args.Button == MouseButtons.Right)
                {
                    foreach (ToolStripItem _item in contextMenuMain.Items)
                        _item.Visible = false;

                    contextMenuOther.Items["CM_DeleteImage"].Visible = true;
                    contextMenuOther.Show(Cursor.Position);
                }
            };

            pStickerImage.MouseMove += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (!_dragging || null == c) return;
                // Don't let the picture go beyond the sticker
                if (c.Top < 0) c.Top = 0;
                else if (c.Left < 0) c.Left = 0;
                else if (c.Bottom > this.Height) c.Top = this.Height - c.Height;
                else if (c.Right > this.Width) c.Left = this.Width - c.Width;
                else
                {
                    c.Top = args.Y + c.Top - _yPos;
                    c.Left = args.X + c.Left - _xPos;
                }
            };
        }

        // Global Variables
        private int _xPos;
        private int _yPos;
        private bool _dragging;

        void AddStickers()
        {
            ToolStripMenuItem MyStickers = contextMenuMain.Items["CM_mystickers"] as ToolStripMenuItem;
            ToolStripMenuItem Templates = contextMenuMain.Items["CM_templates"] as ToolStripMenuItem;
            ToolStripItem item;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKERS");
                foreach (DataRow row in dt.Rows)
                {
                    if (row["template"].ToString() == "1")
                    {
                        item = Templates.DropDown.Items.Add(row["content"].ToString());
                        item.Name = "template";
                    }
                    else
                    {
                        item = MyStickers.DropDown.Items.Add(row["id"].ToString());
                        item.Name = "mysticker";
                    }
                }
            }
            MyStickers.DropDown.ItemClicked += ContextMenuOther_ItemClicked;
            Templates.DropDown.ItemClicked += ContextMenuOther_ItemClicked;
        }

        private void ContextMenuFontFamily_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "F_Verdana" ||
                e.ClickedItem.Name == "F_SansSerif" ||
                e.ClickedItem.Name == "F_ComicSans" ||
                e.ClickedItem.Name == "F_Comfortaa" ||
                e.ClickedItem.Name == "F_Impact" ||
                e.ClickedItem.Name == "F_MiamaNueva" ||
                e.ClickedItem.Name == "F_Pecita" ||
                e.ClickedItem.Name == "F_SegoePrint")
            {
                Editor.Font = new Font(e.ClickedItem.Text, Editor.Font.Size, Editor.Font.Style);
            }
        }

        private void ContextMenuEditor_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "CM_pasteinside")
            {

            }
        }

        private void ContextMenuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "mysticker" || e.ClickedItem.Name == "template")
            {
                int template = e.ClickedItem.Name == "template" ? 1 : 0;
                StickerDummy form = null;// new StickerDummy(null);
                using (BubblesDB db = new BubblesDB())
                {
                    DataTable dt = db.ExecuteQuery("select * from STICKERS where id=`" + e.ClickedItem.Text + "` and template=" + template + "");
                    if (dt.Rows.Count > 0)
                    {
                        var row = dt.Rows[0];
                        string _size = row["sticksize"].ToString();
                        string[] size = _size.Split(':');
                        form.Size = new Size(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
                        form.Editor.Text = row["content"].ToString();
                        form.Editor.Font = new Font(row["fontfamily"].ToString(), Convert.ToInt32(row["textsize"]), Convert.ToInt32(row["textbold"]) == 1 ? FontStyle.Bold : FontStyle.Regular);
                        form.Editor.ForeColor = ColorTranslator.FromHtml(row["textcolor"].ToString());
                        form.Editor.BackColor = ColorTranslator.FromHtml(row["fillcolor"].ToString());
                        form.Tag = row["name"].ToString();
                        form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                    }
                }
            }
        }
        private void ContextMenuOther_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "CM_DeleteImage")
            {
                pStickerImage.Visible = false;
                pStickerImage.ImageLocation = "";
            }
        }

        private void StickerDummy_KeyDown(object sender, KeyEventArgs e)
        {
            if (pPaste.Focused && !pasteFinish)
            {
                pasteFinish = true;
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    ctrlPressed = true;
                }
            }
        }
        bool ctrlPressed = false;

        private void StickerDummy_KeyUp(object sender, KeyEventArgs e)
        {
            pasteFinish = true;
        }
        bool pasteFinish = false;

        private void PPaste_MouseEnter(object sender, EventArgs e)
        {
            pPaste.Focus();
        }

        private void StickerDummy_Resize(object sender, EventArgs e)
        {
            Editor.Height = this.Height - panelControlTop.Height * 2;
        }

        private void pEdit_Click(object sender, EventArgs e)
        {
            if (edit)
                edit = false;
            else
                edit = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Point location = new Point(this.Location.X, this.Location.Y + this.Height);

            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;

            if (location.Y + this.Height > area.Bottom)
                location = new Point(this.Location.X, this.Location.Y - this.Height);

            string alignment = Editor.SelectionAlignment == HorizontalAlignment.Left ? "left" :
                Editor.SelectionAlignment == HorizontalAlignment.Center ? "center" : "right";

            string image = "";
            if (pAddImage.ImageLocation != null && String.IsNullOrEmpty(pAddImage.ImageLocation))
            {
                image = pAddImage.ImageLocation; // path to file
                image += ":" + pAddImage.Location.X + ":" + pAddImage.Location.Y;
            }

            StickerDummy form = new StickerDummy(
                new StickerItem("Your Text...", fontcolor.Tag.ToString(), fillcolor.Tag.ToString(), 
                Editor.Font.FontFamily.Name, (int)Editor.Font.Size, Convert.ToInt32(Editor.Font.Bold),
                this.Width + ":" + this.Height, image, alignment, pStickerType.Tag.ToString()), location);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        private void pMore_Click(object sender, EventArgs e)
        {

        }

        private void fontcolor_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = false;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            Color c = colorDialog1.Color;
            fontcolor.Tag = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
            // "#ffffffff"

            fontcolor.Invalidate(); // change the picture color

            Editor.ForeColor = c;
        }

        private void fillcolor_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = false;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            Color c = colorDialog1.Color;
            fillcolor.Tag = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
            // "#ffffffff"

            fillcolor.Invalidate(); // change the picture color

            Editor.BackColor = c;
            this.BackColor = c;
        }


        private void pFont_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (fontfamily == "Verdana")
                    fontfamily = "Microsoft Sans Serif";
                else if (fontfamily == "Microsoft Sans Serif")
                    fontfamily = "Comic Sans MS";
                else if (fontfamily == "Comic Sans MS")
                    fontfamily = "Comfortaa";
                else if (fontfamily == "Comfortaa")
                    fontfamily = "Impact";
                else if (fontfamily == "Impact")
                    fontfamily = "Miama Nueva";
                else if (fontfamily == "Miama Nueva")
                    fontfamily = "Pecita";
                else if (fontfamily == "Pecita")
                    fontfamily = "Segoe Print";
                else if (fontfamily == "Segoe Print")
                    fontfamily = "Verdana";
                else
                    fontfamily = "Verdana";

                Editor.Font = new Font(fontfamily, Editor.Font.Size, Editor.Font.Style);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripMenuItem item in contextMenuFontFamily.Items)
                {
                    item.Visible = true;
                    if (item.Text == Editor.Font.FontFamily.Name)
                        item.Checked = true;
                    else
                        item.Checked = false;
                }
                contextMenuFontFamily.Show(Cursor.Position);
            }
        }
        string fontfamily = "Verdana";

        private void pBold_Click(object sender, EventArgs e)
        {
            if (Editor.Font.Bold)
            {
                Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size, FontStyle.Regular);
                pBold.Image = Image.FromFile(Utils.ImagesPath + "f_bold.png");
            }
            else
            {
                Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size, FontStyle.Bold);
                pBold.Image = Image.FromFile(Utils.ImagesPath + "f_boldActive.png");
            }
        }

        private void pIncreaseFont_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Editor.Font.Size < 36)
                    Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size + 2, Editor.Font.Style);
            }
            else if (e.Button == MouseButtons.Right)
            {
                F_Size.Width = pAlign.Width * 2;
                contextMenuFontSize.Width = p1.Width;
                contextMenuFontSize.Height = p1.Height;
                contextMenuFontSize.Items["F_Size"].Visible = true;
                F_Size.SelectedItem = Editor.Font.Size.ToString();
                contextMenuFontSize.Show(Cursor.Position);
            }
        }

        private void F_Size_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fsize = Convert.ToInt32(F_Size.SelectedItem.ToString());
            Editor.Font = new Font(Editor.Font.FontFamily, fsize, Editor.Font.Style);
        }

        private void pDecreaseFont_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Editor.Font.Size > 6)
                    Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size - 2, Editor.Font.Style);
            }
            else if (e.Button == MouseButtons.Right)
            {
                F_Size.Width = pAlign.Width * 2;
                contextMenuFontSize.Width = p1.Width;
                contextMenuFontSize.Height = p1.Height;
                contextMenuFontSize.Items["F_Size"].Visible = true;
                F_Size.SelectedItem = Editor.Font.Size.ToString();
                contextMenuFontSize.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Release control paneles capture in order to allow move the form
        /// </summary>
        private void PanelControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Editor_MouseLeave(object sender, EventArgs e)
        {
            if (CursorIsInsideSticker())
                return;

            panelControlTop.Visible = false;
            panelControlBottom.Visible = false;
        }

        private void pVisualStatus_Paint(object sender, PaintEventArgs e)
        {
            PictureBox p = sender as PictureBox;
            Color c = ColorTranslator.FromHtml(p.Tag.ToString());
            SolidBrush myBrush = new SolidBrush(c);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillRectangle(myBrush, p.ClientRectangle);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pAlign_Click(object sender, EventArgs e)
        {
            bool _edit = edit;
            edit = true;
            Editor.SelectAll();
            if (Editor.SelectionAlignment == HorizontalAlignment.Left)
                Editor.SelectionAlignment = HorizontalAlignment.Center;
            else if (Editor.SelectionAlignment == HorizontalAlignment.Center)
                Editor.SelectionAlignment = HorizontalAlignment.Right;
            else if (Editor.SelectionAlignment == HorizontalAlignment.Right)
                Editor.SelectionAlignment = HorizontalAlignment.Left;

            Editor.Select(0, 0);
            edit = _edit;
        }

        private void StickerDummy_Deactivate(object sender, EventArgs e)
        {
            panelControlTop.Visible = false;
            panelControlBottom.Visible = false;
        }

        private void Editor_LostFocus(object sender, EventArgs e)
        {
            if (CursorIsInsideSticker())
                return;

            edit = false;
            panelControlTop.Visible = false;
            panelControlBottom.Visible = false;
        }

        bool CursorIsInsideSticker()
        {
            Rectangle thisForm = new Rectangle(this.Location, this.Size);
            Rectangle cursor = new Rectangle(Cursor.Position, new Size(1,1));

            if (cursor.IntersectsWith(thisForm))
                return true;
            return false;
        }

        private void pSave_Click(object sender, EventArgs e)
        {
            Color c = Editor.ForeColor;
            string fontcolor = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
            c = Editor.BackColor;
            string fillcolor = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();

            string alignment = Editor.SelectionAlignment == HorizontalAlignment.Left ? "left" :
                Editor.SelectionAlignment == HorizontalAlignment.Center ? "center" : "right";

            string image = "";


            using (BubblesDB db = new BubblesDB())
            {
                if (this.Tag == null || string.IsNullOrEmpty(this.Tag.ToString())) // New sticker (id = this.Tag)
                {
                    db.AddSticker(Editor.Text, fontcolor, fillcolor, Editor.Font.FontFamily.Name,
                        (int)Editor.Font.Size, Editor.Font.Bold ? 1 : 0, this.Width + ":" + this.Height,
                        image, alignment, pStickerType.Tag.ToString());

                    // Get auto-created ID
                    DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0)
                        this.Tag = dt.Rows[0][0].ToString();
                }
                else
                {
                    db.ExecuteNonQuery("update STICKERS set " +
                        "content=`" + Editor.Text + "`, " +
                        "textcolor=`" + fontcolor + "`, " +
                        "fillcolor=`" + fillcolor + "`, " +
                        "fontfamily=`" + Editor.Font.FontFamily.Name + "`, " +
                        "textsize=" + (int)Editor.Font.Size + ", " +
                        "textbold=" + (Editor.Font.Bold ? 1 : 0) + ", " +
                        "sticksize=`" + this.Width + ":" + this.Height + "`, " +
                        "image=`" + image + "`, " +
                        "alignment=`" + alignment + "`, " +
                        "type=`" + pStickerType.Tag.ToString() + "`, " +
                        "where id=" + Convert.ToInt32(this.Tag) + "");
                }
            }
        }

        private void pDelete_Click(object sender, EventArgs e)
        {
            if (this.Tag == null || String.IsNullOrEmpty(this.Tag.ToString())) return;

            using (BubblesDB db = new BubblesDB())
                db.ExecuteNonQuery("delete from STICKERS where id=" + Convert.ToInt32(this.Tag) + "");

            this.Close();
        }

        private void pCopy_MouseClick(object sender, MouseEventArgs e)
        {
            if (String.IsNullOrEmpty(Editor.SelectedText))
                Clipboard.SetText(Editor.Text);
            else
                Clipboard.SetText(Editor.SelectedText);
        }

        private void pPaste_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ctrlPressed)
                {
                    Editor.SelectedText = "";
                    Clipboard.SetText(Clipboard.GetText());
                    Editor.Paste();
                }
                else
                    Editor.Text = Clipboard.GetText();
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuMain.Items)
                    item.Visible = false;

                contextMenuMain.Items["CM_pasteinside"].Visible = true;
                contextMenuMain.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Release richTextBox to allow move the form.
        /// When double click, enter in edit mode (you can't move the form).
        /// </summary>
        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (e.Button == MouseButtons.Left && !edit)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            else // double click, turn on Edit Mode
            {
                edit = true;
            }

        }

        private void Editor_SelectionChanged(object sender, EventArgs e)
        {
            if (!edit)
                Editor.DeselectAll();
        }

        [DllImport("user32.dll", EntryPoint = "ShowCaret")]
        public static extern long ShowCaret(IntPtr hwnd);
        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);
        private void Editor_GotFocus(object sender, EventArgs e)
        {
            if (edit)
                ShowCaret(Editor.Handle);
            else
                HideCaret(Editor.Handle);

            if (justCreated)
            {
                justCreated = false;
                return;
            }

            panelControlTop.Visible = true;
            panelControlBottom.Visible = true;
        }
        bool justCreated = true;

        private void Editor_MouseClick(object sender, MouseEventArgs e)
        {
            if (edit)
                ShowCaret(Editor.Handle);
            else
                HideCaret(Editor.Handle);

            if (!edit)
                panelControlTop.Visible = true;
            panelControlBottom.Visible = true;
        }

        private void pStickerImage_MouseClick(object sender, MouseEventArgs e)
        {

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

        bool edit = false;
        public static int DummyStickerWidth;
        public static int DummyStickerHeight;
        public static int DummyStickerImageX;
        public static int DummyStickerImageY;

        ToolStripItemCollection TemplateCollection;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /////
        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        // Copied from dwmapi.h
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);

        private void pAddImage_Click(object sender, EventArgs e)
        {

        }
    }

    public class StickerItem
    {
        public StickerItem(string content, string textcolor, string fillcolor, string fontfamily, 
            int textsize, int textbold, string sticksize, string image, string alignment, string type)
        {
            Content = content;
            TextColor = textcolor;
            FillColor = fillcolor;
            FontFamily = fontfamily;
            TextSize = textsize;
            TextBold = textbold;
            StickSize = sticksize;
            aImage = image;
            Alignment = alignment;
            Type = type;
        }
        public string Content = "/n/nText...";
        public string TextColor = "";
        public string FillColor = "";
        public string FontFamily = "";
        public int TextSize = 9;
        public int TextBold = 0;
        public string StickSize = "";
        public string aImage = "";
        public string Alignment = "center";
        public string Type = "sticker";
    }
}
