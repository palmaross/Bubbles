using Bubbles;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Organizer
{
    public partial class ManageNoteMarkers : UserControl
    {
        public ManageNoteMarkers()
        {
            InitializeComponent();

            lblNoteMarkers.Text = Utils.getString("ManageNoteMarkers.lblNoteMarkers");
            lblIconUp.Text = Utils.getString("ManageNoteMarkers.lblIconUp");
            lblIconDown.Text = Utils.getString("ManageNoteMarkers.lblIconDown");
            lblSelectedIcon.Text = Utils.getString("ManageNoteMarkers.lblSelectedIcon");
            btnReplace.Text = Utils.getString("ManageNoteMarkers.btnReplace");
            btnRename.Text = Utils.getString("ManageNoteMarkers.btnRename");
            btnDelete.Text = Utils.getString("button.delete");
            lblTip.Text = Utils.getString("ManageNoteMarkers.lblTip");

            NoteIcons = new List<PictureBox>() { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16 };

            using (BubblesDB db = new BubblesDB())
            {
                // Fill Note Icons sets
                DataTable dt = db.ExecuteQuery("select * from NOTEICONS order by id");
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int order = Convert.ToInt32(row["_order"]) - 1;
                    if (i > 7) { order += 8; }

                    if (row["fileName"].ToString() == "") // empty icon
                    {
                        NoteIcons[order].BorderStyle = BorderStyle.FixedSingle;
                        NoteIcons[order].Tag = new NoteIconItem("", "", Convert.ToInt32(row["_order"]), i + 1);
                    }
                    else
                    {
                        string name = row["name"].ToString();
                        string filename = row["filename"].ToString();
                        string _filename = filename;
                        string rootPath = Utils.m_dataPath + "IconDB\\";

                        filenames.Add(filename);

                        if (filename.StartsWith("stock"))
                        {
                            rootPath = MMUtils.MindManager.GetPath(Mindjet.MindManager.Interop.MmDirectory.mmDirectoryIcons);
                            _filename = filename.Substring(5) + ".ico"; // stockemail -> email.ico
                        }

                        if (File.Exists(rootPath + _filename))
                        {
                            NoteIcons[order].Tag = new NoteIconItem(name, filename, Convert.ToInt32(row["_order"]), i + 1);
                            NoteIcons[order].Image = Image.FromFile(rootPath + _filename);
                            toolTip1.SetToolTip(NoteIcons[order], name);
                        }
                    }
                    NoteIcons[i].MouseClick += Icon_Click;
                    NoteIcons[i].AllowDrop = true;
                    NoteIcons[i].MouseMove += PBox_MouseMove;
                    NoteIcons[i].DragEnter += Handle_DragEnter;
                    NoteIcons[i].DragDrop += Handle_DragDrop;
                    i++;
                }
            }
        }

        private void Icon_Click(object sender, MouseEventArgs e)
        {
            // Deselect previous selected icon
            if (selectedIcon != null)
                selectedIcon.BackColor = Color.Transparent;

            selectedIcon = sender as PictureBox;

            // If clicked icon is an empty icon, go to Select Icon dialog
            if (((NoteIconItem)selectedIcon.Tag).FileName == "")
                GetIcon();
            else // Otherwise, select clicked icon
            {
                selectedIcon.BackColor = SystemColors.HotTrack;
                txtRename.Text = ((NoteIconItem)selectedIcon.Tag).Name;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (selectedIcon != null)
                GetIcon();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedIcon == null) return;
            if (selectedIcon.Image == null) return;

            var item = selectedIcon.Tag as NoteIconItem;

            selectedIcon.Image.Dispose(); selectedIcon.Image = null;
            selectedIcon.BackColor = Color.Transparent;
            filenames.Remove(item.FileName);
            item.FileName = "";
            item.Name = "";
            selectedIcon.Tag = item;
            toolTip1.SetToolTip(selectedIcon, "");
            selectedIcon.BorderStyle = BorderStyle.FixedSingle;
            txtRename.Text = "";

            using (BubblesDB db = new BubblesDB())
                db.ExecuteNonQuery("update NOTEICONS set name='', fileName='' where id=" + item.ID + "");
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtRename.Text.Trim())) return;
            if (selectedIcon == null) return;

            var item = selectedIcon.Tag as NoteIconItem;
            if (item.FileName == "") return;

            item.Name = txtRename.Text.Trim();
            selectedIcon.Tag = item;
            toolTip1.SetToolTip(selectedIcon, item.Name);

            using (BubblesDB db = new BubblesDB())
            {
                db.ExecuteNonQuery("update NOTEICONS set " +
                    "name=`" + item.Name + "` where id=" + item.ID + "");
            }
        }

        void GetIcon()
        {
            if (selectedIcon == null) return;
            var icon = selectedIcon.Tag as NoteIconItem;

            string iconPath, iconName;

            using (SelectIconDlg _dlg = new SelectIconDlg(filenames, true, true)) // correct
            {
                if (_dlg.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd)) == DialogResult.Cancel)
                    return;
                iconPath = _dlg.iconPath;
                iconName = _dlg.iconName;
            }

            string fileName = "stock" + Path.GetFileNameWithoutExtension(iconPath);

            if (BubbleIcons.StockIconFromString(fileName) == 0) // кастомная иконка, сохраним файл в нашей базе
            {
                fileName = Path.GetFileName(iconPath);
                if (!File.Exists(Utils.m_dataPath + "IconDB\\" + fileName))
                    File.Copy(iconPath, Utils.m_dataPath + "IconDB\\" + fileName);
            }

            filenames.Remove(icon.FileName); // if icon is replacing
            filenames.Add(fileName);
            icon.FileName = fileName;
            icon.Name = iconName;
            selectedIcon.Image = Image.FromFile(iconPath);
            selectedIcon.Tag = icon;
            toolTip1.SetToolTip(selectedIcon, iconName);
            selectedIcon.BorderStyle = BorderStyle.None;
            selectedIcon.BackColor = Color.Transparent;
            txtRename.Text = iconName;

            using (BubblesDB db = new BubblesDB())
            {
                db.ExecuteNonQuery("update NOTEICONS set " +
                    "name=`" + iconName +
                    "`, fileName=`" + fileName +
                    "` where id=" + icon.ID + "");
            }
        }

        #region DragDrop
        private void Handle_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is PictureBox == false)
                return;
            var target = (PictureBox)sender;

            if (e.Data.GetDataPresent(typeof(PictureBox))) // Move the picture box
            {
                var source = (PictureBox)e.Data.GetData(typeof(PictureBox)); // moving PB
                if (source == target) return;

                // castling
                Image img = target.Image;
                target.Image = source.Image;
                source.Image = img;

                NoteIconItem _target = target.Tag as NoteIconItem;
                NoteIconItem _source = source.Tag as NoteIconItem;
                int order = _target.Order;
                _target.Order = _source.Order;
                _source.Order = order;

                if (target.Image == null)
                    target.BorderStyle = BorderStyle.FixedSingle;
                else
                    target.BorderStyle = BorderStyle.None;
                if (source.Image == null)
                    source.BorderStyle = BorderStyle.FixedSingle;
                else
                    source.BorderStyle = BorderStyle.None;

                using (BubblesDB db = new BubblesDB())
                {
                    db.ExecuteNonQuery("update NOTEICONS set " +
                        "_order=" + _target.Order + " where id=" + _target.ID + "");
                    db.ExecuteNonQuery("update NOTEICONS set " +
                        "_order=" + _source.Order + " where id=" + _source.ID + "");
                }
            }
            else // Drop *dragged* data
            {
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string path = draggedFiles[0];
                string fileName = "stock" + Path.GetFileNameWithoutExtension(path);
                string name = fileName;

                if (BubbleIcons.StockIconFromString(fileName) == 0) // кастомная иконка, сохраним файл в нашей базе
                {
                    fileName = Path.GetFileName(path);
                    if (!File.Exists(Utils.m_dataPath + "IconDB\\" + fileName))
                        File.Copy(path, Utils.m_dataPath + "IconDB\\" + fileName);
                }

                if (filenames.Contains(fileName))
                { MessageBox.Show(Utils.getString("float_icons.iconexists")); return; }

                target.Image = Image.FromFile(path);
                filenames.Add(fileName);
                txtRename.Text = name;
                target.BorderStyle = BorderStyle.None;

                NoteIconItem _target = target.Tag as NoteIconItem;
                _target.FileName = fileName;
                _target.Name = name;
                target.Tag = _target;

                using (BubblesDB db = new BubblesDB())
                {
                    db.ExecuteNonQuery("update NOTEICONS set " +
                        "fileName=`" + _target.FileName + "`, " +
                        "name=`" + _target.Name +
                        "` where id=" + _target.ID + "");
                }
            }
        }

        private void Handle_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox))) // Moving picture box
            {
                var source = (PictureBox)e.Data.GetData(typeof(PictureBox)); // moving PB

                if (sender is PictureBox)
                {
                    var target = (PictureBox)sender;
                    if (((source.Tag as NoteIconItem).ID < 9 && (target.Tag as NoteIconItem).ID < 9) ||
                        ((source.Tag as NoteIconItem).ID > 8 && (target.Tag as NoteIconItem).ID > 8))
                        e.Effect = DragDropEffects.Move;
                    else
                        e.Effect = DragDropEffects.None;
                }
            }
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

        List<PictureBox> NoteIcons;
        PictureBox selectedIcon = null;
        List<string> filenames = new List<string>();
    }

    public class NoteIconItem
    {
        public NoteIconItem(string name, string fileName, int order, int id)
        {
            Name = name;
            FileName = fileName;
            Order = order;
            ID = id;
        }
        public string Name = "";
        public string FileName = "";
        public int Order = 0;
        public int ID = 0;

        public override string ToString() => Name;
    }
}
