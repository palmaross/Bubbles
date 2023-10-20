﻿using System;
using System.Runtime.InteropServices;
using Mindjet.MindManager.Interop;
using PRAManager;
using System.IO;
using PRMapCompanion;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

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
            m_cmdBubbles.LargeImagePath = Utils.ImagesPath + "STICKS.png";
            m_cmdBubbles.ImagePath = Utils.ImagesPath + "STICKS.png";
            m_cmdBubbles.Click += new ICommandEvents_ClickEventHandler(m_cmdBubbles_Click);
            m_ctrlBubbles = MMUtils.MindManager.StatusBarControls.AddButton(m_cmdBubbles);

            m_bubblePaste = new BubblePaste();
            m_bubbleBookmarks = new BubbleBookmarks();
            m_bubbleFormat = new BubbleFormat();
            m_bubbleNotepad = new BubbleNotepad();

            m_bubbleSnippets = new BubbleSnippets();
            m_bubblesMenu = new MainMenuDlg();

            DocumentStorage.Subscribe(this);

            m_bCreated = true;

            using (StickerDummy dlg = new StickerDummy(null, new Point(0, 0)))
            {
                StickerDummy.DummyStickerWidth = dlg.Width;
                StickerDummy.DummyStickerHeight = dlg.Height;
                StickerDummy.DummyStickerImageX = dlg.pStickerImage.Location.X;
                StickerDummy.DummyStickerImageY = dlg.pStickerImage.Location.Y;
            }

            try { 
                Directory.CreateDirectory(Utils.m_dataPath + "IconDB");
                Directory.CreateDirectory(Utils.m_dataPath + "ImageDB");
            }
            catch { };

            if (Utils.getRegistry("StartIcons", "0") == "1")
                m_bubblesMenu.Icons_Click(null, null);

            if (Utils.getRegistry("StartPaste", "0") == "1")
                m_bubblesMenu.PasteBubble_Click(null, null);

            if (Utils.getRegistry("StartBookmarks", "0") == "1")
                m_bubblesMenu.Bookmarks_Click(null, null);

            if (Utils.getRegistry("StartPriPro", "0") == "1")
                m_bubblesMenu.PriPro_Click(null, null);

            if (Utils.getRegistry("StartMySources", "0") == "1")
                m_bubblesMenu.MySources_Click(null, null);

            if (Utils.getRegistry("StartFormat", "0") == "1")
                m_bubblesMenu.Formatting_Click(null, null);

            if (Utils.getRegistry("StartNotepad", "0") == "1")
                m_bubblesMenu.Notepad_Click(null, null);
        }

        private void m_cmdBubbles_Click()
        {
            if (m_bubblesMenu.Visible)
                m_bubblesMenu.Hide();
            else
            {
                m_bubblesMenu.Location = new Point(Cursor.Position.X - m_bubblesMenu.Width / 2, MMUtils.MindManager.Top + MMUtils.MindManager.Height - m_bubblesMenu.Height - m_bubblesMenu.Settings.Height);
                m_bubblesMenu.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                int locX = m_bubblesMenu.panel1.Location.X;
                int startLoc = m_bubblesMenu.panel1.Location.Y + m_bubblesMenu.panel1.Height;
                m_bubblesMenu.panel1.Location = new Point(locX, m_bubblesMenu.label1.Location.Y);
                m_bubblesMenu.Refresh();

                do
                {
                    startLoc = startLoc - 7;
                    m_bubblesMenu.panel1.Location = new Point(locX, startLoc);
                    m_bubblesMenu.panel1.Refresh();
                }
                while (m_bubblesMenu.panel1.Location.Y > 0);
            }
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

            //if (m_bubbleIcons.Visible)
            //    m_bubbleIcons.Hide();
            //m_bubbleIcons.Dispose();
            //m_bubbleIcons = null;

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

            if (m_bubbleFormat.Visible)
                m_bubbleFormat.Hide();
            m_bubbleFormat.Dispose();
            m_bubbleFormat = null;

            if (m_bubbleNotepad.Visible)
                m_bubbleNotepad.Hide();
            m_bubbleNotepad.Dispose();
            m_bubbleNotepad = null;

            if (m_bubblesMenu.Visible)
                m_bubblesMenu.Hide();
            m_bubblesMenu.Dispose();
            m_bubblesMenu = null;

            foreach (var stick in STICKS)
            {
                if (stick.Value.Visible)
                    stick.Value.Hide();
                stick.Value.Dispose();
            }

            DocumentStorage.Unsubscribe(this);

            m_bCreated = false;
        }

        private bool m_bCreated;
        public static BubblePaste m_bubblePaste = null;
        public static BubbleSnippets m_bubbleSnippets = null;
        public static BubbleBookmarks m_bubbleBookmarks = null;
        public static BubbleFormat m_bubbleFormat = null;
        public static BubbleNotepad m_bubbleNotepad = null;

        public static MainMenuDlg m_bubblesMenu = null;

        private Command m_cmdBubbles;
        private Mindjet.MindManager.Interop.Control m_ctrlBubbles;

        public static Dictionary<int, Form> STICKS = new Dictionary<int, Form>();
    }
}
