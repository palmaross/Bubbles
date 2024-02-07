using System;
using System.Linq;
using System.Windows.Forms;
using PRAManager;
using Mindjet.MindManager.Interop;
using System.Runtime.InteropServices;
using WindowsInput.Native;
using WindowsInput;
using System.Drawing;
using System.Collections.Generic;

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

            toolTip1.SetToolTip(subtopic, Utils.getString("BubblesPaste.pastesubtopic"));
            toolTip1.SetToolTip(pPaste, Utils.getString("BubblesPaste.pPaste.tooltip"));
            toolTip1.SetToolTip(pCopy, Utils.getString("BubblesPaste.pCopy.tooltip"));
            toolTip1.SetToolTip(PasteLink, Utils.getString("BubblesPaste.PasteLink.tooltip"));
            toolTip1.SetToolTip(PasteNotes, Utils.getString("BubblesPaste.AddNotes.tooltip"));
            toolTip1.SetToolTip(ToggleTextFormat, Utils.getString("BubblesPaste.workwith.unformatted"));
            toolTip1.SetToolTip(UnformatText, Utils.getString("BubblesPaste.unformate.tooltip"));

            toolTip1.SetToolTip(pictureHandle, stickname);

            cmsPasteText.ItemClicked += ContextMenu_ItemClicked;
            cmsCommon.ItemClicked += ContextMenu_ItemClicked;

            cmsPasteText.Items["CP_copy_unformatted"].Text = Utils.getString("paste.contextmenu.copy_unformatted");
            cmsPasteText.Items["CP_copy_formatted"].Text = Utils.getString("paste.contextmenu.copy_formatted");
            cmsPasteText.Items["CP_paste_unformatted"].Text = Utils.getString("paste.contextmenu.paste_unformatted");
            cmsPasteText.Items["CP_paste_formatted"].Text = Utils.getString("paste.contextmenu.paste_formatted");

            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typepaste);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste);

            if (collapsed) {
                collapsed = false; Collapse(); }
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
            if (e.ClickedItem.Name == "CP_copy_unformatted")
            {
                if (MMUtils.ActiveDocument != null)
                {
                    string text = "";
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        text += t.Text;

                    if (text != "")
                        System.Windows.Forms.Clipboard.SetText(text);
                }
            }
            else if(e.ClickedItem.Name == "CP_copy_formatted")
            {
                string text = "";
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    text += t.Title.TextRTF + "\r\n";

                if (text != "")
                {
                    DataObject dto = new DataObject();
                    dto.SetText(text, TextDataFormat.Rtf);
                    dto.SetText(text, TextDataFormat.UnicodeText);
                    System.Windows.Forms.Clipboard.Clear();
                    System.Windows.Forms.Clipboard.SetDataObject(dto);
                }
            }
            if (e.ClickedItem.Name == "CP_paste_unformatted")
            {
                var text = System.Windows.Forms.Clipboard.GetData(DataFormats.Rtf);
                if (MMUtils.ActiveDocument != null)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.Text = System.Windows.Forms.Clipboard.GetText();
                }
            }
            else if (e.ClickedItem.Name == "CP_paste_formatted")
            {

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

                collapseState = this.Location; // remember collapsed location
                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;
                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                    this.Location = collapseState; // restore collapsed location
            }
        }
        Point collapseState = new Point(0, 0);

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

        private void AddPasteTopic_MouseHover(object sender, EventArgs e)
        {
            //PictureBox pb = sender as PictureBox;

            //if (pb.Name == "pPasteTopic")
            //    StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "paste");
            //else
            //    StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "add");
        }

        private void pCopy_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StickUtils.ActivateMindManager();
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsPasteText.Items)
                    item.Visible = false;

                cmsPasteText.Items["CP_copy_unformatted"].Visible = true;
                cmsPasteText.Items["CP_copy_formatted"].Visible = true;

                cmsPasteText.Show(Cursor.Position);
            }
        }

        //private void pCut_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        StickUtils.ActivateMindManager();
        //        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_X);
        //    }
        //}

        private void pPaste_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StickUtils.ActivateMindManager();
                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsPasteText.Items)
                    item.Visible = false;

                cmsPasteText.Items["CP_paste_unformatted"].Visible = true;
                cmsPasteText.Items["CP_paste_formatted"].Visible = true;

                cmsPasteText.Show(Cursor.Position);
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

        private void PasteTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (MMUtils.ActiveDocument == null || 
                MMUtils.ActiveDocument.Selection.PrimaryTopic == null) 
                return;

            if (e.Button == MouseButtons.Left)
            {
                //PasteTopic((sender as PictureBox).Name);
                transTopic = MMUtils.ActiveDocument.Selection.PrimaryTopic;
                AddTopicTransaction(Utils.getString("addtopics.transactionname.insert"));
            }
            else if (e.Button == MouseButtons.Right)
            {
                StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste, "paste");
            }
        }

        private void ToggleTextFormat_Click(object sender, EventArgs e)
        {
            string tooltip = Utils.getString("BubblesPaste.workwith.formatted");

            if (ToggleTextFormat.Tag.ToString() == "formatted")
            {
                ToggleTextFormat.Tag = "unformatted";
                ToggleTextFormat.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "unformattedText.png");
                tooltip = Utils.getString("BubblesPaste.workwith.unformatted");
                toolTip1.SetToolTip(ToggleTextFormat, tooltip);
            }
            else
            {
                ToggleTextFormat.Tag = "formatted";
                ToggleTextFormat.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "formattedText.png");
                toolTip1.SetToolTip(ToggleTextFormat, tooltip);
            }
        }

        void AddTopicTransaction(string trname)
        {
            Transaction _tr = MMUtils.ActiveDocument.NewTransaction(trname);
            _tr.IsUndoable = true;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(PasteTopics);
            _tr.Start();
        }

        public void PasteTopics(Document pDocument)
        {
            if (TopicList.Count > 1 && transTopicType == "nexttopic")
                TopicList = TopicList.Reverse<string>().ToList();

            foreach (var name in TopicList)
                StickUtils.AddTopic(transTopic, transTopicType, name);
        }

        private void pReplace_Click(object sender, EventArgs e)
        {
            new ReplaceDlg().Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        Topic transTopic;
        string transTopicType;
        public List<string> TopicList = new List<string>();

        string orientation = "H";
        int RealLength;
        bool collapsed = false;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        InputSimulator sim = new InputSimulator();
    }
}
