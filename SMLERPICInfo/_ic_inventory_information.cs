using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPICInfo
{
    public partial class _ic_inventory_information : UserControl
    {
        private string _itemCode;
        private int _unitType = 0;
        private DataTable _itemPacking = null;
        private SMLERPControl._ic._icPriceFormulaDetail _icPriceFormula;
        private DataTable _lotDatatable = null;

        public _ic_inventory_information()
        {
            InitializeComponent();
            this._build();
            this._loadData();
        }

        void _build()
        {
            this._icGrid._isEdit = false;
            this._icGrid._table_name = _g.d.ic_inventory._table;
            this._icGrid._width_by_persent = true;
            this._icGrid._addColumn(_g.d.ic_inventory._code, 1, 25, 25);
            this._icGrid._addColumn(_g.d.ic_inventory._name_1, 1, 75, 75);
            this._icGrid._mouseClick += _icGrid__mouseClick;
            this._icGrid._calcPersentWidthToScatter();

            this._icmainScreenTop.Enabled = false;

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            this._icmainScreenTop.Enabled = false;
            this._icmainScreenTop.BorderStyle = BorderStyle.FixedSingle;
            //
            this._whBalanceGrid._getResource = true;
            this._whBalanceGrid._table_name = _g.d.ic_resource._table;
            this._whBalanceGrid._addColumn(_g.d.ic_resource._warehouse, 1, 20, 20);
            this._whBalanceGrid._addColumn(_g.d.ic_resource._qty, 3, 20, 20, true, false, true, false, __formatNumberQty);
            this._whBalanceGrid._addColumn(_g.d.ic_resource._balance_qty, 1, 20, 60);
            this._whBalanceGrid._calcPersentWidthToScatter();
            this._whBalanceGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_whBalanceGrid__beforeDisplayRow);
            this._whBalanceGrid._isEdit = false;
            //
            this._whShelfBalanceGrid._getResource = true;
            this._whShelfBalanceGrid._table_name = _g.d.ic_resource._table;
            this._whShelfBalanceGrid._addColumn(_g.d.ic_resource._warehouse, 1, 20, 20);
            this._whShelfBalanceGrid._addColumn(_g.d.ic_resource._location, 1, 20, 20);
            this._whShelfBalanceGrid._addColumn(_g.d.ic_resource._qty, 3, 20, 30, true, false, true, false, __formatNumberQty);
            this._whShelfBalanceGrid._addColumn(_g.d.ic_resource._balance_qty, 1, 20, 40);
            this._whShelfBalanceGrid._calcPersentWidthToScatter();
            this._whShelfBalanceGrid._isEdit = false;
            this._whShelfBalanceGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_whShelfBalanceGrid__beforeDisplayRow);
            //
            this._gridSerial._getResource = true;
            this._gridSerial._table_name = _g.d.ic_serial._table;
            this._gridSerial._addColumn(_g.d.ic_serial._serial_number, 1, 20, 40);
            this._gridSerial._addColumn(_g.d.ic_serial._ic_unit, 1, 20, 20);
            this._gridSerial._addColumn(_g.d.ic_serial._wh_code, 1, 20, 20);
            this._gridSerial._addColumn(_g.d.ic_serial._shelf_code, 1, 20, 20);
            this._gridSerial._calcPersentWidthToScatter();
            this._gridSerial._isEdit = false;

            //
            this._itemUnitGrid._getResource = true;
            this._itemUnitGrid._isEdit = false;
            this._itemUnitGrid._total_show = false;
            this._itemUnitGrid._table_name = _g.d.ic_unit_use._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._itemUnitGrid._addColumn(_g.d.ic_unit_use._code, 1, 10, 20, true, false, true, true);
            this._itemUnitGrid._addColumn(_g.d.ic_unit_use._name_1, 1, 0, 40, false, false, false, false);
            this._itemUnitGrid._addColumn(_g.d.ic_unit_use._stand_value, 3, 0, 20, true, false, true, true, __formatNumber);
            this._itemUnitGrid._addColumn(_g.d.ic_unit_use._divide_value, 3, 0, 20, true, false, true, true, __formatNumber);
            this._itemUnitGrid._calcPersentWidthToScatter();
            //
            this._icPriceFormula = new SMLERPControl._ic._icPriceFormulaDetail();
            this._icPriceFormula.Dock = DockStyle.Fill;
            this._icPriceFormula._createGrid();
            this._icPriceFormula._toolStrip.Visible = false;
            this._icPriceFormula._grid._isEdit = false;
            this._icPriceFormula._gridResult._isEdit = false;
            this._pricePanel.Controls.Add(this._icPriceFormula);

            this._barcodeGrid._isEdit = false;
            this._barcodeGrid._total_show = false;
            this._barcodeGrid._table_name = _g.d.ic_inventory_barcode._table;
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._barcode, 1, 20, 25, true, false, true, false);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._unit_code, 1, 5, 15, false, false, true, true);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price_member, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price_2, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price_member_2, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price_3, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price_member_3, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price_4, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._addColumn(_g.d.ic_inventory_barcode._price_member_4, 3, 0, 15, true, false, true, false, __formatNumberPrice);
            this._barcodeGrid._calcPersentWidthToScatter();

            this._lotBalanceGrid._isEdit = false;
            this._lotBalanceGrid._table_name = _g.d.ic_resource._table;

            this._lotBalanceGrid._addColumn("wh_code", 1, 10, 8, false, false, false, false, "", "", "", _g.d.ic_resource._warehouse);

            this._lotBalanceGrid._addColumn(_g.d.ic_resource._lot_number, 1, 5, 20);
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._qty_in, 3, 10, 20, false, false, false, false, __formatNumberQty, "", "", "", _g.d.ic_resource._receive);
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._qty_out, 3, 10, 20, false, false, false, false, __formatNumberQty, "", "", "", _g.d.ic_resource._send);
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 20, false, false, false, false, __formatNumberQty);
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._ic_unit_code, 1, 10, 10, false, false, false);

            // this._lotBalanceGrid._addColumn(_g.d.ic_resource._receive_date, 4, 10, 20, false, false, false);

            this._lotBalanceGrid._addColumn(_g.d.ic_resource._doc_date, 4, 255, 20, false, false, false, false, "dd/MM/yyyy", "", "", _g.d.ic_resource._receive_date);
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._mfd_date, 4, 10, 20, false, false, false, false, "dd/MM/yyyy");
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._date_expire, 4, 10, 20, false, false, false, false, "dd/MM/yyyy");
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._mfn_name, 1, 10, 40, false, false, false);


            this._lotBalanceGrid._calcPersentWidthToScatter();
            this._searchLotTextbox.textBox.KeyUp += _searchLotTextbox_KeyUp;

        }

        void _icGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __getItemCode = this._icGrid._cellGet(e._row, _g.d.ic_inventory._code).ToString();
            this._load(__getItemCode);
        }

        MyLib.BeforeDisplayRowReturn _whShelfBalanceGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            if (columnName.Equals(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty))
            {
                decimal __qty = MyLib._myGlobal._decimalPhase(sender._cellGet(row, _g.d.ic_resource._qty).ToString());
                ((ArrayList)senderRow.newData)[columnNumber] = this._packingWord(__qty);
            }
            return senderRow;
        }

        MyLib.BeforeDisplayRowReturn _whBalanceGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            if (columnName.Equals(_g.d.ic_resource._table + "." + _g.d.ic_resource._balance_qty))
            {
                decimal __qty = MyLib._myGlobal._decimalPhase(sender._cellGet(row, _g.d.ic_resource._qty).ToString());
                ((ArrayList)senderRow.newData)[columnNumber] = this._packingWord(__qty);
            }
            return senderRow;
        }

        string _packingWord(decimal qty)
        {
            try
            {
                List<_g.g._convertPackingWordClass> __packing = new List<_g.g._convertPackingWordClass>();
                DataRow[] __selectPacking = this._itemPacking.Select(_g.d.ic_unit_use._row_order + " > 0", _g.d.ic_unit_use._row_order + " desc");
                for (int __loop = 0; __loop < __selectPacking.Length; __loop++)
                {
                    _g.g._convertPackingWordClass __newData = new _g.g._convertPackingWordClass();
                    __newData._unitCode = __selectPacking[__loop][_g.d.ic_unit_use._code].ToString();
                    __newData._unitName = __selectPacking[__loop][_g.d.ic_unit_use._name_1].ToString();
                    __newData._standValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._stand_value].ToString());
                    __newData._divideValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._divide_value].ToString());
                    __packing.Add(__newData);
                }
                return _g.g._convertPackingWord(__packing, qty, false);
            }
            catch
            {
                return qty.ToString();
            }
        }

        public void _load(string itemCode)
        {
            this._itemCode = itemCode;
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            // ตามคลัง
            SMLProcess._icProcess __process = new SMLProcess._icProcess();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // ดึงสินค้า
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._itemCode + "\'"));
            // ตามคลัง
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__process._queryItemBalance(this._itemCode, _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse, _g.d.ic_resource._qty, _g.d.ic_trans_detail._wh_code, "")));
            // หน่วยนับ
            //string __calcPack = "/(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard)";
            string __queryUnit = "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + "{0})";
            string __queryPacking = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_unit_use._code, String.Format(__queryUnit, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code) + " as " + _g.d.ic_unit_use._name_1, "coalesce(" + _g.d.ic_unit_use._row_order + ",1) as " + _g.d.ic_unit_use._row_order, _g.d.ic_unit_use._stand_value, _g.d.ic_unit_use._divide_value) + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._status + "=1 and " + _g.d.ic_unit_use._ic_code + "=\'" + this._itemCode + "\' order by " + _g.d.ic_unit_use._ratio;
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryPacking));
            // ราคาตามสูตร
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + itemCode + "\' order by " + _g.d.ic_inventory_price_formula._unit_code));
            //
            // serial list
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_serial._status + "=0 order by " + _g.d.ic_serial._serial_number));

            // barcode and price
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._ic_code + "=\'" + this._itemCode + "\'"));

            // ยอดคงเหลือตาม LOT
            SMLERPControl._icInfoProcess __icInfoProcess = new SMLERPControl._icInfoProcess();
            // __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__icInfoProcess._stkStockInfoAndBalanceByLotQuery(_g.g._productCostType.ปรกติ, null, itemCode, itemCode, DateTime.Now, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT)));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__icInfoProcess._stkLotInfoAndBalanceQuery(null, this._itemCode, this._itemCode, "", "", true, false).Replace(") as final order by sort_order,doc_date,doc_time,lot_number", ") as final order by wh_code, lot_number, doc_date, balance_qty")));

            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._icmainScreenTop._clear();
            this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
            this._icmainScreenTop._search(true);
            //
            this._whBalanceGrid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
            this._whBalanceGrid.Invalidate();
            //
            this._unitType = (int)MyLib._myGlobal._decimalPhase((((DataSet)__getData[0]).Tables[0]).Rows[0][_g.d.ic_inventory._unit_type].ToString());
            this._itemPacking = ((DataSet)__getData[2]).Tables[0];
            this._itemUnitGrid._loadFromDataTable(this._itemPacking);
            //
            this._icPriceFormula._grid._loadFromDataTable(((DataSet)__getData[3]).Tables[0]);
            this._icPriceFormula._calc();
            //
            if (this._whBalanceGrid._rowData.Count > 0)
            {
                string __getWhCode = this._whBalanceGrid._cellGet(0, _g.d.ic_resource._warehouse).ToString();
                this._loadByLocation();
            }
            else
            {
                this._whShelfBalanceGrid._clear();
            }

            this._gridSerial._loadFromDataTable(((DataSet)__getData[4]).Tables[0]);
            this._barcodeGrid._loadFromDataTable(((DataSet)__getData[5]).Tables[0]);

            this._lotDatatable = ((DataSet)__getData[6]).Tables[0];
            this._lotBalanceGrid._loadFromDataTable(_lotDatatable);

        }

        private void _loadByLocation()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            // ตามคลัง+ที่เก็บ
            SMLProcess._icProcess __process = new SMLProcess._icProcess();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            string __queryBalanceByLocation = __process._queryItemBalance(this._itemCode, _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse + "," + _g.d.ic_trans_detail._shelf_code + " as " + _g.d.ic_resource._location, _g.d.ic_resource._qty, _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code, "");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBalanceByLocation));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._whShelfBalanceGrid._loadFromDataTable(((DataSet)__getData[0]).Tables[0]);
            this._whShelfBalanceGrid.Invalidate();
        }



        void _loadData()
        {
            string __searchTextTrim = this._searchTextbox.textBox.Text.Trim();
            string[] __searchTextSplit = __searchTextTrim.Split(' ');

            // ประกอบ where
            StringBuilder __where = new StringBuilder();
            if (__searchTextSplit.Length > 1)
            {
                // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                for (int __loop = 0; __loop < this._icGrid._columnList.Count; __loop++)
                {
                    bool __whereFirst = false;
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._icGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณี ป้อนการค้นหาหลายคำ (เว้น spacebar)
                            bool __first2 = false;
                            for (int __searchIndex = 0; __searchIndex < __searchTextSplit.Length; __searchIndex++)
                            {
                                if (__searchTextSplit[__searchIndex].Length > 0)
                                {
                                    string __getValue = __searchTextSplit[__searchIndex].ToUpper();
                                    string __newDateValue = __getValue;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            //
                                            decimal __newValue = 0M;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = MyLib._myGlobal._decimalPhase(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == false)
                                                        {
                                                            if (__where.Length > 0)
                                                            {
                                                                __where.Append(" or ");
                                                            }
                                                            __where.Append("(");
                                                            __whereFirst = true;
                                                        }
                                                        if (__first2)
                                                        {
                                                            __where.Append(" and ");
                                                        }
                                                        __first2 = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == false)
                                                    {
                                                        if (__where.Length > 0)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __where.Append("(");
                                                        __whereFirst = true;
                                                    }
                                                    if (__first2)
                                                    {
                                                        __where.Append(" and ");
                                                    }
                                                    __first2 = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            if (__whereFirst == false)
                                            {
                                                if (__where.Length > 0)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __where.Append("(");
                                                __whereFirst = true;
                                            }
                                            if (__first2)
                                            {
                                                __where.Append(" and ");
                                            }
                                            __first2 = true;
                                            //
                                            //if (this._addQuotWhere)
                                            //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                            //else
                                            __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                            if (this._searchTextbox.textBox.Text[0] == '+')
                                            {
                                                __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                            }
                                            else
                                            {
                                                __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                            }
                                            break;
                                    }
                                }
                            }
                            if (__whereFirst)
                            {
                                __where.Append(")");
                            }
                        }
                    }
                } // for
            }
            else
            {
                bool __whereFirst = false;
                for (int __loop = 0; __loop < this._icGrid._columnList.Count; __loop++)
                {
                    MyLib._myGrid._columnType __getColumnType = (MyLib._myGrid._columnType)this._icGrid._columnList[__loop];
                    if (__getColumnType._originalName.Length > 0 && __getColumnType._isQuery == true && __getColumnType._isColumnFilter == true)
                    {
                        if (__getColumnType._isHide == false)
                        {
                            // กรณีการค้นหาตัวเดียว
                            if (this._searchTextbox.textBox.Text.Length > 0)
                            {
                                try
                                {
                                    string __getValue = this._searchTextbox.textBox.Text;
                                    string __newDateValue = __getValue;
                                    Boolean __valueExtra = false;
                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                    {
                                        __newDateValue = __getValue.ToString().Remove(0, 1);
                                        __valueExtra = true;
                                    }
                                    switch (__getColumnType._type)
                                    {
                                        case 2: // Number
                                        case 3:
                                        case 5:
                                            double __newValue = 0;
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    __newValue = Double.Parse(__newDateValue);
                                                    //
                                                    if (__newValue != 0)
                                                    {
                                                        if (__whereFirst == true)
                                                        {
                                                            __where.Append(" or ");
                                                        }
                                                        __whereFirst = true;
                                                        //
                                                        //if (this._addQuotWhere)
                                                        //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                        //else
                                                        __where.Append(string.Concat(__getColumnType._query));
                                                        //
                                                        if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                            __getValue = __getValue[0].ToString() + __newValue.ToString();
                                                        else
                                                            __getValue = String.Concat("=", __newValue.ToString());
                                                        __where.Append(__getValue);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 4: // Date
                                            try
                                            {
                                                if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                {
                                                    DateTime __test = DateTime.Parse(__newDateValue, MyLib._myGlobal._cultureInfo());
                                                    //
                                                    if (__whereFirst == true)
                                                    {
                                                        __where.Append(" or ");
                                                    }
                                                    __whereFirst = true;
                                                    //
                                                    //if (this._addQuotWhere)
                                                    //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                    //else
                                                    __where.Append(string.Concat(__getColumnType._query));
                                                    DateTime __newDate = MyLib._myGlobal._convertDate(__getValue);
                                                    if (__getValue[0] == '=' || __getValue[0] == '>' || __getValue[0] == '<')
                                                    {
                                                        __newDate = MyLib._myGlobal._convertDate(__newDateValue);
                                                        __where.Append(string.Concat(__getValue[0].ToString(), "\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                    else
                                                    {
                                                        __where.Append(string.Concat("=\'", MyLib._myGlobal._convertDateToQuery(__newDate), "\'"));
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        case 11:
                                            {

                                            }
                                            break;
                                        default:// String
                                            //
                                            if (__valueExtra == false)
                                            {
                                                if (__whereFirst == true)
                                                {
                                                    __where.Append(" or ");
                                                }
                                                __whereFirst = true;
                                                //
                                                //if (this._addQuotWhere)
                                                //    __where.Append(string.Concat("\"" + __getColumnType._query + "\""));
                                                //else
                                                __where.Append(string.Concat("upper(" + __getColumnType._query + ")"));
                                                if (this._searchTextbox.textBox.Text[0] == '+')
                                                {
                                                    __where.Append(string.Concat(" like \'", __newDateValue.Remove(0, 1).ToUpper(), "%\'"));
                                                }
                                                else
                                                {
                                                    __where.Append(string.Concat(" like \'%", __newDateValue.ToUpper(), "%\'"));
                                                }
                                            }
                                            break;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                } // for
            }
            if (__where.Length > 0)
            {
                __where = new StringBuilder("(" + __where.ToString() + ")");
            }

            StringBuilder __query = new StringBuilder();
            __query.Append("select code, name_1 from ic_inventory ");

            if (__where.Length > 0)
            {
                __query.Append(" where " + __where.ToString());
            }
            __query.Append(" order by code ");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort(__query.ToString()).Tables[0];
            this._icGrid._loadFromDataTable(__result);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                this._timer.Stop();
                this._timer.Start();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        string _oldText = "";

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                if (_searchAuto.Checked)
                {
                    if (_oldText.CompareTo(this._searchTextbox.textBox.Text) != 0)
                    {
                        _oldText = this._searchTextbox.textBox.Text;
                        this._loadData();
                    }
                }
            }
        }

        private void _searchLotTextbox_KeyUp(object sender, KeyEventArgs e)
        {

            if (this._lotDatatable != null)
            {

                // filter in lot
                if (this._searchLotTextbox.textBox.Text.Length > 0)
                {
                    // start searcg
                    string __querySearch = _g.d.ic_resource._lot_number + " like \'%" + this._searchLotTextbox.textBox.Text + "%\'";
                    DataRow[] __row = this._lotDatatable.Select(__querySearch);
                    this._lotBalanceGrid._loadFromDataTable(this._lotDatatable, __row);

                }
                else
                {
                    this._lotBalanceGrid._loadFromDataTable(this._lotDatatable);
                }
            }

        }
    }
}
