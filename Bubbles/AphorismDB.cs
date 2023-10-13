using BubblesAppManager;

namespace Bubbles
{
    internal class AphorismDB : DatabaseWrapper
    {
        public override string ToString() => "Aphorisms Database";

        protected static string _getDatabaseName()
        {
            string path = Utils.m_defaultDataPath;
            return path + "aphorisms.db";
        }

        public override string getDatabaseName() => _getDatabaseName();

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");
            m_db.ExecuteNonQuery("CREATE TABLE APHORISMS(content, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TEMPLATES(textcolor text, fillcolor text, fontfamily text, " +
                "textsize integer, textbold integer, sticksize text, alignment text, " +
                "reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("END");
        }

        public void AddAphorism(string text)
        {
            m_db.ExecuteNonQuery("insert into APHORISMS values(`"
                + text + "`, "
                + "'', '', 0, 0"
                + ");"
                );
        }

        public void AddTemplate(string textcolor, string fillcolor, string fontfamily, 
            int textsize, int textbold, string sticksize, string alignment)
        {
            m_db.ExecuteNonQuery("insert into TEMPLATES values(`"
                + textcolor + "`, `"
                + fillcolor + "`, `"
                + fontfamily + "`, "
                + textsize + ", "
                + textbold + ", `"
                + sticksize + "`, `"
                + alignment + "`, "
                + "'', '', 0, 0"
                + ");"
                );
        }
    }
}
