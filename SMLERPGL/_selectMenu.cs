using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL
{
    public class _selectMenu
    {
        public static void _openAccountPeriod()
        {
            MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, "", new SMLERPConfig._accountPeriod._accountPeriod());
        }

        public static void _checkPeriod()
        {
            if (_g.g._accountPeriodDateBegin.Count == 0)
            {
                DialogResult messageResult = MessageBox.Show(MyLib._myGlobal._mainForm, "ยังไม่ได้กำหนดงวดบัญชี\nต้องการกำหนดหรือไม่ (แนะนำให้กำหนดก่อน)", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (messageResult == DialogResult.Yes)
                {
                    _openAccountPeriod();
                    _g.g._accountPeriodGet();
                }
            }
        }

        public static Control _getObject(string menuName, string screenName)
        {
            _g.g._accountPeriodGet();
            switch (menuName.ToLower())
            {
                case "menu_singha_report_profit_and_lost": return (new SMLERPGL._report._singhaReportProfitAndLost._report(0));
                case "menu_singha_report_balancesheet_asset": return (new SMLERPGL._report._singhaReportProfitAndLost._report(1));
                case "menu_gl_journal_pass": return (new SMLERPGL._journalPass());
                case "menu_gl_journal_pass_cancel": return (new SMLERPGL._journalPassUndo());
                case "menu_chart_of_account": return (new SMLERPGL._chart._chartOfAccount());
                case "menu_chart_of_account_fast":
                    if (_g.g._accountPeriodDateBegin.Count > 0)
                    {
                        if (MessageBox.Show(MyLib._myGlobal._resource("โปรแกรมจะทำการ Lock ข้อมูลผังบัญชีทั้งหมด ต้องการทำรายการต่อหรือไม่"), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            MyLib._myFrameWork _frameWork = new MyLib._myFrameWork();
                            DataSet __count = _frameWork._query(MyLib._myGlobal._databaseName, "select count(*) as countrecord  from " + _g.d.gl_chart_of_account._table + " where guid_code is not null");
                            string __getCount = __count.Tables[0].Rows[0].ItemArray[0].ToString();
                            if (__getCount.Equals("0"))
                            {
                                return (new SMLERPGL._chart._chartOfAccountFast());
                            }
                            else
                            {
                                MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถ Lock รายการผังบัญชีได้ เนื่องจากมีผู้อื่นใช้งานอยู่บางส่วน"));
                            }
                        }
                    }
                    else
                    {
                        _checkPeriod();
                    }
                    break;
                case "menu_chart_of_account_flow":
                    return (new SMLERPGL._chart._chatOfAccountFlow());
                case "menu_report_chart_of_account":
                    // รายงานรายละเอียดผังบัญชี
                    return (new SMLERPGL._report._chartOfAccount._report());
                case "menu_gl_analysis_trial_balance":
                    // วิเคราะห์มิติแบบงบทดลอง
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._analysis._trialBalance());
                case "menu_gl_report_journal":
                    // รายงานข้อมูลรายวัน (บัญชีแยกประเภท)
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._journal._report());
                case "menu_gl_report_sheet":
                    // รายงานบัญชีแยกประเภท
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._sheet._report());
                case "menu_gl_report_sheet_sum":
                    // รายงานบัญชีแยกประเภท แบบสรุป
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._sheetSum._report());
                case "menu_gl_report_trial_balance":
                    // รายงานงบทดลอง
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._trialBalance._report());
                case "menu_gl_report_trial_balance_branch":
                    // รายงานงบทดลอง (แยกสาขา)
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._trialBalanceBranch._report());
                case "menu_gl_report_trial_balance_branch_depart":
                    // รายงานงบกำไรขาดทุน (แยกสาขา-แผนก)
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._trialBalanceBranchDepart._report());
                case "menu_gl_report_work_sheet":
                case "menu_gl_report_balance_sheet":
                    // รายงานกระดาษทำการ
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._workSheet._report());
                case "menu_gl_report_journal_sum":
                    // รายงานข้อมูลรายวัน แบบสรุป (บัญชีแยกประเภท)
                    return (new SMLERPGL._report._journalSum._report());
                case "menu_gl_report_other_1":
                    // งบกระแสเงินสด
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._other_1._report());
                case "menu_gl_report_profit_and_lost_branch_1":
                    // งบกำไรขาดทุน แยกสาขา
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._report._profitAndLostBranch._report());
                // ข้อมูลรายวัน
                case "menu_gl_journal":
                    _checkPeriod();
                    if (_g.g._accountPeriodDateBegin.Count > 0)
                    {
                        return (new SMLERPGL._journalEntryAccount());
                    }
                    break;
                case "menu_gl_journal_fast":
                    _checkPeriod();
                    return (new SMLERPGL._journalEntryFast());
                case "menu_gl_check_data":
                    // ตรวจสอบความถูกต้อง
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._checkData());
                case "menu_gl_show_sheet":
                    //แสดงบัญชีแยกประเภท
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._display._glList());
                case "menu_gl_show_sheet_sum":
                    //แสดงบัญชีแยกประเภท แบบสรุป
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._display._glListSum());
                case "menu_gl_show_trial_balance":
                    // แสดงงบทดลอง
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._display._glTrialBalance());
                case "menu_gl_show_work_sheet":
                    // แสดงกระดาษทำการ
                    SMLProcess._glProcess._glUpdateAll();
                    return (new SMLERPGL._display._glWorkSheet());
                case "menu_gl_report_design":
                    if (MyLib._myGlobal._isDemo && MyLib._myGlobal._programName != "Account POS Singha")
                    {
                        MyLib._myGlobal._demoVersion();
                        return null;
                    }
                    return (new SMLERPGL._report._reportDesign._glDesign());
                case "menu_gl_picture": return (new SMLERPGL._journalEntryAccount_pic()); //somruk
                case "change_chart_of_account_code":
                    _chart._changeAccountCode __changeCode = new SMLERPGL._chart._changeAccountCode();
                    __changeCode.ShowDialog();
                    break;
                case "menu_report_vat_sale": return (new SMLERPGL._tax._vat(SMLERPGL._tax._vatConditionType.ภาษีขาย, screenName));
                case "menu_report_vat_sale_2557": return (new SMLERPGL._tax._vat(SMLERPGL._tax._vatConditionType.ภาษีขาย_เลขประจำตัวผู้เสียภาษี, screenName));

                // toe
                case "menu_report_except_vat_sale": return (new SMLERPGL._tax._vat(_tax._vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี, screenName));
                case "menu_report_sum_vat_sale": return (new SMLERPGL._tax._vat(_tax._vatConditionType.ภาษีขาย_สรุป, screenName));

                case "menu_report_vat_sale_full_ref": return (new SMLERPGL._tax._vat(_tax._vatConditionType.ภาษีขาย_สินค้ายกเว้นภาษี, screenName));

                case "menu_report_vat_buy": return (new SMLERPGL._tax._vat(SMLERPGL._tax._vatConditionType.ภาษีซื้อ, screenName));
                case "menu_report_vat_buy_2557": return (new SMLERPGL._tax._vat(SMLERPGL._tax._vatConditionType.ภาษีซื้อ_เลขประจำตัวผู้เสียภาษี, screenName));
                case "menu_report_except_vat_buy": return (new SMLERPGL._tax._vat(SMLERPGL._tax._vatConditionType.ภาษีซื้อ_สินค้ายกเว้นภาษี, screenName));
                case "menu_gl_wht_report_out1": return (new SMLERPGL._report._tax._wht(SMLERPGL._tax._whtConditionType.หักณที่จ่ายภงด3, screenName));

                case "menu_gl_wht_report_out2": return (new SMLERPGL._report._tax._wht(SMLERPGL._tax._whtConditionType.หักณที่จ่ายภงด53, screenName));
                //case "menu_gl_wht_report_sum": return (new SMLERPGL._report._tax._wht(SMLERPGL._tax._whtConditionType.ถูกหักณที่จ่าย, screenName));  
                // toe
                case "menu_gl_wht_report_in_53": return (new SMLERPGL._report._tax._wht(SMLERPGL._tax._whtConditionType.หักณที่จ่ายภงด53, screenName));
                case "menu_gl_wht_report_in_3": return (new SMLERPGL._report._tax._wht(SMLERPGL._tax._whtConditionType.หักณที่จ่ายภงด3, screenName));
                case "menu_gl_wht_report_sum": return (new SMLERPGL._report._tax._wht(SMLERPGL._tax._whtConditionType.ถูกหักณที่จ่าย, screenName));
                case "menu_gl_process": return (new _transProcessUserControl());
            }
            return null;
        }
    }
}
