using Mindjet.MindManager.Interop;
using PRAManager;
using PRMapCompanion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppManager
{
    internal class TransactionWrapper : Object
    {
        public enum TransactionType
        {
            UNDEFINED,
            ADD_STRIP_ICON,
            REMOVE_STRIP_ICON,
        }

        public TransactionWrapper(Topic aTopic, TransactionType aType, string args, 
            string aUndoCaption = "Audio Note", bool aUndoable = false)
        {
            m_topic = aTopic;
            m_type = aType;
            m_undoCaption = aUndoCaption;
            m_undoable = aUndoable;
            Args = args;
        }

        public void Execute(bool aWaitForCompletion = false)
        {
            Transaction _tr = m_topic.Document.NewTransaction(m_undoCaption);
            _tr.IsUndoable = m_undoable;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(_tr_Execute);
            _tr.Start();
            if (aWaitForCompletion)
            {
                while (_tr.IsExecuting)
                    System.Threading.Thread.Sleep(15);
                //_tr.Commit();
            }
        }

        private void _tr_Execute(Document pDocument)
        {
            switch (m_type)
            {
                case TransactionType.ADD_STRIP_ICON:
                    {
                        try
                        {
                            m_topic.AddControlStripType(controlStripURI);
                            m_topic.GetAttributes(STRIP_URI).SetAttributeValue(AUDIO_PATH, Args);
                        }
                        catch { }
                        break;
                    }
                case TransactionType.REMOVE_STRIP_ICON:
                    {
                        try
                        {
                            m_topic.RemoveControlStripType(controlStripURI);
                        }
                        catch { }
                        break;
                    }
            }

        }
        #region Static Wrappers

        public static void TopicAddStripIcon(Topic aTopic, string aURI)
        {
            TransactionWrapper _tr = new TransactionWrapper(aTopic, TransactionType.ADD_STRIP_ICON, "tr-AddStripIcon")
            {
                controlStripURI = aURI
            };
            _tr.Execute();
        }

        public static void TopicRemoveStripIcon(Topic aTopic, string aURI)
        {
            TransactionWrapper _tr = new TransactionWrapper(aTopic, TransactionType.REMOVE_STRIP_ICON, "tr-RemoveStripIcon")
            {
                controlStripURI = aURI
            };
            _tr.Execute();
        }

        #endregion
        protected TransactionType m_type = TransactionType.UNDEFINED;
        protected Topic m_topic = null;
        protected string m_undoCaption = "";
        protected bool m_undoable = true;

        public string controlStripURI = "";
        public string Args = "";

        public const string STRIP_URI = "OMNISTIX_STRIPICON_OMNIAUDIO";
        public const string AUDIO_PATH = "OMNIAUDIO_PATH";
        public const string AUDIO_LENGTH = "OMNIAUDIO_LENGTH";
    }
}
