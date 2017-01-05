using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLProcess
{
    public class _glProcess
    {
        DateTime _oldDate;
        int _countDetailPerDay;
        decimal _sumDebit;
        decimal _sumCredit;
        int _countTotal;
        decimal _sumTotalDebit;
        decimal _sumTotalCredit;

        public _glProcess()
        {
        }

        public static void _glUpdateAll()
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // ปรับสถานะของผังบัญชี เพื่อความปลอดภัย กรณีโอนข้อมูลเข้า
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account._table + " set " + _g.d.gl_chart_of_account._account_type + "=0 where " + _g.d.gl_chart_of_account._account_type + " is null"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account._table + " set " + _g.d.gl_chart_of_account._balance_mode + "=0 where " + _g.d.gl_chart_of_account._balance_mode + " is null"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account._table + " set " + _g.d.gl_chart_of_account._department_status + "=0 where " + _g.d.gl_chart_of_account._department_status + " is null"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account._table + " set " + _g.d.gl_chart_of_account._job_status + "=0 where " + _g.d.gl_chart_of_account._job_status + " is null"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account._table + " set " + _g.d.gl_chart_of_account._allocate_status + "=0 where " + _g.d.gl_chart_of_account._allocate_status + " is null"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_chart_of_account._table + " set " + _g.d.gl_chart_of_account._active_status + "=1 where " + _g.d.gl_chart_of_account._active_status + " is null"));
            // ปรับปรุงสถานะ Debit หรือ Credit รายวันย่อย
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._debit_or_credit + "=0 where " + _g.d.gl_journal_detail._debit + "<>0 and (" + _g.d.gl_journal_detail._debit_or_credit + "<>0 or " + _g.d.gl_journal_detail._debit_or_credit + " is null)"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._debit_or_credit + "=1 where " + _g.d.gl_journal_detail._credit + "<>0 and (" + _g.d.gl_journal_detail._debit_or_credit + "<>1 or " + _g.d.gl_journal_detail._debit_or_credit + " is null)"));
            __myquery.Append("</node>");
            _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myquery.ToString());
        }

        /// <summary>
        /// รายงานกระดาษทำการ
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="chartOfAccountTreeView"></param>
        /// <param name="allData"></param>
        /// <param name="showDebitCredit"></param>
        /// <returns></returns>
        public ArrayList _glViewWorkSheet(DateTime dateBegin, DateTime dateEnd, MyLib._myTreeViewDragDrop chartOfAccountTreeView, bool allData, bool showDebitCredit, int isPass)
        {
            string __isPassQuery = " and " + _g.d.gl_journal_detail._is_pass + ((isPass == 0) ? " in (0,1)" : "=1");
            ArrayList __result = new ArrayList();

            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // หายอดยกมา (บันทึกยอดยกมา)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + ",(select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=" + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code) + ") as " + _g.d.gl_chart_of_account._account_type + ",sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._journal_type + "=1" + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            // หายอดยกมา (ก่อนวันที่)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + ",(select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=" + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code) + ") as " + _g.d.gl_chart_of_account._account_type + ",sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + "<\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\'  and " + _g.d.gl_journal_detail._journal_type + "<>1" + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            // หายอดสะสม (ระหว่างวันที่)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + ",(select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_chart_of_account._code) + "=" + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code) + ") as " + _g.d.gl_chart_of_account._account_type + ",sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\' and " + _g.d.gl_journal_detail._journal_type + "<>1" + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            __myquery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataSet __getBalanceFirst = (DataSet)_getData[0];
            DataSet __getBalance = (DataSet)_getData[1];
            DataSet __getPeriod = (DataSet)_getData[2];
            // ประกอบร่าง
            __glWorkSheetSumNode(chartOfAccountTreeView.Nodes[0], __getBalanceFirst.Tables[0], __getBalance.Tables[0], __getPeriod.Tables[0], __result, allData, showDebitCredit);
            if (__result.Count > 0)
            {
                _glViewJounalDetailListType __prev = (_glViewJounalDetailListType)__result[__result.Count - 1];
                decimal __balancePeriod = __prev._debit - __prev._credit;
                decimal __balance = __prev._balanceDebit - __prev._balanceCredit;
                _glViewJounalDetailListType __totalLine1 = new _glViewJounalDetailListType();
                __totalLine1._lineType = _glViewJounalDetailListLineType.SubTotal;
                __totalLine1._accountCode = "";
                __totalLine1._accountName = "กำไร (ขาดทุน)";
                __totalLine1._prevDebit = 0;
                __totalLine1._prevCredit = 0;
                __totalLine1._debit = 0;
                __totalLine1._credit = 0;
                __totalLine1._balanceDebit = 0;
                __totalLine1._balanceCredit = 0;
                if (__prev._debit > __prev._credit)
                {
                    __totalLine1._credit = __balancePeriod;
                }
                else
                {
                    __totalLine1._debit = __balancePeriod * -1;
                }
                if (__prev._balanceDebit > __prev._balanceCredit)
                {
                    __totalLine1._balanceCredit = __balance;
                }
                else
                {
                    __totalLine1._balanceDebit = __balance * -1;
                }
                __totalLine1._accountLevel = 0;
                __totalLine1._accountStatus = 0;
                __totalLine1._show = true;
                __result.Add(__totalLine1);
                //
                _glViewJounalDetailListType __totalLine2 = new _glViewJounalDetailListType();
                __totalLine2._lineType = _glViewJounalDetailListLineType.SubTotal;
                __totalLine2._accountCode = "";
                __totalLine2._accountName = "Balance";
                __totalLine2._prevDebit = 0;
                __totalLine2._prevCredit = 0;
                __totalLine2._debit = __prev._debit + __totalLine1._debit;
                __totalLine2._credit = __prev._credit + __totalLine1._credit;
                __totalLine2._balanceDebit = __prev._balanceDebit + __totalLine1._balanceDebit;
                __totalLine2._balanceCredit = __prev._balanceCredit + __totalLine1._balanceCredit;
                __totalLine2._accountLevel = 0;
                __totalLine2._accountStatus = 0;
                __totalLine2._show = true;
                __result.Add(__totalLine2);
            }
            return __result;
        }

        private _accountSumType __glWorkSheetSumNode(TreeNode getNode, DataTable tableFirstBalance, DataTable tablePrevPeriod, DataTable tableThisPeriod, ArrayList result, bool allData, bool showDebitCredit)
        {
            int __lastDetailLine = -1;
            _accountSumType __result = new _accountSumType();
            _accountNode __getAccount = (_accountNode)getNode.Tag;
            //
            _glViewJounalDetailListType __detailLine = new _glViewJounalDetailListType();
            try
            {
                DataRow[] __getRowFirstBalance = tableFirstBalance.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                __detailLine._accountType = (__getRowFirstBalance.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getRowFirstBalance[0].ItemArray[1].ToString());
                __detailLine._prevDebit = (__getRowFirstBalance.Length == 0) ? 0 : (decimal)Double.Parse(__getRowFirstBalance[0].ItemArray[2].ToString());
                __detailLine._prevCredit = (__getRowFirstBalance.Length == 0) ? 0 : (decimal)Double.Parse(__getRowFirstBalance[0].ItemArray[3].ToString());
            }
            catch
            {
            }
            try
            {
                DataRow[] __getRowPrevPeriod = tablePrevPeriod.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                if (__detailLine._accountType == 0)
                {
                    __detailLine._accountType = (__getRowPrevPeriod.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getRowPrevPeriod[0].ItemArray[1].ToString());
                }
                __detailLine._prevDebit += (__getRowPrevPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowPrevPeriod[0].ItemArray[2].ToString());
                __detailLine._prevCredit += (__getRowPrevPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowPrevPeriod[0].ItemArray[3].ToString());
            }
            catch
            {
            }
            try
            {
                DataRow[] __getRowThisPeriod = tableThisPeriod.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                if (__detailLine._accountType == 0)
                {
                    __detailLine._accountType = (__getRowThisPeriod.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getRowThisPeriod[0].ItemArray[1].ToString());
                }
                __detailLine._prevDebit += (__getRowThisPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[2].ToString());
                __detailLine._prevCredit += (__getRowThisPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[3].ToString());
            }
            catch
            {
            }

            /*
            if (__detailLine._accountType == 3 || __detailLine._accountType == 4)
            {
                __detailLine._debit = __detailLine._prevDebit;
                __detailLine._credit = __detailLine._prevCredit;
                __detailLine._prevDebit = 0;
                __detailLine._prevCredit = 0;
            }

            __detailLine._balanceDebit = __detailLine._prevDebit + __detailLine._debit;
            __detailLine._balanceCredit = __detailLine._prevCredit + __detailLine._credit;            
            */

            // toe
            // กำไรขาดทุน เอาแต่ 3=รายได้ 4=ค่าใช้จ่าย
            if (__detailLine._accountType == 3 || __detailLine._accountType == 4)
            {
                __detailLine._debit = __detailLine._prevDebit;
                __detailLine._credit = __detailLine._prevCredit;
                //__detailLine._prevDebit = 0;
                //__detailLine._prevCredit = 0;
            }
            else
            {
                // งบดุล
                __detailLine._balanceDebit = __detailLine._prevDebit;
                __detailLine._balanceCredit = __detailLine._prevCredit;
                //__detailLine._prevDebit = 0;
                //__detailLine._prevCredit = 0;

            }

            __detailLine._prevDebit = __detailLine._balanceDebit + __detailLine._debit;
            __detailLine._prevCredit = __detailLine._balanceCredit + __detailLine._credit;



            // ปรับข้างรายการ
            if (showDebitCredit == false)
            {
                // งบทดลอง
                decimal __calcBalance = __detailLine._prevDebit - __detailLine._prevCredit;
                if (__calcBalance >= 0)
                {
                    __detailLine._prevDebit = __calcBalance;
                    __detailLine._prevCredit = 0;
                }
                else
                {
                    __detailLine._prevCredit = __calcBalance * -1;
                    __detailLine._prevDebit = 0;
                }
                __calcBalance = __detailLine._debit - __detailLine._credit;
                if (__calcBalance >= 0)
                {
                    __detailLine._debit = __calcBalance;
                    __detailLine._credit = 0;
                }
                else
                {
                    __detailLine._credit = __calcBalance * -1;
                    __detailLine._debit = 0;
                }
                // งบดุล
                __calcBalance = __detailLine._balanceDebit - __detailLine._balanceCredit;
                if (__calcBalance >= 0)
                {
                    __detailLine._balanceDebit = __calcBalance;
                    __detailLine._balanceCredit = 0;
                }
                else
                {
                    __detailLine._balanceCredit = __calcBalance * -1;
                    __detailLine._balanceDebit = 0;
                }
            }
            if (allData || __detailLine._prevDebit != 0 || __detailLine._prevCredit != 0 || __detailLine._debit != 0 || __detailLine._credit != 0 || __detailLine._balanceDebit != 0 || __detailLine._balanceCredit != 0 || __getAccount._accountStatus == 1)
            {
                __detailLine._lineType = _glViewJounalDetailListLineType.Detail;
                __detailLine._accountCode = __getAccount._code;
                __detailLine._accountName = __getAccount._name_1;
                __detailLine._accountLevel = getNode.Level;
                __detailLine._accountStatus = __getAccount._accountStatus;
                __detailLine._show = true;
                __lastDetailLine = result.Add(__detailLine);
            }
            //
            for (int __loop = 0; __loop < getNode.Nodes.Count; __loop++)
            {
                _accountSumType __getResult = __glWorkSheetSumNode(getNode.Nodes[__loop], tableFirstBalance, tablePrevPeriod, tableThisPeriod, result, allData, showDebitCredit);
                if (allData || __getResult._prevCredit != 0 || __getResult._prevDebit != 0 || __getResult._credit != 0 || __getResult._debit != 0 || __getResult._balanceCredit != 0 || __getResult._balanceDebit != 0)
                {
                    __result._count++;
                    // ยอดยกมา
                    __result._prevCredit += __getResult._prevCredit;
                    __result._prevDebit += __getResult._prevDebit;
                    // ยอดประจำงวด
                    __result._credit += __getResult._credit;
                    __result._debit += __getResult._debit;
                    // ยอดยกไป
                    __result._balanceCredit += __getResult._balanceCredit;
                    __result._balanceDebit += __getResult._balanceDebit;
                }
            }
            // Sub Total
            if (__getAccount._accountStatus == 1)
            {
                bool __nextStep = false;
                if (__result._prevCredit != 0 || __result._prevDebit != 0 || __result._credit != 0 || __result._debit != 0 || __result._balanceCredit != 0 || __result._balanceDebit != 0)
                {
                    __nextStep = true;
                }
                if (allData || __nextStep)
                {
                    _glViewJounalDetailListType __totalLine = new _glViewJounalDetailListType();
                    __totalLine._lineType = _glViewJounalDetailListLineType.SubTotal;
                    __totalLine._accountCode = "";
                    __totalLine._accountName = "รวม : " + __result._count.ToString() + " รายการ " + __getAccount._name_1 + " (" + __getAccount._code + ")";
                    __totalLine._prevDebit = __result._prevDebit;
                    __totalLine._prevCredit = __result._prevCredit;
                    __totalLine._debit = __result._debit;
                    __totalLine._credit = __result._credit;
                    __totalLine._balanceDebit = __result._balanceDebit;
                    __totalLine._balanceCredit = __result._balanceCredit;
                    __totalLine._accountLevel = getNode.Level;
                    __totalLine._accountStatus = __getAccount._accountStatus;
                    __totalLine._show = true;
                    result.Add(__totalLine);
                }
                // ลบออก ในกรณีที่ไม่มียอดรวม และไม่มีรายการเลย
                if (__result._count == 0 && allData == false && __lastDetailLine != -1)
                {
                    ((_glViewJounalDetailListType)result[__lastDetailLine])._show = false;
                }
            }
            __result._prevCredit += __detailLine._prevCredit;
            __result._prevDebit += __detailLine._prevDebit;
            __result._credit += __detailLine._credit;
            __result._debit += __detailLine._debit;
            __result._balanceCredit += __detailLine._balanceCredit;
            __result._balanceDebit += __detailLine._balanceDebit;
            return __result;
        }

        /// <summary>
        /// รายงานงบทดลอง แยกสาขา-แผนก
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="chartOfAccountTreeView"></param>
        /// <param name="allData"></param>
        /// <param name="showDebitCredit"></param>
        /// dataType : 0=ประจำงวด,1=สิ้นสุด
        /// <returns></returns>
        public ArrayList _glViewTrialBalanceBranchDepartment(DateTime dateBegin, DateTime dateEnd, MyLib._myTreeViewDragDrop chartOfAccountTreeView, bool allData, int isPass, List<_g.g._branchListStruct> branchList, int dataType)
        {
            string __isPassQuery = " and " + _g.d.gl_journal_detail._is_pass + ((isPass == 0) ? " in (0,1)" : "=1");
            ArrayList __result = new ArrayList();

            StringBuilder __myquery = new StringBuilder();
            string __where2 = "";
            string __where3 = "";
            if (dataType == 0)
            {
                __where2 = _g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\'";
                __where3 = _g.d.gl_journal_depart_list._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_depart_list._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\'";
            }
            else
            {
                __where2 = _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\'";
                __where3 = _g.d.gl_journal_depart_list._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\'";
            }
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // หายอดสะสม (ระหว่างวันที่) ทั้งหมด
            string __queryBalance = "select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + ",coalesce((select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=" + _g.d.gl_journal_detail._account_code + "),0) as " + _g.d.gl_chart_of_account._account_type + ",sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + __where2 + " and " + _g.d.gl_journal_detail._journal_type + "<>1 " + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code + " order by " + _g.d.gl_journal_detail._account_code;
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryBalance));
            // หายอดสะสมของแผนก
            string __queryDepartment = "select account_code, branch_code, department_code, COALESCE((select name_1 from erp_department_list where erp_department_list.code = department_code), 'ERR.' || department_code) as department_name,sum(allocate_amount) as allocate_amount from (select branch_code, department_code, account_code,case when debit = 0 then allocate_amount else allocate_amount * -1 end as allocate_amount from(SELECT coalesce((select branch_code from gl_journal_detail where gl_journal_detail.doc_no = gl_journal_depart_list.doc_no and gl_journal_detail.line_number = gl_journal_depart_list.line_number), 'ERR') as branch_code, coalesce((select account_code from gl_journal_detail where gl_journal_detail.doc_no = gl_journal_depart_list.doc_no and gl_journal_detail.line_number = gl_journal_depart_list.line_number), 'ERR') as account_code, coalesce((select debit from gl_journal_detail where gl_journal_detail.doc_no = gl_journal_depart_list.doc_no and gl_journal_detail.line_number = gl_journal_depart_list.line_number), 0) as debit,allocate_amount,department_code from gl_journal_depart_list where " + __where3 + ") as q1) as q2 group by account_code,branch_code,department_code order by account_code, branch_code, department_code";
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDepartment));
            for (int __loop = 0; __loop < branchList.Count; __loop++)
            {
                // หายอดสะสม (ระหว่างวันที่) แยกสาขา
                string __queryData = "select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + "," + "sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + __where2 + " and " + _g.d.gl_journal_detail._journal_type + "<>1 " + __isPassQuery + " and " + _g.d.gl_journal_detail._branch_code + "='" + branchList[__loop]._code.ToString() + "' group by " + _g.d.gl_journal_detail._account_code;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryData));
            }
            __myquery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataSet __getBalance = (DataSet)__getData[0];
            // ประกอบร่าง
            __glTrialBalanceBranchDepartmentSumNode(chartOfAccountTreeView.Nodes[0], __getBalance.Tables[0], __getData, __result, allData, branchList);
            return __result;
        }

        private _accountSumType __glTrialBalanceBranchDepartmentSumNode(TreeNode getNode, DataTable tableThisPeriod, ArrayList getData, ArrayList result, bool allData, List<_g.g._branchListStruct> branchList)
        {
            DataTable __departmentDataTable = ((DataSet)getData[1]).Tables[0];
            int __lastDetailLine = -1;
            _accountSumType __result = new _accountSumType();
            for (int __loop = 0; __loop < branchList.Count; __loop++)
            {
                __result._branch.Add(new _glViewJounalDetailListBranchType());
                for (int __loop2 = 0; __loop2 < branchList[__loop]._department.Count; __loop2++)
                {
                    __result._branch[__loop]._department.Add(new _glViewJounalDetailListBranchDepartmentType());
                }
            }
            _accountNode __getAccount = (_accountNode)getNode.Tag;
            //            
            _glViewJounalDetailListType __detailLine = new _glViewJounalDetailListType();
            if (tableThisPeriod.Columns.IndexOf(_g.d.gl_journal_detail._account_code) != -1)
            {
                DataRow[] __getRowThisPeriod = tableThisPeriod.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                __detailLine._accountType = (__getRowThisPeriod.Length == 0) ? 0 : (int)int.Parse(__getRowThisPeriod[0].ItemArray[1].ToString());
                __detailLine._amount = (__getRowThisPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[3].ToString()) - (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[2].ToString());
                // ดึงข้อมูลสาขา
                for (int __loop = 0; __loop < branchList.Count; __loop++)
                {
                    _glViewJounalDetailListBranchType __branch = new _glViewJounalDetailListBranchType();
                    __branch._branchCode = branchList[__loop]._code;

                    if (((DataSet)getData[__loop + 2]).Tables[0].Rows.Count > 0)
                    {
                        DataRow[] __data = ((DataSet)getData[__loop + 2]).Tables[0].Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                        __branch._amount = (__data.Length == 0) ? 0 : (decimal)Double.Parse(__data[0].ItemArray[2].ToString()) - (decimal)Double.Parse(__data[0].ItemArray[1].ToString());

                    }
                    else
                    {
                        __branch._amount = 0;
                    }
                    DataRow[] __dataDepartment = __departmentDataTable.Select("account_code=\'" + __getAccount._code + "\' and branch_code=\'" + __branch._branchCode + "\'");
                    //
                    _glViewJounalDetailListBranchDepartmentType __departmentCenter = new _glViewJounalDetailListBranchDepartmentType();
                    __departmentCenter._departmentCode = "CENTER";
                    __departmentCenter._departmentName = "CENTER";
                    __departmentCenter._allocateAmount = __branch._amount;
                    __branch._department.Add(__departmentCenter);
                    //
                    for (int __loop2 = 0; __loop2 < __dataDepartment.Length; __loop2++)
                    {
                        _glViewJounalDetailListBranchDepartmentType __department = new _glViewJounalDetailListBranchDepartmentType();
                        __department._departmentCode = __dataDepartment[__loop2][2].ToString();
                        __department._departmentName = __dataDepartment[__loop2][3].ToString();
                        __department._allocateAmount = (decimal)Double.Parse(__dataDepartment[0].ItemArray[4].ToString());
                        __branch._department.Add(__department);
                        //
                        __branch._department[0]._allocateAmount -= __department._allocateAmount;
                    }
                    __detailLine._branch.Add(__branch);
                }
            }
            //
            if (allData || __detailLine._amount != 0 || __getAccount._accountStatus == 1)
            {
                __detailLine._lineType = _glViewJounalDetailListLineType.Detail;
                __detailLine._accountCode = __getAccount._code;
                __detailLine._accountName = __getAccount._name_1;
                __detailLine._accountLevel = getNode.Level;
                __detailLine._accountStatus = __getAccount._accountStatus;
                __detailLine._show = true;
                __lastDetailLine = result.Add(__detailLine);
            }
            //
            for (int __loop = 0; __loop < getNode.Nodes.Count; __loop++)
            {
                _accountSumType __getResult = __glTrialBalanceBranchDepartmentSumNode(getNode.Nodes[__loop], tableThisPeriod, getData, result, allData, branchList);
                if (allData || __getResult._amount != 0)
                {
                    __result._count++;
                    __result._amount += __getResult._amount;
                    for (int __loop2 = 0; __loop2 < branchList.Count; __loop2++)
                    {
                        __result._branch[__loop2]._amount += __getResult._branch[__loop2]._amount;
                        for (int __loop3 = 0; __loop3 < __result._branch[__loop2]._department.Count; __loop3++)
                        {
                            __result._branch[__loop2]._department[__loop3]._allocateAmount += __getResult._branch[__loop2]._department[__loop3]._allocateAmount;
                        }
                    }
                }
            }
            // Sub Total
            if (__getAccount._accountStatus == 1)
            {
                bool __nextStep = false;
                if (__result._amount != 0)
                {
                    __nextStep = true;
                }
                if (allData || __nextStep)
                {
                    _glViewJounalDetailListType __totalLine = new _glViewJounalDetailListType();
                    __totalLine._lineType = _glViewJounalDetailListLineType.SubTotal;
                    __totalLine._accountCode = "";
                    __totalLine._accountName = "รวม : " + __result._count.ToString() + " รายการ " + __getAccount._name_1 + " (" + __getAccount._code + ")";
                    __totalLine._amount = __result._amount;
                    __totalLine._accountLevel = getNode.Level;
                    __totalLine._accountStatus = __getAccount._accountStatus;
                    __totalLine._show = true;
                    for (int __loop = 0; __loop < branchList.Count; __loop++)
                    {
                        DataTable __data = ((DataSet)getData[__loop + 1]).Tables[0];
                        _glViewJounalDetailListBranchType __branchTotal = new _glViewJounalDetailListBranchType();
                        __branchTotal._branchCode = branchList[__loop]._code;
                        __branchTotal._amount = __result._branch[__loop]._amount;
                        for (int __loop2 = 0; __loop2 < branchList[__loop]._department.Count; __loop2++)
                        {
                            _glViewJounalDetailListBranchDepartmentType __departmentTotal = new _glViewJounalDetailListBranchDepartmentType();
                            __departmentTotal._allocateAmount = __result._branch[__loop]._department[__loop2]._allocateAmount;
                            __branchTotal._department.Add(__departmentTotal);
                        }
                        __totalLine._branch.Add(__branchTotal);
                    }
                    result.Add(__totalLine);
                }
                // ลบออก ในกรณีที่ไม่มียอดรวม และไม่มีรายการเลย
                if (__result._count == 0 && allData == false && __lastDetailLine != -1)
                {
                    ((_glViewJounalDetailListType)result[__lastDetailLine])._show = false;
                }
            }
            __result._amount += __detailLine._amount;
            for (int __loop = 0; __loop < branchList.Count; __loop++)
            {
                __result._branch[__loop]._amount += __detailLine._branch[__loop]._amount;
                for (int __loop2 = 0; __loop2 < __detailLine._branch[__loop]._department.Count; __loop2++)
                {
                    __result._branch[__loop]._department[__loop2]._allocateAmount += __detailLine._branch[__loop]._department[__loop2]._allocateAmount;
                }
            }
            return __result;
        }

        /// <summary>
        /// รายงานงบทดลอง แยกสาขา
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="chartOfAccountTreeView"></param>
        /// <param name="allData"></param>
        /// <param name="showDebitCredit"></param>
        /// dataType,0=ประจำงวด,1=สิ้นสุด ณ
        /// <returns></returns>
        public ArrayList _glViewTrialBalanceBranch(DateTime dateBegin, DateTime dateEnd, MyLib._myTreeViewDragDrop chartOfAccountTreeView, bool allData, bool showDebitCredit, int isPass, List<_g.g._branchListStruct> branchList, int dataType)
        {
            string __isPassQuery = " and " + _g.d.gl_journal_detail._is_pass + ((isPass == 0) ? " in (0,1)" : "=1");
            ArrayList __result = new ArrayList();

            string __where2 = "";
            if (dataType == 0)
            {
                __where2 = _g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\'";
            }
            else
            {
                __where2 = _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\'";
            }
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // หายอดสะสม (ระหว่างวันที่) ทั้งหมด
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + ",coalesce((select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=" + _g.d.gl_journal_detail._account_code + "),0) as " + _g.d.gl_chart_of_account._account_type + ",sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + __where2 + " and " + _g.d.gl_journal_detail._journal_type + "<>1 " + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            for (int __loop = 0; __loop < branchList.Count; __loop++)
            {
                string __where;
                if (branchList[__loop]._code.ToString().IndexOf("XERR.") != -1)
                {
                    __where = _g.d.gl_journal_detail._branch_code + " = '' or " + _g.d.gl_journal_detail._branch_code + " is null";
                }
                else
                {
                    __where = _g.d.gl_journal_detail._branch_code + " = '" + branchList[__loop]._code.ToString() + "'";
                }
                // หายอดสะสม (ระหว่างวันที่) แยกสาขา
                string __queryData = "select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + ",coalesce((select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._code + "=" + _g.d.gl_journal_detail._account_code + "),0) as " + _g.d.gl_chart_of_account._account_type + "," + "sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + __where2 + " and " + _g.d.gl_journal_detail._journal_type + "<>1 " + __isPassQuery + " and " + __where + " group by " + _g.d.gl_journal_detail._account_code + "," + _g.d.gl_chart_of_account._account_type + " order by " + _g.d.gl_journal_detail._account_code;
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryData));
            }
            __myquery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataSet __getBalance = (DataSet)__getData[0];
            // ประกอบร่าง
            __glTrialBalanceBranchSumNode(chartOfAccountTreeView.Nodes[0], __getBalance.Tables[0], __getData, __result, allData, showDebitCredit, branchList);
            return __result;
        }

        private _accountSumType __glTrialBalanceBranchSumNode(TreeNode getNode, DataTable tableThisPeriod, ArrayList getData, ArrayList result, bool allData, bool showDebitCredit, List<_g.g._branchListStruct> branchList)
        {
            int __lastDetailLine = -1;
            _accountSumType __result = new _accountSumType();
            for (int __loop = 0; __loop < branchList.Count; __loop++)
            {
                __result._branch.Add(new _glViewJounalDetailListBranchType());
            }
            _accountNode __getAccount = (_accountNode)getNode.Tag;
            //            
            _glViewJounalDetailListType __detailLine = new _glViewJounalDetailListType();
            if (tableThisPeriod.Columns.IndexOf(_g.d.gl_journal_detail._account_code) != -1)
            {
                DataRow[] __getRowThisPeriod = tableThisPeriod.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                __detailLine._accountType = (__getRowThisPeriod.Length == 0) ? 0 : (int)int.Parse(__getRowThisPeriod[0].ItemArray[1].ToString());
                __detailLine._debit = (__getRowThisPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[2].ToString());
                __detailLine._credit = (__getRowThisPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[3].ToString());
                // ดึงข้อมูลสาขา
                for (int __loop = 0; __loop < branchList.Count; __loop++)
                {
                    if (((DataSet)getData[__loop + 1]).Tables[0].Rows.Count > 0)
                    {
                        DataRow[] __data = ((DataSet)getData[__loop + 1]).Tables[0].Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                        _glViewJounalDetailListBranchType __branch = new _glViewJounalDetailListBranchType();
                        __branch._branchCode = branchList[__loop]._code;
                        __branch._debit = (__data.Length == 0) ? 0 : (decimal)Double.Parse(__data[0].ItemArray[2].ToString());
                        __branch._credit = (__data.Length == 0) ? 0 : (decimal)Double.Parse(__data[0].ItemArray[3].ToString());
                        __detailLine._branch.Add(__branch);
                    }
                    else
                    {
                        _glViewJounalDetailListBranchType __branch = new _glViewJounalDetailListBranchType();
                        __branch._branchCode = branchList[__loop]._code;
                        __branch._debit = 0;
                        __branch._credit = 0;
                        __detailLine._branch.Add(__branch);

                    }
                }
            }
            //
            if (showDebitCredit == false)
            {
                // ประจำงวด
                decimal __calcBalance = __detailLine._debit - __detailLine._credit;
                if (__calcBalance >= 0)
                {
                    __detailLine._debit = __calcBalance;
                    __detailLine._credit = 0;
                }
                else
                {
                    __detailLine._credit = __calcBalance * -1;
                    __detailLine._debit = 0;
                }
                //
                for (int __loop = 0; __loop < branchList.Count; __loop++)
                {
                    decimal __calcBalance2 = __detailLine._branch[__loop]._debit - __detailLine._branch[__loop]._credit;
                    if (__calcBalance2 >= 0)
                    {
                        __detailLine._branch[__loop]._debit = __calcBalance2;
                        __detailLine._branch[__loop]._credit = 0;
                    }
                    else
                    {
                        __detailLine._branch[__loop]._credit = __calcBalance2 * -1;
                        __detailLine._branch[__loop]._debit = 0;
                    }
                }
            }
            if (allData || __detailLine._debit != 0 || __detailLine._credit != 0 || __getAccount._accountStatus == 1)
            {
                __detailLine._lineType = _glViewJounalDetailListLineType.Detail;
                __detailLine._accountCode = __getAccount._code;
                __detailLine._accountName = __getAccount._name_1;
                __detailLine._accountLevel = getNode.Level;
                __detailLine._accountStatus = __getAccount._accountStatus;
                __detailLine._show = true;
                __lastDetailLine = result.Add(__detailLine);
            }
            //
            for (int __loop = 0; __loop < getNode.Nodes.Count; __loop++)
            {
                _accountSumType __getResult = __glTrialBalanceBranchSumNode(getNode.Nodes[__loop], tableThisPeriod, getData, result, allData, showDebitCredit, branchList);
                if (allData || __getResult._credit != 0 || __getResult._debit != 0)
                {
                    __result._count++;
                    __result._credit += __getResult._credit;
                    __result._debit += __getResult._debit;
                    for (int __loop2 = 0; __loop2 < branchList.Count; __loop2++)
                    {
                        __result._branch[__loop2]._debit += __getResult._branch[__loop2]._debit;
                        __result._branch[__loop2]._credit += __getResult._branch[__loop2]._credit;
                    }
                }
            }
            // Sub Total
            if (__getAccount._accountStatus == 1)
            {
                bool __nextStep = false;
                if (__result._credit != 0 || __result._debit != 0)
                {
                    __nextStep = true;
                }
                if (allData || __nextStep)
                {
                    _glViewJounalDetailListType __totalLine = new _glViewJounalDetailListType();
                    __totalLine._lineType = _glViewJounalDetailListLineType.SubTotal;
                    __totalLine._accountCode = "";
                    __totalLine._accountName = "รวม : " + __result._count.ToString() + " รายการ " + __getAccount._name_1 + " (" + __getAccount._code + ")";
                    __totalLine._debit = __result._debit;
                    __totalLine._credit = __result._credit;
                    __totalLine._accountLevel = getNode.Level;
                    __totalLine._accountStatus = __getAccount._accountStatus;
                    __totalLine._accountType = __getAccount._accountType;
                    __totalLine._show = true;
                    for (int __loop = 0; __loop < branchList.Count; __loop++)
                    {
                        DataTable __data = ((DataSet)getData[__loop + 1]).Tables[0];
                        _glViewJounalDetailListBranchType __branch = new _glViewJounalDetailListBranchType();
                        __branch._branchCode = branchList[__loop]._code;
                        __branch._debit = __result._branch[__loop]._debit;
                        __branch._credit = __result._branch[__loop]._credit;
                        __totalLine._branch.Add(__branch);
                    }
                    result.Add(__totalLine);
                }
                // ลบออก ในกรณีที่ไม่มียอดรวม และไม่มีรายการเลย
                if (__result._count == 0 && allData == false && __lastDetailLine != -1)
                {
                    ((_glViewJounalDetailListType)result[__lastDetailLine])._show = false;
                }
            }
            __result._credit += __detailLine._credit;
            __result._debit += __detailLine._debit;
            for (int __loop = 0; __loop < branchList.Count; __loop++)
            {
                __result._branch[__loop]._credit += __detailLine._branch[__loop]._credit;
                __result._branch[__loop]._debit += __detailLine._branch[__loop]._debit;
            }
            return __result;
        }

        /// <summary>
        /// รายงานงบทดลอง
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="chartOfAccountTreeView"></param>
        /// <param name="allData"></param>
        /// <param name="showDebitCredit"></param>
        /// <returns></returns>
        public ArrayList _glViewTrialBalance(DateTime dateBegin, DateTime dateEnd, MyLib._myTreeViewDragDrop chartOfAccountTreeView, bool allData, bool showDebitCredit, int isPass)
        {
            string __isPassQuery = " and " + _g.d.gl_journal_detail._is_pass + ((isPass == 0) ? " in (0,1)" : "=1");
            ArrayList __result = new ArrayList();

            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // หายอดยกมา (บันทึกยอดยกมา)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + "," + "sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._journal_type + "=1 " + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            // หายอดยกมา (ก่อนวันที่)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + "," + "sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + "<\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._journal_type + "<>1 " + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            // หายอดสะสม (ระหว่างวันที่)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code, true) + "," + "sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\' and " + _g.d.gl_journal_detail._journal_type + "<>1 " + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            //
            // กำไรขาดทุน (บันทึกยอดยกมา)
            string __query = "select sum(" + _g.d.gl_journal_detail._debit + ") - sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + " < \'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' " + __isPassQuery + " and (select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code + ") in (3,4)";
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
            // กำไรขาดทุน (ระหว่างวันที่)
            __query = "select sum(" + _g.d.gl_journal_detail._debit + ") - sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + " >=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\' and " + _g.d.gl_journal_detail._journal_type + "<>1 " + __isPassQuery + " and (select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code + ") in (3,4)";
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
            // กำไรขาดทุน (ยกไป)
            __query = "select sum(" + _g.d.gl_journal_detail._debit + ") - sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\' " + __isPassQuery + " and (select " + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._account_code + ") in (3,4)";
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
            //
            __myquery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            ArrayList __getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataSet __getBalanceFirst = (DataSet)__getData[0];
            DataSet __getBalance = (DataSet)__getData[1];
            DataSet __getPeriod = (DataSet)__getData[2];
            DataSet __getProfitAndLostBefore = (DataSet)__getData[3];
            DataSet __getProfitAndLostPeriod = (DataSet)__getData[4];
            DataSet __getProfitAndLostEnd = (DataSet)__getData[5];
            // ประกอบร่าง
            __glTrialBalanceSumNode(chartOfAccountTreeView.Nodes[0], __getBalanceFirst.Tables[0], __getBalance.Tables[0], __getPeriod.Tables[0], __result, allData, showDebitCredit);
            if (__result.Count > 0)
            {
                decimal __profitAndLostBefore = MyLib._myGlobal._decimalPhase(__getProfitAndLostBefore.Tables[0].Rows[0][_g.d.gl_journal_detail._credit].ToString());
                decimal __profitAndLostPeriod = MyLib._myGlobal._decimalPhase(__getProfitAndLostPeriod.Tables[0].Rows[0][_g.d.gl_journal_detail._credit].ToString());
                decimal __profitAndLostEnd = MyLib._myGlobal._decimalPhase(__getProfitAndLostEnd.Tables[0].Rows[0][_g.d.gl_journal_detail._credit].ToString());
                _glViewJounalDetailListType __totalLine1 = new _glViewJounalDetailListType();
                __totalLine1._lineType = _glViewJounalDetailListLineType.SubTotal;
                __totalLine1._accountCode = "";
                __totalLine1._accountName = "กำไร (ขาดทุน)";
                __totalLine1._prevDebit = 0;
                __totalLine1._prevCredit = 0;
                __totalLine1._debit = 0;
                __totalLine1._credit = 0;
                __totalLine1._balanceDebit = 0;
                __totalLine1._balanceCredit = 0;
                //
                if (__profitAndLostBefore > 0)
                {
                    __totalLine1._prevCredit = __profitAndLostBefore;
                }
                else
                {
                    __totalLine1._prevDebit = __profitAndLostBefore * -1;
                }
                if (__profitAndLostPeriod > 0)
                {
                    __totalLine1._credit = __profitAndLostPeriod;
                }
                else
                {
                    __totalLine1._debit = __profitAndLostPeriod * -1;
                }
                if (__profitAndLostEnd > 0)
                {
                    __totalLine1._balanceCredit = __profitAndLostEnd;
                }
                else
                {
                    __totalLine1._balanceDebit = __profitAndLostEnd * -1;
                }
                //
                __totalLine1._accountLevel = 0;
                __totalLine1._accountStatus = 0;
                __totalLine1._show = true;
                __result.Add(__totalLine1);
            }
            return __result;
        }

        private _accountSumType __glTrialBalanceSumNode(TreeNode getNode, DataTable tableFirstBalance, DataTable tablePrevPeriod, DataTable tableThisPeriod, ArrayList result, bool allData, bool showDebitCredit)
        {
            int __lastDetailLine = -1;
            _accountSumType __result = new _accountSumType();
            _accountNode __getAccount = (_accountNode)getNode.Tag;
            //
            _glViewJounalDetailListType __detailLine = new _glViewJounalDetailListType();
            try
            {
                if (tableFirstBalance.Columns.IndexOf(_g.d.gl_journal_detail._account_code) != -1)
                {
                    DataRow[] __getRowFirstBalance = tableFirstBalance.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                    __detailLine._prevDebit = (__getRowFirstBalance.Length == 0) ? 0 : (decimal)Double.Parse(__getRowFirstBalance[0].ItemArray[1].ToString());
                    __detailLine._prevCredit = (__getRowFirstBalance.Length == 0) ? 0 : (decimal)Double.Parse(__getRowFirstBalance[0].ItemArray[2].ToString());
                }
            }
            catch
            {
            }
            try
            {
                if (tablePrevPeriod.Columns.IndexOf(_g.d.gl_journal_detail._account_code) != -1)
                {
                    DataRow[] __getRowPrevPeriod = tablePrevPeriod.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                    __detailLine._prevDebit += (__getRowPrevPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowPrevPeriod[0].ItemArray[1].ToString());
                    __detailLine._prevCredit += (__getRowPrevPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowPrevPeriod[0].ItemArray[2].ToString());
                }
            }
            catch
            {
            }
            try
            {
                if (tableThisPeriod.Columns.IndexOf(_g.d.gl_journal_detail._account_code) != -1)
                {
                    DataRow[] __getRowThisPeriod = tableThisPeriod.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
                    __detailLine._debit = (__getRowThisPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[1].ToString());
                    __detailLine._credit = (__getRowThisPeriod.Length == 0) ? 0 : (decimal)Double.Parse(__getRowThisPeriod[0].ItemArray[2].ToString());
                }
            }
            catch
            {
            }
            //
            __detailLine._balanceDebit = __detailLine._prevDebit + __detailLine._debit;
            __detailLine._balanceCredit = __detailLine._prevCredit + __detailLine._credit;
            // ยอดยกมา
            decimal __calcBalance = __detailLine._prevDebit - __detailLine._prevCredit;
            if (__calcBalance >= 0)
            {
                __detailLine._prevDebit = __calcBalance;
                __detailLine._prevCredit = 0;
            }
            else
            {
                __detailLine._prevCredit = __calcBalance * -1;
                __detailLine._prevDebit = 0;
            }
            if (showDebitCredit == false)
            {
                // ประจำงวด
                __calcBalance = __detailLine._debit - __detailLine._credit;
                if (__calcBalance >= 0)
                {
                    __detailLine._debit = __calcBalance;
                    __detailLine._credit = 0;
                }
                else
                {
                    __detailLine._credit = __calcBalance * -1;
                    __detailLine._debit = 0;
                }
            }
            // ยอดยกไป
            __calcBalance = __detailLine._balanceDebit - __detailLine._balanceCredit;
            if (__calcBalance >= 0)
            {
                __detailLine._balanceDebit = __calcBalance;
                __detailLine._balanceCredit = 0;
            }
            else
            {
                __detailLine._balanceCredit = __calcBalance * -1;
                __detailLine._balanceDebit = 0;
            }
            if (allData || __detailLine._prevDebit != 0 || __detailLine._prevCredit != 0 || __detailLine._debit != 0 || __detailLine._credit != 0 || __detailLine._balanceDebit != 0 || __detailLine._balanceCredit != 0 || __getAccount._accountStatus == 1)
            {
                __detailLine._lineType = _glViewJounalDetailListLineType.Detail;
                __detailLine._accountCode = __getAccount._code;
                __detailLine._accountName = __getAccount._name_1;
                __detailLine._accountLevel = getNode.Level;
                __detailLine._accountStatus = __getAccount._accountStatus;
                __detailLine._show = true;
                __lastDetailLine = result.Add(__detailLine);
            }
            //
            for (int __loop = 0; __loop < getNode.Nodes.Count; __loop++)
            {
                _accountSumType __getResult = __glTrialBalanceSumNode(getNode.Nodes[__loop], tableFirstBalance, tablePrevPeriod, tableThisPeriod, result, allData, showDebitCredit);
                if (allData || __getResult._prevCredit != 0 || __getResult._prevDebit != 0 || __getResult._credit != 0 || __getResult._debit != 0 || __getResult._balanceCredit != 0 || __getResult._balanceDebit != 0)
                {
                    __result._count++;
                    // ยอดยกมา
                    __result._prevCredit += __getResult._prevCredit;
                    __result._prevDebit += __getResult._prevDebit;
                    // ยอดประจำงวด
                    __result._credit += __getResult._credit;
                    __result._debit += __getResult._debit;
                    // ยอดยกไป
                    __result._balanceCredit += __getResult._balanceCredit;
                    __result._balanceDebit += __getResult._balanceDebit;
                }
            }
            // Sub Total
            if (__getAccount._accountStatus == 1)
            {
                bool __nextStep = false;
                if (__result._prevCredit != 0 || __result._prevDebit != 0 || __result._credit != 0 || __result._debit != 0 || __result._balanceCredit != 0 || __result._balanceDebit != 0)
                {
                    __nextStep = true;
                }
                if (allData || __nextStep)
                {
                    _glViewJounalDetailListType __totalLine = new _glViewJounalDetailListType();
                    __totalLine._lineType = _glViewJounalDetailListLineType.SubTotal;
                    __totalLine._accountCode = "";
                    __totalLine._accountName = "รวม : " + __result._count.ToString() + " รายการ " + __getAccount._name_1 + " (" + __getAccount._code + ")";
                    __totalLine._prevDebit = __result._prevDebit;
                    __totalLine._prevCredit = __result._prevCredit;
                    __totalLine._debit = __result._debit;
                    __totalLine._credit = __result._credit;
                    __totalLine._balanceDebit = __result._balanceDebit;
                    __totalLine._balanceCredit = __result._balanceCredit;
                    __totalLine._accountLevel = getNode.Level;
                    __totalLine._accountStatus = __getAccount._accountStatus;
                    __totalLine._show = true;
                    result.Add(__totalLine);
                }
                // ลบออก ในกรณีที่ไม่มียอดรวม และไม่มีรายการเลย
                if (__result._count == 0 && allData == false && __lastDetailLine != -1)
                {
                    ((_glViewJounalDetailListType)result[__lastDetailLine])._show = false;
                }
            }
            __result._prevCredit += __detailLine._prevCredit;
            __result._prevDebit += __detailLine._prevDebit;
            __result._credit += __detailLine._credit;
            __result._debit += __detailLine._debit;
            __result._balanceCredit += __detailLine._balanceCredit;
            __result._balanceDebit += __detailLine._balanceDebit;
            return __result;
        }

        /// <summary>
        /// ประมวลผล รายงานบัญชีแยกประเภทแบบสรุป
        /// </summary>
        /// <param name="dateEnd">วันที่สิ้นสุด</param>
        /// <returns></returns>
        public ArrayList _glViewJounalDetailSum(DateTime dateEnd, MyLib._myTreeViewDragDrop chartOfAccountTreeView, bool allData, bool showDebitCredit, int isPass)
        {
            string __isPassQuery = " and " + _g.d.gl_journal_detail._is_pass + ((isPass == 0) ? " in (0,1)" : "=1");
            ArrayList __result = new ArrayList();

            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_journal_detail._account_code + ",sum(" + _g.d.gl_journal_detail._debit + ") as " + _g.d.gl_journal_detail._debit + ",sum(" + _g.d.gl_journal_detail._credit + ") as " + _g.d.gl_journal_detail._credit +
                    " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\'" + __isPassQuery + " group by " + _g.d.gl_journal_detail._account_code));
            __myquery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataSet __getDetail = (DataSet)_getData[0];
            // ประกอบร่าง
            __glViewJounalDetailSumNode(chartOfAccountTreeView.Nodes[0], __getDetail.Tables[0], __result, allData, showDebitCredit);
            return __result;
        }

        private _accountSumType __glViewJounalDetailSumNode(TreeNode getNode, DataTable table, ArrayList result, bool allData, bool showDebitCredit)
        {
            int __lastDetailLine = -1;
            _accountSumType __result = new _accountSumType();
            _accountNode __getAccount = (_accountNode)getNode.Tag;
            //
            DataRow[] __getRow = table.Select(_g.d.gl_journal_detail._account_code + "=\'" + __getAccount._code + "\'");
            //
            _glViewJounalDetailListType __detailLine = new _glViewJounalDetailListType();
            __detailLine._debit = (__getRow.Length == 0) ? 0 : (decimal)Double.Parse(__getRow[0].ItemArray[1].ToString());
            __detailLine._credit = (__getRow.Length == 0) ? 0 : (decimal)Double.Parse(__getRow[0].ItemArray[2].ToString());
            if (showDebitCredit == false)
            {
                decimal __calcBalance = __detailLine._debit - __detailLine._credit;
                if (__calcBalance >= 0)
                {
                    __detailLine._debit = __calcBalance;
                    __detailLine._credit = 0;
                }
                else
                {
                    __detailLine._credit = __calcBalance * -1;
                    __detailLine._debit = 0;
                }
            }
            if (allData || __detailLine._debit != 0 || __detailLine._credit != 0 || __getAccount._accountStatus == 1)
            {
                __detailLine._lineType = _glViewJounalDetailListLineType.Detail;
                __detailLine._accountCode = __getAccount._code;
                __detailLine._accountName = __getAccount._name_1;
                __detailLine._accountLevel = getNode.Level;
                __detailLine._accountStatus = __getAccount._accountStatus;
                __detailLine._show = true;
                __lastDetailLine = result.Add(__detailLine);
            }
            //
            for (int __loop = 0; __loop < getNode.Nodes.Count; __loop++)
            {
                _accountSumType __getResult = __glViewJounalDetailSumNode(getNode.Nodes[__loop], table, result, allData, showDebitCredit);
                if (allData || __getResult._credit != 0 || __getResult._debit != 0)
                {
                    __result._count++;
                    __result._credit += __getResult._credit;
                    __result._debit += __getResult._debit;
                }
            }
            // Sub Total
            if (__getAccount._accountStatus == 1)
            {
                bool __nextStep = false;
                if (__result._debit != 0 || __result._credit != 0)
                {
                    __nextStep = true;
                }
                if (allData || __nextStep)
                {
                    _glViewJounalDetailListType __totalLine = new _glViewJounalDetailListType();
                    __totalLine._lineType = _glViewJounalDetailListLineType.SubTotal;
                    __totalLine._accountCode = "";
                    __totalLine._accountName = "รวม : " + __result._count.ToString() + " รายการ " + __getAccount._name_1 + " (" + __getAccount._code + ")";
                    __totalLine._debit = __result._debit;
                    __totalLine._credit = __result._credit;
                    __totalLine._accountLevel = getNode.Level;
                    __totalLine._accountStatus = __getAccount._accountStatus;
                    __totalLine._show = true;
                    result.Add(__totalLine);
                }
                // ลบออก ในกรณีที่ไม่มียอดรวม และไม่มีรายการเลย
                if (__result._count == 0 && allData == false && __lastDetailLine != -1)
                {
                    ((_glViewJounalDetailListType)result[__lastDetailLine])._show = false;
                }
            }
            __result._credit += __detailLine._credit;
            __result._debit += __detailLine._debit;
            return __result;
        }

        /// <summary>
        /// ประมวลผล รายงานบัญชีแยกประเภท (เรียกทีละบัญชี) ส่วนยอดรวมสิ้นวัน
        /// </summary>
        /// <returns></returns>
        protected _glViewJounalDetailListType _addSubTotal(string desc, decimal debit, decimal credit)
        {
            _glViewJounalDetailListType __subTotal = new _glViewJounalDetailListType();
            __subTotal._lineType = _glViewJounalDetailListLineType.SubTotal;
            __subTotal._desc = desc;
            __subTotal._debit = debit;
            __subTotal._credit = credit;
            return __subTotal;
        }

        protected _glViewJounalDetailListType _addDateTotal()
        {
            string __desc = (MyLib._myGlobal._language == MyLib._languageEnum.Thai) ? "รวม " + _countDetailPerDay.ToString() + " รายการ" : "Total " + _countDetailPerDay.ToString() + " Records";
            __desc = __desc + " " + MyLib._myGlobal._convertDateToString(_oldDate, false, true);
            return _addSubTotal(__desc, _sumDebit, _sumCredit);
        }

        /// <summary>
        /// ประมวลผล รายงานบัญชีแยกประเภท (เรียกทีละบัญชี)
        /// </summary>
        /// <param name="accountCode">เลขที่บัญชี</param>
        /// <param name="dateBegin">จากวันที่</param>
        /// <param name="dateEnd">ถึงวันที่</param>
        /// <returns></returns>
        public ArrayList _glViewJounalDetail(string accountCode, DateTime dateBegin, DateTime dateEnd, bool sumEndOfDate, int isPass)
        {
            return this._glViewJounalDetail(accountCode, dateBegin, dateEnd, sumEndOfDate, isPass, "");
        }

        public ArrayList _glViewJounalDetail(string accountCode, DateTime dateBegin, DateTime dateEnd, bool sumEndOfDate, int isPass, string extraWhere)
        {
            return this._glViewJounalDetail(accountCode, dateBegin, dateEnd, sumEndOfDate, isPass, extraWhere, false);
        }

        public delegate string sumBookWording();
        public event sumBookWording _sumBookWordingArgs;

        public ArrayList _glViewJounalDetail(string accountCode, DateTime dateBegin, DateTime dateEnd, bool sumEndOfDate, int isPass, string extraWhere, bool sumbyBook)
        {
            ArrayList __result = new ArrayList();
            string __isPassQuery = " and " + _g.d.gl_journal_detail._is_pass + ((isPass == 0) ? " in (0,1)" : "=1");
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            // หายอดยกมา (ก่อนวันที่)
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select sum(" + _g.d.gl_journal_detail._debit + "-" + _g.d.gl_journal_detail._credit + ") as balance from " + _g.d.gl_journal_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code) + "=\'" + accountCode.ToUpper() + "\' and (" + _g.d.gl_journal_detail._doc_date + "<\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\')" + __isPassQuery + ((extraWhere.Length > 0) ? " and " + extraWhere : "") + " group by " + _g.d.gl_journal_detail._account_code));
            // รายละเอียดหลังยอดยกมาจนถึงวันที่เลือกสุดท้าย
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._book_code +
                    ",(select " + _g.d.gl_journal_book._name_1 + " from " + _g.d.gl_journal_book._table +
                    " where " + _g.d.gl_journal_book._table + "." + _g.d.gl_journal_book._code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._book_code + ") as book_name," +
                     _g.d.gl_journal_detail._doc_no + "," +
                     "(select " + _g.d.gl_journal._table + "." + _g.d.gl_journal._description + " from " + _g.d.gl_journal._table + " where " +
                     _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_date + " and " +
                     _g.d.gl_journal._table + "." + _g.d.gl_journal._book_code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._book_code + " and " +
                     _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_no + ")" +
                     " as " + _g.d.gl_journal_detail._account_name + "," + _g.d.gl_journal_detail._description +
                    "," + _g.d.gl_journal_detail._debit + "," + _g.d.gl_journal_detail._credit + "," + _g.d.gl_journal_detail._period_number + " from " + _g.d.gl_journal_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.gl_journal_detail._account_code) + "=\'" +
                    accountCode.ToUpper() + "\' and " + _g.d.gl_journal_detail._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(dateBegin) + "\' and " + _g.d.gl_journal_detail._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(dateEnd) + "\' " + __isPassQuery +
                    ((extraWhere.Length > 0) ? " and " + extraWhere : "") +
                    " order by " + _g.d.gl_journal_detail._doc_date + ((sumbyBook) ? "," + _g.d.gl_journal_detail._book_code : "") + "," + _g.d.gl_journal_detail._doc_no));
            __myquery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataSet __getBalance = (DataSet)_getData[0];
            DataSet __getDetail = (DataSet)_getData[1];
            // ประกอบร่าง
            _glViewJounalDetailListType __balanceLine = new _glViewJounalDetailListType();
            decimal __balance = 0;
            __balanceLine._lineType = _glViewJounalDetailListLineType.Balance;
            __balanceLine._balance = 0;
            if (__getBalance.Tables[0].Rows.Count > 0)
            {
                /// ยอดยกมา (จากการคำนวณ)
                __balanceLine._balance = (decimal)Double.Parse(__getBalance.Tables[0].Rows[0].ItemArray[0].ToString());
                __balance += __balanceLine._balance;
            }
            __balanceLine._desc = (MyLib._myGlobal._language == MyLib._languageEnum.Thai) ? "ยอดยกมา" : "Balance";
            __result.Add(__balanceLine);
            //
            int __columnDate = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._doc_date);
            int __columnNumber = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._doc_no);
            int __columnBook = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._book_code);
            int __columnBookName = __getDetail.Tables[0].Columns.IndexOf("book_name");
            int __columnAccountName = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._account_name);
            int __columnDesc = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._description);
            int __columnDebit = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._debit);
            int __columnCredit = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._credit);

            int __columnPeriodNumber = __getDetail.Tables[0].Columns.IndexOf(_g.d.gl_journal_detail._period_number);

            _oldDate = new DateTime(1000, 1, 1);
            _countDetailPerDay = 0;
            _sumDebit = 0;
            _sumCredit = 0;
            _countTotal = 0;
            _sumTotalDebit = 0;
            _sumTotalCredit = 0;

            string __oldBookCode = "";
            decimal __sumBookDebit = 0;
            decimal __sumBookCredit = 0;

            for (int __row = 0; __row < __getDetail.Tables[0].Rows.Count; __row++)
            {
                _glViewJounalDetailListType __detailLine = new _glViewJounalDetailListType();
                __detailLine._date = MyLib._myGlobal._convertDateFromQuery(__getDetail.Tables[0].Rows[__row].ItemArray[__columnDate].ToString());
                string __bookCode = __getDetail.Tables[0].Rows[__row].ItemArray[__columnBook].ToString();
                if (_oldDate.Year == 1000)
                {
                    _oldDate = __detailLine._date;
                }

                if (__oldBookCode == "")
                {
                    __oldBookCode = __bookCode;
                }

                //  toe sum by book singha
                if (sumbyBook && __oldBookCode.Equals(__bookCode) == false)
                {
                    string __desc = (MyLib._myGlobal._language == MyLib._languageEnum.Thai) ? "รวม" : "Total";
                    if (this._sumBookWordingArgs != null)
                    {
                        __desc = this._sumBookWordingArgs();
                    }
                    _glViewJounalDetailListType __total = _addSubTotal(__desc, __sumBookDebit, __sumBookCredit);

                    __total._balance = __balance;
                    __result.Add(__total);

                    __sumBookDebit = 0;
                    __sumBookCredit = 0;
                    __oldBookCode = __bookCode;

                }

                if (_oldDate.Equals(__detailLine._date) == false)
                {
                    if (sumEndOfDate)
                    {
                        _glViewJounalDetailListType __totalDay = (_glViewJounalDetailListType)_addDateTotal();
                        __totalDay._balance = __balance;
                        __result.Add(__totalDay);
                    }
                    // Clear ยอดรวม
                    _sumDebit = 0;
                    _sumCredit = 0;
                    _countDetailPerDay = 0;
                    //
                    _oldDate = __detailLine._date;
                }

                _countDetailPerDay++;
                _countTotal++;
                __detailLine._lineType = _glViewJounalDetailListLineType.Detail;
                __detailLine._number = __getDetail.Tables[0].Rows[__row].ItemArray[__columnNumber].ToString();
                __detailLine._book = __getDetail.Tables[0].Rows[__row].ItemArray[__columnBook].ToString() + "/" + __getDetail.Tables[0].Rows[__row].ItemArray[__columnBookName].ToString();
                __detailLine._bookCode = __getDetail.Tables[0].Rows[__row].ItemArray[__columnBook].ToString();
                __detailLine._bookName = __getDetail.Tables[0].Rows[__row].ItemArray[__columnBookName].ToString();
                __detailLine._period_number = MyLib._myGlobal._intPhase(__getDetail.Tables[0].Rows[__row].ItemArray[__columnPeriodNumber].ToString());
                __detailLine._desc = __getDetail.Tables[0].Rows[__row].ItemArray[__columnAccountName].ToString();
                string __getDesc = __getDetail.Tables[0].Rows[__row].ItemArray[__columnDesc].ToString();
                if (__getDesc.Length > 0)
                {
                    __detailLine._desc = __detailLine._desc + "/" + __getDesc;
                }
                __detailLine._debit = (decimal)Double.Parse(__getDetail.Tables[0].Rows[__row].ItemArray[__columnDebit].ToString());
                __detailLine._credit = (decimal)Double.Parse(__getDetail.Tables[0].Rows[__row].ItemArray[__columnCredit].ToString());
                _sumDebit += __detailLine._debit;
                _sumCredit += __detailLine._credit;
                __balance += (__detailLine._debit - __detailLine._credit);
                _sumTotalCredit += __detailLine._credit;
                _sumTotalDebit += __detailLine._debit;
                __detailLine._balance = __balance;

                if (sumbyBook)
                {
                    __sumBookDebit += __detailLine._debit;
                    __sumBookCredit += __detailLine._credit;
                }

                __result.Add(__detailLine);
            } //for
            if (_countDetailPerDay != 0)
            {
                if (sumEndOfDate)
                {
                    //__result.Add((_glViewJounalDetailListType)_addDateTotal());
                    //
                    _glViewJounalDetailListType __totalDay = (_glViewJounalDetailListType)_addDateTotal();
                    __totalDay._balance = __balance;
                    __result.Add(__totalDay);

                }

                if (sumbyBook)
                {
                    string __desc = (MyLib._myGlobal._language == MyLib._languageEnum.Thai) ? "รวม" : "Total";
                    if (this._sumBookWordingArgs != null)
                    {
                        __desc = this._sumBookWordingArgs();
                    }
                    _glViewJounalDetailListType __total = _addSubTotal(__desc, __sumBookDebit, __sumBookCredit);
                    __total._balance = __balance;
                    __result.Add(__total);
                }

            }

            if (_countTotal > 0)
            {
                string __desc = (MyLib._myGlobal._language == MyLib._languageEnum.Thai) ? string.Concat("รวม ", _countTotal.ToString(), " รายการ") : string.Concat("Total ", _countTotal.ToString(), " Records");
                __desc = string.Concat(__desc, " ", accountCode);

                // __result.Add((_glViewJounalDetailListType)_addSubTotal(__desc, _sumTotalDebit, _sumTotalCredit));
                _glViewJounalDetailListType __totalDay = (_glViewJounalDetailListType)_addSubTotal(__desc, _sumTotalDebit, _sumTotalCredit);
                __totalDay._balance = __balance;
                __result.Add(__totalDay);

            }
            return __result;
        }

        public MyLib._myTreeViewDragDrop _getChartOfAccountTreeView(MyLib._myTreeViewDragDrop treeNodeSource)
        {
            return _getChartOfAccountTreeView(treeNodeSource, "");
        }
        /// <summary>
        /// สร้าง TreeView จากข้อมูล gl_chart_of_account
        /// </summary>
        /// <param name="treeNodeSource"></param>
        /// <returns></returns>
        public MyLib._myTreeViewDragDrop _getChartOfAccountTreeView(MyLib._myTreeViewDragDrop treeNodeSource, string queryWhere)
        {
            MyLib._myFrameWork __frameWork = new MyLib._myFrameWork();
            string __query = "select " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._name_2 + ",case when " + _g.d.gl_chart_of_account._main_code + " is null then \'\' else " + _g.d.gl_chart_of_account._main_code + " end as " + _g.d.gl_chart_of_account._main_code + "," + _g.d.gl_chart_of_account._status + "," + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " " + queryWhere + " order by " + _g.d.gl_chart_of_account._main_code + "," + _g.d.gl_chart_of_account._code;
            DataSet __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query);
            //
            TreeNode __root = new TreeNode();
            __root.Text = "Root";
            _accountNode __newAccountNode = new _accountNode();
            __newAccountNode._code = "";
            __newAccountNode._name_1 = "บัญชีหลัก";
            __newAccountNode._name_2 = "Main Account";
            __newAccountNode._mainCode = "";
            __newAccountNode._accountStatus = 1;
            __root.Tag = __newAccountNode;
            treeNodeSource.Nodes.Add(__root);
            //
            int __account_code = __getData.Tables[0].Columns.IndexOf(_g.d.gl_chart_of_account._code);
            int __account_name_1 = __getData.Tables[0].Columns.IndexOf(_g.d.gl_chart_of_account._name_1);
            int __account_name_2 = __getData.Tables[0].Columns.IndexOf(_g.d.gl_chart_of_account._name_2);
            int __account_main_code = __getData.Tables[0].Columns.IndexOf(_g.d.gl_chart_of_account._main_code);
            int __account_status = __getData.Tables[0].Columns.IndexOf(_g.d.gl_chart_of_account._status);
            int __account_type = __getData.Tables[0].Columns.IndexOf(_g.d.gl_chart_of_account._account_type);
            for (int __loop = 0; __loop < __getData.Tables[0].Rows.Count; __loop++)
            {
                DataRow __getRow = __getData.Tables[0].Rows[__loop];
                TreeNode __node = new TreeNode();
                __newAccountNode = new _accountNode();
                __newAccountNode._code = __getRow.ItemArray[__account_code].ToString();
                __newAccountNode._mainCode = __getRow.ItemArray[__account_main_code].ToString();
                __newAccountNode._accountType = MyLib._myGlobal._intPhase(__getRow.ItemArray[__account_type].ToString());
                string __getField = __getRow.IsNull(__account_status) ? "0" : __getRow.ItemArray[__account_status].ToString();
                __newAccountNode._accountStatus = (__getField.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getField);
                __newAccountNode._name_1 = __getRow.ItemArray[__account_name_1].ToString();
                __newAccountNode._name_2 = __getRow.ItemArray[__account_name_2].ToString();
                __node.Tag = __newAccountNode;
                __node.Text = __newAccountNode._code + " : " + __newAccountNode._name_1 + " : " + __newAccountNode._name_2;
                TreeNode __getFirstNode = _findParentNode((TreeNode)treeNodeSource.Nodes[0], __newAccountNode._mainCode);
                if (__getFirstNode == null)
                {
                    treeNodeSource.Nodes.Add(__node);
                }
                else
                {
                    __getFirstNode.Nodes.Add(__node);
                }
            }
            treeNodeSource.Sort();
            return treeNodeSource;
        }

        protected TreeNode _findParentNode(TreeNode firstNode, string mainCode)
        {
            TreeNode __result = null;
            _accountNode __getNode = (_accountNode)firstNode.Tag;
            if (__getNode._code.Equals(mainCode))
            {
                __result = firstNode;
                return (firstNode);
            }
            for (int __loop = 0; __loop < firstNode.Nodes.Count; __loop++)
            {
                __result = _findParentNode((TreeNode)firstNode.Nodes[__loop], mainCode);
                if (__result != null)
                {
                    return __result;
                }
            }
            return __result;
        }
    }

    public enum _glViewJounalDetailListLineType : int
    {
        Balance,
        Detail,
        SubTotal
    }

    /// <summary>
    /// รายละเอียดเอกสาร (รายงานบัญชีแยกประเภท)
    /// </summary>
    public class _glViewJounalDetailListType
    {
        /// <summary>
        /// 0=ยอกยกมา,1=ข้อมูล,2=ยอดรวมวัน
        /// </summary>
        public _glViewJounalDetailListLineType _lineType;
        public DateTime _date;
        public string _number;
        public string _desc;
        public string _book;
        public string _bookName;
        public string _bookCode;
        public string _accountCode;
        public string _accountName;
        public int _accountStatus;
        public int _accountLevel;
        public int _accountType;
        public bool _show;
        public decimal _debit;
        public decimal _credit;
        public decimal _balance;
        public decimal _prevDebit;
        public decimal _prevCredit;
        public decimal _balanceDebit;
        public decimal _balanceCredit;
        public decimal _amount;
        public int _period_number;
        public List<_glViewJounalDetailListBranchType> _branch;

        public _glViewJounalDetailListType()
        {
            this._lineType = _glViewJounalDetailListLineType.Detail;
            this._date = new DateTime(1000, 1, 1);
            this._number = "";
            this._desc = "";
            this._book = "";
            this._bookCode = "";
            this._bookName = "";
            this._accountCode = "";
            this._accountName = "";
            this._accountStatus = 0;
            this._accountLevel = 0;
            this._accountType = 0;
            this._show = true;
            this._debit = 0;
            this._credit = 0;
            this._balance = 0;
            this._prevDebit = 0;
            this._prevCredit = 0;
            this._balanceDebit = 0;
            this._balanceCredit = 0;
            this._period_number = 0;
            this._amount = 0;
            this._branch = new List<_glViewJounalDetailListBranchType>();
        }
    }
    public class _glViewJounalDetailListBranchDepartmentType
    {
        public string _departmentCode = "";
        public string _departmentName = "";
        public decimal _allocateAmount = 0;
    }
    public class _glViewJounalDetailListBranchType
    {
        public string _branchCode = "";
        public decimal _debit = 0;
        public decimal _credit = 0;
        public decimal _amount = 0;
        public List<_glViewJounalDetailListBranchDepartmentType> _department = new List<_glViewJounalDetailListBranchDepartmentType>();
    }

    /// <summary>
    /// โครงสร้างบีญชีใช้กับ TreeNode เก็บไว้ใน Tag
    /// </summary>
    public class _accountNode
    {
        public string _code;
        public string _mainCode;
        public string _name_1;
        public string _name_2;
        public int _accountType;
        public int _accountStatus;
        public decimal _debit;
        public decimal _credit;
        public int _accountLevel;

        public _accountNode()
        {
            this._code = "";
            this._mainCode = "";
            this._name_1 = "";
            this._name_2 = "";
            this._accountType = 0;
            this._accountStatus = 0;
            this._debit = 0;
            this._credit = 0;
            this._accountLevel = 0;
        }
    }

    public class _accountSumType
    {
        public int _count;
        public decimal _debit;
        public decimal _credit;
        public decimal _prevDebit;
        public decimal _prevCredit;
        public decimal _balanceDebit;
        public decimal _balanceCredit;
        public decimal _amount;
        public List<_glViewJounalDetailListBranchType> _branch;
        public _accountSumType()
        {
            this._count = 0;
            this._debit = 0;
            this._credit = 0;
            this._prevDebit = 0;
            this._prevCredit = 0;
            this._balanceDebit = 0;
            this._balanceCredit = 0;
            this._amount = 0;
            this._branch = new List<_glViewJounalDetailListBranchType>();
        }
    }
}
