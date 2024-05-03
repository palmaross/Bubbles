using System;
using System.Data;
using BubblesAppManager;

namespace Bubbles
{
    internal class StixDB : DatabaseWrapper
    {
        public override string ToString() => "Bubbles Database";

        protected static string _getDatabaseName()
        {
            string path = Utils.m_defaultDataPath;
            return path + "stix.db";
        }

        public override string getDatabaseName() => _getDatabaseName();

        public void AddIcon(string name, string filename, int order, int stickID)
        {
            m_db.ExecuteNonQuery("insert into ICONS values(`"
                + name + "`, `"
                + filename + "`, "
                + order + ", "
                + stickID + ", "
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

        public void AddTool(string title, string path, string type, int order, int stickID)
        {
            m_db.ExecuteNonQuery("insert into TOOLS values(`"
                + title + "`, `"
                + path + "`, `"
                + type + "`, "
                + order + ", "
                + stickID + ", "
                + "'', '', 0, 0"
                + ");"
            );
        }

        public void AddStick(int id, string name, string type, int start, string orientation, string location)
        {
            m_db.ExecuteNonQuery("insert into STICKS values("
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

        public void AddConfig(string name, int start)
        {
            m_db.ExecuteNonQuery("insert into CONFIGS values(NULL, `"
                + name + "`, "
                + start + ", "
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

        public void AddTaskTemplate(int primary, string name, string topictext, string progress, string priority, 
            string dates, string duration, string effort, string icon, string resources, string tags)
        {
            m_db.ExecuteNonQuery("insert into TASKTEMPLATES values("
                + primary + ", `"
                + name + "`, `"
                + topictext + "`, `"
                + progress + "`, `"
                + priority + "`, `"
                + dates + "`, `"
                + duration + "`, `"
                + effort + "`, `"
                + icon + "`, `"
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

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");

            m_db.ExecuteNonQuery("CREATE TABLE STICKS(id integer unique, name text, " +
                "type text, start integer, orientation text, location text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // name = stick name (by user)
            // type - icons, bookmarks, etc.
            // start - run sticker when MM started
            // orientation - "H" or "V"
            // location - 5120,0:5126,363;0,0:2,358 (screen1Location;screen2Location)

            //// Stix ////
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, " +
                "stickID int, reserved1 text, reserved2 integer);");
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

            m_db.ExecuteNonQuery("CREATE TABLE TOOLS(title text, path text, type text, _order integer, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // path - file path
            // type - file type (.exe, .docx, .txt, etc.) or OmniTool type, starting with "OT"
            //        ex.: if (type.StartsWith("OT")) ... else it's a file
            //        or filename, ex.: "tool-calculator.png" (stored in the IconDB)

            // Quick tasks
            m_db.ExecuteNonQuery("CREATE TABLE TASKTEMPLATES(prime int, name text, topictext text, " +
                "progress text, priority text, dates text, duration text, effort text, " +
                "icon text, resources text, tags text, properties text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // topictext - "state$$$topictext"
            // progress - "state:int
            // priority - "state:int"
            // dates - "statestart:statedue$$$abs:16/12/2024;rel:today:N"
            //          abs - calendar date; rel - period, N - day of week or month
            // duration - "state:1:2" (0 - minutes, 1 - hours, 2 - days, 3 - weeks, 4 - months)
            // effort - "state:4:1"
            // icon - same as in ICONS (file name for stock icons, signature for custom icons)
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

            ////m_db.ExecuteNonQuery("CREATE TABLE OMNISOUNDS(filename text, mappath, topicguid text, " +
            ////    "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("END");

            // Add Base stick
            Random r = new Random();

            int id = r.Next();
            // Add first Icons stick
            AddStick(id, Utils.getString("StixIcons.tooltip"), StixUtils.typeicons, 0, "H", "");

            AddIcon(Utils.getString("icons.firststick.icon1"), "stockexclamation-mark", 1, id);
            AddIcon(Utils.getString("icons.firststick.icon2"), "stockquestion-mark", 2, id);

            // Add TaskInfo stick
            id = r.Next();
            AddStick(id, Utils.getString("StixTaskInfo.tooltip"), StixUtils.typetaskinfo, 0, "H", "");

            // Add first Tools stick
            id = r.Next();
            AddStick(id, Utils.getString("StixTools.tooltip"), StixUtils.typetools, 0, "H", "");

            AddLinkGroup(Utils.getString("LinksDlg.commongroup"), 0, 1);
            // Get created group id
            int groupID = 1;
            DataTable dt = ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);

#if VENDOR_OL
            {
            AddLink(Utils.getString("tools.demo1.title"), "http://www.olympic-limited.co.uk/", "http", groupID);
            AddTool(Utils.getString("tools.demo1.title"), "http://www.olympic-limited.co.uk/", "http", 1, id);

            }
#else
            {
                AddLink(Utils.getString("tools.demo1.title"), "https://palmaross.com/", "http", "", "", groupID);
            AddTool(Utils.getString("tools.demo1.title"), "https://palmaross.com/", "http", 1, id);
            }
#endif

            AddLink(Utils.getString("tools.demo2.title"), Utils.dllPath + "OmniStix.chm", "chm", "", "", groupID);
            AddTool(Utils.getString("tools.demo2.title"), Utils.dllPath + "OmniStix.chm", "chm", 2, id);
            AddTool(Utils.getString("tools.demo3.title"), "c:\\Windows\\System32\\notepad.exe", "tool-notepad.png", 3, id);
            
            AddLinkGroup("Group 1", 0, 2);
            groupID = 1;
            dt = ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) groupID = Convert.ToInt32(dt.Rows[0][0]);

            AddLinkGroup("Group 1.1", groupID, 1);
            AddLinkGroup("Group 1.2", groupID, 2);

            AddLink(Utils.getString("tools.demo4.title"), "https://www.youtube.com/watch?v=U92A8H2rK2I", "youtube", "", "", groupID);

            // Add Bookmarks stick
            id = r.Next();
            AddStick(id, Utils.getString("StixBookmarks.tooltip"), StixUtils.typebookmarks, 0, "H", "");

            // Add <Add Topic> stick
            id = r.Next();
            AddStick(id, Utils.getString("StixAddTopic.tooltip"), StixUtils.typeaddtopic, 0, "H", "");

            // Add Text Operations stick
            id = r.Next();
            AddStick(id, Utils.getString("StixTextOps.tooltip"), StixUtils.typetextops, 0, "H", "");

            // Add Format stick
            id = r.Next();
            AddStick(id, Utils.getString("StixFormat.tooltip"), StixUtils.typeformat, 0, "H", "");

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
            AddTaskTemplate(1, Utils.getString("quicktask.template.default"), "", "checked:0", "", "checked:checked$$$rel:today:1;rel:today:1", "", "", "", "", "");
            AddTaskTemplate(0, Utils.getString("quicktask.template.important"), "", "", "checked:1", "checked:checked$$$rel:today:1;rel:today:1", "", "", "", "", "");
            AddTaskTemplate(0, Utils.getString("quicktask.template.completed"), "", "checked:100", "", ":checked$$$;rel:today:1", "", "", "", "", "");

            // Add values for Topic Width dialog
            AddTopicWidth("numMainWidth", 0, 64, 1);
            AddTopicWidth("numWidth1", 0, 100, 1); AddTopicWidth("numWidth2", 0, 120, 1);
            AddTopicWidth("numWidth3", 0, 150, 1); AddTopicWidth("numWidth4", 0, 180, 1);
            AddTopicWidth("numWidth5", 0, 200, 0); AddTopicWidth("numWidth6", 0, 200, 0);
            AddTopicWidth("numAuto1", 500, 200, 1); AddTopicWidth("numAuto2", 200, 160, 1);
            AddTopicWidth("numAuto3", 150, 120, 1); AddTopicWidth("numAuto4", 150, 120, 1);
            AddTopicWidth("numAuto5", 150, 120, 1); AddTopicWidth("numAuto6", 150, 120, 1);
        }
    }
}
