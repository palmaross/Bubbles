using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace Bubbles
{
    public partial class BrowserDlg : Form
    {
        public BrowserDlg(string path)
        {
            InitializeComponent();

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "OmniBrowser.htm");

            Text = Utils.getString("BrowserDlg.title");
            btnAddSubtopic.Text = Utils.getString("BrowserDlg.btnAddSubtopic");
            toolTip1.SetToolTip(btnAddSubtopic, Utils.getString("BrowserDlg.btnAddSubtopic.tooltip"));
            btnAddNotes.Text = Utils.getString("BrowserDlg.btnAddNotes");
            toolTip1.SetToolTip(btnAddNotes, Utils.getString("BrowserDlg.btnAddSubtopic.tooltip"));
            btnClose.Text = Utils.getString("button.close");

            tabRemove.Text = Utils.getString("BrowserDlg.RemoveTab");

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            tabControl1.MouseClick += TabControl1_MouseClick;

            // There is a "New" tab preinstalled. Save it.
            tabControl1.TabPages[0].Text = Utils.getString("BrowserDlg.NewPage");
            NewPage = tabControl1.TabPages[0];

            // New page is a blank page. 
            string uriAdd = "about:blank";
            BrowserTab bt = new BrowserTab(tabControl1, uriAdd) 
                { Source = new Uri(uriAdd), NewPage = NewPage, path = uriAdd };

            // Add New page to the tab.
            NewPage.Controls.Add(bt);
            NewPage.Tag = bt;

            if (path != "")
            {
                txtAddressBar.Text = path;
                Navigate(true);
            }
        }

        private void pHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "OmniBrowser.htm");
        }

        /// <summary>
        /// Show tab context menu. Get clicked tab.
        /// </summary>
        private void TabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point ee = new Point(e.Location.X, e.Location.Y);
                for (int i = 0; i < this.tabControl1.TabCount; i++)
                {
                    Rectangle r = this.tabControl1.GetTabRect(i);
                    if (r.Contains(ee))
                    {
                        ClickedTab = i;
                        break;
                    }
                }

                foreach (ToolStripItem item in cmsTab.Items)
                    item.Visible = true;

                if (tabControl1.TabPages[ClickedTab] == NewPage)
                    tabRemove.Visible = false;

                cmsTab.Show(Cursor.Position);
            }
        }

        public void Preview(string url)
        {
            if (PreviewPage == null)
            {
                string uriAdd = url;
                PreviewPage = new TabPage("Preview");
                PreviewPage.Name = "PreviewPage";

                BrowserTab bt = new BrowserTab(tabControl1, uriAdd)
                { Source = new Uri(uriAdd), NewPage = NewPage, path = uriAdd };
                PreviewPage.Tag = bt; PreviewPage.Controls.Add(bt);
            }

            if (!tabControl1.Contains(PreviewPage))
            {
                tabControl1.TabPages.Add(PreviewPage);
                tabControl1.TabPages.Remove(NewPage);
                tabControl1.TabPages.Add(NewPage);
            }

            tabControl1.SelectedTab = PreviewPage;
            Navigate(false, url);
        }

        private void txtAddressBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtAddressBar.SelectAll();
        }

        private void txtAddressBar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Navigate(false);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Navigate url presented in the address bar.
        /// </summary>
        /// <param name="newTab">If true, open page in the new tab.</param>
        public void Navigate(bool newTab, string previewPath = "")
        {
            string uriAdd = txtAddressBar.Text;
            if (previewPath != "")
            {
                txtAddressBar.Text = "Preview";
                uriAdd = previewPath;
            }

            if (!newTab && tabControl1.SelectedTab != NewPage) // Open in the selected tab
            {
                TabPage _page = tabControl1.SelectedTab;
                BrowserTab bt = (BrowserTab)_page.Tag;
                bt.Source = new Uri(uriAdd);
            }
            else // Open in the new tab
            {
                tabControl1.TabPages.Add(Utils.getString("BrowserDlg.NoTitle"));
                TabPage page = tabControl1.TabPages[tabControl1.TabPages.Count - 1];

                BrowserTab bt = new BrowserTab(tabControl1, uriAdd)
                    { Source = new Uri(uriAdd), NewPage = NewPage, path = uriAdd };

                page.Tag = bt; page.Controls.Add(bt);

                tabControl1.TabPages.Remove(NewPage);
                tabControl1.TabPages.Add(NewPage);

                tabControl1.SelectedTab = page;
            }

            //txtAddressBar.Text = uriAdd;
        }

        /// <summary>
        /// "Remove tab" context menu 
        /// </summary>
        private void tabRemove_Click(object sender, EventArgs e)
        {
            if (ClickedTab >= 0 && ClickedTab < tabControl1.TabCount - 1)
            {
                BrowserTab bt = tabControl1.TabPages[ClickedTab].Tag as BrowserTab;
                //bt.Source = new Uri("about:blank");
                bt.CoreWebView2.NavigateToString("<html>Unloaded.</html>");
                tabControl1.TabPages.RemoveAt(ClickedTab);
                ClickedTab = -1;
                tabControl1.Tag = "tab_removed";
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == NewPage)
            {
                txtAddressBar.Text = "";
                ClickedTab = -1; // to not remove the New Page tab
            }
            else
                txtAddressBar.Text = ((BrowserTab)tabControl1.SelectedTab.Tag).path;
        }

        private void btnSaveLink_Click(object sender, EventArgs e)
        {
            if (StixMain.m_NewLink == null || StixMain.m_NewLink.IsDisposed)
            {
                StixMain.m_NewLink = new NewLinkDlg();
                StixMain.m_NewLink.OmniBrowser = this;

                StixMain.m_NewLink.Location = new Point(this.Right, this.Top);
                Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;
                if (StixMain.m_NewLink.Right > area.Right) // close to the right
                    StixMain.m_NewLink.Location = new Point(this.Left - StixMain.m_NewLink.Width, this.Top);
                if (StixMain.m_NewLink.Left < area.Left) // close to the left
                    StixMain.m_NewLink.Location = new Point(this.Left, this.Top);


                StixMain.m_NewLink.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }

            StixMain.m_NewLink.from = "OmniBrowser";
            StixMain.m_NewLink.newLink = true;
            StixMain.m_NewLink.txtLink.Text = txtAddressBar.Text;
            StixMain.m_NewLink.txtTitle.Text = tabControl1.SelectedTab.Text;
            StixMain.m_NewLink.txtLink_KeyUp(null, null);
            StixMain.m_NewLink.cbLinkGroup.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAsSubtopic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
                return;

            selectedText = "";
            InitializeWebView3Async();
        }

        private void btnAddToTopicNotes_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
                return;

            selectedText = "";
            InitializeWebView3Async(true);
        }

        /// <summary>
        /// Get selected text from webpage
        /// </summary>
        private async void InitializeWebView3Async(bool notes = false)
        {
            TabPage _page = tabControl1.SelectedTab;
            BrowserTab bt = (BrowserTab)_page.Tag;
            selectedText = await bt.ExecuteScriptAsync("window.getSelection().toString()");

            // To copy formatted text to clipboard:
            //await bt.ExecuteScriptAsync("document.execCommand('copy', true, null)");

            if (selectedText != "" && selectedText != "\"\"")
            {
                selectedText = selectedText.Trim('\"');
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (notes)
                    {
                        string notestext = t.Notes.Text;
                        string newline = "";
                        if (!t.Notes.IsTextEmpty && !notestext.EndsWith("\r") && 
                            !t.Notes.Text.EndsWith("\n") && !t.Notes.Text.EndsWith("\r\n"))
                            newline = "\r\n";

                        t.Notes.Text = notestext + newline + selectedText;
                        t.Notes.Commit();
                    }
                    else
                    {
                        t.AddSubTopic(selectedText);
                    }
                }
            }
        }

        private void pGoBack_Click(object sender, EventArgs e)
        {
            BrowserTab bt = (BrowserTab)tabControl1.SelectedTab.Tag;
            if (bt.CanGoBack) bt.GoBack();
        }

        private void pGoForward_Click(object sender, EventArgs e)
        {
            BrowserTab bt = (BrowserTab)tabControl1.SelectedTab.Tag;
            if (bt.CanGoForward) bt.GoForward();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            int w = pDraw.Width, h = pDraw.Height;
            try
            {
                // Draw the background of the control for each item.
                //e.DrawBackground();

                
                if (tabControl1.TabPages[e.Index] == NewPage) // New Page tab
                {
                    Brush _BackBrush = new SolidBrush(Color.White);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, (rect.X) + w, rect.Y, (rect.Width) - w, rect.Height);

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                               e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                               e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + h);
                }
                else if (tabControl1.TabPages[e.Index] == PreviewPage) // Preview Page tab
                {
                    Brush _BackBrush = new SolidBrush(Color.Yellow);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, (rect.X) + w, rect.Y, (rect.Width) - w, rect.Height);

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                               e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                               e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + h);
                }
                // Active tab exept the New Page and Preview Page
                else if (e.Index == this.tabControl1.SelectedIndex) // Active tab.
                {
                    Brush _BackBrush = new SolidBrush(SystemColors.GradientInactiveCaption);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, (rect.X) + w, rect.Y, (rect.Width) - w, rect.Height);

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                               e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                               e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + h);
                }
                else // All the rest tabs
                {
                    Brush _BackBrush = new SolidBrush(Color.WhiteSmoke);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, rect.X, (rect.Y) - 0, rect.Width, (rect.Height) + (int)(w*1.5));

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                    e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                              e.Bounds.Top + w);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        int ClickedTab = -1;
        TabPage NewPage = null;
        TabPage PreviewPage = null;
        string selectedText = "";
    }
}
