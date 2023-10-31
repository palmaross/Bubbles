using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class MySourcesListDlg : Form
    {
        public MySourcesListDlg() //(int X, int Y, string orientation)
        {
            InitializeComponent();

            btnClose.Text = Utils.getString("button.close");

            imageList1.ImageSize = p2.Size;
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

            // Add a dummy column        
            ColumnHeader header = new ColumnHeader();
            header.Text = "";
            header.Name = "col1";
            listView1.Columns.Add(header);
            listView1.Columns[0].Width = listView1.Width - p1.Width;
            // Then
            listView1.Scrollable = true;
            listView1.View = View.Details;

            this.Paint += MySourcesListDlg_Paint; // paint the border
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            var selectedItem = listView1.SelectedItems[0];
            if (selectedItem != null)
            {
                try {
                    Process.Start(selectedItem.Tag.ToString());
                } catch { }
            }

            DialogResult = DialogResult.OK;
        }

        private void MySourcesListDlg_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
