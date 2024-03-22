using System;
using System.Collections.Generic;
using PRAManager;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Mindjet.MindManager.Interop;
using System.IO;
using System.Linq;
using PRMapCompanion;

namespace Bubbles
{
    internal class Utils
    {
        public static void ErrorToSupport(string error)
        {
            System.Windows.Forms.MessageBox.Show(error + "\r\n\r\n" +
                getString("calendar.fatalerror.text"),
                getString("calendar.fatalerror.caption"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static int StickID()
        {
            return new Random().Next();
        }

        public static void Init()
        {
            FriendlyAddinName = "Sticks";
            I18n = MMUtils._hashtable;
            I18n_common = MMUtils._hashtableCommon;
            ImagesPath = MMUtils.m_imagesPath;
            dllPath = MMUtils.m_dllPath;
            Language = MMUtils.Language;

            m_defaultDataPath = MMUtils.m_defaultDataPath;
            m_dataPath = MMUtils.m_dataPath;
            m_localDataPath = MMUtils.m_localDataPath;

            m_dataPath = getRegistry("DataPath");

            if (m_dataPath == "")
                m_dataPath = m_defaultDataPath;
            else
            {

            }

            string path = MMUtils.MindManager.GetPath(MmDirectory.mmDirectoryIcons);
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo fi in di.GetFiles())
            {
                string _path = fi.FullName;
                string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(_path);

                if (!StockIconsDupes.Keys.Contains(signature))
                    StockIconsDupes.Add(signature, _path);

                string stockicon = "stock" + Path.GetFileNameWithoutExtension(_path);
                MmStockIcon MMstockicon = BubbleIcons.StockIconFromString(stockicon);
                StockIcons.Add(stockicon, MMstockicon);
            }

            GetCustomIcons(di);

            try
            {
                if (!Directory.Exists(m_dataPath + "IconDB"))
                    Directory.CreateDirectory(m_dataPath + "IconDB");
                if (!Directory.Exists(m_dataPath + "ImageDB"))
                    Directory.CreateDirectory(m_dataPath + "ImageDB");

                File.Copy(dllPath + "\\Images\\" + "hello1.png", m_dataPath + "ImageDB\\" + "hello1.png");
                File.Copy(dllPath + "\\Images\\" + "pato.gif", m_dataPath + "ImageDB\\" + "pato.gif");
            }
            catch { };

            di = new DirectoryInfo(m_dataPath + "IconDB");
            foreach (FileInfo fi in di.GetFiles())
            {
                string signature = Path.GetFileNameWithoutExtension(fi.FullName);
                if (!CustomIcons.Keys.Contains(signature))
                    CustomIcons.Add(signature, fi.FullName);
            }
            StockIconsDupes.Clear();
        }

        static void GetCustomIcons(DirectoryInfo directoryInfo)
        {
            foreach (var directory in directoryInfo.GetDirectories())
            {
                foreach (FileInfo fi in directory.GetFiles())
                {
                    string path = fi.FullName;
                    string signature = MMUtils.MindManager.Utilities.GetCustomIconSignature(path);

                    if (StockIconsDupes.Keys.Contains(signature))
                    {
                        string stockicon = "stock" + Path.GetFileNameWithoutExtension(path);
                        MmStockIcon MMstockicon = BubbleIcons.StockIconFromString(stockicon);
                        if (!StockIcons.Keys.Contains(stockicon))
                            StockIcons.Add(stockicon, MMstockicon);
                    }
                    else if (!CustomIcons.Keys.Contains(signature))
                        CustomIcons.Add(signature, path);
                }
                GetCustomIcons(directory);
            }
        }

        /// <summary>
        /// Get value from registry subkey
        /// </summary>
        /// <param name="aKey">key to get value</param>
        /// <param name="aDefValue">value if key not found</param>
        /// <returns>key value</returns>
        public static string getRegistry(string aKey, string aDefValue = "")
        {
            MMUtils.Company = Company;
            MMUtils.AddinName = AddinName;
            return MMUtils.getRegistry("", aKey, aDefValue);
        }

        /// <summary>
        /// Set value to registry subkey
        /// </summary>
        /// <param name="aKey">key to set value</param>
        /// <param name="aValue">value to set</param>
        /// <returns>true - ok, false - failed</returns>
        public static bool setRegistry(string aKey, string aValue)
        {
            MMUtils.Company = Company;
            MMUtils.AddinName = AddinName;
            return MMUtils.setRegistry("", aKey, aValue);
        }

        public static string getString(string name)
        {
            MMUtils._hashtable = I18n;
            return MMUtils.getString(name);
        }

        public static string getCommonString(string name)
        {
            MMUtils._hashtableCommon = I18n_common;
            return MMUtils.getCommonString(name);
        }

        public static bool IsFree()
        {
            return licenseStatus == "Unlicensed";
        }

        /// <summary>
		/// Return True if a certain percent of a rectangle is shown across the 
		/// total screen area of all monitors, otherwise return False.
		/// </summary>
		/// <param name="RecLocation"></param>
		/// <param name="RecSize"></param>
		/// <param name="MinPercentOnScreen"></param>
		/// <returns>False if form is totally off screen</returns>
		public static bool StickIsOnScreen(Point RecLocation, Size RecSize, double MinPercentOnScreen = 1)
        {
            double PixelsVisible = 0;
            Rectangle Rec = new Rectangle(RecLocation, RecSize);

            foreach (Screen Scrn in Screen.AllScreens)
            {
                Rectangle r = Rectangle.Intersect(Rec, Scrn.WorkingArea);
                // intersect rectangle with screen
                if (r.Width != 0 & r.Height != 0)
                {
                    PixelsVisible += (r.Width * r.Height);
                    // tally visible pixels
                }
            }
            return PixelsVisible >= (Rec.Width * Rec.Height) * MinPercentOnScreen;
        }

        public static Point MMScreen(int x, int y)
        {
            foreach (Screen Scr in Screen.AllScreens)
            {
                if (x > Scr.WorkingArea.Left && x < Scr.WorkingArea.Right &&
                    y > Scr.WorkingArea.Top && y < Scr.WorkingArea.Bottom)
                    return Scr.WorkingArea.Location;
            }
            return new Point();
        }

        public static bool _IsOnMMWindow(Point StickLocation, Size StickSize)
        {
            if (StickLocation.X + StickSize.Width < MMUtils.MindManager.Left || // stick is totally to the left
                StickLocation.X > MMUtils.MindManager.Left + MMUtils.MindManager.Width) // stick is totally to the right
                return false;
            if (StickLocation.Y + StickSize.Height < MMUtils.MindManager.Top || // stick is totally above
                StickLocation.Y > MMUtils.MindManager.Top + MMUtils.MindManager.Height) // stick is totally below
                return false;
            return true;
        }

        public static bool IsOnMMWindow(Rectangle rec)
        {
            if (rec.X + rec.Width < MMUtils.MindManager.Left || // stick is totally to the left
                rec.X > MMUtils.MindManager.Left + MMUtils.MindManager.Width) // stick is totally to the right
                return false;
            if (rec.Y + rec.Height < MMUtils.MindManager.Top || // stick is totally above
                rec.Y > MMUtils.MindManager.Top + MMUtils.MindManager.Height) // stick is totally below
                return false;
            return true;
        }

        /// <summary>
        /// Check if file exists, unknowing its extension
        /// </summary>
        /// <param name="fileName">Basically, icon signature)</param>
        public static string GetIconFile(string fileName)
        {
            DirectoryInfo dir = new DirectoryInfo(m_dataPath + "IconDB");
            FileInfo[] files = dir.GetFiles(fileName + ".*");

            if (files.Length > 0)
                return files[0].FullName;
            else
                return "";
        }

        #region DateTime

        public static DateTime NULLDATE = new DateTime(1899, 12, 30, 0, 0, 0);

        public static DateTime? GetDate(string date)
        {
            if (date == "") return NULLDATE;

            string[] parts = date.Split(':');

            if (parts[0] == "abs")
            {
                try {
                    return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { return null; }
            }
            else if (parts[0] == "rel")
            {
                string period = parts[1];
                int days = int.Parse(parts[2]) - 1;

                if (period == "today")
                    return DateTime.Now.Date.AddDays(days).AddHours(8);
                else if (period == "tomorrow")
                    return DateTime.Now.Date.AddDays(days + 1).AddHours(8);
                else if (period == "thisweek")
                    return getWeekBegin().AddDays(days).AddHours(8);
                else if (period == "nextweek")
                    return getWeekBegin(1).AddDays(days).AddHours(8);
                else if (period == "thismonth")
                    return getMonthBegin().AddDays(days).AddHours(8);
                else if (period == "nextmonth")
                    return getMonthBegin(1).AddDays(days).AddHours(8);
            }
            return null;
        }

        public static DateTime getWeekBegin(int aOffset = 0)
        {
            DateTime _date = DateTime.Now.Date;
            int _dow = (int)_date.DayOfWeek - 1;
            if (_dow < 0)
                _dow = 6;
            return _date + new TimeSpan((aOffset * 7) - _dow, 0, 0, 0); // _date now is this week's monday
        }

        public static DateTime getWeekEnd(int aOffset = 0)
        {
            return getWeekBegin(aOffset) + new TimeSpan(6, 0, 0, 0);
        }

        public static DateTime getMonthBegin(int aOffset = 0)
        {
            DateTime _date = DateTime.Now.Date.AddMonths(aOffset);
            return new DateTime(_date.Year, _date.Month, 1, 0, 0, 0);
        }

        public static DateTime getMonthEnd(int aOffset = 0)
        {
            DateTime _date = DateTime.Now.Date.AddMonths(aOffset);
            return new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month), 0, 0, 0);
        }

        #endregion

        /// <summary>
        /// Hashtable of localization file (I18n.ini)
        /// </summary>
        public static System.Collections.Hashtable I18n;
        public static System.Collections.Hashtable I18n_common;

        public static string ImagesPath = "";
        public static string dllPath = "";
        public static string Company;
        public static string Registered_AddinName;

        /// <summary>
        /// Addin name for registry (here "MapNavigator")
        /// </summary>
        public static string AddinName;
        public static string FriendlyAddinName;
        public static string Language;
        public static string licenseStatus = "";

        /// <summary>Path with last backslash!</summary>
		public static string m_defaultDataPath, m_dataPath, m_localDataPath;

        public static Dictionary<string, string> StockIconsDupes = new Dictionary<string, string>();
        public static Dictionary<string, MmStockIcon> StockIcons = new Dictionary<string, MmStockIcon>();
        public static Dictionary<string, string> CustomIcons = new Dictionary<string, string>();
    }

    class ScalingFactor
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90

            // Other constants may be founded on pinvoke.net
        }

        public static float GetScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();

            //int Xdpi = GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSX);
            return GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSY) / 96;
        }
    }
}
