using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
using Color = System.Drawing.Color;

namespace Bubbles
{
    public partial class NotesDlg : Form
    {
        public NotesDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "OrganizerNotes.htm");

            // Form
            Text = Utils.getString("notes.window.title");
            lblNotesName.Text = Utils.getString("notes.window.title");
            toolTip1.SetToolTip(pBack, Utils.getString("notes.pBack"));
            toolTip1.SetToolTip(pMore, Utils.getString("notes.pMore"));
            toolTip1.SetToolTip(pClose, Utils.getString("button.close"));
            toolTip1.SetToolTip(pMinimize, Utils.getString("notes.pMinimize"));
            lblGroupBottom.Text = Utils.getString("notes.lblGroup");
            toolTip1.SetToolTip(pSearch, Utils.getString("notes.pSearch"));
            toolTip1.SetToolTip(pCloseSearch, Utils.getString("notes.pCloseSearch"));
            toolTip1.SetToolTip(pSort, Utils.getString("notes.contextmenu.sort"));

            // FlowLayoutPanel
            toolTip1.SetToolTip(pNewNote, Utils.getString("notes.pNewNote"));

            // Note panel
            lblGroup.Text = Utils.getString("notes.lblGroup");
            toolTip1.SetToolTip(pAddNote, Utils.getString("notes.pAddNote"));

            // Manage Groups panel
            toolTip1.SetToolTip(btnDeleteGroup, Utils.getString("notes.group.delete.tooltip"));
            groupBoxAddNewGroup.Text = Utils.getString("notes.group.groupBoxNewGroup");
            btnAddGroup.Text = Utils.getString("button.add");
            groupBoxRenameGroup.Text = Utils.getString("notes.group.groupBoxRenameGroup");
            btnRenameGroup.Text = Utils.getString("notes.group.rename");

            // Delete Group panel
            rbtnDeleteGroupAndNotes.Text = Utils.getString("notes.group.rbtnDeleteGroupAndNotes");
            rbtnDeleteGroupNoNotes.Text = Utils.getString("notes.group.rbtnDeleteGroupNoNotes");
            rbtnDeleteGroupCancel.Text = Utils.getString("notes.group.rbtnDeleteGroupCancel");

            // Context menu
            cmsMoreButton.ItemClicked += ContextMenu_ItemClicked;
            cmsSort.ItemClicked += ContextMenu_ItemClicked;

            cmsMoreButton.Items["N_notes"].Text = MMUtils.getString("notes.contextmenu.notescaption");

            cmsMoreButton.Items["N_delete"].Text = MMUtils.getString("notes.contextmenu.delete");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_delete"], p2, "deleteall.png");

            cmsMoreButton.Items["N_deleteall"].Text = MMUtils.getString("notes.contextmenu.deleteall");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_deleteall"], p2, "deleteall.png");

            cmsMoreButton.Items["N_edit"].Text = MMUtils.getString("notes.contextmenu.edit");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_edit"], p2, "edit.png");

            cmsMoreButton.Items["N_addtopic"].Text = MMUtils.getString("notes.contextmenu.addtopic");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_addtopic"], p2, "addtotopic.png");

            cmsMoreButton.Items["N_GroupTitle"].Text = MMUtils.getString("notes.contextmenu.groups.title");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_GroupTitle"], p2, "groups.png");

            cmsMoreButton.Items["N_groups"].Size = new Size(cbGroupDummy.Width, cbGroupDummy.Height);

            cmsMoreButton.Items["N_managegroups"].Text = MMUtils.getString("notes.contextmenu.managegroups");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_managegroups"], p2, "manageGroups.png");

            cmsMoreButton.Items["N_help"].Text = MMUtils.getString("button.help");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_help"], p2, "helpSticker.png");

            cmsSort.Items["N_sortAZ"].Text = MMUtils.getString("notes.contextmenu.sortAZ");

            cmsSort.Items["N_sortZA"].Text = MMUtils.getString("notes.contextmenu.sortZA");

            cmsSort.Items["N_sortgroup"].Text = MMUtils.getString("notes.contextmenu.sortgroup");

            cmsSort.Items["N_sortlinks"].Text = MMUtils.getString("notes.contextmenu.sortlinks");

