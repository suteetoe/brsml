using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLActivesync
{
    public partial class _branchSyncControl : UserControl
    {
        public _branchSyncControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._branchGrid._table_name = _g.d.sync_branch_list._table;
            this._branchGrid._addColumn(_g.d.sync_branch_list._branch_code, 1, 10, 15);
            this._branchGrid._addColumn(_g.d.sync_branch_list._branch_name, 1, 15, 15);
            this._branchGrid._addColumn(_g.d.sync_branch_list._access_code, 1, 10, 15);
            this._branchGrid._addColumn(_g.d.sync_branch_list._branch_computer_name, 1, 30, 30);
            this._branchGrid._addColumn(_g.d.sync_branch_list._wait_time, 2, 10, 10);
            this._branchGrid._addColumn(_g.d.sync_branch_list._active, 11, 10, 10);
            this._branchGrid._calcPersentWidthToScatter();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select * from " + _g.d.sync_branch_list._table + " order by " + _g.d.sync_branch_list._branch_code).Tables[0];
            this._branchGrid._loadFromDataTable(__getData);
            //
            this._closeButton.Click += (s1, e1) =>
            {
                this.Dispose();
            };
            this._saveButton.Click += (s1, e1) =>
            {
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate table " + _g.d.sync_branch_list._table));
                // Save Table
                for (int __row = 0; __row < this._branchGrid._rowData.Count; __row++)
                {
                    string __branchCode = this._branchGrid._cellGet(__row, this._branchGrid._findColumnByName(_g.d.sync_branch_list._branch_code)).ToString().ToLower().Trim();
                    string __branchName = this._branchGrid._cellGet(__row, this._branchGrid._findColumnByName(_g.d.sync_branch_list._branch_name)).ToString().ToLower().Trim();
                    string __computerName = this._branchGrid._cellGet(__row, this._branchGrid._findColumnByName(_g.d.sync_branch_list._branch_computer_name)).ToString().ToLower().Trim();
                    string __accessCode = this._branchGrid._cellGet(__row, this._branchGrid._findColumnByName(_g.d.sync_branch_list._access_code)).ToString().ToLower().Trim();
                    int __select = (int)MyLib._myGlobal._decimalPhase(this._branchGrid._cellGet(__row, this._branchGrid._findColumnByName(_g.d.sync_branch_list._active)).ToString());
                    int __waitTime = (int)MyLib._myGlobal._decimalPhase(this._branchGrid._cellGet(__row, this._branchGrid._findColumnByName(_g.d.sync_branch_list._wait_time)).ToString());
                    if (__branchCode.Length > 0 && __computerName.Length > 0 && __accessCode.Length > 0)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.sync_branch_list._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.sync_branch_list._branch_code, _g.d.sync_branch_list._branch_name, _g.d.sync_branch_list._branch_computer_name, _g.d.sync_branch_list._access_code, _g.d.sync_branch_list._active, _g.d.sync_branch_list._wait_time) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + __branchCode + "\'", "\'" + __branchName + "\'", "\'" + __computerName + "\'", "\'" + __accessCode + "\'", __select.ToString(), __waitTime.ToString()) + ")"));
                    }
                }
                __query.Append("</node>");
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("error : " + __result);
                }
            };
        }
    }
}
