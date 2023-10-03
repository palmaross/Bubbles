using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Bubbles
{
    internal partial class BubblePriPro : Form
    {
        public BubblePriPro()
        {
            InitializeComponent();

            //helpProvider1.HelpNamespace = MMMapNavigator.dllPath + "MapNavigator.chm";
            //helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            //helpProvider1.SetHelpKeyword(this, "bookmarks_express.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, Utils.getString("pripro.bubble.tooltip"));

            string location = Utils.getRegistry("PositionPriPro", "");
            orientation = Utils.getRegistry("OrientationPriPro", "H");

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

            if (String.IsNullOrEmpty(location))
                Location = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            else
            {
                string[] xy = location.Split(',');
                Location = new Point(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));

                if (!Utils.IsOnScreen(Location, this.Size))
                    Location = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            contextMenuStrip1.Items["BI_addpriority"].Text = MMUtils.getString("pripro.contextmenu.priority");
            contextMenuStrip1.Items["BI_addprogress"].Text = MMUtils.getString("pripro.contextmenu.progress");
            contextMenuStrip1.Items["BI_delete"].Text = MMUtils.getString("float_icons.contextmenu.delete");
            contextMenuStrip1.Items["BI_deleteall"].Text = MMUtils.getString("float_icons.contextmenu.deleteall");

            contextMenuStrip1.Items["BI_rotate"].Text = MMUtils.getString("float_icons.contextmenu.rotate");
            contextMenuStrip1.Items["BI_close"].Text = MMUtils.getString("float_icons.contextmenu.close");
            contextMenuStrip1.Items["BI_help"].Text = MMUtils.getString("float_icons.contextmenu.help");
            contextMenuStrip1.Items["BI_store"].Text = MMUtils.getString("float_icons.contextmenu.settings");

            ToolStripMenuItem addPriority = contextMenuStrip1.Items["BI_addpriority"] as ToolStripMenuItem;
            addPriority.Text = MMUtils.getString("pripro.contextmenu.priority");
            addPriority.DropDown.Items[0].Text = MMUtils.getString("pripro.priority.caption") + " 1";
            addPriority.DropDown.Items[1].Text = MMUtils.getString("pripro.priority.caption") + " 2";
            addPriority.DropDown.Items[2].Text = MMUtils.getString("pripro.priority.caption") + " 3";
            addPriority.DropDown.Items[3].Text = MMUtils.getString("pripro.priority.caption") + " 4";
            addPriority.DropDown.Items[4].Text = MMUtils.getString("pripro.priority.caption") + " 5";

            addPriority.DropDown.ItemClicked += ContextMenuStrip1_ItemClicked;
            PriorityCollection = addPriority.DropDown.Items;

            ToolStripMenuItem addProgress = contextMenuStrip1.Items["BI_addprogress"] as ToolStripMenuItem;
            addProgress.Text = MMUtils.getString("pripro.contextmenu.progress");
            addProgress.DropDown.ItemClicked += ContextMenuStrip1_ItemClicked;
            ProgressCollection = addProgress.DropDown.Items;

            PR1 = Image.FromFile(Utils.ImagesPath + "pr1.png");
            addPriority.DropDownItems["pri1"].Image = new Bitmap(PR1, new Size(p1.Width, p1.Height));
            PR2 = Image.FromFile(Utils.ImagesPath + "pr2.png");
            addPriority.DropDownItems["pri2"].Image = new Bitmap(PR2, new Size(p1.Width, p1.Height));
            PR3 = Image.FromFile(Utils.ImagesPath + "pr3.png");
            addPriority.DropDownItems["pri3"].Image = new Bitmap(PR3, new Size(p1.Width, p1.Height));
            PR4 = Image.FromFile(Utils.ImagesPath + "pr4.png");
            addPriority.DropDownItems["pri4"].Image = new Bitmap(PR4, new Size(p1.Width, p1.Height));
            PR5 = Image.FromFile(Utils.ImagesPath + "pr5.png");
            addPriority.DropDownItems["pri5"].Image = new Bitmap(PR5, new Size(p1.Width, p1.Height));

            PRG0 = Image.FromFile(Utils.ImagesPath + "pro0.png");
            addProgress.DropDownItems["pro0"].Image = new Bitmap(PRG0, new Size(p1.Width, p1.Height));
            PRG10 = Image.FromFile(Utils.ImagesPath + "pro10.png");
            addProgress.DropDownItems["pro10"].Image = new Bitmap(PRG10, new Size(p1.Width, p1.Height));
            PRG25 = Image.FromFile(Utils.ImagesPath + "pro25.png");
            addProgress.DropDownItems["pro25"].Image = new Bitmap(PRG25, new Size(p1.Width, p1.Height));
            PRG35 = Image.FromFile(Utils.ImagesPath + "pro35.png");
            addProgress.DropDownItems["pro35"].Image = new Bitmap(PRG35, new Size(p1.Width, p1.Height));
            PRG50 = Image.FromFile(Utils.ImagesPath + "pro50.png");
            addProgress.DropDownItems["pro50"].Image = new Bitmap(PRG50, new Size(p1.Width, p1.Height));
            PRG65 = Image.FromFile(Utils.ImagesPath + "pro65.png");
            addProgress.DropDownItems["pro65"].Image = new Bitmap(PRG65, new Size(p1.Width, p1.Height));
            PRG75 = Image.FromFile(Utils.ImagesPath + "pro75.png");
            addProgress.DropDownItems["pro75"].Image = new Bitmap(PRG75, new Size(p1.Width, p1.Height));
            PRG90 = Image.FromFile(Utils.ImagesPath + "pro90.png");
            addProgress.DropDownItems["pro90"].Image = new Bitmap(PRG90, new Size(p1.Width, p1.Height));
            PRG100 = Image.FromFile(Utils.ImagesPath + "pro100.png");
            addProgress.DropDownItems["pro100"].Image = new Bitmap(PRG100, new Size(p1.Width, p1.Height));

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            Manage.Click += Manage_Click;

            string priority = Utils.getRegistry("Priority", "new");
            string progress = Utils.getRegistry("Progress", "new");

            if (priority == "new" && progress == "new")
            {
                PriPros.Add(new PriProItem("pri", 1));
                PriPros.Add(new PriProItem("pri", 2));
                PriPros.Add(new PriProItem("pro", 0));
                PriPros.Add(new PriProItem("pro", 100));
            }
            else 
            {
                if (priority != "")
                {
                    string[] _priority = priority.Split(',');
                    foreach (string pri in _priority)
                        PriPros.Add(new PriProItem("pri", Convert.ToInt32(pri)));
                }
                if (progress != "")
                {
                    string[] _progress = progress.Split(',');
                    foreach (string pro in _progress)
                        PriPros.Add(new PriProItem("pro", Convert.ToInt32(pro)));
                }
            }

            foreach (var item in PriPros)
                AddIcon(item.Type, item.Value);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Items["BI_delete"].Visible = false;

            PriorityCollection["pri1"].Visible = !PriPros.Any(x => x.Value == 1);
            PriorityCollection["pri2"].Visible = !PriPros.Any(x => x.Value == 2);
            PriorityCollection["pri3"].Visible = !PriPros.Any(x => x.Value == 3);
            PriorityCollection["pri4"].Visible = !PriPros.Any(x => x.Value == 4);
            PriorityCollection["pri5"].Visible = !PriPros.Any(x => x.Value == 5);

            ProgressCollection["pro0"].Visible = !PriPros.Any(x => x.Value == 0);
            ProgressCollection["pro10"].Visible = !PriPros.Any(x => x.Value == 10);
            ProgressCollection["pro25"].Visible = !PriPros.Any(x => x.Value == 25);
            ProgressCollection["pro35"].Visible = !PriPros.Any(x => x.Value == 35);
            ProgressCollection["pro50"].Visible = !PriPros.Any(x => x.Value == 50);
            ProgressCollection["pro65"].Visible = !PriPros.Any(x => x.Value == 65);
            ProgressCollection["pro75"].Visible = !PriPros.Any(x => x.Value == 75);
            ProgressCollection["pro90"].Visible = !PriPros.Any(x => x.Value == 90);
            ProgressCollection["pro100"].Visible = !PriPros.Any(x => x.Value == 100);

            contextMenuStrip1.Show(Cursor.Position);
        }

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.StartsWith("pr"))
            {
                string type = e.ClickedItem.Name.Substring(0, 3);
                int value = Convert.ToInt32(e.ClickedItem.Name.Substring(3));

                PriPros.Add(new PriProItem(type, value));

                PriPros = PriPros.OrderBy(item => item.Type).ThenBy(item => item.Value).ToList();

                RefreshBubble();
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                PriProItem _item = (PriProItem)selectedIcon.Tag;

                if (_item != null)
                {
                    PriProItem __item = PriPros.Find(x => x.Type == _item.Type && x.Value == _item.Value);
                    if (__item != null)
                        PriPros.Remove(__item);

                    RefreshBubble();
                }
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                RefreshBubble(true);
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                this.Hide();
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

                Utils.setRegistry("OrientationPriPro", orientation);
                Utils.setRegistry("PositionPriPro", x.ToString() + "," + y.ToString());
            }
        }

        void RefreshBubble(bool deleteall = false)
        {
            // Remove all icons
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

            if (deleteall) // Clean bubble and database
            {
                PriPros.Clear();
                Utils.setRegistry("Priority", "");
                Utils.setRegistry("Progress", "");
            }
            else
            {
                // Add icons to bubble
                k = 0;
                string priority = "", progress = "";
                foreach (var item in PriPros)
                {
                    AddIcon(item.Type, item.Value);
                    if (item.Type == "pri")
                        priority += item.Value.ToString() + ",";
                    else
                        progress += item.Value.ToString() + ",";
                }

                // Store bubble in registry
                Utils.setRegistry("Priority", priority.TrimEnd(','));
                Utils.setRegistry("Progress", progress.TrimEnd(','));
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

        void AddIcon(string type, int value)
        {
            PictureBox pBox = new PictureBox();
            try { pBox.Image = GetImage(type, value); } catch { return; }
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.MouseClick += Icon_Click;
            panel1.Controls.Add(pBox);
            pBox.Visible = true;
            pBox.BringToFront();
            pBox.Tag = new PriProItem(type, value);

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

        Image GetImage(string type, int value)
        {
            if (type == "pri")
            {
                switch (value) 
                {
                    case 1: return PR1;
                    case 2: return PR2;
                    case 3: return PR3;
                    case 4: return PR4;
                    case 5: return PR5;
                }
            }
            else
            {
                switch (value)
                {
                    case 0: return PRG0;
                    case 10: return PRG10;
                    case 25: return PRG25;
                    case 35: return PRG35;
                    case 50: return PRG50;
                    case 65: return PRG65;
                    case 75: return PRG75;
                    case 90: return PRG90;
                    case 100: return PRG100;
                }
            }
            return null;
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

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                PriProItem item = selectedIcon.Tag as PriProItem;
                if (item.Type == "pri") // Set Priority icon
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        MmTaskPriority pr = GetPriority(item.Value);
                        if (pr > 0)
                            t.Task.Priority = pr;
                    }
                }
                else // Set Progress icon
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        if (item.Value >= 0)
                            t.Task.Complete = item.Value;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                contextMenuStrip1.Items["BI_delete"].Visible = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private MmTaskPriority GetPriority(int value)
        {
            switch (value)
            {
                case 1: return MmTaskPriority.mmTaskPriority1;
                case 2: return MmTaskPriority.mmTaskPriority2;
                case 3: return MmTaskPriority.mmTaskPriority3;
                case 4: return MmTaskPriority.mmTaskPriority4;
                case 5: return MmTaskPriority.mmTaskPriority5;
            }
            return 0;
        }

        public static List<PriProItem> PriPros = new List<PriProItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";

        int MinLength, MaxLength, Thickness, panel1MinLength;

        Image PR1, PR2, PR3, PR4, PR5, PRG0, PRG10, PRG25, PRG35, PRG50, PRG65, PRG75, PRG90, PRG100;
        ToolStripItemCollection PriorityCollection;
        ToolStripItemCollection ProgressCollection;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    internal class PriProItem
    {
        public PriProItem(string type, int value)
        {
            Type = type;
            Value = value;
        }
        public string Type = "";
        public int Value = 0;
    }
}
