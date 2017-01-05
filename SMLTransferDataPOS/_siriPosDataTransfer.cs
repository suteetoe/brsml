using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLTransferDataPOS
{
    public partial class _siriPosDataTransfer : UserControl
    {
        MyLib._myGrid _gridTransTable = new MyLib._myGrid();
        MyLib._myGrid _gridCBTrans = new MyLib._myGrid();
        MyLib._myGrid _itemGrid = new MyLib._myGrid();
        MyLib._myGrid _gridCBTransDetail = new MyLib._myGrid();
        MyLib._myGrid _vatSale = new MyLib._myGrid();

        public string _columnSerialNumberCount = "serial_number_count";
        private string _columnPriceRoworder = "price_roworder";
        public string _columnAverageCostUnitStand = "average_cost_stand";
        public string _columnAverageCostUnitDiv = "average_cost_div";
        public string _columnSerialNumber = _g.d.ic_trans_detail._serial_number;

        string _defaultDocTime = "23:00";
        string _defaultDocFormatCode = "SIP";

        public _siriPosDataTransfer()
        {
            InitializeComponent();



            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._sale_pos_screen_top._maxColumn = 2;
            this._sale_pos_screen_top._table_name = _g.d.ic_trans_resource._table;
            this._sale_pos_screen_top._getResource = false;
            this._sale_pos_screen_top._addDateBox(0, 0, 1, 0, _g.d.ic_trans_resource._from_doc_date, 1, true, true, true, "จากวันที่");
            this._sale_pos_screen_top._addDateBox(0, 1, 1, 0, _g.d.ic_trans_resource._to_doc_date, 1, true, true, true, "ถึงวันที่");
            //this._sale_pos_screen_top._addTextBox(1, 0, 1, 0, _g.d.ic_trans._branch_code, 1, 0, 0, true, false, true, false, false, "สาขา");
            //this._sale_pos_screen_top._addCheckBox(2, 0, _g.d.ap_ar_trans_detail._sum_value, true, false, false, "สรุปตามช่วงเวลา");
            //this._sale_pos_screen_top._addCheckBox(2, 1, _g.d.ic_trans._so_cn_balance, true, false, false, "หักรายการลดหนี้");
            //this._sale_pos_screen_top._addCheckBox(3, 0, _g.d.ic_trans._credit_sale, true, false, false, "บันทึกเป็นรายการขายเชื่อ");

            this._sale_pos_screen_top._setDataDate(_g.d.ic_trans_resource._from_doc_date, MyLib._myGlobal._workingDate);
            this._sale_pos_screen_top._setDataDate(_g.d.ic_trans_resource._to_doc_date, MyLib._myGlobal._workingDate);

            this._transGrid._table_name = _g.d.ic_trans._table;
            this._transGrid._isEdit = false;

            this._transGrid._addColumn("check", 11, 1, 2, false, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans._doc_date, 4, 1, 15, false, false, true, true);
            this._transGrid._addColumn(_g.d.ic_trans._pos_id, 1, 1, 20, false, false, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._doc_no, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
            this._transGrid._addColumn(_g.d.ic_trans._total_amount, 3, 1, 8, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
            this._transGrid._addColumn(_g.d.ic_trans._status, 1, 1, 8, true, false, false, false, __formatNumberQty);

            this._transGrid._calcPersentWidthToScatter();

            /* screen top for save data */

            int __row = 0;

            #region trans

            /*this._screen_top._table_name = _g.d.ic_trans._table;
            this._screen_top._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
            this._screen_top._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 0);
            this._screen_top._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_no, 1, 0, 1, true, false, false);
            this._screen_top._addTextBox(__row++, 3, _g.d.ic_trans._doc_format_code, 1);
            this._screen_top._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 2, 0, 1, true, false, false, true, true, _g.d.ic_trans._ar_code);
            this._screen_top._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 1, true, false, false);
            this._screen_top._addTextBox(__row++, 3, 1, 0, _g.d.ic_trans._location_from, 1, 1, 1, true, false, false);

            this._screen_top._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumberAmount);
            this._screen_top._addNumberBox(__row, 1, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumberAmount);
            this._screen_top._addNumberBox(__row, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumberAmount);
            this._screen_top._addNumberBox(__row++, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumberAmount);
            this._screen_top._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 0);
            this._screen_top._addNumberBox(__row, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumberAmount);
            this._screen_top._addNumberBox(__row++, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumberAmount);
            this._screen_top._addNumberBox(__row, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumberAmount);
            this._screen_top._addNumberBox(__row++, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumberAmount);

            this._screen_top._setDataNumber(_g.d.ic_trans._vat_rate, _g.g._companyProfile._vat_rate);

            this._screen_top._enabedControl(_g.d.ic_trans._total_discount, false);
            this._screen_top._enabedControl(_g.d.ic_trans._total_before_vat, false);
            this._screen_top._enabedControl(_g.d.ic_trans._vat_rate, false);
            this._screen_top._enabedControl(_g.d.ic_trans._total_value, false);
            this._screen_top._enabedControl(_g.d.ic_trans._total_vat_value, false);
            this._screen_top._enabedControl(_g.d.ic_trans._total_after_vat, false);
            this._screen_top._enabedControl(_g.d.ic_trans._total_except_vat, false);
            this._screen_top._enabedControl(_g.d.ic_trans._total_amount, false);*/

            this._gridTransTable._table_name = _g.d.ic_trans._table;
            this._gridTransTable._isEdit = false;

            this._gridTransTable._addColumn(_g.d.ic_trans._trans_flag, 2, 1, 5, false, false, true);
            this._gridTransTable._addColumn(_g.d.ic_trans._trans_type, 2, 1, 5, false, false, true);

            this._gridTransTable._addColumn(_g.d.ic_trans._doc_date, 4, 255, 5);
            this._gridTransTable._addColumn(_g.d.ic_trans._doc_time, 1, 20, 5);
            this._gridTransTable._addColumn(_g.d.ic_trans._doc_no, 1, 50, 5);

            this._gridTransTable._addColumn(_g.d.ic_trans._tax_doc_date, 4, 255, 5);
            this._gridTransTable._addColumn(_g.d.ic_trans._tax_doc_no, 1, 50, 5);


            this._gridTransTable._addColumn(_g.d.ic_trans._doc_format_code, 1, 50, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._cust_code, 1, 50, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._wh_from, 1, 50, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._location_from, 1, 50, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._remark, 1, 255, 10);

            this._gridTransTable._addColumn(_g.d.ic_trans._is_pos, 2, 10, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._inquiry_type, 2, 10, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._vat_type, 2, 10, 10);

            this._gridTransTable._addColumn(_g.d.ic_trans._discount_word, 1, 255, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._total_discount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransTable._addColumn(_g.d.ic_trans._total_before_vat, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransTable._addColumn(_g.d.ic_trans._vat_rate, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransTable._addColumn(_g.d.ic_trans._total_value, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransTable._addColumn(_g.d.ic_trans._total_vat_value, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransTable._addColumn(_g.d.ic_trans._total_after_vat, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransTable._addColumn(_g.d.ic_trans._total_except_vat, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._gridTransTable._addColumn(_g.d.ic_trans._total_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);

            this._gridTransTable._addColumn(_g.d.ic_trans._branch_code, 1, 50, 10);
            this._gridTransTable._addColumn(_g.d.ic_trans._department_code, 1, 50, 10);



            this.tabPage2.Controls.Add(this._gridTransTable);
            this._gridTransTable.Dock = DockStyle.Fill;

            #endregion

            #region trans_detail

            this._itemGrid._table_name = _g.d.ic_trans_detail._table;
            this._itemGrid._isEdit = false;

            // ซ่อน
            this._itemGrid._addColumn(_g.d.ic_trans_detail._doc_no, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._doc_date, 4, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._trans_flag, 2, 1, 5, false, false, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._trans_type, 2, 1, 5, false, false, true);

            this._itemGrid._addColumn(_g.d.ic_trans_detail._doc_time, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._cust_code, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._inquiry_type, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._is_pos, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._wh_code, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._calc_flag, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._doc_date_calc, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._doc_time_calc, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._vat_type, 1, 10, 10);

            // รายการ

            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, false, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._barcode, 1, 1, 12, false, (_g.g._companyProfile._use_barcode) ? false : true, true, false);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 10, false, false, true, false);

            this._itemGrid._addColumn(_g.d.ic_trans_detail._discount_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
            this._itemGrid._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);

            // field ซ่อน
            this._itemGrid._addColumn(_g.d.ic_trans_detail._extra, 12, 1, 5, false, true, false);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._dimension, 12, 1, 5, false, true, false);
            // Field ซ่อน
            this._itemGrid._addColumn(_g.d.ic_trans_detail._unit_name, 1, 1, 0, false, true, false);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._unit_type, 2, 1, 0, false, true, false);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._stand_value, 3, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._divide_value, 3, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_type, 2, 1, 10, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_code_main, 1, 1, 10, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._is_permium, 2, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._is_get_price, 2, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._price_exclude_vat, 3, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._total_vat_value, 3, 1, 15, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._sum_amount_exclude_vat, 3, 1, 0, false, true, true, false, __formatNumberAmount);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._hidden_cost_1_exclude_vat, 3, 1, 0, false, true, true);

            this._itemGrid._addColumn(this._columnAverageCostUnitStand, 3, 1, 0, false, true, false);
            this._itemGrid._addColumn(this._columnAverageCostUnitDiv, 3, 1, 0, false, true, false);
            this._itemGrid._addColumn(this._columnPriceRoworder, 2, 1, 0, false, true, false);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._user_approve, 1, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._price_mode, 1, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._price_type, 1, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._is_serial_number, 2, 1, 0, false, true, true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._tax_type, 2, 1, 0, false, true, true);
            this._itemGrid._addColumn(this._columnSerialNumber, 12, 1, 0, false, true, false);
            this._itemGrid._addColumn(this._columnSerialNumberCount, 3, 1, 0, false, true, false);

            this._itemGrid._calcPersentWidthToScatter();

            this.tabPage3.Controls.Add(this._itemGrid);
            this._itemGrid.Dock = DockStyle.Fill;
            #endregion

            #region Pay Detail

            // pay control

            this._gridCBTrans._table_name = _g.d.cb_trans._table;

            this._gridCBTrans._addColumn(_g.d.cb_trans._doc_no, 1, 50, 10);
            this._gridCBTrans._addColumn(_g.d.cb_trans._doc_date, 4, 20, 10);
            this._gridCBTrans._addColumn(_g.d.cb_trans._doc_time, 1, 10, 10);

            this._gridCBTrans._addColumn(_g.d.cb_trans._trans_flag, 2, 1, 5, false, false, true);
            this._gridCBTrans._addColumn(_g.d.cb_trans._trans_type, 2, 1, 5, false, false, true);

            this._gridCBTrans._addColumn(_g.d.cb_trans._total_amount, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._total_net_amount, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._cash_amount, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._total_income_amount, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._card_amount, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._coupon_amount, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._point_qty, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._point_amount, 3, 10, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTrans._addColumn(_g.d.cb_trans._total_amount_pay, 3, 10, 10, true, false, true, false, __formatNumberAmount);

            this.tabPage4.Controls.Add(this._gridCBTrans);
            this._gridCBTrans.Dock = DockStyle.Fill;

            /*
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
            this._screen_pay._addNumberBox(7, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumberAmount);*/

            /* this._screen_pay._enabedControl(_g.d.cb_trans._total_amount, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._total_net_amount, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._cash_amount, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._total_income_amount, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._card_amount, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._coupon_amount, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._point_qty, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._point_amount, false);
             this._screen_pay._enabedControl(_g.d.cb_trans._total_amount_pay, false);*/

            #endregion

            //this._payCreditCardGridControl1._isEdit = false;
            //this._payCouponGridControl1._isEdit = false;

            this._gridCBTransDetail._table_name = _g.d.cb_trans_detail._table;

            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._doc_no, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._doc_date, 4, 20, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._doc_time, 1, 10, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._doc_ref, 1, 50, 10);

            // all
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._trans_flag, 2, 1, 5, false, false, true);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._trans_type, 2, 1, 5, false, false, true);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._doc_type, 2, 1, 0, false, false, true);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._ap_ar_code, 1, 10, 10);

            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._trans_number, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._trans_number_type, 2, 1, 0, false, false, true);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._doc_date_ref, 4, 20, 10);

            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._pass_book_code, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._bank_code, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._bank_branch, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._exchange_rate, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._fee_amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._other_amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._tax_at_pay, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._sum_amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);

            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._remark, 1, 50, 10);
            // petty cash

            // credit
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._no_approved, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._approve_code, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._approve_date, 4, 20, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._charge, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._credit_card_type, 1, 50, 10);

            // chq
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._chq_ref, 1, 50, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._chq_due_date, 4, 20, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._chq_date, 4, 20, 10);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._external_chq, 2, 1, 0, false, false, true);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._chq_on_hand, 2, 1, 0, false, false, true);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._balance_amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);

            /*this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._gridCBTransDetail._addColumn(_g.d.cb_trans_detail._amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);*/

            this.tabPage5.Controls.Add(this._gridCBTransDetail);
            this._gridCBTransDetail.Dock = DockStyle.Fill;

            #region Vat Sale

            this._vatSale._table_name = _g.d.gl_journal_vat_sale._table;
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._doc_no, 1, 50, 10);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._doc_date, 4, 20, 10);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._trans_flag, 2, 1, 5, false, false, true);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._trans_type, 2, 1, 5, false, false, true);

            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._vat_number, 1, 25, 15, true, false, true, false);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._vat_date, 4, 0, 10);

            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._base_caltax_amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._tax_rate, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._except_tax_amount, 3, 0, 10, true, false, true, false, __formatNumberAmount);

            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._is_add, 11, 10, 6, true);

            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._ar_code, 1, 10, 10);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._ar_name, 1, 255, 4, true);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._tax_no, 1, 25, 4, true);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._branch_type, 10, 10, 4, true);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._branch_code, 1, 25, 4, true);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._manual_add, 2, 0, 5, false, true, true);

            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._vat_effective_period, 2, 0, 5, true, false, true, false);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._vat_effective_year, 2, 0, 5, true, false, true, false);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._description, 1, 255, 20, true, false);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._tax_group, 1, 10, 10, true, false, true, true);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._vat_calc, 2, 0, 5, true, false, true, false);
            this._vatSale._addColumn(_g.d.gl_journal_vat_sale._period_number, 2, 0, 5, true, false, true, false);

            this.tabPage6.Controls.Add(this._vatSale);
            this._vatSale.Dock = DockStyle.Fill;

            #endregion

            string __computerName = SystemInformation.ComputerName.ToLower();
            if (__computerName.IndexOf("toe-pc") == -1)
            {
                this.tabControl1.TabPages.Remove(tabPage2);
                this.tabControl1.TabPages.Remove(tabPage3);
                this.tabControl1.TabPages.Remove(tabPage4);
                this.tabControl1.TabPages.Remove(tabPage5);
                this.tabControl1.TabPages.Remove(tabPage6);
            }

        }


        private void _loadButton_Click(object sender, EventArgs e)
        {
            // load sale sum by date
            _loadData();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _loadData()
        {
            // branch filter

            // load sale doc
            string __docDateTransFlagWhere = _g.d.ic_trans._doc_date + " between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) +
                "  and " + _g.d.ic_trans._trans_flag + " in ( " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + ", " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " ) and " + _g.d.ic_trans._last_status + "=0";

            StringBuilder __branchList = new StringBuilder();
            string __getBranchCode = this._sale_pos_screen_top._getDataStr(_g.d.ic_trans._branch_code);
            if (__getBranchCode.Length > 0)
            {
                string[] branch = __getBranchCode.Split(',');
                foreach (string getBranch in branch)
                {
                    if (__branchList.Length > 0)
                    {
                        __branchList.Append(",");
                    }

                    __branchList.Append("\'" + getBranch.ToUpper().Trim() + "\'");

                }

            }


            string __selectTransField = MyLib._myGlobal._fieldAndComma(
               _g.d.ic_trans._doc_date,
               _g.d.ic_trans._pos_id,
               " \'POS\'  || \'-\' || upper(pos_id) || \'-\' || to_char(doc_date, \'YYYYMMDD\') as " + _g.d.ic_trans._doc_no,
               " sum(" + _g.d.ic_trans._total_amount + ") as " + _g.d.ic_trans._total_amount
               );

            string __queryLoad = "select " + __selectTransField + " from " + _g.d.ic_trans._table + " where " + __docDateTransFlagWhere + " and is_pos = 1 group by " + _g.d.ic_trans._doc_date + ",pos_id order by " + _g.d.ic_trans._doc_date + ", pos_id ";


            // sum range
            if (this._sale_pos_screen_top._getDataStr(_g.d.ap_ar_trans_detail._sum_value).Equals("1"))
            {
                __queryLoad = " select " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) + " as doc_date,  'SALE'  || '-' || upper(branch_code) || '-' || " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date).Replace("-", string.Empty) + " as doc_no,  branch_code, sum(total_amount) as total_amount from ( " + __queryLoad + " ) as temp2 group by branch_code ";
            }
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            //string __debug_query = __query.ToString();


            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __queryLoad.ToString());
            if (__result.Tables.Count > 0)
            {
                this._transGrid._loadFromDataTable(__result.Tables[0]);

                for (int __row = 0; __row < this._transGrid._rowData.Count; __row++)
                {
                    this._transGrid._cellUpdate(__row, 0, 1, true);
                }
            }

            /*this._ic_trans_screen_bottom._loadData(((DataSet)__result[0]).Tables[0]);
            this._transGrid._loadFromDataTable(((DataSet)__result[1]).Tables[0]);
            this._screen_pay._loadData(((DataSet)__result[2]).Tables[0]);
            this._payCreditCardGridControl1._loadFromDataTable(((DataSet)__result[3]).Tables[0]);
            this._payCouponGridControl1._loadFromDataTable(((DataSet)__result[4]).Tables[0]);
            this._transGrid._calcTotal(true);*/



        }

        bool _cnImport = false;

        void _loadDataProcess(string docDate, string branch_code, int row)
        {
            //Boolean __sumPeriod = this._sale_pos_screen_top._getDataStr(_g.d.ap_ar_trans_detail._sum_value).Equals("1");
            //Boolean __cncut = this._sale_pos_screen_top._getDataStr(_g.d.ic_trans._so_cn_balance).Equals("1");

            string __branchCodeDetailWhere = " (select pos_id from ic_trans where ic_trans.doc_no = {0}.doc_no and ic_trans.trans_flag ={0}.trans_flag) ";

            // load sale doc and branch_code = \'" + branch_code + "\'
            string __docDateTransFlagWhere = _g.d.ic_trans._doc_date + " = \'" + docDate + "\' " + " and " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);

            //if (__sumPeriod)
            //{
            //    //  and branch_code = \'" + branch_code + "\'
            //    __docDateTransFlagWhere = _g.d.ic_trans._doc_date + " between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) +
            //    "" +
            //    "  and " + _g.d.ic_trans._trans_flag + " = " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
            //}
            // pos_only_filter
            // load doc_no

            // เอาเข้ารายการสุดท้ายของเอกสาร

            string __docNo = this._transGrid._cellGet(row, _g.d.ic_trans._doc_no).ToString();

            #region Trans Query
            string __selectTransField = MyLib._myGlobal._fieldAndComma(

                // group
                "\'" + docDate + "\' as " + _g.d.ic_trans._doc_date,
                "(select code from ic_warehouse where user_group = \'" + branch_code + "\' ) as " + _g.d.ic_trans._wh_from,
                "(select code from ic_shelf where whcode = (select code from ic_warehouse where user_group = '" + branch_code + "' ) limit 1 ) as " + _g.d.ic_trans._location_from,
                "\'" + __docNo + "\' as " + _g.d.ic_trans._doc_no,

                "\'" + docDate + "\' as " + _g.d.ic_trans._tax_doc_date,
                "\'" + __docNo + "\' as " + _g.d.ic_trans._tax_doc_no,

                " \'00009\' as  " + _g.d.ic_trans._cust_code,
                " \'" + _defaultDocFormatCode + "\' as  " + _g.d.ic_trans._doc_format_code,
                "\'" + _defaultDocTime + "\' as " + _g.d.ic_trans._doc_time,
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.ic_trans._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.ic_trans._trans_type,

                " sum(" + _g.d.ic_trans._total_value + ") as " + _g.d.ic_trans._total_value,
                " sum(" + _g.d.ic_trans._total_discount + ") as " + _g.d.ic_trans._total_discount,
                //                                " sum(" + _g.d.ic_trans._total_discount + ") as " + _g.d.ic_trans._discount_word,

                " sum(" + _g.d.ic_trans._total_after_discount + ") as " + _g.d.ic_trans._total_after_discount,
                " sum(" + _g.d.ic_trans._total_except_vat + ") as " + _g.d.ic_trans._total_except_vat,
                " sum(" + _g.d.ic_trans._total_before_vat + ") as " + _g.d.ic_trans._total_before_vat,
                " sum(" + _g.d.ic_trans._total_after_vat + ") as " + _g.d.ic_trans._total_after_vat,
                " sum(" + _g.d.ic_trans._total_vat_value + ") as " + _g.d.ic_trans._total_vat_value,
                " sum(" + _g.d.ic_trans._total_amount + ") as " + _g.d.ic_trans._total_amount,
                " (select vat_rate from erp_option limit 1 ) as " + _g.d.ic_trans._vat_rate,
                " 0 as " + _g.d.ic_trans._is_pos,
                " 1 as " + _g.d.ic_trans._inquiry_type,
                " 1 as " + _g.d.ic_trans._vat_type,
                " (select branch_code from pos_id where machinecode = \'" + branch_code + "\' ) as " + _g.d.ic_trans._branch_code,
                " (select department_code from pos_id where machinecode = \'" + branch_code + "\' ) as " + _g.d.ic_trans._department_code
                );

            string __transWhere = _g.d.ic_trans._last_status + " = 0 and is_pos = 1 ";

            string __queryTrans = "select " + MyLib._myGlobal._fieldAndComma(
                _g.d.ic_trans._doc_date,
                _g.d.ic_trans._total_value,
                _g.d.ic_trans._total_discount,
                _g.d.ic_trans._total_value + " - " + _g.d.ic_trans._total_discount + " as " + _g.d.ic_trans._total_after_discount,
                _g.d.ic_trans._total_before_vat,
                _g.d.ic_trans._total_after_vat,
                _g.d.ic_trans._total_except_vat,
                _g.d.ic_trans._total_vat_value,
                _g.d.ic_trans._total_amount
                ) + " from " + _g.d.ic_trans._table + " where " + __docDateTransFlagWhere + " and " + __transWhere + " and pos_id = \'" + branch_code + "\' ";


            // ขาย หลังร้าน
            string __queryCreditSale = " select doc_date " +
                ", (select code from ic_warehouse where user_group = \'" + branch_code + "\' ) as wh_from " +
                ", (select code from ic_shelf where whcode = (select code from ic_warehouse where user_group = \'" + branch_code + "\' ) limit 1 ) as location_from " +
                ", doc_no, tax_doc_date, tax_doc_no, cust_code, doc_format_code, doc_time, trans_flag, trans_type" +
                ", total_value, total_discount, total_value - total_discount as total_after_discount, total_except_vat, total_before_vat, total_after_vat, total_vat_value, total_amount, vat_rate, is_pos, inquiry_type, vat_type" +
                " from ic_trans where doc_date = \'" + docDate + "\' and branch_code = \'" + branch_code + "\' and trans_flag = 44 and last_status = 0  and is_pos = 0 ";

            string __getCNTrans = " select doc_date " +
                ", wh_from " +
                ", location_from " +
                ", doc_no, tax_doc_date, tax_doc_no, cust_code, doc_format_code, doc_time, trans_flag, trans_type" +
                ", total_value, total_discount, total_value - total_discount as total_after_discount, total_except_vat, total_before_vat, total_after_vat, total_vat_value, total_amount, vat_rate, is_pos, inquiry_type, vat_type" +
                ", " + _g.d.ic_trans._branch_code + "," + _g.d.ic_trans._department_code +
                " from ic_trans where doc_date = \'" + docDate + "\' and trans_flag = 48 and last_status = 0 ";

            // ประกอบ 
            string __queryTransStr = "select " + __selectTransField + " from (" + __queryTrans + ") as temp1 " + ((_cnImport == true) ? " union all " + __getCNTrans : ""); // + " union all " + __queryCreditSale + " union all " + __getCNTrans;


            /*
            if (__cncut)
            {
                __queryTransStr = "select " + __selectTransField + " from (select " +

                    MyLib._myGlobal._fieldAndComma(

                    _g.d.ic_trans._doc_date,
                    " (case when trans_flag = 48 then -1*total_value else total_value end) as " + _g.d.ic_trans._total_value,
                    " (case when trans_flag = 48 then -1*total_discount else total_discount end) as " + _g.d.ic_trans._total_discount,
                    " (case when trans_flag = 48 then -1*total_after_discount else total_after_discount end) as " + _g.d.ic_trans._total_after_discount,
                    " (case when trans_flag = 48 then -1*total_before_vat else total_before_vat end) as " + _g.d.ic_trans._total_before_vat,
                    " (case when trans_flag = 48 then -1*total_after_vat else total_after_vat end) as " + _g.d.ic_trans._total_after_vat,
                    " (case when trans_flag = 48 then -1*total_except_vat else total_except_vat end) as " + _g.d.ic_trans._total_except_vat,
                    " (case when trans_flag = 48 then -1*total_vat_value else total_vat_value end) as " + _g.d.ic_trans._total_vat_value,
                    " (case when trans_flag = 48 then -1*total_amount else total_amount end) as " + _g.d.ic_trans._total_amount
                    ) +
                    " from (" + "select " + MyLib._myGlobal._fieldAndComma(

                    _g.d.ic_trans._trans_flag,
                    _g.d.ic_trans._doc_date,
                    _g.d.ic_trans._total_value,
                    _g.d.ic_trans._total_discount,
                    _g.d.ic_trans._total_value + " - " + _g.d.ic_trans._total_discount + " as " + _g.d.ic_trans._total_after_discount,
                    _g.d.ic_trans._total_before_vat,
                    _g.d.ic_trans._total_after_vat,
                    _g.d.ic_trans._total_except_vat,
                    _g.d.ic_trans._total_vat_value,
                    _g.d.ic_trans._total_amount
                    ) + " from " + _g.d.ic_trans._table + " where " +
                _g.d.ic_trans._doc_date + " between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) +
                "" +
                "  and ((" + _g.d.ic_trans._trans_flag + " = 44 and is_pos = 1 ) or (" + _g.d.ic_trans._trans_flag + " = 48 )) " +

                " and last_status = 0 and branch_code = \'" + branch_code + "\' " + ") as temp1 ) as temp2 ";
            }*/
            #endregion

            // query trans detail
            #region get ic_trans_detail

            //string __fullInvoiceFilter = " and ((select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + ") is null or (select " + _g.d.ic_trans._doc_ref + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + ")  = '')";
            string __selectField = MyLib._myGlobal._fieldAndComma(

               // maste field
               "\'" + __docNo + "\' as " + _g.d.ic_trans_detail._doc_no,
                "\'" + docDate + "\' as " + _g.d.ic_trans_detail._doc_date,
                "\'23:59\' as " + _g.d.ic_trans_detail._doc_time,

                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.ic_trans_detail._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.ic_trans_detail._trans_type,

                "\'00009\' as  " + _g.d.ic_trans._cust_code,
                "1 as " + _g.d.ic_trans_detail._inquiry_type,
                "0 as " + _g.d.ic_trans_detail._is_pos,
                _g.d.ic_trans_detail._wh_code,//"(select code from ic_warehouse where upper(user_group) = \'" + branch_code.ToUpper() + "\' ) as " + _g.d.ic_trans_detail._wh_code,
                _g.d.ic_trans_detail._shelf_code,//"(select code from ic_shelf where whcode = (select code from ic_warehouse where upper(user_group) = '" + branch_code.ToUpper() + "' ) limit 1 ) as " + _g.d.ic_trans_detail._shelf_code,
                "-1 as " + _g.d.ic_trans_detail._calc_flag,
                "\'" + docDate + "\' as " + _g.d.ic_trans_detail._doc_date_calc,
                "\'23:59\' as " + _g.d.ic_trans_detail._doc_time_calc,
                " 1 as " + _g.d.ic_trans_detail._vat_type,

                _g.d.ic_trans_detail._item_code,
                _g.d.ic_trans_detail._item_name,
                _g.d.ic_trans_detail._barcode,
                _g.d.ic_trans_detail._unit_code,
                "sum(" + _g.d.ic_trans_detail._qty + ") as " + _g.d.ic_trans_detail._qty,
                _g.d.ic_trans_detail._price,
                "sum(" + _g.d.ic_trans_detail._discount_amount + ") as " + _g.d.ic_trans_detail._discount_amount,
                "sum(" + _g.d.ic_trans_detail._discount_amount + ") as " + _g.d.ic_trans_detail._discount,
                "sum(" + _g.d.ic_trans_detail._sum_amount + ") as " + _g.d.ic_trans_detail._sum_amount,
                "sum(" + _g.d.ic_trans_detail._total_vat_value + ") as " + _g.d.ic_trans_detail._total_vat_value,
                "sum(" + _g.d.ic_trans_detail._sum_amount_exclude_vat + ") as " + _g.d.ic_trans_detail._sum_amount_exclude_vat,
                "sum(" + _g.d.ic_trans_detail._price_exclude_vat + ") as " + _g.d.ic_trans_detail._price_exclude_vat,
                _g.d.ic_trans_detail._stand_value,
                _g.d.ic_trans_detail._divide_value);

            string __queryTransDetail = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._barcode + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._price + "," + _g.d.ic_trans_detail._discount_amount + "," + _g.d.ic_trans_detail._sum_amount + "," + _g.d.ic_trans_detail._stand_value + "," + _g.d.ic_trans_detail._divide_value + "," + _g.d.ic_trans_detail._total_vat_value + "," + _g.d.ic_trans_detail._sum_amount_exclude_vat + "," + _g.d.ic_trans_detail._price_exclude_vat + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code +
                " from " + _g.d.ic_trans_detail._table +
                " where " + __docDateTransFlagWhere + " and " + _g.d.ic_trans_detail._last_status + "=0 and exists (select doc_no from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag and ic_trans.is_pos = 1 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) and " + string.Format(__branchCodeDetailWhere, _g.d.ic_trans_detail._table) + " =\'" + branch_code + "\'";

            // sale detail credit

            string __queryItemSaleCredit = " select " + MyLib._myGlobal._fieldAndComma(

                    // master field
                    _g.d.ic_trans_detail._doc_no,
                    _g.d.ic_trans_detail._doc_date,
                    _g.d.ic_trans_detail._doc_time,
                    _g.d.ic_trans_detail._trans_flag,
                    _g.d.ic_trans_detail._trans_type,
                    _g.d.ic_trans_detail._cust_code,
                    "0 as " + _g.d.ic_trans_detail._inquiry_type,
                    "0 as " + _g.d.ic_trans_detail._is_pos,
                    _g.d.ic_trans_detail._wh_code,
                    _g.d.ic_trans_detail._shelf_code,
                    _g.d.ic_trans_detail._calc_flag,
                    _g.d.ic_trans_detail._doc_date_calc,
                    _g.d.ic_trans_detail._doc_time_calc,
                    _g.d.ic_trans_detail._vat_type,

                    _g.d.ic_trans_detail._item_code,
                    _g.d.ic_trans_detail._item_name,
                    _g.d.ic_trans_detail._barcode,
                    _g.d.ic_trans_detail._unit_code,
                    _g.d.ic_trans_detail._qty,
                    _g.d.ic_trans_detail._price,
                    _g.d.ic_trans_detail._discount_amount,
                    _g.d.ic_trans_detail._discount_amount + " as " + _g.d.ic_trans_detail._discount,
                    _g.d.ic_trans_detail._sum_amount,
                    _g.d.ic_trans_detail._total_vat_value,
                    _g.d.ic_trans_detail._sum_amount_exclude_vat,
                    _g.d.ic_trans_detail._price_exclude_vat,
                    _g.d.ic_trans_detail._stand_value,
                    _g.d.ic_trans_detail._divide_value
                ) +
                " from " + _g.d.ic_trans_detail._table +
                " where " + __docDateTransFlagWhere + " and " + _g.d.ic_trans_detail._last_status + "=0 and exists (select doc_no from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no and ic_trans.trans_flag = ic_trans_detail.trans_flag and ic_trans.is_pos = 0 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) " + " and " + string.Format(__branchCodeDetailWhere, _g.d.ic_trans_detail._table) + "= \'" + branch_code + "\'";

            string __queryItemCN = " select " + MyLib._myGlobal._fieldAndComma(

                   // master field
                   _g.d.ic_trans_detail._doc_no,
                   _g.d.ic_trans_detail._doc_date,
                   _g.d.ic_trans_detail._doc_time,
                   _g.d.ic_trans_detail._trans_flag,
                   _g.d.ic_trans_detail._trans_type,
                   _g.d.ic_trans_detail._cust_code,
                   _g.d.ic_trans_detail._inquiry_type,
                   "0 as " + _g.d.ic_trans_detail._is_pos,
                   _g.d.ic_trans_detail._wh_code,
                   _g.d.ic_trans_detail._shelf_code,
                   _g.d.ic_trans_detail._calc_flag,
                   _g.d.ic_trans_detail._doc_date_calc,
                   _g.d.ic_trans_detail._doc_time_calc,
                   _g.d.ic_trans_detail._vat_type,

                   _g.d.ic_trans_detail._item_code,
                   _g.d.ic_trans_detail._item_name,
                   _g.d.ic_trans_detail._barcode,
                   _g.d.ic_trans_detail._unit_code,
                   _g.d.ic_trans_detail._qty,
                   _g.d.ic_trans_detail._price,
                   _g.d.ic_trans_detail._discount_amount,
                   _g.d.ic_trans_detail._discount_amount + " as " + _g.d.ic_trans_detail._discount,
                   _g.d.ic_trans_detail._sum_amount,
                   _g.d.ic_trans_detail._total_vat_value,
                   _g.d.ic_trans_detail._sum_amount_exclude_vat,
                   _g.d.ic_trans_detail._price_exclude_vat,
                   _g.d.ic_trans_detail._stand_value,
                   _g.d.ic_trans_detail._divide_value
               ) +
               " from " + _g.d.ic_trans_detail._table +
               " where doc_date = \'" + docDate + "\' and trans_flag = 48 and " + _g.d.ic_trans_detail._last_status + "=0 "; // and " + string.Format(__branchCodeDetailWhere, _g.d.ic_trans_detail._table) + " =\'" + branch_code + "\' 


            StringBuilder __queryTransDetailStr = new StringBuilder();

            __queryTransDetailStr.Append("select " + __selectField + " from (" + __queryTransDetail + ") as temp1 ");
            __queryTransDetailStr.Append(" where  " + _g.d.ic_trans_detail._item_code + " != '' ");
            __queryTransDetailStr.Append(" group by " + _g.d.ic_trans_detail._item_code + "," +
                _g.d.ic_trans_detail._item_name + "," +
                _g.d.ic_trans_detail._barcode + "," +
                _g.d.ic_trans_detail._unit_code + "," +
                _g.d.ic_trans_detail._price + "," +
                _g.d.ic_trans_detail._stand_value + "," +
                _g.d.ic_trans_detail._divide_value + "," +
                _g.d.ic_trans_detail._wh_code + "," +
                _g.d.ic_trans_detail._shelf_code);

            // __queryTransDetailStr.Append(" order by doc_no");

            // union all 
            //__queryTransDetailStr.Append(" union all  " + __queryItemSaleCredit);
            if (_cnImport == true)
                __queryTransDetailStr.Append(" union all " + __queryItemCN);

            // end ic_trans_detail


            /*if (__cncut)
            {
                __queryTransDetailStr = new StringBuilder();
                __queryTransDetailStr.Append("select " + __selectField + " from (");


                __queryTransDetailStr.Append("select " +
                    _g.d.ic_trans_detail._item_code + "," +
                    _g.d.ic_trans_detail._item_name + "," +
                     _g.d.ic_trans_detail._barcode + "," + // " ( case when trans_flag = 48 then (select barcode from ic_trans_detail as d where d.item_code = ic_trans_detail.item_code and d.unit_code = ic_trans_detail.unit_code and trans_flag = 44 limit 1 ) else barcode end) as " +
                    _g.d.ic_trans_detail._unit_code + "," +
                    " (case when trans_flag = 48 then -1*qty else qty end) as " + _g.d.ic_trans_detail._qty + "," +
                    _g.d.ic_trans_detail._price + "," +  // " ( case when trans_flag = 48 then (select price from ic_trans_detail as d where d.item_code = ic_trans_detail.item_code and d.unit_code = ic_trans_detail.unit_code and trans_flag = 44 limit 1 ) else price end) as " + 
                    _g.d.ic_trans_detail._discount_amount + "," +
                    " (case when trans_flag = 48 then -1*sum_amount else sum_amount end) as " + _g.d.ic_trans_detail._sum_amount + "," +
                    _g.d.ic_trans_detail._stand_value + "," +
                    _g.d.ic_trans_detail._divide_value + "," +
                    " (case when trans_flag = 48 then -1*total_vat_value else total_vat_value end) as " + _g.d.ic_trans_detail._total_vat_value + "," +
                    " (case when trans_flag = 48 then -1*sum_amount_exclude_vat else sum_amount_exclude_vat end) as " + _g.d.ic_trans_detail._sum_amount_exclude_vat + "," +
                    " (case when trans_flag = 48 then -1*price_exclude_vat else price_exclude_vat end) as " + _g.d.ic_trans_detail._price_exclude_vat +

                    " from " + _g.d.ic_trans_detail._table +
                    " where trans_flag in (44, 48) and doc_date between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) +
                    " and " + _g.d.ic_trans_detail._last_status + "=0 " +

                    " and " + string.Format(__branchCodeDetailWhere, _g.d.ic_trans_detail._table) + " =\'" + branch_code + "\'"

                );
                __queryTransDetailStr.Append(") as temp1 ");
                __queryTransDetailStr.Append(" group by " + _g.d.ic_trans_detail._item_code + "," +
                    _g.d.ic_trans_detail._item_name + "," +
                    _g.d.ic_trans_detail._barcode + "," +
                    _g.d.ic_trans_detail._unit_code + "," +
                    _g.d.ic_trans_detail._price + "," +
                    _g.d.ic_trans_detail._stand_value + "," +
                    _g.d.ic_trans_detail._divide_value);

            }*/

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
                ) + " from " + _g.d.cb_trans._table + " where " + __docDateTransFlagWhere + " and " + _g.d.cb_trans._trans_type + "=" + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() +
                " and exists (select doc_no from ic_trans where ic_trans.doc_no = cb_trans.doc_no and ic_trans.trans_flag = cb_trans.trans_flag and ic_trans.is_pos = 1 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) " +
                " and " + string.Format(__branchCodeDetailWhere, _g.d.cb_trans._table) + "= \'" + branch_code + "\'");

            __cbTransQuery.Append(") as temp1 where total_amount > 0 ");

            StringBuilder __queryCBTrans2 = new StringBuilder();
            // หลังร้าน
            StringBuilder __queryCBTransBackend = new StringBuilder("select ");
            __queryCBTransBackend.Append(MyLib._myGlobal._fieldAndComma(
                _g.d.cb_trans._doc_no,
                _g.d.cb_trans._doc_date,
                _g.d.cb_trans._doc_time,

                _g.d.cb_trans._trans_flag,
                _g.d.cb_trans._trans_type,

                _g.d.cb_trans._total_amount,
                _g.d.cb_trans._total_net_amount,

                _g.d.cb_trans._cash_amount,
                _g.d.cb_trans._chq_amount,
                _g.d.cb_trans._tranfer_amount,
                _g.d.cb_trans._card_amount,
                _g.d.cb_trans._total_income_amount,
                _g.d.cb_trans._point_amount,
                _g.d.cb_trans._coupon_amount,
                _g.d.cb_trans._total_amount_pay

                ) + " from " + _g.d.cb_trans._table + " where " + __docDateTransFlagWhere + " and " + _g.d.cb_trans._trans_type + "=" + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() +

                " and exists (select doc_no from ic_trans where ic_trans.doc_no = cb_trans.doc_no and ic_trans.trans_flag = cb_trans.trans_flag and ic_trans.is_pos = 0 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) " +
                " and " + string.Format(__branchCodeDetailWhere, _g.d.cb_trans._table) + "= \'" + branch_code + "\'");

            __queryCBTrans2.Append(" select " + MyLib._myGlobal._fieldAndComma(

                "\'" + __docNo + "\' as " + _g.d.cb_trans._doc_no,
                "\'" + docDate + "\' as " + _g.d.cb_trans._doc_date,
                "\'23:59\' as " + _g.d.cb_trans._doc_time,

                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.cb_trans._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.cb_trans._trans_type,

                _g.d.cb_trans._total_amount,
                _g.d.cb_trans._total_net_amount,
                _g.d.cb_trans._cash_amount,
                _g.d.cb_trans._chq_amount,
                _g.d.cb_trans._tranfer_amount,
                _g.d.cb_trans._card_amount,
                _g.d.cb_trans._total_income_amount,
                _g.d.cb_trans._point_amount,
                _g.d.cb_trans._coupon_amount,
                _g.d.cb_trans._total_amount_pay

                ) + " from (" + __cbTransQuery + ") as temp2 where total_amount > 0 ");
            //__queryCBTrans2.Append(__cbTransQuery);

            //__queryCBTrans2.Append(" union all ");
            //__queryCBTrans2.Append(__queryCBTransBackend);

            StringBuilder __queryCBTransCN = new StringBuilder("select ");
            __queryCBTransCN.Append(MyLib._myGlobal._fieldAndComma(
                _g.d.cb_trans._doc_no,
                _g.d.cb_trans._doc_date,
                _g.d.cb_trans._doc_time,

                _g.d.cb_trans._trans_flag,
                _g.d.cb_trans._trans_type,

                _g.d.cb_trans._total_amount,
                _g.d.cb_trans._total_net_amount,

                _g.d.cb_trans._cash_amount,
                _g.d.cb_trans._chq_amount,
                _g.d.cb_trans._tranfer_amount,
                _g.d.cb_trans._card_amount,
                _g.d.cb_trans._total_income_amount,
                _g.d.cb_trans._point_amount,
                _g.d.cb_trans._coupon_amount,
                _g.d.cb_trans._total_amount_pay

                ) + " from " + _g.d.cb_trans._table + " where  doc_date = \'" + docDate + "\' and trans_flag = 48 and " + _g.d.cb_trans._trans_type + "=" + _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString());

            if (_cnImport == true)
            {
                __queryCBTrans2.Append(" union all ");
                __queryCBTrans2.Append(__queryCBTransCN);
            }

            /*if (__cncut)
            {
                __queryCBTrans2 = new StringBuilder();
                __queryCBTrans2.Append(" select " + MyLib._myGlobal._fieldAndComma(

                "\'" + __docNo + "\' as " + _g.d.cb_trans._doc_no,
                "\'" + docDate + "\' as " + _g.d.cb_trans._doc_date,
                "\'23:59\' as " + _g.d.cb_trans._doc_time,

                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.cb_trans._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.cb_trans._trans_type) + "," + __cbTransField +
                " from (select ");

                __queryCBTrans2.Append(MyLib._myGlobal._fieldAndComma(
                    _g.d.cb_trans._doc_no,
                    _g.d.cb_trans._doc_date,
                    _g.d.cb_trans._doc_time,

                    _g.d.cb_trans._trans_flag,
                    _g.d.cb_trans._trans_type,

                    "(case when trans_flag = 48 then -1*total_amount else total_amount end ) as " + _g.d.cb_trans._total_amount,
                    "(case when trans_flag = 48 then -1*total_net_amount else total_net_amount end ) as " + _g.d.cb_trans._total_net_amount,

                    "(case when trans_flag = 48 then -1*cash_amount else cash_amount end ) as " + _g.d.cb_trans._cash_amount,
                    "(case when trans_flag = 48 then -1*chq_amount else chq_amount end ) as " + _g.d.cb_trans._chq_amount,
                    "(case when trans_flag = 48 then -1*tranfer_amount else tranfer_amount end ) as " + _g.d.cb_trans._tranfer_amount,
                    "(case when trans_flag = 48 then -1*card_amount else card_amount end ) as " + _g.d.cb_trans._card_amount,
                    "(case when trans_flag = 48 then -1*total_income_amount else total_income_amount end ) as " + _g.d.cb_trans._total_income_amount,
                    "(case when trans_flag = 48 then -1*point_amount else point_amount end ) as " + _g.d.cb_trans._point_amount,
                    "(case when trans_flag = 48 then -1*coupon_amount else coupon_amount end ) as " + _g.d.cb_trans._coupon_amount,
                    "(case when trans_flag = 48 then -1*total_amount_pay else total_amount_pay end ) as " + _g.d.cb_trans._total_amount_pay

                    ) + " from " + _g.d.cb_trans._table + " where  doc_date between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) + " and trans_flag in (44, 48) and " + string.Format(__branchCodeDetailWhere, _g.d.cb_trans._table) + "= \'" + branch_code + "\' ");

                __queryCBTrans2.Append(") as temp2 ");

            }*/

            //__cbTransQuery.Append(" group by " + _g.d.cb_trans._doc_no);

            #endregion

            #region Credit Card

            // cb_trans_detail pos
            StringBuilder __getCBTransDetailPOSQuery = new StringBuilder();
            __getCBTransDetailPOSQuery.Append("select ");
            __getCBTransDetailPOSQuery.Append(MyLib._myGlobal._fieldAndComma(
                "\'" + __docNo + "\' as " + _g.d.cb_trans_detail._doc_no,
                "\'" + docDate + "\' as " + _g.d.cb_trans_detail._doc_date,
                "\'23:59\' as " + _g.d.cb_trans_detail._doc_time,
                _g.d.cb_trans_detail._doc_no + " as " + _g.d.cb_trans_detail._doc_ref,

                _g.d.cb_trans_detail._trans_flag,
                _g.d.cb_trans_detail._trans_type,
                _g.d.cb_trans_detail._doc_type,
                _g.d.cb_trans_detail._ap_ar_code,

                _g.d.cb_trans_detail._trans_number,
                _g.d.cb_trans_detail._trans_number_type,
                _g.d.cb_trans_detail._doc_date_ref,

                _g.d.cb_trans_detail._pass_book_code,
                _g.d.cb_trans_detail._bank_code,
                _g.d.cb_trans_detail._bank_branch,
                _g.d.cb_trans_detail._exchange_rate,
                _g.d.cb_trans_detail._amount,
                _g.d.cb_trans_detail._fee_amount,
                _g.d.cb_trans_detail._other_amount,
                _g.d.cb_trans_detail._tax_at_pay,
                _g.d.cb_trans_detail._sum_amount,

                _g.d.cb_trans_detail._remark,

                // petty cash
                // credit
                _g.d.cb_trans_detail._no_approved,
                _g.d.cb_trans_detail._approve_code,
                _g.d.cb_trans_detail._approve_date,
                _g.d.cb_trans_detail._charge,
                _g.d.cb_trans_detail._credit_card_type,

                // chq
                _g.d.cb_trans_detail._chq_ref,
                _g.d.cb_trans_detail._chq_due_date,
                _g.d.cb_trans_detail._chq_date,
                _g.d.cb_trans_detail._external_chq,
                _g.d.cb_trans_detail._chq_on_hand,
                _g.d.cb_trans_detail._balance_amount
                ));

            __getCBTransDetailPOSQuery.Append(" from " + _g.d.cb_trans_detail._table);
            __getCBTransDetailPOSQuery.Append(" where " + __docDateTransFlagWhere + " and exists (select doc_no from ic_trans where ic_trans.doc_no = cb_trans_detail.doc_no and ic_trans.trans_flag = cb_trans_detail.trans_flag and ic_trans.is_pos = 1 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) and " + string.Format(__branchCodeDetailWhere, _g.d.cb_trans_detail._table) + "= \'" + branch_code + "\'");

            // cb_trans_detail backend

            StringBuilder __getCBTransDetailBackendQuery = new StringBuilder(" select ");
            __getCBTransDetailBackendQuery.Append(MyLib._myGlobal._fieldAndComma(
                _g.d.cb_trans_detail._doc_no,
                _g.d.cb_trans_detail._doc_date,
                _g.d.cb_trans_detail._doc_time,
                _g.d.cb_trans_detail._doc_ref,

                _g.d.cb_trans_detail._trans_flag,
                _g.d.cb_trans_detail._trans_type,
                _g.d.cb_trans_detail._doc_type,
                _g.d.cb_trans_detail._ap_ar_code,

                _g.d.cb_trans_detail._trans_number,
                _g.d.cb_trans_detail._trans_number_type,
                _g.d.cb_trans_detail._doc_date_ref,

                _g.d.cb_trans_detail._pass_book_code,
                _g.d.cb_trans_detail._bank_code,
                _g.d.cb_trans_detail._bank_branch,
                _g.d.cb_trans_detail._exchange_rate,
                _g.d.cb_trans_detail._amount,
                _g.d.cb_trans_detail._fee_amount,
                _g.d.cb_trans_detail._other_amount,
                _g.d.cb_trans_detail._tax_at_pay,
                _g.d.cb_trans_detail._sum_amount,

                _g.d.cb_trans_detail._remark,

                // petty cash
                // credit
                _g.d.cb_trans_detail._no_approved,
                _g.d.cb_trans_detail._approve_code,
                _g.d.cb_trans_detail._approve_date,
                _g.d.cb_trans_detail._charge,
                _g.d.cb_trans_detail._credit_card_type,

                // chq
                _g.d.cb_trans_detail._chq_ref,
                _g.d.cb_trans_detail._chq_due_date,
                _g.d.cb_trans_detail._chq_date,
                _g.d.cb_trans_detail._external_chq,
                _g.d.cb_trans_detail._chq_on_hand,
                _g.d.cb_trans_detail._balance_amount
                ));

            __getCBTransDetailBackendQuery.Append(" from " + _g.d.cb_trans_detail._table);
            __getCBTransDetailBackendQuery.Append(" where " + __docDateTransFlagWhere + " and exists (select doc_no from ic_trans where ic_trans.doc_no = cb_trans_detail.doc_no and ic_trans.trans_flag = cb_trans_detail.trans_flag and ic_trans.is_pos = 0 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) " + " and " + string.Format(__branchCodeDetailWhere, _g.d.cb_trans_detail._table) + "= \'" + branch_code + "\'");

            StringBuilder __getCBTransDetailCN = new StringBuilder(" select ");
            __getCBTransDetailCN.Append(MyLib._myGlobal._fieldAndComma(
                _g.d.cb_trans_detail._doc_no,
                _g.d.cb_trans_detail._doc_date,
                _g.d.cb_trans_detail._doc_time,
                _g.d.cb_trans_detail._doc_ref,

                _g.d.cb_trans_detail._trans_flag,
                _g.d.cb_trans_detail._trans_type,
                _g.d.cb_trans_detail._doc_type,
                _g.d.cb_trans_detail._ap_ar_code,

                _g.d.cb_trans_detail._trans_number,
                _g.d.cb_trans_detail._trans_number_type,
                _g.d.cb_trans_detail._doc_date_ref,

                _g.d.cb_trans_detail._pass_book_code,
                _g.d.cb_trans_detail._bank_code,
                _g.d.cb_trans_detail._bank_branch,
                _g.d.cb_trans_detail._exchange_rate,
                _g.d.cb_trans_detail._amount,
                _g.d.cb_trans_detail._fee_amount,
                _g.d.cb_trans_detail._other_amount,
                _g.d.cb_trans_detail._tax_at_pay,
                _g.d.cb_trans_detail._sum_amount,

                _g.d.cb_trans_detail._remark,

                // petty cash
                // credit
                _g.d.cb_trans_detail._no_approved,
                _g.d.cb_trans_detail._approve_code,
                _g.d.cb_trans_detail._approve_date,
                _g.d.cb_trans_detail._charge,
                _g.d.cb_trans_detail._credit_card_type,

                // chq
                _g.d.cb_trans_detail._chq_ref,
                _g.d.cb_trans_detail._chq_due_date,
                _g.d.cb_trans_detail._chq_date,
                _g.d.cb_trans_detail._external_chq,
                _g.d.cb_trans_detail._chq_on_hand,
                _g.d.cb_trans_detail._balance_amount
                ));

            __getCBTransDetailCN.Append(" from " + _g.d.cb_trans_detail._table);
            __getCBTransDetailCN.Append(" where doc_date = \'" + docDate + "\' and trans_flag = 48"); //  and " + string.Format(__branchCodeDetailWhere, _g.d.cb_trans_detail._table) + "= \'" + branch_code + "\' 


            StringBuilder __CBTransDetailQuery = new StringBuilder();
            __CBTransDetailQuery.Append(__getCBTransDetailPOSQuery);
            //__CBTransDetailQuery.Append(" union all ");
            //__CBTransDetailQuery.Append(__getCBTransDetailBackendQuery);
            if (_cnImport == true)
            {
                __CBTransDetailQuery.Append(" union all ");
                __CBTransDetailQuery.Append(__getCBTransDetailCN);
            }
            #endregion

            #region GL Journal Vat Sale ไม่เอา
            StringBuilder __getVatSalePOS = new StringBuilder();

            /*__getVatSalePOS.Append("select ");
            __getVatSalePOS.Append(MyLib._myGlobal._fieldAndComma(
                 "\'" + __docNo + "\' as " + _g.d.gl_journal_vat_sale._doc_no,
                "\'" + docDate + "\' as " + _g.d.gl_journal_vat_sale._doc_date,

                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_type,

                _g.d.gl_journal_vat_sale._vat_number,
                _g.d.gl_journal_vat_sale._vat_date,

                _g.d.gl_journal_vat_sale._base_caltax_amount,
                _g.d.gl_journal_vat_sale._tax_rate,
                _g.d.gl_journal_vat_sale._amount,
                _g.d.gl_journal_vat_sale._except_tax_amount,

                _g.d.gl_journal_vat_sale._is_add,

                _g.d.gl_journal_vat_sale._ar_code,
                " case when coalesce(ar_name, '')='' then (select name_1 from ar_customer where code = gl_journal_vat_sale.ar_code) else ar_name end as  " + _g.d.gl_journal_vat_sale._ar_name,
                " case when coalesce(tax_no, '') ='' then ( select case when coalesce(ar_customer_detail.tax_id, '') ='' then ar_customer_detail.card_id else  ar_customer_detail.tax_id end from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code)  else tax_no end  as  " + _g.d.gl_journal_vat_sale._tax_no,
                " case when branch_type is null then (coalesce((select  ar_customer_detail.branch_type from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code), 0))  else  branch_type end as  " + _g.d.gl_journal_vat_sale._branch_type,
                " case when coalesce(branch_code, '') ='' then ( select  ar_customer_detail.branch_code from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code)  else branch_code end  as " + _g.d.gl_journal_vat_sale._branch_code,
                _g.d.gl_journal_vat_sale._manual_add,

                _g.d.gl_journal_vat_sale._vat_effective_period,
                _g.d.gl_journal_vat_sale._vat_effective_year,
                _g.d.gl_journal_vat_sale._description,
                _g.d.gl_journal_vat_sale._tax_group,
                _g.d.gl_journal_vat_sale._vat_calc,
                _g.d.gl_journal_vat_sale._period_number

                ));

            __getVatSalePOS.Append(" from " + _g.d.gl_journal_vat_sale._table);
            __getVatSalePOS.Append(" where " + __docDateTransFlagWhere + " and exists (select doc_no from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and ic_trans.trans_flag = gl_journal_vat_sale.trans_flag and ic_trans.is_pos = 1 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) " + " and " + string.Format(__branchCodeDetailWhere, _g.d.gl_journal_vat_sale._table) + "= \'" + branch_code + "\'");
            */

            __getVatSalePOS.Append("select ");

            __getVatSalePOS.Append(MyLib._myGlobal._fieldAndComma(

                // group
                "\'" + __docNo + "\' as " + _g.d.gl_journal_vat_sale._doc_no,
                "\'" + docDate + "\' as " + _g.d.gl_journal_vat_sale._doc_date,
                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_type,

                "\'" + __docNo + "\' as " + _g.d.gl_journal_vat_sale._vat_number,
                "\'" + docDate + "\' as " + _g.d.gl_journal_vat_sale._vat_date,

                " sum(" + _g.d.ic_trans._total_before_vat + ") as " + _g.d.gl_journal_vat_sale._base_caltax_amount,
                " (select vat_rate from erp_option limit 1 ) as " + _g.d.gl_journal_vat_sale._tax_rate,
                " sum(" + _g.d.ic_trans._total_vat_value + ") as " + _g.d.gl_journal_vat_sale._amount,
                " sum(" + _g.d.ic_trans._total_except_vat + ") as " + _g.d.gl_journal_vat_sale._except_tax_amount,


                "0 as " + _g.d.gl_journal_vat_sale._is_add,


                " \'00009\' as  " + _g.d.gl_journal_vat_sale._ar_code,

                " (select name_1 from ar_customer where code = \'00009\') as  " + _g.d.gl_journal_vat_sale._ar_name,
                " (select tax_id from ar_customer_detail where ar_customer_detail.ar_code =\'00009\') as  " + _g.d.gl_journal_vat_sale._tax_no,
                " (select branch_type from ar_customer_detail where ar_customer_detail.ar_code = \'00009\') as  " + _g.d.gl_journal_vat_sale._branch_type,
                " (select branch_code from ar_customer_detail where ar_customer_detail.ar_code = \'00009\') as " + _g.d.gl_journal_vat_sale._branch_code,
                "0 as " + _g.d.gl_journal_vat_sale._manual_add,

                "extract(month from date(\'" + docDate + "\')) as " + _g.d.gl_journal_vat_sale._vat_effective_period,
                "extract(year from date(\'" + docDate + "\'))+543 as " + _g.d.gl_journal_vat_sale._vat_effective_year,
                "\'\' as " + _g.d.gl_journal_vat_sale._description,
                "\'\' as " + _g.d.gl_journal_vat_sale._tax_group,
                "1 as " + _g.d.gl_journal_vat_sale._vat_calc,
                "0 as " + _g.d.gl_journal_vat_sale._period_number
                ));

            __getVatSalePOS.Append(" from ( " + __queryTrans);
            __getVatSalePOS.Append(" ) as temp1 ");

            // backend

            StringBuilder __getVatSaleBackend = new StringBuilder("select ");
            __getVatSaleBackend.Append(MyLib._myGlobal._fieldAndComma(
                _g.d.gl_journal_vat_sale._doc_no,
                _g.d.gl_journal_vat_sale._doc_date,

                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_type,

                _g.d.gl_journal_vat_sale._vat_number,
                _g.d.gl_journal_vat_sale._vat_date,

                _g.d.gl_journal_vat_sale._base_caltax_amount,
                _g.d.gl_journal_vat_sale._tax_rate,
                _g.d.gl_journal_vat_sale._amount,
                _g.d.gl_journal_vat_sale._except_tax_amount,

                _g.d.gl_journal_vat_sale._is_add,

                _g.d.gl_journal_vat_sale._ar_code,
                " case when coalesce(ar_name, '')='' then (select name_1 from ar_customer where code = gl_journal_vat_sale.ar_code) else ar_name end as  " + _g.d.gl_journal_vat_sale._ar_name,
                " case when coalesce(tax_no, '') ='' then ( select case when coalesce(ar_customer_detail.tax_id, '') ='' then ar_customer_detail.card_id else  ar_customer_detail.tax_id end from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code)  else tax_no end  as  " + _g.d.gl_journal_vat_sale._tax_no,
                " case when branch_type is null then (coalesce((select  ar_customer_detail.branch_type from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code), 0))  else  branch_type end as  " + _g.d.gl_journal_vat_sale._branch_type,
                " case when coalesce(branch_code, '') ='' then ( select  ar_customer_detail.branch_code from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code)  else branch_code end  as " + _g.d.gl_journal_vat_sale._branch_code,
                _g.d.gl_journal_vat_sale._manual_add,

                _g.d.gl_journal_vat_sale._vat_effective_period,
                _g.d.gl_journal_vat_sale._vat_effective_year,
                _g.d.gl_journal_vat_sale._description,
                _g.d.gl_journal_vat_sale._tax_group,
                _g.d.gl_journal_vat_sale._vat_calc,
                _g.d.gl_journal_vat_sale._period_number

                ));

            __getVatSaleBackend.Append(" from " + _g.d.gl_journal_vat_sale._table);
            __getVatSaleBackend.Append(" where " + __docDateTransFlagWhere + " and exists (select doc_no from ic_trans where ic_trans.doc_no = gl_journal_vat_sale.doc_no and ic_trans.trans_flag = gl_journal_vat_sale.trans_flag and ic_trans.is_pos = 0 and ic_trans.last_status = 0 and ic_trans.trans_flag = 44 ) " + " and " + string.Format(__branchCodeDetailWhere, _g.d.gl_journal_vat_sale._table) + "= \'" + branch_code + "\'");

            StringBuilder __getVatSaleCN = new StringBuilder("select ");
            __getVatSaleCN.Append(MyLib._myGlobal._fieldAndComma(
                _g.d.gl_journal_vat_sale._doc_no,
                _g.d.gl_journal_vat_sale._doc_date,

                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " as " + _g.d.gl_journal_vat_sale._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้) + " as " + _g.d.gl_journal_vat_sale._trans_type,

                _g.d.gl_journal_vat_sale._vat_number,
                _g.d.gl_journal_vat_sale._vat_date,

                _g.d.gl_journal_vat_sale._base_caltax_amount,
                _g.d.gl_journal_vat_sale._tax_rate,
                _g.d.gl_journal_vat_sale._amount,
                _g.d.gl_journal_vat_sale._except_tax_amount,

                _g.d.gl_journal_vat_sale._is_add,

                _g.d.gl_journal_vat_sale._ar_code,
                " case when coalesce(ar_name, '')='' then (select name_1 from ar_customer where code = gl_journal_vat_sale.ar_code) else ar_name end as  " + _g.d.gl_journal_vat_sale._ar_name,
                " case when coalesce(tax_no, '') ='' then ( select case when coalesce(ar_customer_detail.tax_id, '') ='' then ar_customer_detail.card_id else  ar_customer_detail.tax_id end from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code)  else tax_no end  as  " + _g.d.gl_journal_vat_sale._tax_no,
                " case when branch_type is null then (coalesce((select  ar_customer_detail.branch_type from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code), 0))  else  branch_type end as  " + _g.d.gl_journal_vat_sale._branch_type,
                " case when coalesce(branch_code, '') ='' then ( select  ar_customer_detail.branch_code from ar_customer_detail where ar_customer_detail.ar_code = gl_journal_vat_sale.ar_code)  else branch_code end  as " + _g.d.gl_journal_vat_sale._branch_code,
                _g.d.gl_journal_vat_sale._manual_add,

                _g.d.gl_journal_vat_sale._vat_effective_period,
                _g.d.gl_journal_vat_sale._vat_effective_year,
                _g.d.gl_journal_vat_sale._description,
                _g.d.gl_journal_vat_sale._tax_group,
                _g.d.gl_journal_vat_sale._vat_calc,
                _g.d.gl_journal_vat_sale._period_number

                ));

            __getVatSaleCN.Append(" from " + _g.d.gl_journal_vat_sale._table);
            __getVatSaleCN.Append(" where doc_date = \'" + docDate + "\' and trans_flag = 48 ");


            StringBuilder __queryVatSale = new StringBuilder();
            /*if (__sumPeriod)
            {
                __queryVatSale.Append("select " + MyLib._myGlobal._fieldAndComma(
                    "\'" + __docNo + "\' as " + _g.d.gl_journal_vat_sale._doc_no,
                "\'" + docDate + "\' as " + _g.d.gl_journal_vat_sale._doc_date,

                _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_flag,
                _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_type,

                "\'" + __docNo + "\' as " + _g.d.gl_journal_vat_sale._vat_number,
                " \'" + docDate + "\' as " + _g.d.gl_journal_vat_sale._vat_date,
                "sum(base_caltax_amount) as base_caltax_amount",
                "tax_rate",
                "sum(amount) as amount",
                "sum(except_tax_amount) as except_tax_amount ",
                " 0 as is_add",
                " \'" + branch_code.ToUpper() + "\' as ar_code",
                " \'\' as ar_name",
                " \'\' as tax_no",
                " 0 as branch_type",
                " \'\' as branch_code",
                " 0 as manual_add",
                "vat_effective_period",
                "vat_effective_year",
                " \'\' as description",
                " \'\' as tax_group",
                " 1 as vat_calc",
                " 0 as period_number")
                + " from (" + __getVatSalePOS.ToString() + ") as temp2  group by tax_rate,  vat_effective_period, vat_effective_year ");
            }
            else*/
            {
                __queryVatSale.Append(__getVatSalePOS);
            }


            //__queryVatSale.Append(" union all ");
            //__queryVatSale.Append(__getVatSaleBackend);
            if (_cnImport == true)
            {
                __queryVatSale.Append(" union all ");
                __queryVatSale.Append(__getVatSaleCN);
            }

            /*if (__cncut)
            {
                __queryVatSale = new StringBuilder();
                __queryVatSale.Append("select " + MyLib._myGlobal._fieldAndComma(
                    "\'" + __docNo + "\' as " + _g.d.gl_journal_vat_sale._doc_no,
                    "\'" + docDate + "\' as " + _g.d.gl_journal_vat_sale._doc_date,

                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_flag,
                    _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " as " + _g.d.gl_journal_vat_sale._trans_type,

                    "\'" + __docNo + "\' as " + _g.d.gl_journal_vat_sale._vat_number,
                    " \'" + docDate + "\' as " + _g.d.gl_journal_vat_sale._vat_date,
                    "sum(total_before_vat) as base_caltax_amount ",
                    "(select vat_rate from erp_option limit 1 ) as tax_rate ",
                    "sum(total_vat_value) as amount ",
                    "sum(total_except_vat) as except_tax_amount",
                    " 0 as is_add",
                    " \'" + branch_code.ToUpper() + "\' as ar_code",
                    " \'\' as ar_name",
                    " \'\' as tax_no",
                    " 0 as branch_type",
                    " \'\' as branch_code",
                    " 0 as manual_add",
                     this._sale_pos_screen_top._getDataDate(_g.d.ic_trans_resource._from_doc_date).Month.ToString() + " as vat_effective_period",
                    (this._sale_pos_screen_top._getDataDate(_g.d.ic_trans_resource._from_doc_date).Year + MyLib._myGlobal._year_add).ToString() + " as vat_effective_year",
                    " \'\' as description",
                    " \'\' as tax_group",
                    " 1 as vat_calc",
                    " 0 as period_number"
                    ));
                __queryVatSale.Append(" from (select " +

                    MyLib._myGlobal._fieldAndComma(

                    _g.d.ic_trans._doc_date,
                    " (case when trans_flag = 48 then -1*total_value else total_value end) as " + _g.d.ic_trans._total_value,
                    " (case when trans_flag = 48 then -1*total_discount else total_discount end) as " + _g.d.ic_trans._total_discount,

                    " (case when trans_flag = 48 then -1*total_before_vat else total_before_vat end) as " + _g.d.ic_trans._total_before_vat,
                    " (case when trans_flag = 48 then -1*total_after_vat else total_after_vat end) as " + _g.d.ic_trans._total_after_vat,
                    " (case when trans_flag = 48 then -1*total_except_vat else total_except_vat end) as " + _g.d.ic_trans._total_except_vat,
                    " (case when trans_flag = 48 then -1*total_vat_value else total_vat_value end) as " + _g.d.ic_trans._total_vat_value,
                    " (case when trans_flag = 48 then -1*total_amount else total_amount end) as " + _g.d.ic_trans._total_amount
                    ) +
                    " from  " + _g.d.ic_trans._table + " where " +
                _g.d.ic_trans._doc_date + " between " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._from_doc_date) + " and " + this._sale_pos_screen_top._getDataStrQuery(_g.d.ic_trans_resource._to_doc_date) +
                "  and ((" + _g.d.ic_trans._trans_flag + " = 44 and is_pos = 1 ) or (" + _g.d.ic_trans._trans_flag + " = 48 )) " +

                " and last_status = 0 and branch_code = \'" + branch_code + "\' " + ") as temp1 ");

                //__queryVatSale.Append(" ) as temp3 ");


            }*/

            #endregion

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTransStr));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTransDetailStr.ToString()));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryCBTrans2.ToString()));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__CBTransDetailQuery.ToString()));

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryVatSale.ToString()));

            // data for cust 00009
            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select code, name_1, (select tax_id from ar_customer_detail where ar_customer_detail.ar_code = code) as tax_id , (select branch_type from ar_customer_detail where ar_customer_detail.ar_code = code) as branch_type, (select branch_code from ar_customer_detail where ar_customer_detail.ar_code = code) as branch_code from ar_customer where code = \'00009\' "));

            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __debug_query = __query.ToString();

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Count > 0)
            {
                this._gridTransTable._loadFromDataTable(((DataSet)__result[0]).Tables[0]);

                this._itemGrid._loadFromDataTable(((DataSet)__result[1]).Tables[0]);
                //if (__sumPeriod == false)
                //{
                this._gridCBTrans._loadFromDataTable(((DataSet)__result[2]).Tables[0]);
                this._gridCBTransDetail._loadFromDataTable(((DataSet)__result[3]).Tables[0]);
                //}
                this._vatSale._loadFromDataTable(((DataSet)__result[4]).Tables[0]);

                // gen vat sale จากตาราง
                //this._vatSale._clear();
                //int __addr = this._vatSale._addRow();

                // update data

                this._itemGrid._calcTotal(true);
            }
        }


        private void _saveButton_Click(object sender, EventArgs e)
        {
            _process();
        }

        string _lastCnImportDate = "";
        void _process()
        {
            if (MessageBox.Show("ยืนยันการโอนข้อมูลการขาย", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _cnImport = true;
                _lastCnImportDate = "";

                for (int row = 0; row < this._transGrid._rowData.Count; row++)
                {
                    string __checked = this._transGrid._cellGet(row, 0).ToString();
                    if (__checked == "1")
                    {
                        string __docDate = MyLib._myGlobal._convertDateToQuery((DateTime)this._transGrid._cellGet(row, _g.d.ic_trans._doc_date));
                        string __branchSync = this._transGrid._cellGet(row, _g.d.ic_trans._pos_id).ToString();
                        string __docNo = this._transGrid._cellGet(row, _g.d.ic_trans._doc_no).ToString();
                        string __docTime = _defaultDocTime;

                        this._gridTransTable._clear();

                        this._itemGrid._clear();
                        this._gridCBTrans._clear();
                        this._gridCBTransDetail._clear();

                        this._vatSale._clear();

                        // load data เข้า grid ก่อน
                        if (_lastCnImportDate.Equals(__docDate) == false)
                            _cnImport = true;

                        _loadDataProcess(__docDate, __branchSync, row);

                        // toe discount word
                        for (int __row2 = 0; __row2 < this._gridTransTable._rowData.Count; __row2++)
                        {
                            string __totalDiscount = MyLib._myGlobal._decimalPhase(this._gridTransTable._cellGet(__row2, _g.d.ic_trans._total_discount).ToString()).ToString("0.000");
                            this._gridTransTable._cellUpdate(__row2, _g.d.ic_trans._discount_word, __totalDiscount, true);

                            // toe ปรับ vat
                        }

                        if (this._sale_pos_screen_top._getDataStr(_g.d.ic_trans._credit_sale).Equals("1"))
                        {
                            for (int __row2 = 0; __row2 < this._gridTransTable._rowData.Count; __row2++)
                            {
                                this._gridTransTable._cellUpdate(__row2, _g.d.ic_trans._inquiry_type, 0, true);
                            }

                            for (int __row2 = 0; __row2 < this._itemGrid._rowData.Count; __row2++)
                            {
                                this._itemGrid._cellUpdate(__row2, _g.d.ic_trans_detail._inquiry_type, 0, true);
                            }
                        }

                        //return;

                        if (this._itemGrid._rowData.Count > 0)
                        {
                            // pack query for save
                            this._itemGrid._updateRowIsChangeAll(true);

                            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                            string __masterField = _g.d.ic_trans._last_status + ",is_lock_record,pos_transfer,";

                            string __masterValue = "0,1,1,";

                            // ic_trans

                            // toe ลบ ic_trans ก่อน
                            for (int __row = 0; __row < this._gridTransTable._rowData.Count; __row++)
                            {
                                string __transDocNo = this._gridTransTable._cellGet(__row, _g.d.ic_trans._doc_no).ToString();
                                string __transTransFlag = this._gridTransTable._cellGet(__row, _g.d.ic_trans._trans_flag).ToString();
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __transDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transTransFlag));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __transDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transTransFlag));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __transDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transTransFlag));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.cb_trans_detail._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __transDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transTransFlag));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_vat_sale._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __transDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transTransFlag));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + __transDocNo + "\' and " + _g.d.gl_journal._trans_flag + "=" + __transTransFlag));
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + __transDocNo + "\' and " + _g.d.gl_journal_detail._trans_flag + "=" + __transTransFlag));
                            }

                            this._gridTransTable._updateRowIsChangeAll(true);
                            __query.Append(this._gridTransTable._createQueryForInsert(_g.d.ic_trans._table, __masterField + _g.d.ic_trans._cashier_code + ",", __masterValue + "\'" + __branchSync.ToUpper() + "\',", false, false));

                            // ic_trans_detail
                            this._itemGrid._updateRowIsChangeAll(true);
                            __query.Append(this._itemGrid._createQueryForInsert(_g.d.ic_trans_detail._table, "", "", false, true));

                            /*
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
                                "\'" + __docDate + "\'",
                                "\'" + __docTime + "\'",
                                "\'" + __custCode + "\'",
                                __transFlag.ToString(),
                                __transType.ToString(),
                                __last_status.ToString(),
                                __is_pos.ToString(),
                                __inquiry_type.ToString(),
                                __vat_type.ToString(),
                                //"\'" + __wh_code + "\'",
                                //"\'" + __shelf_code + "\'",
                                __calcFlag.ToString(),
                                "\'" + __docDate + "\'",
                                "\'" + __docTime + "\'") + ",";
                            __query.Append(this._itemGrid._createQueryForInsert(_g.d.ic_trans_detail._table, __detailFieldList, __detailDataList, false, true));
                            */

                            // cb_trans
                            this._gridCBTrans._updateRowIsChangeAll(true);
                            __query.Append(this._gridCBTrans._createQueryForInsert(_g.d.cb_trans._table, "", "", false, false));

                            /*
                            // จ่ายเงิน
                            int __paytype = 1;
                            int __ap_ar_type = 1;

                            string __cbTransField = MyLib._myGlobal._fieldAndComma(_g.d.cb_trans._doc_no, _g.d.cb_trans._doc_date, _g.d.cb_trans._doc_time, _g.d.cb_trans._trans_flag, _g.d.cb_trans._trans_type, _g.d.cb_trans._doc_format_code,
                                _g.d.cb_trans._pay_type
                                );
                            string __cbTransData = MyLib._myGlobal._fieldAndComma(
                                //"\'" + __docNo + "\'",
                                //"\'" + __docDate + "\'",
                                //"\'" + __docTime + "\'",
                                __transFlag.ToString(),
                                __transType.ToString(),
                                "\'SIP\'",
                                __paytype.ToString()
                                );
                            //ArrayList __dataPayControl = this._screen_pay._createQueryForDatabase();

                            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.cb_trans._table + "(" + __cbTransField + "," + __dataPayControl[0] + ") values(" + __cbTransData + "," + __dataPayControl[1] + ") "));
                            this._gridCBTrans._updateRowIsChangeAll(true);
                            __query.Append(this._gridCBTrans._createQueryForInsert(_g.d.cb_trans._table, __cbTransField + ",", __cbTransData + ",", false, false));


                            string __fieldList = _g.d.cb_trans_detail._doc_no + "," + _g.d.cb_trans_detail._doc_date + "," + _g.d.cb_trans_detail._doc_time + "," + _g.d.cb_trans_detail._trans_type + "," + _g.d.cb_trans_detail._trans_flag + ",";
                            string __dataList = "\'" + __docNo + "\',\'" + __docDate + "\',\'" + __docTime + "\'," + __transType + "," + __transFlag + ",";
                            */

                            // บัตรเครดิต

                            this._gridCBTransDetail._updateRowIsChangeAll(true);
                            __query.Append(this._gridCBTransDetail._createQueryForInsert(_g.d.cb_trans_detail._table, "", "", false, true));

                            /*
                            this._payCreditCardGridControl1._updateRowIsChangeAll(true);
                            __query.Append(this._payCreditCardGridControl1._createQueryForInsert(_g.d.cb_trans_detail._table,
                                __fieldList + _g.d.cb_trans_detail._doc_type + "," + _g.d.cb_trans_detail._trans_number_type + "," + _g.d.cb_trans_detail._ap_ar_type + "," + _g.d.cb_trans_detail._ap_ar_code + ",",
                                __dataList + "3," + __paytype.ToString() + "," + __ap_ar_type + ",\'" + __custCode + "\',"));

                            // คูปอง
                            this._payCouponGridControl1._updateRowIsChangeAll(true);
                            __query.Append(this._payCouponGridControl1._createQueryForInsert(_g.d.cb_trans_detail._table,
                                __fieldList + _g.d.cb_trans_detail._doc_type + "," + _g.d.cb_trans_detail._trans_number_type + "," + _g.d.cb_trans_detail._ap_ar_type + "," + _g.d.cb_trans_detail._ap_ar_code + ",",
                                __dataList + "9," + __paytype.ToString() + "," + __ap_ar_type.ToString() + ",\'" + __custCode + "\',"));
                            */
                            // ภาษีขาย
                            //string __vatFieldList = _g.d.gl_journal_vat_sale._book_code + "," + _g.d.gl_journal_vat_sale._vat_calc + "," + _g.d.gl_journal_vat_sale._trans_type + "," + _g.d.gl_journal_vat_sale._trans_flag + "," + _g.d.gl_journal_vat_sale._doc_date + "," + _g.d.gl_journal_vat_sale._doc_no + "," + _g.d.gl_journal_vat_sale._ar_code + ",";
                            // string __vatDataList = "\'\'," + _g.g._transTypeGlobal._vatSaleType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "," + _getTransType + "," + _getTransFlag + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_date) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._doc_no) + "," + this._icTransScreenTop._getDataStrQuery(_g.d.ic_trans._cust_code) + ",";
                            //__query.Append(this._vatSale._createQueryForInsert(_g.d.gl_journal_vat_sale._table, __vatFieldList, __vatDataList, false, true));

                            this._vatSale._updateRowIsChangeAll(true);
                            __query.Append(this._vatSale._createQueryForInsert(_g.d.gl_journal_vat_sale._table, "", "", false, true));


                            __query.Append("</node>");

                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork(_global._datacenter_server, "SMLConfig" + _global._datacenter_provider.ToUpper() + ".xml", _global._datacenter_database_type);

                            // string __debugQuery = __query.ToString();
                            string __result = __myFrameWork._queryList(_global._datacenter_database_name, __query.ToString());

                            if (__result.Length == 0)
                            {
                                //MyLib._myGlobal._displayWarning(1, "");
                                //this._clear();

                                // update status 
                                this._transGrid._cellUpdate(row, _g.d.ic_trans._status, "Success", true);
                            }
                            else
                            {
                                MessageBox.Show(__result, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }

                            this._transGrid._cellUpdate(row, 0, 0, true);

                            if (_cnImport == true)
                                _cnImport = false;
                        }
                    }
                }

                MessageBox.Show("Success");


                //_docFlowThread __proces = new _docFlowThread(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน, "", "");
                //Thread __thread = new Thread(new ThreadStart(__proces._processAll));
                //__thread.Start();

            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._transGrid._rowData.Count; __row++)
            {
                this._transGrid._cellUpdate(__row, 0, 1, true);
            }
            this._transGrid.Invalidate();
        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._transGrid._rowData.Count; __row++)
            {
                this._transGrid._cellUpdate(__row, 0, 0, true);
            }
            this._transGrid.Invalidate();
        }

    }
}
