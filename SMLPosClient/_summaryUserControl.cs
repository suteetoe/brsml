using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SMLPosClient
{
    public partial class _summaryUserControl : UserControl
    {
        private string _userCode;
        private string _cashierPassword;
        private SMLPOSControl._posScreenConfig _posScreenConfig;
        private string _posFillmoneyDocFormat;
        public string _save_doc_no = "";
        string _startRunningNumber = "";
        Boolean _show_deposit_money = false;
        DataTable _cancelInvoiceDataTable = null;
        DataTable _discountInvoiceDataTable = null;
        string _periodGUID = "";

        public _summaryUserControl(SMLPOSControl._posScreenConfig posScreenConfig, string posFillmoneyDocFormat, string userCode, string password, Boolean sendMoney, string startRunningNumber, Boolean displayBalance, Boolean depositMoney, string period_Guid)
        {
            InitializeComponent();
            //
            if (sendMoney == false)
            {
                this._flowLayout.Visible = false;
            }

            this._userCode = userCode;
            this._cashierPassword = password;
            this._posScreenConfig = posScreenConfig;
            this._posFillmoneyDocFormat = posFillmoneyDocFormat;
            this._startRunningNumber = startRunningNumber;
            this._show_deposit_money = depositMoney;
            this._periodGUID = period_Guid;
            //
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._screen._table_name = _g.d.POSCashierSettle._table;
            this._screen._maxColumn = 2;
            int __row = 0;
            this._screen._addTextBox(__row, 0, _g.d.POSCashierSettle._DocNo, 1);
            this._screen._addTextBox(__row++, 1, _g.d.POSCashierSettle._MACHINECODE, 1);
            this._screen._addDateBox(__row, 0, 1, 0, _g.d.POSCashierSettle._DocDate, 1, true);
            this._screen._addTextBox(__row++, 1, _g.d.POSCashierSettle._doc_time, 1);
            this._screen._addDateBox(__row, 0, 1, 0, _g.d.POSCashierSettle._begin_date, 1, true);
            this._screen._addTextBox(__row++, 1, _g.d.POSCashierSettle._begin_time, 1);

            this._screen._addTextBox(__row++, 0, _g.d.POSCashierSettle._begin_user_code, 1);
            __row++;
            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_in, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._total_out_credit, 1, 2, true, __formatNumber);

            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_amount, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._total_out, 1, 2, true, __formatNumber);

            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_cancel, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._total_out_coupon, 1, 2, true, __formatNumber);

            this._screen._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_balance, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_credit_charge, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._sale_credit_amount, 1, 2, true, __formatNumber);

            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_diff, 1, 2, true, __formatNumber);

            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLTomYumGoong)
            {
                if (this._show_deposit_money)
                    this._screen._addNumberBox(__row, 1, 1, 0, _g.d.POSCashierSettle._deposit_cash_amount, 1, 2, true, __formatNumber);
            }
            __row++;

            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLTomYumGoong)
            {

                this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_advance, 1, 2, true, __formatNumber);
                if (this._show_deposit_money)
                    this._screen._addNumberBox(__row, 1, 1, 0, _g.d.POSCashierSettle._deposit_credit_amount, 1, 2, true, __formatNumber);
                __row++;

                this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_after_advance, 1, 2, true, __formatNumber);
                if (this._show_deposit_money)
                    this._screen._addNumberBox(__row, 1, 1, 0, _g.d.POSCashierSettle._deposit_return_cash_amount, 1, 2, true, __formatNumber);
                __row++;
            }

            __row++;
            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_cash, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._CashAmount, 1, 2, true, __formatNumber);

            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_credit_card, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._CreditCardAmount, 1, 2, true, __formatNumber);

            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_coupon, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._CoupongAmount, 1, 2, true, __formatNumber);

            // ยอดแต้ัม
            this._screen._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_point, 1, 2, true, __formatNumber);

            this._screen._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_sum, 1, 2, true, __formatNumber);
            this._screen._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._sum_amount, 1, 2, true, __formatNumber);

            this._screen._addTextBox(__row, 0, _g.d.POSCashierSettle._CashierCode, 0);
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
            {
                this._screen._addNumberBox(__row, 1, 1, 0, _g.d.POSCashierSettle._order_amount, 1, 2, true, __formatNumber);
            }
            __row++;

            this._screen._addTextBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._cashier_password, 1, 0, 0, true, true);

            this._screen._addTextBox(__row, 0, 2, _g.d.POSCashierSettle._remark, 2, 1);
            //
            this._screen._enabedControl(_g.d.POSCashierSettle._DocNo, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._DocDate, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._doc_time, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._MACHINECODE, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._begin_date, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._begin_time, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._begin_user_code, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_in, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_out, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_out_credit, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_out_coupon, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_amount, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_cancel, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_balance, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_cash, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_credit_card, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_diff, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_coupon, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_sum, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._sum_amount, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_point, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_advance, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_after_advance, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._sale_credit_amount, false);
            this._screen._enabedControl(_g.d.POSCashierSettle._total_credit_charge, false);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
            {
                this._screen._enabedControl(_g.d.POSCashierSettle._order_amount, false);
            }

            if (displayBalance == false)
            {

                _hiddenNumber(_g.d.POSCashierSettle._total_in);
                _hiddenNumber(_g.d.POSCashierSettle._total_out);

                _hiddenNumber(_g.d.POSCashierSettle._total_out_credit);
                _hiddenNumber(_g.d.POSCashierSettle._total_out_coupon);

                _hiddenNumber(_g.d.POSCashierSettle._total_amount);
                _hiddenNumber(_g.d.POSCashierSettle._total_cancel);

                _hiddenNumber(_g.d.POSCashierSettle._total_balance);
                _hiddenNumber(_g.d.POSCashierSettle._total_diff);

                _hiddenNumber(_g.d.POSCashierSettle._total_cash);
                _hiddenNumber(_g.d.POSCashierSettle._total_advance);

                _hiddenNumber(_g.d.POSCashierSettle._total_credit_card);
                _hiddenNumber(_g.d.POSCashierSettle._total_after_advance);

                _hiddenNumber(_g.d.POSCashierSettle._total_coupon);
                _hiddenNumber(_g.d.POSCashierSettle._sale_credit_amount);
                //_hiddenNumber(_g.d.POSCashierSettle._CoupongAmount);

                // ยอดแต้ัม
                _hiddenNumber(_g.d.POSCashierSettle._total_point);

                _hiddenNumber(_g.d.POSCashierSettle._total_sum);
                //_hiddenNumber(_g.d.POSCashierSettle._sum_amount);
                _hiddenNumber(_g.d.POSCashierSettle._total_credit_charge);
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                {
                    _hiddenNumber(_g.d.POSCashierSettle._order_amount);
                }
            }

            this._screen._textBoxChanged += (s1, e1) =>
            {
                decimal __calc = this._screen._getDataNumber(_g.d.POSCashierSettle._CashAmount) + this._screen._getDataNumber(_g.d.POSCashierSettle._CreditCardAmount) + this._screen._getDataNumber(_g.d.POSCashierSettle._CoupongAmount);
                this._screen._setDataNumber(_g.d.POSCashierSettle._sum_amount, __calc);
            };
            //
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.POS_ส่งเงิน, this._posScreenConfig._posid, DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH")), posFillmoneyDocFormat, _g.g._transControlTypeEnum.POS_รับเงินทอน, _g.g._transControlTypeEnum.ว่าง, "", this._startRunningNumber);
                this._screen._setDataStr(_g.d.POSCashierSettle._DocNo, __newDoc);
                this._screen._setDataStr(_g.d.POSCashierSettle._MACHINECODE, posScreenConfig._posid);
                this._screen._setDataStr(_g.d.POSCashierSettle._CashierCode, this._userCode);
                DateTime __beginDate = DateTime.Now;
                string __beginTime = "00:00";
                this._screen._setDataDate(_g.d.POSCashierSettle._DocDate, __beginDate);
                this._screen._setDataStr(_g.d.POSCashierSettle._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));
                // ดึงรายละเอียด การเปิดกะ
                DataTable __dt = __myFrameWork._queryShort("select * from " + _g.d.pos_period._table + " where " + _g.d.pos_period._machine_code + "=\'" + posScreenConfig._posid + "\' order by " + _g.d.pos_period._period_status + "," + _g.d.pos_period._begin_date + " desc," + _g.d.pos_period._begin_time + " desc").Tables[0];
                if (__dt.Rows.Count > 0)
                {
                    int __status = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.pos_period._period_status].ToString());
                    if (__status == 0)
                    {
                        __beginDate = MyLib._myGlobal._convertDateFromQuery(__dt.Rows[0][_g.d.pos_period._begin_date].ToString());
                        __beginTime = __dt.Rows[0][_g.d.pos_period._begin_time].ToString();
                        this._screen._setDataDate(_g.d.POSCashierSettle._begin_date, __beginDate);
                        this._screen._setDataStr(_g.d.POSCashierSettle._begin_time, __beginTime);
                        this._screen._setDataStr(_g.d.POSCashierSettle._begin_user_code, __dt.Rows[0][_g.d.pos_period._begin_user_code].ToString());
                    }
                }
                string __dateTime = __beginDate.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + " " + __beginTime + ":00";
                string __dateCompare = "to_timestamp(date(" + _g.d.POSCashierSettle._DocDate + ")||\' \'||" + _g.d.POSCashierSettle._doc_time + ",'YYYY/MM/DD HH24:MI')::timestamp";
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                // ยอดรับเงิน (เงินทอน) - #1
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CashAmount + "), 0) as " + _g.d.POSCashierSettle._CashAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + posScreenConfig._posid + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=1"));
                // ยอดส่งเงินสด - #2
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CashAmount + "), 0) as " + _g.d.POSCashierSettle._CashAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + posScreenConfig._posid + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=2"));
                // ยอดส่งบัตรเครดิต - #3
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CreditCardAmount + "), 0) as " + _g.d.POSCashierSettle._CreditCardAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + posScreenConfig._posid + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=2"));
                // ยอดส่งคูปอง - #4
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CoupongAmount + "), 0) as " + _g.d.POSCashierSettle._CoupongAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + posScreenConfig._posid + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=2"));
                // ยอดขาย,ยอดยกเลิก,ยอดรับเงินสด,บัตรเครดิต,ปัดเศษ - #5
                string __dateCompareTrans = "to_timestamp(date(" + _g.d.ic_trans._doc_date + ")||\' \'||" + _g.d.ic_trans._doc_time + ",'YYYY/MM/DD HH24:MI')::timestamp";
                string __fieldCancel = "cancel_amount";
                string __fieldCashAmount = "cash_amount";
                string __fieldCreditCardAmount = "credit_card_amount";
                string __fieldCouponAmount = "coupon_amount";
                string __fieldDiffAmount = "diff_amount";
                string __fieldPointAmount = "point_amount";
                // เงินมัดจำ
                string __fieldAdvanceAmount = "advance_amount";
                string __fieldCreditSaleAmount = "creditsale_amount";
                string __fieldTotalCreaditCharge = "total_credit_charge";

                string __queryTrans = "select " +
                    " sum(" + _g.d.ic_trans._total_amount + ") as " + _g.d.ic_trans._total_amount +
                    ", sum(" + _g.d.ic_trans._advance_amount + ") as " + _g.d.ic_trans._advance_amount +
                    ", sum(" + __fieldCancel + ") as " + __fieldCancel +
                    ", sum(" + __fieldCashAmount + ") as " + __fieldCashAmount +
                    ", sum(" + __fieldCreditCardAmount + ") as " + __fieldCreditCardAmount +
                    ", sum(" + __fieldCouponAmount + ") as " + __fieldCouponAmount +
                    ", sum(" + __fieldDiffAmount + ") as " + __fieldDiffAmount +
                    ", sum(" + __fieldPointAmount + ") as " + __fieldPointAmount +
                    ", sum(" + __fieldCreditSaleAmount + ") as " + __fieldCreditSaleAmount +
                    ", sum(" + __fieldTotalCreaditCharge + ") as " + __fieldTotalCreaditCharge +
                    " from ( " +
                    " select " + _g.d.ic_trans._pos_id + "," + _g.d.ic_trans._total_amount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else " + _g.d.ic_trans._advance_amount + " end as " + _g.d.ic_trans._advance_amount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then " + _g.d.ic_trans._total_amount + " else 0 end as " + __fieldCancel +
                    ",case when " + _g.d.ic_trans._inquiry_type + "=0 then " + _g.d.ic_trans._total_amount + " else 0 end as " + __fieldCreditSaleAmount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._cash_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + ") end as " + __fieldCashAmount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._card_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + ") end as " + __fieldCreditCardAmount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._coupon_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " ) end as " + __fieldCouponAmount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._total_income_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " ) end as " + __fieldDiffAmount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._point_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " ) end as " + __fieldPointAmount +
                    ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._total_credit_charge + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " and " + _g.d.cb_trans._table + "." + _g.d.cb_trans._trans_flag + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._trans_flag + " ) end as " + __fieldTotalCreaditCharge +
                    " from " + _g.d.ic_trans._table +
                    " where " + _g.d.ic_trans._trans_flag + "=44 " +
                    " and " + _g.d.ic_trans._is_pos + "=1 " +
                    " and " + _g.d.ic_trans._pos_id + "=\'" + posScreenConfig._posid + "\' " +
                    " and " + __dateCompareTrans + ">=\'" + __dateTime + "\' " +
                    " and ( " + _g.d.ic_trans._doc_ref + " is null or " + _g.d.ic_trans._doc_ref + " = \'\') ) as temp1";


                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTrans));

                // ยกเลิก
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select doc_no, doc_date, doc_ref, doc_ref_date, (select total_amount from ic_trans as x where x.doc_no = ic_trans.doc_ref and x.trans_flag= 44 ) as sum_amount from ic_trans where trans_flag = 45  and " + _g.d.ic_trans._pos_id + "=\'" + posScreenConfig._posid + "\'  and to_timestamp(date(doc_date)||\' \'|| doc_time,'YYYY/MM/DD HH24:MI')::timestamp >=\'" + __dateTime + "\' "));

                // discount
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select doc_no, doc_date, (select coalesce(sum(discount_amount), 0) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag ) as  item_discount, total_value, total_discount , total_amount from ic_trans where trans_flag = 44 and last_status = 0 and is_pos =1  and " + _g.d.ic_trans._pos_id + "=\'" + posScreenConfig._posid + "\'  and (total_discount > 0 or ((select coalesce(sum(discount_amount), 0) from ic_trans_detail where ic_trans_detail.doc_no = ic_trans.doc_no and ic_trans_detail.trans_flag = ic_trans.trans_flag ) > 0) ) and to_timestamp(date(doc_date)||\' \'|| doc_time,'YYYY/MM/DD HH24:MI')::timestamp >=\'" + __dateTime + "\' "));

                //
                // ยอดค้างเช็คบิล
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                {
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.table_order._sum_amount + "), 0) from " + _g.d.table_order._table + " where " + _g.d.table_order._last_status + " in (0, 2)  and to_timestamp(date(doc_date)||\' \'|| doc_time,'YYYY/MM/DD HH24:MI')::timestamp >=\'" + __dateTime + "\' "));
                }

                __myquery.Append("</node>");
                string __tmpQuery = __myquery.ToString();

                ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                DataTable __dt1 = ((DataSet)__data[0]).Tables[0];
                DataTable __dt2 = ((DataSet)__data[1]).Tables[0];
                DataTable __dt3 = ((DataSet)__data[2]).Tables[0];
                DataTable __dt4 = ((DataSet)__data[3]).Tables[0];
                DataTable __dt5 = ((DataSet)__data[4]).Tables[0];
                this._cancelInvoiceDataTable = ((DataSet)__data[5]).Tables[0];
                this._discountInvoiceDataTable = ((DataSet)__data[6]).Tables[0];

                decimal __totalIn = 0M;
                decimal __totalOut = 0M;
                decimal __totalOutCredit = 0M;
                decimal __totalOutCoupon = 0M;
                decimal __totalSale = 0M;
                decimal __totalSaleCancel = 0M;
                decimal __cashAmount = 0M;
                decimal __creditCardAmount = 0M;
                decimal __couponAmount = 0M;
                decimal __diffAmount = 0M;
                decimal __pointAmount = 0M;

                decimal __advanceAmount = 0M;
                decimal __creditSaleAmount = 0M;

                decimal __total_credit_charge = 0M;

                if (__dt1.Rows.Count > 0) __totalIn = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.POSCashierSettle._CashAmount].ToString());
                if (__dt2.Rows.Count > 0) __totalOut = MyLib._myGlobal._decimalPhase(__dt2.Rows[0][_g.d.POSCashierSettle._CashAmount].ToString());
                if (__dt3.Rows.Count > 0) __totalOutCredit = MyLib._myGlobal._decimalPhase(__dt3.Rows[0][_g.d.POSCashierSettle._CreditCardAmount].ToString());
                if (__dt4.Rows.Count > 0) __totalOutCoupon = MyLib._myGlobal._decimalPhase(__dt4.Rows[0][_g.d.POSCashierSettle._CoupongAmount].ToString());
                if (__dt5.Rows.Count > 0)
                {
                    __totalSale = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][_g.d.ic_trans._total_amount].ToString());
                    __advanceAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][_g.d.ic_trans._advance_amount].ToString());
                    __totalSaleCancel = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCancel].ToString());

                    __creditSaleAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCreditSaleAmount].ToString());

                    //__cashAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCashAmount].ToString()) + (__totalIn - (__totalOut + __totalSaleCancel));
                    __cashAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCashAmount].ToString()) + (__totalIn - __totalOut);
                    __creditCardAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCreditCardAmount].ToString()) - __totalOutCredit;
                    __couponAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCouponAmount].ToString()) - __totalOutCoupon;
                    __diffAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldDiffAmount].ToString());
                    __pointAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldPointAmount].ToString());
                    __total_credit_charge = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldTotalCreaditCharge].ToString());
                    // __totalSale += __total_credit_charge;
                }


                this._screen._setDataNumber(_g.d.POSCashierSettle._total_in, __totalIn);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_out, __totalOut);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_out_credit, __totalOutCredit);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_out_coupon, __totalOutCoupon);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_amount, __totalSale + __advanceAmount);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_cancel, __totalSaleCancel);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_cash, __cashAmount);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_credit_card, __creditCardAmount);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_coupon, __couponAmount);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_diff, __diffAmount);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_point, __pointAmount);

                this._screen._setDataNumber(_g.d.POSCashierSettle._total_credit_charge, __total_credit_charge);

                this._screen._setDataNumber(_g.d.POSCashierSettle._total_advance, __advanceAmount);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_after_advance, (__totalSale - (__totalSaleCancel + __diffAmount)));
                // ยอดขายเงินเชื่อ
                this._screen._setDataNumber(_g.d.POSCashierSettle._sale_credit_amount, __creditSaleAmount);

                // คำนวณ
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_balance, (__totalSale + __advanceAmount) - __totalSaleCancel);
                this._screen._setDataNumber(_g.d.POSCashierSettle._total_sum, (__cashAmount + __creditCardAmount + __couponAmount));

                // ยอดค้างเช็คบิล
                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                {
                    DataTable __dt6 = ((DataSet)__data[7]).Tables[0];
                    if (__dt6.Rows.Count > 0)
                    {
                        decimal __orderAmount = MyLib._myGlobal._decimalPhase(__dt6.Rows[0][0].ToString());
                        if (__orderAmount > 0)
                        {
                            this._screen._setDataNumber(_g.d.POSCashierSettle._order_amount, __orderAmount);
                        }
                    }
                }

            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _hiddenNumber(string controlName)
        {
            Control __getControl = this._screen._getControl(controlName);

            if (__getControl != null && __getControl.GetType() == typeof(MyLib._myNumberBox))
            {
                MyLib._myNumberBox __number1 = (MyLib._myNumberBox)__getControl;
                __number1._hiddenNumberValue = true;
            }

        }

        public Boolean _saveData()
        {
            string __password = this._screen._getDataStr(_g.d.POSCashierSettle._cashier_password).ToString();
            if (this._cashierPassword.Equals(__password) || MyLib._myGlobal._isDemo == true)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.POS_ส่งเงิน, this._posScreenConfig._posid, DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("th-TH")), this._posFillmoneyDocFormat, _g.g._transControlTypeEnum.POS_รับเงินทอน, _g.g._transControlTypeEnum.ว่าง, "", this._startRunningNumber);

                if (__newDoc.Length == 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเลขที่เอกสารส่งเงิน"), MyLib._myGlobal._resource("เตือน"));
                    return false;
                }

                this._screen._setDataStr(_g.d.POSCashierSettle._DocNo, __newDoc);

                _save_doc_no = __newDoc;

                //
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                string __fieldList = MyLib._myGlobal._fieldAndComma(_g.d.POSCashierSettle._MACHINECODE, _g.d.POSCashierSettle._DocNo, _g.d.POSCashierSettle._DocDate, _g.d.POSCashierSettle._doc_time, _g.d.POSCashierSettle._CashierCode, _g.d.POSCashierSettle._CashAmount, _g.d.POSCashierSettle._CreditCardAmount, _g.d.POSCashierSettle._CoupongAmount, _g.d.POSCashierSettle._sum_amount,
                    _g.d.POSCashierSettle._total_in,
                    _g.d.POSCashierSettle._total_out,
                    _g.d.POSCashierSettle._total_out_credit,
                    _g.d.POSCashierSettle._total_out_coupon,
                    _g.d.POSCashierSettle._total_amount,
                    _g.d.POSCashierSettle._total_cancel,
                    _g.d.POSCashierSettle._total_cash,
                    _g.d.POSCashierSettle._total_credit_card,
                    _g.d.POSCashierSettle._total_coupon,
                    _g.d.POSCashierSettle._total_diff,
                    _g.d.POSCashierSettle._total_balance,
                    _g.d.POSCashierSettle._total_sum,
                    _g.d.POSCashierSettle._total_point,
                    _g.d.POSCashierSettle._total_advance,
                    _g.d.POSCashierSettle._total_after_advance,
                    _g.d.POSCashierSettle._deposit_cash_amount,
                    _g.d.POSCashierSettle._deposit_credit_amount,
                    _g.d.POSCashierSettle._deposit_return_cash_amount,
                    _g.d.POSCashierSettle._remark, _g.d.POSCashierSettle._trans_type,
                    _g.d.POSCashierSettle._begin_date,
                    _g.d.POSCashierSettle._begin_time,
                    _g.d.POSCashierSettle._begin_user_code,
                    _g.d.POSCashierSettle._branch_code,
                    _g.d.POSCashierSettle._sale_credit_amount,
                    _g.d.POSCashierSettle._order_amount,
                    _g.d.POSCashierSettle._total_credit_charge
                    );
                string __dataList = MyLib._myGlobal._fieldAndComma(this._screen._getDataStrQuery(_g.d.POSCashierSettle._MACHINECODE), this._screen._getDataStrQuery(_g.d.POSCashierSettle._DocNo), this._screen._getDataStrQuery(_g.d.POSCashierSettle._DocDate), this._screen._getDataStrQuery(_g.d.POSCashierSettle._doc_time), this._screen._getDataStrQuery(_g.d.POSCashierSettle._CashierCode), this._screen._getDataNumber(_g.d.POSCashierSettle._CashAmount).ToString(), this._screen._getDataNumber(_g.d.POSCashierSettle._CreditCardAmount).ToString(), this._screen._getDataNumber(_g.d.POSCashierSettle._CoupongAmount).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._sum_amount).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_in).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_out).ToString(),

                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_out_credit).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_out_coupon).ToString(),

                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_amount).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_cancel).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_cash).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_credit_card).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_coupon).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_diff).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_balance).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_sum).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_point).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_advance).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_after_advance).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._deposit_cash_amount).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._deposit_credit_amount).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._deposit_return_cash_amount).ToString(),
                    this._screen._getDataStrQuery(_g.d.POSCashierSettle._remark) + ",2",
                    this._screen._getDataStrQuery(_g.d.POSCashierSettle._begin_date),
                    this._screen._getDataStrQuery(_g.d.POSCashierSettle._begin_time),
                    this._screen._getDataStrQuery(_g.d.POSCashierSettle._begin_user_code),
                    "\'" + MyLib._myGlobal._branchCode + "\'",
                    this._screen._getDataNumber(_g.d.POSCashierSettle._sale_credit_amount).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._order_amount).ToString(),
                    this._screen._getDataNumber(_g.d.POSCashierSettle._total_credit_charge).ToString());
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.POSCashierSettle._table + " (" + __fieldList + ") values (" + __dataList + ")"));

                // cancel doc
                for (int __row = 0; __row < this._cancelInvoiceDataTable.Rows.Count; __row++)
                {
                    string __field = MyLib._myGlobal._fieldAndComma(_g.d.pos_settle_bill_cancel._doc_no, _g.d.pos_settle_bill_cancel._doc_date, _g.d.pos_settle_bill_cancel._doc_time, _g.d.pos_settle_bill_cancel._ref_doc_date, _g.d.pos_settle_bill_cancel._ref_doc_no,
                        _g.d.pos_settle_bill_cancel._invoice_doc_no, _g.d.pos_settle_bill_cancel._invoice_doc_date, _g.d.pos_settle_bill_cancel._ref_amount, _g.d.pos_settle_bill_cancel._pos_id, _g.d.pos_settle_bill_cancel._period_guid);
                    string __value = MyLib._myGlobal._fieldAndComma(
                        "\'" + __newDoc + "\'",
                        this._screen._getDataStrQuery(_g.d.POSCashierSettle._DocDate),
                        "\'" + this._screen._getDataStr(_g.d.POSCashierSettle._doc_time) + "\'",
                        "\'" + this._cancelInvoiceDataTable.Rows[__row]["doc_date"].ToString() + "\'",
                        "\'" + this._cancelInvoiceDataTable.Rows[__row]["doc_no"].ToString() + "\'",
                        "\'" + this._cancelInvoiceDataTable.Rows[__row]["doc_ref"].ToString() + "\'",
                        (this._cancelInvoiceDataTable.Rows[__row]["doc_ref_date"].ToString().Length == 0 ? "null" : "\'" + this._cancelInvoiceDataTable.Rows[__row]["doc_ref_date"].ToString() + "\'"),
                        "\'" + MyLib._myGlobal._decimalPhase(this._cancelInvoiceDataTable.Rows[__row]["sum_amount"].ToString()).ToString() + "\'",
                        "\'" + this._posScreenConfig._posid + "\'",
                        "\'" + this._periodGUID + "\'");

                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.pos_settle_bill_cancel._table + " (" + __field + ") values (" + __value + ")"));

                }

                // discount doc
                for (int __row = 0; __row < this._discountInvoiceDataTable.Rows.Count; __row++)
                {
                    string __field = MyLib._myGlobal._fieldAndComma(_g.d.pos_settle_bill_discount._doc_no, _g.d.pos_settle_bill_discount._doc_date, _g.d.pos_settle_bill_discount._doc_time,
                        _g.d.pos_settle_bill_discount._ref_doc_date, _g.d.pos_settle_bill_discount._ref_doc_no, _g.d.pos_settle_bill_discount._discount_item, _g.d.pos_settle_bill_discount._sum_amount, _g.d.pos_settle_bill_discount._discount_amount, _g.d.pos_settle_bill_discount._total_amount, _g.d.pos_settle_bill_discount._pos_id, _g.d.pos_settle_bill_discount._period_guid);
                    string __value = MyLib._myGlobal._fieldAndComma(
                        "\'" + __newDoc + "\'",
                        this._screen._getDataStrQuery(_g.d.POSCashierSettle._DocDate),
                        "\'" + this._screen._getDataStr(_g.d.POSCashierSettle._doc_time) + "\'",
                        "\'" + this._discountInvoiceDataTable.Rows[__row]["doc_date"].ToString() + "\'",
                        "\'" + this._discountInvoiceDataTable.Rows[__row]["doc_no"].ToString() + "\'",
                        "\'" + this._discountInvoiceDataTable.Rows[__row]["item_discount"].ToString() + "\'",
                        "\'" + this._discountInvoiceDataTable.Rows[__row]["total_value"].ToString() + "\'",
                        "\'" + this._discountInvoiceDataTable.Rows[__row]["total_discount"].ToString() + "\'",
                        "\'" + this._discountInvoiceDataTable.Rows[__row]["total_amount"].ToString() + "\'",
                        "\'" + this._posScreenConfig._posid + "\'",
                        "\'" + this._periodGUID + "\'");
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.pos_settle_bill_discount._table + " (" + __field + ") values (" + __value + ")"));

                }

                __myQuery.Append("</node>");
                string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__resultStr.Length == 0)
                {
                    MessageBox.Show("บันทึกสำเร็จ");
                    return true;
                }
                else
                {
                    MessageBox.Show(__resultStr);
                }
            }
            else
            {
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
            }
            return false;
        }
    }
}
