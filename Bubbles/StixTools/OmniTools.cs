using System;
using PRAManager;
using Mindjet.MindManager.Interop;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Controls;

namespace Bubbles
{
    internal class OmniTools
    {
        public static void RunTool(string tool, Form form = null, string orientation = "")
        {
            switch (tool)
            {
                case "OT_SaveAll":
                    foreach (Document doc in MMUtils.MindManager.AllDocuments)
                        doc.Save();
                    break;
                case "OT_CloseAll":
                    if (MMUtils.ActiveDocument == null) return;

                    using (CloseMapsDlg dlg = new CloseMapsDlg())
                        dlg.ShowDialog();
                    
                    break;
                case "OT_MapContent":
                    if (MMUtils.ActiveDocument == null) return;

                    if (aNavigationDlg == null || aNavigationDlg.IsDisposed || !aNavigationDlg.Visible)
                    {
                        aNavigationDlg = null;
                        aNavigationDlg = new MapContentDlg();
                    }
                    else return;

                    int topicCount = MMUtils.ActiveDocument.CentralTopic.SubTopics.Count + 1;
                    int itemheight = aNavigationDlg.listView1.GetItemRect(0).Height;

                    if (topicCount <= 10) // If not a big amount, change height to adjust items count 
                    {
                        aNavigationDlg.thisHeight = topicCount * itemheight + aNavigationDlg.itemHeight.Width;
                        aNavigationDlg.listView1.Scrollable = false;
                    }

                    // Get tools list location
                    Rectangle child = aNavigationDlg.RectangleToScreen(aNavigationDlg.ClientRectangle);
                    aNavigationDlg.Location = StixUtils.GetChildLocation(form, child, orientation, "tools");

                    aNavigationDlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
                    break;
            }
        }

        static MapContentDlg aNavigationDlg = null;
    }
}
