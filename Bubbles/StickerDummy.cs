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
        public StickerDummy(StickerItem item, Point location, int ID = 0)
        {
            InitializeComponent();

            if (item == null)
                return;

            toolTip1.SetToolTip(btnNew, Utils.getString("stickers.btnNew.tooltip"));
            toolTip1.SetToolTip(pSave, Utils.getString("stickers.pSave.tooltip"));
            toolTip1.SetToolTip(pDelete, Utils.getString("stickers.pDelete.tooltip"));
            toolTip1.SetToolTip(pHelp, Utils.getString("stickers.pHelp.tooltip"));
            toolTip1.SetToolTip(btnClose, Utils.getString("button.close"));
            toolTip1.SetToolTip(fontcolor, Utils.getString("stickers.fontcolor.tooltip"));
            toolTip1.SetToolTip(fillcolor, Utils.getString("stickers.fillcolor.tooltip"));
            toolTip1.SetToolTip(pFont, Utils.getString("stickers.pFont.tooltip"));
            toolTip1.SetToolTip(pBold, Utils.getString("stickers.pBold.tooltip"));
            toolTip1.SetToolTip(pIncreaseFont, Utils.getString("stickers.pIncreaseFont.tooltip"));
            toolTip1.SetToolTip(pDecreaseFont, Utils.getString("stickers.pDecreaseFont.tooltip"));
            toolTip1.SetToolTip(pAlign, Utils.getString("stickers.pAlign.tooltip"));
            toolTip1.SetToolTip(pAddImage, Utils.getString("stickers.pAddImage.tooltip"));

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            // ... for the image as well
            this.Resize += (object sender, EventArgs e) => { pStickerImage.Refresh(); };

            // Allow to move the form with Editor panel and Control top and bottom panels
            Editor.MouseDown += Editor_MouseDown;
            panelControlTop.MouseDown += PanelControl_MouseDown;
            panelControlBottom.MouseDown += PanelControl_MouseDown;

            /////// Context Menus ///////

            // Button New context menus
            contextMenuMain.ItemClicked += ContextMenuMain_ItemClicked;

            contextMenuMain.Items["CM_newsticker"].Text = Utils.getString("stickers.contextmenu.newsticker");
            contextMenuMain.Items["CM_mystickers"].Text = Utils.getString("stickers.contextmenu.mystickers");
            contextMenuMain.Items["CM_templates"].Text = Utils.getString("stickers.contextmenu.templates");
            AddStickers();

            // Font Family context menu
            contextMenuFontFamily.ItemClicked += (object sender, ToolStripItemClickedEventArgs e) =>
            {
                Editor.Font = new Font(e.ClickedItem.Text, Editor.Font.Size, Editor.Font.Style);
            };

            // Font Size context menu
            F_Size.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                int fsize = Convert.ToInt32(F_Size.SelectedItem.ToString());
                Editor.Font = new Font(Editor.Font.FontFamily, fsize, Editor.Font.Style);
            };

            // Editor context menu
            contextMenuEditor.Items["CM_copy"].Text = Utils.getString("DummySticker.contextmenu.copy");
            contextMenuEditor.Items["CM_paste"].Text = Utils.getString("DummySticker.contextmenu.paste");
            contextMenuEditor.Items["CM_pasteinside"].Text = Utils.getString("DummySticker.contextmenu.pasteinside");
            contextMenuEditor.Items["CM_edit"].Text = Utils.getString("DummySticker.contextmenu.editmode");
            contextMenuEditor.ItemClicked += ContextMenuEditor_ItemClicked;

            // Other context menus
            contextMenuOther.ItemClicked += ContextMenuOther_ItemClicked;

            // To hide caret and selection in the Not-Edit Mode
            Editor.MouseEnter += Editor_GotFocus;
            Editor.MouseLeave += Editor_MouseLeave;
            Editor.GotFocus += Editor_GotFocus;
            Editor.LostFocus += Editor_LostFocus;
            Editor.SelectionChanged += (object sender, EventArgs e) => { if (!edit) Editor.DeselectAll(); };

            panelControlTop.MouseLeave += Editor_LostFocus;
            panelControlBottom.MouseLeave += Editor_LostFocus;

            // Hide control panels
            this.Deactivate += (object sender, EventArgs e) =>
            {
                panelControlTop.Visible = false;
                panelControlBottom.Visible = false;
            };

            // Fill Editor content
            Editor.Text = item.Content;
            Editor.ForeColor = ColorTranslator.FromHtml(item.TextColor);
            Editor.BackColor = ColorTranslator.FromHtml(item.FillColor);
            this.BackColor = Editor.BackColor;
            Editor.Font = new Font(item.FontFamily, item.TextSize, item.TextBold == 1 ? FontStyle.Bold : FontStyle.Regular);

            // Editor text alignment. We can do this only with editor in edit mode
            edit = true;
            Editor.SelectAll();
            Editor.SelectionAlignment = item.Alignment == "left" ? HorizontalAlignment.Left :
                item.Alignment == "center" ? HorizontalAlignment.Center : HorizontalAlignment.Right;
            Editor.DeselectAll();
            edit = false;

            // Set stick location
            if (location.X + location.Y == 0)  // "Starting" stick (Stick button in the main menu)
                Location = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                    MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);
            else
                Location = location; // Stick from database or new created by user

            this.Tag = ID; // Stick ID

            // Set Stick image (if any)
            if (item.aImage != "")
            {
                string image = item.aImage;
                string[] parts = item.aImage.Split(':');
                if (File.Exists(Utils.m_dataPath + "ImageDB\\" + parts[0]))
                {
                    pStickerImage.ImageLocation = Utils.m_dataPath + "ImageDB\\" + parts[0];
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

            fontcolor.Tag = item.TextColor;
            fontcolor.Paint += pVisualStatus_Paint;
            fillcolor.Tag = item.FillColor;
            // Paint picture box with given color
            fillcolor.Paint += pVisualStatus_Paint;

            // DragDrop images from library to sticker
            this.DragEnter += StickerDummy_DragEnter;
            this.DragDrop += StickerDummy_DragDrop;
            Editor.AllowDrop = true;
            HookChildrenEvents();
        }

        private void HookChildrenEvents()
        {
            foreach (Control child in this.Controls)
            {
                child.DragEnter += StickerDummy_DragEnter;
                child.DragDrop += StickerDummy_DragDrop;
            }
        }

        private void StickerDummy_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string pic in ((string[])e.Data.GetData(DataFormats.FileDrop)))
            {
                Image img = Image.FromFile(pic);
                pStickerImage.Image = img;
            }
        }

        private void StickerDummy_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// My Stickers and Templates dropdowns (Add New button context menu)
        /// </summary>
        void AddStickers()
        {
            ToolStripMenuItem MyStickers = contextMenuMain.Items["CM_mystickers"] as ToolStripMenuItem;
            ToolStripMenuItem Templates = contextMenuMain.Items["CM_templates"] as ToolStripMenuItem;
            ToolStripMenuItem Reminders = contextMenuMain.Items["CM_Reminders"] as ToolStripMenuItem;
            ToolStripItem item = null;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKERS");
                foreach (DataRow row in dt.Rows)
                {
                    if (row["type"].ToString() == "template")
                    {
                        item = Templates.DropDown.Items.Add(row["content"].ToString());
                        item.Name = "template";
                    }
                    else if (row["type"].ToString() == "sticker")
                    {
                        item = MyStickers.DropDown.Items.Add(row["content"].ToString());
                        item.Name = "mysticker";
                    }
                    else if (row["type"].ToString() == "reminder")
                    {
                        item = Reminders.DropDown.Items.Add(row["content"].ToString());
                        item.Name = "reminder";
                    }
                    item.Tag = row["id"];
                }
            }
            MyStickers.DropDown.ItemClicked += ContextMenuMain_ItemClicked;
            Templates.DropDown.ItemClicked += ContextMenuMain_ItemClicked;
            Reminders.DropDown.ItemClicked += ContextMenuMain_ItemClicked;
        }

        private void ContextMenuEditor_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "CM_copy")
            {
                if (String.IsNullOrEmpty(Editor.SelectedText))
                    Clipboard.SetText(Editor.Text);
                else
                    Clipboard.SetText(Editor.SelectedText);
            }
            else if (e.ClickedItem.Name == "CM_paste")
            {
                Editor.Text = Clipboard.GetText();
            }
            else if (e.ClickedItem.Name == "CM_pasteinside")
            {
                Editor.SelectedText = "";
                Clipboard.SetText(Clipboard.GetText());
                Editor.Paste();
            }
            else if (e.ClickedItem.Name == "CM_edit")
            {
                if (edit)
                    edit = false;
                else
                    edit = true;
            }
        }

        private void ContextMenuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "CM_newsticker")
            {
                NewSticker();
            }
            else if (e.ClickedItem.Name == "mysticker" || e.ClickedItem.Name == "template")
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
            else if (e.ClickedItem.Name == "CM_FromLibrary" || e.ClickedItem.Name == "CM_FromFile")
            {
                string imagePath = ""; bool fromLibrary = false;

                if (e.ClickedItem.Name == "CM_FromLibrary")
                {
                    using (SelectImageDlg dlg = new SelectImageDlg())
                    {
                        if (dlg.ShowDialog() == DialogResult.OK)
                            imagePath = dlg.iconPath;
                        else
                            return;
                        fromLibrary = true;
                    }
                }
                else if (e.ClickedItem.Name == "CM_FromFile")
                {
                    openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    openFileDialog1.Title = Utils.getString("stickerdummy.openfiledialog.title");
                    openFileDialog1.Filter = "Image Files|*.bmp;*.gif;*.ico;*.jpg;*.jpeg;*.png;";
                    openFileDialog1.RestoreDirectory = true;

                    if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
                        return;

                    imagePath = openFileDialog1.FileName;
                    string filename = Path.GetFileName(imagePath);

                    try {
                        File.Copy(imagePath, Utils.m_dataPath + "ImageDB\\" + filename, false);
                    } catch { }
                }

                Image img = Image.FromFile(imagePath);
                if (fromLibrary)
                    img = new Bitmap(img, pImageLibrary.Size);

                if (img.Width > Editor.Width || img.Height > Editor.Height)
                {
                    pStickerImage.Size = SetImage(img.Size);
                    pStickerImage.Location = new Point(Editor.Location.X, Editor.Location.Y);
                    pStickerImage.ImageLocation = imagePath;
                }
                else
                {
                    if (!fromLibrary)
                    {
                        float sf = ScalingFactor.GetScalingFactor();
                        pStickerImage.Size = new Size((int)(img.Width * sf), (int)(img.Height * sf));
                    }
                    pStickerImage.ImageLocation = imagePath;
                }
                pStickerImage.Visible = true;
            }
        }

        private Size SetImage(Size size)
        {
            float s = (float)size.Width / size.Height;

            if (size.Width > Editor.Width)
            {
                size.Width = Editor.Width;
                size.Height = (int)(size.Width / s);
            }
            if (size.Height > Editor.Height)
            {
                size.Height = Editor.Height;
                size.Width = (int)(size.Height * s);
            }

            //img = new Bitmap(img, new Size(imgSize.Width, imgSize.Height));
            return size;
        }

        private void btnNew_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NewSticker();
            }
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripMenuItem item in contextMenuMain.Items)
                {
                    item.Visible = true;
                }
                contextMenuMain.Show(Cursor.Position);
            }
        }

        private void NewSticker()
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
                new StickerItem(0, "Your Text...", fontcolor.Tag.ToString(), fillcolor.Tag.ToString(),
                Editor.Font.FontFamily.Name, (int)Editor.Font.Size, Convert.ToInt32(Editor.Font.Bold),
                this.Width + ":" + this.Height, image, alignment, pStickerType.Tag.ToString()), location);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
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

        //private void F_Size_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int fsize = Convert.ToInt32(F_Size.SelectedItem.ToString());
        //    Editor.Font = new Font(Editor.Font.FontFamily, fsize, Editor.Font.Style);
        //}

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

            if (MessageBox.Show(Utils.getString("stickers.deletesticker.text"), 
                Utils.getString("stickers.deletesticker.title"), 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("delete from STICKERS where id=" + Convert.ToInt32(this.Tag) + "");

                this.Close();
            }
        }

        /// <summary>
        /// Release richTextBox to allow move the form.
        /// When double click, enter in edit mode (you can't move the form).
        /// </summary>
        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1) // single click
                {
                    if (!edit)
                    {
                        ReleaseCapture();
                        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    }
                }
                else // double click turn on Edit Mode
                    edit = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuEditor.Items)
                    item.Visible = true;

                if (!edit)
                    contextMenuEditor.Items["CM_pasteinside"].Visible = false;

                contextMenuEditor.Show(Cursor.Position);
            }
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

        private void pAddImage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuOther.Items)
                item.Visible = false;

            contextMenuOther.Items["CM_FromFile"].Visible = true;
            contextMenuOther.Items["CM_FromLibrary"].Visible = true;
            contextMenuOther.Show(Cursor.Position);
        }

        private void pStickerImage_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ToolStripItem item in contextMenuOther.Items)
                item.Visible = false;

            contextMenuOther.Items["CM_DeleteImage"].Visible = true;
            contextMenuOther.Show(Cursor.Position);
        }

        private void pHelp_Click(object sender, EventArgs e)
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
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute,
            ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);
    }

    public class StickerItem
    {
        public StickerItem(int id, string content, string textcolor, string fillcolor, string fontfamily, 
            int textsize, int textbold, string sticksize, string image, string alignment, string type)
        {
            ID = id;
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
        public int ID;
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
