using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using PopupControl;
using Cursor = System.Windows.Forms.Cursor;

namespace Bubbles
{
    internal partial class BubblePriPro : Form
    {
        public BubblePriPro(int ID, string _orientation, string stickname = "")
        {
            InitializeComponent();

            this.Tag = ID; // correct
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "PriProStick.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, stickname);

            MinLength = this.Width; RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menus
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            cmsIcon.ItemClicked += ContextMenuStrip1_ItemClicked;

            contextMenuStrip1.Items["BI_addpriority"].Text = Utils.getString("pripro.contextmenu.priority");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_addpriority"], "pr1.png");

            contextMenuStrip1.Items["BI_addprogress"].Text = Utils.getString("pripro.contextmenu.progress");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_addprogress"], "pro25.png");

            cmsIcon.Items["BI_delete"].Text = Utils.getString("float_icons.contextmenu.delete");

            StickUtils.SetCommonContextMenu(contextMenuStrip1);

            string priority = Utils.getString("pripro.priority.caption");
            ToolStripMenuItem addPriority = contextMenuStrip1.Items["BI_addpriority"] as ToolStripMenuItem;
            addPriority.DropDown.Items[0].Text = priority + " 1";
            addPriority.DropDown.Items[1].Text = priority + " 2";
            addPriority.DropDown.Items[2].Text = priority + " 3";
            addPriority.DropDown.Items[3].Text = priority + " 4";
            addPriority.DropDown.Items[4].Text = priority + " 5";

            addPriority.DropDown.ItemClicked += ContextMenuStrip1_ItemClicked;
            PriorityCollection = addPriority.DropDown.Items;

            ToolStripMenuItem addProgress = contextMenuStrip1.Items["BI_addprogress"] as ToolStripMenuItem;
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

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from PRIPRO where stickID=" + ID + "");
                
                List<int> pri = new List<int>();
                List<int> pro = new List<int>();

                foreach (DataRow row in dt.Rows)
                {
                    if (row["type"].ToString() == "pri")
                        pri.Add(Convert.ToInt32(row["value"]));
                    else if (row["type"].ToString() == "pro")
                        pro.Add(Convert.ToInt32(row["value"]));
                }

                pri.Sort(); pro.Sort();

                foreach (var value in pri)
                {
                    PriProItem item = new PriProItem("pri", value);
                    PriPros.Add(item);
                }
                foreach (var value in pro)
                {
                    PriProItem item = new PriProItem("pro", value);
                    PriPros.Add(item);
                }
            }

            RefreshStick();

            if (collapsed) {
                collapsed = false; Collapse(); }

            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();
            pictureHandle.MouseDown += Move_Stick;
            this.MouseDown += Move_Stick;
            Manage.Click += Manage_Click;
            this.MouseEnter += this_MouseEnter;
            this.MouseLeave += this_MouseLeave;
        }

        private void this_MouseLeave(object sender, EventArgs e)
        {
            StickUtils.HideCommandPopup(this, orientation);
        }

        private void this_MouseEnter(object sender, EventArgs e)
        {
            StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepripro);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

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
            if (e.ClickedItem.Name.StartsWith("pr")) // new PriPro
            {
                string type = e.ClickedItem.Name.Substring(0, 3);
                int value = Convert.ToInt32(e.ClickedItem.Name.Substring(3));

                PriPros.Add(new PriProItem(type, value));
                PriPros = PriPros.OrderBy(item => item.Type).ThenBy(item => item.Value).ToList();
                using (BubblesDB db = new BubblesDB())
                    db.AddPriPro(type, value, (int)this.Tag);

                RefreshStick();
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                StickUtils.PriPros.Clear(); StickUtils.PriPros.AddRange(PriPros);
                StickUtils.DeleteIcon(selectedIcon, (int)this.Tag, StickUtils.typepripro);
                PriPros.Clear(); PriPros.AddRange(StickUtils.PriPros);
                RefreshStick();
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                PriPros.Clear();
                RefreshStick(true);
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
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "PriProStick.htm");
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
                    BubblePriPro form = new BubblePriPro(0, orientation + _collapsed, name);
                    StickUtils.CreateStick(form, name, StickUtils.typestick);
                }
            }
            else if (e.ClickedItem.Name == "BI_renamestick")
            {
                string newName = StickUtils.GetName(this, orientation, StickUtils.typepripro, 
                    toolTip1.GetToolTip(pictureHandle), true);
                if (newName != "") toolTip1.SetToolTip(pictureHandle, newName);
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                Collapse();
            }
            else if (e.ClickedItem.Name == "BI_delete_stick") // correct
            {
                if (StickUtils.DeleteStick((int)this.Tag, StickUtils.typepripro))
                    this.Close();
            }
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

                StickUtils.Expand(this, RealLength, orientation, contextMenuStrip1);
                //contextMenuStrip1.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.collapse");
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, contextMenuStrip1);
                collapsed = true;
                //contextMenuStrip1.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.expand");
            }
        }

        void RefreshStick(bool deleteall = false)
        {
            StickUtils.PriPros.Clear(); StickUtils.PriPros.AddRange(PriPros);
            List<PictureBox> pBoxs = StickUtils.RefreshStick(this, p1, orientation, MinLength, 
                collapsed, StickUtils.typepripro, deleteall);

            RealLength = StickUtils.stickLength;

            int i = 0;
            foreach (PictureBox pBox in pBoxs)
            {
                pBox.MouseClick += Icon_Click;

                if (collapsed && i++ > 0) // if collapsed hide all icons except the first
                    pBox.Visible = false;
            }
        }

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null) return;

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
                cmsIcon.Items["BI_delete"].Visible = true;
                cmsIcon.Show(Cursor.Position);
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

        public List<PriProItem> PriPros = new List<PriProItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";
        bool collapsed = false;

        int MinLength, RealLength;

        public static Image PR1, PR2, PR3, PR4, PR5, PRG0, PRG10, PRG25, PRG35, PRG50, PRG65, PRG75, PRG90, PRG100;
        ToolStripItemCollection PriorityCollection;
        ToolStripItemCollection ProgressCollection;

        Popup auxPanel;

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
