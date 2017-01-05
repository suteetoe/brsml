using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAR
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_ar_detail": return (new SMLERPAR._ar()); //AR : ข้อมูลลูกหนี้
                case "menu_ar_detail_2": return (new SMLERPAR._ar_detail()); //AR DETAIL : ข้อมูลลูกหนี้ (เพิ่มเติม)
                case "menu_ar_dealer": return (new SMLERPAR._ar_dealer()); //AR REFERENCE : รายละเอียดสมาชิก
                //case "menu_ar_reference": return (new SMLERPAR.xxx()); //AR REFERENCE : บันทึกเอกสารอ้างอิง
                //case "menu_ar_picture": return (new SMLERPAR.xxx()); //AR PICTURE : กำหนดรูปภาพ
                //case "menu_ar_credit_group": return (new SMLERPAR.xxx()); //AR CREDIT GROUP : กำหนดกลุ่มวงเงินลูกหนี้
                /*case "menu_ar_debt_balance": return (new SMLERPAR._ar_debt_balance()); //AR DEBT BALANCE : บันทึกตั้งลูกหนี้ยกมา
                case "menu_ar_cn_balance": return (new SMLERPAR._ar_cn_balance()); //AR CN BALANCE : บันทึกลดหนี้ยกมา
                case "menu_ar_pay_bill": return (new SMLERPAR._ar_pay_bill()); //AR PAY BILL : บันทึกใบวางบิล
                case "menu_ar_pay_bill_auto": return (new SMLERPAR._ar_pay_bill_auto()); //AR PAY BILL : บันs
                case "menu_ar_debt_billing_temp": return (new SMLERPAR._ar_debt_billing_temp()); //AR PAY BILL : ออกใบเสร็จรับเงินชั่วคราว
                case "menu_ar_debt_billing": return (new SMLERPAR._ar_debt_billing()); //AR DEBT BILLING : บันทึกรับชำระหนี้/ออกใบเสร็จรับเงิน
                case "menu_ar_debt_billing_cut": return (new SMLERPAR._ar_debt_billing_cut()); //AR DEBT BILLING CUT : บันทึกตัดหนี้สูญ*/

                //-----------------------------------------------------------------------------------------------------------
                // ยกมา
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกตั้งเจ้าหนี้ยกมา
                case "menu_ar_debt_balance": return (new SMLERPAR._ar_debt_balance());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกเพิ่มหนี้
                case "menu_ar_increase_debt": return (new SMLERPAR._ar_increase_debt());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกลดหนี้ยกมา
                case "menu_ar_cn_balance": return (new SMLERPAR._ar_cn_balance());
                //-----------------------------------------------------------------------------------------------------------
                // อื่นๆๆ
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกตั้งหนี้อื่น
                case "menu_ar_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น, menuName));
                case "menu_ar_cancel_debt_balance_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก, menuName));
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกลดหนี้อื่น
                case "menu_ar_cn_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น, menuName));
                case "menu_ar_cancel_cn_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก, menuName));
                // บันทึกเพิ่มหนี้อื่น
                case "menu_ar_increase_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น, menuName));
                case "menu_ar_cancel_increase_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก, menuName));
                //-----------------------------------------------------------------------------------------------------------
                // จบs
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกใบรับวางบิล
                case "menu_ar_pay_bill": return (new SMLERPAR._ar_pay_bill(menuName));
                case "menu_ar_cancel_pay_bill": return (new SMLERPAR._ar_cancel_pay_bill());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกเตรียมจ่ายชำระหนี้
                case "menu_ar_debt_billing_temp": return (new SMLERPAR._ar_debt_billing_temp());
                case "menu_ar_cancel_debt_billing_temp": return (new SMLERPAR._ar_cancel_debt_billing_temp());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกจ่ายชำระหนี้
                case "menu_ar_debt_billing": return (new SMLERPAR._ar_debt_billing(menuName));
                case "menu_ar_cancel_debt_billing": return (new SMLERPAR._ar_cancel_debt_billing());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกตัดหนี้สูญ
                case "menu_ar_debt_billing_cut": return (new SMLERPAR._ar_debt_billing_cut());
                case "menu_ar_cancel_debt_billing_cut": return (new SMLERPAR._ar_cancel_debt_billing_cut());
                //บันทึกเงินล่วงหน้า
                //////////////case "menu_so_deposit_receive_1": return (new SMLERPAR._ar_advance_money());
                //ยกเลิกเงินล่วงหน้า
                //////////////case "menu_ar_advance_money_cancel": return (new SMLERPAR._ar_advance_money_cancel());
                //รับคืนเงินล่วงหน้า
                //////////////case "menu_so_deposit_return_1": return (new SMLERPAR._ar_advance_money_cancel());
                //------------------------------------------------------------------------------------------------------------
                //บันทึกเงินมัดจำ
                ////////////case "menu_so_deposit_receive_2": return (new SMLERPAR._ar_deposit_money());
                //ยกเลิกเงินมัดจำ
                //////////////case "menu_ar_deposit_money_cancel": return (new SMLERPAR._ar_deposit_money_cancel()); 
                //รับคืนเงินมัดจำ
                //////////////case "menu_so_deposit_return_2": return (new SMLERPAR._ar_deposit_money_cancel()); 

                // toe
                case "menu_ar_label_print": return (new SMLERPAR._ar_label_print());
                case "menu_ar_point_balance": return (new SMLERPAR._ar_point_balance()); // แต้มคงเหลือยกมา
                case "menu_ar_cut_point": return (new SMLERPAR._ar_cut_point()); // ตัดแต้ม

                case "menu_ar_point": return (new SMLERPAR.ar_point());
                case "menu_ar_point_recal": return (new SMLERPAR.ar_point_recal());

                case "menu_ar_doc_picture": return (new SMLERPAR._ar_doc_picture());

                case "menu_imex_bill_collector": return (new SMLERPAR._ar_bill_collector());
                    
            }
            return null;
        }
    }
}
