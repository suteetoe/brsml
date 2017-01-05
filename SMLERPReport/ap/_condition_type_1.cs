using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPReport.ap
{
    public partial class _condition_type_1 : MyLib._myForm
    {
        string __page = "";
        public Boolean __check_submit = false;
        public string __where = "";
        public DataTable __grid_where;
        private int __click_check = 0;

        public _condition_type_1(string __page)
        {
            InitializeComponent();
            this._bt_process.Click -= new EventHandler(_bt_process_Click);
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            this.Load += new EventHandler(_condition_type_1_Load);
            this.__from_name(__page);
            this.__page = __page;
            _condition_ap1._maxColumn = 1;
            _condition_ap1._init(this.__page);
            _screen_grid_ap._setFromToColumn(_g.d.resource_report._from_payable, _g.d.resource_report._to_payable);
        }

        public void __from_name(string __page)
        {
           
                /*001*/
                if(_apEnum.Detail_Payable.ToString().Equals(__page))  this.Text = "รายงานรายละเอียดเจ้าหนี้"; /*this.Height = 200;*/ 
                /*002*/
                if(_apEnum.Documents_Early_Year.ToString().Equals(__page)) this.Text = "รายงานเอกสารยกมาต้นปี"; /*this.Height = 240;*/ 
                /*003*/
                if(_apEnum.Payable_Other.ToString().Equals(__page))  this.Text = "รายงานการตั้งเจ้าหนี้อื่นๆ"; /*this.Height = 280;*/ 
                /*004*/
                if(_apEnum.Movement_Payable.ToString().Equals(__page))  this.Text = "รายงานเคลื่อนไหวเจ้าหนี้"; /*this.Height = 240;*/ 
                /*005*/
                if(_apEnum.Invoice_Arrears_by_Date.ToString().Equals(__page))  this.Text = "รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่"; /*this.Height = 270;*/ 
                /*006*/
                if(_apEnum.Invoice_Due_by_Date.ToString().Equals(__page))  this.Text = "รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่"; /*this.Height = 280;*/
                /*007*/
                if(_apEnum.Status_Payable.ToString().Equals(__page))  this.Text = "รายงานสถานะเจ้าหนี้"; /*this.Height = 260;*/
                /*008*/
                if(_apEnum.Invoice_Overdue_by_Date.ToString().Equals(__page))  this.Text = "รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่ "; /*this.Height = 260;*/ 
                /*009*/
                if(_apEnum.Billing_Value_by_Invoice.ToString().Equals(__page))  this.Text = "รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ"; /*this.Height = 260;*/ 
                /*010*/
                if(_apEnum.Billing_Outstanding.ToString().Equals(__page))  this.Text = "รายงานใบรับวางบิลค่าสินค้าที่คงค้าง"; /*this.Height = 260;*/ 
                /*011*/
                if(_apEnum.Payment_Detail.ToString().Equals(__page))  this.Text = "รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย"; /*this.Height = 260;*/ 
                /*012*/
                if(_apEnum.Invoice_Arrears_Due.ToString().Equals(__page))  this.Text = "รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด"; /*this.Height = 270;*;
                /*013*/
                if(_apEnum.Payment_by_Invoice.ToString().Equals(__page))  this.Text = "รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ "; /*this.Height = 260;*/ 
                /*014*/
                if(_apEnum.Movement_Payable.ToString().Equals(__page))  this.Text = "รายงานเคลื่อนไหวเจ้าหนี้"; /*this.Height = 240;*/ 
                /*015*/
                if(_apEnum.Payment_by_Date.ToString().Equals(__page))  this.Text = "รายงานการจ่ายเงินประจำวัน-ตามวันที่ "; /*this.Height = 270;*/ 
                /*016*/
                if(_apEnum.Payment_by_Department.ToString().Equals(__page))  this.Text = "รายงานการจ่ายเงินประจำวัน-ตามแผนก"; /*this.Height = 270;*/ 
                /*017*/
                if(_apEnum.Debt_Cut_Detail.ToString().Equals(__page))  this.Text = "รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย"; /*this.Height = 240;*/ 
                /*018*/
                if(_apEnum.Debt_Cut_by_Date.ToString().Equals(__page))  this.Text = "รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่"; /*this.Height = 240;*/ 
                /*019*/
                if(_apEnum.Debt_Cut_by_Payable.ToString().Equals(__page))  this.Text = "รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้"; /*this.Height = 240;*/ 
                /*020*/
                if(_apEnum.Payable_by_Currency.ToString().Equals(__page))  this.Text = "รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ"; /*this.Height = 240;*/
                /*021*/
                if (_apEnum.Payable_Ageing.ToString().Equals(__page)) this.Text = "รายงานอายุเจ้าหนี้"; /*this.Height = 270;*/ 
                /*022*/
                if(_apEnum.Increase_Debt.ToString().Equals(__page))  this.Text = "รายงานเอกสารเพิ่มหนี้ยกมา"; /*this.Height = 270;*/
                /*023*/
                if (_apEnum.Reduction_Dept.ToString().Equals(__page)) this.Text = "รายงานเอกสารลดหนี้ยกมา"; /*this.Height = 270;*/
                /*024*/
                //if (_apEnum.Increase_Debt.ToString().Equals(__page)) this.Text = "รายงานเอกสารตั้งหนี้"; /*this.Height = 270;*/
                
            
        }

        void _condition_type_1_Load(object sender, EventArgs e)
        {
            // อันนี้ต้องย้ายไปที่ init (jead)
            /*_condition_ap1._init(this.__page);
            _screen_grid_ap1._setFromToColumn(_g.d.resource_report._from_payable, _g.d.resource_report._to_payable);*/
            this._condition_ap1.AutoSize = true;
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this.__check_submit = false;
            this.Close();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            String __emptyField = this._condition_ap1._checkEmtryField();
            if (__emptyField.Length > 0)
            {
                MessageBox.Show(__emptyField, "กรุณาป้อน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this._process_click();
            this.Close();
        }

        public void _process_click()
        {
            // check condition empty ด้วย

            if (this.__click_check == 0)
            {
                this.__grid_where = null;
            }
            else
            {
                this.__grid_where = _screen_grid_ap._getCondition();
            }
            this.__where = "" + this._condition_ap1._createQueryForDatabase()[1].ToString();
            this.__check_submit = true;
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.Escape == keyData)
            {
                this.Close();
                return true;
            }
            if (Keys.F11 == keyData)
            {
                this._process_click();
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

    public partial class _condition_ap : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        /// <summary>
        /// Search AP
        /// </summary>
        MyLib._searchDataFull _search_masterap_group;
        /// <summary>
        /// Search Doc No
        /// </summary>
        MyLib._searchDataFull _search_doc_no;
        /// <summary>
        /// Search Sale Person
        /// </summary>
        MyLib._searchDataFull _search_sale_person;
        /// <summary>
        /// Search Department
        /// </summary>
        MyLib._searchDataFull _search_department;
        /// <summary>
        /// Search Department
        /// </summary>
        MyLib._searchDataFull _search_invoice;
        /// <summary>
        /// Search Department
        /// </summary>
        MyLib._searchDataFull _search_bill_landing;
        /// <summary>
        /// Search Project
        /// </summary>
        MyLib._searchDataFull _search_project;
        /// <summary>
        /// Search Currency
        /// </summary>
        MyLib._searchDataFull _search_currency;


        public _condition_ap()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_ap__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_ap__textBoxChanged);
        }

        public void _init(string __page)
        {
            this._maxColumn = 2;
            //Detail_Payable,//001-รายงานรายละเอียดเจ้าหนี้ 
            if (__page.Equals(_apEnum.Detail_Payable.ToString()))
            {
                // jead _to_date เปลีายนเป็น _end_date ข้อความ ยอดสิ้นสุด ณ. วันที่
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._balance_date, 1, true, false);
                this._setDataDate(_g.d.resource_report._balance_date, MyLib._myGlobal._workingDate);
                /*this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);*/
                this._search_masterap_group = new MyLib._searchDataFull();
            }
            ///Documents_Early_Year,//002-รายงานเอกสารยกมาต้นปี  //022-รายงานเอกสารเพิ่มหนี้ยกมา	//023-รายงานเอกสารลดหนี้ยกมา	//024-รายงานเอกสารตั้งหนี้
            else if (
                __page.Equals(_apEnum.Documents_Early_Year.ToString()) ||
                __page.Equals(_apEnum.Documents_Early_Year_Cancel.ToString()) ||
                __page.Equals(_apEnum.Increase_Debt.ToString()) ||
                __page.Equals(_apEnum.Increase_Debt_Cancel.ToString()) ||
                __page.Equals(_apEnum.Reduction_Dept.ToString()) ||
                __page.Equals(_apEnum.Reduction_Dept_Cancel.ToString()) ||
                __page.Equals(_apEnum.Documents_Early_Year_Other.ToString()) ||
                __page.Equals(_apEnum.Documents_Early_Year_Cancel_Other.ToString()) ||
                __page.Equals(_apEnum.Increase_Debt_Other.ToString()) ||
                __page.Equals(_apEnum.Increase_Debt_Cancel_Other.ToString()) ||
                __page.Equals(_apEnum.Reduction_Dept_Other.ToString()) ||
                __page.Equals(_apEnum.Reduction_Dept_Cancel_Other.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_date, MyLib._myGlobal._workingDate);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();
            }
            //Movement_Payable,//004-รายงานเคลื่อนไหวเจ้าหนี้     
            else if (__page.Equals(_apEnum.Movement_Payable.ToString()))
            {
                //this._addNumberBox(1, 0, 1, 0, _g.d.resource_report._from_period, 3, 0, true);
                //this._addNumberBox(1, 1, 1, 0, _g.d.resource_report._to_period, 3, 0, true);

                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_period, 1, 3, 1, true, false, true);
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_period, 1, 3, 1, true, false, true);

                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_date, MyLib._myGlobal._workingDate);
                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                
            }
            // Invoice_Arrears_by_Date,//005-รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่ 
            else if (__page.Equals(_apEnum.Invoice_Arrears_by_Date.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._balance_at_date, 1, true, false);

                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_bill, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_bill, 1, true);
                //this._setDataDate(_g.d.resource_report._from_bill, MyLib._myGlobal._workingDate);
                //this._setDataDate(_g.d.resource_report._to_bill, MyLib._myGlobal._workingDate);

                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_due_date, 1, true);
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_due_date, 1, true);
                //this._setDataDate(_g.d.resource_report._from_due_date, MyLib._myGlobal._workingDate);
                //this._setDataDate(_g.d.resource_report._to_due_date, MyLib._myGlobal._workingDate);

                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                //this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();

            }
            // Invoice_Due_by_Date,//006-รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
            else if (__page.Equals(_apEnum.Invoice_Due_by_Date.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_due_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_due_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_due_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_due_date, MyLib._myGlobal._workingDate);

                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_bill, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_bill, 1, true);
                this._setDataDate(_g.d.resource_report._from_bill, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_bill, MyLib._myGlobal._workingDate);

                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_amount, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_amount, 1, 1, 1, true, false, true);
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_credit_day, 1, 1, 1, true, false, true);
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_credit_day, 1, 1, 1, true, false, true);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();
               
            }
            // Status_Payable,//007-รายงานสถานะเจ้าหนี้ 
            else if (__page.Equals(_apEnum.Status_Payable.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._annual_period, 1, 1, 1, true, false, true);
                //this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_date, MyLib._myGlobal._workingDate);

                this._addCheckBox(2, 0, _g.d.resource_report._not_total_lift_zero, false, true);

            }
            // Invoice_Overdue_by_Date,//008-รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่  
            else if (__page.Equals(_apEnum.Invoice_Overdue_by_Date.ToString()))
            {

            }
            //Billing_Value_by_Invoice,//009-รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ  ||  //Billing_Outstanding,//010-รายงานใบรับวางบิลค่าสินค้าที่คงค้าง 
            else if (__page.Equals(_apEnum.Billing_Value_by_Invoice.ToString()) || __page.Equals(_apEnum.Billing_Outstanding.ToString()))
            {
                if (__page.Equals(_apEnum.Billing_Value_by_Invoice.ToString()))
                {
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_invoice, 1, 1, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_invoice, 1, 1, 1, true, false, true);
                    this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true);
                }
                else if (__page.Equals(_apEnum.Billing_Outstanding.ToString()))
                {
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_invoice, 1, 1, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_invoice, 1, 1, 1, true, false, true);
                }

                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_bill_of_landing_place, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_bill_of_landing_place, 1, 1, 1, true, false, true);

                this._setDataDate(_g.d.resource_report._from_invoice_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_invoice_date, MyLib._myGlobal._workingDate);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_invoice = new MyLib._searchDataFull();
                this._search_bill_landing = new MyLib._searchDataFull();
                
            }
            //Payment_Detail,//011-รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย 
            else if (__page.Equals(_apEnum.Payment_Detail.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_date, MyLib._myGlobal._workingDate);

                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addCheckBox(2, 0, _g.d.resource_report._not_total_lift_zero, false, true);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();
                
            }
            //Invoice_Arrears_Due,//012-รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด  
            else if (__page.Equals(_apEnum.Invoice_Arrears_Due.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_invoice_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_invoice_date, MyLib._myGlobal._workingDate);

                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_invoice, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_invoice, 1, 1, 1, true, false, true);

                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_due_date, 1, true);
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_due_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_due_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_due_date, MyLib._myGlobal._workingDate);

                //this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_sale_person, 1, 1, 1, true, false, true);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();
                this._search_sale_person = new MyLib._searchDataFull();
                this._search_invoice = new MyLib._searchDataFull();
            }
            //Payment_by_Invoice,//013-รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ 
            else if (__page.Equals(_apEnum.Payment_by_Invoice.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_invoice_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_invoice_date, MyLib._myGlobal._workingDate);

                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_invoice, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_invoice, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();
                this._search_invoice = new MyLib._searchDataFull();
                
            }
            //Payment_by_Date,//015-รายงานการจ่ายเงินประจำวัน-ตามวันที่  ||  //Payment_by_Department,//016-รายงานการจ่ายเงินประจำวัน-ตามแผนก  
            else if (__page.Equals(_apEnum.Payment_by_Date.ToString()) || __page.Equals(_apEnum.Payment_by_Department.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._setDataDate(_g.d.resource_report._from_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._to_date, MyLib._myGlobal._workingDate);

                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_department, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_department, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_project, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_project, 1, 1, 1, true, false, true);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();
                this._search_project = new MyLib._searchDataFull();
                this._search_department = new MyLib._searchDataFull();
            }
            //Debt_Cut_Detail,//017-รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย  
            //Debt_Cut_by_Date,//018-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่  
            //Debt_Cut_by_Payable,//019-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
            else if (__page.Equals(_apEnum.Debt_Cut_Detail.ToString()) || __page.Equals(_apEnum.Debt_Cut_by_Date.ToString()) || __page.Equals(_apEnum.Debt_Cut_by_Payable.ToString()))
            {
                if (__page.Equals(_apEnum.Debt_Cut_by_Payable.ToString()))
                {
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                    //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                    //this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                }
                else
                {
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                    //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                    //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                }

                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_doc_no = new MyLib._searchDataFull();

            }
            //Payable_by_Currency//020-รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  
            else if (__page.Equals(_apEnum.Payable_by_Currency.ToString()))
            {
                //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true);
                //this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_payable, 1, 1, 1, true, false, true);
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_currency, 1, 1, 1, true, false, true);
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_currency, 1, 1, 1, true, false, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._annual_period, 1, 1, 1, true, false, true);

                this._search_masterap_group = new MyLib._searchDataFull();
                this._search_currency = new MyLib._searchDataFull();
                
            }//"Payable_Ageing": this.Text = "รายงานอายุเจ้าหนี้"; break;
            else if (__page.Equals(_apEnum.Payable_Ageing.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "1");//ช่วงที่ 1 1
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "7");//ถึง 7
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "8");//ช่วงที่ 2 8
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "14");//ถึง 14
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "15");//ช่วงที่ 3 15
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "21");//ถึง  21
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "22");//ช่วงที่ 4 22
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._from_payable, 1, 1, 1, true, false, true, false, false, "28");//ถึง 28
            }
            this.Invalidate();
            this.ResumeLayout();
            // ค้นหาเจ้าหนี้
            if (this._search_masterap_group != null)
            {
                this._search_masterap_group._name = _g.g._search_master_ap_group;
                this._search_masterap_group._dataList._loadViewFormat(this._search_masterap_group._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_masterap_group._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_masterap_group._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_ap__searchEnterKeyPress);
            }
            
            // ค้นหาเอกสาร
            if (this._search_doc_no != null)
            {
                this._search_doc_no._name = _g.g._screen_ap_ar_trans;
                this._search_doc_no._dataList._loadViewFormat(this._search_doc_no._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_doc_no._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_doc_no._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_ap__searchEnterKeyPress);
            }
            // ค้นหารหัสพนักงานขาย
            if (this._search_sale_person != null)
            {
                this._search_sale_person._name = _g.g._search_screen_erp_user;
                this._search_sale_person._dataList._loadViewFormat(this._search_sale_person._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_sale_person._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_sale_person._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_ap__searchEnterKeyPress);
            }
            // ค้นหาแผนก
            if (this._search_department != null)
            {
                this._search_department._name = _g.g._search_screen_erp_department_list;
                this._search_department._dataList._loadViewFormat(this._search_department._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_department._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_department._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_ap__searchEnterKeyPress);
            }
            // ค้นหาเลขที่ใบส่งของ
            if (this._search_invoice != null)
            {
                this._search_invoice._name = _g.g._search_screen_po_so_invoice;
                this._search_invoice._dataList._loadViewFormat(this._search_invoice._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_invoice._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_invoice._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_ap__searchEnterKeyPress);
            }
            // ค้นหาเลขที่ใบรับวางบิล
            //this._search_bill_landing._name = _g.g._se
            // ค้นหาจากโครงการ
            if (this._search_project != null)
            {
                this._search_project._name = _g.g._search_screen_erp_project;
                this._search_project._dataList._loadViewFormat(this._search_project._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_project._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_project._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_ap__searchEnterKeyPress);
            }
            // ค้นหาสกุลเงิน
            if (this._search_currency != null)
            {
                this._search_currency._name = _g.g._search_screen_erp_currency;
                this._search_currency._dataList._loadViewFormat(this._search_currency._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_currency._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_currency._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_ap__searchEnterKeyPress);
            }
             
        }

        //-------------------------------------Search-------------------------------------------------------
        void _search_ap__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }
        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        private void _searchAll(string name, int row)
        {
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            string __result = "";
            if (name.Equals(_g.g._search_master_ap_group))
            {
                __search = _search_masterap_group;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._screen_ap_ar_trans))
            {
                __search = _search_doc_no;
                __result = (string)__search._dataList._gridData._cellGet(row, 1);
            }
            else if (name.Equals(_g.g._search_screen_erp_user))
            {
                __search = _search_sale_person;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_erp_department_list))
            {
                __search = _search_department;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_po_so_invoice))
            {
                __search = _search_invoice;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_erp_project))
            {
                __search = _search_project;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_erp_currency))
            {
                __search = _search_currency;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            //-------------------------------
            if (__result.Length > 0)
            {
                __search.Visible = false;
                this._setDataStr(_searchName, __result, "", false);
                _search(true);
            }

        }
        void _condition_ap__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._ic_name))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                //   _search(true);
            }
            if (name.Equals(_g.d.ic_resource._ic_normal))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                //   _search(true);
            }
        }

        void _condition_ap__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            if (name.Equals(_g.d.resource_report._from_payable) || name.Equals(_g.d.resource_report._to_payable))
            {
                __search = _search_masterap_group;
            }
            if (name.Equals(_g.d.resource_report._from_docno) || name.Equals(_g.d.resource_report._to_docno))
            {
                __search = _search_doc_no;
            }
            if (name.Equals(_g.d.resource_report._from_sale_person) || name.Equals(_g.d.resource_report._to_sale_person))
            {
                __search = _search_sale_person;
            }
            if (name.Equals(_g.d.resource_report._from_department) || name.Equals(_g.d.resource_report._to_department))
            {
                __search = _search_department;
            }
            if (name.Equals(_g.d.resource_report._from_invoice) || name.Equals(_g.d.resource_report._to_invoice))
            {
                __search = _search_invoice;
            }
            if (name.Equals(_g.d.resource_report._from_project) || name.Equals(_g.d.resource_report._to_project))
            {
                __search = _search_project;
            }
            if (name.Equals(_g.d.resource_report._from_currency) || name.Equals(_g.d.resource_report._to_currency))
            {
                __search = _search_currency;
            }
            //------------------------------------
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = name;
            _searchTextBox = __getControl.textBox;
            MyLib._myGlobal._startSearchBox(__getControl, label_name, __search, false);
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        private void _search(bool warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._getDataStr(_g.d.ap_supplier._code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._getDataStr(_g.d.ap_supplier._code) + "\'"));

                __myquery.Append("</node>");
                ArrayList _getData = _myFramework._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.ap_supplier._code, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.ap_supplier._code, (DataSet)_getData[1], warning);
            }
            catch
            {
            }
        }
        private bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.ic_resource._ic_name) || fieldName.Equals(_g.d.ic_resource._ic_normal))
                {
                    this._setDataStr(fieldName, getDataStr);
                    //this._setDataStr(fieldName, getDataStr, getData, true);
                    //  this._setDataStr(_g.d.ic_inventory._name_1, getData);
                }
                else
                {
                    this._setDataStr(fieldName, getDataStr, getData, true);
                }
            }
            else
            {
                this._setDataStr(fieldName, "");
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารเลขที่ หรือ ยอดตัดจ่าย ห้ามว่าง"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

    }
    public partial class _screen_grid_ap : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();

        public _screen_grid_ap()
        {
            this._clickSearchButton += new MyLib.SearchEventHandler(_screen_condition_ap_grid__clickSearchButton);
        }

        public void _setFromToColumn(string __from_column_name, string __to_column_name)
        {
            this._clear();
            this._addColumn(_g.d.resource_report._table + "." + __from_column_name, 1, 1, 50, true, false, false, true);
            this._addColumn(_g.d.resource_report._table + "." + __to_column_name, 1, 1, 50, true, false, false, true);
            this._width_by_persent = true;
            this.AllowDrop = true;
            //this._isEdit = false;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
        }

        void _screen_condition_ap_grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string _searchName = this._cellGet(this._selectRow, this._selectColumn).ToString();
            string _search_text_new = _g.g._search_screen_ap;
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._refreshData();
                this._search_data_full._dataList._loadViewData(0);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, "ค้นหารหัสเจ้าหนี้", this._search_data_full, true);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_ap) == 0)
            {
                this._search_data_full.Close();
                this._cellUpdate(this._selectRow, this._selectColumn, e._text, false);
            }
        }

        public DataTable _getCondition()
        {
            if (this._rowCount(0) == 0) return null;
            DataTable __dataTable = new DataTable("FromTo");
            __dataTable.Columns.Add("from");
            __dataTable.Columns.Add("to");
            for (int __row = 0; __row < this._rowCount(0); __row++)
            {
                DataRow __dataRow = __dataTable.NewRow();
                __dataRow[0] = this._cellGet(__row, 0).ToString();
                __dataRow[1] = this._cellGet(__row, 1).ToString();
                __dataTable.Rows.Add(__dataRow);
            }
            return __dataTable;
        }

    }
    public enum _apEnum
    {
        /// <summary>
        /// 001-รายงานรายละเอียดเจ้าหนี้ 
        /// </summary>
        Detail_Payable,
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
        /// รายงานเอกสารเพิ่มหนี้ยกมา
        /// </summary>
        Reduction_Dept,
        /// <summary>
        /// รายงานยกเลิกเอกสารเพิ่มหนี้ยกมา
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
        /// รายงานเอกสารเพิ่มหนี้อื่นๆ
        /// </summary>
        Reduction_Dept_Other,
        /// <summary>
        /// รายงานยกเลิกเอกสารเพิ่มหนี้อื่นๆ
        /// </summary>
        Reduction_Dept_Cancel_Other,
        //---------------------------------------------------------------------------------------------------
        /// <summary>
        /// 003-รายงานการตั้งเจ้าหนี้อื่นๆ
        /// </summary>
        Payable_Other,//003-รายงานการตั้งเจ้าหนี้อื่นๆ
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

}
