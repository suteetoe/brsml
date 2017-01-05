using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;

namespace SMLERPControl
{
    public class _arInfoProcess
    {
        public void _checkArOverDueAlert()
        {
            string __endDate = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
            try
            {
                StringBuilder __query = new StringBuilder();
                __query.Append("select cust_code as ar_code , (select name_1 from ar_customer where ar_customer.code = xx.cust_code) as ar_detail, doc_no, doc_date, ref_doc_no, ref_doc_date, amount, balance_amount, due_date from (");

                // ขาย
                __query.Append("select cust_code, doc_date , credit_date as due_date , doc_no , trans_flag as doc_type , used_status , doc_ref as ref_doc_no , doc_ref_date as ref_doc_date , coalesce(total_amount,0) as amount " +
                    ", coalesce(total_amount,0)-(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (239) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date ) as balance_amount  " + // and doc_date <= date('2015-08-04')
                    " from ic_trans  where  coalesce(last_status, 0)=0  and trans_flag=44 and (inquiry_type=0  or inquiry_type=2)  and doc_date <= date('" + __endDate + "')  ");

                __query.Append(" union all ");

                __query.Append(" select cust_code , doc_date , credit_date as due_date , doc_no , trans_flag as doc_type , used_status , '' as ref_doc_no , null as ref_doc_date , coalesce(total_amount,0) as amount " +
                    ", coalesce(total_amount,0)-(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (239) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date ) as balance_amount  " + // and doc_date <= date('2015-08-04')
                    " from ic_trans  where  coalesce(last_status, 0)=0   and (trans_flag=46 or trans_flag=93 or trans_flag=99 or trans_flag=95 or trans_flag=101)  and doc_date <= date('" + __endDate + "')  ");

                __query.Append(" union all ");

                __query.Append(" select cust_code , doc_date , credit_date as due_date , doc_no , trans_flag as doc_type , used_status , '' as ref_doc_no , null as ref_doc_date , -1*coalesce(total_amount,0) as amount " +
                    ", -1*(coalesce(total_amount,0)+(select coalesce(sum(coalesce(sum_pay_money,0)),0) from ap_ar_trans_detail where coalesce(last_status, 0)=0 and trans_flag in (239) and ic_trans.doc_no=ap_ar_trans_detail.billing_no and ic_trans.doc_date=ap_ar_trans_detail.billing_date )) as balance_amount  " + // and doc_date <= date('2015-08-04')
                    " from ic_trans  where  coalesce(last_status, 0)=0   and ((trans_flag=48 and inquiry_type in (0,2,4) ) or trans_flag=97 or trans_flag=103)  and doc_date <= date('" + __endDate + "')  ");

                __query.Append(") as xx ");

                __query.Append(" where balance_amount  <> 0 and (due_date is null or due_date < '" + __endDate + "') order by cust_code, doc_date, doc_no ");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                {
                    // load to grid
                    SMLERPControl._alertForm __alert = new _alertForm();

                    // build
                    string __formatNumberQty = _g.g._getFormatNumberStr(3);

                    __alert.Text = "เตือนครบกำหนดรับชำระเงิน";
                    __alert._grid._isEdit = false;
                    __alert._grid._total_show = true;
                    __alert._grid._table_name = _g.d.ic_resource._table;
                    __alert._grid._addColumn(_g.d.ic_resource._ar_code, 1, 20, 20);
                    __alert._grid._addColumn(_g.d.ic_resource._ar_detail, 1, 25, 25);

                    __alert._grid._addColumn(_g.d.ic_resource._doc_date, 4, 20, 20);
                    __alert._grid._addColumn(_g.d.ic_resource._doc_no, 1, 20, 20);
                    __alert._grid._addColumn(_g.d.ic_resource._due_date, 4, 20, 20);

                    __alert._grid._addColumn(_g.d.ic_resource._amount, 3, 20, 20, true, false, true, false, __formatNumberQty);
                    __alert._grid._addColumn(_g.d.ic_resource._balance_amount, 3, 20, 20, true, false, true, false, __formatNumberQty);

                    __alert._grid._calcPersentWidthToScatter();
                    __alert._grid._loadFromDataTable(__result.Tables[0]);
                    __alert.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    __alert.ShowDialog(MyLib._myGlobal._mainForm);
                }
            }
            catch
            {

            }
        }


