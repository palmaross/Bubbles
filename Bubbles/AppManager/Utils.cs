using System;
using System.Collections.Generic;
using PRAManager;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Mindjet.MindManager.Interop;
using System.IO;
using System.Linq;
using System.Xml;
using System.Net;
using Image = System.Drawing.Image;
using System.Text.RegularExpressions;
using System.Text;
using System.Data; 
using System.Threading;

namespace Bubbles
{
    public class Utils
    {
        public static void ErrorToSupport(string error)
        {
            MessageBox.Show(error + "\r\n\r\n" +
                getString("calendar.fatalerror.text"),
                getString("calendar.fatalerror.caption"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static int GetRandom()
        {
            return new Random().Next();
        }

        public static void Init()
        {
            FriendlyAddinName = "OmniStix";
            I18n = MMUtils._hashtable;
            ImagesPath = MMUtils.m_imagesPath;
            dllPath = MMUtils.m_dllPath;
            Language = MMUtils.Language;

            m_defaultDataPath = MMUtils.m_defaultDataPath;
            m_dataPath = MMUtils.m_dataPath;
            m_localDataPath = MMUtils.m_localDataPath;
            m_imagesPath = MMUtils.m_imagesPath;

            m_dataPath = getRegistry("DataPath", "");

            if (m_dataPath == "")
                m_dataPath = m_defaultDataPath;

            string path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
            DirectoryInfo di = new DirectoryInfo(path);

            // Get stock icons.
            foreach (FileInfo fi in di.GetFiles()) // perf! > 1000 ms
            {
                string _path = fi.FullName;
                string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(_path);

                if (!StockIconsDupes.Keys.Contains(signature))
                    StockIconsDupes.Add(signature, _path);

                string stockicon = "stock" + Path.GetFileNameWithoutExtension(_path);
                MmStockIcon MMstockicon = StixIcons.StockIconFromString(stockicon);
                StockIcons.Add(stockicon, MMstockicon);
            }

            GetCustomIcons(di);  // perf!!!
            StockIconsDupes.Clear();

            try
            {
                if (!Directory.Exists(m_dataPath + "Databases"))
                    Directory.CreateDirectory(m_dataPath + "Databases");
                if (!Directory.Exists(m_dataPath + "IconDB"))
                    Directory.CreateDirectory(m_dataPath + "IconDB");
                if (!Directory.Exists(m_dataPath + "FaviconDB"))
                    Directory.CreateDirectory(m_dataPath + "FaviconDB");
                if (!Directory.Exists(m_dataPath + "ImageDB"))
                    Directory.CreateDirectory(m_dataPath + "ImageDB");
                if (!Directory.Exists(m_dataPath + "Demos"))
                    Directory.CreateDirectory(m_dataPath + "Demos");
                if (!Directory.Exists(m_dataPath + "SoundDB"))
                    Directory.CreateDirectory(m_dataPath + "SoundDB");
                if (!Directory.Exists(m_dataPath + "ToolStixApps"))
                    Directory.CreateDirectory(m_dataPath + "ToolStixApps");
                if (!Directory.Exists(m_dataPath + "AppIconDB"))
                    Directory.CreateDirectory(m_dataPath + "AppIconDB");

                if (!File.Exists(m_dataPath + "AppsToIgnore.txt"))
                    File.Copy(dllPath + "AppsToIgnore.txt", m_dataPath + "AppsToIgnore.txt");

                di = new DirectoryInfo(dllPath + "Resources\\ToolStixApps");
                foreach (var file in di.GetFiles())
                {
                    string dest = m_dataPath + "ToolStixApps\\" + file.Name;
                    if (!File.Exists(dest)) File.Copy(file.FullName, dest);
                }

                di = new DirectoryInfo(dllPath + "images\\AppIconDB");
                foreach (var file in di.GetFiles())
                {
                    string dest = m_dataPath + "AppIconDB\\" + file.Name;
                    if (!File.Exists(dest)) File.Copy(file.FullName, dest);
                }

                m_iconDB = m_dataPath + "IconDB\\";

                string from = dllPath + "\\Images\\", to = m_dataPath + "ImageDB\\";
                if (!File.Exists(to + "hello1.png"))
                    File.Copy(from + "hello1.png", to + "hello1.png");
                if (!File.Exists(to + "pato.gif"))
                    File.Copy(from + "pato.gif", to + "pato.gif");
            }
            catch { };

            di = new DirectoryInfo(m_dataPath + "IconDB");
            foreach (FileInfo fi in di.GetFiles())
            {
                string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(fi.FullName);
                if (!CustomIcons.Keys.Contains(signature))
                    CustomIcons.Add(signature, fi.FullName);
            }

            List<string> files = new List<string>(Directory.EnumerateFiles(m_localDataPath));
            foreach (string file in files)
                File.Delete(file);

            MMBounds = new Rectangle(MMUtils.MindManager.Left, MMUtils.MindManager.Top, MMUtils.MindManager.Width, MMUtils.MindManager.Height);

            InitDatabases();
            InitStartedMaps(); // perf!

#if MINDJET20
    MM20 = true;
#elif MINDJET21
    MM21 = true;
#elif MINDJET22
    MM22 = true;
#elif MINDJET23
    MM23 = true;
#elif MINDJET24
    MM24 = true;
#endif
        }

        public static float GetScalingFactor(int actual)
        {
            scalingFactor = actual / 70F; // 70 is StartMenu height at 100% scaling factor
            if (scalingFactor < 1.1) scalingFactor = 1;
            else if (scalingFactor > 1.1 && scalingFactor < 1.3) scalingFactor = 1.25F;
            else if (scalingFactor > 1.4 && scalingFactor < 1.6) scalingFactor = 1.5F;
            else if (scalingFactor > 1.7 && scalingFactor < 1.85) scalingFactor = 1.75F;
            else if (scalingFactor > 1.85 && scalingFactor < 2.1) scalingFactor = 2F;
            else if (scalingFactor > 2.1 && scalingFactor < 2.3) scalingFactor = 2.25F;
            else if (scalingFactor > 2.4 && scalingFactor < 2.6) scalingFactor = 2.5F;
            else if (scalingFactor > 2.7 && scalingFactor < 2.9) scalingFactor = 2.75F;
            else if (scalingFactor > 2.9 && scalingFactor < 3.15) scalingFactor = 3F;
            return scalingFactor;
        }

        static void InitDatabases()
        {
            string dbPath = m_dataPath + "Databases\\";
            int iconStixId = 1, toolStixId = 3;

            if (!File.Exists(dbPath + "Stix.db"))
            {
                Random r = new Random();
                
                using (StixDB db = new StixDB("Stix"))
                {
                    db.AddStix(getString("StixIcons.tooltip"), StixUtils.typeicons, 0, "H", "");
                    db.AddStix(getString("StixTaskInfo.tooltip"), StixUtils.typetaskinfo, 0, "H", "");
                    db.AddStix(getString("StixTools.tooltip"), StixUtils.typetools, 0, "H", "");
                    db.AddStix(getString("StixMapNavigator.tooltip"), StixUtils.typemapnavigator, 0, "H", "");
                    db.AddStix(getString("StixAddTopic.tooltip"), StixUtils.typeaddtopic, 0, "H", "");
                    db.AddStix(getString("StixTextOps.tooltip"), StixUtils.typetextops, 0, "H", "");
                    db.AddStix(getString("StixFormat.tooltip"), StixUtils.typeformat, 0, "H", "");
                }
            }
            else
            {
                using (StixDB db = new StixDB("Stix"))
                {
                    DataTable dt = db.ExecuteQuery("select * from STIX");
                    foreach (DataRow dr in dt.Rows)
                    {
                        switch (dr["type"].ToString())
                        {
                            case StixUtils.typeicons:
                                if (iconStixId == 0) iconStixId = Convert.ToInt32(dr["id"]); break;
                        }
                    }
                }
            }

            if (!File.Exists(dbPath + "Icons.db"))
            {
                using (StixDB db = new StixDB("Icons"))
                {
                    db.AddIcon(getString("icons.firststick.icon1"), "stockexclamation-mark", 1, iconStixId);
                    db.AddIcon(getString("icons.firststick.icon2"), "stockquestion-mark", 2, iconStixId);
                }
            }

            if (!File.Exists(dbPath + "Links.db"))
            {
                using (StixDB db = new StixDB("Links"))
                {
                    db.AddLinkGroup(getString("LinksDlg.commongroup"), 0, 1);
                    db.AddLinkGroup("Group 1", 0, 2);
                    db.AddLinkGroup("Group 1.1", 2, 1);
                    db.AddLinkGroup("Group 1.2", 2, 2);

#if VENDOR_OL
                    db.AddLink(getString("tools.demo1.title"), "http://www.olympic-limited.co.uk/", "http", "", "", 1);
#else
                    db.AddLink(getString("tools.demo1.title"), "https://palmaross.com/", "http", "", "", 1);
#endif
                    db.AddLink(getString("tools.demo2.title"), dllPath + "OmniStix.chm", "chm", "", "", 1);
                    db.AddLink(getString("tools.demo4.title"), "https://www.youtube.com/watch?v=U92A8H2rK2I", "youtube", "", "", 1);
                }
            }

            if (!File.Exists(dbPath + "Tools.db"))
            {
                using (StixDB db = new StixDB("Tools"))
                {
                    // Add to ToolStix
                    db.AddTool(getString("tools.demo2.title"), "", dllPath + "OmniStix.chm", "chm", 2, toolStixId);
                    db.AddTool(getString("tools.notepad"), "", "c:\\Windows\\System32\\notepad.exe", "tool-winnotepad.png", 3, toolStixId);
                    // Add to ToolDB
                    db.AddTool(getString("tools.closeall"), getString("tools.closeall.tooltip"), "OT_CloseAll", "tool-closemaps.png", 0, 0);
                    db.AddTool(getString("tools.saveall"), getString("tools.saveall.tooltip"), "OT_SaveAll", "tool-saveall.png", 0, 0);
                    db.AddTool(getString("tools.readonlytopic"), getString("tools.readonlytopic.tooltip"),
                        "read-only-topic.mmbas", "tool-locktopic.png", 0, 0);
                }
            }

            if (!File.Exists(dbPath + "Resources.db"))
            {
                using (StixDB db = new StixDB("Resources"))
                {
                    db.AddResourceGroup(getString("taskinfo.database.resourcegroup1"));
                    db.AddResourceGroup(getString("taskinfo.database.resourcegroup2"));
                    db.AddResource(getString("taskinfo.database.resources.res1"), "", 1);
                    db.AddResource(getString("taskinfo.database.resources.res2"), "", 1);
                    db.AddResource(getString("taskinfo.database.resources.res3"), "#ff80ff80", 2);
                    db.AddResource(getString("taskinfo.database.resources.res4"), "#ffffff80", 2);
                }
            }

            if (!File.Exists(dbPath + "Audio.db"))
            {
                using (StixDB db = new StixDB("Audio"))
                    db.AddAudioGroup(getString("OmniSound.defaultgroup"));
            }

            if (!File.Exists(dbPath + "Bookmarks.db"))
            {
                using (StixDB db = new StixDB("Bookmarks"))
                    db.AddBookmarkGroup(Utils.getString("BookmarksDlg.defaultgroup"));
            }

            if (!File.Exists(dbPath + "QuickTopics.db"))
            {
                using (StixDB db = new StixDB("QuickTopics"))
                {
                    db.AddQuickTopicGroup(getString("quicktopic.favorites"), 1);
                    db.AddQuickTopicTemplate(getString("quicktask.template.default"), 1, 1, "", "checked:0", "", "checked:checked$$$rel:today:1;rel:today:1", "", "", "", "", "");
                    db.AddQuickTopicTemplate(getString("quicktask.template.important"), 1, 2, "", "", "checked:1", "checked:checked$$$rel:today:1;rel:today:1", "", "", "", "", "");
                    db.AddQuickTopicTemplate(getString("quicktask.template.completed"), 1, 3, "", "checked:100", "", ":checked$$$;rel:today:1", "", "", "", "", "");
                }
            }

            if (!File.Exists(dbPath + "AddTopics.db"))
            {
                using (StixDB db = new StixDB("AddTopics"))
                {
                    // Add ADDTOPIC_TEMPLATES
                    db.AddPattern(getString("Template.Day"), getString("Template.Day") + " ", "increment###1,10,1,end", "subtopic");
                    db.AddPattern(getString("Template.Month"), getString("Template.January") + " ", "increment###1,31,1,end", "subtopic");
                    db.AddPattern(getString("Template.Task"), getString("Template.Task") + " ", "increment###1,5,1,end", "subtopic");
                    db.AddPattern(getString("Template.WeekDays"), "", getString("Template.WeekDays.lang"), "subtopic");
                }
            }

            if (!File.Exists(dbPath + "Misc.db"))
            {
                using (StixDB db = new StixDB("Misc"))
                {
                    db.AddTopicWidth("numMainWidth", 0, 64, 1);
                    // stix widths
                    db.AddTopicWidth("numWidth1", 0, 100, 1); db.AddTopicWidth("numWidth2", 0, 120, 1);
                    db.AddTopicWidth("numWidth3", 0, 150, 1); db.AddTopicWidth("numWidth4", 0, 180, 1);
                    db.AddTopicWidth("numWidth5", 0, 200, 0); db.AddTopicWidth("numWidth6", 0, 200, 0);
                    // auto widths
                    db.AddTopicWidth("numAuto1", 500, 200, 1); db.AddTopicWidth("numAuto2", 200, 160, 1);
                    db.AddTopicWidth("numAuto3", 150, 120, 1); db.AddTopicWidth("numAuto4", 150, 120, 0);
                    db.AddTopicWidth("numAuto5", 150, 120, 0); db.AddTopicWidth("numAuto6", 150, 120, 0);
                }
            }
        }

        static void InitStartedMaps()
        {
            foreach (Document doc in MMUtils.MindManager.VisibleDocuments)
            {
                // Create list of topics with notes.
                foreach (Topic t in doc.Range(MmRange.mmRangeAllTopics))
                {
                    string path = doc.FullName.ToLower();
                    if (!t.Notes.IsEmpty)
                    {
                        if (!StixMain.MapTopicsWithNotes.Keys.Contains(path))
                            StixMain.MapTopicsWithNotes[path]
                                = new Dictionary<string, bool> { { t.Guid, false } };
                        else
                            StixMain.MapTopicsWithNotes[path].Add(t.Guid, false);
                    }
                }
            }
        }

        public static void InitIcons()
        {
            if (audio != null) return; // Icons are initialized already.

            audio = Image.FromFile(ImagesPath + "audio.ico");
            excel = Image.FromFile(ImagesPath + "ms_excel.png");
            exe = Image.FromFile(ImagesPath + "ms_exe.png");
            file = Image.FromFile(ImagesPath + "ms_file.png");
            image = Image.FromFile(ImagesPath + "ms_img.png");
            macros = Image.FromFile(ImagesPath + "ms_macros.png");
            map = Image.FromFile(ImagesPath + "ms_map.png");
            pdf = Image.FromFile(ImagesPath + "ms_pdf.png");
            txt = Image.FromFile(ImagesPath + "ms_txt.png");
            video = Image.FromFile(ImagesPath + "ms_video.png");
            http = Image.FromFile(ImagesPath + "ms_web.png");
            word = Image.FromFile(ImagesPath + "ms_word.png");
            youtube = Image.FromFile(ImagesPath + "ms_youtube.png");
            chm = Image.FromFile(ImagesPath + "ms_chm.png");
            html = Image.FromFile(ImagesPath + "ms_html.png");
        }

        // Get custom mm icons. Check for stock icons dupes. 
        static void GetCustomIcons(DirectoryInfo directoryInfo)
        {
            foreach (var directory in directoryInfo.GetDirectories())
            {
                foreach (FileInfo fi in directory.GetFiles())
                {
                    string path = fi.FullName;
                    string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(path);

                    // Icon is a double of the stock icon.
                    if (StockIconsDupes.Keys.Contains(signature))
                    {
                        string stockicon = Path.GetFileNameWithoutExtension(path);
                        if (!StockIconDupes.ContainsKey(Path.GetFileNameWithoutExtension(path)))
                            StockIconDupes.Add(stockicon, StixIcons.StockIconDupes(stockicon));
                    }
                    else if (!CustomIcons.Keys.Contains(signature))
                        CustomIcons.Add(signature, path);
                }
                GetCustomIcons(directory);
            }
        }

        /// <summary>
        /// Get value from registry subkey
        /// </summary>
        /// <param name="aKey">key to get value</param>
        /// <param name="aDefValue">value if key not found</param>
        /// <returns>key value</returns>
        public static string getRegistry(string aKey, string aDefValue = "")
        {
            MMUtils.Company = Company;
            MMUtils.AddinName = AddinName;
            return MMUtils.getRegistry("", aKey, aDefValue);
        }

        /// <summary>
        /// Set value to registry subkey
        /// </summary>
        /// <param name="aKey">key to set value</param>
        /// <param name="aValue">value to set</param>
        /// <returns>true - ok, false - failed</returns>
        public static bool setRegistry(string aKey, string aValue)
        {
            MMUtils.Company = Company;
            MMUtils.AddinName = AddinName;
            return MMUtils.setRegistry("", aKey, aValue);
        }

        public static string getString(string name)
        {
            MMUtils._hashtable = I18n;
            return MMUtils.getString(name);
        }

        public static Document GetOrOpenDocument(string path, bool activate = false, bool visible = true)
        {
            foreach (Document doc in MMUtils.MindManager.AllDocuments)
            {
                if (doc.FullName.ToLower() == path.ToLower())
                {
                    if (activate) doc.Activate();
                    return doc;
                }
            }

            if (File.Exists(path))
                return MMUtils.MindManager.AllDocuments.Open(path, "", visible);
            else
                return null;
        }

        public static bool IsFree()
        {
            return licenseStatus == "Unlicensed";
        }

        /// <summary>
		/// Return True if a certain percent of a rectangle is shown across the 
		/// total screen area of all monitors, otherwise return False.
		/// </summary>
		/// <param name="RecLocation">Stix location</param>
		/// <param name="RecSize">Stix size</param>
		/// <param name="MinPercentOnScreen"></param>
		/// <returns>False if form is totally off screen</returns>
		public static bool StickIsOnScreen(Point RecLocation, Size RecSize, double MinPercentOnScreen = 1)
        {
            double PixelsVisible = 0;
            Rectangle Rec = new Rectangle(RecLocation, RecSize);

            foreach (Screen Scrn in Screen.AllScreens)
            {
                Rectangle r = Rectangle.Intersect(Rec, Scrn.WorkingArea);
                // intersect rectangle with screen
                if (r.Width != 0 & r.Height != 0)
                {
                    PixelsVisible += (r.Width * r.Height);
                    // tally visible pixels
                }
            }
            return PixelsVisible >= (Rec.Width * Rec.Height) * MinPercentOnScreen;
        }

        /// <summary>
        /// Screen location where MindManager window is located
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point MMScreen(int x, int y)
        {
            foreach (Screen Scr in Screen.AllScreens)
            {
                if (x > Scr.WorkingArea.Left && x < Scr.WorkingArea.Right &&
                    y > Scr.WorkingArea.Top && y < Scr.WorkingArea.Bottom)
                    return Scr.WorkingArea.Location;
            }
            return new Point();
        }

        /// <summary>
        /// Check if Stix is located within MindManager window.
        /// </summary>
        /// <param name="rec">Stix bounds</param>
        /// <returns></returns>
        public static bool IsOnMMWindow(Rectangle rec)
        {
            if (rec.X + rec.Width < MMUtils.MindManager.Left || // stick is totally to the left
                rec.X > MMUtils.MindManager.Left + MMUtils.MindManager.Width) // stick is totally to the right
                return false;
            if (rec.Y + rec.Height < MMUtils.MindManager.Top || // stick is totally above
                rec.Y > MMUtils.MindManager.Top + MMUtils.MindManager.Height) // stick is totally below
                return false;
            return true;
        }

        /// <summary>
        /// Validate string for SQLite
        /// </summary>
        /// <param name="s">Given string</param>
        /// <returns>Validated string</returns>
        public static string VS(string s)
        {
            s = s.Replace("`", "``");
            return s;
        }

        #region DateTime

        public static DateTime NULLDATE = new DateTime(1899, 12, 30, 0, 0, 0);

        public static DateTime? GetDate(string date)
        {
            if (date == "") return NULLDATE;

            string[] parts = date.Split(':');

            if (parts[0] == "abs")
            {
                try {
                    return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { return null; }
            }
            else if (parts[0] == "rel")
            {
                string period = parts[1];
                int days = int.Parse(parts[2]) - 1;

                if (period == "today")
                    return DateTime.Now.Date.AddDays(days).AddHours(8);
                else if (period == "tomorrow")
                    return DateTime.Now.Date.AddDays(days + 1).AddHours(8);
                else if (period == "thisweek")
                    return getWeekBegin().AddDays(days).AddHours(8);
                else if (period == "nextweek")
                    return getWeekBegin(1).AddDays(days).AddHours(8);
                else if (period == "thismonth")
                    return getMonthBegin().AddDays(days).AddHours(8);
                else if (period == "nextmonth")
                    return getMonthBegin(1).AddDays(days).AddHours(8);
            }
            return null;
        }

        public static DateTime getWeekBegin(int aOffset = 0)
        {
            DateTime _date = DateTime.Now.Date;
            int _dow = (int)_date.DayOfWeek - 1;
            if (_dow < 0)
                _dow = 6;
            return _date + new TimeSpan((aOffset * 7) - _dow, 0, 0, 0); // _date now is this week's monday
        }

        public static DateTime getWeekEnd(int aOffset = 0)
        {
            return getWeekBegin(aOffset) + new TimeSpan(6, 0, 0, 0);
        }

        public static DateTime getMonthBegin(int aOffset = 0)
        {
            DateTime _date = DateTime.Now.Date.AddMonths(aOffset);
            return new DateTime(_date.Year, _date.Month, 1, 0, 0, 0);
        }

        public static DateTime getMonthEnd(int aOffset = 0)
        {
            DateTime _date = DateTime.Now.Date.AddMonths(aOffset);
            return new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month), 0, 0, 0);
        }

        #endregion

        /// <summary>
        /// Delete Effort from topic replacing topic's XML
        /// </summary>
        /// <param name="t">Topic to delete effort from</param>
        public static void DeleteEffort(Topic t)
        {
            InitMarkersList(t);

            foreach (XmlNode node in topicXML)
            {
                foreach (XmlNode _node in node.ChildNodes)
                {
                    if (_node.Name == "ap:Task")
                    {
                        try
                        {
                            _node.Attributes.RemoveNamedItem("EffortMinutes");
                            _node.Attributes.RemoveNamedItem("EffortUnit");
                        }
                        catch { return; }
                    }
                }
            }

            t.Xml = topicXML.InnerXml;
        }

        public static void InitMarkersList(Topic t)
        {
            topicXML = new XmlDocument();
            topicXML.LoadXml(t.Xml);
            NSManager = new XmlNamespaceManager(topicXML.NameTable);
            NSManager.AddNamespace("ap", "http://schemas.mindjet.com/MindManager/Application/2003");
        }
        protected static XmlNamespaceManager NSManager;
        protected static XmlDocument topicXML;

        public static string GetWebPageTitle(string url)
        {
            string title = "";
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                using (Stream stream = response.GetResponseStream())
                {
                    // compiled regex to check for <title></title> block
                    Regex titleCheck = new Regex(@"<title>\s*(.+?)\s*</title>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    int bytesToRead = 8092;
                    byte[] buffer = new byte[bytesToRead];
                    string contents = "";
                    int length = 0;
                    while ((length = stream.Read(buffer, 0, bytesToRead)) > 0)
                    {
                        // convert the byte-array to a string and add it to the rest of the
                        // contents that have been downloaded so far
                        contents += Encoding.UTF8.GetString(buffer, 0, length);

                        Match m = titleCheck.Match(contents);
                        if (m.Success)
                        {
                            // we found a <title></title> match =]
                            title = m.Groups[1].Value.ToString();
                            break;
                        }
                        else if (contents.Contains("</head>"))
                        {
                            // reached end of head-block; no title found =[
                            break;
                        }
                    }
                }
            }
            catch (Exception _e)
            {
                Console.WriteLine(_e);
            }

            return title;
        }

        public static Image GetFavicon(string url)
        {
            Image img = null; Image ico = null;

            string host = new Uri(url).Host;
            string faviconDB = Utils.m_dataPath + "FaviconDB\\";

            DirectoryInfo di = new DirectoryInfo(faviconDB);
            FileInfo fi = new FileInfo(faviconDB + host + ".png");

            if (di.GetFiles().Any(x => x.Name == fi.Name))
            {
                img = Image.FromFile(fi.FullName);
                return img;
            }

            HttpWebRequest w = null;
            try
            {
                w = (HttpWebRequest)WebRequest
                    .Create("https://www.google.com/s2/favicons?sz=32&domain_url=" + url);
            }
            catch { }

            if (w != null)
            {
                w.AllowAutoRedirect = true;

                try
                {
                    HttpWebResponse r = (HttpWebResponse)w.GetResponse();
                    using (Stream s = r.GetResponseStream())
                    {
                        try { ico = Image.FromStream(s); }
                        catch { img = http; }
                    }
                } catch { return http; }
            }

            if (ico == null) img = http;
            else { img = ico; img.Save(fi.FullName); }

            return img;
        }

        public static Image GetImage(string type)
        {
            switch (type)
            {
                case "html": return html;
                case "Audio": return audio;
                case "excel": return excel;
                case "exe": return exe;
                case "image": return image;
                case "mmbas": return macros;
                case "map": return map;
                case "pdf": return pdf;
                case "txt": return txt;
                case "video": return video;
                case "http": return http;
                case "word": return word;
                case "youtube": return youtube;
                case "chm": return chm;
            }
            return file;
        }

        public static string GetFileType(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            if (path.ToLower().StartsWith("http"))
            {
                if (path.ToLower().Contains("youtube.com"))
                    return "youtube";
                return "http";
            }
            else if (Audio.Contains(ext))
                return "Audio";
            else if (Video.Contains(ext))
                return "video";
            else if (Word.Contains(ext))
                return "word";
            else if (Excel.Contains(ext))
                return "excel";
            else if (Images.Contains(ext))
                return "image";
            else if (ext == ".html" || ext == ".htm" || ext == ".xhtml")
                return "html";
            else if (ext == ".exe")
                return "exe";
            else if (ext == ".mmbas")
                return "macros";
            else if (ext == ".mmap" || ext == ".mmat")
                return "map";
            else if (ext == ".pdf")
                return "pdf";
            else if (ext == ".txt")
                return "txt";
            else if (ext == ".chm")
                return "chm";
            else
                return "file";
        }

        /// <summary>
        /// Remove invalid (for topic notes) html tags in the topic notes.
        /// </summary>
        /// <param name="html">Given HTML</param>
        /// <returns></returns>
        public static string ClearTopicNotes(string html)
        {
            int i = html.IndexOf("<head>");
            int k = -1;
            if (i > -1) { k = html.IndexOf("</head>"); }

            if (k > -1)
                return html.Remove(i, k - i + 7);
            else return html;
        }

        public static bool FreeVersionLimitExceeded(string stix, bool message = true)
        {
            if (!IsFree()) return false;
            string limits = "";

            switch (stix)
            {
                case StixUtils.typeOmniSound:
                    limits = getString("limitation.omnisound");
                    break;
                case StixUtils.typeicons:
                    limits = getString("limitation.iconstix");
                    break;
                case "addicon":
                    limits = getString("limitation.addicontool");
                    break;
                case StixUtils.typetaskinfo:
                    limits = getString("limitation.taskinfo") + getString("limitation.endrestriction");
                    break;
                case StixUtils.typemapnavigator:
                    limits = getString("limitation.navigator") + getString("limitation.endrestriction");
                    break;
                case "mapnavigator":
                    limits = getString("limitation.navigator2");
                    break;
                case StixUtils.typetools:
                    limits = getString("limitation.toolstix");
                    break;
                case "runtool":
                    limits = getString("limitation.runtool");
                    break;
                case StixUtils.typeaddtopic:
                    limits = getString("limitation.topicstix") + getString("limitation.endrestriction");
                    break;
                case "pastetopics":
                    limits = getString("limitation.pastetopics");
                    break;
                case StixUtils.typetextops:
                    limits = getString("limitation.pasteoptions");
                    break;
                case StixUtils.typeformat:
                    limits = getString("limitation.formatstix");
                    break;
                case "bookmarks":
                    limits = getString("limitation.bookmarks") + getString("limitation.endrestriction");
                    break;
                case "topicnotes":
                    limits = getString("limitation.topicnotes");
                    break;
                case "topic2notes":
                    limits = getString("limitation.topic2notes");
                    break;
                case "links":
                    limits = getString("limitation.links");
                    break;
            }

            if (message) MessageBox.Show(limits, getString("FreeVersionLimitation"), 
                MessageBoxButtons.OK, MessageBoxIcon.Hand);

            return true;
        }

        /// <summary>
        /// Save copy of a map and get the path of this copy.
        /// </summary>
        /// <returns>Path of given document copy</returns>
        public static string GetMapCopy(Document doc)
        {
            string aName = MMUtils.nowUnixTimestamp() + ".mmap"; // temp map
            string path = m_localDataPath + aName;
            doc.SaveAs(path, true); // save it to temp directory
            return path;
        }

        /// <summary>
        /// Check if Active Document is null or if there are no selected topics
        /// </summary>
        /// <param name="checkSelection">True - check if there are selected topics</param>
        public static bool ActiveDocumentOrSelectionNull(bool checkSelection = true)
        {
            if (MMUtils.ActiveDocument == null) return true;
            if (checkSelection && (MMUtils.ActiveDocument.Selection.OfType<Topic>() == null ||
                MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0))
                return true;
            return false;
        }

        public static void ShowNotification(System.Windows.Forms.Control control, string message, int sec = 3)
        {
            control.BackColor = SystemColors.Info;
            control.Visible = true;
            control.BringToFront();

            Thread.Sleep(sec);

            for (int i = 0; i < 10; i++)
                control.BackColor = System.Drawing.Color.FromArgb(i, SystemColors.Info);
        }

        /// <summary> Hashtable of localization file</summary>
        public static System.Collections.Hashtable I18n;

        public static string ImagesPath = "";
        public static string dllPath = "";
        public static string Company;
        public static int Version;
        public static string Registered_AddinName;

        /// <summary>
        /// Addin name for registry (here "MapNavigator")
        /// </summary>
        public static string AddinName;
        public static string FriendlyAddinName;
        public static string Language;
        public static string licenseStatus = "";

        public static Rectangle MMBounds;

        /// <summary>Path with last backslash!</summary>
		public static string m_defaultDataPath, m_dataPath, m_localDataPath, m_iconDB, m_imagesPath;

        public static Dictionary<string, string> StockIconsDupes = new Dictionary<string, string>();

        /// <summary>Key: dupname, Value: stockname</summary>
        public static Dictionary<string, string> StockIconDupes = new Dictionary<string, string>();

        public static Dictionary<string, MmStockIcon> StockIcons = new Dictionary<string, MmStockIcon>();

        /// <summary>Key: signature, Value: path</summary>
        public static Dictionary<string, string> CustomIcons = new Dictionary<string, string>();

        public static Image audio, excel, exe, file, image, macros, map, pdf, txt, video, http, word, youtube, chm, html;

        public static readonly List<string> Images = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png", ".ico" };
        public static readonly List<string> Audio = new List<string> { ".aiff", ".au", ".midi", ".mp3", ".m4a", ".wav", ".wma" };
        public static readonly List<string> Video = new List<string> { ".asf", ".avi", ".mp4", ".mov", ".m4v", ".mpg", ".mpeg", ".wmv" };
        public static readonly List<string> Word = new List<string> { ".doc", ".docm", ".docx", ".rtf" };
        public static readonly List<string> Excel = new List<string> { ".xls", ".xlsx", ".xlsm" };

        public static System.Drawing.Color header = System.Drawing.Color.Moccasin;

        public static float scalingFactor = 1.0f;
        public static int WindowFontSize = 12;

        public static bool MM20 = false, MM21 = false, MM22 = false, MM23 = false, MM24 = false;
    }

    class ScalingFactor
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90

            // Other constants may be founded on pinvoke.net
        }

        public static float GetScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();

            //int Xdpi = GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSX);
            return GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSY) / 96;
        }
    }
}
