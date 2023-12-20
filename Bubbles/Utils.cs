using System;
using System.Collections.Generic;
using PRAManager;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Mindjet.MindManager.Interop;

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
            Registered_AddinName = "Bubbles23.Connect";
            FriendlyAddinName = "Bubbles";
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

        #region MapMarkers

        public static bool AddTagToTopic(Topic aTopic, string tagName, string tagID, string groupName, string groupID, string tagColor = "", bool mutex = false)
        {
            if (aTopic == null)
                return false;

            MapMarkerGroup _mmg = GetMapMarkerGroup(aTopic.Document, groupName, groupID, true, mutex);
            if (_mmg == null)
                return false;

            string groupId = _mmg.GroupId;

            // if group doesn't have this tag, add it to group
            if (!AddTextLabelMarkerToGroup(_mmg, tagName, tagID, tagColor))
                return false;

            try
            {
                // add tag to the topic
                aTopic.TextLabels.AddTextLabelFromGroup(tagName, groupId);
                return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Get or add MapMarkerGroup (only text label group)
        /// </summary>
        /// <param name="aDocument"></param>
        /// <param name="aName">Name of marker group</param>
        /// <param name="aCreateNew">if to create marker group if not found</param>
        /// <param name="mutex">If group must be MutuallyExclusive</param>
        /// <returns>MapMarkerGroup</returns>
		public static MapMarkerGroup GetMapMarkerGroup(Document aDocument, string aName, string groupID = "", bool aCreateNew = true, bool mutex = false)
        {
            foreach (MapMarkerGroup _mmg1 in aDocument.MapMarkerGroups)
            {
                string group_ID = "";

                if (_mmg1.Type == MmMapMarkerGroupType.mmMapMarkerGroupTypeTextLabel)
                {
                    if (_mmg1.ContainsAttributesNamespace(ATTR_MARKERS))
                        group_ID = _mmg1.GetAttributes(ATTR_MARKERS).GetAttributeValue(URI_MARKERS);

                    // search group by CommonLists ID first
                    if (!String.IsNullOrEmpty(group_ID) && group_ID == groupID) // group found by CommonLists ID
                    {
                        if (aName != "" && _mmg1.Name != aName) // group name doesn't match?
                            _mmg1.Name = aName; // make it correct
                        return _mmg1;
                    }

                    if (groupID == _mmg1.GroupId) // group found by given ID
                    {
                        if (aName != "" && _mmg1.Name != aName)
                            _mmg1.Name = aName;
                        return _mmg1;
                    }

                    // not found by ID, search by name
                    if (_mmg1.Name == aName)
                    {
                        return _mmg1;
                    }
                }
            }
            if (aCreateNew)
            {
                // Not found - create new
                try
                {
                    MapMarkerGroup MarkerGroup = aDocument.MapMarkerGroups.AddTextLabelMarkerGroup(aName);
                    if (groupID != "") // from Common Lists
                        MarkerGroup.GetAttributes(ATTR_MARKERS).SetAttributeValue(URI_MARKERS, groupID);
                    MarkerGroup.MutuallyExclusive = mutex;
                    return MarkerGroup;
                }
                catch { }
            }
            return null;
        }

        /// <summary>
        /// Adds marker (tag or resource) to Marker Group
        /// </summary>
        /// <param name="mg">Marker Group to add marker</param>
        /// <param name="tagName">marker name</param>
        /// <param name="color">marker color</param>
        /// <param name="changeColor">if marker exists in the group but has another color, then change color</param>
        /// <returns></returns>
        public static bool AddTextLabelMarkerToGroup(MapMarkerGroup mg, string tagName, string tagID, string color = "", bool changeColor = false, bool resourceMarker = false)
        {
            string groupId = mg.GroupId;
            //InitMarkersList();
            //GetNode(groupId, tagName);
            MapMarker tag = GetTagFromGroup(mg, tagName);

            if (tag == null) // There is no marker in the group
            {
                try
                {
                    if (resourceMarker)
                        tag = mg.AddResourceMarker(tagName);
                    else
                        tag = mg.AddTextLabelMarker(tagName);

                    if (tagID != "")
                    {
                        tag.GetAttributes(ATTR_MARKERS).SetAttributeValue(URI_MARKERS, tagID);
                    }
                    if (color != "")
                    {
#if !MINDJET20 // tag and resource colors can be set only in MM21 and above
                        int c = int.Parse(color.Substring(1), System.Globalization.NumberStyles.HexNumber);
                        tag.Color.SetValue(c);
#endif
                    }
                }
                catch { MessageBox.Show("Problem with adding tag"); return false; }
            }
#if !MINDJET20 // tag and resource colors can be set only in MM21 and above
            else if (changeColor) // tag exists. Check for tag color
            {
                int tagColor = tag.Color.Value;

                if (color == "")
                {
                    if (tagColor != 0)
                        tag.Color.SetValue(0);
                }
                else
                {
                    int c = int.Parse(color.Substring(1), System.Globalization.NumberStyles.HexNumber);
                    if (tagColor != c)
                        tag.Color.SetValue(c);
                }
            }
#endif
            return true;
        }

        /// <summary>
        /// Check if given tag group contains tag
        /// </summary>
        /// <param name="group"></param>
        /// <param name="tagName"></param>
        /// <returns>True if tag group contains tag</returns>
        public static MapMarker GetTagFromGroup(MapMarkerGroup group, string tagName)
        {
            foreach (MapMarker mm in group)
            {
                if (mm.Label == tagName)
                    return mm;
            }
            return null;
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

        public const string ATTR_MARKERS = "PALMAROSS$$$MARKERS";
        public const string URI_MARKERS = "UriUniqueID";
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
