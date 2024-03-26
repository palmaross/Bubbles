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
using System.IO;
using Bubbles;
using System.Runtime.InteropServices;

namespace Organizer

{
    public partial class NotesDlg : Form
    {
        public NotesDlg()
        {
            init = true;
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
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

            // Rounded corners
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // FlowLayoutPanel
            toolTip1.SetToolTip(pNewNote, Utils.getString("notes.pNewNote"));

            // Context menu
            cmsMoreButton.ItemClicked += ContextMenu_ItemClicked;
            cmsSort.ItemClicked += ContextMenu_ItemClicked;

            cmsMoreButton.Items["N_delete"].Text = Utils.getString("notes.contextmenu.delete");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_delete"], "deleteall.png");

            cmsMoreButton.Items["N_deleteall"].Text = Utils.getString("notes.contextmenu.deleteall");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_deleteall"], "deleteall.png");

            cmsMoreButton.Items["N_edit"].Text = Utils.getString("notes.contextmenu.edit");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_edit"], "edit.png");

            cmsMoreButton.Items["N_addtopic"].Text = Utils.getString("notes.contextmenu.addtopic");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_addtopic"], "addtotopic.png");

            cmsMoreButton.Items["N_managegroups"].Text = Utils.getString("notes.contextmenu.managegroups");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_managegroups"], "groups.png");

            cmsMoreButton.Items["N_managetags"].Text = Utils.getString("notes.contextmenu.managetags");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_managetags"], "managetags.png");

            cmsMoreButton.Items["N_managemarkers"].Text = Utils.getString("notes.contextmenu.managemarkers");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_managemarkers"], "emptyIcon.png");

            cmsMoreButton.Items["N_help"].Text = Utils.getString("button.help");
            StickUtils.SetContextMenuImage(cmsMoreButton.Items["N_help"], "help.png");

            cmsSort.Items["N_sortAZ"].Text = Utils.getString("notes.contextmenu.sortAZ");
            cmsSort.Items["N_sortZA"].Text = Utils.getString("notes.contextmenu.sortZA");
            cmsSort.Items["N_sortgroup"].Text = Utils.getString("notes.contextmenu.sortgroup");
            cmsSort.Items["N_sortlinks"].Text = Utils.getString("notes.contextmenu.sortlinks");
            cmsSort.Items["N_sortfirst"].Text = Utils.getString("notes.contextmenu.sortbydate");

            this.MouseDown += NotesDlg_MouseDown; // to move the window
            pBack.Location = pMore.Location;

            // PreFill group comboboxes (All Groups and No Group)
            cbGroups.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.all"), -1));
            cbGroups.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.nogroup"), 0));

            // Fill group comboboxes with custom groups
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTEGROUPS order by name");
                foreach (DataRow row in dt.Rows)
                {
                    NoteGroupItem item = new NoteGroupItem(row["name"].ToString(), Convert.ToInt32(row["id"]));
                    m_manageGroups.cbGroups.Items.Add(item);
                    cbGroups.Items.Add(item);
                }
            }

            cbGroups.SelectedIndex = 0;
            if (m_manageGroups.cbGroups.Items.Count > 0) // manage groups panel (custom groups only)
                m_manageGroups.cbGroups.SelectedIndex = 0;

            FillNotes(-1);

            GetNoteIconPanels();

            init = false;
        }

        /// <summary>
        /// Fill FlowLayoutPanel with notes
        /// </summary>
        /// <param name="groupID">Note group ID</param>
        /// <param name="sort">sort parameter ("A-Z", "Z-A", "id", "group", "links")</param>
        void FillNotes(int groupID, string sort = "id")
        {
            if (NOTES.Count == 0) // first start or group changed
            {
                using (StixDB db = new StixDB())
                {
                    DataTable dt;
                    if (groupID == -1) // Notes from all groups
                        dt = db.ExecuteQuery("select * from NOTES order by id");
                    else
                        dt = db.ExecuteQuery("select * from NOTES where groupID=" + groupID + " order by id");

                    foreach (DataRow row in dt.Rows)
                    {
                        NOTES.Add(new NoteItem(row["name"].ToString(), row["content"].ToString(), 
                            row["link"].ToString(), Convert.ToInt32(row["id"]), Convert.ToInt32(row["groupID"]), 
                            row["icon1"].ToString(), row["icon2"].ToString(), row["tags"].ToString()));
                    }
                }
            }
            else // Sort notes. Use the NOTES list.
            {
                if (sort == "AZ")
                    NOTES.Sort((a, z) => a.Title.CompareTo(z.Title));
                else if (sort == "ZA")
                    NOTES.Sort((z, a) => a.Title.CompareTo(z.Title));
                else if (sort == "group")
                    NOTES.Sort((x, y) => x.GroupID.CompareTo(y.GroupID));
                else if (sort == "link")
                    NOTES.Sort((x, y) => x.Link.CompareTo(y.Link));
                else if (sort == "id")
                    NOTES.Sort((x, y) => x.ID.CompareTo(y.ID));
            }
            foreach (var note in NOTES)
                AddNote(note.Title, note.Content, note.Link, note.ID, note.GroupID, note.Icon1, note.Icon2, note.Tags);
        }

