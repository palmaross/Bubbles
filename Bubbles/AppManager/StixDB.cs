using System;
using System.Data;
using StixAppManager;

namespace Bubbles
{
    internal class StixDB : DatabaseWrapper
    {
        static string dbName, dbFile;
        public StixDB(string db) : base(db)
        {
            switch (db)
            {
                case "Audio":
                    dbName = "OmniStix Audio"; dbFile = "audio.db"; break;
            }
        }

        public override string ToString() => dbName;

        protected static string _getDatabaseName()
        {
            return Utils.m_dataPath + "DataBases\\" + dbFile;
        }

        public override string getDatabaseName() => _getDatabaseName();

        public void AddIcon(string name, string filename, int order, int stixID)
        {
            m_db.ExecuteNonQuery("insert into ICONS values(`"
                + name + "`, `"
                + filename + "`, "
                + order + ", "
                + stixID + ", "
                + "'', 0"
                + ");"
                );
        }

        public void AddLinkGroup(string name, int parentID, int order)
        {
            m_db.ExecuteNonQuery("insert into LINKGROUPS values(NULL, `"
                + name + "`, "
                + parentID + ", "
                + order + ", "
                + "'', 0"
                + ");"
            );
        }

        public void AddLink(string title, string path, string type, string state, string comment, int groupID)
        {
            m_db.ExecuteNonQuery("insert into LINKS values(`"
                + title + "`, `"
                + path + "`, `"
                + type + "`, `"
                + state + "`, `"
                + comment + "`, "
                + groupID + ", "
                + "'', '', 0, 0"
                + ");"
            );
        }

        public void AddTool(string title, string tooltip, string path, string type, int order, int stixID, string args = "")
        {
            m_db.ExecuteNonQuery("insert into TOOLS values(`"
                + title + "`, `"
                + tooltip + "`, `"
                + path + "`, `"
                + type + "`, "
                + order + ", "
                + stixID + ", `"
                + args + "`, "
                + "'', '', 0, 0"
                + ");"
            );
        }

