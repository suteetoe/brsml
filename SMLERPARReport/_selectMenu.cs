using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARReport
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            return _getObject(menuName, "");
        }

        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                //SMLAccount
                //case "menu_ar_report_debtor": return new SMLERPARAPReport.report._debtor();//รายงานรายละเอียดลูกหนี้
                //case "menu_ar_report_early_setup": return new SMLERPARAPReport.report._early_debt(1);//รายงานเอกสารตั้งหนี้ยกมา
                //case "menu_ar_report_early_setup_cancel": return new SMLERPARAPReport.report._early_debt(2);//รายงานยกเลิกเอกสารตั้งหนี้ยกมา
                //case "menu_ar_report_early_debt_increase": return new SMLERPARAPReport.report._early_debt(3);//รายงานเอกสารเพิ่มหนี้ยกมา
                //case "menu_ar_report_early_debt_increase_cancel": return new SMLERPARAPReport.report._early_debt(4);//รายงานยกเลิกเอกสารเพิ่มหนี้ยกมา
                //case "menu_ar_report_early_debt_decrease": return new SMLERPARAPReport.report._early_debt(5);//รายงานเอกสารลดหนี้ยกมา
                //case "menu_ar_report_early_debt_decrease_cancel": return new SMLERPARAPReport.report._early_debt(6);//รายงานยกเลิกเอกสารลดหนี้ยกมา
                //case "menu_ar_report_orther_debt_setup": return new SMLERPARAPReport.report._other_debt(1);//รายงานเอกสารตั้งหนี้อื่นๆ
                //case "menu_ar_report_orther_debt_setup_cancel": return new SMLERPARAPReport.report._other_debt(2);//รายงานยกเลิกเอกสารตั้งหนี้อื่นๆ
                //case "menu_ar_report_orther_debt_increase": return new SMLERPARAPReport.report._other_debt(3);//รายงานเอกสารเพิ่มหนี้อื่นๆ
                //case "menu_ar_report_orther_debt_increase_cancel": return new SMLERPARAPReport.report._other_debt(4);//รายงานยกเลิกเอกสารเพิ่มหนี้อื่นๆ
                //case "menu_ar_report_orther_debt_decrease": return new SMLERPARAPReport.report._other_debt(5);//รายงานเอกสารลดหนี้อื่นๆ
                //case "menu_ar_report_orther_debt_decrease_cancel": return new SMLERPARAPReport.report._other_debt(6);//รายงานยกเลิกเอกสารลดหนี้อื่นๆ
                //case "menu_ar_report_billing": return new SMLERPARAPReport.report._billing();//รายงานใบวางบิล
                //case "menu_ar_report_billing_cancel": return new SMLERPARAPReport.report._billing_cancel();//รายงานยกเลิกใบวางบิล
                //case "menu_ar_report_receipt_temp": return new SMLERPARAPReport.report._receipt_temp();//รายงานการออกใบเสร็จรับเงินชั่วคราว
                //case "menu_ar_report_receipt": return new SMLERPARAPReport.report._receipt();//รายงานการรับชำระหนี้/ออกใบเสร็จรับเงิน
                //case "menu_ar_report_receipt_cancel": return new SMLERPARAPReport.report._receipt_cancel();//รายงานยกเลิกชำระหนี้/ออกใบเสร็จรับเงิน
                //case "menu_ar_report_debt_cut_off": return new SMLERPARAPReport.report._debt_cut_off();//รายงานการตัดหนี้สูญ
                //case "menu_ar_report_status_ar": return new SMLERPARAPReport.report._absolute_status();//รายงานสถานะลูกหนี้
                //case "menu_ar_report_period_ar_debt_remain": return new SMLERPARAPReport.report._absolute_period_debt_remain();//รายงานอายุลูกหนี้แสดงยอดหนี้คงค้าง
                //case "menu_ar_report_trans_ar": return new SMLERPARAPReport.report._absolute_trans();//รายงานเคลื่อนไหวลูกหนี้
                //case "menu_ar_report_check_balance": return new SMLERPARAPReport.report._absolute_check_balance();//รายงานตรวจสอบยอดวงเงิน
                //case "menu_ar_report_invoice_remain_pay": return new SMLERPARAPReport.report._absolute_invoice_remain_pay();//รายงานใบส่งของค้างชำระ-ตามวันที่
                //SMLColorStore
                case "menu_ar_report_detail_ar": return new SMLERPReportTool._reportCustMaster(SMLERPReportTool._reportEnum.ลูกหนี้_รายละเอียด, screenName); //รายงานรายละเอียดลูกหนี้
                case "menu_ar_report_early_year_balance_setup": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ลูกหนี้_ตั้งหนี้ยกมา, _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา, screenName);  //รายงานเอกสารตั้งหนี้ยกมา
                case "menu_ar_report_early_year_balance_increase": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ลูกหนี้_เพิ่มหนี้ยกมา, _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา, screenName);  //รายงานเอกสารเพิ่มหนี้ยกมา
                case "menu_ar_report_early_year_balance_decrease": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.ลูกหนี้_ลดหนี้ยกมา, _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา, screenName);  //รายงานเอกสารลดหนี้ยกมา
                case "menu_ar_report_billing": return new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.ลูกหนี้_รับวางบิล, _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล, screenName);  //รายงานใบวางบิล
                //case "menu_ar_report_billing": return new SMLERPARAPReport.report._billing();//รายงานใบวางบิล
                case "menu_ar_report_billing_cancel": return new SMLERPARAPReport.report._billing_cancel();//รายงานยกเลิกใบวางบิล
                case "menu_ar_report_receipt": return new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.ลูกหนี้_รับชำระหนี้, _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้, screenName); ;//รายงานการรับชำระหนี้/ออกใบเสร็จรับเงิน
                //case "menu_ar_report_receipt": return new SMLERPARAPReport.report._receipt();//รายงานการรับชำระหนี้/ออกใบเสร็จรับเงิน
                case "menu_ar_report_receipt_cancel": return new SMLERPARAPReport.report._receipt_cancel();//รายงานยกเลิกชำระหนี้/ออกใบเสร็จรับเงิน
                //case "menu_ar_report_early_year_balance_setup_cancel": return new SMLERPARAPReport.report._early_debt(2);//รายงานยกเลิกเอกสารตั้งหนี้ยกมา
                //case "menu_ar_report_early_year_balance_increase_cancel": return new SMLERPARAPReport.report._early_debt(4);//รายงานยกเลิกเอกสารเพิ่มหนี้ยกมา
                //case "menu_ar_report_early_year_balance_decrease_cancel": return new SMLERPARAPReport.report._early_debt(6);//รายงานยกเลิกเอกสารลดหนี้ยกมา
                //case "menu_ar_report_billing_automatic": return new SMLERPARAPReport.report._billing_automatic();//รายงานใบวางบิลอัตโนมัติ

                case "menu_ar_report_status_ar": return new SMLERPARAPReport.report._absolute_status(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_สถานะลูกหนี้,screenName);//รายงานสถานะลูกหนี้
                case "menu_ar_report_period_ar_debt_remain": return new SMLERPARAPReport.report._ageing(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้, screenName);//รายงานอายุลูกหนี้
                case "menu_ar_report_period_ar_debt_remain_by_doc": return new SMLERPARAPReport.report._ageingByCodeAndDoc(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร,screenName);//รายงานอายุลูกหนี้ ตามลูกค้า เอกสาร
                case "menu_ar_report_movement": return new SMLERPARAPReport.report._movement(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_เคลื่อนไหว, screenName);//รายงานเคลื่อนไหวลูกหนี้
                //case "menu_ar_report_receipt_daily": return new SMLERPARAPReport.report._receipt_daily();//รายงานการรับเงินประจำวัน 
                    //toe
                case "menu_report_point_balance": return new SMLERPARReport.report._ar_report(SMLERPReportTool._reportEnum.ลูกหนี้_แต้มคงเหลือ, screenName);
            }
            return null;
        }
    }
}
