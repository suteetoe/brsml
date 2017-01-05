using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace SMLERPCASHBANKReport._report_bank
{
    public partial class _report_cash_bank : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        
        //_conditonReceipt _conditonReceiptScreen = new _conditonReceipt();
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;
        //DataSet _ds;
        DataTable _dataTable;
        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");

        private ArrayList _cash_col_name_1 = new ArrayList();
        private ArrayList _cash_col_name_2 = new ArrayList();
        private ArrayList _cash_col_width_1 = new ArrayList();
        private ArrayList _cash_col_width_2 = new ArrayList();
        private ArrayList _cash_col_type_1 = new ArrayList(); // 1 = Date ,  2 = string , 3 = int  , 4 = double
        private ArrayList _cash_col_type_2 = new ArrayList(); // 1 = Date ,  2 = string , 3 = int  , 4 = double
        //---------------------------------------------------------------------------------------------------
        string[] __width;
        string[] __column;
        string[] __width_2;
        string[] __column_2;
        //---------------------------------------------------------------------------------------------------

        private string _report_name = "";
        private int _cash_level = 0;
        private DataTable __data_ap;
        //-------------------Condition------------------------
        string _data_condition = "";
        Boolean __check_submit = false;
        //----------------------------------------------------
        StringBuilder __query;
        _condition_cash_bank _con_cash;
        private _bank_enum _bank_temp;

        public _bank_enum Bank_temp
        {
            get { return _bank_temp; }
            set { _bank_temp = value; }
        } 

        public _report_cash_bank()
        {
            InitializeComponent();
            _view1._pageSetupDialog.PageSettings.Landscape = true;
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            _view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            
        }

        void _view1__loadDataByThread()
        {
            // this._dataTable == null ไม่ต้อง load ซ้ำ
            if (this._dataTable == null)
            {
                
                //---001-008 เงินสด
                if (_bank_temp == _bank_enum.Deposit_Cash || _bank_temp == _bank_enum.Withdraw_Cash || _bank_temp == _bank_enum.Income_Bank || _bank_temp == _bank_enum.Costs_Bank
                  || _bank_temp == _bank_enum.Interest || _bank_temp == _bank_enum.Interest_Expense || _bank_temp == _bank_enum.Transfer || _bank_temp == _bank_enum.Transfer_Out)
                {
                   string _flag_1 = "";
                   string _flag_2 = "";
                   if(_bank_temp == _bank_enum.Deposit_Cash){_flag_1 = "1";_flag_2 = "2";}
                   if(_bank_temp == _bank_enum.Withdraw_Cash){_flag_1 = "3";_flag_2 = "4";}
                   if(_bank_temp == _bank_enum.Income_Bank){_flag_1 = "5";_flag_2 = "6";}
                   if(_bank_temp == _bank_enum.Costs_Bank){_flag_1 = "7";_flag_2 = "5";}
                   if(_bank_temp == _bank_enum.Interest) {_flag_1 = "9";_flag_2 = "10";}
                   if(_bank_temp == _bank_enum.Interest_Expense) {_flag_1 = "11";_flag_2 = "12";}
                   if(_bank_temp == _bank_enum.Transfer) {_flag_1 = "13";_flag_2 = "14";}
                   if(_bank_temp == _bank_enum.Transfer_Out) { _flag_1 = "15"; _flag_2 = "16"; }
                    string __from_ap = "";
                    string __to_ap = "";
                    string __to_date = "";
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                    this.__query.Append("select doc_date,doc_no,pass_book_code,remark,");
                    this.__query.Append("((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=" + _flag_1 + " ) - (select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=" + _flag_2 + " ))as sum_received");
                    this.__query.Append(" from cb_trans_detail");
                    try
                    {

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }


            }
            this._view1._loadDataByThreadSuccess = true;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Parent.Dispose();
        }

        void _view1__getDataObject()
        {
            try
            {
                DataRow[] __dr = this._dataTable.Select();
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)_objReport._columnList[0];
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
                int _no = 0;
                string a = "";

                for (int i = 0; i < this._dataTable.Columns.Count; i++)
                {
                    a += _dataTable.Columns[i].ToString() + ",";
                }
                string[] __column = a.Split(',');

                //001-008
                if (_bank_temp == _bank_enum.Deposit_Cash || _bank_temp == _bank_enum.Withdraw_Cash || _bank_temp == _bank_enum.Income_Bank || _bank_temp == _bank_enum.Costs_Bank
                    || _bank_temp == _bank_enum.Interest || _bank_temp == _bank_enum.Interest_Expense || _bank_temp == _bank_enum.Transfer || _bank_temp == _bank_enum.Transfer_Out)
                {
                    double __total = 0;
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);

                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_date].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._pass_book_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._remark].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        // _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._amount].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        double __debtBalance = Double.Parse(__dr[_i][_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._amount].ToString());
                        _view1._addDataColumn(_objReport, __dataObject, 4, (__debtBalance == 0) ? "" : string.Format(_formatNumber, __debtBalance), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        __total += __debtBalance;
                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(_objReport, __dataObject2);
                    _view1._addDataColumn(_objReport, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 2, "รวม  : " + string.Format(_formatCountNumber, _no) + " รายการ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 3, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 5, string.Format(_formatNumber, __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                }
            }
            catch(Exception)
            {
            }
        }

        private string[] _cash_bank_data()
        {
            //string[] __column = { "วันที่", "เลขที่เอกสาร", "สมุดเงินฝาก", "หมายเหตุ/รายละเอียด", "จำนวนเงิน" };
            string[] __result = { _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_date,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_no,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._pass_book_code,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._remark,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._amount+"*"};


            return __result;
        }
      
        public void _cash_bank_config()
        {
            this.__query = new StringBuilder();
            this._report_name = "";//_apReportName(_apType);
            //001-008
            if (_bank_temp == _bank_enum.Deposit_Cash || _bank_temp == _bank_enum.Withdraw_Cash || _bank_temp == _bank_enum.Income_Bank || _bank_temp == _bank_enum.Costs_Bank
               || _bank_temp == _bank_enum.Interest || _bank_temp == _bank_enum.Interest_Expense || _bank_temp == _bank_enum.Transfer || _bank_temp == _bank_enum.Transfer_Out)
            {
                string __resourceCode = _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_detail;
                this._report_name = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;
                _view1.__excelFlieName = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;
                string[] __width = { "10", "20", "40", "10", "10" };
                string[] __column = _cash_bank_data();
                string[] __type = { "1", "2", "2", "2", "4" };
                this._setColumnName(__width, __column, null, null);
                this._cash_level = 1;
                string _flag_1 = "";
                string _flag_2 = "";

                if (_bank_temp == _bank_enum.Deposit_Cash) { _flag_1 = "1"; _flag_2 = "2"; }
                if (_bank_temp == _bank_enum.Withdraw_Cash) { _flag_1 = "3"; _flag_2 = "4"; }
                if (_bank_temp == _bank_enum.Income_Bank) { _flag_1 = "5"; _flag_2 = "6"; }
                if (_bank_temp == _bank_enum.Costs_Bank) { _flag_1 = "7"; _flag_2 = "5"; }
                if (_bank_temp == _bank_enum.Interest) { _flag_1 = "9"; _flag_2 = "10"; }
                if (_bank_temp == _bank_enum.Interest_Expense) { _flag_1 = "11"; _flag_2 = "12"; }
                if (_bank_temp == _bank_enum.Transfer) { _flag_1 = "13"; _flag_2 = "14"; }
                if (_bank_temp == _bank_enum.Transfer_Out) { _flag_1 = "15"; _flag_2 = "16"; }

                this.__query.Append("select doc_date,doc_no,pass_book_code,remark,");
                this.__query.Append("((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=" + _flag_1 + " ) - (select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=" + _flag_2 + " ))as sum_received");
                this.__query.Append(" from cb_trans_detail"); 
                if (this.__check_submit) 
                {
                    this.__query.Append(" where trans_type=1 and trans_flag in (" + _flag_1 + "," + _flag_2 + ")");
                    string __startDate = this._con_cash._bank_screen1._getDataStrQuery(_g.d.resource_report._from_docdate).Replace("'", "");
                    string __endDate = this._con_cash._bank_screen1._getDataStrQuery(_g.d.resource_report._to_docdate).Replace("'", "");
                    string __fromBook = this._con_cash._bank_screen1._getDataStrQuery(_g.d.resource_report._from_book_code).Replace("'", "");
                    string __toBook = this._con_cash._bank_screen1._getDataStrQuery(_g.d.resource_report._to_book_code).Replace("'", "");
                    string __fromDoc = this._con_cash._bank_screen1._getDataStrQuery(_g.d.resource_report._from_docno).Replace("'", "");
                    string __toDoc = this._con_cash._bank_screen1._getDataStrQuery(_g.d.resource_report._to_docno).Replace("'", "");
                    string __getWhere = this._con_cash._screen_grid_bank1._createWhere(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._pass_book_code);
                    if (!__startDate.Equals("") && !__startDate.Equals("")) this.__query.Append(" and doc_date between '" + __startDate + "' and '" + __endDate + "' ");
                    if (__fromBook.Equals("") && !__toBook.Equals("")) this.__query.Append(" and pass_book_code between '" + __fromBook + "' and '" + __toBook + "' ");
                    if (!__fromDoc.Equals("") && !__toDoc.Equals("")) this.__query.Append(" and doc_no between '" + __fromDoc + "' and '" + __toDoc + "' ");
                }
                this._dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
            }

        }
        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {

            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title    : " + this._report_name, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print Date : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "Description : ", SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                //_view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);          
                // _view1._excelFileName = "รายงานยอดการชำระเงิน";//
                // _view1._maxColumn = 9;
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    if (this._cash_level == 1)
                    {
                        _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    }
                    for (int i = 0; i < this._cash_col_width_1.Count; i++)
                    {
                        //------------ADD Column----------------
                        _view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._cash_col_width_1[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cash_col_name_1[i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    //--------------Level 2---------------
                    if (this._cash_level == 2 || this._cash_level == 3 || this._cash_level == 4)
                    {
                        if (this._cash_level == 3 || this._cash_level == 4)
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        }
                        else
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                        }

                        for (int i = 0; i < this._cash_col_width_2.Count; i++)
                        {
                            //------------ADD Column----------------
                            _view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._cash_col_width_2[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cash_col_name_2[i].ToString(), "", SMLReport._report._cellAlign.Left);
                        }
                    }
                    //--------------Level 3---------------
                    /* if (this._cb_level == 3 || this._cb_level == 4)
                     {
                         if (this._cb_level == 4)
                         {
                             _objReport3 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                         }
                         else
                         {
                             _objReport3 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                         }

                         for (int i = 0; i < this._cb_width_3.Count; i++)
                         {
                             //------------ADD Column----------------
                             _view1._addColumn(_objReport3, MyLib._myGlobal._intPhase(this._cb_width_3[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cb_column_3[i].ToString(), "", SMLReport._report._cellAlign.Left);
                         }
                     }
                     //--------------Level 4---------------
                     if (this._cb_level == 4)
                     {
                         _objReport4 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);

                         for (int i = 0; i < this._cb_width_4.Count; i++)
                         {
                             //------------ADD Column----------------
                             _view1._addColumn(_objReport4, MyLib._myGlobal._intPhase(this._cb_width_4[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cb_column_4[i].ToString(), "", SMLReport._report._cellAlign.Left);
                         }
                     }*/
                    return true;
                }


            return false;
        }

        bool _view1__loadData()
        {

            if (this._dataTable == null)
            {
                try
                {
                    _dataTable = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString()).Tables[0];
                }
                catch
                {
                    this.Cursor = Cursors.Default;
                    //_view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
                    return false;
                }
                //_view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            }
            //this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            /* if (__check_submit.Equals("OK"))
             {*/
            this._cash_col_width_1.Clear();
            this._cash_col_width_2.Clear();
            this._cash_col_name_1.Clear();
            this._cash_col_name_2.Clear();
            this._cash_bank_config();
            _view1._buildReport(SMLReport._report._reportType.Standard);
            /* }
             else
             {
                 MessageBox.Show("ยังไม่ได้เลือกเงื่อนไข");
             }*/
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition();
        }



        void _showCondition()
        {
            this._dataTable = null;
            //string __page = _apType.ToString();
            if (this._con_cash == null)
            {
                // jead ทำแบบนี้เพราะ เมื่อกดเงื่อนไขซ้ำ ที่เลือกไปแล้ว จะได้เหมือนเดิม
                this._con_cash = new _condition_cash_bank(Bank_temp.ToString());
                this._con_cash._whereControl._tableName = _g.d.erp_bank._table;
                this._con_cash._whereControl._addFieldComboBox(this._cash_bank_data());
                //
               /* switch (this._apType)
                {
                    case _apEnum.Detail_Payable:
                        this._con_1._whereControl._tableName = _g.d.ap_supplier._table;
                        this._con_1._whereControl._addFieldComboBox(this._ap_detail_column());
                        break;
                    case _apEnum.Documents_Early_Year:
                        this._con_1._whereControl._tableName = _g.d.ap_ar_trans._table;
                        this._con_1._whereControl._addFieldComboBox(this._ap_year_balance_column());
                        break;
                }*/
                //
                
            }
            this._con_cash.Size = new Size(500, 500);
            this._con_cash.ShowDialog();
            if (this._con_cash.__check_submit)
            {
                this._data_condition = this._con_cash.__where;
                this.__check_submit = this._con_cash.__check_submit;
                this._cash_bank_config();
                this.__data_ap = this._con_cash.__grid_where;
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }

        double _convertNumber(string dataSult)
        {
            double __result = 0.00;
            Double __amount = 0.00;
            try
            {
                __amount = Double.Parse(dataSult);
                if (__amount > 0)
                {
                    __result = __amount;
                }
                else
                {
                    __result = 0;
                }
            }
            catch
            {
            }
            return __result;
        }

        string _checkNumber(string datasult)
        {
            string __result = "";
            Double __Amount = 0.00;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            try
            {
                __Amount = Double.Parse(datasult);
                if (__Amount > 0)
                {
                    __result = string.Format(__formatNumber, __Amount);
                }
                else
                {
                    __result = "";
                }
            }
            catch
            {
            }
            return __result;
        }


        void _setColumnName(string[] __width, string[] __column, string[] __width_2, string[] __column_2)
        {
            try
            {
                this._cash_col_width_1.Clear();
                this._cash_col_name_1.Clear();
                this._cash_col_width_2.Clear();
                this._cash_col_name_2.Clear();
                for (int i = 0; i < __width.Length; i++)
                {
                    this._cash_col_width_1.Add(__width[i]);
                    this._cash_col_name_1.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cash_col_width_2.Add(__width_2[i]);
                    this._cash_col_name_2.Add(__column_2[i]);
                }
            }
            catch
            {
            }
        }
        public enum _bank_enum
        {
            /// <summary>
            /// 001-รายงานการฝากเงินสด
            /// </summary>
            Deposit_Cash,
            /// <summary>
            /// 002-รายงานถอนเงินสด
            /// </summary>
            Withdraw_Cash,
            /// <summary>
            /// 003-รายงานรายได้จากธนาคาร
            /// </summary>
            Income_Bank,
            /// <summary>
            /// 004-รายงานจ่ายจากธนาคาร
            /// </summary>
            Costs_Bank,
            /// <summary>
            /// 005-รายงานดอกเบี้ยรับ
            /// </summary>
            Interest,
            /// <summary>
            /// 006-รายงานดอกเบี้ยจ่าย
            /// </summary>
            Interest_Expense,
            /// <summary>
            /// 007-รายงานโอนเงินเข้า
            /// </summary>
            Transfer,
            /// <summary>
            /// 008-รายงานโอนเงินออก
            /// </summary>
            Transfer_Out,
           
        }
    
       
    }
}
