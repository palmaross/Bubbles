using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ScaleStickDlg : Form
    {
        public ScaleStickDlg(Form _form, string stixType, float scaleFactor)
        {
            InitializeComponent();

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            // Fill Scale Factor
            numSfactor.Text = scaleFactor.ToString() + "%";
            ScaleFactor = scaleFactor;
            StixType = stixType;
            stix = _form;

            if (scaleFactor >= 100 && scaleFactor <= 300 && scaleFactor != 100)
            {
                this.Scale(new SizeF(scaleFactor / 100, scaleFactor / 100)); // scale
                float _fsize = numSfactor.Font.Size * (scaleFactor / 100);
                numSfactor.Font = new Font(numSfactor.Font.FontFamily, _fsize);
                cbSfactor.Font = new Font(cbSfactor.Font.FontFamily, _fsize);
            }

            this.Deactivate += (object sender, EventArgs e) => this.Close();
        }
        float ScaleFactor;
        string StixType;
        Form stix;

        private void cbStixBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (numSfactor.Text == cbSfactor.Text) return;
            numSfactor.Text = cbSfactor.Text;

            int value;

            try {
                value = Convert.ToInt32(numSfactor.Text.Trim('%').Trim());
            } catch { numSfactor.Text = ScaleFactor.ToString() + "%"; return; }

            ScaleStix();
        }

        private void numSfactor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MaskedTextBox mtb = sender as MaskedTextBox;
                int value;

                try
                {
                    value = Convert.ToInt32(mtb.Text.Trim('%').Trim());
                }
                catch { mtb.Text = numSfactor.Text = ScaleFactor.ToString() + "%"; return; }

                if (value < 100 || value > 300) { 
                    mtb.Text = numSfactor.Text = ScaleFactor.ToString() + "%"; return; }

                ScaleStix();

                if (e != null)
                {
                    e.Handled = true; // to avoid the "ding" sound
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void ScaleStix()
        {
            float SF_Stix;

            try
            {
                SF_Stix = Convert.ToInt32(numSfactor.Text.Trim('%').Trim());
            }
            catch { return; }

            switch (StixType)
            {
                case StixUtils.typebase:
                    (stix as StixBase).ScaleStick(ScaleFactor, SF_Stix);
                    break;
                case StixUtils.typeicons:
                    (stix as StixIcons).ScaleStick(ScaleFactor, SF_Stix);
                    break;
                case StixUtils.typetaskinfo:
                    (stix as StixTaskInfo).ScaleStick(ScaleFactor, SF_Stix);
                    break;
                case StixUtils.typeaddtopic:
                    (stix as StixAddTopic).ScaleStick(ScaleFactor, SF_Stix);
                    break;
                case StixUtils.typeformat:
                    (stix as StixFormat).ScaleStick(ScaleFactor, SF_Stix);
                    break;
                case StixUtils.typetools:
                    (stix as StixTools).ScaleStick(ScaleFactor, SF_Stix);
                    break;
                case StixUtils.typebookmarks:
                    (stix as StixBookmarks).ScaleStick(ScaleFactor, SF_Stix);
                    break;
                case StixUtils.typetextops:
                    (stix as StixTextOps).ScaleStick(ScaleFactor, SF_Stix);
                    break;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
