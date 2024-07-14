using HtmlAgilityPack;
using PRAManager;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Policy;

namespace Bubbles
{
    public partial class NewLinkDlg : Form
    {
        public NewLinkDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "Add_Edit_Link.htm");

            Text = Utils.getString("NewLinkDlg.title");
            lblTitle.Text = Utils.getString("NewLinkDlg.lblTitle");
            lblLink.Text = Utils.getString("NewLinkDlg.lblLink");
            lblWait.Text = Utils.getString("NewLinkDlg.lblWait");
            chDownload.Text = Utils.getString("NewLinkDlg.cbDownload");
            lblLinkGroup.Text = Utils.getString("NewLinkDlg.lblLinkGroup");
            grBoxDownload.Text = Utils.getString("NewLinkDlg.grBoxDownload");
            chDownload.Text = Utils.getString("NewLinkDlg.cbDownload");
            btnPreview.Text = Utils.getString("NewLinkDlg.btnPreview");
            lblResult.Text = Utils.getString("NewLinkDlg.lblResult");
            txtComment.Text = Utils.getString("NewLinkDlg.txtComment");
            btnClose.Text = Utils.getString("button.close");

            pasteTxt.Text = Utils.getString("button.paste");

            thisHeight = this.Height;
            this.HelpButtonClicked += this_HelpButtonClicked;

