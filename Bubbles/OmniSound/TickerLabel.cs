using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class TickerLabel : Label
    {
        Timer timer;
        public TickerLabel()
        {
            DoubleBuffered = true;

            timer = new Timer() { Interval = 100 };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        int? left;
        int textWidth = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (textWidth < Width) return; // Text width is less than label width. No heed to scroll.

            if (RightToLeft == RightToLeft.Yes)
            {
                left += 3;
                if (left > Width)
                    left = -textWidth;
            }
            else
            {
                left -= 4;
                if (left < -textWidth)// || left < -(textWidth - Width + Height))
                    left = Height;
                if (left < -(textWidth - Width + Height))
                    left -= 9;

            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            var s = TextRenderer.MeasureText(Text, Font, new Size(0, 0),
                TextFormatFlags.TextBoxControl | TextFormatFlags.SingleLine);
            textWidth = s.Width;
            if (!left.HasValue) left = Height;
            var format = TextFormatFlags.TextBoxControl | TextFormatFlags.SingleLine |
                TextFormatFlags.VerticalCenter;
            if (RightToLeft == RightToLeft.Yes)
            {
                format |= TextFormatFlags.RightToLeft;
                if (!left.HasValue) left = -textWidth;
            }
            TextRenderer.DrawText(e.Graphics, Text, Font,
                new Rectangle(left.Value, 0, textWidth, Height),
                ForeColor, BackColor, format);
        }
    }
}
