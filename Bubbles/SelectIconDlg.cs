using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Mindjet.MindManager.Interop;
using PRAManager;
using Image = System.Drawing.Image;

namespace Bubbles
{
    public partial class SelectIconDlg : Form
    {
        public SelectIconDlg(bool manage)
        {
            InitializeComponent();

            Text = Utils.getString("SelectIconDlg.caption");
            rbtnLeft.Text = Utils.getString("SelectIconDlg.rbtnLeft");
            rbtnRight.Text = Utils.getString("SelectIconDlg.rbtnRight");
            rbtnEnd.Text = Utils.getString("SelectIconDlg.rbtnEnd");
            rbtnBegin.Text = Utils.getString("SelectIconDlg.rbtnBegin");

            imageList1.Images.Add(Image.FromFile(Utils.ImagesPath + "folder.png"));
            space = pSpace.Width;

            string path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
            ListDirectory(treeView1, path);

            rbtnLeft.Enabled = !manage; rbtnRight.Enabled = !manage;

            txtIconName.KeyUp += TxtIconName_KeyUp;
        }

        private void TxtIconName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
                this.DialogResult = DialogResult.OK;
            }
            else if (e.KeyCode == Keys.Escape)
                txtIconName.Visible = false;
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            //treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name)
            {
                ImageIndex = 0,
                SelectedImageIndex = 0,
                Tag = directoryInfo.FullName
            };
            if (topdir)
            {
                directoryNode.Text = "MindManager"; // Utils.getString("SelectIconDlg.Icons");
                directoryNode.Expand();
                topdir = false;
            }
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));

            return directoryNode;
        }

        void treeView1_AfterSelect(object o, TreeViewEventArgs e)
        {
            panel1.Controls.Clear();
            txtIconName.Visible = false;
            string path = e.Node.Tag.ToString();
            DirectoryInfo di = new DirectoryInfo(path);

            //locX += space; locY += space;
            int i = 0;
            foreach (var file in di.GetFiles())
            {
                i++;
                PictureBox icon = new PictureBox
                {
                    Location = new Point(locX, locY),
                    Width = pBox.Width, Height = pBox.Height,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = Image.FromFile(file.FullName),
                    Name = file.FullName
                };
                panel1.Controls.Add(icon);
                icon.MouseClick += Icon_MouseClick;

                if (i == 6) // 6 icons in row
                {
                    locX = 0;
                    locY += pBox.Height + space;
                    i = 0;
                }
                else
                    locX += pBox.Width + space;
            }
            locX = 0;
            locY = 0;
        }

        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox icon = sender as PictureBox;
            iconPath = icon.Name;
            string filename = Path.GetFileNameWithoutExtension(iconPath);

            foreach (var item in BubbleIcons.Icons) // проверим, есть ли в пузыре этот значок
            {
                if (item.FileName == filename + ".ico" || // custom icon
                    item.FileName == "stock" + filename) // stock icon
                {
                    MessageBox.Show(Utils.getString("float_icons.iconexists"));
                    return;
                }
            }

            txtIconName.Visible = true;
            txtIconName.BringToFront();

            int locx = panel1.Location.X + icon.Location.X;
            int locy = panel1.Location.Y + icon.Location.Y + icon.Height;
            if (locx > panel1.Location.X + panel1.Width - txtIconName.Width)
                locx = panel1.Location.X + panel1.Width - txtIconName.Width;

            txtIconName.Text = Path.GetFileNameWithoutExtension(icon.Name);
            txtIconName.Location = new Point(locx, locy);
            txtIconName.Focus();
            txtIconName.SelectAll();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            iconName = txtIconName.Text.Trim();
        }

        bool topdir = true;
        int locX = 0;
        int locY = 0;
        int space = 0;

        /// <summary>Full path to icon file</summary>
        public string iconPath = "";
        public string iconName = "";
    }
}
