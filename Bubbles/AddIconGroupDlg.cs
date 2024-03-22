using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class AddIconGroupDlg : Form
    {
        public AddIconGroupDlg(Form form, string orientation, string name)
        {
            InitializeComponent();

            lblGroupName.Text = Utils.getString("AddIconGroupDlg.lblGroupName");
            cbMutEx.Text = Utils.getString("AddIconGroupDlg.cbMutEx");
            btnCancel.Text = Utils.getString("button.cancel");

            // Get location
            Rectangle child = this.RectangleToScreen(this.ClientRectangle);
            this.Location = StickUtils.GetChildLocation(form, child, orientation, "icons");

            txtGroupName.Text = name;
            this.Paint += This_Paint; // paint the border
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtGroupName.Text.Length > 2)
            {
                if (MapMarkers.GetIconGroup(txtGroupName.Text.Trim(), mutex: cbMutEx.Checked) != null)
                    DialogResult = DialogResult.OK;
            }
        }
    }
}
