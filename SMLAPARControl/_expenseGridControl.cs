using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public class _expenseGridControl : MyLib._myGrid
    {
        public delegate void _refreshDataEvent(_expenseGridControl sender);
        public event _refreshDataEvent _refreshData;

        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());

        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        string _searchName = "";
        string _search_data_full_name = "";
        public _expenseGridControl()
        {
            this._table_name = _g.d.cb_trans_detail._table;

            this._addColumn(_g.d.cb_trans_detail._trans_number, 1, 1, 14, true, false, true, true, "", "", "", _g.d.cb_trans_detail._expense_code);
            this._addColumn(_g.d.cb_trans_detail._description, 1, 1, 15, true, false, false);
            this._addColumn(_g.d.cb_trans_detail._amount, 3, 1, 14, true, false, true, false, __formatNumber);
            this._addColumn(_g.d.cb_trans_detail._remark, 1, 1, 15, true, false, true);

            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;
            this._total_show = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            this._totalCheck += _expenseGridControl__totalCheck;
            this._clickSearchButton += _expenseGridControl__clickSearchButton;
            this._alterCellUpdate += _expenseGridControl__alterCellUpdate;
            this._afterRemoveRow += _expenseGridControl__afterRemoveRow;

            this._calcPersentWidthToScatter();

        }

        void _expenseGridControl__afterRemoveRow(object sender)
        {
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        void _expenseGridControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string __queryWhere = "";
            if (e._columnName.Equals(_g.d.cb_trans_detail._trans_number))
            {
                _search_data_full_name = _g.g._search_screen_expenses_list;
                __queryWhere = "";
                //
                Boolean __found = false;
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (this._search_data_full_buffer_name[__loop].ToString().Equals(_search_data_full_name.ToLower()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(_search_data_full_name.ToLower());
                    this._search_data_full_buffer.Add((MyLib._searchDataFull)new MyLib._searchDataFull());
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;

                if (!this._search_data_full_pointer._name.Equals(_search_data_full_name.ToLower()))
                {
                    this._search_data_full_pointer.Text = e._columnName;
                    this._search_data_full_pointer._name = _search_data_full_name;
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                    this._search_data_full_pointer._dataList._loadViewFormat(_search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, this._search_data_full_pointer, false, true, __queryWhere);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __result = "";
            this._search_data_full_pointer.Close();
            if (this._search_data_full_pointer._name.Equals(_g.g._search_screen_expenses_list))
            {
                __result = this._search_data_full_pointer._dataList._gridData._cellGet(e._row, _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._trans_number, __result, true);
                SendKeys.Send("{ENTER}");
            }
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
            SendKeys.Send("{ENTER}");
        }
        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;


            if (__getParent2._name.Equals(_g.g._search_screen_expenses_list))
            {
                string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, _g.d.erp_expenses_list._table + "." + _g.d.erp_expenses_list._code);
                if (result.Length > 0)
                {
                    this._search_data_full_pointer.Close();
                    this._cellUpdate(this._selectRow, _g.d.cb_trans_detail._trans_number, result, true);
                    _search_detailRow(this._selectRow);
                }
            }
        }


        void _expenseGridControl__alterCellUpdate(object sender, int row, int column)
        {
            _search_detailRow(row);
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }

        bool _expenseGridControl__totalCheck(object sender, int row, int column)
        {
            bool __result = true;
            int __columnNumber = this._findColumnByName(_g.d.cb_trans_detail._trans_number);
            if (this._cellGet(row, __columnNumber).ToString().Trim().Length == 0)
            {
                __result = false;
            }
            return __result;

        }
        void _search_detailRow(int row)
        {
            string __code = this._cellGet(row, _g.d.cb_trans_detail._trans_number).ToString();
            string __query = "select " + _g.d.erp_expenses_list._name_1 + " from " + _g.d.erp_expenses_list._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_expenses_list._code) + "=\'" + __code.ToUpper() + "\' ";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
            if (__getData.Tables[0].Rows.Count == 0)
            {
                this._cellUpdate(row, _g.d.cb_trans_detail._trans_number, "", false);
                this._cellUpdate(row, _g.d.cb_trans_detail._description, "", false);
            }
            else
            {
                this._cellUpdate(row, _g.d.cb_trans_detail._description, __getData.Tables[0].Rows[0][_g.d.erp_expenses_list._name_1].ToString(), false);
            }
        }

    }
}
