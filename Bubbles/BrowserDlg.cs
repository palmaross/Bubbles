using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class BrowserDlg : Form
    {
        public BrowserDlg(string path)
        {
            InitializeComponent();

            tabControl1.MouseClick += TabControl1_MouseClick;

            NewPage = tabControl1.TabPages[0];

            string uriAdd = "about:blank";
            BrowserTab bt = new BrowserTab(tabControl1, uriAdd) 
                { Source = new Uri(uriAdd), NewPage = NewPage, path = uriAdd };

            NewPage.Controls.Add(bt);
            NewPage.Tag = bt;

            uriAdd = path;
            tabControl1.TabPages.Add("NoTitle");

            bt = new BrowserTab(tabControl1, uriAdd)
                { Source = new Uri(uriAdd), NewPage = NewPage, path = uriAdd };

            tabControl1.TabPages[1].Controls.Add(bt);
            tabControl1.TabPages[1].Tag = bt;

            tabControl1.TabPages.Remove(NewPage);
            tabControl1.TabPages.Add(NewPage);
        }

        /// <summary>
        /// Show tab context menu. Get clicked tab.
        /// </summary>
        private void TabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsTab.Items)
                    item.Visible = true;

                cmsTab.Show(Cursor.Position);

                Point ee = new Point(e.Location.X, e.Location.Y);
                for (int i = 0; i < this.tabControl1.TabCount; i++)
                {
                    Rectangle r = this.tabControl1.GetTabRect(i);
                    if (r.Contains(ee))
                    {
                        TabToRemove = i;
                        break;
                    }
                }
            }
        }

        public void btnGo_Click(object sender, EventArgs e)
        {
            Navigate(false);
        }

        public void Navigate(bool newTab)
        {
            string uriAdd = txtAddressBar.Text;

            if (!newTab && tabControl1.SelectedTab != NewPage) // Open in the selected tab
            {
                TabPage _page = tabControl1.SelectedTab;
                BrowserTab bt = (BrowserTab)_page.Tag;
                bt.Source = new Uri(uriAdd);
            }
            else // Open in the new tab
            {
                tabControl1.TabPages.Add("NoTitle");
                TabPage page = tabControl1.TabPages[tabControl1.TabPages.Count - 1];

                BrowserTab bt = new BrowserTab(tabControl1, uriAdd)
                    { Source = new Uri(uriAdd), NewPage = NewPage, path = uriAdd };

                page.Tag = bt; page.Controls.Add(bt);

                tabControl1.TabPages.Remove(NewPage);
                tabControl1.TabPages.Add(NewPage);

                tabControl1.SelectedTab = page;

            }

            txtAddressBar.Text = uriAdd;
        }

        private async void InitializeWebView3Async()
        {
            TabPage _page = tabControl1.SelectedTab;
            BrowserTab bt = (BrowserTab)_page.Tag;
            selectedText = await bt.ExecuteScriptAsync("window.getSelection().toString()");
        }
        string selectedText;

        private void btnGetText_Click(object sender, EventArgs e)
        {
            InitializeWebView3Async();
        }

        private void tabRemove_Click(object sender, EventArgs e)
        {
            if (TabToRemove >= 0)
            {
                tabControl1.TabPages.RemoveAt(TabToRemove);
                TabToRemove = -1;
            }
        }

        int TabToRemove = -1;
        TabPage NewPage;

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == NewPage)
                txtAddressBar.Text = "";
            else
                txtAddressBar.Text = ((BrowserTab)tabControl1.SelectedTab.Tag).path;
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

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Draw the background of the control for each item.
                //e.DrawBackground();

                if (e.Index == this.tabControl1.SelectedIndex)
                {
                    Brush _BackBrush = new SolidBrush(Color.Ivory);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, (rect.X) + 8, rect.Y, (rect.Width) - 8, rect.Height);

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                               e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                               e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 2);
                }
                else if (e.Index == this.tabControl1.TabCount - 1) // New Page
                {
                    Brush _BackBrush = new SolidBrush(Color.Yellow);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, (rect.X) + 8, rect.Y, (rect.Width) - 8, rect.Height);

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                               e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                               e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 2);
                }
                else
                {
                    Brush _BackBrush = new SolidBrush(Color.Bisque);

                    Rectangle rect = e.Bounds;
                    e.Graphics.FillRectangle(_BackBrush, rect.X, (rect.Y) - 0, rect.Width, (rect.Height) + 12);

                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black,
                    e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2,
                              e.Bounds.Top + 10);

                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void pGoBack_Click(object sender, EventArgs e)
        {
            BrowserTab bt = (BrowserTab)tabControl1.SelectedTab.Tag;
            bt.GoBack();
        }

        private void pGoForward_Click(object sender, EventArgs e)
        {
            BrowserTab bt = (BrowserTab)tabControl1.SelectedTab.Tag;
            bt.GoForward();
        }
    }
}