        public void AddStix(string name, string type, int start, string orientation, string location)
        {
            m_db.ExecuteNonQuery("insert into STIX values(NULL, `"
                + name + "`, `"
                + type + "`, "
                + start + ", `"
                + orientation + "`, `"
                + location + "`, "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public void AddResource(string name, string color, int groupID)
        {
            m_db.ExecuteNonQuery("insert into RESOURCES values(`"
                + name + "`, `"
                + color + "`, "
                + groupID + ", "
                + "'', 0"
                + ");"
                );
        }

        public void AddResourceGroup(string name)
        {
            m_db.ExecuteNonQuery("insert into RESOURCEGROUPS values(NULL, `"
                + name + "`, "
                + "'', 0"
                + ");"
                );
        }

        public void AddPattern(string templateName, string topicName, string pattern, string topicType)
        {
            m_db.ExecuteNonQuery("insert into ADDTOPIC_TEMPLATES values(NULL, `"
                + templateName + "`, `"
                + topicName + "`, `"
                + pattern + "`, `"
                + topicType + "`, "
                + "'', 0"
                + ");"
            );
        }

        public void AddQuickTopicGroup(string name, int order = 100)
        {
            m_db.ExecuteNonQuery("insert into QUICKTOPICGROUPS values(NULL, `"
                + name + "`, "
                + order + ", "
                + "'', 0"
                + ");"
                );
        }

        public void AddQuickTopicTemplate(string name, int groupID, int order, string topictext, string progress, string priority,
            string dates, string duration, string effort, string icons, string resources, string tags)
        {
            m_db.ExecuteNonQuery("insert into QUICKTOPICTEMPLATES values(NULL, `"
                + name + "`, "
                + groupID + ", "
                + order + ", `"
                + topictext + "`, `"
                + progress + "`, `"
                + priority + "`, `"
                + dates + "`, `"
                + duration + "`, `"
                + effort + "`, `"
                + icons + "`, `"
                + resources + "`, `"
                + tags + "`, "
                + "'', '', '', 0, 0"
                + ");"
                );
        }

        public void AddTopicWidth(string name, int chars, int _value, int _checked)
        {
            m_db.ExecuteNonQuery("insert into TOPICWIDTHS values(`"
                + name + "`, "
                + chars + ", "
                + _value + ", "
                + _checked + ", "
                + "'', 0"
                + ");"
                );
        }

        public void AddBookmarkGroup(string name)
        {
            m_db.ExecuteNonQuery("insert into BOOKMARKGROUPS values(NULL, `"
                + name + "`, "
                + "'', 0"
                + ");"
            );
        }

        public void AddBookmark(string name, string mapPath, string topicGuid, int groupID)
        {
            m_db.ExecuteNonQuery("insert into BOOKMARKS values(`"
                + name + "`, `"
                + mapPath + "`, `"
                + topicGuid + "`, "
                + groupID + ", "
                + "'', 0"
                + ");"
                );
        }

        public void AddAudioGroup(string name)
        {
            m_db.ExecuteNonQuery("insert into AUDIOGROUPS values(NULL, `"
                + name + "`, "
                + "'', 0"
                + ");"
                );
        }

        public void AddAudio(string title, string path, string mappath, string maptitle, string topicguid,
            int groupID)
        {
            m_db.ExecuteNonQuery("insert into AUDIOS values(NULL, `"
                + title + "`, `"
                + path + "`, `"
                + mappath + "`, `"
                + maptitle + "`, `"
                + topicguid + "`, "
                + groupID + ", "
                + "'', '', 0"
                + ");"
                );
        }

        public override void CreateDatabase(string dbFile)
        {
            Random r = new Random();
            int id;

            base.CreateDatabase(dbFile);
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");

            switch (dbFile)
            {
                case "Stix":
                    m_db.ExecuteNonQuery("CREATE TABLE STIX(id INTEGER PRIMARY KEY, name text, " +
                        "type text, start integer, orientation text, location text, " +
                        "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
                    // name = stick name (by user)
                    // type - icons, bookmarks, etc.
                    // start - run stick when MM started
                    // orientation - "H" or "V"
                    // location - 5120,0:5126,363;0,0:2,358 (screen1Location;screen2Location)
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Icons":
                    m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, " +
                        "stixID int, reserved1 text, reserved2 integer);");
                    // filename: file name for stock icons, signature for custom icons
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Bookmarks":
                    m_db.ExecuteNonQuery("CREATE TABLE BOOKMARKGROUPS(id INTEGER PRIMARY KEY, name text, " +
                        "reserved1 text, reserved2 integer);");

                    m_db.ExecuteNonQuery("CREATE TABLE BOOKMARKS(name text, mappath text, topicguid text, groupID integer, " +
                        "reserved1 text, reserved2 integer);");
                    // mappath and topic guid are the unique id for bookmark
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Resources":
                    m_db.ExecuteNonQuery("CREATE TABLE RESOURCES(name text, color string, groupID int, " +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("CREATE TABLE RESOURCEGROUPS(id INTEGER PRIMARY KEY, name text, " +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Tags":
                    m_db.ExecuteNonQuery("CREATE TABLE TAGS(name text, color string, groupID int, " +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("CREATE TABLE TAGGROUPS(id INTEGER PRIMARY KEY, name text, mutexclusive int, " +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Links":
                    m_db.ExecuteNonQuery("CREATE TABLE LINKGROUPS(id INTEGER PRIMARY KEY, " +
                       "name text, parentID int, _order int, " +
                       "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("CREATE TABLE LINKS(title text, path text, type text, " +
                        "state text, comment text, groupID int, " +
                        "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
                    // type - link type (http, word, excel, etc.)
                    // if it's an http or html, "http:" + favicon name as website host
                    // state - processed, important, etc...
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Tools":
                    m_db.ExecuteNonQuery("CREATE TABLE TOOLS(title text, tooltip text, path text, type text, " +
                        "_order integer, stixID int, args text, " +
                        "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
                    // path - file path or
                    //      OmniTool type, starting with "OT_". Eg.: if (path.StartsWith("OT_")) or
                    //      WindowsTool 'appUserModelID', starting with "WT_". Eg.: WT_Microsoft.WindowsCalculator_8wekyb3d8bbwe!App
                    // type - file type (.exe, .docx, .txt, etc.)
                    //      or tool icon filename, ex.: "tool-calculator.png" (stored in the IconDB)
                    // args - pages to open, for word and pdf documents
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Audio":
                    m_db.ExecuteNonQuery("CREATE TABLE AUDIOGROUPS(id INTEGER PRIMARY KEY, name text, " +
                        "reserved1 text, reserved2 integer);");

                    m_db.ExecuteNonQuery("CREATE TABLE AUDIOS(id INTEGER PRIMARY KEY, title text, path text, " +
                        "mappath text, maptitle text, topicguid text, groupID int, timepoints text, " +
                        "reserved1 text, reserved2 integer);");
                    // timepoints - "tagname:seconds;tagname:seconds"
                    m_db.ExecuteNonQuery("END");
                    break;
                case "QuickTopics":
                    m_db.ExecuteNonQuery("CREATE TABLE QUICKTOPICGROUPS(id INTEGER PRIMARY KEY, name text, _order int, " +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("CREATE TABLE QUICKTOPICTEMPLATES(id INTEGER PRIMARY KEY, name text, " +
                        "groupID int, _order int, topictext text, progress text, priority text, dates text, " +
                        "duration text, effort text, icons text, resources text, tags text, properties text, " +
                        "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
                    // topictext - "state$$$topictext"
                    // progress - "state:int
                    // priority - "state:int"
                    // dates - "statestart:statedue$$$abs:16/12/2024;rel:today:N"
                    //          abs - calendar date; rel - period, N - day of week or month
                    // duration - "state:1:2" (0 - minutes, 1 - hours, 2 - days, 3 - weeks, 4 - months)
                    // effort - "state:4:1"
                    // icons - same as in ICONS (file name for stock icons, signature for custom icons)
                    // resources - "state:resources"
                    // tags - "state;group:tag;group:tag"
                    // properties - "state;name:value:type;name:value:type;name:value:type"

                    // state: "checked", "checkedred", "uncheckedred"
                    m_db.ExecuteNonQuery("END");
                    break;
                case "AddTopics":
                    m_db.ExecuteNonQuery("CREATE TABLE ADDTOPIC_TEMPLATES(id INTEGER PRIMARY KEY, " +
                        "templateName text, topicName text, pattern text, topicType text, " +
                        "reserved1 text, reserved2 integer);");
                    // pattern_data:
                    // "topics###5" - 5 topcs with topic text _topicName_
                    // "custom###topic1###topic2###topic3###etc..."
                    // "increment###start,end,step,position"
                    // topicType: "subtopic", "nexttopic" or "topicbefore"
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Misc":
                    m_db.ExecuteNonQuery("CREATE TABLE TOPICWIDTHS(name text, chars int, _value int, _checked int," +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Lego":
                    m_db.ExecuteNonQuery("CREATE TABLE LEGOCONFIGS(id INTEGER PRIMARY KEY, name text, " +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("CREATE TABLE LEGOSTIX(configID integer, tools text" +
                        "reserved1 text, reserved2 integer);");
                    m_db.ExecuteNonQuery("END");
                    break;
                case "Stickers":

                    m_db.ExecuteNonQuery("END");
                    break;

                    // Get created group id
                    //int groupID = 1;
                    //DataTable dt = ExecuteQuery("SELECT last_insert_rowid()");
                    //if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        public void AddSnippet(string snippet)
        {
            m_db.ExecuteNonQuery("insert into SNIPPETS values(`"
                + snippet + "`, "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public void AddNoteGroup(string name)
        {
            m_db.ExecuteNonQuery("insert into NOTEGROUPS values(NULL, `"
                + name + "`, "
                + "'', 0"
                + ");"
                );
        }

        public void AddNote(string name, string content, string link, int groupID = 0,
            string icon1 = "", string icon2 = "", string tags = "")
        {
            m_db.ExecuteNonQuery("insert into NOTES values(NULL, `"
                + name + "`, `"
                + content + "`, `"
                + link + "`, "
                + groupID + ", `"
                + icon1 + "`, `"
                + icon2 + "`, `"
                + tags + "`, "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public void AddNoteIcon(string name, string filename, int order)
        {
            m_db.ExecuteNonQuery("insert into NOTEICONS values(NULL, `"
                + name + "`, `"
                + filename + "`, "
                + order + ", "
                + "'', 0"
                + ");"
                );
        }
    }
}
