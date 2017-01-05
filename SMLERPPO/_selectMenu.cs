using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPPO
{
    public class _selectMenu
    {
        public static Control _getObject(string menuCode, string menuName)
        {
            switch (menuCode.ToLower())
            {
                case "menu_purchase_requisition": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ, menuName)); // บันทึกใบเสนอซื้อสินค้า
                case "menu_cancel_purchase_requisition": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก, menuName)); // ยกเลิกใบเสนอซื้อสินค้า
                case "menu_purchase_requisition_approval": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ, menuName)); // บันทึกอนุมัติใบเสนอซื้อสินค้า
                case "menu_po_purchase_order": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ, menuName));// บันทึกใบสั่งซื้อสินค้า
                case "menu_po_purchase_order_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก, menuName));// ยกเลิกใบสั่งซื้อสินค้า
                case "menu_po_purchase_order_approve": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ, menuName)); // อนุมัติใบสั่งซื้อสินค้า
                //
                case "menu_po_purchase_billing": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ, menuName)); // บันทึกซื้อสินค้า
                case "menu_po_purchase_billing_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก, menuName)); // ยกเลิกบันทึกซื้อสินค้า
                //
                case "menu_po_addition_debt": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด, menuName)); // ซื้อสินค้าเพิ่ม/เพิ่มหนี้
                case "menu_po_addition_debt_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก, menuName)); // ยกเลิกซื้อสินค้าเพิ่ม/เพิ่มหนี้
                //
                case "menu_po_credit_note": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด, menuName));// บันทึกส่งคืนสินค้า/ลดหนี้
                case "menu_po_credit_note_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก, menuName));// ยกเลิกบันทึกส่งคืนสินค้า/ลดหนี้
                //
                case "menu_po_deposit_payment_1": return (new SMLERPPO._po_advance_money(menuName)); // บันทึกเงินล่วงหน้า
                case "menu_po_deposit_payment_1_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก, menuName)); ; // ยกเลิกบันทึกเงินล่วงหน้า
                //
                case "menu_po_deposit_return_1": return (new SMLERPPO._po_advance_money_return(menuName)); // รับคืนเงินล่วงหน้า
                case "menu_po_deposit_return_1_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก, menuName)); // ยกเลิกรับคืนเงินล่วงหน้า
                //
                case "menu_po_deposit_payment_2": return (new SMLERPPO._po_deposit_money(menuName)); // บันทึกเงินมัดจำ
                case "menu_po_deposit_payment_2_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก, menuName)); // ยกเลิกบันทึกเงินมัดจำ
                case "menu_po_deposit_return_2": return (new SMLERPPO._po_deposit_money_return(menuName)); //รับคืนเงินมัดจำ
                case "menu_po_deposit_return_2_cancel": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก, menuName)); // ยกเลิกรับคืนเงินมัดจำ
                //
                case "menu_po_purchase_partial_item": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า, menuName)); // รับสินค้าแบบพาเชียล
                case "menu_po_purchase_partial_item_debit": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด, menuName)); // ปรับปรุงราคาผิดแบบพาเชียล
                case "menu_po_purchase_partial_1": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้, menuName)); // ตั้งหนี้จากการรับสินค้า
                case "menu_po_purchase_partial_2": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้, menuName));// บันทึกส่งคืนสินค้า/ลดหนี้
                case "menu_po_purchase_partial_3": return (new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้, menuName));// บันทึกส่งคืนสินค้า/ลดหนี้

                case "menu_po_doc_picture": return (new _po_docPicture());
                case "menu_buy_information": return (new _buyInformation());
            }
            return null;
        }
    }
}
