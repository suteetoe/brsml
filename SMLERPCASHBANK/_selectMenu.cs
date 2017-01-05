using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANK
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_cash_income_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น, menuName));
                case "menu_cash_income_other_credit": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้, menuName));
                case "menu_cash_income_other_debit": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้, menuName));
                // ยกเลิก รายได้อื่น ๆ
                case "menu_cash_income_other_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก, menuName));
                case "menu_cash_income_other_credit_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก, menuName));
                case "menu_cash_income_other_debit_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก, menuName));
                //
                case "menu_cash_expense_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น, menuName));
                case "menu_cash_expense_other_credit": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้, menuName));
                case "menu_cash_expense_other_debit": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้, menuName));
                // ยกเลิกรายจ่ายอื่น ๆ 
                case "menu_cash_expense_other_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก, menuName));
                case "menu_cash_expense_other_credit_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก, menuName));
                case "menu_cash_expense_other_debit_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก, menuName));

                // เงินสดย่อย
                case "menu_cb_petty_cash_receive": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย, menuName));
                case "menu_cb_petty_cash_receive_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย_ยกเลิก, menuName));
                case "menu_cb_petty_return": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย, menuName));
                case "menu_cb_petty_return_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย_ยกเลิก, menuName));
                // บันทึกฝากเงิน
                case "menu_cb_cash_payin": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน, menuName));
                case "menu_cb_cash_payin_cancle": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน_ยกเลิก, menuName)); // return (new SMLERPCASHBANK._cb_cash_payin_cancle());
                // บันทึกถอนเงิน
                case "menu_cb_cash_withdraw": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน, menuName));
                case "menu_cb_cash_withdraw_cancle": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน_ยกเลิก, menuName));//return (new SMLERPCASHBANK._cb_cash_withdraw_cancle());
                // โอนเงินระหว่างธนาคาร
                case "menu_cash_transfer": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร, menuName));
                case "menu_cash_transfer_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_โอนเงินออกธนาคาร_ยกเลิก, menuName));

                // เช็คยกมา
                case "menu_cb_chq_in_receive_balance": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา, menuName));
                case "menu_cb_chq_out_payment_balance": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา, menuName));

                // เช็ครับ
                case "menu_cb_chq_in_payin": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก, menuName));
                case "menu_cb_chq_in_pass": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน, menuName));
                case "menu_cb_chq_in_return": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน, menuName));
                case "menu_cb_chq_in_repass": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่, menuName));
                case "menu_cb_chq_in_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก, menuName));
                case "menu_cb_chq_renew": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค, menuName)); // บันทึกเปลี่ยนเช็ครับ

                // เช็ครับยกเลิก
                case "menu_cb_chq_in_payin_cancle": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก_ยกเลิก, menuName));
                case "menu_cb_chq_in_pass_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน_ยกเลิก, menuName));
                case "menu_cb_chq_in_return_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน_ยกเลิก, menuName));
                case "menu_cb_chq_in_repass_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่_ยกเลิก, menuName));
                case "menu_cb_chq_in_cancel_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก_ยกเลิก, menuName));
                case "menu_cb_chq_renew_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค_ยกเลิก, menuName));

                // เช็คจ่าย
                case "menu_cb_chq_out_pass": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน, menuName));
                case "menu_cb_chq_out_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก, menuName));
                case "menu_cb_chq_out_return": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน, menuName)); // เช็คจ่ายคืน
                case "menu_cb_chq_out_renew": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค, menuName)); // บันทึกเปลี่ยนเช็คจ่าย

                // เช็คจ่ายยกเลิก
                case "menu_cb_chq_out_pass_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน_ยกเลิก, menuName));
                case "menu_cb_chq_out_cancel_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก_ยกเลิก, menuName));
                case "menu_cb_chq_out_return_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน_ยกเลิก, menuName));
                case "menu_cb_chq_out_renew_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค_ยกเลิก, menuName));

                // บัตรเครดิต
                case "menu_cb_credit_card_pass": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน, menuName));
                case "menu_cb_credit_card_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก, menuName));
                //
                // เช็ค - เงินสด ธนาคาร
                //เงินสด
                //_bankControl : cash_payin : บันทึกฝากเงิน
                //case "menu_cb_cash_payin": return (new SMLERPCASHBANK._cb_cash_payin());
                //_bankControl : cash_payin_cancle : บันทึกยกเลิกฝากเงิน
                //_bankControl : cash_withdraw : บันทึกถอนเงิน
                //case "menu_cb_cash_withdraw": return (new SMLERPCASHBANK._cb_cash_withdraw());
                //_bankControl : cash_withdraw_cancle : บันทึกยกเลิกถอนเงิน
                //_bankControl : Received : บันทึกรายได้จากธนาคาร 
                case "menu_cb_bank_income": return (new SMLERPCASHBANK._cb_bank_income());
                //_bankControl : Received cancle : บันทึกยกเลิกรายได้จากธนาคาร 
                case "menu_cb_bank_income_cancle": return (new SMLERPCASHBANK._cb_bank_income_cancle());
                //_bankControl : Payment : บันทึกค่าใช้จ่ายธนาคาร
                case "menu_cb_bank_expense": return (new SMLERPCASHBANK._cb_bank_expense());
                //_bankControl : Payment cancle : บันทึกยกเลิกค่าใช้จ่ายธนาคาร
                case "menu_cb_bank_expense_cancle": return (new SMLERPCASHBANK._cb_bank_expense_cancle());
                //_bankControl : interest received : ดอกเบี้ยรับ
                case "menu_cb_interest_received": return (new SMLERPCASHBANK._cb_interest_received());
                //_bankControl : interest received cancle : ยกเลิกดอกเบี้ยรับ
                case "menu_cb_interest_received_cancle": return (new SMLERPCASHBANK._cb_interest_received_cancle());
                //_bankControl : interest payment : ดอกเบี้ยจ่าย
                case "menu_cb_interest_payment": return (new SMLERPCASHBANK._cb_interest_payment());
                //_bankControl : interest payment cancle : ยกเลิกดอกเบี้ยจ่าย
                case "menu_cb_interest_payment_cancle": return (new SMLERPCASHBANK._cb_interest_payment_cancle());
                //_bankControl : cash transfer received : บันทึกโอนเงินเข้า
                case "menu_cb_cash_transfer_received": return (new SMLERPCASHBANK._cb_cash_transfer_received());
                //_bankControl : cash transfer received cancle : ยกเลิกบันทึกโอนเงินเข้า
                case "menu_cb_cash_transfer_received_cancle": return (new SMLERPCASHBANK._cb_cash_transfer_received_cancle());
                //_bankControl : cash_transfer : บันทึกโอนเงินออก
                case "menu_cb_cash_transfer": return (new SMLERPCASHBANK._cb_cash_transfer());
                //_bankControl : cash_transfer_cancle : บันทึกยกเลิกโอนเงินออก
                case "menu_cb_cash_transfer_cancle": return (new SMLERPCASHBANK._cb_cash_transfer_cancle());

                // 7.2 เช็ครับ  
                //_chqListControl : in_receive : ทะเบียนเช็ครับ
                case "menu_cb_chq_in_receive_master": return (new SMLERPCASHBANK._cb_chq_in_receive_master());
                //_bankControl : in_receive : บันทึกเช็ครับ
                case "menu_cb_chq_in_receive": return (new SMLERPCASHBANK._cb_chq_in_receive());
                //_bankControl : in_payin : บันทึกนำฝากเช็ครับ
                //case "menu_cb_chq_in_payin": return (new SMLERPCASHBANK._cb_chq_in_payin());
                //_bankControl : in_payin_cancle : บันทึกยกเลิกนำฝากเช็ครับ
                //case "menu_cb_chq_in_payin_cancle": return (new SMLERPCASHBANK._cb_chq_in_payin_cancle());
                //_bankControl : in_return : บันทึกเช็ครับคืน
                //case "menu_cb_chq_in_return": return (new SMLERPCASHBANK._cb_chq_in_return());
                //_bankControl : in_return_cancle : บันทึกยกเลิกเช็ครับคืน
                case "menu_cb_chq_in_return_cancle": return (new SMLERPCASHBANK._cb_chq_in_return_cancle());
                //_bankControl : in_honor : บันทึกเช็คผ่าน
                case "menu_cb_chq_in_honor": return (new SMLERPCASHBANK._cb_chq_in_honor());
                //_bankControl : in_honor_cancle : บันทึกยกเลิกเช็คผ่าน
                case "menu_cb_chq_in_honor_cancle": return (new SMLERPCASHBANK._cb_chq_in_honor_cancle());
                //_bankControl : in_change : บันทึกเปลี่ยนเช็คนำฝาก
                case "menu_cb_chq_in_change": return (new SMLERPCASHBANK._cb_chq_in_change());
                //_bankControl : in_change_cancle : บันทึกยกเลิกเปลี่ยนเช็คนำฝาก
                case "menu_cb_chq_in_change_cancle": return (new SMLERPCASHBANK._cb_chq_in_change_cancle());
                //_bankControl : in new : บันทึกเช็คนำเข้าใหม่
                case "menu_cb_chq_in_new": return (new SMLERPCASHBANK._cb_chq_in_new());
                //_bankControl : in new cancle : บันทึกยกเลิกเช็คนำเข้าใหม่
                case "menu_cb_chq_in_new_cancle": return (new SMLERPCASHBANK._cb_chq_in_new_cancle());

                // 7.3 เช็คจ่าย 
                //_chqListControl : out_payment : ทะเบียนเช็คจ่าย
                case "menu_cb_chq_out_payment_master": return (new SMLERPCASHBANK._cb_chq_out_payment_master());
                //_bankControl : out_payment : บันทึกเช็คจ่าย
                case "menu_cb_chq_out_payment": return (new SMLERPCASHBANK._cb_chq_out_payment());
                //_bankControl : out_return : บันทึกเช็คจ่ายคืน
                //case "menu_cb_chq_out_return": return (new SMLERPCASHBANK._cb_chq_out_return()); toe เอาออกก่อนซ้ำ
                //_bankControl : out_return_cancle : บันทึกยกเลิกเช็คจ่ายคืน
                case "menu_cb_chq_out_return_cancle": return (new SMLERPCASHBANK._cb_chq_out_return_cancle());
                //_bankControl : out_honor : บันทึกเช็คจ่ายผ่าน
                case "menu_cb_chq_out_honor": return (new SMLERPCASHBANK._cb_chq_out_honor());
                //_bankControl : out_honor_cancle : บันทึกยกเลิกเช็คจ่ายผ่าน
                case "menu_cb_chq_out_honor_cancle": return (new SMLERPCASHBANK._cb_chq_out_honor_cancle());
                //_bankControl : out_change : บันทึกเปลี่ยนเช็คจ่าย
                case "menu_cb_chq_out_change": return (new SMLERPCASHBANK._cb_chq_out_change());
                //_bankControl : out_change_cancle : บันทึกยกเลิกเปลี่ยนเช็คจ่าย
                case "menu_cb_chq_out_change_cancle": return (new SMLERPCASHBANK._cb_chq_out_change_cancle());

                // 7.4 บัตรเครดิต
                //_creditMasterControl : credit_master : รายละเอียดรับบัตรเครดิต
                case "menu_cb_credit_card_receive": return (new SMLERPCASHBANK._cb_credit_master());
                //_creditMasterControl : credit_master : รายละเอียดจ่ายบัตรเครดิต
                case "menu_cb_credit_card_payment": return (new SMLERPCASHBANK._cb_credit_master_payment());
                //_bankControl : credit_money : ขึ้นเงินบัตรเครดิต
                case "menu_cb_credit_card_money": return (new SMLERPCASHBANK._cb_credit_card_money());
                //_bankControl : credit_cancel : ยกเลิกบัตรเครดิต
                //case "menu_cb_credit_card_cancel": return (new SMLERPCASHBANK._cb_credit_card_cancel());


                // 7.1 เงินสดย่อย
                //_pettyCashMasterControl : petty_cash : กำหนดวงเงินสดย่อย
                case "menu_petty_cash": return (new SMLERPCASHBANK._cb_petty_cash());
                //_bankControl : petty_receive : บันทึกรับเงินสดย่อย
                //case "menu_cb_petty_cash_receive": return (new SMLERPCASHBANK._cb_petty_cash_receive());
                //_bankControl : petty_receive_cancle : บันทึกยกเลิกรับเงินสดย่อย
                case "menu_cb_petty_cash_receive_cancle": return (new SMLERPCASHBANK._cb_petty_cash_receive_cancle());
                //_bankControl : petty_request : บันทึกขอเบิกเงินสดย่อย
                case "menu_cb_petty_cash_request": return (new SMLERPCASHBANK._cb_petty_cash_request());
                //_bankControl : petty_request_cancel : ยกเลิกขอเบิกเงินสดย่อย
                case "menu_cb_petty_cash_request_cancel": return (new SMLERPCASHBANK._cb_petty_cash_request_cancel());
                //_bankControl : petty cash return : บันทึกรับคืนเงินสดย่อย
                //case "menu_cb_petty_return": return (new SMLERPCASHBANK._cb_petty_return());
                //_bankControl : petty cash return cancel : ยกเลิกรับคืนเงินสดย่อย
                //case "menu_cb_petty_return_cancel": return (new SMLERPCASHBANK._cb_petty_return_cancel());


                //อื่นๆ ยังไม่ใช้
                //_bankControl : in_cancel : บันทึกยกเลิกเช็ครับ
                //case "menu_cb_chq_in_cancel": return (new SMLERPCASHBANK._cb_chq_in_cancel());
                //_bankControl : in_discount : บันทึกขายลดเช็ค
                //case "menu_cb_chq_in_discount": return (new SMLERPCASHBANK._cb_chq_in_discount());
                //_bankControl : out_cancel : บันทึกยกเลิกเช็คจ่าย
                //case "menu_cb_chq_out_cancel": return (new SMLERPCASHBANK._cb_chq_out_cancel());                
                //_bankControl : petty_payment : บันทึกจ่ายเงินสดย่อย
                //case "menu_cb_petty_cash_payment": return (new SMLERPCASHBANK._cb_petty_cash_payment());
                //_bankControl : petty_clear : ปิดเงินสดย่อย/จ่ายเงินล่วงหน้า
                //case "menu_cb_pety_cash_clear": return (new SMLERPCASHBANK._cb_petty_cash_clear());
                //_bankControl : petty_request_approve : อนุมัติใบขอเบิกเงินสดย่อย
                //case "menu_cb_petty_cash_request_approve": return (new SMLERPCASHBANK._cb_petty_cash_request_approve());
                //_bankControl : petty_request_approve_cancel : ยกเลิกอนุมัติขอเบิกเงินสดย่อย
                //case "menu_cb_petty_cash_requestapprove_cancel": return (new SMLERPCASHBANK._cb_petty_cash_requestapprove_cancel());
            }
            return null;
        }
    }
}
