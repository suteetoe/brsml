using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._ic
{
    public class _icmainGridUnitControl : MyLib._myGrid
    {
        public string _itemCode = "";
        public string _itemName = "";

        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;

        public bool _filterUnitUse = true;

        public _icmainGridUnitControl()
        {
            _build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._table_name = _g.d.ic_unit_use._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._addColumn(_g.d.ic_unit_use._code, 1, 10, 15, true, false, true, true);
            this._addColumn(_g.d.ic_unit_use._name_1, 1, 0, 20, false, false, false, false);
            this._addColumn(_g.d.ic_unit_use._stand_value, 3, 0, 10, true, false, true, true, __formatNumber);
            this._addColumn(_g.d.ic_unit_use._divide_value, 3, 0, 10, true, false, true, true, __formatNumber);
            this._addColumn(_g.d.ic_unit_use._ratio, 3, 0, 10, false, false, true, true, __formatNumber);
            this._addColumn(_g.d.ic_unit_use._row_order, 3, 0, 10, true, false, true, false, __formatNumberNone);
            this._addColumn(_g.d.ic_unit_use._width_length_height, 1, 0, 10, true, false, true, false);
            this._addColumn(_g.d.ic_unit_use._weight, 1, 0, 10, true, false, true, false);
            this._addColumn(_g.d.ic_unit_use._status, 11, 5, 5);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this._total_show = false;
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._clickSearchButton += new MyLib.SearchEventHandler(_icmainGridUnitControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridUnitControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridUnitControl__queryForInsertCheck);
            this._queryForInsertPerRow += new MyLib.QueryForInsertPerRowEventHandler(_icmainGridUnitControl__queryForInsertPerRow);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridUnitControl__afterAddRow);
        }

        public string _createQueryForLoad(string itemCode)
        {
            if (this._filterUnitUse == true)
            {
                return "select *,(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table
                        + " where " + _g.d.ic_unit._code + "=" + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + ") as " + _g.d.ic_unit_use._name_1
                        + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._ic_code + "=\'" + itemCode + "\' order by " + _g.d.ic_unit_use._ratio;
            }
            else
            {
                return "select * from " + _g.d.ic_unit._table + " order by " + _g.d.ic_unit._code;
            }
        }

        MyLib.QueryForInsertPerRowType _icmainGridUnitControl__queryForInsertPerRow(MyLib._myGrid sender, int row)
        {
            MyLib.QueryForInsertPerRowType result = new MyLib.QueryForInsertPerRowType();
            result._field = "line_number";
            result._data = row.ToString();
            return (result);
        }

        void _icmainGridUnitControl__afterAddRow(object sender, int row)
        {
            this._cellUpdate(row, _g.d.ic_unit_use._status, 1, false);
        }

        bool _icmainGridUnitControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_unit_use._code)).Length == 0) ? false : true);
        }

        void _icmainGridUnitControl__alterCellUpdate(object sender, int row, int column)
        {
            try
            {
                if (column == 0)
                {
                    _searchUnitRow(row);
                }
                if (column == 2 || column == 3)
                {
                    decimal __standvalue = Convert.ToDecimal(this._cellGet(row, 2).ToString());
                    decimal __didive = Convert.ToDecimal(this._cellGet(row, 3).ToString());
                    if (__standvalue != 0.00M && __didive != 0.00M)
                    {
                        decimal __getTotal = (__standvalue) / (__didive);
                        this._cellUpdate(row, _g.d.ic_unit_use._ratio, __getTotal, false);
                    }
                }
                /*
                if (column == 6)
                {
                    char[] splitter = { 'x', 'X' };
                    string info = this._cellGet(row, 6).ToString();
                    string[] arInfo = info.Split(splitter);
                    if (arInfo.Length != 4)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("รูปแแบบไม่ถูกต้อง\n -รูปแบบต้องเป็น (9x9x99x99)\n*** 9 แทนตัวเลขของ ความกว้าง ความยาว ความสูง หน่วย\nต้องอยู่ในรูปแบบ กว้างxยาวxสูงxหน่วย"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this._cellUpdate(row, _g.d.ic_unit_use._width_length_height, "", false);
                    }
                }
                */
            }
            catch (Exception)
            {
            }
        }

        void _icmainGridUnitControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                if (e._columnName.Equals(_g.d.ic_unit_use._code))
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
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        private void _searchAll(string name, int row)
        {
            string __result = (string)_searchMasterGrid._dataList._gridData._cellGet(row, _g.d.ic_unit._table + "." + _g.d.ic_unit._code);
            if (__result.Length > 0)
            {
                _searchMasterGrid.Close();
                this._cellUpdate(this._selectRow, _g.d.ic_unit._code, __result, true);
                _searchUnitRow(this._selectRow);
            }
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
            string __unitCode = this._cellGet(row, _g.d.ic_unit_use._code).ToString();
            string __query = "select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + __unitCode.ToUpper() + "\'";
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            string __unitName = "";
            if (__getData.Tables.Count > 0)
            {
                int __indexName1 = __getData.Tables[0].Columns.IndexOf(_g.d.ic_unit._name_1);
                __unitName = __getData.Tables[0].Rows[0].ItemArray.GetValue(__indexName1).ToString();
            }
            this._cellUpdate(row, _g.d.ic_unit_use._name_1, __unitName, false);
        }
    }
}
