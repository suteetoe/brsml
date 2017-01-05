using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SINGHAReport
{
    public class _conditionScreenTop : MyLib._myScreen
    {
        _singhaReportEnum _mode;

        ArrayList _searchScreenMasterList = new System.Collections.ArrayList();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;

        string _old_filed_name = "";
        string _searchName = "";
        TextBox _searchTextBox;
        SMLERPGlobal._searchProperties _searchScreenProperties = new SMLERPGlobal._searchProperties();

        public _conditionScreenTop()
        {
            this._textBoxSearch += _conditionScreenTop__textBoxSearch;
        }

        private void _conditionScreenTop__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Boolean __found = false;
                this._old_filed_name = __getControl._name.ToLower();

                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                    MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                    if (__getControl._iconNumber == 4)
                    {
                        __searchObject.WindowState = FormWindowState.Maximized;
                    }
                    this._search_data_full_buffer.Add(__searchObject);
                    // __searchObject._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                }

                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                if (__getControl.textBox.Text == "")
                {
                    this._search_data_full_pointer._dataList._searchText.TextBox.Text = "";
                }
            }

            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._label._field_name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            _searchScreenMasterList.Clear();

            try
            {
                string __extraWhere = "";
                Boolean __multiSelected = false;
                //_searchScreenMasterList = _searchScreenProperties._setColumnSearch(this._searchName, 0, "", "");

                // ผังบัญชี
                if (this._searchName.Equals(_g.d.resource_report._from_project_account_code) || this._searchName.Equals(_g.d.resource_report._to_project_account_code))
                {
                    _searchScreenMasterList.Clear();
                    _searchScreenMasterList.Add(_g.g._search_screen_gl_chart_of_account);
                    _searchScreenMasterList.Add(_g.d.gl_chart_of_account._table);
                    _searchScreenMasterList.Add(_g.d.gl_chart_of_account._active_status + "=1");

                }
                else if (this._mode == _singhaReportEnum.การ์ดสินค้า && this._searchName.Equals(_g.d.resource_report._item_code))
                {
                    __multiSelected = true;
                    _searchScreenMasterList.Clear();
                    _searchScreenMasterList.Add(_g.g._search_screen_ic_inventory);
                    _searchScreenMasterList.Add(_g.d.ic_inventory._table);
                    //_searchScreenMasterList.Add(_g.d.ic_inventory._item_status + "=1");

                }
                /*else if (this._searchName.Equals(_g.d.resource_report_vat._doc_type))
                {
                    _searchScreenMasterList.Clear();
                    _searchScreenMasterList.Add(_g.g._screen_erp_doc_format);
                    _searchScreenMasterList.Add(_g.d.erp_doc_format._table);
                }*/
                // doc_type


                if (_searchScreenMasterList.Count > 0)
                {
                    if (this._search_data_full_pointer._name.Equals(_searchScreenMasterList[0].ToString().ToLower()) == false)
                    {
                        if (this._search_data_full_pointer._name.Length == 0)
                        {
                            this._search_data_full_pointer._name = _searchScreenMasterList[0].ToString();
                            this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน

                            if (__multiSelected)
                            {
                                this._search_data_full_pointer._dataList._multiSelect = true;
                                this._search_data_full_pointer._dataList._selectSuccessButton.Click += _selectSuccessButton_Click;
                            }
                            else
                            {
                                this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                                this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                                //
                                this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                                this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);

                            }
                        }
                    }
                    __extraWhere = (_searchScreenMasterList.Count == 3) ? _searchScreenMasterList[2].ToString() : "";
                    MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __extraWhere);
                    if (__getControl._iconNumber == 1)
                    {
                        __getControl.Focus();
                        __getControl.textBox.Focus();
                    }
                    else
                    {
                        this._search_data_full_pointer.Focus();
                        this._search_data_full_pointer._firstFocus();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void _selectSuccessButton_Click(object sender, EventArgs e)
        {
            MyLib._myDataList __getParent1 = _search_data_full_pointer._dataList;
            string __getSelect = __getParent1._selectList();
            this._search_data_full_pointer.Visible = false;
            this._setDataStr(this._searchName, __getSelect);
            SendKeys.Send("{ENTER}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        void _searchAll(string screenName, int row)
        {
            int __columnNumber = 0;
            if (screenName.Equals(_g.g._search_screen_gl_chart_of_account))
            {
                __columnNumber = this._search_data_full_pointer._dataList._gridData._findColumnByName(_g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code);
            }

            string __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._search_data_full_pointer.Visible = false;
                this._setDataStr(this._searchName, __result);
            }
        }

        public void _init(_singhaReportEnum mode)
        {
            this._mode = mode;
            int __row = 0;

            switch (mode)
            {
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    this._table_name = _g.d.resource_report._table;
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_project_account_code, 1, 0, 4, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_project_account_code, 1, 0, 4, true, false);

                    this._table_name = _g.d.resource_report_vat._table;
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report_vat._doc_type, 1, 0, 1, true, false);

                    this._textBoxSearch += _conditionScreenTop__textBoxSearch1;

                    break;
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                    this._table_name = _g.d.resource_report._table;
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_project_account_code, 1, 0, 4, true, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_project_account_code, 1, 0, 4, true, true);
                    break;
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                    {
                        this._table_name = _g.d.resource_report_vat._table;
                        this._addComboBox(__row, 0, _g.d.resource_report_vat._vat_month, true, _g.g._monthName(), false);
                        this._addTextBox(__row++, 1, _g.d.resource_report_vat._vat_year, 1);
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report_vat._date_begin, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report_vat._date_end, 1, true, true);
                        this._addTextBox(__row, 0, 3, _g.d.resource_report_vat._address, 2, 2);

                        this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_top__textBoxChanged);

                        __row += 3;

                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:
                    {
                        this._table_name = _g.d.resource_report_vat._table;
                        this._addComboBox(__row, 0, _g.d.resource_report_vat._vat_month, true, _g.g._monthName(), false);
                        this._addTextBox(__row++, 1, _g.d.resource_report_vat._vat_year, 1);
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report_vat._date_begin, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report_vat._date_end, 1, true, true);
                        this._addTextBox(__row, 0, 3, _g.d.resource_report_vat._address, 2, 2);
                        this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_top__textBoxChanged);

                        __row += 3;

                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                    {
                        this._table_name = _g.d.resource_report_vat._table;
                        string[] _vatBuyType = new string[] { _g.d.resource_report_vat._report_vat_all, _g.d.resource_report_vat._report_vat_only, _g.d.resource_report_vat._report_vat_no, _g.d.resource_report_vat._report_vat_buy_3 };
                        this._addComboBox(__row++, 0, _g.d.resource_report_vat._report_vat_type, true, _vatBuyType, true);
                        this._addComboBox(__row, 0, _g.d.resource_report_vat._vat_month, true, _g.g._monthName(), true);
                        this._addTextBox(__row++, 1, _g.d.resource_report_vat._vat_year, 1);
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report_vat._date_begin, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report_vat._date_end, 1, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.resource_report_vat._from_doc_type, 1, 0, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.resource_report_vat._to_doc_type, 1, 0, 1, true, false);
                        this._addComboBox(__row, 0, _g.d.resource_report_vat._sort_by, true, new string[] { _g.d.resource_report_vat._order_by_doc_date, _g.d.resource_report_vat._order_by_vat_date, _g.d.resource_report_vat._order_by_doc_no, _g.d.resource_report_vat._order_by_vat_no }, true);
                        this._table_name = "";
                        this._addTextBox(__row++, 1, 1, 0, "book_code", 1, 0, 1, true, false, true, true, false, _g.d.gl_journal._table + "." + _g.d.gl_journal._book_code);
                        this._table_name = _g.d.resource_report_vat._table;
                        this._addTextBox(__row, 0, 3, _g.d.resource_report_vat._address, 2, 2);

                        DateTime __today = DateTime.Now;
                        //this._setDataDate(_g.d.resource_report_vat._date_begin, new DateTime(__today.Year, __today.Month, 1));
                        //this._setDataDate(_g.d.resource_report_vat._date_end, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));

                        this._setDataStr(_g.d.resource_report_vat._vat_year, (__today.Year + MyLib._myGlobal._year_add).ToString());
                        this._setComboBox(_g.d.resource_report_vat._vat_month, __today.Month - 1);

                        this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_top__textBoxChanged);
                        this._comboBoxSelectIndexChanged += _conditionScreenTop__comboBoxSelectIndexChanged;

                        __row += 3;
                        this._textBoxSearch += _conditionScreenTop__textBoxSearch1;

                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    {
                        this._table_name = _g.d.resource_report_vat._table;
                        string[] _vatBuyType = new string[] { _g.d.resource_report_vat._report_vat_all, _g.d.resource_report_vat._report_vat_only, _g.d.resource_report_vat._report_vat_no, _g.d.resource_report_vat._tax_zero };
                        this._addComboBox(__row++, 0, _g.d.resource_report_vat._report_vat_type, true, _vatBuyType, true);
                        this._addComboBox(__row, 0, _g.d.resource_report_vat._vat_month, true, _g.g._monthName(), true);
                        this._addTextBox(__row++, 1, _g.d.resource_report_vat._vat_year, 1);
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report_vat._date_begin, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report_vat._date_end, 1, true, true);
                        //string[] _vatTransType = new string[] { "ทั้งหมด", "ขาย", "เพิ่มหนี้", "ลดหนี้" };
                        //this._addComboBox(__row++, 0, _g.d.resource_report_vat._vat_group, true, _vatTransType, true);
                        this._addTextBox(__row, 0, 1, 1, _g.d.resource_report_vat._vat_group, 1, 10, 1, true, false);
                        this._addCheckBox(__row++, 1, _g.d.resource_report_vat._show_vat_amount_only, false, true, true);
                        this._addComboBox(__row++, 0, _g.d.resource_report_vat._sort_by, true, new string[] { _g.d.resource_report_vat._order_by_doc_date, _g.d.resource_report_vat._order_by_vat_date, _g.d.resource_report_vat._order_by_doc_no, _g.d.resource_report_vat._order_by_vat_no }, true);

                        //this._addTextBox(__row, 0, 3, _g.d.resource_report_vat._address, 2, 2);
                        //__row += 3;

                        DateTime __today = DateTime.Now;
                        //this._setDataDate(_g.d.resource_report_vat._date_begin, new DateTime(__today.Year, __today.Month, 1));
                        //this._setDataDate(_g.d.resource_report_vat._date_end, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));

                        this._setDataStr(_g.d.resource_report_vat._vat_year, (__today.Year + MyLib._myGlobal._year_add).ToString());
                        this._setComboBox(_g.d.resource_report_vat._vat_month, __today.Month - 1);

                        this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_top__textBoxChanged);
                        this._comboBoxSelectIndexChanged += _conditionScreenTop__comboBoxSelectIndexChanged;
                        this._textBoxSearch += _conditionScreenTop__textBoxSearch1;


                    }
                    break;
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    {
                        this._table_name = _g.d.resource_report._table;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false);

                        this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_ap, 1, 0, 1, true, false);
                        this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_ap, 1, 0, 1, true, false);

                        DateTime __today = DateTime.Now;
                        this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                        this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));

                        this._textBoxSearch += _conditionScreenTop__textBoxSearch1;

                    }
                    break;
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ลูกหนี้คงค้าง:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                    {
                        this._table_name = _g.d.ap_ar_resource._table;
                        __row = 0;

                        this._addDateBox(__row, 0, 1, 0, _g.d.ap_ar_resource._date_begin, 1, true, false);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ap_ar_resource._date_end, 1, true, false);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ap_ar_resource._ar_code_begin, 1, 0, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_resource._ar_code_end, 1, 0, 1, true, false);

                        if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
                        {
                            this._addNumberBox(__row, 0, 1, 1, _g.d.ap_ar_resource._from_credit_day, 1, 0, true);
                            this._addNumberBox(__row++, 1, 1, 1, _g.d.ap_ar_resource._to_credit_day, 1, 0, true);
                        }
                        DateTime __today = DateTime.Now;

                        this._setDataDate(_g.d.ap_ar_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
                        this._setDataDate(_g.d.ap_ar_resource._date_end, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));

                        string __formatNumber = MyLib._myGlobal._getFormatNumber("m00");

                        MyLib._myGroupBox __dueDate = this._addGroupBox(__row, 0, 1, 2, 2, _g.d.ap_ar_resource._due_date_select, true);
                        this._addRadioButtonOnGroupBox(0, 0, __dueDate, _g.d.ap_ar_resource._by_due_date, 0, true);
                        this._addRadioButtonOnGroupBox(0, 1, __dueDate, _g.d.ap_ar_resource._by_doc_date, 1, false);
                        __row += 2;
                        if (this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ || this._mode == _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต)
                        {
                            this._addCheckBox(__row++, 0, _g.d.ap_ar_resource._status, false, false, false, "");
                            ((MyLib._myCheckBox)this._getControl(_g.d.ap_ar_resource._status)).Text = "เฉพาะลูกค้าปิดสถานะ";

                            this._addDateBox(__row, 0, 1, 0, _g.d.ap_ar_resource._close_credit_date_from, 1, true, true);
                            this._addDateBox(__row++, 1, 1, 0, _g.d.ap_ar_resource._close_credit_date_to, 1, true, true);

                            //((MyLib._myDateBox)this._getControl("close_credit_date_from")).Text = "เฉพาะลูกค้าปิดสถานะ";
                            //((MyLib._myDateBox)this._getControl("close_credit_date_to")).Text = "เฉพาะลูกค้าปิดสถานะ";

                        }

                        /*
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
                        */
                        //
                        // this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_screen__textBoxChanged);
                        //
                        /*
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
                        */

                        this._textBoxSearch += _conditionScreenTop__textBoxSearch1;
                    }
                    break;
                case _singhaReportEnum.GL_ข้อมูลรายวัน:
                    {
                        int __line = 0;
                        this._table_name = _g.d.gl_resource._table;
                        this._maxColumn = 2;
                        this._addDateBox(__line, 0, 1, 0, _g.d.gl_resource._date_begin, 1, true, true);
                        this._addDateBox(__line++, 1, 1, 0, _g.d.gl_resource._date_end, 1, true, true);

                        this._addTextBox(__line, 0, 1, 0, _g.d.gl_resource._doc_begin, 1, 0, 0, true, false);
                        this._addTextBox(__line++, 1, 1, 0, _g.d.gl_resource._doc_end, 1, 0, 0, true, false);

                        this._addCheckBox(__line++, 0, _g.d.gl_resource._total_end_date, true, false, true);

                        MyLib._myGroupBox __group = this._addGroupBox(__line++, 0, 2, 1, 1, _g.d.gl_resource._sort_by, false);
                        this._addRadioButtonOnGroupBox(0, 0, __group, _g.d.gl_resource._sort_by_date, 1, true);
                        this._addRadioButtonOnGroupBox(1, 0, __group, _g.d.gl_resource._sort_by_doc_no, 2, false);

                        DateTime __today = DateTime.Now;

                        this._setDataDate(_g.d.gl_resource._date_begin, new DateTime(__today.Year, __today.Month, 1));
                        this._setDataDate(_g.d.gl_resource._date_end, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));

                    }
                    break;
                case _singhaReportEnum.รายงานการตัดเช็ค:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.resource_report._table;
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.resource_report._from_ar, 1, 0, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.resource_report._to_ar, 1, 0, 1, true, false);

                        MyLib._myGroupBox __group = this._addGroupBox(__row++, 0, 2, 1, 1, _g.d.resource_report._show_only, false);
                        this._addRadioButtonOnGroupBox(0, 0, __group, _g.d.resource_report._all, 1, true);
                        this._addRadioButtonOnGroupBox(1, 0, __group, _g.d.resource_report._show_balance_only, 2, false);

                        DateTime __today = DateTime.Now;

                        this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                        this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));


                    }
                    break;
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.resource_report._table;
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.resource_report._from_sale_person, 1, 0, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.resource_report._to_sale_person, 1, 0, 1, true, false);

                        this._addCheckBox(__row++, 0, _g.d.resource_report._display_detail, true, false, true);

                        this._setCheckBox(_g.d.resource_report._display_detail, true);

                        DateTime __today = DateTime.Now;

                        this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                        this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));

                        this._textBoxSearch += _conditionScreenTop__textBoxSearch1;

                    }
                    break;
                case _singhaReportEnum.การ์ดสินค้า:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.resource_report._table;
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.resource_report._item_code, 2, 0, 4, true, false);

                        DateTime __today = DateTime.Now;

                        this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                        this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));
                        this._textBoxSearch += _conditionScreenTop__textBoxSearch1;
                    }
                    break;
                case _singhaReportEnum.BankStatement:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.cb_resource._table;
                        this._addDateBox(__row, 0, 1, 0, _g.d.cb_resource._date_from, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.cb_resource._date_to, 1, true, true);

                        DateTime __today = DateTime.Now;

                        this._setDataDate(_g.d.cb_resource._date_from, new DateTime(__today.Year, __today.Month, 1));
                        this._setDataDate(_g.d.cb_resource._date_to, new DateTime(__today.Year, __today.Month, 1).AddMonths(1).AddDays(-1));

                    }
                    break;
                case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.ic_resource._table;
                        this._addDateBox(__row++, 0, 1, 0, _g.d.ic_resource._date_end, 1, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_resource._warehouse, 1, 0, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_resource._location, 1, 0, 1, true, false);

                        DateTime __today = DateTime.Now;

                        this._setDataDate(_g.d.ic_resource._date_end, __today);
                        this._textBoxSearch += _conditionScreenTop__textBoxSearch1;
                    }
                    break;
                case _singhaReportEnum.รายงานค่าใช้จ่ายอื่น:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.resource_report._table;
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        this._addCheckBox(__row++, 0, _g.d.resource_report._display_detail, true, false, true);

                        this._addTextBox(__row, 0, 1, 0, _g.d.resource_report._from_branch_code, 1, 0, 4, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.resource_report._to_branch_code, 1, 0, 4, true, false);
                    }
                    break;
                case _singhaReportEnum.รายงานยอดขายรายเดือนตามกลุ่มสินค้า_เทียบปี:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.resource_report._table;
                        this._addComboBox(__row, 0, _g.d.resource_report._monthly, true, new string[] { _g.d.resource_report._month_jan, _g.d.resource_report._month_feb, _g.d.resource_report._month_mar, _g.d.resource_report._month_apr, _g.d.resource_report._month_may, _g.d.resource_report._month_jun, _g.d.resource_report._month_jul, _g.d.resource_report._month_aug, _g.d.resource_report._month_sep, _g.d.resource_report._month_oct, _g.d.resource_report._month_nov, _g.d.resource_report._month_dec }, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.resource_report._year, 1, 0, 0, true, false);


                        // add year add 
                        DateTime __today = DateTime.Now;
                        int __startYear = __today.Year + MyLib._myGlobal._year_add;

                        this._setDataStr(_g.d.resource_report._year, __startYear.ToString());
                    }
                    break;
            }

            this.Invalidate();
            this.ResumeLayout();

            this._refresh();
            this._focusFirst();

        }

        MyLib._myTextBox _searchTextbox;
        MyLib._searchDataFull _searchControl = null;

        private void _conditionScreenTop__textBoxSearch1(object sender)
        {
            //_searchTextbox = (MyLib._myTextBox)sender;

            string name = ((MyLib._myTextBox)sender)._name;

            switch (this._mode)
            {
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                case _singhaReportEnum.ลูกหนี้คงค้าง:
                    {
                        string label_name = ((MyLib._myTextBox)sender)._labelName;

                        if (name == _g.d.ap_ar_resource._ar_code_begin || name == _g.d.ap_ar_resource._ar_code_end)
                        {

                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            //__searchControl._dataList._multiSelect = true;
                            _searchControl._name = _g.g._search_screen_ar;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;

                            MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);
                            //__searchControl.StartPosition = FormStartPosition.CenterScreen;
                            //__searchControl.ShowDialog();
                        }
                    }
                    break;

                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    {
                        string label_name = ((MyLib._myTextBox)sender)._labelName;

                        if (name == _g.d.resource_report._from_ap || name == _g.d.resource_report._to_ap)
                        {

                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            //__searchControl._dataList._multiSelect = true;
                            _searchControl._name = _g.g._search_screen_ap;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;

                            MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);
                            //__searchControl.StartPosition = FormStartPosition.CenterScreen;
                            //__searchControl.ShowDialog();
                        }
                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    {
                        string label_name = ((MyLib._myTextBox)sender)._labelName;

                        if (name == _g.d.resource_report_vat._vat_group)
                        {

                            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            _searchControl._dataList._multiSelect = true;
                            _searchControl._name = _g.g._search_screen_gl_tax_group;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            //__searchControl._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                            //__searchControl._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchVatGroup__searchEnterKeyPress);

                            //MyLib._myGlobal._startSearchBox(__getControl, label_name, __searchControl, false);
                            _searchControl._dataList._selectSuccessButton.Click += (s1, e1) =>
                            {
                                string __selectData = _searchControl._dataList._selectList();
                                this._setDataStr(name, __selectData);
                                _searchControl.Close();
                            };
                            _searchControl.StartPosition = FormStartPosition.CenterScreen;
                            _searchControl.ShowDialog();
                        }
                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                    {
                        if (name.Equals(_g.d.resource_report_vat._from_doc_type) || name.Equals(_g.d.resource_report_vat._to_doc_type))
                        {
                            string label_name = ((MyLib._myTextBox)sender)._labelName;
                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            _searchControl._dataList._multiSelect = false;
                            _searchControl._name = _g.g._search_screen_erp_doc_format;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            _searchControl._dataList._extraWhere2 = _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._screen_code + " in (\'PU\', \'PA\', \'PID\', \'PIU\', \'PT\',\'PIC\', \'COB\', \'CCO\', \'CDO\', \'EPO\', \'EPC\', \'EPD\',\'DE\') ";
                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;
                            MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);

                        }
                        else if (name.Equals("book_code"))
                        {
                            string label_name = ((MyLib._myTextBox)sender)._labelName;
                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            _searchControl._dataList._multiSelect = false;
                            _searchControl._name = _g.g._search_screen_gl_journal_book;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;
                            MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);

                        }
                    }
                    break;
                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    {
                        if (name.Equals(_g.d.resource_report._from_sale_person) || name.Equals(_g.d.resource_report._to_sale_person))
                        {
                            string label_name = ((MyLib._myTextBox)sender)._labelName;
                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            _searchControl._dataList._multiSelect = false;
                            _searchControl._name = _g.g._search_screen_erp_user;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            // _searchControl._dataList._extraWhere2 = "coalesce(" +  _g.d.erp_user._table + "." + _g.d.erp_user._is_login_user + ", 0)= 0 ";
                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;
                            MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);

                        }
                    }
                    break;
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    {
                        if (name.Equals(_g.d.resource_report_vat._doc_type))
                        {
                            string label_name = ((MyLib._myTextBox)sender)._labelName;
                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            _searchControl._dataList._multiSelect = false;
                            _searchControl._name = _g.g._search_screen_erp_doc_format;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            // _searchControl._dataList._extraWhere2 = "coalesce(" +  _g.d.erp_user._table + "." + _g.d.erp_user._is_login_user + ", 0)= 0 ";
                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;
                            //MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);
                            _searchControl.StartPosition = FormStartPosition.CenterScreen;
                            _searchControl.ShowDialog();

                        }

                    }
                    break;
                case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {

                        if (name.Equals(_g.d.ic_resource._warehouse))
                        {
                            string label_name = ((MyLib._myTextBox)sender)._labelName;
                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            _searchControl._dataList._multiSelect = false;
                            _searchControl._name = _g.g._search_master_ic_warehouse;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                            // _searchControl._dataList._extraWhere2 = "coalesce(" +  _g.d.erp_user._table + "." + _g.d.erp_user._is_login_user + ", 0)= 0 ";
                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;
                            MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);

                        }
                        else if (name.Equals(_g.d.ic_resource._location))
                        {
                            string __whSelect = this._getDataStr(_g.d.ic_resource._warehouse).ToString();
                            string label_name = ((MyLib._myTextBox)sender)._labelName;
                            _searchTextbox = (MyLib._myTextBox)sender;
                            _searchControl = new MyLib._searchDataFull();
                            _searchControl._dataList._multiSelect = false;
                            _searchControl._name = _g.g._search_master_ic_shelf;
                            _searchControl._dataList._loadViewFormat(_searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);

                            if (__whSelect.Length > 0)
                            {
                                _searchControl._dataList._extraWhere2 = "" + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __whSelect + "\'";
                            }

                            _searchControl._dataList._gridData._mouseClick += _gridData__mouseClick1;
                            _searchControl._searchEnterKeyPress += __searchControl__searchEnterKeyPress;
                            MyLib._myGlobal._startSearchBox(_searchTextbox, label_name, _searchControl, false);

                        }
                    }
                    break;
            }
        }

        private void __searchControl__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            switch (this._mode)
            {
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                case _singhaReportEnum.ลูกหนี้คงค้าง:
                    {
                        if (_searchTextbox._name == _g.d.ap_ar_resource._ar_code_begin || _searchTextbox._name == _g.d.ap_ar_resource._ar_code_end)
                        {
                            if (row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();

                                this._setDataStr(_searchTextbox._name, __getCode);

                                //this._gridUnit._cellUpdate(this._gridUnit._selectRow, _g.d.edi_unit_list._unit_code, __getIcCode, true);
                                //this._gridUnit._cellUpdate(this._gridUnit._selectRow, _g.d.edi_unit_list._unit_name, __getICName, true);
                                SendKeys.Send("{TAB}");

                                this._searchControl.Close();
                            }
                        }
                    }
                    break;
            }
        }

        private void _gridData__mouseClick1(object sender, MyLib.GridCellEventArgs e)
        {
            switch (this._mode)
            {
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ:
                case _singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต:
                case _singhaReportEnum.ลูกหนี้คงค้าง:
                    {
                        if (_searchTextbox._name == _g.d.ap_ar_resource._ar_code_begin || _searchTextbox._name == _g.d.ap_ar_resource._ar_code_end)
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();

                            }

                        }
                    }
                    break;
                case _singhaReportEnum.การ์ดเจ้าหนี้:
                    {
                        if (_searchTextbox._name == _g.d.resource_report._from_ap || _searchTextbox._name == _g.d.resource_report._to_ap)
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();

                            }

                        }

                    }
                    break;
                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                    {
                        if (_searchTextbox._name == _g.d.resource_report_vat._from_doc_type || _searchTextbox._name == _g.d.resource_report_vat._to_doc_type)
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();

                            }

                        }
                        if (_searchTextbox._name == "book_code")
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.gl_journal_book._table + "." + _g.d.gl_journal_book._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();
                            }
                        }

                    }
                    break;

                case _singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย:
                    {
                        if (_searchTextbox._name == _g.d.resource_report._from_sale_person || _searchTextbox._name == _g.d.resource_report._to_sale_person)
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.erp_user._table + "." + _g.d.erp_user._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();

                            }

                        }
                    }
                    break;
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    {
                        if (_searchTextbox._name == _g.d.resource_report_vat._doc_type)
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();
                            }
                        }
                    }
                    break;
                case _singhaReportEnum.สรุปโหลดสินค้า_รายคลัง:
                    {
                        if (_searchTextbox._name == _g.d.ic_resource._warehouse)
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();
                            }
                        }
                        else if (_searchTextbox._name == _g.d.ic_resource._location)
                        {
                            if (e._row != -1)
                            {
                                string __getCode = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code).ToString();
                                this._setDataStr(_searchTextbox._name, __getCode);
                                SendKeys.Send("{TAB}");
                                this._searchControl.Close();
                            }
                        }
                    }
                    break;
            }
        }

        private void _conditionScreenTop__comboBoxSelectIndexChanged(object sender, string name)
        {
            switch (_mode)
            {
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:

                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    {
                        if (name.Equals(_g.d.resource_report_vat._vat_month))
                        {
                            int __year = MyLib._myGlobal._intPhase(this._getDataStr(_g.d.resource_report_vat._vat_year));
                            int __getMonth = ((MyLib._myComboBox)this._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex + 1;

                            DateTime __dateBegin = new DateTime(__year - MyLib._myGlobal._year_add, __getMonth, 1);
                            DateTime __dateEnd = new DateTime(__year - MyLib._myGlobal._year_add, __getMonth, 1).AddMonths(1).AddDays(-1);

                            this._setDataDate(_g.d.resource_report_vat._date_begin, __dateBegin);
                            this._setDataDate(_g.d.resource_report_vat._date_end, __dateEnd);
                        }
                    }
                    break;
            }
        }

        void _condition_top__textBoxChanged(object sender, string name)
        {
            switch (_mode)
            {
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3:
                case _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53:

                case _singhaReportEnum.GL_รายงานภาษีซื้อ:
                case _singhaReportEnum.GL_รายงานภาษีขาย:
                    {
                        if (name.Equals(_g.d.resource_report_vat._vat_year))
                        {
                            int __year = MyLib._myGlobal._intPhase(this._getDataStr(_g.d.resource_report_vat._vat_year));
                            int __getMonth = ((MyLib._myComboBox)this._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex + 1;

                            DateTime __dateBegin = new DateTime(__year - MyLib._myGlobal._year_add, __getMonth, 1);
                            DateTime __dateEnd = new DateTime(__year - MyLib._myGlobal._year_add, __getMonth, 1).AddMonths(1).AddDays(-1);

                            this._setDataDate(_g.d.resource_report_vat._date_begin, __dateBegin);
                            this._setDataDate(_g.d.resource_report_vat._date_end, __dateEnd);
                        }
                    }
                    break;
            }
        }

    }
}
