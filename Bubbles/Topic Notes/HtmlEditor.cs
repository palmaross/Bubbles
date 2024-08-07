using System;
using System.Drawing;
using System.Windows.Forms;
using mshtml;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;

namespace Bubbles
{
    public class HtmlEditor
    {
        WebBrowser webBrowser;
        public dynamic doc;
        private dynamic contentDiv;
        private DOMListener Listener;
        public HtmlEditor(WebBrowser webbrowser, string htmlContent)
        {
            webBrowser = webbrowser;
            webBrowser.DocumentText = @"<div contenteditable=""true""></div>";

            webBrowser.DocumentCompleted += (s, e) =>
            {
                doc = webBrowser.Document.DomDocument;
                contentDiv = doc.getElementsByTagName("div")[0];
                contentDiv.innerHtml = htmlContent;

                StixMain.m_topicNotes.SelectonChanged(webBrowser);
                //webBrowser.Document.AttachEventHandler("onselectionchange", SelectionChanged);

                // Detach the event handler before attaching
                webBrowser.Document.DetachEventHandler("ondblclick", Document_DoubleClick);

                // Now attach the event handler
                webBrowser.Document.AttachEventHandler("ondblclick", Document_DoubleClick);

                HtmlElementCollection links = webBrowser.Document.GetElementsByTagName("A");
                foreach (HtmlElement link in links)
                    link.Click += Link_Click;
            };
        }

        private void Link_Click(object sender, HtmlElementEventArgs e)
        {
            try
            {
                string href = (sender as HtmlElement).OuterHtml;
                string url = Regex.Match(href, "href\\s*=\\s*\"(?<url>.*?)\"").Groups["url"].Value;
                Process.Start(url);
            }
            catch { }
        }

        private void Document_DoubleClick(object sender, EventArgs e)
        {
            IHTMLSelectionObject sel = doc.selection as IHTMLSelectionObject;
            IHTMLTxtRange rng = sel.createRange() as IHTMLTxtRange;
            rng.expand("word");
            rng.select();
        }

        public string HtmlContent => contentDiv.InnerHtml;
        public IHTMLTxtRange Selection => SelectedRange();

        public void Bold() { doc.execCommand("bold", false, null); }
        public void Italic() { doc.execCommand("italic", false, null); }
        public void Underline() { doc.execCommand("underline", false, null); }
        public void Strikethrough() { doc.execCommand("strikethrough", false, null); }
        public void FontSize(int size) { doc.execCommand("FontSize", false, size); }
        public void FontSize(string size) { doc.execCommand("FontSize", false, size); }
        public void Font(string family)
        {
            try
            {
                doc.execCommand("FontName", false, family);
            }
            catch { }
        }

        public void Font2(string family)
        {
            //doc.body.style.fontFamily = family;
            doc.selection.style.fontFamily = family;
        }

        public void Copy() { doc.execCommand("copy", false, null); }
        public void Cut() { doc.execCommand("cut", false, null); }
        public void Paste() { doc.execCommand("paste", false, null); }
        public void Delete() { doc.execCommand("delete", false, null); }
        public void SelectAll() { doc.execCommand("SelectAll", false, null); }
        public void UnselectAll() { doc.execCommand("Unselect", false, Type.Missing); }

