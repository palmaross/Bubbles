using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public class MyToolTip : ToolTip
    {
        public MyToolTip()
        {
            this.OwnerDraw = true;
            this.Popup += new PopupEventHandler(this.OnPopup);
            this.Draw += new DrawToolTipEventHandler(this.OnDraw);
            this.ShowAlways = true;
        }

        private void OnPopup(object sender, PopupEventArgs e) // use this event to set the size of the tool tip
        {
            e.ToolTipSize = new Size(e.ToolTipSize.Width + offset, e.ToolTipSize.Height + offset * 3);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e) // use this event to customise the tool tip
        {
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.WhiteSmoke, e.Bounds);

            g.DrawRectangle(new Pen(Brushes.Gray, 1), new Rectangle(e.Bounds.X, e.Bounds.Y,
                e.Bounds.Width - 1, e.Bounds.Height - 1));

            string[] text = e.ToolTipText.Split(new[] { '\n' }, 2);
            string[] tip = text[1].Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            g.DrawString(text[0], new Font(e.Font, FontStyle.Bold), Brushes.Black,
                new PointF(e.Bounds.X + offset, e.Bounds.Y + offset)); // Title
            if (tip.Length == 1)
            {
                g.DrawString(text[1], new Font(e.Font, FontStyle.Regular), Brushes.Black,
                    new PointF(e.Bounds.X + offset, e.Bounds.Y + offset * 8)); // Description
            }
            else
            {
                string offset2 = "\r\n\r\n\r\n";
                int count = tip[0].Count(f => f == '\n');
                for (int i = 0; i < count; i++) { offset2 += "\r\n"; }

                g.DrawString(tip[0], new Font(e.Font, FontStyle.Regular), Brushes.Black,
                    new PointF(e.Bounds.X + offset, e.Bounds.Y + offset * 8)); // Description
                g.DrawString(offset2 + tip[1], new Font(e.Font, FontStyle.Italic), Brushes.Black,
                    new PointF(e.Bounds.X + offset, e.Bounds.Y + offset * 2)); // Description
            }
        }

        public static int offset = 0;
    }
}
