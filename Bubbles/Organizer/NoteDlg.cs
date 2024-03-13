using Bubbles;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Organizer
{
    public partial class NoteDlg : Form
    {
        public NoteDlg(Panel _note, int groupID = 0, bool _edit = false)
        {
            InitializeComponent();

            edit = _edit;
            note = _note;

            int id = new Random().Next(); // id for new note
            if (note != null)
            {
                NoteItem item = note.Tag as NoteItem;
                id = item.ID;
            }

            this.Tag = id;
            BubblesButton.pNOTES.Add(id, this);

            lblGroup.Text = Utils.getString("notes.lblGroup");
            btnSave.Text = Utils.getString("button.save");
            btnCancel.Text = Utils.getString("button.close");

            cbGroups.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.nogroup"), 0));

            int selectedIndex = 0, i = 1;
            // Fill comboboxes (groups, tags)
            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTEGROUPS order by name");
                foreach (DataRow row in dt.Rows)
                {
                    NoteGroupItem item = new NoteGroupItem(row["name"].ToString(), Convert.ToInt32(row["id"]));
                    cbGroups.Items.Add(item);
                    if (item.ID == groupID)
                        selectedIndex = i;
                    i++;
                }

                dt = db.ExecuteQuery("select * from NOTETAGS order by tag");
                foreach (DataRow row in dt.Rows)
                    cbTags.Items.Add(row["tag"]);
            }

            cbGroups.SelectedIndex = selectedIndex;
            if (cbTags.Items.Count > 0) cbTags.SelectedIndex = 0;

            AddContextMenu(txtContent); // richTextBox context menu
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNoteTitle.ForeColor == SystemColors.ControlDark || txtNoteTitle.ForeColor == SystemColors.ControlDark)
                return; // Note must have Title and Content


            // Check for new tags
            string _tags = Tags.Text.Trim().TrimEnd(',');
            if (String.IsNullOrEmpty(_tags)) return;

            // Get tags from tags textbox
            List<string> __tags = _tags.Split(',').Select(p => p.Trim()).ToList();

            List<string> newTags = new List<string>();
            foreach (string tag in __tags)
            {
                if (!cbTags.Items.Contains(tag))
                    newTags.Add(tag);
            }

            if (newTags.Count > 0) // User added new tag(s)
            {
                string _newTags = "";
                foreach (string tag in newTags)
                    _newTags += tag + ", ";
                _newTags = _newTags.Trim().TrimEnd(',');

                if (MessageBox.Show(Utils.getString("notes.newtags.askuser"),
                    String.Format(Utils.getString("notes.newtags.askuser.title"), _newTags),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Add new tags to the tag database
                    using (SticksDB db = new SticksDB())
                    {
                        foreach (string tag in newTags)
                            db.ExecuteNonQuery("insert into NOTETAGS values(`" + tag + "`, '', 0);");
                    }

                    // Add new tags to the all open Note windows (including this window)
                    foreach (Form note in BubblesButton.pNOTES.Values)
                    {
                        foreach (string tag in newTags)
                        {
                            // Update tags combobox
                            ComboBox _cbTags = (ComboBox)note.Controls.Find("cbTags", false)[0];
                            if (_cbTags != null)
                                _cbTags.Items.Add(tag);
                        }
                    }
                }
            }

            if (edit) // note has been edited
            {
                TextBox tbTitle = (TextBox)note.Controls.Find("Title", false)[0];
                TextBox tbContent = (TextBox)note.Controls.Find("Content", false)[0];
                TextBox tbLink = (TextBox)note.Controls.Find("Link", false)[0];
                TextBox tbTags = (TextBox)note.Controls.Find("Tags", false)[0];
                tbLink.Location = BubblesButton.m_Notes.dummyLink.Location;
                tbTags.Location = BubblesButton.m_Notes.dummyTags.Location;

                tbTitle.Text = txtNoteTitle.Text.Trim();

                // text box doesn't make a new line with "\n", but yes do with "\r\n" 
                tbContent.Text = Regex.Replace(txtContent.Text.Trim(), "(?<!\r)\n", "\r\n");

                tbLink.Text = txtLink.Text.Trim();
                if (String.IsNullOrEmpty(txtLink.Text.Trim()) || txtLink.ForeColor == SystemColors.ControlDark)
                {
                    tbLink.Text = "";
                    tbLink.SendToBack();
                }
                else
                {
                    if (Tags.Text.Trim() == "")
                        tbLink.Location = BubblesButton.m_Notes.dummyTags.Location;
                    else
                        tbLink.Location = BubblesButton.m_Notes.dummyLink.Location;
                }

                tbTags.Text = Tags.Text.Trim(); tbTags.BringToFront();
                if (String.IsNullOrEmpty(Tags.Text.Trim()))
                {
                    tbTags.Text = "";
                    tbTags.SendToBack();
                }

                var noteTag = note.Tag as NoteItem;
                int id = noteTag.ID;

                var group = cbGroups.SelectedItem as NoteGroupItem;

                using (SticksDB db = new SticksDB())
                    db.ExecuteNonQuery("update NOTES set " +
                        "name=`" + tbTitle.Text + "`, " +
                        "content=`" + tbContent.Text + "`, " +
                        "link=`" + tbLink.Text + "`, " +
                        "tags=`" + _tags + "`, " +
                        "groupID=" + group.ID + 
                        " where id=" + id + "");

                note.Tag = new NoteItem(tbTitle.Text, tbContent.Text, tbLink.Text, id, group.ID,
                    noteTag.Icon1, noteTag.Icon2, tbTags.Text);
            }
            else // new note
            {
                int id = 0;
                string link = "";
                if (!String.IsNullOrEmpty(txtLink.Text.Trim()) && txtLink.ForeColor == SystemColors.HotTrack)
                    link = txtLink.Text.Trim();

                var group = cbGroups.SelectedItem as NoteGroupItem;

                // Save note to database
                using (SticksMoreDB db = new SticksMoreDB())
                {
                    db.AddNote(txtNoteTitle.Text.Trim(), txtContent.Text.Trim(), link, group.ID, "", "", _tags);

                    // Get created note id
                    DataTable dt = db.ExecuteQuery("SELECT last_insert_rowid()");
                    if (dt.Rows.Count > 0) 
                        id = Convert.ToInt32(dt.Rows[0][0]);
                }

                edit = true;
                Text = txtNoteTitle.Text.Trim();

                // this ID changed, update the form in the pNOTES list
                BubblesButton.pNOTES.Remove((int)this.Tag);
                this.Tag = id;
                BubblesButton.pNOTES.Add(id, this);

                NoteGroupItem item = BubblesButton.m_Notes.cbGroups.SelectedItem as NoteGroupItem;
                if (item.ID == -1 || group.ID == item.ID) // "All groups" ot this group shown in the Notes window
                {
                    Panel newnote = BubblesButton.m_Notes.AddNote(
                    txtNoteTitle.Text.Trim(), txtContent.Text.Trim(), link, aNoteTags: _tags);
                    newnote.Tag = new NoteItem(txtNoteTitle.Text.Trim(), txtContent.Text.Trim(), link, id, group.ID, "", "", _tags);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BubblesButton.pNOTES.Remove((int)this.Tag);
            this.Close();
        }

        private void NoteDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            BubblesButton.pNOTES.Remove((int)this.Tag);
        }

        public void AddContextMenu(RichTextBox rtb)
        {
            if (rtb.ContextMenuStrip == null)
            {
                ContextMenuStrip cms = new ContextMenuStrip() {
                    ShowImageMargin = false };

                ToolStripMenuItem tsmiCut = new ToolStripMenuItem(Utils.getString("button.cut"));
                tsmiCut.Click += (sender, e) => rtb.Cut();
                cms.Items.Add(tsmiCut);

                ToolStripMenuItem tsmiCopy = new ToolStripMenuItem(Utils.getString("button.copy"));
                tsmiCopy.Click += (sender, e) => rtb.Copy();
                cms.Items.Add(tsmiCopy);

                ToolStripMenuItem tsmiPaste = new ToolStripMenuItem(Utils.getString("button.paste"));
                tsmiPaste.Click += (sender, e) => rtb.Paste();
                cms.Items.Add(tsmiPaste);

                cms.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem tsmiSelectAll = new ToolStripMenuItem(Utils.getString("button.selectall"));
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

        private void txtNote_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).ForeColor == SystemColors.ControlDark)
                {
                    ((TextBox)sender).ForeColor = SystemColors.WindowText;
                    ((TextBox)sender).Text = "";
                }
                if ((TextBox)sender == txtLink)
                    txtLink.ForeColor = SystemColors.HotTrack;
            }
            else // richTextBox
            {
                if (txtContent.ForeColor == SystemColors.ControlDark)
                {
                    txtContent.ForeColor = SystemColors.WindowText;
                    txtContent.Text = "";
                }
            }
        }

        private void txtNote_Leave(object sender, EventArgs e)
        {
            if (txtNoteTitle.Text == "")
            {
                txtNoteTitle.ForeColor = SystemColors.ControlDark;
                txtNoteTitle.Text = Utils.getString("notes.title.dummy");
            }
            if (txtContent.Text == "")
            {
                txtContent.ForeColor = SystemColors.ControlDark;
                txtContent.Text = Utils.getString("notes.content.dummy");
            }
            if (txtLink.Text == "")
            {
                txtLink.ForeColor = SystemColors.ControlDark;
                txtLink.Text = Utils.getString("notes.link.dummy");
            }
        }

        private void txtContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void txtLink_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtLink.Text.Trim());
        }

        private void lblGroupName_Click(object sender, EventArgs e)
        {
            cbGroups.Visible = true;
            cbGroups.Focus();
        }

        private void cbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoteGroupItem item = cbGroups.SelectedItem as NoteGroupItem;
        }

        /// <summary>
        /// Add selected tag to the tags textbox
        /// </summary>
        private void cbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (start)
            {
                start = false; return;
            }

            string _tags = Tags.Text.Trim().TrimEnd(',');

            if (String.IsNullOrEmpty(_tags))
            {
                Tags.Text = cbTags.SelectedItem.ToString(); return;
            }

            List<string> tags = _tags.Split(',').Select(p => p.Trim()).ToList();
            if (tags.Contains(cbTags.SelectedItem.ToString()))
                return;

            _tags += ", " + cbTags.SelectedItem.ToString();
            Tags.Text = _tags;
        }

        bool edit = false;
        bool start = true;
        Panel note;
    }
}
