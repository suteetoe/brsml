using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
namespace SMLERPReport.ap
{
    public partial class _report_ap : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        //_conditonReceipt _conditonReceiptScreen = new _conditonReceipt();
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;
        //DataSet _ds;
        DataTable _dataTable;
        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");

        private ArrayList _ap_col_name_1 = new ArrayList();
        private ArrayList _ap_col_name_2 = new ArrayList();
        private ArrayList _ap_col_width_1 = new ArrayList();
        private ArrayList _ap_col_width_2 = new ArrayList();
        private ArrayList _ap_col_type_1 = new ArrayList(); // 1 = Date ,  2 = string , 3 = int  , 4 = double
        private ArrayList _ap_col_type_2 = new ArrayList(); // 1 = Date ,  2 = string , 3 = int  , 4 = double
        //---------------------------------------------------------------------------------------------------
        string[] __width;
        string[] __column;
        string[] __width_2;
        string[] __column_2;
        //---------------------------------------------------------------------------------------------------

        private string _report_name = "";
        private int _ap_level = 0;
        private DataTable __data_ap;
        //-------------------Condition------------------------
        string _data_condition = "";
        Boolean __check_submit = false;
        //----------------------------------------------------
        StringBuilder __query;
        _condition_type_1 _con_1;
        private _apEnum _apTypeTemp;

        public _apEnum _apType
        {
            get { return _apTypeTemp; }
            set
            {
                _apTypeTemp = value;
                this.Invalidate();

            }
        }
        public _report_ap()
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

