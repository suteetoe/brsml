using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLTransferDataPOS
{
    public partial class _earthTransferTransectionControl : UserControl
    {
        int _transFlag = 0;

        string _fieldSendSuccess = "send_success";
        MyLib._myGrid _gridAppendDoc = new MyLib._myGrid();
        MyLib._myGrid _gridTransDetail = new MyLib._myGrid();

        public string _columnSerialNumberCount = "serial_number_count";
        private string _columnPriceRoworder = "price_roworder";
        public string _columnAverageCostUnitStand = "average_cost_stand";
        public string _columnAverageCostUnitDiv = "average_cost_div";
        public string _columnSerialNumber = _g.d.ic_trans_detail._serial_number;

        public _earthTransferTransectionControl(int transFlag)
        {
            InitializeComponent();
            this._transFlag = transFlag;

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            // check targer column

            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "ALTER TABLE ic_trans ADD COLUMN send_success boolean DEFAULT false; ");
            }
            catch
            {
            }

            this._sale_pos_screen_top._maxColumn = 2;
            this._sale_pos_screen_top._table_name = _g.d.ic_trans_resource._table;
            this._sale_pos_screen_top._getResource = false;
            this._sale_pos_screen_top._addDateBox(0, 0, 1, 0, _g.d.ic_trans_resource._from_doc_date, 1, true, true, true, "จากวันที่");
            this._sale_pos_screen_top._addDateBox(0, 1, 1, 0, _g.d.ic_trans_resource._to_doc_date, 1, true, true, true, "ถึงวันที่");
            this._sale_pos_screen_top._addTextBox(1, 0, 1, 0, _g.d.ic_trans._branch_code, 1, 0, 0, true, false, true, false, false, "สาขา");

            this._transGrid._table_name = _g.d.ic_trans._table;
            this._transGrid._isEdit = false;

            this._transGrid._addColumn("check", 11, 1, 2, false, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans._doc_date, 4, 1, 15, false, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans._doc_no, 1, 1, 12, false, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._branch_code, 1, 1, 20, false, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._status, 1, 1, 8, true, false, false, false, __formatNumberQty);

            //  ซ่อน
            this._transGrid._addColumn(_g.d.ic_trans._doc_time, 1, 1, 12, false, true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._doc_format_code, 1, 1, 12, false, true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._doc_ref_date, 4, 1, 12, false, true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._doc_ref, 1, 1, 12, false, true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._wh_from, 1, 1, 12, false, true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._location_from, 1, 1, 12, false, true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._remark, 1, 1, 12, false, true, true, false);

            this._transGrid._calcPersentWidthToScatter();


            DateTime __beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime __endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            this._sale_pos_screen_top._setDataDate(_g.d.ic_trans_resource._from_doc_date, __beginDate);
            this._sale_pos_screen_top._setDataDate(_g.d.ic_trans_resource._to_doc_date, __endDate);


            #region Trans Grid

            this._gridAppendDoc._table_name = _g.d.ic_trans._table;
            this._gridAppendDoc._isEdit = false;

            this._gridAppendDoc._addColumn(_g.d.ic_trans._trans_flag, 2, 1, 5, false, false, true);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._trans_type, 2, 1, 5, false, false, true);

            this._gridAppendDoc._addColumn(_g.d.ic_trans._doc_date, 4, 255, 5);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._doc_time, 1, 20, 5);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._doc_no, 1, 50, 5);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._branch_code, 1, 1, 20, false, false, true, false);

            this._gridAppendDoc._addColumn(_g.d.ic_trans._doc_format_code, 1, 50, 10);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._doc_ref_date, 4, 1, 12, false, true, true, false);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._doc_ref, 1, 1, 12, false, true, true, false);

            this._gridAppendDoc._addColumn(_g.d.ic_trans._wh_from, 1, 50, 10);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._location_from, 1, 50, 10);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._wh_to, 1, 50, 10);
            this._gridAppendDoc._addColumn(_g.d.ic_trans._location_to, 1, 50, 10);

            this._gridAppendDoc._addColumn(_g.d.ic_trans._remark, 1, 255, 10);

            this._gridAppendDoc.Dock = DockStyle.Fill;
            this.tabPage2.Controls.Add(this._gridAppendDoc);

            #endregion

            #region Trans Detail Grid

            this._gridTransDetail._table_name = _g.d.ic_trans_detail._table;
            this._gridTransDetail._isEdit = false;

            // ซ่อน
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._doc_no, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._doc_date, 4, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._trans_flag, 2, 1, 5, false, false, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._trans_type, 2, 1, 5, false, false, true);

            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._doc_time, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._cust_code, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._inquiry_type, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._is_pos, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._wh_code, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._calc_flag, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._doc_date_calc, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._doc_time_calc, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._vat_type, 1, 10, 10);

            // รายการ

            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, false, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10, false, false, true, false);

            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._discount_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
            this._gridTransDetail._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);

            // Field ซ่อน
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._extra, 12, 1, 5, false, true, false);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._dimension, 12, 1, 5, false, true, false);

            // Field ซ่อน
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._unit_name, 1, 1, 0, false, true, false);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._unit_type, 2, 1, 0, false, true, false);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._stand_value, 3, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._divide_value, 3, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._item_type, 2, 1, 10, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._item_code_main, 1, 1, 10, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._is_permium, 2, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._is_get_price, 2, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._price_exclude_vat, 3, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._total_vat_value, 3, 1, 15, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._sum_amount_exclude_vat, 3, 1, 0, false, true, true, false, __formatNumberAmount);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._hidden_cost_1_exclude_vat, 3, 1, 0, false, true, true);

            this._gridTransDetail._addColumn(this._columnAverageCostUnitStand, 3, 1, 0, false, true, false);
            this._gridTransDetail._addColumn(this._columnAverageCostUnitDiv, 3, 1, 0, false, true, false);
            this._gridTransDetail._addColumn(this._columnPriceRoworder, 2, 1, 0, false, true, false);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._user_approve, 1, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._price_mode, 1, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._price_type, 1, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._is_serial_number, 2, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._tax_type, 2, 1, 0, false, true, true);
            this._gridTransDetail._addColumn(this._columnSerialNumber, 12, 1, 0, false, true, false);
            this._gridTransDetail._addColumn(this._columnSerialNumberCount, 3, 1, 0, false, true, false);

            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._wh_code_2, 1, 10, 10);
            this._gridTransDetail._addColumn(_g.d.ic_trans_detail._shelf_code_2, 1, 10, 10);

            if (transFlag == 72)
            {
                this._gridTransDetail._addColumn(_g.d.ic_trans_detail._line_number, 2, 1, 10, false, true, true);
            }

            this._gridTransDetail._calcPersentWidthToScatter();

            this.tabPage3.Controls.Add(this._gridTransDetail);
            this._gridTransDetail.Dock = DockStyle.Fill;

            #endregion

        }

        void _loadData()
        {
            string __docDateTransFlagWhere = _g.d.ic_trans._doc_date + " between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) +
               "  and " + _g.d.ic_trans._trans_flag + " in ( " + this._transFlag + " ) and " + _g.d.ic_trans._last_status + "=0 ";

            StringBuilder __branchList = new StringBuilder();
            string __getBranchCode = this._sale_pos_screen_top._getDataStr(_g.d.ic_trans._branch_code);
            if (__getBranchCode.Length > 0)
            {
                string[] branch = __getBranchCode.Split(',');
                foreach (string getBranch in branch)
                {
                    if (__branchList.Length > 0)
                    {
                        __branchList.Append(",");
                    }

                    __branchList.Append("\'" + getBranch.ToUpper().Trim() + "\'");

                }

            }


            string __selectTransField = MyLib._myGlobal._fieldAndComma(
               _g.d.ic_trans._doc_date,
                _g.d.ic_trans._doc_no,
               " branch_code as " + _g.d.ic_trans._branch_code,
               _g.d.ic_trans._doc_time,
               _g.d.ic_trans._doc_format_code,
               _g.d.ic_trans._doc_ref_date,
               _g.d.ic_trans._doc_ref,
               _g.d.ic_trans._wh_from,
               _g.d.ic_trans._location_from,
               _g.d.ic_trans._remark
               );

            string __queryLoad = "select " + __selectTransField + " from " + _g.d.ic_trans._table + " where send_success = false and " + __docDateTransFlagWhere + ((__branchList.Length > 0) ? " and branch_code in (" + __branchList.ToString() + ") " : "") + " order by " + _g.d.ic_trans._doc_date + ", doc_no";


            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __queryLoad.ToString());
            if (__result.Tables.Count > 0)
            {
                this._transGrid._loadFromDataTable(__result.Tables[0]);

                for (int __row = 0; __row < this._transGrid._rowData.Count; __row++)
                {
                    this._transGrid._cellUpdate(__row, 0, 1, true);
                }
            }
        }

        void _loadDataProcess(int row)
        {
            string __docNo = this._transGrid._cellGet(row, _g.d.ic_trans._doc_no).ToString();
            // load data to grid
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            switch (this._transFlag)
            {
                case 72:
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from ic_trans_detail where doc_no = \'" + __docNo + "\' and trans_flag in (" + this._transFlag + ", 70) order by line_number "));
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from ic_trans where doc_no = \'" + __docNo + "\' and trans_flag in (" + this._transFlag + ", 70) "));

                    }
                    break;
                default:
                    {
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from ic_trans_detail where doc_no = \'" + __docNo + "\' and trans_flag =" + this._transFlag + " order by line_number "));

                    }
                    break;
            }

            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Count > 0)
            {
                this._gridTransDetail._loadFromDataTable(((DataSet)__result[0]).Tables[0]);

                if (_transFlag == 72)
                {
                    this._gridAppendDoc._loadFromDataTable(((DataSet)__result[1]).Tables[0]);

                }
            }
            else
            {
                MessageBox.Show("error query" + __query.ToString());
            }
        }

        void _process()
        {
            if (MessageBox.Show("ยืนยันการโอนข้อมูล", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                for (int row = 0; row < this._transGrid._rowData.Count; row++)
                {
                    string __checked = this._transGrid._cellGet(row, 0).ToString();
                    if (__checked == "1")
                    {
                        string __docDate = MyLib._myGlobal._convertDateToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date));
                        string __branchSync = this._transGrid._cellGet(row, _g.d.ic_trans._branch_code).ToString();
                        string __docNo = this._transGrid._cellGet(row, _g.d.ic_trans._doc_no).ToString();


                        this._gridTransDetail._clear();

                        // load data เข้า grid ก่อน

                        _loadDataProcess(row);

                        //return;

                        StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                        switch (this._transFlag)
                        {
                            case 72:
                                {
                                    // delete old
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\' and " + _g.d.ic_trans._trans_flag + " in (" + this._transFlag + ",70)"));
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\' and " + _g.d.ic_trans._trans_flag + " in (" + this._transFlag + ",70)"));

                                    /*
                                    string __doc_ref_no = ((this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date) != null && this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date).ToString().Length > 0) ? "\'" + MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date)) + "\'" : "null");
                                    // 
                                    string __transField = MyLib._myGlobal._fieldAndComma(
                                        _g.d.ic_trans._doc_no,
                                        _g.d.ic_trans._doc_date,
                                        _g.d.ic_trans._branch_code,
                                        _g.d.ic_trans._doc_time,
                                        _g.d.ic_trans._doc_format_code,
                                        _g.d.ic_trans._doc_ref_date,
                                        _g.d.ic_trans._doc_ref,
                                        _g.d.ic_trans._wh_from,
                                        _g.d.ic_trans._location_from,
                                        _g.d.ic_trans._remark,
                                        _g.d.ic_trans._trans_flag,
                                        _g.d.ic_trans._trans_type
                                        );

                                    string __transValue = MyLib._myGlobal._fieldAndComma(
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_no).ToString() + "\'",
                                       "\'" + MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date)) + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._branch_code).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_time).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_format_code).ToString() + "\'",
                                       ((this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date) != null && MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date)) == "1000-01-01") ? "\'" + MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date)) + "\'" : "null"),
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._wh_from).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._location_from).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._remark).ToString() + "\'",
                                       this._transFlag.ToString(),
                                       _g.g._transTypeGlobal._transType(_g.g._transFlagGlobal._transFlagByNumber(this._transFlag)).ToString()
                                       );

                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into ic_trans(" + __transField + ") values(" + __transValue + ")"));*/

                                    this._gridAppendDoc._updateRowIsChangeAll(true);
                                    __query.Append(this._gridAppendDoc._createQueryForInsert(_g.d.ic_trans._table, "is_lock_record,", "1,", false, false));

                                    this._gridTransDetail._updateRowIsChangeAll(true);
                                    __query.Append(this._gridTransDetail._createQueryForInsert(_g.d.ic_trans_detail._table, "", "", false, false));

                                }
                                break;
                            default:
                                {
                                    // delete old
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + this._transFlag));
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + this._transFlag));

                                    string __doc_ref_no = ((this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date) != null && this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date).ToString().Length > 0) ? "\'" + MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date)) + "\'" : "null");
                                    // 
                                    string __transField = MyLib._myGlobal._fieldAndComma(
                                        _g.d.ic_trans._doc_no,
                                        _g.d.ic_trans._doc_date,
                                        _g.d.ic_trans._branch_code,
                                        _g.d.ic_trans._doc_time,
                                        _g.d.ic_trans._doc_format_code,
                                        _g.d.ic_trans._doc_ref_date,
                                        _g.d.ic_trans._doc_ref,
                                        _g.d.ic_trans._wh_from,
                                        _g.d.ic_trans._location_from,
                                        _g.d.ic_trans._remark,
                                        _g.d.ic_trans._trans_flag,
                                        _g.d.ic_trans._trans_type,
                                        "is_lock_record"
                                        );

                                    string __transValue = MyLib._myGlobal._fieldAndComma(
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_no).ToString() + "\'",
                                       "\'" + MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date)) + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._branch_code).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_time).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_format_code).ToString() + "\'",
                                       ((this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date) != null && MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref_date)) == "1000-01-01") ? "\'" + MyLib._myGlobal._convertDateTimeToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date)) + "\'" : "null"),
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._doc_ref).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._wh_from).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._location_from).ToString() + "\'",
                                       "\'" + this._transGrid._cellGet(row, _g.d.ic_trans._remark).ToString() + "\'",
                                       this._transFlag.ToString(),
                                       _g.g._transTypeGlobal._transType(_g.g._transFlagGlobal._transFlagByNumber(this._transFlag)).ToString(),
                                       "1"
                                       );

                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into ic_trans(" + __transField + ") values(" + __transValue + ")"));

                                    this._gridTransDetail._updateRowIsChangeAll(true);
                                    __query.Append(this._gridTransDetail._createQueryForInsert(_g.d.ic_trans_detail._table, "", "", false, true));

                                }
                                break;
                        }




                        __query.Append("</node>");

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_global._datacenter_server, "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml", _global._datacenter_database_type);

                        // string __debugQuery = __query.ToString();
                        string __result = __myFrameWork._queryList(_global._datacenter_database_name, __query.ToString());

                        if (__result.Length == 0)
                        {
                            //MyLib._myGlobal._displayWarning(1, "");
                            //this._clear();

                            // update status 
                            this._transGrid._cellUpdate(row, _g.d.ic_trans._status, "Success", true);

                            MyLib._myFrameWork __dataFrameWork = new MyLib._myFrameWork();
                            __dataFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update ic_trans set send_success = true where doc_no = \'" + __docNo + "\' and trans_flag = " + this._transFlag);

                        }
                        else
                        {
                            MessageBox.Show(__result, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        this._transGrid._cellUpdate(row, 0, 0, true);
                    }
                }
            }
        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            //
            _loadData();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _process();
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._transGrid._rowData.Count; __row++)
            {
                this._transGrid._cellUpdate(__row, 0, 1, true);
            }
            this._transGrid.Invalidate();

        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._transGrid._rowData.Count; __row++)
            {
                this._transGrid._cellUpdate(__row, 0, 0, true);
            }
            this._transGrid.Invalidate();

        }
    }


}
