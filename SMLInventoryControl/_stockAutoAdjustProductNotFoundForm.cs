using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.Globalization;

namespace SMLInventoryControl
{
    public partial class _stockAutoAdjustProductNotFoundForm : Form
    {
        string _fieldCheck = "Check";
        string _docNoPack = "";
        DateTime _endDate;
        MyLib._myScreen _conditionScreen;

        public _stockAutoAdjustProductNotFoundForm(MyLib._myScreen conditionScreen, string docNoPack, DateTime endDate)
        {
            InitializeComponent();
            this._conditionScreen = conditionScreen;
            this._docNoPack = docNoPack;
            this._endDate = endDate;
            //
            this._whGrid._table_name = _g.d.ic_warehouse._table;
            this._whGrid._width_by_persent = true;
            this._whGrid._addColumn(this._fieldCheck, 11, 5, 10);
            this._whGrid._addColumn(_g.d.ic_warehouse._code, 1, 20, 30);
            this._whGrid._addColumn(_g.d.ic_warehouse._name_1, 1, 75, 60);
            this._whGrid._calcPersentWidthToScatter();
            //
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            this._itemListGrid._table_name = _g.d.ic_trans_detail._table;
            this._itemListGrid._width_by_persent = true;
            this._itemListGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 20, 20);
            this._itemListGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 30, 30);
            this._itemListGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 10, 10);
            this._itemListGrid._addColumn(_g.d.ic_trans_detail._wh_code, 1, 15, 15);
            this._itemListGrid._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 15, 15);
            this._itemListGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._itemListGrid._addColumn(_g.d.ic_trans_detail._item_type, 2, 0, 0);
            this._itemListGrid._calcPersentWidthToScatter();
            //
            this.Load += (s1, e1) =>
            {
                _myFrameWork __myFrameWork = new _myFrameWork();
                this._whGrid._loadFromDataTable(__myFrameWork._queryShort("select " + _g.d.ic_warehouse._code + "," + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " order by " + _g.d.ic_warehouse._code).Tables[0]);
                this._whGrid.Invalidate();
            };

            this._screen._table_name = _g.d.ic_trans._table;
            this._screen._maxColumn = 2;
            this._screen._addDateBox(0, 0, 1, 1, _g.d.ic_trans._doc_date, 1, true);
            this._screen._addTextBox(0, 1, _g.d.ic_trans._doc_time, 25);
            this._screen._enabedControl(_g.d.ic_trans._doc_date, false);
            this._screen._setDataDate(_g.d.ic_trans._doc_date, this._endDate);
        }

        private string _whPack()
        {
            StringBuilder __whPack = new StringBuilder();
            for (int __row = 0; __row < this._whGrid._rowData.Count; __row++)
            {
                if (this._whGrid._cellGet(__row, this._fieldCheck).ToString() == "1")
                {
                    string __whCode = this._whGrid._cellGet(__row, _g.d.ic_warehouse._code).ToString();
                    if (__whPack.Length > 0)
                    {
                        __whPack.Append(",");
                    }
                    __whPack.Append("\'" + __whCode + "\'");
                }
            }
            return __whPack.ToString();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            try
            {
                _myFrameWork __myFrameWork = new _myFrameWork();

                string __doc_time = this._screen._getDataStr(_g.d.ic_trans._doc_time);
                string __doc_time_filter = "";
                if (__doc_time.Length > 0)
                {
                    __doc_time_filter = " or " + _g.d.ic_trans_detail._doc_date_calc + "='" + MyLib._myGlobal._convertDateToQuery(_endDate) + "' and " + _g.d.ic_trans_detail._doc_time_calc + " <= '" + __doc_time + "'  ";
                }

                string __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, _g.d.ic_trans_detail._qty, "(select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._unit_code, "(select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_type) + " from (" + "select " + _g.d.ic_trans_detail._item_code + "," + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + ",sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end) * (" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) as " + _g.d.ic_trans_detail._qty + " from " + _g.d.ic_trans_detail._table + " where " + _g._icInfoFlag._allFlagQty + " and " + _g.d.ic_trans_detail._wh_code + " in (" + this._whPack() + ") and (" + _g.d.ic_trans_detail._doc_date_calc + "<=\'" + MyLib._myGlobal._convertDateToQuery(this._endDate.AddDays(-1)) + "\' " + __doc_time_filter + " ) and not exists (select * from ic_trans_detail as x where x.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + " and x.item_code=ic_trans_detail.item_code and x.wh_code=ic_trans_detail.wh_code and x.shelf_code=ic_trans_detail.shelf_code and x.doc_no in (" + this._docNoPack + ")) and coalesce(" + _g.d.ic_trans_detail._last_status + ",0) =0 group by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + ") as temp1 where " + _g.d.ic_trans_detail._qty + "<>0 order by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code;
                this._itemListGrid._loadFromDataTable(__myFrameWork._queryShort(__query).Tables[0]);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _createButton_Click(object sender, EventArgs e)
        {
            if (this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format) != "")
            {
                try
                {
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                    // Insert ic_trans
                    string __newDocNo = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format_code), MyLib._myGlobal._convertDateToString(this._endDate, false), this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format), _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, _g.g._transControlTypeEnum.ว่าง);
                    ArrayList __itemList = new ArrayList();
                    StringBuilder __query = new StringBuilder();
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // ลบของเก่าที่สร้างไว้แล้ว
                    string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก).ToString();
                    // เพิ่ม
                    string __docTime = "06:00";
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._last_status, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._cust_code, _g.d.ic_trans._used_status, _g.d.ic_trans._doc_success, _g.d.ic_trans._is_pos, _g.d.ic_resource._doc_format_code, _g.d.ic_trans._branch_code) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._convertDateToQuery(this._endDate) + "\'", "\'" + __docTime + "\'", "\'" + __newDocNo + "\'", __transFlag, "3", "0", "0", "\'\'", "0", "0", "0", "\'" + this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format_code) + "\'", "\'" + MyLib._myGlobal._branchCode + "\'") + ")"));

                    // log
                    StringBuilder __logDetail = new StringBuilder("Process Stock No Count Auto To 0 From Doc : " + _docNoPack.Replace("'", "''"));
                    string __logDetailOld = "";

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.logs._table + " (" + _g.d.logs._function_type + "," + _g.d.logs._computer_name + "," + _g.d.logs._guid + "," +
                    _g.d.logs._doc_date + "," + _g.d.logs._doc_no + "," + _g.d.logs._doc_amount + "," +
                    _g.d.logs._doc_date_old + "," + _g.d.logs._doc_no_old + "," + _g.d.logs._doc_amount_old + "," +
                    _g.d.logs._menu_name + "," + _g.d.logs._screen_code + "," + _g.d.logs._function_code + "," + _g.d.logs._user_code + "," +
                    _g.d.logs._date_time + "," + _g.d.logs._data1 + "," + _g.d.logs._data2 + ") values (2," +
                    "\'" + SystemInformation.ComputerName + "\'," + "\'" + Guid.NewGuid().ToString("N") + "\'," +
                    "\'" + MyLib._myGlobal._convertDateToQuery(this._endDate) + "\'" + ",\'" + __newDocNo + "\'," + "0" + "," +
                    "\'" + MyLib._myGlobal._convertDateToQuery(this._endDate) + "\'" + "," + "\'\'" + "," + "0" + "," +
                    "\'" + "ปรับปรุงสินค้าวัตถุดิบ อัตโนมัติ" + "\'," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก).ToString() + "," + "0" + ",\'" +
                    MyLib._myGlobal._userCode + "\',\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")) + "\',\'" + __logDetail.ToString() + "\',\'" + __logDetailOld + "\')"));



                    for (int __loop = 0; __loop < this._itemListGrid._rowData.Count; __loop++)
                    {
                        string __itemCode = this._itemListGrid._cellGet(__loop, _g.d.ic_trans_detail._item_code).ToString();
                        string __itemName = this._itemListGrid._cellGet(__loop, _g.d.ic_trans_detail._item_name).ToString();
                        string __whCode = this._itemListGrid._cellGet(__loop, _g.d.ic_trans_detail._wh_code).ToString();
                        string __locationCode = this._itemListGrid._cellGet(__loop, _g.d.ic_trans_detail._shelf_code).ToString();
                        string __unitCode = this._itemListGrid._cellGet(__loop, _g.d.ic_trans_detail._unit_code).ToString();
                        decimal __qty = MyLib._myGlobal._decimalPhase(this._itemListGrid._cellGet(__loop, _g.d.ic_trans_detail._qty).ToString()) * -1;
                        int __itemType = (int)MyLib._myGlobal._decimalPhase(this._itemListGrid._cellGet(__loop, _g.d.ic_trans_detail._item_type).ToString());
                        //
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail._table +
                            " (" + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time, _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_date_calc, _g.d.ic_trans_detail._doc_time_calc, _g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type, _g.d.ic_trans_detail._last_status, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._item_type, _g.d.ic_trans_detail._line_number, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._ratio, _g.d.ic_trans_detail._stand_value, _g.d.ic_trans_detail._divide_value, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, _g.d.ic_trans_detail._calc_flag, _g.d.ic_trans_detail._branch_code) + ") " +
                            " values " +
                            " (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._convertDateToQuery(this._endDate) + "\'", "\'" + __docTime + "\'", "\'" + __newDocNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(this._endDate) + "\'", "\'" + __docTime + "\'", __transFlag, "3", "0", "\'" + MyLib._myGlobal._convertStrToQuery(__itemCode) + "\'", "\'" + __unitCode + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__itemName) + "\'", __itemType.ToString(), __loop.ToString(), __qty.ToString(), "1", "1", "1", "\'" + __whCode + "\'", "\'" + __locationCode + "\'", "1", "\'" + MyLib._myGlobal._branchCode + "\'") + ")"));
                        __itemList.Add(__itemCode);
                    }
                    // update flag
                    __query.Append(_g.g._queryUpdateTrans());
                    //
                    __query.Append("</node>");
                    string __queryInsertStr = __query.ToString();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryInsertStr);
                    //
                    if (__result.Length == 0)
                    {
                        string __itemListForProcess = _g.g._getItemRepack(__itemList);
                        //
                        SMLProcess._docFlow __process = new SMLProcess._docFlow();
                        __process._processAll(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, __itemListForProcess, __newDocNo);
                        //
                        string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, __itemListForProcess, __transFlag);
                        if (__resultStr.Length > 0)
                        {
                            MessageBox.Show(__resultStr);
                        }
                        else
                        {
                            MessageBox.Show("Success");
                        }
                    }
                    else
                    {
                        MessageBox.Show(__result.ToString());
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("ไม่พบรูปแบบเลขที่เอกสาร กรุณาเลือก รูปแบบเลขที่เอกสาร");
            }
        }
    }
}
