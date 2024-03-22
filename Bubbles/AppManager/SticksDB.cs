using System;
using BubblesAppManager;

namespace Bubbles
{
    internal class SticksDB : DatabaseWrapper
    {
        public override string ToString() => "Bubbles Database";

        protected static string _getDatabaseName()
        {
            string path = Utils.m_defaultDataPath;
            return path + "sticks.db";
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

        public void AddSource(string title, string path, string type, int order, int stickID)
        {
            m_db.ExecuteNonQuery("insert into SOURCES values(`"
                + title + "`, `"
                + path + "`, `"
                + type + "`, "
                + order + ", "
                + stickID + ", "
                + "'', '', 0, 0"
                + ");"
            );
        }

        public void AddStick(int id, string name, string type, int start, string position, int configID, int group)
        {
            m_db.ExecuteNonQuery("insert into STICKS values("
                + id + ", `"
                + name + "`, `"
                + type + "`, "
                + start + ", `"
                + position + "`, "
                + configID + ", "
                + group + ", "
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
            m_db.ExecuteNonQuery("insert into MT_TEMPLATES values(NULL, `"
                + templateName + "`, `"
                + topicName + "`, `"
                + pattern + "`, `"
                + topicType + "`, "
                + "'', 0"
                + ");"
            );
        }

        public void AddTaskTemplate(int primary, string name, int progress, int priority, string dates,
            string icon, string resources, string tags)
        {
            m_db.ExecuteNonQuery("insert into TASKTEMPLATES values("
                + primary + ", `"
                + name + "`, "
                + progress + ", "
                + priority + ", `"
                + dates + "`, `"
                + icon + "`, `"
                + resources + "`, `"
                + tags + "`, "
                + "'', '', '', 0, 0"
                + ");"
                );
        }

        public void AddSticker(string content, string textcolor, string fillcolor,
            string fontfamily, int textsize, int textbold, string sticksize, string image, string alignment, string type)
        {
            m_db.ExecuteNonQuery("insert into STICKERS values(NULL, `"
                + content + "`, `"
                + textcolor + "`, `"
                + fillcolor + "`, `"
                + fontfamily + "`, "
                + textsize + ", "
                + textbold + ", `"
                + sticksize + "`, `"
                + image + "`, `"
                + alignment + "`, `"
                + type + "`, "
                + "'', '', 0, 0"
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

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");

            m_db.ExecuteNonQuery("CREATE TABLE CONFIGS(id INTEGER PRIMARY KEY, name text, start int, " +
               "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE MT_TEMPLATES(id INTEGER PRIMARY KEY, " +
                "templateName text, topicName text, pattern text, topicType text, " +
                "reserved1 text, reserved2 integer);");
            // pattern_data:
            // "topics###5" - 5 topcs with topic text _topicName_
            // "custom###topic1###topic2###topic3###etc..."
            // "increment###start,end,step,position"
            // topicType: "subtopic", "nexttopic" or "topicbefore"
            m_db.ExecuteNonQuery("CREATE TABLE STICKS(id integer unique, name text, " +
                "type text, start integer, position text, configID integer, _group integer, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // name = stick name (by user)
            // type - icons, bookmarks, etc.
            // start - run sticker when MM started
            // group: 0 - no group, 1 - no mutually exclusive group, 2 - mutually exclusive group
            // position - H#5120,0:5126,363;0,0:2,358 (Horizontal;screen1Location;screen2Location)

            //// Sticks ////
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, " +
                "stickID int, reserved1 text, reserved2 integer);");
            // group: 0 - no group, 1 - no mutually exclusive group, 2 - mutually exclusive group
            // filename: file name for stock icons, signature for custom icons
            //m_db.ExecuteNonQuery("CREATE TABLE ICONGROUPS(id INTEGER PRIMARY KEY, name text, " +
            //    "mutexclusive int, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE RESOURCES(name text, color string, groupID int, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE RESOURCEGROUPS(id INTEGER PRIMARY KEY, name text, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TAGS(name text, color string, groupID int, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TAGGROUPS(id INTEGER PRIMARY KEY, name text, mutexclusive int, " +
                "reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE SOURCES(title text, path text, type text, _order integer, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");

            m_db.ExecuteNonQuery("CREATE TABLE TASKTEMPLATES(prime int, name text, progress int, " +
                "priority int, dates text, icon text, resources text, tags text, properties text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // dates - "abs:16/12/2024;rel:today:N"
            // abs - calendar date; rel - period, N - day of week or month
            // icon - same as in ICONS
            // tags - group:tag;group:tag
            // properties - name:value:type;name:value:type;name:value:type

            m_db.ExecuteNonQuery("CREATE TABLE TOPICWIDTHS(name text, chars int, _value int, _checked int," +
                "reserved1 text, reserved2 integer);");

            // Stickers
            m_db.ExecuteNonQuery("CREATE TABLE STICKERS(id INTEGER PRIMARY KEY, content text, textcolor text, fillcolor text, " +
                "fontfamily text, textsize integer, textbold integer, sticksize text, image text, alignment text, type text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // type:
            // "sticker"
            // "template"
            // "reminder:12:05" (12:05) or "reminder:20" (in 20 minutes)
            // "timer" обратный отсчет
            // "stopwatch" секундомер
            // "autoplay:interval:random"

            m_db.ExecuteNonQuery("CREATE TABLE STICKERTEXTS(id INTEGER PRIMARY KEY, stickid int, " +
                "stickertext text, reserved1 text, reserved2 integer);");

            m_db.ExecuteNonQuery("END");

            // Add first My Icons stick
            Random r = new Random();

            int id = r.Next();
            AddStick(id, Utils.getString("BubbleIcons.bubble.tooltip"), StickUtils.typeicons, 0, "", 0, 0);

            AddIcon(Utils.getString("icons.firststick.icon1"), "stockexclamation-mark", 1, id);
            AddIcon(Utils.getString("icons.firststick.icon2"), "stockquestion-mark", 2, id);

            // Add TaskInfo stick
            id = r.Next();
            AddStick(id, Utils.getString("BubbleTaskInfo.bubble.tooltip"), StickUtils.typetaskinfo, 0, "", 0, 0);

            // Add first My Sources stick
            id = r.Next();
            AddStick(id, Utils.getString("BubbleMySources.bubble.tooltip"), StickUtils.typesources, 0, "", 0, 0);

            AddSource(Utils.getString("mysources.first1.text"), "https://palmaross.com/", "http", 1, id);
            AddSource(Utils.getString("mysources.first2.text"), Utils.dllPath + "WowStix.chm", "file", 2, id);
            AddSource(Utils.getString("mysources.first3.text"), "c:\\Windows\\System32\\notepad.exe", "exe", 3, id);

            // Add Bookmarks stick
            id = r.Next();
            AddStick(id, Utils.getString("BubbleBookmarks.bubble.tooltip"), StickUtils.typebookmarks, 0, "", 0, 0);

            // Add <Add Topic> stick
            id = r.Next();
            AddStick(id, Utils.getString("BubbleAddTopic.bubble.tooltip"), StickUtils.typeaddtopic, 0, "", 0, 0);

            // Add Text Operations stick
            id = r.Next();
            AddStick(id, Utils.getString("BubblePaste.bubble.tooltip"), StickUtils.typepaste, 0, "", 0, 0);

            // Add Format stick
            id = r.Next();
            AddStick(id, Utils.getString("BubbleFormat.bubble.tooltip"), StickUtils.typeformat, 0, "", 0, 0);

            // Add MT_Templates
            AddPattern(Utils.getString("Template.Day"), Utils.getString("Template.Day") + " ", "increment###1,10,1,end", "subtopic");
            AddPattern(Utils.getString("Template.Month"), Utils.getString("Template.January") + " ", "increment###1,31,1,end", "subtopic");
            AddPattern(Utils.getString("Template.Task"), Utils.getString("Template.Task") + " ", "increment###1,5,1,end", "subtopic");
            AddPattern(Utils.getString("Template.WeekDays"), "", Utils.getString("Template.WeekDays.lang"), "subtopic");

            // Add Resource Groups (resources icons for now)
            AddResourceGroup(Utils.getString("taskinfo.database.resourcegroup1"));
            // Add Resources
            AddResource(Utils.getString("taskinfo.database.resources.res1"), "", 0);
            AddResource(Utils.getString("taskinfo.database.resources.res2"), "", 0);
            AddResource(Utils.getString("taskinfo.database.resources.res3"), "", 1);
            AddResource(Utils.getString("taskinfo.database.resources.res4"), "", 1);

            // Add Task Templates
            AddTaskTemplate(1, Utils.getString("quicktask.template.default"), 0, 0, "rel:today:1;rel:today:1", "", "", "");
            AddTaskTemplate(0, Utils.getString("quicktask.template.important"), 0, 1, "rel:today:1;rel:today:1", "", "", "");
            AddTaskTemplate(0, Utils.getString("quicktask.template.completed"), 100, 0, ";rel:today:1", "", "", "");

            // Add values for Topic Width dialog
            AddTopicWidth("numMainWidth", 0, 64, 1);
            AddTopicWidth("numWidth1", 0, 100, 1); AddTopicWidth("numWidth2", 0, 120, 1);
            AddTopicWidth("numWidth3", 0, 150, 1); AddTopicWidth("numWidth4", 0, 180, 1);
            AddTopicWidth("numWidth5", 0, 200, 0); AddTopicWidth("numWidth6", 0, 200, 0);
            AddTopicWidth("numAuto1", 500, 200, 1); AddTopicWidth("numAuto2", 200, 160, 1);
            AddTopicWidth("numAuto3", 150, 120, 1); AddTopicWidth("numAuto4", 150, 120, 1);
            AddTopicWidth("numAuto5", 150, 120, 1); AddTopicWidth("numAuto6", 150, 120, 1);

            // Add a couple of sticker templates
            AddSticker(Utils.getString("stickertemplate1.text"), "#ff0000ff", "#ff00ffff", "Segoe Print",
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
            AddSticker(Utils.getString("stickertemplate2.text"), "#ff0000ff", "#ff0000ff", "Segoe Print",
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
            AddSticker(Utils.getString("stickertemplate3.text"), "#ff0000ff", "#ff00ff00", "Segoe Print",
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
        }
    }
}
