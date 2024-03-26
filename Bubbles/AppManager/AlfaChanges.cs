using Bubbles;
using System;
using System.IO;
using System.Data;
using PRAManager;
using static Community.CsharpSqlite.Sqlite3;

namespace BubblesAppManager
{
    internal class V7Changes
    {
        public static void Changes7_1()
        {
            string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
           
            if (MMUtils.getRegistry("", "v7_1Changes", "0") != "1")
            {
                // Modify dbtemplates.db
                MMBase.TRACE("Modifying bubbles.db");
                try
                {
                    

                    if (File.Exists(MMUtils.m_defaultDataPath + "bubbles_old.db"))
                        File.Delete(MMUtils.m_defaultDataPath + "bubbles_old.db");

                    // rename our db
                    string newdb = MMUtils.m_defaultDataPath + "bubbles.db";
                    File.Move(MMUtils.m_defaultDataPath + "bubbles.db", MMUtils.m_defaultDataPath + "bubbles_old.db");

                    OldDashboardDB db_old = new OldDashboardDB(); // get old db

                    //BubblesDB.defaults = false;
                    StixDB db_new = new StixDB(); // create new db without values

                    // https://tableplus.com/blog/2018/07/sqlite-how-to-copy-table-to-another-database.html

                    db_old.ExecuteNonQuery("ATTACH DATABASE`" + newdb + "` AS " + db_new + "");
                    db_old.ExecuteNonQuery("INSERT INTO new_db.table_name SELECT * FROM old_db.table_name");

                    DataTable dt = db_old.ExecuteQuery("select * from TEMPLATES order by name");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string templateFileName = (string)row["template"];
                            FileInfo _fi = new FileInfo(templateFileName);
                            templateFileName = _fi.Name;

                            //db_new.AddTemplate((string)row["name"], (string)row["descr"], templateFileName, i++);
                        }
                    }

                    db_new.Dispose();
                    db_old.Dispose();
                    MMBase.TRACE("Templates modified");
                }
                catch { MMBase.TRACE("Templates were not modified!"); }

                try
                {
                    File.Delete(MMUtils.m_defaultDataPath + "dbtemplates_old.db");
                }
                catch { }
            }

            MMUtils.setRegistry("", "v7_1Changes", "1");
        }
    }
 
    internal class OldDashboardDB : DatabaseWrapper
    {
        public override string getDatabaseName() => Utils.m_defaultDataPath + "bubbles_old.db";

        public override string ToString()
        {
            return "Dashboard Database Old";
        }
    }
}
