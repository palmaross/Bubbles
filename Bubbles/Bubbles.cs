using System;
using System.Runtime.InteropServices;
using Mindjet.MindManager.Interop;
using PRAManager;
using PRMapCompanion;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using PopupControl;
using System.Linq;
using AppManager;
using System.IO;
using NAudio.Wave;
using Bubbles.AppManager;

namespace Bubbles
{
    class StixMain : MMBase
    {
        public void Create()
        {
            if (m_bCreated)
                return;

            m_cmdDetachNotes = MMUtils.MindManager.Commands.Add(Utils.Registered_AddinName, "omnistix.detach_notes");
            m_cmdDetachNotes.Caption = Utils.getString("topiccontextmenu.notes.detach");
            m_cmdDetachNotes.UpdateState += new ICommandEvents_UpdateStateEventHandler(m_cmdDetachNotes_UpdateState);
            m_cmdDetachNotes.ImagePath = Utils.ImagesPath + "notes_detach.png";
            m_cmdDetachNotes.Click += new ICommandEvents_ClickEventHandler(m_cmdDetachNotes_Click);
            m_cmdDetachNotes.SetDynamicMenu(MmDynamicMenu.mmDynamicMenuContextTopic);

            m_cmdTopicAudioNote = MMUtils.MindManager.Commands.Add(Utils.Registered_AddinName, "omnistix.TopicAudioNote");
            m_cmdTopicAudioNote.Caption = Utils.getString("topiccontextmenu.audinote");
            m_cmdTopicAudioNote.UpdateState += new ICommandEvents_UpdateStateEventHandler(m_cmdTopicAudioNote_UpdateState);
            m_cmdTopicAudioNote.ImagePath = Utils.ImagesPath + "audio.ico";
            m_cmdTopicAudioNote.Click += new ICommandEvents_ClickEventHandler(m_cmdTopicAudioNote_Click);
            m_cmdTopicAudioNote.SetDynamicMenu(MmDynamicMenu.mmDynamicMenuContextTopic);

            m_menus = new DynamicMenus();

            m_controlStrip = MMUtils.MindManager.ControlStripTypeRegistry.RegisterControlStripType(SOUNDSTRIP_URI, Utils.m_imagesPath + "audio.ico", true);
            m_controlStrip.FriendlyName = Utils.getString("topic.playbackicon");
            Controls _stripControls = m_controlStrip.ContextMenu;
            m_controlStripCommand = MMUtils.MindManager.Commands.Add(Utils.Registered_AddinName, "omnistix.controlstrip.audiocommand");
            m_controlStripCommand.UpdateState += new ICommandEvents_UpdateStateEventHandler(m_controlStripCommand_UpdateState);
            m_controlStripCommand.Click += new ICommandEvents_ClickEventHandler(m_controlStripCommand_Click);
            m_controlStrip.Command = m_controlStripCommand;

            // Add buttons to strip context menu
            m_menus.AddButton(_stripControls,
                new SubMenuButtonData("omnistix.stripicon.stopplay",
                    Utils.getString("omnistix.stripicon.stopplay.caption"),
                    "",
                    1),
                    "", "", "",
                    this);
            m_menus.AddButton(_stripControls,
                new SubMenuButtonData("omnistix.stripicon.remove",
                    Utils.getString("button.remove"),
                    "",
                    4),
                    "", "", "",
                    this);

            PRLicenseManager.Get().StartManager();
            Utils.licenseStatus = PRLicenseManager.licenseStatus;

            //m_Snippets = new StixSnippets();
            m_OmniSound = new OmniSound();
            m_StixBase = new StartMenu();
            m_TaskInfo = new StixTaskInfo(0, "H");
            commandPopup.Tag = 0; // Tag is a stick ID

            DocumentStorage.Subscribe(this);

            m_bCreated = true;

            //using (StickerDummy dlg = new StickerDummy(null, new Point(0, 0)))
            //{
            //    StickerDummy.DummyStickerWidth = dlg.Width;
            //    StickerDummy.DummyStickerHeight = dlg.Height;
            //    StickerDummy.DummyStickerImageX = dlg.pStickerImage.Location.X;
            //    StickerDummy.DummyStickerImageY = dlg.pStickerImage.Location.Y;
            //}

            InitializeTopicWidthDlg();
            OmniTools.GetAppIcons();

            OmniStixButton.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            DataTable dt;
            using (StixDB db = new StixDB("Stix"))
                dt = db.ExecuteQuery("select * from STIX order by type");

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["start"]) == 0)
                    continue;

                m_StixBase.startId = Convert.ToInt32(dr["id"]);

