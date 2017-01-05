using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Windows.Forms;

namespace SMLERPGL._tax
{
    public partial class _vatConditionForm : Form
    {
        public string[] _vatDateType = new string[] { _g.d.resource_report_vat._by_vat_date, _g.d.resource_report_vat._by_doc_date, _g.d.resource_report_vat._by_due_date };
        public string[] _vatDateTypeFieldName = new string[] { _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._doc_date, _g.d.ic_trans._due_date };
        public string[] _vatSortBy = new string[] { _g.d.resource_report_vat._order_by_doc_date, _g.d.resource_report_vat._order_by_vat_date, _g.d.resource_report_vat._order_by_doc_no, _g.d.resource_report_vat._order_by_vat_no };
        public string[] _vatSortByFieldName = new string[] { _g.d.ic_trans._doc_date, _g.d.ic_trans._tax_doc_date, _g.d.ic_trans._doc_no, _g.d.ic_trans._tax_doc_no };
        public string[] _vatSortWhtBy = new string[] { _g.d.gl_wht_list_detail._cust_code };
        public string[] _vatSortWhtByFieldName = new string[] { _g.d.gl_wht_list_detail._cust_code };
        public string[] _vatSaleType = new string[] { _g.d.resource_report_vat._report_vat_sale_1, _g.d.resource_report_vat._report_vat_sale_2, _g.d.resource_report_vat._report_vat_sale_3 };
        public string[] _vatBuyType = new string[] { _g.d.resource_report_vat._report_vat_buy_1, _g.d.resource_report_vat._report_vat_buy_2, _g.d.resource_report_vat._report_vat_buy_3, _g.d.resource_report_vat._report_vat_buy_4, _g.d.resource_report_vat._report_vat_buy_5, _g.d.resource_report_vat._report_vat_buy_6 };

        IFormatProvider __culture = new CultureInfo("th-TH");
        public bool _process = false;
        public string _where = "";

