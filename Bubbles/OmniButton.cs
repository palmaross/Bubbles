using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;

namespace Bubbles
{
    public partial class OmniButton : Form
    {
        private Timer drawTimer = new Timer();

        public OmniButton()
        {
            InitializeComponent();

            thisHeight = this.Height;

            //this.MouseDown += Rounded_MouseDown;
            this.MouseHover += Rounded_MouseHover;
            OmniStix.MouseHover += Rounded_MouseHover;
            OmniStix.BackColor = ColorTranslator.FromHtml("#69a642");
        }

        private void Rounded_MouseHover(object sender, EventArgs e)
        {
            if (StixMain.m_StixBase.Visible) { return; }

            int X = this.Location.X - ((StixMain.m_StixBase.Width - this.Width) / 2);
            StixMain.m_StixBase.Location = new Point(X, this.Location.Y - StixMain.m_StixBase.Height);
            StixMain.m_StixBase.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            X = StixMain.m_StixBase.Location.X;
            int Y = StixMain.m_StixBase.Location.Y;

            do
            {
                Y += 4;
                StixMain.m_StixBase.Location = new Point(X, Y);
                StixMain.m_StixBase.Refresh();
            }
            while (Y < this.Location.Y);

            OmniButtonHovered = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            OmniButtonHovered = false;
        }
        public bool OmniButtonHovered = false;

        private void Rounded_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Height = thisHeight;
            DrawForm(null, null);

            if (!DesignMode)
            {
                drawTimer.Interval = 1000;
                drawTimer.Tick += DrawForm;
                drawTimer.Start();
            }
            base.OnLoad(e);
        }
        int thisHeight; bool start = true;

        public void Destroy()
        {
            drawTimer.Stop();
            drawTimer.Tick -= DrawForm;
            drawTimer.Dispose(); drawTimer = null;
        }

        private void DrawForm(object pSender, EventArgs pE)
        {
            // Check if MM position is changed

            // Get screen (location of the screen where the center of MindManager is located)
            Point rec = Utils.MMScreen(MMUtils.MindManager.Left + MMUtils.MindManager.Width / 2,
                MMUtils.MindManager.Top + MMUtils.MindManager.Height / 2);

            int X = Utils.OmniButtonX, Y = MMUtils.MindManager.Top, W = MMUtils.MindManager.Width;
            if (X == 0) X = MMUtils.MindManager.Left + (int)(W / 3 * 1.85);

            if (Y < rec.Y) Y = rec.Y; // correct MindManager top position when MM is maximized

            if (this.Location.X != X || this.Location.Y != Y)
                this.Location = new Point(X, Y);

            // Check if the Resource group is changed
            if (StixMain.m_Resources != null && StixMain.m_Resources.Visible)
            {
                MapMarkerGroup mg = MMUtils.ActiveDocument.MapMarkerGroups.GetMandatoryMarkerGroup(MmMapMarkerGroupType.mmMapMarkerGroupTypeResource);
                List<string> MapResources = new List<string>();
                MapResources.AddRange(StixMain.m_Resources.MapResources.Keys);

                foreach (MapMarker mm in mg)
                {
                    if (MapResources.Contains(mm.Label))
                    {
                        // Check if the resource color is right
                        string color = "#" + mm.Color.Value.ToString("X");
                        if (color == "#0") color = "";

                        if (color == StixMain.m_Resources.MapResources[mm.Label])
                        {
                            MapResources.Remove(mm.Label); continue;
                        }

                        StixMain.m_Resources.InitCurrentMapResources(); break;
                    }
                    else // Add resource to StixButton.m_Resources
                    {
                        StixMain.m_Resources.InitCurrentMapResources(); break;
                    }
                }

                if (MapResources.Count > 0) { StixMain.m_Resources.InitCurrentMapResources(); }
            }

            // Start the OmniStix button (the first time only)
            //if (start)
            {
                start = false;
                Radius.Visible = false;
                Color c = ColorTranslator.FromHtml("#69a642");
                Radius.BackColor = c;

                using (Bitmap backImage = new Bitmap(this.Width, this.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(backImage))
                    {
                        Rectangle gradientRectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                        using (Brush b = new SolidBrush(c))
                        {
                            graphics.SmoothingMode = SmoothingMode.HighQuality;

                            RoundedRectangle.FillRoundedRectangle(graphics, b, gradientRectangle, Radius.Width);

                            foreach (Control ctrl in this.Controls)
                            {
                                using (Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height))
                                {
                                    Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
                                    ctrl.DrawToBitmap(bmp, rect);
                                    graphics.DrawImage(bmp, ctrl.Location);
                                }
                            }

                            PerPixelAlphaBlend.SetBitmap(backImage, Left, Top, Handle);
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                Graphics graphics = e.Graphics;

                Rectangle gradientRectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

                Brush b = new LinearGradientBrush(gradientRectangle, Color.DarkSlateBlue, Color.MediumPurple, 0.0f);

                graphics.SmoothingMode = SmoothingMode.HighQuality;

                RoundedRectangle.FillRoundedRectangle(graphics, b, gradientRectangle, 20);
            }

            base.OnPaint(e);
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.ExStyle |= 0x00080000;
                }
                return cp;
            }
        }

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public static class RoundedRectangle
    {
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            Rectangle arc1 = new Rectangle(bounds.Location, new Size(1, 1));
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc1, 180, 90);

            // top right arc  
            arc.X = bounds.Right;
            Rectangle arc2 = new Rectangle(arc.Location, new Size(1, 1));
            path.AddArc(arc2, 270, 90);

            // bottom right arc  
            arc.X = bounds.Right - diameter;
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        public static void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }
    }

    internal static class PerPixelAlphaBlend
    {
        public static void SetBitmap(Bitmap bitmap, int left, int top, IntPtr handle)
        {
            SetBitmap(bitmap, 255, left, top, handle);
        }

        public static void SetBitmap(Bitmap bitmap, byte opacity, int left, int top, IntPtr handle)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");


            IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
            IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = Win32.SelectObject(memDc, hBitmap);

                Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.Point pointSource = new Win32.Point(0, 0);
                Win32.Point topPos = new Win32.Point(left, top);
                Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
                blend.BlendOp = Win32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = Win32.AC_SRC_ALPHA;

                Win32.UpdateLayeredWindow(handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.ULW_ALPHA);
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBitmap);
                    Win32.DeleteObject(hBitmap);
                }

                Win32.DeleteDC(memDc);
            }
        }
    }

    internal class Win32
    {
        public enum Bool
        {
            False = 0,
            True
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y) { this.x = x; this.y = y; }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }


        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;


        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);
    }
}
