using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._trialBalanceBranchDepart
{
    public partial class _report : UserControl
    {
        ArrayList _getFromProcess;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __accountObject;
        //
        _condition _conditionScreen = new _condition();
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        Boolean _conditionAllAccount = false;
        Boolean _conditionAccountBalance = false;
        Boolean _condirionByPeriod = false;
        List<_g.g._branchListStruct> _branchList;
        //
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        //
        public _report()
        {
            InitializeComponent();
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            _view1._fontStandard = new Font("Angsana New", 4, FontStyle.Regular);
            //
            _showCondition();
        }

        void _showCondition()
        {
            string __dateBegin = _conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin);
            if (__dateBegin == "")
            {
                this._conditionScreen._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_begin, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                this._conditionScreen._screenJournalCondition1._setDataDate(_g.d.gl_resource._date_end, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1));

            }

            _conditionScreen.ShowDialog();
            _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_begin));
            _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._date_end));
            _conditionAllAccount = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._select_all_account).Equals("1") ? true : false;
            _conditionAccountBalance = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._select_account_balance).Equals("1") ? false : true;
            _condirionByPeriod = this._conditionScreen._screenJournalCondition1._getDataStr(_g.d.gl_resource._period).Equals("1") ? true : false;
            this._getFromProcess = null;

            this._view1._buildReport(SMLReport._report._reportType.Standard);

        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _view1__getDataObject()
        {
            int __accountCode = _view1._findColumn(__accountObject, _g.d.gl_list_view._account_code);
            int __accountName1 = _view1._findColumn(__accountObject, _g.d.gl_list_view._account_name);
            int __debit = _view1._findColumn(__accountObject, _g.d.gl_list_view._debit);
            //
            try
            {
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__accountObject._columnList[0];
                //this._view1._reportProgressBar.Value = 0;
                int __totalRecords = this._getFromProcess.Count;
                for (int __accountRow = 0; __accountRow < __totalRecords; __accountRow++)
                {
                    SMLProcess._glViewJounalDetailListType __getRow = (SMLProcess._glViewJounalDetailListType)this._getFromProcess[__accountRow];
                    if (__getRow._show)
                    {
                        SMLReport._report._objectListType __accountDataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(__accountObject, __accountDataObject);
                        Font __newFont = new Font(__getColumn._fontData, (__getRow._accountStatus == 0) ? FontStyle.Regular : FontStyle.Bold);
                        int __accountLevel = (__getRow._accountLevel == 0) ? 0 : ((__getRow._accountLevel - 1) * 2);
                        if (__accountLevel < 0)
                        {
                            __accountLevel = 0;
                        }
                        SMLReport._report._columnBorder __newBorder = (__getRow._lineType == SMLProcess._glViewJounalDetailListLineType.SubTotal) ? SMLReport._report._columnBorder.TopBottom : SMLReport._report._columnBorder.None;
                        _view1._addDataColumn(__accountObject, __accountDataObject, __accountCode, __getRow._accountCode, __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__accountObject, __accountDataObject, __accountName1, __getRow._accountName, __newFont, SMLReport._report._cellAlign.Default, __accountLevel, SMLReport._report._cellType.String);
                        decimal __amount = __getRow._amount;
                        if (__getRow._accountType == 4)
                        {
                            __amount = __amount * -1;
                        }
                        _view1._addDataColumn(__accountObject, __accountDataObject, __debit, MyLib._myUtil._moneyFormat(__amount, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                        for (int __branchLoop = 0; __branchLoop < this._branchList.Count; __branchLoop++)
                        {
                            _view1._addDataColumn(__accountObject, __accountDataObject, _view1._findColumn(__accountObject, _g.d.gl_list_view._debit + this._branchList[__branchLoop]._code), MyLib._myUtil._moneyFormat(__getRow._branch[__branchLoop]._amount, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                            for (int __departmentLoop = 0; __departmentLoop < this._branchList[__branchLoop]._department.Count; __departmentLoop++)
                            {
                                decimal __value = (decimal)0;
                                if (__branchLoop < __getRow._branch.Count)
                                {
                                    if (__departmentLoop < __getRow._branch[__branchLoop]._department.Count)
                                    {
                                        __value = __getRow._branch[__branchLoop]._department[__departmentLoop]._allocateAmount;
                                    }
                                }
                                if (__getRow._accountType == 4)
                                {
                                    __value = __value * -1;
                                }
                                _view1._addDataColumn(__accountObject, __accountDataObject, _view1._findColumn(__accountObject, _g.d.gl_list_view._debit + this._branchList[__branchLoop]._code + this._branchList[__branchLoop]._department[__departmentLoop]._code), MyLib._myUtil._moneyFormat(__value, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                                //                                _view1._addDataColumn(__accountObject, __accountDataObject, _view1._findColumn(__accountObject, _g.d.gl_list_view._debit + this._branchList[__loop]._code + this._branchList[__loop]._department[__loop2]._code), MyLib._myUtil._moneyFormat(__getRow._branch[__loop]._department[__loop2]._allocateAmount, _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, __newBorder, SMLReport._report._cellType.Number);
                            }
                        }
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1.__excelFlieName = "งบกำไรขาดทุนแยกสาขา-แผนก ประจำวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่  : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "งบกำไรขาดทุนแยกสาขา-แผนก ประจำวันที่ : " + MyLib._myGlobal._convertDateToString(_conditionDateBegin, false) + " ถึงวันที่  : " + MyLib._myGlobal._convertDateToString(_conditionDateEnd, false), SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
            {
                // นับแผนก
                int __departmentCount = this._branchList.Count + 1;
                for (int __loop = 0; __loop < this._branchList.Count; __loop++)
                {
                    __departmentCount += this._branchList[__loop]._department.Count;
                }
                // Column บน
                SMLReport._report._objectListType __columnHeaderObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                _view1._addColumn(__columnHeaderObject, 5, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, null, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                _view1._addColumn(__columnHeaderObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, null, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center);
                float __widthPersent = (float)85 / (float)(__departmentCount);
                _view1._addColumn(__columnHeaderObject, __widthPersent, true, SMLReport._report._columnBorder.LeftBottom, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, "ยอดงวด", _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center, true, _view1._fontStandard, Color.Black, 1);
                for (int __loop = 0; __loop < this._branchList.Count; __loop++)
                {
                    _view1._addColumn(__columnHeaderObject, __widthPersent * (float)(this._branchList[__loop]._department.Count + 1), true, SMLReport._report._columnBorder.LeftRightBottom, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, this._branchList[__loop]._name, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Center, true, _view1._fontStandard, Color.Black, this._branchList[__loop]._department.Count + 1);
                }
                // Column ข้อมูล
                __accountObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                _view1._addColumn(__accountObject, 5, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_code, _g.d.gl_list_view._account_code, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, 10, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._account_name, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._account_name, _g.d.gl_list_view._account_name, SMLReport._report._cellAlign.Left);
                _view1._addColumn(__accountObject, __widthPersent, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._debit, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Center);
                for (int __loop = 0; __loop < this._branchList.Count; __loop++)
                {
                    _view1._addColumn(__accountObject, __widthPersent, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._debit + this._branchList[__loop]._code, _g.d.gl_list_view._table + "." + _g.d.gl_list_view._balance, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Center);
                    for (int __loop2 = 0; __loop2 < this._branchList[__loop]._department.Count; __loop2++)
                    {
                        _view1._addColumn(__accountObject, __widthPersent, true, SMLReport._report._columnBorder.Left, SMLReport._report._columnBorder.Left, _g.d.gl_list_view._debit + this._branchList[__loop]._code + this._branchList[__loop]._department[__loop2]._code, this._branchList[__loop]._department[__loop2]._name, _g.d.gl_list_view._debit, SMLReport._report._cellAlign.Center);
                    }
                }
                return true;
            }
            return false;
        }

        bool _view1__loadData()
        {
            //
            if (this._getFromProcess == null)
            {
                // select account_code,branch_code,department_code,sum(allocate_amount) as allocate_amount from (select branch_code,department_code,account_code,case when debit=0 then allocate_amount else allocate_amount*-1 end as allocate_amount from (SELECT coalesce((select branch_code from gl_journal_detail where gl_journal_detail.doc_no = gl_journal_depart_list.doc_no and gl_journal_detail.line_number = gl_journal_depart_list.line_number), 'ERR') as branch_code,coalesce((select account_code from gl_journal_detail where gl_journal_detail.doc_no = gl_journal_depart_list.doc_no and gl_journal_detail.line_number = gl_journal_depart_list.line_number), 'ERR') as account_code,coalesce((select debit from gl_journal_detail where gl_journal_detail.doc_no = gl_journal_depart_list.doc_no and gl_journal_detail.line_number = gl_journal_depart_list.line_number), 0) as debit,allocate_amount,department_code from gl_journal_depart_list) as q1) as q2 group by account_code,branch_code,department_code order by account_code,branch_code,department_code
                this.Cursor = Cursors.WaitCursor;
                string __where3 = "";
                if (this._condirionByPeriod)
                {
                    __where3 = _g.d.gl_journal_depart_list._doc_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(_conditionDateBegin) + "\' and " + _g.d.gl_journal_depart_list._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(_conditionDateEnd) + "\'";
                }
                else
                {
                    __where3 = _g.d.gl_journal_depart_list._doc_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(_conditionDateEnd) + "\'";
                }
                StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select branch_code,COALESCE((select name_1 from erp_branch_list where erp_branch_list.code=branch_code),'ERR.'||branch_code) as name_1 from gl_journal_detail where branch_code is not null and branch_code<>'' group by branch_code order by branch_code"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select branch_code, COALESCE((select name_1 from erp_branch_list where erp_branch_list.code = branch_code), 'ERR.' || branch_code) as branch_name, department_code, COALESCE((select name_1 from erp_department_list where erp_department_list.code = department_code), 'ERR.' || department_code) as department_name from(SELECT coalesce((select branch_code from gl_journal_detail where gl_journal_detail.doc_no = gl_journal_depart_list.doc_no and gl_journal_detail.line_number = gl_journal_depart_list.line_number), 'ERR') as branch_code,department_code from gl_journal_depart_list where " + __where3 + ") as q1 group by branch_code,department_code order by branch_code, department_code"));
                __myquery.Append("</node>");
                ArrayList __getData = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                DataTable __branchCodeTable = ((DataSet)__getData[0]).Tables[0];
                DataTable __departmentTable = ((DataSet)__getData[1]).Tables[0];

                this._branchList = new List<_g.g._branchListStruct>();
                try
                {
                    for (int __row = 0; __row < __branchCodeTable.Rows.Count; __row++)
                    {
                        _g.g._branchListStruct __data = new _g.g._branchListStruct();
                        //
                        _g.g._departmentListStruct __departmentCenterData = new _g.g._departmentListStruct();
                        __departmentCenterData._code = "CENTER";
                        __departmentCenterData._name = "CENTER";
                        __data._department.Add(__departmentCenterData);
                        //
                        __data._code = __branchCodeTable.Rows[__row][0].ToString();
                        __data._name = __branchCodeTable.Rows[__row][1].ToString();
                        if (__departmentTable.Rows.Count > 0)
                        {
                            DataRow[] __department = __departmentTable.Select("branch_code='" + __data._code + "'");
                            for (int __loop = 0; __loop < __department.Length; __loop++)
                            {
                                _g.g._departmentListStruct __departmentData = new _g.g._departmentListStruct();
                                __departmentData._code = __department[__loop]["department_code"].ToString();
                                __departmentData._name = __department[__loop]["department_name"].ToString();
                                __data._department.Add(__departmentData);
                            }
                        }
                        this._branchList.Add(__data);
                    }
                    SMLProcess._glProcess __process = new SMLProcess._glProcess();
                    this._getFromProcess = __process._glViewTrialBalanceBranchDepartment(_conditionDateBegin, _conditionDateEnd, __process._getChartOfAccountTreeView(new MyLib._myTreeViewDragDrop(), " where " + _g.d.gl_chart_of_account._account_type + " in (3,4)"), this._conditionAllAccount, 0, this._branchList, (this._condirionByPeriod) ? 0 : 1);
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
            this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            try
            {
                _view1._buildReport(SMLReport._report._reportType.Standard);
            }
            catch
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถทำรายการได้"));
            }
            this.Cursor = Cursors.Default;
        }
    }
}
