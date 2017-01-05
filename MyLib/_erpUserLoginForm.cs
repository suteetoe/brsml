using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _erpUserLoginForm : Form
    {
        public bool _isPassed = false;
        public string _userCode = "";
        public string _userName = "";

        public _erpUserLoginForm()
        {
            InitializeComponent();

            this._userCodeTextBox.KeyDown += new KeyEventHandler(_TextBox_KeyUp);
            this._passwordTextBox.KeyDown += new KeyEventHandler(_TextBox_KeyUp);

        }

        void _TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && e.Control == false)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
                return;
            }
        }

        private void _loginButton_Click(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __userCode = this._userCodeTextBox.Text;
            string __userPassword = this._passwordTextBox.Text;
            DataTable __dt = __myFrameWork._queryShort("select name_1,password from erp_user where code=\'" + __userCode + "\' ").Tables[0];
            if (__dt.Rows.Count > 0)
            {
                string __getPassword = __dt.Rows[0]["password"].ToString();
                if (__getPassword.Equals(__userPassword))
                {

                    this._isPassed = true;
                    this._userCode = __userCode;
                    this._userName = __dt.Rows[0]["name_1"].ToString();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login fail.");
                }
            }
            else
            {
                MessageBox.Show("Login fail.");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.Equals(Keys.Escape))
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
