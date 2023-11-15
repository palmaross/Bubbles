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
        public BubblePaste(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "PasteStick.htm");

            MinLength = panel2.Width;
            RealLength = this.Width;

            if (orientation == "V")
            {
                orientation = "H";
                RotateBubble();

                foreach (PictureBox p in this.Controls.OfType<PictureBox>())
                {
                    p.Location = new Point(copyTopicText.Location.Y, p.Location.X);
                }
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

            StickUtils.SetCommonContextMenu(contextMenuStrip1, p2, StickUtils.typepaste);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += PictureHandle_MouseDoubleClick;

            if (collapsed) {
                collapsed = false; Collapse(); }
        }

        private void PictureHandle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Collapse();
        }

        private string getString(string name)
        {
            return Utils.getString(name);
        }

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
            base.OnMouseDown(e);
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "BI_rotate")
            {
                RotateBubble();

                foreach (PictureBox p in this.Controls.OfType<PictureBox>())
                {
                    if (p.Name == "Manage") continue;
                    p.Location = new Point(p.Location.Y, p.Location.X);
                }
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "PasteStick.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StickUtils.SaveStick(this.Bounds, (int)this.Tag, orientation, collapsed);
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                Collapse();
            }
        }

        public void Rotate()
        {
            //orientation = StickUtils.RotateStick(this, p1, panel1, Manage, orientation);
        }

        /// <summary>
        /// Collapse/Expand stick
        /// </summary>
        /// <param name="CollapseAll">"Collapse All" command from Main Menu</param>
        /// <param name="ExpandAll">"Expand All" command from Main Menu</param>
        public void Collapse(bool CollapseAll = false, bool ExpandAll = false)
        {
            if (collapsed) // Expand stick
            {
                if (CollapseAll) return;

                StickUtils.Expand(this, RealLength, orientation);
                contextMenuStrip1.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.collapse");
                PasteLink.Visible = true;
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation);
                PasteLink.Visible = false;
                collapsed = true;
                contextMenuStrip1.Items["BI_collapse"].Text = Utils.getString("float_icons.contextmenu.expand");
            }
        }

        void RotateBubble()
        {
            if (orientation == "H")
            {
                orientation = "V";
                Manage.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            }
            else
            {
                orientation = "H";
                Manage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }

            int thisWidth = this.Width;
            int thisHeight = this.Height;

            Point ManageLocation = new Point(Manage.Location.Y, Manage.Location.X);
            this.Size = new Size(thisHeight, thisWidth);
            Manage.Location = ManageLocation;
        }

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
        int MinLength, RealLength;
        bool collapsed = false;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
