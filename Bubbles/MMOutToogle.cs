using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class MMOutToogle : Form
    {
        public MMOutToogle()
        {
            InitializeComponent();

            cms.ItemClicked += ContextMenu_ItemClicked;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == CM_Close)
            {
                this.Hide();
            }
        }

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
                base.OnMouseDown(e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cms.Items)
                    item.Visible = true;

                cms.Show(Cursor.Position);
                return;
            }
        }

        private void pToggle_Click(object sender, EventArgs e)
        {
            if (pToggle.Tag.ToString() == "1")
            {
                pToggle.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                StickUtils.TopicAutoWidth = false;
            }
            else
            {
                pToggle.Image = Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");
                StickUtils.TopicAutoWidth = true;
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
}
