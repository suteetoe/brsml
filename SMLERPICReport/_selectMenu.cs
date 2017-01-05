using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport
{
    public class _selectMenu
    {
        public static Control _getObject(string menuCode, string screenName)
        {
            switch (menuCode.ToLower())
            {

                ////////  รายงาน  master

                case "menu_ic_detail": return (new SMLERPICReport._report_ic_master(screenName)); // รายงานรายละเอียดสินค้า  2.14.1 account erp color
                case "menu_ic_by_license": return (new _report_ic_summary(SMLERPReportTool._reportEnum.Item_by_serial, screenName)); // return (new SMLERPICReport._report_ic_by_serial()); // รายละเอียดสินค้าแบบมี Serial  2.14.2 account erp โต๋ย้าย ไป report_ic_summary
                case "menu_item_status": return (new SMLERPICReport._report_ic_summary(SMLERPReportTool._reportEnum.Item_status, screenName));  //return (new SMLERPICReport._report_ic_status()); // รายงานสถานะสินค้า  2.14.3  account erp toe ย้ายไป report_ic_summary

                case "menu_ic_report_item_barcode": return (new SMLERPICReport._report_ic_barcode(screenName)); // รายงานบาร์โค้ดสินค้า  color
                case "menu_ic_report_item_color_mixing": return (new SMLERPICReport._report_ic_color_mixing(screenName)); // รายงานสูตรผสมสี   color
                case "menu_ic_report_item_set_formula": return (new SMLERPICReport._report_item_set_formula(screenName)); // รายงานสิ้นค้าชุด   color
                case "menu_ic_report_item_sale_price": return (new SMLERPICReport._report_ic_sale_price(screenName, 0)); // รายงานราคาขายสินค้า   color
                case "menu_ic_report_item_sale_price_normal": return (new SMLERPICReport._report_ic_sale_price(screenName, 1));
                case "menu_ic_report_item_purchase_price": return (new SMLERPICReport._report_ic_purchase_price(screenName)); // รายงานราคาซื้อสินค้า   color
                    // รายงานของแถมซื้อ
                case "menu_ic_report_item_giveaway": return (new _report_ic_summary(SMLERPReportTool._reportEnum.Item_Giveaway, screenName)); // return (new SMLERPICReport._report_ic_giveaway()); // รายงานของแถม   color

                case "menu_ic_report_goods_balance": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา, _g.g._transControlTypeEnum.สินค้า_ยอดคงเหลือสินค้ายกมา, screenName));  // รายงานรายการสินค้าและวัตถุดิบ  คงเหลือยกมา   2.14.4  account erp color
                case "menu_wh_report_item_count": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าตรวจนับ, _g.g._transControlTypeEnum.คลัง_ตรวจนับสินค้า, screenName));  // รายงานตรวจนับ 
                case "menu_ic_report_item_count": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานสินค้าตรวจนับ, _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า, screenName));  // รายงานตรวจนับ
                case "menu_import_item_ready_by_date": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป, _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป, screenName));  // รายงานการรับสินค้าสำเร็จรูป-วันที่  2.14.5  account erp color 

                case "menu_ic_report_item_transfer": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ, _g.g._transControlTypeEnum.สินค้า_โอนออก, screenName)); // รายงานโอนสินค้าและวัตถุดิบ
                case "menu_ic_report_item_withdraw": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ, _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ, screenName)); // รายงานการเบิกสินค้า , วัตถุดิบ
                case "menu_ic_report_item_return": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ, _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, screenName)); // รายงานการรับคืนเบิกสินค้า 
                case "menu_ic_report_item_adjust_increase": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน, _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก, screenName)); // รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)

                // toe 
                case "menu_ic_report_item_adjust_decrease": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด, _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด, screenName)); // รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด) 

                // รายงานยกเลิก รับสินค้าสำเร็จรูป 
                case "menu_ic_report_cancel_goods_pack": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป_ยกเลิก, _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก, screenName));
                // รายงานยกเลิกเบิกสินค้า/วัตถุดิบ
                case "menu_ic_report_cancel_raw_material": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ_ยกเลิก, _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก, screenName)); ;
                // รายงานยกเลิก รับคืนสินค้า/วัตถุดิบ จากการเบิก
                case "menu_ic_report_cancel_goods_raw_meterial": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ_ยกเลิก, _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก, screenName)); ;

                // รายงานยกเลิกโอนสินค้า วัตถุดิบ
                case "menu_ic_report_stk_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ_ยกเลิก, _g.g._transControlTypeEnum.สินค้า_โอนออก_ยกเลิก, screenName));

                // ยกเลิกปรับปรุง สินค้าวัตถุดิบ (เกิน)
                case "menu_ic_report_stk_cancel_over": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป_ยกเลิก, _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก, screenName));

                //  ยกเลิกปรับปรุง สินค้าวัตถุดิบ (ขาด)
                case "menu_ic_report_stk_cancel_lost": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป_ยกเลิก, _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต๊อก_ขาด_ยกเลิก, screenName));

                case "menu_ic_report_item_balance_cancel": return (new SMLERPICReport._report_ic_cancel_item_and_staple());  // รายงานรายการยกเลิกสินค้าและวัตถุดิบ  คงเหลือยกมา   2.14.4  account erp color
                case "menu_ic_report_item_withdraw_cancel": return (new SMLERPICReport._report_ic_cancel_withdraw_item_and_staple()); // รายงานการยกเลิกเบิกสินค้า , วัตถุดิบ
                case "menu_ic_report_item_receive_cancel": return (new SMLERPICReport._report_ic_cancel_import_item_ready()); // รายงานการยกเลิกรับสินค้าสำเร็จรูป-วันที่  2.14.5  account erp color 
                case "menu_ic_report_item_transfer_cancel": return (new SMLERPICReport._report_ic_cancel_transfer_item_and_meterial()); // รายงานยกเลิกโอนสินค้าและวัตถุดิบ
                case "menu_ic_report_item_return_cancel": return (new SMLERPICReport._report_ic_cancel_refunded_withdraw_item_and_staple()); // รายงานยกเลิกรับคืนสินค้าและวัตถุดิบ  จากการเบิก
                case "menu_ic_report_item_adjust_increase_cancel": return (new SMLERPICReport._report_ic_cancel_implement_item_and_staple_over()); // รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)
                case "menu_ic_report_item_adjust_decrease_cancel": return (new SMLERPICReport._report_ic_cancel_implement_item_and_staple_minus()); // รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด)

                //////  รายงานสรุป ( วิเตราะห์ )  ครบ

                case "menu_diff_from_count": return (new SMLERPICReport._report_ic_summary(SMLERPReportTool._reportEnum.รายงานผลต่างจากการตรวจนับ, screenName));// รายงานผลต่างจากการตรวจนับ   2.15.12 account erp
                case "menu_report_serial_check": return (new SMLERPICReport._report_ic_summary(SMLERPReportTool._reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial, screenName));

                // toe case "menu_diff_from_count": return (new SMLERPICReport._report_diff_from_count()); // รายงานผลต่างจากการตรวจนับ   2.15.12 account erp
                case "menu_result_transfer_item": return (new SMLERPICReport._report_ic_info_stk_movement_sum(0, screenName)); // รายงานเคลื่อนไหวสินค้าตามปริมาณ   
                case "menu_result_transfer_item_by_amount": return (new SMLERPICReport._report_ic_info_stk_movement_sum(1, screenName)); // รายงานเคลื่อนไหวสินค้าตามปริมาณ   
                case "menu_item_transfer": return (new SMLERPICReport._report_ic_stk_movement(SMLERPReportTool._reportEnum.สินค้า_รายงานเคลื่อนไหว, screenName)); // รายงานเคลื่อนไหวสินค้า   
                case "menu_item_transfer_standard": return (new SMLERPICReport._report_ic_transfer_standard()); // รายงานเคลื่อนไหวสินค้าต้นทุนมาตรฐาน   2.15.10 account erp
                case "menu_account_special_item": return (new SMLERPICReport._report_ic_stk_movement(SMLERPReportTool._reportEnum.สินค้า_รายงานบัญชีคุมพิเศษ, screenName)); // รายงานบัญชีคุมพิฌศษสินค้า   2.15.11 account erp
                //case "menu_result_transfer_item": return (new SMLERPICReport._report_ic_result_transfer()); // รายงานสรุปเคลื่อนไหวปริมาณสินค้า   2.15.8 account erp
                case "menu_item_non_transfer": return (new SMLERPICReport._report_ic_none_movement(screenName)); // รายงานสินค้าที่ไม่มีการเคลื่อนไหว   2.15.7  account erp
                case "menu_item_balance": return (new SMLERPICReport._report_ic_balance(SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า, screenName)); // รายงานยอดคงเหลือสินค้า    2.15.4  account erp
                case "menu_item_balance_hightest": return (new SMLERPICReport._report_ic_balance_hightest()); // รายงานยอดคงเหลือที่ถึงจุดสูงสุด    2.15.5 account erp
                //case "menu_ic_serial_number": return (new SMLERPICReport._report_ic_serial_number(screenName)); // รายงานเคลื่อนไหว Serial Number   2.15.1  account erp
                case "menu_ic_serial_number": return (new SMLERPICReport._report_ic_summary(SMLERPReportTool._reportEnum.Serial_number, screenName));
                case "menu_result_item_export": return (new SMLERPICReport._report_result_item_export()); // รายงานสรุปยอดสินค้าค้างส่ง  2.15.2  account erp
                case "menu_result_item_import": return (new SMLERPICReport._report_result_item_import()); // รายงานสรุปยอดสินค้าค้างรับ   2.15.3  account erp             
                // TOE //case "menu_item_balance_now_only_serial": return (new SMLERPICReport._report_ic_Balance_now_Only_Serial());  // รายงานยอดคงเหลือสินค้า ณ ปัจจุบัน(เฉพาะสินค้ามี Serial)  2.15.6  account erp
                case "menu_item_balance_now_only_serial": return (new SMLERPICReport._report_ic_summary(SMLERPReportTool._reportEnum.Item_Balance_now_Only_Serial, screenName));  // รายงานยอดคงเหลือสินค้า ณ ปัจจุบัน(เฉพาะสินค้ามี Serial)  2.15.6  account erp

                case "menu_ic_report_info_stk_profit_by_doc_and_discount": return (new SMLERPICReport.ic_analysis._report_info_stk_profit_by_doc_and_discount(screenName, SMLERPReportTool._reportEnum.สินค้า_รายงานกำไรขั้นต้นตามเอกสารแบบมีส่วนลด)); // รายงานกำไรขั้นต้อนตามเอกสารแบบมีส่วนลด 
                case "menu_ic_report_info_stk_profit": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_ic_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_ic_cust": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_ic_cust_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_doc_ic": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_cust": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_cust_ic": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_cust_ic_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_cust_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_stk_profit_cust_doc_ic": return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า)); // รายงานกำไรขั้นต้น

                case "menu_ic_report_info_color_profit": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_ic_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_ic_cust": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_ic_cust_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_doc_ic": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_cust": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_cust_ic": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_cust_ic_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_cust_doc": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร)); // รายงานกำไรขั้นต้น
                case "menu_ic_report_info_color_profit_cust_doc_ic": return (new SMLERPICReport.ic_analysis._ic_report_info_color_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า)); // รายงานกำไรขั้นต้น

                case "menu_ic_report_info_stk_reorder": return (new SMLERPICReport.ic_analysis._report_ic_stk_reorder(SMLERPReportTool._reportEnum.สินค้า_ถึงจุดสั่งซื้อ_ตามสินค้า, screenName)); // รายงานสินค้าที่ถึงจุดสั่งซื้อ  smlcolor store

                /////////  ERP
                case "menu_ic_by_supplier": return (new SMLERPICReport._report_ic_by_supplier()); // รายละเอียดสินค้าตามเจ้าหนี้  erp             



                //case "menu_print_document_for_count_by_item": return (new SMLERPReport.ic._report_print_document_for_count_by_item()); // รายงานพิมพ์เอกสารเพื่อตวรจนับ-ตามสินค้า                
                //case "menu_implement_item": return (new SMLERPReport.ic._report_implement_Item()); // รายงานการปรับปรุงสินค้า
                //case "menu_span_import_item": return (new SMLERPReport.ic._report_span_import_item()); // รายงานประเมินการรับสินค้า  
                //case "menu_lot_item": return (new SMLERPReport.ic._report_lot_item()); // รายงานประเมินการรับสินค้า  
                //case "menu_item_and_staple": return (new SMLERPReport.ic._report_item_and_staple());
                //case "menu_print_document_for_count_by_warehouse": return (new SMLERPReport.ic._report_print_document_for_count_by_warehouse()); // พิมพ์เอกสารเพื่อตรวจนับ-ตามคลัง

                //case "menu_receptance_widen_by_date": return (new SMLERPReport.ic._report_receptance_widen_by_date()); // รายงานการรับคืนเบิก-วันที่

                //case "menu_expose_item_price": return (new SMLERPReport.ic._report_expose_item_price()); // รายงานการกำหนดราคาสินค้า
                //case "menu_transfertem_between_warehouse_by_output": return (new SMLERPReport.ic._report_transfer_item_between_warehouse_by_output()); // รายงานโอนสินค้าระหว่างคลัง-ตามคลังโอนออก
                //case "menu_transfer_item_between_and_detail": return (new SMLERPReport.ic._report_transfer_item_between_and_detail()); // รายงานโอนสินค้าระหว่างคลังและรายการย่อย
                //case "menu_import_stock_item": return (new SMLERPReport.ic._report_import_stock_item()); // รายงานการับสต๊อกสินค้า  
                //case "menu_record_total_item_first_year": return (new SMLERPReport.ic._report_ic_Record_Total_Item_First_Year());  // รายงานการบันทึกยอดสินค้ายกมาต้นปี   

                case "menu_ic_report_calc_color_by_sale": return new _report_ic_calc_color_by_sale(screenName, SMLERPReportTool._reportEnum.สินค้า_รายงานการใช้แม่สี);
                case "menu_ic_color_movement": return new _report_ic_color_movement();

                // toe
                case "menu_stock_no_balance_from_count": return (new _report_ic_summary(SMLERPReportTool._reportEnum.Stock_no_count_no_balance, screenName));

                // toe color store
                case "menu_salereport_suggest_customer_group": return (new _report_ic_summary(SMLERPReportTool._reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า, screenName));
                //return (new SMLERPICReport.ic_analysis._ic_report_info_stk_profit(screenName, SMLERPICInfo._infoStkProfitEnum.กำไรขั้นต้น_กลุ่มลูกค้า)); // รายงานกำไรขั้นต้นแบ่งตามกลุ่มลูกค้า
                case "menu_item_balance_lot": return (new _report_ic_balance(SMLERPReportTool._reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า_Lot, screenName));

                case "menu_ic_report_sort": return (new _icReportSort());
            }
            return null;
        }
    }
}
