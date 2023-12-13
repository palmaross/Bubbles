using Bubbles;
using System;
using System.Data;
using System.Windows.Forms;
using Bubbles;

namespace Organizer
{
    public partial class ManageNoteGroups : UserControl
    {
        public ManageNoteGroups()
        {
            InitializeComponent();

            // Manage Groups panel
            toolTip1.SetToolTip(btnDeleteGroup, Utils.getString("notes.group.delete.tooltip"));
            groupBoxAddNewGroup.Text = Utils.getString("notes.group.groupBoxNewGroup");
            btnAddGroup.Text = Utils.getString("button.add");
            groupBoxRenameGroup.Text = Utils.getString("notes.group.groupBoxRenameGroup");
            btnRenameGroup.Text = Utils.getString("button.rename");

            // Delete Group panel
            rbtnDeleteGroupAndNotes.Text = Utils.getString("notes.group.rbtnDeleteGroupAndNotes");
            rbtnDeleteGroupNoNotes.Text = Utils.getString("notes.group.rbtnDeleteGroupNoNotes");
            rbtnDeleteGroupCancel.Text = Utils.getString("notes.group.rbtnDeleteGroupCancel");
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
                BubblesButton.m_Notes.cbGroups.Items.Add(item);

                cbGroups.SelectedItem = item;
                if (!btnDeleteGroup.Enabled)
                    btnDeleteGroup.Enabled = true;

                UpdateMainNoteGroups();
            }
        }

        private void btnRenameGroup_Click(object sender, EventArgs e)
        {
            string newName = txtRenameGroup.Text.Trim();
            if (String.IsNullOrEmpty(newName))
                return;

            NoteGroupItem item = cbGroups.SelectedItem as NoteGroupItem;

            if (newName.ToLower() == item.Name.ToLower())
                return;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTEGROUPS where name=`" + newName + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("notes.addgroup.nameexists")); return;
                }
                db.ExecuteNonQuery("update NOTEGROUPS set name=`" + newName + "` where id=" + item.ID + "");
            }

            cbGroups.Items.Remove(item);
            item.Name = newName;
            cbGroups.Items.Add(item);
            cbGroups.SelectedItem = item;
            UpdateMainNoteGroups();
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
                    // todo! delete notes from this window if "All groups" or this group is selected

                }
                else
                {
                    db.ExecuteNonQuery("update NOTES set groupID=" + 0 + " where id=" + item.ID + "");
                    // todo! update notes ID from this window if "All groups" or this group is selected

                }
            }

            cbGroups.Items.Remove(item);
            panelDeleteGroup.Visible = false;

            if (cbGroups.Items.Count == 0)
                btnDeleteGroup.Enabled = false;

            UpdateMainNoteGroups();
        }

        private void cbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRenameGroup.Text = cbGroups.SelectedItem.ToString() + " (2)";
        }

        private void UpdateMainNoteGroups()
        {
            int selIndex = BubblesButton.m_Notes.cbGroups.SelectedIndex;
            BubblesButton.m_Notes.cbGroups.Items.Clear();

            // PreFill group comboboxes (All Groups and No Group)
            BubblesButton.m_Notes.cbGroups.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.all"), -1));
            BubblesButton.m_Notes.cbGroups.Items.Add(new NoteGroupItem(Utils.getString("notes.notegroup.nogroup"), 0));

            // Fill group comboboxes with custom groups
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTEGROUPS order by name");
                foreach (DataRow row in dt.Rows)
                {
                    NoteGroupItem item = new NoteGroupItem(row["name"].ToString(), Convert.ToInt32(row["id"]));
                    BubblesButton.m_Notes.cbGroups.Items.Add(item);
                }
            }
            BubblesButton.m_Notes.cbGroups.SelectedIndex = selIndex;
        }
    }
}
