using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Bubbles.AppManager
{
    internal class XMLTopicCompanion
    {
        public XMLTopicCompanion() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLTopicCompanion"/> class.
        /// </summary>
        /// <param name="aRoot">XML node that covers the Topic</param>
        /// <param name="aManager">XML namespace manager</param>
        public XMLTopicCompanion(XmlNode aRoot, XmlNamespaceManager aManager, XMLMapCompanion aXMLDocument)
        {
            m_root = aRoot;
            m_manager = aManager;
            m_xmlDocument = aXMLDocument;
        }

        public string CentralTopicText
        {
            get
            {
                if (m_root == null)
                    return null;
                XmlNode _centralTopicNode = m_root.OwnerDocument.DocumentElement.SelectSingleNode("//ap:OneTopic/ap:Topic/ap:Text", NSManager);
                if (_centralTopicNode != null)
                    return _centralTopicNode.Attributes.GetNamedItem("PlainText").Value;
                return "";
            }
        }

        public string TopicText
        {
            get
            {
                if (m_root == null)
                    return null;
                XmlNode topicText = m_root.SelectSingleNode("ap:Text", NSManager);
                if (topicText != null)
                    return topicText.Attributes.GetNamedItem("PlainText").Value;
                return "";
            }
        }

        /// <summary>
        /// Gets plain topic Notes from XHTMLNotes
        /// </summary>
        public string NotesHtml (bool plain = false)
        {
                if (m_root == null)
                    return "";
                try
                {
                    XmlNode _notes = m_root.SelectSingleNode("ap:NotesGroup/ap:NotesXhtmlData", NSManager);
                    if (null == _notes) return "";

                    if (plain)
                        return HtmlToPlainText(_notes.InnerXml);
                    else
                        return _notes.InnerXml;
                }
                catch { return ""; }
        }

        static string HtmlToPlainText(string html)
        {
            string block = "address|article|aside|blockquote|canvas|dd|div|dl|dt|" +
              "fieldset|figcaption|figure|footer|form|h\\d|header|hr|li|main|nav|" +
              "noscript|ol|output|p|pre|section|table|tfoot|ul|video";

            // Remove the first <p> to avoid the \r\n in the begin of text
            var regex = new Regex(Regex.Escape("<p>"));
            string buf = regex.Replace(html, "", 1);

            string patNestedBlock = $"(\\s*?</?({block})[^>]*?>)+\\s*";
            buf = Regex.Replace(buf, patNestedBlock, "\n\n", RegexOptions.IgnoreCase);

            // Replace br tag to newline.
            buf = Regex.Replace(buf, @"<(br)[^>]*>", "\n", RegexOptions.IgnoreCase);

            // (Optional) remove styles and scripts.
            buf = Regex.Replace(buf, @"<(script|style)[^>]*?>.*?</\1>", "", RegexOptions.Singleline);

            // Remove all tags.
            buf = Regex.Replace(buf, @"<[^>]*(>|$)", "", RegexOptions.Multiline);

            // Replace HTML entities.
            buf = WebUtility.HtmlDecode(buf);
            return buf;
        }

        protected XmlNamespaceManager NSManager
        {
            get
            {
                if (m_manager == null)
                {
                    m_manager = new XmlNamespaceManager(m_root.OwnerDocument.NameTable);
                    m_manager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                    m_manager.AddNamespace("ap", "http://schemas.mindjet.com/MindManager/Application/2003");
                    m_manager.AddNamespace("pri", "http://schemas.mindjet.com/MindManager/Primitive/2003");
                    m_manager.AddNamespace("cor", "http://schemas.mindjet.com/MindManager/Core/2003");
                    m_manager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                }
                return m_manager;
            }
        }

        protected XmlNamespaceManager m_manager = null;
        public XmlNode m_root = null;
        protected XMLMapCompanion m_xmlDocument = null;
    }
}
