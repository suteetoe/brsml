using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPAPReport.condition;

namespace SMLERPAPReport.report
{
    public partial class _payment_cancel : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType _detailObject1;
        private SMLReport._report._objectListType _detailObject2;
        private SMLReport._report._objectListType _detailObjectTotal;
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private _condition_form _form_condition;
        private string _from_doc_date;
        private string _to_doc_date;
        private string _from_docno;
        private string _to_docno;
        private string _show_detail;
        private DataTable _conditionFromTo;
        private string _title = MyLib._myResource._findResource(_g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_payment_cancel, _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_payment_cancel)._str;
        private decimal _sum1;
        private decimal _sum2;
        private decimal _sum3;
        private decimal _sum4;
        private decimal _sum5;
        private decimal _sum6;
        private decimal _sum7;
        private string[] _order_by_column = { _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_date,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_no,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._ap_code,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._ap_name,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._remark,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_value,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_discount,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_vat_value,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_pay_tax,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_debt_value,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_pay_money,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_debt_balance};

        public _payment_cancel()
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
            if (this._dataTable == null || this._dataTableDetail == null)
            {
                StringBuilder __getWhereScreen = new StringBuilder();
                StringBuilder __getWhereGrid = new StringBuilder();
                string __getUserWhere1 = "";
                string __getUserWhere2 = "";
                string __getWhereDetail = "";
                string __orderBy = "";
                string __custNameQuery = "";
                string __query = "";
                string __queryDetail = "";
                if (this._to_docno.Trim().Length == 0) this._to_docno = this._from_docno;
                try
                {
                    //where screen===========================================================================================
                    __getWhereScreen.Append(String.Format("({0}=20) and ({1} between \'{2}\' and \'{3}\')",
                        _g.d.ap_ar_trans._trans_flag,
                        _g.d.ap_ar_trans._doc_date,
                        this._from_doc_date,
                        this._to_doc_date));
                    if (this._from_docno.Trim().Length > 0) __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')", _g.d.ap_ar_trans._doc_no, this._from_docno, this._to_docno));
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
                    __getUserWhere1 = this._form_condition._whereUserControl1._getWhere1("");
                    __getUserWhere1 = __getUserWhere1.Replace("\"", "");
                    __getUserWhere1 = __getUserWhere1.Replace("where", "");
                    __getUserWhere2 = this._form_condition._whereUserControl1._getWhere2();
                    __getUserWhere2 = __getUserWhere2.Replace("\"", "");
                    __getUserWhere2 = __getUserWhere2.Replace("where", "");
                    if (__getUserWhere2.Length > 0) __getUserWhere2 = "where " + __getUserWhere2;
                    //order by===============================================================================================
                    __orderBy = this._form_condition._whereUserControl1._getOrderBy();
                    __orderBy = __orderBy.Replace("\"", "");
                    //SubQuery===============================================================================================
                    __custNameQuery = String.Format("(select {2} from {0} where {1}={3}.{4}) as {5}",
                        _g.d.ap_supplier._table,        //{0}
                        _g.d.ap_supplier._code,         //{1}
                        _g.d.ap_supplier._name_1,       //{2}
                        _g.d.ap_ar_trans._table,        //{3}
                        _g.d.ap_ar_trans._cust_code,    //{4}
                        _g.d.ap_ar_trans._ap_name);     //{5}
                    //query==================================================================================================
                    if (__getWhereScreen.Length > 0)
                    {
                        __getWhereScreen.Insert(0, "where ");
                        if (__getWhereGrid.Length > 0) __getWhereGrid.Insert(0, "and ");
                        if (__getUserWhere1.Length > 0) __getUserWhere1 = "and " + __getUserWhere1;
                    }
                    else if (__getWhereGrid.Length > 0)
                    {
                        __getWhereGrid.Insert(0, "where ");
                        if (__getUserWhere1.Length > 0) __getUserWhere1 = "and " + __getUserWhere1;
                    }
                    else
                    {
                        if (__getUserWhere1.Length > 0) __getUserWhere1 = "where " + __getUserWhere1;
                    }

                    __query = String.Format("select {0},{1},{2},{3},{4},coalesce({5},0) as {5},coalesce({6},0) as {6},coalesce({7},0) as {7},coalesce({8},0) as {8},coalesce({9},0) as {9},coalesce({10},0) as {10},coalesce({11},0) as {11},{12} from {13} {14} {15} {16}",
                        _g.d.ap_ar_trans._doc_date,                                                         //{0}
                        _g.d.ap_ar_trans._doc_no,                                                           //{1}
                        _g.d.ap_ar_trans._cust_code + " as " + _g.d.ap_ar_trans._ap_code,                   //{2}
                        __custNameQuery,                                                                    //{3}
                        _g.d.ap_ar_trans._remark,                                                           //{4}
                        _g.d.ap_ar_trans._total_value,                                                      //{5}
                        _g.d.ap_ar_trans._total_discount,                                                   //{6}
                        _g.d.ap_ar_trans._total_vat_value,                                                  //{7}
                        _g.d.ap_ar_trans._total_pay_tax,                                                    //{8}
                        _g.d.ap_ar_trans._total_debt_value,                                                 //{9}
                        _g.d.ap_ar_trans._total_pay_money,                                                  //{10}
                        _g.d.ap_ar_trans._total_debt_balance,                                               //{11}
                        _g.d.ap_ar_trans._last_status,                                                      //{12}
                        _g.d.ap_ar_trans._table,                                                            //{13}
                        __getWhereScreen,                                                                   //{14}
                        __getWhereGrid,                                                                     //{15}
                        __getUserWhere1);                                                                   //{16}
                    __query = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12} from ({13}) as {14} {15} {16}",
                        _g.d.ap_ar_trans._doc_date,                                                         //{0}
                        _g.d.ap_ar_trans._doc_no,                                                           //{1}
                        _g.d.ap_ar_trans._ap_code,                                                          //{2}
                        _g.d.ap_ar_trans._ap_name,                                                          //{3}
                        _g.d.ap_ar_trans._remark,                                                           //{4}
                        _g.d.ap_ar_trans._total_value,                                                      //{5}
                        _g.d.ap_ar_trans._total_discount,                                                   //{6}
                        _g.d.ap_ar_trans._total_vat_value,                                                  //{7}
                        _g.d.ap_ar_trans._total_pay_tax,                                                    //{8}
                        _g.d.ap_ar_trans._total_debt_value,                                                 //{9}
                        _g.d.ap_ar_trans._total_pay_money,                                                  //{10}
                        _g.d.ap_ar_trans._total_debt_balance,                                               //{11}
                        _g.d.ap_ar_trans._last_status,                                                      //{12}
                        __query,                                                                            //{13}
                        _g.d.ap_ar_trans._table,                                                            //{14}
                        __getUserWhere2,                                                                    //{15}
                        __orderBy);                                                                         //{16}
                    //where detail===========================================================================================
                    __getWhereDetail = String.Format("({0}=19) and ({1} in (select {2} from ({3}) as {4}))",
                        _g.d.ap_ar_trans_detail._trans_flag,                                                //{0}
                        _g.d.ap_ar_trans_detail._doc_no,                                                    //{1}
                        _g.d.ap_ar_trans._doc_no,                                                           //{2}
                        __query,                                                                            //{3}
                        _g.d.ap_ar_trans._table);                                                           //{4}
                    //query detail==========================================================================================
                    __queryDetail = String.Format("select {0},{1},{2},{3},coalesce({4},0) as {4},coalesce({5},0) as {5},coalesce({6},0) as {6},coalesce({7},0) as {7},coalesce({8},0) as {8},coalesce({9},0) as {9},coalesce({10},0) as {10},coalesce({11},0) as {11} from {12} where {13} order by {14}",
                        _g.d.ap_ar_trans_detail._doc_no,                                                    //{0}
                        _g.d.ap_ar_trans_detail._billing_no,                                                //{1}
                        _g.d.ap_ar_trans_detail._billing_date,                                              //{2}
                        _g.d.ap_ar_trans_detail._due_date,                                                  //{3}
                        _g.d.ap_ar_trans_detail._sum_value,                                                 //{4}
                        _g.d.ap_ar_trans_detail._sum_before_vat,                                            //{5}
                        _g.d.ap_ar_trans_detail._sum_tax_value,                                             //{6}
                        _g.d.ap_ar_trans_detail._sum_discount,                                              //{7}
                        _g.d.ap_ar_trans_detail._sum_debt_value,                                            //{8}
                        _g.d.ap_ar_trans_detail._balance_ref,                                               //{9}
                        _g.d.ap_ar_trans_detail._sum_pay_money,                                             //{10}
                        _g.d.ap_ar_trans_detail._sum_debt_balance,                                          //{11}
                        _g.d.ap_ar_trans_detail._table,                                                     //{12}
                        __getWhereDetail,                                                                   //{13}
                        //_g.d.ap_ar_trans_detail._line_number);                                              //{14}
                        _g.d.ap_ar_trans_detail._bill_type);                                                //{14}

                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                    if (this._show_detail == "1")
                    {
                        this._dataTableDetail = this._myFrameWork._query(MyLib._myGlobal._databaseName, __queryDetail.ToString()).Tables[0];
                    }
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
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
                this._detailObject1 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                this._view1._addColumn(this._detailObject1, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_date, "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_no, "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._ap_code, "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._ap_name, "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._remark, "", SMLReport._report._cellAlign.Left);
                float __columnWidth = 57f / 7f;
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าสินค้า", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._total_discount, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._total_vat_value, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ภาษีหัก ณ ที่จ่าย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รวมมูลค่า", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._total_pay_money, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดคงเหลือ", "", SMLReport._report._cellAlign.Right);
                this._detailObject2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                if (this._show_detail == "1")
                {
                    this._view1._addColumn(this._detailObject2, 2, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_ref, "", SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_ref_date, "", SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 18, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._due_date, "", SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_value, "", SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดก่อนภาษี", "", SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_tax_value, "", SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ส่วนลด", "", SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_debt_value, "", SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "คงเหลือปัจจุบัน", "", SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_pay_money, "", SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_debt_balance, "", SMLReport._report._cellAlign.Right);
                }

