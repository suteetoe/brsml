using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public class _gridICSpecialSearchWord : MyLib._myGrid
    {
        MyLib._searchDataFull _search;
        MyLib._searchDataFull _searchGroup;

        public _gridICSpecialSearchWord()
        {
            this._width_by_persent = true;
            this._table_name = _g.d.ic_specific_search._table;
            this._addColumn(_g.d.ic_specific_search._ic_code, 1, 25, 20, true, false, true, true);
            this._addColumn(_g.d.ic_specific_search._ic_name, 1, 25, 40, false, false, false);
            this._addColumn(_g.d.ic_specific_search._group_code, 1, 25, 20, false, false, false, false);
            this._addColumn(_g.d.ic_specific_search._sort_order, 2, 25, 20, true, false, true, false);
            this._calcPersentWidthToScatter();

            this._clickSearchButton += _gridICSpecialSearchWord__clickSearchButton;
            this._queryForInsertCheck += _gridICSpecialSearchWord__queryForInsertCheck;
            this._alterCellUpdate += _gridICSpecialSearchWord__alterCellUpdate;
        }

        private void _gridICSpecialSearchWord__alterCellUpdate(object sender, int row, int column)
        {
            if (column == this._findColumnByName(_g.d.ic_specific_search._ic_code))
            {

                string __getItemCode = this._cellGet(row, _g.d.ic_specific_search._ic_code).ToString();
                // check dup
                if (__getItemCode.Length > 0)
                {
                    int __addr = this._findData(this._findColumnByName(_g.d.ic_specific_search._ic_code), __getItemCode);
                    if (__addr != -1 && __addr != row)
                    {
                        MessageBox.Show(__getItemCode + " ซ้ำ");
                        this._cellUpdate(row, _g.d.ic_specific_search._ic_code, "", true);
                        this._cellUpdate(row, _g.d.ic_specific_search._ic_name, "", false);
                        this._cellUpdate(row, _g.d.ic_specific_search._group_code, "", false);
                    }
                    else
                    {
                        // find name group
                        string __query = "select name_1, " + _g.d.ic_inventory._group_main + ", (select name_1 from ic_group where ic_group.code = ic_inventory.code) as group_name from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __getItemCode + "\'";
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        DataTable __result = __myFrameWork._queryShort(__query).Tables[0];
                        if (__result.Rows.Count > 0)
                        {
                            this._cellUpdate(row, _g.d.ic_specific_search._ic_name, __result.Rows[0][_g.d.ic_inventory._name_1].ToString(), true);
                            this._cellUpdate(row, _g.d.ic_specific_search._group_code, __result.Rows[0]["group_name"].ToString(), true);
                        }
                        else
                        {
                            this._cellUpdate(row, _g.d.ic_specific_search._ic_code, "", true);
                            this._cellUpdate(row, _g.d.ic_specific_search._ic_name, "", false);
                            this._cellUpdate(row, _g.d.ic_specific_search._group_code, "", false);
                        }
                    }
                }
                else
                {
                    this._cellUpdate(row, _g.d.ic_specific_search._ic_code, "", true);
                    this._cellUpdate(row, _g.d.ic_specific_search._ic_name, "", false);
                    this._cellUpdate(row, _g.d.ic_specific_search._group_code, "", false);
                }
            }
        }

        private bool _gridICSpecialSearchWord__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (this._cellGet(row, _g.d.ic_specific_search._ic_code).ToString().Length == 0)
                return false;

            return true;
        }

        private void _gridICSpecialSearchWord__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (_search == null)
            {
                _search = new MyLib._searchDataFull();
                _search.Text = MyLib._myGlobal._resource("ค้นหาสินค้า");
                _search._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
                _search.StartPosition = FormStartPosition.CenterScreen;
                _search._dataList._gridData._mouseClick += (s2, e2) =>
                {
                    object __itemCode = _search._dataList._gridData._cellGet(_search._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                    object __itemName = _search._dataList._gridData._cellGet(_search._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1);

                    this._cellUpdate(this._selectRow, _g.d.ic_specific_search._ic_code, __itemCode.ToString(), true);
                    //this._cellUpdate(this._selectRow, _g.d.ic_specific_search._ic_name, __itemName.ToString(), true);
                    _search.Close();
                    SendKeys.Send("{TAB}");

                };

                _search._searchEnterKeyPress += (s2, e2) =>
                {
                    object __itemCode = _search._dataList._gridData._cellGet(_search._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                    object __itemName = _search._dataList._gridData._cellGet(_search._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1);

                    this._cellUpdate(this._selectRow, _g.d.ic_specific_search._ic_code, __itemCode.ToString(), true);
                    //this._cellUpdate(this._selectRow, _g.d.ic_specific_search._ic_name, __itemName.ToString(), true);
                    _search.Close();
                    SendKeys.Send("{TAB}");

                };
            }

            // extrewhere
            // pack ic_code
            StringBuilder __exceptItemCode = new StringBuilder();
            for (int __row = 0; __row < this._rowData.Count; __row++)
            {
                if (__exceptItemCode.Length > 0)
                    __exceptItemCode.Append(",");

                __exceptItemCode.Append("\'" + this._cellGet(__row, _g.d.ic_specific_search._ic_code).ToString() + "\'");
            }

            _search._dataList._extraWhere = " ic_inventory.code not in (" + __exceptItemCode.ToString() + ")";
            _search._dataList._refreshData();


            _search.ShowDialog();
        }

        private void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
