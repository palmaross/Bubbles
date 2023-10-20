using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bubbles
{
    public partial class SelectStickDlg : Form
    {
        public SelectStickDlg(Dictionary<int, string> dict)
        {
            InitializeComponent();

            btnCancel.Text = Utils.getString("button.cancel");

            foreach (var item in dict)
                comboBox1.Items.Add(new StickItem(item.Key, item.Value));


            exit = true;
            comboBox1.SelectedIndex = 0;
        }
        bool exit = false;

        private void btnOK_Click(object sender, EventArgs e)
        {
            var item = comboBox1.SelectedItem as StickItem;
            StickName = item.Name;
            StickID = item.ID;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            if (exit)
            {
                exit = false;
                return;
            }

            var item = comboBox1.SelectedItem as StickItem;
            StickName = item.Name;
            StickID = item.ID;

            DialogResult = DialogResult.OK;
        }

        public string StickName;
        public int StickID;
    }

    internal class StickItem
    {
        public StickItem(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID = 0;
        public string Name = "";

        public override string ToString()
        {
            return Name;
        }
    }
}
