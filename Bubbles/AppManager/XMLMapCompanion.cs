using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Bubbles.AppManager
{
    internal class XMLMapCompanion
    {
        /// <summary>
		/// Create a XMLMapCompanion for opened Document. Registers all found custim icons in Icon database.
		/// </summary>
		/// <param name="aDocument">a document to create Companion for.</param>
		/// <returns></returns>
		public static XMLMapCompanion Get(Document aDocument)
        {
            if (aDocument == null)
                return null;
            XMLMapCompanion _rc = new XMLMapCompanion
            {
                m_XML = aDocument.Xml
            };
            _rc.Path = aDocument.FullName;

            if (_rc.Parse()) return _rc;
            return null;
        }

        public static string GetTopicNotes(string xml, string guid)
        {
            XMLMapCompanion _rc = new XMLMapCompanion
            {
                m_XML = xml
            };

            return _rc.GetNotes(xml, guid);
        }
        public static string CentralTopicText
        {
            get
            {
                return CentralTopic;
            }
        }

        protected string GetNotes(string xml, string guid)
        {
            m_doc.LoadXml(xml);

            XmlNodeList _allTopics = m_doc.DocumentElement.SelectNodes("//ap:OneTopic/ap:Topic/@OId|//ap:SubTopics/ap:Topic/@OId", NSManager);
            foreach (XmlNode _node in _allTopics)
            {
                if (_node.Value == guid)
                {
                    var topic = new XMLTopicCompanion((_node as XmlAttribute).OwnerElement, NSManager, this);
                    return topic.NotesHtml();
                }
            }
            // Process floating topics
            _allTopics = m_doc.DocumentElement.SelectNodes("//ap:FloatingTopics/ap:Topic/@OId", NSManager);
            foreach (XmlNode _node in _allTopics)
            {
                if (_node.Value == guid)
                {
                    var topic = new XMLTopicCompanion((_node as XmlAttribute).OwnerElement, NSManager, this);
                    return topic.NotesHtml();
                }
            }

            return null;
        }

        /// <summary>
		/// Parses the XML representation of the Document.
		/// </summary>
		/// <returns></returns>
		protected bool Parse()
        {
            try
            {
                m_doc.LoadXml(m_XML);

                m_guids.Clear();
                m_topics.Clear();
                m_floatingGuids.Clear();
                m_floatingTopics.Clear();

                XmlNodeList _allTopics = m_doc.DocumentElement.SelectNodes("//ap:OneTopic/ap:Topic/@OId|//ap:SubTopics/ap:Topic/@OId", NSManager);

                XmlNode _centralTopicNode = m_doc.DocumentElement.SelectSingleNode("//ap:OneTopic/ap:Topic/ap:Text", NSManager);
                if (_centralTopicNode != null)
                    CentralTopic = _centralTopicNode.Attributes.GetNamedItem("PlainText").Value;

                foreach (XmlNode _node in _allTopics)
                {
                    m_guids.Add(_node.Value);
                    m_topics.Add(_node.Value, new XMLTopicCompanion((_node as XmlAttribute).OwnerElement, NSManager, this));
                }

                _allTopics = m_doc.DocumentElement.SelectNodes("//ap:FloatingTopics/ap:Topic/@OId", NSManager);
                foreach (XmlNode _node in _allTopics)
                {
                    m_floatingGuids.Add(_node.Value);
                    m_floatingTopics.Add(_node.Value, new XMLTopicCompanion((_node as XmlAttribute).OwnerElement, NSManager, this));
                }
            }
#if DEBUG
            catch (Exception _e)
            {
                System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
            }
#else
			catch { return false; }
#endif
            return true;
        }

        public static string GetDocumentXML(string aFilename)
        {
            if (!File.Exists(aFilename))
                return null;

            string _xml = String.Empty;

            try
            {
                using (var _zip = ZipFile.Open(aFilename, ZipArchiveMode.Read))
                {
                    if (_zip == null)
                        return null;

                    ZipArchiveEntry _part = null;
                    foreach (var _ze in _zip.Entries)
                        if (_ze.Name.Equals("document.xml", StringComparison.InvariantCultureIgnoreCase))
                        {
                            _part = _ze;
                            break;
                        }
                    if (_part == null)
                        return null;
                    using (var _reader = new StreamReader(_part.Open()))
                    {
                        if (_reader == null)
                            return null;
                        _xml = _reader.ReadToEnd();
                    }
                    if (string.IsNullOrWhiteSpace(_xml))
                        return null;

                    return _xml;
                }
            }
            catch (Exception _e)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
            }
            return null;
        }

        /// <summary>
		/// Gets a XMLMapCompanion for a specified file on Disk or in MindManager Cloud storage.
		/// Registers all Custom Icons in Icon Database.
		/// Imports all messages directly to topics.
		/// </summary>
		/// <param name="aFilename">a filename.</param>
		/// <param name="cloudMap">if document is a cloud map.</param>
		/// <param name="remoteMap">if document is on the new Google Drive, or flash card, or wifi disk, etc.</param>
		/// <returns>XML Map Companion or null</returns>
		public static XMLMapCompanion Get(string aFilename)
        {
            if (!File.Exists(aFilename))
                return null;

            // Try to get document if it is already opened
            Document _doc = null;
            foreach (Document _doc1 in MMUtils.MindManager.AllDocuments)
            {
                if (_doc1.FullName.ToLower() == aFilename.ToLower())
                {
                    _doc = _doc1;
                    break;
                }
            }

            if (_doc != null)
                return Get(_doc);

            // Map is not among opened. Get it from zip

            string _xml = String.Empty;

            try
            {
                using (var _zip = ZipFile.Open(aFilename, ZipArchiveMode.Read))
                {
                    if (_zip == null)
                        return null;

                    ZipArchiveEntry _part = null;
                    foreach (var _ze in _zip.Entries)
                        if (_ze.Name.Equals("document.xml", StringComparison.InvariantCultureIgnoreCase))
                        {
                            _part = _ze;
                            break;
                        }
                    if (_part == null)
                        return null;
                    using (var _reader = new StreamReader(_part.Open()))
                    {
                        if (_reader == null)
                            return null;
                        _xml = _reader.ReadToEnd();
                    }
                    if (string.IsNullOrWhiteSpace(_xml))
                        return null;

                    var _rc = new XMLMapCompanion
                    {
                        m_XML = _xml,
                        Path = aFilename
                    };
                    if (!_rc.Parse())
                        return null;

                    return _rc;
                }
            }
            catch (Exception _e)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show("Exception: " + _e.Message + "\r\nSource: " + _e.Source + "\r\nStack: " + _e.StackTrace);
#endif
            }
            return null;
        }

        public XmlNamespaceManager NSManager
        {
            get
            {
                if (m_manager == null)
                {
                    m_manager = new XmlNamespaceManager(m_doc.NameTable);
                    m_manager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                    m_manager.AddNamespace("ap", "http://schemas.mindjet.com/MindManager/Application/2003");
                    m_manager.AddNamespace("pri", "http://schemas.mindjet.com/MindManager/Primitive/2003");
                    m_manager.AddNamespace("cor", "http://schemas.mindjet.com/MindManager/Core/2003");
                    m_manager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                }
                return m_manager;
            }
        }

        protected XmlDocument m_doc = new XmlDocument();
        protected string m_XML = "";
        protected XmlNamespaceManager m_manager = null;
        public string Path = "";
        public static string CentralTopic;

        protected List<string> m_guids = new List<string>(); // Narrowed Guid list, in order of scan
        public static Dictionary<string, XMLTopicCompanion> m_topics = new Dictionary<string, XMLTopicCompanion>(); // Topic companions    }

        protected List<string> m_floatingGuids = new List<string>(); // Narrowed Guid list, in order of scan
        protected Dictionary<string, XMLTopicCompanion> m_floatingTopics = new Dictionary<string, XMLTopicCompanion>(); // Topic companions    }

        public static Dictionary<string, List<TopicNotesItem>> m_topicNotes = new Dictionary<string, List<TopicNotesItem>>();
    }
}