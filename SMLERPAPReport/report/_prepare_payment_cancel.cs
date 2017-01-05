﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPAPReport.condition;

namespace SMLERPAPReport.report
{
    public partial class _prepare_payment_cancel : UserControl
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
        private string _from_doc_no;
        private string _to_doc_no;
        private DataTable _conditionFromTo;
        private string _title = MyLib._myResource._findResource(_g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_detail, "รายงานยกเลิกการเตรียมจ่ายชำระหนี้")._str;
        private string[] _order_by_column = { _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_date,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_no,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_code,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._remark,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_net_value,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_vat_value,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_discount,
                                             _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_pay_money};

        public _prepare_payment_cancel()
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
            this._view1._pageSetupDialog.PageSettings.Landscape = true;

            this._view1._fontHeader1 = new Font("Angsana New", 18, FontStyle.Bold);
            this._view1._fontHeader2 = new Font("Angsana New", 14, FontStyle.Bold);
            this._view1._fontStandard = new Font("Angsana New", 12, FontStyle.Regular);

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
                string __query = "";
                string __queryDetail = "";
                try
                {
                    //where screen===========================================================================================
                    __getWhereScreen.Append(String.Format("({0}=16) and ({1} between \'{2}\' and \'{3}\')",
                        _g.d.ap_ar_trans._trans_flag,
                        _g.d.ap_ar_trans._doc_date,
                        this._from_doc_date,
                        this._to_doc_date));
                    if (this._from_doc_no.Trim().Length > 0)
                    {
                        __getWhereScreen.Append(String.Format(" and ({0} between \'{1}\' and \'{2}\')",
                            _g.d.ap_ar_trans._doc_no,
                            this._from_doc_no,
                            this._to_doc_no));
                    }
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
                        if (__getWhereGrid.Length > 0) __getWhereGrid.Insert(0, " and ");
                    }
                    //where user control=====================================================================================
                    __getUserWhere1 = this._form_condition._whereUserControl1._getWhere1("");
                    __getUserWhere1 = __getUserWhere1.Replace("\"", "");
                    __getUserWhere1 = __getUserWhere1.Replace("where", "and");
                    __getUserWhere2 = this._form_condition._whereUserControl1._getWhere2();
                    __getUserWhere2 = __getUserWhere2.Replace("\"", "");
                    __getUserWhere2 = __getUserWhere2.Replace("where", "and");
                    //order by===============================================================================================
                    __orderBy = this._form_condition._whereUserControl1._getOrderBy();
                    __orderBy = __orderBy.Replace("\"", "");
                    ////query==================================================================================================
                    //__query = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8} from {9} where {10} {11} {12} {13} {14}",
                    //    _g.d.ap_ar_trans._doc_date,                                                         //{0}
                    //    _g.d.ap_ar_trans._doc_no,                                                           //{1}
                    //    _g.d.ap_ar_trans._cust_code,                                                        //{2}
                    //    "(select name_1 from ar_customer where code=ap_ar_trans.cust_code) as cust_name",   //{3}
                    //    _g.d.ap_ar_trans._remark,                                                           //{4}
                    //    _g.d.ap_ar_trans._total_net_value,                                                 //{5}
                    //    _g.d.ap_ar_trans._total_vat_value,                                                  //{6}
                    //    _g.d.ap_ar_trans._total_discount,                                                   //{7}
                    //    _g.d.ap_ar_trans._total_pay_money,                                                  //{8}
                    //    _g.d.ap_ar_trans._table,                                                            //{9}
                    //    __getWhereScreen,                                                                   //{10}
                    //    __getWhereGrid,                                                                     //{11}
                    //    __getUserWhere1,                                                                    //{12}
                    //    __getUserWhere2,                                                                    //{13}
                    //    __orderBy);                                                                         //{14}
                    ////where detail===========================================================================================
                    //__getWhereDetail = String.Format("({0}=2) and ({1}=39) and ({2} in (select {3} from ({4}) as {5}))",
                    //    _g.d.ap_ar_trans_detail._trans_type,                                                //{0}
                    //    _g.d.ap_ar_trans_detail._trans_flag,                                                //{1}
                    //    _g.d.ap_ar_trans_detail._doc_no,                                                    //{2}
                    //    _g.d.ap_ar_trans._doc_no,                                                           //{3}
                    //    __query,                                                                            //{4}
                    //    _g.d.ap_ar_trans._table);                                                           //{5}
                    ////query detail==========================================================================================
                    //__queryDetail = String.Format("select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10} from {11} where {12} {13} {0},{1},{2},{3},{5},{6}",
                    //    _g.d.ap_ar_trans_detail._doc_no,                                                    //{0}
                    //    _g.d.ap_ar_trans_detail._billing_no,                                                //{1}
                    //    _g.d.ap_ar_trans_detail._billing_date,                                              //{2}
                    //    _g.d.ap_ar_trans_detail._billing_no,                                                //{3}
                    //    "\'\' as doc_ref",                                                                  //{4}
                    //    _g.d.ap_ar_trans_detail._remark,                                                    //{5}
                    //    _g.d.ap_ar_trans_detail._doc_group,                                                 //{6}
                    //    "coalesce(sum(" + _g.d.ap_ar_trans_detail._sum_pay_money_cash + "),0) as " + _g.d.ap_ar_trans_detail._sum_pay_money_cash,           //{7}
                    //    "coalesce(sum(" + _g.d.ap_ar_trans_detail._sum_pay_money_chq + "),0) as " + _g.d.ap_ar_trans_detail._sum_pay_money_chq,             //{8}
                    //    "coalesce(sum(" + _g.d.ap_ar_trans_detail._sum_pay_money_credit + "),0) as " + _g.d.ap_ar_trans_detail._sum_pay_money_credit,       //{9}
                    //    "coalesce(sum(" + _g.d.ap_ar_trans_detail._sum_pay_money_transfer + "),0) as " + _g.d.ap_ar_trans_detail._sum_pay_money_transfer,   //{10}
                    //    _g.d.ap_ar_trans_detail._table,                                                     //{11}
                    //    __getWhereDetail,                                                                   //{12}
                    //    "group by ");                                                                       //{13}

