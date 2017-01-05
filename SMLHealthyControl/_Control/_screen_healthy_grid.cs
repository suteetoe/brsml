using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

/*
* @author MooAe 
* @copyright 2009
* @mail naiay@msn.com
*/

namespace SMLHealthyControl._Control
{

    public partial class _screen_healthy_grid : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        private _controlTypeEnumGrid _controlTypeTemp;
        string _unitName = "unit_name";

        public _controlTypeEnumGrid _controlName
        {
            set
            {
                this._controlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        public _screen_healthy_grid()
        {
            this._build();
        }
        void _build()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this.SuspendLayout();
            this._columnList.Clear();
            switch (_controlName)
            {
                case _controlTypeEnumGrid.drugsConsultants_details:
                    this._addRowEnabled = true;
                    this._table_name = _g.d.m_drugsconsultants_details._table;
                    this._addColumn(_g.d.m_drugsconsultants_details._ic_code, 1, 0, 10, true, false, true, true);
                    this._addColumn(_g.d.m_drugsconsultants_details._ic_name, 1, 0, 10, false, false, false, false);
                    this._addColumn(_g.d.m_drugsconsultants_details._unit_name, 1, 0, 10, false, false, false, false);
                    this._addColumn(_g.d.m_drugsconsultants_details._qty, 2, 0, 10, true, false, true, false);
                    this._addColumn(_g.d.m_drugsconsultants_details._times, 1, 0, 10, false, false, true, true);
                    this._addColumn(_g.d.m_drugsconsultants_details._frequency, 1, 0, 10, false, false, true, true);
                    this._addColumn(_g.d.m_drugsconsultants_details._warning, 1, 0, 10, false, false, true, true);
                    this._addColumn(_g.d.m_drugsconsultants_details._properties, 1, 0, 10, false, false, true, true);
                    this._addColumn(_g.d.m_drugsconsultants_details._remark, 1, 0, 20, true, false, true);

                    break;
            }
            this.ColumnBackground = Color.FromArgb(250, 251, 252);
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.ColumnBackgroundAuto = false;
            this._rowNumberWork = true;
            this._moveNextColumn -= new MyLib.MoveNextColumnEventHandler(_arCustomerContactGrid__moveNextColumn);
            this._clickSearchButton -= new MyLib.SearchEventHandler(_arCustomerContactGrid__clickSearchButton);
            this._afterAddRow -= new MyLib.AfterAddRowEventHandler(_screen_ar_grid__afterAddRow);
            this._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_screen_ar_grid__alterCellUpdate);
            //this._beforeDisplayRow -= new MyLib.BeforeDisplayRowEventHandler(_screen_healthy_grid__beforeDisplayRow);
            this._moveNextColumn += new MyLib.MoveNextColumnEventHandler(_arCustomerContactGrid__moveNextColumn);
            this._clickSearchButton += new MyLib.SearchEventHandler(_arCustomerContactGrid__clickSearchButton);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_screen_ar_grid__afterAddRow);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_screen_ar_grid__queryForInsertCheck);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_screen_ar_grid__alterCellUpdate);
            //this._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_screen_healthy_grid__beforeDisplayRow);

            this.ResumeLayout();
        }

        MyLib.BeforeDisplayRowReturn _screen_healthy_grid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            return senderRow;
        }

        void _screen_ar_grid__alterCellUpdate(object sender, int row, int column)
        {
            int __itemCodeColumnNumber = this._findColumnByName(_g.d.m_drugsconsultants_details._ic_code);
            string __itemCode = this._cellGet(row, __itemCodeColumnNumber).ToString().ToUpper();
            switch (_controlName)
            {

                case _controlTypeEnumGrid.drugsConsultants_details:
                    if (column == __itemCodeColumnNumber)
                    {
                        // ค้นหา BarCode ก่อน
                        string __barCodeStr = __itemCode.Replace("*", "").Replace(" ", "");
                        DataTable __barCode = this._myFrameWork._queryShort("select " + _g.d.ic_inventory_barcode._ic_code + "," + _g.d.ic_inventory_barcode._unit_code + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "=\'" + __barCodeStr + "\'").Tables[0];
                        if (__barCode.Rows.Count > 0)
                        {
                            __itemCode = __barCode.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString();
                            string __unitCode = __barCode.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString();
                            this._cellUpdate(row, _g.d.ic_trans_detail._item_code, __itemCode, false);
                            this._cellUpdate(row, _g.d.ic_trans_detail._unit_code, __unitCode, false);

                            //if (this._findColumnByName(_g.d.ic_trans_detail._barcode) != -1)
                            //{
                            //    this._cellUpdate(row, _g.d.ic_trans_detail._barcode, __barCodeStr, false);
                            //}
                        }
                        //
                        this._gridFindItem(row);
                        //this._searchAndWarning(row);

                        // ยังไม่ได้หาจาก barcode 



                    }
                    break;
            }

        }

        public void _gridFindItem(int row)
        {
            switch (_controlName)
            {
                case _controlTypeEnumGrid.drugsConsultants_details:

                    // toe manual search
                    string __itemCode = this._cellGet(row, _g.d.m_drugsconsultants_details._ic_code).ToString();
                    string  __query = "select " + _g.d.ic_inventory._name_1 + ", " + _g.d.ic_inventory._unit_cost + ", (select " + _g.d.m_information._table + "." + _g.d.m_information._times + " from " + _g.d.m_information._table + " where " + _g.d.m_information._table + "." + _g.d.m_information._ic_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " ) as " + _g.d.m_drugsconsultants_details._times + ", (select " + _g.d.m_information._table + "." + _g.d.m_information._frequency + " from " + _g.d.m_information._table + " where " + _g.d.m_information._table + "." + _g.d.m_information._ic_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " ) as " + _g.d.m_drugsconsultants_details._frequency + ", (select " + _g.d.m_information._table + "." + _g.d.m_information._warning + " from " + _g.d.m_information._table + " where " + _g.d.m_information._table + "." + _g.d.m_information._ic_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " ) as " + _g.d.m_drugsconsultants_details._warning + ", (select " + _g.d.m_information._table + "." + _g.d.m_information._properties + " from " + _g.d.m_information._table + " where " + _g.d.m_information._table + "." + _g.d.m_information._ic_code + " = " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " ) as " + _g.d.m_drugsconsultants_details._properties + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __itemCode + "\'";
                    DataSet __result = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    if (__result.Tables.Count > 0)
                    {
                        DataTable __table0 = __result.Tables[0];
                        if (__table0.Rows.Count > 0)
                        {
                            //string __itemName = __result.Rows[0][_g.d.ic_inventory._name_1].ToString();

                            this._cellUpdate(row, _g.d.m_drugsconsultants_details._ic_name, __table0.Rows[0][_g.d.ic_inventory._name_1].ToString(), false);
                            this._cellUpdate(row, _g.d.m_drugsconsultants_details._unit_name, __table0.Rows[0][_g.d.ic_inventory._unit_cost].ToString(), false);
                            this._cellUpdate(row, _g.d.m_drugsconsultants_details._times, __table0.Rows[0][_g.d.m_drugsconsultants_details._times].ToString(), false);
                            this._cellUpdate(row, _g.d.m_drugsconsultants_details._frequency, __table0.Rows[0][_g.d.m_drugsconsultants_details._frequency].ToString(), false);
                            this._cellUpdate(row, _g.d.m_drugsconsultants_details._warning, __table0.Rows[0][_g.d.m_drugsconsultants_details._warning].ToString(), false);
                            this._cellUpdate(row, _g.d.m_drugsconsultants_details._properties, __table0.Rows[0][_g.d.m_drugsconsultants_details._properties].ToString(), false);

                            this._gridFind_m_information(row);
                        }
                    }
                    break;
            }

        }

        public void _gridFind_m_information(int row)
        {
            string __timesStr = this._cellGet(row, _g.d.m_drugsconsultants_details._times).ToString() ;
            string __frequencyStr = this._cellGet(row, _g.d.m_drugsconsultants_details._frequency).ToString();
            string __warningStr = this._cellGet(row, _g.d.m_drugsconsultants_details._warning).ToString();
            string __propertyStr = this._cellGet(row, _g.d.m_drugsconsultants_details._properties).ToString();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            // get times
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_times._name_1 + " from " + _g.d.m_times._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_times._code) + "= \'" + __timesStr + "\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_frequency._name_1 + " from " + _g.d.m_frequency._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_frequency._code) + "= \'" + __frequencyStr + "\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_warning._name_1 + " from " + _g.d.m_warning._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_warning._icode) + "= \'" + __warningStr + "\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.m_properties._name_1 + " from " + _g.d.m_properties._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_properties._code) + "= \'" + __propertyStr + "\'"));
            __query.Append("</node>");

            string __queryStr = __query.ToString();

            ArrayList __dataResult = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);

            DataTable __t0 = ((DataSet)__dataResult[0]).Tables[0];
            DataTable __t1 = ((DataSet)__dataResult[1]).Tables[0];
            DataTable __t2 = ((DataSet)__dataResult[2]).Tables[0];
            DataTable __t3 = ((DataSet)__dataResult[3]).Tables[0];

            if (__t0.Rows.Count > 0)
            {
                string __timesName = __t0.Rows[0][_g.d.m_times._name_1].ToString();
                if (__timesName.Length > 0)
                {
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._times, string.Concat(__timesStr, "~", __timesName), false);
                }
            }
            if (__t1.Rows.Count > 0)
            {
                string __frequencyName = __t1.Rows[0][_g.d.m_frequency._name_1].ToString();
                if (__frequencyName.Length > 0)
                {
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._frequency, string.Concat(__frequencyStr, "~", __frequencyName), false);
                }
            }
            if (__t2.Rows.Count > 0)
            {
                string __warningName = __t2.Rows[0][_g.d.m_warning._name_1].ToString();
                if (__warningName.Length > 0)
                {
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._warning, string.Concat(__warningStr, "~", __warningName), false);
                }
            }
            if (__t3.Rows.Count > 0)
            {
                string __propertyName = __t3.Rows[0][_g.d.m_properties._name_1].ToString();
                if (__propertyName.Length > 0)
                {
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._properties, string.Concat(__propertyStr, "~", __propertyName), false);
                }
            }
        }

        bool _screen_ar_grid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            bool __result = false;
            switch (_controlName)
            {
                case _controlTypeEnumGrid.drugsConsultants_details:
                    __result = ((((string)this._cellGet(row, _g.d.m_drugsconsultants_details._ic_code)).Length == 0) ? false : true);
                    break;
            }
            return __result;
        }

        void _screen_ar_grid__afterAddRow(object sender, int row)
        {
            switch (_controlName)
            {
                case _controlTypeEnumGrid.drugsConsultants_details:
                    //    this._cellUpdate(row, _g.d.m_drugsconsultants_details._line_number, row, false);
                    break;
            }
        }

        void _arCustomerContactGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string _searchName = e._columnName;//this._cellGet(this._selectRow, this._selectColumn).ToString();
            //string _searchName = ((MyLib._myGrid)sender)...ToLower();
            string _search_text_new = _search_screen_neme(_searchName);
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                _search_data_full = new MyLib._searchDataFull();
                _search_data_full._name = _search_text_new;
                _search_data_full._dataList._loadViewFormat(_search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_data_full._dataList._refreshData();
                _search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                _search_data_full._dataList._loadViewData(0);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _search_data_full);
        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }

        void _searchGridByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchGridAll(__getParent2._name, row);
            SendKeys.Send("{ENTER}");
        }

        void _searchGridAll(string name, int row)
        {

            if (name.CompareTo(_g.g._search_screen_ic_inventory) == 0)
            {
                string result = (string)_search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _search_data_full.Close();
                    this._cellUpdate(this._selectRow, _g.d.ar_item_by_customer._ic_code, result, true);
                    _searchAndWarning(this._selectRow);
                }
            }

        }

        public void _searchAndWarning(int row)
        {
            // ค้นหารายการหน่วยนับ
            string _ic_code = "";
            string __query = "";
            DataSet __getData = null;
            string __unitName = "";
            string __properties = "";
            string __dozen = "";
            string __times = "";
            string __ic_name = "";
            string __warning = "";

            switch (_controlName)
            {
                case _controlTypeEnumGrid.drugsConsultants_details:

                    _ic_code = this._cellGet(row, _g.d.m_drugsconsultants_details._ic_code).ToString();
                    __query = " select ic_code,properties ||'~'||(select name_1 from m_properties where code =properties)as properties,dozen ||'~'||(select name_1 from m_dozen where icode =dozen) as dozen,warning ||'~'||(select name_1 from m_warning where icode =warning) as warning,times ||'~'||(select name_1 from m_times where code =times) as times,frequency ||'~'||(select name_1 from m_frequency where code =frequency) as frequency ,(select  name_1 from ic_unit where code = (select unit_standard from ic_inventory where code = \'" + _ic_code.ToUpper() + "\')) as unit_name,(select name_1 from ic_inventory where code =\'" + _ic_code.ToUpper() + "\') as ic_name from m_information  where " + MyLib._myGlobal._addUpper(_g.d.m_drugsconsultants_details._ic_code) + "=\'" + _ic_code.ToUpper() + "\'";
                    __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    __unitName = "";
                    if (__getData.Tables.Count > 0)
                    {

                        int __intproperties = __getData.Tables[0].Columns.IndexOf(_g.d.m_information._properties);
                        int __intic_code = __getData.Tables[0].Columns.IndexOf(_g.d.m_information._ic_code);
                        int __intdozen = __getData.Tables[0].Columns.IndexOf(_g.d.m_information._dozen);
                        int __inttimes = __getData.Tables[0].Columns.IndexOf(_g.d.m_information._times);
                        int __intfrequency = __getData.Tables[0].Columns.IndexOf(_g.d.m_information._frequency);
                        int __intunit_name = __getData.Tables[0].Columns.IndexOf(_g.d.m_drugsconsultants_details._unit_name);
                        int __intic_name = __getData.Tables[0].Columns.IndexOf(_g.d.m_drugsconsultants_details._ic_name);
                        int __intwarning = __getData.Tables[0].Columns.IndexOf(_g.d.m_drugsconsultants_details._warning);
                        if (__intic_code != -1)
                        {
                            _ic_code = __getData.Tables[0].Rows[0].ItemArray.GetValue(__intic_code).ToString();

                        }
                        else
                        {
                            _ic_code = "";
                        }
                        if (__intproperties != -1)
                        {
                            __properties = __getData.Tables[0].Rows[0].ItemArray.GetValue(__intproperties).ToString();

                        }

                        if (__intdozen != -1)
                        {
                            __dozen = __getData.Tables[0].Rows[0].ItemArray.GetValue(__intdozen).ToString();

                        }

                        if (__inttimes != -1)
                        {
                            __times = __getData.Tables[0].Rows[0].ItemArray.GetValue(__inttimes).ToString();

                        }

                        if (__intunit_name != -1)
                        {
                            __unitName = __getData.Tables[0].Rows[0].ItemArray.GetValue(__intunit_name).ToString();

                        }

                        if (__intic_name != -1)
                        {
                            __ic_name = __getData.Tables[0].Rows[0].ItemArray.GetValue(__intic_name).ToString();

                        }

                        if (__intwarning != -1)
                        {
                            __warning = __getData.Tables[0].Rows[0].ItemArray.GetValue(__intwarning).ToString();

                        }

                    }
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._ic_code, _ic_code, false);
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._frequency, __dozen, false);
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._ic_name, __ic_name, false);
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._properties, __properties, false);
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._times, __times, false);
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._warning, __warning, false);
                    this._cellUpdate(row, _g.d.m_drugsconsultants_details._unit_name, __unitName, true);
                    break;
            }

        }


        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;

            if (name.CompareTo(_g.g._search_screen_ic_inventory) == 0)
            {
                _search_data_full.Close();
                //this._cellUpdate(this._selectRow, _g.d.ar_item_by_customer._ic_code, e._text, true);
                this._cellUpdate(this._selectRow, _g.d.m_drugsconsultants_details._ic_code, e._text, true);
                //_searchAndWarning(this._selectRow);
                //this.Invalidate();
                SendKeys.Send("{TAB}");
            }
        }

        MyLib._myGridMoveColumnType _arCustomerContactGrid__moveNextColumn(MyLib._myGrid sender, int newRow, int newColumn)
        {
            MyLib._myGridMoveColumnType __result = new MyLib._myGridMoveColumnType();
            switch (_controlName)
            {
                case _controlTypeEnumGrid.drugsConsultants_details:


                    break;
            }

            __result._newColumn = newColumn;
            __result._newRow = newRow;
            return __result;
        }


        string _search_screen_neme(string _name)
        {
            switch (_name)
            {

                case "ic_code": return _g.g._search_screen_ic_inventory;

            }
            return "";
        }


    }

    public enum _controlTypeEnumGrid
    {
        /// <summary>
        /// ArContactorGrid : ผู้ประวัติการให้คำปรึกษาการใช้ยา
        /// </summary>
        drugsConsultants_details,
    }
}
