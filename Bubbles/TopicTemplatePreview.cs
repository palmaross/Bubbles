using System;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class TopicTemplatePreview : UserControl
    {
        public AddTopicTemplateDlg ParentForm { get; set; }

        public TopicTemplatePreview()
        {
            InitializeComponent();

            lblStart.Text = Utils.getString("TopicTemplateDlg.lblStart");
            lblStep.Text = Utils.getString("TopicTemplateDlg.lblStep");
            lblFinish.Text = Utils.getString("TopicTemplateDlg.lblFinish");
            chBoxNumPosition.Text = Utils.getString("TopicTemplateDlg.chBoxNumPosition");
            linkMore.Text = Utils.getString("TopicTemplateDlg.linkMore");
        }

        private void linkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (panelMore.Visible)
                panelMore.Visible = false;
            else
                panelMore.Visible = true;

            panelMore.BringToFront();
        }

        private void numStart_ValueChanged(object sender, EventArgs e)
        {
            ParentForm.TemplateType_CheckedChanged(null, null);
        }

        private void numEnd_ValueChanged(object sender, EventArgs e)
        {
            ParentForm.TemplateType_CheckedChanged(null, null);
        }

        private void numStep_ValueChanged(object sender, EventArgs e)
        {
            ParentForm.TemplateType_CheckedChanged(null, null);
        }

        private void chBoxNumPosition_CheckedChanged(object sender, EventArgs e)
        {
            ParentForm.TemplateType_CheckedChanged(null, null);
        }
    }
}
