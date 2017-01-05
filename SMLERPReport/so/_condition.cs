using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPReport.so
{
    public partial class _condition : MyLib._myForm
    {
        string __page = "";
        public Boolean __check_submit = false;
        public string __where = "";
        public _condition(string __page)
        {
            InitializeComponent();
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            this.Load += new EventHandler(_condition_Load);
            this.__from_name(__page);
            this.__page = __page;
            _condition_so1._init(__page);
        }
        public void __from_name(string __page)
        {

            /*001*/
            if (_soEnum.Quotation_Deatail.ToString().Equals(__page)) this.Text = "รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย";
            /*002*/
            if (_soEnum.SaleOrder_Deatail.ToString().Equals(__page)) this.Text = "รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย";
            /*003*/
            if (_soEnum.Pay_Oreder.ToString().Equals(__page)) this.Text = "รายงานใบสั่งจ่ายสินค้า";
            /*004*/
            if (_soEnum.Deposit_Cut.ToString().Equals(__page)) this.Text = "รายงานการตัดใบรับเงินมัดจำ";
            /*005*/
            if (_soEnum.Deposit_Record.ToString().Equals(__page)) this.Text = "รายงานการบันทึกใบรับเงินมัดจำ";
            /*006*/
            if (_soEnum.Cash_Advance_by_Date.ToString().Equals(__page)) this.Text = "รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่";
            /*007*/
            if (_soEnum.Invoice_Detail.ToString().Equals(__page)) this.Text = "รายงานรายวันขายสินค้าและบริการ";
            /*008*/
            if (_soEnum.Tax_Invoice_Detail.ToString().Equals(__page)) this.Text = "รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย ";
            /*009*/
            if (_soEnum.Delivery_Money_by_Date.ToString().Equals(__page)) this.Text = "รายงานการนำส่งเงินประจำวัน";
            /*010*/
            if (_soEnum.Explain_Item_Category_Sales.ToString().Equals(__page)) this.Text = "รายงานการขายสินค้าแจกแจงตามประเภทการขาย";
            /*011*/
            if (_soEnum.Explain_Invoice_by_Product.ToString().Equals(__page)) this.Text = "รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า";
            /*012*/
            if (_soEnum.Analysis_Sell_Ex_by_Product.ToString().Equals(__page)) this.Text = "รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า";
            /*013*/
            if (_soEnum.Analysis_Sell_Sum_by_DocNo.ToString().Equals(__page)) this.Text = "รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร";
            /*014*/
            if (_soEnum.Sell_by_Sale.ToString().Equals(__page)) this.Text = "รายงานสรุปการขายสินค้าตามพนักงานขาย";
            /*015*/
            if (_soEnum.Sale_Billing.ToString().Equals(__page)) this.Text = "รายงานการเก็บเงินของพนักงานขาย";
            /*016*/
            if (_soEnum.Debt_Ar_Detail.ToString().Equals(__page)) this.Text = "รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย";
            /*017*/
            if (_soEnum.Check_Beck_Debt_by_Status.ToString().Equals(__page)) this.Text = "รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ";
            /*018*/
            if (_soEnum.Shipments_by_Date.ToString().Equals(__page)) this.Text = "รายงานสรุปยอดขายสินค้า-สรุปตามวันที่";
            /*019*/
            if (_soEnum.Shipments_Compare_Month.ToString().Equals(__page)) this.Text = "รายงานเปรียบเที่ยบยอดสินค้า12เดือน";
            /*020*/
            if (_soEnum.Sale_History_Product.ToString().Equals(__page)) this.Text = "รายงานประวัติการขายสินค้า";
            /*021*/
            if (_soEnum.Product_Rank.ToString().Equals(__page)) this.Text = "รายงานจัดอันดับยอด-สินค้า";
            /*022*/
            if (_soEnum.Margin_reacceptance.ToString().Equals(__page)) this.Text = "รายงานกำไรขั้นต้นแสดงหักรับคืน";
            /*023*/
            if (_soEnum.Income_Cost_Matching.ToString().Equals(__page)) this.Text = "Income & Cost matching report";
            /*024*/
            if (_soEnum.Invoice_Tax_Cut.ToString().Equals(__page)) this.Text = "รายงานการตัดใบส่งของ/ใบกำกับภาษี";

        }
        void _condition_Load(object sender, EventArgs e)
        {
            //_condition_so1._init(this.__page);
            this._condition_so1.AutoSize = true;
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this.__where = "" + this._condition_so1._createQueryForDatabase()[1].ToString();
            this.__check_submit = true;
            this.Close();
        }


    }
    public partial class _condition_so : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        /// <summary>
        /// Search DocNo
        /// </summary>
        MyLib._searchDataFull _search_docno = new MyLib._searchDataFull();
        /// <summary>
        /// Search AR
        /// </summary>
        MyLib._searchDataFull _search_ar = new MyLib._searchDataFull();
        /// <summary>
        /// Search Item Code
        /// </summary>
        MyLib._searchDataFull _search_item_code = new MyLib._searchDataFull();
        /// <summary>
        /// Search Customer
        /// </summary>
        MyLib._searchDataFull _search_customer = new MyLib._searchDataFull();
        /// <summary>
        /// Search Payment
        /// </summary>
        MyLib._searchDataFull _search_payment = new MyLib._searchDataFull();
        /// <summary>
        /// Search Department
        /// </summary>
        MyLib._searchDataFull _search_department = new MyLib._searchDataFull();
        /// <summary>
        /// Search License Sale
        /// </summary>
        MyLib._searchDataFull _search_license_sale = new MyLib._searchDataFull();
        /// <summary>
        /// Search allocation
        /// </summary>
        MyLib._searchDataFull _search_allocation = new MyLib._searchDataFull();
        /// <summary>
        /// Search Group Customer
        /// </summary>
        MyLib._searchDataFull _search_group_customer = new MyLib._searchDataFull();
        /// <summary>
        /// Search Dept Group
        /// </summary>
        MyLib._searchDataFull _search_dept_group = new MyLib._searchDataFull();
        /// <summary>
        /// Search Sale Person
        /// </summary>
        MyLib._searchDataFull _search_sale_person = new MyLib._searchDataFull();
        /// <summary>
        /// Search Check Product return Debt
        /// </summary>
        MyLib._searchDataFull _search_product_return_debt = new MyLib._searchDataFull();
        /// <summary>
        /// Search Group Item
        /// </summary>
        MyLib._searchDataFull _search_group_item = new MyLib._searchDataFull();
        /// <summary>
        /// Search Type
        /// </summary>
        MyLib._searchDataFull _search_type = new MyLib._searchDataFull();
        /// <summary>
        /// Search Brand
        /// </summary>
        MyLib._searchDataFull _search_brand = new MyLib._searchDataFull();
        /// <summary>
        /// Search Unit
        /// </summary>
        MyLib._searchDataFull _search_unit = new MyLib._searchDataFull();
        /// <summary>
        /// Search Card Purchase
        /// </summary>
        MyLib._searchDataFull _search_card_purchase = new MyLib._searchDataFull();
        /// <summary>
        /// Search Booking ID
        /// </summary>
        MyLib._searchDataFull _search_booking_id = new MyLib._searchDataFull();

        public _condition_so()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_so__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_so__textBoxChanged);
        }
        public void _init(string __page)
        {

            //Quotation_Deatail,//001-รายงานการบันทึกใบเสนอราคาพร้อมรายการย่อย
            if (__page.Equals(_soEnum.Quotation_Deatail.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากลูกหนี้
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงลูกหนี้
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addCheckBox(5, 0, _g.d.resource_report._show_number_of_quote_by_date, false, true);//แสดงจำนวนใบเสนอราคาตามวันที่ที่กำหนด
            }
            //SaleOrder_Deatail,//002-รายงานใบสั่งขาย/จองสินค้าพร้อมรายการย่อย
            else if (__page.Equals(_soEnum.SaleOrder_Deatail.ToString()))
            {
                string[] __data = { "เฉพาะใบสั่งขายสินค้า", "เฉพาะใบสั่งจองสินค้า", "รวมเอกสาร" };
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากลูกหนี้
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงลูกหนี้
                this._addComboBox(4, 0, _g.d.resource_report._show_products_by_name_daily, true, __data, true);//แสดงชื่อสินค้าตามรายวัน
                this._addCheckBox(5, 0, _g.d.resource_report._show_number_of_quote_by_date, false, true);//แสดงจำนวนใบเสนอราคาตามวันที่ที่กำหนด
            }
            //Pay_Oreder,//003-รายงานใบสั่งจ่ายสินค้า
            else if (__page.Equals(_soEnum.Pay_Oreder.ToString()))
            {
                string[] __data = { "วันที่เอกสาร", "รหัสลูกค้า", "รหัสสินค้า" };
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากลูกหนี้
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงลูกหนี้
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addNumberBox(4, 0, 1, 1, _g.d.resource_report._from_number, 1, 0, true);// จากจำนวน
                this._addNumberBox(4, 1, 1, 1, _g.d.resource_report._to_number, 1, 0, true);//ถึงจำนวน
                this._addNumberBox(5, 0, 1, 1, _g.d.resource_report._from_amount, 1, 0, true);
                this._addNumberBox(5, 1, 1, 1, _g.d.resource_report._to_amount, 1, 0, true);
                this._addComboBox(6, 0, _g.d.resource_report._order_by, true, __data, true);//เรียงตาม
            }
            //Deposit_Cut,//004-รายงานการตัดใบรับเงินมัดจำ || //Deposit_Record,//005-รายงานการบันทึกใบรับเงินมัดจำ
            else if (__page.Equals(_soEnum.Deposit_Cut.ToString()) || __page.Equals(_soEnum.Deposit_Record.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากลูกหนี้
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงลูกหนี้
            }
            //Cash_Advance_by_Date,//006-รายงานใบรับเงินล่วงหน้าคงค้าง-ตามวันที่
            else if (__page.Equals(_soEnum.Cash_Advance_by_Date.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._print_balance_at_date, 1, true);//พิมพ์ยอดคงเหลือ ณ วันที่
                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true);//จากรหัสลูกค้า
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(5, 0, 1, 0, _g.d.resource_report._from_payment, 1, 1, 1, true, false, true); // จากวิธีรับชำระ
                this._addTextBox(5, 1, 1, 0, _g.d.resource_report._to_payment, 1, 1, 1, true, false, true);//ถึงวิธีรับชำระ
                this._addCheckBox(6, 0, _g.d.resource_report._not_total_lift_zero, false, true);
            }
            //Invoice_Detail,//007-รายงานรายวันขายสินค้าและบริการ
            else if (__page.Equals(_soEnum.Invoice_Detail.ToString()))
            {
                string[] __data = { "จากรายการทั้งหมด", "ขายสินค้าเงินสด", "ขายสินค้าเงินเชื่อ", "ค่าบริการเงินสด", "ค่าบริการเงินเชื่อ" };
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากลูกหนี้
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงลูกหนี้
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_department, 1, 1, 1, true, false, true);
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_department, 1, 1, 1, true, false, true);
                this._addComboBox(5, 0, _g.d.resource_report._category_sale, true, __data, true);//ประเภทการขาย
            }
            //Tax_Invoice_Detail,//008-รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย
            else if (__page.Equals(_soEnum.Tax_Invoice_Detail.ToString()))
            {
                string[] __data = { "จากรายการทั้งหมด", "ขายสินค้าเงินสด", "ขายสินค้าเงินเชื่อ", "ค่าบริการเงินสด", "ค่าบริการเงินเชื่อ" };
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_license_sale, 1, 1, 1, true, false, true);//จากเลขที่ใบขาย
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_license_sale, 1, 1, 1, true, false, true);//ถึงเลขที่ใบขาย
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากลูกหนี้
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงลูกหนี้
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addComboBox(5, 0, _g.d.resource_report._category_sale, true, __data, true);//ประเภทการขาย
                this._addCheckBox(6, 0, _g.d.resource_report._display_serial, false, true);//แสดง serail
            }
            //Delivery_Money_by_Date,//009-รายงานการนำส่งเงินประจำวัน
            else if (__page.Equals(_soEnum.Delivery_Money_by_Date.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true); // จากรหัสลูกค้า
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addNumberBox(4, 0, 1, 1, _g.d.resource_report._from_amount, 1, 0, true);
                this._addNumberBox(5, 0, 1, 1, _g.d.resource_report._to_amount, 1, 0, true);

            }
            //Explain_Item_Category_Sales,//010-รายงานการขายสินค้าแจกแจงตามประเภทการขาย
            else if (__page.Equals(_soEnum.Explain_Item_Category_Sales.ToString()))
            {
                string[] __data = { "จากรายการทั้งหมด", "ขายสินค้าเงินสด", "ขายสินค้าเงินเชื่อ", "ค่าบริการเงินสด", "ค่าบริการเงินเชื่อ" };
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_license_sale, 1, 1, 1, true, false, true);//จากเลขที่ใบขาย
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_license_sale, 1, 1, 1, true, false, true);//ถึงเลขที่ใบขาย
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true); // จากรหัสลูกค้า
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า
                this._addComboBox(4, 0, _g.d.resource_report._category_sale, true, __data, true);//ประเภทการขาย
            }
            //Explain_Invoice_by_Product,//011-รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า
            else if (__page.Equals(_soEnum.Explain_Invoice_by_Product.ToString()))
            {
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true); // จากรหัสลูกค้า
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า
            }
            //Analysis_Sell_Ex_by_Product,//012-รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า
            else if (__page.Equals(_soEnum.Analysis_Sell_Ex_by_Product.ToString()))
            {
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addNumberBox(3, 0, 1, 1, _g.d.resource_report._from_number, 1, 0, true);// จากจำนวน
                this._addNumberBox(3, 1, 1, 1, _g.d.resource_report._to_number, 1, 0, true);//ถึงจำนวน
                this._addNumberBox(4, 0, 1, 1, _g.d.resource_report._from_amount, 1, 0, true);
                this._addNumberBox(4, 1, 1, 1, _g.d.resource_report._to_amount, 1, 0, true);
            }
            //Analysis_Sell_Sum_by_DocNo,//013-รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร
            else if (__page.Equals(_soEnum.Analysis_Sell_Sum_by_DocNo.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true); // จากรหัสลูกค้า
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_allocation_code, 1, 1, 1, true, false, true);
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_allocation_code, 1, 1, 1, true, false, true);
                this._addTextBox(5, 0, 1, 0, _g.d.resource_report._from_customer_group, 1, 1, 1, true, false, true);//จากกลุ่มลูกค้า
                this._addTextBox(5, 1, 1, 0, _g.d.resource_report._to_customer_group, 1, 1, 1, true, false, true);//ถึงกลุ่มลูกค้า
                this._addTextBox(6, 0, 1, 0, _g.d.resource_report._from_dept_group, 1, 1, 1, true, false, true); //From DEPT Group
                this._addTextBox(6, 1, 1, 0, _g.d.resource_report._to_dept_group, 1, 1, 1, true, false, true);//To DEPT Group

            }
            //Sell_by_Sale,//014-รายงานสรุปการขายสินค้าตามพนักงานขาย
            else if (__page.Equals(_soEnum.Sell_by_Sale.ToString()))
            {

            }
            //Sale_Billing,//015-รายงานการเก็บเงินของพนักงานขาย
            else if (__page.Equals(_soEnum.Sale_Billing.ToString()))
            {
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_sale_person, 1, 1, 1, true, false, true);
                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true); // จากรหัสลูกค้า
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า

            }
            //Debt_Ar_Detail,//016-รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย
            else if (__page.Equals(_soEnum.Debt_Ar_Detail.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากรหัสลูกค้า
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า
            }
            //Check_Beck_Debt_by_Status,//017-รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ
            else if (__page.Equals(_soEnum.Check_Beck_Debt_by_Status.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_check_product_returns_debt, 1, 1, 1, true, false, true);//จากเลขที่ใบรับคืนสินค้า/ลด
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_check_product_returns_debt, 1, 1, 1, true, false, true);//ถึงเลขที่ใบรับสืนสินค้า/ลดหนี้
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากรหัสลูกค้า
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงรหัสลูกค้า
            }
            //Shipments_by_Date,//018-รายงานสรุปยอดขายสินค้า-สรุปตามวันที่
            else if (__page.Equals(_soEnum.Shipments_by_Date.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addNumberBox(3, 0, 1, 1, _g.d.resource_report._from_number, 1, 0, true);// จากจำนวน
                this._addNumberBox(3, 1, 1, 1, _g.d.resource_report._to_number, 1, 0, true);//ถึงจำนวน
            }
            //Shipments_Compare_Month,//019-รายงานเปรียบเที่ยบยอดสินค้า12เดือน
            else if (__page.Equals(_soEnum.Shipments_Compare_Month.ToString()))
            {
                //ระบุปี
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_group, 1, 1, 1, true, false, true);//จากกลุ่มสินค้า
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_group, 1, 1, 1, true, false, true);//ถึงกลุ่มสินค้า
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                //this._addComboBox(4, 0, _g.d.resource_report._from_month, true, __data, true);//จากเดือน
                //this._addComboBox(4, 1, _g.d.resource_report._to_month, true, __data, true);//ถึงเดือน
            }
            //Sale_History_Product,//020-รายงานประวัติการขายสินค้า
            else if (__page.Equals(_soEnum.Sale_History_Product.ToString()))
            {
                //this._addCheckBox(1, 0, _g.d.resource_report._sh, false, true);// แสดงรายงานตามลูกค้า
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_customer_code, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_customer_code, 1, 1, 1, true, false, true);
                this._addDateBox(4, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(4, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(5, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(5, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(6, 0, 1, 0, _g.d.resource_report._from_group, 1, 1, 1, true, false, true);
                this._addTextBox(6, 1, 1, 0, _g.d.resource_report._to_group, 1, 1, 1, true, false, true);
                this._addTextBox(7, 0, 1, 0, _g.d.resource_report._from_type, 1, 1, 1, true, false, true);
                this._addTextBox(7, 1, 1, 0, _g.d.resource_report._to_type, 1, 1, 1, true, false, true);
                this._addTextBox(8, 0, 1, 0, _g.d.resource_report._from_brand, 1, 1, 1, true, false, true);
                this._addTextBox(8, 1, 1, 0, _g.d.resource_report._to_brand, 1, 1, 1, true, false, true);
            }
            //Product_Rank,//021-รายงานจัดอันดับยอด-สินค้า
            else if (__page.Equals(_soEnum.Product_Rank.ToString()))
            {
                string[] __data_1 = { _g.d.resource_report._order_quantity, _g.d.resource_report._order_value };
                string[] __data_2 = { _g.d.resource_report._from_large_to_small, _g.d.resource_report._from_small_to_large };
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_unit, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_unit, 1, 1, 1, true, false, true);
                this._addComboBox(4, 0, _g.d.resource_report._category_sale, true, __data_1, true);//เรียงตาม /เรียงตามปริมาณ , เรียงตามมูลค่า
                this._addComboBox(5, 0, _g.d.resource_report._category_sale, true, __data_2, true);//เรียงจาก /เรียงจากมากไปน้อย , เรียงจากน้อยไปมาก
            }
            //Margin_reacceptance,//022-รายงานกำไรขั้นต้นแสดงหักรับคืน
            else if (__page.Equals(_soEnum.Margin_reacceptance.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addNumberBox(4, 0, 1, 1, _g.d.resource_report._from_number, 1, 0, true);// จากจำนวน
                this._addNumberBox(4, 1, 1, 1, _g.d.resource_report._to_number, 1, 0, true);//ถึงจำนวน
                //this._addCheckBox(5, 0, _g.d.resource_report._, false, true);// สรุปตามลูกค้า
            }
            //Income_Cost_Matching,//023-Income & Cost matching report
            else if (__page.Equals(_soEnum.Income_Cost_Matching.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_sale_date, 1, true);//จากวันที่ขาย
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_sale_date, 1, true);//ถึงวันที่ขาย
                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_date_of_purchase, 1, true);//จากวันที่ซื้อ
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_date_of_purchase, 1, true);//ถึงวันที่ซื้อ
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_license_sale, 1, 1, 1, true, false, true);//จากเลขที่ใบขาย
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_license_sale, 1, 1, 1, true, false, true);//ถึงเลขที่ใบขาย
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_card_purchase, 1, 1, 1, true, false, true);//จากเลขที่ใบซื้อ
                this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_card_purchase, 1, 1, 1, true, false, true);//ถึงเลขที่ใบซื้อ
                this._addTextBox(5, 0, 1, 0, _g.d.resource_report._from_booking_id, 1, 1, 1, true, false, true);//From Booking ID
                this._addTextBox(5, 1, 1, 0, _g.d.resource_report._to_booking_id, 1, 1, 1, true, false, true);//TO Booking ID
            }
            //Invoice_Tax_Cut//024-รายงานการตัดใบส่งของ/ใบกำกับภาษี
            else if (__page.Equals(_soEnum.Invoice_Tax_Cut.ToString()))
            {
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 1, 1, true, false, true);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 1, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_ar, 1, 1, 1, true, false, true); // จากลูกหนี้
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_ar, 1, 1, 1, true, false, true);//ถึงลูกหนี้
            }
            this.Invalidate();
            this.ResumeLayout();
            // ค้นหาเอกสาร
            this._search_docno._name = _g.g._search_screen_ic_trans;
            this._search_docno._dataList._loadViewFormat(this._search_docno._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_docno._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_docno._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหาลูกหนี้
            this._search_ar._name = _g.g._search_screen_ar;
            this._search_ar._dataList._loadViewFormat(this._search_ar._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_ar._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_ar._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหารหัสสินค้า
            this._search_item_code._name = _g.g._search_screen_ic_inventory;
            this._search_item_code._dataList._loadViewFormat(this._search_item_code._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_item_code._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_item_code._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหารหัสลูกค้า
            this._search_customer._name = _g.g._screen_ap_ar_trans;
            this._search_customer._dataList._loadViewFormat(this._search_customer._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_customer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_customer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหาวิธีรับชำระ
            /*this._search_payment._name = _g.g.;
            this._search_payment._dataList._loadViewFormat(this._search_payment._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_payment._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_payment._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);*/
            // ค้นหาแผนก
            this._search_department._name = _g.g._search_screen_erp_department_list;
            this._search_department._dataList._loadViewFormat(this._search_department._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_department._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_department._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหาเลขที่ใบขาย
            this._search_license_sale._name = _g.g._search_screen_po_so_inquiry;
            this._search_license_sale._dataList._loadViewFormat(this._search_license_sale._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_license_sale._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_license_sale._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหารหัสการจัดสรร
            this._search_allocation._name = _g.g._search_screen_erp_allocate;
            this._search_allocation._dataList._loadViewFormat(this._search_allocation._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_allocation._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_allocation._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหากลุ่มลูกค้า
            this._search_group_customer._name = _g.g._search_master_ar_customer_group;
            this._search_group_customer._dataList._loadViewFormat(this._search_group_customer._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_group_customer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_group_customer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหา Dept Group
            /*this._search_dept_group._name = _g.g._sear;
            this._search_dept_group._dataList._loadViewFormat(this._search_dept_group._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_dept_group._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_dept_group._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);*/
            // ค้นหาพนักงาน
            this._search_sale_person._name = _g.g._search_screen_erp_user;
            this._search_sale_person._dataList._loadViewFormat(this._search_sale_person._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_sale_person._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_sale_person._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหาจากเลขที่ใบรับคืนสินค้า/ลด
            /*this._search_product_return_debt._name = _g.g.;
            this._search_product_return_debt._loadViewFormat(this._search_product_return_debt._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_product_return_debt._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_product_return_debt._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);*/
            // ค้นหากลุ่มสินค้า
            this._search_group_item._name = _g.g._search_master_ic_group;
            this._search_group_item._dataList._loadViewFormat(this._search_group_item._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_group_item._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_group_item._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหาชนิดสินค้า
            this._search_type._name = _g.g._search_master_ic_type;
            this._search_type._dataList._loadViewFormat(this._search_type._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_type._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_type._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหายี่ห้อสินค้า
            this._search_brand._name = _g.g._search_master_ic_brand;
            this._search_brand._dataList._loadViewFormat(this._search_brand._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_brand._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_brand._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหาหน่วยนับ
            this._search_unit._name = _g.g._search_master_ic_unit;
            this._search_unit._dataList._loadViewFormat(this._search_unit._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_unit._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_unit._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหาเลขที่ใบซื้อ
            this._search_card_purchase._name = _g.g._search_screen_po_so_inquiry;
            this._search_card_purchase._dataList._loadViewFormat(this._search_card_purchase._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_card_purchase._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_card_purchase._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
            // ค้นหา Booking ID
            this._search_booking_id._name = _g.g._search_screen_po_so_inquiry;
            this._search_booking_id._dataList._loadViewFormat(this._search_booking_id._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _search_booking_id._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _search_booking_id._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_docno__searchEnterKeyPress);
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
            if (name.Equals(_g.g._search_screen_ic_trans))
            {
                __search = _search_docno;
                __result = (string)__search._dataList._gridData._cellGet(row, 1);
            }
            if (name.Equals(_g.g._search_screen_ar))
            {
                __search = _search_ar;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_ic_inventory))
            {
                __search = _search_item_code;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._screen_ap_ar_trans))
            {
                __search = _search_customer;
                __result = (string)__search._dataList._gridData._cellGet(row, 2);
            }
            if (name.Equals(_g.g._search_screen_erp_department_list))
            {
                __search = _search_department;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_po_so_inquiry))
            {
                __search = _search_license_sale;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_erp_allocate))
            {
                __search = _search_allocation;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_master_ar_customer_group))
            {
                __search = _search_group_customer;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_erp_user))
            {
                __search = _search_sale_person;
                __result = (string)__search._dataList._gridData._cellGet(row, 2);
            }
            if (name.Equals(_g.g._search_master_ic_group))
            {
                __search = _search_group_item;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_master_ic_type))
            {
                __search = _search_type;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_master_ic_brand))
            {
                __search = _search_brand;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_master_ic_unit))
            {
                __search = _search_unit;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_po_so_inquiry))
            {
                __search = _search_card_purchase;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            if (name.Equals(_g.g._search_screen_po_so_inquiry))
            {
                __search = _search_booking_id;
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
        void _condition_so__textBoxChanged(object sender, string name)
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

        void _condition_so__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            if (name.Equals(_g.d.resource_report._from_docno) || name.Equals(_g.d.resource_report._to_docno))
            {
                __search = _search_docno;
            }
            if (name.Equals(_g.d.resource_report._from_ar) || name.Equals(_g.d.resource_report._to_ar))
            {
                __search = _search_ar;
            }
            if (name.Equals(_g.d.resource_report._from_item_code) || name.Equals(_g.d.resource_report._to_item_code))
            {
                __search = _search_item_code;
            }
            if (name.Equals(_g.d.resource_report._from_customer_code) || name.Equals(_g.d.resource_report._to_customer_code))
            {
                __search = _search_customer;
            }
            if (name.Equals(_g.d.resource_report._from_department) || name.Equals(_g.d.resource_report._to_department))
            {
                __search = _search_department;
            }
            if (name.Equals(_g.d.resource_report._from_license_sale) || name.Equals(_g.d.resource_report._to_license_sale))
            {
                __search = _search_license_sale;
            }
            if (name.Equals(_g.d.resource_report._from_allocation_code) || name.Equals(_g.d.resource_report._to_allocation_code))
            {
                __search = _search_allocation;
            }
            if (name.Equals(_g.d.resource_report._from_customer_group) || name.Equals(_g.d.resource_report._to_customer_group))
            {
                __search = _search_group_customer;
            }
            if (name.Equals(_g.d.resource_report._from_sale_person) || name.Equals(_g.d.resource_report._to_sale_person))
            {
                __search = _search_sale_person;
            }
            if (name.Equals(_g.d.resource_report._from_group) || name.Equals(_g.d.resource_report._to_group))
            {
                __search = _search_group_item;
            }
            if (name.Equals(_g.d.resource_report._from_type) || name.Equals(_g.d.resource_report._to_type))
            {
                __search = _search_type;
            }
            if (name.Equals(_g.d.resource_report._from_brand) || name.Equals(_g.d.resource_report._to_brand))
            {
                __search = _search_brand;
            }
            if (name.Equals(_g.d.resource_report._from_unit) || name.Equals(_g.d.resource_report._to_unit))
            {
                __search = _search_unit;
            }
            if (name.Equals(_g.d.resource_report._from_card_purchase) || name.Equals(_g.d.resource_report._to_card_purchase))
            {
                __search = _search_card_purchase;
            }
            if (name.Equals(_g.d.resource_report._from_booking_id) || name.Equals(_g.d.resource_report._to_booking_id))
            {
                __search = _search_booking_id;
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
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }
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
