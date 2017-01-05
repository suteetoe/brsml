using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPGL._tax;
using System.Globalization;

namespace SMLERPGL._report._tax
{

    public partial class _whtReport : UserControl
    {
        /// <summary>
        /// รหัสหน้าจอ
        /// </summary>
        public string _screen_code = "";

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
        private _whtConditionType _conditionType;
        bool showPrintDialogByCtrl = false;

        public _whtReport(_whtConditionType conditionType, string screenName)
        {
            InitializeComponent();

            _conditionType = conditionType;

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
                    this._screen_code = "RWHT3";
                    break;
                case _whtConditionType.หักณที่จ่ายภงด53:
                    __reportName = MyLib._myGlobal._resource("รายงานภาษีหัก ณ ที่จ่าย (ภ.ง.ด.53)");
                    this._conditionScreenTop._addComboBox(__row++, 0, _g.d.resource_report_vat._sort_by, true, _vatSortWhtBy, true);
                    this._screen_code = "RWHT53";
                    break;
                case _whtConditionType.ถูกหักณที่จ่าย:
                    __reportName = MyLib._myGlobal._resource("รายงานภาษีถูกหัก ณ ที่จ่าย");
                    this._conditionScreenTop._addComboBox(__row++, 0, _g.d.resource_report_vat._sort_by, true, _vatSortWhtBy, true);
                    this._screen_code = "RPTWHT";
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

            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            // call SMLERPReportTools
            /*
            string __reportName = "";
            switch (_conditionType)
            {
                case _whtConditionType.หักณที่จ่ายภงด3 :
                    __reportName = "WHT3";
                    break;

                case _whtConditionType.หักณที่จ่ายภงด53 :
                    __reportName = "WHT-53";
                    break;
            }

            if (__reportName != "")
            {

                SMLReport._formReport._formPrint __form = new SMLReport._formReport._formPrint(__reportName);
                string __begindate = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_begin)));
                string __enddate = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_end)));

                __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("start_date", __begindate));
                __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("end_date", __enddate));
                __form._query();
            }
            */

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                showPrintDialogByCtrl = true;
            }

            string __begindate = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_begin), MyLib._myGlobal._cultureInfo()));
            string __enddate = MyLib._myGlobal._convertDateToQuery(DateTime.Parse(this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._date_end), MyLib._myGlobal._cultureInfo()));

            List<SMLERPReportTool._ReportToolCondition> __condition = new List<SMLERPReportTool._ReportToolCondition>();
            __condition.Add(new SMLERPReportTool._ReportToolCondition("start_date", __begindate));
            __condition.Add(new SMLERPReportTool._ReportToolCondition("end_date", __enddate));

            SMLERPReportTool._global._printForm(_screen_code, (SMLERPReportTool._ReportToolCondition[])__condition.ToArray(), true);

            showPrintDialogByCtrl = false;

        }

        void _vatConditionForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string __year = this._conditionScreenTop._getDataStr(_g.d.resource_report_vat._vat_year);
            int __getMonth = ((MyLib._myComboBox)this._conditionScreenTop._getControl(_g.d.resource_report_vat._vat_month)).SelectedIndex + 1;
            this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_begin, MyLib._myGlobal._convertDate("1-" + __getMonth.ToString() + "-" + __year));
            DateTime __dateEnd = (__getMonth == 12) ? DateTime.Parse("1-1-" + ((int)MyLib._myGlobal._intPhase(__year) + 1).ToString(), MyLib._myGlobal._cultureInfo()).AddDays(-1) : DateTime.Parse("1-" + ((int)__getMonth + 1).ToString() + "-" + __year, MyLib._myGlobal._cultureInfo()).AddDays(-1);
            this._conditionScreenTop._setDataDate(_g.d.resource_report_vat._date_end, __dateEnd);
        }

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


    }
}
