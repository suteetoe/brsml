using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace _g
{
    public partial class _userGroupDetailControl : UserControl
    {
        public _userGroupDetailControl()
        {
            InitializeComponent();

            this._userListGrid._table_name = _g.d.erp_user_group_detail._table;
            this._userListGrid._addColumn("Select", 11, 1, 10);
            this._userListGrid._addColumn(_g.d.erp_user_group_detail._user_code, 1, 10, 30);
            this._userListGrid._addColumn(_g.d.erp_user_group_detail._user_name, 1, 10, 60);
            this._userListGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_userListGrid__alterCellUpdate);
            //
            this._userSelectedGrid._table_name = _g.d.erp_user_group_detail._table;
            this._userSelectedGrid._addColumn(_g.d.erp_user_group_detail._user_code, 1, 10, 30, true);
            this._userSelectedGrid._addColumn(_g.d.erp_user_group_detail._user_name, 1, 10, 70, false);
            //
            this.Dock = DockStyle.Fill;
        }

        void _userListGrid__alterCellUpdate(object sender, int row, int column)
        {
            ArrayList __selectUserCode = new ArrayList();
            ArrayList __selectUserName = new ArrayList();
            for (int __row = 0; __row < this._userListGrid._rowData.Count; __row++)
            {
                string __select = this._userListGrid._cellGet(__row, 0).ToString();
                if (__select.Equals("1"))
                {
                    string __code = this._userListGrid._cellGet(__row, 1).ToString();
                    if (__code.Trim().Length > 0)
                    {
                        string __name = this._userListGrid._cellGet(__row, 2).ToString();
                        __selectUserCode.Add(__code);
                        __selectUserName.Add(__name);
                    }
                }
            }
            this._userSelectedGrid._clear();
            for (int __loop = 0; __loop < __selectUserCode.Count; __loop++)
            {
                int __addr = this._userSelectedGrid._addRow();
                this._userSelectedGrid._cellUpdate(__addr, 0, __selectUserCode[__loop], false);
                this._userSelectedGrid._cellUpdate(__addr, 1, __selectUserName[__loop], false);
            }
        }
    }
}
