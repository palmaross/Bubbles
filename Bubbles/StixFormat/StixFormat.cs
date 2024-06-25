using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using WindowsInput.Native;
using WindowsInput;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Image = System.Drawing.Image;

namespace Bubbles
{
    internal partial class StixFormat : Form
    {
        public StixFormat(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation; // "H" or "V"

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "FormatStix.htm");

            lblTextColor.Text = Utils.getString("stixformat.lblTextColor");
            lblFillColor.Text = Utils.getString("stixformat.lblFillColor");

            toolTip1.SetToolTip(pictureHandle, stickname + Utils.getString("HeadIcon.tooltip"));
            toolTip1.SetToolTip(Manage, Utils.getString("ManageIcon.tooltip"));
            toolTip1.SetToolTip(pClearFormat, Utils.getString("stixformat.clearformat"));
            toolTip1.SetToolTip(pCloseFontSize, Utils.getString("stixformat.pCloseFontSize"));
            toolTip1.SetToolTip(numFontSize, Utils.getString("stixformat.numFontSize"));

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            BI_color.Text = Utils.getString("stixformat.contextmenu.color");
            clear_all.Text = Utils.getString("stixformat.contextmenu.clear_all");
            clear_textformat.Text = Utils.getString("stixformat.contextmenu.clear_textformat");
            clear_textcolor.Text = Utils.getString("stixformat.contextmenu.clear_textcolor");
            clear_fillcolor.Text = Utils.getString("stixformat.contextmenu.clear_fillcolor");

            StixUtils.SetCommonContextMenu(contextMenuStrip1, StixUtils.typeformat);

            fontcolor1.MouseClick += Icon_Click;
            fontcolor2.MouseClick += Icon_Click;
            fontcolor3.MouseClick += Icon_Click;
            fillcolor1.MouseClick += Icon_Click;
            fillcolor2.MouseClick += Icon_Click;
            fillcolor3.MouseClick += Icon_Click;

            fontcolor1.Tag = Utils.getRegistry("fontcolor1", "#ffff0000");
            fontcolor2.Tag = Utils.getRegistry("fontcolor2", "#ff0000ff");
            fontcolor3.Tag = Utils.getRegistry("fontcolor3", "#ff00aa55");
            fillcolor1.Tag = Utils.getRegistry("fillcolor1", "#ffffffa8");
            fillcolor2.Tag = Utils.getRegistry("fillcolor2", "#ffaeffae");
            fillcolor3.Tag = Utils.getRegistry("fillcolor3", "#ffb0ffff");

            fontcolor1.Paint += pVisualStatus_Paint;
            fontcolor2.Paint += pVisualStatus_Paint;
            fontcolor3.Paint += pVisualStatus_Paint;
            fillcolor1.Paint += pVisualStatus_Paint;
            fillcolor2.Paint += pVisualStatus_Paint;
            fillcolor3.Paint += pVisualStatus_Paint;

            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            Manage.Click += Manage_Click;

            // Apply scale factor
            this.Paint += this_Paint; // paint the border depending on scale factor

            fsize = lblTextColor.Font.Size;
            ffsize = numFontSize.Font.Size;
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);

