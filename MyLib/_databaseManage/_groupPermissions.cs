using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _groupPermissions : _myForm
    {
        private _menupermissions_group __menupermissions_group1;
        public _groupPermissions()
        {
            InitializeComponent();
            __menupermissions_group1  = new _menupermissions_group();
            __menupermissions_group1.ButtonExit.Visible = false;
            __menupermissions_group1.Dock = DockStyle.Fill;
            this.Controls.Add(__menupermissions_group1);
        }
        
    }
}