        public _vatConditionForm(_vatConditionType conditionType, string screenName)
        {
            InitializeComponent();
            //
            this._conditionScreenTop._table_name = _g.d.resource_report_vat._table;
            this._label.Text = screenName;
            this._conditionScreenTop._maxColumn = 2;
            int __row = 0;
            string __reportName = "";
            this._conditionScreenTop._addTextBox(__row++, 0, 1, _g.d.resource_report_vat._report_name, 2, 2);
            switch (conditionType)
            {
                case _vatConditionType.ภาษีซื้อ:
                case _vatConditionType.ภาษีซื้อ_เลขประจำตัวผู้เสียภาษี:
                    __reportName = MyLib._myGlobal._resource("รายงานภาษีซื้อ");
                    this._conditionScreenTop._addComboBox(__row, 0, _g.d.resource_report_vat._date_type, true, _vatDateType, true);
                    this._conditionScreenTop._addComboBox(__row++, 1, _g.d.resource_report_vat._report_vat_type, true, _vatBuyType, true);
                    break;
                case _vatConditionType.ภาษีขาย:
                case _vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี:
                case _vatConditionType.ภาษีขาย_สรุป:
                case _vatConditionType.ภาษีขาย_เลขประจำตัวผู้เสียภาษี:
                    __reportName = MyLib._myGlobal._resource("รายงานภาษีขาย");
                    this._conditionScreenTop._addComboBox(__row, 0, _g.d.resource_report_vat._sort_by, true, _vatSortBy, true);
                    this._conditionScreenTop._addComboBox(__row++, 1, _g.d.resource_report_vat._report_vat_type, true, _vatSaleType, true);
                    break;
            }
            this._conditionScreenTop._addComboBox(__row, 0, _g.d.resource_report_vat._vat_month, true, _g.g._monthName(), true);
            this._conditionScreenTop._addTextBox(__row++, 1, _g.d.resource_report_vat._vat_year, 1);
            this._conditionScreenTop._addTextBox(__row, 0, 3, _g.d.resource_report_vat._address, 2, 2);
            __row += 3;
            //
            this._conditionScreenTop._addTextBox(__row, 0, 1, 1, _g.d.resource_report_vat._vat_group, 1, 10, 1, true, false);
            this._conditionScreenTop._addCheckBox(__row++, 1, _g.d.resource_report_vat._show_vat_amount_only, false, true, true);

            this._conditionScreenTop._addTextBox(__row, 0, 1, 1, _g.d.resource_report_vat._doc_type, 1, 10, 1, true, false);
            if (_g.g._companyProfile._branchStatus == 1)
            {
                this._conditionScreenTop._addTextBox(__row, 1, 1, 1, _g.d.resource_report_vat._branch_code, 1, 10, 1, true, false);
            }
            __row++;
            this._conditionScreenTop._addCheckBox(__row++, 0, _g.d.resource_report_vat._show_doc_no, false, true, false);

            //
            MyLib._myGroupBox __formatNumberGroupBox = this._conditionScreenTop._addGroupBox(__row, 0, 1, 2, 2, _g.d.resource_report_vat._format_number_0, true);
            this._conditionScreenTop._addRadioButtonOnGroupBox(0, 0, __formatNumberGroupBox, _g.d.resource_report_vat._format_number_1, 0, true);
            this._conditionScreenTop._addRadioButtonOnGroupBox(0, 1, __formatNumberGroupBox, _g.d.resource_report_vat._format_number_2, 1, false);
            __row += 2;

            // toe เพิ่ม option แสดงเลขที่ใบกำกับภาษีอ้างอิง
            if (conditionType != _vatConditionType.ภาษีขาย_สรุป)
            {
                this._conditionScreenTop._addCheckBox(__row++, 0, _g.d.resource_report_vat._show_full_invoice_doc_no, false, true);
            }
            //
            ((MyLib._myComboBox)this._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndexChanged += new EventHandler(_vatConditionForm_SelectedIndexChanged);
            this._conditionScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_top__textBoxChanged);
            this._conditionScreenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_conditionScreenTop__textBoxSearch);
            //
            this._conditionScreenTop._setDataStr(_g.d.resource_report_vat._address, MyLib._myGlobal._ltdAddress);
            this._conditionScreenTop._setDataStr(_g.d.resource_report_vat._vat_year, DateTime.Now.ToString("yyyy", __culture));
            this._conditionScreenTop._setDataStr(_g.d.resource_report_vat._report_name, __reportName);
            this._conditionScreenTop._setComboBox(_g.d.resource_report_vat._vat_month, DateTime.Now.Month - 1);
            //
            this._conditionScreenTop.Invalidate();

            this.Load += new EventHandler(_vatConditionForm_Load);
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            if (conditionType == _vatConditionType.ภาษีขาย_สรุป)
            {
                this._conditionScreenTop._enabedControl(_g.d.resource_report_vat._sort_by, false);
                this._conditionScreenTop._enabedControl(_g.d.resource_report_vat._report_vat_type, false);
            }
        }

        void _conditionScreenTop__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._searchDataFull __searchControl = null;

