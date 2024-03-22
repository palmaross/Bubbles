using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mindjet.MindManager.Interop;
using PRAManager;
using Image = System.Drawing.Image;

namespace Bubbles
{
    public partial class SelectIconDlg : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames">Filenames of icons of the Icon stick</param>
        /// <param name="tasktemplate">If dialog is called from TaskTemplateDlg</param>
        public SelectIconDlg(List<string> filenames, bool tasktemplate = false)
        {
            InitializeComponent();

            Text = Utils.getString("SelectIconDlg.caption");

            FileNames = filenames;
            taskTemplate = tasktemplate;

            imageList1.Images.Add(Image.FromFile(Utils.ImagesPath + "folder.png"));
            space = pSpace.Width;

            string path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
            ListDirectory(treeView1, path);

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
            if (!taskTemplate)
            {
                var directoryNode = new TreeNode()
                {
                    ImageIndex = 0,
                    SelectedImageIndex = 0,
                    Tag = "PP",
                    Text = Utils.getString("SelectIconDlg.PriPro")
                };
                treeView.Nodes.Add(directoryNode);
            }

            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));

            if (!taskTemplate)
                treeView.SelectedNode = treeView.Nodes[1];
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
                directoryNode.Text = "MindManager";
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

            if (path == "PP")
            {
                panel1.Controls.Add(panelPP);
                panelPP.Visible = true;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(path);

                int i = 0;
                foreach (var file in di.GetFiles())
                {
                    i++;
                    PictureBox icon = new PictureBox
                    {
                        Location = new Point(locX, locY),
                        Width = pBox.Width,
                        Height = pBox.Height,
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
            }
            locX = 0;
            locY = 0;
        }

        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox icon = sender as PictureBox;
            iconPath = icon.Name;
            string filename;

            if (iconPath.StartsWith("pr")) // Priority or Progress icon
            {
                filename = "pripro" + iconPath;
                iconPath = Utils.dllPath + "Images\\" + iconPath + ".png";
            }
            else
                filename = Path.GetFileNameWithoutExtension(iconPath);

            foreach (var _filename in FileNames) // проверим, есть ли в пузыре этот значок
            {
                if (_filename == filename || // Priority or Progress
                    _filename == filename + ".ico" || // custom icon
                    _filename == "stock" + filename) // stock icon
                {
                    MessageBox.Show(Utils.getString("float_icons.iconexists"));
                    return;
                }
            }

            if (!taskTemplate) // This dialog is called from Icon stick
            {
                if (ModifierKeys == Keys.Control)
                {
                    if (SelectedIcons.Keys.Contains(icon))
                    {
                        SelectedIcons.Remove(icon);
                        icon.BackColor = SystemColors.Control;
                        return;
                    }
                    else
                        SelectedIcons.Add(icon, iconPath);

                    icon.BackColor = SystemColors.Highlight;
                }
                else
                {
                    foreach (PictureBox pb in SelectedIcons.Keys.Reverse())
                    {
                        pb.BackColor = SystemColors.Control;
                        SelectedIcons.Remove(pb);
                    }

                    icon.BackColor = SystemColors.Highlight;
                    SelectedIcons.Add(icon, iconPath);
                }

                //txtIconName.Visible = true;
                //txtIconName.BringToFront();

                //// Get name textbox location
                //int locx = panel1.Location.X + icon.Location.X;
                //int locy = panel1.Location.Y + icon.Location.Y + icon.Height;
                //if (locx > panel1.Location.X + panel1.Width - txtIconName.Width)
                //    locx = panel1.Location.X + panel1.Width - txtIconName.Width;

                //// Show name textbox
                //txtIconName.Text = Path.GetFileNameWithoutExtension(icon.Name);
                //txtIconName.Location = new Point(locx, locy);
                //txtIconName.Focus();
                //txtIconName.SelectAll();
            }
            else // This dialog is called from TaskTemplate dialog
                DialogResult = DialogResult.OK;
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

        bool taskTemplate = false;

        /// <summary>Filenames of stick icons. To try the icon exists in the stick</summary>
        private List<string> FileNames = new List<string>();
        /// <summary>Filenames of the selected here icons.</summary>
        public Dictionary<PictureBox, string> SelectedIcons = new Dictionary<PictureBox, string>();
    }
}
