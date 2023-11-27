using System;
using System.Linq;
using System.Windows.Forms;
using PRAManager;
using Mindjet.MindManager.Interop;
using System.Drawing;
using Image = System.Drawing.Image;
using WindowsInput;
using WindowsInput.Native;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

            RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            toolTip1.SetToolTip(pPaste, getString("BubblesPaste.pPaste.tooltip"));
            toolTip1.SetToolTip(pCopy, getString("BubblesPaste.pCopy.tooltip"));
            toolTip1.SetToolTip(pPasteAsSubtopic, getString("BubblesPaste.addsubtopic.tooltip"));
            toolTip1.SetToolTip(PasteLink, getString("BubblesPaste.PasteLink.tooltip"));
            toolTip1.SetToolTip(PasteNotes, getString("BubblesPaste.AddNotes.tooltip"));
            toolTip1.SetToolTip(ToggleTextFormat, getString("BubblesPaste.workwith.unformatted"));
            toolTip1.SetToolTip(UnformatText, getString("BubblesPaste.unformate.tooltip"));

            //toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, stickname);

            cmsAddTopic.ItemClicked += ContextMenu_ItemClicked;
            cmsPasteText.ItemClicked += ContextMenu_ItemClicked;

            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typepaste);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste);
            this.MouseLeave += (sender, e) => StickUtils.HideCommandPopup(this, orientation);

            if (collapsed) {
                collapsed = false; Collapse(); }
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

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "CP_addnext")
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
            else if(e.ClickedItem.Name == "CP_addcallout")
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
            else if (e.ClickedItem.Name == "CP_unformatted")
            {
                if (MMUtils.ActiveDocument != null)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.Text = System.Windows.Forms.Clipboard.GetText();
                }
            }
            else if(e.ClickedItem.Name == "CP_atend")
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
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
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
            orientation = StickUtils.RotateStick(this, Manage, orientation);
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
                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;
                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;
            }
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
            string text = "";

            if (System.Windows.Forms.Clipboard.ContainsText())
                text = System.Windows.Forms.Clipboard.GetText();

            if (MMUtils.ActiveDocument != null && MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 0)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    if (text != "")
                    {
                        if (FormattedText())
                        {
                            ActivateMindManager();
                            Topic _t = t.AddSubTopic("");
                            _t.SelectOnly();
                            // select topic text
                            sim.Keyboard.KeyDown(VirtualKeyCode.F2);
                            // and replace it with the clipboard formatted text
                            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                        }
                        else
                            t.AddSubTopic(text);
                    }
                }
            }
        }

        bool ActivateMindManager()
        {
            Process p = Process.GetProcessesByName("MindManager").FirstOrDefault();
            if (p == null)
                return false;
            else
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                return true;
            }
        }

        bool FormattedText()
        {
            return this.ToggleTextFormat.Tag.ToString() == "formatted";
        }

        private void addsubtopic_MouseHover(object sender, EventArgs e)
        {
            //foreach (ToolStripItem item in cmsAddTopic.Items)
            //    item.Visible = true;

            //Point loc = new Point(
            //    addsubtopic.RectangleToScreen(addsubtopic.ClientRectangle).Left,
            //    addsubtopic.RectangleToScreen(addsubtopic.ClientRectangle).Bottom);
            //cmsAddTopic.Show(loc);

            StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "paste");
        }


        /// <summary>
        /// Paste general (Ctrl+V)
        /// </summary>
        private void pPaste_Click(object sender, EventArgs e)
        {
            
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

        private void ToggleTextFormat_Click(object sender, EventArgs e)
        {
            if (ToggleTextFormat.Tag.ToString() == "unformatted")
            {
                toolTip1.SetToolTip(ToggleTextFormat, getString("BubblesPaste.workwith.formatted"));
                ToggleTextFormat.Tag = "formatted";
                ToggleTextFormat.Image = Image.FromFile(Utils.ImagesPath + "formattedText.png");
            }
            else if (ToggleTextFormat.Tag.ToString() == "formatted")
            {
                toolTip1.SetToolTip(ToggleTextFormat, getString("BubblesPaste.workwith.unformatted"));
                ToggleTextFormat.Tag = "unformatted";
                ToggleTextFormat.Image = Image.FromFile(Utils.ImagesPath + "unformattedText.png");
            }
        }

        private void UnformatText_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    string text = t.Text;
                    t.Text = text;
                }
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsCommon.Items)
                item.Visible = true;

            cmsCommon.Show(Cursor.Position);
        }

        string orientation = "H";
        int RealLength;
        bool collapsed = false;

        InputSimulator sim = new InputSimulator();

        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
