using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLActivesync
{
    public partial class _syncStatusControl : UserControl
    {
        private string _gridSelect = "Select";
        private string _branchCode = "";
        private string _branchName = "";

        public _syncStatusControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip3.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
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
            this._branchGrid._mouseClick += new MyLib.MouseClickHandler(_branchGrid__mouseClick);
            /*
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
                this._branchLabel.Text = this._branchCode + "/" + this._branchName;
            DataTable __getData2 = __myFrameWork._queryShort("select *, (select count(*) as xcount from sync_send_data where sync_send_data.table_name = " + _g.d.sync_table_list._table + "." + _g.d.sync_table_list._table_name + " and sync_send_data.branch_code =\'" + this._branchCode + "\' ) as " + _g.d.sync_table_list._sync_count + " from " + _g.d.sync_table_list._table + " where " + _g.d.sync_table_list._trans_type + "=1 order by " + _g.d.sync_table_list._table_name).Tables[0];
            this._tableGrid._loadFromDataTable(__getData2);
            };
            //
             * */

            this._tableGrid._table_name = _g.d.sync_table_list._table;
            this._tableGrid._addColumn(this._gridSelect, 11, 5, 5);
            this._tableGrid._addColumn(_g.d.sync_table_list._table_name, 1, 75, 65);
            this._tableGrid._addColumn(_g.d.sync_table_list._sync_send_count, 2, 20, 15);
            this._tableGrid._addColumn(_g.d.sync_table_list._sync_receive_count, 2, 20, 15);
            this._tableGrid._isEdit = false;
            this._tableGrid.Invalidate();

            this._logsGrid._table_name = _g.d.sync_log._table;
            this._logsGrid._addColumn("roworder", 2, 20, 20, true, true);
            this._logsGrid._addColumn(_g.d.sync_log._date_time, 1, 20, 20);
            this._logsGrid._addColumn(_g.d.sync_log._message, 1, 75, 75);
            this._logsGrid._addColumn("S", 12, 1, 5, true, false, false);
            this._logsGrid._mouseClickClip += new MyLib.ClipMouseClickHandler(_logsGrid__mouseClickClip);
            this._logsGrid._isEdit = false;

            /*
            **/
        }

        void _logsGrid__mouseClickClip(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals("S"))
            {
                try
                {
                    string __roworder = this._logsGrid._cellGet(_logsGrid._selectRow, 0).ToString();

                    // get message from roworder
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataSet __result = __myFrameWork._queryShort("select " + _g.d.sync_log._message + " from " + _g.d.sync_log._table + " where roworder = " + __roworder);
                    if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show(__result.Tables[0].Rows[0][0].ToString());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void _branchGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            try
            {
                this._branchCode = this._branchGrid._cellGet(e._row, _g.d.sync_branch_list._branch_code).ToString();
                this._branchName = this._branchGrid._cellGet(e._row, _g.d.sync_branch_list._branch_name).ToString();
            }
            catch
            {
                this._branchCode = "";
                this._branchName = "";
            }
            this._branchLabel.Text = this._branchCode + "/" + this._branchName;


            _loadSyndStatus();
        }

        void _loadSyndStatus()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            if (this._branchCode.Length > 0)
            {
                string __querySyncStatus = "select " + _g.d.sync_table_list._table_name + ", (select count(*) as xcount from sync_send_data where sync_send_data.table_name = " + _g.d.sync_table_list._table + "." + _g.d.sync_table_list._table_name + " and sync_send_data.branch_code =\'" + this._branchCode + "\' ) as " + _g.d.sync_table_list._sync_send_count + ", ( select coalesce(" + _g.d.sync_receive_data._sync_count + ", 0) from " + _g.d.sync_receive_data._table + " where " + _g.d.sync_receive_data._table + "." + _g.d.sync_receive_data._table_name + " = " + _g.d.sync_table_list._table + "." + _g.d.sync_table_list._table_name + " and " + _g.d.sync_receive_data._table + "." + _g.d.sync_receive_data._branch_code + " =\'" + this._branchCode + "\') as " + _g.d.sync_table_list._sync_receive_count + " from " + _g.d.sync_table_list._table + " order by " + _g.d.sync_table_list._table_name;
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__querySyncStatus));

                if (__myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.MicrosoftSQL2000 || __myFrameWork._databaseSelectType == MyLib._myGlobal._databaseType.MicrosoftSQL2005)
                {
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select roworder," + _g.d.sync_log._date_time + ",substring(" + _g.d.sync_log._message + ", 0, 100) as " + _g.d.sync_log._message + " from " + _g.d.sync_log._table + " where " + _g.d.sync_log._branch_code + " =\'" + this._branchCode + "\' order by " + _g.d.sync_log._date_time + " desc"));
                }
                else
                {
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select roworder," + _g.d.sync_log._date_time + ",substr(" + _g.d.sync_log._message + ", 0, 100) as " + _g.d.sync_log._message + " from " + _g.d.sync_log._table + " where " + _g.d.sync_log._branch_code + " =\'" + this._branchCode + "\' order by " + _g.d.sync_log._date_time + " desc"));
                }
                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                if (__result.Count > 0)
                {
                    DataTable __getData2 = ((DataSet)__result[0]).Tables[0];
                    DataTable __getData3 = ((DataSet)__result[1]).Tables[0];

                    this._tableGrid._loadFromDataTable(__getData2);
                    this._logsGrid._loadFromDataTable(__getData3);
                }

            }

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _refreshButton_Click(object sender, EventArgs e)
        {
            this._loadSyndStatus();
        }

        private void _clearLogButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm To Clear Log : " + this._branchCode + "(" + this._branchName + ")", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " + _g.d.sync_log._table + " where " + _g.d.sync_log._branch_code + " =\'" + this._branchCode + "\' ");

                if (__result.Length > 0)
                {
                    MessageBox.Show(__result, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Delete Log " + this._branchCode + "(" + this._branchName + ") Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this._loadSyndStatus();
                }

            }
        }

        private void _clearSendDataButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยกเลิกการส่งข้อมูล : " + this._branchCode + "(" + this._branchName + ")", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                for (int __row = 0; __row < this._tableGrid._rowData.Count; __row++)
                {
                    int __select = (int)MyLib._myGlobal._decimalPhase(this._tableGrid._cellGet(__row, this._tableGrid._findColumnByName(this._gridSelect)).ToString());
                    string __tableName = this._tableGrid._cellGet(__row, _g.d.sync_table_list._table_name).ToString();

                    if (__select == 1)
                    {
                        StringBuilder __query = new StringBuilder();
                        __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from sync_send_data where branch_code = \'" + this._branchCode + "\' and table_name = \'" + __tableName + "\' "));
                        __query.Append("</node>");

                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                        if (__result.Length > 0)
                        {
                            MessageBox.Show(__tableName + ":" + __result, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                this._loadSyndStatus();

            }
        }
    }
}
