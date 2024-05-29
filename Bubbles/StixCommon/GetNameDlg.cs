using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class GetNameDlg : Form
    {
        /// <summary>New stick name or new tool/icon name or rename stick/tool/icon</summary>
        /// <param name="rec">Parent stick rectangle</param>
        /// <param name="orientation">stick orientation (Horizontal or Vertical)</param>
        /// <param name="stickType">Stick, tool or icon</param>
        /// <param name="name">Name of stick/tool/icon in case of rename</param>
        public GetNameDlg(Form form, string orientation, string name, string type, bool aStick)
        {
            InitializeComponent();

            stick = aStick; stickType = type;

            if (stick)
                label1.Text = Utils.getString("NewStickDlg.label.title") + ":"; // stick name
            else if (stickType == StixUtils.typeicons)
                label1.Text = Utils.getString("NewStickDlg.icon.title") + ":"; // icon name
            else if (stickType == StixUtils.typetools)
                label1.Text = Utils.getString("NewStickDlg.tool.title") + ":"; // tool name
            btnCancel.Text = Utils.getString("button.cancel");

            // Get location
            Rectangle child = this.RectangleToScreen(this.ClientRectangle);
            this.Location = StixUtils.GetChildLocation(form, child, orientation, "getname");

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
            using (StixDB db = new StixDB())
            {
                if (stick)
                {
                    DataTable dt = db.ExecuteQuery("SELECT from STIX where name=`" + newName + "` and id=" + stixID + "");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("sticks.nameexists"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (stickType == StixUtils.typeicons)
                {
                    DataTable dt = db.ExecuteQuery("SELECT from ICONS where name=`" + newName + "` and id=" + stixID + "");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("sticks.nameexists.icon"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (stickType == StixUtils.typetools)
                {
                    DataTable dt = db.ExecuteQuery("SELECT from TOOLS where title=`" + newName + "` and id=" + stixID + "");
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(Utils.getString("sticks.nameexists.tool"), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            DialogResult = DialogResult.OK;
        }

        public string stickType; public int stixID; public bool stick;
    }
}
