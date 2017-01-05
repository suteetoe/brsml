using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPReport.condition
{
    public partial class _condition_screen : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchDataFull = new MyLib._searchDataFull();
        string _searchName = "";
        string _searchField = "";
        TextBox _searchTextBox;
        string _page;
        SMLERPARAPInfo._apArConditionEnum _mode;
        string _custCodeFullFieldName = "";
        string _custCodeFieldName = "";
        string _custBeginFieldName = "";
        string _custEndFieldName = "";

        public _condition_screen(SMLERPARAPInfo._apArConditionEnum mode)
        {
            this._mode = mode;
            this._init(mode.ToString());
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_screen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_screen__textBoxChanged);
        }

        private void _init(string __page)
        {
            switch (SMLERPARAPInfo._apAr._apArCheck(this._mode))
            {
                case SMLERPARAPInfo._apArEnum.ลูกหนี้:
                    this._searchName = _g.g._search_screen_ar;
                    this._custCodeFullFieldName = _g.d.ar_customer._table + "." + _g.d.ar_customer._code;
                    this._custCodeFieldName = _g.d.ar_customer._code;
                    this._custBeginFieldName = _g.d.ap_ar_resource._ar_code_begin;
                    this._custEndFieldName = _g.d.ap_ar_resource._ar_code_end;
                    break;
                case SMLERPARAPInfo._apArEnum.เจ้าหนี้:
                    this._custCodeFullFieldName = _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code;
                    this._searchName = _g.g._search_screen_ap;
                    this._custCodeFieldName = _g.d.ap_supplier._code;
                    this._custBeginFieldName = _g.d.ap_ar_resource._ap_code_begin;
                    this._custEndFieldName = _g.d.ap_ar_resource._ap_code_end;
                    break;
            }
            this._table_name = _g.d.resource_report._table;
            DateTime __today = DateTime.Now;
            this._page = __page;
            this._maxColumn = 2;
            if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.debtor.ToString()))
            {
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_setup.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_increase.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_decrease.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_setup.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_increase.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_decrease.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_setup_cancel.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_increase_cancel.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_decrease_cancel.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_setup_cancel.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_increase_cancel.ToString()) ||
                __page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_decrease_cancel.ToString()))
            {
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._process_date, 1, true, false);
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                //this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_project, 1, 20, 1, true, false);
                //this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_project, 1, 20, 1, true, false);
                //this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_job, 1, 20, 1, true, false);
                //this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_job, 1, 20, 1, true, false);
                //this._addTextBox(5, 0, 1, 0, _g.d.resource_report._from_department, 1, 20, 1, true, false);
                //this._addTextBox(5, 1, 1, 0, _g.d.resource_report._to_department, 1, 20, 1, true, false);
                //this._addTextBox(6, 0, 1, 0, _g.d.resource_report._from_group, 1, 20, 1, true, false);
                //this._addTextBox(6, 1, 1, 0, _g.d.resource_report._to_group, 1, 20, 1, true, false);
                //this._addTextBox(7, 0, 1, 0, _g.d.resource_report._from_amount, 1, 20, 1, true, false);
                //this._addTextBox(7, 1, 1, 0, _g.d.resource_report._to_amount, 1, 20, 1, true, false);
                this._setDataDate(_g.d.resource_report._process_date, MyLib._myGlobal._workingDate);
                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.billing.ToString()) || __page.Equals(SMLERPARAPInfo._apArConditionEnum.receipt.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                this._addCheckBox(2, 0, _g.d.resource_report._display_detail, false, true, true);
                this._addCheckBox(3, 0, _g.d.resource_report._show_cancel_document, false, true, true);
                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.billing_cancel.ToString()) || __page.Equals(SMLERPARAPInfo._apArConditionEnum.receipt_cancel.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                //this._addCheckBox(2, 0, _g.d.resource_report._display_detail, false, true, true);
                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.receipt_temp.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 20, 1, true, false);
                //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._from_sale_person, 1, 20, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.debt_cut_off.ToString()))
            {
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้.ToString()))
            {
                //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._annual_period, 1, 20, 2, true, false);
                //this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                //this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                //this._addCheckBox(2, 0, _g.d.resource_report._not_total_lift_zero, false, true);
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_สถานะเจ้าหนี้.ToString()))
            {
                //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._annual_period, 1, 20, 2, true, false);
                //this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                //this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                //this._addCheckBox(2, 0, _g.d.resource_report._not_total_lift_zero, false, true);
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.absolute_period_debt_remain.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, "ประจำวันที่", 1, true, false);
                this._addCheckBox(1, 0, "แสดงเฉพาะลูกค้าที่มียอดหนี้", false, true);
                this._addLabel(2, 0, "", "ช่วงวันที่เกินกำหนด", "");
                this._addTextBox(3, 0, 1, 0, "ช่วงที่1", 1, 3, 0, true, false);
                this._addTextBox(3, 1, 1, 0, "ช่วงที่1ถึง", 1, 3, 0, true, false, false);
                this._addTextBox(4, 0, 1, 0, "ช่วงที่2", 1, 3, 0, true, false);
                this._addTextBox(4, 1, 1, 0, "ช่วงที่2ถึง", 1, 3, 0, true, false, false);
                this._addTextBox(5, 0, 1, 0, "ช่วงที่3", 1, 3, 0, true, false);
                this._addTextBox(5, 1, 1, 0, "ช่วงที่3ถึง", 1, 3, 0, true, false, false);
                this._addTextBox(6, 0, 1, 0, "ช่วงที่4", 1, 3, 0, true, false);
                this._addTextBox(6, 1, 1, 0, "ช่วงที่4ถึง", 1, 3, 0, true, false, false);
                this._addTextBox(7, 0, 1, 0, "ช่วงที่5เกินกว่า", 1, 3, 0, true, false);
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_เคลื่อนไหว.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                this._addCheckBox(1, 0, _g.d.resource_report._new_page_by_ar_code, false, true);
                this._addCheckBox(1, 1, _g.d.resource_report._movement_only, false, true, true);
                //
                this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_เคลื่อนไหว.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);
                this._addCheckBox(1, 0, _g.d.resource_report._new_page_by_ar_code, false, true);
                this._addCheckBox(1, 1, _g.d.resource_report._movement_only, false, true, true);
                //
                this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.absolute_check_balance.ToString()))
            {
                this._addNumberBox(1, 0, 1, 0, "จากยอดวงเงินเครดิต", 1, 0, true);
                this._addNumberBox(1, 1, 1, 0, "ถึงยอดวงเงินเครดิต", 1, 0, true);
                this._addNumberBox(2, 0, 1, 0, "จากยอดวงเงินคงเหลือ", 1, 0, true);
                this._addNumberBox(2, 1, 1, 0, "ถึงยอดวงเงินคงเหลือ", 1, 0, true);
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.absolute_invoice_remain_pay.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._balance_date, 1, true, false);
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_bill, 1, true);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_bill, 1, true);
                this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_due_date, 1, true);
                this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_due_date, 1, true);
                this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
            }
            else if (this._mode == SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้ ||
                this._mode == SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร ||
                this._mode == SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยกลูกหนี้_แยกเอกสาร || 
                this._mode == SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้)
            {
                this._table_name = _g.d.ap_ar_resource._table;
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m00");
                this._addTextBox(0, 0, 0, 0, this._custBeginFieldName, 1, 25, 1, true, false, false);
                this._addTextBox(0, 1, 0, 0, this._custEndFieldName, 1, 25, 1, true, false, false);
                this._addDateBox(1, 0, 0, 0, _g.d.ap_ar_resource._date_end, 1, true);
                this._addNumberBox(2, 0, 1, 0, _g.d.ap_ar_resource._term_1_begin, 1, 2, true, __formatNumber);
                this._addNumberBox(2, 1, 1, 0, _g.d.ap_ar_resource._term_1_end, 1, 2, true, __formatNumber);
                this._addNumberBox(3, 0, 1, 0, _g.d.ap_ar_resource._term_2_begin, 1, 2, true, __formatNumber);
                this._addNumberBox(3, 1, 1, 0, _g.d.ap_ar_resource._term_2_end, 1, 2, true, __formatNumber);
                this._addNumberBox(4, 0, 1, 0, _g.d.ap_ar_resource._term_3_begin, 1, 2, true, __formatNumber);
                this._addNumberBox(4, 1, 1, 0, _g.d.ap_ar_resource._term_3_end, 1, 2, true, __formatNumber);
                this._addNumberBox(5, 0, 1, 0, _g.d.ap_ar_resource._term_4_begin, 1, 2, true, __formatNumber);
                this._addNumberBox(5, 1, 1, 0, _g.d.ap_ar_resource._term_4_end, 1, 2, true, __formatNumber);
                MyLib._myGroupBox __dueDate = this._addGroupBox(6, 0, 1, 2, 2, _g.d.ap_ar_resource._due_date_select, true);
                this._addRadioButtonOnGroupBox(0, 0, __dueDate, _g.d.ap_ar_resource._by_due_date, 0, true);
                this._addRadioButtonOnGroupBox(0, 1, __dueDate, _g.d.ap_ar_resource._by_doc_date, 1, false);
                //
                this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_screen__textBoxChanged);
                //
                this._setDataDate(_g.d.ap_ar_resource._date_end, new DateTime(__today.Year, __today.Month, __today.Day));
                this._setDataNumber(_g.d.ap_ar_resource._term_1_begin, 1.0M);
                this._setDataNumber(_g.d.ap_ar_resource._term_1_end, 30.0M);
                this._setDataNumber(_g.d.ap_ar_resource._term_2_begin, 31.0M);
                this._setDataNumber(_g.d.ap_ar_resource._term_2_end, 60.0M);
                this._setDataNumber(_g.d.ap_ar_resource._term_3_begin, 61.0M);
                this._setDataNumber(_g.d.ap_ar_resource._term_3_end, 90.0M);
                this._setDataNumber(_g.d.ap_ar_resource._term_4_begin, 91.0M);
                this._setDataNumber(_g.d.ap_ar_resource._term_4_end, 120.0M);
                this._getControl(_g.d.ap_ar_resource._term_2_begin).Enabled = false;
                this._getControl(_g.d.ap_ar_resource._term_3_begin).Enabled = false;
                this._getControl(_g.d.ap_ar_resource._term_4_begin).Enabled = false;
            }
            else if (__page.Equals(SMLERPARAPInfo._apArConditionEnum.receipt_daily.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
        }

        void _condition_screen__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchField = __getControl._name.ToLower();
            string label_name = __getControl._labelName;

            string __searchTextNew = this._search_screen_neme(this._searchName);
            if (!this._searchDataFull._name.Equals(__searchTextNew.ToLower()))
            {
                this._searchDataFull = new MyLib._searchDataFull();
                this._searchDataFull._name = __searchTextNew;
                this._searchDataFull._dataList._loadViewFormat(this._searchDataFull._name, MyLib._myGlobal._userSearchScreenGroup, false);
                //this._searchDataFull._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchDataFull__searchEnterKeyPress);
                this._searchDataFull._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchDataFull._dataList._refreshData();
            }

            if (this._searchName.Equals("from_docno") || this._searchName.Equals("to_docno"))
            {
                //this._searchDataFull._dataList._orderBy = _g.d.ap_ar_trans._doc_no;
                string __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_type, "2");
                if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_setup.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "93");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_setup_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "94");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_increase.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "95");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_increase_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "96");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_decrease.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "97");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_decrease_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "98");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_setup.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "99");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_setup_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "100");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_increase.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "101");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_increase_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "102");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_decrease.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "103");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_decrease_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "104");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.billing.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "35");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.receipt_temp.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "37");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.receipt.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "39");
                else if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.debt_cut_off.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "41");
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchDataFull, false, true, __where);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchDataFull, false);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _condition_screen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.resource_report._from_date) || name.Equals(_g.d.resource_report._to_date))
            {
                string __string_from_date = this._getDataStr(_g.d.resource_report._from_date);
                string __string_to_date = this._getDataStr(_g.d.resource_report._to_date);
                DateTime __dateTime_from_date = MyLib._myGlobal._convertDate(__string_from_date);
                DateTime __dateTime_to_date = MyLib._myGlobal._convertDate(__string_to_date);
                TimeSpan __timeSpan = __dateTime_to_date - __dateTime_from_date;
                if (__timeSpan.Days < 0)
                {
                    this._setDataDate(_g.d.resource_report._to_date, __dateTime_from_date);
                }
            }
            if (this._mode == SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้ || this._mode == SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้ || this._mode == SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร)
            {
                if (name.Equals(_g.d.ap_ar_resource._term_1_end))
                {
                    int __term_1_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_1_end);
                    this._setDataNumber(_g.d.ap_ar_resource._term_2_begin, __term_1_end + 1);
                }
                else
                    if (name.Equals(_g.d.ap_ar_resource._term_2_end))
                    {
                        int __term_2_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_2_end);
                        this._setDataNumber(_g.d.ap_ar_resource._term_3_begin, __term_2_end + 1);
                    }
                    else
                        if (name.Equals(_g.d.ap_ar_resource._term_3_end))
                        {
                            int __term_3_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_3_end);
                            this._setDataNumber(_g.d.ap_ar_resource._term_4_begin, __term_3_end + 1);
                        }
                        else
                            if (name.Equals(_g.d.ap_ar_resource._term_4_end))
                            {
                                int __term_4_end = (int)this._getDataNumber(_g.d.ap_ar_resource._term_4_end);
                                this._setDataNumber(_g.d.ap_ar_resource._term_5_end, __term_4_end);
                            }
            }
        }

        private string _search_screen_neme(string _name)
        {
            if (_name.ToLower().Equals("from_docno") || _name.ToLower().Equals("to_docno"))
            {
                if (this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_setup.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_increase.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_decrease.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_setup.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_increase.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_decrease.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_setup_cancel.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_increase_cancel.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.early_debt_decrease_cancel.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_setup_cancel.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_increase_cancel.ToString()) ||
                    this._page.Equals(SMLERPARAPInfo._apArConditionEnum.other_debt_decrease_cancel.ToString()))
                {
                    return _g.g._search_screen_ic_trans;
                }
                else
                {
                    return _g.g._screen_ap_trans;
                }
            }
            return _name;
        }

        private void _searchAll(string name, int row)
        {
            if (name.Length > 0)
            {
                string result = (string)this._searchDataFull._dataList._gridData._cellGet(row, 0);
                if (result.Length != 0)
                {
                    this._searchDataFull.Visible = false;
                    this._setDataStr(this._searchField, result, "", true);
                    //this._search(true);
                }
            }
        }
    }
}
