using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _userPermissions : _myForm
    {
        private _menupermissions_user __menupermissions_user1;
        public _userPermissions()
        {
            InitializeComponent();
            __menupermissions_user1 = new _menupermissions_user();
            __menupermissions_user1.ButtonExit.Visible = false;
            __menupermissions_user1.Dock = DockStyle.Fill;
            this.Controls.Add(__menupermissions_user1);
        }
    }
}
