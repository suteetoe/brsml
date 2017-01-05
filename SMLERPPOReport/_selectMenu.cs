using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPPOReport
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                case "menu_po_report_purchase_order": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานใบสั่งซื้อ, _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ, screenName));
                // เงินจ่ายล่วงหน้า
                case "menu_po_report_deposit_payment": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินล่วงหน้า, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า, screenName));
                case "menu_po_report_deposit_payment_return": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน, screenName));
                case "menu_po_report_deposit_payment_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก, screenName));
                case "menu_po_report_deposit_payment_return_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก, screenName));
                case "menu_po_report_deposit_payment_remain": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานยอดคงเหลือเงินจ่ายล่วงหน้า, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า, screenName));
                case "menu_po_report_cut_deposit_payment": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า, screenName));
                // เงินมัดจำจ่าย
                case "menu_po_report_advance_payment": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินมัดจำ, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ, screenName));
                case "menu_po_report_advance_payment_return": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน, screenName));
                case "menu_po_report_advance_payment_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก, screenName));
                case "menu_po_report_advance_payment_return_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก, screenName));
                case "menu_po_report_deposit_payment_2_remain": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานยอดคงเหลือเงินมัดจำจ่าย, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ, screenName));
                case "menu_po_report_cut_deposit_payment_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย, _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ, screenName));
                // Partial
                case "menu_po_report_purchase_partial": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_พาเชียล_รับสินค้า, _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า, screenName));
                case "menu_po_report_purchase_partial_debit": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด, _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด, screenName));
                case "menu_po_report_purchase_partial_1": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_พาเชียล_ตั้งหนี้, _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้, screenName));
                case "menu_po_report_purchase_partial_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_พาเชียล_เพิ่มหนี้, _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้, screenName));
                case "menu_po_report_purchase_partial_3": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_พาเชียล_ลดหนี้, _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้, screenName));
                // ซื้อสินค้าและบริการ 
                case "menu_po_report_purchase":
                case "menu_po_report_purchase_pos":
                    return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ซื้อสินค้าและบริการ, _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ, screenName));
                case "menu_po_report_add_goods": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด, _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด, screenName));
                case "menu_po_report_credit_note": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด, _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด, screenName));
                case "menu_po_purchase_billing_cancel_report": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก, screenName));
                case "menu_po_credit_note_cancel_report": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก, screenName));
                case "menu_po_addition_debt_cancel_report": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก, screenName));
                case "menu_po_hold_receive_by_doc": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ, _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ, screenName));
                //
                case "menu_po_recive_by_item": return (new SMLERPSOReport._report._report_sale_service_by_item(SMLERPReportTool._reportEnum.ซื้อ_รายงานซื้อสินค้าแยกตามสินค้า, screenName));

                case "menu_po_report_purchase_cancel":
                case "menu_po_report_purchase_cancel_pos":
                    return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก, screenName)); // (new SMLERPPOReport._report._report_purchase_cancel()); // รายงานการซื้อสินค้าหรือบริการ  005
                case "menu_po_report_add_goods_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก, screenName)); //(new SMLERPPOReport._report._report_add_goods_cancel()); // รายงานการเพิ่มหนี้/ราคาผิด/เพิ่มสินค้า  006
                case "menu_po_report_credit_note_cancel": return (new SMLERPPOReport._report._report_credit_note_cancel()); // รายงานการส่งคืนสินค้า/ลดหนี้  007

                //---รายงานสรุป
                case "menu_po_report_purchase_total": return (new SMLERPPOReport._report_summary._report_purchase_total()); // รายงานยอดซื้อ(ตามวันที่)  S001  
                case "menu_po_report_purchase_sum_by_tax": return (new SMLERPPOReport._report_summary._report_purchase_sum_by_tax()); // รายงานการซื้อสินค้าสรุปตามประเภทภาษี  S002
                case "menu_po_report_debt_from_purchase": return (new SMLERPPOReport._report_summary._report_debt_from_purchase()); // รายงานใบตั้งหนี้จากการซื้อแบบสรุป-ตามวันที่  S003
                case "menu_po_report_purchase_order_sum": return (new SMLERPPOReport._report_summary._report_purchase_order_sum()); // รายงานการสั่งซื้อสินค้าแบบสรุป-ตามวันที่  S004
                case "menu_po_report_add_goods_sum": return (new SMLERPPOReport._report_summary._report_add_goods_sum()); // รายงานใบเพิ่มสินค้า(เจ้าหนี้)แบบสรุป-ตามสินค้า S005
                case "menu_po_report_return_sum": return (new SMLERPPOReport._report_summary._report_return_sum()); // รายงานการส่งคืนสินค้า/ลดหนี้แบบสรุป-ตามสินค้า  S006
                case "menu_po_report_purchase_request_status": return (new SMLERPPOReport._report_summary._report_purchase_request_status()); // รายงานสถานะใบขอซื้อ  S007
                case "menu_po_report_purchase_order_status": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ, _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ, screenName)); // return (new SMLERPPOReport._report_summary._report_purchase_order_status()); // รายงานสถานะใบสั่งซื้อ  S008
                case "menu_po_report_purchase_order_due": return (new SMLERPPOReport._report_summary._report_purchase_order_due()); // รายงานใบซื้อสินค้าที่ถึงกำหนดจ่ายเงิน  S010
                //---รายงานวิเคราะห์
                case "menu_po_report_rank_purchase_total": return (new SMLERPPOReport._report_analyze._report_rank_purchase_total()); // รายงานการจัดอันดับยอดซื้อ(ตามสินค้า-กลุ่มสินค้า))  A001  
                case "menu_po_report_purchase_analyze": return (new SMLERPPOReport._report_analyze._report_purchase_analyze()); // รายงานวิเคราะห์การซื้อสุทธิ-ตามสินค้า  A002
                case "menu_po_report_compare_purchase_monthly": return (new SMLERPPOReport._report_analyze._report_compare_purchase_monthly()); // รายงานเปรียบเทียบยอดซื้อสินค้า12เดือน(ตามสินค้า-ราคา/ปริมาณ)  A003
            }
            return null;
        }

    }
}
