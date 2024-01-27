using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bubbles
{
    internal partial class BubbleAddTopic : Form
    {
        public BubbleAddTopic(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation.Substring(0, 1); // "H" or "V"
            collapsed = _orientation.Substring(1, 1) == "1";

            helpProvider1.HelpNamespace = Utils.dllPath + "Sticks.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "AddTopicStick.htm");

            RealLength = this.Width;

            if (orientation == "V")
            {
                orientation = "H"; Rotate();
            }

            toolTip1.SetToolTip(Subtopic, Utils.getString("BubblesPaste.addsubtopic"));
            toolTip1.SetToolTip(NextTopic, Utils.getString("BubblesPaste.addtopic"));
            toolTip1.SetToolTip(TopicBefore, Utils.getString("BubblesPaste.addbefore"));
            toolTip1.SetToolTip(ParentTopic, Utils.getString("BubblesPaste.addparent"));
            toolTip1.SetToolTip(Callout, Utils.getString("BubblesPaste.addcallout"));
            toolTip1.SetToolTip(TopicText, Utils.getString("BubbleAddTopic.TopicText"));
            toolTip1.SetToolTip(numUpDown, Utils.getString("BubbleAddTopic.numUpDown"));
            toolTip1.SetToolTip(chIncrement, Utils.getString("BubbleAddTopic.pIncrement"));
            toolTip1.SetToolTip(pAddMultiple, Utils.getString("BubblesPaste.pAddMultiple"));

            toolTip1.SetToolTip(pictureHandle, stickname);

            cmsAddMultiple.ItemClicked += ContextMenu_ItemClicked;
            PopulateAddMultipleMenu();
            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typepaste);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => Collapse();

            Manage.MouseHover += (sender, e) => StickUtils.ShowCommandPopup(this, orientation, StickUtils.typepaste);

            if (collapsed)
            {
                collapsed = false; Collapse();
            }
        }
        void PopulateAddMultipleMenu()
        {
            cmsAddMultiple.Items.Clear();

            ToolStripItem tsi = null;

            //ToolStripItem tsi = cmsAddMultiple.Items.Add(Utils.getString("taskinfo.quicktask.manage"));
            //tsi.Name = "ManageTaskTemplates";
            //StickUtils.SetContextMenuImage(cmsAddMultiple.Items["ManageTaskTemplates"], "manage.png");
            //cmsAddMultiple.Items.Add(new ToolStripSeparator());

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from MT_TEMPLATES order by templateName");

                foreach (DataRow row in dt.Rows)
                {
                    MT_TemplateItem item = new MT_TemplateItem(row["templateName"].ToString(),
                        row["topicName"].ToString(), row["pattern"].ToString());

                    tsi = cmsAddMultiple.Items.Add(item.Name);
                    tsi.Tag = item; tsi.Name = "MT_Template";
                }
            }
        }

        private void PictureHandle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
            base.OnMouseDown(e);
        }

        private void ContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "MT_Template")
            {
                MT_TemplateItem item = e.ClickedItem.Tag as MT_TemplateItem;
            }
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                BubblesButton.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "PasteStick.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StickUtils.SaveStick(this.Bounds, (int)this.Tag, orientation, collapsed);
            }
            else if (e.ClickedItem.Name == "BI_collapse")
            {
                Collapse();
            }
        }

        public void Rotate()
        {
            orientation = StickUtils.RotateStick(this, Manage, orientation);
        }

        /// <summary>
        /// Collapse/Expand stick
        /// </summary>
        /// <param name="CollapseAll">"Collapse All" command from Main Menu</param>
        /// <param name="ExpandAll">"Expand All" command from Main Menu</param>
        public void Collapse(bool CollapseAll = false, bool ExpandAll = false)
        {
            if (collapsed) // Expand stick
            {
                if (CollapseAll) return;
                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;
                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;
            }
        }

        private void Manage_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in cmsCommon.Items)
                item.Visible = true;

            cmsCommon.Show(Cursor.Position);
        }

        private void pAddMultiple_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                {
                    MessageBox.Show(Utils.getString("BubblesPaste.pAddMultiple.error"), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (AddTopicTemplateDlg dlg = new AddTopicTemplateDlg())
                    dlg.ShowDialog();
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ToolStripItem item in cmsAddMultiple.Items)
                    item.Visible = true;

                cmsAddMultiple.Show(Cursor.Position);
            }
        }

        private void AddTopic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TopicList.Clear();
                AddTopic((sender as PictureBox).Name);
            }
        }

        /// <summary>
        /// Add topic or paste to topic
        /// </summary>
        /// <param name="topictype">With which topic perform the operation</param>
        void AddTopic(string topictype)
        {
            if (MMUtils.ActiveDocument == null) return;
            int count = MMUtils.ActiveDocument.Selection.OfType<Topic>().Count();
            if (count == 0) return;

            string topicText = TopicText.Text;
            transTopicType = topictype;

            if (topictype == "Subtopic" || topictype == "NextTopic" || topictype == "TopicBefore")
            {
                if (numUpDown.Value > 1)
                {
                    if (chIncrement.Checked)
                    {
                        for (int i = 1; i <= numUpDown.Value; i++)
                            TopicList.Add(topicText + i);
                    }
                    else
                    {
                        if (topicText == "") topicText = "#default#";
                        for (int i = 1; i <= numUpDown.Value; i++)
                            TopicList.Add(topicText);
                    }
                }
                else
                    TopicList.Add("#default#");
            }

            // add topic
            foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
            {
                transTopic = t; 
                AddTopicTransaction(Utils.getString("addtopics.transactionname.insert"));
            }
        }
        Topic transTopic;
        string transTopicType;
        public List<string> TopicList = new List<string>();

        void AddTopicTransaction(string trname)
        {
            Transaction _tr = MMUtils.ActiveDocument.NewTransaction(trname);
            _tr.IsUndoable = true;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(AddTopics);
            _tr.Start();
        }

        public void AddTopics(Document pDocument)
        {
            if (TopicList.Count > 1 && transTopicType == "NextTopic")
                TopicList = TopicList.Reverse<string>().ToList();

            foreach (var name in TopicList)
                StickUtils.AddTopic(transTopic, transTopicType, name);
        }

        string orientation = "H";
        int RealLength;
        bool collapsed = false;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        InputSimulator sim = new InputSimulator();
    }

    public class MT_TemplateItem
    {
        public MT_TemplateItem(string name, string topicName, string pattern)
        {
            Name = name;
            TopicName = topicName;
            Pattern = pattern;
        }

        public string Name = "";
        public string TopicName = "";
        public string Pattern = "";

        public override string ToString()
        {
            return Name;
        }
    }
}
