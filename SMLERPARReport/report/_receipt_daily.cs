using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPARAPReport.condition;

namespace SMLERPARAPReport.report
{
    public partial class _receipt_daily : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType _detailObject;
        private SMLReport._report._objectListType _detailObjectTotal;
        private DataTable _dataTable;
        private _condition_form _form_condition;
        private string _from_doc_date;
        private string _to_doc_date;
        private DataTable _conditionFromTo;
        private string _title = MyLib._myResource._findResource(_g.d.resource_report_name._table + "." + _g.d.resource_report_name._ar_receipt_daily, _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ar_receipt_daily)._str;
        private decimal _total_cash;
        private decimal _total_cheque;
        private decimal _total_transfer;
        private decimal _total_credit;
        private decimal _total_diff;
        private decimal _total_sum;
        private decimal _total_vat_at_pay;
        private string[] _order_by_column ={_g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_date,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_no,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_name,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._remark,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_cash,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_chq,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_transfer,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_credit,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_diff,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_pay_money};

        public _receipt_daily()
        {
            InitializeComponent();
            this._view1._pageSetupDialog.PageSettings.Landscape = true;
            this._view1._buttonExample.Enabled = false;
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            //this._view1._fontHeader1 = new Font("Angsana New", 18, FontStyle.Bold);
            //this._view1._fontHeader2 = new Font("Angsana New", 14, FontStyle.Bold);
            //this._view1._fontStandard = new Font("Angsana New", 12, FontStyle.Regular);
            this._view1._fontHeader1 = new Font("Angsana New", 16, FontStyle.Bold);
            this._view1._fontHeader2 = new Font("Angsana New", 12, FontStyle.Bold);
            this._view1._fontStandard = new Font("Angsana New", 10, FontStyle.Regular);

            this._showCondition();
        }