            if (name == _g.d.resource_report_vat._vat_group)
            {

                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                __searchControl = new MyLib._searchDataFull();
                __searchControl._dataList._multiSelect = true;
                __searchControl._name = _g.g._search_screen_gl_tax_group;
                __searchControl._dataList._loadViewFormat(__searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                //__searchControl._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                //__searchControl._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchVatGroup__searchEnterKeyPress);

                //MyLib._myGlobal._startSearchBox(__getControl, label_name, __searchControl, false);
                __searchControl._dataList._selectSuccessButton.Click += (s1, e1) =>
                {
                    string __selectData = __searchControl._dataList._selectList();
                    this._conditionScreenTop._setDataStr(_g.d.resource_report_vat._vat_group, __selectData);
                    __searchControl.Close();
                };
                __searchControl.StartPosition = FormStartPosition.CenterScreen;
                __searchControl.ShowDialog();


            }
            else if (name == _g.d.resource_report_vat._doc_type)
            {
                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                __searchControl = new MyLib._searchDataFull();
                __searchControl._dataList._multiSelect = true;
                __searchControl._name = _g.g._screen_erp_doc_format;
                __searchControl._dataList._loadViewFormat(__searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                //__searchControl._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                //__searchControl._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchVatGroup__searchEnterKeyPress);

                //MyLib._myGlobal._startSearchBox(__getControl, label_name, __searchControl, false);
                __searchControl._dataList._extraWhere2 = _g.d.erp_doc_format._table + "." + _g.d.erp_doc_format._screen_code + " in (\'SI\', \'SA\', \'ST\', \'AOB\', \'ACO\',\'ADO\', \'OI\', \'OCN\', \'ODN\', \'EE\') ";

                __searchControl._dataList._selectSuccessButton.Click += (s1, e1) =>
                {
                    string __selectData = __searchControl._dataList._selectList();
                    this._conditionScreenTop._setDataStr(_g.d.resource_report_vat._doc_type, __selectData);
                    __searchControl.Close();
                };
                __searchControl.StartPosition = FormStartPosition.CenterScreen;
                __searchControl.ShowDialog();


            }
            else if (name == _g.d.resource_report_vat._branch_code)
            {
                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                __searchControl = new MyLib._searchDataFull();
                __searchControl._dataList._multiSelect = true;
                __searchControl._name = _g.g._search_master_erp_branch_list;
                __searchControl._dataList._loadViewFormat(__searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                //__searchControl._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                //__searchControl._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchVatGroup__searchEnterKeyPress);

                //MyLib._myGlobal._startSearchBox(__getControl, label_name, __searchControl, false);
                __searchControl._dataList._selectSuccessButton.Click += (s1, e1) =>
                {
                    string __selectData = __searchControl._dataList._selectList();
                    this._conditionScreenTop._setDataStr(_g.d.resource_report_vat._branch_code, __selectData);
                    __searchControl.Close();
                };
                __searchControl.StartPosition = FormStartPosition.CenterScreen;
                __searchControl.ShowDialog();


            }

        }

        /*void _searchVatGroup__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            throw new NotImplementedException();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            throw new NotImplementedException();
        }*/

        void _condition_top__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.resource_report_vat._vat_year))
            {
                string __year = this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year);
                int __getMonth = ((MyLib._myComboBox)this._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex + 1;
                this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_begin, MyLib._myGlobal._convertDate("1-" + __getMonth.ToString() + "-" + __year));
                DateTime __dateEnd = (__getMonth == 12) ? DateTime.Parse("1-1-" + ((int)MyLib._myGlobal._intPhase(__year) + 1).ToString(), MyLib._myGlobal._cultureInfo()).AddDays(-1) : DateTime.Parse("1-" + ((int)__getMonth + 1).ToString() + "-" + __year, MyLib._myGlobal._cultureInfo()).AddDays(-1);
                this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_end, __dateEnd);
            }
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this._exit();
        }

        void _vatConditionForm_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.F11 == keyData)
            {
                this._working();
                return true;
            }
            if (Keys.Escape == keyData)
            {
                this._exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this._working();
        }

        private void _exit()
        {
            this._process = false;
            this.Close();
        }

        private void _working()
        {
            this._conditionScreenTop._focusFirst();
            this._process = true;
            this.Close();

        }

        void _vatConditionForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string __year = this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year);
            int __getMonth = ((MyLib._myComboBox)this._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex + 1;
            this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_begin, MyLib._myGlobal._convertDate("1-" + __getMonth.ToString() + "-" + __year));
            DateTime __dateEnd = (__getMonth == 12) ? DateTime.Parse("1-1-" + ((int)MyLib._myGlobal._intPhase(__year) + 1).ToString(), MyLib._myGlobal._cultureInfo()).AddDays(-1) : DateTime.Parse("1-" + ((int)__getMonth + 1).ToString() + "-" + __year, MyLib._myGlobal._cultureInfo()).AddDays(-1);
            this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_end, __dateEnd);
        }
    }

    public enum _vatConditionType
    {
        ภาษีซื้อ,
        ภาษีซื้อ_ต่างประเทศ,
        ภาษีซื้อ_สินค้ายกเว้นภาษี,
        ภาษีขาย,
        ภาษีขาย_ต่างประเทศ,
        ภาษีขาย_สินค้ายกเว้นภาษี,
        ภาษีขาย_สรุป,
        ภาษีซื้อ_เลขประจำตัวผู้เสียภาษี,
        ภาษีขาย_เลขประจำตัวผู้เสียภาษี
    }
}
