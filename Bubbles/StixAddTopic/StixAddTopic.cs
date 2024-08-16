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
using Color = System.Drawing.Color;

namespace Bubbles
{
    internal partial class StixAddTopic : Form
    {
        public StixAddTopic(int ID, string _orientation, string stickname)
        {
            InitializeComponent();

            this.Tag = ID;
            orientation = _orientation;

            helpProvider1.HelpNamespace = Utils.dllPath + "OmniStix.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetHelpKeyword(this, "AddTopicStix.htm");

            RealLength = this.Width; InitialLength = this.Width;
            HL1 = numUpDown.Location.X;
            HL2 = chIncrement.Location.X;
            HL3 = pAddMultiple.Location.X;
            VL1 = V1.Location.X;
            VL2 = V2.Location.X;
            VL3 = V3.Location.X;

            if (orientation == "V") { orientation = "H"; Rotate(); }

            toolTip1.SetToolTip(subtopic, Utils.getString("AddTopicStix.addsubtopic"));
            toolTip1.SetToolTip(nexttopic, Utils.getString("AddTopicStix.addtopic"));
            toolTip1.SetToolTip(topicbefore, Utils.getString("AddTopicStix.addbefore"));
            toolTip1.SetToolTip(ParentTopic, Utils.getString("AddTopicStix.addparent"));
            toolTip1.SetToolTip(Callout, Utils.getString("AddTopicStix.addcallout"));
            toolTip1.SetToolTip(TopicText, Utils.getString("AddTopicStix.TopicText.tooltip"));
            toolTip1.SetToolTip(numUpDown, Utils.getString("AddTopicStix.numUpDown"));
            toolTip1.SetToolTip(chIncrement, Utils.getString("AddTopicStix.pIncrement"));
            toolTip1.SetToolTip(pAddMultiple, Utils.getString("AddTopicStix.pAddMultiple"));

            TopicText.Text = Utils.getString("AddTopicStix.TopicText");
            TopicText.ForeColor = SystemColors.ControlDark;
            TopicText.GotFocus += TopicText_GotFocus;
            TopicText.LostFocus += TopicText_LostFocus;

            myToolTip1.SetToolTip(pictureHandle, stickname +
                Utils.getString("StixAddTopic.description") + Utils.getString("HeadIcon.tooltip.tips"));
            toolTip1.SetToolTip(Manage, Utils.getString("ManageIcon.tooltip"));

            cmsAddMultiple.ItemClicked += ContextMenu_ItemClicked;
            cmsCommon.ItemClicked += ContextMenu_ItemClicked;
            PopulateAddMultipleMenu();
            StixUtils.SetCommonContextMenu(cmsCommon, StixUtils.typeaddtopic);

            // Resizing window causes black strips...
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            pictureHandle.MouseDown += PictureHandle_MouseDown;
            pictureHandle.MouseDoubleClick += (sender, e) => this.Hide();
            this.Paint += this_Paint; // paint the border depending on scale factor

            // Apply scale factor
            fsize = TopicText.Font.Size; ffsize = chIncrement.Font.Size;
            scaleFactor = Convert.ToInt32(Utils.getRegistry("ScaleFactor_Stix", "100"));
            ScaleStick(100F, scaleFactor);
        }

        private void TopicText_LostFocus(object sender, EventArgs e)
        {
            if (TopicText.Text == "")
            {
                TopicText.ForeColor = SystemColors.ControlDark;
                TopicText.Text = Utils.getString("AddTopicStix.TopicText");
            }
        }

        private void TopicText_GotFocus(object sender, EventArgs e)
        {
            if (TopicText.ForeColor == SystemColors.ControlDark)
            {
                TopicText.ForeColor = SystemColors.WindowText;
                TopicText.Text = "";
            }
        }

        public void ScaleStick(float fromScale, float toScale)
        {
            if (fromScale == toScale) return;
            if (toScale < 100 || toScale > 267) return;

            float scale = 100F / fromScale;
            scaleFactor = toScale;

            if (scale != 1)
                this.Scale(new SizeF(scale, scale)); // reset to 100%

            if (toScale != 100)
                this.Scale(new SizeF(toScale / 100, toScale / 100)); // scale

            float _fsize = fsize * (toScale / 100);
            float _ffsize = ffsize * (toScale / 100);

            numUpDown.Font = new Font(numUpDown.Font.FontFamily, _fsize);
            TopicText.Font = new Font(numUpDown.Font.FontFamily, _fsize);
            chIncrement.Font = new Font(numUpDown.Font.FontFamily, _fsize);

            if (orientation == "H") { RealLength = this.Width; }
            else RealLength = (int)(InitialLength * (toScale / 100));
        }
        float fsize, ffsize;

        private void this_Paint(object sender, PaintEventArgs e)
        {
            if (scaleFactor < 125) return;
            int width = 1;
            //if (scaleFactor > 200) width = 2;
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid,
                Color.Black, width, ButtonBorderStyle.Solid, Color.Black, width, ButtonBorderStyle.Solid);
        }

