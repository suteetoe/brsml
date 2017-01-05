using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
namespace SMLERPReport.so
{
    public partial class _report_so : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        //_conditonReceipt _conditonReceiptScreen = new _conditonReceipt();
        //------------------------
        private ArrayList _so_width = new ArrayList();
        private ArrayList _so_column = new ArrayList();
        private ArrayList _so_width_2 = new ArrayList();
        private ArrayList _so_column_2 = new ArrayList();
        private ArrayList _so_width_3 = new ArrayList();
        private ArrayList _so_column_3 = new ArrayList();
        private ArrayList _so_width_4 = new ArrayList();
        private ArrayList _so_column_4 = new ArrayList();
        private string _report_name = "";
        private int _so_level = 0;
        StringBuilder __query;
        string _formatCountNumber = MyLib._myGlobal._getFormatNumber("m00");
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string __data_condition = "";
        Boolean __check_submit = false;
        //------------------------
        SMLReport._report._objectListType _objReport;
        SMLReport._report._objectListType _objReport2;
        SMLReport._report._objectListType _objReport3;
        SMLReport._report._objectListType _objReport4;

        DataSet _ds_main;
        DataSet _ds_detail;
        DataTable _dt;

        private _soEnum _soType;

        public _soEnum SoType
        {
            get { return _soType; }
            set { _soType = value; }
        }



        public _report_so()
        {
            InitializeComponent();
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            _view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            //_view1._loadDataStream += new SMLReport._report.LoadDataStreamEventHandle(_view1__loadDataStream);
            //_view1._buildReport(SMLReport._report._reportType.Standard);
            //_view1._pageSetupDialog.PageSettings.Landscape = true;         
            //_showCondition();
        }

        /*void _view1__loadDataStream()
        {
            //Quotation_Deatail,//001-รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย
            if (SoType == _soEnum.Quotation_Deatail)
            {
                StringBuilder __queryMain = new StringBuilder();
                StringBuilder __queryDetail = new StringBuilder();
                try
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _show_number_of_quote_by_date = "";
                    string _where_main = "";
                    string _where_detail = "";
                    string _orderby_main = "";
                    string _orderby_detail = "";

                    if (!_from_date.Equals("") && !_to_date.Equals(""))
                    {
                        _where_main += " and " + _g.d.ic_trans._doc_date + " between \'" + _from_date + "\' and \'" + _to_date + "\'";
                    }
                    if (!_from_docno.Equals("") && !_to_docno.Equals(""))
                    {
                        _where_main += " and " + _g.d.ic_trans._doc_no + " between \'" + _from_docno + "\' and \'" + _to_docno + "\'";
                    }
                    if (!_from_ar.Equals("") && !_to_ar.Equals(""))
                    {
                        _where_main += " and " + _g.d.ic_trans._cust_code + " between \'" + _from_ar + "\' and \'" + _to_ar + "\'";
                    }
                    if (!_from__item_code.Equals("") && !_to_item_code.Equals(""))
                    {
                        _where_detail += " and " + _g.d.ic_trans_detail._item_code + " between \'" + _from__item_code + "\' and \'" + _to_item_code + "\'";
                    }

                    __queryMain.Append("select "+ _g.d.ic_trans._doc_no+","+_g.d.ic_trans._doc_date+","+_g.d.ic_trans._due_date+","+_g.d.ic_trans._total_vat_value+","+_g.d.ic_trans._total_after_vat+","+_g.d.ic_trans._total_except_vat+","+_g.d.ic_trans._total_amount);
                    __queryMain.Append(", ( select " + _g.d.ar_customer._code + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._cust_code + ") as ap_code");
                    __queryMain.Append(", ( select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ic_trans._cust_code + ") as ap_code");
                    __queryMain.Append(" from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_type + " = 2 and " + _g.d.ic_trans._trans_flag + "  in ( 22 ) " +_where_main );
                    this._ds_main = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryMain.ToString(), "Quotation_Deatail");
                    
                     __queryDetail.Append("select "+_g.d.ic_trans_detail._item_code+","+_g.d.ic_trans_detail._item_name+","+_g.d.ic_trans_detail._unit_code+","+_g.d.ic_trans_detail._price+","+_g.d.ic_trans_detail._qty+","+_g.d.ic_trans_detail._discount+",");
                     __queryDetail.Append(" from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_type + " = 2 and " + _g.d.ic_trans_detail._trans_flag + "  in ( 22 ) " + _where_detail);
                     this._ds_detail = _myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetail.ToString(), "Quotation_Deatail");
                     this._view1._buildReportActive = true;
                }
                catch
                {
                    this._view1._buildReportActive = false;
                }
                this._view1._buildReportActive = true;
            }
        }
        */
        void _view1__loadDataByThread()
        {
            this._dt = new DataTable();
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
            if (this._dt.Rows.Count == 0)
            {
                //Quotation_Deatail,//001-รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย
                if (SoType == _soEnum.Quotation_Deatail)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _show_number_of_quote_by_date = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_docno = __data_split[2];
                            _to_docno = __data_split[3];
                            _from_ar = __data_split[4];
                            _to_ar = __data_split[5];
                            _from__item_code = __data_split[6];
                            _to_item_code = __data_split[7];
                            _show_number_of_quote_by_date = __data_split[8];
                            if (_from_ar.Equals("")) _from_ar = "0";
                            if (_to_ar.Equals("")) _to_ar = "z";
                        }
                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //SaleOrder_Deatail,//002-รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย
                else if (SoType == _soEnum.SaleOrder_Deatail)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    string _show_products_by_name_daily = "";
                    string _show_number_of_quote_by_date = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_docno = __data_split[2];
                            _to_docno = __data_split[3];
                            _from_ar = __data_split[4];
                            _to_ar = __data_split[5];
                            _show_products_by_name_daily = __data_split[6];
                            _show_number_of_quote_by_date = __data_split[7];

                            if (_from_ar.Equals("")) _from_ar = "0";
                            if (_to_ar.Equals("")) _to_ar = "z";
                        }


                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Pay_Oreder,//003-รายงานใบสั่งจ่ายสินค้า
                else if (SoType == _soEnum.Pay_Oreder)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    string _from_number = "";
                    string _to_number = "";
                    string _from_amount = "";
                    string _to_amount = "";
                    string _order_by = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from__item_code = __data_split[2];
                            _to_item_code = __data_split[3];
                            _from_ar = __data_split[4];
                            _to_ar = __data_split[5];
                            _from_number = __data_split[6];
                            _to_number = __data_split[7];
                            _from_amount = __data_split[8];
                            _to_amount = __data_split[9];
                            _order_by = __data_split[10];

                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Deposit_Cut,//004-รายงานการตัดใบรับเงินมัดจำ || //Deposit_Record,//005-รายงานการบันทึกใบรับเงินมัดจำ
                else if (SoType == _soEnum.Deposit_Cut || SoType == _soEnum.Deposit_Record)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_docno = __data_split[2];
                            _to_docno = __data_split[3];
                            _from_ar = __data_split[4];
                            _to_ar = __data_split[5];

