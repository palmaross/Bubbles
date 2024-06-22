using System;
using System.Data;
using StixAppManager;

namespace Bubbles
{
    internal class StixDB : DatabaseWrapper
    {
        public override string ToString() => "Stix Database";

        protected static string _getDatabaseName()
        {
            string path = Utils.m_defaultDataPath;
            return path + "stix.db";
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

        public void AddStix(int id, string name, string type, int start, string orientation, string location)
        {
            m_db.ExecuteNonQuery("insert into STIX values("
                + id + ", `"
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

        public void AddAudio(string title, string path, int length, string mappath, string topicguid, 
            int groupID, string timepoints)
        {
            m_db.ExecuteNonQuery("insert into AUDIOS values(NULL, `"
                + title + "`, `"
                + path + "`, "
                + length + ", `"
                + mappath + "`, `"
                + topicguid + "`, "
                + groupID + ", `"
                + timepoints + "`, "
                + "'', 0"
                + ");"
                );
        }

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");

            m_db.ExecuteNonQuery("CREATE TABLE STIX(id integer unique, name text, " +
                "type text, start integer, orientation text, location text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // name = stick name (by user)
            // type - icons, bookmarks, etc.
            // start - run stick when MM started
            // orientation - "H" or "V"
            // location - 5120,0:5126,363;0,0:2,358 (screen1Location;screen2Location)

            //// Stix ////
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, " +
                "stixID int, reserved1 text, reserved2 integer);");
            // filename: file name for stock icons, signature for custom icons

            m_db.ExecuteNonQuery("CREATE TABLE BOOKMARKGROUPS(id INTEGER PRIMARY KEY, name text, " +
                "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE BOOKMARKS(name text, mappath text, topicguid text, groupID integer, " +
                "reserved1 text, reserved2 integer);");
            // mappath and topic guid are the unique id for bookmark

            m_db.ExecuteNonQuery("CREATE TABLE RESOURCES(name text, color string, groupID int, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE RESOURCEGROUPS(id INTEGER PRIMARY KEY, name text, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TAGS(name text, color string, groupID int, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TAGGROUPS(id INTEGER PRIMARY KEY, name text, mutexclusive int, " +
                "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE LINKGROUPS(id INTEGER PRIMARY KEY, " +
                "name text, parentID int, _order int, " +
                "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE LINKS(title text, path text, type text, " +
                "state text, comment text, groupID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // state - processed, important, etc...

            m_db.ExecuteNonQuery("CREATE TABLE TOOLS(title text, tooltip text, path text, type text, " +
                "_order integer, stixID int, args text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // path - file path or
            //      OmniTool type, starting with "OT_". Eg.: if (path.StartsWith("OT_")) or
            //      WindowsTool 'appUserModelID', starting with "WT_". Eg.: WT_Microsoft.WindowsCalculator_8wekyb3d8bbwe!App
            // type - file type (.exe, .docx, .txt, etc.)
            //      or tool icon filename, ex.: "tool-calculator.png" (stored in the IconDB)
            // args - pages to open, for word and pdf documents

            m_db.ExecuteNonQuery("CREATE TABLE QUICKTOPICGROUPS(id INTEGER PRIMARY KEY, name text, _order int, " +
                "reserved1 text, reserved2 integer);");

            // Quick topics
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

            // Add Topic templates
            m_db.ExecuteNonQuery("CREATE TABLE ADDTOPIC_TEMPLATES(id INTEGER PRIMARY KEY, " +
                "templateName text, topicName text, pattern text, topicType text, " +
                "reserved1 text, reserved2 integer);");
            // pattern_data:
            // "topics###5" - 5 topcs with topic text _topicName_
            // "custom###topic1###topic2###topic3###etc..."
            // "increment###start,end,step,position"
            // topicType: "subtopic", "nexttopic" or "topicbefore"

            m_db.ExecuteNonQuery("CREATE TABLE TOPICWIDTHS(name text, chars int, _value int, _checked int," +
                "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE AUDIOGROUPS(id INTEGER PRIMARY KEY, name text, " +
                "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE AUDIOS(id INTEGER PRIMARY KEY,title text, path text, " +
                "length integer, mappath text, topicguid text, groupID int, timepoints text, " +
                "reserved1 text, reserved2 integer);");
            // timepoints - "tagname:seconds;tagname:seconds"

            m_db.ExecuteNonQuery("END");

            // Add Base stick
            Random r = new Random();

            int id = r.Next();
            // Add first Icons stick
            AddStix(id, Utils.getString("StixIcons.tooltip"), StixUtils.typeicons, 0, "H", "");

            AddIcon(Utils.getString("icons.firststick.icon1"), "stockexclamation-mark", 1, id);
            AddIcon(Utils.getString("icons.firststick.icon2"), "stockquestion-mark", 2, id);

            // Add TaskInfo stick
            id = r.Next();
            AddStix(id, Utils.getString("StixTaskInfo.tooltip"), StixUtils.typetaskinfo, 0, "H", "");

            // Add first Tools stick
            id = r.Next();
            AddStix(id, Utils.getString("StixTools.tooltip"), StixUtils.typetools, 0, "H", "");

            AddLinkGroup(Utils.getString("LinksDlg.commongroup"), 0, 1);
            // Get created group id
            int groupID = 1;
            DataTable dt = ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);

#if VENDOR_OL
            {
                AddLink(Utils.getString("tools.demo1.title"), "http://www.olympic-limited.co.uk/", "http", groupID);
                AddTool(Utils.getString("tools.demo1.title"), "", "http://www.olympic-limited.co.uk/", "http", 1, id);

            }
#else
            {
                AddLink(Utils.getString("tools.demo1.title"), "https://palmaross.com/", "http", "", "", groupID);
                AddTool(Utils.getString("tools.demo1.title"), "", "https://palmaross.com/", "http", 1, id);
            }
#endif

            AddLink(Utils.getString("tools.demo2.title"), Utils.dllPath + "OmniStix.chm", "chm", "", "", groupID);
            // Add tools to stix
            AddTool(Utils.getString("tools.demo2.title"), "", Utils.dllPath + "OmniStix.chm", "chm", 2, id);
            AddTool(Utils.getString("tools.notepad"), "", "WT_Microsoft.WindowsNotepad_8wekyb3d8bbwe!App", "tool-winnotepad.png", 3, id);

            AddTool(Utils.getString("tools.closeall"), Utils.getString("tools.closeall.tooltip"), "OT_CloseAll", "tool-closemaps.png", 0, 0);
            AddTool(Utils.getString("tools.saveall"), Utils.getString("tools.saveall.tooltip"), "OT_SaveAll", "tool-saveall.png", 0, 0);
            AddTool(Utils.getString("tools.mapcontent"), Utils.getString("tools.mapcontent.tooltip"), "OT_MapContent", "tool-mapcontent.png", 0, 0);
            AddTool(Utils.getString("tools.readonlytopic"), Utils.getString("tools.readonlytopic.tooltip"), 
                Utils.m_dataPath + "ToolStixApps\\read-only-topic.mmbas", "tool-locktopic.png", 0, 0);

            AddLinkGroup("Group 1", 0, 2);
            groupID = 1;
            dt = ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);

            AddLinkGroup("Group 1.1", groupID, 1);
            AddLinkGroup("Group 1.2", groupID, 2);

            AddLink(Utils.getString("tools.demo4.title"), "https://www.youtube.com/watch?v=U92A8H2rK2I", "youtube", "", "", groupID);

            // Add Bookmarks stick
            id = r.Next();
            AddStix(id, Utils.getString("StixMapNavigator.tooltip"), StixUtils.typemapnavigator, 0, "H", "");

            // Add <Add Topic> stick
            id = r.Next();
            AddStix(id, Utils.getString("StixAddTopic.tooltip"), StixUtils.typeaddtopic, 0, "H", "");

            // Add Text Operations stick
            id = r.Next();
            AddStix(id, Utils.getString("StixTextOps.tooltip"), StixUtils.typetextops, 0, "H", "");

            // Add Format stick
            id = r.Next();
            AddStix(id, Utils.getString("StixFormat.tooltip"), StixUtils.typeformat, 0, "H", "");

            // Add ADDTOPIC_TEMPLATES
            AddPattern(Utils.getString("Template.Day"), Utils.getString("Template.Day") + " ", "increment###1,10,1,end", "subtopic");
            AddPattern(Utils.getString("Template.Month"), Utils.getString("Template.January") + " ", "increment###1,31,1,end", "subtopic");
            AddPattern(Utils.getString("Template.Task"), Utils.getString("Template.Task") + " ", "increment###1,5,1,end", "subtopic");
            AddPattern(Utils.getString("Template.WeekDays"), "", Utils.getString("Template.WeekDays.lang"), "subtopic");

            // Add Resources

            int _id = 0;
            AddResourceGroup(Utils.getString("taskinfo.database.resourcegroup1"));
            // Get created group id
            DataTable _dt = ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) _id = Convert.ToInt32(_dt.Rows[0][0]);
            // Add Resources to group
            AddResource(Utils.getString("taskinfo.database.resources.res1"), "", _id);
            AddResource(Utils.getString("taskinfo.database.resources.res2"), "", _id);

            AddResourceGroup(Utils.getString("taskinfo.database.resourcegroup2"));
            // Get created group id
            dt = ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) _id = Convert.ToInt32(dt.Rows[0][0]); else _id = 1;
            // Add Resources to group
            AddResource(Utils.getString("taskinfo.database.resources.res3"), "#ff80ff80", _id);
            AddResource(Utils.getString("taskinfo.database.resources.res4"), "#ffffff80", _id);

            // Add Task Templates
            AddQuickTopicGroup(Utils.getString("quicktopic.favorites"), 1);
            AddQuickTopicTemplate(Utils.getString("quicktask.template.default"), 1, 1, "", "checked:0", "", "checked:checked$$$rel:today:1;rel:today:1", "", "", "", "", "");
            AddQuickTopicTemplate(Utils.getString("quicktask.template.important"), 1, 2, "", "", "checked:1", "checked:checked$$$rel:today:1;rel:today:1", "", "", "", "", "");
            AddQuickTopicTemplate(Utils.getString("quicktask.template.completed"), 1, 3, "", "checked:100", "", ":checked$$$;rel:today:1", "", "", "", "", "");

            // Add values for Topic Width dialog
            AddTopicWidth("numMainWidth", 0, 64, 1);
            AddTopicWidth("numWidth1", 0, 100, 1); AddTopicWidth("numWidth2", 0, 120, 1);
            AddTopicWidth("numWidth3", 0, 150, 1); AddTopicWidth("numWidth4", 0, 180, 1);
            AddTopicWidth("numWidth5", 0, 200, 0); AddTopicWidth("numWidth6", 0, 200, 0);
            AddTopicWidth("numAuto1", 500, 200, 1); AddTopicWidth("numAuto2", 200, 160, 1);
            AddTopicWidth("numAuto3", 150, 120, 1); AddTopicWidth("numAuto4", 150, 120, 1);
            AddTopicWidth("numAuto5", 150, 120, 1); AddTopicWidth("numAuto6", 150, 120, 1);

            AddBookmarkGroup(Utils.getString("BookmarksDlg.defaultgroup"));
        }
    }
}