        public void GetNoteIconPanels()
        {
            // p11 is empty icon
            int locX1 = p11.Location.X + pNISize.Width, locX2 = locX1; 
            int set1 = 1, set2 = 1;
            p11.MouseClick += NoteIcon_Click; p22.MouseClick += NoteIcon_Click;
            for (int i = panelNoteIcons1.Controls.Count - 1; i > 1; i--)
                panelNoteIcons1.Controls.RemoveAt(i);
            for (int i = panelNoteIcons2.Controls.Count - 1; i > 0; i--)
                panelNoteIcons2.Controls.RemoveAt(i);

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTEICONS order by _order");  
                foreach (DataRow row in dt.Rows)
                {
                    if (row["fileName"].ToString() != "") // exlude empty icons
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
                            PictureBox pb = new PictureBox();
                            pb.Size = p11.Size;
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            pb.Visible = true;
                            pb.BringToFront();
                            pb.Image = System.Drawing.Image.FromFile(rootPath + _filename);

                            if (Convert.ToInt32(row["id"]) < 9)
                            {
                                pb.Location = new Point(locX1, p11.Location.Y);
                                locX1 += pNISize.Width;
                                panelNoteIcons1.Controls.Add(pb); set1++;
                            }
                            else
                            {
                                pb.Location = new Point(locX2, p11.Location.Y);
                                locX2 += pNISize.Width;
                                panelNoteIcons2.Controls.Add(pb); set2++;
                            }
                            toolTip1.SetToolTip(pb, name);
                            pb.Tag = name + ":" + filename;
                            pb.MouseClick += NoteIcon_Click;
                        }
                    }
                }
            }
            panelNoteIcons1.Width = (set1 * pNISize.Width);// + (pNISize.Height * 2);
            panelNoteIcons2.Width = (set2 * pNISize.Width);// + (pNISize.Height * 2);
        }

        private void NoteIcon_Click(object sender, MouseEventArgs e)
        {
            NoteIcon.Image = ((PictureBox)sender).Image;
            panelNoteIcons1.Visible = false;
            panelNoteIcons2.Visible = false;

            int noteID = ((NoteItem)NoteIcon.Parent.Tag).ID;

            if (((PictureBox)sender).Tag == null) // empty icon clicked
            {
                using (StixDB db = new StixDB())
                    db.ExecuteNonQuery("update NOTES set " + NoteIcon.Tag.ToString() + "='' where id=" + noteID + "");
            }
            else // icon changed
            {
                string icon = ((PictureBox)sender).Tag.ToString();
                string[] parts = icon.Split(':');
                toolTip1.SetToolTip(NoteIcon, parts[0]);

                using (StixDB db = new StixDB())
                    db.ExecuteNonQuery("update NOTES set " + NoteIcon.Tag.ToString() + "=`" + icon +
                        "` where id=" + noteID + "");
            }
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "N_managegroups")
            {
                m_manageGroups.Left = (this.ClientSize.Width - m_manageGroups.Width) / 2;
                m_manageGroups.Top = (this.ClientSize.Height - m_manageGroups.Height) / 2;
                this.Controls.Add(m_manageGroups);
                m_manageGroups.BringToFront();

                pBack.Visible = true;
                txtSearch.Visible = false;
                //pNewNote.SendToBack();

                if (m_manageGroups.cbGroups.Items.Count > 0)
                    m_manageGroups.cbGroups.SelectedIndex = 0;
                else
                    m_manageGroups.btnDeleteGroup.Enabled = false;
            }
            if (e.ClickedItem.Name == "N_managetags")
            {
                m_manageTags.Left = (this.ClientSize.Width - m_manageTags.Width) / 2;
                m_manageTags.Top = (this.ClientSize.Height - m_manageTags.Height) / 2;
                this.Controls.Add(m_manageTags);
                m_manageTags.BringToFront();

                pBack.Visible = true;
                txtSearch.Visible = false;
                //pNewNote.SendToBack();

                //if (m_manageGroups.cbGroups.Items.Count > 0)
                //    m_manageGroups.cbGroups.SelectedIndex = 0;
                //else
                //    m_manageGroups.btnDeleteGroup.Enabled = false;
            }
            if (e.ClickedItem.Name == "N_managemarkers")
            {
                m_manageMarkers.Left = (this.ClientSize.Width - m_manageMarkers.Width) / 2;
                m_manageMarkers.Top = (this.ClientSize.Height - m_manageMarkers.Height) / 2;
                this.Controls.Add(m_manageMarkers);
                m_manageMarkers.BringToFront();

                pBack.Visible = true;
                txtSearch.Visible = false;
                //pNewNote.SendToBack();

                //if (m_manageGroups.cbGroups.Items.Count > 0)
                //    m_manageGroups.cbGroups.SelectedIndex = 0;
                //else
                //    m_manageGroups.btnDeleteGroup.Enabled = false;
            }
            else if (e.ClickedItem.Name == "N_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OrganizerNotes.htm");
            }
        }

        public Panel AddNote(string aNoteTitle, string aNoteContent, string aNoteLink, int noteID = 0, 
            int noteGroupID = 0, string icon1 = "", string icon2 = "", string aNoteTags = "")
        {
            Panel note = new Panel();
            note.Size = new Size(flowLayoutPanel1.Width, panelDummy.Height);
            note.BorderStyle = BorderStyle.FixedSingle;
            note.Margin = panelDummy.Margin;
            note.BackColor = notePanelColor;

            TextBox noteTitle = new TextBox();
            noteTitle.Name = "Title";
            noteTitle.Font = dummyTitle.Font;
            noteTitle.Text = aNoteTitle;
            noteTitle.Location = dummyTitle.Location;
            FillTextBox(note, noteTitle, noteTitle.Name);

            TextBox noteText = new TextBox();
            noteText.Name = "Content";
            noteText.Font = dummyText.Font;
            noteText.Multiline = true;
            noteText.Location = dummyText.Location;
            noteText.Text = Regex.Replace(aNoteContent, "(?<!\r)\n", "\r\n"); // text box doesn't make a new line with "\n", but yes do with "\r\n" 
            FillTextBox(note, noteText, noteText.Name);

            TextBox noteLink = new TextBox();
            noteLink.Name = "Link";
            noteLink.Font = dummyLink.Font;
            noteLink.Text = aNoteLink;
            noteLink.ForeColor = dummyLink.ForeColor;
            noteLink.BackColor = noteText.BackColor; // if a textbox is read-only this is a trick to trigger it's forecolor!
            FillTextBox(note, noteLink, noteLink.Name);

            if (String.IsNullOrEmpty(aNoteTags) && !String.IsNullOrEmpty(aNoteLink))
                noteLink.Location = dummyTags.Location;
            else
                noteLink.Location = dummyLink.Location;
            if (String.IsNullOrEmpty(aNoteLink))
                noteLink.SendToBack();
            else
            {
                noteLink.BringToFront();
                noteLink.DoubleClick += NoteLink_DoubleClick;
                noteLink.Refresh();
                noteLink.Cursor = Cursors.Hand;
            }

            TextBox noteTags = new TextBox();
            noteTags.Name = "Tags";
            noteTags.Font = dummyTags.Font;
            noteTags.Text = aNoteTags;
            noteTags.Location = dummyTags.Location;
            FillTextBox(note, noteTags, noteTags.Name);
            if (String.IsNullOrEmpty(aNoteTags))
                noteTags.SendToBack();
            else
                noteTags.BringToFront();

            PictureBox pIcon1 = new PictureBox();
            pIcon1.Location = dummyIcon1.Location;
            pIcon1.Tag = "icon1";
            FillPictureBox(note, pIcon1, icon1);

            PictureBox pIcon2 = new PictureBox();
            pIcon2.Location = dummyIcon2.Location;
            pIcon2.Tag = "icon2";
            FillPictureBox(note, pIcon2, icon2);

            if (noteID != 0) // 0 -> new note, Tag wil be added later
                note.Tag = new NoteItem(aNoteTitle, aNoteContent, aNoteLink, noteID, noteGroupID, icon1, icon2, aNoteTags);

            flowLayoutPanel1.Controls.Add(note);
            flowLayoutPanel1.SetFlowBreak(note, true);

            return note;
        }

        private void FillTextBox(Panel note, TextBox control, string name)
        {
            control.Size = dummyTitle.Size;
            if (name == "Content")
                control.Size = dummyText.Size;
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            control.BorderStyle = BorderStyle.None;
            control.ReadOnly = true;
            control.MouseDown += Note_MouseDown;
            if (name != "Link")
                control.MouseDoubleClick += Note_MouseDoubleClick;
            control.KeyUp += Note_KeyUp;
            control.ContextMenuStrip = cmsMoreButton;
            note.Controls.Add(control);
        }

        private void FillPictureBox(Panel note, PictureBox pb, string icon)
        {
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Size = dummyIcon1.Size;
            pb.Cursor = Cursors.Hand;
            note.Controls.Add(pb);
            pb.Image = dummyIcon1.Image;
            pb.BackColor = notePanelColor;
            pb.Click += NoteIcon_Click;

            if (icon != "")
            {
                string[] parts = icon.Split(':');
                string _filename = parts[1];
                toolTip1.SetToolTip(pb, parts[0]);

                string rootPath = Utils.m_dataPath + "IconDB\\";

                if (_filename.StartsWith("stock"))
                {
                    rootPath = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
                    _filename = _filename.Substring(5) + ".ico"; // stockemail -> email.ico
                }

                string iconFile = rootPath + _filename;
                if (File.Exists(iconFile))
                    pb.Image = System.Drawing.Image.FromFile(iconFile);
            }
        }

        private void NoteLink_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(((TextBox)sender).Text);
            }
            catch { }
        }

        private void NoteIcon_Click(object sender, EventArgs e)
        {
            NoteIcon = (PictureBox)sender;
            Panel panelNoteIcons = panelNoteIcons1;

            if ((string)NoteIcon.Tag == "icon1")
            {
                panelNoteIcons2.Visible = false;
                if (panelNoteIcons1.Visible)
                {
                    panelNoteIcons1.Visible = false;
                    return;
                }
            }
            else
            {
                panelNoteIcons1.Visible = false;
                if (panelNoteIcons2.Visible)
                {
                    panelNoteIcons2.Visible = false;
                    return;
                }
                panelNoteIcons = panelNoteIcons2;
            }

            NoteIcon.Parent.Controls.Add(panelNoteIcons);
            panelNoteIcons.Visible = true;
            panelNoteIcons.BringToFront();
            panelNoteIcons.Location = new Point(dummyTitle.Location.X, NoteIcon.Location.Y);
        }
        PictureBox NoteIcon = null;

        /// <summary>
        /// Delete key deletes note
        /// </summary>
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
            using (StixDB db = new StixDB())
            {
                foreach (int id in ids)
                    db.ExecuteNonQuery("delete from NOTES where id=" + id + "");
            }
        }

        /// <summary>
        /// Go to edit note
        /// </summary>
        private void Note_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Panel note = (sender as TextBox).Parent as Panel;
            NoteItem item = note.Tag as NoteItem;

            // Check if note window is open already
            if (BubblesButton.pNOTES.Keys.Contains(item.ID))
            {
                BubblesButton.pNOTES[item.ID].WindowState = FormWindowState.Normal;
                return;
            }

            NoteDlg dlg = new NoteDlg(note, item.GroupID, true);

            dlg.Text = item.Title;
            dlg.txtNoteTitle.Text = item.Title;
            dlg.txtContent.Text = item.Content;
            dlg.Tags.Text = item.Tags;
            dlg.txtLink.Text = item.Link;
            if (item.Link == "")
            {
                dlg.txtLink.Text = Utils.getString("notes.link.dummy");
                dlg.txtLink.ForeColor = SystemColors.ControlDark;
            }

            dlg.StartPosition = FormStartPosition.Manual;
            selectedNotes.Clear();
            dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            // Set dlg location AFTER show form! Very usefull if this form was moved on another screen!
            dlg.Location = new Point(this.Location.X + this.Width, this.Location.Y);
            // If the form is close to the right or bottom screen side..
            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;
            if (dlg.Location.X + dlg.Width > area.Right)
                dlg.Location = new Point(this.Location.X - dlg.Width, this.Location.Y);
        }

        /// <summary>
        /// Click on the note (note title textbox or note content textbox).
        /// Select note or show context menu
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
                        _control.BackColor = notePanelColor;
                }
                selectedNotes.Clear();
            }

            
            if (selectedNotes.Contains(notePanel))
            {
                // Deselect note Panel
                foreach (Control control in notePanel.Controls)
                    control.BackColor = notePanelColor;

                selectedNotes.Remove(notePanel);
            }
            else
            {
                // Highlight textboxes in the selected note Panel
                foreach (Control control in notePanel.Controls)
                {
                    if (!(control is PictureBox))
                        control.BackColor = Color.LightSkyBlue;
                }

                selectedNotes.Add(notePanel);
            }
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
            NoteDlg dlg = new NoteDlg(null);

            dlg.Text = Utils.getString("notes.pNewNote");
            dlg.txtNoteTitle.Text = Utils.getString("notes.title.dummy");
            dlg.txtNoteTitle.ForeColor = SystemColors.ControlDark;
            dlg.txtContent.Text = Utils.getString("notes.content.dummy");
            dlg.txtContent.ForeColor = SystemColors.ControlDark;
            dlg.txtLink.Text = Utils.getString("notes.link.dummy");
            dlg.txtLink.ForeColor = SystemColors.ControlDark;

            dlg.StartPosition = FormStartPosition.Manual;
            dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            dlg.Location = new Point(this.Location.X + this.Width, this.Location.Y);
            // If the form is close to the right or bottom screen side..
            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;
            if (dlg.Location.X + dlg.Width > area.Right)
                dlg.Location = new Point(this.Location.X - dlg.Width, this.Location.Y);
        }

        private void NotesDlg_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pBack_Click(object sender, EventArgs e)
        {
            if (this.Controls.Contains(m_manageMarkers))
            {
                GetNoteIconPanels();
                this.Controls.Remove(m_manageMarkers);
            }
            m_manageGroups.SendToBack();
            m_manageTags.SendToBack();

            pBack.Visible = false;
            pNewNote.BringToFront();
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
                txtSearch.Location = p2.Location;
                txtSearch.Visible = true;
                txtSearch.BringToFront();
                txtSearch.Focus();
                pCloseSearch.Visible = true;
                cbGroups.Visible = false;
                pCloseSearch.BringToFront();
            }
        }

        private void pCloseSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Visible = false;
            pCloseSearch.Visible = false;
            cbGroups.Visible = true;
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

        private void cbGroupsBottom_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoteGroupItem item = cbGroups.SelectedItem as NoteGroupItem;
        }

        List<Panel> selectedNotes = new List<Panel>();

        /// <summary>NOTES list (all groups or specific group)</summary>
        List<NoteItem> NOTES = new List<NoteItem>();

        ManageNoteGroups m_manageGroups = new ManageNoteGroups();
        ManageNoteTags m_manageTags = new ManageNoteTags();
        ManageNoteMarkers m_manageMarkers = new ManageNoteMarkers();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
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

        // Resize form
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

        bool init = true;
        Color notePanelColor = SystemColors.Control;

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            if (init)
                return;

            int scroll = 0;
            int cHeight = panelDummy.Height;
            int margin = panelDummy.Margin.Top;
            int count = flowLayoutPanel1.Controls.Count - 1; // minus dummyPanel

            if (flowLayoutPanel1.Height < ((cHeight + margin) * count))
                scroll = p3.Width; // scroll bar

            foreach (Control ct in flowLayoutPanel1.Controls)
            {
                ct.Width = flowLayoutPanel1.Width - scroll - margin - 1;
            }
        }

        private void cbGroups_DrawItem(object sender, DrawItemEventArgs e)
        {
            var combo = sender as ComboBox;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), e.Bounds);
                e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
                    new SolidBrush(Color.White), new Point(e.Bounds.X, e.Bounds.Y));
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
                    new SolidBrush(Color.Black), new Point(e.Bounds.X, e.Bounds.Y));
            }

            
        }
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
        public NoteItem(string title, string content, string link, int id, int groupID, 
            string icon1, string icon2, string tags) 
        {
            Title = title;
            Content = content;
            Link = link;
            ID = id;
            GroupID = groupID;
            Icon1 = icon1;
            Icon2 = icon2;
            Tags = tags;
        }

        public string Title;
        public string Content;
        public string Link = "";
        public int ID = 0;
        public int GroupID = 0;
        public string Icon1 = "";
        public string Icon2 = "";
        public string Tags = "";
    }
}
