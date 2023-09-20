using System;
using System.Collections.Generic;
using PRAManager;

namespace BubblesAppManager
{
	using Community.CsharpSqlite;

    internal class DatabaseWrapper : Object, IDisposable
	{
		public virtual string getDatabaseName()
		{
			return MMUtils.getDataPath("AppManager") + "database.db";
		}

		public virtual void CreateDatabase()
		{
			//m_db.OpenDatabase(getDatabaseName());
		}

        public virtual void OpenDatabase()
        {
        }

		public virtual void ValidateDatabase(int aTablesCount =-1)
		{
			System.Collections.ArrayList _tables = m_db.GetTables();
			if (_tables.Count == 0)
				throw (new Exception("DatabaseWrapper: validation failed!"));
			if (aTablesCount < 0)
				aTablesCount = _tables.Count;
			if (_tables.Count < aTablesCount)
				throw (new Exception("DatabaseWrapper: validation failed!"));
			m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");
			m_db.ExecuteNonQuery("END");
// 			for (int i = 0; i < aTablesCount; i++)
// 			{
// 				String _query = "select * from " + _tables[i].ToString();
// 				System.Data.DataTable _table = m_db.ExecuteQuery(_query);
// 			}
		}

		public DatabaseWrapper()
		{
			string _dbName = getDatabaseName();
			bool _needToCreate = !System.IO.File.Exists(_dbName);
            m_db = DatabaseContainer.Get(this.ToString(), _dbName);
			if (! _needToCreate)
				try
				{
                    OpenDatabase();
					ValidateDatabase();
				}
				catch (Exception _e)
				{
#if DEBUG
					System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
					RebuildDatabase();
				}
			else
				// Create db structures
				try
				{
					CreateDatabase();
				}
				catch (Exception _e)
				{
#if DEBUG
					System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
				}
		}

		~DatabaseWrapper()
		{
			Dispose();
		}

        public void ExecuteNonQuery(string aQuery, bool aExclusive = true)
		{
			try
			{
				if (aExclusive)
					m_db.ExecuteNonQuery("BEGIN EXCLUSIVE");
				m_db.ExecuteNonQuery(aQuery);
				if (aExclusive)
					m_db.ExecuteNonQuery("END");
				return;
			}
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}
			// Database damaged
			RebuildDatabase();
			m_db.ExecuteNonQuery(aQuery);
		}

		public System.Data.DataTable ExecuteQuery(string aQuery)
		{
			try
			{
				return m_db.ExecuteQuery(aQuery);
			}
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}
			// Database damaged
			RebuildDatabase();
			return m_db.ExecuteQuery(aQuery);
		}

		public void RebuildDatabase()
		{
			try
			{
				m_db.CloseDatabase();
			}
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}
			try
			{
				System.IO.File.Delete(getDatabaseName());
                m_db.OpenDatabase(getDatabaseName());
				CreateDatabase();
			}
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
			}
		}

		public System.Collections.ArrayList GetTables()
		{
			return m_db.GetTables();
		}

		public void Dispose()
		{
			Dispose(true);
			// This object will be cleaned up by the Dispose method. Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		// Dispose(bool disposing) executes in two distinct scenarios. If disposing equals true, the method has been called directly
		// or indirectly by a user's code. Managed and unmanaged resources can be disposed.
		// If disposing equals false, the method has been called by the runtime from inside the finalizer and you should not reference
		// other objects. Only unmanaged resources can be disposed.

		protected virtual void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.m_disposed)
			{
				// If disposing equals true, dispose all managed and unmanaged resources.
				if (disposing)
				{
					// Dispose managed resources.
				}
				// Call the appropriate methods to clean up unmanaged resources here.
				// If disposing is false, only the following code is executed.
                DatabaseContainer.Forget(this.ToString());
				//m_db = null;
				// Note disposing has been done.
				m_disposed = true;
			}
		}

		public override string ToString()
		{
			return "GoogleCalendar Database";
		}

        protected SQLiteDatabase m_db = null;
		protected bool m_disposed = false;
	}

    /// <summary>
    /// Container for all opened databases
    /// </summary>
    internal class DatabaseContainer
    {

        protected static Dictionary<String, SQLiteDatabase> m_databases = new Dictionary<string, SQLiteDatabase>();
        protected static Dictionary<String, int> m_refcnt = new Dictionary<string, int>();

        /// <summary>
        /// Create or reuse a database for specified database key
        /// </summary>
        /// <param name="aDatabaseKey">Database key</param>
        /// <returns></returns>
        public static SQLiteDatabase Get(String aDatabaseKey, String aFilename)
        {
            if (m_databases.ContainsKey(aDatabaseKey))
            {
                if (!m_refcnt.ContainsKey(aDatabaseKey))
                {
                    m_refcnt[aDatabaseKey] = 0;
                }
                m_refcnt[aDatabaseKey] = m_refcnt[aDatabaseKey] + 1;
                return m_databases[aDatabaseKey];
            }
            // Not found - create.
            SQLiteDatabase _db = new SQLiteDatabase();
            try
            {
                _db.OpenDatabase(aFilename);
            }
			catch (Exception _e)
			{
#if DEBUG
				System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
				return null;
			}
			m_databases[aDatabaseKey] = _db;
            m_refcnt[aDatabaseKey] = 1;
            return _db;
        }

        public static void Forget(String aDatabaseKey)
        {
            if (!m_refcnt.ContainsKey(aDatabaseKey) || m_refcnt[aDatabaseKey] <= 0 || !m_databases.ContainsKey(aDatabaseKey))
                return;
            m_refcnt[aDatabaseKey] = m_refcnt[aDatabaseKey] - 1;
            if (m_refcnt[aDatabaseKey] == 0)
            {
                // Really close
                m_databases[aDatabaseKey].CloseDatabase();
                m_databases.Remove(aDatabaseKey);
            }
        }
    
    }
}