                    this._dataTable = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString()).Tables[0];
                    this._dataTableDetail = this._myFrameWork._query(MyLib._myGlobal._databaseName, __queryDetail.ToString()).Tables[0];
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
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หมายเหตุ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดรวม", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รายได้อื่นๆ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ค่าใช้จ่ายอื่นๆ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดชำระหนี้", "", SMLReport._report._cellAlign.Right);
                //พิมพ์ชื่อฟิลด์
                this._detailObject2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(this._detailObject2, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ใบส่งของ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ใบส่งของ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject2, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หมายเหตุ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ประเภทเอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดคงเหลือ ณ ตอนจ่าย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดตัดจ่าย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(this._detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดกำไร/ขาดทุน", "", SMLReport._report._cellAlign.Right);

                this._detailObjectTotal = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None, false);
                this._view1._addColumn(this._detailObjectTotal, 80, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(this._detailObjectTotal, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Left);

                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = new Font("Angsana New", 12, FontStyle.Bold);
            Font __detailFont = null;
            decimal __value = 0;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)this._detailObject1._columnList[0];
            SMLReport._report._objectListType __dataObject = null;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");

            if (this._dataTable == null) return;
            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(this._detailObject1, __dataObject);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 0, __dataRows[__row].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(this._detailObject1, __dataObject, 4, __dataRows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[5].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[5].ToString());
                this._view1._addDataColumn(this._detailObject1, __dataObject, 5, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[6].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[6].ToString());
                this._view1._addDataColumn(this._detailObject1, __dataObject, 6, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[7].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[7].ToString());
                this._view1._addDataColumn(this._detailObject1, __dataObject, 7, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[8].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[8].ToString());
                this._view1._addDataColumn(this._detailObject1, __dataObject, 8, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                __value = Decimal.Parse(__dataRows[__row].ItemArray[9].ToString().Equals("") ? "0" : __dataRows[__row].ItemArray[9].ToString());
                this._view1._addDataColumn(this._detailObject1, __dataObject, 9, string.Format(__formatNumber, __value), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);

                if (this._dataTableDetail.Rows.Count == 0) continue;
                DataRow[] __dataRowsDetail = this._dataTableDetail.Select(_g.d.ap_ar_trans_detail._doc_no + "='" + __dataRows[__row].ItemArray[1].ToString() + "'");
                for (int __rowDetail = 0; __rowDetail < __dataRowsDetail.Length; __rowDetail++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(this._detailObject2, __dataObject);
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 0, "", __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 1, __dataRowsDetail[__rowDetail].ItemArray[1].ToString(), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 2, __dataRowsDetail[__rowDetail].ItemArray[2].ToString(), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 3, __dataRowsDetail[__rowDetail].ItemArray[3].ToString(), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 4, __dataRowsDetail[__rowDetail].ItemArray[4].ToString(), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 5, __dataRowsDetail[__rowDetail].ItemArray[5].ToString(), __detailFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    __value = Decimal.Parse(__dataRowsDetail[__rowDetail].ItemArray[6].ToString().Equals("") ? "0" : __dataRowsDetail[__rowDetail].ItemArray[6].ToString());
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 6, string.Format(__formatNumber, __value), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    __value = Decimal.Parse(__dataRowsDetail[__rowDetail].ItemArray[7].ToString().Equals("") ? "0" : __dataRowsDetail[__rowDetail].ItemArray[7].ToString());
                    this._view1._addDataColumn(this._detailObject2, __dataObject, 7, string.Format(__formatNumber, __value), __detailFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                }
            }
            this._sumData(__dataObject, __dataRows.Length);
        }

        private void _sumData(SMLReport._report._objectListType __dataObject, int __numItem)
        {
            Font __totalFont = new Font("Angsana New", 12, FontStyle.Bold);
            __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
            this._view1._createEmtryColumn(this._detailObjectTotal, __dataObject);
            this._view1._createEmtryColumn(this._detailObjectTotal, __dataObject);
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 0, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 1, "", __totalFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
            this._view1._addDataColumn(this._detailObjectTotal, __dataObject, 2, "รวมทั้งสิ้น " + __numItem + " รายการ", __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
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
                this._form_condition = new _condition_form(_enum_screen_report_ap.payment.ToString(), this._title);
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

                this._from_doc_no = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._from_docno);

                this._to_doc_no = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report._to_docno);

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
