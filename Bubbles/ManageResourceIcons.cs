using System;
using System.Data;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class ManageResourceIcons : UserControl
    {
        public ResourcesDlg aParentForm { get; set; }

        public ManageResourceIcons()
        {
            InitializeComponent();

            lblIconName.Text = Utils.getString("ManageResourceIcons.lblIconName");

            using (SticksDB db = new SticksDB())
            {
                DataTable dt = db.ExecuteQuery("select * from RESOURCEGROUPS");

                foreach (DataRow dr in dt.Rows)
                {
                    int i = Convert.ToInt32(dr["icon"]);
                    switch (i)
                    {
                        case 1: txt1.Text = dr["name"].ToString(); break;
                        case 2: txt2.Text = dr["name"].ToString(); break;
                        case 3: txt3.Text = dr["name"].ToString(); break;
                        case 4: txt4.Text = dr["name"].ToString(); break;
                        case 5: txt5.Text = dr["name"].ToString(); break;
                        case 6: txt6.Text = dr["name"].ToString(); break;
                        case 7: txt7.Text = dr["name"].ToString(); break;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            aParentForm.ResourceIcons.Clear();

            // Update RESOURCEGROUPS database and ResourceIcons dictionary
            using (SticksDB db = new SticksDB())
            {
                db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + txt1.Text.Trim() + "` where icon=" + 1 + "");
                db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + txt2.Text.Trim() + "` where icon=" + 2 + "");
                db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + txt3.Text.Trim() + "` where icon=" + 3 + "");
                db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + txt4.Text.Trim() + "` where icon=" + 4 + "");
                db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + txt5.Text.Trim() + "` where icon=" + 5 + "");
                db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + txt6.Text.Trim() + "` where icon=" + 6 + "");
                db.ExecuteNonQuery("update RESOURCEGROUPS set name=`" + txt7.Text.Trim() + "` where icon=" + 7 + "");
            }

            aParentForm.Init();
            aParentForm.Height = aParentForm.thisHeight;
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            aParentForm.Height = aParentForm.thisHeight;
            this.Hide();
        }
    }
}
