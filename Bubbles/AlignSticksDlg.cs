using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bubbles
{
    public partial class AlignSticksDlg : Form
    {
        public AlignSticksDlg()
        {
            InitializeComponent();

            cbSelectAll.Text = Utils.getString("SettingsDlg.cbSelectAll");

            //add single column; -2 => autosize
            listSticks.Columns.Add("MyColumn", -2, HorizontalAlignment.Left);
            listSticks.FullRowSelect = true;
            listSticks.GridLines = true;

            // Fill stick list
            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKS order by type, name");
                foreach (DataRow dr in dt.Rows)
                {
                    if (BubblesButton.STICKS.ContainsKey(Convert.ToInt32(dr["id"])))
                        listSticks.Items.Add(dr["name"].ToString()).Tag = dr["id"].ToString();
                }
            }
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll.Checked)
            {
                foreach (ListViewItem item in listSticks.Items)
                    item.Checked = true;
            }
            else
            {
                foreach (ListViewItem item in listSticks.Items)
                    item.Checked = false;
            }
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            Point loc = new Point(0, 0);
            int prevStickWidth = 0, prevStickHeight = 0;
            int thickness = StickUtils.stickThickness;
            int gap = p1.Height;
            bool first = true;

            foreach (ListViewItem item in listSticks.CheckedItems)
            {
                int id = Convert.ToInt32(item.Tag);
                var stick = BubblesButton.STICKS[id];

                if (rbtnHH.Checked)
                {
                    ExpandStick(stick); // stick must be expanded!
                    if (stick.Height > stick.Width) // Vertical stick
                        RotateStick(stick); // stick must be Horizontal!

                    if (first) // the first stick by which we have to align the rest
                    {
                        first = false;
                        loc = stick.Location;
                    }
                    else
                        loc = new Point(loc.X + prevStickWidth + gap, loc.Y);
                    prevStickWidth = stick.Width;
                }
                else if (rbtnVV.Checked) // stick must be expanded!
                {
                    ExpandStick(stick); // stick must be expanded!
                    if (stick.Width > stick.Height) // Horizontal stick
                        RotateStick(stick); // stick must be Vertical!

                    if (first) // the first stick by which we have to align the rest
                    {
                        first = false;
                        loc = stick.Location;
                    }
                    else
                        loc = new Point(loc.X, loc.Y + prevStickHeight + gap);
                    prevStickHeight = stick.Height;
                    
                }
                else if (rbtnHV.Checked)
                {
                    if (stick.Height > stick.Width) // Vertical stick
                        RotateStick(stick); // stick must be Horizontal!

                    if (first) // the first stick by which we have to align the rest
                    {
                        first = false;
                        loc = stick.Location;
                    }
                    else
                        loc = new Point(loc.X, loc.Y + thickness + gap);
                }
                else if (rbtnVH.Checked)
                {
                    if (stick.Width > stick.Height) // Horizontal stick
                        RotateStick(stick); // stick must be Vertical!

                    if (first) // the first stick by which we have to align the rest
                    {
                        first = false;
                        loc = stick.Location;
                    }
                    else
                        loc = new Point(loc.X + thickness + gap, loc.Y);
                }
                stick.Location = loc;
            }
        }

        void RotateStick(Form stick)
        {
            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Rotate();
                    break;
                case StickUtils.typepripro:
                    (stick as BubblePriPro).Rotate();
                    break;
                case StickUtils.typeformat:
                    (stick as BubbleFormat).Rotate();
                    break;
                case StickUtils.typesources:
                    (stick as BubbleMySources).Rotate();
                    break;
                case StickUtils.typebookmarks:
                    (stick as BubbleBookmarks).Rotate();
                    break;
                case StickUtils.typepaste:
                    (stick as BubblePaste).Rotate();
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Rotate();
                    break;
            }
        }

        void ExpandStick(Form stick)
        {
            switch (stick.Name)
            {
                case StickUtils.typeicons:
                    (stick as BubbleIcons).Collapse(false, true);
                    break;
                case StickUtils.typepripro:
                    (stick as BubblePriPro).Collapse(false, true);
                    break;
                case StickUtils.typeformat:
                    (stick as BubbleFormat).Collapse(false, true);
                    break;
                case StickUtils.typesources:
                    (stick as BubbleMySources).Collapse(false, true);
                    break;
                case StickUtils.typebookmarks:
                    (stick as BubbleBookmarks).Collapse(false, true);
                    break;
                case StickUtils.typepaste:
                    (stick as BubblePaste).Collapse(false, true);
                    break;
                case StickUtils.typeorganizer:
                    (stick as BubbleOrganizer).Collapse(false, true);
                    break;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveItem(1);
        }

        public void MoveItem(int direction)
        {
            // Checking selected item
            if (listSticks.SelectedItems == null || listSticks.SelectedItems.Count <= 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = listSticks.SelectedIndices[0] + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= listSticks.Items.Count)
                return; // Index out of range - nothing to do

            ListViewItem selected = listSticks.SelectedItems[0];

            // Removing removable element
            listSticks.Items.Remove(selected);
            // Insert it in new position
            listSticks.Items.Insert(newIndex, selected);
            // Restore selection
            listSticks.Items[newIndex].Selected = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
