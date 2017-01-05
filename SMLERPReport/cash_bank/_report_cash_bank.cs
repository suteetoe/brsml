using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
namespace SMLERPReport.cash_bank
{
    public partial class _report_cash_bank : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private DataTable _dataTable;
        private DataSet _dataset;
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _fromDocno = "";
        private string _toDocno = "";
        
        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        //_conditonReceipt _conditonReceiptScreen = new _conditonReceipt();        
        //------------------------
        private ArrayList _cb_width = new ArrayList();
        private ArrayList _cb_column = new ArrayList();
        private ArrayList _cb_width_2 = new ArrayList();
        private ArrayList _cb_column_2 = new ArrayList();
        private ArrayList _cb_width_3 = new ArrayList();
        private ArrayList _cb_column_3 = new ArrayList();
    
        private string _report_name = "";
        private int _cb_level = 0;
        StringBuilder __query;
        //------------------------
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;
        SMLReport._report._objectListType _objReport3;
        string __data_condition = "";
        Boolean __check_submit = false;
        DataSet _ds;
        private _cash_bank_Enum _cash_bank;

        public _cash_bank_Enum Cash_bank
        {
            get { return _cash_bank; }
            set { _cash_bank = value; }
        }

        private _cash_bank_Enum __CBReportTypeTemp;

        public _cash_bank_Enum CBReportType
        {
            set
            {
                this.__CBReportTypeTemp = value;                
                this.Invalidate();
            }
            get
            {
                return this.__CBReportTypeTemp;
            }
        }



        public _report_cash_bank()
        {
            InitializeComponent();
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);

