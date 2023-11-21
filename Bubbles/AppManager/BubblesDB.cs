using BubblesAppManager;
using System.Data;
using System;
using PRAManager;

namespace Bubbles
{
    internal class BubblesDB : DatabaseWrapper
    {
        public override string ToString() => "Bubbles Database";

        protected static string _getDatabaseName()
        {
            string path = Utils.m_defaultDataPath;
            return path + "bubbles.db";
        }

        public override string getDatabaseName() => _getDatabaseName();

        public void AddSnippet(string snippet)
        {
            m_db.ExecuteNonQuery("insert into SNIPPETS values(`"
                + snippet + "`, "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public void AddIcon(string name, string filename, int order, int stickID)
        {
            m_db.ExecuteNonQuery("insert into ICONS values(`"
                + name + "`, `"
                + filename + "`, "
                + order + ", "
                + stickID + ", "
                + "'', '', 0, 0"
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

        public void AddSticker(string content, string textcolor, string fillcolor, 
            string fontfamily, int textsize, int textbold, string sticksize, string image, string alignment,  string type)
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

        public void AddStick(int id, string name, string type, int start, string position, int configID)
        {
            m_db.ExecuteNonQuery("insert into STICKS values("
                + id + ", `"
                + name + "`, `"
                + type + "`, "
                + start + ", `"
                + position + "`, "
                + configID + ", "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public void AddPriPro(string type, int value, int stickID)
        {
            m_db.ExecuteNonQuery("insert into PRIPRO values(`"
                + type + "`, "
                + value + ", "
                + stickID + ", "
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

        public void AddConfig(string name, int start)
        {
            m_db.ExecuteNonQuery("insert into CONFIGS values(NULL, `"
                + name + "`, "
                + start + ", "
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

            m_db.ExecuteNonQuery("CREATE TABLE STICKS(id integer, name text, " +
                "type text, start integer, position text, configID integer, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // name = stick name (by user)
            // type - icons, bookmarks, etc.
            // start - run sticker when MM started
            // position - H#5120,0:5126,363;0,0:2,358 (Horizontal;screen1Location;screen2Location)

            m_db.ExecuteNonQuery("CREATE TABLE NOTEGROUPS(id INTEGER PRIMARY KEY, name text, " +
                "reserved1 text, reserved2 integer);");

            //// Sticks ////
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE PRIPRO(type text, value text, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // type - "priority" or "progress"
            // value - priority or progress value
            m_db.ExecuteNonQuery("CREATE TABLE SOURCES(title text, path text, type text, _order integer, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            
            // Organizer
            m_db.ExecuteNonQuery("CREATE TABLE NOTES(id INTEGER PRIMARY KEY, name text, content text, " +
                "link text, groupID int, icon1 text, icon2 text, tags text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE NOTEICONS(id INTEGER PRIMARY KEY, name text, fileName text, _order int, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE NOTETAGS(tag text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE IDEAS(content text, rating text, groupID int, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE LINKS(id INTEGER PRIMARY KEY, title text, link text, groupID int, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE SNIPPETS(snippet text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TODOS(id INTEGER PRIMARY KEY, todo text, datetime text, groupID int, " +
                "reserved1 text, reserved2 integer);");
            
            // Stickers
            m_db.ExecuteNonQuery("CREATE TABLE STICKERS(id INTEGER PRIMARY KEY, content text, textcolor text, fillcolor text, " +
                "fontfamily text, textsize integer, textbold integer, sticksize text, image text, alignment text, type text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // type:
            // "sticker"
            // "template"
            // "reminder:12:05" (12:05) or "reminder:20" (in 20 minutes)
            m_db.ExecuteNonQuery("END");

            // Add first Icons stick
            int id = Utils.StickID();
            AddStick(id, Utils.getString("BubbleIcons.bubble.tooltip"), StickUtils.typeicons, 0, "", 0);

            AddIcon(Utils.getString("icons.firststick.icon1"), "stockexclamation-mark", 1, id); 
            AddIcon(Utils.getString("icons.firststick.icon2"), "stockquestion-mark", 2, id);

            // Add first PriPro stick
            id = Utils.StickID();
            AddStick(id, Utils.getString("BubblePriPro.bubble.tooltip"), StickUtils.typepripro, 0, "", 0);

            AddPriPro("pri", 1, id); AddPriPro("pri", 2, id);
            AddPriPro("pro", 0, id); AddPriPro("pro", 100, id);

            // Add first sources
            id = Utils.StickID();
            AddStick(id, Utils.getString("BubbleMySources.bubble.tooltip"), StickUtils.typesources, 0, "", 0);

            AddSource(Utils.getString("mysources.first1.text"), "https://palmaross.com/", "http", 1, id);
            AddSource(Utils.getString("mysources.first2.text"), Utils.dllPath + "Sticks.chm", "file", 2, id);
            AddSource(Utils.getString("mysources.first3.text"), "c:\\Windows\\System32\\notepad.exe", "exe", 3, id);

            // Add a couple of sticker templates
            AddSticker(Utils.getString("stickertemplate1.text"), "#ff0000ff", "#ff00ffff", "Segoe Print", 
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
            AddSticker(Utils.getString("stickertemplate2.text"), "#ff0000ff", "#ff0000ff", "Segoe Print", 
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
            AddSticker(Utils.getString("stickertemplate3.text"), "#ff0000ff", "#ff00ff00", "Segoe Print", 
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");

            // Add Note Icons
            AddNoteIcon(Utils.getString("noteicons.icon1"), "stockmarker2", 1);
            AddNoteIcon(Utils.getString("noteicons.icon2"), "stockmarker3", 2);
            AddNoteIcon(Utils.getString("noteicons.icon3"), "stockmarker4", 3);
            AddNoteIcon("", "", 4); AddNoteIcon("", "", 5); AddNoteIcon("", "", 6); 
            AddNoteIcon("", "", 7); AddNoteIcon("", "", 8);
            AddNoteIcon(Utils.getString("noteicons.icon9"), "stockexclamation-mark", 1);
            AddNoteIcon(Utils.getString("noteicons.icon10"), "stockquestion-mark", 2);
            AddNoteIcon(Utils.getString("noteicons.icon11"), "stocklightbulb", 3);
            AddNoteIcon("", "", 4); AddNoteIcon("", "", 5); AddNoteIcon("", "", 6); 
            AddNoteIcon("", "", 7); AddNoteIcon("", "", 8);
        }
    }
}
