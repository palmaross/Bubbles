using BubblesAppManager;

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

        public void AddIcon(string name, string filename, int order)
        {
            m_db.ExecuteNonQuery("insert into ICONS values(`"
                + name + "`, `"
                + filename + "`, "
                + order + ", "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public void AddSource(string title, string path, string type, int order)
        {
            m_db.ExecuteNonQuery("insert into SOURCES values(`"
                + title + "`, `"
                + path + "`, `"
                + type + "`, "
                + order + ", "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");
            m_db.ExecuteNonQuery("CREATE TABLE SNIPPETS(snippet text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE SOURCES(title text, path text, type, _order integer, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
                // bubbles: bubble_name;bubble_orientation (H or V);bubble_location_X:bubble_location_Y
            m_db.ExecuteNonQuery("END");
        }
    }
}
