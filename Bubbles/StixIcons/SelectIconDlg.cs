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
            else if (From == "ManageTools")
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
            if (From != "IconStix" && From != "ManageTools")
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
                string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(iconPath);

                if (_filename == filename || // Priority or Progress
                    _filename == "stock" + filename || // stock icon
                    _filename == signature) // custom icon
                {
                    MessageBox.Show(Utils.getString("stix.iconexists"));
                    return;
                }
            }

            if (From == "IconStix") // Dialog is called from IconStix
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
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                txtPath.Text = openFileDialog1.FileName;
        }

        bool topdir = true;
        int locX = 0;
        int locY = 0;
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