        void PopulateAddMultipleMenu()
        {
            cmsAddMultiple.Items.Clear();
            
            ToolStripItem tsi = null;

            using (StixDB db = new StixDB())
            {
                DataTable dt = db.ExecuteQuery("select * from ADDTOPIC_TEMPLATES order by templateName");

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
                    tsm = (tsi as ToolStripMenuItem).DropDownItems.Add(Utils.getString("AddTopicStix_AddAs"));
                    tsm.Font = new Font(tsi.Font, FontStyle.Bold);
                    //(tsi as ToolStripMenuItem).DropDownItems.Add(new ToolStripSeparator());

                    // Topic types
                    string topictype = row["topicType"].ToString();
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

            cmsAddMultiple.Items.Add(new ToolStripSeparator());
            tsi = cmsAddMultiple.Items.Add(Utils.getString("AddTopicStix.ManageTemplates"));
            tsi.Name = "ManageTemplates";
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
                foreach (var _item in tsm.DropDownItems)
                {
                    if (_item is ToolStripTextBox tb)
                        topicName = tb.Text;
                    else if (_item is ToolStripMenuItem _tsm)
                        if (_tsm.Checked) topicType = _tsm.Name;
                }

                cmsAddMultiple.Close();

                AddTopics(item, topicName, topicType);
            }
            else if (e.ClickedItem.Name == "ManageTemplates")
            {
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
            else if (e.ClickedItem.Name == "BI_rotate")
            {
                Rotate();
            }
            else if (e.ClickedItem.Name == "BI_close")
            {
                StixMain.STICKS.Remove((int)this.Tag);
                this.Close();
            }
            else if (e.ClickedItem.Name == "BI_help")
            {
                Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.Topic, "AddTopicStix.htm");
            }
            else if (e.ClickedItem.Name == "BI_store")
            {
                StixUtils.SaveStick(this.Bounds, (int)this.Tag, orientation);
            }
            else if (e.ClickedItem.Name == "BI_scale")
            {
                ScaleStickDlg dlg = new ScaleStickDlg(this, StixUtils.typeaddtopic, scaleFactor);
                dlg.Location =
                    StixUtils.GetChildLocation(this, dlg.Bounds, orientation, "scale");
                dlg.Show(new WindowWrapper((IntPtr)MMUtils.MindManager.hWnd));
            }
        }

        private void AddTopics(MT_TemplateItem item, string topicName, string topicType)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

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
            if (begin) topictext = number + topictext;
            else topictext += number;

            return topictext;
        }

        public void Rotate()
        {
            orientation = StixUtils.RotateStick(this, Manage, orientation);

            if (orientation == "H")
            {
                TopicText.Location = new Point(TopicText.Location.Y, Manage.Location.Y);
                TopicText.Width = this.Height * 2;
                numUpDown.Location = new Point((int)(HL1 * (scaleFactor / 100)), Manage.Location.Y);
                chIncrement.Location = new Point((int)(HL2 * (scaleFactor / 100)), Manage.Location.Y);
                pAddMultiple.Location = new Point((int)(HL3 * (scaleFactor / 100)), Manage.Location.Y);
                this.Width = RealLength;
            }
            else
            {
                TopicText.Location = new Point(0, TopicText.Location.X);
                TopicText.Width = this.Width;
                numUpDown.Location = new Point(0, (int)(VL1 * (scaleFactor / 100)));
                int chLocX = ((this.Width - chIncrement.Width) / 2) - 1;
                chIncrement.Location = new Point(chLocX, (int)(VL2 * (scaleFactor / 100)));
                pAddMultiple.Location = new Point(Manage.Location.X, (int)(VL3 * (scaleFactor / 100)));
                this.Height -= (int)(this.Width * 1.8);
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
            if (Utils.FreeVersionLimitExceeded(StixUtils.typeaddtopic)) return;

            if (e.Button == MouseButtons.Left)
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
                StixUtils.SourceURL = ""; StixUtils.Links.Clear();
                AddTopic((sender as PictureBox).Name.ToLower());
            }
        }

        /// <summary>
        /// Add topic or multiple topics
        /// </summary>
        /// <param name="topictype">Which topic perform the operation with</param>
        void AddTopic(string topictype)
        {
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            string topicText = TopicText.Text;
            if (TopicText.ForeColor == SystemColors.ControlDark) topicText = "";
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
            if (Utils.ActiveDocumentOrSelectionNull()) return;

            if (TopicsToAdd.Count > 1 && transTopicType == "nexttopic")
                TopicsToAdd = TopicsToAdd.Reverse<string>().ToList();

            if (transTopicType == "parenttopic") // selected topics will be subtopics of the future parent topic
            {
                StixUtils.AddTopic(MMUtils.ActiveDocument.Selection.PrimaryTopic, transTopicType, TopicsToAdd[0]);
            }
            else
            {
                foreach (Topic t in MMUtils.ActiveDocument.Selection.OfType<Topic>())
                {
                    foreach (var name in TopicsToAdd)
                        StixUtils.AddTopic(t, transTopicType, name);
                }
            }
        }

        string orientation = "H";
        int RealLength, InitialLength;
        public float scaleFactor = 100;

        // For this_MouseDown
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        InputSimulator sim = new InputSimulator();

        private void chIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (Utils.FreeVersionLimitExceeded(StixUtils.typeaddtopic, chIncrement.Checked))
            {
                chIncrement.Checked = false;
                return;
            }

            if (chIncrement.Checked)
                chIncrement.Font = new Font(chIncrement.Font, FontStyle.Bold);
            else
                chIncrement.Font = new Font(chIncrement.Font, FontStyle.Regular);
        }

        int HL1, HL2, HL3, VL1, VL2, VL3;
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
