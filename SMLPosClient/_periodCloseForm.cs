using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace SMLPosClient
{
    public partial class _periodCloseForm : Form
    {
        string _machineCode = "";
        string _cashierPassword = "";
        string _roworder = "";
        public Boolean _closePeroidSuccess = false;
        Boolean _disabled_check_send_money = false;

        public _periodCloseForm(string machineCode, string cashierCode, string cashierPassword, Boolean checkSendMoney)
        {
            InitializeComponent();
            //
            this._machineCode = machineCode;
            this._cashierPassword = cashierPassword;
            this._disabled_check_send_money = checkSendMoney;
            //
            this._screen._table_name = _g.d.pos_period._table;
            this._screen._maxColumn = 1;
            int __row = 0;
            this._screen._addTextBox(__row++, 0, _g.d.pos_period._machine_code, 1);
            this._screen._addDateBox(__row++, 0, 1, 0, _g.d.pos_period._begin_date, 1, true);
            this._screen._addTextBox(__row++, 0, _g.d.pos_period._begin_time, 1);
            this._screen._addTextBox(__row++, 0, _g.d.pos_period._begin_user_code, 1);
            this._screen._addDateBox(__row++, 0, 1, 0, _g.d.pos_period._end_date, 1, true);
            this._screen._addTextBox(__row++, 0, _g.d.pos_period._end_time, 1);
            this._screen._addTextBox(__row++, 0, _g.d.pos_period._end_user_code, 1);
            this._screen._addTextBox(__row++, 0, 1, 1, _g.d.pos_period._password, 1, 1, 0, true, true);
            //
            this._screen._enabedControl(_g.d.pos_period._machine_code, false);
            this._screen._enabedControl(_g.d.pos_period._begin_date, false);
            this._screen._enabedControl(_g.d.pos_period._begin_time, false);
            this._screen._enabedControl(_g.d.pos_period._begin_user_code, false);
            this._screen._enabedControl(_g.d.pos_period._end_date, false);
            this._screen._enabedControl(_g.d.pos_period._end_time, false);
            this._screen._enabedControl(_g.d.pos_period._end_user_code, false);
            //
            this._screen._setDataStr(_g.d.pos_period._machine_code, machineCode);
            this._screen._setDataDate(_g.d.pos_period._end_date, DateTime.Now);
            this._screen._setDataStr(_g.d.pos_period._end_time, DateTime.Now.ToString("HH:mm"));
            this._screen._setDataStr(_g.d.pos_period._end_user_code, cashierCode);
            this.Load += new EventHandler(_periodOpenForm_Load);
        }

        void _periodOpenForm_Load(object sender, EventArgs e)
        {
            // ตรวจสอบว่าเปิดกะค้างไว้หรือเปล่า
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __dt = __myFrameWork._queryShort("select * from " + _g.d.pos_period._table + " where " + _g.d.pos_period._machine_code + "=\'" + this._machineCode + "\' order by " + _g.d.pos_period._period_status + "," + _g.d.pos_period._begin_date + " desc," + _g.d.pos_period._begin_time + " desc").Tables[0];
            if (__dt.Rows.Count > 0)
            {
                int __status = (int)MyLib._myGlobal._decimalPhase(__dt.Rows[0][_g.d.pos_period._period_status].ToString());
                if (__status == 1)
                {
                    MessageBox.Show("ยังไม่มีการเปิดกะ");
                    this.Close();
                }
                else
                {

                    // ตรวจสอบยอดส่งเงิน ระหว่างกะ ครบหรือเปล่า
                    DateTime __beginDate = MyLib._myGlobal._convertDateFromQuery(__dt.Rows[0][_g.d.pos_period._begin_date].ToString());
                    string __beginTime = __dt.Rows[0][_g.d.pos_period._begin_time].ToString();
                    string __dateTime = __beginDate.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + " " + __beginTime + ":00";
                    string __dateCompare = "to_timestamp(date(" + _g.d.POSCashierSettle._DocDate + ")||\' \'||" + _g.d.POSCashierSettle._doc_time + ",'YYYY/MM/DD HH24:MI')::timestamp";



                    StringBuilder __myquery = new StringBuilder();
                    __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // ยอดรับเงิน (เงินทอน) - #1
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CashAmount + "), 0) as " + _g.d.POSCashierSettle._CashAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + this._machineCode + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=1"));
                    // ยอดส่งเงินสด - #2
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CashAmount + "), 0) as " + _g.d.POSCashierSettle._CashAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + this._machineCode + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=2"));
                    // ยอดส่งบัตรเครดิต - #3
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CreditCardAmount + "), 0) as " + _g.d.POSCashierSettle._CreditCardAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + this._machineCode + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=2"));
                    // ยอดส่งคูปอง - #4
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select coalesce(sum(" + _g.d.POSCashierSettle._CoupongAmount + "), 0) as " + _g.d.POSCashierSettle._CoupongAmount + " from " + _g.d.POSCashierSettle._table + " where " + _g.d.POSCashierSettle._MACHINECODE + "=\'" + this._machineCode + "\' and " + __dateCompare + ">=\'" + __dateTime + "\' and " + _g.d.POSCashierSettle._trans_type + "=2"));
                    // ยอดขาย,ยอดยกเลิก,ยอดรับเงินสด,บัตรเครดิต,ปัดเศษ - #5
                    string __dateCompareTrans = "to_timestamp(date(" + _g.d.ic_trans._doc_date + ")||\' \'||" + _g.d.ic_trans._doc_time + ",'YYYY/MM/DD HH24:MI')::timestamp";
                    string __fieldCancel = "cancel_amount";
                    string __fieldCashAmount = "cash_amount";
                    string __fieldCreditCardAmount = "credit_card_amount";
                    string __fieldCouponAmount = "coupon_amount";
                    string __fieldDiffAmount = "diff_amount";
                    string __fieldPointAmount = "point_amount";
                    string __fieldTotalCreditCharge = "total_credit_charge";

                    //string __queryTrans = "select sum(" + _g.d.ic_trans._total_amount + ") as " + _g.d.ic_trans._total_amount + ",sum(" + __fieldCancel + ") as " + __fieldCancel + ",sum(" + __fieldCashAmount + ") as " + __fieldCashAmount + ",sum(" + __fieldCreditCardAmount + ") as " + __fieldCreditCardAmount + ",sum(" + __fieldCouponAmount + ") as " + __fieldCouponAmount + ",sum(" + __fieldDiffAmount + ") as " + __fieldDiffAmount + " from (select " + _g.d.ic_trans._pos_id + "," + _g.d.ic_trans._total_amount + ",case when " + _g.d.ic_trans._last_status + "=1 then " + _g.d.ic_trans._total_amount + " else 0 end as " + __fieldCancel + ",(select sum(" + _g.d.cb_trans._cash_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") as " + __fieldCashAmount + ",(select sum(" + _g.d.cb_trans._card_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") as " + __fieldCreditCardAmount + ",(select sum(" + _g.d.cb_trans._coupon_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") as " + __fieldCouponAmount + ",(select sum(" + _g.d.cb_trans._total_income_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") as " + __fieldDiffAmount + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + this._machineCode + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' and  " + _g.d.ic_trans._doc_ref + " is null ) as temp1";
                    string __queryTrans = "select coalesce(sum(" + _g.d.ic_trans._total_amount + "), 0) as " + _g.d.ic_trans._total_amount + 
                        ",coalesce(sum(" + __fieldCancel + "), 0) as " + __fieldCancel + 
                        ",coalesce(sum(" + __fieldCashAmount + "), 0) as " + __fieldCashAmount + 
                        ",coalesce(sum(" + __fieldCreditCardAmount + "), 0) as " + __fieldCreditCardAmount + 
                        ",coalesce(sum(" + __fieldCouponAmount + "), 0) as " + __fieldCouponAmount + 
                        ",coalesce(sum(" + __fieldDiffAmount + "), 0) as " + __fieldDiffAmount + 
                        ",coalesce(sum(" + __fieldPointAmount + "), 0) as " + __fieldPointAmount +
                        ",coalesce(sum(" + __fieldTotalCreditCharge + "), 0) as " + __fieldTotalCreditCharge + 
                        " from (select " + _g.d.ic_trans._pos_id + "," + _g.d.ic_trans._total_amount + 
                        ",case when " + _g.d.ic_trans._last_status + "=1 then " + _g.d.ic_trans._total_amount + " else 0 end as " + __fieldCancel + 
                        ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._cash_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end as " + __fieldCashAmount + 
                        ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._card_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end as " + __fieldCreditCardAmount + 
                        ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._coupon_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end as " + __fieldCouponAmount + 
                        ",case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._total_income_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end as " + __fieldDiffAmount + 
                        ", case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._point_amount + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end as " + __fieldPointAmount +
                        ", case when " + _g.d.ic_trans._last_status + "=1 then 0 else (select sum(" + _g.d.cb_trans._total_credit_charge + ") from " + _g.d.cb_trans._table + " where " + _g.d.cb_trans._table + "." + _g.d.cb_trans._doc_no + "=" + _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no + ") end as " + __fieldTotalCreditCharge + 
                        " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._trans_flag + "=44 and " + _g.d.ic_trans._is_pos + "=1 and " + _g.d.ic_trans._pos_id + "=\'" + this._machineCode + "\' and " + __dateCompareTrans + ">=\'" + __dateTime + "\' and ( " + _g.d.ic_trans._doc_ref + " is null or " + _g.d.ic_trans._doc_ref + " = \'\') ) as temp1";
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryTrans));
                    //
                    __myquery.Append("</node>");
                    string __tmpQuery = __myquery.ToString();

                    // ยอดส่งเงินสด 
                    // ยอดส่งบัตรเครดิต
                    // ยอดส่งคูปอง
                    ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                    DataTable __dt1 = ((DataSet)__data[0]).Tables[0];
                    DataTable __dt2 = ((DataSet)__data[1]).Tables[0];
                    DataTable __dt3 = ((DataSet)__data[2]).Tables[0];
                    DataTable __dt4 = ((DataSet)__data[3]).Tables[0];
                    DataTable __dt5 = ((DataSet)__data[4]).Tables[0];
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
                    decimal __totalCreditCharge = 0M;

                    // การส่งเงิน
                    if (__dt1.Rows.Count > 0) __totalIn = MyLib._myGlobal._decimalPhase(__dt1.Rows[0][_g.d.POSCashierSettle._CashAmount].ToString());
                    if (__dt2.Rows.Count > 0) __totalOut = MyLib._myGlobal._decimalPhase(__dt2.Rows[0][_g.d.POSCashierSettle._CashAmount].ToString());
                    if (__dt3.Rows.Count > 0) __totalOutCredit = MyLib._myGlobal._decimalPhase(__dt3.Rows[0][_g.d.POSCashierSettle._CreditCardAmount].ToString());
                    if (__dt4.Rows.Count > 0) __totalOutCoupon = MyLib._myGlobal._decimalPhase(__dt4.Rows[0][_g.d.POSCashierSettle._CoupongAmount].ToString());

                    // ยอดขาย
                    if (__dt5.Rows.Count > 0)
                    {
                        __totalSale = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][_g.d.ic_trans._total_amount].ToString());
                        __totalSaleCancel = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCancel].ToString());
                        //__cashAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCashAmount].ToString()) + (__totalIn - (__totalOut + __totalSaleCancel)); 
                        __cashAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCashAmount].ToString()) + (__totalIn - __totalOut);
                        __creditCardAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCreditCardAmount].ToString()) - __totalOutCredit;
                        __couponAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldCouponAmount].ToString()) - __totalOutCoupon;
                        __diffAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldDiffAmount].ToString());
                        __pointAmount = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldPointAmount].ToString());
                        __totalCreditCharge = MyLib._myGlobal._decimalPhase(__dt5.Rows[0][__fieldTotalCreditCharge].ToString());
                    }

                    if (this._disabled_check_send_money == true)
                    {

                        if ((__cashAmount - __pointAmount) > 0 || __creditCardAmount > 0 || __couponAmount > 0)
                        {
                            if (MessageBox.Show("ยอดการส่งเงิน ไม่สัมพันธ์ กับยอดขาย \nต้องการทีจะดำเนินการต่อหรือไม่", "เตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                                this.Close();
                        }
                    }
                    else
                    {
                        if ((__cashAmount - __pointAmount) > 0 || __creditCardAmount > 0 || __couponAmount > 0)
                        {
                            MessageBox.Show("ยอดการส่งเงิน ไม่สัมพันธ์ กับยอดขาย \nกรุณาส่งเงิน ให้ตรงกับยอดขาย ก่อนจะทำการปิดกะ", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }

                    this._roworder = __dt.Rows[0]["roworder"].ToString();
                    this._screen._setDataDate(_g.d.pos_period._begin_date, MyLib._myGlobal._convertDateFromQuery(__dt.Rows[0][_g.d.pos_period._begin_date].ToString()));
                    this._screen._setDataStr(_g.d.pos_period._begin_time, __dt.Rows[0][_g.d.pos_period._begin_time].ToString());
                    this._screen._setDataStr(_g.d.pos_period._begin_user_code, __dt.Rows[0][_g.d.pos_period._begin_user_code].ToString());
                }
            }
            else
            {
                MessageBox.Show("ยังไม่มีการเปิดกะ");
                this.Close();
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            string __password = this._screen._getDataStr(_g.d.pos_period._password).ToString();
            if (this._cashierPassword.Equals(__password) || MyLib._myGlobal._isDemo == true)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.pos_period._table + " set " + _g.d.pos_period._end_date + "=" + this._screen._getDataStrQuery(_g.d.pos_period._end_date) + "," + _g.d.pos_period._end_time + "=" + this._screen._getDataStrQuery(_g.d.pos_period._end_time) + "," + _g.d.pos_period._end_user_code + "=" + this._screen._getDataStrQuery(_g.d.pos_period._end_user_code) + "," + _g.d.pos_period._period_status + "=1 where roworder=" + this._roworder));
                __myQuery.Append("</node>");
                string __resultStr = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__resultStr.Length == 0)
                {
                    MessageBox.Show("ปิดกะสำเร็จ");
                    _closePeroidSuccess = true;
                    this.Close();
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
        }
    }
}
