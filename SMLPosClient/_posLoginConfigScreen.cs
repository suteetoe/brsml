using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posLoginConfigScreen : UserControl
    {
        private string _config_pos_screen = "config_pos_screen";
        string _provider_code = "provider_code";
        string _user_code = "user_code";
        string _user_password = "user_password";

        public _posLoginConfigScreen()
        {
            InitializeComponent();
            this.listView1.Click += new EventHandler(listView1_Click);
        }

        void listView1_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection __breakfast = this.listView1.SelectedItems;
            string __menuName = "";
            string __screenText = "";
            foreach (ListViewItem item in __breakfast)
            {
                __menuName = item.Tag.ToString();
                __screenText = item.Text;
                break;
            }

            if (__menuName.Equals(_config_pos_screen))
            {
                if (MyLib._myGlobal._userLoginSuccess)
                {
                    // show dialog config pos screen
                    MyLib._myUtil._startDialog(this, __screenText, new _configPOSScreen());
                }
                else
                {
                    MessageBox.Show("กรุณา Login เข้าสู่ระบบก่อน ถึงจะสามารถทำรายการต่อไปได้", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void _buttonLoginAdmin_Click(object sender, EventArgs e)
        {

        }
    }
}
