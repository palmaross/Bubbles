using Mindjet.MindManager.Interop;
using PRAManager;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class OT_MapOpsDlg : Form
    {
        public OT_MapOpsDlg()
        {
            InitializeComponent();

            this.Paint += This_Paint; // paint the border

            foreach (Document doc in MMUtils.MindManager.VisibleDocuments)
            {
                int i = MapList.Items.Add(new MapItem(doc.CentralTopic.Text, doc));
                MapList.SetItemChecked(i, true);
            }
        }

        private void This_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, System.Drawing.Color.Black, ButtonBorderStyle.Solid);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MapList.Items.Count; i++)
            {
                MapItem item = MapList.Items[i] as MapItem;
                if (item.doc == null || !item.doc.IsValid) continue;

                if (chAddToStix.Checked)
                {

                }
                if (chSave.Checked)
                {
                    item.doc.Save();
                }
                if (chClose.Checked)
                {
                    if (item.doc.IsModified)
                    {
                        DialogResult dr = MessageBox.Show(
                            String.Format(Utils.getString("OT_MapOpsDlg.closenotsaved"), item.title), "MindManager",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (dr == DialogResult.Cancel) continue;
                        else if (dr == DialogResult.Yes) item.doc.Save();
                    }
                    Thread.Sleep(1000);
                    item.doc.Close();
                }
            }

            MapList.Items.Clear();
            DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OT_MapOpsDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            MapList.Items.Clear();
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll.Checked)
            {
                for (int i = 0; i < MapList.Items.Count; i++)
                    MapList.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < MapList.Items.Count; i++)
                    MapList.SetItemChecked(i, false);
            }
        }
    }

    public class MapItem
    {
        public MapItem(string _title, Document _doc)
        {
            doc = _doc;
            title = _title;
        }

        public string title = "";
        public Document doc = null;

        public override string ToString()
        {
            return title;
        }
    }
}
