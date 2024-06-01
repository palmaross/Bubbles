using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ToolListDlg : Form
    {
        public ToolListDlg(List<ToolItem> Tools)
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

            imageList1.ImageSize = imageSize.Size;

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
            imageList1.Images.Add("chm", Image.FromFile(Utils.ImagesPath + "ms_chm.png"));

            string ipath = Utils.m_dataPath + "AppIconDB\\";
            ListViewItem lv;

            foreach (var item in Tools)
            {
                if (item.Type == "exe")
                {
                    try
                    {
                        Icon appIcon = Icon.ExtractAssociatedIcon(item.Path);
                        imageList1.Images.Add(item.Type, appIcon.ToBitmap());
                        lv = listView1.Items.Add(" " + item.Title, item.Type);
                    }
                    catch { lv = listView1.Items.Add(" " + item.Title, item.Type); }
                }
                else if (item.Type.StartsWith("tool")) // custom image
                {
                    imageList1.Images.Add(item.Type, Image.FromFile(ipath + item.Type));
                    lv = listView1.Items.Add(" " + item.Title, item.Type);
                }
                else
                    lv = listView1.Items.Add(" " + item.Title, item.Type);

                lv.Tag = item;
                if (item.Tooltip != "")
                    lv.ToolTipText = item.Tooltip;
            }

            this.Paint += ToolListDlg_Paint; // paint the border
            this.MaximumSize = new Size(this.Width * 2, this.Height * 3);

            this.Deactivate += ToolListDlg_Deactivate;
        }

        private void ToolListDlg_Deactivate(object sender, EventArgs e)
        {
            this.Close(); this.Dispose();
        }

        public int thisHeight;

        private void ToolListDlg_Load(object sender, EventArgs e)
        {
            this.Height = thisHeight;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            var selectedItem = listView1.SelectedItems[0];
            if (selectedItem != null)
            {
                try {
                    (ToolStix as StixTools).RunTool(selectedItem.Tag as ToolItem);
                } catch { }
            }

            DialogResult = DialogResult.OK;
        }

        private void ToolListDlg_Paint(object sender, PaintEventArgs e)
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
        #endregion}

        public Form ToolStix;
    }
}
