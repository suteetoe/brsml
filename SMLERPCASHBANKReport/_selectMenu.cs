using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                case "menu_cash_income_other_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายได้อื่น, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น, screenName); // รายงานรายได้อื่นๆ
                case "menu_cash_income_other_credit_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้, screenName); // รายงานลดหนี้รายได้อื่นๆ
                case "menu_cash_income_other_debit_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้, screenName); // รายงานเพิ่มหนี้รายได้อื่นๆ

                // ยกเลิกรายได้
                case "menu_cash_income_other_cancel_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก, screenName); // รายงานยกเลิกรายได้อื่น
                case "menu_cash_income_other_credit_cancel_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก, screenName); // รายงานยกเลิกลดหนี้รายได้อื่นๆ
                case "menu_cash_income_other_debit_cancel_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก, screenName); // รายงานยกเลิกเพิ่มหนี้รายได้อื่นๆ


                case "menu_cash_expense_other_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายจ่ายอื่น, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น, screenName); // รายงานค่าใช้จ่ายอื่นๆ
                case "menu_cash_expense_other_credit_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้, screenName); // รายงานลดหนี้ค่าใช้จ่ายอื่นๆ
                case "menu_cash_expense_other_debit_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้, screenName); // รายงานเพิ่มหนี้ค่าใช้จ่ายอื่นๆ

                // ยกเลิกรายจ่าย
                case "menu_cash_expense_other_cancel_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก, screenName); // รายงานยกเลิกรายจ่ายอื่น
                case "menu_cash_expense_other_credit_cancel_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก, screenName); // รายงานยกเลิกลดหนี้รายจ่ายอื่น
                case "menu_cash_expense_other_debit_cancel_report": return new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก, screenName); // รายงานยกเลิกเพิ่มหนี้รายจ่ายอื่น

                // toe
                case "menu_cb_report_withdraw": return (new SMLERPCASHBANKReport._report_bank._report_deposit_cash());//001-รายงานการฝากเงินสด
                case "menu_cb_report_transfer_bank": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.ธนาคาร_โอนเงิน, _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร, screenName)); // รายงานการโอนเงินระหว่างธนาคาร

                case "menu_report_cancel_withdraw": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.ธนาคาร_ฝากเงิน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก, screenName)); // รายงานยกเลิกฝากเงิน
                case "menu_report_cancel_payin": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.ธนาคาร_ถอนเงิน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก, screenName)); // รายงานยกเลิกถอนเงิน
                case "menu_report_transfer_bank_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.ธนาคาร_โอนเงิน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก, screenName)); // รายงานยกเลิกการโอนเงินระหว่างธนาคาร

                ////////////ธนาคาร / เงินสด
                case "menu_bank_report_cash_deposit": return (new SMLERPCASHBANKReport._report_bank._report_deposit_cash());//001-รายงานการฝากเงินสด
                case "menu_bank_report_cash_withdraw": return (new SMLERPCASHBANKReport._report_bank._report_withdraw_cash());//002-รายงานถอนเงินสด
                case "menu_bank_report_revenue_by_bank": return (new SMLERPCASHBANKReport._report_bank._report_income_bank());//003-รายงานรายได้จากธนาคาร
                case "menu_bank_report_expend_by_bank": return (new SMLERPCASHBANKReport._report_bank._report_costs_bank());//004-รายงานรายจ่ายจากธนาคาร
                case "menu_bank_report_interest_receive": return (new SMLERPCASHBANKReport._report_bank._report_interest());//005-รายงานดอกเบี้ยรับ
                case "menu_bank_report_interest_payment": return (new SMLERPCASHBANKReport._report_bank._report_interest_expense());//006-รายงานดอกเบี้ยจ่าย
                case "menu_bank_report_transfer_in": return (new SMLERPCASHBANKReport._report_bank._report_transfer());//007-รายงานโอนเงินเข้า
                case "menu_bank_report_transfer_out": return (new SMLERPCASHBANKReport._report_bank._report_transfer_out());//008-รายงานโอนเงินออก


                // toe เช็ครับ ยกเลิก
                case "menu_report_cb_chq_in_payin_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ฝาก_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก, screenName)); // รายงานยกเลิกนำฝากเช็ค
                case "menu_report_cb_chq_in_honor_cancle": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ผ่าน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก, screenName)); // รายงานยกเลิกเช็คผ่าน
                case "menu_report_cb_chq_in_return_cancle": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_รับคืน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก, screenName)); // รายงานยกเลิกเช็ครับคืน
                case "menu_report_cb_chq_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ขาดสิทธิ์_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก, screenName)); // รายงานยกเลิก ยกเลิกเช็ค ขาดสิทธิ์
                case "menu_report_cb_chq_in_change_cancle": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_เปลี่ยน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก, screenName)); // รายงานยกเลิกเปลี่ยนเช็ค

                case "menu_report_cb_chq_out_honor_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_ผ่าน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก, screenName)); // รายงานยกเลิกเช็คจ่ายผ่าน
                case "menu_report_cb_chq_out_cancel_hornor_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_ขาดสิทธิ์_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก, screenName)); // รายงานยกเลิก ยกเลิกเช็คจ่าย
                case "menu_report_chq_out_return_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_คืน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก, screenName)); // รายงานยกเลิกเช็คคืน
                case "menu_report_cb_chq_out_change_cancle": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_เปลี่ยน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก, screenName)); // รายงานยกเลิกเปลี่ยนเช็คจ่าย

                //เช็ค รับ
                // toe 

                case "menu_cb_report_chq_in_receive_balance": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ยกมา, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา, screenName)); //  // รายงานเช็ครับยกมา
                //รายงานนำฝากเช็ครับ
                case "menu_bank_report_chq_in_deposit": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ฝาก, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก, screenName));  // นำฝากเช็ครับ return (new SMLERPCASHBANKReport._report_chq._cb_chqindeposit());
                //รายงานเช็ครับผ่าน
                case "menu_bank_report_chq_in_pass": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ผ่าน, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน, screenName));  // return (new SMLERPCASHBANKReport._report_chq._cb_chqinpass());
                //รายงานเช็ครับคืน
                case "menu_bank_report_chq_in_return": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_รับคืน, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน, screenName));  //return (new SMLERPCASHBANKReport._report_chq._cb_chqinreturn());
                //รายงานยกเลิกเช็ครับผ่าน
                case "menu_bank_report_chq_in_pass_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ขาดสิทธิ์, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก, screenName));  //return (new SMLERPCASHBANKReport._report_chq._cb_chqinpasscancel());
                //รายงานเปลี่ยนเช็คนำฝาก
                case "menu_bank_report_chq_in_change": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_เปลี่ยนเช็ค, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค, screenName));  //return (new SMLERPCASHBANKReport._report_chq._cb_chqinchange());

                //รายงานสถานะเช็ครับ
                case "menu_bank_report_chq_in_status": return (new SMLERPCASHBANKReport._report_chq._cb_chqinstatus());
                //รายงานเช็ครับ
                case "menu_bank_report_chq_in_receive": return (new SMLERPCASHBANKReport._report_chq._cb_chqinreceive());
                //รายงานยกเลิกเช็ครับ
                case "menu_bank_report_chq_in_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqincancel());
                //รายงานยกเลิกนำฝากเช็ครับ
                case "menu_bank_report_chq_in_deposit_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqindepositcancel());
                //รายงานยกเลิกเช็ครับคืน
                case "menu_bank_report_chq_in_return_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqinreturncancel());
                //รายงานยกเลิกเปลี่ยนเช็คนำฝาก
                case "menu_bank_report_chq_in_change_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqinchangecancel());
                //รายงานนำเช็คเข้าใหม่
                case "menu_bank_report_chq_in_renew": return (new SMLERPCASHBANKReport._report_chq._cb_chqinrenew());
                //รายงานยกเลิกนำเช็คเข้าใหม่
                case "menu_bank_report_chq_in_renew_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqinrenewcancel());

                //เช็ค จ่าย
                // เช็คจ่ายยกมา
                case "menu_cb_report_chq_out_payment_balance": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_ยกมา, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา, screenName));
                //รายงานเช็คจ่ายผ่าน
                case "menu_bank_report_chq_out_pass": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_ผ่าน, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน, screenName)); // return (new SMLERPCASHBANKReport._report_chq._cb_chqoutpass());
                //รายงานเช็คจ่ายคืน
                case "menu_bank_report_chq_out_return": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_คืน, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน, screenName)); //return (new SMLERPCASHBANKReport._report_chq._cb_chqoutreturn());
                //รายงานยกเลิกเช็คจ่ายผ่าน
                case "menu_bank_report_chq_out_pass_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_ขาดสิทธิ์, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก, screenName)); // return (new SMLERPCASHBANKReport._report_chq._cb_chqoutpasscancel());
                //รายงานเปลี่ยนเช็คจ่าย
                case "menu_bank_report_chq_out_change": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_เปลี่ยนเช็ค, _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค, screenName)); //return (new SMLERPCASHBANKReport._report_chq._cb_chqoutchange());

                //รายงานสถานะเช็คจ่าย
                case "menu_bank_report_chq_out_status": return (new SMLERPCASHBANKReport._report_chq._cb_chqoutstatus());
                //รายงานเช็คจ่าย
                case "menu_bank_report_chq_out_payment": return (new SMLERPCASHBANKReport._report_chq._cb_chqoutpayment());
                //รายงานยกเลิกเช็คจ่าย
                case "menu_bank_report_chq_out_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqoutcancel());
                //รายงานยกเลิกเช็คจ่ายคืน
                case "menu_bank_report_chq_out_return_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqoutreturncancel());
                //รายงานยกเลิกเปลี่ยนเช็คจ่าย
                case "menu_bank_report_chq_out_change_cancel": return (new SMLERPCASHBANKReport._report_chq._cb_chqoutchangecancel());

                //บัตรเครดิต
                //รายงานรายการบัตรเครดิต
                case "menu_bank_report_credit_card": return (new SMLERPCASHBANKReport._report_credit._cb_creditcard());
                //รายงานยกเลิกรายการบัตรเครดิต
                case "menu_bank_report_credit_card_cancel": return (new SMLERPCASHBANKReport._report_credit._cb_creditcardcancel());
                //รายงานขึ้นเงินบัตรเครดิต
                case "menu_bank_report_credit_card_get_money": return (new SMLERPCASHBANKReport._report_credit._cb_craditcardgetmoney());
                //รายงานยกเลิกขึ้นเงินบัตรเครดิต
                case "menu_bank_report_credit_card_get_money_cancel": return (new SMLERPCASHBANKReport._report_credit._cb_craditcardgetmoneycancel());


                //เงินสดย่อย
                //รายงานกำหนดวงเงินสดย่อย
                case "menu_cash_report_sub_cash_open": return (new SMLERPCASHBANKReport._cb_subcashopen());
                //รายงานรับเงินสดย่อย
                case "menu_cash_report_sub_cash_receive": return (new SMLERPCASHBANKReport._cb_subcashreceive());
                //รายงานยกเลิกรับเงินสดย่อย
                case "menu_cash_report_sub_cash_receive_cancel":
               
                    return (new SMLERPCASHBANKReport._cb_subcashreceivecancel());
                //รายงานขอเบิกเงินสดย่อย
                case "menu_cash_report_sub_cash_withdraw": return (new SMLERPCASHBANKReport._cb_subcashwithdraw());
                //รายงานยกเลิกขอเบิกเงินสดย่อย
                case "menu_cash_report_sub_cash_withdraw_cancle": return (new SMLERPCASHBANKReport._cb_subcashwithdrawcancel());
                //รายงานรับคือเงินสดย่อย
                case "menu_cash_report_sub_cash_return": return (new SMLERPCASHBANKReport._cb_subcashreturn());
                //รายงานยกเลิกรับคือเงินสดย่อย
                case "menu_cash_report_sub_cash_return_cancle":
                
                    return (new SMLERPCASHBANKReport._cb_subcashreturncancel());

                case "menu_report_cb_cancel_pettycash_request":  return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เงินสดย่อย_เบิก_ยกเลิก, _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก, screenName));  // เงินสดย่อย ยกเลิก

                case "menu_report_cb_cancel_pettycash_return": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เงินสดย่อย_รับคืน_ยกเลิก, _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก, screenName));  // เงินสดย่อย ยกเลิก
                                                                                                                                                                                                                                          //รายงานการเคลื่อนไหว
                                                                                                                                                                                                                                          //รายงานเคลื่อนไหวเงินสดย่อย
                case "menu_sub_cash_movements": return (new _report_petty_cash(SMLERPReportTool._reportEnum.เงินสดย่อย_เคลื่อนไหว, _g.g._transControlTypeEnum.ว่าง, screenName)); //(new SMLERPCASHBANKReport._cb_subcashmovements());
                //รายงานเคลื่อนไหวเงินสด
                case "_menu_cash_movements": return (new _cashbank_report(SMLERPReportTool._reportEnum.เคลื่อนไหวเงินสด, _g.g._transControlTypeEnum.ว่าง, screenName));   //return (new SMLERPCASHBANKReport._report_trans._cb_cashmovements());
                //รายงานสรุปการจ่ายเงิน
                case "_menu_summary_payment": return (new SMLERPCASHBANKReport._report_trans._cb_summarypayment());
                //รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
                case "_menu_book_bank_balance": return (new SMLERPCASHBANKReport._report_trans._cb_bookbankbalance());
                //รายงาน Bank Statement
                case "_menu_bank_statement": return (new SMLERPCASHBANKReport._report_trans._cb_bankstatement());
                //รายงาน Statement ล่วงหน้า
                case "_menu_statement_advance": return (new SMLERPCASHBANKReport._report_trans._cb_bankstatementadvance());

                //รายงาน เช็ค เครดิต ของ ICI
                // 
                case "menu_cb_report_chq_in_receive": return (new SMLERPCASHBANKReport._report_chq._chqCreditCardList(screenName, SMLERPReportTool._reportEnum.เช็ค_รายงานเช็ครับ_ตามวันที่รับเช็ค)); // พี่ระให้ย้าย รายละเอียดเช็คมาแทน return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็ครับ_ทะเบียนเช็ครับ, _g.g._transControlTypeEnum.ว่าง, screenName)); // รายงานทะเบียนเช็ครับ
                // case "menu_cb_report_chq_in_receive_by_date" : เปลี่ยนไปเรียก fast report
                //case "menu_cb_report_chq_in_receive": return (new SMLERPCASHBANKReport._report_cb_ici._chq_receive());
                //
                case "menu_cb_report_chq_out_payment": return (new SMLERPCASHBANKReport._report_chq._chqCreditCardList(screenName, SMLERPReportTool._reportEnum.เช็ค_รายงานเช็คจ่าย_ตามวันที่จ่ายเช็ค)); // ย้ายจากรายละเอียดเช็คมา return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.เช็คจ่าย_ทะเบียนเช็คจ่าย, _g.g._transControlTypeEnum.ว่าง, screenName)); // รายงานทะเบียนเช็คจ่าย
                //case "menu_cb_report_chq_out_payment_by_date" : 
                //case "menu_cb_report_chq_out_payment": return (new SMLERPCASHBANKReport._report_cb_ici._chq_payment());

                case "menu_cb_report_credit_card_pass": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.บัตรเครดิต_ขึ้นเงิน, _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน, screenName));//  รายงานขึ้นเงินบัตรเครดิต
                case "menu_report_cb_credit_card_cancel": return (new SMLERPReportTool._reportCashBankTrans(SMLERPReportTool._reportEnum.บัตรเครดิต_ยกเลิก, _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก, screenName));//  รายงานยกเลิกบัตรเครดิต


                //รายงานรายการรับบัตรเครดิต
                case "menu_cb_report_credit_card_receive": return (new SMLERPCASHBANKReport._report_chq._chqCreditCardList(screenName, SMLERPReportTool._reportEnum.บัตรเครดิต_รายงานบัตรเครดิต_ตามวันที่รับ));
                //case "menu_cb_report_credit_card_receive": return (new SMLERPCASHBANKReport._report_cb_ici._credit_receive());
                //รายงานรายการจ่ายบัตรเครดิต
                case "menu_cb_report_credit_card_payment": return (new SMLERPCASHBANKReport._report_cb_ici._credit_payment());
                case "menu_ap_report_payment_daily": return new SMLERPCASHBANKReport._dailyPaymentIncome(screenName, SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายงานการจ่ายเงินประจำวัน);//รายงานการจ่ายเงินประจำวัน
                case "menu_ar_report_receipt_daily": return new SMLERPCASHBANKReport._dailyPaymentIncome(screenName, SMLERPReportTool._reportEnum.เงินสดธนาคาร_รายงานการรับเงินประจำวัน);//รายงานการรับเงินประจำวัน

                // toe รายงานสถานะเงินสดย่อย
                case "menu_peety_cash_status": return (new _report_petty_cash(SMLERPReportTool._reportEnum.เงินสดย่อย_สถานะ, _g.g._transControlTypeEnum.ว่าง, screenName));
            }
            return null;
        }
    }
}