        void _view1__loadDataByThread()
        {
            if (this._dataTable == null)
            {
                StringBuilder __getWhereScreen = new StringBuilder();
                StringBuilder __getWhereGrid = new StringBuilder();
                //string __getUserWhere1 = "";
                string __getUserWhere2 = "";
                string __orderBy = "";
                string __ar_query1 = "";
                string __ar_query2 = "";
                string __case_query = "";
                string __ic_trans_query = "";
                string __ap_ar_trans_query = "";
                string __vat_at_pay_query1 = "";
                string __vat_at_pay_query2 = "";
                string __query = "";
                try
                {
                    //where screen===========================================================================================
                    __getWhereScreen.Append(String.Format("({0} between \'{1}\' and \'{2}\')",
                        _g.d.ap_ar_trans._doc_date,
                        this._from_doc_date,
                        this._to_doc_date));
                    //where grid=============================================================================================
                    if (this._conditionFromTo != null && this._conditionFromTo.Rows.Count > 0)
                    {
                        for (int __row = 0; __row < this._conditionFromTo.Rows.Count; __row++)
                        {
                            __getWhereGrid.Append(string.Format("({0} between \'{1}\' and \'{2}\')",
                                _g.d.ap_ar_trans._cust_code,
                                this._conditionFromTo.Rows[__row][0].ToString(),
                                this._conditionFromTo.Rows[__row][1].ToString()));
                            if (__row != this._conditionFromTo.Rows.Count - 1)
                            {
                                __getWhereGrid.Append(" or ");
                            }
                        }
                    }
                    //where user control=====================================================================================
                    //__getUserWhere1 = this._form_condition._whereUserControl1._getWhere1("");
                    //__getUserWhere1 = __getUserWhere1.Replace("\"", "");
                    //__getUserWhere1 = __getUserWhere1.Replace("where", "");
                    __getUserWhere2 = this._form_condition._whereUserControl1._getWhere2();
                    __getUserWhere2 = __getUserWhere2.Replace("\"", "");
                    __getUserWhere2 = __getUserWhere2.Replace("where", "");
                    if (__getUserWhere2.Length > 0) __getUserWhere2 = "where " + __getUserWhere2;
                    //order by===============================================================================================
                    __orderBy = this._form_condition._whereUserControl1._getOrderBy();
                    __orderBy = __orderBy.Replace("\"", "");
                    //SubQuery===============================================================================================
                    __ar_query1 = String.Format("select {0} from {1} where {1}.{2}={3}.{4}",
                        _g.d.ar_customer._name_1,
                        _g.d.ar_customer._table,
                        _g.d.ar_customer._code,
                        _g.d.ic_trans._table,
                        _g.d.ic_trans._cust_code);
                    __ar_query2 = String.Format("select {0} from {1} where {1}.{2}={3}.{4}",
                        _g.d.ar_customer._name_1,
                        _g.d.ar_customer._table,
                        _g.d.ar_customer._code,
                        _g.d.ap_ar_trans._table,
                        _g.d.ap_ar_trans._cust_code);
                    __case_query = String.Format("case when ({0}=40) then \'รับเงินล่วงหน้า\' "
                                                    + "when ({0}=44) and ({1}=1) then \'{2}\' "
                                                    + "when ({0}=44) and ({1}=2) then \'{3}\' "
                                                + "end",
                                _g.d.ic_trans._trans_flag,
                                _g.d.ic_trans._inquiry_type,
                                MyLib._myResource._findResource(_g.d.ic_trans._table + "." + _g.d.ic_trans._cash_sale, _g.d.ic_trans._table + "." + _g.d.ic_trans._cash_sale)._str,
                                MyLib._myResource._findResource(_g.d.ic_trans._table + "." + _g.d.ic_trans._credit_sale_service, _g.d.ic_trans._table + "." + _g.d.ic_trans._credit_sale_service)._str);
                    __vat_at_pay_query1 = String.Format("select coalesce(sum({0}),0) as {0} from {1} where {1}.{2}={3}.{4}",
                        _g.d.gl_wht_list_detail._tax_value,
                        _g.d.gl_wht_list_detail._table,
                        _g.d.gl_wht_list_detail._doc_no,
                        _g.d.ic_trans._table,
                        _g.d.ic_trans._doc_no);
                    __vat_at_pay_query2 = String.Format("select coalesce(sum({0}),0) as {0} from {1} where {1}.{2}={3}.{4}",
                        _g.d.gl_wht_list_detail._tax_value,
                        _g.d.gl_wht_list_detail._table,
                        _g.d.gl_wht_list_detail._doc_no,
                        _g.d.ap_ar_trans._table,
                        _g.d.ap_ar_trans._doc_no);
                    //query==================================================================================================
                    if (__getWhereScreen.Length > 0)
                    {
                        __getWhereScreen.Insert(0, "and ");
                        if (__getWhereGrid.Length > 0) __getWhereGrid.Insert(0, "and ");
                        //if (__getUserWhere1.Length > 0) __getUserWhere1 = "and " + __getUserWhere1;
                    }
                    else if (__getWhereGrid.Length > 0)
                    {
                        __getWhereGrid.Insert(0, "and ");
                        //if (__getUserWhere1.Length > 0) __getUserWhere1 = "and " + __getUserWhere1;
                    }
                    //else
                    //{
                    //    if (__getUserWhere1.Length > 0) __getUserWhere1 = "where " + __getUserWhere1;
                    //}

                    __ic_trans_query = String.Format("select {0},{1},({2}) as {3},{4},coalesce({5},0) as {5},coalesce({20},0) as {20},coalesce({6},0) as {6},coalesce({7},0) as {7},coalesce({8},0) as {8},coalesce({9},0) as {10},({11}) as {12},({13}) as {14} from {15} where ({16} in (40)) or (({16} in (44)) and ({17} in (1,2))) {18} {19}",
                    _g.d.ic_trans._doc_date,                //{0}
                    _g.d.ic_trans._doc_no,                  //{1}
                    __ar_query1,                            //{2}
                    _g.d.ap_ar_trans._cust_name,            //{3}
                    _g.d.ic_trans._remark,                  //{4}
                    _g.d.ic_trans._sum_pay_money_cash,      //{5}
                    _g.d.ic_trans._sum_pay_money_chq,       //{6}
                    _g.d.ic_trans._sum_pay_money_transfer,  //{7}
                    _g.d.ic_trans._sum_pay_money_credit,    //{8}
                    _g.d.ic_trans._pay_amount,              //{9}
                    _g.d.ap_ar_trans._total_pay_money,      //{10}
                    __case_query,                           //{11}
                    "tag",                                  //{12}
                    __vat_at_pay_query1,                    //{13}
                    "vat_at_pay",                           //{14}
                    _g.d.ic_trans._table,                   //{15}
                    _g.d.ic_trans._trans_flag,              //{16}
                    _g.d.ic_trans._inquiry_type,            //{17}
                    __getWhereScreen,                       //{18}
                    __getWhereGrid,                         //{19}
                    _g.d.ic_trans._sum_pay_money_diff);     //{20}
                    //
                    __ap_ar_trans_query = String.Format("select {0},{1},({2}) as {3},{4},coalesce({5},0) as {5},coalesce({19},0) as {19},coalesce({6},0) as {6},coalesce({7},0) as {7},coalesce({8},0) as {8},coalesce({9},0) as {10},\'{11}\' as {12},({13}) as {14} from {15} where ({16} in (39)) {17} {18}",
                    _g.d.ap_ar_trans._doc_date,                 //{0}
                    _g.d.ap_ar_trans._doc_no,                   //{1}
                    __ar_query2,                                //{2}
                    _g.d.ap_ar_trans._cust_name,                //{3}
                    _g.d.ap_ar_trans._remark,                   //{4}
                    _g.d.ap_ar_trans._sum_pay_money_cash,       //{5}
                    _g.d.ap_ar_trans._sum_pay_money_chq,        //{6}
                    _g.d.ap_ar_trans._sum_pay_money_transfer,   //{7}
                    _g.d.ap_ar_trans._sum_pay_money_credit,     //{8}
                    _g.d.ap_ar_trans._total_pay_money,          //{9}
                    _g.d.ap_ar_trans._total_pay_money,          //{10}
                    "รับชำระหนี้",                                 //{11}
                    "tag",                                      //{12}
                    __vat_at_pay_query2,                        //{13}
                    "vat_at_pay",                               //{14}
                    _g.d.ap_ar_trans._table,                    //{15}
                    _g.d.ap_ar_trans._trans_flag,               //{16}
                    __getWhereScreen,                           //{17}
                    __getWhereGrid,                             //{18}
                    _g.d.ap_ar_trans._sum_pay_money_diff);      //{19}

                    __query = String.Format("({0}) union ({1})", __ic_trans_query, __ap_ar_trans_query);
                    __query = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{15} from ({11}) as {12} {13} {14}",
                        _g.d.ap_ar_trans._doc_date,                 //{0}
                        _g.d.ap_ar_trans._doc_no,                   //{1}
                        _g.d.ap_ar_trans._cust_name,                //{2}
                        _g.d.ap_ar_trans._remark,                   //{3}
                        _g.d.ap_ar_trans._sum_pay_money_cash,       //{4}
                        _g.d.ap_ar_trans._sum_pay_money_chq,        //{5}
                        _g.d.ap_ar_trans._sum_pay_money_transfer,   //{6}
                        _g.d.ap_ar_trans._sum_pay_money_credit,     //{7}
                        _g.d.ap_ar_trans._total_pay_money,          //{8}
                        "tag",                                      //{9}
                        "vat_at_pay",                               //{10}
                        __query,                                    //{11}
                        _g.d.ap_ar_trans._table,                    //{12}
                        __getUserWhere2,                            //{13}
                        __orderBy,                                  //{14}
                        _g.d.ap_ar_trans._sum_pay_money_diff);      //{15}
                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                    return;
                }
            }
            this._view1._loadDataByThreadSuccess = true;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "Title\t: " + this._title, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Printed By\t: " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Printed Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Description\t: ", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                this._detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                this._detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(this._detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._doc_date, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_date, _g.d.resource_report_column._doc_date, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._doc_no, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_no, _g.d.resource_report_column._doc_no, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._ar_name, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._ar_name, _g.d.resource_report_column._ar_name, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._remark, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._remark, _g.d.resource_report_column._remark, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_cash, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_cash, _g.d.resource_report_column._sum_cash, SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_chq, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_chq, _g.d.resource_report_column._sum_chq, SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_transfer, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_transfer, _g.d.resource_report_column._sum_transfer, SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_credit, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_credit, _g.d.resource_report_column._sum_credit, SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._difference, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._difference, _g.d.resource_report_column._difference, SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_total, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_total, _g.d.resource_report_column._sum_total, SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หัก ณ ที่จ่าย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, 11, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._detail, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._detail, _g.d.resource_report_column._detail, SMLReport._report._cellAlign.Left);

