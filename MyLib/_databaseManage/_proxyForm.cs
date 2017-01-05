using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _proxyForm : Form
    {
        public _proxyForm()
        {
            InitializeComponent();
            this._useProxyCheckBox.Checked = (MyLib._myGlobal._proxyUsed == 1 ) ? true  :false;
            this._urlTextBox.Text = MyLib._myGlobal._proxyUrl;
            this._userTextBox.Text = MyLib._myGlobal._proxyUser;
            this._passwordTextBox.Text = MyLib._myGlobal._proxyPassword;
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            MyLib._myGlobal._proxyUsed = (this._useProxyCheckBox.Checked) ? 1 : 0;
            MyLib._myGlobal._proxyUrl = this._urlTextBox.Text;
            MyLib._myGlobal._proxyUser = this._userTextBox.Text;
            MyLib._myGlobal._proxyPassword = this._passwordTextBox.Text;
            this.Dispose();
        }
    }
}
