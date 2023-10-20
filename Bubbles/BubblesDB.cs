﻿using BubblesAppManager;
using System.Data;
using System;

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

        public void AddStick(string name, string type, int start, string position)
        {
            m_db.ExecuteNonQuery("insert into STICKS values(NULL, `"
                + name + "`, `"
                + type + "`, "
                + start + ", `"
                + position + "`, "
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

        public override void CreateDatabase()
        {
            base.CreateDatabase();
            m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");
            m_db.ExecuteNonQuery("CREATE TABLE STICKS(id INTEGER PRIMARY KEY, name text, type text, " +
                "start integer, position text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // name = stick name (by user)
            // type - icons, bookmarks, etc.
            // start - run sticker when MM started
            // position - H#5120,0:5126,363;0,0:2,358 (Horizontal;screen1Location;screen2Location)
            m_db.ExecuteNonQuery("CREATE TABLE ICONS(name text, filename text, _order integer, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE PRIPRO(type text, value text, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // type - "priority" or "progress"
            // value - priority or progress value
            m_db.ExecuteNonQuery("CREATE TABLE SOURCES(title text, path text, type text, _order integer, stickID int, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE NOTEPADS(content text, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE IDEAS(content text, stickID int, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE NOTES(info text, link text, stickID int, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE LINKS(title text, link text, stickID int, reserved1 text, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE SNIPPETS(content text, reserved1 text, stickID int, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE TODOS(content text, reserved1 text, stickID int, reserved2 integer);");
            m_db.ExecuteNonQuery("CREATE TABLE STICKERS(id INTEGER PRIMARY KEY, content text, textcolor text, fillcolor text, " +
                "fontfamily text, textsize integer, textbold integer, sticksize text, image text, alignment text, type text, " +
                "reserved1 text, reserved2 text, reserved3 integer, reserved4 integer);");
            // type:
            // "sticker"
            // "template"
            // "reminder:12:05" (12:05) or "reminder:20" (in 20 minutes)
            m_db.ExecuteNonQuery("END");

            // Add first PriPro stick
            AddStick(Utils.getString("BubblesMenuDlg.lblPriPro.text"), StickUtils.typepripro, 0, "");
            int id = 1;
            DataTable dt = m_db.ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

            AddPriPro("pri", 1, id);
            AddPriPro("pri", 2, id);
            AddPriPro("pro", 0, id);
            AddPriPro("pro", 100, id);

            // Add first sources
            AddStick(Utils.getString("mysources.bubble.tooltip"), StickUtils.typesources, 0, "");
            id = 1;
            dt = m_db.ExecuteQuery("SELECT last_insert_rowid()");
            if (dt.Rows.Count > 0) id = Convert.ToInt32(dt.Rows[0][0]);

            AddSource(Utils.getString("mysources.first1.text"), "https://palmaross.com/", "http", 1, id);
            AddSource(Utils.getString("mysources.first2.text"), Utils.dllPath + "Sticks.chm", "file", 2, id);

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