                this._detailObjectTotal = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None, false);
                this._view1._addColumn(this._detailObjectTotal, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);

                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            //Font __documentFont = (this._show_detail == "1") ? new Font("Angsana New", 12, FontStyle.Bold) : new Font("Angsana New", 12, FontStyle.Regular);
            Font __documentFont = (this._show_detail == "1") ? new Font("Angsana New", 10, FontStyle.Bold) : new Font("Angsana New", 10, FontStyle.Regular);
            Font __newFont = __documentFont;
            Font __detailFont = null;
            string __dateTimeString = "";
            decimal __value = 0;
            int __countData = 0;
            SMLReport._report._objectListType __dataObject = null;

            if (this._dataTable == null) return;
            this._sum1 = 0;
            this._sum2 = 0;
            this._sum3 = 0;
            this._sum4 = 0;
            this._sum5 = 0;
            this._sum6 = 0;
            this._sum7 = 0;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(this._detailObject1, __dataObject);
                __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRows[__row][_g.d.ap_ar_trans._doc_date].ToString()).ToShortDateString();
                this._view1._addDataColumn(this._detailObject1, __dataObject, 0, __dateTimeString, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 1, __dataRows[__row][_g.d.ap_ar_trans._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 2, __dataRows[__row][_g.d.ap_ar_trans._ap_code].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 3, __dataRows[__row][_g.d.ap_ar_trans._ap_name].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 4, __dataRows[__row][_g.d.ap_ar_trans._remark].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_discount), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_vat_value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_pay_tax), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_debt_value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_pay_money), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 11, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_debt_balance), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_value].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_value].ToString());
                this._sum1 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_discount].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_discount].ToString());
                this._sum2 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_vat_value].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_vat_value].ToString());
                this._sum3 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_pay_tax].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_pay_tax].ToString());
                this._sum4 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_debt_value].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_debt_value].ToString());
                this._sum5 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_pay_money].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_pay_money].ToString());
                this._sum6 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_debt_balance].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_debt_balance].ToString());
                this._sum7 += __value;
                __countData++;
                if (this._show_detail == "1" && this._dataTableDetail != null && this._dataTableDetail.Rows.Count > 0)
                {
                    DataRow[] __dataRowsDetail = this._dataTableDetail.Select(_g.d.ap_ar_trans_detail._doc_no + "='" + __dataRows[__row][_g.d.ap_ar_trans._doc_no].ToString() + "'");
                    for (int __rowDetail = 0; __rowDetail < __dataRowsDetail.Length; __rowDetail++)
                    {
                        __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(this._detailObject2, __dataObject);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 0, "", __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRowsDetail[__rowDetail][_g.d.ap_ar_trans_detail._billing_date].ToString()).ToShortDateString();
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 1, __dateTimeString, __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 2, __dataRowsDetail[__rowDetail][_g.d.ap_ar_trans_detail._billing_no].ToString(), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRowsDetail[__rowDetail][_g.d.ap_ar_trans_detail._due_date].ToString()).ToShortDateString();
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 3, __dateTimeString, __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._sum_value), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._sum_before_vat), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._sum_tax_value), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._sum_discount), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._sum_debt_value), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._balance_ref), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._sum_pay_money), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 11, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, _g.d.ap_ar_trans_detail._sum_debt_balance), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    }
                }
            }
            this._sumData(__dataObject, __countData);
        }

        private void _sumData(SMLReport._report._objectListType __dataObject, int __numItem)
        {
            DataTable __dataTable = new DataTable();
            __dataTable.Columns.Add("sum1");
            __dataTable.Columns.Add("sum2");
            __dataTable.Columns.Add("sum3");
            __dataTable.Columns.Add("sum4");
            __dataTable.Columns.Add("sum5");
            __dataTable.Columns.Add("sum6");
            __dataTable.Columns.Add("sum7");
            __dataTable.Rows.Add(this._sum1, this._sum2, this._sum3, this._sum4, this._sum5, this._sum6, this._sum7);
            DataRow[] __dataRows = __dataTable.Select();
            //Font __totalFont = new Font("Angsana New", 12, FontStyle.Bold);
            Font __totalFont = new Font("Angsana New", 10, FontStyle.Bold);
            string __sum_string = String.Format("{0} {1} {2}",
                MyLib._myResource._findResource(_g.d.resource_report._table + "." + _g.d.resource_report._sum_all_item, _g.d.resource_report._table + "." + _g.d.resource_report._sum_all_item)._str,
                __numItem,
                MyLib._myResource._findResource(_g.d.resource_report._table + "." + _g.d.resource_report._sum_item_list, _g.d.resource_report._table + "." + _g.d.resource_report._sum_item_list)._str);
            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            this._view1._createEmtryColumn(this._detailObjectTotal, __dataObject);
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 0, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 1, __sum_string, __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            if (__numItem > 0)
            {
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 2, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 3, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 4, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum1"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum2"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum3"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum4"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum5"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum6"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 11, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum7"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
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
            this._dataTableDetail = null;
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
                this._form_condition = new _condition_form(_enum_screen_report_ap.payment_cancel.ToString(), this._title);
                this._form_condition._whereUserControl1._tableName = _g.d.ap_ar_trans._table;
                this._form_condition._whereUserControl1._addFieldComboBox(this._order_by_column);
            }
            this._form_condition._process = false;
            this._form_condition.ShowDialog();

            if (this._form_condition._process)
            {
                this._dataTable = null; // จะได้ load data ใหม่
                this._dataTableDetail = null;

                this._conditionFromTo = this._form_condition._condition_grid1._getCondition();

                this._from_doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_docdate)));

                this._to_doc_date = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._convertDate(this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_docdate)));

                this._from_docno = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_docno);

                this._to_docno = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_docno);

                this._show_detail = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._display_detail);

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}