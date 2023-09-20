namespace Bubbles23
{
    using System;
    using System.Runtime.InteropServices;
    using Mindjet.MindManager.Interop;
    using PRAManager;
    using Bubbles;

    /// <summary>
    ///   The object for implementing an Add-in.
    /// </summary>
    /// <seealso class='IDTExtensibility2' />
    [GuidAttribute("A6E25E96-3200-4C90-9D4C-58BB430A8406"), ProgId("Bubbles23.Connect")]
    public class Connect : Object, Extensibility.IDTExtensibility2
    {
        /// <summary>
        ///		Implements the constructor for the Add-in object.
        ///		Place your initialization code within this method.
        /// </summary>
        public Connect()
        {
        }

        /// <summary>
        ///	  Implements the OnConnection method of the IDTExtensibility2 interface.
        ///	  Receives notification that the Add-in is being loaded.
        /// </summary>
        /// <param term='application'>
        ///	  Root object of the host application.
        /// </param>
        /// <param term='connectMode'>
        ///	  Describes how the Add-in is being loaded.
        /// </param>
        /// <param term='addInInst'>
        ///	  Object representing this Add-in.
        /// </param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, Extensibility.ext_ConnectMode connectMode, object addInInst, ref System.Array custom)
        {
            string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            try
            {
                string logfile = myDocumentsFolder + "\\MapNavigator_logfile.txt";
                if (System.IO.File.Exists(logfile))
                    System.IO.File.Delete(logfile);
            }
            catch { }

            try
            {
                MMUtils.Version = 23;
                MMUtils.Registered_AddinName = "Bubbles23.Connect";
                MMUtils.CLSID = "A6E25E96-3200-4C90-9D4C-58BB430A8406";
                MMUtils.CreateAddinFolder = true;
                MMUtils.CreateAddinAppDataFolder = false;
                MMUtils.checkForUpdates = true;

                MMUtils.AddinName = "Bubbles";
                MMUtils.FriendlyAddinName = "Bubbles";
                MMUtils.Company = "PalmaRoss";
                Utils.Company = "PalmaRoss";
                Utils.AddinName = "Bubbles";
                MMUtils.Language = Utils.getRegistry("language", "english");
                MMUtils.AddinVersion = Utils.getRegistry("version");
                MMUtils.licenseKeyStartsWith = "BB";

                MMUtils.MindManager = (Application)application;
                if (MMUtils.DoNotStartAddin)
                    return;

                Utils.Init();

                PRMapCompanion.DocumentStorage.Init();

                // Start interface
                m_Bubbles = new BubblesButton();
                m_Bubbles.Create();
            }
            catch (Exception e)
            {
                MMBase.TRACE("Error while starting Map Navigator...\r\n\r\n" + e.Message + "\r\n\r\n" + e.StackTrace);
                MMUtils.ErrorToSupport();
            }
        }

        /// <summary>
        ///	 Implements the OnDisconnection method of the IDTExtensibility2 interface.
        ///	 Receives notification that the Add-in is being unloaded.
        /// </summary>
        /// <param term='disconnectMode'>
        ///	  Describes how the Add-in is being unloaded.
        /// </param>
        /// <param term='custom'>
        ///	  Array of parameters that are host application specific.
        /// </param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(Extensibility.ext_DisconnectMode disconnectMode, ref System.Array custom)
        {
            m_Bubbles.Destroy();

            MMUtils.AddinsConnected -= 1;
            if (MMUtils.AddinsConnected <= 0) // it's the last add-in, dispose all events
            {
                PRMapCompanion.DocumentStorage.Destroy();
                MMUtils.MindManager = null;
            }
        }

        /// <summary>
        ///	  Implements the OnAddInsUpdate method of the IDTExtensibility2 interface.
        ///	  Receives notification that the collection of Add-ins has changed.
        /// </summary>
        /// <param term='custom'>
        ///	  Array of parameters that are host application specific.
        /// </param>
        /// <seealso class='IDTExtensibility2' />
        public void OnAddInsUpdate(ref System.Array custom)
        {
        }

        /// <summary>
        ///	  Implements the OnStartupComplete method of the IDTExtensibility2 interface.
        ///	  Receives notification that the host application has completed loading.
        /// </summary>
        /// <param term='custom'>
        ///	  Array of parameters that are host application specific.
        /// </param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref System.Array custom)
        {
        }

        /// <summary>
        ///	  Implements the OnBeginShutdown method of the IDTExtensibility2 interface.
        ///	  Receives notification that the host application is being unloaded.
        /// </summary>
        /// <param term='custom'>
        ///	  Array of parameters that are host application specific.
        /// </param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref System.Array custom)
        {
        }

        private BubblesButton m_Bubbles;
    }
}
