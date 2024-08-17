using System;
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
        public SelectIconDlg(string from, List<string> filenames = null)
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "IconStix.htm#addicon");

            Text = Utils.getString("SelectIconDlg.caption");

            if (filenames != null) FileNames = filenames;
            From = from;

            imageList1.Images.Add(Image.FromFile(Utils.ImagesPath + "folder.png"));
            space = pSpace.Width;

            string path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
            ListDirectory(treeView1, path);

            this.HelpButtonClicked += this_HelpButtonClicked;
            sel_offset = pOffset.Width;
            locX = sel_offset; locY = sel_offset;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "IconStix.htm#addicon");
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            if (From == "IconStix") // Show Priority/Progress node for IconStix only
            {
                var priproNode = new TreeNode()
                {
                    ImageIndex = 0,
                    SelectedImageIndex = 0,
                    Tag = "PP",
                    Text = Utils.getString("SelectIconDlg.PriPro")
                };
                treeView.Nodes.Add(priproNode);
            }
            else if (From == "ManageTools" || From == "EditTool")
            {
                var toolsNode = new TreeNode()
                {
                    ImageIndex = 0,
                    SelectedImageIndex = 0,
                    Tag = Utils.m_dataPath + "AppIconDB",
                    Text = Utils.getString("SelectIconDlg.ToolIcons")
                };
                treeView.Nodes.Add(toolsNode);
            }

            // Custom icons
            var customiconsNode = new TreeNode()
            {
                ImageIndex = 0,
                SelectedImageIndex = 0,
                Tag = Utils.m_dataPath + "IconDB",
                Text = Utils.getString("SelectIconDlg.CustomIcons")
            };
            treeView.Nodes.Add(customiconsNode);

            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));

            // First node can be PriPro node (if from IconStix) or Custom Icons node 
            if (From != "IconStix" && From != "ManageTools" && From != "EditTool")
                treeView.SelectedNode = treeView.Nodes[1];
            else // if from IconStix or Manage Tools window
                treeView.SelectedNode = treeView.Nodes[2];
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
            SelectedIcons.Clear();
            string path = e.Node.Tag.ToString();

            if (path == "PP") // Priority & Progress icons
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
                        locX = sel_offset;
                        locY += pBox.Height + space;
                        i = 0;
                    }
                    else
                        locX += pBox.Width + space;
                }
            }
            locX = sel_offset;
            locY = sel_offset;
        }

        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox icon = sender as PictureBox;
            iconPath = icon.Name;
            string filename;

            if (icon.Name == "selected") // Click on the selected icon mark.
            {
                ((PictureBox)icon.Tag).Tag = null; // remove tag from icon
                SelectedIcons.Remove((PictureBox)icon.Tag); // remove icon from SelectedIcons
                panel1.Controls.Remove(icon); // remove icon check mark
                return;
            }

            if (iconPath.StartsWith("pr")) // Priority or Progress icon
            {
                filename = "pripro" + iconPath;
                iconPath = Utils.dllPath + "Images\\" + iconPath + ".png";
            }
            else
            {
                filename = Path.GetFileNameWithoutExtension(iconPath);
                if (Utils.StockIconDupes.ContainsKey(filename))
                {
                    // Icon is a double of the stock icon. Replace it with the stock icon!
                    string path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
                    filename = Utils.StockIconDupes[filename];
                    iconPath = path + filename + ".ico";
                }
            }

            foreach (var _filename in FileNames) // проверим, есть ли в пузыре этот значок
            {
                string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(iconPath);

                if (_filename == filename || // Priority or Progress
                    _filename == "stock" + filename || // stock icon
                    _filename == signature) // custom icon
                {
                    MessageBox.Show(Utils.getString("stix.iconexists"));
                    return;
                }
            }

            if (From == "IconStix") // Dialog is called from IconStix. User can select mutiple icons.
            {
                PictureBox selected = new PictureBox(); selected.Name = "selected";
                selected.Image = Image.FromFile(Utils.m_imagesPath + "icon_selected.png");
                selected.Size = pSelected.Size; selected.SizeMode = PictureBoxSizeMode.Zoom;

                if (ModifierKeys == Keys.Control)
                {
                    if (SelectedIcons.Keys.Contains(icon))
                    {
                        panel1.Controls.Remove((PictureBox)icon.Tag);
                        SelectedIcons.Remove(icon); icon.Tag = null;
                        return;
                    }
                }
                else
                {
                    bool sel = icon.Tag is PictureBox;
                    foreach (PictureBox pb in SelectedIcons.Keys.Reverse())
                    {
                        panel1.Controls.Remove((PictureBox)pb.Tag);
                        SelectedIcons.Remove(pb);
                        pb.Tag = null;
                    }
                    if (sel) return;
                }

                SelectedIcons.Add(icon, iconPath);
                icon.Tag = selected; selected.Tag = icon;
                selected.MouseClick += Icon_MouseClick;
                selected.Location = new Point(icon.Location.X - pOffset.Width, icon.Location.Y - pOffset.Width);
                panel1.Controls.Add(selected); selected.BringToFront();
            }
            else // This dialog is called from TaskTemplate dialog
                DialogResult = DialogResult.OK;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string path = txtPath.Text.Trim();
            if (path != "" && File.Exists(path))
                iconPath = path;
        }

        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "Image files (*.ico, *.png, *.jpg, *.gif, *.bmp)|*.ico;*.png;*.jpg;*.jpeg;*.gif;*.bmp;|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                txtPath.Text = openFileDialog1.FileName;
        }

        bool topdir = true;
        int locX = 0;
        int locY = 0;
        int sel_offset;
        int space = 0;

        /// <summary>Full path to icon file</summary>
        public string iconPath = "";
        public string iconName = "";

        string From = "";

        /// <summary>Filenames of stick icons. To try the icon exists in the stick</summary>
        private List<string> FileNames = new List<string>();
        /// <summary>Filenames of the selected here icons.</summary>
        public Dictionary<PictureBox, string> SelectedIcons = new Dictionary<PictureBox, string>();
    }
}