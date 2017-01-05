using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLActivesync
{
    public partial class _reSyncReciveControl : UserControl
    {
        private string _gridSelect = "Select";
        private string _gridTruncate = "Truncate";
        private string _branchCode = "";
        private string _branchName = "";

        public _reSyncReciveControl()
        {
            InitializeComponent();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            //
            this._branchGrid._table_name = _g.d.sync_branch_list._table;
            this._branchGrid._addColumn(_g.d.sync_branch_list._branch_code, 1, 30, 15);
            this._branchGrid._addColumn(_g.d.sync_branch_list._branch_name, 1, 40, 15);
            this._branchGrid._addColumn(_g.d.sync_branch_list._branch_computer_name, 1, 30, 30);
            this._branchGrid._isEdit = false;
            this._branchGrid._calcPersentWidthToScatter();
            DataTable __getData1 = __myFrameWork._queryShort("select * from " + _g.d.sync_branch_list._table + " order by " + _g.d.sync_branch_list._branch_code).Tables[0];
            this._branchGrid._loadFromDataTable(__getData1);
            this._branchGrid._mouseClick += (s1, e1) =>
            {
                //string __branchName = "";
                try
                {
                    this._branchCode = this._branchGrid._cellGet(e1._row, _g.d.sync_branch_list._branch_code).ToString();
                    this._branchName = this._branchGrid._cellGet(e1._row, _g.d.sync_branch_list._branch_name).ToString();
                }
                catch
                {
                    this._branchCode = "";
                    this._branchName = "";
                }
                this._branchSelectLabel.Text = this._branchCode + "/" + this._branchName;
            };

            this._tableGrid._table_name = _g.d.sync_table_list._table;
            this._tableGrid._addColumn(this._gridSelect, 11, 10, 10);
            this._tableGrid._addColumn(_g.d.sync_table_list._table_name, 1, 80, 80);
            this._tableGrid._addColumn(_g.d.sync_table_list._trans_command, 1, 80, 80, true, true);
            //this._tableGrid._addColumn(this._gridTruncate, 11, 10, 10, true, false);

            this._tableGrid._isEdit = false;
            this._tableGrid._calcPersentWidthToScatter();
            this._tableGrid.Invalidate();
            DataTable __getData2 = __myFrameWork._queryShort("select * from " + _g.d.sync_table_list._table + " where " + _g.d.sync_table_list._trans_type + " in (2, 3) order by " + _g.d.sync_table_list._table_name).Tables[0];
            this._tableGrid._loadFromDataTable(__getData2);

        }

        private void _updateTableSelect(int value)
        {
            for (int __row = 0; __row < this._tableGrid._rowData.Count; __row++)
            {
                this._tableGrid._cellUpdate(__row, this._gridSelect, value, false);
            }
            this._tableGrid.Invalidate();
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            _updateTableSelect(1);
        }

        private void _removeAllButton_Click(object sender, EventArgs e)
        {
            _updateTableSelect(0);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            if (this._branchCode.Length == 0)
            {
                MessageBox.Show("Select Branch Code to Resync");
            }
            else
            {
                DialogResult __dl = MessageBox.Show("Reset Receive From " + this._branchCode + "(" + this._branchName + ")", "Ask", MessageBoxButtons.YesNo);
                if (__dl == DialogResult.Yes)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    for (int __row = 0; __row < this._tableGrid._rowData.Count; __row++)
                    {
                        int __select = (int)MyLib._myGlobal._decimalPhase(this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(this._gridSelect)).ToString());
                        if (__select == 1)
                        {
                            //int __tuncate = (int)MyLib._myGlobal._decimalPhase(this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(this._gridTruncate)).ToString());

                            StringBuilder __query = new StringBuilder();
                            __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                            string __tableName = this._tableGrid._cellGet(__row, _g.d.sync_table_list._table_name).ToString();
                            string __transCommand = this._tableGrid._cellGet(__row, _g.d.sync_table_list._trans_command).ToString().ToLower();

                            string __extraWhereCommand = "";
                            if (__transCommand.Trim().Length > 0)
                            {
                                __transCommand = __transCommand.Replace("&table_sync&", __tableName).Replace("&branch_code&", "\'" + this._branchCode + "\'");
                                __extraWhereCommand = " where " + __transCommand;
                            }

                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from sync_reset_receive_data where branch_code = \'" + this._branchCode + "\' and table_name =\'" + __tableName + "\' "));
                            //if (__tuncate == 1)
                            //{
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_reset_receive_data (branch_code,table_name) values (\'" + this._branchCode + "\',\'" + __tableName + "\' )"));
                            //}
                            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from sync_send_data where branch_code=\'" + this._branchCode + "\' and table_name=\'" + __tableName + "\' "));
                            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_send_data (branch_code,table_name,command_mode,guid) (select \'" + this._branchCode + "\',\'" + __tableName + "\',1,guid from " + __tableName + " " + __extraWhereCommand + " )"));
                            __query.Append("</node>");
                            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                            if (__result.Length > 0)
                            {
                                MessageBox.Show(__tableName + ":" + __result, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    MessageBox.Show("Reset Receive From " + this._branchCode + "(" + this._branchName + ") Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
