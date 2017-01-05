using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posLoginForm : Form
    {
        public bool _isPassed = false;
        public string _userCode = "";

        public _posLoginForm()
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
            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.erp_user._password + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + __userCode + "\' and " + _g.d.erp_user._password + "=\'" + __userPassword + "\'").Tables[0];
            if (__dt.Rows.Count > 0)
            {
                this._isPassed = true;
                this._userCode = __userCode;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login fail.");
            }
        }
    }
}