            FillGroups();
        }
        int thisHeight;

        private void this_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "Add_Edit_Link.htm");
        }

        public void FillGroups()
        {
            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from LINKGROUPS order by name");
                foreach (DataRow dr in dt.Rows)
                {
                    cbLinkGroup.Items.Add(new LinkGroupItem(Convert.ToInt32(dr["id"]), dr["name"].ToString()));
                }
            }
        }

        public void SelectGroup(int groupID)
        {
            foreach (var group in cbLinkGroup.Items)
            {
                LinkGroupItem item = group as LinkGroupItem;
                if (item.ID == groupID)
                {
                    cbLinkGroup.SelectedItem = item;
                    break;
                }
            }
        }

        private void txtLink_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtLink.SelectAll();
        }

        private void txtTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtTitle.SelectAll();
        }

        private void paste_Click(object sender, EventArgs e)
        {
            txtLink.Text = Clipboard.GetText().Trim();
            if (txtLink.Text != "")
                txtLink_KeyUp(null, null);
        }

        public void txtLink_KeyUp(object sender, KeyEventArgs e)
        {
            string link = txtLink.Text.Trim();
            if (link == "") return;
            string title = "";
            grBoxDownload.Visible = false;

            if (link.StartsWith("http"))
            {
                var uri = new Uri(link);
                baseUrl = uri.GetLeftPart(UriPartial.Authority);

                lblWait.Visible = true;
                HtmlWeb web = new HtmlWeb();
                try
                {
                    htmlDoc = web.Load(link);
                    try
                    {
                        var node = htmlDoc.DocumentNode.SelectSingleNode("//head");

                        foreach (var nNode in htmlDoc.DocumentNode.Descendants())
                        {
                            if (nNode.Name == "title")
                            {
                                title = nNode.InnerText;
                                break;
                            }
                        }
                    }
                    catch { }

                    lblWait.Visible = false;

                    if (!link.Contains("youtube.com")) // youtube is not suitable for html file
                    {
                        // Add source url.
                        HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//html/body");

                        string html = bodyNode.InnerHtml;
                        html = "<div style='border: 2px double red; padding: 6px 6px 0 6px;'><p><strong>Source:</strong> <a href='" + link + "' target='_blank'>" + title + "</a></p></div>" + html;
                        bodyNode.InnerHtml = html;

                        grBoxDownload.Visible = true;
                    }
                }
                catch { }
            }
            else // file
            {
                try { title = Path.GetFileName(link); }
                catch { }
            }

            if (!String.IsNullOrEmpty(title))
                txtTitle.Text = title;

            if (grBoxDownload.Visible)
                this.Height = thisHeight;
            else
                this.Height = thisHeight - grBoxDownload.Height;

            if (e != null)
            {
                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (htmlDoc == null) return;

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                if (!string.IsNullOrEmpty(link.Attributes["href"].Value))
                {
                    HtmlAttribute attr = link.Attributes["href"];

                    var url = GetAbsoluteUrlString(baseUrl, attr.Value);
                    attr.Value = url;
                    //att.Value = this.AbsoluteUrlByRelative(att.Value);
                }
            }

            Random r = new Random();
            string filename = r.Next().ToString() + ".html";

            string filepath = Utils.m_localDataPath + filename;
            htmlDoc.Save(filepath);

            if (OmniBrowser == null || OmniBrowser.IsDisposed)
            {
                OmniBrowser = new BrowserDlg("");
                //OmniBrowser.txtAddressBar.Text = filepath;
                OmniBrowser.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }

            OmniBrowser.p_url = txtLink.Text;
            OmniBrowser.p_title = txtTitle.Text;
            OmniBrowser.p_comment = txtComment.Text;
            OmniBrowser.p_group = cbLinkGroup.SelectedIndex;
            OmniBrowser.Preview(filepath);
        }

        static string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(baseUrl), uri);
            return uri.ToString();
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            string link = txtLink.Text.Trim();
            string title = txtTitle.Text.Trim();

            if (link == "" || title == "") return; // to do message to user

            // Download webpage.
            if (chDownload.Checked)
            {
                // Validate file name
                string titlevalid = string.Concat(title.Split(Path.GetInvalidFileNameChars()));
                if (titlevalid.Length > 50) titlevalid = titlevalid.Substring(0, 50);

                saveFileDialog1.DefaultExt = "html";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.FileName = titlevalid + ".html";
                saveFileDialog1.Filter = "Webpage | *.html";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                link = saveFileDialog1.FileName;
                htmlDoc.Save(link);
            }

            SaveNewLink(title, link);

            lblResult.Text = Utils.getString("NewLinkDlg.lblResult");
            if (!newLink)
                lblResult.Text = Utils.getString("NewLinkDlg.lblResult.modify");
            lblResult.Visible = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            lblResult.Visible = false;
        }

        void SaveNewLink(string title, string link)
        {
            bool fromLinksDlg = from == "LinksDlg";

            int groupID = (cbLinkGroup.SelectedItem as LinkGroupItem).ID;
            string type = Utils.GetFileType(link);
            string comment = "";
            if (txtComment.Text != "" && txtComment.Text != Utils.getString("NewLinkDlg.txtComment"))
                comment = txtComment.Text;

            using (StixDB _db = new StixDB())
            {
                DataTable dt = _db.ExecuteQuery("select * from LINKS " +
                    "where groupID=" + groupID + " and path=`" + link + "`");

                if (newLink) // Add new link
                {
                    if (dt.Rows.Count > 0) return; // to do message to user
                    if (groupID == 0) return;

                    //if (fromLinksDlg)
                    {
                        LinksDialog.AddToTable(title, link, LinksDialog.selectedNode.Text, comment, groupID);
                        LinksDialog.txtComment.Text = comment;
                    }

                    _db.AddLink(title, link, type, "", comment, groupID);
                }
                else if (fromLinksDlg) // Edit link
                {
                    var tableRow = LinksDialog.dataGridView1.SelectedRows[0];

                    int oldGroupID = (int)tableRow.Cells["GroupID"].Value;

                    if (oldGroupID != groupID && LinksDialog.selectedNode.Index != 0) // group changed!
                    {
                        // Remove row if it is not in the "All Links" group
                        LinksDialog.dataGridView1.Rows.Remove(tableRow);
                    }
                    else // Modify row in the table
                    {
                        // If link is in the "All Links" group, modify "Group" cell
                        if (LinksDialog.selectedNode.Index == 0)
                            tableRow.Cells["LinkGroup"].Value = cbLinkGroup.Text;

                        tableRow.Cells["LinkTitle"].Value = title;
                        tableRow.Cells["LinkPath"].Value = link;
                        tableRow.Cells["Comment"].Value = comment;
                        LinksDialog.txtComment.Text = comment;
                    }

                    // Update in the database
                    _db.ExecuteNonQuery("update LINKS set " +
                        "title=`" + title + "`, path=`" + link + "`, comment=`" + comment + "`, groupID=" + groupID +
                        " where groupID=" + oldGroupID + " and path=`" + link + "`");
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txtLink.Text = openFileDialog1.FileName;

                grBoxDownload.Visible = false;
                string title = "";
                try 
                { 
                    title = Path.GetFileName(txtLink.Text); 
                    txtTitle.Text = title; 
                }
                catch { }
            }
        }

        private void txtComment_Enter(object sender, EventArgs e)
        {
            if (txtComment.Text == Utils.getString("NewLinkDlg.txtComment"))
            {
                txtComment.Text = "";
                txtComment.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtComment_Leave(object sender, EventArgs e)
        {
            if (txtComment.Text == "")
            {
                txtComment.Text = Utils.getString("NewLinkDlg.txtComment");
                txtComment.ForeColor = SystemColors.GrayText;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public BrowserDlg OmniBrowser;
        public LinksDlg LinksDialog;
        public string from;

        public bool newLink;
        public int groupID;
        string baseUrl = "";

        HtmlAgilityPack.HtmlDocument htmlDoc = null;
    }

    internal class LinkGroupItem : Object
    {
        public LinkGroupItem(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID;
        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
