using System;
using System.Runtime.InteropServices;
using Mindjet.MindManager.Interop;
using PRAManager;
using System.IO;
using PRMapCompanion;

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
        }

        private void m_cmdBubbles_Click()
        {
            BubblesMenuDlg dlg = new BubblesMenuDlg();
            dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        private void m_cmdBubbles_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
            pChecked = false;
        }

        public override void onDocumentActivated(MMEventArgs aArgs)
        {
            // Закладки!
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

            DocumentStorage.Unsubscribe(this);

            m_bCreated = false;
        }

        private bool m_bCreated;
        public static BubblePaste m_bubblePaste = null;
        public static BubbleIcons m_bubbleIcons = null;
        public static BubbleSnippets m_bubbleSnippets = null;
        public static BubbleBookmarks m_bubbleBookmarks = null;

        private Command m_cmdBubbles;
        private Control m_ctrlBubbles;
    }
}
