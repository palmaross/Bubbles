﻿using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class BubblesIcons : Form
    {
        public BubblesIcons()
        {
            InitializeComponent();

            //helpProvider1.HelpNamespace = MMMapNavigator.dllPath + "MapNavigator.chm";
            //helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            //helpProvider1.SetHelpKeyword(this, "bookmarks_express.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));

            string location = Utils.getRegistry("PositionIcons", "");
            orientation = Utils.getRegistry("OrientationIcons", "H");

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

            if (String.IsNullOrEmpty(location))
                Location = new Point(MMUtils.MindManager.Left + MMUtils.MindManager.Width - this.Width - label1.Width * 2, MMUtils.MindManager.Top + label1.Width);
            else
            {
                string[] xy = location.Split(',');
                Location = new Point(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));
            }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            contextMenuStrip1.Items["BI_new"].Text = MMUtils.getString("float_icons.contextmenu.new");
            contextMenuStrip1.Items["BI_delete"].Text = MMUtils.getString("float_icons.contextmenu.delete");
            contextMenuStrip1.Items["BI_rename"].Text = MMUtils.getString("float_icons.contextmenu.edit");

            contextMenuStrip1.Items["BI_rotate"].Text = MMUtils.getString("float_icons.contextmenu.rotate");
            contextMenuStrip1.Items["BI_close"].Text = MMUtils.getString("float_icons.contextmenu.close");
            contextMenuStrip1.Items["BI_help"].Text = MMUtils.getString("float_icons.contextmenu.help");
            contextMenuStrip1.Items["BI_store"].Text = MMUtils.getString("float_icons.contextmenu.settings");

            panel1.MouseClick += Panel1_MouseClick;
            panel1.MouseDown += Panel1_MouseDown;
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            txtName.KeyUp += TxtName_KeyUp;
            this.Deactivate += BubblesIcons_Deactivate;
            Manage.Click += Manage_Click;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from ICONS order by _order");

                foreach (DataRow row in dt.Rows)
                {
                    string name = row["name"].ToString();
                    string filename = row["filename"].ToString();
                    string _filename = filename;
                    string rootPath = Utils.m_dataPath + "IconDB\\";

                    if (filename.StartsWith("stock"))
                    {
                        rootPath = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
                        _filename = filename.Substring(5) + ".ico"; // stockemail -> email.ico
                    }

                    if (File.Exists(rootPath + _filename))
                        AddIcon(name, rootPath + _filename, filename);
                }
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Items["BI_delete"].Visible = false;
            contextMenuStrip1.Items["BI_rename"].Visible = false;

            contextMenuStrip1.Show(Cursor.Position);
        }

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            txtName.Visible = false;
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void BubblesIcons_Deactivate(object sender, EventArgs e)
        {
            txtName.Visible = false;
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            txtName.Visible = false;
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void TxtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                IconItem item = (IconItem)selectedIcon.Tag;
                if (item == null)
                    return;
                ((IconItem)selectedIcon.Tag).IconName = txtName.Text.Trim();
                Icons.Find(p => p.FileName == item.FileName).IconName = txtName.Text.Trim();
                txtName.Visible = false;
                toolTip1.SetToolTip(selectedIcon, txtName.Text.Trim());

                // change Name in the data base
                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("update ICONS set name=`" + txtName.Text.Trim() + "` where filename=`" + item.FileName + "`");
            }
            else if (e.KeyCode == Keys.Escape)
                txtName.Visible = false;
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string iconPath, iconName;
            if (e.ClickedItem.Name == "BI_new")
            {
                using (SelectIconDlg _dlg = new SelectIconDlg())
                {
                    if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;
                    iconPath = _dlg.iconPath;
                    iconName = _dlg.iconName;
                }

                string fileName = "stock" + Path.GetFileNameWithoutExtension(iconPath);

                if (StockIconFromString(fileName) == 0) // кастомная иконка, сохраним файл в нашей базе
                {
                    fileName = Path.GetFileName(iconPath);
                    if (!File.Exists(Utils.m_dataPath + "IconDB\\" + fileName))
                        File.Copy(iconPath, Utils.m_dataPath + "IconDB\\" + fileName);
                }

                // Add icon to bubble and to Icons list
                AddIcon(iconName, iconPath, fileName);

                // Add icon to db
                using (BubblesDB db = new BubblesDB())
                    db.AddIcon(iconName, fileName, Icons.Count);
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                IconItem _item = (IconItem)selectedIcon.Tag;
                int k = _item.Order;
                string filename = _item.FileName;

                if (_item != null) 
                {
                    Icons.Remove(_item);
                    selectedIcon.Dispose();

                    // Delete file from IconDB if exists
                    if (!filename.StartsWith("stock"))
                    {
                        string path = Utils.m_dataPath + "IconDB\\" + filename;
                        if (File.Exists(path))
                        {
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Delete(path);
                        }
                    }

                    int i = 0;
                    using (BubblesDB db = new BubblesDB())
                    {
                        // delete icon from db
                        db.ExecuteNonQuery("delete from ICONS where filename=`" + filename + "`");

                        foreach (var item in Icons)
                        {
                            item.Order = ++i;
                            if (i >= k)
                            {
                                PictureBox p = FindByTag(item);
                                if (p != null)
                                {
                                    if (orientation == "H")
                                        p.Location = new Point(p.Location.X - icondist.Width, p.Location.Y);
                                    else
                                        p.Location = new Point(p.Location.X, p.Location.Y - icondist.Width);
                                    ((IconItem)p.Tag).Order = item.Order;
                                }
                                // Update icon orders
                                db.ExecuteNonQuery("update ICONS set _order=" + i + " where filename=`" + item.FileName + "`");
                            }
                        }
                    }

                    if (Icons.Count > 3) 
                    { 
                        if (orientation == "H")
                            this.Width -= icondist.Width;
                        else
                            this.Height -= icondist.Width;
                    }
                }
            }
            else if (e.ClickedItem.Name == "BI_rename")
            {
                txtName.Visible = true;
                txtName.BringToFront();
                txtName.Location = p1.Location;
                txtName.Focus();
                txtName.Text = ((IconItem)selectedIcon.Tag).IconName;
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

                Utils.setRegistry("OrientationIcons", orientation);
                Utils.setRegistry("PositionIcons", x.ToString() + "," + y.ToString());
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

        PictureBox FindByTag(IconItem item)
        {
            foreach (PictureBox p in panel1.Controls.OfType<PictureBox>())
            {
                if (p.Tag != null && (p.Tag as IconItem) == item)
                    return p;
            }
            return null;
        }

        void AddIcon(string name, string path, string fileName)
        {
            int iconOrder = icondist.Width * Icons.Count;
            PictureBox pBox = new PictureBox();
            pBox.Size = p1.Size;
            pBox.SizeMode = PictureBoxSizeMode.Zoom;
            pBox.MouseClick += Icon_Click;
            pBox.Image = System.Drawing.Image.FromFile(path);
            panel1.Controls.Add(pBox);
            pBox.Visible = true;
            toolTip1.SetToolTip(pBox, name);
            pBox.BringToFront();

            pBox.Tag = new IconItem(name, fileName, Icons.Count + 1);
            Icons.Add((IconItem)pBox.Tag);

            if (orientation == "H")
            {
                pBox.Location = new Point(p1.Location.X + iconOrder, p1.Location.Y);
                if (Icons.Count > 4)
                    this.Width += icondist.Width;
            }
            else
            {
                pBox.Location = new Point(p1.Location.X, p1.Location.Y + iconOrder);
                if (Icons.Count > 4)
                    this.Height += icondist.Width;
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

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                IconItem item = selectedIcon.Tag as IconItem;
                string fileName = item.FileName;
                if (fileName.StartsWith("stock"))
                {
                    MmStockIcon icon = StockIconFromString(fileName);
                    if (StockIconFromString(fileName) != 0)
                    {
                        foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            GetIcon(icon, "", item.IconName, "");
                            try { t.AllIcons.AddStockIcon(icon); }
                            catch { }
                        }
                    }
                }
                else
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        string path = Utils.m_dataPath + "IconDB\\" + item.FileName;
                        if (File.Exists(path))
                        {
                            string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(path);
                            GetIcon(0, signature, item.IconName, path);
                            try { t.AllIcons.AddCustomIconFromMap(signature); }
                            catch { }
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                contextMenuStrip1.Items["BI_new"].Visible = true;
                contextMenuStrip1.Items["BI_delete"].Visible = true;
                contextMenuStrip1.Items["BI_rename"].Visible = true;
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
                contextMenuStrip1.Items["BI_rename"].Visible = false;

                contextMenuStrip1.Show(Cursor.Position);
            }
        }
        /// <summary>
		/// Gets stock icon enumaration
		/// </summary>
		/// <param name="aStockIcon">string representation of a stock icon</param>
		/// <returns>MM Stock icon enumerator, or 0 in case of error</returns>
		public static MmStockIcon StockIconFromString(string aStockIcon)
        {
            aStockIcon = aStockIcon.ToLower();
            switch (aStockIcon)
            {
                case "stockunknown":
                    return MmStockIcon.mmStockIconUnknown;
                case "stocksmiley-happy":
                    return MmStockIcon.mmStockIconSmileyHappy;
                case "stocksmiley-neutral":
                    return MmStockIcon.mmStockIconSmileyNeutral;
                case "stocksmiley-sad":
                    return MmStockIcon.mmStockIconSmileySad;
                case "stocksmiley-angry":
                    return MmStockIcon.mmStockIconSmileyAngry;
                case "stocksmiley-screaming":
                    return MmStockIcon.mmStockIconSmileyScreaming;
                case "stockclock":
                    return MmStockIcon.mmStockIconClock;
                case "stocktime_15":
                    return MmStockIcon.mmStockIconClock; // double
                case "stockcalendar":
                    return MmStockIcon.mmStockIconCalendar;
                case "stocksingles_calendar":
                    return MmStockIcon.mmStockIconCalendar; // double
                case "stockletter":
                    return MmStockIcon.mmStockIconLetter;
                case "stockdocuments_7":
                    return MmStockIcon.mmStockIconLetter; // double
                case "stockemail":
                    return MmStockIcon.mmStockIconEmail;
                case "stocksymbol_04":
                    return MmStockIcon.mmStockIconEmail; // double
                case "stockmailbox":
                    return MmStockIcon.mmStockIconMailbox;
                case "stocksingles_mailbox":
                    return MmStockIcon.mmStockIconMailbox; // double
                case "stockmegaphone":
                    return MmStockIcon.mmStockIconMegaphone;
                case "stocksingles_megaphone":
                    return MmStockIcon.mmStockIconMegaphone; // double
                case "stockhouse":
                    return MmStockIcon.mmStockIconHouse;
                case "stocksingles_house":
                    return MmStockIcon.mmStockIconHouse; // double
                case "stockrolodex":
                    return MmStockIcon.mmStockIconRolodex;
                case "stocksingles_id_card":
                    return MmStockIcon.mmStockIconRolodex; // double
                case "stockdollar":
                    return MmStockIcon.mmStockIconDollar;
                case "stocksingles_dollar":
                    return MmStockIcon.mmStockIconDollar; // double
                case "stockeuro":
                    return MmStockIcon.mmStockIconEuro;
                case "stockcurrencycurrency_3":
                    return MmStockIcon.mmStockIconEuro; // double
                case "stockflag-red":
                    return MmStockIcon.mmStockIconFlagRed;
                case "stockflag-blue":
                    return MmStockIcon.mmStockIconFlagBlue;
                case "stockflag-green":
                    return MmStockIcon.mmStockIconFlagGreen;
                case "stockflag-black":
                    return MmStockIcon.mmStockIconFlagBlack;
                case "stockflag-orange":
                    return MmStockIcon.mmStockIconFlagOrange;
                case "stockflag-yellow":
                    return MmStockIcon.mmStockIconFlagYellow;
                case "stockflag-purple":
                    return MmStockIcon.mmStockIconFlagPurple;
                case "stocktraffic-lights-red":
                    return MmStockIcon.mmStockIconTrafficLightsRed;
                case "stocksigns_01":
                    return MmStockIcon.mmStockIconTrafficLightsRed; // double
                case "stockmarker1":
                    return MmStockIcon.mmStockIconMarker1;
                case "stockmarker2":
                    return MmStockIcon.mmStockIconMarker2;
                case "stockmarker3":
                    return MmStockIcon.mmStockIconMarker3;
                case "stockmarker4":
                    return MmStockIcon.mmStockIconMarker4;
                case "stockmarker5":
                    return MmStockIcon.mmStockIconMarker5;
                case "stockmarker6":
                    return MmStockIcon.mmStockIconMarker6;
                case "stockmarker7":
                    return MmStockIcon.mmStockIconMarker7;
                case "stockresource1":
                    return MmStockIcon.mmStockIconResource1;
                case "stockset_user_blue":
                    return MmStockIcon.mmStockIconResource1; // double
                case "stockresource2":
                    return MmStockIcon.mmStockIconResource2;
                case "stockset_user_red":
                    return MmStockIcon.mmStockIconResource2; // double
                case "stockpadlock-locked":
                    return MmStockIcon.mmStockIconPadlockLocked;
                case "stocksingles_lock":
                    return MmStockIcon.mmStockIconPadlockLocked; // double
                case "stockpadlock-unlocked":
                    return MmStockIcon.mmStockIconPadlockUnlocked;
                case "stocksingles_unlocked":
                    return MmStockIcon.mmStockIconPadlockUnlocked; // double
                case "stockarrow-up":
                    return MmStockIcon.mmStockIconArrowUp;
                case "stockarrowsarrow_19":
                    return MmStockIcon.mmStockIconArrowUp; // double
                case "stockarrow-right":
                    return MmStockIcon.mmStockIconArrowRight;
                case "stockarrowsarrow_25":
                    return MmStockIcon.mmStockIconArrowRight; // double
                case "stocktwo-end-arrow":
                    return MmStockIcon.mmStockIconTwoEndArrow;
                case "stockarrowsarrow_02":
                    return MmStockIcon.mmStockIconTwoEndArrow; // double
                case "stockphone":
                    return MmStockIcon.mmStockIconPhone;
                case "stockcellphone":
                    return MmStockIcon.mmStockIconCellphone;
                case "stocksingles_phone":
                    return MmStockIcon.mmStockIconCellphone; // double
                case "stockcamera":
                    return MmStockIcon.mmStockIconCamera;
                case "stocksingles_camera":
                    return MmStockIcon.mmStockIconCamera; // double
                case "stockfax":
                    return MmStockIcon.mmStockIconFax;
                case "stockstop":
                    return MmStockIcon.mmStockIconStop;
                case "stocksigns_11":
                    return MmStockIcon.mmStockIconStop; // double
                case "stockexclamation-mark":
                    return MmStockIcon.mmStockIconExclamationMark;
                case "stocksigns_08":
                    return MmStockIcon.mmStockIconExclamationMark; // double
                case "stockquestion-mark":
                    return MmStockIcon.mmStockIconQuestionMark;
                case "stockfeedback_q":
                    return MmStockIcon.mmStockIconQuestionMark; // double
                case "stockthumbs-up":
                    return MmStockIcon.mmStockIconThumbsUp;
                case "stockfeedback_10":
                    return MmStockIcon.mmStockIconThumbsUp; // double
                case "stockon-hold":
                    return MmStockIcon.mmStockIconOnHold;
                case "stockhourglass":
                    return MmStockIcon.mmStockIconHourglass;
                case "stocktime_03":
                    return MmStockIcon.mmStockIconHourglass; // double
                case "stockemergency":
                    return MmStockIcon.mmStockIconEmergency;
                case "stocksingles_alarm":
                    return MmStockIcon.mmStockIconEmergency; // double
                case "stockno-entry":
                    return MmStockIcon.mmStockIconNoEntry;
                case "stocksigns_09":
                    return MmStockIcon.mmStockIconNoEntry; // double
                case "stockbomb":
                    return MmStockIcon.mmStockIconBomb;
                case "stocksingles_bomb":
                    return MmStockIcon.mmStockIconBomb; // double
                case "stockkey":
                    return MmStockIcon.mmStockIconKey;
                case "stocksingles_key":
                    return MmStockIcon.mmStockIconKey; // double
                case "stockglasses":
                    return MmStockIcon.mmStockIconGlasses;
                case "stocksingles_glasses":
                    return MmStockIcon.mmStockIconGlasses; // double
                case "stockjudge-hammer":
                    return MmStockIcon.mmStockIconJudgeHammer;
                case "stocksingles_gavel":
                    return MmStockIcon.mmStockIconJudgeHammer; // double
                case "stockrocket":
                    return MmStockIcon.mmStockIconRocket;
                case "stocksingles_rocket":
                    return MmStockIcon.mmStockIconRocket; // double
                case "stockscales":
                    return MmStockIcon.mmStockIconScales;
                case "stocksingles_scale":
                    return MmStockIcon.mmStockIconScales; // double
                case "stockredo":
                    return MmStockIcon.mmStockIconRedo;
                case "stockarrowsarrow_07":
                    return MmStockIcon.mmStockIconRedo; // double
                case "stocklightbulb":
                    return MmStockIcon.mmStockIconLightbulb;
                case "stocksingles_light_bulb":
                    return MmStockIcon.mmStockIconLightbulb; // double
                case "stockcoffee-cup":
                    return MmStockIcon.mmStockIconCoffeeCup;
                case "stocksingles_coffee_2":
                    return MmStockIcon.mmStockIconCoffeeCup; // double
                case "stocktwo-feet":
                    return MmStockIcon.mmStockIconTwoFeet;
                case "stocksingles_foot_print":
                    return MmStockIcon.mmStockIconTwoFeet; // double
                case "stockmeeting":
                    return MmStockIcon.mmStockIconMeeting;
                case "stocksingles_handshake":
                    return MmStockIcon.mmStockIconMeeting; // double
                case "stockcheck":
                    return MmStockIcon.mmStockIconCheck;
                case "stocknote":
                    return MmStockIcon.mmStockIconNote;
                case "stocksingles_note":
                    return MmStockIcon.mmStockIconNote; // double
                case "stockthumbs-down":
                    return MmStockIcon.mmStockIconThumbsDown;
                case "stockfeedback_12":
                    return MmStockIcon.mmStockIconThumbsDown; // double
                case "stockarrow-left":
                    return MmStockIcon.mmStockIconArrowLeft;
                case "stockarrowsarrow_24":
                    return MmStockIcon.mmStockIconArrowLeft; // double
                case "stockarrow-down":
                    return MmStockIcon.mmStockIconArrowDown;
                case "stockarrowsarrow_22":
                    return MmStockIcon.mmStockIconArrowDown; // double
                case "stockbook":
                    return MmStockIcon.mmStockIconBook;
                case "stocksingles_book_2":
                    return MmStockIcon.mmStockIconBook; // double
                case "stockmagnifying-glass":
                    return MmStockIcon.mmStockIconMagnifyingGlass;
                case "stocksingles_magnifying_glass":
                    return MmStockIcon.mmStockIconMagnifyingGlass; // double
                case "stockbroken-connection":
                    return MmStockIcon.mmStockIconBrokenConnection;
                case "stockinformation":
                    return MmStockIcon.mmStockIconInformation;
                case "stockfolder":
                    return MmStockIcon.mmStockIconFolder;
                case "stockdocuments_6":
                    return MmStockIcon.mmStockIconFolder; // double
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Get or add MapMarkerGroup (only text label group)
        /// </summary>
        /// <param name="aDocument"></param>
        /// <param name="aName">Name of marker group</param>
        /// <param name="aCreateNew">if to create marker group if not found</param>
        /// <param name="mutex">If group must be MutuallyExclusive</param>
        /// <returns>MapMarkerGroup</returns>
		public void GetIcon(MmStockIcon aIcon, string signature, string iconName, string path)
        {
            foreach (MapMarkerGroup mg in MMUtils.ActiveDocument.MapMarkerGroups)
            {
                if (mg.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeIcon || mg.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeSingleIcon)
                {
                    foreach (MapMarker icon in mg)
                    {
                        if (icon.Icon.Type == MmIconType.mmIconTypeStock)
                        {
                            if (icon.Icon.StockIcon == aIcon)
                                return;
                        }
                        else // custom icon
                        {
                            if (icon.Icon.CustomIconSignature == signature)
                                return;
                        }
                    }
                }
            }

            // Icon not found. Add icon to the Single Icons group
            MapMarkerGroup _mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeSingleIcon);
            if (signature == "")
                _mg.AddStockIconMarker(iconName, aIcon);
            else
                _mg.AddCustomIconMarker(iconName, path);
        }

        public static List<IconItem> Icons = new List<IconItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";

        int MinLength, MaxLength, Thickness;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    internal class IconItem
    {
        public IconItem(string name, string filename, int order) 
        {
            IconName = name;
            FileName = filename;
            Order = order;
        }
        public string IconName = "";
        public string FileName = "";
        public int Order = 0;
    }
}
