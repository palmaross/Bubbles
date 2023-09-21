using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bubbles
{
    internal partial class BubbleSnippets : Form
    {
        public BubbleSnippets()
        {
            InitializeComponent();

            this.MinimumSize = new Size((int)(this.Width / 2), this.Height / 2);
            this.MaximumSize = new Size(this.Width * 2, Screen.AllScreens.Max(s => s.Bounds.Height));

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            listBox1.MouseClick += ListBox1_MouseClick;
            listBox1.MouseDown += ListBox1_MouseDown;
            txtAddItem.KeyDown += txtAddItem_KeyDown;

            // ListBox item context menu
            contextMenuItemDelete = new ToolStripMenuItem { Text = "Удалить сниппет" };
            contextMenuItemDelete.Click += toolStripMenuItemDelete_Click;

            contextMenuItemEdit = new ToolStripMenuItem { Text = "Редактировать сниппет" };
            contextMenuItemEdit.Click += toolStripMenuItemEdit_Click;

            contextMenuItemNew = new ToolStripMenuItem { Text = "Новый сниппет" };
            contextMenuItemNew.Click += toolStripMenuItemNew_Click;

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from SNIPPETS");

                foreach (DataRow row in dt.Rows)
                    listBox1.Items.Add(row["snippet"]);
            }
        }

        private void txtAddItem_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter = OK button, Shift+Enter = new line
            if (e.KeyCode == Keys.Enter && (ModifierKeys & Keys.Shift) != Keys.Shift)
            {
                e.SuppressKeyPress = true;
                btnOK_Click(null, null);
            }
        }

        ToolStripMenuItem contextMenuItemDelete, contextMenuItemEdit, contextMenuItemNew;

        private void ListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var item = listBox1.IndexFromPoint(e.Location);
                if (item < 0)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
                return;
            }

            if (e.Button != MouseButtons.Right) return;

            contextMenuStrip.Items.Clear();

            var index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                listBox1.SelectedItem = listBox1.Items[index];
                contextMenuStrip.Items.AddRange(new ToolStripItem[] { contextMenuItemDelete, contextMenuItemEdit, contextMenuItemNew });
            }
            else
            {
                contextMenuStrip.Items.Add(contextMenuItemNew);
            }

            contextMenuStrip.Show(Cursor.Position);
            contextMenuStrip.Visible = true;
        }

        private void ListBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            if (e.Button == MouseButtons.Left)
            {
                //select the item under the mouse pointer
                listBox1.SelectedIndex = listBox1.IndexFromPoint(e.Location);
                if (listBox1.SelectedIndex != -1)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        if (rbtnPasteInside.Checked)
                            t.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                        else
                            t.AddSubTopic(listBox1.Items[listBox1.SelectedIndex].ToString());
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtAddItem.Text.Trim()))
            {
                if (edit)
                {
                    using (BubblesDB db = new BubblesDB())
                    {
                        DataTable dt = db.ExecuteQuery("select * from SNIPPETS where snippet=`" + txtAddItem.Text + "`");
                        if (dt.Rows.Count == 0)
                        {
                            listBox1.Items[listBox1.SelectedIndex] = txtAddItem.Text;
                            db.ExecuteNonQuery("update SNIPPETS set snippet=`" + txtAddItem.Text + "`where snippet=`" + selected + "`");
                        }
                        else
                            MessageBox.Show("Такой сниппет уже есть");
                    }
                }
                else
                {
                    using (BubblesDB db = new BubblesDB())
                    {
                        DataTable dt = db.ExecuteQuery("select * from SNIPPETS where snippet=`" + txtAddItem.Text + "`");
                        if (dt.Rows.Count == 0)
                        {
                            listBox1.Items.Add(txtAddItem.Text);
                            db.AddSnippet(txtAddItem.Text);
                        }
                        else
                            MessageBox.Show("Такой сниппет уже есть");
                    }
                }
                btnCancel_Click(null, null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtAddItem.Text = string.Empty;
            txtAddItem.Visible = false;
            btnOK.Visible = false;
            btnCancel.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            int i = listBox1.SelectedIndex;

            using (BubblesDB db = new BubblesDB())
                db.ExecuteNonQuery("delete from SNIPPETS where snippet=`" + listBox1.SelectedItem.ToString() + "`");

            listBox1.Items.RemoveAt(i);

            if (i == 0)
                listBox1.SelectedIndex = 0;
            else
                listBox1.SelectedIndex = i - 1;

        }

        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            txtAddItem.Visible = true;
            txtAddItem.Text = listBox1.SelectedItem.ToString();
            btnOK.Visible = true;
            btnOK.BringToFront();
            btnCancel.Visible = true;
            btnCancel.BringToFront();

            edit = true;
            selected = listBox1.SelectedItem.ToString();
        }

        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            txtAddItem.Visible = true;
            btnOK.Visible = true;
            btnOK.BringToFront();
            btnCancel.Visible = true;
            btnCancel.BringToFront();

            edit = false;
            txtAddItem.Focus();
        }

        private void pClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
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

        bool edit = false;
        string selected = "";

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
