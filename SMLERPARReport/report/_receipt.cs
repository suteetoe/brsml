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
    public partial class _receipt : UserControl
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
        private string _show_cancel_doc;
        private DataTable _conditionFromTo;
        private string _title = MyLib._myResource._findResource(_g.d.resource_report_name._table + "." + _g.d.resource_report_name._ar_receipt, _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ar_receipt)._str;
        private decimal _sum1;
        private decimal _sum2;
        private decimal _sum3;
        private decimal _sum4;
        private decimal _sum5;
        private decimal _sum6;
        private decimal _sum7;
        private decimal _sum8;
        private decimal _sum9;
        private string[] _order_by_column = { _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_date,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_no,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_code,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._remark,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_debt_value,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_cash,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_transfer,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_credit,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_chq,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_pay_tax};

        public _receipt()
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
                string __payQuery = "";
                string __query = "";
                string __queryDetail = "";
                if (this._to_docno.Trim().Length == 0) this._to_docno = this._from_docno;
                try
                {
                    //where screen===========================================================================================
                    __getWhereScreen.Append(String.Format("({0}=39) and ({1} between \'{2}\' and \'{3}\')",
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
                    //SubQuery
                    __custNameQuery = String.Format("(select {2} from {0} where {1}={3}.{4}) as {5}",
                        _g.d.ar_customer._table,        //{0}
                        _g.d.ar_customer._code,         //{1}
                        _g.d.ar_customer._name_1,       //{2}
                        _g.d.ap_ar_trans._table,        //{3}
                        _g.d.ap_ar_trans._cust_code,    //{4}
                        _g.d.ap_ar_trans._cust_name);   //{5}
                    __payQuery = String.Format("(select coalesce(sum({0}),0) from {1} where {1}.{2}={3}.{4}) as {5}",
                        _g.d.ap_ar_trans_detail._sum_pay_money,
                        _g.d.ap_ar_trans_detail._table,
                        _g.d.ap_ar_trans_detail._doc_no,
                        _g.d.ap_ar_trans._table,
                        _g.d.ap_ar_trans._doc_no,
                        _g.d.ap_ar_trans._total_pay_money);
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

                    __query = String.Format("select {0},{1},{2},{3},{4},coalesce({5},0) as {5},coalesce({6},0) as {6},coalesce({7},0) as {7},coalesce({8},0) as {8},coalesce({9},0) as {9},coalesce({10},0) as {10},0 as {11},{12},{13} from {14} {15} {16} {17}",
                        _g.d.ap_ar_trans._doc_date,                                                         //{0}
                        _g.d.ap_ar_trans._doc_no,                                                           //{1}
                        _g.d.ap_ar_trans._cust_code,                                                        //{2}
                        __custNameQuery,                                                                    //{3}
                        _g.d.ap_ar_trans._remark,                                                           //{4}
                        _g.d.ap_ar_trans._total_debt_value,                                                 //{5}
                        _g.d.ap_ar_trans._sum_pay_money_cash,                                               //{6}
                        _g.d.ap_ar_trans._sum_pay_money_transfer,                                           //{7}
                        _g.d.ap_ar_trans._sum_pay_money_credit,                                             //{8}
                        _g.d.ap_ar_trans._sum_pay_money_chq,                                                //{9}
                        _g.d.ap_ar_trans._total_pay_tax,                                                    //{10}
                        "diff",                                                                             //{11}
                        __payQuery,                                                                         //{12}
                        _g.d.ap_ar_trans._last_status,                                                      //{13}
                        _g.d.ap_ar_trans._table,                                                            //{14}
                        __getWhereScreen,                                                                   //{15}
                        __getWhereGrid,                                                                     //{16}
                        __getUserWhere1);                                                                   //{17}
                    __query = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},({5}-{12}) as {13},{14} from ({15}) as {16} {17} {18}",
                        _g.d.ap_ar_trans._doc_date,                                                         //{0}
                        _g.d.ap_ar_trans._doc_no,                                                           //{1}
                        _g.d.ap_ar_trans._cust_code,                                                        //{2}
                        _g.d.ap_ar_trans._cust_name,                                                        //{3}
                        _g.d.ap_ar_trans._remark,                                                           //{4}
                        _g.d.ap_ar_trans._total_debt_value,                                                 //{5}
                        _g.d.ap_ar_trans._sum_pay_money_cash,                                               //{6}
                        _g.d.ap_ar_trans._sum_pay_money_transfer,                                           //{7}
                        _g.d.ap_ar_trans._sum_pay_money_credit,                                             //{8}
                        _g.d.ap_ar_trans._sum_pay_money_chq,                                                //{9}
                        _g.d.ap_ar_trans._total_pay_tax,                                                    //{10}
                        "diff",                                                                             //{11}
                        _g.d.ap_ar_trans._total_pay_money,                                                  //{12}
                        _g.d.ap_ar_trans._total_debt_balance,                                               //{13}
                        _g.d.ap_ar_trans._last_status,                                                      //{14}
                        __query,                                                                            //{15}
                        _g.d.ap_ar_trans._table,                                                            //{16}
                        __getUserWhere2,                                                                    //{17}
                        __orderBy);                                                                         //{18}
                    //where detail===========================================================================================
                    __getWhereDetail = String.Format("({0}=39) and ({1} in (select {2} from ({3}) as {4}))",
                        _g.d.ap_ar_trans_detail._trans_flag,                                                //{0}
                        _g.d.ap_ar_trans_detail._doc_no,                                                    //{1}
                        _g.d.ap_ar_trans._doc_no,                                                           //{2}
                        __query,                                                                            //{3}
                        _g.d.ap_ar_trans._table);                                                           //{4}
                    //query detail==========================================================================================
                    __queryDetail = String.Format("select {0},{1},{2},{3},coalesce({4},0) as {4},coalesce({5},0) as {5},coalesce({6},0) as {6} from {7} where {8} order by {9}",
                        _g.d.ap_ar_trans_detail._doc_no,                                                    //{0}
                        _g.d.ap_ar_trans_detail._billing_no,                                                //{1}
                        _g.d.ap_ar_trans_detail._billing_date,                                              //{2}
                        _g.d.ap_ar_trans_detail._due_date,                                                  //{3}
                        _g.d.ap_ar_trans_detail._sum_debt_value,                                            //{4}
                        _g.d.ap_ar_trans_detail._sum_pay_money,                                             //{5}
                        _g.d.ap_ar_trans_detail._sum_debt_balance,                                          //{6}
                        _g.d.ap_ar_trans_detail._table,                                                     //{7}
                        __getWhereDetail,                                                                   //{8}
                        _g.d.ap_ar_trans_detail._line_number);                                              //{9}

                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                    if (this._show_detail == "1")
                    {
                        this._dataTableDetail = this._myFrameWork._query(MyLib._myGlobal._databaseName, __queryDetail.ToString()).Tables[0];
                    }
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
                this._detailObject1 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                this._view1._addColumn(this._detailObject1, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._doc_date, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_date, _g.d.resource_report_column._doc_date, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._doc_no, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_no, _g.d.resource_report_column._doc_no, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._ar_code, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._ar_code, _g.d.resource_report_column._ar_code, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._ar_name, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._ar_name, _g.d.resource_report_column._ar_name, SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._remark, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._remark, _g.d.resource_report_column._remark, SMLReport._report._cellAlign.Left);
                float __columnWidth = 56f / 9f;
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_debt_balance, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_cash, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_transfer, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_credit, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_chq, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_pay_vat, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._difference, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_pay_money, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_debt_remain, "", SMLReport._report._cellAlign.Right);
                this._detailObject2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                if (this._show_detail == "1")
                {
                    this._view1._addColumn(this._detailObject2, 2, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._doc_ref, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_ref, _g.d.resource_report_column._doc_ref, SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._doc_ref_date, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_ref_date, _g.d.resource_report_column._doc_ref_date, SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 32, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._due_date, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._due_date, _g.d.resource_report_column._due_date, SMLReport._report._cellAlign.Left);
                    this._view1._addColumn(this._detailObject2, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_debt_value, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_debt_value, _g.d.resource_report_column._sum_debt_value, SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_pay_money, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_pay_money, _g.d.resource_report_column._sum_pay_money, SMLReport._report._cellAlign.Right);
                    this._view1._addColumn(this._detailObject2, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.resource_report_column._sum_debt_balance, _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_debt_balance, _g.d.resource_report_column._sum_debt_balance, SMLReport._report._cellAlign.Right);
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
            Font __cancelFont = new Font(__documentFont.Name, __documentFont.Size, FontStyle.Strikeout);
            Font __newFont = null;
            Font __detailFont = null;
            string __dateTimeString = "";
            decimal __value = 0;
            int __cancelDocument = 0;
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
            this._sum8 = 0;
            this._sum9 = 0;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                //__cancelDocument = MyLib._myGlobal._intPhase(__dataRows[__row].ItemArray[9].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[9].ToString());
                __cancelDocument = MyLib._myGlobal._intPhase(__dataRows[__row][_g.d.ap_ar_trans._last_status].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._last_status].ToString());
                if (this._show_cancel_doc == "1")
                {
                    if (__cancelDocument == 0)
                    {
                        __newFont = __documentFont;
                        __detailFont = null;
                    }
                    else if (__cancelDocument == 1)
                    {
                        __newFont = __cancelFont;
                        __detailFont = __cancelFont;
                    }
                }
                else
                {
                    if (__cancelDocument == 0)
                    {
                        __newFont = __documentFont;
                        __detailFont = null;
                    }
                    else if (__cancelDocument == 1) continue;
                }
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(this._detailObject1, __dataObject);
                __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRows[__row].ItemArray[0].ToString()).ToShortDateString();
                this._view1._addDataColumn(this._detailObject1, __dataObject, 0, __dateTimeString, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 4, __dataRows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "total_debt_value"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_cash"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_transfer"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_credit"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "sum_pay_money_chq"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "total_pay_vat"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 11, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "diff"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 12, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "total_pay_money"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[5].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[5].ToString()) - Decimal.Parse(__dataRows[__row].ItemArray[12].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[12].ToString());
                __dataRows[__row].ItemArray[13] = __value;
                this._view1._addDataColumn(this._detailObject1, __dataObject, 13, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "total_debt_balance"), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[5].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[5].ToString());
                if (__cancelDocument == 0) this._sum1 += __value;
                __value = Decimal.Parse(__dataRows[__row].ItemArray[6].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[6].ToString());
                if (__cancelDocument == 0) this._sum2 += __value;
                __value = Decimal.Parse(__dataRows[__row].ItemArray[7].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[7].ToString());
                if (__cancelDocument == 0) this._sum3 += __value;
                __value = Decimal.Parse(__dataRows[__row].ItemArray[8].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[8].ToString());
                if (__cancelDocument == 0) this._sum5 += __value;
                __value = Decimal.Parse(__dataRows[__row].ItemArray[9].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[9].ToString());
                if (__cancelDocument == 0) this._sum5 += __value;
                __value = Decimal.Parse(__dataRows[__row].ItemArray[10].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[10].ToString());
                if (__cancelDocument == 0) this._sum6 += __value;
                __value = Decimal.Parse(__dataRows[__row].ItemArray[11].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[11].ToString());
                if (__cancelDocument == 0) this._sum7 += __value;
                __value = Decimal.Parse(__dataRows[__row].ItemArray[12].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[12].ToString());
                if (__cancelDocument == 0) this._sum8 += __value;
                //__value = Decimal.Parse(__dataRows[__row].ItemArray[13].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[13].ToString());
                __value = Decimal.Parse(__dataRows[__row].ItemArray[5].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[5].ToString()) - Decimal.Parse(__dataRows[__row].ItemArray[12].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[12].ToString());
                if (__cancelDocument == 0) this._sum9 += __value;
                __countData++;
                if (this._show_detail == "1" && this._dataTableDetail != null && this._dataTableDetail.Rows.Count > 0)
                {
                    DataRow[] __dataRowsDetail = this._dataTableDetail.Select(_g.d.ap_ar_trans_detail._doc_no + "='" + __dataRows[__row].ItemArray[1].ToString() + "'");
                    for (int __rowDetail = 0; __rowDetail < __dataRowsDetail.Length; __rowDetail++)
                    {
                        __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        this._view1._createEmtryColumn(this._detailObject2, __dataObject);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 0, "", __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 1, __dataRowsDetail[__rowDetail].ItemArray[1].ToString(), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRowsDetail[__rowDetail].ItemArray[2].ToString()).ToShortDateString();
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 2, __dateTimeString, __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRowsDetail[__rowDetail].ItemArray[3].ToString()).ToShortDateString();
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 3, __dateTimeString, __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, "sum_debt_value"), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, "sum_pay_money"), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        this._view1._addDataColumn(this._detailObject2, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRowsDetail, __rowDetail, "sum_debt_balance"), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
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
            __dataTable.Columns.Add("sum8");
            __dataTable.Columns.Add("sum9");
            __dataTable.Rows.Add(this._sum1, this._sum2, this._sum3, this._sum4, this._sum5, this._sum6, this._sum7, this._sum8, this._sum9);
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
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 1, __sum_string, __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
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
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 12, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum8"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 13, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum9"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
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
                this._form_condition = new _condition_form(SMLERPARAPInfo._apArConditionEnum.receipt, SMLERPARAPInfo._apArConditionEnum.receipt.ToString(), this._title);
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

                this._show_cancel_doc = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._show_cancel_document);

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}