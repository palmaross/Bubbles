using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class SourceListDlg : Form
    {
        public SourceListDlg()
        {
            InitializeComponent();

            btnClose.Text = Utils.getString("button.close");

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Add a dummy column
            listView1.Columns.Add("", 0, HorizontalAlignment.Left);
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.Columns[0].Width = listView1.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip_ItemClicked;

            MS_delete.Text = Utils.getString("button.delete");
            StixUtils.SetContextMenuImage(MS_delete, "deleteall.png");

            MS_rename.Text = Utils.getString("button.rename");
            StixUtils.SetContextMenuImage(MS_delete, "edit.png");

            imageList1.ImageSize = imageSize.Size;
            listView1.SmallImageList = imageList1;

            imageList1.Images.Add("audio", Image.FromFile(Utils.ImagesPath + "ms_audio.png"));
            imageList1.Images.Add("exe", Image.FromFile(Utils.ImagesPath + "ms_exe.png"));
            imageList1.Images.Add("file", Image.FromFile(Utils.ImagesPath + "ms_file.png"));
            imageList1.Images.Add("image", Image.FromFile(Utils.ImagesPath + "ms_img.png"));
            imageList1.Images.Add("macros", Image.FromFile(Utils.ImagesPath + "ms_macros.png"));
            imageList1.Images.Add("map", Image.FromFile(Utils.ImagesPath + "ms_map.png"));
            imageList1.Images.Add("pdf", Image.FromFile(Utils.ImagesPath + "ms_pdf.png"));
            imageList1.Images.Add("txt", Image.FromFile(Utils.ImagesPath + "ms_txt.png"));
            imageList1.Images.Add("http", Image.FromFile(Utils.ImagesPath + "ms_web.png"));
            imageList1.Images.Add("word", Image.FromFile(Utils.ImagesPath + "ms_word.png"));
            imageList1.Images.Add("excel", Image.FromFile(Utils.ImagesPath + "ms_excel.png"));
            imageList1.Images.Add("youtube", Image.FromFile(Utils.ImagesPath + "ms_youtube.png"));
            imageList1.Images.Add("video", Image.FromFile(Utils.ImagesPath + "ms_video.png"));
            imageList1.Images.Add("chm", Image.FromFile(Utils.ImagesPath + "chm.png"));

            this.Paint += MySourcesListDlg_Paint; // paint the border
            this.MaximumSize = new Size(this.Width * 2, this.Height * 3);

            this.Deactivate += SourceListDlg_Deactivate;
        }

        private void SourceListDlg_Deactivate(object sender, EventArgs e)
        {
            this.Close(); this.Dispose();
        }

        public int thisHeight;

        private void SourceListDlg_Load(object sender, EventArgs e)
        {
            this.Height = thisHeight;
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "MS_delete")
            {
                var selected = listView1.SelectedItems[0];
                string path = (string)selected.Tag;

                using (StixDB db = new StixDB())
                {
                    //db.ExecuteNonQuery("delete from SOURCES where name=")
                }

                listView1.SelectedItems[0].Remove();
            }
            if (e.ClickedItem.Name == "MS_rename")
            {

            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            if (e.Button == MouseButtons.Left)
            {
                if (listView1.SelectedItems.Count == 0)
                    return;

                var selectedItem = listView1.SelectedItems[0];
                if (selectedItem != null)
                {
                    try
                    {
                        Process.Start(selectedItem.Tag.ToString());
                    }
                    catch { }
                }

                DialogResult = DialogResult.OK;
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Show context menu
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = true;

                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void MySourcesListDlg_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
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
