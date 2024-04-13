using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class AllSourcesDlg : Form
    {
        public AllSourcesDlg()
        {
            InitializeComponent();

            Text = Utils.getString("AllSourcesDlg.title");
            lblLink.Text = Utils.getString("AllSourcesDlg.lblLink");
            btnNew.Text = Utils.getString("AllSourcesDlg.btnNew");
            btnModify.Text = Utils.getString("button.modify");
            btnDelete.Text = Utils.getString("button.delete");
            btnOpen.Text = Utils.getString("AllSourcesDlg.btnOpen");
            btnClose.Text = Utils.getString("button.close");

            lblTitle.Text = Utils.getString("AllSourcesDlg.SourceTitle");
            lblLink2.Text = Utils.getString("AllSourcesDlg.lblLink");
            lblGroup.Text = Utils.getString("AllSourcesDlg.SourceGroup");
            btnCancel.Text = Utils.getString("button.cancel");

            SourceTitle.HeaderText = Utils.getString("AllSourcesDlg.SourceTitle");
            SourceGroup.HeaderText = Utils.getString("AllSourcesDlg.SourceGroup");

            SourceImage.Width = (int)(pSize.Width * 1.5); // source type icon
            SourceTitle.Width = (int)(dataGridView1.Width / 1.8);
            SourceGroup.Width = (int)(dataGridView1.Width / 3);
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.ResizeEnd += AllSourcesDlg_ResizeEnd;

            Init();
        }

        private void AllSourcesDlg_ResizeEnd(object sender, EventArgs e)
        {
            SourceImage.Width = (int)(pSize.Width * 1.5); // map type icon
            SourceTitle.Width = (int)(dataGridView1.Width / 1.85);
            SourceGroup.Width = (int)(dataGridView1.Width / 3);
        }

        void Init()
        {
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from SOURCEGROUPS");
                foreach (DataRow dr in dt.Rows)
                    SourceGroups.Add(Convert.ToInt32(dr["id"]), dr["name"].ToString());

                dt = db.ExecuteQuery("select * from SOURCES order by title");
                foreach (DataRow dr in dt.Rows)
                    AddToTable(dr["title"].ToString(), dr["path"].ToString(), Convert.ToInt32(dr["groupID"]));
            }
        }

        private void AddToTable(string title, string path, int groupID)
        {
            string imageType = BubbleSources.GetFileType(path);
            Image img;

            if (imageType == "exe")
            {
                try
                {
                    Icon appIcon = Icon.ExtractAssociatedIcon(path);
                    img = appIcon.ToBitmap();
                }
                catch { img = GetImage(imageType); }
            }
            else
                img = GetImage(imageType);

            img = new Bitmap(img, new Size(pSize.Width, pSize.Height));

            string group = SourceGroups[groupID];

            int rowId = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[rowId];

            row.Cells["SourceImage"].Value = img;
            row.Cells["SourceTitle"].Value = title;
            row.Cells["SourceGroup"].Value = group; // group name
            row.Cells["SourcePath"].Value = path;
            row.Cells["GroupID"].Value = groupID;
            row.Cells["SortByImage"].Value = imageType;
        }

        public static Image GetImage(string type)
        {
            switch (type)
            {
                case "audio": return Image.FromFile(Utils.ImagesPath + "ms_audio.png");
                case "excel": return Image.FromFile(Utils.ImagesPath + "ms_excel.png");
                case "exe": return Image.FromFile(Utils.ImagesPath + "ms_exe.png");
                case "image": return Image.FromFile(Utils.ImagesPath + "ms_img.png");
                case "macros": return Image.FromFile(Utils.ImagesPath + "ms_macros.png");
                case "map": return Image.FromFile(Utils.ImagesPath + "ms_map.png");
                case "pdf": return Image.FromFile(Utils.ImagesPath + "ms_pdf.png");
                case "txt": return Image.FromFile(Utils.ImagesPath + "ms_txt.png");
                case "video": return Image.FromFile(Utils.ImagesPath + "ms_video.png");
                case "http": return Image.FromFile(Utils.ImagesPath + "ms_web.png");
                case "word": return Image.FromFile(Utils.ImagesPath + "ms_word.png");
                case "youtube": return Image.FromFile(Utils.ImagesPath + "ms_youtube.png");
                case "chm": return Image.FromFile(Utils.ImagesPath + "chm.png");
            }
            return BubbleSources.file;
        }

        /// <summary>
        /// Sort by the first column (map type)
        /// </summary>
        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 && e.ColumnIndex == 0)
            {
                sortby = "SortByImage";
                thenby = "SourceTitle";
                SortByType();
            }
        }

        private void SortByType()
        {
            if (Direction == ListSortDirection.Ascending)
            {
                Direction = ListSortDirection.Descending;
                dataGridView1.Sort(new MyComparer(SortOrder.Descending));
            }
            else
            {
                Direction = ListSortDirection.Ascending;
                dataGridView1.Sort(new MyComparer(SortOrder.Ascending));
            }
        }
        private ListSortDirection Direction = ListSortDirection.Descending;

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            panelModify.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelModify.Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var item = dataGridView1.SelectedRows[0];

                txtLink.Text = item.Cells["SourcePath"].Value.ToString();
            }
        }

        Dictionary<int, string> SourceGroups = new Dictionary<int, string>();

        public static string sortby;
        public static string thenby;
    }

    internal class MyComparer : System.Collections.IComparer
    {
        private static int sortOrderModifier = 1;

        public MyComparer(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

            string by = AllSourcesDlg.sortby;
            string thenby = AllSourcesDlg.thenby;
            string value1 = DataGridViewRow1.Cells[by].Value.ToString();
            string value2 = DataGridViewRow2.Cells[by].Value.ToString();
            string thenvalue1 = DataGridViewRow1.Cells[thenby].Value.ToString();
            string thenvalue2 = DataGridViewRow2.Cells[thenby].Value.ToString();

            // Try to sort based on the sortby column
            int CompareResult = String.Compare(value1, value2);

            // If the results are equal, sort based on the thenby column (file name).
            if (CompareResult == 0)
            {
                CompareResult = String.Compare(thenvalue1, thenvalue2);
                CompareResult *= sortOrderModifier; // to have always ascending sort order in this column
            }
            return CompareResult * sortOrderModifier;
        }
    }
}