            //_view1._buildReport(SMLReport._report._reportType.Standard);
            //_view1._pageSetupDialog.PageSettings.Landscape = true;         
            //_showCondition();
        }

        void _view1__loadDataByThread()
        {
            SMLProcess._cbProcess __cbProcess = new SMLProcess._cbProcess();
            //ตัวแปลใช้ทั้ง ฟังค์ชั่น
            string __dateBegin ="";
            string __dateEnd = "";
            string __where = "";
            string __orderBy = "";
            int __reporttype = 0;
            // this._dataTable == null ไม่ต้อง load ซ้ำ            
            if (this._dataTable == null)
            {
                //001-รายงานรายได้จากธนาคาร
                if (Cash_bank == _cash_bank_Enum.Revenue_by_Bank)
                {
                    string _from_docdate = "";
                    string _to_docdate = "";
                    string _from_account_book_code = "";
                    string _to_account_book_code = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_book_code = "";
                    string _to_book_code = "";
                    
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');

                            if (!__data_split[0].Equals("")) { _from_docdate = __data_split[0]; };
                            if (!__data_split[1].Equals("")) { _to_docdate = __data_split[1]; };
                            if (!__data_split[2].Equals("")) { _from_docno = __data_split[2]; };
                            if (!__data_split[3].Equals("")) { _to_docno = __data_split[3]; };
                            if (!__data_split[4].Equals("")) { _from_book_code = __data_split[4]; };
                            if (!__data_split[5].Equals("")) { _to_book_code = __data_split[5]; };

                            __dateBegin = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(_from_docdate, MyLib._myGlobal._cultureInfo()));
                            __dateEnd = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(_to_docdate, MyLib._myGlobal._cultureInfo()));
                            __where = _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no + " between \'" + _from_docno + "\' and \'" + _to_docno + "\' ";
                            __orderBy = "order by " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no;
                        }
                        else
                        {                           
                            __dateBegin = "";
                            __dateEnd = "";
                            __where = "";
                            __orderBy = "order by " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no;
                        }
                        __reporttype = 1;
                        this._dataTable = __cbProcess._queryCBReportProcess(MyLib._myGlobal._databaseName, __dateBegin, __dateEnd, __where, __orderBy, __reporttype).Tables[0];                        
                        
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //002-รายงานการฝากเงินสด
                else if (Cash_bank == _cash_bank_Enum.Cash_deposit)
                {
                    
                    try
                    {
                        if (this.__check_submit)
                        {
                           
                        }
 
                           // __where = _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no + " between \'" + _from_docno + "\' and \'" + _to_docno + "\' ";
                            __orderBy = "order by " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no;
                            __reporttype = 2;
                            string __qurey = "select doc_date,doc_no,pass_book_code,remark,"+
                                                "((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=1 ) - (select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=2 ))as sum_received"+
                                                " from cb_trans_detail";
                            string __message = "";
                           
                            //this._dataTable = __cbProcess._processCBStatement(MyLib._myGlobal._databaseName, __dateBegin, __dateEnd, __where, __orderBy, __reporttype).Tables[0];
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //003-รายงาน Statement ล่วงหน้า
                else if (Cash_bank == _cash_bank_Enum.Statement_Advance)
                {
                    string _from_due_date = "";
                    string _to_due_date = "";
                    string _from_number_check_credit_card = "";
                    string _to_number_check_credit_card = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_due_date = __data_split[0];
                             _to_due_date = __data_split[1];
                             _from_number_check_credit_card = __data_split[2];
                             _to_number_check_credit_card = __data_split[3];
                             _from_docno = __data_split[4];
                             _to_docno = __data_split[5];
                        }
                            //string __dateBegin = MyLib._myGlobal._convertDateToQuery(this._fromDate);
                            //string __dateEnd = MyLib._myGlobal._convertDateToQuery(this._toDate);
                        this._fromDate = DateTime.Parse("2009-07-22", MyLib._myGlobal._cultureInfo());
                        this._toDate = DateTime.Parse("2009-07-22", MyLib._myGlobal._cultureInfo());
                            __dateBegin = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
                            __dateEnd = MyLib._myGlobal._convertDateToQuery(DateTime.Now);

                            if (_from_docno.Equals("")) _from_docno = "CB-0";
                            if (_to_docno.Equals("")) _to_docno = "CB-10";

                            __where = _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no + " between \'" + _from_docno + "\' and \'" + _to_docno + "\' ";
                            __orderBy = "order by " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no;
                            __reporttype = 4;
                            //this._dataTable = __cbProcess._processCBStatement(MyLib._myGlobal._databaseName, __dateBegin, __dateEnd, __where, __orderBy, __reporttype).Tables[0];
                        
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //004-รายงานการโอนเงินระหว่างธนาคาร
                else if (Cash_bank == _cash_bank_Enum.Transfer_Money_Between_Banks)
                {
                    string _from_docdate = "";
                    string _to_docdate = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_docdate = __data_split[0];
                             _to_docdate = __data_split[1];
                             _from_docno = __data_split[2];
                             _to_docno = __data_split[3];
                        }
                            //string __dateBegin = MyLib._myGlobal._convertDateToQuery(this._fromDate);
                            //string __dateEnd = MyLib._myGlobal._convertDateToQuery(this._toDate);
                            this._fromDate = DateTime.Parse("2009-07-22");
                            this._toDate = DateTime.Parse("2009-07-22");
                            __dateBegin = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
                            __dateEnd = MyLib._myGlobal._convertDateToQuery(DateTime.Now);

                            if (_from_docno.Equals("")) _from_docno = "CB-0";
                            if (_to_docno.Equals("")) _to_docno = "CB-10";

                            __where = _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no + " between \'" + _from_docno + "\' and \'" + _to_docno + "\' ";
                            __orderBy = "order by " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no;
                            __reporttype = 4;
                            //this._dataTable = __cbProcess._processCBStatement(MyLib._myGlobal._databaseName, __dateBegin, __dateEnd, __where, __orderBy, __reporttype).Tables[0];
                        
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //005-รายงาน Bank Statement
                else if (Cash_bank == _cash_bank_Enum.Bank_Statement)
                {
                    string _from_book_code = "";
                    string _to_book_code = "";
                    string _from_period = "";
                    string _to_period = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_book_code = __data_split[0];
                             _to_book_code = __data_split[1];
                             _from_period = __data_split[2];
                             _to_period = __data_split[3];
                        }
                            //string __dateBegin = MyLib._myGlobal._convertDateToQuery(this._fromDate);
                            //string __dateEnd = MyLib._myGlobal._convertDateToQuery(this._toDate);
                            this._fromDate = DateTime.Parse("2009-07-22");
                            this._toDate = DateTime.Parse("2009-07-22");
                            __dateBegin = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
                            __dateEnd = MyLib._myGlobal._convertDateToQuery(DateTime.Now);


                            __where = "";// _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no + " between \'" + +"\' and \'" + _to_docno + "\' ";
                            __orderBy = "order by " + _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no;
                            __reporttype = 5;
                            //this._dataTable = __cbProcess._processCBStatement(MyLib._myGlobal._databaseName, __dateBegin, __dateEnd, __where, __orderBy, __reporttype).Tables[0];
                        
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Received_Cash_sub_item,//006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
                else if (Cash_bank == _cash_bank_Enum.Received_Cash_sub_item)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_date = __data_split[0];
                             _to_date = __data_split[1];
                             _from_docno = __data_split[2];
                             _to_docno = __data_split[3];
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                  
                }
                //Open_sub_Cash,//007-รายงานการตั้งเบิกเงินสดย่อย
                else if (Cash_bank == _cash_bank_Enum.Open_sub_Cash)
                {
                    string _from_cash_sub_code = "";
                    string _from_cost_date = "";
                    string _to_cost_date = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_cash_sub_code = __data_split[0];
                             _from_cost_date = __data_split[1];
                             _to_cost_date = __data_split[2];
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }                    
                }
                //Cash_movements,//008-รายงานเคลื่อนไหวเงินสด
                else if (Cash_bank == _cash_bank_Enum.Cash_movements)
                {
                    string _from_date = "";
                    string _to_date = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_date = __data_split[0];
                             _to_date = __data_split[1];
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }                    
                }
                //Pay_Cash_by_DocDate,//009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
                else if (Cash_bank == _cash_bank_Enum.Pay_Cash_by_DocDate)
                {
                    string _from_docdate = "";
                    string _to_docdate = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_staff_code = "";
                    string _to_staff_code = "";
                    string _conditions = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_docdate = __data_split[0];
                             _to_docdate = __data_split[1];
                             _from_docno = __data_split[2];
                             _to_docno = __data_split[3];
                             _from_staff_code = __data_split[4];
                             _to_staff_code = __data_split[5];
                             _conditions = __data_split[6];
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }                                     
                }
                //Monthly_Payment_Book,//010-รายงานสมุดจ่ายเงินประจำเดือน
                else if (Cash_bank == _cash_bank_Enum.Monthly_Payment_Book)
                {
                    string _year = "";
                    string _monthly = "";
                    string _from_account_book_code = "";
                    string _to_account_book_code = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _year = __data_split[0];
                             _monthly = __data_split[1];
                             _from_account_book_code = __data_split[2];
                             _to_account_book_code = __data_split[3];
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }                            
                }
                //Book_Bank_Balance,//011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
                else if (Cash_bank == _cash_bank_Enum.Book_Bank_Balance)
                {
                    string _from_address_book = "";
                    string _to_address_book = "";
                    string _at_date = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_address_book = __data_split[0];
                             _to_address_book = __data_split[1];
                             _at_date = __data_split[2];
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }                    
 
                }
                //Sub_Cash_Movements,//012-รายงานเคลื่อนไหวเงินสดย่อย
                else if (Cash_bank == _cash_bank_Enum.Sub_Cash_Movements)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_cash_sub_code = "";
                    string _to_cash_sub_code = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_date = "";
                             _to_date = "";
                             _from_cash_sub_code = "";
                             _to_cash_sub_code = "";
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Summary_Payment//013-รายงานสรุปการจ่ายเงิน
                else if (Cash_bank == _cash_bank_Enum.Summary_Payment)
                {
                    string _year = "";
                    string _monthly = "";
                    string _from_project_account_code = "";
                    string _to_project_account_code = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _year = __data_split[0];
                             _monthly = __data_split[1];
                             _from_project_account_code = __data_split[2];
                             _to_project_account_code = __data_split[3];
                        }
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
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)_objReport._columnList[0];
                DataRow[] __dr =  this._dataTable.Select();
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
                int _no = 0;

              
                //Revenue_by_Bank//001-รายงานรายได้จากธนาคาร
                if (Cash_bank == _cash_bank_Enum.Revenue_by_Bank)
                {
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i]["doc_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i]["pass_book_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i]["sum_received"].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        _no++;
                       
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    }
                }
                //Cash_deposit//002-รายงานการฝากเงินสด
                else if (Cash_bank == _cash_bank_Enum.Cash_deposit)
                {
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i]["doc_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i]["pass_book_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i]["sum_received"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    }
                }
                //Statement_Advance//003-รายงาน Statement ล่วงหน้า
                else if (Cash_bank == _cash_bank_Enum.Statement_Advance)
                {
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i]["chq_due_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["chq_number"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i]["trans_type"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i]["pass_book_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i]["sum_received"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i]["sum_payment"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    }
                }
                //Transfer_Money_Between_Banks//004-รายงานการโอนเงินระหว่างธนาคาร
                else if (Cash_bank == _cash_bank_Enum.Transfer_Money_Between_Banks)
                {
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        //_view1._reportProgressBar.Style = ProgressBarStyle.Continuous;

                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i]["doc_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i]["pass_book_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i]["sum_received"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i]["sum_payment"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    }
                }
                //Bank_Statement//005-รายงาน Bank Statement
                else if (Cash_bank == _cash_bank_Enum.Bank_Statement)
                {
                    string __doc_date = "";
                    string __doc_no = "";
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        __doc_date = __dr[_i]["doc_date"].ToString();
                        __doc_no = __dr[_i]["doc_no"].ToString();

                        _view1._reportProgressBar.Style = ProgressBarStyle.Continuous;
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i]["doc_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                        for (int _j = 0; _j < __dr.Length; _j++)
                        {
                            if (__doc_date.Equals(__dr[_j][""].ToString()) && __doc_no.Equals(__dr[_j][""].ToString()))
                            {

                                SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(_objReport2, __dataObject2);
                                _view1._addDataColumn(_objReport2, __dataObject2, 0, __dr[_j]["doc_date"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[_j]["doc_no"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 2, __dr[_j]["chq_number"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 3, __dr[_j]["book_code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 4, __dr[_j]["remark"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 5, __dr[_j]["sum_received"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 6, __dr[_j]["sum_payment"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 7, __dr[_j]["sum_total"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 8, __dr[_j]["currency_name"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 9, __dr[_j]["exchange_rate"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            }

                        }
                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        /* _view1._addDataColumn(_objReport, __dataObject, 2, " ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 3, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 4, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 5, " ", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        _view1._addDataColumn(_objReport, __dataObject, 6, "รวมเงิน:", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        */
                        _view1._createEmtryColumn(_objReport, __dataObject);
                    }
                }
                //Received_Cash_sub_item//006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
                else if (Cash_bank == _cash_bank_Enum.Received_Cash_sub_item)
                {

                }
                //Open_sub_Cash//007-รายงานการตั้งเบิกเงินสดย่อย
                else if (Cash_bank == _cash_bank_Enum.Open_sub_Cash)
                {
                    string __doc_date = "";
                    string __doc_no = "";
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        __doc_date = __dr[_i][""].ToString();
                        __doc_no = __dr[_i][""].ToString();

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        _view1._reportProgressBar.Style = ProgressBarStyle.Continuous;
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                        for (int _j = 0; _j < __dr.Length; _j++)
                        {
                            if (__doc_date.Equals(__dr[_j][""].ToString()) && __doc_no.Equals(__dr[_j][""].ToString()))
                            {

                                SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(_objReport2, __dataObject2);
                                _view1._addDataColumn(_objReport2, __dataObject2, 0, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 2, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 3, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 4, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 5, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            }

                        }
                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        /* _view1._addDataColumn(_objReport, __dataObject, 2, " ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 3, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 4, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 5, " ", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        _view1._addDataColumn(_objReport, __dataObject, 6, "รวมเงิน:", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        */
                        _view1._createEmtryColumn(_objReport, __dataObject);
                    }
                }
                //Cash_movements//008-รายงานเคลื่อนไหวเงินสด
                else if (Cash_bank == _cash_bank_Enum.Cash_movements)
                {
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        //_view1._reportProgressBar.Style = ProgressBarStyle.Continuous;

                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    }
                }
                //Pay_Cash_by_DocDate//009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
                else if (Cash_bank == _cash_bank_Enum.Pay_Cash_by_DocDate)
                {
                    string __doc_date = "";
                    string __doc_no = "";
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        __doc_date = __dr[_i][""].ToString();
                        __doc_no = __dr[_i][""].ToString();

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        _view1._reportProgressBar.Style = ProgressBarStyle.Continuous;
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                        for (int _j = 0; _j < __dr.Length; _j++)
                        {
                            if (__doc_date.Equals(__dr[_j][""].ToString()) && __doc_no.Equals(__dr[_j][""].ToString()))
                            {

                                SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(_objReport2, __dataObject2);
                                _view1._addDataColumn(_objReport2, __dataObject2, 0, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            }

                        }
                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        /* _view1._addDataColumn(_objReport, __dataObject, 2, " ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 3, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 4, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 5, " ", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        _view1._addDataColumn(_objReport, __dataObject, 6, "รวมเงิน:", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        */
                        _view1._createEmtryColumn(_objReport, __dataObject);
                    }
                }
                //Monthly_Payment_Book//010-รายงานสมุดจ่ายเงินประจำเดือน
                else if (Cash_bank == _cash_bank_Enum.Monthly_Payment_Book)
                {

                }
                //Book_Bank_Balance//011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
                else if (Cash_bank == _cash_bank_Enum.Book_Bank_Balance)
                {
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        //_view1._reportProgressBar.Style = ProgressBarStyle.Continuous;

                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    }

                }
                //Sub_Cash_Movements//012-รายงานเคลื่อนไหวเงินสดย่อย
                else if (Cash_bank == _cash_bank_Enum.Sub_Cash_Movements)
                {
                    string __doc_date = "";
                    string __doc_no = "";
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        __doc_date = __dr[_i][""].ToString();
                        __doc_no = __dr[_i][""].ToString();

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        _view1._reportProgressBar.Style = ProgressBarStyle.Continuous;
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                        for (int _j = 0; _j < __dr.Length; _j++)
                        {
                            if (__doc_date.Equals(__dr[_j][""].ToString()) && __doc_no.Equals(__dr[_j][""].ToString()))
                            {

                                SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(_objReport2, __dataObject2);
                                _view1._addDataColumn(_objReport2, __dataObject2, 0, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 2, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 3, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 4, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 5, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            }

                        }
                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        /* _view1._addDataColumn(_objReport, __dataObject, 2, " ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 3, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 4, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 5, " ", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        _view1._addDataColumn(_objReport, __dataObject, 6, "รวมเงิน:", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        */
                        _view1._createEmtryColumn(_objReport, __dataObject);
                    }
                }
                //Summary_Payment//013-รายงานสรุปการจ่ายเงิน
                else if (Cash_bank == _cash_bank_Enum.Summary_Payment)
                {
                    string __doc_date = "";
                    string __doc_no = "";
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        __doc_date = __dr[_i][""].ToString();
                        __doc_no = __dr[_i][""].ToString();

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        _view1._reportProgressBar.Style = ProgressBarStyle.Continuous;
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                        for (int _j = 0; _j < __dr.Length; _j++)
                        {
                            if (__doc_date.Equals(__dr[_j][""].ToString()) && __doc_no.Equals(__dr[_j][""].ToString()))
                            {

                                SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                                _view1._createEmtryColumn(_objReport2, __dataObject2);
                                _view1._addDataColumn(_objReport2, __dataObject2, 0, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 1, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 2, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 3, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                            }

                        }
                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    if (_no == __dr.Length)
                    {
                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.TopBottom);
                        _view1._createEmtryColumn(_objReport, __dataObject);
                        _view1._addDataColumn(_objReport, __dataObject, 0, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, "รวม  : " + _no, __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        /* _view1._addDataColumn(_objReport, __dataObject, 2, " ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 3, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 4, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 5, " ", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        _view1._addDataColumn(_objReport, __dataObject, 6, "รวมเงิน:", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        */
                        _view1._createEmtryColumn(_objReport, __dataObject);
                    }
                }
            //---------END---------------//---------END---------------
            }
            catch (Exception ex)
            {
                 MessageBox.Show(":: ERR :: \n" + ex.Message);
            }
        }
        void _setColumnName(string[] __width, string[] __column, string[] __width_2, string[] __column_2)
        {
            try
            {
                this._cb_width.Clear();
                this._cb_column.Clear();
                this._cb_width_2.Clear();
                this._cb_column_2.Clear();
                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cb_width_2.Add(__width_2[i]);
                    this._cb_column_2.Add(__column_2[i]);
                }
            }
            catch
            {
            }
        }
        private string[] _cash_deposit_data()
        {
            //string[] __column = { "วันที่", "เลขที่เอกสาร", "สมุดเงินฝาก", "หมายเหตุ/รายละเอียด", "จำนวนเงิน" };
            string[] __result = { _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_date,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_no,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._pass_book_code,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._remark,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._amount+"*"};
                                    

            return __result;
        }
        private string[] _cash_chq_data()
        {
            string[] __result = { _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_date,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._doc_no,
                                    _g.d.cb_chq_list._table+"."+_g.d.cb_chq_list._chq_get_date,
                                    _g.d.cb_chq_list._table+"."+_g.d.cb_chq_list._chq_due_date,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._trans_number,
                                    _g.d.cb_chq_list._table+"."+_g.d.cb_chq_list._owner_name,
                                    _g.d.erp_bank._table+"."+_g.d.erp_bank._name_1,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._status,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._remark,
                                    _g.d.cb_trans_detail._table+"."+_g.d.cb_trans_detail._amount+"*"};


            return __result;
        }
        public void _cash_bank_config()
        {
           
            this.__query = new StringBuilder();

            //Revenue_by_Bank//001-รายงานรายได้จากธนาคาร,รายงานค่าใช้จ่ายธนาคาร
            if (Cash_bank == _cash_bank_Enum.Revenue_by_Bank)
            {
                this._report_name = "รายงานรายได้จากธนาคาร";
               
                string[] __width = { "10", "10", "10", "10", "10" };
                string[] __column = _cash_deposit_data();
                this._cb_level = 1;
                this._setColumnName(__width, __column, null, null);
               
            }
            //Cash_deposit//002-รายงานการฝากเงินสด,รายงานถอนเงินสด
            else if (Cash_bank == _cash_bank_Enum.Cash_deposit)
            {
                this._report_name = "รายงานการฝากเงินสด";
                string[] __width = { "10", "10", "10", "10", "10" };
                string[] __column = _cash_deposit_data();
                this._cb_level = 1;
                this._setColumnName(__width, __column, null, null);

               
            }
            //Statement_Advance//003-รายงาน Statement ล่วงหน้า
            else if (Cash_bank == _cash_bank_Enum.Statement_Advance)
            {
                this._report_name = "รายงาน Statement ล่วงหน้า";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่ครบกำหนด", "เลขที่เช็ค/บัตร", "ประเภท", "เลขที่เอกสาร", "สมุดเงินฝาก", "หมายเหตุ", "ยอดรับเงิน", "ยอดจ่าย" };
                this._cb_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
            }
            //Transfer_Money_Between_Banks//004-รายงานการโอนเงินระหว่างธนาคาร
            else if (Cash_bank == _cash_bank_Enum.Transfer_Money_Between_Banks)
            {
                this._report_name = "รายงานการโอนเงินระหว่างธนาคาร";
                string[] __width = { "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่", "เลขที่เอกสาร", "โอนจากสมุดเงินฝาก", "โอนเข้าสมุดเงินฝาก", "หมายเหตุ/รายละเอียด", "จำนวนเงิน" };
                this._cb_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
            }
            //Bank_Statement//005-รายงาน Bank Statement
            else if (Cash_bank == _cash_bank_Enum.Bank_Statement)
            {
                this._report_name = "รายงาน Bank Statement";
                string[] __width = { "10", "10" };
                string[] __column = { "เลขที่บัญชี", "ชื่อบัญชี" };
                string[] __width_2 = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column_2 = { "วัน", "เลขที่เอกสาร", "เลขที่เช็ค/บัตร", "รายวัน", "หมายเหตุบิล", "ยอดฝาก", "ยอดถอน", "ยอดคงเหลือ", "ชื่อสกุลเงิน", "อัตราแลกเปลี่ยน" };
                this._cb_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cb_width_2.Add(__width_2[i]);
                    this._cb_column_2.Add(__column_2[i]);
                }
            }
            //Received_Cash_sub_item//006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
            else if (Cash_bank == _cash_bank_Enum.Received_Cash_sub_item)
            {
                this._report_name = "รายงานการรับเงินอื่นๆพร้อมรายการย่อย";
                string[] __width = { "10", "10", "10", "10", "10", "10" };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "รหัสสมุดรายวันที่ GL", "รับจาก(ลูกค้า)", "รหัสแผนก", "รวมทั้งสิ้น" };
                string[] __width_2 = { "10" };
                string[] __column_2 = { "คำอธิบาย" };
                string[] __width_3 = { "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column_3 = { "รหัสผังบัญชี", "รายการผังบัญชี", "คำอธิบาย", "รหัสแผนก", "รหัสการจัดสรร", "รหัสโครงการ", "เดบิต", "เครดิต" };

                this._cb_level = 3;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cb_width_2.Add(__width_2[i]);
                    this._cb_column_2.Add(__column_2[i]);
                }
                for (int i = 0; i < __width_3.Length; i++)
                {
                    this._cb_width_3.Add(__width_3[i]);
                    this._cb_column_3.Add(__column_3[i]);
                }
            }
            //Open_sub_Cash//007-รายงานการตั้งเบิกเงินสดย่อย
            else if (Cash_bank == _cash_bank_Enum.Open_sub_Cash)
            {
                this._report_name = "รายงานการตั้งเบิกเงินสดย่อย";
                string[] __width = { "10", "10" };
                string[] __column = { "รหัสวงเงินสดย่อย", "วงเงินสดย่อย" };
                string[] __width_2 = { "10", "10", "10", "10", "10", "10" };
                string[] __column_2 = { "วันที่เอกสาร", "เลขที่เอกสาร", "หมายเหตุคำอธิบาย", "ยอดรับ", "ยอดจ่าย", "ยอดคงเหลือ" };
                this._cb_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cb_width_2.Add(__width_2[i]);
                    this._cb_column_2.Add(__column_2[i]);
                }
            }
            //Cash_movements//008-รายงานเคลื่อนไหวเงินสด
            else if (Cash_bank == _cash_bank_Enum.Cash_movements)
            {
                this._report_name = "รายงานเคลื่อนไหวเงินสด";
                string[] __width = { "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่เอกสาร", "เลขเอกสาร", "หมายเหตุคำอธิบาย", "ยอดรับ", "ยอดจ่าย", "ยอดคงเหลือ" };
                this._cb_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
            }
            //Pay_Cash_by_DocDate//009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
            else if (Cash_bank == _cash_bank_Enum.Pay_Cash_by_DocDate)
            {
                this._report_name = "รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร";
                string[] __width = { "10", "10" };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "แผนก", "โครงการ", "ชื่อพนักงาน", "" };
                string[] __width_2 = { "10", "10", "10", "10" };
                string[] __column_2 = { "ชื่อ/รายละเอียด", "", "", "จำนวนเงิน" };
                this._cb_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cb_width_2.Add(__width_2[i]);
                    this._cb_column_2.Add(__column_2[i]);
                }
            }
            //Monthly_Payment_Book//010-รายงานสมุดจ่ายเงินประจำเดือน
            else if (Cash_bank == _cash_bank_Enum.Monthly_Payment_Book)
            {

            }
            //Book_Bank_Balance//011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
            else if (Cash_bank == _cash_bank_Enum.Book_Bank_Balance)
            {
                this._report_name = "รายงานยอดเงินคงเหลือสมุดฝากธนาคาร";
                string[] __width = { "10", "10", "10" };
                string[] __column = { "เลขที่บัญชี", "ชื่อบัญชี", "ยอดคงเหลือ" };
                this._cb_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
            }
            //Sub_Cash_Movements//012-รายงานเคลื่อนไหวเงินสดย่อย
            else if (Cash_bank == _cash_bank_Enum.Sub_Cash_Movements)
            {
                this._report_name = "รายงานเคลื่อนไหวเงินสดย่อย";
                string[] __width = { "10", "10" };
                string[] __column = { "รหัสวงเงินสดย่อย", "วงเงินสดย่อย" };
                string[] __width_2 = { "10", "10", "10", "10", "10", "10" };
                string[] __column_2 = { "วันที่เอกสาร", "เลขที่เอกสาร", "หมายเหตุคำอธิบาย", "ยอดรับ", "ยอดจ่าย", "ยอดคงเหลือ" };
                this._cb_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cb_width_2.Add(__width_2[i]);
                    this._cb_column_2.Add(__column_2[i]);
                }
            }
            //Summary_Payment//013-รายงานสรุปการจ่ายเงิน
            else if (Cash_bank == _cash_bank_Enum.Summary_Payment)
            {
                this._report_name = "รายงานสรุปการจ่ายเงิน";
                string[] __width = { "10", "10" };
                string[] __column = { "รหัสผังบัญชี", "ชื่อบัญชี" };
                string[] __width_2 = { "10", "10", "10", "10", "10", "10" };
                string[] __column_2 = { "วันที่", "ใบสำคัญจ่าย", "หมายเหตุ", "จำนวนเงิน" };
                this._cb_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._cb_width.Add(__width[i]);
                    this._cb_column.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._cb_width_2.Add(__width_2[i]);
                    this._cb_column_2.Add(__column_2[i]);
                }
            }
            //---------------End-----------------
        }
        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {


            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                // _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.Text, SMLReport._report._reportValueDefault._ltdaddress, SMLReport._report._cellAlign.Center, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Title    : " + this._report_name, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                //_view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.Text, "Print Date: " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print By : " + MyLib._myGlobal._userName, SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "Print Date: " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "Description : ", SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                // _view1._excelFileName = "รายงานยอดการชำระเงิน";//
                // _view1._maxColumn = 9;
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    if (this._cb_level == 1)
                    {
                        _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    }
                    for (int i = 0; i < this._cb_width.Count; i++)
                    {
                        //------------ADD Column----------------
                        _view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._cb_width[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cb_column[i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    //--------------Level 2---------------
                    if (this._cb_level == 2 || this._cb_level == 3 || this._cb_level == 4)
                    {
                        if (this._cb_level == 3 || this._cb_level == 4)
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        }
                        else
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                        }

                        for (int i = 0; i < this._cb_width_2.Count; i++)
                        {
                            //------------ADD Column----------------
                            _view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._cb_width_2[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._cb_column_2[i].ToString(), "", SMLReport._report._cellAlign.Left);
                        }
                    }
                    //--------------Level 3---------------
                    if (this._cb_level == 3 || this._cb_level == 4)
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
                   /* if (this._cb_level == 4)
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

            if (_ds == null)
            {
                try
                {
                    _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, this.__query.ToString());
                }
                catch
                {
                    this.Cursor = Cursors.Default;
                    _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
                    return false;
                }
                _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            }
            //this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {            
            this._view1._loadDataByThreadSuccess = true;
            this._cb_width.Clear();
            this._cb_width_2.Clear();
            this._cb_width_3.Clear();
    
            this._cb_column.Clear();
            this._cb_column_2.Clear();
            this._cb_column_3.Clear();
            this._view1__loadDataByThread();
            this._cash_bank_config();
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }


        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }
        _condition _con_cb;
        void _showCondition()
        {
            this._dataTable = null;
            string __page = Cash_bank.ToString();
            if (this._con_cb == null)
            {
                this._con_cb = new _condition(__page);
                switch (this._cash_bank)
                {
                    case _cash_bank_Enum.Revenue_by_Bank:
                        this._con_cb._whereControl._tableName = _g.g._search_screen_bank;
                        // this._con_cb._whereControl._addFieldComboBox();
                        break;
                    case _cash_bank_Enum.Cash_deposit:
                        this._con_cb._whereControl._tableName = _g.g._search_screen_cb_trans;
                        this._con_cb._whereControl._addFieldComboBox(this._cash_deposit_data());
                        break;
                }
                this._con_cb.Size = new Size(500, 500);
            }
                this._con_cb.ShowDialog();
                if (this._con_cb.__check_submit)
                {
                    this.__data_condition = _con_cb.__where;
                    this.__check_submit = _con_cb.__check_submit;
                    this._cash_bank_config();
                    _view1._buildReport(SMLReport._report._reportType.Standard);
                }
                
            
            _ds = null;
     
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


        public static class _CBReportTypeGlobal
        {
            public static int CBREportType(_cash_bank_Enum CBREportType)
            {
                switch (CBREportType)
                {
                    case _cash_bank_Enum.Revenue_by_Bank: return 1;
                    case _cash_bank_Enum.Cash_deposit: return 2;
                    case _cash_bank_Enum.Statement_Advance: return 3;
                    case _cash_bank_Enum.Transfer_Money_Between_Banks: return 4;
                    case _cash_bank_Enum.Bank_Statement: return 5;
                    case _cash_bank_Enum.Received_Cash_sub_item: return 6;
                    case _cash_bank_Enum.Open_sub_Cash: return 7;
                    case _cash_bank_Enum.Cash_movements: return 8;
                    case _cash_bank_Enum.Pay_Cash_by_DocDate: return 9;
                    case _cash_bank_Enum.Monthly_Payment_Book: return 10;
                    case _cash_bank_Enum.Book_Bank_Balance: return 11;
                    case _cash_bank_Enum.Sub_Cash_Movements: return 12;
                    case _cash_bank_Enum.Summary_Payment: return 13;                        
                }
                return 0;
            }
        }

        public enum _cash_bank_Enum
        {
            /// <summary>
            /// 001-รายงานรายได้จากธนาคาร
            /// </summary>
            Revenue_by_Bank,
            /// <summary>
            /// 002-รายงานการฝากเงินสด
            /// </summary>
            Cash_deposit,
            /// <summary>
            /// 003-รายงาน Statement ล่วงหน้า
            /// </summary>
            Statement_Advance,
            /// <summary>
            /// 004-รายงานการโอนเงินระหว่างธนาคาร
            /// </summary>
            Transfer_Money_Between_Banks,
            /// <summary>
            /// 005-รายงาน Bank Statement
            /// </summary>
            Bank_Statement,
            /// <summary>
            /// 006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
            /// </summary>
            Received_Cash_sub_item,
            /// <summary>
            /// 007-รายงานการตั้งเบิกเงินสดย่อย
            /// </summary>
            Open_sub_Cash,
            /// <summary>
            /// 008-รายงานเคลื่อนไหวเงินสด
            /// </summary>
            Cash_movements,
            /// <summary>
            /// 009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
            /// </summary>
            Pay_Cash_by_DocDate,
            /// <summary>
            /// 010-รายงานสมุดจ่ายเงินประจำเดือน
            /// </summary>
            Monthly_Payment_Book,
            /// <summary>
            /// 011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
            /// </summary>
            Book_Bank_Balance,
            /// <summary>
            /// 012-รายงานเคลื่อนไหวเงินสดย่อย
            /// </summary>
            Sub_Cash_Movements,
            /// <summary>
            /// 013-รายงานสรุปการจ่ายเงิน
            /// </summary>
            Summary_Payment
            

        }

    }


}
