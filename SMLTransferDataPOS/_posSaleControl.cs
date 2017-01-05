using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace SMLTransferDataPOS
{
    public partial class _posSaleControl : UserControl
    {
        public string _columnSerialNumberCount = "serial_number_count";
        private string _columnPriceRoworder = "price_roworder";
        public string _columnAverageCostUnitStand = "average_cost_stand";
        public string _columnAverageCostUnitDiv = "average_cost_div";
        public string _columnSerialNumber = _g.d.ic_trans_detail._serial_number;

        public _posSaleControl()
        {
            InitializeComponent();

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._sale_pos_screen_top._maxColumn = 4;
            this._sale_pos_screen_top._table_name = _g.d.ic_trans_resource._table;

            this._sale_pos_screen_top._addDateBox(0, 0, 1, 0, _g.d.ic_trans_resource._from_doc_date, 1, true);
            this._sale_pos_screen_top._addDateBox(0, 1, 1, 0, _g.d.ic_trans_resource._to_doc_date, 1, true);
            this._sale_pos_screen_top._addTextBox(0, 2, 1, 0, _g.d.ic_trans_resource._from_doc_no, 1, 0, 1, true, false);
            this._sale_pos_screen_top._addTextBox(0, 3, 1, 0, _g.d.ic_trans_resource._to_doc_no, 1, 0, 1, true, false);
            this._sale_pos_screen_top._setDataDate(_g.d.ic_trans_resource._from_doc_date, MyLib._myGlobal._workingDate);
            this._sale_pos_screen_top._setDataDate(_g.d.ic_trans_resource._to_doc_date, MyLib._myGlobal._workingDate);

            this._sale_pos_screen_top._textBoxSearch += new MyLib.TextBoxSearchHandler(_sale_pos_screen_top__textBoxSearch);
            //this._sale_pos_screen_top._textBoxChanged += new MyLib.TextBoxChangedHandler(_sale_pos_screen_top__textBoxChanged);

            // ic_trans_screen_top
            this._ic_trans_screen_top._table_name = _g.d.ic_trans._table;
            this._ic_trans_screen_top._maxColumn = 4;
            this._ic_trans_screen_top._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
            this._ic_trans_screen_top._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 0);
            this._ic_trans_screen_top._addTextBox(0, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 0, 1, true, false, false);
            this._ic_trans_screen_top._addTextBox(0, 3, _g.d.ic_trans._doc_format_code, 1);
            this._ic_trans_screen_top._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 0, 1, true, false, false, true, true, _g.d.ic_trans._ar_code);
            this._ic_trans_screen_top._addTextBox(1, 2, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, false);
            this._ic_trans_screen_top._addTextBox(1, 3, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, false);

            //this._ic_trans_screen_top._addTextBox(2, 0, 2, 4, _g.d.ic_trans._remark, 2, 0);

            this._ic_trans_screen_top._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
            this._ic_trans_screen_top._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString());

            this._ic_trans_screen_top._textBoxSearch += new MyLib.TextBoxSearchHandler(_ic_trans_screen_top__textBoxSearch);
            this._ic_trans_screen_top._textBoxChanged += new MyLib.TextBoxChangedHandler(_ic_trans_screen_top__textBoxChanged);
            //
            //this._sale_pos_screen_top._enabedControl(_g.d.ic_trans_resource._doc_format_code, false);
            this._ic_trans_screen_top._enabedControl(_g.d.ic_trans._doc_format_code, false);
            //

            // screen bottom
            this._ic_trans_screen_bottom._table_name = _g.d.ic_trans._table;
            this._ic_trans_screen_bottom._maxColumn = 4;
            this._ic_trans_screen_bottom._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumberAmount);
            this._ic_trans_screen_bottom._addNumberBox(0, 1, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumberAmount);
            this._ic_trans_screen_bottom._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumberAmount);
            this._ic_trans_screen_bottom._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumberAmount);
            this._ic_trans_screen_bottom._addTextBox(1, 0, 2, 0, _g.d.ic_trans._remark, 2, 0);
            this._ic_trans_screen_bottom._addNumberBox(1, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumberAmount);
            this._ic_trans_screen_bottom._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumberAmount);
            this._ic_trans_screen_bottom._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumberAmount);
            this._ic_trans_screen_bottom._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumberAmount);

            //this._ic_trans_screen_bottom._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._total_value, 1, 2, true);
            //this._ic_trans_screen_bottom._addNumberBox(0, 1, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true);
            //this._ic_trans_screen_bottom._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true);
            //this._ic_trans_screen_bottom._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._vat_rate, _g.g._companyProfile._vat_rate);

            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._total_discount, false);
            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._total_before_vat, false);
            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._vat_rate, false);
            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._total_value, false);
            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._total_vat_value, false);
            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._total_after_vat, false);
            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._total_except_vat, false);
            this._ic_trans_screen_bottom._enabedControl(_g.d.ic_trans._total_amount, false);


            // pay control
            this._screen_pay._table_name = _g.d.cb_trans._table;
            this._screen_pay._maxColumn = 2;

            this._screen_pay._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumberAmount);
            this._screen_pay._addNumberBox(7, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumberAmount);
            //
            this._screen_pay._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumberAmount);
            this._screen_pay._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumberAmount);
            //this._screen_pay._addNumberBox(2, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumberAmount);
            //this._screen_pay._addNumberBox(3, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumberAmount);
            //this._screen_pay._addNumberBox(4, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumberAmount);
            //this._screen_pay._addNumberBox(5, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumberAmount);
            //this._screen_pay._addNumberBox(6, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumberAmount);
            this._screen_pay._addNumberBox(2, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumberAmount);
            this._screen_pay._addNumberBox(3, 1, 1, 0, _g.d.cb_trans._coupon_amount, 1, 2, true, __formatNumberAmount);
            this._screen_pay._addNumberBox(4, 1, 1, 0, _g.d.cb_trans._point_qty, 1, 2, true, __formatNumberAmount);
            //this._screen_pay._addNumberBox(10, 1, 1, 0, _g.d.cb_trans._point_rate, 1, 2, true, __formatNumberAmount);
            this._screen_pay._addNumberBox(5, 1, 1, 0, _g.d.cb_trans._point_amount, 1, 2, true, __formatNumberAmount);
            this._screen_pay._addNumberBox(7, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumberAmount);

            this._screen_pay._enabedControl(_g.d.cb_trans._total_amount, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._total_net_amount, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._cash_amount, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._total_income_amount, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._card_amount, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._coupon_amount, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._point_qty, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._point_amount, false);
            this._screen_pay._enabedControl(_g.d.cb_trans._total_amount_pay, false);

            //
            this._transGrid._table_name = _g.d.ic_trans_detail._table;
            this._transGrid._isEdit = false;

            this._transGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, false, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
            this._transGrid._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
            this._transGrid._addColumn(_g.d.ic_trans_detail._discount_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._transGrid._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._transGrid._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
            this._transGrid._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
            this._transGrid._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
            this._transGrid._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);

            // field ซ่อน
            this._transGrid._addColumn(_g.d.ic_trans_detail._extra, 12, 1, 5, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans_detail._dimension, 12, 1, 5, false, true, false);
            // Field ซ่อน
            this._transGrid._addColumn(_g.d.ic_trans_detail._unit_name, 1, 1, 0, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans_detail._unit_type, 2, 1, 0, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans_detail._stand_value, 3, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._divide_value, 3, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._item_type, 2, 1, 10, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._item_code_main, 1, 1, 10, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._is_permium, 2, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._is_get_price, 2, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._price_exclude_vat, 3, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._total_vat_value, 3, 1, 15, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._sum_amount_exclude_vat, 3, 1, 0, false, true, true, false, __formatNumberAmount);
            this._transGrid._addColumn(_g.d.ic_trans_detail._hidden_cost_1_exclude_vat, 3, 1, 0, false, true, true);
            //this._addColumn(_g.d.ic_trans_detail._doc_date_calc, 4, 1, 0, false, true, true);
            //this._addColumn(_g.d.ic_trans_detail._doc_time_calc, 1, 1, 0, false, true, true);
            this._transGrid._addColumn(this._columnAverageCostUnitStand, 3, 1, 0, false, true, false);
            this._transGrid._addColumn(this._columnAverageCostUnitDiv, 3, 1, 0, false, true, false);
            this._transGrid._addColumn(this._columnPriceRoworder, 2, 1, 0, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans_detail._user_approve, 1, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._price_mode, 1, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._price_type, 1, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._is_serial_number, 2, 1, 0, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans_detail._tax_type, 2, 1, 0, false, true, true);
            this._transGrid._addColumn(this._columnSerialNumber, 12, 1, 0, false, true, false);
            this._transGrid._addColumn(this._columnSerialNumberCount, 3, 1, 0, false, true, false);

            this._transGrid._calcPersentWidthToScatter();

            this._payCreditCardGridControl1._isEdit = false;
            this._payCouponGridControl1._isEdit = false;

            this.Load += new EventHandler(_posSaleControl_Load);
            this._transGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_transGrid__afterCalcTotal);
        }

        void _transGrid__afterCalcTotal(object sender)
        {
            //_calc((MyLib._myGrid)sender);
        }

        void _posSaleControl_Load(object sender, EventArgs e)
        {
            


            //

        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            _loadData();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        TextBox _searchTextBox = null;
        void _ic_trans_screen_top__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            //_searchTextBox = __getControl;
            _searchName = __getControl._name;
            string label_name = __getControl._labelName;
            string _searchWhere = "";

            if (_searchName.Equals(_g.d.ic_trans._doc_no))
            {
                this._searchControl = new MyLib._searchDataFull();
                this._searchControl._dataList._webServiceURL = _global._datacenter_server;
                this._searchControl._dataList._ProviderName = "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml";
                this._searchControl._dataList._databaseName = _global._datacenter_database_name;
                this._searchControl._dataList._databaseType = _global._datacenter_database_type;
                this._searchControl._dataList._userOhterConnection = true;
                this._searchControl._dataList._loadViewFormat(_g.g._screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
                _searchWhere = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'SI\'";

                this._searchControl._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input", serialNumber);

                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                };
                this._searchControl._searchEnterKeyPress += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input", serialNumber);
                };

                MyLib._myGlobal._startSearchBox(__getControl, label_name, _searchControl, false, true, _searchWhere);
            }
            if (_searchName.Equals(_g.d.ic_trans._cust_code))
            {
                this._searchControl = new MyLib._searchDataFull();
                this._searchControl._dataList._webServiceURL = _global._datacenter_server;
                this._searchControl._dataList._ProviderName = "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml";
                this._searchControl._dataList._databaseName = _global._datacenter_database_name;
                this._searchControl._dataList._databaseType = _global._datacenter_database_type;
                this._searchControl._dataList._userOhterConnection = true;
                this._searchControl.Text = MyLib._myGlobal._resource("ค้นหาลูกค้า");
                this._searchControl._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);

                this._searchControl._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input", serialNumber);

                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                };
                this._searchControl._searchEnterKeyPress += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input", serialNumber);
                };

                MyLib._myGlobal._startSearchBox(__getControl, label_name, _searchControl, false, true, _searchWhere);

            }
            else if (_searchName.Equals(_g.d.ic_trans._wh_from))
            {
                this._searchControl = new MyLib._searchDataFull();
                this._searchControl._dataList._webServiceURL = _global._datacenter_server;
                this._searchControl._dataList._ProviderName = "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml";
                this._searchControl._dataList._databaseName = _global._datacenter_database_name;
                this._searchControl._dataList._databaseType = _global._datacenter_database_type;
                this._searchControl._dataList._userOhterConnection = true;

                this._searchControl._dataList._loadViewFormat(_g.g._search_master_ic_warehouse, MyLib._myGlobal._userSearchScreenGroup, false);

                this._searchControl._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                };
                this._searchControl._searchEnterKeyPress += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                };


                MyLib._myGlobal._startSearchBox(__getControl, label_name, _searchControl, false, true, _searchWhere);

            }
            else if (_searchName.Equals(_g.d.ic_trans._location_from))
            {
                this._searchControl = new MyLib._searchDataFull();
                this._searchControl._dataList._webServiceURL = _global._datacenter_server;
                this._searchControl._dataList._ProviderName = "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml";
                this._searchControl._dataList._databaseName = _global._datacenter_database_name;
                this._searchControl._dataList._databaseType = _global._datacenter_database_type;
                this._searchControl._dataList._userOhterConnection = true;
                this._searchControl._dataList._loadViewFormat(_g.g._search_master_ic_shelf, MyLib._myGlobal._userSearchScreenGroup, false);

                this._searchControl._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                };
                this._searchControl._searchEnterKeyPress += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, 0).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    this._ic_trans_screen_top._setDataStr(_searchName, __docNo);
                };
                _searchWhere = _g.d.ic_shelf._whcode + "=\'" + this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._wh_from) + "\'";
                MyLib._myGlobal._startSearchBox(__getControl, label_name, _searchControl, false, true, _searchWhere);

            }
        }

        void _ic_trans_screen_top__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_trans._doc_no))
            {
                string _docFormatCode = "";
                string __docNo = this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._doc_no);

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_global._datacenter_server, "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml", _global._datacenter_database_type);
                DataTable __getFormat = __myFrameWork._query(_global._datacenter_database_name, "select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    _docFormatCode = __getFormat.Rows[0][1].ToString();


                    string __newDoc = _getAutoRun(__docNo, this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, _g.d.ic_trans._table);
                    //_g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docNo, this._sale_pos_screen_top._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, _g.g._transControlTypeEnum.ว่าง, _g.d.ic_trans._table);


                    this._ic_trans_screen_top._setDataStr(_g.d.ic_trans._doc_no, __newDoc, "", true);
                    this._ic_trans_screen_top._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                }

                if (_docFormatCode.Trim().Length == 0)
                {
                    string __firstString = MyLib._myGlobal._firstString(__docNo);
                    if (__firstString.Length > 0)
                    {
                        _docFormatCode = __firstString;
                        this._ic_trans_screen_top._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }
                /*if (this._docFormatCode.Equals(MyLib._myGlobal._firstString(__docNo)) == false)
                {
                    DialogResult __message = MessageBox.Show("ประเภทเอกสารไม่สัมพันธ์กับเลขที่เอกสาร ต้องการเปลี่ยนตามเลขที่เอกสารเลยหรือไม่", "Doc Number", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    if (__message == DialogResult.Yes)
                    {
                        this._docFormatCode = MyLib._myGlobal._firstString(__docNo);
                        this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }*/
            }
            else if (name.Equals(_g.d.ic_trans._cust_code))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true, this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._cust_code).ToString());
            }
            else if (name.Equals(_g.d.ic_trans._wh_from))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true, this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._wh_from).ToString());
            }
            else if (name.Equals(_g.d.ic_trans._location_from))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true, this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._location_from).ToString());
            }
        }

        public void _search(Boolean warning, string searchValue)
        {
            string __searchValue = searchValue; // _ar_point_balance_screen1._getDataStr(_searchName);

            string __query = "";
            string __searchName = "";

            if (_searchName.Equals(_g.d.ic_trans._cust_code))
            {
                __searchName = _searchName;
                __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "=\'" + __searchValue.ToUpper() + "\'";
            }
            else if (_searchName.Equals(_g.d.ic_trans._wh_from))
            {
                __searchName = _searchName;
                __query = "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ic_warehouse._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse._code) + "=\'" + __searchValue.ToUpper() + "\'";
            }
            else if (_searchName.Equals(_g.d.ic_trans._location_from))
            {
                __searchName = _searchName;
                __query = "select " + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_shelf._code) + "=\'" + __searchValue.ToUpper() + "\' and upper(whcode) = \'" + this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._wh_from) + "\'";
            }

            if (!__query.Equals("") && !__searchName.Equals(""))
            {
                try
                {
                    //_searchAndWarning(__searchName, __query, warning);
                    MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork(_global._datacenter_server, "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml", _global._datacenter_database_type);
                    DataSet dataResult = myFrameWork._query(_global._datacenter_database_name, __query);
                    if (dataResult.Tables[0].Rows.Count > 0)
                    {
                        string getData = dataResult.Tables[0].Rows[0][0].ToString();


                        string getDataStr = "";

                        if (_searchName.Equals(_g.d.ic_trans._cust_code))
                        {
                            getDataStr = this._ic_trans_screen_top._getDataStr(_searchName);
                            _ic_trans_screen_top._setDataStr(_searchName, getDataStr, getData, true);
                        }
                        else if (_searchName.Equals(_g.d.ic_trans._wh_from))
                        {
                            getDataStr = this._ic_trans_screen_top._getDataStr(_searchName);
                            _ic_trans_screen_top._setDataStr(_searchName, getDataStr, getData, true);
                        }
                        else if (_searchName.Equals(_g.d.ic_trans._location_from))
                        {
                            getDataStr = this._ic_trans_screen_top._getDataStr(_searchName);
                            _ic_trans_screen_top._setDataStr(_searchName, getDataStr, getData, true);
                        }

                        if (_searchTextBox != null)
                        {
                            string __GetFieldValue = "";
                            string __labelName = "";
                            if (_searchName.Equals(_g.d.ic_trans._cust_code))
                            {
                                __GetFieldValue = this._ic_trans_screen_top._getDataStr(_searchName);
                                __labelName = this._ic_trans_screen_top._getLabelName(_searchName);
                            }

                            if (_searchName.Equals(_searchName) && __GetFieldValue != "")
                            {
                                if (dataResult.Tables[0].Rows.Count == 0 && warning)
                                {
                                    ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                                    ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                                    ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                                    ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                                    _searchTextBox.Focus();
                                    //
                                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __labelName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    //__result = true;
                                }
                            }
                        }
                    }

                }
                catch
                {
                }
            }

        }

        //bool _searchAndWarning(string fieldName, string query, Boolean warning)
        //{
        //    bool __result = false;
        //    MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
        //    DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);
        //    if (dataResult.Tables[0].Rows.Count > 0)
        //    {
        //        string getData = dataResult.Tables[0].Rows[0][0].ToString();
        //        string getDataStr = this._ic_trans_screen_top._getDataStr(fieldName);
        //        _ic_trans_screen_top._setDataStr(fieldName, getDataStr, getData, true);
        //    }

        //    if (_searchTextBox != null)
        //    {
        //        if (_searchName.Equals(fieldName) && _ar_point_balance_screen1._getDataStr(fieldName) != "")
        //        {
        //            if (dataResult.Tables[0].Rows.Count == 0 && warning)
        //            {
        //                ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
        //                ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
        //                ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
        //                ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
        //                _searchTextBox.Focus();
        //                //
        //                MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + _ar_point_balance_screen1._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //                __result = true;
        //            }
        //        }
        //    }
        //    return __result;
        //}

        string _getAutoRun(string docNo, string docDate, string format, string tableName)
        {
            string __getFormat = format;
            string __result = __getFormat;
            string __docFormatResult = "";
            if (__getFormat.Length > 0)
            {
                //DateTime __date = docDate;
                DateTime __date = MyLib._myGlobal._convertDate(docDate);
                CultureInfo __dateUSZone = new CultureInfo("en-US");
                CultureInfo __dateTHZone = new CultureInfo("th-TH");
                __getFormat = __getFormat.Replace("DD", __date.ToString("dd", __dateUSZone));
                __getFormat = __getFormat.Replace("MM", __date.ToString("MM", __dateUSZone));
                __getFormat = __getFormat.Replace("YYYY", __date.ToString("yyyy", __dateUSZone));
                __getFormat = __getFormat.Replace("YY", __date.ToString("yy", __dateUSZone));
                __getFormat = __getFormat.Replace("วว", __date.ToString("dd", __dateTHZone));
                __getFormat = __getFormat.Replace("ดด", __date.ToString("MM", __dateTHZone));
                __getFormat = __getFormat.Replace("ปปปป", __date.ToString("yyyy", __dateTHZone));
                __getFormat = __getFormat.Replace("ปป", __date.ToString("yy", __dateTHZone));
                __getFormat = __getFormat.Replace("@", docNo);
                int __numberBegin = __getFormat.IndexOf("#");
                if (__numberBegin != -1)
                {
                    StringBuilder __getFormatNumber = new StringBuilder();
                    int __numberEnd = __numberBegin;
                    while (__numberEnd < __getFormat.Length && __getFormat[__numberEnd] == '#')
                    {
                        __getFormatNumber.Append("#");
                        __numberEnd++;
                    }
                    __getFormat = __getFormat.Remove(__numberBegin, __numberEnd - __numberBegin);
                    string __getDocQuery = __getFormat + "z";
                    string __qw = "";

                    __qw = " " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and " + _g.d.ic_trans._doc_no + " <= '" + __getDocQuery + "'";
                    //__result = MyLib._myGlobal._getAutoRun(tableName, _g.d.ic_trans._doc_no, __qw, __getFormat, true, __getFormatNumber.ToString()).ToString();

                    string fildName = _g.d.ic_trans._doc_no;
                    string quereyWhere = __qw;


                    try
                    {
                        double __runningNumber = 1;
                        string __newFormat = __getFormatNumber.ToString().Replace('#', '0');
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_global._datacenter_server, "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml", _global._datacenter_database_type);
                        // ตรวจสอบ Data Bases Type
                        string __query = "";
                        switch (__myFrameWork._databaseSelectType)
                        {
                            case MyLib._myGlobal._databaseType.MicrosoftSQL2005:
                            case MyLib._myGlobal._databaseType.MicrosoftSQL2000:
                                __query = "select top 1 " + fildName + " from  " + tableName + " where " + quereyWhere + " order by " + fildName + " desc ";
                                break;
                            default:
                                __query = "select " + fildName + " from  " + tableName + " where " + quereyWhere + " order by " + fildName + " desc  limit(1)";
                                break;
                        }
                        DataSet __getLastCode = __myFrameWork._query(_global._datacenter_database_name, __query);
                        if (__getLastCode.Tables[0].Rows.Count > 0)
                        {
                            if (__getLastCode.Tables[0].Rows[0][0].ToString().IndexOf(__getFormat) != -1)
                            {
                                string __docRun = __getLastCode.Tables[0].Rows[0][0].ToString();
                                __docRun = __docRun.Substring(__getFormat.Length);
                                __runningNumber = double.Parse(__docRun) + 1;
                            }
                        }
                        __docFormatResult = __getFormat + String.Format("{0:" + __newFormat.Remove(0, 1) + "#}", __runningNumber);
                    }
                    catch
                    {
                    }
                }
            }

            return __docFormatResult;
        }

        string _searchName = "";
        MyLib._searchDataFull _searchControl = null;
        void _sale_pos_screen_top__textBoxSearch(object sender)
        {
            // do search ก่อน
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = __getControl._name;
            string label_name = __getControl._labelName;
            string _searchWhere = "";

            if (_searchName.Equals(_g.d.ic_trans_resource._from_doc_no) || _searchName.Equals(_g.d.ic_trans_resource._to_doc_no))
            {
                // search sale doc
                this._searchControl = new MyLib._searchDataFull();
                this._searchControl.WindowState = FormWindowState.Maximized;
                this._searchControl._dataList._loadViewFormat(_g.g._search_screen_sale, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchControl._dataList._extraWhere = _g.d.ic_trans._is_pos + "=1 and (" + _g.d.ic_trans._doc_ref + " is null or " + _g.d.ic_trans._doc_ref + " =\'\') ";

                this._searchControl._dataList._gridData._mouseClick += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input", serialNumber);

                    this._sale_pos_screen_top._setDataStr(_searchName, __docNo);
                };
                this._searchControl._searchEnterKeyPress += (s1, e1) =>
                {
                    string __docNo = this._searchControl._dataList._gridData._cellGet(this._searchControl._dataList._gridData._selectRow, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                    this._searchControl.Close();
                    this._searchControl.Dispose();
                    this._sale_pos_screen_top._setDataStr(_searchName, __docNo);
                    //this._command("enter:textbox:input=" + __arCode + "@end:textbox:input", serialNumber);
                };

                //MyLib._myGlobal._startSearchBox(__getControl, label_name, _searchControl, true);
                this._searchControl.ShowDialog(MyLib._myGlobal._mainForm);

            }

        }

        void _loadData()
        {
            // load sale doc
            string __docDateTransFlagWhere = _g.d.ic_trans._doc_date + " between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) + "  and " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " ";

            string __fromDocNo = this._sale_pos_screen_top._getDataStr(_g.d.ic_trans_resource._from_doc_no);
            string __toDocNo = this._sale_pos_screen_top._getDataStr(_g.d.ic_trans_resource._to_doc_no);

            if (__fromDocNo.Length > 0 && __toDocNo.Length > 0)
            {
                __docDateTransFlagWhere = __docDateTransFlagWhere + " AND ( doc_no between \'" + __fromDocNo + "\' and \'" + __toDocNo + "\') ";
            }

            #region Trans Query
            string __selectTransField = MyLib._myGlobal._fieldAndComma(
                " sum(" + _g.d.ic_trans._total_value + ") as " + _g.d.ic_trans._total_value,
                " sum(" + _g.d.ic_trans._total_discount + ") as " + _g.d.ic_trans._total_discount,
                " sum(" + _g.d.ic_trans._total_after_discount + ") as " + _g.d.ic_trans._total_after_discount,
                " sum(" + _g.d.ic_trans._total_except_vat + ") as " + _g.d.ic_trans._total_except_vat,
                " sum(" + _g.d.ic_trans._total_before_vat + ") as " + _g.d.ic_trans._total_before_vat,
                " sum(" + _g.d.ic_trans._total_after_vat + ") as " + _g.d.ic_trans._total_after_vat,
                " sum(" + _g.d.ic_trans._total_vat_value + ") as " + _g.d.ic_trans._total_vat_value,
                " sum(" + _g.d.ic_trans._total_amount + ") as " + _g.d.ic_trans._total_amount
                );

            string __transWhere = _g.d.ic_trans._is_pos + " = 1 and " + _g.d.ic_trans._last_status + " = 0 and (" + _g.d.ic_trans._doc_ref + " is null or " + _g.d.ic_trans._doc_ref + " = '' ) ";

            string __queryTrans = "select " + MyLib._myGlobal._fieldAndComma(
                _g.d.ic_trans._total_value,
                _g.d.ic_trans._total_discount,
                _g.d.ic_trans._total_value + " - " + _g.d.ic_trans._total_discount + " as " + _g.d.ic_trans._total_after_discount,
                _g.d.ic_trans._total_before_vat,
                _g.d.ic_trans._total_after_vat,
                _g.d.ic_trans._total_except_vat,
                _g.d.ic_trans._total_vat_value,
                _g.d.ic_trans._total_amount
                ) + " from " + _g.d.ic_trans._table + " where " + __docDateTransFlagWhere + " and " + __transWhere;


            // ประกอบ 
            string __queryTransStr = "select " + __selectTransField + " from (" + __queryTrans + ") as temp1";

            #endregion

            // query trans detail
            #region get ic_trans_detail

            string __fullInvoiceFilter = " and ((select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + ") is null or (select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + ")  = '')";
            string __selectField = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code,
                _g.d.ic_trans_detail._item_name,
                _g.d.ic_trans_detail._barcode,
                _g.d.ic_trans_detail._unit_code,
                "sum(" + _g.d.ic_trans_detail._qty + ") as " + _g.d.ic_trans_detail._qty,
                _g.d.ic_trans_detail._price,
                "sum(" + _g.d.ic_trans_detail._discount_amount + ") as " + _g.d.ic_trans_detail._discount_amount,
                "sum(" + _g.d.ic_trans_detail._sum_amount + ") as " + _g.d.ic_trans_detail._sum_amount,
                "sum(" + _g.d.ic_trans_detail._total_vat_value + ") as " + _g.d.ic_trans_detail._total_vat_value,
                "sum(" + _g.d.ic_trans_detail._sum_amount_exclude_vat + ") as " + _g.d.ic_trans_detail._sum_amount_exclude_vat,
                "sum(" + _g.d.ic_trans_detail._price_exclude_vat + ") as " + _g.d.ic_trans_detail._price_exclude_vat,
                _g.d.ic_trans_detail._stand_value,
                _g.d.ic_trans_detail._divide_value);

            string __queryTransDetail = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount_amount + "," + _g.d.ic_trans_detail._sum_amount + "," + _g.d.ic_trans_detail._stand_value + "," + _g.d.ic_trans_detail._divide_value + "," + _g.d.ic_trans_detail._total_vat_value + "," + _g.d.ic_trans_detail._sum_amount_exclude_vat + "," + _g.d.ic_trans_detail._price_exclude_vat + " from " + _g.d.ic_trans_detail._table + " where " + __docDateTransFlagWhere + " and " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._is_pos + "=1 " + __fullInvoiceFilter;

            StringBuilder __queryTransDetailStr = new StringBuilder();

            __queryTransDetailStr.Append("select " + __selectField + " from (" + __queryTransDetail + ") as temp1 ");
            __queryTransDetailStr.Append(" where  " + _g.d.ic_trans_detail._item_code + " != '' ");
            __queryTransDetailStr.Append(" group by " + _g.d.ic_trans_detail._item_code + "," +
                _g.d.ic_trans_detail._item_name + "," +
                _g.d.ic_trans_detail._barcode + "," +
                _g.d.ic_trans_detail._unit_code + "," +
                _g.d.ic_trans_detail._price + "," +
                _g.d.ic_trans_detail._stand_value + "," +
                _g.d.ic_trans_detail._divide_value);

            __queryTransDetailStr.Append(" order by " + _g.d.ic_trans_detail._item_code);

            // end ic_trans_detail

            #endregion

            #region CB Trans

            string __cbTransField = MyLib._myGlobal._fieldAndComma(
                "sum(" + _g.d.cb_trans._total_amount + ") as " + _g.d.cb_trans._total_amount,
                "sum(" + _g.d.cb_trans._total_net_amount + ") as " + _g.d.cb_trans._total_net_amount,
                "sum(" + _g.d.cb_trans._cash_amount + ") as " + _g.d.cb_trans._cash_amount,
                "sum(" + _g.d.cb_trans._chq_amount + ") as " + _g.d.cb_trans._chq_amount,
                "sum(" + _g.d.cb_trans._tranfer_amount + ") as " + _g.d.cb_trans._tranfer_amount,
                "sum(" + _g.d.cb_trans._card_amount + ") as " + _g.d.cb_trans._card_amount,
                "sum(" + _g.d.cb_trans._total_income_amount + ") as " + _g.d.cb_trans._total_income_amount,
                "sum(" + _g.d.cb_trans._point_amount + ") as " + _g.d.cb_trans._point_amount,
                "sum(" + _g.d.cb_trans._coupon_amount + ") as " + _g.d.cb_trans._coupon_amount,
                "( sum(" + _g.d.cb_trans._cash_amount + ") + sum(" + _g.d.cb_trans._chq_amount + ") +  sum(" + _g.d.cb_trans._tranfer_amount + ") + sum(" + _g.d.cb_trans._card_amount + ") + sum(" + _g.d.cb_trans._total_income_amount + ") + sum(" + _g.d.cb_trans._point_amount + ") ) as " + _g.d.cb_trans._total_amount_pay
                );

            StringBuilder __cbTransQuery = new StringBuilder();
            __cbTransQuery.Append(" select " + __cbTransField);
            __cbTransQuery.Append(" from (");
            __cbTransQuery.Append(" select " + MyLib._myGlobal._fieldAndComma(_g.d.cb_trans._doc_no, _g.d.cb_trans._doc_date, _g.d.cb_trans._doc_time, _g.d.cb_trans._trans_flag, _g.d.cb_trans._trans_type, _g.d.cb_trans._doc_format_code,
                _g.d.cb_trans._total_amount,
                _g.d.cb_trans._total_net_amount,
                _g.d.cb_trans._cash_amount,
                _g.d.cb_trans._card_amount,
                _g.d.cb_trans._point_amount,
                _g.d.cb_trans._chq_amount,
                _g.d.cb_trans._tranfer_amount,
                "(" + _g.d.cb_trans._total_income_amount + ") as " + _g.d.cb_trans._total_income_amount,
                _g.d.cb_trans._coupon_amount
                ) + " from " + _g.d.cb_trans._table + " where " + __docDateTransFlagWhere + " and " + _g.d.cb_trans._trans_type + "=" + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and ( select is_pos from ic_trans where ic_trans.doc_no = cb_trans.doc_no and ic_trans.trans_flag= cb_trans.trans_flag  ) = 1 and ( select last_status from ic_trans where ic_trans.doc_no = cb_trans.doc_no and ic_trans.trans_flag= cb_trans.trans_flag  ) = 0 ");

            __cbTransQuery.Append(") as temp1");
            //__cbTransQuery.Append(" group by " + _g.d.cb_trans._doc_no);

            #endregion

            #region Credit Card

            string __cbTransCreditCard = "select * from cb_trans_detail where " + __docDateTransFlagWhere + " and " + _g.d.cb_trans._trans_type + "=" + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and last_status = 0 and doc_type= 3";
            string __cbTransCoupon = "select * from cb_trans_detail where " + __docDateTransFlagWhere + " and " + _g.d.cb_trans._trans_type + "=" + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + " and last_status = 0 and doc_type= 9";


            #endregion

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTransStr));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTransDetailStr.ToString()));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__cbTransQuery.ToString()));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__cbTransCreditCard.ToString()));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__cbTransCoupon.ToString()));
            __query.Append("</node>");


            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __debug_query = __query.ToString();


            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            this._ic_trans_screen_bottom._loadData(((DataSet)__result[0]).Tables[0]);
            this._transGrid._loadFromDataTable(((DataSet)__result[1]).Tables[0]);
            this._screen_pay._loadData(((DataSet)__result[2]).Tables[0]);
            this._payCreditCardGridControl1._loadFromDataTable(((DataSet)__result[3]).Tables[0]);
            this._payCouponGridControl1._loadFromDataTable(((DataSet)__result[4]).Tables[0]);
            this._transGrid._calcTotal(true);
        }

        void _calc(MyLib._myGrid sender)
        {
            // แสดงยอดรวม
            int __columnTotalDiscount = this._transGrid._findColumnByName(_g.d.ic_trans_detail._discount_amount);
            int __columnTotalAmount = this._transGrid._findColumnByName(_g.d.ic_trans_detail._sum_amount);

            MyLib._myGrid._columnType __getColumnDiscount = (MyLib._myGrid._columnType)sender._columnList[__columnTotalDiscount];
            decimal __getTotalDiscountValue = __getColumnDiscount._total;

            MyLib._myGrid._columnType __getColumnSumAmount = (MyLib._myGrid._columnType)sender._columnList[__columnTotalAmount];
            decimal __getTotalSumAmountValue = __getColumnSumAmount._total;

            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_value, __getTotalSumAmountValue);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_discount, __getTotalDiscountValue);

            // 

            //string __manual = this._getDataStr(_g.d.ic_trans._total_manual).ToString();
            //if (__manual.Equals("1") == false)
            //{
            //decimal __totalAdvance = (this._advanceAmount == null) ? 0M : this._advanceAmount(this._vatType);
            //this._setDataNumber(_g.d.ic_trans._advance_amount, __totalAdvance);
            //
            decimal __totalValueVat = 0M;
            decimal __totalValueNoVat = 0M;
            for (int __row = 0; __row < this._transGrid._rowData.Count; __row++)
            {
                int __itemType = (int)this._transGrid._cellGet(__row, _g.d.ic_trans_detail._item_type);
                if (__itemType != 3 && __itemType != 5)
                {
                    // 0=มีภาษี,1=ยกเว้นภาษี
                    if ((int)this._transGrid._cellGet(__row, _g.d.ic_trans_detail._tax_type) == 0)
                    {
                        __totalValueVat += (decimal)this._transGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount);
                    }
                    else
                    {
                        __totalValueNoVat += (decimal)this._transGrid._cellGet(__row, _g.d.ic_trans_detail._sum_amount);
                    }
                }
            }
            //decimal __totalValue = this._getDataNumber(_g.d.ic_trans._total_value);
            decimal __totalValue = __totalValueVat + __totalValueNoVat;
            decimal __totalDiscount = __totalValue - MyLib._myGlobal._calcAfterDiscount(this._ic_trans_screen_bottom._getDataStr(_g.d.ic_trans._total_discount), __totalValue, _g.g._companyProfile._item_amount_decimal);
            decimal __vatRate = _g.g._companyProfile._vat_rate;

            decimal __beforeVat = 0;
            decimal __vatValue = 0;
            decimal __afterVat = 0;
            decimal __totalAmount = 0;
            //switch (this._vatType)
            //{
            //    case _g.g._vatTypeEnum.ภาษีแยกนอก:
            //        {
            //            __beforeVat = (__totalValueVat - __totalDiscount) - __totalAdvance;
            //            __vatValue = MyLib._myGlobal._round(__beforeVat * (__vatRate / 100.0M), _g.g._companyProfile._item_amount_decimal);
            //            __afterVat = __beforeVat + __vatValue;
            //            __totalAmount = __totalValueNoVat + __afterVat;
            //        }
            //        break;
            //    case _g.g._vatTypeEnum.ภาษีรวมใน:
            //        {
            __totalAmount = (__totalValue - __totalDiscount);
            __beforeVat = MyLib._myGlobal._round((__totalValueVat * 100.0M) / (100.0M + __vatRate), _g.g._companyProfile._item_amount_decimal);
            __vatValue = MyLib._myGlobal._round(__totalValueVat - __beforeVat, _g.g._companyProfile._item_amount_decimal);
            __afterVat = __beforeVat + __vatValue;
            //        }
            //        break;
            //    case _g.g._vatTypeEnum.ยกเว้นภาษี:
            //        __vatValue = 0;
            //        __totalAmount = (__totalValue - __totalDiscount) - __totalAdvance;
            //        break;
            //}
            //
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_value, __totalValueVat + __totalValueNoVat);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_discount, __totalDiscount);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_before_vat, __beforeVat);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_vat_value, __vatValue);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_after_vat, __afterVat);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_except_vat, __totalValueNoVat);
            this._ic_trans_screen_bottom._setDataNumber(_g.d.ic_trans._total_amount, __totalAmount);

        }

        void _clear()
        {
            this._transGrid._clear();
            this._ic_trans_screen_top._clear();
            this._ic_trans_screen_bottom._clear();
            this._screen_pay._clear();
            this._payCouponGridControl1._clear();
            this._payCreditCardGridControl1._clear();
            this._sale_pos_screen_top._clear();
        }

        void _save_data()
        {
            string __check = this._ic_trans_screen_top._checkEmtryField();


            bool __pass = true;
            if (__check.Length > 0)
            {
                MessageBox.Show("กรุณาป้อนข้อมูล " + __check, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                __pass = false;
            }


            if (this._transGrid._rowData.Count == 0 && __pass == true)
            {
                MessageBox.Show("ไม่พบรายการสินค้า ", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                __pass = false;
            }

            if (__pass)
            {
                ArrayList __screenTopResult = this._ic_trans_screen_top._createQueryForDatabase();
                ArrayList __screenBottomResult = this._ic_trans_screen_bottom._createQueryForDatabase();
                this._transGrid._updateRowIsChangeAll(true);

                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                string __docNo = this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._doc_no);
                string __docDate = this._ic_trans_screen_top._getDataStrQuery(_g.d.ic_trans._doc_date);
                string __docTime = this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._doc_time);
                string __custCode = this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._cust_code);
                int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                int __transType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                int __vat_type = 1;
                int __inquiry_type = 1;
                int __last_status = 0;
                int __is_pos = 0;
                int __calcFlag = _g.g._transCalcTypeGlobal._transStockCalcType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                string __wh_code = this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._wh_from);
                string __shelf_code = this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._location_from);
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_trans where doc_no = \'" + __docNo  + "\'"));
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_trans_detail where doc_no = \'" + __docNo + "\'"));

                string __transFieldList = MyLib._myGlobal._fieldAndComma(
                    _g.d.ic_trans._trans_flag,
                    _g.d.ic_trans._trans_type,
                    _g.d.ic_trans._last_status,
                    _g.d.ic_trans._is_pos,
                    _g.d.ic_trans._vat_type,
                    _g.d.ic_trans._inquiry_type
                    );

                string __transValueList = MyLib._myGlobal._fieldAndComma(__transFlag.ToString(), __transType.ToString(), __last_status.ToString(), __is_pos.ToString(), __vat_type.ToString(), __inquiry_type.ToString());

                string __transQuery = "insert into " + _g.d.ic_trans._table + " (" + __screenTopResult[0] + "," + __screenBottomResult[0] + "," + __transFieldList + ") values (" + __screenTopResult[1] + "," + __screenBottomResult[1] + "," + __transValueList + ")";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__transQuery));


                string __detailFieldList = MyLib._myGlobal._fieldAndComma(
                    _g.d.ic_trans_detail._doc_no,
                    _g.d.ic_trans_detail._doc_date,
                    _g.d.ic_trans_detail._doc_time,
                    _g.d.ic_trans_detail._cust_code,
                    _g.d.ic_trans_detail._trans_flag,
                    _g.d.ic_trans_detail._trans_type,
                    _g.d.ic_trans_detail._last_status,
                    _g.d.ic_trans_detail._is_pos,

                    _g.d.ic_trans_detail._inquiry_type,
                    _g.d.ic_trans_detail._vat_type,
                    _g.d.ic_trans_detail._wh_code,
                    _g.d.ic_trans_detail._shelf_code,
                    _g.d.ic_trans_detail._calc_flag,
                    _g.d.ic_trans_detail._doc_date_calc,
                    _g.d.ic_trans_detail._doc_time_calc) + ",";

                string __detailDataList = MyLib._myGlobal._fieldAndComma(
                    "\'" + __docNo + "\'",
                    "" + __docDate + "",
                    "\'" + __docTime + "\'",
                    "\'" + __custCode + "\'",
                    __transFlag.ToString(),
                    __transType.ToString(),
                    __last_status.ToString(),
                    __is_pos.ToString(),
                    __inquiry_type.ToString(),
                    __vat_type.ToString(),
                    "\'" + __wh_code + "\'",
                    "\'" + __shelf_code + "\'",
                    __calcFlag.ToString(),
                    "" + __docDate + "",
                    "\'" + __docTime + "\'") + ",";
                __query.Append(this._transGrid._createQueryForInsert(_g.d.ic_trans_detail._table, __detailFieldList, __detailDataList, false, true));

                // จ่ายเงิน
                int __paytype = 1;
                int __ap_ar_type = 1;
                string __cbTransField = MyLib._myGlobal._fieldAndComma(_g.d.cb_trans._doc_no, _g.d.cb_trans._doc_date, _g.d.cb_trans._doc_time, _g.d.cb_trans._trans_flag, _g.d.cb_trans._trans_type, _g.d.cb_trans._doc_format_code,
                    _g.d.cb_trans._pay_type
                    );
                string __cbTransData = MyLib._myGlobal._fieldAndComma(
                    "\'" + __docNo + "\'",
                    __docDate,
                    "\'" + __docTime + "\'",
                    __transFlag.ToString(),
                    __transType.ToString(),
                    "\'" + this._ic_trans_screen_top._getDataStr(_g.d.ic_trans._doc_format_code) + "\'",
                    __paytype.ToString()
                    );
                ArrayList __dataPayControl = this._screen_pay._createQueryForDatabase();

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_trans._table + "(" + __cbTransField + "," + __dataPayControl[0] + ") values(" + __cbTransData + "," + __dataPayControl[1] + ") "));

                string __fieldList = _g.d.cb_trans_detail._doc_no + "," + _g.d.cb_trans_detail._doc_date + "," + _g.d.cb_trans_detail._doc_time + "," + _g.d.cb_trans_detail._trans_type + "," + _g.d.cb_trans_detail._trans_flag + ",";
                string __dataList = "\'" + __docNo + "\'," + __docDate + ",\'" + __docTime + "\'," + __transType + "," + __transFlag + ",";

                // บัตรเครดิต
                this._payCreditCardGridControl1._updateRowIsChangeAll(true);
                __query.Append(this._payCreditCardGridControl1._createQueryForInsert(_g.d.cb_trans_detail._table,
                    __fieldList + _g.d.cb_trans_detail._doc_type + "," + _g.d.cb_trans_detail._trans_number_type + "," + _g.d.cb_trans_detail._ap_ar_type + "," + _g.d.cb_trans_detail._ap_ar_code + ",",
                    __dataList + "3," + __paytype.ToString() + "," + __ap_ar_type + ",\'" + __custCode + "\',"));

                // คูปอง
                this._payCouponGridControl1._updateRowIsChangeAll(true);
                __query.Append(this._payCouponGridControl1._createQueryForInsert(_g.d.cb_trans_detail._table,
                    __fieldList + _g.d.cb_trans_detail._doc_type + "," + _g.d.cb_trans_detail._trans_number_type + "," + _g.d.cb_trans_detail._ap_ar_type + "," + _g.d.cb_trans_detail._ap_ar_code + ",",
                    __dataList + "9," + __paytype.ToString() + "," + __ap_ar_type.ToString() + ",\'" + __custCode + "\',"));


                __query.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_global._datacenter_server, "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml", _global._datacenter_database_type);

                string __debugQuery = __query.ToString();
                string __result = __myFrameWork._queryList(_global._datacenter_database_name, __query.ToString());

                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, "");
                    this._clear();
                }
                else
                {
                    MessageBox.Show(__result, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }


}