            this.Width = panelFormWidth.Width;
            thisWidth = this.Width;
            this.MouseDown += NotesDlg_MouseDown;
            pBack.Location = pMore.Location;
            _noteText.LinkClicked += richTextBox1_LinkClicked;

            AddContextMenu(_noteText);

            // PreFill group comboboxes
            N_groups.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.all"), -1));
            cbGroupsBottom.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.all"), -1));
            N_groups.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.nogroup"), 0));
            cbNewNoteGroup.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.nogroup"), 0));
            cbGroupsBottom.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.nogroup"), 0));

            // Fill group comboboxes
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTEGROUPS order by name");
                foreach (DataRow row in dt.Rows)
                {
                    NoteGroupItem item = new NoteGroupItem(row["name"].ToString(), Convert.ToInt32(row["id"]));
                    N_groups.Items.Add(item);
                    cbGroups.Items.Add(item);
                    cbNewNoteGroup.Items.Add(item);
                    cbGroupsBottom.Items.Add(item);
                }
            }

            N_groups.SelectedIndex = 0; // context menu
            cbGroupsBottom.SelectedIndex = 0; // form head
            if (cbGroups.Items.Count > 0) // manage groups panel
                cbGroups.SelectedIndex = 0;
            cbNewNoteGroup.SelectedIndex = 0; // new/edit note panel

            //lblNoteGroup.Location = new Point(lblNotesName.Location.X + lblNotesName.Width, lblNoteGroup.Location.Y);
            lblNoteGroup.Text = Utils.getString("notes.notegroup.all");
            FillNotes(-1);
        }

        /// <summary>
        /// Fill FlowLayoutPanel with notes
        /// </summary>
        /// <param name="groupID">Note group ID</param>
        /// <param name="sort">sort parameter ("A-Z", "Z-A", "group")</param>
        void FillNotes(int groupID, string sorttype = "")
        {
            flowLayoutPanel1.Controls.Clear();

            if (NOTES.Count == 0) // first start or group changed
            {
                using (BubblesDB db = new BubblesDB())
                {
                    DataTable dt;
                    if (groupID == -1) // Notes from all groups
                        dt = db.ExecuteQuery("select * from NOTES");
                    else
                        dt = db.ExecuteQuery("select * from NOTES where groupID=" + groupID + "");

                    foreach (DataRow row in dt.Rows)
                    {
                        NOTES.Add(new NoteItem(row["name"].ToString(), row["content"].ToString(), row["link"].ToString(),
                            Convert.ToInt32(row["id"]), Convert.ToInt32(row["groupID"])));

                        if (sorttype == "AZ")
                            NOTES.Sort((a, z) => a.Title.CompareTo(z.Title));
                        else if (sorttype == "ZA")
                            NOTES.Sort((z, a) => a.Title.CompareTo(z.Title));
                        else if (sorttype == "group")
                            NOTES.Sort((x, y) => x.GroupID.CompareTo(y.GroupID));
                        else if (sorttype == "link")
                            NOTES.Sort((x, y) => x.Link.CompareTo(y.Link));

                        foreach (var note in NOTES)
                            AddNote(note.Title, note.Content, note.Link, note.ID, note.GroupID);
                    }
                }
            }
            else // Sort notes. Use the NOTES list.
            {
                if (sorttype == "AZ")
                    NOTES.Sort((a, z) => a.Title.CompareTo(z.Title));
                else if (sorttype == "ZA")
                    NOTES.Sort((z, a) => a.Title.CompareTo(z.Title));
                else if (sorttype == "group")
                    NOTES.Sort((x, y) => x.GroupID.CompareTo(y.GroupID));
                else if (sorttype == "link")
                    NOTES.Sort((x, y) => x.Link.CompareTo(y.Link));
                else if (sorttype == "id")
                    NOTES.Sort((x, y) => x.ID.CompareTo(y.ID));

                foreach (var note in NOTES)
                    AddNote(note.Title, note.Content, note.Link, note.ID, note.GroupID);
            }
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "N_managegroups")
            {
                panelManageGroups.Location = flowLayoutPanel1.Location;
                panelManageGroups.Visible = true;
                panelManageGroups.BringToFront();
                pBack.Visible = true;
                txtSearch.Visible = false;
                pNewNote.SendToBack();

                if (cbGroups.Items.Count > 0)
                    cbGroups.SelectedIndex = 0;
            }
            else if (e.ClickedItem.Name == "N_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OrganizerNotes.htm");
            }
        }

        private void cbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRenameGroup.Text = cbGroups.SelectedItem.ToString() + " (2)";
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void pAddNote_Click(object sender, EventArgs e)
        {
            if (_noteTitle.ForeColor == SystemColors.ControlDark || _noteText.ForeColor == SystemColors.ControlDark)
                return; // Note must have Title and Content

            if (edit) // note has been edited
            {
                if (selectedNotes == null || selectedNotes.Count == 0)
                    return;

                Panel selectedNote = (Panel)selectedNotes[0];

                foreach (TextBox tb in selectedNote.Controls.OfType<TextBox>())
                {
                    if (tb.Name == "Title")
                        tb.Text = _noteTitle.Text.Trim();
                    if (tb.Name == "Content")
                    {
                        // text box doesn't make a new line with "\n", but yes do with "\r\n" 
                        tb.Text = Regex.Replace(_noteText.Text.Trim(), "(?<!\r)\n", "\r\n");
                        
                        if (String.IsNullOrEmpty(_noteLink.Text) || _noteLink.ForeColor == SystemColors.ControlDark)
                        { // There is not link
                            if (tb.Controls.Count > 0)
                                tb.Controls.RemoveAt(0);
                        }
                        else // There is a link
                        {
                            if (tb.Controls.Count > 0)
                            {
                                tb.Controls[0].Tag = _noteLink.Text;
                                toolTip1.SetToolTip(tb.Controls[0], _noteLink.Text);
                            }
                            else
                            {
                                PictureBox pb = new PictureBox();
                                pb.Image = pLink.Image;
                                pb.SizeMode = PictureBoxSizeMode.Zoom;
                                pb.Location = pLink.Location;
                                pb.Size = pLink.Size;
                                pb.Cursor = Cursors.Hand;
                                tb.Controls.Add(pb);
                                toolTip1.SetToolTip(pb, _noteLink.Text);
                                pb.Tag = _noteLink.Text.Trim();
                                pb.Click += Pb_Click;
                            }
                        }
                    }
                }

                int id = (selectedNote.Tag as NoteItem).ID;
                var group = cbNewNoteGroup.SelectedItem as NoteGroupItem;
                string link = _noteLink.Text.Trim();
                if (_noteLink.ForeColor == SystemColors.ControlDark) link = "";

                using (BubblesDB db = new BubblesDB())
                    db.ExecuteNonQuery("update NOTES set " +
                        "name=`" + _noteTitle.Text.Trim() + "`, " +
                        "content=`" + _noteText.Text.Trim() + "`, " +
                        "link=`" + link + "`, " + 
                        "groupID=" + group.ID + " where id=" + id + "");

                selectedNote.Tag = new NoteItem(_noteTitle.Text.Trim(), _noteText.Text.Trim(), link, id, group.ID);
                edit = false;
            }
            else // new note
            {
                string link = "";
                if (!String.IsNullOrEmpty(_noteLink.Text) && _noteLink.ForeColor == SystemColors.HotTrack)
                    link = _noteLink.Text.Trim();

                Panel note = AddNote(_noteTitle.Text.Trim(), _noteText.Text.Trim(), link);

                var group = cbNewNoteGroup.SelectedItem as NoteGroupItem;

                using (BubblesDB db = new BubblesDB())
                {
                    db.AddNote(_noteTitle.Text.Trim(), _noteText.Text.Trim(), link, group.ID);
                    
                    // Get note id and save it to the panel Tag
                    int id = 0;
                    DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                    note.Tag = new NoteItem(_noteTitle.Text.Trim(), _noteText.Text.Trim(), link, id, group.ID);
                }
            }

            panelNote.Visible = false;
            pBack.Visible = false;
            pNewNote.BringToFront();
        }

        private Panel AddNote(string aNoteTitle, string aNoteContent, string aNoteLink, int noteID = 0, int noteGroupID = 0)
        {
            Panel note = new Panel();
            note.Size = panelDummy.Size;

            TextBox noteTitle = new TextBox();
            noteTitle.Name = "Title";
            noteTitle.Size = dummyTitle.Size;
            noteTitle.Location = dummyTitle.Location;
            noteTitle.Font = dummyTitle.Font;
            noteTitle.BorderStyle = BorderStyle.None;
            noteTitle.Text = aNoteTitle;
            noteTitle.MouseDown += Note_MouseDown;
            noteTitle.MouseDoubleClick += Note_MouseDoubleClick;
            noteTitle.KeyUp += Note_KeyUp;
            noteTitle.ContextMenuStrip = cmsMoreButton;
            note.Controls.Add(noteTitle);

            TextBox noteText = new TextBox();
            noteText.Name = "Content";
            noteText.Multiline = true;
            noteText.Size = dummyText.Size;
            noteText.Location = dummyText.Location;
            noteText.Font = dummyText.Font;
            noteText.BorderStyle = BorderStyle.None;
            noteText.Text = Regex.Replace(aNoteContent, "(?<!\r)\n", "\r\n"); // text box doesn't make a new line with "\n", but yes do with "\r\n" 
            noteText.MouseDown += Note_MouseDown;
            noteText.MouseDoubleClick += Note_MouseDoubleClick;
            noteText.KeyUp += Note_KeyUp;
            noteText.ContextMenuStrip = cmsMoreButton;
            note.Controls.Add(noteText);

            if (!String.IsNullOrEmpty(aNoteLink))
            {
                PictureBox pb = new PictureBox();
                pb.Image = pLink.Image;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Location = pLink.Location;
                pb.Size = pLink.Size;
                pb.Cursor = Cursors.Hand;
                noteText.Controls.Add(pb);
                toolTip1.SetToolTip(pb, aNoteLink);
                pb.Tag = aNoteLink;
                pb.Click += Pb_Click;
            }

            if (noteID != 0)
                note.Tag = new NoteItem(aNoteTitle, aNoteContent, aNoteLink, noteID, noteGroupID);

            flowLayoutPanel1.Controls.Add(note);
            flowLayoutPanel1.SetFlowBreak(note, true);

            return note;
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            try{
                System.Diagnostics.Process.Start(((PictureBox)sender).Tag.ToString());
            }
            catch { }
        }

        private void Note_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedNotes();
        }

        private void DeleteSelectedNotes()
        {
            if (selectedNotes == null)
                return;
            if (selectedNotes.Count > 1)
            {
                if (MessageBox.Show(Utils.getString("notes.deletenotes.confirm"), 
                    Utils.getString("notes.deletenotes.caption"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }

            List<int> ids = new List<int>();
            // Delete notes (note paneles) from the container
            foreach (Panel note in selectedNotes.OfType<Panel>().Reverse<Panel>())
            {
                int id = (note.Tag as NoteItem).ID;
                ids.Add(id);
                flowLayoutPanel1.Controls.Remove(note);
            }
            // Delete notes from the database
            using (BubblesDB db = new BubblesDB())
            {
                foreach (int id in ids)
                    db.ExecuteNonQuery("delete from NOTES where id=" + id + "");
            }
        }

        // Edit note
        private void Note_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            panelNote.Visible = true;
            panelNote.Location = flowLayoutPanel1.Location;
            panelNote.BringToFront();
            pBack.Visible = true;

            Panel note = (sender as TextBox).Parent as Panel;
            NoteItem item = note.Tag as NoteItem;

            _noteTitle.Text = item.Title;
            _noteTitle.ForeColor = SystemColors.WindowText;
            _noteText.Text = item.Content;
            _noteText.ForeColor = SystemColors.WindowText;

            _noteLink.Text = item.Link;
            if (_noteLink.Text == "")
            {
                _noteLink.Text = Utils.getString("notes.link.dummy");
                _noteLink.ForeColor = SystemColors.ControlDark;
            }
            else
                _noteText.ForeColor = SystemColors.WindowText;

            selectedNotes.Clear();
            selectedNotes.Add(note);
            pAddNote.BringToFront();
            edit = true;
        }

        /// <summary>
        /// Click on the note (note title textbox or note content textbox)
        /// </summary>
        private void Note_MouseDown(object sender, MouseEventArgs e)
        {
            Panel notePanel = (sender as TextBox).Parent as Panel;

            if (e.Button == MouseButtons.Right) // Show note context menu
            {
                foreach (ToolStripItem item in cmsMoreButton.Items)
                    item.Visible = false;

                cmsMoreButton.Items["N_edit"].Visible = true;
                cmsMoreButton.Items["N_delete"].Visible = true;
                cmsMoreButton.Items["N_addtopic"].Visible = true;
                cmsMoreButton.Show(Cursor.Position);

                if (selectedNotes.Contains(notePanel))
                    return; // right click on the selected note
            }

            if (ModifierKeys != Keys.Control) // if not Control key pressed, clear previous selections
            {
                foreach (Control control in flowLayoutPanel1.Controls) // Note Paneles
                {
                    foreach (Control _control in control.Controls) // TextBoxes in the Paneles
                        _control.BackColor = SystemColors.Window;
                }
                selectedNotes.Clear();
            }

            
            if (selectedNotes.Contains(notePanel))
            {
                // Deselect note Panel
                foreach (Control control in notePanel.Controls)
                    control.BackColor = SystemColors.Window;

                selectedNotes.Remove(notePanel);
            }
            else
            {
                // Highlight textboxes in the selected note Panel
                foreach (Control control in notePanel.Controls)
                    control.BackColor = Color.LightSkyBlue;

                selectedNotes.Add(notePanel);
            }
        }

        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            int i = flowLayoutPanel1.Controls.Count;
            if (i > 6 && i < 12)
                this.Height = panelToSize.Height + ((i-1) * (panelDummy.Height + pCorrect.Height)) - (i - 6);
            else if (i == 12)
                this.Width = this.Width + pCorrect.Width;
        }

        private void flowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            int i = flowLayoutPanel1.Controls.Count;
            if (i > 6 && i < 11)
                this.Height = panelToSize.Height + ((i - 1) * (panelDummy.Height + pCorrect.Height)) - (i - 6);
            else if (i == 11)
                this.Width = thisWidth;
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            this.Close();
            BubblesButton.m_Notes = null;
        }

        private void pMore_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsMoreButton.Items)
                item.Visible = true;

            cmsMoreButton.Show(Cursor.Position);
        }

        private void pNewNote_Click(object sender, EventArgs e)
        {
            panelNote.Visible = true;
            panelNote.Location = flowLayoutPanel1.Location;
            panelNote.BringToFront();
            pBack.Visible = true;
            pAddNote.Visible = true;
            pAddNote.BringToFront();

            panelNoteGroup.Visible = true;
            panelNoteGroup.Location = new Point(pMore.Location.X + pNewNote.Width, panelNoteGroup.Location.Y);

            _noteTitle.Text = Utils.getString("notes.title.dummy");
            _noteTitle.ForeColor = SystemColors.ControlDark;
            _noteText.Text = Utils.getString("notes.content.dummy");
            _noteText.ForeColor = SystemColors.ControlDark;
            _noteLink.Text = Utils.getString("notes.link.dummy");
            _noteLink.ForeColor = SystemColors.ControlDark;
        }

        public void AddContextMenu(RichTextBox rtb)
        {
            if (rtb.ContextMenuStrip == null)
            {
                ContextMenuStrip cms = new ContextMenuStrip()
                {
                    ShowImageMargin = false
                };

                ToolStripMenuItem tsmiCut = new ToolStripMenuItem(MMUtils.getString("button.cut"));
                tsmiCut.Click += (sender, e) => rtb.Cut();
                cms.Items.Add(tsmiCut);

                ToolStripMenuItem tsmiCopy = new ToolStripMenuItem(MMUtils.getString("button.copy"));
                tsmiCopy.Click += (sender, e) => rtb.Copy();
                cms.Items.Add(tsmiCopy);

                ToolStripMenuItem tsmiPaste = new ToolStripMenuItem(MMUtils.getString("button.paste"));
                tsmiPaste.Click += (sender, e) => rtb.Paste();
                cms.Items.Add(tsmiPaste);

                cms.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem tsmiSelectAll = new ToolStripMenuItem(MMUtils.getString("button.selectall"));
                tsmiSelectAll.Click += (sender, e) => rtb.SelectAll();
                cms.Items.Add(tsmiSelectAll);

                cms.Opening += (sender, e) =>
                {
                    tsmiCut.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                    tsmiCopy.Enabled = rtb.SelectionLength > 0;
                    tsmiPaste.Enabled = !rtb.ReadOnly && System.Windows.Clipboard.ContainsText();
                    tsmiSelectAll.Enabled = rtb.TextLength > 0 && rtb.SelectionLength < rtb.TextLength;
                };

                rtb.ContextMenuStrip = cms;
            }
        }

        private void NotesDlg_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void _noteText_TextChanged(object sender, EventArgs e)
        {
            var lines = _noteText.Lines.Count();
        }

        private void pBack_Click(object sender, EventArgs e)
        {
            panelManageGroups.Visible = false;
            panelNote.Visible = false;
            panelNoteGroup.Visible = false;
            panelDeleteGroup.Visible = false;

            pBack.Visible = false;
            pAddNote.Visible = false;
            pNewNote.BringToFront();
        }

        private void _noteLink_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_noteLink.Text);
            }
            catch { }
        }

        private void pSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Visible) // search for text
            {
                if (String.IsNullOrEmpty(txtSearch.Text.Trim()))
                    return;

                // First, clear all notes backcolor
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    foreach (Control _control in control.Controls)
                        _control.BackColor = SystemColors.Window;
                }

                // Search
                foreach (Panel note in flowLayoutPanel1.Controls.OfType<Panel>())
                {
                    foreach (TextBox tb in note.Controls.OfType<TextBox>())
                    {
                        if (tb.Text.ToLower().Contains(txtSearch.Text.ToLower()))
                        {
                            // Hihglight found note
                            tb.BackColor = Color.LightSkyBlue;
                        }

                        if (tb.Name == "Content" && tb.Controls.Count > 0) // there is a Link picture box
                        {
                            if (tb.Controls[0].Tag.ToString().ToLower().Contains(txtSearch.Text.ToLower()))
                            {
                                // Hihglight Link
                                tb.Controls[0].BackColor = Color.LawnGreen;
                            }
                        }
                    }
                }
            }
            else // search bar
            {
                txtSearch.Location = new Point(pMore.Location.X, txtSearch.Location.Y);
                txtSearch.Visible = true;
                txtSearch.BringToFront();
                txtSearch.Focus();
                pCloseSearch.Visible = true;
                pCloseSearch.BringToFront();
            }
        }

        private void pCloseSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Visible = false;
            pCloseSearch.Visible = false;
        }

        private void _noteText_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).ForeColor == SystemColors.ControlDark)
                {
                    ((TextBox)sender).ForeColor = SystemColors.WindowText;
                    ((TextBox)sender).Text = "";
                }
                if ((TextBox)sender == _noteLink)
                    _noteLink.ForeColor = SystemColors.HotTrack;
            }
            else // richTextBox
            {
                if (_noteText.ForeColor == SystemColors.ControlDark)
                {
                    _noteText.ForeColor = SystemColors.WindowText;
                    _noteText.Text = "";
                }
            }
        }

        private void _noteText_Leave(object sender, EventArgs e)
        {
            if (_noteTitle.Text == "")
            {
                _noteTitle.ForeColor = SystemColors.ControlDark;
                _noteTitle.Text = Utils.getString("notes.title.dummy");
            }
            if (_noteText.Text == "")
            {
                _noteText.ForeColor = SystemColors.ControlDark;
                _noteText.Text = Utils.getString("notes.content.dummy");
            }
            if (_noteLink.Text == "")
            {
                _noteLink.ForeColor = SystemColors.ControlDark;
                _noteLink.Text = Utils.getString("notes.link.dummy");
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { pSearch_Click(null, null); }
        }

        private void pHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OrganizerNotes.htm");
        }

        private void pMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblNotesName_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void pSort_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsSort.Items)
                item.Visible = true;

            cmsSort.Show(Cursor.Position);
        }

        private void lblNoteGroup_Click(object sender, EventArgs e)
        {
            cbGroupsBottom.Location = new Point(lblGroupBottom.Location.X + lblGroupBottom.Width, cbGroupsBottom.Location.Y);
            cbGroupsBottom.Visible = true;
            cbGroupsBottom.BringToFront();
            cbGroupsBottom.Focus();
        }

        private void cbGroupsBottom_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoteGroupItem item = cbGroupsBottom.SelectedItem as NoteGroupItem;
            lblNoteGroup.Text = item.Name;
        }

        private void cbGroupsBottom_Leave(object sender, EventArgs e)
        {
            cbGroupsBottom.Visible = false;
        }

        private void cbGroupsBottom_DropDownClosed(object sender, EventArgs e)
        {
            cbGroupsBottom.Visible = false;
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string groupName = txtNewGroup.Text.Trim();
            if (String.IsNullOrEmpty(groupName))
                return;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTEGROUPS where name=`" + groupName + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("notes.addgroup.nameexists")); return;
                }

                db.AddNoteGroup(groupName);

                // Get group id
                int id = 0;
                dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

                NoteGroupItem item = new NoteGroupItem(groupName, id);
                cbGroups.Items.Add(item);
                cbGroupsBottom.Items.Add(item);
                N_groups.Items.Add(item);
                cbNewNoteGroup.Items.Add(item);

                cbGroups.SelectedItem = item;
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            string newName = txtNewGroup.Text.Trim();
            if (String.IsNullOrEmpty(newName))
                return;

            NoteGroupItem item = cbGroups.SelectedItem as NoteGroupItem;
            string oldName = item.Name;
            int id = item.ID;

            if (newName.ToLower() == item.Name.ToLower())
                return;

            using (BubblesDB db = new BubblesDB())
                db.ExecuteNonQuery("update NOTEGROUPS set name=`" + newName + "` where id=" + item.ID + "");

            // change group name in all checkboxes
            item.Name = newName;
            cbGroups.SelectedItem = item;

            for (int i = 0; i < cbGroupsBottom.Items.Count; i++)
            {
                item = cbGroupsBottom.Items[i] as NoteGroupItem;
                if (item.ID == id)
                {
                    item.Name = newName; cbGroupsBottom.Items[i] = item;
                }
            }
            for (int i = 0; i < N_groups.Items.Count; i++)
            {
                item = N_groups.Items[i] as NoteGroupItem;
                if (item.ID == id)
                {
                    item.Name = newName; N_groups.Items[i] = item;
                }
            }
            for (int i = 0; i < cbNewNoteGroup.Items.Count; i++)
            {
                item = cbNewNoteGroup.Items[i] as NoteGroupItem;
                if (item.ID == id)
                {
                    item.Name = newName; cbNewNoteGroup.Items[i] = item;
                }
            }
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            panelDeleteGroup.Visible = true;
        }

        private void btnDeleteGroupOK_Click(object sender, EventArgs e)
        {
            if (rbtnDeleteGroupCancel.Checked)
            {
                panelDeleteGroup.Visible = false; return;
            }

            NoteGroupItem item = cbGroups.SelectedItem as NoteGroupItem;

            using (BubblesDB db = new BubblesDB())
            {
                db.ExecuteNonQuery("delete from NOTEGROUPS where id=" + item.ID + "");

                if (rbtnDeleteGroupAndNotes.Checked) // delete group and its notes
                {
                    db.ExecuteNonQuery("delete from NOTES where groupID=" + item.ID + "");
                    // delete notes from this window if All or this group is selected

                }
                else
                {
                    db.ExecuteNonQuery("update NOTES set groupID=" + 0 + " where id=" + item.ID + "");
                    // update notes ID from this window if All or this group is selected

                }
            }

            cbGroups.Items.Remove(item);
            cbGroupsBottom.Items.Remove(item);
            N_groups.Items.Remove(item);
            cbNewNoteGroup.Items.Remove(item);
            panelDeleteGroup.Visible = false;
        }

        List<Panel> selectedNotes = new List<Panel>();

        int thisWidth = 0;
        bool edit = false;
        List<NoteItem> NOTES = new List<NoteItem>();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public class NoteGroupItem
    {
        public NoteGroupItem(string name, int id)
        {
            Name = name;
            ID = id;
        }
        public string Name = "";
        public int ID = 0;

        public override string ToString() => Name;
    }

    class NoteItem
    {
        public NoteItem(string title, string content, string link,  int id, int groupID) 
        {
            Title = title;
            Content = content;
            Link = link;
            ID = id;
            GroupID = groupID;
        }

        public string Title;
        public string Content;
        public string Link = "";
        public int ID = 0;
        public int GroupID = 0;
    }

}
