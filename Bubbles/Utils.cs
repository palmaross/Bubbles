using System;
using System.Collections.Generic;
using PRAManager;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Bubbles
{
    internal class Utils
    {
        public static void ErrorToSupport(string error)
        {
            System.Windows.Forms.MessageBox.Show(error + "\r\n\r\n" +
                getString("calendar.fatalerror.text"),
                getString("calendar.fatalerror.caption"),
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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


        /// <summary>
        /// Change RRULE in the reccurence list. Insert new or replace old UNTIL part with new date.
        /// </summary>
        /// <param name="Recurrence">List of event recurrence rules</param>
        /// <param name="start">Date of sequence of events to delete</param>
        /// <param name="instanceIndex">Index of recurring event "This" instanse in "ThisAndFollowing" option</param>
        /// <returns>List of recurrence rules</returns>
        public static List<string> ChangeRRULE(List<string> Recurrence, string start, int instanceIndex = -1)
        {
            List<string> aRecurrence = new List<string>();
            aRecurrence.AddRange(Recurrence);
            string rrule = "";
            int i = 0;

            foreach (string rec in Recurrence)
            {
                if (rec.ToLower().StartsWith("rrule"))
                {
                    rrule = rec;
                    break;
                }
                i++;
            }

            int count = rrule.ToLower().IndexOf("count");
            int until = rrule.ToLower().IndexOf("until");
            int end;

            // "ThisAndFollowing" option for modified events with UNTIL rule or without limiting rules
            if (instanceIndex >= 0 && (until > 1 || (count == -1 && until == -1)))
                return Recurrence;

            // Replace COUNT with UNTIL=(start date of this event - 1). Thus this and following events will be removed.
            if (count > 1)
            {
                end = rrule.IndexOf(';', count);
                if (end == -1) end = rrule.Length;
                if (instanceIndex == -1)
                {
                    string _count = rrule.Substring(count, end - count);
                    rrule = rrule.Replace(_count, "UNTIL=" + start);
                }
                else
                {
                    int _start = rrule.IndexOf('=', count) + 1;
                    string _count = rrule.Substring(_start, end - _start);
                    string newcount = (Convert.ToInt32(_count) - instanceIndex).ToString();
                    rrule = rrule.Replace(_count, newcount);
                }
            }
            // Replace original UNTIL value with (start date of this event - 1)
            else if (until > 1)
            {
                until = rrule.IndexOf('=', until) + 1;
                end = rrule.IndexOf(';', until);
                if (end == -1) end = rrule.Length;
                string old_until = rrule.Substring(until, end - until);
                rrule = rrule.Replace(old_until, start);
            }
            // Insert UNTIL=(start date of this event - 1) to original RRULE
            else
            {
                end = rrule.ToLower().IndexOf(';');
                string rrule1 = rrule.Substring(0, end);
                rrule = rrule.Replace(rrule1, rrule1 + ";" + "UNTIL=" + start);
            }

            aRecurrence.RemoveAt(i);
            aRecurrence.Insert(i, rrule);

            return aRecurrence;
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
		public static bool IsOnScreen(Point RecLocation, Size RecSize, double MinPercentOnScreen = 1)
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

        public static bool IsOnMMWindow(Point WandLocation, Size WandSize)
        {
            if (WandLocation.X + WandSize.Width < MMUtils.MindManager.Left || // wand is totally to the left
                WandLocation.X > MMUtils.MindManager.Left + MMUtils.MindManager.Width) // wand is totally to the right
                return false;
            if (WandLocation.Y + WandSize.Height < MMUtils.MindManager.Top || // wand is totally above
                WandLocation.Y > MMUtils.MindManager.Top + MMUtils.MindManager.Height) // wand is totally below
                return false;
            return true;
        }

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
    }
}
