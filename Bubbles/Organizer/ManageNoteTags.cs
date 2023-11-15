using Bubbles;
//using Mindjet.MindManager.Interop;
//using Mindjet.MindManager.Interop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Organizer
{
    public partial class ManageNoteTags : UserControl
    {
        public ManageNoteTags()
        {
            InitializeComponent();

            // Manage Groups panel
            toolTip1.SetToolTip(btnDeleteTag, Utils.getString("notes.tags.delete.tooltip"));
            groupBoxAddNewTag.Text = Utils.getString("notes.tags.groupBoxAddNewTag");
            btnAddTag.Text = Utils.getString("button.add");
            groupBoxRenameTag.Text = Utils.getString("notes.tags.groupBoxRenameTag");
            btnRenameTag.Text = Utils.getString("notes.group.rename");

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTETAGS");
                foreach (DataRow dr in dt.Rows)
                    cbTags.Items.Add(dr["tag"]);
            }

            if (cbTags.Items.Count > 0)
                cbTags.SelectedIndex = 0;
        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            string tagName = txtNewTag.Text.Trim();
            if (String.IsNullOrEmpty(tagName))
                return;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTETAGS where name=`" + tagName + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("notes.addtag.nameexists")); return;
                }

                db.ExecuteNonQuery("insert into NOTETAGS values(`" + tagName + "`, '', 0);" );

                cbTags.Items.Add(tagName);
                cbTags.SelectedItem = tagName;
                txtNewTag.Text = "";

                if (!btnDeleteTag.Enabled)
                    btnDeleteTag.Enabled = true;
            }

            // Add tag in the all open Note windows
            foreach (Form note in BubblesButton.pNOTES.Values)
            {
                // Update tags combobox
                ComboBox cbTags = (ComboBox)note.Controls.Find("cbTags", true)[0];
                if (cbTags != null)
                    cbTags.Items.Add(tagName);
            }
        }

        private void btnRenameTag_Click(object sender, EventArgs e)
        {
            string newName = txtRenameTag.Text.Trim();
            if (String.IsNullOrEmpty(newName))
                return;

            string oldName = cbTags.SelectedItem.ToString();

            if (newName == oldName) return;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from NOTETAGS where tag=`" + newName + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("notes.addtag.nameexists")); return;
                }

                db.ExecuteNonQuery("update NOTETAGS set tag=`" + newName + "` where tag=`" + oldName + "`");

                // Rename tag in Notes database
                dt = db.ExecuteQuery("select * from NOTES");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tags"].ToString() != "")
                    {
                        List<string> tags = dr["tags"].ToString().Split(',').Select(p => p.Trim()).ToList();
                        if (tags.Contains(oldName))
                        {
                            tags[tags.IndexOf(oldName)] = newName;

                            string mytags = "";
                            foreach (var tag in tags) mytags += tag + ", ";
                            mytags = mytags.Trim().TrimEnd(',');

                            db.ExecuteNonQuery("update NOTES set tags=`" + mytags + 
                                "` where id=" + Convert.ToInt32(dr["id"]) + "");
                        }
                    }
                }
            }

            cbTags.Items.Remove(oldName);
            cbTags.Items.Add(newName);
            cbTags.SelectedItem = newName;

            // Rename tag in the all open Note windows
            foreach (Form note in BubblesButton.pNOTES.Values)
            {
                // Update combobox
                ComboBox cbTags = (ComboBox)note.Controls.Find("cbTags", true)[0];

                if (cbTags != null && cbTags.Items.Count > 0)
                {
                    List<string> tags = new List<string>();
                    tags.AddRange(cbTags.Items.Cast<object>().Select(item => item.ToString()).ToList());
                    if (tags.Contains(oldName))
                    {
                        tags[tags.IndexOf(oldName)] = newName;
                        cbTags.Items.Clear();
                        cbTags.Items.AddRange(tags.ToArray());
                    }
                }

                // update tags text box
                ReplaceTag(note, oldName, newName);
            }

            // Rename tag in the all displayed notes
            Control flp = this.Parent.Controls.Find("flowLayoutPanel1", false)[0]; // FlowLayoutPanel
            foreach (var note in flp.Controls.OfType<Panel>())
            {
                if (note.Name == "panelDummy") 
                    continue;

                ReplaceTag(note, oldName, newName);
            }
        }

        private void btnDeleteTag_Click(object sender, EventArgs e)
        {
            string tag = cbTags.SelectedItem.ToString();

            using (BubblesDB db = new BubblesDB())
                db.ExecuteNonQuery("delete from NOTETAGS where name=`" + tag + "`");

            // Delete tag in the all open Note windows
            foreach (Form note in BubblesButton.pNOTES.Values)
            {
                // Update tags combobox
                ComboBox cbTags = (ComboBox)note.Controls.Find("cbTags", true)[0];
                if (cbTags != null)
                    cbTags.Items.Remove(tag);

                // Update tags textbox
                ReplaceTag(note, tag, "", true);
            }

            // Delete tag in the all displayed notes
            foreach (var note in this.Parent.Controls.OfType<Panel>())
            {
                if (note.Name != "panelDummy")
                    ReplaceTag(note, tag, "", true);
            }
        }

        /// <summary>
        /// Replace tags in the tags text box
        /// </summary>
        /// <param name="note">Control where tags text box is located</param>
        private void ReplaceTag(Control note, string oldName, string newName, bool delete = false)
        {
            TextBox tbTags = (TextBox)note.Controls.Find("Tags", false)[0];

            if (tbTags == null || tbTags.Text == "") return;

            // We have a Tags textBox, search for renamed (or deleted) tag
            List<string> tags = tbTags.Text.Split(',').Select(p => p.Trim()).ToList();
            if (tags.Contains(oldName))
            {
                tbTags.ReadOnly = false;

                if (delete)
                    tags.Remove(oldName);
                else
                    tags[tags.IndexOf(oldName)] = newName;

                string mytags = "";
                foreach (var tag in tags) mytags += tag + ", ";
                mytags = mytags.Trim().TrimEnd(',');
                tbTags.Text = mytags;

                if (note is Panel)
                    tbTags.ReadOnly = true;
            }
        }

        private void cbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRenameTag.Text = cbTags.SelectedItem.ToString() + " (2)";
        }
    }
}
