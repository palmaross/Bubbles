using System;
using System.Runtime.InteropServices;
using Mindjet.MindManager.Interop;
using PRAManager;
using System.IO;
using PRMapCompanion;
using System.Threading;
using System.Windows.Forms;
using Control = Mindjet.MindManager.Interop.Control;
using System.Drawing;

namespace Bubbles
{
    class BubblesButton : MMBase
    {
        public void Create()
        {
            if (m_bCreated)
                return;

            m_cmdBubbles = MMUtils.MindManager.Commands.Add(Utils.Registered_AddinName, "ribbon.palmaross.bubbles");
            m_cmdBubbles.Caption = "";
            m_cmdBubbles.ToolTip = Utils.getString("main.tooltip") + "\n" + Utils.getString("main.name");
            m_cmdBubbles.UpdateState += new ICommandEvents_UpdateStateEventHandler(m_cmdBubbles_UpdateState);
            m_cmdBubbles.LargeImagePath = Utils.ImagesPath + "bubble.png";
            m_cmdBubbles.ImagePath = Utils.ImagesPath + "bubble.png";
            m_cmdBubbles.Click += new ICommandEvents_ClickEventHandler(m_cmdBubbles_Click);
            m_ctrlBubbles = MMUtils.MindManager.StatusBarControls.AddButton(m_cmdBubbles);

            m_bubblePaste = new BubblePaste();
            m_bubbleIcons = new BubbleIcons();
            m_bubbleSnippets = new BubbleSnippets();
            m_bubbleBookmarks = new BubbleBookmarks();
            m_bubblePriPro = new BubblePriPro();
            m_bubbleMySources = new BubbleMySources();
            m_MySourcesList = new MySourcesListDlg(0, 0, "H");

            DocumentStorage.Subscribe(this);

            m_bCreated = true;

            try { Directory.CreateDirectory(Utils.m_dataPath + "IconDB"); }
            catch { };

            if (Utils.getRegistry("StartIcons", "0") == "1")
                m_bubbleIcons.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            if (Utils.getRegistry("StartPaste", "0") == "1")
                m_bubblePaste.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            if (Utils.getRegistry("StartSnippets", "0") == "1")
                m_bubbleSnippets.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            if (Utils.getRegistry("StartBookmarks", "0") == "1")
                m_bubbleBookmarks.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            if (Utils.getRegistry("StartPriPro", "0") == "1")
                m_bubblePriPro.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            if (Utils.getRegistry("StartMySources", "0") == "1")
                m_bubbleMySources.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            try { BubblesMenuDlg.screenCount = Convert.ToInt32(Utils.getRegistry("Screens", "1")); }
            catch { BubblesMenuDlg.screenCount = 1; }
            try { BubblesMenuDlg.screen = Convert.ToInt32(Utils.getRegistry("Screen", "1")); }
            catch { BubblesMenuDlg.screen = 1; }
        }

        private void m_cmdBubbles_Click()
        {
            BubblesMenuDlg dlg = new BubblesMenuDlg();
            dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            int locX = dlg.panel1.Location.X;
            int startLoc = dlg.panel1.Location.Y + dlg.panel1.Height;
            dlg.panel1.Location = new Point(locX, dlg.lblSettings.Location.Y);
            dlg.Refresh();

            do
            {
                startLoc = startLoc - 5;
                dlg.panel1.Location = new Point(locX, startLoc);
                dlg.panel1.Refresh();
            }
            while (dlg.panel1.Location.Y > 0);
        }

        private void m_cmdBubbles_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
            pChecked = false;
        }

        public override void onDocumentActivated(MMEventArgs aArgs)
        {
            m_bubbleBookmarks.Init();
        }

        public void Destroy()
        {
            if (!m_bCreated)
                return;

            m_ctrlBubbles.Delete(); Marshal.ReleaseComObject(m_ctrlBubbles); m_ctrlBubbles = null;
            Marshal.ReleaseComObject(m_cmdBubbles); m_cmdBubbles = null;

            if (m_bubblePaste.Visible)
                m_bubblePaste.Hide();
            m_bubblePaste.Dispose();
            m_bubblePaste = null;

            if (m_bubbleIcons.Visible)
                m_bubbleIcons.Hide();
            m_bubbleIcons.Dispose();
            m_bubbleIcons = null;

            if (m_bubbleSnippets.Visible)
                m_bubbleSnippets.Hide();
            m_bubbleSnippets.Dispose();
            m_bubbleSnippets = null;

            m_bubbleBookmarks.BookmarkedDocuments.Clear();
            m_bubbleBookmarks.BookmarkedDocuments = null;

            if (m_bubbleBookmarks.Visible)
                m_bubbleBookmarks.Hide();
            m_bubbleBookmarks.Dispose();
            m_bubbleBookmarks = null;

            if (m_bubblePriPro.Visible)
                m_bubblePriPro.Hide();
            m_bubblePriPro.Dispose();
            m_bubblePriPro = null;

            if (m_bubbleMySources.Visible)
                m_bubbleMySources.Hide();
            m_bubbleMySources.Dispose();
            m_bubbleMySources = null;

            if (m_MySourcesList.Visible)
                m_MySourcesList.Hide();
            m_MySourcesList.Dispose();
            m_MySourcesList = null;

            DocumentStorage.Unsubscribe(this);

            m_bCreated = false;
        }

        private bool m_bCreated;
        public static BubblePaste m_bubblePaste = null;
        public static BubbleIcons m_bubbleIcons = null;
        public static BubbleSnippets m_bubbleSnippets = null;
        public static BubbleBookmarks m_bubbleBookmarks = null;
        public static BubblePriPro m_bubblePriPro = null;
        public static BubbleMySources m_bubbleMySources = null;
        public static MySourcesListDlg m_MySourcesList = null;

        private Command m_cmdBubbles;
        private Control m_ctrlBubbles;
    }
}
