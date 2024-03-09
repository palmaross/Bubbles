using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;

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
            HL1 = numUpDown.Location.X;
            HL2 = chIncrement.Location.X;
            HL3 = pAddMultiple.Location.X;

            if (orientation == "V")
            {
                orientation = "H"; Rotate();
            }

            toolTip1.SetToolTip(subtopic, Utils.getString("BubblesPaste.addsubtopic"));
            toolTip1.SetToolTip(nexttopic, Utils.getString("BubblesPaste.addtopic"));
            toolTip1.SetToolTip(topicbefore, Utils.getString("BubblesPaste.addbefore"));
            toolTip1.SetToolTip(ParentTopic, Utils.getString("BubblesPaste.addparent"));
            toolTip1.SetToolTip(Callout, Utils.getString("BubblesPaste.addcallout"));
            toolTip1.SetToolTip(TopicText, Utils.getString("BubbleAddTopic.TopicText"));
            toolTip1.SetToolTip(numUpDown, Utils.getString("BubbleAddTopic.numUpDown"));
            toolTip1.SetToolTip(chIncrement, Utils.getString("BubbleAddTopic.pIncrement"));
            toolTip1.SetToolTip(pAddMultiple, Utils.getString("BubblesPaste.pAddMultiple"));

            toolTip1.SetToolTip(pictureHandle, stickname);

            cmsAddMultiple.ItemClicked += ContextMenu_ItemClicked;
            cmsCommon.ItemClicked += ContextMenu_ItemClicked;
            PopulateAddMultipleMenu();
            StickUtils.SetCommonContextMenu(cmsCommon, StickUtils.typeaddtopic);

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

            //tsi.Name = "ManageTaskTemplates";
            //StickUtils.SetContextMenuImage(cmsAddMultiple.Items["ManageTaskTemplates"], "manage.png");

            using (BubblesDB db = new BubblesDB())
            {
                DataTable dt = db.ExecuteQuery("select * from MT_TEMPLATES order by templateName");

                foreach (DataRow row in dt.Rows)
                {
                    // Template name
                    MT_TemplateItem item = new MT_TemplateItem(row["templateName"].ToString(),
                        row["topicName"].ToString(), row["pattern"].ToString(), "");

                    tsi = cmsAddMultiple.Items.Add(item.Name);
                    tsi.Tag = item; tsi.Name = "MT_Template";
                    (tsi as ToolStripMenuItem).DropDown.Closing += DropDown_Closing; // do not close dropdown

                    //// Add dropdown items ////

                    ToolStripItem tsm;
                    if (!item.Pattern.StartsWith("custom"))
                    {
                        // Label "Topic Text"
                        tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("TopicTemplateDlg.lblTopicText"));
                        tsm.Font = new Font(tsi.Font, FontStyle.Bold);

                        // TextBox for user's topic text
                        ToolStripTextBox tb = new ToolStripTextBox();
                        tb.Text = item.TopicName;
                        tb.BorderStyle = BorderStyle.FixedSingle;
                        tb.Size = new Size(Manage.Width * 5, tb.Height);
                        (tsi as ToolStripMenuItem).DropDownItems.Add(tb);
                    }

                    // Label "Topic Type"
                    tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("BubbleAddTopic_AddAs"));
                    tsm.Font = new Font(tsi.Font, FontStyle.Bold);
                    //(tsi as ToolStripMenuItem).DropDownItems.Add(new ToolStripSeparator());

                    // Topic types
                    string topictype = row["reserved1"].ToString();
                    if (topictype == "") topictype = "subtopic";

                    tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("TopicTemplateDlg.rbtnSubtopic"));
                    (tsm as ToolStripMenuItem).CheckOnClick = true;
                    (tsm as ToolStripMenuItem).Checked = topictype == "subtopic";
                    tsm.Name = "subtopic";
                    tsm.Click += SubmenuItem_Click;
                    tsm.Tag = tsi as ToolStripMenuItem;

                    tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("TopicTemplateDlg.rbtnNextTopic"));
                    (tsm as ToolStripMenuItem).CheckOnClick = true;
                    (tsm as ToolStripMenuItem).Checked = topictype == "nexttopic";
                    tsm.Name = "nexttopic";
                    tsm.Click += SubmenuItem_Click;
                    tsm.Tag = tsi as ToolStripMenuItem;

                    tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("TopicTemplateDlg.rbtnTopicBefore"));
                    (tsm as ToolStripMenuItem).CheckOnClick = true;
                    (tsm as ToolStripMenuItem).Checked = topictype == "topicbefore";
                    tsm.Name = "topicbefore";
                    tsm.Click += SubmenuItem_Click;
                    tsm.Tag = tsi as ToolStripMenuItem;
                }
            }
        }

        /// <summary>
        /// Check/Uncheck submenu items (topic type) like radiobuttons
        /// </summary>
        private void SubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = sender as ToolStripItem; // submenu item
            ToolStripMenuItem tsm = tsi.Tag as ToolStripMenuItem; // submenu parent

            if ((tsi as ToolStripMenuItem).Checked) // uncheck other items
            {
                foreach (var item in tsm.DropDownItems.OfType<ToolStripMenuItem>())
                    if (item.Name != tsi.Name && item.Checked) item.Checked = false;
            }
            else // item unchecking, check the primary item
            {
                (tsm.DropDownItems["subtopic"] as ToolStripMenuItem).Checked = true;
            }
        }

        /// <summary>
        /// Do not close submenu after the click on item
        /// </summary>
        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
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
                ToolStripMenuItem tsm = e.ClickedItem as ToolStripMenuItem;
                MT_TemplateItem item = e.ClickedItem.Tag as MT_TemplateItem;

                string topicName = "", topicType = "subtopic";
                foreach (var _item in tsm.DropDownItems)//.OfType<ToolStripMenuItem>())
                {
                    if (_item is ToolStripTextBox tb)
                        topicName = tb.Text;
                    else if (_item is ToolStripMenuItem _tsm)
                        if (_tsm.Checked) topicType = _tsm.Name;
                }

                cmsAddMultiple.Close();

                AddTopics(item, topicName, topicType);
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

        private void AddTopics(MT_TemplateItem item, string topicName, string topicType)
        {
            TopicsToAdd.Clear();

            List<string> pattern = item.Pattern.Split(new string[] { "###" }, StringSplitOptions.None).ToList();
            string template = pattern[0];

            if (template == "topics")
            {
                if (topicName == "") return;
                for (int i = 0; i < Convert.ToInt32(pattern[1]); i++) 
                    TopicsToAdd.Add(topicName);
            }
            else if (template == "custom")
            {
                for (int i = 1; i < pattern.Count; i++)
                    TopicsToAdd.Add(pattern[i]);
            }
            else if (template == "increment")
            {
                GetTopicList(topicName, pattern[1]);
            }

            if (TopicsToAdd != null && TopicsToAdd.Count > 0)
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    transTopicType = topicType;
                    AddTopicTransaction(Utils.getString("addtopics.transactionname.addtopics"));
                }
            }
        }

        List<string> GetTopicList(string name, string incrementdata)
        {
            TopicsToAdd.Clear();
            string[] incrementData = incrementdata.Split(',');

            int start = Convert.ToInt32(incrementData[0]);
            int finish = Convert.ToInt32(incrementData[1]);
            int step = Convert.ToInt32(incrementData[2]);

            bool begin = incrementData[3] == "begin";

            if (step > 0)
            {
                for (int i = start; i <= finish; i += step)
                    TopicsToAdd.Add(GetPreview(name, i, begin));
            }
            else
            {
                for (int i = start; i >= finish; i += step)
                    TopicsToAdd.Add(GetPreview(name, i, begin));
            }

            return TopicsToAdd;
        }

        string GetPreview(string topictext, int number, bool begin)
        {
            if (begin) topictext = number + " " + topictext;
            else topictext += " " + number;

            return topictext;
        }

        public void Rotate()
        {
            orientation = StickUtils.RotateStick(this, Manage, orientation);

            if (orientation == "H")
            {
                TopicText.Location = new Point(TopicText.Location.Y, V1.Location.Y);
                TopicText.Width = this.Height * 2;
                numUpDown.Location = new Point(HL1, V1.Location.Y);
                chIncrement.Location = new Point(HL2, V1.Location.Y);
                pAddMultiple.Location = new Point(HL3, V1.Location.Y);
                this.Width = RealLength;
            }
            else
            {
                TopicText.Location = new Point(0, TopicText.Location.X);
                TopicText.Width = this.Width;
                numUpDown.Location = new Point(0, V1.Location.X);
                chIncrement.Location = new Point(0, V2.Location.X);
                pAddMultiple.Location = new Point(pAddMultiple.Location.X, V3.Location.X);
                this.Height -= TopicText.Width * 2;
            }
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

                collapseState = this.Location; // remember collapsed location
                collapseOrientation = orientation;
                StickUtils.Expand(this, RealLength, orientation, cmsCommon);
                collapsed = false;
            }
            else // Collapse stick
            {
                if (ExpandAll) return;

                StickUtils.Collapse(this, orientation, cmsCommon);
                collapsed = true;

                if (collapseState.X + collapseState.Y > 0) // ignore initial collapse command
                {
                    this.Location = collapseState; // restore collapsed location
                    if (orientation != collapseOrientation) Rotate();
                }
            }
        }
        Point collapseState = new Point(0, 0);
        string collapseOrientation = "N";

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
                //if (MMUtils.ActiveDocument == null || MMUtils.ActiveDocument.Selection.OfType<Topic>().Count() == 0)
                //{
                //    MessageBox.Show(Utils.getString("BubblesPaste.pAddMultiple.error"), "",
                //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                using (AddTopicTemplateDlg dlg = new AddTopicTemplateDlg())
                {
                    dlg.changed = false;
                    dlg.ShowDialog();
                    if (dlg.changed)
                    {
                        // Templates were changed. Update menu
                        PopulateAddMultipleMenu();
                    }
                }
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
                TopicsToAdd.Clear();
                AddTopic((sender as PictureBox).Name);
            }
        }

        /// <summary>
        /// Add topic or multiple topics
        /// </summary>
        /// <param name="topictype">Which topic perform the operation with</param>
        void AddTopic(string topictype)
        {
            if (MMUtils.ActiveDocument == null) return;
            if (MMUtils.ActiveDocument.Selection.PrimaryTopic == null) return;

            string topicText = TopicText.Text;
            transTopicType = topictype;

            // Create TopicsToAdd list (for adding multiple topics)
            if (topictype == "subtopic" || topictype == "nexttopic" || topictype == "topicbefore")
            {
                if (chIncrement.Checked)
                {
                    for (int i = 1; i <= numUpDown.Value; i++)
                        TopicsToAdd.Add(topicText + i);
                }
                else
                {
                    if (topicText == "") topicText = "#default#";
                    for (int i = 1; i <= numUpDown.Value; i++)
                        TopicsToAdd.Add(topicText);
                }
            }
            else // parent topic or callout topic. Can be one only.
            {
                if (topicText == "") topicText = "#default#";
                TopicsToAdd.Add(topicText);

                //IDataObject data_object = System.Windows.Forms.Clipboard.GetDataObject();
                //if (data_object.GetDataPresent(DataFormats.Rtf))
                //{
                //    rchRtf.Rtf =
                //        data_object.GetData(DataFormats.Rtf).ToString();
                //    txtRtfCode.Text =
                //        data_object.GetData(DataFormats.Rtf).ToString();
                //}
            }

            // Add topics
            AddTopicTransaction(Utils.getString("addtopics.transactionname.addtopics"));
        }
        string transTopicType;
        public List<string> TopicsToAdd = new List<string>();

        void AddTopicTransaction(string trname)
        {
            Transaction _tr = MMUtils.ActiveDocument.NewTransaction(trname);
            _tr.IsUndoable = true;
            _tr.Execute += new ITransactionEvents_ExecuteEventHandler(AddTopics);
            _tr.Start();
        }

        public void AddTopics(Document pDocument)
        {
            if (TopicsToAdd.Count > 1 && transTopicType == "nexttopic")
                TopicsToAdd = TopicsToAdd.Reverse<string>().ToList();

            if (transTopicType == "ParentTopic") // selected topics will be subtopics of the future parent topic
            {
                StickUtils.AddTopic(MMUtils.ActiveDocument.Selection.PrimaryTopic, transTopicType, TopicsToAdd[0]);
            }
            else
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    foreach (var name in TopicsToAdd)
                        StickUtils.AddTopic(t, transTopicType, name);
                }
            }
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

        int HL1, HL2, HL3;
    }

    public class MT_TemplateItem
    {
        public MT_TemplateItem(string name, string topicName, string pattern, string topicType)
        {
            Name = name;
            TopicName = topicName;
            Pattern = pattern;
            TopicType = topicType;
        }

        public string Name = "";
        public string TopicName = "";
        public string Pattern = "";
        public string TopicType = "";

        public override string ToString()
        {
            return Name;
        }
    }
}
