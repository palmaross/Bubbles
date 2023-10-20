using PRAManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    internal class StickUtils
    {
        public static string RenameStick(Form form, string orientation, string oldname)
        {
            using (NewStickDlg dlg = new NewStickDlg(form.Bounds, orientation, oldname))
            {
                if (dlg.ShowDialog() == DialogResult.Cancel)
                    return "";

                string stickname = dlg.textBox1.Text.Trim();

                if (oldname != "")
                    using (BubblesDB db = new BubblesDB())
                        db.ExecuteNonQuery("update STICKS set name=`" + stickname + "` where id=" + (int)form.Tag + "");

                return stickname;
            }
        }

        public static void CreateStick(Form newForm, string stickname)
        {
            int id = 0;
            using (BubblesDB db = new BubblesDB())
            {
                db.AddStick(stickname, StickUtils.typeicons, 0, "");

                DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                if (dt.Rows.Count > 0)
                    id = Convert.ToInt32(dt.Rows[0][0]);
            }

            if (id != 0)
            {
                newForm.Location = BubblesButton.m_bubblesMenu.GetStickLocation("");
                newForm.Tag = id;
                BubblesButton.STICKS.Add(id, newForm);
                newForm.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public static string RotateStick(Form form, PictureBox p1, Panel panel1, 
            PictureBox Manage, string orientation, bool start = false)
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

            int thisWidth = form.Width;
            int thisHeight = form.Height;

            Size panel1Size = new Size(panel1.Height, panel1.Width);
            Point panel1Location = new Point(panel1.Location.Y, panel1.Location.X);
            Point ManageLocation = new Point(Manage.Location.Y, Manage.Location.X);

            form.Size = new Size(thisHeight, thisWidth);

            panel1.Size = panel1Size;
            panel1.Location = panel1Location;
            Manage.Location = ManageLocation;

            if (start)
                p1.Location = new Point(p1.Location.Y, p1.Location.X);
            else
                foreach (PictureBox p in panel1.Controls.OfType<PictureBox>())
                    p.Location = new Point(p.Location.Y, p.Location.X);

            return orientation;
        }

        public static void SaveStick(Rectangle rec, int id, string orientation)
        {
            string position = "";

            if (!Utils.IsOnMMWindow(rec))
            {
                if (MessageBox.Show(Utils.getString("sticks.stickisoutMMwindow.text"),
                    Utils.getString("sticks.stickisoutMMwindow.title"),
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }

            Point screenXY = Utils.MMScreen(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);

            using (BubblesDB db = new BubblesDB())
            {
                string location = "";
                DataTable dt = db.ExecuteQuery("select * from STICKS where id=" + id + "");

                if (dt.Rows.Count > 0)
                    position = dt.Rows[0]["position"].ToString();

                if (position != "")
                {
                    string[] parts = position.Split('#');
                    location = parts[1];
                }

                bool found = false;
                if (!String.IsNullOrEmpty(location)) // Search foe screen
                {
                    string[] xy = location.Split(';');
                    foreach (string part in xy)
                    {
                        if (part.StartsWith(screenXY.X + "," + screenXY.Y))
                        {
                            location = location.Replace(part, screenXY.X + "," + screenXY.Y +
                                ":" + rec.X + "," + rec.Y);
                            found = true;
                            break;
                        }
                    }
                }

                if (!found) // screen not found, so new screen
                    location += ";" + screenXY.X + "," + screenXY.Y + ":" + rec.X + "," + rec.Y;

                position = orientation + "#" + location.TrimStart(';');
                db.ExecuteNonQuery("update STICKS set position=`" + position + "` where id=" + id + "");
            }
        }

        public static List<PictureBox> RefreshStick(Form form, PictureBox p1, Panel panel1,
            string orientation, int Thickness, int MinLength, int panel1MinLength, 
            int icondist, ref int RealLength, string sticktype, bool deleteall = false)
        {
            List <PictureBox> lpb = new List<PictureBox>();

            // Remove all icons
            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>().Reverse())
            {
                if (p.Name != "p1")
                    p.Dispose();
            }

            // Reset bubble size to minimum
            if (orientation == "H")
            {
                form.Size = new Size(MinLength, Thickness);
                panel1.Width = panel1MinLength;
            }
            else
            {
                form.Size = new Size(Thickness, MinLength);
                panel1.Width = panel1MinLength;
            }

            using (BubblesDB db = new BubblesDB())
            {
                if (deleteall) // Clean bubble and database
                {
                    if (sticktype == typeicons)
                    {
                        Icons.Clear();
                        db.ExecuteNonQuery("delete from ICONS where stickID =" + (int)form.Tag + "");
                    }
                    else if (sticktype == typepripro)
                    {
                        PriPros.Clear();
                        db.ExecuteNonQuery("delete from PRIPRO where stickID =" + (int)form.Tag + "");
                    }
                }
                else // Add icons to stick
                {
                    int k = 0;
                    if (sticktype == typeicons)
                    {
                        foreach (var item in Icons)
                        {
                            lpb.Add(AddIcon(form, p1, item, item.Path, panel1, orientation, icondist, k++));
                            db.ExecuteNonQuery("update ICONS set _order=" + item.Order + " where stickID=" + (int)form.Tag + " and filename =`" + item.FileName + "`");
                        }
                    }
                    else if (sticktype == typepripro)
                    {
                        foreach (var item in PriPros)
                            lpb.Add(AddPriPro(form, p1, item, panel1, orientation, icondist, k++));
                    }
                    else if (sticktype == typesources)
                    {
                        foreach (var item in Sources)
                        {
                            lpb.Add(AddSource(form, p1, item, item.Path, panel1, orientation, icondist, k++));
                            db.ExecuteNonQuery("update SOURCES set _order=" + item.Order + " where stickID=" + (int)form.Tag + " and path =`" + item.Path + "`");
                        }
                    }
                }
            }

            if (orientation == "H")
                RealLength = form.Width;
            else
                RealLength = form.Height;

            return lpb;
        }

        public static PictureBox AddIcon(Form form, PictureBox p1, IconItem item, string path, 
            Panel panel1, string orientation, int icondist, int k)
        {
            PictureBox pBox = AddPitureBox(form, p1, panel1, orientation, icondist, path, k, typeicons);
            new ToolTip().SetToolTip(pBox, item.IconName);
            pBox.Tag = item;
            return pBox;
        }

        public static PictureBox AddPriPro(Form form, PictureBox p1, PriProItem item, Panel panel1,
            string orientation, int icondist, int k)
        {
            PictureBox pBox = AddPitureBox(form, p1, panel1, orientation, icondist, "", k, typepripro,
                item.Type, item.Value);
            if (item.Type == "pro")
                new ToolTip().SetToolTip(pBox, item.Type);
            pBox.Tag = item;
            return pBox;
        }

        public static PictureBox AddSource(Form form, PictureBox p1, MySourcesItem item, string path, 
            Panel panel1, string orientation, int icondist, int k)
        {
            PictureBox pBox = AddPitureBox(form, p1, panel1, orientation, icondist, path, k, typesources, item.Type);
            new ToolTip().SetToolTip(pBox, item.Title);
            pBox.Tag = item;
            return pBox;
        }

        static PictureBox AddPitureBox(Form form, PictureBox p1, Panel panel1, string orientation, 
            int icondist, string path, int k, string stickType, string imageType = "", int prirproValue = 0)
        {
            PictureBox pBox = new PictureBox();
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.AllowDrop = true;
            if (stickType == typeicons)
                pBox.Image = Image.FromFile(path);
            else if (stickType == typepripro)
                pBox.Image = GetImage(imageType, prirproValue);
            else if (stickType == typesources)
            {
                if (imageType == "exe")
                {
                    try
                    {
                        Icon appIcon = Icon.ExtractAssociatedIcon(path);
                        pBox.Image = appIcon.ToBitmap();
                    }
                    catch { pBox.Image = GetImage(imageType); }
                }
                else
                    pBox.Image = GetImage(imageType);
            }
            panel1.Controls.Add(pBox);
            pBox.Visible = true;
            pBox.BringToFront();

            if (orientation == "H")
            {
                pBox.Location = new Point(icondist * k++, p1.Location.Y);
                if (k > 4)
                    form.Width += icondist;
            }
            else
            {
                pBox.Location = new Point(p1.Location.X, icondist * k++);
                if (k > 4)
                    form.Height += icondist;
            }
            return pBox;
        }

        public static void DeleteIcon(PictureBox selectedIcon, int id, string type)
        {
            if (type == typeicons)
            {
                IconItem _item = (IconItem)selectedIcon.Tag;
                string filename = _item.FileName;

                if (_item == null) return;

                _item = Icons.Find(x => x.FileName == filename);
                Icons.Remove(_item);

                for (int i = 0; i < Icons.Count; i++)
                    Icons[i].Order = i + 1;

                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("delete from ICONS where stickID=" + id + " and filename=`" + filename + "`");
            }
            else if (type == typepripro)
            {
                PriProItem _item = (PriProItem)selectedIcon.Tag;

                PriProItem __item = PriPros.Find(x => x.Type == _item.Type && x.Value == _item.Value);
                if (__item != null)
                    PriPros.Remove(__item);

                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("delete from PRIPRO where stickID=" + id + 
                        " and type=`" + _item.Type + "` and value=" + _item.Value + "");
            }
            else if (type == typesources)
            {
                MySourcesItem _item = (MySourcesItem)selectedIcon.Tag;
                string filename = _item.Path;

                if (_item == null) return;

                _item = Sources.Find(x => x.Path == filename);
                Sources.Remove(_item);

                for (int i = 0; i < Sources.Count; i++)
                    Sources[i].Order = i + 1;

                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("delete from SOURCES where path=`" + filename + "`");
            }
        }

        public static bool DeleteStick(int id, string type)
        {
            if (MessageBox.Show(Utils.getString("sticks.deletestick.warning"), 
                Utils.getString("float_icons.contextmenu.deletestick"), 
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return false;

            using (BubblesDB db = new BubblesDB())
            {
                if (type == typeicons)
                    db.ExecuteNonQuery("delete from ICONS where stickID=" + id + "");
                else if (type == typepripro)
                    db.ExecuteNonQuery("delete from PRIPRO where stickID=" + id + "");

                db.ExecuteNonQuery("delete from STICKS where id=" + id + "");
                BubblesButton.STICKS.Remove(id);
            }
            return true;
        }

        public static Image GetImage(string type, int value)
        {
            if (type == "pri")
            {
                switch (value)
                {
                    case 1: return BubblePriPro.PR1;
                    case 2: return BubblePriPro.PR2;
                    case 3: return BubblePriPro.PR3;
                    case 4: return BubblePriPro.PR4;
                    case 5: return BubblePriPro.PR5;
                }
            }
            else
            {
                switch (value)
                {
                    case 0: return BubblePriPro.PRG0;
                    case 10: return BubblePriPro.PRG10;
                    case 25: return BubblePriPro.PRG25;
                    case 35: return BubblePriPro.PRG35;
                    case 50: return BubblePriPro.PRG50;
                    case 65: return BubblePriPro.PRG65;
                    case 75: return BubblePriPro.PRG75;
                    case 90: return BubblePriPro.PRG90;
                    case 100: return BubblePriPro.PRG100;
                }
            }
            return null;
        }

        public static Image GetImage(string type)
        {
            switch (type)
            {
                case "audio": return BubbleMySources.audio;
                case "excel": return BubbleMySources.excel;
                case "exe": return BubbleMySources.exe;
                case "image": return BubbleMySources.image;
                case "macros": return BubbleMySources.macros;
                case "map": return BubbleMySources.map;
                case "pdf": return BubbleMySources.pdf;
                case "txt": return BubbleMySources.txt;
                case "video": return BubbleMySources.video;
                case "http": return BubbleMySources.http;
                case "word": return BubbleMySources.word;
                case "youtube": return BubbleMySources.youtube;
                case "chm": return BubbleMySources.chm;
            }
            return BubbleMySources.file;
        }

        public static void SetCommonContextMenu(ContextMenuStrip cms, PictureBox p2)
        {
            cms.Items.Add(new ToolStripSeparator());

            ToolStripItem tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.collapse"));
            tsi.Name = "BI_collapse";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "collapse.png"), p2.Size);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.expand"));
            tsi.Name = "BI_expand";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "expand.png"), p2.Size);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.rotate"));
            tsi.Name = "BI_rotate";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "rotate.png"), p2.Size);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.settings"));
            tsi.Name = "BI_store";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "remember.png"), p2.Size);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.deletestick"));
            tsi.Name = "BI_delete_stick";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "deleteStick.png"), p2.Size);

            cms.Items.Add(new ToolStripSeparator());

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.newstick"));
            tsi.Name = "BI_newstick";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "newStick.png"), p2.Size);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.renamestick"));
            tsi.Name = "BI_renamestick";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "edit.png"), p2.Size);

            cms.Items.Add(new ToolStripSeparator());

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.help"));
            tsi.Name = "BI_help";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "helpSticker.png"), p2.Size);

            tsi = cms.Items.Add(Utils.getString("float_icons.contextmenu.close"));
            tsi.Name = "BI_close";
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + "close_sticker.png"), p2.Size);
        }

        public static void SetContextMenuImage(ToolStripItem tsi, PictureBox p2, string imgName)
        {
            tsi.ImageScaling = ToolStripItemImageScaling.None;
            tsi.Image = new Bitmap(Image.FromFile(Utils.ImagesPath + imgName), p2.Size);
        }

        public static List<IconItem> Icons = new List<IconItem>();
        public static List<PriProItem> PriPros = new List<PriProItem>();
        public static List<BookmarkItem> Bookmarks = new List<BookmarkItem>();
        public static List<MySourcesItem> Sources = new List<MySourcesItem>();

        public const string typeicons = "icons", typepripro = "pripro", typesources = "mysources", 
            typebookmarks = "bookmarks";
    }

    class ResizeStick : Form
    {
        public ResizeStick(Form form) { aForm = form; }
        public Form aForm;

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
                        Point clientPoint = aForm.PointToClient(screenPoint);
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
    }
}
