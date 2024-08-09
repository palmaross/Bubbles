using PRAManager;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class NewToolDlg : Form
    {
        public NewToolDlg(Form aManageTools, Form aStix)
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "ToolStix.htm#newtool");

            lblSpecifyPath.Text = Utils.getString("NewToolDlg.lblSpecifyPath");
            lblToolName.Text = Utils.getString("NewToolDlg.lblTitle");
            lblTooltip.Text = Utils.getString("NewToolDlg.lblTooltip");
            lblChangeIcon.Text = Utils.getString("NewToolDlg.lblChangeIcon");
            lblToolIcon.Text = Utils.getString("NewToolDlg.lblToolIcon");
            lblTip.Text = Utils.getString("NewToolDlg.lblTip");
            chAddToDatabase.Text = Utils.getString("NewToolDlg.chAddToDatabase");
            toolTip1.SetToolTip(chAddToDatabase, "NewToolDlg.chAddToDatabase.tooltip");
            btnAddTool.Text = Utils.getString("button.add");
            btnClose.Text = Utils.getString("button.close");

            btnPages.Text = Utils.getString("NewToolDlg.btnPages");
            toolTip1.SetToolTip(btnPages, Utils.getString("NewToolDlg.btnPages.tooltip"));

            txtboxPaste.Text = Utils.getString("button.paste");
            txtboxClear.Text = Utils.getString("button.clear");

            Stix = aStix; ManageTools = aManageTools;

            if (Stix == null)
                chAddToDatabase.Visible = false;

            this.Paint += This_Paint; // paint the border
            this.HelpButtonClicked += this_HelpButtonClicked;
        }

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "ToolStix.htm#newtool");
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void txtPath_KeyUp(object sender, KeyEventArgs e)
        {
            if (e == null || e.KeyCode == Keys.Enter || (e.KeyCode == Keys.V && e.Control))
            {
                ClearPages();
                btnPages.Visible = false;
                btnPages.Font = new Font(btnPages.Font, FontStyle.Regular);

                string path = txtPath.Text.Trim();
                if (path == "") return;
                string title = "";

                if (path.StartsWith("http"))
                {
                    lblWait.Visible = true;
                    title = Utils.GetWebPageTitle(path);
                    lblWait.Visible = false;
                }
                else // file
                {
                    if (!File.Exists(path)) return;

                    //if (path.EndsWith(".pdf"))
                    //    btnPages.Visible = true;
                    //else
                    //    btnPages.Visible = false;

                    try { title = Path.GetFileName(path); }
                    catch { }
                }

                if (!String.IsNullOrEmpty(title))
                    txtTitle.Text = title;

                // Set app icon
                string imageType = Utils.GetFileType(path);
                Image img = StixUtils.GetToolImage(imageType, path);
                if (img != null) pIcon.Image = img;
                pIcon.Tag = imageType;

                this.Refresh();

                if (e != null)
                {
                    e.Handled = true; // to avoid the "ding" sound
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void btnClearPages_Click(object sender, EventArgs e)
        {
            ClearPages(); Arguments = "";
            btnPages.Font = new Font(btnPages.Font, FontStyle.Regular);
        }

        private void ClearPages()
        {
            //foreach (NumericUpDown num in groupPages.Controls.OfType<NumericUpDown>())
            //    num.Value = 1;
            //foreach (TextBox tb in groupPages.Controls.OfType<TextBox>())
            //    tb.Text = "";
        }

        private void ProcessPages()
        {
            var alias = this.Controls.OfType<TextBox>().ToList();
            alias.Sort((c1, c2) => c1.TabIndex.CompareTo(c2.TabIndex));

            var numbers = this.Controls.OfType<NumericUpDown>().ToList();
            numbers.Sort((c1, c2) => c1.TabIndex.CompareTo(c2.TabIndex));

            string args = "?args=";
            for (int i = 0; i < alias.Count; i++)
            {
                if (alias[i].Text.Trim() != "")
                    args += numbers[i].Value.ToString() + "###" + alias[i].Text.Trim() + "%%%";
            }

            args = args.Trim('%');

            if (args != "?args=")
            {
                btnPages.Font = new Font(btnPages.Font, FontStyle.Bold);
                Arguments = args;
            }
            else
                Arguments = ""; // no pages
        }

        private void txtPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtPath.SelectAll();
        }

        private void pIcon_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            string iconPath = "";

            using (SelectIconDlg dlg = new SelectIconDlg("ManageTools"))
            {
                if (dlg.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                    iconPath = dlg.iconPath;
            }

            pb.Image = Image.FromFile(iconPath);

            string filename = Path.GetFileName(iconPath);
            if (filename.StartsWith("tool-"))
                pb.Tag = Path.GetFileName(iconPath);
            else
                pb.Tag = "tool-" + Path.GetFileName(iconPath);

            string path = Utils.m_dataPath + "AppIconDB\\" + pb.Tag.ToString();
            if (!File.Exists(path))
                File.Copy(iconPath, path);
        }

        private void btnAddTool_Click(object sender, EventArgs e)
        {
            string toolPath = txtPath.Text.Trim();
            string title = txtTitle.Text.Trim();
            string tooltip = txtTooltip.Text.Trim();

            if (toolPath == "" || title == "" || pIcon.Tag.ToString() == "") return;

            if (toolPath.ToLower().EndsWith(".pdf"))
            {
                //ProcessPages();
                toolPath += Arguments;
            }

            // Add tool to Stix
            if (Stix != null)
                (Stix as StixTools).NewIcon(toolPath, title, "end", pIcon.Tag.ToString(), tooltip);

            // Add tool to database
            if (ManageTools != null || (chAddToDatabase.Visible && chAddToDatabase.Checked))
            {
                // Add to database
                using (StixDB db = new StixDB())
                    db.AddTool(title, tooltip, toolPath, pIcon.Tag.ToString(), 0, 0);

                // Add to the ManageTools window
                if (StixMain.m_ManageTools != null && StixMain.m_ManageTools.Visible)
                    StixMain.m_ManageTools.InitOmniTools();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter =
                "App files (*.exe)|*.exe;|" +
                "Word files (*.docx, *.doc)|*.docx;*.doc;|" +
                "Excel files (*.xlsx, *.xls)|*.xlsx;*.xls;|" +
                "PDF files (*.pdf)|*.pdf;|" +
                "MindManager files (*.mmap, *.mmat)|*.mmap;*.mmat;|" +
                "MindManager Macro files (*.mmbas)|*.mmbas;|" +
                "All files (*.*)|*.*";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
                txtPath_KeyUp(sender, null); // proceed with title and icon
            }
        }

        public string Arguments = "";
        Form ManageTools = null;
        Form Stix = null;

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim() == "" || txtTitle.Text.Trim() == "") btnAddTool.Enabled = false;
            else btnAddTool.Enabled = true;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "" || txtPath.Text.Trim() == "") btnAddTool.Enabled = false;
            else btnAddTool.Enabled = true;
        }

        private void txtboxPaste_Click(object sender, EventArgs e)
        {
            txtPath.Text = Clipboard.GetText();
            txtPath_KeyUp(sender, null);
        }

        private void txtboxClear_Click(object sender, EventArgs e)
        {
            txtPath.Clear();
        }
    }
}
