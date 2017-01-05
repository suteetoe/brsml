using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSOReport
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                // เสนอราคา
                case "menu_so_report_quotation": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_เสนอราคา, _g.g._transControlTypeEnum.ขาย_เสนอราคา, screenName));
                case "menu_so_report_quotation_approve": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_เสนอราคา_อนุมัติ, _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ, screenName));
                case "menu_so_report_quotation_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_เสนอราคา_ยกเลิก, _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก, screenName));
                case "menu_so_report_quotation_stat": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_เสนอราคา_สถานะ, _g.g._transControlTypeEnum.ขาย_เสนอราคา, screenName));
                // สั่งซื้อ/สั่งจอง
                case "menu_so_report_inquiry": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า, _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า, screenName));
                case "menu_so_report_inquiry_approve": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ, _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ, screenName));
                case "menu_so_report_inquiry_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก, _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก, screenName));
                case "menu_so_report_inquiry_stat": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ, _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า, screenName));
                // สั่งขาย
                case "menu_so_report_sale_order": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งขาย, _g.g._transControlTypeEnum.ขาย_สั่งขาย, screenName));
                case "menu_so_report_sale_order_approve": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งขาย_อนุมัติ, _g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ, screenName));
                case "menu_so_report_sale_order_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งขาย_ยกเลิก, _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก, screenName));
                case "menu_so_report_sale_order_stat": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_สั่งขาย_สถานะ, _g.g._transControlTypeEnum.ขาย_สั่งขาย, screenName));
                // เงินรับล่วงหน้า
                case "menu_so_report_deposit_receive": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินล่วงหน้า, _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า, screenName));
                case "menu_so_report_deposit_return": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินล่วงหน้า_คืน, _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน, screenName));
                case "menu_so_report_deposit_payment_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก, _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก, screenName));
                case "menu_so_report_deposit_payment_return_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินล่วงหน้า_คืน_ยกเลิก, _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก, screenName));
                case "menu_so_report_deposit_payment_remain": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินล่วงหน้า_รายงานยอดคงเหลือ, _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า, screenName));
                case "menu_so_report_cut_deposit_payment": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน, _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า, screenName));
                // เงินรับมัดจำ
                case "menu_so_report_deposit_receive_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินมัดจำ, _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ, screenName));
                case "menu_so_report_deposit_return_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินมัดจำ_คืน, _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน, screenName));
                case "menu_so_report_deposit_payment_cancel_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินมัดจำ_ยกเลิก, _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก, screenName));
                case "menu_so_report_deposit_payment_return_cancel_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินมัดจำ_คืน_ยกเลิก, _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก, screenName));
                case "menu_so_report_deposit_payment_remain_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ, _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ, screenName));
                case "menu_so_report_cut_deposit_payment_2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน, _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ, screenName));
                // ขายสินค้าและบริการ
                case "menu_so_report_invoice": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_ขายสินค้าและบริการ, _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, screenName));
                case "menu_so_report_invoice_credit_note_add_goods": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_ขายสินค้าและบริการ_ลดหนี้_เพิ่มหนี้, _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, screenName));
                case "menu_so_report_add_goods": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด, _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้, screenName));
                case "menu_so_report_credit_note":
                case "menu_so_report_credit_note_pos":
                    return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับคืนสินค้า, _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้, screenName));
                case "menu_so_report_invoice_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก, _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก, screenName));
                case "menu_so_report_add_goods_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก, _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก, screenName));
                case "menu_so_report_credit_note_cancel":
                case "menu_so_report_credit_note_cancel_pos":
                    return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_รับคืนสินค้า_ยกเลิก, _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก, screenName));
                // toe
                case "menu_so_report_pos_full_invoice": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม, _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน, screenName));
                //
                case "menu_so_sale_service_by_item": return (new SMLERPSOReport._report._report_sale_service_by_item(SMLERPReportTool._reportEnum.ขาย_รายงานขายสินค้าแยกตามสินค้า, screenName));
                //------------รายงายสรุป----------
                case "menu_sell_by_sale": return (new SMLERPSOReport._report_summary._report_sell_by_sale(SMLERPReportTool._reportEnum.สินค้า_sell_by_sale, screenName)); // รายงานสรุปการขายสินค้าตามพนักงานขาย  S-001

                // case "menu_sell_by_sale": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_sell_by_sale, _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, screenName)); // toe เพิ่ม
                case "menu_shipments_by_date": return (new SMLERPSOReport._report_summary._report_shipments_by_date()); // รายงานสรุปยอดขายสินค้า-สรุปตามวันที่  S-002
                case "menu_sale_history_product": return (new SMLERPSOReport._report_summary._report_sale_history_product()); // รายงานประวัติการขายสินค้า S-003
                //------------รายงานวิเคราะห์------
                case "menu_analysis_sell_ex_by_product": return (new SMLERPSOReport._report_analyze._report_analysis_sell_ex_by_product()); // รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า  A-001
                case "menu_analysis_sell_sum_by_docno": return (new SMLERPSOReport._report_analyze._report_analysis_sell_sum_by_docno()); // รายงานวิเคราะห์การขายแบบสรุป-ตามเอกสาร  A-002
                case "menu_margin_reacceptance": return (new SMLERPSOReport._report_analyze._report_margin_reacceptance()); // รายงานกำไรขั้นต้นแสดงหักรับคืน A-003
                case "menu_product_rank": return (new SMLERPSOReport._report_analyze._report_product_rank()); // รายงานจัดอันดับยอด-สินค้า  A-004
                case "menu_shipments_compare_month": return (new SMLERPSOReport._report_analyze._report_shipments_compare_month()); // รายงานเปรียบเที่ยบยอดสินค้า12เดือน A-005

                // toe pos
                case "menu_report_pos_sale_sugest_by_item_and_serial": return (new SMLERPReportTool._reportPosAnalyze(SMLERPReportTool._reportEnum.สินค้า_pos_sale_sugest_by_item_and_serial, _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, screenName));
                case "menu_so_report_sale_drug_by_lot": return new _report_drug_sale_8();

                case "menu_so_report_sale_drug_13": return new _report_sale_drug_13();
            }
            return null;
        }
    }
}
