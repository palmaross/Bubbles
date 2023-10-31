using Bubbles.Properties;
using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Clipboard = System.Windows.Forms.Clipboard;

namespace Bubbles
{
    internal partial class BubbleIcons : Form
    {
        public BubbleIcons(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation;

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "IconStick.htm");

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, stickname);

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

            contextMenuStrip1.Items["BI_new"].Text = MMUtils.getString("float_icons.contextmenu.new");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_new"], p2, "newsticker.png");

            contextMenuStrip1.Items["BI_rename"].Text = MMUtils.getString("float_icons.contextmenu.edit");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_rename"], p2, "edit.png");

            contextMenuStrip1.Items["BI_paste"].Text = MMUtils.getString("float_icons.contextmenu.paste");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_paste"], p2, "paste.png");

            contextMenuStrip1.Items["BI_delete"].Text = MMUtils.getString("float_icons.contextmenu.delete");
            StickUtils.SetContextMenuImage(contextMenuStrip1.Items["BI_delete"], p2, "deleteall.png");

            StickUtils.SetCommonContextMenu(contextMenuStrip1, p2, StickUtils.typeicons);

            panel1.MouseDown += Panel1_MouseDown;
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            Manage.Click += Manage_Click;
            pictureHandle.Click += PictureHandle_Click;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from ICONS where stickID=" + ID + " order by _order");