                this._detailObjectTotal = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None, false);
                this._view1._addColumn(this._detailObjectTotal, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 11, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);

                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = null;
            decimal __value = 0;
            string __dateTimeString = "";
            SMLReport._report._objectListType __dataObject = null;

            if (this._dataTable == null) return;
            this._total_cash = 0;
            this._total_cheque = 0;
            this._total_transfer = 0;
            this._total_credit = 0;
            this._total_diff = 0;
            this._total_sum = 0;
            this._total_vat_at_pay = 0;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(this._detailObject, __dataObject);
                __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRows[__row].ItemArray[0].ToString()).ToShortDateString();
                this._view1._addDataColumn(this._detailObject, __dataObject, 0, __dateTimeString, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_cash"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_chq"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_transfer"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_credit"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_diff"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "total_pay_money"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "vat_at_pay"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                //this._view1._addDataColumn(this._detailObject, __dataObject, 10, __dataRows[__row].ItemArray[11].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_cash].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[4].ToString());
                this._total_cash += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_chq].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[5].ToString());
                this._total_cheque += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_transfer].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[6].ToString());
                this._total_transfer += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_credit].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[7].ToString());
                this._total_credit += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_diff].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[7].ToString());
                this._total_diff += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_pay_money].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[8].ToString());
                this._total_sum += __value;
                __value = Decimal.Parse(__dataRows[__row]["vat_at_pay"].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[10].ToString());
                this._total_vat_at_pay += __value;
            }
            this._sumData(__dataObject, __dataRows.Length);
        }

        private void _sumData(SMLReport._report._objectListType __dataObject, int __numItem)
        {
            DataTable __dataTable = new DataTable();
            __dataTable.Columns.Add("total_cash");
            __dataTable.Columns.Add("total_cheque");
            __dataTable.Columns.Add("total_transfer");
            __dataTable.Columns.Add("total_credit");
            __dataTable.Columns.Add("total_diff");
            __dataTable.Columns.Add("total_sum");
            __dataTable.Columns.Add("total_vat_at_pay");
            __dataTable.Rows.Add(this._total_cash, this._total_cheque, this._total_transfer, this._total_credit, this._total_diff, this._total_sum, this._total_vat_at_pay);
            DataRow[] __dataRows = __dataTable.Select();
            //Font __totalFont = new Font("Angsana New", 12, FontStyle.Bold);
            Font __totalFont = new Font("Angsana New", 10, FontStyle.Bold);
            string __sum_string = String.Format("{0} {1} {2}",
                MyLib._myResource._findResource(_g.d.resource_report._table + "." + _g.d.resource_report._sum_all_item, _g.d.resource_report._table + "." + _g.d.resource_report._sum_all_item)._str,
                __numItem,
                MyLib._myResource._findResource(_g.d.resource_report._table + "." + _g.d.resource_report._sum_item_list, _g.d.resource_report._table + "." + _g.d.resource_report._sum_item_list)._str);
            //
            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            this._view1._createEmtryColumn(this._detailObjectTotal, __dataObject);
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 0, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 1, __sum_string, __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
            if (this._dataTable.Rows.Count > 0)
            {
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 2, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 3, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "total_cash"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "total_cheque"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "total_transfer"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "total_credit"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "total_diff"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "total_sum"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "total_vat_at_pay"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 11, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            }
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (this._form_condition._condition_screen1._checkEmtryField().Length != 0)
            {
                MessageBox.Show(MyLib._myResource._findResource(_g.d.resource_report._table + "." + _g.d.resource_report._must_set_condition, _g.d.resource_report._table + "." + _g.d.resource_report._must_set_condition)._str, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this._dataTable = null; // จะได้ load data ใหม่
            this._view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this._view1._buildReport(SMLReport._report._reportType.Standard);
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new _condition_form(SMLERPARAPInfo._apArConditionEnum.receipt_daily, SMLERPARAPInfo._apArConditionEnum.receipt_daily.ToString(), this._title);
                //this._form_condition._whereUserControl1._tableName = _g.d.ap_ar_trans._table;
                this._form_condition._whereUserControl1._addFieldComboBox(this._order_by_column);
            }
            this._form_condition._process = false;
            this._form_condition.ShowDialog();

            if (this._form_condition._process)
            {
                this._dataTable = null; // จะได้ load data ใหม่

                this._conditionFromTo = this._form_condition._condition_grid1._getCondition();

                this._from_doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_docdate)));

                this._to_doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_docdate)));

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
