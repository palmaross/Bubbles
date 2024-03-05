using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.IO;
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

            Text = Utils.getString("ReplaceDlg.Title");
            rbtnWholeMap.Text = Utils.getString("ReplaceDlg.rbtnWholeMap");
            rbtnSelectedTopics.Text = Utils.getString("ReplaceDlg.rbtnSelectedTopics");
            labelWith.Text = Utils.getString("ReplaceDlg.labelWith");
            linkReplaceAll.Text = Utils.getString("ReplaceDlg.linkReplaceAll");
            linkGoToTopic.Text = Utils.getString("ReplaceDlg.linkGoToTopic");
            btnSkip.Text = Utils.getString("ReplaceDlg.btnSkip");
            btnClose.Text = Utils.getString("button.close");

            thisHeight = this.Height;
            linkMore_LinkClicked(null, null);

            this.FormClosing += ReplaceDlg_FormClosing;
        }

        private void ReplaceDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            string text1 = txtText1.Text; string text2 = txtText2.Text;

            if (linkMore.Tag.ToString() == "Max") // replace step by step
            {

            }
            else // Replace all
            {
                List<Topic> topics = new List<Topic>();

                if (rbtnWholeMap.Checked) { topics.AddRange(MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics).OfType<Topic>()); }
                else { topics.AddRange(MMUtils.ActiveDocument.Selection.OfType<Topic>()); }

                foreach (Topic topic in topics)
                {
                    string text = topic.Text;
                    string textRTF = topic.Title.TextRTF;

                    if (text.Contains(text1))
                    {
                        if (textRTF.Contains(text1))
                            topic.Title.TextRTF = textRTF.Replace(text1, text2);
                        else // unicode
                        {

                        }
                    }
                }
            }

            //foreach (Topic topic in topics)
            //{
                //topic.Find(text1);
                //string text = topic.Title.TextRTF;
                //rtb.Clear();
                //rtb.Rtf = text;
                ////rtb.Rtf = text;

                //string tt = topic.Text;
                //int indexOfSearchText = rtb.Find(text1);
                //if (indexOfSearchText != -1)
                //{
                //    rtb.Select(indexOfSearchText, text1.Length);
                //    rtb.SelectionBackColor = System.Drawing.Color.Yellow;
                //    topic.Title.TextRTF = rtb.Rtf;
                //}
                //Regex regex = new Regex(text1, RegexOptions.IgnoreCase);
                //MatchCollection matches = regex.Matches(tt);
                //rtb.SelectAll();
                //rtb.SelectionBackColor = rtb.BackColor;
                //foreach (Match match in matches)
                //{
                //    rtb.Select(match.Index, match.Length);
                //    rtb.SelectionBackColor = System.Drawing.Color.Yellow;
                //}

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
            //}
        }

        //void ReplaceMethod(FlowDocument rtBox, string strOld, string strNew)
        //{
        //    rtBox = (FlowDocument)rtb.SelectedText;
        //    string rtf = "";
        //    TextRange tr = new TextRange(rtBox.ContentStart, rtBox.ContentEnd);
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        tr.Save(memoryStream, DataFormats.Rtf);
        //        rtf = Encoding.Default.GetString(memoryStream.ToArray());
        //    }
        //    rtf = rtf.Replace(ConvertString2RTF(strOld), ConvertString2RTF(strNew));
        //    MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(rtf));
        //    rtBox.SelectAll();
        //    rtBox.Selection.Load(stream, DataFormats.Rtf);
        //}

        private string ConvertString2RTF(string input)
        {
            //first take care of special RTF chars  
            StringBuilder backslashed = new StringBuilder(input);
            backslashed.Replace(@"\", @"\\");
            backslashed.Replace(@"{", @"\{");
            backslashed.Replace(@"}", @"\}");

            //then convert the string char by char  
            StringBuilder sb = new StringBuilder();
            foreach (char character in backslashed.ToString())
            {
                if (character <= 0x7f)
                    sb.Append(character);
                else
                    sb.Append("\\u" + Convert.ToUInt32(character) + "?");
            }
            return sb.ToString();
        }

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
            this.Hide();
        }

        private void linkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkMore.Tag.ToString() == "Max")
            {
                linkMore.Tag = "Min";
                this.Height = thisHeight - minSize.Width;
                rtb.Visible = false;
                linkMore.Text = Utils.getString("ReplaceDlg.linkMore1");
                btnReplace.Width = btnSkip.Width + btnSkip.Height;
                btnReplace.Text = Utils.getString("ReplaceDlg.btnReplace2");
                btnSkip.Visible = false;
                lblTopicCount.Visible = false;
                linkReset.Visible = false;
                linkMore.Visible = false;
            }
            else
            {
                linkMore.Tag = "Max";
                this.Height = thisHeight;
                rtb.Visible = true;
                linkMore.Text = Utils.getString("ReplaceDlg.linkMore2");
                btnReplace.Width = btnSkip.Width;
                btnReplace.Text = Utils.getString("ReplaceDlg.btnReplace1");
                btnSkip.Visible = true;
                lblTopicCount.Visible = true;
                linkReset.Visible = true;
            }
        }
    }
}
