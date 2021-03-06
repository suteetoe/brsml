﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPCASHBANKReport._condition;

namespace SMLERPCASHBANKReport._report_chq
{
    public partial class _cb_chqoutpayment : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __ojtReport;
        SMLReport._report._objectListType __ojtReportDetail;
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m01");
        //_cbCondition _myCondition = new _cbCondition();        
        bool _openpop = false;
        private string _company = "";
        DataTable _ds;
        DataTable _conditionFromTo;
        private _condition_form _myCondition;
        private string[] _cb_detail_column = {_g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_no,
                                             _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_date,
                                             _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._chq_number};


        public _cb_chqoutpayment()
        {
            InitializeComponent();
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            //////////////////_view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._showCondition();
        }

        void _view1__loadDataByThread()
        {
            if (this._ds == null)
            {
                try
                {
                    //where user control
                    string __getUserWhere1 = this._myCondition._whereControl._getWhere1("");
                    __getUserWhere1 = __getUserWhere1.Replace("\"", "");
                    __getUserWhere1 = __getUserWhere1.Replace("where", "and");
                    string __getUserWhere2 = this._myCondition._whereControl._getWhere2();
                    __getUserWhere2 = __getUserWhere2.Replace("\"", "");
                    __getUserWhere2 = __getUserWhere2.Replace("where", "and");
                    //where
                    //=======================================================================================================
                    //StringBuilder __where = new StringBuilder("(balance_amount<>0) and ");
                    StringBuilder __where = new StringBuilder();
                    //=======================================================================================================
                    if (this._conditionFromTo != null && this._conditionFromTo.Rows.Count > 0)
                    {
                        for (int __row = 0; __row < this._conditionFromTo.Rows.Count; __row++)
                        {
                            __where.Append(string.Format("({0} between \'{1}\' and \'{2}\')",
                                _g.d.cb_trans_detail._doc_no, this._conditionFromTo.Rows[__row][0].ToString(),
                                this._conditionFromTo.Rows[__row][1].ToString()));
                            if (__row != this._conditionFromTo.Rows.Count - 1)
                            {
                                __where.Append(" or ");
                            }
                        }
                    }
                    //=======================================================================================================
                    else
                    {
                        __where.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.cb_trans_detail._doc_no, "0", "z"));
                    }
                    //=======================================================================================================
                    //order by
                    string __orderBy = this._myCondition._whereControl._getOrderBy();
                    __orderBy = __orderBy.Replace("\"", "");
                    //sub query
                    string __query = string.Format("select {1},{2},{3},{4},{5}"
                        + " from {0} where trans_type=2 and trans_flag=43 and  {6} {7} {8} {9}",
                        _g.d.cb_trans_detail._table,          //{0}
                        _g.d.cb_trans_detail._doc_date,       //{1}
                        _g.d.cb_trans_detail._doc_no,         //{2}
                        _g.d.cb_trans_detail._chq_number,//{3}
                        _g.d.cb_trans_detail._remark,         //{4}
                         "(select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=2 and trans_flag=43 ) as sum_amount",  //{5}                        
                        __where,                            //{6}
                        __getUserWhere1,                    //{7}
                        __getUserWhere2,                    //{8}
                        __orderBy);                         //{9}
                    this._ds = this._myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            this._view1._loadDataByThreadSuccess = true;
        }

        private void _showCondition()
        {
            if (this._myCondition == null)
            {
                this._myCondition = new _condition_form(_enum_screen_report_cb._cb_chqoutpayment.ToString());
                this._myCondition._whereControl._tableName = _g.d.cb_trans._table;
                this._myCondition._whereControl._addFieldComboBox(this._cb_detail_column);
            }
            this._myCondition._process = false;
            this._myCondition.ShowDialog();
            if (this._myCondition._process)
            {
                this._ds = null; // จะได้ load data ใหม่

                //this._from_credit = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report.);
                //this._to_credit = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report.);
                //this._from_remain = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report.);
                //this._to_remain = this._form_condition._condition_screen1._getDataStr(_g.d.resource_report.);

                this._conditionFromTo = this._myCondition._condition_grid1._getCondition();

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            ////////////////this._myCondition.TitleName = ":: รายงานเช็คจ่าย ::";
            ////////////////this._myCondition.PageName = "_cb_chqoutpayment";
            ////////////////this._myCondition.ShowDialog();
            ////////////////_openpop = true;
            ////////////////_ds = null;
            this._showCondition();
        }

        void _view1__getDataObject()
        {
            try
            {
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m01");
                DataRow[] _dr = _ds.Select("");
                double __total_amount = 0;
                for (int _row = 0; _row < _dr.Length; _row++)
                {
                    SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                    _view1._addDataColumn(__ojtReport, __dataObject, 0, _dr[_row].ItemArray[0].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row].ItemArray[1].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row].ItemArray[2].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row].ItemArray[3].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 4, _dr[_row].ItemArray[4].ToString(), null, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);

                    if (_row == (_dr.Length - 1))
                    {
                        _row++;
                        SMLReport._report._objectListType ___dataTotalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.All);
                        _view1._createEmtryColumn(__ojtReport, ___dataTotalObject);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 0, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 1, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 4, "", __newFont, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(":: ERR :: \n" + ex.Message);
            }
        }

        //////////////bool _view1__loadData()
        //////////////{
        //////////////    try
        //////////////    {
        //////////////        String _getConditonResult = "";
        //////////////        if (_openpop) _getConditonResult = (this._myCondition.Result.Equals("")) ? "" : " and " + this._myCondition.Result;

        //////////////        string _query = "select doc_date,doc_no,chq_number,remark,"
        //////////////            + "(select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=2 and trans_flag=43 )"
        //////////////            + " as sum_amount"
        //////////////            + " from cb_trans_detail where trans_type=2 and trans_flag=43 " + _getConditonResult;
        //////////////        _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, _query);

        //////////////    }
        //////////////    catch
        //////////////    {
        //////////////        this.Cursor = Cursors.Default;
        //////////////        _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        //////////////        return false;
        //////////////    }
        //////////////    _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        //////////////    this.Cursor = Cursors.Default;
        //////////////    return true;
        //////////////}

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                _getCompanyValue();
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, this._company, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานเช็คจ่าย", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เช็ค", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หมายเหตุ/รายละเอียด", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนเงิน", "", SMLReport._report._cellAlign.Default);
                    __ojtReportDetail = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.Bottom);


                    return true;
                }
            return false;
        }

        public void _getCompanyValue()
        {
            DataSet __getLastCode = _myFrameWork._query(MyLib._myGlobal._databaseName, "select company_name_1  from  erp_company_profile order by company_name_1 asc");
            if (__getLastCode.Tables[0].Rows.Count > 0)
            {
                this._company = __getLastCode.Tables[0].Rows[0].ItemArray[0].ToString();
            }
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            /////////////////_view1._buildReport(SMLReport._report._reportType.Standard);
            if (this._myCondition._condition_screen1._checkEmtryField().Length != 0)
            {
                                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้กำหนดเงื่อนไข"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this._ds = null; // จะได้ load data ใหม่
            this._view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this._view1._buildReport(SMLReport._report._reportType.Standard);
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }

    }
}
