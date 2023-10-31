using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class GetNameDlg : Form
    {
        /// <summary>New stick name or new source/icon name or rename stick/source/icon</summary>
        /// <param name="rec">Parent stick rectangle</param>
        /// <param name="orientation">stick orientation (Horizontal or Vertical)</param>
        /// <param name="type">Stick, source or icon</param>
        /// <param name="name">Name of stick/source/icon in case of rename</param>
        public GetNameDlg(Rectangle rec, string orientation, string type, string name = "")
        {
            InitializeComponent();

            label1.Text = Utils.getString("NewStickDlg.label.title") + ":";
            if (type == StickUtils.typeicons)
                label1.Text = Utils.getString("NewStickDlg.icon.title") + ":";
            if (type == StickUtils.typesources)
                label1.Text = Utils.getString("NewStickDlg.source.title") + ":";
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
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show(Utils.getString("NewStickDlg.error.text"), 
                    Utils.getString(label1.Text.Replace(':', '!')), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
