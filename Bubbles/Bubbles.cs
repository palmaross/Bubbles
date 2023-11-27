using System;
using System.Runtime.InteropServices;
using Mindjet.MindManager.Interop;
using PRAManager;
using System.IO;
using PRMapCompanion;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using PopupControl;

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

            m_bubbleSnippets = new BubbleSnippets();
            m_bubblesMenu = new MainMenuDlg();
            commandPopup.Tag = 0; // Tag is a stick ID

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

            DataTable dt;
            using (BubblesDB db = new BubblesDB())
                dt = db.ExecuteQuery("select * from STICKS  order by type");

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["start"]) == 0)
                    continue;

                m_bubblesMenu.startId = Convert.ToInt32(dr["id"]);

                switch (dr["type"].ToString()) 
                {
                    case StickUtils.typeicons:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.pIcons, null);
                        break;
                    case StickUtils.typepripro:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.PriPro, null);
                        break;
                    case StickUtils.typeformat:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Format, null);
                        break;
                    case StickUtils.typesources:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.MySources, null);
                        break;
                    case StickUtils.typebookmarks:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Bookmarks, null);
                        break;
                    case StickUtils.typepaste:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Paste, null);
                        break;
                    case StickUtils.typeorganizer:
                        m_bubblesMenu.MenuIcon_Click(m_bubblesMenu.Organizer, null);
                        break;
                }
            }
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
            if (m_Bookmarks != null)
                m_Bookmarks.Init();
        }

        public override void onDocumentDeactivated(MMEventArgs aArgs)
        {
            if (MMUtils.ActiveDocument == null && m_Bookmarks != null)
                m_Bookmarks.Init();
        }

        public void Destroy()
        {
            if (!m_bCreated)
                return;

            m_ctrlBubbles.Delete(); Marshal.ReleaseComObject(m_ctrlBubbles); m_ctrlBubbles = null;
            Marshal.ReleaseComObject(m_cmdBubbles); m_cmdBubbles = null;

            if (m_bubbleSnippets.Visible)
                m_bubbleSnippets.Hide();
            m_bubbleSnippets.Dispose();
            m_bubbleSnippets = null;

            if (BubbleBookmarks.BookmarkedDocuments != null && BubbleBookmarks.BookmarkedDocuments.Count > 0)
            {
                BubbleBookmarks.BookmarkedDocuments.Clear();
                BubbleBookmarks.BookmarkedDocuments = null;
            }

            if (m_BookmarkList != null)
            {
                m_BookmarkList.Hide();
                m_BookmarkList.Dispose();
                m_BookmarkList = null;
            }

            if (m_bubblesMenu.Visible)
                m_bubblesMenu.Hide();
            m_bubblesMenu.Dispose();
            m_bubblesMenu = null;

            if (m_Notes != null)
            {
                m_Notes.Dispose();
                m_Notes = null;
            }

            foreach (var stick in STICKS)
            {
                if (stick.Value.Visible)
                    stick.Value.Hide();
                stick.Value.Dispose();
            }
            STICKS.Clear();

            foreach (var note in pNOTES)
            {
                if (note.Value.Visible)
                    note.Value.Hide();
                note.Value.Dispose();
            }
            pNOTES.Clear();

            if (commandPopup != null)
            {
                commandPopup.Dispose(); commandPopup = null;
            }

            DocumentStorage.Unsubscribe(this);

            m_bCreated = false;
        }

        private bool m_bCreated;

        public static BubbleSnippets m_bubbleSnippets = null;

        public static BubbleBookmarks m_Bookmarks;
        public static BookmarkListDlg m_BookmarkList;

        public static Organizer.NotesDlg m_Notes;

        public static MainMenuDlg m_bubblesMenu = null;

        private Command m_cmdBubbles;
        private Mindjet.MindManager.Interop.Control m_ctrlBubbles;

        public static Dictionary<int, Form> STICKS = new Dictionary<int, Form>();
        public static Dictionary<int, Form> pNOTES = new Dictionary<int, Form>();

        public static Popup commandPopup = new Popup(new StickPopup().panelH);
    }
}
