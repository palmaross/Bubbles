using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class GetNameDlg : Form
    {
        /// <summary>New stick name or new source/icon name or rename stick/source/icon</summary>
        /// <param name="rec">Parent stick rectangle</param>
        /// <param name="orientation">stick orientation (Horizontal or Vertical)</param>
        /// <param name="stickType">Stick, source or icon</param>
        /// <param name="name">Name of stick/source/icon in case of rename</param>
        public GetNameDlg(Rectangle rec, string orientation, string name)
        {
            InitializeComponent();

            if (stick)
                label1.Text = Utils.getString("NewStickDlg.label.title") + ":"; // stick name
            else if (stickType == StickUtils.typeicons)
                label1.Text = Utils.getString("NewStickDlg.icon.title") + ":"; // icon name
            else if (stickType == StickUtils.typesources)
                label1.Text = Utils.getString("NewStickDlg.source.title") + ":"; // source name
            btnCancel.Text = Utils.getString("button.cancel");

            Point location = new Point(rec.X, rec.Y + rec.Height);
            if (orientation == "V")
                location = new Point(rec.X + rec.Width, rec.Y);

            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;

            if (orientation == "H")
            {
                if (location.Y + this.Height > area.Bottom)
                    location = new Point(rec.X, rec.Y - this.Height);
            }
            else if (location.X + this.Width > area.Right)
            {
                location = new Point(rec.X - this.Width, rec.Y);
            }

            this.Location = location;
            textBox1.Text = name;
            this.Paint += This_Paint; // paint the border
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string newName = textBox1.Text.Trim();

            if (String.IsNullOrEmpty(newName))
            {
                MessageBox.Show(Utils.getString("NewStickDlg.error.text"), 
                    Utils.getString(label1.Text.Replace(':', '!')), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if name exists
            using (BubblesDB db = new BubblesDB())
            {
                if (stick)
                {
                    DataTable dt = db.ExecuteQuery("SELECT from STICKS where name=`" + newName + "` and id=" + stickID + "");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("sticks.nameexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (stickType == StickUtils.typeicons)
                {
                    DataTable dt = db.ExecuteQuery("SELECT from ICONS where name=`" + newName + "` and id=" + stickID + "");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("sticks.nameexists.icon"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (stickType == StickUtils.typesources)
                {
                    DataTable dt = db.ExecuteQuery("SELECT from SOURCES where title=`" + newName + "` and id=" + stickID + "");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("sticks.nameexists.source"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            DialogResult = DialogResult.OK;
        }

        public string stickType; public int stickID; public bool stick;
    }
}
