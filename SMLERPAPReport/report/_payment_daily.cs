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
    public partial class _payment_daily : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType _detailObject;
        private SMLReport._report._objectListType _detailObjectTotal;
        private DataTable _dataTable;
        private _condition_form _form_condition;
        private string _from_doc_date;
        private string _to_doc_date;
        private DataTable _conditionFromTo;
        private string _title = MyLib._myResource._findResource(_g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_payment_daily, _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_payment_daily)._str;
        private decimal _sum1;
        private decimal _sum2;
        private decimal _sum3;
        private decimal _sum4;
        private decimal _sum5;
        private decimal _sum6;
        private decimal _sum7;
        private decimal _sum8;
        private decimal _sum9;
        private decimal _sum10;
        private string[] _order_by_column ={_g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_date,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_no,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_name,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._remark,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_debt_value,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_cash,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_chq,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_transfer,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._sum_pay_money_credit,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_pay_tax,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_pay_money,
                                            _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_debt_balance};

        public _payment_daily()
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
                string __ap_query1 = "";
                string __ap_query2 = "";
                string __credit_charge_query1 = "";
                string __credit_charge_query2 = "";
                string __pay_vat_query1 = "";
                string __pay_vat_query2 = "";
                string __ic_trans_query = "";
                string __ap_ar_trans_query = "";
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
                    __ap_query1 = String.Format("(select {0} from {1} where {1}.{2}={3}.{4}) as {5}",
                        _g.d.ap_supplier._name_1,       //{0}
                        _g.d.ap_supplier._table,        //{1}
                        _g.d.ap_supplier._code,         //{2}
                        _g.d.ic_trans._table,           //{3}
                        _g.d.ic_trans._cust_code,       //{4}
                        _g.d.ap_ar_trans._ap_name);     //{5}
                    __ap_query2 = String.Format("(select {0} from {1} where {1}.{2}={3}.{4}) as {5}",
                        _g.d.ap_supplier._name_1,       //{0}
                        _g.d.ap_supplier._table,        //{1}
                        _g.d.ap_supplier._code,         //{2}
                        _g.d.ap_ar_trans._table,        //{3}
                        _g.d.ap_ar_trans._cust_code,    //{4}
                        _g.d.ap_ar_trans._ap_name);     //{5}
                    __credit_charge_query1 = String.Format("(select coalesce({0},0) from {1} where {1}.{2}={3}.{4}) as {0}",
                        _g.d.cb_credit_card._charge,    //{0}
                        _g.d.cb_credit_card._table,     //{1}
                        _g.d.cb_credit_card._doc_ref,   //{2}
                        _g.d.ic_trans._table,           //{3}
                        _g.d.ic_trans._doc_no);         //{4}
                    __credit_charge_query2 = String.Format("(select coalesce({0},0) from {1} where {1}.{2}={3}.{4}) as {0}",
                        _g.d.cb_credit_card._charge,    //{0}
                        _g.d.cb_credit_card._table,     //{1}
                        _g.d.cb_credit_card._doc_ref,   //{2}
                        _g.d.ap_ar_trans._table,        //{3}
                        _g.d.ap_ar_trans._doc_no);      //{4}
                    __pay_vat_query1 = String.Format("(select coalesce(sum({0}),0) as {0} from {1} where {1}.{2}={3}.{4}) as {5}",
                        _g.d.gl_wht_list_detail._tax_value, //{0}
                        _g.d.gl_wht_list_detail._table,     //{1}
                        _g.d.gl_wht_list_detail._doc_no,    //{2}
                        _g.d.ic_trans._table,               //{3}
                        _g.d.ic_trans._doc_no,              //{4}
                        _g.d.ap_ar_trans._total_pay_tax);   //{5}
                    __pay_vat_query2 = String.Format("(select coalesce(sum({0}),0) as {0} from {1} where {1}.{2}={3}.{4}) as {5}",
                        _g.d.gl_wht_list_detail._tax_value, //{0}
                        _g.d.gl_wht_list_detail._table,     //{1}
                        _g.d.gl_wht_list_detail._doc_no,    //{2}
                        _g.d.ap_ar_trans._table,            //{3}
                        _g.d.ic_trans._doc_no,              //{4}
                        _g.d.ap_ar_trans._total_pay_tax);   //{5}
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

                    __ic_trans_query = String.Format("select {0},{1},{2},{3},{4},coalesce({5},0) as {6},coalesce({7},0) as {7},coalesce({8},0) as {8},coalesce({9},0) as {9},coalesce({10},0) as {10}," +
                        "{11},{12},{13},coalesce({14},0) as {15},coalesce({16},0) as {17} " +
                        "from {18} where last_status=0 and (({19} in (10)) or (({19} in (12)) and ({20} in (1,2)))) {21} {22}",
                        _g.d.ic_trans._doc_date,                                        //{0}
                        _g.d.ic_trans._doc_no,                                          //{1}
                        _g.d.ic_trans._cust_code + " as " + _g.d.ap_ar_trans._ap_code,  //{2}
                        __ap_query1,                                                    //{3}
                        _g.d.ic_trans._remark,                                          //{4}
                        _g.d.ic_trans._total_debt_amount,                               //{5}
                        _g.d.ap_ar_trans._total_debt_value,                             //{6}
                        _g.d.ic_trans._sum_pay_money_cash,                              //{7}
                        _g.d.ic_trans._sum_pay_money_chq,                               //{8}
                        _g.d.ic_trans._sum_pay_money_transfer,                          //{9}
                        _g.d.ic_trans._sum_pay_money_credit,                            //{10}
                        __credit_charge_query1,                                         //{11}
                        __pay_vat_query1,                                               //{12}
                        "0 as diff",                                                    //{13}
                        _g.d.ic_trans._pay_amount,                                      //{14}
                        _g.d.ap_ar_trans._total_pay_money,                              //{15}
                        _g.d.ic_trans._balance_amount,                                  //{16}
                        _g.d.ap_ar_trans._total_debt_balance,                           //{17}
                        _g.d.ic_trans._table,                                           //{18}
                        _g.d.ic_trans._trans_flag,                                      //{19}
                        _g.d.ic_trans._inquiry_type,                                    //{20}
                        __getWhereScreen,                                               //{21}
                        __getWhereGrid);                                                //{22}

                    __ap_ar_trans_query = String.Format("select {0},{1},{2},{3},{4},coalesce({5},0) as {5},coalesce({6},0) as {6},coalesce({7},0) as {7},coalesce({8},0) as {8},coalesce({9},0) as {9}," +
                        "{10},coalesce({11},0) as {11},{12},coalesce({13},0) as {13},coalesce({14},0) as {14} " +
                        "from {15} where last_status=0 and ({16} in (19)) {17} {18}",
                        _g.d.ap_ar_trans._doc_date,                                         //{0}
                        _g.d.ap_ar_trans._doc_no,                                           //{1}
                        _g.d.ap_ar_trans._cust_code + " as " + _g.d.ap_ar_trans._ap_code,   //{2}
                        __ap_query2,                                                        //{3}
                        _g.d.ap_ar_trans._remark,                                           //{4}
                        _g.d.ap_ar_trans._total_debt_value,                                 //{5}
                        _g.d.ap_ar_trans._sum_pay_money_cash,                               //{6}
                        _g.d.ap_ar_trans._sum_pay_money_chq,                                //{7}
                        _g.d.ap_ar_trans._sum_pay_money_transfer,                           //{8}
                        _g.d.ap_ar_trans._sum_pay_money_credit,                             //{9}
                        __credit_charge_query2,                                             //{10}
                        _g.d.ap_ar_trans._total_pay_tax,                                    //{11}
                        "0 as diff",                                                        //{12}
                        _g.d.ap_ar_trans._total_pay_money,                                  //{13}
                        _g.d.ap_ar_trans._total_debt_balance,                               //{14}
                        _g.d.ap_ar_trans._table,                                            //{15}
                        _g.d.ap_ar_trans._trans_flag,                                       //{16}
                        __getWhereScreen,                                                   //{17}
                        __getWhereGrid);                                                    //{18}

                    __query = String.Format("({0}) union ({1})", __ic_trans_query, __ap_ar_trans_query);
                    __query = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14} from ({15}) as {16} {17} {18}",
                        _g.d.ap_ar_trans._doc_date,                 //{0}
                        _g.d.ap_ar_trans._doc_no,                   //{1}
                        _g.d.ap_ar_trans._ap_code,                  //{2}
                        _g.d.ap_ar_trans._ap_name,                  //{3}
                        _g.d.ap_ar_trans._remark,                   //{4}
                        _g.d.ap_ar_trans._total_debt_value,         //{5}
                        _g.d.ap_ar_trans._sum_pay_money_cash,       //{6}
                        _g.d.ap_ar_trans._sum_pay_money_chq,        //{7}
                        _g.d.ap_ar_trans._sum_pay_money_transfer,   //{8}
                        _g.d.ap_ar_trans._sum_pay_money_credit,     //{9}
                        _g.d.cb_credit_card._charge,                //{10}
                        _g.d.ap_ar_trans._total_pay_tax,            //{11}
                        "diff",                                     //{12}
                        _g.d.ap_ar_trans._total_pay_money,          //{13}
                        _g.d.ap_ar_trans._total_debt_balance,       //{14}
                        __query,                                    //{15}
                        _g.d.ap_ar_trans._table,                    //{16}
                        __getUserWhere2,                            //{17}
                        __orderBy);                                 //{18}
                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
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
                this._detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                this._detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(this._detailObject, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_date, "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject, 7, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._doc_no, "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._ap_name, "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._remark, "", SMLReport._report._cellAlign.Left);
                float __columnWidth = 66f / 10f;
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รวมมูลค่า", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_cash, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_chq, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_transfer, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._sum_credit, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "Charge", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หัก ณ ที่จ่าย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ผลต่าง", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_column._table + "." + _g.d.resource_report_column._total_pay_money, "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดคงเหลือ", "", SMLReport._report._cellAlign.Right);

                this._detailObjectTotal = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None, false);
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
                this._view1._addColumn(this._detailObjectTotal, __columnWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);

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
            this._sum1 = 0;
            this._sum2 = 0;
            this._sum3 = 0;
            this._sum4 = 0;
            this._sum5 = 0;
            this._sum6 = 0;
            this._sum7 = 0;
            this._sum8 = 0;
            this._sum9 = 0;
            this._sum10 = 0;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(this._detailObject, __dataObject);
                __dateTimeString = MyLib._myGlobal._convertDateFromQuery(__dataRows[__row][_g.d.ap_ar_trans._doc_date].ToString()).ToShortDateString();
                this._view1._addDataColumn(this._detailObject, __dataObject, 0, __dateTimeString, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 1, __dataRows[__row][_g.d.ap_ar_trans._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 2, __dataRows[__row][_g.d.ap_ar_trans._ap_name].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 3, __dataRows[__row][_g.d.ap_ar_trans._remark].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_debt_value), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._sum_pay_money_cash), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._sum_pay_money_chq), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._sum_pay_money_transfer), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._sum_pay_money_credit), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.cb_credit_card._charge), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_pay_tax), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 11, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, "diff"), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 12, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_pay_money), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObject, __dataObject, 13, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, __row, _g.d.ap_ar_trans._total_debt_balance), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_debt_value].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_debt_value].ToString());
                this._sum1 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_cash].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_cash].ToString());
                this._sum2 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_chq].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_chq].ToString());
                this._sum3 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_transfer].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_transfer].ToString());
                this._sum4 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_credit].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._sum_pay_money_credit].ToString());
                this._sum5 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.cb_credit_card._charge].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.cb_credit_card._charge].ToString());
                this._sum6 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_pay_tax].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_pay_tax].ToString());
                this._sum7 += __value;
                __value = Decimal.Parse(__dataRows[__row]["diff"].ToString().Equals("") ? "0" : __dataRows[__row]["diff"].ToString());
                this._sum8 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_pay_money].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_pay_money].ToString());
                this._sum9 += __value;
                __value = Decimal.Parse(__dataRows[__row][_g.d.ap_ar_trans._total_debt_balance].ToString().Equals("") ? "0" : __dataRows[__row][_g.d.ap_ar_trans._total_debt_balance].ToString());
                this._sum10 += __value;
            }
            this._sumData(__dataObject, __dataRows.Length);
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
            __dataTable.Columns.Add("sum10");
            __dataTable.Rows.Add(this._sum1, this._sum2, this._sum3, this._sum4, this._sum5, this._sum6, this._sum7, this._sum8, this._sum9, this._sum10);
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
            if (this._dataTable.Rows.Count > 0)
            {
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 2, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 3, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 4, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum1"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 5, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum2"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 6, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum3"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 7, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum4"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 8, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum5"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 9, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum6"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 10, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum7"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 11, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum8"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 12, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum9"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 13, MyLib._myGlobal._formatNumberForReport(_g.g._companyProfile._item_amount_decimal, __dataRows, 0, "sum10"), __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
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
                this._form_condition = new _condition_form(_enum_screen_report_ap.payment_daily.ToString(), this._title);
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
