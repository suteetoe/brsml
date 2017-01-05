using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.Threading;
using System.Globalization;

namespace SMLInventoryControl
{
    public partial class _stockAutoAdjustControl : UserControl
    {
        string _fieldCheck = "Check";
        Thread _thread = null;

        public _stockAutoAdjustControl()
        {
            InitializeComponent();
            //
            this._conditionScreen._maxColumn = 2;
            this._conditionScreen._table_name = _g.d.ic_resource._table;
            this._conditionScreen._addDateBox(0, 0, 1, 0, _g.d.ic_resource._date_begin, 1, true);
            this._conditionScreen._addDateBox(0, 1, 1, 0, _g.d.ic_resource._date_end, 1, true);
            this._conditionScreen._addTextBox(1, 0, _g.d.ic_resource._doc_format_code, 1);
            this._conditionScreen._addTextBox(1, 1, _g.d.ic_resource._doc_format, 1);
            if (MyLib._myGlobal._isVersionAccount)
            {
                this._conditionScreen._addTextBox(2, 0, 1, 1, _g.d.ic_resource._doc_format_code_adjust_down, 1, 25, 1, true, false);

            }
            //
            this._conditionScreen._enabedControl(_g.d.ic_resource._doc_format_code, false);
            this._conditionScreen._enabedControl(_g.d.ic_resource._doc_format, false);
            //
            this._conditionScreen._setDataDate(_g.d.ic_resource._date_begin, MyLib._myGlobal._workingDate);
            this._conditionScreen._setDataDate(_g.d.ic_resource._date_end, MyLib._myGlobal._workingDate);
            this._conditionScreen._textBoxSearch += _conditionScreen__textBoxSearch;
            //
            this._selectGrid._table_name = _g.d.ic_trans._table;
            this._selectGrid._addColumn(this._fieldCheck, 11, 5, 5);
            this._selectGrid._addColumn(_g.d.ic_trans._doc_date, 4, 10, 10);
            this._selectGrid._addColumn(_g.d.ic_trans._doc_time, 1, 10, 10);
            this._selectGrid._addColumn(_g.d.ic_trans._doc_no, 1, 20, 20);
            this._selectGrid._addColumn(_g.d.ic_trans._doc_ref_date, 4, 10, 10);
            this._selectGrid._addColumn(_g.d.ic_trans._doc_ref, 1, 20, 20);
            this._selectGrid._addColumn(_g.d.ic_trans._remark, 1, 30, 30);
            this._selectGrid._addColumn(_g.d.ic_trans._last_status, 1, 10, 10);
            this._selectGrid._calcPersentWidthToScatter();
            //
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            this._checkGrid._table_name = _g.d.ic_trans_detail._table;
            this._checkGrid._addColumn(_g.d.ic_trans_detail._doc_date, 4, 10, 10);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._doc_time, 1, 10, 10);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._doc_no, 1, 20, 20);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 10, 10);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 20, 20);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 10, 10);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._wh_code, 1, 10, 10);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 10, 10);
            this._checkGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._checkGrid._calcPersentWidthToScatter();
            //
            this.Disposed += (s1, e1) =>
            {
                this._stopThread();
            };
            try
            {
                this._stopButton.Enabled = false;
                // ค้นหารูปแบบเอกสาร
                _myFrameWork __myFrameWork = new _myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._screen_code + "=\'IA\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    if (__getFormat.Rows.Count == 1)
                    {
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code, __getFormat.Rows[0][_g.d.erp_doc_format._code].ToString());
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format, __getFormat.Rows[0][_g.d.erp_doc_format._format].ToString());
                        this._selectDocFormatButton.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบประเภทเอกสารปรับปรุงสินค้า");
                }

                if (MyLib._myGlobal._isVersionAccount)
                {
                    DataTable __getFormatDown = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._screen_code + "=\'IS\'").Tables[0];
                    if (__getFormatDown.Rows.Count > 0)
                    {
                        if (__getFormatDown.Rows.Count == 1)
                        {
                            this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code_adjust_down, __getFormatDown.Rows[0][_g.d.erp_doc_format._code].ToString());
                        }
                    }
                }

                string __removeQuery = "delete from ic_trans_detail where trans_flag = 66 and not exists(select doc_no from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag) ";
                //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __removeQuery);

            }
            catch
            {
            }


        }

        private void _conditionScreen__textBoxSearch(object sender)
        {
            string __searchName = ((MyLib._myTextBox)sender)._name;
            if (__searchName.Equals(_g.d.ic_resource._doc_format_code_adjust_down))
            {
                MyLib._searchDataFull _search = new _searchDataFull();
                _search.StartPosition = FormStartPosition.CenterScreen;
                _search._dataList._loadViewFormat(_g.g._search_screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
                _search._dataList._extraWhere2 = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'IS\'";

                _search._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        string __docFormatCode = _search._dataList._gridData._cellGet(e1._row, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code_adjust_down, __docFormatCode);

                        _search.Close();
                    }
                };

                _search._searchEnterKeyPress += (s1, e1) =>
                {
                    if (e1 != -1)
                    {
                        string __docFormatCode = _search._dataList._gridData._cellGet(e1, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                        this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code_adjust_down, __docFormatCode);

                        _search.Close();
                    }
                };
                _search.ShowDialog();
            }
        }

        private void _selectDocFormatButton_Click(object sender, EventArgs e)
        {
            MyLib._searchDataFull _search = new _searchDataFull();
            _search.StartPosition = FormStartPosition.CenterScreen;
            _search._dataList._loadViewFormat(_g.g._search_screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
            _search._dataList._extraWhere2 = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'IA\'";

            _search._dataList._gridData._mouseClick += (s1, e1) =>
            {
                if (e1._row != -1)
                {
                    string __docFormatCode = _search._dataList._gridData._cellGet(e1._row, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                    string __docFormat = _search._dataList._gridData._cellGet(e1._row, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._format).ToString();
                    this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code, __docFormatCode);
                    this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format, __docFormat);

                    _search.Close();
                }
            };

            _search._searchEnterKeyPress += (s1, e1) =>
            {
                if (e1 != -1)
                {
                    string __docFormatCode = _search._dataList._gridData._cellGet(e1, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                    string __docFormat = _search._dataList._gridData._cellGet(e1, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._format).ToString();
                    this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format_code, __docFormatCode);
                    this._conditionScreen._setDataStr(_g.d.ic_resource._doc_format, __docFormat);

                    _search.Close();
                }
            };
            _search.ShowDialog();
        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            _myFrameWork __myFrameWork = new _myFrameWork();
            this._selectGrid._clear();
            String __query = "select " + MyLib._myGlobal._fieldAndComma("1 as " + this._fieldCheck, _g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._doc_ref_date, _g.d.ic_trans._doc_ref, _g.d.ic_trans._remark) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_date + " between " + this._conditionScreen._getDataStrQuery(_g.d.ic_resource._date_begin) + " and " + this._conditionScreen._getDataStrQuery(_g.d.ic_resource._date_end) + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + " and coalesce(" + _g.d.ic_trans._last_status + ", 0)=0 and coalesce(" + _g.d.ic_trans._doc_success + ", 0)=0 order by " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_time;
            this._selectGrid._loadFromDataTable(__myFrameWork._queryShort(__query).Tables[0]);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            if (this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format) != "")
            {

                this._processButton.Enabled = false;
                this._stopButton.Enabled = true;
                this._thread = new Thread(new ThreadStart(this._processNow));
                this._thread.Start();
            }
            else
            {
                MessageBox.Show("ไม่พบรูปแบบเลขที่เอกสาร กรุณาเลือก รูปแบบเลขที่เอกสาร");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=รวมทุกบิล,1=แยกบิล</param>
        /// <param name="row"></param>
        /// <param name="docNo"></param>
        private void _process(int mode, int row, string docNo)
        {
            string __fieldBalance = "qty_balance";

            _myFrameWork __myFrameWork = new _myFrameWork();
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();

            string __docNoWhere = (mode == 0) ? " in (" + docNo + ")" : "=\'" + docNo + "\'";
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select distinct " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + __docNoWhere));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select distinct " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + __docNoWhere));


            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select (select " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + ") as " + _g.d.ic_trans_detail._item_type + "," + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, "sum(" + _g.d.ic_trans_detail._qty + "*(" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + ")) as " + _g.d.ic_trans_detail._qty) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + __docNoWhere + " group by " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code)));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + __docNoWhere + " order by doc_date, doc_time "));
            __myquery.Append("</node>");

            string __debugQuery = __myquery.ToString();

            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

            if (__getData.Count > 0)
            {
                DataTable __source1 = ((DataSet)__getData[0]).Tables[0];
                DataTable __source2 = ((DataSet)__getData[1]).Tables[0];
                DataTable __source3 = ((DataSet)__getData[2]).Tables[0];
                DataTable __source4 = ((DataSet)__getData[3]).Tables[0];
                //
                if (__source1.Rows.Count > 0)
                {
                    DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__source4.Rows[0][_g.d.ic_trans._doc_date].ToString());
                    DateTime __docDateCalc = __docDate.AddDays(-1);
                    string __docTime = __source4.Rows[0][_g.d.ic_trans._doc_time].ToString();
                    string __docTimeCalc = "";
                    //return;
                    if (mode == 0)
                    {
                        __docTimeCalc = "24:58";
                        __docTime = "24:59";
                    }
                    else
                    {
                        string[] __docTimeSplit = __docTime.Split(':');
                        int __hour = (int)MyLib._myGlobal._decimalPhase(__docTimeSplit[0].ToString());
                        int __minute = (int)MyLib._myGlobal._decimalPhase(__docTimeSplit[1].ToString());
                        if (--__minute < 0)
                        {
                            __minute = 59;
                            __hour--;
                        }
                        __docTimeCalc = string.Format("{0:00}:{1:00}", __hour, __minute);
                    }
                    //
                    StringBuilder __itemCode = new StringBuilder();
                    StringBuilder __whCode = new StringBuilder();
                    StringBuilder __locationCode = new StringBuilder();
                    for (int __loop = 0; __loop < __source1.Rows.Count; __loop++)
                    {
                        if (__loop != 0)
                        {
                            __itemCode.Append(",");
                        }
                        __itemCode.Append("\'" + __source1.Rows[__loop][_g.d.ic_trans_detail._item_code].ToString() + "\'");
                    }
                    for (int __loop = 0; __loop < __source2.Rows.Count; __loop++)
                    {
                        if (__loop != 0)
                        {
                            __whCode.Append(",");
                            __locationCode.Append(",");
                        }
                        __whCode.Append("\'" + __source2.Rows[__loop][_g.d.ic_trans_detail._wh_code].ToString() + "\'");
                        __locationCode.Append("\'" + __source2.Rows[__loop][_g.d.ic_trans_detail._shelf_code].ToString() + "\'");
                    }
                    // คำนวณยอดคงเหลือแยกตามที่เก็บ เฉพาะสินค้าในเอกสาร
                    //String __queryStockBalance = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + ",sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end) * (" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) as " + __fieldBalance + " from " + _g.d.ic_trans_detail._table + " where " + _g._icInfoFlag._allFlag + " and " + _g.d.ic_trans_detail._item_code + " in (" + __itemCode.ToString() + ") and " + _g.d.ic_trans_detail._wh_code + " in (" + __whCode.ToString() + ") and " + _g.d.ic_trans_detail._shelf_code + " in (" + __locationCode.ToString() + ") and (" + _g.d.ic_trans_detail._doc_date_calc + "<=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' or (" + _g.d.ic_trans_detail._doc_date_calc + "=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' and " + _g.d.ic_trans_detail._doc_time_calc + "<=\'" + __docTimeCalc + "\')) group by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + " order by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code;  q ผิด โต๊ แก้ไขตรง  (" + _g.d.ic_trans_detail._doc_date_calc + "=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' and " + _g.d.ic_trans_detail._doc_time_calc + "<=\'" + __docTimeCalc + "\'))  ต้องเป็นวันที่ เช็ค stock
                    String __queryStockBalance = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + ",sum(" + _g.d.ic_trans_detail._calc_flag + "*((case when " + _g.d.ic_trans_detail._trans_flag + " in (14,16) and " + _g.d.ic_trans_detail._inquiry_type + "=1 then 0 else " + _g.d.ic_trans_detail._qty + " end) * (" + _g.d.ic_trans_detail._stand_value + "/" + _g.d.ic_trans_detail._divide_value + "))) as " + __fieldBalance + " from " + _g.d.ic_trans_detail._table + " where " + _g._icInfoFlag._allFlagQty + " and " + _g.d.ic_trans_detail._last_status + " =0 and " + _g.d.ic_trans_detail._item_code + " in (" + __itemCode.ToString() + ") and " + _g.d.ic_trans_detail._wh_code + " in (" + __whCode.ToString() + ") and " + _g.d.ic_trans_detail._shelf_code + " in (" + __locationCode.ToString() + ") and (" + _g.d.ic_trans_detail._doc_date_calc + "<=\'" + MyLib._myGlobal._convertDateToQuery(__docDateCalc) + "\' or (" + _g.d.ic_trans_detail._doc_date_calc + "=\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\' and " + _g.d.ic_trans_detail._doc_time_calc + "<=\'" + __docTimeCalc + "\')) group by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + " order by " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code; // เพิ่ม last_status = 0 

                    __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryStockBalance));
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._code, _g.d.ic_inventory._unit_cost, _g.d.ic_inventory._name_1) + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " in (" + __itemCode.ToString() + ") order by " + _g.d.ic_inventory._code));
                    __myquery.Append("</node>");
                    string __queryStr = __myquery.ToString();
                    ArrayList __getData2 = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                    DataTable __stockBalance = ((DataSet)__getData2[0]).Tables[0];
                    DataTable __item = ((DataSet)__getData2[1]).Tables[0];
                    //

                    Boolean __foundAdjustDown = false;

                    List<_compareBalance> __dataList = new List<_compareBalance>();
                    for (int __loop = 0; __loop < __source3.Rows.Count; __loop++)
                    {
                        _compareBalance __data = new _compareBalance();
                        __data._itemCode = __source3.Rows[__loop][_g.d.ic_trans_detail._item_code].ToString();
                        __data._whCode = __source3.Rows[__loop][_g.d.ic_trans_detail._wh_code].ToString();
                        __data._locationCode = __source3.Rows[__loop][_g.d.ic_trans_detail._shelf_code].ToString();
                        __data._qtyNew = MyLib._myGlobal._decimalPhase(__source3.Rows[__loop][_g.d.ic_trans_detail._qty].ToString());

                        // toe item_type
                        __data._item_type = (int)MyLib._myGlobal._decimalPhase(__source3.Rows[__loop][_g.d.ic_trans_detail._item_type].ToString());
                        if (__stockBalance.Rows.Count > 0)
                        {
                            DataRow[] __find = __stockBalance.Select(_g.d.ic_trans_detail._item_code + "=\'" + __data._itemCode + "\' and " + _g.d.ic_trans_detail._wh_code + "=\'" + __data._whCode + "\' and " + _g.d.ic_trans_detail._shelf_code + "=\'" + __data._locationCode + "\'");
                            if (__find.Length > 0)
                            {
                                __data._qtyOld = MyLib._myGlobal._decimalPhase(__find[0][__fieldBalance].ToString());
                                __data._qtyDiff = __data._qtyNew - __data._qtyOld;
                            }
                            else
                            {
                                // กรณีไม่พบยอดคงเหลือ แต่มียอดตรวจนับ
                                __data._qtyOld = 0M;
                                __data._qtyDiff = __data._qtyNew;
                            }
                        }
                        else
                        {
                            __data._qtyOld = 0M;
                            __data._qtyDiff = __data._qtyNew - __data._qtyOld;
                        }
                        if (__data._qtyDiff != 0)
                        {
                            if (MyLib._myGlobal._isVersionAccount && __foundAdjustDown == false)
                            {
                                if (__data._qtyDiff < 0)
                                {
                                    __foundAdjustDown = true;
                                }
                            }

                            __dataList.Add(__data);
                        }
                    }
                    // Insert ic_trans
                    string __newDocNo = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format_code), MyLib._myGlobal._convertDateToString(__docDate, false), this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format), _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, _g.g._transControlTypeEnum.ว่าง);
                    string __newDocNoDown = "-" + __newDocNo;
                    ArrayList __itemList = new ArrayList();
                    StringBuilder __query = new StringBuilder();
                    __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // ลบของเก่าที่สร้างไว้แล้ว
                    string __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก).ToString();
                    string __transFlagDown = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด).ToString();

                    if (mode == 1)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_ref + "=\'" + docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag));
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_ref + "=\'" + docNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlag));

                        if (MyLib._myGlobal._isVersionAccount && __foundAdjustDown)
                        {
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_ref + "=\'" + docNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + __transFlagDown));

                        }
                    }
                    // เพิ่ม
                    string __docRefNo = (mode == 0) ? "" : docNo;
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref_date, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._last_status, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._cust_code, _g.d.ic_trans._used_status, _g.d.ic_trans._doc_success, _g.d.ic_trans._is_pos, _g.d.ic_trans._doc_format_code, _g.d.ic_trans._branch_code) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", "\'" + __newDocNo + "\'", "\'" + __docRefNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", __transFlag, "3", "0", "0", "\'\'", "0", "0", "0", "\'" + this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format_code) + "\'", "\'" + MyLib._myGlobal._branchCode + "\'") + ")"));

                    if (MyLib._myGlobal._isVersionAccount && __foundAdjustDown)
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref_date, _g.d.ic_trans._trans_flag, _g.d.ic_trans._trans_type, _g.d.ic_trans._last_status, _g.d.ic_trans._inquiry_type, _g.d.ic_trans._cust_code, _g.d.ic_trans._used_status, _g.d.ic_trans._doc_success, _g.d.ic_trans._is_pos, _g.d.ic_trans._doc_format_code, _g.d.ic_trans._branch_code) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", "\'" + __newDocNoDown + "\'", "\'" + __docRefNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", __transFlagDown, "3", "0", "0", "\'\'", "0", "0", "0", "\'" + this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format_code_adjust_down) + "\'", "\'" + MyLib._myGlobal._branchCode + "\'") + ")"));
                    }

                    {
                        for (int __loop = 0; __loop < __dataList.Count; __loop++)
                        {


                            DataRow[] __itemSelect = __item.Select(_g.d.ic_inventory._code + "=\'" + __dataList[__loop]._itemCode + "\'");
                            string __unitCost = "";
                            string __itemName = "";
                            if (__itemSelect.Length > 0)
                            {
                                __unitCost = __itemSelect[0][_g.d.ic_inventory._unit_cost].ToString();
                                __itemName = __itemSelect[0][_g.d.ic_inventory._name_1].ToString();
                            }

                            if (MyLib._myGlobal._isVersionAccount && __dataList[__loop]._qtyDiff < 0)
                            {
                                decimal __qtyDown = __dataList[__loop]._qtyDiff * -1;
                                // ปรับลด
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time, _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_ref, _g.d.ic_trans_detail._doc_date_calc, _g.d.ic_trans_detail._doc_time_calc, _g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type, _g.d.ic_trans_detail._last_status, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._line_number, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._ratio, _g.d.ic_trans_detail._stand_value, _g.d.ic_trans_detail._divide_value, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, _g.d.ic_trans_detail._calc_flag, _g.d.ic_trans_detail._item_type, _g.d.ic_trans_detail._branch_code) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", "\'" + __newDocNoDown + "\'", "\'" + __docRefNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", __transFlagDown, "3", "0", "\'" + MyLib._myGlobal._convertStrToQuery(__dataList[__loop]._itemCode) + "\'", "\'" + __unitCost + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__itemName) + "\'", __loop.ToString(), __qtyDown.ToString(), "1", "1", "1", "\'" + __dataList[__loop]._whCode + "\'", "\'" + __dataList[__loop]._locationCode + "\'", "-1", __dataList[__loop]._item_type.ToString(), "\'" + MyLib._myGlobal._branchCode + "\'") + ")")); // toe เพิ่ม item_type = 0 
                            }
                            else
                            {
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail._table + " (" + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_time, _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_ref, _g.d.ic_trans_detail._doc_date_calc, _g.d.ic_trans_detail._doc_time_calc, _g.d.ic_trans_detail._trans_flag, _g.d.ic_trans_detail._trans_type, _g.d.ic_trans_detail._last_status, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._line_number, _g.d.ic_trans_detail._qty, _g.d.ic_trans_detail._ratio, _g.d.ic_trans_detail._stand_value, _g.d.ic_trans_detail._divide_value, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, _g.d.ic_trans_detail._calc_flag, _g.d.ic_trans_detail._item_type, _g.d.ic_trans_detail._branch_code) + ") values (" + MyLib._myGlobal._fieldAndComma("\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", "\'" + __newDocNo + "\'", "\'" + __docRefNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'", "\'" + __docTime + "\'", __transFlag, "3", "0", "\'" + MyLib._myGlobal._convertStrToQuery(__dataList[__loop]._itemCode) + "\'", "\'" + __unitCost + "\'", "\'" + MyLib._myGlobal._convertStrToQuery(__itemName) + "\'", __loop.ToString(), __dataList[__loop]._qtyDiff.ToString(), "1", "1", "1", "\'" + __dataList[__loop]._whCode + "\'", "\'" + __dataList[__loop]._locationCode + "\'", "1", __dataList[__loop]._item_type.ToString(), "\'" + MyLib._myGlobal._branchCode + "\'") + ")")); // toe เพิ่ม item_type = 0 
                            }


                            __itemList.Add(__dataList[__loop]._itemCode);
                        }
                    }

                    // doc_ref
                    string __fieldList2 = _g.d.ap_ar_trans_detail._calc_flag + "," + _g.d.ap_ar_trans_detail._trans_type + "," + _g.d.ap_ar_trans_detail._trans_flag + "," + _g.d.ap_ar_trans_detail._doc_date + "," + _g.d.ap_ar_trans_detail._doc_no;
                    string __dataList2 = _g.g._transCalcTypeGlobal._apArTransCalcType(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก).ToString() + "," + _g.g._arapTransTypeGlobal._transType(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก).ToString() + "," + __transFlag + ",\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\',\'" + __newDocNo + "\'";


                    if (mode == 0)
                    {
                        // doc pack
                        string[] __docNoSplit = docNo.Split(',');
                        foreach (string __docNoStr in __docNoSplit)
                        {
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ap_ar_trans_detail._table + "(" + __fieldList2 + "," + _g.d.ap_ar_trans_detail._billing_no + ") values (" + __dataList2 + "," + __docNoStr + ")"));
                            if (MyLib._myGlobal._isVersionAccount && __foundAdjustDown)
                            {
                                __fieldList2 = _g.d.ap_ar_trans_detail._calc_flag + "," + _g.d.ap_ar_trans_detail._trans_type + "," + _g.d.ap_ar_trans_detail._trans_flag + "," + _g.d.ap_ar_trans_detail._doc_date + "," + _g.d.ap_ar_trans_detail._doc_no;
                                __dataList2 = _g.g._transCalcTypeGlobal._apArTransCalcType(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด).ToString() + "," + _g.g._arapTransTypeGlobal._transType(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด).ToString() + "," + __transFlagDown + ",\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\',\'" + __newDocNoDown + "\'";
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ap_ar_trans_detail._table + "(" + __fieldList2 + "," + _g.d.ap_ar_trans_detail._billing_no + ") values (" + __dataList2 + "," + __docNoStr + ")"));

                            }
                        }
                    }
                    else
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ap_ar_trans_detail._table + "(" + __fieldList2 + ", " + _g.d.ap_ar_trans_detail._billing_no + ") values (" + __dataList2 + ",\'" + docNo + "\')"));
                        if (MyLib._myGlobal._isVersionAccount && __foundAdjustDown)
                        {
                            __fieldList2 = _g.d.ap_ar_trans_detail._calc_flag + "," + _g.d.ap_ar_trans_detail._trans_type + "," + _g.d.ap_ar_trans_detail._trans_flag + "," + _g.d.ap_ar_trans_detail._doc_date + "," + _g.d.ap_ar_trans_detail._doc_no;
                            __dataList2 = _g.g._transCalcTypeGlobal._apArTransCalcType(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด).ToString() + "," + _g.g._arapTransTypeGlobal._transType(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด).ToString() + "," + __transFlagDown + ",\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\',\'" + __newDocNoDown + "\'";
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ap_ar_trans_detail._table + "(" + __fieldList2 + ", " + _g.d.ap_ar_trans_detail._billing_no + ") values (" + __dataList2 + ",\'" + docNo + "\')"));

                        }
                    }

                    // update flag 
                    __query.Append(_g.g._queryUpdateTrans());
                    //
                    // log
                    StringBuilder __logDetail = new StringBuilder("Process Auto Adjust from doc : " + MyLib._myGlobal._convertStrToQuery(docNo));
                    string __logDetailOld = "";

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.logs._table + " (" + _g.d.logs._function_type + "," + _g.d.logs._computer_name + "," + _g.d.logs._guid + "," +
                    _g.d.logs._doc_date + "," + _g.d.logs._doc_no + "," + _g.d.logs._doc_amount + "," +
                    _g.d.logs._doc_date_old + "," + _g.d.logs._doc_no_old + "," + _g.d.logs._doc_amount_old + "," +
                    _g.d.logs._menu_name + "," + _g.d.logs._screen_code + "," + _g.d.logs._function_code + "," + _g.d.logs._user_code + "," +
                    _g.d.logs._date_time + "," + _g.d.logs._data1 + "," + _g.d.logs._data2 + ") values (2," +
                    "\'" + SystemInformation.ComputerName + "\'," + "\'" + Guid.NewGuid().ToString("N") + "\'," +
                    "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'" + ",\'" + __newDocNo + "\'," + "0" + "," +
                    "\'" + MyLib._myGlobal._convertDateToQuery(__docDate) + "\'" + "," + "\'\'" + "," + "0" + "," +
                    "\'" + "ปรับปรุงสินค้าวัตถุดิบ อัตโนมัติ" + "\'," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก).ToString() + "," + mode.ToString() + ",\'" +
                    MyLib._myGlobal._userCode + "\',\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")) + "\',\'" + __logDetail.ToString() + "\',\'" + __logDetailOld + "\')"));

                    __query.Append("</node>");
                    string __queryInsertStr = __query.ToString();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryInsertStr);
                    //
                    if (__result.Length == 0)
                    {
                        if (mode == 1)
                        {
                            this._selectGrid._cellUpdate(row, _g.d.ic_trans._last_status, "Success", false);
                            this._selectGrid.Invalidate();
                        }
                        //
                        string __itemListForProcess = _g.g._getItemRepack(__itemList);
                        SMLProcess._docFlow __process = new SMLProcess._docFlow();
                        __process._processAll(_g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, __itemListForProcess, docNo);
                        //
                        string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, __itemListForProcess, __transFlag);
                        if (__resultStr.Length > 0)
                        {
                            MessageBox.Show(__resultStr);
                        }
                    }
                    else
                    {
                        MessageBox.Show(__result.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Error Query" + __myquery.ToString());
            }
        }

        private void _processNow()
        {
            if (_g.g._companyProfile._count_stock_sum)
            {
                // แบบรวมทุกบิล กำหนดเวลาให้เป็น 24:00
                this._process(0, 0, this._docNoPack());
            }
            else
            {
                for (int __row = 0; __row < this._selectGrid._rowData.Count; __row++)
                {
                    if (this._selectGrid._cellGet(__row, this._fieldCheck).ToString() == "1")
                    {
                        string __docNo = this._selectGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString();
                        this._process(1, __row, __docNo);
                    }
                }
            }
            MessageBox.Show("ประมวลผลสำเร็จ");
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            this._stopThread();
        }

        private void _stopThread()
        {
            try
            {
                this._thread.Abort();
                this._processButton.Enabled = true;
                this._stopButton.Enabled = false;
            }
            catch
            {
            }
        }

        public class _compareBalance
        {
            public string _itemCode = "";
            public string _whCode = "";
            public string _locationCode = "";
            public decimal _qtyOld = 0M;
            public decimal _qtyNew = 0M;
            public decimal _qtyDiff = 0M;
            public int _item_type = 0;
        }

        private string _docNoPack()
        {
            StringBuilder __docPack = new StringBuilder();
            for (int __row = 0; __row < this._selectGrid._rowData.Count; __row++)
            {
                if (this._selectGrid._cellGet(__row, this._fieldCheck).ToString() == "1")
                {
                    string __docNo = this._selectGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString();
                    if (__docPack.Length > 0)
                    {
                        __docPack.Append(",");
                    }
                    __docPack.Append("\'" + __docNo + "\'");
                }
            }
            return __docPack.ToString();
        }

        private void _checkButton_Click(object sender, EventArgs e)
        {
            try
            {
                _myFrameWork __myFrameWork = new _myFrameWork();
                string __query = "select " + _g.d.ic_trans_detail._item_code + " from (select " + _g.d.ic_trans_detail._item_code + ",count(*) as xcount from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " in (" + this._docNoPack() + ") group by " + _g.d.ic_trans_detail._item_code + ") as xxx where xcount>1 and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString();
                __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._doc_date, _g.d.ic_trans_detail._doc_no, _g.d.ic_trans_detail._doc_time, _g.d.ic_trans_detail._item_code, _g.d.ic_trans_detail._item_name, _g.d.ic_trans_detail._unit_code, _g.d.ic_trans_detail._wh_code, _g.d.ic_trans_detail._shelf_code, _g.d.ic_trans_detail._qty) + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + " and " + _g.d.ic_trans_detail._item_code + " in (" + __query + ") order by " + _g.d.ic_trans_detail._item_code;
                this._checkGrid._loadFromDataTable(__myFrameWork._queryShort(__query).Tables[0]);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _productNotFoundButton_Click(object sender, EventArgs e)
        {
            if (this._conditionScreen._getDataStr(_g.d.ic_resource._doc_format) != "")
            {
                _stockAutoAdjustProductNotFoundForm __form = new _stockAutoAdjustProductNotFoundForm(this._conditionScreen, this._docNoPack(), MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_end)));
                __form.ShowDialog();
            }
            else
            {
                MessageBox.Show("ไม่พบรูปแบบเลขที่เอกสาร กรุณาเลือก รูปแบบเลขที่เอกสาร");
            }
        }
    }
}
