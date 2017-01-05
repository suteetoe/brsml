using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPASSET._controls
{
    public partial class _saleGrid : UserControl
    {
        MyLib._searchDataFull _searchAsset;

        struct _columnType
        {
            public int _width;
            public bool _visible;
        }

        public _saleGrid()
        {
            ArrayList __columnWidth = new ArrayList();
            // Asset
            _columnType _column = new _columnType();
            _column._width = 15;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Asset Value
            _column = new _columnType();
            _column._width = 10;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Depreciate Value
            _column = new _columnType();
            _column._width = 10;
            _column._visible = true;
            __columnWidth.Add(_column);
            // After
            _column = new _columnType();
            _column._width = 10;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Price
            _column = new _columnType();
            _column._width = 10;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Tax
            _column = new _columnType();
            _column._width = 10;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Net Amount
            _column = new _columnType();
            _column._width = 10;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Remark
            _column = new _columnType();
            _column._width = 25;
            _column._visible = true;
            __columnWidth.Add(_column);
            // คำนวณความกว้าง
            int __sumPersent = 0;
            for (int __loop = 0; __loop < __columnWidth.Count; __loop++)
            {
                _columnType __getColumn = (_columnType)__columnWidth[__loop];
                if (__getColumn._visible)
                {
                    __sumPersent += __getColumn._width;
                }
            }
            for (int __loop = 0; __loop < __columnWidth.Count; __loop++)
            {
                _columnType __getColumn = (_columnType)__columnWidth[__loop];
                if (__getColumn._visible)
                {
                    __getColumn._width = (__getColumn._width * 100) / __sumPersent;
                }
            }
            //
            InitializeComponent();
            //
            this._myGrid1._table_name = _g.d.as_asset_sale_detail._table;
            this._myGrid1._width_by_persent = true;
            this._myGrid1._total_show = true;
            this._myGrid1._displayRowNumber = false;
            this._myGrid1._rowNumberWork = true;
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._as_code, 1, 25, ((_columnType)__columnWidth[0])._width, true, false, true, true);
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._as_value, 3, 0, ((_columnType)__columnWidth[1])._width, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._depreciate_value, 3, 0, ((_columnType)__columnWidth[2])._width, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._after_depreciate, 3, 0, ((_columnType)__columnWidth[3])._width, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._sale_price, 3, 0, ((_columnType)__columnWidth[4])._width, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._vat_value, 3, 0, ((_columnType)__columnWidth[5])._width, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._net_value, 3, 0, ((_columnType)__columnWidth[6])._width, true, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn(_g.d.as_asset_sale_detail._remark, 1, 255, ((_columnType)__columnWidth[7])._width, true, false, true, false);
            this._myGrid1._addColumn(this._myGrid1._rowNumberName, 2, 0, 15, false, true, true);
            // Event
            this._myGrid1._clickSearchButton += new MyLib.SearchEventHandler(_myGrid1__clickSearchButton);
            this._myGrid1._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_myGrid1__alterCellUpdate);
            this._myGrid1._totalCheck += new MyLib.TotalCheckEventHandler(_myGrid1__totalCheck);
            this._myGrid1._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_myGrid1__queryForInsertCheck);
            this._myGrid1._queryForInsertPerRow += new MyLib.QueryForInsertPerRowEventHandler(_myGrid1__queryForInsertPerRow);
            this._myGrid1._moveNextColumn += new MyLib.MoveNextColumnEventHandler(_myGrid1__moveNextColumn);
        }

        MyLib.QueryForInsertPerRowType _myGrid1__queryForInsertPerRow(MyLib._myGrid sender, int row)
        {
            MyLib.QueryForInsertPerRowType result = new MyLib.QueryForInsertPerRowType();
            result._field = "line_number";
            result._data = row.ToString();
            return (result);
        }

        MyLib._myGridMoveColumnType _myGrid1__moveNextColumn(MyLib._myGrid sender, int newRow, int newColumn)
        {
            MyLib._myGridMoveColumnType __result = new MyLib._myGridMoveColumnType();
            if (this._myGrid1._cellGet(this._myGrid1._selectRow, _g.d.as_asset_sale_detail._as_code).ToString().Length == 0)
            {
                newColumn = this._myGrid1._findColumnByName(_g.d.as_asset_sale_detail._as_code);
                MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึกรหัสสินทรัพย์ก่อน" : "Please input asset code");
            }
            __result._newColumn = newColumn;
            __result._newRow = newRow;
            return __result;
        }

        bool _myGrid1__totalCheck(object sender, int row, int column)
        {
            bool result = true;
            if (((string)this._myGrid1._cellGet(row, 0)).ToString().Length == 0)
            {
                result = false;
            }
            return (result);
        }

        bool _myGrid1__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._myGrid1._cellGet(row, _g.d.as_asset_sale_detail._as_code)).Length == 0) ? false : true);
        }

        void _myGrid1__alterCellUpdate(object sender, int row, int column)
        {
            if (column == 0)
            {
                _searchAssetCode(row);
            }
        }
        void searchAsset()
        {
            _searchAsset = new MyLib._searchDataFull();
            _searchAsset._name = _g.g._search_screen_as;
            _searchAsset._dataList._loadViewFormat(_searchAsset._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchAsset.WindowState = FormWindowState.Maximized;
            _searchAsset._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchAsset._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchAsset__searchEnterKeyPress);
        }

        void _searchAsset__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchAsset.Close();
            this._myGrid1._cellUpdate(this._myGrid1._selectRow, _g.d.as_asset_sale_detail._as_code, sender._cellGet(sender._selectRow, _g.d.as_asset._table + "." + _g.d.as_asset._code).ToString(), true);
            SendKeys.Send("{TAB}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_as) == 0)
            {
                _searchAsset.Close();
                this._myGrid1._cellUpdate(this._myGrid1._selectRow, _g.d.as_asset_sale_detail._as_code, e._text, true);
            }
        }
        void _searchAssetCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string getAsset = this._myGrid1._cellGet(row, _g.d.as_asset_sale_detail._as_code).ToString();
            string query = "select " + _g.d.as_asset._name_1 + " from " + _g.d.as_asset._table + " where " + _g.d.as_asset._code + "=\'" + getAsset + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count == 0)
                {
                    if (getAsset.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this._myGrid1._cellUpdate(row, _g.d.as_asset_sale_detail._as_code, "", false);
                }
            }
            catch
            {
            }
        }

        void _myGrid1__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_g.d.as_asset_sale_detail._as_code) == 0)
            {
                if (_searchAsset == null)
                {
                    searchAsset();
                }
                _searchAsset.Text = e._columnName;
                _searchAsset.ShowDialog();
            }
        }
    }
}