                switch (dr["type"].ToString()) 
                {
                    case StixUtils.typeicons:
                        m_StixBase.BaseIcon_MouseClick(m_StixBase.stxIcons, null);
                        break;
                    case StixUtils.typetaskinfo:
                        m_StixBase.BaseIcon_MouseClick(m_StixBase.stxTaskInfo, null);
                        break;
                    case StixUtils.typeformat:
                        m_StixBase.BaseIcon_MouseClick(m_StixBase.stxFormat, null);
                        break;
                    case StixUtils.typetools:
                        m_StixBase.BaseIcon_MouseClick(m_StixBase.stxTools, null);
                        break;
                    case StixUtils.typemapnavigator:
                        m_StixBase.BaseIcon_MouseClick(m_StixBase.stxMapNavigator, null);
                        break;
                    case StixUtils.typeaddtopic:
                        m_StixBase.BaseIcon_MouseClick(m_StixBase.stxAddTopics, null);
                        break;
                    case StixUtils.typetextops:
                        m_StixBase.BaseIcon_MouseClick(m_StixBase.stxTextOps, null);
                        break;
                }
            }
            dt.Dispose(); dt = null;

            HidePopup = new Timer() { Interval = 2000 };
            HidePopup.Tick += HidePopup_Tick;
            HidePopup.Start();

            stopPlayTimer = new Timer() { Interval = 500 };
            stopPlayTimer.Tick += StopPlayTimer_Tick;

            int interval = Convert.ToInt32(Utils.getRegistry("SaveMaps", "5"));
            saveMapsTimer = new Timer() { Interval = interval };
            saveMapsTimer.Tick += SaveMapsTimer_Tick;
            if (Utils.getRegistry("SaveMapsEnabled", "0") == "1")
                saveMapsTimer.Start();

            m_ReplaceDlg = new ReplaceDlg();

            if (MMUtils.ActiveDocument != null)
                onDocumentActivated(null);
        }

        private void m_controlStripCommand_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
        }

        /// <summary>
        /// Click on the audio icon
        /// </summary>
        private void m_controlStripCommand_Click()
        {
            Play();
        }
        public static string playingtopicguid = "";

        public static void Play()
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t == null) return;

            // Current topic sounding. Stop it.
            if (m_OmniSound.audioFile != null && playingtopicguid == t.Guid)
            {
                if (m_OmniSound.outputDevice.PlaybackState != PlaybackState.Stopped)
                    m_OmniSound.outputDevice.Stop();
                playingtopicguid = "";
            }

            string audioPath = t.GetAttributes(SOUNDSTRIP_URI).GetAttributeValue(AUDIO_PATH);
            if (String.IsNullOrEmpty(audioPath))
            {
                MessageBox.Show(Utils.getString("OmniSound.isnotaudiotopic"));
                return;
            }

            string[] parts = audioPath.Split(new string[] { "###" }, StringSplitOptions.None);

            int id = Convert.ToInt32(parts[0]);
            string attachGuid = "";

            if (parts.Length > 1)
                attachGuid = parts[1];

            audioPath = ""; string title = "";

            using (StixDB db = new StixDB("Audio"))
            {
                DataTable dt = db.ExecuteQuery("select * from AUDIOS where id=" + id + "");

                if (dt.Rows.Count > 0)
                {
                    title = dt.Rows[0]["title"].ToString();
                    audioPath = dt.Rows[0]["path"].ToString();
                }
            }

            if (audioPath == "" && attachGuid == "") // Audiofile not found and no attachment
            {
                MessageBox.Show(String.Format(Utils.getString("OmniSound.filenotexists"), ""));
                return;
            }

            string path = audioPath;
            if (audioPath == Path.GetFileName(audioPath)) // file name, not a path
                path = Utils.m_dataPath + "SoundDB\\" + audioPath;

            if (audioPath != "" && attachGuid != "") // audio attachment on topic
            {
                foreach (Attachment attach in t.Attachments) // save attached file to SoundDB folder
                {
                    if (attach.Guid == attachGuid)
                    {
                        path = Utils.m_dataPath + "SoundDB\\" + attach.FileName;

                        if (!File.Exists(path))
                            attach.SaveAs(path);
                    }
                }
            }

            Play(path, title, t.Guid);
        }

        public static void Play(string audioPath, string title = "", string topicGuid = "")
        {
            // Does file exist?
            if (!File.Exists(audioPath))
            {
                MessageBox.Show(String.Format(Utils.getString("OmniSound.filenotexists"), audioPath));
                return;
            }

            string trackName = title;
            if (title == "")
                trackName = Path.GetFileNameWithoutExtension(audioPath);
            
            if (m_OmniSound.Visible) // Select record in OmniSound window
            {
                foreach (var item in m_OmniSound.cbRecordings.Items)
                {
                    if ((item as AudioItem).Path == audioPath)
                        m_OmniSound.cbRecordings.SelectedItem = item;
                }
            }
            else
            {
                if (m_TopicPlayer == null || m_TopicPlayer.IsDisposed)
                {
                    m_TopicPlayer = new TopicPlayer(OmniStixButton.Bounds);
                    m_TopicPlayer.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                }

                m_TopicPlayer.lblTitle.Text = trackName;
                m_TopicPlayer.btnPause.Visible = true;
                m_TopicPlayer.btnPlay.Visible = false;
            }

            m_OmniSound.FilePath = audioPath;
            m_OmniSound.TrackName = trackName;
            playingtopicguid = topicGuid;
            m_OmniSound.btnPlay_Click(null, null);
        }

        private void StopPlayTimer_Tick(object sender, EventArgs e)
        {
            stopPlayTimer.Stop();
        }

        private void SaveMapsTimer_Tick(object sender, EventArgs e)
        {
            saveMapsTimer.Stop();
            foreach (Document doc in MMUtils.MindManager.VisibleDocuments)
                if (doc.IsModified) doc.Save();
            saveMapsTimer.Start();
        }
        

        /// <summary>
        /// Hide command popup if cursor position is out of stick or popup bounds
        /// </summary>
        private void HidePopup_Tick(object sender, EventArgs e)
        {
            if (commandPopup.Visible)
            {
                int stixID = Convert.ToInt32(commandPopup.Tag);
                Form form = STICKS[stixID];

                if (form.RectangleToScreen(form.ClientRectangle).Contains(Cursor.Position) ||
                    commandPopup.RectangleToScreen(commandPopup.ClientRectangle).Contains(Cursor.Position) ||
                    commandPopup.Name.StartsWith("calendar"))
                return;

                StixUtils.ActivateMindManager(); // = Hide popup
            }
        }

        private void m_cmdDetachNotes_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
            pChecked = false;
        }

        private void m_cmdTopicAudioNote_Click()
        {
            if (Utils.FreeVersionLimitExceeded(StixUtils.typeOmniSound))
                return;

            if (MMUtils.ActiveDocument.Path == "")
            {
                MessageBox.Show(Utils.getString("OmniStix.SaveMap"));
                return;
            }

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t == null) return;

            if (t.ContainsControlStripType(SOUNDSTRIP_URI))
            {
                MessageBox.Show(Utils.getString("OmniSound.topichasaudio"), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                t = null; return;
            }

            if (m_OmniSound.writer != null) // Recorder is busy.
            {
                MessageBox.Show(Utils.getString("OmniSound.busy.recording"));
                return;
            }
            if (m_OmniSound.outputDevice.PlaybackState != PlaybackState.Stopped) // OmniPlayer is busy.
            {
                MessageBox.Show(Utils.getString("OmniSound.busy.playing"));
                return;
            }

            if (m_TopicRecorder == null || m_TopicRecorder.IsDisposed)
            {
                m_TopicRecorder = new TopicRecorder(OmniStixButton.Bounds, t.Guid);
                m_TopicRecorder.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
            t = null;

            using (SaveRecordDlg sr = new SaveRecordDlg(true, false))
            {
                int locx = (sr.Width - m_TopicRecorder.Width) / 2;
                sr.Location = new Point(m_TopicRecorder.Left - locx, m_TopicRecorder.Bottom);

                sr.ShowDialog(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void m_cmdTopicAudioNote_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
            pChecked = false;
        }

        private void m_cmdDetachNotes_Click()
        {
            if (m_topicNotes == null || m_topicNotes.IsDisposed)
                m_topicNotes = new TopicNotesDlg();

            if (!m_topicNotes.Visible)
                m_topicNotes.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));

            if (m_topicNotes.Height < m_topicNotes.panelMinimized.Height + 10)
                m_topicNotes.Bounds = m_topicNotes.WindowExpanded;

            TreeNode map = null, node = null;
            string mappath = MMUtils.ActiveDocument.FullName.ToLower();
            foreach (TreeNode _node in m_topicNotes.listTopics.Nodes)
            {
                if (_node.Name == mappath) {
                    map = _node; break; }
            }
            if (map == null)
            {
                if (m_topicNotes.listTopics.Nodes.Count > 0 && Utils.FreeVersionLimitExceeded("topicnotes"))
                    return;

                map = m_topicNotes.listTopics.Nodes.Add(mappath, MMUtils.ActiveDocument.CentralTopic.Text, 0);
                map.NodeFont = new Font(m_topicNotes.listTopics.Font, FontStyle.Bold);
                map.Text = map.Text;
            }

            UpdateTopicNotes(MMUtils.ActiveDocument);

            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (String.IsNullOrEmpty(t.Notes.Text))
                    continue;

                // Check if topic already is 
                bool found = false;
                foreach (TreeNode _node in map.Nodes)
                    if ((_node.Tag as TopicNotesItem).TopicGuid == t.Guid)
                    {
                        m_topicNotes.listTopics.SelectedNode = _node;
                        m_topicNotes.Select();
                        m_topicNotes.listTopics.Select();
                        found = true;
                        break;
                    }
                if (found) continue;

                string topictext = t.Text.Trim();
                if (String.IsNullOrEmpty(topictext)) topictext = Utils.getString("TopicNotesDlg.noname");

                string html = Utils.ClearTopicNotes(t.Notes.TextXHTML);

                TopicNotesItem item = new TopicNotesItem(topictext, t.Guid, html);

                node = map.Nodes.Add(topictext);
                node.Tag = item;

                if (OmniTopics.Keys.Contains(mappath))
                {
                    if (OmniTopics[mappath].Keys.Contains(t.Guid)) continue;
                    OmniTopics[mappath].Add(t.Guid, node);
                }
                else
                    OmniTopics[mappath] = new Dictionary<string, TreeNode> { { t.Guid, node } };
            }

            // Select appropiate node
            if (node != null) m_topicNotes.listTopics.SelectedNode = node;
            m_topicNotes.Select();
            m_topicNotes.listTopics.Select();
        }

        /// <summary>
        /// MindManager API Bug.
        /// Check if there are changed topic notes in the given document.
        /// If there are, update them from the saved document copy.
        /// </summary>
        /// <param name="doc"></param>
        public static void UpdateTopicNotes(Document doc, List<string> topics = null)
        {
            if (Utils.ActiveDocumentOrSelectionNull(false)) return;

            if (topics == null)
            {
                topics = new List<string>();
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                    topics.Add(t.Guid);
            }

            string mappath = doc.FullName.ToLower();
            bool modified = false;

            if (!MapTopicsWithNotes.Keys.Contains(mappath)) return;

            // Check if there are modified topic notes among selected topics
            foreach (var pair in MapTopicsWithNotes[mappath])
                if (pair.Value == true && topics.Contains(pair.Key)) { // topic notes are changed
                    modified = true; break; }

            if (!modified) return; // There are no modified topic notes

            string docCopyPath = Utils.GetMapCopy(doc); // Prepare map copy for XML

            XMLMapCompanion.Get(docCopyPath); // Get XML

            foreach (var pair in MapTopicsWithNotes[mappath].Reverse())
            {
                if (pair.Value == true) // affected notes
                {
                    foreach (var _pair in XMLMapCompanion.m_topics)
                    {
                        if (_pair.Key == pair.Key)
                        {
                            string notes = _pair.Value.NotesHtml();
                            Topic t = doc.FindByGuid(_pair.Key) as Topic;
                            if (t != null)
                            {
                                falsealarm2 = true;
                                t.Notes.TextXHTML = notes; // false alarm?
                            }
                            MapTopicsWithNotes[mappath][pair.Key] = false;
                        }
                    }
                }
            }

            if (File.Exists(docCopyPath))
                File.Delete(docCopyPath);
        }

        public static void MarkOrAddTopicToBugList(Topic t, bool affected)
        {
            string mappath = t.Document.FullName.ToLower();

            if (MapTopicsWithNotes.Keys.Contains(mappath))
            {
                if (MapTopicsWithNotes[mappath].Keys.Contains(t.Guid))
                    MapTopicsWithNotes[mappath][t.Guid] = affected;
                else
                    MapTopicsWithNotes[mappath].Add(t.Guid, affected);
            }
            else
                MapTopicsWithNotes[mappath] = new Dictionary<string, bool>() { {  t.Guid, affected } };
        }

        public override void onDocumentActivated(MMEventArgs aArgs)
        {
            if (m_MapNavigator != null && m_MapNavigator.Visible) m_MapNavigator.Init();
            else if (m_MapNavigatorDlg != null && m_MapNavigatorDlg.Visible) m_MapNavigatorDlg.Init();
            
            if (m_Resources != null && m_Resources.Visible) m_Resources.InitCurrentMapResources();
            if (m_TaskInfo.Visible) m_TaskInfo.PopulateResources();
            if (m_TopicPlayer != null && m_TopicPlayer.Visible) m_TopicPlayer.InitSoundTopicsList();

            if (m_SearchText != null && m_SearchText.Visible)
                m_SearchText.txtSearch_TextChanged(null, null);

            foreach (var form in STICKS.Values)
            {
                if (form.Name == "StixIcons")
                    (form as StixIcons).m_updateIconsGroupMenu = true;
            }

            DocumentStorage.Sync(MMUtils.ActiveDocument); // subscribe document to events
        }

        public override void onDocumentOpened(MMEventArgs aArgs)
        {
            // Create list of topics with notes.
            foreach (Topic t in MMUtils.ActiveDocument.Range(MmRange.mmRangeAllTopics))
            {
                string path = MMUtils.ActiveDocument.FullName.ToLower();
                if (!t.Notes.IsEmpty)
                {
                    if (!MapTopicsWithNotes.Keys.Contains(path))
                        MapTopicsWithNotes[path]
                            = new Dictionary<string, bool> { { t.Guid, false } };
                    else if (!MapTopicsWithNotes[path].Keys.Contains(t.Guid))
                        MapTopicsWithNotes[path].Add(t.Guid, false);
                }
            }
        }

        public override void onObjectAdded(MMEventArgs aArgs)
        {
            if (aArgs.target is Topic t)
            {
                // Paste operation from stick
                if (StixTextOps.pastetext)
                {
                    if (!StixTextOps.PastedTopics.Contains(t))
                        StixTextOps.PastedTopics.Add(t);
                }
                // Paste operation from MindManager.
                else if (StixUtils.TopicAutoWidth) // Topic autowidth enabled
                {
                    StixUtils.TopicWidthList.Add(t);
                    StixUtils.SetTopicWidth();
                    StixUtils.TopicWidthList.Clear();
                }

                // Process MapNavigator Stix. It will also process MP window
                if (m_MapNavigator != null && !m_MapNavigator.IsDisposed)
                    m_MapNavigator.TopicAffected(MMUtils.ActiveDocument.Guid, t, "added");
                // Well, there is a window only
                else if (m_MapNavigatorDlg != null && !m_MapNavigatorDlg.IsDisposed)
                    m_MapNavigatorDlg.TopicAffected(MMUtils.ActiveDocument.Guid, t, "added");
            }
        }

        public override void onBeforeObjectRemoved(MMEventArgs aArgs)
        {
            if (aArgs.target is Topic t)
            {
                // Process MapNavigator Stix. It will also process MP window
                if (m_MapNavigator != null && !m_MapNavigator.IsDisposed)
                    m_MapNavigator.TopicAffected(MMUtils.ActiveDocument.Guid, t, "deleted");
                // Well, there is a window only
                else if (m_MapNavigatorDlg != null && !m_MapNavigatorDlg.IsDisposed)
                    m_MapNavigatorDlg.TopicAffected(MMUtils.ActiveDocument.Guid, t, "deleted");

                if (t.IsMainTopic) { InitMainTopics = true; }
            }
        }
        bool InitMainTopics = false;

        public override void onAfterObjectRemoved(MMEventArgs aArgs)
        {
            if (InitMainTopics)
            {
                InitMainTopics = false;

                // Process MapNavigator Stix. It will also process MP window
                if (m_MapNavigator != null && !m_MapNavigator.IsDisposed)
                    m_MapNavigator.InitMainTopicsContextMenu();
                // Well, there is a window only
                else if (m_MapNavigatorDlg != null && !m_MapNavigatorDlg.IsDisposed)
                    m_MapNavigatorDlg.InitMainTopics();
            }
        }

        public override void onDocumentDeactivated(MMEventArgs aArgs)
        {
            // last visible document is closing
            if (MMUtils.MindManager.VisibleDocuments.Count == 1 && m_MapNavigator != null)
                m_MapNavigator.Init();
        }

        public override void onDocumentClosed(MMEventArgs aArgs)
        {
            //MapTopicsWithNotes.Remove(aArgs.Document.FullName.ToLower());
        }

        public override void onDocumentClipboardPasteOrDrop(MMEventArgs aArgs)
        {
            if (StixTextOps.pastetext) return;
            MMPaste = true;
        }
        bool MMPaste = false;

        // For Task Info stick. For topic notes.
        public override void onObjectChanged(MMEventArgs aArgs)
        {
            if (aArgs.what.Contains("selection"))
            {
                if (m_TaskInfo != null && m_TaskInfo.Visible)// && !m_TaskInfo.stickDuration)
                {
                    // If map selection changed, change the dates in the TaskInfo stick with selected topic dates
                    SetDates();
                }

                // Process FormatStix
                foreach (var form in STICKS.Values)
                {
                    // Set font state in the FormatStix
                    if (form.Name == "StixFormat" && form.Visible)
                    {
                        var stix = form as StixFormat;
                        ClearFontButtons(stix);

                        bool bold = true, italic = true, underline = true, strikethrough = true;
                        float size = 0; bool sizeequal = true;

                        if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0) {
                            bold = false; italic = false; underline = false; strikethrough = false; }

                        foreach (Topic _t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                        {
                            if (size == 0) size = _t.Font.Size;
                            if (_t.Font.Size != size) sizeequal = false;

                            if (!_t.Font.Bold) bold = false; if (!_t.Font.Italic) italic = false;
                            if (!_t.Font.Underline) underline = false; if (!_t.Font.Strikethrough) strikethrough = false;
                        }

                        if (size > 3 && sizeequal)
                        {
                            stix.numFontSize.Value = (int)size;
                            stix.numFontSize.Text = size.ToString();
                        }
                        if (bold) stix.pBold.Image = stix.BoldA;
                        if (italic) stix.pItalic.Image = stix.ItalicA;
                        if (underline) stix.pUnder.Image = stix.UnderlineA;
                        if (strikethrough) stix.pStrike.Image = stix.StrikethroughA;
                    }
                }
            }

            if (!(aArgs.target is Topic t)) return;

            if (aArgs.what == "text") // topic text changed
            {
                if (StixUtils.TopicAutoWidth && // Topic AutoWidth enabled
                    MMPaste && // Text is pasted into topic via MindManager
                    !StixTextOps.pastetext) // to insure: it's not a TextOpsStix operation)
                {
                    // Set topic width
                    StixUtils.TopicWidthList.Add(t);
                    StixUtils.SetTopicWidth();
                    StixUtils.TopicWidthList.Clear();
                    MMPaste = false;
                }

                // Process MapNavigator Stix. It will also process MP window
                if (m_MapNavigator != null && !m_MapNavigator.IsDisposed)
                    m_MapNavigator.TopicAffected(MMUtils.ActiveDocument.Guid, t, "text");
                // Well, there is a window only
                else if (m_MapNavigatorDlg != null && !m_MapNavigatorDlg.IsDisposed)
                    m_MapNavigatorDlg.TopicAffected(MMUtils.ActiveDocument.Guid, t, "text");
            }

            if (aArgs.what.Contains("notesxhtmldata"))
            {
                ///// MindManager API bug /////
                
                if (StixTextOps.falsealarm) { StixTextOps.falsealarm = false; return; }

                // Changes are from the OmniTopicNotes window
                if (m_topicNotes != null && m_topicNotes.fromTNDlg) return;
                if (falsealarm2) { falsealarm2 = false; return; }

                // Add topic to MapTopicsWithNotes dictionary (MTWND)
                string path = MMUtils.ActiveDocument.FullName.ToLower();
                if (MapTopicsWithNotes.Keys.Contains(path)) // MTWND contains map
                {
                    if (MapTopicsWithNotes[path].Keys.Contains(t.Guid)) // topic notes changed, mark it as changed
                        MapTopicsWithNotes[path][t.Guid] = true;
                    else { // but not contains topic. It's a topic with created, not changed notes {
                        MapTopicsWithNotes[path].Add(t.Guid, false); return; }

                }
                else // First created topic notes in this map 
                {
                    MapTopicsWithNotes[path] = new Dictionary<string, bool>() { { t.Guid, false } };
                    return;
                }

                // Topic notes were changed

                // Is OmniTopicNotes window opened?
                if (m_topicNotes == null || !m_topicNotes.Visible) return;

                // Check if the topic is in the OmniTopicNotes window
                if (OmniTopics.Count == 0 || !OmniTopics.Keys.Contains(path) ||
                    !OmniTopics[path].Keys.Contains(t.Guid)) return; // Not

                // We have to update topic notes
                string aName = MMUtils.nowUnixTimestamp() + ".mmap"; // temp map
                string _path = Utils.m_localDataPath + aName;
                MMUtils.ActiveDocument.SaveAs(_path, true); // save it to temp directory

                // Get topic notes from document copy
                var m_xmlDocument = XMLMapCompanion.GetDocumentXML(_path);
                string notes = XMLMapCompanion.GetTopicNotes(m_xmlDocument, t.Guid);
                if (notes == null) return; // Fail with document XML

                // Get topic node
                TreeNode node = OmniTopics[path][t.Guid];
                TopicNotesItem item = node.Tag as TopicNotesItem;
                t.Notes.TextXHTML = notes; t.Notes.Commit();

                // Check if the notes are opened in the tab page and modified there
                foreach (TabPage tp in m_topicNotes.tabControl1.TabPages)
                {
                    if (tp.Controls.OfType<WebBrowser>().Count() == 0) continue;

                    WebBrowser wb = tp.Controls.OfType<WebBrowser>().First();
                    wbItem rtbitem = wb.Tag as wbItem;

                    if (rtbitem.Node == node) // Yes, they are in this page
                    {
                        if (tp.AccessibleName == "edited") // And modified in the window. Conflict.
                        {
                            // Warning the user
                            if (MessageBox.Show(Utils.getString("TopicNotesDlg.notesconflict"), "",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            return;
                        }

                        // Replace node notes
                        falsealarm2 = true;
                        item.TopicNotes = notes; // !!
                        MapTopicsWithNotes[path][t.Guid] = false;
                        WebBrowser newwb = m_topicNotes.CreateWB(notes, node);
                        tp.Controls.Remove(wb);
                        tp.Controls.Add(newwb);

                        tp.AccessibleName = "";
                        m_topicNotes.tabControl1.Invalidate();
                    }
                }
                return;
            }

            if (m_TaskInfo != null && m_TaskInfo.Visible)
            {
                if (aArgs.what.Contains("task")) // it's possible that user changed task dates
                    SetDates2();
            }
        }
        static bool falsealarm2 = false;

        void ClearFontButtons(StixFormat stix)
        {
            stix.pBold.Image = stix.Bold; stix.pItalic.Image = stix.Italic;
            stix.pUnder.Image = stix.Underline; stix.pStrike.Image = stix.Strikethrough;
            stix.numFontSize.Text = "";
        }

        public static void SetDates()
        {
            if (!m_TaskInfo.Visible) return;

            // No topics selected. Disable Task Info stick controls
            if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.PrimaryTopic == null)
            {
                if (m_TaskInfo.pPriority.Enabled)  // if controls are enabled
                    EnableTaskInfoControls(false); // disable them
                return;
            }

            // There are topics selected

            // Enable Task Info stick controls if they are disabled
            if (!m_TaskInfo.pPriority.Enabled) EnableTaskInfoControls();

            // Set dates in the TaskInfo stick (from topic)
            SetDates2();
        }

        public static void SetDates2()
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            Topic t = MMUtils.ActiveDocument.Selection.PrimaryTopic;
            if (t == null) return;

            //m_TaskInfo.MMDuration = true;

            DateTime startdate = t.Task.StartDate, duedate = t.Task.DueDate;
            bool startequal = true, dueequal = true;

            foreach (Topic _t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                if (!_t.Task.HasStartDate || _t.Task.StartDate != startdate)
                    startequal = false;
                if (!_t.Task.HasDueDate || _t.Task.DueDate != duedate)
                    dueequal = false; 
            }

            // Start Date
            DateTime dt = t.Task.StartDate;
            if (dt == MMUtils.NULLDATE || !startequal)
            {
                m_TaskInfo.pStartDate.BackColor = SystemColors.ControlLight;
                m_TaskInfo.pStartDateToggle.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                m_TaskInfo.pStartDateToggle.Tag = false;
                tt.SetToolTip(m_TaskInfo.pStartDateToggle, Utils.getString("taskinfo.pTopicStartDate.set.tooltip"));
            }
            else
            {
                m_TaskInfo.pStartDate.BackColor = SystemColors.Window;
                string topicdate = dt.ToString("dd, MM").Replace(", ", "/");

                if (m_TaskInfo.pStartDate.Text != topicdate)
                {
                    m_TaskInfo.pStartDate.Text = topicdate;
                    m_TaskInfo.pStartDate.Tag = dt.Date.AddHours(8);
                    tt.SetToolTip(m_TaskInfo.pStartDate, dt.ToLongDateString());
                }
                m_TaskInfo.pStartDate.Select(0, 0); // set carret to the beginning
                m_TaskInfo.pStartDateToggle.Tag = true;
                m_TaskInfo.pStartDateToggle.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");
                tt.SetToolTip(m_TaskInfo.pStartDateToggle, Utils.getString("taskinfo.pTopicStartDate.remove.tooltip"));
            }

            // Due Date
            dt = t.Task.DueDate;
            if (dt == MMUtils.NULLDATE || !dueequal)
            {
                m_TaskInfo.pDueDate.BackColor = SystemColors.ControlLight;
                m_TaskInfo.pDueDateToggle.Tag = false;
                m_TaskInfo.pDueDateToggle.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                tt.SetToolTip(m_TaskInfo.pDueDateToggle, Utils.getString("taskinfo.pTopicDueDate.set.tooltip"));
            }
            else
            {
                m_TaskInfo.pDueDate.BackColor = SystemColors.Window;
                string topicdate = dt.ToString("dd, MM").Replace(", ", "/");

                if (m_TaskInfo.pDueDate.Text != topicdate)
                {
                    m_TaskInfo.pDueDate.Text = topicdate;
                    m_TaskInfo.pDueDate.Tag = dt.Date.AddHours(8);
                    tt.SetToolTip(m_TaskInfo.pDueDate, dt.ToLongDateString());
                }
                m_TaskInfo.pDueDate.Select(0, 0);
                m_TaskInfo.pDueDateToggle.Tag = true;
                m_TaskInfo.pDueDateToggle.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_active.png");
                tt.SetToolTip(m_TaskInfo.pDueDateToggle, Utils.getString("taskinfo.pTopicDueDate.remove.tooltip"));
            }

            if (m_TaskInfo.stickDuration) return;

            // Duration and Effort

            // If task doesn't have dates or if multiple topics selected
            // set duration to 0 (no duration)
            if ((t.Task.StartDate == MMUtils.NULLDATE && t.Task.DueDate == MMUtils.NULLDATE) ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 1)
            {
                m_TaskInfo.ST_DurationUnits.SelectedIndex = 2;
                m_TaskInfo.numDuration.Value = 0;
            }
            else
            {
                SetTaskInfoDurationUnit(t);

                int duration = t.Task.GetDuration(t.Task.DurationUnit);
                int i = m_TaskInfo.ST_DurationUnits.SelectedIndex;
                MmDurationUnit unit = StixTaskInfo.GetDurationUnit(i);

                if (m_TaskInfo.numDuration.Value != duration || t.Task.DurationUnit != unit)
                    m_TaskInfo.numDuration.Value = duration;
            }

            if (MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() > 1)
            {
                m_TaskInfo.ST_EffortUnits.SelectedIndex = 2;
                m_TaskInfo.numEffort.Value = 0; m_TaskInfo.numEffort.Text = "";
            }
            else
            {
                if (t.Task.HasEffort)
                {
                    SetTaskInfoEffortUnit(t);

                    int duration = t.Task.GetEffort(t.Task.EffortUnit);
                    int i = m_TaskInfo.ST_EffortUnits.SelectedIndex;
                    MmDurationUnit unit = StixTaskInfo.GetDurationUnit(i);

                    if (m_TaskInfo.numEffort.Value != duration || t.Task.EffortUnit != unit)
                        m_TaskInfo.numEffort.Value = duration;
                
                }
                else
                {
                    m_TaskInfo.ST_EffortUnits.SelectedIndex = 2;
                    m_TaskInfo.numEffort.Value = 0; m_TaskInfo.numEffort.Text = "";
                }
            }
        }

        public static void SetTaskInfoDurationUnit(Topic t)
        {
            MmDurationUnit unit = t.Task.DurationUnit;

            switch (unit)
            {
                case MmDurationUnit.mmDurationUnitMinute:
                    if (m_TaskInfo.ST_DurationUnits.SelectedIndex != 0)
                        m_TaskInfo.ST_DurationUnits.SelectedIndex = 0; break;
                case MmDurationUnit.mmDurationUnitHour:
                    if (m_TaskInfo.ST_DurationUnits.SelectedIndex != 1)
                        m_TaskInfo.ST_DurationUnits.SelectedIndex = 1; break;
                case MmDurationUnit.mmDurationUnitDay:
                    if (m_TaskInfo.ST_DurationUnits.SelectedIndex != 2)
                        m_TaskInfo.ST_DurationUnits.SelectedIndex = 2; break;
                case MmDurationUnit.mmDurationUnitWeek:
                    if (m_TaskInfo.ST_DurationUnits.SelectedIndex != 3)
                        m_TaskInfo.ST_DurationUnits.SelectedIndex = 3; break;
                case MmDurationUnit.mmDurationUnitMonth:
                    if (m_TaskInfo.ST_DurationUnits.SelectedIndex != 4)
                        m_TaskInfo.ST_DurationUnits.SelectedIndex = 4; break;
            }
        }

        public static void SetTaskInfoEffortUnit(Topic t)
        {
            MmDurationUnit unit = t.Task.EffortUnit;

            switch (unit)
            {
                case MmDurationUnit.mmDurationUnitMinute:
                    if (m_TaskInfo.ST_EffortUnits.SelectedIndex != 0)
                        m_TaskInfo.ST_EffortUnits.SelectedIndex = 0; break;
                case MmDurationUnit.mmDurationUnitHour:
                    if (m_TaskInfo.ST_EffortUnits.SelectedIndex != 1)
                        m_TaskInfo.ST_EffortUnits.SelectedIndex = 1; break;
                case MmDurationUnit.mmDurationUnitDay:
                    if (m_TaskInfo.ST_EffortUnits.SelectedIndex != 2)
                        m_TaskInfo.ST_EffortUnits.SelectedIndex = 2; break;
                case MmDurationUnit.mmDurationUnitWeek:
                    if (m_TaskInfo.ST_EffortUnits.SelectedIndex != 3)
                        m_TaskInfo.ST_EffortUnits.SelectedIndex = 3; break;
                case MmDurationUnit.mmDurationUnitMonth:
                    if (m_TaskInfo.ST_EffortUnits.SelectedIndex != 4)
                        m_TaskInfo.ST_EffortUnits.SelectedIndex = 4; break;
            }
        }

        static void EnableTaskInfoControls(bool enable = true)
        {
            //foreach (System.Windows.Forms.Control c in m_TaskInfo.Controls)
            //{
            //    if (c.Name != "pictureHandle" && c.Name != "Manage" && 
            //        c.Name != "pResources" && c.Name != "pQuickTask" && c.Name != "pRemoveTaskInfo")
            //    {
            //        c.Enabled = enable;
            //    }
            //}

            // Disable set date checkbox if controls are disabled
            if (!enable)
            {
                m_TaskInfo.pStartDateToggle.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                m_TaskInfo.pDueDateToggle.Image = System.Drawing.Image.FromFile(Utils.ImagesPath + "topic_setdate_noactive.png");
                m_TaskInfo.pStartDate.BackColor = SystemColors.ControlLight;
                m_TaskInfo.pDueDate.BackColor = SystemColors.ControlLight;
            }
        }

        void InitializeTopicWidthDlg()
        {
            Dictionary<int, int> awidths = new Dictionary<int, int>();

            using (StixDB db = new StixDB("Misc"))
            {
                DataTable dtWidths = db.ExecuteQuery("select * from TOPICWIDTHS");

                foreach (DataRow row in dtWidths.Rows)
                {
                    int _value = Convert.ToInt32(row["_value"]);
                    int chars = Convert.ToInt32(row["chars"]);
                    bool _checked = row["_checked"].ToString() == "1";

                    switch (row["name"].ToString())
                    {
                        case "numMainWidth":
                            TopicWidthsDlg.mainwidth = _value; break;
                        case "numWidth1":
                            TopicWidthsDlg.widths[0] = _value;
                            TopicWidthsDlg.checkstate[0] = _checked; break;
                        case "numWidth2":
                            TopicWidthsDlg.widths[1] = _value;
                            TopicWidthsDlg.checkstate[1] = _checked; break;
                        case "numWidth3":
                            TopicWidthsDlg.widths[2] = _value;
                            TopicWidthsDlg.checkstate[2] = _checked; break;
                        case "numWidth4":
                            TopicWidthsDlg.widths[3] = _value;
                            TopicWidthsDlg.checkstate[3] = _checked; break;
                        case "numWidth5":
                            TopicWidthsDlg.widths[4] = _value;
                            TopicWidthsDlg.checkstate[4] = _checked; break;
                        case "numAuto1":
                            AutoWidthsDlg.checkstate[0] = _checked;
                            AutoWidthsDlg.chars[0] = chars;
                            AutoWidthsDlg.lengths[0] = _value; break;
                        case "numAuto2":
                            AutoWidthsDlg.checkstate[1] = _checked;
                            AutoWidthsDlg.chars[1] = chars;
                            AutoWidthsDlg.lengths[1] = _value; break;
                        case "numAuto3":
                            AutoWidthsDlg.checkstate[2] = _checked;
                            AutoWidthsDlg.chars[2] = chars;
                            AutoWidthsDlg.lengths[2] = _value; break;
                        case "numAuto4":
                            AutoWidthsDlg.checkstate[3] = _checked;
                            AutoWidthsDlg.chars[3] = chars;
                            AutoWidthsDlg.lengths[3] = _value; break;
                        case "numAuto5":
                            AutoWidthsDlg.checkstate[4] = _checked;
                            AutoWidthsDlg.chars[4] = chars;
                            AutoWidthsDlg.lengths[4] = _value; break;
                        case "numAuto6":
                            AutoWidthsDlg.checkstate[5] = _checked;
                            AutoWidthsDlg.chars[5] = chars;
                            AutoWidthsDlg.lengths[5] = _value; break;
                    }

                    if (row["name"].ToString().StartsWith("numWidth"))
                    {
                        if (_checked && !TopicWidthsDlg.stixwidths.Contains(_value)) TopicWidthsDlg.stixwidths.Add(_value);
                    }
                    else if (row["name"].ToString().StartsWith("numAuto"))
                    {
                        if (_checked && !awidths.Keys.Contains(chars)) awidths[chars] = _value;
                    }
                }
            }

            StixUtils.AutoTopicWidths = awidths.OrderByDescending(key => key.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            StixUtils.MinAutoTopicWidth = StixUtils.AutoTopicWidths.Keys.Last();
            StixUtils.TopicAutoWidth = Utils.getRegistry("TopicAutoWidth", "0") == "1";
            if (Utils.FreeVersionLimitExceeded(StixUtils.typetextops, false))
            {
                if (StixUtils.TopicAutoWidth) Utils.setRegistry("TopicAutoWidth", "0");
                StixUtils.TopicAutoWidth = false;
            }
        }

        public override void SubMenuButtonCallbackUpdateState(ref bool pEnabled, ref bool pChecked, SubMenuButtonData aData)
        {
            Topic _selectedTopic = MMUtils.SelectedTopic();
            base.SubMenuButtonCallbackUpdateState(ref pEnabled, ref pChecked, aData);
        }

        public override void SubMenuButtonCallbackClick(SubMenuButtonData aData, Command aCommand)
        {
            switch (aData.intdata)
            {
                case 1: // Stop playing.
                    m_OmniSound.outputDevice.Stop();
                    if (m_OmniSound.audioFile != null)
                    {
                        m_OmniSound.audioFile.Dispose();
                        m_OmniSound.audioFile = null;
                    }
                    return;
                case 4: // Remove audio strip icon.
                    if (!(MMUtils.SelectedTopic() is Topic _t))
                        return;

                    DialogResult dr;
                    dr = MessageBox.Show(Utils.getString("OmniSound.removeaudio"), "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (dr == DialogResult.Cancel) return;

                    if (!_t.ContainsControlStripType(SOUNDSTRIP_URI)) return;

                    int id = 0; string attachGuid = "";
                    using (StixDB db = new StixDB("Audio"))
                    {
                        string audioPath = _t.GetAttributes(SOUNDSTRIP_URI).GetAttributeValue(AUDIO_PATH);
                        // parts = id###path
                        string[] parts = audioPath.Split(new string[] { "###" }, StringSplitOptions.None);
                        try {
                            id = Convert.ToInt32(parts[0]);
                        }
                        catch {
                            _t.GetAttributes(SOUNDSTRIP_URI).DeleteAll();

                            // Remove strip icon.
                            TransactionWrapper _q = new TransactionWrapper(_t,
                                TransactionWrapper.TransactionType.REMOVE_STRIP_ICON, "");
                            _q.controlStripURI = SOUNDSTRIP_URI;
                            _q.Execute();
                            return;
                        }

                        if (parts.Length > 1)
                            attachGuid = parts[1];

                        audioPath = ""; int groupID = 1; string filename = "";
                        DataTable dt = db.ExecuteQuery("select * from AUDIOS where id=" + id + "");

                        if (dt.Rows.Count > 0)
                        {
                            audioPath = dt.Rows[0]["path"].ToString();
                            groupID = Convert.ToInt32(dt.Rows[0]["groupID"]);
                        }

                        if (dr == DialogResult.Yes) // delete file
                        {
                            string path = audioPath;
                            if (audioPath == Path.GetFileName(audioPath)) // file name, not a path
                                path = Utils.m_dataPath + "SoundDB\\" + audioPath;

                            if (File.Exists(path)) File.Delete(path);
                            // Delete from database
                            db.ExecuteNonQuery("delete from AUDIOS where path=`" + path + "`");
                        }
                        else // move audio to the General group
                        {
                            db.ExecuteNonQuery("update AUDIOS set groupID=1" +
                                " where path=`" + audioPath + "` and groupID=" + groupID + "");
                        }
                    }

                    // Remove sound attribute from topic.
                    _t.GetAttributes(SOUNDSTRIP_URI).DeleteAll();

                    // Remove strip icon.
                    TransactionWrapper _w = new TransactionWrapper(_t,
                        TransactionWrapper.TransactionType.REMOVE_STRIP_ICON, "");
                    _w.controlStripURI = SOUNDSTRIP_URI;
                    _w.Execute();
                    return;
            }

            base.SubMenuButtonCallbackClick(aData, aCommand);
        }

        public void Destroy()
        {
            if (!m_bCreated)
                return;

            Marshal.ReleaseComObject(m_cmdDetachNotes); m_cmdDetachNotes = null;
            Marshal.ReleaseComObject(m_cmdTopicAudioNote); m_cmdTopicAudioNote = null;

            try
            {
                m_menus.DeleteDynamicMenu(m_controlStrip.ContextMenu);
                MMUtils.MindManager.ControlStripTypeRegistry.UnRegisterControlStripType(SOUNDSTRIP_URI);
                Marshal.ReleaseComObject(m_controlStrip); m_controlStrip = null;
                Marshal.ReleaseComObject(m_controlStripCommand); m_controlStripCommand = null;
            }
            catch { }

            //if (m_Snippets.Visible)
            //    m_Snippets.Hide();
            //m_Snippets.Dispose();
            //m_Snippets = null;

            if (StixMapNavigator.DocumentBookmarks != null && StixMapNavigator.DocumentBookmarks.Count > 0)
            {
                StixMapNavigator.DocumentBookmarks.Clear();
                StixMapNavigator.DocumentBookmarks = null;
            }

            if (m_MapNavigatorDlg != null)
            {
                m_MapNavigatorDlg.Hide();
                m_MapNavigatorDlg.Dispose();
                m_MapNavigatorDlg = null;
            }

            if (m_NewLink != null)
            {
                m_NewLink.Hide();
                m_NewLink.Dispose();
                m_NewLink = null;
            }

            if (m_ReplaceDlg != null)
            {
                m_ReplaceDlg.Hide();
                m_ReplaceDlg.Dispose();
                m_ReplaceDlg = null;
            }

            if (m_Resources != null)
            {
                m_Resources.Hide();
                m_Resources.Dispose();
                m_Resources = null;
            }

            if (m_AllSources != null)
            {
                m_AllSources.Hide();
                m_AllSources.Dispose();
                m_AllSources = null;
            }

            if (m_topicNotes != null)
            {
                m_topicNotes.MMClose = true;
                m_topicNotes.listTopics.Nodes.Clear();
                m_topicNotes.Close();
                m_topicNotes.Dispose();
                m_topicNotes = null;
            }

            OmniStixButton.Destroy();

            if (OmniStixButton.Visible)
                OmniStixButton.Hide();
            OmniStixButton.Dispose();
            OmniStixButton = null;

            if (m_topicAutoWidth != null && m_topicAutoWidth.Visible)
            {
                m_topicAutoWidth.Hide();
                m_topicAutoWidth.Dispose();
                m_topicAutoWidth = null;
            }

            if (m_ManageAudio != null && m_ManageAudio.Visible)
            {
                m_ManageAudio.Hide();
                m_ManageAudio.Dispose();
                m_ManageAudio = null;
            }

            if (m_StixBase.Visible)
                m_StixBase.Hide();
            m_StixBase.Dispose();
            m_StixBase = null;

            //if (m_Notes != null)
            //{
            //    m_Notes.Dispose();
            //    m_Notes = null;
            //}

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

            HidePopup.Stop();
            HidePopup.Tick -= HidePopup_Tick;
            HidePopup.Dispose(); HidePopup = null;

            StixTextOps.PasteOperations.Stop();
            StixTextOps.PasteOperations.Dispose(); StixTextOps.PasteOperations = null;

            StixTextOps.PastedTopics.Clear(); StixTextOps.SelectedTopics.Clear();

            DocumentStorage.Unsubscribe(this);

            Utils.StockIcons.Clear();

            StixUtils.TopicWidthList.Clear();

            saveMapsTimer.Stop();
            saveMapsTimer.Tick -= SaveMapsTimer_Tick;
            saveMapsTimer.Dispose(); saveMapsTimer = null;

            m_OmniSound.Destroy();

            m_bCreated = false;
        }

        private bool m_bCreated;

        public static OmniButton OmniStixButton = new OmniButton();
        public const string AUDIO_PATH = "OMNIAUDIO_PATH";

        //public static StixSnippets m_Snippets = null;

        public static StixMapNavigator m_MapNavigator;
        public static MapNavigatorDlg m_MapNavigatorDlg;
        public static BookmarksDlg m_Bookmarks;
        public static SearchTextDlg m_SearchText;
        public static NewLinkDlg m_NewLink;
        public static OmniSound m_OmniSound;
        public static ManageToolsDlg m_ManageTools;
        public static QuickTopicsDlg m_QuickTopics;
        public static AutoWidthsDlg m_topicAutoWidth;

        public static BrowserDlg OmniBrowser = null;

        public static ResourcesDlg m_Resources;
        public static LinksDlg m_AllSources;

        public static StixTaskInfo m_TaskInfo;

        //public static Organizer.NotesDlg m_Notes;

        public static ReplaceDlg m_ReplaceDlg;

        public static StartMenu m_StixBase = null;

        private Command m_cmdDetachNotes;
        public static TopicNotesDlg m_topicNotes;

        private Command m_cmdTopicAudioNote;

        public static Dictionary<int, Form> STICKS = new Dictionary<int, Form>();
        public static Dictionary<int, Form> pNOTES = new Dictionary<int, Form>();

        public static Popup commandPopup = new Popup(new StixPopup().panelH);

        Timer HidePopup = new Timer();
        public static Timer stopPlayTimer = new Timer();
        public static Timer saveMapsTimer = new Timer();

        static ToolTip tt = new ToolTip() { ShowAlways = true, AutoPopDelay = 3000 };

        private DynamicMenus m_menus;

        private ControlStripType m_controlStrip = null;
        private Command m_controlStripCommand = null;
        public const string SOUNDSTRIP_URI = "OMNISTIX_STRIPICON_OMNIAUDIO";

        public static TopicPlayer m_TopicPlayer;
        public static TopicRecorder m_TopicRecorder;
        public static ManageAudioDlg m_ManageAudio;

        /// <summary>
        /// All map topics with notes.
        /// Key: MapPath, Value: Dictionary [Key: Topic Guid, Value: changed]
        /// </summary>
        public static Dictionary<string, Dictionary<string, bool>> MapTopicsWithNotes = new Dictionary<string, Dictionary<string, bool>>();
        /// <summary>
        /// Key: MapPath, Value: Dictionary [Key: Topic Guid, Value: Topic Node]
        /// </summary>
        public static Dictionary<string, Dictionary<string, TreeNode>> OmniTopics = new Dictionary<string, Dictionary<string, TreeNode>>();
    }
}
