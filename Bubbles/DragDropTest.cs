using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DragDropTest
{
    public partial class DDResult : Form
    {
        private FileDropResults DD_Results;
        private List<Bitmap> DroppedImages;
        private Panel PreviousPanel = null;
        private bool DrawSelection = false;
        private bool ClearHtmlPage = false;
        private bool DownloadLinkedImages = true;

        public DDResult()
        {
            InitializeComponent();

            DroppedImages = new List<Bitmap>();

            //Add fixed tab stops to the listbox
            List<int> iTabs = new List<int>();
            iTabs.AddRange(new int[] { 60, 120 });
            SetListBoxTabs(listBox1, iTabs);

            //Add fixed tab stops to the richtextbox
            rtbResults.WordWrap = false;
            rtbResults.SelectionTabs = new int[] { 120, 200 };
            rtbResults.AcceptsTab = true;
        }

        private void DDResult_Load(object sender, EventArgs e)
        {
            //Initializes the WebBrowser Document, setting its Mode to the latest IE version available
            this.webBrowser1.Navigate("");
            this.webBrowser1.Document.OpenNew(true);
            this.webBrowser1.Document.Write("<!DOCTYPE html><html><head><meta http-equiv='x-ua-compatible' content='IE=edge'></head><body> </body></html>");
        }

        public class FileDropResults
        {
            public enum DataFormat : int
            {
                MemoryStream = 0,
                Text,
                UnicodeText,
                Html,
                Bitmap,
                ImageBits,
            }

            public FileDropResults() { this.Contents = new List<DropContent>(); }

            public List<DropContent> Contents { get; set; }

            public class DropContent
            {
                public object Content { get; set; }
                public string Result { get; set; }
                public DataFormat Format { get; set; }
                public string DataFormatName { get; set; }
                public List<Bitmap> Images { get; set; }
                public List<string> HttpSourceImages { get; set; }
            }
        }

        private void btnCopyHtml_Click(object sender, EventArgs e) => 
            Clipboard.SetText(webBrowser1.DocumentText, TextDataFormat.UnicodeText);

        private void chkClearHtml_CheckedChanged(object sender, EventArgs e) => 
            ClearHtmlPage = chkClearHtml.Checked;

        private void chkDownloadImages_CheckedChanged(object sender, EventArgs e) => 
            DownloadLinkedImages = chkDownloadImages.Checked;

        private void TextBox1_DragDrop(object sender, DragEventArgs e)
        {
            this.rtbResults.Clear(); 
            this.listBox1.Items.Clear();
            GetDataFormats(e.Data, DownloadLinkedImages);
        }

        private void TextBox1_DragEnter(object sender, DragEventArgs e)
        {
            this.rtbResults.Clear();
            this.listBox1.Items.Clear();
            e.Effect = DragDropEffects.Copy;
        }

        private void panImage_DragDrop(object sender, DragEventArgs e) => 
            GetDataFormats(e.Data, DownloadLinkedImages);

        private void panImage_DragEnter(object sender, DragEventArgs e) => 
            e.Effect = DragDropEffects.Copy;

        private void ShowPreviewImages()
        {
            int PanelsCount = panPreview.Controls.Count;
            if (PanelsCount > 0)
            {
                for (int i = PanelsCount - 1; i > -1; i--)
                {
                    panPreview.Controls[i].Click -= this.OnPreviewPanelClick;
                    panPreview.Controls[i].Paint -= this.OnPanelPreviewPaint;
                    panPreview.Controls[i].Dispose();
                }
                panPreview.Controls.Clear();
                panPreview.Update();
            }

            if ((DroppedImages == null) || (DroppedImages.Count < 2))
            {
                panPreview.SendToBack();
                return;
            }

            for (int i = 0; i < DroppedImages.Count; i++)
            {
                Panel panel = new Panel() {
                    Size = new Size(panPreview.Width, panPreview.Width), 
                    BackgroundImageLayout = ImageLayout.Zoom, 
                    BackgroundImage = DroppedImages[i], 
                    BackColor = panPreview.BackColor,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(0, panPreview.Width * i), 
                    Tag = i
                };
                panel.Paint += new PaintEventHandler(this.OnPanelPreviewPaint);
                panel.Click += new EventHandler(this.OnPreviewPanelClick);
                panPreview.Controls.Add(panel);
            }
            panPreview.BringToFront();
        }

        private void OnPreviewPanelClick(object sender, EventArgs e)
        {
            panImage.BackgroundImage = DroppedImages[(int)((Panel)sender).Tag];
            DrawSelection = true;
            ((Panel)sender).Invalidate();
        }

        //Draws the images on the small preview panels
        private void OnPanelPreviewPaint(object sender, PaintEventArgs e)
        {
            if (DrawSelection)
            {
                Rectangle SelectionBorder = new Rectangle(Point.Empty, new Size(panPreview.Width - 3, panPreview.Width - 3));
                e.Graphics.DrawRectangle(new Pen(Color.Yellow, 1), SelectionBorder);
                if (PreviousPanel != null)
                    using (Graphics g = PreviousPanel.CreateGraphics())
                        g.DrawRectangle(new Pen(Color.Gray, 1), SelectionBorder);
                DrawSelection = false;
            }
            PreviousPanel = (Panel)sender;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                rtbResults.Clear();

                FileDropResults.DropContent formatcontent = DD_Results.Contents[listBox1.SelectedIndex];
                rtbResults.AppendText("Raw Format: \t" + formatcontent.DataFormatName + "\r\n");
                rtbResults.AppendText("Converted Format: \t" + formatcontent.Format.ToString() + "\r\n");
                RTBSetLastInsertColor(Color.LightGreen, formatcontent.Format.ToString());
                rtbResults.AppendText("Content Lenght: \t" + ((formatcontent.Content != null) ? formatcontent.Content.ToString().Length.ToString() : "N/A") + "\r\n");
                rtbResults.AppendText("Images: \t" + ((formatcontent.Images != null) ? formatcontent.Images.Count.ToString() : "N/A") + "\r\n");
                if (formatcontent.Images != null) RTBSetLastInsertColor(Color.LightGreen, formatcontent.Images.Count.ToString());
                rtbResults.AppendText("Http Images: \t" + ((formatcontent.HttpSourceImages != null) ? formatcontent.HttpSourceImages.Count.ToString() : "N/A") + "\r\n");
                if (formatcontent.HttpSourceImages != null) RTBSetLastInsertColor(Color.LightGreen, formatcontent.HttpSourceImages.Count.ToString());
            }
        }

        //Changes the color of the string specified in InsertedText
        private void RTBSetLastInsertColor(Color color, string InsertedText)
        {
            int sLenght = InsertedText.Length + 1;
            rtbResults.Select(rtbResults.Text.Length - sLenght, sLenght);
            rtbResults.SelectionColor = color;
            rtbResults.ScrollToCaret();
        }

        public void GetDataFormats(IDataObject Data, bool DownloadHttpImages)
        {
            DD_Results = new FileDropResults();
            DroppedImages.Clear();
            DroppedImages.Capacity = 0;
            
            List<string> formats = Data.GetFormats(false).ToList();

            foreach (string format in formats)
            {
                FileDropResults.DropContent CurrentContents = new FileDropResults.DropContent() 
                { DataFormatName = format };

                switch (format)
                {
                    case ("FileGroupDescriptor"):
                    case ("FileGroupDescriptorW"):
                    case ("DragContext"):
                    case ("UntrustedDragDrop"):
                        break;
                    case ("DragImageBits"):
                        CurrentContents.Content = (MemoryStream)Data.GetData(format, true);
                        CurrentContents.Format = FileDropResults.DataFormat.ImageBits;
                        break;
                    case ("FileDrop"):
                        CurrentContents.Content = null;
                        string[] FileDropData = ((string[])Data.GetData(DataFormats.FileDrop, true));
                        foreach(string filedrop in FileDropData)
                        {
                            bool IsDirectory = (new FileInfo(filedrop).Attributes & FileAttributes.Directory) == FileAttributes.Directory;
                        }
                        CurrentContents.Format = FileDropResults.DataFormat.Bitmap;
                        CurrentContents.Images = new List<Bitmap>();
                        CurrentContents.Images.AddRange(
                            ((string[])Data.GetData(DataFormats.FileDrop, true))
                            .Select(img => Image.FromFile(img))
                            .Cast<Bitmap>().ToArray());
                        break;
                    case ("HTML Format"):
                        CurrentContents.Format = FileDropResults.DataFormat.Html;
                        CurrentContents.Content = Data.GetData(DataFormats.Html, true);
                        int HtmlContentInit = CurrentContents.Content.ToString().IndexOf("<html>", StringComparison.InvariantCultureIgnoreCase);
                        if (HtmlContentInit > 0)
                            CurrentContents.Content = CurrentContents.Content.ToString().Substring(HtmlContentInit);
                        CurrentContents.HttpSourceImages = DD_GetHtmlImages(CurrentContents.Content.ToString());
                        break;
                    case ("UnicodeText"):
                        CurrentContents.Format = FileDropResults.DataFormat.UnicodeText;
                        CurrentContents.Content = Data.GetData(DataFormats.UnicodeText, true);
                        break;
                    case ("Text"):
                        CurrentContents.Format = FileDropResults.DataFormat.Text;
                        CurrentContents.Content = Data.GetData(DataFormats.Text, true);
                        break;
                    default:
                        CurrentContents.Format = FileDropResults.DataFormat.MemoryStream;
                        CurrentContents.Content = Data.GetData(format, true);
                        break;
                }

                if (CurrentContents.Content != null)
                {
                    if (CurrentContents.Content.GetType() == typeof(MemoryStream))
                    {
                        using (MemoryStream _memStream = new MemoryStream())
                        {
                            ((MemoryStream)CurrentContents.Content).CopyTo(_memStream);
                            _memStream.Position = 0;

                            CurrentContents.Result = Encoding.Unicode.GetString(_memStream.ToArray());
                        }
                    }
                    else
                    {
                        if (CurrentContents.Content.GetType() == typeof(String))
                            CurrentContents.Result = CurrentContents.Content.ToString();
                    }
                }
                DD_Results.Contents.Add(CurrentContents);
            }

            //Verify whether a Text source available => updates the TextBox content.
            textBox1.Text = DD_Results.Contents.Where(txt => txt.Format == FileDropResults.DataFormat.UnicodeText)
                                               .Select(txt => txt.Result)
                                               .FirstOrDefault();
            
            //If some images have been already downloaded (by the WebBrowser) => shows the first one.
            DroppedImages = DD_Results.Contents.Where(img => img.Format == FileDropResults.DataFormat.Bitmap)
                                               .SelectMany(img => img.Images)
                                               .ToList();
            if (DroppedImages.Count > 0)
                panImage.BackgroundImage = DroppedImages.First();

            //If the Html source contained some Image Links => Create a list
            //If the DownloadHttpImages = true, download the images
            List<string> HttpImageLinks = DD_Results.Contents.Where(res => 
                                                                   (res.Format == FileDropResults.DataFormat.Html & 
                                                                   (res.HttpSourceImages != null)))
                                                             .SelectMany(img => img.HttpSourceImages)
                                                             .ToList();

            //If the Download of Image source is enable => Download all links and add the Bitmaps to a list
            if ((HttpImageLinks.Count > 0) && (DownloadHttpImages == true))
            {
                int CurrentCount = DroppedImages.Count;
                DroppedImages.AddRange(DownloadImages(HttpImageLinks));
                if (CurrentCount == 0 && DroppedImages.Count > 0)
                    panImage.BackgroundImage = DroppedImages.First();
            }

            listBox1.Items.Clear();
            listBox1.Items.AddRange(DD_Results.Contents.Select(res => 
                                    res.Format.ToString() + ": \t" + 
                                    res.DataFormatName).ToArray());

            //Verify whether an Html source is available => updates the WebBrowser content..
            string htmlsnippet = DD_Results.Contents.Where(html => html.Format == FileDropResults.DataFormat.Html)
                                                    .Select(html => html.Result)
                                                    .FirstOrDefault();
            //Clear the WB document if the option is enabled
            if (ClearHtmlPage) WBInitialize();
            //this.webBrowser1.Document.Write(htmlsnippet);


            //Show a Preview of the dropped images. It pops only if there's more than one image
            ShowPreviewImages();
        }

        public List<string> DD_GetHtmlImages(string HtmlSource)
        {
            MatchCollection matches = Regex.Matches(HtmlSource, @"<img[^>]+src=""([^""]*)""",
                                      RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            return (matches.Count > 0)
                   ? matches.Cast<Match>()
                            .Select(x => x.Groups[1]
                            .ToString()).ToList()
                   : null;
        }

        private List<Bitmap> DownloadImages(List<string> Links)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            using (WebClient webclient = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                webclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                webclient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10; Win64; x64; rv:56.0) Gecko/20100101 Firefox/56.0");
                webclient.Headers.Add(HttpRequestHeader.Accept, "ext/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                webclient.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
                webclient.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
                webclient.Headers.Add(HttpRequestHeader.KeepAlive, "keep-alive");
                webclient.UseDefaultCredentials = true;

                foreach (string httpimage in Links)
                {
                    MemoryStream stream = new MemoryStream(webclient.DownloadData(new Uri(httpimage)));
                    bitmaps.Add(new Bitmap(stream));
                }
            };
            return bitmaps;
        }

        private void WBInitialize()
        {
            this.webBrowser1.Navigate("");
            this.webBrowser1.Document.OpenNew(true);
            this.webBrowser1.Document.Write("<meta http-equiv='x-ua-compatible' content='IE=edge'>");
        }

        public void SetListBoxTabs(ListBox listbox, IList<int> tabs)
        {
            listbox.UseTabStops = true;
            listbox.UseCustomTabOffsets = true;
            ListBox.IntegerCollection offsets = listbox.CustomTabOffsets;

            foreach (int tab in tabs)
                offsets.Add(tab);
        }
    }
}
