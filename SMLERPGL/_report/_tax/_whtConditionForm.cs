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
    public partial class _whtConditionForm : Form
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

        public _whtConditionForm(_whtConditionType conditionType, string screenName)
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
                case _whtConditionType.หักณที่จ่ายภงด3:
                    __reportName = MyLib._myGlobal._resource("รายงานภาษีหัก ณ ที่จ่าย (ภ.ง.ด.3)");
                    this._conditionScreenTop._addComboBox(__row++, 0, _g.d.resource_report_vat._sort_by, true, _vatSortWhtBy, true);
                    break;
                case _whtConditionType.หักณที่จ่ายภงด53:
                    __reportName = MyLib._myGlobal._resource("รายงานภาษีหัก ณ ที่จ่าย (ภ.ง.ด.53)");
                    this._conditionScreenTop._addComboBox(__row++, 0, _g.d.resource_report_vat._sort_by, true, _vatSortWhtBy, true);
                    break;
                case _whtConditionType.ถูกหักณที่จ่าย:
                    __reportName = MyLib._myGlobal._resource("รายงานภาษีถูกหัก ณ ที่จ่าย");
                    this._conditionScreenTop._addComboBox(__row++, 0, _g.d.resource_report_vat._sort_by, true, _vatSortWhtBy, true);
                    break;
            }
            this._conditionScreenTop._addComboBox(__row, 0, _g.d.resource_report_vat._vat_month, true, _g.g._monthName(), true);
            this._conditionScreenTop._addTextBox(__row++, 1, _g.d.resource_report_vat._vat_year, 1);
            this._conditionScreenTop._addDateBox(__row, 0, 1, 0, _g.d.resource_report_vat._date_begin, 1, true, true);
            this._conditionScreenTop._addDateBox(__row++, 1, 1, 0, _g.d.resource_report_vat._date_end, 1, true, true);
            this._conditionScreenTop._addTextBox(__row, 0, 3, _g.d.resource_report_vat._address, 2, 2);
            __row += 3;
            //
            this._conditionScreenTop._addTextBox(__row++, 0, 1, _g.d.resource_report_vat._vat_group, 1, 1);
            //
            MyLib._myGroupBox __formatNumberGroupBox = this._conditionScreenTop._addGroupBox(__row, 0, 1, 2, 2, _g.d.resource_report_vat._format_number_0, true);
            this._conditionScreenTop._addRadioButtonOnGroupBox(0, 0, __formatNumberGroupBox, _g.d.resource_report_vat._format_number_1, 0, true);
            this._conditionScreenTop._addRadioButtonOnGroupBox(0, 1, __formatNumberGroupBox, _g.d.resource_report_vat._format_number_2, 1, false);
            __row += 3;

            //toe 
            this._conditionScreenTop._addCheckBox(__row++, 0, _g.d.resource_report_vat._show_remark, true, true);

            //
            ((MyLib._myComboBox)this._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndexChanged += new EventHandler(_vatConditionForm_SelectedIndexChanged);
            this._conditionScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_top__textBoxChanged);
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

        }

        void _condition_top__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.resource_report_vat._vat_year))
            {
                string __year = this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year);
                int __getMonth = ((MyLib._myComboBox)this._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex + 1;

                DateTime __dateBegin = new DateTime(MyLib._myGlobal._intPhase(__year) - MyLib._myGlobal._year_add, __getMonth, 1);
                DateTime __dateEnd = (__getMonth == 12) ? new DateTime(((int)MyLib._myGlobal._intPhase(__year) + 1), 12, 31) : new DateTime(MyLib._myGlobal._intPhase(__year) - MyLib._myGlobal._year_add, __getMonth + 1, 1).AddDays(-1);
                // __dateEnd = (__getMonth == 12) ? DateTime.Parse("1-1-" + ((int)MyLib._myGlobal._intPhase(__year) + 1).ToString(), MyLib._myGlobal._cultureInfo()).AddDays(-1) : DateTime.Parse("1-" + ((int)__getMonth + 1).ToString() + "-" + __year, MyLib._myGlobal._cultureInfo()).AddDays(-1);

                //this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_begin, MyLib._myGlobal._convertDate("1-" + __getMonth.ToString() + "-" + __year));
                //this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_end, __dateEnd);

                //DateTime __dateEnd = (__getMonth == 12) ? DateTime.Parse("1-1-" + ((int)MyLib._myGlobal._intPhase(__year) + 1).ToString(), MyLib._myGlobal._cultureInfo()).AddDays(-1) : DateTime.Parse("1-" + ((int)__getMonth + 1).ToString() + "-" + __year, MyLib._myGlobal._cultureInfo()).AddDays(-1);


                /*
                                DateTime __dateBegin = new DateTime(MyLib._myGlobal._intPhase(__year) - MyLib._myGlobal._year_add, __getMonth, 1);
                                //DateTime __dateEnd = (__getMonth == 12) ? DateTime.Parse("1-1-" + ((int)MyLib._myGlobal._intPhase(__year) + 1).ToString(), MyLib._myGlobal._cultureInfo()).AddDays(-1) : DateTime.Parse("1-" + ((int)__getMonth + 1).ToString() + "-" + __year, MyLib._myGlobal._cultureInfo()).AddDays(-1);
                                DateTime __dateEnd = (__getMonth == 12) ? new DateTime(((int)MyLib._myGlobal._intPhase(__year) + 1), 12, 31) : new DateTime(MyLib._myGlobal._intPhase(__year) - MyLib._myGlobal._year_add, __getMonth + 1, 1).AddDays(-1);
                */

                this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_begin, __dateBegin);
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


            DateTime __dateBegin = new DateTime(MyLib._myGlobal._intPhase(__year) - MyLib._myGlobal._year_add, __getMonth, 1);
            //DateTime __dateEnd = (__getMonth == 12) ? DateTime.Parse("1-1-" + ((int)MyLib._myGlobal._intPhase(__year) + 1).ToString(), MyLib._myGlobal._cultureInfo()).AddDays(-1) : DateTime.Parse("1-" + ((int)__getMonth + 1).ToString() + "-" + __year, MyLib._myGlobal._cultureInfo()).AddDays(-1);
            DateTime __dateEnd = (__getMonth == 12) ? new DateTime(((int)MyLib._myGlobal._intPhase(__year) + 1), 12, 31) : new DateTime(MyLib._myGlobal._intPhase(__year) - MyLib._myGlobal._year_add, __getMonth + 1, 1).AddDays(-1);

            this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_begin, __dateBegin);
            this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_end, __dateEnd);


        }
    }

    public enum _whtConditionType
    {
        หักณที่จ่ายภงด3,
        หักณที่จ่ายภงด53,
        ถูกหักณที่จ่าย
    }
}
