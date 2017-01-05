using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;

namespace SMLERPReport.ar
{
    public partial class _grid_condition_ar_new : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();

        public _grid_condition_ar_new()
        {
            this._clickSearchButton += new MyLib.SearchEventHandler(_grid_condition_ar_new__clickSearchButton);
        }

        void _grid_condition_ar_new__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string _searchName = this._cellGet(this._selectRow, this._selectColumn).ToString();
            string _search_text_new = _g.g._search_screen_ar;
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._refreshData();
                this._search_data_full._dataList._loadViewData(0);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, "ค้นหาลูกหนี้", this._search_data_full, true);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_ar) == 0)
            {
                this._search_data_full.Close();
                this._cellUpdate(this._selectRow, this._selectColumn, e._text, false);
            }
        }

        public void _setFromToColumn(string __from_column_name, string __to_column_name)
        {
            this._clear();
            this._addColumn(_g.d.resource_report._table + "." + __from_column_name, 1, 1, 50, true, false, false, true);
            this._addColumn(_g.d.resource_report._table + "." + __to_column_name, 1, 1, 50, true, false, false, true);
            this._width_by_persent = true;
            this.AllowDrop = true;
            //this._isEdit = false;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
        }

        public DataTable _getCondition()
        {
            if (this._rowCount(0) == 0) return null;
            DataTable __dataTable = new DataTable("FromTo");
            __dataTable.Columns.Add("from");
            __dataTable.Columns.Add("to");
            for (int __row = 0; __row < this._rowCount(0); __row++)
            {
                DataRow __dataRow = __dataTable.NewRow();
                __dataRow[0] = this._cellGet(__row, 0).ToString();
                __dataRow[1] = this._cellGet(__row, 1).ToString();
                __dataTable.Rows.Add(__dataRow);
            }
            return __dataTable;
        }
    }
}