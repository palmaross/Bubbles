using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class MyDateTimePicker : Form
    {
        public MyDateTimePicker()
        {
            InitializeComponent();

            toolTip1.SetToolTip(Apply, Utils.getString("taskinfo.datetimepicker.setdate"));

            this.MinimumSize = this.Size;
        }

        // Apply date to the selected topic and close this window
        private void Apply_Click(object sender, EventArgs e)
        {
            if (this.AccessibleName != null)
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                    return;

                DateTime dt = dateTimePicker1.Value.Date.AddHours(8);

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (this.AccessibleName == "calendar_startdate")
                        t.Task.StartDate = dt;
                    else
                        t.Task.DueDate = dt;
                }
            }
            this.Close();
        }

        // Enter applies date to the selected topic and closes this window
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Apply_Click(null, null);

                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            DateTime dt = dateTimePicker1.Value.Date.AddHours(8);

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (this.AccessibleName == "calendar_startdate")
                    t.Task.StartDate = dt;
                else
                    t.Task.DueDate = dt;
            }
            this.Close();
        }

        // Click outside window closes window
        private const uint WM_NCACTIVATE = 0x0086;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // if click outside dialog -> Close Dlg
            if (m.Msg == WM_NCACTIVATE) //0x86
            {
                if (this.Visible)
                {
                    if (!this.RectangleToScreen(this.DisplayRectangle).Contains(Cursor.Position))
                        this.Close();
                }
            }
        }
    }
}
