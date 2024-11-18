using System.Windows.Forms;

namespace Bubbles
{
    internal class TreeViewNonHSB : TreeView
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x8000; // TVS_NOHSCROLL
                cp.Style |= 0x80;  // Turn on TVS_NOTOOLTIPS
                return cp;
            }
        }
    }
}