                            if (_from_ar.Equals("")) _from_ar = "0";
                            if (_to_ar.Equals("")) _to_ar = "z";
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Cash_Advance_by_Date,//006-รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่
                else if (SoType == _soEnum.Cash_Advance_by_Date)
                {
                    string _print_balance_at_date = "";
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    string _from_payment = "";
                    string _to_payment = "";
                    string _not_total_lift_zero = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _print_balance_at_date = __data_split[0];
                            _from_date = __data_split[1];
                            _to_date = __data_split[2];
                            _from_docno = __data_split[3];
                            _to_docno = __data_split[4];
                            _from_customer_code = __data_split[5];
                            _to_customer_code = __data_split[6];
                            _from_payment = __data_split[7];
                            _to_payment = __data_split[8];
                            _not_total_lift_zero = __data_split[9];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Invoice_Detail,//007-รายงานรายวันขายสินค้าและบริการ
                else if (SoType == _soEnum.Invoice_Detail)
                {
                    
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    string _from_department = "";
                    string _to_department = "";
                    string _category_sale = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_date = __data_split[0];
                             _to_date = __data_split[1];
                             _from_docno = __data_split[2];
                             _to_docno = __data_split[3];
                             _from_ar = __data_split[4];
                             _to_ar = __data_split[5];
                             _from_department = __data_split[6];
                             _to_department = __data_split[7];
                             _category_sale = __data_split[8];

                             if (_from_ar.Equals("")) _from_ar = "0";
                             if (_to_ar.Equals("")) _to_ar = "z";
                        }


                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Tax_Invoice_Detail,//008-รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย
                else if (SoType == _soEnum.Tax_Invoice_Detail)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_license_sale = "";
                    string _to_license_sale = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _category_sale = "";
                    string _display_serial = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_license_sale = __data_split[2];
                            _to_license_sale = __data_split[3];
                            _from_ar = __data_split[4];
                            _to_ar = __data_split[5];
                            _from__item_code = __data_split[6];
                            _to_item_code = __data_split[7];
                            _category_sale = __data_split[8];
                            _display_serial = __data_split[9];

                            if (_from_ar.Equals("")) _from_ar = "0";
                            if (_to_ar.Equals("")) _to_ar = "z";

                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Delivery_Money_by_Date,//009-รายงานการนำส่งเงินประจำวัน
                else if (SoType == _soEnum.Delivery_Money_by_Date)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_amount = "";
                    string _to_amount = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_customer_code = __data_split[2];
                            _to_customer_code = __data_split[3];
                            _from_docno = __data_split[4];
                            _to_docno = __data_split[5];
                            _from_amount = __data_split[6];
                            _to_amount = __data_split[7];
                        }    

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Explain_Item_Category_Sales,//010-รายงานการขายสินค้าแจกแจงตามประเภทการขาย
                else if (SoType == _soEnum.Explain_Item_Category_Sales)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    string _from_payment = "";
                    string _to_payment = "";
                    string _category_sale = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_customer_code = __data_split[4];
                            _to_customer_code = __data_split[5];
                            _from_payment = __data_split[2];
                            _to_payment = __data_split[3];
                            _category_sale = __data_split[6];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Explain_Invoice_by_Product,//011-รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า
                else if (SoType == _soEnum.Explain_Invoice_by_Product)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[2];
                            _to_date = __data_split[3];
                            _from_customer_code = __data_split[6];
                            _to_customer_code = __data_split[7];
                            _from__item_code = __data_split[0];
                            _to_item_code = __data_split[1];
                            _from_docno = __data_split[4];
                            _to_docno = __data_split[5];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Analysis_Sell_Ex_by_Product,//012-รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า
                else if (SoType == _soEnum.Analysis_Sell_Ex_by_Product)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_amount = "";
                    string _to_amount = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_number = "";
                    string _to_number = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[2];
                            _to_date = __data_split[3];
                            _from_amount = __data_split[6];
                            _to_amount = __data_split[7];
                            _from__item_code = __data_split[0];
                            _to_item_code = __data_split[1];
                            _from_number = __data_split[4];
                            _to_number = __data_split[5];
                        }


                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Analysis_Sell_Sum_by_DocNo,//013-รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร
                else if (SoType == _soEnum.Analysis_Sell_Sum_by_DocNo)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    string _from_allocation_code = "";
                    string _to_allocation_code = "";
                    string _from_customer_group = "";
                    string _to_customer_group = "";
                    string _from_dept_group = "";
                    string _to_dept_group = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_docno = __data_split[2];
                            _to_docno = __data_split[3];
                            _from_customer_code = __data_split[4];
                            _to_customer_code = __data_split[5];
                            _from_allocation_code = __data_split[6];
                            _to_allocation_code = __data_split[7];
                            _from_customer_group = __data_split[8];
                            _to_customer_group = __data_split[9];
                            _from_dept_group = __data_split[10];
                            _to_dept_group = __data_split[11];
                        }



                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Sell_by_Sale,//014-รายงานสรุปการขายสินค้าตามพนักงานขาย
                else if (SoType == _soEnum.Sell_by_Sale)
                {

                }
                //Sale_Billing,//015-รายงานการเก็บเงินของพนักงานขาย
                else if (SoType == _soEnum.Sale_Billing)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_sale_person = "";
                    string _to_sale_person = "";
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[2];
                            _to_date = __data_split[3];
                            _from_sale_person = __data_split[0];
                            _to_sale_person = __data_split[1];
                            _from_customer_code = __data_split[4];
                            _to_customer_code = __data_split[5];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Debt_Ar_Detail,//016-รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย
                else if (SoType == _soEnum.Debt_Ar_Detail)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_docno = __data_split[2];
                            _to_docno = __data_split[3];
                            _from_ar = __data_split[4];
                            _to_ar = __data_split[5];

                            if (_from_ar.Equals("")) _from_ar = "0";
                            if (_to_ar.Equals("")) _to_ar = "z";
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Check_Beck_Debt_by_Status,//017-รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ
                else if (SoType == _soEnum.Check_Beck_Debt_by_Status)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_check_product_returns_debt = "";
                    string _to_check_product_returns_debt = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from_check_product_returns_debt = __data_split[2];
                            _to_check_product_returns_debt = __data_split[3];
                            _from_ar = __data_split[4];
                            _to_ar = __data_split[5];

                            if (_from_ar.Equals("")) _from_ar = "0";
                            if (_to_ar.Equals("")) _to_ar = "z";
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Shipments_by_Date,//018-รายงานสรุปยอดขายสินค้า-สรุปตามวันที่
                else if (SoType == _soEnum.Shipments_by_Date)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_number = "";
                    string _to_number = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_date = __data_split[0];
                            _to_date = __data_split[1];
                            _from__item_code = __data_split[2];
                            _to_item_code = __data_split[3];
                            _from_number = __data_split[4];
                            _to_number = __data_split[5];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Shipments_Compare_Month,//019-รายงานเปรียบเที่ยบยอดสินค้า12เดือน
                else if (SoType == _soEnum.Shipments_Compare_Month)
                {
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    string _from_group = "";
                    string _to_group = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_month = "";
                    string _to_month = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _from_customer_code = __data_split[0];
                            _to_customer_code = __data_split[1];
                            _from_group = __data_split[2];
                            _to_group = __data_split[3];
                            _from__item_code = __data_split[4];
                            _to_item_code = __data_split[5];
                            _from_month = __data_split[6];
                            _to_month = __data_split[7];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Sale_History_Product,//020-รายงานประวัติการขายสินค้า
                else if (SoType == _soEnum.Sale_History_Product)
                {
                    string _order_by_customer = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_customer_code = "";
                    string _to_customer_code = "";
                    string _from_date = "";
                    string _to_date = "";
                    string _from_group = "";
                    string _to_group = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_type = "";
                    string _to_type = "";
                    string _from_brand = "";
                    string _to_brand = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                            _order_by_customer = __data_split[0];
                             _from__item_code = __data_split[1];
                             _to_item_code = __data_split[2];
                             _from_customer_code = __data_split[3];
                             _to_customer_code = __data_split[4];
                             _from_date = __data_split[5];
                             _to_date = __data_split[6];
                             _from_group = __data_split[9];
                             _to_group = __data_split[10];
                             _from_docno = __data_split[7];
                             _to_docno = __data_split[8];
                             _from_type = __data_split[11];
                             _to_type = __data_split[12];
                             _from_brand = __data_split[13];
                             _to_brand = __data_split[14];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Product_Rank,//021-รายงานจัดอันดับยอด-สินค้า
                else if (SoType == _soEnum.Product_Rank)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_unit = "";
                    string _to_unit = "";
                    string _category_sale = "";
                    string _to_group = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_date = __data_split[0];
                             _to_date = __data_split[1];
                             _from__item_code = __data_split[2];
                             _to_item_code = __data_split[3];
                             _from_unit = __data_split[4];
                             _to_unit = __data_split[5];
                             _category_sale = __data_split[6];
                             _to_group = __data_split[7];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Margin_reacceptance,//022-รายงานกำไรขั้นต้นแสดงหักรับคืน
                else if (SoType == _soEnum.Margin_reacceptance)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from__item_code = "";
                    string _to_item_code = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_number = "";
                    string _to_number = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_date = __data_split[0];
                             _to_date = __data_split[1];
                             _from__item_code = __data_split[2];
                             _to_item_code = __data_split[3];
                             _from_docno = __data_split[4];
                             _to_docno = __data_split[5];
                             _from_number = __data_split[6];
                             _to_number = __data_split[7];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }
                }
                //Income_Cost_Matching,//023-Income & Cost matching report
                else if (SoType == _soEnum.Income_Cost_Matching)
                {
                    string _from_sale_date = "";
                    string _to_sale_date = "";
                    string _from_date_of_purchase = "";
                    string _to_date_of_purchase = "";
                    string _from_license_sale = "";
                    string _to_license_sale = "";
                    string _from_card_purchase = "";
                    string _to_card_purchase = "";
                    string _from_booking_id = "";
                    string _to_booking_id = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_sale_date = __data_split[0];
                             _to_sale_date = __data_split[1];
                             _from_date_of_purchase = __data_split[2];
                             _to_date_of_purchase = __data_split[3];
                             _from_license_sale = __data_split[4];
                             _to_license_sale = __data_split[5];
                             _from_card_purchase = __data_split[6];
                             _to_card_purchase = __data_split[7];
                             _from_booking_id = __data_split[8];
                             _to_booking_id = __data_split[9];
                        }

                    }
                    catch
                    {
                        this._view1._loadDataByThreadSuccess = false;
                    }

                }
                //Invoice_Tax_Cut//024-รายงานการตัดใบส่งของ/ใบกำกับภาษี
                else if (SoType == _soEnum.Invoice_Tax_Cut)
                {
                    string _from_date = "";
                    string _to_date = "";
                    string _from_docno = "";
                    string _to_docno = "";
                    string _from_ar = "";
                    string _to_ar = "";
                    try
                    {
                        if (this.__check_submit)
                        {
                            string[] __data_split = this.__data_condition.Replace("\'", "").Split(',');
                             _from_date = __data_split[0];
                             _to_date = __data_split[1];
                             _from_docno = __data_split[2];
                             _to_docno = __data_split[3];
                             _from_ar = __data_split[4];
                             _to_ar = __data_split[5];

                             if (_from_ar.Equals("")) _from_ar = "0";
                             if (_to_ar.Equals("")) _to_ar = "z";
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
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)_objReport._columnList[0];
            DataRow[] __dr = this._ds_main.Tables[0].Select();
            Font __newFont = new Font(__getColumn._fontData, FontStyle.Regular);
            int _no = 0;

            /*int _rowprogreesbar = 1;
            if (__dr.Length > 0)
            {
                _rowprogreesbar = (100 / __dr.Length);
            }*/
            //int loopprogressbar = 0;

            //Quotation_Deatail//001-รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย
            if (SoType == _soEnum.Quotation_Deatail)
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
            //SaleOrder_Deatail//002-รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย
            else if (SoType == _soEnum.SaleOrder_Deatail)
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
            //Pay_Oreder//003-รายงานใบสั่งจ่ายสินค้า
            else if (SoType == _soEnum.Pay_Oreder)
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
                            _view1._addDataColumn(_objReport2, __dataObject2, 5, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 6, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 7, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 8, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 9, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 10, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 11, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 12, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 13, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
            // Deposit_Cut//004-รายงานการตัดใบรับเงินมัดจำ
            else if (SoType == _soEnum.Deposit_Cut)
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
            //Deposit_Record//005-รายงานการบันทึกใบรับเงินมัดจำ
            else if (SoType == _soEnum.Deposit_Record)
            {
                for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 12, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 13, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 14, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    
                    }
                    _no++;
                    //_view1._reportProgressBar.Value = loopprogressbar;
                
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
            //Cash_Advance_by_Date//006-รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่
            else if (SoType == _soEnum.Cash_Advance_by_Date)
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
                            _view1._addDataColumn(_objReport2, __dataObject2, 5, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 6, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 7, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 8, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            _view1._addDataColumn(_objReport2, __dataObject2, 9, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                            

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
            //Invoice_Detail//007-รายงานรายวันขายสินค้าและบริการ
            else if (SoType == _soEnum.Invoice_Detail)
            {
                for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 12, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 13, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 14, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            //Tax_Invoice_Detail_//008-รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย
            else if (SoType == _soEnum.Tax_Invoice_Detail)
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
                    _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 12, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
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
            //Delivery_Money_by_Date//009-รายงานการนำส่งเงินประจำวัน
            else if (SoType == _soEnum.Delivery_Money_by_Date)
            {
                for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 10, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 11, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 12, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 13, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 14, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            // Explain_Item_Category Sales//010-รายงานการขายสินค้าแจกแจงตามประเภทการขาย
            else if (SoType == _soEnum.Explain_Item_Category_Sales)
            {
                for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                   
                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            //Explain_Invoice_by_Product//011-รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า
            else if (SoType == _soEnum.Explain_Invoice_by_Product)
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
            //Analysis_Sell_Ex_by_Product//012-รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า
            else if (SoType == _soEnum.Analysis_Sell_Ex_by_Product)
            {
                 for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                   
                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            // Analysis_Sell_Sum_by_DocNo//013-รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร
            else if (SoType == _soEnum.Analysis_Sell_Sum_by_DocNo)
            {
                for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 9, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            //Sell_by_Sale//014-รายงานสรุปการขายสินค้าตามพนักงานขาย
            else if (SoType == _soEnum.Sell_by_Sale)
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
            //Sale_Billing//015-รายงานการเก็บเงินของพนักงานขาย
            else if (SoType == _soEnum.Sale_Billing)
            {
              
            }
            //Debt_Ar_Detail//016-รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย
            else if (SoType == _soEnum.Debt_Ar_Detail)
            {

            }
            //Check_Beck_Debt_by_Status//017-รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ
            else if (SoType == _soEnum.Check_Beck_Debt_by_Status)
            {
                for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 8, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    
                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            //Shipments_by_Date//018-รายงานสรุปยอดขายสินค้า-สรุปตามวันที่
            else if (SoType == _soEnum.Shipments_by_Date)
            {
              for (int _i = 0; _i < __dr.Length; _i++)
                {
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

                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            //Shipments_Compare_Month//019-รายงานเปรียบเที่ยบยอดสินค้า12เดือน
            else if (SoType == _soEnum.Shipments_Compare_Month)
            {

            }
            //Sale_History_Product//020-รายงานประวัติการขายสินค้า
            else if (SoType == _soEnum.Sale_History_Product)
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
             //Product_Rank//021-รายงานจัดอันดับยอด-สินค้า
            else if (SoType == _soEnum.Product_Rank)
            {
                for (int _i = 0; _i < __dr.Length; _i++)
                {
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
                    _view1._addDataColumn(_objReport, __dataObject, 6, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);
                    _view1._addDataColumn(_objReport, __dataObject, 7, __dr[_i][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);


                }
                _no++;
                //_view1._reportProgressBar.Value = loopprogressbar;

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
            //Margin_reacceptance//022-รายงานกำไรขั้นต้นแสดงหักรับคืน
            else if (SoType == _soEnum.Margin_reacceptance)
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
                            _view1._addDataColumn(_objReport2, __dataObject2, 13, __dr[_j][""].ToString(), __newFont, SMLReport._report._cellAlign.Left, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String);

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
            //Income_Cost_Matching//023-Income & Cost matching report
            else if (SoType == _soEnum.Income_Cost_Matching)
            {

            }
            //Invoice_Tax_Cut//024-รายงานการตัดใบส่งของ/ใบกำกับภาษี
            else if (SoType == _soEnum.Invoice_Tax_Cut)
            {

            }
            //---------END---------------//---------END---------------
        }
        
        public void _so_config()
        {
            //Quotation_Deatail//001-รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย
            this.__query = new StringBuilder();
            if (SoType == _soEnum.Quotation_Deatail)
            {
               
                this._report_name = "รายงานการบันทึกใบเสนอราคาพร้อมรายการย้อย";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "วันที่ครบกำหนด", "รหัสลูกค้า", "ชื่อลูกหนี้", "ยอดภาษี", "มูลค่ารวมภาษี", "ยอดยกเว้นภาษี", "มูลค่าสุทธิทั้งบิล" };

                string[] __width_2 = { "13", "12", "17", "10", "10", "10", "10", "10", "10" };
                string[] __column_2 = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "ราคาขาย/หน่วย", "ปริมาณ", "ยอดรวมสินค้า", "ส่วนลด", "จำนวนเงิน", "ยอดสุทธิ" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
              
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
               

            }
            //SaleOrder_Deatail//002-รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย
            else if (SoType == _soEnum.SaleOrder_Deatail)
            {
                this._report_name = "รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "วันที่ครบกำหนด", "รหัสลูกค้า", "ชื่อลูกหนี้", "ยอดภาษี", "มูลค่ารวมภาษี", "ยอดยกเว้นภาษี", "มูลค่าสุทธิทั้งบิล" };

                string[] __width_2 = { "13", "12", "17", "10", "10", "10", "10", "10", "10" };
                string[] __column_2 = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "ราคาขาย/หน่วย", "ปริมาณ", "ยอดรวมสินค้า", "ส่วนลด", "จำนวนเงิน", "ยอดสุทธิ" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
             
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
          


            }
            //Pay_Oreder//003-รายงานใบสั่งจ่ายสินค้า
            else if (SoType == _soEnum.Pay_Oreder)
            {
                this._report_name = "รายงานใบสั่งจ่ายสินค้า";
                string[] __width = { "10" };
                string[] __column = { "วันที่เอกสาร" };

                string[] __width_2 = { "13", "12", "17", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column_2 = { "เลขที่เอกสาร", "ชื่อสินค้า", "สถานที่ส่งสินค้า", "ผู้ขนส่ง", "สถานที่เบิก", "รหัสสินค้า", "ชื่อสินค้า", "คลังสินค้า", "พื้นที่เก็บ", "หน่วยนับ", "จำนวน", "ราคาขาย/หน่วย", "ส่วนลด%,บาท", "จำนวนเงิน" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
             
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
                
            }
            // Deposit_Cut//004-รายงานการตัดใบรับเงินมัดจำ
            else if (SoType == _soEnum.Deposit_Cut)
            {
                this._report_name = "รายงานการตัดใบรับเงินมัดจำ";
                string[] __width = { "10", "10", "10", "10", "10" };
                string[] __column = { "เลขที่ใบรับเงินมัดจำ", "วันที่รับ", "รหัสลูกค้า", "ชื่อลูกค้า", "ยอดสุทธิ" };

                string[] __width_2 = { "13", "12", "17", "10", "10", "10" };
                string[] __column_2 = { "มูลค่าก่อนตัด", "เลขที่ใบขายสินค้า", "วันที่ขาย", "คำอธิบาย", "ยอดตัดจ่าย", "ยอดคงเหลือ" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
               
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
             
            }
            //Deposit_Record//005-รายงานการบันทึกใบรับเงินมัดจำ
            else if (SoType == _soEnum.Deposit_Record)
            {
                this._report_name = "รายงานการบันทึกใบรับเงินมัดจำ";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "เลขที่ใบรับเงิน", "วันที่รับ", "เลขที่ใบกำกับ", "รหัสลูกค้า", "เลขที่ใบจอง", "ยอดก่อนภาษี", "ยอดรวมภาษี", "ยอมรวมภาษีหัก ณ ที่จ่าย", "ยอดเงินสุทธิ", "ยอดเงินสด", "ยอดเงินโอน", "ยอดบัตรเครดิต", "เช็คจ่าย", "ยอดเงินรวม" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            
               
            }
            //Cash_Advance_by_Date//006-รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่
            else if (SoType == _soEnum.Cash_Advance_by_Date)
            {
                this._report_name = "รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่";
                string[] __width = { "10"};
                string[] __column = { "วันที่เอกสาร"};

                string[] __width_2 = { "13", "12", "17", "10", "10", "10", "17", "10", "10", "10" };
                string[] __column_2 = { "เลขที่เอกสาร", "วันที่ครบกำหนด", "รหัสลูกค้า", "ชื่อลูกค้า", "วิธีรับชำระ", "หมายเหตุ", "ใบรับเงินล่วงหน้า", "รับคืนเงินล่วงหน้า", "รับชำระหนี้", "ยอดคงเหลือ" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
               
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
                
            }
            //Invoice_Detail//007-รายงานรายวันขายสินค้าและบริการ
            else if (SoType == _soEnum.Invoice_Detail)
            {
                this._report_name = "รายงานรายวันขายสินค้าและบริการ";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "ใบกำกับภาษี", "รหัสลูกค้า", "ชื่อลูกค้า", "มูลค่าสินค้า", "ยอดท้ายบิล", "ยอดเงินมัดจำ", "ยอดก่อนภาษี", "ยอดภาษี", "ราคารวมภาษี", "ตราศูนย์", "เว้นภาษี ณ ที่จ่าย", "ยอดสุทธิ","ยอดคงค้าง" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
                
            }
            //Tax_Invoice_Detail_//008-รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย
            else if (SoType == _soEnum.Tax_Invoice_Detail)
            {
                this._report_name = "รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "เลขที่ใบขายสินค้า", "วันที่ขาย", "รหัสลูกค้า", "ชื่อลูกค้า", "ประเภทภาษี", "ประเภทการขาย", "รวมมูลค่าสินค้า", "ส่วนลดท้ายบิล", "ยอดเงินมัดจำ", "ภาษีมูลค่าเพิ่ม", "มูลค่ารวมภาษี", "มูลค่ายกเว้นภาษี", "มูลค่าสุทธิ" };

                string[] __width_2 = { "13", "12", "17", "10", "10", "10", "17", "10", "10", "10", "10" };
                string[] __column_2 = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "พื้นที่เก็บ", "คลัง", "ราคาขาย/หน่วย", "จำนวน", "ยอดรวมสินค้า", "ส่วยลด", "จำนวนเงิน", "ยอดสุทธิ" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
              
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
                
            }
            //Delivery_Money_by_Date//009-รายงานการนำส่งเงินประจำวัน
            else if (SoType == _soEnum.Delivery_Money_by_Date)
            {
                this._report_name = "รายงานการนำส่งเงินประจำวัน";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "เลขที่อ้างอิง", "ชื่อลูกค้า", "มูลค่าสินค้า", "ส่วนลด", "หักมัดจำ", "จำนวนเงินท้ายบิล", "ยอดรับชำระ", "ยอดคงค้าง", "เงินสด", "เช็ค", "บัตรเครดิต", "Charge","เงินโอน" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
               
            }
           // Explain_Item_Category Sales//010-รายงานการขายสินค้าแจกแจงตามประเภทการขาย
            else if (SoType == _soEnum.Explain_Item_Category_Sales)
            {
                this._report_name = "รายงานการขายสินค้าแจกแจงตามประเภทการขาย";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่ออกเอกสาร", "เลขที่เอกสาร", "รหัสลูกค้า", "ชื่อลูกค้า", "หมายเหตุ", "ขายสด(รับเป็นเงินสด)", "ขายสด(รับอื่น ๆ)", "ขายเชื่อ", "รวม" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
          
            }
            //Explain_Invoice_by_Product//011-รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า
            
            else if (SoType == _soEnum.Explain_Invoice_by_Product)
            {
                
                this._report_name = "รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า";
                string[] __width = { "10", "10" };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า" };

                string[] __width_2 = { "13", "12", "17", "10", "10", "10", "17", "10" };
                string[] __column_2 = { "เลขที่ใบขายสินค้า", "รหัสลูกหนี้", "ชื่อลูกหนี้", "หน่วยนับ", "ราคาขาย/หน่วย", "ปริมาณ", "ยอดรวมสินค้า", "วันที่กำหนดส่ง" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
        
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
                
            }
            //Analysis_Sell_Ex_by_Product//012-รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า
            else if (SoType == _soEnum.Analysis_Sell_Ex_by_Product)
            {
                this._report_name = "รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หมายเหตุ", "หน่วยนับ", "ราคาขาย/หน่วย", "จำนวน", "ยอดรวม", "ส่วนลด", "จำนวนเงิน", "ยอดสุทธิ" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            
            }
         // Analysis_Sell_Sum_by_DocNo//013-รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร
            else if (SoType == _soEnum.Analysis_Sell_Sum_by_DocNo)
            {
                this._report_name = "รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่ขายสินค้า", "เลขที่ใบขายสินค้า", "รหัสลูกค้า", "ชื่อลูกค้า/บริษัท", "มูลค่าสินค้าคิดภาษี", "ส่วนลดท้ายบิล", "ยอดก่อนภาษี", "ยอดภาษี", "ภาษีหัก ณ ที่จ่าย", "มูลค่ารวมสุทธิ","ยอดยกเว้นภาษี" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
              
            }
            //Sell_by_Sale//014-รายงานสรุปการขายสินค้าตามพนักงานขาย
            else if (SoType == _soEnum.Sell_by_Sale)
            {
                this._report_name = "รายงานสรุปการขายสินค้าตามพนักงานขาย";
                string[] __width = { "10", "10" };
                string[] __column = { "รหัสพนักงานขาย", "ชื่อพนักงานขาย" };

                string[] __width_2 = { "13", "12", "17", "10", "10", "10", "17", "10", "17", "10" };
                string[] __column_2 = { "เลขที่ใบขายสินค้า", "รหัสลูกค้า", "ชื่อลูกค้า/บริษัท", "ยอดก่อนภาษี", "ยอดภาษี", "ภาษีหัก ณ ที่จ่าย", "มูลค่ารวมภาษี", "ยกเว้นภาษี", "มูลค่าสุทธิทั้งบิล", "ยอดลดหนี้" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
               
            }
            //Sale_Billing//015-รายงานการเก็บเงินของพนักงานขาย
            else if (SoType == _soEnum.Sale_Billing)
            {
                this._report_name = "รายงานสรุปการขายสินค้าตามพนักงานขาย";
                string[] __width = { "10", "10" };
                string[] __column = { "รหัสพนักงานขาย", "ชื่อพนักงานขาย" };

                string[] __width_2 = { "13", "12" };
                string[] __column_2 = { "รหัสลูกหนี้", "ชื่อลูกหนี้" };

                string[] __width_3 = { "10", "30", "30", "30"};
                string[] __column_3 = { "", "-------------ใบส่งสินค้า-------------", "-------------รับชำระเงิน-------------", "-------------เช็ค/บัตรเครดิต/เงินโอน-------------" };

                string[] __width_4 = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column_4 = { "เลขที่", "วันที่", "วันครบกำหนด", "จำนวนเงิน", "ยอดคูปอง", "วันที่รับชำระ", "CA/CH/CO/CR", "ยอดรับชำระ", "เลขที่", "วันที่ถึงกำหนด","จำนวนวัน" };

                this._so_level = 4;
                //-------------1
                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
               
                //-------------2
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
              
                //-------------3
                for (int i = 0; i < __width_3.Length; i++)
                {
                    this._so_width_3.Add(__width_3[i]);
                    this._so_column_3.Add(__column_3[i]);
                }
               
                //-------------4
                for (int i = 0; i < __width_4.Length; i++)
                {
                    this._so_width_4.Add(__width_4[i]);
                    this._so_column_4.Add(__column_4[i]);
                }
               
            }
            //Debt_Ar_Detail//016-รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย
            else if (SoType == _soEnum.Debt_Ar_Detail)
            {
                this._report_name = "รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่เอกสาร", "เลขที่เอกสาร", "ใบกำกับภาษี", "ชื่อลูกหนี้", "ยอดใบกำกับเดิม", "มูลค่าสินค้า", "มูลค่าส่วนลดก่อนภาษี", "ยอดภาษี", "มูลค่ารวมภาษี", "ยอดยกเว้นภาษี", "มูลค่าเพิ่มหนี้", "มูลค่าที่ถูกต้อง" };

                string[] __width_2 = { "13", "12", "13", "12", "13", "12", "13", "12" };
                string[] __column_2 = { "เลขที่ใบส่งของ", "ใบกำกับภาษี", "ส่วนลดเดิม", "มูลค่าเดิม", "ยอดรวมสินค้า", "ยอดยกเว้นภาษี", "มูลค่าเพิ่มหนี้", "มูลค่าที่ถูกต้อง" };

                string[] __width_3 = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column_3 = { "รหัสสินค้า", "ชื่อสินค้า", "คลัง", "พื้นที่เก็บ", "หน่วยนับ", "จำนวน", "จำนวนสินค้าที่เพิ่ม", "ราคาขาย", "ส่วนลด", "จำนวนเงิน" };

                this._so_level = 3;
                //-------------1
                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            
                //-------------2
                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }

                //-------------3
                for (int i = 0; i < __width_3.Length; i++)
                {
                    this._so_width_3.Add(__width_3[i]);
                    this._so_column_3.Add(__column_3[i]);
                }

            }
            //Check_Beck_Debt_by_Status//017-รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ
            else if (SoType == _soEnum.Check_Beck_Debt_by_Status)
            {
                this._report_name = "รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่รับคืน/ลดหนี้", "เลขที่ใบรับคืน/ลดหนี้", "รหัสลูกค้า", "ชื่อลูกค้า", "รับคืนเงินสด(รับเป็นเงินสด)", "รับคืนเงินสด(รับอื่นๆ)", "รับคืนเครดิต", "ยอดรวม", "รับคืน(Y)/ลดหนี้(N)" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
             
            }
            //Shipments_by_Date//018-รายงานสรุปยอดขายสินค้า-สรุปตามวันที่
            else if (SoType == _soEnum.Shipments_by_Date)
            {
                this._report_name = "รายงานสรุปยอดขายสินค้า-สรุปตามวันที่";
                string[] __width = { "10", "10", "10", "10", "10", "10" };
                string[] __column = { "วันที่เอกสาร", "มูลค่าสินค้าคิดภาษี", "มูลค่าสินค้ายกเว้นภาษี", "มูลค่าสินค้าภาษีอัตราศูนย์", "ยอดส่วนลด", "ยอดรวม" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            }
            //Shipments_Compare_Month//019-รายงานเปรียบเที่ยบยอดสินค้า12เดือน
            else if (SoType == _soEnum.Shipments_Compare_Month)
            {
                this._report_name = "รายงานเปรียบเที่ยบยอดสินค้า12เดือน";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฏาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม", "ยอดรวม" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            }
            //Sale_History_Product//020-รายงานประวัติการขายสินค้า
            else if (SoType == _soEnum.Sale_History_Product)
            {
                this._report_name = "รายงานประวัติการขายสินค้า";
                string[] __width = { "10", "10" };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า" };

                string[] __width_2 = { "13", "12", "17", "1 0", "10", "10", "17", "10" };
                string[] __column_2 = { "วันที่เอกสาร", "เลขที่เอกสาร", "รหัสลูกค้า", "ชื่อลูกค้า", "ชื่อหน่วยนับ", "ราคาขาย", "จำนวนขาย", "ยอดเงินรวม" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }

                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);
                    this._so_column_2.Add(__column_2[i]);
                }
            }
            //Product_Rank//021-รายงานจัดอันดับยอด-สินค้า
            else if (SoType == _soEnum.Product_Rank)
            {
                this._report_name = "รายงานเปรียบเที่ยบยอดสินค้า12เดือน";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "จำนวน", "มูลค่า", "ต้นทุน", "กำไร(ขาดทุน)", "%" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            }
            //Margin_reacceptance//022-รายงานกำไรขั้นต้นแสดงหักรับคืน
            else if (SoType == _soEnum.Margin_reacceptance)
            {
                this._report_name = "รายงานกำไรขั้นต้นแสดงหักรับคืน";
                string[] __width = { "10", "10" };
                string[] __column = { "วันที่", "เลขที่เอกสาร" };

                string[] __width_2 = { "13", "12", "17", "1 0", "10", "10", "17", "10", "17", "1 0", "10", "10", "17", "10" };
                string[] __column_2 = { "รหัสสินค้า", "ชื่อสินค้า", "หน่วยนับ", "จำนวนขาย", "มูลค่าขาย", "ต้นทุนขาย", "จำนวนรับคืน", "มูลค่ารับคืน", "ต้นทุนรับคืน", "มูลค่าสุทธิ", "ต้นทุนสุทธิ", "กำไร(ขาดทุน)", "%ต่อต้นทุน", "%ต่อยอดสุทธิ" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }

                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);

                    this._so_column_2.Add(__column_2[i]);
                }
            }
            //Income_Cost_Matching//023-Income & Cost matching report
            else if (SoType == _soEnum.Income_Cost_Matching)
            {
                this._report_name = "Income & Cost matching report";
                string[] __width = { "10", "10", "10", "10", "10", "10", "10", "10" };
                string[] __column = { "Date of Received", "Incoming Received", "Booking ID", "Total Received", "Ex-rate", "ต้นทุน", "กำไร(ขาดทุน)", "%" };

                this._so_level = 1;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }
            }
            //Invoice_Tax_Cut//024-รายงานการตัดใบส่งของ/ใบกำกับภาษี
            else if (SoType == _soEnum.Invoice_Tax_Cut)
            {
                this._report_name = "รายงานการตัดใบส่งของ/ใบกำกับภาษี";
                string[] __width = { "10", "10", "10", "10", "10" };
                string[] __column = { "เลขที่ใบขายสินค้า", "วันที่ขาย", "รหัสลูกค้า", "ชื่อลูกค้า", "มูลค่าสุทธิ" };

                string[] __width_2 = { "13", "12", "17", "1 0", "10", "10" };
                string[] __column_2 = { "มูลค่าก่อนตัด", "เลขที่ใบตัด", "วันที่ตัด", "คำอธิบาย", "ยอดตัดจ่าย", "ยอดคงเหลือ" };

                this._so_level = 2;

                for (int i = 0; i < __width.Length; i++)
                {
                    this._so_width.Add(__width[i]);
                    this._so_column.Add(__column[i]);
                }

                for (int i = 0; i < __width_2.Length; i++)
                {
                    this._so_width_2.Add(__width_2[i]);

                    this._so_column_2.Add(__column_2[i]);
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
                    _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                    // _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.Text, SMLReport._report._reportValueDefault._ltdaddress, SMLReport._report._cellAlign.Center, _view1._fontStandard);
                    _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String,this._report_name , SMLReport._report._cellAlign.Center, _view1._fontStandard);
                    _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, " ", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                    _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                    _view1._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                    // _view1._excelFileName = "รายงานยอดการชำระเงิน";//
                    // _view1._maxColumn = 9;
                    return true;
                }
                else
                    if (type == SMLReport._report._objectType.Detail)
                    {
                        _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                        if (this._so_level == 1)
                        {
                            _objReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                        }
                        for (int i = 0; i < this._so_width.Count; i++)
                        {
                            //------------ADD Column----------------
                            _view1._addColumn(_objReport, MyLib._myGlobal._intPhase(this._so_width[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._so_column[i].ToString(), "", SMLReport._report._cellAlign.Left);
                        }
                        //--------------Level 2---------------
                        if (this._so_level == 2 || this._so_level == 3 || this._so_level == 4)
                        {
                            if (this._so_level == 3 || this._so_level == 4)
                            {
                                _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            }
                            else
                            {
                                _objReport2 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                            }
                            
                            for (int i = 0; i < this._so_width_2.Count; i++)
                            {
                                //------------ADD Column----------------
                                _view1._addColumn(_objReport2, MyLib._myGlobal._intPhase(this._so_width_2[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._so_column_2[i].ToString(), "", SMLReport._report._cellAlign.Left);
                            }
                        }
                        //--------------Level 3---------------
                        if (this._so_level == 3 || this._so_level == 4)
                        {
                            if ( this._so_level == 4)
                            {
                                _objReport3 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                            }
                            else
                            {
                                _objReport3 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                            }
                            
                            for (int i = 0; i < this._so_width_3.Count; i++)
                            {
                                //------------ADD Column----------------
                                _view1._addColumn(_objReport3, MyLib._myGlobal._intPhase(this._so_width_3[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._so_column_3[i].ToString(), "", SMLReport._report._cellAlign.Left);
                            }
                        }
                        //--------------Level 4---------------
                        if (this._so_level == 4)
                        {                          
                            _objReport4 = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);

                            for (int i = 0; i < this._so_width_4.Count; i++)
                            {
                                //------------ADD Column----------------
                                _view1._addColumn(_objReport4, MyLib._myGlobal._intPhase(this._so_width_4[i].ToString()), true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", this._so_column_4[i].ToString(), "", SMLReport._report._cellAlign.Left);
                            }
                        }
                        return true;
                    }
 
            return false;
        }

        bool _view1__loadData()
        {
            
            /*if (_ds == null)
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
            }*/
            //this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (_ds_main != null)
            {
                this._so_width.Clear();
                this._so_width_2.Clear();
                this._so_width_3.Clear();
                this._so_width_4.Clear();
                this._so_column.Clear();
                this._so_column_2.Clear();
                this._so_column_3.Clear();
                this._so_column_4.Clear();
                this._ds_main.Clear();
            }
            this._so_config();
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }

        
        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition();
        }
        _condition _con_so;
        void _showCondition()
        {
            string __page = this.SoType.ToString();
            if (this._con_so == null)
            {
                this._con_so = new _condition(__page);
                switch (this.SoType)
                {
                    case _soEnum.Quotation_Deatail:
                        this._con_so._whereControl._tableName = _g.g._search_screen_ic_trans;
                        // this._con_cb._whereControl._addFieldComboBox();
                        break;
                }
            }
            _con_so.ShowDialog();
            if (_con_so.__check_submit)
            {
                this.__data_condition = _con_so.__where;
                this.__check_submit = _con_so.__check_submit;
                this._so_config();
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
            _ds_main = null;
           
           
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
       
        public enum _soEnum
        {
            /// <summary>
            /// 001-รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย
            /// </summary>
            Quotation_Deatail,//001-รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย
            /// <summary>
            /// 002-รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย
            /// </summary>
            SaleOrder_Deatail,//002-รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย
            /// <summary>
            /// 003-รายงานใบสั่งจ่ายสินค้า
            /// </summary>
            Pay_Oreder,//003-รายงานใบสั่งจ่ายสินค้า
            /// <summary>
            /// 004-รายงานการตัดใบรับเงินมัดจำ
            /// </summary>
            Deposit_Cut,//004-รายงานการตัดใบรับเงินมัดจำ
            /// <summary>
            /// 005-รายงานการบันทึกใบรับเงินมัดจำ
            /// </summary>
            Deposit_Record,//005-รายงานการบันทึกใบรับเงินมัดจำ
            /// <summary>
            /// 006-รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่
            /// </summary>
            Cash_Advance_by_Date,//006-รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่
            /// <summary>
            /// 007-รายงานรายวันขายสินค้าและบริการ
            /// </summary>
            Invoice_Detail,//007-รายงานรายวันขายสินค้าและบริการ
            /// <summary>
            /// 008-รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย
            /// </summary>
            Tax_Invoice_Detail,//008-รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย
            /// <summary>
            /// 009-รายงานการนำส่งเงินประจำวัน
            /// </summary>
            Delivery_Money_by_Date,//009-รายงานการนำส่งเงินประจำวัน
            /// <summary>
            /// 010-รายงานการขายสินค้าแจกแจงตามประเภทการขาย
            /// </summary>
            Explain_Item_Category_Sales,//010-รายงานการขายสินค้าแจกแจงตามประเภทการขาย
            /// <summary>
            /// 011-รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า
            /// </summary>
            Explain_Invoice_by_Product,//011-รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า
            /// <summary>
            /// 012-รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า
            /// </summary>
            Analysis_Sell_Ex_by_Product,//012-รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า
            /// <summary>
            /// 013-รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร
            /// </summary>
            Analysis_Sell_Sum_by_DocNo,//013-รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร
            /// <summary>
            /// 014-รายงานสรุปการขายสินค้าตามพนักงานขาย
            /// </summary>
            Sell_by_Sale,//014-รายงานสรุปการขายสินค้าตามพนักงานขาย
            /// <summary>
            /// 015-รายงานการเก็บเงินของพนักงานขาย
            /// </summary>
            Sale_Billing,//015-รายงานการเก็บเงินของพนักงานขาย
            /// <summary>
            /// 016-รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย
            /// </summary>
            Debt_Ar_Detail,//016-รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย
            /// <summary>
            /// 017-รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ
            /// </summary>
            Check_Beck_Debt_by_Status,//017-รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ
            /// <summary>
            /// 018-รายงานสรุปยอดขายสินค้า-สรุปตามวันที่
            /// </summary>
            Shipments_by_Date,//018-รายงานสรุปยอดขายสินค้า-สรุปตามวันที่
            /// <summary>
            /// 019-รายงานเปรียบเที่ยบยอดสินค้า12เดือน
            /// </summary>
            Shipments_Compare_Month,//019-รายงานเปรียบเที่ยบยอดสินค้า12เดือน
            /// <summary>
            /// 020-รายงานประวัติการขายสินค้า
            /// </summary>
            Sale_History_Product,//020-รายงานประวัติการขายสินค้า
            /// <summary>
            /// 021-รายงานจัดอันดับยอด-สินค้า
            /// </summary>
            Product_Rank,//021-รายงานจัดอันดับยอด-สินค้า
            /// <summary>
            /// 022-รายงานกำไรขั้นต้นแสดงหักรับคืน
            /// </summary>
            Margin_reacceptance,//022-รายงานกำไรขั้นต้นแสดงหักรับคืน
            /// <summary>
            /// 023-Income & Cost matching report
            /// </summary>
            Income_Cost_Matching,//023-Income & Cost matching report
            /// <summary>
            /// 024-รายงานการตัดใบส่งของ/ใบกำกับภาษี
            /// </summary>
            Invoice_Tax_Cut//024-รายงานการตัดใบส่งของ/ใบกำกับภาษี
        }

    }


}
