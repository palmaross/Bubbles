using BubblesAppManager;
using System;

namespace Bubbles
{
    internal class SticksMoreDB : DatabaseWrapper
    {
        public override string ToString() => "Bubbles Database";

        protected static string _getDatabaseName()
        {
            string path = Utils.m_defaultDataPath;
            return path + "sticks_more.db";
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

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");


            m_db.ExecuteNonQuery("CREATE TABLE NOTEGROUPS(id INTEGER PRIMARY KEY, name text, " +
                "reserved1 text, reserved2 integer);");

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

            m_db.ExecuteNonQuery("END");

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
