using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Mindjet.MindManager.Interop;
using PRAManager;
using Image = System.Drawing.Image;

namespace Bubbles
{
    public partial class SelectImageDlg : Form
    {
        public SelectImageDlg()
        {
            InitializeComponent();

            Text = Utils.getString("SelectImageDlg.caption");

            imageList1.Images.Add(Image.FromFile(Utils.ImagesPath + "folder.png"));
            space = pSpace.Width;

            ListDirectory(treeView1);
        }

        private void ListDirectory(TreeView treeView)
        {
            string path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryImages);
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, false, Utils.getString("selectimagedlg." + rootDirectoryInfo.Name)));

            path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
            rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, true, Utils.getString("selectimagedlg." + rootDirectoryInfo.Name)));

            path = Utils.m_dataPath + "ImageDB";
            rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, false, Utils.getString("selectimagedlg.savedpictures")));
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, bool icons, string name = "")
        {
            if (name == "") name = directoryInfo.Name;
            var directoryNode = new TreeNode(name)
            {
                ImageIndex = 0,
                SelectedImageIndex = 0,
                Tag = directoryInfo.FullName,
                Name = icons ? "icons" : ""
            };

            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory, icons));

            return directoryNode;
        }

        void treeView1_AfterSelect(object o, TreeViewEventArgs e)
        {
            panel1.Controls.Clear();
            string path = e.Node.Tag.ToString();
            DirectoryInfo di = new DirectoryInfo(path);

            int imgWidth = pImage.Width, imgHeight = pImage.Height;
            int k = 6; // images in row
            if (e.Node.Name == "icons")
            {
                imgWidth = pIcon.Width; 
                imgHeight = pIcon.Height;
                k = 10;
            }

            int i = 0;
            foreach (var file in di.GetFiles())
            {
                if (file.Name.EndsWith(".txt"))
                    continue;

                i++;
                PictureBox icon = new PictureBox
                {
                    Location = new Point(locX, locY),
                    Width = imgWidth, Height = imgHeight,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = Image.FromFile(file.FullName),
                    Name = file.FullName
                };
                panel1.Controls.Add(icon);
                icon.MouseClick += Icon_MouseClick;

                if (i == k) // images in row
                {
                    locX = 0;
                    locY += imgHeight + space;
                    i = 0;
                }
                else
                    locX += imgWidth + space;
            }
            locX = 0;
            locY = 0;
        }

        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox icon = sender as PictureBox;
            iconPath = icon.Name;
            DialogResult = DialogResult.OK;
        }

        int locX = 0;
        int locY = 0;
        int space = 0;

        /// <summary>Full path to icon file</summary>
        public string iconPath = "";
    }
}
