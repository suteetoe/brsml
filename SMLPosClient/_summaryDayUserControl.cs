using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace SMLPosClient
{
    public partial class _summaryDayUserControl : UserControl
    {
        private string _cashierCode = "";
        private string _pos_id = "";
        private string _cashierPass = "";

        private string _saleDateSelectResult = "";

        public string _saleDateSelected
        {
            get
            {
                return _saleDateSelectResult;
            }
        }

        public _summaryDayUserControl(string posid, string cashierCode, string cashierPassword)
        {
            InitializeComponent();

            this._cashierCode = cashierCode;
            this._pos_id = posid;
            this._cashierPass = cashierPassword;

            this._screen._maxColumn = 1;
            this._screen._table_name = _g.d.POSCashierSettle._table;

            this._screen._addDateBox(0, 0, 1, 0, _g.d.POSCashierSettle._DocDate, 1, true);

            this._screen._addTextBox(1, 0, _g.d.POSCashierSettle._CashierCode, 0);
            this._screen._addTextBox(2, 0, 1, 0, _g.d.POSCashierSettle._cashier_password, 1, 0, 0, true, true);

            this._screen._addTextBox(3, 0, _g.d.POSCashierSettle._manager_code, 0);
            this._screen._addTextBox(4, 0, 1, 0, _g.d.POSCashierSettle._manager_password, 1, 0, 0, true, true);

            this._screen._setDataDate(_g.d.POSCashierSettle._DocDate, DateTime.Now);
            this._screen._setDataStr(_g.d.POSCashierSettle._CashierCode, this._cashierCode);

            this._screen._enabedControl(_g.d.POSCashierSettle._CashierCode, false);
        }

        public bool _processSaleDaily()
        {
            // check user and password  return true is have permission

            bool __result = false;
            string __userCodeStr = this._screen._getDataStr(_g.d.POSCashierSettle._CashierCode);
            string __userPassStr = this._screen._getDataStr(_g.d.POSCashierSettle._cashier_password);

            string __managerCode = this._screen._getDataStr(_g.d.POSCashierSettle._manager_code);
            string __managerPass = this._screen._getDataStr(_g.d.POSCashierSettle._manager_password);
            string __viewSaleDate = this._screen._getDataStrQuery(_g.d.POSCashierSettle._DocDate);
            // ตรวจสอบ user,password
            if (this._cashierCode.Equals(__userCodeStr) && this._cashierPass.Equals(__userPassStr))
            {
                // ตรวจสอบ ผู้จัดการ
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // รหัสผ่านผู้จัดการ
                string __managerQuery = "select "+ _g.d.erp_user._password + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + __managerCode + "\' and " + _g.d.erp_user._password + "=\'" + __managerPass + "\'";
                DataTable __manager = __myFrameWork._queryShort(__managerQuery).Tables[0];
                if (__manager.Rows.Count > 0)
                {
                    this._saleDateSelectResult = __viewSaleDate;
                    return true;
                }
                else
                {
                    this._saleDateSelectResult = "";
                    MessageBox.Show("รหัสหรือรหัสผ่านผู้จัดการไม่ถูกต้อง");
                }
            }
            else
            {
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง");
            }

            return __result;

        }

        /// <summary>
        /// แสดงตัวอย่างสรุปรายละเอียดการขายสินค้าประจำวัน
        /// </summary>
        /// <param name="selectSaleDate"></param>
        public void _itemSaleSummaryPreview(string selectSaleDate, string posid)
        {
            if (this._processSaleDaily())
            {
                // process query 
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader);
                __query.Append("<node>");

                string __queryItemSummary = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name +
                    ", sum(" + _g.d.ic_trans_detail._qty + ") as " + _g.d.ic_trans_detail._qty +
                    ", sum(" + _g.d.ic_trans_detail._sum_amount + ") as " + _g.d.ic_trans_detail._sum_amount +
                    " from " + _g.d.ic_trans_detail._table +
                    " where " +
                    _g.d.ic_trans_detail._is_pos + " = 1 and " +
                    _g.d.ic_trans_detail._last_status + " = 0 and " +
                    _g.d.ic_trans_detail._doc_date + " = " + selectSaleDate + " and " +
                    " (select " + _g.d.ic_trans._table + "." + _g.d.ic_trans._pos_id + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + ") = \'" + posid + "\' and " +
                    " ( (select coalesce(" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref + ", '') from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " )  = \'\') " +
                    " group by " + _g.d.ic_trans_detail._item_code + ", " + _g.d.ic_trans_detail._item_name + " order by " + _g.d.ic_trans_detail._item_code;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryItemSummary));

                string __queyrTotalItem = "select sum(" + _g.d.ic_trans_detail._qty + ") as " + _g.d.ic_trans_detail._qty +
                    ", sum(" + _g.d.ic_trans_detail._sum_amount + ") as " + _g.d.ic_trans_detail._sum_amount +
                    " from " + _g.d.ic_trans_detail._table +
                    " where " + _g.d.ic_trans_detail._is_pos + " = 1 and " +
                    _g.d.ic_trans_detail._last_status + " = 0 and " +
                    _g.d.ic_trans_detail._doc_date + " = " + selectSaleDate + " and " +
                    " (select " + _g.d.ic_trans._table + "." + _g.d.ic_trans._pos_id + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + ") = \'" + posid + "\' and " +
                    " ( (select coalesce(" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref + ", '') from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + " = " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ) + " ) = \'\') ";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queyrTotalItem));

                //string __queryTrans = "select " +
                //    " coalesce((select count(" + _g.d.ic_trans._doc_no + ")  from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + this._pos_id + "\' and " + _g.d.ic_trans._doc_date + "=" + _saleDateSelected + " and " + _g.d.ic_trans._doc_ref + " is null ), 0) as _invoice_count," +
                //    " coalesce((select sum(" + _g.d.ic_trans._total_discount + ")  from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + this._pos_id + "\' and " + _g.d.ic_trans._doc_date + "=" + _saleDateSelected + " and " + _g.d.ic_trans._doc_ref + " is null ), 0) as " + _g.d.ic_trans._total_discount;
                //    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + _posScreenConfig._posid + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' and " + _g.d.ic_trans._doc_ref + " is null order by " + _g.d.ic_trans._doc_no + " limit 1), '') as _begin_invoice," +
                //    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + _posScreenConfig._posid + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' and " + _g.d.ic_trans._doc_ref + " is null order by " + _g.d.ic_trans._doc_no + " desc limit 1), '') as _end_invoice," +
                //    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=45 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + _posScreenConfig._posid + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' order by " + _g.d.ic_trans._doc_no + " limit 1), '') as _begin_cn," +
                //    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=45 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + _posScreenConfig._posid + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' order by " + _g.d.ic_trans._doc_no + " desc limit 1), '') as _end_cn";

                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTrans));
                //__query.Append(MyLib._myUtil._convertTextToXmlForQuery(""));

                __query.Append("</node>");

                string __debug_query = __query.ToString();

                ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                DataTable __dt1 = ((DataSet)_getData[0]).Tables[0];
                DataTable __dt2 = ((DataSet)_getData[1]).Tables[0];

                string __header = _getHeaderHTML();

                string __footer = @"</table></body>";

                // gentext
                string _viewDate = MyLib._myGlobal._convertDateFromQuery(this._saleDateSelected.Replace("'", string.Empty)).ToString("dd/MM/yyyy");
                string _printDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                //decimal _totalSale = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_amount].ToString());
                //decimal _totalDiscount = MyLib._myGlobal._decimalPhase(__dt2.Rows[0][_g.d.ic_trans._total_discount].ToString());

                StringBuilder __content = new StringBuilder();
                __content.Append(__header);

                __content.Append("<tr><td colspan='2' >วันที่สรุปการขาย :  " + _viewDate + "</td></tr>");
                __content.Append("<tr><td colspan='2' >วันที่ทำรายการ :  " + _printDate + "</td></tr>");
                __content.Append(_getLine());


                // รายละเอียดสินค้า
                for (int __row = 0; __row < __dt1.Rows.Count; __row++)
                {
                    __content.Append(_getTextLine(__dt1.Rows[__row][_g.d.ic_trans_detail._item_code].ToString(), MyLib._myGlobal._decimalPhase(__dt1.Rows[__row][_g.d.ic_trans_detail._qty].ToString()), "left"));
                    __content.Append(_getTextLine(__dt1.Rows[__row][_g.d.ic_trans_detail._item_name].ToString(), MyLib._myGlobal._decimalPhase(__dt1.Rows[__row][_g.d.ic_trans_detail._sum_amount].ToString())));
                }

                // สรุป 
                __content.Append(_getLine());
                __content.Append(_getTextLine("รวม", MyLib._myGlobal._decimalPhase(__dt2.Rows[0][_g.d.ic_trans_detail._qty].ToString()), "left")); // รวมจำนวน
                __content.Append(_getTextLine("", MyLib._myGlobal._decimalPhase(__dt2.Rows[0][_g.d.ic_trans_detail._sum_amount].ToString()))); // รวมราคา


                __content.Append(__footer);

                _helpForm __help = new _helpForm(__content.ToString());
                __help.Text = "สรุปการขายสินค้าประจำวัน";
                __help.ShowDialog(MyLib._myGlobal._mainForm);
            }
        }

        /// <summary>
        /// แสดงตัวอย่างสรุปยอดขายในจอ (แสดงเป็น HTML)
        /// </summary>
        public void _saleSummaryPreview(string selectSaleDate, string posId)
        {
            if (this._processSaleDaily())
            {
                string __fieldCancel = "cancel_amount";
                string __fieldCashAmount = "cash_amount";
                string __fieldCreditCardAmount = "credit_card_amount";
                string __fieldCouponAmount = "coupon_amount";
                string __fieldDiffAmount = "diff_amount";

                string __fieldInvoiceCount = "invoice_count";
                // process query 
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader);
                __query.Append("<node>");

                // 1.สรุปยอดขาย รายได้ ส่วนลด รับเงินสด บัตรเครดิต คูปอง มูลค่าภาษี มูลค่ายกเว้นภาษี่
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(
                    "select " +
                    " count(" + _g.d.ic_trans._doc_no + " ) as " + __fieldInvoiceCount +
                    " , sum(" + _g.d.ic_trans._total_value + ") as " + _g.d.ic_trans._total_value +
                    " , sum(" + _g.d.ic_trans._total_amount + ") as " + _g.d.ic_trans._total_amount +
                    " , sum(" + _g.d.ic_trans._total_discount + ") as " + _g.d.ic_trans._total_discount +
                    " , (sum(" + _g.d.ic_trans._total_amount + ") - sum(" + _g.d.ic_trans._total_discount + ")) as " + _g.d.ic_trans._total_after_discount +
                    " , sum(" + _g.d.ic_trans._total_before_vat + ") as " + _g.d.ic_trans._total_before_vat +
                    " , sum(" + _g.d.ic_trans._total_vat_value + ") as " + _g.d.ic_trans._total_vat_value +
                    " , sum(" + _g.d.ic_trans._total_except_vat + ") as " + _g.d.ic_trans._total_except_vat +
                    " , sum(" + __fieldCancel + ") as " + __fieldCancel +
                    " , sum(" + __fieldCashAmount + ") as " + __fieldCashAmount +
                    " , sum(" + __fieldCreditCardAmount + ") as " + __fieldCreditCardAmount +
                    " , sum(" + __fieldCouponAmount + ") as " + __fieldCouponAmount +
                    " , sum(" + __fieldDiffAmount + ") as " + __fieldDiffAmount +
                    " from " +
                    " (select  " + _g.d.ic_trans._doc_no +
                    " , " + _g.d.ic_trans._pos_id +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else " + _g.d.ic_trans._total_value + " end ) as " + _g.d.ic_trans._total_value +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else " + _g.d.ic_trans._total_amount + " end) as " + _g.d.ic_trans._total_amount +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else " + _g.d.ic_trans._total_discount + " end ) as " + _g.d.ic_trans._total_discount +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else " + _g.d.ic_trans._total_vat_value + " end ) as " + _g.d.ic_trans._total_vat_value +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else " + _g.d.ic_trans._total_before_vat + " end ) as " + _g.d.ic_trans._total_before_vat +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else " + _g.d.ic_trans._total_except_vat + " end ) as " + _g.d.ic_trans._total_except_vat +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then " + _g.d.ic_trans._total_amount + " else 0 end ) as " + __fieldCancel +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._cash_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end ) as " + _g.d.cb_trans._cash_amount +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._card_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end ) as " + __fieldCreditCardAmount +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._coupon_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end ) as " + _g.d.cb_trans._coupon_amount +
                    " , ( case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._total_income_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end ) as " + __fieldDiffAmount +
                    " from " + _g.d.ic_trans._table +
                    " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "='" + posId + "' and " + _g.d.ic_trans._doc_date + "=" + selectSaleDate + " and coalesce(" + _g.d.ic_trans._doc_ref + ", '') = \'\' " +
                    ") as table1"));
                // 2.รับเงินทอน
                string __fillMoneyQuery = "select sum(" + _g.d.POSCashierSettle._CashAmount + ") as " + _g.d.POSCashierSettle._CashAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + posId + "\' and " + _g.d.POSCashierSettle._DocDate + " = " + selectSaleDate + " and " + _g.d.POSCashierSettle._trans_type + "=1";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__fillMoneyQuery));
                // 3.ส่งเงินสด บัตรเครดิต คูปอง
                string __sendMoneyQuery = "select  " + _g.d.POSCashierSettle._doc_time + ", " + _g.d.POSCashierSettle._CashAmount + "," + _g.d.POSCashierSettle._CreditCardAmount + "," + _g.d.POSCashierSettle._CoupongAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + posId + "\' and " + _g.d.POSCashierSettle._DocDate + " = " + selectSaleDate + "  and " + _g.d.POSCashierSettle._trans_type + "=2";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__sendMoneyQuery));
                // 4.ส่งเงินสด บัตรเครดิต คูปอง
                string __summarySendMondyQuery = "select sum(" + _g.d.POSCashierSettle._CashAmount + ") as " + _g.d.POSCashierSettle._CashAmount +
                    ",sum(" + _g.d.POSCashierSettle._CreditCardAmount + ") as " + _g.d.POSCashierSettle._CreditCardAmount +
                    ",sum(" + _g.d.POSCashierSettle._CoupongAmount + ") as " + _g.d.POSCashierSettle._CoupongAmount +
                    " from " + _g.d.POSCashierSettle._table +
                    " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + posId + "\' and " + _g.d.POSCashierSettle._DocDate + " = " + selectSaleDate + "  and " + _g.d.POSCashierSettle._trans_type + "=2";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__summarySendMondyQuery));
                // 5 รายละเอียดบิลขาย
                string __queryTrans = "select " +
                    "(select " + _g.d.POS_ID._POS_ID + " from " + _g.d.POS_ID._table + " where " + _g.d.POS_ID._MACHINECODE + "=\'" + posId + "\') as " + _g.d.POS_ID._POS_ID + "," +
                    " coalesce((select count(" + _g.d.ic_trans._doc_no + ")  from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + posId + "\' and doc_date = " + selectSaleDate + " and coalesce(" + _g.d.ic_trans._doc_ref + ", '') = \'\' ), 0) as invoice_count," +
                    " coalesce((select sum(" + _g.d.ic_trans._total_discount + ")  from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + posId + "\' and doc_date = " + selectSaleDate + " and coalesce(" + _g.d.ic_trans._doc_ref + ", '') = \'\' ), 0) as total_discount," +
                    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + posId + "\' and doc_date = " + selectSaleDate + " and coalesce(" + _g.d.ic_trans._doc_ref + ", '') = \'\' order by " + _g.d.ic_trans._doc_no + " limit 1), '') as begin_invoice," +
                    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + posId + "\' and doc_date = " + selectSaleDate + " and coalesce(" + _g.d.ic_trans._doc_ref + ", '') = \'\' order by " + _g.d.ic_trans._doc_no + " desc limit 1), '') as end_invoice," +
                    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=45 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + posId + "\' and doc_date = " + selectSaleDate + " order by " + _g.d.ic_trans._doc_no + " limit 1), '') as _begin_cn," +
                    " coalesce((select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=45 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + posId + "\' and doc_date = " + selectSaleDate + " order by " + _g.d.ic_trans._doc_no + " desc limit 1), '') as _end_cn";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTrans));

                __query.Append("</node>");

                string __debug_query = __query.ToString();

                ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                DataTable __dt1 = ((DataSet)_getData[0]).Tables[0];
                DataTable __dt2 = ((DataSet)_getData[1]).Tables[0];
                DataTable __dt3 = ((DataSet)_getData[2]).Tables[0];
                DataTable __dt4 = ((DataSet)_getData[3]).Tables[0];
                DataTable __dt5 = ((DataSet)_getData[4]).Tables[0];


                string __header = _getHeaderHTML();

                string __footer = @"</table></body>";

                // gentext
                string _viewDate = MyLib._myGlobal._convertDateFromQuery(this._saleDateSelected.Replace("'", string.Empty)).ToString("dd/MM/yyyy");
                string _printDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

                decimal __total_value = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_value].ToString());

                decimal _totalSale = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_amount].ToString());

                decimal _totalDiscount = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_discount].ToString()); // จาก ส่วนลดท้ายบิล + ลด byitem
                decimal __totalAfterDiscount = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_after_discount].ToString()); // จาก ส่วนลดท้ายบิล + ลด byitem

                decimal __diffAmount = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][__fieldDiffAmount].ToString());
                decimal _totalCancel = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][__fieldCancel].ToString());
                decimal _totalCashAmount = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][__fieldCashAmount].ToString()); // เงินเข้า หัก คืน เป็น ยอดเงินสด
                decimal _totalCreditCardAmount = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][__fieldCreditCardAmount].ToString());
                decimal _totalCouponAmount = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][__fieldCouponAmount].ToString());


                decimal _totalSalevalue = _totalSale + _totalDiscount; // ยอดขายยังไม่หักส่วนลด
                decimal _totalAmount = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_amount].ToString());// _totalSale - _totalCancel;
                decimal _totalImcome = _totalCashAmount + _totalCreditCardAmount + _totalCouponAmount + __diffAmount;

                decimal _moneyIn = MyLib._myGlobal._decimalPhase(__dt2.Rows[0][_g.d.POSCashierSettle._CashAmount].ToString());
                decimal _moneyOut = MyLib._myGlobal._decimalPhase(__dt4.Rows[0][_g.d.POSCashierSettle._CashAmount].ToString());
                decimal _creditCardOut = MyLib._myGlobal._decimalPhase(__dt4.Rows[0][_g.d.POSCashierSettle._CreditCardAmount].ToString());
                decimal _couponOut = MyLib._myGlobal._decimalPhase(__dt4.Rows[0][_g.d.POSCashierSettle._CoupongAmount].ToString());

                decimal _moneyTotalAmount = (_moneyIn + _totalCashAmount) - _moneyOut;
                decimal _totalOut = _moneyOut + _creditCardOut + _couponOut;

                decimal _totalVatValue = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_vat_value].ToString());
                decimal _totalExceptVat = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_except_vat].ToString());
                decimal __total_before_vat = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.ic_trans._total_before_vat].ToString());

                StringBuilder __content = new StringBuilder();
                __content.Append(__header);

                // ชื่อร้าน
                __content.Append("<tr><td colspan='2' align='center' >" + MyLib._myGlobal._ltdName + "</td></tr>");
                __content.Append("<tr><td colspan='2' align='center' >ใบสรุปยอดการขาย</td></tr>");
                __content.Append("<tr><td colspan='2' align='center' >POS ID : " + __dt5.Rows[0][_g.d.POS_ID._POS_ID].ToString() + "</td></tr>");
                if (MyLib._myGlobal._ltdTax != null && MyLib._myGlobal._ltdTax.Length > 0)
                {
                    __content.Append("<tr><td colspan='2' align='center' >Tax ID : " + MyLib._myGlobal._ltdTax + "</td></tr>");
                }

                __content.Append("<tr><td colspan='2' >วันที่ :  " + _viewDate + "</td></tr>");
                //__content.Append("<tr><td colspan='2' >Print Date :  " + _printDate + "</td></tr>");
                __content.Append(_getLine());
                __content.Append(_getHeaderText("รายการขาย"));
                __content.Append(_getStrLing("ใบเสร็จเริ่มต้น : ", __dt5.Rows[0]["begin_invoice"].ToString(), "right"));
                __content.Append(_getStrLing("ใบเสร็จสุดท้าย : ", __dt5.Rows[0]["end_invoice"].ToString(), "right"));


                // ยอดขาย ส่วนลด ยอดขายหักส่วนลด หักรับคืน ยอดขายสุทธิ ยอดขายสุทธิรวม 
                __content.Append(_getHeaderText("สรุปยอดขาย"));
                __content.Append(_getStrLing("จำนวนบิลขาย", MyLib._myGlobal._intPhase(__dt5.Rows[0]["invoice_count"].ToString()).ToString(), "right"));
                __content.Append(_getTextLine("ยอดขาย", __total_value));
                __content.Append(_getTextLine("ส่วนลด", _totalDiscount));
                __content.Append(_getTextLine("ยอดขายหักส่วนลด", __totalAfterDiscount));
                //__content.Append(_getTextLine("หักรับคืน", _totalCancel));
                //__content.Append(_getTextLine("ยอดขายสุทธิ", 0M));
                __content.Append(_getLine());
                __content.Append(_getTextLine("ยอดขายสุทธิ", _totalAmount));
                __content.Append(_getLine());

                __content.Append(_getHeaderText("สรุปภาษี"));
                __content.Append(_getTextLine("มูลค่ายกเว้นภาษี", _totalExceptVat));
                __content.Append(_getTextLine("มูลค่าฐานภาษี", __total_before_vat));
                __content.Append(_getTextLine("ภาษีมูลค่าเพิ่ม", _totalVatValue));
                __content.Append(_getLine());

                // สรุปการรับเงิน 
                // เงินสด เครดิตการ์ด คูปอง รวมรับเงินทั้งสิ้น 
                __content.Append(_getHeaderText("สรุปการรับเงิน"));
                __content.Append(_getTextLine("เงินสด", _totalCashAmount));
                __content.Append(_getTextLine("เครดิตการ์ด", _totalCreditCardAmount));
                __content.Append(_getTextLine("คูปอง", _totalCouponAmount));
                __content.Append(_getTextLine("ปัดเศษ", __diffAmount));
                __content.Append(_getLine());
                __content.Append(_getTextLine("รวมรับเงินทั้งสิ้น", _totalImcome));
                __content.Append(_getLine());

                // สรุปยอดเงินสดในลิ้นชัก 
                // นำเงินสดเข้า รับจาการขาย นำเงินสดออก เงินคงเหลือในลิ้นชัก 
                __content.Append(_getHeaderText("สรุปยอดเงินสดในลิ้นชัก"));
                __content.Append(_getTextLine("นำเงินสดเข้า", _moneyIn));
                __content.Append(_getTextLine("รับจาการขาย", _totalCashAmount));
                __content.Append(_getTextLine("นำเงินสดออก", _moneyOut));
                __content.Append(_getLine());
                __content.Append(_getTextLine("เงินคงเหลือในลิ้นชัก", _moneyTotalAmount));
                __content.Append(_getLine());

                // สรุปยอดเงินส่ง
                if (__dt3.Rows.Count > 0)
                {
                    __content.Append(_getHeaderText("สรุปยอดเงินส่ง"));

                    for (int __row = 0; __row < __dt3.Rows.Count; __row++)
                    {
                        __content.Append(_getStrLing("ครั้งที่ " + (__row + 1) + " เวลา " + __dt3.Rows[__row][_g.d.POSCashierSettle._doc_time].ToString(), "", "left" ));
                        __content.Append(_getTextLine(" เงินสด", MyLib._myGlobal._decimalPhase(__dt3.Rows[__row][_g.d.POSCashierSettle._CashAmount].ToString())));

                        decimal __creditCard = MyLib._myGlobal._decimalPhase(__dt3.Rows[__row][_g.d.POSCashierSettle._CreditCardAmount].ToString());
                        if (__creditCard > 0)
                        {
                            __content.Append(_getTextLine(" บัตรเครดิต", __creditCard));
                        }

                        decimal __coupon = MyLib._myGlobal._decimalPhase(__dt3.Rows[__row][_g.d.POSCashierSettle._CoupongAmount].ToString());
                        if (__coupon > 0)
                        {
                            __content.Append(_getTextLine(" คูปอง", __coupon));
                        }
                    }

                    __content.Append(_getLine());
                    __content.Append(_getTextLine("รวมสุทธิ", _totalOut));
                    __content.Append(_getLine());
                }

                // สรุปภาษี 
                // ยกเว้นภาษี มีภาษี หักภาษีแล้ว ภาษีมูลค่าเพิ่ม 
                //__content.Append(_getTextLine("ยกเว้นภาษี", _totalExceptVat));
                //__content.Append(_getTextLine("มีภาษี", _totalVatValue));
                //__content.Append(_getTextLine("หักภาษีแล้ว", 0M));
                //__content.Append(_getTextLine("ภาษีมูลค่าเพิ่ม", 0M));

                __content.Append(__footer);

                _helpForm __help = new _helpForm(__content.ToString());
                __help.Text = "สรุปการขายประจำวัน";
                __help.ShowDialog(MyLib._myGlobal._mainForm);
            }
        }

        private string _getTextLine(string strName, decimal numberValue)
        {
            return _getTextLine(strName, numberValue, "right");
        }

        private string _getStrLing(string strName, string value, string align)
        {
            return "<tr><td width='80%'>" + strName + "</td><td width='20%' class='" + align + "' >" + value + "</td></tr>";
        }

        private string _getTextLine(string strName, decimal numberValue, string align)
        {
            return "<tr><td width='80%'>" + strName + "</td><td width='20%' class='" + align + "' >" + ((!numberValue.Equals(-1)) ? numberValue.ToString("#,0.00") : numberValue.ToString("#,0.00")) + "</td></tr>";
        }

        private string _getHeaderText(string strHeader)
        {
            return "<tr><td colspan='2' ><h3>" + strHeader + "</h3></td></tr>";
        }

        private string _getLine()
        {
            return "<tr class='border' ><td colspan='2' >&nbsp;</td></tr>";
        }

        private string _getHeaderHTML()
        {
            string __header = @"
<head>
<style type='text/css'>
body,table,tr,td { font-family: Tahoma,Arial, Helvetica, sans-serif; font-size:13px; }
h3 { margin: 10px 0px; font-weight: normal; text-align: center; }
.border { height: 1px; }
.right { text-align: right; }
.border td { border-bottom: 1px solid #000; height: 1px; font-size: 1px; }
</style>

</style>
</head>
<body>
<table borde='0' cellspacing='0' width='100%' >
";
            return __header;
        }

        #region Delegate And Event

        private void _previewButton_Click(object sender, EventArgs e)
        {
            if (_processSaleDaily())
            {
                if (_buttonPreviewClick != null)
                {
                    _buttonPreviewClick(this, e);
                }
            }
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            if (_processSaleDaily())
            {
                if (_buttonPrintClick != null)
                {
                    _buttonPrintClick(this, e);
                }
            }

        }

        #endregion

        public event _PreviewButtonClick _buttonPreviewClick;
        public event _PrintButtonClick _buttonPrintClick;
    }

    public delegate void _PreviewButtonClick(object sender, EventArgs e);
    public delegate void _PrintButtonClick(object sender, EventArgs e);
}
