using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _screen_condition_ar_new : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;

        public _screen_condition_ar_new()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screen_condition_ar_new__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screen_condition_ar_new__textBoxChanged);
        }

        public void _init(string __page)
        {
            this._maxColumn = 2;
            if (__page.Equals(_enum_screen_report_ar.ar_detail.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._balance_date, 1, true, false);
                this._setDataDate(_g.d.resource_report._balance_date, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_ar.ar_billing.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true, false);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_bill_of_landing_place, 1, 20, 1, true, false);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_bill_of_landing_place, 1, 20, 1, true, false);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 20, 1, true, false);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_sale_person, 1, 20, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_invoice_date, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_invoice_date, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_ar.ar_receipt_temp.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 20, 1, true, false);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._from_sale_person, 1, 20, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_invoice_date, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_invoice_date, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_ar.ar_receipt.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true, false);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 20, 1, true, false);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_sale_person, 1, 20, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_invoice_date, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_invoice_date, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_ar.ar_debt_cut_off.ToString()))
            {
                this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_invoice_date, 1, true, false);
                this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_invoice_date, 1, true, false);
                this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 20, 1, true, false);
                this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_sale_person, 1, 20, 1, true, false);
                this._setDataDate(_g.d.resource_report._from_invoice_date, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_invoice_date, MyLib._myGlobal._workingDate);
            }
        }

        void _screen_condition_ar_new__textBoxSearch(object sender)
        {

        }

        void _screen_condition_ar_new__textBoxChanged(object sender, string name)
        {

        }
    }
}