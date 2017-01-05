using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.Cheque_Card
{
    public partial class condition_cheque_card : Form
    {
        public string _where = "";
        //string __where_date = "";
        public string _page = "";

        public condition_cheque_card()
        {

            this.InitializeComponent();
            this.Load += new EventHandler(condition_cheque_card_Load);
            this._vistaButton_exit.Click += new EventHandler(_vistaButton_exit_Click);
            this._vistaButton_process.Click += new EventHandler(_vistaButton_process_Click);
        }

        void _vistaButton_process_Click(object sender, EventArgs e)
        {
            _get_where();
        }

        void _get_where()
        {
            if (_page.Equals(_cheque_cardEnumration.EndDate_Card.ToString()))
            {
                //  รายงานรายละเอียดบัตรเครดิต-ครบกำหนด
                string[] __data = _condition_cheque_card1._createQueryForDatabase()[1].ToString().Split(',');
                string __from_date = ""; string __to_date = ""; string __from_docno = ""; string __to_docno = ""; string __from_ar = ""; string __to_ar = ""; string __status_card = "";
                string __where_date = ""; string __where_docno = ""; string __where_ar = ""; string __where_status = "";
                // รับค่าจากหน้าจอ 
                __from_date = __data[0].ToString(); __to_date = __data[1].ToString();
                __from_docno = __data[2].ToString(); __to_docno = __data[3].ToString();
                __from_ar = __data[4].ToString(); __to_ar = __data[5].ToString();
                __status_card = __data[6].ToString();
                if (__from_date != "null" && __to_date != "null")
                {
                    __where_date = " and " + _g.d.cb_trans_detail._doc_date + " between " + __from_date + " and " + __to_date;
                }
                else
                {
                    if (__from_date != "null") __where_date = " and " + _g.d.cb_trans_detail._doc_date + " = " + __from_date;
                    if (__to_date != "null") __where_date = " and " + _g.d.cb_trans_detail._doc_date + " = " + __to_date;
                }

                if (__from_docno != "null" && __to_docno != "null")
                {
                    __where_docno = " and " + _g.d.cb_trans_detail._doc_no + " between " + __from_docno + " and " + __to_docno;
                }
                else
                {
                    if (__from_docno != "null") __where_docno = " and " + _g.d.cb_trans_detail._doc_no + " = " + __from_docno;
                    if (__to_docno != "null") __where_docno = " and " + _g.d.cb_trans_detail._doc_no + " = " + __to_docno;
                }

                if (__from_ar != "null" && __to_ar != "null")
                {
                    __where_ar = " and " + _g.d.ar_customer._name_1 + " between " + __from_ar + " and " + __to_ar;
                }
                else
                {
                    if (__from_ar != "null") __where_ar = " and " + _g.d.ar_customer._name_1 + " = " + __from_ar;
                    if (__to_ar != "null") __where_ar = " and " + _g.d.ar_customer._name_1 + " = " + __to_ar;
                }

                if (__status_card != "null") __where_status = " and " + _g.d.cb_credit_card._credit_card_type + " = " + __status_card;

                this._where = _g.d.cb_trans_detail._credit_card_no + " <> '' " + __where_date + __where_docno + __where_ar + __where_status;
            }
            else if (_page.Equals(_cheque_cardEnumration.EndDate_Pay_Cheque.ToString()))
            {
                //  รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด
                string[] __data = _condition_cheque_card1._createQueryForDatabase()[1].ToString().Split(',');
                string __from_date = ""; string __to_date = ""; string __from_docno = ""; string __to_docno = ""; string __from_ap = ""; string __to_ap = ""; string __status_card = "";
                string __where_date = ""; string __where_docno = ""; string __where_ap = ""; string __where_status = "";
                // รับค่าจากหน้าจอ 
                __from_date = __data[0].ToString(); __to_date = __data[1].ToString();
                __from_docno = __data[2].ToString(); __to_docno = __data[3].ToString();
                __from_ap = __data[4].ToString(); __to_ap = __data[5].ToString();
                __status_card = __data[6].ToString();
                if (__from_date != "null" && __to_date != "null")
                {
                    __where_date = " and " + _g.d.cb_trans_detail._doc_date + " between " + __from_date + " and " + __to_date;
                }
                else
                {
                    if (__from_date != "null") __where_date = " and " + _g.d.cb_trans_detail._doc_date + " = " + __from_date;
                    if (__to_date != "null") __where_date = " and " + _g.d.cb_trans_detail._doc_date + " = " + __to_date;
                }

                if (__from_docno != "null" && __to_docno != "null")
                {
                    __where_docno = " and " + _g.d.cb_trans_detail._doc_no + " between " + __from_docno + " and " + __to_docno;
                }
                else
                {
                    if (__from_docno != "null") __where_docno = " and " + _g.d.cb_trans_detail._doc_no + " = " + __from_docno;
                    if (__to_docno != "null") __where_docno = " and " + _g.d.cb_trans_detail._doc_no + " = " + __to_docno;
                }

                if (__from_ap != "null" && __to_ap != "null")
                {
                    __where_ap = " and " + _g.d.ap_supplier._name_1 + " between " + __from_ap + " and " + __to_ap;
                }
                else
                {
                    if (__from_ap != "null") __where_ap = " and " + _g.d.ap_supplier._name_1 + " = " + __from_ap;
                    if (__to_ap != "null") __where_ap = " and " + _g.d.ap_supplier._name_1 + " = " + __to_ap;
                }

                if (__status_card != "null") __where_status = " and " + _g.d.cb_credit_card._credit_card_type + " = " + __status_card;

                this._where = _g.d.cb_trans_detail._trans_number + " <> '' " + __where_date + __where_docno + __where_ap + __where_status;
            }

            else if (_page.Equals(_cheque_cardEnumration.Cash_money_detail.ToString()))
            {
                //  รายงานรายวันขี้นเงินพร้อมรายการย่อย
            }

            else if (_page.Equals(_cheque_cardEnumration.Cancel_cheque_detail.ToString()))
            {
                //  รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย
            }

            else if (_page.Equals(_cheque_cardEnumration.Cancel_Card_detail.ToString()))
            {
                //  รายงานการยกเลิกบัตรพร้อมรายการย่อย
            }

            else if (_page.Equals(_cheque_cardEnumration.Disposit_Cheque_detail.ToString()))
            {
                //  รายงานใบนำฝากเช็ครับพร้อมรายการย่อย
            }

            else if (_page.Equals(_cheque_cardEnumration.Detail_Cheque_by_Date_import.ToString()))
            {
                //  รายงานรายละเอียดเช็ครับ-ตามวันที่รับ
            }

            this.Close();

        }

        void _vistaButton_exit_Click(object sender, EventArgs e)
        {
            this._condition_cheque_card1._clear();
            this.Close();
        }

        void condition_cheque_card_Load(object sender, EventArgs e)
        {
            this._condition_cheque_card1._build(this._page);
            this.Width = 650;
            if (_page.Equals(_cheque_cardEnumration.EndDate_Card.ToString()))
            {
                this.Height = 230;
                this.Text = "รายงานรายละเอียดบัตรเครดิต-ครบกำหนด";
            }
            else if (_page.Equals(_cheque_cardEnumration.EndDate_Pay_Cheque.ToString()))
            {
                this.Height = 230;
                this.Text = "รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด";
            }
            else if (_page.Equals(_cheque_cardEnumration.Cash_money_detail.ToString()))
            {
                this.Height = 220;
                this.Text = "รายงานรายวันขี้นเงินพร้อมรายการย่อย";
            }
            else if (_page.Equals(_cheque_cardEnumration.Cancel_cheque_detail.ToString()))
            {
                this.Height = 220;
                this.Text = "รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย";
            }
            else if (_page.Equals(_cheque_cardEnumration.Cancel_Card_detail.ToString()))
            {
                this.Height = 220;
                this.Text = "รายงานการยกเลิกบัตรพร้อมรายการย่อย";
            }
            else if (_page.Equals(_cheque_cardEnumration.Disposit_Cheque_detail.ToString()))
            {
                this.Height = 280;
                this.Text = "รายงานใบนำฝากเช็ครับพร้อมรายการย่อย ";
            }
            else if (_page.Equals(_cheque_cardEnumration.Detail_Cheque_by_Date_import.ToString()))
            {
                this.Height = 230;
                this.Text = "รายงานรายละเอียดเช็ครับ-ตามวันที่รับ";
            }

        }

    }


    public partial class _condition_cheque_card_screen : MyLib._myScreen
    {
        public void _build(string __page)
        {
            this._table_name = _g.d.resource_report._table;
            if (_cheque_cardEnumration.EndDate_Card.ToString().Equals(__page))
            {
                // รายงานรายละเอียดบัตรเครดิต-ครบกำหนด

                this._maxColumn = 4;
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_end_date, 2, true, true);
                this._addDateBox(0, 2, 1, 0, _g.d.resource_report._to_end_date, 2, true, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 2, 2, 1, true, false, true);
                this._addTextBox(1, 2, 1, 0, _g.d.resource_report._to_docno, 2, 2, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_ar, 2, 2, 1, true, false, true);
                this._addTextBox(2, 2, 1, 0, _g.d.resource_report._to_ar, 2, 2, 1, true, false, true);

                string[] __data = { _g.d.resource_report._all, _g.d.resource_report._in_hand, _g.d.resource_report._up_money_already, _g.d.resource_report._cancel };
                this._addComboBox(3, 0, _g.d.resource_report._select_status_card, true, __data, true);
            }
            else if (_cheque_cardEnumration.EndDate_Pay_Cheque.ToString().Equals(__page))
            {
                //  รายงานรายละเอียดเช็คจ่าย-วันครบกำหนด
                this._maxColumn = 4;
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_end_date, 2, true, true);
                this._addDateBox(0, 2, 1, 0, _g.d.resource_report._to_end_date, 2, true, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 2, 2, 1, true, false, true);
                this._addTextBox(1, 2, 1, 0, _g.d.resource_report._to_docno, 2, 2, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_payable, 2, 2, 1, true, false, true);
                this._addTextBox(2, 2, 1, 0, _g.d.resource_report._to_payable, 2, 2, 1, true, false, true);

                string[] __data = { _g.d.resource_report._all, _g.d.resource_report._in_hand, _g.d.resource_report._cheque_pass, _g.d.resource_report._cheque_cancel };
                this._addComboBox(3, 0, _g.d.resource_report._select_status_card, true, __data, true);
            }
            else if (_cheque_cardEnumration.Cash_money_detail.ToString().Equals(__page) || _cheque_cardEnumration.Cancel_cheque_detail.ToString().Equals(__page) || _cheque_cardEnumration.Cancel_Card_detail.ToString().Equals(__page))
            {
                //  รายงานรายวันขี้นเงินพร้อมรายการย่อย    รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย    รายงานการยกเลิกบัตรพร้อมรายการย่อย
                this._maxColumn = 4;
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 2, true, true);
                this._addDateBox(0, 2, 1, 0, _g.d.resource_report._to_docdate, 2, true, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 2, 2, 1, true, false, true);
                this._addTextBox(1, 2, 1, 0, _g.d.resource_report._to_docno, 2, 2, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_daily_day, 2, 2, 1, true, false, true);
                this._addTextBox(2, 2, 1, 0, _g.d.resource_report._to_daily_day, 2, 2, 1, true, false, true);
            }


            else if (_cheque_cardEnumration.Disposit_Cheque_detail.ToString().Equals(__page))
            {
                //  รายงานใบนำฝากเช็ครับพร้อมรายการย่อย 
                this._maxColumn = 4;
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 2, true, true);
                this._addDateBox(0, 2, 1, 0, _g.d.resource_report._to_docdate, 2, true, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 2, 2, 1, true, false, true);
                this._addTextBox(1, 2, 1, 0, _g.d.resource_report._to_docno, 2, 2, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_daily_day, 2, 2, 1, true, false, true);
                this._addTextBox(2, 2, 1, 0, _g.d.resource_report._to_daily_day, 2, 2, 1, true, false, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_customer_code, 2, 2, 1, true, false, true);
                this._addTextBox(3, 2, 1, 0, _g.d.resource_report._to_customer_code, 2, 2, 1, true, false, true);
                this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_sale_person, 2, 2, 1, true, false, true);
                this._addTextBox(4, 2, 1, 0, _g.d.resource_report._to_sale_person, 2, 2, 1, true, false, true);

                string[] __data = { _g.d.resource_report._docno, _g.d.resource_report._book_deposit, _g.d.resource_report._custom_code, _g.d.resource_report._employee_by_customer, _g.d.resource_report._employee_by_invoice };
                this._addComboBox(5, 0, _g.d.resource_report._order_by, true, __data, true);
            }
            else if (_cheque_cardEnumration.Detail_Cheque_by_Date_import.ToString().Equals(__page))
            {
                //  รายงานรายละเอียดเช็ครับ-ตามวันที่รับ
                this._maxColumn = 4;
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date_import_cheque, 2, true, true);
                this._addDateBox(0, 2, 1, 0, _g.d.resource_report._to_date_import_cheque, 2, true, true);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 2, 2, 1, true, false, true);
                this._addTextBox(1, 2, 1, 0, _g.d.resource_report._to_docno, 2, 2, 1, true, false, true);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_ar, 2, 2, 1, true, false, true);
                this._addTextBox(2, 2, 1, 0, _g.d.resource_report._to_ar, 2, 2, 1, true, false, true);

                string[] __data = { _g.d.resource_report._all, _g.d.resource_report._cheque_in_hand, _g.d.resource_report._cheque_deposit_bank, _g.d.resource_report._cheque_pass, _g.d.resource_report._cheque_return, _g.d.resource_report._cheque_cancel, _g.d.resource_report._sale_fall_cheque, _g.d.resource_report._cheque_return_bank };
                this._addComboBox(3, 0, _g.d.resource_report._select_status_cheque, true, __data, true);  //  เลือกสถานเช็ครับ
            }
        }
    }
}







