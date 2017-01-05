using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icmainGridControl : UserControl
    {
        public _icmainGridControl()
        {
            InitializeComponent();
        }
    }

    public class _icmainGridUnitOpposiControl : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;
        public _icmainGridUnitOpposiControl()
        {
            _build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._table_name = _g.d.ic_opposite_unit._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._addColumn(_g.d.ic_opposite_unit._unit_code, 1, 10, 20, true, false, true, true);
            this._addColumn(_g.d.ic_opposite_unit._unit_name, 1, 0, 35, false, false, true, false);
            this._addColumn(_g.d.ic_opposite_unit._stand_value, 3, 0, 20, true, false, true, true, __formatNumber);
            this._addColumn(_g.d.ic_opposite_unit._divide_value, 3, 0, 20, true, false, true, true, __formatNumber);
            this._addColumn(_g.d.ic_opposite_unit._status, 11, 5, 5);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridUnitOpposiControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridUnitOpposiControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridUnitOpposiControl__queryForInsertCheck);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridUnitOpposiControl__afterAddRow);
        }

        void _icmainGridUnitOpposiControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_opposite_unit._status, 1, false);
        }

        bool _icmainGridUnitOpposiControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_opposite_unit._unit_code)).Length == 0) ? false : true);
        }

        void _icmainGridUnitOpposiControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemUnitColumn = this._findColumnByName(_g.d.ic_opposite_unit._unit_code);
            if (__getItemUnitColumn == column)
            {
                _searchUnitRow(row);
            }
        }

        void _icmainGridUnitOpposiControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                if (e._columnName.Equals(_g.d.ic_opposite_unit._unit_code))
                {
                    _searchMasterList = _searchProperties._setColumnSearch(e._columnName, 1, "");
                    this._searchMasterGrid.Text = e._columnName;
                    this._searchMasterGrid._dataList._tableName = _searchMasterList[1].ToString();
                    this._searchMasterGrid._name = _searchMasterList[0].ToString();
                    this._searchMasterGrid._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._dataList._loadViewFormat(_searchMasterGrid._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterGrid._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterGrid._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _searchMasterGrid);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterGrid__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            //_searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            string __fieldName = _searchMasterGrid.Text;
            if (name.CompareTo(_g.g._search_master_ic_unit) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_unit._table + "." + _g.d.ic_unit._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        void _searchUnitRow(int row)
        {
            // ค้นหารายการหน่วยนับ
            string __unitCode = this._cellGet(row, _g.d.ic_opposite_unit._unit_code).ToString();
            string __query = "select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + __unitCode.ToUpper() + "\'";
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            string __unitName = "";
            if (__getData.Tables.Count > 0)
            {
                int __indexName1 = __getData.Tables[0].Columns.IndexOf(_g.d.ic_unit._name_1);
                __unitName = __getData.Tables[0].Rows[0].ItemArray.GetValue(__indexName1).ToString();
            }
            this._cellUpdate(row, _g.d.ic_opposite_unit._unit_name, __unitName, false);
        }
    }

    public class _icmainGridWarehouseLocationControl : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;
        public _icmainGridWarehouseLocationControl()
        {
            _build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._table_name = _g.d.ic_wh_shelf._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._addColumn(_g.d.ic_wh_shelf._wh_code, 1, 10, 10, true, false, true, true);
            this._addColumn(_g.d.ic_wh_shelf._wh_name, 1, 10, 10, false, false, false, false);
            this._addColumn(_g.d.ic_wh_shelf._shelf_code, 1, 10, 10, true, false, true, true);
            this._addColumn(_g.d.ic_wh_shelf._shelf_name, 1, 10, 10, false, false, false, false);
            this._addColumn(_g.d.ic_wh_shelf._shelf_list, 1, 30, 35, true, false, true, false);
            this._addColumn(_g.d.ic_wh_shelf._min_point, 3, 10, 10, true, false, true, true, __formatNumber);
            this._addColumn(_g.d.ic_wh_shelf._max_point, 3, 10, 10, true, false, true, true, __formatNumber);
            this._addColumn(_g.d.ic_wh_shelf._status, 11, 5, 5, true, false, true, false);
            // ซ่อน
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            this._addColumn(this._temp1, 1, 5, 5, false, true, false, false);
            this._total_show = false;
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridBranchControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridBranchControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridBranchControl__queryForInsertCheck);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridBranchControl__afterAddRow);
        }

        public string _createQueryForLoad(string itemCode, string extraWhere)
        {
            return "select *,(select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where "
                    + _g.d.ic_warehouse._code + "=" + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._wh_code + ")" + " as " + _g.d.ic_wh_shelf._wh_name
                    + ",(select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where "
                    + _g.d.ic_shelf._whcode + "=" + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._wh_code + " and "
                    + _g.d.ic_shelf._code + "=" + _g.d.ic_wh_shelf._table + "." + _g.d.ic_wh_shelf._shelf_code + ") as " + _g.d.ic_wh_shelf._shelf_name + ","
                    + _g.d.ic_wh_shelf._wh_code + "||'~'||" + _g.d.ic_wh_shelf._shelf_code + " as " + this._temp1
                    + " from " + _g.d.ic_wh_shelf._table + " where " + _g.d.ic_wh_shelf._ic_code + "=\'" + itemCode + "\' " +
                    ((extraWhere.Length > 0) ? " and " + extraWhere : "") +
                    " order by " + _g.d.ic_wh_shelf._wh_code + "," + _g.d.ic_wh_shelf._shelf_code;
        }

        public string _createQueryForLoad(string itemCode)
        {
            return _createQueryForLoad(itemCode, "");
        }

        void _icmainGridBranchControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_wh_shelf._status, 1, false);
        }

        bool _icmainGridBranchControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_wh_shelf._wh_code)).Length == 0) ? false : true);
        }

        void _icmainGridBranchControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getWHColumn = this._findColumnByName(_g.d.ic_wh_shelf._wh_code);
            int __getSHColumn = this._findColumnByName(_g.d.ic_wh_shelf._shelf_code);
            if (__getWHColumn == column)
            {
                _searchWHCode(row);
            }
            if (__getSHColumn == column)
            {
                _searchShelfCode(row);
            }
            string __wareHouseCode = this._cellGet(row, __getWHColumn).ToString();
            string __shelfCode = this._cellGet(row, __getSHColumn).ToString();
            this._cellUpdate(row, this._temp1, __wareHouseCode + "~" + __shelfCode, false);
        }

        void _icmainGridBranchControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                _searchMasterList = _searchProperties._setColumnSearch(e._columnName, 1, "");
                if (!_searchMasterGrid._name.Equals(e._columnName))
                {
                    this._searchMasterGrid.Text = e._columnName;
                    this._searchMasterGrid._dataList._tableName = _searchMasterList[1].ToString();
                    this._searchMasterGrid._name = _searchMasterList[0].ToString();
                    this._searchMasterGrid._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._dataList._loadViewFormat(_searchMasterGrid._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterGrid._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterGrid._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    if (e._columnName.Equals(_g.d.ic_wh_shelf._shelf_code))
                    {
                        this._searchMasterGrid._dataList._extraWhere = _g.d.ic_shelf._whcode + "=\'" + this._cellGet(e._row, _g.d.ic_wh_shelf._wh_code).ToString() + "\'";
                    }
                    this._searchMasterGrid._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _searchMasterGrid);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterGrid__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            _searchAll(name, e._row);
        }

        private void _searchAll(string name, int row)
        {
            if (name.CompareTo(_g.g._search_master_ic_warehouse) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(row, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, _g.d.ic_wh_shelf._wh_code, __result, true);
                    _searchWHCode(this._selectRow);
                }
            }
            if (name.CompareTo(_g.g._search_master_ic_shelf) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(row, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, _g.d.ic_wh_shelf._shelf_code, __result, true);
                    _searchShelfCode(this._selectRow);
                }
            }
        }

        private void _searchWHCode(int row)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __getMaintainCode = this._cellGet(row, _g.d.ic_wh_shelf._wh_code).ToString();
            string __query = "select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._code + "=\'" + __getMaintainCode + "\'";
            try
            {
                DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                if (__dataResult.Tables[0].Rows.Count > 0)
                {
                    string __getData = __dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_wh_shelf._wh_name, __getData, false);
                }
                else
                {
                    if (__getMaintainCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getMaintainCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this._cellUpdate(row, _g.d.ic_wh_shelf._wh_code, "", false);
                    this._cellUpdate(row, _g.d.ic_wh_shelf._wh_name, "", false);
                }
            }
            catch
            {
            }
        }

        bool _checkDuplicateWareHouseAndShelfCode(int rowSource)
        {
            Boolean __result = false;
            string __get_wh_code = this._cellGet(rowSource, _g.d.ic_wh_shelf._wh_code).ToString();
            string __get_shelf_code = this._cellGet(rowSource, _g.d.ic_wh_shelf._shelf_code).ToString();
            if (__get_wh_code.Trim().Length != 0 && __get_shelf_code.Trim().Length != 0)
            {
                string __compareCode = __get_wh_code + __get_shelf_code;
                if (this._cellGet(rowSource, _g.d.ic_wh_shelf._wh_code).ToString().Length == 0)
                {
                    this._cellUpdate(rowSource, _g.d.ic_wh_shelf._shelf_code, "", true);
                    this._cellUpdate(rowSource, _g.d.ic_wh_shelf._shelf_name, "", true);
                }
                for (int __row = 0; __row < this._rowData.Count; __row++)
                {
                    if (__row != rowSource)
                    {
                        string _get_search_wh_code = this._cellGet(__row, _g.d.ic_wh_shelf._wh_code).ToString();
                        string _get_search_shelf_code = this._cellGet(__row, _g.d.ic_wh_shelf._shelf_code).ToString();
                        string _getSearchCode = _get_search_wh_code + _get_search_shelf_code;
                        if (_getSearchCode.ToLower().ToString().Trim() == __compareCode.ToLower().ToString().Trim())
                        {
                            this._cellUpdate(rowSource, _g.d.ic_wh_shelf._shelf_code, "", true);
                            this._cellUpdate(rowSource, _g.d.ic_wh_shelf._shelf_name, "", true);
                            __result = true;
                        }
                    }
                }
            }
            return __result;
        }

        private void _searchShelfCode(int row)
        {
            if (_checkDuplicateWareHouseAndShelfCode(row) == false)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __getWareHouseCode = this._cellGet(row, _g.d.ic_wh_shelf._wh_code).ToString();
                string __getShelfCode = this._cellGet(row, _g.d.ic_wh_shelf._shelf_code).ToString();
                string __query = "select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._whcode + "=\'" + __getWareHouseCode + "\'";
                try
                {
                    DataSet __dataResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    if (__dataResult.Tables[0].Rows.Count > 0)
                    {
                        this._cellUpdate(row, _g.d.ic_wh_shelf._shelf_name, __dataResult.Tables[0].Rows[0][0].ToString(), false);
                    }
                    else
                    {
                        if (__getShelfCode.Length > 0)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("warning124") + " : " + __getShelfCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        this._cellUpdate(row, _g.d.ic_wh_shelf._shelf_code, "", false);
                        this._cellUpdate(row, _g.d.ic_wh_shelf._shelf_name, "", false);
                    }
                }
                catch
                {
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("warning125"), MyLib._myGlobal._resource("เตือน"));
                this._inputCell(row, this._selectColumn);
            }
        }
    }


    public class _icmainGridMarketControl : MyLib._myGrid
    {
        public _icmainGridMarketControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._clear();
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_name_merket._table;
            this._addColumn(_g.d.ic_name_merket._name_1, 1, 0, 95, true, false, true, false);
            this._addColumn(_g.d.ic_name_merket._status, 11, 5, 5);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainGridBillingControl : MyLib._myGrid
    {
        public _icmainGridBillingControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._clear();
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_name_billing._table;
            this._addColumn(_g.d.ic_name_billing._name_1, 1, 0, 95, true, false, true, false);
            this._addColumn(_g.d.ic_name_billing._status, 11, 5, 5);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainGridShortNameControl : MyLib._myGrid
    {
        public _icmainGridShortNameControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._clear();
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_name_short._table;
            this._addColumn(_g.d.ic_name_short._name_1, 1, 0, 95, true, false, true, false);
            this._addColumn(_g.d.ic_name_short._status, 11, 5, 5);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainGridPosNameControl : MyLib._myGrid
    {
        public _icmainGridPosNameControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._clear();
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_name_pos._table;
            this._addColumn(_g.d.ic_name_pos._name_1, 1, 0, 95, true, false, true, false);
            this._addColumn(_g.d.ic_name_pos._status, 11, 5, 5);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public class _icmainGridColorControl : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;
        public _icmainGridColorControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_color_use._table;
            this._addColumn(_g.d.ic_color_use._code, 1, 10, 35, true, false, true, true);
            this._addColumn(_g.d.ic_color_use._name_1, 1, 0, 60, false, false, false, false);
            this._addColumn(_g.d.ic_color_use._status, 11, 5, 5, false, false, true, false);
            this._addColumn(this._rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridColorControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridColorControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridColorControl__queryForInsertCheck);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridColorControl__afterAddRow);
        }

        void _icmainGridColorControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_unit_use._status, 1, false);
        }

        bool _icmainGridColorControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_color_use._code)).Length == 0) ? false : true);
        }

        void _icmainGridColorControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemColorColumn = this._findColumnByName(_g.d.ic_color_use._code);
            if (__getItemColorColumn == column)
            {
                _searchColorRow(row);
            }
        }

        void _icmainGridColorControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {

                _searchMasterList = _searchProperties._setColumnSearch(e._columnName, 0, _g.d.ic_color_use._table);
                if (!_searchMasterGrid._name.Equals(e._columnName))
                {
                    this._searchMasterGrid.Text = e._columnName;
                    this._searchMasterGrid._dataList._tableName = _searchMasterList[1].ToString();
                    this._searchMasterGrid._name = _searchMasterList[0].ToString();
                    this._searchMasterGrid._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._dataList._loadViewFormat(_searchMasterGrid._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterGrid._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterGrid._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _searchMasterGrid);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterGrid__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            //_searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            string __fieldName = _searchMasterGrid.Text;
            if (name.CompareTo(_searchMasterGrid._name) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_color._table + "." + _g.d.ic_color._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        void _searchColorRow(int row)
        {
            string __colorCode = this._cellGet(row, _g.d.ic_color_use._code).ToString();
            string query = "select " + _g.d.ic_color._name_1 + " from " + _g.d.ic_color._table + " where " + _g.d.ic_color._code + "=\'" + __colorCode + "\'";
            try
            {
                DataSet dataResult = _myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_color_use._name_1, getData, false);
                    this._cellUpdate(row, _g.d.ic_color_use._status, 1, false);
                }
                else
                {
                    if (__colorCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __colorCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this._cellUpdate(row, _g.d.ic_color_use._code, "", false);
                    this._cellUpdate(row, _g.d.ic_color_use._status, 0, false);
                }
            }
            catch
            {
            }
        }
    }



    public class _icmainGridPromotionControl : MyLib._myGrid
    {
        // search
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;

        public _icmainGridPromotionControl()
        {
            this.__bluid();
        }

        void __bluid()
        {
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_promotion_detail._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._addColumn(_g.d.ic_promotion_detail._ic_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_promotion_detail._ic_name, 1, 20, 15, false, false, true, false);
            this._addColumn(_g.d.ic_promotion_detail._unit_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_promotion_detail._qty_1, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_promotion_detail._qty_2, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_promotion_detail._promote_price, 3, 20, 15, false, false, true, false);
            this._addColumn(_g.d.ic_promotion_detail._free_ic_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_promotion_detail._free_ic_unit, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_promotion_detail._free_qty, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_promotion_detail._remark, 1, 20, 15, false, false, true, false);
            this._addColumn(_g.d.ic_promotion_detail._status, 11, 5, 5);
            //this._addColumn(_g.d.ic_promotion_detail._row_number, 2, 0, 15, false, true, true);
            this._total_show = false;

            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridPromotionControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridPromotionControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridPromotionControl__queryForInsertCheck);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridPromotionControl__afterAddRow);
        }

        void _icmainGridPromotionControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_promotion._status, 1, false);
        }

        bool _icmainGridPromotionControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_promotion._promote_code)).Length == 0) ? false : true);
        }

        void _icmainGridPromotionControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemColorColumn = this._findColumnByName(_g.d.ic_promotion._promote_code);
            if (__getItemColorColumn == column)
            {
                _searchIcCode(row);
                _searchUnitCode(row);
                _searchFreeIcCode(row);
                _searchFreeUnitCode(row);
            }
        }

        private void _searchUnitCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getUnitCode = this._cellGet(row, _g.d.ic_promotion_detail._unit_code).ToString();
            string query = "select " + _g.d.ic_unit_use._code + " , " + _g.d.ic_unit_use._name_1 +
                " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._code + "=\'" + __getUnitCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_code, __getUnitCode, false);
                    string __testName = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, dataResult.Tables[0].Rows[0][0].ToString() + "~" + dataResult.Tables[0].Rows[0][1].ToString(), false);

                }
                else
                {
                    if (__getUnitCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getUnitCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, "", false);

                }
            }
            catch
            {
            }
        }

        private void _searchFreeUnitCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getFreeUnitCode = this._cellGet(row, _g.d.ic_promotion_detail._free_ic_unit).ToString();
            string query = "select " + _g.d.ic_unit_use._code + " , " + _g.d.ic_unit_use._name_1 +
                " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._code + "=\'" + __getFreeUnitCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_code, __getFreeUnitCode, false);
                    string __testName = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._free_ic_unit, dataResult.Tables[0].Rows[0][0].ToString() + "~" + dataResult.Tables[0].Rows[0][1].ToString(), false);

                }
                else
                {
                    if (__getFreeUnitCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getFreeUnitCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, "", false);

                }
            }
            catch
            {
            }
        }


        private void _searchFreeIcCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getFreeIcCode = this._cellGet(row, _g.d.ic_promotion_detail._free_ic_code).ToString();
            string query = "select " + _g.d.ic_inventory._code + " , " + _g.d.ic_inventory._name_1 +  //_g.d.ic_inventory.+ "," +

                " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __getFreeIcCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][1].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_code, __getFreeIcCode, false);
                    string __testName = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._free_ic_code, dataResult.Tables[0].Rows[0][0].ToString() + "~" + dataResult.Tables[0].Rows[0][1].ToString(), false);

                }
                else
                {
                    if (__getFreeIcCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getFreeIcCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_name, "", false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._remark, "", false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, "", false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._status, 0, false);
                }
            }
            catch
            {
            }
        }

        private void _searchIcCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getIcCode = this._cellGet(row, _g.d.ic_promotion_detail._ic_code).ToString();
            string query = "select " + _g.d.ic_inventory._name_1 + "," + //_g.d.ic_inventory.+ "," +
                _g.d.ic_inventory._remark + " , " + _g.d.ic_inventory._status + " , " +
                _g.d.ic_inventory._unit_standard +
                " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __getIcCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][1].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_code, __getIcCode, false);
                    string __testName = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_name, dataResult.Tables[0].Rows[0][0].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._remark, dataResult.Tables[0].Rows[0][1].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, dataResult.Tables[0].Rows[0][3].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._status, 1, false);
                }
                else
                {
                    if (__getIcCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getIcCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_name, "", false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._remark, "", false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, "", false);
                    this._cellUpdate(row, _g.d.ic_promotion_detail._status, 0, false);
                }
            }
            catch
            {
            }
        }

        void _icmainGridPromotionControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                _searchMasterList = _searchProperties._setColumnSearch(e._columnName, 0, _g.d.ic_inventory._table);
                if (!_searchMasterGrid._name.Equals(e._columnName))
                {
                    this._searchMasterGrid.Text = e._columnName;
                    this._searchMasterGrid._name = _searchMasterList[0].ToString();
                    this._searchMasterGrid._dataList._tableName = _searchMasterList[1].ToString();
                    if (e._columnName.Equals(_g.d.ic_promotion_detail._unit_code))
                    {
                        this._searchMasterGrid._dataList._extraWhere = _g.d.ic_unit_use._ic_code + " = '" + this._cellGet(this._selectRow, 0).ToString() + "'";
                    }
                    if (e._columnName.Equals(_g.d.ic_promotion_detail._free_ic_unit))
                    {
                        this._searchMasterGrid._dataList._extraWhere = _g.d.ic_unit_use._ic_code + " = '" + this._cellGet(this._selectRow, 0).ToString() + "'";
                    }
                    this._searchMasterGrid._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._dataList._loadViewFormat(_searchMasterGrid._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterGrid._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterGrid._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _searchMasterGrid);
            }
            catch (Exception)
            {
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            string __fieldName = _searchMasterGrid.Text;

            if (name.CompareTo(_g.g._search_screen_ic_inventory) == 0)
            {

                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");


                    if (__fieldName.Equals(_g.d.ic_promotion_detail._ic_code))
                    {
                        _searchIcCode(this._selectRow);
                    }
                    else if (__fieldName.Equals(_g.d.ic_promotion_detail._free_ic_code))
                    {
                        _searchFreeIcCode(this._selectRow);
                    }
                }

            }
            else if (name.CompareTo(_g.g._search_master_ic_unit_use) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");
                    if (__fieldName.Equals(_g.d.ic_promotion_detail._unit_code))
                    {
                        _searchUnitCode(this._selectRow);
                    }
                    else if (__fieldName.Equals(_g.d.ic_promotion_detail._free_ic_unit))
                    {
                        _searchFreeUnitCode(this._selectRow);
                    }

                }
            }

        }

        void _searchMasterGrid__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            // _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");

        }
    }

    public class _icmainGridPurchasePriceControl : MyLib._myGrid
    {

        // search
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;

        public _icmainGridPurchasePriceControl()
        {
            this._build();
        }

        void _build()
        {
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_purchase_price_detail._table;

            this._addColumn(_g.d.ic_purchase_price_detail._ic_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_purchase_price_detail._unit_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_purchase_price_detail._credit_code, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_purchase_price_detail._sending_type, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_purchase_price_detail._from_qty, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_purchase_price_detail._to_qty, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_purchase_price_detail._price, 3, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_purchase_price_detail._discount, 3, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_purchase_price_detail._status, 11, 5, 5);
            //this._addColumn(_g.d.ic_purchase_price_detail._row_number, 2, 20, 15, true, false, true, false);
            this._total_show = false;

            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridPurchasePriceControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridPurchasePriceControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridPurchasePriceControl__queryForInsertCheck);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridPurchasePriceControl__afterAddRow);
        }

        void _icmainGridPurchasePriceControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_purchase_price_detail._status, 1, false);
        }

        bool _icmainGridPurchasePriceControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_promotion._promote_code)).Length == 0) ? false : true);
        }

        void _icmainGridPurchasePriceControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemColorColumn = this._findColumnByName(_g.d.ic_promotion._promote_code);
            if (__getItemColorColumn == column)
            {
                _searchPurchasePrice(row);
                _searchUnitCode(row);
            }
        }

        private void _searchUnitCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getUnitCode = this._cellGet(row, _g.d.ic_purchase_price_detail._unit_code).ToString();
            string query = "select " + _g.d.ic_unit_use._code + " , " + _g.d.ic_unit_use._name_1 +
                " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._code + "=\'" + __getUnitCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_code, __getUnitCode, false);
                    string __testName = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, dataResult.Tables[0].Rows[0][0].ToString() + "~" + dataResult.Tables[0].Rows[0][1].ToString(), false);

                }
                else
                {
                    if (__getUnitCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getUnitCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    this._cellUpdate(row, _g.d.ic_promotion_detail._unit_code, "", false);

                }
            }
            catch
            {
            }
        }

        private void _searchPurchasePrice(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getIcCode = this._cellGet(row, _g.d.ic_promotion_detail._ic_code).ToString();
            string query = "select " + _g.d.ic_inventory._unit_standard + "," + //_g.d.ic_inventory.+ "," +
                 _g.d.ic_inventory._status +
                " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __getIcCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][1].ToString();
                    this._cellUpdate(row, _g.d.ic_promotion_detail._ic_code, __getIcCode, false);
                    string __testName = dataResult.Tables[0].Rows[0][0].ToString();
                    this._cellUpdate(row, _g.d.ic_purchase_price_detail._unit_code, dataResult.Tables[0].Rows[0][0].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_purchase_price_detail._status, 1, false);
                }
                else
                {
                    if (__getIcCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getIcCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }


                    this._cellUpdate(row, _g.d.ic_purchase_price_detail._unit_code, "", false);
                    this._cellUpdate(row, _g.d.ic_purchase_price_detail._status, 0, false);
                }
            }
            catch
            {
            }
        }

        void _icmainGridPurchasePriceControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                _searchMasterList = _searchProperties._setColumnSearch(e._columnName, 0, _g.d.ic_inventory._table);
                if (!_searchMasterGrid._name.Equals(e._columnName))
                {
                    this._searchMasterGrid.Text = e._columnName;
                    this._searchMasterGrid._name = _searchMasterList[0].ToString();
                    this._searchMasterGrid._dataList._tableName = _searchMasterList[1].ToString();
                    if (e._columnName.Equals(_g.d.ic_purchase_price_detail._unit_code))
                    {
                        this._searchMasterGrid._dataList._extraWhere = _g.d.ic_unit_use._ic_code + " = '" + this._cellGet(this._selectRow, 0).ToString() + "'";
                    }
                    this._searchMasterGrid._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._dataList._loadViewFormat(_searchMasterGrid._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterGrid._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterGrid._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _searchMasterGrid);
            }
            catch (Exception)
            {
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            string __fieldName = _searchMasterGrid.Text;
            if (name.CompareTo(_g.g._search_screen_ic_inventory) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");
                    _searchPurchasePrice(this._selectRow);
                }
            }
            else if (name.CompareTo(_g.g._search_master_ic_unit_use) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");
                    _searchUnitCode(this._selectRow);

                }
            }
        }

        void _searchMasterGrid__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            // _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");

        }


    }

    public class _icmainGridRelationControl : MyLib._myGrid
    {

        public _icmainGridRelationControl()
        {
            this._build();
        }

        private void _build()
        {
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_relation_detail._table;
            this._addColumn(_g.d.ic_relation_detail._doc_date, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._doc_no, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._ic_code_1, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._qty_1, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._line_number_1, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._doc_ref, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._ic_code_2, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._qty_2, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._line_number_2, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_relation_detail._transection_flag, 2, 20, 15, true, false, true, false);

            //this._addColumn(_g.d.ic_purchase_price_detail._row_number, 2, 20, 15, true, false, true, false);
            this._total_show = false;
        }

    }

    public class _icmainGridStkBuildControl : MyLib._myGrid
    {
        public _icmainGridStkBuildControl()
        {
            this._build();
        }

        void _build()
        {
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_stk_build_detail._table;
            this._addColumn(_g.d.ic_stk_build_detail._doc_date, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_stk_build_detail._doc_no, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_stk_build_detail._ic_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_stk_build_detail._ic_name, 1, 20, 15, true, false, false, false);
            this._addColumn(_g.d.ic_stk_build_detail._wh_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_stk_build_detail._shelf_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_stk_build_detail._unit_code, 1, 20, 15, true, false, true, true);
            this._addColumn(_g.d.ic_stk_build_detail._qty, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_stk_build_detail._status, 11, 5, 5);
            //this._addColumn(_g.d.ic_purchase_price_detail._row_number, 2, 20, 15, true, false, true, false);
            this._total_show = false;

        }
    }

    public class _icmainGridWeightCostControl : MyLib._myGrid
    {
        public _icmainGridWeightCostControl()
        {

            this.__buid();
        }

        void __buid()
        {
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_weight_cost_detail._table;
            this._addColumn(_g.d.ic_weight_cost_detail._doc_date, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_weight_cost_detail._doc_no, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_weight_cost_detail._bill_date, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_weight_cost_detail._bill_no, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_weight_cost_detail._amount, 3, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_weight_cost_detail._remark, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_weight_cost_detail._status, 11, 5, 5);
            //this._addColumn(_g.d.ic_purchase_price_detail._row_number, 2, 20, 15, true, false, true, false);
            this._total_show = false;
        }
    }

    public class _icmainGridDateAdjustControl : MyLib._myGrid
    {
        public _icmainGridDateAdjustControl()
        {
            this._build();

        }

        private void _build()
        {
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_date_adjust_detail._table;
            this._addColumn(_g.d.ic_date_adjust_detail._doc_date, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._doc_no, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._bill_date, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._bill_no, 1, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._credit_day_old, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._due_date_old, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._sending_date_old, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._credit_day_new, 2, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._due_date_new, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._sending_date_new, 4, 20, 15, true, false, true, false);
            this._addColumn(_g.d.ic_date_adjust_detail._status, 11, 5, 5);
            this._total_show = false;
        }
    }

    public class _icmainGridSizeControl : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;
        public _icmainGridSizeControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_size_use._table;
            this._addColumn(_g.d.ic_size_use._code, 1, 10, 20, true, false, true, true);
            this._addColumn(_g.d.ic_size_use._name_1, 1, 0, 30, false, false, false, false);
            this._addColumn(_g.d.ic_size_use._width_length_height, 1, 20, 25, false, false, true, false);
            this._addColumn(_g.d.ic_size_use._weight, 1, 0, 20, false, false, true, false);
            this._addColumn(_g.d.ic_size_use._status, 11, 5, 5);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridSizeControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridSizeControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridSizeControl__queryForInsertCheck);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridSizeControl__afterAddRow);
        }

        void _icmainGridSizeControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_size_use._status, 1, false);
        }

        bool _icmainGridSizeControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_size_use._code)).Length == 0) ? false : true);
        }

        void _icmainGridSizeControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemColorColumn = this._findColumnByName(_g.d.ic_size_use._code);
            if (__getItemColorColumn == column)
            {
                _searchSizeCode(row);
            }
        }

        void _icmainGridSizeControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                _searchMasterList = _searchProperties._setColumnSearch(e._columnName, 0, _g.d.ic_size_use._table);
                if (!_searchMasterGrid._name.Equals(e._columnName))
                {
                    this._searchMasterGrid.Text = e._columnName;
                    this._searchMasterGrid._dataList._tableName = _searchMasterList[1].ToString();
                    this._searchMasterGrid._name = _searchMasterList[0].ToString();
                    this._searchMasterGrid._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._dataList._loadViewFormat(_searchMasterGrid._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterGrid._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterGrid._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _searchMasterGrid);
            }
            catch (Exception)
            {
            }
        }

        void _searchMasterGrid__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            // _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchAll(string name, int row)
        {
            if (name.CompareTo(_g.g._search_master_ic_size) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(row, 0);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, _g.d.ic_size_use._code, __result, true);
                    _searchSizeCode(row);
                }
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            string __fieldName = _searchMasterGrid.Text;
            if (name.CompareTo(_searchMasterGrid._name) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_size._table + "." + _g.d.ic_size._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        void _searchSizeCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getSizeCode = this._cellGet(row, _g.d.ic_size_use._code).ToString();
            string query = "select " + _g.d.ic_size._name_1 + "," + _g.d.ic_size._width_length_height + "," +
                _g.d.ic_size._weight + " from " + _g.d.ic_size._table + " where " + _g.d.ic_size._code + "=\'" + __getSizeCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][1].ToString();
                    this._cellUpdate(row, _g.d.ic_size_use._code, __getSizeCode, false);
                    this._cellUpdate(row, _g.d.ic_size_use._name_1, dataResult.Tables[0].Rows[0][0].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_size_use._width_length_height, dataResult.Tables[0].Rows[0][1].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_size_use._weight, dataResult.Tables[0].Rows[0][2].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_size_use._status, 1, false);
                }
                else
                {
                    if (__getSizeCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getSizeCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this._cellUpdate(row, _g.d.ic_size_use._code, "", false);
                    this._cellUpdate(row, _g.d.ic_size_use._name_1, "", false);
                    this._cellUpdate(row, _g.d.ic_size_use._width_length_height, "", false);
                    this._cellUpdate(row, _g.d.ic_size_use._weight, "", false);
                    this._cellUpdate(row, _g.d.ic_size_use._status, 0, false);
                }
            }
            catch
            {
            }
        }
    }

    public class _icmainGridPatternControl : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;
        public _icmainGridPatternControl()
        {
            this._build();
        }

        private void _build()
        {
            this._clear();
            this.AllowDrop = true;
            this._rowNumberWork = true;
            this._getResource = true;
            this._table_name = _g.d.ic_pattern._table;
            this._addColumn(_g.d.ic_pattern._code, 1, 15, 20, true, false, true, true);
            this._addColumn(_g.d.ic_pattern._name_1, 1, 20, 35, true, false, false, false);
            this._addColumn(_g.d.ic_pattern._name_2, 1, 20, 35, true, false, false, false);
            this._addColumn(_g.d.ic_pattern._status, 11, 10, 10);
            this._total_show = false;
            this.Invalidate();
            this.ResumeLayout();
            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridPatternControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridPatternControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridPatternControl__queryForInsertCheck);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridPatternControl__afterAddRow);
        }

        void _icmainGridPatternControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_pattern._status, 1, false);
        }

        bool _icmainGridPatternControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_pattern._code)).Length == 0) ? false : true);
        }

        void _icmainGridPatternControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemColorColumn = this._findColumnByName(_g.d.ic_pattern._code);
            if (__getItemColorColumn == column)
            {
                _searchPatternCode(row);
            }
        }

        private void _searchPatternCode(int row)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string __getPatternCode = this._cellGet(row, _g.d.ic_pattern._code).ToString();
            string query = "select " + _g.d.ic_pattern._name_1 + "," + _g.d.ic_pattern._name_2 + "," +
                _g.d.ic_pattern._status + " from " + _g.d.ic_pattern._table + " where " + _g.d.ic_pattern._code + "=\'" + __getPatternCode + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                if (dataResult.Tables[0].Rows.Count > 0)
                {
                    string getData = dataResult.Tables[0].Rows[0][1].ToString();
                    this._cellUpdate(row, _g.d.ic_pattern._code, __getPatternCode, false);
                    this._cellUpdate(row, _g.d.ic_pattern._name_1, dataResult.Tables[0].Rows[0][0].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_pattern._name_2, dataResult.Tables[0].Rows[0][1].ToString(), false);
                    //this._cellUpdate(row, _g.d.ic_pattern._weight, dataResult.Tables[0].Rows[0][2].ToString(), false);
                    this._cellUpdate(row, _g.d.ic_pattern._status, 1, false);
                }
                else
                {
                    if (__getPatternCode.Length > 0)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getPatternCode, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    this._cellUpdate(row, _g.d.ic_pattern._code, "", false);
                    this._cellUpdate(row, _g.d.ic_pattern._name_1, "", false);
                    this._cellUpdate(row, _g.d.ic_pattern._name_2, "", false);
                    //this._cellUpdate(row, _g.d.ic_size_use._weight, "", false);
                    this._cellUpdate(row, _g.d.ic_pattern._status, 0, false);
                }
            }
            catch
            {
            }
        }

        void _icmainGridPatternControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {

            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                _searchMasterList = _searchProperties._setColumnSearch(e._columnName, 0, _g.d.ic_pattern._table);
                if (_searchMasterList.Count == 0)
                {
                    _searchMasterList.Add(_g.g._search_master_ic_pattern);
                    _searchMasterList.Add(_g.d.ic_pattern._table);
                }
                if (!_searchMasterGrid._name.Equals(e._columnName))
                {
                    this._searchMasterGrid.Text = e._columnName;
                    this._searchMasterGrid._dataList._tableName = _searchMasterList[1].ToString();
                    this._searchMasterGrid._name = _searchMasterList[0].ToString();
                    this._searchMasterGrid._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMasterGrid__searchEnterKeyPress);
                    this._searchMasterGrid._dataList._loadViewFormat(_searchMasterGrid._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    this._searchMasterGrid._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._searchMasterGrid._dataList._refreshData();
                }
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _searchMasterGrid);
            }
            catch (Exception)
            {
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            string __fieldName = _searchMasterGrid.Text;
            if (name.CompareTo(_searchMasterGrid._name) == 0)
            {
                string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(e._row, _g.d.ic_pattern._table + "." + _g.d.ic_pattern._code);
                if (__result.Length > 0)
                {
                    _searchMasterGrid.Close();
                    this._cellUpdate(this._selectRow, __fieldName, __result, true);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        void _searchMasterGrid__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            // _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }
    }

    //public class _icmainGridFormatControl : MyLib._myGrid
    //{
    //    public _icmainGridFormatControl()
    //    {
    //        this.__build();
    //    }

    //    void __build()
    //    {
    //        this._clear();
    //        this.AllowDrop = true;
    //        this._rowNumberWork = true;
    //        this._getResource = true;
    //        this._table_name = _g.d.icic_format._table;
    //        this._addColumn(_g.d.ic_format._code, 1, 10, 35, true, false, true, true);
    //        this._addColumn(_g.d.ic_format._name, 1, 0, 60, false, false, true, false);
    //        this._addColumn(_g.d.ic_color._status, 11, 5, 5);
    //        this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
    //        this._total_show = false;
    //    }
    //}

    public class _icmainGridReplacementControl : MyLib._myGrid
    {
        MyLib._searchDataFull _search_data_full;

        public void _load(string itemCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select * ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._table + "." + _g.d.ic_inventory_replacement._ic_replace_code) + ") as " + _g.d.ic_inventory_replacement._ic_name + " from " + _g.d.ic_inventory_replacement._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._ic_replace_code) + "=\'" + itemCode.ToString().ToUpper() + "\'";

            DataSet __getData = __myFrameWork._queryShort(__query);
            this._loadFromDataTable(__getData.Tables[0]);

        }

        public _icmainGridReplacementControl()
        {
            this._table_name = _g.d.ic_inventory_replacement._table;
            this._addColumn(_g.d.ic_inventory_replacement._ic_code, 1, 0, 20, true, false, true, true);
            this._addColumn(_g.d.ic_inventory_replacement._ic_name, 1, 0, 37, false, false, false);
            this._addColumn(_g.d.ic_inventory_replacement._remark, 1, 0, 37, true, false, true);
            this._addColumn(_g.d.ic_inventory_replacement._status, 11, 0, 6, true, false, true);
            //this._addColumn(_g.d.ic_inventory_replacement._line_number, 1, 1, 10, false, true, true);

            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridReplacement__clickSearchButton);
        }

        void _icmainGridReplacement__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {

            _search_data_full = new MyLib._searchDataFull();
            _search_data_full._name = _g.g._search_screen_ic_inventory;
            _search_data_full._dataList._loadViewFormat(_search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_data_full._dataList._refreshData();
            _search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
            _search_data_full._dataList._loadViewData(0);

            MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _search_data_full);

        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {

            //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
            //this._search_data_full.Close();
            search_ic(sender, row);
        }

        void search_ic(object sender, int row)
        {
            string __code = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            string __name = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();
            this._search_data_full.Close();

            this._cellUpdate(this._selectRow, _g.d.ic_inventory_replacement._ic_code, __code, false);
            this._cellUpdate(this._selectRow, _g.d.ic_inventory_replacement._ic_name, __name, false);
            this._cellUpdate(this._selectRow, _g.d.ic_inventory_replacement._status, 1, false);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            search_ic(sender, e._row);
            //throw new NotImplementedException();
            //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
            //this._search_data_full.Close();
        }
    }

    public class _icmainGridSuggestControl : MyLib._myGrid
    {
        MyLib._searchDataFull _search_data_full;

        public void _load(string itemCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select " + _g.d.ic_inventory_suggest._ic_code + ", (select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._table + "." + _g.d.ic_inventory_suggest._ic_suggest_code) + ") as " + _g.d.ic_inventory_replacement._ic_name + " from " + _g.d.ic_inventory_suggest._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_suggest_code) + "=\'" + itemCode.ToString().ToUpper() + "\'";

            DataSet __getData = __myFrameWork._queryShort(__query);
            this._loadFromDataTable(__getData.Tables[0]);

        }

        public _icmainGridSuggestControl()
        {
            this._table_name = _g.d.ic_inventory_suggest._table;
            this._addColumn(_g.d.ic_inventory_suggest._ic_code, 1, 0, 20, true, false, true, true);
            this._addColumn(_g.d.ic_inventory_suggest._ic_name, 1, 0, 74, false, false, false);
            //this._addColumn(_g.d.ic_inventory_suggest._remark, 1, 0, 37, true, false, true);
            this._addColumn(_g.d.ic_inventory_suggest._status, 11, 0, 6, true, false, true);
            //this._addColumn(_g.d.ic_inventory_replacement._line_number, 1, 1, 10, false, true, true);

            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridReplacement__clickSearchButton);
        }

        void _icmainGridReplacement__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {

            _search_data_full = new MyLib._searchDataFull();
            _search_data_full._name = _g.g._search_screen_ic_inventory;
            _search_data_full._dataList._loadViewFormat(_search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_data_full._dataList._refreshData();
            _search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
            _search_data_full._dataList._loadViewData(0);

            if (MyLib._myGlobal._OEMVersion.Equals("imex"))
            {
                _search_data_full.StartPosition = FormStartPosition.CenterScreen;
                _search_data_full.ShowDialog();
            }
            else
                MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _search_data_full);

        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {

            //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
            //this._search_data_full.Close();
            search_ic(sender, row);
        }

        void search_ic(object sender, int row)
        {
            string __code = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            string __name = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();
            this._search_data_full.Close();

            this._cellUpdate(this._selectRow, _g.d.ic_inventory_suggest._ic_code, __code, false);
            this._cellUpdate(this._selectRow, _g.d.ic_inventory_suggest._ic_name, __name, false);
            this._cellUpdate(this._selectRow, _g.d.ic_inventory_suggest._status, 1, false);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            search_ic(sender, e._row);
            //throw new NotImplementedException();
            //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
            //this._search_data_full.Close();
        }
    }

    public class _icmainGridBundleControl : MyLib._myGrid
    {
        MyLib._searchDataFull _search_data_full;

        public void _load(string itemCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select * ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_bundle._table + "." + _g.d.ic_inventory_bundle._ic_code) + ") as " + _g.d.ic_inventory_bundle._ic_name + " from " + _g.d.ic_inventory_bundle._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_bundle._ic_code) + "=\'" + itemCode.ToString().ToUpper() + "\'";

            DataSet __getData = __myFrameWork._queryShort(__query);
            this._loadFromDataTable(__getData.Tables[0]);

        }

        public _icmainGridBundleControl()
        {
            this._table_name = _g.d.ic_inventory_bundle._table;
            this._addColumn(_g.d.ic_inventory_bundle._barcode, 1, 0, 20, true, false, true, true);
            this._addColumn(_g.d.ic_inventory_bundle._ic_code, 1, 0, 20, false, false, true, false);
            this._addColumn(_g.d.ic_inventory_bundle._ic_name, 1, 0, 35, false, false, false);
            this._addColumn(_g.d.ic_inventory_bundle._unit_code, 1, 0, 15, false, false, true);
            this._addColumn(_g.d.ic_inventory_bundle._qty, 3, 0, 10, true, false, true, false, "#,###,###.00");
            //this._addColumn(_g.d.ic_inventory_bundle._line_number, 1, 1, 10, false, true, true);

            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridReplacement__clickSearchButton);
            this._queryForInsertCheck += _icmainGridBundleControl__queryForInsertCheck;
        }

        bool _icmainGridBundleControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((this._cellGet(row, _g.d.ic_inventory_bundle._barcode).ToString().Trim().Length == 0) ? false : true);
        }

        void _icmainGridReplacement__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {

            _search_data_full = new MyLib._searchDataFull();
            _search_data_full._name = _g.g._search_screen_ic_barcode;
            _search_data_full._dataList._loadViewFormat(_search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_data_full._dataList._refreshData();
            _search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
            _search_data_full._dataList._loadViewData(0);

            MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _search_data_full);

        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {

            //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
            //this._search_data_full.Close();
            search_ic(sender, row);
        }

        void search_ic(object sender, int row)
        {
            string __barcode = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
            string __code = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code).ToString();
            string __name = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._description).ToString();
            string __unitCode = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code).ToString();
            this._search_data_full.Close();

            this._cellUpdate(this._selectRow, _g.d.ic_inventory_bundle._ic_code, __code, false);
            this._cellUpdate(this._selectRow, _g.d.ic_inventory_bundle._ic_name, __name, false);
            this._cellUpdate(this._selectRow, _g.d.ic_inventory_bundle._barcode, __barcode, false);
            this._cellUpdate(this._selectRow, _g.d.ic_inventory_bundle._unit_code, __unitCode, false);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            search_ic(sender, e._row);
            //throw new NotImplementedException();
            //string __code = this._search_data_full._dataList._gridData._cellGet(this._search_data_full._dataList._gridData._selectRow, _g.d.erp_view_table._table + "." + _g.d.erp_view_table._screen_code).ToString();
            //this._search_data_full.Close();
        }
    }

    public class _icmainGridInteraction : MyLib._myGrid
    {
        MyLib._searchDataFull _search_data_full;

        public void _load(string itemCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = ""; // "select * ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=" + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._table + "." + _g.d.ic_inventory_replacement._ic_replace_code) + ") as " + _g.d.ic_inventory_replacement._ic_name + " from " + _g.d.ic_inventory_replacement._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_replacement._ic_replace_code) + "=\'" + itemCode.ToString().ToUpper() + "\'";

            DataSet __getData = __myFrameWork._queryShort(__query);
            this._loadFromDataTable(__getData.Tables[0]);

        }

        public _icmainGridInteraction()
        {
            this._table_name = _g.d.m_druginteraction._table;
            this._addColumn(_g.d.m_druginteraction._item_code_2, 1, 0, 20, true, false, true, true, "", "", "", _g.d.m_druginteraction._item_code);
            this._addColumn(_g.d.m_druginteraction._item_name, 1, 0, 30, false, false, false);
            this._addColumn(_g.d.m_druginteraction._remark, 1, 0, 50, true, false, true);

            this._clickSearchButton += _icmainGridInteraction__clickSearchButton;
        }

        void _icmainGridInteraction__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _search_data_full = new MyLib._searchDataFull();
            _search_data_full._name = _g.g._search_screen_ic_inventory;
            _search_data_full._dataList._loadViewFormat(_search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_data_full._dataList._gridData._mouseClick += _gridData__mouseClick;
            _search_data_full._dataList._refreshData();
            _search_data_full._searchEnterKeyPress += _search_data_full__searchEnterKeyPress;
            _search_data_full._dataList._loadViewData(0);

            MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _search_data_full);

        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            search_ic(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            search_ic(sender, e._row);
        }

        void search_ic(object sender, int row)
        {
            string __code = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            string __name = this._search_data_full._dataList._gridData._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();
            this._search_data_full.Close();

            this._cellUpdate(this._selectRow, _g.d.m_druginteraction._item_code_2, __code, false);
            this._cellUpdate(this._selectRow, _g.d.m_druginteraction._item_name, __name, false);
        }

    }

    public class _icPriceFormulaGrid : MyLib._myGrid
    {
        public SMLERPControl._ic._icTransItemGridSelectUnitForm _icTransItemGridSelectUnit = new SMLERPControl._ic._icTransItemGridSelectUnitForm();
        object[] _sale_type = new object[] { "ไม่เลือก", "ขายสด", "ขายเชื่อ" };

        public _icPriceFormulaGrid()
        {
            // create grid price formula
            this._width_by_persent = true;
            this._table_name = _g.d.ic_inventory_price_formula._table;
            this._addColumn(_g.d.ic_inventory_price_formula._unit_code, 1, 0, 10, false, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_0, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_1, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_2, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_3, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_4, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_5, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_6, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_7, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_8, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._price_9, 1, 0, 10, true, false);
            this._addColumn(_g.d.ic_inventory_price_formula._sale_type, 10, 0, 10, true, false);

            this._columnExtraWord(_g.d.ic_inventory_price_formula._unit_code, "(F4)");

            if (this.DesignMode == false)
            {
                this._cellComboBoxGet += _icPriceFormulaGrid__cellComboBoxGet;
                this._cellComboBoxItem += _icPriceFormulaGrid__cellComboBoxItem;
                this._icTransItemGridSelectUnit._selectUnitCode += _icTransItemGridSelectUnit__selectUnitCode;
                this._afterAddRow += _icPriceFormulaGrid__afterAddRow;
                this._queryForInsertCheck += _icPriceFormulaGrid__queryForInsertCheck;
            }

            this._calcPersentWidthToScatter();
            this.Invalidate();
        }

        bool _icPriceFormulaGrid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                return (this._cellGet(row, _g.d.ic_inventory_price_formula._price_1).ToString().Trim().Length != 0 || this._cellGet(row, _g.d.ic_inventory_price_formula._price_1).ToString().Trim().Length != 0) ? true : false;
            }
            return (this._cellGet(row, _g.d.ic_inventory_price_formula._price_0).ToString().Trim().Length == 0) ? false : true;
        }

        void _icPriceFormulaGrid__afterAddRow(object sender, int row)
        {
            if (this.GetUnitCode != null)
            {
                string __getUnitCode = GetUnitCode(this);
                this._cellUpdate(this._selectRow, _g.d.ic_inventory_price_formula._unit_code, __getUnitCode, false);
            }
        }

        void _icTransItemGridSelectUnit__selectUnitCode(int mode, string unitCode)
        {
            if (mode == 1)
            {
                if (this._selectRow < this._rowData.Count)
                {
                    this._cellUpdate(this._selectRow, _g.d.ic_inventory_price_formula._unit_code, unitCode, false);
                }
            }
        }

        object[] _icPriceFormulaGrid__cellComboBoxItem(object sender, int row, int column)
        {
            return _sale_type;
        }

        string _icPriceFormulaGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            return (_sale_type[(select == -1) ? 0 : select].ToString());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F4:
                    _selectUnitCode();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _selectUnitCode()
        {
            int __unitType = (this.GetUnitType == null) ? 0 : this.GetUnitType(this);
            string __itemDesc = (this.GetItemDesc == null) ? "" : this.GetItemDesc(this);
            string __itemCode = (this.GetItemCode == null) ? "" : this.GetItemCode(this);

            if (__unitType == 0)
            {
                MessageBox.Show(__itemDesc + " : สินค้านี้มีหน่วยนับเดียว");
            }
            else
            {
                string __unitCode = this._cellGet(this._selectRow, _g.d.ic_inventory_price_formula._unit_code).ToString();
                this._icTransItemGridSelectUnit._itemCode = __itemCode;
                this._icTransItemGridSelectUnit._lastCode = __unitCode;
                this._icTransItemGridSelectUnit.Text = __itemDesc;
                this._icTransItemGridSelectUnit.ShowDialog();
            }
        }

        public event GetUnitTypeEventHandler GetUnitType;
        public event GetItemDescEventHandler GetItemDesc;
        public event GetItemCodeEventHandler GetItemCode;
        public event GetUnitCodeEventHandler GetUnitCode;

    }

}
