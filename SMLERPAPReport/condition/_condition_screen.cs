using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPReport.condition
{
    public partial class _condition_screen : MyLib._myScreen
    {
        MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        MyLib._searchDataFull _searchDataFull = new MyLib._searchDataFull();
        string _searchName = "";
        TextBox _searchTextBox;
        string _page;
        private _enum_screen_report_ap _controlTypeTemp;
        public _enum_screen_report_ap TransControlType
        {
            set
            {
                this._controlTypeTemp = value;
                this._init(value.ToString());
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        public _condition_screen()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_screen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_screen__textBoxChanged);
        }

        public void _init(string __page)
        {
            this._page = __page;
            this._maxColumn = 2;
            if (__page.Equals(_enum_screen_report_ap.เจ้าหนี้_รายละเอียดเจ้าหนี้.ToString()))
            {
            }
            else if (__page.Equals(_enum_screen_report_ap.early_debt_setup.ToString()) ||
                __page.Equals(_enum_screen_report_ap.early_debt_increase.ToString()) ||
                __page.Equals(_enum_screen_report_ap.early_debt_decrease.ToString()) ||
                __page.Equals(_enum_screen_report_ap.other_debt_setup.ToString()) ||
                __page.Equals(_enum_screen_report_ap.other_debt_increase.ToString()) ||
                __page.Equals(_enum_screen_report_ap.other_debt_decrease.ToString()) ||
                __page.Equals(_enum_screen_report_ap.early_debt_setup_cancel.ToString()) ||
                __page.Equals(_enum_screen_report_ap.early_debt_increase_cancel.ToString()) ||
                __page.Equals(_enum_screen_report_ap.early_debt_decrease_cancel.ToString()) ||
                __page.Equals(_enum_screen_report_ap.other_debt_setup_cancel.ToString()) ||
                __page.Equals(_enum_screen_report_ap.other_debt_increase_cancel.ToString()) ||
                __page.Equals(_enum_screen_report_ap.other_debt_decrease_cancel.ToString()))
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
            else if (__page.Equals(_enum_screen_report_ap.billing.ToString()) ||
                __page.Equals(_enum_screen_report_ap.payment.ToString()))
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
            else if (__page.Equals(_enum_screen_report_ap.billing_cancel.ToString()) ||
                __page.Equals(_enum_screen_report_ap.payment_cancel.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
                this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
                this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
                this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
                //this._addCheckBox(2, 0, _g.d.resource_report._display_detail, false, true, true);
                this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
                this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            }
            //else if (__page.Equals(_enum_screen_report_ap.prepare_payment.ToString()) ||
            //    __page.Equals(_enum_screen_report_ap.prepare_payment_cancel.ToString()) ||
            //    __page.Equals(_enum_screen_report_ap.debt_cut_off.ToString()) ||
            //    __page.Equals(_enum_screen_report_ap.debt_cut_off_cancel.ToString()))
            //{
            //    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_docdate, 1, true, false);
            //    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_docdate, 1, true, false);
            //    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 20, 1, true, false);
            //    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 20, 1, true, false);
            //    this._setDataDate(_g.d.resource_report._from_docdate, new DateTime(MyLib._myGlobal._workingDate.Year, MyLib._myGlobal._workingDate.Month, 1));
            //    this._setDataDate(_g.d.resource_report._to_docdate, MyLib._myGlobal._workingDate);
            //}
            else if (__page.Equals(_enum_screen_report_ap.absolute_movement.ToString()))
            {
            }
            else if (__page.Equals(_enum_screen_report_ap.absolute_status.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report._process_date, 1, true, false);
                this._setDataDate(_g.d.resource_report._process_date, MyLib._myGlobal._workingDate);
            }
            else if (__page.Equals(_enum_screen_report_ap.รายงานอายุเจ้าหนี้.ToString()))
            {
                this._addDateBox(0, 0, 1, 0, _g.d.resource_report_column._process_date, 1, true, false);
            }
            else if (__page.Equals(_enum_screen_report_ap.absolute_not_movement.ToString()))
            {
            }
            else if (__page.Equals(_enum_screen_report_ap.payment_daily.ToString()))
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
            this._searchName = __getControl._name.ToLower();
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
                string __where = "";
                if (this._page.Equals(_enum_screen_report_ap.early_debt_setup.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "81");
                else if (this._page.Equals(_enum_screen_report_ap.early_debt_setup_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "82");
                else if (this._page.Equals(_enum_screen_report_ap.early_debt_increase.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "83");
                else if (this._page.Equals(_enum_screen_report_ap.early_debt_increase_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "84");
                else if (this._page.Equals(_enum_screen_report_ap.early_debt_decrease.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "85");
                else if (this._page.Equals(_enum_screen_report_ap.early_debt_decrease_cancel.ToString())) __where = String.Format(" and {0}={1}", _g.d.ic_trans._trans_flag, "86");
                else if (this._page.Equals(_enum_screen_report_ap.other_debt_setup.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "87");
                else if (this._page.Equals(_enum_screen_report_ap.other_debt_setup_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "88");
                else if (this._page.Equals(_enum_screen_report_ap.other_debt_increase.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "89");
                else if (this._page.Equals(_enum_screen_report_ap.other_debt_increase_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "90");
                else if (this._page.Equals(_enum_screen_report_ap.other_debt_decrease.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "91");
                else if (this._page.Equals(_enum_screen_report_ap.other_debt_decrease_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ic_trans._trans_flag, "92");
                else if (this._page.Equals(_enum_screen_report_ap.billing.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "13");
                else if (this._page.Equals(_enum_screen_report_ap.billing_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "14");
                else if (this._page.Equals(_enum_screen_report_ap.prepare_payment.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "15");
                else if (this._page.Equals(_enum_screen_report_ap.prepare_payment_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "16");
                else if (this._page.Equals(_enum_screen_report_ap.payment.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "19");
                else if (this._page.Equals(_enum_screen_report_ap.payment_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "20");
                else if (this._page.Equals(_enum_screen_report_ap.debt_cut_off.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "21");
                else if (this._page.Equals(_enum_screen_report_ap.debt_cut_off_cancel.ToString())) __where = String.Format("{0}={1}", _g.d.ap_ar_trans._trans_flag, "22");
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
            if (name.Equals(_g.d.resource_report._from_date) ||
                name.Equals(_g.d.resource_report._to_date))
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
        }

        private string _search_screen_neme(string _name)
        {
            if (_name.ToLower().Equals("from_docno") || _name.ToLower().Equals("to_docno"))
            {
                if (this._page.Equals(_enum_screen_report_ap.early_debt_setup.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.early_debt_increase.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.early_debt_decrease.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.other_debt_setup.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.other_debt_increase.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.other_debt_decrease.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.early_debt_setup_cancel.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.early_debt_increase_cancel.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.early_debt_decrease_cancel.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.other_debt_setup_cancel.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.other_debt_increase_cancel.ToString()) ||
                    this._page.Equals(_enum_screen_report_ap.other_debt_decrease_cancel.ToString()))
                {
                    return _g.g._search_screen_ic_trans;
                }
                else
                {
                    return _g.g._screen_ap_trans;
                }
            }
            return "";
        }

        private void _searchAll(string name, int row)
        {
            if (name.Length > 0)
            {
                string result = (string)this._searchDataFull._dataList._gridData._cellGet(row, 1);
                if (result.Length != 0)
                {
                    this._searchDataFull.Visible = false;
                    this._setDataStr(this._searchName, result, "", true);
                    //this._search(true);
                }
            }
        }
    }

    
}
