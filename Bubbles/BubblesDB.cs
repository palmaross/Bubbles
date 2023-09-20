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

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");
            m_db.ExecuteNonQuery("CREATE TABLE SNIPPETS(snippet text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("END");
        }
    }
}
