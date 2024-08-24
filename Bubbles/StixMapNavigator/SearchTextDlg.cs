using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class SearchTextDlg : Form
    {
        public SearchTextDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "SearchTopics.htm");

            lblTitle.Text = Utils.getString("SearchTextDlg.title");
            rbtnContains.Text = Utils.getString("SearchTextDlg.rbtnContains");
            rbtnStartsWith.Text = Utils.getString("SearchTextDlg.rbtnStartsWith");
            pClose.Text = Utils.getString("button.close");

            panelHead.BackColor = Utils.header;

            this.Paint += This_Paint; // paint the border

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.MouseDown += SearchTextDlg_MouseDown;
            panelHead.MouseDown += SearchTextDlg_MouseDown;

            if (Utils.scalingFactor == 1)
            {
                listTopics.Width -= 1; listTopics.Height -= 1;
            }
        }

        private void pHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "SearchTopics.htm");
        }

        private void SearchTextDlg_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listTopics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.ActiveDocumentOrSelectionNull(false))
            {
                listTopics.Items.Clear(); return;
            }

            TopicItem item = listTopics.SelectedItem as TopicItem;

            Topic t = MMUtils.ActiveDocument.FindByGuid(item.TopicGuid) as Topic;
            if (t != null)
            {
                t.SelectOnly(); t.SnapIntoView(); StixUtils.ActivateMindManager();
            }
        }

        public void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listTopics.Items.Clear();

            if (MMUtils.ActiveDocument == null) return;

            if (txtSearch.Text.Trim().Length > 1)
            {
                string search = txtSearch.Text.ToLower();

                foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
                {
                    if (rbtnContains.Checked)
                    {
                        if (t.Text.ToLower().Contains(search))
                            listTopics.Items.Add(new TopicItem(t.Text, t.Guid));
                    }
                    else if (rbtnStartsWith.Checked)
                    {
                        if (t.Text.ToLower().StartsWith(search))
                            listTopics.Items.Add(new TopicItem(t.Text, t.Guid));
                    }
                }
            }
        }

        #region resize dialog
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

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    internal class TopicItem
    {
        public TopicItem(string topicName, string topicGuid)
        {
            TopicName = topicName;
            TopicGuid = topicGuid;
        }
        public string TopicName = "";
        public string TopicGuid = "";

        public override string ToString()
        {
            return TopicName;
        }
    }
}