        public void OrderedList() { doc.execCommand("insertOrderedList", false, null); }
        public void UnorderedList() { doc.execCommand("insertUnOrderedList", false, null); }
        public void ForeColor(Color color)
        {
            doc.execCommand("foreColor", false, ColorTranslator.ToHtml(color));
        }
        public void BackColor(Color color)
        {
            doc.execCommand("backColor", false, ColorTranslator.ToHtml(color));
        }
        public void InsertImage(Image image)
        {
            var bytes = (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
            var src = $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
            doc.execCommand("insertImage", false, src);
        }
        public void Heading(Headings heading)
        {
            doc.execCommand("formatBlock", false, $"<{heading}>");
        }
        public bool IsBold()
        {
            if (Selection != null) return Selection.queryCommandValue("bold");
            return false;
        }
        public bool IsItalic()
        {
            if (Selection != null) return Selection.queryCommandValue("italic");
            return false;
        }
        public bool IsUnderline()
        {
            if (Selection != null) return Selection.queryCommandValue("underline");
            return false;
        }
        public bool IsStrikeThrough()
        {
            if (Selection != null) return Selection.queryCommandValue("strikethrough");
            return false;
        }
        IHTMLTxtRange SelectedRange()
        {
            try
            {
                IHTMLSelectionObject selection = (IHTMLSelectionObject)doc.selection;
                return (IHTMLTxtRange)selection.createRange();
            } catch { return null; }
        }

        public enum Headings { H1, H2, H3, H4, H5, H6 }

        public Image fBold, fItalic, fBoldActive, fItalicActive;

        public void SearchText(List<string> _search)
        {
            foreach (string search in _search)
            {
                StringBuilder strBuilder = new StringBuilder(doc.body.outerHTML);
                string HTMLString = strBuilder.ToString().Replace("&nbsp;", " ");

                string replacePattern = "$1<span style=\"background-color: rgb(255, 250, 0);\">$2</span>$3";
                string searchPattern = String.Format("(>[^<>]*?)({0})([^<>]*?<)", search.Trim());
                HTMLString = Regex.Replace(HTMLString, searchPattern, replacePattern, RegexOptions.IgnoreCase);

                doc.body.innerHTML = HTMLString;
            }
        }

        public void ClearSearchedText()
        {
            if (doc == null) return;
            string ReplacementTag = @"<span style=""background-color: rgb(255, 250, 0);"">";
            StringBuilder strBuilder = new StringBuilder(doc.body.outerHTML);
            string HTMLString = strBuilder.ToString();

            int index = HTMLString.IndexOf(ReplacementTag, 0, StringComparison.InvariantCultureIgnoreCase);
            while (index > 0 && index < HTMLString.Length)
            {
                HTMLString = HTMLString.Remove(index, ReplacementTag.Length);
                int index2 = HTMLString.IndexOf("</span>", index);
                HTMLString = HTMLString.Remove(index2, 7);
                index = HTMLString.IndexOf(ReplacementTag, index2, StringComparison.InvariantCultureIgnoreCase);
            }
            doc.body.innerHTML = HTMLString;
        }
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisibleAttribute(true)]
    public class DOMListener
    {
        public event Action DOMChanged;

        public WebBrowser Browser;

        public DOMListener(WebBrowser webbrowser)
        {
            this.Browser = webbrowser;
            this.Browser.ObjectForScripting = this;
            this.Browser.DocumentCompleted += OnDOMLoaded;
        }

        private void OnDOMLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlElement script = this.Browser.Document.CreateElement("script");
            script.InnerHtml = "function listenToDOM() { document.addEventListener('DOMSubtreeModified', function(e) { window.external.DOMUpdate() }); }";
            this.Browser.Document.GetElementsByTagName("body")[0].AppendChild(script);
            this.Browser.Document.InvokeScript("listenToDOM");
        }

        public void DOMUpdate()
        {
            if (this.DOMChanged != null) this.DOMChanged.Invoke();
        }
    }

    public class InterceptKeys
    {
        public delegate int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        //Declare the mouse hook constant. //For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.
        private const int WH_KEYBOARD = 2;
        private const int HC_ACTION = 0;

        public static void SetHook()
        {
            _hookID = SetWindowsHookEx(WH_KEYBOARD, _proc, IntPtr.Zero, (uint)AppDomain.GetCurrentThreadId());
        }

        public static void ReleaseHook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static int HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int PreviousStateBit = 31;
            bool KeyWasAlreadyPressed;
            Int64 bitmask = (Int64)Math.Pow(2, (PreviousStateBit - 1));

            if (nCode < 0)
            {
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
            else
            {
                if (nCode == HC_ACTION)
                {
                    Keys keyData = (Keys)wParam;
                    KeyWasAlreadyPressed = ((Int64)lParam & bitmask) > 0;

                    if (Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.C && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+C
                        StixMain.m_topicNotes.editor.Copy();
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.A && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+A
                        StixMain.m_topicNotes.editor.SelectAll();
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.V && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+V
                        StixMain.m_topicNotes.editor.Paste();
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.ShiftKey) &&
                        keyData == Keys.V && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+Shift+V
                        StixMain.m_topicNotes.AddToNotes();
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.X && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+X
                        StixMain.m_topicNotes.editor.Cut();
                    }
                    else if (keyData == Keys.Delete && KeyWasAlreadyPressed == false)
                    {
                        // Delete key
                        StixMain.m_topicNotes.editor.Delete();
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.B && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+B
                        StixMain.m_topicNotes.pBold_Click(null, null);
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.I && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+I
                        StixMain.m_topicNotes.pItalic_Click(null, null);
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && keyData == Keys.U && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+U
                        StixMain.m_topicNotes.pUnderline_Click(null, null);
                    }
                    else if (Functions.IsKeyDown(Keys.ControlKey) && Functions.IsKeyDown(Keys.ShiftKey) &&
                        keyData == Keys.S && KeyWasAlreadyPressed == false)
                    {
                        // Ctrl+Shift+S
                        StixMain.m_topicNotes.pStrikeout_Click(null, null);
                    }
                }
                return (int)CallNextHookEx(_hookID, nCode, wParam, lParam);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    }

    public class Functions
    {
        public static bool IsKeyDown(Keys keys)
        {
            return (GetKeyState((int)keys) & 0x8000) == 0x8000;
        }

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);
    }
}
