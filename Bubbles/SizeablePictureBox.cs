using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Community.CsharpSqlite.Sqlite3;

namespace Bubbles
{
    public class SizeablePictureBox : PictureBox
    {
        public SizeablePictureBox()
        {
            this.ResizeRedraw = true;
            this.MouseEnter += SizeablePictureBox_MouseEnter;
            this.MouseLeave += SizeablePictureBox_MouseLeave;

            this.MouseUp += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (null == c) return;
                _dragging = false;
                _resizing = false;

                // Return the picture if it goes beyond the sticker
                if (c.Top < 0) c.Top = 0;
                if (c.Left < 0) c.Left = 0;
                if (c.Bottom > this.Parent.Height) c.Top = this.Parent.Height - c.Height;
                if (c.Right > this.Parent.Width) c.Left = this.Parent.Width - c.Width;

                int x = this.Width - _pWidth;
                int y = this.Height - _pHeight;

                float s = (float)this.Image.Width / this.Image.Height;
                if (x > y)
                    this.Width = (int)(this.Height * s);
                else
                    this.Height = (int)(this.Width / s);
            };

            this.MouseDown += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (args.Button == MouseButtons.Left)
                {
                    if (args.X > c.Width - 16 && args.X < c.Width &&
                    args.Y > c.Height - 16 && args.Y < c.Height)
                        _resizing = true;
                    else
                        _dragging = true;

                    _pWidth = c.Width; _pHeight = c.Height;
                    _xPos = args.X; _yPos = args.Y;
                }
                //else if (args.Button == MouseButtons.Right && pb == pStickerImage)
                //{
                //    foreach (ToolStripItem item in contextMenuOther.Items)
                //        item.Visible = false;

                //    contextMenuOther.Items["CM_DeleteImage"].Visible = true;
                //    contextMenuOther.Show(Cursor.Position);
                //}
            };

            this.MouseMove += (sender, args) =>
            {
                var c = sender as PictureBox;
                if ((!_dragging && !_resizing) || null == c)
                {
                    Point mouse = args.Location;
                    if (mouse.X > c.Width - 16 && mouse.Y > c.Height - 16)
                        c.Cursor = Cursors.SizeNWSE;
                    else
                        c.Cursor = Cursors.Default;
                    return;
                }

                // Don't let the picture go beyond the sticker
                if (c.Top < 0) c.Top = 0;
                else if (c.Left < 0) c.Left = 0;
                else if (c.Bottom > this.Parent.Height) c.Top = this.Parent.Height - c.Height;
                else if (c.Right > this.Parent.Width) c.Left = this.Parent.Width - c.Width;
                else
                {
                    if (_resizing)
                    {
                        int x = args.X - _xPos;
                        int y = args.Y - _yPos;
                        c.Width = _pWidth + x;
                        c.Height = _pHeight + y;
                    }
                    else // dragging
                    {
                        c.Top = args.Y + c.Top - _yPos;
                        c.Left = c.Left + args.X - _xPos;
                    }
                }
            };
        }

        private void SizeablePictureBox_MouseEnter(object sender, EventArgs e)
        {
            resize = true;
            Invalidate();
        }
        bool resize = false;

        private void SizeablePictureBox_MouseLeave(object sender, EventArgs e)
        {
            resize = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (resize)
            {
                var rc = new Rectangle(this.ClientSize.Width - grab, this.ClientSize.Height - grab, grab, grab);
                ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            }
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var cp = base.CreateParams;
        //        cp.Style |= 0x840000;  // Turn on WS_BORDER + WS_THICKFRAME
        //        return cp;
        //    }
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == 0x84)
        //    {  // Trap WM_NCHITTEST
        //        var pos = this.PointToClient(new Point(m.LParam.ToInt32()));
        //        Size s = this.ClientSize;
        //        if (pos.X >= this.ClientSize.Width - grab && pos.Y >= this.ClientSize.Height - grab)
        //            m.Result = new IntPtr(17);  // HT_BOTTOMRIGHT
        //    }
        //}

        // Global Variables for picture mover
        private int _xPos;
        private int _yPos;
        private bool _dragging;
        private bool _resizing;
        int _pWidth = 0;
        int _pHeight = 0;

        private const int grab = 16;
    }
}
