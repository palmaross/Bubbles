using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ReplaceDlg : Form
    {
        int thisHeight;
        public ReplaceDlg()
        {
            InitializeComponent();

            thisHeight = this.Height;
            linkMore_LinkClicked(null, null);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            List<Topic> topics = new List<Topic>();
            bool replaceall = chbReplaceAll.Checked;

            string text1 = txtText1.Text; string text2 = txtText2.Text;

            if (rbtnWholeMap.Checked ) { topics.AddRange(MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics).OfType<Topic>()); }
            else { topics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>()); }

            foreach (Topic topic in topics)
            {
                //topic.Find(text1);
                string text = topic.Title.TextRTF;
                rtb.Clear();
                rtb.Rtf = text;
                //rtb.Rtf = text;

                string tt = topic.Text;
                int indexOfSearchText = rtb.Find(text1);
                if (indexOfSearchText != -1)
                {
                    rtb.Select(indexOfSearchText, text1.Length);
                    rtb.SelectionBackColor = System.Drawing.Color.Yellow;
                    topic.Title.TextRTF = rtb.Rtf;
                }
                //Regex regex = new Regex(text1, RegexOptions.IgnoreCase);
                //MatchCollection matches = regex.Matches(tt);
                //rtb.SelectAll();
                //rtb.SelectionBackColor = rtb.BackColor;
                //foreach (Match match in matches)
                //{
                //    rtb.Select(match.Index, match.Length);
                //    rtb.SelectionBackColor = System.Drawing.Color.Yellow;
                //}
                tt = rtb.Rtf;
                //MessageBox.Show("Text found");
                //if (rtb.Rtf.Contains(text1))
                //{
                //    MessageBox.Show("Text found");
                //}
                ////test(text);
                //if (text.Contains(text1))
                //{
                //    if (!replaceall)
                //    {
                //        topic.SelectOnly(); topic.SnapIntoView();
                //    }
                //    text = text.Replace(text1, text2);
                //    topic.Title.TextRTF = text;
                //}
            }
        }
        bool start = true;

        void test(string HTMLText)
        {
            int findUTF = -1;
            bool continueUTFSearch = true;
            do
            {
                findUTF = HTMLText.IndexOf(@"\'", findUTF + 1);
                if (findUTF != -1)
                {
                    string replacedString = HTMLText.Substring(findUTF, 4);
                    string esacpeddString = replacedString.Substring(2);

                    int esacpeddCharValue = Convert.ToInt16(esacpeddString, 16);
                    char esacpeddChar = Convert.ToChar(esacpeddCharValue);

                    esacpeddString = esacpeddChar.ToString();

                    HTMLText = HTMLText.Replace(replacedString, esacpeddString);
                    findUTF = -1;
                }
                else
                {
                    continueUTFSearch = false;
                }
            }
            while (continueUTFSearch);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkMore.Tag.ToString() == "Max")
            {
                linkMore.Tag = "Min";
                checkTopicText.Visible = false;
                checkTopicNotes.Visible = false;
                chbReplaceAll.Visible = false;
                this.Height = thisHeight - minSize.Width;
                linkMore.Text = "More >>>>";
            }
            else
            {
                linkMore.Tag = "Max";
                checkTopicText.Visible = true;
                checkTopicNotes.Visible = true;
                chbReplaceAll.Visible = true;
                this.Height = thisHeight;
                linkMore.Text = "Less <<<<";
            }
        }
    }
}
