using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace Bubbles
{
    internal partial class BubbleNotepad : Form
    {
        public BubbleNotepad()
        {
            InitializeComponent();

            //helpProvider1.HelpNamespace = MMMapNavigator.dllPath + "MapNavigator.chm";
            //helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, Utils.getString("notepad.bubble.tooltip"));

            orientation = Utils.getRegistry("OrientationNotepad", "H");

            MinLength = this.Width;
            MaxLength = this.Width * 4;
            Thickness = this.Height;

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

            // Stickers
            contextMenuStrip1.Items["BI_mystickers"].Text = MMUtils.getString("stickers.contextmenu.mystickers");
            contextMenuStrip1.Items["BI_templates"].Text = MMUtils.getString("stickers.contextmenu.templates");
            contextMenuStrip1.Items["BI_newsticker"].Text = MMUtils.getString("stickers.contextmenu.newsticker");
            //AddMyStickers();

            contextMenuStrip1.Items["BI_rotate"].Text = MMUtils.getString("float_icons.contextmenu.rotate");
            contextMenuStrip1.Items["BI_close"].Text = MMUtils.getString("float_icons.contextmenu.close");
            contextMenuStrip1.Items["BI_help"].Text = MMUtils.getString("float_icons.contextmenu.help");
            contextMenuStrip1.Items["BI_store"].Text = MMUtils.getString("float_icons.contextmenu.settings");

            panel1.MouseClick += Panel1_MouseClick;
            panel1.MouseDown += Panel1_MouseDown;
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            Manage.Click += Manage_Click;

            fontcolor1.MouseClick += Icon_Click;
            fontcolor2.MouseClick += Icon_Click;
            fontcolor3.MouseClick += Icon_Click;
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Items["BI_mystickers"].Visible = false;

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
            if (e.ClickedItem.Name == "BI_newsticker")
            {
                //using (CreateStickerDlg dlg = new CreateStickerDlg())
                //    dlg.ShowDialog();
            }
            else if (e.ClickedItem.Name == "mysticker" || e.ClickedItem.Name == "template")
            {
                int template = e.ClickedItem.Name == "template" ? 1 : 0;
                StickerDummy form = null;// new StickerDummy(null);
                using (BubblesDB db = new BubblesDB())
                {
                    DataTable dt = db.ExecuteQuery("select * from STICKERS where name=`" + e.ClickedItem.Text + "` and template=" + template + "");
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
            else if (e.ClickedItem.Name == "BI_close")
            {
                this.Hide();
                BubblesButton.m_bubblesMenu.Organizer.Image = BubblesButton.m_bubblesMenu.mwOrganizer;
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
                if (!Utils.IsOnMMWindow(this.Bounds))
                {
                    if (MessageBox.Show("Стик находится вне окна MindManager! Уверены, что хотите запомнить эту позицитю?",
                        "Подтвердите позицию",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                }

                Utils.setRegistry("OrientationNotepad", orientation);

                Point rec = Utils.MMScreen(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                    MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);

                string location = Utils.getRegistry("PositionNotepad", "");

                if (!String.IsNullOrEmpty(location))
                {
                    string[] xy = location.Split(';');

                    foreach (string part in xy)
                    {
                        if (part.StartsWith(rec.X + "," + rec.Y))
                        {
                            location = location.Replace(part, rec.X + "," + rec.Y +
                                ":" + this.Location.X + "," + this.Location.Y);
                            Utils.setRegistry("PositionNotepad", location);
                            return;
                        }
                    }
                }
                // screen not found, so new screen
                location += ";" + rec.X + "," + rec.Y + ":" + this.Location.X + "," + this.Location.Y;
                Utils.setRegistry("PositionNotepad", location.TrimStart(';'));
            }
        }

        void RotateBubble()
        {
            if (orientation == "H")
            {
                orientation = "V";
                panel1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                Manage.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            }
            else
            {
                orientation = "H";
                panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                Manage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            int thisWidth = this.Width;
            int thisHeight = this.Height;

            Size panel1Size = new Size(panel1.Height, panel1.Width);
            Point panel1Location = new Point(panel1.Location.Y, panel1.Location.X);
            Point ManageLocation = new Point(Manage.Location.Y, Manage.Location.X);

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
        /// Click on the text and fill color pictures
        /// </summary>
        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                if (selectedIcon == pStickers)
                {
                    contextMenuStrip1.Items["BI_newsticker"].Visible = true;
                    contextMenuStrip1.Items["BI_mystickers"].Visible = true;
                    contextMenuStrip1.Items["BI_templates"].Visible = true;
                }

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

        private void p1_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_B);
        }

        private void pItalic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_I);
        }


        private void pStickers_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                contextMenuStrip1.Items["BI_newsticker"].Visible = true;
                contextMenuStrip1.Items["BI_mystickers"].Visible = true;
                contextMenuStrip1.Items["BI_templates"].Visible = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
        private void pSticker_Click(object sender, EventArgs e)
        {
            Point location;
            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;

            if (orientation == "H")
            {
                location = new Point(this.Location.X, this.Location.Y + this.Height);

                if (location.Y + StickerDummy.DummyStickerHeight > area.Bottom)
                    location = new Point(this.Location.X, this.Location.Y - StickerDummy.DummyStickerHeight);
            }
            else
            {
                location = new Point(this.Location.X + this.Width, this.Location.Y);

                if (location.X + StickerDummy.DummyStickerWidth > area.Right)
                    location = new Point(this.Location.X - StickerDummy.DummyStickerWidth, this.Location.Y);
            }

            StickerDummy form = new StickerDummy(
                new StickerItem(0, "Your Text...", "#515151", "#B9B9F9", "Verdana", 9, 0, "0", "", "center", "sticker"), location);
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        int stickerN = 1; 
        List<Form> Stickers = new List<Form>();

        private void Close_Click(object sender, EventArgs e)
        {
            int stick = (int)(sender as Button).Tag;
            foreach (Form form in Stickers)
            {
                if ((int)form.Tag == stick)
                    form.Close();
            }
        }

        private void pFontIncrease_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.OEM_PERIOD);
        }

        private void pFontDecrease_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.OEM_COMMA);
        }

        private void pClearFormat_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.SPACE);
        }

        bool ActivateMindManager()
        {
            Process p = Process.GetProcessesByName("MindManager").FirstOrDefault();
            if (p == null)
                return false;
            else
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                return true;
            }
        }

        void AddMyStickers()
        {
            ToolStripMenuItem MyStickers = contextMenuStrip1.Items["BI_mystickers"] as ToolStripMenuItem;
            ToolStripMenuItem Templates = contextMenuStrip1.Items["BI_templates"] as ToolStripMenuItem;
            ToolStripItem item;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKERS");
                foreach (DataRow row in dt.Rows) 
                {
                    if (row["template"].ToString() == "1")
                    {
                        item = Templates.DropDown.Items.Add(row["name"].ToString());
                        item.Name = "template";
                    }
                    else
                    {
                        item = MyStickers.DropDown.Items.Add(row["name"].ToString());
                        item.Name = "mysticker";
                    }
                }
            }
            MyStickers.DropDown.ItemClicked += ContextMenuStrip1_ItemClicked;
            Templates.DropDown.ItemClicked += ContextMenuStrip1_ItemClicked;
        }

        PictureBox selectedIcon = null;
        string orientation = "H";

        int MinLength, MaxLength, Thickness;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        InputSimulator sim = new InputSimulator();
    }
}