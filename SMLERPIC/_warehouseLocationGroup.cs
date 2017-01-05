using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _warehouseLocationGroup : UserControl
    {
        MyLib._searchDataFull _searchWh = new MyLib._searchDataFull();

        public _warehouseLocationGroup()
        {
            InitializeComponent();
            _build();
        }

        void _build()
        {
            this._selectedGrid.WidthByPersent = true;
            this._selectedGrid._table_name = _g.d.ic_shelf._table;
            this._selectedGrid._addColumn("select", 11, 10, 10);
            this._selectedGrid._addColumn(_g.d.ic_shelf._code, 1, 30, 30);
            this._selectedGrid._addColumn(_g.d.ic_shelf._name_1, 1, 60, 60);
            this._selectedGrid._isEdit = false;
            this._selectedGrid._calcPersentWidthToScatter();

            this._listGrid.WidthByPersent = true;
            this._listGrid._table_name = _g.d.ic_warehouse_location_group_detail._table;
            this._listGrid._addColumn(_g.d.ic_warehouse_location_group_detail._location, 1, 30, 30);
            this._listGrid._addColumn(_g.d.ic_warehouse_location_group_detail._location_name, 1, 70, 70, false);
            this._listGrid._isEdit = false;
            this._listGrid._calcPersentWidthToScatter();

            this._screenTop._textBoxSearch += _screenTop__textBoxSearch;
            this._screenTop._textBoxChanged += _screenTop__textBoxChanged;

            _searchWh._name = _g.g._search_master_ic_warehouse;
            _searchWh._dataList._loadViewFormat(_searchWh._name, MyLib._myGlobal._userSearchScreenGroup, false);

            _searchWh._dataList._gridData._mouseClick += _gridData__mouseClick;
            _searchWh._searchEnterKeyPress += _searchWh__searchEnterKeyPress;

            this._selectedGrid._alterCellUpdate += _selectedGrid__alterCellUpdate;
        }

        public void _search(bool warning)
        {
            string __whCode = this._screenTop._getDataStr(_g.d.ic_warehouse_location_group._wh_code);
            if (__whCode.Length > 0)
            {
                StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                // search wh_name
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where  " + _g.d.ic_warehouse._code + "=\'" + __whCode + "\' "));
                // search location list
                __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_shelf._code + "," + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where  " + _g.d.ic_shelf._whcode + "=\'" + __whCode + "\' "));
                __queryList.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());

                if (__result.Count > 0)
                {
                    DataTable __warehouseTable = ((DataSet)__result[0]).Tables[0];

                    this._screenTop._setDataStr(_g.d.ic_warehouse_location_group._wh_code, __whCode, __warehouseTable.Rows[0][0].ToString(), true);
                    this._selectedGrid._clear();
                    this._listGrid._clear();
                    this._selectedGrid._loadFromDataTable(((DataSet)__result[1]).Tables[0]);
                }

            }
        }

        private void _selectedGrid__alterCellUpdate(object sender, int row, int column)
        {
            if (column == 0)
            {
                ArrayList __selectLocationCode = new ArrayList();
                ArrayList __selectLocationName = new ArrayList();
                for (int __row = 0; __row < this._selectedGrid._rowData.Count; __row++)
                {
                    string __select = this._selectedGrid._cellGet(__row, 0).ToString();
                    if (__select.Equals("1"))
                    {
                        string __code = this._selectedGrid._cellGet(__row, 1).ToString();
                        if (__code.Trim().Length > 0)
                        {
                            string __name = this._selectedGrid._cellGet(__row, 2).ToString();
                            __selectLocationCode.Add(__code);
                            __selectLocationName.Add(__name);
                        }
                    }
                }
                this._listGrid._clear();
                for (int __loop = 0; __loop < __selectLocationCode.Count; __loop++)
                {
                    int __addr = this._listGrid._addRow();
                    this._listGrid._cellUpdate(__addr, 0, __selectLocationCode[__loop], false);
                    this._listGrid._cellUpdate(__addr, 1, __selectLocationName[__loop], false);
                }
            }
        }

        private void _screenTop__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_warehouse_location_group._wh_code))
            {
                _search(false);
            }
        }

        private void _searchWh__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            //this._searchAll(__getParent2._name, row);
            //SendKeys.Send("{TAB}");
            string __getWhCode = __getParent1._gridData._cellGet(row, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
            this._screenTop._setDataStr(_g.d.ic_warehouse_location_group._wh_code, __getWhCode);

            _searchWh.Close();
        }

        private void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            //this._searchAll(__getParent2._name, e._row);

            string __getWhCode = __getParent1._gridData._cellGet(e._row, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();

            this._screenTop._setDataStr(_g.d.ic_warehouse_location_group._wh_code, __getWhCode);
            _searchWh.Close();
        }

        private void _screenTop__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            string label_name = __getControl._labelName;

            MyLib._myGlobal._startSearchBox(__getControl, label_name, _searchWh, false);

        }
    }
}