            //_view1._buildReport(SMLReport._report._reportType.Standard);
            //_view1._pageSetupDialog.PageSettings.Landscape = true;         
            //_showCondition();
        }

        void _view1__loadDataByThread()
        {
            // this._dataTable == null ไม่ต้อง load ซ้ำ
            if (this._dataTable == null)
            {
                //
                /*this._dataTable = new DataTable();
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                if (this._dataTable.Rows.Count == 0)
                {

                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                            __from_ap = __data_split[1];
                            __to_ap = __data_split[2];

                            __from_date = "2009-07-22";
                        }
                        if (__from_ap.Equals("")) __from_ap = "0";
                        if (__to_ap.Equals("")) __to_ap = "z";
                        StringBuilder __where_data = new StringBuilder();
                        if (this.__data_ap != null)
                        {
                            for (int __row = 0; __row < this.__data_ap.Rows.Count; __row++)
                            {
                                __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, this.__data_ap.Rows[__row][0].ToString(), this.__data_ap.Rows[__row][1].ToString()));
                                if (__row != this.__data_ap.Rows.Count - 1)
                                {
                                    __where_data.Append(" or ");
                                }
                            }
                        }
                        else
                        {
                            __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, __from_ap, __to_ap));
                        }
                        string __where = " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " between \'" + __from_ap + "\' and \'" + __to_ap + "\'";
                        string __orderBy = "order by " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " limit 10";
                        string __where2 = "";
                        this._dataTable = __smlFrameWork._processApStatus(MyLib._myGlobal._databaseName, __from_date, __where, __where2, __orderBy, 1, -1, -1).detail.Tables[0];

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

*/


                if (_apType == _apEnum.Status_Payable)//007-รายงานสถานะเจ้าหนี้
                {
                    string __from_ap = "";
                    string __to_ap = "";
                    string __from_date = "";
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                            __from_ap = __data_split[1];
                            __to_ap = __data_split[2];
                            /*string __from_date = __data_split[3];
                            string __to_date = __data_split[4];*/
                            //__from_date = "2009-07-22";
                        }
                        if (__from_ap.Equals("")) __from_ap = "0";
                        if (__to_ap.Equals("")) __to_ap = "z";
                        StringBuilder __where_data = new StringBuilder();
                        if (this.__data_ap != null)
                        {
                            for (int __row = 0; __row < this.__data_ap.Rows.Count; __row++)
                            {
                                __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, this.__data_ap.Rows[__row][0].ToString(), this.__data_ap.Rows[__row][1].ToString()));
                                if (__row != this.__data_ap.Rows.Count - 1)
                                {
                                    __where_data.Append(" or ");
                                }
                            }
                        }
                        else
                        {
                            __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, __from_ap, __to_ap));
                        }
                        string __where = " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " between \'" + __from_ap + "\' and \'" + __to_ap + "\'";
                        string __orderBy = "order by " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " limit 10";
                        string __where2 = "";
                        this._dataTable = __smlFrameWork._processApStatus(MyLib._myGlobal._databaseName, __from_date, __where, __where2, __orderBy, 1, -1, -1).detail.Tables[0];

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                else if (_apType == _apEnum.Payable_Ageing)//021-รายงานอายุเจ้าหนี้
                {
                    string __from_ap = "";
                    string __to_ap = "";
                    string __to_date = "";
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this._data_condition.Replace("\'", "").Split(',');

                            __from_ap = __data_split[0];
                            __to_ap = __data_split[1];
                            __to_date = __data_split[2];
                        }
                        StringBuilder __where_data = new StringBuilder();
                        if (this.__data_ap != null)
                        {
                            for (int __row = 0; __row < this.__data_ap.Rows.Count; __row++)
                            {
                                __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, this.__data_ap.Rows[__row][0].ToString(), this.__data_ap.Rows[__row][1].ToString()));
                                if (__row != this.__data_ap.Rows.Count - 1)
                                {
                                    __where_data.Append(" or ");
                                }
                            }
                        }
                        else
                        {
                            __where_data.Append(string.Format("({0} between \'{1}\' and \'{2}\')", _g.d.ar_customer._code, __from_ap, __to_ap));
                        }
                        /*if (__from_ap.Equals("")) __from_ap = "0";
                        if (__to_ap.Equals("")) __to_ap = "z";*/
                        __to_date = "2009-07-28";
                        string __where = " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " between \'" + __from_ap + "\' and \'" + __to_ap + "\'";
                        string __orderBy = "order by " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " limit 10";
                        this._dataTable = __smlFrameWork._processApAgeing(MyLib._myGlobal._databaseName, __to_date, 0, 0, 0, 0, __where, __orderBy).Tables[0];
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }

                //Detail_Payable,//001-รายงานรายละเอียดเจ้าหนี้ 
                else if (_apType == _apEnum.Detail_Payable)
                {
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                    try
                    {
                        string __endDate = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._workingDate);
                        string __where = "";
                        string __where2 = "";
                        string __orderBy = "";
                        if (this._con_1 != null)
                        {
                            __endDate = this._con_1._condition_ap1._getDataStrQuery(_g.d.resource_report._balance_date).Replace("'", "");
                            string __getWhere = this._con_1._screen_grid_ap._createWhere(_g.d.ap_supplier._table + "." + _g.d.ap_supplier._code);
                            __where = this._con_1._whereControl._getWhere1(__getWhere);
                            __where2 = this._con_1._whereControl._getWhere2();
                            __orderBy = this._con_1._whereControl._getOrderBy();
                        }
                        this._dataTable = __smlFrameWork._processApStatus(MyLib._myGlobal._databaseName, __endDate, __where, __where2, __orderBy, 0, -1, -1).detail.Tables[0];
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                    if (__smlFrameWork._lastError.Length > 0)
                    {
                        MessageBox.Show(__smlFrameWork._lastError);
                    }
                }
                // รายงานเอกสารยกมาต้นปี , อื่นๆ
                else if (
                    _apType == _apEnum.Documents_Early_Year ||
                    _apType == _apEnum.Documents_Early_Year_Cancel ||
                    _apType == _apEnum.Increase_Debt ||
                    _apType == _apEnum.Increase_Debt_Cancel ||
                    _apType == _apEnum.Reduction_Dept ||
                    _apType == _apEnum.Increase_Debt_Cancel ||
                    _apType == _apEnum.Documents_Early_Year_Other ||
                    _apType == _apEnum.Documents_Early_Year_Cancel_Other ||
                    _apType == _apEnum.Increase_Debt_Other ||
                    _apType == _apEnum.Increase_Debt_Cancel_Other ||
                    _apType == _apEnum.Reduction_Dept_Other ||
                    _apType == _apEnum.Reduction_Dept_Cancel_Other)
                {
                    SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                    try
                    {
                        string __beginDate = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._workingDate);
                        string __endDate = MyLib._myGlobal._convertDateToQuery(MyLib._myGlobal._workingDate);
                        string __where = "";
                        string __where2 = "";
                        string __docBegin = "";
                        string __docEnd = "";
                        if (this._con_1 != null)
                        {
                            __beginDate = this._con_1._condition_ap1._getDataStrQuery(_g.d.resource_report._from_date).Replace("'", "");
                            __endDate = this._con_1._condition_ap1._getDataStrQuery(_g.d.resource_report._to_date).Replace("'", "");
                            __docBegin = this._con_1._condition_ap1._getDataStr(_g.d.resource_report._from_docno).ToUpper().Trim();
                            __docEnd = this._con_1._condition_ap1._getDataStr(_g.d.resource_report._to_docno).ToUpper().Trim();
                            string __getWhere = this._con_1._screen_grid_ap._createWhere(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code);
                            __where = this._con_1._whereControl._getWhere1(__getWhere);
                            __where2 = this._con_1._whereControl._getWhere2();
                        }
                        if (__docBegin.Length == 0) __docBegin = __docEnd;
                        if (__docEnd.Length == 0) __docEnd = __docBegin;
                        if (__docBegin.Length != 0)
                        {
                            if (__where.Length == 0)
                            {
                                __where = " where ";
                            }
                            else
                            {
                                __where = __where + " and ";
                            }
                            __where = __where + "(" + _g.d.ap_ar_trans._doc_no + " between \'" + __docBegin + "\' and \'" + __docEnd + "\') ";
                        }
                        int _trans_flag = this._apFlag(_apType);
                        this._dataTable = __smlFrameWork._process_ap_trans(MyLib._myGlobal._databaseName, __beginDate, __endDate, __where, __where2, "order by " + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._doc_no, 0, _trans_flag, -1, -1).detail.Tables[0];
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                        MessageBox.Show(__smlFrameWork._lastError);
                    }
                }
                //Payable_Other,//003-รายงานการตั้งเจ้าหนี้อื่นๆ
                /*else if (_apType == _apEnum.Payable_Other)
                {
                    string __from_ap = "";
                    string __to_ap = "";
                    string __from_doc_no = "";
                    string __to_doc_no = "";
                    string __from_date = "";
                    string __to_date = "";
                    string __from_sale_person = "";
                    string __to_sale_person = "";
                    string __from_department = "";
                    string __to_department = "";
                    string __not_list_account = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                            __from_ap = __data_split[4];
                            __to_ap = __data_split[5];
                            __from_doc_no = __data_split[2];
                            __to_doc_no = __data_split[3];
                            __from_date = __data_split[0];
                            __to_date = __data_split[1];
                            __from_sale_person = __data_split[6];
                            __to_sale_person = __data_split[7];
                            __from_department = __data_split[8];
                            __to_department = __data_split[9];
                            __not_list_account = __data_split[10];
                        }
                        if (__from_ap.Equals("")) __from_ap = "0";
                        if (__to_ap.Equals("")) __to_ap = "z";
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }*/
            }
            //Movement_Payable,//004-รายงานเคลื่อนไหวเจ้าหนี้     
            else if (_apType == _apEnum.Movement_Payable)
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                string __from_period = "";
                string __to_period = "";
                string __from_payable = "";
                string __to_payable = "";
                string __to_date = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        __from_period = __data_split[0];
                        __to_period = __data_split[1];
                        __from_payable = __data_split[4];
                        __to_payable = __data_split[5];
                    }
                    if (__from_payable.Equals("")) __from_payable = "0";
                    if (__to_payable.Equals("")) __to_payable = "z";
                    __to_date = "2009-12-31";
                    string __where = " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " between \'" + __from_payable + "\' and \'" + __to_payable + "\'";
                    string __orderBy = "order by " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " limit 10";
                    string __where2 = "";
                    this._dataTable = __smlFrameWork._processApStatus(MyLib._myGlobal._databaseName, __to_date, __where, __where2, __orderBy, 3, -1, -1).detail.Tables[0];

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Invoice_Arrears_by_Date,//005-รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่ 
            else if (_apType == _apEnum.Invoice_Arrears_by_Date)
            {
                string __balance_at_date = "";
                string __from_bill = "";
                string __to_bill = "";
                string __from_due_date = "";
                string __to_due_date = "";
                string __from_docno = "";
                string __to_docno = "";
                string __from_payable = "";
                string __to_payable = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        __balance_at_date = __data_split[0];
                        __from_bill = __data_split[1];
                        __to_bill = __data_split[2];
                        __from_due_date = __data_split[3];
                        __to_due_date = __data_split[4];
                        __from_docno = __data_split[5];
                        __to_docno = __data_split[6];
                        __from_payable = __data_split[7];
                        __to_payable = __data_split[8];
                    }
                    if (__from_payable.Equals("")) __from_payable = "0";
                    if (__to_payable.Equals("")) __to_payable = "z";

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Invoice_Due_by_Date,//006-รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
            else if (_apType == _apEnum.Invoice_Due_by_Date)
            {
                string _from_due_date = "";
                string _to_due_date = "";
                string _from_bill = "";
                string _to_bill = "";
                string _from_payable = "";
                string _to_payable = "";
                string _from_docno = "";
                string _to_docno = "";
                string _from_amount = "";
                string _to_amount = "";
                string _from_credit_day = "";
                string _to_credit_day = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_due_date = __data_split[0];
                        _to_due_date = __data_split[1];
                        _from_bill = __data_split[2];
                        _to_bill = __data_split[3];
                        _from_payable = __data_split[4];
                        _to_payable = __data_split[5];
                        _from_docno = __data_split[6];
                        _to_docno = __data_split[7];
                        _from_amount = __data_split[8];
                        _to_amount = __data_split[9];
                        _from_credit_day = __data_split[10];
                        _to_credit_day = __data_split[11];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Invoice_Overdue_by_Date,//008-รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่  
            else if (_apType == _apEnum.Invoice_Overdue_by_Date)
            {
                try
                {

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Billing_Value_by_Invoice,//009-รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ  
            else if (_apType == _apEnum.Billing_Value_by_Invoice)
            {
                string _from_invoice = "";
                string _to_invoice = "";
                string _from_invoice_date = "";
                string _to_invoice_date = "";
                string _from_payable = "";
                string _to_payable = "";
                string _from_bill_of_landing_place = "";
                string _to_bill_of_landing_place = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_invoice = __data_split[0];
                        _to_invoice = __data_split[1];
                        _from_invoice_date = __data_split[2];
                        _to_invoice_date = __data_split[3];
                        _from_payable = __data_split[4];
                        _to_payable = __data_split[5];
                        _from_bill_of_landing_place = __data_split[6];
                        _to_bill_of_landing_place = __data_split[7];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Billing_Outstanding,//010-รายงานใบรับวางบิลค่าสินค้าที่คงค้าง 
            else if (_apType == _apEnum.Billing_Outstanding)
            {
                string _from_invoice = "";
                string _to_invoice = "";
                string _from_invoice_date = "";
                string _to_invoice_date = "";
                string _from_payable = "";
                string _to_payable = "";
                string _from_bill_of_landing_place = "";
                string _to_bill_of_landing_place = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_invoice = __data_split[2];
                        _to_invoice = __data_split[3];
                        _from_invoice_date = __data_split[0];
                        _to_invoice_date = __data_split[1];
                        _from_payable = __data_split[4];
                        _to_payable = __data_split[5];
                        _from_bill_of_landing_place = __data_split[6];
                        _to_bill_of_landing_place = __data_split[7];
                    }
                    if (_from_payable.Equals("null")) _from_payable = "0";
                    if (_to_payable.Equals("null")) _to_payable = "z";

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Payment_Detail,//011-รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย 
            else if (_apType == _apEnum.Payment_Detail)
            {
                string _from_date = "";
                string _to_date = "";
                string _from_docno = "";
                string _to_docno = "";
                string _from_payable = "";
                string _to_payable = "";
                string _not_total_lift_zero = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_date = __data_split[0];
                        _to_date = __data_split[1];
                        _from_docno = __data_split[2];
                        _to_docno = __data_split[3];
                        _from_payable = __data_split[4];
                        _to_payable = __data_split[5];
                        _not_total_lift_zero = __data_split[6];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Invoice_Arrears_Due,//012-รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด  
            else if (_apType == _apEnum.Invoice_Arrears_Due)
            {
                string _from_invoice_date = "";
                string _to_invoice_date = "";
                string _from_invoice = "";
                string _to_invoice = "";
                string _from_due_date = "";
                string _to_due_date = "";
                string _from_payable = "";
                string _to_payable = "";
                string _from_sale_person = "";
                string _to_sale_person = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_invoice_date = __data_split[0];
                        _to_invoice_date = __data_split[1];
                        _from_invoice = __data_split[2];
                        _to_invoice = __data_split[3];
                        _from_due_date = __data_split[4];
                        _to_due_date = __data_split[5];
                        _from_payable = __data_split[6];
                        _to_payable = __data_split[7];
                        _from_sale_person = __data_split[8];
                        _to_sale_person = __data_split[9];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Payment_by_Invoice,//013-รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ 
            else if (_apType == _apEnum.Payment_by_Invoice)
            {
                string _from_invoice_date = "";
                string _to_invoice_date = "";
                string _from_invoice = "";
                string _to_invoice = "";
                string _from_payable = "";
                string _to_payable = "";
                string _from_docno = "";
                string _to_docno = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_invoice_date = __data_split[0];
                        _to_invoice_date = __data_split[1];
                        _from_invoice = __data_split[2];
                        _to_invoice = __data_split[3];
                        _from_payable = __data_split[4];
                        _to_payable = __data_split[5];
                        _from_docno = __data_split[6];
                        _to_docno = __data_split[7];
                    }
                    if (_from_payable.Equals("null")) _from_payable = "0";
                    if (_to_payable.Equals("null")) _to_payable = "z";
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Payment_by_Date,//015-รายงานการจ่ายเงินประจำวัน-ตามวันที่  
            else if (_apType == _apEnum.Payment_by_Date)
            {
                string _from_date = "";
                string _to_date = "";
                string _from_docno = "";
                string _to_docno = "";
                string _from_payable = "";
                string _to_payable = "";
                string _from_department = "";
                string _to_department = "";
                string _from_project = "";
                string _to_project = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_date = __data_split[0];
                        _to_date = __data_split[1];
                        _from_docno = __data_split[2];
                        _to_docno = __data_split[3];
                        _from_payable = __data_split[4];
                        _to_payable = __data_split[5];
                        _from_department = __data_split[6];
                        _to_department = __data_split[7];
                        _from_project = __data_split[8];
                        _to_project = __data_split[9];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Payment_by_Department,//016-รายงานการจ่ายเงินประจำวัน-ตามแผนก  
            else if (_apType == _apEnum.Payment_by_Department)
            {
                string _from_date = "";
                string _to_date = "";
                string _from_docno = "";
                string _to_docno = "";
                string _from_payable = "";
                string _to_payable = "";
                string _from_department = "";
                string _to_department = "";
                string _from_project = "";
                string _to_project = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_date = __data_split[0];
                        _to_date = __data_split[1];
                        _from_docno = __data_split[2];
                        _to_docno = __data_split[3];
                        _from_payable = __data_split[4];
                        _to_payable = __data_split[5];
                        _from_department = __data_split[6];
                        _to_department = __data_split[7];
                        _from_project = __data_split[8];
                        _to_project = __data_split[9];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Debt_Cut_Detail,//017-รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย  
            else if (_apType == _apEnum.Debt_Cut_Detail)
            {
                string _from_docno = "";
                string _to_docno = "";
                string _from_invoice_date = "";
                string _to_invoice_date = "";
                string _from_payable = "";
                string _to_payable = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_docno = __data_split[0];
                        _to_docno = __data_split[1];
                        _from_invoice_date = __data_split[4];
                        _to_invoice_date = __data_split[5];
                        _from_payable = __data_split[2];
                        _to_payable = __data_split[3];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Debt_Cut_by_Date,//018-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่  
            else if (_apType == _apEnum.Debt_Cut_by_Date)
            {
                string _from_docno = "";
                string _to_docno = "";
                string _from_invoice_date = "";
                string _to_invoice_date = "";
                string _from_payable = "";
                string _to_payable = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_docno = __data_split[0];
                        _to_docno = __data_split[1];
                        _from_invoice_date = __data_split[4];
                        _to_invoice_date = __data_split[5];
                        _from_payable = __data_split[2];
                        _to_payable = __data_split[3];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Debt_Cut_by_Payable,//019-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
            else if (_apType == _apEnum.Debt_Cut_by_Payable)
            {
                string _from_docno = "";
                string _to_docno = "";
                string _from_invoice_date = "";
                string _to_invoice_date = "";
                string _from_payable = "";
                string _to_payable = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_docno = __data_split[4];
                        _to_docno = __data_split[5];
                        _from_invoice_date = __data_split[2];
                        _to_invoice_date = __data_split[3];
                        _from_payable = __data_split[0];
                        _to_payable = __data_split[1];
                    }
                    if (_from_payable.Equals("")) _from_payable = "0";
                    if (_to_payable.Equals("")) _to_payable = "z";

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            //Payable_by_Currency,//020-รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  
            else if (_apType == _apEnum.Payable_by_Currency)
            {
                string _from_payable = "";
                string _to_payable = "";
                string _from_currency = "";
                string _to_currency = "";
                string _annual_period = "";
                try
                {
                    if (this.__check_submit)
                    {
                        string[] __data_split = this._data_condition.Replace("\'", "").Split(',');
                        _from_payable = __data_split[0];
                        _to_payable = __data_split[1];
                        _from_currency = __data_split[2];
                        _to_currency = __data_split[3];
                        _annual_period = __data_split[4];
                    }
                    if (_from_payable.Equals("null")) _from_payable = "0";
                    if (_to_payable.Equals("null")) _to_payable = "z";

                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
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
                //double __total = 0;

                // MOO เอาไว้ก่อน เดี๋ยวเขียนใหม่

                /* วางยาอีกแล้ว ทำให้รายงาน เบิ้ล ต้อง if _apTypeTemp ด้วย มันจะได้ไม่ปนกัน
                 * for (int _i = 0; _i < __dr.Length; _i++)
                {

                    SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(_objReport, __dataObject);
                    for (int __col = 0; __col < this._ap_col_name_1.Count; __col++)
                    {
                        string __data = "";
                        if (!_ap_col_name_1[__col].Equals(""))
                        {
                            if (_ap_col_name_1[__col].ToString().Equals(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date))
                            {
                                __data = __dr[_i][_ap_col_name_1[__col].ToString()].ToString();
                            }
                            else if (_ap_col_name_1[__col].ToString().Equals(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_net_value) ||
                                _ap_col_name_1[__col].ToString().Equals(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_debt_balance))
                            {

                                double __debtBalance = Double.Parse(__dr[_i][__column[__col]].ToString());
                                __data = string.Format(_formatNumber, __debtBalance);
                            }
                            else
                            {
                                __data = __dr[_i][__column[__col]].ToString();
                            }
                            _view1._addDataColumn(_objReport, __dataObject, __col, __data.ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                            _no++;
                        }
                        if (this._ap_level == 2)
                        {
                            if (_ap_col_name_2.Count > 0)
                            {

                                string __data_detail = "";
                                if (!_ap_col_name_1[__col].Equals(""))
                                {
                                    if (_ap_col_name_1[__col].ToString().Equals(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date))
                                    {
                                        __data_detail = __dr[_i][_ap_col_name_1[__col].ToString()].ToString();
                                    }
                                    else if (_ap_col_name_1[__col].ToString().Equals(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_net_value) ||
                                        _ap_col_name_1[__col].ToString().Equals(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_debt_balance))
                                    {
                                        double __debtBalance = Double.Parse(__dr[_i][__column[__col]].ToString());
                                        __data_detail = string.Format(_formatNumber, __debtBalance);
                                    }
                                    else
                                    {
                                        __data_detail = __dr[_i][__column[__col]].ToString();
                                    }
                                    _view1._addDataColumn(_objReport, __dataObject, __col, __data.ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                                    _no++;
                                }

                            }
                        }


                    }
                }
*/



                //Detail_Payable//001-รายงานรายละเอียดเจ้าหนี้ 
                if (_apTypeTemp == _apEnum.Detail_Payable)
                {
                    double __total = 0;
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);

                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][_g.d.ap_supplier._table + "." + _g.d.ap_supplier._code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][_g.d.ap_supplier._table + "." + _g.d.ap_supplier._name_1].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][_g.d.ap_supplier._table + "." + _g.d.ap_supplier._address].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i][_g.d.ap_supplier._table + "." + _g.d.ap_supplier._telephone].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][_g.d.ap_supplier._table + "." + _g.d.ap_supplier._fax].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        double __debtBalance = Double.Parse(__dr[_i][_g.d.ap_supplier._table + "." + _g.d.ap_supplier._debt_balance].ToString());
                        _view1._addDataColumn(_objReport, __dataObject, 5, (__debtBalance == 0) ? "" : string.Format(_formatNumber, __debtBalance), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
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
                    _view1._addDataColumn(_objReport, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.TopBottom , SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 5, string.Format(_formatNumber, __total), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                }
                //Documents_Early_Year,//002-รายงานเอกสารยกมาต้นปี  //022-รายงานเอกสารเพิ่มหนี้ยกมา	//023-รายงานเอกสารลดหนี้ยกมา	//024-รายงานเอกสารตั้งหนี้
                else if (
                        _apType == _apEnum.Documents_Early_Year ||
                        _apType == _apEnum.Documents_Early_Year_Cancel ||
                        _apType == _apEnum.Increase_Debt ||
                        _apType == _apEnum.Increase_Debt_Cancel ||
                        _apType == _apEnum.Reduction_Dept ||
                        _apType == _apEnum.Increase_Debt_Cancel ||
                        _apType == _apEnum.Documents_Early_Year_Other ||
                        _apType == _apEnum.Documents_Early_Year_Cancel_Other ||
                        _apType == _apEnum.Increase_Debt_Other ||
                        _apType == _apEnum.Increase_Debt_Cancel_Other ||
                        _apType == _apEnum.Reduction_Dept_Other ||
                        _apType == _apEnum.Reduction_Dept_Cancel_Other)
                {
                    double __total1 = 0;
                    double __total2 = 0;
                    for (int _i = 0; _i < __dr.Length; _i++)
                    {

                        //_rowprogreesbar = (_i + 1 * 100) / __dr.Length;
                        //_view1._reportProgressBar.Value = _rowprogreesbar;
                        //loopprogressbar = loopprogressbar + _rowprogreesbar;
                        //_view1._reportStatus.Text = loopprogressbar + "%";

                        //_view1._reportProgressBar.Style = ProgressBarStyle.Continuous;

                        SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(_objReport, __dataObject);

                        _view1._addDataColumn(_objReport, __dataObject, 0, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._convertDate(__dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date].ToString()), false), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_name].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._remark].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        double __totalDebtAmount = 0;
                        if (!__dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_net_value].ToString().Equals(""))
                        {
                            string __getData1 = __dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_net_value].ToString();
                            __totalDebtAmount = Double.Parse(__getData1);
                        }
                        _view1._addDataColumn(_objReport, __dataObject, 5, (__totalDebtAmount == 0) ? "" : string.Format(_formatNumber, __totalDebtAmount), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        double __totalDebtBalance = 0;
                        if (!__dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_debt_balance].ToString().Equals(""))
                        {
                            __totalDebtBalance = Double.Parse(__dr[_i][_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._total_debt_balance].ToString());
                        }
                        _view1._addDataColumn(_objReport, __dataObject, 6, (__totalDebtBalance == 0) ? "" : string.Format(_formatNumber, __totalDebtBalance), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        __total1 += __totalDebtAmount;
                        __total2 += __totalDebtBalance;
                        _no++;
                        //_view1._reportProgressBar.Value = loopprogressbar;
                    }
                    SMLReport._report._objectListType __dataObject2 = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(_objReport, __dataObject2);
                    _view1._addDataColumn(_objReport, __dataObject2, 0, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 1, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 2, "รวม  : " + string.Format(_formatCountNumber, _no) + " รายการ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 4, "", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject2, 5, string.Format(_formatNumber, __total1), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                    _view1._addDataColumn(_objReport, __dataObject2, 6, string.Format(_formatNumber, __total2), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.TopBottom, SMLReport._report._cellType.Number);
                }


                //Movement_Payable//004-รายงานเคลื่อนไหวเจ้าหนี้    
                else if (_apType == _apEnum.Movement_Payable)
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
                                _view1._addDataColumn(_objReport2, __dataObject2, 6, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 7, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
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
                //Invoice_Arrears_by_Date//005-รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่ 
                else if (_apType == _apEnum.Invoice_Arrears_by_Date)
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
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 12, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                //Invoice_Due_by_Date//006-รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
                else if (_apType == _apEnum.Invoice_Due_by_Date)
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
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
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
                //Status_Payable//007-รายงานสถานะเจ้าหนี้ 
                else if (_apType == _apEnum.Status_Payable)
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
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i]["code"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i]["name_1"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i]["sum_befor_cb_balance"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i]["sum_ic_sale_balance"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i]["sum_debt_balance"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i]["sum_increase_debt"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i]["sum_debt_billing"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i]["sum_debt_value"].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Number);
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
                //Invoice_Overdue_by_Date//008-รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่ 
                else if (_apType == _apEnum.Invoice_Overdue_by_Date)
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
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 12, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 13, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
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
                //Billing_Value_by_Invoice//009-รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ
                else if (_apType == _apEnum.Billing_Value_by_Invoice)
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
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                //Billing_Outstanding//010-รายงานใบรับวางบิลค่าสินค้าที่คงค้าง
                else if (_apType == _apEnum.Billing_Outstanding)
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
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                //Payment_Detail//011-รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย 
                else if (_apType == _apEnum.Payment_Detail)
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
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                                _view1._addDataColumn(_objReport2, __dataObject2, 6, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                //Invoice_Arrears_Due//012-รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด 
                else if (_apType == _apEnum.Invoice_Arrears_Due)
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
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                //Payment_by_Invoice//013-รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ 
                else if (_apType == _apEnum.Payment_by_Invoice)
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
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
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
                //Payment_by_Date//015-รายงานการจ่ายเงินประจำวัน-ตามวันที่  
                else if (_apType == _apEnum.Payment_by_Date)
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
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 12, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
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
                //Payment_by_Department//016-รายงานการจ่ายเงินประจำวัน-ตามแผนก
                else if (_apType == _apEnum.Payment_by_Department)
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
                                _view1._addDataColumn(_objReport2, __dataObject2, 6, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 7, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 8, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 9, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 10, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 11, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                                _view1._addDataColumn(_objReport2, __dataObject2, 12, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
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
                //Debt_Cut_Detail//017-รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย
                else if (_apType == _apEnum.Debt_Cut_Detail)
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
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


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
                //Debt_Cut_by_Date,//018-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่  
                else if (_apType == _apEnum.Debt_Cut_by_Date)
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
                        /*_view1._addDataColumn(_objReport, __dataObject, 2, " ", __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 3, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 4, " ", __newFont, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Text);
                         _view1._addDataColumn(_objReport, __dataObject, 5, " ", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        _view1._addDataColumn(_objReport, __dataObject, 6, "รวมเงิน:", __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.Double);
                        */
                        _view1._createEmtryColumn(_objReport, __dataObject);
                    }
                }
                //Debt_Cut_by_Payable,//019-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
                else if (_apType == _apEnum.Debt_Cut_by_Payable)
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
                //Payable_by_Currency//020-รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  
                else if (_apType == _apEnum.Payable_by_Currency)
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
                //Payable_Ageing//021-รายงานอายุเจ้าหนี้
                else if (_apType == _apEnum.Payable_Ageing)
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
                        _view1._addDataColumn(_objReport, __dataObject, 0, __dr[_i][0].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 1, __dr[_i][1].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 2, __dr[_i][2].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 3, __dr[_i][3].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 4, __dr[_i][4].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 5, __dr[_i][5].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][6].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][7].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][8].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][9].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                        _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][10].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
                MessageBox.Show(" >>  " + ex.Message);
            }
        }

        private string[] _ap_detail_column()
        {
            string[] __result = { _g.d.ap_supplier._table+"."+_g.d.ap_supplier._code, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._name_1, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._address, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._telephone, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._fax, 
                                  _g.d.ap_supplier._table +"."+_g.d.ap_supplier._debt_balance+"*"}; // มี * เพื่อบังคับให้ชิดขวา -- jead เอาดอกจันออกทำไม ผมจะให้มันชิดขวา
            return __result;
        }

        private string[] _ap_year_balance_column()
        {
            //string[] __column = { "วัน", "เลขที่เอกสาร", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ",  "ยอดสุทธิ", "ยอดคงเหลือ" };
            string[] __result = { _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_date,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_no,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_code,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_name,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._remark,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_net_value+"*",
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_debt_balance+"*"};

            return __result;
        }

        public void _ap_config()
        {
            this.__query = new StringBuilder();
            this._report_name = _apReportName(_apType);
            if (this._apType == _apEnum.Detail_Payable)
            {
                string __resourceCode = _g.d.resource_report_name._table + "." + _g.d.resource_report_name._ap_detail;
                this._report_name = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;
                _view1.__excelFlieName = ((MyLib._myResourceType)MyLib._myResource._findResource(__resourceCode, __resourceCode))._str;
                string[] __width = { "10", "20", "40", "10", "10", "10" };
                string[] __column = _ap_detail_column();
                string[] __type = { "2", "2", "2", "2", "2", "4" };
                this._setColumnName(__width, __column, null, null);
                this._ap_level = 1;
            }
            else if (
                       _apType == _apEnum.Documents_Early_Year ||
                       _apType == _apEnum.Increase_Debt ||
                       _apType == _apEnum.Reduction_Dept ||
                       _apType == _apEnum.Documents_Early_Year_Other ||
                       _apType == _apEnum.Increase_Debt_Other ||
                       _apType == _apEnum.Reduction_Dept_Other)
            {
                string[] __width = { "10", "10", "10", "30", "20", "10", "10" };
                string[] __column = _ap_year_balance_column();
                string[] __type = { "1", "2", "2", "2", "2", "4", "4" };
                this._setColumnName(__width, __column, null, null);
                this._ap_level = 1;
            }
            else if (
                   _apType == _apEnum.Documents_Early_Year_Cancel ||
                   _apType == _apEnum.Increase_Debt_Cancel ||
                   _apType == _apEnum.Increase_Debt_Cancel ||
                   _apType == _apEnum.Documents_Early_Year_Cancel_Other ||
                   _apType == _apEnum.Increase_Debt_Cancel_Other)
            {
                string[] __width = { "10", "10", "10", "30", "20", "10", "10" };
                string[] __column = _ap_year_balance_column();
                this._setColumnName(__width, __column, null, null);
                this._ap_level = 1;
            }
            else if (_apType == _apEnum.Movement_Payable)
            {
                string[] __width = { "8", "8" };
                string[] __column = { "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้" };
                string[] __width_2 = { "8", "8", "8", "8", "8", "10", "10", "10" };
                string[] __column_2 = { "วันที่", "เลขที่เอกสาร", "เลขที่อ้างอิง", "ประเภทเอกสาร", "ยอดเดบิต", "ยอดเครดิต", "ยอดคงเหลือ", "กำไร/ขาดทุน" };
                this._ap_level = 2;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //Invoice_Arrears_by_Date//005-รายงานใบส่งของค้างจ่ายชำระ-ตามวันที่ 
            else if (_apType == _apEnum.Invoice_Arrears_by_Date)
            {

                string __queryGetApCOde = "";

                _view1.__excelFlieName = "รายงานใบส่งของค้างจ่ายชำระ-ตามวันที่";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8", "8", "10", "10", "10" };
                string[] __column = { "วันเอกสาร", "เลขที่เอกสาร", "ใบกำกับภาษี", "ครบกำหนด", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "ยอดตั้งหนี้", "ยอดลดหนี้", "จ่ายชำระหนี้", "ยอดคงเหลือ", "เกิน(วัน)", "ข้อมูลรายวัน" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Invoice_Due_by_Date//006-รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
            else if (_apType == _apEnum.Invoice_Due_by_Date)
            {
                _view1.__excelFlieName = "รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8", "8", "10", "10" };
                string[] __column = { "วันเอกสาร", "เลขที่เอกสาร", "ใบกำกับภาษี", "ครบกำหนด", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "จำนวนเงิน", "ยอดลดหนี้", "จ่ายชำระหนี้", "ยอดคงเหลือ", "ข้อมูลรายวัน" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Status_Payable//007-รายงานสถานะเจ้าหนี้ 
            else if (_apType == _apEnum.Status_Payable)
            {
                _view1.__excelFlieName = "รายงานสถานะเจ้าหนี้";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10" };
                string[] __column = { "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "ยอดยกมา", "ยอดตั้งเจ้าหนี้", "ยอดลดหนี้", "ยอดเพิ่มหนี้", "ยอดตัดชำระหนี้", "ยอดยกไป/ขาดทุนประจำงวด" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Invoice_Overdue_by_Date//008-รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่ 
            else if (_apType == _apEnum.Invoice_Overdue_by_Date)
            {
                _view1.__excelFlieName = "รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8", "8", "10", "10", "10", "10" };
                string[] __column = { "วันเอกสาร", "เลขที่เอกสาร", "คำอธิบาย", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "ครบกำหนด", "ไม่อยู่ในช่วง", "จาก 1-7 วัน", "จาก 8-14 วัน", "จาก 15-21 วัน", "จาก 22-28 วัน", "เกินกว่า 30 วัน", "เกิน(วัน)", "ข้อมูลรายวัน" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Billing_Value_by_Invoice//009-รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ
            else if (_apType == _apEnum.Billing_Value_by_Invoice)
            {
                _view1.__excelFlieName = "รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8" };
                string[] __column = { "เลขที่เอกสาร", "วันเอกสาร", "เลขที่ใบรับวางบิล", "ครบกำหนด", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "ประเภทเอกสาร", "มูลค่าที่รับวางบิล" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Billing_Outstanding//010-รายงานใบรับวางบิลค่าสินค้าที่คงค้าง
            else if (_apType == _apEnum.Billing_Outstanding)
            {
                _view1.__excelFlieName = "รายงานใบรับวางบิลค่าสินค้าที่คงค้าง";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8" };
                string[] __column = { "เลขที่เอกสาร", "วันเอกสาร", "เลขที่ใบรับวางบิล", "ครบกำหนด", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "ประเภทเอกสาร", "มูลค่าที่รับวางบิล" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Payment_Detail//011-รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย 
            else if (_apType == _apEnum.Payment_Detail)
            {
                _view1.__excelFlieName = "รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย";
                string[] __width = { "8", "8", "8", "8", "8", "8", "8", "8", "8", "8" };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "ยอดรวม", "รายได้อื่นๆ", "ค่าใช้จ่ายอื่นๆ", "ยอดภาษี", "ยอดชำระหนี้" };
                string[] __width_2 = { "8", "8", "8", "8", "8", "10", "10" };
                string[] __column_2 = { "เลขที่ใบส่งของ", "วันที่ใบส่งของ", "หมายเหตุ", "ประเภทเอกสาร", "ยอดคงเหลือ ณ ตอนจ่าย", "ยอดตัดจ่าย", "กำไร/ขาดทุน" };
                this._ap_level = 2;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //Invoice_Arrears_Due//012-รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด 
            else if (_apType == _apEnum.Invoice_Arrears_Due)
            {
                _view1.__excelFlieName = "รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8" };
                string[] __column = { "เลขที่ใบส่งของ", "วันใบส่งของ", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "ประเภทเอกสาร", "เครดิต(วัน)", "วันที่ครบกำหนด", "ยอดสุทธิ", "ค้างชำระ" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Payment_by_Invoice//013-รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ 
            else if (_apType == _apEnum.Payment_by_Invoice)
            {
                _view1.__excelFlieName = "รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8", "8" };
                string[] __column = { "เลขที่ใบส่งของ", "วันที่ชำระ", "เลขที่เอกสาร", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "ประเภทเอกสาร", "ยอดรวม", "ยอดจ่าย", "กำไร/ขาดทุน" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Payment_by_Date//015-รายงานการจ่ายเงินประจำวัน-ตามวันที่  
            else if (_apType == _apEnum.Payment_by_Date)
            {
                _view1.__excelFlieName = "รายงานการจ่ายเงินประจำวัน-ตามวันที่";
                string[] __width = { "8", "8", "8", "8", "8", "10", "10", "10", "8", "8", "10", "8", "8" };
                string[] __column = { "วันที่", "เลขที่เอกสาร", "ชื่อเจ้าหนี้", "หมายเหตุ", "ยอดเงินสดจ่าย", "ยอดเช็คจ่าย", "ยอดเงินโอน", "เงินสดย่อย", "เกินใบเสร็จ", "หักเงินเกิน", "ปัดเป็นรายได้", "ปัดเป็นค่าใช้จ่าย", "ยอดเงินรวม" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, null, null);
            }
            //Payment_by_Department//016-รายงานการจ่ายเงินประจำวัน-ตามแผนก
            else if (_apType == _apEnum.Payment_by_Department)
            {
                _view1.__excelFlieName = "รายงานการจ่ายเงินประจำวัน-ตามแผนก";
                string[] __width = { "8", "8" };
                string[] __column = { "รหัสแผนก", "ชื่อแผนก" };
                string[] __width_2 = { "8", "8", "8", "8", "8", "10", "10", "10", "8", "8", "10", "8", "8" };
                string[] __column_2 = { "วันที่", "เลขที่เอกสาร", "ชื่อเจ้าหนี้", "หมายเหตุ", "ยอดเงินสดจ่าย", "ยอดเช็คจ่าย", "ยอดเงินโอน", "เงินสดย่อย", "เกินใบเสร็จ", "หักเงินเกิน", "ปัดเป็นรายได้", "ปัดเป็นค่าใช้จ่าย", "ยอดเงินรวม" };
                this._ap_level = 2;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //Debt_Cut_Detail//017-รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย
            else if (_apType == _apEnum.Debt_Cut_Detail)
            {
                _view1.__excelFlieName = "รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย";
                string[] __width = { "8", "8", "8", "8", "8" };
                string[] __column = { "เลขที่เอกสาร", "วันที่เอกสาร", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "ยอดตัดหนี้สูญ" };
                string[] __width_2 = { "8", "8", "8", "8", "8" };
                string[] __column_2 = { "เลขที่ใบส่งของ", "ลงวันที่", "วันที่ครบกำหนด", "รายวัน/คำอธิบาย", "ยอดตัดจ่าย" };
                this._ap_level = 2;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //Debt_Cut_by_Date,//018-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่  
            else if (_apType == _apEnum.Debt_Cut_by_Date)
            {
                _view1.__excelFlieName = "รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่";
                string[] __width = { "8" };
                string[] __column = { "วันที่เอกสาร" };
                string[] __width_2 = { "8", "8", "8", "8", "8" };
                string[] __column_2 = { "เลขที่เอกสาร", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ", "ยอดตัดหนี้สูญ" };
                this._ap_level = 2;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //Debt_Cut_by_Payable,//019-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
            else if (_apType == _apEnum.Debt_Cut_by_Payable)
            {
                _view1.__excelFlieName = "รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้";
                string[] __width = { "8", "8" };
                string[] __column = { "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้" };
                string[] __width_2 = { "8", "8", "8", "8" };
                string[] __column_2 = { "เลขที่เอกสาร", "วันที่เอกสาร", "หมายเหตุ", "ยอดตัดหนี้สูญ" };
                this._ap_level = 2;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //Payable_by_Currency//020-รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  
            else if (_apType == _apEnum.Payable_by_Currency)
            {
                _view1.__excelFlieName = "รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ";
                string[] __width = { "8", "8" };
                string[] __column = { "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้" };
                string[] __width_2 = { "8", "8", "8", "8" };
                string[] __column_2 = { "รหัสสกุลเงิน", "ชื่อสกุลเงิน", "Period", "ยอดหนี้เงินบาท", "ยอดหนี้อื่นๆ" };
                this._ap_level = 2;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //Payable_Ageing//021-รายงานอายุเจ้าหนี้
            else if (_apType == _apEnum.Payable_Ageing)
            {
                _view1.__excelFlieName = "รายงานอายุเจ้าหนี้";
                string[] __width = { "8", "8", "8", "8", "8", "8", "8", "8", "8", "8", "8" };
                string[] __column = { "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "ไม่อยู่ในช่วง", "ช่วง 1-7 วัน", "ช่วง 8-14 วัน", "ช่วง 15-21 วัน", "ช่วง 22-28 วัน", "เกิน 28 วัน", "ยอดลดหนี้", "ยอดเงินล่วงหน้า", "รวม" };
                string[] __width_2 = { "8", "8", "8", "8", "8", "8", "8", "8" };
                this._ap_level = 1;
                this._setColumnName(__width, __column, __width_2, __column_2);
            }
            //---------END---------------//---------END---------------

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
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "Description : " , SMLReport._report._cellAlign.Left, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 4, -1, SMLReport._report._cellType.String, "", SMLReport._report._cellAlign.Right, _view1._fontHeader2);
                //_view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.Text, "Page No. : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontHeader2);          
                // _view1._excelFileName = "รายงานยอดการชำระเงิน";//
                // _view1._maxColumn = 9;
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    if (this._ap_level == 1)
                    {
                        _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    }
                    for (int i = 0; i < this._ap_col_width_1.Count; i++)
                    {
                        //------------ADD Column----------------
                        _view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._ap_col_width_1[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._ap_col_name_1[i].ToString(), "", SMLReport._report._cellAlign.Left);
                    }
                    //--------------Level 2---------------
                    if (this._ap_level == 2 || this._ap_level == 3 || this._ap_level == 4)
                    {
                        if (this._ap_level == 3 || this._ap_level == 4)
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        }
                        else
                        {
                            _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                        }

                        for (int i = 0; i < this._ap_col_width_2.Count; i++)
                        {
                            //------------ADD Column----------------
                            _view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._ap_col_width_2[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._ap_col_name_2[i].ToString(), "", SMLReport._report._cellAlign.Left);
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
            /* if (__check_submit)
             {*/
            this._ap_col_width_1.Clear();
            this._ap_col_width_2.Clear();
            this._ap_col_name_1.Clear();
            this._ap_col_name_2.Clear();
            this._ap_config();
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
            string __page = _apType.ToString();
            if (this._con_1 == null)
            {
                // jead ทำแบบนี้เพราะ เมื่อกดเงื่อนไขซ้ำ ที่เลือกไปแล้ว จะได้เหมือนเดิม
                this._con_1 = new _condition_type_1(__page);
                //
                switch (this._apType)
                {
                    case _apEnum.Detail_Payable:
                        this._con_1._whereControl._tableName = _g.d.ap_supplier._table;
                        this._con_1._whereControl._addFieldComboBox(this._ap_detail_column());
                        break;
                    case _apEnum.Documents_Early_Year:
                        this._con_1._whereControl._tableName = _g.d.ap_ar_trans._table;
                        this._con_1._whereControl._addFieldComboBox(this._ap_year_balance_column());
                        break;
                }
                //
                this._con_1.Size = new Size(500, 500);
            }

            this._con_1.ShowDialog();
            if (this._con_1.__check_submit)
            {
                this._data_condition = this._con_1.__where;
                this.__check_submit = this._con_1.__check_submit;
                this._ap_config();
                this.__data_ap = this._con_1.__grid_where;
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

        public enum _apEnum
        {
            /// <summary>
            /// 001-รายงานรายละเอียดเจ้าหนี้ 
            /// </summary>
            Detail_Payable,//001-รายงานรายละเอียดเจ้าหนี้ 
            //---------------------------------------------------------------------------------------------------
            // รายงานเอกสารยกมาต้นปี
            // MOOOOOOOOOOOOOOOOOM
            //---------------------------------------------------------------------------------------------------
            /// <summary>
            /// 002-รายงานเอกสารยกมาต้นปี
            /// </summary>
            Documents_Early_Year,
            /// <summary>
            /// 002-รายงานเอกสารยกมาต้นปี
            /// </summary>
            Documents_Early_Year_Cancel,
            /// <summary>
            /// รายงานเอกสารเพิ่มหนี้ยกมา
            /// </summary>
            Increase_Debt,
            /// <summary>
            /// รายงานยกเลิกเอกสารเพิ่มหนี้ยกมา
            /// </summary>
            Increase_Debt_Cancel,
            /// <summary>
            /// รายงานเอกสารลดหนี้ยกมา
            /// </summary>
            Reduction_Dept,
            /// <summary>
            /// รายงานยกเลิกเอกสารลดหนี้ยกมา
            /// </summary>
            Reduction_Dept_Cancel,
            //---------------------------------------------------------------------------------------------------
            // รายงานเอกสารอื่นๆๆ
            // MOOOOOOOOOOOOOOOOOM
            //---------------------------------------------------------------------------------------------------
            /// <summary>
            /// 002-รายงานเอกสารอื่นๆ
            /// </summary>
            Documents_Early_Year_Other,
            /// <summary>
            /// 002-รายงานเอกสารยกมาอื่นๆ
            /// </summary>
            Documents_Early_Year_Cancel_Other,
            /// <summary>
            /// รายงานเอกสารเพิ่มหนี้อื่นๆ
            /// </summary>
            Increase_Debt_Other,
            /// <summary>
            /// รายงานยกเลิกเอกสารเพิ่มหนี้อื่นๆ
            /// </summary>
            Increase_Debt_Cancel_Other,
            /// <summary>
            /// รายงานเอกสารลดหนี้อื่นๆ
            /// </summary>
            Reduction_Dept_Other,
            /// <summary>
            /// รายงานยกเลิกเอกสารเลดมหนี้อื่นๆ
            /// </summary>
            Reduction_Dept_Cancel_Other,
            //---------------------------------------------------------------------------------------------------
            /// <summary>
            /// 004-รายงานเคลื่อนไหวเจ้าหนี้ 
            /// </summary>
            Movement_Payable,//004-รายงานเคลื่อนไหวเจ้าหนี้     
            /// <summary>
            /// 005-รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่ 
            /// </summary>
            Invoice_Arrears_by_Date,//005-รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่ 
            /// <summary>
            /// 006-รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
            /// </summary>
            Invoice_Due_by_Date,//006-รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
            /// <summary>
            /// 007-รายงานสถานะเจ้าหนี้
            /// </summary>
            Status_Payable,//007-รายงานสถานะเจ้าหนี้ 
            /// <summary>
            /// 008-รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่
            /// </summary>
            Invoice_Overdue_by_Date,//008-รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่  
            /// <summary>
            /// 009-รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ  
            /// </summary>
            Billing_Value_by_Invoice,//009-รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ  
            /// <summary>
            /// 010-รายงานใบรับวางบิลค่าสินค้าที่คงค้าง
            /// </summary>
            Billing_Outstanding,//010-รายงานใบรับวางบิลค่าสินค้าที่คงค้าง 
            /// <summary>
            /// 011-รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย 
            /// </summary>
            Payment_Detail,//011-รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย 
            /// <summary>
            /// 012-รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด  
            /// </summary>
            Invoice_Arrears_Due,//012-รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด  
            /// <summary>
            /// 013-รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ
            /// </summary>
            Payment_by_Invoice,//013-รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ 
            /// <summary>
            /// 015-รายงานการจ่ายเงินประจำวัน-ตามวันที่
            /// </summary>
            Payment_by_Date,//015-รายงานการจ่ายเงินประจำวัน-ตามวันที่  
            /// <summary>
            /// 016-รายงานการจ่ายเงินประจำวัน-ตามแผนก
            /// </summary>
            Payment_by_Department,//016-รายงานการจ่ายเงินประจำวัน-ตามแผนก  
            /// <summary>
            /// 017-รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย
            /// </summary>
            Debt_Cut_Detail,//017-รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย  
            /// <summary>
            /// 018-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่
            /// </summary>
            Debt_Cut_by_Date,//018-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่  
            /// <summary>
            /// 019-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
            /// </summary>
            Debt_Cut_by_Payable,//019-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
            /// <summary>
            /// 020-รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  
            /// </summary>
            Payable_by_Currency,//020-รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  
            /// <summary>
            /// 021-รายงานอายุเจ้าหนี้
            /// </summary>
            Payable_Ageing,//021-รายงานอายุเจ้าหนี้
        }




        void _setColumnName(string[] __width, string[] __column, string[] __width_2, string[] __column_2)
        {
            try
            {
                this._ap_col_width_1.Clear();
                this._ap_col_name_1.Clear();
                this._ap_col_width_2.Clear();
                this._ap_col_name_2.Clear();
                for (int i = 0; i < __width.Length; i++)
                {
                    this._ap_col_width_1.Add(__width[i]);
                    this._ap_col_name_1.Add(__column[i]);
                }
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._ap_col_width_2.Add(__width_2[i]);
                    this._ap_col_name_2.Add(__column_2[i]);
                }
            }
            catch
            {
            }
        }
        private string _apReportName(_apEnum ReportName)
        {
            switch (ReportName)
            {
                case _apEnum.Detail_Payable: return "รายงานรายละเอียดเจ้าหนี้";
                //---------------------------------------------------------------------------------------------------
                // รายงานเอกสารยกมาต้นปี
                // MOOOOOOOOOOOOOOOOOM
                //---------------------------------------------------------------------------------------------------
                case _apEnum.Documents_Early_Year: return "รายงานเอกสารยกมาต้นปี";
                case _apEnum.Documents_Early_Year_Cancel: return "รายงานยกเลิกเอกสารยกมาต้นปี";
                case _apEnum.Increase_Debt: return "รายงานเอกสารเพิ่มหนี้ยกมา";
                case _apEnum.Increase_Debt_Cancel: return "รายงานยกเลิกเอกสารเพิ่มหนี้ยกมา";
                case _apEnum.Reduction_Dept: return "รายงานเอกสารเพิ่มหนี้ยกมา";
                case _apEnum.Reduction_Dept_Cancel: return "รายงานยกเลิกเอกสารลดหนี้ยกมา";
                //---------------------------------------------------------------------------------------------------
                // รายงานเอกสารอื่นๆๆ
                // MOOOOOOOOOOOOOOOOOM
                //---------------------------------------------------------------------------------------------------
                case _apEnum.Documents_Early_Year_Other: return "รายงานเอกสารอื่นๆ";
                case _apEnum.Documents_Early_Year_Cancel_Other: return "รายงานยกเลิกเอกสารยกมาอื่นๆ";
                case _apEnum.Increase_Debt_Other: return "รายงานเอกสารเพิ่มหนี้อื่นๆ";
                case _apEnum.Increase_Debt_Cancel_Other: return "รายงานยกเลิกเอกสารเพิ่มหนี้อื่นๆ";
                case _apEnum.Reduction_Dept_Other: return "รายงานเอกสารลดหนี้อื่นๆ";
                case _apEnum.Reduction_Dept_Cancel_Other: return "รายงานยกเลิกเอกสารลดหนี้อื่นๆ";
                //---------------------------------------------------------------------------------------------------
                case _apEnum.Movement_Payable: return "รายงานเคลื่อนไหวเจ้าหนี้";
                case _apEnum.Invoice_Arrears_by_Date: return "รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่ ";
                case _apEnum.Invoice_Due_by_Date: return "รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่";
                case _apEnum.Status_Payable: return "รายงานสถานะเจ้าหนี้";
                case _apEnum.Invoice_Overdue_by_Date: return "รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่";
                case _apEnum.Billing_Value_by_Invoice: return "รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ  ";
                case _apEnum.Billing_Outstanding: return "รายงานใบรับวางบิลค่าสินค้าที่คงค้าง";
                case _apEnum.Payment_Detail: return "รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย ";
                case _apEnum.Invoice_Arrears_Due: return "รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด  ";
                case _apEnum.Payment_by_Invoice: return "รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ";
                case _apEnum.Payment_by_Date: return "รายงานการจ่ายเงินประจำวัน-ตามวันที่";
                case _apEnum.Payment_by_Department: return "รายงานการจ่ายเงินประจำวัน-ตามแผนก";
                case _apEnum.Debt_Cut_Detail: return "รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย";
                case _apEnum.Debt_Cut_by_Date: return "รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่";
                case _apEnum.Debt_Cut_by_Payable: return "รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้";
                case _apEnum.Payable_by_Currency: return "รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  ";
                case _apEnum.Payable_Ageing: return "รายงานอายุเจ้าหนี้";
            }
            return "";
        }

        private int _apFlag(_apEnum TransControlType)
        {
            switch (TransControlType)
            {
                //(เจ้าหนี้)   
                //----------------------------------------------------------------------------------
                // ยกมา
                //----------------------------------------------------------------------------------
                // 1 = ตั้งหนี้
                case _apEnum.Documents_Early_Year: return 1;
                // 2 = ยกเลิกตั้งหนี้
                case _apEnum.Documents_Early_Year_Cancel: return 2;
                // 3 = เพิ่มหนี้
                case _apEnum.Increase_Debt: return 3;
                // 4 = ยกเลิกเพิ่มหนี้
                case _apEnum.Increase_Debt_Cancel: return 4;
                // 5 = บันทึกลดหนี้ยกมา
                case _apEnum.Reduction_Dept: return 5;
                // 6 = ยกเลิกบันทึกลดหนี้ยกมา
                case _apEnum.Reduction_Dept_Cancel: return 6;
                //----------------------------------------------------------------------------------
                // อื่นๆๆๆ
                //----------------------------------------------------------------------------------
                // 7 = ตั้งหนี้
                case _apEnum.Documents_Early_Year_Other: return 7;
                // 8 = ยกเลิกตั้งหนี้
                case _apEnum.Documents_Early_Year_Cancel_Other: return 8;
                // 9 = เพิ่มหนี้
                case _apEnum.Increase_Debt_Other: return 9;
                // 10 = ยกเลิกเพิ่มหนี้
                case _apEnum.Increase_Debt_Cancel_Other: return 10;
                // 11 = บันทึกลดหนี้ยกมา
                case _apEnum.Reduction_Dept_Other: return 11;
                // 12 = ยกเลิกบันทึกลดหนี้ยกมา
                case _apEnum.Reduction_Dept_Cancel_Other: return 12;
                //----------------------------------------------------------------------------------
                // APPayBill : 13 = รับวางบิล
                /*case _apEnum.APPayBill: return 13;
                // APCancelPayBill : 14 = ยกเลิกรับวางบิล
                case _apEnum.APCancelPayBill: return 14;
                //APDebtBillingTemp 15 = เตรียมจ่าย
                case _apEnum.APDebtBillingTemp: return 15;
                //APCancelDebtBillingTemp 16 = ยกเลิกเตรียมจ่าย
                case _apEnum.APCancelDebtBillingTemp: return 16;
                //APApprovePayBill 17=อนุมัติเตรียมจ่าย
                case _apEnum.APApprovePayBill: return 17;
                //APCancelApprovePayBill 18=ยกเลิกอนุมัติเตรียมจ่าย
                case _apEnum.APCancelApprovePayBill: return 18;
                // APDebtBilling : 19 = จ่ายชำระหนี้
                case _apEnum.APDebtBilling: return 19;
                // APCancelDebtBilling : 20 = ยกเลิกจ่ายชำระหนี้
                case _apEnum.APCancelDebtBilling: return 20;
                // APDebtBillingCut : 21 = ตัดหนี้สูญ
                case _apEnum.APDebtBillingCut: return 21;
                // APCancelDebtBillingCut : 22 = ยกเลิกตัดหนี้สูญ
                case _apEnum.APCancelDebtBillingCut: return 22;
                //----------------------------------------------------------------------------------
                // ลูกหนี้
                //----------------------------------------------------------------------------------
                // ยกมา
                //----------------------------------------------------------------------------------
                // ARDebtBalance : 23 = ตั้งหนี้
                case _apEnum.ARDebtBalance: return 23;
                // ARCancelDebtBalance : 24 = ยกเลิกตั้งหนี้
                case _apEnum.ARCancelDebtBalance: return 24;
                // APIncreaseDebt : 25 = เพิ่มหนี้
                case _apEnum.ARIncreaseDebt: return 25;
                // APCancelIncreaseDebt : 26 = ยกเลิกเพิ่มหนี้
                case _apEnum.ARCancelIncreaseDebt: return 26;
                // ARCNBalance : 27 = บันทึกลดหนี้ยกมา
                case _apEnum.ARCNBalance: return 27;
                // ARCancelCNBalance : 28 = ยกเลิกบันทึกลดหนี้ยกมา
                case _apEnum.ARCancelCNBalance: return 28;
                //----------------------------------------------------------------------------------
                // อื่นๆๆๆ
                //----------------------------------------------------------------------------------
                // ARDebtBalanceOther : 29 = ตั้งหนี้
                case _apEnum.ARDebtBalanceOther: return 29;
                // ARCancelDebtBalanceOther : 20 = ยกเลิกตั้งหนี้
                case _apEnum.ARCancelDebtBalanceOther: return 30;
                // APIncreaseDebtOther : 31 = เพิ่มหนี้
                case _apEnum.ARIncreaseDebtOther: return 31;
                // APCancelIncreaseDebtOther : 32 = ยกเลิกเพิ่มหนี้
                case _apEnum.ARCancelIncreaseDebtOther: return 32;
                // ARCNBalanceOther : 33 = บันทึกลดหนี้ยกมา
                case _apEnum.ARCNBalanceOther: return 33;
                // ARCancelCNBalanceOther : 34 = ยกเลิกบันทึกลดหนี้ยกมา
                case _apEnum.ARCancelCNBalanceOther: return 34;
                //-----------------------------------------------------------------------------------
                // ARCancelPayBill : 35 = วางบิล
                case _apEnum.ARCancelPayBill: return 35;
                // ARPayBill : 36 = ยกเลิกวางบิล
                case _apEnum.ARPayBill: return 36;
                // ARDebtBillingTemp 37 = ออกใบเสร็จชั่วคราว
                case _apEnum.ARDebtBillingTemp: return 37;
                // ARCancelDebtBillingTemp 38 = ยกเลิกออกใบเสร็จชั่วคราว
                case _apEnum.ARCancelDebtBillingTemp: return 38;
                // ARDebtBilling : 39 = รับชำระหนี้
                case _apEnum.ARDebtBilling: return 39;
                // ARCancelDebtBilling : 40 = ยกเลิกรับชำระหนี้
                case _apEnum.ARCancelDebtBilling: return 40;
                // ARDebtBillingCut : 41 = ตัดหนี้สูญ
                case _apEnum.ARDebtBillingCut: return 41;
                // ARCancelDebtBillingCut : 42 = ยกเลิกตัดหนี้สูญ
                case _apEnum.ARCancelDebtBillingCut: return 42;*/
            }
            return 0;
        }
    }
}
