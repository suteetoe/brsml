using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _serverChangePassword : Form
    {
        public _serverChangePassword()
        {
            InitializeComponent();
        }

        private void _buttonOK_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._webServiceServer = this._urlTextBox.Text;
            _myFrameWork __myFrameWork = new _myFrameWork();
            if (__myFrameWork._systemLogin(this._textBoxOldPassword.Text, this._urlTextBox.Text) == "1")
            {
                if (this._textBoxNewPassword1.Text.Length > 1 || this._textBoxNewPassword1.Text.Equals(this._textBoxNewPassword2.Text))
                {
                    if (__myFrameWork._systemChangePassword(this._textBoxOldPassword.Text, this._textBoxNewPassword1.Text, this._urlTextBox.Text).Equals("1"))
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("warning11"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("warning12"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning13"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("warning14"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _serverChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}
