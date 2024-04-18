using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Bubbles
{
    internal class BrowserTab : WebView2
    {
        TabControl tabCtrl;

        public BrowserTab(TabControl tabCtrl, string uri) : base()
        {
            Dock = DockStyle.Fill; // necessary for showing 
            this.tabCtrl = tabCtrl; // for adding new TabPage controls
            path = uri;

            if (!WebViewInitialized)
                InitializeWebView2Async(@"C:\Temp");

            //HistoryStack = new List<Uri>();
            //HistoryStack_Index = 0;
            //fromHistory = false;
            //CoreWebView2..Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(WebView_Navigated);
            //UpdateNavButtons();

        }
        bool WebViewInitialized = false;
        public string path;

        private void BrowserTab_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            WebViewInitialized = true;
            CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested; // This is the man
            CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged; // Just cosmetic code
        }

        private void CoreWebView2_NewWindowRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true; // let the default new window 

            TabPage tpage = new TabPage(); // boy

            BrowserTab bt = new BrowserTab(tabCtrl, e.Uri)
            { Source = new Uri(e.Uri), NewPage = NewPage, path = e.Uri };

            tpage.Controls.Add(bt); // toy
            tpage.Tag = bt;

            tabCtrl.TabPages.Add(tpage); // daddy
            tabCtrl.SelectedTab = tpage; // user expectation

            tabCtrl.TabPages.Remove(NewPage);
            tabCtrl.TabPages.Add(NewPage);
        }

        //Just cosmetic code
        private void CoreWebView2_DocumentTitleChanged(object sender, object e)
        {
            string pageTitle = CoreWebView2.DocumentTitle;
            if (pageTitle.Length > 30) { pageTitle = pageTitle.Substring(0, 30) + "..."; }

            //int last = tabCtrl.TabPages.Count - 2;
            //tabCtrl.TabPages[last].Text = pageTitle;

            tabCtrl.SelectedTab.Text = " " + pageTitle;

            int tabwidth = 0;
            foreach (TabPage tab in tabCtrl.TabPages)
                tabwidth += tab.Width;
        }

        private async void InitializeWebView2Async(string tempDir = "")
        {
            CoreWebView2Environment webView2Environment = null;

            //set value
            string tempDir2 = tempDir;

            if (String.IsNullOrEmpty(tempDir2))
            {
                //get fully-qualified path to user's temp folder
                tempDir2 = Path.GetTempPath();
            }//if

            //add event handler for CoreWebView2Ready - before webView2Ctl is initialized
            //it's important to not use webViewCtrl until CoreWebView2Ready event is thrown
            CoreWebView2InitializationCompleted += BrowserTab_CoreWebView2InitializationCompleted;

            CoreWebView2EnvironmentOptions options = null;

            //set webView2 temp folder. The temp folder is used to store webView2
            //cached objects. If not specified, the folder where the executable
            //was started will be used. If the user doesn't have write permissions
            //on that folder, such as C:\Program Files\<your application folder>\,
            //then webView2 will fail. 

            webView2Environment = await CoreWebView2Environment.CreateAsync(null, tempDir2, options);

            //webView2Ctl must be inialized before it can be used
            //wait for coreWebView2 initialization
            //when complete, CoreWebView2Ready event will be thrown
            await EnsureCoreWebView2Async(webView2Environment);

            //CoreWebView2.Navigate("https://palmaross.com/");
            Source = new Uri(path, UriKind.Absolute);

            //add other event handlers - after webView2Ctrl is initialized
            //webView2Ctl.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
            //webView2Ctl.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
            //webView2Ctl.NavigationCompleted += WebView2Ctl_NavigationCompleted;
            //webView2Ctl.NavigationStarting += WebView2Ctl_NavigationStarting;
        }

        List<Uri> HistoryStack;
        int HistoryStack_Index;
        bool fromHistory;

        // Constructor
        //public HelpView()
        //{
        //    InitializeComponent();
        //    HistoryStack = new List<Uri>();
        //    HistoryStack_Index = 0;
        //    fromHistory = false;
        //    webView.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(WebView_Navigated);
        //    UpdateNavButtons();
        //}

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryStack_Index > 1)
            {
                HistoryStack_Index--;
                fromHistory = true;
                CoreWebView2.Navigate(HistoryStack[HistoryStack_Index - 1].ToString());
                UpdateNavButtons();
            }
        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryStack_Index < HistoryStack.Count)
            {
                HistoryStack_Index++;
                fromHistory = true;
                CoreWebView2.Navigate(HistoryStack[HistoryStack_Index - 1].ToString());
                UpdateNavButtons();
            }
        }

        private void UpdateNavButtons()
        {
            //this.backButton.IsEnabled = HistoryStack_Index > 1;
            //this.forwardButton.IsEnabled = HistoryStack_Index < HistoryStack.Count;
        }

        //private void WebView_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        //{
        //    if (!fromHistory)
        //    {
        //        if (HistoryStack_Index < HistoryStack.Count)
        //        {
        //            HistoryStack.RemoveRange(HistoryStack_Index, HistoryStack.Count - HistoryStack_Index);
        //        }

        //        HistoryStack.Add(e.Uri);
        //        HistoryStack_Index++;
        //        UpdateNavButtons();
        //    }

        //    fromHistory = false;
        //}

        public TabPage NewPage;
    }
}
