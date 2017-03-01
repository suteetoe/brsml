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

    public class _icStandardCostControl : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchMasterGrid;
        public _icStandardCostControl()
        {
            _build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._table_name = _g.d.ic_standard_cost._table;
            string __formatNumber = _g.g._getFormatNumberStr(2); // MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._addColumn(_g.d.ic_standard_cost._date_start, 4, 10, 25, true, false, true);
            this._addColumn(_g.d.ic_standard_cost._unit_code, 1, 10, 25, false, false, true, true);
            this._addColumn(_g.d.ic_standard_cost._unit_name, 1, 0, 25, false, false, false, false);
            this._addColumn(_g.d.ic_standard_cost._cost1, 3, 0, 25, true, false, true, true, __formatNumber);
            this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
            this.Dock = DockStyle.Fill;
            this.Invalidate();
            this.ResumeLayout();
            this._clickSearchButton += new MyLib.SearchEventHandler(_icStandardCostGridControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icStandardCostGridControl__alterCellUpdate);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icStandardCostGridControl__queryForInsertCheck);
        }

        bool _icStandardCostGridControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return ((((string)this._cellGet(row, _g.d.ic_standard_cost._unit_code)).Length == 0) ? false : true);
        }

        void _icStandardCostGridControl__alterCellUpdate(object sender, int row, int column)
        {
            int __getItemUnitColumn = this._findColumnByName(_g.d.ic_standard_cost._unit_code);
            if (__getItemUnitColumn == column)
            {
                _searchUnitRow(row);
            }
        }

        void _icStandardCostGridControl__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            _searchMasterGrid = new MyLib._searchDataFull();
            SMLERPGlobal._searchProperties _searchProperties = new SMLERPGlobal._searchProperties();
            ArrayList _searchMasterList = new ArrayList();
            try
            {
                if (e._columnName.Equals(_g.d.ic_standard_cost._unit_code))
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
            string __unitCode = this._cellGet(row, _g.d.ic_standard_cost._unit_code).ToString();
            string __query = "select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + __unitCode.ToUpper() + "\'";
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            string __unitName = "";
            if (__getData.Tables.Count > 0)
            {
                int __indexName1 = __getData.Tables[0].Columns.IndexOf(_g.d.ic_unit._name_1);
                __unitName = __getData.Tables[0].Rows[0].ItemArray.GetValue(__indexName1).ToString();
            }
            this._cellUpdate(row, _g.d.ic_standard_cost._unit_name, __unitName, false);
        }
    }
}
