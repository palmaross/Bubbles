using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PRAManager;
using Mindjet.MindManager.Interop;

namespace Bubbles
{
    internal partial class BubblePaste : Form
    {
        public BubblePaste()
        {
            InitializeComponent();

            //helpProvider1.HelpNamespace = MMMapNavigator.dllPath + "MapNavigator.chm";
            //helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            //helpProvider1.SetHelpKeyword(this, "bookmarks_express.htm");

            orientation = Utils.getRegistry("OrientationPaste", "H");

            ThisLength = this.Width;
            Thickness = this.Height;

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            if (orientation == "V")
            {
                orientation = "H";
                RotateBubble();

                foreach (PictureBox p in this.Controls.OfType<PictureBox>())
                    p.Location = new Point(p.Location.Y, p.Location.X);
            }

            toolTip1.SetToolTip(PasteLink, getString("BubblesPaste.PasteLink.tooltip"));
            toolTip1.SetToolTip(PasteNotes, getString("BubblesPaste.AddNotes.tooltip"));
            toolTip1.SetToolTip(PasteCallout, getString("BubblesPaste.pasteascallout.tooltip"));
            toolTip1.SetToolTip(addtopic, getString("BubblesPaste.addtopic.tooltip"));
            toolTip1.SetToolTip(addsubtopic, getString("BubblesPaste.addsubtopic.tooltip"));
            toolTip1.SetToolTip(addToTopic, getString("BubblesPaste.addtotopic.tooltip"));
            toolTip1.SetToolTip(pasteToTopic, getString("BubblesPaste.pastetotopic.tooltip"));
            toolTip1.SetToolTip(copyTopicText, getString("BubblesPaste.copyTopicText.tooltip"));
            toolTip1.SetToolTip(UnformatText, getString("BubblesPaste.unformate.tooltip"));

            toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, Utils.getString("copypaste.bubble.tooltip"));

            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            contextMenuStrip1.Items["BI_rotate"].Text = MMUtils.getString("float_icons.contextmenu.rotate");
            contextMenuStrip1.Items["BI_close"].Text = MMUtils.getString("float_icons.contextmenu.close");
            contextMenuStrip1.Items["BI_help"].Text = MMUtils.getString("float_icons.contextmenu.help");
            contextMenuStrip1.Items["BI_store"].Text = MMUtils.getString("float_icons.contextmenu.settings");

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
        }

        private string getString(string name)
        {
            return Utils.getString(name);
        }

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_rotate")
            {
                RotateBubble();

                foreach (PictureBox p in this.Controls.OfType<PictureBox>())
                    p.Location = new Point(p.Location.Y, p.Location.X);
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                this.Hide();
                BubblesButton.m_bubblesMenu.Paste.Image = BubblesButton.m_bubblesMenu.mwCopyPaste;
            }
            else if (e.ClickedItem.Name == "BI_help")
            {

            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                int x = this.Location.X;
                int y = this.Location.Y;

                Utils.setRegistry("OrientationPaste", orientation);
                Utils.setRegistry("PositionPaste", x.ToString() + "," + y.ToString());
            }
        }
        void RotateBubble()
        {
            if (orientation == "H")
                orientation = "V";
            else
                orientation = "H";

            int thisWidth = this.Width;
            int thisHeight = this.Height;

            if (orientation == "H")
            {
                this.MinimumSize = new Size(ThisLength, Thickness);
                this.MaximumSize = new Size(ThisLength, Thickness);
            }
            else
            {
                this.MinimumSize = new Size(Thickness, ThisLength);
                this.MaximumSize = new Size(Thickness, ThisLength);
            }

            this.Size = new Size(thisHeight, thisWidth);
        }

        #region resize dialog
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
        #endregion

        private void PasteLink_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null &&
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0 &&
                System.Windows.Forms.Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Hyperlinks.AddHyperlink(System.Windows.Forms.Clipboard.GetText());
                }
            }
        }

        private void PasteNotes_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null &&
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0 &&
                System.Windows.Forms.Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Notes.CursorPosition = -1;
                    t.Notes.Insert(System.Windows.Forms.Clipboard.GetText());
                    t.Notes.Commit();
                }
            }
        }

        private void addtopic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null &&
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0 &&
                System.Windows.Forms.Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    int i = 2;
                    foreach (Topic _t in t.ParentTopic.AllSubTopics)
                    {
                        if (_t == t)
                            break;
                        i++;
                    }

                    Topic newTopic = t.ParentTopic.AddSubTopic(System.Windows.Forms.Clipboard.GetText());
                    t.ParentTopic.AllSubTopics.Insert(newTopic, i);
                }
            }
        }

        private void addsubtopic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null &&
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0 &&
                System.Windows.Forms.Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.AddSubTopic(System.Windows.Forms.Clipboard.GetText());
                }
            }
        }

        private void addtotopic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null &&
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0 &&
                System.Windows.Forms.Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Text = t.Text + " " + (System.Windows.Forms.Clipboard.GetText());
                }
            }
        }

        private void callout_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null &&
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0 &&
                System.Windows.Forms.Clipboard.ContainsText())
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.AllCalloutTopics.Add().Text = System.Windows.Forms.Clipboard.GetText();
                }
            }
        }

        private void copyTopicText_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null)
            {
                string text = "";
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    text += t.Text;
                }

                if (text != "")
                    System.Windows.Forms.Clipboard.SetText(text);
            }
        }

        private void pasteToTopic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.Text = System.Windows.Forms.Clipboard.GetText();
            }
        }

        private void UnformatText_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    //SendKeys.SendWait("^{SPACE}");
                    t.TextColor.SetAutomatic();
                    t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagSize);
                    t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagItalic);
                    t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagBold);
                    t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagName);
                    t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagStrikethrough);
                    t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagUnderline);
                }
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            contextMenuStrip1.Show(Cursor.Position);
        }

        string orientation = "H";
        int ThisLength, Thickness;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
