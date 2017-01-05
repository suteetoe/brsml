using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPConfig
{
    public class _gridSalePlanDetail : MyLib._myGrid
    {
        private MyLib._searchDataFull _searchMaster;
        string _searchName = "";
        public _gridSalePlanDetail()
        {
            this._table_name = _g.d.erp_sale_plan_detail._table;
            this._addColumn(_g.d.erp_sale_plan_detail._sale_area_code, 1, 15, 15, true, false, true, true);
            this._addColumn(_g.d.erp_sale_plan_detail._sale_code, 1, 15, 20, true, false, true, true);
            this._addColumn(_g.d.erp_sale_plan_detail._item_code, 1, 15, 15, true, false, true, true);
            this._addColumn(_g.d.erp_sale_plan_detail._sale_plan_amount, 3, 20, 20, true, false, true, false, "#,###,###.00");
            this._addColumn(_g.d.erp_sale_plan_detail._remark, 1, 30, 30);

            this._addColumn(_g.d.erp_sale_plan_detail._sale_area_name, 1, 15, 15, true, true, false, true);
            this._addColumn(_g.d.erp_sale_plan_detail._sale_name, 1, 15, 15, true, true, false, true);
            this._addColumn(_g.d.erp_sale_plan_detail._item_name, 1, 15, 15, true, true, false, true);
            this._clickSearchButton += _gridSalePlanDetail__clickSearchButton;
            this._beforeDisplayRendering += _gridSalePlanDetail__beforeDisplayRendering;
            this._calcPersentWidthToScatter();
        }

        MyLib.BeforeDisplayRowReturn _gridSalePlanDetail__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, System.Drawing.Graphics e)
        {
            if (columnName.Equals(_g.d.erp_sale_plan_detail._table + "." + _g.d.erp_sale_plan_detail._sale_area_code))
            {
                int __unitNameColumn = this._findColumnByName(_g.d.erp_sale_plan_detail._sale_area_name);
                string __unitName = this._cellGet(row, __unitNameColumn).ToString();
                if (__unitName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __unitName);
            }
            else
                if (columnName.Equals(_g.d.erp_sale_plan_detail._table + "." + _g.d.erp_sale_plan_detail._sale_code))
                {
                    int __whNameColumn = this._findColumnByName(_g.d.erp_sale_plan_detail._sale_name);
                    string __whName = this._cellGet(row, __whNameColumn).ToString();
                    if (__whName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __whName);
                }
                else if (columnName.Equals(_g.d.erp_sale_plan_detail._table + "." + _g.d.erp_sale_plan_detail._item_code))
                {
                    int __whNameColumn = this._findColumnByName(_g.d.erp_sale_plan_detail._item_name);
                    string __whName = this._cellGet(row, __whNameColumn).ToString();
                    if (__whName.Length > 0) ((ArrayList)senderRow.newData)[columnNumber] = string.Concat(((ArrayList)senderRow.newData)[columnNumber].ToString(), "~", __whName);
                }
            return (senderRow);
        }

        void _gridSalePlanDetail__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {

            if (e._columnName.Equals(_g.d.erp_sale_plan_detail._sale_area_code))
            {
                this._searchMaster = new MyLib._searchDataFull();
                this._searchMaster._name = _g.g._search_master_ar_area_code;
                this._searchName = e._columnName;
                //
                this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMaster__searchEnterKeyPress);
                this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchMaster._dataList._refreshData();
                this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                this._searchMaster.ShowDialog();
            }
            else if (e._columnName.Equals(_g.d.erp_sale_plan_detail._sale_code))
            {
                this._searchMaster = new MyLib._searchDataFull();
                this._searchMaster._name = _g.g._search_screen_erp_user;
                this._searchName = e._columnName;
                //
                this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMaster__searchEnterKeyPress);
                this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchMaster._dataList._refreshData();
                this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                this._searchMaster.ShowDialog();
            }
            else if (e._columnName.Equals(_g.d.erp_sale_plan_detail._item_code))
            {
                this._searchMaster = new MyLib._searchDataFull();
                this._searchMaster._name = _g.g._search_screen_ic_inventory;
                this._searchName = e._columnName;
                //
                this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchMaster._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchMaster__searchEnterKeyPress);
                this._searchMaster._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchMaster._dataList._refreshData();
                this._searchMaster.StartPosition = FormStartPosition.CenterScreen;
                this._searchMaster.ShowDialog();
            }

        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchMaster.Close();

            if (this._searchName.Equals(_g.d.erp_sale_plan_detail._sale_area_code))
            {
                MyLib._myGrid __grid = (MyLib._myGrid)sender;
                string __code = __grid._cellGet(e._row, _g.d.ar_sale_area._table + "." + _g.d.ar_sale_area._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.erp_sale_plan_detail._sale_area_code, __code, true);
                _searchSaleAreaUserAndItemName(this.SelectRow);
            }
            else if (this._searchName.Equals(_g.d.erp_sale_plan_detail._sale_code))
            {
                MyLib._myGrid __grid = (MyLib._myGrid)sender;
                string __code = __grid._cellGet(e._row, _g.d.erp_user._table + "." + _g.d.erp_user._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.erp_sale_plan_detail._sale_code, __code, true);
                _searchSaleAreaUserAndItemName(this.SelectRow);
            }
            else if (this._searchName.Equals(_g.d.erp_sale_plan_detail._item_code))
            {
                MyLib._myGrid __grid = (MyLib._myGrid)sender;
                string __code = __grid._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.erp_sale_plan_detail._item_code, __code, true);
                _searchSaleAreaUserAndItemName(this.SelectRow);
            }

            SendKeys.Send("{TAB}");
            this._searchName = "";
        }

        void _searchMaster__searchEnterKeyPress(MyLib._myGrid __grid, int row)
        {
            this._searchMaster.Close();
            if (this._searchName.Equals(_g.d.erp_sale_plan_detail._sale_area_code))
            {
                //MyLib._myGrid __grid = (MyLib._myGrid)sender;
                string __code = __grid._cellGet(row, _g.d.ar_sale_area._table + "." + _g.d.ar_sale_area._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.erp_sale_plan_detail._sale_area_code, __code, true);
            }
            else if (this._searchName.Equals(_g.d.erp_sale_plan_detail._sale_code))
            {
                //MyLib._myGrid __grid = (MyLib._myGrid)sender;
                string __code = __grid._cellGet(row, _g.d.erp_user._table + "." + _g.d.erp_user._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.erp_sale_plan_detail._sale_code, __code, true);
            }
            else if (this._searchName.Equals(_g.d.erp_sale_plan_detail._item_code))
            {
                //MyLib._myGrid __grid = (MyLib._myGrid)sender;
                string __code = __grid._cellGet(row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                this._cellUpdate(this._selectRow, _g.d.erp_sale_plan_detail._item_code, __code, true);
            }
            SendKeys.Send("{TAB}");
            this._searchName = "";

        }

        public void _searchSaleAreaUserAndItemName(int row)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __areaCode = this._cellGet(row, _g.d.erp_sale_plan_detail._sale_area_code).ToString().ToUpper();
                string __userCode = this._cellGet(row, _g.d.erp_sale_plan_detail._sale_code).ToString().ToUpper();
                string __itemCode = this._cellGet(row, _g.d.erp_sale_plan_detail._item_code).ToString().ToUpper();

                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_sale_area._name_1 + " from " + _g.d.ar_sale_area._table +
                    " where " + _g.d.ar_sale_area._code + "=\'" + __areaCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table +
                    " where " + MyLib._myGlobal._addUpper(_g.d.erp_user._code )+ "=\'" + __userCode.ToUpper() + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table +
                    " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'"));
                __query.Append("</node>");
                String __queryStr = __query.ToString();
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                //
                string __areaName = "";
                string __saleName = "";
                string __itemName = "";

                DataTable __t1 = ((DataSet)__dataResult[0]).Tables[0];
                DataTable __t2 = ((DataSet)__dataResult[1]).Tables[0];
                DataTable __t3 = ((DataSet)__dataResult[2]).Tables[0];

                if (__t1.Rows.Count > 0) __areaName = __t1.Rows[0][_g.d.ar_sale_area._name_1].ToString();
                if (__t2.Rows.Count > 0) __saleName = __t2.Rows[0][_g.d.erp_user._name_1].ToString();
                if (__t3.Rows.Count > 0) __itemName = __t3.Rows[0][_g.d.ic_inventory._name_1].ToString();

                this._cellUpdate(row, _g.d.erp_sale_plan_detail._sale_area_name, __areaName, false);
                this._cellUpdate(row, _g.d.erp_sale_plan_detail._sale_name, __saleName, false);
                this._cellUpdate(row, _g.d.erp_sale_plan_detail._item_name, __itemName, false);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }


    }
}
