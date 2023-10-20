using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class NewStickDlg : Form
    {
        public NewStickDlg(Rectangle rec, string orientation, string name = "") // Parent sticker rectangle
        {
            InitializeComponent();

            Text = Utils.getString("NewStickDlg.title");
            label1.Text = Utils.getString("NewStickDlg.label.title");
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
                    Utils.getString("NewStickDlg.error.title"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
