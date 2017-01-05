using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPCASHBANKReport._condition;

namespace SMLERPCASHBANKReport._report_cb_ici
{
    public partial class _chq_receive : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __ojtReport;
        SMLReport._report._objectListType __ojtReportDetail;
        //string __formatNumber = MyLib._myGlobal._getFormatNumber("m01");        
        //bool _openpop = false;
        //private string _company = "";
        //private string _title = MyLib._myResource._findResource(_g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_detail, "รายงานรายละเอียดเจ้าหนี้")._str;
        DataTable _ds;
        DataTable _conditionFromTo;
        private _condition_form _myCondition;
        private string[] _cb_detail_column = {_g.d.cb_chq_list._table+"."+_g.d.cb_chq_list._chq_number,
                                             _g.d.cb_chq_list._table+"."+_g.d.cb_chq_list._doc_ref,
                                             _g.d.cb_chq_list._table+"."+_g.d.cb_chq_list._chq_get_date};
        public _chq_receive()
        {
            InitializeComponent();            
            this._view1._buttonExample.Enabled = false;
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);            
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
                        //for (int __row = 0; __row < this._conditionFromTo.Rows.Count; __row++)
                        //{
                        //    __where.Append(string.Format("({0} between \'{1}\' and \'{2}\')",
                        //        _g.d.cb_chq_list._chq_number, this._conditionFromTo.Rows[__row][0].ToString(),
                        //        this._conditionFromTo.Rows[__row][1].ToString()));
                        //    if (__row != this._conditionFromTo.Rows.Count - 1)
                        //    {
                        //        __where.Append(" or ");
                        //    }
                        //}
                    }
                    //=======================================================================================================
                    else
                    {
                        string __fromChqNumber = this._myCondition._condition_screen1._getDataStr(_g.d.resource_report._from_check_number);
                        string __toChqNumber = this._myCondition._condition_screen1._getDataStr(_g.d.resource_report._to_check_number);
                        if (__fromChqNumber.Length != 0 && __toChqNumber.Length != 0)
                        {
                            __where.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.cb_chq_list._chq_number, __fromChqNumber, __toChqNumber));
                        }
                        else if (__fromChqNumber.Length != 0 && __toChqNumber.Length == 0)
                        {
                            __where.Append(string.Format("({0} = \'{1}\')", _g.d.cb_chq_list._chq_number, __fromChqNumber));
                        }
                        else if (__fromChqNumber.Length == 0 && __toChqNumber.Length != 0)
                        {
                            __where.Append(string.Format("({0} = \'{1}\')", _g.d.cb_chq_list._chq_number, __toChqNumber));
                        }
                        else
                        {
                            __where.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.cb_chq_list._chq_number, "0", "z"));
                        }
                    }
                    //=======================================================================================================
                    //order by
                    string __orderBy = this._myCondition._whereControl._getOrderBy();
                    __orderBy = __orderBy.Replace("\"", "");
                    //sub query
                    string __query = string.Format("select {1},{2},{3},{4},{5}"
                        + " from {0} where chq_type = 1 and  {6} {7} {8} {9}",
                        _g.d.cb_chq_list._table,          //{0}
                        _g.d.cb_chq_list._chq_get_date,       //{1}
                        _g.d.cb_chq_list._doc_ref,         //{2}
                        _g.d.cb_chq_list._chq_number,//{3}
                        _g.d.cb_chq_list._remark,         //{4}
                        _g.d.cb_chq_list._amount,  //{5}                        
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
                this._myCondition = new _condition_form(_enum_screen_report_cb._chq_receive.ToString());
                this._myCondition._whereControl._tableName = _g.d.cb_chq_list._table;
                this._myCondition._whereControl._addFieldComboBox(this._cb_detail_column);
            }
            this._myCondition._process = false;
            this._myCondition.ShowDialog();
            if (this._myCondition._process)
            {
                this._ds = null; // จะได้ load data ใหม่

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
            this._showCondition();
        }               

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
               
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);                
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._title_chq_receive, _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._title_chq_receive))._str, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._page_no, _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._page_no))._str + "" + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._printed_by, _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._printed_by))._str + "" + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._prited_date, _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._prited_date))._str + "" + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, this._view1._fontHeader2);
                //this._view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Description\t: ", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);                
                return true;
            }
            else if (type == SMLReport._report._objectType.Detail)
            {                
                __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._date_chq_receive, "", SMLReport._report._cellAlign.Default);
                _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._doc_ref, "", SMLReport._report._cellAlign.Default);
                _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._chq_no, "", SMLReport._report._cellAlign.Default);
                _view1._addColumn(__ojtReport, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._remark, "", SMLReport._report._cellAlign.Default);
                _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._amount, "", SMLReport._report._cellAlign.Right);                    
                
               

                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            try
            {
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
                SMLReport._report._objectListType __dataObject = null;
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                Font __totalFont = new Font(__getColumn._fontData, FontStyle.Bold);
                decimal __total_amount = 0;                
                DataRow[] _dr = _ds.Select();                
                for (int _row = 0; _row < _dr.Length; _row++)
                {
                    __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                    _view1._addDataColumn(__ojtReport, __dataObject, 0, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.cb_chq_list._chq_get_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.cb_chq_list._doc_ref].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.cb_chq_list._chq_number].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.cb_chq_list._remark].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    decimal __amount = MyLib._myGlobal._decimalPhase(_dr[_row][_g.d.cb_chq_list._amount].ToString().Equals("") ? "0" : _dr[_row][_g.d.cb_chq_list._amount].ToString());
                    _view1._addDataColumn(__ojtReport, __dataObject, 4, (__amount == 0) ? "0.00" : string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);                    
                    __total_amount = __amount + __total_amount;                  
                }
                __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                _view1._createEmtryColumn(__ojtReport, __dataObject);
                _view1._addDataColumn(__ojtReport, __dataObject, 0, "", __totalFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__ojtReport, __dataObject, 1, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._included, _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._included))._str, __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr.Length.ToString() + ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._item, _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._item))._str, __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__ojtReport, __dataObject, 3, ((MyLib._myResourceType)MyLib._myResource._findResource(_g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._total, _g.d.resource_report_bank._table + "." + _g.d.resource_report_bank._total))._str, __totalFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.String);
                _view1._addDataColumn(__ojtReport, __dataObject, 4, string.Format(__formatNumber, __total_amount), __totalFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(":: ERR :: \n" + ex.Message);
            }
        }

       
        void _buttonBuildReport_Click(object sender, EventArgs e)
        {           
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
