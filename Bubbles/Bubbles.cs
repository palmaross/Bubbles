using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Mindjet.MindManager.Interop;
using PRAManager;
using PRMapCompanion;

namespace Bubbles
{
    class BubblesButton
    {
        public void Create()
        {
            if (m_bCreated)
                return;

            m_cmdBubbles = MMUtils.MindManager.Commands.Add(Registered_AddinName, "ribbon.palmaross.bubbles");
            m_cmdBubbles.Caption = "";
            m_cmdBubbles.ToolTip = getString("main.tooltip") + "\n" + getString("main.name");
            m_cmdBubbles.UpdateState += new ICommandEvents_UpdateStateEventHandler(m_cmdBubbles_UpdateState);
            m_cmdBubbles.LargeImagePath = ImagesPath + "bubble.png";
            m_cmdBubbles.ImagePath = ImagesPath + "bubble.png";
            m_cmdBubbles.Click += new ICommandEvents_ClickEventHandler(m_cmdBubbles_Click);
            m_ctrlBubbles = MMUtils.MindManager.StatusBarControls.AddButton(m_cmdBubbles);

            m_bubblesPaste = new BubblesPaste();

            m_bCreated = true;
        }

        private void m_cmdBubbles_Click()
        {
            if (m_bubblesPaste.Visible)
                m_bubblesPaste.Hide();
            else
                m_bubblesPaste.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
        }

        private void m_cmdBubbles_UpdateState(ref bool pEnabled, ref bool pChecked)
        {
            pEnabled = true;
            pChecked = false;
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
        /// Get value from registry subkey
        /// </summary>
        /// <param name="aSubKey">subkey to open</param>
        /// <param name="aKey">key to get value</param>
        /// <param name="aDefValue">value if key not found</param>
        /// <returns>key value or aDefValue</returns>
        public static string getRegistry(string aSubKey, string aKey, string aDefValue = "")
        {
            MMUtils.Company = Company;
            MMUtils.AddinName = AddinName;
            return MMUtils.getRegistry(aSubKey, aKey, aDefValue);
        }

        /// <summary>
        /// Set value to registry subkey
        /// </summary>
        /// <param name="aSubKey">subkey to create or open</param>
        /// <param name="aKey">key to set value</param>
        /// <param name="aValue">value to set</param>
        /// <returns>true - ok, false - failed</returns>
        public static bool setRegistry(string aSubKey, string aKey, string aValue)
        {
            MMUtils.Company = Company;
            MMUtils.AddinName = AddinName;
            return MMUtils.setRegistry(aSubKey, aKey, aValue);
        }


        public void Destroy()
        {
            if (!m_bCreated)
                return;

            m_ctrlBubbles.Delete(); Marshal.ReleaseComObject(m_ctrlBubbles); m_ctrlBubbles = null;
            Marshal.ReleaseComObject(m_cmdBubbles); m_cmdBubbles = null;

            if (m_bubblesPaste.Visible)
                m_bubblesPaste.Hide();
            m_bubblesPaste.Dispose();
            m_bubblesPaste = null;

            m_bCreated = false;
        }

        private bool m_bCreated;
        public static BubblesPaste m_bubblesPaste = null;

        // Commands and controls

        private Command m_cmdBubbles;
        private Control m_ctrlBubbles;

        /// <summary>
        /// Hashtable of localization file (I18n.ini)
        /// </summary>
		public static System.Collections.Hashtable I18n;
        public static System.Collections.Hashtable I18n_common;

        public static string ImagesPath = "";
        public static string dllPath = "";

        public static string Company = "PalmaRoss";
        public static string Registered_AddinName = "MapNavigator21.Connect";

        /// <summary>
        /// Addin name for registry (here "MapNavigator")
        /// </summary>
        public static string AddinName = "MapNavigator";
        public static string FriendlyAddinName = "Map Navigator";
        public static string Language;
        public static string licenseStatus = "";
    }
}
