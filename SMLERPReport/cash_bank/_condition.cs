using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPReport.cash_bank
{
    public partial class _condition : MyLib._myForm
    {
        string __page = "";
        public Boolean __check_submit =false;
        public string __where = "";
        public _condition(string __page)
        {
            InitializeComponent();
            this._bt_process.Click+=new EventHandler(_bt_process_Click);
            this._bt_exit.Click +=new EventHandler(_bt_exit_Click);
            this.Load += new EventHandler(_condition_Load);
            this.__from_name(__page);
            this.__page = __page;
            _condition_cash_bank1._init(__page);
        }
        public void __from_name(string __page)
        {
           
                /*001*/
                    if(_cash_bank_Enum.Revenue_by_Bank.ToString().Equals(__page)) this.Text = "รายงานรายได้จากธนาคาร";
                /*002*/
                    if(_cash_bank_Enum.Cash_deposit.ToString().Equals(__page)) this.Text = "รายงานการฝากเงินสด";
                /*003*/
                    if(_cash_bank_Enum.Statement_Advance.ToString().Equals(__page)) this.Text = "รายงาน Statement ล่วงหน้า";               
                /*004*/
                    if(_cash_bank_Enum.Transfer_Money_Between_Banks.ToString().Equals(__page)) this.Text = "รายงานการโอนเงินระหว่างธนาคาร";               
                /*005*/
                    if(_cash_bank_Enum.Bank_Statement.ToString().Equals(__page)) this.Text = "รายงาน Bank Statement";                
                /*006*/
                    if(_cash_bank_Enum.Received_Cash_sub_item.ToString().Equals(__page)) this.Text = "รายงานการรับเงินอื่นๆพร้อมรายการย่อย";                
                /*007*/
                    if(_cash_bank_Enum.Open_sub_Cash.ToString().Equals(__page)) this.Text = "รายงานการตั้งเบิกเงินสดย่อย";
                /*008*/
                    if(_cash_bank_Enum.Cash_movements.ToString().Equals(__page)) this.Text = "รายงานเคลื่อนไหวเงินสด"; 
                /*009*/
                    if(_cash_bank_Enum.Pay_Cash_by_DocDate.ToString().Equals(__page)) this.Text = "รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร"; 
                /*010*/
                    if(_cash_bank_Enum.Monthly_Payment_Book.ToString().Equals(__page)) this.Text = "รายงานสมุดจ่ายเงินประจำเดือน";   
                /*011*/
                    if(_cash_bank_Enum.Book_Bank_Balance.ToString().Equals(__page)) this.Text = "รายงานยอดเงินคงเหลือสมุดฝากธนาคาร";
                /*012*/
                    if(_cash_bank_Enum.Sub_Cash_Movements.ToString().Equals(__page)) this.Text = "รายงานเคลื่อนไหวเงินสดย่อย";
                /*013*/
                    if(_cash_bank_Enum.Summary_Payment.ToString().Equals(__page)) this.Text = "รายงานสรุปการจ่ายเงิน"; 
            
        }
        void _condition_Load(object sender, EventArgs e)
        {           
            this._condition_cash_bank1.AutoSize = true;
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this.__check_submit = false;
            this.Close();            
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            //if (this.__click_check == 0)
            //{
            //    this.__grid_where = null;
            //}
            //else
            //{
            //    this.__grid_where = _screen_grid_ap._getCondition();
            //}
            this.__where = "" + this._condition_cash_bank1._createQueryForDatabase()[1].ToString();
            this.__check_submit = true;
            this.Close();
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

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
    public partial class _condition_cash_bank : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        /// <summary>
        /// Group Search Cash Bank
        /// </summary>
        MyLib._searchDataFull _search_mastercb_group;
        /// <summary>
        /// ค้นหาเลขที่เอกสาร
        /// </summary>
        MyLib._searchDataFull _search_docno;
        /// <summary>
        /// ค้นหาสมุดรายวัน
        /// </summary>
        MyLib._searchDataFull _search_account_book_code;
        /// <summary>
        /// ค้นหาสมุดบัญชีธนาคาร
        /// </summary>
        MyLib._searchDataFull _search_pass_book_code;
        /// <summary>
        /// ค้นหาเลขที่เช็ค
        /// </summary>
        MyLib._searchDataFull _search_number_check;
        /// <summary>
        /// ค้นหาเลขที่บัตรเครดิต
        /// </summary>
        MyLib._searchDataFull _search_number_credit_card;
        /// <summary>
        /// ค้นหาพนักงาน
        /// </summary>
        MyLib._searchDataFull _search_staff_code;        
        /// <summary>
        /// ค้นหารหัสเงินสดย่อย
        /// </summary>
        MyLib._searchDataFull _search_cash_sub_code;
        /// <summary>
        /// ค้นหารหัสผังบัญชี
        /// </summary>
        MyLib._searchDataFull _search_project_ac_code;
        /// <summary>
        /// Search Address Book, 
        /// </summary>
        //MyLib._searchDataFull _search_addr_Code;
        public _condition_cash_bank()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_cash_bank__textBoxSearch);
            this._textBoxChanged+=new MyLib.TextBoxChangedHandler(_condition_cash_bank__textBoxChanged);
        }

       
        public void _init(string __page)
        {
            this._maxColumn = 2;
            //Revenue_by_Bank,//001-รายงานรายได้จากธนาคาร || //Cash_deposit,//002-รายงานการฝากเงินสด
            if (__page.Equals(_cash_bank_Enum.Revenue_by_Bank.ToString()) || __page.Equals(_cash_bank_Enum.Cash_deposit.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_account_book_code, 1, 1, 1, true, false, true);//จากรหัสสมุดบัญชี
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_account_book_code, 1, 1, 1, true, false, true);//ถึงรหัสสมุดบัญชี
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_book_code, 1, 1, 1, true, false, true);//จากรหัสสมุดบัญชี
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_book_code, 1, 1, 1, true, false, true);//ถึงรหัสสมุดบัญชี
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_docno = new MyLib._searchDataFull();
                //_search_account_book_code = new MyLib._searchDataFull();
                _search_pass_book_code = new MyLib._searchDataFull();
            }
            //Statement_Advance,//003-รายงาน Statement ล่วงหน้า
            else if (__page.Equals(_cash_bank_Enum.Statement_Advance.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_due_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_due_date, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_check_number, 1, 1, 1, true, false, true);//จากเลขที่เช็ค
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_check_number, 1, 1, 1, true, false, true);//ถึงเลขที่เช็ค
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_credit_number, 1, 1, 1, true, false, true);//จากเลขที่บัตรเครดิต
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_credit_number, 1, 1, 1, true, false, true);//ถึงเลขที่บัตรเครดิต
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_number_check = new MyLib._searchDataFull();
                _search_number_credit_card = new MyLib._searchDataFull();
                _search_docno = new MyLib._searchDataFull();
          
            }
            //Transfer_Money_Between_Banks,//004-รายงานการโอนเงินระหว่างธนาคาร
            else if (__page.Equals(_cash_bank_Enum.Transfer_Money_Between_Banks.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_docno = new MyLib._searchDataFull();
            }
            //Bank_Statement,//005-รายงาน Bank Statement
            else if (__page.Equals(_cash_bank_Enum.Bank_Statement.ToString()))
            {   //--------------------------not-----------------
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_book_code, 1, 1, 1, true, false, true);
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._to_book_code, 1, 1, 1, true, false, true);
                //this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_period, 1, true);
                //this._addDateBox(1, 0, 1, 0, _g.d.resource_report._to_period, 1, true);
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_pass_book_code = new MyLib._searchDataFull();
               
            }
            //Received_Cash_sub_item,//006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
            else if (__page.Equals(_cash_bank_Enum.Received_Cash_sub_item.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_docno = new MyLib._searchDataFull();
            }
            //Open_sub_Cash,//007-รายงานการตั้งเบิกเงินสดย่อย
            else if (__page.Equals(_cash_bank_Enum.Open_sub_Cash.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_cash_sub_code, 1, 1, 1, true, false, true);//จากรหัสเงินสดย่อย
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_cash_sub_code, 1, 1, 1, true, false, true);//ถึงรหัสเงินสดย่อย
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_cost_date, 1, true);//จากวันที่ค่าใช้จ่าย
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_cost_date, 1, true);//ถึงวันที่ค่าใช้จ่าย
                _search_mastercb_group = new MyLib._searchDataFull();
            }
            //Cash_movements,//008-รายงานเคลื่อนไหวเงินสด
            else if (__page.Equals(_cash_bank_Enum.Cash_movements.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 10, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 10, true);
                _search_mastercb_group = new MyLib._searchDataFull();
                
            }
            //Pay_Cash_by_DocDate,//009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
            else if (__page.Equals(_cash_bank_Enum.Pay_Cash_by_DocDate.ToString()))
            {
                string[] __data = { _g.d.resource_report._all_print, _g.d.resource_report._print_only_to_pay, _g.d.resource_report._to_pay_particular_type };
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_staff_code, 1, 1, 1, true, false, true);//จากรหัสพนักงาน
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_staff_code, 1, 1, 1, true, false, true);//ถึงรหัสพนักงาน
                this._addComboBox(3, 0, _g.d.resource_report._conditions, true, __data, true); //เงื่อนไข--  พิมพ์ทั้งหมด ,พิมพ์เฉพาะที่ยังไม่จ่าย,พิมพ์เฉพาะที่จ่ายแล้ว      
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_staff_code = new MyLib._searchDataFull();
                _search_docno = new MyLib._searchDataFull();
            }
            //Monthly_Payment_Book,//010-รายงานสมุดจ่ายเงินประจำเดือน
            else if (__page.Equals(_cash_bank_Enum.Monthly_Payment_Book.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._year, 1, 1, 1, true, false, true);//ปี
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._monthly, 1, 1, 1, true, false, true);//ประจำเดือน
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_account_book_code, 1, 1, 1, true, false, true);//จากรหัสสมุดบัญชี
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_account_book_code, 1, 1, 1, true, false, true);//ถึงรหัสสมุดบัญชี
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_account_book_code = new MyLib._searchDataFull();
                
            }
            //Book_Bank_Balance,//011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
            else if (__page.Equals(_cash_bank_Enum.Book_Bank_Balance.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_address_book, 1, 1, 1, true, false, true);//จากเลขที่สมุด
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_address_book, 1, 1, 1, true, false, true);//ถึงเลขที่สมุด
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._at_date, 1, 1, 1, true, false, true);//ณ วันที่
                _search_mastercb_group = new MyLib._searchDataFull();
                //_search_addr_Code = new MyLib._searchDataFull();
                
            }
            //Sub_Cash_Movements,//012-รายงานเคลื่อนไหวเงินสดย่อย
            else if (__page.Equals(_cash_bank_Enum.Sub_Cash_Movements.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_cash_sub_code, 1, 1, 1, true, false, true);//จากรหัสเงินสดย่อย
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_cash_sub_code, 1, 1, 1, true, false, true);//ถึงรหัสเงินสดย่อย
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_cash_sub_code = new MyLib._searchDataFull();
                
            }
            //Summary_Payment//013-รายงานสรุปการจ่ายเงิน
            else if (__page.Equals(_cash_bank_Enum.Summary_Payment.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._year, 1, 1, 1, true, false, true);//ปี
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._monthly, 1, 1, 1, true, false, true);//ประจำเดือน
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_project_account_code, 1, 1, 1, true, false, true);//จากรหัสผังบัญชี
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_project_account_code, 1, 1, 1, true, false, true);//ถึงรหัสผังบัญชี
                _search_mastercb_group = new MyLib._searchDataFull();
                _search_project_ac_code = new MyLib._searchDataFull();
                
            }
            this.Invalidate();
            this.ResumeLayout();
            // ค้นหาเจ้าหนี้
            //if (this._search_masterap_group != null)
            //{
            //    this._search_masterap_group._name = _g.g._search_master_ap_group;
            //    this._search_masterap_group._dataList._loadViewFormat(this._search_masterap_group._name, MyLib._myGlobal._userSearchScreenGroup, false);
            //    _search_masterap_group._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            //    _search_masterap_group._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            //}
            // ค้นหาเอกสาร
            if (this._search_docno != null)
            {
                this._search_docno._name = _g.g._search_screen_cb_trans;
                this._search_docno._dataList._loadViewFormat(this._search_docno._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_docno._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_docno._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
            // ค้นหาจากรหัสสมุดรายวัน
            if (this._search_account_book_code != null)
            {
                this._search_account_book_code._name = _g.g._search_screen_gl_journal_book;
                this._search_account_book_code._dataList._loadViewFormat(this._search_account_book_code._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_account_book_code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_account_book_code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
            // ค้นหาจากจากรหัสสมุดบัญชีธนาคาร
            if (this._search_pass_book_code != null)
            {
                this._search_pass_book_code._name = _g.g._search_screen_สมุดเงินฝาก;
                this._search_pass_book_code._dataList._loadViewFormat(this._search_pass_book_code._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_pass_book_code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_pass_book_code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
            // ค้นหาจากเลขที่เช็ค
            if (this._search_number_check != null)
            {
                this._search_number_check._name = _g.g._search_screen_cb_เช็ครับ;
                this._search_number_check._dataList._loadViewFormat(this._search_number_check._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_number_check._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_number_check._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
            // ค้นหาจากเลขที่บัตรเครดิต
            if (this._search_number_credit_card != null)
            {
                this._search_number_credit_card._name = _g.g._search_screen_บัตรเครดิต;
                this._search_number_credit_card._dataList._loadViewFormat(this._search_number_credit_card._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_number_credit_card._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_number_credit_card._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
            // ค้นหาจากรหัสพนักงาน
            if (this._search_staff_code != null)
            {
                this._search_staff_code._name = _g.g._search_screen_erp_user;
                this._search_staff_code._dataList._loadViewFormat(this._search_staff_code._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_staff_code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_staff_code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
            // ค้นหาจากเลขที่สมุด
            //if (this._search_addr_Code != null)
            //{
            //    this._search_addr_Code._name = _g.g._search_screen_ic_trans;
            //    this._search_addr_Code._dataList._loadViewFormat(this._search_addr_Code._name, MyLib._myGlobal._userSearchScreenGroup, false);
            //    _search_addr_Code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            //    _search_addr_Code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            //}
            // ค้นหาจากรหัสเงินสดย่อย
            if (this._search_cash_sub_code != null)
            {
                this._search_cash_sub_code._name = _g.g._search_screen_cb_petty_cash;
                this._search_cash_sub_code._dataList._loadViewFormat(this._search_cash_sub_code._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_cash_sub_code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_cash_sub_code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
            // ค้นหาจากรหัสผังบัญชี
            if (this._search_project_ac_code != null)
            {
                this._search_project_ac_code._name = _g.g._search_screen_gl_cash_flow_group;
                this._search_project_ac_code._dataList._loadViewFormat(this._search_project_ac_code._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_project_ac_code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_project_ac_code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            }
        }
        void _search_docno__searchEnterKeyPress(MyLib._myGrid sender, int row)
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
            if (name.Equals(_g.g._search_screen_cb_trans))
            {
                __search = _search_docno;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_gl_journal_book))
            {
                __search = _search_account_book_code;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_สมุดเงินฝาก))
            {
                __search = _search_pass_book_code;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_cb_เช็ครับ))
            {
                __search = _search_number_check;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_cb_เช็คจ่าย))
            {
                __search = _search_number_check;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_บัตรเครดิต))
            {
                __search = _search_number_credit_card;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_erp_user))
            {
                __search = _search_staff_code;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            //if (name.Equals(_g.g._search_screen_ic_trans))
            //{
            //    __search = _search_addr_Code;
            //    __result = (string)__search._dataList._gridData._cellGet(row, 0);
            //}
            if (name.Equals(_g.g._search_screen_cb_petty_cash))
            {
                __search = _search_cash_sub_code;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_gl_cash_flow_group))
            {
                __search = _search_project_ac_code;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            //จับใส่ Text
            if (__result.Length > 0)
            {
                __search.Visible = false;
                this._setDataStr(_searchName, __result, "", false);
                _search(true);
            }
        }
        void _condition_cash_bank__textBoxChanged(object sender, string name)
        {
            //if (name.Equals(_g.d.ic_resource._ic_name))
            //{
            //    _searchTextBox = (TextBox)sender;
            //    _searchName = name;
            //    //   _search(true);
            //}
            //if (name.Equals(_g.d.ic_resource._ic_normal))
            //{
            //    _searchTextBox = (TextBox)sender;
            //    _searchName = name;
            //    //   _search(true);
            //}
        }

        void _condition_cash_bank__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            if (name.Equals(_g.d.resource_report._from_docno) || name.Equals(_g.d.resource_report._to_docno))
            {
                __search = _search_docno;
            }
            if (name.Equals(_g.d.resource_report._from_account_book_code) || name.Equals(_g.d.resource_report._to_account_book_code))
            {
                __search = _search_account_book_code;
            }
            if (name.Equals(_g.d.resource_report._from_book_code) || name.Equals(_g.d.resource_report._to_book_code))
            {
                __search = _search_pass_book_code;
            }
            if (name.Equals(_g.d.resource_report._from_check_number) || name.Equals(_g.d.resource_report._to_check_number))
            {
                __search = _search_number_check;
            }
            if (name.Equals(_g.d.resource_report._from_credit_number) || name.Equals(_g.d.resource_report._to_credit_number))
            {
                __search = _search_number_credit_card;
            }
            if (name.Equals(_g.d.resource_report._from_staff_code) || name.Equals(_g.d.resource_report._to_staff_code))
            {
                __search = _search_staff_code;
            }
            //if (name.Equals(_g.d.resource_report._from_address_book) || name.Equals(_g.d.resource_report._to_address_book))
            //{
            //    __search = _search_addr_Code;
            //}
            if (name.Equals(_g.d.resource_report._from_cash_sub_code) || name.Equals(_g.d.resource_report._to_cash_sub_code))
            {
                __search = _search_cash_sub_code;
            }
            if (name.Equals(_g.d.resource_report._from_project_account_code) || name.Equals(_g.d.resource_report._to_project_account_code))
            {
                __search = _search_project_ac_code;
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
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_trans._doc_no + " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._doc_no + "=\'" + this._getDataStr(_g.d.resource_report._from_docno) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.cb_trans._doc_no + " from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._doc_no + "=\'" + this._getDataStr(_g.d.resource_report._to_docno) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._getDataStr(_g.d.resource_report._from_staff_code) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._getDataStr(_g.d.resource_report._to_staff_code) + "\'"));

                __myquery.Append("</node>");
                ArrayList _getData = _myFramework._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.resource_report._from_docno, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.resource_report._to_docno, (DataSet)_getData[1], warning);
                _searchAndWarning(_g.d.resource_report._from_staff_code, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.resource_report._to_staff_code, (DataSet)_getData[3], warning);
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
                if (fieldName.Equals(_g.d.resource_report._from_docno) || fieldName.Equals(_g.d.resource_report._to_docno))
                {
                    this._setDataStr(fieldName, getDataStr);
                   // this._setDataStr(fieldName, getDataStr, getData, true);
                    //this._setDataStr(_g.d.resource_report._from_docno, getData);
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
    //public partial class _screen_grid_bank : MyLib._myGrid
    //{
    //    MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
    //    MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();

    //    public _screen_grid_bank()
    //    {
    //        this._clickSearchButton += new MyLib.SearchEventHandler(_screen_grid_bank__clickSearchButton);
    //    }

    //    public void _setFromToColumn(string __from_column_name, string __to_column_name)
    //    {
    //        this._clear();
    //        this._addColumn(_g.d.resource_report._table + "." + __from_column_name, 1, 1, 50, true, false, false, true);
    //        this._addColumn(_g.d.resource_report._table + "." + __to_column_name, 1, 1, 50, true, false, false, true);
    //        this._width_by_persent = true;
    //        this.AllowDrop = true;
    //        //this._isEdit = false;

    //        this.ColumnBackgroundAuto = false;
    //        this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
    //        this.RowOddBackground = Color.AliceBlue;
    //        this.AutoSize = true;
    //    }

    //    void _screen_grid_bank__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
    //    {
    //        string _searchName = this._cellGet(this._selectRow, this._selectColumn).ToString();
    //        string _search_text_new = _g.g._search_screen_ap;
    //        if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
    //        {
    //            this._search_data_full = new MyLib._searchDataFull();
    //            this._search_data_full._name = _search_text_new;
    //            this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
    //            this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
    //            this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
    //            this._search_data_full._dataList._refreshData();
    //            this._search_data_full._dataList._loadViewData(0);
    //        }
    //        MyLib._myGlobal._startSearchBox(this._inputTextBox, "ค้นหารหัสเจ้าหนี้", this._search_data_full, true);
    //    }
    //}
    public enum _cash_bank_Enum
    {
        /// <summary>
        /// 001-รายงานรายได้จากธนาคาร
        /// </summary>
        Revenue_by_Bank,//001-รายงานรายได้จากธนาคาร
        /// <summary>
        /// 002-รายงานการฝากเงินสด
        /// </summary>
        Cash_deposit,//002-รายงานการฝากเงินสด
        /// <summary>
        /// 003-รายงาน Statement ล่วงหน้า
        /// </summary>
        Statement_Advance,//003-รายงาน Statement ล่วงหน้า
        /// <summary>
        /// 004-รายงานการโอนเงินระหว่างธนาคาร
        /// </summary>
        Transfer_Money_Between_Banks,//004-รายงานการโอนเงินระหว่างธนาคาร
        /// <summary>
        /// 005-รายงาน Bank Statement
        /// </summary>
        Bank_Statement,//005-รายงาน Bank Statement
        /// <summary>
        /// 006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
        /// </summary>
        Received_Cash_sub_item,//006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
        /// <summary>
        /// 007-รายงานการตั้งเบิกเงินสดย่อย
        /// </summary>
        Open_sub_Cash,//007-รายงานการตั้งเบิกเงินสดย่อย
        /// <summary>
        /// 008-รายงานเคลื่อนไหวเงินสด
        /// </summary>
        Cash_movements,//008-รายงานเคลื่อนไหวเงินสด
        /// <summary>
        /// 009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
        /// </summary>
        Pay_Cash_by_DocDate,//009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
        /// <summary>
        /// 010-รายงานสมุดจ่ายเงินประจำเดือน
        /// </summary>
        Monthly_Payment_Book,//010-รายงานสมุดจ่ายเงินประจำเดือน
        /// <summary>
        /// 011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
        /// </summary>
        Book_Bank_Balance,//011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
        /// <summary>
        /// 012-รายงานเคลื่อนไหวเงินสดย่อย
        /// </summary>
        Sub_Cash_Movements,//012-รายงานเคลื่อนไหวเงินสดย่อย
        /// <summary>
        /// 013-รายงานสรุปการจ่ายเงิน
        /// </summary>
        Summary_Payment//013-รายงานสรุปการจ่ายเงิน


    }

}
