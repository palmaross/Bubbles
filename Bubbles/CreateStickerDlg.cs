using PRAManager;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class CreateStickerDlg : Form
    {
        public CreateStickerDlg()
        {
            InitializeComponent();

            lblText.Text = Utils.getString("CreateStickerDlg.lblText");
            lblBody.Text = Utils.getString("CreateStickerDlg.lblBody");
            lblStickName.Text = Utils.getString("CreateStickerDlg.lblStickName");
            Editor.Text = Utils.getString("CreateStickerDlg.Stick.Text");
            btnSave.Text = Utils.getString("button.save");
            btnApply.Text = Utils.getString("button.apply");
            btnClose.Text = Utils.getString("button.close");

            toolTip1.SetToolTip(lblStickName, Utils.getString("lblStickName.tooltip"));
            toolTip1.SetToolTip(txtTitle, Utils.getString("lblStickName.tooltip"));
            toolTip1.SetToolTip(btnSave, Utils.getString("btnSave.tooltip"));
            toolTip1.SetToolTip(btnApply, Utils.getString("btnApply.tooltip"));
            toolTip1.SetToolTip(btnClose, Utils.getString("btnCancel.tooltip"));

            txtTitle.Text = Utils.getString("CreateStickerDlg.txtTitle.initial");
            Editor.Text = Utils.getString("CreateStickerDlg.sticktext.initial");
            pBold.Image = Image.FromFile(Utils.ImagesPath + "f_boldActive.png");
            pBold.Tag = "Bold";
            Editor.ForeColor = ColorTranslator.FromHtml("#ff0000ff");
            Editor.BackColor = ColorTranslator.FromHtml("#ff00ffff");
            Editor.Font = new Font("Segoe Print", 14, FontStyle.Bold);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            panelTop.MouseDown += Title_MouseDown;

            Editor.SelectAll();
            Editor.SelectionAlignment = HorizontalAlignment.Center;

            fontcolor.Tag = "#ff0000ff";
            fontcolor.Paint += pVisualStatus_Paint;
            fillcolor.Tag = "#ff00ffff";
            fillcolor.Paint += pVisualStatus_Paint;

            cmbFonts.Items.Add("Verdana");
            cmbFonts.Items.Add("Comic Sans MS");
            cmbFonts.Items.Add("Comfortaa");
            cmbFonts.Items.Add("Impact");
            cmbFonts.Items.Add("Miama Nueva");
            cmbFonts.Items.Add("Pecita");
            cmbFonts.Items.Add("Segoe Print");

            cmbFonts.DrawItem += CmbFonts_DrawItem;
            cmbFonts.SelectedItem = "Segoe Print";
        }

        private void CmbFonts_DrawItem(object sender, DrawItemEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            try
            {
                FontFamily fontFamily = new FontFamily(comboBox.Items[e.Index].ToString());
            
            var font = new Font(fontFamily, comboBox.Font.Size);

            e.DrawBackground();
            e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
            }
            catch { }
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show(Utils.getString("createsticker.namefail"));
                return;
            }

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from STICKERS where name=`" + txtTitle.Text.Trim() + "`");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(Utils.getString("stickerdlg.nameexists"), 
                        Utils.getString("stickerdlg.nameexists.title"), 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //db.AddSticker(txtTitle.Text.Trim(), Editor.Text, fontcolor.Tag.ToString(), fillcolor.Tag.ToString(), Editor.Font.FontFamily.Name.ToString(),
                    //(int)Editor.Font.Size, Editor.Font.Bold ? 1 : 0, panelEditor.Width + ":" + panelEditor.Height, 0);
            }

            btnApply_Click(btnSave, null);
        }

        private void pVisualStatus_Paint(object sender, PaintEventArgs e)
        {
            PictureBox p = sender as PictureBox;
            Color c = ColorTranslator.FromHtml(p.Tag.ToString());
            SolidBrush myBrush = new SolidBrush(c);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillRectangle(myBrush, p.ClientRectangle);
        }

        private void fontcolor_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            Color c = colorDialog1.Color;
            fontcolor.Tag = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
            // "#ffffffff"

            fontcolor.Invalidate(); // change the picture color

            Editor.ForeColor = c;
        }

        private void fillcolor_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            Color c = colorDialog1.Color;
            fillcolor.Tag = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B).ToLower();
            // "#ffffffff"

            fillcolor.Invalidate(); // change the picture color

            Editor.BackColor = c;
        }

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editor.Font = new Font(cmbFonts.SelectedItem.ToString(), Editor.Font.Size, Editor.Font.Style);
        }

        private void pIncreaseFont_Click(object sender, EventArgs e)
        {
            if (Editor.Font.Size < 48)
                Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size + 2, Editor.Font.Style);
        }

        private void pDecreaseFont_Click(object sender, EventArgs e)
        {
            if (Editor.Font.Size > 4)
                Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size - 2, Editor.Font.Style);
        }

        private void pBold_Click(object sender, EventArgs e)
        {
            if (pBold.Tag.ToString() == "Bold")
            {
                Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size, FontStyle.Regular);
                pBold.Image = Image.FromFile(Utils.ImagesPath + "f_bold.png");
                pBold.Tag = "Regular";
            }
            else
            {
                Editor.Font = new Font(Editor.Font.FontFamily, Editor.Font.Size, FontStyle.Bold);
                pBold.Image = Image.FromFile(Utils.ImagesPath + "f_boldActive.png");
                pBold.Tag = "Bold";
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            StickerDummy form = null;// new StickerDummy(null);
            if ((sender as Button).Name == "btnSave") // saved sticker has name
                form.Tag = txtTitle.Text.Trim();
            form.Size = panelEditor.Size;
            form.Editor.Text = Editor.Text;
            form.Editor.Font = Editor.Font;
            form.Editor.ForeColor = Editor.ForeColor;
            form.Editor.BackColor = Editor.BackColor;
            form.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            DialogResult = DialogResult.OK;
        }

        #region resize dialog
        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }
        #endregion

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
