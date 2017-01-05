using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
	public partial class _smlChangePassword : Form
	{
        private string _userCode = "";

		public _smlChangePassword(string userCode)
		{
			InitializeComponent();
            this._userCode = userCode;
		}

		private void _buttonOK_Click(object sender, EventArgs e)
		{
            _myFrameWork __myFrameWork = new _myFrameWork();
            if (__myFrameWork._changePassword(MyLib._myGlobal._databaseConfig,MyLib._myGlobal._mainDatabase, this._userCode, this._textBoxOldPassword.Text, this._textBoxNewPassword1.Text).Equals("1"))
            {
                // เปลี่ยนรหัสผ่านใหม่สำเร็จ
                MessageBox.Show(MyLib._myGlobal._resource("warning8"), MyLib._myGlobal._resource("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("warning9"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
		}

		private void _changePassword_Load(object sender, EventArgs e)
		{

		}
	}
}