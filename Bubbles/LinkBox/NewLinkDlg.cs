using HtmlAgilityPack;
using PRAManager;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class NewLinkDlg : Form
    {
        public NewLinkDlg()
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "NewLinkDlg.htm");

            Text = Utils.getString("NewLinkDlg.title");
            lblTitle.Text = Utils.getString("NewLinkDlg.lblTitle");
            lblLink.Text = Utils.getString("NewLinkDlg.lblLink");
            lblWait.Text = Utils.getString("NewLinkDlg.lblWait");
            cbDownload.Text = Utils.getString("NewLinkDlg.cbDownload");
            lblLinkGroup.Text = Utils.getString("NewLinkDlg.lblLinkGroup");
            grBoxDownload.Text = Utils.getString("NewLinkDlg.grBoxDownload");
            cbDownload.Text = Utils.getString("NewLinkDlg.cbDownload");
            btnPreview.Text = Utils.getString("NewLinkDlg.btnPreview");
            btnClose.Text = Utils.getString("button.close");

            FillGroups();
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

        public void txtLink_KeyUp(object sender, KeyEventArgs e)
        {
            string link = txtLink.Text.Trim();
            if (link == "") return;
            string title = "";

            if (link.StartsWith("http"))
            {
                lblWait.Visible = true;
                HtmlWeb web = new HtmlWeb();
                htmlDoc = web.Load(link);

                title = htmlDoc.DocumentNode.SelectSingleNode("//head/title").InnerText;
                lblWait.Visible = false;

                // Add source url.
                HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//html/body");

                string html = bodyNode.InnerHtml;
                html = "<div style='border: 2px double red; padding: 6px 6px 0 6px;'><p><strong>Source:</strong> <a href='" + link + "' target='_blank'>" + title + "</a></p></div>" + html;
                bodyNode.InnerHtml = html;

                grBoxDownload.Visible = true;
            }
            else // file
            {
                grBoxDownload.Visible = false;

                try { title = Path.GetFileName(link); }
                catch { }
            }

            if (!String.IsNullOrEmpty(title))
                txtTitle.Text = title;

            if (e != null)
            {
                e.Handled = true; // to avoid the "ding" sound
                e.SuppressKeyPress = true;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (htmlDoc == null) return;

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

            OmniBrowser.Preview(filepath);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string link = txtLink.Text.Trim();
            string title = txtTitle.Text.Trim();

            if (link == "" || title == "") return; // to do message to user

            // Download webpage.
            if (cbDownload.Checked)
            {
                // Validate file name
                string titlevalid = string.Concat(title.Split(Path.GetInvalidFileNameChars()));
                if (titlevalid.Length > 50) titlevalid = titlevalid.Substring(0, 60);

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
        }

        void SaveNewLink(string title, string link)
        {
            bool fromLinksDlg = from == "LinksDlg";

            int groupID = (cbLinkGroup.SelectedItem as LinkGroupItem).ID;
            string type = Utils.GetFileType(link);

            using (StixDB _db = new StixDB())
            {
                DataTable dt = _db.ExecuteQuery("select * from LINKS " +
                    "where groupID=" + groupID + " and path=`" + link + "`");

                if (dt.Rows.Count > 0) return; // to do message to user

                if (newLink) // Add new link
                {
                    if (groupID == 0) return;

                    if (fromLinksDlg)
                        LinksDialog.AddToTable(title, link, LinksDialog.selectedNode.Text, groupID);

                    _db.AddLink(title, link, type, "", LinksDialog.txtComment.Text, groupID);
                }
                else if (fromLinksDlg) // Edit link
                {
                    if (groupID == 0) // "All Links" group selected. Get link group id from link itself
                        groupID = (int)LinksDialog.dataGridView1.SelectedRows[0].Cells["GroupID"].Value;

                    // Modify in the table
                    LinksDialog.dataGridView1.SelectedRows[0].Cells["LinkTitle"].Value = title;
                    LinksDialog.dataGridView1.SelectedRows[0].Cells["LinkPath"].Value = link;
                    txtLink.Text = link;

                    // Update in the database
                    _db.ExecuteNonQuery("update LINKS set" +
                        "title=`" + title + "`, path=`" + link + "`");
                }
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
