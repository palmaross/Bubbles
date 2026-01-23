namespace Bubbles
{
    using System;
    using System.Runtime.InteropServices;
    using Mindjet.MindManager.Interop;
    using PRAManager;

    /// <summary>
    ///   The object for implementing an Add-in.
    /// </summary>
    /// <seealso class='IDTExtensibility2' />
    [GuidAttribute("47753526-1994-4211-A1DA-926C94F51BB2"), ProgId("OmniStix22.Connect")]
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
                string logfile = myDocumentsFolder + "\\OmniStix_logfile.txt";
                if (System.IO.File.Exists(logfile))
                    System.IO.File.Delete(logfile);
            }
            catch { }

            try
            {
                MMUtils.Version = 22;
                Utils.Version = 22;
                MMUtils.Registered_AddinName = "OmniStix22.Connect";
                MMUtils.CLSID = "47753526-1994-4211-A1DA-926C94F51BB2";
                MMUtils.CreateAddinFolder = true;
                MMUtils.CreateAddinAppDataFolder = true;
                MMUtils.checkForUpdates = true;

                MMUtils.AddinName = "OmniStix";
                MMUtils.FriendlyAddinName = "OmniStix";
                MMUtils.Company = "PalmaRoss";
                Utils.Company = "PalmaRoss";
                Utils.AddinName = "OmniStix";
                Utils.Registered_AddinName = "OmniStix22.Connect";
                MMUtils.Language = Utils.getRegistry("language", "english");
                MMUtils.AddinVersion = Utils.getRegistry("version");
                MMUtils.licenseKeyStartsWith = "OS";

                MMUtils.MindManager = (Application)application;
                if (MMUtils.DoNotStartAddin)
                    return;

                Utils.Init();

                PRMapCompanion.DocumentStorage.Init();

                // Start interface
                m_Stix = new StixMain();
                m_Stix.Create();
            }
            catch (Exception e)
            {
                MMBase.TRACE("Error while starting OmniStix...\r\n\r\n" + e.Message + "\r\n\r\n" + e.StackTrace);
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
            m_Stix.Destroy();

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

        private StixMain m_Stix;
    }
}
