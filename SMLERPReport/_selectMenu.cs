using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport
{
    public static class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                case "menu_documents_early_year_other": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_ตั้งหนี้อื่น, _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น, screenName);  // ตั้งหนี้อื่นๆ
                case "menu_increase_debt_other": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_เพิ่มหนี้อื่น, _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น, screenName);  // เพิ่มหนี้อื่นๆ
                case "menu_reduction_dept_other": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_ลดหนี้อื่น, _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น, screenName);  // ลดหนี้อื่นๆ
                //เช็ค / บัตร  
                case "menu_report_chq_card_enddate_card": return (new SMLERPReport.Cheque_Card._report_chq_card_EndDate_Card()); // รายงานรายละเอียดบัตรเครดิต - ครบกำหนด
                case "menu_report_chq_card_enddate_pay_cheque": return (new SMLERPReport.Cheque_Card._report_chq_card_EndDate_Pay_Cheque()); // รายงานรายละเอียดเช็คจ่าย - วันครบกำหนด
                case "menu_report_ch_card_cash_money_detail": return (new SMLERPReport.Cheque_Card._report_ch_card_Cash_Money_Detail()); // รายงานรายรายวันขึ้นเงินพร้อมรายการย่อย
                case "menu_report_chq_card_cancel_cheque_detail": return (new SMLERPReport.Cheque_Card._report_chq_card_Cancel_Cheque_Detail()); // รายงานการยกเลิกเช็คจ่ายพร้อยรายการย่อย
                case "menu_report_chq_card_cancel_card_detail": return (new SMLERPReport.Cheque_Card._report_chq_card_Cancel_Card_Detail()); // รายงานการยกเลิกบัตรพร้อมรายการย่อย
                case "menu_report_chq_card_disposit_cheque_detail": return (new SMLERPReport.Cheque_Card._report_chq_card_Disposit_Cheque_Detail()); // รายงานใบนำฝากเช็ครับพร้อมรายการย่อย
                case "menu_report_chq_card_detail_cheque_by_date_import": return (new SMLERPReport.Cheque_Card._report_chq_card_Detail_Cheque_by_Date_Import()); // รายงานรายละเอียดเช็ครับ - ตามวันที่รับ              

                case "menu_ar_report_cancel_pay_billing": return (new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.ลูกหนี้_รับวางบิล_ยกเลิก, _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก, screenName));// รายงานยกเลิกใบวางบิล(ลูกหนี้)
                case "menu_ar_report_cancel_invoice": return (new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก, _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก, screenName)); // 

                //สินค้า
                //case "menu_ic_detail": return (new SMLERPReport.ic._report_ic_master()); // รายงานรายละเอียดสินค้า
                //case "menu_ic_by_supplier": return (new SMLERPReport.ic._report_ic_by_supplier()); // รายละเอียดสินค้าตามเจ้าหนี้
                //case "menu_ic_by_license": return (new SMLERPReport.ic._report_ic_by_license()); // รายละเอียดสินค้าแบบมี Serial
                //case "menu_ic_serial_number": return (new SMLERPReport.ic._report_ic_serial_number()); // รายงานเคลื่อนไหว Serial Number
                //case "menu_item_status": return (new SMLERPReport.ic._report_item_status()); // รายงานสถานะสินค้า
                //case "menu_result_item_export": return (new SMLERPReport.ic._report_result_item_export()); // รายงานสรุปยอดสินค้าค้างส่ง
                //case "menu_result_item_import": return (new SMLERPReport.ic._report_result_item_import()); // รายงานสรุปยอดสินค้าค้างรับ
                //case "menu_item_balance": return (new SMLERPReport.ic._report_item_balance()); // รายงานยอดคงเหลือสินคึ้า
                //case "menu_item_balance_hightest": return (new SMLERPReport.ic._report_item_balance_hightest()); // รายงานยอดคงเหลือที่ถึงจุดสูงสุด
                //case "menu_item_non_transfer": return (new SMLERPReport.ic._report_item_non_transfer()); // รายงานสินค้าที่ไม่มีการเคลื่อนไหว
                //case "menu_result_transfer_item": return (new SMLERPReport.ic._report_result_transfer_item()); // รายงานสรุปเคลื่อนไหวปริมาณสินค้า
                //case "menu_item_transfer": return (new SMLERPReport.ic._report_item_transfer()); // รายงานเคลื่อนไหวสินค้า
                //case "menu_item_transfer_standard": return (new SMLERPReport.ic._report_item_transfer_standard()); // รายงานเคลื่อนไหวสินค้าต้นทุนมาตรฐาน
                //case "menu_account_special_item": return (new SMLERPReport.ic._report_account_Special_Item()); // รายงานบัญชีคุมพิฌศษสินค้า
                //case "menu_diff_from_count": return (new SMLERPReport.ic._report_diff_from_count()); // รายงานผลต่างจากการตรวจนับ
                //case "menu_print_document_for_count_by_item": return (new SMLERPReport.ic._report_print_document_for_count_by_item()); // รายงานพิมพ์เอกสารเพื่อตวรจนับ-ตามสินค้า                
                //case "menu_implement_item": return (new SMLERPReport.ic._report_implement_Item()); // รายงานการปรับปรุงสินค้า
                //case "menu_span_import_item": return (new SMLERPReport.ic._report_span_import_item()); // รายงานประเมินการรับสินค้า  
                //case "menu_lot_item": return (new SMLERPReport.ic._report_lot_item()); // รายงานประเมินการรับสินค้า  
                //case "menu_item_and_staple": return (new SMLERPReport.ic._report_item_and_staple());
                //case "menu_print_document_for_count_by_warehouse": return (new SMLERPReport.ic._report_print_document_for_count_by_warehouse()); // พิมพ์เอกสารเพื่อตรวจนับ-ตามคลัง
                //case "menu_import_item_ready_by_date": return (new SMLERPReport.ic._report_import_item_ready_by_date()); // รายงานการรับสินค้าสำเร็จรูป-วันที่
                //case "menu_receptance_widen_by_date": return (new SMLERPReport.ic._report_receptance_widen_by_date()); // รายงานการรับคืนเบิก-วันที่
                //case "menu_widen_item_staple_date": return (new SMLERPReport.ic._report_widen_item_staple_date()); // รายงานการเบิกสินค้า , วัตถุดิบ-วันที่เบิก
                //case "menu_expose_item_price": return (new SMLERPReport.ic._report_expose_item_price()); // รายงานการกำหนดราคาสินค้า
                //case "menu_transfertem_between_warehouse_by_output": return (new SMLERPReport.ic._report_transfer_item_between_warehouse_by_output()); // รายงานโอนสินค้าระหว่างคลัง-ตามคลังโอนออก
                //case "menu_transfer_item_between_and_detail": return (new SMLERPReport.ic._report_transfer_item_between_and_detail()); // รายงานโอนสินค้าระหว่างคลังและรายการย่อย
                //case "menu_import_stock_item": return (new SMLERPReport.ic._report_import_stock_item()); // รายงานการับสต๊อกสินค้า  
                //case "menu_record_total_item_first_year": return (new SMLERPReport.ic._report_ic_Record_Total_Item_First_Year());  // รายงานการบันทึกยอดสินค้ายกมาต้นปี   
                //case "menu_item_first_year": return (new SMLERPReport.ic._report_ic_Item_First_Year());  // รายงานรายการสินค้ายกมาต้นปี  
                //case "menu_item_balance_now_only_serial": return (new SMLERPReport.ic._report_ic_Item_Balance_now_Only_Serial());  // รายงานยอดคงเหลือสินค้า ณ ปัจจุบัน(เฉพาะสินค้ามี Serial)


                //เพิ่มข้อมูลธนาคาร
                //case "menu_addata_bank": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, menuName, new SMLERPReport.ar.night_gen_data()); break;
                // เจ้าหนี้
                case "menu_ap_report_payable": return (new SMLERPReport.ap.Detail_Payable());//001-รายงานรายละเอียดเจ้าหนี้
                //---------------------------------------------------------------------------------------------------
                // รายงานเอกสารยกมาต้นปี
                // MOOOOOOOOOOOOOOOOOM
                //---------------------------------------------------------------------------------------------------
                case "menu_ap_report_early_setup": return (new SMLERPReport.ap.Documents_Early_Year());//ตั้งหนี้
                case "menu_ap_report_early_setup_cancel": return (new SMLERPReport.ap.Documents_Early_Year_Cancel());//ยกเลิกตั้งหนี้
                case "menu_ap_report_early_increase_debt": return (new SMLERPReport.ap.Increase_Debt());//เพิ่มนี้
                case "menu_ap_report_early_increase_debt_cancel": return (new SMLERPReport.ap.Increase_Debt_Cancel());//ยกเลิกเพิ่มนี้
                case "menu_ap_report_early_decrease_debt": return (new SMLERPReport.ap.Reduction_Dept());//ลดหนี้
                case "menu_ap_report_early_decrease_debt_cancel": return (new SMLERPReport.ap.Reduction_Dept_Cancel());//ยกเลิกลดหนี้
                //---------------------------------------------------------------------------------------------------
                // รายงานเอกสารอื่นๆๆ
                // MOOOOOOOOOOOOOOOOOM
                //---------------------------------------------------------------------------------------------------
                case "menu_cancel_documents_early_year_other": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก, _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก, screenName)); //(new SMLERPReport.ap.Documents_Early_Year_Other_Cancel());//ยกเลิกตั้งหนี้
                case "menu_cancel_increase_debt_other": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก, _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก, screenName)); //(new SMLERPReport.ap.Increase_Debt_Other_Cancel());//ยกเลิกเพิ่มนี้
                case "menu_cancel_reduction_dept_other": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก, _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก, screenName)); // (new SMLERPReport.ap.Reduction_Dept_Other_Cancel());//ยกเลิกลดหนี้
                //---------------------------------------------------------------------------------------------------
                //case "menu_payable_other": return (new SMLERPReport.ap.Payable_Other());//003-รายงานการตั้งเจ้าหนี้อื่นๆ
                case "menu_movement_payable": return (new SMLERPReport.ap.Movement_Payable());//004-รายงานเคลื่อนไหวเจ้าหนี้     
                // toe
                case "menu_invoice_arrears_by_date": return (new SMLERPReport.ap.Invoice_Arrears_by_Date());//005-รายงานใบส่งของค้างจ่ายขำระ-ตามวันที่ 
                case "menu_invoice_due_by_date": return (new SMLERPReport.ap.Invoice_Due_by_Date());//006-รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
                case "menu_status_payable": return (new SMLERPReport.ap.Status_Payable());//007-รายงานสถานะเจ้าหนี้ 
                case "menu_invoice_overdue_by_date": return (new SMLERPReport.ap.Invoice_Due_by_Date());//008-รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่ 
                case "menu_billing_value_by_invoice": return (new SMLERPReport.ap.Billing_Value_by_Invoice());//009-รายงานใบรับวางบิลค่าสินค้า-ตามใบส่งของ  
                case "menu_billing_outstanding": return (new SMLERPReport.ap.Billing_Outstanding());//010-รายงานใบรับวางบิลค่าสินค้าที่คงค้าง 
                case "menu_payment_detail": return (new SMLERPReport.ap.Payment_Detail());//011-รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย 
                case "menu_invoice_arrears_due": return (new SMLERPReport.ap.Invoice_Arrears_Due());//012-รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด  
                case "menu_payment_by_invoice": return (new SMLERPReport.ap.Payment_by_Invoice());//013-รายงานจ่ายชำระค่าสินค้า-ตามใบส่งของ 
                case "menu_payment_by_date": return (new SMLERPReport.ap.Payment_by_Date());//015-รายงานการจ่ายเงินประจำวัน-ตามวันที่  
                case "menu_payment_by_department": return (new SMLERPReport.ap.Payment_by_Department());//016-รายงานการจ่ายเงินประจำวัน-ตามแผนก  
                case "menu_debt_cut_detail": return (new SMLERPReport.ap.Debt_Cut_Detail());//017-รายงานการตัดหนี้สูญ(เจ้าหนี้)พร้อมรายการย่อย  
                case "menu_debt_cut_by_date": return (new SMLERPReport.ap.Debt_Cut_by_Date());//018-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่  
                case "menu_debt_cut_by_payable": return (new SMLERPReport.ap.Debt_Cut_by_Payable());//019-รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
                case "menu_payable_by_currency": return (new SMLERPReport.ap.Payable_by_Currency());//020-รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ  
                case "menu_payable_ageing": return (new SMLERPReport.ap.Payable_Ageing());//020-รายงานอายุเจ้าหนี้
                //ลูกหนี้----------------------------------------
                case "menu_ar_report_status_ar": return (new SMLERPReport.ar._report_ar_status()); // รายงานสถานะลูกหนี้-ตามลูกหนี้
                case "menu_ar_report_period_ar_debt_remain": return (new SMLERPReport.ar._report_ar_period_debt_remain()); // รายงานอายุลูกหนี้แสดงยอดหนี้คงค้าง
                case "menu_ar_report_invoice_remain_pay": return (new SMLERPReport.ar._report_ar_invoice_payment_remain()); // รายงานใบส่งของค้างชำระ-ตามวันที่
                case "menu_ar_report_trans_ar": return (new SMLERPReport.ar._report_ar_trans()); // รายงานเคลื่อนไหวลูกหนี้
                case "menu_ar_report_check_balance": return (new SMLERPReport.ar._report_ar_check_balance(screenName)); // รายงานตรวจสอบยอดวงเงิน
                case "menu_ar_report_detail_ar": return (new SMLERPReport.ar._report_ar_detail()); // รายงานรายละเอียดลูกค้า-ตามรหัสลูกค้า
                case "menu_ar_report_record_ar": return (new SMLERPReport.ar._report_ar_record()); // รายงานการตั้งลูกหนี้อื่นๆ
                case "menu_ar_report_billing_and_detail": return (new SMLERPReport.ar._report_ar_billing_and_detail()); // รายงานใบวางบิลพร้อมรายการย่อย
                case "menu_ar_report_pay_debt_trans": return new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.ลูกหนี้_รายงานการรับชำระหนี้ประจำวัน, _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้, screenName); //return (new SMLERPReport.ar._report_ar_trans_debt_payment()); // รายงานการรับชำระหนี้ประจำวัน โต๋ ย้ายไป 
                case "menu_ar_report_receipt_and_detail": return (new SMLERPReport.ar._report_ar_receipt_and_detail()); // รายงายใบเสร็จรับเงินพร้อมรายการย่อย
                case "menu_ar_report_cut_debt_lost_and_detail": return (new SMLERPReport.ar._report_ar_cut_debt_lost()); // รายงานการตัดหนี้สูญ(ลูกหนี้)พร้อมรายการย่อย
                //ลูกหนี้ SML Account==============================================================================================================
                case "menu_ar_report_debtor": return (new SMLERPReport.ar._report_ar_detail()); // รายงานรายละเอียดลูกหนี้
                case "menu_ar_report_billing": return (new SMLERPReport.ar._report_ar_billing_and_detail()); // รายงานใบวางบิล
                case "menu_ar_report_receipt_temp": return (new SMLERPReport.ar._report_ar_receipt_temp()); // รายงายใบเสร็จรับเงินชั่วคราว
                case "menu_ar_report_receipt": return (new SMLERPReport.ar._report_ar_receipt_and_detail()); // รายงายการรับชำระหนี้/ออกใบเสร็จรับเงิน
                case "menu_ar_report_debt_cut_off": return (new SMLERPReport.ar._report_ar_cut_debt_lost()); // รายงานการตัดหนี้สูญ
                //==============================================================================================================================
                // ซื้อ
                case "menu_po_report_record_requisition_purchase": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ใบเสนอซื้อ, _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ, screenName)); // รายงานการบันทึกใบขออนุมัติซื้อพร้อมรายการย่อย
                case "menu_po_report_requisition_purchase1": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ, _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ, screenName));
                case "menu_po_report_requisition_purchase2": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก, _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก, screenName));
                case "menu_po_report_requisition_purchase": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ, _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ, screenName)); // MOO 3.1.รายงานสถานะใบเสนอซื้อ ส่งแฟค 3 ตัว ให้เลือก ซื้อ_เสนอซื้อ ไว้ก่อน ด้านในส่งอีก 2 ตัว
                case "menu_po_report_purchase_order": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานใบสั่งซื้อ, _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ, screenName));  // รายงานบันทึกใบสั่งซื้อ  002
                case "menu_po_report_purchase_order_approve": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ, _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ, screenName)); // รายงานบันทึกยกเลิกใบสั่งซื้อ  002
                case "menu_po_report_purchase_order_cancel": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ, _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก, screenName)); // รายงานบันทึกยกเลิกใบสั่งซื้อ  002
                case "menu_po_report_purchase_order_status": return (new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ, _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ, screenName)); // รายงานสถานะใบสั่งซื้อ  S008 ส่งแฟค 3 ตัว ให้เลือก ซื้อ_ใบสั่งซื้อ ไว้ก่อน ด้านในส่งอีก 2 ตัว

                case "menu_po_report_cut_purchase_order": return (new SMLERPReport.po._report_po_cut_purchase_order());// รายงานการตัดใบสั่งซื้อสินค้า
                case "menu_po_report_purchase_order_explain": return (new SMLERPReport.po._report_po_purchase_order_explain());// รายงานการสั่งซื้อสินค้าแบบแจกแจง
                case "menu_po_report_status_purchase_order": return (new SMLERPReport.po._report_po_status_purchase_order());// รายงานสถานะใบสั่งซื้อสินค้า
                case "menu_po_report_cut_receipt": return (new SMLERPReport.po._report_po_cut_receipt());// รายงานการตัดใบรับสินค้า
                case "menu_po_report_purchase_order_due": return (new SMLERPReport.po._report_po_purchase_order_due_payment());// รายงานใบซื้อสินค้าที่ถึงกำหนดจ่ายเงิน
                case "menu_po_report_purchase_total": return (new SMLERPReport.po._report_po_purchase_total());// รายงานยอดซื้อ(ตามวันที่)
                case "menu_po_report_receipt_explain": return (new SMLERPReport.po._report_po_receipt_explain());// รายงานใบรับสินค้าแบบแจกแจง-ตามวันที่
                case "menu_po_report_debt_from_purchase": return (new SMLERPReport.po._report_po_debt_from_purchase());// รายงานใบตั้งหนี้จากการซื้อแบบสรุป-ตามวันที่
                case "menu_po_report_compare_purchase_monthly": return (new SMLERPReport.po._report_po_compare_purchase_monthly());// รายงานเปรียบเทียบยอดซื้อสินค้า12เดือน(ตามสินค้า-ราคา/ปริมาณ)
                case "menu_po_report_purchase_analyze": return (new SMLERPReport.po._report_po_purchase_analyze());// รายงานวิเคราะห์การซื้อสุทธิ-ตามสินค้า
                case "menu_po_report_rank_purchase_total": return (new SMLERPReport.po._report_po_rank_purchase_total());// รายงานการจัดอันดับยอดซื้อ(ตามสินค้า-กลุ่มสินค้า)
                case "menu_po_report_purchase_sum_by_tax": return (new SMLERPReport.po._report_po_purchase_sum_by_tax());// รายงานการซื้อสินค้าสรุปตามประเภทภาษี
                case "menu_po_report_purchase_explain_by_tax": return (new SMLERPReport.po._report_po_purchase_explain_by_tax());// รายงานการซื้อสินค้าแจกแจงตามประเภทภาษี
                case "menu_po_report_return_sum": return (new SMLERPReport.po._report_po_return_sum());// รายงานการส่งคืนสินค้า/ลดหนี้แบบสรุป-ตามสินค้า
                case "menu_po_report_purchase_order_sum": return (new SMLERPReport.po._report_po_purchase_order_sum());// รายงานการสั่งซื้อสินค้าแบบสรุป-ตามวันที่

                //ธนาคาร / เงินสด
                case "_menu_revenue_by_bank": return (new SMLERPReport.cash_bank.Revenue_by_Bank());//001-รายงานรายได้จากธนาคาร
                //case "_menu_cash_deposit": return (new SMLERPReport.cash_bank.Cash_deposit());//002-รายงานการฝากเงินสด
                case "_menu_statement_advance": return (new SMLERPReport.cash_bank.Statement_Advance());//003-รายงาน Statement ล่วงหน้า
                case "_menu_transfer_money_between_banks": return (new SMLERPReport.cash_bank.Transfer_Money_Between_Banks());//004-รายงานการโอนเงินระหว่างธนาคาร
                case "_menu_bank_statement": return (new SMLERPReport.cash_bank._bankStatment());//005-รายงาน Bank Statement
                case "_menu_received_cash_sub_item": return (new SMLERPReport.cash_bank.Received_Cash_sub_item());//006-รายงานการรับเงินอื่นๆพร้อมรายการย่อย
                case "_menu_open_sub_cash": return (new SMLERPReport.cash_bank.Open_sub_Cash());//007-รายงานการตั้งเบิกเงินสดย่อย
                case "_menu_cash_movements": return (new SMLERPReport.cash_bank.Pay_Cash_by_DocDate());//008-รายงานเคลื่อนไหวเงินสด
                case "_menu_pay_cash_by_docdate": return (new SMLERPReport.cash_bank.Pay_Cash_by_DocDate());//009-รายงานบันทึกขอเบิกเงินทดลองจ่าย-เรียงตามวันที่เอกสาร
                //--not --case "_menu_Monthly_Payment_Book": return (new SMLERPReport.cash_bank.Bank_Statement());//010-รายงานสมุดจ่ายเงินประจำเดือน
                case "_menu_book_bank_balance": return (new SMLERPReport.cash_bank.Book_Bank_Balance());//011-รายงานยอดเงินคงเหลือสมุดฝากธนาคาร
                case "_menu_sub_cash_movements": return (new SMLERPReport.cash_bank.Sub_Cash_Movements());//012-รายงานเคลื่อนไหวเงินสดย่อย
                case "_menu_summary_payment": return (new SMLERPReport.cash_bank.Summary_Payment());//013-รายงานสรุปการจ่ายเงิน             

                //ขาย

                case "menu_pay_oreder": return (new SMLERPReport.so.Pay_Oreder());//003-รายงานใบสั่งจ่ายสินค้า
                case "menu_deposit_cut": return (new SMLERPReport.so.Deposit_Cut());//004-รายงานการตัดใบรับเงินมัดจำ
                case "menu_deposit_record": return (new SMLERPReport.so.Deposit_Record());//005-รายงานการบันทึกใบรับเงินมัดจำ
                case "menu_tax_invoice_detail": return (new SMLERPReport.so.Tax_Invoice_Detail());//008-รายงานใบส่งของ/ใบกำกับภาษีพร้อมรายการย่อย
                case "menu_delivery_money_by_date": return (new SMLERPReport.so.Delivery_Money_by_Date());//009-รายงานการนำส่งเงินประจำวัน
                case "menu_explain_item_category_sales": return (new SMLERPReport.so.Explain_Item_Category_Sales());//010-รายงานการขายสินค้าแจกแจงตามประเภทการขาย
                case "menu_explain_invoice_by_product": return (new SMLERPReport.so.Explain_Invoice_by_Product());//011-รายงานการขายสินค้าแบบแจกแจง-ตามสินค้า
                case "menu_analysis_sell_ex_by_product": return (new SMLERPReport.so.Analysis_Sell_Ex_by_Product());//012-รายงานวิเคราะห์การขายแบบแจกแจง-ตามสินค้า
                case "menu_analysis_sell_sum_by_docno": return (new SMLERPReport.so.Analysis_Sell_Sum_by_DocNo());//013-รายงานวิเคราะห์การขายแบบสรุป-ตามเลขที่เกอสาร
                case "menu_sell_by_sale": return (new SMLERPReport.so.Sell_by_Sale());//014-รายงานสรุปการขายสินค้าตามพนักงานขาย
                case "menu_sale_billing": return (new SMLERPReport.so.Sale_Billing());//015-รายงานการเก็บเงินของพนักงานขาย
                case "menu_debt_ar_detail": return (new SMLERPReport.so.Debt_Ar_Detail());//016-รายงานใบเพิ่มหนี้(ลูกหนี้)พร้อมรายการย่อย
                case "menu_check_beck_debt_by_status": return (new SMLERPReport.so.Check_Beck_Debt_by_Status());//017-รายงานใบรับคืนสินค้า/ลดหนี้แสดงตามสถานะ
                case "menu_shipments_by_date": return (new SMLERPReport.so.Shipments_by_Date());//018-รายงานสรุปยอดขายสินค้า-สรุปตามวันที่         
                case "menu_shipments_compare_month": return (new SMLERPReport.so.Shipments_Compare_Month());//019-รายงานเปรียบเที่ยบยอดสินค้า12เดือน
                case "menu_sale_history_product": return (new SMLERPReport.so.Sale_History_Product());//020-รายงานประวัติการขายสินค้า           
                case "menu_product_rank": return (new SMLERPReport.so.Product_Rank());//021-รายงานจัดอันดับยอด-สินค้า
                case "menu_margin_reacceptance": return (new SMLERPReport.so.Margin_reacceptance());//022-รายงานกำไรขั้นต้นแสดงหักรับคืน          
                case "menu_income_cost_matching": return (new SMLERPReport.so.Income_Cost_Matching());//023-Income & Cost matching report         
                case "menu_invoice_tax_cut": return (new SMLERPReport.so.Invoice_Tax_Cut());//024-รายงานการตัดใบส่งของ/ใบกำกับภาษี
            }
            return null;
        }
    }
}
