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

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE SOURCES(title text, path text, type, _order integer, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE NOTEPADS(content text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE IDEAS(content text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE NOTES(info text, link text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE LINKS(title text, link text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE SNIPPETS(content text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TODOS(content text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE STICKERS(id INTEGER PRIMARY KEY, content text, textcolor text, fillcolor text, " +
                "fontfamily text, textsize integer, textbold integer, sticksize text, image text, alignment text, type text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
                // type:
                // "sticker"
                // "template"
                // "reminder:12:05"
            m_db.ExecuteNonQuery("END");

            // Add a couple of templates
            AddSticker(Utils.getString("stickertemplate1.text"), "#ff0000ff", "#ff00ffff", "Segoe Print", 
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
            AddSticker(Utils.getString("stickertemplate2.text"), "#ff0000ff", "#ff0000ff", "Segoe Print", 
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
            AddSticker(Utils.getString("stickertemplate3.text"), "#ff0000ff", "#ff00ff00", "Segoe Print", 
                14, 1, StickerDummy.DummyStickerWidth + ":" + StickerDummy.DummyStickerHeight, "", "center", "template");
        }
    }
}
