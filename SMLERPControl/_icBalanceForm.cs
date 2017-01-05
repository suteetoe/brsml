using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _icBalanceForm : Form
    {
        public delegate void WareHouseLocationHandler(string whCode, string locationCode);
        //
        private string _itemCode;
        private int _unitType = 0;
        private DataTable _itemPacking = null;
        public SMLERPControl._ic._icPriceFormulaDetail _icPriceFormula;
        //
        public event WareHouseLocationHandler _getWarehouseLocation;


        public _icBalanceForm()
        {
            InitializeComponent();
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
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
            if (MyLib._myGlobal._programName.Equals("SML CM"))
            {
                this._whShelfBalanceGrid._addColumn(_g.d.ic_resource._book_out_qty, 3, 20, 30, true, false, true, false, __formatNumberQty);
                this._whShelfBalanceGrid._addColumn(_g.d.ic_resource._accrued_out_qty, 3, 20, 30, true, false, true, false, __formatNumberQty);
                this._whShelfBalanceGrid._addColumn(_g.d.ic_resource._purchase_balance_qty, 3, 20, 30, true, false, true, false, __formatNumberQty);

            }

            this._whShelfBalanceGrid._calcPersentWidthToScatter();
            this._whShelfBalanceGrid._isEdit = false;
            this._whShelfBalanceGrid._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_whShelfBalanceGrid__beforeDisplayRow);
            this._whShelfBalanceGrid._mouseClick += _whShelfBalanceGrid__mouseClick;
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
            this._icPriceFormula = new _ic._icPriceFormulaDetail();
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
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._lot_number, 1, 5, 60);
            this._lotBalanceGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 40, false, false, false, false, __formatNumberQty);
            this._lotBalanceGrid._calcPersentWidthToScatter();

        }

        private void _whShelfBalanceGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (this._getWarehouseLocation != null)
            {
                String __whCode = this._whShelfBalanceGrid._cellGet(this._whShelfBalanceGrid._selectRow, _g.d.ic_resource._warehouse).ToString();
                String __locationCode = this._whShelfBalanceGrid._cellGet(this._whShelfBalanceGrid._selectRow, _g.d.ic_resource._location).ToString();
                this._getWarehouseLocation(__whCode, __locationCode);
            }
        }

        public void _clear()
        {
            this._itemCode = "";
            this._webBrowser.DocumentText = "";
            //
            this._whBalanceGrid._clear();
            //
            this._itemUnitGrid._clear();
            //
            this._icPriceFormula._grid._clear();
            //
            this._whBalanceGrid._clear();
            this._whShelfBalanceGrid._clear();

            this._gridSerial._clear();
            this._barcodeGrid._clear();

            this._lotBalanceGrid._clear();
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
            // ดึงสินค้า+ยอดคงเหลือ
            string __fieldBalanceQtyByCost = "balance_qty_cost";
            string __queryUnit = "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + "{0})";
            string __calcPack = "/(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard)";
            string __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._name_1, String.Format(__queryUnit + "||\'(\'||{0}||\')\'", _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard) + " as " + _g.d.ic_inventory._unit_standard, _g.d.ic_inventory._balance_qty + " as " + __fieldBalanceQtyByCost, _g.d.ic_inventory._balance_qty + __calcPack + " as " + _g.d.ic_inventory._balance_qty, _g.d.ic_inventory._book_out_qty + __calcPack + " as " + _g.d.ic_inventory._book_out_qty, _g.d.ic_inventory._accrued_in_qty + __calcPack + " as " + _g.d.ic_inventory._accrued_in_qty, _g.d.ic_inventory._accrued_out_qty + __calcPack + " as " + _g.d.ic_inventory._accrued_out_qty, _g.d.ic_inventory._unit_type) + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemCode + "\'";
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
            // ตามคลัง
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__process._queryItemBalance(this._itemCode, _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse, _g.d.ic_resource._qty, _g.d.ic_trans_detail._wh_code, "")));
            // หน่วยนับ
            //string __calcPack = "/(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard)";
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
            _icInfoProcess __icInfoProcess = new _icInfoProcess();
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__icInfoProcess._stkStockInfoAndBalanceByLotQuery(_g.g._productCostType.ปรกติ, null, itemCode, itemCode, DateTime.Now, true, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT)));

            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            //
            {
                StringBuilder __html = new StringBuilder(@"
    <html><head><style>body {
	font-family: tahoma, verdana, geneva, arial, helvetica, sans-serif;
	font-size: 9pt;
    color='#585858';
	margin: 2pt;
	margin-bottom: 0pt;
	margin-left: 2pt;
	margin-right: 0pt;
	margin-top: 2pt;	
    }</style></head><body bgcolor='#E0F8F7'>");
                DataTable __dt = ((DataSet)__getData[0]).Tables[0];
                if (__dt.Rows.Count > 0)
                {
                    decimal __balanceQtyByCost = MyLib._myGlobal._decimalPhase(__dt.Rows[0][__fieldBalanceQtyByCost].ToString());
                    decimal __balanceQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._balance_qty].ToString());
                    decimal __bookOutQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._book_out_qty].ToString());
                    decimal __accruedInQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._accrued_in_qty].ToString());
                    decimal __accruedOutQty = MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._accrued_out_qty].ToString());
                    decimal __availableQty = __balanceQty - __accruedOutQty;
                    int __unitType = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.ic_inventory._unit_type].ToString());
                    __html.Append("<table width = '100%'>");
                    __html.Append("<tr>");
                    __html.Append("<td colspan='3'>");
                    __html.Append("<b><font color='black'>" + __dt.Rows[0][_g.d.ic_inventory._name_1].ToString() + "</font></b>");
                    __html.Append("</td>");
                    __html.Append("</tr>");

                    __html.Append("<tr>");
                    __html.Append("<td>");
                    __html.Append(MyLib._myGlobal._resource("คงเหลือ"));
                    __html.Append("</td>");
                    __html.Append("<td align='right'>");
                    __html.Append("<font color='green'><b>");
                    __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __balanceQty));
                    __html.Append("&nbsp;</b></font></td>");
                    __html.Append("<td>");
                    __html.Append("<font color='black'><b>");
                    __html.Append(__dt.Rows[0][_g.d.ic_inventory._unit_standard].ToString());
                    __html.Append("</b></font>&nbsp;");
                    __html.Append("</td>");
                    __html.Append("</tr>");

                    if (__accruedInQty != 0.0M)
                    {
                        __html.Append("<tr>");
                        __html.Append("<td>");
                        __html.Append(MyLib._myGlobal._resource("ค้างรับ"));
                        __html.Append("</td>");
                        __html.Append("<td align='right'>");
                        __html.Append("<font color='green'><b>");
                        __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __accruedInQty));
                        __html.Append("</b></font></b>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("<td>");
                        __html.Append("<font color='black'><b>");
                        __html.Append(__dt.Rows[0][_g.d.ic_inventory._unit_standard].ToString());
                        __html.Append("</b></font>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("</tr>");
                    }

                    if (__bookOutQty != 0.0M)
                    {
                        __html.Append("<tr>");
                        __html.Append("<td>");
                        __html.Append(MyLib._myGlobal._resource("ค้างจอง"));
                        __html.Append("</td>");
                        __html.Append("<td align='right'>");
                        __html.Append("<font color='red'><b>");
                        __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __bookOutQty));
                        __html.Append("</b></font></b>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("<td>");
                        __html.Append("<font color='black'><b>");
                        __html.Append(__dt.Rows[0][_g.d.ic_inventory._unit_standard].ToString());
                        __html.Append("</b></font>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("</tr>");
                    }
                    if (__accruedOutQty != 0.0M)
                    {
                        __html.Append("<tr>");
                        __html.Append("<td>");
                        __html.Append(MyLib._myGlobal._resource("ค้างส่ง"));
                        __html.Append("</td>");
                        __html.Append("<td align='right'>");
                        __html.Append("<font color='red'><b>");
                        __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __accruedOutQty));
                        __html.Append("</b></font></b>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("<td>");
                        __html.Append("<font color='black'><b>");
                        __html.Append(__dt.Rows[0][_g.d.ic_inventory._unit_standard].ToString());
                        __html.Append("</b></font>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("</tr>");
                    }
                    if (__availableQty != __balanceQty)
                    {
                        __html.Append("<tr>");
                        __html.Append("<td>");
                        __html.Append(MyLib._myGlobal._resource("ยอดที่ขายได้"));
                        __html.Append("</td>");
                        __html.Append("<td align='right'>");
                        __html.Append("<font color='blue'><b>");
                        __html.Append(MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_qty_decimal, __availableQty));
                        __html.Append("</b></font></b>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("<td>");
                        __html.Append("<font color='black'><b>");
                        __html.Append(__dt.Rows[0][_g.d.ic_inventory._unit_standard].ToString());
                        __html.Append("</b></font>&nbsp;");
                        __html.Append("</td>");
                        __html.Append("</tr>");
                    }
                    __html.Append("</table>");
                    if (__unitType == 1)
                    {
                        // หลายหน่วยนับ
                        ///string __queryPacking = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_unit_use._code, String.Format(__queryUnit, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code) + " as " + _g.d.ic_unit_use._name_1, _g.d.ic_unit_use._row_order, _g.d.ic_unit_use._stand_value, _g.d.ic_unit_use._divide_value) + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._status + "=1 and " + _g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\' order by " + _g.d.ic_unit_use._ratio;
                        DataTable __dtPacking = ((DataSet)__getData[2]).Tables[0]; //__myFrameWork._queryShort(__queryPacking).Tables[0];
                        if (__dtPacking.Rows.Count > 0)
                        {
                            StringBuilder __packingStr = new StringBuilder();
                            for (int __loop = 0; __loop < __dtPacking.Rows.Count; __loop++)
                            {
                                string __unitCode = __dtPacking.Rows[__loop][_g.d.ic_unit_use._code].ToString();
                                string __unitName = __dtPacking.Rows[__loop][_g.d.ic_unit_use._name_1].ToString();
                                if (__packingStr.Length != 0)
                                {
                                    __packingStr.Append(",");
                                }
                                if (__unitCode.Equals(__unitName))
                                {
                                    __packingStr.Append(__unitCode);
                                }
                                else
                                {
                                    __packingStr.Append(__unitName + "(" + __unitCode + ")");
                                }
                            }
                            __html.Append(MyLib._myGlobal._resource("หน่วยนับ") + ":&nbsp;<b><font color='black'>" + __packingStr.ToString() + "</font></b>&nbsp;");
                            //
                            try
                            {
                                List<_g.g._convertPackingWordClass> __packing = new List<_g.g._convertPackingWordClass>();
                                DataRow[] __selectPacking = __dtPacking.Select(_g.d.ic_unit_use._row_order + " > 0", _g.d.ic_unit_use._row_order + " desc");
                                for (int __loop = 0; __loop < __selectPacking.Length; __loop++)
                                {
                                    _g.g._convertPackingWordClass __newData = new _g.g._convertPackingWordClass();
                                    __newData._unitCode = __selectPacking[__loop][_g.d.ic_unit_use._code].ToString();
                                    __newData._unitName = __selectPacking[__loop][_g.d.ic_unit_use._name_1].ToString();
                                    __newData._standValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._stand_value].ToString());
                                    __newData._divideValue = MyLib._myGlobal._decimalPhase(__selectPacking[__loop][_g.d.ic_unit_use._divide_value].ToString());
                                    __packing.Add(__newData);
                                }
                                string __packingWord = _g.g._convertPackingWord(__packing, __balanceQtyByCost, true);
                                if (__packingWord.Length > 0)
                                {
                                    __html.Append(MyLib._myGlobal._resource("คงเหลือ") + ":&nbsp;<b><font color='black'>" + __packingWord + "</font></b>&nbsp;");
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                DataRow __item = (((DataSet)__getData[0]).Tables[0]).Rows[0];
                __html.Append("</body></html>");               
                this._webBrowser.DocumentText = __html.ToString();
            }
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

            this._lotBalanceGrid._loadFromDataTable(((DataSet)__getData[6]).Tables[0]);

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _loadByLocation()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            // ตามคลัง+ที่เก็บ
            
            SMLProcess._icProcess __process = new SMLProcess._icProcess();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            string __queryBalanceByLocation = __process._queryItemBalance(this._itemCode, _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse + "," + _g.d.ic_trans_detail._shelf_code + " as " + _g.d.ic_resource._location, _g.d.ic_resource._qty, _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code, "");

            if (MyLib._myGlobal._programName.Equals("SML CM"))
            {
                StringBuilder __queryGetBookOutQty = new StringBuilder();
                __queryGetBookOutQty.Append("coalesce( ((select sum(qty* (stand_value/divide_value)) from ic_trans_detail as reserved  where reserved.item_code = ic_trans_detail.item_code and reserved.wh_code = ic_trans_detail.wh_code and reserved.shelf_code = ic_trans_detail.shelf_code and reserved.trans_flag= 34 and reserved.last_status=0  )) - (select sum(qty* (stand_value/divide_value)) from ic_trans_detail as reduce  where reduce.item_code = ic_trans_detail.item_code and reduce.wh_code = ic_trans_detail.wh_code and reduce.shelf_code = ic_trans_detail.shelf_code and reduce.trans_flag in (44,36,39) and reduce.last_status=0 and ref_doc_no in (select doc_no from ic_trans_detail as reserved  where reserved.item_code = ic_trans_detail.item_code and reserved.wh_code = ic_trans_detail.wh_code and reserved.shelf_code = ic_trans_detail.shelf_code and reserved.trans_flag= 34 and reserved.last_status=0  ) ), 0) as " + _g.d.ic_resource._book_out_qty);

                StringBuilder __queryAccruedOutQty = new StringBuilder("");
                __queryAccruedOutQty.Append("coalesce( ((select sum(qty* (stand_value/divide_value)) from ic_trans_detail as reserved  where reserved.item_code = ic_trans_detail.item_code and reserved.wh_code = ic_trans_detail.wh_code and reserved.shelf_code = ic_trans_detail.shelf_code and reserved.trans_flag= 36 and reserved.last_status=0  )) - (select sum(qty* (stand_value/divide_value)) from ic_trans_detail as reduce  where reduce.item_code = ic_trans_detail.item_code and reduce.wh_code = ic_trans_detail.wh_code and reduce.shelf_code = ic_trans_detail.shelf_code and reduce.trans_flag in (44,37) and reduce.last_status=0 and ref_doc_no in (select doc_no from ic_trans_detail as reserved  where reserved.item_code = ic_trans_detail.item_code and reserved.wh_code = ic_trans_detail.wh_code and reserved.shelf_code = ic_trans_detail.shelf_code and reserved.trans_flag= 36 and reserved.last_status=0  ) ), 0) as " + _g.d.ic_resource._accrued_out_qty);

                StringBuilder __queryAccruedInQty = new StringBuilder();
                __queryAccruedInQty.Append("coalesce( ((select sum(qty* (stand_value/divide_value)) from ic_trans_detail as reserved  where reserved.item_code = ic_trans_detail.item_code and reserved.wh_code = ic_trans_detail.wh_code and reserved.shelf_code = ic_trans_detail.shelf_code and reserved.trans_flag= 6 and reserved.last_status=0  )) - (select sum(qty* (stand_value/divide_value)) from ic_trans_detail as reduce  where reduce.item_code = ic_trans_detail.item_code and reduce.wh_code = ic_trans_detail.wh_code and reduce.shelf_code = ic_trans_detail.shelf_code and reduce.trans_flag in (12,310,7) and reduce.last_status=0 and ref_doc_no in (select doc_no from ic_trans_detail as reserved  where reserved.item_code = ic_trans_detail.item_code and reserved.wh_code = ic_trans_detail.wh_code and reserved.shelf_code = ic_trans_detail.shelf_code and reserved.trans_flag= 6 and reserved.last_status=0  ) ), 0) as " + _g.d.ic_resource._purchase_balance_qty);

                __queryBalanceByLocation = __queryBalanceByLocation.Replace(" as qty ", " as qty ," + __queryGetBookOutQty.ToString() + ", " + __queryAccruedOutQty.ToString() + ", " + __queryAccruedInQty.ToString() + " ");
            }

            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBalanceByLocation));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._whShelfBalanceGrid._loadFromDataTable(((DataSet)__getData[0]).Tables[0]);
            this._whShelfBalanceGrid.Invalidate();
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
    }
}
