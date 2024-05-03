using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class NavigationDlg : Form
    {
        public NavigationDlg()
        {
            InitializeComponent();

            toolTip1.SetToolTip(btnClose, Utils.getString("button.close"));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Add a dummy column
            listView1.Columns.Add("", 0, HorizontalAlignment.Left);
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.Columns[0].Width = listView1.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            ListViewItem item = listView1.Items.Add("Central Topic");
            item.Tag = "0"; item.Font = new Font(item.Font, FontStyle.Bold);

            foreach (Topic t in MMUtils.ActiveDocument.CentralTopic.SubTopics)
                listView1.Items.Add(t.Text).Tag = t.Guid;

            this.Paint += NavigationDlg_Paint; // paint the border
            this.MaximumSize = new Size(this.Width * 2, this.Height * 3);

            this.Deactivate += NavigationDlg_Deactivate;
        }

        private void NavigationDlg_Deactivate(object sender, EventArgs e)
        {
            this.Close(); this.Dispose();
        }

        public int thisHeight;

        private void NavigationDlg_Load(object sender, EventArgs e)
        {
            this.Height = thisHeight;
        }

        private void NavigationDlg_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            var selectedItem = listView1.SelectedItems[0];
            if (selectedItem != null)
            {
                try
                {
                    string guid = (string)selectedItem.Tag;
                    Topic t = MMUtils.ActiveDocument.CentralTopic;

                    if (guid != "0")
                        t = MMUtils.ActiveDocument.FindByGuid(guid) as Topic; 

                    if (t != null)
                    {
                        t.SelectOnly();
                        t.SnapIntoView();
                    }
                }
                catch { }
            }

            DialogResult = DialogResult.OK;
        }

        #region Resize window
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
        #endregion
    }
}