                int k = 0;
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
                    {
                        IconItem item = new IconItem(name, filename, Convert.ToInt32(row["_order"]), rootPath + _filename);
                        Icons.Add(item);
                        PictureBox pBox = StickUtils.AddIcon(this, p1, item, rootPath + _filename,
                            panel1, orientation, icondist.Width, k++);
                        pBox.MouseClick += Icon_Click;
                        pBox.MouseMove += PBox_MouseMove;
                        pBox.DragEnter += Handle_DragEnter;
                        pBox.DragDrop += Handle_DragEnter;
                    }
                }
                RealLength = this.Width;
            }

            // Handle drag drop to place icon to the end
            this.DragEnter += Handle_DragEnter;
            this.DragDrop += Handle_DragDrop;

            // Handle drag drop to place icon to the begin
            pictureHandle.AllowDrop = true;
            pictureHandle.DragEnter += Handle_DragEnter;
            pictureHandle.DragDrop += Handle_DragDrop;
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Items["BI_delete"].Visible = false;
            contextMenuStrip1.Items["BI_rename"].Visible = false;
            toolStripSeparator1.Visible = false;

            selectedIcon = pictureHandle;
            manage = true;
            contextMenuStrip1.Show(Cursor.Position);
        }

        private void PictureHandle_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = false;

            contextMenuStrip1.Items["BI_paste"].Visible = true;

            manage = false;
            contextMenuStrip1.Show(Cursor.Position);
        }

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
            string iconPath, iconName, position = "";
            if (e.ClickedItem.Name == "BI_new")
            {
                using (SelectIconDlg _dlg = new SelectIconDlg(Icons, manage)) // correct
                {
                    if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                        return;
                    iconPath = _dlg.iconPath;
                    iconName = _dlg.iconName;
                    position = _dlg.rbtnEnd.Checked ? "end" :
                        _dlg.rbtnBegin.Checked ? "begin" :
                        _dlg.rbtnLeft.Checked ? "left" : "right";
                }
                NewIcon(iconPath, iconName, position);
            }
            else if (e.ClickedItem.Name == "BI_delete")
            {
                StickUtils.Icons.Clear(); StickUtils.Icons.AddRange(Icons);
                StickUtils.DeleteIcon(selectedIcon, (int)this.Tag, StickUtils.typeicons);
                Icons.Clear(); Icons.AddRange(StickUtils.Icons);
                RefreshStick();
            }
            else if (e.ClickedItem.Name == "BI_deleteall")
            {
                Icons.Clear();
                RefreshStick(true);
                if (collapsed)
                {
                    collapsed = false;
                    this.BackColor = System.Drawing.Color.Lavender;
                }
            }
            else if (e.ClickedItem.Name == "BI_rename")
            {
                IconItem item = (IconItem)selectedIcon.Tag;
                if (item == null) return;

                // Get new source's name
                string name = StickUtils.GetName(this, orientation, StickUtils.typeicons, item.IconName);
                if (name != "")
                {
                    // Change title in the picture box tag
                    ((IconItem)selectedIcon.Tag).IconName = name;
                    // Change title in the Source list item
                    Icons.Find(p => p.Path == item.Path).IconName = name;
                    toolTip1.SetToolTip(selectedIcon, name);

                    // Change title in the database
                    using (BubblesDB db = new BubblesDB())
                        db.ExecuteNonQuery("update ICONS set name=`" + name + "` where filename=`" +
                            item.Path + "` and stickID=" + (int)this.Tag + "");
                }
            }
            else if (e.ClickedItem.Name == "BI_paste")
            {
                string path = null;
                string[] copiedFiles = (string[])Clipboard.GetData(DataFormats.FileDrop);
                if (path == null && copiedFiles == null)
                    return;

                string title = StickUtils.Handle_DragDrop(ref path, copiedFiles, Icons, null);

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
                    string name = StickUtils.GetName(this, orientation, StickUtils.typeicons, title);
                    if (name != "")
                        NewIcon(path, name, position);
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
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "IconStick.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StickUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_newstick")
            {
                string name = StickUtils.GetName(this, orientation, StickUtils.typeicons, "");
                if (name != "")
                {
                    BubbleIcons form = new BubbleIcons(0, orientation, name);
                    StickUtils.CreateStick(form, name, StickUtils.typeicons);
                }
            }
            else if (e.ClickedItem.Name == "BI_renamestick")
            {
                string newName = StickUtils.GetName(this, orientation, StickUtils.typeicons, toolTip1.GetToolTip(pictureHandle));
                if (newName != "") toolTip1.SetToolTip(pictureHandle, newName);
            }
            else if (e.ClickedItem.Name == "BI_expand")
            {
                if (StickUtils.Expand(this, RealLength, orientation))
                    collapsed = false;
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                if (StickUtils.Collapse(this, MinLength, orientation))
                    collapsed = true;
            }
            else if (e.ClickedItem.Name == "BI_delete_stick")
            {
                if (StickUtils.DeleteStick((int)this.Tag, StickUtils.typeicons))
                    this.Close();
            }
        }

        private void NewIcon(string iconPath, string iconName, string position)
        {
            string fileName = "stock" + Path.GetFileNameWithoutExtension(iconPath);

            if (StockIconFromString(fileName) == 0) // кастомная иконка, сохраним файл в нашей базе
            {
                fileName = Path.GetFileName(iconPath);
                if (!File.Exists(Utils.m_dataPath + "IconDB\\" + fileName))
                    File.Copy(iconPath, Utils.m_dataPath + "IconDB\\" + fileName);
            }

            // Add icon to Icons list
            int order = Icons.Count + 1; // at the end
            if (position == "begin")
                order = 1;
            if (position == "left" || position == "right")
            {
                IconItem _item = (IconItem)selectedIcon.Tag;
                if (position == "left")
                    order = _item.Order == 1 ? 1 : _item.Order;
                else
                    order = _item.Order == Icons.Count ? Icons.Count + 1 : _item.Order + 1;
            }

            IconItem item = new IconItem(iconName, fileName, order, iconPath);
            using (BubblesDB db = new BubblesDB())
                db.AddIcon(iconName, fileName, order, (int)this.Tag);

            Icons.Insert(order - 1, item);
            for (int i = 0; i < Icons.Count; i++)
                Icons[i].Order = i + 1;

            RefreshStick();
        }

        void RefreshStick(bool deleteall = false)
        {
            StickUtils.Icons.Clear(); StickUtils.Icons.AddRange(Icons);
            List<PictureBox> pBoxs = StickUtils.RefreshStick(this, p1, panel1, orientation, Thickness, 
                MinLength, panel1MinLength, icondist.Width, ref RealLength, StickUtils.typeicons, deleteall);

            foreach (PictureBox pBox in pBoxs)
            {
                pBox.MouseClick += Icon_Click;
                pBox.MouseMove += PBox_MouseMove;
                pBox.DragEnter += Handle_DragEnter;
                pBox.DragDrop += Handle_DragDrop;
            }

            if (collapsed)
                this.Width = MinLength;
        }

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
                int sourceIndex = (source.Tag as IconItem).Order; // moving PB order
                int targetIndex = 0;

                if (sender is PictureBox) // also, can be this form
                {
                    var target = (PictureBox)sender;

                    if (target.Name == "pictureHandle")
                        targetIndex = 0; // move PB to the begin
                    else
                    {
                        // or after the target PB
                        try { targetIndex = (target.Tag as IconItem).Order; }
                        catch { }
                    }
                }
                else // sender is this form or something else (moving PB to the end)
                    targetIndex = panel1.Controls.OfType<PictureBox>().Count() - 2; // minus pictureHandle and p1

                if (sourceIndex != targetIndex)
                {
                    // Reorder Sources list
                    Icons.RemoveAt(sourceIndex - 1);
                    if (sourceIndex < targetIndex) { targetIndex--; }
                    Icons.Insert(targetIndex, source.Tag as IconItem);
                    for (int i = 0; i < Icons.Count; i++)
                        Icons[i].Order = i + 1;

                    RefreshStick();
                }
            }
            else // Drop *dragged* data
            {
                string path = null;
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string title = StickUtils.Handle_DragDrop(ref path, draggedFiles, Icons, null);

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

                    // Get icon name
                    string name = StickUtils.GetName(this, orientation, StickUtils.typesources, title);
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

        private void PBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var pb = (PictureBox)sender;
                pb.DoDragDrop(pb, DragDropEffects.Move);
            }
        }

        #endregion

        #region stock icon enumaration
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
        #endregion

        /// <summary>
        /// Check if icon exists in the Map Index. If not, add icon to Map Index
        /// </summary>
        /// <param name="aIcon">Stock Icon to check</param>
        /// <param name="signature">Custom Icon signature</param>
        /// <param name="iconName">Icon name</param>
        /// <param name="path">Path to custom icon</param>
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

        private List<IconItem> Icons = new List<IconItem>();
        PictureBox selectedIcon = null;
        string orientation = "H";
        bool collapsed = false;
        string new_icon, position;
        bool manage = false;

        int MinLength, RealLength, Thickness, panel1MinLength;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        // For release capture
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public class IconItem
    {
        public IconItem(string name, string filename, int order, string path) 
        {
            IconName = name;
            FileName = filename;
            Order = order;
            Path = path;
        }
        public string IconName = "";
        public string FileName = "";
        public int Order = 0;
        public string Path = "";
    }
}
