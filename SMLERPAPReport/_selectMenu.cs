using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPReport
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
                //case "menu_ap_report_payable": return new SMLERPAPReport.report._payable();//รายงานรายละเอียดเจ้าหนี้
                //case "menu_ap_report_early_setup": return new SMLERPAPReport.report._early_debt(1);//รายงานเอกสารตั้งหนี้ยกมา
                //case "menu_ap_report_early_setup_cancel": return new SMLERPAPReport.report._early_debt(2);//รายงานยกเลิกเอกสารตั้งหนี้ยกมา
                //case "menu_ap_report_early_increase_debt": return new SMLERPAPReport.report._early_debt(3);//รายงานเอกสารเพิ่มหนี้ยกมา
                //case "menu_ap_report_early_increase_debt_cancel": return new SMLERPAPReport.report._early_debt(4);//รายงานยกเลิกเอกสารเพิ่มหนี้ยกมา
                //case "menu_ap_report_early_decrease_debt": return new SMLERPAPReport.report._early_debt(5);//รายงานเอกสารลดหนี้ยกมา
                //case "menu_ap_report_early_decrease_debt_cancel": return new SMLERPAPReport.report._early_debt(6);//รายงานยกเลิกเอกสารลดหนี้ยกมา
                //case "menu_ap_report_other_debt_setup": return new SMLERPAPReport.report._other_debt(1);//รายงานเอกสารตั้งหนี้อื่นๆ
                //case "menu_ap_report_other_debt_setup_cancel": return new SMLERPAPReport.report._other_debt(2);//รายงานยกเลิกเอกสารตั้งหนี้อื่นๆ
                //case "menu_ap_report_other_debt_increase": return new SMLERPAPReport.report._other_debt(3);//รายงานเอกสารเพิ่มหนี้อื่นๆ
                //case "menu_ap_report_other_debt_increase_cancel": return new SMLERPAPReport.report._other_debt(4);//รายงานยกเลิกเอกสารเพิ่มหนี้อื่นๆ
                //case "menu_ap_report_other_debt_decrease": return new SMLERPAPReport.report._other_debt(5);//รายงานเอกสารลดหนี้อื่นๆ
                //case "menu_ap_report_other_debt_decrease_cancel": return new SMLERPAPReport.report._other_debt(6);//รายงานยกเลิกเอกสารลดหนี้อื่นๆ
                //case "menu_ap_report_billing": return new SMLERPAPReport.report._billing();//รายงานใบรับวางบิล
                //case "menu_ap_report_prepare_payment": return new SMLERPAPReport.report._prepare_payment();//รายงานการเตรียมจ่ายชำระ
                //case "menu_ap_report_payment": return new SMLERPAPReport.report._payment();//รายงานการจ่ายชำระหนี้
                //case "menu_ap_report_debt_cut_off": return new SMLERPAPReport.report._debt_cut_off();//รายงานการตัดหนี้สูญ
                //case "menu_invoice_due_by_date": return null;//รายงานใบส่งของถึงกำหนดชำระ-ตามวันที่
                //case "menu_invoice_overdue_by_date": return null;//รายงานใบส่งของเกินกำหนดชำระ-ตามวันที่
                //case "menu_billing_outstanding": return null;//รายงานใบรับวางบิลค่าสินค้าที่คงค้าง
                //case "menu_payment_detail": return null;//รายงานจ่ายชำระค่าสินค้าพร้อมรายการย่อย
                //case "menu_invoice_arrears_due": return null;//รายงานใบส่งของค้างชำระแสดงวันที่ถึงกำหนด
                //case "menu_payment_by_date": return null;//รายงานการจ่ายชำระประจำวัน-ตามวันที่
                //case "menu_payment_by_department": return null;//รายงานการจ่ายเงินประจำวัน-ตามแผนก
                //case "menu_debt_cut_by_date": return null;//รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามวันที่
                //case "menu_debt_cut_by_payable": return null;//รายงานการตัดหนี้สูญ(เจ้าหนี้)-ตามเจ้าหนี้
                //case "menu_status_payable": return null;//รายงานสถานะเจ้าหนี้
                //case "menu_movement_payable": return null;//รายงานเคลื่อนไหวเจ้าหนี้
                //case "menu_payable_by_currency": return null;//รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ
                //SMLColorStore
                case "menu_ap_report_detail_ap": return new SMLERPReportTool._reportCustMaster( SMLERPReportTool._reportEnum.เจ้าหนี้_รายละเอียด,screenName); //รายงานรายละเอียดเจ้าหนี้
                case "menu_ap_report_early_year_balance_setup": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_ตั้งหนี้ยกมา, _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา, screenName);  //รายงานเอกสารตั้งหนี้ยกมา
                case "menu_ap_report_early_year_balance_increase": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_เพิ่มหนี้ยกมา, _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา, screenName);  //รายงานเอกสารเพิ่มหนี้ยกมา
                case "menu_ap_report_early_year_balance_decrease": return new SMLERPReportTool._reportTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_ลดหนี้ยกมา, _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา, screenName);  //รายงานเอกสารลดหนี้ยกมา

                case "menu_ap_report_billing": return new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_รับวางบิล, _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล, screenName);  // รายงานใบรับวางบิล (เจ้าหนี้)
                case "menu_ap_report_cancel_pay_bill": return new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_รับวางบิล_ยกเลิก, _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก, screenName);  // รายงานยกเลิกใบรับวางบิล ( เจ้าหนี้ )
                case "menu_ap_report_payment": return new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_จ่ายชำระหนี้, _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้, screenName);  // รายงานการจ่ายชำระหนี้ (เจ้าหนี้)
                case "menu_ap_report_cancel_debt_billing": return new SMLERPReportTool._reportArApTrans(SMLERPReportTool._reportEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก, _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก, screenName);  // รายงานยกเลิกจ่ายชำระ (เจ้าหนี้)


                //case "menu_ap_report_billing": return new SMLERPAPReport.report._billing();//รายงานใบรับวางบิล (เจ้าหนี้)
                case "menu_ap_report_billing_cancel": return new SMLERPAPReport.report._billing_cancel();//รายงานยกเลิกใบรับวางบิล (เจ้าหนี้)

                //case "menu_ap_report_payment": return new SMLERPAPReport.report._payment();//รายงานการจ่ายชำระหนี้ (เจ้าหนี้)
                case "menu_ap_report_payment_cancel": return new SMLERPAPReport.report._payment_cancel();//รายงานยกเลิกจ่ายชำระหนี้ (เจ้าหนี้)
                //case "menu_ap_report_early_year_balance_setup_cancel": return new SMLERPAPReport.report._early_debt(2);//รายงานยกเลิกเอกสารตั้งหนี้ยกมา
                //case "menu_ap_report_early_year_balance_increase_cancel": return new SMLERPAPReport.report._early_debt(4);//รายงานยกเลิกเอกสารเพิ่มหนี้ยกมา
                //case "menu_ap_report_early_year_balance_decrease_cancel": return new SMLERPAPReport.report._early_debt(6);//รายงานยกเลิกเอกสารลดหนี้ยกมา
                //case "menu_ap_report_payment_prepare": return new SMLERPAPReport.report._prepare_payment();//รายงานการเตรียมจ่ายชำระหนี้
                //case "menu_ap_report_payment_prepare_cancel": return new SMLERPAPReport.report._prepare_payment_cancel();//รายงานยกเลิกการเตรียมจ่ายชำระ
                //case "menu_ap_report_debt_cut_off": return new SMLERPAPReport.report._debt_cut_off();//รายงานการตัดหนี้สูญ
                //case "menu_ap_report_debt_cut_off_cancel": return new SMLERPAPReport.report._debt_cut_off_cancel();//รายงานยกเลิกการตัดหนี้สูญ

                case "menu_ap_report_payable_movement": return new SMLERPARAPReport.report._movement(SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_เคลื่อนไหว, screenName);//รายงานเคลื่อนไหวเจ้าหนี้
                case "menu_ap_report_payable_status": return new SMLERPARAPReport.report._absolute_status(SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_สถานะเจ้าหนี้,screenName);//รายงานสถานะเจ้าหนี้
                case "menu_ap_report_payable_aging": return new SMLERPARAPReport.report._ageing(SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้,screenName);//รายงานอายุเจ้าหนี้
                case "menu_ap_report_payable_aging_by_doc": return new SMLERPARAPReport.report._ageingByCodeAndDoc(SMLERPARAPInfo._apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยกลูกหนี้_แยกเอกสาร, screenName);//รายงานอายุลูกหนี้ ตามลูกค้า เอกสาร
                //case "menu_ap_report_payable_by_currency": return null;//รายงานยอดเจ้าหนี้ตามสกุลเงินต่างๆ
                //case "menu_ap_report_payable_not_movement": return new SMLERPAPReport.report._absolute_not_movement();//รายงานเจ้าหนี้ที่ไม่มีการเคลื่อนไหว
                //case "menu_ap_report_payment_daily": return new SMLERPAPReport.report._payment_daily();//รายงานารจ่ายเงินประจำวัน
            }
            return null;
        }
    }

    public enum _enum_report_ap
    {
        payable,
        early_debt_setup,
        early_debt_setup_cancel,
        early_debt_increase,
        early_debt_increase_cancel,
        early_debt_decrease,
        early_debt_decrease_cancel,
        other_debt_setup,
        other_debt_setup_cancel,
        other_debt_increase,
        other_debt_increase_cancel,
        other_debt_decrease,
        other_debt_decrease_cancel,
        billing,
        prepare_payment,
        payment,
        debt_cut_off,
        billing_cancel,
        prepare_payment_cancel,
        payment_cancel,
        debt_cut_off_cancel,
        absolute_movement,
        absolute_status,
        รายงานอายุเจ้าหนี้,
        absolute_not_movement,
        payment_daily
    }
}