        public static void _sendRequestARCredit()
        {
            Thread.Sleep(60 * 1000);

            while (true)
            {
                string __getRequestARCreditQuery = "select doc_no, approve_code, cust_code, (select total_amount from ic_trans_draft where ic_trans_draft.doc_no = erp_request_order.doc_no) as sum_amount " +

                    ", (select name_1 from ar_customer where ar_customer.code = erp_request_order.cust_code ) as cust_name " +
                    // เบอร์ SMS สาขา ของบิล
                    ", (select phone_number_approve from erp_branch_list where erp_branch_list.code =(select branch_code from ic_trans_draft where ic_trans_draft.doc_no = erp_request_order.doc_no)) as sms_phone " +

                    " from " + _g.d.erp_request_order._table + " where " + _g.d.erp_request_order._send_success + "=0";

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getARRequest = __myFrameWork._queryShort(__getRequestARCreditQuery).Tables[0];


                for (int __row = 0; __row < __getARRequest.Rows.Count; __row++)
                {
                    string __refNo = __getARRequest.Rows[__row][_g.d.erp_request_order._doc_no].ToString();
                    string __approveCode = __getARRequest.Rows[__row][_g.d.erp_request_order._approve_code].ToString();
                    string __custCode = __getARRequest.Rows[__row][_g.d.erp_request_order._cust_code].ToString();
                    decimal __requestAmount = MyLib._myGlobal._decimalPhase(__getARRequest.Rows[__row]["sum_amount"].ToString());
                    string __custName = __getARRequest.Rows[__row]["cust_name"].ToString();

                    string __smsFromBranch = __getARRequest.Rows[__row]["sms_phone"].ToString();

                    SMLERPARAPInfo._process __arapProcess = new SMLERPARAPInfo._process();
                    DataTable __creditTable = __arapProcess._arCreditMoneyBalance(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้, __custCode);

                    decimal __credit_money = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money].ToString());
                    decimal __credit_money_max = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._credit_money_max].ToString());
                    decimal __credit_balance = MyLib._myGlobal._decimalPhase(__creditTable.Rows[0][_g.d.ap_ar_resource._balance_end].ToString());
                    decimal __credit_status = MyLib._myGlobal._intPhase(__creditTable.Rows[0][_g.d.ar_customer_detail._credit_status].ToString());

                    if (_g.g._companyProfile._ar_credit_chq_outstanding)
                    {
                        decimal __chqOnHand = MyLib._myGlobal._intPhase(__creditTable.Rows[0]["chq_outstanding"].ToString());
                        __credit_balance += __chqOnHand;
                    }

                    // send sms

                    string __message = "";

                    if (_g.g._companyProfile._request_credit_type == 1)
                    {
                        // sms
                        __message = string.Format("Cus {2} TC {4} Debt {5} Buy {3} Doc {0} UC {1}", __refNo, __approveCode, __custName, __requestAmount.ToString(), __credit_money.ToString(), __credit_balance.ToString());

                        // 1. เอา เบอร์จากสาขาเอกสาร
                        string __phoneNumber = __smsFromBranch;

                        // 2. ตามตารางกำหนดช่วงวงเงินอนุมัติ

                        // 3. เอาจาก 1.1.2
                        if (__phoneNumber.Length == 0)
                        {
                            __phoneNumber = _g.g._companyProfile._phone_number_approve;
                        }

                        if (__phoneNumber.Length > 0)
                        {
                            // 
                            string __sendSMSresult = MyLib.SendSMS._sendSMS._send(__phoneNumber, System.Uri.EscapeUriString(__message));
                            if (__sendSMSresult.Length > 0)
                            {
                                // หยุดการส่งข้อความทั้งหมด และ ออกจาก thread
                                break;
                            }
                        }

                    }
                    else if (_g.g._companyProfile._request_credit_type == 2)
                    {
                        // sale hub
                        __message = string.Format("เกินวงเงิน {2} วงเงินเครดิต {4} บาท ยอดหนี้ปัจจุบัน {5} บาท ยอดที่สั่งซื้อ {3} บาท เลขที่ {0} รหัสอนุมัติ {1}", __refNo, __approveCode, __custName, __requestAmount, __credit_money, __credit_balance);

                    }

                    // update send_success
                    string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update erp_request_order set send_success = 1 where doc_no =\'" + __refNo + "\' ");
                    if (__result.Length > 0)
                    {
                        //Console.WriteLine(__result);
                    }

                    // sleep
                    Thread.Sleep(5000);
                }

                Thread.Sleep(30 * 1000);
            }
        }
    }
}
