using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SINGHAReport
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName)
            {
                case "menu_report_cash_gl_sheet": return (new GL._glReport(_singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด, screenName)); // 18.4 สมุดบัญชีแยกประเภทเงินสด
                case "menu_report_transfer_gl_sheet": return (new GL._glReport(_singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน, screenName)); // 18.4 สมุดบัญชีแยกประเงินโอน

                case "menu_singha_report_product_wht_3": return (new GL._glReport(_singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3, screenName));  // 18.18
                case "menu_singha_report_product_wht_53": return (new GL._glReport(_singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53, screenName));  // 18.18

                case "menu_singha_report_vat_buy": return (new GL._glReportGenerate(_singhaReportEnum.GL_รายงานภาษีซื้อ, screenName));  // 18.19
                case "menu_singha_report_vat_sale": return (new GL._glReportGenerate(_singhaReportEnum.GL_รายงานภาษีขาย, screenName));  // 18.19

                case "menu_singha_report_supplier_card": return (new _singhareportGenerate(_singhaReportEnum.การ์ดเจ้าหนี้, screenName));  // การ์ดเจ้าหนี้

                case "menu_singha_report_customer_credit_balance": return (new _singhareportGenerate(_singhaReportEnum.ยอดลูกหนี้คงเหลือ, screenName));  // ยอดลูกหนี้คงเหลือ
                case "menu_singha_report_customer_credit_balance_by_creditdate": return (new _singhareportGenerate(_singhaReportEnum.ยอดลูกหนี้คงเหลือ_ตามระยะเวลาเครดิต, screenName));  // ยอดลูกหนี้คงเหลือ

                case "menu_singha_report_journal": return (new GL._glReport(_singhaReportEnum.GL_ข้อมูลรายวัน, screenName));  // 18.18
                case "report_singha_chq_receive_movement": return (new _singhareportGenerate(_singhaReportEnum.รายงานการตัดเช็ค, screenName));

                case "menu_singha_report_sale_summary_by_saleman": return (new _singhareportGenerate(_singhaReportEnum.รายงานสรุปการขายสินค้าตามพนักงานขาย, screenName));
                case "menu_singha_report_ar_outstanding": return (new _singhareportGenerate(_singhaReportEnum.ลูกหนี้คงค้าง, screenName));

                case "menu_singha_report_stock_movement": return (new GL._glReport(_singhaReportEnum.การ์ดสินค้า, screenName));
                case "menu_singha_report_bank_statement": return (new _singhareportGenerate(_singhaReportEnum.BankStatement, screenName));

                case "menu_singha_report_load_product_warehouse": return (new GL._glReport(_singhaReportEnum.สรุปโหลดสินค้า_รายคลัง, screenName));

                case "menu_singha_report_other_expenses": return (new _singhareportGenerate(_singhaReportEnum.รายงานค่าใช้จ่ายอื่น, screenName)); // รายงานค่าใช้จ่ายอื่น ๆ

                case "menu_sync_data_saleout": return (new _singhaDataSyncControl(2));
                case "menu_sync_data_stock": return (new _singhaDataSyncControl(3));

                case "menu_report_sale_category_year_compare": return (new GL._glReport(_singhaReportEnum.รายงานยอดขายรายเดือนตามกลุ่มสินค้า_เทียบปี, screenName));

                // st
                case "menu_saletools_customer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TmV3Q3VzdG9tZXIuYXNweA==");

                ///case "menu_2.Route": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_route_planning": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=Um91dGUuYXNweA==");
                case "menu_saletools_adhoc_jobs": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFuYWdlQWRIb2MuYXNweA==");
                case "menu_saletools_manage_route_plan": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=c2NoZWR1bGUuYXNweA==");

                //case "menu_3.Transaction": return new _saleToolsWebControl(menuName, "");
                //case "menu_ 3.1 VAN": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_create_cash_sales": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT0w");
                case "menu_saletools_create_credit_sales": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT0x");
                case "menu_saletools_approve_invoice": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RE9IaXN0b3J5LmFzcHg/VHlwZT1BJlZhbj1ZJkludm9pY2U9WSZTcGxpdD1OJk9yZGVyaWQ9");

                //case "menu_ 3.2 Pre-Order": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_create_sales_order": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT0z");
                case "menu_saletools_approve_sales_order": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT0zJkN1c0lEPQ==");
                case "menu_saletools_approve_sales_invoice": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT0zJkN1c0lEPQ==");

                //case "menu_3.3 Bill Collection": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_create_bill_collection": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UGF5QmlsbC5hc3B4");
                case "menu_saletools_approve_bill_collection": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=Q29uZmlybVBhaWQuYXNweA==");

                case "menu_saletools_create_ods_transfer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT0yJkN1c0lEPQ==");
                case "menu_saletools_approve_ods_transfer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT0yJkN1c0lEPQ==");
                case "menu_saletools_create_ods_received": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1H");
                case "menu_saletools_approve_ods_received": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1HJkN1c0lEPQ==");
                case "menu_saletools_create_ods_issue": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1J");
                case "menu_saletools_approve_ods_issue": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1JJkN1c0lEPQ==");
                case "menu_saletools_create_ods_recieved_customer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1HJkdDdXM9WQ==");
                case "menu_saletools_create_ods_issued_customer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1JJkdDdXM9WQ==");
                case "menu_saletools_approve_ods_recieved_customer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1HJkdDdXM9WQ==");
                case "menu_saletools_approve_ods_issued_customer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1JJkdDdXM9WQ==");
                case "menu_saletools_ods_exchange": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT00JkN1c0lEPQ==");

                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_3.7 Generate Code": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_approve_code": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=R2VuZXJhdGVDYW5jZWxDb2RlLmFzcHg=");
                case "menu_saletools_document_running_no": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFzdGVyRG9jbm8uYXNweA==");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_3.10 Interface SML": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_ods_transfer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=U01MSW50ZXJmYWNlbWFzdGVyLmFzcHg=");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_4.Photo": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_photo_group": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=cGhvdG9ncm91cGxpc3RWMi5hc3B4P1R5cGU9UEg=");
                case "menu_saletools_photos": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=U2hvcFBob3RvLmFzcHg=");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_5.Map": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_hea_map": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=aGVhdG1hcC5hc3B4");
                case "menu_saletools_view_map": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFwLmFzcHg=");
                case "menu_saletools_fleet": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=Vmlld01hcC5hc3B4");
                case "menu_saletools_real_time": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=cmVhbHRpbWUuYXNweA==");
                case "menu_saletools_customer_gps_by_salesman": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=c2FsZXBlcnNvbnJlcG9ydC5hc3B4");
                case "menu_saletools_aap_analysis": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFwUHJvZHVjdC5hc3B4");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_promotions": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UHJvbW90aW9uTGlzdC5hc3B4");
                case "menu_saletools_import_promotion": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=aW1wb3J0ZXhwb3J0cHJvbW90aW9uLmFzcHg=");
                case "menu_saletools_promotion_group": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UHJvbW90aW9uR3JvdXAuYXNweA==");
                case "menu_saletools_advance_price": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QWR2YW5jZVByaWNlTGlzdC5hc3B4");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_8.Users": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_web_users": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=dXNlcjIuYXNweA==");
                case "menu_saletools_change_password": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=Q2hhbmdlUGFzc3dvcmQuYXNweA==");
                case "menu_saletools_mobile_users": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFwSWRDdXN0b21lci5hc3B4");
                case "menu_saletools_sync_log": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=c3luY2xvZ3MuYXNweA==");
                case "menu_saletools_user_roles": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFzdGVyUm9sZS5hc3B4");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_9.Master Data": return new _saleToolsWebControl(menuName, "");
                //case "menu_9.1 Administrator": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_menu_info": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWVudUluZm8uYXNweA==");
                case "menu_saletools_menu_authorize": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWVudUF1dGhvcml6ZS5hc3B4");
                case "menu_saletools_import_export_data": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFzdGVySW1wb3J0RXhwb3J0LmFzcHg=");
                case "menu_saletools_update_mobile": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=VXBkYXRlVmVyc2lvbi5hc3B4");
                case "menu_saletools_import_master_sml": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=U01MSW50ZXJmYWNlTWFzdGVyLmFzcHg=");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_9.2 Common ": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_product": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QWRkUHJvZHVjdENhdGVnb3J5VjIuYXNweA==");
                //case "menu_9.2.2 Reason": return new _saleToolsWebControl(menuName, "error");
                case "menu_saletools_store": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFzdGVyU3RvcmUuYXNweA==");
                case "menu_saletools_region": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVnaW9uTWFzdGVyLmFzcHg=");
                case "menu_saletools_state": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UHJvdmluY2VNYXN0ZXIuYXNweA==");
                case "menu_saletools_city": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RGlzdHJpY3RNYXN0ZXIuYXNweA==");
                case "menu_saletools_town": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QWRkVG93bi5hc3B4");
                case "menu_saletools_customer_type": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QVJ0eXBlLmFzcHg=");
                case "menu_saletools_credit_days": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFzdGVyQ3JlZGl0RGF5cy5hc3B4");
                case "menu_saletools_price_level": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UHJpY2VMZXZlbE1hc3Rlci5hc3B4");
                case "menu_saletools_adjust_money": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=YWRqbW9uZXltYXN0ZXIuYXNweA==");
                case "menu_saletools_news_type": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=bmV3c3R5cGUuYXNweA==");
                case "menu_saletools_payment_type": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UGF5bWVudFR5cGUuYXNweA==");
                case "menu_saletools_survey_type": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=c3VydmV5dHlwZS5hc3B4");
                case "menu_saletools_delivery_days": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RGVsaXZlcnlEYXlzLmFzcHg=");
                case "menu_saletools_material_type": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWF0VHlwZU1hc3Rlci5hc3B4");
                case "menu_saletools_display_type": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RGlzcGxheVR5cGUuYXNweA==");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_ 9.3 Agent/DC": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_distributor": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RENNYXN0ZXIuYXNweA==");
                case "menu_saletools_company": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=Q29tcGFueS5hc3B4");
                case "menu_saletools_warehouse": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TWFzdGVyV2FyZUhvdXNlLmFzcHg=");
                case "menu_saletools_dc_state": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RENQcm92aW5jZS5hc3B4");
                case "menu_saletools_dcsection": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RENTZWN0aW9uLmFzcHg=");
                case "menu_saletools_sales_area": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=U2FsZXNBcmVhTWFzdGVyLmFzcHg=");
                case "menu_saletools_delivery_route": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=RGVsaXZlcnlSb3V0ZU1hc3Rlci5hc3B4");
                case "menu_saletools_bank": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QmFua01hc3Rlci5hc3B4");
                case "menu_saletools_bank_account": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QmFua0FjY291bnRNYXN0ZXIuYXNweA==");
                //case "menu_9.3.12 mapvehicledeliveryroute.aspx": return new _saleToolsWebControl(menuName, "error");
                //case "menu_": return new _saleToolsWebControl(menuName, "");
                //case "menu_9.4 Operation": return new _saleToolsWebControl(menuName, "");
                case "menu_saletools_news_and_documents": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TmV3c0xpc3QuYXNweA==");
                case "menu_saletools_mustlist_product": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=TXVzdExpc3QuYXNweA==");
                //case "menu_9.4.5 SaleTargetAll.aspx": return new _saleToolsWebControl(menuName, "error");
                //case "menu_9.4.9 Supplier": return new _saleToolsWebControl(menuName, "error");
                // http://????????/dbo_agent/login.aspx?userid=admin&url=d2ViY29uZmlnbWFzdGVyLmFzcHg=
                case "menu_saletools_14_9": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=d2ViY29uZmlnbWFzdGVyLmFzcHg=");

                case "menu_saletools_approve_goods_transfer_request": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1S");
                case "menu_saletools_approve_goods_return": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT01JkN1c0lEPQ==");

                case "menu_saletools_web_call_detail_in_plan": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=cmVwb3J0VmlzaXRDdXN0LmFzcHg=");
                case "menu_saletools_web_call_detail_out_plan": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=cmVwb3J0dmlzaXRwbGFub3V0LmFzcHg=");
                case "menu_saletools_web_call_summary_daily": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=cmVwb3J0dmlzaXQuYXNweA==");
                case "menu_saletools_web_call_summary_monthly": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=cmVwb3J0dmlzaXRTYWxlVGFnZXQuYXNweA==");
                case "menu_saletools_web_call_trend": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0Q2FsbFRyZW5kX0xpdGUuYXNweA==");

                case "menu_saletools_ap_goods_received": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1L");
                case "menu_saletools_approve_ap_goods_received": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1L");
                case "menu_saletools_ap_good_issues": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1Y");
                case "menu_saletools_approve_ap_good_issues": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1Y");

                case "menu_saletools_approve_payment": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=Q29uZmlybVBhaWRfQVAuYXNweA==");

                case "menu_saletools_product_qty_deposit_movement": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0U3RvY2tDYXJkTW92ZW1lbnRTTUwuYXNweA==");
                case "menu_saletools_product_amount_deposit_movement": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0U3RvY2tDYXJkTW92ZW1lbnRTTUxWMi5hc3B4");
                case "menu_saletools_product_qty_ar_deposit": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0U3RvY2tDdXMuYXNweD9HQ3VzPVk=");
                case "menu_saletools_product_qty_ap_deposit": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0U3RvY2tDdXMuYXNweD9HQ3VzPQ==");

                case "menu_saletools_approve_return_from_cus": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPU4mVHlwZT1HJkdDdXM9WQ==");

                case "menu_saletools_master_import": return new _saleToolsWebControl(menuName, "login.aspx?&userid=admagent&url=TWFzdGVySW1wb3J0RXhwb3J0LmFzcHg=");

                case "menu_saletools_promotion_get": return new _saleToolsWebControl(menuName, "login.aspx?&userid=admagent&url=cmVwb3J0UHJvbW90aW9uQW10LmFzcHg=");

                case "menu_saletools_discount_summary_report": return new _saleToolsWebControl(menuName, "login.aspx?&userid=admagent&url=UmVwb3J0RGlzY291bnRPbnRvcFN1bW1hcnkuYXNweA==");

                case "menu_saletools_discount_detail_report": return new _saleToolsWebControl(menuName, "login.aspx?&userid=admagent&url=UmVwb3J0RGlzY291bnRPbnRvcC5hc3B4");

                case "menu_saletools_sale_pivot_report": return new _saleToolsWebControl(menuName, "login.aspx?&userid=admagent&url=cmVwb3J0QkkuYXNweA==");

                case "menu_saletools_customer_pivot_report": return new _saleToolsWebControl(menuName, "login.aspx?&userid=admagent&url=cmVwb3J0QklDdXN0b21lci5hc3B4");

                case "menu_saletools_issue_to_customer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1JJkdDdXM9WQ==");
                case "menu_saletools_receive_from_customer": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=QXBwcm92ZU5ldy5hc3B4P05ld09yZGVyPVkmVHlwZT1HJkdDdXM9WQ==");
                case "menu_saletools_sale_target_config": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=U2FsZVRhcmdldEFsbC5hc3B4");

                case "menu_saletools_report_discount_by_shop_and_product": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0RGlzY291bnRCeUNoYW5uZWxCeU1hdGVyaWFsLmFzcHg=");
                case "menu_saletools_report_discount_changed": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0RGlzY291bnRBZGouYXNweA==");
                case "menu_saletools_report_discount_by_product_group": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0RGlzY291bnRPbnRvcFN1bW1hcnkuYXNweA==");
                case "menu_saletools_report_promotion_use_by_level": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0UHJvbW90aW9uRWFjaFVzZUJ5TGV2ZWwuYXNweA==");
                case "menu_saletools_report_sale_report": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuU2FsZS5hc3B4P1JlcG9ydD0xJk5hbWU9U2FsZVZvbHVtbmJ5U2FsZQ==");
                case "menu_saletools_report_sale_report_by_saleman": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuU2FsZS5hc3B4P1JlcG9ydD0yJk5hbWU9U2FsZVZhbHVlYnlTYWxl");
                case "menu_saletools_report_sale_report_by_shop": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0zJk5hbWU9RWZmZWN0aXZlQ2FsbA==");
                case "menu_saletools_report_purchase_by_shop_saleman": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD00Jk5hbWU9QWN0aXZlT3V0bGV0c19TYWxl");
                case "menu_saletools_report_purchase_percent_by_shop_saleman": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD01Jk5hbWU9QWN0aXZlT3V0bGV0c190YXJnZXQ=");
                case "menu_saletools_report_purchase_average_per_serway": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD02Jk5hbWU9QWN0aXZlT3V0bGV0c19Sb3V0ZQ==");
                case "menu_saletools_report_sale_average_per_doc": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD03Jk5hbWU9QVZHU2FsZV9pbnZvaWNl");
                case "menu_saletools_report_sale_product_average_per_doc": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD04Jk5hbWU9QVZHTnVtU2t1X2ludm9pY2U=");
                case "menu_saletools_report_sale_groupproduct_amount": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD05Jk5hbWU9S2V5U2t1RGlz");
                case "menu_saletools_report_sale_ratio_average_by_groupproduct_per_shop": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0xMCZOYW1lPUtleVNLVV9UYXJnZXQ=");
                case "menu_saletools_report_sale_ratio_average_by_groupproduct_per_shop_map": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0xMSZuQU1FPUtleVNLVV9Sb3V0ZVBsYQ==");
                case "menu_saletools_report_sale_ratio_groupproduct": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0xMiZOYW1lPUtleVByb2R1Y3RHcm91cERpcw==");
                case "menu_saletools_report_purchase_ratio_by_saleman": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0xMyZOYW1lPUtleVByb2R1Y3RHcm91cERpc19UYXJnZXQ=");
                case "menu_saletools_report_purchase_ratio_in_map": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0xNCZOYW1lPUtleVByb2R1Y3RHcm91cERpc19Sb3V0ZVBsYQ==");
                case "menu_saletools_report_shop_in_month": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0xNSZOYW1lPURpc2J5RGlz");
                case "menu_saletools_report_shop_in_month_byamper": return new _saleToolsWebControl(menuName, "login.aspx?userid=admagent&url=UmVwb3J0dmFuc2FsZS5hc3B4P1JlcG9ydD0xNiZOYW1lPURpc0J5UHJv");


            }
            return null;
        }
    }
}
