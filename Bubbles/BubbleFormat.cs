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

namespace Bubbles
{
    internal partial class BubbleFormat : Form
    {
        public BubbleFormat(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "WowStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "StickFormat.htm");

            lblTextColor.Text = Utils.getString("BubbleFormat.lblTextColor");
            lblFillColor.Text = Utils.getString("BubbleFormat.lblFillColor");

            //toolTip1.SetToolTip(Manage, Utils.getString("bubble.manage.tooltip"));
            toolTip1.SetToolTip(pictureHandle, stickname);
            toolTip1.SetToolTip(pClearFormat, Utils.getString("bubbleformat.clearformat"));

            RealLength = this.Width;

            if (orientation == "V") {
                orientation = "H"; Rotate(); }

            // Resizing window causes black strips...
            //this.DoubleBuffered = true;
            //this.ResizeRedraw = true;

            // Context menu
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            contextMenuStrip1.Items["BI_color"].Text = Utils.getString("bubbleformat.contextmenu.color");
            StickUtils.SetCommonContextMenu(contextMenuStrip1, StickUtils.typeformat);

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

            if (collapsed) {
                collapsed = false; Collapse(); }

            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();
            pictureHandle.MouseDown += PictureHandle_MouseDown;
            Manage.Click += Manage_Click;

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typeformat);

            // Apply scale factor
            this.Paint += this_Paint; // paint the border depending on scale factor
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            if (scale != 1)
                this.Scale(new SizeF(scale, scale)); // reset to 100%

            this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale
            scaleFactor = toScale;
        }

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

            contextMenuStrip1.Items["BI_color"].Visible = false;
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
                BubblesButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "StickFormat.htm");
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
                collapseOrientation = orientation;
                StickUtils.Expand(this, RealLength, orientation, contextMenuStrip1);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, contextMenuStrip1);
                collapsed = true;
                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                {
                    this.Location = collapseState; // restore collapsed location
                    if (orientation != collapseOrientation) Rotate();
                }
            }
        }
        Point collapseState = new Point(0, 0);
        string collapseOrientation = "N";

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

        private void p1_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_B);
        }

        public void pItalic_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_I);
        }

        private void pUnder_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_U);
        }

        private void pStrike_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_S);
        }

        private void pFontIncrease_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.OEM_PERIOD);
        }

        private void pFontDecrease_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.OEM_COMMA);
        }

        private void pClearTextColor_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.TextColor.SetAutomatic();
            }
        }

        private void pClearFillColor_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument != null)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    t.FillColor.SetAutomatic();
            }
        }

        public void pClearFormat_Click(object sender, EventArgs e)
        {
            if (MMUtils.ActiveDocument == null || !ActivateMindManager())
                return;

            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.SPACE);
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
        bool collapsed = false;

        int RealLength;
        public float scaleFactor = 100;

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
