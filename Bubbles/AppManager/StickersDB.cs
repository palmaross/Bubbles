using BubblesAppManager;

namespace Bubbles
{
    internal class StickersDB : DatabaseWrapper
    {
        public override string ToString() => "Stickers Database";

        protected static string _getDatabaseName()
        {
            string path = Utils.m_defaultDataPath;
            return path + "stickers.db";
        }

        public override string getDatabaseName() => _getDatabaseName();

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

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");

            m_db.ExecuteNonQuery("CREATE TABLE APHORISMS(content, reserved1 text, reserved2 integer);");
            
            m_db.ExecuteNonQuery("CREATE TABLE TEMPLATES(textcolor text, fillcolor text, fontfamily text, " +
                "textsize integer, textbold integer, sticksize text, alignment text, " +
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
