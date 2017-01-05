using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAP
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_ap_detail": return (new SMLERPAP._ap()); //AP : ข้อมูลเจ้าหนี้
                case "menu_ap_detail_2": return (new SMLERPAP._ap_detail()); //AP DETAIL : ข้อมูลเจ้าหนี้ (เพิ่มเติม)
                //case "menu_ap_picture": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, "ffffffffff", new SMLERPAP._addpat()); break;

                //case "menu_ap_reference": return (new SMLERPAP._addDataTest()); //AP REFERENCE : บันทึกเอกสารอ้างอิง
                //case "menu_ap_picture": return (new SMLERPAP.xxx()); //AP PICTURE : กำหนดรูปภาพ
                //-----------------------------------------------------------------------------------------------------------
                // ยกมา
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกตั้งเจ้าหนี้ยกมา
                case "menu_ap_debt_balance": return (new SMLERPAP._ap_debt_balance());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกเพิ่มหนี้ ยกมา
                case "menu_ap_increase_debt": return (new SMLERPAP._ap_increase_debt());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกลดหนี้ยกมา
                case "menu_ap_cn_balance": return (new SMLERPAP._ap_cn_balance());
                //-----------------------------------------------------------------------------------------------------------
                // อื่นๆๆ
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกตั้งเจ้าหนี้อื่นๆ
                case "menu_ap_debt_other": return (new SMLInventoryControl._clone_icTrans( _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น, menuName));
                case "menu_ap_cancel_debt_balance_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก, menuName));
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกลดหนี้อื่นๆ
                case "menu_ap_cn_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น, menuName));
                case "menu_ap_cancel_cn_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก, menuName));
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกเพิ่มหนี้อื่นๆ
                case "menu_ap_increase_debt_other": return (new SMLInventoryControl._clone_icTrans( _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น, menuName));
                case "menu_ap_cancel_increase_debt_other": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก, menuName));
                //-----------------------------------------------------------------------------------------------------------
                // จบs
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกใบรับวางบิล
                case "menu_ap_pay_bill": return (new SMLERPAP._ap_pay_bill(menuName));
                case "menu_ap_cancel_pay_bill": return (new SMLERPAP._ap_cancel_pay_bill());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกเตรียมจ่ายชำระหนี้
                case "menu_ap_debt_billing_temp": return (new SMLERPAP._ap_debt_billing_temp());
                case "menu_ap_cancel_debt_billing_temp": return (new SMLERPAP._ap_cancel_debt_billing_temp());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกจ่ายชำระหนี้
                case "menu_ap_debt_billing": return (new SMLERPAP._ap_debt_billing(menuName));
                case "menu_ap_cancel_debt_billing": return (new SMLERPAP._ap_cancel_debt_billing());
                //-----------------------------------------------------------------------------------------------------------
                // บันทึกตัดหนี้สูญ
                case "menu_ap_debt_billing_cut": return (new SMLERPAP._ap_debt_billing_cut());
                case "menu_ap_cancel_debt_billing_cut": return (new SMLERPAP._ap_cancel_debt_billing_cut()); 
                //บันทึกเงินล่วงหน้า
                //////////////case "menu_po_deposit_payment_1": return (new SMLERPAP._ap_advance_money());
                //ยกเลิกเงินล่วงหน้า
                ///////////////case "menu_ap_advance_money_cancel": return (new SMLERPAP._ap_advance_money_cancel()); 
                //รับคืนเงินล่วงหน้า
                ///////////////case "menu_po_deposit_return_1": return (new SMLERPAP._ap_advance_money_return());
                //------------------------------------------------------------------------------------------------------------
                //บันทึกเงินมัดจำ
                /////////////case "menu_po_deposit_payment_2": return (new SMLERPAP._ap_deposit_money());
                //ยกเลิกเงินมัดจำ
                ///////////////case "menu_ap_deposit_money_cancel": return (new SMLERPAP._ap_deposit_money_cancel()); 
                //รับคืนเงินมัดจำ 
                ///////////////case "menu_po_deposit_return_2": return (new SMLERPAP._ap_deposit_money_cancel()); 

                case "menu_ap_doc_picture": return (new SMLERPAP._ap_doc_picture());
            }
            return null;
        }
    }
}