            Bold = pBold.Image; Italic = pItalic.Image; Underline = pUnder.Image; Strikethrough = pStrike.Image;
            BoldA = Image.FromFile(Utils.m_imagesPath + "f_boldActive.png");
            ItalicA = Image.FromFile(Utils.m_imagesPath + "f_italicActive.png");
            UnderlineA = Image.FromFile(Utils.m_imagesPath + "f_underActive.png");
            StrikethroughA = Image.FromFile(Utils.m_imagesPath + "f_strikeActive.png");
        }
        
        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            scaleFactor = toScale;

            if (scale != 1)
                this.Scale(new SizeF(scale, scale)); // reset to 100%

            if (toScale != 100)
                this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale

            float _fsize = fsize * (toScale / 100);
            float _ffsize = ffsize * (toScale / 100);
            if (scaleFactor > 140) _ffsize = (float)(_ffsize * 1.05);

            lblTextColor.Font = new Font(lblTextColor.Font.FontFamily, _fsize);
            lblFillColor.Font = new Font(lblFillColor.Font.FontFamily, _fsize);
            numFontSize.Font = new Font(numFontSize.Font.FontFamily, _ffsize);
        }
        float fsize, ffsize;

        private void this_Paint(object sender, PaintEventArgs e)
        {
            if (scaleFactor < 125) return;
            int width = 1;
            //if (scaleFactor > 200) width = 2;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid);
        }

        private void pVisualStatus_Paint(object sender, PaintEventArgs e)
        {
            PictureBox p = sender as PictureBox;
            Color c = ColorTranslator.FromHtml(p.Tag.ToString());
            SolidBrush myBrush = new SolidBrush(c);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (p.Name.StartsWith("fill"))
                e.Graphics.FillRectangle(myBrush, p.ClientRectangle);
            else
                e.Graphics.FillEllipse(myBrush, p.ClientRectangle);
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = true;

            BI_color.Visible = false;
            clear_all.Visible = false;
            clear_textformat.Visible = false;
            clear_textcolor.Visible = false;
            clear_fillcolor.Visible = false;

            contextMenuStrip1.Show(Cursor.Position);
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
            if (e.ClickedItem.Name == "BI_color")
            {
                colorDialog1.FullOpen = true;
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                Color c = colorDialog1.Color;
                selectedIcon.Tag = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
                // "#ffffffff"

                Utils.setRegistry(selectedIcon.Name, selectedIcon.Tag.ToString());
                selectedIcon.Invalidate(); // change the picture color
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixMain.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem == clear_all)
            {
                if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                    return;

                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.SPACE);
                pBold.Image = Bold; pItalic.Image = Italic; pUnder.Image = Underline; pStrike.Image = Strikethrough;
            }
            else if (e.ClickedItem == clear_textformat)
            {
                if (MMUtils.ActiveDocument != null)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    {
                        t.Font.Bold = false; t.Font.Italic = false;
                        t.Font.Underline = false; t.Font.Strikethrough = false;
                        t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagSize);
                        t.Font.SetAttributeAutomatic((int)MmFontAttributeFlags.mmFontAttributeFlagName);
                    }
                    pBold.Image = Bold; pItalic.Image = Italic; pUnder.Image = Underline; pStrike.Image = Strikethrough;
                }
            }
            else if (e.ClickedItem == clear_textcolor)
            {
                if (MMUtils.ActiveDocument != null)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.TextColor.SetAutomatic();
                }
            }
            else if (e.ClickedItem == clear_fillcolor)
            {
                if (MMUtils.ActiveDocument != null)
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.FillColor.SetAutomatic();
                }
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixMain.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "FormatStix.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typeformat, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation);

            if (orientation == "H")
            {
                panelFontSize.Location = new Point(panelFontSize.Location.Y, pClearFormat.Location.Y);
            }
            else
            {
                panelFontSize.Location = new Point(0, panelFontSize.Location.X);
            }
        }

        /// <summary>
        /// Click on the text and fill color pictures
        /// </summary>
        private void Icon_Click(object sender, MouseEventArgs e)
        {
            selectedIcon = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null)
                    return;

                int value = Convert.ToInt32(selectedIcon.Tag.ToString().TrimStart('#'), 16);

                if (selectedIcon.Name.StartsWith("fill"))
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.FillColor.Value = value;
                }
                else
                {
                    foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        t.TextColor.Value = value;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                contextMenuStrip1.Items["BI_color"].Visible = true;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void pBold_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager() ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_B);

            if (pBold.Image == Bold) pBold.Image = BoldA;
            else pBold.Image = Bold;
        }

        public void pItalic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager() ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_I);

            if (pItalic.Image == Italic) pItalic.Image = ItalicA;
            else pItalic.Image = Italic;
        }

        private void pUnder_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager() ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_U);

            if (pUnder.Image == Underline) pUnder.Image = UnderlineA;
            else pUnder.Image = Underline;
        }

        private void pStrike_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager() ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                return;

            sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_S);
            
            if (pStrike.Image == Strikethrough) pStrike.Image = StrikethroughA;
            else pStrike.Image = Strikethrough;
        }

        private void pFontIncrease_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || !ActivateMindManager()) return;
                sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.OEM_PERIOD);
            }
            else if (e.Button == MouseButtons.Right)
            {
                panelFontSize.Visible = true;
            }
        }

        private void pFontDecrease_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || !ActivateMindManager()) return;
                sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.OEM_COMMA);
            }
            else if (e.Button == MouseButtons.Right)
            {
                panelFontSize.Visible = true;
            }
        }

        private void pCloseFontSize_Click(object sender, EventArgs e)
        {
            panelFontSize.Visible = false;
        }

        private void numFontSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                pCloseFontSize_Click(null, null);
            else if (e.KeyCode == Keys.Enter)
            {
                if (MMUtils.ActiveDocument == null || !ActivateMindManager()) return;

                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    t.Font.Size = (int)numFontSize.Value;
                    e.Handled = true; // to avoid the "ding" sound
                    e.SuppressKeyPress = true;
                    //e.KeyChar = (char)Keys.D2;
                    //button1.PerformClick();
                }
            }
        }

        private void pClearFormat_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                    return;

                sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.SPACE);

                // Clear buttons.
                pBold.Image = Bold; pItalic.Image = Italic; pUnder.Image = Underline; pStrike.Image = Strikethrough;
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                    item.Visible = false;

                clear_all.Visible = true;
                clear_textformat.Visible = true;
                clear_textcolor.Visible = true;
                clear_fillcolor.Visible = true;

                contextMenuStrip1.Show(Cursor.Position);
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

        PictureBox selectedIcon = null;
        string orientation = "H";

        public float scaleFactor = 100;

        public Image Bold, Italic, Underline, Strikethrough, BoldA, ItalicA, UnderlineA, StrikethroughA;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        InputSimulator sim = new InputSimulator();
    }
}
